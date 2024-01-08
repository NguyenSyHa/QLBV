namespace QLBV.FormThamSo
{
    partial class frm_SoTHCLS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SoTHCLS));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.radThanhtoan = new DevExpress.XtraEditors.RadioGroup();
            this.radDanhmuc = new DevExpress.XtraEditors.RadioGroup();
            this.cboInBia = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ckcSoSATim = new DevExpress.XtraEditors.CheckEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.radBN = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lupChonSo = new DevExpress.XtraEditors.LookUpEdit();
            this.lupKpTH = new DevExpress.XtraEditors.LookUpEdit();
            this.rgDTuong = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.deDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.deTuNgay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radThanhtoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDanhmuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInBia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSoSATim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupChonSo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKpTH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.radThanhtoan);
            this.panelControl1.Controls.Add(this.radDanhmuc);
            this.panelControl1.Controls.Add(this.cboInBia);
            this.panelControl1.Controls.Add(this.ckcSoSATim);
            this.panelControl1.Controls.Add(this.btnHuy);
            this.panelControl1.Controls.Add(this.btnInBC);
            this.panelControl1.Controls.Add(this.radBN);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.lupKpTH);
            this.panelControl1.Controls.Add(this.rgDTuong);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.deDenNgay);
            this.panelControl1.Controls.Add(this.deTuNgay);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(611, 410);
            this.panelControl1.TabIndex = 2;
            // 
            // radThanhtoan
            // 
            this.radThanhtoan.Location = new System.Drawing.Point(301, 289);
            this.radThanhtoan.Name = "radThanhtoan";
            this.radThanhtoan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Đã TT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chưa TT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radThanhtoan.Size = new System.Drawing.Size(284, 27);
            this.radThanhtoan.TabIndex = 110;
            // 
            // radDanhmuc
            // 
            this.radDanhmuc.Location = new System.Drawing.Point(301, 252);
            this.radDanhmuc.Name = "radDanhmuc";
            this.radDanhmuc.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radDanhmuc.Size = new System.Drawing.Size(284, 30);
            this.radDanhmuc.TabIndex = 109;
            // 
            // cboInBia
            // 
            this.cboInBia.EditValue = "In tờ bìa";
            this.cboInBia.Location = new System.Drawing.Point(301, 226);
            this.cboInBia.Name = "cboInBia";
            this.cboInBia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboInBia.Properties.Items.AddRange(new object[] {
            "Tờ bìa kiểu 1",
            "Tờ bìa kiểu 2"});
            this.cboInBia.Properties.NullText = "In tờ bìa (A3)";
            this.cboInBia.Size = new System.Drawing.Size(128, 20);
            this.cboInBia.TabIndex = 108;
            this.cboInBia.EditValueChanged += new System.EventHandler(this.cboInBia_EditValueChanged);
            // 
            // ckcSoSATim
            // 
            this.ckcSoSATim.Location = new System.Drawing.Point(237, 69);
            this.ckcSoSATim.Name = "ckcSoSATim";
            this.ckcSoSATim.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcSoSATim.Properties.Appearance.Options.UseFont = true;
            this.ckcSoSATim.Properties.Caption = "Sổ siêu âm doppler tim";
            this.ckcSoSATim.Size = new System.Drawing.Size(182, 19);
            this.ckcSoSATim.TabIndex = 106;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(301, 334);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(127, 23);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(134, 334);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(128, 23);
            this.btnInBC.TabIndex = 5;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // radBN
            // 
            this.radBN.Location = new System.Drawing.Point(301, 190);
            this.radBN.Name = "radBN";
            this.radBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radBN.Properties.Appearance.Options.UseFont = true;
            this.radBN.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radBN.Size = new System.Drawing.Size(284, 29);
            this.radBN.TabIndex = 4;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcKhoaphong);
            this.groupControl2.Location = new System.Drawing.Point(5, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(228, 271);
            this.groupControl2.TabIndex = 105;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcKhoaphong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.Location = new System.Drawing.Point(2, 2);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.Size = new System.Drawing.Size(224, 267);
            this.grcKhoaphong.TabIndex = 0;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.OptionsView.ShowViewCaption = true;
            this.grvKhoaphong.ViewCaption = "Khoa Phòng chỉ định";
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging);
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 40;
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên K.phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 190;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lupChonSo);
            this.groupControl1.Location = new System.Drawing.Point(237, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(348, 61);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Chọn sổ (Sổ này chỉ thống kê BN có qua CĐ CLS)";
            // 
            // lupChonSo
            // 
            this.lupChonSo.Location = new System.Drawing.Point(5, 24);
            this.lupChonSo.Name = "lupChonSo";
            this.lupChonSo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupChonSo.Properties.Appearance.Options.UseFont = true;
            this.lupChonSo.Properties.Appearance.Options.UseTextOptions = true;
            this.lupChonSo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lupChonSo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupChonSo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", 10, "IDTN"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", 100, "Tên sổ")});
            this.lupChonSo.Properties.DisplayMember = "Ten";
            this.lupChonSo.Properties.NullText = "Chọn sổ";
            this.lupChonSo.Properties.ValueMember = "ID";
            this.lupChonSo.Size = new System.Drawing.Size(338, 24);
            this.lupChonSo.TabIndex = 0;
            this.lupChonSo.EditValueChanged += new System.EventHandler(this.lupChonSo_EditValueChanged);
            // 
            // lupKpTH
            // 
            this.lupKpTH.Location = new System.Drawing.Point(301, 160);
            this.lupKpTH.Name = "lupKpTH";
            this.lupKpTH.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKpTH.Properties.Appearance.Options.UseFont = true;
            this.lupKpTH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKpTH.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 150, "Tên Khoa|Phòng")});
            this.lupKpTH.Properties.DisplayMember = "TenKP";
            this.lupKpTH.Properties.NullText = "";
            this.lupKpTH.Properties.ValueMember = "MaKP";
            this.lupKpTH.Size = new System.Drawing.Size(284, 20);
            this.lupKpTH.TabIndex = 3;
            // 
            // rgDTuong
            // 
            this.rgDTuong.Location = new System.Drawing.Point(301, 123);
            this.rgDTuong.Name = "rgDTuong";
            this.rgDTuong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgDTuong.Properties.Appearance.Options.UseFont = true;
            this.rgDTuong.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "KSK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rgDTuong.Size = new System.Drawing.Size(284, 27);
            this.rgDTuong.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(414, 98);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày: ";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(239, 158);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 28);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "K.Phòng\r\nT.Hiện:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(237, 129);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(61, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Đ.Tượng: ";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(237, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Từ ngày: ";
            // 
            // deDenNgay
            // 
            this.deDenNgay.EditValue = null;
            this.deDenNgay.Location = new System.Drawing.Point(486, 95);
            this.deDenNgay.Name = "deDenNgay";
            this.deDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deDenNgay.Properties.Appearance.Options.UseFont = true;
            this.deDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Size = new System.Drawing.Size(99, 20);
            this.deDenNgay.TabIndex = 1;
            // 
            // deTuNgay
            // 
            this.deTuNgay.EditValue = null;
            this.deTuNgay.Location = new System.Drawing.Point(301, 95);
            this.deTuNgay.Name = "deTuNgay";
            this.deTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTuNgay.Properties.Appearance.Options.UseFont = true;
            this.deTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Size = new System.Drawing.Size(107, 20);
            this.deTuNgay.TabIndex = 0;
            // 
            // frm_SoTHCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 410);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_SoTHCLS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ tổng hợp Cận Lâm Sàng";
            this.Load += new System.EventHandler(this.frm_SoTHCLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radThanhtoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDanhmuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInBia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSoSATim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupChonSo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKpTH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deDenNgay;
        private DevExpress.XtraEditors.DateEdit deTuNgay;
        private DevExpress.XtraEditors.RadioGroup rgDTuong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupKpTH;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.RadioGroup radBN;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.CheckEdit ckcSoSATim;
        private DevExpress.XtraEditors.LookUpEdit lupChonSo;
        private DevExpress.XtraEditors.ComboBoxEdit cboInBia;
        private DevExpress.XtraEditors.RadioGroup radDanhmuc;
        private DevExpress.XtraEditors.RadioGroup radThanhtoan;
    }
}