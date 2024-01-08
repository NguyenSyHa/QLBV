namespace QLBV.FormNhap
{
    partial class frmNhapNgaySaoDienBien
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
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtNgaySao = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgaySao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgaySao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDongY
            // 
            this.btnDongY.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDongY.Appearance.Options.UseFont = true;
            this.btnDongY.Location = new System.Drawing.Point(247, 43);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(98, 30);
            this.btnDongY.TabIndex = 0;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ngày sao diễn biến:";
            // 
            // dtNgaySao
            // 
            this.dtNgaySao.EditValue = null;
            this.dtNgaySao.Location = new System.Drawing.Point(146, 11);
            this.dtNgaySao.Name = "dtNgaySao";
            this.dtNgaySao.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgaySao.Properties.Appearance.Options.UseFont = true;
            this.dtNgaySao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgaySao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgaySao.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dtNgaySao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtNgaySao.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dtNgaySao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtNgaySao.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm";
            this.dtNgaySao.Size = new System.Drawing.Size(199, 26);
            this.dtNgaySao.TabIndex = 2;
            // 
            // frmNhapNgaySaoDienBien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 78);
            this.Controls.Add(this.dtNgaySao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDongY);
            this.Name = "frmNhapNgaySaoDienBien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sao diễn biến";
            this.Load += new System.EventHandler(this.frmNhapNgaySaoDienBien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtNgaySao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgaySao.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dtNgaySao;
    }
}