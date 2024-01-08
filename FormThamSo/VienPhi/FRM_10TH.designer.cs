namespace QLBV.FormThamSo
{
    partial class FRM_10TH
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
            this.OK = new DevExpress.XtraEditors.SimpleButton();
            this.Huy = new DevExpress.XtraEditors.SimpleButton();
            this.lupngaytu = new DevExpress.XtraEditors.DateEdit();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày: ";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(12, 113);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 4;
            this.OK.Text = "Tạo báo cáo";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Huy
            // 
            this.Huy.Location = new System.Drawing.Point(112, 113);
            this.Huy.Name = "Huy";
            this.Huy.Size = new System.Drawing.Size(75, 23);
            this.Huy.TabIndex = 5;
            this.Huy.Text = "Huỷ";
            this.Huy.Click += new System.EventHandler(this.Huy_Click);
            // 
            // lupngaytu
            // 
            this.lupngaytu.EditValue = null;
            this.lupngaytu.Location = new System.Drawing.Point(69, 41);
            this.lupngaytu.Name = "lupngaytu";
            this.lupngaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupngaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupngaytu.Properties.Mask.EditMask = "";
            this.lupngaytu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.lupngaytu.Size = new System.Drawing.Size(100, 20);
            this.lupngaytu.TabIndex = 0;
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(69, 67);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgayden.Properties.Mask.EditMask = "";
            this.lupNgayden.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.lupNgayden.Size = new System.Drawing.Size(100, 20);
            this.lupNgayden.TabIndex = 1;
            // 
            // FRM_10TH
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 160);
            this.Controls.Add(this.Huy);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupngaytu);
            this.Controls.Add(this.lupNgayden);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_10TH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biểu 10 TH";
            this.Load += new System.EventHandler(this.FRM_10TH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton OK;
        private DevExpress.XtraEditors.SimpleButton Huy;
        private DevExpress.XtraEditors.DateEdit lupngaytu;
        private DevExpress.XtraEditors.DateEdit lupNgayden;

    }
}