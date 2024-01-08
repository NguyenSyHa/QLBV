namespace QLBV.FormTraCuu
{
    partial class Frm_TCThuocTTchuaXuatct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TCThuocTTchuaXuatct));
            this.grcNhapCT = new DevExpress.XtraGrid.GridControl();
            this.grvNhapCT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDNhapct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.conDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongXD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoPL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaDuoc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lupSoLo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cboSoLo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.lupDonVi = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cboVAT = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.gcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.popupContainerEdit2 = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl2 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gcKho = new DevExpress.XtraGrid.GridControl();
            this.grvKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboOptionBC = new System.Windows.Forms.ComboBox();
            this.btnInBaoCao = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grcNhapCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhapCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSoLo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSoLo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl2)).BeginInit();
            this.popupContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).BeginInit();
            this.SuspendLayout();
            // 
            // grcNhapCT
            // 
            this.grcNhapCT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grcNhapCT.Location = new System.Drawing.Point(0, 92);
            this.grcNhapCT.MainView = this.grvNhapCT;
            this.grcNhapCT.Name = "grcNhapCT";
            this.grcNhapCT.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupMaDuoc,
            this.lupSoLo,
            this.cboSoLo,
            this.lupDonVi,
            this.cboVAT});
            this.grcNhapCT.Size = new System.Drawing.Size(1304, 439);
            this.grcNhapCT.TabIndex = 2;
            this.grcNhapCT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNhapCT});
            // 
            // grvNhapCT
            // 
            this.grvNhapCT.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvNhapCT.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvNhapCT.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvNhapCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvNhapCT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.colIDNhapct,
            this.colNgay,
            this.colKhoaPhong,
            this.colTenDV,
            this.conDonVi,
            this.colDonGia,
            this.colSoLuong,
            this.colThanhTien,
            this.colSoLuongXD,
            this.colMaBN,
            this.colTenBN,
            this.colSoPL,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.grvNhapCT.GridControl = this.grcNhapCT;
            this.grvNhapCT.Name = "grvNhapCT";
            this.grvNhapCT.OptionsBehavior.Editable = false;
            this.grvNhapCT.OptionsBehavior.ReadOnly = true;
            this.grvNhapCT.OptionsView.ColumnAutoWidth = false;
            this.grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvNhapCT.OptionsView.ShowFooter = true;
            this.grvNhapCT.OptionsView.ShowGroupPanel = false;
            this.grvNhapCT.OptionsView.ShowViewCaption = true;
            this.grvNhapCT.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvNhapCT_CustomDrawCell);
            // 
            // STT
            // 
            this.STT.Caption = "STT";
            this.STT.Name = "STT";
            this.STT.Visible = true;
            this.STT.VisibleIndex = 0;
            this.STT.Width = 40;
            // 
            // colIDNhapct
            // 
            this.colIDNhapct.Caption = "IDCT";
            this.colIDNhapct.FieldName = "ID";
            this.colIDNhapct.Name = "colIDNhapct";
            this.colIDNhapct.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colIDNhapct.Visible = true;
            this.colIDNhapct.VisibleIndex = 1;
            this.colIDNhapct.Width = 46;
            // 
            // colNgay
            // 
            this.colNgay.Caption = "Ngày tháng";
            this.colNgay.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgay.FieldName = "Ngay";
            this.colNgay.Name = "colNgay";
            this.colNgay.Visible = true;
            this.colNgay.VisibleIndex = 2;
            this.colNgay.Width = 78;
            // 
            // colKhoaPhong
            // 
            this.colKhoaPhong.Caption = "Tên Khoa/Phòng";
            this.colKhoaPhong.FieldName = "TenKP";
            this.colKhoaPhong.Name = "colKhoaPhong";
            this.colKhoaPhong.Visible = true;
            this.colKhoaPhong.VisibleIndex = 3;
            this.colKhoaPhong.Width = 117;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên dược";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.Width = 129;
            // 
            // conDonVi
            // 
            this.conDonVi.Caption = "Đơn vị";
            this.conDonVi.FieldName = "DonVi";
            this.conDonVi.Name = "conDonVi";
            this.conDonVi.Visible = true;
            this.conDonVi.VisibleIndex = 6;
            this.conDonVi.Width = 61;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 7;
            this.colDonGia.Width = 64;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "SL Kê đơn";
            this.colSoLuong.FieldName = "SoLuongKD";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 8;
            this.colSoLuong.Width = 62;
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "TT kê đơn";
            this.colThanhTien.FieldName = "ThanhTienKD";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 9;
            this.colThanhTien.Width = 86;
            // 
            // colSoLuongXD
            // 
            this.colSoLuongXD.Caption = "SL đã xuất";
            this.colSoLuongXD.FieldName = "SoLuongXD";
            this.colSoLuongXD.Name = "colSoLuongXD";
            this.colSoLuongXD.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colSoLuongXD.Visible = true;
            this.colSoLuongXD.VisibleIndex = 10;
            this.colSoLuongXD.Width = 67;
            // 
            // colMaBN
            // 
            this.colMaBN.Caption = "Mã BN";
            this.colMaBN.FieldName = "MaBN";
            this.colMaBN.Name = "colMaBN";
            this.colMaBN.Visible = true;
            this.colMaBN.VisibleIndex = 4;
            this.colMaBN.Width = 65;
            // 
            // colTenBN
            // 
            this.colTenBN.Caption = "Tên BN";
            this.colTenBN.FieldName = "TenBN";
            this.colTenBN.Name = "colTenBN";
            this.colTenBN.Visible = true;
            this.colTenBN.VisibleIndex = 5;
            this.colTenBN.Width = 97;
            // 
            // colSoPL
            // 
            this.colSoPL.Caption = "Số Phiếu lĩnh";
            this.colSoPL.FieldName = "SoPL";
            this.colSoPL.Name = "colSoPL";
            this.colSoPL.Visible = true;
            this.colSoPL.VisibleIndex = 11;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Số lô";
            this.gridColumn1.FieldName = "SoLo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 12;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Hạn dùng";
            this.gridColumn2.FieldName = "HanDung";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 13;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Khoa phòng xuất";
            this.gridColumn3.FieldName = "TenKXuat";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 14;
            this.gridColumn3.Width = 125;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày vào";
            this.gridColumn4.FieldName = "NgayVao";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 15;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày ra";
            this.gridColumn5.FieldName = "NgayRa";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 16;
            // 
            // lupMaDuoc
            // 
            this.lupMaDuoc.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupMaDuoc.AppearanceDropDown.Options.UseFont = true;
            this.lupMaDuoc.AutoHeight = false;
            this.lupMaDuoc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaDuoc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", 200, "Tên dược"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDV", "Mã dược", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DonVi", 50, "Đơn vị")});
            this.lupMaDuoc.DisplayMember = "TenDV";
            this.lupMaDuoc.Name = "lupMaDuoc";
            this.lupMaDuoc.NullText = "";
            this.lupMaDuoc.PopupFormMinSize = new System.Drawing.Size(250, 200);
            this.lupMaDuoc.ValueMember = "MaDV";
            // 
            // lupSoLo
            // 
            this.lupSoLo.AutoHeight = false;
            this.lupSoLo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupSoLo.Name = "lupSoLo";
            // 
            // cboSoLo
            // 
            this.cboSoLo.AutoHeight = false;
            this.cboSoLo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSoLo.Name = "cboSoLo";
            // 
            // lupDonVi
            // 
            this.lupDonVi.AutoHeight = false;
            this.lupDonVi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDonVi.Name = "lupDonVi";
            // 
            // cboVAT
            // 
            this.cboVAT.AutoHeight = false;
            this.cboVAT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVAT.Name = "cboVAT";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(241, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(82, 19);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "labelControl1";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControl3.Location = new System.Drawing.Point(12, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 13);
            this.labelControl3.TabIndex = 37;
            this.labelControl3.Text = "Khoa/Phòng:";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(461, 53);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 23);
            this.btnTimKiem.TabIndex = 44;
            this.btnTimKiem.Text = "&Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // gcKhoaPhong
            // 
            this.gcKhoaPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKhoaPhong.Location = new System.Drawing.Point(0, 0);
            this.gcKhoaPhong.MainView = this.grvKhoaPhong;
            this.gcKhoaPhong.Name = "gcKhoaPhong";
            this.gcKhoaPhong.Size = new System.Drawing.Size(310, 182);
            this.gcKhoaPhong.TabIndex = 45;
            this.gcKhoaPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong});
            // 
            // grvKhoaPhong
            // 
            this.grvKhoaPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenKP});
            this.grvKhoaPhong.GridControl = this.gcKhoaPhong;
            this.grvKhoaPhong.Name = "grvKhoaPhong";
            this.grvKhoaPhong.OptionsSelection.MultiSelect = true;
            this.grvKhoaPhong.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvKhoaPhong.OptionsView.ShowGroupPanel = false;
            // 
            // colTenKP
            // 
            this.colTenKP.Caption = "Tên khoa phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gcKhoaPhong);
            this.popupContainerControl1.Location = new System.Drawing.Point(416, 203);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(310, 182);
            this.popupContainerControl1.TabIndex = 46;
            // 
            // popupContainerEdit1
            // 
            this.popupContainerEdit1.Location = new System.Drawing.Point(92, 55);
            this.popupContainerEdit1.Name = "popupContainerEdit1";
            this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
            this.popupContainerEdit1.Size = new System.Drawing.Size(127, 20);
            this.popupContainerEdit1.TabIndex = 47;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControl1.Location = new System.Drawing.Point(259, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 48;
            this.labelControl1.Text = "Kho:";
            // 
            // popupContainerEdit2
            // 
            this.popupContainerEdit2.Location = new System.Drawing.Point(291, 55);
            this.popupContainerEdit2.Name = "popupContainerEdit2";
            this.popupContainerEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit2.Properties.PopupControl = this.popupContainerControl2;
            this.popupContainerEdit2.Size = new System.Drawing.Size(127, 20);
            this.popupContainerEdit2.TabIndex = 49;
            // 
            // popupContainerControl2
            // 
            this.popupContainerControl2.Controls.Add(this.gcKho);
            this.popupContainerControl2.Location = new System.Drawing.Point(38, 221);
            this.popupContainerControl2.Name = "popupContainerControl2";
            this.popupContainerControl2.Size = new System.Drawing.Size(218, 164);
            this.popupContainerControl2.TabIndex = 50;
            // 
            // gcKho
            // 
            this.gcKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKho.Location = new System.Drawing.Point(0, 0);
            this.gcKho.MainView = this.grvKho;
            this.gcKho.Name = "gcKho";
            this.gcKho.Size = new System.Drawing.Size(218, 164);
            this.gcKho.TabIndex = 0;
            this.gcKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKho});
            // 
            // grvKho
            // 
            this.grvKho.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenKho});
            this.grvKho.GridControl = this.gcKho;
            this.grvKho.Name = "grvKho";
            this.grvKho.OptionsSelection.MultiSelect = true;
            this.grvKho.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvKho.OptionsView.ShowGroupPanel = false;
            // 
            // colTenKho
            // 
            this.colTenKho.Caption = "Tên kho";
            this.colTenKho.FieldName = "TenKP";
            this.colTenKho.Name = "colTenKho";
            this.colTenKho.Visible = true;
            this.colTenKho.VisibleIndex = 1;
            // 
            // cboOptionBC
            // 
            this.cboOptionBC.FormattingEnabled = true;
            this.cboOptionBC.Items.AddRange(new object[] {
            "Ngày xuất",
            "Biểu 20"});
            this.cboOptionBC.Location = new System.Drawing.Point(1064, 55);
            this.cboOptionBC.Name = "cboOptionBC";
            this.cboOptionBC.Size = new System.Drawing.Size(121, 21);
            this.cboOptionBC.TabIndex = 52;
            this.cboOptionBC.SelectedIndexChanged += new System.EventHandler(this.cboOptionBC_SelectedIndexChanged);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBaoCao.Appearance.Options.UseFont = true;
            this.btnInBaoCao.Location = new System.Drawing.Point(1204, 53);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(100, 23);
            this.btnInBaoCao.TabIndex = 53;
            this.btnInBaoCao.Text = "&In Báo Cáo";
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // Frm_TCThuocTTchuaXuatct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 543);
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.cboOptionBC);
            this.Controls.Add(this.popupContainerControl2);
            this.Controls.Add(this.popupContainerEdit2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.popupContainerEdit1);
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grcNhapCT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TCThuocTTchuaXuatct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết lịch sử dược";
            this.Load += new System.EventHandler(this.Frm_TcNhapXuatTonct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcNhapCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhapCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupSoLo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSoLo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl2)).EndInit();
            this.popupContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcNhapCT;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNhapCT;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn colIDNhapct;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaDuoc;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay;
        private DevExpress.XtraGrid.Columns.GridColumn conDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupSoLo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboSoLo;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongXD;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBN;
        private DevExpress.XtraGrid.Columns.GridColumn colSoPL;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit2;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl2;
        private DevExpress.XtraGrid.GridControl gcKho;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKho;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKho;
        private System.Windows.Forms.ComboBox cboOptionBC;
        private DevExpress.XtraEditors.SimpleButton btnInBaoCao;
    }
}