namespace QLBV.FormThamSo
{
    partial class frm_THXuatDuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_THXuatDuoc));
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.lupNhaCC = new DevExpress.XtraEditors.LookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.chkChiTiet = new DevExpress.XtraEditors.CheckEdit();
            this.chkHienTong = new DevExpress.XtraEditors.CheckEdit();
            this.grcPPXuat = new DevExpress.XtraGrid.GridControl();
            this.grvPPXuat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkchon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenPPX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKieuDon = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChiTiet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienTong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcPPXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPPXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkchon)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 52;
            this.label4.Text = "Nhà cung cấp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Kho";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(216, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 53;
            this.label3.Text = "đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 50;
            this.label2.Text = "Từ ngày:";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(256, 12);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Size = new System.Drawing.Size(110, 22);
            this.dateDenNgay.TabIndex = 45;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(100, 12);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(110, 22);
            this.dateTuNgay.TabIndex = 44;
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(100, 40);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.PopupSizeable = false;
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(266, 22);
            this.lupKho.TabIndex = 46;
            // 
            // lupNhaCC
            // 
            this.lupNhaCC.Location = new System.Drawing.Point(100, 68);
            this.lupNhaCC.Name = "lupNhaCC";
            this.lupNhaCC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNhaCC.Properties.Appearance.Options.UseFont = true;
            this.lupNhaCC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhaCC.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", 200, "Tên nhà cung cấp")});
            this.lupNhaCC.Properties.DisplayMember = "TenCC";
            this.lupNhaCC.Properties.NullText = "";
            this.lupNhaCC.Properties.PopupSizeable = false;
            this.lupNhaCC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupNhaCC.Properties.ValueMember = "MaCC";
            this.lupNhaCC.Size = new System.Drawing.Size(266, 22);
            this.lupNhaCC.TabIndex = 47;
            // 
            // btnHuy
            // 
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(293, 306);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 49;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(111, 306);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(81, 23);
            this.btnInBC.TabIndex = 48;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // chkChiTiet
            // 
            this.chkChiTiet.Location = new System.Drawing.Point(98, 267);
            this.chkChiTiet.Name = "chkChiTiet";
            this.chkChiTiet.Properties.Caption = "Hiện chi tiết";
            this.chkChiTiet.Size = new System.Drawing.Size(112, 19);
            this.chkChiTiet.TabIndex = 54;
            // 
            // chkHienTong
            // 
            this.chkHienTong.Location = new System.Drawing.Point(254, 267);
            this.chkHienTong.Name = "chkHienTong";
            this.chkHienTong.Properties.Caption = "Hiện tổng xuất";
            this.chkHienTong.Size = new System.Drawing.Size(103, 19);
            this.chkHienTong.TabIndex = 55;
            // 
            // grcPPXuat
            // 
            this.grcPPXuat.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcPPXuat.Location = new System.Drawing.Point(100, 97);
            this.grcPPXuat.MainView = this.grvPPXuat;
            this.grcPPXuat.Margin = new System.Windows.Forms.Padding(4);
            this.grcPPXuat.Name = "grcPPXuat";
            this.grcPPXuat.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkchon});
            this.grcPPXuat.Size = new System.Drawing.Size(266, 163);
            this.grcPPXuat.TabIndex = 58;
            this.grcPPXuat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPPXuat});
            // 
            // grvPPXuat
            // 
            this.grvPPXuat.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvPPXuat.Appearance.ViewCaption.Options.UseFont = true;
            this.grvPPXuat.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvPPXuat.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvPPXuat.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.TenPPX,
            this.colKieuDon});
            this.grvPPXuat.GridControl = this.grcPPXuat;
            this.grvPPXuat.Name = "grvPPXuat";
            this.grvPPXuat.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvPPXuat.OptionsView.ShowGroupPanel = false;
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.chkchon;
            this.colChon.FieldName = "chon";
            this.colChon.MinWidth = 10;
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 56;
            // 
            // chkchon
            // 
            this.chkchon.AutoHeight = false;
            this.chkchon.Caption = "Check";
            this.chkchon.Name = "chkchon";
            // 
            // TenPPX
            // 
            this.TenPPX.Caption = "Tên PL Xuất";
            this.TenPPX.FieldName = "tenppxd";
            this.TenPPX.Name = "TenPPX";
            this.TenPPX.OptionsColumn.AllowEdit = false;
            this.TenPPX.Visible = true;
            this.TenPPX.VisibleIndex = 1;
            this.TenPPX.Width = 192;
            // 
            // colKieuDon
            // 
            this.colKieuDon.Caption = "Kiểu đơn";
            this.colKieuDon.FieldName = "mappxd";
            this.colKieuDon.Name = "colKieuDon";
            // 
            // frm_THXuatDuoc
            // 
            this.AcceptButton = this.btnHuy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 344);
            this.Controls.Add(this.grcPPXuat);
            this.Controls.Add(this.chkHienTong);
            this.Controls.Add(this.chkChiTiet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.dateTuNgay);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.lupNhaCC);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_THXuatDuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Báo cáo xuất dược";
            this.Load += new System.EventHandler(this.frmTsBcNXTXuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhaCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChiTiet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienTong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcPPXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPPXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkchon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private DevExpress.XtraEditors.LookUpEdit lupNhaCC;
        private DevExpress.XtraEditors.CheckEdit chkChiTiet;
        private DevExpress.XtraEditors.CheckEdit chkHienTong;
        private DevExpress.XtraGrid.GridControl grcPPXuat;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPPXuat;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkchon;
        private DevExpress.XtraGrid.Columns.GridColumn TenPPX;
        private DevExpress.XtraGrid.Columns.GridColumn colKieuDon;
    }
}