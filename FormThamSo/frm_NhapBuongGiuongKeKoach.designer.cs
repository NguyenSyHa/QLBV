namespace QLBV.FormThamSo
{
    partial class frm_NhapBuongGiuongKeKoach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NhapBuongGiuongKeKoach));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.celGiuong = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.grb = new DevExpress.XtraGrid.GridControl();
            this.grbuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.buong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbbNam = new DevExpress.XtraEditors.ComboBoxEdit();
            this.KPhong1 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            this.grGiuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.giuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.magiuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grG = new DevExpress.XtraGrid.GridControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.namtu = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.dennam = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.celGiuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grGiuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namtu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dennam.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(273, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Khoa phòng:";
            // 
            // celGiuong
            // 
            this.celGiuong.Enabled = false;
            this.celGiuong.Location = new System.Drawing.Point(314, 34);
            this.celGiuong.Name = "celGiuong";
            this.celGiuong.Size = new System.Drawing.Size(48, 20);
            this.celGiuong.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(231, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Số giường:";
            // 
            // grb
            // 
            this.grb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb.Enabled = false;
            this.grb.Location = new System.Drawing.Point(0, 0);
            this.grb.MainView = this.grbuong;
            this.grb.Name = "grb";
            this.grb.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.grb.Size = new System.Drawing.Size(178, 264);
            this.grb.TabIndex = 3;
            this.grb.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grbuong});
            // 
            // grbuong
            // 
            this.grbuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.buong});
            this.grbuong.GridControl = this.grb;
            this.grbuong.Name = "grbuong";
            this.grbuong.NewItemRowText = "Buồng số";
            this.grbuong.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grbuong.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grbuong.OptionsView.ShowGroupPanel = false;
            this.grbuong.OptionsView.ShowViewCaption = true;
            this.grbuong.ViewCaption = "Buồng";
            this.grbuong.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grbuong_FocusedRowChanged);
            this.grbuong.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grbuong_CellValueChanged);
            this.grbuong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grbuong_CellValueChanging);
            this.grbuong.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grbuong_ValidateRow);
            // 
            // buong
            // 
            this.buong.Caption = "Buồng";
            this.buong.ColumnEdit = this.repositoryItemTextEdit1;
            this.buong.FieldName = "buong";
            this.buong.Name = "buong";
            this.buong.Visible = true;
            this.buong.VisibleIndex = 0;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbbNam);
            this.panel1.Controls.Add(this.KPhong1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.celGiuong);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 58);
            this.panel1.TabIndex = 6;
            // 
            // cbbNam
            // 
            this.cbbNam.Location = new System.Drawing.Point(314, 8);
            this.cbbNam.Name = "cbbNam";
            this.cbbNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbNam.Properties.Items.AddRange(new object[] {
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021"});
            this.cbbNam.Size = new System.Drawing.Size(48, 20);
            this.cbbNam.TabIndex = 1;
            this.cbbNam.SelectedIndexChanged += new System.EventHandler(this.cbbNam_SelectedIndexChanged);
            // 
            // KPhong1
            // 
            this.KPhong1.AutoSize = true;
            this.KPhong1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KPhong1.Location = new System.Drawing.Point(93, 23);
            this.KPhong1.Name = "KPhong1";
            this.KPhong1.Size = new System.Drawing.Size(0, 16);
            this.KPhong1.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridSplitContainer1);
            this.panelControl1.Location = new System.Drawing.Point(0, 64);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(182, 268);
            this.panelControl1.TabIndex = 7;
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSplitContainer1.Grid = this.grb;
            this.gridSplitContainer1.Location = new System.Drawing.Point(2, 2);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Panel1.Controls.Add(this.grb);
            this.gridSplitContainer1.Size = new System.Drawing.Size(178, 264);
            this.gridSplitContainer1.TabIndex = 0;
            // 
            // grGiuong
            // 
            this.grGiuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.giuong,
            this.magiuong});
            this.grGiuong.GridControl = this.grG;
            this.grGiuong.Name = "grGiuong";
            this.grGiuong.NewItemRowText = "Giường số";
            this.grGiuong.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grGiuong.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grGiuong.OptionsView.ShowGroupPanel = false;
            this.grGiuong.OptionsView.ShowViewCaption = true;
            this.grGiuong.ViewCaption = "Giường";
            this.grGiuong.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grGiuong_ValidateRow);
            // 
            // giuong
            // 
            this.giuong.Caption = "Giường";
            this.giuong.FieldName = "giuongTT";
            this.giuong.Name = "giuong";
            this.giuong.Visible = true;
            this.giuong.VisibleIndex = 0;
            // 
            // magiuong
            // 
            this.magiuong.Caption = "gridColumn1";
            this.magiuong.FieldName = "buong";
            this.magiuong.Name = "magiuong";
            // 
            // grG
            // 
            this.grG.Enabled = false;
            this.grG.Location = new System.Drawing.Point(186, 64);
            this.grG.MainView = this.grGiuong;
            this.grG.Name = "grG";
            this.grG.Size = new System.Drawing.Size(210, 266);
            this.grG.TabIndex = 4;
            this.grG.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grGiuong});
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = global::QLBV.Properties.Resources.save_16x16;
            this.btnOK.Location = new System.Drawing.Point(200, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 31);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&Lưu";
            this.btnOK.ToolTipTitle = "jhgjgjhj";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(306, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(92, 31);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.btnThoat);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 387);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(400, 43);
            this.panelControl2.TabIndex = 9;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(16, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(100, 31);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "&Update";
            this.simpleButton2.ToolTipTitle = "jhgjgjhj";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(122, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(72, 31);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "&Sửa";
            this.simpleButton1.ToolTipTitle = "jhgjgjhj";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // namtu
            // 
            this.namtu.Location = new System.Drawing.Point(68, 348);
            this.namtu.Name = "namtu";
            this.namtu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.namtu.Properties.Items.AddRange(new object[] {
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.namtu.Size = new System.Drawing.Size(48, 20);
            this.namtu.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Năm cũ:";
            // 
            // dennam
            // 
            this.dennam.Location = new System.Drawing.Point(246, 348);
            this.dennam.Name = "dennam";
            this.dennam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dennam.Properties.Items.AddRange(new object[] {
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.dennam.Size = new System.Drawing.Size(48, 20);
            this.dennam.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(174, 351);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Năm mới:";
            // 
            // frm_NhapBuongGiuongKeKoach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 430);
            this.Controls.Add(this.dennam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.namtu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grG);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_NhapBuongGiuongKeKoach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buồng giường";
            this.Load += new System.EventHandler(this.frm_NhapBuongGiuongKeKoach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.celGiuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grGiuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namtu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dennam.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit celGiuong;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl grb;
        private DevExpress.XtraGrid.Columns.GridColumn buong;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public System.Windows.Forms.Label KPhong1;
        private DevExpress.XtraEditors.ComboBoxEdit cbbNam;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        private DevExpress.XtraGrid.Views.Grid.GridView grGiuong;
        private DevExpress.XtraGrid.Columns.GridColumn giuong;
        private DevExpress.XtraGrid.GridControl grG;
        private DevExpress.XtraGrid.Columns.GridColumn magiuong;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView grbuong;
        private System.Windows.Forms.BindingSource bindingSource2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.ComboBoxEdit namtu;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit dennam;
        private System.Windows.Forms.Label label5;

    }
}