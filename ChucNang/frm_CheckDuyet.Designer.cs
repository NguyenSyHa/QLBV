namespace QLBV.ChucNang
{
    partial class frm_CheckDuyet
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CheckDuyet));
            this.memoNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ckcKoTamUng = new DevExpress.XtraEditors.CheckEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.memoNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckcKoTamUng.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoNoiDung
            // 
            this.memoNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoNoiDung.Location = new System.Drawing.Point(2, 21);
            this.memoNoiDung.Name = "memoNoiDung";
            this.memoNoiDung.Size = new System.Drawing.Size(368, 91);
            this.memoNoiDung.TabIndex = 54;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.memoNoiDung);
            this.groupControl1.Location = new System.Drawing.Point(-1, -2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(372, 114);
            this.groupControl1.TabIndex = 55;
            this.groupControl1.Text = "Nội dung";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ckcKoTamUng
            // 
            this.ckcKoTamUng.Location = new System.Drawing.Point(12, 120);
            this.ckcKoTamUng.Name = "ckcKoTamUng";
            this.ckcKoTamUng.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckcKoTamUng.Properties.Appearance.Options.UseFont = true;
            this.ckcKoTamUng.Properties.Caption = "Không tính tạm ứng";
            this.ckcKoTamUng.Size = new System.Drawing.Size(143, 19);
            this.ckcKoTamUng.TabIndex = 56;
            this.ckcKoTamUng.Visible = false;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(262, 118);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 23);
            this.btnHuy.TabIndex = 53;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(161, 118);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(95, 23);
            this.btnInBC.TabIndex = 52;
            this.btnInBC.Text = "&Duyệt";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // frm_CheckDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 154);
            this.Controls.Add(this.ckcKoTamUng);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CheckDuyet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duyệt chi phí bệnh nhân";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_CheckDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckcKoTamUng.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.MemoEdit memoNoiDung;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.CheckEdit ckcKoTamUng;
    }
}