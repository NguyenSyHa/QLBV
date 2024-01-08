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
    public partial class frm_NXT_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_NXT_01071()
        {
            InitializeComponent();
        }
        public class loaixuat1
        {
            private string tenCot;

            public string TenCot
            {
                get { return tenCot; }
                set { tenCot = value; }
            }
            private string maCot;

            public string MaCot
            {
                get { return maCot; }
                set { maCot = value; }
            }
            private int maPL;

            public int MaPL
            {
                get { return maPL; }
                set { maPL = value; }
            }
        }
        List<loaixuat1> _lCot = new List<loaixuat1>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_NXT_01071_Load(object sender, EventArgs e)
        {
            _lCot.Add(new loaixuat1 { MaPL = -1, MaCot = "0", TenCot = "Tất cả" });
            _lCot.Add(new loaixuat1 { MaPL = 0, MaCot = "Xuatngtru", TenCot = "Xuất ngoại trú" });
            _lCot.Add(new loaixuat1 { MaPL = 1, MaCot = "Xuatnoitru", TenCot = "Xuất nội trú" });
            _lCot.Add(new loaixuat1 { MaPL = 2, MaCot = "Xuatnoibo", TenCot = "Xuất nội bộ" });
            _lCot.Add(new loaixuat1 { MaPL = 3, MaCot = "XuatNgoaiBV", TenCot = "Xuất ngoài BV" });
            _lCot.Add(new loaixuat1 { MaPL = 4, MaCot = "XuatDTNgTru", TenCot = "Xuất điều trị ngoại trú" });
            _lCot.Add(new loaixuat1 { MaPL = 5, MaCot = "XuatCLS", TenCot = "Xuất Cận Lâm Sàng" });
            _lCot.Add(new loaixuat1 { MaPL = 6, MaCot = "XuatTuTruc", TenCot = "Xuất tủ trực" });
            _lCot.Add(new loaixuat1 { MaPL = 7, MaCot = "XuatPhongKham", TenCot = "Xuất phòng khám" });
            _lCot.Add(new loaixuat1 { MaPL = 8, MaCot = "XuatKiemNghiem", TenCot = "Xuất kiểm nghiệm" });
            _lCot.Add(new loaixuat1 { MaPL = 9, MaCot = "XuatKhac", TenCot = "Xuất khác" });
            _lCot.Add(new loaixuat1 { MaPL = 10, MaCot = "XuatSX", TenCot = "Xuất sản xuất" });
            _lCot.Add(new loaixuat1 { MaPL = 11, MaCot = "XuatLS", TenCot = "Xuất lâm sàng" });

            loaixuat.DataSource = _lCot.ToList();
            for (int i = 0; i < loaixuat.ItemCount; i++)
            {
                if (loaixuat.GetItemValue(i) != null)
                    loaixuat.SetItemChecked(i, true);
                else
                    loaixuat.SetItemChecked(i, false);
            }
            loaixuat.DataSource = _lCot;
            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lnhom.Add(new NhomDV { IDNhom = -1, TenNhom = " Tất cả" });
            lupNhom.Properties.DataSource = _lnhom.OrderBy(p => p.TenNhom).ToList();
            lookUpEdit1.Properties.DataSource = _lCot.ToList();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = dskp;
        }
        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (checkEdit1.Checked == true)
            {
                if (lookUpEdit1.Text == null || lookUpEdit1.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn phân loại xuất");
                    lookUpEdit1.Focus();
                    return false;
                }
                else return true;
            }
            else return true;
        }
        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {

            List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
            int id = -1;
            if (lupNhom.EditValue != null)
                id = Convert.ToInt32(lupNhom.EditValue);
            _ltnhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            _ltnhom.Add(new TieuNhomDV { IdTieuNhom = -1, TenTN = " Tất cả" });
            if (id >= 0)
                _ltnhom = _ltnhom.Where(p => p.IDNhom == id).ToList();
            lupTieuNhom.Properties.DataSource = _ltnhom.OrderBy(p => p.TenTN).ToList();
        }

        private void loaixuat_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < loaixuat.ItemCount; i++)
            {
                if (loaixuat.GetItemChecked(i))
                    check++;
            }
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    loaixuat.CheckAll();
                else
                    loaixuat.UnCheckAll();
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    check++;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            int idnhom = -1, idtieunhom = -1;
            string _tenkho = "", _tenNCC = "";
            List<KPhong> _kpChon = new List<KPhong>();
            List<loaixuat1> _listxuatchon = new List<loaixuat1>();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (KTtaoBcNXT())
            {
                for (int i = 0; i < loaixuat.ItemCount; i++)
                {
                    if (loaixuat.GetItemChecked(i))
                        _listxuatchon.Add(new loaixuat1 { TenCot = loaixuat.GetItemText(i), MaCot = Convert.ToString(loaixuat.GetItemValue(i)) });
                }
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemChecked(i))
                        _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                }
                int pl = Convert.ToInt32(lookUpEdit1.EditValue);
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;


                if (lupNhom.EditValue != null)
                    idnhom = Convert.ToInt32(lupNhom.EditValue);
                if (lupTieuNhom.EditValue != null)
                    idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
                foreach (var item in _kpChon)
                {
                    _tenkho += item.TenKP + "; ";
                }
                var qtenncc = (from nhapd in data.NhapDs
                               join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                               where (nhacc.MaCC == _nhacc)
                               select new { nhacc.TenCC }).ToList();
                if (qtenncc.Count > 0)
                {
                    _tenNCC = qtenncc.First().TenCC;
                }
                if (_kpChon.Count > 0)
                {
                    // int count = kpXP.Count;
                    var qnxt0 = (from nhapd in data.NhapDs//.Where(p=> (chk_mauToanVien.Checked || count==0) ? !(kpXP.Contains(p.MaKPnx??0)) : true)
                                 join nhapdct in data.NhapDcts
                                 on nhapd.IDNhap equals nhapdct.IDNhap
                                 join bn in data.BenhNhans on nhapdct.MaBNhan equals bn.MaBNhan into k
                                 from k1 in k.DefaultIfEmpty()
                                 where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                                 //group new{nhapd,nhapdct} by new {}
                                 select new { NoiTru = k1 != null ? k1.NoiTru : -1, DTNT = k1 != null ? k1.DTNT : false,
                                     DTuong = k1 != null ? k1.DTuong : "",
                                     nhapdct.IDDTBN , nhapd.TraDuoc_KieuDon, nhapd.XuatTD, nhapd.MaKP, nhapd.MaKPnx, nhapdct.MaDV,  nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia,
                                     SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN :0,
                                     SoLuongX = (nhapd.PLoai == 2 || nhapd .PLoai == 3) ? nhapdct.SoLuongX:0,
                                     ThanhTienN =nhapd.PLoai == 1 ?  nhapdct.ThanhTienN : 0,
                                     ThanhTienX =(nhapd.PLoai == 2 || nhapd .PLoai == 3) ?  nhapdct.ThanhTienX : 0 ,
                                     DonGiaCT = nhapd.PLoai == 2 ? nhapdct.DonGia : nhapdct.DonGiaCT + nhapdct.DonGiaCT * nhapdct.VAT, nhapd.SoPL }).OrderByDescending(p => p.NgayNhap).ToList();
                    var qdtbn = data.DTBNs.ToList();
                    var qnxt2 = (from  nhapdct in qnxt0
                               join dt in qdtbn on nhapdct.IDDTBN equals dt.IDDTBN into k
                                 from k1 in k.DefaultIfEmpty()
                               
                                 select new
                                 {
                                     NoiTru = nhapdct.NoiTru,
                                     DTNT =nhapdct.DTNT,
                                     DTuong = k1 != null ? k1.DTBN1.Trim() : nhapdct.DTuong.Trim(),
                                     nhapdct.IDDTBN,
                                     nhapdct.TraDuoc_KieuDon,
                                     nhapdct.XuatTD,
                                     nhapdct.MaKP,
                                     nhapdct.MaKPnx,
                                     nhapdct.MaDV,
                                     nhapdct.PLoai,
                                     nhapdct.KieuDon,
                                     nhapdct.NgayNhap,
                                     nhapdct.DonGia,
                                     nhapdct.SoLuongN,
                                     nhapdct.SoLuongX,
                                     nhapdct.ThanhTienN,
                                     nhapdct.ThanhTienX,
                                     DonGiaCT = nhapdct.DonGiaCT,
                                     nhapdct.SoPL
                                 }).OrderByDescending(p => p.NgayNhap).ToList();

                    var dichvu = (from dv in data.DichVus
                                  join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                  join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                  where (idnhom == -1 ? true : nhomdv.IDNhom == idnhom) && (idtieunhom == -1 ? true : tn.IdTieuNhom == idtieunhom)
                                  select new { dv.MaDV, TenDV = dv.TenDV, tn.TenTN, nhomdv.TenNhom, tn.STT, dv.MaCC, dv.DonVi, dv.HamLuong, dv.TenHC, dv.MaTam, dv.SoDK, dv.DuongD }).ToList();

                    var qnxt = (from a in qnxt2
                                join dv in dichvu on a.MaDV equals dv.MaDV
                                join kp in _kpChon//.Where(p=> chk_mauToanVien.Checked? kpBV.Contains(p.MaKP) : true) 
                                on a.MaKP equals kp.MaKP
                                group a by new { dv.STT, dv.MaCC, dv.TenTN, dv.TenNhom, dv.TenDV, dv.DonVi, a.DonGia, a.MaDV, dv.MaTam } into kq
                                select new
                                {
                                    kq.Key.STT,
                                    kq.Key.TenTN,
                                    kq.Key.MaCC,
                                    MaDV = kq.Key.MaDV,
                                    MaTam = kq.Key.MaTam,
                                    TenNhomDuoc = kq.Key.TenNhom,
                                    TenHamLuong = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    //tồn đầu kỳ
                                    TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                    //nhập trong kỳ (trừ nhập trả)
                                    NhapTKSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon != 2).Sum(p => p.SoLuongN),
                                    //Nhập theo Hóa đơn
                                    NhapHD = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN),
                                    //nhập trả lại
                                    NhapTra_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                                    //xuất trong kỳ
                                    TongXuatTKSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất ngoại trú
                                    Xuatngtru = checkEdit1.Checked == false ? (kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) : (kq.Where(p => p.PLoai == 2 && p.DTuong == "Dịch vụ" && (p.KieuDon == pl || pl == -1)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                    //Xuất nội trú
                                    Xuatnoitru = checkEdit1.Checked == false ? (kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) : (kq.Where(p => p.PLoai == 2 && p.DTuong == "BHYT" && (p.KieuDon == pl || pl == -1)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                    Xuatnoitru1 = (kq.Where(p => p.PLoai == 2 && p.DTuong == "BHYT" && (p.KieuDon == pl || pl == -1)).Where(p => p.NoiTru == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                    Xuatnoitru2 = (kq.Where(p => p.PLoai == 2 && p.DTuong == "BHYT" && (p.KieuDon == pl || pl == -1)).Where(p => p.NoiTru == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                    //Xuất nội bộ
                                    Xuatnoibo = kq.Where(p => p.PLoai == 2 && p.KieuDon == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất ngoài bv
                                    XuatNgoaiBV = kq.Where(p => p.PLoai == 2 && p.KieuDon == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất điều trị ngoại trú
                                    XuatDTNgTru = kq.Where(p => p.PLoai == 2 && p.KieuDon == 4).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất cận lâm sàng
                                    XuatCLS = kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất tủ trực
                                    XuatTuTruc = kq.Where(p => p.PLoai == 2 && p.KieuDon == 6).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất phòng khám
                                    XuatPhongKham = kq.Where(p => p.PLoai == 2 && p.KieuDon == 7).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất kiểm nghiệm
                                    XuatKiemNghiem = kq.Where(p => p.PLoai == 2 && p.KieuDon == 8).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất khác
                                    XuatKhac = checkEdit1.Checked == false ? (kq.Where(p => p.PLoai == 2 && p.KieuDon == 9).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)) : (kq.Where(p => p.PLoai == 2 && (p.DTuong != "Dịch vụ" && p.DTuong != "BHYT") && (p.KieuDon == pl || pl == -1)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX)),
                                    //Xuất sản xuất
                                    XuatSX = kq.Where(p => p.PLoai == 2 && p.KieuDon == 10).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //Xuất lâm sàng
                                    XuatLS = kq.Where(p => p.PLoai == 2 && p.KieuDon == 11).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    //tồn cuối kỳ
                                    TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.TongXuatTKSL != 0).OrderBy(p => p.TenHamLuong).ToList();
                    string msg = "";
                    for (int i = 0; i < qnxt.Count; i++)
                    {
                        if (qnxt[i].TonCKSL == 0 && qnxt[i].TonCKTT != 0)
                        {
                            double tonCK = Math.Round(qnxt[i].TonCKTT, 1);
                            double TT = Math.Round((qnxt[i].TonCKSL * qnxt[i].DonGia), 1);
                            if (tonCK != TT)
                            {
                                msg += "- Thuốc: " + qnxt[i].TenHamLuong + ", MaDV: " + qnxt[i].MaDV + ", có số lượng cuối kỳ = " + qnxt[i].TonCKSL + ", đơn giá: " + qnxt[i].DonGia + " nhưng thành tiền cuối kỳ = " + qnxt[i].TonCKTT + "\n";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(msg))
                        MessageBox.Show(msg, "Thông báo những thuốc sai tiền cuối kỳ");
                    var _sq = (from a in qnxt
                               select new
                               {
                                   a.STT,
                                   a.DonGia,
                                   a.DonVi,
                                   a.MaCC,
                                   a.MaDV,
                                   a.TenTN,
                                   a.MaTam,
                                   NhapTKSL = a.NhapTKSL,
                                   a.TenHamLuong,
                                   a.TenNhomDuoc,
                                   NhapHD_SL = a.NhapHD,
                                   NhapTra_SL = a.NhapTra_SL,
                                   TongXuatTKSL = a.TongXuatTKSL,
                                   TonCKSL = a.TonCKSL,
                                   TonCKTT = a.TonCKTT,
                                   TonDKSL = a.TonDKSL,
                                   XuatTKTongSL = a.TongXuatTKSL,
                                   Xuatngtru = a.Xuatngtru,
                                   Xuatnoitru = a.Xuatnoitru,
                                   Xuatnoitru1 = a.Xuatnoitru1,
                                   Xuatnoitru2 = a.Xuatnoitru2,
                                   Xuatnoibo = a.Xuatnoibo,
                                   XuatNgoaiBV = a.XuatNgoaiBV,
                                   XuatDTNgTru = a.XuatDTNgTru,
                                   XuatCLS = a.XuatCLS,
                                   XuatTuTruc = a.XuatTuTruc,
                                   XuatPhongKham = a.XuatPhongKham,
                                   XuatKiemNghiem = a.XuatKiemNghiem,
                                   XuatKhac = a.XuatKhac,
                                   XuatSX = a.XuatSX,
                                   XuatLS = a.XuatLS,
                               }).OrderBy(p => p.TenNhomDuoc).ThenBy(p => p.STT).ThenBy(p => p.TenHamLuong).ToList();

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    string[] arr = { "", "", "", "", "", "", "" };
                    string[] arr1 = { "", "", "", "", "", "", "" };
                    int y = 0;
                    foreach (var item in _listxuatchon)
                    {
                        if (item.MaCot != "0")
                        {
                            if (y < 6)
                                arr[y] = item.MaCot;
                            else if (y >= 6)
                                arr1[y - 6] = item.MaCot;
                            y++;
                        }
                    }
                    var q = _sq.Where(p => (string.IsNullOrEmpty(_nhacc)) ? true : p.MaCC == _nhacc).ToList();
                    if (checkEdit1.Checked == false)
                    {
                        if (_listxuatchon.Where(p => p.MaCot != "0").Count() <= 6)
                        {
                            BaoCao.repBcNX_01071 rep = new BaoCao.repBcNX_01071(arr);
                            int x = 0;
                            foreach (var item in _listxuatchon.Where(p => p.MaCot != "0").ToList())
                            {

                                if (x == 0)
                                {
                                    rep.Xuat1.Text = item.TenCot;
                                }
                                if (x == 1)
                                {
                                    rep.Xuat2.Text = item.TenCot;
                                }
                                if (x == 2)
                                {
                                    rep.Xuat3.Text = item.TenCot;
                                }
                                if (x == 3)
                                {
                                    rep.Xuat4.Text = item.TenCot;
                                }
                                if (x == 4)
                                {
                                    rep.Xuat5.Text = item.TenCot;
                                }
                                if (x == 5)
                                {
                                    rep.Xuat6.Text = item.TenCot;
                                }
                                x++;
                            }
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                        else if (_listxuatchon.Where(p => p.MaCot != "0").Count() > 6)
                        {
                            BaoCao.repBcNX_01071 rep = new BaoCao.repBcNX_01071(arr);
                            int x = 0;
                            foreach (var item in _listxuatchon.Where(p => p.MaCot != "0").ToList())
                            {

                                if (x == 0)
                                {
                                    rep.Xuat1.Text = item.TenCot;
                                }
                                if (x == 1)
                                {
                                    rep.Xuat2.Text = item.TenCot;
                                }
                                if (x == 2)
                                {
                                    rep.Xuat3.Text = item.TenCot;
                                }
                                if (x == 3)
                                {
                                    rep.Xuat4.Text = item.TenCot;
                                }
                                if (x == 4)
                                {
                                    rep.Xuat5.Text = item.TenCot;
                                }
                                if (x == 5)
                                {
                                    rep.Xuat6.Text = item.TenCot;
                                }
                                x++;
                            }
                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();

                            BaoCao.repBcNX_01071 rep2 = new BaoCao.repBcNX_01071(arr1);
                            x = 0;
                            foreach (var item in _listxuatchon.Where(p => p.MaCot != "0").Skip(6).ToList())
                            {

                                if (x == 0)
                                {
                                    rep2.Xuat1.Text = item.TenCot;
                                }
                                if (x == 1)
                                {
                                    rep2.Xuat2.Text = item.TenCot;
                                }
                                if (x == 2)
                                {
                                    rep2.Xuat3.Text = item.TenCot;
                                }
                                if (x == 3)
                                {
                                    rep2.Xuat4.Text = item.TenCot;
                                }
                                if (x == 4)
                                {
                                    rep2.Xuat5.Text = item.TenCot;
                                }
                                if (x == 5)
                                {
                                    rep2.Xuat6.Text = item.TenCot;
                                }
                                x++;
                            }
                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo NXT", "C:\\TsBCNXT_RG.xls", true, this.Name);
                            rep2.TuNgay.Value = dateTuNgay.Text;
                            rep2.DenNgay.Value = dateDenNgay.Text;
                            rep2.Kho.Value = _tenkho;
                            rep2.DataSource = q;
                            rep2.NhaCC.Value = _tenNCC;
                            rep2.BindingData();
                            rep2.CreateDocument();
                            frm.prcIN.PrintingSystem = rep2.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "01049")
                        {
                            BaoCao.repBcNX_01049_maumoi rep = new BaoCao.repBcNX_01049_maumoi(); 
                            frmIn frm = new frmIn();
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            BaoCao.repBcNX_0107_maumoi rep = new BaoCao.repBcNX_0107_maumoi();
                            frmIn frm = new frmIn();
                            rep.TuNgay.Value = dateTuNgay.Text;
                            rep.DenNgay.Value = dateDenNgay.Text;
                            rep.Kho.Value = _tenkho;
                            rep.DataSource = q;
                            rep.NhaCC.Value = _tenNCC;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                groupControl3.Visible = false;
                lookUpEdit1.Visible = true;

            }
            else
            {
                groupControl3.Visible = true;
                lookUpEdit1.Visible = false;
            }
        }
    }
}