namespace QLBV.ChucNang
{
    partial class frm_MoMayAnh
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnChupAnh = new DevExpress.XtraEditors.SimpleButton();
            this.btnTatMayAnh = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoCam = new DevExpress.XtraEditors.SimpleButton();
            this.CboChonCam = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChupAnh
            // 
            this.btnChupAnh.Location = new System.Drawing.Point(89, 250);
            this.btnChupAnh.Name = "btnChupAnh";
            this.btnChupAnh.Size = new System.Drawing.Size(75, 23);
            this.btnChupAnh.TabIndex = 3;
            this.btnChupAnh.Text = "Chụp ảnh";
            this.btnChupAnh.Click += new System.EventHandler(this.btnChupAnh_Click);
            // 
            // btnTatMayAnh
            // 
            this.btnTatMayAnh.Location = new System.Drawing.Point(170, 250);
            this.btnTatMayAnh.Name = "btnTatMayAnh";
            this.btnTatMayAnh.Size = new System.Drawing.Size(75, 23);
            this.btnTatMayAnh.TabIndex = 4;
            this.btnTatMayAnh.Text = "Tắt máy ảnh";
            this.btnTatMayAnh.Click += new System.EventHandler(this.btnTatMayAnh_Click);
            // 
            // btnMoCam
            // 
            this.btnMoCam.Location = new System.Drawing.Point(8, 250);
            this.btnMoCam.Name = "btnMoCam";
            this.btnMoCam.Size = new System.Drawing.Size(75, 23);
            this.btnMoCam.TabIndex = 6;
            this.btnMoCam.Text = "Mở camera";
            this.btnMoCam.Click += new System.EventHandler(this.btnMoCam_Click);
            // 
            // CboChonCam
            // 
            this.CboChonCam.FormattingEnabled = true;
            this.CboChonCam.Location = new System.Drawing.Point(8, 2);
            this.CboChonCam.Name = "CboChonCam";
            this.CboChonCam.Size = new System.Drawing.Size(237, 21);
            this.CboChonCam.TabIndex = 7;
            this.CboChonCam.Text = "Chọn máy ảnh";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 213);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 289);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Lưu ý:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(46, 289);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(172, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Hướng dẫn sử dụng chức máy ảnh";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frm_MoMayAnh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 315);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CboChonCam);
            this.Controls.Add(this.btnMoCam);
            this.Controls.Add(this.btnTatMayAnh);
            this.Controls.Add(this.btnChupAnh);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_MoMayAnh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Máy ảnh";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_MoMayAnh_FormClosed);
            this.Load += new System.EventHandler(this.frm_MoMayAnh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnChupAnh;
        private DevExpress.XtraEditors.SimpleButton btnTatMayAnh;
        private DevExpress.XtraEditors.SimpleButton btnMoCam;
        private System.Windows.Forms.ComboBox CboChonCam;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
