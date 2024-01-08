namespace QLBV.FormThamSo
{
    partial class Frm_TongHop_CLS_12121
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
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cboTT = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
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
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lup_NhomXN = new DevExpress.XtraEditors.LookUpEdit();
            this.radTrongDM = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.hyp_HuyChon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyp_Chon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.ckl_DichVu = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.chk_HT_TheBH = new DevExpress.XtraEditors.CheckEdit();
            this.chk_DV_KD = new DevExpress.XtraEditors.CheckEdit();
            this.chk_CDCLS = new DevExpress.XtraEditors.CheckEdit();
            this.cbo_DoiTuong = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadDK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_NhomXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckl_DichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_HT_TheBH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_DV_KD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_CDCLS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(13, 256);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 17);
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "Chọn K.Phòng:";
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.cklKP);
            this.groupControl1.Location = new System.Drawing.Point(127, 256);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(373, 178);
            this.groupControl1.TabIndex = 58;
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.DisplayMember = "tenkp";
            this.cklKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklKP.Location = new System.Drawing.Point(0, 0);
            this.cklKP.MultiColumn = true;
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(373, 178);
            this.cklKP.TabIndex = 2;
            this.cklKP.ValueMember = "makp";
            this.cklKP.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKP_ItemCheck);
            // 
            // cboTT
            // 
            this.cboTT.EditValue = "Đã thực hiện(lấy theo ngày thực hiện)";
            this.cboTT.Location = new System.Drawing.Point(127, 188);
            this.cboTT.Margin = new System.Windows.Forms.Padding(4);
            this.cboTT.Name = "cboTT";
            this.cboTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTT.Properties.Appearance.Options.UseFont = true;
            this.cboTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTT.Properties.Items.AddRange(new object[] {
            "Đã thực hiện(lấy theo ngày thực hiện)",
            "Đã thanh toán (lấy theo ngày thanh toán)"});
            this.cboTT.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTT.Size = new System.Drawing.Size(373, 24);
            this.cboTT.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(13, 191);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 17);
            this.labelControl3.TabIndex = 56;
            this.labelControl3.Text = "Thanh toán:";
            // 
            // ButHuy
            // 
            this.ButHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ButHuy.Appearance.Options.UseFont = true;
            this.ButHuy.Location = new System.Drawing.Point(966, 416);
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
            this.butTaoBC.Location = new System.Drawing.Point(858, 416);
            this.butTaoBC.Margin = new System.Windows.Forms.Padding(4);
            this.butTaoBC.Name = "butTaoBC";
            this.butTaoBC.Size = new System.Drawing.Size(100, 30);
            this.butTaoBC.TabIndex = 7;
            this.butTaoBC.Text = "&Tạo báo cáo";
            this.butTaoBC.Click += new System.EventHandler(this.butTaoBC_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(271, 92);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 17);
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(13, 92);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 17);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(361, 89);
            this.LupNgayden.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayden.Properties.Appearance.Options.UseFont = true;
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Size = new System.Drawing.Size(139, 24);
            this.LupNgayden.TabIndex = 2;
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(127, 89);
            this.LupNgaytu.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Size = new System.Drawing.Size(136, 24);
            this.LupNgaytu.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(13, 124);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 17);
            this.labelControl5.TabIndex = 56;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(13, 35);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(95, 17);
            this.labelControl6.TabIndex = 56;
            this.labelControl6.Text = "Nhóm dịch vụ:";
            this.labelControl6.ToolTip = "Chọn in báo cáo cận lâm sàng";
            // 
            // RadDK
            // 
            this.RadDK.Location = new System.Drawing.Point(127, 219);
            this.RadDK.Name = "RadDK";
            this.RadDK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.RadDK.Properties.Appearance.Options.UseFont = true;
            this.RadDK.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Báo cáo theo số lượng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Báo cáo theo kết quả")});
            this.RadDK.Size = new System.Drawing.Size(373, 30);
            this.RadDK.TabIndex = 6;
            // 
            // cbo_NoiTru
            // 
            this.cbo_NoiTru.EditValue = "Tất cả";
            this.cbo_NoiTru.Location = new System.Drawing.Point(361, 121);
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
            this.cbo_NoiTru.Size = new System.Drawing.Size(139, 24);
            this.cbo_NoiTru.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(271, 124);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(82, 17);
            this.labelControl7.TabIndex = 62;
            this.labelControl7.Text = "Nội|Ngoại trú:";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl8.Location = new System.Drawing.Point(13, 219);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(61, 17);
            this.labelControl8.TabIndex = 63;
            this.labelControl8.Text = "Hình thức:";
            // 
            // lup_NhomXN
            // 
            this.lup_NhomXN.EditValue = "";
            this.lup_NhomXN.Location = new System.Drawing.Point(127, 32);
            this.lup_NhomXN.Margin = new System.Windows.Forms.Padding(4);
            this.lup_NhomXN.Name = "lup_NhomXN";
            this.lup_NhomXN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lup_NhomXN.Properties.Appearance.Options.UseFont = true;
            this.lup_NhomXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_NhomXN.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenRG", "Xét nghiệm")});
            this.lup_NhomXN.Properties.DisplayMember = "TenRG";
            this.lup_NhomXN.Properties.NullText = "Chọn nhóm dịch vụ";
            this.lup_NhomXN.Properties.PopupSizeable = false;
            this.lup_NhomXN.Properties.ValueMember = "TenRG";
            this.lup_NhomXN.Size = new System.Drawing.Size(373, 24);
            this.lup_NhomXN.TabIndex = 0;
            this.lup_NhomXN.EditValueChanged += new System.EventHandler(this.lup_NhomXN_EditValueChanged);
            // 
            // radTrongDM
            // 
            this.radTrongDM.Location = new System.Drawing.Point(127, 152);
            this.radTrongDM.Name = "radTrongDM";
            this.radTrongDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTrongDM.Properties.Appearance.Options.UseFont = true;
            this.radTrongDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radTrongDM.Size = new System.Drawing.Size(373, 29);
            this.radTrongDM.TabIndex = 65;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl9.Location = new System.Drawing.Point(13, 152);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(104, 17);
            this.labelControl9.TabIndex = 66;
            this.labelControl9.Text = "Trong|Ngoài DM:";
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.Controls.Add(this.hyp_HuyChon);
            this.groupControl4.Controls.Add(this.hyp_Chon);
            this.groupControl4.Controls.Add(this.ckl_DichVu);
            this.groupControl4.Location = new System.Drawing.Point(507, 35);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(559, 374);
            this.groupControl4.TabIndex = 67;
            this.groupControl4.Text = "Dịch vụ sử dụng";
            // 
            // hyp_HuyChon
            // 
            this.hyp_HuyChon.EditValue = "Bỏ chọn";
            this.hyp_HuyChon.Location = new System.Drawing.Point(85, 349);
            this.hyp_HuyChon.Name = "hyp_HuyChon";
            this.hyp_HuyChon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_HuyChon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_HuyChon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_HuyChon.Properties.Appearance.Options.UseFont = true;
            this.hyp_HuyChon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_HuyChon.Size = new System.Drawing.Size(70, 20);
            this.hyp_HuyChon.TabIndex = 4;
            this.hyp_HuyChon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_HuyChon_OpenLink);
            // 
            // hyp_Chon
            // 
            this.hyp_Chon.EditValue = "Chọn tất cả";
            this.hyp_Chon.Location = new System.Drawing.Point(9, 349);
            this.hyp_Chon.Name = "hyp_Chon";
            this.hyp_Chon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_Chon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_Chon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_Chon.Properties.Appearance.Options.UseFont = true;
            this.hyp_Chon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_Chon.Size = new System.Drawing.Size(70, 20);
            this.hyp_Chon.TabIndex = 3;
            this.hyp_Chon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_Chon_OpenLink);
            // 
            // ckl_DichVu
            // 
            this.ckl_DichVu.Appearance.Options.UseTextOptions = true;
            this.ckl_DichVu.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ckl_DichVu.CheckOnClick = true;
            this.ckl_DichVu.ColumnWidth = 300;
            this.ckl_DichVu.DisplayMember = "TenDV";
            this.ckl_DichVu.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckl_DichVu.Location = new System.Drawing.Point(2, 20);
            this.ckl_DichVu.MultiColumn = true;
            this.ckl_DichVu.Name = "ckl_DichVu";
            this.ckl_DichVu.Size = new System.Drawing.Size(555, 322);
            this.ckl_DichVu.TabIndex = 1;
            this.ckl_DichVu.ValueMember = "MaDV";
            // 
            // chk_HT_TheBH
            // 
            this.chk_HT_TheBH.Location = new System.Drawing.Point(507, 415);
            this.chk_HT_TheBH.Name = "chk_HT_TheBH";
            this.chk_HT_TheBH.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_HT_TheBH.Properties.Appearance.Options.UseFont = true;
            this.chk_HT_TheBH.Properties.Caption = "Hiển thị số thẻ BHYT";
            this.chk_HT_TheBH.Size = new System.Drawing.Size(171, 20);
            this.chk_HT_TheBH.TabIndex = 68;
            // 
            // chk_DV_KD
            // 
            this.chk_DV_KD.Location = new System.Drawing.Point(329, 61);
            this.chk_DV_KD.Name = "chk_DV_KD";
            this.chk_DV_KD.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_DV_KD.Properties.Appearance.Options.UseFont = true;
            this.chk_DV_KD.Properties.Caption = "Dịch vụ nhập trực tiếp";
            this.chk_DV_KD.Size = new System.Drawing.Size(171, 20);
            this.chk_DV_KD.TabIndex = 69;
            this.chk_DV_KD.CheckedChanged += new System.EventHandler(this.chk_DV_KD_CheckedChanged);
            // 
            // chk_CDCLS
            // 
            this.chk_CDCLS.EditValue = true;
            this.chk_CDCLS.Location = new System.Drawing.Point(125, 61);
            this.chk_CDCLS.Name = "chk_CDCLS";
            this.chk_CDCLS.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_CDCLS.Properties.Appearance.Options.UseFont = true;
            this.chk_CDCLS.Properties.Caption = "Dịch vụ qua chỉ định CLS";
            this.chk_CDCLS.Size = new System.Drawing.Size(171, 20);
            this.chk_CDCLS.TabIndex = 70;
            // 
            // cbo_DoiTuong
            // 
            this.cbo_DoiTuong.EditValue = "Tất cả";
            this.cbo_DoiTuong.Location = new System.Drawing.Point(127, 121);
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
            this.cbo_DoiTuong.Size = new System.Drawing.Size(136, 24);
            this.cbo_DoiTuong.TabIndex = 64;
            // 
            // Frm_TongHop_CLS
            // 
            this.AcceptButton = this.butTaoBC;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 472);
            this.Controls.Add(this.chk_CDCLS);
            this.Controls.Add(this.chk_DV_KD);
            this.Controls.Add(this.chk_HT_TheBH);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.radTrongDM);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.cbo_NoiTru);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.RadDK);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cboTT);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.ButHuy);
            this.Controls.Add(this.butTaoBC);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.Controls.Add(this.lup_NhomXN);
            this.Controls.Add(this.cbo_DoiTuong);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TongHop_CLS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp số lượng-kết quả cận lâm sàng";
            this.Load += new System.EventHandler(this.Frm_ThHoaSinhMauSL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadDK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_NhomXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckl_DichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_HT_TheBH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_DV_KD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_CDCLS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboTT;
        private DevExpress.XtraEditors.LabelControl labelControl3;
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
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lup_NhomXN;
        private DevExpress.XtraEditors.RadioGroup radTrongDM;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.CheckedListBoxControl ckl_DichVu;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_HuyChon;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_Chon;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.CheckEdit chk_HT_TheBH;
        private DevExpress.XtraEditors.CheckEdit chk_DV_KD;
        private DevExpress.XtraEditors.CheckEdit chk_CDCLS;
        private DevExpress.XtraEditors.LookUpEdit cbo_DoiTuong;
    }
}