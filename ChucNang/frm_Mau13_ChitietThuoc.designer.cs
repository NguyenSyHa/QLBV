namespace QLBV.FormNhap
{
    partial class frm_Mau13_ChitietThuoc
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
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.grcThanhToan = new DevExpress.XtraGrid.GridControl();
            this.grvThanhToan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNgayKe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrongBH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKPTongKet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lup_MaKP = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.htp_Sua = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colIDDonct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaDVtt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.luptrongDM = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.htp_Sua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptrongDM)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl6
            // 
            this.groupControl6.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl6.AppearanceCaption.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupControl6.AppearanceCaption.Options.UseFont = true;
            this.groupControl6.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.groupControl6.Controls.Add(this.grcThanhToan);
            this.groupControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl6.Location = new System.Drawing.Point(0, 0);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(793, 330);
            this.groupControl6.TabIndex = 41;
            this.groupControl6.Text = "danh sách chi tiết thuốc";
            // 
            // grcThanhToan
            // 
            this.grcThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcThanhToan.Location = new System.Drawing.Point(2, 20);
            this.grcThanhToan.MainView = this.grvThanhToan;
            this.grcThanhToan.Name = "grcThanhToan";
            this.grcThanhToan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupMaDVtt,
            this.luptrongDM,
            this.htp_Sua,
            this.lup_MaKP});
            this.grcThanhToan.Size = new System.Drawing.Size(789, 308);
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
            this.colNgayKe,
            this.colMaDV,
            this.colDonVi,
            this.colDonGia,
            this.colSoLuong,
            this.colThanhTien,
            this.colTrongBH,
            this.colTenKP,
            this.colTenKPTongKet,
            this.colSua,
            this.colIDDonct});
            this.grvThanhToan.GridControl = this.grcThanhToan;
            this.grvThanhToan.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "thanhtien", this.colThanhTien, "##,###"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "madv", null, "")});
            this.grvThanhToan.Name = "grvThanhToan";
            this.grvThanhToan.OptionsView.ColumnAutoWidth = false;
            this.grvThanhToan.OptionsView.ShowFooter = true;
            this.grvThanhToan.OptionsView.ShowGroupPanel = false;
            // 
            // colNgayKe
            // 
            this.colNgayKe.Caption = "Ngày kê";
            this.colNgayKe.FieldName = "NgayKe";
            this.colNgayKe.Name = "colNgayKe";
            this.colNgayKe.OptionsColumn.AllowEdit = false;
            this.colNgayKe.OptionsColumn.ReadOnly = true;
            this.colNgayKe.Visible = true;
            this.colNgayKe.VisibleIndex = 0;
            this.colNgayKe.Width = 70;
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Tên chi phí";
            this.colMaDV.FieldName = "TenDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.AllowEdit = false;
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 1;
            this.colMaDV.Width = 250;
            // 
            // colDonVi
            // 
            this.colDonVi.AppearanceCell.Options.UseTextOptions = true;
            this.colDonVi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDonVi.Caption = "Đơn vị";
            this.colDonVi.FieldName = "DonVi";
            this.colDonVi.Name = "colDonVi";
            this.colDonVi.OptionsColumn.AllowEdit = false;
            this.colDonVi.OptionsColumn.ReadOnly = true;
            this.colDonVi.Visible = true;
            this.colDonVi.VisibleIndex = 2;
            this.colDonVi.Width = 60;
            // 
            // colDonGia
            // 
            this.colDonGia.AppearanceCell.Options.UseTextOptions = true;
            this.colDonGia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "##,###.000";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowEdit = false;
            this.colDonGia.OptionsColumn.ReadOnly = true;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 3;
            this.colDonGia.Width = 64;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AppearanceCell.Options.UseTextOptions = true;
            this.colSoLuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.OptionsColumn.AllowEdit = false;
            this.colSoLuong.OptionsColumn.ReadOnly = true;
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 4;
            this.colSoLuong.Width = 57;
            // 
            // colThanhTien
            // 
            this.colThanhTien.AppearanceCell.Options.UseTextOptions = true;
            this.colThanhTien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colThanhTien.Caption = "thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "##,###.000";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.OptionsColumn.AllowEdit = false;
            this.colThanhTien.OptionsColumn.ReadOnly = true;
            this.colThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 5;
            this.colThanhTien.Width = 85;
            // 
            // colTrongBH
            // 
            this.colTrongBH.Caption = "Trong DMBH";
            this.colTrongBH.FieldName = "TrongBH";
            this.colTrongBH.Name = "colTrongBH";
            this.colTrongBH.OptionsColumn.AllowEdit = false;
            this.colTrongBH.OptionsColumn.ReadOnly = true;
            this.colTrongBH.Visible = true;
            this.colTrongBH.VisibleIndex = 6;
            this.colTrongBH.Width = 80;
            // 
            // colTenKP
            // 
            this.colTenKP.Caption = "Khoa|Phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.OptionsColumn.AllowEdit = false;
            this.colTenKP.OptionsColumn.ReadOnly = true;
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 7;
            this.colTenKP.Width = 150;
            // 
            // colTenKPTongKet
            // 
            this.colTenKPTongKet.Caption = "Khoa phòng tổng kết";
            this.colTenKPTongKet.ColumnEdit = this.lup_MaKP;
            this.colTenKPTongKet.FieldName = "MaKPtongket";
            this.colTenKPTongKet.Name = "colTenKPTongKet";
            this.colTenKPTongKet.Visible = true;
            this.colTenKPTongKet.VisibleIndex = 8;
            this.colTenKPTongKet.Width = 150;
            // 
            // lup_MaKP
            // 
            this.lup_MaKP.AutoHeight = false;
            this.lup_MaKP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MaKP.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa tổng kết")});
            this.lup_MaKP.DisplayMember = "TenKP";
            this.lup_MaKP.Name = "lup_MaKP";
            this.lup_MaKP.NullText = "";
            this.lup_MaKP.ValueMember = "MaKP";
            // 
            // colSua
            // 
            this.colSua.Caption = "Sửa";
            this.colSua.ColumnEdit = this.htp_Sua;
            this.colSua.Name = "colSua";
            this.colSua.OptionsColumn.AllowFocus = false;
            this.colSua.OptionsColumn.ReadOnly = true;
            this.colSua.Width = 74;
            // 
            // htp_Sua
            // 
            this.htp_Sua.AllowFocused = false;
            this.htp_Sua.AutoHeight = false;
            this.htp_Sua.Name = "htp_Sua";
            this.htp_Sua.NullText = "Sửa";
            this.htp_Sua.ReadOnly = true;
            // 
            // colIDDonct
            // 
            this.colIDDonct.Caption = "gridColumn1";
            this.colIDDonct.FieldName = "IDDonct";
            this.colIDDonct.Name = "colIDDonct";
            // 
            // lupMaDVtt
            // 
            this.lupMaDVtt.AutoHeight = false;
            this.lupMaDVtt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaDVtt.DisplayMember = "TenDV";
            this.lupMaDVtt.Name = "lupMaDVtt";
            this.lupMaDVtt.NullText = "";
            this.lupMaDVtt.ValueMember = "MaDV";
            // 
            // luptrongDM
            // 
            this.luptrongDM.AutoHeight = false;
            this.luptrongDM.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptrongDM.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten_TrongBH", "Trong DMBH"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TrongBH", "value")});
            this.luptrongDM.DisplayMember = "Ten_TrongBH";
            this.luptrongDM.Name = "luptrongDM";
            this.luptrongDM.NullText = "";
            this.luptrongDM.ValueMember = "TrongBH";
            // 
            // frm_Mau13_ChitietThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 330);
            this.Controls.Add(this.groupControl6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Mau13_ChitietThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết thuốc";
            this.Load += new System.EventHandler(this.frm_Mau13_ChitietThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.htp_Sua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptrongDM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl grcThanhToan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayKe;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colTrongBH;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKPTongKet;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lup_MaKP;
        private DevExpress.XtraGrid.Columns.GridColumn colSua;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit htp_Sua;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDonct;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaDVtt;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luptrongDM;
    }
}