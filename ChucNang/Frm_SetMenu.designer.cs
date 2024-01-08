namespace QLBV.FormThamSo
{
    partial class Frm_SetMenu
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.GrcMenu = new DevExpress.XtraGrid.GridControl();
            this.GrvMenu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colNhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboNhom = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnBanDau = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtTimkiem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrcMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(792, 573);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl3);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(788, 569);
            this.groupControl1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.GrcMenu);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 20);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(784, 493);
            this.panelControl3.TabIndex = 2;
            // 
            // GrcMenu
            // 
            this.GrcMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrcMenu.Location = new System.Drawing.Point(0, 0);
            this.GrcMenu.MainView = this.GrvMenu;
            this.GrcMenu.Name = "GrcMenu";
            this.GrcMenu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboNhom,
            this.repositoryItemHyperLinkEdit1});
            this.GrcMenu.Size = new System.Drawing.Size(784, 493);
            this.GrcMenu.TabIndex = 0;
            this.GrcMenu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrvMenu});
            // 
            // GrvMenu
            // 
            this.GrvMenu.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.GrvMenu.Appearance.Row.Options.UseFont = true;
            this.GrvMenu.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.GrvMenu.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.GrvMenu.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.GrvMenu.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.GrvMenu.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrvMenu.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.GrvMenu.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrvMenu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTenBC,
            this.colLoai,
            this.colSua,
            this.colNhom,
            this.colStatus});
            this.GrvMenu.GridControl = this.GrcMenu;
            this.GrvMenu.Name = "GrvMenu";
            this.GrvMenu.OptionsView.AutoCalcPreviewLineCount = true;
            this.GrvMenu.OptionsView.EnableAppearanceOddRow = true;
            this.GrvMenu.OptionsView.ShowGroupPanel = false;
            this.GrvMenu.OptionsView.ShowViewCaption = true;
            this.GrvMenu.ViewCaption = "Danh sách báo cáo";
            this.GrvMenu.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GrvMenu_RowClick);
            this.GrvMenu.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GrvMenu_RowCellClick);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowFocus = false;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 61;
            // 
            // colTenBC
            // 
            this.colTenBC.Caption = "Tên BC";
            this.colTenBC.FieldName = "TenBC";
            this.colTenBC.Name = "colTenBC";
            this.colTenBC.Visible = true;
            this.colTenBC.VisibleIndex = 1;
            this.colTenBC.Width = 396;
            // 
            // colLoai
            // 
            this.colLoai.Caption = "Loại BC";
            this.colLoai.FieldName = "Loai";
            this.colLoai.Name = "colLoai";
            this.colLoai.OptionsColumn.AllowEdit = false;
            this.colLoai.OptionsColumn.AllowFocus = false;
            this.colLoai.Visible = true;
            this.colLoai.VisibleIndex = 2;
            this.colLoai.Width = 169;
            // 
            // colSua
            // 
            this.colSua.Caption = "Sửa";
            this.colSua.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colSua.Name = "colSua";
            this.colSua.OptionsColumn.AllowEdit = false;
            this.colSua.Visible = true;
            this.colSua.VisibleIndex = 3;
            this.colSua.Width = 82;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.NullText = "Sửa loại BC";
            // 
            // colNhom
            // 
            this.colNhom.Caption = "Nhóm BC";
            this.colNhom.ColumnEdit = this.cboNhom;
            this.colNhom.FieldName = "Nhom";
            this.colNhom.Name = "colNhom";
            this.colNhom.Visible = true;
            this.colNhom.VisibleIndex = 4;
            this.colNhom.Width = 222;
            // 
            // cboNhom
            // 
            this.cboNhom.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cboNhom.AutoHeight = false;
            this.cboNhom.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNhom.Name = "cboNhom";
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Hiển thị";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 5;
            this.colStatus.Width = 114;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 513);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(784, 54);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnBanDau);
            this.groupControl2.Controls.Add(this.simpleButton2);
            this.groupControl2.Controls.Add(this.simpleButton1);
            this.groupControl2.Controls.Add(this.txtTimkiem);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(784, 54);
            this.groupControl2.TabIndex = 1;
            // 
            // btnBanDau
            // 
            this.btnBanDau.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBanDau.Appearance.Options.UseFont = true;
            this.btnBanDau.Location = new System.Drawing.Point(477, 26);
            this.btnBanDau.Name = "btnBanDau";
            this.btnBanDau.Size = new System.Drawing.Size(108, 23);
            this.btnBanDau.TabIndex = 4;
            this.btnBanDau.Text = "Thiết lập ban đầu";
            this.btnBanDau.Click += new System.EventHandler(this.btnBanDau_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(683, 26);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(86, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "&Lưu";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(591, 26);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "&Tìm kiếm";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Location = new System.Drawing.Point(72, 21);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimkiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimkiem.Properties.NullText = "Nhập ID|Tên báo cáo";
            this.txtTimkiem.Size = new System.Drawing.Size(271, 22);
            this.txtTimkiem.TabIndex = 1;
            this.txtTimkiem.EditValueChanged += new System.EventHandler(this.txtTimkiem_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(10, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tìm kiếm:";
            // 
            // Frm_SetMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SetMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập các báo cáo";
            this.Load += new System.EventHandler(this.Frm_SetMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrcMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl GrcMenu;
        private DevExpress.XtraGrid.Views.Grid.GridView GrvMenu;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBC;
        private DevExpress.XtraGrid.Columns.GridColumn colLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colNhom;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboNhom;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colSua;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtTimkiem;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnBanDau;
    }
}