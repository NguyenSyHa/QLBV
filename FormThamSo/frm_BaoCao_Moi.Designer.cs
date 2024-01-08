
namespace QLBV.FormThamSo
{
    partial class frm_BaoCao_Moi
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.grcKPhong = new DevExpress.XtraGrid.GridControl();
            this.grvKPhong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(310, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 23);
            this.btnCancel.TabIndex = 137;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(208, 259);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 23);
            this.btnOK.TabIndex = 135;
            this.btnOK.Text = "Tạo báo cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(200, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 16);
            this.labelControl2.TabIndex = 138;
            this.labelControl2.Text = "Ngày đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(3, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 16);
            this.labelControl1.TabIndex = 136;
            this.labelControl1.Text = "Ngày từ:";
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(272, 9);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgayden.Properties.Appearance.Options.UseFont = true;
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Size = new System.Drawing.Size(131, 22);
            this.lupNgayden.TabIndex = 134;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(66, 9);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(123, 22);
            this.lupNgaytu.TabIndex = 133;
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.grcKPhong);
            this.groupControl3.Location = new System.Drawing.Point(3, 34);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(400, 219);
            this.groupControl3.TabIndex = 139;
            // 
            // grcKPhong
            // 
            this.grcKPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKPhong.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcKPhong.Location = new System.Drawing.Point(2, 22);
            this.grcKPhong.MainView = this.grvKPhong;
            this.grcKPhong.Margin = new System.Windows.Forms.Padding(4);
            this.grcKPhong.Name = "grcKPhong";
            this.grcKPhong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit3});
            this.grcKPhong.Size = new System.Drawing.Size(396, 195);
            this.grcKPhong.TabIndex = 117;
            this.grcKPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKPhong});
            // 
            // grvKPhong
            // 
            this.grvKPhong.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvKPhong.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvKPhong.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvKPhong.Appearance.ViewCaption.Options.UseFont = true;
            this.grvKPhong.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvKPhong.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvKPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenKP,
            this.colMaKP});
            this.grvKPhong.GridControl = this.grcKPhong;
            this.grvKPhong.Name = "grvKPhong";
            this.grvKPhong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKPhong.OptionsView.ShowGroupPanel = false;
            this.grvKPhong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKPhong_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colChon.FieldName = "Check";
            this.colChon.MinWidth = 10;
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 55;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Caption = "Check";
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // colTenKP
            // 
            this.colTenKP.Caption = "Khoa|Phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.OptionsColumn.AllowEdit = false;
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            this.colTenKP.Width = 373;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "Mã KP";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            // 
            // frm_BaoCao_Moi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 289);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupNgayden);
            this.Controls.Add(this.lupNgaytu);
            this.Name = "frm_BaoCao_Moi";
            this.Text = "Báo cáo mới";
            this.Load += new System.EventHandler(this.frm_BaoCao_Moi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit lupNgayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl grcKPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
    }
}