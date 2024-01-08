namespace QLBV.FormThamSo
{
    partial class frm_BaoCaoNBChuyendi
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.sbtTao = new DevExpress.XtraEditors.SimpleButton();
            this.sbtHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(27, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(27, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(109, 23);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Size = new System.Drawing.Size(167, 26);
            this.LupNgaytu.TabIndex = 2;
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(109, 55);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LupNgayden.Properties.Appearance.Options.UseFont = true;
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Size = new System.Drawing.Size(167, 26);
            this.LupNgayden.TabIndex = 3;
            // 
            // sbtTao
            // 
            this.sbtTao.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtTao.Appearance.Options.UseFont = true;
            this.sbtTao.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.sbtTao.Location = new System.Drawing.Point(109, 98);
            this.sbtTao.Name = "sbtTao";
            this.sbtTao.Size = new System.Drawing.Size(86, 32);
            this.sbtTao.TabIndex = 4;
            this.sbtTao.Text = "In BC";
            this.sbtTao.Click += new System.EventHandler(this.sbtTao_Click);
            // 
            // sbtHuy
            // 
            this.sbtHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbtHuy.Appearance.Options.UseFont = true;
            this.sbtHuy.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.sbtHuy.Location = new System.Drawing.Point(201, 98);
            this.sbtHuy.Name = "sbtHuy";
            this.sbtHuy.Size = new System.Drawing.Size(75, 32);
            this.sbtHuy.TabIndex = 5;
            this.sbtHuy.Text = "Huỷ";
            this.sbtHuy.Click += new System.EventHandler(this.sbtHuy_Click);
            // 
            // frm_BaoCaoNBChuyendi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 147);
            this.Controls.Add(this.sbtHuy);
            this.Controls.Add(this.sbtTao);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BaoCaoNBChuyendi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo NB chuyển đi";
            this.Load += new System.EventHandler(this.frm_BaoCaoNBChuyendi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.SimpleButton sbtTao;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
    }
}