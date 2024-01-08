namespace QLBV.BHYT
{
    partial class frm_DuyetBN
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
            this.grcThanhToan = new DevExpress.XtraGrid.GridControl();
            this.grvThanhToan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaDVtt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuyet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkDuyet = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTienBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienBH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTTDuyet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienChenh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienChenhBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.chkDuyetBN = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuyet = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtLydo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtChanDoan = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaICD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenBN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtNgayRa = new DevExpress.XtraEditors.DateEdit();
            this.binSDuyet = new System.Windows.Forms.BindingSource(this.components);
            this.colMaQD = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLydo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChanDoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayRa.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayRa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binSDuyet)).BeginInit();
            this.SuspendLayout();
            // 
            // grcThanhToan
            // 
            this.grcThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcThanhToan.Location = new System.Drawing.Point(2, 20);
            this.grcThanhToan.MainView = this.grvThanhToan;
            this.grcThanhToan.Name = "grcThanhToan";
            this.grcThanhToan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupMaDVtt,
            this.chkDuyet});
            this.grcThanhToan.Size = new System.Drawing.Size(920, 277);
            this.grcThanhToan.TabIndex = 58;
            this.grcThanhToan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThanhToan});
            // 
            // grvThanhToan
            // 
            this.grvThanhToan.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvThanhToan.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grvThanhToan.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvThanhToan.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvThanhToan.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvThanhToan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvThanhToan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colDonVi,
            this.colDonGia,
            this.colSoLuong,
            this.colThanhTien,
            this.colDuyet,
            this.colTienBN,
            this.colTienBH,
            this.colTTDuyet,
            this.colTienChenh,
            this.colSoLuongD,
            this.colTienChenhBN,
            this.colMaQD});
            this.grvThanhToan.GridControl = this.grcThanhToan;
            this.grvThanhToan.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "thanhtien", this.colThanhTien, "##,###"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "madv", null, "")});
            this.grvThanhToan.Name = "grvThanhToan";
            this.grvThanhToan.OptionsView.ShowFooter = true;
            this.grvThanhToan.OptionsView.ShowGroupPanel = false;
            this.grvThanhToan.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvThanhToan_CellValueChanged);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Tên chi phí";
            this.colMaDV.ColumnEdit = this.lupMaDVtt;
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.AllowFocus = false;
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 1;
            this.colMaDV.Width = 217;
            // 
            // lupMaDVtt
            // 
            this.lupMaDVtt.AllowFocused = false;
            this.lupMaDVtt.AutoHeight = false;
            this.lupMaDVtt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaDVtt.DisplayMember = "TenDV";
            this.lupMaDVtt.Name = "lupMaDVtt";
            this.lupMaDVtt.ReadOnly = true;
            this.lupMaDVtt.ValueMember = "MaDV";
            // 
            // colDonVi
            // 
            this.colDonVi.AppearanceCell.Options.UseTextOptions = true;
            this.colDonVi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDonVi.Caption = "Đơn vị";
            this.colDonVi.FieldName = "DonVi";
            this.colDonVi.Name = "colDonVi";
            this.colDonVi.OptionsColumn.AllowFocus = false;
            this.colDonVi.OptionsColumn.ReadOnly = true;
            this.colDonVi.Visible = true;
            this.colDonVi.VisibleIndex = 2;
            this.colDonVi.Width = 40;
            // 
            // colDonGia
            // 
            this.colDonGia.AppearanceCell.Options.UseTextOptions = true;
            this.colDonGia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "##,###";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowFocus = false;
            this.colDonGia.OptionsColumn.ReadOnly = true;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 3;
            this.colDonGia.Width = 48;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AppearanceCell.Options.UseTextOptions = true;
            this.colSoLuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.DisplayFormat.FormatString = "##,###";
            this.colSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.OptionsColumn.AllowFocus = false;
            this.colSoLuong.OptionsColumn.ReadOnly = true;
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 4;
            this.colSoLuong.Width = 53;
            // 
            // colThanhTien
            // 
            this.colThanhTien.AppearanceCell.Options.UseTextOptions = true;
            this.colThanhTien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colThanhTien.Caption = "thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "##,###";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.OptionsColumn.AllowFocus = false;
            this.colThanhTien.OptionsColumn.ReadOnly = true;
            this.colThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 5;
            this.colThanhTien.Width = 62;
            // 
            // colDuyet
            // 
            this.colDuyet.Caption = "Duyệt";
            this.colDuyet.ColumnEdit = this.chkDuyet;
            this.colDuyet.FieldName = "Duyet";
            this.colDuyet.Name = "colDuyet";
            this.colDuyet.Visible = true;
            this.colDuyet.VisibleIndex = 10;
            this.colDuyet.Width = 53;
            // 
            // chkDuyet
            // 
            this.chkDuyet.AutoHeight = false;
            this.chkDuyet.Caption = "Check";
            this.chkDuyet.Name = "chkDuyet";
            // 
            // colTienBN
            // 
            this.colTienBN.Caption = "Tiền BN";
            this.colTienBN.DisplayFormat.FormatString = "##,###";
            this.colTienBN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colTienBN.FieldName = "TienBN";
            this.colTienBN.Name = "colTienBN";
            this.colTienBN.OptionsColumn.AllowFocus = false;
            this.colTienBN.OptionsColumn.ReadOnly = true;
            this.colTienBN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colTienBN.Visible = true;
            this.colTienBN.VisibleIndex = 6;
            this.colTienBN.Width = 67;
            // 
            // colTienBH
            // 
            this.colTienBH.Caption = "Tiền BHYT";
            this.colTienBH.DisplayFormat.FormatString = "##,###";
            this.colTienBH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colTienBH.FieldName = "TienBH";
            this.colTienBH.Name = "colTienBH";
            this.colTienBH.OptionsColumn.AllowFocus = false;
            this.colTienBH.OptionsColumn.ReadOnly = true;
            this.colTienBH.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colTienBH.Visible = true;
            this.colTienBH.VisibleIndex = 7;
            this.colTienBH.Width = 61;
            // 
            // colTTDuyet
            // 
            this.colTTDuyet.Caption = "trạng thái";
            this.colTTDuyet.FieldName = "Duyet";
            this.colTTDuyet.Name = "colTTDuyet";
            // 
            // colTienChenh
            // 
            this.colTienChenh.Caption = "T.Tiền KD";
            this.colTienChenh.DisplayFormat.FormatString = "##,###";
            this.colTienChenh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colTienChenh.FieldName = "TienDuyet";
            this.colTienChenh.Name = "colTienChenh";
            this.colTienChenh.OptionsColumn.AllowFocus = false;
            this.colTienChenh.OptionsColumn.ReadOnly = true;
            this.colTienChenh.Visible = true;
            this.colTienChenh.VisibleIndex = 9;
            this.colTienChenh.Width = 64;
            // 
            // colSoLuongD
            // 
            this.colSoLuongD.Caption = "S.Lượng KD";
            this.colSoLuongD.FieldName = "SoLuongD";
            this.colSoLuongD.Name = "colSoLuongD";
            this.colSoLuongD.Visible = true;
            this.colSoLuongD.VisibleIndex = 8;
            this.colSoLuongD.Width = 65;
            // 
            // colTienChenhBN
            // 
            this.colTienChenhBN.Caption = "Tiền Chênh BN";
            this.colTienChenhBN.FieldName = "TienChenhBN";
            this.colTienChenhBN.Name = "colTienChenhBN";
            this.colTienChenhBN.OptionsColumn.AllowFocus = false;
            this.colTienChenhBN.OptionsColumn.ReadOnly = true;
            this.colTienChenhBN.Visible = true;
            this.colTienChenhBN.VisibleIndex = 11;
            this.colTienChenhBN.Width = 76;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.chkDuyetBN);
            this.panelBottom.Controls.Add(this.simpleButton1);
            this.panelBottom.Controls.Add(this.btnDuyet);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 422);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(938, 38);
            this.panelBottom.TabIndex = 42;
            // 
            // chkDuyetBN
            // 
            this.chkDuyetBN.AutoSize = true;
            this.chkDuyetBN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkDuyetBN.Location = new System.Drawing.Point(12, 11);
            this.chkDuyetBN.Name = "chkDuyetBN";
            this.chkDuyetBN.Size = new System.Drawing.Size(86, 20);
            this.chkDuyetBN.TabIndex = 42;
            this.chkDuyetBN.Text = "Duyệt BN";
            this.chkDuyetBN.UseVisualStyleBackColor = true;
            this.chkDuyetBN.CheckedChanged += new System.EventHandler(this.chkDuyetBN_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(658, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(99, 22);
            this.simpleButton1.TabIndex = 41;
            this.simpleButton1.Text = "&Thoát";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnDuyet
            // 
            this.btnDuyet.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuyet.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnDuyet.Appearance.Options.UseFont = true;
            this.btnDuyet.Appearance.Options.UseForeColor = true;
            this.btnDuyet.Location = new System.Drawing.Point(553, 9);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(99, 22);
            this.btnDuyet.TabIndex = 40;
            this.btnDuyet.Text = "&Duyệt";
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // groupControl6
            // 
            this.groupControl6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl6.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl6.AppearanceCaption.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupControl6.AppearanceCaption.Options.UseFont = true;
            this.groupControl6.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.groupControl6.Controls.Add(this.grcThanhToan);
            this.groupControl6.Location = new System.Drawing.Point(2, 117);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(924, 299);
            this.groupControl6.TabIndex = 39;
            this.groupControl6.Text = "Chi tiết dịch vụ";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Controls.Add(this.groupControl6);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(938, 460);
            this.panelControl2.TabIndex = 43;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.txtLydo);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtChanDoan);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtMaICD);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtTenBN);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.dtNgayRa);
            this.groupControl1.Location = new System.Drawing.Point(2, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(924, 106);
            this.groupControl1.TabIndex = 40;
            this.groupControl1.Text = "Thông tin người bệnh";
            // 
            // txtLydo
            // 
            this.txtLydo.Location = new System.Drawing.Point(118, 81);
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLydo.Properties.Appearance.Options.UseFont = true;
            this.txtLydo.Size = new System.Drawing.Size(562, 22);
            this.txtLydo.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(5, 84);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 16);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Lý do từ chối TT:";
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(118, 53);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtChanDoan.Properties.Appearance.Options.UseFont = true;
            this.txtChanDoan.Properties.ReadOnly = true;
            this.txtChanDoan.Size = new System.Drawing.Size(562, 22);
            this.txtChanDoan.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(5, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(73, 16);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Chẩn đoán:";
            // 
            // txtMaICD
            // 
            this.txtMaICD.Location = new System.Drawing.Point(580, 25);
            this.txtMaICD.Name = "txtMaICD";
            this.txtMaICD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaICD.Properties.Appearance.Options.UseFont = true;
            this.txtMaICD.Properties.ReadOnly = true;
            this.txtMaICD.Size = new System.Drawing.Size(100, 22);
            this.txtMaICD.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(524, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 16);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Mã ICD:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(314, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 16);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Ngày ra viện:";
            // 
            // txtTenBN
            // 
            this.txtTenBN.Location = new System.Drawing.Point(118, 25);
            this.txtTenBN.Name = "txtTenBN";
            this.txtTenBN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenBN.Properties.Appearance.Options.UseFont = true;
            this.txtTenBN.Properties.ReadOnly = true;
            this.txtTenBN.Size = new System.Drawing.Size(190, 22);
            this.txtTenBN.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên BN:";
            // 
            // dtNgayRa
            // 
            this.dtNgayRa.EditValue = null;
            this.dtNgayRa.Location = new System.Drawing.Point(406, 24);
            this.dtNgayRa.Name = "dtNgayRa";
            this.dtNgayRa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtNgayRa.Properties.Appearance.Options.UseFont = true;
            this.dtNgayRa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayRa.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayRa.Properties.Mask.EditMask = "";
            this.dtNgayRa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtNgayRa.Properties.ReadOnly = true;
            this.dtNgayRa.Size = new System.Drawing.Size(112, 22);
            this.dtNgayRa.TabIndex = 3;
            // 
            // colMaQD
            // 
            this.colMaQD.Caption = "Mã QĐ";
            this.colMaQD.FieldName = "MaQD";
            this.colMaQD.Name = "colMaQD";
            this.colMaQD.Visible = true;
            this.colMaQD.VisibleIndex = 0;
            this.colMaQD.Width = 96;
            // 
            // frm_DuyetBN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 460);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DuyetBN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duyệt chi phí Bệnh Nhân";
            this.Load += new System.EventHandler(this.frm_DuyetBN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLydo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChanDoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayRa.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayRa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binSDuyet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcThanhToan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaDVtt;
        private DevExpress.XtraGrid.Columns.GridColumn colDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colDuyet;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkDuyet;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnDuyet;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.BindingSource binSDuyet;
        private DevExpress.XtraGrid.Columns.GridColumn colTienBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTienBH;
        private System.Windows.Forms.CheckBox chkDuyetBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTTDuyet;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtChanDoan;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtMaICD;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtTenBN;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtNgayRa;
        private DevExpress.XtraGrid.Columns.GridColumn colTienChenh;
        private DevExpress.XtraEditors.TextEdit txtLydo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongD;
        private DevExpress.XtraGrid.Columns.GridColumn colTienChenhBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaQD;
    }
}