namespace QLBV.FormThamSo
{
    partial class frm_BCSL_BenhNhantheo_BS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCSL_BenhNhantheo_BS));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnTaoBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(17, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 20);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(17, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 20);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // btnTaoBaoCao
            // 
            this.btnTaoBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoBaoCao.Image")));
            this.btnTaoBaoCao.Location = new System.Drawing.Point(155, 115);
            this.btnTaoBaoCao.Name = "btnTaoBaoCao";
            this.btnTaoBaoCao.Size = new System.Drawing.Size(92, 30);
            this.btnTaoBaoCao.TabIndex = 4;
            this.btnTaoBaoCao.Text = "Tạo báo cáo";
            this.btnTaoBaoCao.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = null;
            this.dtpTuNgay.Location = new System.Drawing.Point(107, 39);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Size = new System.Drawing.Size(239, 26);
            this.dtpTuNgay.TabIndex = 7;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = null;
            this.dtpDenNgay.Location = new System.Drawing.Point(107, 70);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Size = new System.Drawing.Size(239, 26);
            this.dtpDenNgay.TabIndex = 8;
            // 
            // frm_BCSL_BenhNhantheo_BS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 165);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.btnTaoBaoCao);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCSL_BenhNhantheo_BS";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo số lượng bệnh nhân theo bác sĩ";
            this.Load += new System.EventHandler(this.frm_BCSL_BenhNhantheo_BS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnTaoBaoCao;
        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
    }
}