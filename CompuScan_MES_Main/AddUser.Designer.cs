namespace CompuScan_MES_Main
{
    partial class AddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUser));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_AU_Name = new System.Windows.Forms.TextBox();
            this.txt_AU_Surname = new System.Windows.Forms.TextBox();
            this.txt_AU_AccessLevel = new System.Windows.Forms.TextBox();
            this.btn_RFIDSetup = new System.Windows.Forms.Button();
            this.btn_Done = new System.Windows.Forms.Button();
            this.lbl_AU_ScannedStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Accesslevel:";
            // 
            // txt_AU_Name
            // 
            this.txt_AU_Name.Location = new System.Drawing.Point(121, 24);
            this.txt_AU_Name.Name = "txt_AU_Name";
            this.txt_AU_Name.Size = new System.Drawing.Size(178, 20);
            this.txt_AU_Name.TabIndex = 1;
            // 
            // txt_AU_Surname
            // 
            this.txt_AU_Surname.Location = new System.Drawing.Point(121, 64);
            this.txt_AU_Surname.Name = "txt_AU_Surname";
            this.txt_AU_Surname.Size = new System.Drawing.Size(178, 20);
            this.txt_AU_Surname.TabIndex = 1;
            // 
            // txt_AU_AccessLevel
            // 
            this.txt_AU_AccessLevel.Location = new System.Drawing.Point(121, 104);
            this.txt_AU_AccessLevel.Name = "txt_AU_AccessLevel";
            this.txt_AU_AccessLevel.Size = new System.Drawing.Size(178, 20);
            this.txt_AU_AccessLevel.TabIndex = 1;
            // 
            // btn_RFIDSetup
            // 
            this.btn_RFIDSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RFIDSetup.Location = new System.Drawing.Point(12, 145);
            this.btn_RFIDSetup.Name = "btn_RFIDSetup";
            this.btn_RFIDSetup.Size = new System.Drawing.Size(137, 47);
            this.btn_RFIDSetup.TabIndex = 2;
            this.btn_RFIDSetup.Text = "Add RFID Card";
            this.btn_RFIDSetup.UseVisualStyleBackColor = true;
            this.btn_RFIDSetup.Click += new System.EventHandler(this.Btn_RFIDSetup_Click);
            this.btn_RFIDSetup.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_RFIDSetup_Paint);
            this.btn_RFIDSetup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_RFIDSetup_MouseDown);
            this.btn_RFIDSetup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_RFIDSetup_MouseUp);
            // 
            // btn_Done
            // 
            this.btn_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Done.Location = new System.Drawing.Point(164, 145);
            this.btn_Done.Name = "btn_Done";
            this.btn_Done.Size = new System.Drawing.Size(135, 47);
            this.btn_Done.TabIndex = 2;
            this.btn_Done.Text = "Done";
            this.btn_Done.UseVisualStyleBackColor = true;
            this.btn_Done.Click += new System.EventHandler(this.Btn_Done_Click);
            this.btn_Done.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Done_Paint);
            this.btn_Done.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Done_MouseDown);
            this.btn_Done.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Done_MouseUp);
            // 
            // lbl_AU_ScannedStatus
            // 
            this.lbl_AU_ScannedStatus.AutoSize = true;
            this.lbl_AU_ScannedStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AU_ScannedStatus.Location = new System.Drawing.Point(103, 201);
            this.lbl_AU_ScannedStatus.Name = "lbl_AU_ScannedStatus";
            this.lbl_AU_ScannedStatus.Size = new System.Drawing.Size(107, 15);
            this.lbl_AU_ScannedStatus.TabIndex = 3;
            this.lbl_AU_ScannedStatus.Text = "Card Not Scanned";
            this.lbl_AU_ScannedStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(311, 225);
            this.Controls.Add(this.lbl_AU_ScannedStatus);
            this.Controls.Add(this.btn_Done);
            this.Controls.Add(this.btn_RFIDSetup);
            this.Controls.Add(this.txt_AU_AccessLevel);
            this.Controls.Add(this.txt_AU_Surname);
            this.Controls.Add(this.txt_AU_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add User";
            this.Load += new System.EventHandler(this.AddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_AU_Name;
        private System.Windows.Forms.TextBox txt_AU_Surname;
        private System.Windows.Forms.TextBox txt_AU_AccessLevel;
        private System.Windows.Forms.Button btn_RFIDSetup;
        private System.Windows.Forms.Button btn_Done;
        private System.Windows.Forms.Label lbl_AU_ScannedStatus;
    }
}