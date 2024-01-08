using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    partial class frmTsBcMau21BHYT
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
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.radNoiTru = new DevExpress.XtraEditors.RadioGroup();
            this.ckQuy = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.cboDoiTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkXuatExel = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupKhoaphong2 = new DevExpress.XtraEditors.LookUpEdit();
            this.radTimKiem = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(315, 27);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lupDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupDenNgay.Size = new System.Drawing.Size(119, 24);
            this.lupDenNgay.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(233, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 40;
            this.label3.Text = "đến:";
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(108, 27);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lupTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupTuNgay.Size = new System.Drawing.Size(119, 24);
            this.lupTuNgay.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "Từ ngày:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Location = new System.Drawing.Point(347, 300);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(87, 27);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(108, 300);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 27);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "&In Báo cáo";
            this.btnOK.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(108, 153);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNoiTru.Properties.Appearance.Options.UseFont = true;
            this.radNoiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú")});
            this.radNoiTru.Size = new System.Drawing.Size(326, 27);
            this.radNoiTru.TabIndex = 4;
            // 
            // ckQuy
            // 
            this.ckQuy.Location = new System.Drawing.Point(106, 252);
            this.ckQuy.Name = "ckQuy";
            this.ckQuy.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ckQuy.Properties.Appearance.Options.UseFont = true;
            this.ckQuy.Properties.Caption = "In theo quý";
            this.ckQuy.Size = new System.Drawing.Size(173, 22);
            this.ckQuy.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(17, 124);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 17);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Nhóm chi phí:";
            // 
            // lupNhom
            // 
            this.lupNhom.Location = new System.Drawing.Point(108, 121);
            this.lupNhom.Name = "lupNhom";
            this.lupNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNhom.Properties.Appearance.Options.UseFont = true;
            this.lupNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", 150, "Tên nhóm CP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDNhom", "MaKP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupNhom.Properties.DisplayMember = "TenNhom";
            this.lupNhom.Properties.NullText = "";
            this.lupNhom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNhom.Properties.ValueMember = "IDNhom";
            this.lupNhom.Size = new System.Drawing.Size(326, 24);
            this.lupNhom.TabIndex = 3;
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.EditValue = "BHYT";
            this.cboDoiTuong.Location = new System.Drawing.Point(108, 188);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDoiTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDoiTuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ"});
            this.cboDoiTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDoiTuong.Size = new System.Drawing.Size(326, 24);
            this.cboDoiTuong.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(17, 191);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 17);
            this.labelControl1.TabIndex = 44;
            this.labelControl1.Text = "Đối tượng:";
            // 
            // chkXuatExel
            // 
            this.chkXuatExel.Location = new System.Drawing.Point(313, 250);
            this.chkXuatExel.Name = "chkXuatExel";
            this.chkXuatExel.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkXuatExel.Properties.Appearance.Options.UseFont = true;
            this.chkXuatExel.Properties.Caption = "Xuất Excel";
            this.chkXuatExel.Size = new System.Drawing.Size(103, 22);
            this.chkXuatExel.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(17, 157);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 17);
            this.labelControl2.TabIndex = 49;
            this.labelControl2.Text = "Nội|Ngoại trú:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(17, 223);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 17);
            this.labelControl4.TabIndex = 50;
            this.labelControl4.Text = "Khoa phòng:";
            // 
            // lupKhoaphong2
            // 
            this.lupKhoaphong2.Location = new System.Drawing.Point(108, 220);
            this.lupKhoaphong2.Name = "lupKhoaphong2";
            this.lupKhoaphong2.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoaphong2.Properties.Appearance.Options.UseFont = true;
            this.lupKhoaphong2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoaphong2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã ", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoaphong2.Properties.DisplayMember = "TenKP";
            this.lupKhoaphong2.Properties.NullText = "";
            this.lupKhoaphong2.Properties.ValueMember = "MaKP";
            this.lupKhoaphong2.Size = new System.Drawing.Size(326, 24);
            this.lupKhoaphong2.TabIndex = 6;
            // 
            // radTimKiem
            // 
            this.radTimKiem.EditValue = 0;
            this.radTimKiem.Location = new System.Drawing.Point(108, 59);
            this.radTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.radTimKiem.Name = "radTimKiem";
            this.radTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radTimKiem.Properties.Appearance.Options.UseFont = true;
            this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tìm kiếm theo ngày RV"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Tìm kiếm theo ngày T.Toán")});
            this.radTimKiem.Properties.SelectedIndexChanged += new System.EventHandler(this.rdFont_Properties_SelectedIndexChanged);
            this.radTimKiem.Size = new System.Drawing.Size(326, 54);
            this.radTimKiem.TabIndex = 2;
            // 
            // frmTsBcMau21BHYT
            // 
            this.AcceptButton = this.btnOK;
            this.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 354);
            this.Controls.Add(this.radTimKiem);
            this.Controls.Add(this.lupKhoaphong2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.chkXuatExel);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboDoiTuong);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupNhom);
            this.Controls.Add(this.ckQuy);
            this.Controls.Add(this.radNoiTru);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTsBcMau21BHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo mẫu 21/BHYT";
            this.Load += new System.EventHandler(this.frmTsBcMau20_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTimKiem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.RadioGroup radNoiTru;
        private DevExpress.XtraEditors.CheckEdit ckQuy;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupNhom;
        private DevExpress.XtraEditors.ComboBoxEdit cboDoiTuong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkXuatExel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupKhoaphong2;
        private DevExpress.XtraEditors.RadioGroup radTimKiem;
    }
}