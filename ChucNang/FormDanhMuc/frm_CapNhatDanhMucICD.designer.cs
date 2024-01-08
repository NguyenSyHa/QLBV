namespace QLBV.FormNhap
{
    partial class frm_CapNhatDanhMucICD
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
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupCB_TK = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenWHOe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenWHO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaICD = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.grc_MBICD = new DevExpress.XtraGrid.GridControl();
            this.grv_MBICD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDMB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThaiSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckStatus = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colMaBenh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenWHO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenWHOe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupCB = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.DS_MaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.http_Xoa = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.binMBICD = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB_TK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenWHOe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenWHO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_MBICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_MBICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.http_Xoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binMBICD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1188, 120);
            this.panel2.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lupCB_TK);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtSTT);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtTenWHOe);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtTenWHO);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtMaICD);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1184, 110);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 15);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Mã ICD:";
            // 
            // lupCB_TK
            // 
            this.lupCB_TK.Location = new System.Drawing.Point(317, 74);
            this.lupCB_TK.Name = "lupCB_TK";
            this.lupCB_TK.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupCB_TK.Properties.Appearance.Options.UseFont = true;
            this.lupCB_TK.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCB_TK.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TENCB", "Chương bệnh")});
            this.lupCB_TK.Properties.DisplayMember = "TENCB";
            this.lupCB_TK.Properties.NullText = "";
            this.lupCB_TK.Properties.ValueMember = "IDCB";
            this.lupCB_TK.Size = new System.Drawing.Size(499, 22);
            this.lupCB_TK.TabIndex = 12;
            this.lupCB_TK.EditValueChanged += new System.EventHandler(this.lupCB_TK_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(230, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 15);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Tên WHO:";
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(84, 45);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Properties.Appearance.Options.UseFont = true;
            this.txtSTT.Properties.Mask.EditMask = "n";
            this.txtSTT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSTT.Size = new System.Drawing.Size(112, 22);
            this.txtSTT.TabIndex = 11;
            this.txtSTT.EditValueChanged += new System.EventHandler(this.txtSTT_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(230, 49);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 15);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Tên WHOe:";
            // 
            // txtTenWHOe
            // 
            this.txtTenWHOe.Location = new System.Drawing.Point(317, 46);
            this.txtTenWHOe.Name = "txtTenWHOe";
            this.txtTenWHOe.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenWHOe.Properties.Appearance.Options.UseFont = true;
            this.txtTenWHOe.Size = new System.Drawing.Size(499, 22);
            this.txtTenWHOe.TabIndex = 10;
            this.txtTenWHOe.EditValueChanged += new System.EventHandler(this.txtTenWHOe_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(230, 78);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 15);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Chương bệnh:";
            // 
            // txtTenWHO
            // 
            this.txtTenWHO.Location = new System.Drawing.Point(317, 18);
            this.txtTenWHO.Name = "txtTenWHO";
            this.txtTenWHO.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenWHO.Properties.Appearance.Options.UseFont = true;
            this.txtTenWHO.Size = new System.Drawing.Size(499, 22);
            this.txtTenWHO.TabIndex = 9;
            this.txtTenWHO.EditValueChanged += new System.EventHandler(this.txtTenWHO_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(5, 49);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 15);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "STT:";
            // 
            // txtMaICD
            // 
            this.txtMaICD.Location = new System.Drawing.Point(84, 17);
            this.txtMaICD.Name = "txtMaICD";
            this.txtMaICD.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaICD.Properties.Appearance.Options.UseFont = true;
            this.txtMaICD.Size = new System.Drawing.Size(112, 22);
            this.txtMaICD.TabIndex = 8;
            this.txtMaICD.EditValueChanged += new System.EventHandler(this.txtMaICD_EditValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 492);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1188, 38);
            this.panelControl2.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.grc_MBICD);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 120);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1188, 372);
            this.panelControl3.TabIndex = 6;
            // 
            // grc_MBICD
            // 
            this.grc_MBICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_MBICD.Location = new System.Drawing.Point(0, 0);
            this.grc_MBICD.MainView = this.grv_MBICD;
            this.grc_MBICD.Name = "grc_MBICD";
            this.grc_MBICD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupCB,
            this.ckStatus,
            this.http_Xoa});
            this.grc_MBICD.Size = new System.Drawing.Size(1188, 372);
            this.grc_MBICD.TabIndex = 0;
            this.grc_MBICD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_MBICD});
            // 
            // grv_MBICD
            // 
            this.grv_MBICD.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grv_MBICD.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_MBICD.Appearance.Row.Options.UseTextOptions = true;
            this.grv_MBICD.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.grv_MBICD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDMB,
            this.colTrangThaiSD,
            this.colMaBenh,
            this.colTenWHO,
            this.colTenWHOe,
            this.colSTT,
            this.colTenCB,
            this.DS_MaICD,
            this.colXoa});
            this.grv_MBICD.GridControl = this.grc_MBICD;
            this.grv_MBICD.Name = "grv_MBICD";
            this.grv_MBICD.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grv_MBICD.OptionsView.EnableAppearanceEvenRow = true;
            this.grv_MBICD.OptionsView.EnableAppearanceOddRow = true;
            this.grv_MBICD.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grv_MBICD.OptionsView.ShowGroupPanel = false;
            this.grv_MBICD.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grv_MBICD_RowCellClick);
            this.grv_MBICD.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grv_MBICD_ValidateRow);
            // 
            // colIDMB
            // 
            this.colIDMB.Caption = "ID";
            this.colIDMB.FieldName = "ID_MBICD";
            this.colIDMB.Name = "colIDMB";
            // 
            // colTrangThaiSD
            // 
            this.colTrangThaiSD.Caption = "Trạng thái sd";
            this.colTrangThaiSD.ColumnEdit = this.ckStatus;
            this.colTrangThaiSD.FieldName = "STATUS";
            this.colTrangThaiSD.Name = "colTrangThaiSD";
            this.colTrangThaiSD.Visible = true;
            this.colTrangThaiSD.VisibleIndex = 0;
            this.colTrangThaiSD.Width = 66;
            // 
            // ckStatus
            // 
            this.ckStatus.AutoHeight = false;
            this.ckStatus.Caption = "Check";
            this.ckStatus.Name = "ckStatus";
            // 
            // colMaBenh
            // 
            this.colMaBenh.Caption = "Mã ICD";
            this.colMaBenh.FieldName = "MaICD";
            this.colMaBenh.Name = "colMaBenh";
            this.colMaBenh.Visible = true;
            this.colMaBenh.VisibleIndex = 4;
            this.colMaBenh.Width = 134;
            // 
            // colTenWHO
            // 
            this.colTenWHO.Caption = "Tên WHO";
            this.colTenWHO.FieldName = "TenWHO";
            this.colTenWHO.Name = "colTenWHO";
            this.colTenWHO.Visible = true;
            this.colTenWHO.VisibleIndex = 2;
            this.colTenWHO.Width = 269;
            // 
            // colTenWHOe
            // 
            this.colTenWHOe.Caption = "Tên WHOe";
            this.colTenWHOe.FieldName = "TenWHOe";
            this.colTenWHOe.Name = "colTenWHOe";
            this.colTenWHOe.Visible = true;
            this.colTenWHOe.VisibleIndex = 3;
            this.colTenWHOe.Width = 269;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.Caption = "STT";
            this.colSTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 53;
            // 
            // colTenCB
            // 
            this.colTenCB.Caption = "Tên chương bệnh";
            this.colTenCB.ColumnEdit = this.lupCB;
            this.colTenCB.FieldName = "IDCB";
            this.colTenCB.Name = "colTenCB";
            this.colTenCB.Visible = true;
            this.colTenCB.VisibleIndex = 5;
            this.colTenCB.Width = 132;
            // 
            // lupCB
            // 
            this.lupCB.AutoHeight = false;
            this.lupCB.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupCB.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TENCB", "Chương bệnh")});
            this.lupCB.DisplayMember = "TENCB";
            this.lupCB.Name = "lupCB";
            this.lupCB.NullText = "";
            this.lupCB.ValueMember = "IDCB";
            // 
            // DS_MaICD
            // 
            this.DS_MaICD.Caption = "Danh sách Mã ICD";
            this.DS_MaICD.FieldName = "DS_MaICD";
            this.DS_MaICD.Name = "DS_MaICD";
            this.DS_MaICD.Visible = true;
            this.DS_MaICD.VisibleIndex = 6;
            this.DS_MaICD.Width = 194;
            // 
            // colXoa
            // 
            this.colXoa.Caption = "Xóa";
            this.colXoa.ColumnEdit = this.http_Xoa;
            this.colXoa.Name = "colXoa";
            this.colXoa.OptionsColumn.AllowEdit = false;
            this.colXoa.OptionsColumn.ReadOnly = true;
            this.colXoa.Visible = true;
            this.colXoa.VisibleIndex = 7;
            this.colXoa.Width = 53;
            // 
            // http_Xoa
            // 
            this.http_Xoa.AllowFocused = false;
            this.http_Xoa.AutoHeight = false;
            this.http_Xoa.Caption = "Xóa";
            this.http_Xoa.Name = "http_Xoa";
            this.http_Xoa.NullText = "Xóa";
            // 
            // frm_CapNhatDanhMucICD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 530);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel2);
            this.Name = "frm_CapNhatDanhMucICD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật danh mục ICD _ Chương bệnh ";
            this.Load += new System.EventHandler(this.frm_CapNhatDanhMucICD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB_TK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenWHOe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenWHO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_MBICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_MBICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupCB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.http_Xoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binMBICD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl grc_MBICD;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_MBICD;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMB;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBenh;
        private DevExpress.XtraGrid.Columns.GridColumn colTenWHO;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCB;
        private System.Windows.Forms.BindingSource binMBICD;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupCB;
        private DevExpress.XtraGrid.Columns.GridColumn colTenWHOe;
        private DevExpress.XtraEditors.LookUpEdit lupCB_TK;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraEditors.TextEdit txtTenWHOe;
        private DevExpress.XtraEditors.TextEdit txtTenWHO;
        private DevExpress.XtraEditors.TextEdit txtMaICD;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThaiSD;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckStatus;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.Columns.GridColumn DS_MaICD;
        private DevExpress.XtraGrid.Columns.GridColumn colXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit http_Xoa;
    }
}