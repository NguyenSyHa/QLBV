namespace QLBV.FormThamSo
{
    partial class frm_BC_ChucNangHoHap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_ChucNangHoHap));
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.ChkNgoaiGio = new System.Windows.Forms.CheckBox();
            this.ChkTrongGio = new System.Windows.Forms.CheckBox();
            this.NgoaiGio = new DevExpress.XtraEditors.CheckEdit();
            this.TrongGio = new DevExpress.XtraEditors.CheckEdit();
            this.chkBNcoCLS = new DevExpress.XtraEditors.CheckEdit();
            this.chkBNkhongCLS = new DevExpress.XtraEditors.CheckEdit();
            this.radBN = new DevExpress.XtraEditors.RadioGroup();
            this.radMau = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboTKBN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupDichVu = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NgoaiGio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrongGio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNcoCLS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNkhongCLS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDichVu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(10, 378);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(392, 14);
            this.labelControl3.TabIndex = 134;
            this.labelControl3.Text = "* Ghi chú: Bệnh nhân không qua chỉ định không lấy được trong ngoài giờ hành chính" +
    "";
            this.labelControl3.ToolTip = "Chọn DV";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.ChkNgoaiGio);
            this.groupControl2.Controls.Add(this.ChkTrongGio);
            this.groupControl2.Controls.Add(this.NgoaiGio);
            this.groupControl2.Controls.Add(this.TrongGio);
            this.groupControl2.Location = new System.Drawing.Point(342, 229);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(342, 55);
            this.groupControl2.TabIndex = 133;
            this.groupControl2.Text = "Trong| Ngoài giờ hành chính";
            // 
            // ChkNgoaiGio
            // 
            this.ChkNgoaiGio.AutoSize = true;
            this.ChkNgoaiGio.Checked = true;
            this.ChkNgoaiGio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkNgoaiGio.Location = new System.Drawing.Point(170, 24);
            this.ChkNgoaiGio.Name = "ChkNgoaiGio";
            this.ChkNgoaiGio.Size = new System.Drawing.Size(70, 17);
            this.ChkNgoaiGio.TabIndex = 8;
            this.ChkNgoaiGio.Text = "Ngoài giờ";
            this.ChkNgoaiGio.UseVisualStyleBackColor = true;
            // 
            // ChkTrongGio
            // 
            this.ChkTrongGio.AutoSize = true;
            this.ChkTrongGio.Checked = true;
            this.ChkTrongGio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkTrongGio.Location = new System.Drawing.Point(29, 24);
            this.ChkTrongGio.Name = "ChkTrongGio";
            this.ChkTrongGio.Size = new System.Drawing.Size(71, 17);
            this.ChkTrongGio.TabIndex = 7;
            this.ChkTrongGio.Text = "Trong giờ";
            this.ChkTrongGio.UseVisualStyleBackColor = true;
            // 
            // NgoaiGio
            // 
            this.NgoaiGio.EditValue = true;
            this.NgoaiGio.Location = new System.Drawing.Point(185, 57);
            this.NgoaiGio.Name = "NgoaiGio";
            this.NgoaiGio.Properties.Caption = "Ngoài giờ";
            this.NgoaiGio.Size = new System.Drawing.Size(75, 19);
            this.NgoaiGio.TabIndex = 6;
            // 
            // TrongGio
            // 
            this.TrongGio.EditValue = true;
            this.TrongGio.Location = new System.Drawing.Point(7, 57);
            this.TrongGio.Name = "TrongGio";
            this.TrongGio.Properties.Caption = "Trong giờ";
            this.TrongGio.Size = new System.Drawing.Size(75, 19);
            this.TrongGio.TabIndex = 5;
            // 
            // chkBNcoCLS
            // 
            this.chkBNcoCLS.EditValue = true;
            this.chkBNcoCLS.Location = new System.Drawing.Point(340, 175);
            this.chkBNcoCLS.Name = "chkBNcoCLS";
            this.chkBNcoCLS.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkBNcoCLS.Properties.Appearance.Options.UseFont = true;
            this.chkBNcoCLS.Properties.Caption = "Thống kê BN thực hiện có qua chỉ định CLS.";
            this.chkBNcoCLS.Size = new System.Drawing.Size(344, 21);
            this.chkBNcoCLS.TabIndex = 121;
            // 
            // chkBNkhongCLS
            // 
            this.chkBNkhongCLS.Location = new System.Drawing.Point(340, 202);
            this.chkBNkhongCLS.Name = "chkBNkhongCLS";
            this.chkBNkhongCLS.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkBNkhongCLS.Properties.Appearance.Options.UseFont = true;
            this.chkBNkhongCLS.Properties.Caption = "Thống kê BN thực hiện không qua chỉ định CLS.";
            this.chkBNkhongCLS.Size = new System.Drawing.Size(344, 21);
            this.chkBNkhongCLS.TabIndex = 122;
            this.chkBNkhongCLS.CheckedChanged += new System.EventHandler(this.chkBNkhongCLS_CheckedChanged);
            // 
            // radBN
            // 
            this.radBN.Location = new System.Drawing.Point(342, 100);
            this.radBN.Name = "radBN";
            this.radBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radBN.Properties.Appearance.Options.UseFont = true;
            this.radBN.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả BN"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN Ngoại trú")});
            this.radBN.Size = new System.Drawing.Size(345, 32);
            this.radBN.TabIndex = 119;
            // 
            // radMau
            // 
            this.radMau.Location = new System.Drawing.Point(342, 138);
            this.radMau.Name = "radMau";
            this.radMau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radMau.Properties.Appearance.Options.UseFont = true;
            this.radMau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In mẫu A4"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "In mẫu A3")});
            this.radMau.Size = new System.Drawing.Size(345, 31);
            this.radMau.TabIndex = 120;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(266, 47);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 17);
            this.labelControl1.TabIndex = 129;
            this.labelControl1.Text = "Thống kê:";
            this.labelControl1.ToolTip = "Thống kê bệnh nhân";
            // 
            // cboTKBN
            // 
            this.cboTKBN.EditValue = "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)";
            this.cboTKBN.Location = new System.Drawing.Point(342, 44);
            this.cboTKBN.Name = "cboTKBN";
            this.cboTKBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTKBN.Properties.Appearance.Options.UseFont = true;
            this.cboTKBN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTKBN.Properties.Items.AddRange(new object[] {
            "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)",
            "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)"});
            this.cboTKBN.Size = new System.Drawing.Size(346, 26);
            this.cboTKBN.TabIndex = 117;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(8, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(245, 358);
            this.groupControl1.TabIndex = 128;
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
            this.grcKhoaphong.Size = new System.Drawing.Size(241, 354);
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
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(602, 290);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(86, 23);
            this.btnHuy.TabIndex = 124;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(483, 290);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(99, 23);
            this.btnInBC.TabIndex = 123;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(480, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 127;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(264, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 125;
            this.label2.Text = "Từ ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = null;
            this.dtpDenNgay.Location = new System.Drawing.Point(555, 13);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Size = new System.Drawing.Size(132, 26);
            this.dtpDenNgay.TabIndex = 116;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = null;
            this.dtpTuNgay.Location = new System.Drawing.Point(342, 12);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Size = new System.Drawing.Size(132, 26);
            this.dtpTuNgay.TabIndex = 115;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(266, 75);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 17);
            this.labelControl2.TabIndex = 137;
            this.labelControl2.Text = "Chọn DV:";
            this.labelControl2.ToolTip = "Chọn DV";
            // 
            // lupDichVu
            // 
            this.lupDichVu.Location = new System.Drawing.Point(342, 72);
            this.lupDichVu.Name = "lupDichVu";
            this.lupDichVu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDichVu.Properties.Appearance.Options.UseFont = true;
            this.lupDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDichVu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("madv", "Mã"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tendv", "Tên dịch vụ")});
            this.lupDichVu.Properties.DisplayMember = "madv";
            this.lupDichVu.Properties.NullText = "Tất cả";
            this.lupDichVu.Properties.ValueMember = "tendv";
            this.lupDichVu.Size = new System.Drawing.Size(344, 26);
            this.lupDichVu.TabIndex = 136;
            // 
            // frm_BC_ChucNangHoHap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 395);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lupDichVu);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.chkBNcoCLS);
            this.Controls.Add(this.chkBNkhongCLS);
            this.Controls.Add(this.radBN);
            this.Controls.Add(this.radMau);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboTKBN);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Name = "frm_BC_ChucNangHoHap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chức năng hô hấp";
            this.Load += new System.EventHandler(this.frm_BC_ChucNangHoHap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NgoaiGio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrongGio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNcoCLS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNkhongCLS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDichVu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit NgoaiGio;
        private DevExpress.XtraEditors.CheckEdit TrongGio;
        private DevExpress.XtraEditors.CheckEdit chkBNcoCLS;
        private DevExpress.XtraEditors.CheckEdit chkBNkhongCLS;
        private DevExpress.XtraEditors.RadioGroup radBN;
        private DevExpress.XtraEditors.RadioGroup radMau;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboTKBN;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private System.Windows.Forms.CheckBox ChkNgoaiGio;
        private System.Windows.Forms.CheckBox ChkTrongGio;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lupDichVu;
    }
}