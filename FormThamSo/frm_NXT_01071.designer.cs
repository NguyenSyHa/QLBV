namespace QLBV.FormThamSo
{
    partial class frm_NXT_01071
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NXT_01071));
            this.loaixuat = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.label7 = new System.Windows.Forms.Label();
            this.lupTieuNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.lupNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.lupNhaCC = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.loaixuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // loaixuat
            // 
            this.loaixuat.CheckOnClick = true;
            this.loaixuat.DisplayMember = "TenCot";
            this.loaixuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loaixuat.Location = new System.Drawing.Point(2, 21);
            this.loaixuat.MultiColumn = true;
            this.loaixuat.Name = "loaixuat";
            this.loaixuat.Size = new System.Drawing.Size(281, 121);
            this.loaixuat.TabIndex = 0;
            this.loaixuat.ValueMember = "MaCot";
            this.loaixuat.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.loaixuat_ItemCheck);
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.loaixuat);
            this.groupControl3.Location = new System.Drawing.Point(17, 85);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(285, 144);
            this.groupControl3.TabIndex = 6;
            this.groupControl3.Text = "Chọn loại xuất";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(305, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 14);
            this.label7.TabIndex = 74;
            this.label7.Text = "Tiểu nhóm:";
            // 
            // lupTieuNhom
            // 
            this.lupTieuNhom.Location = new System.Drawing.Point(403, 59);
            this.lupTieuNhom.Name = "lupTieuNhom";
            this.lupTieuNhom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTieuNhom.Properties.Appearance.Options.UseFont = true;
            this.lupTieuNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTieuNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", 200, "nhóm dược")});
            this.lupTieuNhom.Properties.DisplayMember = "TenTN";
            this.lupTieuNhom.Properties.NullText = "";
            this.lupTieuNhom.Properties.PopupSizeable = false;
            this.lupTieuNhom.Properties.ValueMember = "IdTieuNhom";
            this.lupTieuNhom.Size = new System.Drawing.Size(185, 20);
            this.lupTieuNhom.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(305, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 14);
            this.label8.TabIndex = 72;
            this.label8.Text = "Nhóm:";
            // 
            // lupNhom
            // 
            this.lupNhom.Location = new System.Drawing.Point(403, 33);
            this.lupNhom.Name = "lupNhom";
            this.lupNhom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNhom.Properties.Appearance.Options.UseFont = true;
            this.lupNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", 200, "Loại dược")});
            this.lupNhom.Properties.DisplayMember = "TenNhom";
            this.lupNhom.Properties.NullText = "";
            this.lupNhom.Properties.PopupSizeable = false;
            this.lupNhom.Properties.ValueMember = "IDNhom";
            this.lupNhom.Size = new System.Drawing.Size(185, 20);
            this.lupNhom.TabIndex = 4;
            this.lupNhom.EditValueChanged += new System.EventHandler(this.lupNhom_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 67;
            this.label2.Text = "Từ ngày:";
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(115, 6);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(180, 20);
            this.dateTuNgay.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(305, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 14);
            this.label4.TabIndex = 68;
            this.label4.Text = "Nhà cung cấp:";
            // 
            // lupNhaCC
            // 
            this.lupNhaCC.Location = new System.Drawing.Point(403, 6);
            this.lupNhaCC.Name = "lupNhaCC";
            this.lupNhaCC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNhaCC.Properties.Appearance.Options.UseFont = true;
            this.lupNhaCC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhaCC.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", 200, "Tên nhà cung cấp")});
            this.lupNhaCC.Properties.DisplayMember = "TenCC";
            this.lupNhaCC.Properties.NullText = "";
            this.lupNhaCC.Properties.PopupSizeable = false;
            this.lupNhaCC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNhaCC.Properties.ValueMember = "MaCC";
            this.lupNhaCC.Size = new System.Drawing.Size(185, 20);
            this.lupNhaCC.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 69;
            this.label3.Text = "Đến:";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(115, 32);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Size = new System.Drawing.Size(180, 20);
            this.dateDenNgay.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.cklKP);
            this.groupControl1.Location = new System.Drawing.Point(308, 85);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(329, 144);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Chọn kho";
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.DisplayMember = "TenKP";
            this.cklKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklKP.Location = new System.Drawing.Point(2, 21);
            this.cklKP.MultiColumn = true;
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(325, 121);
            this.cklKP.TabIndex = 0;
            this.cklKP.ValueMember = "MaKP";
            this.cklKP.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKP_ItemCheck);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(15, 58);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.checkEdit1.Properties.Appearance.Options.UseFont = true;
            this.checkEdit1.Properties.Caption = "Xuất BH, DV";
            this.checkEdit1.Size = new System.Drawing.Size(103, 19);
            this.checkEdit1.TabIndex = 75;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(115, 58);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCot", 200, "Phân Loại")});
            this.lookUpEdit1.Properties.DisplayMember = "TenCot";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.PopupSizeable = false;
            this.lookUpEdit1.Properties.ValueMember = "MaPL";
            this.lookUpEdit1.Size = new System.Drawing.Size(180, 20);
            this.lookUpEdit1.TabIndex = 76;
            this.lookUpEdit1.Visible = false;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(562, 235);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(464, 235);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(92, 23);
            this.btnInBC.TabIndex = 8;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // frm_NXT_01071
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 270);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lupTieuNhom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lupNhom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTuNgay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lupNhaCC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.groupControl3);
            this.Name = "frm_NXT_01071";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhập, xuất, tồn";
            this.Load += new System.EventHandler(this.frm_NXT_01071_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loaixuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl loaixuat;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.LookUpEdit lupTieuNhom;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.LookUpEdit lupNhom;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.LookUpEdit lupNhaCC;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;


    }
}