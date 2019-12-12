namespace CompuScan_MES_Main
{
    partial class AddProcess
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
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Done = new System.Windows.Forms.Button();
            this.Lbl_ProcNum = new System.Windows.Forms.Label();
            this.Cbb_Type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cbb_Comp = new System.Windows.Forms.ComboBox();
            this.Nud_Steps = new System.Windows.Forms.NumericUpDown();
            this.Lbl_Steps = new System.Windows.Forms.Label();
            this.Lbl_Retries = new System.Windows.Forms.Label();
            this.Nud_Retries = new System.Windows.Forms.NumericUpDown();
            this.Rtb_Instruct = new System.Windows.Forms.RichTextBox();
            this.Lbl_Instruct = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Lbl_Groups = new System.Windows.Forms.Label();
            this.Txt_Groups = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Steps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Retries)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Location = new System.Drawing.Point(15, 184);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 32);
            this.Btn_Cancel.TabIndex = 8;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Cancel_Paint);
            this.Btn_Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Cancel_MouseDown);
            this.Btn_Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Cancel_MouseUp);
            // 
            // Btn_Done
            // 
            this.Btn_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Done.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Done.Location = new System.Drawing.Point(294, 184);
            this.Btn_Done.Name = "Btn_Done";
            this.Btn_Done.Size = new System.Drawing.Size(100, 32);
            this.Btn_Done.TabIndex = 7;
            this.Btn_Done.Text = "Done";
            this.Btn_Done.UseVisualStyleBackColor = true;
            this.Btn_Done.Click += new System.EventHandler(this.Btn_Done_Click);
            this.Btn_Done.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Done_Paint);
            this.Btn_Done.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Done_MouseDown);
            this.Btn_Done.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Done_MouseUp);
            // 
            // Lbl_ProcNum
            // 
            this.Lbl_ProcNum.AutoSize = true;
            this.Lbl_ProcNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ProcNum.Location = new System.Drawing.Point(154, 9);
            this.Lbl_ProcNum.Name = "Lbl_ProcNum";
            this.Lbl_ProcNum.Size = new System.Drawing.Size(101, 20);
            this.Lbl_ProcNum.TabIndex = 0;
            this.Lbl_ProcNum.Text = "New Process";
            // 
            // Cbb_Type
            // 
            this.Cbb_Type.FormattingEnabled = true;
            this.Cbb_Type.Items.AddRange(new object[] {
            "Pick (3)",
            "Bolt (4)"});
            this.Cbb_Type.Location = new System.Drawing.Point(118, 44);
            this.Cbb_Type.Name = "Cbb_Type";
            this.Cbb_Type.Size = new System.Drawing.Size(185, 21);
            this.Cbb_Type.TabIndex = 1;
            this.Cbb_Type.SelectedIndexChanged += new System.EventHandler(this.Cbb_Type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Type:";
            // 
            // Cbb_Comp
            // 
            this.Cbb_Comp.FormattingEnabled = true;
            this.Cbb_Comp.Location = new System.Drawing.Point(118, 72);
            this.Cbb_Comp.Name = "Cbb_Comp";
            this.Cbb_Comp.Size = new System.Drawing.Size(276, 21);
            this.Cbb_Comp.TabIndex = 2;
            // 
            // Nud_Steps
            // 
            this.Nud_Steps.Location = new System.Drawing.Point(118, 124);
            this.Nud_Steps.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Nud_Steps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Steps.Name = "Nud_Steps";
            this.Nud_Steps.Size = new System.Drawing.Size(63, 20);
            this.Nud_Steps.TabIndex = 4;
            this.Nud_Steps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Steps.ValueChanged += new System.EventHandler(this.Nud_Steps_ValueChanged);
            // 
            // Lbl_Steps
            // 
            this.Lbl_Steps.AutoSize = true;
            this.Lbl_Steps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Steps.Location = new System.Drawing.Point(71, 124);
            this.Lbl_Steps.Name = "Lbl_Steps";
            this.Lbl_Steps.Size = new System.Drawing.Size(41, 15);
            this.Lbl_Steps.TabIndex = 12;
            this.Lbl_Steps.Text = "Steps:";
            // 
            // Lbl_Retries
            // 
            this.Lbl_Retries.AutoSize = true;
            this.Lbl_Retries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Retries.Location = new System.Drawing.Point(63, 150);
            this.Lbl_Retries.Name = "Lbl_Retries";
            this.Lbl_Retries.Size = new System.Drawing.Size(49, 15);
            this.Lbl_Retries.TabIndex = 12;
            this.Lbl_Retries.Text = "Retries:";
            // 
            // Nud_Retries
            // 
            this.Nud_Retries.Location = new System.Drawing.Point(118, 150);
            this.Nud_Retries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Retries.Name = "Nud_Retries";
            this.Nud_Retries.Size = new System.Drawing.Size(63, 20);
            this.Nud_Retries.TabIndex = 5;
            this.Nud_Retries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Rtb_Instruct
            // 
            this.Rtb_Instruct.Location = new System.Drawing.Point(118, 98);
            this.Rtb_Instruct.MaxLength = 100;
            this.Rtb_Instruct.Name = "Rtb_Instruct";
            this.Rtb_Instruct.Size = new System.Drawing.Size(276, 72);
            this.Rtb_Instruct.TabIndex = 6;
            this.Rtb_Instruct.Text = "";
            // 
            // Lbl_Instruct
            // 
            this.Lbl_Instruct.AutoSize = true;
            this.Lbl_Instruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Instruct.Location = new System.Drawing.Point(12, 99);
            this.Lbl_Instruct.Name = "Lbl_Instruct";
            this.Lbl_Instruct.Size = new System.Drawing.Size(100, 15);
            this.Lbl_Instruct.TabIndex = 12;
            this.Lbl_Instruct.Text = "Place Instruction:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(38, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Component:";
            // 
            // Lbl_Groups
            // 
            this.Lbl_Groups.AutoSize = true;
            this.Lbl_Groups.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Groups.Location = new System.Drawing.Point(62, 99);
            this.Lbl_Groups.Name = "Lbl_Groups";
            this.Lbl_Groups.Size = new System.Drawing.Size(50, 15);
            this.Lbl_Groups.TabIndex = 12;
            this.Lbl_Groups.Text = "Groups:";
            // 
            // Txt_Groups
            // 
            this.Txt_Groups.Enabled = false;
            this.Txt_Groups.Location = new System.Drawing.Point(118, 98);
            this.Txt_Groups.Name = "Txt_Groups";
            this.Txt_Groups.Size = new System.Drawing.Size(63, 20);
            this.Txt_Groups.TabIndex = 16;
            this.Txt_Groups.TabStop = false;
            this.Txt_Groups.Text = "1";
            // 
            // AddProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(406, 228);
            this.Controls.Add(this.Txt_Groups);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Nud_Retries);
            this.Controls.Add(this.Lbl_Retries);
            this.Controls.Add(this.Nud_Steps);
            this.Controls.Add(this.Lbl_Steps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cbb_Comp);
            this.Controls.Add(this.Cbb_Type);
            this.Controls.Add(this.Lbl_ProcNum);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Done);
            this.Controls.Add(this.Rtb_Instruct);
            this.Controls.Add(this.Lbl_Groups);
            this.Controls.Add(this.Lbl_Instruct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 243);
            this.Name = "AddProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processes";
            this.Load += new System.EventHandler(this.AddProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Steps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Retries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Done;
        private System.Windows.Forms.Label Lbl_ProcNum;
        private System.Windows.Forms.ComboBox Cbb_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Cbb_Comp;
        private System.Windows.Forms.NumericUpDown Nud_Steps;
        private System.Windows.Forms.Label Lbl_Steps;
        private System.Windows.Forms.Label Lbl_Retries;
        private System.Windows.Forms.NumericUpDown Nud_Retries;
        private System.Windows.Forms.RichTextBox Rtb_Instruct;
        private System.Windows.Forms.Label Lbl_Instruct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Lbl_Groups;
        private System.Windows.Forms.TextBox Txt_Groups;
    }
}