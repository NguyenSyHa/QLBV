namespace QLBV.FormThamSo
{
    partial class frm_BC_CTNhapThuoc_VTYT_30009
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_CTNhapThuoc_VTYT_30009));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.date_TuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.date_DenNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lupNhomDV = new DevExpress.XtraEditors.LookUpEdit();
            this.lupTieuNhom = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(80, 102);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(282, 196);
            this.groupControl1.TabIndex = 134;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcKhoaphong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcKhoaphong.Location = new System.Drawing.Point(2, 2);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.Size = new System.Drawing.Size(278, 192);
            this.grcKhoaphong.TabIndex = 0;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenKP,
            this.colMaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.ViewCaption = "Kho dược";
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colChon.AppearanceHeader.Options.UseFont = true;
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Chon";
            this.colChon.MinWidth = 10;
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 57;
            // 
            // colTenKP
            // 
            this.colTenKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenKP.AppearanceHeader.Options.UseFont = true;
            this.colTenKP.Caption = "Tên Kho";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.OptionsColumn.AllowEdit = false;
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            this.colTenKP.Width = 203;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "MaKP";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(0, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 125;
            this.label1.Text = "Kho";
            // 
            // date_TuNgay
            // 
            this.date_TuNgay.EditValue = null;
            this.date_TuNgay.Location = new System.Drawing.Point(80, 12);
            this.date_TuNgay.Name = "date_TuNgay";
            this.date_TuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.date_TuNgay.Properties.Appearance.Options.UseFont = true;
            this.date_TuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_TuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_TuNgay.Size = new System.Drawing.Size(100, 24);
            this.date_TuNgay.TabIndex = 128;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(3, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 17);
            this.labelControl1.TabIndex = 126;
            this.labelControl1.Text = "Từ ngày";
            // 
            // date_DenNgay
            // 
            this.date_DenNgay.EditValue = null;
            this.date_DenNgay.Location = new System.Drawing.Point(262, 12);
            this.date_DenNgay.Name = "date_DenNgay";
            this.date_DenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.date_DenNgay.Properties.Appearance.Options.UseFont = true;
            this.date_DenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_DenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_DenNgay.Size = new System.Drawing.Size(100, 24);
            this.date_DenNgay.TabIndex = 129;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(186, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 17);
            this.labelControl3.TabIndex = 127;
            this.labelControl3.Text = "Đến ngày";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(284, 334);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(78, 28);
            this.btnThoat.TabIndex = 136;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(174, 334);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 28);
            this.btnOK.TabIndex = 135;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "In báo &cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lupNhomDV
            // 
            this.lupNhomDV.Location = new System.Drawing.Point(80, 42);
            this.lupNhomDV.Name = "lupNhomDV";
            this.lupNhomDV.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNhomDV.Properties.Appearance.Options.UseFont = true;
            this.lupNhomDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhomDV.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", "Tên Nhóm")});
            this.lupNhomDV.Properties.DisplayMember = "TenNhom";
            this.lupNhomDV.Properties.NullText = "";
            this.lupNhomDV.Properties.ValueMember = "IDNhom";
            this.lupNhomDV.Size = new System.Drawing.Size(282, 24);
            this.lupNhomDV.TabIndex = 137;
            this.lupNhomDV.EditValueChanged += new System.EventHandler(this.lupNhomDV_EditValueChanged);
            // 
            // lupTieuNhom
            // 
            this.lupTieuNhom.Location = new System.Drawing.Point(80, 72);
            this.lupTieuNhom.Name = "lupTieuNhom";
            this.lupTieuNhom.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTieuNhom.Properties.Appearance.Options.UseFont = true;
            this.lupTieuNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTieuNhom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên")});
            this.lupTieuNhom.Properties.DisplayMember = "TenTN";
            this.lupTieuNhom.Properties.NullText = "";
            this.lupTieuNhom.Properties.ValueMember = "IdTieuNhom";
            this.lupTieuNhom.Size = new System.Drawing.Size(282, 24);
            this.lupTieuNhom.TabIndex = 137;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(3, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 17);
            this.labelControl2.TabIndex = 126;
            this.labelControl2.Text = "Nhóm DV";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(3, 75);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(61, 17);
            this.labelControl4.TabIndex = 126;
            this.labelControl4.Text = "Tiểu nhóm";
            // 
            // frm_BC_CTNhapThuoc_VTYT_30009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 374);
            this.Controls.Add(this.lupTieuNhom);
            this.Controls.Add(this.lupNhomDV);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.date_TuNgay);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.date_DenNgay);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_CTNhapThuoc_VTYT_30009";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết nhập thuốc - VTYT";
            this.Load += new System.EventHandler(this.frm_BC_CTNhapThuoc_VTYT_30009_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTieuNhom.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit date_TuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit date_DenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LookUpEdit lupNhomDV;
        private DevExpress.XtraEditors.LookUpEdit lupTieuNhom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}