namespace QLBV.FormThamSo
{
    partial class frm_THHoatDongChuyenMon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_THHoatDongChuyenMon));
            this.label2 = new System.Windows.Forms.Label();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.rd6Thang = new DevExpress.XtraEditors.RadioGroup();
            this.ckIn6thang = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.rd6Thang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIn6thang.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(24, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 115;
            this.label2.Text = "Chọn năm báo cáo";
            // 
            // cbNam
            // 
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(158, 18);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(94, 21);
            this.cbNam.TabIndex = 1;
            // 
            // rd6Thang
            // 
            this.rd6Thang.Enabled = false;
            this.rd6Thang.Location = new System.Drawing.Point(27, 91);
            this.rd6Thang.Name = "rd6Thang";
            this.rd6Thang.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 Tháng đầu năm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 tháng cuối năm")});
            this.rd6Thang.Size = new System.Drawing.Size(280, 52);
            this.rd6Thang.TabIndex = 3;
            // 
            // ckIn6thang
            // 
            this.ckIn6thang.Location = new System.Drawing.Point(27, 64);
            this.ckIn6thang.Name = "ckIn6thang";
            this.ckIn6thang.Properties.Caption = "BC kết quả 6 tháng";
            this.ckIn6thang.Size = new System.Drawing.Size(124, 19);
            this.ckIn6thang.TabIndex = 2;
            this.ckIn6thang.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.ckIn6thang_EditValueChanging);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(150, 161);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(233, 161);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 23);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frm_THHoatDongChuyenMon
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 196);
            this.Controls.Add(this.ckIn6thang);
            this.Controls.Add(this.rd6Thang);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Name = "frm_THHoatDongChuyenMon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoạt dộng chuyên môn của bệnh viện";
            this.Load += new System.EventHandler(this.frm_THHoatDongChuyenMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rd6Thang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIn6thang.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private System.Windows.Forms.ComboBox cbNam;
        private DevExpress.XtraEditors.RadioGroup rd6Thang;
        private DevExpress.XtraEditors.CheckEdit ckIn6thang;

    }
}