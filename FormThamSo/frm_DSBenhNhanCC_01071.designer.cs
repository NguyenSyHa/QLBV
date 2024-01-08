namespace QLBV.FormThamSo
{
    partial class frm_DSBenhNhanCC_01071
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupKPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.luptungay = new DevExpress.XtraEditors.DateEdit();
            this.lupdenngay = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.cboDtuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.rdhtthanhtoan = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDtuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdhtthanhtoan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 19);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "KPhòng:";
            // 
            // lupKPhong
            // 
            this.lupKPhong.Location = new System.Drawing.Point(95, 89);
            this.lupKPhong.Name = "lupKPhong";
            this.lupKPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKPhong.Properties.Appearance.Options.UseFont = true;
            this.lupKPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", 5, "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 30, "Tên KP")});
            this.lupKPhong.Properties.DisplayMember = "TenKP";
            this.lupKPhong.Properties.NullText = "Chọn Khoa Phòng";
            this.lupKPhong.Properties.ValueMember = "MaKP";
            this.lupKPhong.Size = new System.Drawing.Size(220, 26);
            this.lupKPhong.TabIndex = 1;
            // 
            // luptungay
            // 
            this.luptungay.EditValue = null;
            this.luptungay.Location = new System.Drawing.Point(95, 21);
            this.luptungay.Name = "luptungay";
            this.luptungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luptungay.Properties.Appearance.Options.UseFont = true;
            this.luptungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Properties.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.luptungay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.luptungay.Properties.EditFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.luptungay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.luptungay.Properties.Mask.EditMask = "g";
            this.luptungay.Size = new System.Drawing.Size(220, 26);
            this.luptungay.TabIndex = 2;
            // 
            // lupdenngay
            // 
            this.lupdenngay.EditValue = null;
            this.lupdenngay.Location = new System.Drawing.Point(95, 55);
            this.lupdenngay.Name = "lupdenngay";
            this.lupdenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupdenngay.Properties.Appearance.Options.UseFont = true;
            this.lupdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Properties.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.lupdenngay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.lupdenngay.Properties.EditFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.lupdenngay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.lupdenngay.Properties.Mask.EditMask = "g";
            this.lupdenngay.Size = new System.Drawing.Size(220, 26);
            this.lupdenngay.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = global::QLBV.Properties.Resources.next_32x32;
            this.simpleButton1.Location = new System.Drawing.Point(95, 242);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(106, 36);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Tạo BC";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = global::QLBV.Properties.Resources.cancel_32x32;
            this.simpleButton2.Location = new System.Drawing.Point(207, 242);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(108, 36);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "Hủy";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // cboDtuong
            // 
            this.cboDtuong.Location = new System.Drawing.Point(95, 123);
            this.cboDtuong.Name = "cboDtuong";
            this.cboDtuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDtuong.Properties.Appearance.Options.UseFont = true;
            this.cboDtuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDtuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ",
            "Cả hai"});
            this.cboDtuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDtuong.Size = new System.Drawing.Size(220, 26);
            this.cboDtuong.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(12, 126);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 19);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "ĐTượng:";
            // 
            // rdhtthanhtoan
            // 
            this.rdhtthanhtoan.Location = new System.Drawing.Point(95, 156);
            this.rdhtthanhtoan.Name = "rdhtthanhtoan";
            this.rdhtthanhtoan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdhtthanhtoan.Properties.Appearance.Options.UseFont = true;
            this.rdhtthanhtoan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "TT Trực tiếp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "TT Chuyển khoản"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.rdhtthanhtoan.Size = new System.Drawing.Size(220, 76);
            this.rdhtthanhtoan.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(12, 163);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(79, 19);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "HT T.Toán:";
            // 
            // frm_DSBenhNhanCC_01071
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 281);
            this.Controls.Add(this.rdhtthanhtoan);
            this.Controls.Add(this.cboDtuong);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.lupdenngay);
            this.Controls.Add(this.luptungay);
            this.Controls.Add(this.lupKPhong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_DSBenhNhanCC_01071";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh nhân cấp cứu";
            this.Load += new System.EventHandler(this.frm_DSBenhNhanCC_01071_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDtuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdhtthanhtoan.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupKPhong;
        private DevExpress.XtraEditors.DateEdit luptungay;
        private DevExpress.XtraEditors.DateEdit lupdenngay;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.ComboBoxEdit cboDtuong;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup rdhtthanhtoan;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}