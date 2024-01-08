namespace QLBV.FormThamSo
{
    partial class frm_SoBanGiaoHSBN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SoBanGiaoHSBN));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lupKhoa = new DevExpress.XtraEditors.LookUpEdit();
            this.lupnguoigiao = new DevExpress.XtraEditors.LookUpEdit();
            this.lupnguoinhan = new DevExpress.XtraEditors.LookUpEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnguoigiao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnguoinhan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Khoa phòng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Người giao:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Người nhận:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(13, 108);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 19);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Ngày:";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(103, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 25);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "In ";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(167, 137);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(81, 25);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(24, 137);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(73, 25);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "In bìa";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lupKhoa
            // 
            this.lupKhoa.Location = new System.Drawing.Point(97, 10);
            this.lupKhoa.Name = "lupKhoa";
            this.lupKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoa.Properties.Appearance.Options.UseFont = true;
            this.lupKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng")});
            this.lupKhoa.Properties.DisplayMember = "TenKP";
            this.lupKhoa.Properties.NullText = "";
            this.lupKhoa.Properties.PopupSizeable = false;
            this.lupKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoa.Properties.ValueMember = "MaKP";
            this.lupKhoa.Size = new System.Drawing.Size(151, 26);
            this.lupKhoa.TabIndex = 1;
            this.lupKhoa.EditValueChanged += new System.EventHandler(this.lupKhoa_EditValueChanged);
            // 
            // lupnguoigiao
            // 
            this.lupnguoigiao.Location = new System.Drawing.Point(97, 42);
            this.lupnguoigiao.Name = "lupnguoigiao";
            this.lupnguoigiao.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupnguoigiao.Properties.Appearance.Options.UseFont = true;
            this.lupnguoigiao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupnguoigiao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 200, "Tên cán bộ")});
            this.lupnguoigiao.Properties.DisplayMember = "TenCB";
            this.lupnguoigiao.Properties.NullText = "";
            this.lupnguoigiao.Properties.PopupSizeable = false;
            this.lupnguoigiao.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupnguoigiao.Properties.ValueMember = "MaCB";
            this.lupnguoigiao.Size = new System.Drawing.Size(151, 26);
            this.lupnguoigiao.TabIndex = 2;
            // 
            // lupnguoinhan
            // 
            this.lupnguoinhan.Location = new System.Drawing.Point(97, 74);
            this.lupnguoinhan.Name = "lupnguoinhan";
            this.lupnguoinhan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupnguoinhan.Properties.Appearance.Options.UseFont = true;
            this.lupnguoinhan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupnguoinhan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 200, "Tên cán bộ")});
            this.lupnguoinhan.Properties.DisplayMember = "TenCB";
            this.lupnguoinhan.Properties.NullText = "";
            this.lupnguoinhan.Properties.PopupSizeable = false;
            this.lupnguoinhan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupnguoinhan.Properties.ValueMember = "MaCB";
            this.lupnguoinhan.Size = new System.Drawing.Size(151, 26);
            this.lupnguoinhan.TabIndex = 3;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(97, 105);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(151, 26);
            this.lupNgaytu.TabIndex = 4;
            // 
            // frm_SoBanGiaoHSBN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 171);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.lupnguoinhan);
            this.Controls.Add(this.lupnguoigiao);
            this.Controls.Add(this.lupKhoa);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SoBanGiaoHSBN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ giao ban HSBA";
            this.Load += new System.EventHandler(this.frm_SoBanGiaoHSBN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnguoigiao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupnguoinhan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LookUpEdit lupKhoa;
        private DevExpress.XtraEditors.LookUpEdit lupnguoigiao;
        private DevExpress.XtraEditors.LookUpEdit lupnguoinhan;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
    }
}