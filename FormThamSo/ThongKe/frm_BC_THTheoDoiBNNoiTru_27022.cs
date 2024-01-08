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
    public partial class frm_BC_THTheoDoiBNNoiTru_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_THTheoDoiBNNoiTru_27022()
        {
            InitializeComponent();
        }
        #region class Khoa
        private class Khoa
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        #region class BNNoiTru
        private class BNNoiTru
        {
            public string NgayNhap { get; set; }
            public string Khoa { get; set; }
            public string MaBA { get; set; }
            public string HoTen { get; set; }
            public string NgayVV { get; set; }
            public string DiaChi { get; set; }
            public string NgayCV { get; set; }
            public string Tuyen { get; set; }
            public string MST { get; set; }
            public string TenBenh { get; set; }
            public string MaBenh { get; set; }
            public double? TienUng { get; set; }
            public string NgayRV { get; set; }
            public double? TongTien { get; set; }
            public double? TienBN { get; set; }
            public double? TTNgoai { get; set; }
            public double? TienBH { get; set; }
            public string NgayTT { get; set; }
            public double? TienThua { get; set; }
            public double? ThuThem { get; set; }
            public int? MaDV { get; set; }
            public string LoaiTTT { get; set; }
        }
        #endregion

        List<Khoa> _lKhoa = new List<Khoa>();
        private void frm_BC_THTheoDoiBNNoiTru_27022_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            var dsKhoa = (from kp in data.KPhongs.Where(p => p.PLoai.Equals("Lâm sàng") && p.Status == 1) select new { kp.MaKP, kp.TenKP }).OrderBy(p => p.TenKP).ToList();
            if (dsKhoa.Count > 0)
            {
                Khoa moi = new Khoa();
                moi.Chon = true;
                moi.MaKP = 0;
                moi.TenKP = "Chọn tất cả";
                _lKhoa.Add(moi);
                foreach (var item in dsKhoa)
                {
                    Khoa moi1 = new Khoa();
                    moi1.Chon = true;
                    moi1.MaKP = item.MaKP;
                    moi1.TenKP = item.TenKP;
                    _lKhoa.Add(moi1);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoa.First().Chon == true)
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<BNNoiTru> _lBN = new List<BNNoiTru>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            //0: tạm ứng, 1: thu, 2: chi, 3: thu trực tiếp, 4 chi tạm thu
            //tuyến: 1-Đúng tuyến, 2-Trái tuyến
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            List<Khoa> dsKhoa = new List<Khoa>();
            dsKhoa = _lKhoa.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            var qdv = (from dv in data.DichVus.Where(p => p.TenDV.ToLower().Contains("thủy tinh thể") && p.PLoai == 1)
                       join dtct in data.DThuoccts on dv.MaDV equals dtct.MaDV
                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                       group new { dv, dt, dtct } by new { dv.MaDV, dv.TenDV, dt.MaBNhan } into q
                       select new { q.Key.MaDV, q.Key.TenDV, q.Key.MaBNhan }).ToList();
            //var qtamung = (from tu in data.TamUngs
            //               select new
            //               {
            //                   tu.MaBNhan,
            //                   tu.IDTamUng,
            //                   TienUng = tu.SoTien + ((tu.PhanLoai == 4) ? tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0)) - ((tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.TienChenh : 0),
            //                   TienThua = (tu.PhanLoai == 4) ? tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0),
            //                   ThuThem = (tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.TienChenh : 0
            //               }).ToList();
            var qvp = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay)
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       join t in data.TamUngs on vpct.IDTamUng equals t.IDTamUng into qtamung
                       from qtamung1 in qtamung.DefaultIfEmpty()
                       group new { vp, vpct } by new { vp.MaBNhan, vp.NgayTT, vpct.IDTamUng, qtamung1.PhanLoai, qtamung1.TienChenh, qtamung1.SoTien } into k
                       select new
                       {
                           k.Key.MaBNhan,
                           k.Key.NgayTT,
                           k.Key.IDTamUng,
                           ThanhTien = k.Sum(p => p.vpct.ThanhTien),
                           TienBH = k.Sum(p => p.vpct.TienBH),
                           TienBN = k.Where(p => p.vpct.TrongBH == 1).Sum(p => (double?)p.vpct.TienBN) ?? 0,
                           TTNgoai = k.Where(p => p.vpct.TrongBH == 0).Sum(p => (double?)p.vpct.TienBN) ?? 0,
                           ChiTienThua = (k.Key.PhanLoai != null) ? ((k.Key.PhanLoai == 4 ? k.Key.SoTien : (k.Key.PhanLoai == 2 ? k.Key.TienChenh : 0))) : 0,
                           ThuThem = (k.Key.PhanLoai != null) ? ((k.Key.PhanLoai == 1 || k.Key.PhanLoai == 3) ? k.Key.TienChenh : 0) : 0,
                           TienUng = k.Key.SoTien + ((k.Key.PhanLoai != null) ? ((k.Key.PhanLoai == 4 ? k.Key.SoTien : (k.Key.PhanLoai == 2 ? k.Key.TienChenh : 0))) : 0) -
                                     ((k.Key.PhanLoai != null) ? ((k.Key.PhanLoai == 1 || k.Key.PhanLoai == 3) ? k.Key.TienChenh : 0) : 0),
                       }).ToList();
            var qbn = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                       select new
                       {
                           bn.MaBNhan,
                           bn.SThe,
                           bn.NNhap,
                           bn.TenBNhan,
                           bn.DChi,
                           Tuyen = (bn.CapCuu == 1) ? "Cấp cứu" : (bn.Tuyen == 1 ? "Đ.Tuyến" : "T.Tuyến"),
                           rv.NgayVao,
                           rv.MaKP,
                           vv.SoBA,
                           NgayCV = (rv.Status == 1) ? rv.NgayRa : null,
                           NgayRa = (rv.Status == 2) ? rv.NgayRa : null,
                           TenBenh = rv.ChanDoan.Contains(";") == false ? rv.ChanDoan : rv.ChanDoan.Substring(0, rv.ChanDoan.IndexOf(";")),
                           MaBenh = rv.MaICD.Contains(";") == false ? rv.MaICD : rv.MaICD.Substring(0, rv.MaICD.IndexOf(";")),
                       }).ToList();
            var kq = (from a in qvp
                      join b in qbn on a.MaBNhan equals b.MaBNhan
                      join c in dsKhoa on b.MaKP equals c.MaKP
                      join d in qdv on a.MaBNhan equals d.MaBNhan into k
                      from k1 in k.DefaultIfEmpty()
                      group new { a, b, c, k1 } by new
                      {
                          a.MaBNhan,
                          a.NgayTT,
                          b.NNhap,
                          b.TenBNhan,
                          b.SThe,
                          b.DChi,
                          b.Tuyen,
                          b.NgayVao,
                          b.MaKP,
                          b.NgayCV,
                          b.NgayRa,
                          b.TenBenh,
                          b.MaBenh,
                          b.SoBA,
                          c.TenKP,
                          TenDV = k1 == null ? "" : k1.TenDV
                      } into k
                      select new
                      {
                          k.Key.MaBNhan,
                          k.Key.NgayTT,
                          ThanhTien = k.Sum(p => p.a.ThanhTien),
                          TienBH = k.Sum(p => p.a.TienBH),
                          TienBN = k.Sum(p => p.a.TienBN),
                          TTNgoai = k.Sum(p => p.a.TTNgoai),
                          k.Key.NNhap,
                          k.Key.TenBNhan,
                          k.Key.SThe,
                          k.Key.DChi,
                          k.Key.Tuyen,
                          k.Key.NgayVao,
                          k.Key.MaKP,
                          k.Key.NgayCV,
                          k.Key.NgayRa,
                          k.Key.TenBenh,
                          k.Key.MaBenh,
                          k.Key.SoBA,
                          TienUng = k.Sum(p => p.a.TienUng),
                          ChiTienThua = k.Sum(p => p.a.ChiTienThua),
                          ThuThem = k.Sum(p => p.a.ThuThem),
                          k.Key.TenKP,
                          k.Key.TenDV
                      }).ToList();

            var query = (from n in kq
                         select new
                         {
                             n.MaBNhan,
                             NgayTT = n.NgayTT != null ? String.Format("{0:dd/MM}", n.NgayTT) : "",
                             n.ThanhTien,
                             n.TienBH,
                             n.TienBN,
                             n.TTNgoai,
                             NNhap = n.NNhap != null ? String.Format("{0:dd/MM}", n.NNhap) : "",
                             n.TenBNhan,
                             n.SThe,
                             n.DChi,
                             n.Tuyen,
                             NgayVao = n.NgayVao != null ? String.Format("{0:dd/MM}", n.NgayVao) : "",
                             n.MaKP,
                             NgayCV = n.NgayCV != null ? String.Format("{0:dd/MM}", n.NgayCV) : "",
                             NgayRa = n.NgayRa != null ? String.Format("{0:dd/MM}", n.NgayRa) : "",
                             n.TenBenh,
                             n.MaBenh,
                             n.TienUng,
                             n.ChiTienThua,
                             n.ThuThem,
                             n.TenKP,
                             n.SoBA,
                             n.TenDV
                         }).ToList();
            BaoCao.Rep_BC_THTheoDoiBNNoiTru_27022 rep = new BaoCao.Rep_BC_THTheoDoiBNNoiTru_27022();
            frmIn frm = new frmIn();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = query.OrderBy(p => p.TenBNhan).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}