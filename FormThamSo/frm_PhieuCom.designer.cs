namespace QLBV.FormThamSo
{
    partial class frm_PhieuCom
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuCom));
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lup_Ngay = new DevExpress.XtraEditors.DateEdit();
            this.lup_khoa = new DevExpress.XtraEditors.LookUpEdit();
            this.cbb_DTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.grcBenhNhan = new DevExpress.XtraGrid.GridControl();
            this.grvBenhNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Check = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Check_BN = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.MaBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.celXuat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Suat = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.IDChamCom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhtien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.bnt_Thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_In = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Tha = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_luu = new DevExpress.XtraEditors.SimpleButton();
            this.txtDonGia = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Ngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Ngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_khoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_DTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Check_BN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Suat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl4.Appearance.Font")));
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // lup_Ngay
            // 
            resources.ApplyResources(this.lup_Ngay, "lup_Ngay");
            this.lup_Ngay.Name = "lup_Ngay";
            this.lup_Ngay.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lup_Ngay.Properties.Appearance.Font")));
            this.lup_Ngay.Properties.Appearance.Options.UseFont = true;
            this.lup_Ngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lup_Ngay.Properties.Buttons"))))});
            this.lup_Ngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lup_Ngay.Properties.CalendarTimeProperties.Buttons"))))});
            this.lup_Ngay.EditValueChanged += new System.EventHandler(this.lup_Ngay_EditValueChanged);
            // 
            // lup_khoa
            // 
            resources.ApplyResources(this.lup_khoa, "lup_khoa");
            this.lup_khoa.Name = "lup_khoa";
            this.lup_khoa.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lup_khoa.Properties.Appearance.Font")));
            this.lup_khoa.Properties.Appearance.Options.UseFont = true;
            this.lup_khoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lup_khoa.Properties.Buttons"))))});
            this.lup_khoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lup_khoa.Properties.Columns"), ((int)(resources.GetObject("lup_khoa.Properties.Columns1"))), resources.GetString("lup_khoa.Properties.Columns2")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lup_khoa.Properties.Columns3"), resources.GetString("lup_khoa.Properties.Columns4"))});
            this.lup_khoa.Properties.DisplayMember = "TenKP";
            this.lup_khoa.Properties.NullText = resources.GetString("lup_khoa.Properties.NullText");
            this.lup_khoa.Properties.ValueMember = "MaKP";
            this.lup_khoa.EditValueChanged += new System.EventHandler(this.lup_khoa_EditValueChanged);
            // 
            // cbb_DTuong
            // 
            resources.ApplyResources(this.cbb_DTuong, "cbb_DTuong");
            this.cbb_DTuong.Name = "cbb_DTuong";
            this.cbb_DTuong.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("cbb_DTuong.Properties.Appearance.Font")));
            this.cbb_DTuong.Properties.Appearance.Options.UseFont = true;
            this.cbb_DTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cbb_DTuong.Properties.Buttons"))))});
            this.cbb_DTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("cbb_DTuong.Properties.Columns"), resources.GetString("cbb_DTuong.Properties.Columns1"))});
            this.cbb_DTuong.Properties.DisplayMember = "MaDTuong";
            this.cbb_DTuong.Properties.NullText = resources.GetString("cbb_DTuong.Properties.NullText");
            this.cbb_DTuong.Properties.PopupSizeable = false;
            this.cbb_DTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbb_DTuong.Properties.ValueMember = "MaDTuong";
            this.cbb_DTuong.EditValueChanged += new System.EventHandler(this.cbb_DTuong_EditValueChanged);
            // 
            // grcBenhNhan
            // 
            resources.ApplyResources(this.grcBenhNhan, "grcBenhNhan");
            this.grcBenhNhan.MainView = this.grvBenhNhan;
            this.grcBenhNhan.Name = "grcBenhNhan";
            this.grcBenhNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Check_BN,
            this.Suat,
            this.repositoryItemCheckEdit1});
            this.grcBenhNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBenhNhan});
            // 
            // grvBenhNhan
            // 
            this.grvBenhNhan.Appearance.HeaderPanel.BackColor = ((System.Drawing.Color)(resources.GetObject("grvBenhNhan.Appearance.HeaderPanel.BackColor")));
            this.grvBenhNhan.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.grvBenhNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Check,
            this.MaBNhan,
            this.TenBNhan,
            this.DChi,
            this.celXuat,
            this.IDChamCom,
            this.colDonGia,
            this.colThanhtien,
            this.gridColumn1});
            this.grvBenhNhan.GridControl = this.grcBenhNhan;
            this.grvBenhNhan.Name = "grvBenhNhan";
            this.grvBenhNhan.OptionsView.ShowFooter = true;
            this.grvBenhNhan.OptionsView.ShowGroupPanel = false;
            this.grvBenhNhan.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvBenhNhan_RowStyle);
            this.grvBenhNhan.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvBenhNhan_CellValueChanged);
            this.grvBenhNhan.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvBenhNhan_CellValueChanging);
            this.grvBenhNhan.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.grvBenhNhan_RowUpdated);
            this.grvBenhNhan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvBenhNhan_MouseDown);
            this.grvBenhNhan.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvBenhNhan_ValidatingEditor);
            // 
            // Check
            // 
            this.Check.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("Check.AppearanceCell.Font")));
            this.Check.AppearanceCell.Options.UseFont = true;
            this.Check.AppearanceCell.Options.UseTextOptions = true;
            this.Check.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Check.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Check.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("Check.AppearanceHeader.Font")));
            this.Check.AppearanceHeader.Options.UseFont = true;
            this.Check.AppearanceHeader.Options.UseTextOptions = true;
            this.Check.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Check.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Check.ColumnEdit = this.Check_BN;
            this.Check.FieldName = "Chek";
            this.Check.Image = ((System.Drawing.Image)(resources.GetObject("Check.Image")));
            resources.ApplyResources(this.Check, "Check");
            this.Check.Name = "Check";
            this.Check.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.Check.OptionsColumn.ShowCaption = false;
            this.Check.OptionsFilter.AllowAutoFilter = false;
            this.Check.OptionsFilter.AllowFilter = false;
            this.Check.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.Check.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.Check.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.False;
            // 
            // Check_BN
            // 
            resources.ApplyResources(this.Check_BN, "Check_BN");
            this.Check_BN.Name = "Check_BN";
            // 
            // MaBNhan
            // 
            this.MaBNhan.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("MaBNhan.AppearanceCell.Font")));
            this.MaBNhan.AppearanceCell.Options.UseFont = true;
            this.MaBNhan.AppearanceCell.Options.UseTextOptions = true;
            this.MaBNhan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaBNhan.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MaBNhan.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("MaBNhan.AppearanceHeader.Font")));
            this.MaBNhan.AppearanceHeader.Options.UseFont = true;
            this.MaBNhan.AppearanceHeader.Options.UseTextOptions = true;
            this.MaBNhan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaBNhan.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.MaBNhan, "MaBNhan");
            this.MaBNhan.FieldName = "MaBNhan";
            this.MaBNhan.Name = "MaBNhan";
            this.MaBNhan.OptionsColumn.AllowEdit = false;
            this.MaBNhan.OptionsColumn.ReadOnly = true;
            // 
            // TenBNhan
            // 
            this.TenBNhan.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("TenBNhan.AppearanceCell.Font")));
            this.TenBNhan.AppearanceCell.Options.UseFont = true;
            this.TenBNhan.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("TenBNhan.AppearanceHeader.Font")));
            this.TenBNhan.AppearanceHeader.Options.UseFont = true;
            this.TenBNhan.AppearanceHeader.Options.UseTextOptions = true;
            this.TenBNhan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenBNhan.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.TenBNhan, "TenBNhan");
            this.TenBNhan.FieldName = "TenBNhan";
            this.TenBNhan.Name = "TenBNhan";
            this.TenBNhan.OptionsColumn.AllowEdit = false;
            this.TenBNhan.OptionsColumn.ReadOnly = true;
            // 
            // DChi
            // 
            this.DChi.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("DChi.AppearanceCell.Font")));
            this.DChi.AppearanceCell.Options.UseFont = true;
            this.DChi.AppearanceCell.Options.UseTextOptions = true;
            this.DChi.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.DChi.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("DChi.AppearanceHeader.Font")));
            this.DChi.AppearanceHeader.Options.UseFont = true;
            this.DChi.AppearanceHeader.Options.UseTextOptions = true;
            this.DChi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DChi.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.DChi, "DChi");
            this.DChi.FieldName = "DChi";
            this.DChi.Name = "DChi";
            this.DChi.OptionsColumn.AllowEdit = false;
            this.DChi.OptionsColumn.ReadOnly = true;
            // 
            // celXuat
            // 
            this.celXuat.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("celXuat.AppearanceCell.Font")));
            this.celXuat.AppearanceCell.Options.UseFont = true;
            this.celXuat.AppearanceCell.Options.UseTextOptions = true;
            this.celXuat.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.celXuat.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("celXuat.AppearanceHeader.Font")));
            this.celXuat.AppearanceHeader.Options.UseFont = true;
            this.celXuat.AppearanceHeader.Options.UseTextOptions = true;
            this.celXuat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.celXuat.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.celXuat, "celXuat");
            this.celXuat.ColumnEdit = this.Suat;
            this.celXuat.FieldName = "Xuat";
            this.celXuat.Name = "celXuat";
            // 
            // Suat
            // 
            resources.ApplyResources(this.Suat, "Suat");
            this.Suat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("Suat.Buttons"))))});
            this.Suat.Items.AddRange(new object[] {
            resources.GetString("Suat.Items"),
            resources.GetString("Suat.Items1")});
            this.Suat.Name = "Suat";
            this.Suat.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // IDChamCom
            // 
            resources.ApplyResources(this.IDChamCom, "IDChamCom");
            this.IDChamCom.FieldName = "IDChamCom";
            this.IDChamCom.Name = "IDChamCom";
            // 
            // colDonGia
            // 
            this.colDonGia.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colDonGia.AppearanceCell.Font")));
            this.colDonGia.AppearanceCell.Options.UseFont = true;
            this.colDonGia.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonGia.AppearanceHeader.Font")));
            this.colDonGia.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonGia, "colDonGia");
            this.colDonGia.DisplayFormat.FormatString = "{0:n2}";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            // 
            // colThanhtien
            // 
            this.colThanhtien.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colThanhtien.AppearanceCell.Font")));
            this.colThanhtien.AppearanceCell.Options.UseFont = true;
            this.colThanhtien.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colThanhtien.AppearanceHeader.Font")));
            this.colThanhtien.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colThanhtien, "colThanhtien");
            this.colThanhtien.DisplayFormat.FormatString = "{0:n2}";
            this.colThanhtien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhtien.FieldName = "ThanhTien";
            this.colThanhtien.Name = "colThanhtien";
            this.colThanhtien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colThanhtien.Summary"))), resources.GetString("colThanhtien.Summary1"), resources.GetString("colThanhtien.Summary2"))});
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "SoLuong";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // bnt_Thoat
            // 
            this.bnt_Thoat.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("bnt_Thoat.Appearance.Font")));
            this.bnt_Thoat.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.bnt_Thoat, "bnt_Thoat");
            this.bnt_Thoat.Name = "bnt_Thoat";
            this.bnt_Thoat.Click += new System.EventHandler(this.bnt_Thoat_Click);
            // 
            // btn_In
            // 
            this.btn_In.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btn_In.Appearance.Font")));
            this.btn_In.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.btn_In, "btn_In");
            this.btn_In.Name = "btn_In";
            this.btn_In.Click += new System.EventHandler(this.btn_In_Click);
            // 
            // radioGroup2
            // 
            resources.ApplyResources(this.radioGroup2, "radioGroup2");
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioGroup2.Properties.Items"))), resources.GetString("radioGroup2.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioGroup2.Properties.Items2"))), resources.GetString("radioGroup2.Properties.Items3"))});
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dateDenNgay
            // 
            resources.ApplyResources(this.dateDenNgay, "dateDenNgay");
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("dateDenNgay.Properties.Appearance.Font")));
            this.dateDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateDenNgay.Properties.Buttons"))))});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateDenNgay.Properties.CalendarTimeProperties.Buttons"))))});
            this.dateDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy ";
            this.dateDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy ";
            this.dateDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // radioGroup1
            // 
            resources.ApplyResources(this.radioGroup1, "radioGroup1");
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioGroup1.Properties.Items"))), resources.GetString("radioGroup1.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioGroup1.Properties.Items2"))), resources.GetString("radioGroup1.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioGroup1.Properties.Items4"))), resources.GetString("radioGroup1.Properties.Items5"))});
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl5.Appearance.Font")));
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // Tha
            // 
            this.Tha.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Tha.Appearance.Font")));
            resources.ApplyResources(this.Tha, "Tha");
            this.Tha.Name = "Tha";
            // 
            // comboBoxEdit1
            // 
            resources.ApplyResources(this.comboBoxEdit1, "comboBoxEdit1");
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("comboBoxEdit1.Properties.Appearance.Font")));
            this.comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEdit1.Properties.Buttons"))))});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            resources.GetString("comboBoxEdit1.Properties.Items"),
            resources.GetString("comboBoxEdit1.Properties.Items1"),
            resources.GetString("comboBoxEdit1.Properties.Items2"),
            resources.GetString("comboBoxEdit1.Properties.Items3"),
            resources.GetString("comboBoxEdit1.Properties.Items4"),
            resources.GetString("comboBoxEdit1.Properties.Items5"),
            resources.GetString("comboBoxEdit1.Properties.Items6"),
            resources.GetString("comboBoxEdit1.Properties.Items7"),
            resources.GetString("comboBoxEdit1.Properties.Items8"),
            resources.GetString("comboBoxEdit1.Properties.Items9"),
            resources.GetString("comboBoxEdit1.Properties.Items10"),
            resources.GetString("comboBoxEdit1.Properties.Items11")});
            // 
            // btn_luu
            // 
            this.btn_luu.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btn_luu.Appearance.Font")));
            this.btn_luu.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.btn_luu, "btn_luu");
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // txtDonGia
            // 
            resources.ApplyResources(this.txtDonGia, "txtDonGia");
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtDonGia.Properties.Appearance.Font")));
            this.txtDonGia.Properties.Appearance.Options.UseFont = true;
            this.txtDonGia.Properties.Mask.EditMask = resources.GetString("txtDonGia.Properties.Mask.EditMask");
            this.txtDonGia.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDonGia.Properties.Mask.MaskType")));
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-checked-checkbox-16.png");
            this.imageList1.Images.SetKeyName(1, "icons8-unchecked-checkbox-16.png");
            // 
            // frm_PhieuCom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_luu);
            this.Controls.Add(this.radioGroup2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.Tha);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.btn_In);
            this.Controls.Add(this.bnt_Thoat);
            this.Controls.Add(this.grcBenhNhan);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.lup_khoa);
            this.Controls.Add(this.lup_Ngay);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cbb_DTuong);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PhieuCom";
            this.Load += new System.EventHandler(this.frm_PhieuCom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Ngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Ngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_khoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_DTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Check_BN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Suat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit lup_Ngay;
        private DevExpress.XtraEditors.LookUpEdit lup_khoa;
        private DevExpress.XtraEditors.LookUpEdit cbb_DTuong;
        private DevExpress.XtraGrid.GridControl grcBenhNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBenhNhan;
        private DevExpress.XtraGrid.Columns.GridColumn Check;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit Check_BN;
        private DevExpress.XtraGrid.Columns.GridColumn MaBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn TenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn DChi;
        private DevExpress.XtraEditors.SimpleButton bnt_Thoat;
        private DevExpress.XtraEditors.SimpleButton btn_In;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl Tha;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn celXuat;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox Suat;
        private DevExpress.XtraEditors.SimpleButton btn_luu;
        private DevExpress.XtraGrid.Columns.GridColumn IDChamCom;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhtien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit txtDonGia;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.ImageList imageList1;

    }
}