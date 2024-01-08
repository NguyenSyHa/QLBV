namespace QLBV.FormThamSo
{
    partial class frm_CTTonThucSDNoiTru
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.grcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckGrvKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colmaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.bdSourceKP = new System.Windows.Forms.BindingSource(this.components);
            this.lookUpKhoXuat = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chk2 = new DevExpress.XtraEditors.CheckEdit();
            this.chk1 = new DevExpress.XtraEditors.CheckEdit();
            this.chk0 = new DevExpress.XtraEditors.CheckEdit();
            this.lupNhomDV = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.rdChiTiet = new DevExpress.XtraEditors.RadioGroup();
            this.rdNoiTru = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdSourceKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKhoXuat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdChiTiet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiTru.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày : ";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(68, 12);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupTuNgay.Size = new System.Drawing.Size(170, 20);
            this.lupTuNgay.TabIndex = 1;
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(68, 32);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupDenNgay.Size = new System.Drawing.Size(170, 20);
            this.lupDenNgay.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Kho xuất :";
            // 
            // grcKhoaPhong
            // 
            this.grcKhoaPhong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grcKhoaPhong.Location = new System.Drawing.Point(244, 12);
            this.grcKhoaPhong.MainView = this.grvKhoaPhong;
            this.grcKhoaPhong.Name = "grcKhoaPhong";
            this.grcKhoaPhong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaPhong.Size = new System.Drawing.Size(400, 244);
            this.grcKhoaPhong.TabIndex = 12;
            this.grcKhoaPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong});
            // 
            // grvKhoaPhong
            // 
            this.grvKhoaPhong.Appearance.ViewCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvKhoaPhong.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grvKhoaPhong.Appearance.ViewCaption.Options.UseFont = true;
            this.grvKhoaPhong.Appearance.ViewCaption.Options.UseForeColor = true;
            this.grvKhoaPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckGrvKP,
            this.colmaKP,
            this.colTenKP});
            this.grvKhoaPhong.GridControl = this.grcKhoaPhong;
            this.grvKhoaPhong.Name = "grvKhoaPhong";
            this.grvKhoaPhong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaPhong.OptionsView.ShowViewCaption = true;
            this.grvKhoaPhong.ViewCaption = "Chọn Khoa Phòng";
            this.grvKhoaPhong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaPhong_CellValueChanging);
            // 
            // colCheckGrvKP
            // 
            this.colCheckGrvKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCheckGrvKP.AppearanceHeader.Options.UseFont = true;
            this.colCheckGrvKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colCheckGrvKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckGrvKP.Caption = "Chọn";
            this.colCheckGrvKP.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCheckGrvKP.FieldName = "Check";
            this.colCheckGrvKP.Name = "colCheckGrvKP";
            this.colCheckGrvKP.Visible = true;
            this.colCheckGrvKP.VisibleIndex = 0;
            this.colCheckGrvKP.Width = 48;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colmaKP
            // 
            this.colmaKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colmaKP.AppearanceHeader.Options.UseFont = true;
            this.colmaKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colmaKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colmaKP.Caption = "Mã KP";
            this.colmaKP.FieldName = "MaKP";
            this.colmaKP.Name = "colmaKP";
            this.colmaKP.Width = 51;
            // 
            // colTenKP
            // 
            this.colTenKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenKP.AppearanceHeader.Options.UseFont = true;
            this.colTenKP.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenKP.Caption = "Tên Khoa Phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.OptionsColumn.ReadOnly = true;
            this.colTenKP.OptionsFilter.AllowFilter = false;
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            this.colTenKP.Width = 334;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(432, 262);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 23);
            this.btnIn.TabIndex = 13;
            this.btnIn.Text = "In Báo Cáo";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(548, 262);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lookUpKhoXuat
            // 
            this.lookUpKhoXuat.Location = new System.Drawing.Point(68, 52);
            this.lookUpKhoXuat.Name = "lookUpKhoXuat";
            this.lookUpKhoXuat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpKhoXuat.Properties.NullText = "Chọn kho xuất";
            this.lookUpKhoXuat.Size = new System.Drawing.Size(170, 20);
            this.lookUpKhoXuat.TabIndex = 5;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.chk2);
            this.groupControl1.Controls.Add(this.chk1);
            this.groupControl1.Controls.Add(this.chk0);
            this.groupControl1.Location = new System.Drawing.Point(12, 96);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(226, 77);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Danh Mục trong BHYT";
            // 
            // chk2
            // 
            this.chk2.Location = new System.Drawing.Point(5, 53);
            this.chk2.Name = "chk2";
            this.chk2.Properties.Caption = "2 : Chi phí Vật tư y tế(kèm theo dịch vụ)";
            this.chk2.Size = new System.Drawing.Size(216, 19);
            this.chk2.TabIndex = 2;
            // 
            // chk1
            // 
            this.chk1.EditValue = true;
            this.chk1.Location = new System.Drawing.Point(5, 34);
            this.chk1.Name = "chk1";
            this.chk1.Properties.Caption = "1 : Chi Phí trong danh mục BHYT";
            this.chk1.Size = new System.Drawing.Size(200, 19);
            this.chk1.TabIndex = 1;
            // 
            // chk0
            // 
            this.chk0.Location = new System.Drawing.Point(5, 15);
            this.chk0.Name = "chk0";
            this.chk0.Properties.Caption = "0 : Chi Phí ngoài danh mục BHYT";
            this.chk0.Size = new System.Drawing.Size(200, 19);
            this.chk0.TabIndex = 0;
            // 
            // lupNhomDV
            // 
            this.lupNhomDV.Location = new System.Drawing.Point(68, 72);
            this.lupNhomDV.Name = "lupNhomDV";
            this.lupNhomDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNhomDV.Properties.NullText = "Chọn nhóm dịch vụ";
            this.lupNhomDV.Size = new System.Drawing.Size(170, 20);
            this.lupNhomDV.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Nhóm DV :";
            // 
            // rdChiTiet
            // 
            this.rdChiTiet.EditValue = "rdThucSD";
            this.rdChiTiet.Location = new System.Drawing.Point(12, 179);
            this.rdChiTiet.Name = "rdChiTiet";
            this.rdChiTiet.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdThucSD", "Thực sử dụng(đã thanh toán)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdTon", "Tồn thực sử dụng(chưa thanh toán)")});
            this.rdChiTiet.Size = new System.Drawing.Size(226, 43);
            this.rdChiTiet.TabIndex = 10;
            // 
            // rdNoiTru
            // 
            this.rdNoiTru.EditValue = "rdNoiTru";
            this.rdNoiTru.Location = new System.Drawing.Point(12, 228);
            this.rdNoiTru.Name = "rdNoiTru";
            this.rdNoiTru.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdNoiTru", "Nội Trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rddieutriNT", "Điều trị ngoại trú")});
            this.rdNoiTru.Size = new System.Drawing.Size(226, 28);
            this.rdNoiTru.TabIndex = 11;
            // 
            // frm_CTTonThucSDNoiTru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 292);
            this.Controls.Add(this.rdNoiTru);
            this.Controls.Add(this.rdChiTiet);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lupNhomDV);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lookUpKhoXuat);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.grcKhoaPhong);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CTTonThucSDNoiTru";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết tồn và thực sử dụng tại các khoa";
            this.Load += new System.EventHandler(this.frm_CTTonThucSD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdSourceKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKhoXuat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNhomDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdChiTiet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdNoiTru.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl grcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckGrvKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn colmaKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private System.Windows.Forms.BindingSource bdSourceKP;
        private DevExpress.XtraEditors.LookUpEdit lookUpKhoXuat;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chk2;
        private DevExpress.XtraEditors.CheckEdit chk1;
        private DevExpress.XtraEditors.CheckEdit chk0;
        private DevExpress.XtraEditors.LookUpEdit lupNhomDV;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup rdChiTiet;
        private DevExpress.XtraEditors.RadioGroup rdNoiTru;
    }
}