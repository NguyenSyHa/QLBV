namespace QLBV.FormNhap
{
    partial class frm_DmDTBN
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DmDTBN));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lup_HTTT = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtid = new DevExpress.XtraEditors.TextEdit();
            this.txtmota = new DevExpress.XtraEditors.TextEdit();
            this.txtdoituong = new DevExpress.XtraEditors.TextEdit();
            this.cbo_Status = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.luusua = new DevExpress.XtraEditors.SimpleButton();
            this.sua1111 = new DevExpress.XtraEditors.SimpleButton();
            this.them = new DevExpress.XtraEditors.SimpleButton();
            this.xoa = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.danhsachdoituong = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HTTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.binS_DTBN = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_HTTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmota.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdoituong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_Status.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.danhsachdoituong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binS_DTBN)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.lup_HTTT);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtid);
            this.panelControl1.Controls.Add(this.txtmota);
            this.panelControl1.Controls.Add(this.txtdoituong);
            this.panelControl1.Controls.Add(this.cbo_Status);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(781, 85);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl5.Location = new System.Drawing.Point(466, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(71, 15);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Mức hưởng :";
            // 
            // lup_HTTT
            // 
            this.lup_HTTT.Location = new System.Drawing.Point(556, 21);
            this.lup_HTTT.Name = "lup_HTTT";
            this.lup_HTTT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lup_HTTT.Properties.Appearance.Options.UseFont = true;
            this.lup_HTTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_HTTT.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Mức Hưởng")});
            this.lup_HTTT.Properties.DisplayMember = "Ten";
            this.lup_HTTT.Properties.NullText = "";
            this.lup_HTTT.Properties.ReadOnly = true;
            this.lup_HTTT.Properties.ValueMember = "_HTTT";
            this.lup_HTTT.Size = new System.Drawing.Size(213, 22);
            this.lup_HTTT.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(468, 50);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(42, 15);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Status :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(12, 50);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 15);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Mô Tả :";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(187, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 15);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Đối tượng :";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 15);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "ID :";
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(59, 21);
            this.txtid.Name = "txtid";
            this.txtid.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtid.Properties.Appearance.Options.UseFont = true;
            this.txtid.Properties.Mask.EditMask = "\\d{0,2}";
            this.txtid.Properties.Mask.IgnoreMaskBlank = false;
            this.txtid.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtid.Properties.ReadOnly = true;
            this.txtid.Size = new System.Drawing.Size(122, 22);
            this.txtid.TabIndex = 2;
            // 
            // txtmota
            // 
            this.txtmota.Location = new System.Drawing.Point(59, 47);
            this.txtmota.Name = "txtmota";
            this.txtmota.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmota.Properties.Appearance.Options.UseFont = true;
            this.txtmota.Properties.ReadOnly = true;
            this.txtmota.Size = new System.Drawing.Size(401, 22);
            this.txtmota.TabIndex = 4;
            // 
            // txtdoituong
            // 
            this.txtdoituong.Location = new System.Drawing.Point(255, 21);
            this.txtdoituong.Name = "txtdoituong";
            this.txtdoituong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtdoituong.Properties.Appearance.Options.UseFont = true;
            this.txtdoituong.Properties.ReadOnly = true;
            this.txtdoituong.Size = new System.Drawing.Size(205, 22);
            this.txtdoituong.TabIndex = 3;
            // 
            // cbo_Status
            // 
            this.cbo_Status.Location = new System.Drawing.Point(556, 47);
            this.cbo_Status.Name = "cbo_Status";
            this.cbo_Status.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_Status.Properties.Appearance.Options.UseFont = true;
            this.cbo_Status.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_Status.Properties.Items.AddRange(new object[] {
            "Không sử dụng",
            "Sử dụng"});
            this.cbo_Status.Properties.PopupSizeable = true;
            this.cbo_Status.Properties.ReadOnly = true;
            this.cbo_Status.Properties.Tag = "";
            this.cbo_Status.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_Status.Size = new System.Drawing.Size(213, 22);
            this.cbo_Status.TabIndex = 5;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.luusua);
            this.panelControl2.Controls.Add(this.sua1111);
            this.panelControl2.Controls.Add(this.them);
            this.panelControl2.Controls.Add(this.xoa);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 406);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(781, 40);
            this.panelControl2.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(378, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "hủy";
            // 
            // luusua
            // 
            this.luusua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.luusua.Enabled = false;
            this.luusua.Image = ((System.Drawing.Image)(resources.GetObject("luusua.Image")));
            this.luusua.Location = new System.Drawing.Point(459, 12);
            this.luusua.Name = "luusua";
            this.luusua.Size = new System.Drawing.Size(75, 23);
            this.luusua.TabIndex = 0;
            this.luusua.Text = "Lưu";
            this.luusua.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // sua1111
            // 
            this.sua1111.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sua1111.Image = ((System.Drawing.Image)(resources.GetObject("sua1111.Image")));
            this.sua1111.Location = new System.Drawing.Point(621, 12);
            this.sua1111.Name = "sua1111";
            this.sua1111.Size = new System.Drawing.Size(75, 23);
            this.sua1111.TabIndex = 0;
            this.sua1111.Text = "Sửa";
            this.sua1111.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // them
            // 
            this.them.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.them.Image = ((System.Drawing.Image)(resources.GetObject("them.Image")));
            this.them.Location = new System.Drawing.Point(540, 12);
            this.them.Name = "them";
            this.them.Size = new System.Drawing.Size(75, 23);
            this.them.TabIndex = 2;
            this.them.Text = "Thêm";
            this.them.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // xoa
            // 
            this.xoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xoa.Image = ((System.Drawing.Image)(resources.GetObject("xoa.Image")));
            this.xoa.Location = new System.Drawing.Point(702, 12);
            this.xoa.Name = "xoa";
            this.xoa.Size = new System.Drawing.Size(75, 23);
            this.xoa.TabIndex = 4;
            this.xoa.Text = "xóa";
            this.xoa.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.danhsachdoituong);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 85);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(781, 321);
            this.panelControl3.TabIndex = 7;
            // 
            // danhsachdoituong
            // 
            this.danhsachdoituong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danhsachdoituong.Location = new System.Drawing.Point(2, 2);
            this.danhsachdoituong.MainView = this.gridView1;
            this.danhsachdoituong.Name = "danhsachdoituong";
            this.danhsachdoituong.Size = new System.Drawing.Size(777, 317);
            this.danhsachdoituong.TabIndex = 0;
            this.danhsachdoituong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idDT,
            this.DT,
            this.mota,
            this.status,
            this.HTTT});
            this.gridView1.GridControl = this.danhsachdoituong;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
            // 
            // idDT
            // 
            this.idDT.Caption = "IDDT";
            this.idDT.FieldName = "IDDTBN";
            this.idDT.MinWidth = 30;
            this.idDT.Name = "idDT";
            this.idDT.Visible = true;
            this.idDT.VisibleIndex = 0;
            this.idDT.Width = 68;
            // 
            // DT
            // 
            this.DT.Caption = "Đối Tượng";
            this.DT.FieldName = "DTBN1";
            this.DT.MinWidth = 200;
            this.DT.Name = "DT";
            this.DT.Visible = true;
            this.DT.VisibleIndex = 1;
            this.DT.Width = 200;
            // 
            // mota
            // 
            this.mota.Caption = "Mô Tả";
            this.mota.FieldName = "MoTa";
            this.mota.Name = "mota";
            this.mota.Visible = true;
            this.mota.VisibleIndex = 2;
            this.mota.Width = 197;
            // 
            // status
            // 
            this.status.Caption = "Trạng thái";
            this.status.FieldName = "Status";
            this.status.MinWidth = 60;
            this.status.Name = "status";
            this.status.Visible = true;
            this.status.VisibleIndex = 4;
            this.status.Width = 74;
            // 
            // HTTT
            // 
            this.HTTT.Caption = "Phân loại ĐT";
            this.HTTT.FieldName = "_HTTT";
            this.HTTT.Name = "HTTT";
            this.HTTT.Visible = true;
            this.HTTT.VisibleIndex = 3;
            this.HTTT.Width = 142;
            // 
            // frm_DmDTBN
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 446);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_DmDTBN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập Đối tượng bệnh nhân";
            this.Load += new System.EventHandler(this.frm_nhapdtbt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lup_HTTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmota.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdoituong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_Status.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.danhsachdoituong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binS_DTBN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtid;
        private DevExpress.XtraEditors.TextEdit txtmota;
        private DevExpress.XtraEditors.TextEdit txtdoituong;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton luusua;
        private DevExpress.XtraEditors.SimpleButton sua1111;
        private DevExpress.XtraEditors.SimpleButton them;
        private DevExpress.XtraEditors.SimpleButton xoa;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl danhsachdoituong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn idDT;
        private DevExpress.XtraGrid.Columns.GridColumn DT;
        private DevExpress.XtraGrid.Columns.GridColumn mota;
        private DevExpress.XtraGrid.Columns.GridColumn status;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.BindingSource binS_DTBN;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lup_HTTT;
        //private DevExpress.XtraGrid.Columns.GridColumn HTTT;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_Status;
        private DevExpress.XtraGrid.Columns.GridColumn HTTT;

    }
}