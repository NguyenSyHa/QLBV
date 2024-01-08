namespace QLBV.FormNhap
{
    partial class frm_DuyetPhieuLinh
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbotrangthai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lupkhoxuat = new DevExpress.XtraEditors.LookUpEdit();
            this.lupkhoake = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSoPL = new DevExpress.XtraEditors.TextEdit();
            this.dtdenngay = new DevExpress.XtraEditors.DateEdit();
            this.dttungay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grcDSPhieuLinh = new DevExpress.XtraGrid.GridControl();
            this.grvDSPhieuLinh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colsopl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colngaytao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkhoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkhoxuat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colduyet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnluu = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.hybochon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hychon = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotrangthai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoxuat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoake.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSPhieuLinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSPhieuLinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hybochon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hychon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.cbotrangthai);
            this.groupControl1.Controls.Add(this.lupkhoxuat);
            this.groupControl1.Controls.Add(this.lupkhoake);
            this.groupControl1.Controls.Add(this.txtSoPL);
            this.groupControl1.Controls.Add(this.dtdenngay);
            this.groupControl1.Controls.Add(this.dttungay);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(805, 82);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // cbotrangthai
            // 
            this.cbotrangthai.Location = new System.Drawing.Point(294, 51);
            this.cbotrangthai.Name = "cbotrangthai";
            this.cbotrangthai.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbotrangthai.Properties.Appearance.Options.UseFont = true;
            this.cbotrangthai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbotrangthai.Properties.Items.AddRange(new object[] {
            "Chưa duyệt",
            "Đã duyệt",
            "Đã xuất"});
            this.cbotrangthai.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbotrangthai.Size = new System.Drawing.Size(130, 22);
            this.cbotrangthai.TabIndex = 3;
            this.cbotrangthai.SelectedIndexChanged += new System.EventHandler(this.cbotrangthai_SelectedIndexChanged);
            // 
            // lupkhoxuat
            // 
            this.lupkhoxuat.Location = new System.Drawing.Point(541, 51);
            this.lupkhoxuat.Name = "lupkhoxuat";
            this.lupkhoxuat.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupkhoxuat.Properties.Appearance.Options.UseFont = true;
            this.lupkhoxuat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupkhoxuat.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", 5, "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 30, "Tên Kho")});
            this.lupkhoxuat.Properties.DisplayMember = "TenKP";
            this.lupkhoxuat.Properties.NullText = "";
            this.lupkhoxuat.Properties.ValueMember = "MaKP";
            this.lupkhoxuat.Size = new System.Drawing.Size(168, 22);
            this.lupkhoxuat.TabIndex = 5;
            this.lupkhoxuat.EditValueChanged += new System.EventHandler(this.lupkhoxuat_EditValueChanged);
            // 
            // lupkhoake
            // 
            this.lupkhoake.Location = new System.Drawing.Point(541, 23);
            this.lupkhoake.Name = "lupkhoake";
            this.lupkhoake.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupkhoake.Properties.Appearance.Options.UseFont = true;
            this.lupkhoake.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupkhoake.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", 5, "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 30, "Tên Khoa|Phòng")});
            this.lupkhoake.Properties.DisplayMember = "TenKP";
            this.lupkhoake.Properties.NullText = "";
            this.lupkhoake.Properties.ValueMember = "MaKP";
            this.lupkhoake.Size = new System.Drawing.Size(168, 22);
            this.lupkhoake.TabIndex = 4;
            this.lupkhoake.EditValueChanged += new System.EventHandler(this.lupkhoake_EditValueChanged);
            // 
            // txtSoPL
            // 
            this.txtSoPL.Location = new System.Drawing.Point(294, 25);
            this.txtSoPL.Name = "txtSoPL";
            this.txtSoPL.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoPL.Properties.Appearance.Options.UseFont = true;
            this.txtSoPL.Size = new System.Drawing.Size(130, 22);
            this.txtSoPL.TabIndex = 2;
            this.txtSoPL.Leave += new System.EventHandler(this.txtSoPL_Leave);
            // 
            // dtdenngay
            // 
            this.dtdenngay.EditValue = null;
            this.dtdenngay.Location = new System.Drawing.Point(68, 51);
            this.dtdenngay.Name = "dtdenngay";
            this.dtdenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtdenngay.Properties.Appearance.Options.UseFont = true;
            this.dtdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Size = new System.Drawing.Size(140, 22);
            this.dtdenngay.TabIndex = 1;
            this.dtdenngay.EditValueChanged += new System.EventHandler(this.dtdenngay_EditValueChanged);
            // 
            // dttungay
            // 
            this.dttungay.EditValue = null;
            this.dttungay.Location = new System.Drawing.Point(68, 25);
            this.dttungay.Name = "dttungay";
            this.dttungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dttungay.Properties.Appearance.Options.UseFont = true;
            this.dttungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Size = new System.Drawing.Size(140, 22);
            this.dttungay.TabIndex = 0;
            this.dttungay.EditValueChanged += new System.EventHandler(this.dttungay_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(7, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 15);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(222, 54);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(61, 15);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Trạng thái:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(222, 27);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 15);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Số PL:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(442, 54);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 15);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Kho Xuất:";
            this.labelControl5.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(442, 26);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(88, 15);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Khoa|Phòng Kê:";
            this.labelControl4.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(7, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // grcDSPhieuLinh
            // 
            this.grcDSPhieuLinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDSPhieuLinh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grcDSPhieuLinh.Location = new System.Drawing.Point(2, 22);
            this.grcDSPhieuLinh.MainView = this.grvDSPhieuLinh;
            this.grcDSPhieuLinh.Name = "grcDSPhieuLinh";
            this.grcDSPhieuLinh.Size = new System.Drawing.Size(801, 340);
            this.grcDSPhieuLinh.TabIndex = 0;
            this.grcDSPhieuLinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDSPhieuLinh});
            // 
            // grvDSPhieuLinh
            // 
            this.grvDSPhieuLinh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colsopl,
            this.colngaytao,
            this.colkhoa,
            this.colkhoxuat,
            this.colduyet});
            this.grvDSPhieuLinh.GridControl = this.grcDSPhieuLinh;
            this.grvDSPhieuLinh.Name = "grvDSPhieuLinh";
            this.grvDSPhieuLinh.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.grvDSPhieuLinh.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDSPhieuLinh.OptionsView.EnableAppearanceOddRow = true;
            this.grvDSPhieuLinh.OptionsView.ShowFooter = true;
            this.grvDSPhieuLinh.OptionsView.ShowGroupPanel = false;
            this.grvDSPhieuLinh.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvDSPhieuLinh_RowCellClick);
            this.grvDSPhieuLinh.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDSPhieuLinh_CellValueChanged);
            this.grvDSPhieuLinh.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDSPhieuLinh_CellValueChanging);
            this.grvDSPhieuLinh.DoubleClick += new System.EventHandler(this.grvDSPhieuLinh_DoubleClick);
            // 
            // colsopl
            // 
            this.colsopl.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colsopl.AppearanceCell.Options.UseFont = true;
            this.colsopl.AppearanceCell.Options.UseTextOptions = true;
            this.colsopl.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colsopl.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colsopl.AppearanceHeader.Options.UseFont = true;
            this.colsopl.AppearanceHeader.Options.UseTextOptions = true;
            this.colsopl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colsopl.Caption = "Số PL";
            this.colsopl.FieldName = "sopl";
            this.colsopl.Name = "colsopl";
            this.colsopl.OptionsColumn.AllowEdit = false;
            this.colsopl.OptionsColumn.AllowFocus = false;
            this.colsopl.OptionsColumn.ReadOnly = true;
            this.colsopl.Visible = true;
            this.colsopl.VisibleIndex = 0;
            this.colsopl.Width = 215;
            // 
            // colngaytao
            // 
            this.colngaytao.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colngaytao.AppearanceCell.Options.UseFont = true;
            this.colngaytao.AppearanceCell.Options.UseTextOptions = true;
            this.colngaytao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colngaytao.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colngaytao.AppearanceHeader.Options.UseFont = true;
            this.colngaytao.AppearanceHeader.Options.UseTextOptions = true;
            this.colngaytao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colngaytao.Caption = "Ngày tạo";
            this.colngaytao.FieldName = "ngaytao";
            this.colngaytao.Name = "colngaytao";
            this.colngaytao.OptionsColumn.AllowEdit = false;
            this.colngaytao.OptionsColumn.AllowFocus = false;
            this.colngaytao.OptionsColumn.ReadOnly = true;
            this.colngaytao.Visible = true;
            this.colngaytao.VisibleIndex = 1;
            this.colngaytao.Width = 178;
            // 
            // colkhoa
            // 
            this.colkhoa.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colkhoa.AppearanceCell.Options.UseFont = true;
            this.colkhoa.AppearanceCell.Options.UseTextOptions = true;
            this.colkhoa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colkhoa.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colkhoa.AppearanceHeader.Options.UseFont = true;
            this.colkhoa.AppearanceHeader.Options.UseTextOptions = true;
            this.colkhoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colkhoa.Caption = "Khoa|Phòng Kê";
            this.colkhoa.FieldName = "makp";
            this.colkhoa.Name = "colkhoa";
            this.colkhoa.OptionsColumn.AllowEdit = false;
            this.colkhoa.OptionsColumn.AllowFocus = false;
            this.colkhoa.OptionsColumn.ReadOnly = true;
            this.colkhoa.Visible = true;
            this.colkhoa.VisibleIndex = 2;
            this.colkhoa.Width = 284;
            // 
            // colkhoxuat
            // 
            this.colkhoxuat.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colkhoxuat.AppearanceCell.Options.UseFont = true;
            this.colkhoxuat.AppearanceCell.Options.UseTextOptions = true;
            this.colkhoxuat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colkhoxuat.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colkhoxuat.AppearanceHeader.Options.UseFont = true;
            this.colkhoxuat.AppearanceHeader.Options.UseTextOptions = true;
            this.colkhoxuat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colkhoxuat.Caption = "Kho Xuất";
            this.colkhoxuat.FieldName = "makxuat";
            this.colkhoxuat.Name = "colkhoxuat";
            this.colkhoxuat.OptionsColumn.AllowEdit = false;
            this.colkhoxuat.OptionsColumn.AllowFocus = false;
            this.colkhoxuat.OptionsColumn.ReadOnly = true;
            this.colkhoxuat.Visible = true;
            this.colkhoxuat.VisibleIndex = 3;
            this.colkhoxuat.Width = 324;
            // 
            // colduyet
            // 
            this.colduyet.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colduyet.AppearanceHeader.Options.UseFont = true;
            this.colduyet.AppearanceHeader.Options.UseTextOptions = true;
            this.colduyet.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colduyet.Caption = "Duyệt";
            this.colduyet.FieldName = "duyet";
            this.colduyet.Name = "colduyet";
            this.colduyet.Visible = true;
            this.colduyet.VisibleIndex = 4;
            this.colduyet.Width = 77;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnluu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 446);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(805, 37);
            this.panelControl1.TabIndex = 2;
            // 
            // btnluu
            // 
            this.btnluu.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnluu.Appearance.Options.UseFont = true;
            this.btnluu.Image = global::QLBV.Properties.Resources.apply_16x16;
            this.btnluu.Location = new System.Drawing.Point(690, 2);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(103, 32);
            this.btnluu.TabIndex = 0;
            this.btnluu.Text = "Lưu";
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.hybochon);
            this.groupControl2.Controls.Add(this.hychon);
            this.groupControl2.Controls.Add(this.grcDSPhieuLinh);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 82);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(805, 364);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Danh sách phiếu lĩnh";
            // 
            // hybochon
            // 
            this.hybochon.EditValue = "Bỏ chọn tất cả";
            this.hybochon.Location = new System.Drawing.Point(718, 342);
            this.hybochon.Name = "hybochon";
            this.hybochon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hybochon.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.hybochon.Properties.Appearance.Options.UseFont = true;
            this.hybochon.Properties.Appearance.Options.UseForeColor = true;
            this.hybochon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hybochon.Size = new System.Drawing.Size(82, 18);
            this.hybochon.TabIndex = 2;
            this.hybochon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hybochon_OpenLink);
            // 
            // hychon
            // 
            this.hychon.EditValue = "Chọn";
            this.hychon.Location = new System.Drawing.Point(668, 342);
            this.hychon.Name = "hychon";
            this.hychon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hychon.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.hychon.Properties.Appearance.Options.UseFont = true;
            this.hychon.Properties.Appearance.Options.UseForeColor = true;
            this.hychon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hychon.Size = new System.Drawing.Size(41, 18);
            this.hychon.TabIndex = 1;
            this.hychon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hychon_OpenLink);
            // 
            // frm_DuyetPhieuLinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 483);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "frm_DuyetPhieuLinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duyệt phiếu lĩnh Thuốc - VTYT";
            this.Load += new System.EventHandler(this.frm_DuyetPhieuLinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotrangthai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoxuat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoake.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSPhieuLinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSPhieuLinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hybochon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hychon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.DateEdit dtdenngay;
        private DevExpress.XtraEditors.DateEdit dttungay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl grcDSPhieuLinh;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDSPhieuLinh;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnluu;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LookUpEdit lupkhoxuat;
        private DevExpress.XtraEditors.LookUpEdit lupkhoake;
        private DevExpress.XtraEditors.TextEdit txtSoPL;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cbotrangthai;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn colsopl;
        private DevExpress.XtraGrid.Columns.GridColumn colngaytao;
        private DevExpress.XtraGrid.Columns.GridColumn colkhoa;
        private DevExpress.XtraGrid.Columns.GridColumn colkhoxuat;
        private DevExpress.XtraGrid.Columns.GridColumn colduyet;
        private DevExpress.XtraEditors.HyperLinkEdit hychon;
        private DevExpress.XtraEditors.HyperLinkEdit hybochon;
    }
}