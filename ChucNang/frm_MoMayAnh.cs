using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AForge.Video.DirectShow;

namespace QLBV.ChucNang
{
    public partial class frm_MoMayAnh : DevExpress.XtraEditors.XtraForm
    {
        public frm_MoMayAnh()
        {
            InitializeComponent();
            
        }
        private static QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities();

        private FilterInfoCollection cameras;
        private VideoCaptureDevice cam;

        private void btnMoCam_Click(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();

            }
            cam = new VideoCaptureDevice(cameras[CboChonCam.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame;
            cam.Start();
        }
        private void Cam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap map = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = map;
        }

        private void frm_MoMayAnh_Load(object sender, EventArgs e)
        {
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in cameras)
            {
                CboChonCam.Items.Add(item.Name);
            }
            CboChonCam.SelectedIndex = 0;
        }

        private void btnChupAnh_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string Filename = folderBrowserDialog.SelectedPath;
            saveFileDialog.InitialDirectory = Filename;
            Bitmap source = null;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                source = new Bitmap(pictureBox1.Image);
                pictureBox1.Image.Save(saveFileDialog.FileName + ".jpg");
                QLBV.FormNhap.frmHSBNNhapMoi.patdpimg = saveFileDialog.FileName + ".jpg";
            }                  
            this.Close();
        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }
        private void btnTatMayAnh_Click(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void frm_MoMayAnh_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            } 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://vssoft.vn/huong-dan-su-dung-ung-dung-ivcam-phan-mem-quan-ly-tong-the-benh-vien-vssoft/");
        }
    }
}
