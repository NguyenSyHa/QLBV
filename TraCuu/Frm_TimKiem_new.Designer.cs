
namespace QLBV.TraCuu
{
    partial class Frm_TimKiem_new
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
            this.grTimKiem = new DevExpress.XtraEditors.GroupControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtMaicd = new System.Windows.Forms.TextBox();
            this.grKetQua = new DevExpress.XtraEditors.GroupControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnclear = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaICDKQ = new DevExpress.XtraEditors.TextEdit();
            this.txtTenBenhKQ = new DevExpress.XtraEditors.TextEdit();
            this.grcICD = new DevExpress.XtraGrid.GridControl();
            this.grvICD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenBenh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grTimKiem)).BeginInit();
            this.grTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grKetQua)).BeginInit();
            this.grKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICDKQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBenhKQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).BeginInit();
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
            this.grTimKiem.Size = new System.Drawing.Size(521, 55);
            this.grTimKiem.TabIndex = 2;
            this.grTimKiem.Text = "Tìm Kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimKiem.Location = new System.Drawing.Point(2, 23);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.NullText = "Gõ tên bệnh hoặc mã bệnh";
            this.txtTimKiem.Size = new System.Drawing.Size(517, 22);
            this.txtTimKiem.TabIndex = 3;
            this.txtTimKiem.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtTimKiem_KeyUp);
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
            // txtMaicd
            // 
            this.txtMaicd.Location = new System.Drawing.Point(437, 78);
            this.txtMaicd.Name = "txtMaicd";
            this.txtMaicd.Size = new System.Drawing.Size(10, 22);
            this.txtMaicd.TabIndex = 4;
            this.txtMaicd.Visible = false;
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
            this.grKetQua.Controls.Add(this.btnThoat);
            this.grKetQua.Controls.Add(this.txtMaicd);
            this.grKetQua.Controls.Add(this.btnclear);
            this.grKetQua.Controls.Add(this.labelControl2);
            this.grKetQua.Controls.Add(this.btnOK);
            this.grKetQua.Controls.Add(this.labelControl1);
            this.grKetQua.Controls.Add(this.txtMaICDKQ);
            this.grKetQua.Controls.Add(this.txtTenBenhKQ);
            this.grKetQua.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grKetQua.Location = new System.Drawing.Point(0, 206);
            this.grKetQua.Name = "grKetQua";
            this.grKetQua.Size = new System.Drawing.Size(521, 107);
            this.grKetQua.TabIndex = 5;
            this.grKetQua.Text = "Kết Quả";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(425, 77);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnclear
            // 
            this.btnclear.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.Appearance.Options.UseFont = true;
            this.btnclear.Location = new System.Drawing.Point(346, 77);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(73, 23);
            this.btnclear.TabIndex = 8;
            this.btnclear.Text = "Clear";
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
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
            this.btnOK.Location = new System.Drawing.Point(265, 77);
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
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Tên Bệnh";
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
            // txtTenBenhKQ
            // 
            this.txtTenBenhKQ.Location = new System.Drawing.Point(77, 30);
            this.txtTenBenhKQ.Name = "txtTenBenhKQ";
            this.txtTenBenhKQ.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenBenhKQ.Properties.Appearance.Options.UseFont = true;
            this.txtTenBenhKQ.Properties.ReadOnly = true;
            this.txtTenBenhKQ.Size = new System.Drawing.Size(423, 22);
            this.txtTenBenhKQ.TabIndex = 4;
            // 
            // grcICD
            // 
            this.grcICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcICD.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcICD.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcICD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcICD.Location = new System.Drawing.Point(0, 55);
            this.grcICD.MainView = this.grvICD;
            this.grcICD.Name = "grcICD";
            this.grcICD.Size = new System.Drawing.Size(521, 151);
            this.grcICD.TabIndex = 6;
            this.grcICD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvICD});
            // 
            // grvICD
            // 
            this.grvICD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenBenh,
            this.MaICD});
            this.grvICD.GridControl = this.grcICD;
            this.grvICD.Name = "grvICD";
            this.grvICD.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvICD.OptionsView.ShowGroupPanel = false;
            this.grvICD.OptionsView.ShowViewCaption = true;
            this.grvICD.ViewCaption = "Nội dung";
            this.grvICD.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvICD_FocusedRowChanged);
            this.grvICD.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvICD_CellValueChanging);
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 128;
            // 
            // TenBenh
            // 
            this.TenBenh.Caption = "Tên Bệnh";
            this.TenBenh.FieldName = "tenbenh";
            this.TenBenh.Name = "TenBenh";
            this.TenBenh.OptionsColumn.AllowEdit = false;
            this.TenBenh.Visible = true;
            this.TenBenh.VisibleIndex = 2;
            this.TenBenh.Width = 471;
            // 
            // MaICD
            // 
            this.MaICD.Caption = "Mã ICD";
            this.MaICD.FieldName = "maicd";
            this.MaICD.Name = "MaICD";
            this.MaICD.Visible = true;
            this.MaICD.VisibleIndex = 1;
            this.MaICD.Width = 92;
            // 
            // Frm_TimKiem_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 313);
            this.Controls.Add(this.grcICD);
            this.Controls.Add(this.grKetQua);
            this.Controls.Add(this.grTimKiem);
            this.Name = "Frm_TimKiem_new";
            this.Text = "Danh mục ICD10";
            this.Load += new System.EventHandler(this.Frm_TimKiem_new_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grTimKiem)).EndInit();
            this.grTimKiem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grKetQua)).EndInit();
            this.grKetQua.ResumeLayout(false);
            this.grKetQua.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaICDKQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBenhKQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grTimKiem;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl grKetQua;
        private DevExpress.XtraEditors.SimpleButton btnclear;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaICDKQ;
        private DevExpress.XtraEditors.TextEdit txtTenBenhKQ;
        private DevExpress.XtraGrid.GridControl grcICD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvICD;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraGrid.Columns.GridColumn TenBenh;
        private DevExpress.XtraGrid.Columns.GridColumn MaICD;
        private System.Windows.Forms.TextBox txtMaicd;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
    }
}