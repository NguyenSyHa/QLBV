using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading.Tasks;

namespace QLBV.FormThamSo
{
    public partial class frm_BaoCaoCLSCacKhoa_14017 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoCLSCacKhoa_14017()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao);

        }



        private class BaoCao
        {
            public string TenDichVu { get; set; }
            public double DonGia { get; set; }
            public int MaNhom { get; set; }
            public string TenNhom { get; set; }

            public double SoLuongKhoaNoiNhi { get; set; }
            public double SoTienKhoaNoiNhi { get; set; }

            public double SoLuongKhoaNgoai { get; set; }
            public double SoTienKhoaNgoai { get; set; }

            public double SoLuongKhoaCKX { get; set; }
            public double SoTienKhoaCKX { get; set; }

            public double SoLuongKhoaLaoKhoa { get; set; }
            public double SoTienKhoaLaoKhoa { get; set; }

            public double SoLuongKhoaPHCN { get; set; }
            public double SoTienKhoaPHCN { get; set; }

            public double SoLuongKhoaKBDaKhoa { get; set; }
            public double SoTienKhoaKBDaKhoa { get; set; }

            public double SoLuongKhoaChamCu { get; set; }
            public double SoTienKhoaChamCuu { get; set; }

            public double SoLuongToanTien { get; set; }
            public double SoTienToanVien { get; set; }

        }

        private void TaoBaoCao()
        {
            List<BaoCao> rep = new List<BaoCao>();
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            var q2 = (from cd in data.ChiDinhs
                      join cls in data.CLS.Where(p => p.NgayTH >= TuNgay && p.NgayTH <= DenNgay && p.Status == 1) on cd.IdCLS equals cls.IdCLS
                      join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                      join dv in data.DichVus.Where(p => p.IDNhom == 2 || p.IDNhom == 1 || p.IDNhom == 8) on cd.MaDV equals dv.MaDV
                      join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                      select new
                      {
                          MaDV = dv.MaDV,
                          TenDV = dv.TenDV,
                          SoLuong = 1,
                          Makp = kp.MaKP,
                          MaNhom = ndv.IDNhom,
                          ndv.TenNhom,
                          DonGia = cd.DonGia,
                      }).ToList();
            var q1 = (from q in q2
                      group q by new { MaDV = q.MaDV, q.TenDV, q.Makp, q.MaNhom, q.TenNhom, q.DonGia } into kq
                      select new
                      {
                          MaNhom = kq.Key.MaNhom,
                          MaDV = kq.Key.MaDV,
                          TenNhom = kq.Key.TenNhom,
                          TenDV = kq.Key.TenDV,
                          SoLuong = kq.Sum(p => p.SoLuong),
                          Makp = kq.Key.Makp,
                          DonGia = kq.Key.DonGia,
                      }).ToList();
            var dichvu = (from q in q1 select new { q.MaDV, q.TenDV, q.MaNhom, q.TenNhom, q.DonGia }).Distinct().ToList();

            foreach (var item in dichvu)
            {
                BaoCao bc = new BaoCao();
                bc.TenDichVu = item.TenDV;
                bc.DonGia = item.DonGia;
                bc.MaNhom = item.MaNhom == 8 ? 3 : item.MaNhom;
                bc.TenNhom = item.TenNhom.ToUpper();

                bc.SoLuongKhoaNoiNhi = q1.Where(p => p.Makp == 13 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaNoiNhi = bc.SoLuongKhoaNoiNhi * bc.DonGia;

                bc.SoLuongKhoaNgoai = q1.Where(p => p.Makp == 12 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaNgoai = bc.SoLuongKhoaNgoai * bc.DonGia;

                bc.SoLuongKhoaCKX = q1.Where(p => p.Makp == 17 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaCKX = bc.SoLuongKhoaCKX * bc.DonGia;

                bc.SoLuongKhoaLaoKhoa = q1.Where(p => p.Makp == 18 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaLaoKhoa = bc.SoLuongKhoaLaoKhoa * bc.DonGia;

                bc.SoLuongKhoaPHCN = q1.Where(p => p.Makp == 9 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaPHCN = bc.SoLuongKhoaPHCN * bc.DonGia;

                bc.SoLuongKhoaKBDaKhoa = q1.Where(p => p.Makp == 8 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaKBDaKhoa = bc.SoLuongKhoaKBDaKhoa * bc.DonGia;

                bc.SoLuongKhoaChamCu = q1.Where(p => p.Makp == 14 && p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienKhoaChamCuu = bc.SoLuongKhoaChamCu * bc.DonGia;

                bc.SoLuongToanTien = q1.Where(p => p.MaDV == item.MaDV && p.DonGia == item.DonGia).Sum(p => p.SoLuong);
                bc.SoTienToanVien = bc.SoLuongToanTien * bc.DonGia;

                rep.Add(bc);
            }
            if (rep.Count() > 0)
            {
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("Ngay", "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy"));
                DungChung.Ham.Print(DungChung.PrintConfig.rep_BCTongHopCLS_14017, rep, _dic, false);
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }





        }

        private void frm_BaoCaoCLSCacKhoa_14017_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = DateTime.Now;
            dtpDenNgay.DateTime = DateTime.Now;
        }


    }
}