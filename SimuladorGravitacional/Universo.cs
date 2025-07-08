namespace SimuladorGravitacional
{
    public class Universo
    {
        public List<Corpo> corpos;
        public List<Corpo> corposParaAdicionar;
        public int QuantidadeColididos;
        private readonly object _lockObj = new object();
        public double constanteGravitacional = 10; // Constante gravitacional ajustável

        public Universo()
        {
            corpos = new List<Corpo>();
            corposParaAdicionar = new List<Corpo>();
            QuantidadeColididos = 0;
        }

        public void AdicionarCorpo(Corpo corpo)
        {
            lock (_lockObj)
            {
                corpos.Add(corpo);
            }
        }

        public void SetConstanteGravitacional(double valor)
        {
            constanteGravitacional = valor;
        }

        public void Atualizar(int larguraTela, int alturaTela, bool rebaterNasBordas = true)
        {
            // Cria uma cópia segura para iteração
            List<Corpo> corposCopia;
            lock (_lockObj)
            {
                corposCopia = corpos.Where(c => !c.Removido).ToList();
            }

            // Resetar as forças antes de calcular
            Parallel.ForEach(corposCopia, corpo =>
            {
                corpo.ForcaX = 0.0;
                corpo.ForcaY = 0.0;
            });

            // Calcular forças gravitacionais entre todos os pares de corpos
            Parallel.For(0, corposCopia.Count, i =>
            {
                for (int j = i + 1; j < corposCopia.Count; j++)
                {
                    var corpo1 = corposCopia[i];
                    var corpo2 = corposCopia[j];

                    // Evitar cálculos com corpos marcados para remoção
                    if (corpo1.Removido || corpo2.Removido) continue;

                    // Verificar colisão
                    if (corpo1.Colidiu(corpo2))
                    {
                        // Tratar colisão
                        TratamentoColisao(corpo1, corpo2);
                    }
                    else
                    {
                        // Calcular força gravitacional
                        CalcularForcaGravitacional(corpo1, corpo2);
                    }
                }
            });

            // Atualizar velocidades e posições
            Parallel.ForEach(corposCopia, corpo =>
            {
                if (corpo.Removido) return;

                // Atualizar velocidade
                if (corpo.Massa > 0)
                {
                    corpo.VelX += corpo.ForcaX / corpo.Massa;
                    corpo.VelY += corpo.ForcaY / corpo.Massa;

                    // Limitar velocidade para evitar instabilidade
                    LimitarVelocidade(corpo);
                }

                // Atualizar posição
                corpo.AtualizarPosicao();

                // Lidar com bordas da tela
                corpo.LidarComBordas(larguraTela, alturaTela, rebaterNasBordas);
            });

            // Adicionar novos corpos e remover os marcados
            lock (_lockObj)
            {
                // Adicionar novos corpos (resultantes de colisões)
                if (corposParaAdicionar.Count > 0)
                {
                    corpos.AddRange(corposParaAdicionar);
                    corposParaAdicionar.Clear();
                }

                // Remover corpos marcados
                corpos.RemoveAll(c => c.Removido);
            }
        }

        private void CalcularForcaGravitacional(Corpo corpo1, Corpo corpo2)
        {
            double distanciaX = corpo2.PosX - corpo1.PosX;
            double distanciaY = corpo2.PosY - corpo1.PosY;
            double distanciaQuadrada = distanciaX * distanciaX + distanciaY * distanciaY;

            // Evitar divisão por zero e forças excessivas a curtas distâncias
            if (distanciaQuadrada < 0.0001)
            {
                distanciaQuadrada = 0.0001;
            }

            double distancia = Math.Sqrt(distanciaQuadrada);

            // Calcula a força gravitacional total entre os corpos
            double forca = constanteGravitacional * (corpo1.Massa * corpo2.Massa) / distanciaQuadrada;

            // Componentes da força
            double forcaX = forca * distanciaX / distancia;
            double forcaY = forca * distanciaY / distancia;

            // Aplicar força aos corpos (terceira lei de Newton: ação e reação)
            corpo1.ForcaX += forcaX;
            corpo1.ForcaY += forcaY;
            corpo2.ForcaX -= forcaX;
            corpo2.ForcaY -= forcaY;
        }

        private void TratamentoColisao(Corpo corpo1, Corpo corpo2)
        {
            // Evitar processamento de corpos já marcados para remoção
            if (corpo1.Removido || corpo2.Removido) return;

            // Marcar os corpos para remoção
            corpo1.Removido = true;
            corpo2.Removido = true;

            // Criar novo corpo resultante da colisão
            Corpo novoCorpo = corpo1 + corpo2;

            // Adicionar o novo corpo à lista de corpos para adicionar
            lock (_lockObj)
            {
                corposParaAdicionar.Add(novoCorpo);
                QuantidadeColididos++;
            }
        }

        private void LimitarVelocidade(Corpo corpo)
        {
            // Limitar velocidade para evitar instabilidade na simulação
            double velocidadeMax = 20.0;
            double velocidadeAtual = Math.Sqrt(corpo.VelX * corpo.VelX + corpo.VelY * corpo.VelY);

            if (velocidadeAtual > velocidadeMax)
            {
                double fator = velocidadeMax / velocidadeAtual;
                corpo.VelX *= fator;
                corpo.VelY *= fator;
            }
        }
    }
}
