namespace RpG_Software
{
    partial class FormGrupos
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
            this.listVGrupos = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.listVPessoas = new System.Windows.Forms.ListView();
            this.nome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFac = new System.Windows.Forms.ComboBox();
            this.cmbFacAux = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblqtdP = new System.Windows.Forms.Label();
            this.btAlocarAuto = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAplicarTroca = new System.Windows.Forms.Button();
            this.cmBGrupos = new System.Windows.Forms.ComboBox();
            this.btExportar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listVGrupos
            // 
            this.listVGrupos.HideSelection = false;
            this.listVGrupos.Location = new System.Drawing.Point(12, 52);
            this.listVGrupos.MultiSelect = false;
            this.listVGrupos.Name = "listVGrupos";
            this.listVGrupos.Size = new System.Drawing.Size(378, 269);
            this.listVGrupos.TabIndex = 0;
            this.listVGrupos.UseCompatibleStateImageBehavior = false;
            this.listVGrupos.View = System.Windows.Forms.View.List;
            this.listVGrupos.SelectedIndexChanged += new System.EventHandler(this.listVGrupos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtrar grupos por sexo";
            // 
            // listVPessoas
            // 
            this.listVPessoas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listVPessoas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nome,
            this.idade});
            this.listVPessoas.HideSelection = false;
            this.listVPessoas.Location = new System.Drawing.Point(483, 52);
            this.listVPessoas.Name = "listVPessoas";
            this.listVPessoas.Size = new System.Drawing.Size(369, 269);
            this.listVPessoas.TabIndex = 3;
            this.listVPessoas.UseCompatibleStateImageBehavior = false;
            this.listVPessoas.View = System.Windows.Forms.View.Details;
            this.listVPessoas.SelectedIndexChanged += new System.EventHandler(this.listVPessoas_SelectedIndexChanged);
            // 
            // nome
            // 
            this.nome.Text = "Nome";
            this.nome.Width = 281;
            // 
            // idade
            // 
            this.idade.Text = "Idade";
            this.idade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.idade.Width = 83;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Feminino",
            "Masculino",
            "Ambos"});
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Facilitador (a)";
            // 
            // cmbFac
            // 
            this.cmbFac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFac.FormattingEnabled = true;
            this.cmbFac.Location = new System.Drawing.Point(484, 25);
            this.cmbFac.Name = "cmbFac";
            this.cmbFac.Size = new System.Drawing.Size(87, 21);
            this.cmbFac.TabIndex = 6;
            this.cmbFac.SelectedIndexChanged += new System.EventHandler(this.cmbFac_SelectedIndexChanged);
            // 
            // cmbFacAux
            // 
            this.cmbFacAux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFacAux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacAux.FormattingEnabled = true;
            this.cmbFacAux.Location = new System.Drawing.Point(594, 25);
            this.cmbFacAux.Name = "cmbFacAux";
            this.cmbFacAux.Size = new System.Drawing.Size(112, 21);
            this.cmbFacAux.TabIndex = 8;
            this.cmbFacAux.SelectedIndexChanged += new System.EventHandler(this.cmbFac_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(591, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Facilitador (a) auxiliar";
            // 
            // lblqtdP
            // 
            this.lblqtdP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblqtdP.AutoSize = true;
            this.lblqtdP.Location = new System.Drawing.Point(712, 33);
            this.lblqtdP.Name = "lblqtdP";
            this.lblqtdP.Size = new System.Drawing.Size(122, 13);
            this.lblqtdP.TabIndex = 4;
            this.lblqtdP.Text = "Quantidade de pessoas:";
            // 
            // btAlocarAuto
            // 
            this.btAlocarAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAlocarAuto.Location = new System.Drawing.Point(648, 327);
            this.btAlocarAuto.Name = "btAlocarAuto";
            this.btAlocarAuto.Size = new System.Drawing.Size(204, 23);
            this.btAlocarAuto.TabIndex = 9;
            this.btAlocarAuto.Text = "Alocar todas pessoas automaticamente";
            this.btAlocarAuto.UseVisualStyleBackColor = true;
            this.btAlocarAuto.Click += new System.EventHandler(this.btAlocarAuto_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btAplicarTroca);
            this.groupBox1.Controls.Add(this.cmBGrupos);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 327);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 66);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alocar pessoa em outro grupo";
            // 
            // btAplicarTroca
            // 
            this.btAplicarTroca.Location = new System.Drawing.Point(132, 29);
            this.btAplicarTroca.Name = "btAplicarTroca";
            this.btAplicarTroca.Size = new System.Drawing.Size(75, 23);
            this.btAplicarTroca.TabIndex = 1;
            this.btAplicarTroca.Text = "Aplicar";
            this.btAplicarTroca.UseVisualStyleBackColor = true;
            this.btAplicarTroca.Click += new System.EventHandler(this.btAplicarTroca_Click);
            // 
            // cmBGrupos
            // 
            this.cmBGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBGrupos.FormattingEnabled = true;
            this.cmBGrupos.Location = new System.Drawing.Point(6, 31);
            this.cmBGrupos.Name = "cmBGrupos";
            this.cmBGrupos.Size = new System.Drawing.Size(108, 21);
            this.cmBGrupos.TabIndex = 0;
            // 
            // btExportar
            // 
            this.btExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btExportar.Location = new System.Drawing.Point(485, 327);
            this.btExportar.Name = "btExportar";
            this.btExportar.Size = new System.Drawing.Size(148, 23);
            this.btExportar.TabIndex = 11;
            this.btExportar.Text = "Exportar todos os grupos";
            this.btExportar.UseVisualStyleBackColor = true;
            this.btExportar.Click += new System.EventHandler(this.btExportar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(12, 3, 1, 3);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 433);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btExportar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btAlocarAuto);
            this.Controls.Add(this.cmbFacAux);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblqtdP);
            this.Controls.Add(this.listVPessoas);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listVGrupos);
            this.Name = "Form2";
            this.Text = "Grupos";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listVGrupos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listVPessoas;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader nome;
        private System.Windows.Forms.ColumnHeader idade;
        private System.Windows.Forms.ComboBox cmbFac;
        private System.Windows.Forms.ComboBox cmbFacAux;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblqtdP;
        private System.Windows.Forms.Button btAlocarAuto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btAplicarTroca;
        private System.Windows.Forms.ComboBox cmBGrupos;
        private System.Windows.Forms.Button btExportar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}