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
    public partial class frm_BCBNRa_VaoVien : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCBNRa_VaoVien()
        {
            InitializeComponent();
        }
        private class c_KPhong
        {
            private string TenKP;
            private int MaKP;
            private string PLoai;
            private string ChuyenKhoa;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public string chuyenkhoa
            { set { ChuyenKhoa = value; } get { return ChuyenKhoa; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<c_KPhong> _Kphong = new List<c_KPhong>();
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BCBNRa_VaoVien_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.Focus();
            _Kphong.Clear();
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP, kp.PLoai, kp.ChuyenKhoa }).ToList();
            if (kphong.Count > 0)
            {
                c_KPhong themmoi1 = new c_KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.ploai = "";
                themmoi1.chuyenkhoa = "";
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    c_KPhong themmoi = new c_KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.ploai = a.PLoai;
                    themmoi.chuyenkhoa = a.ChuyenKhoa;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                cklKP.DataSource = _Kphong.ToList();
            }
            cklKP.CheckAll();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(cklKP.SelectedValue) == 0)
            {
                if (cklKP.GetItemChecked(cklKP.SelectedIndex))
                {
                    cklKP.CheckAll();

                }
                else
                {
                    cklKP.UnCheckAll();
                }
            }

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                if (cklKP.GetItemChecked(i))
                {

                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp || item.makp == 0)
                        {
                            item.chon = false;
                            // break;
                        }
                    }
                }
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            BaoCao.rep_SoBNVao_RaVien rep = new BaoCao.rep_SoBNVao_RaVien();
            frmIn frm = new frmIn();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            break;
                        }
                    }
                }
            }

                var kp11 = _Kphong.Where(p => p.chon == true).ToList();
                if (ckcbn.Checked)
                {
                    var _lbnvv = data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Select(p => new { p.MaBNhan, p.NgayVao, p.MaKP }).Distinct().ToList();
                    var _lbnrv = data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => new { p.MaBNhan, p.NgayRa, MaKPr = p.MaKP }).ToList();
                    var _kqc = (from vv in _lbnvv
                                join rv in _lbnrv on vv.MaBNhan equals rv.MaBNhan
                                join kp in kp11 on rv.MaKPr equals kp.makp
                                where (vv.NgayVao.Value.Date == rv.NgayRa.Value.Date)
                                group new { vv, rv } by new { vv.MaBNhan, vv.NgayVao, rv.NgayRa, vv.MaKP, rv.MaKPr } into kq
                                select new
                                {
                                    kq.Key.MaBNhan,
                                    NgayRa = Convert.ToInt32(kq.Key.NgayRa.Value.ToString("HH")),
                                    NgayVao = Convert.ToInt32(kq.Key.NgayVao.Value.ToString("HH")),
                                    kq.Key.MaKP,
                                    kq.Key.MaKPr,

                                }).Distinct().ToList();


                    if (_kqc.Count() > 0)
                    {
                        rep.TIEUDE.Value = "BÁO CÁO TỔNG HỢP SỐ LIỆU VÀO/RA VIỆN";
                        rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TENBV.Value = DungChung.Bien.TenCQ.ToUpper();
                        int vvs = 0, vvc = 0, rvs = 0, rvc = 0;
                        vvs = _kqc.Where(p => p.NgayVao > 0 && p.NgayVao < 12).Select(p => p.MaBNhan).Count();
                        vvc = _kqc.Where(p => p.NgayVao >= 12 && p.NgayVao < 24).Select(p => p.MaBNhan).Count();
                        rvs = _kqc.Where(p => p.NgayRa > 0 && p.NgayRa < 12).Select(p => p.MaBNhan).Count();
                        rvc = _kqc.Where(p => p.NgayRa >= 12 && p.NgayRa < 24).Select(p => p.MaBNhan).Count();
                        rep.VVSANG.Value = vvs;
                        rep.VVCHIEU.Value = vvc;
                        rep.RVSANG.Value = rvs;
                        rep.RVCHIEU.Value = rvc;
                        rep.TONG.Value = vvc + vvs + rvs + rvc;
                        rep.NLB.Value = DungChung.Bien.NguoiLapBieu;
                        rep.NGAYTHANG.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Không có số liệu !", "Thông báo", MessageBoxButtons.OK);
                        lupTuNgay.Focus();
                    }
                }
            else
                {
                    var _lbnvv1 = (from vv in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay)
                                  join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on vv.MaBNhan equals bn.MaBNhan
                                  select new
                                  {
                                      vv.MaBNhan,
                                      vv.NgayVao,
                                      vv.idVaoVien,
                                      vv.MaKP
                                  }).Distinct().ToList();
                    var _lbnvv = (from vv in _lbnvv1
                                  join kp in kp11 on vv.MaKP equals kp.makp
                                  group vv by vv into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.idVaoVien,
                                      NgayVao = Convert.ToInt32(kq.Key.NgayVao.Value.ToString("HH"))
                                  }).ToList();
                    var _lbnrv1 = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                                  join bn in data.BenhNhans.Where(p => p.NoiTru == 1) on rv.MaBNhan equals bn.MaBNhan
                                  select new
                                  {
                                      rv.MaBNhan,
                                      rv.IdRaVien,rv.MaKP,
                                      rv.NgayRa
                                  }).Distinct().ToList();
                    var _lbnrv = (from rv in _lbnrv1
                                  join kp in kp11 on rv.MaKP equals kp.makp
                                  group rv by rv into kq
                                  select new
                                  {
                                      kq.Key.IdRaVien,
                                      kq.Key.MaBNhan,
                                      NgayRa = Convert.ToInt32(kq.Key.NgayRa.Value.ToString("HH")),
                                  }).ToList();
                    int vvs = 0, vvc = 0, rvs = 0, rvc = 0;
                    if(_lbnvv.Count()>0)
                    {
                        vvs = _lbnvv.Where(p => p.NgayVao >= 0 && p.NgayVao < 12).Select(p => p.MaBNhan).Count();
                        vvc = _lbnvv.Where(p => p.NgayVao >= 12 && p.NgayVao < 24).Select(p => p.MaBNhan).Count();
                        rep.VVSANG.Value = vvs;
                        rep.VVCHIEU.Value = vvc;
                    }
                    if(_lbnrv.Count()>0)
                    {
                        rvs = _lbnrv.Where(p => p.NgayRa >= 0 && p.NgayRa < 12).Select(p => p.MaBNhan).Count();
                        rvc = _lbnrv.Where(p => p.NgayRa >= 12 && p.NgayRa < 24).Select(p => p.MaBNhan).Count();
                        rep.RVSANG.Value = rvs;
                        rep.RVCHIEU.Value = rvc;
                    }
                    rep.TONG.Value = vvc + vvs + rvs + rvc;
                    rep.TIEUDE.Value = "BÁO CÁO TỔNG HỢP SỐ LIỆU VÀO/RA VIỆN";
                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.TENBV.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.NLB.Value = DungChung.Bien.NguoiLapBieu;
                    rep.NGAYTHANG.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            
           
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}