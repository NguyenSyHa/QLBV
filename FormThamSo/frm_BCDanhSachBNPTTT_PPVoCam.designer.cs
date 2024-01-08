namespace QLBV.FormThamSo
{
    partial class frm_BCDanhSachBNPTTT_PPVoCam
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
            this.components = new System.ComponentModel.Container();
            this.deDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.deTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.radTrongDM = new DevExpress.XtraEditors.RadioGroup();
            this.cbo_NoiTru = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbo_DoiTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rgNgayBC = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgNgayBC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // deDenNgay
            // 
            this.deDenNgay.EditValue = null;
            this.deDenNgay.Location = new System.Drawing.Point(96, 46);
            this.deDenNgay.Name = "deDenNgay";
            this.deDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deDenNgay.Properties.Appearance.Options.UseFont = true;
            this.deDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Size = new System.Drawing.Size(258, 24);
            this.deDenNgay.TabIndex = 1;
            // 
            // deTuNgay
            // 
            this.deTuNgay.EditValue = null;
            this.deTuNgay.Location = new System.Drawing.Point(96, 12);
            this.deTuNgay.Name = "deTuNgay";
            this.deTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTuNgay.Properties.Appearance.Options.UseFont = true;
            this.deTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Size = new System.Drawing.Size(258, 24);
            this.deTuNgay.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(5, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 17);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 17);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Location = new System.Drawing.Point(370, 217);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(86, 23);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Location = new System.Drawing.Point(255, 217);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(99, 23);
            this.btnInBC.TabIndex = 6;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl9.Location = new System.Drawing.Point(6, 187);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(87, 17);
            this.labelControl9.TabIndex = 82;
            this.labelControl9.Text = "Trong|Ng DM:";
            // 
            // radTrongDM
            // 
            this.radTrongDM.Location = new System.Drawing.Point(96, 181);
            this.radTrongDM.Name = "radTrongDM";
            this.radTrongDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTrongDM.Properties.Appearance.Options.UseFont = true;
            this.radTrongDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radTrongDM.Size = new System.Drawing.Size(258, 29);
            this.radTrongDM.TabIndex = 4;
            // 
            // cbo_NoiTru
            // 
            this.cbo_NoiTru.EditValue = "Tất cả";
            this.cbo_NoiTru.Location = new System.Drawing.Point(96, 149);
            this.cbo_NoiTru.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_NoiTru.Name = "cbo_NoiTru";
            this.cbo_NoiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_NoiTru.Properties.Appearance.Options.UseFont = true;
            this.cbo_NoiTru.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_NoiTru.Properties.Items.AddRange(new object[] {
            "Ngoại trú",
            "Nội trú",
            "Tất cả"});
            this.cbo_NoiTru.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_NoiTru.Size = new System.Drawing.Size(258, 24);
            this.cbo_NoiTru.TabIndex = 3;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(6, 152);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(82, 17);
            this.labelControl7.TabIndex = 81;
            this.labelControl7.Text = "Nội|Ngoại trú:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(5, 121);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 17);
            this.labelControl5.TabIndex = 80;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // cbo_DoiTuong
            // 
            this.cbo_DoiTuong.EditValue = "99";
            this.cbo_DoiTuong.Location = new System.Drawing.Point(96, 117);
            this.cbo_DoiTuong.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_DoiTuong.Name = "cbo_DoiTuong";
            this.cbo_DoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_DoiTuong.Properties.Appearance.Options.UseFont = true;
            this.cbo_DoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_DoiTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDDTBN", "IDDTBN")});
            this.cbo_DoiTuong.Properties.DisplayMember = "DTBN1";
            this.cbo_DoiTuong.Properties.NullText = "";
            this.cbo_DoiTuong.Properties.PopupSizeable = false;
            this.cbo_DoiTuong.Properties.ValueMember = "IDDTBN";
            this.cbo_DoiTuong.Size = new System.Drawing.Size(258, 24);
            this.cbo_DoiTuong.TabIndex = 2;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcKhoaphong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.Location = new System.Drawing.Point(370, 12);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.Size = new System.Drawing.Size(277, 198);
            this.grcKhoaphong.TabIndex = 5;
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
            // rgNgayBC
            // 
            this.rgNgayBC.Location = new System.Drawing.Point(96, 79);
            this.rgNgayBC.Name = "rgNgayBC";
            this.rgNgayBC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgNgayBC.Properties.Appearance.Options.UseFont = true;
            this.rgNgayBC.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngày thực hiện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngày ra viện")});
            this.rgNgayBC.Size = new System.Drawing.Size(258, 25);
            this.rgNgayBC.TabIndex = 83;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(6, 86);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 17);
            this.labelControl3.TabIndex = 80;
            this.labelControl3.Text = "Ngày BC:";
            // 
            // frm_BCDanhSachBNPTTT_PPVoCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 247);
            this.Controls.Add(this.rgNgayBC);
            this.Controls.Add(this.grcKhoaphong);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.radTrongDM);
            this.Controls.Add(this.cbo_NoiTru);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.cbo_DoiTuong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.deDenNgay);
            this.Controls.Add(this.deTuNgay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BCDanhSachBNPTTT_PPVoCam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách BN PTTT sử dụng PP vô cảm";
            this.Load += new System.EventHandler(this.frm_BCDanhSachBNPTTT_PPVoCam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgNgayBC.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit deDenNgay;
        private DevExpress.XtraEditors.DateEdit deTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.RadioGroup radTrongDM;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_NoiTru;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cbo_DoiTuong;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.RadioGroup rgNgayBC;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}