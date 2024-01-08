namespace QLBV.FormNhap
{
    partial class frm_TTTruyenDich
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupCanBo = new DevExpress.XtraEditors.LookUpEdit();
            this.lupTocDo = new DevExpress.XtraEditors.TextEdit();
            this.deBatDau = new DevExpress.XtraEditors.DateEdit();
            this.deKetThuc = new DevExpress.XtraEditors.DateEdit();
            this.mmDienBien = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnxoa = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtSLTruyen = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.cboDonVi = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCanBo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTocDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBatDau.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBatDau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deKetThuc.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deKetThuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmDienBien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSLTruyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(7, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "C.Bộ T.Hiện:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(252, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Tốc độ truyền:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(7, 72);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 19);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Bắt đầu từ:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(256, 72);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 19);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Kết thúc đến:";
            // 
            // lupCanBo
            // 
            this.lupCanBo.Location = new System.Drawing.Point(98, 35);
            this.lupCanBo.Name = "lupCanBo";
            this.lupCanBo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupCanBo.Properties.Appearance.Options.UseFont = true;
            this.lupCanBo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCanBo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", 10, "Mã CB"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 50, "Tên Cán Bộ")});
            this.lupCanBo.Properties.DisplayMember = "TenCB";
            this.lupCanBo.Properties.NullText = "";
            this.lupCanBo.Properties.ValueMember = "MaCB";
            this.lupCanBo.Size = new System.Drawing.Size(148, 26);
            this.lupCanBo.TabIndex = 1;
            // 
            // lupTocDo
            // 
            this.lupTocDo.Location = new System.Drawing.Point(355, 35);
            this.lupTocDo.Name = "lupTocDo";
            this.lupTocDo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTocDo.Properties.Appearance.Options.UseFont = true;
            this.lupTocDo.Properties.Mask.EditMask = "n0";
            this.lupTocDo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.lupTocDo.Size = new System.Drawing.Size(77, 26);
            this.lupTocDo.TabIndex = 2;
            // 
            // deBatDau
            // 
            this.deBatDau.EditValue = null;
            this.deBatDau.Location = new System.Drawing.Point(98, 69);
            this.deBatDau.Name = "deBatDau";
            this.deBatDau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deBatDau.Properties.Appearance.Options.UseFont = true;
            this.deBatDau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBatDau.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBatDau.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deBatDau.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deBatDau.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deBatDau.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deBatDau.Properties.Mask.EditMask = "g";
            this.deBatDau.Size = new System.Drawing.Size(148, 26);
            this.deBatDau.TabIndex = 3;
            // 
            // deKetThuc
            // 
            this.deKetThuc.EditValue = null;
            this.deKetThuc.Location = new System.Drawing.Point(355, 69);
            this.deKetThuc.Name = "deKetThuc";
            this.deKetThuc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deKetThuc.Properties.Appearance.Options.UseFont = true;
            this.deKetThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deKetThuc.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deKetThuc.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deKetThuc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deKetThuc.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.deKetThuc.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.deKetThuc.Properties.Mask.EditMask = "g";
            this.deKetThuc.Size = new System.Drawing.Size(167, 26);
            this.deKetThuc.TabIndex = 3;
            // 
            // mmDienBien
            // 
            this.mmDienBien.Location = new System.Drawing.Point(98, 135);
            this.mmDienBien.Name = "mmDienBien";
            this.mmDienBien.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmDienBien.Properties.Appearance.Options.UseFont = true;
            this.mmDienBien.Size = new System.Drawing.Size(424, 138);
            this.mmDienBien.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(7, 136);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(86, 57);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "D.Biến trong\r\nquá trình \r\ntruyền dịch:";
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Location = new System.Drawing.Point(388, 278);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(7, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(528, 27);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "fdf";
            // 
            // btnxoa
            // 
            this.btnxoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Appearance.Options.UseFont = true;
            this.btnxoa.Location = new System.Drawing.Point(307, 278);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(75, 23);
            this.btnxoa.TabIndex = 7;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(434, 39);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(91, 16);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "(Đơn vị: giọt/ph)";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(7, 104);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(70, 19);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "SL truyền:";
            // 
            // txtSLTruyen
            // 
            this.txtSLTruyen.Location = new System.Drawing.Point(98, 101);
            this.txtSLTruyen.Name = "txtSLTruyen";
            this.txtSLTruyen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSLTruyen.Properties.Appearance.Options.UseFont = true;
            this.txtSLTruyen.Properties.Mask.EditMask = "n2";
            this.txtSLTruyen.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSLTruyen.Size = new System.Drawing.Size(148, 26);
            this.txtSLTruyen.TabIndex = 8;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Location = new System.Drawing.Point(256, 104);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(50, 19);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Đơn vị:";
            // 
            // cboDonVi
            // 
            this.cboDonVi.Location = new System.Drawing.Point(355, 101);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonVi.Properties.Appearance.Options.UseFont = true;
            this.cboDonVi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDonVi.Properties.Items.AddRange(new object[] {
            "mml",
            "l"});
            this.cboDonVi.Size = new System.Drawing.Size(167, 26);
            this.cboDonVi.TabIndex = 9;
            // 
            // frm_TTTruyenDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 308);
            this.Controls.Add(this.cboDonVi);
            this.Controls.Add(this.txtSLTruyen);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.mmDienBien);
            this.Controls.Add(this.deKetThuc);
            this.Controls.Add(this.deBatDau);
            this.Controls.Add(this.lupTocDo);
            this.Controls.Add(this.lupCanBo);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_TTTruyenDich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin truyền dịch";
            this.Load += new System.EventHandler(this.frm_TTTruyenDich_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupCanBo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTocDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBatDau.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBatDau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deKetThuc.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deKetThuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmDienBien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSLTruyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lupCanBo;
        private DevExpress.XtraEditors.TextEdit lupTocDo;
        private DevExpress.XtraEditors.DateEdit deBatDau;
        private DevExpress.XtraEditors.DateEdit deKetThuc;
        private DevExpress.XtraEditors.MemoEdit mmDienBien;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnxoa;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtSLTruyen;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ComboBoxEdit cboDonVi;
    }
}