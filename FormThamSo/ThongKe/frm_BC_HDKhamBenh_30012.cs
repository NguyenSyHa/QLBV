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
    public partial class frm_BC_HDKhamBenh_30012 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_HDKhamBenh_30012()
        {
            InitializeComponent();
        }

        private void frm_BC_HDKhamBenh_30009_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KB> _lKB = new List<KB>();

            var qBNKB = (from bn in data.BenhNhans
                         join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                         join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                         join ck in data.ChuyenKhoas on bnkb.MaCK equals ck.MaCK into kq
                         from kq1 in kq.DefaultIfEmpty()
                         select new
                         {
                             bn.MaBNhan,
                             bn.SThe,
                             bn.Tuoi,
                             bn.CapCuu,
                             bn.MaDTuong,
                             bnkb.NgayKham,
                             bnkb.MaKP,
                             bnkb.MaCK,
                             TenCK = kq1 == null ? "" : kq1.TenCK,
                             bnkb.PhuongAn,
                             bnkb.IDKB,
                             dtbn.DTBN1,
                             dtbn.HTTT
                         }).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
            #region Tính theo số lượt khám
            if (rdMau.SelectedIndex == 0)
            {
                _lKB = (from bn in qBNKB
                        join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                        where (DungChung.Bien.MaBV.Equals("30012") ? kp.PLoai.ToLower().Contains("phòng khám") : (kp.PLoai.ToLower().Contains("phòng khám") || kp.PLoai.ToLower().Contains("lâm sàng")))
                        group bn by new
                        {
                            bn.TenCK,
                            bn.MaCK,
                        } into kq
                        select new KB
                        {
                            ChuyenKhoa = kq.Key.TenCK,
                            TongSo = kq.Count(),
                            BHYT = kq.Where(p => p.SThe != null && p.SThe.Trim() != "").Count(),
                            VienPhi = kq.Where(p => p.SThe == null || p.SThe.Trim() == "").Count(),
                            KhongThuDuoc = kq.Count() - kq.Where(p => p.SThe != null && p.SThe.Trim() != "").Count() - kq.Where(p => p.SThe == null || p.SThe.Trim() == "").Count(),
                            CapCuu = kq.Where(p => p.CapCuu == 1).Count(),
                            Duoi1T = kq.Where(p => p.Tuoi < 1).Count(),
                            Duoi6T = kq.Where(p => p.Tuoi >= 1 && p.Tuoi < 6).Count(),
                            Duoi15T = kq.Where(p => p.Tuoi >= 6 && p.Tuoi <= 15).Count(),
                            Tren60 = kq.Where(p => p.Tuoi >= 60).Count(),
                            BNVaoVien = kq.Where(p => p.PhuongAn == 1).Count(),
                            BNChuyenVien = kq.Where(p => p.PhuongAn == 2).Count(),
                            DTKhacMP = kq.Where(p => p.HTTT == 0).Count(),
                            DTNgheo = kq.Where(p => p.MaDTuong == "HN").Count()
                        }).ToList();
            }
            #endregion
            #region Tính theo số BN đến khám
            if (rdMau.SelectedIndex == 1)
            {
                var luotBN = (from bn in qBNKB
                              group bn by new
                              {
                                  bn.TenCK,
                                  bn.MaCK,
                                  bn.MaBNhan,
                                  bn.SThe,
                                  bn.Tuoi,
                                  bn.MaDTuong,
                                  bn.CapCuu,
                                  bn.HTTT
                              } into kq
                              select new
                              {
                                  kq.Key.TenCK,
                                  kq.Key.MaCK,
                                  kq.Key.MaBNhan,
                                  kq.Key.SThe,
                                  kq.Key.Tuoi,
                                  kq.Key.MaDTuong,
                                  kq.Key.HTTT,
                                  kq.Key.CapCuu,
                                  SoVaoVien = kq.Where(p => p.PhuongAn == 1).Count(),
                                  SoCV = kq.Where(p => p.PhuongAn == 2).Count()
                              }).ToList();
                _lKB = (from n in luotBN
                        group n by new { n.TenCK, n.MaCK } into kq
                        select new KB
                        {
                            ChuyenKhoa = kq.Key.TenCK,
                            TongSo = kq.Count(),
                            BHYT = kq.Where(p => p.SThe != null && p.SThe.Trim() != "").Count(),
                            VienPhi = kq.Where(p => p.SThe == null || p.SThe.Trim() == "").Count(),
                            KhongThuDuoc = kq.Count() - kq.Where(p => p.SThe != null && p.SThe.Trim() != "").Count() - kq.Where(p => p.SThe == null || p.SThe.Trim() == "").Count(),
                            CapCuu = kq.Where(p => p.CapCuu == 1).Count(),
                            Duoi1T = kq.Where(p => p.Tuoi < 1).Count(),
                            Duoi6T = kq.Where(p => p.Tuoi >= 1 && p.Tuoi < 6).Count(),
                            Duoi15T = kq.Where(p => p.Tuoi >= 6 && p.Tuoi <= 15).Count(),
                            Tren60 = kq.Where(p => p.Tuoi >= 60).Count(),
                            BNVaoVien = kq.Sum(p => p.SoVaoVien),
                            BNChuyenVien = kq.Sum(p => p.SoCV),
                            DTKhacMP = kq.Where(p => p.HTTT == 0).Count(),
                            DTNgheo = kq.Where(p => p.MaDTuong == "HN").Count()
                        }).ToList();
            }
            #endregion
            #region  Xuat excel
            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "Stt", "Chuyên khoa", "Tổng số BN khám bệnh", "BN BHYT", "BN Viện phí", "BN Không thu được", "BN cấp cứu", "BN < 1T", "BN 1-6T", "BN 6-15T", "BN > 60T", "BN vào viện", "BN chuyển viện", "Khám miễn phí", "Người nghèo" };
            string _filePath = "C:\\" + "HoatDongKhamBenh_30009.xls";
            int[] _arrWidth = new int[] { };
            var qexcel = _lKB;
            DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 15];
            for (int i = 0; i < 15; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in qexcel)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.ChuyenKhoa;
                DungChung.Bien.MangHaiChieu[num, 2] = r.TongSo;
                DungChung.Bien.MangHaiChieu[num, 3] = r.BHYT;
                DungChung.Bien.MangHaiChieu[num, 4] = r.VienPhi;
                DungChung.Bien.MangHaiChieu[num, 5] = r.KhongThuDuoc;
                DungChung.Bien.MangHaiChieu[num, 6] = r.CapCuu;
                DungChung.Bien.MangHaiChieu[num, 7] = r.Duoi1T;
                DungChung.Bien.MangHaiChieu[num, 8] = r.Duoi6T;
                DungChung.Bien.MangHaiChieu[num, 9] = r.Duoi15T;
                DungChung.Bien.MangHaiChieu[num, 10] = r.Tren60;
                DungChung.Bien.MangHaiChieu[num, 11] = r.BNVaoVien;
                DungChung.Bien.MangHaiChieu[num, 12] = r.BNChuyenVien;
                DungChung.Bien.MangHaiChieu[num, 13] = r.DTKhacMP;
                DungChung.Bien.MangHaiChieu[num, 14] = r.DTNgheo;
                num++;
            }
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
            #endregion

            BaoCao.Rep_BC_HDKhamBenh_30012 rep = new BaoCao.Rep_BC_HDKhamBenh_30012();
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "HoatDongKhamBenh_30009", _filePath, true, this.Name);
            if (rdMau.SelectedIndex == 0)
                rep.lbl_ThoiGian.Text = "Từ ngày " + lupTuNgay.DateTime.Day + " tháng " + lupTuNgay.DateTime.Month + " năm " + lupTuNgay.DateTime.Year +
                                    " đến ngày " + lupDenNgay.DateTime.Day + " tháng " + lupDenNgay.DateTime.Month + " năm " + lupDenNgay.DateTime.Year + " - BC theo số lần khám";
            else
                rep.lbl_ThoiGian.Text = "Từ ngày " + lupTuNgay.DateTime.Day + " tháng " + lupTuNgay.DateTime.Month + " năm " + lupTuNgay.DateTime.Year +
                                    " đến ngày " + lupDenNgay.DateTime.Day + " tháng " + lupDenNgay.DateTime.Month + " năm " + lupDenNgay.DateTime.Year + " - BC theo số lượt BN đến khám";
            rep.DataSource = _lKB.OrderBy(p => p.ChuyenKhoa).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        #region class KB
        public class KB
        {
            public string ChuyenKhoa { get; set; }
            public int? TongSo { get; set; }// tổng số
            public int? BHYT { get; set; }
            public int? VienPhi { get; set; }
            public int? KhongThuDuoc { get; set; }
            public int? Duoi1T { get; set; }
            public int? Duoi6T { get; set; }
            public int? Duoi15T { get; set; }
            public int? Tren60 { get; set; }
            public int? CapCuu { get; set; }
            public int? BNVaoVien { get; set; }// vào viện
            public int? BNChuyenVien { get; set; }//chuyển viện
            public int? DTKhacMP { get; set; }//tổng số người đtrị ngoại trú
            public int? DTNgheo { get; set; }
        }
        #endregion
    }
}