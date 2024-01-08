using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.MoRong.ManHinhCLS
{
    public partial class ManHinhGoiSo_LayMauBenhPham : DevExpress.XtraEditors.XtraForm
    {
        public ManHinhGoiSo_LayMauBenhPham()
        {
            InitializeComponent();
        }
        int maKP;
        public ManHinhGoiSo_LayMauBenhPham(int mkp)
        {
            InitializeComponent();
            try
            {
                this.maKP = mkp;
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ManHinhGoiSo_LayMauBenhPham_Load(object sender, EventArgs e)
        {
            timer1.Start();
            ImgLogo.Image = DungChung.Ham.GetLogo();
            var kp = data.KPhongs.FirstOrDefault(o => o.MaKP == maKP);
            if (kp != null)
                lblKP.Text = kp.TenKP.ToUpper();
            var cb = data.CanBoes.FirstOrDefault(o => o.MaCB == DungChung.Bien.MaCB);
            if (cb != null)
            {
                lblBacSi.Text = (!string.IsNullOrEmpty(cb.ChucVu) ? cb.ChucVu.ToUpper() + ": " : "BÁC SỸ: ") + cb.TenCB.ToUpper();
            }
            lblFooter.Text = DungChung.Bien.TenCQ.ToUpper() + " - TẬN TỤY - NGHĨA TÌNH - HẾT MÌNH VÌ NGƯỜI BỆNH";
            RefreshData();
        }
        public void Call(string tenBN, string maBN, string stt)
        {
            lblMaBN.Text = "Mã số: " + maBN.ToUpper();
            lblTenBN.Text = tenBN.ToUpper();
            lblSTT.Text = stt;
            RefreshData();
        }
        public void RefreshData()
        {
            try
            {
                DateTime NgayTu = DungChung.Ham.NgayTu(DateTime.Now);
                DateTime NgayDen = DungChung.Ham.NgayDen(DateTime.Now);
                var tbBenhnhan = (from bn in data.BenhNhans
                                  join cls in data.CLS.Where(p => p.NgayThang >= NgayTu && p.NgayThang <= NgayDen) on bn.MaBNhan equals cls.MaBNhan
                                  join kp in data.KPhongs.Where(p => p.MaKP == maKP) on cls.MaKPth equals kp.MaKP
                                  select new
                                  {
                                      bn.TenBNhan,
                                      bn.Tuoi,
                                      bn.SoTT

                                  }).ToList();

                gridControlDanhSachCho.BeginUpdate();
                gridControlDanhSachCho.DataSource = tbBenhnhan;
                gridControlDanhSachCho.EndUpdate();

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}