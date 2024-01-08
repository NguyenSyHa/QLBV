using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;


namespace QLBV.FormThamSo
{
    public partial class Frm_BcNXTTuTruc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTTuTruc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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
          
            return true;
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
        List<KhoaPhong> _lKPsd = new List<KhoaPhong>();
        private void Frm_BcNXTTuTruc_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var q = (from TK in data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) select new { TK.MaKP, TK.TenKP }).ToList();
            q.Insert(0, new { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaPhong.Properties.DataSource = q.ToList();
            if (DungChung.Bien.MaBV == "24012")
            {
                lupKhoaPhong.ItemIndex = 0;
            }
            var ld = (from a in data.NhomDVs.Where(p => p.Status == 1) select new { a.IDNhom, a.TenNhom }).ToList();
            ld.Insert(0, new { IDNhom = 0, TenNhom = "Tất cả" });
            lupLoaiDuoc.Properties.DataSource = ld.ToList();
            _lKPsd = (from kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                      select new KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = _lKPsd;
            //cmbPL.EditValue = "Thuốc";
        }
        //public static void _loadKPsd(string dsKPsd, List<KhoaPhong> _lKPsd, CheckedListBoxControl cklKP)
        //{

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(dsKPsd))
        //        {
        //            string[] kp = dsKPsd.Split(';');
        //            for (int i = 0; i < cklKP.ItemCount; i++)
        //            {

        //                cklKP.SetItemChecked(i, false);

        //            }
        //            foreach (var item in kp)
        //            {
        //                foreach (var item2 in _lKPsd)
        //                {
        //                    if (!string.IsNullOrEmpty(item))
        //                        if (Convert.ToInt32(item) == item2.MaKP)
        //                        {
        //                            item2.Check = true;
        //                            for (int i = 0; i < cklKP.ItemCount; i++)
        //                            {
        //                                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == item2.MaKP)
        //                                {
        //                                    cklKP.SetItemChecked(i, true);
        //                                    break;
        //                                }
        //                            }
        //                            break;
        //                        }
        //                }
        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi load Danh sách khoa phòng sử dụng: " + ex.Message);
        //    }

        //}
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBcNXT())
            {
                if(DungChung.Bien.MaBV == "30007")
                {
                    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcNXTTuTruc_30007 rep = new BaoCao.Rep_BcNXTTuTruc_30007(ckHienThiTieuNhom.Checked);

                    //rep.TuNgay.Value = dateTuNgay.Text;
                    //rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + "  đến ngày " + dateDenNgay.Text;
                    int[] kho = new int[100];
                    for (int i = 0; i < 100; i++)
                        kho[i] = -1;
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                            kho[i] = Convert.ToInt32(cklKP.GetItemValue(i));
                    }
                    int _kp = 0, _ld = 0;
                    if (lupKhoaPhong.EditValue != null)
                        _kp = lupKhoaPhong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaPhong.EditValue);
                    if(lupLoaiDuoc.EditValue != null)
                        _ld = lupLoaiDuoc.EditValue == null ? 0 : Convert.ToInt32(lupLoaiDuoc.EditValue);
                    //int _tt = 0;
                    //if (lupTuTruc.EditValue != null)
                    //    _tt = Convert.ToInt32(lupTuTruc.EditValue);

                    List<int> ltt = new List<int>();
                    for (int i = 0; i < cklTuTruc.ItemCount; i++)
                    {
                        if (cklTuTruc.GetItemChecked(i))
                            ltt.Add(Convert.ToInt32(cklTuTruc.GetItemValue(i)));
                    }
                    var qtenxp = (from xp in data.KPhongs.Where(p => p.MaKP == _kp)
                                  select new { xp.TenKP }).ToList();
                    if (qtenxp.Count > 0)
                    {
                        rep.TenKP.Value = (qtenxp.First().TenKP);
                    }
                    rep.TieuDe.Value = ("báo cáo nhập - xuất - tồn ").ToUpper();
                    double a = 0;
                    var dv1 = (from dv in data.DichVus.Where(p => _ld == 0 || p.IDNhom == _ld)
                               select new { dv.MaDV, dv.TenDV, dv.DonVi, dv.IdTieuNhom, dv.MaTam }).ToList();

                    var nhapduoc = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2))
                                    join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    select new {nhapdct.MaDV, nhapd.MaKP, nhapd.MaKPnx, nhapdct.DonGia,
                                                SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                                SoLuongX = (nhapd.PLoai == 2) ? nhapdct.SoLuongX : 0,
                                                ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0,
                                                ThanhTienX = (nhapd.PLoai == 2) ? nhapdct.ThanhTienX : 0,
                                        nhapd.NgayNhap }).ToList();

                    var qnxt2 = (from nhapd in nhapduoc
                                 join dv in dv1 on nhapd.MaDV equals dv.MaDV
                                 select new { nhapd.MaKP, nhapd.MaKPnx, dv.MaDV, dv.TenDV, dv.DonVi, nhapd.DonGia, nhapd.SoLuongX, nhapd.ThanhTienX, nhapd.SoLuongN, nhapd.ThanhTienN, nhapd.NgayNhap, dv.IdTieuNhom, dv.MaTam }).ToList();
                    
                    List<BC> qnxt = (from dv in qnxt2.Where(p => ltt.Contains(p.MaKPnx ?? 0))//(_tt > 0 ? p.MaKPnx == _tt : true))
                                //  join kh in kho on dv.MaKP equals kh
                                group new { dv } by new { dv.MaKP, dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, dv.IdTieuNhom, dv.MaTam } into kq
                                select new BC
                                {
                                    MaDV = kq.Key.MaDV,
                                    MaNB = kq.Key.MaTam,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TonDKSL = kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.SoLuongX) - kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.SoLuongN),
                                    TonDKTT = kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.ThanhTienN),

                                    NhapTKSL = kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongX) - kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongN),
                                    NhapTKTT = kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienN),

                                    XuatTKSL = a,
                                    XuatTKTT = a,

                                    TonCKSL = kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongX) - kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongN),
                                    TonCKTT = kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienN),
                                }).ToList();


                    // chưa trừ trường hợp hủy số phiếu lĩnh

                    var ttruc = (from kp in data.KPhongs.Where(p => p.PLoai == "Tủ trực" && ltt.Contains(p.MaKP)) //(_tt>0?p.MaKP==_tt:true) )
                                 select kp).ToList();
                    var dt1 = (from dthuoc in data.DThuocs
                               join dthuocct in data.DThuoccts.Where(P => P.Status != 2) on dthuoc.IDDon equals dthuocct.IDDon
                               where (dthuocct.SoLuong > 0 || (dthuocct.SoLuong < 0 && dthuoc.KieuDon == 2))
                               select new {dthuocct.MaDV, dthuoc.MaKXuat, dthuoc.NgayKe, dthuocct.DonGia, dthuocct.ThanhTien, dthuocct.SoLuong }).ToList();
                    var qxtt2 = (from dthuoc in dt1
                                 join dv in dv1 on dthuoc.MaDV equals dv.MaDV // kiểu trả thuốc chỉ lấy những thuốc đã kê cho bệnh nhân trong khoa xuất từ tủ trực 
                                 select new { dthuoc.MaKXuat, dthuoc.NgayKe, dv.MaDV, dv.TenDV, dv.IdTieuNhom, dv.DonVi, dthuoc.DonGia, dthuoc.ThanhTien, dthuoc.SoLuong, dv.MaTam }).ToList();
                    
                    List<BC> qxtt = (from dv in qxtt2
                                join tt in ttruc on dv.MaKXuat equals tt.MaKP
                                group new { dv } by new { dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, dv.IdTieuNhom, dv.MaTam } into kq
                                select new BC
                                {
                                    MaDV = kq.Key.MaDV,
                                    MaNB = kq.Key.MaTam,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TonDKSL = kq.Where(p => p.dv.NgayKe < tungay).Sum(p => p.dv.SoLuong) * (-1),
                                    TonDKTT = kq.Where(p => p.dv.NgayKe < tungay).Sum(p => p.dv.ThanhTien) * (-1),

                                    NhapTKSL = a,
                                    NhapTKTT = a,

                                    XuatTKSL = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong),
                                    XuatTKTT = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.ThanhTien),

                                    TonCKSL = kq.Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong) * (-1),
                                    TonCKTT = kq.Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.ThanhTien) * (-1),
                                }).ToList();
                    qnxt.AddRange(qxtt);
                    //var q = qnxt.Union(qxtt).Select(x => new
                    //{
                    //    x.TenDV,
                    //    x.MaDV,
                    //    x.MaNB,
                    //    x.DVT,
                    //    x.DonGia,
                    //    x.IdTieuNhom,
                    //    sltondk = x.TonDKSL,
                    //    tttondk = x.TonDKTT,
                    //    slnhap = x.NhapTKSL,
                    //    ttnhap = x.NhapTKTT,
                    //    slxuat = x.XuatTKSL,
                    //    ttxuat = x.XuatTKTT,
                    //    sltonck = x.TonDKSL + x.NhapTKSL - x.XuatTKSL,
                    //    tttonck = x.TonDKTT + x.NhapTKTT + x.XuatTKTT,
                    //}).ToList();

                    var qtn = data.TieuNhomDVs.ToList();
                    var nxt = (from q1 in qnxt
                               join tn in qtn on q1.IdTieuNhom equals tn.IdTieuNhom
                               group new { q1 } by new { q1.MaDV, q1.TenDV, q1.DonGia, q1.DVT, tn.IdTieuNhom, tn.TenTN, q1.MaNB } into kq
                               select new
                               {
                                   TieuNhomDV = kq.Key.TenTN,
                                   TenDV = kq.Key.TenDV,
                                   MaNB = kq.Key.MaNB,
                                   MaDV = kq.Key.MaDV,
                                   DVT = kq.Key.DVT,
                                   DonGia = kq.Key.DonGia,
                                   sltondk = kq.Sum(p => p.q1.TonDKSL),
                                   tttondk = kq.Sum(p => p.q1.TonDKTT),
                                   slnhap = kq.Sum(p => p.q1.NhapTKSL),
                                   ttnhap = kq.Sum(p => p.q1.NhapTKTT),
                                   slxuat = kq.Sum(p => p.q1.XuatTKSL),
                                   ttxuat = kq.Sum(p => p.q1.XuatTKTT),
                                   sltonck = kq.Sum(p => p.q1.TonDKSL) + kq.Sum(p => p.q1.NhapTKSL) - kq.Sum(p => p.q1.XuatTKSL),
                                   tttonck = kq.Sum(p => p.q1.TonDKTT) + kq.Sum(p => p.q1.NhapTKTT) - kq.Sum(p => p.q1.XuatTKTT),
                               }).Where(p => p.sltondk != 0 || p.slnhap != 0 || p.slxuat != 0 || p.sltonck != 0).ToList();
                    rep.DataSource = nxt.OrderBy(p => p.TenDV);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcNXTTuTruc rep = new BaoCao.Rep_BcNXTTuTruc(ckHienThiTieuNhom.Checked);

                    //rep.TuNgay.Value = dateTuNgay.Text;
                    //rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + "  đến ngày " + dateDenNgay.Text;
                    int[] kho = new int[100];
                    for (int i = 0; i < 100; i++)
                        kho[i] = -1;
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                            kho[i] = Convert.ToInt32(cklKP.GetItemValue(i));
                    }
                    int _kp = 0, _ld = 0;
                    if (lupKhoaPhong.EditValue != null)
                        _kp = lupKhoaPhong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaPhong.EditValue);
                    if (lupLoaiDuoc.EditValue != null)
                        _ld = lupLoaiDuoc.EditValue == null ? 0 : Convert.ToInt32(lupLoaiDuoc.EditValue);
                    //int _tt = 0;
                    //if (lupTuTruc.EditValue != null)
                    //    _tt = Convert.ToInt32(lupTuTruc.EditValue);

                    List<int> ltt = new List<int>();
                    for (int i = 0; i < cklTuTruc.ItemCount; i++)
                    {
                        if (cklTuTruc.GetItemChecked(i))
                            ltt.Add(Convert.ToInt32(cklTuTruc.GetItemValue(i)));
                    }

                    var qtenxp = (from xp in data.KPhongs.Where(p => p.MaKP == _kp)
                                  select new { xp.TenKP }).ToList();
                    if (qtenxp.Count > 0)
                    {
                        rep.TenKP.Value = (qtenxp.First().TenKP);
                    }
                    rep.TieuDe.Value = DungChung.Bien.MaBV == "01830" ? ("danh mục thuốc bàn giao tử trực").ToUpper() : ("báo cáo nhập - xuất - tồn ").ToUpper();
                    double a = 0;
                    var dv1 = (from dv in data.DichVus.Where(p => _ld == 0 || p.IDNhom == _ld)
                               select new { dv.MaDV, dv.TenDV, dv.DonVi, dv.IdTieuNhom, dv.MaTam }).ToList();

                    var nhapduoc = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2))
                                    join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    select new { nhapdct.MaDV, nhapd.MaKP, nhapd.MaKPnx, nhapdct.DonGia,
                                                 SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                                 SoLuongX = (nhapd.PLoai == 2) ? nhapdct.SoLuongX : 0,
                                                 ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0,
                                                 ThanhTienX = (nhapd.PLoai == 2) ? nhapdct.ThanhTienX : 0,
                                        nhapd.NgayNhap }).ToList();

                    var qnxt2 = (from nhapd in nhapduoc
                                 join dv in dv1 on nhapd.MaDV equals dv.MaDV
                                 select new { nhapd.MaKP, nhapd.MaKPnx, dv.MaDV, dv.TenDV, dv.DonVi, nhapd.DonGia, nhapd.SoLuongX, nhapd.ThanhTienX, nhapd.SoLuongN, nhapd.ThanhTienN, nhapd.NgayNhap, dv.IdTieuNhom, dv.MaTam }).ToList();
                    List<BC> qnxt = (from dv in qnxt2.Where(p => ltt.Contains(p.MaKPnx ?? 0))//(_tt > 0 ? p.MaKPnx == _tt : true))
                                //  join kh in kho on dv.MaKP equals kh
                                group new { dv } by new { dv.MaKP, dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, dv.IdTieuNhom, dv.MaTam } into kq
                                select new BC
                                {
                                    MaDV = kq.Key.MaDV,
                                    MaNB = kq.Key.MaTam,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TonDKSL = kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.SoLuongX) - kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.SoLuongN),
                                    TonDKTT = kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => p.dv.NgayNhap < tungay).Sum(p => p.dv.ThanhTienN),

                                    NhapTKSL = kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongX) - kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongN),
                                    NhapTKTT = kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => kho.Contains(p.dv.MaKP ?? 0)).Where(p => p.dv.NgayNhap >= tungay).Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienN),

                                    XuatTKSL = a,
                                    XuatTKTT = a,

                                    TonCKSL = kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongX) - kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.SoLuongN),
                                    TonCKTT = kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienX) - kq.Where(p => p.dv.NgayNhap <= denngay).Sum(p => p.dv.ThanhTienN),
                                }).ToList();


                    // chưa trừ trường hợp hủy số phiếu lĩnh

                    var ttruc = (from kp in data.KPhongs.Where(p => p.PLoai == "Tủ trực" && ltt.Contains(p.MaKP)) //(_tt>0?p.MaKP==_tt:true) )
                                 select kp).ToList();
                    var dt1 = (from dthuoc in data.DThuocs
                               join dthuocct in data.DThuoccts.Where(P => P.Status != 2) on dthuoc.IDDon equals dthuocct.IDDon
                               where (dthuocct.SoLuong > 0 || (dthuocct.SoLuong < 0 && dthuoc.KieuDon == 2))
                               select new { dthuocct.MaDV, dthuoc.MaKXuat, dthuoc.NgayKe, dthuocct.DonGia, dthuocct.ThanhTien, dthuocct.SoLuong }).ToList();
                    var qxtt2 = (from dthuoc in dt1
                                 join dv in dv1 on dthuoc.MaDV equals dv.MaDV // kiểu trả thuốc chỉ lấy những thuốc đã kê cho bệnh nhân trong khoa xuất từ tủ trực 
                                 select new { dthuoc.MaKXuat, dthuoc.NgayKe, dv.MaDV, dv.TenDV, dv.IdTieuNhom, dv.DonVi, dthuoc.DonGia, dthuoc.ThanhTien, dthuoc.SoLuong, dv.MaTam }).ToList();
                    List<BC> qxtt = (from dv in qxtt2
                                join tt in ttruc on dv.MaKXuat equals tt.MaKP
                                group new { dv } by new { dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, dv.IdTieuNhom, dv.MaTam } into kq
                                select new BC
                                {
                                    MaDV = kq.Key.MaDV,
                                    MaNB = kq.Key.MaTam,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TonDKSL = kq.Where(p => p.dv.NgayKe < tungay).Sum(p => p.dv.SoLuong) * (-1),
                                    TonDKTT = kq.Where(p => p.dv.NgayKe < tungay).Sum(p => p.dv.ThanhTien) * (-1),

                                    NhapTKSL = a,
                                    NhapTKTT = a,

                                    XuatTKSL = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong),
                                    XuatTKTT = kq.Where(p => p.dv.NgayKe >= tungay).Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.ThanhTien),

                                    TonCKSL = kq.Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.SoLuong) * (-1),
                                    TonCKTT = kq.Where(p => p.dv.NgayKe <= denngay).Sum(p => p.dv.ThanhTien) * (-1),
                                }).ToList();
                    qnxt.AddRange(qxtt);
                    //var q = qnxt.Union(qxtt).Select(x => new
                    //{
                    //    x.TenDV,
                    //    x.MaDV,
                    //    x.MaNB,
                    //    x.DVT,
                    //    x.DonGia,
                    //    x.IdTieuNhom,
                    //    sltondk = x.TonDKSL,
                    //    tttondk = x.TonDKTT,
                    //    slnhap = x.NhapTKSL,
                    //    ttnhap = x.NhapTKTT,
                    //    slxuat = x.XuatTKSL,
                    //    ttxuat = x.XuatTKTT,
                    //    sltonck = x.TonDKSL + x.NhapTKSL - x.XuatTKSL,
                    //    tttonck = x.TonDKTT + x.NhapTKTT + x.XuatTKTT,
                    //}).ToList();

                    var qtn = data.TieuNhomDVs.ToList();
                    var nxt = (from q1 in qnxt
                               join tn in qtn on q1.IdTieuNhom equals tn.IdTieuNhom
                               group new { q1 } by new { q1.MaDV, q1.TenDV, q1.DonGia, q1.DVT, tn.IdTieuNhom, tn.TenTN, q1.MaNB } into kq
                               select new
                               {
                                   TieuNhomDV = kq.Key.TenTN,
                                   TenDV = kq.Key.TenDV,
                                   MaNB = kq.Key.MaNB,
                                   MaDV = kq.Key.MaDV,
                                   DVT = kq.Key.DVT,
                                   DonGia = kq.Key.DonGia,
                                   sltondk = kq.Sum(p => p.q1.TonDKSL),
                                   tttondk = kq.Sum(p => p.q1.TonDKTT),
                                   slnhap = kq.Sum(p => p.q1.NhapTKSL),
                                   ttnhap = kq.Sum(p => p.q1.NhapTKTT),
                                   slxuat = kq.Sum(p => p.q1.XuatTKSL),
                                   ttxuat = kq.Sum(p => p.q1.XuatTKTT),
                                   sltonck = kq.Sum(p => p.q1.TonDKSL) + kq.Sum(p => p.q1.NhapTKSL) - kq.Sum(p => p.q1.XuatTKSL),
                                   tttonck = kq.Sum(p => p.q1.TonDKTT) + kq.Sum(p => p.q1.NhapTKTT) - kq.Sum(p => p.q1.XuatTKTT),
                               }).ToList();
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30002")
                    {
                        nxt = nxt.Where(p => p.sltondk > 0 || p.slnhap > 0).Where(p => p.sltondk != 0 || p.slnhap != 0 || p.slxuat != 0 || p.sltonck != 0).ToList();
                    }
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        var _kq = nxt.Where(p => p.sltondk > 0 || p.slnhap > 0).ToList();
                        rep.DataSource = _kq.OrderBy(p => p.TenDV);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {

                        rep.DataSource = nxt.Where(p => DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30002" ? true : p.sltondk > 0 || p.slnhap > 0).OrderBy(p => p.TenDV);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            int _tt = 0;

            if (lupKhoaPhong.EditValue.ToString() != null)
            {
                if (lupKhoaPhong.Text == "Tất cả")
                {
                    var qtt = (from kp in data.KPhongs.Where(p => p.PLoai == "Tủ trực") select new { kp.MaKP, kp.TenKP }).ToList();
                    cklTuTruc.DataSource = qtt;
                    if (DungChung.Bien.MaBV != "24012")
                    {
                        cklTuTruc.CheckAll();
                    }
                }
                else
                {
                    _tt = Convert.ToInt32(lupKhoaPhong.EditValue);
                    string MKP = _tt.ToString();
                    var qtt = (from kp in data.KPhongs.Where(p => p.PLoai == "Tủ trực" && (DungChung.Bien.MaBV == "24012" ? (p.MaKPsd != null ? p.MaKPsd.Contains(MKP) : true) : p.NhomKP == _tt)) select new { kp.MaKP, kp.TenKP }).ToList();
                    //lupTuTruc.Properties.DataSource = qtt;
                    cklTuTruc.DataSource = qtt;
                    cklTuTruc.CheckAll();
                }
            }

        }
        class BC
        {
            public int MaDV { get; set; }

            public string MaNB { get; set; }

            public string TenDV { get; set; }

            public string DVT { get; set; }

            public double DonGia { get; set; }

            public int? IdTieuNhom { get; set; }

            public double TonDKSL { get; set; }

            public double TonDKTT { get; set; }

            public double NhapTKSL { get; set; }

            public double NhapTKTT { get; set; }

            public double XuatTKSL { get; set; }

            public double XuatTKTT { get; set; }

            public double TonCKSL { get; set; }

            public double TonCKTT { get; set; }
        }
    }
}

    

