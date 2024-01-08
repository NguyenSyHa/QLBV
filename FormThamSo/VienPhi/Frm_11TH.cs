using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_11TH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_11TH()
        {
            InitializeComponent();
        }

        private void Frm_11TH_Load(object sender, EventArgs e)
        {
            luptungay.DateTime = System.DateTime.Now;
            lupdenngay.DateTime = System.DateTime.Now;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string _Matinh=DungChung.Bien.MaBV.Substring(0,2);
            DateTime Ngaytu=DungChung.Ham.NgayTu(luptungay.DateTime);
            DateTime Ngayden=DungChung.Ham.NgayDen(lupdenngay.DateTime);
            List<DmTinh> _ldmTinh = _Data.DmTinhs.ToList();
            var q2 = (from bn in _Data.BenhNhans.Where(p => p.NoiTinh == 3 && p.MaKCB == DungChung.Bien.MaBV)
                     join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                     join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                     where (vp.NgayTT <= Ngayden && vp.NgayTT >= Ngaytu)
                     select new
                     {
                         vp.Duyet,
                        bn.SThe,
                         bn.NoiTru,
                         vpct.TienBH,
                         vpct.TienChenh,
                     }).ToList();
            var q3 = (from a in q2.Where(p=>p.SThe!=null && p.SThe.Length==15)
                      join tinh in _ldmTinh on a.SThe.Substring(3, 2) equals tinh.MaTinh
                      where (a.Duyet == 1 || a.Duyet == 2)
                      group new { a, tinh } by new { tinh.TenTinh, tinh.MaTinh } into kp
                      select new
                      {
                          Tentinh = kp.Key.TenTinh,
                          Matinh = kp.Key.MaTinh,
                          NgoaitruBH = kp.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienBH) - kp.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienChenh),
                          NoitruBH = kp.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienBH) - kp.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienChenh)
                      }).ToList();
            BaoCao.Rep_11TH rep = new BaoCao.Rep_11TH();
            frmIn frm = new frmIn();
            if (q3.Count > 0)
            {
                rep.Tungaydenngay.Value = "Từ ngày: " + luptungay.DateTime.ToString().Substring(0, 10) + " Đến ngày: " + lupdenngay.DateTime.ToString().Substring(0, 10);
                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                rep.Soyte.Value = DungChung.Bien.TenCQCQ;
                rep.ngaytu.Value = Ngaytu;
                rep.ngayden.Value = Ngayden;
                rep.DataSource = q3;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}