namespace QLBV.ChucNang
{
    partial class frm_UpdateMaCC
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaDuocMoi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnThemMoi = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaDV = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenDV = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.lupMaCC = new DevExpress.XtraEditors.LookUpEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupTenCC = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDuocMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenCC)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.txtMaDuocMoi);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnThemMoi);
            this.panelControl1.Controls.Add(this.txtMaDV);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txtTenDV);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl24);
            this.panelControl1.Controls.Add(this.lupMaCC);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(690, 95);
            this.panelControl1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(31, 57);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(145, 23);
            this.simpleButton1.TabIndex = 60;
            this.simpleButton1.Text = "Cắt khoảng trắng";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtMaDuocMoi
            // 
            this.txtMaDuocMoi.EnterMoveNextControl = true;
            this.txtMaDuocMoi.Location = new System.Drawing.Point(493, 32);
            this.txtMaDuocMoi.Name = "txtMaDuocMoi";
            this.txtMaDuocMoi.Properties.MaxLength = 10;
            this.txtMaDuocMoi.Size = new System.Drawing.Size(145, 20);
            this.txtMaDuocMoi.TabIndex = 58;
            this.txtMaDuocMoi.Leave += new System.EventHandler(this.txtMaDuocMoi_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(389, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "Mã dược  mới*:";
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Enabled = false;
            this.btnThemMoi.Location = new System.Drawing.Point(493, 66);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(145, 23);
            this.btnThemMoi.TabIndex = 57;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // txtMaDV
            // 
            this.txtMaDV.Location = new System.Drawing.Point(493, 5);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Properties.ReadOnly = true;
            this.txtMaDV.Size = new System.Drawing.Size(145, 20);
            this.txtMaDV.TabIndex = 55;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(389, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(58, 13);
            this.labelControl6.TabIndex = 56;
            this.labelControl6.Text = "Mã dược*:";
            // 
            // txtTenDV
            // 
            this.txtTenDV.Location = new System.Drawing.Point(107, 5);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Properties.ReadOnly = true;
            this.txtTenDV.Size = new System.Drawing.Size(275, 20);
            this.txtTenDV.TabIndex = 53;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(10, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 54;
            this.labelControl2.Text = "Tên Dược*";
            // 
            // labelControl24
            // 
            this.labelControl24.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl24.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.labelControl24.Location = new System.Drawing.Point(10, 33);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(86, 14);
            this.labelControl24.TabIndex = 52;
            this.labelControl24.Text = "Nhà cung cấp:";
            // 
            // lupMaCC
            // 
            this.lupMaCC.EnterMoveNextControl = true;
            this.lupMaCC.Location = new System.Drawing.Point(107, 31);
            this.lupMaCC.Name = "lupMaCC";
            this.lupMaCC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaCC.Properties.Appearance.Options.UseFont = true;
            this.lupMaCC.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupMaCC.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lupMaCC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaCC.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", 200, "Tên nhà cung cấp"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCC", "Name4", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NguoiCC", 150, "Người giao")});
            this.lupMaCC.Properties.DisplayMember = "TenCC";
            this.lupMaCC.Properties.NullText = "";
            this.lupMaCC.Properties.PopupFormMinSize = new System.Drawing.Size(350, 200);
            this.lupMaCC.Properties.ReadOnly = true;
            this.lupMaCC.Properties.ValueMember = "MaCC";
            this.lupMaCC.Size = new System.Drawing.Size(275, 20);
            this.lupMaCC.TabIndex = 51;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(387, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(199, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Update MaCC vào dịch vụ";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(31, 0);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Chưa có MaCC"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Đã có MaCC"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radioGroup1.Size = new System.Drawing.Size(284, 41);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.radioGroup1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 475);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(690, 46);
            this.panelControl2.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gridControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 95);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(690, 380);
            this.panelControl3.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupTenCC});
            this.gridControl1.Size = new System.Drawing.Size(686, 376);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenDV,
            this.colMaCC,
            this.colTenCC});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã dược";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.OptionsColumn.ReadOnly = true;
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên Dược";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 237;
            // 
            // colMaCC
            // 
            this.colMaCC.Caption = "Mã CC";
            this.colMaCC.FieldName = "MaCC";
            this.colMaCC.Name = "colMaCC";
            this.colMaCC.Visible = true;
            this.colMaCC.VisibleIndex = 2;
            // 
            // colTenCC
            // 
            this.colTenCC.Caption = "Tên nhà CC";
            this.colTenCC.ColumnEdit = this.lupTenCC;
            this.colTenCC.FieldName = "MaCC";
            this.colTenCC.Name = "colTenCC";
            this.colTenCC.Visible = true;
            this.colTenCC.VisibleIndex = 3;
            this.colTenCC.Width = 215;
            // 
            // lupTenCC
            // 
            this.lupTenCC.AutoHeight = false;
            this.lupTenCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTenCC.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCC", "MaCC"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCC", "TenCC")});
            this.lupTenCC.DisplayMember = "TenCC";
            this.lupTenCC.Name = "lupTenCC";
            this.lupTenCC.NullText = "";
            this.lupTenCC.ValueMember = "MaCC";
            // 
            // frm_UpdateMaCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 521);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_UpdateMaCC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật mã cung cấp vào danh mục";
            this.Load += new System.EventHandler(this.frm_UpdateMaCC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDuocMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTenCC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCC;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCC;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupTenCC;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.LookUpEdit lupMaCC;
        private DevExpress.XtraEditors.TextEdit txtTenDV;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMaDV;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnThemMoi;
        private DevExpress.XtraEditors.TextEdit txtMaDuocMoi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}