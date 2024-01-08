namespace QLBV.FormThamSo
{
    partial class frm_HDKCB_24009
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_HDKCB_24009));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupngayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radBieu = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNgayThang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cklDTBN = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgayThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklDTBN)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(24, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 17);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // lupngayden
            // 
            this.lupngayden.EditValue = null;
            this.lupngayden.EnterMoveNextControl = true;
            this.lupngayden.Location = new System.Drawing.Point(133, 54);
            this.lupngayden.Name = "lupngayden";
            this.lupngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden.Properties.Appearance.Options.UseFont = true;
            this.lupngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Size = new System.Drawing.Size(238, 24);
            this.lupngayden.TabIndex = 25;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.EnterMoveNextControl = true;
            this.lupNgaytu.Location = new System.Drawing.Point(133, 24);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(238, 24);
            this.lupNgaytu.TabIndex = 24;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(24, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 17);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // radBieu
            // 
            this.radBieu.Location = new System.Drawing.Point(22, 214);
            this.radBieu.Name = "radBieu";
            this.radBieu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBieu.Properties.Appearance.Options.UseFont = true;
            this.radBieu.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Hoạt động KCB (11.1)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "HĐ KB dự phòng, tử vong và CLS (11.2)")});
            this.radBieu.Size = new System.Drawing.Size(349, 54);
            this.radBieu.TabIndex = 30;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(24, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(104, 17);
            this.labelControl3.TabIndex = 31;
            this.labelControl3.Text = "Ngày tháng HT:";
            // 
            // txtNgayThang
            // 
            this.txtNgayThang.Location = new System.Drawing.Point(133, 84);
            this.txtNgayThang.Name = "txtNgayThang";
            this.txtNgayThang.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgayThang.Properties.Appearance.Options.UseFont = true;
            this.txtNgayThang.Size = new System.Drawing.Size(238, 22);
            this.txtNgayThang.TabIndex = 32;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(24, 118);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(74, 17);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "Đối tượng: ";
            // 
            // cklDTBN
            // 
            this.cklDTBN.CheckOnClick = true;
            this.cklDTBN.DisplayMember = "DTBN1";
            this.cklDTBN.Location = new System.Drawing.Point(133, 113);
            this.cklDTBN.Name = "cklDTBN";
            this.cklDTBN.Size = new System.Drawing.Size(238, 93);
            this.cklDTBN.TabIndex = 34;
            this.cklDTBN.ValueMember = "IDDTBN";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(187, 281);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(86, 23);
            this.btnHuy.TabIndex = 29;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnIn
            // 
            this.btnIn.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(102, 281);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(79, 23);
            this.btnIn.TabIndex = 28;
            this.btnIn.Text = "In BC";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // frm_HDKCB_24009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 309);
            this.Controls.Add(this.cklDTBN);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtNgayThang);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.radBieu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lupngayden);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.labelControl2);
            this.Name = "frm_HDKCB_24009";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BC HĐ KCB, KCB dự phòng, tử vong, CLS";
            this.Load += new System.EventHandler(this.frm_HDKCB_24009_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNgayThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklDTBN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit lupngayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.RadioGroup radBieu;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNgayThang;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckedListBoxControl cklDTBN;
    }
}