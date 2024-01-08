namespace QLBV.FormNhap
{
    partial class frmChonBN
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
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtNhapTBN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcDSBN = new DevExpress.XtraGrid.GridControl();
            this.grvDSBN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMSBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaCS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHanBHTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHanBHDen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNGAYCAP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNhapTBN.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Appearance.Options.UseForeColor = true;
            this.btnThoat.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnThoat.Location = new System.Drawing.Point(701, 12);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "&Cancel";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Appearance.Options.UseForeColor = true;
            this.btnOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnOK.Location = new System.Drawing.Point(620, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            // 
            // txtNhapTBN
            // 
            this.txtNhapTBN.Location = new System.Drawing.Point(98, 9);
            this.txtNhapTBN.Name = "txtNhapTBN";
            this.txtNhapTBN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapTBN.Properties.Appearance.Options.UseFont = true;
            this.txtNhapTBN.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.txtNhapTBN.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.txtNhapTBN.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtNhapTBN.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNhapTBN.Size = new System.Drawing.Size(193, 22);
            this.txtNhapTBN.TabIndex = 22;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            this.labelControl2.Location = new System.Drawing.Point(9, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 14);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Tên BN: ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.txtNhapTBN);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 41);
            this.panel1.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 59);
            this.panel2.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel3.Controls.Add(this.groupControl2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 304);
            this.panel3.TabIndex = 26;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.grcDSBN);
            this.groupControl2.Location = new System.Drawing.Point(3, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(778, 292);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Danh sách Bệnh Nhân";
            // 
            // grcDSBN
            // 
            this.grcDSBN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grcDSBN.Location = new System.Drawing.Point(5, 24);
            this.grcDSBN.MainView = this.grvDSBN;
            this.grcDSBN.Name = "grcDSBN";
            this.grcDSBN.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.grcDSBN.Size = new System.Drawing.Size(768, 263);
            this.grcDSBN.TabIndex = 2;
            this.grcDSBN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDSBN});
            // 
            // grvDSBN
            // 
            this.grvDSBN.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvDSBN.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.grvDSBN.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDSBN.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDSBN.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDSBN.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDSBN.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDSBN.Appearance.Row.Options.UseFont = true;
            this.grvDSBN.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.grvDSBN.Appearance.ViewCaption.Options.UseFont = true;
            this.grvDSBN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMSBN,
            this.colTenBNhan,
            this.colSThe,
            this.colGTinh,
            this.colNSinh,
            this.colDChi,
            this.colMaCS,
            this.colHanBHTu,
            this.colHanBHDen,
            this.colNGAYCAP,
            this.colNoiCap,
            this.colSTATUS});
            this.grvDSBN.GridControl = this.grcDSBN;
            this.grvDSBN.Name = "grvDSBN";
            this.grvDSBN.OptionsBehavior.AutoSelectAllInEditor = false;
            this.grvDSBN.OptionsBehavior.Editable = false;
            this.grvDSBN.OptionsBehavior.ReadOnly = true;
            this.grvDSBN.OptionsView.ColumnAutoWidth = false;
            this.grvDSBN.OptionsView.ShowGroupPanel = false;
            // 
            // colMSBN
            // 
            this.colMSBN.Caption = "Mã số BN";
            this.colMSBN.FieldName = "MaBN";
            this.colMSBN.Name = "colMSBN";
            this.colMSBN.Width = 100;
            // 
            // colTenBNhan
            // 
            this.colTenBNhan.Caption = "Tên Bệnh nhân";
            this.colTenBNhan.FieldName = "TenBNhan";
            this.colTenBNhan.Name = "colTenBNhan";
            this.colTenBNhan.Visible = true;
            this.colTenBNhan.VisibleIndex = 0;
            this.colTenBNhan.Width = 169;
            // 
            // colSThe
            // 
            this.colSThe.Caption = "Số thẻ BHYT";
            this.colSThe.FieldName = "SThe";
            this.colSThe.Name = "colSThe";
            this.colSThe.Visible = true;
            this.colSThe.VisibleIndex = 3;
            this.colSThe.Width = 165;
            // 
            // colGTinh
            // 
            this.colGTinh.AppearanceCell.Options.UseTextOptions = true;
            this.colGTinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGTinh.Caption = "G.Tính";
            this.colGTinh.FieldName = "GTinh";
            this.colGTinh.Name = "colGTinh";
            this.colGTinh.Visible = true;
            this.colGTinh.VisibleIndex = 2;
            this.colGTinh.Width = 59;
            // 
            // colNSinh
            // 
            this.colNSinh.AppearanceCell.Options.UseTextOptions = true;
            this.colNSinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNSinh.Caption = "Ngày sinh";
            this.colNSinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNSinh.FieldName = "NSinh";
            this.colNSinh.Name = "colNSinh";
            this.colNSinh.Visible = true;
            this.colNSinh.VisibleIndex = 1;
            this.colNSinh.Width = 64;
            // 
            // colDChi
            // 
            this.colDChi.Caption = "Địa chỉ";
            this.colDChi.FieldName = "DChi";
            this.colDChi.Name = "colDChi";
            this.colDChi.Visible = true;
            this.colDChi.VisibleIndex = 5;
            this.colDChi.Width = 226;
            // 
            // colMaCS
            // 
            this.colMaCS.Caption = "Mã CS";
            this.colMaCS.FieldName = "MaCS";
            this.colMaCS.Name = "colMaCS";
            this.colMaCS.Visible = true;
            this.colMaCS.VisibleIndex = 4;
            // 
            // colHanBHTu
            // 
            this.colHanBHTu.Caption = "Ngày bắt đầu";
            this.colHanBHTu.FieldName = "HanBHTu";
            this.colHanBHTu.Name = "colHanBHTu";
            // 
            // colHanBHDen
            // 
            this.colHanBHDen.Caption = "Ngày hết hạn";
            this.colHanBHDen.FieldName = "HanBHDen";
            this.colHanBHDen.Name = "colHanBHDen";
            // 
            // colNGAYCAP
            // 
            this.colNGAYCAP.Caption = "Ngày cấp";
            this.colNGAYCAP.FieldName = "NgayCap";
            this.colNGAYCAP.Name = "colNGAYCAP";
            // 
            // colNoiCap
            // 
            this.colNoiCap.Caption = "Nơi cấp";
            this.colNoiCap.FieldName = "NoiCap";
            this.colNoiCap.Name = "colNoiCap";
            // 
            // colSTATUS
            // 
            this.colSTATUS.Caption = "DTNHA";
            this.colSTATUS.FieldName = "STATUS";
            this.colSTATUS.Name = "colSTATUS";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            // 
            // frmChonBN
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 404);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChonBN";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn BN khám bệnh";
            this.Load += new System.EventHandler(this.frmChonBN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNhapTBN.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDSBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtNhapTBN;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcDSBN;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDSBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMSBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colSThe;
        private DevExpress.XtraGrid.Columns.GridColumn colGTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colNSinh;
        private DevExpress.XtraGrid.Columns.GridColumn colDChi;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCS;
        private DevExpress.XtraGrid.Columns.GridColumn colHanBHTu;
        private DevExpress.XtraGrid.Columns.GridColumn colHanBHDen;
        private DevExpress.XtraGrid.Columns.GridColumn colNGAYCAP;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiCap;
        private DevExpress.XtraGrid.Columns.GridColumn colSTATUS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}