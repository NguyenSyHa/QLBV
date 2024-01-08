namespace QLBV.FormThamSo
{
    partial class frm_BC_HDDieuTri_30012
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
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.ck_KhoaYHCT = new DevExpress.XtraEditors.CheckEdit();
            this.ck_MS02 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_KhoaYHCT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_MS02.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(85, 66);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên Khoa"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP")});
            this.lupKho.Properties.NullText = "";
            this.lupKho.Size = new System.Drawing.Size(187, 22);
            this.lupKho.TabIndex = 191;
            this.lupKho.EditValueChanged += new System.EventHandler(this.lupKho_EditValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(8, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 188;
            this.label5.Text = "Khoa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(8, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 187;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 186;
            this.label2.Text = "Từ ngày:";
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(85, 39);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lupDenNgay.Properties.Appearance.Options.UseFont = true;
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(187, 22);
            this.lupDenNgay.TabIndex = 185;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(85, 12);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lupTuNgay.Properties.Appearance.Options.UseFont = true;
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(187, 22);
            this.lupTuNgay.TabIndex = 184;
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.btnThoat.Location = new System.Drawing.Point(197, 159);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 25);
            this.btnThoat.TabIndex = 189;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnInBC.Location = new System.Drawing.Point(85, 159);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(91, 25);
            this.btnInBC.TabIndex = 190;
            this.btnInBC.Text = "In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // ck_KhoaYHCT
            // 
            this.ck_KhoaYHCT.Location = new System.Drawing.Point(85, 94);
            this.ck_KhoaYHCT.Name = "ck_KhoaYHCT";
            this.ck_KhoaYHCT.Properties.Caption = "In mẫu của khoa YHCT";
            this.ck_KhoaYHCT.Size = new System.Drawing.Size(187, 19);
            this.ck_KhoaYHCT.TabIndex = 192;
            this.ck_KhoaYHCT.CheckedChanged += new System.EventHandler(this.ck_KhoaYHCT_CheckedChanged);
            // 
            // ck_MS02
            // 
            this.ck_MS02.Location = new System.Drawing.Point(85, 119);
            this.ck_MS02.Name = "ck_MS02";
            this.ck_MS02.Properties.Caption = "BC hoạt động điều trị (MS 02)";
            this.ck_MS02.Size = new System.Drawing.Size(187, 19);
            this.ck_MS02.TabIndex = 192;
            this.ck_MS02.CheckedChanged += new System.EventHandler(this.ck_MS02_CheckedChanged);
            // 
            // frm_BC_HDDieuTri_30012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.ck_MS02);
            this.Controls.Add(this.ck_KhoaYHCT);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_HDDieuTri_30012";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo hoạt động điều trị";
            this.Load += new System.EventHandler(this.frm_BC_HDDieuTri_30009_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_KhoaYHCT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ck_MS02.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.CheckEdit ck_KhoaYHCT;
        private DevExpress.XtraEditors.CheckEdit ck_MS02;
    }
}