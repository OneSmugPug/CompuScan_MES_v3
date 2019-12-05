namespace CompuScan_MES_Main
{
    partial class SeqManager
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
            this.Btn_ShortC = new System.Windows.Forms.Button();
            this.Btn_Var = new System.Windows.Forms.Button();
            this.Btn_Seq = new System.Windows.Forms.Button();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.DGV_SM = new ADGV.AdvancedDataGridView();
            this.Btn_RemoveSeq = new System.Windows.Forms.Button();
            this.Btn_AddSeq = new System.Windows.Forms.Button();
            this.Btn_Export = new System.Windows.Forms.Button();
            this.Btn_Import = new System.Windows.Forms.Button();
            this.Cbb_Column = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_AddVar = new System.Windows.Forms.Button();
            this.Btn_AddSC = new System.Windows.Forms.Button();
            this.Btn_RemoveVar = new System.Windows.Forms.Button();
            this.Btn_RemoveSC = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Search = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SM)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_ShortC
            // 
            this.Btn_ShortC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ShortC.Location = new System.Drawing.Point(212, 12);
            this.Btn_ShortC.Name = "Btn_ShortC";
            this.Btn_ShortC.Size = new System.Drawing.Size(100, 37);
            this.Btn_ShortC.TabIndex = 0;
            this.Btn_ShortC.Text = "Short Codes";
            this.Btn_ShortC.UseVisualStyleBackColor = true;
            this.Btn_ShortC.Click += new System.EventHandler(this.Btn_ShortC_Click);
            this.Btn_ShortC.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_ShortC_Paint);
            this.Btn_ShortC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ShortC_MouseDown);
            // 
            // Btn_Var
            // 
            this.Btn_Var.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Var.Location = new System.Drawing.Point(112, 12);
            this.Btn_Var.Name = "Btn_Var";
            this.Btn_Var.Size = new System.Drawing.Size(100, 37);
            this.Btn_Var.TabIndex = 0;
            this.Btn_Var.Text = "Variants/Models";
            this.Btn_Var.UseVisualStyleBackColor = true;
            this.Btn_Var.Click += new System.EventHandler(this.Btn_Var_Click);
            this.Btn_Var.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Var_Paint);
            this.Btn_Var.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Var_MouseDown);
            // 
            // Btn_Seq
            // 
            this.Btn_Seq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Seq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Seq.Location = new System.Drawing.Point(12, 12);
            this.Btn_Seq.Name = "Btn_Seq";
            this.Btn_Seq.Size = new System.Drawing.Size(100, 37);
            this.Btn_Seq.TabIndex = 0;
            this.Btn_Seq.Text = "Sequences";
            this.Btn_Seq.UseVisualStyleBackColor = true;
            this.Btn_Seq.Click += new System.EventHandler(this.Btn_Seq_Click);
            this.Btn_Seq.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Seq_Paint);
            this.Btn_Seq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Seq_MouseDown);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(12, 33);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(729, 35);
            this.bunifuSeparator1.TabIndex = 1;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // DGV_SM
            // 
            this.DGV_SM.AllowUserToAddRows = false;
            this.DGV_SM.AllowUserToDeleteRows = false;
            this.DGV_SM.AllowUserToResizeRows = false;
            this.DGV_SM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV_SM.AutoGenerateContextFilters = true;
            this.DGV_SM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_SM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_SM.DateWithTime = false;
            this.DGV_SM.Location = new System.Drawing.Point(12, 103);
            this.DGV_SM.MultiSelect = false;
            this.DGV_SM.Name = "DGV_SM";
            this.DGV_SM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_SM.Size = new System.Drawing.Size(729, 563);
            this.DGV_SM.TabIndex = 2;
            this.DGV_SM.TimeFilter = false;
            // 
            // Btn_RemoveSeq
            // 
            this.Btn_RemoveSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_RemoveSeq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_RemoveSeq.Location = new System.Drawing.Point(12, 680);
            this.Btn_RemoveSeq.Name = "Btn_RemoveSeq";
            this.Btn_RemoveSeq.Size = new System.Drawing.Size(117, 37);
            this.Btn_RemoveSeq.TabIndex = 0;
            this.Btn_RemoveSeq.Text = "Remove Sequence";
            this.Btn_RemoveSeq.UseVisualStyleBackColor = true;
            this.Btn_RemoveSeq.Visible = false;
            this.Btn_RemoveSeq.Click += new System.EventHandler(this.Btn_RemoveSeq_Click);
            this.Btn_RemoveSeq.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_RemoveSeq_Paint);
            this.Btn_RemoveSeq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveSeq_MouseDown);
            this.Btn_RemoveSeq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveSeq_MouseUp);
            // 
            // Btn_AddSeq
            // 
            this.Btn_AddSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_AddSeq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_AddSeq.Location = new System.Drawing.Point(641, 680);
            this.Btn_AddSeq.Name = "Btn_AddSeq";
            this.Btn_AddSeq.Size = new System.Drawing.Size(100, 37);
            this.Btn_AddSeq.TabIndex = 0;
            this.Btn_AddSeq.Text = "Add Sequence";
            this.Btn_AddSeq.UseVisualStyleBackColor = true;
            this.Btn_AddSeq.Visible = false;
            this.Btn_AddSeq.Click += new System.EventHandler(this.Btn_AddSeq_Click);
            this.Btn_AddSeq.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_AddSeq_Paint);
            this.Btn_AddSeq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_AddSeq_MouseDown);
            this.Btn_AddSeq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_AddSeq_MouseUp);
            // 
            // Btn_Export
            // 
            this.Btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Export.Location = new System.Drawing.Point(675, 60);
            this.Btn_Export.Name = "Btn_Export";
            this.Btn_Export.Size = new System.Drawing.Size(66, 37);
            this.Btn_Export.TabIndex = 0;
            this.Btn_Export.Text = "Export";
            this.Btn_Export.UseVisualStyleBackColor = true;
            this.Btn_Export.Visible = false;
            this.Btn_Export.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Export_Paint);
            this.Btn_Export.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Export_MouseDown);
            this.Btn_Export.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Export_MouseUp);
            // 
            // Btn_Import
            // 
            this.Btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Import.Location = new System.Drawing.Point(603, 60);
            this.Btn_Import.Name = "Btn_Import";
            this.Btn_Import.Size = new System.Drawing.Size(66, 37);
            this.Btn_Import.TabIndex = 0;
            this.Btn_Import.Text = "Import";
            this.Btn_Import.UseVisualStyleBackColor = true;
            this.Btn_Import.Visible = false;
            this.Btn_Import.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Import_Paint);
            this.Btn_Import.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Import_MouseDown);
            this.Btn_Import.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Import_MouseUp);
            // 
            // Cbb_Column
            // 
            this.Cbb_Column.FormattingEnabled = true;
            this.Cbb_Column.Location = new System.Drawing.Point(75, 69);
            this.Cbb_Column.Name = "Cbb_Column";
            this.Cbb_Column.Size = new System.Drawing.Size(169, 21);
            this.Cbb_Column.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Column:";
            // 
            // Btn_AddVar
            // 
            this.Btn_AddVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_AddVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_AddVar.Location = new System.Drawing.Point(641, 680);
            this.Btn_AddVar.Name = "Btn_AddVar";
            this.Btn_AddVar.Size = new System.Drawing.Size(100, 37);
            this.Btn_AddVar.TabIndex = 0;
            this.Btn_AddVar.Text = "Add Variant";
            this.Btn_AddVar.UseVisualStyleBackColor = true;
            this.Btn_AddVar.Visible = false;
            this.Btn_AddVar.Click += new System.EventHandler(this.Btn_AddVar_Click);
            this.Btn_AddVar.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_AddVar_Paint);
            this.Btn_AddVar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_AddVar_MouseDown);
            this.Btn_AddVar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_AddVar_MouseUp);
            // 
            // Btn_AddSC
            // 
            this.Btn_AddSC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_AddSC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_AddSC.Location = new System.Drawing.Point(641, 680);
            this.Btn_AddSC.Name = "Btn_AddSC";
            this.Btn_AddSC.Size = new System.Drawing.Size(100, 37);
            this.Btn_AddSC.TabIndex = 0;
            this.Btn_AddSC.Text = "Add Short Code";
            this.Btn_AddSC.UseVisualStyleBackColor = true;
            this.Btn_AddSC.Visible = false;
            this.Btn_AddSC.Click += new System.EventHandler(this.Btn_AddSC_Click);
            this.Btn_AddSC.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_AddSC_Paint);
            this.Btn_AddSC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_AddSC_MouseDown);
            this.Btn_AddSC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_AddSC_MouseUp);
            // 
            // Btn_RemoveVar
            // 
            this.Btn_RemoveVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_RemoveVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_RemoveVar.Location = new System.Drawing.Point(12, 680);
            this.Btn_RemoveVar.Name = "Btn_RemoveVar";
            this.Btn_RemoveVar.Size = new System.Drawing.Size(117, 37);
            this.Btn_RemoveVar.TabIndex = 0;
            this.Btn_RemoveVar.Text = "Remove Variant";
            this.Btn_RemoveVar.UseVisualStyleBackColor = true;
            this.Btn_RemoveVar.Visible = false;
            this.Btn_RemoveVar.Click += new System.EventHandler(this.Btn_RemoveVar_Click);
            this.Btn_RemoveVar.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_RemoveVar_Paint);
            this.Btn_RemoveVar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveVar_MouseDown);
            this.Btn_RemoveVar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveVar_MouseUp);
            // 
            // Btn_RemoveSC
            // 
            this.Btn_RemoveSC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_RemoveSC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_RemoveSC.Location = new System.Drawing.Point(12, 680);
            this.Btn_RemoveSC.Name = "Btn_RemoveSC";
            this.Btn_RemoveSC.Size = new System.Drawing.Size(117, 37);
            this.Btn_RemoveSC.TabIndex = 0;
            this.Btn_RemoveSC.Text = "Remove Short Code";
            this.Btn_RemoveSC.UseVisualStyleBackColor = true;
            this.Btn_RemoveSC.Visible = false;
            this.Btn_RemoveSC.Click += new System.EventHandler(this.Btn_RemoveSC_Click);
            this.Btn_RemoveSC.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_RemoveSC_Paint);
            this.Btn_RemoveSC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveSC_MouseDown);
            this.Btn_RemoveSC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_RemoveSC_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(272, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search:";
            // 
            // Txt_Search
            // 
            this.Txt_Search.Location = new System.Drawing.Point(335, 69);
            this.Txt_Search.Name = "Txt_Search";
            this.Txt_Search.Size = new System.Drawing.Size(212, 20);
            this.Txt_Search.TabIndex = 5;
            this.Txt_Search.TextChanged += new System.EventHandler(this.Txt_Search_TextChanged);
            // 
            // SeqManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(753, 729);
            this.Controls.Add(this.Txt_Search);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cbb_Column);
            this.Controls.Add(this.DGV_SM);
            this.Controls.Add(this.Btn_Import);
            this.Controls.Add(this.Btn_Export);
            this.Controls.Add(this.Btn_ShortC);
            this.Controls.Add(this.Btn_Var);
            this.Controls.Add(this.Btn_Seq);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.Btn_AddSeq);
            this.Controls.Add(this.Btn_RemoveSeq);
            this.Controls.Add(this.Btn_AddSC);
            this.Controls.Add(this.Btn_RemoveSC);
            this.Controls.Add(this.Btn_RemoveVar);
            this.Controls.Add(this.Btn_AddVar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(753, 729);
            this.Name = "SeqManager";
            this.Text = "SequenceManager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VariantManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_ShortC;
        private System.Windows.Forms.Button Btn_Var;
        private System.Windows.Forms.Button Btn_Seq;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private ADGV.AdvancedDataGridView DGV_SM;
        private System.Windows.Forms.Button Btn_RemoveSeq;
        private System.Windows.Forms.Button Btn_AddSeq;
        private System.Windows.Forms.Button Btn_Export;
        private System.Windows.Forms.Button Btn_Import;
        private System.Windows.Forms.ComboBox Cbb_Column;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_AddVar;
        private System.Windows.Forms.Button Btn_AddSC;
        private System.Windows.Forms.Button Btn_RemoveVar;
        private System.Windows.Forms.Button Btn_RemoveSC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_Search;
    }
}