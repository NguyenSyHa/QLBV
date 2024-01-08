namespace QLBV.FormThamSo
{
    partial class frm_BCXN_TheoBoPhan_01071
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
            this.dttungay = new DevExpress.XtraEditors.DateEdit();
            this.dtdenngay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.rdDoiTuong = new DevExpress.XtraEditors.RadioGroup();
            this.rdNhom = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ckcTieuNhom = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.hyp_HuyChon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyp_Chon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaoBC = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcTieuNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(11, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(11, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // dttungay
            // 
            this.dttungay.EditValue = null;
            this.dttungay.Location = new System.Drawing.Point(85, 11);
            this.dttungay.Name = "dttungay";
            this.dttungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dttungay.Properties.Appearance.Options.UseFont = true;
            this.dttungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dttungay.Properties.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dttungay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dttungay.Properties.EditFormat.FormatString = "{dd/MM/yyyy HH:mm}";
            this.dttungay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dttungay.Properties.Mask.EditMask = "g";
            this.dttungay.Size = new System.Drawing.Size(203, 22);
            this.dttungay.TabIndex = 0;
            // 
            // dtdenngay
            // 
            this.dtdenngay.EditValue = null;
            this.dtdenngay.Location = new System.Drawing.Point(85, 45);
            this.dtdenngay.Name = "dtdenngay";
            this.dtdenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtdenngay.Properties.Appearance.Options.UseFont = true;
            this.dtdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtdenngay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm";
            this.dtdenngay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtdenngay.Properties.EditFormat.FormatString = "dd/MM/yyyy hh:mm";
            this.dtdenngay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtdenngay.Properties.Mask.EditMask = "g";
            this.dtdenngay.Size = new System.Drawing.Size(203, 22);
            this.dtdenngay.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(11, 80);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "ĐTượng:";
            // 
            // rdDoiTuong
            // 
            this.rdDoiTuong.Location = new System.Drawing.Point(85, 77);
            this.rdDoiTuong.Name = "rdDoiTuong";
            this.rdDoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDoiTuong.Properties.Appearance.Options.UseFont = true;
            this.rdDoiTuong.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả Hai"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dịch Vụ")});
            this.rdDoiTuong.Size = new System.Drawing.Size(203, 24);
            this.rdDoiTuong.TabIndex = 2;
            // 
            // rdNhom
            // 
            this.rdNhom.Location = new System.Drawing.Point(85, 110);
            this.rdNhom.Name = "rdNhom";
            this.rdNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNhom.Properties.Appearance.Options.UseFont = true;
            this.rdNhom.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CĐHA"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Xét Nghiệm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "T.Thuật - PT")});
            this.rdNhom.Size = new System.Drawing.Size(202, 43);
            this.rdNhom.TabIndex = 3;
            this.rdNhom.SelectedIndexChanged += new System.EventHandler(this.rdNhom_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(11, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 17);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Nhóm:";
            // 
            // ckcTieuNhom
            // 
            this.ckcTieuNhom.CheckOnClick = true;
            this.ckcTieuNhom.DisplayMember = "TenTN";
            this.ckcTieuNhom.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckcTieuNhom.Location = new System.Drawing.Point(2, 22);
            this.ckcTieuNhom.Name = "ckcTieuNhom";
            this.ckcTieuNhom.Size = new System.Drawing.Size(291, 159);
            this.ckcTieuNhom.TabIndex = 0;
            this.ckcTieuNhom.ValueMember = "IDTieuNhom";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.hyp_HuyChon);
            this.groupControl1.Controls.Add(this.hyp_Chon);
            this.groupControl1.Controls.Add(this.ckcTieuNhom);
            this.groupControl1.Location = new System.Drawing.Point(294, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(295, 212);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Tiểu Nhóm";
            // 
            // hyp_HuyChon
            // 
            this.hyp_HuyChon.EditValue = "Bỏ chọn";
            this.hyp_HuyChon.Location = new System.Drawing.Point(84, 187);
            this.hyp_HuyChon.Name = "hyp_HuyChon";
            this.hyp_HuyChon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_HuyChon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_HuyChon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_HuyChon.Properties.Appearance.Options.UseFont = true;
            this.hyp_HuyChon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_HuyChon.Size = new System.Drawing.Size(70, 20);
            this.hyp_HuyChon.TabIndex = 2;
            this.hyp_HuyChon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_HuyChon_OpenLink);
            // 
            // hyp_Chon
            // 
            this.hyp_Chon.EditValue = "Chọn tất cả";
            this.hyp_Chon.Location = new System.Drawing.Point(8, 187);
            this.hyp_Chon.Name = "hyp_Chon";
            this.hyp_Chon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_Chon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_Chon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_Chon.Properties.Appearance.Options.UseFont = true;
            this.hyp_Chon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_Chon.Size = new System.Drawing.Size(70, 20);
            this.hyp_Chon.TabIndex = 1;
            this.hyp_Chon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_Chon_OpenLink);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btnHuy.Location = new System.Drawing.Point(184, 159);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(83, 32);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnTaoBC
            // 
            this.btnTaoBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoBC.Appearance.Options.UseFont = true;
            this.btnTaoBC.Image = global::QLBV.Properties.Resources.apply_16x16;
            this.btnTaoBC.Location = new System.Drawing.Point(85, 159);
            this.btnTaoBC.Name = "btnTaoBC";
            this.btnTaoBC.Size = new System.Drawing.Size(82, 32);
            this.btnTaoBC.TabIndex = 4;
            this.btnTaoBC.Text = "Tạo BC";
            this.btnTaoBC.Click += new System.EventHandler(this.btnTaoBC_Click);
            // 
            // frm_BCXN_TheoBoPhan_01071
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 225);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnTaoBC);
            this.Controls.Add(this.rdNhom);
            this.Controls.Add(this.rdDoiTuong);
            this.Controls.Add(this.dtdenngay);
            this.Controls.Add(this.dttungay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_BCXN_TheoBoPhan_01071";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo dịch vụ CLS theo nhóm";
            this.Load += new System.EventHandler(this.frm_BCXN_TheoBoPhan_01071_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dttungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcTieuNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dttungay;
        private DevExpress.XtraEditors.DateEdit dtdenngay;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup rdDoiTuong;
        private DevExpress.XtraEditors.RadioGroup rdNhom;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckedListBoxControl ckcTieuNhom;
        private DevExpress.XtraEditors.SimpleButton btnTaoBC;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_HuyChon;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_Chon;
    }
}