namespace QLBV.FormThamSo
{
    partial class frm_BC_DoanhthuVAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_DoanhthuVAT));
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lupMaKho = new DevExpress.XtraEditors.LookUpEdit();
            this.lupKhoa = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.grcTieunhom = new DevExpress.XtraGrid.GridControl();
            this.grvTieunhom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenTN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdTN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckcDonGiaVat = new DevExpress.XtraEditors.CheckEdit();
            this.ckcHTDoanhThu = new DevExpress.XtraEditors.CheckEdit();
            this.rdFont = new DevExpress.XtraEditors.RadioGroup();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnChonFilePath = new DevExpress.XtraEditors.SimpleButton();
            this.Xuatex = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTieunhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTieunhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcDonGiaVat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcHTDoanhThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xuatex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 110;
            this.label1.Text = "Bộ phận:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(-30, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 15);
            this.label7.TabIndex = 109;
            this.label7.Text = "Kho:";
            // 
            // lupMaKho
            // 
            this.lupMaKho.Location = new System.Drawing.Point(77, 40);
            this.lupMaKho.Name = "lupMaKho";
            this.lupMaKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaKho.Properties.Appearance.Options.UseFont = true;
            this.lupMaKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupMaKho.Properties.DisplayMember = "TenKP";
            this.lupMaKho.Properties.NullText = "";
            this.lupMaKho.Properties.PopupSizeable = false;
            this.lupMaKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupMaKho.Properties.ValueMember = "MaKP";
            this.lupMaKho.Size = new System.Drawing.Size(255, 24);
            this.lupMaKho.TabIndex = 105;
            // 
            // lupKhoa
            // 
            this.lupKhoa.Location = new System.Drawing.Point(77, 70);
            this.lupKhoa.Name = "lupKhoa";
            this.lupKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoa.Properties.Appearance.Options.UseFont = true;
            this.lupKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên Xã"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã Xã", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoa.Properties.DisplayMember = "TenKP";
            this.lupKhoa.Properties.NullText = "";
            this.lupKhoa.Properties.PopupSizeable = false;
            this.lupKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoa.Properties.ValueMember = "MaKP";
            this.lupKhoa.Size = new System.Drawing.Size(255, 24);
            this.lupKhoa.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(190, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 108;
            this.label3.Text = "đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(10, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 107;
            this.label2.Text = "Từ ngày:";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(225, 12);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Size = new System.Drawing.Size(107, 22);
            this.dateDenNgay.TabIndex = 104;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(77, 12);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(108, 22);
            this.dateTuNgay.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(10, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 111;
            this.label4.Text = "Kho:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(338, 234);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(92, 35);
            this.btnHuy.TabIndex = 113;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(221, 234);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(97, 35);
            this.btnInBC.TabIndex = 112;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // grcTieunhom
            // 
            this.grcTieunhom.Location = new System.Drawing.Point(338, 12);
            this.grcTieunhom.MainView = this.grvTieunhom;
            this.grcTieunhom.Name = "grcTieunhom";
            this.grcTieunhom.Size = new System.Drawing.Size(283, 205);
            this.grcTieunhom.TabIndex = 115;
            this.grcTieunhom.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTieunhom});
            // 
            // grvTieunhom
            // 
            this.grvTieunhom.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenTN,
            this.colIdTN});
            this.grvTieunhom.GridControl = this.grcTieunhom;
            this.grvTieunhom.Name = "grvTieunhom";
            this.grvTieunhom.OptionsView.ShowGroupPanel = false;
            this.grvTieunhom.OptionsView.ShowViewCaption = true;
            this.grvTieunhom.ViewCaption = "Danh sách tiểu nhóm ";
            this.grvTieunhom.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvTieunhom_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 77;
            // 
            // colTenTN
            // 
            this.colTenTN.Caption = "Tên tiểu nhóm";
            this.colTenTN.FieldName = "tenTN";
            this.colTenTN.Name = "colTenTN";
            this.colTenTN.Visible = true;
            this.colTenTN.VisibleIndex = 1;
            this.colTenTN.Width = 446;
            // 
            // colIdTN
            // 
            this.colIdTN.Caption = "Mã tiểu nhóm";
            this.colIdTN.FieldName = "maTN";
            this.colIdTN.Name = "colIdTN";
            this.colIdTN.Visible = true;
            this.colIdTN.VisibleIndex = 2;
            this.colIdTN.Width = 173;
            // 
            // ckcDonGiaVat
            // 
            this.ckcDonGiaVat.Location = new System.Drawing.Point(13, 115);
            this.ckcDonGiaVat.Name = "ckcDonGiaVat";
            this.ckcDonGiaVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcDonGiaVat.Properties.Appearance.Options.UseFont = true;
            this.ckcDonGiaVat.Properties.Caption = "Tính đơn giá có VAT ko tính tỷ lệ chiết khấu";
            this.ckcDonGiaVat.Size = new System.Drawing.Size(314, 19);
            this.ckcDonGiaVat.TabIndex = 116;
            this.ckcDonGiaVat.Visible = false;
            // 
            // ckcHTDoanhThu
            // 
            this.ckcHTDoanhThu.Location = new System.Drawing.Point(13, 140);
            this.ckcHTDoanhThu.Name = "ckcHTDoanhThu";
            this.ckcHTDoanhThu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcHTDoanhThu.Properties.Appearance.Options.UseFont = true;
            this.ckcHTDoanhThu.Properties.Caption = "Chỉ hiển thị doanh thu";
            this.ckcHTDoanhThu.Size = new System.Drawing.Size(314, 19);
            this.ckcHTDoanhThu.TabIndex = 117;
            this.ckcHTDoanhThu.CheckedChanged += new System.EventHandler(this.ckcHTDoanhThu_CheckedChanged);
            // 
            // rdFont
            // 
            this.rdFont.EditValue = 1;
            this.rdFont.Location = new System.Drawing.Point(124, 5);
            this.rdFont.Name = "rdFont";
            this.rdFont.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdFont.Properties.Appearance.Options.UseFont = true;
            this.rdFont.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "TCVN3"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Unicode")});
            this.rdFont.Size = new System.Drawing.Size(151, 25);
            this.rdFont.TabIndex = 508;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(13, 33);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Properties.Appearance.Options.UseFont = true;
            this.txtFilePath.Properties.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(262, 24);
            this.txtFilePath.TabIndex = 507;
            // 
            // btnChonFilePath
            // 
            this.btnChonFilePath.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonFilePath.Appearance.Options.UseFont = true;
            this.btnChonFilePath.Location = new System.Drawing.Point(281, 33);
            this.btnChonFilePath.Name = "btnChonFilePath";
            this.btnChonFilePath.Size = new System.Drawing.Size(24, 24);
            this.btnChonFilePath.TabIndex = 506;
            this.btnChonFilePath.Text = "...";
            this.btnChonFilePath.ToolTip = "Chọn nơi lưu";
            this.btnChonFilePath.Click += new System.EventHandler(this.btnChonFilePath_Click);
            // 
            // Xuatex
            // 
            this.Xuatex.Location = new System.Drawing.Point(11, 6);
            this.Xuatex.Name = "Xuatex";
            this.Xuatex.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Xuatex.Properties.Appearance.Options.UseFont = true;
            this.Xuatex.Properties.Caption = "Xuất Excel:";
            this.Xuatex.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Xuatex.Size = new System.Drawing.Size(98, 21);
            this.Xuatex.TabIndex = 505;
            this.Xuatex.CheckedChanged += new System.EventHandler(this.Xuatex_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.rdFont);
            this.panelControl1.Controls.Add(this.Xuatex);
            this.panelControl1.Controls.Add(this.txtFilePath);
            this.panelControl1.Controls.Add(this.btnChonFilePath);
            this.panelControl1.Location = new System.Drawing.Point(13, 163);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(319, 65);
            this.panelControl1.TabIndex = 509;
            // 
            // frm_BC_DoanhthuVAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 275);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ckcHTDoanhThu);
            this.Controls.Add(this.ckcDonGiaVat);
            this.Controls.Add(this.grcTieunhom);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lupMaKho);
            this.Controls.Add(this.lupKhoa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.dateTuNgay);
            this.Name = "frm_BC_DoanhthuVAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu";
            this.Load += new System.EventHandler(this.frm_new_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTieunhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTieunhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcDonGiaVat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcHTDoanhThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xuatex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.LookUpEdit lupMaKho;
        private DevExpress.XtraEditors.LookUpEdit lupKhoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraGrid.GridControl grcTieunhom;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTieunhom;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTN;
        private DevExpress.XtraGrid.Columns.GridColumn colIdTN;
        private DevExpress.XtraEditors.CheckEdit ckcDonGiaVat;
        private DevExpress.XtraEditors.CheckEdit ckcHTDoanhThu;
        private DevExpress.XtraEditors.RadioGroup rdFont;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.SimpleButton btnChonFilePath;
        private DevExpress.XtraEditors.CheckEdit Xuatex;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}