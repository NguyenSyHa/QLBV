namespace QLBV.FormThamSo
{
    partial class frm_SoKiemNhapYHCT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SoKiemNhapYHCT));
            this.grcDSCT = new DevExpress.XtraGrid.GridControl();
            this.grvDSCT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDNhap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnInPhieu = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.RGChonmau = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGChonmau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcDSCT
            // 
            this.grcDSCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDSCT.Location = new System.Drawing.Point(2, 2);
            this.grcDSCT.MainView = this.grvDSCT;
            this.grcDSCT.Name = "grcDSCT";
            this.grcDSCT.Size = new System.Drawing.Size(452, 276);
            this.grcDSCT.TabIndex = 4;
            this.grcDSCT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDSCT});
            // 
            // grvDSCT
            // 
            this.grvDSCT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDNhap,
            this.colNgayNhap,
            this.colSoCT,
            this.colGhiChu,
            this.colCheck});
            this.grvDSCT.GridControl = this.grcDSCT;
            this.grvDSCT.Name = "grvDSCT";
            this.grvDSCT.OptionsView.ShowGroupPanel = false;
            this.grvDSCT.OptionsView.ShowViewCaption = true;
            this.grvDSCT.ViewCaption = "Danh sách chứng từ";
            this.grvDSCT.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDSCT_CellValueChanged);
            this.grvDSCT.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDSCT_CellValueChanging);
            // 
            // colIDNhap
            // 
            this.colIDNhap.Caption = "gridColumn1";
            this.colIDNhap.FieldName = "IDNhap";
            this.colIDNhap.Name = "colIDNhap";
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNgayNhap.AppearanceHeader.Options.UseFont = true;
            this.colNgayNhap.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgayNhap.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayNhap.Caption = "Ngày nhập";
            this.colNgayNhap.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayNhap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colNgayNhap.FieldName = "NgayNhap";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Visible = true;
            this.colNgayNhap.VisibleIndex = 0;
            this.colNgayNhap.Width = 174;
            // 
            // colSoCT
            // 
            this.colSoCT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSoCT.AppearanceHeader.Options.UseFont = true;
            this.colSoCT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSoCT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSoCT.Caption = "Số CT";
            this.colSoCT.FieldName = "SoCT";
            this.colSoCT.Name = "colSoCT";
            this.colSoCT.Visible = true;
            this.colSoCT.VisibleIndex = 1;
            this.colSoCT.Width = 184;
            // 
            // colGhiChu
            // 
            this.colGhiChu.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGhiChu.AppearanceHeader.Options.UseFont = true;
            this.colGhiChu.AppearanceHeader.Options.UseTextOptions = true;
            this.colGhiChu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGhiChu.Caption = "Nội dung";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 2;
            this.colGhiChu.Width = 239;
            // 
            // colCheck
            // 
            this.colCheck.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCheck.AppearanceHeader.Options.UseFont = true;
            this.colCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.colCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheck.Caption = "chọn";
            this.colCheck.FieldName = "Chon";
            this.colCheck.Name = "colCheck";
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 3;
            this.colCheck.Width = 99;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(4, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 181;
            this.label5.Text = "Kho Nhập:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(71, 39);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenkp", 200, "Tên kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("makp", "Mã kho", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "tenkp";
            this.lupKho.Properties.NullText = "Tất cả";
            this.lupKho.Properties.PopupSizeable = false;
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "makp";
            this.lupKho.Size = new System.Drawing.Size(377, 22);
            this.lupKho.TabIndex = 178;
            this.lupKho.EditValueChanged += new System.EventHandler(this.lupKho_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(230, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 180;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(4, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 179;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(297, 15);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(151, 22);
            this.lupDenNgay.TabIndex = 177;
            this.lupDenNgay.EditValueChanged += new System.EventHandler(this.lupDenNgay_EditValueChanged);
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(71, 15);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(151, 22);
            this.lupTuNgay.TabIndex = 176;
            this.lupTuNgay.EditValueChanged += new System.EventHandler(this.lupTuNgay_EditValueChanged);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Appearance.Options.UseForeColor = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(380, 9);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(71, 29);
            this.btnThoat.TabIndex = 183;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnInPhieu
            // 
            this.btnInPhieu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInPhieu.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnInPhieu.Appearance.Options.UseFont = true;
            this.btnInPhieu.Appearance.Options.UseForeColor = true;
            this.btnInPhieu.Image = ((System.Drawing.Image)(resources.GetObject("btnInPhieu.Image")));
            this.btnInPhieu.Location = new System.Drawing.Point(303, 9);
            this.btnInPhieu.Name = "btnInPhieu";
            this.btnInPhieu.Size = new System.Drawing.Size(71, 29);
            this.btnInPhieu.TabIndex = 182;
            this.btnInPhieu.Text = " &In ";
            this.btnInPhieu.ToolTip = "In phiếu khám bệnh";
            this.btnInPhieu.Click += new System.EventHandler(this.btnInPhieu_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.RGChonmau);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnInPhieu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 351);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(456, 56);
            this.panelControl1.TabIndex = 184;
            // 
            // RGChonmau
            // 
            this.RGChonmau.Location = new System.Drawing.Point(12, 9);
            this.RGChonmau.Name = "RGChonmau";
            this.RGChonmau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGChonmau.Properties.Appearance.Options.UseFont = true;
            this.RGChonmau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chọn mẫu in A4"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chọn mẫu in A3")});
            this.RGChonmau.Size = new System.Drawing.Size(280, 29);
            this.RGChonmau.TabIndex = 184;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcDSCT);
            this.panelControl2.Location = new System.Drawing.Point(0, 68);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(456, 280);
            this.panelControl2.TabIndex = 185;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.label2);
            this.panelControl3.Controls.Add(this.lupTuNgay);
            this.panelControl3.Controls.Add(this.lupDenNgay);
            this.panelControl3.Controls.Add(this.label5);
            this.panelControl3.Controls.Add(this.label3);
            this.panelControl3.Controls.Add(this.lupKho);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(456, 68);
            this.panelControl3.TabIndex = 186;
            // 
            // frm_SoKiemNhapYHCT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 407);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SoKiemNhapYHCT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ kiểm nhập dược liệu, vị thuốc YHCT ";
            this.Load += new System.EventHandler(this.frm_SoKiemNhapYHCT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcDSCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGChonmau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcDSCT;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDSCT;
        private DevExpress.XtraGrid.Columns.GridColumn colIDNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCT;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnInPhieu;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.RadioGroup RGChonmau;
    }
}