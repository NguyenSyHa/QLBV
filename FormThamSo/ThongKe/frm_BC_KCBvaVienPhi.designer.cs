namespace QLBV.FormThamSo
{
    partial class frm_BC_KCBvaVienPhi
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.luptungay = new DevExpress.XtraEditors.DateEdit();
            this.lupdenngay = new DevExpress.XtraEditors.DateEdit();
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.cboDTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Đối tượng:";
            // 
            // luptungay
            // 
            this.luptungay.EditValue = null;
            this.luptungay.Location = new System.Drawing.Point(95, 9);
            this.luptungay.Name = "luptungay";
            this.luptungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luptungay.Properties.Appearance.Options.UseFont = true;
            this.luptungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Size = new System.Drawing.Size(166, 24);
            this.luptungay.TabIndex = 1;
            // 
            // lupdenngay
            // 
            this.lupdenngay.EditValue = null;
            this.lupdenngay.Location = new System.Drawing.Point(94, 43);
            this.lupdenngay.Name = "lupdenngay";
            this.lupdenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupdenngay.Properties.Appearance.Options.UseFont = true;
            this.lupdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Size = new System.Drawing.Size(166, 24);
            this.lupdenngay.TabIndex = 2;
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Location = new System.Drawing.Point(95, 110);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(73, 23);
            this.btntaobc.TabIndex = 4;
            this.btntaobc.Text = "Tạo BC";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(187, 110);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(74, 23);
            this.btnhuy.TabIndex = 5;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // cboDTuong
            // 
            this.cboDTuong.EditValue = "Cả hai";
            this.cboDTuong.Location = new System.Drawing.Point(94, 80);
            this.cboDTuong.Name = "cboDTuong";
            this.cboDTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDTuong.Properties.Items.AddRange(new object[] {
            "BHYT",
            "Dịch vụ",
            "Cả hai"});
            this.cboDTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboDTuong.Size = new System.Drawing.Size(167, 24);
            this.cboDTuong.TabIndex = 3;
            // 
            // frm_BC_KCBvaVienPhi
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 141);
            this.Controls.Add(this.cboDTuong);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btntaobc);
            this.Controls.Add(this.lupdenngay);
            this.Controls.Add(this.luptungay);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_KCBvaVienPhi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BC khám chữa bệnh và viện phí";
            this.Load += new System.EventHandler(this.frm_BC_KCBvaVienPhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit luptungay;
        private DevExpress.XtraEditors.DateEdit lupdenngay;
        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.ComboBoxEdit cboDTuong;
    }
}