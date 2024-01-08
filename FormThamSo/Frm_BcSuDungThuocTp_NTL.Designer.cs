namespace QLBV.FormThamSo
{
    partial class Frm_BcSuDungThuocTp_NTL
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
            this.txtTungay = new System.Windows.Forms.DateTimePicker();
            this.txtDenngay = new System.Windows.Forms.DateTimePicker();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupthuoc = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.lupKP = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupthuoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTungay
            // 
            this.txtTungay.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTungay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtTungay.Location = new System.Drawing.Point(119, 29);
            this.txtTungay.Name = "txtTungay";
            this.txtTungay.Size = new System.Drawing.Size(200, 25);
            this.txtTungay.TabIndex = 0;
            // 
            // txtDenngay
            // 
            this.txtDenngay.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDenngay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDenngay.Location = new System.Drawing.Point(119, 56);
            this.txtDenngay.Name = "txtDenngay";
            this.txtDenngay.Size = new System.Drawing.Size(200, 25);
            this.txtDenngay.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(22, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(22, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // lupthuoc
            // 
            this.lupthuoc.EditValue = "";
            this.lupthuoc.Location = new System.Drawing.Point(119, 110);
            this.lupthuoc.Name = "lupthuoc";
            this.lupthuoc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupthuoc.Properties.Appearance.Options.UseFont = true;
            this.lupthuoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupthuoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", "Tên thuốc")});
            this.lupthuoc.Properties.DisplayMember = "TenDV";
            this.lupthuoc.Properties.DropDownRows = 6;
            this.lupthuoc.Properties.ValueMember = "TenDV";
            this.lupthuoc.Size = new System.Drawing.Size(200, 24);
            this.lupthuoc.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(22, 114);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 17);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Chọn thuốc:";
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReport.Location = new System.Drawing.Point(350, 0);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(89, 151);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Lấy báo cáo";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lupKP
            // 
            this.lupKP.EditValue = "";
            this.lupKP.Location = new System.Drawing.Point(119, 85);
            this.lupKP.Name = "lupKP";
            this.lupKP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKP.Properties.Appearance.Options.UseFont = true;
            this.lupKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên khoa")});
            this.lupKP.Properties.DisplayMember = "TenKP";
            this.lupKP.Properties.DropDownRows = 6;
            this.lupKP.Properties.ValueMember = "TenKP";
            this.lupKP.Size = new System.Drawing.Size(200, 24);
            this.lupKP.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(22, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 17);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Khoa phòng:";
            // 
            // Frm_BcSuDungThuocTp_NTL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 151);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.lupKP);
            this.Controls.Add(this.lupthuoc);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDenngay);
            this.Controls.Add(this.txtTungay);
            this.Name = "Frm_BcSuDungThuocTp_NTL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo sử dụng thuốc thành phẩm";
            this.Load += new System.EventHandler(this.Frm_BcSuDungThuocTp_NTL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupthuoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtTungay;
        private System.Windows.Forms.DateTimePicker txtDenngay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lupthuoc;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.LookUpEdit lupKP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}