namespace QLBV.FormThamSo
{
    partial class frm_BC_BNNTLinhThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_BNNTLinhThuoc));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radio1T = new System.Windows.Forms.RadioButton();
            this.radio2T = new System.Windows.Forms.RadioButton();
            this.dtpTungay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenngay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(69, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 25);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "Tạo báo &cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(209, 109);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(65, 25);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl1.Location = new System.Drawing.Point(35, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(269, 18);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Thống kê bệnh nhân ngoại trú lĩnh thuốc:";
            // 
            // radio1T
            // 
            this.radio1T.AutoSize = true;
            this.radio1T.Checked = true;
            this.radio1T.Location = new System.Drawing.Point(71, 45);
            this.radio1T.Name = "radio1T";
            this.radio1T.Size = new System.Drawing.Size(64, 17);
            this.radio1T.TabIndex = 12;
            this.radio1T.TabStop = true;
            this.radio1T.Text = "1 Tháng";
            this.radio1T.UseVisualStyleBackColor = true;
            // 
            // radio2T
            // 
            this.radio2T.AutoSize = true;
            this.radio2T.Location = new System.Drawing.Point(188, 45);
            this.radio2T.Name = "radio2T";
            this.radio2T.Size = new System.Drawing.Size(64, 17);
            this.radio2T.TabIndex = 13;
            this.radio2T.Text = "2 Tháng";
            this.radio2T.UseVisualStyleBackColor = true;
            // 
            // dtpTungay
            // 
            this.dtpTungay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTungay.Location = new System.Drawing.Point(68, 73);
            this.dtpTungay.Name = "dtpTungay";
            this.dtpTungay.Size = new System.Drawing.Size(100, 21);
            this.dtpTungay.TabIndex = 14;
            // 
            // dtpDenngay
            // 
            this.dtpDenngay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenngay.Location = new System.Drawing.Point(256, 73);
            this.dtpDenngay.Name = "dtpDenngay";
            this.dtpDenngay.Size = new System.Drawing.Size(100, 21);
            this.dtpDenngay.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Từ ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "đến ngày:";
            // 
            // frm_BC_BNNTLinhThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 153);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDenngay);
            this.Controls.Add(this.dtpTungay);
            this.Controls.Add(this.radio2T);
            this.Controls.Add(this.radio1T);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnThoat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_BNNTLinhThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO HOẠT ĐỘNG ĐIỀU TRỊ NỘI TRÚ";
            this.Load += new System.EventHandler(this.frm_BC_BNNTLinhThuoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.RadioButton radio1T;
        private System.Windows.Forms.RadioButton radio2T;
        private System.Windows.Forms.DateTimePicker dtpTungay;
        private System.Windows.Forms.DateTimePicker dtpDenngay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}