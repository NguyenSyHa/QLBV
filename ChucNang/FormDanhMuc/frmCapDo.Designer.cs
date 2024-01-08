namespace QLBV.FormDanhMuc
{
    partial class frmCapDo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapDo));
            this.memoCapDo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoCapDo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoCapDo
            // 
            this.memoCapDo.EditValue = resources.GetString("memoCapDo.EditValue");
            this.memoCapDo.Location = new System.Drawing.Point(12, 12);
            this.memoCapDo.Name = "memoCapDo";
            this.memoCapDo.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.memoCapDo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoCapDo.Properties.Appearance.Options.UseFont = true;
            this.memoCapDo.Properties.Appearance.Options.UseTextOptions = true;
            this.memoCapDo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.memoCapDo.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoCapDo.Size = new System.Drawing.Size(382, 403);
            this.memoCapDo.TabIndex = 3;
            // 
            // frmCapDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 427);
            this.Controls.Add(this.memoCapDo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCapDo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin về cấp độ của tài khoản";
            this.Load += new System.EventHandler(this.frmCapDo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoCapDo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoCapDo;

    }
}