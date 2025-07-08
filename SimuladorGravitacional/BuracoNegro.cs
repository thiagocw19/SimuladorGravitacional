//using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;

//namespace SimuladorGravitacional
//{
//    // Enum para diferenciar tipos de corpos celestes
//    public enum TipoCorpo
//    {
//        Planeta,
//        Estrela,
//        BuracoNegro
//    }

//    // Extensão da classe Corpo para definir propriedades específicas
//    public class BuracoNegro : Corpo
//    {
//        public double RaioHorizonteEventos { get; private set; }
//        public double FatorEscalaVisual { get; set; } = 2.0; // Fator para visualização (maior que o raio real)

//        // Construtor para Buraco Negro
//        public BuracoNegro(string nome, double massa, double posX, double posY)
//            : base(nome, massa, 100000, posX, posY) // Buracos negros têm densidade extrema
//        {
//            // Configurar o buraco negro
//            Tipo = TipoCorpo.BuracoNegro;
//            VelX = 0; // Buraco negro estático
//            VelY = 0; // Buraco negro estático

//            // Calcular o raio do horizonte de eventos
//            // Simplificado: proporcional à massa do buraco negro
//            CalcularRaioHorizonteEventos();
//        }

//        // Calcula o raio do horizonte de eventos com base na massa
//        private void CalcularRaioHorizonteEventos()
//        {
//            // Simplificação: raio proporcional à massa
//            // Na realidade seria baseado na equação de Schwarzchild: Rs = 2GM/c²
//            RaioHorizonteEventos = Math.Sqrt(Massa) * 0.8;
//        }

//        // Método para verificar se um corpo está dentro do horizonte de eventos
//        public bool EstaDentroHorizonteEventos(Corpo outro)
//        {
//            double distancia = Distancia(outro);
//            return distancia <= RaioHorizonteEventos;
//        }

//        // Sobrescreve o método de colisão para considerar o horizonte de eventos
//        public override bool Colidiu(Corpo outro)
//        {
//            // Para buracos negros, consideramos colisão se o objeto estiver dentro do horizonte de eventos
//            return EstaDentroHorizonteEventos(outro);
//        }

//        // Método para "engolir" um corpo que entrou no horizonte de eventos
//        public void EngolirCorpo(Corpo corpo)
//        {
//            // Um buraco negro cresce quando engole massa
//            Massa += corpo.Massa;

//            // Recalcular o raio do horizonte de eventos
//            CalcularRaioHorizonteEventos();
//        }

//        // Método para desenhar o buraco negro e seu horizonte de eventos
//        public void Desenhar(Graphics g, double fatorTamanho)
//        {
//            // Calcular tamanho visual
//            double tamanhoNucleo = Massa * fatorTamanho * 0.5; // Núcleo menor
//            tamanhoNucleo = Math.Min(tamanhoNucleo, 60); // Limite máximo
//            tamanhoNucleo = Math.Max(tamanhoNucleo, 10); // Limite mínimo

//            // Tamanho do horizonte de eventos na visualização
//            double tamanhoHorizonte = RaioHorizonteEventos * 2 * FatorEscalaVisual;

//            // Coordenadas de desenho
//            float x = (float)(PosX - tamanhoNucleo / 2);
//            float y = (float)(PosY - tamanhoNucleo / 2);

//            float xHorizonte = (float)(PosX - tamanhoHorizonte / 2);
//            float yHorizonte = (float)(PosY - tamanhoHorizonte / 2);

//            // Desenhar horizonte de eventos (efeito de distorção espacial)
//            using (GraphicsPath path = new GraphicsPath())
//            {
//                path.AddEllipse(xHorizonte, yHorizonte, (float)tamanhoHorizonte, (float)tamanhoHorizonte);

//                // Criar uma região de gradiente para simular a curvatura do espaço-tempo
//                using (PathGradientBrush brush = new PathGradientBrush(path))
//                {
//                    brush.CenterColor = Color.FromArgb(80, 0, 0, 0);
//                    brush.SurroundColors = new Color[] { Color.FromArgb(5, 0, 0, 40) };

//                    g.FillEllipse(brush, xHorizonte, yHorizonte, (float)tamanhoHorizonte, (float)tamanhoHorizonte);
//                }
//            }

//            // Desenhar o núcleo do buraco negro (com efeito de blur)
//            for (int i = 0; i < 3; i++)
//            {
//                float blurFactor = (3 - i) * 1.5f;
//                float blurSize = (float)tamanhoNucleo + blurFactor;
//                float blurX = (float)(PosX - blurSize / 2);
//                float blurY = (float)(PosY - blurSize / 2);

//                using (Brush blurBrush = new SolidBrush(Color.FromArgb(100 - i * 30, 0, 0, 0)))
//                {
//                    g.FillEllipse(blurBrush, blurX, blurY, blurSize, blurSize);
//                }
//            }

//            // Desenhar o centro do buraco negro
//            using (Brush coreBrush = new SolidBrush(Color.Black))
//            {
//                g.FillEllipse(coreBrush, x, y, (float)tamanhoNucleo, (float)tamanhoNucleo);
//            }

//            // Desenhar uma fina borda branca ao redor do disco de acreção
//            float tamanhoDiscoAcrecao = (float)(tamanhoNucleo * 1.5);
//            float xDisco = (float)(PosX - tamanhoDiscoAcrecao / 2);
//            float yDisco = (float)(PosY - tamanhoDiscoAcrecao / 2);

//            using (Pen acrecaoPen = new Pen(Color.FromArgb(120, 180, 180, 255), 0.5f))
//            {
//                g.DrawEllipse(acrecaoPen, xDisco, yDisco, tamanhoDiscoAcrecao, tamanhoDiscoAcrecao);
//            }
//        }
//    }
//}