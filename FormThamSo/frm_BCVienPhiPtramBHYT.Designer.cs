namespace QLBV.FormThamSo
{
    partial class frm_BCVienPhiPtramBHYT
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.cklKhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.rgCHon = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.rgchonmau = new DevExpress.XtraEditors.RadioGroup();
            this.ckBHYT = new DevExpress.XtraEditors.CheckEdit();
            this.ckDichVu = new DevExpress.XtraEditors.CheckEdit();
            this.ckKSK = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgCHon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgchonmau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBHYT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckKSK.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(30, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(248, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(4, 39);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Khoa|Phòng:";
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(94, 12);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(145, 24);
            this.detungay.TabIndex = 0;
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(320, 12);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(145, 24);
            this.dedenngay.TabIndex = 1;
            // 
            // cklKhoaPhong
            // 
            this.cklKhoaPhong.CheckOnClick = true;
            this.cklKhoaPhong.DisplayMember = "TenKP";
            this.cklKhoaPhong.Location = new System.Drawing.Point(94, 39);
            this.cklKhoaPhong.Name = "cklKhoaPhong";
            this.cklKhoaPhong.Size = new System.Drawing.Size(371, 141);
            this.cklKhoaPhong.TabIndex = 2;
            this.cklKhoaPhong.ValueMember = "MaKP";
            this.cklKhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKhoaPhong_ItemCheck);
            this.cklKhoaPhong.SelectedIndexChanged += new System.EventHandler(this.cklKhoaPhong_SelectedIndexChanged);
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Location = new System.Drawing.Point(304, 303);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(75, 23);
            this.btntaobc.TabIndex = 3;
            this.btntaobc.Text = "Tạo BC";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(390, 303);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(75, 23);
            this.btnhuy.TabIndex = 4;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // rgCHon
            // 
            this.rgCHon.Location = new System.Drawing.Point(94, 217);
            this.rgCHon.Name = "rgCHon";
            this.rgCHon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgCHon.Properties.Appearance.Options.UseFont = true;
            this.rgCHon.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN cùng chi trả"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Khác"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rgCHon.Size = new System.Drawing.Size(371, 29);
            this.rgCHon.TabIndex = 5;
            this.rgCHon.SelectedIndexChanged += new System.EventHandler(this.rgCHon_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(4, 191);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(75, 17);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Đối Tượng:";
            // 
            // rgchonmau
            // 
            this.rgchonmau.Location = new System.Drawing.Point(94, 252);
            this.rgchonmau.Name = "rgchonmau";
            this.rgchonmau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgchonmau.Properties.Appearance.Options.UseFont = true;
            this.rgchonmau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chi phí theo ngày duyệt"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chi phí theo ngày thu")});
            this.rgchonmau.Size = new System.Drawing.Size(371, 28);
            this.rgchonmau.TabIndex = 7;
            this.rgchonmau.ToolTip = "Chi phí theo ngày thu chỉ áp dụng cho thu trực tiếp dịch vụ";
            // 
            // ckBHYT
            // 
            this.ckBHYT.Location = new System.Drawing.Point(94, 192);
            this.ckBHYT.Name = "ckBHYT";
            this.ckBHYT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckBHYT.Properties.Appearance.Options.UseFont = true;
            this.ckBHYT.Properties.Caption = "BHYT";
            this.ckBHYT.Size = new System.Drawing.Size(75, 19);
            this.ckBHYT.TabIndex = 8;
            // 
            // ckDichVu
            // 
            this.ckDichVu.Location = new System.Drawing.Point(217, 192);
            this.ckDichVu.Name = "ckDichVu";
            this.ckDichVu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckDichVu.Properties.Appearance.Options.UseFont = true;
            this.ckDichVu.Properties.Caption = "Dịch vụ";
            this.ckDichVu.Size = new System.Drawing.Size(75, 19);
            this.ckDichVu.TabIndex = 9;
            // 
            // ckKSK
            // 
            this.ckKSK.Location = new System.Drawing.Point(339, 192);
            this.ckKSK.Name = "ckKSK";
            this.ckKSK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckKSK.Properties.Appearance.Options.UseFont = true;
            this.ckKSK.Properties.Caption = "KSK";
            this.ckKSK.Size = new System.Drawing.Size(75, 19);
            this.ckKSK.TabIndex = 10;
            // 
            // frm_BCVienPhiPtramBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 334);
            this.Controls.Add(this.ckKSK);
            this.Controls.Add(this.ckDichVu);
            this.Controls.Add(this.ckBHYT);
            this.Controls.Add(this.rgchonmau);
            this.Controls.Add(this.rgCHon);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btntaobc);
            this.Controls.Add(this.cklKhoaPhong);
            this.Controls.Add(this.dedenngay);
            this.Controls.Add(this.detungay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BCVienPhiPtramBHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BC Thu viện phí theo PTrăm BHYT";
            this.Load += new System.EventHandler(this.frm_BCVienPhiPtramBHYT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgCHon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgchonmau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBHYT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckKSK.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKhoaPhong;
        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.RadioGroup rgCHon;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup rgchonmau;
        private DevExpress.XtraEditors.CheckEdit ckBHYT;
        private DevExpress.XtraEditors.CheckEdit ckDichVu;
        private DevExpress.XtraEditors.CheckEdit ckKSK;
    }
}