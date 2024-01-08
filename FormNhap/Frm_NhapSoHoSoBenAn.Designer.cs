namespace QLBV.FormNhap
{
    partial class Frm_NhapSoHoSoBenAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NhapSoHoSoBenAn));
            this.txtSoHSBA = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblThongTinBenhNhan = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHSBA.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSoHSBA
            // 
            this.txtSoHSBA.Location = new System.Drawing.Point(134, 46);
            this.txtSoHSBA.Name = "txtSoHSBA";
            this.txtSoHSBA.Size = new System.Drawing.Size(349, 20);
            this.txtSoHSBA.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(104, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Số hồ sơ bệnh án:";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Image = ((System.Drawing.Image)(resources.GetObject("btnXacNhan.Image")));
            this.btnXacNhan.Location = new System.Drawing.Point(378, 72);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(105, 23);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(9, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(122, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Thông tin bệnh nhân:";
            // 
            // lblThongTinBenhNhan
            // 
            this.lblThongTinBenhNhan.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinBenhNhan.Location = new System.Drawing.Point(134, 12);
            this.lblThongTinBenhNhan.Name = "lblThongTinBenhNhan";
            this.lblThongTinBenhNhan.Size = new System.Drawing.Size(0, 17);
            this.lblThongTinBenhNhan.TabIndex = 5;
            // 
            // Frm_NhapSoHoSoBenAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 126);
            this.Controls.Add(this.lblThongTinBenhNhan);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSoHSBA);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NhapSoHoSoBenAn";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hồ sơ bệnh án";
            this.Load += new System.EventHandler(this.Frm_NhapSoHoSoBenAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHSBA.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtSoHSBA;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblThongTinBenhNhan;
    }
}