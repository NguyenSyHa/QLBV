namespace QLBV.FormThamSo
{
    partial class frm_ChamCongPhauThuat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChamCongPhauThuat));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupNhomDichVu = new DevExpress.XtraEditors.LookUpEdit();
            this.lupTT = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.deNgayTu = new DevExpress.XtraEditors.DateEdit();
            this.deNgayDen = new DevExpress.XtraEditors.DateEdit();
            this.lupDichVu = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lupKPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.chkDT = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lupTenCB = new DevExpress.XtraEditors.LookUpEdit();
            this.rgbc = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.tltt = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tltt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(10, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(213, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(10, 210);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 17);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Dịch vụ:";
            // 
            // lupNhomDichVu
            // 
            this.lupNhomDichVu.Location = new System.Drawing.Point(103, 89);
            this.lupNhomDichVu.Name = "lupNhomDichVu";
            this.lupNhomDichVu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNhomDichVu.Properties.Appearance.Options.UseFont = true;
            this.lupNhomDichVu.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNhomDichVu.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lupNhomDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhomDichVu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên Tiểu Nhóm", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdTieuNhom", "IdTieuNhom", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenRG", "Tên Rút Gọn", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupNhomDichVu.Properties.DisplayMember = "TenTN";
            this.lupNhomDichVu.Properties.NullText = "Chọn nhóm dịch vụ";
            this.lupNhomDichVu.Properties.ValueMember = "IdTieuNhom";
            this.lupNhomDichVu.Size = new System.Drawing.Size(285, 24);
            this.lupNhomDichVu.TabIndex = 3;
            this.lupNhomDichVu.EditValueChanged += new System.EventHandler(this.lupNhomDichVu_EditValueChanged);
            // 
            // lupTT
            // 
            this.lupTT.Location = new System.Drawing.Point(103, 238);
            this.lupTT.Name = "lupTT";
            this.lupTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTT.Properties.Appearance.Options.UseFont = true;
            this.lupTT.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTT.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lupTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTT.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KieuTT", "Thanh Toán"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Giatri", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupTT.Properties.NullText = "Chọn kiểu thanh toán";
            this.lupTT.Size = new System.Drawing.Size(285, 24);
            this.lupTT.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(10, 240);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 17);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Thanh toán:";
            // 
            // deNgayTu
            // 
            this.deNgayTu.EditValue = null;
            this.deNgayTu.Location = new System.Drawing.Point(103, 5);
            this.deNgayTu.Name = "deNgayTu";
            this.deNgayTu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deNgayTu.Properties.Appearance.Options.UseFont = true;
            this.deNgayTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayTu.Size = new System.Drawing.Size(104, 24);
            this.deNgayTu.TabIndex = 0;
            // 
            // deNgayDen
            // 
            this.deNgayDen.EditValue = null;
            this.deNgayDen.Location = new System.Drawing.Point(284, 5);
            this.deNgayDen.Name = "deNgayDen";
            this.deNgayDen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deNgayDen.Properties.Appearance.Options.UseFont = true;
            this.deNgayDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayDen.Size = new System.Drawing.Size(104, 24);
            this.deNgayDen.TabIndex = 1;
            // 
            // lupDichVu
            // 
            this.lupDichVu.Location = new System.Drawing.Point(103, 208);
            this.lupDichVu.Name = "lupDichVu";
            this.lupDichVu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupDichVu.Properties.Appearance.Options.UseFont = true;
            this.lupDichVu.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupDichVu.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lupDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDichVu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", "Tên Dịch Vụ", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDV", "Mã DV", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupDichVu.Properties.DisplayMember = "TenDV";
            this.lupDichVu.Properties.NullText = "Chọn dịch vụ";
            this.lupDichVu.Properties.ValueMember = "MaDV";
            this.lupDichVu.Size = new System.Drawing.Size(285, 24);
            this.lupDichVu.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(10, 91);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(69, 17);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "Nhóm DV:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(10, 61);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(85, 17);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Khoa Phòng:";
            // 
            // lupKPhong
            // 
            this.lupKPhong.Location = new System.Drawing.Point(103, 59);
            this.lupKPhong.Name = "lupKPhong";
            this.lupKPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKPhong.Properties.Appearance.Options.UseFont = true;
            this.lupKPhong.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKPhong.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lupKPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên Khoa Phòng", 285, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.lupKPhong.Properties.DisplayMember = "TenKP";
            this.lupKPhong.Properties.NullText = "Chọn khoa phòng";
            this.lupKPhong.Properties.ValueMember = "MaKP";
            this.lupKPhong.Size = new System.Drawing.Size(285, 24);
            this.lupKPhong.TabIndex = 2;
            // 
            // chkDT
            // 
            this.chkDT.Location = new System.Drawing.Point(103, 268);
            this.chkDT.Name = "chkDT";
            this.chkDT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkDT.Properties.Appearance.Options.UseFont = true;
            this.chkDT.Properties.Caption = "Để trắng (để viết tay) từ cột số 8.";
            this.chkDT.Size = new System.Drawing.Size(283, 23);
            this.chkDT.TabIndex = 18;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(10, 121);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(29, 17);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Loại";
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.checkedListBoxControl1.Appearance.Options.UseFont = true;
            this.checkedListBoxControl1.CheckOnClick = true;
            this.checkedListBoxControl1.DisplayMember = "TenLoai";
            this.checkedListBoxControl1.Location = new System.Drawing.Point(103, 119);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(285, 83);
            this.checkedListBoxControl1.TabIndex = 20;
            this.checkedListBoxControl1.ValueMember = "Loai";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(10, 36);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(77, 17);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Tên cán bộ:";
            // 
            // lupTenCB
            // 
            this.lupTenCB.Location = new System.Drawing.Point(103, 35);
            this.lupTenCB.Name = "lupTenCB";
            this.lupTenCB.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTenCB.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lupTenCB.Properties.Appearance.Options.UseFont = true;
            this.lupTenCB.Properties.Appearance.Options.UseForeColor = true;
            this.lupTenCB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTenCB.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", "Tên cán bộ"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", "Mã Cán Bộ")});
            this.lupTenCB.Properties.DisplayMember = "TenCB";
            this.lupTenCB.Properties.NullText = "Chọn tên cán bộ";
            this.lupTenCB.Properties.ValueMember = "MaCB";
            this.lupTenCB.Size = new System.Drawing.Size(285, 24);
            this.lupTenCB.TabIndex = 21;
            this.lupTenCB.EditValueChanged += new System.EventHandler(this.lupTenCB_EditValueChanged);
            // 
            // rgbc
            // 
            this.rgbc.Location = new System.Drawing.Point(10, 308);
            this.rgbc.Name = "rgbc";
            this.rgbc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgbc.Properties.Appearance.Options.UseFont = true;
            this.rgbc.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu chung"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu BV: 27022"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu BV: 01830"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu BV: 26007")});
            this.rgbc.Size = new System.Drawing.Size(433, 56);
            this.rgbc.TabIndex = 22;
            this.rgbc.SelectedIndexChanged += new System.EventHandler(this.rgbc_SelectedIndexChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Location = new System.Drawing.Point(10, 376);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(45, 17);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "TLTT:";
            this.labelControl9.Visible = false;
            // 
            // tltt
            // 
            this.tltt.Location = new System.Drawing.Point(103, 374);
            this.tltt.Name = "tltt";
            this.tltt.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tltt.Properties.Appearance.Options.UseFont = true;
            this.tltt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tltt.Properties.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "Tất cả"});
            this.tltt.Size = new System.Drawing.Size(107, 24);
            this.tltt.TabIndex = 25;
            this.tltt.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(103, 404);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 40);
            this.btnOK.TabIndex = 6;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "&Tạo báo cáo";
            this.btnOK.ToolTipTitle = "jhgjgjhj";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(213, 404);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(107, 40);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frm_ChamCongPhauThuat
            // 
            this.AcceptButton = this.btnOK;
            this.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 453);
            this.Controls.Add(this.tltt);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.rgbc);
            this.Controls.Add(this.lupTenCB);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.chkDT);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.lupKPhong);
            this.Controls.Add(this.lupDichVu);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.deNgayDen);
            this.Controls.Add(this.deNgayTu);
            this.Controls.Add(this.lupTT);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lupNhomDichVu);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChamCongPhauThuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo chấm công phẫu thuật";
            this.Load += new System.EventHandler(this.frm_ChamCongPhauThuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tltt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupNhomDichVu;
        private DevExpress.XtraEditors.LookUpEdit lupTT;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit deNgayTu;
        private DevExpress.XtraEditors.DateEdit deNgayDen;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LookUpEdit lupDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lupKPhong;
        private DevExpress.XtraEditors.CheckEdit chkDT;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lupTenCB;
        private DevExpress.XtraEditors.RadioGroup rgbc;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ComboBoxEdit tltt;
    }
}