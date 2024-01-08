namespace QLBV.FormThamSo
{
    partial class frm_DsMaDV
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
            this.txtTimkiem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grc_DSDichVu = new DevExpress.XtraGrid.GridControl();
            this.grv_DSDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDv = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_DSDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DSDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Location = new System.Drawing.Point(104, 5);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(243, 20);
            this.txtTimkiem.TabIndex = 0;
            this.txtTimkiem.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Tìm kiếm";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.txtTimkiem);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(645, 34);
            this.panelControl1.TabIndex = 2;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(558, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "&Thoát";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(477, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.grc_DSDichVu);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 34);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(645, 332);
            this.panelControl2.TabIndex = 3;
            // 
            // grc_DSDichVu
            // 
            this.grc_DSDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_DSDichVu.Location = new System.Drawing.Point(0, 0);
            this.grc_DSDichVu.MainView = this.grv_DSDichVu;
            this.grc_DSDichVu.Name = "grc_DSDichVu";
            this.grc_DSDichVu.Size = new System.Drawing.Size(645, 332);
            this.grc_DSDichVu.TabIndex = 0;
            this.grc_DSDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_DSDichVu});
            // 
            // grv_DSDichVu
            // 
            this.grv_DSDichVu.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_DSDichVu.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_DSDichVu.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grv_DSDichVu.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grv_DSDichVu.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grv_DSDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenDv});
            this.grv_DSDichVu.GridControl = this.grc_DSDichVu;
            this.grv_DSDichVu.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grv_DSDichVu.Name = "grv_DSDichVu";
            this.grv_DSDichVu.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grv_DSDichVu.OptionsBehavior.Editable = false;
            this.grv_DSDichVu.OptionsBehavior.ReadOnly = true;
            this.grv_DSDichVu.OptionsView.ShowGroupPanel = false;
            this.grv_DSDichVu.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grv_DSDichVu.DoubleClick += new System.EventHandler(this.grv_DSDichVu_DoubleClick);
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "Mã Dịch vụ";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 165;
            // 
            // colTenDv
            // 
            this.colTenDv.Caption = "Tên thuốc";
            this.colTenDv.FieldName = "TenDV";
            this.colTenDv.Name = "colTenDv";
            this.colTenDv.Visible = true;
            this.colTenDv.VisibleIndex = 1;
            this.colTenDv.Width = 462;
            // 
            // frm_DsMaDV
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 366);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DsMaDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách dịch vụ";
            this.Load += new System.EventHandler(this.frm_DsMaDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimkiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_DSDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DSDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtTimkiem;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grc_DSDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_DSDichVu;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDv;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}