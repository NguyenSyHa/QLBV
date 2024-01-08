namespace QLBV.FormThamSo
{
    partial class frm_BC_ChiTieuChuyenMon_BVDKKimThanh
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbbNam = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cbbKhoa = new System.Windows.Forms.ComboBox();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.chkMauMoi = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMauMoi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Chọn năm BC";
            // 
            // cbbNam
            // 
            this.cbbNam.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbbNam.FormattingEnabled = true;
            this.cbbNam.Location = new System.Drawing.Point(104, 21);
            this.cbbNam.Name = "cbbNam";
            this.cbbNam.Size = new System.Drawing.Size(175, 25);
            this.cbbNam.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(12, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Chọn KP:";
            this.labelControl2.ToolTip = "Chọn khoa phòng";
            // 
            // cbbKhoa
            // 
            this.cbbKhoa.DropDownHeight = 150;
            this.cbbKhoa.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbbKhoa.FormattingEnabled = true;
            this.cbbKhoa.IntegralHeight = false;
            this.cbbKhoa.ItemHeight = 17;
            this.cbbKhoa.Location = new System.Drawing.Point(104, 56);
            this.cbbKhoa.Name = "cbbKhoa";
            this.cbbKhoa.Size = new System.Drawing.Size(175, 25);
            this.cbbKhoa.TabIndex = 2;
            this.cbbKhoa.SelectedIndexChanged += new System.EventHandler(this.cbbKhoa_SelectedIndexChanged);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btnThoat.Location = new System.Drawing.Point(204, 115);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 26);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnOK.Location = new System.Drawing.Point(104, 115);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "In báo cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkMauMoi
            // 
            this.chkMauMoi.Location = new System.Drawing.Point(188, 87);
            this.chkMauMoi.Name = "chkMauMoi";
            this.chkMauMoi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chkMauMoi.Properties.Appearance.Options.UseFont = true;
            this.chkMauMoi.Properties.Caption = "Mẫu mới";
            this.chkMauMoi.Size = new System.Drawing.Size(90, 22);
            this.chkMauMoi.TabIndex = 5;
            // 
            // frm_BC_ChiTieuChuyenMon_BVDKKimThanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 149);
            this.Controls.Add(this.chkMauMoi);
            this.Controls.Add(this.cbbKhoa);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbbNam);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_ChiTieuChuyenMon_BVDKKimThanh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chỉ tiêu chuyên môn";
            this.Load += new System.EventHandler(this.frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkMauMoi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cbbNam;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cbbKhoa;
        private DevExpress.XtraEditors.CheckEdit chkMauMoi;
    }
}