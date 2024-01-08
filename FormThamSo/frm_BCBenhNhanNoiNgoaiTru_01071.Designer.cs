namespace QLBV.FormThamSo
{
    partial class frm_BCBenhNhanNoiNgoaiTru_01071
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BCBenhNhanNoiNgoaiTru_01071));
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lupKhoaPhong = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaPhong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.Location = new System.Drawing.Point(91, 19);
            this.dtTuNgay.Margin = new System.Windows.Forms.Padding(4);
            this.dtTuNgay.MinDate = new System.DateTime(1975, 1, 1, 0, 0, 0, 0);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Size = new System.Drawing.Size(124, 23);
            this.dtTuNgay.TabIndex = 0;
            this.dtTuNgay.Value = new System.DateTime(2021, 7, 29, 0, 0, 0, 0);
            this.dtTuNgay.ValueChanged += new System.EventHandler(this.dtTuNgay_ValueChanged);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.Location = new System.Drawing.Point(311, 19);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(4);
            this.dtDenNgay.MinDate = new System.DateTime(1975, 1, 1, 0, 0, 0, 0);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(124, 23);
            this.dtDenNgay.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(37, 22);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(251, 22);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Location = new System.Drawing.Point(17, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Khoa phòng";
            // 
            // btnIn
            // 
            this.btnIn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnIn.Location = new System.Drawing.Point(122, 118);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 23);
            this.btnIn.TabIndex = 3;
            this.btnIn.Text = "In báo cáo";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lupKhoaPhong
            // 
            this.lupKhoaPhong.EnterMoveNextControl = true;
            this.lupKhoaPhong.Location = new System.Drawing.Point(91, 59);
            this.lupKhoaPhong.Name = "lupKhoaPhong";
            this.lupKhoaPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKhoaPhong.Properties.Appearance.Options.UseFont = true;
            this.lupKhoaPhong.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupKhoaPhong.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lupKhoaPhong.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupKhoaPhong.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupKhoaPhong.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKhoaPhong.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lupKhoaPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoaPhong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã KP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa phòng")});
            this.lupKhoaPhong.Properties.DisplayMember = "TenKP";
            this.lupKhoaPhong.Properties.NullText = "Chọn khoa | phòng ";
            this.lupKhoaPhong.Properties.PopupSizeable = false;
            this.lupKhoaPhong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoaPhong.Properties.ValueMember = "MaKP";
            this.lupKhoaPhong.Size = new System.Drawing.Size(344, 30);
            this.lupKhoaPhong.TabIndex = 2;
            // 
            // frm_BCBenhNhanNoiNgoaiTru_01071
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(462, 165);
            this.Controls.Add(this.lupKhoaPhong);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.dtTuNgay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_BCBenhNhanNoiNgoaiTru_01071";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo bệnh nhân nội - ngoại trú";
            this.Load += new System.EventHandler(this.frm_BCBenhNhanNoiNgoaiTru_01071_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaPhong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LookUpEdit lupKhoaPhong;
    }
}