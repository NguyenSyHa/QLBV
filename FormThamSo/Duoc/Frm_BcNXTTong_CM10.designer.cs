namespace QLBV.FormThamSo
{
    partial class Frm_BcNXTTong_CM10
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BcNXTTong_CM10));
            this.label1 = new System.Windows.Forms.Label();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkTheoHD = new DevExpress.XtraEditors.CheckEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.labNhaCC = new System.Windows.Forms.Label();
            this.lupNhaCC = new DevExpress.XtraEditors.LookUpEdit();
            this.chkHienThi = new DevExpress.XtraEditors.CheckEdit();
            this.lupPL = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTheoHD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienThi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupPL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(228, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "Phân loại:";
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(84, 82);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã Khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.PopupSizeable = false;
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(139, 20);
            this.lupKho.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(228, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Từ ngày:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(304, 144);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(107, 23);
            this.btnHuy.TabIndex = 73;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(84, 144);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(107, 23);
            this.btnInBC.TabIndex = 72;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(304, 56);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateDenNgay.Size = new System.Drawing.Size(139, 20);
            this.dateDenNgay.TabIndex = 69;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(84, 56);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateTuNgay.Size = new System.Drawing.Size(139, 20);
            this.dateTuNgay.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(14, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 16);
            this.label5.TabIndex = 78;
            this.label5.Text = "Báo cáo Nhập xuất tồn dành cho các kho ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(14, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(308, 16);
            this.label6.TabIndex = 78;
            this.label6.Text = "có nghiệp vụ Xuất các xã và xuất các kho  nội bộ BV!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(14, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 16);
            this.label7.TabIndex = 78;
            // 
            // chkTheoHD
            // 
            this.chkTheoHD.Location = new System.Drawing.Point(296, 3);
            this.chkTheoHD.Name = "chkTheoHD";
            this.chkTheoHD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkTheoHD.Properties.Appearance.Options.UseFont = true;
            this.chkTheoHD.Properties.Caption = "theo hóa đơn";
            this.chkTheoHD.Size = new System.Drawing.Size(141, 19);
            this.chkTheoHD.TabIndex = 79;
            this.chkTheoHD.Visible = false;
            this.chkTheoHD.CheckedChanged += new System.EventHandler(this.chkTheoHD_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "Kho:";
            // 
            // labNhaCC
            // 
            this.labNhaCC.AutoSize = true;
            this.labNhaCC.Location = new System.Drawing.Point(9, 111);
            this.labNhaCC.Name = "labNhaCC";
            this.labNhaCC.Size = new System.Drawing.Size(65, 13);
            this.labNhaCC.TabIndex = 82;
            this.labNhaCC.Text = "Bỏ nhà CC:";
            // 
            // lupNhaCC
            // 
            this.lupNhaCC.Location = new System.Drawing.Point(84, 108);
            this.lupNhaCC.Name = "lupNhaCC";
            this.lupNhaCC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhaCC.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", 200, "Tên nhà cung cấp")});
            this.lupNhaCC.Properties.DisplayMember = "TenCC";
            this.lupNhaCC.Properties.NullText = "";
            this.lupNhaCC.Properties.PopupSizeable = false;
            this.lupNhaCC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNhaCC.Properties.ValueMember = "MaCC";
            this.lupNhaCC.Size = new System.Drawing.Size(139, 20);
            this.lupNhaCC.TabIndex = 81;
            // 
            // chkHienThi
            // 
            this.chkHienThi.Location = new System.Drawing.Point(302, 108);
            this.chkHienThi.Name = "chkHienThi";
            this.chkHienThi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkHienThi.Properties.Appearance.Options.UseFont = true;
            this.chkHienThi.Properties.Caption = "Hiển thị tên nhóm";
            this.chkHienThi.Size = new System.Drawing.Size(135, 19);
            this.chkHienThi.TabIndex = 93;
            // 
            // lupPL
            // 
            this.lupPL.Location = new System.Drawing.Point(304, 82);
            this.lupPL.Name = "lupPL";
            this.lupPL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupPL.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhomCT", "tennhom", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", "TenNhom")});
            this.lupPL.Properties.DisplayMember = "TenNhom";
            this.lupPL.Properties.NullText = "";
            this.lupPL.Properties.PopupSizeable = false;
            this.lupPL.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupPL.Properties.ValueMember = "TenNhomCT";
            this.lupPL.Size = new System.Drawing.Size(139, 20);
            this.lupPL.TabIndex = 71;
            // 
            // Frm_BcNXTTong_CM10
            // 
            this.AcceptButton = this.btnHuy;
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 199);
            this.Controls.Add(this.chkHienThi);
            this.Controls.Add(this.labNhaCC);
            this.Controls.Add(this.lupNhaCC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkTheoHD);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.dateTuNgay);
            this.Controls.Add(this.lupPL);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BcNXTTong_CM10";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Báo cáo NXT kho tổng";
            this.Load += new System.EventHandler(this.Frm_BcNXTTong_CM10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTheoHD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienThi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupPL.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.CheckEdit chkTheoHD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labNhaCC;
        private DevExpress.XtraEditors.LookUpEdit lupNhaCC;
        private DevExpress.XtraEditors.CheckEdit chkHienThi;
        private DevExpress.XtraEditors.LookUpEdit lupPL;
    }
}