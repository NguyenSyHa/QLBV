namespace QLBV.FormThamSo
{
    partial class frm_BangKeChungTuThu_30003
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
            this.rgdtuong = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.rgplthu = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lupCB = new DevExpress.XtraEditors.LookUpEdit();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.NoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.TienPT = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rgdtuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgplthu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TienPT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(3, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(185, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 15);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // rgdtuong
            // 
            this.rgdtuong.Location = new System.Drawing.Point(56, 39);
            this.rgdtuong.Name = "rgdtuong";
            this.rgdtuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgdtuong.Properties.Appearance.Options.UseFont = true;
            this.rgdtuong.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "KSK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.rgdtuong.Size = new System.Drawing.Size(330, 23);
            this.rgdtuong.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(3, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 15);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Đ.Tượng:";
            // 
            // rgplthu
            // 
            this.rgplthu.Location = new System.Drawing.Point(56, 97);
            this.rgplthu.Name = "rgplthu";
            this.rgplthu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgplthu.Properties.Appearance.Options.UseFont = true;
            this.rgplthu.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thu thanh toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thu trực tiếp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.rgplthu.Size = new System.Drawing.Size(330, 23);
            this.rgplthu.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(3, 101);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(45, 15);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "PL Thu:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(6, 188);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(47, 15);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "CB Thu:";
            // 
            // lupCB
            // 
            this.lupCB.Location = new System.Drawing.Point(59, 186);
            this.lupCB.Name = "lupCB";
            this.lupCB.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupCB.Properties.Appearance.Options.UseFont = true;
            this.lupCB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCB.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", "Mã CB"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", "Tên CB Thu")});
            this.lupCB.Properties.DisplayMember = "TenCB";
            this.lupCB.Properties.ValueMember = "MaCB";
            this.lupCB.Size = new System.Drawing.Size(330, 22);
            this.lupCB.TabIndex = 4;
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(56, 10);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(127, 22);
            this.detungay.TabIndex = 0;
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(246, 10);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(140, 22);
            this.dedenngay.TabIndex = 1;
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Location = new System.Drawing.Point(219, 217);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(75, 23);
            this.btntaobc.TabIndex = 5;
            this.btntaobc.Text = "Tạo BC";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(300, 217);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // NoiNgoaiTru
            // 
            this.NoiNgoaiTru.Location = new System.Drawing.Point(56, 68);
            this.NoiNgoaiTru.Name = "NoiNgoaiTru";
            this.NoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.NoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.NoiNgoaiTru.Size = new System.Drawing.Size(330, 23);
            this.NoiNgoaiTru.TabIndex = 8;
            // 
            // TienPT
            // 
            this.TienPT.Location = new System.Drawing.Point(56, 126);
            this.TienPT.Name = "TienPT";
            this.TienPT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TienPT.Properties.Appearance.Options.UseFont = true;
            this.TienPT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Phiếu thu >= 200k"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Phiếu thu < 200k"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả")});
            this.TienPT.Size = new System.Drawing.Size(330, 54);
            this.TienPT.TabIndex = 10;
            // 
            // frm_BangKeChungTuThu_30003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 253);
            this.Controls.Add(this.TienPT);
            this.Controls.Add(this.NoiNgoaiTru);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btntaobc);
            this.Controls.Add(this.dedenngay);
            this.Controls.Add(this.detungay);
            this.Controls.Add(this.lupCB);
            this.Controls.Add(this.rgplthu);
            this.Controls.Add(this.rgdtuong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BangKeChungTuThu_30003";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng kê chứng từ thu";
            this.Load += new System.EventHandler(this.frm_BangKeChungTuThu_30003_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rgdtuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgplthu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TienPT.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup rgdtuong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup rgplthu;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lupCB;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.RadioGroup NoiNgoaiTru;
        private DevExpress.XtraEditors.RadioGroup TienPT;
    }
}