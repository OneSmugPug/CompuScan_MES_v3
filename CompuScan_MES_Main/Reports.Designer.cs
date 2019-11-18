namespace CompuScan_MES_Main
{
    partial class Reports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reports));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ReportSubject = new System.Windows.Forms.ComboBox();
            this.add_Filter = new System.Windows.Forms.Button();
            this.remove_Filter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Export = new System.Windows.Forms.Button();
            this.separator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report On:";
            // 
            // cb_ReportSubject
            // 
            this.cb_ReportSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ReportSubject.FormattingEnabled = true;
            this.cb_ReportSubject.Location = new System.Drawing.Point(105, 15);
            this.cb_ReportSubject.Name = "cb_ReportSubject";
            this.cb_ReportSubject.Size = new System.Drawing.Size(242, 28);
            this.cb_ReportSubject.TabIndex = 1;
            this.cb_ReportSubject.SelectedValueChanged += new System.EventHandler(this.Cb_ReportSubject_SelectedValueChanged);
            // 
            // add_Filter
            // 
            this.add_Filter.Enabled = false;
            this.add_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_Filter.Location = new System.Drawing.Point(191, 49);
            this.add_Filter.Name = "add_Filter";
            this.add_Filter.Size = new System.Drawing.Size(30, 30);
            this.add_Filter.TabIndex = 2;
            this.add_Filter.Text = "+";
            this.add_Filter.UseVisualStyleBackColor = true;
            this.add_Filter.Click += new System.EventHandler(this.Add_Filter_Click);
            this.add_Filter.Paint += new System.Windows.Forms.PaintEventHandler(this.Add_Filter_Paint);
            this.add_Filter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Add_Filter_MouseDown);
            this.add_Filter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Add_Filter_MouseUp);
            // 
            // remove_Filter
            // 
            this.remove_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remove_Filter.Location = new System.Drawing.Point(227, 49);
            this.remove_Filter.Name = "remove_Filter";
            this.remove_Filter.Size = new System.Drawing.Size(30, 30);
            this.remove_Filter.TabIndex = 2;
            this.remove_Filter.Text = "-";
            this.remove_Filter.UseVisualStyleBackColor = true;
            this.remove_Filter.Click += new System.EventHandler(this.Remove_Filter_Click);
            this.remove_Filter.Paint += new System.Windows.Forms.PaintEventHandler(this.Remove_Filter_Paint);
            this.remove_Filter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Remove_Filter_MouseDown);
            this.remove_Filter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Remove_Filter_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add Filter:";
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(263, 49);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(74, 30);
            this.btn_Export.TabIndex = 2;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.Btn_Export_Click);
            this.btn_Export.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Export_Paint);
            this.btn_Export.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Export_MouseDown);
            this.btn_Export.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Export_MouseUp);
            // 
            // separator1
            // 
            this.separator1.BackColor = System.Drawing.Color.Transparent;
            this.separator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.separator1.LineThickness = 1;
            this.separator1.Location = new System.Drawing.Point(0, 85);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(382, 5);
            this.separator1.TabIndex = 4;
            this.separator1.Transparency = 255;
            this.separator1.Vertical = false;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(381, 94);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.remove_Filter);
            this.Controls.Add(this.add_Filter);
            this.Controls.Add(this.cb_ReportSubject);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reports";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_ReportSubject;
        private System.Windows.Forms.Button add_Filter;
        private System.Windows.Forms.Button remove_Filter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Export;
        private Bunifu.Framework.UI.BunifuSeparator separator1;
    }
}