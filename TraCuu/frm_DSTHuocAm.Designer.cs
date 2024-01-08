namespace QLBV.TraCuu
{
    partial class frm_DSTHuocAm
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
            this.grcThuocTon = new DevExpress.XtraGrid.GridControl();
            this.grvThuocTon = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenTHuoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.htpXem = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grcThuocTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThuocTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.htpXem)).BeginInit();
            this.SuspendLayout();
            // 
            // grcThuocTon
            // 
            this.grcThuocTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcThuocTon.Location = new System.Drawing.Point(0, 0);
            this.grcThuocTon.MainView = this.grvThuocTon;
            this.grcThuocTon.Name = "grcThuocTon";
            this.grcThuocTon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.htpXem});
            this.grcThuocTon.Size = new System.Drawing.Size(534, 273);
            this.grcThuocTon.TabIndex = 0;
            this.grcThuocTon.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThuocTon});
            // 
            // grvThuocTon
            // 
            this.grvThuocTon.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvThuocTon.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvThuocTon.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grvThuocTon.Appearance.Row.Options.UseFont = true;
            this.grvThuocTon.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenTHuoc,
            this.colDonGia,
            this.colSoLuong,
            this.colMaDV,
            this.colXem});
            this.grvThuocTon.GridControl = this.grcThuocTon;
            this.grvThuocTon.Name = "grvThuocTon";
            this.grvThuocTon.OptionsBehavior.Editable = false;
            this.grvThuocTon.OptionsBehavior.ReadOnly = true;
            this.grvThuocTon.OptionsView.EnableAppearanceEvenRow = true;
            this.grvThuocTon.OptionsView.EnableAppearanceOddRow = true;
            this.grvThuocTon.OptionsView.ShowGroupPanel = false;
            this.grvThuocTon.OptionsView.ShowViewCaption = true;
            this.grvThuocTon.ViewCaption = "Danh sách thuốc có số lượng tồn < 0";
            this.grvThuocTon.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvThuocTon_RowCellClick);
            // 
            // colTenTHuoc
            // 
            this.colTenTHuoc.Caption = "Tên thuốc";
            this.colTenTHuoc.FieldName = "TenDV";
            this.colTenTHuoc.Name = "colTenTHuoc";
            this.colTenTHuoc.Visible = true;
            this.colTenTHuoc.VisibleIndex = 0;
            this.colTenTHuoc.Width = 373;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "##,###.00";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 1;
            this.colDonGia.Width = 93;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 2;
            this.colSoLuong.Width = 89;
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "MaDV";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            // 
            // colXem
            // 
            this.colXem.Caption = "Xem";
            this.colXem.ColumnEdit = this.htpXem;
            this.colXem.Name = "colXem";
            this.colXem.Visible = true;
            this.colXem.VisibleIndex = 3;
            this.colXem.Width = 201;
            // 
            // htpXem
            // 
            this.htpXem.AutoHeight = false;
            this.htpXem.Name = "htpXem";
            this.htpXem.NullText = "Xem quá trình N_X";
            // 
            // frm_DSTHuocAm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 273);
            this.Controls.Add(this.grcThuocTon);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DSTHuocAm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách thuốc có số lượng tồn <0";
            this.Load += new System.EventHandler(this.frm_DSTHuocAm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcThuocTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThuocTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.htpXem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcThuocTon;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThuocTon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTHuoc;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colXem;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit htpXem;
    }
}