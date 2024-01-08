namespace QLBV.FormNhap
{
    partial class frm_ShowThongTinBHYTTrenCong
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
            this.picInfo = new DevExpress.XtraEditors.PictureEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.picWarming = new DevExpress.XtraEditors.PictureEdit();
            this.mmMesshow = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarming.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picInfo
            // 
            this.picInfo.EditValue = global::QLBV.Properties.Resources.apply_16x16;
            this.picInfo.Location = new System.Drawing.Point(3, 1);
            this.picInfo.Name = "picInfo";
            this.picInfo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picInfo.Properties.Appearance.Options.UseFont = true;
            this.picInfo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picInfo.Properties.ZoomAcceleration = 100D;
            this.picInfo.Properties.ZoomPercent = 150D;
            this.picInfo.Size = new System.Drawing.Size(142, 212);
            this.picInfo.TabIndex = 1;
            this.picInfo.Visible = false;
            // 
            // picWarming
            // 
            this.picWarming.EditValue = global::QLBV.Properties.Resources.apply_16x16;
            this.picWarming.Location = new System.Drawing.Point(3, 1);
            this.picWarming.Name = "picWarming";
            this.picWarming.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picWarming.Properties.Appearance.Options.UseFont = true;
            this.picWarming.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picWarming.Properties.ZoomAcceleration = 100D;
            this.picWarming.Size = new System.Drawing.Size(142, 212);
            this.picWarming.TabIndex = 3;
            this.picWarming.Visible = false;
            this.picWarming.EditValueChanged += new System.EventHandler(this.picWarming_EditValueChanged);
            // 
            // mmMesshow
            // 
            this.mmMesshow.Appearance.BackColor = System.Drawing.Color.White;
            this.mmMesshow.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.mmMesshow.Appearance.Options.UseBackColor = true;
            this.mmMesshow.Appearance.Options.UseFont = true;
            this.mmMesshow.Appearance.Options.UseForeColor = true;
            this.mmMesshow.Appearance.Options.UseTextOptions = true;
            this.mmMesshow.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.mmMesshow.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.mmMesshow.Location = new System.Drawing.Point(151, 1);
            this.mmMesshow.Name = "mmMesshow";
            this.mmMesshow.Size = new System.Drawing.Size(523, 212);
            this.mmMesshow.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(599, 219);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frm_ShowThongTinBHYTTrenCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 245);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.mmMesshow);
            this.Controls.Add(this.picWarming);
            this.Controls.Add(this.picInfo);
            this.Name = "frm_ShowThongTinBHYTTrenCong";
            this.Text = "Thông báo";
            this.Load += new System.EventHandler(this.frm_ShowThongTinBHYTTrenCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarming.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PictureEdit picInfo;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.PictureEdit picWarming;
        private DevExpress.XtraEditors.LabelControl mmMesshow;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}