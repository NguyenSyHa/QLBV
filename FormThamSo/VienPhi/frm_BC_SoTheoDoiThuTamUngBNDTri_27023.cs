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
    public partial class frm_BC_SoTheoDoiThuTamUngBNDTri_27023 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_SoTheoDoiThuTamUngBNDTri_27023()
        {
            InitializeComponent();
        }

        private void frm_BC_SoTheoDoiThuTamUngBNDTri_27023_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            cbDoituong.SelectedIndex = 2;
            rgThanhToan.SelectedIndex = 2;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<BNDTNoiTru> _lBNDT = new List<BNDTNoiTru>();
        List<TienUng> _lTienUng = new List<TienUng>();
        List<MucTT> _listmuc = new List<MucTT>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            _lBNDT.Clear();
            _lTienUng.Clear();
            int Dthuong = cbDoituong.SelectedIndex;
            int ThanhToan = rgThanhToan.SelectedIndex;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            TienUng moi = new TienUng();
            var qtu = (from tu in data.TamUngs.Where(p => p.PhanLoai == 0)
                       group tu by new { tu.MaBNhan, tu.SoTien, tu.NgayThu } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           SotienTU = kq.Key.SoTien,
                           kq.Key.NgayThu
                       }).OrderBy(p => p.NgayThu).ToList();

            foreach (var item in qtu)
            {
                moi = new TienUng();
                moi.MaBNhan = item.MaBNhan ?? 0;
                moi.SoTien = item.SotienTU;
                moi.TongTien = qtu.Where(p => p.MaBNhan == item.MaBNhan).Sum(p => p.SotienTU);
                moi.NgayThu = item.NgayThu;
                if (_lTienUng.Count > 0)
                {
                    var lan = _lTienUng.Where(p => p.MaBNhan == item.MaBNhan).Select(p => p.SoLan).OrderByDescending(p => p.Value).ToList();
                    if (lan.Count > 0)
                        moi.SoLan = lan.First().Value + 1;
                    else
                        moi.SoLan = 1;
                }
                else
                {
                    moi.SoLan = 1;
                }
                _lTienUng.Add(moi);
            }

            var qvienphi = (from vp in data.VienPhis
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join bn in data.BenhNhans.Where(p => Dthuong == 2 ? p.DTuong == "BHYT" || p.DTuong == "Dịch vụ" : (Dthuong == 0 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                            group new { vp, vpct } by new { vp.MaBNhan, vp.NgayTT } into kq
                            select new { kq.Key.MaBNhan,kq.Key.NgayTT, ThanhToan = kq.Sum(p => p.vpct.TienBN)/*, ThuThem = kq.Where(p => p.tu.PhanLoai == 1).Sum(p => p.tu.TienChenh), ChiTra = kq.Where(p => p.tu.PhanLoai == 2).Sum(p => p.tu.TienChenh)*/ }).ToList();

            var qbn = (from tu in data.TamUngs
                       join bn in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => Dthuong == 2 ? p.DTuong == "BHYT" || p.DTuong == "Dịch vụ" : (Dthuong == 0 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")) on tu.MaBNhan equals bn.MaBNhan// thiếu  đối tượng
                       join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into kq
                       from kq1 in kq.DefaultIfEmpty()
                       //where (kq1 == null)
                       select new
                       {
                           bn.MucHuong,
                           bn.Tuyen,
                           bn.NoThe,
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.NoiTru,
                           bn.SThe,
                           DTBenhNhan = bn.DTuong.ToUpper().Contains("BHYT") ? "BẢO HIỂM Y TẾ" : "DỊCH VỤ",
                           bn.DChi,
                           tu.NgayThu,
                           
                           NgayVao = vv.NgayVao,
                           bv.MaBV,
                           bv.TuyenBV,
                           HangBV = bv.TuyenBV.Trim() == "A" ? 1 : (bv.TuyenBV.Trim() == "B" ? 2 : (bv.TuyenBV.Trim() == "C" ? 3 : (bv.TuyenBV.Trim() == "D" ? 4 : 0))),
                       }).ToList();
            var bc = (from bn in qbn
                      join tu in _lTienUng on bn.MaBNhan equals tu.MaBNhan//.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                      join vp in qvienphi on bn.MaBNhan equals vp.MaBNhan into qvp
                      from q1 in qvp.DefaultIfEmpty()
                      where (rgNgay.SelectedIndex == 0 ? (tu.NgayThu >= tungay && tu.NgayThu <= denngay) : (q1 != null && q1.NgayTT >= tungay && q1.NgayTT <= denngay))
                      where (ThanhToan == 2 ? true : (ThanhToan == 1 ? q1 != null : q1 == null))
                      group new { bn, tu } by new
                      {
                          bn.MaBNhan,
                          bn.NgayVao,
                          bn.TenBNhan,
                          bn.DChi,
                          bn.DTBenhNhan,
                          SoTien = q1 == null ? 0 : q1.ThanhToan,
                          //bn.HangBV,
                          //bn.Muc,
                          bn.SThe,
                          bn.NoiTru,
                          bn.NgayThu,
                          tu.TongTien,
                          Muc = MucHuong(bn.MucHuong ?? 0, bn.HangBV, bn.Tuyen ?? 0, bn.NoThe),
                          //ThuThem = q1 == null ? 0 : q1.ThuThem,
                          //ChiTra = q1 == null ? 0 : q1.ChiTra
                      } into kq
                      select new
                      {
                          kq.Key.MaBNhan,
                          kq.Key.NgayVao,
                          kq.Key.TenBNhan,
                          kq.Key.DChi,
                          kq.Key.DTBenhNhan,
                          kq.Key.Muc,
                          UngLan1 = kq.Where(p => p.tu.SoLan == 1).Sum(p => p.tu.SoTien),
                          UngLan2 = kq.Where(p => p.tu.SoLan == 2).Sum(p => p.tu.SoTien),
                          UngLan3 = kq.Where(p => p.tu.SoLan == 3).Sum(p => p.tu.SoTien),
                          UngLan4 = kq.Where(p => p.tu.SoLan == 4).Sum(p => p.tu.SoTien),
                          TongTien = kq.Key.TongTien,
                          kq.Key.SoTien
                      }).OrderBy(p => p.MaBNhan).ToList();
            var query = (from a in bc
                         group a by new
                         {
                             a.DTBenhNhan,
                             a.NgayVao,
                             a.Muc,
                             a.MaBNhan,
                             a.TenBNhan,
                             a.DChi,
                             a.UngLan1,
                             a.UngLan2,
                             a.UngLan3,
                             a.UngLan4,
                             a.TongTien,
                             a.SoTien,
                             //a.ChiTra,
                             //a.ThuThieu
                         } into kq
                         select new BNDTNoiTru
                         {
                             DTuong = kq.Key.DTBenhNhan,
                             NgayVao = kq.Key.NgayVao,
                             DT = kq.Key.Muc.ToString() + "%",
                             MaBNhan = kq.Key.MaBNhan,
                             HoTen = kq.Key.TenBNhan,
                             DiaChi = kq.Key.DChi,
                             TULan1 = kq.Key.UngLan1 == 0 ? null : kq.Key.UngLan1,
                             TULan2 = kq.Key.UngLan2 == 0 ? null : kq.Key.UngLan2,
                             TULan3 = kq.Key.UngLan3 == 0 ? null : kq.Key.UngLan3,
                             TULan4 = kq.Key.UngLan4 == 0 ? null : kq.Key.UngLan4,
                             TongTU = kq.Key.TongTien == 0 ? null : kq.Key.TongTien,
                             ThanhToan = kq.Key.SoTien == 0 ? "" : String.Format("{0:##,##0}", kq.Key.SoTien),
                             //ChiTra = kq.Key.ChiTra,
                             //ThuThieu = kq.Key.ThuThieu
                             ThuThieu = (kq.Key.SoTien > 0 && kq.Key.SoTien > kq.Key.TongTien) ? kq.Key.SoTien - kq.Key.TongTien : null,
                             ChiTra = (kq.Key.SoTien > 0 && kq.Key.SoTien < kq.Key.TongTien) ? kq.Key.TongTien - kq.Key.SoTien : null
                         }).ToList();

            BaoCao.Rep_BC_SoTheoDoiThuTamUngBNDTri_27023 rep1 = new BaoCao.Rep_BC_SoTheoDoiThuTamUngBNDTri_27023();
            frmIn frm = new frmIn();
            rep1.celThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep1.DataSource = query.OrderBy(p => p.NgayVao).ThenBy(p => p.HoTen).ToList();
            rep1.BindingData();
            rep1.CreateDocument();
            frm.prcIN.PrintingSystem = rep1.PrintingSystem;
            frm.ShowDialog();
        }
        #region function Tinh mức hưởng
        private string MucHuong(decimal muchuong, int hangbv, int traituyen, bool nothe)
        {
            string muc = "";
            string mh = muchuong.ToString();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qmuc = (from m in data.MucTTs.Where(p => p.MaMuc.Equals(mh)) select new { m.PTTT }).ToList();
            //var qbv = (from a in data.BenhViens.Where(p => p.MaBV.Equals(mabv)) select new { a.HangBV }).ToList();
            if (nothe)
                muc = "Chờ BH";
            else
            {
                if ((traituyen == 0 && muchuong == 0) || muchuong == 0)
                {
                    muc = "100";
                }
                else
                {
                    int muctt = qmuc.First().PTTT ?? 0;
                    //int hangbv = qbv.First().HangBV ?? 0;
                    if (traituyen == 2)
                    {
                        switch (hangbv)
                        {
                            case 1:
                                muc = (100 - (0.4 * muctt)).ToString();
                                break;
                            case 2:
                                muc = (100 - (0.6 * muctt)).ToString();
                                break;
                            case 3:
                                muc = (100 - (0.7 * muctt)).ToString();
                                break;
                            case 4:
                                muc = "0";
                                break;
                        }
                    }
                    if (traituyen == 1)
                    {
                        switch (hangbv)
                        {
                            case 1:
                                muc = (100 - muctt).ToString();
                                break;
                            case 2:
                                muc = (100 - muctt).ToString();
                                break;
                            case 3:
                                muc = (100 - muctt).ToString();
                                break;
                            case 4:
                                muc = "0";
                                break;
                        }
                    }
                }
            }
            return muc.ToString();
        }
        #endregion
        #region class TienUng
        private class TienUng
        {
            public int MaBNhan { get; set; }
            public DateTime? NgayThu { get; set; }
            public double? SoTien { get; set; }
            public double? TongTien { get; set; }
            public int? SoLan { get; set; }
        }
        #endregion
        #region class SoTheoDoiBNDT
        private class BNDTNoiTru
        {
            public DateTime? NgayVao { get; set; }
            public string DTuong { get; set; }
            public string DT { get; set; }
            public int MaBNhan { get; set; }
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public double? TULan1 { get; set; }
            public double? TULan2 { get; set; }
            public double? TULan3 { get; set; }
            public double? TULan4 { get; set; }
            public double? TongTU { get; set; }
            public string ThanhToan { get; set; }
            public double? ChiTra { get; set; }
            public double? ThuThieu { get; set; }
        }
        #endregion

        private void rgThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rgNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgNgay.SelectedIndex == 1)
            {
                rgThanhToan.SelectedIndex = 1;
                rgThanhToan.ReadOnly = true;
            }
            else
            {
                rgThanhToan.SelectedIndex = 2;
                rgThanhToan.ReadOnly = false;

            }
        }
    }
}