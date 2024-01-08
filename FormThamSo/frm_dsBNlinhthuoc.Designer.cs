namespace QLBV.FormThamSo
{
    partial class frm_dsBNlinhthuoc
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
            this.grcDsBNhan = new DevExpress.XtraGrid.GridControl();
            this.grvDsBNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTuoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayKe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.colMaBN = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grcDsBNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDsBNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcDsBNhan
            // 
            this.grcDsBNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDsBNhan.Location = new System.Drawing.Point(2, 22);
            this.grcDsBNhan.MainView = this.grvDsBNhan;
            this.grcDsBNhan.Name = "grcDsBNhan";
            this.grcDsBNhan.Size = new System.Drawing.Size(598, 267);
            this.grcDsBNhan.TabIndex = 0;
            this.grcDsBNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDsBNhan});
            // 
            // grvDsBNhan
            // 
            this.grvDsBNhan.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.grvDsBNhan.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDsBNhan.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvDsBNhan.Appearance.GroupRow.Options.UseFont = true;
            this.grvDsBNhan.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDsBNhan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDsBNhan.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvDsBNhan.Appearance.Row.Options.UseFont = true;
            this.grvDsBNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBN,
            this.colTenBNhan,
            this.colTuoi,
            this.colDiaChi,
            this.colSoLuong,
            this.colNgayKe});
            this.grvDsBNhan.GridControl = this.grcDsBNhan;
            this.grvDsBNhan.GroupFormat = "{1}";
            this.grvDsBNhan.Name = "grvDsBNhan";
            this.grvDsBNhan.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDsBNhan.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDsBNhan.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvDsBNhan.OptionsBehavior.Editable = false;
            this.grvDsBNhan.OptionsBehavior.ReadOnly = true;
            this.grvDsBNhan.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDsBNhan.OptionsView.ShowFooter = true;
            this.grvDsBNhan.OptionsView.ShowGroupPanel = false;
            // 
            // colTenBNhan
            // 
            this.colTenBNhan.Caption = "Tên bệnh nhân";
            this.colTenBNhan.FieldName = "TenBNhan";
            this.colTenBNhan.Name = "colTenBNhan";
            this.colTenBNhan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colTenBNhan.Visible = true;
            this.colTenBNhan.VisibleIndex = 0;
            this.colTenBNhan.Width = 156;
            // 
            // colTuoi
            // 
            this.colTuoi.AppearanceCell.Options.UseTextOptions = true;
            this.colTuoi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTuoi.Caption = "Tuổi";
            this.colTuoi.FieldName = "Tuoi";
            this.colTuoi.Name = "colTuoi";
            this.colTuoi.Visible = true;
            this.colTuoi.VisibleIndex = 3;
            this.colTuoi.Width = 42;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "DChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 4;
            this.colDiaChi.Width = 254;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 2;
            this.colSoLuong.Width = 51;
            // 
            // colNgayKe
            // 
            this.colNgayKe.Caption = "Ngày kê";
            this.colNgayKe.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayKe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayKe.FieldName = "NgayKe";
            this.colNgayKe.Name = "colNgayKe";
            this.colNgayKe.OptionsColumn.AllowEdit = false;
            this.colNgayKe.Visible = true;
            this.colNgayKe.VisibleIndex = 1;
            this.colNgayKe.Width = 82;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Teal;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.grcDsBNhan);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(602, 291);
            this.groupControl1.TabIndex = 1;
            // 
            // colMaBN
            // 
            this.colMaBN.Caption = "Mã Bệnh Nhân";
            this.colMaBN.FieldName = "MaBNhan";
            this.colMaBN.Name = "colMaBN";
            this.colMaBN.Visible = true;
            this.colMaBN.VisibleIndex = 5;
            // 
            // frm_dsBNlinhthuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 291);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_dsBNlinhthuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh nhân lĩnh thuốc";
            this.Load += new System.EventHandler(this.frm_dsBNlinhthuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcDsBNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDsBNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcDsBNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDsBNhan;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colTuoi;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayKe;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBN;
    }
}