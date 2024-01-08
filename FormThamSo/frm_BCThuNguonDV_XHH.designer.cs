namespace QLBV.FormThamSo
{
    partial class frm_BCThuNguonDV_XHH
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
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.lupngayden = new DevExpress.XtraEditors.DateEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cklKhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.trongbh = new System.Windows.Forms.CheckBox();
            this.ngoaibh = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.EnterMoveNextControl = true;
            this.lupNgaytu.Location = new System.Drawing.Point(94, 30);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(123, 24);
            this.lupNgaytu.TabIndex = 58;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(260, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 65;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(370, 234);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 27);
            this.btnThoat.TabIndex = 63;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lupngayden
            // 
            this.lupngayden.EditValue = null;
            this.lupngayden.EnterMoveNextControl = true;
            this.lupngayden.Location = new System.Drawing.Point(334, 30);
            this.lupngayden.Name = "lupngayden";
            this.lupngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden.Properties.Appearance.Options.UseFont = true;
            this.lupngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Size = new System.Drawing.Size(123, 24);
            this.lupngayden.TabIndex = 59;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(279, 234);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(91, 27);
            this.btnOK.TabIndex = 62;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "In BC";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(20, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // cklKhoaPhong
            // 
            this.cklKhoaPhong.CheckOnClick = true;
            this.cklKhoaPhong.Cursor = System.Windows.Forms.Cursors.Default;
            this.cklKhoaPhong.DisplayMember = "TenKP";
            this.cklKhoaPhong.Location = new System.Drawing.Point(94, 60);
            this.cklKhoaPhong.Name = "cklKhoaPhong";
            this.cklKhoaPhong.Size = new System.Drawing.Size(363, 168);
            this.cklKhoaPhong.TabIndex = 67;
            this.cklKhoaPhong.ValueMember = "MaKP";
            this.cklKhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKhoaPhong_ItemCheck);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(4, 60);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 17);
            this.labelControl3.TabIndex = 66;
            this.labelControl3.Text = "Khoa|Phòng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(129, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 15);
            this.label1.TabIndex = 68;
            this.label1.Text = "* Báo cáo chỉ lấy bệnh nhân BHYT";
            // 
            // trongbh
            // 
            this.trongbh.AutoSize = true;
            this.trongbh.Checked = true;
            this.trongbh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trongbh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.trongbh.Location = new System.Drawing.Point(94, 240);
            this.trongbh.Name = "trongbh";
            this.trongbh.Size = new System.Drawing.Size(86, 21);
            this.trongbh.TabIndex = 69;
            this.trongbh.Text = "Trong BH";
            this.trongbh.UseVisualStyleBackColor = true;
            this.trongbh.CheckedChanged += new System.EventHandler(this.trongbh_CheckedChanged);
            // 
            // ngoaibh
            // 
            this.ngoaibh.AutoSize = true;
            this.ngoaibh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ngoaibh.Location = new System.Drawing.Point(186, 240);
            this.ngoaibh.Name = "ngoaibh";
            this.ngoaibh.Size = new System.Drawing.Size(87, 21);
            this.ngoaibh.TabIndex = 70;
            this.ngoaibh.Text = "Ngoài BH";
            this.ngoaibh.UseVisualStyleBackColor = true;
            this.ngoaibh.CheckedChanged += new System.EventHandler(this.ngoaibh_CheckedChanged);
            // 
            // frm_BCThuNguonDV_XHH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 283);
            this.Controls.Add(this.ngoaibh);
            this.Controls.Add(this.trongbh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cklKhoaPhong);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.lupngayden);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BCThuNguonDV_XHH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo thu nguồn DV_XHH";
            this.Load += new System.EventHandler(this.frm_BCThuNguonDV_VHH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.DateEdit lupngayden;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKhoaPhong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox trongbh;
        private System.Windows.Forms.CheckBox ngoaibh;
    }
}