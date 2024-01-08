namespace QLBV.FormThamSo
{
    partial class frm_SaoChiDinh
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
            this.grcDichVu = new DevExpress.XtraGrid.GridControl();
            this.grvDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDCLS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lupKPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.cbotrangthai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dateNgayCD1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.chkSaoNhieu = new DevExpress.XtraEditors.CheckEdit();
            this.cboTyLe = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSaoCD = new DevExpress.XtraEditors.SimpleButton();
            this.dateNgayCD = new DevExpress.XtraEditors.DateEdit();
            this.dateNgayTH = new DevExpress.XtraEditors.DateEdit();
            this.ckcSaoKQ = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grcDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbotrangthai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaoNhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTyLe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTH.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSaoKQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcDichVu
            // 
            this.grcDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDichVu.Location = new System.Drawing.Point(2, 2);
            this.grcDichVu.MainView = this.grvDichVu;
            this.grcDichVu.Name = "grcDichVu";
            this.grcDichVu.Size = new System.Drawing.Size(849, 388);
            this.grcDichVu.TabIndex = 0;
            this.grcDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDichVu});
            this.grcDichVu.Click += new System.EventHandler(this.grcDichVu_Click);
            // 
            // grvDichVu
            // 
            this.grvDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDCLS,
            this.colChon,
            this.colTenDV});
            this.grvDichVu.GridControl = this.grcDichVu;
            this.grvDichVu.Name = "grvDichVu";
            this.grvDichVu.OptionsView.ShowGroupPanel = false;
            this.grvDichVu.OptionsView.ShowViewCaption = true;
            this.grvDichVu.ViewCaption = "Danh sách dịch vụ có thể sao";
            this.grvDichVu.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDichVu_CellValueChanging);
            // 
            // colIDCLS
            // 
            this.colIDCLS.Caption = "gridColumn1";
            this.colIDCLS.FieldName = "IDCLS";
            this.colIDCLS.Name = "colIDCLS";
            // 
            // colChon
            // 
            this.colChon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colChon.AppearanceHeader.Options.UseFont = true;
            this.colChon.AppearanceHeader.Options.UseTextOptions = true;
            this.colChon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 115;
            // 
            // colTenDV
            // 
            this.colTenDV.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenDV.AppearanceHeader.Options.UseFont = true;
            this.colTenDV.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenDV.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.OptionsColumn.AllowFocus = false;
            this.colTenDV.OptionsColumn.ReadOnly = true;
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 963;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lupKPhong);
            this.panelControl1.Controls.Add(this.cbotrangthai);
            this.panelControl1.Controls.Add(this.dedenngay);
            this.panelControl1.Controls.Add(this.detungay);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(853, 37);
            this.panelControl1.TabIndex = 1;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // lupKPhong
            // 
            this.lupKPhong.Location = new System.Drawing.Point(487, 8);
            this.lupKPhong.Name = "lupKPhong";
            this.lupKPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKPhong.Properties.Appearance.Options.UseFont = true;
            this.lupKPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 100, "Tên KP")});
            this.lupKPhong.Properties.DisplayMember = "TenKP";
            this.lupKPhong.Properties.NullText = "";
            this.lupKPhong.Properties.ValueMember = "MaKP";
            this.lupKPhong.Size = new System.Drawing.Size(165, 20);
            this.lupKPhong.TabIndex = 3;
            this.lupKPhong.EditValueChanged += new System.EventHandler(this.lupKPhong_EditValueChanged);
            // 
            // cbotrangthai
            // 
            this.cbotrangthai.Location = new System.Drawing.Point(732, 8);
            this.cbotrangthai.Name = "cbotrangthai";
            this.cbotrangthai.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbotrangthai.Properties.Appearance.Options.UseFont = true;
            this.cbotrangthai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbotrangthai.Properties.Items.AddRange(new object[] {
            "Chưa làm",
            "Đã làm",
            "Cả hai"});
            this.cbotrangthai.Size = new System.Drawing.Size(103, 20);
            this.cbotrangthai.TabIndex = 2;
            this.cbotrangthai.SelectedIndexChanged += new System.EventHandler(this.cbotrangthai_SelectedIndexChanged);
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(262, 8);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(121, 20);
            this.dedenngay.TabIndex = 1;
            this.dedenngay.EditValueChanged += new System.EventHandler(this.dedenngay_EditValueChanged);
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(65, 9);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(116, 20);
            this.detungay.TabIndex = 1;
            this.detungay.EditValueChanged += new System.EventHandler(this.detungay_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(399, 10);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(82, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Khoa|Phòng:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(658, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Trạng thái:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(194, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.dateNgayCD1);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.chkSaoNhieu);
            this.panelControl2.Controls.Add(this.cboTyLe);
            this.panelControl2.Controls.Add(this.btnSaoCD);
            this.panelControl2.Controls.Add(this.dateNgayCD);
            this.panelControl2.Controls.Add(this.dateNgayTH);
            this.panelControl2.Controls.Add(this.ckcSaoKQ);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 429);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(853, 68);
            this.panelControl2.TabIndex = 2;
            // 
            // dateNgayCD1
            // 
            this.dateNgayCD1.EditValue = null;
            this.dateNgayCD1.Location = new System.Drawing.Point(432, 8);
            this.dateNgayCD1.Name = "dateNgayCD1";
            this.dateNgayCD1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNgayCD1.Properties.Appearance.Options.UseFont = true;
            this.dateNgayCD1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayCD1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayCD1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayCD1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayCD1.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayCD1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayCD1.Properties.Mask.EditMask = "g";
            this.dateNgayCD1.Size = new System.Drawing.Size(164, 20);
            this.dateNgayCD1.TabIndex = 11;
            this.dateNgayCD1.ToolTip = "Ngày chỉ định mới";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(368, 10);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(55, 14);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "Ngày CĐ:";
            // 
            // chkSaoNhieu
            // 
            this.chkSaoNhieu.Location = new System.Drawing.Point(15, 6);
            this.chkSaoNhieu.Name = "chkSaoNhieu";
            this.chkSaoNhieu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaoNhieu.Properties.Appearance.Options.UseFont = true;
            this.chkSaoNhieu.Properties.Caption = "Sao nhiều lần";
            this.chkSaoNhieu.Size = new System.Drawing.Size(116, 19);
            this.chkSaoNhieu.TabIndex = 9;
            this.chkSaoNhieu.CheckedChanged += new System.EventHandler(this.chkSaoNhieu_CheckedChanged);
            // 
            // cboTyLe
            // 
            this.cboTyLe.EditValue = "100";
            this.cboTyLe.Location = new System.Drawing.Point(432, 32);
            this.cboTyLe.Name = "cboTyLe";
            this.cboTyLe.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cboTyLe.Properties.Appearance.Options.UseFont = true;
            this.cboTyLe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTyLe.Properties.MaxLength = 3;
            this.cboTyLe.Size = new System.Drawing.Size(79, 20);
            this.cboTyLe.TabIndex = 8;
            this.cboTyLe.Visible = false;
            // 
            // btnSaoCD
            // 
            this.btnSaoCD.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaoCD.Appearance.Options.UseFont = true;
            this.btnSaoCD.Location = new System.Drawing.Point(517, 30);
            this.btnSaoCD.Name = "btnSaoCD";
            this.btnSaoCD.Size = new System.Drawing.Size(79, 23);
            this.btnSaoCD.TabIndex = 3;
            this.btnSaoCD.Text = "Sao CĐ";
            this.btnSaoCD.Click += new System.EventHandler(this.btnSaoCD_Click);
            // 
            // dateNgayCD
            // 
            this.dateNgayCD.EditValue = null;
            this.dateNgayCD.Location = new System.Drawing.Point(210, 7);
            this.dateNgayCD.Name = "dateNgayCD";
            this.dateNgayCD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNgayCD.Properties.Appearance.Options.UseFont = true;
            this.dateNgayCD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayCD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayCD.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayCD.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayCD.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayCD.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayCD.Properties.Mask.EditMask = "g";
            this.dateNgayCD.Size = new System.Drawing.Size(142, 20);
            this.dateNgayCD.TabIndex = 2;
            this.dateNgayCD.ToolTip = "Ngày chỉ định mới";
            // 
            // dateNgayTH
            // 
            this.dateNgayTH.EditValue = null;
            this.dateNgayTH.Location = new System.Drawing.Point(211, 32);
            this.dateNgayTH.Name = "dateNgayTH";
            this.dateNgayTH.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNgayTH.Properties.Appearance.Options.UseFont = true;
            this.dateNgayTH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTH.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTH.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayTH.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayTH.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateNgayTH.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayTH.Properties.Mask.EditMask = "g";
            this.dateNgayTH.Size = new System.Drawing.Size(142, 20);
            this.dateNgayTH.TabIndex = 2;
            this.dateNgayTH.ToolTip = "Ngày thực hiện mới, chỉ áp dụng cho sao kết quả";
            this.dateNgayTH.Visible = false;
            // 
            // ckcSaoKQ
            // 
            this.ckcSaoKQ.Location = new System.Drawing.Point(15, 32);
            this.ckcSaoKQ.Name = "ckcSaoKQ";
            this.ckcSaoKQ.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcSaoKQ.Properties.Appearance.Options.UseFont = true;
            this.ckcSaoKQ.Properties.Caption = "Sao cả kết quả";
            this.ckcSaoKQ.Size = new System.Drawing.Size(116, 19);
            this.ckcSaoKQ.TabIndex = 1;
            this.ckcSaoKQ.ToolTip = "Sao kết quả những chỉ định đã thực hiện";
            this.ckcSaoKQ.CheckedChanged += new System.EventHandler(this.ckcSaoKQ_CheckedChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(148, 35);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Ngày TH:";
            this.labelControl5.Visible = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(368, 34);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(50, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Tỷ lệ TT:";
            this.labelControl7.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(146, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Ngày CĐ:";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grcDichVu);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 37);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(853, 392);
            this.panelControl3.TabIndex = 3;
            // 
            // frm_SaoChiDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 497);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_SaoChiDinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_SaoChiDinh";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_SaoChiDinh_FormClosed);
            this.Load += new System.EventHandler(this.frm_SaoChiDinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbotrangthai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaoNhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTyLe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTH.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcSaoKQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDichVu;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn colIDCLS;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraEditors.ComboBoxEdit cbotrangthai;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit ckcSaoKQ;
        private DevExpress.XtraEditors.DateEdit dateNgayCD;
        private DevExpress.XtraEditors.DateEdit dateNgayTH;
        private DevExpress.XtraEditors.SimpleButton btnSaoCD;
        private DevExpress.XtraEditors.LookUpEdit lupKPhong;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit cboTyLe;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckEdit chkSaoNhieu;
        private DevExpress.XtraEditors.DateEdit dateNgayCD1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}