namespace QLBV.FormNhap
{
    partial class frmDienBienBenh
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grcDienBien = new DevExpress.XtraGrid.GridControl();
            this.grvDienBien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNgayNhapdb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colDienBien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colYlenh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colMaBNhandb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXoadb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colIDdb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTHYL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colMaCB_db = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaCB_dienBien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colHuongDtri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnLayKQTT = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dedenngaydb = new DevExpress.XtraEditors.DateEdit();
            this.detungaydb = new DevExpress.XtraEditors.DateEdit();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.btngetKQCLS = new DevExpress.XtraEditors.SimpleButton();
            this.cboInToDieuTri = new DevExpress.XtraEditors.ComboBoxEdit();
            this.binsDienBien = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDienBien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDienBien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB_dienBien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngaydb.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngaydb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungaydb.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungaydb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInToDieuTri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binsDienBien)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcDienBien);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1040, 499);
            this.panelControl2.TabIndex = 3;
            // 
            // grcDienBien
            // 
            this.grcDienBien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDienBien.Location = new System.Drawing.Point(2, 2);
            this.grcDienBien.MainView = this.grvDienBien;
            this.grcDienBien.Name = "grcDienBien";
            this.grcDienBien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit2,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemHyperLinkEdit2,
            this.repositoryItemMemoEdit3,
            this.lupMaCB_dienBien,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit4});
            this.grcDienBien.Size = new System.Drawing.Size(1036, 458);
            this.grcDienBien.TabIndex = 1;
            this.grcDienBien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDienBien});
            // 
            // grvDienBien
            // 
            this.grvDienBien.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDienBien.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDienBien.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDienBien.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDienBien.Appearance.OddRow.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDienBien.Appearance.Row.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDienBien.Appearance.RowSeparator.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.RowSeparator.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDienBien.Appearance.VertLine.Options.UseTextOptions = true;
            this.grvDienBien.Appearance.VertLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDienBien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNgayNhapdb,
            this.colDienBien,
            this.colYlenh,
            this.colMaBNhandb,
            this.colXoadb,
            this.colIDdb,
            this.colTHYL,
            this.colMaCB_db,
            this.colHuongDtri});
            this.grvDienBien.DetailHeight = 900;
            this.grvDienBien.GridControl = this.grcDienBien;
            this.grvDienBien.IndicatorWidth = 4;
            this.grvDienBien.Name = "grvDienBien";
            this.grvDienBien.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvDienBien.OptionsBehavior.Editable = false;
            this.grvDienBien.OptionsNavigation.AutoFocusNewRow = true;
            this.grvDienBien.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvDienBien.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDienBien.OptionsView.EnableAppearanceOddRow = true;
            this.grvDienBien.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvDienBien.OptionsView.RowAutoHeight = true;
            this.grvDienBien.OptionsView.ShowGroupPanel = false;
            this.grvDienBien.OptionsView.ShowViewCaption = true;
            this.grvDienBien.RowHeight = 50;
            this.grvDienBien.ViewCaption = "Diễn biến bệnh";
            this.grvDienBien.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvDienBien_RowCellClick);
            // 
            // colNgayNhapdb
            // 
            this.colNgayNhapdb.Caption = "Ngày giờ";
            this.colNgayNhapdb.ColumnEdit = this.repositoryItemDateEdit2;
            this.colNgayNhapdb.DisplayFormat.FormatString = "g";
            this.colNgayNhapdb.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayNhapdb.FieldName = "NgayNhap";
            this.colNgayNhapdb.Name = "colNgayNhapdb";
            this.colNgayNhapdb.Visible = true;
            this.colNgayNhapdb.VisibleIndex = 0;
            this.colNgayNhapdb.Width = 117;
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.DisplayFormat.FormatString = "g";
            this.repositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.EditFormat.FormatString = "g";
            this.repositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.Mask.EditMask = "g";
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // colDienBien
            // 
            this.colDienBien.AppearanceCell.Options.UseTextOptions = true;
            this.colDienBien.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colDienBien.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDienBien.Caption = "Diễn biến bệnh";
            this.colDienBien.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colDienBien.FieldName = "DienBien1";
            this.colDienBien.Name = "colDienBien";
            this.colDienBien.Visible = true;
            this.colDienBien.VisibleIndex = 1;
            this.colDienBien.Width = 223;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colYlenh
            // 
            this.colYlenh.AppearanceCell.Options.UseTextOptions = true;
            this.colYlenh.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colYlenh.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colYlenh.Caption = "Y lệnh";
            this.colYlenh.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colYlenh.FieldName = "YLenh";
            this.colYlenh.Name = "colYlenh";
            this.colYlenh.Visible = true;
            this.colYlenh.VisibleIndex = 2;
            this.colYlenh.Width = 218;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colMaBNhandb
            // 
            this.colMaBNhandb.Caption = "maBnhan";
            this.colMaBNhandb.FieldName = "MaBNhan";
            this.colMaBNhandb.Name = "colMaBNhandb";
            // 
            // colXoadb
            // 
            this.colXoadb.Caption = "Xóa CT";
            this.colXoadb.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.colXoadb.Name = "colXoadb";
            this.colXoadb.OptionsColumn.AllowFocus = false;
            this.colXoadb.OptionsColumn.ReadOnly = true;
            this.colXoadb.Visible = true;
            this.colXoadb.VisibleIndex = 5;
            this.colXoadb.Width = 108;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AllowFocused = false;
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            this.repositoryItemHyperLinkEdit2.NullText = "Xóa CT";
            this.repositoryItemHyperLinkEdit2.ReadOnly = true;
            // 
            // colIDdb
            // 
            this.colIDdb.Caption = "gridColumn1";
            this.colIDdb.FieldName = "ID";
            this.colIDdb.Name = "colIDdb";
            // 
            // colTHYL
            // 
            this.colTHYL.Caption = "Thực hiện Y lệnh";
            this.colTHYL.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colTHYL.FieldName = "ThucHienYL";
            this.colTHYL.Name = "colTHYL";
            this.colTHYL.Width = 166;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colMaCB_db
            // 
            this.colMaCB_db.Caption = "Người ra y lệnh";
            this.colMaCB_db.ColumnEdit = this.lupMaCB_dienBien;
            this.colMaCB_db.FieldName = "MaCB";
            this.colMaCB_db.Name = "colMaCB_db";
            this.colMaCB_db.Visible = true;
            this.colMaCB_db.VisibleIndex = 4;
            this.colMaCB_db.Width = 139;
            // 
            // lupMaCB_dienBien
            // 
            this.lupMaCB_dienBien.AutoHeight = false;
            this.lupMaCB_dienBien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaCB_dienBien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 200, "Tên CB"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCCHN", 100, "CCHN")});
            this.lupMaCB_dienBien.DisplayMember = "TenCB";
            this.lupMaCB_dienBien.Name = "lupMaCB_dienBien";
            this.lupMaCB_dienBien.NullText = "Chọn BS ra y lệnh";
            this.lupMaCB_dienBien.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lupMaCB_dienBien.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupMaCB_dienBien.ValueMember = "MaCB";
            // 
            // colHuongDtri
            // 
            this.colHuongDtri.AppearanceCell.Options.UseTextOptions = true;
            this.colHuongDtri.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colHuongDtri.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colHuongDtri.Caption = "Chế độ chăm sóc";
            this.colHuongDtri.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colHuongDtri.FieldName = "HuongDTri";
            this.colHuongDtri.Name = "colHuongDtri";
            this.colHuongDtri.Visible = true;
            this.colHuongDtri.VisibleIndex = 3;
            this.colHuongDtri.Width = 213;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.simpleButton3);
            this.panelControl3.Controls.Add(this.simpleButton5);
            this.panelControl3.Controls.Add(this.simpleButton2);
            this.panelControl3.Controls.Add(this.btnLayKQTT);
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Controls.Add(this.dedenngaydb);
            this.panelControl3.Controls.Add(this.detungaydb);
            this.panelControl3.Controls.Add(this.labelControl28);
            this.panelControl3.Controls.Add(this.labelControl13);
            this.panelControl3.Controls.Add(this.btngetKQCLS);
            this.panelControl3.Controls.Add(this.cboInToDieuTri);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 460);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1036, 37);
            this.panelControl3.TabIndex = 2;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(792, 7);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(63, 23);
            this.simpleButton3.TabIndex = 16;
            this.simpleButton3.Text = "Sửa";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(918, 7);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(63, 23);
            this.simpleButton5.TabIndex = 15;
            this.simpleButton5.Text = "K.Lưu";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(855, 7);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(63, 23);
            this.simpleButton2.TabIndex = 14;
            this.simpleButton2.Text = "Lưu";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnLayKQTT
            // 
            this.btnLayKQTT.Location = new System.Drawing.Point(183, 7);
            this.btnLayKQTT.Name = "btnLayKQTT";
            this.btnLayKQTT.Size = new System.Drawing.Size(75, 23);
            this.btnLayKQTT.TabIndex = 11;
            this.btnLayKQTT.Text = "Lấy KQ TT-PT";
            this.btnLayKQTT.Click += new System.EventHandler(this.btnLayKQTT_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(88, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 23);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "Lấy KQ CĐHA";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // dedenngaydb
            // 
            this.dedenngaydb.EditValue = null;
            this.dedenngaydb.Location = new System.Drawing.Point(470, 9);
            this.dedenngaydb.Name = "dedenngaydb";
            this.dedenngaydb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngaydb.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngaydb.Size = new System.Drawing.Size(100, 20);
            this.dedenngaydb.TabIndex = 9;
            this.dedenngaydb.EditValueChanged += new System.EventHandler(this.dedenngaydb_EditValueChanged);
            // 
            // detungaydb
            // 
            this.detungaydb.EditValue = null;
            this.detungaydb.Location = new System.Drawing.Point(311, 9);
            this.detungaydb.Name = "detungaydb";
            this.detungaydb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungaydb.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungaydb.Size = new System.Drawing.Size(100, 20);
            this.detungaydb.TabIndex = 9;
            this.detungaydb.EditValueChanged += new System.EventHandler(this.detungaydb_EditValueChanged);
            // 
            // labelControl28
            // 
            this.labelControl28.Location = new System.Drawing.Point(414, 12);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(51, 13);
            this.labelControl28.TabIndex = 8;
            this.labelControl28.Text = "Đến ngày:";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(265, 12);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(44, 13);
            this.labelControl13.TabIndex = 8;
            this.labelControl13.Text = "Từ ngày:";
            // 
            // btngetKQCLS
            // 
            this.btngetKQCLS.Location = new System.Drawing.Point(3, 7);
            this.btngetKQCLS.Name = "btngetKQCLS";
            this.btngetKQCLS.Size = new System.Drawing.Size(79, 23);
            this.btngetKQCLS.TabIndex = 7;
            this.btngetKQCLS.Text = "Lấy KQ XN";
            this.btngetKQCLS.Click += new System.EventHandler(this.btngetKQCLS_Click);
            // 
            // cboInToDieuTri
            // 
            this.cboInToDieuTri.Location = new System.Drawing.Point(576, 8);
            this.cboInToDieuTri.Name = "cboInToDieuTri";
            this.cboInToDieuTri.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboInToDieuTri.Properties.Appearance.Options.UseFont = true;
            this.cboInToDieuTri.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboInToDieuTri.Properties.Items.AddRange(new object[] {
            "Tờ điều trị (MS: 39/BV-01)",
            "Phiếu điều trị(mẫu YHCT)"});
            this.cboInToDieuTri.Properties.NullText = "Chọn mẫu tờ điều trị";
            this.cboInToDieuTri.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboInToDieuTri.Size = new System.Drawing.Size(139, 22);
            this.cboInToDieuTri.TabIndex = 6;
            this.cboInToDieuTri.SelectedIndexChanged += new System.EventHandler(this.cboInToDieuTri_SelectedIndexChanged);
            // 
            // frmDienBienBenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 499);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDienBienBenh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diễn Biến Bệnh";
            this.Load += new System.EventHandler(this.frmDienBienBenh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDienBien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDienBien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB_dienBien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngaydb.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngaydb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungaydb.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungaydb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInToDieuTri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binsDienBien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grcDienBien;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDienBien;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhapdb;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colDienBien;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colYlenh;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBNhandb;
        private DevExpress.XtraGrid.Columns.GridColumn colXoadb;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colIDdb;
        private DevExpress.XtraGrid.Columns.GridColumn colTHYL;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCB_db;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaCB_dienBien;
        private DevExpress.XtraGrid.Columns.GridColumn colHuongDtri;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnLayKQTT;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.DateEdit dedenngaydb;
        private DevExpress.XtraEditors.DateEdit detungaydb;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.SimpleButton btngetKQCLS;
        private DevExpress.XtraEditors.ComboBoxEdit cboInToDieuTri;
        private System.Windows.Forms.BindingSource binsDienBien;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
    }
}