using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.DungChung;

namespace QLBV.CLS.ManHinhGoiSo_30372
{
    public partial class frm_ManHinhBenhNhan_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_ManHinhBenhNhan_30007()
        {
            InitializeComponent();
        }
        ConnectData connect = new ConnectData();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int maKP;
        public frm_ManHinhBenhNhan_30007(int _maKP)
        {
            InitializeComponent();
            try
            {
                this.maKP = _maKP;
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }
        private void frm_ManHinhBenhNhan_30007_Load(object sender, EventArgs e)
        {
            ptbLogo.Image = DungChung.Ham.GetLogo();
            var kp = data.KPhongs.FirstOrDefault(o => o.MaKP == maKP);
            if (kp != null)
                lblTenKP.Text = kp.TenKP.ToUpper();
            var cb = data.CanBoes.FirstOrDefault(o => o.MaCB == DungChung.Bien.MaCB);
            if (cb != null)
            {
                lblBacSi.Text = (!string.IsNullOrEmpty(cb.ChucVu) ? cb.ChucVu.ToUpper() + ": " : "BÁC SỸ: ") + cb.TenCB.ToUpper();
            }
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper() + " - TẬN TỤY - NGHĨA TÌNH - HẾT MÌNH VÌ NGƯỜI BỆNH";
            RefreshData();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

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
                                  join kp in data.KPhongs.Where(p=>p.MaKP == maKP) on cls.MaKPth equals kp.MaKP
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
    }

}