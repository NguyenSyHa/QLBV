namespace QLBV.FormThamSo
{
    partial class frm_Yenson_tonghop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Yenson_tonghop));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.sbtTao = new DevExpress.XtraEditors.SimpleButton();
            this.sbtHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Ngày từ:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Ngày đến:";
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(65, 46);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgayden.Properties.Appearance.Options.UseFont = true;
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Size = new System.Drawing.Size(149, 22);
            this.lupNgayden.TabIndex = 2;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(65, 20);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(149, 22);
            this.lupNgaytu.TabIndex = 3;
            // 
            // sbtTao
            // 
            this.sbtTao.Image = ((System.Drawing.Image)(resources.GetObject("sbtTao.Image")));
            this.sbtTao.Location = new System.Drawing.Point(65, 98);
            this.sbtTao.Name = "sbtTao";
            this.sbtTao.Size = new System.Drawing.Size(75, 28);
            this.sbtTao.TabIndex = 4;
            this.sbtTao.Text = "Tạo BC";
            this.sbtTao.Click += new System.EventHandler(this.sbtTao_Click);
            // 
            // sbtHuy
            // 
            this.sbtHuy.Image = ((System.Drawing.Image)(resources.GetObject("sbtHuy.Image")));
            this.sbtHuy.Location = new System.Drawing.Point(153, 98);
            this.sbtHuy.Name = "sbtHuy";
            this.sbtHuy.Size = new System.Drawing.Size(61, 28);
            this.sbtHuy.TabIndex = 5;
            this.sbtHuy.Text = "Hủy";
            this.sbtHuy.Click += new System.EventHandler(this.sbtHuy_Click);
            // 
            // frm_Yenson_tonghop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 137);
            this.Controls.Add(this.sbtHuy);
            this.Controls.Add(this.sbtTao);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.lupNgayden);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Yenson_tonghop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp khám chữa bệnh";
            this.Load += new System.EventHandler(this.frm_Yenson_tonghop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit lupNgayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.SimpleButton sbtTao;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
    }
}