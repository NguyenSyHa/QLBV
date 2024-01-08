namespace QLBV.FormNhap
{
    partial class frm_UpdateDonGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UpdateDonGia));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tex_spl = new DevExpress.XtraEditors.TextEdit();
            this.lou_kho = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tex_ton = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.tex_tendv = new DevExpress.XtraEditors.TextEdit();
            this.tex_madv = new DevExpress.XtraEditors.TextEdit();
            this.gct_dthuocct = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.maduoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Donvi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dongia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.soluong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.thanhtien = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tex_spl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lou_kho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tex_tendv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tex_madv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gct_dthuocct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.tex_spl);
            this.panelControl2.Controls.Add(this.lou_kho);
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.tex_ton);
            this.panelControl2.Controls.Add(this.lookUpEdit1);
            this.panelControl2.Controls.Add(this.tex_tendv);
            this.panelControl2.Controls.Add(this.tex_madv);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1080, 70);
            this.panelControl2.TabIndex = 4;
            // 
            // tex_spl
            // 
            this.tex_spl.Location = new System.Drawing.Point(25, 24);
            this.tex_spl.Name = "tex_spl";
            this.tex_spl.Properties.ReadOnly = true;
            this.tex_spl.Size = new System.Drawing.Size(37, 20);
            this.tex_spl.TabIndex = 12;
            // 
            // lou_kho
            // 
            this.lou_kho.Location = new System.Drawing.Point(861, 25);
            this.lou_kho.Name = "lou_kho";
            this.lou_kho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lou_kho.Properties.DisplayMember = "TenKP";
            this.lou_kho.Properties.ValueMember = "MaKP";
            this.lou_kho.Size = new System.Drawing.Size(119, 20);
            this.lou_kho.TabIndex = 11;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(986, 23);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(89, 23);
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "update ";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(209, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Tên Dược :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(69, 28);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Mã được :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(460, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Đơn Giá update:";
            // 
            // tex_ton
            // 
            this.tex_ton.Appearance.ForeColor = System.Drawing.Color.Red;
            this.tex_ton.Location = new System.Drawing.Point(734, 28);
            this.tex_ton.Name = "tex_ton";
            this.tex_ton.Size = new System.Drawing.Size(63, 13);
            this.tex_ton.TabIndex = 4;
            this.tex_ton.Text = "Số lượng Tồn";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(545, 24);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.DisplayMember = "DonGia";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "soton";
            this.lookUpEdit1.Size = new System.Drawing.Size(171, 20);
            this.lookUpEdit1.TabIndex = 3;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // tex_tendv
            // 
            this.tex_tendv.Location = new System.Drawing.Point(268, 24);
            this.tex_tendv.Name = "tex_tendv";
            this.tex_tendv.Properties.ReadOnly = true;
            this.tex_tendv.Size = new System.Drawing.Size(173, 20);
            this.tex_tendv.TabIndex = 1;
            // 
            // tex_madv
            // 
            this.tex_madv.Location = new System.Drawing.Point(121, 24);
            this.tex_madv.Name = "tex_madv";
            this.tex_madv.Properties.ReadOnly = true;
            this.tex_madv.Size = new System.Drawing.Size(76, 20);
            this.tex_madv.TabIndex = 0;
            // 
            // gct_dthuocct
            // 
            this.gct_dthuocct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_dthuocct.Location = new System.Drawing.Point(0, 70);
            this.gct_dthuocct.MainView = this.gridView1;
            this.gct_dthuocct.Name = "gct_dthuocct";
            this.gct_dthuocct.Size = new System.Drawing.Size(1080, 379);
            this.gct_dthuocct.TabIndex = 5;
            this.gct_dthuocct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.maduoc,
            this.TenDV,
            this.Donvi,
            this.dongia,
            this.soluong,
            this.thanhtien});
            this.gridView1.GridControl = this.gct_dthuocct;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
            // 
            // maduoc
            // 
            this.maduoc.Caption = "Mã Dược";
            this.maduoc.FieldName = "MaDV";
            this.maduoc.Name = "maduoc";
            this.maduoc.Visible = true;
            this.maduoc.VisibleIndex = 0;
            // 
            // TenDV
            // 
            this.TenDV.Caption = "Tên Thuốc";
            this.TenDV.FieldName = "TenDV";
            this.TenDV.Name = "TenDV";
            this.TenDV.Visible = true;
            this.TenDV.VisibleIndex = 1;
            // 
            // Donvi
            // 
            this.Donvi.Caption = "Đơn Vị";
            this.Donvi.FieldName = "DonVi";
            this.Donvi.Name = "Donvi";
            this.Donvi.Visible = true;
            this.Donvi.VisibleIndex = 2;
            // 
            // dongia
            // 
            this.dongia.Caption = "Đơn Giá Sai";
            this.dongia.FieldName = "DonGia";
            this.dongia.Name = "dongia";
            this.dongia.Visible = true;
            this.dongia.VisibleIndex = 3;
            // 
            // soluong
            // 
            this.soluong.Caption = "Số Lượng";
            this.soluong.FieldName = "SoLuong";
            this.soluong.Name = "soluong";
            this.soluong.Visible = true;
            this.soluong.VisibleIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(19, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "SP :";
            // 
            // thanhtien
            // 
            this.thanhtien.Caption = "Thành tiền";
            this.thanhtien.FieldName = "ThanhTien";
            this.thanhtien.Name = "thanhtien";
            this.thanhtien.Visible = true;
            this.thanhtien.VisibleIndex = 5;
            // 
            // frm_UpdateDonGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 449);
            this.Controls.Add(this.gct_dthuocct);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_UpdateDonGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "thay doi";
            this.Load += new System.EventHandler(this.frm_UpdateDonGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tex_spl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lou_kho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tex_tendv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tex_madv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gct_dthuocct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl tex_ton;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.TextEdit tex_tendv;
        private DevExpress.XtraEditors.TextEdit tex_madv;
        private DevExpress.XtraGrid.GridControl gct_dthuocct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn maduoc;
        private DevExpress.XtraGrid.Columns.GridColumn TenDV;
        private DevExpress.XtraGrid.Columns.GridColumn Donvi;
        private DevExpress.XtraGrid.Columns.GridColumn dongia;
        private DevExpress.XtraGrid.Columns.GridColumn soluong;
        private DevExpress.XtraEditors.LookUpEdit lou_kho;
        private DevExpress.XtraEditors.TextEdit tex_spl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn thanhtien;



    }
}