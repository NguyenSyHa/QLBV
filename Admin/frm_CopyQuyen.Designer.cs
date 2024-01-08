namespace QLBV.Admin
{
    partial class frm_CopyQuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CopyQuyen));
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.grc_Admin = new DevExpress.XtraGrid.GridControl();
            this.grv_Admin = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDTenDN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColKhoaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaCB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTenDangNhap = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_Admin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_Admin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDangNhap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.grc_Admin);
            this.panelControl3.Location = new System.Drawing.Point(2, 26);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(618, 349);
            this.panelControl3.TabIndex = 0;
            // 
            // grc_Admin
            // 
            this.grc_Admin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_Admin.Location = new System.Drawing.Point(0, 0);
            this.grc_Admin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.grc_Admin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grc_Admin.MainView = this.grv_Admin;
            this.grc_Admin.Name = "grc_Admin";
            this.grc_Admin.Size = new System.Drawing.Size(618, 349);
            this.grc_Admin.TabIndex = 1;
            this.grc_Admin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_Admin});
            // 
            // grv_Admin
            // 
            this.grv_Admin.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            this.grv_Admin.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_Admin.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Maroon;
            this.grv_Admin.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grv_Admin.Appearance.FocusedRow.Options.UseFont = true;
            this.grv_Admin.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grv_Admin.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_Admin.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Navy;
            this.grv_Admin.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_Admin.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grv_Admin.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grv_Admin.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grv_Admin.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grv_Admin.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_Admin.Appearance.Row.Options.UseFont = true;
            this.grv_Admin.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDTenDN,
            this.colTenDN,
            this.colTenGoi,
            this.ColKhoaPhong,
            this.colMaCB});
            this.grv_Admin.GridControl = this.grc_Admin;
            this.grv_Admin.Name = "grv_Admin";
            this.grv_Admin.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grv_Admin.OptionsBehavior.Editable = false;
            this.grv_Admin.OptionsBehavior.ReadOnly = true;
            this.grv_Admin.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grv_Admin.OptionsSelection.MultiSelect = true;
            this.grv_Admin.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grv_Admin.OptionsView.ShowGroupPanel = false;
            this.grv_Admin.OptionsView.ShowIndicator = false;
            // 
            // colIDTenDN
            // 
            this.colIDTenDN.Caption = "ID ";
            this.colIDTenDN.FieldName = "ID";
            this.colIDTenDN.Name = "colIDTenDN";
            this.colIDTenDN.Width = 50;
            // 
            // colTenDN
            // 
            this.colTenDN.Caption = "Tên đăng nhập";
            this.colTenDN.FieldName = "TenDN";
            this.colTenDN.Name = "colTenDN";
            this.colTenDN.OptionsColumn.AllowEdit = false;
            this.colTenDN.Visible = true;
            this.colTenDN.VisibleIndex = 1;
            this.colTenDN.Width = 100;
            // 
            // colTenGoi
            // 
            this.colTenGoi.Caption = "Tên gọi";
            this.colTenGoi.FieldName = "TenGoi";
            this.colTenGoi.Name = "colTenGoi";
            this.colTenGoi.OptionsColumn.AllowEdit = false;
            this.colTenGoi.Visible = true;
            this.colTenGoi.VisibleIndex = 2;
            this.colTenGoi.Width = 191;
            // 
            // ColKhoaPhong
            // 
            this.ColKhoaPhong.Caption = "Khoa phòng";
            this.ColKhoaPhong.FieldName = "TenKP";
            this.ColKhoaPhong.Name = "ColKhoaPhong";
            this.ColKhoaPhong.OptionsColumn.AllowEdit = false;
            this.ColKhoaPhong.Visible = true;
            this.ColKhoaPhong.VisibleIndex = 3;
            this.ColKhoaPhong.Width = 190;
            // 
            // colMaCB
            // 
            this.colMaCB.Caption = "Mã cán bộ";
            this.colMaCB.FieldName = "MaCB";
            this.colMaCB.Name = "colMaCB";
            this.colMaCB.OptionsColumn.AllowEdit = false;
            this.colMaCB.Visible = true;
            this.colMaCB.VisibleIndex = 4;
            this.colMaCB.Width = 94;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtTenDangNhap);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(427, 205, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(622, 403);
            this.layoutControl1.TabIndex = 7;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(2, 2);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Properties.NullValuePrompt = "Tìm theo tên";
            this.txtTenDangNhap.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTenDangNhap.Size = new System.Drawing.Size(618, 20);
            this.txtTenDangNhap.StyleController = this.layoutControl1;
            this.txtTenDangNhap.TabIndex = 4;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txtTenDangNhap_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(504, 379);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Đồng ý";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(622, 403);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl3;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(622, 353);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.Location = new System.Drawing.Point(502, 377);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(120, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 377);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(502, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtTenDangNhap;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(622, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frm_CopyQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 403);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frm_CopyQuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn tài khoản đích";
            this.Load += new System.EventHandler(this.frm_CopyQuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_Admin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_Admin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDangNhap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl grc_Admin;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_Admin;
        private DevExpress.XtraGrid.Columns.GridColumn colIDTenDN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenGoi;
        private DevExpress.XtraGrid.Columns.GridColumn ColKhoaPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCB;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtTenDangNhap;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}