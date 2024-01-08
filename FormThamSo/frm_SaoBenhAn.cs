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
    public partial class frm_SaoBenhAn : DevExpress.XtraEditors.XtraForm
    {
        public frm_SaoBenhAn()
        {
            InitializeComponent();
        }
        int _Mabn = 0;
        public frm_SaoBenhAn(int mabn)
        {
            InitializeComponent();
            _Mabn = mabn;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        class CanLamSang
        {
            public string MaCBth { get; set; }

            public int MaBNhan { get; set; }

            public int? STT { get; set; }

            public string ChanDoan { get; set; }

            public string GhiChu { get; set; }

            public int IdCLS { get; set; }

            public int IDBB { get; set; }

            public string MaCB { get; set; }

            public DateTime? NgayThang { get; set; }

            public bool Check { get; set; }

            public int? STTDV { get; set; }
            public int? MaDV { get; set; }
        }
        List<CanLamSang> _lchidinh = new List<CanLamSang>();
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            BaoCao.rep_SaoBenhAn rep = new BaoCao.rep_SaoBenhAn();
            var bn1 = (from a in _data.BenhNhans.Where(p => p.MaBNhan == (_Mabn))
                       join b in _data.TTboXungs on a.MaBNhan equals b.MaBNhan
                       join f in _data.DmNNs on b.MaNN equals f.MaNN into k
                       from k1 in k.DefaultIfEmpty()
                       select new
                       {
                           a.MaBNhan,
                           a.TenBNhan,
                           a.NamSinh,
                           a.DChi,
                           a.CDNoiGT,
                           k1.TenNN,
                           b.NoiLV,
                           a.NNhap
                       }).ToList();
            var vrv = (from c in _data.VaoViens.Where(p => p.MaBNhan == (_Mabn))
                       join d in _data.RaViens on c.MaBNhan equals d.MaBNhan
                       select new
                       {
                           c.MaBNhan,
                           c.SoVV,
                           d.SoLT,
                           c.NgayVao,
                           ChanDoanVV = c.ChanDoan,
                           ChanDoanRV = d.Status == 1 ? d.ChanDoanCV : d.ChanDoan,
                           d.NgayRa,
                           d.SoNgaydt,
                           c.TinhTrangVV,
                           d.TinhTrangRV,
                           d.DienBienDT,
                           d.XNCLS,
                           c.ChanDoan,
                           d.Status,
                           ChanDoan1 = d.ChanDoan,
                           d.ChanDoanCV
                       }).ToList();

            var bn = (from a in bn1
                      join b in vrv on a.MaBNhan equals b.MaBNhan
                      select new { 
                          a.MaBNhan, 
                          a.TenBNhan,
                          a.NamSinh, 
                          a.DChi, 
                          a.CDNoiGT,
                          a.TenNN,
                          a.NoiLV,
                          b.SoVV, 
                          b.SoLT, 
                          b.NgayVao,
                          ChanDoanVV = b.ChanDoan,
                          ChanDoanRV = b.Status == 1 ? b.ChanDoanCV : b.ChanDoan1,
                          b.NgayRa, 
                          b.SoNgaydt,
                          b.TinhTrangVV,
                          b.TinhTrangRV,
                          b.DienBienDT,
                          b.XNCLS
                      }).ToList();
            
            rep.HoTen.Value = bn.First().TenBNhan.ToUpper();
            rep.Tuoi.Value = bn.First().NamSinh;
            rep.SoVV.Value = bn.First().SoVV;
            rep.SoLT.Value = bn.First().SoLT;
            rep.DiaChi.Value = bn.First().DChi;
            rep.NgheNghiep.Value = bn.First().TenNN;
            rep.CoQuanCT.Value = bn.First().NoiLV;
            rep.NgayVaoVien.Value = bn.First().NgayVao;
            rep.NgayRaVien.Value = bn.First().NgayRa;
            rep.TongSoNDT.Value = bn.First().SoNgaydt;
            rep.CDTruoc.Value = bn.First().CDNoiGT;
            rep.CDVV.Value = bn.First().ChanDoanVV;
            rep.CVRV.Value = bn.First().ChanDoanRV;
            rep.TinhTrangVV.Value = bn.First().TinhTrangVV;
            rep.TinhTrangRV.Value = bn.First().TinhTrangRV;
            rep.DienBien.Value = bn.First().DienBienDT;
            rep.XNCLS.Value = bn.First().XNCLS;
            rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            //var xetnghiem = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
            //                 join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
            //                 join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
            //                 join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //                 join dv in _data.DichVus on dvct.MaDV equals dv.MaDV
            //                 join tn in _data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
            //                 select new { cd.MaCBth, dv.TenDV, dv.MaDV, clsct.KetQua, cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, dvct.TenDVct, TenDVctkq = dvct.TenDVct + ": " + clsct.KetQua + " " + dvct.DonVi, cls.IdCLS, kqcdha = dv.TenDV + ":" + cd.KetLuan }).OrderBy(p => p.IdCLS).ToList();
            //var chidinh = _lchidinh.Where(p => p.Check == true).ToList();
            //var ketqua = (from a in xetnghiem.Where(p => p.Status == 1 && (p.IDNhom == 1 || p.IDNhom == 2))
            //              join b in chidinh on a.MaDV equals b.MaDV
            //              where a.NgayThang == b.NgayThang
            //              group new { a } by new { a.NgayTH, a.MaCBth, a.TenDV, a.MaDV } into kq
            //              select new
            //              {
            //                  kq.Key.NgayTH,
            //                  kq.Key.MaCBth,
            //                  kq.Key.MaDV,
            //                  kq.Key.TenDV,
            //                  TenXN = "    Ngày " + kq.Key.NgayTH.Value.Day + "/" + kq.Key.NgayTH.Value.Month + "/" + kq.Key.NgayTH.Value.Year + ": " + string.Join("; ", kq.Select(p => p.a.TenDVctkq).ToArray()),
            //              }).OrderBy(p => p.MaDV).ToList();

            //rep.DataSource = ketqua.ToList();
            //rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void frm_SaoBenhAn_Load(object sender, EventArgs e)
        {
            var bn1 = (from a in _data.BenhNhans.Where(p => p.MaBNhan == (_Mabn))
                       join b in _data.TTboXungs on a.MaBNhan equals b.MaBNhan
                       join f in _data.DmNNs on b.MaNN equals f.MaNN into k
                       from k1 in k.DefaultIfEmpty()
                       select new
                       {
                           a.MaBNhan,
                           a.TenBNhan,
                           a.NamSinh,
                           a.DChi,
                           a.CDNoiGT,
                           k1.TenNN,
                           b.NoiLV,
                       }).ToList();
            var vrv = (from c in _data.VaoViens.Where(p => p.MaBNhan == (_Mabn))
                       join d in _data.RaViens on c.MaBNhan equals d.MaBNhan
                       select new
                       {
                           c.MaBNhan,
                           c.SoVV,
                           d.SoLT,
                           c.NgayVao,
                           ChanDoanVV = c.ChanDoan,
                           ChanDoanRV = d.Status == 1 ? d.ChanDoanCV : d.ChanDoan,
                           d.NgayRa,
                           d.SoNgaydt,
                           c.TinhTrangVV,
                           d.TinhTrangRV,
                           d.DienBienDT,
                           d.XNCLS,
                           c.ChanDoan,
                           d.Status,
                           ChanDoan1 = d.ChanDoan,
                           d.ChanDoanCV
                       }).ToList();

            var bn = (from a in bn1
                      join b in vrv on a.MaBNhan equals b.MaBNhan
                      select new
                      {
                          a.MaBNhan,
                          a.TenBNhan,
                          a.NamSinh,
                          a.DChi,
                          a.CDNoiGT,
                          a.TenNN,
                          a.NoiLV,
                          b.SoVV,
                          b.SoLT,
                          b.NgayVao,
                          ChanDoanVV = b.ChanDoan,
                          ChanDoanRV = b.Status == 1 ? b.ChanDoanCV : b.ChanDoan1,
                          b.NgayRa,
                          b.SoNgaydt,
                          b.TinhTrangVV,
                          b.TinhTrangRV,
                          b.DienBienDT,
                          b.XNCLS
                      }).ToList();

            HoTen.Text = bn.First().TenBNhan;
            Tuoi.Text = bn.First().NamSinh.ToString();
            DiaChi.Text = bn.First().DChi;
            NgheNghiep.Text = bn.First().TenNN;
            NoiLV.Text = bn.First().NoiLV;
            NgayVao.Text = bn.First().NgayVao.Value.Day + "/" + bn.First().NgayVao.Value.Month + "/" + bn.First().NgayVao.Value.Year;
            ngayra.Text = bn.First().NgayRa.Value.Day + "/" + bn.First().NgayRa.Value.Month + "/" + bn.First().NgayRa.Value.Year;
            SoNgayDT.Text = bn.First().SoNgaydt.ToString();
            memoTTVV.Text = bn.First().TinhTrangVV;
            memoTTRV.Text = bn.First().TinhTrangRV;
            memoDieuTri.Text = bn.First().DienBienDT;
            memoChiDinh.Text = bn.First().XNCLS;

            _lchidinh.Clear();
            var a1 = (from cl in _data.CLS.Where(p => p.MaBNhan == (_Mabn))
                     join chidinh in _data.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals chidinh.IdCLS
                     join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                     join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                     group new { tn, cl, dv } by new { cl.MaCBth, chidinh.IDCD, dv.TenDV, cl.STT, tn.TenRG, cl.NgayThang, cl.IdCLS, cl.MaCB, dv.SoTT, chidinh.MaDV } into kq
                     select new
                     {
                         kq.Key.MaCBth,
                         kq.Key.IDCD,
                         STT = kq.Key.STT,
                         ChanDoan = kq.Key.TenRG,
                         kq.Key.TenDV,
                         IdCLS = kq.Key.IdCLS,
                         IDBB = 0,
                         MaCB = kq.Key.MaCB,
                         NgayThang = kq.Key.NgayThang,
                         STTDV = kq.Key.SoTT,
                         kq.Key.MaDV
                     }).ToList();
            _lchidinh = (from cl in a1
                         select new CanLamSang
                         {
                             Check = true,
                             MaCBth = cl.MaCBth,
                             MaBNhan = cl.IDCD,
                             STT = cl.STT,
                             ChanDoan = cl.ChanDoan,
                             GhiChu = cl.TenDV,
                             IdCLS = cl.IdCLS,
                             IDBB = 0,
                             MaCB = cl.MaCB,
                             NgayThang = cl.NgayThang,
                             STTDV = cl.STTDV,
                             MaDV = cl.MaDV
                         }).ToList();
            GrcNhomDV.DataSource = _lchidinh.OrderBy(p => p.NgayThang).ThenBy(p => p.STTDV).ToList();
            if ((bn.First().TinhTrangVV != null && bn.First().TinhTrangVV != "") || (bn.First().TinhTrangRV != null && bn.First().TinhTrangRV != "") || (bn.First().DienBienDT != null && bn.First().DienBienDT != ""))
            {
                memoTTVV.Enabled = false;
                memoTTRV.Enabled = false;
                memoDieuTri.Enabled = false;
                memoChiDinh.Enabled = false;
                LayCD.Enabled = false;
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = true;
            }
            else
            {
                simpleButton2_Click(sender,e);
                simpleButton3.Enabled = true;
                KLuu.Enabled = true;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            memoTTVV.Enabled = true;
            memoTTRV.Enabled = true;
            memoDieuTri.Enabled = true;
            memoChiDinh.Enabled = true;
            LayCD.Enabled = true;
            simpleButton1.Enabled = false;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = true;
            KLuu.Enabled = true;
        }

        private void KLuu_Click(object sender, EventArgs e)
        {
            memoTTVV.Enabled = false;
            memoTTRV.Enabled = false;
            memoDieuTri.Enabled = false;
            memoChiDinh.Enabled = false;
            LayCD.Enabled = false;
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = true;
            simpleButton3.Enabled = false;
            KLuu.Enabled = false;
            frm_SaoBenhAn_Load(sender,e);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(memoTTVV.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tình trạng vào viện!");
                memoTTVV.Focus();
            }
            else if ( memoDieuTri.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập diễn biến trong điều trị!");
                memoDieuTri.Focus();
            }
            else if (memoChiDinh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập xét nghiệm, cân lâm sàng!");
                memoChiDinh.Focus();
            }
            else if (memoTTRV.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tình trạng ra viện!");
                memoTTRV.Focus();
            }
            else
            {
                memoTTVV.Enabled = false;
                memoTTRV.Enabled = false;
                memoDieuTri.Enabled = false;
                memoChiDinh.Enabled = false;
                LayCD.Enabled = false;
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = true;
                simpleButton3.Enabled = false;
                KLuu.Enabled = false;
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var vv = _data.VaoViens.Single(p => p.MaBNhan == _Mabn);
                var rv = _data.RaViens.Single(p => p.MaBNhan == _Mabn);
                vv.TinhTrangVV = memoTTVV.Text;
                rv.TinhTrangRV = memoTTRV.Text;
                rv.DienBienDT = memoDieuTri.Text;
                rv.XNCLS = memoChiDinh.Text;
                _data.SaveChanges();
                frm_SaoBenhAn_Load(sender, e);
                MessageBox.Show("Lưu thành công!");
            }
        }

        private void LayCD_Click(object sender, EventArgs e)
        {
            var xetnghiem = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                             join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                             join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                             join dv in _data.DichVus on dvct.MaDV equals dv.MaDV
                             join tn in _data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                             select new {tn.TenRG, cd.MaCBth, dv.TenDV, dv.MaDV, clsct.KetQua, cls.MaCB, cd.Status, cls.NgayTH, tn.IDNhom, cls.NgayThang, dvct.TenDVct, TenDVctkq = dvct.TenDVct + ": " + clsct.KetQua + " " + dvct.DonVi, cls.IdCLS, kqcdha = dv.TenDV + ":" + cd.KetLuan }).OrderBy(p => p.IdCLS).ToList();
            var chidinh = _lchidinh.Where(p => p.Check == true).ToList();
            var ketqua1 = (from a in xetnghiem.Where(p => p.Status == 1 && p.IDNhom == 1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                          join b in chidinh on a.MaDV equals b.MaDV
                          where a.NgayThang == b.NgayThang
                          group new { a } by new { a.NgayTH, a.MaCBth, a.TenRG } into kq
                          select new
                          {
                              kq.Key.NgayTH,
                              kq.Key.MaCBth,
                              kq.Key.TenRG,
                              TenXN = "    Ngày " + kq.Key.NgayTH.Value.Day + "/" + kq.Key.NgayTH.Value.Month + "/" + kq.Key.NgayTH.Value.Year + ": " + string.Join("; ", kq.Select(p => p.a.TenDVctkq).ToArray()),
                          }).OrderBy(p => p.TenRG).ToList();
            var ketqua = (from a in xetnghiem.Where(p => p.Status == 1 && (p.IDNhom == 1 || p.IDNhom == 2)).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                          join b in chidinh on a.MaDV equals b.MaDV
                          where a.NgayThang == b.NgayThang
                          group new { a } by new { a.NgayTH, a.MaCBth, a.TenDV, a.MaDV } into kq
                          select new
                          {
                              kq.Key.NgayTH,
                              kq.Key.MaCBth,
                              kq.Key.MaDV,
                              kq.Key.TenDV,
                              TenXN = "    Ngày " + kq.Key.NgayTH.Value.Day + "/" + kq.Key.NgayTH.Value.Month + "/" + kq.Key.NgayTH.Value.Year + ": " + string.Join("; ", kq.Select(p => p.a.TenDVctkq).ToArray()),
                          }).OrderBy(p => p.MaDV).ToList();
            string kq1 = memoChiDinh.Text;
            int xqq = 1;
            if (ketqua1.Count > 0)
            {
                kq1 = kq1 + xqq.ToString() + ". " + "Xét nghiệm SH máu:" + " \r\n";
                xqq++;
            }

            foreach (var item in ketqua1)
            {
                kq1 = kq1 + item.TenXN + " \r\n";
            }
            string xq = ketqua.Count > 0 ? ketqua.First().TenDV : "";
            if (ketqua.Count > 0)
            {
                kq1 = kq1 + xqq.ToString() + ". " + xq + ":" + " \r\n";
                xqq++;
            }
            foreach(var item in ketqua)
            {
                if (item.TenDV != xq)
                { 
                    kq1 = kq1 + xqq.ToString() + ". " + item.TenDV + ":" + " \r\n";
                    xqq++;
                }

                kq1 = kq1 + item.TenXN + " \r\n";
            }
            memoChiDinh.Text = kq1;
        }
    }
}