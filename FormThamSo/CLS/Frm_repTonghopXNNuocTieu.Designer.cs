namespace QLBV.FormThamSo
{
    partial class Frm_repTonghopXNNuocTieu
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
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.butTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.ButHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(71, 27);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgaytu.Size = new System.Drawing.Size(100, 20);
            this.LupNgaytu.TabIndex = 0;
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(71, 53);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgayden.Size = new System.Drawing.Size(100, 20);
            this.LupNgayden.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // butTaoBC
            // 
            this.butTaoBC.Location = new System.Drawing.Point(24, 97);
            this.butTaoBC.Name = "butTaoBC";
            this.butTaoBC.Size = new System.Drawing.Size(75, 23);
            this.butTaoBC.TabIndex = 4;
            this.butTaoBC.Text = "Tạo báo cáo";
            this.butTaoBC.Click += new System.EventHandler(this.butTaoBC_Click);
            // 
            // ButHuy
            // 
            this.ButHuy.Location = new System.Drawing.Point(133, 97);
            this.ButHuy.Name = "ButHuy";
            this.ButHuy.Size = new System.Drawing.Size(75, 23);
            this.ButHuy.TabIndex = 5;
            this.ButHuy.Text = "Hủy";
            this.ButHuy.Click += new System.EventHandler(this.ButHuy_Click);
            // 
            // Frm_repTonghopXNNuocTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 144);
            this.Controls.Add(this.ButHuy);
            this.Controls.Add(this.butTaoBC);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_repTonghopXNNuocTieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp XN Nước tiểu";
            this.Load += new System.EventHandler(this.Frm_repTonghopHoaSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton butTaoBC;
        private DevExpress.XtraEditors.SimpleButton ButHuy;
    }
}