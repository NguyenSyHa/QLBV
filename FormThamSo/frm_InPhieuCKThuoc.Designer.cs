namespace QLBV.FormThamSo
{
    partial class frm_InPhieuCKThuoc
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
            this.components = new System.ComponentModel.Container();
            this.rgChonMau = new DevExpress.XtraEditors.RadioGroup();
            this.btnInbc = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ckcVTYT = new DevExpress.XtraEditors.CheckEdit();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcVTYT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rgChonMau
            // 
            this.rgChonMau.Enabled = false;
            this.rgChonMau.Location = new System.Drawing.Point(84, 82);
            this.rgChonMau.Name = "rgChonMau";
            this.rgChonMau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgChonMau.Properties.Appearance.Options.UseFont = true;
            this.rgChonMau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thuốc và VTYT trên cùng một mẫu"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tách riêng thuốc và VTYT")});
            this.rgChonMau.Size = new System.Drawing.Size(252, 54);
            this.rgChonMau.TabIndex = 0;
            this.rgChonMau.SelectedIndexChanged += new System.EventHandler(this.rgChonMau_SelectedIndexChanged);
            // 
            // btnInbc
            // 
            this.btnInbc.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInbc.Appearance.Options.UseFont = true;
            this.btnInbc.Location = new System.Drawing.Point(84, 159);
            this.btnInbc.Name = "btnInbc";
            this.btnInbc.Size = new System.Drawing.Size(95, 30);
            this.btnInbc.TabIndex = 1;
            this.btnInbc.Text = "In Phiếu";
            this.btnInbc.Click += new System.EventHandler(this.btnInbc_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(239, 159);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(97, 30);
            this.btnhuy.TabIndex = 1;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(84, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Chọn mẫu in:";
            // 
            // ckcVTYT
            // 
            this.ckcVTYT.Location = new System.Drawing.Point(84, 21);
            this.ckcVTYT.Name = "ckcVTYT";
            this.ckcVTYT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcVTYT.Properties.Appearance.Options.UseFont = true;
            this.ckcVTYT.Properties.Caption = "Thống kê cả VTYT";
            this.ckcVTYT.Size = new System.Drawing.Size(229, 23);
            this.ckcVTYT.TabIndex = 3;
            this.ckcVTYT.CheckedChanged += new System.EventHandler(this.ckcVTYT_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_InPhieuCKThuoc
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 217);
            this.Controls.Add(this.ckcVTYT);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnInbc);
            this.Controls.Add(this.rgChonMau);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_InPhieuCKThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Phiếu Công Khai Thuốc";
            this.Load += new System.EventHandler(this.frm_InPhieuCKThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rgChonMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcVTYT.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup rgChonMau;
        private DevExpress.XtraEditors.SimpleButton btnInbc;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit ckcVTYT;
        private System.Windows.Forms.Timer timer1;
    }
}