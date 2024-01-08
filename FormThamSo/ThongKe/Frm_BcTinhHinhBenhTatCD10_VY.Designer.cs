namespace QLBV.FormThamSo
{
    partial class Frm_BcTinhHinhBenhTatCD10_VY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BcTinhHinhBenhTatCD10_VY));
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.chkIn = new DevExpress.XtraEditors.CheckEdit();
            this.radXP = new DevExpress.XtraEditors.RadioGroup();
            this.cklDTBN = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.check1 = new DevExpress.XtraEditors.CheckEdit();
            this.txtNgayThang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblNoitru = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblSL = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grc_BenhNhanChuaCoICD = new DevExpress.XtraGrid.GridControl();
            this.grv_BenhNhanChuaCoICD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoitru = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupCB = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.ckStatus = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radXP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklDTBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgayThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_BenhNhanChuaCoICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_BenhNhanChuaCoICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(312, 283);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(101, 27);
            this.btnHuy.TabIndex = 53;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(200, 283);
            this.btnInBC.Margin = new System.Windows.Forms.Padding(4);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(104, 27);
            this.btnInBC.TabIndex = 52;
            this.btnInBC.Text = "&In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(212, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 55;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 54;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(298, 19);
            this.lupDenNgay.Margin = new System.Windows.Forms.Padding(4);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(115, 26);
            this.lupDenNgay.TabIndex = 49;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(88, 19);
            this.lupTuNgay.Margin = new System.Windows.Forms.Padding(4);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(115, 26);
            this.lupTuNgay.TabIndex = 48;
            // 
            // chkIn
            // 
            this.chkIn.Location = new System.Drawing.Point(86, 224);
            this.chkIn.Name = "chkIn";
            this.chkIn.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkIn.Properties.Appearance.Options.UseFont = true;
            this.chkIn.Properties.Caption = "Chỉ hiển thị mã ICD được sử dụng trong kỳ";
            this.chkIn.Size = new System.Drawing.Size(325, 23);
            this.chkIn.TabIndex = 58;
            // 
            // radXP
            // 
            this.radXP.EditValue = 3;
            this.radXP.Location = new System.Drawing.Point(88, 79);
            this.radXP.Name = "radXP";
            this.radXP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radXP.Properties.Appearance.Options.UseFont = true;
            this.radXP.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Trong bệnh viện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Xã phường"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "PK Khu Vực"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Tất cả")});
            this.radXP.Size = new System.Drawing.Size(325, 47);
            this.radXP.TabIndex = 60;
            // 
            // cklDTBN
            // 
            this.cklDTBN.CheckOnClick = true;
            this.cklDTBN.DisplayMember = "DTBN1";
            this.cklDTBN.Location = new System.Drawing.Point(88, 134);
            this.cklDTBN.Name = "cklDTBN";
            this.cklDTBN.Size = new System.Drawing.Size(325, 88);
            this.cklDTBN.TabIndex = 62;
            this.cklDTBN.ValueMember = "IDDTBN";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(13, 134);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 17);
            this.labelControl4.TabIndex = 61;
            this.labelControl4.Text = "Đối tượng: ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.check1);
            this.panelControl1.Controls.Add(this.txtNgayThang);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lblNoitru);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lblSL);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lupDenNgay);
            this.panelControl1.Controls.Add(this.cklDTBN);
            this.panelControl1.Controls.Add(this.lupTuNgay);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.radXP);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.chkIn);
            this.panelControl1.Controls.Add(this.btnInBC);
            this.panelControl1.Controls.Add(this.btnHuy);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(430, 371);
            this.panelControl1.TabIndex = 63;
            // 
            // check1
            // 
            this.check1.Location = new System.Drawing.Point(85, 253);
            this.check1.Name = "check1";
            this.check1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.check1.Properties.Appearance.Options.UseFont = true;
            this.check1.Properties.Caption = "Hiển thị theo số lượt khám";
            this.check1.Size = new System.Drawing.Size(325, 23);
            this.check1.TabIndex = 69;
            // 
            // txtNgayThang
            // 
            this.txtNgayThang.Location = new System.Drawing.Point(88, 51);
            this.txtNgayThang.Name = "txtNgayThang";
            this.txtNgayThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgayThang.Properties.Appearance.Options.UseFont = true;
            this.txtNgayThang.Size = new System.Drawing.Size(322, 22);
            this.txtNgayThang.TabIndex = 68;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(13, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 17);
            this.labelControl2.TabIndex = 67;
            this.labelControl2.Text = "Ngày HT:";
            // 
            // lblNoitru
            // 
            this.lblNoitru.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNoitru.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoitru.Location = new System.Drawing.Point(410, 345);
            this.lblNoitru.Name = "lblNoitru";
            this.lblNoitru.Size = new System.Drawing.Size(7, 16);
            this.lblNoitru.TabIndex = 66;
            this.lblNoitru.Text = "0";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(347, 344);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 17);
            this.labelControl3.TabIndex = 65;
            this.labelControl3.Text = "Nội trú:";
            // 
            // lblSL
            // 
            this.lblSL.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSL.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSL.Location = new System.Drawing.Point(295, 346);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(7, 16);
            this.lblSL.TabIndex = 64;
            this.lblSL.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(230, 344);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "Tổng số:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.grc_BenhNhanChuaCoICD);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(430, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(382, 371);
            this.panelControl2.TabIndex = 64;
            // 
            // grc_BenhNhanChuaCoICD
            // 
            this.grc_BenhNhanChuaCoICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_BenhNhanChuaCoICD.Location = new System.Drawing.Point(0, 0);
            this.grc_BenhNhanChuaCoICD.MainView = this.grv_BenhNhanChuaCoICD;
            this.grc_BenhNhanChuaCoICD.Name = "grc_BenhNhanChuaCoICD";
            this.grc_BenhNhanChuaCoICD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupCB,
            this.ckStatus});
            this.grc_BenhNhanChuaCoICD.Size = new System.Drawing.Size(382, 371);
            this.grc_BenhNhanChuaCoICD.TabIndex = 1;
            this.grc_BenhNhanChuaCoICD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_BenhNhanChuaCoICD});
            // 
            // grv_BenhNhanChuaCoICD
            // 
            this.grv_BenhNhanChuaCoICD.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grv_BenhNhanChuaCoICD.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_BenhNhanChuaCoICD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBN,
            this.colMaICD,
            this.colMess,
            this.colNoitru});
            this.grv_BenhNhanChuaCoICD.GridControl = this.grc_BenhNhanChuaCoICD;
            this.grv_BenhNhanChuaCoICD.Name = "grv_BenhNhanChuaCoICD";
            this.grv_BenhNhanChuaCoICD.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grv_BenhNhanChuaCoICD.OptionsView.ColumnAutoWidth = false;
            this.grv_BenhNhanChuaCoICD.OptionsView.EnableAppearanceEvenRow = true;
            this.grv_BenhNhanChuaCoICD.OptionsView.EnableAppearanceOddRow = true;
            this.grv_BenhNhanChuaCoICD.OptionsView.ShowGroupPanel = false;
            // 
            // colMaBN
            // 
            this.colMaBN.Caption = "Mã bệnh nhân";
            this.colMaBN.FieldName = "MaBN";
            this.colMaBN.Name = "colMaBN";
            this.colMaBN.Visible = true;
            this.colMaBN.VisibleIndex = 0;
            // 
            // colMaICD
            // 
            this.colMaICD.Caption = "Mã ICD";
            this.colMaICD.FieldName = "MaICD1";
            this.colMaICD.Name = "colMaICD";
            this.colMaICD.Visible = true;
            this.colMaICD.VisibleIndex = 1;
            this.colMaICD.Width = 100;
            // 
            // colMess
            // 
            this.colMess.Caption = "Nguyên nhân";
            this.colMess.FieldName = "Mess";
            this.colMess.Name = "colMess";
            this.colMess.Visible = true;
            this.colMess.VisibleIndex = 2;
            this.colMess.Width = 250;
            // 
            // colNoitru
            // 
            this.colNoitru.Caption = "Nội ngoại trú";
            this.colNoitru.FieldName = "Noitru";
            this.colNoitru.Name = "colNoitru";
            this.colNoitru.Visible = true;
            this.colNoitru.VisibleIndex = 3;
            // 
            // lupCB
            // 
            this.lupCB.AutoHeight = false;
            this.lupCB.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCB.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TENCB", "Chương bệnh")});
            this.lupCB.DisplayMember = "TENCB";
            this.lupCB.Name = "lupCB";
            this.lupCB.NullText = "";
            this.lupCB.ValueMember = "IDCB";
            // 
            // ckStatus
            // 
            this.ckStatus.AutoHeight = false;
            this.ckStatus.Caption = "Check";
            this.ckStatus.Name = "ckStatus";
            // 
            // Frm_BcTinhHinhBenhTatCD10_VY
            // 
            this.AcceptButton = this.btnInBC;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 371);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BcTinhHinhBenhTatCD10_VY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Tình hình bệnh tật, tử vong theo ICD10";
            this.Load += new System.EventHandler(this.Frm_BcTinhHinhBenhTatCD10_VY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radXP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklDTBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgayThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_BenhNhanChuaCoICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_BenhNhanChuaCoICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.CheckEdit chkIn;
        private DevExpress.XtraEditors.RadioGroup radXP;
        private DevExpress.XtraEditors.CheckedListBoxControl cklDTBN;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grc_BenhNhanChuaCoICD;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_BenhNhanChuaCoICD;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaICD;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupCB;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colMess;
        private DevExpress.XtraEditors.LabelControl lblSL;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colNoitru;
        private DevExpress.XtraEditors.LabelControl lblNoitru;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNgayThang;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit check1;
    }
}