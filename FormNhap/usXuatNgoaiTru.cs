using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class usXuatNgoaiTru : DevExpress.XtraEditors.XtraUserControl
    {
        public usXuatNgoaiTru()
        {
            InitializeComponent();
        }

        private QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public bool _Xoact = false;
        private List<DThuocct> _lDThuocct = new List<DThuocct>();
        private DateTime _ngayke = System.DateTime.Now;
        private DateTime _dttu = System.DateTime.Now;
        private DateTime _dtden = System.DateTime.Now;
        private List<BenhNhan> _lTKbn = new List<BenhNhan>();
        private List<datasoureBN> dt = new List<datasoureBN>();
        private int _makp = DungChung.Bien.MaKP;// makp tìm kiếm
        private int _maKhoXuat = 0;
        private int idmabn = 0;

        private class datasoureBN
        {
            public int idtu { get; set; }
            public string MaCB { get; set; }
            public int IDNhap { get; set; }
            public int? SttBN { get; set; }
            public int TrongDM { get; set; }
            public bool DonTra { get; set; }
            public int KieuDon { set; get; }
            public int Status { set; get; }
            public bool Chon { get; set; }
            private int iDDTBN;

            public int IDDTBN
            {
                get { return iDDTBN; }
                set { iDDTBN = value; }
            }

            private int maBNhan;

            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }

            private string tenBNhan;

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }

            private int iDDon;

            public int IDDon
            {
                get { return iDDon; }
                set { iDDon = value; }
            }

            private string dTuong;

            public string DTuong
            {
                get { return dTuong; }
                set { dTuong = value; }
            }

            private int maKXuat;

            public int MaKXuat
            {
                get { return maKXuat; }
                set { maKXuat = value; }
            }

            private DateTime ngayKe;

            public DateTime NgayKe
            {
                get { return ngayKe; }
                set { ngayKe = value; }
            }
        }

        private List<datasoureBN> _ldsBN = new List<datasoureBN>();
        List<datasoureBN> temp = new List<datasoureBN>();

        #region Hàm set status cho đơn thuốc

        private void _setStatus(int id, int i)
        {
            var dthuoc = _dataContext.DThuoccts.Where(p => p.IDDon == (id)).ToList();
            foreach (var item in dthuoc)
            {
                item.Status = i;
            }

            _dataContext.SaveChanges();
        }

        #endregion Hàm set status cho đơn thuốc

        #region class tim kiem

        private class SearchModel
        {
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public int TrongDM { get; set; }
            public int MaKP { get; set; }
            public string HoTen { get; set; }
            public int MaBN { get; set; }
            public bool DonTra { get; set; }
            public bool DaHuy { get; set; }
        }

        #endregion class tim kiem

        #region duc sua form

        private List<datasoureBN> dsDuoc(SearchModel keySearch)
        {
            _ldsBN.Clear();
            temp.Clear();
            _dttu = DungChung.Ham.NgayTu(keySearch.TuNgay);
            _dtden = DungChung.Ham.NgayDen(keySearch.DenNgay);
            var a = (from dt1 in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP)
                     join vp1 in _dataContext.VienPhis.Where(p => !keySearch.DonTra ? (p.NgayTT > _dttu && p.NgayTT < _dtden) : true) on dt1.MaBNhan equals vp1.MaBNhan
                     select new
                     {
                         vp1.idVPhi,
                     }).ToList();
            var b = (from tu in _dataContext.TamUngs.Where(p => !keySearch.DonTra ? (p.NgayThu > _dttu && p.NgayThu < _dtden) : true)
                     select new
                     {
                         tu.IDTamUng
                     }).ToList();
            // đơn trả thuốc bắt theo ngày kê đơn
            #region 30372
            if (DungChung.Bien.MaBV == "30372")
            {
                if (chkBNKD.Checked)
                {
                    var rs1 = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")
                               join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on bn.MaBNhan equals dt.MaBNhan
                               join dtct in _dataContext.DThuoccts/*.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status == 0)*/ on dt.IDDon equals dtct.IDDon
                               join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                               where (dtct.Status == 0 || dtct.ThanhToan == 0) && bn.Status != 3
                               select new datasoureBN
                               {
                                   KieuDon = dt.KieuDon ?? 0,
                                   IDDTBN = bn.IDDTBN,
                                   MaBNhan = (int)dt.MaBNhan,
                                   TenBNhan = bn.TenBNhan,
                                   IDDon = dt.IDDon,
                                   IDNhap = dt.IDDon,
                                   SttBN = bn.Status,
                                   DTuong = bn.DTuong,
                                   MaKXuat = dt.MaKXuat ?? 0,
                                   NgayKe = dt.NgayKe ?? DateTime.Now,
                                   Status = dtct.Status ?? 0,
                                   TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                   MaCB = dt.MaCBXuat,
                                   DonTra = dtct.SoLuongct < 0 ? true : false,
                               }).ToList();
                    _ldsBN = (from bn in rs1
                              group bn by new { bn.KieuDon, bn.IDDTBN, bn.MaBNhan, bn.TenBNhan, bn.IDDon, bn.DTuong, bn.MaKXuat, bn.NgayKe, bn.Status, bn.TrongDM, bn.MaCB, bn.DonTra, bn.SttBN } into kq
                              select new datasoureBN
                              {
                                  IDDon = kq.Key.IDDon,
                                  IDNhap = kq.Key.IDDon,
                                  IDDTBN = kq.Key.IDDTBN,
                                  KieuDon = kq.Key.KieuDon,
                                  MaBNhan = kq.Key.MaBNhan,
                                  MaKXuat = kq.Key.MaKXuat,
                                  TenBNhan = kq.Key.TenBNhan,
                                  NgayKe = kq.Key.NgayKe,
                                  DTuong = kq.Key.DTuong,
                                  Status = kq.Key.Status,
                                  TrongDM = kq.Key.TrongDM,
                                  MaCB = kq.Key.MaCB,
                                  DonTra = kq.Key.DonTra,
                                  SttBN = kq.Key.SttBN,
                              }).Distinct().ToList();
                }
                else
                {

                    if (a.Count() > 0)
                    {
                        var rs = (from vp in _dataContext.VienPhis.Where(p => !keySearch.DonTra ? (p.NgayTT > _dttu && p.NgayTT < _dtden) : true)
                                  join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ") on vp.MaBNhan equals bn.MaBNhan
                                  join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on bn.MaBNhan equals dt.MaBNhan
                                  join dtct in _dataContext.DThuoccts.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status != 2) on dt.IDDon equals dtct.IDDon
                                  where dtct.ThanhToan != 1
                                  join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                  select new datasoureBN
                                  {
                                      KieuDon = dt.KieuDon ?? 0,
                                      IDDTBN = bn.IDDTBN,
                                      MaBNhan = (int)dt.MaBNhan,
                                      TenBNhan = bn.TenBNhan,
                                      IDDon = dt.IDDon,
                                      IDNhap = dt.IDDon,
                                      SttBN = bn.Status,
                                      DTuong = bn.DTuong,
                                      MaKXuat = dt.MaKXuat ?? 0,
                                      NgayKe = dt.NgayKe ?? DateTime.Now,
                                      Status = dtct.Status ?? 0,
                                      TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                      MaCB = dt.MaCBXuat,
                                      DonTra = dtct.SoLuongct < 0 ? true : false,
                                  }).ToList();
                        _ldsBN = (from bn in rs
                                  group bn by new { bn.KieuDon, bn.IDDTBN, bn.MaBNhan, bn.TenBNhan, bn.IDDon, bn.DTuong, bn.MaKXuat, bn.NgayKe, bn.Status, bn.TrongDM, bn.MaCB, bn.DonTra, bn.SttBN } into kq
                                  select new datasoureBN
                                  {
                                      IDDon = kq.Key.IDDon,
                                      IDNhap = kq.Key.IDDon,
                                      IDDTBN = kq.Key.IDDTBN,
                                      KieuDon = kq.Key.KieuDon,
                                      MaBNhan = kq.Key.MaBNhan,
                                      MaKXuat = kq.Key.MaKXuat,
                                      TenBNhan = kq.Key.TenBNhan,
                                      NgayKe = kq.Key.NgayKe,
                                      DTuong = kq.Key.DTuong,
                                      Status = kq.Key.Status,
                                      TrongDM = kq.Key.TrongDM,
                                      MaCB = kq.Key.MaCB,
                                      DonTra = kq.Key.DonTra,
                                      SttBN = kq.Key.SttBN,
                                  }).Distinct().ToList();
                        temp = _ldsBN;
                    }
                    if (b.Count() > 0)
                    {
                        var rs = (from tu in _dataContext.TamUngs.Where(p => !keySearch.DonTra ? (p.NgayThu > _dttu && p.NgayThu < _dtden) : true)
                                  join tuct in _dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                  join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ") on tu.MaBNhan equals bn.MaBNhan
                                  join dtct in _dataContext.DThuoccts.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status != 2) on tuct.IDDonct equals dtct.IDDonct
                                  join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on dtct.IDDon equals dt.IDDon
                                  join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                  select new datasoureBN
                                  {
                                      KieuDon = dt.KieuDon ?? 0,
                                      IDDTBN = bn.IDDTBN,
                                      MaBNhan = (int)dt.MaBNhan,
                                      TenBNhan = bn.TenBNhan,
                                      IDDon = dt.IDDon,
                                      IDNhap = dt.IDDon,
                                      SttBN = bn.Status,
                                      DTuong = bn.DTuong,
                                      MaKXuat = dt.MaKXuat ?? 0,
                                      NgayKe = dt.NgayKe ?? DateTime.Now,
                                      Status = dtct.Status ?? 0,
                                      TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                      MaCB = dt.MaCBXuat,
                                      DonTra = dtct.SoLuongct < 0 ? true : false,
                                  }).ToList();
                        _ldsBN = (from bn in rs
                                  group bn by new { bn.KieuDon, bn.IDDTBN, bn.MaBNhan, bn.TenBNhan, bn.IDDon, bn.DTuong, bn.MaKXuat, bn.NgayKe, bn.Status, bn.TrongDM, bn.MaCB, bn.DonTra, bn.SttBN } into kq
                                  select new datasoureBN
                                  {
                                      IDDon = kq.Key.IDDon,
                                      IDNhap = kq.Key.IDDon,
                                      IDDTBN = kq.Key.IDDTBN,
                                      KieuDon = kq.Key.KieuDon,
                                      MaBNhan = kq.Key.MaBNhan,
                                      MaKXuat = kq.Key.MaKXuat,
                                      TenBNhan = kq.Key.TenBNhan,
                                      NgayKe = kq.Key.NgayKe,
                                      DTuong = kq.Key.DTuong,
                                      Status = kq.Key.Status,
                                      TrongDM = kq.Key.TrongDM,
                                      MaCB = kq.Key.MaCB,
                                      DonTra = kq.Key.DonTra,
                                      SttBN = kq.Key.SttBN,
                                  }).Distinct().ToList();
                        temp.AddRange(_ldsBN);
                    }
                }
            }
            #endregion
            #region BV Khác
            else
            {
                if (chkBNKD.Checked)
                {
                    var rs1 = (from bn in _dataContext.BenhNhans.Where(p => (DungChung.Bien.MaBV == "27022" ? (p.NoiTru == 0 || p.NoiTru == 1) && p.NNhap > _dttu && p.NNhap < _dtden : p.NoiTru == 0)).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")
                               join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on bn.MaBNhan equals dt.MaBNhan
                               join dtct in _dataContext.DThuoccts.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status != 2) on dt.IDDon equals dtct.IDDon
                               join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                               select new datasoureBN
                               {
                                   KieuDon = dt.KieuDon ?? 0,
                                   IDDTBN = bn.IDDTBN,
                                   MaBNhan = (int)dt.MaBNhan,
                                   TenBNhan = bn.TenBNhan,
                                   IDDon = dt.IDDon,
                                   IDNhap = dt.IDDon,
                                   SttBN = bn.Status,
                                   DTuong = bn.DTuong,
                                   MaKXuat = dt.MaKXuat ?? 0,
                                   NgayKe = dt.NgayKe ?? DateTime.Now,
                                   Status = dtct.Status ?? 0,
                                   TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                   MaCB = dt.MaCBXuat,
                                   DonTra = dtct.SoLuongct < 0 ? true : false,
                               }).ToList();
                    _ldsBN = (from bn in rs1
                              group bn by new
                              {
                                  bn.KieuDon,
                                  bn.IDDTBN,
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.IDDon,
                                  bn.DTuong,
                                  bn.MaKXuat,
                                  bn.NgayKe,
                                  bn.Status,
                                  bn.TrongDM,
                                  bn.MaCB,
                                  bn.DonTra,
                                  bn.SttBN
                              }into kq
                              select new datasoureBN
                              {
                                  IDDon = kq.Key.IDDon,
                                  IDNhap = kq.Key.IDDon,
                                  IDDTBN = kq.Key.IDDTBN,
                                  KieuDon = kq.Key.KieuDon,
                                  MaBNhan = kq.Key.MaBNhan,
                                  MaKXuat = kq.Key.MaKXuat,
                                  TenBNhan = kq.Key.TenBNhan,
                                  NgayKe = kq.Key.NgayKe,
                                  DTuong = kq.Key.DTuong,
                                  Status = kq.Key.Status,
                                  TrongDM = kq.Key.TrongDM,
                                  MaCB = kq.Key.MaCB,
                                  DonTra = kq.Key.DonTra,
                                  SttBN = kq.Key.SttBN,
                              }).Distinct().ToList();
                }
                else
                {
                    var rs = (from vp in _dataContext.VienPhis.Where(p => !keySearch.DonTra ? (p.NgayTT > _dttu && p.NgayTT < _dtden) : true)
                              join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ") on vp.MaBNhan equals bn.MaBNhan
                              join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on bn.MaBNhan equals dt.MaBNhan
                              join dtct in _dataContext.DThuoccts.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status != 2) on dt.IDDon equals dtct.IDDon
                              join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                              select new datasoureBN
                              {
                                  KieuDon = dt.KieuDon ?? 0,
                                  IDDTBN = bn.IDDTBN,
                                  MaBNhan = (int)dt.MaBNhan,
                                  TenBNhan = bn.TenBNhan,
                                  IDDon = dt.IDDon,
                                  IDNhap = dt.IDDon,
                                  SttBN = bn.Status,
                                  DTuong = bn.DTuong,
                                  MaKXuat = dt.MaKXuat ?? 0,
                                  NgayKe = dt.NgayKe ?? DateTime.Now,
                                  Status = dtct.Status ?? 0,
                                  TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                  MaCB = dt.MaCBXuat,
                                  DonTra = dtct.SoLuongct < 0 ? true : false,
                              }).ToList();

                    if (DungChung.Bien.MaBV == "27022")
                    {
                        rs = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap > _dttu && p.NNhap < _dtden).Where(p => keySearch.TrongDM == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")
                              join dt in _dataContext.DThuocs.Where(p => keySearch.DonTra ? (p.NgayKe > _dttu && p.NgayKe < _dtden) : true).Where(p => p.PLDV == 1 && p.MaKXuat == keySearch.MaKP) on bn.MaBNhan equals dt.MaBNhan
                              join dtct in _dataContext.DThuoccts.Where(p => keySearch.DaHuy ? p.Status == 2 : p.Status != 2) on dt.IDDon equals dtct.IDDon
                              join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                              where (bn.NoiTru == 1 && dtct.SoPL == -1) || (bn.NoiTru == 0 && bn.Status == 3)
                              select new datasoureBN
                              {
                                  KieuDon = dt.KieuDon ?? 0,
                                  IDDTBN = bn.IDDTBN,
                                  MaBNhan = (int)dt.MaBNhan,
                                  TenBNhan = bn.TenBNhan,
                                  IDDon = dt.IDDon,
                                  IDNhap = dt.IDDon,
                                  SttBN = bn.Status,
                                  DTuong = bn.DTuong,
                                  MaKXuat = dt.MaKXuat ?? 0,
                                  NgayKe = dt.NgayKe ?? DateTime.Now,
                                  Status = dtct.Status ?? 0,
                                  TrongDM = bn.DTuong == "BHYT" ? 1 : 0,
                                  MaCB = dt.MaCBXuat,
                                  DonTra = dtct.SoLuongct < 0 ? true : false,
                              }).ToList();
                    }
                    _ldsBN = (from bn in rs
                              group bn by new { bn.KieuDon, bn.IDDTBN, bn.MaBNhan, bn.TenBNhan, bn.IDDon, bn.DTuong, bn.MaKXuat, bn.NgayKe, bn.Status, bn.TrongDM, bn.MaCB, bn.DonTra, bn.SttBN } into kq
                              select new datasoureBN
                              {
                                  IDDon = kq.Key.IDDon,
                                  IDNhap = kq.Key.IDDon,
                                  IDDTBN = kq.Key.IDDTBN,
                                  KieuDon = kq.Key.KieuDon,
                                  MaBNhan = kq.Key.MaBNhan,
                                  MaKXuat = kq.Key.MaKXuat,
                                  TenBNhan = kq.Key.TenBNhan,
                                  NgayKe = kq.Key.NgayKe,
                                  DTuong = kq.Key.DTuong,
                                  Status = kq.Key.Status,
                                  TrongDM = kq.Key.TrongDM,
                                  MaCB = kq.Key.MaCB,
                                  DonTra = kq.Key.DonTra,
                                  SttBN = kq.Key.SttBN,
                              }).Distinct().ToList();
                }
            }
            #endregion
            if (DungChung.Bien.MaBV == "30372" && chkBNKD.Checked == false)
            {
                if (keySearch.MaBN > 0)
                    return temp.Where(p => p.MaBNhan == keySearch.MaBN).ToList();

                if (!string.IsNullOrEmpty(keySearch.HoTen) && keySearch.HoTen != "Tìm tên|Mã BN")
                    return temp.Where(p => p.TenBNhan.Contains(keySearch.HoTen)).ToList();

                return temp;
            }
            else
            {
                if (keySearch.MaBN > 0)
                    return _ldsBN.Where(p => p.MaBNhan == keySearch.MaBN).ToList();

                if (!string.IsNullOrEmpty(keySearch.HoTen) && keySearch.HoTen != "Tìm tên|Mã BN")
                    return _ldsBN.Where(p => p.TenBNhan.Contains(keySearch.HoTen)).ToList();

                return _ldsBN;
            }
        }

        #endregion duc sua form

        private class MyObject
        {
            public int TrongBH { set; get; }
            public string Text { set; get; }
        }

        private void usXuatNgoaiTru_Load(object sender, EventArgs e)
        {
            ckcDonTra.Visible = DungChung.Bien.MaBV == "24012" ? true : false;
            ckcDonTra.Checked = DungChung.Bien.MaBV == "24012" ? true : false;
            radNgoaiDM.SelectedIndex = 1;
            List<MyObject> lMyObj = new List<MyObject>();
            lMyObj.Add(new MyObject { TrongBH = 0, Text = "Ngoài DM" });
            lMyObj.Add(new MyObject { TrongBH = 1, Text = "Trong DM" });
            lupTrongDM.DataSource = lMyObj;
            var kp = _dataContext.KPhongs.Where(p => p.PLoai == "Khoa dược" && p.Status == 1).ToList();

            var tendv = (from dv in _dataContext.DichVus.Where(p => p.PLoai == 1)
                         select new
                         {
                             TenDV = DungChung.Bien.MaBV == "24009" ? dv.TenRG : dv.TenDV,
                             dv.MaDV
                         }).ToList();
            lupMaDV.DataSource = tendv.ToList();

            lupTimMaKP.Properties.DataSource = kp.ToList();
            if (DungChung.Bien.MaKP > 0)
                lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            lupMaCBx.DataSource = _dataContext.CanBoes.ToList();

            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtNgayXuat.DateTime = System.DateTime.Now;

            if (DungChung.Bien.MaBV != "24272")
            {
                colChon.Visible = chkSelectAll.Visible = false;
            }

            int makho1 = 0;
            if (lupTimMaKP.EditValue != null)
                makho1 = Convert.ToInt32(lupTimMaKP.EditValue);

            GC.Collect();

            load();
        }

        private void LoadData(List<datasoureBN> dt)
        {
            if (chkHuydon.Checked)
                grcBenhNhankd.DataSource = dt.Where(p => p.Status == 2);
            else
                grcBenhNhankd.DataSource = dt.Where(p => p.Status == 0);
            var test = dt.Where(p => p.Status == 0).ToList();
            grcBNhanxd.DataSource = dt.Where(p => p.Status == 1);
            if (DungChung.Bien.MaBV == "24272")
            {
                grcBNhanxd.DataSource = dt.Where(p => p.Status == 1);
            }
        }

        private void LoadData(List<datasoureBN> dt, string searchKey, DevExpress.XtraGrid.GridControl gridControl, int status)
        {
            int _int_maBN = 0;
            if (!string.IsNullOrEmpty(searchKey))
            {
                searchKey.ToLower();
                int rs;
                if (Int32.TryParse(searchKey, out rs))
                    _int_maBN = Convert.ToInt32(searchKey);
            }
            var result = dt.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(o => o.TenBNhan).ToList();
            if (result.Count() > 0)
            {
                gridControl.DataSource = result.Where(p => p.Status == status);
                if (DungChung.Bien.MaBV == "24272")
                {
                    gridControl.DataSource = result.Where(p => p.Status == status);
                }
            }
            else
            {
                result = dt.Where(p => p.TenBNhan.ToLower().Contains(searchKey.ToLower())).OrderByDescending(o => o.TenBNhan).ToList();
                if (result.Count() > 0)
                {
                    gridControl.DataSource = result.Where(p => p.Status == status);
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        gridControl.DataSource = result.Where(p => p.Status == status).Where(c => c.Chon == true);
                    }
                }
                else
                {
                    gridControl.DataSource = null;
                }
            }
        }

        private class chitietDuoc
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public string TrongBH { get; set; }
            public string DonVi { get; set; }
            public string DonGia { get; set; }
            public string SoLuong { get; set; }
            public string SoLo { get; set; }
            public string ThanhTien { get; set; }
            public string HanDung { get; set; }
            public string TyLeTT { get; set; }
            public string IDDonct { get; set; }
        }

        private int _idkd = 0;// iddon
        private int _mabn = 0;

        private void GrvBenhNhankd_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) //minhvd
        {
            if (grvBenhNhankd.GetRowCellValue(e.RowHandle, colIDDon) != null && DungChung.Bien.MaBV == "24012")
            {
                int i = Convert.ToInt32(grvBenhNhankd.GetRowCellValue(e.RowHandle, colIDDon));
                var check = _dataContext.DThuoccts.Where(p => p.IDDon == i).ToList();

                if (check.Count > 0)
                {
                    if (check.First().SoLuong < 0)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
                    }
                }
                if (grvBenhNhankd.GetRowCellValue(e.RowHandle, colKieuDon) != null)
                {
                    if (grvBenhNhankd.GetRowCellValue(e.RowHandle, colKieuDon).ToString() == "2")
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void grvBenhNhankd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _mabn = 0;
            _idkd = 0;
            sovv = null;
            if (grvBenhNhankd.GetFocusedRowCellValue(colIDDon) != null && grvBenhNhankd.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                _idkd = int.Parse(grvBenhNhankd.GetFocusedRowCellValue(colIDDon).ToString());
            }
            txtIDDon.Text = _idkd.ToString();
            if (grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                _mabn = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan));
                txtMaBNhan.Text = grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan).ToString();
                txtTenBnhan.Text = grvBenhNhankd.GetFocusedRowCellValue(colTenBNhan).ToString();
            }
            else
                txtMaBNhan.Text = "";
            if (grvBenhNhankd.GetFocusedRowCellValue(colDTuong) != null)
                _dTuongBN = grvBenhNhankd.GetFocusedRowCellValue(colDTuong).ToString();
            var qtt = _dataContext.VienPhis.Where(p => p.MaBNhan == (_mabn)).FirstOrDefault();
            if (qtt != null)
            {
                dtNgayXuat.DateTime = System.DateTime.Now;
                if (DungChung.Bien.MaBV == "24012")
                {
                    var ngayke = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == _idkd)
                                  join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                  where (dtct.SoLuongct < 0)
                                  select new { dt.NgayKe }).ToList();
                    if (ngayke.Count > 0)
                    {
                        dtNgayXuat.DateTime = Convert.ToDateTime(ngayke.First().NgayKe.ToString());
                    }
                    else
                    {
                        var ngaytt = qtt.NgayTT;
                        if (ngaytt != null)
                            dtNgayXuat.DateTime = ngaytt.Value;
                    }
                }
                else
                {
                    var ngaytt = qtt.NgayTT;
                    if (ngaytt != null)
                        dtNgayXuat.DateTime = ngaytt.Value;
                }
                _ngayke = dtNgayXuat.DateTime;
            }

            int makho = 0;
            if (lupTimMaKP.EditValue != null)
                makho = Convert.ToInt32(lupTimMaKP.EditValue);
            _lDThuocct = _dataContext.DThuoccts.Where(p => p.IDDon == _idkd).Where(p => DungChung.Bien.keNhieuKho ? p.MaKXuat == makho : true).Where(p => chkHuydon.Checked ? p.Status == 2 : (p.Status == 0 || p.Status == null)).OrderBy(p => p.IDDonct).ToList();

            //var chitiet = (from dtct in _dataContext.DThuoccts.Where(p => p.IDDon == _idkd)
            //               join dt in _dataContext.DThuocs on dtct.IDDon equals dt.IDDon
            //               select new chitietDuoc
            //               {
            //                   MaDV = dt.
            //               }).ToList();

            bindingSource1.DataSource = _lDThuocct.ToList();
            grcDonThuocct.DataSource = bindingSource1;
            idmabn = _mabn;

            var qdt = _dataContext.DThuocs.Where(p => p.IDDon == _idkd).FirstOrDefault();
            if (qdt != null)
            {
                _maKhoXuat = qdt.MaKXuat ?? 0;
                sovv = qdt.SoVV ?? 0;
                if (qdt.SoVV == -1)
                    btnDuyet.Text = "Hủy duyệt";
                else
                    btnDuyet.Text = "Duyệt";
            }
            if (qtt != null)
            {
                btnSua.Enabled = false;
                btnDuyet.Enabled = false;
            }
            else
            {
                btnSua.Enabled = true;
                btnDuyet.Enabled = true;
            }
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            grvDonThuocct.OptionsBehavior.ReadOnly = true;
            grvDonThuocct.OptionsBehavior.Editable = false;
            grvDonThuocct.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            grvDonThuocct.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            GC.Collect();
        }

        private void btnXuatDuoc_Click(object sender, EventArgs e)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DialogResult _result = DialogResult.No;
            string _tenbn = "";
            string _dtuong = "";
            byte IdDTBN = 0;
            int _maBN = -1, ot;
            double soluongton = 0;
            int kieudon = -1;
            bool _cothexuat = true;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _maBN = Convert.ToInt32(txtMaBNhan.Text);
            int makho = 0;
            if (lupTimMaKP.EditValue != null)
                makho = Convert.ToInt32(lupTimMaKP.EditValue);

            var bnChon = dt.Where(c => c.MaKXuat == makho).Where(x => x.Chon == true).ToList();
            if (DungChung.Bien.MaBV == "24272")
            {
                if (bnChon.Count() >= 0)
                {
                    foreach (var item in bnChon)
                    {
                        _cothexuat = true;
                        _dtuong = item.DTuong;
                        _tenbn = item.TenBNhan;
                        _maBN = item.MaBNhan;
                        makho = item.MaKXuat;
                        IdDTBN = Convert.ToByte(item.IDDTBN);
                        kieudon = item.KieuDon;
                        string thuochet = "";
                        bool checkDT = false;
                        DThuoc dtt = new DThuoc();
                        if (_cothexuat)
                        {
                            _lDThuocct = _dataContext.DThuoccts.Where(p => p.IDDon == item.IDDon).Where(p => DungChung.Bien.keNhieuKho ? p.MaKXuat == makho : true).Where(p => chkHuydon.Checked ? p.Status == 2 : (p.Status == 0 || p.Status == null)).OrderBy(p => p.IDDonct).ToList();
                            foreach (var i in _lDThuocct)
                            {
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                {
                                    if (DungChung.Bien.MaBV != "24297")
                                    {
                                        if (!checkDT)
                                        {
                                            dtt = _dataContext.DThuocs.FirstOrDefault(o => o.IDDon == i.IDDon);
                                        }
                                        //Check duyệt đơn thuốc mới cho xuất
                                        if (dtt == null || dtt.SoVV != -1)
                                        {
                                            MessageBox.Show("Đơn thuốc chưa được duyệt không thể xuất");
                                            checkDT = true;
                                            return;
                                        }
                                    }
                                    //Check duyệt thử quỹ mới cho xuất
                                    var duyetTQ = (from tuct in _dataContext.TamUngcts.Where(o => o.IDDonct == i.IDDonct && o.Status != 1)
                                                   join tu in _dataContext.TamUngs on tuct.IDTamUng equals tu.IDTamUng
                                                   select tu).FirstOrDefault();
                                    BenhNhan benhNhan = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _maBN);
                                    if (duyetTQ != null)
                                    {
                                        if (benhNhan != null && string.IsNullOrWhiteSpace(benhNhan.SThe) && duyetTQ.DuyetPhieuThu != true)
                                        {
                                            MessageBox.Show(string.Format("Số phiếu: {0} chưa được duyệt tạm thu không thể xuất", duyetTQ.IDTamUng));
                                            return;
                                        }
                                    }
                                }
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "30372")
                                {
                                    //Check đã thanh toán = tu+vp mới cho xuất
                                    var checkTU = (from tu in _dataContext.TamUngs.Where(o => o.MaBNhan == _maBN)
                                                   join tuct in _dataContext.TamUngcts.Where(o => o.MaDV == i.MaDV && o.Status != 1) on tu.IDTamUng equals tuct.IDTamUng
                                                   select tuct).FirstOrDefault();
                                    var checkVP = (from vp in _dataContext.VienPhis.Where(o => o.MaBNhan == _maBN)
                                                   join vpct in _dataContext.VienPhicts.Where(o => o.MaDV == i.MaDV) on vp.idVPhi equals vpct.idVPhi
                                                   select vpct).FirstOrDefault();
                                    if (checkTU == null && checkVP == null)
                                    {
                                        MessageBox.Show(string.Format("Có thuốc/vật tư mã: {0} chưa thanh toán không thể xuất", i.MaDV));
                                        return;
                                    }
                                }

                                soluongton = 0;

                                #region check theo lấy giá nhập

                                double dongiaNhap = i.DonGia;
                                //if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                                //    dongiaNhap = DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// DungChung.Ham.getGiaNhapByGiaXuat(i.MaDV, i.DonGia, i.TrongBH);

                                #endregion check theo lấy giá nhập

                                soluongton = DungChung.Ham._checkTon(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, makho.ToString() == null ? 0 : makho, dongiaNhap, i.SoLuong, i.SoLo);
                                if (soluongton < 0)
                                {
                                    var tenthuoc = _dataContext.DichVus.Where(p => p.MaDV == i.MaDV).ToList();
                                    if (tenthuoc.Count > 0)
                                    {
                                        thuochet += i.SoLuongct < 0 ? "" : tenthuoc.First().TenDV + "; ";
                                        _cothexuat = false;
                                    }
                                }
                            }
                        }
                        if (lupTimMaKP.Text == "")
                        {
                            MessageBox.Show("Chưa chọn kho kê đơn!");
                            return;
                        }
                        else if (!string.IsNullOrEmpty(thuochet))
                        {
                            MessageBox.Show(thuochet + " đã hết, hãy kê thuốc khác cho bệnh nhân " + item.MaBNhan + "(" + item.TenBNhan + ")");
                            continue;
                        }
                        if (_cothexuat)
                            _result = MessageBox.Show("Xuất dược cho bệnh nhân: " + _tenbn + " ?", "Xuất dược", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.No)
                            _cothexuat = false;

                        if (_cothexuat)
                        {
                            if (ckcxuattutruc.Checked == true)
                            {
                                var ktrakhoxuat = _dataContext.KPhongs.Where(p => p.MaKP == makho && p.PLoai.Contains("Tủ trực")).ToList();
                                if (ktrakhoxuat.Count > 0)
                                {
                                    MessageBox.Show("Bạn không thể xuất dược từ tủ trực sang tủ trực");
                                    return;
                                }
                                else
                                {
                                    //using (System.Data.Entity.DbContextTransaction trans = _dataContext.Database.BeginTransaction())
                                    //{
                                    //    try
                                    //    {
                                    NhapD _xuat = new NhapD();
                                    _xuat.MaBNhan = _maBN;
                                    _xuat.SoCT = item.IDDon.ToString();
                                    _xuat.SoPL = item.IDDon;
                                    _xuat.PLoai = 2;
                                    _xuat.KieuDon = 6;
                                    _xuat.NgayNhap = item.NgayKe;
                                    _xuat.XuatTD = 1;
                                    _xuat.MaKP = makho;
                                    _xuat.MaCB = DungChung.Bien.MaCB;
                                    _xuat.IDNhap = item.IDNhap;
                                    _xuat.SoPhieu = DungChung.Ham._idphieutheokp(2, Convert.ToInt32(makho));
                                    int Iddon = item.IDDon;
                                    var rv = _dataContext.DThuocs.Where(p => p.MaKXuat != null && p.IDDon == Iddon).Select(p => p.MaKXuat).ToList();
                                    if (rv.Count > 0)
                                        _xuat.MaKPnx = rv.First();
                                    _dataContext.NhapDs.Add(_xuat);
                                    if (_dataContext.SaveChanges() >= 0)
                                    {
                                        int iddt = item.IDDon;

                                        var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddt).ToList();
                                        foreach (var itemdt in dtct)
                                        {
                                            itemdt.Status = 1;
                                        }
                                        _dataContext.SaveChanges();
                                        int id = _xuat.IDNhap;
                                        List<DichVu> listDichVus = new List<DichVu>();
                                        foreach (var i in _lDThuocct)
                                        {
                                            NhapDct _xuatct = new NhapDct();
                                            _xuatct.IDNhap = id;
                                            _xuatct.MaDV = i.MaDV;
                                            _xuatct.DonVi = i.DonVi;
                                            _xuatct.DonGiaX = i.DonGia;
                                            if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                                            {
                                                _xuatct.DonGia = DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// laays don gia nhap
                                                _xuatct.DonGiaX = i.DonGia;
                                            }
                                            else
                                            {
                                                _xuatct.DonGia = i.DonGia;
                                            }

                                            _xuatct.MaCC = i.MaCC;
                                            _xuatct.SoLuongX = i.SoLuong;
                                            _xuatct.ThanhTienX = Math.Round(i.DonGia * i.SoLuong, 3);
                                            _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, i.SoLuong, makho);
                                            _xuatct.ThanhTienDY = Math.Round(_xuatct.SoLuongDY * i.DonGia, 3);
                                            _xuatct.SoLuongN = 0;
                                            _xuatct.ThanhTienN = 0;
                                            _xuatct.SoLuongSD = 0;
                                            _xuatct.ThanhTienSD = 0;
                                            _xuatct.SoLuongKK = 0;
                                            _xuatct.ThanhTienKK = 0;
                                            _xuatct.SoLo = i.SoLo;
                                            _xuatct.HanDung = i.HanDung;
                                            _xuatct.TrongBH = i.TrongBH;
                                            _xuatct.IDDTBN = IdDTBN;
                                            _xuatct.MaBNhan = Convert.ToInt32(txtMaBNhan.Text);
                                            _dataContext.NhapDcts.Add(_xuatct);
                                            listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });
                                        }
                                        if (_lDThuocct.Count > 0)
                                            _dataContext.SaveChanges();
                                        if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0) // tét
                                        {
                                            DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0);
                                        }
                                    }

                                    //        trans.Commit();

                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        trans.Rollback();
                                    //        MessageBox.Show("Lỗi quá trình: " + ex.Message);

                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                NhapD _xuat = new NhapD();

                                _xuat.MaBNhan = _maBN;
                                _xuat.SoCT = item.IDDon.ToString();
                                _xuat.SoPL = item.IDDon;
                                _xuat.PLoai = 2;

                                //if (kieudon == 6)
                                //{
                                //    _xuat.KieuDon = 4;
                                //    if(DungChung.Bien.MaBV == "27183")
                                //        _xuat.KieuDon = 0;
                                //}
                                //else
                                _xuat.KieuDon = 0;

                                _xuat.NgayNhap = item.NgayKe;
                                _xuat.XuatTD = 1;
                                _xuat.IDNhap = item.IDNhap;
                                _xuat.MaKP = item.MaKXuat;
                                _xuat.MaCB = DungChung.Bien.MaCB;
                                _xuat.SoPhieu = DungChung.Ham._idphieutheokp(2, makho);
                                var rv = _dataContext.RaViens.Where(p => p.MaKP != null && p.MaBNhan == _maBN).Select(p => p.MaKP).ToList();
                                if (rv.Count > 0)
                                    _xuat.MaKPnx = rv.First();
                                else if (_lDThuocct.Count > 0 && _lDThuocct.First() != null)
                                    _xuat.MaKPnx = _lDThuocct.First().MaKP;
                                _dataContext.NhapDs.Add(_xuat);
                                if (_dataContext.SaveChanges() >= 0)
                                {
                                    int idDon = item.IDDon;
                                    var dthuoc = _dataContext.DThuocs.Where(p => p.IDDon == idDon).FirstOrDefault();
                                    if (dthuoc != null)
                                    {
                                        dthuoc.MaCBXuat = DungChung.Bien.MaCB;
                                        _dataContext.SaveChanges();
                                    }
                                    //DThuoc dt1 = new DThuoc();

                                    int iddt = item.IDDon;

                                    var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddt).ToList();
                                    foreach (var itemdt in dtct)
                                    {
                                        itemdt.Status = 1;
                                    }
                                    try
                                    {
                                        _dataContext.SaveChanges();
                                    }
                                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                                    {
                                        Exception raise = dbEx;

                                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                                        {
                                            foreach (var validationError in validationErrors.ValidationErrors)
                                            {
                                                string message = string.Format("{0}:{1}",

                                                  validationErrors.Entry.Entity.ToString(),

                                                    validationError.ErrorMessage);

                                                // raise a new exception nesting

                                                // the current instance as InnerException

                                                raise = new InvalidOperationException(message, raise);
                                            }
                                        }

                                        throw raise;
                                    }
                                    //_dataContext.SaveChanges();0
                                    int id = _xuat.IDNhap;
                                    List<DichVu> listDichVus = new List<DichVu>();
                                    foreach (var i in _lDThuocct)
                                    {
                                        NhapDct _xuatct = new NhapDct();
                                        _xuatct.IDNhap = id;
                                        _xuatct.MaDV = i.MaDV;
                                        _xuatct.DonVi = i.DonVi;
                                        _xuatct.DonGiaX = i.DonGia;
                                        if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                                        {
                                            _xuatct.DonGia = i.DonGia; //DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// laays don gia nhap
                                            _xuatct.DonGiaX = i.DonGia;
                                        }
                                        else
                                        {
                                            _xuatct.DonGia = i.DonGia;
                                        }

                                        _xuatct.MaCC = i.MaCC;
                                        _xuatct.SoLuongX = i.SoLuong;
                                        _xuatct.ThanhTienX = Math.Round(i.DonGia * i.SoLuong, 3);
                                        _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, i.SoLuong, makho);
                                        _xuatct.ThanhTienDY = Math.Round(_xuatct.SoLuongDY * i.DonGia, 3);
                                        _xuatct.SoLuongN = 0;
                                        _xuatct.ThanhTienN = 0;
                                        _xuatct.SoLuongSD = 0;
                                        _xuatct.ThanhTienSD = 0;
                                        _xuatct.SoLuongKK = 0;
                                        _xuatct.ThanhTienKK = 0;
                                        _xuatct.SoLo = i.SoLo;
                                        _xuatct.HanDung = i.HanDung;
                                        _xuatct.TrongBH = i.TrongBH;
                                        _xuatct.IDDTBN = IdDTBN;
                                        _xuatct.MaBNhan = _maBN;
                                        _dataContext.NhapDcts.Add(_xuatct);
                                        listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });
                                    }
                                    if (_lDThuocct.Count > 0)
                                        _dataContext.SaveChanges();
                                    if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0)
                                    {
                                        DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0); // tét
                                    }
                                }

                                _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                                _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);

                                int makho1 = 0;
                                if (lupTimMaKP.EditValue != null)
                                    makho1 = item.MaKXuat;

                                load();
                            }
                        }
                    }
                    //load();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    if (grvBenhNhankd.GetFocusedRowCellDisplayText(colDTuong) != null && grvBenhNhankd.GetFocusedRowCellDisplayText(colDTuong).ToString() != "")
                        _dtuong = grvBenhNhankd.GetFocusedRowCellDisplayText(colDTuong).ToString();
                    if (grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan) != null && grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan).ToString() != "")
                        _tenbn = grvBenhNhankd.GetFocusedRowCellDisplayText(colTenBNhan).ToString();

                    if (grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDTBN) != null && grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDTBN).ToString() != "")
                        IdDTBN = Convert.ToByte(grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDTBN));

                    if (DungChung.Bien.MaBV == "33050" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30012")
                    {
                        var tctt = _dataContext.TamUngs.Where(p => p.MaBNhan == _maBN).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).ToList();
                        if (tctt.Count <= 0)
                        {
                            MessageBox.Show("Bệnh nhân chưa duyệt thu-chi thanh toán, bạn không thể xuất dược");
                            _cothexuat = false;
                        }
                    }
                    string thuochet = "";
                    bool checkDT = false;
                    DThuoc dt = new DThuoc();
                    if (_cothexuat)
                        foreach (var i in _lDThuocct)
                        {
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                            {
                                if (DungChung.Bien.MaBV != "24297")
                                {
                                    if (!checkDT)
                                    {
                                        dt = _dataContext.DThuocs.FirstOrDefault(o => o.IDDon == i.IDDon);
                                    }
                                    //Check duyệt đơn thuốc mới cho xuất
                                    if (dt == null || dt.SoVV != -1)
                                    {
                                        MessageBox.Show("Đơn thuốc chưa được duyệt không thể xuất");
                                        checkDT = true;
                                        return;
                                    }
                                }
                                //Check duyệt thử quỹ mới cho xuất
                                var duyetTQ = (from tuct in _dataContext.TamUngcts.Where(o => o.IDDonct == i.IDDonct && o.Status != 1)
                                               join tu in _dataContext.TamUngs on tuct.IDTamUng equals tu.IDTamUng
                                               select tu).FirstOrDefault();
                                BenhNhan benhNhan = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _maBN);
                                if (duyetTQ != null)
                                {
                                    if (benhNhan != null && string.IsNullOrWhiteSpace(benhNhan.SThe) && duyetTQ.DuyetPhieuThu != true)
                                    {
                                        MessageBox.Show(string.Format("Số phiếu: {0} chưa được duyệt tạm thu không thể xuất", duyetTQ.IDTamUng));
                                        return;
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "30372")
                            {
                                //Check đã thanh toán = tu+vp mới cho xuất
                                var checkTU = (from tu in _dataContext.TamUngs.Where(o => o.MaBNhan == _maBN)
                                               join tuct in _dataContext.TamUngcts.Where(o => o.MaDV == i.MaDV && o.Status != 1) on tu.IDTamUng equals tuct.IDTamUng
                                               select tuct).FirstOrDefault();
                                var checkVP = (from vp in _dataContext.VienPhis.Where(o => o.MaBNhan == _maBN)
                                               join vpct in _dataContext.VienPhicts.Where(o => o.MaDV == i.MaDV) on vp.idVPhi equals vpct.idVPhi
                                               select vpct).FirstOrDefault();
                                if (checkTU == null && checkVP == null)
                                {
                                    MessageBox.Show(string.Format("Có thuốc/vật tư mã: {0} chưa thanh toán không thể xuất", i.MaDV));
                                    return;
                                }
                            }

                            soluongton = 0;

                            #region check theo lấy giá nhập

                            double dongiaNhap = i.DonGia;
                            //if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                            //    dongiaNhap = DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// DungChung.Ham.getGiaNhapByGiaXuat(i.MaDV, i.DonGia, i.TrongBH);

                            #endregion check theo lấy giá nhập

                            soluongton = DungChung.Ham._checkTon(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), dongiaNhap, i.SoLuong, i.SoLo);
                            if (soluongton < 0)
                            {
                                var tenthuoc = _dataContext.DichVus.Where(p => p.MaDV == i.MaDV).ToList();
                                if (tenthuoc.Count > 0)
                                {
                                    thuochet += i.SoLuongct < 0 ? "" : tenthuoc.First().TenDV + "; ";
                                    _cothexuat = false;
                                }
                            }
                        }
                    if (lupTimMaKP.Text == "")
                    {
                        MessageBox.Show("Chưa chọn kho kê đơn!");
                        return;
                    }
                    else if (!string.IsNullOrEmpty(thuochet))
                    {
                        MessageBox.Show(thuochet + " đã hết, hãy kê thuốc khác cho bệnh nhân");
                        return;
                    }

                    if (_cothexuat)
                        _result = MessageBox.Show("Xuất dược cho bệnh nhân: " + _tenbn + " ?", "Xuất dược", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.No)
                        _cothexuat = false;

                    if (grvBenhNhankd.GetFocusedRowCellDisplayText(colKieuDon) != null)
                        kieudon = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellDisplayText(colKieuDon));
                    if (_cothexuat)
                    {
                        if (ckcxuattutruc.Checked == true)
                        {
                            var ktrakhoxuat = _dataContext.KPhongs.Where(p => p.MaKP == makho && p.PLoai.Contains("Tủ trực")).ToList();
                            if (ktrakhoxuat.Count > 0)
                            {
                                MessageBox.Show("Bạn không thể xuất dược từ tủ trực sang tủ trực");
                                return;
                            }
                            else
                            {
                                //using (System.Data.Entity.DbContextTransaction trans = _dataContext.Database.BeginTransaction())
                                //{
                                //    try
                                //    {
                                NhapD _xuat = new NhapD();
                                _xuat.MaBNhan = _maBN;
                                _xuat.SoCT = txtIDDon.Text;
                                _xuat.SoPL = Convert.ToInt32(txtIDDon.Text);
                                _xuat.PLoai = 2;
                                _xuat.KieuDon = 6;
                                _xuat.NgayNhap = _ngayke;
                                _xuat.XuatTD = 1;
                                _xuat.MaKP = makho;
                                _xuat.MaCB = DungChung.Bien.MaCB;
                                _xuat.SoPhieu = DungChung.Ham._idphieutheokp(2, Convert.ToInt32(makho));
                                int Iddon = Convert.ToInt32(txtIDDon.Text);
                                var rv = _dataContext.DThuocs.Where(p => p.MaKXuat != null && p.IDDon == Iddon).Select(p => p.MaKXuat).ToList();
                                if (rv.Count > 0)
                                    _xuat.MaKPnx = rv.First();
                                _dataContext.NhapDs.Add(_xuat);
                                if (_dataContext.SaveChanges() >= 0)
                                {
                                    int iddt = Convert.ToInt32(txtIDDon.Text);

                                    var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddt).ToList();
                                    foreach (var item in dtct)
                                    {
                                        item.Status = 1;
                                    }
                                    _dataContext.SaveChanges();
                                    int id = _xuat.IDNhap;
                                    List<DichVu> listDichVus = new List<DichVu>();
                                    foreach (var i in _lDThuocct)
                                    {
                                        NhapDct _xuatct = new NhapDct();
                                        _xuatct.IDNhap = id;
                                        _xuatct.MaDV = i.MaDV;
                                        _xuatct.DonVi = i.DonVi;
                                        _xuatct.DonGiaX = i.DonGia;
                                        if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                                        {
                                            _xuatct.DonGia = DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// laays don gia nhap
                                            _xuatct.DonGiaX = i.DonGia;
                                        }
                                        else
                                        {
                                            _xuatct.DonGia = i.DonGia;
                                        }

                                        _xuatct.MaCC = i.MaCC;
                                        _xuatct.SoLuongX = i.SoLuong;
                                        _xuatct.ThanhTienX = Math.Round(i.DonGia * i.SoLuong, 3);
                                        _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, i.SoLuong, makho);
                                        _xuatct.ThanhTienDY = Math.Round(_xuatct.SoLuongDY * i.DonGia, 3);
                                        _xuatct.SoLuongN = 0;
                                        _xuatct.ThanhTienN = 0;
                                        _xuatct.SoLuongSD = 0;
                                        _xuatct.ThanhTienSD = 0;
                                        _xuatct.SoLuongKK = 0;
                                        _xuatct.ThanhTienKK = 0;
                                        _xuatct.SoLo = i.SoLo;
                                        _xuatct.HanDung = i.HanDung;
                                        _xuatct.TrongBH = i.TrongBH;
                                        _xuatct.IDDTBN = IdDTBN;
                                        _xuatct.MaBNhan = Convert.ToInt32(txtMaBNhan.Text);
                                        _dataContext.NhapDcts.Add(_xuatct);
                                        listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });
                                    }
                                    if (_lDThuocct.Count > 0)
                                        _dataContext.SaveChanges();
                                    if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0) // tét
                                    {
                                        DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0);
                                    }
                                }

                                //        trans.Commit();

                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        trans.Rollback();
                                //        MessageBox.Show("Lỗi quá trình: " + ex.Message);

                                //    }
                                //}
                            }
                        }
                        else
                        {
                            NhapD _xuat = new NhapD();

                            _xuat.MaBNhan = _maBN;
                            _xuat.SoCT = txtIDDon.Text;
                            _xuat.SoPL = Convert.ToInt32(txtIDDon.Text);
                            _xuat.PLoai = 2;

                            //if (kieudon == 6)
                            //{
                            //    _xuat.KieuDon = 4;
                            //    if(DungChung.Bien.MaBV == "27183")
                            //        _xuat.KieuDon = 0;
                            //}
                            //else
                            _xuat.KieuDon = 0;

                            _xuat.NgayNhap = _ngayke;
                            _xuat.XuatTD = 1;
                            _xuat.MaKP = makho;
                            _xuat.MaCB = DungChung.Bien.MaCB;
                            _xuat.SoPhieu = DungChung.Ham._idphieutheokp(2, Convert.ToInt32(makho));
                            var rv = _dataContext.RaViens.Where(p => p.MaKP != null && p.MaBNhan == _maBN).Select(p => p.MaKP).ToList();
                            if (rv.Count > 0)
                                _xuat.MaKPnx = rv.First();
                            else if (_lDThuocct.Count > 0 && _lDThuocct.First() != null)
                                _xuat.MaKPnx = _lDThuocct.First().MaKP;
                            _dataContext.NhapDs.Add(_xuat);
                            if (_dataContext.SaveChanges() >= 0)
                            {
                                int idDon = Convert.ToInt32(txtIDDon.Text);
                                var dthuoc = _dataContext.DThuocs.Where(p => p.IDDon == idDon).FirstOrDefault();
                                if (dthuoc != null)
                                {
                                    dthuoc.MaCBXuat = DungChung.Bien.MaCB;
                                    _dataContext.SaveChanges();
                                }
                                //DThuoc dt1 = new DThuoc();

                                int iddt = Convert.ToInt32(txtIDDon.Text);

                                var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddt).ToList();
                                foreach (var item in dtct)
                                {
                                    item.Status = 1;
                                }
                                try
                                {
                                    _dataContext.SaveChanges();
                                }
                                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                                {
                                    Exception raise = dbEx;

                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            string message = string.Format("{0}:{1}",

                                              validationErrors.Entry.Entity.ToString(),

                                                validationError.ErrorMessage);

                                            // raise a new exception nesting

                                            // the current instance as InnerException

                                            raise = new InvalidOperationException(message, raise);
                                        }
                                    }

                                    throw raise;
                                }
                                //_dataContext.SaveChanges();0
                                int id = _xuat.IDNhap;
                                List<DichVu> listDichVus = new List<DichVu>();
                                foreach (var i in _lDThuocct)
                                {
                                    NhapDct _xuatct = new NhapDct();
                                    _xuatct.IDNhap = id;
                                    _xuatct.MaDV = i.MaDV;
                                    _xuatct.DonVi = i.DonVi;
                                    _xuatct.DonGiaX = i.DonGia;
                                    if (DungChung.Bien.MaBV == "30340" || radNgoaiDM.SelectedIndex == 0)// || DungChung.Bien.MaBV == "01071"
                                    {
                                        _xuatct.DonGia = i.DonGia; //DungChung.Ham._getGiaSD(_dataContext, i.MaDV ?? 0, i.DonGia, i.TrongBH, 1, makho, DateTime.Now);// laays don gia nhap
                                        _xuatct.DonGiaX = i.DonGia;
                                    }
                                    else
                                    {
                                        _xuatct.DonGia = i.DonGia;
                                    }

                                    _xuatct.MaCC = i.MaCC;
                                    _xuatct.SoLuongX = i.SoLuong;
                                    _xuatct.ThanhTienX = Math.Round(i.DonGia * i.SoLuong, 3);
                                    _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, i.MaDV == null ? 0 : i.MaDV.Value, i.SoLuong, makho);
                                    _xuatct.ThanhTienDY = Math.Round(_xuatct.SoLuongDY * i.DonGia, 3);
                                    _xuatct.SoLuongN = 0;
                                    _xuatct.ThanhTienN = 0;
                                    _xuatct.SoLuongSD = 0;
                                    _xuatct.ThanhTienSD = 0;
                                    _xuatct.SoLuongKK = 0;
                                    _xuatct.ThanhTienKK = 0;
                                    _xuatct.SoLo = i.SoLo;
                                    _xuatct.HanDung = i.HanDung;
                                    _xuatct.TrongBH = i.TrongBH;
                                    _xuatct.IDDTBN = IdDTBN;
                                    _xuatct.MaBNhan = Convert.ToInt32(txtMaBNhan.Text);
                                    _dataContext.NhapDcts.Add(_xuatct);
                                    listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });
                                }
                                if (_lDThuocct.Count > 0)
                                    _dataContext.SaveChanges();
                                if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0)
                                {
                                    DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0); // tét
                                }
                            }

                            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);

                            int makho1 = 0;
                            if (lupTimMaKP.EditValue != null)
                                makho1 = Convert.ToInt32(lupTimMaKP.EditValue);

                            load();
                        }
                    }
                }
            }
        }

        private void grvBenhNhankd_DataSourceChanged(object sender, EventArgs e)
        {
            grvBenhNhankd_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm tên|Mã BN")
                txtTimKiem.Text = "";
        }

        private void txtTimKiemxd_Leave(object sender, EventArgs e)
        {
        }

        private void txtTimKiemxd_Click(object sender, EventArgs e)
        {
            if (txtTimKiemxd.Text == "Tìm tên|Mã BN")
                txtTimKiemxd.Text = "";
        }

        //sophieulinh
        private bool XoaXuat(int _id, int _mbn)
        {
            try
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var xoac = _dataContext.NhapDs.Single(p => p.SoPL == (_id));

                int idnhap = xoac.IDNhap;

                var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == idnhap).ToList();
                foreach (var xoa in xoact)
                {
                    var _xoa = _dataContext.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                    _dataContext.NhapDcts.Remove(_xoa);
                }
                if (xoact.Count > 0)
                    _dataContext.SaveChanges();
                int iddon = 0;

                iddon = xoac.SoPL ?? 0;
                _dataContext.NhapDs.Remove(xoac);
                _dataContext.SaveChanges();

                var dthuocct = _dataContext.DThuoccts.Where(p => p.IDDon == iddon && p.Status != -1).ToList();
                var listDichVus = dthuocct.Select(o => new DichVu { MaDV = (o.MaDV ?? 0) }).ToList();
                foreach (var item in dthuocct)
                {
                    item.Status = 0;
                }

                if (listDichVus.Count > 0 && DungChung.Bien.MaBV == "200012") //tét
                {
                    DungChung.Ham.UpdateTonDichVu(listDichVus, xoac.MaKP ?? 0, true);
                }

                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dtNgayXuat_Leave(object sender, EventArgs e)
        {
            _ngayke = dtNgayXuat.DateTime;
        }

        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }

        public bool _ktmatkhau = false;

        private void grvBNhanxd_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoaXuat")
            {
                int id = 0;
                if (grvBNhanxd.GetFocusedRowCellValue(colIDNhap) != null && grvBNhanxd.GetFocusedRowCellValue(colIDNhap).ToString() != "")
                {
                    id = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colIDNhap));
                    string tenbnxd = "";
                    if (grvBNhanxd.GetFocusedRowCellValue(colTenBNhanxd) != null && grvBNhanxd.GetFocusedRowCellValue(colTenBNhanxd).ToString() != "")
                        tenbnxd = grvBNhanxd.GetFocusedRowCellValue(colTenBNhanxd).ToString();
                    var kt = _dataContext.NhapDs.Where(p => p.SoPL == id).ToList();
                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        DialogResult _result;
                        _result = MessageBox.Show("Bạn muốn xóa xuất dược của BN: " + tenbnxd, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            _ktmatkhau = false;
                            if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24272")
                            {
                                _ktmatkhau = true;
                            }
                            else
                            {
                                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                frm.ShowDialog();
                            }
                            int _mbn = 0;
                            if (grvBNhanxd.GetFocusedRowCellValue(colMaBNhanxd) != null)
                                _mbn = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colMaBNhanxd));

                            if (_ktmatkhau)
                            {
                                if (XoaXuat(id, _mbn))
                                {
                                    MessageBox.Show("Đã xóa!");
                                    load();
                                }
                                else
                                    MessageBox.Show("Xóa không thành công!");

                                //frm_Check frm = new frm_Check(id, _mbn, 1, true);
                            }
                            idmabn = _mbn;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void grvBNhanxd_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (grvBNhanxd.GetFocusedRowCellValue(colIDNhap) != null && grvBNhanxd.GetFocusedRowCellValue(colIDNhap).ToString() != "")
                {
                    int id = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colIDNhap));
                    frm_XemXuatDuoc frm = new frm_XemXuatDuoc(id, grvBNhanxd.GetFocusedRowCellDisplayText(colTenBNhanxd));
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (chkHuydon.Checked == false)
            {
                if (!string.IsNullOrEmpty(txtIDDon.Text))
                {
                    DialogResult _resul = MessageBox.Show("Bạn muốn hủy đơn của BN: " + txtTenBnhan.Text, "Hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_resul == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(txtIDDon.Text);
                        frm_Check frm = new frm_Check(id, 6);
                        frm.ShowDialog();
                        load();
                    }
                }
            }
            else
            {
                MessageBox.Show("Đơn đã hủy");
                load();
            }
        }

        private void chkHuydon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHuydon.Checked)
                btnHuyDon.Enabled = false;
            else
                btnHuyDon.Enabled = true;
        }

        private void chkBNKD_CheckedChanged(object sender, EventArgs e)
        {
            if ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789") && chkBNKD.CheckState == CheckState.Checked)
                panel1.Visible = true;
            else
                panel1.Visible = false;
            if (chkBNKD.Checked && DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "30372" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                btnXuatDuoc.Enabled = false;
            else
                btnXuatDuoc.Enabled = true;
        }

        private List<DungChung.Ham.ThuocTon> _lthuocton = new List<DungChung.Ham.ThuocTon>();

        private void txtTonDuoc_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            _lthuocton = DungChung.Ham.KiemTraTonDuoc(_dataContext, _makp);
            TraCuu.frm_DSTHuocAm frm = new TraCuu.frm_DSTHuocAm(_lthuocton);
            frm.ShowDialog();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            FormTraCuu.FrmTC_NhapXuatTon frm = new FormTraCuu.FrmTC_NhapXuatTon();
            frm.ShowDialog();
        }

        private void grpChuaKham_Paint(object sender, PaintEventArgs e)
        {
        }

        public void PhieuXuat_NhaThuoc_27022(int IDDon)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.Rep_PhieuXuatThuoc_NhaThuoc_27022 rep = new BaoCao.Rep_PhieuXuatThuoc_NhaThuoc_27022();
            frmIn frm = new frmIn();
            var dthuoc = (from a in _dataContext.DThuocs.Where(p => p.IDDon == IDDon)
                          join b in _dataContext.DThuoccts on a.IDDon equals b.IDDon
                          join c in _dataContext.DichVus on b.MaDV equals c.MaDV
                          select new
                          {
                              a.MaBNhan,
                              b.MaDV,
                              c.TenDV,
                              c.DonVi,
                              b.DonGia,
                              b.SoLuong,
                              b.ThanhTien
                          }).ToList();
            if (dthuoc.Count > 0)
            {
                double tong = dthuoc.Sum(p => p.ThanhTien);
                int _mabn = dthuoc.Max(p => p.MaBNhan ?? 0);
                var bn = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                          join b in _dataContext.BNKBs on a.MaBNhan equals b.MaBNhan
                          select new
                          {
                              a.TenBNhan,
                              a.Tuoi,
                              a.GTinh,
                              a.DChi,
                              b.IDKB,
                              b.ChanDoan,
                              b.BenhKhac
                          }).ToList();
                rep.TienBangChu.Value = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tong, " đồng./.");
                if (bn.Count > 0)
                {
                    int idkb = bn.Max(p => p.IDKB);
                    bn = bn.Where(p => p.IDKB == idkb).ToList();
                    rep.TenBN.Value = bn.First().TenBNhan;
                    rep.Tuoi.Value = bn.First().Tuoi;
                    if (bn.First().GTinh == 0)
                        rep.Gtinh.Value = "Nữ";
                    else rep.Gtinh.Value = "Nam";
                    rep.DChi.Value = bn.First().DChi;
                    if (bn.First().ChanDoan != null || bn.First().BenhKhac != null)
                        rep.ChanDoan.Value = "Chẩn đoán: " + bn.First().ChanDoan + "; " + bn.First().BenhKhac;
                    else rep.ChanDoan.Value = "Chẩn đoán: ";
                    rep.NgayXuat.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                }
            }

            rep.DataSource = dthuoc;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int iddon = -1;
            if (grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDon) != null || grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDon) != "")
            {
                iddon = int.Parse(grvBenhNhankd.GetFocusedRowCellDisplayText(colIDDon).ToString());
                PhieuXuat_NhaThuoc_27022(iddon);
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int rs;
            int id = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                id = Convert.ToInt32(txtMaBNhan.Text);
            int makp = 0;
            if (lupTimMaKP.Text != "")
                makp = Convert.ToInt32(lupTimMaKP.EditValue);
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == id)
                      join b in _dataContext.DThuocs.Where(p => (radNgoaiDM.SelectedIndex == 1 ? p.KieuDon != 6 : true) && (p.MaKXuat == makp || makp == 0)) on a.MaBNhan equals b.MaBNhan
                      join d in _dataContext.DThuoccts on b.IDDon equals d.IDDon
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on d.MaDV equals c.MaDV
                      select new
                      {
                          b.IDDon,
                          b.MaKP,
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          d.DonVi,
                          d.SoLuong,
                          d.DonGia,
                          d.ThanhTien,
                          c.NuocSX,
                          d.MaKXuat
                      }).ToList();
            var x1 = (from a in dt
                      group a by new { a.IDDon, a.MaKP, a.MaKXuat } into kq
                      select new { kq.Key.IDDon, kq.Key.MaKP, kq.Key.MaKXuat }).OrderBy(p => p.IDDon).ToList();
            int minid = x1.Min(p => p.IDDon);
            int makpx = x1.Where(p => p.IDDon == minid).First().MaKXuat ?? 0;
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPhatThuoc rep = usXuatNgoaiTru.innhieudon(_dataContext, id, minid, makpx);
            foreach (var item in x1.Where(p => p.IDDon != minid))
            {
                BaoCao.rep_PhieuPhatThuoc rep2 = usXuatNgoaiTru.innhieudon(_dataContext, id, item.IDDon, item.MaKXuat ?? 0);
                rep.Pages.AddRange(rep2.Pages);
            }
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        public static BaoCao.rep_PhieuPhatThuoc innhieudon(QLBV_Database.QLBVEntities _dataContext, int _Mabn, int IDDon, int MaKPX)
        {
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == _Mabn)
                      join b in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.IDDon == IDDon) on a.MaBNhan equals b.MaBNhan
                      join d in _dataContext.DThuoccts.Where(p => p.MaKXuat == MaKPX) on b.IDDon equals d.IDDon
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on d.MaDV equals c.MaDV
                      select new
                      {
                          b.IDDon,
                          b.MaKP,
                          b.MaKXuat,
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          d.DonVi,
                          d.SoLuong,
                          d.DonGia,
                          d.ThanhTien,
                          c.NuocSX,
                      }).ToList();
            int makp2 = dt.FirstOrDefault().MaKP ?? 0;
            var bnkb = (from a in _dataContext.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp2)
                        group a by new { a.MaBNhan } into kq
                        select new
                        {
                            kq.Key.MaBNhan,
                            IDBK = kq.Max(p => p.IDKB),
                            ChanDoan = "",
                            ChanDoanKhac = "",
                        }).ToList();
            var makp = (from a in bnkb
                        join b in _dataContext.BNKBs on a.IDBK equals b.IDKB
                        join c in _dataContext.KPhongs on b.MaKP equals c.MaKP
                        join d in _dataContext.RaViens on a.MaBNhan equals d.MaBNhan
                        select new { c.TenKP, c.MaKP, b.ChanDoan, b.BenhKhac }).ToList();
            BaoCao.rep_PhieuPhatThuoc rep = new BaoCao.rep_PhieuPhatThuoc();
            var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == IDDon).ToList();
            rep.TongKhoan.Value = dt.Count().ToString();
            rep.TongTien.Value = dt.Sum(p => p.ThanhTien).ToString("###,###.00") + " đồng";
            rep.TenKP.Value = makp.FirstOrDefault().TenKP;
            int makp1 = dtct.FirstOrDefault().MaKXuat ?? 0;
            rep.TenKho.Value = _dataContext.KPhongs.Where(p => p.MaKP == makp1).FirstOrDefault().TenKP;
            rep.ChanDoan.Value = makp.FirstOrDefault().ChanDoan;
            rep.BenhKhac.Value = makp.FirstOrDefault().BenhKhac;
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList().FirstOrDefault();
            rep._MaBNhan.Value = bn.MaBNhan.ToString();
            rep._TenBNhan.Value = bn.TenBNhan;
            rep.Tuoi.Value = bn.Tuoi;
            rep.GTinh.Value = bn.GTinh == 0 ? "Nữ" : "Nam";
            rep.DiaChi.Value = bn.DChi;
            rep.SThe.Value = bn.SThe;
            rep.GT.Value = bn.HanBHTu.Value.ToString("dd/MM/yyyy");
            rep.Den.Value = bn.HanBHDen.Value.ToString("dd/MM/yyyy");
            rep.DT.Value = bn.DTuong;
            rep.ngayke.Value = DateTime.Now;
            if (dt.Count > 0)
            {
                rep.DataSource = dt.ToList();
                rep.BindData();
                rep.CreateDocument();
            }
            return rep;
        }

        private void grvBNhanxd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvBNhanxd.GetFocusedRowCellValue(colMaBNhanxd) != null)
                idmabn = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colMaBNhanxd));
            var test = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == idmabn && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ") && p.NoiTru == 0 && p.DTNT == false)
                        join b in _dataContext.RaViens on a.MaBNhan equals b.MaBNhan
                        join c in _dataContext.VienPhis on a.MaBNhan equals c.MaBNhan
                        select new { a.MaBNhan, a.TenBNhan }).ToList();
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == idmabn)
                      join b in _dataContext.VienPhicts on a.idVPhi equals b.idVPhi
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1) on b.MaDV equals c.MaDV
                      select new
                      {
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          b.DonVi,
                          b.SoLuong,
                          b.DonGia,
                          b.ThanhTien,
                          c.NuocSX,
                      }).ToList();
            if (test.Count > 0 && dt.Count > 0)
            {
                simpleButton2.Enabled = true;
            }
            else
            {
                simpleButton2.Enabled = false;
            }
        }

        private void GrvBNhanxd_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) //minhvd
        {
            if (grvBNhanxd.GetRowCellValue(e.RowHandle, colIdDon2) != null && DungChung.Bien.MaBV == "24012")
            {
                int i = Convert.ToInt32(grvBNhanxd.GetRowCellValue(e.RowHandle, colIdDon2));
                var check = _dataContext.DThuoccts.Where(p => p.IDDon == i).ToList();

                if (check.Count > 0)
                {
                    if (check.First().SoLuong < 0)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
                    }
                }
                if (grvBNhanxd.GetRowCellValue(e.RowHandle, colKieuDon) != null)
                {
                    if (grvBNhanxd.GetRowCellValue(e.RowHandle, colKieuDon1).ToString() == "2")
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void txtTimKiemxd_EditValueChanged(object sender, EventArgs e)
        {
            LoadData(dt, txtTimKiemxd.Text, grcBNhanxd, 1);
        }

        private int? sovv = null;

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_idkd > 0)
            {
                if (sovv == -1)
                    MessageBox.Show("Đơn thuốc đã được duyệt, bạn không thể sửa");
                else
                {
                    btnSua.Enabled = false;
                    btnDuyet.Enabled = false;
                    btnXoa.Enabled = true;
                    btnLuu.Enabled = true;
                    grvDonThuocct.OptionsBehavior.ReadOnly = false;
                    grvDonThuocct.OptionsBehavior.Editable = true;
                    grvDonThuocct.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                    grvDonThuocct.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                }
            }
            else
                MessageBox.Show("Bạn chưa chọn đơn");
        }

        private List<DichVu> _lThuoc = new List<DichVu>();
        private string _dTuongBN = "";

        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int madv = 0;
            switch (e.Column.Name)
            {
                case "colMaDV":

                    #region colMaDV

                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null && grvDonThuocct.GetFocusedRowCellValue(colMaDV).ToString() != "")
                    {
                        madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDV));

                        // hiện dường dùng
                        double tyleBHTT = 100;
                        int _trongBH = 0;
                        if (_dTuongBN == "BHYT")
                        {
                            var trongBH = _lThuoc.Where(p => p.MaDV == madv).ToList();
                            if (trongBH.Count > 0)
                            {
                                tyleBHTT = trongBH.First().BHTT ?? 100;
                                _trongBH = trongBH.First().TrongDM.Value;
                            }
                        }
                        var query = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1 && (_maKhoXuat == 0 ? true : p.MaKP == _maKhoXuat) && p.KieuDon != 2)
                                     from ndct in _dataContext.NhapDcts.Where(p => p.MaDV == madv)
                                         .Where(p => p.IDNhap == nd.IDNhap)
                                         .OrderByDescending(o => o.IDNhapct).Take(1)
                                     select new
                                     {
                                         nd.IDNhap,
                                         DonGia = ndct != null ? ndct.DonGia : 0,
                                         DonVi = ndct != null ? ndct.DonVi : "",
                                         SoLo = ndct != null ? ndct.SoLo : "",
                                         HanDung = ndct != null ? ndct.HanDung : null
                                     }).FirstOrDefault();
                        grvDonThuocct.SetFocusedRowCellValue(colTyLeTT, tyleBHTT);
                        grvDonThuocct.SetFocusedRowCellValue(colTrongBH, _trongBH);
                        if (query != null)
                        {
                            grvDonThuocct.SetFocusedRowCellValue(colDonVi, query.DonVi);
                            grvDonThuocct.SetFocusedRowCellValue(colDonGia, query.DonGia);
                            grvDonThuocct.SetFocusedRowCellValue(colSoLo, query.SoLo);
                            grvDonThuocct.SetFocusedRowCellValue(colHanDung, query.HanDung);
                        }

                        double ton = DungChung.Ham._checkTon_KD(_dataContext, madv, _maKhoXuat, query.DonGia, 0, query.SoLo);
                        groupControl1.Text = "Tồn : " + ton.ToString("###.##");
                    }

                    break;

                #endregion colMaDV

                case "colDonGia":

                    #region colDonGia

                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null && grvDonThuocct.GetFocusedRowCellValue(colMaDV).ToString() != "")
                    {
                        double dongia = 0, soluong = 0, thanhtien = 0;
                        if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null)
                            dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null)
                            soluong = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuong));
                        if (dongia > 0 && soluong > 0)
                        {
                            thanhtien = (double)dongia * (double)soluong;
                            grvDonThuocct.SetFocusedRowCellValue(colThanhTienCT, thanhtien);
                        }
                    }

                    break;

                #endregion colDonGia

                case "colSoLuong":

                    #region colSoLuong

                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null && grvDonThuocct.GetFocusedRowCellValue(colMaDV).ToString() != "")
                    {
                        madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDV));
                        double dongia = 0, soluong = 0, thanhtien = 0;
                        if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null)
                            dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null)
                            soluong = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuong));
                        if (dongia > 0 && soluong > 0)
                        {
                            thanhtien = (double)dongia * (double)soluong;
                            grvDonThuocct.SetFocusedRowCellValue(colThanhTienCT, thanhtien);
                        }

                        #region tính tồn

                        double tongSL = 0; // tổng số lượng kê
                        for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                        {
                            if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDV) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colMaDV).ToString() != "" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                            {
                                if (Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colMaDV)) == madv)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                    {
                                        double sl = 0, dg = 0;
                                        sl = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                        dg = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                        tongSL = tongSL + sl;
                                    }
                                }
                            }
                        }
                        _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        var qsua = _dataContext.DThuoccts.Where(p => p.IDDon == _idkd && p.MaDV == madv && p.DonGia == dongia).ToList();
                        double sluongSua = 0;
                        if (qsua.Count > 0)
                            sluongSua = qsua.Sum(p => p.SoLuong);
                        double ton = DungChung.Ham._checkTon_KD(_dataContext, madv, _maKhoXuat, dongia, 0, grvDonThuocct.GetFocusedRowCellDisplayText(colSoLo) ?? "");
                        ton = ton + sluongSua - tongSL - soluong;
                        groupControl1.Text = "Tồn : " + ton.ToString("###.##");
                        if (ton < 0)
                            grvDonThuocct.SetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLuong, 0);

                        #endregion tính tồn
                    }

                    break;

                #endregion colSoLuong

                case "colTrongBH":

                    #region colTrongBH

                    bool kt = true;
                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDV) != null && grvDonThuocct.GetFocusedRowCellValue(colMaDV).ToString() != "")
                    {
                        if (grvDonThuocct.GetFocusedRowCellValue(colTrongBH) != null && grvDonThuocct.GetFocusedRowCellValue(colTrongBH).ToString() != "")
                        {
                            int trongBH = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colTrongBH));
                            if (trongBH == 1)
                            {
                                if (_dTuongBN != "BHYT")
                                    kt = false;
                                else
                                {
                                    madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDV));
                                    var qdv = _lThuoc.Where(p => p.MaDV == madv).FirstOrDefault();
                                    if (qdv != null && qdv.TrongDM == 1)
                                        kt = true;
                                    else
                                        kt = false;
                                }
                            }
                        }
                    }
                    if (!kt)
                    {
                        MessageBox.Show("Thuốc/ vật tư không được sửa thành trong danh mục");
                        grvDonThuocct.SetFocusedRowCellValue(colTrongBH, 0);
                    }
                    break;

                    #endregion colTrongBH
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool ktra = true;
            string mssMaDV = "";
            string mssSL = "Các thuốc";
            string mssDonGia = "Các thuốc";
            for (int i = 0; i < grvDonThuocct.RowCount; i++)
            {
                if (grvDonThuocct.GetRowCellValue(i, colMaDV) != null && grvDonThuocct.GetRowCellValue(i, colMaDV).ToString() != "" && grvDonThuocct.GetRowCellValue(i, colMaDV).ToString() != "0")
                {
                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0")
                    {
                        if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "" && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "0")
                        {
                        }
                        else
                        {
                            ktra = false;
                            mssDonGia += grvDonThuocct.GetRowCellDisplayText(i, colMaDV);
                        }
                    }
                    else
                    {
                        ktra = false;
                        mssSL += grvDonThuocct.GetRowCellDisplayText(i, colMaDV);
                    }
                }
            }
            if (ktra)
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qdtctsua = _dataContext.DThuoccts.Where(p => p.IDDon == _idkd).FirstOrDefault();
                for (int i = 0; i < grvDonThuocct.RowCount; i++)
                {
                    if (grvDonThuocct.GetRowCellValue(i, colMaDV) != null && grvDonThuocct.GetRowCellValue(i, colMaDV).ToString() != "")
                    {
                        int iddtct = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString() != "")
                            iddtct = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct));
                        int madv = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDV));
                        double dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                        double sl = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                        double thanhtien = (double)dongia * (double)sl;
                        string solo = grvDonThuocct.GetRowCellDisplayText(i, colSoLo);
                        string donvi = grvDonThuocct.GetRowCellDisplayText(i, colDonVi);
                        int trongbh = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colTrongBH) != null && grvDonThuocct.GetRowCellValue(i, colTrongBH).ToString() != "")
                            trongbh = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colTrongBH));
                        int tylett = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colTyLeTT) != null && grvDonThuocct.GetRowCellValue(i, colTyLeTT).ToString() != "")
                            tylett = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colTyLeTT));
                        DateTime? handung = null;
                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                            handung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                        if (iddtct > 0)
                        {
                            DThuocct dt = _dataContext.DThuoccts.Single(p => p.IDDonct == iddtct);
                            dt.MaDV = madv;
                            dt.SoLuong = sl;
                            dt.SoLuongct = sl;
                            dt.DonGia = dongia;
                            dt.SoLo = solo;
                            dt.DonVi = donvi;
                            dt.ThanhTien = thanhtien;
                            dt.TrongBH = trongbh;
                            dt.TyLeTT = tylett;
                            dt.HanDung = handung;

                            _dataContext.SaveChanges();
                        }
                        else
                        {
                            DThuocct dt = new DThuocct();
                            dt.MaDV = madv;
                            dt.SoLuong = sl;
                            dt.SoLuongct = sl;
                            dt.DonGia = dongia;
                            dt.SoLo = solo;
                            dt.DonVi = donvi;
                            dt.ThanhTien = thanhtien;
                            dt.TrongBH = trongbh;
                            dt.TyLeTT = tylett;
                            dt.HanDung = handung;
                            dt.IDDon = _idkd;
                            dt.Status = 0;
                            if (qdtctsua != null)
                            {
                                dt.IDKB = qdtctsua.IDKB;
                                dt.Loai = qdtctsua.Loai;
                                dt.LoaiDV = qdtctsua.LoaiDV;
                                dt.MaCB = qdtctsua.MaCB;
                                dt.MaKP = qdtctsua.MaKP;
                                dt.MaKPtk = qdtctsua.MaKPtk;
                                dt.MaKXuat = qdtctsua.MaKXuat;
                                dt.Mien = qdtctsua.Mien;
                                dt.NgayNhap = qdtctsua.NgayNhap;
                                dt.ThanhToan = qdtctsua.ThanhToan;
                                dt.XHH = qdtctsua.XHH;
                            }
                            _dataContext.DThuoccts.Add(dt);
                            _dataContext.SaveChanges();
                        }
                    }
                }
                MessageBox.Show("Cập nhật thành công");
                btnSua.Enabled = true;
                btnDuyet.Enabled = true;
                btnLuu.Enabled = false;
                btnXoa.Enabled = false;
                grvDonThuocct.OptionsBehavior.ReadOnly = true;
                grvDonThuocct.OptionsBehavior.Editable = false;
                grvDonThuocct.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                grvDonThuocct.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            }
            else
            {
                if (mssSL != "Các thuốc")
                {
                    mssMaDV = mssSL + "chưa nhập số lượng";
                }
                if (mssDonGia != "Các thuốc")
                {
                    mssMaDV = mssMaDV + "\n" + mssDonGia + "chưa nhập đơn giá";
                }
                MessageBox.Show(mssMaDV);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "0")
            {
                string thuoc = grvDonThuocct.GetFocusedRowCellDisplayText(colMaDV);
                DialogResult result;
                result = MessageBox.Show("Bạn muốn xóa thuốc '" + thuoc + "' ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int iddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                    DThuocct dt = _dataContext.DThuoccts.Single(p => p.IDDonct == iddonct);
                    _dataContext.DThuoccts.Remove(dt);
                    _dataContext.SaveChanges();
                    grvDonThuocct.DeleteRow(grvDonThuocct.FocusedRowHandle);
                }
            }
            else
                grvDonThuocct.DeleteRow(grvDonThuocct.FocusedRowHandle);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (btnDuyet.Text == "Duyệt")
            {
                DThuoc dt = _dataContext.DThuocs.Where(p => p.IDDon == _idkd).FirstOrDefault();
                if (dt != null)
                {
                    if (dt.SoVV != -1)
                    {
                        dt.SoVV = -1;
                        if (_dataContext.SaveChanges() > 0)
                        {
                            MessageBox.Show("Duyệt thành công");
                            btnDuyet.Text = "Hủy duyệt";
                            sovv = -1;
                        }
                    }
                    else
                        MessageBox.Show("Đơn thuốc đã được duyệt");
                }
                else
                    MessageBox.Show("Không tìm được đơn thuốc");
            }
            else if (btnDuyet.Text == "Hủy duyệt")
            {
                DThuoc dt = _dataContext.DThuocs.Where(p => p.IDDon == _idkd).FirstOrDefault();
                if (dt != null)
                {
                    int mabn = dt.MaBNhan ?? 0;
                    var kttt = _dataContext.VienPhis.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                    if (kttt != null)
                    {
                        MessageBox.Show("Bệnh nhân đã thanh toán, không thể hủy duyệt!");
                    }
                    else
                    {
                        if (dt.SoVV == -1)
                        {
                            dt.SoVV = null;

                            if (_dataContext.SaveChanges() > 0)
                            {
                                MessageBox.Show("Hủy duyệt thành công");
                                btnDuyet.Text = "Duyệt";
                                sovv = null;
                            }
                        }
                        else
                            MessageBox.Show("Đơn thuốc chưa được duyệt");
                    }
                }
                else
                    MessageBox.Show("Không tìm được đơn thuốc");
            }
        }

        private void btnInPhieuPT_Click(object sender, EventArgs e)
        {
            if (sovv == -1)
            {
                FormNhap.usKhamBenh frNhap = new FormNhap.usKhamBenh();
                frNhap.InPhieu(_mabn);
            }
            else
            {
                MessageBox.Show("Đơn thuốc chưa được duyệt");
            }
        }

        private void grcBNhanxd_Click(object sender, EventArgs e)
        {
        }

        private void load()
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);

            if (chkBNKD.Checked)
            {
                btnXuatDuoc.Enabled = false;
            }
            else
            {
                btnXuatDuoc.Enabled = true;
            }
            bool DaHuy = chkHuydon.Checked;
            bool donTra = ckcDonTra.Checked;
            bool BNCTT = chkBNKD.Checked;
            int makho1 = 0;
            if (lupTimMaKP.EditValue != null)
                makho1 = Convert.ToInt32(lupTimMaKP.EditValue);

            SearchModel key = new SearchModel();
            key.TuNgay = dtTimTuNgay.DateTime;
            key.DenNgay = dtTimDenNgay.DateTime;
            key.HoTen = txtTimKiem.Text;
            key.MaKP = makho1;
            key.DaHuy = DaHuy;
            key.TrongDM = radNgoaiDM.SelectedIndex;
            key.DonTra = donTra;
            dt = dsDuoc(key);
            if (DungChung.Bien.MaBV == "24012")
            {
                if (donTra)
                {
                    dt = dt.ToList();
                }
                else
                {
                    dt = dt.Where(p => p.DonTra == false).ToList();
                }
            }
            else
            {
                if (donTra)
                {
                    dt = dt.Where(p => p.DonTra).ToList();
                }
            }
            if (BNCTT)
            {
                dt = dt.Where(p => p.SttBN != 3).ToList();
            }
            if (dt.Count() > 0)
                LoadData(dt);
            else
            {
                LoadData(dt);
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        private void ckcDonTra_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            if (chkHuydon.Checked)
                LoadData(dt, txtTimKiem.Text, grcBenhNhankd, 2);
            else
                LoadData(dt, txtTimKiem.Text, grcBenhNhankd, 0);
        }

        private void grvBenhNhankd_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int idmabn = grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan) == null ? 0 : Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan));
            //
            string _T = grvBenhNhankd.GetFocusedRowCellValue(colTenBNhan).ToString();
            if (dt.Where(p => p.MaBNhan == idmabn).Count() > 0)
            {
                var sua = dt.Where(p => p.MaBNhan == idmabn).First();
                if (e.Column == colChon) // Nếu click vào cột Chọn
                {
                    if (sua.Chon == false)
                    {
                        sua.Chon = true;
                    }
                    else if (sua.Chon == true)
                    {
                        sua.Chon = false;
                    }
                }
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (grvBenhNhankd.RowCount > 0)
            {
                for (int i = 0; i < grvBenhNhankd.RowCount; i++)
                {
                    grvBenhNhankd.SetRowCellValue(i, colChon, chkSelectAll.Checked);
                }
            }
        }
    }
}