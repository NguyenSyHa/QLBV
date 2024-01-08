namespace QLBV.FormThamSo
{
    partial class frm_BC_TKThuocTTBHYT_30009
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.ckBHYT = new DevExpress.XtraEditors.CheckEdit();
            this.ckDichVu = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.radDT = new DevExpress.XtraEditors.RadioGroup();
            this.rdTrongBH = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBHYT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongBH.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.Location = new System.Drawing.Point(74, 20);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Size = new System.Drawing.Size(202, 22);
            this.dtTuNgay.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(74, 46);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Size = new System.Drawing.Size(202, 22);
            this.dtDenNgay.TabIndex = 2;
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.btnThoat.Location = new System.Drawing.Point(198, 190);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 27);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnInBC.Location = new System.Drawing.Point(103, 190);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(89, 27);
            this.btnInBC.TabIndex = 5;
            this.btnInBC.Text = "In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // ckBHYT
            // 
            this.ckBHYT.EditValue = true;
            this.ckBHYT.Location = new System.Drawing.Point(74, 74);
            this.ckBHYT.Name = "ckBHYT";
            this.ckBHYT.Properties.Caption = "BHYT";
            this.ckBHYT.Size = new System.Drawing.Size(75, 19);
            this.ckBHYT.TabIndex = 3;
            // 
            // ckDichVu
            // 
            this.ckDichVu.Location = new System.Drawing.Point(201, 74);
            this.ckDichVu.Name = "ckDichVu";
            this.ckDichVu.Properties.Caption = "Dịch vụ";
            this.ckDichVu.Size = new System.Drawing.Size(75, 19);
            this.ckDichVu.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 91);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Đối tượng:";
            this.labelControl3.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // radDT
            // 
            this.radDT.Location = new System.Drawing.Point(71, 99);
            this.radDT.Name = "radDT";
            this.radDT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radDT.Size = new System.Drawing.Size(205, 28);
            this.radDT.TabIndex = 7;
            // 
            // rdTrongBH
            // 
            this.rdTrongBH.EditValue = "1";
            this.rdTrongBH.Location = new System.Drawing.Point(71, 133);
            this.rdTrongBH.Name = "rdTrongBH";
            this.rdTrongBH.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTrongBH.Properties.Appearance.Options.UseFont = true;
            this.rdTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Ngoài BHYT"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Trong BHYT")});
            this.rdTrongBH.Size = new System.Drawing.Size(205, 25);
            this.rdTrongBH.TabIndex = 8;
            // 
            // frm_BC_TKThuocTTBHYT_30009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.rdTrongBH);
            this.Controls.Add(this.radDT);
            this.Controls.Add(this.ckDichVu);
            this.Controls.Add(this.ckBHYT);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.dtTuNgay);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_TKThuocTTBHYT_30009";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê thuốc thanh toán BHYT";
            this.Load += new System.EventHandler(this.frm_BC_TKThuocTTBHYT_30009_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBHYT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTrongBH.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.CheckEdit ckBHYT;
        private DevExpress.XtraEditors.CheckEdit ckDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup radDT;
        private DevExpress.XtraEditors.RadioGroup rdTrongBH;
    }
}