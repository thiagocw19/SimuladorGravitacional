namespace SimuladorGravitacional
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            groupBox1 = new GroupBox();
            label11 = new Label();
            label13 = new Label();
            label6 = new Label();
            label1 = new Label();
            label3 = new Label();
            label9 = new Label();
            label2 = new Label();
            fast = new Button();
            slow = new Button();
            velAtual = new TextBox();
            label4 = new Label();
            label12 = new Label();
            Forcay_box = new TextBox();
            Forcax_box = new TextBox();
            label5 = new Label();
            Parar_bt = new Button();
            Iniciar_bt = new Button();
            VelY_Box = new TextBox();
            label10 = new Label();
            VelX_Box = new TextBox();
            label8 = new Label();
            PosY_Box = new TextBox();
            label7 = new Label();
            PosX_Box = new TextBox();
            Iteracoes_Box = new TextBox();
            Corpos_Box = new TextBox();
            labelColisao = new Label();
            labelCorpos2 = new Label();
            labelCG = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(fast);
            groupBox1.Controls.Add(slow);
            groupBox1.Controls.Add(velAtual);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(Forcay_box);
            groupBox1.Controls.Add(Forcax_box);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(Parar_bt);
            groupBox1.Controls.Add(Iniciar_bt);
            groupBox1.Controls.Add(VelY_Box);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(VelX_Box);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(PosY_Box);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(PosX_Box);
            groupBox1.Controls.Add(Iteracoes_Box);
            groupBox1.Controls.Add(Corpos_Box);
            groupBox1.Location = new Point(0, 510);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(808, 135);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 15F);
            label11.Location = new Point(678, 56);
            label11.Name = "label11";
            label11.Size = new Size(23, 28);
            label11.TabIndex = 44;
            label11.Text = "↳";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 15F);
            label13.Location = new Point(130, 48);
            label13.Name = "label13";
            label13.Size = new Size(23, 28);
            label13.TabIndex = 43;
            label13.Text = "↲";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(590, 38);
            label6.Name = "label6";
            label6.Size = new Size(110, 15);
            label6.TabIndex = 41;
            label6.Text = "Velocidade Atual ➝";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 28);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 40;
            label1.Text = "Quantos corpos? ➝";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(314, 92);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 39;
            label3.Text = "Iterações ➝";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(456, 38);
            label9.Name = "label9";
            label9.Size = new Size(58, 15);
            label9.TabIndex = 38;
            label9.Text = "ForçaX ➝";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(350, 63);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 37;
            label2.Text = "VelY ➝";
            // 
            // fast
            // 
            fast.Location = new Point(705, 63);
            fast.Name = "fast";
            fast.Size = new Size(75, 23);
            fast.TabIndex = 36;
            fast.Text = "Acelerar";
            fast.UseVisualStyleBackColor = true;
            fast.Click += fast_Click;
            // 
            // slow
            // 
            slow.Location = new Point(705, 88);
            slow.Name = "slow";
            slow.Size = new Size(75, 23);
            slow.TabIndex = 35;
            slow.Text = "Retroceder";
            slow.UseVisualStyleBackColor = true;
            slow.Click += slow_Click;
            // 
            // velAtual
            // 
            velAtual.Location = new Point(696, 34);
            velAtual.Name = "velAtual";
            velAtual.ReadOnly = true;
            velAtual.Size = new Size(49, 23);
            velAtual.TabIndex = 34;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(676, 9);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 32;
            label4.Text = "REPRODUÇÃO";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(456, 63);
            label12.Name = "label12";
            label12.Size = new Size(58, 15);
            label12.TabIndex = 31;
            label12.Text = "ForçaY ➝";
            // 
            // Forcay_box
            // 
            Forcay_box.Location = new Point(510, 60);
            Forcay_box.Name = "Forcay_box";
            Forcay_box.ReadOnly = true;
            Forcay_box.Size = new Size(49, 23);
            Forcay_box.TabIndex = 30;
            // 
            // Forcax_box
            // 
            Forcax_box.Location = new Point(510, 34);
            Forcax_box.Name = "Forcax_box";
            Forcax_box.ReadOnly = true;
            Forcax_box.Size = new Size(49, 23);
            Forcax_box.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(381, 9);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 26;
            label5.Text = "INFORMAÇÕES";
            // 
            // Parar_bt
            // 
            Parar_bt.Location = new Point(49, 84);
            Parar_bt.Name = "Parar_bt";
            Parar_bt.Size = new Size(75, 23);
            Parar_bt.TabIndex = 23;
            Parar_bt.Text = "Parar";
            Parar_bt.UseVisualStyleBackColor = true;
            Parar_bt.Click += Parar_bt_Click;
            // 
            // Iniciar_bt
            // 
            Iniciar_bt.Location = new Point(49, 55);
            Iniciar_bt.Name = "Iniciar_bt";
            Iniciar_bt.Size = new Size(75, 23);
            Iniciar_bt.TabIndex = 22;
            Iniciar_bt.Text = "Iniciar";
            Iniciar_bt.UseVisualStyleBackColor = true;
            Iniciar_bt.Click += Iniciar_bt_Click;
            // 
            // VelY_Box
            // 
            VelY_Box.Location = new Point(391, 61);
            VelY_Box.Name = "VelY_Box";
            VelY_Box.ReadOnly = true;
            VelY_Box.Size = new Size(49, 23);
            VelY_Box.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(350, 38);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 19;
            label10.Text = "VelX ➝";
            // 
            // VelX_Box
            // 
            VelX_Box.Location = new Point(391, 34);
            VelX_Box.Name = "VelX_Box";
            VelX_Box.ReadOnly = true;
            VelX_Box.Size = new Size(49, 23);
            VelX_Box.TabIndex = 18;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(241, 64);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 15;
            label8.Text = "PosY ➝";
            // 
            // PosY_Box
            // 
            PosY_Box.Location = new Point(286, 61);
            PosY_Box.Name = "PosY_Box";
            PosY_Box.ReadOnly = true;
            PosY_Box.Size = new Size(49, 23);
            PosY_Box.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(241, 38);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 13;
            label7.Text = "PosX ➝";
            // 
            // PosX_Box
            // 
            PosX_Box.Location = new Point(286, 34);
            PosX_Box.Name = "PosX_Box";
            PosX_Box.ReadOnly = true;
            PosX_Box.Size = new Size(49, 23);
            PosX_Box.TabIndex = 12;
            // 
            // Iteracoes_Box
            // 
            Iteracoes_Box.Location = new Point(391, 89);
            Iteracoes_Box.Name = "Iteracoes_Box";
            Iteracoes_Box.ReadOnly = true;
            Iteracoes_Box.Size = new Size(49, 23);
            Iteracoes_Box.TabIndex = 5;
            // 
            // Corpos_Box
            // 
            Corpos_Box.Location = new Point(114, 25);
            Corpos_Box.Name = "Corpos_Box";
            Corpos_Box.Size = new Size(49, 23);
            Corpos_Box.TabIndex = 1;
            // 
            // labelColisao
            // 
            labelColisao.AutoSize = true;
            labelColisao.ForeColor = SystemColors.Desktop;
            labelColisao.Location = new Point(12, 9);
            labelColisao.Name = "labelColisao";
            labelColisao.Size = new Size(44, 15);
            labelColisao.TabIndex = 3;
            labelColisao.Text = "label14";
            // 
            // labelCorpos2
            // 
            labelCorpos2.AutoSize = true;
            labelCorpos2.ForeColor = SystemColors.Desktop;
            labelCorpos2.Location = new Point(12, 34);
            labelCorpos2.Name = "labelCorpos2";
            labelCorpos2.Size = new Size(44, 15);
            labelCorpos2.TabIndex = 4;
            labelCorpos2.Text = "label14";
            // 
            // labelCG
            // 
            labelCG.AutoSize = true;
            labelCG.ForeColor = SystemColors.MenuText;
            labelCG.Location = new Point(12, 60);
            labelCG.Name = "labelCG";
            labelCG.Size = new Size(44, 15);
            labelCG.TabIndex = 5;
            labelCG.Text = "label14";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(808, 645);
            Controls.Add(labelCG);
            Controls.Add(labelCorpos2);
            Controls.Add(labelColisao);
            Controls.Add(groupBox1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SimuladorGravitacional";
            WindowState = FormWindowState.Maximized;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private GroupBox groupBox1;
        private TextBox Corpos_Box;
        private TextBox Iteracoes_Box;
        private Label label8;
        private TextBox PosY_Box;
        private Label label7;
        private TextBox PosX_Box;
        private TextBox VelY_Box;
        private Label label10;
        private TextBox VelX_Box;
        private Button Iniciar_bt;
        private Button Parar_bt;
        private Label label5;
        private TextBox Forcax_box;
        private TextBox Forcay_box;
        private Label label12;
        private Label label4;
        private Button slow;
        private TextBox velAtual;
        private Button fast;
        private Label label2;
        private Label label9;
        private Label label6;
        private Label label1;
        private Label label3;
        private Label label11;
        private Label label13;
        private Label labelCorpos2;
        private Label labelColisao;
        private Label labelCG;
    }
}
