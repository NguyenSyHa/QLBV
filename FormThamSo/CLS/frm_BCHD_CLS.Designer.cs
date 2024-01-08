namespace QLBV.FormThamSo
{
    partial class frm_BCHD_CLS
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboDTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupngayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.grcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckGrvKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colmaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.radNoiTru = new DevExpress.XtraEditors.RadioGroup();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cbo_NgayTT = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ckHienThiXNKhoa = new DevExpress.XtraEditors.CheckEdit();
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cboTrongDM = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ckc_theoluot = new DevExpress.XtraEditors.CheckEdit();
            this.lupNgaytu1 = new DevExpress.XtraEditors.DateEdit();
            this.lupngayden1 = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NgayTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckHienThiXNKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckc_theoluot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(401, 44);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 15);
            this.labelControl5.TabIndex = 24;
            this.labelControl5.Text = "Đối Tượng:";
            // 
            // cboDTuong
            // 
            this.cboDTuong.EditValue = "Cả hai";
            this.cboDTuong.Location = new System.Drawing.Point(490, 41);
            this.cboDTuong.Name = "cboDTuong";
            this.cboDTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDTuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ",
            "Cả hai"});
            this.cboDTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDTuong.Size = new System.Drawing.Size(228, 22);
            this.cboDTuong.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(151, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 22);
            this.label1.TabIndex = 19;
            this.label1.Text = "BÁO CÁO HOẠT ĐỘNG CẬN LÂM SÀNG";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 15);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // lupngayden
            // 
            this.lupngayden.EditValue = null;
            this.lupngayden.EnterMoveNextControl = true;
            this.lupngayden.Location = new System.Drawing.Point(270, 42);
            this.lupngayden.Name = "lupngayden";
            this.lupngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden.Properties.Appearance.Options.UseFont = true;
            this.lupngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Size = new System.Drawing.Size(103, 22);
            this.lupngayden.TabIndex = 1;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.EnterMoveNextControl = true;
            this.lupNgaytu.Location = new System.Drawing.Point(110, 41);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(105, 22);
            this.lupNgaytu.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(231, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 15);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "đến:";
            // 
            // grcKhoaPhong
            // 
            this.grcKhoaPhong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grcKhoaPhong.Location = new System.Drawing.Point(110, 136);
            this.grcKhoaPhong.MainView = this.grvKhoaPhong;
            this.grcKhoaPhong.Name = "grcKhoaPhong";
            this.grcKhoaPhong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaPhong.Size = new System.Drawing.Size(263, 248);
            this.grcKhoaPhong.TabIndex = 4;
            this.grcKhoaPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong});
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
            this.grvKhoaPhong.OptionsView.ColumnAutoWidth = false;
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
            this.colCheckGrvKP.Width = 40;
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
            this.colTenKP.Width = 221;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.labelControl4.Location = new System.Drawing.Point(401, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 15);
            this.labelControl4.TabIndex = 53;
            this.labelControl4.Text = "Nội|Ngoại trú:";
            // 
            // radNoiTru
            // 
            this.radNoiTru.EditValue = ((short)(2));
            this.radNoiTru.Location = new System.Drawing.Point(490, 69);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNoiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Cả hai")});
            this.radNoiTru.Size = new System.Drawing.Size(228, 28);
            this.radNoiTru.TabIndex = 3;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Location = new System.Drawing.Point(611, 397);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(94, 27);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(506, 397);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&In Báo cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(12, 73);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(57, 15);
            this.labelControl6.TabIndex = 55;
            this.labelControl6.Text = "Tìm kiếm:";
            // 
            // cbo_NgayTT
            // 
            this.cbo_NgayTT.EditValue = "Tất cả Bệnh Nhân đã thực hiện(theo ngày thực hiện)";
            this.cbo_NgayTT.Location = new System.Drawing.Point(110, 71);
            this.cbo_NgayTT.Name = "cbo_NgayTT";
            this.cbo_NgayTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_NgayTT.Properties.Appearance.Options.UseFont = true;
            this.cbo_NgayTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_NgayTT.Properties.Items.AddRange(new object[] {
            "Tất cả Bệnh Nhân đã thực hiện(theo ngày thực hiện)",
            "Tất cả Bệnh Nhân đã thanh toán(theo ngày thanh toán)"});
            this.cbo_NgayTT.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_NgayTT.Size = new System.Drawing.Size(263, 22);
            this.cbo_NgayTT.TabIndex = 54;
            // 
            // ckHienThiXNKhoa
            // 
            this.ckHienThiXNKhoa.Location = new System.Drawing.Point(490, 109);
            this.ckHienThiXNKhoa.Name = "ckHienThiXNKhoa";
            this.ckHienThiXNKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckHienThiXNKhoa.Properties.Appearance.Options.UseFont = true;
            this.ckHienThiXNKhoa.Properties.Caption = "Hiển thị XN làm tại khoa";
            this.ckHienThiXNKhoa.Size = new System.Drawing.Size(228, 20);
            this.ckHienThiXNKhoa.TabIndex = 56;
            this.ckHienThiXNKhoa.CheckedChanged += new System.EventHandler(this.ckHienThiXNKhoa_CheckedChanged);
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.Enabled = false;
            this.cklKP.Location = new System.Drawing.Point(490, 135);
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(228, 248);
            this.cklKP.TabIndex = 59;
            // 
            // cboTrongDM
            // 
            this.cboTrongDM.Location = new System.Drawing.Point(110, 95);
            this.cboTrongDM.Name = "cboTrongDM";
            this.cboTrongDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTrongDM.Properties.Appearance.Options.UseFont = true;
            this.cboTrongDM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrongDM.Properties.Items.AddRange(new object[] {
            "Ngoài DM BH",
            "Trong DM BH",
            "Tất cả"});
            this.cboTrongDM.Size = new System.Drawing.Size(263, 22);
            this.cboTrongDM.TabIndex = 60;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(12, 98);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 15);
            this.labelControl3.TabIndex = 61;
            this.labelControl3.Text = "Trong|Ngoài DM:";
            // 
            // ckc_theoluot
            // 
            this.ckc_theoluot.Location = new System.Drawing.Point(110, 402);
            this.ckc_theoluot.Name = "ckc_theoluot";
            this.ckc_theoluot.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckc_theoluot.Properties.Appearance.Options.UseFont = true;
            this.ckc_theoluot.Properties.Caption = "Hiển thị tổng số lần";
            this.ckc_theoluot.Size = new System.Drawing.Size(263, 20);
            this.ckc_theoluot.TabIndex = 56;
            this.ckc_theoluot.CheckedChanged += new System.EventHandler(this.ckHienThiXNKhoa_CheckedChanged);
            // 
            // lupNgaytu1
            // 
            this.lupNgaytu1.EditValue = null;
            this.lupNgaytu1.EnterMoveNextControl = true;
            this.lupNgaytu1.Location = new System.Drawing.Point(110, 41);
            this.lupNgaytu1.Name = "lupNgaytu1";
            this.lupNgaytu1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu1.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu1.Properties.DisplayFormat.FormatString = "g";
            this.lupNgaytu1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupNgaytu1.Properties.EditFormat.FormatString = "g";
            this.lupNgaytu1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupNgaytu1.Properties.Mask.EditMask = "g";
            this.lupNgaytu1.Size = new System.Drawing.Size(116, 22);
            this.lupNgaytu1.TabIndex = 62;
            this.lupNgaytu1.Visible = false;
            // 
            // lupngayden1
            // 
            this.lupngayden1.EditValue = null;
            this.lupngayden1.EnterMoveNextControl = true;
            this.lupngayden1.Location = new System.Drawing.Point(261, 42);
            this.lupngayden1.Name = "lupngayden1";
            this.lupngayden1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden1.Properties.Appearance.Options.UseFont = true;
            this.lupngayden1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden1.Properties.DisplayFormat.FormatString = "g";
            this.lupngayden1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupngayden1.Properties.EditFormat.FormatString = "g";
            this.lupngayden1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupngayden1.Properties.Mask.EditMask = "g";
            this.lupngayden1.Size = new System.Drawing.Size(112, 22);
            this.lupngayden1.TabIndex = 63;
            this.lupngayden1.Visible = false;
            // 
            // frm_BCHD_CLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 432);
            this.Controls.Add(this.lupngayden1);
            this.Controls.Add(this.lupNgaytu1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboTrongDM);
            this.Controls.Add(this.cklKP);
            this.Controls.Add(this.ckc_theoluot);
            this.Controls.Add(this.ckHienThiXNKhoa);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cbo_NgayTT);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.radNoiTru);
            this.Controls.Add(this.grcKhoaPhong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.cboDTuong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupngayden);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.labelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCHD_CLS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo hoạt động cận lâm sàng";
            this.Load += new System.EventHandler(this.frm_BCHD_CLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NgayTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckHienThiXNKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckc_theoluot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cboDTuong;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit lupngayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl grcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckGrvKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colmaKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup radNoiTru;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_NgayTT;
        private DevExpress.XtraEditors.CheckEdit ckHienThiXNKhoa;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.ComboBoxEdit cboTrongDM;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckc_theoluot;
        private DevExpress.XtraEditors.DateEdit lupNgaytu1;
        private DevExpress.XtraEditors.DateEdit lupngayden1;
    }
}