namespace QLBV.FormThamSo
{
    partial class us_NangCap
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grcMenu = new DevExpress.XtraGrid.GridControl();
            this.grvMenu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNgay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenbang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colTenTruong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKieuDuLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1058, 17);
            this.panelControl1.TabIndex = 1;
            this.panelControl1.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcMenu);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 17);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1058, 511);
            this.panelControl2.TabIndex = 2;
            // 
            // grcMenu
            // 
            this.grcMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcMenu.Location = new System.Drawing.Point(2, 2);
            this.grcMenu.MainView = this.grvMenu;
            this.grcMenu.Name = "grcMenu";
            this.grcMenu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.grcMenu.Size = new System.Drawing.Size(1054, 507);
            this.grcMenu.TabIndex = 0;
            this.grcMenu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMenu});
            // 
            // grvMenu
            // 
            this.grvMenu.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvMenu.Appearance.Row.ForeColor = System.Drawing.Color.Navy;
            this.grvMenu.Appearance.Row.Options.UseFont = true;
            this.grvMenu.Appearance.Row.Options.UseForeColor = true;
            this.grvMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grvMenu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNgay,
            this.colTenbang,
            this.colTenTruong,
            this.colKieuDuLieu,
            this.colGhiChu});
            this.grvMenu.GridControl = this.grcMenu;
            this.grvMenu.Name = "grvMenu";
            this.grvMenu.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvMenu.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvMenu.OptionsBehavior.Editable = false;
            this.grvMenu.OptionsBehavior.ReadOnly = true;
            this.grvMenu.OptionsView.ShowGroupPanel = false;
            this.grvMenu.OptionsView.ShowIndicator = false;
            this.grvMenu.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvMenu_RowCellClick);
            // 
            // colNgay
            // 
            this.colNgay.AppearanceCell.ForeColor = System.Drawing.Color.Gray;
            this.colNgay.AppearanceCell.Options.UseForeColor = true;
            this.colNgay.Caption = "ID";
            this.colNgay.FieldName = "ID";
            this.colNgay.Name = "colNgay";
            this.colNgay.Visible = true;
            this.colNgay.VisibleIndex = 0;
            this.colNgay.Width = 26;
            // 
            // colTenbang
            // 
            this.colTenbang.Caption = "Tên Bảng";
            this.colTenbang.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colTenbang.FieldName = "TenBang";
            this.colTenbang.Name = "colTenbang";
            this.colTenbang.OptionsColumn.AllowFocus = false;
            this.colTenbang.OptionsColumn.ReadOnly = true;
            this.colTenbang.Visible = true;
            this.colTenbang.VisibleIndex = 1;
            this.colTenbang.Width = 85;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // colTenTruong
            // 
            this.colTenTruong.Caption = "Tên Trường";
            this.colTenTruong.FieldName = "TenTruong";
            this.colTenTruong.Name = "colTenTruong";
            this.colTenTruong.Visible = true;
            this.colTenTruong.VisibleIndex = 2;
            this.colTenTruong.Width = 116;
            // 
            // colKieuDuLieu
            // 
            this.colKieuDuLieu.Caption = "Kiểu dữ liệu";
            this.colKieuDuLieu.Name = "colKieuDuLieu";
            this.colKieuDuLieu.Visible = true;
            this.colKieuDuLieu.VisibleIndex = 3;
            this.colKieuDuLieu.Width = 547;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 4;
            // 
            // us_NangCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "us_NangCap";
            this.Size = new System.Drawing.Size(1058, 528);
            this.Load += new System.EventHandler(this.us_menubc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grcMenu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMenu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgay;
        private DevExpress.XtraGrid.Columns.GridColumn colTenbang;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTruong;
        private DevExpress.XtraGrid.Columns.GridColumn colKieuDuLieu;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
    }
}
