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
    public partial class frm_TongHopThuChi_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_TongHopThuChi_27022()
        {
            InitializeComponent();
        }

        private void frm_TongHopThuChi_27022_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DTBN> dtuong = data.DTBNs.Where(p => p.Status == 1).ToList();
            dtuong.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            LupDTuong.Properties.DisplayMember = "DTBN1";
            LupDTuong.Properties.ValueMember = "IDDTBN";
            LupDTuong.Properties.DataSource = dtuong;
            LupDTuong.EditValue = Convert.ToInt32(LupDTuong.Properties.GetKeyValueByDisplayText("Tất cả"));
        }
        List<MucTT> _listmuc = new List<MucTT>();

        public class BC151
        {
            public int? MaBNhan { get; set; }
            public DateTime? NgayDuyet { get; set; }
            public double? TyLeBHTT { get; set; }
            public double? TienBN { get; set; }
            public int? TrongBH { get; set; }
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon, 18000);
            _listmuc = data.MucTTs.ToList();
            int iddtuong = 99;
            if (LupDTuong.EditValue != null)
                iddtuong = Convert.ToInt32(LupDTuong.EditValue);
            int noitru = 2;
            if (radNoiTru.SelectedIndex == 0)
                noitru = 0;
            if (radNoiTru.SelectedIndex == 1)
                noitru = 1;
            if (radNoiTru.SelectedIndex == 2)
                noitru = 2;
            var qbn = (from tu in data.TamUngs.Where(p => rdThuChi.SelectedIndex == 0 ? p.PhanLoai == 1 : ((rdThuChi.SelectedIndex == 1 || rdThuChi.SelectedIndex == 3) ? p.PhanLoai == 2 : (p.PhanLoai == 0 || p.PhanLoai == 1)))
                       join bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru).Where(p => iddtuong == 99 || p.IDDTBN == iddtuong) on tu.MaBNhan equals bn.MaBNhan// thiếu  đối tượng
                       join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan into kq
                       from kq1 in kq.DefaultIfEmpty()
                           //group new { tu, bv, kq1 } by new { bn.MaBNhan, bn.SThe, bv.TuyenBV, bn.TenBNhan, bn.NoiTru, bn.Tuyen, bn.DChi, tu.NgayThu, bn.NNhap, SoBA = kq1 == null ? "" : kq1.SoBA, tu.QuyenHD, tu.PhanLoai } into kq
                       select new
                       {
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.NoiTru,
                           bn.SThe,
                           bn.Tuyen,
                           bv.TuyenBV,
                           bn.DChi,
                           tu.NgayThu,
                           tu.IDTamUng,
                           bn.NNhap,
                           SoBA = kq1 == null ? "" : kq1.SoBA,
                           tu.QuyenHD,
                           tu.SoHD,
                           SoTien = tu.SoTien,
                           TienChenh = tu.TienChenh,
                           ThuThem = tu.PhanLoai == 1 ? tu.TienChenh : 0,
                           ChiTra = tu.PhanLoai == 2 ? tu.TienChenh : 0
                       }).ToList();
            var qtu2 = (from tu in data.TamUngs.Where(p => p.PhanLoai == 0 || p.PhanLoai == 4)

                        select tu).ToList();
            var qtu = (from tu in qtu2
                       group tu by new { tu.MaBNhan, NgayThu = tu.NgayThu == null ? DateTime.Now.Date : tu.NgayThu.Value.Date } into kq
                       select new
                       {
                           Key = kq.Key.MaBNhan,
                           // IDTamUng = kq.Max(p=>p.IDTamUng),
                           SotienTU = (kq.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien)) ?? 0 - (kq.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien)) ?? 0,
                           NgayThu = kq.Key.NgayThu,
                           SoLan = kq.Count()
                       }).ToList();

            List<BC151> qvienphi = new List<BC151>();
            if (DungChung.Bien.MaBV != "27022")
            {
                qvienphi = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay)
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join bn in data.BenhNhans.Where(p => p.DTuong.Contains("BHYT") || p.DTuong.ToLower().Contains("dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                            where (bn.DTuong.Contains("BHYT") ? vpct.TrongBH == 1 : vpct.TrongBH == 0)
                            group new { vp, vpct } by new { vp.MaBNhan, vpct.TyLeBHTT, vp.NgayDuyet, vpct.TrongBH } into kq
                            select new BC151
                            {
                                MaBNhan = kq.Key.MaBNhan,
                                TyLeBHTT = kq.Key.TyLeBHTT,
                                NgayDuyet = kq.Key.NgayDuyet,
                                TienBN = kq.Sum(p => p.vpct.TienBN),
                                TrongBH = kq.Key.TrongBH,
                            }).ToList();
            }
            else
            {
                 qvienphi = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay)
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join bn in data.BenhNhans.Where(p => p.DTuong.Contains("BHYT") || p.DTuong.ToLower().Contains("dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                            where (bn.DTuong.Contains("BHYT") ? vpct.TrongBH == 1 : vpct.TrongBH == 0)
                            group new { vp, vpct } by new { vp.MaBNhan, vpct.TyLeBHTT, vp.NgayTT, vpct.TrongBH } into kq
                            select new BC151
                            {
                                MaBNhan = kq.Key.MaBNhan,
                                TyLeBHTT = kq.Key.TyLeBHTT,
                                NgayDuyet = kq.Key.NgayTT,
                                TienBN = kq.Sum(p => p.vpct.TienBN),
                                TrongBH = kq.Key.TrongBH,
                            }).ToList();
                qvienphi.ForEach(o => { o.NgayDuyet = o.NgayDuyet.Value.Date; });
            }

            var bc = (from bn in qbn
                      join vp in qvienphi on bn.MaBNhan equals vp.MaBNhan
                      join tu in qtu on bn.MaBNhan equals tu.Key //into kq
                      //from kq1 in kq.DefaultIfEmpty()
                      //where (kq1 != null)
                      select new
                      {
                          bn.QuyenHD,
                          SoHD = (DungChung.Bien.MaBV == "27023" && (rdThuChi.SelectedIndex == 2)) ? bn.IDTamUng.ToString() : bn.SoHD,
                          bn.SoBA,
                          bn.NNhap,
                          NgayThu = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "27022") ? vp.NgayDuyet : tu.NgayThu,
                          bn.TenBNhan,
                          bn.Tuyen,
                          bn.DChi,
                          bn.NoiTru,
                          bn.SThe,
                          SotienTU = tu.SotienTU,
                          bn.SoTien,
                          ThuThieu = bn.ThuThem,
                          SoLan = tu.SoLan,
                          bn.TienChenh,
                          vp.TienBN,
                          vp.TrongBH,

                          //HangBV = bn.TuyenBV.Trim() == "A" ? 1 : (bn.TuyenBV.Trim() == "B" ? 2 : (bn.TuyenBV.Trim() == "C" ? 3 : (bn.TuyenBV.Trim() == "D" ? 4 : 0))),
                          Muc = 100 - vp.TyLeBHTT
                      }).ToList();
            var bc1 = (from bn in bc
                       group bn by new
                       {

                           bn.QuyenHD,
                           bn.SoHD,
                           bn.SoBA,
                           bn.NNhap,
                           bn.NgayThu,
                           bn.TenBNhan,
                           bn.Tuyen,
                           bn.DChi,
                           bn.NoiTru,
                           bn.SThe,
                           bn.SoTien,
                           SoLan = bn.SoLan,
                           bn.ThuThieu,
                           bn.TienChenh,
                           bn.TrongBH,
                           bn.TienBN,
                           //Muc = 100 - DungChung.Ham._getmuc(_listmuc, bn.HangBV, bn.SThe, bn.Tuyen ?? 0, bn.NoiTru ?? 0, bn.NgayThu.Value)
                           Muc = bn.Muc
                       } into kq
                       select new
                       {
                           kq.Key.QuyenHD,
                           SoHD = (kq.Key.SoHD != "" ? Convert.ToInt32(kq.Key.SoHD) : 0),
                           kq.Key.SoBA,
                           kq.Key.NNhap,
                           kq.Key.NgayThu,
                           kq.Key.TenBNhan,
                           kq.Key.Tuyen,
                           kq.Key.DChi,
                           kq.Key.NoiTru,
                           kq.Key.TrongBH,
                           kq.Key.SThe,
                           SotienTU = kq.Sum(p => p.SotienTU),
                           SoTien = kq.Key.TrongBH == 1 ? kq.Key.TienBN : kq.Key.SoTien,
                           SoLan = kq.Key.SoLan,
                           kq.Key.ThuThieu,
                           kq.Key.TienChenh,
                           Muc = kq.Key.Muc
                       }).OrderBy(p => p.NgayThu).ThenBy(p => p.QuyenHD).ThenBy(p => p.SoHD).ThenBy(p => p.TenBNhan).ToList();

            double tongtien = bc1.Sum(p => p.TienChenh);
            frmIn frm = new frmIn();
            if (DungChung.Bien.MaBV == "27023")
            {
                BaoCao.rep_BaoCaoChiTraBN_27023 rep = new BaoCao.rep_BaoCaoChiTraBN_27023();
                rep.celNgayIn.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                rep.celSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, "Đồng");
                if (rdThuChi.SelectedIndex == 0)
                {
                    rep.celTit.Text = "TỔNG HỢP THANH TOÁN THU THÊM BỆNH NHÂN RA VIỆN";
                    rep.celtitchiTra.Text = "THU THÊM";
                    rep.DataSource = bc1.OrderBy(P => P.QuyenHD).ThenBy(p => p.SoHD).ThenBy(p => p.NgayThu).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (rdThuChi.SelectedIndex == 1)
                {
                    if (LupDTuong.Text != "Tất cả")
                    { rep.celTit.Text = "TỔNG HỢP THANH TOÁN CHI TRẢ BỆNH NHÂN " + LupDTuong.Text.ToUpper() + " RA VIỆN"; }
                    else
                    { rep.celTit.Text = "TỔNG HỢP THANH TOÁN CHI TRẢ BỆNH NHÂN RA VIỆN"; }
                    rep.DataSource = bc1.OrderBy(P => P.QuyenHD).ThenBy(p => p.SoHD).ThenBy(p => p.NgayThu).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                var dataBCs = (from b in bc1
                               group b by new
                               {
                                   b.QuyenHD,
                                   b.SoHD,
                                   b.SoBA,
                                   b.NNhap,
                                   b.NgayThu,
                                   b.TenBNhan,
                                   b.Tuyen,
                                   b.DChi,
                                   b.NoiTru,
                                   b.SThe,
                                   b.TrongBH,
                                   b.Muc
                               } into kq
                               select new
                               {
                                   kq.Key.QuyenHD,
                                   kq.Key.SoHD,
                                   kq.Key.SoBA,
                                   kq.Key.NNhap,
                                   kq.Key.NgayThu,
                                   kq.Key.TenBNhan,
                                   kq.Key.Tuyen,
                                   kq.Key.DChi,
                                   kq.Key.NoiTru,
                                   kq.Key.TrongBH,
                                   kq.Key.SThe,
                                   SotienTU = kq.Sum(p => p.SotienTU),
                                   SoTien = kq.Sum(p => p.SoTien),
                                   SoLan = kq.Sum(p => p.SoLan),
                                   ThuThieu = kq.Sum(p => p.ThuThieu),
                                   TienChenh = kq.Sum(p => p.TienChenh),
                                   kq.Key.Muc
                               }
                             ).OrderBy(p => p.NgayThu).ThenBy(p => p.QuyenHD).ThenBy(p => p.SoHD).ThenBy(p => p.TenBNhan).ToList();
                BaoCao.rep_BaoCaoChiTraBN_27022 rep = new BaoCao.rep_BaoCaoChiTraBN_27022();
                rep.celNgayIn.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                rep.celSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, "Đồng");
                if (rdThuChi.SelectedIndex == 0)
                {
                    rep.celTit.Text = "TỔNG HỢP THANH TOÁN THU THÊM BỆNH NHÂN RA VIỆN";
                    rep.celtitchiTra.Text = "THU THÊM";
                    rep.DataSource = (DungChung.Bien.MaBV != "27022" ? bc1 : dataBCs);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (rdThuChi.SelectedIndex == 1)
                {
                    if (LupDTuong.Text != "Tất cả")
                    { rep.celTit.Text = "TỔNG HỢP THANH TOÁN CHI TRẢ BỆNH NHÂN " + LupDTuong.Text.ToUpper() + " RA VIỆN"; }
                    else
                    { rep.celTit.Text = "TỔNG HỢP THANH TOÁN CHI TRẢ BỆNH NHÂN RA VIỆN"; }
                    rep.DataSource = (DungChung.Bien.MaBV != "27022" ? bc1 : dataBCs);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            if (rdThuChi.SelectedIndex == 2)//thu tạm ứng và phần thiếu viện phí(27023)
            {

                var bc3 = (from b in bc1.Where(p => p.NoiTru == 1).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                           group b by new { b.TenBNhan, b.DChi, b.SoLan, b.SotienTU, b.Muc, NgayThu = String.Format("{0:dd/MM}", b.NgayThu) } into kq
                           select new
                           {
                               SoHD = kq.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Max(p => p.SoHD),
                               kq.Key.NgayThu,
                               kq.Key.TenBNhan,
                               kq.Key.DChi,
                               SoLan = (kq.Sum(p => p.ThuThieu) > 0) ? "F thiếu" : kq.Key.SoLan.ToString(),
                               kq.Key.SotienTU,
                               ThuThieu = kq.Sum(p => p.ThuThieu),
                               kq.Key.Muc
                           }).Distinct().ToList();
                BaoCao.Rep_BC_ThuTamUng_27023 rep1 = new BaoCao.Rep_BC_ThuTamUng_27023();
                rep1.celNgay.Text = "Thu tạm ứng và phần thiếu viện phí: Tháng " + denngay.Month + " năm " + denngay.Year;
                if (DungChung.Bien.MaBV == "27023")
                    bc3 = bc3.OrderBy(p => p.SoHD).ThenBy(p => p.NgayThu).ToList();
                else
                    bc3 = bc3.OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();
                rep1.DataSource = bc3.ToList();
                rep1.BindingData();
                rep1.CreateDocument();
                frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm.ShowDialog();
            }
            if (rdThuChi.SelectedIndex == 3)//chi trả tiền ứng thừa(27023)
            {
                var bc4 = (from b in bc1.Where(p => p.NoiTru == 1)
                           group b by new { b.TenBNhan, b.DChi, b.Muc, NgayThu = String.Format("{0:dd/MM}", b.NgayThu) } into kq
                           select new
                           {
                               SoHD = kq.Max(p => p.SoHD),
                               kq.Key.NgayThu,
                               kq.Key.TenBNhan,
                               kq.Key.DChi,
                               TienChenh = kq.Sum(p => p.TienChenh),
                               kq.Key.Muc
                           }).Distinct().ToList();
                if (DungChung.Bien.MaBV == "27023")
                    bc4 = bc4.OrderBy(p => p.SoHD).ThenBy(p => p.NgayThu).ToList();
                else
                    bc4 = bc4.OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();
                BaoCao.Rep_BC_ChiTraTienUngThua_27023 rep1 = new BaoCao.Rep_BC_ChiTraTienUngThua_27023();
                rep1.celNgay.Text = "Chi trả tiền tạm ứng thừa: Tháng " + denngay.Month + " năm " + denngay.Year;
                rep1.DataSource = bc4;
                rep1.BindingData();
                rep1.CreateDocument();
                frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm.ShowDialog();
            }
            if (rdThuChi.SelectedIndex == 4)
            {
                DateTime _tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                DateTime _denngay = DungChung.Ham.NgayDen(lupTuNgay.DateTime);
                var _lkq = (from tu in data.TamUngs.Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay).Where(p => p.PhanLoai == 1 || p.PhanLoai == 0)
                            join bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru).Where(p => iddtuong == 99 || p.IDDTBN == iddtuong) on tu.MaBNhan equals bn.MaBNhan
                            select new
                            {
                                SoHD = tu.SoHD,
                                tu.QuyenHD,
                                tu.NgayThu,
                                bn.TenBNhan,
                                bn.DChi,
                                bn.MucHuong,
                                bn.MaBNhan,
                                tu.SoTien,
                                tu.TienChenh,
                                tu.PhanLoai
                            }).ToList();
                var ketqua = (from a in _lkq.Where(p => p.PhanLoai == 0 || (p.PhanLoai == 1 && p.TienChenh > 0))
                              select new
                              {
                                  Muc = 100 - DungChung.Ham._PtramTT(data, a.MucHuong.ToString()),
                                  a.SoHD,
                                  a.QuyenHD,
                                  NgayThu = String.Format("{0:dd/MM}", a.NgayThu),
                                  a.TenBNhan,
                                  SotienTU = a.PhanLoai == 0 ? a.SoTien : 0,
                                  ThuThieu = (a.PhanLoai == 1) ? a.TienChenh : 0,
                                  a.DChi,
                                  SoLan = a.PhanLoai == 1 ? "F thiếu" : "1"
                              }).ToList();
                if (DungChung.Bien.MaBV == "27023")
                    ketqua = ketqua.OrderBy(P => P.QuyenHD).ThenBy(p => p.SoHD).ThenBy(p => p.NgayThu).ToList();
                else
                    ketqua = ketqua.OrderBy(p => p.NgayThu).ThenBy(p => p.TenBNhan).ToList();
                BaoCao.Rep_BC_ThuTamUng_27023 rep1 = new BaoCao.Rep_BC_ThuTamUng_27023();
                rep1.celNgay.Text = "Thu tạm ứng và phần thiếu viện phí: Ngày " + _tungay.Day + " tháng " + _tungay.Month + " năm " + _tungay.Year;
                rep1.DataSource = ketqua.ToList();
                rep1.BindingData();
                rep1.CreateDocument();
                frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm.ShowDialog();
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}