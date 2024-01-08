namespace QLBV.FormThamSo
{
    partial class frm_InXnTongHop
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
            this.gridControlTN = new DevExpress.XtraGrid.GridControl();
            this.gridViewTN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.hyp_HuyChon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyp_Chon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.rdo_TrongNgoaiDM = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_TrongNgoaiDM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.gridControlTN);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(543, 171);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Chọn tiểu nhóm in gộp";
            // 
            // gridControlTN
            // 
            this.gridControlTN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTN.Location = new System.Drawing.Point(2, 23);
            this.gridControlTN.MainView = this.gridViewTN;
            this.gridControlTN.Name = "gridControlTN";
            this.gridControlTN.Size = new System.Drawing.Size(539, 146);
            this.gridControlTN.TabIndex = 0;
            this.gridControlTN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTN});
            // 
            // gridViewTN
            // 
            this.gridViewTN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn5});
            this.gridViewTN.GridControl = this.gridControlTN;
            this.gridViewTN.Name = "gridViewTN";
            this.gridViewTN.OptionsSelection.CheckBoxSelectorColumnWidth = 50;
            this.gridViewTN.OptionsSelection.MultiSelect = true;
            this.gridViewTN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewTN.OptionsView.ShowGroupPanel = false;
            this.gridViewTN.OptionsView.ShowIndicator = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên tiểu nhóm";
            this.gridColumn2.FieldName = "TenTN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 222;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ngày chỉ định";
            this.gridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn3.FieldName = "NgayThang";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 91;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khoa phòng CĐ";
            this.gridColumn4.FieldName = "TenKP";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 176;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "IdTieuNhom";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.FieldName = "IdCLS";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(155, 179);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "&In Phiếu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(236, 179);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "&Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // hyp_HuyChon
            // 
            this.hyp_HuyChon.EditValue = "Bỏ chọn";
            this.hyp_HuyChon.Location = new System.Drawing.Point(81, 181);
            this.hyp_HuyChon.Name = "hyp_HuyChon";
            this.hyp_HuyChon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_HuyChon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_HuyChon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_HuyChon.Properties.Appearance.Options.UseFont = true;
            this.hyp_HuyChon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_HuyChon.Size = new System.Drawing.Size(60, 20);
            this.hyp_HuyChon.TabIndex = 7;
            this.hyp_HuyChon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_HuyChon_OpenLink);
            // 
            // hyp_Chon
            // 
            this.hyp_Chon.EditValue = "Chọn tất cả";
            this.hyp_Chon.Location = new System.Drawing.Point(5, 181);
            this.hyp_Chon.Name = "hyp_Chon";
            this.hyp_Chon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_Chon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_Chon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_Chon.Properties.Appearance.Options.UseFont = true;
            this.hyp_Chon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_Chon.Size = new System.Drawing.Size(70, 20);
            this.hyp_Chon.TabIndex = 6;
            this.hyp_Chon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_Chon_OpenLink);
            // 
            // rdo_TrongNgoaiDM
            // 
            this.rdo_TrongNgoaiDM.Location = new System.Drawing.Point(317, 179);
            this.rdo_TrongNgoaiDM.Name = "rdo_TrongNgoaiDM";
            this.rdo_TrongNgoaiDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM", true, null, "chk_NgoaiDM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM", true, null, "chk_TrongDM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả", true, null, "chk_ALL")});
            this.rdo_TrongNgoaiDM.Size = new System.Drawing.Size(214, 23);
            this.rdo_TrongNgoaiDM.TabIndex = 8;
            this.rdo_TrongNgoaiDM.Visible = false;
            // 
            // frm_InXnTongHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 211);
            this.Controls.Add(this.rdo_TrongNgoaiDM);
            this.Controls.Add(this.hyp_HuyChon);
            this.Controls.Add(this.hyp_Chon);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Name = "frm_InXnTongHop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In kết quả XN tổng hợp";
            this.Load += new System.EventHandler(this.frm_InXnTongHop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_TrongNgoaiDM.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_HuyChon;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_Chon;
        private DevExpress.XtraGrid.GridControl gridControlTN;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTN;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.RadioGroup rdo_TrongNgoaiDM;
    }
}