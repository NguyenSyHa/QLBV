namespace QLBV.FormDanhMuc
{
    partial class Frm_Dm_NgheNghiep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Dm_NgheNghiep));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenCT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenNN = new DevExpress.XtraEditors.TextEdit();
            this.txtMaNN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.grcDmNN = new DevExpress.XtraGrid.GridControl();
            this.grvDmNN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaNN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenNN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoi = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenNN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmNN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmNN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Teal;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtTenCT);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtTenNN);
            this.groupControl1.Controls.Add(this.txtMaNN);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtID);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(784, 98);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "DANH MỤC NGHỀ NGHIỆP";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 13);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Mã NN*:";
            this.labelControl3.ToolTip = "Mã nghề nghiệp";
            // 
            // txtTenCT
            // 
            this.txtTenCT.Location = new System.Drawing.Point(509, 55);
            this.txtTenCT.Name = "txtTenCT";
            this.txtTenCT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenCT.Properties.Appearance.Options.UseFont = true;
            this.txtTenCT.Size = new System.Drawing.Size(250, 20);
            this.txtTenCT.TabIndex = 38;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(437, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 13);
            this.labelControl1.TabIndex = 41;
            this.labelControl1.Text = "Tên chi tiết:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(-77, 42);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 40;
            this.labelControl6.Text = "Mã NN*:";
            this.labelControl6.ToolTip = "Mã nghề nghiệp";
            // 
            // txtTenNN
            // 
            this.txtTenNN.Location = new System.Drawing.Point(240, 54);
            this.txtTenNN.Name = "txtTenNN";
            this.txtTenNN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenNN.Properties.Appearance.Options.UseFont = true;
            this.txtTenNN.Size = new System.Drawing.Size(186, 20);
            this.txtTenNN.TabIndex = 37;
            // 
            // txtMaNN
            // 
            this.txtMaNN.Location = new System.Drawing.Point(62, 55);
            this.txtMaNN.Name = "txtMaNN";
            this.txtMaNN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaNN.Properties.Appearance.Options.UseFont = true;
            this.txtMaNN.Properties.Mask.EditMask = "n0";
            this.txtMaNN.Size = new System.Drawing.Size(109, 20);
            this.txtMaNN.TabIndex = 36;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(177, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 39;
            this.labelControl2.Text = "Tên NN*:";
            this.labelControl2.ToolTip = "Tên nghề nghiệp";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(73, 27);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Size = new System.Drawing.Size(37, 20);
            this.txtID.TabIndex = 35;
            this.txtID.Visible = false;
            // 
            // grcDmNN
            // 
            this.grcDmNN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDmNN.Location = new System.Drawing.Point(0, 98);
            this.grcDmNN.MainView = this.grvDmNN;
            this.grcDmNN.Name = "grcDmNN";
            this.grcDmNN.Size = new System.Drawing.Size(784, 464);
            this.grcDmNN.TabIndex = 2;
            this.grcDmNN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDmNN});
            this.grcDmNN.Click += new System.EventHandler(this.grcDmNN_Click);
            // 
            // grvDmNN
            // 
            this.grvDmNN.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDmNN.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmNN.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmNN.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDmNN.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDmNN.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmNN.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmNN.Appearance.Row.Options.UseFont = true;
            this.grvDmNN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaNN,
            this.colTenNN,
            this.colTenCT});
            this.grvDmNN.GridControl = this.grcDmNN;
            this.grvDmNN.Name = "grvDmNN";
            this.grvDmNN.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmNN.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmNN.OptionsBehavior.Editable = false;
            this.grvDmNN.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvDmNN.OptionsView.ColumnAutoWidth = false;
            this.grvDmNN.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDmNN.OptionsView.EnableAppearanceOddRow = true;
            this.grvDmNN.OptionsView.ShowGroupPanel = false;
            this.grvDmNN.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDmNN_FocusedRowChanged);
            // 
            // colMaNN
            // 
            this.colMaNN.Caption = "Mã nghề nghiệp";
            this.colMaNN.FieldName = "MaNN";
            this.colMaNN.Name = "colMaNN";
            this.colMaNN.Visible = true;
            this.colMaNN.VisibleIndex = 0;
            this.colMaNN.Width = 149;
            // 
            // colTenNN
            // 
            this.colTenNN.Caption = "Tên nghề nghiệp";
            this.colTenNN.FieldName = "TenNN";
            this.colTenNN.Name = "colTenNN";
            this.colTenNN.Visible = true;
            this.colTenNN.VisibleIndex = 1;
            this.colTenNN.Width = 276;
            // 
            // colTenCT
            // 
            this.colTenCT.Caption = "Tên chi tiết nghề nghiệp";
            this.colTenCT.FieldName = "TenCT";
            this.colTenCT.Name = "colTenCT";
            this.colTenCT.Visible = true;
            this.colTenCT.VisibleIndex = 2;
            this.colTenCT.Width = 334;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.txtTimKiem);
            this.panelControl3.Controls.Add(this.panelControl6);
            this.panelControl3.Controls.Add(this.panelControl4);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 522);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(784, 40);
            this.panelControl3.TabIndex = 3;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.EditValue = "Tìm kiếm theo tên NN";
            this.txtTimKiem.Location = new System.Drawing.Point(312, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.Appearance.Options.UseForeColor = true;
            this.txtTimKiem.Properties.NullText = "Tìm kiếm theo tên TN";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 24);
            this.txtTimKiem.TabIndex = 38;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // panelControl6
            // 
            this.panelControl6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl6.Appearance.Options.UseBackColor = true;
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Controls.Add(this.simpleButton5);
            this.panelControl6.Controls.Add(this.simpleButton6);
            this.panelControl6.Controls.Add(this.simpleButton7);
            this.panelControl6.Controls.Add(this.simpleButton8);
            this.panelControl6.Location = new System.Drawing.Point(262, 525);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(230, 29);
            this.panelControl6.TabIndex = 37;
            // 
            // simpleButton5
            // 
            this.simpleButton5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton5.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton5.Appearance.Options.UseFont = true;
            this.simpleButton5.Appearance.Options.UseForeColor = true;
            this.simpleButton5.Image = global::QLBV.Properties.Resources.saveall_16x16;
            this.simpleButton5.Location = new System.Drawing.Point(177, 3);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(48, 23);
            this.simpleButton5.TabIndex = 3;
            this.simpleButton5.Text = "&Lưu";
            // 
            // simpleButton6
            // 
            this.simpleButton6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton6.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton6.Appearance.Options.UseFont = true;
            this.simpleButton6.Appearance.Options.UseForeColor = true;
            this.simpleButton6.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton6.Image")));
            this.simpleButton6.Location = new System.Drawing.Point(120, 3);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(51, 23);
            this.simpleButton6.TabIndex = 2;
            this.simpleButton6.Text = "&Xóa";
            // 
            // simpleButton7
            // 
            this.simpleButton7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton7.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton7.Appearance.Options.UseFont = true;
            this.simpleButton7.Appearance.Options.UseForeColor = true;
            this.simpleButton7.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton7.Image")));
            this.simpleButton7.Location = new System.Drawing.Point(60, 3);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(54, 23);
            this.simpleButton7.TabIndex = 1;
            this.simpleButton7.Text = "&Sửa";
            // 
            // simpleButton8
            // 
            this.simpleButton8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton8.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton8.Appearance.Options.UseFont = true;
            this.simpleButton8.Appearance.Options.UseForeColor = true;
            this.simpleButton8.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton8.Image")));
            this.simpleButton8.Location = new System.Drawing.Point(4, 3);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(50, 23);
            this.simpleButton8.TabIndex = 0;
            this.simpleButton8.Text = "&Mới";
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnLuu);
            this.panelControl4.Controls.Add(this.btnXoa);
            this.panelControl4.Controls.Add(this.btnSua);
            this.panelControl4.Controls.Add(this.btnMoi);
            this.panelControl4.Location = new System.Drawing.Point(49, 6);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(230, 29);
            this.panelControl4.TabIndex = 37;
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Appearance.Options.UseForeColor = true;
            this.btnLuu.Image = global::QLBV.Properties.Resources.saveall_16x16;
            this.btnLuu.Location = new System.Drawing.Point(177, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(48, 23);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Appearance.Options.UseForeColor = true;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(120, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(51, 23);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Appearance.Options.UseForeColor = true;
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.Location = new System.Drawing.Point(60, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(54, 23);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "&Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnMoi.Appearance.Options.UseFont = true;
            this.btnMoi.Appearance.Options.UseForeColor = true;
            this.btnMoi.Image = ((System.Drawing.Image)(resources.GetObject("btnMoi.Image")));
            this.btnMoi.Location = new System.Drawing.Point(4, 3);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(50, 23);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Text = "&Mới";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // Frm_Dm_NgheNghiep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.grcDmNN);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm_Dm_NgheNghiep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục nghề nghiệp";
            this.Load += new System.EventHandler(this.Frm_Dm_NgheNghiep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenCT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenNN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmNN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmNN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtTenCT;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtTenNN;
        private DevExpress.XtraEditors.TextEdit txtMaNN;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl grcDmNN;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDmNN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenNN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCT;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnMoi;
    }
}