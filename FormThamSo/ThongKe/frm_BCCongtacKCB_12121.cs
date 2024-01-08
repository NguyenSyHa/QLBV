using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BCCongtacKCB_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCongtacKCB_12121()
        {
            InitializeComponent();
        }
 
        private void frm_BCCongtacKCB_30003_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _Kphong = new List<KPhong>();
            List<content> _lcontent=new List<content>();
            content _moi = new content();
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.Rep_BCCongtacKCB_12121 rep = new BaoCao.Rep_BCCongtacKCB_12121();
                rep.NGAYT.Value = "(Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text + ")";
                rep.ngay.Value = "Lai Châu, ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                //rep.THANG.Value = "Tháng " + Convert.ToDateTime(lupDenNgay.Text).Month + " năm " + Convert.ToDateTime(lupDenNgay.Text).Year;
                var qkb3 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                            join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                            select new { kb,bn }).ToList();

                var qkb2 = (from bn in qkb3
                            group new { bn } by new { bn.bn } into kq
                                select new { kq.Key.bn, IDKB = kq.Max(p => p.bn.kb.IDKB)}).ToList();
                var qkb1 = (from a in qkb3
                            join b in qkb2 on a.kb.IDKB equals b.IDKB
                            select new { a.bn.MaBNhan, MaDTuong = a.bn.MaDTuong == null ? "" : a.bn.MaDTuong.ToLower().Trim(), a.bn.Tuoi, a.bn.SThe, a.bn.DTuong, a.bn.NoiTru, a.bn.DTNT, a.bn.NNhap, a.kb.IDKB, a.kb.MaKP, a.bn.MaBV, a.kb.NgayKham, a.kb.PhuongAn }).ToList();
                var noitru = (from a in qkb1.Where(p => p.NoiTru == 1) 
                              select new {a.MaBNhan }).ToList();
                //List<DichVu> dichvu = data.DichVus.ToList();
                //List<TieuNhomDV> _ltn = data.TieuNhomDVs.ToList();
                //bệnh nhân khám tư vấn=tổng số bệnh nhân - tổng số bệnh nhân có MaDV ko thuộc nhóm 24(nhóm công khám)
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV, tn.TenTN, tn.TenRG, tn.IdTieuNhom,tn.IDNhom,dv.DongY }).ToList();
                var _lrvq = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                             select rv).ToList();
                var _ldtuoc = (from dt in data.DThuocs
                               join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                               select new { dt.MaBNhan,dtct.MaDV}).ToList();
                var dthuoc = (from rv in qkb1
                              join dt in _ldtuoc on rv.MaBNhan  equals dt.MaBNhan
                              select new { dt.MaBNhan,dt.MaDV}).ToList();
            
                var dthuockb = (from dt in dthuoc
                                join dv in _ldv.Where(p=>p.IDNhom!=13) on dt.MaDV equals dv.MaDV
                                //join tn in _ltn.Where(p => p.IDNhom != 13) on dv.IdTieuNhom equals tn.IdTieuNhom
                                select new { dt.MaBNhan}).ToList();
                var l_bnngtru = (from bn in qkb1.Where(p => p.NoiTru == 0 && p.DTNT == false)
                                 select bn).ToList();
                var _lbntuvan = (from bn in l_bnngtru
                                 join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                 group new { bn, dtct } by new { bn.MaBNhan } into kq
                                 select new
                                 {
                                     kq.Key.MaBNhan,
                                 }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvantv = (from bn in l_bnngtru
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();

                var cv = (from a in qkb1.Where(p => p.PhuongAn == 2) select a).Distinct().ToList();

                
            double ktvantong = _lbntuvan.Distinct().Count() - _bntuvantv.Distinct().Count() - cv.Count();
                //bn tư vấn viện phí
                var _lbntuvanvp = (from bn in l_bnngtru.Where(p => p.DTuong == "Dịch vụ")
                                   join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                   group new { bn, dtct } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                   }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvanvp = (from bn in l_bnngtru.Where(p => p.DTuong == "Dịch vụ")
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();
                double ktvanvp = _lbntuvanvp.Distinct().Count() - _bntuvanvp.Distinct().Count() - cv.Where(p => p.DTuong == "Dịch vụ").Count();
                //bn tư vấn người nghèo
                var _lbntuvannn = (from bn in l_bnngtru.Where(p => p.MaDTuong == "hn" || p.MaDTuong == "cn" || p.MaDTuong == "dt")
                                   join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                   group new { bn, dtct } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                   }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvannn = (from bn in l_bnngtru.Where(p => p.MaDTuong == "hn" || p.MaDTuong == "cn" || p.MaDTuong == "dt")
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();
                double ktvannn = _lbntuvannn.Distinct().Count() - _bntuvannn.Distinct().Count() - cv.Where(p => p.MaDTuong == "hn" || p.MaDTuong == "cn" || p.MaDTuong == "dt").Count();
                //bn tư vấn < 6 tuổi
                var _lbntuvan06 = (from bn in l_bnngtru.Where(p => p.Tuoi <= 6)
                                   join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                   group new { bn, dtct } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                   }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvan06 = (from bn in l_bnngtru.Where(p => p.Tuoi <= 6)
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();
                double ktuvan06 = _lbntuvan06.Distinct().Count() - _bntuvan06.Distinct().Count() - cv.Where(p => p.Tuoi <= 6).Count();
                //bệnh nhân tư vấn >60 tuổi
                var _lbntuvan60 = (from bn in l_bnngtru.Where(p => p.Tuoi >= 60)
                                   join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                   group new { bn, dtct } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                   }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvan60 = (from bn in l_bnngtru.Where(p => p.Tuoi >= 60)
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();
                double ktuvan60 = _lbntuvan60.Distinct().Count() - _bntuvan60.Distinct().Count() - cv.Where(p => p.Tuoi >= 60).Count();
                //bệnh nhân tư vấn >80 tuổi
                var _lbntuvan80 = (from bn in l_bnngtru.Where(p => p.Tuoi >= 80)
                                   join dtct in dthuoc on bn.MaBNhan equals dtct.MaBNhan
                                   group new { bn, dtct } by new { bn.MaBNhan } into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                   }).OrderBy(p => p.MaBNhan).ToList();
                var _bntuvan80 = (from bn in l_bnngtru.Where(p => p.Tuoi >= 80)
                                  join dtct in dthuockb on bn.MaBNhan equals dtct.MaBNhan
                                  group new { bn, dtct } by new { bn.MaBNhan } into kq
                                  select new
           
                                  
                                  {
                                      kq.Key.MaBNhan,
                                  }).OrderBy(p => p.MaBNhan).ToList();
                double ktuvan80 = _lbntuvan80.Distinct().Count() - _bntuvan80.Distinct().Count() - cv.Where(p => p.Tuoi >= 80).Count();
                List<KPhong> _lkp = data.KPhongs.ToList();
                var qkb = (from bn in qkb1
                           //join p in data.BNKBs on k.IDKB equals p.IDKB
                           ////join kp in data.KPhongs on k.MaKP equals kp.MaKP
                           ////join kp in khoa on k.MaKP equals kp.makp
                           //join bn in data.BenhNhans on p.MaBNhan equals bn.MaBNhan
                           //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                           join kp in _lkp on bn.MaKP equals kp.MaKP
                           group new { bn,kp } by new { bn.MaBNhan, bn.SThe } into kq
                           select new
                           {
                               // makp=42: Nội,makp=36: châm cứu,makp=41: phục hồi chức năng
                               //tổng số khám bệnh
                               TSKB = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT42 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT36 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT41 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),

                               //TSNGTRU = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTpk = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnoi = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP==42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTcc = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTphcn = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTts = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //khám chữa bệnh viện phí

                               TSVP = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT42VP = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "Dịch vụ" && p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT36VP = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "Dịch vụ" && p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT41VP = kq.Where(p => p.bn.NoiTru == 1 && p.bn.DTuong == "Dịch vụ" && p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSNGTRUDV = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSDTNTDV = kq.Where(p => p.bn.DTuong == "Dịch vụ" && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTpkdv = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true&&p.bn.DTuong == "Dịch vụ").Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTdvnoi = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.DTuong == "Dịch vụ").Where(p => p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTdvcc = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.DTuong == "Dịch vụ").Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTdvphcn = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.DTuong == "Dịch vụ").Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTdvts = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.DTuong == "Dịch vụ").Select(p => p.bn.MaBNhan).Distinct().Count(),

                               //khám bệnh trẻ em < 6 tuổi
                               TSTE = kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT42TE = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi <= 6 && p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT36TE = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi <= 6 && p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT41TE = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi <= 6 && p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSNGTRUTE = kq.Where(p => p.bn.NoiTru == 0 && p.bn.Tuoi <= 6 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSDTNTTE = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTpkte = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTtenoi = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Where(p => p.bn.MaKP==42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTtecc = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTtephcn = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTtets = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Distinct().Count(),

                               //khám chữa bệnh người nghèo
                               TSNN = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT42NN = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 42).Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT36NN = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 36).Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT41NN = kq.Where(p => p.bn.NoiTru == 1 && p.bn.MaKP == 41).Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSNGTRUNN = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p=> p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSDTNTNN = kq.Where(p => p.bn.MaDTuong == "hn" && p.bn.MaDTuong == "cn" && p.bn.MaDTuong == "dt" && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnnpk = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnnnoi = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnncc = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnnphcn = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTnnts = kq.Where(p => p.bn.MaDTuong == "hn" || p.bn.MaDTuong == "cn" || p.bn.MaDTuong == "dt").Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),

                               //khám chữa bênh bn >60 tuổi
                               TS60 = kq.Where(p => p.bn.Tuoi > 60).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT4260 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 60 && p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT3660 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 60 && p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT4160 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 60 && p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSNGTRU60 = kq.Where(p => p.bn.NoiTru == 0 && p.bn.Tuoi >= 60 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSDTNT60 = kq.Where(p => p.bn.Tuoi >= 60 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTpk60 = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 60).Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT60noi = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 60).Where(p => p.bn.MaKP==42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT60cc = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 60).Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT60phcn = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 60).Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT60ts = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 60).Select(p => p.bn.MaBNhan).Distinct().Count(),

                               //khám chữa bệnh bn >80 tuổi
                               TS80 = kq.Where(p => p.bn.Tuoi > 80).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT4280 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 80 && p.bn.MaKP == 42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT3680 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 80 && p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSNT4180 = kq.Where(p => p.bn.NoiTru == 1 && p.bn.Tuoi >= 80 && p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TSNGTRU80 = kq.Where(p => p.bn.Tuoi >= 80 && p.bn.NoiTru == 0 && p.bn.DTNT == false).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT80 = kq.Where(p => p.bn.Tuoi >= 80 && p.bn.NoiTru == 0 && p.bn.DTNT == true).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNTpk80 = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 80).Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT80noi = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 80).Where(p => p.bn.MaKP==42).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT80cc = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 80).Where(p => p.bn.MaKP == 36).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT80phcn = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 80).Where(p => p.bn.MaKP == 41).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               TSDTNT80ts = kq.Where(p => p.bn.NoiTru == 0 && p.bn.DTNT == true && p.bn.Tuoi >= 80).Select(p => p.bn.MaBNhan).Distinct().Count(),

                           }).ToList();

                var _lngtru = (from rv in dthuoc
                               join dv in _ldv on rv.MaDV equals dv.MaDV
                               join bn in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on rv.MaBNhan equals bn.MaBNhan
                               group new { bn, dv } by new { bn.MaBNhan } into kq
                               select new
                               {
                                   TSNGTRU = kq.Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                   TSNGTRUDV = kq.Where(p => p.bn.DTuong == "Dịch vụ").Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                   TSNGTRUTE = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                   TSNGTRUNN = kq.Where(p => p.bn.MaDTuong != null && p.bn.MaDTuong.Trim().ToLower() == "hn" || p.bn.MaDTuong.Trim().ToLower() == "cn" || p.bn.MaDTuong.Trim().ToLower() == "dt").Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                   TSNGTRU60 = kq.Where(p => p.bn.Tuoi >= 60).Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count(),
                                   TSNGTRU80 = kq.Where(p => p.bn.Tuoi >= 80).Where(p => p.dv.IDNhom != 13).Select(p => p.bn.MaBNhan).Distinct().Count()
                               }).ToList();

                var _lrv = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                            join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                            select new { rv, bn.NoiTru }).ToList();
                List<BenhVien> _lbv = data.BenhViens.ToList();
                var qwe = (from bn in qkb1.Where(p=>p.MaBV!=null)
                           select new { bn.MaBNhan, bn.MaBV }).Distinct().ToList();
                var ctct = (from bn in qwe
                            join bv in _lbv on bn.MaBV equals bv.MaBV
                            group new { bn, bv } by new { bn.MaBNhan, bn.MaBV, bv.TuyenBV } into kq
                            select new
                            {
                                TUTUYENTREN = kq.Where(p => p.bv.TuyenBV.Trim() == "A").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                CUNGTUYEN = kq.Where(p => p.bv.TuyenBV.Trim() == "B").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                TUTUYENDUOI = kq.Where(p => p.bv.TuyenBV.Trim() == "C" || p.bv.TuyenBV.Trim() == "D").Select(p => p.bn.MaBNhan).Distinct().Count()
                            }).ToList();
                var ctctdi = (from rv in _lrv
                              join bv in _lbv on rv.rv.MaBVC equals bv.MaBV
                              group new { rv, bv } by new { rv.rv.MaBNhan, bv.MaBV } into kq
                              select new
                              {
                                  LENTUYENTREN = kq.Where(p => p.bv.TuyenBV.Trim() == "A").Select(p => p.rv.rv.MaBNhan).Distinct().Count(),
                                  CUNGTUYEN = kq.Where(p => p.bv.TuyenBV.Trim() == "B").Select(p => p.rv.rv.MaBNhan).Distinct().Count(),
                                  XUONGTUYENDUOI = kq.Where(p => p.bv.TuyenBV.Trim() == "C" || p.bv.TuyenBV.Trim() == "D").Select(p => p.rv.rv.MaBNhan).Distinct().Count(),
                              }).ToList();

                var ravien = (from rv in _lrv.Where(p => p.rv.MaBVC == null && p.rv.KetQua != null && p.NoiTru == 1)
                              group rv by new { rv.rv.MaBNhan, rv.rv.MaKP, rv.rv.KetQua } into kq
                              select new
                              {
                                  //tổng số ngày điều trị
                                  TSNGAY42 = kq.Where(p => p.rv.MaKP == 42).Sum(p => p.rv.SoNgaydt),
                                  TSNGAY36 = kq.Where(p => p.rv.MaKP == 36).Sum(p => p.rv.SoNgaydt),
                                  TSNGAY41 = kq.Where(p => p.rv.MaKP == 41).Sum(p => p.rv.SoNgaydt),
                                  //Tổng số ngày điều trị BN khỏi
                                  TSNGAYKHOI42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Khỏi").Sum(p => p.rv.SoNgaydt),
                                  TSNGAYKHOI36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Khỏi").Sum(p => p.rv.SoNgaydt),
                                  TSNGAYKHOI41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Khỏi").Sum(p => p.rv.SoNgaydt),
                                  //tổng số bệnh nhân ra viện
                                  // makp=42: Nội,makp=36: châm cứu,makp=41: phục hồi chức năng
                                  RVTS42 = kq.Where(p => p.rv.MaKP == 42).Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVTS36 = kq.Where(p => p.rv.MaKP == 36).Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVTS41 = kq.Where(p => p.rv.MaKP == 41).Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  //bệnh nhân khỏi
                                  RVKHOI42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Khỏi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVKHOI36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Khỏi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVKHOI41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Khỏi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  //bẹnh nhân đỡ, giảm
                                  RVDO42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Đỡ|Giảm").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVDO36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Đỡ|Giảm").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVDO41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Đỡ|Giảm").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  //bệnh nhân ko thay đổi
                                  RVKOTD42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Không T.đổi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVKOTD36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Không T.đổi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVKOTD41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Không T.đổi").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  //bệnh nhân nặng hơn
                                  RVNANGHON42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Nặng hơn").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVNANGHON36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Nặng hơn").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVNANGHON41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Nặng hơn").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  //bệnh nhân tử vong
                                  RVTUVONG42 = kq.Where(p => p.rv.MaKP == 42 && p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVTUVONG36 = kq.Where(p => p.rv.MaKP == 36 && p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Distinct().Count(),
                                  RVTUVONG41 = kq.Where(p => p.rv.MaKP == 41 && p.rv.KetQua == "Tử vong").Select(p => p.rv.MaBNhan).Distinct().Count(),
                              }).ToList();
                double songaytb42 = 0, songaytb36 = 0, songaytb41 = 0, songaytbtong = 0;
                double sobnkhoi36 = ravien.Sum(p => p.RVKHOI36), sobnkhoi42 = ravien.Sum(p => p.RVKHOI42), sobnkhoi41 = ravien.Sum(p => p.RVKHOI41);
                double songaykhoi36 =Convert.ToDouble(ravien.Sum(p => p.TSNGAYKHOI36)),songaykhoi42=Convert.ToDouble(ravien.Sum(p => p.TSNGAYKHOI42)),songaykhoi41=Convert.ToDouble(ravien.Sum(p => p.TSNGAYKHOI41));
                if (sobnkhoi42 > 0)
            {
                songaytb42 = Math.Round(songaykhoi42 / sobnkhoi42, MidpointRounding.AwayFromZero);
            }
                if (sobnkhoi36 > 0)
            {
                songaytb36 = Math.Round(songaykhoi36 / sobnkhoi36, MidpointRounding.AwayFromZero);

            }
            if (sobnkhoi41 > 0)
            {
                songaytb41 = Math.Round(songaykhoi41 / sobnkhoi41, MidpointRounding.AwayFromZero);
            }
            if ((sobnkhoi42 + sobnkhoi36+sobnkhoi41)>0)
            {
                songaytbtong = Math.Round(((songaykhoi36 + songaykhoi42 + songaykhoi41) / (sobnkhoi36 + sobnkhoi41 + sobnkhoi42)), MidpointRounding.AwayFromZero);
            }
            var dtyhct = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay && p.PLDV == 1)
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          select new { dt, dtct }).ToList();
            var _bnyhct = (from dt in dtyhct
                           join dv in _ldv.Where(p=>p.DongY==1) on dt.dtct.MaDV equals dv.MaDV
                           //join tn in data.TieuNhomDVs.Where(p=>p.TenRG=="Thuốc đông y") on dv.IdTieuNhom equals tn.IdTieuNhom
                           group new { dt,dv } by new { dt.dt.MaBNhan,dv.TenRG } into kq
                           select new
                           {
                               BNYHCT42 = kq.Where(p => p.dt.dt.MaKP == 42).Select(p => p.dt.dt.MaBNhan).Distinct().Count(),
                               BNYHCT36 = kq.Where(p => p.dt.dt.MaKP == 36).Select(p => p.dt.dt.MaBNhan).Distinct().Count(),
                               BNYHCT41 = kq.Where(p => p.dt.dt.MaKP == 41).Select(p => p.dt.dt.MaBNhan).Distinct().Count(),
                           }).ToList();
            string danhmuctong = "I. CÔNG TÁC KHÁM CHỮA BỆNH";
                    #region I.CÔNG TÁC KHÁM CHỮA BỆNH
                    #region 1.Tổng số lần khám bệnh 
                    //Tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 1;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "1. Tổng số";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT42) + qkb.Sum(p => p.TSNT36) + qkb.Sum(p => p.TSNT41) + qkb.Sum(p => p.TSDTNTts) + _lngtru.Sum(p => p.TSNGTRU) + ktvantong;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT42) + qkb.Sum(p => p.TSNT36) + qkb.Sum(p => p.TSNT41) + qkb.Sum(p => p.TSDTNTts) + _lngtru.Sum(p => p.TSNGTRU) + ktvantong;
                    _lcontent.Add(_moi);
                    //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 1;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "1. Tổng số";
                    _moi.danhmucct = "- Bệnh nhân điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null;
                    _moi.noi = qkb.Sum(p=>p.TSNT42);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT36);
                    _moi.phcn = qkb.Sum(p => p.TSNT41);
                    _moi.tongso = qkb.Sum(p => p.TSNT42) + qkb.Sum(p => p.TSNT36) + qkb.Sum(p => p.TSNT41);
                    _lcontent.Add(_moi);
                    //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 1;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "1. Tổng số";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p=>p.TSDTNTpk);
                    _moi.noi = qkb.Sum(p => p.TSDTNTcc);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNTnoi);
                    _moi.phcn = qkb.Sum(p => p.TSDTNTphcn);
                    _moi.tongso = qkb.Sum(p => p.TSDTNTts);
                    _lcontent.Add(_moi);
                    //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 1;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "1. Tổng số";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRU);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRU);
                    _lcontent.Add(_moi);
                    //bệnh nhân khám tư vấn
                    _moi = new content();
                    _moi.stt = 1;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "1. Tổng số";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktvantong;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktvantong;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 2. khám chữa bệnh viện phí
                    //tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 2;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "2. Khám chữa bệnh viện phí";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT42VP) + qkb.Sum(p => p.TSNT36VP) + qkb.Sum(p => p.TSNT41VP) + qkb.Sum(p => p.TSDTNTdvts) + _lngtru.Sum(p => p.TSNGTRUDV) + ktvanvp;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT42VP) + qkb.Sum(p => p.TSNT36VP) + qkb.Sum(p => p.TSNT41VP) + qkb.Sum(p => p.TSDTNTdvts) + _lngtru.Sum(p => p.TSNGTRUDV) + ktvanvp;
                    _lcontent.Add(_moi);
                    //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 2;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "2. Khám chữa bệnh viện phí";
                    _moi.danhmucct = "- Bệnh nhân điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null;
                    _moi.noi = qkb.Sum(p => p.TSNT42VP);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT36VP);
                    _moi.phcn = qkb.Sum(p => p.TSNT41VP);
                    _moi.tongso = qkb.Sum(p => p.TSNT42VP) + qkb.Sum(p => p.TSNT36VP) + qkb.Sum(p => p.TSNT41VP);
                    _lcontent.Add(_moi);
                    //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 2;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "2. Khám chữa bệnh viện phí";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p=>p.TSDTNTpkdv);
                    _moi.noi = qkb.Sum(p => p.TSDTNTdvnoi);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNTdvcc);
                    _moi.phcn = qkb.Sum(p => p.TSDTNTdvphcn);
                    _moi.tongso = qkb.Sum(p => p.TSDTNTdvts);
                    _lcontent.Add(_moi);
                    //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 2;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "2. Khám chữa bệnh viện phí";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRUDV);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRUDV);
                    _lcontent.Add(_moi);
                    //bệnh nhấn khám tư vấn
                    _moi = new content();
                    _moi.stt = 2;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "2. Khám chữa bệnh viện phí";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktvanvp;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktvanvp;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 3.khám chữa bệnh người nghèo
                //tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 3;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "3. Khám chữa bệnh người nghèo";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT42NN) + qkb.Sum(p => p.TSNT36NN) + qkb.Sum(p => p.TSNT41NN) + qkb.Sum(p => p.TSDTNTnnts) + _lngtru.Sum(p => p.TSNGTRUNN) + ktvannn;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT42NN) + qkb.Sum(p => p.TSNT36NN) + qkb.Sum(p => p.TSNT41NN) + qkb.Sum(p => p.TSDTNTnnts) + _lngtru.Sum(p => p.TSNGTRUNN) + ktvannn;
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 3;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "3. Khám chữa bệnh người nghèo";
                    _moi.danhmucct = "- Bệnh nhân điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null;
                    _moi.noi = qkb.Sum(p => p.TSNT42NN);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT36NN);
                    _moi.phcn = qkb.Sum(p => p.TSNT41NN);
                    _moi.tongso = qkb.Sum(p => p.TSNT42NN) + qkb.Sum(p => p.TSNT36NN) + qkb.Sum(p => p.TSNT41NN);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 3;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "3. Khám chữa bệnh người nghèo";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p=>p.TSDTNTnnpk);
                    _moi.noi = qkb.Sum(p => p.TSDTNTnnnoi);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNTnncc);
                    _moi.phcn = qkb.Sum(p => p.TSDTNTnnphcn);
                    _moi.tongso = qkb.Sum(p=>p.TSDTNTnnts);
                    _lcontent.Add(_moi);
                //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 3;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "3. Khám chữa bệnh người nghèo";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRUNN);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRUNN);
                    _lcontent.Add(_moi);
                //bệnh nhân khám tư vấn
                    _moi = new content();
                    _moi.stt = 3;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "3. Khám chữa bệnh người nghèo";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktvannn;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktvannn;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 4. khám chữa bệnh trẻ em <6 tuổi
                //tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 4;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "4. Khám chữa bệnh trẻ em < 6 tuổi";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT42TE) + qkb.Sum(p => p.TSNT36TE) + qkb.Sum(p => p.TSNT41TE) + qkb.Sum(p => p.TSDTNTtets) + _lngtru.Sum(p => p.TSNGTRUTE) + ktuvan06;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT42TE) + qkb.Sum(p => p.TSNT36TE) + qkb.Sum(p => p.TSNT41TE) + qkb.Sum(p => p.TSDTNTtets) + _lngtru.Sum(p => p.TSNGTRUTE) + ktuvan06;
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 4;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "4. Khám chữa bệnh trẻ em < 6 tuổi";
                    _moi.danhmucct = "- Bệnh nhân điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null; 
                    _moi.noi = qkb.Sum(p => p.TSNT42TE);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT36TE);
                    _moi.phcn = qkb.Sum(p => p.TSNT41TE);
                    _moi.tongso = qkb.Sum(p => p.TSNT42TE) + qkb.Sum(p => p.TSNT36TE) + qkb.Sum(p => p.TSNT41TE);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 4;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "4. Khám chữa bệnh trẻ em < 6 tuổi";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p=>p.TSDTNTpkte);
                    _moi.noi = qkb.Sum(p => p.TSDTNTtenoi);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNTtecc);
                    _moi.phcn = qkb.Sum(p => p.TSDTNTtephcn);
                    _moi.tongso = qkb.Sum(p=>p.TSDTNTtets);
                    _lcontent.Add(_moi);
                //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 4;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "4. Khám chữa bệnh trẻ em < 6 tuổi";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRUTE);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRUTE);
                    _lcontent.Add(_moi);
                //bệnh nhân khám tư vấn
                    _moi = new content();
                    _moi.stt = 4;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "4. Khám chữa bệnh trẻ em < 6 tuổi";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktuvan06;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktuvan06;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 5.khám chữa bệnh bệnh nhân >60 tuổi
                //tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 5;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "5. Khám chữa bệnh bệnh nhân > 60 tuổi";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT4260) + qkb.Sum(p => p.TSNT3660) + qkb.Sum(p => p.TSNT4160) + qkb.Sum(p => p.TSDTNT60ts) + _lngtru.Sum(p => p.TSNGTRU60) + ktuvan60;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT4260) + qkb.Sum(p => p.TSNT3660) + qkb.Sum(p => p.TSNT4160) + qkb.Sum(p => p.TSDTNT60ts) + _lngtru.Sum(p => p.TSNGTRU60) + ktuvan60;
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 5;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "5. Khám chữa bệnh bệnh nhân > 60 tuổi";
                    _moi.danhmucct = "- Bệnh nhên điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null;
                    _moi.noi = qkb.Sum(p => p.TSNT4260);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT3660);
                    _moi.phcn = qkb.Sum(p => p.TSNT4160);
                    _moi.tongso = qkb.Sum(p => p.TSNT4260) + qkb.Sum(p => p.TSNT3660) + qkb.Sum(p => p.TSNT4160);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 5;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "5. Khám chữa bệnh bệnh nhân > 60 tuổi";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p=>p.TSDTNTpk60);
                    _moi.noi = qkb.Sum(p => p.TSDTNT60noi);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNT60cc);
                    _moi.phcn = qkb.Sum(p => p.TSDTNT60phcn);
                    _moi.tongso = qkb.Sum(p=>p.TSDTNT60ts);
                    _lcontent.Add(_moi);
                //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 5;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "5. Khám chữa bệnh bệnh nhân > 60 tuổi";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRU60);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRU60);
                    _lcontent.Add(_moi);
                //bệnh nhân khám tư vấn
                    _moi = new content();
                    _moi.stt = 5;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "5. Khám chữa bệnh bệnh nhân > 60 tuổi";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktuvan60;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktuvan60;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 6. khám chữa bệnh bệnh nhân > 80 tuổi
                //tổng số lần khám bệnh
                    _moi = new content();
                    _moi.stt = 6;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "6. Khám chữa bệnh bệnh nhân > 80 tuổi";
                    _moi.danhmucct = "- Tổng số lần khám bệnh";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSNT4280) + qkb.Sum(p => p.TSNT3680) + qkb.Sum(p => p.TSNT4180) + qkb.Sum(p => p.TSDTNT80ts) + _lngtru.Sum(p => p.TSNGTRU80) + ktuvan80;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = qkb.Sum(p => p.TSNT4280) + qkb.Sum(p => p.TSNT3680) + qkb.Sum(p => p.TSNT4180) + qkb.Sum(p => p.TSDTNT80ts) + _lngtru.Sum(p => p.TSNGTRU80) + ktuvan80;
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị nội trú
                    _moi = new content();
                    _moi.stt = 6;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "6. Khám chữa bệnh bệnh nhân > 80 tuổi";
                    _moi.danhmucct = "- Bệnh nhân điều trị nội trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = null;
                    _moi.noi = qkb.Sum(p => p.TSNT4280);
                    _moi.chamcuu = qkb.Sum(p => p.TSNT3680);
                    _moi.phcn = qkb.Sum(p => p.TSNT4180);
                    _moi.tongso = qkb.Sum(p => p.TSNT4280) + qkb.Sum(p => p.TSNT3680) + qkb.Sum(p => p.TSNT4180);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị ngoại trú
                    _moi = new content();
                    _moi.stt = 6;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "6. Khám chữa bệnh bệnh nhân > 80 tuổi";
                    _moi.danhmucct = "- Bệnh nhân điều trị ngoại trú";
                    _moi.donvi = "Người";
                    _moi.khambenh = qkb.Sum(p => p.TSDTNTpk80);
                    _moi.noi = qkb.Sum(p => p.TSDTNT80noi);
                    _moi.chamcuu = qkb.Sum(p => p.TSDTNT80cc);
                    _moi.phcn = qkb.Sum(p => p.TSDTNT80phcn);
                    _moi.tongso = qkb.Sum(p=>p.TSDTNT80ts);
                    _lcontent.Add(_moi);
                //bệnh nhân kê đơn
                    _moi = new content();
                    _moi.stt = 6;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "6. Khám chữa bệnh bệnh nhân > 80 tuổi";
                    _moi.danhmucct = "- Bệnh nhân kê đơn";
                    _moi.donvi = "Người";
                    _moi.khambenh = _lngtru.Sum(p => p.TSNGTRU80);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = _lngtru.Sum(p => p.TSNGTRU80);
                    _lcontent.Add(_moi);
                //bệnh nhân khám tư vấn
                    _moi = new content();
                    _moi.stt = 6;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "6. Khám chữa bệnh bệnh nhân > 80 tuổi";
                    _moi.danhmucct = "- Bệnh nhân khám tư vấn";
                    _moi.donvi = "Người";
                    _moi.khambenh = ktuvan80;
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ktuvan80;
                    _lcontent.Add(_moi);
                    #endregion
                    #region 7. tổng số ngày điều trị nội trú
                    _moi = new content();
                    _moi.stt = 7;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "7. Tổng số ngày điều trị nội trú";
                    //_moi.danhmucct = "";
                    _moi.donvi = "Ngày";
                    //_moi.khambenh = null;
                    _moi.tongnoi = ravien.Sum(p=>p.TSNGAY42);
                    _moi.tongcc = ravien.Sum(p => p.TSNGAY36);
                    _moi.tongphcn = ravien.Sum(p => p.TSNGAY41);
                    _moi.tong = ravien.Sum(p => p.TSNGAY42) + ravien.Sum(p => p.TSNGAY36) + ravien.Sum(p => p.TSNGAY41);
                    _lcontent.Add(_moi);
                    #endregion
                    #region 8. tổng số bệnh nhân ra viện
                    //tổng số
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Tổng số";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVTS42);
                    _moi.chamcuu = ravien.Sum(p => p.RVTS36);
                    _moi.phcn = ravien.Sum(p => p.RVTS41);
                    _moi.tongso = ravien.Sum(p => p.RVTS42) + ravien.Sum(p => p.RVTS36) + ravien.Sum(p => p.RVTS41);
                    _lcontent.Add(_moi);
                //bệnh nhân khỏi
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Bệnh nhân khỏi";
                    //_moi.tongnoi = ravien.Sum(p => p.RVTS42);
                    //_moi.tongcc = ravien.Sum(p => p.RVTS36);
                    //_moi.tongphcn = ravien.Sum(p => p.RVTS41);
                    //_moi.tong = ravien.Sum(p => p.RVTS42) + ravien.Sum(p => p.RVTS36) + ravien.Sum(p => p.RVTS41);
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVKHOI42);
                    _moi.chamcuu = ravien.Sum(p => p.RVKHOI36);
                    _moi.phcn = ravien.Sum(p => p.RVKHOI41);
                    _moi.tongso = ravien.Sum(p => p.RVKHOI42) + ravien.Sum(p => p.RVKHOI36) + ravien.Sum(p => p.RVKHOI41);
                    _lcontent.Add(_moi);
                //bệnh nhân đỡ, giảm
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Bệnh đỡ, giảm";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVDO42);
                    _moi.chamcuu = ravien.Sum(p => p.RVDO36);
                    _moi.phcn = ravien.Sum(p => p.RVDO41);
                    _moi.tongso = ravien.Sum(p => p.RVDO42) + ravien.Sum(p => p.RVDO36) + ravien.Sum(p => p.RVDO41);
                    _lcontent.Add(_moi);
                //bệnh nhân ko hay đổi
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Bệnh nhân không thay đổi";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVKOTD42);
                    _moi.chamcuu = ravien.Sum(p => p.RVKOTD36);
                    _moi.phcn = ravien.Sum(p => p.RVKOTD41);
                    _moi.tongso = ravien.Sum(p => p.RVKOTD42) + ravien.Sum(p => p.RVKOTD36) + ravien.Sum(p => p.RVKOTD41);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị nặng hơn
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Bệnh nhân điều trị nặng hơn";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVNANGHON42);
                    _moi.chamcuu = ravien.Sum(p => p.RVNANGHON36);
                    _moi.phcn = ravien.Sum(p => p.RVNANGHON41);
                    _moi.tongso = ravien.Sum(p => p.RVNANGHON42) + ravien.Sum(p => p.RVNANGHON36) + ravien.Sum(p => p.RVNANGHON41);
                    _lcontent.Add(_moi);
                //bệnh nhân điều trị tiên
                    _moi = new content();
                    _moi.stt = 8;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "8. Tổng số bệnh nhân ra viện";
                    _moi.danhmucct = "- Bệnh nhân điều trị tiên";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.noi = ravien.Sum(p => p.RVTUVONG42);
                    _moi.chamcuu = ravien.Sum(p => p.RVTUVONG36);
                    _moi.phcn = ravien.Sum(p => p.RVTUVONG41);
                    _moi.tongso = ravien.Sum(p => p.RVTUVONG42) + ravien.Sum(p => p.RVTUVONG36) + ravien.Sum(p => p.RVTUVONG41);
                    _lcontent.Add(_moi);
                    #endregion
                    #region 9. điều trị bằng yhct đơn
                    _moi = new content();
                    _moi.stt = 9;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "9. Điều trị bằng YHCT đơn";
                    //_moi.danhmucct = "";
                    _moi.donvi = "Người";
                    //_moi.khambenh = null;
                    _moi.tongnoi = _bnyhct.Sum(p => p.BNYHCT42);
                    _moi.tongcc = _bnyhct.Sum(p => p.BNYHCT36);
                    _moi.tongphcn = _bnyhct.Sum(p => p.BNYHCT41);
                    _moi.tong = _bnyhct.Sum(p => p.BNYHCT42) + _bnyhct.Sum(p => p.BNYHCT36) + _bnyhct.Sum(p => p.BNYHCT41);
                    _lcontent.Add(_moi);
                    #endregion
                    #region 10. ngày điều trị TB 1 BN khỏi
                    _moi = new content();
                    _moi.stt = 10;
                    _moi.danhmuctong = danhmuctong;
                    _moi.danhmuc = "10. Ngày điều trị TB 1 BN khỏi";
                    _moi.donvi = "Ngày";
                    //_moi.danhmucct = "";
                    _moi.tongnoi = songaytb42;
                    _moi.tongcc = songaytb36;
                    _moi.tongphcn = songaytb41 ;
                    _moi.tong = songaytbtong;
                    _lcontent.Add(_moi);
                    #endregion
                    #endregion
                    #region II. CÔNG TÁC CHUYỂN TUYẾN
                    #region 1. nhận người bệnh từ các tuyến chuyển đến
                //từ tuyến trên
                    _moi = new content();
                    _moi.stt = 11;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "1. Nhận người bệnh từ các tuyến chuyển đến";
                    _moi.danhmucct = "- Từ tuyến trên";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctct.Sum(p=>p.TUTUYENTREN);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctct.Sum(p => p.TUTUYENTREN);
                    _lcontent.Add(_moi);
                //cùng tuyến
                    _moi = new content();
                    _moi.stt = 11;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "1. Nhận người bệnh từ các tuyến chuyển đến";
                    _moi.danhmucct = "- Cùng tuyến";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctct.Sum(p => p.CUNGTUYEN);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctct.Sum(p => p.CUNGTUYEN);
                    _lcontent.Add(_moi);
                //từ tuyến dưới
                    _moi = new content();
                    _moi.stt = 11;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "1. Nhận người bệnh từ các tuyến chuyển đến";
                    _moi.danhmucct = "- Từ tuyến dưới";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctct.Sum(p => p.TUTUYENDUOI);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctct.Sum(p => p.TUTUYENDUOI);
                    _lcontent.Add(_moi);
                    #endregion
                    #region 2. chuyển người bệnh đi các tuyến
                //lên tuyến trên
                    _moi = new content();
                    _moi.stt = 12;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "2. Chuyển người bệnh đi các tuyến";
                    _moi.danhmucct = "- Lên tuyến trên";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctctdi.Sum(p=>p.LENTUYENTREN);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctctdi.Sum(p => p.LENTUYENTREN);
                    _lcontent.Add(_moi);
                //cùng tuyến
                    _moi = new content();
                    _moi.stt = 12;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "2. Chuyển người bệnh đi các tuyến";
                    _moi.danhmucct = "- Cùng tuyến";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctctdi.Sum(p => p.CUNGTUYEN);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctctdi.Sum(p => p.CUNGTUYEN);
                    _lcontent.Add(_moi);
                //xuống tuyến dưới
                    _moi = new content();
                    _moi.stt = 12;
                    _moi.danhmuctong = "II. CÔNG TÁC CHUYỂN TUYẾN";
                    _moi.danhmuc = "2. Chuyển người bệnh đi các tuyến";
                    _moi.danhmucct = "- Xuống tuyến dưới";
                    _moi.donvi = "Người";
                    _moi.khambenh = ctctdi.Sum(p => p.XUONGTUYENDUOI);
                    _moi.noi = null;
                    _moi.chamcuu = null;
                    _moi.phcn = null;
                    _moi.tongso = ctctdi.Sum(p => p.XUONGTUYENDUOI);
                    _lcontent.Add(_moi);
                    #endregion
                    #endregion
                    #region III. CÁC DANH MUC DICH VỤ KỸ THUẬT
            //các danh mục dịch vụ kỹ thuật: tính theo dịch vụ phẫu thuật và thủ thuật

                    var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                 join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                 select new { cd.IDCD, cd.MaDV, cls.MaKP, cls.MaKPth,cls.MaBNhan }).ToList();
                    //var _dmkttong = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                    //                 join dv in data.DichVus.Where(p => p.IdTieuNhom == 47 || p.IdTieuNhom == 13) on dtct.MaDV equals dv.MaDV
                    //                 select new
                    //                 {
                    //                     dtct.MaKP,
                    //                     dtct.SoLuong
                    //                 }).ToList();
                    //var _dmkttongkq = (from dtct in _dmkttong
                    //                 group new { dtct } by new {dtct.MaKP } into kq
                    //                 select new
                    //                 {
                    //                     kq.Key.MaKP,
                    //                     dvnoi = kq.Where(p => p.dtct.MaKP == 42).Sum(p =>  p.dtct.SoLuong),
                    //                     dvcc = kq.Where(p => p.dtct.MaKP == 36).Sum(p => p.dtct.SoLuong),
                    //                     dvphcn = kq.Where(p => p.dtct.MaKP == 41).Sum(p => p.dtct.SoLuong),
                    //                 }).ToList();
                    var _dmkttongkq = (from cls in _lcls
                                       join dv in _ldv.Where(p => p.TenRG == "Thủ thuật" || p.TenRG == "Phẫu thuật") on cls.MaDV equals dv.MaDV
                                       join kp in _lkp on cls.MaKP equals kp.MaKP
                                     group new { dv, cls,kp } by new { dv.MaDV, cls.MaKP, cls.IDCD,kp.PLoai } into kq
                                     select new
                                     {
                                         kq.Key.MaDV,
                                         kq.Key.MaKP,
                                         dvpk = kq.Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.cls.IDCD).Count(),
                                         dvnoi = kq.Where(p => p.cls.MaKP == 42).Select(p => p.cls.IDCD).Count(),
                                         dvcc = kq.Where(p => p.cls.MaKP == 36).Select(p => p.cls.IDCD).Count(),
                                         dvphcn = kq.Where(p => p.cls.MaKP == 41).Select(p => p.cls.IDCD).Count()
                                     }).ToList();
                    double tongpk = _dmkttongkq.Sum(p => p.dvpk);
                    double tongnoi = _dmkttongkq.Sum(p => p.dvnoi);
                    double tongcc = _dmkttongkq.Sum(p => p.dvcc);
                    double tongphcn = _dmkttongkq.Sum(p => p.dvphcn);
                    var _dmktkq = (from dv in _ldv.Where(p => p.TenRG == "Phẫu thuật" || p.TenRG == "Thủ thuật")
                                   join cls in _lcls on dv.MaDV equals cls.MaDV
                                   join kp in _lkp on cls.MaKP equals kp.MaKP
                                   group new { dv, cls,kp } by new { dv.MaDV, dv.TenDV, cls.MaKP,kp.PLoai } into kq
                                   select new content
                                   {
                                       stt=13,
                                       danhmuctong = "III. CÁC DANH MỤC KỸ THUẬT",
                                       danhmuc = "Tổng số",
                                       donvi = "Lượt",
                                       danhmucct = kq.Key.TenDV,
                                       khambenh = kq.Where(p => p.kp.PLoai == "Phòng khám").Select(p => p.cls.IDCD).Count(),
                                       noi = kq.Where(p => p.cls.MaKP == 42).Select(p => p.cls.IDCD).Count(),
                                       chamcuu = kq.Where(p => p.cls.MaKP == 36).Select(p => p.cls.IDCD).Count(),
                                       phcn = kq.Where(p => p.cls.MaKP == 41).Select(p => p.cls.IDCD).Count(),
                                       tongso = kq.Select(p => p.cls.IDCD).Count(),
                                       tongnoi = tongnoi,
                                       tongcc = tongcc,
                                       tongphcn = tongphcn,
                                       tongkb=tongpk,
                                       tong = tongcc + tongnoi + tongphcn+tongpk

                                   }).ToList();


                    //var _dmktkq = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                    //               join dv in data.DichVus.Where(p => p.IdTieuNhom == 47 || p.IdTieuNhom == 13) on dtct.MaDV equals dv.MaDV
                    //             group new { dtct,dv } by new { dtct.MaDV ,dv.TenDV} into kq
                    //             select new content
                    //             {
                    //                 danhmuctong = "III. CÁC DANH MỤC KỸ THUẬT",
                    //                 danhmuc = "Tổng số",
                    //                 donvi = "Lượt",
                    //                 danhmucct = kq.Key.TenDV,
                    //                 noi = kq.Where(p => p.dtct.MaKP == 42).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dtct.MaKP == 42).Sum(p => p.dtct.SoLuong),
                    //                 chamcuu = kq.Where(p => p.dtct.MaKP == 36).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dtct.MaKP == 36).Sum(p => p.dtct.SoLuong),
                    //                 phcn = kq.Where(p => p.dtct.MaKP == 41).Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Where(p => p.dtct.MaKP == 41).Sum(p => p.dtct.SoLuong),
                    //                 tongso = kq.Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Sum(p => p.dtct.SoLuong),
                    //                 tongnoi=tongnoi,
                    //                 tongcc=tongcc,
                    //                 tongphcn=tongphcn,
                    //                 tong=tongcc+tongnoi+tongphcn
                    //                 //tongcc = 
                    //                 //tongphcn = 
                    //                 //tong = 
                    //             }).ToList();

                    //if (_dmkttong != null)
                    //{
                    //    rep.tongdvnoi.Value = _dmkttong.Sum(p => p.noi).ToString();
                    //    rep.tongdvcc.Value = _dmkttong.Sum(p => p.chamcuu).ToString();
                    //    rep.tongdvphcn.Value = _dmkttong.Sum(p => p.phcn).ToString();
                    //    rep.tongso.Value = _dmkttong.Sum(p => p.tongso).ToString();
                    //}
                    //_lcontent.AddRange(_dmkttong);
           _lcontent.AddRange(_dmktkq);
           //int i = 1;

           // foreach(var item in _dmkt)
           // {

           //     _moi = new content();
           //     _moi.stt = i;
           //     _moi.danhmuctong = "III. CÁC DANH MUC KY THUẬT";
           //     _moi.danhmuc = "";
           //     _moi.danhmucct = item.danhmucct;
           //     _moi.donvi = "Lượt";
           //     _moi.khambenh = null;
           //     _moi.noi = null;
           //     _moi.chamcuu = null;
           //     _moi.phcn = null;
           //     _moi.tongso = null;
           //     _lcontent.Add(_moi);
           //     i++;
           // }
                    #endregion
                    
           //var kq1 = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
           //           join dt in data.DThuocs.Where(p=>p.PLDV==2) on dtct.IDDon equals dt.IDDon
           //           join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
           //           select new
           //           {
           //               dt.MaKP,
           //               dtct.MaDV,
           //               bn.NoiTru,
           //               dtct.SoLuong
           //           }).ToList();
           var kq1 = (from cls in _lcls
                      join dv in _ldv on cls.MaDV equals dv.MaDV
                      join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                      select new
                      {
                          cls.MaKP,
                          dv.TenRG,
                          dv.IdTieuNhom,
                          dv.MaDV,
                          bn.NoiTru,
                          cls.IDCD
                      }).ToList();
           var tong = (from bn in kq1
                       //join dv in _ldv on bn.MaDV equals dv.MaDV
                       //join tn in _ltn on dv.IdTieuNhom equals tn.IdTieuNhom
                       group new { bn} by new { bn.IdTieuNhom, bn.TenRG } into kq
                       select new
                       {
                           SATONG = kq.Where(p => p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p=>p.bn.IDCD).Count(),
                           XQTONG = kq.Where(p => p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.bn.IDCD).Count(),
                           DNTONG = kq.Where(p => p.bn.TenRG == "Điện não đồ").Select(p=>p.bn.IDCD).Count(),
                           DTTONG = kq.Where(p => p.bn.TenRG == "Điện tim").Select(p=>p.bn.IDCD).Count(),
                           DXTONG = kq.Where(p => p.bn.TenRG == "Đo mật độ xương").Select(p=>p.bn.IDCD).Count(),
                           TTTONG = kq.Where(p => p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count()
                       }).ToList();
           var clspk = (from bn in kq1.Where(p=>p.NoiTru==0)
                        //join kp in _lkp.Where(p => p.PLoai == "Phòng khám") on bn.MaKP equals kp.MaKP
                        //join kp in khoa on bn.MaKP equals kp.makp
                        //join dv in _ldv on bn.MaDV equals dv.MaDV
                        //join tn in _ltn on dv.IdTieuNhom equals tn.IdTieuNhom
                        group new { bn } by new { bn.TenRG, bn.IdTieuNhom } into kq
                        select new
                        {
                            SANGTPK = kq.Where(p => p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.bn.IDCD).Count(),
                            XQNGTPK = kq.Where(p => p.bn.TenRG == "X-Quang").Select(p => p.bn.IDCD).Count(),
                            DNNGTPK = kq.Where(p => p.bn.TenRG == "Điện não đồ").Select(p => p.bn.IDCD).Count(),
                            DTNGTPK = kq.Where(p => p.bn.TenRG == "Điện tim").Select(p => p.bn.IDCD).Count(),
                            DXNGTPK = kq.Where(p => p.bn.TenRG == "Đo mật độ xương").Select(p => p.bn.IDCD).Count(),
                            TTNGTPK = kq.Where(p => p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count()
                        }).ToList();
           var lcls = (from bn in kq1.Where(p => p.NoiTru == 1)
                       //join dv in dichvu on bn.MaDV equals dv.MaDV
                       //join tn in _ltn on dv.IdTieuNhom equals tn.IdTieuNhom
                       join kp in _lkp on bn.MaKP equals kp.MaKP
                       group new { bn,kp } by new { bn.IdTieuNhom, bn.TenRG ,bn.MaKP,kp.PLoai} into kq
                       select new
                       {
                           //siêu âm
                           SAPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.bn.IDCD).Count(),
                           SANOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.bn.IDCD).Count(),
                           SACHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.bn.IDCD).Count(),
                           SAPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.bn.IDCD).Count(),
                           //X-quang
                           XQPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.bn.IDCD).Count(),
                           XQNOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.bn.IDCD).Count(),
                           XQCHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.bn.IDCD).Count(),
                           XQPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.bn.IDCD).Count(),
                           //điện não
                           DNPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == "Điện não đồ").Select(p => p.bn.IDCD).Count(),
                           DNNOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == "Điện não đồ").Select(p => p.bn.IDCD).Count(),
                           DNCHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == "Điện não đồ").Select(p => p.bn.IDCD).Count(),
                           DNPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == "Điện não đồ").Select(p => p.bn.IDCD).Count(),
                           //điện tim đồ
                           DTPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == "Điện tim").Select(p => p.bn.IDCD).Count(),
                           DTNOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == "Điện tim").Select(p => p.bn.IDCD).Count(),
                           DTCHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == "Điện tim").Select(p => p.bn.IDCD).Count(),
                           DTPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == "Điện tim").Select(p => p.bn.IDCD).Count(),
                           //đo mật độ xương
                           DXPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == "Đo mật độ xương").Select(p => p.bn.IDCD).Count(),
                           DXNOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == "Đo mật độ xương").Select(p => p.bn.IDCD).Count(),
                           DXCHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == "Đo mật độ xương").Select(p => p.bn.IDCD).Count(),
                           DXPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == "Đo mật độ xương").Select(p => p.bn.IDCD).Count(),
                           //Thủ thuật
                           TTPK = kq.Where(p => p.kp.PLoai == "Phòng khám" && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count(),
                           TTNOI = kq.Where(p => p.bn.MaKP == 42 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count(),
                           TTCHAMCUU = kq.Where(p => p.bn.MaKP == 36 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count(),
                           TTPHCN = kq.Where(p => p.bn.MaKP == 41 && p.bn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Select(p => p.bn.IDCD).Count(),
                       }).ToList();
                   #region IV. CẬN LÂM SÀNG
                   #region 1. siêu âm
            //nội trú
           _moi = new content();
           _moi.stt = 14;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "1. Tổng số siêu âm";
           _moi.tongcls = tong.Sum(p => p.SATONG);
           _moi.tong = tong.Sum(p => p.SATONG);
           _moi.danhmucct = "- Nội trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = lcls.Sum(p => p.SAPK);
           _moi.noi = lcls.Sum(p=>p.SANOI);
           _moi.chamcuu = lcls.Sum(p=>p.SACHAMCUU);
           _moi.phcn = lcls.Sum(p=>p.SAPHCN);
           _moi.cls = lcls.Sum(p => p.SANOI) + lcls.Sum(p => p.SACHAMCUU) + lcls.Sum(p => p.SAPHCN) + lcls.Sum(p => p.SAPK);
           _moi.tongso = lcls.Sum(p => p.SANOI) + lcls.Sum(p => p.SACHAMCUU) + lcls.Sum(p => p.SAPHCN) + lcls.Sum(p => p.SAPK);
           _lcontent.Add(_moi);
            //ngoại trú
           _moi = new content();
           _moi.stt = 14;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "1. Tổng số siêu âm";
           _moi.danhmucct = "- Ngoại trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = clspk.Sum(p=>p.SANGTPK);
           _moi.noi = null;
           _moi.chamcuu = null;
           _moi.phcn = null;
           _moi.cls = clspk.Sum(p => p.SANGTPK);
           _moi.tongso = clspk.Sum(p => p.SANGTPK);
           _lcontent.Add(_moi);
                   #endregion
                   #region 2. X-Quang
            //nội trú
           _moi = new content();
           _moi.stt = 15;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "2. Tổng số X-Quang";
           _moi.tongcls = tong.Sum(p => p.XQTONG);
           _moi.tong = tong.Sum(p => p.XQTONG);
           _moi.danhmucct = "- Nội trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = lcls.Sum(p => p.XQPK);
           _moi.noi = lcls.Sum(p=>p.XQNOI);
           _moi.chamcuu = lcls.Sum(p => p.XQCHAMCUU);
           _moi.phcn = lcls.Sum(p => p.XQPHCN);
           _moi.cls = lcls.Sum(p => p.XQNOI) + lcls.Sum(p => p.XQCHAMCUU) + lcls.Sum(p => p.XQPHCN) + lcls.Sum(p => p.XQPK);
           _moi.tongso = lcls.Sum(p => p.XQNOI) + lcls.Sum(p => p.XQCHAMCUU) + lcls.Sum(p => p.XQPHCN) + lcls.Sum(p => p.XQPK);
           _lcontent.Add(_moi);
            //ngoại trú
           _moi = new content();
           _moi.stt = 15;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "2. Tổng số X-Quang";
           _moi.danhmucct = "- Ngoại trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = clspk.Sum(p=>p.XQNGTPK);
           _moi.noi = null;
           _moi.chamcuu = null;
           _moi.phcn = null;
           _moi.cls = clspk.Sum(p => p.XQNGTPK);
           _moi.tongso = clspk.Sum(p => p.XQNGTPK);
           _lcontent.Add(_moi);
                   #endregion
                   #region 3. điện não
            //nội trú
           _moi = new content();
           _moi.stt = 16;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "3. Điện não";
           _moi.tongcls = tong.Sum(p => p.DNTONG);
           _moi.tong = tong.Sum(p => p.DNTONG);
           _moi.danhmucct = "- Nội trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = lcls.Sum(p => p.DNPK);
           _moi.noi = lcls.Sum(p=>p.DNNOI);
           _moi.chamcuu = lcls.Sum(p => p.DNCHAMCUU);
           _moi.phcn = lcls.Sum(p => p.DNPHCN);
           _moi.cls = lcls.Sum(p => p.DNNOI) + lcls.Sum(p => p.DNCHAMCUU) + lcls.Sum(p => p.DNPHCN) + lcls.Sum(p => p.DNPK);
           _moi.tongso = lcls.Sum(p => p.DNNOI) + lcls.Sum(p => p.DNCHAMCUU) + lcls.Sum(p => p.DNPHCN) + lcls.Sum(p => p.DNPK);
           _lcontent.Add(_moi);
            //ngoại trú
           _moi = new content();
           _moi.stt = 16;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "3. Điện não";
           _moi.danhmucct = "- Ngoại trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = clspk.Sum(p=>p.DNNGTPK);
           _moi.noi = null;
           _moi.chamcuu = null;
           _moi.phcn = null;
           _moi.cls = clspk.Sum(p => p.DNNGTPK);
           _moi.tongso = clspk.Sum(p => p.DNNGTPK);
           _lcontent.Add(_moi);
           #endregion
                   #region 4. điện tim
            //nội trú
           _moi = new content();
           _moi.stt = 17;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "4. Điện tâm đồ";
           _moi.tongcls = tong.Sum(p => p.DTTONG);
           _moi.tong = tong.Sum(p => p.DTTONG);
           _moi.danhmucct = "- Nội trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = lcls.Sum(p => p.DTPK);
           _moi.noi = lcls.Sum(p=>p.DTNOI);
           _moi.chamcuu = lcls.Sum(p => p.DTCHAMCUU);
           _moi.phcn = lcls.Sum(p => p.DTPHCN);
           _moi.cls = lcls.Sum(p => p.DTNOI) + lcls.Sum(p => p.DTCHAMCUU) + lcls.Sum(p => p.DTPHCN) + lcls.Sum(p => p.DTPK);
           _moi.tongso = lcls.Sum(p => p.DTNOI) + lcls.Sum(p => p.DTCHAMCUU) + lcls.Sum(p => p.DTPHCN) + lcls.Sum(p => p.DTPK);
           _lcontent.Add(_moi);
            //ngoại trú
           _moi = new content();
           _moi.stt = 17;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "4. Điện tâm đồ";
           _moi.danhmucct = "- Ngoại trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = clspk.Sum(p => p.DTNGTPK);
           _moi.noi = null;
           _moi.chamcuu = null;
           _moi.phcn = null;
           _moi.cls = clspk.Sum(p=>p.DTNGTPK);
           _moi.tongso = clspk.Sum(p => p.DTNGTPK);
           _lcontent.Add(_moi);
           #endregion
                   #region 5. đo độ loãng xương
            //nội trú
           _moi = new content();
           _moi.stt = 18;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "5. Đo độ loãng xương";
           _moi.tongcls = tong.Sum(p => p.DXTONG);
           _moi.tong = tong.Sum(p => p.DXTONG);
           _moi.danhmucct = "- Nội trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = lcls.Sum(p => p.DXPK);
           _moi.noi = lcls.Sum(p=>p.DXNOI);
           _moi.chamcuu = lcls.Sum(p => p.DXCHAMCUU);
           _moi.phcn = lcls.Sum(p => p.DXPHCN);
           _moi.cls = lcls.Sum(p => p.DXNOI) + lcls.Sum(p => p.DXCHAMCUU) + lcls.Sum(p => p.DXPHCN) + lcls.Sum(p => p.DXPK);
           _moi.tongso = lcls.Sum(p => p.DXNOI) + lcls.Sum(p => p.DXCHAMCUU) + lcls.Sum(p => p.DXPHCN) + lcls.Sum(p => p.DXPK);
           _lcontent.Add(_moi);
            //ngoại trú
           _moi = new content();
           _moi.stt = 18;
           _moi.danhmuctong = "IV. CẬN LÂM SÀNG";
           _moi.danhmuc = "5. Đo độ loãng xương";
           _moi.danhmucct = "- Ngoại trú";
           _moi.donvi = "Lượt";
           _moi.khambenh = clspk.Sum(p=>p.DXNGTPK);
           _moi.noi = null;
           _moi.chamcuu = null;
           _moi.phcn = null;
           _moi.cls = clspk.Sum(p => p.DXNGTPK);
           _moi.tongso = clspk.Sum(p => p.DXNGTPK);
           _lcontent.Add(_moi);
#endregion
                   
           var lclsxetnghiem = (from cd in _lcls
                           join kp in data.KPhongs on cd.MaKP equals kp.MaKP
                           
                           select new
                           {

                               cd.IDCD,
                               cd.MaDV,
                               cd.MaKP,
                               kp.PLoai,
                               cd.MaKPth
                           }).ToList();
           #region 6. thủ thuật
           var lshtong1 = (from sh in lclsxetnghiem
                          join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat) on sh.MaDV equals dv.MaDV
                          //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group sh by new { sh.MaKP, sh.PLoai } into kq
                          select new
                          {
                              tongkb = kq.Where(p => p.PLoai == "Phòng khám").Select(p => p.IDCD).Count(),
                              tongcc = kq.Where(p => p.MaKP == 36).Select(p => p.IDCD).Count(),
                              tongphcn = kq.Where(p => p.MaKP == 41).Select(p => p.IDCD).Count(),
                              tongnoi = kq.Where(p => p.MaKP == 42).Select(p => p.IDCD).Count(),
                              tongcls = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              tong = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count()
                          }).ToList();

           var lsinhhoakq1 = (from sh in lclsxetnghiem
                             join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat) on sh.MaDV equals dv.MaDV

                             //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on dv.IdTieuNhom equals tn.IdTieuNhom
                             group new { sh, dv } by new { sh.MaDV, dv.IdTieuNhom, dv.TenRG, sh.IDCD, dv.TenDV } into kq
                             select new content
                             {
                                 stt = 19,
                                 danhmuctong = "IV. CẬN LÂM SÀNG",
                                 danhmuc = "6. Thủ thuật",
                                 danhmucct = kq.Key.TenDV,
                                 donvi = "Lượt",
                                 khambenh = kq.Where(p => p.sh.PLoai == "Phòng khám").Select(p => p.sh.IDCD).Count(),
                                 noi = kq.Where(p => p.sh.MaKP == 42).Select(p => p.sh.IDCD).Count(),
                                 chamcuu = kq.Where(p => p.sh.MaKP == 36).Select(p => p.sh.IDCD).Count(),
                                 phcn = kq.Where(p => p.sh.MaKP == 41).Select(p => p.sh.IDCD).Count(),
                                 cls = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongso = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongkb = lshtong1.Sum(p => p.tongkb),
                                 tongnoi = lshtong1.Sum(p => p.tongnoi),
                                 tongcc = lshtong1.Sum(p => p.tongcc),
                                 tongphcn = lshtong1.Sum(p => p.tongphcn),
                                 tongcls = lshtong1.Sum(p => p.tongcls),
                                 tong = lshtong1.Sum(p => p.tong)
                             }).ToList();

           _lcontent.AddRange(lsinhhoakq1.OrderBy(p => p.danhmucct));
           #endregion
           #region 7. Xét nghiệm sinh hóa máu
           var lshtong = (from sh in lclsxetnghiem
                          join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on sh.MaDV equals dv.MaDV
                          //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group sh by new { sh.MaKP, sh.PLoai } into kq
                          select new 
                          {
                              tongkb = kq.Where(p => p.PLoai == "Phòng khám").Select(p => p.IDCD).Count(),
                              tongcc = kq.Where(p => p.MaKP == 36).Select(p => p.IDCD).Count(),
                              tongphcn = kq.Where(p => p.MaKP == 41).Select(p => p.IDCD).Count(),
                              tongnoi = kq.Where(p => p.MaKP == 42).Select(p => p.IDCD).Count(),
                              tongcls = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              tong = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count()
                          }).ToList();
           
           var lsinhhoakq = (from sh in lclsxetnghiem
                             join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on sh.MaDV equals dv.MaDV

                             //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) on dv.IdTieuNhom equals tn.IdTieuNhom
                             group new { sh, dv } by new { sh.MaDV, dv.IdTieuNhom, dv.TenRG, sh.IDCD, dv.TenDV } into kq
                             select new content
                             {
                                 stt=20,
                                 danhmuctong = "IV. CẬN LÂM SÀNG",
                                 danhmuc = "7. Xét nghiệm hóa sinh máu",
                                 
                                 danhmucct = kq.Key.TenDV,
                                 donvi = "Lượt",
                                 khambenh = kq.Where(p => p.sh.PLoai == "Phòng khám").Select(p => p.sh.IDCD).Count(),
                                 noi = kq.Where(p => p.sh.MaKP == 42).Select(p => p.sh.IDCD).Count(),
                                 chamcuu = kq.Where(p => p.sh.MaKP == 36).Select(p => p.sh.IDCD).Count(),
                                 phcn = kq.Where(p => p.sh.MaKP == 41).Select(p => p.sh.IDCD).Count(),
                                 cls = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongso = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongkb = lshtong.Sum(p => p.tongkb),
                                 tongnoi=lshtong.Sum(p=>p.tongnoi),
                                 tongcc=lshtong.Sum(p=>p.tongcc),
                                 tongphcn = lshtong.Sum(p => p.tongphcn),
                                 tongcls = lshtong.Sum(p => p.tongcls),
                                 tong = lshtong.Sum(p => p.tong)
                             }).ToList();

           _lcontent.AddRange(lsinhhoakq.OrderBy(p=>p.danhmucct));
           #endregion
           #region 8. Xét Nghiệm huyết học
           var lhhtong = (from sh in lclsxetnghiem
                          join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) on sh.MaDV equals dv.MaDV
                          //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group sh by new { sh.MaKP, sh.PLoai } into kq
                          select new
                          {
                              tongkb = kq.Where(p => p.PLoai == "Phòng khám").Select(p => p.IDCD).Count(),
                              tongcc = kq.Where(p => p.MaKP == 36).Select(p => p.IDCD).Count(),
                              tongphcn = kq.Where(p => p.MaKP == 41).Select(p => p.IDCD).Count(),
                              tongnoi = kq.Where(p => p.MaKP == 42).Select(p => p.IDCD).Count(),
                              tongcls = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              tong = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              
                          }).ToList();
           var lhuyethockq = (from sh in lclsxetnghiem
                              join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) on sh.MaDV equals dv.MaDV
                              //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) on dv.IdTieuNhom equals tn.IdTieuNhom
                              group new { sh, dv } by new { sh.MaDV, dv.IdTieuNhom, dv.TenRG, sh.IDCD, dv.TenDV } into kq
                             select new content
                             {
                                 stt=21,
                                 danhmuctong = "IV. CẬN LÂM SÀNG",
                                 danhmuc = "8. Xét nghiệm huyết học",
                                 danhmucct = kq.Key.TenDV,
                                 donvi = "Lượt",
                                 khambenh = kq.Where(p => p.sh.PLoai == "Phòng khám").Select(p => p.sh.IDCD).Count(),
                                 noi = kq.Where(p => p.sh.MaKP == 42).Select(p => p.sh.IDCD).Count(),
                                 chamcuu = kq.Where(p => p.sh.MaKP == 36).Select(p => p.sh.IDCD).Count(),
                                 phcn = kq.Where(p => p.sh.MaKP == 41).Select(p => p.sh.IDCD).Count(),
                                 cls = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongso = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                 tongkb = lhhtong.Sum(p => p.tongkb),
                                 tongnoi = lhhtong.Sum(p => p.tongnoi),
                                 tongcc = lhhtong.Sum(p => p.tongcc),
                                 tongphcn = lhhtong.Sum(p => p.tongphcn),
                                 tongcls = lhhtong.Sum(p => p.tongcls),
                                 tong = lhhtong.Sum(p => p.tong)
                             }).ToList();
           _lcontent.AddRange(lhuyethockq.OrderBy(p=>p.danhmucct));
           #endregion
           #region 9.Xét nghiệm nước tiểu 10
           var lnttong = (from sh in lclsxetnghiem
                          join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo) on sh.MaDV equals dv.MaDV
                          //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group sh by new { sh.MaKP, sh.PLoai } into kq
                          select new
                          {
                              tongkb = kq.Where(p => p.PLoai == "Phòng khám").Select(p => p.IDCD).Count(),
                              tongcc = kq.Where(p => p.MaKP == 36).Select(p => p.IDCD).Count(),
                              tongphcn = kq.Where(p => p.MaKP == 41).Select(p => p.IDCD).Count(),
                              tongnoi = kq.Where(p => p.MaKP == 42).Select(p => p.IDCD).Count(),
                              tongcls = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              tong = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),

                          }).ToList();
           var lxnnuoctieu = (from sh in lclsxetnghiem
                              join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo) on sh.MaDV equals dv.MaDV

                              //join tn in data.TieuNhomDVs.Where(p => p.TenRG ==  DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo) on dv.IdTieuNhom equals tn.IdTieuNhom
                              group new { sh, dv } by new { sh.MaDV, dv.IdTieuNhom, dv.TenRG, sh.IDCD, dv.TenDV } into kq
                              select new content
                              {
                                  stt=22,
                                  danhmuctong = "IV. CẬN LÂM SÀNG",
                                  danhmuc = "9. Xét nghiệm nước tiểu 10",
                                  danhmucct = kq.Key.TenDV,
                                  donvi = "Lượt",
                                  khambenh = kq.Where(p => p.sh.PLoai == "Phòng khám").Select(p => p.sh.IDCD).Count(),
                                  noi = kq.Where(p => p.sh.MaKP == 42).Select(p => p.sh.IDCD).Count(),
                                  chamcuu = kq.Where(p => p.sh.MaKP == 36).Select(p => p.sh.IDCD).Count(),
                                  phcn = kq.Where(p => p.sh.MaKP == 41).Select(p => p.sh.IDCD).Count(),
                                  cls = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                  tongso = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                                  tongkb = lnttong.Sum(p => p.tongkb),
                                  tongnoi = lnttong.Sum(p => p.tongnoi),
                                  tongcc = lnttong.Sum(p => p.tongcc),
                                  tongphcn = lnttong.Sum(p => p.tongphcn),
                                  tongcls = lnttong.Sum(p => p.tongcls),
                                  tong = lnttong.Sum(p => p.tong)
                              }).ToList();
           _lcontent.AddRange(lxnnuoctieu.OrderBy(p=>p.danhmucct));
           #endregion
           #region 10. Xét nghiệm khác
           var lxnktong = (from sh in lclsxetnghiem
                           join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac) on sh.MaDV equals dv.MaDV
                           //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group sh by new { sh.MaKP, sh.PLoai } into kq
                          select new
                          {
                              tongkb = kq.Where(p => p.PLoai == "Phòng khám").Select(p => p.IDCD).Count(),
                              tongcc = kq.Where(p => p.MaKP == 36).Select(p => p.IDCD).Count(),
                              tongphcn = kq.Where(p => p.MaKP == 41).Select(p => p.IDCD).Count(),
                              tongnoi = kq.Where(p => p.MaKP == 42).Select(p => p.IDCD).Count(),
                              tongcls = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),
                              tong = kq.Where(p => p.MaKPth == 34).Select(p => p.IDCD).Count(),

                          }).ToList();
           var lxnkhac = (from sh in lclsxetnghiem
                          join dv in _ldv.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac) on sh.MaDV equals dv.MaDV
                          //join tn in _ltn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac) on dv.IdTieuNhom equals tn.IdTieuNhom
                          group new { sh, dv } by new { sh.MaDV, dv.IdTieuNhom, dv.TenRG, sh.IDCD, dv.TenDV } into kq
                          select new content
                          {
                              stt=23,
                              danhmuctong = "IV. CẬN LÂM SÀNG",
                              danhmuc = "10. Các xét nghiệm khác",
                              danhmucct = kq.Key.TenDV,
                              donvi = "Lượt",
                              khambenh = kq.Where(p => p.sh.PLoai == "Phòng khám").Select(p => p.sh.IDCD).Count(),
                              noi = kq.Where(p => p.sh.MaKP == 42).Select(p => p.sh.IDCD).Count(),
                              chamcuu = kq.Where(p => p.sh.MaKP == 36).Select(p => p.sh.IDCD).Count(),
                              phcn = kq.Where(p => p.sh.MaKP == 41).Select(p => p.sh.IDCD).Count(),
                              cls = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                              tongso = kq.Where(p => p.sh.MaKPth == 34).Select(p => p.sh.IDCD).Count(),
                              tongkb = lxnktong.Sum(p => p.tongkb),
                              tongnoi = lxnktong.Sum(p => p.tongnoi),
                              tongcc = lxnktong.Sum(p => p.tongcc),
                              tongphcn = lxnktong.Sum(p => p.tongphcn),
                              tongcls = lxnktong.Sum(p => p.tongcls),
                              tong = lxnktong.Sum(p => p.tong)
                          }).ToList();
           _lcontent.AddRange(lxnkhac);
           #endregion
                   #endregion
           var ketqua = (from lct in _lcontent
                                  group lct by new
                                  {
                                      lct.danhmuctong,
                                      lct.danhmuc,
                                      lct.danhmucct,
                                      lct.stt,
                                      lct.donvi,
                                      lct.tongkb,
                                      lct.tongnoi,
                                      lct.tongcc,
                                      lct.tongphcn,
                                      lct.tongcls,
                                      lct.tong
                                  } into kq
                                  select new
                                  {
                                      kq.Key.danhmuctong,
                                      kq.Key.danhmuc,
                                      kq.Key.danhmucct,
                                      kq.Key.stt,
                                      tongkb = kq.Key.tongkb == 0 ? null : kq.Key.tongkb,
                                      tongcc = kq.Key.tongcc == 0 ? null : kq.Key.tongcc,
                                      tongphcn= kq.Key.tongphcn==0? null:kq.Key.tongphcn,
                                      tongnoi = kq.Key.tongnoi == 0 ? null : kq.Key.tongnoi,
                                      tongcls = kq.Key.tongcls == 0 ? null : kq.Key.tongcls,
                                      tong = kq.Key.tong,
                                      khambenh = kq.Sum(p => p.khambenh) == 0 ? null : kq.Sum(p => p.khambenh),
                                      noi = kq.Sum(p => p.noi) == 0 ? null : kq.Sum(p => p.noi),
                                      kq.Key.donvi,
                                      chamcuu = kq.Sum(p => p.chamcuu) == 0 ? null : kq.Sum(p => p.chamcuu),
                                      phcn = kq.Sum(p => p.phcn) == 0 ? null : kq.Sum(p => p.phcn),
                                      cls = kq.Sum(p => p.cls) == 0 ? null : kq.Sum(p => p.cls),
                                      tongso = kq.Sum(p => p.tongso)
                                  }).ToList();
           rep.DataSource = ketqua.OrderBy(p => p.stt);
                    rep.databinding();
                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            }
        
    }
    public class content
    {

        public int stt { get; set; }
        public string danhmuctong { get; set; }
        public string danhmuc { get; set; }
        public string danhmucct { get; set; }
        public string donvi { get; set; }
        public double? khambenh { get; set; }
        public double? noi { get; set; }
        public double? chamcuu { get; set; }
        public double? phcn { get; set; }
        public double? cls { get; set; }
        public double? tongso { get; set; }
        public double? tongkb { get; set; }
        public double? tongnoi { get; set; }
        public double? tongcc { get; set; }
        public double? tongphcn { get; set; }
        public double? tongcls { get; set; }
        public double? tong { get; set; }
    }

