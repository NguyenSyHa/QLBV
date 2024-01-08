namespace QLBV.FormNhap
{
    partial class frm_NhapTTHD
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
            this.txtTenDonVi = new DevExpress.XtraEditors.TextEdit();
            this.txtMaSoThue = new DevExpress.XtraEditors.TextEdit();
            this.txtSoTaiKhoan = new DevExpress.XtraEditors.TextEdit();
            this.txtNganHang = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoThue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTaiKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNganHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenDonVi
            // 
            this.txtTenDonVi.Location = new System.Drawing.Point(113, 12);
            this.txtTenDonVi.Name = "txtTenDonVi";
            this.txtTenDonVi.Size = new System.Drawing.Size(273, 20);
            this.txtTenDonVi.TabIndex = 0;
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(113, 69);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(273, 20);
            this.txtMaSoThue.TabIndex = 1;
            // 
            // txtSoTaiKhoan
            // 
            this.txtSoTaiKhoan.Location = new System.Drawing.Point(113, 100);
            this.txtSoTaiKhoan.Name = "txtSoTaiKhoan";
            this.txtSoTaiKhoan.Size = new System.Drawing.Size(273, 20);
            this.txtSoTaiKhoan.TabIndex = 2;
            // 
            // txtNganHang
            // 
            this.txtNganHang.Location = new System.Drawing.Point(113, 127);
            this.txtNganHang.Name = "txtNganHang";
            this.txtNganHang.Size = new System.Drawing.Size(273, 20);
            this.txtNganHang.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 153);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Đồng ý";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Tên đơn vị";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Mã số thuế:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Số tài khoản:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 130);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Ngân hàng:";
            // 
            // txtDC
            // 
            this.txtDC.Location = new System.Drawing.Point(113, 38);
            this.txtDC.Name = "txtDC";
            this.txtDC.Size = new System.Drawing.Size(273, 20);
            this.txtDC.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(13, 45);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(32, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Địa chỉ";
            // 
            // frm_NhapTTHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 188);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNganHang);
            this.Controls.Add(this.txtSoTaiKhoan);
            this.Controls.Add(this.txtMaSoThue);
            this.Controls.Add(this.txtDC);
            this.Controls.Add(this.txtTenDonVi);
            this.Name = "frm_NhapTTHD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập thông tin hóa đơn";
            this.Load += new System.EventHandler(this.frm_NhapTTHD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoThue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTaiKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNganHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtTenDonVi;
        private DevExpress.XtraEditors.TextEdit txtMaSoThue;
        private DevExpress.XtraEditors.TextEdit txtSoTaiKhoan;
        private DevExpress.XtraEditors.TextEdit txtNganHang;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDC;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}