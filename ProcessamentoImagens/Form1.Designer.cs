namespace ProcessamentoImagens
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CarregaImagem1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.imagemResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.carregarImagem2 = new System.Windows.Forms.Button();
            this.Negativa = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Array = new System.Windows.Forms.Button();
            this.btAdicao = new System.Windows.Forms.Button();
            this.btSubtracao = new System.Windows.Forms.Button();
            this.btSalvarImagem = new System.Windows.Forms.Button();
            this.numValorX = new System.Windows.Forms.NumericUpDown();
            this.numValorY = new System.Windows.Forms.NumericUpDown();
            this.numLarguraRecorte = new System.Windows.Forms.NumericUpDown();
            this.numAlturaRecorte = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btCortarImagem = new System.Windows.Forms.Button();
            this.btConcatenarImagens = new System.Windows.Forms.Button();
            this.btEscalaCinza = new System.Windows.Forms.Button();
            this.numLimiar = new System.Windows.Forms.NumericUpDown();
            this.btRgbBinario = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btlupOUT = new System.Windows.Forms.Button();
            this.btlupIN = new System.Windows.Forms.Button();
            this.numOperacao = new System.Windows.Forms.NumericUpDown();
            this.rbValorFixo = new System.Windows.Forms.RadioButton();
            this.rbPelaImagem = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLarguraRecorte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlturaRecorte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLimiar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOperacao)).BeginInit();
            this.SuspendLayout();
            // 
            // CarregaImagem1
            // 
            this.CarregaImagem1.Location = new System.Drawing.Point(22, 176);
            this.CarregaImagem1.Name = "CarregaImagem1";
            this.CarregaImagem1.Size = new System.Drawing.Size(107, 23);
            this.CarregaImagem1.TabIndex = 0;
            this.CarregaImagem1.Text = "Carregar Imagem 1";
            this.CarregaImagem1.UseVisualStyleBackColor = true;
            this.CarregaImagem1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(22, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 141);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(317, 29);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 141);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(831, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(155, 141);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // imagemResult
            // 
            this.imagemResult.AutoSize = true;
            this.imagemResult.Location = new System.Drawing.Point(828, 0);
            this.imagemResult.Name = "imagemResult";
            this.imagemResult.Size = new System.Drawing.Size(76, 13);
            this.imagemResult.TabIndex = 4;
            this.imagemResult.Text = "imagem Result";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "imagem 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "imagem 1";
            // 
            // carregarImagem2
            // 
            this.carregarImagem2.Location = new System.Drawing.Point(317, 176);
            this.carregarImagem2.Name = "carregarImagem2";
            this.carregarImagem2.Size = new System.Drawing.Size(107, 23);
            this.carregarImagem2.TabIndex = 7;
            this.carregarImagem2.Text = "Carregar Imagem 2";
            this.carregarImagem2.UseVisualStyleBackColor = true;
            this.carregarImagem2.Click += new System.EventHandler(this.carregarImagem2_Click);
            // 
            // Negativa
            // 
            this.Negativa.Location = new System.Drawing.Point(22, 267);
            this.Negativa.Name = "Negativa";
            this.Negativa.Size = new System.Drawing.Size(120, 23);
            this.Negativa.TabIndex = 8;
            this.Negativa.Text = "Negativa";
            this.Negativa.UseVisualStyleBackColor = true;
            this.Negativa.Click += new System.EventHandler(this.Negativa_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(831, 193);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(155, 232);
            this.textBox1.TabIndex = 9;
            // 
            // Array
            // 
            this.Array.Location = new System.Drawing.Point(22, 296);
            this.Array.Name = "Array";
            this.Array.Size = new System.Drawing.Size(120, 23);
            this.Array.TabIndex = 10;
            this.Array.Text = "Converter Array";
            this.Array.UseVisualStyleBackColor = true;
            this.Array.Click += new System.EventHandler(this.Array_Click);
            // 
            // btAdicao
            // 
            this.btAdicao.Location = new System.Drawing.Point(682, 10);
            this.btAdicao.Name = "btAdicao";
            this.btAdicao.Size = new System.Drawing.Size(75, 23);
            this.btAdicao.TabIndex = 11;
            this.btAdicao.Text = "Soma";
            this.btAdicao.UseVisualStyleBackColor = true;
            this.btAdicao.Click += new System.EventHandler(this.btAdicao_Click);
            // 
            // btSubtracao
            // 
            this.btSubtracao.Location = new System.Drawing.Point(682, 39);
            this.btSubtracao.Name = "btSubtracao";
            this.btSubtracao.Size = new System.Drawing.Size(75, 23);
            this.btSubtracao.TabIndex = 12;
            this.btSubtracao.Text = "Subtração";
            this.btSubtracao.UseVisualStyleBackColor = true;
            this.btSubtracao.Click += new System.EventHandler(this.btSubtracao_Click);
            // 
            // btSalvarImagem
            // 
            this.btSalvarImagem.Location = new System.Drawing.Point(856, 163);
            this.btSalvarImagem.Name = "btSalvarImagem";
            this.btSalvarImagem.Size = new System.Drawing.Size(107, 23);
            this.btSalvarImagem.TabIndex = 13;
            this.btSalvarImagem.Text = "Salvar Imagem";
            this.btSalvarImagem.UseVisualStyleBackColor = true;
            this.btSalvarImagem.Click += new System.EventHandler(this.btSalvarImagem_Click);
            // 
            // numValorX
            // 
            this.numValorX.Location = new System.Drawing.Point(697, 244);
            this.numValorX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numValorX.Name = "numValorX";
            this.numValorX.Size = new System.Drawing.Size(60, 20);
            this.numValorX.TabIndex = 14;
            // 
            // numValorY
            // 
            this.numValorY.Location = new System.Drawing.Point(697, 270);
            this.numValorY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numValorY.Name = "numValorY";
            this.numValorY.Size = new System.Drawing.Size(60, 20);
            this.numValorY.TabIndex = 15;
            // 
            // numLarguraRecorte
            // 
            this.numLarguraRecorte.Location = new System.Drawing.Point(697, 313);
            this.numLarguraRecorte.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLarguraRecorte.Name = "numLarguraRecorte";
            this.numLarguraRecorte.Size = new System.Drawing.Size(60, 20);
            this.numLarguraRecorte.TabIndex = 16;
            // 
            // numAlturaRecorte
            // 
            this.numAlturaRecorte.Location = new System.Drawing.Point(697, 339);
            this.numAlturaRecorte.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAlturaRecorte.Name = "numAlturaRecorte";
            this.numAlturaRecorte.Size = new System.Drawing.Size(60, 20);
            this.numAlturaRecorte.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(635, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Valor de X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(635, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Valor de Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(592, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Largura do Recorte";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(601, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Altura do Recorte";
            // 
            // btCortarImagem
            // 
            this.btCortarImagem.Location = new System.Drawing.Point(650, 382);
            this.btCortarImagem.Name = "btCortarImagem";
            this.btCortarImagem.Size = new System.Drawing.Size(107, 23);
            this.btCortarImagem.TabIndex = 22;
            this.btCortarImagem.Text = "Cortar Imagem";
            this.btCortarImagem.UseVisualStyleBackColor = true;
            this.btCortarImagem.Click += new System.EventHandler(this.btCortarImagem_Click);
            // 
            // btConcatenarImagens
            // 
            this.btConcatenarImagens.Location = new System.Drawing.Point(22, 325);
            this.btConcatenarImagens.Name = "btConcatenarImagens";
            this.btConcatenarImagens.Size = new System.Drawing.Size(120, 23);
            this.btConcatenarImagens.TabIndex = 23;
            this.btConcatenarImagens.Text = "Concatenar Imagens";
            this.btConcatenarImagens.UseVisualStyleBackColor = true;
            this.btConcatenarImagens.Click += new System.EventHandler(this.btConcatenarImagens_Click);
            // 
            // btEscalaCinza
            // 
            this.btEscalaCinza.Location = new System.Drawing.Point(22, 238);
            this.btEscalaCinza.Name = "btEscalaCinza";
            this.btEscalaCinza.Size = new System.Drawing.Size(120, 23);
            this.btEscalaCinza.TabIndex = 24;
            this.btEscalaCinza.Text = "Escala de Cinza";
            this.btEscalaCinza.UseVisualStyleBackColor = true;
            this.btEscalaCinza.Click += new System.EventHandler(this.btEscalaCinza_Click);
            // 
            // numLimiar
            // 
            this.numLimiar.Location = new System.Drawing.Point(148, 354);
            this.numLimiar.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLimiar.Name = "numLimiar";
            this.numLimiar.Size = new System.Drawing.Size(51, 20);
            this.numLimiar.TabIndex = 25;
            // 
            // btRgbBinario
            // 
            this.btRgbBinario.Location = new System.Drawing.Point(22, 354);
            this.btRgbBinario.Name = "btRgbBinario";
            this.btRgbBinario.Size = new System.Drawing.Size(120, 23);
            this.btRgbBinario.TabIndex = 26;
            this.btRgbBinario.Text = "rgb -> binario";
            this.btRgbBinario.UseVisualStyleBackColor = true;
            this.btRgbBinario.Click += new System.EventHandler(this.btRgbBinario_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(205, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Digite o valor da mediana";
            // 
            // btlupOUT
            // 
            this.btlupOUT.Location = new System.Drawing.Point(682, 104);
            this.btlupOUT.Name = "btlupOUT";
            this.btlupOUT.Size = new System.Drawing.Size(75, 23);
            this.btlupOUT.TabIndex = 30;
            this.btlupOUT.Text = "LupOUT";
            this.btlupOUT.UseVisualStyleBackColor = true;
            this.btlupOUT.Click += new System.EventHandler(this.btlupOUT_Click);
            // 
            // btlupIN
            // 
            this.btlupIN.Location = new System.Drawing.Point(682, 133);
            this.btlupIN.Name = "btlupIN";
            this.btlupIN.Size = new System.Drawing.Size(75, 23);
            this.btlupIN.TabIndex = 31;
            this.btlupIN.Text = "LupIN";
            this.btlupIN.UseVisualStyleBackColor = true;
            this.btlupIN.Click += new System.EventHandler(this.btlupIN_Click);
            // 
            // numOperacao
            // 
            this.numOperacao.Location = new System.Drawing.Point(625, 39);
            this.numOperacao.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numOperacao.Name = "numOperacao";
            this.numOperacao.Size = new System.Drawing.Size(51, 20);
            this.numOperacao.TabIndex = 32;
            // 
            // rbValorFixo
            // 
            this.rbValorFixo.AutoSize = true;
            this.rbValorFixo.Location = new System.Drawing.Point(547, 39);
            this.rbValorFixo.Name = "rbValorFixo";
            this.rbValorFixo.Size = new System.Drawing.Size(71, 17);
            this.rbValorFixo.TabIndex = 33;
            this.rbValorFixo.TabStop = true;
            this.rbValorFixo.Text = "Valor Fixo";
            this.rbValorFixo.UseVisualStyleBackColor = true;
            // 
            // rbPelaImagem
            // 
            this.rbPelaImagem.AutoSize = true;
            this.rbPelaImagem.Location = new System.Drawing.Point(548, 16);
            this.rbPelaImagem.Name = "rbPelaImagem";
            this.rbPelaImagem.Size = new System.Drawing.Size(86, 17);
            this.rbPelaImagem.TabIndex = 34;
            this.rbPelaImagem.TabStop = true;
            this.rbPelaImagem.Text = "Pela Imagem";
            this.rbPelaImagem.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 450);
            this.Controls.Add(this.rbPelaImagem);
            this.Controls.Add(this.rbValorFixo);
            this.Controls.Add(this.numOperacao);
            this.Controls.Add(this.btlupIN);
            this.Controls.Add(this.btlupOUT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btRgbBinario);
            this.Controls.Add(this.numLimiar);
            this.Controls.Add(this.btEscalaCinza);
            this.Controls.Add(this.btConcatenarImagens);
            this.Controls.Add(this.btCortarImagem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numAlturaRecorte);
            this.Controls.Add(this.numLarguraRecorte);
            this.Controls.Add(this.numValorY);
            this.Controls.Add(this.numValorX);
            this.Controls.Add(this.btSalvarImagem);
            this.Controls.Add(this.btSubtracao);
            this.Controls.Add(this.btAdicao);
            this.Controls.Add(this.Array);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Negativa);
            this.Controls.Add(this.carregarImagem2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagemResult);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CarregaImagem1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLarguraRecorte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlturaRecorte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLimiar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOperacao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CarregaImagem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label imagemResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button carregarImagem2;
        private System.Windows.Forms.Button Negativa;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Array;
        private System.Windows.Forms.Button btAdicao;
        private System.Windows.Forms.Button btSubtracao;
        private System.Windows.Forms.Button btSalvarImagem;
        private System.Windows.Forms.NumericUpDown numValorX;
        private System.Windows.Forms.NumericUpDown numValorY;
        private System.Windows.Forms.NumericUpDown numLarguraRecorte;
        private System.Windows.Forms.NumericUpDown numAlturaRecorte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btCortarImagem;
        private System.Windows.Forms.Button btConcatenarImagens;
        private System.Windows.Forms.Button btEscalaCinza;
        private System.Windows.Forms.NumericUpDown numLimiar;
        private System.Windows.Forms.Button btRgbBinario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btlupOUT;
        private System.Windows.Forms.Button btlupIN;
        private System.Windows.Forms.NumericUpDown numOperacao;
        private System.Windows.Forms.RadioButton rbValorFixo;
        private System.Windows.Forms.RadioButton rbPelaImagem;
    }
}

