namespace QLBV.FormThamSo
{
    partial class frm_BCHX_27022
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
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.btnthoat = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.grcKho = new DevExpress.XtraGrid.GridControl();
            this.grvKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTenKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPPXuat = new DevExpress.XtraGrid.GridControl();
            this.grvPPXuat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Check = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.PhanLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcPPXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPPXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Location = new System.Drawing.Point(165, 274);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(154, 42);
            this.btntaobc.TabIndex = 3;
            this.btntaobc.Text = "Tạo Báo Cáo";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // btnthoat
            // 
            this.btnthoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthoat.Appearance.Options.UseFont = true;
            this.btnthoat.Location = new System.Drawing.Point(325, 274);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(156, 42);
            this.btnthoat.TabIndex = 4;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(3, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(332, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(69, 11);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(250, 26);
            this.detungay.TabIndex = 0;
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(398, 11);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(250, 26);
            this.dedenngay.TabIndex = 1;
            // 
            // grcKho
            // 
            this.grcKho.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcKho.Location = new System.Drawing.Point(3, 44);
            this.grcKho.MainView = this.grvKho;
            this.grcKho.Margin = new System.Windows.Forms.Padding(4);
            this.grcKho.Name = "grcKho";
            this.grcKho.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.grcKho.Size = new System.Drawing.Size(316, 223);
            this.grcKho.TabIndex = 2;
            this.grcKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKho});
            // 
            // grvKho
            // 
            this.grvKho.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvKho.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvKho.Appearance.ViewCaption.Options.UseFont = true;
            this.grvKho.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.grvKho.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvKho.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenKho,
            this.colMaKP});
            this.grvKho.GridControl = this.grcKho;
            this.grvKho.Name = "grvKho";
            this.grvKho.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKho.OptionsView.ShowGroupPanel = false;
            this.grvKho.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMaKP, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvKho.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKho_CellValueChanged);
            this.grvKho.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKho_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colChon.FieldName = "Check";
            this.colChon.MinWidth = 10;
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 140;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Check";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colTenKho
            // 
            this.colTenKho.Caption = "Kho dược";
            this.colTenKho.FieldName = "TenKP";
            this.colTenKho.Name = "colTenKho";
            this.colTenKho.OptionsColumn.AllowEdit = false;
            this.colTenKho.Visible = true;
            this.colTenKho.VisibleIndex = 1;
            this.colTenKho.Width = 556;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "Mã KP";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            // 
            // grcPPXuat
            // 
            this.grcPPXuat.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcPPXuat.Location = new System.Drawing.Point(327, 44);
            this.grcPPXuat.MainView = this.grvPPXuat;
            this.grcPPXuat.Margin = new System.Windows.Forms.Padding(4);
            this.grcPPXuat.Name = "grcPPXuat";
            this.grcPPXuat.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcPPXuat.Size = new System.Drawing.Size(321, 223);
            this.grcPPXuat.TabIndex = 5;
            this.grcPPXuat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPPXuat});
            // 
            // grvPPXuat
            // 
            this.grvPPXuat.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvPPXuat.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvPPXuat.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvPPXuat.Appearance.ViewCaption.Options.UseFont = true;
            this.grvPPXuat.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvPPXuat.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvPPXuat.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Check,
            this.PhanLoai,
            this.colID});
            this.grvPPXuat.GridControl = this.grcPPXuat;
            this.grvPPXuat.Name = "grvPPXuat";
            this.grvPPXuat.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvPPXuat.OptionsView.ShowGroupPanel = false;
            this.grvPPXuat.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvPPXuat.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvPPXuat_CellValueChanging_1);
            // 
            // Check
            // 
            this.Check.Caption = "Chọn";
            this.Check.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Check.FieldName = "Check";
            this.Check.MinWidth = 10;
            this.Check.Name = "Check";
            this.Check.Visible = true;
            this.Check.VisibleIndex = 0;
            this.Check.Width = 55;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // PhanLoai
            // 
            this.PhanLoai.Caption = "Phân loại xuất";
            this.PhanLoai.FieldName = "PhanLoai";
            this.PhanLoai.Name = "PhanLoai";
            this.PhanLoai.OptionsColumn.AllowEdit = false;
            this.PhanLoai.Visible = true;
            this.PhanLoai.VisibleIndex = 1;
            this.PhanLoai.Width = 373;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "Id";
            this.colID.Name = "colID";
            // 
            // frm_BCHX_27022
            // 
            this.AcceptButton = this.btntaobc;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 318);
            this.Controls.Add(this.grcPPXuat);
            this.Controls.Add(this.grcKho);
            this.Controls.Add(this.dedenngay);
            this.Controls.Add(this.detungay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btntaobc);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCHX_27022";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO HÀNG XUẤT";
            this.Load += new System.EventHandler(this.frm_test1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcPPXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPPXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.SimpleButton btnthoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraGrid.GridControl grcKho;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKho;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKho;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraGrid.GridControl grcPPXuat;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPPXuat;
        private DevExpress.XtraGrid.Columns.GridColumn Check;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn PhanLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colID;

    }
}