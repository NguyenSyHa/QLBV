using DevExpress.XtraEditors;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV
{
    public partial class frm_BaoCaoTongHopSoLieu_14018 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoTongHopSoLieu_14018()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class repBC
        {
            public string TenDV { get; set; }

            public string KhoaYHCT { get; set; }
            public string KBDKCCHS { get; set; }
            public string KhoaNoiNhi { get; set; }
            public string KhoaNgoaiChinhHinh { get; set; }
            public string KhoaTuNguyen { get; set; }
            public string KhoaAnDuong { get; set; }
            public string Tong { get; set; }


        }
        private class SumrepBC
        {
            public string TenDV { get; set; }

            public double KhoaYHCT { get; set; }
            public double KBDKCCHS { get; set; }
            public double KhoaNoiNhi { get; set; }
            public double KhoaNgoaiChinhHinh { get; set; }
            public double KhoaTuNguyen { get; set; }
            public double KhoaAnDuong { get; set; }
            public double Tong { get; set; }


        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo báo cáo", "Xin đợi 1 lát");

        }
        private void TaoBaoCao()
        {
            List<repBC> rep = new List<repBC>();
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime NgayDen = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            var q2 = (from cd in data.ChiDinhs
                      join cls in data.CLS.Where(p => p.NgayTH >= TuNgay && p.NgayTH <= NgayDen && p.Status == 1) on cd.IdCLS equals cls.IdCLS
                      join kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") on cls.MaKP equals kp.MaKP
                      join dv in data.DichVus.Where(p => chkDichVuThucHien.Checked == true ? p.IS_EXECUTE_CLS == true : true && p.IDNhom == 8) on cd.MaDV equals dv.MaDV
                      join tndv in data.TieuNhomDVs.Where(p=>p.TenTN == "Điều trị vận động" || p.TenTN == "Điều trị vật lý" || p.TenTN == "Điều trị ngôn ngữ trị liệu" ||p.TenTN == "Điều trị y học cổ truyền") on dv.IdTieuNhom equals tndv.IdTieuNhom
                      select new
                      {
                          MaDV = dv.MaDV,
                          TenDV = dv.TenDV,
                          SoLuong = 1,
                          TenKP = kp.TenKP,
                      }).ToList();
            var q1 = (from q in q2
                      group q by new { MaDV = q.MaDV, q.TenDV, q.TenKP } into kq
                      select new
                      {
                          MaDV = kq.Key.MaDV,
                          TenDV = kq.Key.TenDV,
                          SoLuong = kq.Sum(p=>p.SoLuong),
                          TenKP = kq.Key.TenKP,
                      }).ToList();
            var dichvu = (from q in q1 select new { q.MaDV, q.TenDV }).Distinct().ToList();
            List<SumrepBC> _listSummarry = new List<SumrepBC>();
            foreach (var item in dichvu)
            {
                SumrepBC k = new SumrepBC();
                k.TenDV = item.TenDV;
                k.KhoaYHCT = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa Y học cổ truyền")).Sum(p => p.SoLuong);
                k.KBDKCCHS = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa khám bệnh đa khoa-CCHS")).Sum(p => p.SoLuong);
                k.KhoaAnDuong = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa An dưỡng")).Sum(p => p.SoLuong);
                k.KhoaNoiNhi = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa Nội-Nhi")).Sum(p => p.SoLuong);
                k.KhoaNgoaiChinhHinh = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa Ngoại-Chỉnh hình")).Sum(p => p.SoLuong);
                k.KhoaTuNguyen = q1.Where(p => p.MaDV == item.MaDV && p.TenKP.Contains("Khoa khám chuyên khoa PHCN ab")).Sum(p => p.SoLuong);
                k.Tong = q1.Where(p => p.MaDV == item.MaDV).Sum(p => p.SoLuong);
                _listSummarry.Add(k);
            }

            foreach (var item in _listSummarry)
            {
                repBC k = new repBC();
                k.TenDV = item.TenDV;
                k.KhoaYHCT = item.KhoaYHCT.ToString("#,##") ?? "";
                k.KBDKCCHS = item.KBDKCCHS.ToString("#,##") ?? "";
                k.KhoaAnDuong = item.KhoaAnDuong.ToString("#,##") ?? "";
                k.KhoaNoiNhi = item.KhoaNoiNhi.ToString("#,##") ?? "";
                k.KhoaNgoaiChinhHinh = item.KhoaNgoaiChinhHinh.ToString("#,##") ?? "";
                k.KhoaTuNguyen = item.KhoaTuNguyen.ToString("#,##") ?? "";
                k.Tong = item.Tong.ToString("#,##") ?? "";
                rep.Add(k);
            }

            Dictionary<string, object> m = new Dictionary<string, object>();

            m.Add("NgayThang", "Từ ngày: " + TuNgay.ToString("dd/MM/yyyy") + " - " + "Đến ngày: " + NgayDen.ToString("dd/MM/yyyy") + ".");
            m.Add("TongKhoaYHCT", _listSummarry.Sum(p => p.KhoaYHCT).ToString("#,##") ?? "");
            m.Add("TongKBDKCCHS", _listSummarry.Sum(p => p.KBDKCCHS).ToString("#,##") ?? "");
            m.Add("TongKhoaAnDuong", _listSummarry.Sum(p => p.KhoaAnDuong).ToString("#,##") ?? "");
            m.Add("TongKhoaNoiNhi", _listSummarry.Sum(p => p.KhoaNoiNhi).ToString("#,##") ?? "");
            m.Add("TongNgoaiChinhHinh", _listSummarry.Sum(p => p.KhoaNgoaiChinhHinh).ToString("#,##") ?? "");
            m.Add("TongKhoaTuNguyen", _listSummarry.Sum(p => p.KhoaTuNguyen).ToString("#,##") ?? "");
            m.Add("SumTong", _listSummarry.Sum(p => p.Tong).ToString("#,##") ?? "");
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCTongHopSoLieuBenhNhanThucHienCLSTheoKhoa, rep.ToList(), m, false);

        }

        private void frm_BaoCaoTongHopSoLieu_14018_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = dtpDenNgay.DateTime = DateTime.Now;

        }
        private class dichvu
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public bool Chon { get; set; }
        }
        public class khoaphong
        {
            public int Makp { get; set; }
            public string TenKP { get; set; }
            public bool Chon { get; set; }

        }



    }
}