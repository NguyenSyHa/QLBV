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
    public partial class frmTsBcSuDungThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcSuDungThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            if (radMau.SelectedIndex == 1)
            {
                if (lupKho.EditValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn kho");
                    lupKho.Focus();
                    return false;
                }
                return true;
            }
            else if (radMau.SelectedIndex == 2)
            {
                for (int i = 1; i < cklKhoaPhong.ItemCount; i++)
                {
                    if (cklKhoaPhong.GetItemChecked(i))
                    {
                        return true;
                    }
                }
                MessageBox.Show("Bạn chưa chọn kho");
                for (int i = 1; i < lupTenPLoai1.ItemCount; i++)
                {
                    if (lupTenPLoai1.GetItemChecked(i))
                    {
                        return true;
                    }
                }
                MessageBox.Show("Bạn chưa phân loại");
                return false;
            }
            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

            if (KTtaoBc())
            {
                frmIn frm = new frmIn();
                BaoCao.repBcSuDungThuoc rep = new BaoCao.repBcSuDungThuoc();
                var qcqcq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                string macqcq = "";
                if (qcqcq != null)
                {
                    rep.lblDonvi.Text = "Đơn vị: 1.000đ";
                    macqcq = qcqcq.MaChuQuan;
                }
                rep.TenBC.Value = "Báo cáo sử dụng".ToUpper();
                string nhom = "";
                if (lupTenPLoai.EditValue != null)
                {
                    nhom = lupTenPLoai.EditValue.ToString();
                }
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32(lupKho.EditValue.ToString());
                List<int> _lmakp = new List<int>();
                List<string> khoxuat = new List<string>();
                for (int i = 1; i < cklKhoaPhong.ItemCount; i++)
                {
                    if (cklKhoaPhong.GetItemChecked(i))
                    {
                        _lmakp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
                        khoxuat.Add(cklKhoaPhong.GetItemText(i));
                    }
                }
                List<BC> listBc = new List<BC>();
                List<int> lTuTruc = new List<int>();
                List<int> lKhoDuoc = new List<int>();
                List<KPhong> lkpAll = new List<KPhong>();
                List<KPhong> lkp = data.KPhongs.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) && p.Status == 1 && p.TrongBV == 1).ToList();
                if (lkp.Count > 0)
                {
                    lkpAll = (from kd in lkp join chon in _lmakp on kd.MaKP equals chon select kd).ToList();
                    if (lkpAll.Count > 0)
                    {
                        lTuTruc = (from kp in lkpAll where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc select kp.MaKP).ToList();
                        lKhoDuoc = (from kp in lkpAll where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc select kp.MaKP).ToList();

                    }
                }
                if(radMau.SelectedIndex == 0 || radMau.SelectedIndex == 1)
                rep.Kho.Value = lupKho.Text;
                else
                {
                    rep.Kho.Value = string.Join(", ", khoxuat);
                }
                if (ckInThang.Checked == true)
                {
                    rep.ThangNam.Value = "Tháng " + denngay.Month + " năm " + denngay.Year;
                }
                else rep.ThangNam.Value = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                string _pl = lupTenPLoai.Text;

                List<string> tenDV = new List<string>();
                if (DungChung.Bien.MaBV == "01071")
                {
                    for (int i = 1; i < lupTenPLoai1.ItemCount; i++)
                    {
                        if (lupTenPLoai1.GetItemChecked(i))
                        {
                            tenDV.Add(lupTenPLoai1.GetItemText(i));
                        }
                    }

                }
                if (!string.IsNullOrEmpty(_pl) || tenDV.Count > 0)
                {
                    #region nhóm thuốc trong danh mục, nhóm vtyt
                    if (nhom != "Nhóm hóa chất")
                    {
                        if (nhom == "Thuốc trong danh mục BHYT" || nhom == "Thuốc thanh toán theo tỷ lệ" || nhom == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục" || nhom == "Máu và chế phẩm của máu")
                        {
                            // _pl = "Thuốc trong danh mục BHYT";
                            rep.TenBC.Value = "BÁO CÁO SỬ DỤNG THUỐC";
                            rep.MS.Value = "05D/BV-01";
                            rep.TenDuoc.Value = "Tên thuốc, nồng độ, hàm lượng";
                            rep.TenCot1.Value = "Nội trú";
                            rep.TenCot2.Value = "Ngoại trú";
                        }

                        if (nhom == "Vật tư y tế trong danh mục BHYT" || nhom == "VTYT thanh toán theo tỷ lệ")
                        {
                            // _pl = "Nhóm vật tư y tế";
                            rep.TenBC.Value = "BÁO CÁO SỬ DỤNG VẬT TƯ Y TẾ TIÊU HAO";
                            rep.MS.Value = "09D/BV-01";
                            rep.TenDuoc.Value = "Tên vật tư y tế tiêu hao";
                            rep.TenCot1.Value = "Nội trú";
                            rep.TenCot2.Value = "Ngoại trú";
                        }
                        int _dvbc = 0;
                        if (lupDVBC.EditValue != null)
                            _dvbc = Convert.ToInt32(lupDVBC.EditValue.ToString());
                        #region mẫu chung
                        //chỉ lấy sử dụng dược của kho xã
                        if (radMau.SelectedIndex == 0)
                        {
                            var q = (from nhapd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3).Where(p => p.MaKP == _kho && (_dvbc == 0 ? true : p.MaKPnx == _dvbc) && p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                     join nhomdv in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT)) on dv.IDNhom equals nhomdv.IDNhom
                                     join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                     join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                                     group new { dv, nhapd, nhapdct } by new { dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, nhomdv.TenNhom, tn.TenTN } into kq
                                     select new
                                     {
                                         MaDV = kq.Key.MaDV,
                                         TenDV = kq.Key.TenDV,
                                         DonVi = kq.Key.DonVi,
                                         DonGia = kq.Key.DonGia,
                                         TenNhomThuoc = kq.Key.TenNhom,
                                         Tentn = kq.Key.TenTN,
                                         NoiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD),
                                         NoiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD),

                                         NgoaiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD),
                                         NgoaiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD),

                                         KhacSL = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD),
                                         KhacT = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD),

                                         HuySL = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX),
                                         HuyT = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX),

                                         TongCongSL = (kq.Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Sum(p => p.nhapdct.SoLuongSD)) + (kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX)),
                                         TongCongT = (kq.Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Sum(p => p.nhapdct.ThanhTienSD)) + (kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX)),

                                     }).ToList();


                            if (q.Count() > 0)
                            {
                                rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                                MessageBox.Show("Không có dữ liệu");
                        }
                        #endregion
                        #region mẫu bv Tam đường: dùng cho các kho xuất trực tiếp cho bệnh nhân (cả kho dược trong bv và kho xã phường)
                        else if (radMau.SelectedIndex == 1)
                        {

                            if (_kho > 0)
                            {

                                var qkp = data.KPhongs.Where(p => p.MaKP == _kho).FirstOrDefault();
                                if (qkp != null)
                                {
                                    var qdvu = (from n in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT))
                                                join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                                                join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                                select new { dv.MaDV, dv.TenDV, n.TenNhom, n.TenNhomCT, tn.TenTN }).ToList();
                                    #region xã phường
                                    //if (qkp.MaBVsd != "12001")
                                    //{
                                    //    var qnd = (from nhapd in data.NhapDs.Where(p => p.PLoai == 5).Where(p => p.MaKP == _kho && p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    //              join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    //              select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon, nhapdct.SoLuongX, nhapdct.SoLuongSD, nhapdct.ThanhTienX, nhapdct.ThanhTienSD }).ToList();
                                    //    var qkq1 = (from nd in qnd
                                    //                group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                    //                select new
                                    //                {
                                    //                    MaDV = kq.Key.MaDV,
                                    //                    DonVi = kq.Key.DonVi,
                                    //                    DonGia = kq.Key.DonGia,

                                    //                    NoiTruSL = kq.Where(p => p.PLoai == 5 && p.KieuDon == 1).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 1).Sum(p => p.SoLuongSD),
                                    //                    NoiTruT = kq.Where(p => p.PLoai == 5 && p.KieuDon == 1).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 1).Sum(p => p.ThanhTienSD),

                                    //                    NgoaiTruSL = kq.Where(p => p.PLoai == 5 && p.KieuDon == 0).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 0).Sum(p => p.SoLuongSD),
                                    //                    NgoaiTruT = kq.Where(p => p.PLoai == 5 && p.KieuDon == 0).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 0).Sum(p => p.ThanhTienSD),

                                    //                    KhacSL = kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == null).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == null).Sum(p => p.SoLuongSD),
                                    //                    KhacT = kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 5 || p.KieuDon == 6  || p.KieuDon == null).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 5 || p.KieuDon == 6  || p.KieuDon == null).Sum(p => p.ThanhTienSD),

                                    //                    HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                    //                    HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX)
                                    //                }).ToList();


                                    //    var q = (from nd in qkq1 join dv in qdvu on nd.MaDV equals dv.MaDV                                             
                                    //              select new
                                    //              {
                                    //                  MaDV = dv.MaDV,
                                    //                  TenDV = dv.TenDV,
                                    //                  DonVi = nd.DonVi,
                                    //                  DonGia = nd.DonGia / 1000,
                                    //                  TenNhomThuoc = dv.TenNhom,
                                    //                  Tentn = dv.TenTN,
                                    //                  NoiTruSL = nd.NoiTruSL,
                                    //                  NoiTruT = nd.NoiTruT / 1000,
                                    //                  NgoaiTruSL = nd.NgoaiTruSL,
                                    //                  NgoaiTruT = nd.NgoaiTruT / 1000,
                                    //                  KhacSL = nd.KhacSL,
                                    //                  KhacT = nd.KhacT / 1000,
                                    //                  HuySL = nd.HuySL,
                                    //                  HuyT = nd.HuyT / 1000,
                                    //                  TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                    //                  TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000
                                    //              }).ToList();



                                    //    //var q1 = (from nhapd in data.NhapDs.Where(p => p.PLoai == 5).Where(p => p.MaKP == _kho && p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    //    //         join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    //    //         join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                    //    //         join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_pl)) on dv.IDNhom equals nhomdv.IDNhom
                                    //    //         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    //    //         group new { dv, nhapd, nhapdct } by new { dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, nhomdv.TenNhom, tn.TenTN } into kq
                                    //    //         select new
                                    //    //         {
                                    //    //             MaDV = kq.Key.MaDV,
                                    //    //             TenDV = kq.Key.TenDV,
                                    //    //             DonVi = kq.Key.DonVi,
                                    //    //             DonGia = kq.Key.DonGia,
                                    //    //             TenNhomThuoc = kq.Key.TenNhom,
                                    //    //             Tentn = kq.Key.TenTN,
                                    //    //             NoiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD),
                                    //    //             NoiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD),

                                    //    //             NgoaiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD),
                                    //    //             NgoaiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD),

                                    //    //             KhacSL = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD),
                                    //    //             KhacT = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD),

                                    //    //             HuySL = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX),
                                    //    //             HuyT = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX),


                                    //        //     }).ToList();

                                    //    //var q = (from 
                                    //    //         nd in q1
                                    //    //         select new
                                    //    //         {
                                    //    //             MaDV = nd.MaDV,
                                    //    //             TenDV = nd.TenDV,
                                    //    //             DonVi = nd.DonVi,
                                    //    //             DonGia = nd.DonGia,
                                    //    //             TenNhomThuoc = nd.TenNhomThuoc,
                                    //    //             Tentn = nd.Tentn,
                                    //    //             NoiTruSL = nd.NoiTruSL,
                                    //    //             NoiTruT = nd.NoiTruT,
                                    //    //             NgoaiTruSL = nd.NgoaiTruSL,
                                    //    //             NgoaiTruT =nd.NgoaiTruT,

                                    //    //             KhacSL = nd.KhacSL, 
                                    //    //             KhacT = nd.KhacT,
                                    //    //             HuySL =nd.HuySL,
                                    //    //             HuyT = nd.HuyT,
                                    //    //             TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL+ nd.KhacSL + nd.HuySL,
                                    //    //             TongCongT = nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT


                                    //    //         }).ToList();


                                    //    if (q.Count > 0)
                                    //    {
                                    //        rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                    //        rep.BindingData();
                                    //        rep.CreateDocument();
                                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    //        frm.ShowDialog();
                                    //    }
                                    //    else
                                    //        MessageBox.Show("Không có dữ liệu");
                                    //}
                                    #endregion
                                    #region kho dược (ko phải tủ trực)
                                    if (qkp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                                    {

                                        var qnd = (from nhapd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaKP == _kho).Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 & p.KieuDon == 2))
                                                   join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                                   select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.SoLuongSD, nhapdct.ThanhTienN, nhapdct.ThanhTienX, nhapdct.ThanhTienSD, nhapd.TraDuoc_KieuDon }).ToList();
                                        var qkq1 = (from nd in qnd
                                                    group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                {
                                                    MaDV = kq.Key.MaDV,
                                                    DonVi = kq.Key.DonVi,
                                                    DonGia = kq.Key.DonGia,
                                                    NoiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN)),
                                                    NoiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN)),
                                                    NgoaiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.SoLuongN)),
                                                    NgoaiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.ThanhTienN)),
                                                    KhacSL = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 2 && p.KieuDon != 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 2 && p.KieuDon != 3).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.SoLuongN)),
                                                    KhacT = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 2 && p.KieuDon != 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 2 && p.KieuDon != 3).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.ThanhTienN)),
                                                    HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                                    HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX),
                                                }).ToList();

                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).Where(p => p.TongCongSL > 0).ToList();
                                        //var q0 = (from nhapd in data.NhapDs.Where(p=> p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.MaKP == _kho).Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 & p.KieuDon == 2))
                                        //          join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                        //          join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                        //          join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_pl)) on dv.IDNhom equals nhomdv.IDNhom
                                        //          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                        //          group new { dv, nhapd, nhapdct } by new { dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, nhomdv.TenNhom, tn.TenTN } into kq
                                        //          select new
                                        //          {
                                        //              MaDV = kq.Key.MaDV,
                                        //              TenDV = kq.Key.TenDV,
                                        //              DonVi = kq.Key.DonVi,
                                        //              DonGia = kq.Key.DonGia,
                                        //              TenNhomThuoc = kq.Key.TenNhom,
                                        //              Tentn = kq.Key.TenTN,
                                        //              NoiTruSL = (kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 1).Sum(p => p.nhapdct.SoLuongN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 1).Sum(p => p.nhapdct.SoLuongN)),
                                        //              NoiTruT = (kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 1).Sum(p => p.nhapdct.ThanhTienN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 1).Sum(p => p.nhapdct.ThanhTienN)),
                                        //              NgoaiTruSL = (kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 0).Sum(p => p.nhapdct.SoLuongN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 0).Sum(p => p.nhapdct.SoLuongN)),
                                        //              NgoaiTruT = (kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 0).Sum(p => p.nhapdct.ThanhTienN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon == 0).Sum(p => p.nhapdct.ThanhTienN)),
                                        //              KhacSL = (kq.Where(p => p.nhapd.PLoai == 2).Where(p => p.nhapd.KieuDon != 0 && p.nhapd.KieuDon != 1 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2).Where(p => p.nhapd.KieuDon != 0 && p.nhapd.KieuDon != 1 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.SoLuongX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon != 0 && p.nhapd.TraDuoc_KieuDon != 1 && p.nhapd.TraDuoc_KieuDon != 3).Sum(p => p.nhapdct.SoLuongN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon != 0 && p.nhapd.TraDuoc_KieuDon != 1 && p.nhapd.TraDuoc_KieuDon != 3).Sum(p => p.nhapdct.SoLuongN)),
                                        //              KhacT = (kq.Where(p => p.nhapd.PLoai == 2).Where(p => p.nhapd.KieuDon != 0 && p.nhapd.KieuDon != 1 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 2).Where(p => p.nhapd.KieuDon != 0 && p.nhapd.KieuDon != 1 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.ThanhTienX)) - (kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon != 0 && p.nhapd.TraDuoc_KieuDon != 1 && p.nhapd.TraDuoc_KieuDon != 3).Sum(p => p.nhapdct.ThanhTienN) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 1 && p.nhapd.TraDuoc_KieuDon != 0 && p.nhapd.TraDuoc_KieuDon != 1 && p.nhapd.TraDuoc_KieuDon != 3).Sum(p => p.nhapdct.ThanhTienN)),
                                        //              HuySL = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX),
                                        //              HuyT = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX),
                                        //          }).ToList();

                                        //var q = (from
                                        //         nd in q0
                                        //         select new
                                        //         {
                                        //             MaDV = nd.MaDV,
                                        //             TenDV = nd.TenDV,
                                        //             DonVi = nd.DonVi,
                                        //             DonGia = nd.DonGia,
                                        //             TenNhomThuoc = nd.TenNhomThuoc,
                                        //             Tentn = nd.Tentn,
                                        //             NoiTruSL = nd.NoiTruSL,
                                        //             NoiTruT = nd.NoiTruT,
                                        //             NgoaiTruSL = nd.NgoaiTruSL,
                                        //             NgoaiTruT = nd.NgoaiTruT,
                                        //             KhacSL = nd.KhacSL,
                                        //             KhacT = nd.KhacT,
                                        //             HuySL = nd.HuySL,
                                        //             HuyT = nd.HuyT,
                                        //             TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                        //             TongCongT = nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT
                                        //         }).ToList();


                                        if (q.Count() > 0)
                                        {
                                            rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                            MessageBox.Show("Không có dữ liệu");
                                    }
                                    #endregion
                                    #region Tủ trực
                                    else if (qkp.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                    {
                                        var qnd = (from nhapd in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                                   join nhapdct in data.DThuoccts.Where(p => p.MaKXuat == _kho) on nhapd.IDDon equals nhapdct.IDDon
                                                   select new { nhapd.MaBNhan, nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuong, nhapdct.ThanhTien }).ToList();
                                        var qbn = data.BenhNhans.ToList();
                                        var qkq1 = (from nd in qnd
                                                    join bn in qbn on nd.MaBNhan equals bn.MaBNhan into kq1
                                                    from k1 in kq1.DefaultIfEmpty()
                                                    group new { nd, k1 } by new { k1, nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        NoiTruSL = kq.Key.k1 != null && kq.Key.k1.NoiTru == 0 ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NoiTruT = kq.Key.k1 != null && kq.Key.k1.NoiTru == 0 ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        NgoaiTruSL = kq.Key.k1 != null && kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NgoaiTruT = kq.Key.k1 != null && kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        KhacSL = kq.Key.k1 == null ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        KhacT = kq.Key.k1 == null ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        HuySL = 0,
                                                        HuyT = 0,

                                                    }).ToList();


                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).Where(p => p.TongCongSL != 0).ToList();

                                        //var q = (from nhapd in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                        //          join nhapdct in data.DThuoccts.Where(p => p.MaKXuat == _kho) on nhapd.IDDon equals nhapdct.IDDon                                              
                                        //          join bn in data.BenhNhans on nhapd.MaBNhan equals bn.MaBNhan into kq1 from k1 in kq1.DefaultIfEmpty()
                                        //          join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                        //          join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_pl)) on dv.IDNhom equals nhomdv.IDNhom
                                        //         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                        //         group new { dv, k1, nhapdct } by new {k1, dv.MaDV, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia, nhomdv.TenNhom, tn.TenTN } into kq

                                        //          select new
                                        //          {
                                        //              MaDV = kq.Key.MaDV,                                                 
                                        //              DonVi = kq.Key.DonVi,
                                        //              DonGia = kq.Key.DonGia,
                                        //              TenDV = kq.Key.TenDV,                                                 
                                        //              TenNhomThuoc = kq.Key.TenNhom,
                                        //              Tentn = kq.Key.TenTN,
                                        //              NoiTruSL =  kq.Key.k1 != null &&  kq.Key.k1.NoiTru == 0 ? kq.Sum(p=>p.nhapdct.SoLuong) : 0,
                                        //              NoiTruT = kq.Key.k1 != null &&  kq.Key.k1.NoiTru == 0 ? kq.Sum(p => p.nhapdct.ThanhTien) : 0,
                                        //              NgoaiTruSL = kq.Key.k1 != null &&  kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nhapdct.SoLuong) : 0,
                                        //              NgoaiTruT =kq.Key.k1 != null &&  kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nhapdct.ThanhTien) : 0,
                                        //              KhacSL = kq.Key.k1 == null  ? kq.Sum(p => p.nhapdct.SoLuong) : 0,
                                        //              KhacT = kq.Key.k1 == null ? kq.Sum(p => p.nhapdct.ThanhTien) : 0,
                                        //              HuySL = 0,
                                        //              HuyT = 0,
                                        //              TongCongSL = kq.Sum(p => p.nhapdct.SoLuong),
                                        //              TongCongT = kq.Sum(p => p.nhapdct.ThanhTien)
                                        //          }).ToList();                                 

                                        if (q.Count() > 0)
                                        {
                                            rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                            MessageBox.Show("Không có dữ liệu");
                                    }
                                    #endregion
                                }
                            }
                            else
                                MessageBox.Show("Chưa chọn kho xuất");

                        }
                        #endregion
                        #region mẫu chung nhiều khoa phòng
                        else
                        {
                            if (lkp.Count > 0)
                            {
                                if (lkpAll.Count > 0)
                                {
                                    var qdvu = (from n in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT))
                                                join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                                                join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                                select new { dv.MaDV, dv.TenDV, n.TenNhom, n.TenNhomCT, tn.TenTN }).ToList();


                                    
                                    #region kho dược (ko phải tủ trực)
                                    if (lKhoDuoc.Count > 0)
                                    {

                                        var qnd = (from nhapd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => lKhoDuoc.Contains(p.MaKP ?? 0)).Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 & p.KieuDon == 2))
                                                   join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                                   select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon,
                                                                SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN :0,
                                                                SoLuongX = (nhapd.PLoai == 2 || nhapd .PLoai == 3) ? nhapdct.SoLuongX:0,
                                                                ThanhTienN =nhapd.PLoai == 1 ?  nhapdct.ThanhTienN : 0,
                                                                ThanhTienX =(nhapd.PLoai == 2 || nhapd .PLoai == 3) ?  nhapdct.ThanhTienX : 0,
                                                                nhapdct.SoLuongSD,
                                                                nhapdct.ThanhTienSD,
                                                                nhapd.TraDuoc_KieuDon
                                                   }).ToList();
                                        var qkq1 = (from nd in qnd
                                                    group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        NoiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN)),
                                                        NoiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN)),
                                                        NgoaiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.SoLuongN)),
                                                        NgoaiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 0).Sum(p => p.ThanhTienN)),
                                                        KhacSL = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && (DungChung.Bien.MaBV != "20001" ? ( p.KieuDon != 2 && p.KieuDon != 3) : p.KieuDon != 2)).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && (DungChung.Bien.MaBV != "20001" ? ( p.KieuDon != 2 && p.KieuDon != 3) : p.KieuDon != 2)).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.SoLuongN)),
                                                        KhacT = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && (DungChung.Bien.MaBV != "20001" ? ( p.KieuDon != 2 && p.KieuDon != 3) : p.KieuDon != 2)).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && (DungChung.Bien.MaBV != "20001" ? ( p.KieuDon != 2 && p.KieuDon != 3) : p.KieuDon != 2)).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.KieuDon != 2 && p.TraDuoc_KieuDon != 3).Sum(p => p.ThanhTienN)),
                                                        HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                                        HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX),
                                                    }).ToList();

                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new BC
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).Where(p => p.TongCongSL > 0).ToList();
                                        listBc.AddRange(q);

                                    }
                                    #endregion
                                    #region Tủ trực
                                    if (lTuTruc.Count > 0)
                                    {
                                        var qnd = (from nhapd in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                                   join nhapdct in data.DThuoccts.Where(p => lTuTruc.Contains(p.MaKXuat ?? 0)) on nhapd.IDDon equals nhapdct.IDDon
                                                   select new { nhapd.MaBNhan, nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuong, nhapdct.ThanhTien }).ToList();
                                        var qbn = data.BenhNhans.ToList();
                                        var qkq1 = (from nd in qnd
                                                    join bn in qbn on nd.MaBNhan equals bn.MaBNhan into kq1
                                                    from k1 in kq1.DefaultIfEmpty()
                                                    group new { nd, k1 } by new { k1, nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        NoiTruSL = kq.Key.k1 != null && kq.Key.k1.NoiTru == 0 ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NoiTruT = kq.Key.k1 != null && kq.Key.k1.NoiTru == 0 ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        NgoaiTruSL = kq.Key.k1 != null && kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NgoaiTruT = kq.Key.k1 != null && kq.Key.k1.NoiTru == 1 ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        KhacSL = kq.Key.k1 == null ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        KhacT = kq.Key.k1 == null ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        HuySL = 0,
                                                        HuyT = 0,

                                                    }).ToList();


                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new BC
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).Where(p => p.TongCongSL != 0).ToList();

                                        listBc.AddRange(q);

                                    }
                                    #endregion

                                    if (listBc.Count > 0)
                                    {
                                        rep.DataSource = listBc.OrderBy(p => p.TenDV).ToList();
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                    }
                                    else
                                        MessageBox.Show("Không có dữ liệu");

                                }

                                else
                                    MessageBox.Show("Chưa chọn kho xuất");
                            }

                        }
                        #endregion
                    }
                    #endregion
                    #region nhóm hóa chất
                    else
                    {
                        _pl = "Nhóm hóa chất";
                        rep.TenBC.Value = "BÁO CÁO SỬ DỤNG HOÁ CHẤT";
                        rep.MS.Value = "08D/BV-01";
                        rep.TenDuoc.Value = "Tên hoá chất, nước sản xuất";
                        rep.TenCot1.Value = "Lâm sàng";
                        rep.TenCot2.Value = "Cận lâm sàng";


                        int _dvbc = 0;
                        if (lupDVBC.EditValue != null)
                            _dvbc = Convert.ToInt32(lupDVBC.EditValue.ToString());
                        #region mẫu chung
                        //chỉ lấy sử dụng dược của kho xã
                        if (radMau.SelectedIndex == 0)
                        {
                            var q = (from nhapd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3).Where(p => p.MaKP == _kho && (_dvbc == 0 ? true : p.MaKPnx == _dvbc) && p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                     join nhomdv in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT)) on dv.IDNhom equals nhomdv.IDNhom
                                     join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                     join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                                     group new { dv, nhapd, nhapdct, nhomdv } by new { dv.MaDV, dv.TenDV, dv.DonVi, dv.DonGia, nhomdv.TenNhom, tn.TenTN } into kq
                                     select new
                                     {
                                         MaDV = kq.Key.MaDV,
                                         TenDV = kq.Key.TenDV,
                                         DonVi = kq.Key.DonVi,
                                         DonGia = kq.Key.DonGia,
                                         TenNhomThuoc = kq.Key.TenNhom,
                                         Tentn = kq.Key.TenTN,
                                         NoiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongSD),
                                         NoiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienSD),

                                         NgoaiTruSL = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.SoLuongSD),
                                         NgoaiTruT = kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5 && p.nhapd.KieuDon == 0).Sum(p => p.nhapdct.ThanhTienSD),

                                         KhacSL = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.SoLuongSD),
                                         KhacT = kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 5).Where(p => p.nhapd.KieuDon == 3 || p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == null).Sum(p => p.nhapdct.ThanhTienSD),

                                         HuySL = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX),
                                         HuyT = kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX),

                                         TongCongSL = (kq.Sum(p => p.nhapdct.SoLuongSD) == null ? 0 : kq.Sum(p => p.nhapdct.SoLuongSD)) + (kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.SoLuongX)),
                                         TongCongT = (kq.Sum(p => p.nhapdct.ThanhTienSD) == null ? 0 : kq.Sum(p => p.nhapdct.ThanhTienSD)) + (kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX) == null ? 0 : kq.Where(p => p.nhapd.PLoai == 3).Sum(p => p.nhapdct.ThanhTienX)),

                                     }).ToList();

                            if (q.Count() > 0)
                            {
                                rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                                MessageBox.Show("Không có dữ liệu");
                        }
                        #endregion
                        #region mẫu bv Tam đường: dùng cho các kho xuất trực tiếp cho bệnh nhân (cả kho dược trong bv và kho xã phường)
                        else if (radMau.SelectedIndex == 1)
                        {
                            if (_kho > 0)
                            {

                                var qkp = data.KPhongs.Where(p => p.MaKP == _kho).FirstOrDefault();
                                if (qkp != null)
                                {
                                    var qdvu = (from n in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT))
                                                join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                                                join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                                select new { dv.MaDV, dv.TenDV, n.TenNhom, n.TenNhomCT, tn.TenTN }).ToList();
                                    #region xã phường
                                    if (qkp.MaBVsd != "12001")
                                    {
                                        var qnd = (from nhapd in data.NhapDs.Where(p => p.PLoai == 5).Where(p => p.MaKP == _kho && p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                                   join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                                   select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon, nhapdct.SoLuongX, nhapdct.SoLuongSD, nhapdct.ThanhTienX, nhapdct.ThanhTienSD }).ToList();
                                        var qkq1 = (from nd in qnd
                                                    group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        //lâm sàng
                                                        NoiTruSL = kq.Where(p => p.PLoai == 5 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongSD),
                                                        NoiTruT = kq.Where(p => p.PLoai == 5 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienSD),
                                                        //CLS
                                                        NgoaiTruSL = kq.Where(p => p.PLoai == 5 && p.KieuDon == 5).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 5).Sum(p => p.SoLuongSD),
                                                        NgoaiTruT = kq.Where(p => p.PLoai == 5 && p.KieuDon == 5).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5 && p.KieuDon == 5).Sum(p => p.ThanhTienSD),

                                                        KhacSL = kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 6 || p.KieuDon == 2 || p.KieuDon == null).Sum(p => p.SoLuongSD) == null ? 0 : kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 6 || p.KieuDon == 2 || p.KieuDon == null).Sum(p => p.SoLuongSD),
                                                        KhacT = kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 6 || p.KieuDon == 2 || p.KieuDon == null).Sum(p => p.ThanhTienSD) == null ? 0 : kq.Where(p => p.PLoai == 5).Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 6 || p.KieuDon == 2 || p.KieuDon == null).Sum(p => p.ThanhTienSD),

                                                        HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                                        HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX)
                                                    }).ToList();


                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000
                                                 }).ToList();



                                        if (q.Count() > 0)
                                        {
                                            rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                            MessageBox.Show("Không có dữ liệu");
                                    }
                                    #endregion
                                    #region kho dược (ko phải tủ trực)
                                    else if (qkp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                                    {

                                        var qnd = (from nhapd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaKP == _kho).Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 & p.KieuDon == 2))
                                                   join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                                   select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon,
                                                                SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN :0,
                                                                SoLuongX = (nhapd.PLoai == 2 || nhapd .PLoai == 3) ? nhapdct.SoLuongX:0,
                                                                ThanhTienN =nhapd.PLoai == 1 ?  nhapdct.ThanhTienN : 0,
                                                                ThanhTienX =(nhapd.PLoai == 2 || nhapd .PLoai == 3) ?  nhapdct.ThanhTienX : 0,
                                                                nhapdct.SoLuongSD,
                                                                nhapdct.ThanhTienSD,
                                                                nhapd.TraDuoc_KieuDon
                                                   }).ToList();
                                        var qkq1 = (from nd in qnd
                                                    group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        //lâm sàng
                                                        NoiTruSL = (kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.SoLuongN)),
                                                        NoiTruT = (kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.ThanhTienN)),
                                                        //CLS
                                                        NgoaiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.SoLuongN)),
                                                        NgoaiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.ThanhTienN)),
                                                        KhacSL = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.SoLuongN)),
                                                        KhacT = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.ThanhTienN)),
                                                        HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                                        HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX),
                                                    }).ToList();

                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).ToList();

                                        if (q.Count() > 0)
                                        {
                                            rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                            MessageBox.Show("Không có dữ liệu");
                                    }
                                    #endregion
                                    #region Tủ trực
                                    else if (qkp.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                    {
                                        var qnd = (from nhapd in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                                   join nhapdct in data.DThuoccts.Where(p => p.MaKXuat == _kho) on nhapd.IDDon equals nhapdct.IDDon
                                                   select new { nhapd.MaBNhan, nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuong, nhapdct.ThanhTien }).ToList();
                                        var qbn = data.BenhNhans.ToList();
                                        var qkq1 = (from nd in qnd
                                                    join bn in qbn on nd.MaBNhan equals bn.MaBNhan into kq1
                                                    from k1 in kq1.DefaultIfEmpty()
                                                    group new { nd, k1 } by new { k1, nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        //lâm sàng
                                                        NoiTruSL = kq.Key.k1 != null && (kq.Key.k1.NoiTru == 0 || kq.Key.k1.NoiTru == 1) ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NoiTruT = kq.Key.k1 != null && (kq.Key.k1.NoiTru == 0 || kq.Key.k1.NoiTru == 1) ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        //CLS
                                                        NgoaiTruSL = 0,
                                                        NgoaiTruT = 0,
                                                        KhacSL = kq.Key.k1 == null ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        KhacT = kq.Key.k1 == null ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        HuySL = 0,
                                                        HuyT = 0,

                                                    }).ToList();


                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).ToList();


                                        if (q.Count() > 0)
                                        {
                                            rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        else
                                            MessageBox.Show("Không có dữ liệu");
                                    }
                                    #endregion
                                }
                            }
                            else
                                MessageBox.Show("Chưa chọn kho xuất");

                        }
                        #endregion
                        #region chung nhiều kho
                        else
                        {
                            if (lkp.Count > 0)
                            {
                                if (lkpAll.Count > 0)
                                {
                                    var qdvu = (from n in data.NhomDVs.Where(p => DungChung.Bien.MaBV != "01071" ? p.TenNhomCT.Contains(_pl) : tenDV.Contains(p.TenNhomCT))
                                                join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                                                join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                                select new { dv.MaDV, dv.TenDV, n.TenNhom, n.TenNhomCT, tn.TenTN }).ToList();

                                    #region kho dược (ko phải tủ trực)
                                    if (lKhoDuoc.Count > 0)
                                    {

                                        var qnd = (from nhapd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => lKhoDuoc.Contains(p.MaKP ?? 0)).Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 & p.KieuDon == 2))
                                                   join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                                   select new { nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon,
                                                                SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN :0,
                                                                SoLuongX = (nhapd.PLoai == 2 || nhapd .PLoai == 3) ? nhapdct.SoLuongX:0,
                                                                ThanhTienN =nhapd.PLoai == 1 ?  nhapdct.ThanhTienN : 0,
                                                                ThanhTienX =(nhapd.PLoai == 2 || nhapd .PLoai == 3) ?  nhapdct.ThanhTienX : 0,
                                                                nhapdct.SoLuongSD,
                                                                nhapdct.ThanhTienSD,
                                                                nhapd.TraDuoc_KieuDon
                                                   }).ToList();
                                        var qkq1 = (from nd in qnd
                                                    group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        //lâm sàng
                                                        NoiTruSL = (kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.SoLuongN)),
                                                        NoiTruT = (kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 0)).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 0)).Sum(p => p.ThanhTienN)),
                                                        //CLS
                                                        NgoaiTruSL = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.SoLuongN)),
                                                        NgoaiTruT = (kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2 && p.KieuDon == 5).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon == 5).Sum(p => p.ThanhTienN)),
                                                        KhacSL = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.SoLuongX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.SoLuongN)),
                                                        KhacT = (kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1 && p.KieuDon != 3 && p.KieuDon != 5).Sum(p => p.ThanhTienX)) - (kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1 && p.TraDuoc_KieuDon != 0 && p.TraDuoc_KieuDon != 1 && p.TraDuoc_KieuDon != 3 && p.TraDuoc_KieuDon != 5).Sum(p => p.ThanhTienN)),
                                                        HuySL = kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.SoLuongX),
                                                        HuyT = kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 3).Sum(p => p.ThanhTienX),
                                                    }).ToList();

                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new BC
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).ToList();
                                        listBc.AddRange(q);

                                    }
                                    #endregion
                                    #region Tủ trực
                                    if (lTuTruc.Count > 0)
                                    {
                                        var qnd = (from nhapd in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                                   join nhapdct in data.DThuoccts.Where(p => lTuTruc.Contains(p.MaKXuat ?? 0)) on nhapd.IDDon equals nhapdct.IDDon
                                                   select new { nhapd.MaBNhan, nhapdct.MaDV, nhapdct.DonVi, nhapdct.DonGia, nhapdct.SoLuong, nhapdct.ThanhTien }).ToList();
                                        var qbn = data.BenhNhans.ToList();
                                        var qkq1 = (from nd in qnd
                                                    join bn in qbn on nd.MaBNhan equals bn.MaBNhan into kq1
                                                    from k1 in kq1.DefaultIfEmpty()
                                                    group new { nd, k1 } by new { k1, nd.MaDV, nd.DonVi, nd.DonGia } into kq
                                                    select new
                                                    {
                                                        MaDV = kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        DonGia = kq.Key.DonGia,
                                                        //lâm sàng
                                                        NoiTruSL = kq.Key.k1 != null && (kq.Key.k1.NoiTru == 0 || kq.Key.k1.NoiTru == 1) ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        NoiTruT = kq.Key.k1 != null && (kq.Key.k1.NoiTru == 0 || kq.Key.k1.NoiTru == 1) ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        //CLS
                                                        NgoaiTruSL = 0,
                                                        NgoaiTruT = 0,
                                                        KhacSL = kq.Key.k1 == null ? kq.Sum(p => p.nd.SoLuong) : 0,
                                                        KhacT = kq.Key.k1 == null ? kq.Sum(p => p.nd.ThanhTien) : 0,
                                                        HuySL = 0,
                                                        HuyT = 0,

                                                    }).ToList();


                                        var q = (from nd in qkq1
                                                 join dv in qdvu on nd.MaDV equals dv.MaDV
                                                 select new BC
                                                 {
                                                     MaDV = dv.MaDV,
                                                     TenDV = dv.TenDV,
                                                     DonVi = nd.DonVi,
                                                     DonGia = nd.DonGia / 1000,
                                                     TenNhomThuoc = dv.TenNhom,
                                                     Tentn = dv.TenTN,
                                                     NoiTruSL = nd.NoiTruSL,
                                                     NoiTruT = nd.NoiTruT / 1000,
                                                     NgoaiTruSL = nd.NgoaiTruSL,
                                                     NgoaiTruT = nd.NgoaiTruT / 1000,
                                                     KhacSL = nd.KhacSL,
                                                     KhacT = nd.KhacT / 1000,
                                                     HuySL = nd.HuySL,
                                                     HuyT = nd.HuyT / 1000,
                                                     TongCongSL = nd.NoiTruSL + nd.NgoaiTruSL + nd.KhacSL + nd.HuySL,
                                                     TongCongT = (nd.NoiTruT + nd.NgoaiTruT + nd.KhacT + nd.HuyT) / 1000

                                                 }).ToList();

                                        listBc.AddRange(q);
                                    }
                                    #endregion
                                }
                                if (listBc.Count > 0)
                                {
                                    rep.DataSource = listBc.OrderBy(p => p.TenDV).ToList();
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                    MessageBox.Show("Không có dữ liệu");

                            }
                            else
                                MessageBox.Show("Chưa chọn kho xuất");

                        }
                        #endregion

                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn nhóm dược");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsBcSuDungThuoc_Load(object sender, EventArgs e)
        {

            var q = (from sdd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3)
                     join kp in data.KPhongs on sdd.MaKP equals kp.MaKP
                     select new { TenKP = kp.TenKP, MaKP = kp.MaKP }).Distinct().ToList();
            lupKho.Properties.DataSource = q.ToList();

            List<KPhong> lkp = (from kp in q select new KPhong { MaKP = kp.MaKP, TenKP = kp.TenKP }).ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            cklKhoaPhong.DataSource = lkp;
            cklKhoaPhong.DisplayMember = "TenKP";
            cklKhoaPhong.ValueMember = "MaKP";
            cklKhoaPhong.CheckAll();
            var qdv = from dvbc in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3)
                      join kp in data.KPhongs on dvbc.MaKPnx equals kp.MaKP
                      select new { kp.TenKP, kp.MaKP };
            lupDVBC.Properties.DataSource = qdv.Distinct().ToList();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTenPLoai.Properties.DataSource = data.NhomDVs.Where(p => p.Status == 1).ToList();
            if (DungChung.Bien.MaBV == "12001")
                radMau.SelectedIndex = 1;

        }

        private void ckQuy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radMau.SelectedIndex == 0)
            {
                if (DungChung.Bien.MaBV == "01071")
                {
                    lupTenPLoai1.Visible = false;
                    lupTenPLoai.Visible = true;
                }
                var q = from sdd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3)
                        join kp in data.KPhongs on sdd.MaKP equals kp.MaKP
                        select new { kp.TenKP, kp.MaKP };
                lupKho.Properties.DataSource = q.Distinct().ToList();
                var qdv = from dvbc in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 3)
                          join kp in data.KPhongs on dvbc.MaKPnx equals kp.MaKP
                          select new { kp.TenKP, kp.MaKP };
                lupDVBC.Properties.DataSource = qdv.Distinct().ToList();
                lupKho.Visible = true;
                cklKhoaPhong.Visible = false;
            }
            else if (radMau.SelectedIndex == 1)
            {
                if (DungChung.Bien.MaBV == "01071")
                {
                    lupTenPLoai1.Visible = false;
                    lupTenPLoai.Visible = true;
                }
                var qkp = data.KPhongs.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) && p.Status == 1 && p.TrongBV == 1).ToList();
                lupKho.Properties.DataSource = qkp;
                lupDVBC.Properties.DataSource = qkp;
               
                lupKho.Visible = true;
                cklKhoaPhong.Visible = false;
            }
            else
            {   
                if(DungChung.Bien.MaBV == "01071")
                {
                    lupTenPLoai1.Visible = true;
                    lupTenPLoai.Visible = false;
                    List<NhomDV> tenDV = data.NhomDVs.Where(p => p.Status == 1).ToList();
                    tenDV.Insert(0, new NhomDV { IDNhom = 0, TenNhom = "Tất cả" });
                    lupTenPLoai1.DataSource = tenDV;
                    lupTenPLoai1.DisplayMember = "TenNhom";
                    lupTenPLoai1.ValueMember = "IDNhom";
                    lupTenPLoai1.CheckAll();
                }

                lupKho.Visible = false;
                cklKhoaPhong.Visible = true;
                List<KPhong> lkp = data.KPhongs.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) && p.Status == 1 && p.TrongBV == 1).ToList();
                lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
                cklKhoaPhong.DataSource = lkp;
                cklKhoaPhong.DisplayMember = "TenKP";
                cklKhoaPhong.ValueMember = "MaKP";
                cklKhoaPhong.CheckAll();

            }
        }

        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }
        private void lupTenPLoai1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (lupTenPLoai1.Visible == true && e.Index == 0)
            {
                if (lupTenPLoai1.GetItemChecked(0) == true)
                    lupTenPLoai1.CheckAll();
                else
                    lupTenPLoai1.UnCheckAll();
            }
        }

        private class BC
        {

            public int MaDV { get; set; }

            public string TenDV { get; set; }

            public string DonVi { get; set; }

            public double DonGia { get; set; }

            public string TenNhomThuoc { get; set; }

            public string Tentn { get; set; }

            public double NoiTruSL { get; set; }

            public double NoiTruT { get; set; }

            public double NgoaiTruSL { get; set; }

            public double NgoaiTruT { get; set; }

            public double KhacSL { get; set; }

            public double KhacT { get; set; }

            public double HuySL { get; set; }

            public double HuyT { get; set; }

            public double TongCongSL { get; set; }

            public double TongCongT { get; set; }
        }


    }
}