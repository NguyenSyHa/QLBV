namespace QLBV.FormThamSo
{
    partial class frm_TKDichVuKhongThuTien
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ButHuy = new DevExpress.XtraEditors.SimpleButton();
            this.butTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.RadDK = new DevExpress.XtraEditors.RadioGroup();
            this.cbo_NoiTru = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lup_DTuongBN = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lup_NhomXN = new DevExpress.XtraEditors.LookUpEdit();
            this.cbo_loaiCP = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.chk_TT = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadDK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_DTuongBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_NhomXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_loaiCP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_TT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(13, 201);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(101, 17);
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "Chọn K.Phòng:";
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(120, 201);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(373, 178);
            this.groupControl1.TabIndex = 58;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcKhoaphong.Location = new System.Drawing.Point(0, 0);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Margin = new System.Windows.Forms.Padding(4);
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaphong.Size = new System.Drawing.Size(373, 178);
            this.grcKhoaphong.TabIndex = 0;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvKhoaphong.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging);
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 46;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên Khoa|Phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 305;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // ButHuy
            // 
            this.ButHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ButHuy.Appearance.Options.UseFont = true;
            this.ButHuy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ButHuy.Location = new System.Drawing.Point(393, 4);
            this.ButHuy.Margin = new System.Windows.Forms.Padding(4);
            this.ButHuy.Name = "ButHuy";
            this.ButHuy.Size = new System.Drawing.Size(100, 30);
            this.ButHuy.TabIndex = 8;
            this.ButHuy.Text = "&Hủy";
            this.ButHuy.Click += new System.EventHandler(this.ButHuy_Click);
            // 
            // butTaoBC
            // 
            this.butTaoBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.butTaoBC.Appearance.Options.UseFont = true;
            this.butTaoBC.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.butTaoBC.Location = new System.Drawing.Point(285, 4);
            this.butTaoBC.Margin = new System.Windows.Forms.Padding(4);
            this.butTaoBC.Name = "butTaoBC";
            this.butTaoBC.Size = new System.Drawing.Size(100, 30);
            this.butTaoBC.TabIndex = 7;
            this.butTaoBC.Text = "&Tạo báo cáo";
            this.butTaoBC.Click += new System.EventHandler(this.butTaoBC_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(264, 72);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 17);
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(13, 72);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(359, 69);
            this.LupNgayden.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayden.Properties.Appearance.Options.UseFont = true;
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgayden.Size = new System.Drawing.Size(134, 24);
            this.LupNgayden.TabIndex = 2;
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(120, 69);
            this.LupNgaytu.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgaytu.Size = new System.Drawing.Size(136, 24);
            this.LupNgaytu.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(13, 104);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 17);
            this.labelControl5.TabIndex = 56;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(13, 40);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(80, 17);
            this.labelControl6.TabIndex = 56;
            this.labelControl6.Text = "Nhóm dược:";
            this.labelControl6.ToolTip = "Chọn in báo cáo cận lâm sàng";
            // 
            // RadDK
            // 
            this.RadDK.Location = new System.Drawing.Point(120, 164);
            this.RadDK.Name = "RadDK";
            this.RadDK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.RadDK.Properties.Appearance.Options.UseFont = true;
            this.RadDK.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Báo cáo theo số lượng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Báo cáo theo thành tiền")});
            this.RadDK.Size = new System.Drawing.Size(373, 30);
            this.RadDK.TabIndex = 6;
            // 
            // cbo_NoiTru
            // 
            this.cbo_NoiTru.EditValue = "Tất cả";
            this.cbo_NoiTru.Location = new System.Drawing.Point(359, 101);
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
            this.cbo_NoiTru.Size = new System.Drawing.Size(134, 24);
            this.cbo_NoiTru.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(264, 104);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(92, 17);
            this.labelControl7.TabIndex = 62;
            this.labelControl7.Text = "Nội|Ngoại trú:";
            // 
            // lup_DTuongBN
            // 
            this.lup_DTuongBN.EditValue = "Tất cả";
            this.lup_DTuongBN.Location = new System.Drawing.Point(120, 101);
            this.lup_DTuongBN.Margin = new System.Windows.Forms.Padding(4);
            this.lup_DTuongBN.Name = "lup_DTuongBN";
            this.lup_DTuongBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lup_DTuongBN.Properties.Appearance.Options.UseFont = true;
            this.lup_DTuongBN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_DTuongBN.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng")});
            this.lup_DTuongBN.Properties.DisplayMember = "DTBN1";
            this.lup_DTuongBN.Properties.NullText = "";
            this.lup_DTuongBN.Properties.PopupSizeable = false;
            this.lup_DTuongBN.Properties.ValueMember = "IDDTBN";
            this.lup_DTuongBN.Size = new System.Drawing.Size(136, 24);
            this.lup_DTuongBN.TabIndex = 3;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl8.Location = new System.Drawing.Point(13, 164);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(70, 17);
            this.labelControl8.TabIndex = 63;
            this.labelControl8.Text = "Hình thức:";
            // 
            // lup_NhomXN
            // 
            this.lup_NhomXN.Location = new System.Drawing.Point(120, 37);
            this.lup_NhomXN.Margin = new System.Windows.Forms.Padding(4);
            this.lup_NhomXN.Name = "lup_NhomXN";
            this.lup_NhomXN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lup_NhomXN.Properties.Appearance.Options.UseFont = true;
            this.lup_NhomXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_NhomXN.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhomCT", "Nhóm dược")});
            this.lup_NhomXN.Properties.DisplayMember = "TenNhomCT";
            this.lup_NhomXN.Properties.NullText = "";
            this.lup_NhomXN.Properties.PopupSizeable = false;
            this.lup_NhomXN.Properties.ValueMember = "TenNhomCT";
            this.lup_NhomXN.Size = new System.Drawing.Size(373, 24);
            this.lup_NhomXN.TabIndex = 0;
            // 
            // cbo_loaiCP
            // 
            this.cbo_loaiCP.EditValue = "Cả hai";
            this.cbo_loaiCP.Location = new System.Drawing.Point(359, 133);
            this.cbo_loaiCP.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_loaiCP.Name = "cbo_loaiCP";
            this.cbo_loaiCP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_loaiCP.Properties.Appearance.Options.UseFont = true;
            this.cbo_loaiCP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_loaiCP.Properties.Items.AddRange(new object[] {
            "Không thu tiền",
            "Thu tiền",
            "Cả hai"});
            this.cbo_loaiCP.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_loaiCP.Size = new System.Drawing.Size(134, 24);
            this.cbo_loaiCP.TabIndex = 65;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl9.Location = new System.Drawing.Point(264, 135);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(58, 17);
            this.labelControl9.TabIndex = 66;
            this.labelControl9.Text = "Loại CP:";
            // 
            // chk_TT
            // 
            this.chk_TT.Location = new System.Drawing.Point(120, 133);
            this.chk_TT.Margin = new System.Windows.Forms.Padding(4);
            this.chk_TT.Name = "chk_TT";
            this.chk_TT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_TT.Properties.Appearance.Options.UseFont = true;
            this.chk_TT.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chk_TT.Properties.Caption = "Đã thanh toán";
            this.chk_TT.Size = new System.Drawing.Size(136, 22);
            this.chk_TT.TabIndex = 5;
            // 
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textEdit1.EditValue = "Bảng kê thuốc, VTYT tiêu hao không thanh toán";
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(516, 26);
            this.textEdit1.TabIndex = 67;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.ButHuy);
            this.panelControl1.Controls.Add(this.butTaoBC);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 404);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(516, 61);
            this.panelControl1.TabIndex = 68;
            // 
            // frm_TKDichVuKhongThuTien
            // 
            this.AcceptButton = this.butTaoBC;
            this.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 465);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.cbo_loaiCP);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.cbo_NoiTru);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.RadDK);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.Controls.Add(this.lup_DTuongBN);
            this.Controls.Add(this.lup_NhomXN);
            this.Controls.Add(this.chk_TT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TKDichVuKhongThuTien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "bảng kê thuốc, VTYT không thanh toán";
            this.Load += new System.EventHandler(this.Frm_ThHoaSinhMauSL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadDK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_DTuongBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_NhomXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_loaiCP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_TT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.SimpleButton ButHuy;
        private DevExpress.XtraEditors.SimpleButton butTaoBC;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup RadDK;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_NoiTru;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit lup_DTuongBN;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lup_NhomXN;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_loaiCP;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.CheckEdit chk_TT;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}