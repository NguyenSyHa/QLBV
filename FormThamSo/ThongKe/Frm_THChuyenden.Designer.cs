namespace QLBV.FormThamSo
{
    partial class Frm_THChuyenden
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
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.sbtTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.sbtHuy = new DevExpress.XtraEditors.SimpleButton();
            this.radTT = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.cklBV = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cklBV)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(14, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(191, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(254, 33);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayden.Properties.Appearance.Options.UseFont = true;
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Size = new System.Drawing.Size(101, 20);
            this.LupNgayden.TabIndex = 2;
            this.LupNgayden.EditValueChanged += new System.EventHandler(this.LupNgayden_EditValueChanged);
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(84, 33);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Size = new System.Drawing.Size(101, 20);
            this.LupNgaytu.TabIndex = 3;
            this.LupNgaytu.EditValueChanged += new System.EventHandler(this.LupNgaytu_EditValueChanged);
            // 
            // sbtTaoBC
            // 
            this.sbtTaoBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtTaoBC.Appearance.Options.UseFont = true;
            this.sbtTaoBC.Location = new System.Drawing.Point(103, 265);
            this.sbtTaoBC.Name = "sbtTaoBC";
            this.sbtTaoBC.Size = new System.Drawing.Size(82, 23);
            this.sbtTaoBC.TabIndex = 4;
            this.sbtTaoBC.Text = "Tạo BC";
            this.sbtTaoBC.Click += new System.EventHandler(this.sbtTaoBC_Click);
            // 
            // sbtHuy
            // 
            this.sbtHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtHuy.Appearance.Options.UseFont = true;
            this.sbtHuy.Location = new System.Drawing.Point(254, 265);
            this.sbtHuy.Name = "sbtHuy";
            this.sbtHuy.Size = new System.Drawing.Size(82, 23);
            this.sbtHuy.TabIndex = 5;
            this.sbtHuy.Text = "Huỷ";
            this.sbtHuy.Click += new System.EventHandler(this.sbtHuy_Click);
            // 
            // radTT
            // 
            this.radTT.Location = new System.Drawing.Point(84, 216);
            this.radTT.Name = "radTT";
            this.radTT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Đã thanh toán(lấy theo ngày TT)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chưa thanh toán(lấy theo ngày khám bệnh)")});
            this.radTT.Size = new System.Drawing.Size(271, 43);
            this.radTT.TabIndex = 6;
            this.radTT.SelectedIndexChanged += new System.EventHandler(this.radTT_SelectedIndexChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.cklBV);
            this.groupControl3.Location = new System.Drawing.Point(84, 59);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(284, 151);
            this.groupControl3.TabIndex = 46;
            this.groupControl3.Text = "Chọn bệnh viện";
            // 
            // cklBV
            // 
            this.cklBV.CheckOnClick = true;
            this.cklBV.DisplayMember = "TenBV";
            this.cklBV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklBV.Location = new System.Drawing.Point(2, 21);
            this.cklBV.MultiColumn = true;
            this.cklBV.Name = "cklBV";
            this.cklBV.Size = new System.Drawing.Size(280, 128);
            this.cklBV.TabIndex = 0;
            this.cklBV.ValueMember = "MaBV";
            this.cklBV.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklBV_ItemCheck);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(14, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 47;
            this.labelControl3.Text = "Bệnh viện:";
            // 
            // Frm_THChuyenden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 296);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.radTT);
            this.Controls.Add(this.sbtHuy);
            this.Controls.Add(this.sbtTaoBC);
            this.Controls.Add(this.LupNgaytu);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_THChuyenden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp bệnh nhân chuyển đến";
            this.Load += new System.EventHandler(this.Frm_THChuyenden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cklBV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.SimpleButton sbtTaoBC;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
        private DevExpress.XtraEditors.RadioGroup radTT;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.CheckedListBoxControl cklBV;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}