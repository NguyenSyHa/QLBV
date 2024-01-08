namespace QLBV
{
    partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chkGhiNho = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK2 = new System.Windows.Forms.Button();
            this.Giaodien = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.P_DangNhap = new System.Windows.Forms.Panel();
            this.P_KetNoi = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pcxThoat2 = new System.Windows.Forms.PictureBox();
            this.txtMatKhausql = new QLBV.Class.RJTextBox();
            this.txtTaiKhoan = new QLBV.Class.RJTextBox();
            this.btnKetnoi2 = new QLBV.Class.RJButton();
            this.txtTenCSDL = new QLBV.Class.RJTextBox();
            this.txt_TenMC = new QLBV.Class.RJTextBox();
            this.pcxThoat = new System.Windows.Forms.PictureBox();
            this.txt_MK = new QLBV.Class.RJTextBox();
            this.txtTenDN = new QLBV.Class.RJTextBox();
            this.btnTroGiup2 = new System.Windows.Forms.Label();
            this.rjButton1 = new QLBV.Class.RJButton();
            this.txtMatKhau = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGhiNho.Properties)).BeginInit();
            this.P_DangNhap.SuspendLayout();
            this.P_KetNoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcxThoat2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcxThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhau.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(466, 299);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 18);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Mở Rộng>>";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // chkGhiNho
            // 
            this.chkGhiNho.Location = new System.Drawing.Point(253, 229);
            this.chkGhiNho.Name = "chkGhiNho";
            this.chkGhiNho.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chkGhiNho.Properties.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkGhiNho.Properties.Appearance.Options.UseFont = true;
            this.chkGhiNho.Properties.Appearance.Options.UseForeColor = true;
            this.chkGhiNho.Properties.Caption = "Nhớ tên đăng nhập và mật khẩu";
            this.chkGhiNho.Size = new System.Drawing.Size(205, 19);
            this.chkGhiNho.TabIndex = 2;
            // 
            // btnOK2
            // 
            this.btnOK2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOK2.FlatAppearance.BorderSize = 0;
            this.btnOK2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOK2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnOK2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK2.ForeColor = System.Drawing.Color.AliceBlue;
            this.btnOK2.Location = new System.Drawing.Point(395, 256);
            this.btnOK2.Name = "btnOK2";
            this.btnOK2.Size = new System.Drawing.Size(79, 23);
            this.btnOK2.TabIndex = 5;
            this.btnOK2.Text = "&Đăng nhập";
            this.btnOK2.UseVisualStyleBackColor = false;
            this.btnOK2.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Giaodien
            // 
            this.Giaodien.LookAndFeel.SkinName = "Office 2007 Blue";
            // 
            // P_DangNhap
            // 
            this.P_DangNhap.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("P_DangNhap.BackgroundImage")));
            this.P_DangNhap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.P_DangNhap.Controls.Add(this.P_KetNoi);
            this.P_DangNhap.Controls.Add(this.pcxThoat);
            this.P_DangNhap.Controls.Add(this.txt_MK);
            this.P_DangNhap.Controls.Add(this.txtTenDN);
            this.P_DangNhap.Controls.Add(this.btnTroGiup2);
            this.P_DangNhap.Controls.Add(this.rjButton1);
            this.P_DangNhap.Controls.Add(this.btnOK2);
            this.P_DangNhap.Controls.Add(this.labelControl2);
            this.P_DangNhap.Controls.Add(this.chkGhiNho);
            this.P_DangNhap.Controls.Add(this.txtMatKhau);
            this.P_DangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_DangNhap.Location = new System.Drawing.Point(0, 0);
            this.P_DangNhap.Name = "P_DangNhap";
            this.P_DangNhap.Size = new System.Drawing.Size(600, 400);
            this.P_DangNhap.TabIndex = 37;
            // 
            // P_KetNoi
            // 
            this.P_KetNoi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("P_KetNoi.BackgroundImage")));
            this.P_KetNoi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.P_KetNoi.Controls.Add(this.labelControl1);
            this.P_KetNoi.Controls.Add(this.pcxThoat2);
            this.P_KetNoi.Controls.Add(this.txtMatKhausql);
            this.P_KetNoi.Controls.Add(this.txtTaiKhoan);
            this.P_KetNoi.Controls.Add(this.btnKetnoi2);
            this.P_KetNoi.Controls.Add(this.txtTenCSDL);
            this.P_KetNoi.Controls.Add(this.txt_TenMC);
            this.P_KetNoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_KetNoi.Location = new System.Drawing.Point(0, 0);
            this.P_KetNoi.Name = "P_KetNoi";
            this.P_KetNoi.Size = new System.Drawing.Size(600, 400);
            this.P_KetNoi.TabIndex = 6;
            this.P_KetNoi.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(475, 322);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 18);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Quay lại<<";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // pcxThoat2
            // 
            this.pcxThoat2.BackColor = System.Drawing.Color.White;
            this.pcxThoat2.Image = ((System.Drawing.Image)(resources.GetObject("pcxThoat2.Image")));
            this.pcxThoat2.Location = new System.Drawing.Point(568, 4);
            this.pcxThoat2.Name = "pcxThoat2";
            this.pcxThoat2.Size = new System.Drawing.Size(29, 29);
            this.pcxThoat2.TabIndex = 55;
            this.pcxThoat2.TabStop = false;
            this.pcxThoat2.Click += new System.EventHandler(this.pcxThoat2_Click);
            // 
            // txtMatKhausql
            // 
            this.txtMatKhausql.BackColor = System.Drawing.Color.Transparent;
            this.txtMatKhausql.BackColorArtan = System.Drawing.Color.LightGray;
            this.txtMatKhausql.BorderColor = System.Drawing.Color.Transparent;
            this.txtMatKhausql.BorderSize = 0;
            this.txtMatKhausql.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhausql.ForeColor = System.Drawing.Color.Black;
            this.txtMatKhausql.Location = new System.Drawing.Point(257, 237);
            this.txtMatKhausql.Name = "txtMatKhausql";
            this.txtMatKhausql.PasswordChar = '\0';
            this.txtMatKhausql.Radius = 7;
            this.txtMatKhausql.Size = new System.Drawing.Size(289, 36);
            this.txtMatKhausql.TabIndex = 3;
            this.txtMatKhausql.TabStop = false;
            this.txtMatKhausql.Text = "Mật khẩu";
            this.txtMatKhausql.UseSystemPasswordChar = false;
            this.txtMatKhausql.WaterMarckColor = System.Drawing.Color.Gray;
            this.txtMatKhausql.WaterMark = null;
            this.txtMatKhausql.Enter += new System.EventHandler(this.txtMatKhausql_Enter);
            this.txtMatKhausql.Leave += new System.EventHandler(this.txtMatKhausql_Leave);
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.BackColor = System.Drawing.Color.Transparent;
            this.txtTaiKhoan.BackColorArtan = System.Drawing.Color.LightGray;
            this.txtTaiKhoan.BorderColor = System.Drawing.Color.Transparent;
            this.txtTaiKhoan.BorderSize = 0;
            this.txtTaiKhoan.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaiKhoan.ForeColor = System.Drawing.Color.Black;
            this.txtTaiKhoan.Location = new System.Drawing.Point(257, 195);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.PasswordChar = '\0';
            this.txtTaiKhoan.Radius = 7;
            this.txtTaiKhoan.Size = new System.Drawing.Size(289, 36);
            this.txtTaiKhoan.TabIndex = 2;
            this.txtTaiKhoan.TabStop = false;
            this.txtTaiKhoan.Text = "Tài khoản";
            this.txtTaiKhoan.UseSystemPasswordChar = false;
            this.txtTaiKhoan.WaterMarckColor = System.Drawing.Color.Gray;
            this.txtTaiKhoan.WaterMark = null;
            this.txtTaiKhoan.Enter += new System.EventHandler(this.txtTaiKhoan_Enter);
            this.txtTaiKhoan.Leave += new System.EventHandler(this.txtTaiKhoan_Leave);
            // 
            // btnKetnoi2
            // 
            this.btnKetnoi2.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKetnoi2.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.btnKetnoi2.Bordercolor = System.Drawing.Color.PaleVioletRed;
            this.btnKetnoi2.BorderRadius = 40;
            this.btnKetnoi2.BorderSize = 0;
            this.btnKetnoi2.FlatAppearance.BorderSize = 0;
            this.btnKetnoi2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKetnoi2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKetnoi2.ForeColor = System.Drawing.Color.White;
            this.btnKetnoi2.Location = new System.Drawing.Point(255, 279);
            this.btnKetnoi2.Name = "btnKetnoi2";
            this.btnKetnoi2.Size = new System.Drawing.Size(289, 40);
            this.btnKetnoi2.TabIndex = 4;
            this.btnKetnoi2.Text = "Kết Nối";
            this.btnKetnoi2.TextColor = System.Drawing.Color.White;
            this.btnKetnoi2.UseVisualStyleBackColor = false;
            this.btnKetnoi2.Click += new System.EventHandler(this.btnKetnoi2_Click);
            // 
            // txtTenCSDL
            // 
            this.txtTenCSDL.BackColor = System.Drawing.Color.Transparent;
            this.txtTenCSDL.BackColorArtan = System.Drawing.Color.LightGray;
            this.txtTenCSDL.BorderColor = System.Drawing.Color.Transparent;
            this.txtTenCSDL.BorderSize = 0;
            this.txtTenCSDL.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenCSDL.ForeColor = System.Drawing.Color.Black;
            this.txtTenCSDL.Location = new System.Drawing.Point(257, 153);
            this.txtTenCSDL.Name = "txtTenCSDL";
            this.txtTenCSDL.PasswordChar = '\0';
            this.txtTenCSDL.Radius = 7;
            this.txtTenCSDL.Size = new System.Drawing.Size(289, 36);
            this.txtTenCSDL.TabIndex = 1;
            this.txtTenCSDL.TabStop = false;
            this.txtTenCSDL.Text = "Tên CSDL";
            this.txtTenCSDL.UseSystemPasswordChar = false;
            this.txtTenCSDL.WaterMarckColor = System.Drawing.Color.DarkGray;
            this.txtTenCSDL.WaterMark = null;
            this.txtTenCSDL.Enter += new System.EventHandler(this.txtTenCSDL_Enter);
            this.txtTenCSDL.Leave += new System.EventHandler(this.txtTenCSDL_Leave);
            // 
            // txt_TenMC
            // 
            this.txt_TenMC.BackColor = System.Drawing.Color.Transparent;
            this.txt_TenMC.BackColorArtan = System.Drawing.Color.LightGray;
            this.txt_TenMC.BorderColor = System.Drawing.Color.Transparent;
            this.txt_TenMC.BorderSize = 0;
            this.txt_TenMC.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TenMC.ForeColor = System.Drawing.Color.Black;
            this.txt_TenMC.Location = new System.Drawing.Point(257, 111);
            this.txt_TenMC.Name = "txt_TenMC";
            this.txt_TenMC.PasswordChar = '\0';
            this.txt_TenMC.Radius = 7;
            this.txt_TenMC.Size = new System.Drawing.Size(289, 36);
            this.txt_TenMC.TabIndex = 0;
            this.txt_TenMC.TabStop = false;
            this.txt_TenMC.Text = "Tên máy chủ";
            this.txt_TenMC.UseSystemPasswordChar = false;
            this.txt_TenMC.WaterMarckColor = System.Drawing.Color.Gray;
            this.txt_TenMC.WaterMark = null;
            this.txt_TenMC.Click += new System.EventHandler(this.txt_TenMC_Click);
            this.txt_TenMC.Enter += new System.EventHandler(this.txt_TenMC_Enter);
            this.txt_TenMC.Leave += new System.EventHandler(this.txt_TenMC_Leave);
            // 
            // pcxThoat
            // 
            this.pcxThoat.BackColor = System.Drawing.Color.White;
            this.pcxThoat.Image = ((System.Drawing.Image)(resources.GetObject("pcxThoat.Image")));
            this.pcxThoat.Location = new System.Drawing.Point(568, 3);
            this.pcxThoat.Name = "pcxThoat";
            this.pcxThoat.Size = new System.Drawing.Size(29, 29);
            this.pcxThoat.TabIndex = 48;
            this.pcxThoat.TabStop = false;
            this.pcxThoat.Click += new System.EventHandler(this.pcxThoat_Click);
            // 
            // txt_MK
            // 
            this.txt_MK.BackColor = System.Drawing.Color.Transparent;
            this.txt_MK.BackColorArtan = System.Drawing.Color.LightGray;
            this.txt_MK.BorderColor = System.Drawing.Color.Transparent;
            this.txt_MK.BorderSize = 0;
            this.txt_MK.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MK.ForeColor = System.Drawing.Color.Black;
            this.txt_MK.Location = new System.Drawing.Point(255, 187);
            this.txt_MK.Name = "txt_MK";
            this.txt_MK.PasswordChar = '\0';
            this.txt_MK.Radius = 7;
            this.txt_MK.Size = new System.Drawing.Size(289, 36);
            this.txt_MK.TabIndex = 1;
            this.txt_MK.TabStop = false;
            this.txt_MK.Text = "Mật khẩu";
            this.txt_MK.UseSystemPasswordChar = false;
            this.txt_MK.WaterMarckColor = System.Drawing.Color.Gray;
            this.txt_MK.WaterMark = null;
            this.txt_MK.Enter += new System.EventHandler(this.txt_MK_Enter);
            this.txt_MK.Leave += new System.EventHandler(this.txt_MK_Leave);
            // 
            // txtTenDN
            // 
            this.txtTenDN.BackColor = System.Drawing.Color.Transparent;
            this.txtTenDN.BackColorArtan = System.Drawing.Color.LightGray;
            this.txtTenDN.BorderColor = System.Drawing.Color.Transparent;
            this.txtTenDN.BorderSize = 0;
            this.txtTenDN.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDN.ForeColor = System.Drawing.Color.Black;
            this.txtTenDN.Location = new System.Drawing.Point(255, 145);
            this.txtTenDN.Name = "txtTenDN";
            this.txtTenDN.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.txtTenDN.PasswordChar = '\0';
            this.txtTenDN.Radius = 7;
            this.txtTenDN.Size = new System.Drawing.Size(289, 36);
            this.txtTenDN.TabIndex = 0;
            this.txtTenDN.TabStop = false;
            this.txtTenDN.Text = "Tên đăng nhập";
            this.txtTenDN.UseSystemPasswordChar = false;
            this.txtTenDN.WaterMarckColor = System.Drawing.Color.DarkGray;
            this.txtTenDN.WaterMark = null;
            this.txtTenDN.Enter += new System.EventHandler(this.txtTenDN_Enter);
            this.txtTenDN.Leave += new System.EventHandler(this.rjTextBox1_Leave_1);
            // 
            // btnTroGiup2
            // 
            this.btnTroGiup2.AutoSize = true;
            this.btnTroGiup2.BackColor = System.Drawing.Color.White;
            this.btnTroGiup2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTroGiup2.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnTroGiup2.Location = new System.Drawing.Point(535, 373);
            this.btnTroGiup2.Name = "btnTroGiup2";
            this.btnTroGiup2.Size = new System.Drawing.Size(62, 18);
            this.btnTroGiup2.TabIndex = 5;
            this.btnTroGiup2.Text = "Trợ Giúp";
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.SteelBlue;
            this.rjButton1.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.rjButton1.Bordercolor = System.Drawing.Color.PaleVioletRed;
            this.rjButton1.BorderRadius = 40;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(253, 253);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(289, 40);
            this.rjButton1.TabIndex = 3;
            this.rjButton1.Text = "Đăng Nhập";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(364, 256);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhau.Properties.Appearance.Options.UseFont = true;
            this.txtMatKhau.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.txtMatKhau.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.txtMatKhau.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtMatKhau.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMatKhau.Properties.UseSystemPasswordChar = true;
            this.txtMatKhau.Size = new System.Drawing.Size(25, 22);
            this.txtMatKhau.TabIndex = 5;
            this.txtMatKhau.EditValueChanged += new System.EventHandler(this.txtMatKhau_EditValueChanged);
            // 
            // frmDangNhap
            // 
            this.AcceptButton = this.btnOK2;
            this.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.ControlBox = false;
            this.Controls.Add(this.P_DangNhap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDangNhap";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".....VSSoft.vn.....";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDangNhap_FormClosed);
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkGhiNho.Properties)).EndInit();
            this.P_DangNhap.ResumeLayout(false);
            this.P_DangNhap.PerformLayout();
            this.P_KetNoi.ResumeLayout(false);
            this.P_KetNoi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcxThoat2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcxThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhau.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit chkGhiNho;
        private DevExpress.LookAndFeel.DefaultLookAndFeel Giaodien;
        private System.Windows.Forms.Button btnOK2;
        private System.Windows.Forms.Panel P_DangNhap;
        private Class.RJButton rjButton1;
        private DevExpress.XtraEditors.TextEdit txtMatKhau;
        private System.Windows.Forms.Label btnTroGiup2;
        private Class.RJTextBox txtTenDN;
        private Class.RJTextBox txt_MK;
        private System.Windows.Forms.Panel P_KetNoi;
        private Class.RJTextBox txtMatKhausql;
        private Class.RJTextBox txtTaiKhoan;
        private Class.RJButton btnKetnoi2;
        private Class.RJTextBox txtTenCSDL;
        private Class.RJTextBox txt_TenMC;
        private System.Windows.Forms.PictureBox pcxThoat;
        private System.Windows.Forms.PictureBox pcxThoat2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}