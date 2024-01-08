namespace QLBV.FormThamSo
{
    partial class frm_BCTonKhoThuocGNHTT_30002
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCTonKhoThuocGNHTT_30002));
            this.rd6Thang = new DevExpress.XtraEditors.RadioGroup();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.ckNhapTheoHD = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.rd6Thang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckNhapTheoHD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rd6Thang
            // 
            this.rd6Thang.Location = new System.Drawing.Point(12, 39);
            this.rd6Thang.Name = "rd6Thang";
            this.rd6Thang.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 Tháng đầu năm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 tháng cuối năm")});
            this.rd6Thang.Size = new System.Drawing.Size(280, 52);
            this.rd6Thang.TabIndex = 2;
            // 
            // cbNam
            // 
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(141, 12);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(151, 21);
            this.cbNam.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(218, 132);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 23);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(135, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(13, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 19);
            this.label1.TabIndex = 123;
            this.label1.Text = "Kho:";
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(79, 100);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Kho")});
            this.lupKho.Properties.NullText = "";
            this.lupKho.Size = new System.Drawing.Size(213, 24);
            this.lupKho.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 19);
            this.label3.TabIndex = 124;
            this.label3.Text = "Chọn năm báo cáo:";
            // 
            // ckNhapTheoHD
            // 
            this.ckNhapTheoHD.Location = new System.Drawing.Point(77, 165);
            this.ckNhapTheoHD.Name = "ckNhapTheoHD";
            this.ckNhapTheoHD.Properties.Caption = "Chỉ tính số lượng nhập theo hóa đơn";
            this.ckNhapTheoHD.Size = new System.Drawing.Size(213, 19);
            this.ckNhapTheoHD.TabIndex = 125;
            // 
            // frm_BCTonKhoThuocGNHTT_30002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 204);
            this.Controls.Add(this.ckNhapTheoHD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.rd6Thang);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Name = "frm_BCTonKhoThuocGNHTT_30002";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tồn kho thuốc GNHTT";
            this.Load += new System.EventHandler(this.frm_BCTonKhoThuocGNHTT_30002_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rd6Thang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckNhapTheoHD.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup rd6Thang;
        private System.Windows.Forms.ComboBox cbNam;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckEdit ckNhapTheoHD;
    }
}