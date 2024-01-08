namespace QLBV.FormThamSo
{
    partial class frm_KetNoiXaPhuong
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
            this.lupBenhVien = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_bak = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnChonDuongDan = new DevExpress.XtraEditors.SimpleButton();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.grcDSBenhNhan = new DevExpress.XtraGrid.GridControl();
            this.grvDSBenhNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckChon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTenBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaLK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayVao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayRa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMucHuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBNTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBHTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXemCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hplView = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnBoChon = new DevExpress.XtraEditors.SimpleButton();
            this.btnChonTatCa = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupBenhVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_bak.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lupBenhVien
            // 
            this.lupBenhVien.Location = new System.Drawing.Point(131, 7);
            this.lupBenhVien.Name = "lupBenhVien";
            this.lupBenhVien.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupBenhVien.Properties.Appearance.Options.UseFont = true;
            this.lupBenhVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupBenhVien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenBV", "Tên BV")});
            this.lupBenhVien.Properties.DisplayMember = "TenBV";
            this.lupBenhVien.Properties.NullText = "";
            this.lupBenhVien.Properties.ValueMember = "MaBV";
            this.lupBenhVien.Size = new System.Drawing.Size(155, 24);
            this.lupBenhVien.TabIndex = 0;
            this.lupBenhVien.EditValueChanged += new System.EventHandler(this.lupBenhVien_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(14, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Chọn nơi chuyển:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txt_bak);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.btnChonDuongDan);
            this.panelControl1.Controls.Add(this.txtFilePath);
            this.panelControl1.Controls.Add(this.lupBenhVien);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1077, 38);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(771, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 17);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "bak:";
            // 
            // txt_bak
            // 
            this.txt_bak.Location = new System.Drawing.Point(809, 7);
            this.txt_bak.Name = "txt_bak";
            this.txt_bak.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_bak.Properties.Appearance.Options.UseFont = true;
            this.txt_bak.Properties.ReadOnly = true;
            this.txt_bak.Size = new System.Drawing.Size(256, 24);
            this.txt_bak.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(298, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(134, 17);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Thư mục lấy dữ liệu:";
            // 
            // btnChonDuongDan
            // 
            this.btnChonDuongDan.Location = new System.Drawing.Point(713, 5);
            this.btnChonDuongDan.Name = "btnChonDuongDan";
            this.btnChonDuongDan.Size = new System.Drawing.Size(29, 23);
            this.btnChonDuongDan.TabIndex = 4;
            this.btnChonDuongDan.Text = "...";
            this.btnChonDuongDan.Click += new System.EventHandler(this.btnChonDuongDan_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(451, 7);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Properties.Appearance.Options.UseFont = true;
            this.txtFilePath.Properties.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(256, 24);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 38);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1077, 475);
            this.panelControl2.TabIndex = 3;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.grcDSBenhNhan);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1077, 422);
            this.panelControl4.TabIndex = 1;
            // 
            // grcDSBenhNhan
            // 
            this.grcDSBenhNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDSBenhNhan.Location = new System.Drawing.Point(0, 0);
            this.grcDSBenhNhan.MainView = this.grvDSBenhNhan;
            this.grcDSBenhNhan.Name = "grcDSBenhNhan";
            this.grcDSBenhNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ckChon,
            this.hplView});
            this.grcDSBenhNhan.Size = new System.Drawing.Size(1077, 422);
            this.grcDSBenhNhan.TabIndex = 0;
            this.grcDSBenhNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDSBenhNhan});
            // 
            // grvDSBenhNhan
            // 
            this.grvDSBenhNhan.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDSBenhNhan.Appearance.FooterPanel.Options.UseFont = true;
            this.grvDSBenhNhan.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDSBenhNhan.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDSBenhNhan.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDSBenhNhan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDSBenhNhan.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDSBenhNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colTenBN,
            this.colMaLK,
            this.colSoThe,
            this.colNgayVao,
            this.colNgayRa,
            this.colNgayTT,
            this.colMucHuong,
            this.colBNTT,
            this.colBHTT,
            this.colTongTien,
            this.colXemCT});
            this.grvDSBenhNhan.GridControl = this.grcDSBenhNhan;
            this.grvDSBenhNhan.Name = "grvDSBenhNhan";
            this.grvDSBenhNhan.OptionsView.ColumnAutoWidth = false;
            this.grvDSBenhNhan.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDSBenhNhan.OptionsView.EnableAppearanceOddRow = true;
            this.grvDSBenhNhan.OptionsView.ShowFooter = true;
            this.grvDSBenhNhan.OptionsView.ShowGroupPanel = false;
            this.grvDSBenhNhan.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvDSBenhNhan_RowCellClick);
            // 
            // colCheck
            // 
            this.colCheck.Caption = "Chọn";
            this.colCheck.ColumnEdit = this.ckChon;
            this.colCheck.FieldName = "Check";
            this.colCheck.Name = "colCheck";
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 50;
            // 
            // ckChon
            // 
            this.ckChon.AllowFocused = false;
            this.ckChon.AutoHeight = false;
            this.ckChon.Caption = "Chọn";
            this.ckChon.Name = "ckChon";
            // 
            // colTenBN
            // 
            this.colTenBN.Caption = "Tên BN";
            this.colTenBN.FieldName = "Ho_ten";
            this.colTenBN.Name = "colTenBN";
            this.colTenBN.OptionsColumn.AllowEdit = false;
            this.colTenBN.OptionsColumn.ReadOnly = true;
            this.colTenBN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colTenBN.Visible = true;
            this.colTenBN.VisibleIndex = 1;
            this.colTenBN.Width = 175;
            // 
            // colMaLK
            // 
            this.colMaLK.Caption = "Mã LK_XP";
            this.colMaLK.FieldName = "Ma_lk";
            this.colMaLK.Name = "colMaLK";
            // 
            // colSoThe
            // 
            this.colSoThe.Caption = "Số thẻ";
            this.colSoThe.FieldName = "Ma_the";
            this.colSoThe.Name = "colSoThe";
            this.colSoThe.OptionsColumn.AllowEdit = false;
            this.colSoThe.OptionsColumn.ReadOnly = true;
            this.colSoThe.Visible = true;
            this.colSoThe.VisibleIndex = 2;
            this.colSoThe.Width = 153;
            // 
            // colNgayVao
            // 
            this.colNgayVao.Caption = "Ngày vào viện";
            this.colNgayVao.FieldName = "Ngay_vao";
            this.colNgayVao.Name = "colNgayVao";
            this.colNgayVao.OptionsColumn.AllowEdit = false;
            this.colNgayVao.OptionsColumn.ReadOnly = true;
            this.colNgayVao.Visible = true;
            this.colNgayVao.VisibleIndex = 3;
            this.colNgayVao.Width = 77;
            // 
            // colNgayRa
            // 
            this.colNgayRa.Caption = "Ngày ra";
            this.colNgayRa.FieldName = "Ngay_ra";
            this.colNgayRa.Name = "colNgayRa";
            this.colNgayRa.OptionsColumn.AllowEdit = false;
            this.colNgayRa.OptionsColumn.ReadOnly = true;
            this.colNgayRa.Visible = true;
            this.colNgayRa.VisibleIndex = 4;
            this.colNgayRa.Width = 65;
            // 
            // colNgayTT
            // 
            this.colNgayTT.Caption = "Ngày TT";
            this.colNgayTT.FieldName = "Ngay_tt";
            this.colNgayTT.Name = "colNgayTT";
            this.colNgayTT.OptionsColumn.AllowEdit = false;
            this.colNgayTT.OptionsColumn.ReadOnly = true;
            this.colNgayTT.Visible = true;
            this.colNgayTT.VisibleIndex = 5;
            this.colNgayTT.Width = 65;
            // 
            // colMucHuong
            // 
            this.colMucHuong.Caption = "Mức hưởng";
            this.colMucHuong.FieldName = "Muc_huong";
            this.colMucHuong.Name = "colMucHuong";
            this.colMucHuong.OptionsColumn.AllowEdit = false;
            this.colMucHuong.OptionsColumn.ReadOnly = true;
            this.colMucHuong.Visible = true;
            this.colMucHuong.VisibleIndex = 6;
            this.colMucHuong.Width = 48;
            // 
            // colBNTT
            // 
            this.colBNTT.Caption = "Bệnh nhân thanh toán";
            this.colBNTT.DisplayFormat.FormatString = "{0:##,###.##}";
            this.colBNTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBNTT.FieldName = "T_bntt";
            this.colBNTT.Name = "colBNTT";
            this.colBNTT.OptionsColumn.AllowEdit = false;
            this.colBNTT.OptionsColumn.ReadOnly = true;
            this.colBNTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T_bntt", "{0:##,###.##}")});
            this.colBNTT.Visible = true;
            this.colBNTT.VisibleIndex = 7;
            this.colBNTT.Width = 111;
            // 
            // colBHTT
            // 
            this.colBHTT.Caption = "BHTT";
            this.colBHTT.DisplayFormat.FormatString = "{0:##,###.##}";
            this.colBHTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBHTT.FieldName = "T_bhtt";
            this.colBHTT.Name = "colBHTT";
            this.colBHTT.OptionsColumn.AllowEdit = false;
            this.colBHTT.OptionsColumn.ReadOnly = true;
            this.colBHTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T_bhtt", "{0:##,###.##}")});
            this.colBHTT.Visible = true;
            this.colBHTT.VisibleIndex = 8;
            this.colBHTT.Width = 121;
            // 
            // colTongTien
            // 
            this.colTongTien.Caption = "Tổng tiền";
            this.colTongTien.DisplayFormat.FormatString = "{0:##,###.##}";
            this.colTongTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colTongTien.FieldName = "T_tongchi";
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.OptionsColumn.AllowEdit = false;
            this.colTongTien.OptionsColumn.ReadOnly = true;
            this.colTongTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T_tongchi", "{0:##,###.##}")});
            this.colTongTien.Visible = true;
            this.colTongTien.VisibleIndex = 9;
            this.colTongTien.Width = 126;
            // 
            // colXemCT
            // 
            this.colXemCT.Caption = "Xem chi tiết";
            this.colXemCT.ColumnEdit = this.hplView;
            this.colXemCT.Name = "colXemCT";
            this.colXemCT.OptionsColumn.AllowEdit = false;
            this.colXemCT.OptionsColumn.ReadOnly = true;
            this.colXemCT.Visible = true;
            this.colXemCT.VisibleIndex = 10;
            // 
            // hplView
            // 
            this.hplView.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hplView.Appearance.Options.UseFont = true;
            this.hplView.AutoHeight = false;
            this.hplView.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.hplView.Caption = "Xem";
            this.hplView.Name = "hplView";
            this.hplView.NullText = "Xem";
            this.hplView.Click += new System.EventHandler(this.hplView_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnAdd);
            this.panelControl3.Controls.Add(this.btnBoChon);
            this.panelControl3.Controls.Add(this.btnChonTatCa);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 422);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1077, 53);
            this.panelControl3.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(147, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(119, 45);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Thêm vào CSDL";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBoChon
            // 
            this.btnBoChon.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoChon.Appearance.Options.UseFont = true;
            this.btnBoChon.Location = new System.Drawing.Point(14, 28);
            this.btnBoChon.Name = "btnBoChon";
            this.btnBoChon.Size = new System.Drawing.Size(75, 23);
            this.btnBoChon.TabIndex = 1;
            this.btnBoChon.Text = "Bỏ chọn";
            this.btnBoChon.Click += new System.EventHandler(this.btnBoChon_Click);
            // 
            // btnChonTatCa
            // 
            this.btnChonTatCa.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonTatCa.Appearance.Options.UseFont = true;
            this.btnChonTatCa.Location = new System.Drawing.Point(14, 4);
            this.btnChonTatCa.Name = "btnChonTatCa";
            this.btnChonTatCa.Size = new System.Drawing.Size(75, 23);
            this.btnChonTatCa.TabIndex = 0;
            this.btnChonTatCa.Text = "Chọn tât cả";
            this.btnChonTatCa.Click += new System.EventHandler(this.btnChonTatCa_Click);
            // 
            // frm_KetNoiXaPhuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 513);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_KetNoiXaPhuong";
            this.Text = "Kết nối xã phường";
            this.Load += new System.EventHandler(this.frm_LayBenhNhanXaPhuongcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupBenhVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_bak.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDSBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lupBenhVien;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl grcDSBenhNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDSBenhNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaLK;
        private DevExpress.XtraGrid.Columns.GridColumn colSoThe;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayVao;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayRa;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayTT;
        private DevExpress.XtraGrid.Columns.GridColumn colMucHuong;
        private DevExpress.XtraGrid.Columns.GridColumn colBNTT;
        private DevExpress.XtraGrid.Columns.GridColumn colBHTT;
        private DevExpress.XtraGrid.Columns.GridColumn colTongTien;
        private DevExpress.XtraGrid.Columns.GridColumn colXemCT;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit hplView;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnBoChon;
        private DevExpress.XtraEditors.SimpleButton btnChonTatCa;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnChonDuongDan;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_bak;
    }
}