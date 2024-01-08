namespace QLBV.ChucNang
{
    partial class frm_dsTonDuoc
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
            this.btnupdate = new DevExpress.XtraEditors.SimpleButton();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grcTonDuoc = new DevExpress.XtraGrid.GridControl();
            this.GrvTonDuoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongTon = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcTonDuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvTonDuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnupdate);
            this.panelControl1.Controls.Add(this.searchControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(757, 36);
            this.panelControl1.TabIndex = 0;
            // 
            // btnupdate
            // 
            this.btnupdate.Location = new System.Drawing.Point(569, 5);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(188, 23);
            this.btnupdate.TabIndex = 1;
            this.btnupdate.Text = "update tồn dược";
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(12, 8);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl1.Properties.Appearance.ForeColor = System.Drawing.Color.MediumBlue;
            this.searchControl1.Properties.Appearance.Options.UseFont = true;
            this.searchControl1.Properties.Appearance.Options.UseForeColor = true;
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.NullValuePrompt = "Nhập từ cần tìm kiếm.....";
            this.searchControl1.Size = new System.Drawing.Size(408, 20);
            this.searchControl1.TabIndex = 0;
            this.searchControl1.TextChanged += new System.EventHandler(this.searchControl1_TextChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcTonDuoc);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 36);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(757, 395);
            this.panelControl2.TabIndex = 1;
            // 
            // grcTonDuoc
            // 
            this.grcTonDuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcTonDuoc.Location = new System.Drawing.Point(2, 2);
            this.grcTonDuoc.MainView = this.GrvTonDuoc;
            this.grcTonDuoc.Name = "grcTonDuoc";
            this.grcTonDuoc.Size = new System.Drawing.Size(753, 391);
            this.grcTonDuoc.TabIndex = 0;
            this.grcTonDuoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrvTonDuoc});
            this.grcTonDuoc.Click += new System.EventHandler(this.grcTonDuoc_Click);
            // 
            // GrvTonDuoc
            // 
            this.GrvTonDuoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenDV,
            this.colTenKho,
            this.colDonGia,
            this.colSoLuongTon});
            this.GrvTonDuoc.GridControl = this.grcTonDuoc;
            this.GrvTonDuoc.GroupCount = 1;
            this.GrvTonDuoc.Name = "GrvTonDuoc";
            this.GrvTonDuoc.OptionsBehavior.AutoExpandAllGroups = true;
            this.GrvTonDuoc.OptionsView.ShowChildrenInGroupPanel = true;
            this.GrvTonDuoc.OptionsView.ShowGroupPanel = false;
            this.GrvTonDuoc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTenKho, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã DV";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 97;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên thuốc";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 411;
            // 
            // colTenKho
            // 
            this.colTenKho.Caption = "Tên kho";
            this.colTenKho.FieldName = "TenKP";
            this.colTenKho.Name = "colTenKho";
            this.colTenKho.Visible = true;
            this.colTenKho.VisibleIndex = 2;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 157;
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.Caption = "Số lượng tồn";
            this.colSoLuongTon.FieldName = "SoLuongTon";
            this.colSoLuongTon.Name = "colSoLuongTon";
            this.colSoLuongTon.Visible = true;
            this.colSoLuongTon.VisibleIndex = 3;
            this.colSoLuongTon.Width = 159;
            // 
            // frm_dsTonDuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 431);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_dsTonDuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "danh sách tồn dược";
            this.Load += new System.EventHandler(this.frm_dsTonDuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcTonDuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvTonDuoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnupdate;
        private DevExpress.XtraGrid.GridControl grcTonDuoc;
        private DevExpress.XtraGrid.Views.Grid.GridView GrvTonDuoc;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKho;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongTon;
    }
}