namespace QLBV.FormThamSo
{
    partial class frm_LoadDuocXP
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LoadDuocXP));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.grcFile = new DevExpress.XtraGrid.GridControl();
            this.grvFile = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ck_Chon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTenFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXemChiTiet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_ViewChiTiet = new DevExpress.XtraCharts.Designer.Native.RepositoryItemImageButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnBackUpFolder = new DevExpress.XtraEditors.SimpleButton();
            this.txtBackUP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnChonFilePath_XML = new DevExpress.XtraEditors.SimpleButton();
            this.txtFromFileFolder = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grcErr = new DevExpress.XtraGrid.GridControl();
            this.grvErr = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMalk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageButtonEdit1 = new DevExpress.XtraCharts.Designer.Native.RepositoryItemImageButtonEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.TimKiem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grcFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Chon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ViewChiTiet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackUP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFileFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcFile
            // 
            this.grcFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcFile.Location = new System.Drawing.Point(2, 2);
            this.grcFile.MainView = this.grvFile;
            this.grcFile.Name = "grcFile";
            this.grcFile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btn_ViewChiTiet,
            this.ck_Chon});
            this.grcFile.Size = new System.Drawing.Size(398, 447);
            this.grcFile.TabIndex = 0;
            this.grcFile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFile});
            // 
            // grvFile
            // 
            this.grvFile.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvFile.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvFile.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvFile.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvFile.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvFile.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colTenFile,
            this.colDateEdit,
            this.colTrangThai,
            this.colXemChiTiet});
            this.grvFile.GridControl = this.grcFile;
            this.grvFile.Name = "grvFile";
            this.grvFile.OptionsView.ColumnAutoWidth = false;
            this.grvFile.OptionsView.ShowGroupPanel = false;
            this.grvFile.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvFile_FocusedRowChanged);
            // 
            // colCheck
            // 
            this.colCheck.Caption = "Chọn";
            this.colCheck.ColumnEdit = this.ck_Chon;
            this.colCheck.FieldName = "check";
            this.colCheck.Name = "colCheck";
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 40;
            // 
            // ck_Chon
            // 
            this.ck_Chon.AutoHeight = false;
            this.ck_Chon.Name = "ck_Chon";
            // 
            // colTenFile
            // 
            this.colTenFile.Caption = "Tên file";
            this.colTenFile.FieldName = "filename";
            this.colTenFile.Name = "colTenFile";
            this.colTenFile.Visible = true;
            this.colTenFile.VisibleIndex = 1;
            this.colTenFile.Width = 180;
            // 
            // colDateEdit
            // 
            this.colDateEdit.Caption = "Ngày tạo";
            this.colDateEdit.FieldName = "datemodify";
            this.colDateEdit.Name = "colDateEdit";
            this.colDateEdit.Visible = true;
            this.colDateEdit.VisibleIndex = 2;
            this.colDateEdit.Width = 80;
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.FieldName = "trangthai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 3;
            this.colTrangThai.Width = 60;
            // 
            // colXemChiTiet
            // 
            this.colXemChiTiet.Caption = "Xem chi tiết";
            this.colXemChiTiet.ColumnEdit = this.btn_ViewChiTiet;
            this.colXemChiTiet.Name = "colXemChiTiet";
            this.colXemChiTiet.Width = 60;
            // 
            // btn_ViewChiTiet
            // 
            this.btn_ViewChiTiet.AutoHeight = false;
            this.btn_ViewChiTiet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chi tiết", 16, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::QLBV.Properties.Resources.preview_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btn_ViewChiTiet.Name = "btn_ViewChiTiet";
            this.btn_ViewChiTiet.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btn_ViewChiTiet.Click += new System.EventHandler(this.btn_ViewChiTiet_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TimKiem);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.btnBackUpFolder);
            this.panel1.Controls.Add(this.txtBackUP);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.btnChonFilePath_XML);
            this.panel1.Controls.Add(this.txtFromFileFolder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 64);
            this.panel1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = global::QLBV.Properties.Resources.saveall_16x16;
            this.simpleButton1.Location = new System.Drawing.Point(538, 31);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(95, 29);
            this.simpleButton1.TabIndex = 27;
            this.simpleButton1.Text = "&Lưu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(639, 31);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 29);
            this.btnHuy.TabIndex = 26;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(13, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(95, 15);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "Thư mục backup:";
            // 
            // btnBackUpFolder
            // 
            this.btnBackUpFolder.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUpFolder.Appearance.Options.UseFont = true;
            this.btnBackUpFolder.Location = new System.Drawing.Point(371, 36);
            this.btnBackUpFolder.Name = "btnBackUpFolder";
            this.btnBackUpFolder.Size = new System.Drawing.Size(24, 24);
            this.btnBackUpFolder.TabIndex = 24;
            this.btnBackUpFolder.Text = "...";
            this.btnBackUpFolder.ToolTip = "Chọn nơi lưu";
            this.btnBackUpFolder.Click += new System.EventHandler(this.btnBackUpFolder_Click);
            // 
            // txtBackUP
            // 
            this.txtBackUP.Location = new System.Drawing.Point(119, 38);
            this.txtBackUP.Name = "txtBackUP";
            this.txtBackUP.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackUP.Properties.Appearance.Options.UseFont = true;
            this.txtBackUP.Size = new System.Drawing.Size(246, 22);
            this.txtBackUP.TabIndex = 23;
            this.txtBackUP.EditValueChanged += new System.EventHandler(this.txtBackUP_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 15);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Thư mục load file:";
            // 
            // btnChonFilePath_XML
            // 
            this.btnChonFilePath_XML.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonFilePath_XML.Appearance.Options.UseFont = true;
            this.btnChonFilePath_XML.Location = new System.Drawing.Point(371, 4);
            this.btnChonFilePath_XML.Name = "btnChonFilePath_XML";
            this.btnChonFilePath_XML.Size = new System.Drawing.Size(24, 24);
            this.btnChonFilePath_XML.TabIndex = 20;
            this.btnChonFilePath_XML.Text = "...";
            this.btnChonFilePath_XML.ToolTip = "Chọn nơi lưu";
            this.btnChonFilePath_XML.Click += new System.EventHandler(this.btnChonFilePath_XML_Click);
            // 
            // txtFromFileFolder
            // 
            this.txtFromFileFolder.Location = new System.Drawing.Point(119, 6);
            this.txtFromFileFolder.Name = "txtFromFileFolder";
            this.txtFromFileFolder.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromFileFolder.Properties.Appearance.Options.UseFont = true;
            this.txtFromFileFolder.Size = new System.Drawing.Size(246, 22);
            this.txtFromFileFolder.TabIndex = 0;
            this.txtFromFileFolder.EditValueChanged += new System.EventHandler(this.txtFromFileFolder_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grcErr);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 64);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(865, 455);
            this.panelControl1.TabIndex = 2;
            // 
            // grcErr
            // 
            this.grcErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcErr.Location = new System.Drawing.Point(404, 2);
            this.grcErr.MainView = this.grvErr;
            this.grcErr.Name = "grcErr";
            this.grcErr.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageButtonEdit1,
            this.repositoryItemCheckEdit1});
            this.grcErr.Size = new System.Drawing.Size(459, 451);
            this.grcErr.TabIndex = 2;
            this.grcErr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvErr});
            // 
            // grvErr
            // 
            this.grvErr.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvErr.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvErr.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvErr.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvErr.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvErr.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMalk,
            this.colMess,
            this.colField,
            this.colMaDV});
            this.grvErr.GridControl = this.grcErr;
            this.grvErr.Name = "grvErr";
            this.grvErr.OptionsView.ColumnAutoWidth = false;
            this.grvErr.OptionsView.ShowGroupPanel = false;
            // 
            // colMalk
            // 
            this.colMalk.Caption = "Mã liên kết";
            this.colMalk.FieldName = "Ma_lk";
            this.colMalk.Name = "colMalk";
            this.colMalk.Visible = true;
            this.colMalk.VisibleIndex = 0;
            this.colMalk.Width = 80;
            // 
            // colMess
            // 
            this.colMess.Caption = "Thông điệp lỗi";
            this.colMess.FieldName = "Mss";
            this.colMess.Name = "colMess";
            this.colMess.Visible = true;
            this.colMess.VisibleIndex = 1;
            this.colMess.Width = 200;
            // 
            // colField
            // 
            this.colField.Caption = "Tên trường";
            this.colField.FieldName = "Field_err";
            this.colField.Name = "colField";
            this.colField.Visible = true;
            this.colField.VisibleIndex = 2;
            this.colField.Width = 70;
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã DV";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 3;
            this.colMaDV.Width = 60;
            // 
            // repositoryItemImageButtonEdit1
            // 
            this.repositoryItemImageButtonEdit1.AutoHeight = false;
            this.repositoryItemImageButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chi tiết", 16, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::QLBV.Properties.Resources.preview_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.repositoryItemImageButtonEdit1.Name = "repositoryItemImageButtonEdit1";
            this.repositoryItemImageButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcFile);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(402, 451);
            this.panelControl2.TabIndex = 1;
            // 
            // TimKiem
            // 
            this.TimKiem.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.TimKiem.Appearance.Options.UseFont = true;
            this.TimKiem.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.TimKiem.Location = new System.Drawing.Point(437, 31);
            this.TimKiem.Name = "TimKiem";
            this.TimKiem.Size = new System.Drawing.Size(95, 29);
            this.TimKiem.TabIndex = 28;
            this.TimKiem.Text = "&Tìm kiếm";
            this.TimKiem.Click += new System.EventHandler(this.TimKiem_Click);
            // 
            // frm_LoadDuocXP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 519);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frm_LoadDuocXP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập dược xã phường";
            this.Load += new System.EventHandler(this.frm_KetNoiXP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_Chon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ViewChiTiet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackUP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFileFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcFile;
        private DevExpress.XtraGrid.Views.Grid.GridView grvFile;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit txtFromFileFolder;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnChonFilePath_XML;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ck_Chon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenFile;
        private DevExpress.XtraGrid.Columns.GridColumn colDateEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraCharts.Designer.Native.RepositoryItemImageButtonEdit btn_ViewChiTiet;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnBackUpFolder;
        private DevExpress.XtraEditors.TextEdit txtBackUP;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraGrid.Columns.GridColumn colXemChiTiet;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grcErr;
        private DevExpress.XtraGrid.Views.Grid.GridView grvErr;
        private DevExpress.XtraGrid.Columns.GridColumn colMalk;
        private DevExpress.XtraGrid.Columns.GridColumn colMess;
        private DevExpress.XtraGrid.Columns.GridColumn colField;
        public DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraCharts.Designer.Native.RepositoryItemImageButtonEdit repositoryItemImageButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton TimKiem;
    }
}