namespace QLBV.FormThamSo
{
    partial class frm_BCSLTon_ChenhLech
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCSLTon_ChenhLech));
            this.previewBar3 = new DevExpress.XtraPrinting.Preview.PreviewBar();
            this.previewBar1 = new DevExpress.XtraPrinting.Preview.PreviewBar();
            this.grc_DichVu = new DevExpress.XtraGrid.GridControl();
            this.grv_DichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hplXoa = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grc_DichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplXoa)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewBar3
            // 
            this.previewBar3.BarName = "Main Menu";
            this.previewBar3.DockCol = 0;
            this.previewBar3.DockRow = 0;
            this.previewBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.previewBar3.FloatLocation = new System.Drawing.Point(438, 187);
            this.previewBar3.OptionsBar.MultiLine = true;
            this.previewBar3.OptionsBar.UseWholeRow = true;
            this.previewBar3.Text = "Main Menu";
            // 
            // previewBar1
            // 
            this.previewBar1.BarName = "Main Menu";
            this.previewBar1.DockCol = 0;
            this.previewBar1.DockRow = 0;
            this.previewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.previewBar1.FloatLocation = new System.Drawing.Point(438, 187);
            this.previewBar1.OptionsBar.MultiLine = true;
            this.previewBar1.OptionsBar.UseWholeRow = true;
            this.previewBar1.Text = "Main Menu";
            // 
            // grc_DichVu
            // 
            this.grc_DichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_DichVu.Location = new System.Drawing.Point(0, 0);
            this.grc_DichVu.MainView = this.grv_DichVu;
            this.grc_DichVu.Name = "grc_DichVu";
            this.grc_DichVu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.hplXoa});
            this.grc_DichVu.Size = new System.Drawing.Size(684, 394);
            this.grc_DichVu.TabIndex = 0;
            this.grc_DichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_DichVu});
            // 
            // grv_DichVu
            // 
            this.grv_DichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenDV,
            this.colSLT,
            this.colSLMin,
            this.colCL,
            this.colMaDV,
            this.gridColumn1});
            this.grv_DichVu.GridControl = this.grc_DichVu;
            this.grv_DichVu.Name = "grv_DichVu";
            this.grv_DichVu.OptionsBehavior.Editable = false;
            this.grv_DichVu.OptionsBehavior.ReadOnly = true;
            this.grv_DichVu.OptionsView.ShowGroupPanel = false;
            this.grv_DichVu.OptionsView.ShowViewCaption = true;
            this.grv_DichVu.ViewCaption = "Danh sách thuốc có số lượng tồn nhỏ hơn số lượng Min ";
            // 
            // colTenDV
            // 
            this.colTenDV.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenDV.AppearanceHeader.Options.UseFont = true;
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.OptionsColumn.ReadOnly = true;
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            // 
            // colSLT
            // 
            this.colSLT.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSLT.AppearanceHeader.Options.UseFont = true;
            this.colSLT.Caption = "Số lượng tồn";
            this.colSLT.FieldName = "SLT";
            this.colSLT.Name = "colSLT";
            this.colSLT.OptionsColumn.AllowEdit = false;
            this.colSLT.OptionsColumn.ReadOnly = true;
            this.colSLT.Visible = true;
            this.colSLT.VisibleIndex = 2;
            // 
            // colSLMin
            // 
            this.colSLMin.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSLMin.AppearanceHeader.Options.UseFont = true;
            this.colSLMin.Caption = "Số lượng Min";
            this.colSLMin.FieldName = "SLMin";
            this.colSLMin.Name = "colSLMin";
            this.colSLMin.OptionsColumn.AllowEdit = false;
            this.colSLMin.OptionsColumn.ReadOnly = true;
            this.colSLMin.Visible = true;
            this.colSLMin.VisibleIndex = 3;
            // 
            // colCL
            // 
            this.colCL.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCL.AppearanceHeader.Options.UseFont = true;
            this.colCL.Caption = "Chênh lệch";
            this.colCL.FieldName = "CL";
            this.colCL.Name = "colCL";
            this.colCL.OptionsColumn.AllowEdit = false;
            this.colCL.OptionsColumn.ReadOnly = true;
            this.colCL.Visible = true;
            this.colCL.VisibleIndex = 4;
            // 
            // colMaDV
            // 
            this.colMaDV.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMaDV.AppearanceHeader.Options.UseFont = true;
            this.colMaDV.Caption = "Mã dịch vụ";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.AllowEdit = false;
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Xóa";
            this.gridColumn1.ColumnEdit = this.hplXoa;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            // 
            // hplXoa
            // 
            this.hplXoa.AutoHeight = false;
            this.hplXoa.Caption = "Xóa";
            this.hplXoa.Name = "hplXoa";
            this.hplXoa.Click += new System.EventHandler(this.hplXoa_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grc_DichVu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 394);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.btnIn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 394);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 40);
            this.panel2.TabIndex = 2;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(38, 14);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(7, 16);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(19, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "SL:";
            // 
            // btnIn
            // 
            this.btnIn.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(315, 7);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 23);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // frm_BCSLTon_ChenhLech
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 434);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCSLTon_ChenhLech";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_BCSLTon_ChenhLech";
            this.Load += new System.EventHandler(this.frm_BCSLTon_ChenhLech_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grc_DichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplXoa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.Preview.PreviewBar previewBar3;
        private DevExpress.XtraPrinting.Preview.PreviewBar previewBar1;
        private DevExpress.XtraGrid.GridControl grc_DichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_DichVu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colSLT;
        private DevExpress.XtraGrid.Columns.GridColumn colSLMin;
        private DevExpress.XtraGrid.Columns.GridColumn colCL;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit hplXoa;
    }
}