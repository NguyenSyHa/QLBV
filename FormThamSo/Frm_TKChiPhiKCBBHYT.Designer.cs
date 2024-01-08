namespace QLBV.FormThamSo
{
    partial class Frm_TKChiPhiKCBBHYT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TKChiPhiKCBBHYT));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboTKBN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.radTimKiem = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(224, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 106;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 105;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(292, 41);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupDenNgay.Size = new System.Drawing.Size(117, 22);
            this.lupDenNgay.TabIndex = 104;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(101, 41);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupTuNgay.Size = new System.Drawing.Size(117, 22);
            this.lupTuNgay.TabIndex = 103;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(15, 76);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 15);
            this.labelControl1.TabIndex = 110;
            this.labelControl1.Text = "Thống kê BN:";
            this.labelControl1.ToolTip = "Thống kê bệnh nhân";
            // 
            // cboTKBN
            // 
            this.cboTKBN.EditValue = "Tất cả BN";
            this.cboTKBN.Location = new System.Drawing.Point(101, 69);
            this.cboTKBN.Name = "cboTKBN";
            this.cboTKBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTKBN.Properties.Appearance.Options.UseFont = true;
            this.cboTKBN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTKBN.Properties.Items.AddRange(new object[] {
            "Tất cả BN",
            "BN Nội trú",
            "BN Ngoại trú"});
            this.cboTKBN.Size = new System.Drawing.Size(308, 22);
            this.cboTKBN.TabIndex = 109;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(313, 157);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 108;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(181, 157);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(81, 23);
            this.btnInBC.TabIndex = 107;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // radTimKiem
            // 
            this.radTimKiem.EditValue = 0;
            this.radTimKiem.Location = new System.Drawing.Point(101, 98);
            this.radTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.radTimKiem.Name = "radTimKiem";
            this.radTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTimKiem.Properties.Appearance.Options.UseFont = true;
            this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tìm kiếm theo ngày RV"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Tìm kiếm theo ngày T.Toán")});
            this.radTimKiem.Properties.SelectedIndexChanged += new System.EventHandler(this.rdFont_Properties_SelectedIndexChanged);
            this.radTimKiem.Size = new System.Drawing.Size(308, 31);
            this.radTimKiem.TabIndex = 111;
            // 
            // Frm_TKChiPhiKCBBHYT
            // 
            this.AcceptButton = this.btnInBC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 219);
            this.Controls.Add(this.radTimKiem);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboTKBN);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TKChiPhiKCBBHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Thống kê chi phí KCB BHYT";
            this.Load += new System.EventHandler(this.Frm_TKChiPhiKCBBHYT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTKBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboTKBN;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.RadioGroup radTimKiem;
    }
}