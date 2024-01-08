namespace QLBV.FormThamSo
{
    partial class frm_BaoCaoVienPhiTongHop_30002
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
            this.rdLoaiNgay = new DevExpress.XtraEditors.RadioGroup();
            this.rdBHYT = new DevExpress.XtraEditors.RadioGroup();
            this.rdNoiNgoaiTru = new DevExpress.XtraEditors.RadioGroup();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckcCPDinhKem = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.rdLoaiNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBHYT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiNgoaiTru.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcCPDinhKem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rdLoaiNgay
            // 
            this.rdLoaiNgay.EditValue = 0;
            this.rdLoaiNgay.Location = new System.Drawing.Point(9, 7);
            this.rdLoaiNgay.Name = "rdLoaiNgay";
            this.rdLoaiNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdLoaiNgay.Properties.Appearance.Options.UseFont = true;
            this.rdLoaiNgay.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tìm theo ngày TT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Tìm theo ngày duyệt")});
            this.rdLoaiNgay.Size = new System.Drawing.Size(400, 23);
            this.rdLoaiNgay.TabIndex = 21;
            // 
            // rdBHYT
            // 
            this.rdBHYT.EditValue = 0;
            this.rdBHYT.Location = new System.Drawing.Point(9, 91);
            this.rdBHYT.Name = "rdBHYT";
            this.rdBHYT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBHYT.Properties.Appearance.Options.UseFont = true;
            this.rdBHYT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Dịch vụ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "KSK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tât cả")});
            this.rdBHYT.Size = new System.Drawing.Size(400, 23);
            this.rdBHYT.TabIndex = 13;
            // 
            // rdNoiNgoaiTru
            // 
            this.rdNoiNgoaiTru.EditValue = 0;
            this.rdNoiNgoaiTru.Location = new System.Drawing.Point(9, 62);
            this.rdNoiNgoaiTru.Name = "rdNoiNgoaiTru";
            this.rdNoiNgoaiTru.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNoiNgoaiTru.Properties.Appearance.Options.UseFont = true;
            this.rdNoiNgoaiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.rdNoiNgoaiTru.Size = new System.Drawing.Size(400, 23);
            this.rdNoiNgoaiTru.TabIndex = 12;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(314, 343);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(93, 23);
            this.simpleButton2.TabIndex = 17;
            this.simpleButton2.Text = "Hủy";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(212, 343);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Tạo báo cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(206, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 16);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Ngày đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(9, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 16);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "Ngày từ:";
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(278, 37);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgayden.Properties.Appearance.Options.UseFont = true;
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Size = new System.Drawing.Size(131, 22);
            this.lupNgayden.TabIndex = 11;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(72, 37);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(123, 22);
            this.lupNgaytu.TabIndex = 10;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcKhoaphong);
            this.groupControl2.Location = new System.Drawing.Point(9, 120);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(400, 218);
            this.groupControl2.TabIndex = 107;
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
            this.grcKhoaphong.Size = new System.Drawing.Size(396, 214);
            this.grcKhoaphong.TabIndex = 0;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.OptionsView.ShowViewCaption = true;
            this.grvKhoaphong.ViewCaption = "Khoa|Phòng";
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging_1);
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 40;
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên K.phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 190;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // ckcCPDinhKem
            // 
            this.ckcCPDinhKem.Location = new System.Drawing.Point(9, 344);
            this.ckcCPDinhKem.Name = "ckcCPDinhKem";
            this.ckcCPDinhKem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcCPDinhKem.Properties.Appearance.Options.UseFont = true;
            this.ckcCPDinhKem.Properties.Caption = "Hiển thị chi phí đính kèm";
            this.ckcCPDinhKem.Size = new System.Drawing.Size(197, 20);
            this.ckcCPDinhKem.TabIndex = 108;
            // 
            // frm_BaoCaoVienPhiTongHop_30002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 373);
            this.Controls.Add(this.ckcCPDinhKem);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.rdLoaiNgay);
            this.Controls.Add(this.rdBHYT);
            this.Controls.Add(this.rdNoiNgoaiTru);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupNgayden);
            this.Controls.Add(this.lupNgaytu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BaoCaoVienPhiTongHop_30002";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo viện phí tổng hợp";
            this.Load += new System.EventHandler(this.frm_BaoCaoVienPhiTongHop_30002_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rdLoaiNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBHYT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiNgoaiTru.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcCPDinhKem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup rdLoaiNgay;
        private DevExpress.XtraEditors.RadioGroup rdBHYT;
        private DevExpress.XtraEditors.RadioGroup rdNoiNgoaiTru;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit lupNgayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.CheckEdit ckcCPDinhKem;
    }
}