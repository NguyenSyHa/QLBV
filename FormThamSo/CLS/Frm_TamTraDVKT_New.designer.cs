namespace QLBV.FormThamSo
{
    partial class Frm_TamTraDVKT_New
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
            this.LupNgayTu = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayDen = new DevExpress.XtraEditors.DateEdit();
            this.LupKhoaPhong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Taoso = new DevExpress.XtraEditors.SimpleButton();
            this.Huy = new DevExpress.XtraEditors.SimpleButton();
            this.radNT = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.grcBuong = new DevExpress.XtraGrid.GridControl();
            this.grvBuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenBuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupKhoaPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcBuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LupNgayTu
            // 
            this.LupNgayTu.EditValue = null;
            this.LupNgayTu.Location = new System.Drawing.Point(114, 12);
            this.LupNgayTu.Name = "LupNgayTu";
            this.LupNgayTu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayTu.Properties.Appearance.Options.UseFont = true;
            this.LupNgayTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayTu.Size = new System.Drawing.Size(193, 24);
            this.LupNgayTu.TabIndex = 0;
            this.LupNgayTu.EditValueChanged += new System.EventHandler(this.LupNgayTu_EditValueChanged);
            // 
            // LupNgayDen
            // 
            this.LupNgayDen.EditValue = null;
            this.LupNgayDen.Location = new System.Drawing.Point(114, 52);
            this.LupNgayDen.Name = "LupNgayDen";
            this.LupNgayDen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupNgayDen.Properties.Appearance.Options.UseFont = true;
            this.LupNgayDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayDen.Size = new System.Drawing.Size(193, 24);
            this.LupNgayDen.TabIndex = 1;
            // 
            // LupKhoaPhong
            // 
            this.LupKhoaPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.LupKhoaPhong.Location = new System.Drawing.Point(2, 2);
            this.LupKhoaPhong.Name = "LupKhoaPhong";
            this.LupKhoaPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LupKhoaPhong.Properties.Appearance.Options.UseFont = true;
            this.LupKhoaPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupKhoaPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "MaKP", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.LupKhoaPhong.Properties.DisplayMember = "TenKP";
            this.LupKhoaPhong.Properties.NullText = "Chọn Khoa|Phòng";
            this.LupKhoaPhong.Properties.ValueMember = "MaKP";
            this.LupKhoaPhong.Size = new System.Drawing.Size(305, 24);
            this.LupKhoaPhong.TabIndex = 2;
            this.LupKhoaPhong.EditValueChanged += new System.EventHandler(this.LupKhoaPhong_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(12, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Đến ngày:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // Taoso
            // 
            this.Taoso.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Taoso.Appearance.Options.UseFont = true;
            this.Taoso.Location = new System.Drawing.Point(114, 178);
            this.Taoso.Name = "Taoso";
            this.Taoso.Size = new System.Drawing.Size(100, 26);
            this.Taoso.TabIndex = 6;
            this.Taoso.Text = "&Tạo sổ";
            this.Taoso.Click += new System.EventHandler(this.Taoso_Click);
            // 
            // Huy
            // 
            this.Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Huy.Appearance.Options.UseFont = true;
            this.Huy.Location = new System.Drawing.Point(220, 178);
            this.Huy.Name = "Huy";
            this.Huy.Size = new System.Drawing.Size(87, 26);
            this.Huy.TabIndex = 7;
            this.Huy.Text = "&Hủy";
            this.Huy.Click += new System.EventHandler(this.Huy_Click);
            // 
            // radNT
            // 
            this.radNT.Location = new System.Drawing.Point(114, 82);
            this.radNT.Name = "radNT";
            this.radNT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radNT.Properties.Appearance.Options.UseFont = true;
            this.radNT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ĐT ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radNT.Size = new System.Drawing.Size(193, 90);
            this.radNT.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(12, 121);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(92, 17);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Nội|Ngoại trú:";
            this.labelControl5.Click += new System.EventHandler(this.labelControl5_Click);
            // 
            // grcBuong
            // 
            this.grcBuong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grcBuong.Location = new System.Drawing.Point(2, 28);
            this.grcBuong.MainView = this.grvBuong;
            this.grcBuong.Name = "grcBuong";
            this.grcBuong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkCheck});
            this.grcBuong.Size = new System.Drawing.Size(305, 204);
            this.grcBuong.TabIndex = 17;
            this.grcBuong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBuong});
            // 
            // grvBuong
            // 
            this.grvBuong.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvBuong.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvBuong.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvBuong.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvBuong.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvBuong.Appearance.Row.Options.UseFont = true;
            this.grvBuong.Appearance.ViewCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.grvBuong.Appearance.ViewCaption.Options.UseFont = true;
            this.grvBuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenBuong,
            this.colChon});
            this.grvBuong.GridControl = this.grcBuong;
            this.grvBuong.Name = "grvBuong";
            this.grvBuong.OptionsView.EnableAppearanceEvenRow = true;
            this.grvBuong.OptionsView.EnableAppearanceOddRow = true;
            this.grvBuong.OptionsView.ShowGroupPanel = false;
            this.grvBuong.OptionsView.ShowViewCaption = true;
            this.grvBuong.ViewCaption = "Danh sách buồng trong khoa";
            // 
            // colTenBuong
            // 
            this.colTenBuong.Caption = "Tên buồng";
            this.colTenBuong.FieldName = "Buong";
            this.colTenBuong.Name = "colTenBuong";
            this.colTenBuong.Visible = true;
            this.colTenBuong.VisibleIndex = 1;
            this.colTenBuong.Width = 195;
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.chkCheck;
            this.colChon.FieldName = "Check";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 47;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoHeight = false;
            this.chkCheck.Caption = "Check";
            this.chkCheck.Name = "chkCheck";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grcBuong);
            this.panelControl1.Controls.Add(this.LupKhoaPhong);
            this.panelControl1.Location = new System.Drawing.Point(316, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(309, 234);
            this.panelControl1.TabIndex = 20;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.Taoso);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.LupNgayTu);
            this.panelControl3.Controls.Add(this.Huy);
            this.panelControl3.Controls.Add(this.LupNgayDen);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.radNT);
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(312, 232);
            this.panelControl3.TabIndex = 22;
            // 
            // Frm_TamTraDVKT_New
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 237);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TamTraDVKT_New";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tam tra dịch vụ - kỹ thuật";
            this.Load += new System.EventHandler(this.Frm_TamTraThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupKhoaPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radNT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcBuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit LupNgayTu;
        private DevExpress.XtraEditors.DateEdit LupNgayDen;
        private DevExpress.XtraEditors.LookUpEdit LupKhoaPhong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton Taoso;
        private DevExpress.XtraEditors.SimpleButton Huy;
        private DevExpress.XtraEditors.RadioGroup radNT;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl grcBuong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBuong;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBuong;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkCheck;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
    }
}