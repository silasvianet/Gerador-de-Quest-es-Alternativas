namespace Gerador_de_Questões_Alternativas
{
    partial class Consulta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Consulta));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestao = new System.Windows.Forms.TextBox();
            this.cboAssunto = new System.Windows.Forms.ComboBox();
            this.cboDisciplina = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFecharList = new System.Windows.Forms.Button();
            this.lstImportacao = new System.Windows.Forms.ListBox();
            this.gdvExcel = new System.Windows.Forms.DataGridView();
            this.pnlManutencao = new System.Windows.Forms.Panel();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRegistro = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.btnImportacao = new System.Windows.Forms.Button();
            this.BtnLimpar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvExcel)).BeginInit();
            this.pnlManutencao.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtQuestao);
            this.groupBox1.Controls.Add(this.cboAssunto);
            this.groupBox1.Controls.Add(this.cboDisciplina);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(390, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Questão";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Assunto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Disciplina";
            // 
            // txtQuestao
            // 
            this.txtQuestao.Location = new System.Drawing.Point(393, 32);
            this.txtQuestao.Name = "txtQuestao";
            this.txtQuestao.Size = new System.Drawing.Size(157, 20);
            this.txtQuestao.TabIndex = 2;
            // 
            // cboAssunto
            // 
            this.cboAssunto.FormattingEnabled = true;
            this.cboAssunto.Location = new System.Drawing.Point(223, 31);
            this.cboAssunto.Name = "cboAssunto";
            this.cboAssunto.Size = new System.Drawing.Size(164, 21);
            this.cboAssunto.TabIndex = 1;
            // 
            // cboDisciplina
            // 
            this.cboDisciplina.FormattingEnabled = true;
            this.cboDisciplina.Location = new System.Drawing.Point(5, 31);
            this.cboDisciplina.Name = "cboDisciplina";
            this.cboDisciplina.Size = new System.Drawing.Size(211, 21);
            this.cboDisciplina.TabIndex = 0;
            this.cboDisciplina.SelectedIndexChanged += new System.EventHandler(this.cboDisciplina_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFecharList);
            this.groupBox2.Controls.Add(this.lstImportacao);
            this.groupBox2.Controls.Add(this.gdvExcel);
            this.groupBox2.Location = new System.Drawing.Point(7, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(859, 399);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnFecharList
            // 
            this.btnFecharList.Location = new System.Drawing.Point(735, 40);
            this.btnFecharList.Name = "btnFecharList";
            this.btnFecharList.Size = new System.Drawing.Size(60, 21);
            this.btnFecharList.TabIndex = 2;
            this.btnFecharList.Text = "Fechar";
            this.btnFecharList.UseVisualStyleBackColor = true;
            this.btnFecharList.Click += new System.EventHandler(this.btnFecharList_Click);
            // 
            // lstImportacao
            // 
            this.lstImportacao.FormattingEnabled = true;
            this.lstImportacao.Location = new System.Drawing.Point(21, 25);
            this.lstImportacao.Name = "lstImportacao";
            this.lstImportacao.Size = new System.Drawing.Size(816, 316);
            this.lstImportacao.TabIndex = 1;
            // 
            // gdvExcel
            // 
            this.gdvExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvExcel.Location = new System.Drawing.Point(9, 19);
            this.gdvExcel.Name = "gdvExcel";
            this.gdvExcel.Size = new System.Drawing.Size(839, 333);
            this.gdvExcel.TabIndex = 0;
            // 
            // pnlManutencao
            // 
            this.pnlManutencao.Controls.Add(this.pBar);
            this.pnlManutencao.Controls.Add(this.label4);
            this.pnlManutencao.Controls.Add(this.lblRegistro);
            this.pnlManutencao.Location = new System.Drawing.Point(4, 78);
            this.pnlManutencao.Name = "pnlManutencao";
            this.pnlManutencao.Size = new System.Drawing.Size(862, 399);
            this.pnlManutencao.TabIndex = 5;
            this.pnlManutencao.UseWaitCursor = true;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(13, 154);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(831, 36);
            this.pBar.Step = 1;
            this.pBar.TabIndex = 5;
            this.pBar.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Registro(s): ";
            this.label4.UseWaitCursor = true;
            // 
            // lblRegistro
            // 
            this.lblRegistro.AutoSize = true;
            this.lblRegistro.Location = new System.Drawing.Point(88, 339);
            this.lblRegistro.Name = "lblRegistro";
            this.lblRegistro.Size = new System.Drawing.Size(13, 13);
            this.lblRegistro.TabIndex = 3;
            this.lblRegistro.Text = "0";
            this.lblRegistro.UseWaitCursor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnConsulta);
            this.groupBox3.Controls.Add(this.btnImportacao);
            this.groupBox3.Controls.Add(this.BtnLimpar);
            this.groupBox3.Location = new System.Drawing.Point(570, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 69);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnConsulta
            // 
            this.btnConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsulta.Image = ((System.Drawing.Image)(resources.GetObject("btnConsulta.Image")));
            this.btnConsulta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConsulta.Location = new System.Drawing.Point(15, 9);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(86, 54);
            this.btnConsulta.TabIndex = 2;
            this.btnConsulta.Text = "&Consulta";
            this.btnConsulta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConsulta.UseVisualStyleBackColor = true;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // btnImportacao
            // 
            this.btnImportacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportacao.Image = ((System.Drawing.Image)(resources.GetObject("btnImportacao.Image")));
            this.btnImportacao.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImportacao.Location = new System.Drawing.Point(207, 9);
            this.btnImportacao.Name = "btnImportacao";
            this.btnImportacao.Size = new System.Drawing.Size(83, 53);
            this.btnImportacao.TabIndex = 1;
            this.btnImportacao.Text = "&Importação";
            this.btnImportacao.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImportacao.UseVisualStyleBackColor = true;
            this.btnImportacao.Click += new System.EventHandler(this.btnImportacao_Click);
            // 
            // BtnLimpar
            // 
            this.BtnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLimpar.Image = ((System.Drawing.Image)(resources.GetObject("BtnLimpar.Image")));
            this.BtnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnLimpar.Location = new System.Drawing.Point(107, 9);
            this.BtnLimpar.Name = "BtnLimpar";
            this.BtnLimpar.Size = new System.Drawing.Size(96, 54);
            this.BtnLimpar.TabIndex = 0;
            this.BtnLimpar.Text = "&Limpar banco";
            this.BtnLimpar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnLimpar.UseVisualStyleBackColor = true;
            this.BtnLimpar.Click += new System.EventHandler(this.BtnLimpar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(872, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 513);
            this.Controls.Add(this.pnlManutencao);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Consulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta";
            this.Load += new System.EventHandler(this.Consulta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdvExcel)).EndInit();
            this.pnlManutencao.ResumeLayout(false);
            this.pnlManutencao.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gdvExcel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Button btnImportacao;
        private System.Windows.Forms.Button BtnLimpar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestao;
        private System.Windows.Forms.ComboBox cboAssunto;
        private System.Windows.Forms.ComboBox cboDisciplina;
        private System.Windows.Forms.Button btnFecharList;
        private System.Windows.Forms.ListBox lstImportacao;
        private System.Windows.Forms.Label lblRegistro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlManutencao;
        private System.Windows.Forms.ProgressBar pBar;
    }
}