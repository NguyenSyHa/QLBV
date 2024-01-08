namespace QLBV.FormThamSo
{
    partial class frm_BCRV
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
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnTAORV = new DevExpress.XtraEditors.SimpleButton();
            this.cklKhoaPhong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(390, 208);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(75, 23);
            this.btnhuy.TabIndex = 12;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // btnTAORV
            // 
            this.btnTAORV.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTAORV.Appearance.Options.UseFont = true;
            this.btnTAORV.Location = new System.Drawing.Point(304, 208);
            this.btnTAORV.Name = "btnTAORV";
            this.btnTAORV.Size = new System.Drawing.Size(75, 23);
            this.btnTAORV.TabIndex = 11;
            this.btnTAORV.Text = "Tạo BC";
            this.btnTAORV.Click += new System.EventHandler(this.btnTAORV_Click);
            // 
            // cklKhoaPhong
            // 
            this.cklKhoaPhong.CheckOnClick = true;
            this.cklKhoaPhong.Cursor = System.Windows.Forms.Cursors.Default;
            this.cklKhoaPhong.DisplayMember = "TenKP";
            this.cklKhoaPhong.Location = new System.Drawing.Point(94, 34);
            this.cklKhoaPhong.Name = "cklKhoaPhong";
            this.cklKhoaPhong.Size = new System.Drawing.Size(371, 168);
            this.cklKhoaPhong.TabIndex = 10;
            this.cklKhoaPhong.ValueMember = "MaKP";
            this.cklKhoaPhong.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklKhoaPhong_ItemCheck);
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(320, 7);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(145, 24);
            this.dedenngay.TabIndex = 9;
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(94, 7);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(145, 24);
            this.detungay.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(248, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(4, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 17);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Khoa|Phòng:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // frm_BCRV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 236);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnTAORV);
            this.Controls.Add(this.cklKhoaPhong);
            this.Controls.Add(this.dedenngay);
            this.Controls.Add(this.detungay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BCRV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo bệnh nhân BHYT ra viện";
            this.Load += new System.EventHandler(this.frm_BCRV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cklKhoaPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.SimpleButton btnTAORV;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKhoaPhong;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}