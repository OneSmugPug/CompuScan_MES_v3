namespace CompuScan_MES_Main
{
    partial class UserManagement
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
            this.dgv_UserManagement = new System.Windows.Forms.DataGridView();
            this.btn_AddUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UserManagement)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Management";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgv_UserManagement
            // 
            this.dgv_UserManagement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_UserManagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_UserManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_UserManagement.Location = new System.Drawing.Point(12, 77);
            this.dgv_UserManagement.Name = "dgv_UserManagement";
            this.dgv_UserManagement.Size = new System.Drawing.Size(1020, 686);
            this.dgv_UserManagement.TabIndex = 1;
            this.dgv_UserManagement.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_UserManagement_CellDoubleClick);
            // 
            // btn_AddUser
            // 
            this.btn_AddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddUser.Location = new System.Drawing.Point(871, 12);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new System.Drawing.Size(161, 59);
            this.btn_AddUser.TabIndex = 2;
            this.btn_AddUser.Text = "Add User";
            this.btn_AddUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_AddUser.UseVisualStyleBackColor = true;
            this.btn_AddUser.Click += new System.EventHandler(this.Btn_AddUser_Click);
            this.btn_AddUser.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_AddUser_Paint);
            this.btn_AddUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_AddUser_MouseDown);
            this.btn_AddUser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_AddUser_MouseUp);
            // 
            // UserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1044, 788);
            this.Controls.Add(this.btn_AddUser);
            this.Controls.Add(this.dgv_UserManagement);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserManagement";
            this.Text = "UserManagement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UserManagement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_UserManagement;
        private System.Windows.Forms.Button btn_AddUser;
    }
}