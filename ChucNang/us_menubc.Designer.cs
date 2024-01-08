namespace QLBV.FormThamSo
{
    partial class us_menubc
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
            this.components = new System.ComponentModel.Container();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grcMenu = new DevExpress.XtraGrid.GridControl();
            this.grvMenu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colNhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcMenu);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 23);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1054, 503);
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
            this.grcMenu.Size = new System.Drawing.Size(1050, 499);
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
            this.colID,
            this.colTenBC,
            this.colNhom});
            this.grvMenu.GridControl = this.grcMenu;
            this.grvMenu.GroupCount = 1;
            this.grvMenu.Name = "grvMenu";
            this.grvMenu.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvMenu.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvMenu.OptionsBehavior.Editable = false;
            this.grvMenu.OptionsBehavior.ReadOnly = true;
            this.grvMenu.OptionsView.ShowGroupPanel = false;
            this.grvMenu.OptionsView.ShowIndicator = false;
            this.grvMenu.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNhom, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvMenu.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvMenu_RowCellClick);
            // 
            // colID
            // 
            this.colID.AppearanceCell.ForeColor = System.Drawing.Color.Gray;
            this.colID.AppearanceCell.Options.UseForeColor = true;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 93;
            // 
            // colTenBC
            // 
            this.colTenBC.Caption = "Tên Báo cáo";
            this.colTenBC.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colTenBC.FieldName = "TenBC";
            this.colTenBC.Name = "colTenBC";
            this.colTenBC.OptionsColumn.AllowFocus = false;
            this.colTenBC.OptionsColumn.ReadOnly = true;
            this.colTenBC.Visible = true;
            this.colTenBC.VisibleIndex = 1;
            this.colTenBC.Width = 961;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // colNhom
            // 
            this.colNhom.Caption = "menu";
            this.colNhom.FieldName = "Nhom";
            this.colNhom.Name = "colNhom";
            this.colNhom.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.colNhom.Visible = true;
            this.colNhom.VisibleIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1058, 528);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Báo cáo";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // us_menubc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "us_menubc";
            this.Size = new System.Drawing.Size(1058, 528);
            this.Load += new System.EventHandler(this.us_menubc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grcMenu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMenu;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colNhom;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
