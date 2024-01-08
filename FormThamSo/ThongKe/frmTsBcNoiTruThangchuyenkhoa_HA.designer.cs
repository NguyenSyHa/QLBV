namespace QLBV.FormThamSo
{
    partial class frmTsBcNoiTruThangchuyenkhoa_HA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTsBcNoiTruThangchuyenkhoa_HA));
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.radNoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.lupKhoa = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.lupCK = new DevExpress.XtraEditors.LookUpEdit();
            this.radNgay = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNgay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(337, 282);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(87, 30);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(233, 282);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(98, 30);
            this.btnInBC.TabIndex = 7;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(103, 210);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Không thống kê những BN chuyển phòng khám"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thống kê cả những BN chuyển phòng khám")});
            this.radioGroup1.Size = new System.Drawing.Size(321, 54);
            this.radioGroup1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(251, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Từ ngày:";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(291, 23);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Size = new System.Drawing.Size(133, 26);
            this.dateDenNgay.TabIndex = 2;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(103, 23);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(133, 26);
            this.dateTuNgay.TabIndex = 1;
            // 
            // radNoiNgoaiTru
            // 
            this.radNoiNgoaiTru.EditValue = ((short)(2));
            this.radNoiNgoaiTru.Location = new System.Drawing.Point(103, 174);
            this.radNoiNgoaiTru.Name = "radNoiNgoaiTru";
            this.radNoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Cả hai")});
            this.radNoiNgoaiTru.Size = new System.Drawing.Size(321, 30);
            this.radNoiNgoaiTru.TabIndex = 5;
            this.radNoiNgoaiTru.SelectedIndexChanged += new System.EventHandler(this.radNoiNgoaiTru_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(13, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Khoa/Phòng:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lupKhoa
            // 
            this.lupKhoa.Location = new System.Drawing.Point(103, 115);
            this.lupKhoa.Name = "lupKhoa";
            this.lupKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoa.Properties.Appearance.Options.UseFont = true;
            this.lupKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoa.Properties.DisplayMember = "TenKP";
            this.lupKhoa.Properties.NullText = "Tất cả";
            this.lupKhoa.Properties.PopupSizeable = false;
            this.lupKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoa.Properties.ValueMember = "MaKP";
            this.lupKhoa.Size = new System.Drawing.Size(321, 24);
            this.lupKhoa.TabIndex = 3;
            this.lupKhoa.EditValueChanged += new System.EventHandler(this.lupKhoa_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(13, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Chuyên khoa:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lupCK
            // 
            this.lupCK.Location = new System.Drawing.Point(103, 144);
            this.lupCK.Name = "lupCK";
            this.lupCK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupCK.Properties.Appearance.Options.UseFont = true;
            this.lupCK.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCK.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("chuyenkhoa", 200, "Tên chuyên khoa")});
            this.lupCK.Properties.DisplayMember = "chuyenkhoa";
            this.lupCK.Properties.NullText = "Tất cả";
            this.lupCK.Properties.PopupSizeable = false;
            this.lupCK.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupCK.Properties.ValueMember = "mack";
            this.lupCK.Size = new System.Drawing.Size(321, 24);
            this.lupCK.TabIndex = 4;
            this.lupCK.EditValueChanged += new System.EventHandler(this.lupCK_EditValueChanged);
            // 
            // radNgay
            // 
            this.radNgay.Location = new System.Drawing.Point(103, 55);
            this.radNgay.Name = "radNgay";
            this.radNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNgay.Properties.Appearance.Options.UseFont = true;
            this.radNgay.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tìm theo ngày khám bệnh"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tìm theo ngày bệnh nhân đến BV")});
            this.radNgay.Size = new System.Drawing.Size(321, 54);
            this.radNgay.TabIndex = 6;
            this.radNgay.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // frmTsBcNoiTruThangchuyenkhoa_HA
            // 
            this.AcceptButton = this.btnInBC;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(472, 324);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lupCK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lupKhoa);
            this.Controls.Add(this.radNgay);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.dateTuNgay);
            this.Controls.Add(this.radNoiNgoaiTru);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTsBcNoiTruThangchuyenkhoa_HA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In BC bệnh nhân khám bệnh theo chuyên khoa";
            this.Load += new System.EventHandler(this.frmTsBcNoiTruThangchuyenkhoa_HA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNgay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.RadioGroup radNoiNgoaiTru;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupKhoa;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.LookUpEdit lupCK;
        private DevExpress.XtraEditors.RadioGroup radNgay;
    }
}