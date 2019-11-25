namespace CompuScan_MES_Client
{
    partial class Pick
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
            this.label1 = new System.Windows.Forms.Label();
            this.current_pick = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.total_pick = new System.Windows.Forms.Label();
            this.txt_station_num = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pickBoxPnl = new System.Windows.Forms.Panel();
            this.lbl_picked = new System.Windows.Forms.Label();
            this.lbl_Req = new System.Windows.Forms.Label();
            this.pickBoxPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pick";
            // 
            // current_pick
            // 
            this.current_pick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_pick.Location = new System.Drawing.Point(223, 355);
            this.current_pick.Name = "current_pick";
            this.current_pick.Size = new System.Drawing.Size(30, 24);
            this.current_pick.TabIndex = 1;
            this.current_pick.Text = "##";
            this.current_pick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(259, 355);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Of";
            // 
            // total_pick
            // 
            this.total_pick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_pick.Location = new System.Drawing.Point(294, 355);
            this.total_pick.Name = "total_pick";
            this.total_pick.Size = new System.Drawing.Size(30, 24);
            this.total_pick.TabIndex = 3;
            this.total_pick.Text = "##";
            this.total_pick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_station_num
            // 
            this.txt_station_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_station_num.AutoSize = true;
            this.txt_station_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_station_num.Location = new System.Drawing.Point(392, 9);
            this.txt_station_num.Name = "txt_station_num";
            this.txt_station_num.Size = new System.Drawing.Size(27, 20);
            this.txt_station_num.TabIndex = 36;
            this.txt_station_num.Text = "##";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(271, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "Station Number:";
            // 
            // pickBoxPnl
            // 
            this.pickBoxPnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pickBoxPnl.Controls.Add(this.lbl_picked);
            this.pickBoxPnl.Controls.Add(this.lbl_Req);
            this.pickBoxPnl.Location = new System.Drawing.Point(527, 12);
            this.pickBoxPnl.Name = "pickBoxPnl";
            this.pickBoxPnl.Size = new System.Drawing.Size(273, 574);
            this.pickBoxPnl.TabIndex = 37;
            // 
            // lbl_picked
            // 
            this.lbl_picked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_picked.AutoSize = true;
            this.lbl_picked.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_picked.Location = new System.Drawing.Point(182, 38);
            this.lbl_picked.Name = "lbl_picked";
            this.lbl_picked.Size = new System.Drawing.Size(67, 24);
            this.lbl_picked.TabIndex = 64;
            this.lbl_picked.Text = "Picked";
            // 
            // lbl_Req
            // 
            this.lbl_Req.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Req.AutoSize = true;
            this.lbl_Req.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Req.Location = new System.Drawing.Point(13, 38);
            this.lbl_Req.Name = "lbl_Req";
            this.lbl_Req.Size = new System.Drawing.Size(88, 24);
            this.lbl_Req.TabIndex = 63;
            this.lbl_Req.Text = "Required";
            // 
            // Pick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(812, 598);
            this.Controls.Add(this.pickBoxPnl);
            this.Controls.Add(this.txt_station_num);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.total_pick);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.current_pick);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pick";
            this.Text = "Pick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pick_FormClosing);
            this.Load += new System.EventHandler(this.Pick_Load);
            this.pickBoxPnl.ResumeLayout(false);
            this.pickBoxPnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label current_pick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label total_pick;
        private System.Windows.Forms.Label txt_station_num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pickBoxPnl;
        private System.Windows.Forms.Label lbl_picked;
        private System.Windows.Forms.Label lbl_Req;
    }
}