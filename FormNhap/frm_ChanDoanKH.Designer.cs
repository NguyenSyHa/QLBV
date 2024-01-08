namespace QLBV.FormNhap
{
    partial class frm_ChanDoanKH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChanDoanKH));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.LupICD2 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtBenhPhu2 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.lupMaICDkb = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl36 = new DevExpress.XtraEditors.LabelControl();
            this.lupChanDoanKb = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBenhKhac2 = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBenhKhac1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lupKhac = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.dtNgayKham = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl41 = new DevExpress.XtraEditors.LabelControl();
            this.lupNguoiKhamkb = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBenhChinh = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.LupICD2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhPhu2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaICDkb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupChanDoanKb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhKhac2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhKhac1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKham.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKham.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNguoiKhamkb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhChinh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LupICD2
            // 
            this.LupICD2.EnterMoveNextControl = true;
            this.LupICD2.Location = new System.Drawing.Point(552, 52);
            this.LupICD2.Name = "LupICD2";
            this.LupICD2.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupICD2.Properties.Appearance.Options.UseFont = true;
            this.LupICD2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.LupICD2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.LupICD2.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.LupICD2.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.LupICD2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("LupICD2.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Chỉ định gói theo ICD", null, null, true)});
            this.LupICD2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenICD", 200, "Tên bệnh"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaICD", 50, "Mã ICD")});
            this.LupICD2.Properties.DisplayMember = "MaICD";
            this.LupICD2.Properties.NullText = "";
            this.LupICD2.Properties.PopupFormMinSize = new System.Drawing.Size(250, 0);
            this.LupICD2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.LupICD2.Properties.ValueMember = "MaICD";
            this.LupICD2.Size = new System.Drawing.Size(113, 24);
            this.LupICD2.TabIndex = 97;
            this.LupICD2.EditValueChanged += new System.EventHandler(this.LupICD2_EditValueChanged);
            this.LupICD2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LupICD2_PreviewKeyDown);
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl17.Location = new System.Drawing.Point(483, 56);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(46, 15);
            this.labelControl17.TabIndex = 101;
            this.labelControl17.Text = "Mã ICD:";
            // 
            // txtBenhPhu2
            // 
            this.txtBenhPhu2.Location = new System.Drawing.Point(87, 52);
            this.txtBenhPhu2.Name = "txtBenhPhu2";
            this.txtBenhPhu2.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhPhu2.Properties.Appearance.Options.UseFont = true;
            this.txtBenhPhu2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBenhPhu2.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick_1);
            this.txtBenhPhu2.Size = new System.Drawing.Size(392, 24);
            this.txtBenhPhu2.TabIndex = 103;
            this.txtBenhPhu2.Visible = false;
            this.txtBenhPhu2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBenhPhu2_KeyDown);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(7, 57);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(63, 15);
            this.labelControl7.TabIndex = 100;
            this.labelControl7.Text = "Bệnh khác:";
            // 
            // labelControl35
            // 
            this.labelControl35.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl35.Location = new System.Drawing.Point(483, 33);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(46, 15);
            this.labelControl35.TabIndex = 99;
            this.labelControl35.Text = "Mã ICD:";
            // 
            // lupMaICDkb
            // 
            this.lupMaICDkb.EnterMoveNextControl = true;
            this.lupMaICDkb.Location = new System.Drawing.Point(552, 28);
            this.lupMaICDkb.Name = "lupMaICDkb";
            this.lupMaICDkb.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaICDkb.Properties.Appearance.Options.UseFont = true;
            this.lupMaICDkb.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupMaICDkb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.lupMaICDkb.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupMaICDkb.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupMaICDkb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("lupMaICDkb.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Chỉ định gói theo ICD", null, null, true)});
            this.lupMaICDkb.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaICD", 50, "Mã ICD"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaICD", "Name2", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupMaICDkb.Properties.DisplayMember = "MaICD";
            this.lupMaICDkb.Properties.NullText = "";
            this.lupMaICDkb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupMaICDkb.Properties.ValueMember = "MaICD";
            this.lupMaICDkb.Size = new System.Drawing.Size(113, 24);
            this.lupMaICDkb.TabIndex = 95;
            this.lupMaICDkb.EditValueChanged += new System.EventHandler(this.lupMaICDkb_EditValueChanged);
            this.lupMaICDkb.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lupMaICDkb_PreviewKeyDown);
            // 
            // labelControl36
            // 
            this.labelControl36.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl36.Location = new System.Drawing.Point(7, 33);
            this.labelControl36.Name = "labelControl36";
            this.labelControl36.Size = new System.Drawing.Size(67, 15);
            this.labelControl36.TabIndex = 98;
            this.labelControl36.Text = "Bệnh chính:";
            // 
            // lupChanDoanKb
            // 
            this.lupChanDoanKb.Location = new System.Drawing.Point(87, 28);
            this.lupChanDoanKb.Name = "lupChanDoanKb";
            this.lupChanDoanKb.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lupChanDoanKb.Properties.Appearance.Options.UseFont = true;
            this.lupChanDoanKb.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupChanDoanKb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.lupChanDoanKb.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupChanDoanKb.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupChanDoanKb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupChanDoanKb.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaICD", 6, "Mã ICD"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenICD", 30, "Tên Bệnh")});
            this.lupChanDoanKb.Properties.DisplayMember = "TenICD";
            this.lupChanDoanKb.Properties.NullText = "";
            this.lupChanDoanKb.Properties.PopupFormMinSize = new System.Drawing.Size(350, 300);
            this.lupChanDoanKb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupChanDoanKb.Properties.ValueMember = "TenICD";
            this.lupChanDoanKb.Size = new System.Drawing.Size(392, 24);
            this.lupChanDoanKb.TabIndex = 94;
            this.lupChanDoanKb.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lupChanDoanKb_Closed);
            this.lupChanDoanKb.EditValueChanged += new System.EventHandler(this.lupChanDoanKb_EditValueChanged);
            this.lupChanDoanKb.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lupChanDoanKb_PreviewKeyDown);
            // 
            // txtBenhKhac2
            // 
            this.txtBenhKhac2.Location = new System.Drawing.Point(87, 52);
            this.txtBenhKhac2.Name = "txtBenhKhac2";
            this.txtBenhKhac2.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBenhKhac2.Properties.Appearance.Options.UseFont = true;
            this.txtBenhKhac2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.txtBenhKhac2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.txtBenhKhac2.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtBenhKhac2.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtBenhKhac2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBenhKhac2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaICD", 6, "Mã ICD"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenICD", 30, "Tên Bệnh")});
            this.txtBenhKhac2.Properties.DisplayMember = "TenICD";
            this.txtBenhKhac2.Properties.NullText = "";
            this.txtBenhKhac2.Properties.PopupFormMinSize = new System.Drawing.Size(350, 300);
            this.txtBenhKhac2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtBenhKhac2.Properties.ValueMember = "TenICD";
            this.txtBenhKhac2.Size = new System.Drawing.Size(392, 24);
            this.txtBenhKhac2.TabIndex = 96;
            this.txtBenhKhac2.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.txtBenhKhac2_Closed);
            this.txtBenhKhac2.EditValueChanged += new System.EventHandler(this.txtBenhKhac2_EditValueChanged);
            // 
            // txtBenhKhac1
            // 
            this.txtBenhKhac1.EditValue = "";
            this.txtBenhKhac1.Location = new System.Drawing.Point(87, 53);
            this.txtBenhKhac1.Name = "txtBenhKhac1";
            this.txtBenhKhac1.Properties.AllowMultiSelect = true;
            this.txtBenhKhac1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhKhac1.Properties.Appearance.Options.UseFont = true;
            this.txtBenhKhac1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBenhKhac1.Properties.DropDownRows = 10;
            this.txtBenhKhac1.Properties.IncrementalSearch = true;
            this.txtBenhKhac1.Properties.SelectAllItemVisible = false;
            this.txtBenhKhac1.Properties.SeparatorChar = ';';
            this.txtBenhKhac1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtBenhKhac1.Size = new System.Drawing.Size(392, 24);
            this.txtBenhKhac1.TabIndex = 104;
            // 
            // lupKhac
            // 
            this.lupKhac.EditValue = "";
            this.lupKhac.Location = new System.Drawing.Point(552, 52);
            this.lupKhac.Name = "lupKhac";
            this.lupKhac.Properties.AllowMultiSelect = true;
            this.lupKhac.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhac.Properties.Appearance.Options.UseFont = true;
            this.lupKhac.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupKhac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.lupKhac.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupKhac.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupKhac.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("lupKhac.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Chỉ định gói theo ICD", null, null, true)});
            this.lupKhac.Properties.DropDownRows = 10;
            this.lupKhac.Properties.IncrementalSearch = true;
            this.lupKhac.Properties.PopupFormMinSize = new System.Drawing.Size(250, 0);
            this.lupKhac.Properties.SelectAllItemVisible = false;
            this.lupKhac.Properties.SeparatorChar = ';';
            this.lupKhac.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhac.Size = new System.Drawing.Size(113, 24);
            this.lupKhac.TabIndex = 105;
            this.lupKhac.EditValueChanged += new System.EventHandler(this.lupKhac_EditValueChanged);
            this.lupKhac.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lupKhac_PreviewKeyDown);
            // 
            // dtNgayKham
            // 
            this.dtNgayKham.EditValue = null;
            this.dtNgayKham.Location = new System.Drawing.Point(87, 4);
            this.dtNgayKham.Name = "dtNgayKham";
            this.dtNgayKham.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.dtNgayKham.Properties.Appearance.Options.UseFont = true;
            this.dtNgayKham.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKham.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKham.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dtNgayKham.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtNgayKham.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm";
            this.dtNgayKham.Size = new System.Drawing.Size(239, 24);
            this.dtNgayKham.TabIndex = 106;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(7, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 15);
            this.labelControl1.TabIndex = 107;
            this.labelControl1.Text = "Ngày khám:";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(552, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 23);
            this.btnSave.TabIndex = 108;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl41
            // 
            this.labelControl41.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl41.Location = new System.Drawing.Point(337, 8);
            this.labelControl41.Name = "labelControl41";
            this.labelControl41.Size = new System.Drawing.Size(81, 15);
            this.labelControl41.TabIndex = 110;
            this.labelControl41.Text = "Bác sĩ điều trị:";
            // 
            // lupNguoiKhamkb
            // 
            this.lupNguoiKhamkb.EnterMoveNextControl = true;
            this.lupNguoiKhamkb.Location = new System.Drawing.Point(424, 4);
            this.lupNguoiKhamkb.Name = "lupNguoiKhamkb";
            this.lupNguoiKhamkb.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNguoiKhamkb.Properties.Appearance.Options.UseFont = true;
            this.lupNguoiKhamkb.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupNguoiKhamkb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.lupNguoiKhamkb.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupNguoiKhamkb.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupNguoiKhamkb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNguoiKhamkb.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", "Tên Bác Sĩ"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", "Mã", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupNguoiKhamkb.Properties.DisplayMember = "TenCB";
            this.lupNguoiKhamkb.Properties.NullText = "";
            this.lupNguoiKhamkb.Properties.PopupSizeable = false;
            this.lupNguoiKhamkb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNguoiKhamkb.Properties.ValueMember = "MaCB";
            this.lupNguoiKhamkb.Size = new System.Drawing.Size(241, 24);
            this.lupNguoiKhamkb.TabIndex = 109;
            // 
            // txtBenhChinh
            // 
            this.txtBenhChinh.Location = new System.Drawing.Point(87, 28);
            this.txtBenhChinh.Name = "txtBenhChinh";
            this.txtBenhChinh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhChinh.Properties.Appearance.Options.UseFont = true;
            this.txtBenhChinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBenhChinh.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick_1);
            this.txtBenhChinh.Size = new System.Drawing.Size(392, 24);
            this.txtBenhChinh.TabIndex = 111;
            this.txtBenhChinh.Visible = false;
            this.txtBenhChinh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBenhChinh_KeyDown);
            // 
            // frm_ChanDoanKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 110);
            this.Controls.Add(this.txtBenhChinh);
            this.Controls.Add(this.labelControl41);
            this.Controls.Add(this.lupNguoiKhamkb);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dtNgayKham);
            this.Controls.Add(this.LupICD2);
            this.Controls.Add(this.labelControl17);
            this.Controls.Add(this.txtBenhPhu2);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl35);
            this.Controls.Add(this.lupMaICDkb);
            this.Controls.Add(this.labelControl36);
            this.Controls.Add(this.lupChanDoanKb);
            this.Controls.Add(this.txtBenhKhac2);
            this.Controls.Add(this.txtBenhKhac1);
            this.Controls.Add(this.lupKhac);
            this.Name = "frm_ChanDoanKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chẩn đoán khoa kết hợp";
            this.Load += new System.EventHandler(this.frm_ChanDoanKH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupICD2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhPhu2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaICDkb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupChanDoanKb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhKhac2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhKhac1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKham.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKham.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNguoiKhamkb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBenhChinh.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit LupICD2;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.ButtonEdit txtBenhPhu2;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl35;
        private DevExpress.XtraEditors.LookUpEdit lupMaICDkb;
        private DevExpress.XtraEditors.LabelControl labelControl36;
        private DevExpress.XtraEditors.LookUpEdit lupChanDoanKb;
        private DevExpress.XtraEditors.LookUpEdit txtBenhKhac2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtBenhKhac1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit lupKhac;
        private DevExpress.XtraEditors.DateEdit dtNgayKham;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl41;
        private DevExpress.XtraEditors.LookUpEdit lupNguoiKhamkb;
        private DevExpress.XtraEditors.ButtonEdit txtBenhChinh;
    }
}