namespace QLBV.FormNhap
{
    partial class HuHaoDongY
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.grcthuocton = new DevExpress.XtraGrid.GridControl();
            this.grvthuocton = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.olTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSluongT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnTim = new DevExpress.XtraEditors.SimpleButton();
            this.cboSLMin = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lopKhoaP = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.cbolydo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtsoct = new DevExpress.XtraEditors.TextEdit();
            this.datngaytao = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.grcthuochuhao = new DevExpress.XtraGrid.GridControl();
            this.grvthuochuhao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colmadv1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltendv1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldongia1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsluonghuhao = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcthuocton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvthuocton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSLMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopKhoaP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbolydo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsoct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datngaytao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datngaytao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcthuochuhao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvthuochuhao)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Location = new System.Drawing.Point(2, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(441, 460);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.grcthuocton);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(2, 39);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(437, 419);
            this.panelControl4.TabIndex = 1;
            // 
            // grcthuocton
            // 
            this.grcthuocton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcthuocton.Location = new System.Drawing.Point(2, 2);
            this.grcthuocton.MainView = this.grvthuocton;
            this.grcthuocton.Name = "grcthuocton";
            this.grcthuocton.Size = new System.Drawing.Size(433, 415);
            this.grcthuocton.TabIndex = 0;
            this.grcthuocton.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvthuocton});
            // 
            // grvthuocton
            // 
            this.grvthuocton.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.olTenDV,
            this.colDonGia,
            this.colSluongT,
            this.colChon});
            this.grvthuocton.GridControl = this.grcthuocton;
            this.grvthuocton.Name = "grvthuocton";
            this.grvthuocton.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvthuocton.OptionsView.ShowFooter = true;
            this.grvthuocton.OptionsView.ShowGroupPanel = false;
            this.grvthuocton.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvthuocton_CellValueChanged);
            this.grvthuocton.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvthuocton_CellValueChanging);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã DV";
            this.colMaDV.FieldName = "madv";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.AllowEdit = false;
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "madv", "{0}")});
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 84;
            // 
            // olTenDV
            // 
            this.olTenDV.Caption = "Tên Thuốc";
            this.olTenDV.FieldName = "tendv";
            this.olTenDV.Name = "olTenDV";
            this.olTenDV.OptionsColumn.AllowEdit = false;
            this.olTenDV.OptionsColumn.ReadOnly = true;
            this.olTenDV.Visible = true;
            this.olTenDV.VisibleIndex = 1;
            this.olTenDV.Width = 309;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "dongia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowEdit = false;
            this.colDonGia.OptionsColumn.ReadOnly = true;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 105;
            // 
            // colSluongT
            // 
            this.colSluongT.Caption = "Số lượng tồn";
            this.colSluongT.FieldName = "sluongton";
            this.colSluongT.Name = "colSluongT";
            this.colSluongT.Visible = true;
            this.colSluongT.VisibleIndex = 3;
            this.colSluongT.Width = 122;
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 4;
            this.colChon.Width = 76;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnTim);
            this.panelControl3.Controls.Add(this.cboSLMin);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.lopKhoaP);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(437, 36);
            this.panelControl3.TabIndex = 0;
            // 
            // btnTim
            // 
            this.btnTim.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Appearance.Options.UseFont = true;
            this.btnTim.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnTim.Location = new System.Drawing.Point(408, 8);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(24, 23);
            this.btnTim.TabIndex = 3;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // cboSLMin
            // 
            this.cboSLMin.Location = new System.Drawing.Point(341, 8);
            this.cboSLMin.Name = "cboSLMin";
            this.cboSLMin.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSLMin.Properties.Appearance.Options.UseFont = true;
            this.cboSLMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSLMin.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cboSLMin.Size = new System.Drawing.Size(61, 22);
            this.cboSLMin.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(249, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Số lượng Min:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Chọn KP:";
            // 
            // lopKhoaP
            // 
            this.lopKhoaP.Location = new System.Drawing.Point(81, 7);
            this.lopKhoaP.Name = "lopKhoaP";
            this.lopKhoaP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lopKhoaP.Properties.Appearance.Options.UseFont = true;
            this.lopKhoaP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lopKhoaP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã Kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 80, "Tên Kho")});
            this.lopKhoaP.Properties.DisplayMember = "TenKP";
            this.lopKhoaP.Properties.NullText = "";
            this.lopKhoaP.Properties.ValueMember = "MaKP";
            this.lopKhoaP.Size = new System.Drawing.Size(162, 22);
            this.lopKhoaP.TabIndex = 0;
            this.lopKhoaP.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl6);
            this.panelControl2.Controls.Add(this.panelControl5);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(445, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(487, 465);
            this.panelControl2.TabIndex = 0;
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.cbolydo);
            this.panelControl6.Controls.Add(this.labelControl5);
            this.panelControl6.Controls.Add(this.labelControl4);
            this.panelControl6.Controls.Add(this.labelControl3);
            this.panelControl6.Controls.Add(this.txtsoct);
            this.panelControl6.Controls.Add(this.datngaytao);
            this.panelControl6.Controls.Add(this.simpleButton1);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(2, 391);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(483, 72);
            this.panelControl6.TabIndex = 1;
            // 
            // cbolydo
            // 
            this.cbolydo.Location = new System.Drawing.Point(78, 39);
            this.cbolydo.Name = "cbolydo";
            this.cbolydo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbolydo.Properties.Appearance.Options.UseFont = true;
            this.cbolydo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbolydo.Properties.Items.AddRange(new object[] {
            "Tạo hư hao tồn kho"});
            this.cbolydo.Size = new System.Drawing.Size(275, 24);
            this.cbolydo.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(15, 42);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 17);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Lý do:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(257, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 17);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Số chứng từ:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(15, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 17);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Ngày tạo:";
            // 
            // txtsoct
            // 
            this.txtsoct.Location = new System.Drawing.Point(337, 5);
            this.txtsoct.Name = "txtsoct";
            this.txtsoct.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsoct.Properties.Appearance.Options.UseFont = true;
            this.txtsoct.Size = new System.Drawing.Size(136, 24);
            this.txtsoct.TabIndex = 2;
            // 
            // datngaytao
            // 
            this.datngaytao.EditValue = null;
            this.datngaytao.Location = new System.Drawing.Point(78, 5);
            this.datngaytao.Name = "datngaytao";
            this.datngaytao.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datngaytao.Properties.Appearance.Options.UseFont = true;
            this.datngaytao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datngaytao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datngaytao.Properties.DisplayFormat.FormatString = "g";
            this.datngaytao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datngaytao.Properties.EditFormat.FormatString = "g";
            this.datngaytao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datngaytao.Size = new System.Drawing.Size(161, 24);
            this.datngaytao.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(369, 35);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(104, 27);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Tạo hư hao";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.grcthuochuhao);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(2, 2);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(483, 383);
            this.panelControl5.TabIndex = 0;
            // 
            // grcthuochuhao
            // 
            this.grcthuochuhao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcthuochuhao.Location = new System.Drawing.Point(2, 2);
            this.grcthuochuhao.MainView = this.grvthuochuhao;
            this.grcthuochuhao.Name = "grcthuochuhao";
            this.grcthuochuhao.Size = new System.Drawing.Size(479, 379);
            this.grcthuochuhao.TabIndex = 0;
            this.grcthuochuhao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvthuochuhao});
            this.grcthuochuhao.Click += new System.EventHandler(this.grcthuochuhao_Click);
            // 
            // grvthuochuhao
            // 
            this.grvthuochuhao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colmadv1,
            this.coltendv1,
            this.coldongia1,
            this.colsluonghuhao});
            this.grvthuochuhao.GridControl = this.grcthuochuhao;
            this.grvthuochuhao.Name = "grvthuochuhao";
            this.grvthuochuhao.OptionsView.ShowGroupPanel = false;
            // 
            // colmadv1
            // 
            this.colmadv1.Caption = "Mã Thuốc";
            this.colmadv1.FieldName = "madv";
            this.colmadv1.Name = "colmadv1";
            this.colmadv1.Visible = true;
            this.colmadv1.VisibleIndex = 0;
            this.colmadv1.Width = 91;
            // 
            // coltendv1
            // 
            this.coltendv1.Caption = "Tên Thuốc";
            this.coltendv1.FieldName = "tendv";
            this.coltendv1.Name = "coltendv1";
            this.coltendv1.Visible = true;
            this.coltendv1.VisibleIndex = 1;
            this.coltendv1.Width = 377;
            // 
            // coldongia1
            // 
            this.coldongia1.Caption = "Đơn giá";
            this.coldongia1.FieldName = "dongia";
            this.coldongia1.Name = "coldongia1";
            this.coldongia1.Visible = true;
            this.coldongia1.VisibleIndex = 2;
            this.coldongia1.Width = 80;
            // 
            // colsluonghuhao
            // 
            this.colsluonghuhao.Caption = "Số lượng hư hao";
            this.colsluonghuhao.FieldName = "sluongton";
            this.colsluonghuhao.Name = "colsluonghuhao";
            this.colsluonghuhao.Visible = true;
            this.colsluonghuhao.VisibleIndex = 3;
            this.colsluonghuhao.Width = 148;
            // 
            // HuHaoDongY
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 465);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HuHaoDongY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hư hao thuốc đông y";
            this.Load += new System.EventHandler(this.HuHaoDongY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcthuocton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvthuocton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSLMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopKhoaP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbolydo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsoct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datngaytao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datngaytao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcthuochuhao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvthuochuhao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl grcthuocton;
        private DevExpress.XtraGrid.Views.Grid.GridView grvthuocton;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lopKhoaP;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraGrid.GridControl grcthuochuhao;
        private DevExpress.XtraGrid.Views.Grid.GridView grvthuochuhao;
        private DevExpress.XtraEditors.ComboBoxEdit cboSLMin;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn olTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSluongT;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.SimpleButton btnTim;
        private DevExpress.XtraGrid.Columns.GridColumn colmadv1;
        private DevExpress.XtraGrid.Columns.GridColumn coltendv1;
        private DevExpress.XtraGrid.Columns.GridColumn coldongia1;
        private DevExpress.XtraGrid.Columns.GridColumn colsluonghuhao;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtsoct;
        private DevExpress.XtraEditors.DateEdit datngaytao;
        private DevExpress.XtraEditors.ComboBoxEdit cbolydo;
        private DevExpress.XtraEditors.LabelControl labelControl5;

    }
}