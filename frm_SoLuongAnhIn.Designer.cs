namespace QLBV
{
    partial class frm_SoLuongAnhIn
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
            this.cboSoLuong = new System.Windows.Forms.ComboBox();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(18, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(159, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Số lượng ảnh cần in";
            // 
            // cboSoLuong
            // 
            this.cboSoLuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoLuong.FormattingEnabled = true;
            this.cboSoLuong.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8"});
            this.cboSoLuong.Location = new System.Drawing.Point(183, 34);
            this.cboSoLuong.Name = "cboSoLuong";
            this.cboSoLuong.Size = new System.Drawing.Size(74, 21);
            this.cboSoLuong.TabIndex = 1;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Appearance.Options.UseFont = true;
            this.btnXacNhan.Location = new System.Drawing.Point(102, 69);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(75, 23);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // frm_SoLuongAnhIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 119);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.cboSoLuong);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SoLuongAnhIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_SoLuongAnhIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cboSoLuong;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
    }
}