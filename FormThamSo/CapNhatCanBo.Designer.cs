namespace QLBV.FormThamSo
{
    partial class CapNhatCanBo
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
            this.btnupdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnbackup = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnupdate
            // 
            this.btnupdate.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate.Appearance.Options.UseFont = true;
            this.btnupdate.Location = new System.Drawing.Point(44, 48);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(143, 34);
            this.btnupdate.TabIndex = 0;
            this.btnupdate.Text = "UPDATE";
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btnbackup
            // 
            this.btnbackup.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbackup.Appearance.Options.UseFont = true;
            this.btnbackup.Location = new System.Drawing.Point(44, 12);
            this.btnbackup.Name = "btnbackup";
            this.btnbackup.Size = new System.Drawing.Size(143, 30);
            this.btnbackup.TabIndex = 1;
            this.btnbackup.Text = "BACKUP CÁN BỘ";
            this.btnbackup.Click += new System.EventHandler(this.btnbackup_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(44, 89);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(143, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // CapNhatCanBo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 159);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnbackup);
            this.Controls.Add(this.btnupdate);
            this.Name = "CapNhatCanBo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Cán Bộ";
            this.Load += new System.EventHandler(this.CapNhatCanBo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnupdate;
        private DevExpress.XtraEditors.SimpleButton btnbackup;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}