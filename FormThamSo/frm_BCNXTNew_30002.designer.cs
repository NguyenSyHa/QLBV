namespace QLBV.FormThamSo
{
    partial class frm_BCNXTNew_30002
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.detungay = new DevExpress.XtraEditors.DateEdit();
            this.dedenngay = new DevExpress.XtraEditors.DateEdit();
            this.lupkhotong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cklKP = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnthoat = new DevExpress.XtraEditors.SimpleButton();
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.lupLoaiDuoc = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhotong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupLoaiDuoc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl2.Location = new System.Drawing.Point(219, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // detungay
            // 
            this.detungay.EditValue = null;
            this.detungay.Location = new System.Drawing.Point(76, 10);
            this.detungay.Name = "detungay";
            this.detungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detungay.Properties.Appearance.Options.UseFont = true;
            this.detungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.detungay.Size = new System.Drawing.Size(141, 24);
            this.detungay.TabIndex = 0;
            // 
            // dedenngay
            // 
            this.dedenngay.EditValue = null;
            this.dedenngay.Location = new System.Drawing.Point(291, 10);
            this.dedenngay.Name = "dedenngay";
            this.dedenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dedenngay.Properties.Appearance.Options.UseFont = true;
            this.dedenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dedenngay.Size = new System.Drawing.Size(152, 24);
            this.dedenngay.TabIndex = 1;
            // 
            // lupkhotong
            // 
            this.lupkhotong.Location = new System.Drawing.Point(76, 60);
            this.lupkhotong.Name = "lupkhotong";
            this.lupkhotong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupkhotong.Properties.Appearance.Options.UseFont = true;
            this.lupkhotong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupkhotong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", 5, "Mã Kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 30, "Tên Kho")});
            this.lupkhotong.Properties.DisplayMember = "TenKP";
            this.lupkhotong.Properties.NullText = "";
            this.lupkhotong.Properties.ValueMember = "MaKP";
            this.lupkhotong.Size = new System.Drawing.Size(367, 24);
            this.lupkhotong.TabIndex = 3;
            this.lupkhotong.EditValueChanged += new System.EventHandler(this.lupkhotong_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl3.Location = new System.Drawing.Point(4, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Kho tổng:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl4.Location = new System.Drawing.Point(18, 84);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 17);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Kho xã:";
            // 
            // cklKP
            // 
            this.cklKP.CheckOnClick = true;
            this.cklKP.DisplayMember = "TenKP";
            this.cklKP.Location = new System.Drawing.Point(76, 85);
            this.cklKP.MultiColumn = true;
            this.cklKP.Name = "cklKP";
            this.cklKP.Size = new System.Drawing.Size(367, 185);
            this.cklKP.TabIndex = 4;
            this.cklKP.ValueMember = "MaKP";
            // 
            // btnthoat
            // 
            this.btnthoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthoat.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnthoat.Appearance.Options.UseFont = true;
            this.btnthoat.Appearance.Options.UseForeColor = true;
            this.btnthoat.Location = new System.Drawing.Point(368, 276);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(75, 23);
            this.btnthoat.TabIndex = 6;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Appearance.Options.UseForeColor = true;
            this.btntaobc.Location = new System.Drawing.Point(287, 276);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(75, 23);
            this.btntaobc.TabIndex = 5;
            this.btntaobc.Text = "Tạo BC";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // lupLoaiDuoc
            // 
            this.lupLoaiDuoc.Location = new System.Drawing.Point(76, 35);
            this.lupLoaiDuoc.Name = "lupLoaiDuoc";
            this.lupLoaiDuoc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupLoaiDuoc.Properties.Appearance.Options.UseFont = true;
            this.lupLoaiDuoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupLoaiDuoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", 200, "Tên nhóm"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDNhom", "Mã Nhóm", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupLoaiDuoc.Properties.DisplayMember = "TenNhom";
            this.lupLoaiDuoc.Properties.NullText = "";
            this.lupLoaiDuoc.Properties.PopupSizeable = false;
            this.lupLoaiDuoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupLoaiDuoc.Properties.ValueMember = "IDNhom";
            this.lupLoaiDuoc.Size = new System.Drawing.Size(367, 24);
            this.lupLoaiDuoc.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(23, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 99;
            this.label4.Text = "Nhóm:";
            // 
            // frm_BCNXTNew_30002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 303);
            this.Controls.Add(this.lupLoaiDuoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btntaobc);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.cklKP);
            this.Controls.Add(this.lupkhotong);
            this.Controls.Add(this.dedenngay);
            this.Controls.Add(this.detungay);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_BCNXTNew_30002";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo Nhập - Xuất - Tồn";
            this.Load += new System.EventHandler(this.frm_BCNXTNew_30002_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupkhotong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupLoaiDuoc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit detungay;
        private DevExpress.XtraEditors.DateEdit dedenngay;
        private DevExpress.XtraEditors.LookUpEdit lupkhotong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckedListBoxControl cklKP;
        private DevExpress.XtraEditors.SimpleButton btnthoat;
        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.LookUpEdit lupLoaiDuoc;
        private System.Windows.Forms.Label label4;
    }
}