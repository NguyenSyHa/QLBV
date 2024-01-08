namespace QLBV.FormThamSo
{
    partial class frm_rep79aCT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_rep79aCT));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.lupngayden = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ckBC = new DevExpress.XtraEditors.CheckEdit();
            this.lupKhoaphong = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.chkNhandan = new DevExpress.XtraEditors.CheckEdit();
            this.Xuatex = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cbosx = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkGT = new DevExpress.XtraEditors.CheckEdit();
            this.rdFont = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNhandan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xuatex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbosx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(180, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "Tạo báo &cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(285, 200);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(89, 23);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 15);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(220, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 15);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(83, 52);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgaytu.Size = new System.Drawing.Size(124, 22);
            this.lupNgaytu.TabIndex = 4;
            // 
            // lupngayden
            // 
            this.lupngayden.EditValue = null;
            this.lupngayden.Location = new System.Drawing.Point(275, 52);
            this.lupngayden.Name = "lupngayden";
            this.lupngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden.Properties.Appearance.Options.UseFont = true;
            this.lupngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupngayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupngayden.Size = new System.Drawing.Size(148, 22);
            this.lupngayden.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(12, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 15);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Khoa phòng:";
            // 
            // ckBC
            // 
            this.ckBC.Location = new System.Drawing.Point(273, 83);
            this.ckBC.Name = "ckBC";
            this.ckBC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ckBC.Properties.Appearance.Options.UseFont = true;
            this.ckBC.Properties.Caption = "Báo cáo theo quý";
            this.ckBC.Size = new System.Drawing.Size(136, 20);
            this.ckBC.TabIndex = 8;
            // 
            // lupKhoaphong
            // 
            this.lupKhoaphong.Location = new System.Drawing.Point(83, 80);
            this.lupKhoaphong.Name = "lupKhoaphong";
            this.lupKhoaphong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoaphong.Properties.Appearance.Options.UseFont = true;
            this.lupKhoaphong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoaphong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "MaKP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoaphong.Properties.DisplayMember = "TenKP";
            this.lupKhoaphong.Properties.NullText = "";
            this.lupKhoaphong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoaphong.Properties.ValueMember = "MaKP";
            this.lupKhoaphong.Size = new System.Drawing.Size(125, 22);
            this.lupKhoaphong.TabIndex = 6;
            this.lupKhoaphong.EditValueChanged += new System.EventHandler(this.lupKhoaphong_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(139, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "TẠO BÁO CÁO 79A";
            // 
            // chkNhandan
            // 
            this.chkNhandan.Location = new System.Drawing.Point(83, 108);
            this.chkNhandan.Name = "chkNhandan";
            this.chkNhandan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkNhandan.Properties.Appearance.Options.UseFont = true;
            this.chkNhandan.Properties.Caption = "Bệnh nhân nhân dân";
            this.chkNhandan.Size = new System.Drawing.Size(145, 20);
            this.chkNhandan.TabIndex = 10;
            // 
            // Xuatex
            // 
            this.Xuatex.Location = new System.Drawing.Point(81, 164);
            this.Xuatex.Name = "Xuatex";
            this.Xuatex.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Xuatex.Properties.Appearance.Options.UseFont = true;
            this.Xuatex.Properties.Caption = "Xuất ra Excel";
            this.Xuatex.Size = new System.Drawing.Size(136, 20);
            this.Xuatex.TabIndex = 11;
            this.Xuatex.CheckedChanged += new System.EventHandler(this.Xuatex_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(12, 134);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 15);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Sắp xếp:";
            // 
            // cbosx
            // 
            this.cbosx.EditValue = "Nhóm đối tượng";
            this.cbosx.Location = new System.Drawing.Point(83, 133);
            this.cbosx.Name = "cbosx";
            this.cbosx.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbosx.Properties.Appearance.Options.UseFont = true;
            this.cbosx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbosx.Properties.Items.AddRange(new object[] {
            "",
            "Mã bệnh nhân (Số phiếu)",
            "Nhóm đối tượng",
            "Ngày thanh toán"});
            this.cbosx.Size = new System.Drawing.Size(340, 22);
            this.cbosx.TabIndex = 12;
            // 
            // chkGT
            // 
            this.chkGT.Location = new System.Drawing.Point(273, 109);
            this.chkGT.Name = "chkGT";
            this.chkGT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkGT.Properties.Appearance.Options.UseFont = true;
            this.chkGT.Properties.Caption = "Tách giới tính/ năm sinh";
            this.chkGT.Size = new System.Drawing.Size(154, 20);
            this.chkGT.TabIndex = 14;
            // 
            // rdFont
            // 
            this.rdFont.Location = new System.Drawing.Point(226, 161);
            this.rdFont.Name = "rdFont";
            this.rdFont.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "TCVN3"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Unicode")});
            this.rdFont.Size = new System.Drawing.Size(197, 23);
            this.rdFont.TabIndex = 15;
            this.rdFont.SelectedIndexChanged += new System.EventHandler(this.rdFont_SelectedIndexChanged);
            // 
            // frm_rep79aCT
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 238);
            this.Controls.Add(this.rdFont);
            this.Controls.Add(this.chkGT);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.Xuatex);
            this.Controls.Add(this.chkNhandan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckBC);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupngayden);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lupKhoaphong);
            this.Controls.Add(this.cbosx);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_rep79aCT";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê 79a";
            this.Load += new System.EventHandler(this.frm_rep79aCT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNhandan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xuatex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbosx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.DateEdit lupngayden;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckBC;
        private DevExpress.XtraEditors.LookUpEdit lupKhoaphong;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit chkNhandan;
        private DevExpress.XtraEditors.CheckEdit Xuatex;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cbosx;
        private DevExpress.XtraEditors.CheckEdit chkGT;
        private DevExpress.XtraEditors.RadioGroup rdFont;
    }
}