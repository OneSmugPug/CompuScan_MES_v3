namespace CompuScan_MES_Main
{
    partial class VariantManager
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
            this.dgv_VM = new ADGV.AdvancedDataGridView();
            this.btn_SCExp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SCImp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_VarImp = new System.Windows.Forms.Button();
            this.btn_VarExp = new System.Windows.Forms.Button();
            this.btn_AddVar = new System.Windows.Forms.Button();
            this.btn_RemVar = new System.Windows.Forms.Button();
            this.bunifuDropdown1 = new Bunifu.Framework.UI.BunifuDropdown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VM)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_VM
            // 
            this.dgv_VM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_VM.AutoGenerateContextFilters = true;
            this.dgv_VM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_VM.DateWithTime = false;
            this.dgv_VM.Location = new System.Drawing.Point(0, 82);
            this.dgv_VM.Name = "dgv_VM";
            this.dgv_VM.Size = new System.Drawing.Size(656, 570);
            this.dgv_VM.TabIndex = 0;
            this.dgv_VM.TimeFilter = false;
            // 
            // btn_SCExp
            // 
            this.btn_SCExp.AutoSize = true;
            this.btn_SCExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SCExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SCExp.Location = new System.Drawing.Point(85, 19);
            this.btn_SCExp.Name = "btn_SCExp";
            this.btn_SCExp.Size = new System.Drawing.Size(69, 32);
            this.btn_SCExp.TabIndex = 3;
            this.btn_SCExp.Text = "Export";
            this.btn_SCExp.UseVisualStyleBackColor = true;
            this.btn_SCExp.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_SCExp_Paint);
            this.btn_SCExp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_SCExp_MouseDown);
            this.btn_SCExp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_SCExp_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.btn_SCImp);
            this.groupBox1.Controls.Add(this.btn_SCExp);
            this.groupBox1.Location = new System.Drawing.Point(479, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Short Codes";
            // 
            // btn_SCImp
            // 
            this.btn_SCImp.AutoSize = true;
            this.btn_SCImp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SCImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SCImp.Location = new System.Drawing.Point(10, 19);
            this.btn_SCImp.Name = "btn_SCImp";
            this.btn_SCImp.Size = new System.Drawing.Size(69, 32);
            this.btn_SCImp.TabIndex = 4;
            this.btn_SCImp.Text = "Import";
            this.btn_SCImp.UseVisualStyleBackColor = true;
            this.btn_SCImp.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_SCImp_Paint);
            this.btn_SCImp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_SCImp_MouseDown);
            this.btn_SCImp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_SCImp_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.btn_VarImp);
            this.groupBox2.Controls.Add(this.btn_VarExp);
            this.groupBox2.Location = new System.Drawing.Point(309, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 64);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Variants";
            // 
            // btn_VarImp
            // 
            this.btn_VarImp.AutoSize = true;
            this.btn_VarImp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_VarImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_VarImp.Location = new System.Drawing.Point(10, 19);
            this.btn_VarImp.Name = "btn_VarImp";
            this.btn_VarImp.Size = new System.Drawing.Size(69, 32);
            this.btn_VarImp.TabIndex = 4;
            this.btn_VarImp.Text = "Import";
            this.btn_VarImp.UseVisualStyleBackColor = true;
            this.btn_VarImp.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_VarImp_Paint);
            this.btn_VarImp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_VarImp_MouseDown);
            this.btn_VarImp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_VarImp_MouseUp);
            // 
            // btn_VarExp
            // 
            this.btn_VarExp.AutoSize = true;
            this.btn_VarExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_VarExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_VarExp.Location = new System.Drawing.Point(85, 19);
            this.btn_VarExp.Name = "btn_VarExp";
            this.btn_VarExp.Size = new System.Drawing.Size(69, 32);
            this.btn_VarExp.TabIndex = 3;
            this.btn_VarExp.Text = "Export";
            this.btn_VarExp.UseVisualStyleBackColor = true;
            this.btn_VarExp.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_VarExp_Paint);
            this.btn_VarExp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_VarExp_MouseDown);
            this.btn_VarExp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_VarExp_MouseUp);
            // 
            // btn_AddVar
            // 
            this.btn_AddVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddVar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddVar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddVar.Location = new System.Drawing.Point(482, 658);
            this.btn_AddVar.Name = "btn_AddVar";
            this.btn_AddVar.Size = new System.Drawing.Size(161, 59);
            this.btn_AddVar.TabIndex = 7;
            this.btn_AddVar.Text = "Add Variant";
            this.btn_AddVar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_AddVar.UseVisualStyleBackColor = true;
            this.btn_AddVar.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_AddVar_Paint);
            this.btn_AddVar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_AddVar_MouseDown);
            this.btn_AddVar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_AddVar_MouseUp);
            // 
            // btn_RemVar
            // 
            this.btn_RemVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_RemVar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemVar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RemVar.Location = new System.Drawing.Point(12, 658);
            this.btn_RemVar.Name = "btn_RemVar";
            this.btn_RemVar.Size = new System.Drawing.Size(161, 59);
            this.btn_RemVar.TabIndex = 8;
            this.btn_RemVar.Text = "Remove Variant";
            this.btn_RemVar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_RemVar.UseVisualStyleBackColor = true;
            this.btn_RemVar.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_RemVar_Paint);
            this.btn_RemVar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_RemVar_MouseDown);
            this.btn_RemVar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_RemVar_MouseUp);
            // 
            // bunifuDropdown1
            // 
            this.bunifuDropdown1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuDropdown1.BorderRadius = 3;
            this.bunifuDropdown1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuDropdown1.ForeColor = System.Drawing.Color.Black;
            this.bunifuDropdown1.Items = new string[0];
            this.bunifuDropdown1.Location = new System.Drawing.Point(6, 19);
            this.bunifuDropdown1.Name = "bunifuDropdown1";
            this.bunifuDropdown1.NomalColor = System.Drawing.Color.LightGray;
            this.bunifuDropdown1.onHoverColor = System.Drawing.Color.Gainsboro;
            this.bunifuDropdown1.selectedIndex = -1;
            this.bunifuDropdown1.Size = new System.Drawing.Size(245, 35);
            this.bunifuDropdown1.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightGray;
            this.groupBox3.Controls.Add(this.bunifuDropdown1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 64);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search";
            // 
            // VariantManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(655, 729);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_RemVar);
            this.Controls.Add(this.btn_AddVar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_VM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VariantManager";
            this.Text = "VariantManager";
            this.Load += new System.EventHandler(this.VariantManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView dgv_VM;
        private System.Windows.Forms.Button btn_SCExp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_SCImp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_VarImp;
        private System.Windows.Forms.Button btn_VarExp;
        private System.Windows.Forms.Button btn_AddVar;
        private System.Windows.Forms.Button btn_RemVar;
        private Bunifu.Framework.UI.BunifuDropdown bunifuDropdown1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}