namespace QLBV.FormThamSo
{
    partial class Frm_GiayCD_PTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_GiayCD_PTTT));
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.chk = new DevExpress.XtraEditors.CheckEdit();
            this.txtTenCD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radGT = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTuoi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDanToc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtNK = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtNN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtNoiLV = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenBN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.rad2 = new DevExpress.XtraEditors.RadioGroup();
            this.lupKhoa = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDanToc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiLV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lab1
            // 
            this.lab1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lab1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lab1.Location = new System.Drawing.Point(81, 12);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(0, 19);
            this.lab1.TabIndex = 0;
            // 
            // chk
            // 
            this.chk.Location = new System.Drawing.Point(212, 38);
            this.chk.Name = "chk";
            this.chk.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk.Properties.Appearance.Options.UseFont = true;
            this.chk.Properties.Caption = "Bệnh nhân trực tiếp cam đoan";
            this.chk.Size = new System.Drawing.Size(245, 24);
            this.chk.TabIndex = 1;
            this.chk.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // txtTenCD
            // 
            this.txtTenCD.Location = new System.Drawing.Point(126, 74);
            this.txtTenCD.Name = "txtTenCD";
            this.txtTenCD.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenCD.Properties.Appearance.Options.UseFont = true;
            this.txtTenCD.Size = new System.Drawing.Size(165, 26);
            this.txtTenCD.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(297, 77);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 19);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Giới tính:";
            // 
            // radGT
            // 
            this.radGT.Location = new System.Drawing.Point(371, 73);
            this.radGT.Name = "radGT";
            this.radGT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radGT.Properties.Appearance.Options.UseFont = true;
            this.radGT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nam"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nữ")});
            this.radGT.Size = new System.Drawing.Size(110, 27);
            this.radGT.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(30, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(90, 19);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Tên người CĐ:";
            // 
            // txtTuoi
            // 
            this.txtTuoi.Location = new System.Drawing.Point(529, 74);
            this.txtTuoi.Name = "txtTuoi";
            this.txtTuoi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTuoi.Properties.Appearance.Options.UseFont = true;
            this.txtTuoi.Size = new System.Drawing.Size(49, 26);
            this.txtTuoi.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(493, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 19);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Tuổi:";
            // 
            // txtDC
            // 
            this.txtDC.Location = new System.Drawing.Point(126, 170);
            this.txtDC.Name = "txtDC";
            this.txtDC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDC.Properties.Appearance.Options.UseFont = true;
            this.txtDC.Size = new System.Drawing.Size(452, 26);
            this.txtDC.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(30, 170);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(45, 19);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Địa chỉ:";
            // 
            // txtDanToc
            // 
            this.txtDanToc.Location = new System.Drawing.Point(126, 106);
            this.txtDanToc.Name = "txtDanToc";
            this.txtDanToc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDanToc.Properties.Appearance.Options.UseFont = true;
            this.txtDanToc.Size = new System.Drawing.Size(165, 26);
            this.txtDanToc.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(30, 109);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 19);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "Dân tộc:";
            // 
            // txtNK
            // 
            this.txtNK.Location = new System.Drawing.Point(371, 106);
            this.txtNK.Name = "txtNK";
            this.txtNK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNK.Properties.Appearance.Options.UseFont = true;
            this.txtNK.Size = new System.Drawing.Size(207, 26);
            this.txtNK.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(296, 109);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(69, 19);
            this.labelControl7.TabIndex = 3;
            this.labelControl7.Text = "Ngoại kiều:";
            // 
            // txtNN
            // 
            this.txtNN.Location = new System.Drawing.Point(126, 138);
            this.txtNN.Name = "txtNN";
            this.txtNN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNN.Properties.Appearance.Options.UseFont = true;
            this.txtNN.Size = new System.Drawing.Size(165, 26);
            this.txtNN.TabIndex = 5;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl8.Location = new System.Drawing.Point(30, 141);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(79, 19);
            this.labelControl8.TabIndex = 3;
            this.labelControl8.Text = "Nghề nghiệp:";
            // 
            // txtNoiLV
            // 
            this.txtNoiLV.Location = new System.Drawing.Point(371, 138);
            this.txtNoiLV.Name = "txtNoiLV";
            this.txtNoiLV.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNoiLV.Properties.Appearance.Options.UseFont = true;
            this.txtNoiLV.Size = new System.Drawing.Size(207, 26);
            this.txtNoiLV.TabIndex = 6;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl9.Location = new System.Drawing.Point(297, 141);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(71, 19);
            this.labelControl9.TabIndex = 3;
            this.labelControl9.Text = "Nơi L.Việc:";
            // 
            // txtTenBN
            // 
            this.txtTenBN.Location = new System.Drawing.Point(126, 202);
            this.txtTenBN.Name = "txtTenBN";
            this.txtTenBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenBN.Properties.Appearance.Options.UseFont = true;
            this.txtTenBN.Size = new System.Drawing.Size(452, 26);
            this.txtTenBN.TabIndex = 8;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl10.Location = new System.Drawing.Point(30, 205);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(91, 19);
            this.labelControl10.TabIndex = 3;
            this.labelControl10.Text = "Là N.Nhà BN:";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl11.Location = new System.Drawing.Point(30, 237);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(77, 19);
            this.labelControl11.TabIndex = 3;
            this.labelControl11.Text = "Đang ĐT tại:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(362, 351);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 23);
            this.btnHuy.TabIndex = 12;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(207, 351);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(104, 23);
            this.btnInBC.TabIndex = 11;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // rad2
            // 
            this.rad2.Location = new System.Drawing.Point(126, 266);
            this.rad2.Name = "rad2";
            this.rad2.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rad2.Properties.Appearance.Options.UseFont = true;
            this.rad2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Đồng ý xin phẫu thuật, thủ thuật, gây mê hồi sức "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Không đồng ý phẫu thuật, thủ thuật, gây mê hồi sức ")});
            this.rad2.Size = new System.Drawing.Size(452, 57);
            this.rad2.TabIndex = 10;
            // 
            // lupKhoa
            // 
            this.lupKhoa.Location = new System.Drawing.Point(126, 234);
            this.lupKhoa.Name = "lupKhoa";
            this.lupKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoa.Properties.Appearance.Options.UseFont = true;
            this.lupKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoa.Properties.DisplayMember = "TenKP";
            this.lupKhoa.Properties.NullText = "";
            this.lupKhoa.Properties.PopupSizeable = false;
            this.lupKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoa.Properties.ValueMember = "MaKP";
            this.lupKhoa.Size = new System.Drawing.Size(452, 26);
            this.lupKhoa.TabIndex = 9;
            // 
            // Frm_GiayCD_PTTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 407);
            this.Controls.Add(this.lupKhoa);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.rad2);
            this.Controls.Add(this.radGT);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtTuoi);
            this.Controls.Add(this.txtTenBN);
            this.Controls.Add(this.txtDC);
            this.Controls.Add(this.txtNK);
            this.Controls.Add(this.txtNoiLV);
            this.Controls.Add(this.txtNN);
            this.Controls.Add(this.txtDanToc);
            this.Controls.Add(this.txtTenCD);
            this.Controls.Add(this.chk);
            this.Controls.Add(this.lab1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_GiayCD_PTTT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Giấy cam đoan chấp nhận phẫu thuật, thủ thuật và gây mê hồi sức. ";
            this.Load += new System.EventHandler(this.Frm_GiayCD_PTTT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDanToc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiLV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.CheckEdit chk;
        private DevExpress.XtraEditors.TextEdit txtTenCD;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup radGT;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTuoi;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDC;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDanToc;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtNK;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtNN;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtNoiLV;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtTenBN;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.RadioGroup rad2;
        private DevExpress.XtraEditors.LookUpEdit lupKhoa;
    }
}