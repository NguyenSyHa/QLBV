namespace QLBV.FormThamSo
{
    partial class Frm_BCSudungthuoctaikhoaDT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BCSudungthuoctaikhoaDT));
            this.label1 = new System.Windows.Forms.Label();
            this.radTimKiem = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupMaKP = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboDoiTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radNoiTru = new DevExpress.XtraEditors.RadioGroup();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.radTrongDM = new DevExpress.XtraEditors.RadioGroup();
            this.lupNhomDuoc = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDuoc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(10, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 70;
            this.label1.Text = "Nội|Ngoại trú:";
            // 
            // radTimKiem
            // 
            this.radTimKiem.EditValue = 0;
            this.radTimKiem.Location = new System.Drawing.Point(100, 44);
            this.radTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.radTimKiem.Name = "radTimKiem";
            this.radTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTimKiem.Properties.Appearance.Options.UseFont = true;
            this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tìm kiếm theo ngày ra viện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Tìm kiếm theo ngày thanh toán")});
            this.radTimKiem.Size = new System.Drawing.Size(307, 46);
            this.radTimKiem.TabIndex = 56;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(12, 216);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 17);
            this.labelControl3.TabIndex = 69;
            this.labelControl3.Text = "Khoa Phòng:";
            // 
            // lupMaKP
            // 
            this.lupMaKP.CausesValidation = false;
            this.lupMaKP.EditValue = 0;
            this.lupMaKP.Location = new System.Drawing.Point(100, 213);
            this.lupMaKP.Name = "lupMaKP";
            this.lupMaKP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaKP.Properties.Appearance.Options.UseFont = true;
            this.lupMaKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên KP")});
            this.lupMaKP.Properties.DisplayMember = "TenKP";
            this.lupMaKP.Properties.NullText = "";
            this.lupMaKP.Properties.ValueMember = "MaKP";
            this.lupMaKP.Size = new System.Drawing.Size(307, 24);
            this.lupMaKP.TabIndex = 61;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(13, 156);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 17);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Đối tượng:";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.EditValue = "BHYT";
            this.cboDoiTuong.Location = new System.Drawing.Point(100, 153);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDoiTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDoiTuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ"});
            this.cboDoiTuong.Properties.ReadOnly = true;
            this.cboDoiTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDoiTuong.Size = new System.Drawing.Size(118, 24);
            this.cboDoiTuong.TabIndex = 58;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(332, 317);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 60;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(227, 317);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(95, 23);
            this.btnInBC.TabIndex = 59;
            this.btnInBC.Text = "&In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(224, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 66;
            this.label3.Text = "đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 65;
            this.label2.Text = "Từ ngày:";
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(100, 98);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNoiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Điều trị ngoại trú")});
            this.radNoiTru.Size = new System.Drawing.Size(307, 46);
            this.radNoiTru.TabIndex = 57;
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(289, 12);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(118, 24);
            this.lupDenNgay.TabIndex = 55;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(100, 12);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(118, 24);
            this.lupTuNgay.TabIndex = 54;
            // 
            // radTrongDM
            // 
            this.radTrongDM.EditValue = ((short)(1));
            this.radTrongDM.Location = new System.Drawing.Point(100, 252);
            this.radTrongDM.Name = "radTrongDM";
            this.radTrongDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTrongDM.Properties.Appearance.Options.UseFont = true;
            this.radTrongDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Ngoài DM BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Trong DM BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Chi phí không phải TT")});
            this.radTrongDM.Size = new System.Drawing.Size(307, 48);
            this.radTrongDM.TabIndex = 71;
            // 
            // lupNhomDuoc
            // 
            this.lupNhomDuoc.CausesValidation = false;
            this.lupNhomDuoc.EditValue = "0";
            this.lupNhomDuoc.Location = new System.Drawing.Point(100, 183);
            this.lupNhomDuoc.Name = "lupNhomDuoc";
            this.lupNhomDuoc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNhomDuoc.Properties.Appearance.Options.UseFont = true;
            this.lupNhomDuoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhomDuoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", "Nhóm dược")});
            this.lupNhomDuoc.Properties.DisplayMember = "TenNhom";
            this.lupNhomDuoc.Properties.NullText = "";
            this.lupNhomDuoc.Properties.ValueMember = "IDNhom";
            this.lupNhomDuoc.Size = new System.Drawing.Size(307, 24);
            this.lupNhomDuoc.TabIndex = 72;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(13, 186);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 17);
            this.labelControl2.TabIndex = 73;
            this.labelControl2.Text = "Nhóm dịch vụ:";
            // 
            // Frm_BCSudungthuoctaikhoaDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 358);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lupNhomDuoc);
            this.Controls.Add(this.radTrongDM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radTimKiem);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupMaKP);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboDoiTuong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radNoiTru);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BCSudungthuoctaikhoaDT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê thuốc sử dụng tại khoa";
            this.Load += new System.EventHandler(this.Frm_BCSudungthuoctaikhoaDT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDuoc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup radTimKiem;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupMaKP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboDoiTuong;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.RadioGroup radNoiTru;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.RadioGroup radTrongDM;
        private DevExpress.XtraEditors.LookUpEdit lupNhomDuoc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}