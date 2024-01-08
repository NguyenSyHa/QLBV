
namespace QLBV.FormThamSo
{
    partial class frm_BC_KhamBenhTheo_PK
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.cboNam = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboThang = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lupKhoa = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(228, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 23);
            this.btnCancel.TabIndex = 144;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(228, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 23);
            this.btnOK.TabIndex = 143;
            this.btnOK.Text = "Tạo báo cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboNam
            // 
            this.cboNam.Location = new System.Drawing.Point(96, 39);
            this.cboNam.Name = "cboNam";
            this.cboNam.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNam.Properties.Appearance.Options.UseFont = true;
            this.cboNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNam.Size = new System.Drawing.Size(121, 22);
            this.cboNam.TabIndex = 146;
            // 
            // cboThang
            // 
            this.cboThang.EditValue = "1";
            this.cboThang.Location = new System.Drawing.Point(96, 11);
            this.cboThang.Name = "cboThang";
            this.cboThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.Properties.Appearance.Options.UseFont = true;
            this.cboThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboThang.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cboThang.Size = new System.Drawing.Size(121, 22);
            this.cboThang.TabIndex = 145;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 147;
            this.label3.Text = "Năm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 148;
            this.label2.Text = "Tháng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 173;
            this.label1.Text = "Khoa/Phòng:";
            // 
            // lupKhoa
            // 
            this.lupKhoa.EnterMoveNextControl = true;
            this.lupKhoa.Location = new System.Drawing.Point(96, 71);
            this.lupKhoa.Name = "lupKhoa";
            this.lupKhoa.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupKhoa.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.lupKhoa.Properties.Appearance.Options.UseFont = true;
            this.lupKhoa.Properties.Appearance.Options.UseForeColor = true;
            this.lupKhoa.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.lupKhoa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.lupKhoa.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lupKhoa.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lupKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Khoa điều trị"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Name5", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKhoa.Properties.DisplayMember = "TenKP";
            this.lupKhoa.Properties.NullText = "";
            this.lupKhoa.Properties.PopupSizeable = false;
            this.lupKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKhoa.Properties.ValueMember = "MaKP";
            this.lupKhoa.Size = new System.Drawing.Size(121, 22);
            this.lupKhoa.TabIndex = 174;
            // 
            // frm_BC_KhamBenhTheo_PK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 100);
            this.Controls.Add(this.lupKhoa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "frm_BC_KhamBenhTheo_PK";
            this.Text = "Báo Cáo Khám Bệnh Theo Khoa Phòng";
            this.Load += new System.EventHandler(this.frm_BC_KhamBenhTheo_PK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.ComboBoxEdit cboNam;
        private DevExpress.XtraEditors.ComboBoxEdit cboThang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lupKhoa;
    }
}