namespace QLBV.ChucNang
{
    partial class frm_PhieuXNCovid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuXNCovid));
            this.grcPhuongPhap = new DevExpress.XtraGrid.GridControl();
            this.grvPhuongPhap = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PhuongPhap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMaBN = new DevExpress.XtraEditors.TextEdit();
            this.txtTenNhom = new DevExpress.XtraEditors.TextEdit();
            this.txtIdCls = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grcPhuongPhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhuongPhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCls.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grcPhuongPhap
            // 
            this.grcPhuongPhap.Location = new System.Drawing.Point(12, 12);
            this.grcPhuongPhap.MainView = this.grvPhuongPhap;
            this.grcPhuongPhap.Name = "grcPhuongPhap";
            this.grcPhuongPhap.Size = new System.Drawing.Size(417, 182);
            this.grcPhuongPhap.TabIndex = 0;
            this.grcPhuongPhap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPhuongPhap});
            // 
            // grvPhuongPhap
            // 
            this.grvPhuongPhap.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grvPhuongPhap.Appearance.FixedLine.Options.UseFont = true;
            this.grvPhuongPhap.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.grvPhuongPhap.ColumnPanelRowHeight = 25;
            this.grvPhuongPhap.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PhuongPhap});
            this.grvPhuongPhap.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.grvPhuongPhap.GridControl = this.grcPhuongPhap;
            this.grvPhuongPhap.Name = "grvPhuongPhap";
            this.grvPhuongPhap.OptionsView.ShowGroupPanel = false;
            this.grvPhuongPhap.RowHeight = 25;
            this.grvPhuongPhap.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grvPhuongPhap_RowClick);
            // 
            // PhuongPhap
            // 
            this.PhuongPhap.Caption = "Phương pháp";
            this.PhuongPhap.FieldName = "PhuongPhap";
            this.PhuongPhap.Name = "PhuongPhap";
            this.PhuongPhap.OptionsColumn.AllowEdit = false;
            this.PhuongPhap.Visible = true;
            this.PhuongPhap.VisibleIndex = 0;
            this.PhuongPhap.Width = 349;
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(12, 200);
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(100, 20);
            this.txtMaBN.TabIndex = 1;
            this.txtMaBN.Visible = false;
            // 
            // txtTenNhom
            // 
            this.txtTenNhom.Location = new System.Drawing.Point(171, 200);
            this.txtTenNhom.Name = "txtTenNhom";
            this.txtTenNhom.Size = new System.Drawing.Size(100, 20);
            this.txtTenNhom.TabIndex = 2;
            this.txtTenNhom.Visible = false;
            // 
            // txtIdCls
            // 
            this.txtIdCls.Location = new System.Drawing.Point(329, 200);
            this.txtIdCls.Name = "txtIdCls";
            this.txtIdCls.Size = new System.Drawing.Size(100, 20);
            this.txtIdCls.TabIndex = 3;
            this.txtIdCls.Visible = false;
            // 
            // frm_PhieuXNCovid
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(441, 203);
            this.Controls.Add(this.txtIdCls);
            this.Controls.Add(this.txtTenNhom);
            this.Controls.Add(this.txtMaBN);
            this.Controls.Add(this.grcPhuongPhap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_PhieuXNCovid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phương pháp xét nghiệm SARS-COV-2";
            this.Load += new System.EventHandler(this.frm_PhieuXNCovid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcPhuongPhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhuongPhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCls.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcPhuongPhap;
        private DevExpress.XtraGrid.Columns.GridColumn PhuongPhap;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPhuongPhap;
        private DevExpress.XtraEditors.TextEdit txtMaBN;
        private DevExpress.XtraEditors.TextEdit txtTenNhom;
        private DevExpress.XtraEditors.TextEdit txtIdCls;
    }
}