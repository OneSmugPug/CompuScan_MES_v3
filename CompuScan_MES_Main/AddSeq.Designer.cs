namespace CompuScan_MES_Main
{
    partial class AddSeq
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
            this.Cbb_Line = new System.Windows.Forms.ComboBox();
            this.Cbb_Station = new System.Windows.Forms.ComboBox();
            this.Cbb_Var = new System.Windows.Forms.ComboBox();
            this.Cbb_Proc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Cbb_Line
            // 
            this.Cbb_Line.FormattingEnabled = true;
            this.Cbb_Line.Location = new System.Drawing.Point(112, 44);
            this.Cbb_Line.Name = "Cbb_Line";
            this.Cbb_Line.Size = new System.Drawing.Size(251, 21);
            this.Cbb_Line.TabIndex = 1;
            // 
            // Cbb_Station
            // 
            this.Cbb_Station.FormattingEnabled = true;
            this.Cbb_Station.Location = new System.Drawing.Point(112, 71);
            this.Cbb_Station.Name = "Cbb_Station";
            this.Cbb_Station.Size = new System.Drawing.Size(167, 21);
            this.Cbb_Station.TabIndex = 2;
            // 
            // Cbb_Var
            // 
            this.Cbb_Var.FormattingEnabled = true;
            this.Cbb_Var.Location = new System.Drawing.Point(112, 98);
            this.Cbb_Var.Name = "Cbb_Var";
            this.Cbb_Var.Size = new System.Drawing.Size(167, 21);
            this.Cbb_Var.TabIndex = 3;
            // 
            // Cbb_Proc
            // 
            this.Cbb_Proc.Location = new System.Drawing.Point(112, 125);
            this.Cbb_Proc.Name = "Cbb_Proc";
            this.Cbb_Proc.Size = new System.Drawing.Size(100, 20);
            this.Cbb_Proc.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add New Sequence";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Line:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Station:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Variant/Model:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(39, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Processes:";
            // 
            // Btn_Next
            // 
            this.Btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Next.Location = new System.Drawing.Point(263, 160);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(100, 32);
            this.Btn_Next.TabIndex = 5;
            this.Btn_Next.Text = "Next";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            this.Btn_Next.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Next_Paint);
            this.Btn_Next.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Next_MouseDown);
            this.Btn_Next.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Next_MouseUp);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Location = new System.Drawing.Point(17, 160);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 32);
            this.Btn_Cancel.TabIndex = 6;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Next_Click);
            this.Btn_Cancel.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Next_Paint);
            this.Btn_Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Next_MouseDown);
            this.Btn_Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Next_MouseUp);
            // 
            // AddSeq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(379, 204);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cbb_Proc);
            this.Controls.Add(this.Cbb_Var);
            this.Controls.Add(this.Cbb_Station);
            this.Controls.Add(this.Cbb_Line);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(395, 243);
            this.MinimumSize = new System.Drawing.Size(395, 243);
            this.Name = "AddSeq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Sequence";
            this.Load += new System.EventHandler(this.AddSeq_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cbb_Line;
        private System.Windows.Forms.ComboBox Cbb_Station;
        private System.Windows.Forms.ComboBox Cbb_Var;
        private System.Windows.Forms.TextBox Cbb_Proc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_Cancel;
    }
}