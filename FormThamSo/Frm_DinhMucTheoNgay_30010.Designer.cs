namespace QLBV.FormThamSo
{
    partial class Frm_DinhMucTheoNgay_30010
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
            this.txtTungay = new System.Windows.Forms.DateTimePicker();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Location = new System.Drawing.Point(23, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // txtTungay
            // 
            this.txtTungay.CustomFormat = "dd/MM/yyyy";
            this.txtTungay.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTungay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtTungay.Location = new System.Drawing.Point(86, 22);
            this.txtTungay.Name = "txtTungay";
            this.txtTungay.Size = new System.Drawing.Size(200, 24);
            this.txtTungay.TabIndex = 1;
            this.txtTungay.Value = new System.DateTime(2019, 11, 1, 0, 0, 0, 0);
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.Location = new System.Drawing.Point(86, 65);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(200, 42);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Lấy báo cáo";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // Frm_DinhMucTheoNgay_30010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 119);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.txtTungay);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_DinhMucTheoNgay_30010";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo định mức theo ngày";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker txtTungay;
        private DevExpress.XtraEditors.SimpleButton btnReport;
    }
}