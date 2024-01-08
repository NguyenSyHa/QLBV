using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frm_CNDichVu_5084 : DevExpress.XtraEditors.XtraForm
    {
        public frm_CNDichVu_5084()
        {
            InitializeComponent();
        }
        int _ploai = -1;
        public frm_CNDichVu_5084(int pl)
        {
            InitializeComponent();
            _ploai = pl;
        }
        QLBV_Database.QLBVEntities data;
        List<DichVu> _lDichVuAll = new List<DichVu>();// danh sách tất cả dịch vụ
        List<DichVu> _lDichVu = new List<DichVu>();// danh sách dịch vụ hiển thị
        List<DuongDung> _lDuongDung = new List<DuongDung>();
        List<DonVi> _ldonvi = new List<DonVi>();
        private void frm_CNDichVu_5084_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lDichVuAll = new List<DichVu>();
            _lDichVuAll = data.DichVus.Where(p=>p.PLoai==_ploai).OrderBy(p => p.MaDV).ToList();
            _lC5084 = data.C5084.OrderBy(p => p.idDichVu).ToList();
            _lDuongDung = data.DuongDungs.ToList();
            _ldonvi = data.DonVis.ToList();
            loadDSDV();
            loadDSDV_5084();
            SetCheckAll();
        }

        private void SetCheckAll()
        {
            ck_DongGoi_CV.Checked = ckChonTatCa.Checked;
            ck_DonViTinh_CV.Checked = ckChonTatCa.Checked;
            ck_DuongDung_CV.Checked = ckChonTatCa.Checked;
            ck_HamLuong_CV.Checked = ckChonTatCa.Checked;
            ck_HoatChat_CV.Checked = ckChonTatCa.Checked;
            ck_MaDvQD_Cv.Checked = ckChonTatCa.Checked;
            ck_MaKhoa_CV.Checked = ckChonTatCa.Checked;
            ck_NhaSX_CV.Checked = ckChonTatCa.Checked;
            ck_NoiDK_CV.Checked = ckChonTatCa.Checked;
            ck_SDK_CV.Checked = ckChonTatCa.Checked;
            ck_SLuong_CV.Checked = ckChonTatCa.Checked;
            ck_SoQD_CV.Checked = ckChonTatCa.Checked;
            ck_TenDV_CV.Checked = ckChonTatCa.Checked;
            ck_TieuChuan_CV.Checked = ckChonTatCa.Checked;
            ck_TuoiTho_CV.Checked = ckChonTatCa.Checked;
            ck_NgayQD_Cv.Checked = ckChonTatCa.Checked;

        }

        private void loadDSDV()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lDichVu = new List<DichVu>();
            _lDichVu = data.DichVus.Where(p => p.PLoai == _ploai).OrderBy(p => p.MaDV).ToList();
            grc_DichVu.DataSource = _lDichVu;
        }

        //int _maDV = 0, _idDichVu = 0;// _maDV : mã DV trong bảng DichVu; _idDichVu: idDichVu trong bảng 5084
        DichVu _dichvu = new DichVu();

        private void grv_DichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int row = grv_DichVu.FocusedRowHandle;
            int _maDV = 0;
            _dichvu = new DichVu();
            _c5084 = new C5084();
            resetDV();
            resetDV_Cv();
            if (row >= 0 && grv_DichVu.GetRowCellValue(row, colMaDV) != null)
            {
                _maDV = Convert.ToInt32(grv_DichVu.GetRowCellValue(row, colMaDV));
                getDVByMaDV(_maDV);
            }
            lblThongBao.Text = "";
            lblThongBao.ForeColor = Color.Green;
            

        }

        public bool getDVByMaDV(int maDV)
        {
            if (maDV > 0)
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                _lDichVuAll = data.DichVus.Where(p => p.PLoai == _ploai).OrderBy(p => p.MaDV).ToList();               
                _dichvu = _lDichVuAll.Single(p => p.MaDV == maDV);
                txtMaDvQD.Text = _dichvu.MaDV.ToString();
                txtTenDV_RG.Text = String.IsNullOrEmpty(_dichvu.TenRG) ? _dichvu.TenDV : _dichvu.TenRG;
                txtTimKiem.Text = txtTenDV_RG.Text;
                txtHamLuong.Text = _dichvu.HamLuong;
                txtDonViTinh.Text = _dichvu.DonVi;
                txtHoatChat.Text = _dichvu.TenHC;
                txtDuongDung.Text = getDuongDung(_dichvu.MaDuongDung) == "" ? _dichvu.DuongD : getDuongDung(_dichvu.MaDuongDung);
                txtSDK.Text = _dichvu.SoDK;
                txtNhaSX.Text = getTenDonVi(_dichvu.MaCTySX) == "" ? _dichvu.NhaSX : getTenDonVi(_dichvu.MaCTySX);
                txtNoiDK.Text = getTenDonVi(_dichvu.MaCTyDK);
                txtDongGoi.Text = _dichvu.QCPC;
                txtTieuChuan.Text = _dichvu.TieuChuan;
                txtTuoiTho.Text = _dichvu.TuoiTho;
                //thiếu mã khoa
                txtSoQD.Text = _dichvu.SoQD;
                txtNgayQD.Text = _dichvu.NgayQD == null ? "" : _dichvu.NgayQD.Value.ToShortDateString();

                return true;
            }
            else
                return false;

        }

        private string getTenDonVi(int? macty)
        {
            if (macty == null)
                return "";
            else
            {
                var q = _ldonvi.Where(p => p.MaDonVi == macty).ToList();
                if (q.Count > 0)
                    return q.First().TenDonVi;
                else return "";
            }
        }

        private string getDuongDung(string maduongdung)
        {
            var q = _lDuongDung.Where(p => p.MaDuongDung == maduongdung).ToList();
            if (q.Count > 0)
                return q.First().DuongDung1;
            else return "";
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            int ma = _dichvu.MaDV;
            if (ma > 0)
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _dichvu = data.DichVus.Single(p => p.MaDV == ma);
                if (ck_MaDvQD_Cv.Checked)
                    _dichvu.MaQD = _c5084.MaDV;
                if (ck_TenDV_CV.Checked)
                    _dichvu.TenRG = _c5084.TenDV;
                if (ck_HamLuong_CV.Checked)
                    _dichvu.HamLuong = _c5084.HamLuong;
                if (ck_DonViTinh_CV.Checked)
                    _dichvu.DonVi = _c5084.DonViTinh;
                if (ck_HoatChat_CV.Checked)
                    _dichvu.TenHC = _c5084.HoatChat;
                if (ck_DuongDung_CV.Checked)
                    _dichvu.DuongD = txtDuongDung_CV.Text;
                if (ck_SDK_CV.Checked)
                    _dichvu.SoDK = _c5084.SoDangKy;
                if (ck_NhaSX_CV.Checked)
                {
                    _dichvu.NhaSX = txtNhaSX_CV.Text;
                    _dichvu.MaCTySX = _c5084.MaCtySX;
                }
                if (ck_NoiDK_CV.Checked)
                    _dichvu.MaCTyDK = _c5084.MaCtyDk;
                if (ck_DongGoi_CV.Checked)
                    _dichvu.QCPC = _c5084.DongGoi;
                if (ck_TieuChuan_CV.Checked)
                    _dichvu.TieuChuan = _c5084.TieuChuan;
                if (ck_TuoiTho_CV.Checked)
                    _dichvu.TuoiTho = _c5084.TuoiTho;
                // thiếu mã khoa
                if (ck_SLuong_CV.Checked && !String.IsNullOrEmpty(txtSLuong_CV.Text.Trim()))
                    _dichvu.SLuong = Convert.ToDouble(txtSLuong_CV.Text.Trim());
                if (ck_SoQD_CV.Checked)
                    _dichvu.SoQD = txtSoQD_CV.Text;
                if (ck_NgayQD_Cv.Checked && dtNgayQD.EditValue != null)
                    _dichvu.NgayQD = Convert.ToDateTime(dtNgayQD.EditValue);
                if (data.SaveChanges() >= 0)
                {
                    lblThongBao.Text = "Cập nhật dịch vụ " + _dichvu.MaDV.ToString() + " thành công";
                    lblThongBao.ForeColor = Color.Green;
                    getDVByMaDV(_dichvu.MaDV);
                }
                else
                {
                    lblThongBao.Text = "Thêm mới dịch vụ " +_dichvu.MaDV.ToString() +" thất bại";
                    lblThongBao.ForeColor = Color.Red;
                }
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            loadDSDV_5084();
        }

        private void resetDV()
        {
            txtMaDvQD.Text = "";
            txtTenDV_RG.Text = "";
            txtTimKiem.Text = "";
            txtHamLuong.Text = "";
            txtDonViTinh.Text = "";
            txtHoatChat.Text = "";
            txtDuongDung.Text = "";
            txtSDK.Text = "";
            txtNhaSX.Text = "";
            txtNoiDK.Text = "";
            txtDongGoi.Text = "";
            txtTieuChuan.Text = "";
            txtTuoiTho.Text = "";
            //thiếu mã khoa
            txtSoQD.Text = "";
            txtNgayQD.Text = "";
        }
        private void resetDV_Cv()
        {
            txtMaDvQD_Cv.Text = "";
            txtTenDV_CV.Text = "";
            txtHamLuong_CV.Text = "";
            txtDonViTinh_CV.Text = "";
            txtHoatChat_CV.Text = "";
            txtDuongDung_CV.Text = "";
            txtSDK_CV.Text = "";
            txtNhaSX_CV.Text = "";
            txtNoiDK_CV.Text = "";
            txtDongGoi_CV.Text = "";
            txtTieuChuan_CV.Text = "";
            txtTuoiTho_CV.Text = "";
            txtSLuong_CV.Text = "";
            //thiếu mã khoa
            txtSoQD_CV.Text = "";//Số quyết định trúng thầu
        }
        private void loadDSDV_5084()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<C5084> _l5084 = new List<C5084>();
            if (txtTimKiem.Text.Trim() != "")
            {
                if (ckTimChinhXac.Checked)
                {
                    string tk = txtTimKiem.Text.Trim().ToLower();
                    _l5084 = data.C5084.Where(p => p.TenDV.ToLower().Contains(tk)).ToList();

                }
                else
                {
                    List<string> _listTK = txtTimKiem.Text.Split(' ').ToList();
                    _l5084 = (from l in _listTK
                              from c in data.C5084.Where(p => p.TenDV.ToLower().Contains(l.ToLower()))
                              select c).ToList();
                }
            }
            lblCount.Text = _l5084.Count.ToString();
            grc_CV.DataSource = _l5084;
        }

        private void ckTimChinhXac_CheckedChanged(object sender, EventArgs e)
        {
            loadDSDV_5084();
        }

        private void grv_CV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            int row = grv_CV.FocusedRowHandle;
            int _idDV = 0;
           // resetDV_Cv();
            if (row >= 0 && grv_CV.GetRowCellValue(row, col_CV_IDDV) != null)
            {
                _idDV = Convert.ToInt32(grv_CV.GetRowCellValue(row, col_CV_IDDV));
                getDVByIDDV_5084(_idDV);

            }

        }

        C5084 _c5084 = new C5084();
        List<C5084> _lC5084 = new List<C5084>();
        private void setCheck(bool b) {
            ck_MaDvQD_Cv.Checked = b;
            ck_TenDV_CV.Checked = b;
            ck_HamLuong_CV.Checked = b;
            ck_DonViTinh_CV.Checked = b;
            ck_HoatChat_CV.Checked = b;
            ck_DuongDung_CV.Checked = b;
            ck_SDK_CV.Checked = b;
            ck_NhaSX_CV.Checked = b;
            ck_NoiDK_CV.Checked = b;
            ck_DongGoi_CV.Checked = b;
            ck_TieuChuan_CV.Checked = b;
            ck_TuoiTho_CV.Checked = b;
            ck_SLuong_CV.Checked = b;
        }
        private bool getDVByIDDV_5084(int _idDV)
        {
            _c5084 = new C5084();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            setCheck(true);
            if (_idDV > 0)
            {

                _lC5084 = data.C5084.OrderBy(p => p.idDichVu).ToList();
                _c5084 = _lC5084.Single(p => p.idDichVu == _idDV);
                txtMaDvQD_Cv.Text = _c5084.MaDV;
                txtTenDV_CV.Text = _c5084.TenDV;
                txtHamLuong_CV.Text = _c5084.HamLuong;
                txtDonViTinh_CV.Text = _c5084.DonViTinh;
                txtHoatChat_CV.Text = _c5084.HoatChat;
                txtDuongDung_CV.Text = getDuongDung(_c5084.MaDuongDung);
                txtSDK_CV.Text = _c5084.SoDangKy;
                txtNhaSX_CV.Text = getTenDonVi(_c5084.MaCtySX);
                txtNoiDK_CV.Text = getTenDonVi(_c5084.MaCtyDk);
                txtDongGoi_CV.Text = _c5084.DongGoi;
                txtTieuChuan_CV.Text = _c5084.TieuChuan;
                txtTuoiTho_CV.Text = _c5084.TuoiTho;
                txtSLuong_CV.Text = "";
                //thiếu mã khoa
                txtSoQD_CV.Text = "";//Số quyết định trúng thầu
                if (string.IsNullOrEmpty(txtMaDvQD_Cv.Text))
                    ck_MaDvQD_Cv.Checked = false;
                if (string.IsNullOrEmpty(txtTenDV_CV.Text))
                    ck_TenDV_CV.Checked = false;
                if (string.IsNullOrEmpty(txtHamLuong_CV.Text))
                    ck_HamLuong_CV.Checked = false;
                if (string.IsNullOrEmpty(txtDonViTinh_CV.Text))
                    ck_DonViTinh_CV.Checked = false;
                if (string.IsNullOrEmpty(txtHoatChat_CV.Text))
                    ck_HoatChat_CV.Checked = false;
                if (string.IsNullOrEmpty(txtDuongDung_CV.Text))
                    ck_DuongDung_CV.Checked = false;
                if (string.IsNullOrEmpty(txtSDK_CV.Text))
                    ck_SDK_CV.Checked = false;
                if (string.IsNullOrEmpty(txtNhaSX_CV.Text))
                    ck_NhaSX_CV.Checked = false;
                if (string.IsNullOrEmpty(txtNoiDK_CV.Text))
                    ck_NoiDK_CV.Checked = false;
                if (string.IsNullOrEmpty(txtDongGoi_CV.Text))
                    ck_DongGoi_CV.Checked = false;
                if (string.IsNullOrEmpty(txtTieuChuan_CV.Text))
                    ck_TieuChuan_CV.Checked = false;
                if (string.IsNullOrEmpty(txtTuoiTho_CV.Text))
                    ck_TuoiTho_CV.Checked = false;
                if (string.IsNullOrEmpty(txtSLuong_CV.Text))
                    ck_SLuong_CV.Checked = false;
                return true;
            }
            else
            {
                setCheck(false);
                return false;
            }
        }

        private void ckChonTatCa_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckAll();
        }

        private void grv_DichVu_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int row = e.RowHandle;
            grv_DichVu.FocusedRowHandle = row;
        }

        private void grv_CV_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int row = e.RowHandle;
            grv_CV.FocusedRowHandle = row;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang nâng cấp");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q =(from dv in data.DichVus.Where(p => p.Status == 1 && p.PLoai==1) join nhom in data.NhomDVs.Where(p=>p.TenNhomCT=="Thuốc trong danh mục BHYT" || p.TenNhomCT=="Thuốc thanh toán theo tỷ lệ")
                    on dv.IDNhom equals nhom.IDNhom
                    select dv).ToList();
            var q0 = (from dv in q
                      join ncc in data.NhaCCs on dv.MaCC equals ncc.MaCC into kq
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          dv,
                          TenCC = kq1 == null ? "" : kq1.TenCC
                      }).ToList();
            var q1 = (from dv in q0
                      join dvi in data.DonVis on dv.dv.MaCTySX equals dvi.MaDonVi into kq
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          dv.dv,
                          dv.TenCC,
                          TenDonVi = kq1 == null ? "" : kq1.TenDonVi,
                          Nuoc = kq1 == null ? "" : kq1.Nuoc,
                          DiaChi = kq1 == null ? "" : kq1.DiaChi
                      }).ToList();
            var qdv = (from dv in q1
                       join dd in data.DuongDungs on dv.dv.MaDuongDung equals dd.MaDuongDung into kq
                       from kq1 in kq.DefaultIfEmpty()
                       select new { dv.dv, dv.TenCC, dv.TenDonVi, dv.Nuoc, dv.DiaChi, DuongDung1 = kq1 == null ? "" : kq1.DuongDung1 }).ToList();

     
            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "stt", "ma_hoat_chat", "hoat_chat", "ma_duong_dung", "duong_dung", "ham_luong", "ten_thuoc", "so_dang_ky", "dong_goi", "don_vi_tinh", "don_gia", "don_gia_tt", "so_luong", "ma_cskcb", "hang_sx", "nuoc_sx", "nha_thau", "quyet_dinh", "cong_bo", "ma_thuoc_bv" };
            string _filePath = "D:\\CV_5084.xls";
            int[] _arrWidth = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[qdv.Count + 1, 20];
            for (int i = 0; i < 20; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in qdv)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.dv.MaQD;
                DungChung.Bien.MangHaiChieu[num, 2] = r.dv.TenHC;
                DungChung.Bien.MangHaiChieu[num, 3] = r.dv.MaDuongDung;
                DungChung.Bien.MangHaiChieu[num, 4] = r.DuongDung1;
                DungChung.Bien.MangHaiChieu[num, 5] = r.dv.HamLuong;
                DungChung.Bien.MangHaiChieu[num, 6] = r.dv.TenDV;
                DungChung.Bien.MangHaiChieu[num, 7] = r.dv.SoDK;
                DungChung.Bien.MangHaiChieu[num, 8] = r.dv.QCPC;
                DungChung.Bien.MangHaiChieu[num, 9] = r.dv.DonVi;
                DungChung.Bien.MangHaiChieu[num, 10] = r.dv.DonGia;
                DungChung.Bien.MangHaiChieu[num, 11] = r.dv.DonGia;
                DungChung.Bien.MangHaiChieu[num, 12] = r.dv.SLuong;
                DungChung.Bien.MangHaiChieu[num, 13] = DungChung.Bien.MaBV;
                DungChung.Bien.MangHaiChieu[num, 14] = r.TenDonVi;
                DungChung.Bien.MangHaiChieu[num, 15] = r.Nuoc;
                DungChung.Bien.MangHaiChieu[num, 16] = r.TenCC;
                DungChung.Bien.MangHaiChieu[num, 17] = r.dv.SoQD;
                DungChung.Bien.MangHaiChieu[num, 18] = r.dv.NgayQD;
                DungChung.Bien.MangHaiChieu[num, 19] = r.dv.MaDV;
                num++;
            }
            QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);

        }

        







    }
}