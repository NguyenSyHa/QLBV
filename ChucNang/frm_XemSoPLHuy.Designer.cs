namespace QLBV.FormNhap
{
    partial class frm_XemSoPLHuy
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.grcPLhuy = new DevExpress.XtraGrid.GridControl();
            this.grvBenhNhankd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSoPL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKphong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayHuy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupKieuDon = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcPLhuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhankd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKieuDon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl6);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(664, 573);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl6
            // 
            this.groupControl6.Appearance.BackColor = System.Drawing.Color.Silver;
            this.groupControl6.Appearance.Options.UseBackColor = true;
            this.groupControl6.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl6.AppearanceCaption.ForeColor = System.Drawing.Color.CadetBlue;
            this.groupControl6.AppearanceCaption.Options.UseFont = true;
            this.groupControl6.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl6.Controls.Add(this.grcPLhuy);
            this.groupControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl6.Location = new System.Drawing.Point(0, 0);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(664, 573);
            this.groupControl6.TabIndex = 1;
            this.groupControl6.Text = "Danh sách phiếu lĩnh đã hủy";
            // 
            // grcPLhuy
            // 
            this.grcPLhuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcPLhuy.Location = new System.Drawing.Point(2, 20);
            this.grcPLhuy.MainView = this.grvBenhNhankd;
            this.grcPLhuy.Name = "grcPLhuy";
            this.grcPLhuy.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupKieuDon});
            this.grcPLhuy.Size = new System.Drawing.Size(660, 551);
            this.grcPLhuy.TabIndex = 0;
            this.grcPLhuy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBenhNhankd});
            // 
            // grvBenhNhankd
            // 
            this.grvBenhNhankd.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvBenhNhankd.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.grvBenhNhankd.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvBenhNhankd.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvBenhNhankd.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvBenhNhankd.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvBenhNhankd.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.grvBenhNhankd.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.grvBenhNhankd.Appearance.Row.Options.UseFont = true;
            this.grvBenhNhankd.Appearance.Row.Options.UseForeColor = true;
            this.grvBenhNhankd.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.grvBenhNhankd.Appearance.ViewCaption.Options.UseFont = true;
            this.grvBenhNhankd.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSoPL,
            this.colNgayNhap,
            this.colTenCB,
            this.colKphong,
            this.colNgayHuy});
            this.grvBenhNhankd.GridControl = this.grcPLhuy;
            this.grvBenhNhankd.Name = "grvBenhNhankd";
            this.grvBenhNhankd.NewItemRowText = "Thêm mới";
            this.grvBenhNhankd.OptionsView.ShowGroupPanel = false;
            this.grvBenhNhankd.ViewCaption = "Chi tiết đơn thuốc";
            // 
            // colSoPL
            // 
            this.colSoPL.Caption = "Số phiếu lĩnh";
            this.colSoPL.FieldName = "SoPL1";
            this.colSoPL.Name = "colSoPL";
            this.colSoPL.Visible = true;
            this.colSoPL.VisibleIndex = 0;
            this.colSoPL.Width = 120;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.Caption = "Ngày tạo";
            this.colNgayNhap.FieldName = "NgayNhap";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Visible = true;
            this.colNgayNhap.VisibleIndex = 1;
            this.colNgayNhap.Width = 143;
            // 
            // colTenCB
            // 
            this.colTenCB.Caption = "Tên cán bộ";
            this.colTenCB.FieldName = "TenCB";
            this.colTenCB.Name = "colTenCB";
            this.colTenCB.Visible = true;
            this.colTenCB.VisibleIndex = 2;
            this.colTenCB.Width = 144;
            // 
            // colKphong
            // 
            this.colKphong.Caption = "Khoa phòng";
            this.colKphong.FieldName = "TenKP";
            this.colKphong.Name = "colKphong";
            this.colKphong.Visible = true;
            this.colKphong.VisibleIndex = 3;
            this.colKphong.Width = 135;
            // 
            // colNgayHuy
            // 
            this.colNgayHuy.Caption = "Ngày hủy";
            this.colNgayHuy.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.colNgayHuy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayHuy.FieldName = "TGHuy";
            this.colNgayHuy.Name = "colNgayHuy";
            this.colNgayHuy.Visible = true;
            this.colNgayHuy.VisibleIndex = 4;
            this.colNgayHuy.Width = 154;
            // 
            // lupKieuDon
            // 
            this.lupKieuDon.AutoHeight = false;
            this.lupKieuDon.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKieuDon.DisplayMember = "Ten";
            this.lupKieuDon.Name = "lupKieuDon";
            this.lupKieuDon.ValueMember = "Index";
            // 
            // frm_XemSoPLHuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 573);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_XemSoPLHuy";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "danh sách phiếu lĩnh";
            this.Load += new System.EventHandler(this.frm_XemSoPLHuy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcPLhuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhankd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKieuDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl grcPLhuy;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBenhNhankd;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupKieuDon;
        private DevExpress.XtraGrid.Columns.GridColumn colSoPL;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCB;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHuy;
        private DevExpress.XtraGrid.Columns.GridColumn colKphong;
    }
}