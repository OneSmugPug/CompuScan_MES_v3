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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VM)).BeginInit();
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
            this.dgv_VM.Location = new System.Drawing.Point(0, 151);
            this.dgv_VM.Name = "dgv_VM";
            this.dgv_VM.Size = new System.Drawing.Size(656, 578);
            this.dgv_VM.TabIndex = 0;
            this.dgv_VM.TimeFilter = false;
            // 
            // VariantManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(655, 729);
            this.Controls.Add(this.dgv_VM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VariantManager";
            this.Text = "VariantManager";
            this.Load += new System.EventHandler(this.VariantManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView dgv_VM;
    }
}