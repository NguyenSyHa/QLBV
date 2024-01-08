namespace QLBV.FormThamSo
{
    partial class Frm_BcHoatDongDTri_CL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BcHoatDongDTri_CL));
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboTKBN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.radIn = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.csg = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboDTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.rdHTThanhToan = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.ckcSoNgaydt = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdHTThanhToan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSoNgaydt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(370, 443);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 36);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(267, 443);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 36);
            this.btnOK.TabIndex = 4;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(96, 32);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(350, 167);
            this.groupControl1.TabIndex = 116;
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
            this.grcKhoaphong.Size = new System.Drawing.Size(346, 163);
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
            this.grvKhoaphong.ViewCaption = "Khoa phòng điều trị";
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
            this.Chọn.Width = 45;
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên K.phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 122;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(241, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 115;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(1, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 114;
            this.label2.Text = "Từ ngày:";
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(96, 3);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(132, 26);
            this.lupTuNgay.TabIndex = 0;
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(314, 3);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(132, 26);
            this.lupDenNgay.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(4, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 17);
            this.labelControl2.TabIndex = 118;
            this.labelControl2.Text = "Khoa|Phòng:";
            this.labelControl2.ToolTip = "Chọn khoa phòng";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(96, 313);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(266, 19);
            this.labelControl1.TabIndex = 118;
            this.labelControl1.Text = "Chọn cách hiển thị thời gian lên báo cáo:";
            this.labelControl1.ToolTip = "Chọn khoa phòng";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(4, 227);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(68, 17);
            this.labelControl3.TabIndex = 144;
            this.labelControl3.Text = "Thống kê:";
            this.labelControl3.ToolTip = "Thống kê bệnh nhân";
            // 
            // cboTKBN
            // 
            this.cboTKBN.EditValue = "BN đang điều trị tại khoa (BN vào khoa - BN đã ra)";
            this.cboTKBN.Location = new System.Drawing.Point(96, 224);
            this.cboTKBN.Name = "cboTKBN";
            this.cboTKBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTKBN.Properties.Appearance.Options.UseFont = true;
            this.cboTKBN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTKBN.Properties.Items.AddRange(new object[] {
            "BN đang điều trị tại khoa (BN vào khoa - BN đã ra)",
            "BN điều trị tại khoa đã ra viện (Tính theo ngày ra viện)"});
            this.cboTKBN.Size = new System.Drawing.Size(350, 26);
            this.cboTKBN.TabIndex = 2;
            // 
            // radIn
            // 
            this.radIn.Location = new System.Drawing.Point(96, 335);
            this.radIn.Name = "radIn";
            this.radIn.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radIn.Properties.Appearance.Options.UseFont = true;
            this.radIn.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In Từ ngày - Đến ngày"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In Quý"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In 6 tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In 9 tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In cả năm")});
            this.radIn.Size = new System.Drawing.Size(350, 75);
            this.radIn.TabIndex = 151;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(96, 202);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(151, 19);
            this.labelControl4.TabIndex = 152;
            this.labelControl4.Text = "Công suất giường KH :";
            this.labelControl4.ToolTip = "Chọn khoa phòng";
            // 
            // csg
            // 
            this.csg.Location = new System.Drawing.Point(278, 203);
            this.csg.Name = "csg";
            this.csg.Properties.Caption = "";
            this.csg.Size = new System.Drawing.Size(15, 19);
            this.csg.TabIndex = 153;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(4, 256);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(89, 17);
            this.labelControl5.TabIndex = 154;
            this.labelControl5.Text = "Đ.Tượng BN:";
            this.labelControl5.ToolTip = "Thống kê bệnh nhân";
            // 
            // cboDTuong
            // 
            this.cboDTuong.EditValue = "Tất cả";
            this.cboDTuong.Location = new System.Drawing.Point(96, 253);
            this.cboDTuong.Name = "cboDTuong";
            this.cboDTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDTuong.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.cboDTuong.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.cboDTuong.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.cboDTuong.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.cboDTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDDTBN", 5, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đ.Tượng")});
            this.cboDTuong.Properties.DisplayMember = "DTBN1";
            this.cboDTuong.Properties.NullText = "";
            this.cboDTuong.Properties.PopupSizeable = false;
            this.cboDTuong.Properties.ValueMember = "IDDTBN";
            this.cboDTuong.Size = new System.Drawing.Size(350, 24);
            this.cboDTuong.TabIndex = 155;
            // 
            // rdHTThanhToan
            // 
            this.rdHTThanhToan.Location = new System.Drawing.Point(96, 280);
            this.rdHTThanhToan.Name = "rdHTThanhToan";
            this.rdHTThanhToan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdHTThanhToan.Properties.Appearance.Options.UseFont = true;
            this.rdHTThanhToan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trực tiếp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "C.Khoản"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.rdHTThanhToan.Size = new System.Drawing.Size(350, 27);
            this.rdHTThanhToan.TabIndex = 529;
            this.rdHTThanhToan.SelectedIndexChanged += new System.EventHandler(this.rdHTThanhToan_SelectedIndexChanged);
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Location = new System.Drawing.Point(4, 285);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(78, 17);
            this.labelControl13.TabIndex = 527;
            this.labelControl13.Text = "HT T.Toán:";
            // 
            // ckcSoNgaydt
            // 
            this.ckcSoNgaydt.Location = new System.Drawing.Point(96, 416);
            this.ckcSoNgaydt.Name = "ckcSoNgaydt";
            this.ckcSoNgaydt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcSoNgaydt.Properties.Appearance.Options.UseFont = true;
            this.ckcSoNgaydt.Properties.Caption = "Số ngày điều trị cộng 1";
            this.ckcSoNgaydt.Size = new System.Drawing.Size(340, 20);
            this.ckcSoNgaydt.TabIndex = 530;
            // 
            // Frm_BcHoatDongDTri_CL
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 483);
            this.Controls.Add(this.ckcSoNgaydt);
            this.Controls.Add(this.rdHTThanhToan);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.cboDTuong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.csg);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.radIn);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboTKBN);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BcHoatDongDTri_CL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo hoạt động khoa điều trị";
            this.Load += new System.EventHandler(this.Frm_BcHoatDongDtri_TH04_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdHTThanhToan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSoNgaydt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cboTKBN;
        private DevExpress.XtraEditors.RadioGroup radIn;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit csg;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboDTuong;
        private DevExpress.XtraEditors.RadioGroup rdHTThanhToan;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.CheckEdit ckcSoNgaydt;
    }
}