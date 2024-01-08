using System;
using QLBV_Database;
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
    public partial class frm_TKBNVaoChuyenVien : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKBNVaoChuyenVien()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime tungay = System.DateTime.Now.Date;
        DateTime denngay = System.DateTime.Now.Date;
        private void btnInBC_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();

            int _mkp = 0;
            string _noitru = "Phòng khám";

            int _giaiquyet = -1;
            _giaiquyet = cboHinhThuc.SelectedIndex;

            if (lupKhoaPhong.EditValue != null && lupKhoaPhong.EditValue.ToString() != "")
            {
                _mkp = Convert.ToInt32(lupKhoaPhong.EditValue.ToString());

            }
            //_noitru=radNoiTru.SelectedIndex;
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

            if (_giaiquyet == 0)
            {
                if (cboThanhToan.SelectedIndex == 0)
                {
                    BaoCao.rep_TKBNVaoVien rep = new BaoCao.rep_TKBNVaoVien();
                    rep.TenKP.Value = lupKhoaPhong.Text;
                    rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                    rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                    var que = (from bn in _data.BenhNhans
                               join rv in _data.VaoViens on bn.MaBNhan equals rv.MaBNhan
                               where !((from vp in _data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan))
                               where (rv.NgayVao >= tungay && rv.NgayVao <= denngay)
                               where (_mkp == 0 ? true : rv.MaKP == _mkp)
                               group new { bn, rv } by new { bn.MaBNhan, bn.Tuoi, bn.TenBNhan, rv.ChanDoan, bn.DChi, bn.DTuong, bn.SThe } into kq
                               select new
                               {
                                   kq.Key.TenBNhan,
                                   kq.Key.DTuong,
                                   Tuoi = kq.Key.Tuoi,
                                   kq.Key.MaBNhan,
                                   kq.Key.DChi,
                                   kq.Key.SThe,
                                   ChanDoan = kq.Key.ChanDoan
                               }).ToList();
                    rep.DataSource = que.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    if (cboThanhToan.SelectedIndex == 1)
                    {
                        BaoCao.rep_TKBNVaoVien rep = new BaoCao.rep_TKBNVaoVien();
                        rep.TenKP.Value = lupKhoaPhong.Text;
                        rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                        rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                        var que = (from bn in _data.BenhNhans
                                   join vp in _data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                   join rv in _data.VaoViens on bn.MaBNhan equals rv.MaBNhan
                                   where (rv.NgayVao >= tungay && rv.NgayVao <= denngay)
                                   where (_mkp == 0 ? true : rv.MaKP == _mkp)
                                   group new { bn, rv } by new { bn.MaBNhan, bn.Tuoi, bn.TenBNhan, rv.ChanDoan, bn.DChi, bn.DTuong, bn.SThe } into kq
                                   select new
                                   {
                                       kq.Key.TenBNhan,
                                       kq.Key.DTuong,
                                       Tuoi = kq.Key.Tuoi,
                                       kq.Key.MaBNhan,
                                       kq.Key.DChi,
                                       kq.Key.SThe,
                                       ChanDoan = kq.Key.ChanDoan
                                   }).ToList();
                        rep.DataSource = que.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.rep_TKBNVaoVien rep = new BaoCao.rep_TKBNVaoVien();
                        rep.TenKP.Value = lupKhoaPhong.Text;
                        rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                        rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                        var que = (from bn in _data.BenhNhans
                                   join rv in _data.VaoViens on bn.MaBNhan equals rv.MaBNhan
                                   where (rv.NgayVao >= tungay && rv.NgayVao <= denngay)
                                   where (_mkp == 0 ? true : rv.MaKP == _mkp)
                                   group new { bn, rv } by new { bn.MaBNhan, bn.Tuoi, bn.TenBNhan, rv.ChanDoan, bn.DChi, bn.DTuong, bn.SThe } into kq
                                   select new
                                   {
                                       kq.Key.TenBNhan,
                                       kq.Key.DTuong,
                                       kq.Key.Tuoi,
                                       kq.Key.MaBNhan,
                                       kq.Key.DChi,
                                       kq.Key.SThe,
                                       ChanDoan = kq.Key.ChanDoan
                                   }).ToList();
                        rep.DataSource = que.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
            else
            { // chuyển viện
                var lCanbo = _data.CanBoes.ToList();
                BaoCao.rep_TKBNChuyenVien rep = new BaoCao.rep_TKBNChuyenVien();
                List<CV> que = new List<CV>();
                if (cboThanhToan.SelectedIndex == 0)
                {

                    rep.TenKP.Value = lupKhoaPhong.Text;
                    rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                    rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                    var q1 = (from bn in _data.BenhNhans
                              where !((from vp in _data.VienPhis select vp.MaBNhan).Contains(bn.MaBNhan))
                              join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                              where (rv.NgayRa >= tungay && rv.NgayRa <= denngay)
                              where ((_mkp == 0 ? true : rv.MaKP == _mkp) && rv.Status == _giaiquyet)
                              join bv in _data.BenhViens on rv.MaBVC equals bv.MaBV
                              join bnkb in _data.BNKBs on new { rv.MaBNhan, rv.MaKP } equals new { MaBNhan = bnkb.MaBNhan ?? 0, MaKP = bnkb.MaKP ?? 0 }
                              join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                              group new { bn, rv, cb } by new { bv.TenBV, bn.MaBNhan, rv.MaICD, bn.Tuoi, bn.TenBNhan, rv.ChanDoan, bn.DChi, bn.DTuong, rv.MaKP, cb.TenCB, bn.SThe } into kq
                              select new
                              {
                                  kq.Key.TenBV,
                                  kq.Key.TenBNhan,
                                  kq.Key.DTuong,
                                  kq.Key.SThe,
                                  kq.Key.Tuoi,
                                  kq.Key.MaBNhan,
                                  kq.Key.DChi,
                                  kq.Key.ChanDoan,
                                  kq.Key.MaICD,
                                  //MaKP = kq.Key.MaKP ?? 0,
                                  //TenBSDT = kq.Key.TenCB == null ? "" : kq.Key.TenCB
                              }).ToList();

                    que = (from q in q1
                           group new { q } by new { q.TenBV, q.MaBNhan, q.Tuoi, q.TenBNhan, q.ChanDoan, q.DChi, q.DTuong, q.SThe, q.MaICD } into kq
                           select new CV
                           {
                               TenBV = kq.Key.TenBV,
                               TenBNhan = kq.Key.TenBNhan,
                               DTuong = kq.Key.DTuong,
                               SThe = kq.Key.SThe,
                               //Tuoi = kq.Key.Tuoi,
                               MaBNhan = kq.Key.MaBNhan,
                               DChi = kq.Key.DChi,
                               ChanDoan = DungChung.Ham.FreshString(kq.Key.ChanDoan),
                               MaICD = DungChung.Ham.FreshString(kq.Key.MaICD),
                               //MaKP = kq.Key.MaKP,
                               //TenBSDT = string.Join(",", kq.Select(p => p.q.TenBSDT))
                           }).ToList();

                    rep.DataSource = que.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    if (cboThanhToan.SelectedIndex == 1)
                    {

                        rep.TenKP.Value = lupKhoaPhong.Text;
                        rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                        rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                        var q1 = (from bn in _data.BenhNhans
                                  join vp in _data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                  join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                  where rv.NgayRa >= tungay && rv.NgayRa <= denngay
                                  where (_mkp == 0 || rv.MaKP == _mkp) && rv.Status == _giaiquyet
                                  join bv in _data.BenhViens on rv.MaBVC equals bv.MaBV
                                  join bnkb in _data.BNKBs on new { rv.MaBNhan, rv.MaKP } equals new { MaBNhan = bnkb.MaBNhan ?? 0, MaKP = bnkb.MaKP ?? 0 }
                                  join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                                  group new { bn, rv, cb } by new { bv.TenBV, rv.MaICD, bn.MaBNhan, bn.Tuoi, bn.TenBNhan, rv.ChanDoan, bn.DChi, bn.DTuong, rv.MaKP, cb.TenCB, bn.SThe } into kq
                                  select new
                                  {
                                      kq.Key.TenBV,
                                      kq.Key.TenBNhan,
                                      kq.Key.DTuong,
                                      kq.Key.SThe,
                                      kq.Key.Tuoi,
                                      kq.Key.MaBNhan,
                                      kq.Key.DChi,
                                      kq.Key.MaICD,
                                      kq.Key.ChanDoan,
                                      //MaKP = kq.Key.MaKP ?? 0,
                                      //TenBSDT = kq.Key.TenCB == null ? "" : kq.Key.TenCB
                                  }).ToList();
                        que = (from q in q1
                               group new { q } by new { q.TenBV, q.MaBNhan, q.Tuoi, q.TenBNhan, q.MaICD, q.ChanDoan, q.DChi, q.DTuong, q.SThe } into kq
                               select new CV
                               {
                                   TenBV = kq.Key.TenBV,
                                   TenBNhan = kq.Key.TenBNhan,
                                   DTuong = kq.Key.DTuong,
                                   SThe = kq.Key.SThe,
                                   //Tuoi = kq.Key.Tuoi,
                                   MaBNhan = kq.Key.MaBNhan,
                                   DChi = kq.Key.DChi,
                                   ChanDoan = DungChung.Ham.FreshString(kq.Key.ChanDoan),
                                   MaICD = DungChung.Ham.FreshString(kq.Key.MaICD),
                                   //MaKP = kq.Key.MaKP,
                                   //TenBSDT = string.Join(",", kq.Select(p => p.q.TenBSDT))
                               }).ToList();
                    }
                    else
                    {
                        rep.TenKP.Value = lupKhoaPhong.Text;
                        rep.Tieude.Value = ("Danh sách bệnh nhân " + cboHinhThuc.Text).ToUpper();
                        rep.ThoiGian.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);
                        var q1 = (from bn in _data.BenhNhans
                                  join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                  where rv.NgayRa >= tungay && rv.NgayRa <= denngay
                                  where (_mkp == 0 || rv.MaKP == _mkp) && rv.Status == _giaiquyet
                                  join bv in _data.BenhViens on rv.MaBVC equals bv.MaBV
                                  join bnkb in _data.BNKBs on new { MaBNhan = rv.MaBNhan, MaKP = rv.MaKP } equals new { MaBNhan = bnkb.MaBNhan ?? 0, MaKP = bnkb.MaKP ?? 0 }
                                  join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB

                                  group new { bn, rv, cb } by new { bv.TenBV, bn.MaBNhan, bn.Tuoi, bn.TenBNhan, rv.MaICD, rv.ChanDoan, bn.DChi, bn.DTuong, rv.MaKP, cb.TenCB, bn.SThe } into kq
                                  select new
                                  {
                                      kq.Key.TenBV,
                                      kq.Key.TenBNhan,
                                      kq.Key.SThe,
                                      kq.Key.DTuong,
                                      kq.Key.Tuoi,
                                      kq.Key.MaBNhan,
                                      kq.Key.DChi,
                                      kq.Key.MaICD,
                                      kq.Key.ChanDoan
                                      //MaKP = kq.Key.MaKP ?? 0,
                                      // TenCB = kq.Key.TenCB == null ? "" : kq.Key.TenCB
                                  }).ToList();

                        que = (from q in q1
                               group new { q } by new { q.TenBV, q.MaBNhan, q.Tuoi, q.TenBNhan, q.ChanDoan, q.DChi, q.DTuong, q.SThe, q.MaICD } into kq
                               select new CV
                               {
                                   TenBV = kq.Key.TenBV,
                                   TenBNhan = kq.Key.TenBNhan,
                                   SThe = kq.Key.SThe,
                                   DTuong = kq.Key.DTuong,
                                   // Tuoi = kq.Key.Tuoi,
                                   MaBNhan = kq.Key.MaBNhan,
                                   DChi = kq.Key.DChi,
                                   ChanDoan = DungChung.Ham.FreshString(kq.Key.ChanDoan),
                                   MaICD = DungChung.Ham.FreshString(kq.Key.MaICD),
                                   //  MaKP = kq.Key.MaKP,
                                   // TenBSDT = string.Join(",", kq.Select(p => p.q.TenBSDT))
                               }).ToList();

                    }
                    //foreach(CV cv in que)
                    //{
                    //    var qbnkb = _data.BNKBs.Where(p => p.MaBNhan == cv.MaBNhan && p.MaKP == cv.MaKP).OrderByDescending(p => p.IDKB).FirstOrDefault();
                    //    if(qbnkb != null)
                    //    {
                    //      var  qcb = lCanbo.Where(p => p.MaCB == qbnkb.MaCB).FirstOrDefault();
                    //      cv.TenBSDT = qcb.TenCB;
                    //    }
                    //}
                    rep.DataSource = que.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }

        }
        //private string GetChanDoan (string maicd,string chandoan)
        //{
        //    string kq = "";
        //    kq = DungChung.Ham.FreshString(maicd);
        //    return kq;
        //}
        public class CV
        {

            public string TenBV { get; set; }

            public string TenBNhan { get; set; }
            public string SThe { get; set; }

            public string DTuong { get; set; }

            public string Tuoi { get; set; }

            public int MaBNhan { get; set; }

            public string DChi { get; set; }

            public string MaICD { get; set; }

            public string ChanDoan { get; set; }

            public int MaKP { get; set; }

            public string TenBSDT { get; set; }
        }
        private void frm_TKBNVaoChuyenVien_Load(object sender, EventArgs e)
        {
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            var KP = (from kp in _data.KPhongs where (kp.PLoai == ("Lâm sàng") || kp.PLoai == ("Phòng khám")) select kp).ToList();
            lupKhoaPhong.Properties.DataSource = KP.ToList().OrderBy(p => p.TenKP);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}