namespace QLBV.FormThamSo
{
    partial class frm_BaocaoTT37_Bieu9
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
            this.btnBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.txtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.radNgayKham = new System.Windows.Forms.RadioButton();
            this.radNgayRaVien = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBaoCao.Appearance.Options.UseFont = true;
            this.btnBaoCao.Location = new System.Drawing.Point(236, 93);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(100, 31);
            this.btnBaoCao.TabIndex = 2;
            this.btnBaoCao.Text = "In Báo cáo";
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 72);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtTuNgay.Location = new System.Drawing.Point(120, 40);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(216, 21);
            this.txtTuNgay.TabIndex = 4;
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDenNgay.Location = new System.Drawing.Point(120, 66);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(216, 21);
            this.txtDenNgay.TabIndex = 4;
            // 
            // radNgayKham
            // 
            this.radNgayKham.AutoSize = true;
            this.radNgayKham.Location = new System.Drawing.Point(120, 12);
            this.radNgayKham.Name = "radNgayKham";
            this.radNgayKham.Size = new System.Drawing.Size(78, 17);
            this.radNgayKham.TabIndex = 5;
            this.radNgayKham.TabStop = true;
            this.radNgayKham.Text = "Ngày khám";
            this.radNgayKham.UseVisualStyleBackColor = true;
            // 
            // radNgayRaVien
            // 
            this.radNgayRaVien.AutoSize = true;
            this.radNgayRaVien.Location = new System.Drawing.Point(249, 12);
            this.radNgayRaVien.Name = "radNgayRaVien";
            this.radNgayRaVien.Size = new System.Drawing.Size(86, 17);
            this.radNgayRaVien.TabIndex = 5;
            this.radNgayRaVien.TabStop = true;
            this.radNgayRaVien.Text = "Ngày ra viện";
            this.radNgayRaVien.UseVisualStyleBackColor = true;
            // 
            // frm_BaocaoTT37_Bieu9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 139);
            this.Controls.Add(this.radNgayRaVien);
            this.Controls.Add(this.radNgayKham);
            this.Controls.Add(this.txtDenNgay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.btnBaoCao);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BaocaoTT37_Bieu9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO CƠ SỞ, GIƯỜNG BỆNH VÀ HOẠT ĐỘNG KHÁM CHỮA BỆNH THEO THÔNG TƯ 37 BIỂU 9";
            this.Load += new System.EventHandler(this.frm_BaocaoTT37_Bieu9_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBaoCao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.DateTimePicker txtTuNgay;
        private System.Windows.Forms.DateTimePicker txtDenNgay;
        private System.Windows.Forms.RadioButton radNgayKham;
        private System.Windows.Forms.RadioButton radNgayRaVien;
    }
}