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
    public partial class frm_BCSDThuocBV : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCSDThuocBV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCSDThuocBV_Load(object sender, EventArgs e)
        {

            
            //cbo_cot1.Properties.DataSource = ReturnList(true);
            //cbo_cot2.Properties.DataSource = ReturnList(true);
            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            List<NhaCC> _Nhacc = new List<NhaCC>();
            _Nhacc = data.NhaCCs.ToList();
            _Nhacc.Add(new NhaCC { MaCC = "-1", TenCC = "Tất cả" });
            lupNhaCC.Properties.DataSource = _Nhacc.ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lnhom.Add(new NhomDV { IDNhom = -1, TenNhom = " Tất cả" });
            lupNhom.Properties.DataSource = _lnhom.OrderBy(p => p.TenNhom).ToList();
            var dskp = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            lup_KhoTong.Properties.DataSource = dskp;
            cklKP.DataSource = dskp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
                else
                    cklKP.SetItemChecked(i, false);
            }
            //if (radMauIn.SelectedIndex == 0)
            //{
            //    radLoaiXuat.Enabled = false;
            //    cbo_cot1.Properties.ReadOnly = true;
            //    cbo_cot2.Properties.ReadOnly = true;
            //}
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if(tungay > denngay)
            {
                MessageBox.Show("Ngày từ không được lớn hơn ngày đến");

            }
            else
            {
                List<KPhong> _kpChon = new List<KPhong>();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int idnhom = -1, idtieunhom = -1;
                //string _tenc1 = "", _tenc2 = "";
                string _tenkho = "", _tenNCC = "";
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemChecked(i))
                        _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                }
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
                // kiểm tra lại
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
                    int maKhoTong = lup_KhoTong.EditValue == null ? 0 : Convert.ToInt32(lup_KhoTong.EditValue);
                    var dv = (from a in data.DichVus
                              join b in data.NhomDVs on a.IDNhom equals b.IDNhom
                              join c in data.TieuNhomDVs on a.IdTieuNhom equals c.IdTieuNhom
                              where (idnhom == -1 ? true : b.IDNhom == idnhom) && (idtieunhom == -1 ? true : c.IdTieuNhom == idtieunhom)
                              select new
                              {
                                  a.MaDV,
                                  a.TenDV,
                                  b.TenNhom,
                                  b.TenNhomCT,
                                  c.TenRG,
                                  c.TenTN,
                                  a.NuocSX,
                                  a.MaCC,
                                  a.MaTam
                              }).ToList();
                    var nhap = (from a in data.NhapDs
                                join b in data.NhapDcts on a.IDNhap equals b.IDNhap
                                select new
                                {
                                    b.MaDV,
                                    a.NgayNhap,
                                    a.PLoai,
                                    SoLuongN = a.PLoai == 1 ? b.SoLuongN : 0,
                                    SoLuongX = (a.PLoai == 2 || a.PLoai == 3) ? b.SoLuongX : 0,
                                    ThanhTienN = a.PLoai == 1 ? b.ThanhTienN : 0,
                                    ThanhTienX = (a.PLoai == 2 || a.PLoai == 3) ? b.ThanhTienX : 0,
                                    b.SoLuongSD,
                                    b.DonVi,
                                    a.MaKP,
                                    a.KieuDon,
                                    b.ThanhTienSD
                                }).ToList();
                    var daata = (from a in nhap
                                 join b in dv on a.MaDV equals b.MaDV
                                 join kp in _kpChon.Where(p => p.MaKP != maKhoTong)
                                    on a.MaKP equals kp.MaKP
                                 group new { a, b } by new { a.MaDV, b.TenDV, b.MaCC, b.NuocSX, b.MaTam, a.DonVi } into kq
                                 select new
                                 {
                                     kq.Key.MaDV,
                                     kq.Key.TenDV,
                                     kq.Key.MaCC,
                                     kq.Key.NuocSX,
                                     kq.Key.MaTam,
                                     kq.Key.DonVi,
                                     //tồn đầu kỳ
                                     TonDKSL = -kq.Where(p => p.a.NgayNhap < tungay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX),
                                     TonDKTT = -kq.Where(p => p.a.NgayNhap < tungay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX),
                                     //nhập theo hóa đơn
                                     NhapHD = 0.0,
                                     NhapHDTT = 0.0,
                                     //Nhập trả lại
                                     NhapTL = 0.0,
                                     NhapTLTT = 0.0,
                                     //Nhập chuyển kho
                                     NhapCK = 0.0,
                                     NhapCKTT = 0.0,
                                     //thành tiền
                                     ThanhTien =0.0,
                                     // Xuất trong kỳ
                                     XuatTKSL = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 2 && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX),
                                     XuatTKTT = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 2 && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX),
                                     //Sử dụng trong kỳ
                                     SDTKSL = 0.0,
                                     SDTKTT = 0.0,
                                     //Tồn cuối kỳ
                                     TonCKSL = -kq.Where(p => p.a.NgayNhap <= denngay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX),
                                     TonCKTT = -kq.Where(p => p.a.NgayNhap <= denngay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX),

                                 }).ToList();

                    var daata1 = (from a in nhap.Where(p => p.MaKP == maKhoTong)
                                  join b in dv on a.MaDV equals b.MaDV
                                  group new { a, b } by new { a.MaDV, b.TenDV, b.MaCC, b.NuocSX, b.MaTam, a.DonVi } into kq
                                  select new
                                  {
                                      kq.Key.MaDV,
                                      kq.Key.TenDV,
                                      kq.Key.MaCC,
                                      kq.Key.NuocSX,
                                      kq.Key.MaTam,
                                      kq.Key.DonVi,
                                      //tồn đầu kỳ
                                      TonDKSL = kq.Where(p => p.a.NgayNhap < tungay && p.a.PLoai == 1).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap < tungay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX) - kq.Where(p => p.a.NgayNhap < tungay && p.a.PLoai == 5).Sum(p => p.a.SoLuongSD),
                                      TonDKTT = kq.Where(p => p.a.NgayNhap < tungay && p.a.PLoai == 1).Sum(p => p.a.ThanhTienN) - kq.Where(p => p.a.NgayNhap < tungay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX) - kq.Where(p => p.a.NgayNhap < tungay && p.a.PLoai == 5).Sum(p => p.a.ThanhTienSD),
                                      //nhập theo hóa đơn
                                      NhapHD = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 1).Sum(p => p.a.SoLuongN),
                                      NhapHDTT = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 1).Sum(p => p.a.ThanhTienN),
                                      //Nhập trả lại
                                      NhapTL = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.SoLuongN),
                                      NhapTLTT = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 2).Sum(p => p.a.ThanhTienN),
                                      //Nhập chuyển kho
                                      NhapCK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 0).Sum(p => p.a.SoLuongN),
                                      NhapCKTT = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1 && p.a.KieuDon == 0).Sum(p => p.a.ThanhTienN),
                                      //thành tiền
                                      ThanhTien = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay).Where(p => p.a.PLoai != 2).Sum(p => p.a.ThanhTienN),
                                      // Xuất trong kỳ
                                      XuatTKSL = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 2 && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX),
                                      XuatTKTT = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay && p.a.PLoai == 2 && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX),
                                      //Sử dụng trong kỳ
                                      SDTKSL = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongSD),
                                      SDTKTT = kq.Where(p => p.a.NgayNhap >= tungay && p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienSD),
                                      //Tồn cuối kỳ
                                      TonCKSL = kq.Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1).Sum(p => p.a.SoLuongN) - kq.Where(p => p.a.NgayNhap <= denngay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.SoLuongX) - kq.Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 5).Sum(p => p.a.SoLuongSD),
                                      TonCKTT = kq.Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 1).Sum(p => p.a.ThanhTienN) - kq.Where(p => p.a.NgayNhap <= denngay && (p.a.PLoai == 2 || p.a.PLoai == 3) && p.a.KieuDon != 2 && p.a.KieuDon != 3).Sum(p => p.a.ThanhTienX) - kq.Where(p => p.a.NgayNhap <= denngay && p.a.PLoai == 5).Sum(p => p.a.ThanhTienSD),

                                  }).ToList();

                    var union = daata.Union(daata1);
                    var daata2 = (from a in union
                                  group a by new { a.MaDV, a.TenDV, a.MaCC, a.NuocSX, a.MaTam, a.DonVi } into kq
                                  select new
                                  {
                                      kq.Key.MaDV,
                                      kq.Key.TenDV,
                                      kq.Key.MaCC,
                                      kq.Key.NuocSX,
                                      kq.Key.MaTam,
                                      kq.Key.DonVi,

                                      TonDKSL = kq.Sum(p => p.TonDKSL),
                                      TonDKTT = kq.Sum(p => p.TonDKTT),
                                      //nhập theo hóa đơn
                                      NhapHD = kq.Sum(p => p.NhapHD),
                                      NhapHDTT = kq.Sum(p => p.NhapHDTT),
                                      //Nhập trả lại
                                      NhapTL = kq.Sum(p => p.NhapTL),
                                      NhapTLTT = kq.Sum(p => p.NhapTLTT),
                                      //Nhập chuyển kho
                                      NhapCK = kq.Sum(p => p.NhapCK),
                                      NhapCKTT = kq.Sum(p => p.NhapCKTT),
                                      //thành tiền
                                      ThanhTien = kq.Sum(p => p.ThanhTien),
                                      // Xuất trong kỳ
                                      XuatTKSL = kq.Sum(p => p.XuatTKSL),
                                      XuatTKTT = kq.Sum(p => p.XuatTKTT),
                                      //Sử dụng trong kỳ
                                      SDTKSL = kq.Sum(p => p.SDTKSL),
                                      SDTKTT = kq.Sum(p => p.SDTKTT),
                                      //Tồn cuối kỳ
                                      TonCKSL = kq.Sum(p => p.TonCKSL),
                                      TonCKTT = kq.Sum(p => p.TonCKTT),
                                  }).ToList();
                    daata2 = daata2.Where(p => (string.IsNullOrEmpty(_nhacc) || _nhacc == "-1") ? true : p.MaCC == _nhacc).ToList();
                    string[] _arr = new string[] { "0", "@", "@", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[daata2.Count + 1, 15];
                    string[] _tieude = { "STT", "TenThuoc", "DVT", "SL tồn ĐK", "TT Tồn ĐK", "Nhập HĐTK ", "Nhập TLTK", "Nhập CKTK", "Thành tiền", "SL xuất TK", "TT Xuất TK", "SL Sử dụng TK", "TT Sử dụng TK", "Tồn CK - SL", "Tồn CK - TT" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    //for (int i = 0; i <= 17; i++) {
                    //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                    //}
                    foreach (var r in daata2)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.TonDKSL;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKTT;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.NhapHD;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTL;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.NhapCK;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTien;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.XuatTKSL;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.XuatTKTT;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.SDTKSL;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.SDTKTT;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TonCKSL;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.TonCKTT;
                        num++;
                    }
                    BaoCao.rep_BCSDThuocBV rep = new BaoCao.rep_BCSDThuocBV();
                    frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo sử dụng thuốc bệnh viện", "C:\\BCSDThuocBV.xls", true, this.Name);
                    rep.DataSource = daata2;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn kho");
                }

            }
            
           
        }

        #region function ReturnList
        //private List<NoiDung> ReturnList(bool tt)
        //{
        //    //true: Load loại xuất theo kiểu đơn, false: loại xuất theo đối tượng bệnh nhân
        //    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    List<NoiDung> _lNoiDung = new List<NoiDung>();
        //    if (tt)
        //    {
        //        _lNoiDung.Clear();
        //        List<DungChung.Bien.c_PhanLoaiXuat> list = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
        //        foreach (var a in list)
        //        {
        //            _lNoiDung.Add(new NoiDung { Id = a.Id, Ten = a.PhanLoai });
        //        }
        //        //_lNoiDung.Add(new NoiDung { Id = 0, Ten = "Xuất ngoại trú" });
        //        //_lNoiDung.Add(new NoiDung { Id = 1, Ten = "Xuất nội trú" });
        //        //_lNoiDung.Add(new NoiDung { Id = 2, Ten = "Xuất nội bộ" });
        //        //_lNoiDung.Add(new NoiDung { Id = 3, Ten = "Xuất ngoài BV" });
        //        //_lNoiDung.Add(new NoiDung { Id = 4, Ten = "Xuất nhân dân" });
        //        //_lNoiDung.Add(new NoiDung { Id = 5, Ten = "Xuất Cận Lâm Sàng" });
        //        //_lNoiDung.Add(new NoiDung { Id = 6, Ten = "Xuất tủ trực" });
        //        //_lNoiDung.Add(new NoiDung { Id = 7, Ten = "Xuất phòng khám" });
        //        //_lNoiDung.Add(new NoiDung { Id = 8, Ten = "Xuất kiểm nghiệm" });
        //        //_lNoiDung.Add(new NoiDung { Id = 9, Ten = "Xuất khác" });
        //        return _lNoiDung.ToList();
        //    }
        //    else
        //    {
        //        _lNoiDung.Clear();
        //        NoiDung moi = new NoiDung();
        //        var dtbn = data.DTBNs.ToList();
        //        foreach (var item in dtbn)
        //        {
        //            moi = new NoiDung();
        //            moi.Id = item.IDDTBN;
        //            moi.Ten = item.DTBN1;
        //            _lNoiDung.Add(moi);
        //        }
        //        return _lNoiDung.ToList();
        //    }
        //}
        #endregion
    }
}