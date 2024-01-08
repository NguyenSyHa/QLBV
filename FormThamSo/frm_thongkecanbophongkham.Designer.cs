namespace QLBV.FormThamSo
{
    partial class frm_thongkecanbophongkham
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(32, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(207, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "THỐNG KÊ LƯỢT KCB";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(32, 143);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(84, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "TẠO BÁO CÁO";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(172, 143);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(84, 23);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "THOÁT";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(131, 64);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgaytu.Properties.Mask.EditMask = "";
            this.lupNgaytu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.lupNgaytu.Properties.NullText = "[EditValue is null]";
            this.lupNgaytu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lupNgaytu.Size = new System.Drawing.Size(118, 20);
            this.lupNgaytu.TabIndex = 1;
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(131, 90);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgayden.Properties.Mask.EditMask = "";
            this.lupNgayden.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.lupNgayden.Properties.NullText = "[EditValue is null]";
            this.lupNgayden.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lupNgayden.Size = new System.Drawing.Size(118, 20);
            this.lupNgayden.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Từ ngày:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Đến ngày:";
            // 
            // frm_thongkecanbophongkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 189);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.lupNgayden);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thongkecanbophongkham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê cán bộ phòng khám";
            this.Load += new System.EventHandler(this.frm_thongkecanbophongkham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.DateEdit lupNgayden;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}