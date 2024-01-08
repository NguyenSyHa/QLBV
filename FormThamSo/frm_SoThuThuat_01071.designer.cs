namespace QLBV.FormThamSo
{
    partial class frm_SoThuThuat_01071
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
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.radTrongDM = new DevExpress.XtraEditors.RadioGroup();
            this.cbo_NoiTru = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.cbo_DoiTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.ButHuy = new DevExpress.XtraEditors.SimpleButton();
            this.butTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lup_KPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_KPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl9.Location = new System.Drawing.Point(13, 79);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(104, 17);
            this.labelControl9.TabIndex = 76;
            this.labelControl9.Text = "Trong|Ngoài DM:";
            // 
            // radTrongDM
            // 
            this.radTrongDM.Location = new System.Drawing.Point(127, 73);
            this.radTrongDM.Name = "radTrongDM";
            this.radTrongDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTrongDM.Properties.Appearance.Options.UseFont = true;
            this.radTrongDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.radTrongDM.Size = new System.Drawing.Size(373, 29);
            this.radTrongDM.TabIndex = 5;
            // 
            // cbo_NoiTru
            // 
            this.cbo_NoiTru.EditValue = "Tất cả";
            this.cbo_NoiTru.Location = new System.Drawing.Point(361, 42);
            this.cbo_NoiTru.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_NoiTru.Name = "cbo_NoiTru";
            this.cbo_NoiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_NoiTru.Properties.Appearance.Options.UseFont = true;
            this.cbo_NoiTru.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_NoiTru.Properties.Items.AddRange(new object[] {
            "Ngoại trú",
            "Nội trú",
            "Tất cả"});
            this.cbo_NoiTru.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_NoiTru.Size = new System.Drawing.Size(139, 24);
            this.cbo_NoiTru.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(271, 45);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(82, 17);
            this.labelControl7.TabIndex = 73;
            this.labelControl7.Text = "Nội|Ngoại trú:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(13, 45);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 17);
            this.labelControl5.TabIndex = 72;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(271, 13);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 17);
            this.labelControl2.TabIndex = 71;
            this.labelControl2.Text = "đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 17);
            this.labelControl1.TabIndex = 70;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(361, 10);
            this.LupNgayden.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayden.Properties.Appearance.Options.UseFont = true;
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Size = new System.Drawing.Size(139, 24);
            this.LupNgayden.TabIndex = 2;
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(127, 10);
            this.LupNgaytu.Margin = new System.Windows.Forms.Padding(4);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Size = new System.Drawing.Size(136, 24);
            this.LupNgaytu.TabIndex = 1;
            // 
            // cbo_DoiTuong
            // 
            this.cbo_DoiTuong.EditValue = "99";
            this.cbo_DoiTuong.Location = new System.Drawing.Point(127, 42);
            this.cbo_DoiTuong.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_DoiTuong.Name = "cbo_DoiTuong";
            this.cbo_DoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_DoiTuong.Properties.Appearance.Options.UseFont = true;
            this.cbo_DoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_DoiTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDDTBN", "IDDTBN")});
            this.cbo_DoiTuong.Properties.DisplayMember = "DTBN1";
            this.cbo_DoiTuong.Properties.NullText = "";
            this.cbo_DoiTuong.Properties.PopupSizeable = false;
            this.cbo_DoiTuong.Properties.ValueMember = "IDDTBN";
            this.cbo_DoiTuong.Size = new System.Drawing.Size(136, 24);
            this.cbo_DoiTuong.TabIndex = 3;
            // 
            // ButHuy
            // 
            this.ButHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ButHuy.Appearance.Options.UseFont = true;
            this.ButHuy.Location = new System.Drawing.Point(292, 144);
            this.ButHuy.Margin = new System.Windows.Forms.Padding(4);
            this.ButHuy.Name = "ButHuy";
            this.ButHuy.Size = new System.Drawing.Size(61, 30);
            this.ButHuy.TabIndex = 8;
            this.ButHuy.Text = "&Hủy";
            this.ButHuy.Click += new System.EventHandler(this.ButHuy_Click);
            // 
            // butTaoBC
            // 
            this.butTaoBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.butTaoBC.Appearance.Options.UseFont = true;
            this.butTaoBC.Location = new System.Drawing.Point(184, 144);
            this.butTaoBC.Margin = new System.Windows.Forms.Padding(4);
            this.butTaoBC.Name = "butTaoBC";
            this.butTaoBC.Size = new System.Drawing.Size(100, 30);
            this.butTaoBC.TabIndex = 7;
            this.butTaoBC.Text = "&Tạo báo cáo";
            this.butTaoBC.Click += new System.EventHandler(this.butTaoBC_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(13, 115);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(74, 17);
            this.labelControl6.TabIndex = 80;
            this.labelControl6.Text = "Khoa phòng:";
            // 
            // lup_KPhong
            // 
            this.lup_KPhong.Location = new System.Drawing.Point(127, 112);
            this.lup_KPhong.Margin = new System.Windows.Forms.Padding(4);
            this.lup_KPhong.Name = "lup_KPhong";
            this.lup_KPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lup_KPhong.Properties.Appearance.Options.UseFont = true;
            this.lup_KPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_KPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa")});
            this.lup_KPhong.Properties.DisplayMember = "TenKP";
            this.lup_KPhong.Properties.NullText = "";
            this.lup_KPhong.Properties.PopupSizeable = false;
            this.lup_KPhong.Properties.ValueMember = "MaKP";
            this.lup_KPhong.Size = new System.Drawing.Size(373, 24);
            this.lup_KPhong.TabIndex = 6;
            // 
            // frm_SoThuThuat_01071
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 194);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.lup_KPhong);
            this.Controls.Add(this.ButHuy);
            this.Controls.Add(this.butTaoBC);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.radTrongDM);
            this.Controls.Add(this.cbo_NoiTru);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.Controls.Add(this.cbo_DoiTuong);
            this.Name = "frm_SoThuThuat_01071";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ thủ thuật";
            this.Load += new System.EventHandler(this.frm_SoThuThuat_01071_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radTrongDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_NoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_DoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_KPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.RadioGroup radTrongDM;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_NoiTru;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.LookUpEdit cbo_DoiTuong;
        private DevExpress.XtraEditors.SimpleButton ButHuy;
        private DevExpress.XtraEditors.SimpleButton butTaoBC;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lup_KPhong;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}