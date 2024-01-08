namespace QLBV.FormThamSo
{
    partial class frm_BNSai
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
            this.grcBNhan = new DevExpress.XtraGrid.GridControl();
            this.grvBNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupGTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDTuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coNgayVao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayRa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoNgayDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grcBNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupGTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcBNhan
            // 
            this.grcBNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcBNhan.Location = new System.Drawing.Point(0, 0);
            this.grcBNhan.MainView = this.grvBNhan;
            this.grcBNhan.Name = "grcBNhan";
            this.grcBNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupGTinh});
            this.grcBNhan.Size = new System.Drawing.Size(917, 434);
            this.grcBNhan.TabIndex = 0;
            this.grcBNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBNhan});
            // 
            // grvBNhan
            // 
            this.grvBNhan.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvBNhan.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvBNhan.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvBNhan.Appearance.Row.Options.UseFont = true;
            this.grvBNhan.Appearance.Row.Options.UseTextOptions = true;
            this.grvBNhan.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.grvBNhan.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvBNhan.AppearancePrint.Row.Options.UseTextOptions = true;
            this.grvBNhan.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvBNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBN,
            this.colTenBN,
            this.colGTinh,
            this.colSThe,
            this.colDTuong,
            this.colMaICD,
            this.colDiaChi,
            this.coNgayVao,
            this.colNgayRa,
            this.colNgayTT,
            this.colSoNgayDT});
            this.grvBNhan.GridControl = this.grcBNhan;
            this.grvBNhan.Name = "grvBNhan";
            this.grvBNhan.OptionsBehavior.Editable = false;
            this.grvBNhan.OptionsBehavior.ReadOnly = true;
            this.grvBNhan.OptionsView.EnableAppearanceEvenRow = true;
            this.grvBNhan.OptionsView.EnableAppearanceOddRow = true;
            this.grvBNhan.OptionsView.ShowFooter = true;
            this.grvBNhan.OptionsView.ShowGroupPanel = false;
            // 
            // colMaBN
            // 
            this.colMaBN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colMaBN.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaBN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaBN.Caption = "Mã BN";
            this.colMaBN.FieldName = "MaBNhan";
            this.colMaBN.Name = "colMaBN";
            this.colMaBN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colMaBN.Visible = true;
            this.colMaBN.VisibleIndex = 0;
            this.colMaBN.Width = 49;
            // 
            // colTenBN
            // 
            this.colTenBN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenBN.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenBN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenBN.Caption = "Tên BN";
            this.colTenBN.FieldName = "TenBNhan";
            this.colTenBN.Name = "colTenBN";
            this.colTenBN.Visible = true;
            this.colTenBN.VisibleIndex = 1;
            this.colTenBN.Width = 124;
            // 
            // colGTinh
            // 
            this.colGTinh.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGTinh.AppearanceHeader.Options.UseTextOptions = true;
            this.colGTinh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGTinh.Caption = "G.Tính";
            this.colGTinh.ColumnEdit = this.lupGTinh;
            this.colGTinh.FieldName = "GTinh";
            this.colGTinh.Name = "colGTinh";
            this.colGTinh.Visible = true;
            this.colGTinh.VisibleIndex = 2;
            this.colGTinh.Width = 42;
            // 
            // lupGTinh
            // 
            this.lupGTinh.AutoHeight = false;
            this.lupGTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupGTinh.DisplayMember = "Name";
            this.lupGTinh.Name = "lupGTinh";
            this.lupGTinh.NullText = "";
            this.lupGTinh.ReadOnly = true;
            this.lupGTinh.ValueMember = "Gtri";
            // 
            // colSThe
            // 
            this.colSThe.AppearanceCell.Options.UseTextOptions = true;
            this.colSThe.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSThe.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSThe.AppearanceHeader.Options.UseTextOptions = true;
            this.colSThe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSThe.Caption = "Số Thẻ";
            this.colSThe.FieldName = "SThe";
            this.colSThe.Name = "colSThe";
            this.colSThe.Visible = true;
            this.colSThe.VisibleIndex = 3;
            this.colSThe.Width = 106;
            // 
            // colDTuong
            // 
            this.colDTuong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDTuong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDTuong.AppearanceHeader.Options.UseTextOptions = true;
            this.colDTuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDTuong.Caption = "Đối Tượng";
            this.colDTuong.FieldName = "DTuong";
            this.colDTuong.Name = "colDTuong";
            this.colDTuong.Width = 80;
            // 
            // colMaICD
            // 
            this.colMaICD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMaICD.Caption = "Mã ICD";
            this.colMaICD.FieldName = "MaICD";
            this.colMaICD.Name = "colMaICD";
            this.colMaICD.Visible = true;
            this.colMaICD.VisibleIndex = 4;
            this.colMaICD.Width = 47;
            // 
            // colDiaChi
            // 
            this.colDiaChi.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.colDiaChi.AppearanceCell.Options.UseForeColor = true;
            this.colDiaChi.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDiaChi.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiaChi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiaChi.Caption = "Ghi chú";
            this.colDiaChi.FieldName = "DChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 9;
            this.colDiaChi.Width = 189;
            // 
            // coNgayVao
            // 
            this.coNgayVao.AppearanceHeader.Options.UseTextOptions = true;
            this.coNgayVao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coNgayVao.Caption = "Ngày vào";
            this.coNgayVao.DisplayFormat.FormatString = "d";
            this.coNgayVao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.coNgayVao.FieldName = "NgayVao";
            this.coNgayVao.Name = "coNgayVao";
            this.coNgayVao.Visible = true;
            this.coNgayVao.VisibleIndex = 5;
            this.coNgayVao.Width = 63;
            // 
            // colNgayRa
            // 
            this.colNgayRa.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgayRa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayRa.Caption = "Ngày ra";
            this.colNgayRa.DisplayFormat.FormatString = "d";
            this.colNgayRa.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayRa.FieldName = "NgayRa";
            this.colNgayRa.Name = "colNgayRa";
            this.colNgayRa.Visible = true;
            this.colNgayRa.VisibleIndex = 6;
            this.colNgayRa.Width = 65;
            // 
            // colNgayTT
            // 
            this.colNgayTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgayTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayTT.Caption = "Ngày TT";
            this.colNgayTT.FieldName = "NgayTT";
            this.colNgayTT.Name = "colNgayTT";
            this.colNgayTT.Visible = true;
            this.colNgayTT.VisibleIndex = 7;
            this.colNgayTT.Width = 65;
            // 
            // colSoNgayDT
            // 
            this.colSoNgayDT.Caption = "Số ngày ĐT";
            this.colSoNgayDT.FieldName = "SoNgaydt";
            this.colSoNgayDT.Name = "colSoNgayDT";
            this.colSoNgayDT.Visible = true;
            this.colSoNgayDT.VisibleIndex = 8;
            this.colSoNgayDT.Width = 71;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSua);
            this.panelControl1.Controls.Add(this.btnIn);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 434);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(917, 30);
            this.panelControl1.TabIndex = 1;
            // 
            // btnSua
            // 
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Enabled = false;
            this.btnSua.Location = new System.Drawing.Point(706, 4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 23);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa ICD";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnIn
            // 
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.Location = new System.Drawing.Point(802, 3);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(103, 23);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "In Danh Sách";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.grcBNhan);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(917, 434);
            this.panelControl2.TabIndex = 2;
            // 
            // frm_BNSai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 464);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_BNSai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh nhân không hợp lệ";
            this.Load += new System.EventHandler(this.frm_BNSai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcBNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupGTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcBNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBN;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBN;
        private DevExpress.XtraGrid.Columns.GridColumn colGTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colSThe;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colDTuong;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupGTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colMaICD;
        private DevExpress.XtraGrid.Columns.GridColumn coNgayVao;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayRa;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayTT;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraGrid.Columns.GridColumn colSoNgayDT;
    }
}