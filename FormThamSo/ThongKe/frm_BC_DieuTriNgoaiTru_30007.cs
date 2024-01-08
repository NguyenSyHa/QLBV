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
    public partial class frm_BC_DieuTriNgoaiTru_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_DieuTriNgoaiTru_30007()
        {
            InitializeComponent();
        }

        private void frm_DTNgoaiTru_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string pageHeader = string.Empty;
            string reportHeader = string.Empty;
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (denngay >= tungay)
            {
                var benhnhan = (from vv in data.VaoViens
                                join bn in data.BenhNhans on vv.MaBNhan equals bn.MaBNhan
                                join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                from kq1 in kq.DefaultIfEmpty()
                                select new
                                {
                                    vv.NgayVao,
                                    bn.MaBNhan,
                                    bn.Tuoi,
                                    bn.IDDTBN,
                                    kq1.NgayRa,
                                    bn.NoiTru,
                                    bn.DTNT,
                                    MaKP = (kq1 != null && kq1.NgayRa >= tungay && kq1.NgayRa <= denngay) ? kq1.MaKP : vv.MaKP,
                                    kq1.KetQua,
                                    kq1.SoNgaydt
                                }).Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay) || (p.NgayVao <= tungay && (p.NgayRa == null || p.NgayRa >= denngay || (p.NgayRa >= tungay && p.NgayRa <= denngay))) || (p.NgayVao >= tungay && p.NgayVao <= denngay && p.NgayRa >= denngay)).ToList();

                var query = (from n in benhnhan
                             join kp in data.KPhongs on n.MaKP equals kp.MaKP
                             join dtbn in data.DTBNs on n.IDDTBN equals dtbn.IDDTBN
                             select new
                             {
                                 kp.TenKP,
                                 dtbn.DTBN1,
                                 n.Tuoi,
                                 n.MaBNhan,
                                 n.IDDTBN,
                                 n.MaKP,
                                 n.KetQua,
                                 n.SoNgaydt
                             }).ToList();

                if (rad_MauBC.SelectedIndex == 0)
                {
                    reportHeader = "TỔNG HỢP SỐ LIỆU ĐIỀU TRỊ NGOẠI TRÚ";
                    pageHeader = "Điều trị ngoại trú";

                    var result = (from n in query
                                  group n by new { n.TenKP } into kq
                                  select new
                                  {
                                      TenKhoa = kq.Key.TenKP,
                                      TongMacBenh = kq.Count(),
                                      TongTuVong = kq.Where(p => p.KetQua == "Tử vong").Count(),
                                      TongNgayDT = kq.Sum(p => p.SoNgaydt),
                                      TongTEMacBenh = kq.Where(p => p.Tuoi.Value < 15).Count(),
                                      TongTEMacBenh_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Count(),
                                      TV_TongTE = kq.Where(p => p.Tuoi.Value < 15).Where(p => p.KetQua == "Tử vong").Count(),
                                      TV_TongTE_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Where(p => p.KetQua == "Tử vong").Count(),
                                      TE_DieuTri_T = kq.Where(p => p.Tuoi.Value < 15).Sum(p => p.SoNgaydt),
                                      TE_0_6_DT_T = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Sum(p => p.SoNgaydt),
                                      TongNguoiCTMacBenh = kq.Where(p => p.Tuoi.Value > 60).Count(),
                                      CaoTuoi_NgayDT = kq.Where(p => p.Tuoi.Value > 60).Sum(p => p.SoNgaydt)
                                  }).ToList();

                    BaoCao.Rep_BC_DieuTriNgoaiTru_30007 rep = new BaoCao.Rep_BC_DieuTriNgoaiTru_30007();
                    frmIn frm = new frmIn();
                    rep.DataSource = result;
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy");
                    rep.rep_Header.Text = reportHeader;
                    rep.page_Header.Text = pageHeader;
                    rep.Bindingdata();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (rad_MauBC.SelectedIndex == 1)
                {
                    reportHeader = "TỔNG HỢP SỐ LIỆU ĐIỀU TRỊ NGOẠI TRÚ (BHYT)";
                    pageHeader = "Điều trị ngoại trú (BHYT)";

                    var result = (from n in query.Where(p => p.DTBN1 == "BHYT")
                                  group n by new { n.TenKP } into kq
                                  select new
                                  {
                                      TenKhoa = kq.Key.TenKP,
                                      TongMacBenh = kq.Count(),
                                      TongTuVong = kq.Where(p => p.KetQua == "Tử vong").Count(),
                                      TongNgayDT = kq.Sum(p => p.SoNgaydt),
                                      TongTEMacBenh = kq.Where(p => p.Tuoi.Value < 15).Count(),
                                      TongTEMacBenh_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Count(),
                                      TV_TongTE = kq.Where(p => p.Tuoi.Value < 15).Where(p => p.KetQua == "Tử vong").Count(),
                                      TV_TongTE_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Where(p => p.KetQua == "Tử vong").Count(),
                                      TE_DieuTri_T = kq.Where(p => p.Tuoi.Value < 15).Sum(p => p.SoNgaydt),
                                      TE_0_6_DT_T = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Sum(p => p.SoNgaydt),
                                      TongNguoiCTMacBenh = kq.Where(p => p.Tuoi.Value > 60).Count(),
                                      CaoTuoi_NgayDT = kq.Where(p => p.Tuoi.Value > 60).Sum(p => p.SoNgaydt)
                                  }).ToList();

                    BaoCao.Rep_BC_DieuTriNgoaiTru_30007 rep = new BaoCao.Rep_BC_DieuTriNgoaiTru_30007();
                    frmIn frm = new frmIn();
                    rep.DataSource = result;
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy");
                    rep.rep_Header.Text = reportHeader;
                    rep.page_Header.Text = pageHeader;
                    rep.Bindingdata();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (rad_MauBC.SelectedIndex == 2)
                {
                    reportHeader = "TỔNG HỢP SỐ LIỆU ĐIỀU TRỊ NGOẠI TRÚ (VIỆN PHÍ)";
                    pageHeader = "Điều trị ngoại trú (Viện phí)";

                    var result = (from n in query.Where(p => p.DTBN1 != "BHYT")
                                  group n by new { n.TenKP } into kq
                                  select new
                                  {
                                      TenKhoa = kq.Key.TenKP,
                                      TongMacBenh = kq.Count(),
                                      TongTuVong = kq.Where(p => p.KetQua == "Tử vong").Count(),
                                      TongNgayDT = kq.Sum(p => p.SoNgaydt),
                                      TongTEMacBenh = kq.Where(p => p.Tuoi.Value < 15).Count(),
                                      TongTEMacBenh_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Count(),
                                      TV_TongTE = kq.Where(p => p.Tuoi.Value < 15).Where(p => p.KetQua == "Tử vong").Count(),
                                      TV_TongTE_0_6 = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Where(p => p.KetQua == "Tử vong").Count(),
                                      TE_DieuTri_T = kq.Where(p => p.Tuoi.Value < 15).Sum(p => p.SoNgaydt),
                                      TE_0_6_DT_T = kq.Where(p => p.Tuoi == null || p.Tuoi.Value <= 6).Sum(p => p.SoNgaydt),
                                      TongNguoiCTMacBenh = kq.Where(p => p.Tuoi.Value > 60).Count(),
                                      CaoTuoi_NgayDT = kq.Where(p => p.Tuoi.Value > 60).Sum(p => p.SoNgaydt)
                                  }).ToList();

                    BaoCao.Rep_BC_DieuTriNgoaiTru_30007 rep = new BaoCao.Rep_BC_DieuTriNgoaiTru_30007();
                    frmIn frm = new frmIn();
                    rep.DataSource = result;
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy");
                    rep.rep_Header.Text = reportHeader;
                    rep.page_Header.Text = pageHeader;
                    rep.Bindingdata();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Đến ngày phải lớn hơn từ ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}