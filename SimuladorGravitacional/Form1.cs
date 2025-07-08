using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimuladorGravitacional
{
    public partial class Form1 : Form
    {
        private Universo universo;
        private int iteracoes;
        private readonly double fatorDeTamanho = 0.01;
        private List<Corpo> corposParaDesenhar;
        private System.Windows.Forms.Timer simulacaoTimer;
        private Corpo corpoSelecionado;
        private bool simulacaoRodando = false;
        private bool rebaterNasBordas = true;

        public Form1()
        {
            InitializeComponent();

            // Configurar o universo
            universo = new Universo();
            corposParaDesenhar = new List<Corpo>();

            // Configurar timer de simulação
            simulacaoTimer = new System.Windows.Forms.Timer();
            simulacaoTimer.Interval = 30;
            simulacaoTimer.Tick += SimulacaoTimer_Tick;

            // Configurar interface
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Aqui você pode adicionar controles para:
            // - Ajustar a constante gravitacional
            // - Opção para rebater nas bordas ou não
            // - Mostrar/ocultar trajetórias
            // - Ajustar escala visual
            // etc.

            // Certifique-se de que as caixas de texto são limpas e iniciadas com valores padrão
            LimparCamposInfo();

            // Adicionar manipulador do evento FormClosing
            this.FormClosing += Form1_FormClosing;
        }

        private void LimparCamposInfo()
        {
            // Limpar campos de informação
            if (PosX_Box != null) PosX_Box.Text = "0";
            if (PosY_Box != null) PosY_Box.Text = "0";
            if (VelX_Box != null) VelX_Box.Text = "0";
            if (VelY_Box != null) VelY_Box.Text = "0";
            if (Forcax_box != null) Forcax_box.Text = "0";
            if (Forcay_box != null) Forcay_box.Text = "0";
        }

        private void SimulacaoTimer_Tick(object sender, EventArgs e)
        {
            AtualizarEDesenhar();
        }

        public void AtualizarEDesenhar()
        {
            // Atualiza a simulação
            universo.Atualizar(this.ClientSize.Width, this.ClientSize.Height, rebaterNasBordas);

            // Incrementa o contador de iterações
            iteracoes++;

            // Atualizar informações na interface
            AtualizarInfoInterface();

            // Atualizar a visualização a cada 4 iterações para melhorar desempenho
            if (iteracoes % 4 == 0)
            {
                // Copia segura dos corpos para desenho
                lock (corposParaDesenhar)
                {
                    corposParaDesenhar.Clear();
                    corposParaDesenhar.AddRange(universo.corpos);
                }

                // Redesenha a tela
                this.Invalidate();
            }

            // Atualizar informações do corpo selecionado
            AtualizarInfoCorpoSelecionado();
        }

        private void AtualizarInfoInterface()
        {
            // Atualizar contadores e informações
            if (Iteracoes_Box != null) Iteracoes_Box.Text = iteracoes.ToString();
            if (velAtual != null) velAtual.Text = simulacaoTimer.Interval.ToString();

            // Mostrar quantidade de corpos e colisões
            if (universo != null)
            {                
                labelCorpos2.Text = $"Corpos: {universo.corpos.Count}";
                labelCG.Text = $"Constante Gravitacional: {universo.constanteGravitacional}";
                labelColisao.Text = $"Colisões: {universo.QuantidadeColididos}";
            }
        }

        private void AtualizarInfoCorpoSelecionado()
        {
            if (corpoSelecionado != null && !corpoSelecionado.Removido)
            {
                // Atualizar informações do corpo selecionado
                if (PosX_Box != null) PosX_Box.Text = corpoSelecionado.PosX.ToString("F2");
                if (PosY_Box != null) PosY_Box.Text = corpoSelecionado.PosY.ToString("F2");
                if (VelX_Box != null) VelX_Box.Text = corpoSelecionado.VelX.ToString("F2");
                if (VelY_Box != null) VelY_Box.Text = corpoSelecionado.VelY.ToString("F2");
                if (Forcax_box != null) Forcax_box.Text = corpoSelecionado.ForcaX.ToString("F2");
                if (Forcay_box != null) Forcay_box.Text = corpoSelecionado.ForcaY.ToString("F2");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Desenhar corpos
            DesenharCorpos(e.Graphics);
        }

        public void DesenharCorpos(Graphics g)
        {
            const double tamanhoMaximo = 100.0;

            // Ajustar antialiasing para melhor qualidade visual
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Desenhar os corpos
            lock (corposParaDesenhar)
            {
                foreach (Corpo corpo in corposParaDesenhar)
                {
                    // Calcular o tamanho da elipse
                    double tamanho = corpo.Massa * fatorDeTamanho;
                    tamanho = Math.Min(tamanho, tamanhoMaximo);
                    tamanho = Math.Max(tamanho, 1.0); // Tamanho mínimo

                    // Calcular as coordenadas de desenho
                    float x = (float)(corpo.PosX - tamanho / 2);
                    float y = (float)(corpo.PosY - tamanho / 2);

                    // Desenhar círculo de influência gravitacional
                    g.DrawEllipse(new Pen(Color.Black),
                                   x - 10, y - 10, (float)tamanho + 20, (float)tamanho + 20);

                    // Desenhar corpo
                    Color corpoColor = Color.Black;

                    // Destacar corpo selecionado
                    if (corpoSelecionado == corpo)
                    {
                        g.DrawEllipse(new Pen(Color.White, 2f),
                                     x - 2, y - 2, (float)tamanho + 4, (float)tamanho + 4);
                        corpoColor = Color.Yellow;
                    }

                    // Desenhar elipse transparente
                    using (Brush brush = new SolidBrush(Color.FromArgb(180, corpoColor)))
                    {
                        g.FillEllipse(brush, x, y, (float)tamanho, (float)tamanho);
                    }

                    // Desenhar borda
                    g.DrawEllipse(new Pen(corpoColor), x, y, (float)tamanho, (float)tamanho);

                    // Desenhar vetor velocidade
                    DesenharVetor(g, corpo.PosX, corpo.PosY, corpo.VelX, corpo.VelY, Color.Blue);
                }
            }
        }

        private void DesenharVetor(Graphics g, double x, double y, double vx, double vy, Color cor)
        {
            // Desenhar um vetor representando a velocidade ou força
            float escala = 5.0f; // Escala do vetor para melhor visualização
            float x2 = (float)(x + vx * escala);
            float y2 = (float)(y + vy * escala);

            // Desenhar linha do vetor
            g.DrawLine(new Pen(cor, 1.5f), (float)x, (float)y, x2, y2);

            // Desenhar ponta de seta
            float tamanhoSeta = 5.0f;
            float angulo = (float)Math.Atan2(y2 - y, x2 - x);

            PointF[] pontaSeta = {
                new PointF(x2, y2),
                new PointF(x2 - tamanhoSeta * (float)Math.Cos(angulo - Math.PI/6),
                          y2 - tamanhoSeta * (float)Math.Sin(angulo - Math.PI/6)),
                new PointF(x2 - tamanhoSeta * (float)Math.Cos(angulo + Math.PI/6),
                          y2 - tamanhoSeta * (float)Math.Sin(angulo + Math.PI/6)),
            };

            g.FillPolygon(new SolidBrush(cor), pontaSeta);
        }

        public void Iniciar_bt_Click(object sender, EventArgs e)
        {
            // Limpa o universo e reinicia as iterações
            universo = new Universo();
            iteracoes = 0;

            if (int.TryParse(Corpos_Box.Text, out int quantidade) && quantidade > 0)
            {
                // Gerar corpos aleatórios
                GerarCorposAleatorios(quantidade);

                // Iniciar simulação
                IniciarSimulacao();
            }
            else
            {
                MessageBox.Show("Por favor, insira um número válido de corpos.");
            }
        }

        private void GerarCorposAleatorios(int quantidade)
        {
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                // Gerar massa com distribuição mais realista
                double massa = random.Next(10, 1001);
                double densidade = random.Next(1000, 10000);

                // Posição aleatória (com margem para evitar bordas)
                double posX = random.Next(50, this.ClientSize.Width - 50);
                double posY = random.Next(50, this.ClientSize.Height - 50);

                // Velocidades aleatórias
                double velX = random.NextDouble() * 2 - 1; // Entre -1 e 1
                double velY = random.NextDouble() * 2 - 1; // Entre -1 e 1

                // Criar corpo
                Corpo novoCorpo = new Corpo($"Corpo {i + 1}", massa, densidade, posX, posY)
                {
                    VelX = velX,
                    VelY = velY
                };

                universo.AdicionarCorpo(novoCorpo);
            }
        }

        private void IniciarSimulacao()
        {
            // Iniciar o timer de simulação
            simulacaoTimer.Start();
            simulacaoRodando = true;

            // Atualizar interface
            AtualizarInfoInterface();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Verificar se há algum corpo na posição do clique
            corpoSelecionado = null;

            foreach (Corpo corpo in universo.corpos)
            {
                double distancia = Math.Sqrt(Math.Pow(e.X - corpo.PosX, 2) + Math.Pow(e.Y - corpo.PosY, 2));
                double raioVisual = corpo.Massa * fatorDeTamanho / 2;

                if (distancia <= raioVisual)
                {
                    corpoSelecionado = corpo;
                    break;
                }
            }

            // Atualizar informações do corpo selecionado
            AtualizarInfoCorpoSelecionado();

            // Redesenhar para destacar o corpo selecionado
            this.Invalidate();
        }

        public void Parar_bt_Click(object sender, EventArgs e)
        {
            // Parar a simulação
            simulacaoTimer.Stop();
            simulacaoRodando = false;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Carregar corpos de um arquivo
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos do Universo (*.uni)|*.uni|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione um arquivo de universo";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        CarregarArquivoIni(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar o arquivo: {ex.Message}");
                    }
                }
            }
        }

        public void CarregarArquivoIni(string filePath)
        {
            try
            {
                // Parar simulação se estiver rodando
                if (simulacaoRodando)
                {
                    simulacaoTimer.Stop();
                }

                // Criar novo universo
                universo = new Universo();
                iteracoes = 0;

                // Ler arquivo
                string[] linhas = System.IO.File.ReadAllLines(filePath);

                if (linhas.Length == 0)
                {
                    throw new Exception("O arquivo está vazio.");
                }

                // Primeira linha contém a quantidade de corpos
                if (!int.TryParse(linhas[0].Trim(), out int quantidadeCorpos) || quantidadeCorpos <= 0)
                {
                    throw new Exception("A quantidade de corpos não é válida.");
                }

                // Ler corpos
                for (int i = 1; i <= quantidadeCorpos && i < linhas.Length; i++)
                {
                    string linha = linhas[i];
                    var partes = linha.Split(';');

                    if (partes.Length >= 5)
                    {
                        // Interpretar partes do arquivo
                        string nome = partes[0].Trim();
                        double massa = double.Parse(partes[1].Trim());
                        double densidade = double.Parse(partes[2].Trim());
                        double posX = double.Parse(partes[3].Trim());
                        double posY = double.Parse(partes[4].Trim());

                        // Velocidades (opcionais)
                        double velX = 0.0;
                        double velY = 0.0;

                        if (partes.Length >= 7)
                        {
                            velX = double.Parse(partes[5].Trim());
                            velY = double.Parse(partes[6].Trim());
                        }

                        // Criar corpo
                        Corpo corpo = new Corpo(nome, massa, densidade, posX, posY)
                        {
                            VelX = velX,
                            VelY = velY
                        };

                        universo.AdicionarCorpo(corpo);
                    }
                    else
                    {
                        throw new Exception($"Formato inválido na linha {i + 1}.");
                    }
                }

                // Iniciar simulação
                IniciarSimulacao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        private void fast_Click(object sender, EventArgs e)
        {
            // Aumentar velocidade da simulação
            if (simulacaoTimer.Interval > 10)
            {
                simulacaoTimer.Interval -= 10;
            }
            else if (simulacaoTimer.Interval > 1)
            {
                simulacaoTimer.Interval -= 1;
            }

            velAtual.Text = simulacaoTimer.Interval.ToString();
        }

        private void slow_Click(object sender, EventArgs e)
        {
            // Diminuir velocidade da simulação
            simulacaoTimer.Interval += 10;
            if (simulacaoTimer.Interval > 200)
            {
                simulacaoTimer.Interval = 200; // Limitar para não ficar muito lento
            }

            velAtual.Text = simulacaoTimer.Interval.ToString();
        }

        // Novo método para salvar o estado atual do universo
        private void SalvarUniverso_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivos do Universo (*.uni)|*.uni";
                saveFileDialog.Title = "Salvar universo atual";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Salvar universo em formato de arquivo
                        SalvarArquivoUniverso(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}");
                    }
                }
            }
        }

        private void SalvarArquivoUniverso(string filePath)
        {
            // Cria uma lista de strings para salvar no arquivo
            List<string> linhas = new List<string>();

            // Primeira linha: quantidade de corpos
            linhas.Add(universo.corpos.Count.ToString());

            // Adicionar cada corpo
            foreach (Corpo corpo in universo.corpos)
            {
                // Formato: nome;massa;densidade;posX;posY;velX;velY
                string linha = $"{corpo.Nome};{corpo.Massa};{corpo.Densidade};{corpo.PosX};{corpo.PosY};{corpo.VelX};{corpo.VelY}";
                linhas.Add(linha);
            }

            // Salvar no arquivo
            System.IO.File.WriteAllLines(filePath, linhas);
        }

        // Novo método para adicionar um corpo personalizado
        private void AdicionarCorpo_Click(object sender, EventArgs e)
        {

            Form formNovoCorpo = new Form
            {
                Text = "Adicionar Novo Corpo",
                Size = new Size(300, 250),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Campos para entrada de dados
            Label lblNome = new Label { Text = "Nome:", Location = new Point(10, 20) };
            TextBox txtNome = new TextBox { Text = $"Corpo {universo.corpos.Count + 1}", Location = new Point(100, 20), Width = 150 };

            Label lblMassa = new Label { Text = "Massa:", Location = new Point(10, 50) };
            TextBox txtMassa = new TextBox { Text = "500", Location = new Point(100, 50), Width = 150 };

            Label lblDensidade = new Label { Text = "Densidade:", Location = new Point(10, 80) };
            TextBox txtDensidade = new TextBox { Text = "5000", Location = new Point(100, 80), Width = 150 };

            Label lblPosX = new Label { Text = "Posição X:", Location = new Point(10, 110) };
            TextBox txtPosX = new TextBox { Text = (this.ClientSize.Width / 2).ToString(), Location = new Point(100, 110), Width = 150 };

            Label lblPosY = new Label { Text = "Posição Y:", Location = new Point(10, 140) };
            TextBox txtPosY = new TextBox { Text = (this.ClientSize.Height / 2).ToString(), Location = new Point(100, 140), Width = 150 };

            Button btnAdicionar = new Button { Text = "Adicionar", Location = new Point(100, 180), DialogResult = DialogResult.OK };

            // Adicionar controles ao formulário
            formNovoCorpo.Controls.AddRange(new Control[] {
                lblNome, txtNome, lblMassa, txtMassa, lblDensidade, txtDensidade,
                lblPosX, txtPosX, lblPosY, txtPosY, btnAdicionar
            });

            // Mostrar formulário
            if (formNovoCorpo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Criar novo corpo com os dados informados
                    string nome = txtNome.Text;
                    double massa = double.Parse(txtMassa.Text);
                    double densidade = double.Parse(txtDensidade.Text);
                    double posX = double.Parse(txtPosX.Text);
                    double posY = double.Parse(txtPosY.Text);

                    Corpo novoCorpo = new Corpo(nome, massa, densidade, posX, posY);
                    universo.AdicionarCorpo(novoCorpo);

                    // Atualizar visualização
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao adicionar corpo: {ex.Message}");
                }
            }
        }

        // Método para ajustar a constante gravitacional
        private void AjustarConstanteG_ValueChanged(object sender, EventArgs e)
        {
            // Usando um TrackBar ou NumericUpDown na interface
            // Exemplo com NumericUpDown:
            NumericUpDown numG = (NumericUpDown)sender;
            universo.SetConstanteGravitacional((double)numG.Value);
        }

        // Método para alternar o comportamento das bordas
        private void AlternarBordas_CheckedChanged(object sender, EventArgs e)
        {
            // Exemplo com CheckBox:
            CheckBox chkRebater = (CheckBox)sender;
            rebaterNasBordas = chkRebater.Checked;
        }

        // Método para limpar o universo
        private void LimparUniverso_Click(object sender, EventArgs e)
        {
            // Parar simulação se estiver rodando
            if (simulacaoRodando)
            {
                simulacaoTimer.Stop();
                simulacaoRodando = false;
            }

            // Criar novo universo vazio
            universo = new Universo();
            iteracoes = 0;

            // Limpar informações
            LimparCamposInfo();

            // Atualizar visualização
            corposParaDesenhar.Clear();
            this.Invalidate();
        }

        // Manipulador para eventos de arrastar/soltar um corpo
        private bool arrastando = false;
        private Corpo corpoArrastado = null;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left && corpoSelecionado != null)
            {
                arrastando = true;
                corpoArrastado = corpoSelecionado;

                // Opcional: Pausar a simulação durante o arrasto
                if (simulacaoRodando)
                {
                    simulacaoTimer.Stop();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (arrastando && corpoArrastado != null)
            {
                // Atualizar posição do corpo arrastado
                corpoArrastado.PosX = e.X;
                corpoArrastado.PosY = e.Y;

                // Zerar velocidade ao arrastar 
                // corpoArrastado.VelX = 0;
                // corpoArrastado.VelY = 0;

                // Redesenhar
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (arrastando)
            {
                arrastando = false;

                // Reiniciar simulação se estava rodando antes
                if (simulacaoRodando)
                {
                    simulacaoTimer.Start();
                }
            }
        }

        // Liberar recursos quando o formulário for fechado
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Parar o timer
            if (simulacaoTimer != null)
            {
                simulacaoTimer.Stop();
                simulacaoTimer.Dispose();
            }
        }
    }
}
