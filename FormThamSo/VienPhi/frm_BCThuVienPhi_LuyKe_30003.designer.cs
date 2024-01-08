namespace QLBV.FormThamSo
{
    partial class frm_BCThuVienPhi_LuyKe_30003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCThuVienPhi_LuyKe_30003));
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrongdo = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.ckHTCTPhongKham = new DevExpress.XtraEditors.CheckEdit();
            this.ckListCP = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.ckTrongNgoaiBH = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lupDoituong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrongdo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckHTCTPhongKham.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboThang
            // 
            this.cboThang.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(170, 26);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(156, 25);
            this.cboThang.TabIndex = 133;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 17);
            this.label1.TabIndex = 132;
            this.label1.Text = "Chọn tháng báo cáo";
            // 
            // cbNam
            // 
            this.cbNam.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(170, 54);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(156, 25);
            this.cbNam.TabIndex = 131;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(25, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 128;
            this.label2.Text = "Chọn năm báo cáo";
            // 
            // txtTrongdo
            // 
            this.txtTrongdo.Location = new System.Drawing.Point(110, 243);
            this.txtTrongdo.Name = "txtTrongdo";
            this.txtTrongdo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrongdo.Properties.Appearance.Options.UseFont = true;
            this.txtTrongdo.Size = new System.Drawing.Size(216, 24);
            this.txtTrongdo.TabIndex = 137;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(24, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 138;
            this.label3.Text = "Ghi chú:";
            // 
            // ckHTCTPhongKham
            // 
            this.ckHTCTPhongKham.Location = new System.Drawing.Point(110, 218);
            this.ckHTCTPhongKham.Name = "ckHTCTPhongKham";
            this.ckHTCTPhongKham.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckHTCTPhongKham.Properties.Appearance.Options.UseFont = true;
            this.ckHTCTPhongKham.Properties.Caption = "Hiển thị chi tiết chi phí phòng khám";
            this.ckHTCTPhongKham.Size = new System.Drawing.Size(216, 19);
            this.ckHTCTPhongKham.TabIndex = 139;
            // 
            // ckListCP
            // 
            this.ckListCP.CheckOnClick = true;
            this.ckListCP.FormattingEnabled = true;
            this.ckListCP.Items.AddRange(new object[] {
            "Chi Phí BN chi trả",
            "Chi Phí BH chi trả",
            "Chi Phí được miễn"});
            this.ckListCP.Location = new System.Drawing.Point(170, 160);
            this.ckListCP.Name = "ckListCP";
            this.ckListCP.Size = new System.Drawing.Size(156, 52);
            this.ckListCP.TabIndex = 140;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(110, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 23);
            this.btnOK.TabIndex = 129;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(210, 277);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(116, 23);
            this.btnThoat.TabIndex = 130;
            this.btnThoat.Text = "&Thoát";
            // 
            // ckTrongNgoaiBH
            // 
            this.ckTrongNgoaiBH.CheckOnClick = true;
            this.ckTrongNgoaiBH.FormattingEnabled = true;
            this.ckTrongNgoaiBH.Items.AddRange(new object[] {
            "Chi Phí Ngoài BH",
            "Chi Phí Trong BH"});
            this.ckTrongNgoaiBH.Location = new System.Drawing.Point(170, 117);
            this.ckTrongNgoaiBH.Name = "ckTrongNgoaiBH";
            this.ckTrongNgoaiBH.Size = new System.Drawing.Size(156, 36);
            this.ckTrongNgoaiBH.TabIndex = 141;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(24, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 17);
            this.label4.TabIndex = 142;
            this.label4.Text = "CP trong ngoài DM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(26, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 17);
            this.label5.TabIndex = 143;
            this.label5.Text = "CP theo đối tượng";
            // 
            // lupDoituong
            // 
            this.lupDoituong.EnterMoveNextControl = true;
            this.lupDoituong.Location = new System.Drawing.Point(170, 81);
            this.lupDoituong.Name = "lupDoituong";
            this.lupDoituong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDoituong.Properties.Appearance.Options.UseFont = true;
            this.lupDoituong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDoituong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng")});
            this.lupDoituong.Properties.NullText = "BHYT";
            this.lupDoituong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupDoituong.Size = new System.Drawing.Size(156, 26);
            this.lupDoituong.TabIndex = 144;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(28, 86);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 17);
            this.labelControl5.TabIndex = 145;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // frm_BCThuVienPhi_LuyKe_30003
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 308);
            this.Controls.Add(this.lupDoituong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckTrongNgoaiBH);
            this.Controls.Add(this.ckListCP);
            this.Controls.Add(this.ckHTCTPhongKham);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTrongdo);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Name = "frm_BCThuVienPhi_LuyKe_30003";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo thu viện phí lũy kế";
            this.Load += new System.EventHandler(this.frm_BCThuVienPhi_LuyKe_30003_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTrongdo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckHTCTPhongKham.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNam;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtTrongdo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckEdit ckHTCTPhongKham;
        private System.Windows.Forms.CheckedListBox ckListCP;
        private System.Windows.Forms.CheckedListBox ckTrongNgoaiBH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lupDoituong;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}