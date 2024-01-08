namespace QLBV.ChucNang
{
    partial class frm_DSTamUng_TCTT_ct
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
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.chk_Khoa = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl10 = new DevExpress.XtraEditors.GroupControl();
            this.grcTamUngct = new DevExpress.XtraGrid.GridControl();
            this.grvTamUngCt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.madv_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lup_TenDVtu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.DonVi_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonGia_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoLuong_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ThanhTien_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TrongBH_tu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lup_TrongDMtu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Khoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).BeginInit();
            this.groupControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcTamUngct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTamUngCt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_TenDVtu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_TrongDMtu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit6)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Controls.Add(this.chk_Khoa);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(736, 42);
            this.panelControl1.TabIndex = 0;
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Location = new System.Drawing.Point(101, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // chk_Khoa
            // 
            this.chk_Khoa.Location = new System.Drawing.Point(12, 5);
            this.chk_Khoa.Name = "chk_Khoa";
            this.chk_Khoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_Khoa.Properties.Appearance.Options.UseFont = true;
            this.chk_Khoa.Properties.Caption = "Khóa:";
            this.chk_Khoa.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chk_Khoa.Size = new System.Drawing.Size(63, 19);
            this.chk_Khoa.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl10);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 42);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(736, 355);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl10
            // 
            this.groupControl10.Appearance.BackColor = System.Drawing.Color.Silver;
            this.groupControl10.Appearance.Options.UseBackColor = true;
            this.groupControl10.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupControl10.AppearanceCaption.Options.UseFont = true;
            this.groupControl10.Controls.Add(this.grcTamUngct);
            this.groupControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl10.Location = new System.Drawing.Point(2, 2);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.Size = new System.Drawing.Size(732, 351);
            this.groupControl10.TabIndex = 4;
            this.groupControl10.Text = "Chi phí thu thẳng";
            // 
            // grcTamUngct
            // 
            this.grcTamUngct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcTamUngct.Location = new System.Drawing.Point(2, 20);
            this.grcTamUngct.MainView = this.grvTamUngCt;
            this.grcTamUngct.Name = "grcTamUngct";
            this.grcTamUngct.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lup_TenDVtu,
            this.lup_TrongDMtu,
            this.repositoryItemLookUpEdit6,
            this.repositoryItemHyperLinkEdit4,
            this.repositoryItemLookUpEdit5});
            this.grcTamUngct.Size = new System.Drawing.Size(728, 329);
            this.grcTamUngct.TabIndex = 59;
            this.grcTamUngct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTamUngCt});
            // 
            // grvTamUngCt
            // 
            this.grvTamUngCt.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvTamUngCt.Appearance.FooterPanel.Options.UseFont = true;
            this.grvTamUngCt.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvTamUngCt.Appearance.GroupFooter.Options.UseFont = true;
            this.grvTamUngCt.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvTamUngCt.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grvTamUngCt.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvTamUngCt.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvTamUngCt.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvTamUngCt.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvTamUngCt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.madv_tu,
            this.DonVi_tu,
            this.DonGia_tu,
            this.SoLuong_tu,
            this.ThanhTien_tu,
            this.SoTien,
            this.TrongBH_tu,
            this.gridColumn7,
            this.gridColumn8});
            this.grvTamUngCt.GridControl = this.grcTamUngct;
            this.grvTamUngCt.Name = "grvTamUngCt";
            this.grvTamUngCt.OptionsBehavior.Editable = false;
            this.grvTamUngCt.OptionsBehavior.ReadOnly = true;
            this.grvTamUngCt.OptionsView.EnableAppearanceEvenRow = true;
            this.grvTamUngCt.OptionsView.EnableAppearanceOddRow = true;
            this.grvTamUngCt.OptionsView.ShowFooter = true;
            this.grvTamUngCt.OptionsView.ShowGroupPanel = false;
            // 
            // madv_tu
            // 
            this.madv_tu.Caption = "Tên chi phí";
            this.madv_tu.ColumnEdit = this.lup_TenDVtu;
            this.madv_tu.FieldName = "MaDV";
            this.madv_tu.Name = "madv_tu";
            this.madv_tu.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.madv_tu.Visible = true;
            this.madv_tu.VisibleIndex = 0;
            this.madv_tu.Width = 254;
            // 
            // lup_TenDVtu
            // 
            this.lup_TenDVtu.AutoHeight = false;
            this.lup_TenDVtu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_TenDVtu.DisplayMember = "TenDV";
            this.lup_TenDVtu.Name = "lup_TenDVtu";
            this.lup_TenDVtu.NullText = "";
            this.lup_TenDVtu.ValueMember = "MaDV";
            // 
            // DonVi_tu
            // 
            this.DonVi_tu.AppearanceCell.Options.UseTextOptions = true;
            this.DonVi_tu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DonVi_tu.Caption = "Đơn vị";
            this.DonVi_tu.FieldName = "DonVi";
            this.DonVi_tu.Name = "DonVi_tu";
            this.DonVi_tu.Visible = true;
            this.DonVi_tu.VisibleIndex = 1;
            this.DonVi_tu.Width = 42;
            // 
            // DonGia_tu
            // 
            this.DonGia_tu.AppearanceCell.Options.UseTextOptions = true;
            this.DonGia_tu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DonGia_tu.Caption = "Đơn giá";
            this.DonGia_tu.DisplayFormat.FormatString = "##,###.000";
            this.DonGia_tu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.DonGia_tu.FieldName = "DonGia";
            this.DonGia_tu.Name = "DonGia_tu";
            this.DonGia_tu.Visible = true;
            this.DonGia_tu.VisibleIndex = 3;
            this.DonGia_tu.Width = 80;
            // 
            // SoLuong_tu
            // 
            this.SoLuong_tu.AppearanceCell.Options.UseTextOptions = true;
            this.SoLuong_tu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.SoLuong_tu.Caption = "Số lượng";
            this.SoLuong_tu.DisplayFormat.FormatString = "##,###";
            this.SoLuong_tu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.SoLuong_tu.FieldName = "SoLuong";
            this.SoLuong_tu.Name = "SoLuong_tu";
            this.SoLuong_tu.Visible = true;
            this.SoLuong_tu.VisibleIndex = 4;
            this.SoLuong_tu.Width = 71;
            // 
            // ThanhTien_tu
            // 
            this.ThanhTien_tu.AppearanceCell.Options.UseTextOptions = true;
            this.ThanhTien_tu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ThanhTien_tu.Caption = "thành tiền";
            this.ThanhTien_tu.DisplayFormat.FormatString = "##,###.000";
            this.ThanhTien_tu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.ThanhTien_tu.FieldName = "ThanhTien";
            this.ThanhTien_tu.Name = "ThanhTien_tu";
            this.ThanhTien_tu.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", "{0:##,###.##}")});
            this.ThanhTien_tu.Visible = true;
            this.ThanhTien_tu.VisibleIndex = 5;
            this.ThanhTien_tu.Width = 102;
            // 
            // SoTien
            // 
            this.SoTien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.SoTien.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.SoTien.AppearanceCell.Options.UseFont = true;
            this.SoTien.AppearanceCell.Options.UseForeColor = true;
            this.SoTien.Caption = "Số tiền thu";
            this.SoTien.DisplayFormat.FormatString = "##,###.000";
            this.SoTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.SoTien.FieldName = "SoTien";
            this.SoTien.Name = "SoTien";
            this.SoTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SoTien", "{0:##,###.##}")});
            this.SoTien.Visible = true;
            this.SoTien.VisibleIndex = 6;
            this.SoTien.Width = 100;
            // 
            // TrongBH_tu
            // 
            this.TrongBH_tu.Caption = "Trong DMBH";
            this.TrongBH_tu.ColumnEdit = this.lup_TrongDMtu;
            this.TrongBH_tu.FieldName = "TrongBH";
            this.TrongBH_tu.Name = "TrongBH_tu";
            this.TrongBH_tu.Visible = true;
            this.TrongBH_tu.VisibleIndex = 2;
            this.TrongBH_tu.Width = 48;
            // 
            // lup_TrongDMtu
            // 
            this.lup_TrongDMtu.AutoHeight = false;
            this.lup_TrongDMtu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_TrongDMtu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten_TrongBH", "Trong DMBH"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TrongBH", "value")});
            this.lup_TrongDMtu.DisplayMember = "Ten_TrongBH";
            this.lup_TrongDMtu.Name = "lup_TrongDMtu";
            this.lup_TrongDMtu.NullText = "";
            this.lup_TrongDMtu.ValueMember = "TrongBH";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Sửa";
            this.gridColumn7.ColumnEdit = this.repositoryItemHyperLinkEdit4;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Width = 46;
            // 
            // repositoryItemHyperLinkEdit4
            // 
            this.repositoryItemHyperLinkEdit4.AutoHeight = false;
            this.repositoryItemHyperLinkEdit4.Caption = "Sửa";
            this.repositoryItemHyperLinkEdit4.Name = "repositoryItemHyperLinkEdit4";
            this.repositoryItemHyperLinkEdit4.NullText = "Sửa";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Khoa|Phòng";
            this.gridColumn8.ColumnEdit = this.repositoryItemLookUpEdit5;
            this.gridColumn8.FieldName = "MaKP";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 87;
            // 
            // repositoryItemLookUpEdit5
            // 
            this.repositoryItemLookUpEdit5.AutoHeight = false;
            this.repositoryItemLookUpEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit5.DisplayMember = "TenKP";
            this.repositoryItemLookUpEdit5.Name = "repositoryItemLookUpEdit5";
            this.repositoryItemLookUpEdit5.NullText = "";
            this.repositoryItemLookUpEdit5.ReadOnly = true;
            this.repositoryItemLookUpEdit5.ValueMember = "MaKP";
            // 
            // repositoryItemLookUpEdit6
            // 
            this.repositoryItemLookUpEdit6.AutoHeight = false;
            this.repositoryItemLookUpEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit6.Name = "repositoryItemLookUpEdit6";
            // 
            // frm_DSTamUng_TCTT_ct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 397);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DSTamUng_TCTT_ct";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_DSTamUng_TCTT_ct";
            this.Load += new System.EventHandler(this.frm_DSTamUng_TCTT_ct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_Khoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).EndInit();
            this.groupControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcTamUngct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTamUngCt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_TenDVtu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_TrongDMtu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl10;
        private DevExpress.XtraGrid.GridControl grcTamUngct;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTamUngCt;
        private DevExpress.XtraGrid.Columns.GridColumn madv_tu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lup_TenDVtu;
        private DevExpress.XtraGrid.Columns.GridColumn DonVi_tu;
        private DevExpress.XtraGrid.Columns.GridColumn DonGia_tu;
        private DevExpress.XtraGrid.Columns.GridColumn SoLuong_tu;
        private DevExpress.XtraGrid.Columns.GridColumn ThanhTien_tu;
        private DevExpress.XtraGrid.Columns.GridColumn SoTien;
        private DevExpress.XtraGrid.Columns.GridColumn TrongBH_tu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lup_TrongDMtu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit6;
        private DevExpress.XtraEditors.CheckEdit chk_Khoa;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
    }
}