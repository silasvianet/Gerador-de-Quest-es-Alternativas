namespace Gerador_de_Questões_Alternativas
{
    partial class principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(principal));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnAtualiza = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkGeracaoRadomica = new System.Windows.Forms.CheckBox();
            this.chkFraseRelacionada = new System.Windows.Forms.CheckBox();
            this.grBoxTipoQuestao = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numTpQuestaoAnalise4 = new System.Windows.Forms.NumericUpDown();
            this.numTpQuestaoAnalise3 = new System.Windows.Forms.NumericUpDown();
            this.numTpQuestaoIncorreta = new System.Windows.Forms.NumericUpDown();
            this.numTpQuestaoCorreta = new System.Windows.Forms.NumericUpDown();
            this.chkFormatoNovo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numQuestao = new System.Windows.Forms.NumericUpDown();
            this.cboAssunto = new System.Windows.Forms.ComboBox();
            this.cboDisciplina = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnManutencao = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlPrincipal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grBoxTipoQuestao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoAnalise4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoAnalise3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoIncorreta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoCorreta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestao)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(733, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 388);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnNovo);
            this.groupBox4.Controls.Add(this.btnAtualiza);
            this.groupBox4.Location = new System.Drawing.Point(585, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 176);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // btnNovo
            // 
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.Location = new System.Drawing.Point(5, 96);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(104, 77);
            this.btnNovo.TabIndex = 1;
            this.btnNovo.Text = "&Limpar";
            this.btnNovo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnAtualiza
            // 
            this.btnAtualiza.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualiza.Image = ((System.Drawing.Image)(resources.GetObject("btnAtualiza.Image")));
            this.btnAtualiza.Location = new System.Drawing.Point(5, 16);
            this.btnAtualiza.Name = "btnAtualiza";
            this.btnAtualiza.Size = new System.Drawing.Size(104, 74);
            this.btnAtualiza.TabIndex = 0;
            this.btnAtualiza.Text = "&Gerar";
            this.btnAtualiza.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAtualiza.UseVisualStyleBackColor = true;
            this.btnAtualiza.Click += new System.EventHandler(this.btnAtualiza_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlPrincipal);
            this.groupBox3.Controls.Add(this.btnCopy);
            this.groupBox3.Controls.Add(this.txtResultado);
            this.groupBox3.Location = new System.Drawing.Point(8, 198);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(698, 179);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resultado";
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.Controls.Add(this.pBar);
            this.pnlPrincipal.Location = new System.Drawing.Point(3, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(694, 173);
            this.pnlPrincipal.TabIndex = 5;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(5, 60);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(682, 38);
            this.pBar.TabIndex = 0;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(6, 150);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(72, 23);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Copiar";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultado.Location = new System.Drawing.Point(6, 21);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultado.Size = new System.Drawing.Size(686, 123);
            this.txtResultado.TabIndex = 0;
            this.txtResultado.WordWrap = false;
            this.txtResultado.TextChanged += new System.EventHandler(this.txtResultado_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkGeracaoRadomica);
            this.groupBox2.Controls.Add(this.chkFraseRelacionada);
            this.groupBox2.Controls.Add(this.grBoxTipoQuestao);
            this.groupBox2.Controls.Add(this.chkFormatoNovo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numQuestao);
            this.groupBox2.Controls.Add(this.cboAssunto);
            this.groupBox2.Controls.Add(this.cboDisciplina);
            this.groupBox2.Location = new System.Drawing.Point(6, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 174);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Consulta";
            // 
            // chkGeracaoRadomica
            // 
            this.chkGeracaoRadomica.AutoSize = true;
            this.chkGeracaoRadomica.Location = new System.Drawing.Point(303, 123);
            this.chkGeracaoRadomica.Name = "chkGeracaoRadomica";
            this.chkGeracaoRadomica.Size = new System.Drawing.Size(15, 14);
            this.chkGeracaoRadomica.TabIndex = 10;
            this.chkGeracaoRadomica.UseVisualStyleBackColor = true;
            this.chkGeracaoRadomica.CheckedChanged += new System.EventHandler(this.chkGeracaoRadomica_CheckedChanged);
            // 
            // chkFraseRelacionada
            // 
            this.chkFraseRelacionada.AutoSize = true;
            this.chkFraseRelacionada.Location = new System.Drawing.Point(367, 73);
            this.chkFraseRelacionada.Name = "chkFraseRelacionada";
            this.chkFraseRelacionada.Size = new System.Drawing.Size(195, 17);
            this.chkFraseRelacionada.TabIndex = 9;
            this.chkFraseRelacionada.Text = "Retornar frase similar desse assunto";
            this.chkFraseRelacionada.UseVisualStyleBackColor = true;
            this.chkFraseRelacionada.Visible = false;
            // 
            // grBoxTipoQuestao
            // 
            this.grBoxTipoQuestao.Controls.Add(this.label8);
            this.grBoxTipoQuestao.Controls.Add(this.label7);
            this.grBoxTipoQuestao.Controls.Add(this.label6);
            this.grBoxTipoQuestao.Controls.Add(this.label5);
            this.grBoxTipoQuestao.Controls.Add(this.numTpQuestaoAnalise4);
            this.grBoxTipoQuestao.Controls.Add(this.numTpQuestaoAnalise3);
            this.grBoxTipoQuestao.Controls.Add(this.numTpQuestaoIncorreta);
            this.grBoxTipoQuestao.Controls.Add(this.numTpQuestaoCorreta);
            this.grBoxTipoQuestao.Location = new System.Drawing.Point(324, 118);
            this.grBoxTipoQuestao.Name = "grBoxTipoQuestao";
            this.grBoxTipoQuestao.Size = new System.Drawing.Size(238, 56);
            this.grBoxTipoQuestao.TabIndex = 8;
            this.grBoxTipoQuestao.TabStop = false;
            this.grBoxTipoQuestao.Text = "Tipo de questâo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(180, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Analise 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(124, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Analise 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Incorreta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Correta";
            // 
            // numTpQuestaoAnalise4
            // 
            this.numTpQuestaoAnalise4.Location = new System.Drawing.Point(180, 30);
            this.numTpQuestaoAnalise4.Name = "numTpQuestaoAnalise4";
            this.numTpQuestaoAnalise4.Size = new System.Drawing.Size(50, 20);
            this.numTpQuestaoAnalise4.TabIndex = 3;
            this.numTpQuestaoAnalise4.ValueChanged += new System.EventHandler(this.numTpQuestaoAnalise4_ValueChanged);
            this.numTpQuestaoAnalise4.Click += new System.EventHandler(this.numTpQuestaoAnalise4_Click);
            // 
            // numTpQuestaoAnalise3
            // 
            this.numTpQuestaoAnalise3.Location = new System.Drawing.Point(124, 30);
            this.numTpQuestaoAnalise3.Name = "numTpQuestaoAnalise3";
            this.numTpQuestaoAnalise3.Size = new System.Drawing.Size(50, 20);
            this.numTpQuestaoAnalise3.TabIndex = 2;
            this.numTpQuestaoAnalise3.ValueChanged += new System.EventHandler(this.numTpQuestaoAnalise3_ValueChanged);
            this.numTpQuestaoAnalise3.Click += new System.EventHandler(this.numTpQuestaoAnalise3_Click);
            // 
            // numTpQuestaoIncorreta
            // 
            this.numTpQuestaoIncorreta.Location = new System.Drawing.Point(68, 30);
            this.numTpQuestaoIncorreta.Name = "numTpQuestaoIncorreta";
            this.numTpQuestaoIncorreta.Size = new System.Drawing.Size(50, 20);
            this.numTpQuestaoIncorreta.TabIndex = 1;
            this.numTpQuestaoIncorreta.ValueChanged += new System.EventHandler(this.numTpQuestaoIncorreta_ValueChanged);
            this.numTpQuestaoIncorreta.Click += new System.EventHandler(this.numTpQuestaoIncorreta_Click);
            // 
            // numTpQuestaoCorreta
            // 
            this.numTpQuestaoCorreta.Location = new System.Drawing.Point(12, 30);
            this.numTpQuestaoCorreta.Name = "numTpQuestaoCorreta";
            this.numTpQuestaoCorreta.Size = new System.Drawing.Size(50, 20);
            this.numTpQuestaoCorreta.TabIndex = 0;
            this.numTpQuestaoCorreta.ValueChanged += new System.EventHandler(this.numTpQuestaoCorreta_ValueChanged);
            this.numTpQuestaoCorreta.Click += new System.EventHandler(this.numTpQuestaoCorreta_Click);
            // 
            // chkFormatoNovo
            // 
            this.chkFormatoNovo.AutoSize = true;
            this.chkFormatoNovo.Location = new System.Drawing.Point(154, 151);
            this.chkFormatoNovo.Name = "chkFormatoNovo";
            this.chkFormatoNovo.Size = new System.Drawing.Size(93, 17);
            this.chkFormatoNovo.TabIndex = 7;
            this.chkFormatoNovo.Text = "Formato Novo";
            this.chkFormatoNovo.UseVisualStyleBackColor = true;
            this.chkFormatoNovo.CheckedChanged += new System.EventHandler(this.chkFormatoNovo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Quantidade de questões";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Assunto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Disciplina";
            // 
            // numQuestao
            // 
            this.numQuestao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuestao.Location = new System.Drawing.Point(6, 148);
            this.numQuestao.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numQuestao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuestao.Name = "numQuestao";
            this.numQuestao.Size = new System.Drawing.Size(142, 22);
            this.numQuestao.TabIndex = 2;
            this.numQuestao.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboAssunto
            // 
            this.cboAssunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAssunto.FormattingEnabled = true;
            this.cboAssunto.Location = new System.Drawing.Point(6, 93);
            this.cboAssunto.Name = "cboAssunto";
            this.cboAssunto.Size = new System.Drawing.Size(556, 24);
            this.cboAssunto.TabIndex = 1;
            this.cboAssunto.SelectedIndexChanged += new System.EventHandler(this.cboAssunto_SelectedIndexChanged);
            this.cboAssunto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboAssunto_KeyPress);
            // 
            // cboDisciplina
            // 
            this.cboDisciplina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDisciplina.FormattingEnabled = true;
            this.cboDisciplina.Location = new System.Drawing.Point(6, 37);
            this.cboDisciplina.Name = "cboDisciplina";
            this.cboDisciplina.Size = new System.Drawing.Size(556, 24);
            this.cboDisciplina.TabIndex = 0;
            this.cboDisciplina.SelectedIndexChanged += new System.EventHandler(this.cboDisciplina_SelectedIndexChanged);
            this.cboDisciplina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboDisciplina_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(473, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Software desenvolvido pela ProrSoft Software Ltda";
            // 
            // btnManutencao
            // 
            this.btnManutencao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManutencao.Image = ((System.Drawing.Image)(resources.GetObject("btnManutencao.Image")));
            this.btnManutencao.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnManutencao.Location = new System.Drawing.Point(8, 402);
            this.btnManutencao.Name = "btnManutencao";
            this.btnManutencao.Size = new System.Drawing.Size(110, 50);
            this.btnManutencao.TabIndex = 3;
            this.btnManutencao.Text = "&Manutenção";
            this.btnManutencao.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnManutencao.UseVisualStyleBackColor = true;
            this.btnManutencao.Click += new System.EventHandler(this.btnManutencao_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 50);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 480);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnManutencao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.Name = "principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de Questões Alternativas Versão 1.2.6";
            this.Load += new System.EventHandler(this.principal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnlPrincipal.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grBoxTipoQuestao.ResumeLayout(false);
            this.grBoxTipoQuestao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoAnalise4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoAnalise3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoIncorreta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTpQuestaoCorreta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnAtualiza;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numQuestao;
        private System.Windows.Forms.ComboBox cboAssunto;
        private System.Windows.Forms.ComboBox cboDisciplina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnManutencao;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.CheckBox chkFormatoNovo;
        private System.Windows.Forms.GroupBox grBoxTipoQuestao;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTpQuestaoAnalise4;
        private System.Windows.Forms.NumericUpDown numTpQuestaoAnalise3;
        private System.Windows.Forms.NumericUpDown numTpQuestaoIncorreta;
        private System.Windows.Forms.NumericUpDown numTpQuestaoCorreta;
        private System.Windows.Forms.CheckBox chkFraseRelacionada;
        private System.Windows.Forms.CheckBox chkGeracaoRadomica;
    }
}

