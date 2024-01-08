namespace QLBV.FormThamSo
{
    partial class frm_XemTB
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grcTBCu = new DevExpress.XtraGrid.GridControl();
            this.grvTBCu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNgayNhap2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCBTB2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNDung2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dtdenngay = new DevExpress.XtraEditors.DateEdit();
            this.dttungay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupcanbo = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.grcTBMoi = new DevExpress.XtraGrid.GridControl();
            this.grvTBMoi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNgayNhap1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCBNhap1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNDung1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtTenCB = new DevExpress.XtraEditors.TextEdit();
            this.mmNDung = new DevExpress.XtraEditors.MemoEdit();
            this.deNgayNhap = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcTBCu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTBCu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupcanbo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcTBMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTBMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmNDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayNhap.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayNhap.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.grcTBCu);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 371);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1044, 310);
            this.panelControl1.TabIndex = 0;
            // 
            // grcTBCu
            // 
            this.grcTBCu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcTBCu.Location = new System.Drawing.Point(0, 0);
            this.grcTBCu.MainView = this.grvTBCu;
            this.grcTBCu.Name = "grcTBCu";
            this.grcTBCu.Size = new System.Drawing.Size(1044, 265);
            this.grcTBCu.TabIndex = 0;
            this.grcTBCu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTBCu});
            // 
            // grvTBCu
            // 
            this.grvTBCu.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvTBCu.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Blue;
            this.grvTBCu.Appearance.ViewCaption.Options.UseFont = true;
            this.grvTBCu.Appearance.ViewCaption.Options.UseForeColor = true;
            this.grvTBCu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNgayNhap2,
            this.colCBTB2,
            this.colNDung2,
            this.colID});
            this.grvTBCu.GridControl = this.grcTBCu;
            this.grvTBCu.Name = "grvTBCu";
            this.grvTBCu.OptionsBehavior.ReadOnly = true;
            this.grvTBCu.OptionsView.ShowGroupPanel = false;
            this.grvTBCu.OptionsView.ShowViewCaption = true;
            this.grvTBCu.ViewCaption = "Thông báo đã xem";
            this.grvTBCu.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvTBCu_FocusedRowChanged);
            // 
            // colNgayNhap2
            // 
            this.colNgayNhap2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNgayNhap2.AppearanceHeader.Options.UseFont = true;
            this.colNgayNhap2.Caption = "Ngày nhập";
            this.colNgayNhap2.FieldName = "NgayNhap";
            this.colNgayNhap2.Name = "colNgayNhap2";
            this.colNgayNhap2.Visible = true;
            this.colNgayNhap2.VisibleIndex = 0;
            this.colNgayNhap2.Width = 133;
            // 
            // colCBTB2
            // 
            this.colCBTB2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCBTB2.AppearanceHeader.Options.UseFont = true;
            this.colCBTB2.Caption = "Cán bộ TB";
            this.colCBTB2.FieldName = "TenCB";
            this.colCBTB2.Name = "colCBTB2";
            this.colCBTB2.Visible = true;
            this.colCBTB2.VisibleIndex = 1;
            this.colCBTB2.Width = 162;
            // 
            // colNDung2
            // 
            this.colNDung2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNDung2.AppearanceHeader.Options.UseFont = true;
            this.colNDung2.Caption = "Nội dung";
            this.colNDung2.FieldName = "NoiDung";
            this.colNDung2.Name = "colNDung2";
            this.colNDung2.Visible = true;
            this.colNDung2.VisibleIndex = 2;
            this.colNDung2.Width = 783;
            // 
            // colID
            // 
            this.colID.Caption = "gridColumn4";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.dtdenngay);
            this.groupControl1.Controls.Add(this.dttungay);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lupcanbo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 265);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1044, 45);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // dtdenngay
            // 
            this.dtdenngay.EditValue = null;
            this.dtdenngay.Location = new System.Drawing.Point(288, 22);
            this.dtdenngay.Name = "dtdenngay";
            this.dtdenngay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtdenngay.Properties.Appearance.Options.UseFont = true;
            this.dtdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Size = new System.Drawing.Size(131, 20);
            this.dtdenngay.TabIndex = 1;
            this.dtdenngay.EditValueChanged += new System.EventHandler(this.dtdenngay_EditValueChanged);
            // 
            // dttungay
            // 
            this.dttungay.EditValue = null;
            this.dttungay.Location = new System.Drawing.Point(65, 22);
            this.dttungay.Name = "dttungay";
            this.dttungay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dttungay.Properties.Appearance.Options.UseFont = true;
            this.dttungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Size = new System.Drawing.Size(131, 20);
            this.dttungay.TabIndex = 0;
            this.dttungay.EditValueChanged += new System.EventHandler(this.dttungay_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(436, 24);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(91, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "CB thông báo:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(220, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "Đến ngày:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(5, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Từ ngày:";
            // 
            // lupcanbo
            // 
            this.lupcanbo.Location = new System.Drawing.Point(533, 22);
            this.lupcanbo.Name = "lupcanbo";
            this.lupcanbo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupcanbo.Properties.Appearance.Options.UseFont = true;
            this.lupcanbo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupcanbo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", 10, "Mã CB"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 30, "Tên CB")});
            this.lupcanbo.Properties.DisplayFormat.FormatString = "d";
            this.lupcanbo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupcanbo.Properties.DisplayMember = "TenCB";
            this.lupcanbo.Properties.EditFormat.FormatString = "d";
            this.lupcanbo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lupcanbo.Properties.NullText = "";
            this.lupcanbo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupcanbo.Properties.ValueMember = "MaCB";
            this.lupcanbo.Size = new System.Drawing.Size(184, 20);
            this.lupcanbo.TabIndex = 2;
            this.lupcanbo.EditValueChanged += new System.EventHandler(this.lupcanbo_EditValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1044, 371);
            this.panelControl2.TabIndex = 1;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.grcTBMoi);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(487, 371);
            this.panelControl4.TabIndex = 1;
            // 
            // grcTBMoi
            // 
            this.grcTBMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcTBMoi.Location = new System.Drawing.Point(0, 0);
            this.grcTBMoi.MainView = this.grvTBMoi;
            this.grcTBMoi.Name = "grcTBMoi";
            this.grcTBMoi.Size = new System.Drawing.Size(487, 371);
            this.grcTBMoi.TabIndex = 0;
            this.grcTBMoi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTBMoi});
            // 
            // grvTBMoi
            // 
            this.grvTBMoi.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvTBMoi.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Red;
            this.grvTBMoi.Appearance.ViewCaption.Options.UseFont = true;
            this.grvTBMoi.Appearance.ViewCaption.Options.UseForeColor = true;
            this.grvTBMoi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNgayNhap1,
            this.colCBNhap1,
            this.colNDung1,
            this.colID1});
            this.grvTBMoi.GridControl = this.grcTBMoi;
            this.grvTBMoi.Name = "grvTBMoi";
            this.grvTBMoi.OptionsBehavior.ReadOnly = true;
            this.grvTBMoi.OptionsView.ShowFooter = true;
            this.grvTBMoi.OptionsView.ShowGroupPanel = false;
            this.grvTBMoi.OptionsView.ShowViewCaption = true;
            this.grvTBMoi.ViewCaption = "Thông báo mới";
            this.grvTBMoi.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvTBMoi_FocusedRowChanged);
            // 
            // colNgayNhap1
            // 
            this.colNgayNhap1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNgayNhap1.AppearanceHeader.Options.UseFont = true;
            this.colNgayNhap1.Caption = "Ngày nhập";
            this.colNgayNhap1.FieldName = "NgayNhap";
            this.colNgayNhap1.Name = "colNgayNhap1";
            this.colNgayNhap1.Visible = true;
            this.colNgayNhap1.VisibleIndex = 0;
            this.colNgayNhap1.Width = 223;
            // 
            // colCBNhap1
            // 
            this.colCBNhap1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCBNhap1.AppearanceHeader.Options.UseFont = true;
            this.colCBNhap1.Caption = "CB Thông báo";
            this.colCBNhap1.FieldName = "TenCB";
            this.colCBNhap1.Name = "colCBNhap1";
            this.colCBNhap1.Visible = true;
            this.colCBNhap1.VisibleIndex = 1;
            this.colCBNhap1.Width = 246;
            // 
            // colNDung1
            // 
            this.colNDung1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNDung1.AppearanceHeader.Options.UseFont = true;
            this.colNDung1.Caption = "Nội dung";
            this.colNDung1.FieldName = "NoiDung";
            this.colNDung1.Name = "colNDung1";
            this.colNDung1.Visible = true;
            this.colNDung1.VisibleIndex = 2;
            this.colNDung1.Width = 609;
            // 
            // colID1
            // 
            this.colID1.Caption = "ID";
            this.colID1.FieldName = "ID";
            this.colID1.Name = "colID1";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(487, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(557, 371);
            this.panelControl3.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.txtTenCB);
            this.groupControl2.Controls.Add(this.mmNDung);
            this.groupControl2.Controls.Add(this.deNgayNhap);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(557, 371);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Nội dung";
            // 
            // txtTenCB
            // 
            this.txtTenCB.Location = new System.Drawing.Point(382, 27);
            this.txtTenCB.Name = "txtTenCB";
            this.txtTenCB.Properties.ReadOnly = true;
            this.txtTenCB.Size = new System.Drawing.Size(163, 20);
            this.txtTenCB.TabIndex = 1;
            // 
            // mmNDung
            // 
            this.mmNDung.Location = new System.Drawing.Point(6, 73);
            this.mmNDung.Name = "mmNDung";
            this.mmNDung.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmNDung.Properties.Appearance.Options.UseFont = true;
            this.mmNDung.Properties.ReadOnly = true;
            this.mmNDung.Size = new System.Drawing.Size(548, 292);
            this.mmNDung.TabIndex = 2;
            // 
            // deNgayNhap
            // 
            this.deNgayNhap.EditValue = null;
            this.deNgayNhap.Location = new System.Drawing.Point(81, 26);
            this.deNgayNhap.Name = "deNgayNhap";
            this.deNgayNhap.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deNgayNhap.Properties.Appearance.Options.UseFont = true;
            this.deNgayNhap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayNhap.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayNhap.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deNgayNhap.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deNgayNhap.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deNgayNhap.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deNgayNhap.Properties.Mask.EditMask = "g";
            this.deNgayNhap.Properties.ReadOnly = true;
            this.deNgayNhap.Size = new System.Drawing.Size(149, 20);
            this.deNgayNhap.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(287, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(89, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "CB thông báo:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(6, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(49, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Chi tiết:";
            this.labelControl3.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(6, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Ngày nhập:";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // frm_XemTB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 681);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_XemTB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông Báo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_XemTB_FormClosed);
            this.Load += new System.EventHandler(this.frm_XemTB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcTBCu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTBCu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupcanbo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcTBMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTBMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmNDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayNhap.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayNhap.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grcTBCu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTBCu;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl grcTBMoi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTBMoi;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deNgayNhap;
        private DevExpress.XtraEditors.MemoEdit mmNDung;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap1;
        private DevExpress.XtraGrid.Columns.GridColumn colCBNhap1;
        private DevExpress.XtraGrid.Columns.GridColumn colNDung1;
        private DevExpress.XtraGrid.Columns.GridColumn colID1;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap2;
        private DevExpress.XtraGrid.Columns.GridColumn colCBTB2;
        private DevExpress.XtraGrid.Columns.GridColumn colNDung2;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.TextEdit txtTenCB;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dtdenngay;
        private DevExpress.XtraEditors.DateEdit dttungay;
        private DevExpress.XtraEditors.LookUpEdit lupcanbo;
    }
}