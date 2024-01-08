namespace QLBV.FormThamSo
{
    partial class frm_TimKiemICD9
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.grTimKiem = new DevExpress.XtraEditors.GroupControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grNoiDung = new DevExpress.XtraEditors.GroupControl();
            this.grcNoiDung = new DevExpress.XtraGrid.GridControl();
            this.grvNoiDung = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenPTTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grKetQua = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaICDKQ = new DevExpress.XtraEditors.TextEdit();
            this.txtTenPTTT = new DevExpress.XtraEditors.TextEdit();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grTimKiem)).BeginInit();
            this.grTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grNoiDung)).BeginInit();
            this.grNoiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcNoiDung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNoiDung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grKetQua)).BeginInit();
            this.grKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICDKQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenPTTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grTimKiem
            // 
            this.grTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grTimKiem.Appearance.Options.UseFont = true;
            this.grTimKiem.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grTimKiem.AppearanceCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.grTimKiem.AppearanceCaption.Options.UseFont = true;
            this.grTimKiem.AppearanceCaption.Options.UseForeColor = true;
            this.grTimKiem.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grTimKiem.Controls.Add(this.txtTimKiem);
            this.grTimKiem.Controls.Add(this.groupControl1);
            this.grTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.grTimKiem.Location = new System.Drawing.Point(0, 0);
            this.grTimKiem.Name = "grTimKiem";
            this.grTimKiem.Size = new System.Drawing.Size(512, 55);
            this.grTimKiem.TabIndex = 1;
            this.grTimKiem.Text = "Tìm Kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimKiem.EditValue = "Gõ tên PTTT hoặc mã ICD9";
            this.txtTimKiem.Location = new System.Drawing.Point(2, 21);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.NullText = "Gõ tên bệnh hoặc mã bệnh";
            this.txtTimKiem.Size = new System.Drawing.Size(508, 22);
            this.txtTimKiem.TabIndex = 3;
            this.txtTimKiem.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtTimKiem_KeyUp);
            this.txtTimKiem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyUp);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Location = new System.Drawing.Point(0, 61);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(333, 55);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Tìm Kiếm";
            // 
            // grNoiDung
            // 
            this.grNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grNoiDung.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grNoiDung.Appearance.Options.UseFont = true;
            this.grNoiDung.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grNoiDung.AppearanceCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.grNoiDung.AppearanceCaption.Options.UseFont = true;
            this.grNoiDung.AppearanceCaption.Options.UseForeColor = true;
            this.grNoiDung.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grNoiDung.Controls.Add(this.grcNoiDung);
            this.grNoiDung.Location = new System.Drawing.Point(0, 56);
            this.grNoiDung.Name = "grNoiDung";
            this.grNoiDung.Size = new System.Drawing.Size(512, 198);
            this.grNoiDung.TabIndex = 3;
            this.grNoiDung.Text = "Nội Dung";
            // 
            // grcNoiDung
            // 
            this.grcNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.grcNoiDung.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grcNoiDung.Location = new System.Drawing.Point(2, 21);
            this.grcNoiDung.MainView = this.grvNoiDung;
            this.grcNoiDung.Name = "grcNoiDung";
            this.grcNoiDung.Size = new System.Drawing.Size(508, 175);
            this.grcNoiDung.TabIndex = 0;
            this.grcNoiDung.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNoiDung});
            // 
            // grvNoiDung
            // 
            this.grvNoiDung.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvNoiDung.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvNoiDung.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvNoiDung.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvNoiDung.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaICD,
            this.TenPTTT,
            this.ID});
            this.grvNoiDung.GridControl = this.grcNoiDung;
            this.grvNoiDung.Name = "grvNoiDung";
            this.grvNoiDung.OptionsBehavior.Editable = false;
            this.grvNoiDung.OptionsBehavior.ReadOnly = true;
            this.grvNoiDung.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvNoiDung.OptionsView.EnableAppearanceOddRow = true;
            this.grvNoiDung.OptionsView.ShowGroupPanel = false;
            this.grvNoiDung.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvNoiDung_FocusedRowChanged);
            this.grvNoiDung.DoubleClick += new System.EventHandler(this.grvNoiDung_DoubleClick);
            // 
            // MaICD
            // 
            this.MaICD.Caption = "Mã ICD";
            this.MaICD.FieldName = "MaICD";
            this.MaICD.Name = "MaICD";
            this.MaICD.Visible = true;
            this.MaICD.VisibleIndex = 1;
            this.MaICD.Width = 76;
            // 
            // TenPTTT
            // 
            this.TenPTTT.Caption = "Tên PTTT";
            this.TenPTTT.FieldName = "TenPTTT";
            this.TenPTTT.Name = "TenPTTT";
            this.TenPTTT.Visible = true;
            this.TenPTTT.VisibleIndex = 2;
            this.TenPTTT.Width = 414;
            // 
            // grKetQua
            // 
            this.grKetQua.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grKetQua.Appearance.Options.UseFont = true;
            this.grKetQua.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grKetQua.AppearanceCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.grKetQua.AppearanceCaption.Options.UseFont = true;
            this.grKetQua.AppearanceCaption.Options.UseForeColor = true;
            this.grKetQua.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grKetQua.Controls.Add(this.txtID);
            this.grKetQua.Controls.Add(this.simpleButton2);
            this.grKetQua.Controls.Add(this.labelControl2);
            this.grKetQua.Controls.Add(this.btnOK);
            this.grKetQua.Controls.Add(this.labelControl1);
            this.grKetQua.Controls.Add(this.txtMaICDKQ);
            this.grKetQua.Controls.Add(this.txtTenPTTT);
            this.grKetQua.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grKetQua.Location = new System.Drawing.Point(0, 258);
            this.grKetQua.Name = "grKetQua";
            this.grKetQua.Size = new System.Drawing.Size(512, 124);
            this.grKetQua.TabIndex = 4;
            this.grKetQua.Text = "Kết Quả";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(425, 78);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "&Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.labelControl2.Location = new System.Drawing.Point(12, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 16);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Mã ICD";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(344, 78);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.labelControl1.Location = new System.Drawing.Point(12, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Tên PTTT";
            // 
            // txtMaICDKQ
            // 
            this.txtMaICDKQ.Location = new System.Drawing.Point(77, 50);
            this.txtMaICDKQ.Name = "txtMaICDKQ";
            this.txtMaICDKQ.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaICDKQ.Properties.Appearance.Options.UseFont = true;
            this.txtMaICDKQ.Properties.ReadOnly = true;
            this.txtMaICDKQ.Size = new System.Drawing.Size(423, 22);
            this.txtMaICDKQ.TabIndex = 5;
            // 
            // txtTenPTTT
            // 
            this.txtTenPTTT.Location = new System.Drawing.Point(77, 30);
            this.txtTenPTTT.Name = "txtTenPTTT";
            this.txtTenPTTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenPTTT.Properties.Appearance.Options.UseFont = true;
            this.txtTenPTTT.Properties.ReadOnly = true;
            this.txtTenPTTT.Size = new System.Drawing.Size(423, 22);
            this.txtTenPTTT.TabIndex = 4;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(77, 75);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(126, 22);
            this.txtID.TabIndex = 8;
            // 
            // frm_TimKiemICD9
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 382);
            this.Controls.Add(this.grKetQua);
            this.Controls.Add(this.grNoiDung);
            this.Controls.Add(this.grTimKiem);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TimKiemICD9";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục ICD9";
            this.Load += new System.EventHandler(this.frm_TimKiemICD9_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grTimKiem)).EndInit();
            this.grTimKiem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grNoiDung)).EndInit();
            this.grNoiDung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcNoiDung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNoiDung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grKetQua)).EndInit();
            this.grKetQua.ResumeLayout(false);
            this.grKetQua.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICDKQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenPTTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grTimKiem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl grNoiDung;
        private DevExpress.XtraEditors.GroupControl grKetQua;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaICDKQ;
        private DevExpress.XtraEditors.TextEdit txtTenPTTT;
        private DevExpress.XtraGrid.GridControl grcNoiDung;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNoiDung;
        private DevExpress.XtraGrid.Columns.GridColumn MaICD;
        private DevExpress.XtraGrid.Columns.GridColumn TenPTTT;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraEditors.TextEdit txtID;

    }
}