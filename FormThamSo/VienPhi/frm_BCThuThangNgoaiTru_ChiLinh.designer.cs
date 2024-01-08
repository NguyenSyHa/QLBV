namespace QLBV.FormThamSo
{
    partial class frm_BCThuThangNgoaiTru_ChiLinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCThuThangNgoaiTru_ChiLinh));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.radTimKiem = new DevExpress.XtraEditors.RadioGroup();
            this.rdTrongBH = new DevExpress.XtraEditors.RadioGroup();
            this.rdFont = new DevExpress.XtraEditors.RadioGroup();
            this.lbDuongDan = new DevExpress.XtraEditors.LabelControl();
            this.txtDuongDan = new DevExpress.XtraEditors.TextEdit();
            this.chkXuatExel = new DevExpress.XtraEditors.CheckEdit();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuongDan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.grcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckGrvKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colmaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.cboNoiTinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lupDoituong = new DevExpress.XtraEditors.LookUpEdit();
            this.cklNhomDV = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.label4 = new System.Windows.Forms.Label();
            this.radNoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.rgChonMau = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongBH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongDan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNoiTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklNhomDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMau.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(163, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "Đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(203, 11);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.lupDenNgay.Properties.CalendarTimeProperties.EditFormat.FormatString = "dd/MM/yyyy";
            this.lupDenNgay.Size = new System.Drawing.Size(95, 24);
            this.lupDenNgay.TabIndex = 2;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(68, 11);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(95, 24);
            this.lupTuNgay.TabIndex = 1;
            // 
            // radTimKiem
            // 
            this.radTimKiem.EditValue = 1;
            this.radTimKiem.Location = new System.Drawing.Point(13, 213);
            this.radTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.radTimKiem.Name = "radTimKiem";
            this.radTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTimKiem.Properties.Appearance.Options.UseFont = true;
            this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tìm kiếm theo ngày ra viện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Tìm kiếm theo ngày T.Toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Tìm kiếm theo ngày duyệt "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tìm kiếm theo ngày thu")});
            this.radTimKiem.Properties.SelectedIndexChanged += new System.EventHandler(this.rdFont_Properties_SelectedIndexChanged);
            this.radTimKiem.Size = new System.Drawing.Size(285, 91);
            this.radTimKiem.TabIndex = 3;
            this.radTimKiem.SelectedIndexChanged += new System.EventHandler(this.radTimKiem_SelectedIndexChanged);
            // 
            // rdTrongBH
            // 
            this.rdTrongBH.Location = new System.Drawing.Point(12, 341);
            this.rdTrongBH.Name = "rdTrongBH";
            this.rdTrongBH.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTrongBH.Properties.Appearance.Options.UseFont = true;
            this.rdTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Ngoài BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Trong BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rdTrongBH.Size = new System.Drawing.Size(285, 25);
            this.rdTrongBH.TabIndex = 5;
            // 
            // rdFont
            // 
            this.rdFont.Location = new System.Drawing.Point(122, 9);
            this.rdFont.Name = "rdFont";
            this.rdFont.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "TCVN3"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Unicode")});
            this.rdFont.Size = new System.Drawing.Size(200, 23);
            this.rdFont.TabIndex = 18;
            this.rdFont.Visible = false;
            this.rdFont.EditValueChanged += new System.EventHandler(this.rdFont_EditValueChanged);
            // 
            // lbDuongDan
            // 
            this.lbDuongDan.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbDuongDan.Location = new System.Drawing.Point(34, 44);
            this.lbDuongDan.Name = "lbDuongDan";
            this.lbDuongDan.Size = new System.Drawing.Size(64, 15);
            this.lbDuongDan.TabIndex = 80;
            this.lbDuongDan.Text = "Đường dẫn:";
            this.lbDuongDan.Visible = false;
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Location = new System.Drawing.Point(149, 41);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuongDan.Properties.Appearance.Options.UseFont = true;
            this.txtDuongDan.Properties.ReadOnly = true;
            this.txtDuongDan.Size = new System.Drawing.Size(173, 20);
            this.txtDuongDan.TabIndex = 20;
            this.txtDuongDan.Visible = false;
            // 
            // chkXuatExel
            // 
            this.chkXuatExel.Location = new System.Drawing.Point(31, 10);
            this.chkXuatExel.Margin = new System.Windows.Forms.Padding(4);
            this.chkXuatExel.Name = "chkXuatExel";
            this.chkXuatExel.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkXuatExel.Properties.Appearance.Options.UseFont = true;
            this.chkXuatExel.Properties.Caption = "Xuất Excel";
            this.chkXuatExel.Size = new System.Drawing.Size(87, 19);
            this.chkXuatExel.TabIndex = 17;
            this.chkXuatExel.CheckedChanged += new System.EventHandler(this.chkXuatExel_CheckedChanged);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(453, 382);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(102, 29);
            this.btnInBC.TabIndex = 24;
            this.btnInBC.Text = "&In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // btnDuongDan
            // 
            this.btnDuongDan.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDuongDan.Appearance.Options.UseFont = true;
            this.btnDuongDan.Image = global::QLBV.Properties.Resources.save_16x16;
            this.btnDuongDan.Location = new System.Drawing.Point(122, 40);
            this.btnDuongDan.Name = "btnDuongDan";
            this.btnDuongDan.Size = new System.Drawing.Size(25, 23);
            this.btnDuongDan.TabIndex = 19;
            this.btnDuongDan.Visible = false;
            this.btnDuongDan.Click += new System.EventHandler(this.btnDuongDan_Click_1);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(561, 382);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 29);
            this.btnHuy.TabIndex = 25;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // grcKhoaPhong
            // 
            this.grcKhoaPhong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grcKhoaPhong.Location = new System.Drawing.Point(304, 3);
            this.grcKhoaPhong.MainView = this.grvKhoaPhong;
            this.grcKhoaPhong.Name = "grcKhoaPhong";
            this.grcKhoaPhong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaPhong.Size = new System.Drawing.Size(352, 272);
            this.grcKhoaPhong.TabIndex = 13;
            this.grcKhoaPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong});
            this.grcKhoaPhong.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grcKhoaPhong_ProcessGridKey);
            // 
            // grvKhoaPhong
            // 
            this.grvKhoaPhong.Appearance.ViewCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvKhoaPhong.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grvKhoaPhong.Appearance.ViewCaption.Options.UseFont = true;
            this.grvKhoaPhong.Appearance.ViewCaption.Options.UseForeColor = true;
            this.grvKhoaPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckGrvKP,
            this.colmaKP,
            this.colTenKP});
            this.grvKhoaPhong.GridControl = this.grcKhoaPhong;
            this.grvKhoaPhong.Name = "grvKhoaPhong";
            this.grvKhoaPhong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaPhong.OptionsView.ShowViewCaption = true;
            this.grvKhoaPhong.ViewCaption = "Chọn Khoa Phòng";
            this.grvKhoaPhong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaPhong_CellValueChanging);
            // 
            // colCheckGrvKP
            // 
            this.colCheckGrvKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCheckGrvKP.AppearanceHeader.Options.UseFont = true;
            this.colCheckGrvKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colCheckGrvKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckGrvKP.Caption = "Chọn";
            this.colCheckGrvKP.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCheckGrvKP.FieldName = "Check";
            this.colCheckGrvKP.Name = "colCheckGrvKP";
            this.colCheckGrvKP.Visible = true;
            this.colCheckGrvKP.VisibleIndex = 0;
            this.colCheckGrvKP.Width = 48;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colmaKP
            // 
            this.colmaKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colmaKP.AppearanceHeader.Options.UseFont = true;
            this.colmaKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colmaKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colmaKP.Caption = "Mã KP";
            this.colmaKP.FieldName = "MaKP";
            this.colmaKP.Name = "colmaKP";
            this.colmaKP.Width = 51;
            // 
            // colTenKP
            // 
            this.colTenKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenKP.AppearanceHeader.Options.UseFont = true;
            this.colTenKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenKP.Caption = "Tên Khoa Phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.OptionsColumn.ReadOnly = true;
            this.colTenKP.OptionsFilter.AllowFilter = false;
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            this.colTenKP.Width = 334;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.radioGroup1);
            this.panelControl1.Controls.Add(this.chkXuatExel);
            this.panelControl1.Controls.Add(this.rdFont);
            this.panelControl1.Controls.Add(this.txtDuongDan);
            this.panelControl1.Controls.Add(this.lbDuongDan);
            this.panelControl1.Controls.Add(this.btnDuongDan);
            this.panelControl1.Location = new System.Drawing.Point(304, 310);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(352, 68);
            this.panelControl1.TabIndex = 14;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(31, -21);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Size = new System.Drawing.Size(100, 15);
            this.radioGroup1.TabIndex = 507;
            // 
            // cboNoiTinh
            // 
            this.cboNoiTinh.EditValue = "Tất cả";
            this.cboNoiTinh.Location = new System.Drawing.Point(99, 372);
            this.cboNoiTinh.Name = "cboNoiTinh";
            this.cboNoiTinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboNoiTinh.Properties.Appearance.Options.UseFont = true;
            this.cboNoiTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNoiTinh.Properties.Items.AddRange(new object[] {
            "Tất cả",
            "A. BN nội tỉnh KCB ban đầu",
            "B. BN nội tỉnh đến",
            "C. BN ngoại tỉnh đến"});
            this.cboNoiTinh.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboNoiTinh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboNoiTinh.Size = new System.Drawing.Size(198, 24);
            this.cboNoiTinh.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(11, 375);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 17);
            this.labelControl3.TabIndex = 90;
            this.labelControl3.Text = "Nội|NgTỉnh:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(10, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 94;
            this.label1.Text = "Đối tượng:";
            // 
            // lupDoituong
            // 
            this.lupDoituong.EnterMoveNextControl = true;
            this.lupDoituong.Location = new System.Drawing.Point(98, 311);
            this.lupDoituong.Name = "lupDoituong";
            this.lupDoituong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDoituong.Properties.Appearance.Options.UseFont = true;
            this.lupDoituong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDoituong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng")});
            this.lupDoituong.Properties.DisplayMember = "DTBN1";
            this.lupDoituong.Properties.NullText = "";
            this.lupDoituong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupDoituong.Properties.ValueMember = "IDDTBN";
            this.lupDoituong.Size = new System.Drawing.Size(198, 24);
            this.lupDoituong.TabIndex = 95;
            this.lupDoituong.EditValueChanged += new System.EventHandler(this.lupDoituong_EditValueChanged);
            // 
            // cklNhomDV
            // 
            this.cklNhomDV.CheckOnClick = true;
            this.cklNhomDV.Location = new System.Drawing.Point(67, 73);
            this.cklNhomDV.Name = "cklNhomDV";
            this.cklNhomDV.Size = new System.Drawing.Size(230, 135);
            this.cklNhomDV.TabIndex = 504;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(5, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 505;
            this.label4.Text = "Nhóm:";
            // 
            // radNoiNgoaiTru
            // 
            this.radNoiNgoaiTru.Location = new System.Drawing.Point(8, 42);
            this.radNoiNgoaiTru.Name = "radNoiNgoaiTru";
            this.radNoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radNoiNgoaiTru.Size = new System.Drawing.Size(290, 25);
            this.radNoiNgoaiTru.TabIndex = 506;
            // 
            // rgChonMau
            // 
            this.rgChonMau.Location = new System.Drawing.Point(304, 280);
            this.rgChonMau.Name = "rgChonMau";
            this.rgChonMau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgChonMau.Properties.Appearance.Options.UseFont = true;
            this.rgChonMau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tổng hợp theo dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chi tiết theo BN")});
            this.rgChonMau.Size = new System.Drawing.Size(352, 24);
            this.rgChonMau.TabIndex = 507;
            this.rgChonMau.SelectedIndexChanged += new System.EventHandler(this.rgChonMau_SelectedIndexChanged);
            // 
            // frm_BCThuThangNgoaiTru_ChiLinh
            // 
            this.AcceptButton = this.btnInBC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 418);
            this.Controls.Add(this.rgChonMau);
            this.Controls.Add(this.radNoiNgoaiTru);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cklNhomDV);
            this.Controls.Add(this.lupDoituong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboNoiTinh);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grcKhoaPhong);
            this.Controls.Add(this.rdTrongBH);
            this.Controls.Add(this.radTimKiem);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCThuThangNgoaiTru_ChiLinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê chi phí thu thẳng bệnh nhân ngoại trú";
            this.Load += new System.EventHandler(this.frmTsBcMau20_1399_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongBH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongDan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNoiTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklNhomDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMau.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.RadioGroup radTimKiem;
        private DevExpress.XtraEditors.RadioGroup rdTrongBH;
        private DevExpress.XtraEditors.RadioGroup rdFont;
        private DevExpress.XtraEditors.LabelControl lbDuongDan;
        private DevExpress.XtraEditors.SimpleButton btnDuongDan;
        private DevExpress.XtraEditors.TextEdit txtDuongDan;
        private DevExpress.XtraEditors.CheckEdit chkXuatExel;
        private DevExpress.XtraGrid.GridControl grcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckGrvKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colmaKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboNoiTinh;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupDoituong;
        private DevExpress.XtraEditors.CheckedListBoxControl cklNhomDV;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.RadioGroup radNoiNgoaiTru;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup rgChonMau;
    }
}