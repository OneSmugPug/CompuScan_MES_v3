namespace CompuScan_MES_Main
{
    partial class EditDelUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDelUser));
            this.txt_EDU_AccessLevel = new System.Windows.Forms.TextBox();
            this.txt_EDU_Surname = new System.Windows.Forms.TextBox();
            this.txt_EDU_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_EDU_Cancel = new System.Windows.Forms.Button();
            this.btn_EDU_Delete = new System.Windows.Forms.Button();
            this.btn_EDU_UpdateRFID = new System.Windows.Forms.Button();
            this.btn_EDU_Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_EDU_AccessLevel
            // 
            this.txt_EDU_AccessLevel.Location = new System.Drawing.Point(120, 101);
            this.txt_EDU_AccessLevel.Name = "txt_EDU_AccessLevel";
            this.txt_EDU_AccessLevel.Size = new System.Drawing.Size(178, 20);
            this.txt_EDU_AccessLevel.TabIndex = 5;
            // 
            // txt_EDU_Surname
            // 
            this.txt_EDU_Surname.Location = new System.Drawing.Point(120, 61);
            this.txt_EDU_Surname.Name = "txt_EDU_Surname";
            this.txt_EDU_Surname.Size = new System.Drawing.Size(178, 20);
            this.txt_EDU_Surname.TabIndex = 6;
            // 
            // txt_EDU_Name
            // 
            this.txt_EDU_Name.Location = new System.Drawing.Point(120, 21);
            this.txt_EDU_Name.Name = "txt_EDU_Name";
            this.txt_EDU_Name.Size = new System.Drawing.Size(178, 20);
            this.txt_EDU_Name.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Accesslevel:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Surname:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // btn_EDU_Cancel
            // 
            this.btn_EDU_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EDU_Cancel.Location = new System.Drawing.Point(164, 197);
            this.btn_EDU_Cancel.Name = "btn_EDU_Cancel";
            this.btn_EDU_Cancel.Size = new System.Drawing.Size(135, 47);
            this.btn_EDU_Cancel.TabIndex = 8;
            this.btn_EDU_Cancel.Text = "Cancel";
            this.btn_EDU_Cancel.UseVisualStyleBackColor = true;
            this.btn_EDU_Cancel.Click += new System.EventHandler(this.Btn_EDU_Cancel_Click);
            this.btn_EDU_Cancel.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_EDU_Cancel_Paint);
            this.btn_EDU_Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Cancel_MouseDown);
            this.btn_EDU_Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Cancel_MouseUp);
            // 
            // btn_EDU_Delete
            // 
            this.btn_EDU_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EDU_Delete.Location = new System.Drawing.Point(12, 197);
            this.btn_EDU_Delete.Name = "btn_EDU_Delete";
            this.btn_EDU_Delete.Size = new System.Drawing.Size(137, 47);
            this.btn_EDU_Delete.TabIndex = 9;
            this.btn_EDU_Delete.Text = "Delete";
            this.btn_EDU_Delete.UseVisualStyleBackColor = true;
            this.btn_EDU_Delete.Click += new System.EventHandler(this.Btn_EDU_Delete_Click);
            this.btn_EDU_Delete.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_EDU_Delete_Paint);
            this.btn_EDU_Delete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Delete_MouseDown);
            this.btn_EDU_Delete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Delete_MouseUp);
            // 
            // btn_EDU_UpdateRFID
            // 
            this.btn_EDU_UpdateRFID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EDU_UpdateRFID.Location = new System.Drawing.Point(12, 144);
            this.btn_EDU_UpdateRFID.Name = "btn_EDU_UpdateRFID";
            this.btn_EDU_UpdateRFID.Size = new System.Drawing.Size(137, 47);
            this.btn_EDU_UpdateRFID.TabIndex = 9;
            this.btn_EDU_UpdateRFID.Text = "Update RFID";
            this.btn_EDU_UpdateRFID.UseVisualStyleBackColor = true;
            this.btn_EDU_UpdateRFID.Click += new System.EventHandler(this.Btn_EDU_UpdateRFID_Click);
            this.btn_EDU_UpdateRFID.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_EDU_UpdateRFID_Paint);
            this.btn_EDU_UpdateRFID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_UpdateRFID_MouseDown);
            this.btn_EDU_UpdateRFID.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_UpdateRFID_MouseUp);
            // 
            // btn_EDU_Done
            // 
            this.btn_EDU_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EDU_Done.Location = new System.Drawing.Point(164, 144);
            this.btn_EDU_Done.Name = "btn_EDU_Done";
            this.btn_EDU_Done.Size = new System.Drawing.Size(135, 47);
            this.btn_EDU_Done.TabIndex = 8;
            this.btn_EDU_Done.Text = "Done";
            this.btn_EDU_Done.UseVisualStyleBackColor = true;
            this.btn_EDU_Done.Click += new System.EventHandler(this.Btn_EDU_Done_Click);
            this.btn_EDU_Done.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_EDU_Done_Paint);
            this.btn_EDU_Done.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Done_MouseDown);
            this.btn_EDU_Done.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_EDU_Done_MouseUp);
            // 
            // EditDelUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(311, 260);
            this.Controls.Add(this.btn_EDU_Done);
            this.Controls.Add(this.btn_EDU_Cancel);
            this.Controls.Add(this.btn_EDU_UpdateRFID);
            this.Controls.Add(this.btn_EDU_Delete);
            this.Controls.Add(this.txt_EDU_AccessLevel);
            this.Controls.Add(this.txt_EDU_Surname);
            this.Controls.Add(this.txt_EDU_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditDelUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update User";
            this.Load += new System.EventHandler(this.EditDelUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_EDU_AccessLevel;
        private System.Windows.Forms.TextBox txt_EDU_Surname;
        private System.Windows.Forms.TextBox txt_EDU_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_EDU_Cancel;
        private System.Windows.Forms.Button btn_EDU_Delete;
        private System.Windows.Forms.Button btn_EDU_UpdateRFID;
        private System.Windows.Forms.Button btn_EDU_Done;
    }
}