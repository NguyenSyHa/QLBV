namespace QLBV.FormThamSo
{
    partial class frm_mau13
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chk_TrongDM = new DevExpress.XtraEditors.CheckEdit();
            this.chk_NgoaiDM = new DevExpress.XtraEditors.CheckEdit();
            this.chl_KhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.grcThanhToan = new DevExpress.XtraGrid.GridControl();
            this.grvThanhToan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaDVtt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrongBH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.luptrongDM = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.htp_Sua = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.col_makp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lup_MaKP = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_TrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_NgoaiDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chl_KhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptrongDM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.htp_Sua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(650, 71);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 14;
            this.simpleButton1.Text = "&In phiếu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(731, 71);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 15;
            this.simpleButton2.Text = "Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnUpdate.Appearance.Options.UseFont = true;
            this.btnUpdate.Location = new System.Drawing.Point(740, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.chl_KhoaPhong);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(827, 100);
            this.panelControl1.TabIndex = 17;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.chk_TrongDM);
            this.groupControl1.Controls.Add(this.chk_NgoaiDM);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(274, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 100);
            this.groupControl1.TabIndex = 17;
            this.groupControl1.Text = "Trong|Ngoài DM";
            this.groupControl1.Visible = false;
            // 
            // chk_TrongDM
            // 
            this.chk_TrongDM.Location = new System.Drawing.Point(6, 48);
            this.chk_TrongDM.Name = "chk_TrongDM";
            this.chk_TrongDM.Properties.Caption = "Trong DM";
            this.chk_TrongDM.Size = new System.Drawing.Size(75, 19);
            this.chk_TrongDM.TabIndex = 1;
            // 
            // chk_NgoaiDM
            // 
            this.chk_NgoaiDM.Location = new System.Drawing.Point(6, 23);
            this.chk_NgoaiDM.Name = "chk_NgoaiDM";
            this.chk_NgoaiDM.Properties.Caption = "Ngoài DM";
            this.chk_NgoaiDM.Size = new System.Drawing.Size(75, 19);
            this.chk_NgoaiDM.TabIndex = 0;
            // 
            // chl_KhoaPhong
            // 
            this.chl_KhoaPhong.DisplayMember = "TenKP";
            this.chl_KhoaPhong.Dock = System.Windows.Forms.DockStyle.Left;
            this.chl_KhoaPhong.Location = new System.Drawing.Point(0, 0);
            this.chl_KhoaPhong.Name = "chl_KhoaPhong";
            this.chl_KhoaPhong.Size = new System.Drawing.Size(274, 100);
            this.chl_KhoaPhong.TabIndex = 14;
            this.chl_KhoaPhong.ValueMember = "IDKB";
            this.chl_KhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.chl_KhoaPhong_ItemCheck);
            this.chl_KhoaPhong.SelectedIndexChanged += new System.EventHandler(this.chl_KhoaPhong_SelectedIndexChanged);
            this.chl_KhoaPhong.SelectedValueChanged += new System.EventHandler(this.chl_KhoaPhong_SelectedValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 453);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(827, 39);
            this.panelControl2.TabIndex = 18;
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
            this.groupControl6.Size = new System.Drawing.Size(827, 353);
            this.groupControl6.TabIndex = 40;
            this.groupControl6.Text = "Chi tiết dịch vụ";
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
            this.grcThanhToan.Size = new System.Drawing.Size(823, 331);
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
            this.colTrongBH,
            this.colSua,
            this.col_makp});
            this.grvThanhToan.GridControl = this.grcThanhToan;
            this.grvThanhToan.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "thanhtien", this.colThanhTien, "##,###"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "madv", null, "")});
            this.grvThanhToan.Name = "grvThanhToan";
            this.grvThanhToan.OptionsBehavior.Editable = false;
            this.grvThanhToan.OptionsBehavior.ReadOnly = true;
            this.grvThanhToan.OptionsView.ShowFooter = true;
            this.grvThanhToan.OptionsView.ShowGroupPanel = false;
            this.grvThanhToan.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvThanhToan_RowCellClick);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Tên chi phí";
            this.colMaDV.ColumnEdit = this.lupMaDVtt;
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 1;
            this.colMaDV.Width = 251;
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
            // colDonVi
            // 
            this.colDonVi.AppearanceCell.Options.UseTextOptions = true;
            this.colDonVi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDonVi.Caption = "Đơn vị";
            this.colDonVi.FieldName = "DonVi";
            this.colDonVi.Name = "colDonVi";
            this.colDonVi.Visible = true;
            this.colDonVi.VisibleIndex = 2;
            this.colDonVi.Width = 44;
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
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 3;
            this.colDonGia.Width = 79;
            // 
            // colSoLuong
            // 
            this.colSoLuong.AppearanceCell.Options.UseTextOptions = true;
            this.colSoLuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 4;
            this.colSoLuong.Width = 56;
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
            this.colThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 5;
            this.colThanhTien.Width = 89;
            // 
            // colTrongBH
            // 
            this.colTrongBH.Caption = "Trong DMBH";
            this.colTrongBH.ColumnEdit = this.luptrongDM;
            this.colTrongBH.FieldName = "TrongBH";
            this.colTrongBH.Name = "colTrongBH";
            this.colTrongBH.Visible = true;
            this.colTrongBH.VisibleIndex = 6;
            this.colTrongBH.Width = 82;
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
            // colSua
            // 
            this.colSua.Caption = "Sửa";
            this.colSua.ColumnEdit = this.htp_Sua;
            this.colSua.Name = "colSua";
            this.colSua.OptionsColumn.AllowFocus = false;
            this.colSua.OptionsColumn.ReadOnly = true;
            this.colSua.Visible = true;
            this.colSua.VisibleIndex = 7;
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
            // col_makp
            // 
            this.col_makp.Caption = "Khoa|Phòng";
            this.col_makp.ColumnEdit = this.lup_MaKP;
            this.col_makp.FieldName = "MaKP";
            this.col_makp.Name = "col_makp";
            this.col_makp.Visible = true;
            this.col_makp.VisibleIndex = 0;
            this.col_makp.Width = 132;
            // 
            // lup_MaKP
            // 
            this.lup_MaKP.AutoHeight = false;
            this.lup_MaKP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MaKP.DisplayMember = "TenKP";
            this.lup_MaKP.Name = "lup_MaKP";
            this.lup_MaKP.NullText = "";
            this.lup_MaKP.ValueMember = "MaKP";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 100);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(827, 353);
            this.panelControl3.TabIndex = 19;
            // 
            // frm_mau13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 492);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_mau13";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mẫu 13";
            this.Load += new System.EventHandler(this.frm_mau13_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_TrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_NgoaiDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chl_KhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThanhToan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptrongDM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.htp_Sua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraGrid.GridControl grcThanhToan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaDVtt;
        private DevExpress.XtraGrid.Columns.GridColumn colDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colTrongBH;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luptrongDM;
        private DevExpress.XtraGrid.Columns.GridColumn colSua;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit htp_Sua;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn col_makp;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lup_MaKP;
        private DevExpress.XtraEditors.CheckedListBoxControl chl_KhoaPhong;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chk_TrongDM;
        private DevExpress.XtraEditors.CheckEdit chk_NgoaiDM;
    }
}