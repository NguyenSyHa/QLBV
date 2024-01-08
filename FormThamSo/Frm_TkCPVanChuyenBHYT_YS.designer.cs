namespace QLBV.FormThamSo
{
    partial class Frm_TkCPVanChuyenBHYT_YS
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
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.btnIn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radNoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.radioDTuong = new DevExpress.XtraEditors.RadioGroup();
            this.radTimKiem = new DevExpress.XtraEditors.RadioGroup();
            this.lupKP = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(106, 86);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(151, 22);
            this.lupDenNgay.TabIndex = 53;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(106, 58);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(151, 22);
            this.lupTuNgay.TabIndex = 52;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(27, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 15);
            this.labelControl2.TabIndex = 57;
            this.labelControl2.Text = "Đến ngày:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(27, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 15);
            this.labelControl1.TabIndex = 56;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // memoEdit1
            // 
            this.memoEdit1.EditValue = "Để lên được báo cáo chi phí vận chuyển này tên nhóm CT phải là \"Chi phí vận chuyể" +
    "n\"";
            this.memoEdit1.Location = new System.Drawing.Point(27, 12);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.AllowFocused = false;
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoEdit1.Size = new System.Drawing.Size(231, 40);
            this.memoEdit1.TabIndex = 58;
            // 
            // btnIn
            // 
            this.btnIn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnIn.FlatAppearance.BorderSize = 0;
            this.btnIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(27, 263);
            this.btnIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(66, 27);
            this.btnIn.TabIndex = 59;
            this.btnIn.Text = "&In báo cáo";
            this.btnIn.UseVisualStyleBackColor = false;
            this.btnIn.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(191, 263);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 27);
            this.button1.TabIndex = 60;
            this.button1.Text = "&Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // radNoiNgoaiTru
            // 
            this.radNoiNgoaiTru.EditValue = ((short)(2));
            this.radNoiNgoaiTru.Location = new System.Drawing.Point(26, 164);
            this.radNoiNgoaiTru.Name = "radNoiNgoaiTru";
            this.radNoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Cả hai")});
            this.radNoiNgoaiTru.Size = new System.Drawing.Size(231, 26);
            this.radNoiNgoaiTru.TabIndex = 62;
            // 
            // radioDTuong
            // 
            this.radioDTuong.EditValue = ((short)(2));
            this.radioDTuong.Location = new System.Drawing.Point(27, 196);
            this.radioDTuong.Name = "radioDTuong";
            this.radioDTuong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radioDTuong.Properties.Appearance.Options.UseFont = true;
            this.radioDTuong.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Cả hai")});
            this.radioDTuong.Size = new System.Drawing.Size(231, 26);
            this.radioDTuong.TabIndex = 63;
            // 
            // radTimKiem
            // 
            this.radTimKiem.EditValue = ((short)(0));
            this.radTimKiem.Location = new System.Drawing.Point(26, 114);
            this.radTimKiem.Name = "radTimKiem";
            this.radTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTimKiem.Properties.Appearance.Options.UseFont = true;
            this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Tìm theo ngày thanh toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Tìm theo ngày ra viện")});
            this.radTimKiem.Size = new System.Drawing.Size(231, 44);
            this.radTimKiem.TabIndex = 65;
            // 
            // lupKP
            // 
            this.lupKP.Location = new System.Drawing.Point(106, 228);
            this.lupKP.Name = "lupKP";
            this.lupKP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKP.Properties.Appearance.Options.UseFont = true;
            this.lupKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 100, "Tên KP")});
            this.lupKP.Properties.DisplayMember = "TenKP";
            this.lupKP.Properties.NullText = "";
            this.lupKP.Properties.ValueMember = "MaKP";
            this.lupKP.Size = new System.Drawing.Size(151, 22);
            this.lupKP.TabIndex = 67;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(27, 231);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 15);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "Khoa|Phòng:";
            this.labelControl3.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // Frm_TkCPVanChuyenBHYT_YS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 316);
            this.Controls.Add(this.lupKP);
            this.Controls.Add(this.radTimKiem);
            this.Controls.Add(this.radioDTuong);
            this.Controls.Add(this.radNoiNgoaiTru);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TkCPVanChuyenBHYT_YS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Bảng thống kê chi phí KCB BHYT";
            this.Load += new System.EventHandler(this.Frm_TkCPVanChuyenBHYT_YS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.RadioGroup radNoiNgoaiTru;
        private DevExpress.XtraEditors.RadioGroup radioDTuong;
        private DevExpress.XtraEditors.RadioGroup radTimKiem;
        private DevExpress.XtraEditors.LookUpEdit lupKP;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}