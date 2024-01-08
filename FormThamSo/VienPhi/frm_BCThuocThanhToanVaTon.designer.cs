namespace QLBV.FormThamSo
{
    partial class frm_BCThuocThanhToanVaTon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCThuocThanhToanVaTon));
            this.dtHienTai = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDoituong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.radio_DTNT = new DevExpress.XtraEditors.RadioGroup();
            this.rdTrongNgoaiDM = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.grcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckGrvKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colmaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtHienTai.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHienTai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_DTNT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongNgoaiDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtHienTai
            // 
            this.dtHienTai.EditValue = null;
            this.dtHienTai.Location = new System.Drawing.Point(89, 3);
            this.dtHienTai.Margin = new System.Windows.Forms.Padding(4);
            this.dtHienTai.Name = "dtHienTai";
            this.dtHienTai.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtHienTai.Properties.Appearance.Options.UseFont = true;
            this.dtHienTai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtHienTai.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtHienTai.Size = new System.Drawing.Size(184, 26);
            this.dtHienTai.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 64;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDoituong
            // 
            this.lupDoituong.EnterMoveNextControl = true;
            this.lupDoituong.Location = new System.Drawing.Point(89, 74);
            this.lupDoituong.Name = "lupDoituong";
            this.lupDoituong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupDoituong.Properties.Appearance.Options.UseFont = true;
            this.lupDoituong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDoituong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng")});
            this.lupDoituong.Properties.DisplayMember = "DTBN1";
            this.lupDoituong.Properties.NullText = "Tất cả";
            this.lupDoituong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupDoituong.Properties.ValueMember = "IDDTBN";
            this.lupDoituong.Size = new System.Drawing.Size(184, 26);
            this.lupDoituong.TabIndex = 65;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(9, 75);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 17);
            this.labelControl5.TabIndex = 66;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // radio_DTNT
            // 
            this.radio_DTNT.Location = new System.Drawing.Point(89, 106);
            this.radio_DTNT.Name = "radio_DTNT";
            this.radio_DTNT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radio_DTNT.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.radio_DTNT.Properties.Appearance.Options.UseFont = true;
            this.radio_DTNT.Properties.Appearance.Options.UseForeColor = true;
            this.radio_DTNT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ĐT Ngoại.T"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radio_DTNT.Size = new System.Drawing.Size(184, 82);
            this.radio_DTNT.TabIndex = 506;
            // 
            // rdTrongNgoaiDM
            // 
            this.rdTrongNgoaiDM.Location = new System.Drawing.Point(89, 191);
            this.rdTrongNgoaiDM.Name = "rdTrongNgoaiDM";
            this.rdTrongNgoaiDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdTrongNgoaiDM.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.rdTrongNgoaiDM.Properties.Appearance.Options.UseFont = true;
            this.rdTrongNgoaiDM.Properties.Appearance.Options.UseForeColor = true;
            this.rdTrongNgoaiDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CP. Không thanh toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CP.Thanh toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.rdTrongNgoaiDM.Size = new System.Drawing.Size(184, 83);
            this.rdTrongNgoaiDM.TabIndex = 507;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 19);
            this.label1.TabIndex = 511;
            this.label1.Text = "Đến ngày";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(89, 38);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(4);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Size = new System.Drawing.Size(184, 26);
            this.dtDenNgay.TabIndex = 510;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(482, 279);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(92, 25);
            this.btnThoat.TabIndex = 509;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(369, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 25);
            this.btnOK.TabIndex = 508;
            this.btnOK.Text = "Tạo báo &cáo";
            this.btnOK.ToolTipTitle = "jhgjgjhj";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grcKhoaPhong
            // 
            this.grcKhoaPhong.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grcKhoaPhong.Location = new System.Drawing.Point(289, 2);
            this.grcKhoaPhong.MainView = this.grvKhoaPhong;
            this.grcKhoaPhong.Name = "grcKhoaPhong";
            this.grcKhoaPhong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaPhong.Size = new System.Drawing.Size(296, 271);
            this.grcKhoaPhong.TabIndex = 512;
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
            this.grvKhoaPhong.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaPhong_CellValueChanged);
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
            // frm_BCThuocThanhToanVaTon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 308);
            this.Controls.Add(this.grcKhoaPhong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdTrongNgoaiDM);
            this.Controls.Add(this.radio_DTNT);
            this.Controls.Add(this.lupDoituong);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtHienTai);
            this.Name = "frm_BCThuocThanhToanVaTon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo thuốc thanh toán và tồn bệnh án";
            this.Load += new System.EventHandler(this.frm_BCThuocThanhToanVaTon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtHienTai.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHienTai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDoituong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_DTNT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongNgoaiDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtHienTai;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LookUpEdit lupDoituong;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.RadioGroup radio_DTNT;
        private DevExpress.XtraEditors.RadioGroup rdTrongNgoaiDM;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraGrid.GridControl grcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckGrvKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colmaKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
    }
}