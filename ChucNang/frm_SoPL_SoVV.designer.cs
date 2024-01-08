namespace QLBV.FormThamSo
{
    partial class frm_SoPL_SoVV
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.rdNoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dtNgayThang = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupKP = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoVV = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupKPtimKiem = new DevExpress.XtraEditors.LookUpEdit();
            this.grc_SoVV = new DevExpress.XtraGrid.GridControl();
            this.grv_SoVV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupKPHT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSoVV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayThang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiTru = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupNoiNgoaiTru = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lupPLoai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayThang.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoVV.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPtimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grc_SoVV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_SoVV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPHT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNoiNgoaiTru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupPLoai.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(657, 447);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupControl1);
            this.tabPage1.Controls.Add(this.rdNoiNgoaiTru);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.dtNgayThang);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.lupKP);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.txtSoVV);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(649, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lấy số";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnDelete);
            this.groupControl1.Controls.Add(this.btnNew);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.btnSua);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(378, 354);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(268, 64);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(186, 24);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 32);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.Enabled = false;
            this.btnNew.Location = new System.Drawing.Point(3, 24);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(61, 32);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "Mới";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(125, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 32);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Enabled = false;
            this.btnSua.Location = new System.Drawing.Point(64, 24);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(61, 32);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.Visible = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // rdNoiNgoaiTru
            // 
            this.rdNoiNgoaiTru.Location = new System.Drawing.Point(470, 90);
            this.rdNoiNgoaiTru.Name = "rdNoiNgoaiTru";
            this.rdNoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.rdNoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Không set")});
            this.rdNoiNgoaiTru.Size = new System.Drawing.Size(155, 59);
            this.rdNoiNgoaiTru.TabIndex = 13;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(384, 101);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(0, 13);
            this.labelControl6.TabIndex = 12;
            // 
            // dtNgayThang
            // 
            this.dtNgayThang.EditValue = null;
            this.dtNgayThang.Location = new System.Drawing.Point(470, 64);
            this.dtNgayThang.Name = "dtNgayThang";
            this.dtNgayThang.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayThang.Properties.Appearance.Options.UseFont = true;
            this.dtNgayThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayThang.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayThang.Size = new System.Drawing.Size(155, 20);
            this.dtNgayThang.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(384, 64);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(75, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Ngày tháng:";
            // 
            // lupKP
            // 
            this.lupKP.Location = new System.Drawing.Point(470, 7);
            this.lupKP.Name = "lupKP";
            this.lupKP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKP.Properties.Appearance.Options.UseFont = true;
            this.lupKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa phòng")});
            this.lupKP.Properties.DisplayMember = "TenKP";
            this.lupKP.Properties.NullText = "";
            this.lupKP.Properties.ValueMember = "MaKP";
            this.lupKP.Size = new System.Drawing.Size(155, 20);
            this.lupKP.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(384, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Khoa phòng:";
            // 
            // txtSoVV
            // 
            this.txtSoVV.Location = new System.Drawing.Point(470, 35);
            this.txtSoVV.Name = "txtSoVV";
            this.txtSoVV.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoVV.Properties.Appearance.Options.UseFont = true;
            this.txtSoVV.Properties.Mask.EditMask = "d";
            this.txtSoVV.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSoVV.Size = new System.Drawing.Size(155, 20);
            this.txtSoVV.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(384, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Số hiện tại:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.lupKPtimKiem);
            this.panel1.Controls.Add(this.grc_SoVV);
            this.panel1.Controls.Add(this.lupPLoai);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 415);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(8, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 14);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "P.Loại";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(8, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(79, 14);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Khoa phòng:";
            // 
            // lupKPtimKiem
            // 
            this.lupKPtimKiem.Location = new System.Drawing.Point(111, 28);
            this.lupKPtimKiem.Name = "lupKPtimKiem";
            this.lupKPtimKiem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKPtimKiem.Properties.Appearance.Options.UseFont = true;
            this.lupKPtimKiem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKPtimKiem.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa phòng")});
            this.lupKPtimKiem.Properties.DisplayMember = "TenKP";
            this.lupKPtimKiem.Properties.NullText = "";
            this.lupKPtimKiem.Properties.ValueMember = "MaKP";
            this.lupKPtimKiem.Size = new System.Drawing.Size(261, 20);
            this.lupKPtimKiem.TabIndex = 6;
            this.lupKPtimKiem.EditValueChanged += new System.EventHandler(this.lupKPtimKiem_EditValueChanged);
            // 
            // grc_SoVV
            // 
            this.grc_SoVV.Location = new System.Drawing.Point(0, 51);
            this.grc_SoVV.MainView = this.grv_SoVV;
            this.grc_SoVV.Name = "grc_SoVV";
            this.grc_SoVV.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupKPHT,
            this.lupNoiNgoaiTru});
            this.grc_SoVV.Size = new System.Drawing.Size(372, 359);
            this.grc_SoVV.TabIndex = 0;
            this.grc_SoVV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_SoVV});
            // 
            // grv_SoVV
            // 
            this.grv_SoVV.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_SoVV.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_SoVV.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grv_SoVV.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grv_SoVV.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grv_SoVV.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_SoVV.Appearance.Row.Options.UseFont = true;
            this.grv_SoVV.ColumnPanelRowHeight = 35;
            this.grv_SoVV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKhoaPhong,
            this.colSoVV,
            this.colNgayThang,
            this.colNoiTru});
            this.grv_SoVV.GridControl = this.grc_SoVV;
            this.grv_SoVV.Name = "grv_SoVV";
            this.grv_SoVV.OptionsBehavior.Editable = false;
            this.grv_SoVV.OptionsView.ShowGroupPanel = false;
            this.grv_SoVV.OptionsView.ShowViewCaption = true;
            this.grv_SoVV.ViewCaption = "Danh sách";
            this.grv_SoVV.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grv_SoVV_FocusedRowChanged);
            this.grv_SoVV.Click += new System.EventHandler(this.grv_SoVV_Click);
            this.grv_SoVV.DataSourceChanged += new System.EventHandler(this.grv_SoVV_DataSourceChanged);
            // 
            // colKhoaPhong
            // 
            this.colKhoaPhong.Caption = "Khoa phòng";
            this.colKhoaPhong.ColumnEdit = this.lupKPHT;
            this.colKhoaPhong.FieldName = "MaKP";
            this.colKhoaPhong.Name = "colKhoaPhong";
            this.colKhoaPhong.OptionsColumn.ReadOnly = true;
            this.colKhoaPhong.Visible = true;
            this.colKhoaPhong.VisibleIndex = 0;
            this.colKhoaPhong.Width = 167;
            // 
            // lupKPHT
            // 
            this.lupKPHT.AutoHeight = false;
            this.lupKPHT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKPHT.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa phòng")});
            this.lupKPHT.DisplayMember = "TenKP";
            this.lupKPHT.Name = "lupKPHT";
            this.lupKPHT.NullText = "";
            this.lupKPHT.ValueMember = "MaKP";
            // 
            // colSoVV
            // 
            this.colSoVV.Caption = "Số hiện tại";
            this.colSoVV.FieldName = "SoPL1";
            this.colSoVV.Name = "colSoVV";
            this.colSoVV.OptionsColumn.ReadOnly = true;
            this.colSoVV.Visible = true;
            this.colSoVV.VisibleIndex = 1;
            this.colSoVV.Width = 60;
            // 
            // colNgayThang
            // 
            this.colNgayThang.Caption = "Ngày tháng";
            this.colNgayThang.FieldName = "NgayNhap";
            this.colNgayThang.Name = "colNgayThang";
            this.colNgayThang.OptionsColumn.ReadOnly = true;
            this.colNgayThang.Visible = true;
            this.colNgayThang.VisibleIndex = 2;
            this.colNgayThang.Width = 61;
            // 
            // colNoiTru
            // 
            this.colNoiTru.Caption = "Nội ngoại trú";
            this.colNoiTru.ColumnEdit = this.lupNoiNgoaiTru;
            this.colNoiTru.FieldName = "NoiTru";
            this.colNoiTru.Name = "colNoiTru";
            this.colNoiTru.Visible = true;
            this.colNoiTru.VisibleIndex = 3;
            this.colNoiTru.Width = 66;
            // 
            // lupNoiNgoaiTru
            // 
            this.lupNoiNgoaiTru.AutoHeight = false;
            this.lupNoiNgoaiTru.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNoiNgoaiTru.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "Nội ngoại trú")});
            this.lupNoiNgoaiTru.DisplayMember = "Text";
            this.lupNoiNgoaiTru.Name = "lupNoiNgoaiTru";
            this.lupNoiNgoaiTru.NullText = "";
            this.lupNoiNgoaiTru.ValueMember = "Value";
            // 
            // lupPLoai
            // 
            this.lupPLoai.Location = new System.Drawing.Point(111, 5);
            this.lupPLoai.Name = "lupPLoai";
            this.lupPLoai.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupPLoai.Properties.Appearance.Options.UseFont = true;
            this.lupPLoai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupPLoai.Properties.PopupSizeable = true;
            this.lupPLoai.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lupPLoai.Size = new System.Drawing.Size(261, 20);
            this.lupPLoai.TabIndex = 8;
            this.lupPLoai.SelectedIndexChanged += new System.EventHandler(this.lupPLoai_SelectedIndexChanged_1);
            // 
            // frm_SoPL_SoVV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 447);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SoPL_SoVV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập số vào viện, số PL";
            this.Load += new System.EventHandler(this.frm_SoPL_SoVV_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayThang.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoVV.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPtimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grc_SoVV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_SoVV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPHT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNoiNgoaiTru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupPLoai.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl grc_SoVV;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_SoVV;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colSoVV;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayThang;
        private DevExpress.XtraEditors.DateEdit dtNgayThang;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupKP;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSoVV;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupKPtimKiem;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupKPHT;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit lupPLoai;
        private DevExpress.XtraEditors.RadioGroup rdNoiNgoaiTru;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiTru;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupNoiNgoaiTru;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}