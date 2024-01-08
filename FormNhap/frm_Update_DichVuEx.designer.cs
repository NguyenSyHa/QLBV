namespace QLBV.FormNhap
{
    partial class frm_Update_DichVuEx
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grc_DichVu = new DevExpress.XtraGrid.GridControl();
            this.grv_DichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaQD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenThhau2017 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaATC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHinhThucMua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupHinhThucMua = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhaSX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaHC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupNhaCC = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lupMaATC = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.grLupNCC = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVEN = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_DichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupHinhThucMua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaATC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grLupNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(909, 58);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtTimKiem);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(905, 54);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.EditValue = "";
            this.txtTimKiem.Location = new System.Drawing.Point(6, 25);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.NullText = "Mã / tên dịch vụ/ Mã QD";
            this.txtTimKiem.Size = new System.Drawing.Size(248, 22);
            this.txtTimKiem.TabIndex = 3;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.txtTimKiem.Click += new System.EventHandler(this.txtTimKiem_Click);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grc_DichVu);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 58);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(909, 454);
            this.panelControl2.TabIndex = 1;
            // 
            // grc_DichVu
            // 
            this.grc_DichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_DichVu.Location = new System.Drawing.Point(2, 2);
            this.grc_DichVu.MainView = this.grv_DichVu;
            this.grc_DichVu.Name = "grc_DichVu";
            this.grc_DichVu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupHinhThucMua,
            this.lupNhaCC,
            this.lupMaATC,
            this.grLupNCC});
            this.grc_DichVu.Size = new System.Drawing.Size(905, 450);
            this.grc_DichVu.TabIndex = 0;
            this.grc_DichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_DichVu});
            // 
            // grv_DichVu
            // 
            this.grv_DichVu.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.grv_DichVu.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_DichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colMaQD,
            this.colTenDV,
            this.colTenThhau2017,
            this.colDonGia,
            this.colMaATC,
            this.colHinhThucMua,
            this.colNCC,
            this.colNhaSX,
            this.colMaHC,
            this.colVEN});
            this.grv_DichVu.GridControl = this.grc_DichVu;
            this.grv_DichVu.Name = "grv_DichVu";
            this.grv_DichVu.OptionsView.ColumnAutoWidth = false;
            this.grv_DichVu.OptionsView.EnableAppearanceOddRow = true;
            this.grv_DichVu.OptionsView.ShowGroupPanel = false;
            this.grv_DichVu.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grv_DichVu_ValidateRow);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã dịch vụ";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.AllowEdit = false;
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 60;
            // 
            // colMaQD
            // 
            this.colMaQD.Caption = "Mã QĐ";
            this.colMaQD.FieldName = "MaQD";
            this.colMaQD.Name = "colMaQD";
            this.colMaQD.OptionsColumn.AllowEdit = false;
            this.colMaQD.OptionsColumn.ReadOnly = true;
            this.colMaQD.Visible = true;
            this.colMaQD.VisibleIndex = 1;
            this.colMaQD.Width = 60;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.OptionsColumn.ReadOnly = true;
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 2;
            this.colTenDV.Width = 150;
            // 
            // colTenThhau2017
            // 
            this.colTenThhau2017.Caption = "Gói thầu";
            this.colTenThhau2017.FieldName = "TenThau2017";
            this.colTenThhau2017.Name = "colTenThhau2017";
            this.colTenThhau2017.Visible = true;
            this.colTenThhau2017.VisibleIndex = 3;
            this.colTenThhau2017.Width = 90;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowEdit = false;
            this.colDonGia.OptionsColumn.ReadOnly = true;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 4;
            // 
            // colMaATC
            // 
            this.colMaATC.Caption = "Mã ATC";
            this.colMaATC.FieldName = "MaATC";
            this.colMaATC.Name = "colMaATC";
            this.colMaATC.Visible = true;
            this.colMaATC.VisibleIndex = 5;
            this.colMaATC.Width = 70;
            // 
            // colHinhThucMua
            // 
            this.colHinhThucMua.Caption = "Hình thức mua";
            this.colHinhThucMua.ColumnEdit = this.lupHinhThucMua;
            this.colHinhThucMua.FieldName = "MaHinhThuc";
            this.colHinhThucMua.Name = "colHinhThucMua";
            this.colHinhThucMua.Visible = true;
            this.colHinhThucMua.VisibleIndex = 7;
            this.colHinhThucMua.Width = 120;
            // 
            // lupHinhThucMua
            // 
            this.lupHinhThucMua.AutoHeight = false;
            this.lupHinhThucMua.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupHinhThucMua.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHinhThuc", "Hình thức mua")});
            this.lupHinhThucMua.DisplayMember = "TenHinhThuc";
            this.lupHinhThucMua.Name = "lupHinhThucMua";
            this.lupHinhThucMua.NullText = "";
            this.lupHinhThucMua.ValueMember = "MaHinhThuc";
            // 
            // colNCC
            // 
            this.colNCC.Caption = "Nhà cung cấp";
            this.colNCC.FieldName = "NCC";
            this.colNCC.Name = "colNCC";
            this.colNCC.OptionsColumn.AllowEdit = false;
            this.colNCC.OptionsColumn.ReadOnly = true;
            this.colNCC.Visible = true;
            this.colNCC.VisibleIndex = 8;
            this.colNCC.Width = 150;
            // 
            // colNhaSX
            // 
            this.colNhaSX.Caption = "Nhà sản xuất";
            this.colNhaSX.FieldName = "NhaSX";
            this.colNhaSX.Name = "colNhaSX";
            this.colNhaSX.OptionsColumn.AllowEdit = false;
            this.colNhaSX.OptionsColumn.ReadOnly = true;
            this.colNhaSX.Visible = true;
            this.colNhaSX.VisibleIndex = 9;
            this.colNhaSX.Width = 150;
            // 
            // colMaHC
            // 
            this.colMaHC.Caption = "Mã hoạt chất";
            this.colMaHC.FieldName = "MaHC";
            this.colMaHC.Name = "colMaHC";
            this.colMaHC.Visible = true;
            this.colMaHC.VisibleIndex = 6;
            // 
            // lupNhaCC
            // 
            this.lupNhaCC.AutoHeight = false;
            this.lupNhaCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhaCC.DisplayMember = "TenCC";
            this.lupNhaCC.Name = "lupNhaCC";
            this.lupNhaCC.NullText = "";
            this.lupNhaCC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNhaCC.ValueMember = "MaCC";
            // 
            // lupMaATC
            // 
            this.lupMaATC.AutoHeight = false;
            this.lupMaATC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaATC.DisplayMember = "MaATC";
            this.lupMaATC.Name = "lupMaATC";
            this.lupMaATC.NullText = "";
            this.lupMaATC.ValueMember = "MaATC";
            // 
            // grLupNCC
            // 
            this.grLupNCC.AutoHeight = false;
            this.grLupNCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grLupNCC.DisplayMember = "TenCC";
            this.grLupNCC.ImmediatePopup = true;
            this.grLupNCC.Name = "grLupNCC";
            this.grLupNCC.NullText = "";
            this.grLupNCC.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.grLupNCC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.grLupNCC.ValueMember = "MaCC";
            this.grLupNCC.View = this.gridView1;
            this.grLupNCC.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colVEN
            // 
            this.colVEN.Caption = "VEN";
            this.colVEN.FieldName = "VEN";
            this.colVEN.Name = "colVEN";
            this.colVEN.Visible = true;
            this.colVEN.VisibleIndex = 10;
            this.colVEN.Width = 50;
            // 
            // frm_Update_DichVuEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 512);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Update_DichVuEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin dịch vụ bổ sung";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Update_DichVuEx_FormClosing);
            this.Load += new System.EventHandler(this.frm_Update_DichVuEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_DichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupHinhThucMua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaATC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grLupNCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grc_DichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_DichVu;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colMaATC;
        private DevExpress.XtraGrid.Columns.GridColumn colHinhThucMua;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupHinhThucMua;
        private DevExpress.XtraGrid.Columns.GridColumn colNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colNhaSX;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaATC;
        private DevExpress.XtraGrid.Columns.GridColumn colMaQD;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupNhaCC;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit grLupNCC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colMaHC;
        private DevExpress.XtraGrid.Columns.GridColumn colTenThhau2017;
        private DevExpress.XtraGrid.Columns.GridColumn colVEN;
    }
}