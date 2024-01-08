namespace QLBV.FormNhap
{
    partial class frmSoXNT_GayNghien_HTT_PhongXa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoXNT_GayNghien_HTT_PhongXa));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupTenDV = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.radMau = new DevExpress.XtraEditors.RadioGroup();
            this.chkPhanTrang = new DevExpress.XtraEditors.CheckEdit();
            this.ckGopThuocTheoNgay = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lupTieuNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lupngay = new DevExpress.XtraEditors.DateEdit();
            this.lupngaytu = new DevExpress.XtraEditors.DateEdit();
            this.lupnhathau = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPhanTrang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGopThuocTheoNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnhathau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(232, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 17);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "đến:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(201, 0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 15);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Tên thuốc:";
            this.labelControl4.Visible = false;
            // 
            // lupTenDV
            // 
            this.lupTenDV.Location = new System.Drawing.Point(262, -3);
            this.lupTenDV.Name = "lupTenDV";
            this.lupTenDV.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTenDV.Properties.Appearance.Options.UseFont = true;
            this.lupTenDV.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTenDV.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lupTenDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTenDV.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", 200, "Tên thuốc")});
            this.lupTenDV.Properties.DisplayMember = "TenDV";
            this.lupTenDV.Properties.NullText = "";
            this.lupTenDV.Properties.PopupSizeable = false;
            this.lupTenDV.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTenDV.Properties.ValueMember = "MaDV";
            this.lupTenDV.Size = new System.Drawing.Size(106, 22);
            this.lupTenDV.TabIndex = 4;
            this.lupTenDV.Visible = false;
            this.lupTenDV.EditValueChanged += new System.EventHandler(this.comboBoxEdit1_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.radMau);
            this.panelControl1.Controls.Add(this.chkPhanTrang);
            this.panelControl1.Controls.Add(this.ckGopThuocTheoNgay);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.lupTieuNhom);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.lupTenDV);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lupngay);
            this.panelControl1.Controls.Add(this.lupngaytu);
            this.panelControl1.Controls.Add(this.lupnhathau);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(400, 339);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 119);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 17);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "Khoa dược:";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.cklKP);
            this.groupControl3.Location = new System.Drawing.Point(106, 65);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(284, 122);
            this.groupControl3.TabIndex = 45;
            this.groupControl3.Text = "Chọn kho";
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.DisplayMember = "TenKP";
            this.cklKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklKP.Location = new System.Drawing.Point(2, 21);
            this.cklKP.MultiColumn = true;
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(280, 99);
            this.cklKP.TabIndex = 0;
            this.cklKP.ValueMember = "MaKP";
            this.cklKP.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKP_ItemCheck);
            // 
            // radMau
            // 
            this.radMau.Location = new System.Drawing.Point(107, 224);
            this.radMau.Name = "radMau";
            this.radMau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Sổ NXT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Báo cáo NXT")});
            this.radMau.Size = new System.Drawing.Size(281, 27);
            this.radMau.TabIndex = 5;
            this.radMau.SelectedIndexChanged += new System.EventHandler(this.radMau_SelectedIndexChanged);
            // 
            // chkPhanTrang
            // 
            this.chkPhanTrang.Location = new System.Drawing.Point(238, 257);
            this.chkPhanTrang.Name = "chkPhanTrang";
            this.chkPhanTrang.Properties.Caption = "Phân trang theo mỗi thuốc";
            this.chkPhanTrang.Size = new System.Drawing.Size(150, 19);
            this.chkPhanTrang.TabIndex = 7;
            // 
            // ckGopThuocTheoNgay
            // 
            this.ckGopThuocTheoNgay.Location = new System.Drawing.Point(106, 257);
            this.ckGopThuocTheoNgay.Name = "ckGopThuocTheoNgay";
            this.ckGopThuocTheoNgay.Properties.Caption = "Gộp thuốc theo ngày";
            this.ckGopThuocTheoNgay.Size = new System.Drawing.Size(141, 19);
            this.ckGopThuocTheoNgay.TabIndex = 6;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(12, 196);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(80, 17);
            this.labelControl7.TabIndex = 11;
            this.labelControl7.Text = "Nhóm dược:";
            // 
            // lupTieuNhom
            // 
            this.lupTieuNhom.Location = new System.Drawing.Point(106, 193);
            this.lupTieuNhom.Name = "lupTieuNhom";
            this.lupTieuNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTieuNhom.Properties.Appearance.Options.UseFont = true;
            this.lupTieuNhom.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTieuNhom.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lupTieuNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTieuNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", 200, "Tên nhóm dược"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdTieuNhom", "Name2", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupTieuNhom.Properties.DisplayMember = "TenTN";
            this.lupTieuNhom.Properties.NullText = "";
            this.lupTieuNhom.Properties.PopupSizeable = false;
            this.lupTieuNhom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTieuNhom.Properties.ValueMember = "IdTieuNhom";
            this.lupTieuNhom.Size = new System.Drawing.Size(282, 24);
            this.lupTieuNhom.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(11, 11);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 15);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Nhà thầu:";
            this.labelControl6.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(13, 38);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 17);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Ngày từ:";
            // 
            // lupngay
            // 
            this.lupngay.EditValue = null;
            this.lupngay.Location = new System.Drawing.Point(261, 35);
            this.lupngay.Name = "lupngay";
            this.lupngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngay.Properties.Appearance.Options.UseFont = true;
            this.lupngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngay.Size = new System.Drawing.Size(129, 24);
            this.lupngay.TabIndex = 2;
            // 
            // lupngaytu
            // 
            this.lupngaytu.EditValue = null;
            this.lupngaytu.Location = new System.Drawing.Point(108, 35);
            this.lupngaytu.Name = "lupngaytu";
            this.lupngaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngaytu.Properties.Appearance.Options.UseFont = true;
            this.lupngaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngaytu.Size = new System.Drawing.Size(118, 24);
            this.lupngaytu.TabIndex = 1;
            // 
            // lupnhathau
            // 
            this.lupnhathau.Location = new System.Drawing.Point(69, 9);
            this.lupnhathau.Name = "lupnhathau";
            this.lupnhathau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupnhathau.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", 200, "Tên nhà cung cấp"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCC", "Macc", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupnhathau.Properties.DisplayMember = "TenCC";
            this.lupnhathau.Properties.NullText = "";
            this.lupnhathau.Properties.ValueMember = "MaCC";
            this.lupnhathau.Size = new System.Drawing.Size(43, 20);
            this.lupnhathau.TabIndex = 10;
            this.lupnhathau.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 292);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(400, 47);
            this.panelControl2.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(195, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 29);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "TẠO SỔ";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(290, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(98, 29);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "KHÔNG TẠO";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // frmSoXNT_GayNghien_HTT_PhongXa
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 339);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSoXNT_GayNghien_HTT_PhongXa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo sổ/BC xuất nhập tồn thuốc gây nghiện, hướng thần, phóng xạ";
            this.Load += new System.EventHandler(this.frmThekho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupTenDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPhanTrang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGopThuocTheoNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnhathau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupTenDV;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.DateEdit lupngay;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit lupngaytu;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lupnhathau;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit lupTieuNhom;
        private DevExpress.XtraEditors.CheckEdit ckGopThuocTheoNgay;
        private DevExpress.XtraEditors.CheckEdit chkPhanTrang;
        private DevExpress.XtraEditors.RadioGroup radMau;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}