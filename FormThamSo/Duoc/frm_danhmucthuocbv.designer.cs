namespace QLBV.FormNhap
{
    partial class frm_danhmucthuocbv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_danhmucthuocbv));
            this.button1 = new System.Windows.Forms.Button();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.radiotatca = new System.Windows.Forms.RadioButton();
            this.radiodangsudung = new System.Windows.Forms.RadioButton();
            this.radiokhong = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radiothuoc = new System.Windows.Forms.RadioButton();
            this.radiovattu = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(279, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Danh mục thuốc vật tư y tế theo cơ sở KCB";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(1, 136);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(279, 40);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "In";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // radiotatca
            // 
            this.radiotatca.AutoSize = true;
            this.radiotatca.Checked = true;
            this.radiotatca.Location = new System.Drawing.Point(7, 10);
            this.radiotatca.Name = "radiotatca";
            this.radiotatca.Size = new System.Drawing.Size(55, 17);
            this.radiotatca.TabIndex = 3;
            this.radiotatca.TabStop = true;
            this.radiotatca.Text = "Tất cả";
            this.radiotatca.UseVisualStyleBackColor = true;
            // 
            // radiodangsudung
            // 
            this.radiodangsudung.AutoSize = true;
            this.radiodangsudung.Location = new System.Drawing.Point(73, 10);
            this.radiodangsudung.Name = "radiodangsudung";
            this.radiodangsudung.Size = new System.Drawing.Size(93, 17);
            this.radiodangsudung.TabIndex = 4;
            this.radiodangsudung.Text = "Đang sử dụng";
            this.radiodangsudung.UseVisualStyleBackColor = true;
            // 
            // radiokhong
            // 
            this.radiokhong.AutoSize = true;
            this.radiokhong.Location = new System.Drawing.Point(172, 10);
            this.radiokhong.Name = "radiokhong";
            this.radiokhong.Size = new System.Drawing.Size(97, 17);
            this.radiokhong.TabIndex = 5;
            this.radiokhong.Text = "Không sử dụng";
            this.radiokhong.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.radiotatca);
            this.panel1.Controls.Add(this.radiokhong);
            this.panel1.Controls.Add(this.radiodangsudung);
            this.panel1.Location = new System.Drawing.Point(1, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 35);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.radiothuoc);
            this.panel2.Controls.Add(this.radiovattu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(279, 35);
            this.panel2.TabIndex = 7;
            // 
            // radiothuoc
            // 
            this.radiothuoc.AutoSize = true;
            this.radiothuoc.Checked = true;
            this.radiothuoc.Location = new System.Drawing.Point(9, 10);
            this.radiothuoc.Name = "radiothuoc";
            this.radiothuoc.Size = new System.Drawing.Size(54, 17);
            this.radiothuoc.TabIndex = 3;
            this.radiothuoc.TabStop = true;
            this.radiothuoc.Text = "Thuốc";
            this.radiothuoc.UseVisualStyleBackColor = true;
            // 
            // radiovattu
            // 
            this.radiovattu.AutoSize = true;
            this.radiovattu.Location = new System.Drawing.Point(173, 10);
            this.radiovattu.Name = "radiovattu";
            this.radiovattu.Size = new System.Drawing.Size(77, 17);
            this.radiovattu.TabIndex = 4;
            this.radiovattu.Text = "Vật tư y tế";
            this.radiovattu.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(247, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 23);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frm_danhmucthuocbv
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 177);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_danhmucthuocbv";
            this.Padding = new System.Windows.Forms.Padding(1, 25, 1, 1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_danhmucthuocbv_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RadioButton radiotatca;
        private System.Windows.Forms.RadioButton radiodangsudung;
        private System.Windows.Forms.RadioButton radiokhong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radiothuoc;
        private System.Windows.Forms.RadioButton radiovattu;
        private System.Windows.Forms.Button button2;
    }
}