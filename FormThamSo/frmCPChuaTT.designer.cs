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
    partial class frmCPChuaTT
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
            this.lupKhoaphong = new DevExpress.XtraEditors.LookUpEdit();
            this.cboDoiTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkXuatExel = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(114, 58);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lupDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupDenNgay.Size = new System.Drawing.Size(146, 20);
            this.lupDenNgay.TabIndex = 1;
            this.lupDenNgay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lupDenNgay_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 40;
            this.label3.Text = "Đến ngày";
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(114, 32);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lupTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupTuNgay.Size = new System.Drawing.Size(146, 20);
            this.lupTuNgay.TabIndex = 0;
            this.lupTuNgay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lupTuNgay_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Từ ngày";
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(185, 191);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(18, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&In Báo cáo";
            this.btnOK.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(114, 109);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú")});
            this.radNoiTru.Size = new System.Drawing.Size(146, 23);
            this.radNoiTru.TabIndex = 2;
            // 
            // ckQuy
            // 
            this.ckQuy.Location = new System.Drawing.Point(72, 165);
            this.ckQuy.Name = "ckQuy";
            this.ckQuy.Properties.Caption = "In theo quý";
            this.ckQuy.Size = new System.Drawing.Size(148, 19);
            this.ckQuy.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(18, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 16);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Khoa Phòng:";
            // 
            // lupKhoaphong
            // 
            this.lupKhoaphong.Location = new System.Drawing.Point(114, 84);
            this.lupKhoaphong.Name = "lupKhoaphong";
            this.lupKhoaphong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoaphong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 150, "Tên KP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoaphong.Properties.DisplayMember = "TenKP";
            this.lupKhoaphong.Properties.NullText = "";
            this.lupKhoaphong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoaphong.Properties.ValueMember = "MaKP";
            this.lupKhoaphong.Size = new System.Drawing.Size(146, 20);
            this.lupKhoaphong.TabIndex = 41;
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.EditValue = "BHYT";
            this.cboDoiTuong.Location = new System.Drawing.Point(114, 138);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDoiTuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ"});
            this.cboDoiTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDoiTuong.Size = new System.Drawing.Size(146, 20);
            this.cboDoiTuong.TabIndex = 43;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(18, 139);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 16);
            this.labelControl1.TabIndex = 44;
            this.labelControl1.Text = "Đối tượng:";
            // 
            // chkXuatExel
            // 
            this.chkXuatExel.Location = new System.Drawing.Point(160, 165);
            this.chkXuatExel.Name = "chkXuatExel";
            this.chkXuatExel.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkXuatExel.Properties.Appearance.Options.UseFont = true;
            this.chkXuatExel.Properties.Caption = "Xuất Excel";
            this.chkXuatExel.Size = new System.Drawing.Size(88, 20);
            this.chkXuatExel.TabIndex = 48;
            // 
            // frmCPChuaTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 250);
            this.Controls.Add(this.chkXuatExel);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboDoiTuong);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupKhoaphong);
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
            this.Name = "frmCPChuaTT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Báo Cáo Chi Phí Chưa Thanh Toán";
            this.Load += new System.EventHandler(this.frmTsBcMau20_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNoiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaphong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatExel.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LookUpEdit lupKhoaphong;
        private DevExpress.XtraEditors.ComboBoxEdit cboDoiTuong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkXuatExel;
    }
}