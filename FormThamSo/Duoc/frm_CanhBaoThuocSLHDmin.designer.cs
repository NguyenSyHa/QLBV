namespace QLBV.FormThamSo
{
    partial class frm_CanhBaoThuocSLHDmin
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
            this.dENgay = new DevExpress.XtraEditors.DateEdit();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.check6 = new DevExpress.XtraEditors.CheckEdit();
            this.check12 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.check9 = new DevExpress.XtraEditors.CheckEdit();
            this.check3 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dENgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dENgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.check6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.check12.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.check9.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.check3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dENgay
            // 
            this.dENgay.EditValue = "";
            this.dENgay.Location = new System.Drawing.Point(101, 12);
            this.dENgay.Name = "dENgay";
            this.dENgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dENgay.Properties.Appearance.Options.UseFont = true;
            this.dENgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dENgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dENgay.Size = new System.Drawing.Size(266, 24);
            this.dENgay.TabIndex = 22;
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(101, 51);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên kho")});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(266, 24);
            this.lupKho.TabIndex = 23;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(11, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 17);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "Ngày tháng: ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(11, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "Kho dược: ";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(12, 88);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Min = 30% Tồn ";
            this.checkEdit1.Size = new System.Drawing.Size(75, 19);
            this.checkEdit1.TabIndex = 26;
            // 
            // check6
            // 
            this.check6.Location = new System.Drawing.Point(249, 88);
            this.check6.Name = "check6";
            this.check6.Properties.Caption = "Cảnh báo 6 tháng";
            this.check6.Size = new System.Drawing.Size(118, 19);
            this.check6.TabIndex = 27;
            this.check6.CheckedChanged += new System.EventHandler(this.check6_CheckedChanged);
            // 
            // check12
            // 
            this.check12.Location = new System.Drawing.Point(249, 113);
            this.check12.Name = "check12";
            this.check12.Properties.Caption = "Cảnh báo 12 tháng";
            this.check12.Size = new System.Drawing.Size(118, 19);
            this.check12.TabIndex = 28;
            this.check12.CheckedChanged += new System.EventHandler(this.check12_CheckedChanged);
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(11, 113);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Hiển thị số lô";
            this.checkEdit2.Size = new System.Drawing.Size(118, 19);
            this.checkEdit2.TabIndex = 29;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Appearance.Options.UseForeColor = true;
            this.btnThoat.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btnThoat.Location = new System.Drawing.Point(204, 138);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(66, 25);
            this.btnThoat.TabIndex = 21;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBaoCao.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnInBaoCao.Appearance.Options.UseFont = true;
            this.btnInBaoCao.Appearance.Options.UseForeColor = true;
            this.btnInBaoCao.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnInBaoCao.Location = new System.Drawing.Point(101, 138);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(97, 25);
            this.btnInBaoCao.TabIndex = 20;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // check9
            // 
            this.check9.Location = new System.Drawing.Point(116, 113);
            this.check9.Name = "check9";
            this.check9.Properties.Caption = "Cảnh báo 9 tháng";
            this.check9.Size = new System.Drawing.Size(118, 19);
            this.check9.TabIndex = 31;
            this.check9.CheckedChanged += new System.EventHandler(this.check9_CheckedChanged);
            // 
            // check3
            // 
            this.check3.Location = new System.Drawing.Point(116, 88);
            this.check3.Name = "check3";
            this.check3.Properties.Caption = "Cảnh báo 3 tháng";
            this.check3.Size = new System.Drawing.Size(118, 19);
            this.check3.TabIndex = 30;
            this.check3.CheckedChanged += new System.EventHandler(this.check3_CheckedChanged);
            // 
            // frm_CanhBaoThuocSLHDmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 173);
            this.Controls.Add(this.check9);
            this.Controls.Add(this.check3);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.check12);
            this.Controls.Add(this.check6);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.dENgay);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnInBaoCao);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CanhBaoThuocSLHDmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THUỐC CÓ SỐ LƯỢNG HẠN DÙNG MIN";
            this.Load += new System.EventHandler(this.frm_CanhBaoThuocSLHDmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dENgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dENgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.check6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.check12.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.check9.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.check3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnInBaoCao;
        private DevExpress.XtraEditors.DateEdit dENgay;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit check6;
        private DevExpress.XtraEditors.CheckEdit check12;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit check9;
        private DevExpress.XtraEditors.CheckEdit check3;

    }
}