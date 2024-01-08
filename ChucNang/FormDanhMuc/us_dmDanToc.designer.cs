namespace QLBV.FormDanhMuc
{
    partial class us_dmDanToc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(us_dmDanToc));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtTenDT = new DevExpress.XtraEditors.TextEdit();
            this.txtMaDT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.grcDanToc = new DevExpress.XtraGrid.GridControl();
            this.grvDanToc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuuKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoaKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnSuaKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoiKb = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDanToc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanToc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtTenDT);
            this.panelControl1.Controls.Add(this.txtMaDT);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.cboStatus);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1024, 93);
            this.panelControl1.TabIndex = 0;
            // 
            // txtTenDT
            // 
            this.txtTenDT.Location = new System.Drawing.Point(230, 45);
            this.txtTenDT.Name = "txtTenDT";
            this.txtTenDT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenDT.Properties.Appearance.Options.UseFont = true;
            this.txtTenDT.Size = new System.Drawing.Size(197, 20);
            this.txtTenDT.TabIndex = 11;
            // 
            // txtMaDT
            // 
            this.txtMaDT.Location = new System.Drawing.Point(80, 45);
            this.txtMaDT.Name = "txtMaDT";
            this.txtMaDT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaDT.Properties.Appearance.Options.UseFont = true;
            this.txtMaDT.Size = new System.Drawing.Size(63, 20);
            this.txtMaDT.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(416, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(201, 23);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "DANH MỤC DÂN TỘC";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(6, 47);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(65, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Mã dân tộc:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(432, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Trạng thái:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(152, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Tên dân tộc:";
            // 
            // cboStatus
            // 
            this.cboStatus.EditValue = "Hoạt động";
            this.cboStatus.Location = new System.Drawing.Point(499, 43);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Items.AddRange(new object[] {
            "Hoạt động",
            "Không hoạt động"});
            this.cboStatus.Properties.PopupSizeable = true;
            this.cboStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboStatus.Size = new System.Drawing.Size(118, 20);
            this.cboStatus.TabIndex = 14;
            // 
            // grcDanToc
            // 
            this.grcDanToc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDanToc.Location = new System.Drawing.Point(0, 93);
            this.grcDanToc.MainView = this.grvDanToc;
            this.grcDanToc.Name = "grcDanToc";
            this.grcDanToc.Size = new System.Drawing.Size(1024, 507);
            this.grcDanToc.TabIndex = 1;
            this.grcDanToc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDanToc});
            // 
            // grvDanToc
            // 
            this.grvDanToc.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDanToc.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDanToc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDanToc.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDanToc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDanToc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDanToc.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDanToc.Appearance.Row.Options.UseFont = true;
            this.grvDanToc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDT,
            this.colTenDT,
            this.colStatus});
            this.grvDanToc.GridControl = this.grcDanToc;
            this.grvDanToc.Name = "grvDanToc";
            this.grvDanToc.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDanToc.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDanToc.OptionsBehavior.Editable = false;
            this.grvDanToc.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvDanToc.OptionsView.ColumnAutoWidth = false;
            this.grvDanToc.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDanToc.OptionsView.EnableAppearanceOddRow = true;
            this.grvDanToc.OptionsView.ShowGroupPanel = false;
            this.grvDanToc.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDanToc_FocusedRowChanged);
            // 
            // colMaDT
            // 
            this.colMaDT.Caption = "Mã dân tộc";
            this.colMaDT.FieldName = "MaDT";
            this.colMaDT.Name = "colMaDT";
            this.colMaDT.Visible = true;
            this.colMaDT.VisibleIndex = 0;
            this.colMaDT.Width = 113;
            // 
            // colTenDT
            // 
            this.colTenDT.Caption = "Tên dân tộc";
            this.colTenDT.FieldName = "TenDT";
            this.colTenDT.Name = "colTenDT";
            this.colTenDT.Visible = true;
            this.colTenDT.VisibleIndex = 1;
            this.colTenDT.Width = 289;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 2;
            this.colStatus.Width = 83;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtTimKiem);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 561);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1024, 39);
            this.panelControl2.TabIndex = 2;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(457, 11);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Size = new System.Drawing.Size(306, 20);
            this.txtTimKiem.TabIndex = 34;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl4.Location = new System.Drawing.Point(315, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(136, 13);
            this.labelControl4.TabIndex = 35;
            this.labelControl4.Text = "Tìm kiếm (theo tên DT):";
            this.labelControl4.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnLuuKb);
            this.panelControl4.Controls.Add(this.btnXoaKb);
            this.panelControl4.Controls.Add(this.btnSuaKb);
            this.panelControl4.Controls.Add(this.btnMoiKb);
            this.panelControl4.Location = new System.Drawing.Point(70, 5);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(230, 29);
            this.panelControl4.TabIndex = 33;
            // 
            // btnLuuKb
            // 
            this.btnLuuKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuuKb.Appearance.Options.UseFont = true;
            this.btnLuuKb.Appearance.Options.UseForeColor = true;
            this.btnLuuKb.Image = global::QLBV.Properties.Resources.saveall_16x16;
            this.btnLuuKb.Location = new System.Drawing.Point(177, 3);
            this.btnLuuKb.Name = "btnLuuKb";
            this.btnLuuKb.Size = new System.Drawing.Size(48, 23);
            this.btnLuuKb.TabIndex = 3;
            this.btnLuuKb.Text = "&Lưu";
            this.btnLuuKb.Click += new System.EventHandler(this.btnLuuKb_Click);
            // 
            // btnXoaKb
            // 
            this.btnXoaKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnXoaKb.Appearance.Options.UseFont = true;
            this.btnXoaKb.Appearance.Options.UseForeColor = true;
            this.btnXoaKb.Image = ((System.Drawing.Image)(resources.GetObject("btnXoaKb.Image")));
            this.btnXoaKb.Location = new System.Drawing.Point(120, 3);
            this.btnXoaKb.Name = "btnXoaKb";
            this.btnXoaKb.Size = new System.Drawing.Size(51, 23);
            this.btnXoaKb.TabIndex = 2;
            this.btnXoaKb.Text = "&Xóa";
            this.btnXoaKb.Click += new System.EventHandler(this.btnXoaKb_Click);
            // 
            // btnSuaKb
            // 
            this.btnSuaKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnSuaKb.Appearance.Options.UseFont = true;
            this.btnSuaKb.Appearance.Options.UseForeColor = true;
            this.btnSuaKb.Image = ((System.Drawing.Image)(resources.GetObject("btnSuaKb.Image")));
            this.btnSuaKb.Location = new System.Drawing.Point(60, 3);
            this.btnSuaKb.Name = "btnSuaKb";
            this.btnSuaKb.Size = new System.Drawing.Size(54, 23);
            this.btnSuaKb.TabIndex = 1;
            this.btnSuaKb.Text = "&Sửa";
            this.btnSuaKb.Click += new System.EventHandler(this.btnSuaKb_Click);
            // 
            // btnMoiKb
            // 
            this.btnMoiKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoiKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnMoiKb.Appearance.Options.UseFont = true;
            this.btnMoiKb.Appearance.Options.UseForeColor = true;
            this.btnMoiKb.Image = ((System.Drawing.Image)(resources.GetObject("btnMoiKb.Image")));
            this.btnMoiKb.Location = new System.Drawing.Point(4, 3);
            this.btnMoiKb.Name = "btnMoiKb";
            this.btnMoiKb.Size = new System.Drawing.Size(50, 23);
            this.btnMoiKb.TabIndex = 0;
            this.btnMoiKb.Text = "&Mới";
            this.btnMoiKb.Click += new System.EventHandler(this.btnMoiKb_Click);
            // 
            // us_dmDanToc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grcDanToc);
            this.Controls.Add(this.panelControl1);
            this.Name = "us_dmDanToc";
            this.Size = new System.Drawing.Size(1024, 600);
            this.Load += new System.EventHandler(this.us_dmDanToc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDanToc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanToc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtTenDT;
        private DevExpress.XtraEditors.TextEdit txtMaDT;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl grcDanToc;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDanToc;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDT;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDT;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnLuuKb;
        private DevExpress.XtraEditors.SimpleButton btnXoaKb;
        private DevExpress.XtraEditors.SimpleButton btnSuaKb;
        private DevExpress.XtraEditors.SimpleButton btnMoiKb;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
