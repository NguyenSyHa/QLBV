
namespace QLBV.FormThamSo
{
    partial class frm_TheoDoi_VaoRaChuyenVien
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
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.Taoso = new DevExpress.XtraEditors.SimpleButton();
            this.Huy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LupNgayTu = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayDen = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.LupKhoaPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.rdCKhoan = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupKhoaPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdCKhoan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.rdCKhoan);
            this.panelControl3.Controls.Add(this.Taoso);
            this.panelControl3.Controls.Add(this.Huy);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.LupNgayTu);
            this.panelControl3.Controls.Add(this.LupNgayDen);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(312, 145);
            this.panelControl3.TabIndex = 23;
            // 
            // Taoso
            // 
            this.Taoso.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Taoso.Appearance.Options.UseFont = true;
            this.Taoso.Location = new System.Drawing.Point(127, 107);
            this.Taoso.Name = "Taoso";
            this.Taoso.Size = new System.Drawing.Size(87, 26);
            this.Taoso.TabIndex = 8;
            this.Taoso.Text = "&Tạo sổ";
            this.Taoso.Click += new System.EventHandler(this.Taoso_Click);
            // 
            // Huy
            // 
            this.Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Huy.Appearance.Options.UseFont = true;
            this.Huy.Location = new System.Drawing.Point(220, 107);
            this.Huy.Name = "Huy";
            this.Huy.Size = new System.Drawing.Size(87, 26);
            this.Huy.TabIndex = 9;
            this.Huy.Text = "&Hủy";
            this.Huy.Click += new System.EventHandler(this.Huy_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(30, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // LupNgayTu
            // 
            this.LupNgayTu.EditValue = null;
            this.LupNgayTu.Location = new System.Drawing.Point(128, 8);
            this.LupNgayTu.Name = "LupNgayTu";
            this.LupNgayTu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayTu.Properties.Appearance.Options.UseFont = true;
            this.LupNgayTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayTu.Size = new System.Drawing.Size(179, 24);
            this.LupNgayTu.TabIndex = 0;
            // 
            // LupNgayDen
            // 
            this.LupNgayDen.EditValue = null;
            this.LupNgayDen.Location = new System.Drawing.Point(128, 38);
            this.LupNgayDen.Name = "LupNgayDen";
            this.LupNgayDen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayDen.Properties.Appearance.Options.UseFont = true;
            this.LupNgayDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayDen.Size = new System.Drawing.Size(179, 24);
            this.LupNgayDen.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(30, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // LupKhoaPhong
            // 
            this.LupKhoaPhong.Location = new System.Drawing.Point(6, 32);
            this.LupKhoaPhong.Name = "LupKhoaPhong";
            this.LupKhoaPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupKhoaPhong.Properties.Appearance.Options.UseFont = true;
            this.LupKhoaPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupKhoaPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "MaKP", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.LupKhoaPhong.Properties.DisplayMember = "TenKP";
            this.LupKhoaPhong.Properties.NullText = "Chọn Khoa|Phòng";
            this.LupKhoaPhong.Properties.ValueMember = "MaKP";
            this.LupKhoaPhong.Size = new System.Drawing.Size(157, 24);
            this.LupKhoaPhong.TabIndex = 24;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.LupKhoaPhong);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(312, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(168, 145);
            this.panelControl1.TabIndex = 25;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(5, 9);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(119, 17);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "Chọn Khoa Phòng";
            // 
            // rdCKhoan
            // 
            this.rdCKhoan.Location = new System.Drawing.Point(127, 68);
            this.rdCKhoan.Name = "rdCKhoan";
            this.rdCKhoan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdCKhoan.Properties.Appearance.Options.UseFont = true;
            this.rdCKhoan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngày vào viện")});
            this.rdCKhoan.Size = new System.Drawing.Size(120, 25);
            this.rdCKhoan.TabIndex = 524;
            // 
            // frm_TheoDoi_VaoRaChuyenVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 145);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "frm_TheoDoi_VaoRaChuyenVien";
            this.Text = "frm_TheoDoi_VaoRaChuyenVien";
            this.Load += new System.EventHandler(this.frm_TheoDoi_VaoRaChuyenVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupKhoaPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdCKhoan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit LupNgayTu;
        private DevExpress.XtraEditors.DateEdit LupNgayDen;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton Taoso;
        private DevExpress.XtraEditors.SimpleButton Huy;
        private DevExpress.XtraEditors.LookUpEdit LupKhoaPhong;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup rdCKhoan;
    }
}