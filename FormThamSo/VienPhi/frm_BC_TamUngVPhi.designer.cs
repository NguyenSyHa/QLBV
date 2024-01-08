namespace QLBV.FormThamSo
{
    partial class frm_BC_TamUngVPhi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_TamUngVPhi));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_TieudeBC = new DevExpress.XtraEditors.TextEdit();
            this.date_TuNgay = new DevExpress.XtraEditors.DateEdit();
            this.date_DenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btn_Print = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_TieudeBC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(31, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(97, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tiêu đề báo cáo:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(31, 94);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Từ ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(341, 94);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Đến ngày:";
            // 
            // txt_TieudeBC
            // 
            this.txt_TieudeBC.Location = new System.Drawing.Point(134, 51);
            this.txt_TieudeBC.Name = "txt_TieudeBC";
            this.txt_TieudeBC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TieudeBC.Properties.Appearance.Options.UseFont = true;
            this.txt_TieudeBC.Size = new System.Drawing.Size(468, 24);
            this.txt_TieudeBC.TabIndex = 1;
            // 
            // date_TuNgay
            // 
            this.date_TuNgay.EditValue = null;
            this.date_TuNgay.Location = new System.Drawing.Point(134, 91);
            this.date_TuNgay.Name = "date_TuNgay";
            this.date_TuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_TuNgay.Properties.Appearance.Options.UseFont = true;
            this.date_TuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_TuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_TuNgay.Properties.DisplayFormat.FormatString = "";
            this.date_TuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date_TuNgay.Properties.EditFormat.FormatString = "";
            this.date_TuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date_TuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.date_TuNgay.Size = new System.Drawing.Size(201, 24);
            this.date_TuNgay.TabIndex = 1;
            // 
            // date_DenNgay
            // 
            this.date_DenNgay.EditValue = null;
            this.date_DenNgay.Location = new System.Drawing.Point(407, 91);
            this.date_DenNgay.Name = "date_DenNgay";
            this.date_DenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_DenNgay.Properties.Appearance.Options.UseFont = true;
            this.date_DenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_DenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_DenNgay.Properties.DisplayFormat.FormatString = "";
            this.date_DenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date_DenNgay.Properties.EditFormat.FormatString = "";
            this.date_DenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date_DenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.date_DenNgay.Size = new System.Drawing.Size(195, 24);
            this.date_DenNgay.TabIndex = 1;
            // 
            // btn_Print
            // 
            this.btn_Print.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.Image")));
            this.btn_Print.Location = new System.Drawing.Point(509, 151);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(93, 31);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "In báo cáo";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // frm_BC_TamUngVPhi
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 194);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.txt_TieudeBC);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.date_TuNgay);
            this.Controls.Add(this.date_DenNgay);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frm_BC_TamUngVPhi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kê khai tạm ứng viện phí";
            this.Load += new System.EventHandler(this.frm_BC_TamUngVPhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_TieudeBC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_TuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_DenNgay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_TieudeBC;
        private DevExpress.XtraEditors.DateEdit date_TuNgay;
        private DevExpress.XtraEditors.DateEdit date_DenNgay;
        private DevExpress.XtraEditors.SimpleButton btn_Print;
    }
}