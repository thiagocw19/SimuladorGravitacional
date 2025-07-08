namespace SimuladorGravitacional
{
    public class Corpo
    {
        public string Nome { get; set; }
        public double Massa { get; set; }
        public double Raio { get; set; }
        public double Densidade { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }
        public double ForcaX { get; set; }
        public double ForcaY { get; set; }
        public bool Removido { get; set; } = false; // Marca para remoção segura

        public Corpo(string nome, double massa, double densidade, double posX, double posY)
        {
            Nome = nome;
            Massa = massa;
            Densidade = densidade;
            Raio = Math.Pow((3 * massa) / (4 * Math.PI * densidade), 1.0 / 3.0);
            PosX = posX;
            PosY = posY;
            VelX = 0.0;
            VelY = 0.0;
            ForcaX = 0.0;
            ForcaY = 0.0;
        }

        public static Corpo operator +(Corpo a, Corpo b)
        {
            double novaMassa = a.Massa + b.Massa;
            double novaPosX = (a.PosX * a.Massa + b.PosX * b.Massa) / novaMassa; // Posição média ponderada
            double novaPosY = (a.PosY * a.Massa + b.PosY * b.Massa) / novaMassa; // Posição média ponderada

            // Conservação de momento
            double novaVelX = (a.VelX * a.Massa + b.VelX * b.Massa) / novaMassa;
            double novaVelY = (a.VelY * a.Massa + b.VelY * b.Massa) / novaMassa;

            // Conservação de densidade média
            double novaDensidade = (a.Densidade * a.Massa + b.Densidade * b.Massa) / novaMassa;

            return new Corpo($"Corpo Colidido {Guid.NewGuid().ToString().Substring(0, 8)}",
                             novaMassa, novaDensidade, novaPosX, novaPosY)
            {
                VelX = novaVelX,
                VelY = novaVelY
            };
        }

        public double Distancia(Corpo outro)
        {
            return Math.Sqrt(Math.Pow(this.PosX - outro.PosX, 2) + Math.Pow(this.PosY - outro.PosY, 2));
        }

        public bool Colidiu(Corpo outro)
        {
            return Distancia(outro) <= (this.Raio + outro.Raio);
        }

        public void AtualizarPosicao()
        {
            // Atualiza a posição do corpo
            PosX += VelX;
            PosY += VelY;
        }

        // Método para lidar com bordas da tela
        public void LidarComBordas(int larguraTela, int alturaTela, bool rebater = true)
        {
            if (rebater)
            {
                // Rebater nas bordas
                if (PosX - Raio < 0)
                {
                    PosX = Raio;
                    VelX = Math.Abs(VelX) * 0.8; // Redução de velocidade após colisão
                }
                else if (PosX + Raio > larguraTela)
                {
                    PosX = larguraTela - Raio;
                    VelX = -Math.Abs(VelX) * 0.8;
                }

                if (PosY - Raio < 0)
                {
                    PosY = Raio;
                    VelY = Math.Abs(VelY) * 0.8;
                }
                else if (PosY + Raio > alturaTela)
                {
                    PosY = alturaTela - Raio;
                    VelY = -Math.Abs(VelY) * 0.8;
                }
            }
            else
            {
                // Permitir que o corpo continue além das bordas (com limite)
                if (PosX < -larguraTela || PosX > 2 * larguraTela)
                {
                    VelX = -VelX * 0.5;
                }

                if (PosY < -alturaTela || PosY > 2 * alturaTela)
                {
                    VelY = -VelY * 0.5;
                }
            }
        }
    }
}