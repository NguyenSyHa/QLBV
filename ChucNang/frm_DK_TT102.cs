using DevExpress.XtraEditors;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_DK_TT102 : DevExpress.XtraEditors.XtraForm
    {
        public frm_DK_TT102()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DK_TT102_Load(object sender, EventArgs e)
        {
            cboSapXep.SelectedIndex = 0;
            rgNoiKCB.SelectedIndex = 3;
            rgDoiTuong.SelectedIndex = 3;
            rgGuiBHXH.SelectedIndex = 2;
            rgTimKiem.SelectedIndex = 1;
            cboDuyetTT.SelectedIndex = 2;
            rgThuchi.SelectedIndex = 2;
            rgNoiKCB_SelectedIndexChanged(sender, e);
            dtpTuNgay.DateTime = DungChung.Ham.NgayTu(DateTime.Now);
            dtpDenNgay.DateTime = DungChung.Ham.NgayDen(DateTime.Now);
            lupCanBo.Properties.DataSource = CanBoTT().ToList();
            lupCanBo.EditValue = lupCanBo.Properties.GetDisplayTextByKeyValue("Tất cả");
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            cboDoiTuong.Properties.DataSource = _lDTBN.ToList();
            lchkDauThe.DataSource = ListDT().ToList();
            lchkDauThe.CheckAll();
            Khoaphong();
            _ldv = data.DichVus.ToList();

            if (DungChung.Bien.MaBV == "24012")
            {
                rgChonBieu.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tổng hợp"));
            }
        }
        #region Mã đối tượng thẻ bh
        class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }
        private List<MyObject> ListDT()
        {
            List<MyObject> lMaDtuong = new List<MyObject>();
            lMaDtuong = data.DTuongs.Where(p => p.Status == 1).Select(p => new MyObject { value = p.MaDTuong, Text = p.MaDTuong }).OrderBy(p => p.Text).ToList();
            lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            return lMaDtuong;
        }
        #endregion
        #region Danh mục Xã phường _ Địa điểm- Khoa Phòng
        public class KhoaPhong
        {
            public bool _check;
            public string _maKP;
            public string _kp;

            public string MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        List<KhoaPhong> _lCSKCB = new List<KhoaPhong>();
        private void rgNoiKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            var kphong = data.KPhongs.ToList();

            if (rgNoiKCB.SelectedIndex == 0)
            {
                _lCSKCB.Add(new KhoaPhong { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });

            }
            if (rgNoiKCB.SelectedIndex == 1)
            {

                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong || (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });

            }
            if (rgNoiKCB.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();


            }
            if (rgNoiKCB.SelectedIndex == 3)
            {

                _lCSKCB = (from kp in data.BenhViens.Where(p => p.Connect)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });

            }
            LBenhVien.DataSource = null;
            LBenhVien.DataSource = _lCSKCB;
            LBenhVien.CheckAll();
        }
        class KPhong2 { public int MaKP { get; set; } public string TenKP { get; set; } }
        private void Khoaphong()
        {
            List<KPhong2> _lKphong = new List<KPhong2>();
            KPhong2 k = new KPhong2();
            k.MaKP = 0;
            k.TenKP = "Tất cả";
            _lKphong.Add(k);

            var kp = data.KPhongs
            .Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
            .ToList();
            foreach (var item in kp)
            {
                KPhong2 ka = new KPhong2();
                ka.MaKP = item.MaKP;
                ka.TenKP = item.TenKP;
                _lKphong.Add(ka);
            }
            cboKhoaPhong.Properties.DataSource = _lKphong.ToList();
            cboKhoaPhong.EditValue = 0;
        }
        #endregion
        #region Cán bộ TT
        List<CanBo> CanBoTT()
        {
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in data.CanBoes
                       join kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan) on cb.MaKP equals kp.MaKP
                       select cb).ToList();
            _lcanbo.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            return _lcanbo;
        }

        #endregion
        class CLS79_ChiPhi
        {
            public string TenBenhNhan { get; set; }
            public string NamSinh { get; set; }
            public string GioiTinh { get; set; }
            public string SoThe { get; set; }
            public string MaBenh { get; set; }
            public string NgayRa { get; set; }
            public string NgayVao { get; set; }

            // Chi phí 

            public double TongCong { get; set; }
            public double KhamBenh { get; set; }
            public double NgayGiuong { get; set; }
            public double XetNghiem { get; set; }
            public double CDHA { get; set; }
            public double ThuThuat { get; set; }
            public double mau { get; set; }
            public double Thuoc { get; set; }
            public double VTYT { get; set; }
            public double VanChuyenNguoiBenh { get; set; }

            //Quỹ BH trả

            public double TinhThanhPho { get; set; }
            public double TaiTrungUong { get; set; }
            public double ND70 { get; set; }

            // Người bệnh
            public double CungChiTra { get; set; }
            public double TuTra { get; set; }

            //Nguồn khác
            public double NSDiaPhuong { get; set; }
            public double HoTro { get; set; }

            //Chi phí ngoài phạm vi BHYT
            public double CPNgoaiBHYT { get; set; }

        }
        List<DichVu> _ldv = new List<DichVu>();
        List<int> _lstIdCDHA;
        private void setIDNhom()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.ToList();
            _lstIdCDHA = new List<int>();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case "Xét nghiệm":
                        if (tenNhom.Where(a => a.TenNhomCT == "Xét nghiệm").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Xét nghiệm");
                        idXN = item.IDNhom;
                        if (idXN != 1)
                            MessageBox.Show("Nhóm xét nghiệm sai mã nhóm theo CV 9324");
                        break;
                    case "Chẩn đoán hình ảnh":
                        _lstIdCDHA.Add(item.IDNhom);

                        if (item.IDNhom != 2)
                            MessageBox.Show("Nhóm chẩn đoán hình ảnh sai mã nhóm theo CV 9324");
                        continue;
                    case "Thăm dò chức năng":
                        _lstIdCDHA.Add(item.IDNhom);
                        if (item.IDNhom != 3)
                            MessageBox.Show("Nhóm thăm dò chức năng sai mã nhóm theo CV 9324");
                        continue;
                    case "Thuốc trong danh mục BHYT":
                        idThuoc = item.IDNhom;
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thuốc trong danh mục BHYT");
                        if (item.IDNhom != 4)
                            MessageBox.Show("Nhóm Thuốc trong danh mục BHYT sai mã nhóm theo CV 9324");
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều Thuốc điều trị ung thư, chống thải ghép ngoài danh mục");
                        if (item.IDNhom != 5)
                            MessageBox.Show("Nhóm Thuốc điều trị ung thư, chống thải ghép ngoài danh mục sai mã nhóm theo CV 9324");
                        idThuocUTCTG = item.IDNhom;
                        break;
                    case "Thuốc thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thuốc thanh toán theo tỷ lệ");
                        if (item.IDNhom != 6)
                            MessageBox.Show("Nhóm Thuốc thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idThuocTyLe = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        if (tenNhom.Where(a => a.TenNhomCT == "Máu và chế phẩm của máu").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm máu và chế phẩm của máu");
                        if (item.IDNhom != 7)
                            MessageBox.Show("Nhóm Máu và chế phẩm của máu sai mã nhóm theo CV 9324");
                        idMau = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thủ thuật, phẫu thuật").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thủ thuật phẫu thuật");
                        if (item.IDNhom != 8)
                            MessageBox.Show("Nhóm Thủ thuật, phẫu thuật sai mã nhóm theo CV 9324");
                        idTTPT = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm DVKT thanh toán theo tỷ lệ");
                        if (item.IDNhom != 9)
                            MessageBox.Show("Nhóm DVKT thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idDVKTC = item.IDNhom;
                        break;
                    case "Vật tư y tế trong danh mục BHYT":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Vật tư y tế trong danh mục BHYT");
                        if (item.IDNhom != 10)
                            MessageBox.Show("Nhóm Vật tư y tế trong danh mục BHYT sai mã nhóm theo CV 9324");
                        idVTYT = item.IDNhom;
                        break;
                    case "VTYT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm VTYT thanh toán theo tỷ lệ");
                        if (item.IDNhom != 11)
                            MessageBox.Show("Nhóm VTYT thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idVTTT = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vận chuyển").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Vận chuyển");
                        if (item.IDNhom != 12)
                            MessageBox.Show("Nhóm Vận chuyển sai mã nhóm theo CV 9324");
                        idChiPhiVC = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Khám bệnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Khám bệnh");
                        if (item.IDNhom != 13)
                            MessageBox.Show("Nhóm Khám bệnh sai mã nhóm theo CV 9324");
                        idCongKham = item.IDNhom;
                        break;

                    case "Giường điều trị ngoại trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị ngoại trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị ngoại trú");
                        if (item.IDNhom != 14)
                            MessageBox.Show("Nhóm Giường điều trị ngoại trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNgoaiTru = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị nội trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị nội trú");
                        if (item.IDNhom != 15)
                            MessageBox.Show("Nhóm Giường điều trị nội trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNoiTru = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        if (tenNhom.Where(a => a.TenNhomCT == "Nhóm hóa chất").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm hóa chất");
                        idHoaChat = item.IDNhom;
                        break;
                }
            }
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1, idThuocUTCTG = -1, idVTYT = -1, idNgayGiuongNoiTru = -1, idNgayGiuongNgoaiTru = -1, idChiPhiVC = -1, idVTTT = -1, idThuocTyLe = -1, idHoaChat = -1;

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo báo cáo.", "Dữ liệu báo cáo lớn, xin chờ.");
        }

        void TaoBaoCao()
        {
            string LamTron = DungChung.Bien.FormatString[1];
            setIDNhom();
            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < LBenhVien.ItemCount; i++)
            {
                if (LBenhVien.GetItemChecked(i))
                {
                    _dsCSKCB.Add(LBenhVien.GetItemValue(i).ToString());
                }
            }
            _dsCSKCB = _dsCSKCB.Distinct().ToList();
            int KieuTimKiem = rgTimKiem.SelectedIndex;
            int _GuiBHXH = rgGuiBHXH.SelectedIndex;
            int _intduyet = 2;
            _intduyet = cboDuyetTT.SelectedIndex;
            int id_DoiTuongKham = 1;
            if (cboDoiTuong.EditValue != null)
                id_DoiTuongKham = Convert.ToInt32(cboDoiTuong.EditValue);
            List<string> lmaDtuong = new List<string>();
            string macb = "";
            int Doituong = rgDoiTuong.SelectedIndex;
            if (lupCanBo.EditValue != null)
            {
                macb = lupCanBo.EditValue.ToString();
            }

            for (int i = 0; i < lchkDauThe.ItemCount; i++)
            {
                if (lchkDauThe.GetItemChecked(i) == true)
                    lmaDtuong.Add(lchkDauThe.GetItemValue(i).ToString());
            }

            int trongBH = 0;
            trongBH = cboChiPhi.SelectedIndex;
            DateTime NgayTu = dtpTuNgay.DateTime;
            DateTime NgayDen = dtpDenNgay.DateTime;
            DateTime ngaytunew = NgayTu.AddMonths(-6);
            DateTime ngaydennew = NgayDen.AddMonths(6);


            int _MaKPc = 0;
            if (!string.IsNullOrEmpty(cboKhoaPhong.Text))
            {
                _MaKPc = Convert.ToInt32(cboKhoaPhong.EditValue);
            }

            var q22 = (from vp in data.VienPhis
                           // where vp.MaBNhan==113423
                       join vpct in data.VienPhicts.Where(o => (cboChiPhi.SelectedIndex == 2 ? true : (o.TrongBH == cboChiPhi.SelectedIndex))) on vp.idVPhi equals vpct.idVPhi
                       where (macb == "" || (RGKieuCanBo.SelectedIndex == 0 && vp.MaCB == macb) || (RGKieuCanBo.SelectedIndex == 1))
                       where ((KieuTimKiem == 2) ? (vp.NgayDuyet >= NgayTu && vp.NgayDuyet <= NgayDen) :
                              (KieuTimKiem == 1 ? (vp.NgayTT >= NgayTu && vp.NgayTT <= NgayDen) :
                              (KieuTimKiem == 3 ? (vp.NgayDuyetCP >= NgayTu && vp.NgayDuyetCP <= NgayDen) : (vp.NgayRa >= NgayTu && vp.NgayRa <= NgayDen))))
                       //where (_GuiBHXH == 2 ? true : (_GuiBHXH == 0 ? vp.ExportBHXH == false : vp.ExportBHXH == true))
                       select new
                       {
                           vpct.SoLuong,
                           vpct.DonGia,
                           vpct.TyLeTT,
                           vpct.XHH,
                           vpct.IDTamUng,
                           vpct.MaDV,
                           vp.MaBNhan,
                           vp.NgayDuyet,
                           vpct.TrongBH,
                           vpct.MaKP,
                           vpct.LoaiDV,
                           vpct.TyLeBHTT,
                           vpct.ThanhToan,
                           ThanhTien = vpct.ThanhTien,
                           TienBN = vpct.TBNCTT + vpct.TBNTT,
                           TienBH = vpct.TienBH,
                           vpct.TBNCTT,
                           vpct.TBNTT,
                           vpct.TienNguonKhac,
                           vp.NgayTT,
                           vp.ExportBHXH
                       }).ToList().Select(x => new
                       {
                           x.XHH,
                           x.IDTamUng,
                           x.MaDV,
                           x.MaBNhan,
                           x.NgayDuyet,
                           x.TrongBH,
                           x.MaKP,
                           x.TyLeBHTT,
                           x.ThanhToan,
                           x.LoaiDV,
                           ThanhTien = x.ThanhTien,
                           TienBN = x.TienBN,
                           TienBH = x.TienBH,
                           TongCong = x.TienBN + x.TienBH,
                           //ThanhTienVTYT = (Math.Round(x.SoLuong, 2) * Math.Round(x.DonGia, 2) * (x.TyLeTT / 100)),
                           x.NgayTT,
                           x.ExportBHXH,
                           x.TBNCTT,
                           x.TBNTT,
                           x.TienNguonKhac,
                       }).ToList();
            int lamtron = 4;
            var q6 = (from a in q22
                      where (_intduyet == 2 ? true : (_intduyet == 1 ? a.NgayDuyet != null : a.NgayDuyet == null))
                      select a.MaBNhan).Distinct().ToList();
            var q2 = (from a in q22.Where(p => _GuiBHXH == 2 ? true : (_GuiBHXH == 0 ? p.ExportBHXH == false : p.ExportBHXH == true))
                      select new
                      {
                          a.XHH,
                          // IDTamUng = _lIDtamung.Where(p => p.IDTamUng == a.IDTamUng).ToList().Count > 0 ? _lIDtamung.Where(p => p.IDTamUng == a.IDTamUng).First().IDTamUng : 0,
                          a.MaDV,
                          a.MaBNhan,
                          a.NgayDuyet,
                          a.TrongBH,
                          a.MaKP,
                          a.TyLeBHTT,
                          a.ThanhToan,
                          a.LoaiDV,
                          ThanhTien = a.ThanhTien, //chkLamTron.Checked ? a.ThanhTien : (_CP_BH == 0 ? a.ThanhTien : (_CP_BH == 1 ? (a.TienBH) : a.TienBN)),
                          TienBN = a.TienBN,
                          TienBH = a.TienBH,
                          //ThanhTienVTYT = a.ThanhTienVTYT,
                          a.NgayTT,
                          a.TBNTT,
                          a.TienNguonKhac,
                          a.TBNCTT,
                          a.TongCong
                      }).ToList();
            var q71 = (from bn in data.BenhNhans
                       join rv in data.RaViens.Where(p => p.NgayRa >= ngaytunew && p.NgayRa <= ngaydennew) on bn.MaBNhan equals rv.MaBNhan
                       join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                       where ((id_DoiTuongKham == 99 ? true : bn.IDDTBN == id_DoiTuongKham))
                       select new
                       {
                           MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper(),
                           bn.DChi,
                           bn.HanBHDen,
                           bn.HanBHTu,
                           bn.TuyenDuoi,
                           bn.DTNT,
                           bn.DTuong,
                           bn.NoiTru,
                           bn.NoiTinh,
                           bn.Tuyen,
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.NamSinh,
                           bn.NgaySinh,
                           bn.ThangSinh,
                           bn.SThe,
                           bn.GTinh,
                           bn.MaCS,
                           MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                           bn.CapCuu,
                           bn.Tuoi,
                           bn.KhuVuc,
                           bn.MaBV,
                           bn.SoDK,
                           bn.NNhap,
                           rv.MaICD,
                           rv.NgayVao,
                           rv.NgayRa,
                           rv.SoNgaydt,
                           rv.Status,
                           rv.KetQua,
                           rv.ChanDoan,
                           KhoaTongKet = rv.MaKP,
                           MaKPBn = bn.MaKP
                       }).Distinct().ToList();
            var q7 = (from a in q6
                      join bn in q71 on a equals bn.MaBNhan
                      //join bn in data.BenhNhans on a equals bn.MaBNhan
                      //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                      //join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                      //where ((id_DoiTuongKham == 99 ? true : bn.IDDTBN == id_DoiTuongKham) && bn.NoiTru == rdBieu.SelectedIndex)
                      select new
                      {
                          bn.MaKCB,
                          bn.DChi,
                          bn.HanBHDen,
                          bn.HanBHTu,
                          bn.TuyenDuoi,
                          bn.DTNT,
                          bn.DTuong,
                          bn.NoiTru,
                          bn.NoiTinh,
                          bn.Tuyen,
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.NamSinh,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.SThe,
                          bn.GTinh,
                          bn.MaCS,
                          bn.MaDTuong,
                          bn.CapCuu,
                          bn.Tuoi,
                          bn.KhuVuc,
                          bn.MaBV,
                          bn.SoDK,
                          bn.MaICD,
                          bn.NgayVao,
                          bn.NgayRa,
                          bn.SoNgaydt,
                          bn.Status,
                          bn.KetQua,
                          bn.ChanDoan,
                          bn.NNhap,
                          bn.MaKPBn,
                          KhoaTongKet = bn.KhoaTongKet
                      }).OrderBy(p => p.MaBNhan).ToList();

            var q4 = (from a in q2.Where(p => p.LoaiDV == 0)
                      join bn in q7 on a.MaBNhan equals bn.MaBNhan
                      join dt in lmaDtuong on bn.MaDTuong equals dt
                      join cskcb in _dsCSKCB on bn.MaKCB equals cskcb
                      where (rgThuchi.SelectedIndex == 2 ? true : (//a.IDTamUng > 0 && 
                      (rgThuchi.SelectedIndex == 0 ? a.ThanhToan == 1 : a.ThanhToan == 0)))
                      && (Doituong == 3 ? true : (Doituong == 1 ? (bn.NoiTru == 0 && bn.DTNT == true) : (Doituong == 2 ? bn.NoiTru == 1 : bn.NoiTru == 0)))
                      select new
                      {
                          a.XHH,
                          bn.MaKCB,
                          a.TrongBH,
                          a.MaKP,
                          bn.DChi,
                          bn.HanBHDen,
                          bn.HanBHTu,
                          bn.TuyenDuoi,
                          bn.DTNT,
                          bn.DTuong,
                          bn.NoiTru,
                          bn.SoDK,
                          bn.NoiTinh,
                          bn.Tuyen,
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.NamSinh,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.SThe,
                          bn.GTinh,
                          bn.MaCS,
                          bn.MaDTuong,
                          bn.CapCuu,
                          bn.MaICD,
                          ChanDoan = (bn.MaICD != null && bn.MaICD != "") ? bn.ChanDoan : "",
                          bn.NgayVao,
                          bn.NgayRa,
                          bn.SoNgaydt,
                          bn.Status,
                          bn.KetQua,
                          a.MaDV,
                          a.ThanhTien,
                          //a.ThanhTienVTYT,
                          a.TienBN,
                          a.TienBH,
                          a.NgayTT,
                          bn.Tuoi,
                          bn.KhuVuc,
                          bn.MaBV,
                          bn.NNhap,
                          TyLeBHTT = bn.DTuong == "BHYT" ? a.TyLeBHTT : 0,
                          KhoaTongKet = MaKPQD(bn.KhoaTongKet),
                          bn.MaKPBn,
                          a.TBNCTT,
                          a.TBNTT,
                          a.TienNguonKhac,
                          a.TongCong,
                      }).OrderBy(p => p.MaBNhan).ToList();



            var q33 = (from a in q4
                       join dv in _ldv on a.MaDV equals dv.MaDV
                       where (cboDoiTuong.Text == "BHYT" ? ((rgNoiKCB.SelectedIndex < 3 ? a.TuyenDuoi == rgNoiKCB.SelectedIndex : true) && (cboNoiNgoaiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiNgoaiTinh.SelectedIndex : true)) : true)
                       where trongBH > 2 ? true : (a.TrongBH == trongBH)
                       //where ckcDVTheoYC.Checked? 
                       where a.XHH == 0
                       where (_MaKPc == 0 ? true : (DungChung.Bien.MaBV == "26007" ? a.MaKPBn == _MaKPc : a.MaKP == _MaKPc))//26007 tìm kiếm theo MaKP trong bảng bn Liễu y/c 22-07
                       group new { a, dv } by new { a.NNhap, a.MaKCB, a.SoDK, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.MaBNhan, a.NoiTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV, a.KhoaTongKet } into kq
                       select
                       new
                       {
                           kq.Key.MaKCB,
                           MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                           kq.Key.SoNgaydt,
                           kq.Key.DTuong,
                           kq.Key.NoiTru,
                           TrongBH = cboChiPhi.SelectedIndex,// kq.Key.TrongBH,
                           kq.Key.TuyenDuoi,
                           kq.Key.DTNT,
                           kq.Key.NgayTT,
                           kq.Key.MaDTuong,
                           kq.Key.CapCuu,
                           kq.Key.NgaySinh,
                           kq.Key.ThangSinh,
                           kq.Key.KhuVuc,
                           kq.Key.MaBV,
                           kq.Key.KetQua,
                           kq.Key.Status,
                           kq.Key.KhoaTongKet,
                           kq.Key.SoDK,
                           kq.Key.NNhap,
                           NhomPL = NhomPLoai(STTA(kq.Key.NoiTru, kq.Key.MaDTuong)),
                           NoiTinh = kq.Key.NoiTinh,
                           Tuyen = kq.Key.Tuyen,
                           MaBNhan = kq.Key.MaBNhan,
                           TenBNhan = kq.Key.TenBNhan,
                           NSinh = kq.Key.NamSinh,
                           SThe = kq.Key.SThe,
                           Nam = kq.Key.GTinh,
                           GTinh = kq.Key.GTinh,
                           MaCS = kq.Key.MaCS,
                           MaICD = kq.Key.MaICD,
                           ChanDoan = kq.Key.ChanDoan,
                           Ngaykham = kq.Key.NgayVao,
                           Ngayra = kq.Key.NgayRa,
                           Tuoi = kq.Key.Tuoi,
                           BHtu = kq.Key.HanBHTu,
                           BHden = kq.Key.HanBHDen,
                           Diachi = kq.Key.DChi,
                           Mabn = kq.Key.MaBNhan,
                           TBNCTT = kq.Sum(p => p.a.TBNCTT),
                           TBNTT = kq.Sum(p => p.a.TBNTT),
                           TienNguonKhac = kq.Sum(p => p.a.TienNguonKhac),
                           Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.IDNhom == idVTYT).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                           Thuoc = Math.Round(kq.Where(p => p.dv.IDNhom == idThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                           CDHA = Math.Round(kq.Where(p => _lstIdCDHA.Contains(Convert.ToInt32(p.dv.IDNhom))).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                           Congkham = Math.Round(kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.a.ThanhTien), lamtron),

                           TienGiuong = Math.Round(rgChonBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.IDNhom == idNgayGiuongNgoaiTru || p.dv.IDNhom == idNgayGiuongNoiTru).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.IDNhom == idNgayGiuongNoiTru || p.dv.IDNhom == idNgayGiuongNgoaiTru).Sum(p => p.a.ThanhTien), lamtron),

                           Xetnghiem = Math.Round(kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.a.ThanhTien), lamtron),
                           Mau = Math.Round(kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.a.ThanhTien), lamtron),
                           TTPT = Math.Round(kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.a.ThanhTien), lamtron),
                           VTYT = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTien), lamtron),
                           DVKT_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.a.ThanhTien), lamtron),
                           Thuoc_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idThuocTyLe).Sum(p => p.a.ThanhTien), lamtron),

                           VTYT_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.a.ThanhTien), lamtron),
                           CPVanchuyen = Math.Round(kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien), lamtron),
                           CPNgoaiBH = Math.Round(kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien), lamtron),
                           ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           NgoaiDS = Math.Round(kq.Where(p => p.dv.IDNhom == 12 || p.dv.IDNhom == 5 || p.dv.IDNhom == 7
                                                            || p.a.MaICD == "C0" // mã ICD
                                                            || p.a.MaICD == "D0"
                                                            || p.a.SThe.Substring(0, 2) == "CA" // 2 ký tự đầu của số thẻ
                                                            || p.a.SThe.Substring(0, 2) == "QN"
                                                            || p.a.SThe.Substring(0, 2) == "CY"
                                                            || p.dv.TenDV.ToLower().Contains("thận nhân tạo thường qui") //Tên dịch vụ
                                                            || p.dv.TenDV.ToLower().Contains("lọc màng bụng cấp cứu liên tục")
                                                            || p.dv.TenDV.ToLower().Contains("lọc màng bụng chu kỳ")
                                                            ).Sum(p => p.a.ThanhTien), lamtron),
                           Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                           TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                           //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           TongCong = Math.Round(kq.Sum(p => p.a.TongCong), lamtron),

                       }).ToList();

            if (DungChung.Bien.MaBV == "24012")
            {
                q33 = (from a in q4
                       join dv in _ldv on a.MaDV equals dv.MaDV
                       where (cboDoiTuong.Text == "BHYT" ? ((rgNoiKCB.SelectedIndex < 3 ? a.TuyenDuoi == rgNoiKCB.SelectedIndex : true) && (cboNoiNgoaiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiNgoaiTinh.SelectedIndex : true)) : true)
                       where trongBH > 2 ? true : (a.TrongBH == trongBH)
                       //where ckcDVTheoYC.Checked? 
                       where a.XHH == 0
                       where (_MaKPc == 0 ? true : (DungChung.Bien.MaBV == "26007" ? a.MaKPBn == _MaKPc : a.MaKP == _MaKPc))//26007 tìm kiếm theo MaKP trong bảng bn Liễu y/c 22-07
                       group new { a, dv } by new { a.NNhap, a.MaKCB, a.SoDK, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.MaBNhan, a.NoiTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV, a.KhoaTongKet } into kq
                       select
                       new
                       {
                           kq.Key.MaKCB,
                           MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                           kq.Key.SoNgaydt,
                           kq.Key.DTuong,
                           kq.Key.NoiTru,
                           TrongBH = cboChiPhi.SelectedIndex,// kq.Key.TrongBH,
                           kq.Key.TuyenDuoi,
                           kq.Key.DTNT,
                           kq.Key.NgayTT,
                           kq.Key.MaDTuong,
                           kq.Key.CapCuu,
                           kq.Key.NgaySinh,
                           kq.Key.ThangSinh,
                           kq.Key.KhuVuc,
                           kq.Key.MaBV,
                           kq.Key.KetQua,
                           kq.Key.Status,
                           kq.Key.KhoaTongKet,
                           kq.Key.SoDK,
                           kq.Key.NNhap,
                           NhomPL = NhomPLoai(STTA(kq.Key.NoiTru, kq.Key.MaDTuong)),
                           NoiTinh = kq.Key.NoiTinh,
                           Tuyen = kq.Key.Tuyen,
                           MaBNhan = kq.Key.MaBNhan,
                           TenBNhan = kq.Key.TenBNhan,
                           NSinh = kq.Key.NamSinh,
                           SThe = kq.Key.SThe,
                           Nam = kq.Key.GTinh,
                           GTinh = kq.Key.GTinh,
                           MaCS = kq.Key.MaCS,
                           MaICD = kq.Key.MaICD,
                           ChanDoan = kq.Key.ChanDoan,
                           Ngaykham = kq.Key.NgayVao,
                           Ngayra = kq.Key.NgayRa,
                           Tuoi = kq.Key.Tuoi,
                           BHtu = kq.Key.HanBHTu,
                           BHden = kq.Key.HanBHDen,
                           Diachi = kq.Key.DChi,
                           Mabn = kq.Key.MaBNhan,
                           TBNCTT = kq.Sum(p => p.a.TBNCTT),
                           TBNTT = kq.Sum(p => p.a.TBNTT),
                           TienNguonKhac = kq.Sum(p => p.a.TienNguonKhac),
                           Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.IDNhom == idVTYT).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                           Thuoc = Math.Round(kq.Where(p => p.dv.IDNhom == idThuoc).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                           CDHA = Math.Round(kq.Where(p => _lstIdCDHA.Contains(Convert.ToInt32(p.dv.IDNhom))).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                           Congkham = Math.Round(kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.a.ThanhTien), lamtron),

                           TienGiuong = Math.Round(rgChonBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.IDNhom == idNgayGiuongNgoaiTru || p.dv.IDNhom == idNgayGiuongNoiTru).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.IDNhom == idNgayGiuongNoiTru || p.dv.IDNhom == idNgayGiuongNgoaiTru).Sum(p => p.a.ThanhTien), lamtron),

                           Xetnghiem = Math.Round(kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.a.ThanhTien), lamtron),
                           Mau = Math.Round(kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.a.ThanhTien), lamtron),
                           TTPT = Math.Round(kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.a.ThanhTien), lamtron),
                           VTYT = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTien), lamtron),
                           DVKT_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.a.ThanhTien), lamtron),
                           Thuoc_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idThuocTyLe).Sum(p => p.a.ThanhTien), lamtron),

                           VTYT_tl = Math.Round(kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.a.ThanhTien), lamtron),
                           CPVanchuyen = Math.Round(kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien), lamtron),
                           CPNgoaiBH = Math.Round(kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien), lamtron),
                           ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           NgoaiDS = Math.Round(kq.Where(p => p.dv.IDNhom == 12 || p.dv.IDNhom == 5 || p.dv.IDNhom == 7
                                                            || p.a.MaICD == "C0" // mã ICD
                                                            || p.a.MaICD == "D0"
                                                            || p.dv.TenDV.ToLower().Contains("thận nhân tạo thường qui") //Tên dịch vụ
                                                            || p.dv.TenDV.ToLower().Contains("lọc màng bụng cấp cứu liên tục")
                                                            || p.dv.TenDV.ToLower().Contains("lọc màng bụng chu kỳ")
                                                            ).Sum(p => p.a.ThanhTien), lamtron),
                           Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                           TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                           TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                           //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           TongCong = Math.Round(kq.Sum(p => p.a.TongCong), lamtron),

                       }).ToList();
            }

            if (rgChonBieu.SelectedIndex == 0)
            {
                var q3 = q33.Select(x => new repcount
                {
                    NhomPLoai = NhomPLoai(STTA(x.NoiTru, x.MaDTuong)),
                    NoiTinh = x.NoiTinh,
                    SoNgaydt = x.SoNgaydt,
                    DTuong = x.DTuong,
                    NoiTru = x.NoiTru,
                    grmadt = grMaDT(x.MaDTuong),
                    MaDTuong = x.MaDTuong,
                    NNhap = x.NNhap.ToString(),
                    KCBNNT = x.NoiTru == 0 ? "A.KHÁM, CHỮA BỆNH NGOẠI TRÚ " : "B. ĐIỀU TRỊ NỘI TRÚ",
                    STTa = STTA(x.NoiTru, x.MaDTuong),
                    DoiTuongTheoND = TheoMaDT(x.MaDTuong),
                    BenhNhanNNT = NoiTru(x.NoiTinh) + q33.Where(p => p.NoiTinh == x.NoiTinh && p.NoiTru == x.NoiTru && p.NhomPL == x.NhomPL && p.TenBNhan != null).Count() + " Lượt",
                    SSTC = STTB(x.NoiTinh),
                    TenBNhan = x.TenBNhan.ToUpper(),
                    NSinh = x.NSinh,
                    SThe = x.SThe,
                    GTinh = x.GTinh == 1 ? "Nam" : "Nữ",
                    MaICD = x.MaICD,
                    Ngaykham = DungChung.Ham.NgaySangChu((DateTime)x.Ngaykham, 11),
                    Ngayra = DungChung.Ham.NgaySangChu(Convert.ToDateTime(x.Ngayra), 11),
                    Tong = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.CDHA + x.Congkham + x.TienGiuong + x.Xetnghiem + x.Mau + x.TTPT + x.VTYT + x.VTYT_tl + x.DVKT_tl + x.Thuoc_tl + x.CPVanchuyen),
                    Thuoc = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.Thuoc_tl),
                    CDHA = cboChiPhi.SelectedIndex == 0 ? 0 : x.CDHA,
                    Congkham = cboChiPhi.SelectedIndex == 0 ? 0 : x.Congkham,
                    TienGiuong = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienGiuong,
                    Xetnghiem = cboChiPhi.SelectedIndex == 0 ? 0 : x.Xetnghiem,
                    Mau = cboChiPhi.SelectedIndex == 0 ? 0 : x.Mau,
                    TTPT = cboChiPhi.SelectedIndex == 0 ? 0 : x.TTPT,
                    VTYT = cboChiPhi.SelectedIndex == 0 ? 0 : (x.VTYT + x.VTYT_tl),
                    DVKT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.DVKT_tl,
                    Thuoc_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.Thuoc_tl,
                    VTYT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.VTYT_tl,
                    CPVanchuyen = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPVanchuyen,
                    CPNgoaiBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPNgoaiBH,
                    ThanhTien = cboChiPhi.SelectedIndex == 0 ? 0 : x.ThanhTien,
                    Tongchi = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongchi,
                    Tongcong = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongcong,
                    TienBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienBH,
                    TTTP = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? 0 : x.TienBH),//(q33.Where(o => MaDT().Contains(o.MaDTuong)).Count() < 1 ? x.TienBH : 0),
                    ND70 = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? x.TienBH : 0),//(q33.Where(o => MaDT().Contains(o.MaDTuong)).Count() >= 1 ? x.TienBH : 0),
                    CungChiTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNCTT,
                    TuTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNTT,
                    CPNBHYT = cboChiPhi.SelectedIndex == 1 ? 0 : x.ThanhTien,
                    TaiTrungUong = 0,
                    HoTroTaiTro = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienNguonKhac ?? 0,
                    NSDiaPhuong = 0,
                }).ToList();

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        repcount a2 = new repcount();
                        a2.NoiTru = 0;
                        a2.KCBNNT = "A. KHÁM, CHỮA BỆNH NGOẠI TRÚ";
                        a2.STTa = (i == 0 ? STTA(0, "") : STTA(0, "CY"));
                        a2.DoiTuongTheoND = (i == 0 ? TheoMaDT("") : TheoMaDT("CY"));
                        a2.SSTC = STTB(i + 1);
                        a2.BenhNhanNNT = "";
                        a2.Tong = 0;
                        q3.Add(a2);
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        repcount a2 = new repcount();
                        a2.NoiTru = 1;
                        a2.KCBNNT = "B. ĐIỀU TRỊ NỘI TRÚ";
                        a2.STTa = (i == 0 ? STTA(1, "") : STTA(1, "CY"));
                        a2.DoiTuongTheoND = (i == 0 ? TheoMaDT("") : TheoMaDT("CY"));
                        a2.SSTC = STTB(j + 1);
                        a2.BenhNhanNNT = "";
                        a2.Tong = 0;
                        q3.Add(a2);
                    }
                }
                frmIn frm = new frmIn();
                QLBV.BaoCao.rep_TT102 rep = new BaoCao.rep_TT102();
                rep.lblNgayThang.Text = "Từ ngày " + NgayTu.ToString("dd/MM/yyyy") + " đến ngày " + NgayDen.ToString("dd/MM/yyyy");
                rep.DataSource = q3;
                #region footer
                rep.colTongAB.Text = "TỔNG CỘNG A+B: " + q3.Where(p => p.TenBNhan != null).Count() + " LƯỢT";
                double SoTienCanThanhToan = q3.Sum(p => p.ND70 + p.TTTP + p.TaiTrungUong);
                rep.label9.Text = "Số tiền đề nghị thanh toán (viết bằng chữ): " + DungChung.Ham.DocTienBangChu(SoTienCanThanhToan, " đồng.");
                #endregion
                rep.Bingding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            else if (rgChonBieu.SelectedIndex == 1)
            {
                var q3 = q33.Select(x => new repcount
                {

                    STT_NoiDung = STTDoiTuong(x.NoiTru, TheoMaDT(x.MaDTuong), x.NoiTinh, x.MaDTuong),
                    NoiDung = NoiDungDoiTuong(TheoMaDT(x.MaDTuong), x.NoiTinh, x.MaDTuong),
                    NoiTru = x.NoiTru,
                    MaDTuong = x.MaDTuong,
                    TenBNhan = x.TenBNhan,
                    KCBNNT = DungChung.Bien.MaBV == "24012" ? (x.NoiTru == 0 ? "A. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "B. ĐIỀU TRỊ NỘI TRÚ") : (x.NoiTru == 0 ? "A1. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "A2. ĐIỀU TRỊ NỘI TRÚ"),
                    STTA = STTA(x.NoiTru, x.MaDTuong),
                    DoiTuongTheoND = TheoMaDT(x.MaDTuong),
                    Tong = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.CDHA + x.Congkham + x.TienGiuong + x.Xetnghiem + x.Mau + x.TTPT + x.VTYT + x.VTYT_tl + x.DVKT_tl + x.Thuoc_tl + x.CPVanchuyen),
                    Thuoc = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.Thuoc_tl),
                    CDHA = cboChiPhi.SelectedIndex == 0 ? 0 : x.CDHA,
                    Congkham = cboChiPhi.SelectedIndex == 0 ? 0 : x.Congkham,
                    TienGiuong = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienGiuong,
                    Xetnghiem = cboChiPhi.SelectedIndex == 0 ? 0 : x.Xetnghiem,
                    Mau = cboChiPhi.SelectedIndex == 0 ? 0 : x.Mau,
                    TTPT = cboChiPhi.SelectedIndex == 0 ? 0 : x.TTPT,
                    VTYT = cboChiPhi.SelectedIndex == 0 ? 0 : (x.VTYT + x.VTYT_tl),
                    DVKT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.DVKT_tl,
                    Thuoc_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.Thuoc_tl,
                    VTYT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.VTYT_tl,
                    CPVanchuyen = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPVanchuyen,
                    CPNgoaiBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPNgoaiBH,
                    ThanhTien = cboChiPhi.SelectedIndex == 0 ? 0 : x.ThanhTien,
                    Tongchi = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongchi,
                    Tongcong = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongcong,
                    TienBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienBH,
                    TTTP = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? 0 : x.TienBH),
                    ND70 = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? x.TienBH : 0),
                    CungChiTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNCTT,
                    TuTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNTT,
                    CPNBHYT = cboChiPhi.SelectedIndex == 1 ? 0 : x.ThanhTien,
                    TaiTrungUong = 0,
                    HoTroTaiTro = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienNguonKhac ?? 0,
                    NSDiaPhuong = 0,
                    SoNgaydt = x.SoNgaydt
                }).ToList();

                List<repcount> listAdd = new List<repcount>();

                if (DungChung.Bien.MaBV == "24012")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (j == 0)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    repcount a2 = new repcount();
                                    a2.NoiTru = i == 1 ? 1 : 0;
                                    a2.KCBNNT = (i == 0 ? "A. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "B. ĐIỀU TRỊ NỘI TRÚ");
                                    a2.DoiTuongTheoND = j == 0 ? "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146" : "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                                    a2.STTA = i == 0 ? (j == 0 ? "A.1" : "A.2") : (j == 0 ? "B.1" : "B.2");
                                    a2.BenhNhanNNT = "";
                                    a2.Tong = 0;
                                    switch (k)
                                    {
                                        case 0:
                                            {
                                                a2.STT_NoiDung = i == 0 ? "1" : "I";
                                                a2.NoiDung = "Đối tượng đang ký KCB tại cơ sở";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 1:
                                            {
                                                a2.STT_NoiDung = i == 0 ? "2" : "II";
                                                a2.NoiDung = "Bệnh nhân đa tuyến nội tỉnh đến";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 2:
                                            {
                                                a2.STT_NoiDung = i == 0 ? "3" : "III";
                                                a2.NoiDung = "Bệnh nhân đa tuyến ngoại tỉnh đến";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                for (int k = 0; k < 4; k++)
                                {
                                    repcount a2 = new repcount();
                                    a2.NoiTru = i == 1 ? 1 : 0;
                                    a2.KCBNNT = (i == 0 ? "A. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "B. ĐIỀU TRỊ NỘI TRÚ");
                                    a2.DoiTuongTheoND = j == 0 ? "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146" : "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                                    a2.STTA = i == 0 ? (j == 0 ? "A.1" : "A.2") : (j == 0 ? "B.1" : "B.2");
                                    a2.BenhNhanNNT = "";
                                    a2.Tong = 0;
                                    switch (k)
                                    {
                                        case 0:
                                            {
                                                a2.STT_NoiDung = "1";
                                                a2.NoiDung = "Đối tượng CY nội tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 1:
                                            {
                                                a2.STT_NoiDung = "2";
                                                a2.NoiDung = "Đối tượng CY Ngoại Tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 2:
                                            {
                                                a2.STT_NoiDung = "3";
                                                a2.NoiDung = "Đối tượng QN";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 3:
                                            {
                                                a2.STT_NoiDung = "4";
                                                a2.NoiDung = "Đối tượng CA";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (j == 0)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    repcount a2 = new repcount();
                                    a2.NoiTru = i == 1 ? 1 : 0;
                                    a2.KCBNNT = (i == 0 ? "A1. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "A2. ĐIỀU TRỊ NỘI TRÚ");
                                    a2.DoiTuongTheoND = j == 0 ? "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146" : "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                                    a2.STTA = j == 0 ? "I" : "II";
                                    a2.BenhNhanNNT = "";
                                    a2.Tong = 0;
                                    switch (k)
                                    {
                                        case 0:
                                            {
                                                a2.STT_NoiDung = "1";
                                                a2.NoiDung = "Người bệnh ĐKBĐ tại cơ sở KCB";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 1:
                                            {
                                                a2.STT_NoiDung = "2";
                                                a2.NoiDung = "Người bệnh đa tuyến đến nội tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 2:
                                            {
                                                a2.STT_NoiDung = "3";
                                                a2.NoiDung = "Người bệnh đa tuyến đến ngoại tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    repcount a2 = new repcount();
                                    a2.NoiTru = i == 1 ? 1 : 0;
                                    a2.KCBNNT = (i == 0 ? "A1. KHÁM, CHỮA BỆNH NGOẠI TRÚ" : "A2. ĐIỀU TRỊ NỘI TRÚ");
                                    a2.DoiTuongTheoND = j == 0 ? "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146" : "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                                    a2.STTA = i == 0 ? (j == 0 ? "I" : "II") : (j == 0 ? "I" : "II");
                                    a2.BenhNhanNNT = "";
                                    a2.Tong = 0;
                                    switch (k)
                                    {
                                        case 0:
                                            {
                                                a2.STT_NoiDung = "1";
                                                a2.NoiDung = "Người bệnh ĐKBĐ tại cơ sở KCB";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 1:
                                            {
                                                a2.STT_NoiDung = "2";
                                                a2.NoiDung = "Người bệnh đa tuyến đến nội tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                        case 2:
                                            {
                                                a2.STT_NoiDung = "3";
                                                a2.NoiDung = "Người bệnh đa tuyến đến ngoại tỉnh";
                                                listAdd.Add(a2);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }

                q3.AddRange(listAdd);

                var qs = (from q in q3
                          group new { q } by new { q.NoiDung, q.KCBNNT, q.DoiTuongTheoND, q.NoiTru, q.STTA, q.STT_NoiDung } into kq
                          select new Bieu80
                          {

                              stta = kq.Key.STTA,
                              NoiTru = kq.First().q.NoiTru,
                              DoiTuongKCB = kq.Key.KCBNNT,
                              DoiTuongTheoNghiDinh = kq.Key.DoiTuongTheoND,
                              STT_NoiDung = kq.Key.STT_NoiDung,
                              NoiDung = kq.Key.NoiDung,
                              SoLuot = kq.Where(o => o.q.TenBNhan != null && o.q.TenBNhan != "").Count(),
                              Tong = kq.Sum(p => p.q.Tong),
                              Thuoc = kq.Sum(p => p.q.Thuoc),
                              CDHA = kq.Sum(p => p.q.CDHA),
                              Congkham = kq.Sum(p => p.q.Congkham),
                              TienGiuong = kq.Sum(p => p.q.TienGiuong),
                              Xetnghiem = kq.Sum(p => p.q.Xetnghiem),
                              Mau = kq.Sum(p => p.q.Mau),
                              TTPT = kq.Sum(p => p.q.TTPT),
                              VTYT = kq.Sum(p => p.q.VTYT),
                              DVKT_tl = kq.Sum(p => p.q.DVKT_tl),
                              Thuoc_tl = kq.Sum(p => p.q.Thuoc_tl),
                              VTYT_tl = kq.Sum(p => p.q.VTYT_tl),
                              CPVanchuyen = kq.Sum(p => p.q.CPVanchuyen),
                              CPNgoaiBH = kq.Sum(p => p.q.CPNgoaiBH),
                              ThanhTien = kq.Sum(p => p.q.ThanhTien),
                              Tongchi = kq.Sum(p => p.q.Tongchi),
                              Tongcong = kq.Sum(p => p.q.Tongcong),
                              TienBH = kq.Sum(p => p.q.TienBH),
                              TienBN = kq.Sum(p => p.q.TienBN),
                              TTTP = kq.Sum(p => p.q.TTTP),
                              ND70 = kq.Sum(p => p.q.ND70),
                              CungChiTra = kq.Sum(p => p.q.CungChiTra),
                              TuTra = kq.Sum(p => p.q.TuTra),
                              CPNBHYT = kq.Sum(p => p.q.CPNBHYT),
                              TaiTrungUong = kq.Sum(p => p.q.TaiTrungUong),
                              NSDiaPhuong = kq.Sum(p => p.q.NSDiaPhuong),
                              HoTroTaiTro = kq.Sum(p => p.q.HoTroTaiTro),
                              SoNgayDT = kq.Sum(p => p.q.SoNgaydt),
                          }).OrderBy(o => o.STT_NoiDung).ToList();
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TuNgay", "Từ ngày " + NgayTu.ToString("dd/MM/yyyy") + " đến ngày " + NgayDen.ToString("dd/MM/yyyy"));
                _dic.Add("SoTienBangChu", "Số tiền đề nghị thanh toán ( viết bằng chữ): " + DungChung.Ham.DocTienBangChu(q3.Sum(p => p.ND70 + p.TTTP + p.TaiTrungUong), " đồng."));

                DungChung.Ham.Print(DungChung.Bien.MaBV == "24012" ? DungChung.PrintConfig.rep_TT102_80_24012 :  DungChung.PrintConfig.rep_TT102_80, qs, _dic, false);
            }
            // mau tong hop
            else
            {
                var q3 = q33.Select(x => new repcount
                {
                    STT_NoiDung = STTDoiTuong(x.NoiTru, TheoMaDT(x.MaDTuong), x.NoiTinh, x.MaDTuong),
                    NoiDung = NoiDungDoiTuong(TheoMaDT(x.MaDTuong), x.NoiTinh, x.MaDTuong),
                    NoiTru = x.NoiTru,
                    MaDTuong = x.MaDTuong,
                    TenBNhan = x.TenBNhan,
                    STTA = STTA(x.NoiTru, x.MaDTuong),
                    DoiTuongTheoND = TheoMaDT(x.MaDTuong),
                    Tong = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.CDHA + x.Congkham + x.TienGiuong + x.Xetnghiem + x.Mau + x.TTPT + x.VTYT + x.VTYT_tl + x.DVKT_tl + x.Thuoc_tl + x.CPVanchuyen),
                    Thuoc = cboChiPhi.SelectedIndex == 0 ? 0 : (x.Thuoc + x.Thuoc_tl),
                    CDHA = cboChiPhi.SelectedIndex == 0 ? 0 : x.CDHA,
                    Congkham = cboChiPhi.SelectedIndex == 0 ? 0 : x.Congkham,
                    TienGiuong = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienGiuong,
                    Xetnghiem = cboChiPhi.SelectedIndex == 0 ? 0 : x.Xetnghiem,
                    Mau = cboChiPhi.SelectedIndex == 0 ? 0 : x.Mau,
                    TTPT = cboChiPhi.SelectedIndex == 0 ? 0 : x.TTPT,
                    VTYT = cboChiPhi.SelectedIndex == 0 ? 0 : (x.VTYT + x.VTYT_tl),
                    DVKT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.DVKT_tl,
                    Thuoc_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.Thuoc_tl,
                    VTYT_tl = cboChiPhi.SelectedIndex == 0 ? 0 : x.VTYT_tl,
                    CPVanchuyen = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPVanchuyen,
                    CPNgoaiBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.CPNgoaiBH,
                    ThanhTien = cboChiPhi.SelectedIndex == 0 ? 0 : x.ThanhTien,
                    Tongchi = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongchi,
                    Tongcong = cboChiPhi.SelectedIndex == 0 ? 0 : x.Tongcong,
                    TienBH = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienBH,
                    TTTP = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? 0 : x.TienBH),
                    ND70 = cboChiPhi.SelectedIndex == 0 ? 0 : (MaDT().Contains(x.MaDTuong) ? x.TienBH : 0),
                    CungChiTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNCTT,
                    TuTra = cboChiPhi.SelectedIndex == 0 ? 0 : x.TBNTT,
                    CPNBHYT = cboChiPhi.SelectedIndex == 1 ? 0 : x.ThanhTien,
                    TaiTrungUong = 0,
                    HoTroTaiTro = cboChiPhi.SelectedIndex == 0 ? 0 : x.TienNguonKhac ?? 0,
                    NSDiaPhuong = 0,
                    SoNgaydt = x.SoNgaydt,
                    DTNT = x.DTNT,
                    NoiTinh = x.NoiTinh,
                    DTuong = x.DTuong,
                    NgoaiDS = x.NgoaiDS
                }).ToList();

                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenBV", "BỆNH VIỆN PHỤC HỒI CHỨC NĂNG BẮC GIANG");
                _dic.Add("MaBV", "MÃ CƠ SỞ Y TẾ: " + "24 - 012");
                _dic.Add("TieuDe", "SỐ LIỆU TỔNG HỢP 7980 ĐỀ NGHỊ THANH TOÁN TẠI CƠ SỞ KHÁM, CHỮA BỆNH");
                _dic.Add("TuNgay", "Từ ngày " + NgayTu.ToString("dd/MM/yyyy") + " đến ngày " + NgayDen.ToString("dd/MM/yyyy"));

                List<tonghop553> listAdd = new List<tonghop553>();

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tonghop553 a = new tonghop553();
                        if (i == 0)
                        {
                            string kcbnnt = "KHÁM BỆNH";
                            switch (j)
                            {
                                case 0:
                                    {
                                        a = addData(q3, kcbnnt, "A. BỆNH NHÂN NỘI TỈNH KCB BAN ĐẦU", 0, false, 1);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 1:
                                    {
                                        a = addData(q3, kcbnnt, "B. BỆNH NHÂN NỘI TỈNH ĐẾN", 0, false, 2);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 2:
                                    {
                                        a = addData(q3, kcbnnt, "C. BỆNH NHÂN NGOẠI TỈNH ĐẾN", 0, false, 3);
                                        listAdd.Add(a);
                                        break;
                                    }
                            }
                        }
                        else if (i == 1)
                        {
                            string kcbnnt = "ĐIỀU TRỊ NGOẠI TRÚ";
                            switch (j)
                            {
                                case 0:
                                    {
                                        a = addData(q3, kcbnnt, "A. BỆNH NHÂN NỘI TỈNH KCB BAN ĐẦU", 0, true, 1);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 1:
                                    {
                                        a = addData(q3, kcbnnt, "B. BỆNH NHÂN NỘI TỈNH ĐẾN", 0, true, 2);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 2:
                                    {
                                        a = addData(q3, kcbnnt, "C. BỆNH NHÂN NGOẠI TỈNH ĐẾN", 0, true, 3);
                                        listAdd.Add(a);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            string kcbnnt = "ĐIỀU TRỊ NỘI TRÚ";
                            switch (j)
                            {
                                case 0:
                                    {
                                        a = addData(q3, kcbnnt, "A. BỆNH NHÂN NỘI TỈNH KCB BAN ĐẦU", 1, false, 1);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 1:
                                    {
                                        a = addData(q3, kcbnnt, "B. BỆNH NHÂN NỘI TỈNH ĐẾN", 1, false, 2);
                                        listAdd.Add(a);
                                        break;
                                    }
                                case 2:
                                    {
                                        a = addData(q3, kcbnnt, "C. BỆNH NHÂN NGOẠI TỈNH ĐẾN", 1, false, 3);
                                        listAdd.Add(a);
                                        break;
                                    }
                            }
                        }
                    }
                }

                DungChung.Ham.Print(DungChung.PrintConfig.rep_TT102_80_TH, listAdd, _dic, false);
            }
        }

        private tonghop553 addData(List<repcount> q3, string KCBNNT, string NoiDung, int NoiTru, bool DTNT, int NoiTinh)
        {
            tonghop553 a = new tonghop553();
            a.KCBNNT = KCBNNT;
            a.NoiDung = NoiDung;
            try
            {
                var kq = q3.Where(p => p.DTuong.ToLower().Equals("bhyt")
                                    && p.NoiTru.Equals(NoiTru)
                                    && p.DTNT.Equals(DTNT)
                                    && p.NoiTinh.Equals(NoiTinh)
                                    ).ToList();
                if (kq.Count() > 0)
                {
                    a.LuotKCB = kq.Where(o => o.TenBNhan != null && o.TenBNhan != "").Count();
                    a.SoNgayDT = kq.Sum(p => p.SoNgaydt);
                    a.XetNghiem = kq.Sum(p => p.Xetnghiem);
                    a.CDHATDCN = kq.Sum(p => p.CDHA);
                    a.Thuoc = kq.Sum(p => p.Thuoc);
                    a.TTPT = kq.Sum(p => p.TTPT);
                    a.VTYT = kq.Sum(p => p.VTYT);
                    a.TienKham = kq.Sum(p => p.Congkham);
                    a.TienGiuong = kq.Sum(p => p.TienGiuong);
                    a.Tongcong = a.XetNghiem + a.CDHATDCN + a.Thuoc + a.TTPT + a.VTYT + a.TienKham + a.TienGiuong;
                    a.TienBHYT = kq.Sum(p => p.TienBH);
                    a.NgoaiQuyDS = kq.Sum(p => p.NgoaiDS);
                    a.BNhanChiTra = a.Tongcong - a.TienBHYT;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

            return a;
        }

        private int PloaiTheoNghiDinhs(string giatri)
        {
            int i = 0;
            switch (giatri)
            {
                case "A.1":
                    i = 1;
                    break;
                case "A.2":
                    i = 2;
                    break;
                case "B.1":
                    i = 3;
                    break;
                case "B.2":
                    i = 4;
                    break;
            }
            return i;
        }
        private List<string> MaDT()
        {
            string[] ma = new string[] { "QN", "CA", "CY" };
            List<string> madt = new List<string>();
            madt.AddRange(ma);
            return madt;
        }
        private int grMaDT(string MaDt)
        {
            int madt = 1;
            switch (MaDt)
            {// ("QN" || "CA" || "CY") ? "" : "",
                case "QN":
                case "CA":
                case "CY":
                    madt = 2;
                    break;
            }
            return madt;
        }
        public class repcount
        {

            public int NhomPLoai { get; set; }
            public int? NoiTinh { get; set; }
            public int? PloaiKCB { get; set; }

            public int PloaiTheoNghiDinh { get; set; }
            public string STTA { get; set; }
            public string TenSTT { get; set; }

            public string STT_NoiDung { get; set; }
            public string NoiDung { get; set; }
            public int Ploai { get; set; }


            public int grmadt { get; set; }
            public int? SoNgaydt { get; set; }
            public string DTuong { get; set; }
            public int? NoiTru { get; set; }
            public string MaDTuong { get; set; }
            public string NNhap { get; set; }
            public string KCBNNT { get; set; }
            public string STTa { get; set; }
            public string DoiTuongTheoND { get; set; }
            public string BenhNhanNNT { get; set; }
            public string SSTC { get; set; }

            public string TenBNhan { get; set; }
            public string NSinh { get; set; }
            public string SThe { get; set; }
            public string GTinh { get; set; }
            public string MaICD { get; set; }
            public string Ngaykham { get; set; }
            public string Ngayra { get; set; }

            public double Tong { get; set; }
            public double Thuoc { get; set; }
            public double CDHA { get; set; }
            public double Congkham { get; set; }
            public double TienGiuong { get; set; }
            public double Xetnghiem { get; set; }
            public double Mau { get; set; }
            public double TTPT { get; set; }
            public double VTYT { get; set; }
            public double DVKT_tl { get; set; }
            public double Thuoc_tl { get; set; }
            public double VTYT_tl { get; set; }
            public double CPVanchuyen { get; set; }
            public double CPNgoaiBH { get; set; }
            public double ThanhTien { get; set; }
            public double Tongchi { get; set; }
            public double Tongcong { get; set; }
            public double TienBH { get; set; }
            public double TienBN { get; set; }
            public double TTTP { get; set; }
            public double ND70 { get; set; }
            public double CungChiTra { get; set; }
            public double TuTra { get; set; }
            public double CPNBHYT { get; set; }
            public double TaiTrungUong { get; set; }
            public double NSDiaPhuong { get; set; }
            public double HoTroTaiTro { get; set; }
            public bool DTNT { get; set; }
            public double NgoaiDS { get; set; }
        }
        public class Bieu80
        {
            public string DoiTuongKCB { get; set; }
            public string DoiTuongTheoNghiDinh { get; set; }
            public int PhanLoai { get; set; }
            public string STT_NoiDung { get; set; }
            public string NoiDung { get; set; }
            public int? NoiTru { get; set; }
            public string stta { get; set; }
            public int PLSTTA { get; set; }
            public int SoLuot { get; set; }
            public double Tong { get; set; }
            public double Thuoc { get; set; }
            public double CDHA { get; set; }
            public double Congkham { get; set; }
            public double TienGiuong { get; set; }
            public double Xetnghiem { get; set; }
            public double Mau { get; set; }
            public double TTPT { get; set; }
            public double VTYT { get; set; }
            public double DVKT_tl { get; set; }
            public double Thuoc_tl { get; set; }
            public double VTYT_tl { get; set; }
            public double CPVanchuyen { get; set; }
            public double CPNgoaiBH { get; set; }
            public double ThanhTien { get; set; }
            public double Tongchi { get; set; }
            public double Tongcong { get; set; }
            public double TienBH { get; set; }
            public double TienBN { get; set; }
            public double TTTP { get; set; }
            public double ND70 { get; set; }
            public double CungChiTra { get; set; }
            public double TuTra { get; set; }
            public double CPNBHYT { get; set; }
            public double TaiTrungUong { get; set; }
            public double NSDiaPhuong { get; set; }
            public double HoTroTaiTro { get; set; }
            public int? SoNgayDT { get; set; }
        }
        class tonghop553
        {
            public string KCBNNT { get; set; }
            public string NoiDung { get; set; }
            public int LuotKCB { get; set; }
            public int? SoNgayDT { get; set; }
            public double XetNghiem { get; set; }
            public double CDHATDCN { get; set; }
            public double Thuoc { get; set; }
            public double Mau { get; set; }
            public double TTPT { get; set; }
            public double VTYT { get; set; }
            public double TienKham { get; set; }
            public double TienGiuong { get; set; }
            public double TienBHYT { get; set; }
            public double BNhanChiTra { get; set; }
            public double NgoaiQuyDS { get; set; }
            public double Tongcong { get; set; }
        }


        #region Dành cho biểu 79
        private string NoiTru(int? NoiTinh)
        {
            string nt = "";
            if (NoiTinh == 1)
            {
                nt = "NGƯỜI BỆNH ĐKBĐ TẠI CƠ SỞ KCB: ";
            }
            else if (NoiTinh == 2)
            {
                nt = "BỆNH NHÂN NỘI TỈNH ĐẾN: ";
            }
            else { nt = "BỆNH NHÂN NGOẠI TỈNH ĐẾN: "; }
            return nt;

        }

        private string STTB(int? NoiTinh)
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                string STT = "";
                if (NoiTinh == 1)
                {
                    STT = "1";
                }
                else if (NoiTinh == 2)
                {
                    STT = "2";
                }
                else { STT = "3"; }
                return STT;
            }
            else
            {
                string STT = "";
                if (NoiTinh == 1)
                {
                    STT = "I";
                }
                else if (NoiTinh == 2)
                {
                    STT = "II";
                }
                else { STT = "III"; }
                return STT;
            }
            

        }
        private string STTA(int? NoiTru, string MaDT)
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                string STT = "I";
                switch (MaDT)
                {
                    case "QN":
                    case "CA":
                    case "CY":
                        STT = "II";
                        break;
                }
                return STT;
            }
            else
            {
                string STT = "";
                if (NoiTru == 0)
                {
                    STT = "A." + 1;
                    switch (MaDT)
                    {
                        case "QN":
                        case "CA":
                        case "CY":
                            STT = "A." + 2;
                            break;
                    }
                }
                else if (NoiTru == 1)
                {
                    STT = "B." + 1;
                    switch (MaDT)
                    {
                        case "QN":
                        case "CA":
                        case "CY":
                            STT = "B." + 2;
                            break;
                    }
                }
                return STT;
            }
        }
        private string TheoMaDT(string MaDT)
        {
            string TenDT = "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146";
            switch (MaDT)
            {// ("QN" || "CA" || "CY") ? "" : "",
                case "QN":
                case "CA":
                case "CY":
                    TenDT = "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                    break;
            }
            return TenDT;
        }

        #endregion

        #region dành cho biểu 80

        private string NoiTru80(int? NoiTinh)
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                string nt = "";
                if (NoiTinh == 1)
                {
                    nt = "Người bệnh ĐKBĐ tại cơ sở KCB";
                }
                else if (NoiTinh == 2)
                {
                    nt = "Người bệnh đa tuyến đến nội tỉnh";
                }
                else { nt = "Người bệnh đa tuyến đến ngoại tỉnh"; }
                return nt;
            }
            else
            {
                string nt = "";
                if (NoiTinh == 1)
                {
                    nt = "Đối tượng đang ký KCB tại cơ sở";
                }
                else if (NoiTinh == 2)
                {
                    nt = "Bệnh nhân đa tuyến nội tỉnh đến";
                }
                else { nt = "Bệnh nhân đa tuyến ngoại tỉnh đến"; }
                return nt;
            }
        }

        private void rgChonBieu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rgDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string STT_NoiTru80(int? NoiTinh, int? noiTru)
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                string nt = "";
                if (NoiTinh == 1)
                {
                    nt = "1";// (noiTru == 1 ? "I" : "1");
                }
                else if (NoiTinh == 2)
                {
                    nt = "2";// (noiTru == 1 ? "II" : "2");
                }
                else
                {
                    nt = "3";// (noiTru == 1 ? "III" : "3");
                }
                return nt;
            }
            else
            {
                string nt = "";
                if (NoiTinh == 1)
                {
                    nt = (noiTru == 1 ? "I" : "1");
                }
                else if (NoiTinh == 2)
                {
                    nt = (noiTru == 1 ? "II" : "2");
                }
                else
                {
                    nt = (noiTru == 1 ? "III" : "3");
                }
                return nt;

            }


        }
        private string NoiDungDoiTuong(string DoiTuongTheoNghiDinh, int? PloaiKCB, string MaDauThe = "")
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                return NoiTru80(PloaiKCB);
            }
            else
            {
                string a = "";
                switch (DoiTuongTheoNghiDinh)
                {
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146":
                        a = NoiTru80(PloaiKCB);
                        break;
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70":
                        a = DoiTuongTheoNghiDinh70(MaDauThe, (int)PloaiKCB);
                        break;
                }
                return a;
            }
        }

        private string STTDoiTuong(int? noiTru, string DoiTuongTheoNghiDinh, int? PloaiKCB, string MaDauThe = "")
        {
            if (DungChung.Bien.MaBV != "24012" && rgChonBieu.SelectedIndex == 1)
            {
                string a = "";
                switch (DoiTuongTheoNghiDinh)
                {
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146":
                        a = STT_NoiTru80(PloaiKCB, noiTru);
                        break;
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70":
                        a = STT_DoiTuongTheoNghiDinh70(MaDauThe, (int)PloaiKCB);
                        break;
                }
                return STT_NoiTru80(PloaiKCB, noiTru);
            }
            else
            {
                string a = "";
                switch (DoiTuongTheoNghiDinh)
                {
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146":
                        a = STT_NoiTru80(PloaiKCB, noiTru);
                        break;
                    case "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70":
                        a = STT_DoiTuongTheoNghiDinh70(MaDauThe, (int)PloaiKCB);
                        break;
                }
                return a;
            }
        }

        private string DoiTuongTheoNghiDinh70(string MaDauThe, int PloaiKCB)
        {
            string a = "";
            switch (MaDauThe)
            {
                case "QN":
                    a = "Đối tượng QN";
                    break;
                case "CA":
                    a = "Đối tượng CA";
                    break;
                case "CY":
                    if (PloaiKCB == 2)
                    {
                        a = "Đối tượng CY nội tỉnh";
                    }
                    else
                    {
                        a = "Đối tượng CY Ngoại Tỉnh";
                    }
                    break;
            }
            return a;
        }

        private string STT_DoiTuongTheoNghiDinh70(string MaDauThe, int PloaiKCB)
        {
            string a = "";
            switch (MaDauThe)
            {
                case "QN":
                    a = "3";
                    break;
                case "CA":
                    a = "4";
                    break;
                case "CY":
                    if (PloaiKCB == 2)
                    {
                        a = "1";
                    }
                    else
                    {
                        a = "2";
                    }
                    break;
            }
            return a;
        }


        private int PloDoiTuong(int NoiTru, string DoiTuongTheoNghiDinh, string NoiDung)
        {
            int i = 0;
            if (NoiTru == 0)
            {
                if (DoiTuongTheoNghiDinh == "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146")
                {
                    if (NoiDung == "Đối tượng đang ký KCB tại cơ sở")
                    {
                        i = 1;
                    }
                    if (NoiDung == "Bệnh nhân đa tuyến nội tỉnh đến")
                    {
                        i = 2;
                    }
                    if (NoiDung == "Bệnh nhân đa tuyến ngoại tỉnh đến")
                    {
                        i = 3;
                    }
                }
                if (DoiTuongTheoNghiDinh == "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70")
                {
                    if (NoiDung == "Đối tượng CY nội tỉnh")
                    {
                        i = 4;
                    }
                    if (NoiDung == "Đối tượng CY Ngoại Tỉnh")
                    {
                        i = 5;
                    }
                    if (NoiDung == "Đối tượng QN")
                    {
                        i = 6;
                    }
                    if (NoiDung == "Đối tượng CA")
                    {
                        i = 7;
                    }
                }
            }
            if (NoiTru == 1)
            {
                if (DoiTuongTheoNghiDinh == "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146")
                {
                    if (NoiDung == "Đối tượng đang ký KCB tại cơ sở")
                    {
                        i = 8;
                    }
                    if (NoiDung == "Bệnh nhân đa tuyến nội tỉnh đến")
                    {
                        i = 9;
                    }
                    if (NoiDung == "Bệnh nhân đa tuyến ngoại tỉnh đến")
                    {
                        i = 10;
                    }
                }
                if (DoiTuongTheoNghiDinh == "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70")
                {
                    if (NoiDung == "Đối tượng CY nội tỉnh")
                    {

                        i = 11;
                    }
                    if (NoiDung == "Đối tượng CY Ngoại Tỉnh")
                    {
                        i = 12;
                    }
                    if (NoiDung == "Đối tượng QN")
                    {
                        i = 13;
                    }
                    if (NoiDung == "Đối tượng CA")
                    {
                        i = 14;
                    }
                }
            }
            return i;
        }
        private int NhomPLoai(string NhomDT)
        {
            int i = 0;
            switch (NhomDT)
            {
                case "A.1":
                    i = 1;
                    break;
                case "A.2":
                    i = 2;
                    break;
                case "B.1":
                    i = 3;
                    break;
                case "B.2":
                    i = 4;
                    break;
            }
            return i;
        }


        #endregion

        List<int> _lDSMaDV = new List<int>();
        private void PassData(List<int> lmaDV)
        {
            _lDSMaDV = data.DichVus.Select(p => p.MaDV).ToList();

        }
        List<KPhong> _lKphong = new List<KPhong>();
        private string MaKPQD(int _MaKPc)
        {
            string rs = "";
            var q = _lKphong.Where(p => p.MaKP == _MaKPc).ToList();
            if (q.Count > 0)
                rs = q.First().MaQD == null ? "" : q.First().MaQD.ToString();
            return rs;
        }

        private void lchkDauThe_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

            if (e.Index == 0)
            {
                if (lchkDauThe.GetItemChecked(0) == true)
                    lchkDauThe.CheckAll();
                else
                    lchkDauThe.UnCheckAll();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}