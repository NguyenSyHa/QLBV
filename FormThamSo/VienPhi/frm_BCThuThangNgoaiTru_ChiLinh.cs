using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using OpenXmlPackaging;
using QLBV.DungChung;
using COMExcel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Xml.Linq;

namespace QLBV.FormThamSo
{
    /// <summary>
    ///A. Xuất XML: Bảng 2,3 thiếu ma_bac_si (Bác sỹ chỉ định theo chứng chỉ hành nghề); ngày_yl: ngày ra y lệnh : yyyymmddHHmm; Bảng 3 thiếu ngay_kq: ngày có kết quả( định dạng như ngay_yl)
    /// Bảng 2: Biểu 20; Bảng 3 tách ra làm biểu 19( VTYT) và biểu 21 (dịch vụ)
    ///B. Báo cáo thống kê thuốc theo đối tượng : sử dụng cho Tam đường
    /// </summary>
    public partial class frm_BCThuThangNgoaiTru_ChiLinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThuThangNgoaiTru_ChiLinh()
        {
            InitializeComponent();
        }
        int _mauso = 20, Font = 0;
        public frm_BCThuThangNgoaiTru_ChiLinh(int mau)
        {
            _mauso = mau;
            InitializeComponent();
        }
        private bool KTtaoBcMau20()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }

        //private string theoquy()
        //{
        //    string quy = "";

        //    if (ckQuy.Checked == true)
        //    {
        //        switch (timquy(lupTuNgay.DateTime.Month))
        //        {
        //            case 1:
        //                quy = "THÁNG " + lupTuNgay.DateTime.Month + " QUÝ I NĂM 2015";
        //                break;
        //            case 2:
        //                quy = "THÁNG " + lupTuNgay.DateTime.Month + " QUÝ II NĂM 2015 ";
        //                break;
        //            case 3:
        //                quy = "THÁNG " + lupTuNgay.DateTime.Month + " QUÝ III NĂM 2015";
        //                break;
        //            case 4:
        //                quy = "THÁNG " + lupTuNgay.DateTime.Month + " QUÝ IV NĂM 2015";
        //                break;
        //        }

        //    }
        //    else
        //    {
        //        quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
        //    }
        //    return quy;
        //}

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int load = 0;
        List<NhomDV> _listNhomDV = new List<NhomDV>();
        private void frmTsBcMau20_1399_Load(object sender, EventArgs e)
        {
            rgChonMau.SelectedIndex = 0;
            rgChonMau_SelectedIndexChanged(null, null);

            //var nhacc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            //lupNhaCC.Properties.DataSource = nhacc;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;

            var q = (from k in data.KPhongs
                     join rv in data.RaViens on k.MaKP equals rv.MaKP
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                q = (from k in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            }
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Add(new DTBN { IDDTBN = 0, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Dịch vụ");
            _listNhomDV = data.NhomDVs.Where(p => p.Status == 2).ToList();
            cklNhomDV.DisplayMember = "TenNhomCT";
            cklNhomDV.ValueMember = "IDNhom";
            cklNhomDV.DataSource = _listNhomDV;
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                cklNhomDV.SetItemChecked(i, true);
            }
            load++;
        }

        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;
            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        private string MaKPQD(int mKP)
        {
            string rs = "";
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKP = new List<KPhong>();
            _lKP = data.KPhongs.Where(p => p.MaKP == mKP).ToList();
            if (_lKP.Count > 0)
                rs = _lKP.First().MaQD == null ? "" : _lKP.First().MaQD.ToString();
            return rs;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            #region Lấy danh sách khoa phòng
            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? -1 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));

                        if (mKhoa == 0)
                        {
                            kp = 0;

                            break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }
            #endregion lấy danh sách khoa phòng
            #region Biến
            var a = data.NhomDVs.ToList();

            string macc = "";
            int _makp = -1;

            DateTime tungay, denngay;
            int trongBH = 5;

            int _idDtuong = -1;
            if (lupDoituong.EditValue != null)
                _idDtuong = Convert.ToInt16(lupDoituong.EditValue);

            if (KTtaoBcMau20())
            {
                if (lupDoituong.Text == "BHYT")
                {
                    trongBH = rdTrongBH.SelectedIndex;
                }
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;
                List<string> _ltamung = new List<string>();

            #endregion biến
                #region select tất cả

                List<NhomDV> _lnhom = new List<NhomDV>();
                List<int> _idNhomDV = new List<int>();
                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                        _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
                }

                _lnhom = (from nhom in data.NhomDVs join id in _idNhomDV on nhom.IDNhom equals id select nhom).ToList();
                var qdv = (from nhom in _lnhom
                           join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new
                           {
                               nhom,
                               tn.TenTN,
                               dv
                           }).ToList();
                //
                // bệnh nhân join với bảng tạm ứng và tạm ứng chi tiểt
                //
                var qtuct = (from bn in data.BenhNhans.Where(p => radNoiNgoaiTru.SelectedIndex == 2 || p.NoiTru == radNoiNgoaiTru.SelectedIndex)
                             join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                             join tuct in data.TamUngcts.Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng// select new {bn, tuct})   
                             //join cd in data.ChiDinhs on tu.IDTamUng equals cd.SoPhieu
                             //join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                             where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                             select new
                             {
                                 TenBNhan=bn.TenBNhan,
                                 MaBNhan = bn.MaBNhan,
                                 DTuong = bn.DTuong,
                                 MaDTuong = bn.MaDTuong,
                                 IDDTBN = bn.IDDTBN,
                                 NoiTru = bn.NoiTru,
                                 TuyenDuoi = bn.TuyenDuoi,
                                 NgayThu = tu.NgayThu,
                                 MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                                 TrongBH = tuct.TrongBH,
                                 DonGia = tuct.DonGia,
                                 MaDV = tuct.MaDV,
                                 SoLuong = tuct.SoLuong,
                                 ThanhTien = (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? tuct.SoTien : tuct.ThanhTien,
                                 IDTamUng = tu.IDTamUng,
                                 IDGoiDV = tu.IDGoiDV,
                                 TenDV = "",
                                 IDTamUngct = tuct.IDTamUngct
                                 //NgayThang = cls.NgayThang
                             }).ToList();

                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    var cd = (from a1 in data.ChiDinhs
                              join b in data.CLS on a1.IdCLS equals b.IdCLS
                              select new { a1.IDCD, a1.SoPhieu, a1.MaDV, b.MaKPth , b.NgayThang}).ToList();
                    var test = (from bn in data.BenhNhans.Where(p => radNoiNgoaiTru.SelectedIndex == 2 || p.NoiTru == radNoiNgoaiTru.SelectedIndex)
                                join tu in data.TamUngs.Where(p => p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                                join goi in data.DmGoiDVs on tu.IDGoiDV equals goi.IDGoi
                                //join cls in cd on tu.IDTamUng equals cls.SoPhieu
                                where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                                select new
                                {
                                    TenBNhan=bn.TenBNhan,
                                    MaBNhan = bn.MaBNhan,
                                    DTuong = bn.DTuong,
                                    MaDTuong = bn.MaDTuong,
                                    IDDTBN = bn.IDDTBN,
                                    NoiTru = bn.NoiTru,
                                    TuyenDuoi = bn.TuyenDuoi,
                                    NgayThu = tu.NgayThu,
                                    MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                                    TrongBH = 0,
                                    DonGia = goi.DonGia,
                                    MaDV = 0,
                                    SoLuong = 1.00,
                                    ThanhTien = tu.SoTien ?? 0,
                                    IDTamUng = tu.IDTamUng,
                                    IDGoiDV = tu.IDGoiDV,
                                    TenDV = goi.TenGoi,
                                    IDTamUngct = 0
                                    //NgayThang = cls.NgayThang
                                }).ToList();
                    qtuct = (from a1 in qtuct.Where(p => p.IDGoiDV == 0)
                             join b in cd on new { IDTamUng = a1.IDTamUng, MaDV = a1.MaDV } equals new { IDTamUng = b.SoPhieu ?? 0, MaDV = b.MaDV ?? 0 } into k
                             from k1 in k.DefaultIfEmpty()
                             select new
                             {
                                 TenBNhan=a1.TenBNhan,
                                 MaBNhan = a1.MaBNhan,
                                 DTuong = a1.DTuong,
                                 MaDTuong = a1.MaDTuong,
                                 IDDTBN = a1.IDDTBN,
                                 NoiTru = a1.NoiTru,
                                 TuyenDuoi = a1.TuyenDuoi,
                                 NgayThu = a1.NgayThu,
                                 MaKP = k1 != null ? k1.MaKPth : a1.MaKP,// Khoa phòng thực hiện
                                 TrongBH = a1.TrongBH,
                                 DonGia = a1.DonGia,
                                 MaDV = a1.MaDV,
                                 SoLuong = a1.SoLuong,
                                 ThanhTien = a1.ThanhTien,
                                 IDTamUng = a1.IDTamUng,
                                 IDGoiDV = a1.IDGoiDV,
                                 TenDV = a1.TenDV,
                                 IDTamUngct = a1.IDTamUngct
                                 //NgayThang = k1 != null ? k1.NgayThang : null
                             }).Distinct().ToList();
                    qtuct.AddRange(test);
                }

                //
                // tìm kiếm theo điều kiện
                //
                var qtk = (from tu in qtuct
                           join nhom in qdv on tu.MaDV equals nhom.dv.MaDV
                           where (_idDtuong == 0 || tu.IDDTBN == _idDtuong)
                           where (macc == "" ? true : nhom.dv.MaCC == macc)
                           where (kp == 0 ? true : _lMaKhoa.Contains(tu.MaKP == null ? 0 : tu.MaKP.Value))
                           where (trongBH == 2 || tu.TrongBH == trongBH || trongBH == 5)
                           // where (lupDoituong.Text ==   "BHYT" ? ((tu.TrongBH == trongBH) ) : true)                          
                           select new
                           {
                              tu.TenBNhan,
                               MaBNhan = tu.MaBNhan,
                               DTuong = tu.DTuong,
                               MaDTuong = tu.MaDTuong,
                               IDDTBN = tu.IDDTBN,
                               NoiTru = tu.NoiTru,
                               TuyenDuoi = tu.TuyenDuoi,
                               NgayThu = tu.NgayThu,
                               MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                               TrongBH = tu.TrongBH,
                               DonGia = tu.DonGia,
                               MaDV = tu.MaDV,
                               SoLuong = tu.SoLuong,
                               ThanhTien = tu.ThanhTien,
                               SoTTqd = nhom.dv.SoTTqd,
                               IDNhom = nhom.dv.IDNhom ?? 0,
                               BHTT = nhom.dv.BHTT ?? 0,
                               TenNhom = nhom.nhom.TenNhom,
                               TenTN = nhom.TenTN,
                               TenHC = nhom.dv.TenHC,
                               MaQD = nhom.dv.MaQD,
                               DuongD = nhom.dv.DuongD,
                               TenDV = nhom.dv.TenDV,
                               HamLuong = nhom.dv.HamLuong,
                               DonVi = nhom.dv.DonVi,
                               SoDK = nhom.dv.SoDK,
                               QCPC = nhom.dv.QCPC,
                               MaCC = nhom.dv.MaCC
                               //NgayThang = tu.NgayThang
                           }).ToList();
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    qtk = (from tu in qtuct.Where(p => p.TenDV == "")
                           join nhom in qdv on tu.MaDV equals nhom.dv.MaDV
                           where (_idDtuong == 0 || tu.IDDTBN == _idDtuong)
                           where (macc == "" ? true : nhom.dv.MaCC == macc)
                           where (kp == 0 ? true : _lMaKhoa.Contains(tu.MaKP == null ? 0 : tu.MaKP.Value))
                           where (trongBH == 2 || tu.TrongBH == trongBH || trongBH == 5)
                           // where (lupDoituong.Text ==   "BHYT" ? ((tu.TrongBH == trongBH) ) : true)                          
                           select new
                           {
                               tu.TenBNhan,
                               MaBNhan = tu.MaBNhan,
                               DTuong = tu.DTuong,
                               MaDTuong = tu.MaDTuong,
                               IDDTBN = tu.IDDTBN,
                               NoiTru = tu.NoiTru,
                               TuyenDuoi = tu.TuyenDuoi,
                               NgayThu = tu.NgayThu,
                               MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                               TrongBH = tu.TrongBH,
                               DonGia = tu.DonGia,
                               MaDV = tu.MaDV,
                               SoLuong = tu.SoLuong,
                               ThanhTien = tu.ThanhTien,
                               SoTTqd = nhom.dv.SoTTqd,
                               IDNhom = nhom.dv.IDNhom ?? 0,
                               BHTT = nhom.dv.BHTT ?? 0,
                               TenNhom = nhom.nhom.TenNhom,
                               TenTN = nhom.TenTN,
                               TenHC = nhom.dv.TenHC,
                               MaQD = nhom.dv.MaQD,
                               DuongD = nhom.dv.DuongD,
                               TenDV = nhom.dv.TenDV,
                               HamLuong = nhom.dv.HamLuong,
                               DonVi = nhom.dv.DonVi,
                               SoDK = nhom.dv.SoDK,
                               QCPC = nhom.dv.QCPC,
                               MaCC = nhom.dv.MaCC
                               //NgayThang = tu.NgayThang
                           }).ToList();
                    var qtk1 = (from tu in qtuct.Where(p => p.TenDV != "")
                                where (_idDtuong == 0 || tu.IDDTBN == _idDtuong)
                                where (kp == 0 ? true : _lMaKhoa.Contains(tu.MaKP == null ? 0 : tu.MaKP.Value))
                                where (trongBH == 2 || tu.TrongBH == trongBH || trongBH == 5)
                                // where (lupDoituong.Text ==   "BHYT" ? ((tu.TrongBH == trongBH) ) : true)                          
                                select new
                                {
                                    tu.TenBNhan,
                                    MaBNhan = tu.MaBNhan,
                                    DTuong = tu.DTuong,
                                    MaDTuong = tu.MaDTuong,
                                    IDDTBN = tu.IDDTBN,
                                    NoiTru = tu.NoiTru,
                                    TuyenDuoi = tu.TuyenDuoi,
                                    NgayThu = tu.NgayThu,
                                    MaKP = tu.MaKP,// phòng khám hoặc khoa phòng điều trị ( không phải phòng thu viện phí)
                                    TrongBH = tu.TrongBH,
                                    DonGia = tu.DonGia,
                                    MaDV = tu.MaDV,
                                    SoLuong = tu.SoLuong,
                                    ThanhTien = tu.ThanhTien,
                                    SoTTqd = "",
                                    IDNhom = 0,
                                    BHTT = 0,
                                    TenNhom = "Gói dịch vụ",
                                    TenTN = "Gói dịch vụ",
                                    TenHC = "",
                                    MaQD = "",
                                    DuongD = "",
                                    TenDV = tu.TenDV,
                                    HamLuong = "",
                                    DonVi = "Gói",
                                    SoDK = "",
                                    QCPC = "",
                                    MaCC = ""
                                    //NgayThang = tu.NgayThang
                                }).ToList();
                    qtk.AddRange(qtk1);
                }
                //
                // left join với bảng ra viện để lấy ngày ra
                //
                var qrv = (from tuct in qtk
                           join rv in data.RaViens on tuct.MaBNhan equals rv.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               tuct.TenBNhan,
                               tuct.MaBNhan,
                               tuct.DTuong,
                               tuct.MaDTuong,
                               tuct.IDDTBN,
                               tuct.NoiTru,
                               tuct.TuyenDuoi,
                               tuct.MaKP,
                               tuct.TrongBH,
                               tuct.DonGia,
                               tuct.MaDV,
                               tuct.SoLuong,
                               tuct.ThanhTien,
                               tuct.NgayThu,
                               tuct.SoTTqd,
                               tuct.IDNhom,
                               tuct.BHTT,
                               tuct.TenNhom,
                               tuct.TenTN,
                               tuct.TenHC,
                               tuct.MaQD,
                               tuct.DuongD,
                               tuct.TenDV,
                               tuct.HamLuong,
                               tuct.DonVi,
                               tuct.SoDK,
                               tuct.QCPC,
                               tuct.MaCC,
                               NgayRa = kq1 == null ? null : kq1.NgayRa,
                               MaICD = kq1 == null ? null : kq1.MaICD
                               //tuct.NgayThang
                           }).ToList();
                //
                // left join với bảng viện phí để lấy ngày thanh toán
                //
                var qvp = (from rv in qrv
                           join vp in data.VienPhis on rv.MaBNhan equals vp.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               rv.TenBNhan,
                               rv.MaBNhan,
                               rv.DTuong,
                               rv.MaDTuong,
                               rv.IDDTBN,
                               rv.NoiTru,
                               rv.TuyenDuoi,
                               rv.MaKP,
                               rv.TrongBH,
                               rv.DonGia,
                               rv.MaDV,
                               rv.SoLuong,
                               rv.ThanhTien,
                               rv.NgayThu,
                               rv.SoTTqd,
                               rv.IDNhom,
                               rv.BHTT,
                               rv.TenNhom,
                               rv.TenTN,
                               rv.TenHC,
                               rv.MaQD,
                               rv.DuongD,
                               rv.TenDV,
                               rv.HamLuong,
                               rv.DonVi,
                               rv.SoDK,
                               rv.QCPC,
                               rv.MaCC,
                               rv.NgayRa,
                               rv.MaICD,
                               NgayTT = kq1 == null ? null : kq1.NgayTT,
                               NgayDuyet = kq1 == null ? null : kq1.NgayDuyet
                               //rv.NgayThang
                               //Ngay=_ngaytt == 0 ? rv.NgayRa : (_ngaytt == 1 ? (kq1 == null ? null : kq1.NgayTT) : (_ngaytt == 2 ? (kq1.NgayDuyet) : (_ngaytt == 3 ? (rv.NgayThu))))
                           }).Where(p => _ngaytt == 0 ? (p.NgayRa >= tungay && p.NgayRa <= denngay) : (_ngaytt == 1 ? (p.NgayTT >= tungay && p.NgayTT <= denngay) : (_ngaytt == 2 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (_ngaytt == 3 ? (p.NgayThu >= tungay && p.NgayThu <= denngay) : false)))).ToList();
                #endregion
                if (rgChonMau.SelectedIndex == 1)
                {
                    var q12 = (from ab in qvp
                               select new
                               {
                                   ab.MaBNhan,
                                   ab.NgayThu,
                                   Ngay = _ngaytt == 0 ? ab.NgayRa : (_ngaytt == 1 ? ab.NgayTT : (_ngaytt == 2 ? ab.NgayDuyet : ab.NgayThu)),
                                   ab.TenDV,
                                   ab.DonGia,
                                   ab.ThanhTien,
                                   ab.TenBNhan
                               }).OrderBy(p => p.TenDV).ThenBy(p => p.NgayThu).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.rep_BCChiTietBN501 rep = new BaoCao.rep_BCChiTietBN501();

                    rep.ngaytu.Value = "Từ ngày  " + lupTuNgay.DateTime.ToShortDateString() + "  đến ngày  " + lupDenNgay.DateTime.ToShortDateString();
                    rep.DataSource = q12;

                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    List<cls19_20> _list = new List<cls19_20>();
                    #region group để tính tổng tiền theo dichj vuj
                    var q2 = (from lq in qvp
                              group lq by new
                              {
                                  lq.TrongBH,
                                  lq.DonGia,
                                  lq.SoTTqd,
                                  lq.IDNhom,
                                  lq.TenNhom,
                                  lq.TenTN,
                                  lq.TenHC,
                                  lq.MaQD,
                                  lq.DuongD,
                                  lq.QCPC,
                                  lq.TenDV,
                                  lq.MaCC,
                                  lq.HamLuong,
                                  lq.DonVi,
                                  lq.SoDK,
                                  lq.MaDV
                              } into kq
                              select new
                              {
                                  TrongBH = kq.Key.TrongBH,
                                  DonGia = kq.Key.DonGia,
                                  SoTTqd = kq.Key.SoTTqd,
                                  IdNhom = kq.Key.IDNhom,
                                  TenNhomThuoc = kq.Key.TenNhom,
                                  TenTNhom = kq.Key.TenTN,
                                  TenHC = kq.Key.TenHC,
                                  MaQD = kq.Key.MaQD,
                                  DuongDung = kq.Key.DuongD,
                                  TenThuoc = kq.Key.TenDV,
                                  HamLuong = kq.Key.HamLuong,
                                  DonVi = kq.Key.DonVi,
                                  SoDK = kq.Key.SoDK,
                                  MaDV = kq.Key.MaDV,
                                  MaCC = kq.Key.MaCC,
                                  kq.Key.QCPC,
                                  SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                                  SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                                  SoLuong139 = kq.Where(p => p.MaDTuong == "DT" || p.MaDTuong == "HN" || p.MaDTuong == "DK").Sum(p => p.SoLuong),
                                  SoLuongTE = kq.Where(p => p.MaDTuong == "TE").Sum(p => p.SoLuong),
                                  SoLuongBHYT_DV = (lupDoituong.Text == "Dịch vụ") ? (kq.Sum(p => p.SoLuong)) : ((kq.Where(p => p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong))),
                                  SoLuong = kq.Sum(p => p.SoLuong),
                                  ThanhTien = kq.Sum(p => p.ThanhTien)
                              }).OrderBy(p => p.TenThuoc).ToList();
                    #endregion group để tính theo tiền bệnh nhân
                    #region đổ vào list cls19_20
                    foreach (var l in q2)
                    {
                        cls19_20 cls = new cls19_20();
                        cls.TrongBH = Convert.ToInt16(l.TrongBH);
                        cls.Don_gia = Convert.ToDouble(l.DonGia);
                        cls.SoTTqd = l.SoTTqd != null ? l.SoTTqd.ToString() : "";
                        cls.Ma_nhom = Convert.ToInt32(l.IdNhom);
                        cls.Tennhom = l.TenNhomThuoc != null ? l.TenNhomThuoc : "";
                        cls.TenTN = l.TenTNhom == null ? "" : l.TenTNhom;
                        cls.TenHC = l.TenHC == null ? "" : l.TenHC;
                        cls.MaQD = l.MaQD == null ? "" : l.MaQD;
                        cls.QCPC = l.QCPC == null ? "" : l.QCPC;
                        cls.Duong_dung = l.DuongDung == null ? "" : l.DuongDung;
                        cls.Ten_thuoc = l.TenThuoc == null ? "" : l.TenThuoc;
                        cls.Ham_luong = l.HamLuong == null ? "" : l.HamLuong;
                        cls.Don_vi_tinh = l.DonVi == null ? "" : l.DonVi;
                        cls.So_dang_ky = l.SoDK == null ? "" : l.SoDK;
                        cls.Ma_thuoc = l.MaDV.ToString();
                        cls.MaCC = l.MaCC == null ? "" : l.MaCC;
                        cls.SoluongNT = Convert.ToDouble(l.SoLuongNT);
                        cls.SoluongNgT = Convert.ToDouble(l.SoLuongNgT);
                        cls.SoLuong139 = Convert.ToDouble(l.SoLuong139);
                        cls.SoLuongTE = Convert.ToDouble(l.SoLuongTE);
                        cls.SoLuongBHYT_DV = Convert.ToDouble(l.SoLuongBHYT_DV);
                        cls.So_luong = Convert.ToDouble(l.SoLuong);
                        cls.Thanh_tien = Convert.ToDouble(l.ThanhTien);
                        _list.Add(cls);
                    }
                    _list.OrderBy(p => p.Ten_thuoc);

                    if (_list.Count > 0)
                    {

                        frmIn frm = new frmIn();
                        BaoCao.rep_BCThuThangNgoaiTru rep;
                        rep = new BaoCao.rep_BCThuThangNgoaiTru();

                        rep.TuNgayDenNgay.Value = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
                        rep.txtTieuDe.Text = "BÁO CÁO CHI PHÍ DỊCH VỤ THU THẲNG";
                        //if (!string.IsNullOrEmpty(macc))
                        //    rep.NhaCC.Value = lupNhaCC.Text;
                        rep.DataSource = _list.OrderBy(p => p.Ten_thuoc);

                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();

                        if (chkXuatExel.Checked)
                        {
                            xuatExcel(_list, txtDuongDan.Text);
                        }


                    #endregion in báo cáo biểu 20

                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu !");
                    }

                }

            }
        }

        #region xuất excel
        /// <summary>
        /// xuất excel biểu 19, 20
        /// </summary>
        /// <param name="_lKetQua"></param> List<cls19_20>
        /// <param name="duongdan"></param>
        /// <param name="mauso"></param> 19: mẫu 19, 20: Mẫu 20
        private void xuatExcel(List<cls19_20> _lKetQua, string duongdan)
        {


            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            exSheet.Name = "CPThuThangBNNgoaiTru";
            int k = 0;
            int i = 1;
            string[] arr = new string[] {"STT", "ten_thuoc", "hamluong", "sodangky", "donvitinh", "soluong", "dongia",
                    "thanhtien", "ma_thuoc", "mabv", "loaikcb", "stt_dmbyt", "hoat_chat" };
            foreach (var b in arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = b.ToString();
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }
            foreach (var a in _lKetQua)
            {
                i++;
                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                r1.Value2 = a.SoQD;
                r1.Columns.AutoFit();
                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                r2.NumberFormat = "@";
                r2.Value2 = a.Ten_thuoc;
                r2.EntireColumn.ColumnWidth = 12;
                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                r3.NumberFormat = "@";
                r3.Value = a.Ham_luong;
                r3.EntireColumn.ColumnWidth = 40;
                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                r4.NumberFormat = "@";
                r4.Value = a.So_dang_ky;
                r4.EntireColumn.ColumnWidth = 30;
                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                r5.NumberFormat = "@";
                r5.Value = a.Don_vi_tinh;
                r5.EntireColumn.ColumnWidth = 12;
                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                r6.NumberFormat = "@";
                r6.Value = Math.Round(a.SoluongNgT + a.SoluongNT, 2);
                r6.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                r7.NumberFormat = "@";
                r7.Value = a.Don_gia;
                r7.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                r8.NumberFormat = "@";
                r8.Value = a.Thanh_tien;
                r8.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                r9.NumberFormat = "@";
                r9.Value = a.MaQD;
                r9.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                r10.NumberFormat = "@";
                r10.Value = DungChung.Bien.MaBV;
                r10.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                r11.NumberFormat = "@";

                r11.Value = "NGOAI";

                r11.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                r12.Value = a.SoTTqd;
                r12.EntireColumn.ColumnWidth = 15;
                COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                r13.NumberFormat = "@";
                r13.Value = a.TenHC;
                r13.EntireColumn.ColumnWidth = 15;
            }
            exApp.Visible = true;
            try
            {
                exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);
                MessageBox.Show("Xuất file thành công");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
            }

        }
        #endregion xuất excel

        static String NgayTu_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 00:00:00 AM");
        }
        public static String NgayDen_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 23:59:59.998 PM");
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        SaveFileDialog dialog = new SaveFileDialog();
        private void btnDuongDan_Click_1(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDan.Text = dialog.FileName;
            }
        }

        private void chkXuatExel_CheckedChanged(object sender, EventArgs e)
        {
            txtDuongDan.Visible = chkXuatExel.Checked;
            btnDuongDan.Visible = chkXuatExel.Checked;
            lbDuongDan.Visible = chkXuatExel.Checked;
            rdFont.Visible = chkXuatExel.Checked;
            txtDuongDan.Text = chkXuatExel.Checked == true ? ("C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xls") : "";
        }

        private void rdFont_EditValueChanged(object sender, EventArgs e)
        {
            if (rdFont.SelectedIndex == 0)
            {
                Font = 0;
            }
            else
                Font = 1;
        }
        private static char[] arrTCVN = {'µ','¸','¶','·','¹','¨', '»', '¾', '¼', '½', 'Æ','©', 'Ç', 'Ê', 'È', 'É', 'Ë', 
                                         '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ','ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö','×', 'Ý', 'Ø', 'Ü', 'Þ', 
                                         'ß', 'ã', 'á', 'â', 'ä','«', 'å', 'è', 'æ', 'ç', 'é','¬', 'ê', 'í', 'ë', 'ì', 'î','ï',
                                         'ó', 'ñ', 'ò', 'ô','­', 'õ', 'ø', 'ö', '÷', 'ù','ú', 'ý', 'û', 'ü', 'þ','¡', '¢', '§', '£', '¤', '¥', '¦'
                                        };
        private static char[] arrUnicode = {'à', 'á', 'ả', 'ã', 'ạ','ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ','â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ','đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 
                                           'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ','ì', 'í', 'ỉ', 'ĩ', 'ị','ò', 'ó', 'ỏ', 'õ', 'ọ','ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 
                                          'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ','ù', 'ú', 'ủ', 'ũ', 'ụ','ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự','ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ','Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
                                        };
        private static Char[] Converter;

        private String convertFont(String str)
        {
            String result = "";
            if (Font == 0)
            {
                result = convert(str);
                return result;
            }
            else
                return str;
        }
        private String convert(String str)
        {
            if (str != null)
            {
                bool tt = false;
                Converter = new char[str.Length];
                Char[] arrStr = str.ToCharArray();
                for (int i = 0; i < arrStr.Length; i++)
                {
                    for (int j = 0; j < arrUnicode.Length; j++)
                    {
                        if (arrStr[i] == (arrUnicode[j]))
                        {
                            Converter[i] = arrTCVN[j];
                            tt = true;
                            break;
                        }
                    }
                    if (tt == false)
                    {
                        Converter[i] = arrStr[i];
                    }
                    tt = false;
                }
                return new String(Converter);
            }
            return str;
        }


        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {

                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }









        private void grcKhoaPhong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {

                GridControl grid = sender as GridControl;
                GridView view = grid.FocusedView as GridView;
                if (view.IsEditing)
                    view.CloseEditor();
                grid.SelectNextControl(grid, e.Modifiers == Keys.None, false, false, true);
                e.Handled = true;
            }
        }



        private void lupDoituong_EditValueChanged(object sender, EventArgs e)
        {

            if (lupDoituong.Text == ("BHYT"))
            {
                rdTrongBH.Enabled = true;
                cboNoiTinh.Enabled = true;
            }
            else
            {
                cboNoiTinh.Enabled = false;
                cboNoiTinh.SelectedIndex = 0;
                rdTrongBH.Enabled = false;
                rdTrongBH.SelectedIndex = 2;

            }
        }

        private void radTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void rgChonMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgChonMau.SelectedIndex == 0)
            {
                panelControl1.Visible = true;
            }
            else
            {
                panelControl1.Visible = false;
                chkXuatExel.Checked = false;
            }
        }




    }
}
