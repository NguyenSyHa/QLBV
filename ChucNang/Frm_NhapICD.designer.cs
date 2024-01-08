namespace QLBV.FormThamSo
{
    partial class Frm_NhapICD
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcBenhNhan = new DevExpress.XtraGrid.GridControl();
            this.grvBenhNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LupMaICD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colChandoan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtTimkiem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sbtLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupMaICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcBenhNhan);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(558, 335);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Danh sách bệnh nhân";
            // 
            // grcBenhNhan
            // 
            this.grcBenhNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcBenhNhan.Location = new System.Drawing.Point(2, 21);
            this.grcBenhNhan.MainView = this.grvBenhNhan;
            this.grcBenhNhan.Name = "grcBenhNhan";
            this.grcBenhNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.LupMaICD});
            this.grcBenhNhan.Size = new System.Drawing.Size(554, 312);
            this.grcBenhNhan.TabIndex = 0;
            this.grcBenhNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBenhNhan});
            // 
            // grvBenhNhan
            // 
            this.grvBenhNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBN,
            this.colTenBN,
            this.colMaICD,
            this.colChandoan});
            this.grvBenhNhan.GridControl = this.grcBenhNhan;
            this.grvBenhNhan.Name = "grvBenhNhan";
            this.grvBenhNhan.OptionsView.ShowGroupPanel = false;
            // 
            // colMaBN
            // 
            this.colMaBN.Caption = "Mã bệnh nhân";
            this.colMaBN.FieldName = "MaBN";
            this.colMaBN.Name = "colMaBN";
            this.colMaBN.OptionsColumn.AllowEdit = false;
            this.colMaBN.Visible = true;
            this.colMaBN.VisibleIndex = 0;
            this.colMaBN.Width = 77;
            // 
            // colTenBN
            // 
            this.colTenBN.Caption = "Tên bệnh nhân";
            this.colTenBN.FieldName = "TenBN";
            this.colTenBN.Name = "colTenBN";
            this.colTenBN.OptionsColumn.AllowEdit = false;
            this.colTenBN.OptionsColumn.AllowFocus = false;
            this.colTenBN.Visible = true;
            this.colTenBN.VisibleIndex = 1;
            this.colTenBN.Width = 194;
            // 
            // colMaICD
            // 
            this.colMaICD.Caption = "Mã ICD";
            this.colMaICD.ColumnEdit = this.LupMaICD;
            this.colMaICD.FieldName = "MaICD";
            this.colMaICD.Name = "colMaICD";
            this.colMaICD.Visible = true;
            this.colMaICD.VisibleIndex = 2;
            this.colMaICD.Width = 65;
            // 
            // LupMaICD
            // 
            this.LupMaICD.AutoHeight = false;
            this.LupMaICD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupMaICD.DisplayMember = "MaICD";
            this.LupMaICD.Name = "LupMaICD";
            this.LupMaICD.NullText = "";
            this.LupMaICD.ValueMember = "MaICD";
            // 
            // colChandoan
            // 
            this.colChandoan.Caption = "Chẩn đoán";
            this.colChandoan.FieldName = "ChanDoan";
            this.colChandoan.Name = "colChandoan";
            this.colChandoan.OptionsColumn.AllowEdit = false;
            this.colChandoan.OptionsColumn.AllowFocus = false;
            this.colChandoan.Visible = true;
            this.colChandoan.VisibleIndex = 3;
            this.colChandoan.Width = 202;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtTimkiem);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.sbtLuu);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 335);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(558, 82);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Công cụ";
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Location = new System.Drawing.Point(62, 35);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(174, 20);
            this.txtTimkiem.TabIndex = 2;
            this.txtTimkiem.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Tìm kiếm:";
            // 
            // sbtLuu
            // 
            this.sbtLuu.Location = new System.Drawing.Point(302, 32);
            this.sbtLuu.Name = "sbtLuu";
            this.sbtLuu.Size = new System.Drawing.Size(121, 23);
            this.sbtLuu.TabIndex = 0;
            this.sbtLuu.Text = "Lưu và thoát";
            this.sbtLuu.Click += new System.EventHandler(this.sbtLuu_Click);
            // 
            // Frm_NhapICD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 417);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NhapICD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập mã ICD";
            this.Load += new System.EventHandler(this.Frm_NhapICD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupMaICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcBenhNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBenhNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaICD;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit LupMaICD;
        private DevExpress.XtraGrid.Columns.GridColumn colChandoan;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtTimkiem;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtLuu;
    }
}