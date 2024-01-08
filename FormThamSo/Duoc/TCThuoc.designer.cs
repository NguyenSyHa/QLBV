namespace QLBV.TraCuu
{
    partial class TCThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCThuoc));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoDuoc = new DevExpress.XtraGrid.GridControl();
            this.grvKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chonKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaDV = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.grcTraCuu = new DevExpress.XtraGrid.GridControl();
            this.grvTraCuu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenThuoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtTimTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtTimDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.grcChiTietThuoc = new DevExpress.XtraGrid.GridControl();
            this.grvChiTietThuoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDDon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngayKe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBoPhanKe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoPL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.donGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.soLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnIn = new System.Windows.Forms.Button();
            this.grcKhoa = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chonKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoDuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTraCuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTraCuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTietThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kho";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Khoa/Phòng";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcKhoDuoc);
            this.groupControl2.Location = new System.Drawing.Point(25, 77);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(336, 124);
            this.groupControl2.TabIndex = 126;
            // 
            // grcKhoDuoc
            // 
            this.grcKhoDuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoDuoc.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoDuoc.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcKhoDuoc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoDuoc.Location = new System.Drawing.Point(2, 2);
            this.grcKhoDuoc.MainView = this.grvKho;
            this.grcKhoDuoc.Name = "grcKhoDuoc";
            this.grcKhoDuoc.Size = new System.Drawing.Size(332, 120);
            this.grcKhoDuoc.TabIndex = 0;
            this.grcKhoDuoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKho});
            // 
            // grvKho
            // 
            this.grvKho.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.chonKho,
            this.tenKho,
            this.maKho});
            this.grvKho.GridControl = this.grcKhoDuoc;
            this.grvKho.Name = "grvKho";
            this.grvKho.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKho.OptionsView.ShowGroupPanel = false;
            this.grvKho.OptionsView.ShowViewCaption = true;
            this.grvKho.ViewCaption = "Kho";
            this.grvKho.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKho_CellValueChanging);
            // 
            // chonKho
            // 
            this.chonKho.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chonKho.AppearanceHeader.Options.UseFont = true;
            this.chonKho.Caption = "Chọn";
            this.chonKho.FieldName = "Chon";
            this.chonKho.MinWidth = 10;
            this.chonKho.Name = "chonKho";
            this.chonKho.Visible = true;
            this.chonKho.VisibleIndex = 0;
            this.chonKho.Width = 28;
            // 
            // tenKho
            // 
            this.tenKho.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.tenKho.AppearanceHeader.Options.UseFont = true;
            this.tenKho.Caption = "Tên kho";
            this.tenKho.FieldName = "TenKP";
            this.tenKho.Name = "tenKho";
            this.tenKho.OptionsColumn.AllowEdit = false;
            this.tenKho.Visible = true;
            this.tenKho.VisibleIndex = 1;
            this.tenKho.Width = 252;
            // 
            // maKho
            // 
            this.maKho.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.maKho.AppearanceHeader.Options.UseFont = true;
            this.maKho.Caption = "Mã";
            this.maKho.FieldName = "MaKP";
            this.maKho.Name = "maKho";
            this.maKho.Visible = true;
            this.maKho.VisibleIndex = 2;
            this.maKho.Width = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 127;
            this.label3.Text = "Mã thuốc:";
            // 
            // txtMaDV
            // 
            this.txtMaDV.Location = new System.Drawing.Point(83, 23);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Size = new System.Drawing.Size(100, 20);
            this.txtMaDV.TabIndex = 128;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 129;
            this.label4.Text = "Từ ngày:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 130;
            this.label5.Text = "đến ngày";
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTraCuu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTraCuu.Image = ((System.Drawing.Image)(resources.GetObject("btnTraCuu.Image")));
            this.btnTraCuu.Location = new System.Drawing.Point(14, 84);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(108, 31);
            this.btnTraCuu.TabIndex = 133;
            this.btnTraCuu.Text = " Tra cứu";
            this.btnTraCuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTraCuu.UseVisualStyleBackColor = true;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.ForeColor = System.Drawing.Color.Green;
            this.btnXuatExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatExcel.Image")));
            this.btnXuatExcel.Location = new System.Drawing.Point(265, 84);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(100, 31);
            this.btnXuatExcel.TabIndex = 134;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // grcTraCuu
            // 
            this.grcTraCuu.Location = new System.Drawing.Point(15, 223);
            this.grcTraCuu.MainView = this.grvTraCuu;
            this.grcTraCuu.Name = "grcTraCuu";
            this.grcTraCuu.Size = new System.Drawing.Size(373, 203);
            this.grcTraCuu.TabIndex = 1;
            this.grcTraCuu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTraCuu});
            // 
            // grvTraCuu
            // 
            this.grvTraCuu.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvTraCuu.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvTraCuu.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvTraCuu.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvTraCuu.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grvTraCuu.Appearance.Row.Options.UseFont = true;
            this.grvTraCuu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenThuoc,
            this.colDonGia,
            this.colSoLuong});
            this.grvTraCuu.GridControl = this.grcTraCuu;
            this.grvTraCuu.Name = "grvTraCuu";
            this.grvTraCuu.OptionsView.ColumnAutoWidth = false;
            this.grvTraCuu.OptionsView.ShowFooter = true;
            this.grvTraCuu.OptionsView.ShowGroupPanel = false;
            this.grvTraCuu.OptionsView.ShowViewCaption = true;
            this.grvTraCuu.ViewCaption = "Kết quả tìm kiếm thuốc tồn kho";
            this.grvTraCuu.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvTraCuu_FocusedRowChanged);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 37;
            // 
            // colTenThuoc
            // 
            this.colTenThuoc.Caption = "Tên thuốc";
            this.colTenThuoc.FieldName = "TenDV";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.Visible = true;
            this.colTenThuoc.VisibleIndex = 1;
            this.colTenThuoc.Width = 178;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 57;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 3;
            this.colSoLuong.Width = 58;
            // 
            // dtTimTuNgay
            // 
            this.dtTimTuNgay.EditValue = null;
            this.dtTimTuNgay.EnterMoveNextControl = true;
            this.dtTimTuNgay.Location = new System.Drawing.Point(83, 50);
            this.dtTimTuNgay.Name = "dtTimTuNgay";
            this.dtTimTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtTimTuNgay.Properties.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtTimTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTimTuNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtTimTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtTimTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtTimTuNgay.Size = new System.Drawing.Size(100, 20);
            this.dtTimTuNgay.TabIndex = 136;
            // 
            // dtTimDenNgay
            // 
            this.dtTimDenNgay.EditValue = null;
            this.dtTimDenNgay.EnterMoveNextControl = true;
            this.dtTimDenNgay.Location = new System.Drawing.Point(257, 50);
            this.dtTimDenNgay.Name = "dtTimDenNgay";
            this.dtTimDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtTimDenNgay.Properties.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.dtTimDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTimDenNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtTimDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtTimDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtTimDenNgay.Size = new System.Drawing.Size(100, 20);
            this.dtTimDenNgay.TabIndex = 137;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Firebrick;
            this.label6.Location = new System.Drawing.Point(347, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(412, 31);
            this.label6.TabIndex = 138;
            this.label6.Text = "Tra cứu thuốc tồn tại kho và khoa";
            // 
            // grcChiTietThuoc
            // 
            this.grcChiTietThuoc.Location = new System.Drawing.Point(394, 223);
            this.grcChiTietThuoc.MainView = this.grvChiTietThuoc;
            this.grcChiTietThuoc.Name = "grcChiTietThuoc";
            this.grcChiTietThuoc.Size = new System.Drawing.Size(747, 203);
            this.grcChiTietThuoc.TabIndex = 140;
            this.grcChiTietThuoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiTietThuoc});
            // 
            // grvChiTietThuoc
            // 
            this.grvChiTietThuoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvChiTietThuoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvChiTietThuoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvChiTietThuoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvChiTietThuoc.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grvChiTietThuoc.Appearance.Row.Options.UseFont = true;
            this.grvChiTietThuoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDDon,
            this.ngayKe,
            this.colMaKP,
            this.colBN,
            this.colMaKX,
            this.colBoPhanKe,
            this.colSoPL,
            this.donGia,
            this.soLuong});
            this.grvChiTietThuoc.GridControl = this.grcChiTietThuoc;
            this.grvChiTietThuoc.Name = "grvChiTietThuoc";
            this.grvChiTietThuoc.OptionsView.ColumnAutoWidth = false;
            this.grvChiTietThuoc.OptionsView.ShowFooter = true;
            this.grvChiTietThuoc.OptionsView.ShowGroupPanel = false;
            this.grvChiTietThuoc.OptionsView.ShowViewCaption = true;
            this.grvChiTietThuoc.ViewCaption = "Chi tiết thuốc";
            // 
            // colIDDon
            // 
            this.colIDDon.Caption = "Id đơn";
            this.colIDDon.FieldName = "IDDon";
            this.colIDDon.Name = "colIDDon";
            this.colIDDon.Visible = true;
            this.colIDDon.VisibleIndex = 0;
            this.colIDDon.Width = 44;
            // 
            // ngayKe
            // 
            this.ngayKe.Caption = "Ngày Kê";
            this.ngayKe.FieldName = "NgayKe";
            this.ngayKe.Name = "ngayKe";
            this.ngayKe.Visible = true;
            this.ngayKe.VisibleIndex = 3;
            this.ngayKe.Width = 90;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "Mã khoa phòng";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            this.colMaKP.Visible = true;
            this.colMaKP.VisibleIndex = 1;
            this.colMaKP.Width = 94;
            // 
            // colBN
            // 
            this.colBN.Caption = "Bệnh nhân";
            this.colBN.FieldName = "TenBN";
            this.colBN.Name = "colBN";
            this.colBN.OptionsColumn.AllowEdit = false;
            this.colBN.OptionsColumn.ReadOnly = true;
            this.colBN.Visible = true;
            this.colBN.VisibleIndex = 7;
            this.colBN.Width = 101;
            // 
            // colMaKX
            // 
            this.colMaKX.Caption = "Mã kho xuất";
            this.colMaKX.FieldName = "MaKXuat";
            this.colMaKX.Name = "colMaKX";
            this.colMaKX.OptionsColumn.AllowEdit = false;
            this.colMaKX.OptionsColumn.ReadOnly = true;
            this.colMaKX.Visible = true;
            this.colMaKX.VisibleIndex = 2;
            this.colMaKX.Width = 80;
            // 
            // colBoPhanKe
            // 
            this.colBoPhanKe.Caption = "Bộ phận kê";
            this.colBoPhanKe.FieldName = "TenKP";
            this.colBoPhanKe.Name = "colBoPhanKe";
            this.colBoPhanKe.Visible = true;
            this.colBoPhanKe.VisibleIndex = 4;
            this.colBoPhanKe.Width = 95;
            // 
            // colSoPL
            // 
            this.colSoPL.Caption = "Số phiếu lĩnh";
            this.colSoPL.FieldName = "SoPL";
            this.colSoPL.Name = "colSoPL";
            this.colSoPL.Visible = true;
            this.colSoPL.VisibleIndex = 8;
            this.colSoPL.Width = 79;
            // 
            // donGia
            // 
            this.donGia.Caption = "Đơn giá";
            this.donGia.FieldName = "DonGia";
            this.donGia.Name = "donGia";
            this.donGia.Visible = true;
            this.donGia.VisibleIndex = 5;
            this.donGia.Width = 59;
            // 
            // soLuong
            // 
            this.soLuong.Caption = "Số lượng";
            this.soLuong.FieldName = "SoLuong";
            this.soLuong.Name = "soLuong";
            this.soLuong.Visible = true;
            this.soLuong.VisibleIndex = 6;
            this.soLuong.Width = 59;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.btnIn);
            this.groupControl1.Controls.Add(this.txtMaDV);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.dtTimDenNgay);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.btnXuatExcel);
            this.groupControl1.Controls.Add(this.btnTraCuu);
            this.groupControl1.Controls.Add(this.dtTimTuNgay);
            this.groupControl1.Location = new System.Drawing.Point(752, 75);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(379, 124);
            this.groupControl1.TabIndex = 141;
            this.groupControl1.Text = "Tra cứu";
            // 
            // btnIn
            // 
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.ForeColor = System.Drawing.Color.Purple;
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(140, 84);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(107, 31);
            this.btnIn.TabIndex = 138;
            this.btnIn.Text = "In báo cáo";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // grcKhoa
            // 
            this.grcKhoa.Location = new System.Drawing.Point(402, 77);
            this.grcKhoa.MainView = this.grvKhoaPhong;
            this.grcKhoa.Name = "grcKhoa";
            this.grcKhoa.Size = new System.Drawing.Size(329, 122);
            this.grcKhoa.TabIndex = 0;
            this.grcKhoa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong,
            this.gridView1});
            // 
            // grvKhoaPhong
            // 
            this.grvKhoaPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.chonKhoaPhong,
            this.tenKhoaPhong,
            this.maKhoaPhong});
            this.grvKhoaPhong.GridControl = this.grcKhoa;
            this.grvKhoaPhong.Name = "grvKhoaPhong";
            this.grvKhoaPhong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaPhong.OptionsView.ShowViewCaption = true;
            this.grvKhoaPhong.ViewCaption = "Khoa/Phòng";
            this.grvKhoaPhong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaPhong_CellValueChanging);
            // 
            // chonKhoaPhong
            // 
            this.chonKhoaPhong.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.chonKhoaPhong.AppearanceHeader.Options.UseFont = true;
            this.chonKhoaPhong.Caption = "Chọn";
            this.chonKhoaPhong.FieldName = "Chon";
            this.chonKhoaPhong.Name = "chonKhoaPhong";
            this.chonKhoaPhong.Visible = true;
            this.chonKhoaPhong.VisibleIndex = 0;
            this.chonKhoaPhong.Width = 28;
            // 
            // tenKhoaPhong
            // 
            this.tenKhoaPhong.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.tenKhoaPhong.AppearanceHeader.Options.UseFont = true;
            this.tenKhoaPhong.Caption = "Tên Khoa Phòng";
            this.tenKhoaPhong.FieldName = "TenKP";
            this.tenKhoaPhong.Name = "tenKhoaPhong";
            this.tenKhoaPhong.Visible = true;
            this.tenKhoaPhong.VisibleIndex = 1;
            this.tenKhoaPhong.Width = 250;
            // 
            // maKhoaPhong
            // 
            this.maKhoaPhong.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.maKhoaPhong.AppearanceHeader.Options.UseFont = true;
            this.maKhoaPhong.Caption = "Mã";
            this.maKhoaPhong.FieldName = "MaKP";
            this.maKhoaPhong.Name = "maKhoaPhong";
            this.maKhoaPhong.Visible = true;
            this.maKhoaPhong.VisibleIndex = 2;
            this.maKhoaPhong.Width = 31;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grcKhoa;
            this.gridView1.Name = "gridView1";
            // 
            // TCThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 446);
            this.Controls.Add(this.grcKhoa);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grcTraCuu);
            this.Controls.Add(this.grcChiTietThuoc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TCThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCThuoc";
            this.Load += new System.EventHandler(this.TCThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoDuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTraCuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTraCuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTietThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcKhoDuoc;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKho;
        private DevExpress.XtraGrid.Columns.GridColumn chonKho;
        private DevExpress.XtraGrid.Columns.GridColumn tenKho;
        private DevExpress.XtraGrid.Columns.GridColumn maKho;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtMaDV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Button btnXuatExcel;
        private DevExpress.XtraGrid.GridControl grcTraCuu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTraCuu;
        private DevExpress.XtraGrid.Columns.GridColumn colTenThuoc;
        private DevExpress.XtraEditors.DateEdit dtTimTuNgay;
        private DevExpress.XtraEditors.DateEdit dtTimDenNgay;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl grcChiTietThuoc;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiTietThuoc;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDon;
        private DevExpress.XtraGrid.Columns.GridColumn colBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKX;
        private DevExpress.XtraGrid.Columns.GridColumn colSoPL;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraGrid.Columns.GridColumn ngayKe;
        private DevExpress.XtraGrid.Columns.GridColumn donGia;
        private DevExpress.XtraGrid.Columns.GridColumn soLuong;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoa;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn chonKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn tenKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn maKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colBoPhanKe;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private System.Windows.Forms.Button btnIn;
    }
}