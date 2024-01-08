namespace QLBV.FormDanhMuc
{
    partial class CLS
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.grcCLS = new DevExpress.XtraGrid.GridControl();
            this.grvCLS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupDanToc = new DevExpress.XtraEditors.LookUpEdit();
            this.cboNgaySinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMaBNhan = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDanToc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgaySinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaBNhan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1024, 100);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 100);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(200, 500);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(196, 496);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grcCLS);
            this.panelControl3.Controls.Add(this.lupDanToc);
            this.panelControl3.Controls.Add(this.cboNgaySinh);
            this.panelControl3.Controls.Add(this.txtMaBNhan);
            this.panelControl3.Controls.Add(this.btnOK);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(200, 100);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(824, 500);
            this.panelControl3.TabIndex = 2;
            this.panelControl3.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl3_Paint);
            // 
            // grcCLS
            // 
            this.grcCLS.Location = new System.Drawing.Point(16, 188);
            this.grcCLS.MainView = this.grvCLS;
            this.grcCLS.Name = "grcCLS";
            this.grcCLS.Size = new System.Drawing.Size(637, 223);
            this.grcCLS.TabIndex = 4;
            this.grcCLS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCLS});
            // 
            // grvCLS
            // 
            this.grvCLS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenBNhan,
            this.gridColumn2,
            this.gridColumn3});
            this.grvCLS.GridControl = this.grcCLS;
            this.grvCLS.Name = "grvCLS";
            this.grvCLS.OptionsNavigation.AutoFocusNewRow = true;
            this.grvCLS.OptionsView.ColumnAutoWidth = false;
            this.grvCLS.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            // 
            // colTenBNhan
            // 
            this.colTenBNhan.Caption = "Tên bệnh nhân";
            this.colTenBNhan.FieldName = "TenBNhan";
            this.colTenBNhan.Name = "colTenBNhan";
            this.colTenBNhan.Visible = true;
            this.colTenBNhan.VisibleIndex = 0;
            this.colTenBNhan.Width = 135;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 103;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 251;
            // 
            // lupDanToc
            // 
            this.lupDanToc.Location = new System.Drawing.Point(120, 162);
            this.lupDanToc.Name = "lupDanToc";
            this.lupDanToc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDanToc.Size = new System.Drawing.Size(100, 20);
            this.lupDanToc.TabIndex = 3;
            // 
            // cboNgaySinh
            // 
            this.cboNgaySinh.Location = new System.Drawing.Point(120, 124);
            this.cboNgaySinh.Name = "cboNgaySinh";
            this.cboNgaySinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNgaySinh.Properties.Items.AddRange(new object[] {
            "1 la 1",
            "2 la 2"});
            this.cboNgaySinh.Size = new System.Drawing.Size(100, 20);
            this.cboNgaySinh.TabIndex = 2;
            // 
            // txtMaBNhan
            // 
            this.txtMaBNhan.Location = new System.Drawing.Point(102, 63);
            this.txtMaBNhan.Name = "txtMaBNhan";
            this.txtMaBNhan.Size = new System.Drawing.Size(100, 20);
            this.txtMaBNhan.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(354, 41);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // CLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "CLS";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDanToc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgaySinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaBNhan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupDanToc;
        private DevExpress.XtraEditors.ComboBoxEdit cboNgaySinh;
        private DevExpress.XtraEditors.TextEdit txtMaBNhan;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl grcCLS;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCLS;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}
