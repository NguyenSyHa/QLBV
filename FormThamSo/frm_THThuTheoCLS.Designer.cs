namespace QLBV.FormThamSo
{
    partial class frm_THThuTheoCLS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_THThuTheoCLS));
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grcKhoaPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnTaoBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = null;
            this.dtpTuNgay.Location = new System.Drawing.Point(56, 2);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTuNgay.Size = new System.Drawing.Size(181, 26);
            this.dtpTuNgay.StyleController = this.layoutControl1;
            this.dtpTuNgay.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Controls.Add(this.grcKhoaPhong);
            this.layoutControl1.Controls.Add(this.btnTaoBaoCao);
            this.layoutControl1.Controls.Add(this.dtpTuNgay);
            this.layoutControl1.Controls.Add(this.dtpDenNgay);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(478, 366);
            this.layoutControl1.TabIndex = 10;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(427, 342);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(49, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "Hủy";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Location = new System.Drawing.Point(2, 342);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(328, 22);
            this.layoutControl2.TabIndex = 10;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(328, 22);
            this.Root.TextVisible = false;
            // 
            // grcKhoaPhong
            // 
            this.grcKhoaPhong.Location = new System.Drawing.Point(2, 32);
            this.grcKhoaPhong.MainView = this.grvKhoaPhong;
            this.grcKhoaPhong.Name = "grcKhoaPhong";
            this.grcKhoaPhong.Size = new System.Drawing.Size(474, 306);
            this.grcKhoaPhong.TabIndex = 9;
            this.grcKhoaPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaPhong});
            // 
            // grvKhoaPhong
            // 
            this.grvKhoaPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grvChon,
            this.grvTenKP});
            this.grvKhoaPhong.GridControl = this.grcKhoaPhong;
            this.grvKhoaPhong.Name = "grvKhoaPhong";
            this.grvKhoaPhong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaPhong.OptionsView.ShowViewCaption = true;
            this.grvKhoaPhong.ViewCaption = "Chọn khoa phòng cận lâm sàng";
            this.grvKhoaPhong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaPhong_CellValueChanging);
            // 
            // grvChon
            // 
            this.grvChon.AppearanceHeader.Options.UseTextOptions = true;
            this.grvChon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvChon.Caption = "Chọn";
            this.grvChon.FieldName = "chon";
            this.grvChon.Name = "grvChon";
            this.grvChon.Visible = true;
            this.grvChon.VisibleIndex = 0;
            this.grvChon.Width = 89;
            // 
            // grvTenKP
            // 
            this.grvTenKP.AppearanceHeader.Options.UseTextOptions = true;
            this.grvTenKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvTenKP.Caption = "Tên Khoa Phòng";
            this.grvTenKP.FieldName = "tenkp";
            this.grvTenKP.Name = "grvTenKP";
            this.grvTenKP.Visible = true;
            this.grvTenKP.VisibleIndex = 1;
            this.grvTenKP.Width = 328;
            // 
            // btnTaoBaoCao
            // 
            this.btnTaoBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoBaoCao.Image")));
            this.btnTaoBaoCao.Location = new System.Drawing.Point(334, 342);
            this.btnTaoBaoCao.Name = "btnTaoBaoCao";
            this.btnTaoBaoCao.Size = new System.Drawing.Size(89, 22);
            this.btnTaoBaoCao.StyleController = this.layoutControl1;
            this.btnTaoBaoCao.TabIndex = 4;
            this.btnTaoBaoCao.Text = "Tạo báo cáo";
            this.btnTaoBaoCao.Click += new System.EventHandler(this.btnTaoBaoCao_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = null;
            this.dtpDenNgay.Location = new System.Drawing.Point(295, 2);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDenNgay.Size = new System.Drawing.Size(181, 26);
            this.dtpDenNgay.StyleController = this.layoutControl1;
            this.dtpDenNgay.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(478, 366);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dtpTuNgay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(239, 30);
            this.layoutControlItem1.Text = "Từ ngày:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dtpDenNgay;
            this.layoutControlItem2.Location = new System.Drawing.Point(239, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(239, 30);
            this.layoutControlItem2.Text = "Đến ngày:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grcKhoaPhong;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(478, 310);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnTaoBaoCao;
            this.layoutControlItem4.Location = new System.Drawing.Point(332, 340);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl2;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 340);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(332, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton1;
            this.layoutControlItem6.Location = new System.Drawing.Point(425, 340);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(53, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // frm_THThuTheoCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 366);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_THThuTheoCLS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tổng hợp thu theo từng phòng CLS";
            this.Load += new System.EventHandler(this.frm_THThuTheoCLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnTaoBaoCao;
        private DevExpress.XtraGrid.GridControl grcKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn grvChon;
        private DevExpress.XtraGrid.Columns.GridColumn grvTenKP;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}