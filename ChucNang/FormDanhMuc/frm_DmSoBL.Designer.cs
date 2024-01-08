namespace QLBV.FormDanhMuc
{
    partial class frm_DmSoBL
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
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoi = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cboPLoai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoHT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDenSo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTuSo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtQuyen = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lupMaCB = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcDMSoBL = new DevExpress.XtraGrid.GridControl();
            this.grvDMSoBL = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colQuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoDen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colMaCB = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenSo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuSo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDMSoBL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDMSoBL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DisplayMember = "TenCB";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "MaCB";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnXoa);
            this.groupControl1.Controls.Add(this.btnLuu);
            this.groupControl1.Controls.Add(this.btnSua);
            this.groupControl1.Controls.Add(this.btnMoi);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.cboPLoai);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtSoHT);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtDenSo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtTuSo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtQuyen);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtStatus);
            this.groupControl1.Controls.Add(this.lupMaCB);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(832, 124);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Chi tiết danh mục biên lai";
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Location = new System.Drawing.Point(601, 83);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 19;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click_1);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Location = new System.Drawing.Point(678, 83);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 18;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Location = new System.Drawing.Point(524, 83);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 16;
            this.btnSua.Text = "&Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnMoi.Appearance.Options.UseFont = true;
            this.btnMoi.Location = new System.Drawing.Point(447, 83);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(75, 23);
            this.btnMoi.TabIndex = 15;
            this.btnMoi.Text = "&Thêm mới";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl7.Location = new System.Drawing.Point(14, 26);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(58, 19);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Phân loại:";
            // 
            // cboPLoai
            // 
            this.cboPLoai.Location = new System.Drawing.Point(92, 23);
            this.cboPLoai.Name = "cboPLoai";
            this.cboPLoai.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboPLoai.Properties.Appearance.Options.UseFont = true;
            this.cboPLoai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPLoai.Properties.Items.AddRange(new object[] {
            "0: Biên lai tạm thu",
            "1: Biên lai thu",
            "2: Biên lai chi",
            "3. Sổ chứng sinh"});
            this.cboPLoai.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPLoai.Size = new System.Drawing.Size(130, 26);
            this.cboPLoai.TabIndex = 0;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl6.Location = new System.Drawing.Point(402, 54);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(100, 19);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Cán bộ sử dụng:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(228, 54);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 19);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Trạng thái:";
            // 
            // txtSoHT
            // 
            this.txtSoHT.Location = new System.Drawing.Point(92, 51);
            this.txtSoHT.Name = "txtSoHT";
            this.txtSoHT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoHT.Properties.Appearance.Options.UseFont = true;
            this.txtSoHT.Properties.Mask.EditMask = "\\d{0,10}";
            this.txtSoHT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSoHT.Size = new System.Drawing.Size(130, 26);
            this.txtSoHT.TabIndex = 4;
            this.txtSoHT.EditValueChanged += new System.EventHandler(this.txtSoHT_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(14, 54);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 19);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Số hiện tại:";
            // 
            // txtDenSo
            // 
            this.txtDenSo.Location = new System.Drawing.Point(653, 23);
            this.txtDenSo.Name = "txtDenSo";
            this.txtDenSo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDenSo.Properties.Appearance.Options.UseFont = true;
            this.txtDenSo.Properties.Mask.EditMask = "\\d{0,10}";
            this.txtDenSo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDenSo.Size = new System.Drawing.Size(100, 26);
            this.txtDenSo.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(590, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 19);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Đến số:";
            // 
            // txtTuSo
            // 
            this.txtTuSo.Location = new System.Drawing.Point(475, 23);
            this.txtTuSo.Name = "txtTuSo";
            this.txtTuSo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTuSo.Properties.Appearance.Options.UseFont = true;
            this.txtTuSo.Properties.Mask.EditMask = "\\d{0,10}";
            this.txtTuSo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTuSo.Size = new System.Drawing.Size(100, 26);
            this.txtTuSo.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(402, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(39, 19);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Từ số:";
            // 
            // txtQuyen
            // 
            this.txtQuyen.Location = new System.Drawing.Point(296, 23);
            this.txtQuyen.Name = "txtQuyen";
            this.txtQuyen.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtQuyen.Properties.Appearance.Options.UseFont = true;
            this.txtQuyen.Size = new System.Drawing.Size(100, 26);
            this.txtQuyen.TabIndex = 1;
            this.txtQuyen.Leave += new System.EventHandler(this.txtQuyen_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(239, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Quyển:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(296, 51);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtStatus.Properties.Appearance.Options.UseFont = true;
            this.txtStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStatus.Properties.Items.AddRange(new object[] {
            "Không sử dụng",
            "Sử dụng",
            "Đã sử dụng"});
            this.txtStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtStatus.Size = new System.Drawing.Size(100, 26);
            this.txtStatus.TabIndex = 5;
            // 
            // lupMaCB
            // 
            this.lupMaCB.Location = new System.Drawing.Point(536, 51);
            this.lupMaCB.Name = "lupMaCB";
            this.lupMaCB.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaCB.Properties.Appearance.Options.UseFont = true;
            this.lupMaCB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaCB.Properties.DisplayMember = "TenCB";
            this.lupMaCB.Properties.NullText = "";
            this.lupMaCB.Properties.PopupSizeable = false;
            this.lupMaCB.Properties.ValueMember = "MaCB";
            this.lupMaCB.Size = new System.Drawing.Size(217, 26);
            this.lupMaCB.TabIndex = 6;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcDMSoBL);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 124);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(832, 295);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Danh sách ";
            // 
            // grcDMSoBL
            // 
            this.grcDMSoBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDMSoBL.Location = new System.Drawing.Point(2, 20);
            this.grcDMSoBL.MainView = this.grvDMSoBL;
            this.grcDMSoBL.Name = "grcDMSoBL";
            this.grcDMSoBL.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboStatus,
            this.repositoryItemComboBox1});
            this.grcDMSoBL.Size = new System.Drawing.Size(828, 273);
            this.grcDMSoBL.TabIndex = 0;
            this.grcDMSoBL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDMSoBL});
            // 
            // grvDMSoBL
            // 
            this.grvDMSoBL.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDMSoBL.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grvDMSoBL.Appearance.FocusedRow.Options.UseFont = true;
            this.grvDMSoBL.Appearance.FocusedRow.Options.UseForeColor = true;
            this.grvDMSoBL.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvDMSoBL.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.grvDMSoBL.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDMSoBL.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.grvDMSoBL.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.grvDMSoBL.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDMSoBL.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDMSoBL.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDMSoBL.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDMSoBL.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDMSoBL.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDMSoBL.Appearance.Row.Options.UseFont = true;
            this.grvDMSoBL.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPLoai,
            this.colQuyen,
            this.colSoTu,
            this.colSoDen,
            this.colSoHT,
            this.colStatus,
            this.colMaCB});
            this.grvDMSoBL.GridControl = this.grcDMSoBL;
            this.grvDMSoBL.Name = "grvDMSoBL";
            this.grvDMSoBL.OptionsView.ColumnAutoWidth = false;
            this.grvDMSoBL.OptionsView.ShowGroupPanel = false;
            this.grvDMSoBL.OptionsView.ShowViewCaption = true;
            this.grvDMSoBL.ViewCaption = "Danh mục quyển - Số biên lai";
            this.grvDMSoBL.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDMSoBL_FocusedRowChanged);
            this.grvDMSoBL.DataSourceChanged += new System.EventHandler(this.grvDMSoBL_DataSourceChanged);
            // 
            // colPLoai
            // 
            this.colPLoai.Caption = "Phân loại";
            this.colPLoai.ColumnEdit = this.repositoryItemComboBox1;
            this.colPLoai.FieldName = "PLoai";
            this.colPLoai.Name = "colPLoai";
            this.colPLoai.Visible = true;
            this.colPLoai.VisibleIndex = 0;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "0: biên lai tạm thu",
            "1: biên lai thu",
            "2: biên lai chi"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // colQuyen
            // 
            this.colQuyen.AppearanceCell.Options.UseTextOptions = true;
            this.colQuyen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuyen.Caption = "Quyển";
            this.colQuyen.FieldName = "Quyen";
            this.colQuyen.Name = "colQuyen";
            this.colQuyen.Visible = true;
            this.colQuyen.VisibleIndex = 1;
            this.colQuyen.Width = 122;
            // 
            // colSoTu
            // 
            this.colSoTu.AppearanceCell.Options.UseTextOptions = true;
            this.colSoTu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSoTu.Caption = "Từ số";
            this.colSoTu.FieldName = "SoTu";
            this.colSoTu.Name = "colSoTu";
            this.colSoTu.Visible = true;
            this.colSoTu.VisibleIndex = 2;
            // 
            // colSoDen
            // 
            this.colSoDen.AppearanceCell.Options.UseTextOptions = true;
            this.colSoDen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSoDen.Caption = "Đến số";
            this.colSoDen.FieldName = "SoDen";
            this.colSoDen.Name = "colSoDen";
            this.colSoDen.Visible = true;
            this.colSoDen.VisibleIndex = 3;
            // 
            // colSoHT
            // 
            this.colSoHT.AppearanceCell.Options.UseTextOptions = true;
            this.colSoHT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSoHT.Caption = "Số hiện tại";
            this.colSoHT.FieldName = "SoHT";
            this.colSoHT.Name = "colSoHT";
            this.colSoHT.Visible = true;
            this.colSoHT.VisibleIndex = 4;
            this.colSoHT.Width = 109;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.ColumnEdit = this.cboStatus;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 5;
            this.colStatus.Width = 131;
            // 
            // cboStatus
            // 
            this.cboStatus.AutoHeight = false;
            this.cboStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Items.AddRange(new object[] {
            "Không sử dụng",
            "Sử dụng",
            "Đã sử dụng"});
            this.cboStatus.Name = "cboStatus";
            // 
            // colMaCB
            // 
            this.colMaCB.Caption = "Cán bộ sử dụng";
            this.colMaCB.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colMaCB.FieldName = "MaCB";
            this.colMaCB.Name = "colMaCB";
            this.colMaCB.Visible = true;
            this.colMaCB.VisibleIndex = 6;
            this.colMaCB.Width = 201;
            // 
            // frm_DmSoBL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 419);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DmSoBL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục biên lai";
            this.Load += new System.EventHandler(this.frm_DmSoBL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDenSo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTuSo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDMSoBL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDMSoBL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcDMSoBL;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDMSoBL;
        private DevExpress.XtraGrid.Columns.GridColumn colPLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colQuyen;
        private DevExpress.XtraGrid.Columns.GridColumn colSoTu;
        private DevExpress.XtraGrid.Columns.GridColumn colSoDen;
        private DevExpress.XtraGrid.Columns.GridColumn colSoHT;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colMaCB;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit cboPLoai;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtSoHT;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDenSo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTuSo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtQuyen;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit txtStatus;
        private DevExpress.XtraEditors.LookUpEdit lupMaCB;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnMoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
    }
}