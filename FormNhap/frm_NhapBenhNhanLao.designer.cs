namespace QLBV.FormNhap
{
    partial class frm_NhapBenhNhanLao
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
            this.txtSo_eTBM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtThangThoiDoi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupChanDoan = new DevExpress.XtraEditors.LookUpEdit();
            this.lupDoiTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupTinhTrangH = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.rdTienSuDtriLao = new DevExpress.XtraEditors.RadioGroup();
            this.lblErr = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtDtuongLaoKhac = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSo_eTBM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThoiDoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupChanDoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTinhTrangH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTienSuDtriLao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtuongLaoKhac.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSo_eTBM
            // 
            this.txtSo_eTBM.Location = new System.Drawing.Point(137, 11);
            this.txtSo_eTBM.Name = "txtSo_eTBM";
            this.txtSo_eTBM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSo_eTBM.Properties.Appearance.Options.UseFont = true;
            this.txtSo_eTBM.Size = new System.Drawing.Size(314, 24);
            this.txtSo_eTBM.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(24, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Số eTBM:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(24, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 17);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Tháng theo dõi: ";
            // 
            // txtThangThoiDoi
            // 
            this.txtThangThoiDoi.EditValue = "0";
            this.txtThangThoiDoi.Location = new System.Drawing.Point(137, 41);
            this.txtThangThoiDoi.Name = "txtThangThoiDoi";
            this.txtThangThoiDoi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThangThoiDoi.Properties.Appearance.Options.UseFont = true;
            this.txtThangThoiDoi.Size = new System.Drawing.Size(314, 24);
            this.txtThangThoiDoi.TabIndex = 2;
            this.txtThangThoiDoi.EditValueChanged += new System.EventHandler(this.txtThangThoiDoi_EditValueChanged);
            this.txtThangThoiDoi.TextChanged += new System.EventHandler(this.txtThangThoiDoi_TextChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(24, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 17);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Chẩn đoán:";
            // 
            // lupChanDoan
            // 
            this.lupChanDoan.Location = new System.Drawing.Point(137, 100);
            this.lupChanDoan.Name = "lupChanDoan";
            this.lupChanDoan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupChanDoan.Properties.Appearance.Options.UseFont = true;
            this.lupChanDoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupChanDoan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Chẩn đoán")});
            this.lupChanDoan.Properties.NullText = "";
            this.lupChanDoan.Size = new System.Drawing.Size(314, 24);
            this.lupChanDoan.TabIndex = 4;
            this.lupChanDoan.EditValueChanged += new System.EventHandler(this.lupChanDoan_EditValueChanged);
            // 
            // lupDoiTuong
            // 
            this.lupDoiTuong.Location = new System.Drawing.Point(137, 128);
            this.lupDoiTuong.Name = "lupDoiTuong";
            this.lupDoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupDoiTuong.Properties.Appearance.Options.UseFont = true;
            this.lupDoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDoiTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Đối tượng")});
            this.lupDoiTuong.Properties.NullText = "";
            this.lupDoiTuong.Size = new System.Drawing.Size(314, 24);
            this.lupDoiTuong.TabIndex = 5;
            this.lupDoiTuong.EditValueChanged += new System.EventHandler(this.lupDoiTuong_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(24, 130);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 17);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Đối tượng:";
            this.labelControl4.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // lupTinhTrangH
            // 
            this.lupTinhTrangH.Location = new System.Drawing.Point(137, 71);
            this.lupTinhTrangH.Name = "lupTinhTrangH";
            this.lupTinhTrangH.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTinhTrangH.Properties.Appearance.Options.UseFont = true;
            this.lupTinhTrangH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTinhTrangH.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Tình trạng H")});
            this.lupTinhTrangH.Properties.NullText = "";
            this.lupTinhTrangH.Size = new System.Drawing.Size(314, 24);
            this.lupTinhTrangH.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(24, 73);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 17);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Tình trạng H:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(24, 192);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(100, 17);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Tiền sử Đtrị Lao:";
            // 
            // rdTienSuDtriLao
            // 
            this.rdTienSuDtriLao.Location = new System.Drawing.Point(137, 191);
            this.rdTienSuDtriLao.Name = "rdTienSuDtriLao";
            this.rdTienSuDtriLao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "< 1 tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "≥ 1 tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Không xác định")});
            this.rdTienSuDtriLao.Size = new System.Drawing.Size(314, 52);
            this.rdTienSuDtriLao.TabIndex = 6;
            // 
            // lblErr
            // 
            this.lblErr.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErr.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblErr.Location = new System.Drawing.Point(139, 285);
            this.lblErr.Name = "lblErr";
            this.lblErr.Size = new System.Drawing.Size(0, 15);
            this.lblErr.TabIndex = 14;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btnThoat.Location = new System.Drawing.Point(269, 270);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(107, 29);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = global::QLBV.Properties.Resources.save_16x16;
            this.btnOK.Location = new System.Drawing.Point(137, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(117, 29);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Lưu";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDtuongLaoKhac
            // 
            this.txtDtuongLaoKhac.Enabled = false;
            this.txtDtuongLaoKhac.Location = new System.Drawing.Point(137, 159);
            this.txtDtuongLaoKhac.Name = "txtDtuongLaoKhac";
            this.txtDtuongLaoKhac.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDtuongLaoKhac.Properties.Appearance.Options.UseFont = true;
            this.txtDtuongLaoKhac.Size = new System.Drawing.Size(314, 24);
            this.txtDtuongLaoKhac.TabIndex = 15;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Enabled = false;
            this.labelControl7.Location = new System.Drawing.Point(83, 162);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(35, 17);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "Khác:";
            // 
            // frm_NhapBenhNhanLao
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 311);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtDtuongLaoKhac);
            this.Controls.Add(this.lblErr);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdTienSuDtriLao);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.lupTinhTrangH);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lupDoiTuong);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lupChanDoan);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtThangThoiDoi);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSo_eTBM);
            this.Name = "frm_NhapBenhNhanLao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập bệnh nhân lao";
            this.Load += new System.EventHandler(this.frm_NhapBenhNhanLao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSo_eTBM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThoiDoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupChanDoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTinhTrangH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTienSuDtriLao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtuongLaoKhac.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtSo_eTBM;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtThangThoiDoi;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupChanDoan;
        private DevExpress.XtraEditors.LookUpEdit lupDoiTuong;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupTinhTrangH;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup rdTienSuDtriLao;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.LabelControl lblErr;
        private DevExpress.XtraEditors.TextEdit txtDtuongLaoKhac;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}