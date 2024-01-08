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
    public partial class frm_CanhBaoThuocSLHDmin : DevExpress.XtraEditors.XtraForm
    {
        public frm_CanhBaoThuocSLHDmin()
        {
            InitializeComponent();

            dENgay.DateTime = System.DateTime.Now;
        }


        private void frm_CanhBaoThuocSLHDmin_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<frm_ThongKeBNHuySoHSBA.KhoaPhong> lstKho = new List<frm_ThongKeBNHuySoHSBA.KhoaPhong>();
            lstKho = db.KPhongs.Where(p => p.PLoai.Equals("Khoa dược")).Select(p => new frm_ThongKeBNHuySoHSBA.KhoaPhong { MaKP = p.MaKP, TenKP = p.TenKP }).ToList();
            frm_ThongKeBNHuySoHSBA.KhoaPhong allkp = new frm_ThongKeBNHuySoHSBA.KhoaPhong();
            allkp.MaKP = 0;
            allkp.TenKP = "Tất cả";
            lstKho.Insert(0, allkp);
            lupKho.Properties.DataSource = lstKho;
            lupKho.EditValue = 0;
            if (DungChung.Bien.MaBV == "01830")
            {
                checkEdit1.Checked = true;
            }
            else
                checkEdit1.Checked = false;
            if (DungChung.Bien.MaBV == "20001")
            {
                checkEdit2.Checked = true;
            }
            else
                checkEdit2.Checked = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            //QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            RepCanhBaoThuocSLHDmin(dENgay.Text, int.Parse(lupKho.EditValue.ToString()));
        }
        private void RepCanhBaoThuocSLHDmin(string ngay, int makho)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (checkEdit2.Checked == true)
            {
                BaoCao.rep_CanhBaoThuocSLHDmin_20001 rep = new BaoCao.rep_CanhBaoThuocSLHDmin_20001();
                rep.paramNgay.Value = GetDate(DateTime.Parse(ngay));
                DateTime ngay1 = DungChung.Ham.NgayDen(DateTime.Parse(ngay));
                DateTime ngaynull = new DateTime(1900,1,1);

                var lstTon11 = (from nd in db.NhapDs.Where(p => p.NgayNhap <= ngay1)
                                join ndct in db.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                where ((nd.PLoai == 1 || nd.PLoai == 2) && (nd.NgayNhap != null || ndct.HanDung != null) && nd.KieuDon != -1)
                                select new
                                {
                                    ndct.SoLo,
                                    ndct.MaDV,
                                    nd.NgayNhap,
                                    ndct.HanDung,
                                    makho = nd.MaKP,
                                    slnhap = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                    slxuat = nd.PLoai == 2 ? ndct.SoLuongX : 0,
                                    nd.PLoai
                                }).ToList();



                if (makho != 0)
                {
                    lstTon11 = lstTon11.Where(p => p.makho == makho).ToList();
                }

                var lstTon1 = (from ton in lstTon11
                               group ton by new { ton.MaDV, ton.SoLo } into kq
                               select new
                               {
                                   kq.Key.SoLo,
                                   kq.Key.MaDV,
                                   NgayNhap = kq.Where(p => p.PLoai == 1).Where(p => p.NgayNhap <= ngay1).Min(p => p.NgayNhap),
                                   HanDung = kq.Where(p => p.HanDung != null && p.HanDung != ngaynull).Min(p => p.HanDung),
                                   ton = kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slnhap) - kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slxuat)
                               }).ToList();

                var lstThuoc = (from dv in db.DichVus
                                where (dv.PLoai == 1)
                                select new
                                {
                                    dv.MaDV,
                                    dv.TenDV,
                                    dv.SLMin
                                }).ToList();
                DateTime hdmax = new DateTime();

                var _lstNgayThuoc = (from lngay in lstTon1.Where(p => p.ton > 0 && p.HanDung != null && (p.HanDung.Value - ngay1).Days >= 0)
                                     join thuoc in lstThuoc on lngay.MaDV equals thuoc.MaDV
                                     select new
                                     {
                                         madv = thuoc.MaDV,
                                         tenthuoc = thuoc.TenDV,
                                         ngaynhap = lngay.NgayNhap == null ? hdmax : lngay.NgayNhap.Value,
                                         handung = (lngay.HanDung == null || lngay.HanDung == ngaynull) ? hdmax : lngay.HanDung.Value,
                                         ngayht = ngay,
                                         slton = lngay.ton > 0 ? lngay.ton : 0,
                                         slmin = thuoc.SLMin == null ? 0 : thuoc.SLMin.Value,
                                         // baosl = lngay.ton==null?0:lngay.ton-thuoc.SLMin.Value,
                                         baohd = (lngay.HanDung == null || lngay.HanDung == ngaynull)? 0 : ((lngay.HanDung.Value - DateTime.Parse(ngay)).Days + 1),
                                         solo = lngay.SoLo
                                     }).Distinct().ToList();

                var lstNgayThuoc = (from ngaythuoc in _lstNgayThuoc
                                    select new
                                    {
                                        madv = ngaythuoc.madv,
                                        tenthuoc = ngaythuoc.tenthuoc,
                                        ngaynhap = ngaythuoc.ngaynhap == hdmax ? "" : ngaythuoc.ngaynhap.ToString("dd/MM/yyyy"),
                                        handung = ngaythuoc.handung == hdmax ? "" : ngaythuoc.handung.ToString("dd/MM/yyyy"),
                                        ngayht = ngaythuoc.ngayht,
                                        slton = ngaythuoc.slton,
                                        slmin = ngaythuoc.slmin,
                                        baosl = ngaythuoc.slton - ngaythuoc.slmin == 0 ? "" : (ngaythuoc.slton - ngaythuoc.slmin).ToString(),
                                        baohd = ngaythuoc.baohd == 0 ? 0 : ngaythuoc.baohd,
                                        solo = ngaythuoc.solo
                                    }).OrderBy(p => p.baohd).ToList();
                if (checkEdit1.Checked == true)
                {
                    lstNgayThuoc = (from ngaythuoc in _lstNgayThuoc
                                    select new
                                    {
                                        madv = ngaythuoc.madv,
                                        tenthuoc = ngaythuoc.tenthuoc,
                                        ngaynhap = ngaythuoc.ngaynhap == hdmax ? "" : ngaythuoc.ngaynhap.ToString("dd/MM/yyyy"),
                                        handung = ngaythuoc.handung == hdmax ? "" : ngaythuoc.handung.ToString("dd/MM/yyyy"),
                                        ngayht = ngaythuoc.ngayht,
                                        slton = ngaythuoc.slton,
                                        slmin = Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3)),
                                        baosl = ngaythuoc.slton - Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3)) == 0 ? "" : (ngaythuoc.slton - Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3))).ToString(),
                                        baohd = ngaythuoc.baohd == 0 ? 0 : ngaythuoc.baohd,
                                        solo = ngaythuoc.solo
                                    }).OrderBy(p => p.baohd).ToList();
                }
                int thang1 = 3650;
                if (check3.Checked == true)
                {
                    thang1 = 3 * 30;
                    
                }

                if (check6.Checked == true)
                {
                    thang1 = 6 * 30;
                    
                }

                if (check9.Checked == true)
                {
                    thang1 = 9 * 30;
                }

                if (check12.Checked == true)
                {
                    thang1 = 365;
                    
                }
                    
                lstNgayThuoc = lstNgayThuoc.Where(p => p.baohd <= thang1).ToList();
                frmIn frm = new frmIn();
                rep.DataSource = lstNgayThuoc.Where(p => p.slton > 0).ToList();
                rep.Bind();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_CanhBaoThuocSLHDmin rep = new BaoCao.rep_CanhBaoThuocSLHDmin();
                rep.paramNgay.Value = GetDate(DateTime.Parse(ngay));
                DateTime ngay1 = DungChung.Ham.NgayDen(DateTime.Parse(ngay));
                DateTime ngaynull = new DateTime(1900, 1, 1);
                var lstTon11 = (from nd in db.NhapDs.Where(p => p.NgayNhap <= ngay1)
                                join ndct in db.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                where ((nd.PLoai == 1 || nd.PLoai == 2) && (nd.NgayNhap != null || ndct.HanDung != null) && nd.KieuDon != -1)
                                select new
                                {
                                    ndct.MaDV,
                                    nd.NgayNhap,
                                    ndct.HanDung,
                                    makho = nd.MaKP,
                                    slnhap = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                    slxuat = nd.PLoai == 2 ? ndct.SoLuongX : 0,
                                    nd.PLoai
                                }).ToList();



                if (makho != 0)
                {
                    lstTon11 = lstTon11.Where(p => p.makho == makho).ToList();
                }

                var lstTon1 = (from ton in lstTon11
                               group ton by new { ton.MaDV } into kq
                               select new
                               {
                                   kq.Key.MaDV,
                                   NgayNhap = kq.Where(p => p.PLoai == 1).Where(p => p.NgayNhap <= ngay1).Min(p => p.NgayNhap),
                                   HanDung = kq.Where(p => p.HanDung != null && p.HanDung != ngaynull).Min(p => p.HanDung),
                                   ton = kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slnhap) - kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slxuat)
                               }).ToList();
                if (DungChung.Bien.MaBV == "27023")
                {
                    lstTon1 = (from ton in lstTon11
                               group ton by new { ton.MaDV } into kq
                               select new
                               {
                                   kq.Key.MaDV,
                                   NgayNhap = kq.Where(p => p.PLoai == 1).Where(p => p.HanDung == (kq.Where(p1 => p1.PLoai == 1).Where(p1 => p1.HanDung != null).Min(p1 => p1.HanDung))).Where(p => p.NgayNhap <= ngay1).Min(p => p.NgayNhap),
                                   HanDung = kq.Where(p => p.PLoai == 1).Where(p => p.HanDung != null && p.HanDung != ngaynull).Min(p => p.HanDung),
                                   ton = kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slnhap) - kq.Where(p => p.NgayNhap <= ngay1).Sum(p => p.slxuat)
                               }).ToList();
                }
                var lstThuoc = (from dv in db.DichVus
                                where (dv.PLoai == 1)
                                select new
                                {
                                    dv.MaDV,
                                    dv.TenDV,
                                    dv.SLMin
                                }).ToList();
                DateTime hdmax = new DateTime();

                var _lstNgayThuoc = (from lngay in lstTon1
                                     join thuoc in lstThuoc on lngay.MaDV equals thuoc.MaDV
                                     select new
                                     {
                                         madv = thuoc.MaDV,
                                         tenthuoc = thuoc.TenDV,
                                         ngaynhap = lngay.NgayNhap == null ? hdmax : lngay.NgayNhap.Value,
                                         handung = (lngay.HanDung == null || lngay.HanDung == ngaynull) ? hdmax : lngay.HanDung.Value,
                                         ngayht = ngay1,
                                         slton = lngay.ton,
                                         slmin = thuoc.SLMin == null ? 0 : thuoc.SLMin.Value,
                                         // baosl = lngay.ton==null?0:lngay.ton-thuoc.SLMin.Value,
                                         baohd = (lngay.HanDung == null || lngay.HanDung == ngaynull) ? 0 : (lngay.HanDung.Value - DateTime.Parse(ngay)).Days + 1
                                     }).Distinct().ToList();

                var lstNgayThuoc = (from ngaythuoc in _lstNgayThuoc
                                    select new
                                    {
                                        madv = ngaythuoc.madv,
                                        tenthuoc = ngaythuoc.tenthuoc,
                                        ngaynhap = ngaythuoc.ngaynhap == hdmax ? "" : ngaythuoc.ngaynhap.ToString("dd/MM/yyyy"),
                                        handung = ngaythuoc.handung == hdmax ? "" : ngaythuoc.handung.ToString("dd/MM/yyyy"),
                                        ngayht = ngaythuoc.ngayht,
                                        slton = ngaythuoc.slton,
                                        slmin = ngaythuoc.slmin,
                                        baosl = ngaythuoc.slton - ngaythuoc.slmin == 0 ? "" : (ngaythuoc.slton - ngaythuoc.slmin).ToString(),
                                        baohd = ngaythuoc.baohd == 0 ? 0 : ngaythuoc.baohd
                                    }).OrderBy(p => p.baohd).ToList();
                if (checkEdit1.Checked == true)
                {
                    lstNgayThuoc = (from ngaythuoc in _lstNgayThuoc
                                    select new
                                    {
                                        madv = ngaythuoc.madv,
                                        tenthuoc = ngaythuoc.tenthuoc,
                                        ngaynhap = ngaythuoc.ngaynhap == hdmax ? "" : ngaythuoc.ngaynhap.ToString("dd/MM/yyyy"),
                                        handung = ngaythuoc.handung == hdmax ? "" : ngaythuoc.handung.ToString("dd/MM/yyyy"),
                                        ngayht = ngaythuoc.ngayht,
                                        slton = ngaythuoc.slton,
                                        slmin = Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3)),
                                        baosl = ngaythuoc.slton - Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3)) == 0 ? "" : (ngaythuoc.slton - Convert.ToInt32(Math.Round(ngaythuoc.slton * 0.3))).ToString(),
                                        baohd = ngaythuoc.baohd == 0 ? 0 : ngaythuoc.baohd
                                    }).OrderBy(p => p.baohd).ToList();
                }
                int thang1 = 3650;
                if (check6.Checked == true)
                    thang1 = 6 * 30;
                if (check12.Checked == true)
                    thang1 = 365;
                lstNgayThuoc = lstNgayThuoc.Where(p => p.baohd <= thang1).ToList();
                frmIn frm = new frmIn();
                rep.DataSource = lstNgayThuoc.Where(p => p.slton > 0).ToList();
                rep.Bind();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private string GetDate(DateTime ngay)
        {
            DateTime date = DateTime.Parse(ngay.ToShortDateString());
            return "Ngày " + date.Day.ToString() + " tháng " + date.Month.ToString() + " năm " + date.Year.ToString();
        }

        private void check6_CheckedChanged(object sender, EventArgs e)
        {
            if (check6.Checked == true)
            {
                check3.Checked = false;
                check9.Checked = false;
                check12.Checked = false;
            }
                
        }

        private void check12_CheckedChanged(object sender, EventArgs e)
        {
            if (check12.Checked == true)
            {
                check6.Checked = false;
                check9.Checked = false;
                check3.Checked = false;
            }
                
        }

        private void check3_CheckedChanged(object sender, EventArgs e)
        {
            if (check3.Checked == true)
            {
                check6.Checked = false;
                check9.Checked = false;
                check12.Checked = false;
            }
        }

        private void check9_CheckedChanged(object sender, EventArgs e)
        {
            if (check9.Checked == true)
            {
                check6.Checked = false;
                check3.Checked = false;
                check12.Checked = false;
            }
        }
    }
}