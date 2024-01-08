namespace QLBV.FormThamSo
{
    partial class Frm_Upload
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDuongdan = new DevExpress.XtraEditors.TextEdit();
            this.sbtchon = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTaikhoan = new DevExpress.XtraEditors.TextEdit();
            this.txtMatkhau = new DevExpress.XtraEditors.TextEdit();
            this.sbtUpload = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongdan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaikhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatkhau.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(14, 89);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(214, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Kiểm tra kết nối";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 121);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Đường dẫn:";
            // 
            // txtDuongdan
            // 
            this.txtDuongdan.Location = new System.Drawing.Point(78, 118);
            this.txtDuongdan.Name = "txtDuongdan";
            this.txtDuongdan.Size = new System.Drawing.Size(92, 20);
            this.txtDuongdan.TabIndex = 2;
            // 
            // sbtchon
            // 
            this.sbtchon.Location = new System.Drawing.Point(176, 115);
            this.sbtchon.Name = "sbtchon";
            this.sbtchon.Size = new System.Drawing.Size(52, 23);
            this.sbtchon.TabIndex = 3;
            this.sbtchon.Text = "Browser";
            this.sbtchon.Click += new System.EventHandler(this.sbtchon_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Tài khoản:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // txtTaikhoan
            // 
            this.txtTaikhoan.Location = new System.Drawing.Point(78, 23);
            this.txtTaikhoan.Name = "txtTaikhoan";
            this.txtTaikhoan.Size = new System.Drawing.Size(150, 20);
            this.txtTaikhoan.TabIndex = 0;
            this.txtTaikhoan.EditValueChanged += new System.EventHandler(this.txtTaikhoan_EditValueChanged);
            // 
            // txtMatkhau
            // 
            this.txtMatkhau.Location = new System.Drawing.Point(78, 54);
            this.txtMatkhau.Name = "txtMatkhau";
            this.txtMatkhau.Properties.PasswordChar = '*';
            this.txtMatkhau.Size = new System.Drawing.Size(150, 20);
            this.txtMatkhau.TabIndex = 1;
            this.txtMatkhau.EditValueChanged += new System.EventHandler(this.txtMatkhau_EditValueChanged);
            // 
            // sbtUpload
            // 
            this.sbtUpload.Location = new System.Drawing.Point(14, 161);
            this.sbtUpload.Name = "sbtUpload";
            this.sbtUpload.Size = new System.Drawing.Size(205, 23);
            this.sbtUpload.TabIndex = 4;
            this.sbtUpload.Text = "UpLoad";
            this.sbtUpload.Click += new System.EventHandler(this.sbtUpload_Click);
            // 
            // Frm_Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 205);
            this.Controls.Add(this.sbtUpload);
            this.Controls.Add(this.txtMatkhau);
            this.Controls.Add(this.txtTaikhoan);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.sbtchon);
            this.Controls.Add(this.txtDuongdan);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Upload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Upload";
            this.Load += new System.EventHandler(this.Frm_Upload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongdan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaikhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatkhau.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDuongdan;
        private DevExpress.XtraEditors.SimpleButton sbtchon;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTaikhoan;
        private DevExpress.XtraEditors.TextEdit txtMatkhau;
        private DevExpress.XtraEditors.SimpleButton sbtUpload;
    }
}