namespace CompuScan_MES_Client
{
    partial class Bolt
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.total_bolt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.current_bolt = new System.Windows.Forms.Label();
            this.imageList_Bolt = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txt_station_num = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(358, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bolt";
            // 
            // total_bolt
            // 
            this.total_bolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_bolt.Location = new System.Drawing.Point(425, 357);
            this.total_bolt.Name = "total_bolt";
            this.total_bolt.Size = new System.Drawing.Size(30, 24);
            this.total_bolt.TabIndex = 6;
            this.total_bolt.Text = "##";
            this.total_bolt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(390, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Of";
            // 
            // current_bolt
            // 
            this.current_bolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_bolt.Location = new System.Drawing.Point(354, 357);
            this.current_bolt.Name = "current_bolt";
            this.current_bolt.Size = new System.Drawing.Size(30, 24);
            this.current_bolt.TabIndex = 4;
            this.current_bolt.Text = "##";
            this.current_bolt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList_Bolt
            // 
            this.imageList_Bolt.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_Bolt.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList_Bolt.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(655, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Station Number:";
            // 
            // txt_station_num
            // 
            this.txt_station_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_station_num.AutoSize = true;
            this.txt_station_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_station_num.Location = new System.Drawing.Point(776, 9);
            this.txt_station_num.Name = "txt_station_num";
            this.txt_station_num.Size = new System.Drawing.Size(27, 20);
            this.txt_station_num.TabIndex = 8;
            this.txt_station_num.Text = "##";
            // 
            // Bolt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(812, 598);
            this.Controls.Add(this.txt_station_num);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.total_bolt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.current_bolt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bolt";
            this.Text = "Bolt";
            this.Load += new System.EventHandler(this.Bolt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label total_bolt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label current_bolt;
        private System.Windows.Forms.ImageList imageList_Bolt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txt_station_num;
    }
}