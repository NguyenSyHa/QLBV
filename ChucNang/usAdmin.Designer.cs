namespace QLBV.FormDanhMuc
{
    partial class usAdmin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtTenDN = new DevExpress.XtraEditors.TextEdit();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuuKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoaKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnSuaKb = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoiKb = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.grcAdmin = new DevExpress.XtraGrid.GridControl();
            this.grvAdmin = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaCB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapDo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCauHoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTraLoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaKP = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lupMaCB = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtTenDN);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 726);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1024, 42);
            this.panelControl2.TabIndex = 1;
            // 
            // txtTenDN
            // 
            this.txtTenDN.Location = new System.Drawing.Point(815, 6);
            this.txtTenDN.Name = "txtTenDN";
            this.txtTenDN.Size = new System.Drawing.Size(100, 20);
            this.txtTenDN.TabIndex = 35;
            this.txtTenDN.Visible = false;
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnLuuKb);
            this.panelControl4.Controls.Add(this.btnXoaKb);
            this.panelControl4.Controls.Add(this.btnSuaKb);
            this.panelControl4.Controls.Add(this.btnMoiKb);
            this.panelControl4.Location = new System.Drawing.Point(5, 6);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(230, 29);
            this.panelControl4.TabIndex = 34;
            // 
            // btnLuuKb
            // 
            this.btnLuuKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuuKb.Appearance.Options.UseFont = true;
            this.btnLuuKb.Appearance.Options.UseForeColor = true;
            this.btnLuuKb.Image = global::QLBV.Properties.Resources.save_16x16;
            this.btnLuuKb.Location = new System.Drawing.Point(177, 3);
            this.btnLuuKb.Name = "btnLuuKb";
            this.btnLuuKb.Size = new System.Drawing.Size(48, 23);
            this.btnLuuKb.TabIndex = 5;
            this.btnLuuKb.Text = "&Lưu";
            this.btnLuuKb.Click += new System.EventHandler(this.btnLuuKb_Click);
            // 
            // btnXoaKb
            // 
            this.btnXoaKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnXoaKb.Appearance.Options.UseFont = true;
            this.btnXoaKb.Appearance.Options.UseForeColor = true;
            this.btnXoaKb.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.btnXoaKb.Location = new System.Drawing.Point(120, 3);
            this.btnXoaKb.Name = "btnXoaKb";
            this.btnXoaKb.Size = new System.Drawing.Size(51, 23);
            this.btnXoaKb.TabIndex = 2;
            this.btnXoaKb.Text = "&Xóa";
            this.btnXoaKb.Click += new System.EventHandler(this.btnXoaKb_Click);
            // 
            // btnSuaKb
            // 
            this.btnSuaKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnSuaKb.Appearance.Options.UseFont = true;
            this.btnSuaKb.Appearance.Options.UseForeColor = true;
            this.btnSuaKb.Image = global::QLBV.Properties.Resources.editcontact_16x16;
            this.btnSuaKb.Location = new System.Drawing.Point(60, 3);
            this.btnSuaKb.Name = "btnSuaKb";
            this.btnSuaKb.Size = new System.Drawing.Size(54, 23);
            this.btnSuaKb.TabIndex = 1;
            this.btnSuaKb.Text = "&Sửa";
            this.btnSuaKb.Click += new System.EventHandler(this.btnSuaKb_Click);
            // 
            // btnMoiKb
            // 
            this.btnMoiKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoiKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnMoiKb.Appearance.Options.UseFont = true;
            this.btnMoiKb.Appearance.Options.UseForeColor = true;
            this.btnMoiKb.Image = global::QLBV.Properties.Resources.newcontact_16x16;
            this.btnMoiKb.Location = new System.Drawing.Point(4, 3);
            this.btnMoiKb.Name = "btnMoiKb";
            this.btnMoiKb.Size = new System.Drawing.Size(50, 23);
            this.btnMoiKb.TabIndex = 0;
            this.btnMoiKb.Text = "&Mới";
            this.btnMoiKb.Click += new System.EventHandler(this.btnMoiKb_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.grcAdmin);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1024, 726);
            this.panelControl3.TabIndex = 2;
            // 
            // grcAdmin
            // 
            this.grcAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcAdmin.Location = new System.Drawing.Point(0, 0);
            this.grcAdmin.MainView = this.grvAdmin;
            this.grcAdmin.Name = "grcAdmin";
            this.grcAdmin.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupMaCB,
            this.lupMaKP});
            this.grcAdmin.Size = new System.Drawing.Size(1024, 726);
            this.grcAdmin.TabIndex = 2;
            this.grcAdmin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAdmin});
            this.grcAdmin.Load += new System.EventHandler(this.grcAdmin_Load);
            // 
            // grvAdmin
            // 
            this.grvAdmin.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvAdmin.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvAdmin.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvAdmin.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Navy;
            this.grvAdmin.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvAdmin.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvAdmin.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvAdmin.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvAdmin.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvAdmin.Appearance.Row.Options.UseFont = true;
            this.grvAdmin.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvAdmin.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Teal;
            this.grvAdmin.Appearance.ViewCaption.Options.UseFont = true;
            this.grvAdmin.Appearance.ViewCaption.Options.UseForeColor = true;
            this.grvAdmin.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTenDN,
            this.colMaCB,
            this.colCapDo,
            this.colMatK,
            this.colCauHoi,
            this.colTraLoi,
            this.colStatus,
            this.colTenCB,
            this.colMaKP});
            this.grvAdmin.GridControl = this.grcAdmin;
            this.grvAdmin.Name = "grvAdmin";
            this.grvAdmin.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvAdmin.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvAdmin.OptionsBehavior.Editable = false;
            this.grvAdmin.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvAdmin.OptionsView.ColumnAutoWidth = false;
            this.grvAdmin.OptionsView.EnableAppearanceEvenRow = true;
            this.grvAdmin.OptionsView.EnableAppearanceOddRow = true;
            this.grvAdmin.OptionsView.ShowGroupPanel = false;
            this.grvAdmin.OptionsView.ShowViewCaption = true;
            this.grvAdmin.ViewCaption = "Danh sách tài khoản đăng nhập";
            this.grvAdmin.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvAdmin_FocusedRowChanged);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 59;
            // 
            // colTenDN
            // 
            this.colTenDN.Caption = "Tên đăng nhập";
            this.colTenDN.FieldName = "TenDN";
            this.colTenDN.Name = "colTenDN";
            this.colTenDN.Visible = true;
            this.colTenDN.VisibleIndex = 0;
            this.colTenDN.Width = 196;
            // 
            // colMaCB
            // 
            this.colMaCB.Caption = "Tên gọi";
            this.colMaCB.FieldName = "MaCB";
            this.colMaCB.Name = "colMaCB";
            this.colMaCB.Width = 226;
            // 
            // colCapDo
            // 
            this.colCapDo.Caption = "Cấp độ";
            this.colCapDo.FieldName = "CapDo";
            this.colCapDo.Name = "colCapDo";
            this.colCapDo.Visible = true;
            this.colCapDo.VisibleIndex = 3;
            this.colCapDo.Width = 78;
            // 
            // colMatK
            // 
            this.colMatK.Caption = "mật khẩu";
            this.colMatK.FieldName = "MatK";
            this.colMatK.Name = "colMatK";
            this.colMatK.Width = 217;
            // 
            // colCauHoi
            // 
            this.colCauHoi.Caption = "Câu hỏi";
            this.colCauHoi.FieldName = "CauHoi";
            this.colCauHoi.Name = "colCauHoi";
            // 
            // colTraLoi
            // 
            this.colTraLoi.Caption = "câu trả lời";
            this.colTraLoi.FieldName = "TraLoi";
            this.colTraLoi.Name = "colTraLoi";
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 4;
            this.colStatus.Width = 126;
            // 
            // colTenCB
            // 
            this.colTenCB.Caption = "Tên gọi";
            this.colTenCB.FieldName = "TenCB";
            this.colTenCB.Name = "colTenCB";
            this.colTenCB.Visible = true;
            this.colTenCB.VisibleIndex = 1;
            this.colTenCB.Width = 225;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "Khoa phòng";
            this.colMaKP.ColumnEdit = this.lupMaKP;
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            this.colMaKP.Visible = true;
            this.colMaKP.VisibleIndex = 2;
            this.colMaKP.Width = 237;
            // 
            // lupMaKP
            // 
            this.lupMaKP.AutoHeight = false;
            this.lupMaKP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaKP.DisplayMember = "TenKP";
            this.lupMaKP.Name = "lupMaKP";
            this.lupMaKP.NullText = "";
            this.lupMaKP.ValueMember = "MaKP";
            // 
            // lupMaCB
            // 
            this.lupMaCB.AutoHeight = false;
            this.lupMaCB.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaCB.Name = "lupMaCB";
            // 
            // usAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "usAdmin";
            this.Size = new System.Drawing.Size(1024, 768);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl grcAdmin;
        private DevExpress.XtraGrid.Views.Grid.GridView grvAdmin;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCB;
        private DevExpress.XtraGrid.Columns.GridColumn colCapDo;
        private DevExpress.XtraGrid.Columns.GridColumn colMatK;
        private DevExpress.XtraGrid.Columns.GridColumn colCauHoi;
        private DevExpress.XtraGrid.Columns.GridColumn colTraLoi;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnLuuKb;
        private DevExpress.XtraEditors.SimpleButton btnXoaKb;
        private DevExpress.XtraEditors.SimpleButton btnSuaKb;
        private DevExpress.XtraEditors.SimpleButton btnMoiKb;
        private DevExpress.XtraEditors.TextEdit txtTenDN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCB;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupMaCB;
    }
}
