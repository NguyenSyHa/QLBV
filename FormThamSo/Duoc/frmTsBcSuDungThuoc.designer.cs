namespace QLBV.FormThamSo
{
    partial class frmTsBcSuDungThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTsBcSuDungThuoc));
            this.ckInThang = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupDVBC = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.lupTenPLoai = new DevExpress.XtraEditors.LookUpEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.lupTenNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.radMau = new DevExpress.XtraEditors.RadioGroup();
            this.cklKhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lupTenPLoai1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.ckInThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDVBC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenPLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenPLoai1)).BeginInit();
            this.SuspendLayout();
            // 
            // ckInThang
            // 
            this.ckInThang.Location = new System.Drawing.Point(129, 274);
            this.ckInThang.Name = "ckInThang";
            this.ckInThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ckInThang.Properties.Appearance.Options.UseFont = true;
            this.ckInThang.Properties.Caption = "In theo tháng";
            this.ckInThang.Size = new System.Drawing.Size(120, 23);
            this.ckInThang.TabIndex = 6;
            this.ckInThang.CheckedChanged += new System.EventHandler(this.ckQuy_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(32, 169);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 19);
            this.labelControl1.TabIndex = 72;
            this.labelControl1.Text = "ĐV báo cáo:";
            this.labelControl1.ToolTip = "Đơn vị báo cáo sử dụng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(28, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 70;
            this.label1.Text = "Kho sử dụng:";
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(131, 123);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.PopupSizeable = false;
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(303, 26);
            this.lupKho.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(266, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 19);
            this.label3.TabIndex = 76;
            this.label3.Text = "đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(28, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 75;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(306, 73);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(128, 26);
            this.lupDenNgay.TabIndex = 1;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(131, 73);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(129, 26);
            this.lupTuNgay.TabIndex = 0;
            // 
            // lupDVBC
            // 
            this.lupDVBC.Location = new System.Drawing.Point(131, 166);
            this.lupDVBC.Name = "lupDVBC";
            this.lupDVBC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDVBC.Properties.Appearance.Options.UseFont = true;
            this.lupDVBC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDVBC.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupDVBC.Properties.DisplayMember = "TenKP";
            this.lupDVBC.Properties.NullText = "";
            this.lupDVBC.Properties.PopupSizeable = false;
            this.lupDVBC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupDVBC.Properties.ValueMember = "MaKP";
            this.lupDVBC.Size = new System.Drawing.Size(303, 26);
            this.lupDVBC.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(28, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 19);
            this.label4.TabIndex = 117;
            this.label4.Text = "Phân loại DV:";
            // 
            // lupTenPLoai
            // 
            this.lupTenPLoai.Location = new System.Drawing.Point(131, 207);
            this.lupTenPLoai.Name = "lupTenPLoai";
            this.lupTenPLoai.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTenPLoai.Properties.Appearance.Options.UseFont = true;
            this.lupTenPLoai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTenPLoai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", "Phân loại")});
            this.lupTenPLoai.Properties.DisplayMember = "TenNhom";
            this.lupTenPLoai.Properties.NullText = "";
            this.lupTenPLoai.Properties.PopupSizeable = false;
            this.lupTenPLoai.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTenPLoai.Properties.ValueMember = "TenNhomCT";
            this.lupTenPLoai.Size = new System.Drawing.Size(303, 26);
            this.lupTenPLoai.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(28, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 19);
            this.label5.TabIndex = 119;
            this.label5.Text = "Nhóm dược:";
            this.label5.Visible = false;
            // 
            // lupTenNhom
            // 
            this.lupTenNhom.Location = new System.Drawing.Point(131, 251);
            this.lupTenNhom.Name = "lupTenNhom";
            this.lupTenNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTenNhom.Properties.Appearance.Options.UseFont = true;
            this.lupTenNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTenNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên nhóm")});
            this.lupTenNhom.Properties.DisplayMember = "TenTN";
            this.lupTenNhom.Properties.NullText = "";
            this.lupTenNhom.Properties.PopupSizeable = false;
            this.lupTenNhom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTenNhom.Properties.ValueMember = "IdTieuNhom";
            this.lupTenNhom.Size = new System.Drawing.Size(303, 26);
            this.lupTenNhom.TabIndex = 5;
            this.lupTenNhom.Visible = false;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(331, 304);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(103, 23);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(131, 304);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(101, 23);
            this.btnInBC.TabIndex = 7;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // radMau
            // 
            this.radMau.Location = new System.Drawing.Point(131, 12);
            this.radMau.Name = "radMau";
            this.radMau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu chung"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu BV Tam Đường"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu nhiều kho")});
            this.radMau.Size = new System.Drawing.Size(303, 55);
            this.radMau.TabIndex = 120;
            this.radMau.SelectedIndexChanged += new System.EventHandler(this.radMau_SelectedIndexChanged);
            // 
            // cklKhoaPhong
            // 
            this.cklKhoaPhong.CheckOnClick = true;
            this.cklKhoaPhong.DisplayMember = "TenKP";
            this.cklKhoaPhong.Location = new System.Drawing.Point(131, 105);
            this.cklKhoaPhong.Name = "cklKhoaPhong";
            this.cklKhoaPhong.Size = new System.Drawing.Size(303, 59);
            this.cklKhoaPhong.TabIndex = 121;
            this.cklKhoaPhong.ValueMember = "MaKP";
            this.cklKhoaPhong.Visible = false;
            this.cklKhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKhoaPhong_ItemCheck);
            // 
            // lupTenPLoai1
            // 
            this.lupTenPLoai1.CheckOnClick = true;
            this.lupTenPLoai1.DisplayMember = "TenNhom";
            this.lupTenPLoai1.Location = new System.Drawing.Point(131, 198);
            this.lupTenPLoai1.Name = "lupTenPLoai1";
            this.lupTenPLoai1.Size = new System.Drawing.Size(303, 70);
            this.lupTenPLoai1.TabIndex = 122;
            this.lupTenPLoai1.ValueMember = "IDNhom";
            this.lupTenPLoai1.Visible = false;
            this.lupTenPLoai1.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.lupTenPLoai1_ItemCheck);
            // 
            // frmTsBcSuDungThuoc
            // 
            this.AcceptButton = this.btnHuy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 336);
            this.Controls.Add(this.lupTenPLoai1);
            this.Controls.Add(this.cklKhoaPhong);
            this.Controls.Add(this.radMau);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lupTenNhom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lupDVBC);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.ckInThang);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.lupTenPLoai);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTsBcSuDungThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "In Báo cáo sử dụng dược";
            this.Load += new System.EventHandler(this.frmTsBcSuDungThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ckInThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDVBC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenPLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenPLoai1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.CheckEdit ckInThang;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.LookUpEdit lupDVBC;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.LookUpEdit lupTenPLoai;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lupTenNhom;
        private DevExpress.XtraEditors.RadioGroup radMau;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKhoaPhong;
        private DevExpress.XtraEditors.CheckedListBoxControl lupTenPLoai1;
    }
}