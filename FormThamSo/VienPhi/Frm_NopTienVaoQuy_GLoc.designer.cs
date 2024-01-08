namespace QLBV.FormThamSo
{
    partial class Frm_NopTienVaoQuy_GLoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NopTienVaoQuy_GLoc));
            this.DoiTuong = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.radioGroupLoaiThu = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.rgChonMauBCct = new DevExpress.XtraEditors.RadioGroup();
            this.lupkhoaphong = new DevExpress.XtraEditors.LookUpEdit();
            this.lupDTBN = new DevExpress.XtraEditors.LookUpEdit();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.dateNgayTu = new DevExpress.XtraEditors.DateEdit();
            this.lupCanBo = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLoaiThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMauBCct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoaphong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDTBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCanBo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DoiTuong
            // 
            this.DoiTuong.AutoSize = true;
            this.DoiTuong.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.DoiTuong.Location = new System.Drawing.Point(10, 76);
            this.DoiTuong.Name = "DoiTuong";
            this.DoiTuong.Size = new System.Drawing.Size(70, 17);
            this.DoiTuong.TabIndex = 65;
            this.DoiTuong.Text = "Đối tượng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(9, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 63;
            this.label1.Text = "Nhân viên BC:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 62;
            this.label2.Text = "Ngày từ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(10, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 67;
            this.label3.Text = "Ngày đến:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(10, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 65;
            this.label4.Text = "Khoa|Phòng";
            this.label4.Visible = false;
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.ImageOptions.Image")));
            this.btnInBC.Location = new System.Drawing.Point(219, 307);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(92, 30);
            this.btnInBC.TabIndex = 60;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // radioGroupLoaiThu
            // 
            this.radioGroupLoaiThu.Location = new System.Drawing.Point(13, 255);
            this.radioGroupLoaiThu.Name = "radioGroupLoaiThu";
            this.radioGroupLoaiThu.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thu thường"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thu ngân hàng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chuyển khoản"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radioGroupLoaiThu.Size = new System.Drawing.Size(396, 30);
            this.radioGroupLoaiThu.TabIndex = 72;
            this.radioGroupLoaiThu.Visible = false;
            this.radioGroupLoaiThu.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 224);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu tổng hợp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu chi tiết")});
            this.radioGroup1.Size = new System.Drawing.Size(397, 25);
            this.radioGroup1.TabIndex = 72;
            this.radioGroup1.Visible = false;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // rgChonMauBCct
            // 
            this.rgChonMauBCct.Location = new System.Drawing.Point(12, 167);
            this.rgChonMauBCct.Name = "rgChonMauBCct";
            this.rgChonMauBCct.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgChonMauBCct.Properties.Appearance.Options.UseFont = true;
            this.rgChonMauBCct.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nộp tiền vào quỹ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tổng hợp tiền thu theo ca")});
            this.rgChonMauBCct.Size = new System.Drawing.Size(397, 56);
            this.rgChonMauBCct.TabIndex = 71;
            this.rgChonMauBCct.SelectedIndexChanged += new System.EventHandler(this.rgChonMauBCct_SelectedIndexChanged);
            // 
            // lupkhoaphong
            // 
            this.lupkhoaphong.Location = new System.Drawing.Point(110, 137);
            this.lupkhoaphong.Name = "lupkhoaphong";
            this.lupkhoaphong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupkhoaphong.Properties.Appearance.Options.UseFont = true;
            this.lupkhoaphong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupkhoaphong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên KP", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lupkhoaphong.Properties.DisplayMember = "TenKP";
            this.lupkhoaphong.Properties.NullText = "";
            this.lupkhoaphong.Properties.ValueMember = "MaKP";
            this.lupkhoaphong.Size = new System.Drawing.Size(299, 24);
            this.lupkhoaphong.TabIndex = 70;
            this.lupkhoaphong.Visible = false;
            this.lupkhoaphong.EditValueChanged += new System.EventHandler(this.lupkhoaphong_EditValueChanged);
            // 
            // lupDTBN
            // 
            this.lupDTBN.Location = new System.Drawing.Point(110, 73);
            this.lupDTBN.Name = "lupDTBN";
            this.lupDTBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDTBN.Properties.Appearance.Options.UseFont = true;
            this.lupDTBN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDTBN.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lupDTBN.Properties.DisplayMember = "DTBN1";
            this.lupDTBN.Properties.NullText = "Tất cả";
            this.lupDTBN.Properties.PopupSizeable = false;
            this.lupDTBN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupDTBN.Properties.ValueMember = "IDDTBN";
            this.lupDTBN.Size = new System.Drawing.Size(299, 24);
            this.lupDTBN.TabIndex = 68;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(110, 41);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dtDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtDenNgay.Properties.EditFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dtDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtDenNgay.Properties.MaskSettings.Set("mask", "g");
            this.dtDenNgay.Size = new System.Drawing.Size(299, 24);
            this.dtDenNgay.TabIndex = 66;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.ImageOptions.Image")));
            this.btnHuy.Location = new System.Drawing.Point(317, 307);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(92, 30);
            this.btnHuy.TabIndex = 61;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // dateNgayTu
            // 
            this.dateNgayTu.EditValue = null;
            this.dateNgayTu.Location = new System.Drawing.Point(110, 9);
            this.dateNgayTu.Name = "dateNgayTu";
            this.dateNgayTu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateNgayTu.Properties.Appearance.Options.UseFont = true;
            this.dateNgayTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTu.Properties.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dateNgayTu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayTu.Properties.EditFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dateNgayTu.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateNgayTu.Properties.MaskSettings.Set("mask", "g");
            this.dateNgayTu.Size = new System.Drawing.Size(299, 24);
            this.dateNgayTu.TabIndex = 56;
            // 
            // lupCanBo
            // 
            this.lupCanBo.Location = new System.Drawing.Point(110, 105);
            this.lupCanBo.Name = "lupCanBo";
            this.lupCanBo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupCanBo.Properties.Appearance.Options.UseFont = true;
            this.lupCanBo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCanBo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", "Tên cán bộ", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", "MaCB", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lupCanBo.Properties.DisplayMember = "TenCB";
            this.lupCanBo.Properties.NullText = "";
            this.lupCanBo.Properties.PopupSizeable = false;
            this.lupCanBo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupCanBo.Properties.ValueMember = "MaCB";
            this.lupCanBo.Size = new System.Drawing.Size(299, 24);
            this.lupCanBo.TabIndex = 59;
            // 
            // Frm_NopTienVaoQuy_GLoc
            // 
            this.AcceptButton = this.btnInBC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 349);
            this.Controls.Add(this.radioGroupLoaiThu);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.rgChonMauBCct);
            this.Controls.Add(this.lupkhoaphong);
            this.Controls.Add(this.lupDTBN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DoiTuong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.dateNgayTu);
            this.Controls.Add(this.lupCanBo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NopTienVaoQuy_GLoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In báo cáo nộp tiền vào quỹ";
            this.Load += new System.EventHandler(this.Frm_NopTienVaoQuy_GLoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLoaiThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMauBCct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhoaphong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDTBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCanBo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DoiTuong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.DateEdit dateNgayTu;
        private DevExpress.XtraEditors.LookUpEdit lupCanBo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.LookUpEdit lupDTBN;
        private DevExpress.XtraEditors.LookUpEdit lupkhoaphong;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.RadioGroup rgChonMauBCct;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup radioGroupLoaiThu;
    }
}