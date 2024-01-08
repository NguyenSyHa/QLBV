namespace QLBV.ChucNang
{
    partial class frm_ThanhToanLai
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
            this.rad_ExPort = new DevExpress.XtraEditors.RadioGroup();
            this.hyp_HuyChon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyp_Chon = new DevExpress.XtraEditors.HyperLinkEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.dtTimDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtTimTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grc_Export_XML_2348 = new DevExpress.XtraGrid.GridControl();
            this.grv_Export_XML_2348 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chk_chon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colNgayra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.colMaGD_BHXH = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rad_ExPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_Export_XML_2348)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_Export_XML_2348)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_chon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.rad_ExPort);
            this.panelControl1.Controls.Add(this.hyp_HuyChon);
            this.panelControl1.Controls.Add(this.hyp_Chon);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.dtTimDenNgay);
            this.panelControl1.Controls.Add(this.dtTimTuNgay);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(664, 81);
            this.panelControl1.TabIndex = 0;
            // 
            // rad_ExPort
            // 
            this.rad_ExPort.EditValue = ((short)(0));
            this.rad_ExPort.Location = new System.Drawing.Point(78, 35);
            this.rad_ExPort.Name = "rad_ExPort";
            this.rad_ExPort.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rad_ExPort.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_ExPort.Properties.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rad_ExPort.Properties.Appearance.Options.UseBackColor = true;
            this.rad_ExPort.Properties.Appearance.Options.UseFont = true;
            this.rad_ExPort.Properties.Appearance.Options.UseForeColor = true;
            this.rad_ExPort.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Chưa TT lại"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Đã TT lại")});
            this.rad_ExPort.Size = new System.Drawing.Size(309, 23);
            this.rad_ExPort.TabIndex = 49;
            this.rad_ExPort.SelectedIndexChanged += new System.EventHandler(this.rad_ExPort_SelectedIndexChanged);
            // 
            // hyp_HuyChon
            // 
            this.hyp_HuyChon.EditValue = "Bỏ chọn";
            this.hyp_HuyChon.Location = new System.Drawing.Point(109, 55);
            this.hyp_HuyChon.Name = "hyp_HuyChon";
            this.hyp_HuyChon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_HuyChon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_HuyChon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_HuyChon.Properties.Appearance.Options.UseFont = true;
            this.hyp_HuyChon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_HuyChon.Size = new System.Drawing.Size(70, 20);
            this.hyp_HuyChon.TabIndex = 40;
            this.hyp_HuyChon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_HuyChon_OpenLink);
            // 
            // hyp_Chon
            // 
            this.hyp_Chon.EditValue = "Chọn tất cả";
            this.hyp_Chon.Location = new System.Drawing.Point(5, 55);
            this.hyp_Chon.Name = "hyp_Chon";
            this.hyp_Chon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyp_Chon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hyp_Chon.Properties.Appearance.Options.UseBackColor = true;
            this.hyp_Chon.Properties.Appearance.Options.UseFont = true;
            this.hyp_Chon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyp_Chon.Size = new System.Drawing.Size(70, 20);
            this.hyp_Chon.TabIndex = 39;
            this.hyp_Chon.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyp_Chon_OpenLink);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl12.Location = new System.Drawing.Point(2, 8);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(56, 18);
            this.labelControl12.TabIndex = 37;
            this.labelControl12.Text = "Ngày từ:";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl9.Location = new System.Drawing.Point(215, 8);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(28, 18);
            this.labelControl9.TabIndex = 38;
            this.labelControl9.Text = "đến:";
            // 
            // dtTimDenNgay
            // 
            this.dtTimDenNgay.EditValue = null;
            this.dtTimDenNgay.EnterMoveNextControl = true;
            this.dtTimDenNgay.Location = new System.Drawing.Point(256, 5);
            this.dtTimDenNgay.Name = "dtTimDenNgay";
            this.dtTimDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTimDenNgay.Properties.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.dtTimDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTimDenNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtTimDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimDenNgay.Size = new System.Drawing.Size(131, 24);
            this.dtTimDenNgay.TabIndex = 36;
            this.dtTimDenNgay.EditValueChanged += new System.EventHandler(this.dtTimDenNgay_EditValueChanged);
            // 
            // dtTimTuNgay
            // 
            this.dtTimTuNgay.EditValue = null;
            this.dtTimTuNgay.EnterMoveNextControl = true;
            this.dtTimTuNgay.Location = new System.Drawing.Point(78, 5);
            this.dtTimTuNgay.Name = "dtTimTuNgay";
            this.dtTimTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTimTuNgay.Properties.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.dtTimTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTimTuNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtTimTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimTuNgay.Size = new System.Drawing.Size(131, 24);
            this.dtTimTuNgay.TabIndex = 35;
            this.dtTimTuNgay.EditValueChanged += new System.EventHandler(this.dtTimTuNgay_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(436, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(95, 73);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Thanh Toán";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.progressBarControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 375);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(664, 26);
            this.panelControl2.TabIndex = 1;
            // 
            // grc_Export_XML_2348
            // 
            this.grc_Export_XML_2348.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_Export_XML_2348.Location = new System.Drawing.Point(2, 2);
            this.grc_Export_XML_2348.MainView = this.grv_Export_XML_2348;
            this.grc_Export_XML_2348.Name = "grc_Export_XML_2348";
            this.grc_Export_XML_2348.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.chk_chon});
            this.grc_Export_XML_2348.Size = new System.Drawing.Size(660, 290);
            this.grc_Export_XML_2348.TabIndex = 1;
            this.grc_Export_XML_2348.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_Export_XML_2348});
            // 
            // grv_Export_XML_2348
            // 
            this.grv_Export_XML_2348.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grv_Export_XML_2348.Appearance.FooterPanel.Options.UseFont = true;
            this.grv_Export_XML_2348.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.grv_Export_XML_2348.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grv_Export_XML_2348.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grv_Export_XML_2348.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grv_Export_XML_2348.Appearance.GroupFooter.Options.UseFont = true;
            this.grv_Export_XML_2348.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grv_Export_XML_2348.Appearance.GroupPanel.Options.UseFont = true;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grv_Export_XML_2348.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Brown;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grv_Export_XML_2348.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grv_Export_XML_2348.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_Export_XML_2348.Appearance.Row.Options.UseFont = true;
            this.grv_Export_XML_2348.ColumnPanelRowHeight = 40;
            this.grv_Export_XML_2348.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.colTenBNhan,
            this.colChon,
            this.colNgayra,
            this.colNgayTT,
            this.colMaGD_BHXH});
            this.grv_Export_XML_2348.GridControl = this.grc_Export_XML_2348;
            this.grv_Export_XML_2348.Name = "grv_Export_XML_2348";
            this.grv_Export_XML_2348.OptionsView.EnableAppearanceEvenRow = true;
            this.grv_Export_XML_2348.OptionsView.EnableAppearanceOddRow = true;
            this.grv_Export_XML_2348.OptionsView.ShowFooter = true;
            this.grv_Export_XML_2348.OptionsView.ShowGroupPanel = false;
            // 
            // STT
            // 
            this.STT.Caption = "TT";
            this.STT.Name = "STT";
            this.STT.Visible = true;
            this.STT.VisibleIndex = 0;
            this.STT.Width = 35;
            // 
            // colTenBNhan
            // 
            this.colTenBNhan.Caption = "Mã Bệnh Nhân";
            this.colTenBNhan.FieldName = "MaBNhan";
            this.colTenBNhan.Name = "colTenBNhan";
            this.colTenBNhan.OptionsColumn.AllowFocus = false;
            this.colTenBNhan.OptionsColumn.ReadOnly = true;
            this.colTenBNhan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colTenBNhan.Visible = true;
            this.colTenBNhan.VisibleIndex = 2;
            this.colTenBNhan.Width = 185;
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.chk_chon;
            this.colChon.FieldName = "ExportBYT";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 1;
            this.colChon.Width = 74;
            // 
            // chk_chon
            // 
            this.chk_chon.AutoHeight = false;
            this.chk_chon.Caption = "Check";
            this.chk_chon.Name = "chk_chon";
            // 
            // colNgayra
            // 
            this.colNgayra.Caption = "Ngày ra viện";
            this.colNgayra.FieldName = "NgayRa";
            this.colNgayra.Name = "colNgayra";
            this.colNgayra.OptionsColumn.AllowFocus = false;
            this.colNgayra.OptionsColumn.ReadOnly = true;
            this.colNgayra.Visible = true;
            this.colNgayra.VisibleIndex = 3;
            this.colNgayra.Width = 123;
            // 
            // colNgayTT
            // 
            this.colNgayTT.Caption = "Ngày TT";
            this.colNgayTT.FieldName = "NgayTT";
            this.colNgayTT.Name = "colNgayTT";
            this.colNgayTT.Visible = true;
            this.colNgayTT.VisibleIndex = 4;
            this.colNgayTT.Width = 104;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.NullText = "Xem";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarControl1.Location = new System.Drawing.Point(2, 2);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(660, 22);
            this.progressBarControl1.TabIndex = 54;
            this.progressBarControl1.Visible = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grc_Export_XML_2348);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 81);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(664, 294);
            this.panelControl3.TabIndex = 2;
            // 
            // colMaGD_BHXH
            // 
            this.colMaGD_BHXH.Caption = "Thanh toán lại";
            this.colMaGD_BHXH.FieldName = "MaGD_BHXH";
            this.colMaGD_BHXH.Name = "colMaGD_BHXH";
            this.colMaGD_BHXH.Visible = true;
            this.colMaGD_BHXH.VisibleIndex = 5;
            // 
            // frm_ThanhToanLai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 401);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ThanhToanLai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_ThanhToanLai";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_ThanhToanLai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rad_ExPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_HuyChon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyp_Chon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_Export_XML_2348)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_Export_XML_2348)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_chon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.DateEdit dtTimDenNgay;
        private DevExpress.XtraEditors.DateEdit dtTimTuNgay;
        private DevExpress.XtraGrid.GridControl grc_Export_XML_2348;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_Export_XML_2348;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chk_chon;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayra;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_HuyChon;
        private DevExpress.XtraEditors.HyperLinkEdit hyp_Chon;
        private DevExpress.XtraEditors.RadioGroup rad_ExPort;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn colMaGD_BHXH;
    }
}