namespace QLBV.FormThamSo
{
    partial class frm_BCNhomVienPhiTheoKhoa_30003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCNhomVienPhiTheoKhoa_30003));
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.rdNgay = new DevExpress.XtraEditors.RadioGroup();
            this.rgCHon = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.rdBaoHiem = new DevExpress.XtraEditors.RadioGroup();
            this.cklKhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgCHon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBaoHiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // cbNam
            // 
            this.cbNam.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(93, 40);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(117, 23);
            this.cbNam.TabIndex = 122;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 119;
            this.label2.Text = "Năm BC:";
            // 
            // cboThang
            // 
            this.cboThang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(93, 11);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(117, 23);
            this.cboThang.TabIndex = 124;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 123;
            this.label1.Text = "Tháng BC:";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(93, 185);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chi phí thu dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chi phí thuốc, VTYT")});
            this.radioGroup1.Size = new System.Drawing.Size(286, 27);
            this.radioGroup1.TabIndex = 125;
            // 
            // rdNgay
            // 
            this.rdNgay.Location = new System.Drawing.Point(93, 149);
            this.rdNgay.Name = "rdNgay";
            this.rdNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNgay.Properties.Appearance.Options.UseFont = true;
            this.rdNgay.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngày thu"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngày Duyệt")});
            this.rdNgay.Size = new System.Drawing.Size(286, 30);
            this.rdNgay.TabIndex = 126;
            this.rdNgay.SelectedIndexChanged += new System.EventHandler(this.rdNgay_SelectedIndexChanged);
            // 
            // rgCHon
            // 
            this.rgCHon.Location = new System.Drawing.Point(93, 97);
            this.rgCHon.Name = "rgCHon";
            this.rgCHon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgCHon.Properties.Appearance.Options.UseFont = true;
            this.rgCHon.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN cùng chi trả"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Khác"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rgCHon.Size = new System.Drawing.Size(286, 46);
            this.rgCHon.TabIndex = 130;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(19, 73);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(63, 17);
            this.labelControl4.TabIndex = 127;
            this.labelControl4.Text = "Đ.Tượng:";
            // 
            // rdBaoHiem
            // 
            this.rdBaoHiem.Location = new System.Drawing.Point(93, 69);
            this.rdBaoHiem.Name = "rdBaoHiem";
            this.rdBaoHiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBaoHiem.Properties.Appearance.Options.UseFont = true;
            this.rdBaoHiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BHXH"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "KSK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rdBaoHiem.Size = new System.Drawing.Size(286, 25);
            this.rdBaoHiem.TabIndex = 131;
            // 
            // cklKhoaPhong
            // 
            this.cklKhoaPhong.CheckOnClick = true;
            this.cklKhoaPhong.DisplayMember = "TenKP";
            this.cklKhoaPhong.Location = new System.Drawing.Point(385, 12);
            this.cklKhoaPhong.Name = "cklKhoaPhong";
            this.cklKhoaPhong.Size = new System.Drawing.Size(206, 200);
            this.cklKhoaPhong.TabIndex = 133;
            this.cklKhoaPhong.ValueMember = "MaKP";
            this.cklKhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKhoaPhong_ItemCheck);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(295, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 17);
            this.labelControl3.TabIndex = 132;
            this.labelControl3.Text = "Khoa|Phòng:";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(202, 218);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(136, 23);
            this.btnThoat.TabIndex = 121;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(122, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 120;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frm_BCNhomVienPhiTheoKhoa_30003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 250);
            this.Controls.Add(this.cklKhoaPhong);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.rdBaoHiem);
            this.Controls.Add(this.rgCHon);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.rdNgay);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Name = "frm_BCNhomVienPhiTheoKhoa_30003";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhóm viện phí theo khoa";
            this.Load += new System.EventHandler(this.frm_BCVienPhi_30003_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgCHon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBaoHiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbNam;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup rdNgay;
        private DevExpress.XtraEditors.RadioGroup rgCHon;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup rdBaoHiem;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKhoaPhong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}