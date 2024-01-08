namespace QLBV.FormThamSo
{
    partial class frmThongKeTTPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongKeTTPT));
            this.dtThang = new DevExpress.XtraEditors.DateEdit();
            this.radiStatus = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.cboTieuNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.chkhienbenh = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkhienbenh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dtThang
            // 
            this.dtThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtThang.EditValue = null;
            this.dtThang.Location = new System.Drawing.Point(2, 21);
            this.dtThang.Margin = new System.Windows.Forms.Padding(4);
            this.dtThang.Name = "dtThang";
            this.dtThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtThang.Properties.Appearance.Options.UseFont = true;
            this.dtThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtThang.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtThang.Properties.EditFormat.FormatString = "MM/yyyy";
            this.dtThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtThang.Properties.Mask.EditMask = "MM/yyyy";
            this.dtThang.Size = new System.Drawing.Size(177, 26);
            this.dtThang.TabIndex = 0;
            // 
            // radiStatus
            // 
            this.radiStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radiStatus.Location = new System.Drawing.Point(2, 21);
            this.radiStatus.Margin = new System.Windows.Forms.Padding(4);
            this.radiStatus.Name = "radiStatus";
            this.radiStatus.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiStatus.Properties.Appearance.Options.UseFont = true;
            this.radiStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chưa thực hiện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Đã thực hiện")});
            this.radiStatus.Size = new System.Drawing.Size(374, 44);
            this.radiStatus.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cklKP);
            this.groupControl1.Location = new System.Drawing.Point(6, 180);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(378, 152);
            this.groupControl1.TabIndex = 509;
            this.groupControl1.Text = "Khoa|Phòng";
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.DisplayMember = "TenKP";
            this.cklKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklKP.Location = new System.Drawing.Point(2, 21);
            this.cklKP.MultiColumn = true;
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(374, 129);
            this.cklKP.TabIndex = 509;
            this.cklKP.ValueMember = "MaKP";
            this.cklKP.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKP_ItemCheck);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dtThang);
            this.groupControl2.Location = new System.Drawing.Point(4, 8);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(181, 50);
            this.groupControl2.TabIndex = 510;
            this.groupControl2.Text = "Tháng";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.radiStatus);
            this.groupControl3.Location = new System.Drawing.Point(6, 111);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(378, 67);
            this.groupControl3.TabIndex = 511;
            this.groupControl3.Text = "Trạng thái";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.cboTieuNhom);
            this.groupControl4.Location = new System.Drawing.Point(6, 59);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(380, 50);
            this.groupControl4.TabIndex = 514;
            this.groupControl4.Text = "dịch vụ thủ thuật - phẫu thuật";
            // 
            // cboTieuNhom
            // 
            this.cboTieuNhom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTieuNhom.Location = new System.Drawing.Point(2, 21);
            this.cboTieuNhom.Margin = new System.Windows.Forms.Padding(4);
            this.cboTieuNhom.Name = "cboTieuNhom";
            this.cboTieuNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTieuNhom.Properties.Appearance.Options.UseFont = true;
            this.cboTieuNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTieuNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", 200, "Tên DV"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDV", "Mã", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.cboTieuNhom.Properties.DisplayFormat.FormatString = "y";
            this.cboTieuNhom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboTieuNhom.Properties.DisplayMember = "TenDV";
            this.cboTieuNhom.Properties.EditFormat.FormatString = "y";
            this.cboTieuNhom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboTieuNhom.Properties.NullText = "";
            this.cboTieuNhom.Properties.PopupFormMinSize = new System.Drawing.Size(200, 0);
            this.cboTieuNhom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboTieuNhom.Properties.ValueMember = "MaDV";
            this.cboTieuNhom.Size = new System.Drawing.Size(376, 26);
            this.cboTieuNhom.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(290, 336);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(92, 40);
            this.btnThoat.TabIndex = 513;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(184, 336);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 40);
            this.btnOK.TabIndex = 512;
            this.btnOK.Text = "Tạo báo &cáo";
            this.btnOK.ToolTipTitle = "jhgjgjhj";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.chkhienbenh);
            this.groupControl5.Location = new System.Drawing.Point(190, 8);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(196, 50);
            this.groupControl5.TabIndex = 515;
            this.groupControl5.Text = "Hiện căn bệnh";
            // 
            // chkhienbenh
            // 
            this.chkhienbenh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkhienbenh.EditValue = null;
            this.chkhienbenh.Location = new System.Drawing.Point(2, 21);
            this.chkhienbenh.Margin = new System.Windows.Forms.Padding(4);
            this.chkhienbenh.Name = "chkhienbenh";
            this.chkhienbenh.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkhienbenh.Properties.Appearance.Options.UseFont = true;
            this.chkhienbenh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkhienbenh.Properties.Caption = "Hiển thị căn bệnh";
            this.chkhienbenh.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.chkhienbenh.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.chkhienbenh.Properties.EditFormat.FormatString = "MM/yyyy";
            this.chkhienbenh.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.chkhienbenh.Size = new System.Drawing.Size(192, 23);
            this.chkhienbenh.TabIndex = 0;
            // 
            // frmThongKeTTPT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 376);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThongKeTTPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê số lần thủ thuật - phẫu thuật";
            this.Load += new System.EventHandler(this.frmThongKeTTPT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkhienbenh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtThang;
        private DevExpress.XtraEditors.RadioGroup radiStatus;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.CheckEdit chkhienbenh;
        private DevExpress.XtraEditors.LookUpEdit cboTieuNhom;
    }
}