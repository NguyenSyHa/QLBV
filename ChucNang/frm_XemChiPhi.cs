using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormNhap;
using QLBV_Database.Common;

namespace QLBV.FormThamSo
{
    public partial class frm_XemChiPhi : DevExpress.XtraEditors.XtraForm
    {
        public frm_XemChiPhi()
        {
            InitializeComponent();
        }
        string _tenbn = "";
        int _mabn = 0;
        public frm_XemChiPhi(int mabn, string tenbn)
        {
            InitializeComponent();
            _mabn = mabn;
            _tenbn = tenbn;
        }

        public class VienPhiViewModel
        {
            public string TenNhom { get; set; }
            public double? TyLeTT { get; set; }
            public int? Mien { get; set; }
            public int? MaKP { get; set; }
            public int? madv { get; set; }
            public int? PLDV { get; set; }
            public double? dongia { get; set; }
            public int? TrongBH { get; set; }
            public string donvi { get; set; }
            public double? soluong { get; set; }
            public double? thanhtien { get; set; }
            public string DongY { get; set; }
            public int? STT { get; set; }
            public int IDNhom { get; set; }
            //DongY = kq.Key.DongY,
        }

        #region Ktra ra viện
        private bool KTRavien(int _mabn)
        {
            try
            {
                var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.NoiTru).ToList();
                if (noitru.Count > 0)
                {
                    if (noitru.First() == 1)
                    {
                        var rv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                        if (rv.Count > 0)
                        {
                            if (rv.First().Status == 3)
                            {
                                MessageBox.Show("Bệnh nhân chốn viện, bạn không thể thanh toán");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân chưa làm thủ tục ra viện");
                            return false;
                        }
                    }
                    else
                    {
                        var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
                        if (vv.Count > 0)
                        {
                            var rv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                            if (rv.Count > 0)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân chưa làm thủ tục ra viện");
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {

                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi hàm 'KTRavien' kiểm tra ra viện");
                return false;
            }
        }
        #endregion

        //QLBVEntities _dataContext = new QLBVEntities(DungChung.Bien.StrCon);
        QLBVEntities _dataContext = EntityDbContext.DbContext;
        List<DungChung.Ham.TrongDMBHYT> _lTrongDM = new List<DungChung.Ham.TrongDMBHYT>();

        public class PhanLoaiTamThu
        {
            string phanLoai;

            public string PhanLoai
            {
                get { return phanLoai; }
                set { phanLoai = value; }
            }
            int iD;

            public int ID
            {
                get { return iD; }
                set { iD = value; }
            }
        }

        private void frm_XemChiPhi_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                panelControl1.Enabled = false;
                panelControl1.Visible = true;
            }
            if (DungChung.Bien.MaBV != "23456")
            {
                btnXemChiPhi.Visible = false;
                groupControl10.Dock = DockStyle.Fill;
            }

            if (DungChung.Bien.MaBV == "14017")
            {
                colMaKP.Visible = false;
            }
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<PhanLoaiTamThu> _plTamThu = new List<PhanLoaiTamThu>();
            _plTamThu.Add(new PhanLoaiTamThu { PhanLoai = "Tạm thu", ID = 0 });
            _plTamThu.Add(new PhanLoaiTamThu { PhanLoai = "Thu thanh toán", ID = 1 });
            _plTamThu.Add(new PhanLoaiTamThu { PhanLoai = "Chi thanh toán", ID = 2 });
            _plTamThu.Add(new PhanLoaiTamThu { PhanLoai = "Thu thẳng (Thu trực tiếp)", ID = 3 });
            _plTamThu.Add(new PhanLoaiTamThu { PhanLoai = "Chi tạm ứng", ID = 4 });
            lup_PhanLoai.DataSource = _plTamThu;
            var _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDTamUng).ToList();
            grcTamUng.DataSource = _litamung.ToList();
            _lTrongDM = DungChung.Ham._SetValue_TrongDMBH();
            luptrongDM.DataSource = _lTrongDM;
            lup_TrongDMtu.DataSource = _lTrongDM;
            lupNguoiTT.EditValue = DungChung.Bien.MaCB;
            var q = (from kp in _dataContext.KPhongs select new { kp.TenKP, kp.MaKP }).OrderBy(p => p.TenKP).ToList();
            lupBPKe.Properties.DataSource = q.ToList();
            lupMaKP.DataSource = q.ToList();
            var pk = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            if (pk.Count > 0)
                lupBPKe.EditValue = pk.First().MaKP;
            int Thanhtoan = 0;
            var dv = _dataContext.DichVus.ToList();
            lupMaDVtt.DataSource = dv.ToList();
            _lCanBo = _dataContext.CanBoes.ToList();
            lupNguoiTT.Properties.DataSource = _lCanBo;
            lup_CanBoThu.DataSource = _lCanBo;
            var kt = (from ds in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDDon) select ds).ToList();

            dtNgayTT.DateTime = System.DateTime.Now;


            if (DungChung.Ham.KTraTT(_dataContext, _mabn))
            {
                var tt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                if (tt.Count > 0 && tt.First().MaKP != null)
                {
                    lupBPKe.EditValue = tt.First().MaKP;
                    dtNgayTT.DateTime = tt.First().NgayTT.Value;
                    if (tt.First().MaCB != null)
                        lupNguoiTT.EditValue = tt.First().MaCB;
                }
                btnThanhToan.Enabled = false;
                dtNgayTT.Enabled = false;
                btnXoa.Enabled = true;
                btnXem.Enabled = true;
                Thanhtoan = 1;
                //lupBPKe.Properties.ReadOnly = true;
                //lupNguoiTT.Properties.ReadOnly = true;
                dtNgayTT.Properties.ReadOnly = true;
            }
            else
            {
                btnThanhToan.Enabled = true;
                dtNgayTT.Enabled = true;
                btnXoa.Enabled = false;
                btnXem.Enabled = false;
                Thanhtoan = 0;
                //lupBPKe.Properties.ReadOnly = false;
                //lupNguoiTT.Properties.ReadOnly = false;
                dtNgayTT.Properties.ReadOnly = false;
                lupBPKe.EditValue = DungChung.Bien.MaKP;
                lupNguoiTT.EditValue = DungChung.Bien.MaCB;
            }
            if (DungChung.Bien.MaBV != "24297")
            {
                panChucNang.Visible = DungChung.Ham.checkQuyen(this.Name)[0];
            }
            else
            {
                var canbo = _dataContext.CanBoes.FirstOrDefault(p => p.MaCB == DungChung.Bien.MaCB).MaKPsd;
                var benhnhan = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn).MaKP;
                panChucNang.Visible = canbo.Contains(benhnhan.ToString());
            }           
            switch (DungChung.Bien.MaBV)
            {
                case "12121":
                    panChucNang.Visible = false;
                    break;
                case "01071":
                    panChucNang.Visible = false;
                    break;
                case "01049":
                    panChucNang.Visible = false;
                    break;
                case "04004":
                    panChucNang.Visible = true;
                    break;
                case "06007":
                    panChucNang.Visible = true;
                    break;
                case "04011":
                    panChucNang.Visible = true;
                    break;
                case "08204":
                    panChucNang.Visible = true;
                    break;
                case "30002":
                    panChucNang.Visible = true;
                    break;
                case "04016":
                    panChucNang.Visible = true;
                    break;
                case "08602":
                    panChucNang.Visible = true;
                    break;
                case "04006":
                    panChucNang.Visible = true;
                    break;
                //case "24009":
                //    panelBottom.Visible = true;
                //    break;
                case "26007":
                    panChucNang.Visible = true;
                    break;
                case "33080":
                    panChucNang.Visible = true;
                    break;
                case "33050":
                    panChucNang.Visible = true;
                    break;
                case "30009":
                    panChucNang.Visible = true;
                    break;
                case "12001":
                    panChucNang.Visible = true;
                    break;
                case "01830":
                    panChucNang.Visible = true;
                    break;
                case "19048":
                    panChucNang.Visible = true;
                    break;
                case "04002":
                    panChucNang.Visible = true;
                    break;
                case "08104":
                    panChucNang.Visible = true;
                    break;
            }

            this.Text = "Xem chi phí bệnh nhân: " + _tenbn;

            var chiphi = (from kd in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.KieuDon != -2)
                          join kdct in _dataContext.DThuoccts.OrderBy(p => p.IDDonct) on kd.IDDon equals kdct.IDDon
                          join d in _dataContext.DichVus on kdct.MaDV equals d.MaDV
                          join n in _dataContext.NhomDVs on d.IDNhom equals n.IDNhom
                          select new { kd, kdct, n, d }).ToList();
            foreach (var item in chiphi)
            {
                double tyle = item.kdct.TyLeTT;
                int Mien = item.kdct.Mien;
                if (tyle == 0)
                    tyle = 100;
                if (item.kdct.MienCT == 0)
                {
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        double tienchenh = item.kdct.ThanhTien - (item.kdct.SoLuong * item.kdct.DonGia * tyle * (double)(100 - Mien) / 10000);
                        if (tienchenh > 1 || tienchenh < -1)
                        {
                            var update = _dataContext.DThuoccts.Single(p => p.IDDonct == item.kdct.IDDonct);

                            update.TyLeTT = tyle;
                            update.ThanhTien = Math.Round(update.SoLuong * update.DonGia * tyle * (double)(100 - Mien) / 10000, DungChung.Bien.LamTronSo);                           
                            _dataContext.SaveChanges();
                        }
                    }
                    else
                    {
                        double tienchenh = item.kdct.ThanhTien - (item.kdct.SoLuong * item.kdct.DonGia * tyle * 0.01);
                        if (tienchenh > 1 || tienchenh < -1)
                        {
                            var update = _dataContext.DThuoccts.Single(p => p.IDDonct == item.kdct.IDDonct);

                            update.TyLeTT = tyle;
                            update.ThanhTien = Math.Round(update.SoLuong * update.DonGia * tyle * 0.01, DungChung.Bien.LamTronSo);
                            _dataContext.SaveChanges();
                        }
                    }
                }
            }
            var vienphi = (from kd in chiphi
                           group new { kd } by new { kd.n.STT, kd.d.DongY, kd.n.TenNhom, kd.kdct.TyLeTT, kd.kdct.Mien, kd.kdct.MaKP, kd.kd.PLDV, kd.kdct.MaDV, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.TrongBH, kd.n.IDNhom } into kq
                           select new
                           {
                               DongY = kq.Key.DongY,
                               //DongY = kq.Key.DongY,
                               TenNhom = kq.Key.TenNhom,
                               kq.Key.TyLeTT,
                               kq.Key.Mien,
                               MaKP = kq.Key.MaKP,
                               madv = kq.Key.MaDV,
                               kq.Key.PLDV,
                               dongia = kq.Key.DonGia,
                               TrongBH = kq.Key.TrongBH,
                               donvi = kq.Key.DonVi,
                               STT = kq.Key.STT,
                               soluong = kq.Sum(p => p.kd.kdct.SoLuong),
                               thanhtien = kq.Sum(p => p.kd.kdct.ThanhTien),
                               kq.Key.IDNhom
                           }).ToList();

            var result1 = vienphi.Where(p => p.soluong > 0 && p.TenNhom == "Thuốc trong danh mục BHYT").OrderBy(p => p.PLDV).ThenBy(p => p.TrongBH).ThenBy(p => p.donvi).ToList();
            var result2 = vienphi.Where(p => p.soluong > 0 && p.TenNhom != "Thuốc trong danh mục BHYT").OrderBy(p => p.PLDV).ThenBy(p => p.TrongBH).ThenBy(p => p.donvi).ToList();
            //var result3 = vienphi.Where(p => p.soluong > 0 && p.TenNhom == "Khám bệnh").OrderBy(p => p.PLDV).ThenBy(p => p.TrongBH).ThenBy(p => p.donvi).ToList();
            List<VienPhiViewModel> kqvp = new List<VienPhiViewModel>();
            if (result1.Count > 0)
            {
                foreach (var item in result1)
                {
                    VienPhiViewModel moi = new VienPhiViewModel();
                    if (item.STT > 10)
                        moi.TenNhom = item.STT + ". " + item.TenNhom;
                    else
                        moi.TenNhom = "0" + item.STT + ". " + item.TenNhom;
                    moi.STT = item.STT;
                    moi.TyLeTT = item.TyLeTT;
                    moi.Mien = item.Mien;
                    moi.MaKP = item.MaKP;
                    moi.madv = item.madv;
                    moi.PLDV = item.PLDV;
                    moi.dongia = item.dongia;
                    moi.TrongBH = item.TrongBH;
                    moi.donvi = item.donvi;
                    moi.soluong = item.soluong;
                    moi.thanhtien = item.thanhtien;
                    moi.DongY = item.DongY == 1 ? "Thuốc đông y" : "Thuốc tây y, Dịch truyền";
                    moi.IDNhom = item.IDNhom;
                    kqvp.Add(moi);
                }
            }

            if (result2.Count > 0)
            {
                foreach (var item in result2)
                {
                    VienPhiViewModel moi = new VienPhiViewModel();
                    if (item.STT > 10)
                        moi.TenNhom = item.STT + ". " + item.TenNhom;
                    else
                        moi.TenNhom = "0" + item.STT + ". " + item.TenNhom;
                    moi.STT = item.STT;
                    moi.TyLeTT = item.TyLeTT;
                    moi.Mien = item.Mien;
                    moi.MaKP = item.MaKP;
                    moi.madv = item.madv;
                    moi.PLDV = item.PLDV;
                    moi.dongia = item.dongia;
                    moi.TrongBH = item.TrongBH;
                    moi.donvi = item.donvi;
                    moi.soluong = item.soluong;
                    moi.thanhtien = item.thanhtien;
                    moi.IDNhom = item.IDNhom;
                    //moi.DongY = item.DongY == 1 ? "Thuốc đông y" : "Thuốc tây y";
                    kqvp.Add(moi);
                }
            }

            grcThanhToan.DataSource = kqvp;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                int _mien = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMien));
                textEdit1.Text = _mien.ToString();
            }
        }
        int _HangBV = 4;
        int idvp = 0;
        int _tuyen = 1;
        string _DTuong = "";
        double _tienBH = 0;
        double _tienBN = 0;
        double _tienTThu = 0;
        int _pttt = 0;
        string _muc = "";
        public bool KTraLuu()
        {
            if (string.IsNullOrEmpty(dtNgayTT.Text))
            {
                MessageBox.Show("Bạn chưa chọn ngày TT");
                dtNgayTT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupBPKe.Text))
            {
                MessageBox.Show("Bạn chưa chọn bộ phận TT");
                lupBPKe.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupNguoiTT.Text))
            {
                MessageBox.Show("Bạn chưa chọn cán bộ TT");
                lupNguoiTT.Focus();
                return false;
            }
            return true;
        }
        #region
        public bool KTCongKham(int mabn)
        {
            var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan == (mabn)).Select(p => p.NoiTru).ToList();
            if (noitru.Count > 0 && noitru.First().Value == 1)
            {
                var kt = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == (mabn))
                          join dt in _dataContext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                          join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT == ("Giường điều trị nội trú")) on tn.IDNhom equals nhom.IDNhom
                          select dtct.IDDonct).ToList();
                if (kt.Count <= 0)
                {
                    DialogResult _result = MessageBox.Show("Bệnh nhân: " + _tenbn + " chưa có tiền ngày giường, \n Bạn vẫn muốn thanh toán?", "Hỏi thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                        return false;
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var ck = (from nhom in _dataContext.NhomDVs.Where(p => p.TenNhom.Contains("khám"))
                          join dvu in _dataContext.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                          select new { dvu.DonGia, dvu.MaDV, dvu.DonVi }).ToList();
                int madv = 0;
                if (ck.Count > 0)
                {
                    madv = ck.First().MaDV;
                }
                var kt = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == (mabn))
                          join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == (mabn)) on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                          where (dtct.MaDV == madv)
                          select dtct.IDDonct).ToList();
                if (kt.Count <= 0)
                {
                    DialogResult _result = MessageBox.Show("Bệnh nhân: " + _tenbn + " chưa có tiền khám bệnh, \n Bạn vẫn muốn thanh toán?", "Hỏi thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                        return false;
                    else
                        return true;
                }
                else
                {
                    if (kt.Count == 1)
                        return false;
                    else
                    {
                        DialogResult _result = MessageBox.Show("Bệnh nhân có: " + kt.Count + " lần công khám, \n Bạn vẫn muốn thanh toán?", "Hỏi thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                            return false;
                        else
                            return true;
                    }
                }
            }
            // return false;
        }
        #endregion
        public delegate void getvalue(bool tt);
        public getvalue _getdata;
        public void _getNgayTachCP(DateTime ngaycp, bool tt2the)
        {
            _ngaytachcp = ngaycp;
            TT2The = tt2the;
        }
        DateTime _ngaytachcp = DateTime.Now;
        bool TT2The = false;

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[0];

            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị khóa");
            }

            int _makptt = 0;
            if (lupBPKe.Text != null)
            {
                _makptt = Convert.ToInt32(lupBPKe.EditValue);
            }

            var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
            if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(10) > DateTime.Now))
            {
                string time = "Thời gian khám bệnh của bệnh nhân < 10 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) \nBạn có muốn tiếp tục?";
                DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;
            }

            var qkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            var ktAdmin = _dataContext.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP && p.PLoai == "Admin").ToList();
            if (qkb.Count > 0 && ktAdmin.Count == 0)
            {

                if (qkb.First().MaKP != _makptt)
                {
                    MessageBox.Show("Khoa phòng hiện tại không phải khoa phòng cuối cùng. Bạn không thể thanh toán");
                    _sua = false;
                }
                else
                {
                    if (qkb.First().PhuongAn == 3 && qkb.First().NgayNghi != null)
                    {
                        MessageBox.Show("Bệnh nhân đã chuyển phòng khám, bạn không thể thanh toán");
                        _sua = false;
                    }
                }
            }
            if (_sua)
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (ktraGiaTT39(_dataContext, _mabn, dtNgayTT.DateTime))
                {
                    if (DungChung.Bien.MaBV == "27001" && DungChung.Ham.KTCBCongKhamBNKB(_dataContext, _mabn))
                    {
                        MessageBox.Show("Cán bộ khám không khớp nhau!", "Thông báo");
                        return;
                    }
                    if (DungChung.Ham.Ktra2The(_dataContext, _mabn))
                    {
                        QLBV.FormThamSo.frm_NgayTachCP2The frm = new QLBV.FormThamSo.frm_NgayTachCP2The(_mabn, dtNgayTT.DateTime);
                        frm.getdata = new QLBV.FormThamSo.frm_NgayTachCP2The.getValue(_getNgayTachCP);
                        frm.ShowDialog();
                        if (TT2The)
                        {
                            if (DungChung.Ham.ThanhToan2The(_dataContext, _mabn, dtNgayTT.DateTime))
                            {
                                _kieu = 0;
                                btnXem_Click(sender, e);
                                bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30009 tự động gửi BHXH
                                bool bnchuyenvien = false;
                                if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30303")
                                {
                                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                                    if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                        ktraNgoaiGio = true;
                                }

                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var qchuynvien = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn && p.Status == 1).FirstOrDefault();
                                    if (qchuynvien != null)
                                        bnchuyenvien = true;
                                }
                                int noingoai = CheckNoiNgoaiTru(_mabn);
                                if ((noingoai == 0 && (DungChung.Bien.PPXuat_BHXH[0] == 4 || DungChung.Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (DungChung.Bien.PPXuat_BHXH[1] == 4 || DungChung.Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio || bnchuyenvien)
                                {
                                    List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                    _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _mabn, Export = false });
                                    BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[10];
                                    BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[11];
                                    BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                    clsBHXH._updateExPort(_dataContext, _listVPBH, _mabn, false, 1);
                                }
                                //}
                                frm_XemChiPhi_Load(sender, e);
                            }
                        }
                        else
                        {
                            if (DungChung.Ham.ThanhToan(_dataContext, _mabn, dtNgayTT.DateTime, _makptt))
                            {
                                _kieu = 0;
                                btnXem_Click(sender, e);
                                //if (DungChung.Bien.MaBV != "20001")
                                //{
                                bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30009 tự động gửi BHXH
                                bool bnchuyenvien = false;
                                if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30303")
                                {
                                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                                    if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                        ktraNgoaiGio = true;
                                }
                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var qchuynvien = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn && p.Status == 1).FirstOrDefault();
                                    if (qchuynvien != null)
                                        bnchuyenvien = true;
                                }
                                int noingoai = CheckNoiNgoaiTru(_mabn);
                                if ((noingoai == 0 && (DungChung.Bien.PPXuat_BHXH[0] == 4 || DungChung.Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (DungChung.Bien.PPXuat_BHXH[1] == 4 || DungChung.Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio || bnchuyenvien)
                                {
                                    List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                    _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _mabn, Export = false });
                                    BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[10];
                                    BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[11];
                                    BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                    clsBHXH._updateExPort(_dataContext, _listVPBH, _mabn, false, 1);
                                }

                                //}
                                frm_XemChiPhi_Load(sender, e);
                            }
                        }
                    }
                    else
                    {
                        if (DungChung.Ham.ThanhToan(_dataContext, _mabn, dtNgayTT.DateTime, _makptt))
                        {
                            _kieu = 0;
                            btnXem_Click(sender, e);
                            //if (DungChung.Bien.MaBV != "20001")
                            //{
                            bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30009 tự động gửi BHXH
                            bool bnchuyenvien = false;
                            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30303")
                            {
                                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                                if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                    ktraNgoaiGio = true;
                            }
                            if (DungChung.Bien.MaBV == "26007")
                            {
                                var qchuynvien = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn && p.Status == 1).FirstOrDefault();
                                if (qchuynvien != null)
                                    bnchuyenvien = true;
                            }


                            int noingoai = CheckNoiNgoaiTru(_mabn);
                            if ((noingoai == 0 && (DungChung.Bien.PPXuat_BHXH[0] == 4 || DungChung.Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (DungChung.Bien.PPXuat_BHXH[1] == 4 || DungChung.Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio || bnchuyenvien)
                            {
                                List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _mabn, Export = false });
                                BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[10];
                                BHYT.us_Export_XML_2348.user = DungChung.Bien.xmlFilePath_LIS[11];
                                BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                clsBHXH._updateExPort(_dataContext, _listVPBH, _mabn, false, 1);
                            }
                            //}

                            frm_XemChiPhi_Load(sender, e);
                        }
                    }
                }
                //_getdata(true);
            }

        }

        private bool ktraGiaTT39(QLBV_Database.QLBVEntities _data, int _int_maBN, DateTime ngaytt)
        {
            bool kt = true;
            var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var qvv = _data.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var qdichvu = _data.DichVus.Where(p => p.PLoai == 2).ToList();
            List<int> lIDCD = new List<int>();
            if (qbn != null && ngaytt >= DungChung.Bien.ngayGiaMoiTT39 && DungChung.Bien.ngayGiaMoiTT39 > new DateTime(2000, 01, 01))
            {

                if (qbn.NNhap != null && qbn.DTuong == "BHYT")
                {
                    string ms = "";
                    if ((qbn.NNhap.Value >= DungChung.Bien.ngayGiaMoiTT39 && qvv == null) || (qvv != null && qvv.NgayVao >= DungChung.Bien.ngayGiaMoiTT39))
                    {
                        var qcls = (from cls in _data.CLS.Where(p => p.MaBNhan == _int_maBN)
                                    join cd in _data.ChiDinhs.Where(p => p.TrongBH != null && p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                                    select new { cls.IdCLS, cls.NgayThang, cd.MaDV, cd.DonGia, cd.IDCD }).ToList();

                        foreach (var a in qcls)
                        {
                            var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                            if (qdv != null)
                            {
                                if (a.DonGia != qdv.DonGiaTT39)
                                {
                                    ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                    lIDCD.Add(a.IDCD);
                                }
                            }
                        }

                        var qdthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                                       join dtct in _data.DThuoccts.Where(p => p.TrongBH != null && p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                       join dichvu in _data.DichVus.Where(p => p.PLoai == 2) on dtct.MaDV equals dichvu.MaDV
                                       select new { dt.NgayKe, dt.IDDon, dtct.IDDonct, dtct.MaDV, dtct.DonGia, dtct.IDCD, dtct.SoLuong, dtct.TyLeTT }).ToList();

                        foreach (var a in qdthuoc)
                        {
                            var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                            if (qdv != null)
                            {
                                if (a.DonGia != qdv.DonGiaTT39)
                                {

                                    if (a.IDCD == null)
                                    {
                                        ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                    }
                                    else
                                    {
                                        if (lIDCD.Count == 0 || !lIDCD.Contains(a.IDCD ?? 0))
                                        {
                                            ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                        }
                                    }


                                }
                            }
                        }
                        if (ms != "")
                        {
                            MessageBox.Show("Các dịch vụ: " + ms + " chưa được cập nhật về giá TT39");
                            kt = false;
                        }
                    }
                }
            }

            return kt;
        }

        int _maubk = 0;
        int _kieu = 0;// 0: in tự động, 1: theo mẫu chọn
        private void btnXem_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30340" || DateTime.Now > Convert.ToDateTime("01/12/2018"))
                DungChung.Ham.In_BangKe_2018(_dataContext, _mabn);
            else
                DungChung.Ham.In_BangKe01_02(_dataContext, _mabn, _maubk, _kieu);
            #region bỏ

            #endregion
        }
        string _lydoLodg = "";
        public void _getLyDoLog(string a)
        {
            _lydoLodg = a;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //try
            //{
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[2];
            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị khóa");
            }
            if (_sua && _mabn > 0 && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayTT.DateTime, "KhoaVP") == false)
            {
                List<VienPhi> xoa = new List<VienPhi>();
                bool _xoavp = true;
                xoa = (from bn in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn) select bn).ToList();

                if (xoa.Count <= 0)
                {
                    MessageBox.Show("bệnh nhân không có thanh toán để xóa!");
                }
                else
                {
                    if (xoa.First().ExportBHXH || xoa.First().ExportBYT || xoa.First().Export)
                    {
                        MessageBox.Show("Dữ liệu đã được gửi, bạn không thể xóa");
                        return;
                    }
                    if (!DungChung.Ham._KiemTraCBSuaXoa(_dataContext, xoa.First().MaCB, DungChung.Bien.MaCB))
                    {
                        _xoavp = false;
                        MessageBox.Show("Mã cán bộ không khớp, bạn không thể xóa!");
                    }
                    if (_xoavp)
                    {
                        var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn && p.NoiTru == 0).FirstOrDefault();
                        var ktxd = _dataContext.NhapDs.Where(p => p.MaBNhan == _mabn).ToList();
                        if (ktxd.Count > 0 && bn != null)
                        {
                            MessageBox.Show("Bệnh nhân đã được xuất dược, bạn không thể xóa!");
                            _xoavp = false;
                        }
                    }
                    //if(DungChung.Bien.MaBV=="27023")
                    //{
                    //    if(DungChung.Bien.PLoaiKP!=DungChung.Bien.st_PhanLoaiKP.Admin)
                    //    {
                    //        if(xoa.First().MaCB!=DungChung.Bien.MaCB)
                    //        {
                    //            MessageBox.Show("")
                    //        }
                    //    }
                    //}

                    if (xoa.Count > 0 && xoa.First().Export == true)
                    {
                        MessageBox.Show("Dữ liệu đã được gửi, bạn không thể xóa");
                        _xoavp = false;
                    }
                    if (_xoavp)
                    {
                        FormNhap.usTamThu_TToan tamthuTT = new FormNhap.usTamThu_TToan();
                        if (tamthuTT.KtraTCTT(_mabn, 0))
                        {
                            int _idxoa = xoa.First().idVPhi;
                            DialogResult result;
                            result = MessageBox.Show("Bạn muốn xóa thanh toán BN: " + _tenbn, "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                bool _suaq = true;
                                if (_suaq)
                                {
                                    string tb = "Nhập lý do xóa thanh toán BN: " + _mabn.ToString();
                                    QLBV.frm_GetLyDoLog frm = new frm_GetLyDoLog(tb);
                                    frm.ok = new frm_GetLyDoLog._getdata(_getLyDoLog);
                                    frm.ShowDialog();
                                    if (!string.IsNullOrEmpty(_lydoLodg))
                                        _suaq = true;
                                    else
                                        _suaq = false;
                                }
                                if (_suaq)
                                {
                                    List<VienPhict> sl = new List<VienPhict>();
                                    sl = _dataContext.VienPhicts.Where(p => p.idVPhi == _idxoa).ToList();
                                    if (sl.Count > 0)
                                    {

                                        foreach (var s in sl)
                                        {
                                            var dtct = _dataContext.VienPhicts.Single(p => p.idVPhict == s.idVPhict);
                                            _dataContext.VienPhicts.Remove(dtct);

                                        }
                                    }
                                    var xoad = _dataContext.VienPhis.Single(p => p.idVPhi == _idxoa);
                                    _dataContext.VienPhis.Remove(xoad);
                                    if (_dataContext.SaveChanges() > 0)
                                    {
                                        LOG moi = new LOG();
                                        moi.DateLog = DateTime.Now;
                                        moi.LyDo = _lydoLodg;
                                        moi.UserName = DungChung.Bien.TenDN;
                                        moi.MaBNhan = _mabn;
                                        moi.IdForm = 905;
                                        moi.MaCB = DungChung.Bien.MaCB;
                                        moi.ComputerName = SystemInformation.ComputerName;
                                        moi.Status = 4;
                                        _dataContext.LOGs.Add(moi);
                                        _dataContext.SaveChanges();
                                        _lydoLodg = "";
                                    }

                                    // xóa sao lưu\
                                    //DungChung.Ham.xoaChiPhiDV(_dataContext, _idxoa);
                                    // xóa ra viện
                                    try
                                    {
                                        int _ntru = 0;
                                        var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.NoiTru).ToList();
                                        if (noitru.Count > 0)
                                        {
                                            _ntru = noitru.First().Value;
                                        }
                                        if (_ntru == 0)
                                        {
                                            var vvien = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();

                                            if (vvien.Count <= 0)
                                            {
                                                var ravien = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn && p.Status == 2).ToList();  // kiểm tra nếu là chuyển viện thì không xóa Status==2
                                                if (DungChung.Bien.MaBV == "24272")
                                                {
                                                    DungChung.Ham._setStatus(_mabn, 2);
                                                }
                                                else if (ravien.Count > 0) // 24272 yêu cầu k xóa ra viện, HIS 2384
                                                {
                                                    if (DungChung.Bien.MaBV == "27001")
                                                    {
                                                        DungChung.Ham._LuuXoaRaVien(_dataContext, _mabn, DateTime.Now, "luu", 2);
                                                    }
                                                    else
                                                    {
                                                        DungChung.Ham._LuuXoaRaVien(_dataContext, _mabn, DateTime.Now, "Xoa", 2);
                                                    }
                                                    //var _xoaravien = _dataContext.RaViens.Single(p => p.MaBNhan == _mabn);
                                                    //_dataContext.Remove(_xoaravien);
                                                    //_dataContext.SaveChanges();
                                                    //DungChung.Ham._setStatus(_mabn, 1);
                                                }
                                            }
                                            else
                                            {
                                                DungChung.Ham._setStatus(_mabn, 2);
                                            }
                                        }
                                        else
                                        {
                                            DungChung.Ham._setStatus(_mabn, 2);
                                        }
                                        grcThanhToan.DataSource = "";
                                        //DungChung.Ham._LuuXoaRaVien(_dataContext, _mabn, DateTime.Now, "Xoa", 2);
                                        MessageBox.Show("Xóa thành công");
                                        frm_XemChiPhi_Load(sender, e);

                                    }


                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi xóa ra viện:" + ex.Message);
                                    }
                                }
                                // sau này tách rieng code ravien chuyển thành     
                            }
                        } //
                    }

                }//
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân hoặc không có bệnh nhân để xóa tt");
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi xóa thanh toán: " + ex.Message);
            //}
        }
        List<CanBo> _lCanBo = new List<CanBo>();
        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            //_lCanBo = _dataContext.CanBoes.ToList();
            //string makp = "";
            //if (lupBPKe.EditValue != null)
            //    makp = lupBPKe.EditValue.ToString();
            //lupNguoiTT.Properties.DataSource = _lCanBo.Where(p => p.MaKP== (makp)).ToList();
        }
        #region Phiếu thanh toán ra viện mẫu 38
        public static void _phieuTTRV_MS38(QLBV_Database.QLBVEntities _dataContext, int _mabn)
        {
            var kp = (from bns in _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn) join kps in _dataContext.KPhongs on bns.MaKP equals kps.MaKP select new { kps.TenKP, kps.MaKP, bns.IDKB, bns.Buong, bns.Giuong }).OrderByDescending(p => p.IDKB).ToList();
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuTTRV38 rep = new BaoCao.rep_PhieuTTRV38();
            string _DTuong = "", _muc = "";
            if (kp.Count > 0)
            {
                rep.KhoaPhong.Value = kp.First().TenKP;
                rep.Buong.Value = kp.First().Buong;
                rep.Giuong.Value = kp.First().Giuong;
            }

            rep.MaBNhan.Value = _mabn;


            var par = (from bn in _dataContext.BenhNhans
                       where bn.MaBNhan == _mabn
                       select bn).ToList();

            if (par.Count > 0)
            {
                string dtuong = par.FirstOrDefault().DTuong;
                rep.tenBN.Value = par.First().TenBNhan;
                rep.Tuoi.Value = par.First().NamSinh;
                rep.DiaChi.Value = par.First().DChi;
                int gioiTinh = int.Parse(par.First().GTinh.ToString());
                if (gioiTinh == 1)
                {
                    rep.Nu.Value = "/";
                }
                else if (gioiTinh == 0)
                {
                    rep.Nam.Value = "/";
                }

                var vaovien = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
                if (vaovien.Count > 0 && vaovien.First().NgayVao != null)
                    rep.NgayVao.Value = vaovien.First().NgayVao.Value.Hour + " giờ " + vaovien.First().NgayVao.Value.Minute + " ngày " + vaovien.First().NgayVao.Value.Day + " tháng " + vaovien.First().NgayVao.Value.Month + " năm " + vaovien.First().NgayVao.Value.Year;

                var tt = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).Select(p => p.NgayRa).ToList();
                if (tt.Count > 0 && tt.First().Value != null)
                    rep.NgayRa.Value = tt.First().Value.Hour + " giờ " + tt.First().Value.Minute + " ngày " + tt.First().Value.Day + " tháng " + tt.First().Value.Month + " năm " + tt.First().Value.Year;

                if (DungChung.Bien.MaBV == "30281")
                {
                    if (par.First().NoiTru == 1)
                    {
                        rep.txtKeToanVP.Text = "Họ tên: " + DungChung.Bien.KeToanVPnt;
                    }
                    else
                        rep.txtKeToanVP.Text = "Họ tên: " + DungChung.Bien.KeToanVPdv;
                }

                _DTuong = (par.First().DTuong).ToString();
                if (_DTuong.Contains("BHYT"))
                {
                    rep.SoThe.Value = par.First().SThe;
                    rep.coBH.Value = "X";
                    if (par.First().SThe != null && par.First().SThe.ToString() != "" && par.First().SThe.Length > 10)
                        _muc = par.First().SThe.Substring(2, 1);

                    rep.HanBHTu.Value = par.First().HanBHTu;
                    rep.HanBHDen.Value = par.First().HanBHDen;
                    string macs = "";
                    if (par.First().MaCS != null)
                    {
                        macs = par.First().MaCS;
                        rep.MaCS.Value = macs.Substring(0, 2) + "-" + macs.Substring(2, 3);
                    }
                    var csdkbd = _dataContext.BenhViens.Where(p => p.MaBV == macs).ToList();
                    if (csdkbd.Count > 0)
                    {
                        rep.CSDKKCB.Value = csdkbd.First().TenBV;
                    }

                    int dungtuyen = 0;
                    if (par.First().Tuyen != null)
                    {
                        dungtuyen = int.Parse(par.First().Tuyen.ToString());
                    }
                    if (dungtuyen == 1)
                    {
                        rep.DungTuyen.Value = "X";
                        rep.mucHuong.Value = "Mức hưởng: " + DungChung.Ham._PtramTT(_dataContext, _muc) + "%";

                    }
                    else if (dungtuyen == 2)
                    {
                        rep.TraiTuyen.Value = "X";

                        int _HangBV = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                        switch (_HangBV)
                        {
                            case 1:
                                rep.mucHuong.Value = "Mức hưởng: " + "30" + "%";
                                break;
                            case 2:
                                rep.mucHuong.Value = "Mức hưởng: " + "50" + "%";
                                break;
                            case 3:
                                rep.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;
                            case 4:
                                rep.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;

                        }

                    }
                    int capcuu = int.Parse(par.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep.CapCuu.Value = "X";
                        rep.DungTuyen.Value = "";
                        rep.TraiTuyen.Value = "";
                    }
                }
                else
                {
                    rep.mucHuong.Value = "Dành cho BN không có BHYT";
                    rep.koBH.Value = "X";
                    int capcuu = int.Parse(par.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep.CapCuu.Value = "X";
                    }
                    rep.HanBHTu.Value = "";
                }

                rep.ChanDoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD, 0)[1];
                rep.MaICD.Value = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD, 0)[0];
                rep.TongNgay.Value = "1 ngày";
                string _ngaysinh = "";
                if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                    _ngaysinh = par.First().NgaySinh.ToString() + "/";
                if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par.First().ThangSinh.ToString() + "/";
                if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par.First().NamSinh.ToString();
                rep.NSinh.Value = _ngaysinh;
                var ngaytt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).Select(p => p.NgayTT).ToList();
                if (ngaytt.Count > 0)
                {

                    rep.NgayGD.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                    rep.NgayGD.Value = "Ngày ..... tháng ..... năm 20...";
                    rep.NgayTT.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                }

                var bk01 = (from vp1 in _dataContext.DThuocs
                            join vpct in _dataContext.DThuoccts.Where(p => dtuong == "BHYT" ? p.TrongBH == 1 : (p.TrongBH == 1 || p.TrongBH == 0)) on vp1.IDDon equals vpct.IDDon
                            join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                            join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            where vp1.MaBNhan == _mabn
                            group new { nhomdv, dv, vpct } by new { vpct.TrongBH, nhomdv.TenNhom, nhomdv.STT, dv.TenDV, vpct.DonVi, vpct.DonGia, dv.DongY, vpct.TyLeTT } into kq
                            select new { TrongDM = kq.Key.TrongBH, kq.Key.TyLeTT, kq.Key.TenNhom, kq.Key.STT, Dongy = kq.Key.DongY == 1 ? "Thuốc Đông Y" : "Thuốc thường", kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.vpct.SoLuong), ThanhTien = kq.Sum(p => p.vpct.ThanhTien), TienBN = kq.Sum(p => p.vpct.TienBH) }).ToList();
                rep.Tongtien.Value = bk01.Sum(p => p.ThanhTien);
                rep.TienBN.Value = bk01.Sum(p => p.TienBN);
                rep.DataSource = bk01.OrderBy(p => p.TenDV).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        #endregion
        #region Phiếu thanh toán mẫu 40
        public static void _phieuTTRV_MS40(QLBV_Database.QLBVEntities _dataContext, int _mabn)
        {
            var vaovien3 = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            var kp3 = (from bns in _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn) join kps in _dataContext.KPhongs on bns.MaKP equals kps.MaKP select new { kps.TenKP, bns.IDKB }).OrderByDescending(p => p.IDKB).ToList();
            frmIn frm3 = new frmIn();
            BaoCao.rep_PhieuTTRV40 rep3 = new BaoCao.rep_PhieuTTRV40();

            rep3.MaBNhan.Value = _mabn;
            if (vaovien3.Count > 0 && vaovien3.First().SoBA != null)
                rep3.SoBA.Value = vaovien3.First().SoBA;
            if (kp3.Count > 0)
            {
                rep3.KhoaPhong.Value = kp3.First().TenKP;
            }
            var par3 = (from bn in _dataContext.BenhNhans
                        join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                        where bn.MaBNhan == _mabn
                        select new { bn.TenBNhan, bn.Tuoi, kb.Buong, kb.Giuong, kb.BenhKhac, bn.MaBNhan, bn.DChi, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.GTinh, bn.CapCuu, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.SThe, bn.MaCS, bn.NNhap, bn.Tuyen, bn.MaBV, kb.NgayKham, kb.MaICD, ChanDoan = kb.ChanDoan + "-" + kb.BenhKhac, kb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
            if (par3.Count > 0)
            {
                rep3.tenBN.Value = par3.First().TenBNhan;
                rep3.DiaChi.Value = par3.First().DChi;
                int gioiTinh = int.Parse(par3.First().GTinh.ToString());
                if (gioiTinh == 1)
                {
                    rep3.Nam.Value = "/";
                }
                else if (gioiTinh == 0)
                {
                    rep3.Nu.Value = "/";
                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    rep3.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _mabn, DungChung.Bien.formatAge_24012);
                }
                else
                    rep3.Tuoi.Value = par3.First().Tuoi;
                rep3.Buong.Value = par3.First().Buong;
                rep3.Giuong.Value = par3.First().Giuong;
                string _DTuong = "", _muc = "";
                _DTuong = (par3.First().DTuong).ToString();
                if (_DTuong.Contains("BHYT"))
                {
                    rep3.SoThe.Value = par3.First().SThe;
                    rep3.coBH.Value = "X";
                    if (par3.First().SThe != null && par3.First().SThe.ToString() != "" && par3.First().SThe.Length > 10)
                        _muc = par3.First().SThe.Substring(2, 1);

                    rep3.HanBHTu.Value = par3.First().HanBHTu;
                    rep3.HanBHDen.Value = par3.First().HanBHDen;
                    string macs = "";
                    if (par3.First().MaCS != null)
                    {
                        macs = par3.First().MaCS;
                        rep3.MaCS.Value = macs.Substring(0, 2) + "-" + macs.Substring(2, 3);
                    }
                    var csdkbd = _dataContext.BenhViens.Where(p => p.MaBV == macs).ToList();
                    if (csdkbd.Count > 0)
                    {
                        rep3.CSDKKCB.Value = csdkbd.First().TenBV;
                    }
                    rep3.NgayVao.Value = par3.First().NgayKham;
                    var tt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).Select(p => p.NgayTT).ToList();
                    if (tt.Count > 0)
                        rep3.NgayRa.Value = tt.First().Value;

                    int dungtuyen = 0;
                    if (par3.First().Tuyen != null)
                    {
                        dungtuyen = int.Parse(par3.First().Tuyen.ToString());
                    }
                    if (dungtuyen == 1)
                    {
                        rep3.DungTuyen.Value = "X";
                        rep3.mucHuong.Value = "Mức hưởng: " + DungChung.Ham._PtramTT(_dataContext, _muc) + "%";

                    }
                    else if (dungtuyen == 2)
                    {
                        rep3.TraiTuyen.Value = "X";
                        int _HangBV = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                        switch (_HangBV)
                        {
                            case 1:
                                rep3.mucHuong.Value = "Mức hưởng: " + "30" + "%";
                                break;
                            case 2:
                                rep3.mucHuong.Value = "Mức hưởng: " + "50" + "%";
                                break;
                            case 3:
                                rep3.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;
                            case 4:
                                rep3.mucHuong.Value = "Mức hưởng: " + "70" + "%";
                                break;

                        }

                    }
                    int capcuu = int.Parse(par3.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep3.CapCuu.Value = "X";
                        rep3.DungTuyen.Value = "";
                        rep3.TraiTuyen.Value = "";
                    }
                }
                else
                {
                    rep3.mucHuong.Value = "Dành cho BN không có BHYT";
                    rep3.koBH.Value = "X";
                    rep3.NgayVao.Value = par3.First().NgayKham;
                    var tt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).Select(p => p.NgayTT).ToList();
                    if (tt.Count > 0)
                        rep3.NgayRa.Value = tt.First().Value;
                    int capcuu = int.Parse(par3.First().CapCuu.ToString());
                    if (capcuu == 1)
                    {
                        rep3.CapCuu.Value = "X";
                    }
                    rep3.HanBHTu.Value = "";
                }

                rep3.ChanDoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD, 0)[1];
                rep3.MaICD.Value = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD, 0)[0];
                rep3.TongNgay.Value = "1 ngày";
                string _ngaysinh = "";
                if (par3.First().NgaySinh != null && par3.First().NgaySinh.ToString() != "")
                    _ngaysinh = par3.First().NgaySinh.ToString() + "/";
                if (par3.First().ThangSinh != null && par3.First().ThangSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par3.First().ThangSinh.ToString() + "/";
                if (par3.First().NamSinh != null && par3.First().NamSinh.ToString() != "")
                    _ngaysinh = _ngaysinh + par3.First().NamSinh.ToString();
                rep3.NSinh.Value = _ngaysinh;
                var ngaytt = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).Select(p => p.NgayTT).ToList();
                if (ngaytt.Count > 0)
                {

                    //rep.NgayGD.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                    rep3.NgayGD.Value = "Ngày ..... tháng ..... năm 20...";
                    rep3.NgayTT.Value = DungChung.Ham.NgaySangChu(ngaytt.First().Value);
                }

                var bk01 = (from vp1 in _dataContext.DThuocs
                            join vpct in _dataContext.DThuoccts on vp1.IDDon equals vpct.IDDon
                            join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                            join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhomdv in _dataContext.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                            where vp1.MaBNhan == _mabn
                            where (vpct.TrongBH == 0 || vpct.TrongBH == 1)
                            group new { nhomdv, dv, vpct } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, vpct.DonVi, vpct.DonGia } into kq
                            select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.vpct.SoLuong), ThanhTien = kq.Sum(p => p.vpct.ThanhTien), TienBN = kq.Sum(p => p.vpct.TienBN) }).ToList();
                rep3.Tongtien.Value = bk01.Sum(p => p.ThanhTien);
                rep3.TienBN.Value = bk01.Sum(p => p.TienBN);
                rep3.DataSource = bk01.ToList();
                rep3.BindingData();
                rep3.CreateDocument();
                //rep3.DataMember = "Table";
                frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                frm3.ShowDialog();
                //}
            }
        }
        #endregion
        private void cboInBangKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            _kieu = 1;
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            switch (cboInBangKe.SelectedIndex)
            {
                case 0:
                    _maubk = 5;
                    break;
                case 1:
                    _maubk = 4;
                    break;
                case 2:
                    _maubk = 41;
                    break;
                case 3:
                    // phiếu thanh toán ra viện (mẫu 38 -BG)
                    _phieuTTRV_MS38(_dataContext, _mabn);
                    break;
                case 4: //  phiếu thanh toán ra viện (mẫu 40)
                    _phieuTTRV_MS40(_dataContext, _mabn);
                    break;
                case 5:
                    try
                    {
                        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        frmIn frm4 = new frmIn();
                        BaoCao.rep_PhieuKCBNgoaiT rep4 = new BaoCao.rep_PhieuKCBNgoaiT();
                        var ktkd = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                    join
                                        cb in _data.CanBoes on dt.MaCB equals cb.MaCB

                                    select new { dt.PLDV, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, cb.TenCB }).ToList().OrderBy(p => p.PLDV).ToList();
                        if (ktkd.Count > 0)
                        {


                            if (ktkd.First().NgayKe.Value.Day > 0)
                            {

                                rep4.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                            }
                        }
                        rep4._TenBNhan.Value = _tenbn.Trim();

                        //rep._idDon.Value = ktkd.First().IDDon;


                        var tt = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                                  join kb in _data.BNKBs on bn.MaBNhan equals kb.MaBNhan
                                  join cb in _data.CanBoes on kb.MaCB equals cb.MaCB
                                  join kp2 in _data.KPhongs on kb.MaKP equals kp2.MaKP
                                  select new { kb.NgayKham, kp2.TenKP, cb.CapBac, bn.SoTT, bn.GTinh, bn.MaCS, bn.NamSinh, cb.TenCB, bn.HanBHDen, bn.HanBHTu, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi }).OrderByDescending(p => p.IDKB).ToList();
                        if (tt.Count > 0)
                        {
                            rep4.TenCB.Value = tt.First().CapBac + ": " + tt.First().TenCB;
                            rep4.Ngaykham.Value = tt.First().NgayKham.ToString().Substring(0, 10);
                            rep4.TenKP.Value = tt.First().TenKP;
                            rep4.Tuoi.Value = tt.First().NamSinh;


                            switch (tt.First().GTinh)
                            {
                                case 1:
                                    rep4.GTinh.Value = "Nam";
                                    break;
                                case 0:
                                    rep4.GTinh.Value = "Nữ";
                                    break;
                            }
                            if (tt.First().HanBHDen != null && tt.First().HanBHDen.Value.Day > 0)
                                rep4.HanDen.Value = tt.First().HanBHDen.ToString().Substring(0, 10);
                            if (tt.First().HanBHTu != null && tt.First().HanBHTu.Value.Day > 0)
                                rep4.HanTu.Value = tt.First().HanBHTu.ToString().Substring(0, 10);
                            rep4.MaCS.Value = tt.First().MaCS;
                            rep4.ICD.Value = tt.First().MaICD;
                            rep4.SThe.Value = tt.First().SThe;
                            rep4.ChanDoan.Value = tt.First().ChanDoan + tt.First().BenhKhac;

                            rep4.DiaChi.Value = tt.First().DChi;
                            DateTime _ngaynhap = tt.First().NNhap.Value;
                            //var lSoPhieu = _data.BenhNhans.Where(p => p.NNhap == _ngaynhap).ToList();
                            //int maxid = lSoPhieu.Max(p => p.MaBNhan);
                            //int minid
                            rep4.SoPhieu.Value = "Phiếu số: " + tt.First().SoTT;
                            rep4._MaBNhan.Value = _mabn;

                            // lấy mã KCB ban đầu
                            var madkkcb = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                                           join bv in _data.BenhViens on bn.MaCS equals bv.MaBV
                                           select new { bv.TenBV }).ToList();
                            if (madkkcb.Count > 0)
                                rep4.dkkcbbd.Value = madkkcb.First().TenBV;
                        }
                        //int id = ktkd.First().IDDon;
                        if (DungChung.Bien.MaBV == "30009")
                        {
                            var q = (from dv in _data.DichVus
                                     join dtct in _data.DThuoccts.Where(p => p.TrongBH == 1) on dv.MaDV equals dtct.MaDV
                                     join dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn) on dtct.IDDon equals dt.IDDon
                                     join nhomdv in _data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                     //where (dtct.IDDon == id)
                                     group new { dv, dtct, nhomdv } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.DonVi, dtct.DonGia } into kq
                                     select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.dtct.SoLuong), ThanhTien = kq.Sum(p => p.dtct.ThanhTien) }).OrderBy(p => p.STT).OrderBy(p => p.TenDV).ToList();

                            rep4.DataSource = q.ToList();
                        }
                        else
                        {
                            var q = (from dv in _data.DichVus
                                     join dtct in _data.DThuoccts on dv.MaDV equals dtct.MaDV
                                     join dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn) on dtct.IDDon equals dt.IDDon
                                     join nhomdv in _data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                     //where (dtct.IDDon == id)
                                     group new { dv, dtct, nhomdv } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.DonVi, dtct.DonGia } into kq
                                     select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.dtct.SoLuong), ThanhTien = kq.Sum(p => p.dtct.ThanhTien) }).OrderBy(p => p.STT).OrderBy(p => p.TenDV).ToList();

                            rep4.DataSource = q.ToList();
                        }
                        //rep.ShowPreviewDialog();
                        //rep.DataMember = "Table";
                        rep4.BindData();
                        rep4.CreateDocument();
                        frm4.prcIN.PrintingSystem = rep4.PrintingSystem;

                        frm4.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi không in được phiếu:" + ex.Message);
                    }
                    break;
                case 6:// phiếu theo dõi điều trị(mẫu 13)
                    #region  phiếu theo dõi điều trị(mẫu 13)
                    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30010")
                    {
                        this.Hide();
                        FormThamSo.frm_CapNhatKPtongKet frm_13 = new frm_CapNhatKPtongKet(_mabn);
                        frm_13.ShowDialog();
                        ;
                        this.Close();
                        break;
                    }
                    if (DungChung.Bien.MaBV == "04011")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int trongBH = -1;
                            if (i == 0)
                                trongBH = 1;
                            if (i == 1)
                                trongBH = 0;
                            if (i == 2)
                                trongBH = i;
                            int mabn = _mabn;
                            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            BaoCao.rep_PhieuTDDT_04011 rep13 = new BaoCao.rep_PhieuTDDT_04011();
                            frmIn frm13 = new frmIn();
                            var bk01 =
                                (from vp1 in _dataContext.DThuocs
                                 join vpct in _dataContext.DThuoccts.Where(p => p.TrongBH == trongBH) on vp1.IDDon equals vpct.IDDon
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                 where vp1.MaBNhan == (mabn)
                                 group new { nhomdv, dv, vpct } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, vpct.DonVi, vpct.DonGia } into kq
                                 select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.vpct.SoLuong), ThanhTien = kq.Sum(p => p.vpct.ThanhTien), TienBN = kq.Sum(p => p.vpct.TienBN), TienBH = kq.Sum(p => p.vpct.TienBH) }).OrderBy(p => p.STT).ToList();
                            rep13.Tongtien.Value = bk01.Sum(p => p.ThanhTien);
                            rep13.TienBN.Value = bk01.Sum(p => p.TienBN);
                            if (trongBH == 0)
                                rep13.paramTrongNgoaiDM.Value = "(NGOÀI DANH MỤC BHYT)";
                            if (trongBH == 1)
                                rep13.paramTrongNgoaiDM.Value = "(TRONG DANH MỤC BHYT)";
                            if (trongBH == 2)
                                rep13.paramTrongNgoaiDM.Value = "(BỆNH NHÂN KHÔNG PHẢI THANH TOÁN)";
                            rep13.DataSource = null;
                            if (bk01.Count > 0)
                            {
                                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
                                if (bn.Count > 0)
                                {
                                    rep13.NgayV.Value = "Ngày vào: ";
                                    rep13.NgayR.Value = "Ngày ra: ";
                                    rep13.MaBNhan.Value = mabn;
                                    rep13.tenBN.Value = bn.First().TenBNhan.ToUpper();
                                    if (bn.First().NoiTru == 0)
                                    {

                                        rep13.LAB_tieude.Text = "PHIẾU THEO DÕI ĐIỀU TRỊ NGOẠI TRÚ";
                                    }
                                    if (bn.First().GTinh == 0)
                                    {
                                        rep13.Nu.Value = "/".ToUpper();
                                    }
                                    else
                                        rep13.Nam.Value = "/".ToUpper();
                                    rep13.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _mabn, DungChung.Bien.formatAge);
                                    rep13.DiaChi.Value = bn.First().DChi;
                                    string sothe = "";
                                    if (bn.First().SThe != null)
                                        sothe = bn.First().SThe;
                                    if (sothe.Length >= 15)
                                    {
                                        rep13.cell_ST1.Text = sothe.Substring(0, 3);
                                        rep13.cell_ST2.Text = sothe.Substring(3, 2);
                                        rep13.cell_ST3.Text = sothe.Substring(5, 2);
                                        rep13.cell_ST4.Text = sothe.Substring(7, 3);
                                        rep13.cell_ST5.Text = sothe.Substring(10, 5);
                                    }
                                    string macs = bn.First().MaCS;
                                    if (_dataContext.BenhViens.Where(p => p.MaBV == macs).Count() > 0)
                                    {
                                        rep13.CSDKKCB.Value = _dataContext.BenhViens.Where(p => p.MaBV == macs).First().TenBV;
                                    }
                                    rep13.HanBHTu.Value = bn.First().HanBHTu;
                                    rep13.HanBHDen.Value = bn.First().HanBHDen;
                                }
                                var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                                if (vv.Count > 0)
                                {
                                    rep13.NgayVao.Value = vv.First().NgayVao;
                                    rep13.SoBA.Value = vv.First().SoBA;
                                }
                                var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                                if (rv.Count > 0)
                                {
                                    rep13.Status.Value = rv.First().Status.ToString();
                                    rep13.KetQua.Value = rv.First().KetQua;
                                    rep13.NgayRa.Value = rv.First().NgayRa;
                                    rep13.ChanDoan.Value = rv.First().ChanDoan;
                                    rep13.MaICD.Value = rv.First().MaICD;
                                    int _ma = rv.First().MaKP;
                                    var kprv = _dataContext.KPhongs.Where(p => p.MaKP == _ma).ToList();
                                    if (kprv.Count > 0)
                                        rep13.KhoaPhong.Value = kprv.First().TenKP;
                                }
                                rep13.paramTienBH.Value = bk01.First().TienBH;
                                rep13.DataSource = bk01.ToList();
                                rep13.BindingData();
                                rep13.CreateDocument();
                                frm13.prcIN.PrintingSystem = rep13.PrintingSystem;
                                frm13.ShowDialog();

                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int trongBH = -1;
                            if (i == 0)
                                trongBH = 1;
                            if (i == 1)
                                trongBH = 0;
                            if (i == 2)
                                trongBH = i;
                            int mabn = _mabn;
                            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            BaoCao.rep_PhieuTDDT rep13 = new BaoCao.rep_PhieuTDDT();
                            frmIn frm13 = new frmIn();
                            var bk01 =
                                (from vp1 in _dataContext.DThuocs
                                 join vpct in _dataContext.DThuoccts.Where(p => p.TrongBH == trongBH) on vp1.IDDon equals vpct.IDDon
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                 where vp1.MaBNhan == (mabn)
                                 group new { nhomdv, dv, vpct } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, vpct.DonVi, vpct.DonGia } into kq
                                 select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.vpct.SoLuong), ThanhTien = kq.Sum(p => p.vpct.ThanhTien), TienBN = kq.Sum(p => p.vpct.TienBN), TienBH = kq.Sum(p => p.vpct.TienBH) }).OrderBy(p => p.STT).ToList();
                            rep13.Tongtien.Value = bk01.Sum(p => p.ThanhTien);
                            rep13.TienBN.Value = bk01.Sum(p => p.TienBN);
                            if (trongBH == 0)
                                rep13.paramTrongNgoaiDM.Value = "(NGOÀI DANH MỤC BHYT)";
                            if (trongBH == 1)
                                rep13.paramTrongNgoaiDM.Value = "(TRONG DANH MỤC BHYT)";
                            if (trongBH == 2)
                                rep13.paramTrongNgoaiDM.Value = "(BỆNH NHÂN KHÔNG PHẢI THANH TOÁN)";
                            rep13.DataSource = null;
                            if (bk01.Count > 0)
                            {
                                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
                                if (bn.Count > 0)
                                {
                                    rep13.NgayV.Value = "Ngày vào: ";
                                    rep13.NgayR.Value = "Ngày ra: ";
                                    rep13.MaBNhan.Value = mabn;
                                    rep13.tenBN.Value = bn.First().TenBNhan.ToUpper();
                                    if (bn.First().GTinh == 0)
                                    {
                                        rep13.Nu.Value = "/".ToUpper();
                                    }
                                    else
                                        rep13.Nam.Value = "/".ToUpper();
                                    rep13.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _mabn, DungChung.Bien.formatAge);
                                    rep13.DiaChi.Value = bn.First().DChi;
                                    rep13.paramSoThe.Value = bn.First().SThe;
                                    string macs = bn.First().MaCS;
                                    if (_dataContext.BenhViens.Where(p => p.MaBV == macs).Count() > 0)
                                    {
                                        rep13.CSDKKCB.Value = _dataContext.BenhViens.Where(p => p.MaBV == macs).First().TenBV;
                                    }
                                    rep13.HanBHTu.Value = bn.First().HanBHTu;
                                    rep13.HanBHDen.Value = bn.First().HanBHDen;
                                }
                                var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                                if (vv.Count > 0)
                                {
                                    rep13.NgayVao.Value = vv.First().NgayVao;
                                    rep13.SoBA.Value = vv.First().SoBA;
                                }
                                var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                                if (rv.Count > 0)
                                {
                                    rep13.Status.Value = rv.First().Status.ToString();
                                    rep13.KetQua.Value = rv.First().KetQua;
                                    rep13.NgayRa.Value = rv.First().NgayRa;
                                    rep13.ChanDoan.Value = rv.First().ChanDoan;
                                    rep13.MaICD.Value = rv.First().MaICD;
                                    int _ma = rv.First().MaKP;
                                    var kprv = _dataContext.KPhongs.Where(p => p.MaKP == _ma).ToList();
                                    if (kprv.Count > 0)
                                        rep13.KhoaPhong.Value = kprv.First().TenKP;
                                }
                                rep13.paramTienBH.Value = bk01.First().TienBH;
                                rep13.DataSource = bk01.ToList();
                                rep13.BindingData();
                                rep13.CreateDocument();
                                frm13.prcIN.PrintingSystem = rep13.PrintingSystem;
                                frm13.ShowDialog();

                            }
                        }
                    }
                    #endregion
                    break;
                case 7:
                    FormThamSo.Frm_Bieu38 frm6 = new Frm_Bieu38(_mabn);
                    frm6.ShowDialog();
                    break;
                case 8:
                    InBangKePhongKham_01071(_mabn);
                    break;


            }
            if (cboInBangKe.SelectedIndex == 0 || cboInBangKe.SelectedIndex == 1)
            {
                //btnXem_Click(sender, e);
                DungChung.Ham.In_BangKe01_02(_dataContext, _mabn, _maubk, _kieu);

            }
            if (cboInBangKe.SelectedIndex == 9)
            {
                DungChung.Ham.In_BangKe_2018(_dataContext, _mabn);
            }
            _maubk = 0;
            cboInBangKe.SelectedIndex = -1;
        }

        #region Bảng kê chi phí theo khoa phòng
        private class LisChiPhi_01071
        {
            public int STT1 { get; set; }
            public string TrongDM { get; set; }
            public string TenKP { get; set; }
            public int STTN { get; set; }
            public string TenNhom { get; set; }
            public string TenNhomBK02 { get; set; }
            public int STTTN { get; set; }
            public int IDTieuNhom { get; set; }
            public string TenTN { get; set; }
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public double Sluong { get; set; }
            public int TrongBH { get; set; }
            public int ThanhToan { get; set; }
        }

        public void InBangKePhongKham_01071(int _MaBN)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<LisChiPhi_01071> _lKetQua = new List<LisChiPhi_01071>();
            int _hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
            var _lDichVu = (from nhom in _data.NhomDVs
                            join tn in _data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                            join dv in _data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                            select new { tn.TenRG, tn.IdTieuNhom, dv.MaDV, nhom.TenNhomBK02, dv.TenDV, dv.DonVi, nhom.TenNhom, tn.TenTN, nhom.STT, STTTN = tn.STT }).ToList();
            var _lKPhong = (from kp in _data.KPhongs
                            select new { kp.MaKP, kp.TenKP }).ToList();
            var _lDthuocct = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _MaBN)
                              join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                              select new { dtct.MaKP, dtct.MaDV, dtct.TrongBH, dtct.SoLuong, dtct.ThanhToan }).ToList();
            var _list1 = (from dv in _lDichVu
                          join dt in _lDthuocct on dv.MaDV equals dt.MaDV
                          join kp in _lKPhong on dt.MaKP equals kp.MaKP
                          group new { dv, dt, kp } by new { dv.TenNhomBK02, dt.TrongBH, dt.MaKP, kp.TenKP, dv.TenNhom, dv.STT, dv.TenTN, dv.IdTieuNhom, dv.MaDV, dv.TenDV, dv.DonVi, dv.STTTN, dt.ThanhToan } into kq
                          select new
                          {
                              TrongDM = kq.Key.TrongBH == 0 ? "CHI PHÍ NGOÀI DANH MỤC" : "CHI PHÍ TRONG DANH MỤC",
                              STT1 = (kq.Key.TrongBH == 1 || kq.Key.TrongBH == 2) ? 1 : 2,
                              kq.Key.TrongBH,
                              kq.Key.TenKP,
                              kq.Key.MaKP,
                              kq.Key.TenNhom,
                              kq.Key.TenNhomBK02,
                              kq.Key.STT,
                              kq.Key.TenTN,
                              kq.Key.IdTieuNhom,
                              kq.Key.MaDV,
                              kq.Key.TenDV,
                              kq.Key.DonVi,
                              kq.Key.STTTN,
                              kq.Key.ThanhToan,
                              SoLuong = kq.Sum(p => p.dt.SoLuong)
                          }).OrderBy(p => p.STT1).ThenBy(p => p.MaKP).ThenBy(p => p.STT).ThenBy(p => p.STTTN).ToList();
            foreach (var i in _list1)
            {
                LisChiPhi_01071 moi = new LisChiPhi_01071();
                moi.STT1 = i.STT1;
                moi.TrongDM = i.TrongDM;
                moi.TenKP = i.TenKP;
                moi.STTN = Convert.ToInt32(i.STT);
                moi.TenNhom = i.TenNhom;
                moi.TenNhomBK02 = i.TenNhomBK02;
                moi.STTTN = Convert.ToInt32(i.STTTN);
                moi.TenTN = i.TenTN;
                moi.MaDV = i.MaDV;
                moi.TenDV = i.TenDV;
                moi.DonVi = i.DonVi;
                moi.Sluong = i.SoLuong;
                moi.TrongBH = i.TrongBH;
                moi.ThanhToan = i.ThanhToan;
                _lKetQua.Add(moi);
            }
            for (int a = 0; a < 2; a++)
            {
                if (a == 0)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (i == 2)
                            i = 0;
                        if (i == 3)
                            i = 2;
                        var _lkq = _lKetQua.Where(p => p.ThanhToan == 0).Where(p => p.TrongBH == i).Where(p => p.Sluong > 0).ToList();
                        if (_lkq.Count > 0)
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep_BangKe_NgoaiTru_01071 rep = new BaoCao.rep_BangKe_NgoaiTru_01071();
                            var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                            if (_lTTBN != null)
                            {
                                string kvuc = "";
                                if (_lTTBN.KhuVuc != null)
                                    kvuc = _lTTBN.KhuVuc;
                                double _muchuong = 0;
                                int noitru = 0;
                                noitru = _lTTBN.NoiTru ?? 0;
                                if (_lTTBN.MucHuong != null)
                                {
                                    string muc = _lTTBN.MucHuong.ToString();
                                    _muchuong = DungChung.Ham._PtramTT(_data, muc);
                                    //if (_lTTBN.Tuyen == 1)
                                    //{
                                    //    _muchuong = DungChung.Ham._PtramTT(_data, muc);
                                    //}
                                    //else
                                    //{
                                    //    switch (_hangbv)
                                    //    {
                                    //        case 1:

                                    //            _muchuong = DungChung.Ham._PtramTT(_dataContext, muc) * 0.4;
                                    //            break;
                                    //        case 2:
                                    //            if (DungChung.Bien.MaBV == "01830")
                                    //                _muchuong = DungChung.Ham._PtramTT(_dataContext, muc) * 0.7;
                                    //            else
                                    //            {
                                    //                if ((kvuc.ToLower().Contains("k") && noitru == 1) || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                    //                    _muchuong = DungChung.Ham._PtramTT(_dataContext, muc);
                                    //                else
                                    //                    _muchuong = DungChung.Ham._PtramTT(_dataContext, muc) * 0.6;
                                    //            }
                                    //            break;
                                    //        case 3:
                                    //            if (DungChung.Bien.MaBV == "08204")// || _tongtien < DungChung.Bien.GHanTT100)
                                    //            {

                                    //                if (kvuc.ToLower().Contains("k"))
                                    //                    _muchuong = DungChung.Ham._PtramTT(_dataContext, muc);
                                    //                else
                                    //                    _muchuong = 70;
                                    //            }
                                    //            else
                                    //            {
                                    //                if (kvuc.ToLower().Contains("k"))
                                    //                    _muchuong = DungChung.Ham._PtramTT(_dataContext, muc);
                                    //                else
                                    //                    _muchuong = DungChung.Ham._PtramTT(_dataContext, muc) * 0.7;
                                    //            }
                                    //            break;
                                    //        case 4:
                                    //            _muchuong = DungChung.Ham._PtramTT(_dataContext, muc);
                                    //            break;

                                    //    }
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30009" && DungChung.Bien.MaKP == 145)
                                {

                                    rep.TieuDe.Value = i == 0 ? "Ngoài danh mục BH" : (i == 1 ? ("Mức hưởng:" + _muchuong + " %") : "NGUỒN KHÁC");

                                }
                                else
                                {
                                    rep.TieuDe.Value = i == 0 ? "Ngoài danh mục BH" : (i == 1 ? ("Mức hưởng:" + _muchuong + " %") : "Không phải thanh toán");
                                }
                                rep.TenBN.Value = _lTTBN.TenBNhan.ToUpper();
                                rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, _MaBN, DungChung.Bien.formatAge);
                                rep.Gtinh.Value = _lTTBN.GTinh == 1 ? "Nam" : "Nữ";
                                rep.DChi.Value = _lTTBN.DChi.ToString();
                                rep.NgayVao.Value = _lTTBN.NNhap.Value.ToShortDateString();
                                rep.KyBN.Value = _lTTBN.TenBNhan.ToString();
                                rep.Sthe.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.SThe : "";
                                rep.HanTheTu.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.HanBHTu.Value.ToShortDateString() : "";
                                rep.HanTheDen.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.HanBHDen.Value.ToShortDateString() : "";
                                if (_lTTBN.MaCS != null)
                                {
                                    string MaCS = _lTTBN.MaCS;
                                    var DchiCS = _data.BenhViens.Where(p => p.MaBV == MaCS).Select(p => p.TenBV).FirstOrDefault();
                                    if (DchiCS != null)
                                        rep.CSKCB.Value = MaCS + ":" + DchiCS.ToString();
                                    else
                                        rep.CSKCB.Value = MaCS;
                                }
                                rep.CapCuu.Value = _lTTBN.CapCuu == 0 ? "" : "X";
                                if (_lTTBN.Tuyen != null && _lTTBN.CapCuu == 0)
                                {
                                    rep.DungTuyen.Value = _lTTBN.Tuyen == 1 ? "X" : "";
                                    rep.TraiTuyen.Value = _lTTBN.Tuyen == 1 ? "" : "X";
                                }
                                else
                                {
                                    rep.DungTuyen.Value = "";
                                    rep.TraiTuyen.Value = "";
                                }

                            }
                            rep.colsophieu.Text = _MaBN.ToString();
                            rep.Tencq.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.SoPhieu.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            var _lTTRaVien = _data.RaViens.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                            if (_lTTRaVien != null)
                            {
                                if (_lTTRaVien.MaBVC != null)
                                {
                                    string _MaBVC = _lTTRaVien.MaBVC;
                                    var TenBVC = _data.BenhViens.Where(p => p.MaBV == _MaBVC).Select(p => p.TenBV).FirstOrDefault();
                                    rep.NoiChuyen.Value = TenBVC != null ? TenBVC : "";
                                }
                                int _makp = _lTTRaVien.MaKP;
                                var TenKP = _data.KPhongs.Where(p => p.MaKP == _makp).Select(p => p.TenKP).FirstOrDefault();
                                if (TenKP != null)
                                {
                                    rep.TenKP.Value = TenKP.ToUpper().ToString();
                                }
                                rep.NgayRa.Value = _lTTRaVien.NgayRa.Value.ToShortDateString();
                                if (_lTTRaVien.ChanDoan != null)
                                    rep.Chandoan.Value = DungChung.Ham.FreshString(_lTTRaVien.ChanDoan.ToString());
                                if (_lTTRaVien.MaICD != null)
                                    rep.MaICD.Value = DungChung.Ham.FreshString(_lTTRaVien.MaICD.ToString());
                                if (_lTTRaVien.KetQua != null)
                                    rep.KetQua.Value = _lTTRaVien.Status == 1 ? "" : _lTTRaVien.KetQua.ToString();
                            }

                            rep.DataSource = _lkq;
                            rep.Databinding(i);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                        }


                        if (i == 2)
                            break;
                        if (i == 0)
                            i = 2;
                    }
                }
                else
                {
                    var _lkq = _lKetQua.Where(p => p.ThanhToan == 1).Where(p => p.Sluong > 0).ToList();
                    if (_lkq.Count > 0)
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BangKe_NgoaiTru_01071 rep = new BaoCao.rep_BangKe_NgoaiTru_01071();
                        var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                        if (_lTTBN != null)
                        {
                            rep.TieuDe.Value = "Thu trực tiếp";
                            rep.TenBN.Value = _lTTBN.TenBNhan.ToUpper();
                            rep.Tuoi.Value = _lTTBN.Tuoi;
                            rep.Gtinh.Value = _lTTBN.GTinh == 1 ? "Nam" : "Nữ";
                            rep.DChi.Value = _lTTBN.DChi.ToString();
                            rep.NgayVao.Value = _lTTBN.NNhap.Value.ToShortDateString();
                            rep.KyBN.Value = _lTTBN.TenBNhan.ToString();
                            rep.Sthe.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.SThe : "";
                            rep.HanTheTu.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.HanBHTu.Value.ToShortDateString() : "";
                            rep.HanTheDen.Value = _lTTBN.DTuong == "BHYT" ? _lTTBN.HanBHDen.Value.ToShortDateString() : "";
                            if (_lTTBN.MaCS != null)
                            {
                                string MaCS = _lTTBN.MaCS;
                                var DchiCS = _data.BenhViens.Where(p => p.MaBV == MaCS).Select(p => p.TenBV).FirstOrDefault();
                                if (DchiCS != null)
                                    rep.CSKCB.Value = MaCS + ":" + DchiCS.ToString();
                                else
                                    rep.CSKCB.Value = MaCS;
                            }
                            rep.CapCuu.Value = _lTTBN.CapCuu == 0 ? "" : "X";
                            if (_lTTBN.Tuyen != null && _lTTBN.CapCuu == 0)
                            {
                                rep.DungTuyen.Value = _lTTBN.Tuyen == 1 ? "X" : "";
                                rep.TraiTuyen.Value = _lTTBN.Tuyen == 1 ? "" : "X";
                            }
                            else
                            {
                                rep.DungTuyen.Value = "";
                                rep.TraiTuyen.Value = "";
                            }

                        }
                        rep.colsophieu.Text = _MaBN.ToString();
                        rep.Tencq.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.SoPhieu.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        var _lTTRaVien = _data.RaViens.Where(p => p.MaBNhan == _MaBN).FirstOrDefault();
                        if (_lTTRaVien != null)
                        {
                            if (_lTTRaVien.MaBVC != null)
                            {
                                string _MaBVC = _lTTRaVien.MaBVC;
                                var TenBVC = _data.BenhViens.Where(p => p.MaBV == _MaBVC).Select(p => p.TenBV).FirstOrDefault();
                                rep.NoiChuyen.Value = TenBVC != null ? TenBVC : "";
                            }
                            int _makp = _lTTRaVien.MaKP;
                            var TenKP = _data.KPhongs.Where(p => p.MaKP == _makp).Select(p => p.TenKP).FirstOrDefault();
                            if (TenKP != null)
                            {
                                rep.TenKP.Value = TenKP.ToUpper().ToString();
                            }
                            rep.NgayRa.Value = _lTTRaVien.NgayRa.Value.ToShortDateString();
                            if (_lTTRaVien.ChanDoan != null)
                                rep.Chandoan.Value = DungChung.Ham.FreshString(_lTTRaVien.ChanDoan.ToString());
                            if (_lTTRaVien.MaICD != null)
                                rep.MaICD.Value = DungChung.Ham.FreshString(_lTTRaVien.MaICD.ToString());
                            if (_lTTRaVien.KetQua != null)
                                rep.KetQua.Value = _lTTRaVien.Status == 1 ? "" : _lTTRaVien.KetQua.ToString();
                        }

                        rep.DataSource = _lkq;
                        rep.Databinding();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();

                    }
                }
            }
        }

        #endregion
        private void grvThanhToan_Click(object sender, EventArgs e)
        {

        }

        private void grvThanhToan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (e.Column.Name == "colSua")// && DungChung.Bien.MaBV != "01071")
            {
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    panelControl1.Enabled = true;
                    int _trongBH = -1;
                    if (grvThanhToan.GetFocusedRowCellValue(colTrongBH) != null)
                        _trongBH = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTrongBH));
                    if (_trongBH == 1)
                    {
                        rdbTrongDM.Checked = true;
                        rdbNgoaiDM.Checked = false;
                        rdbKhongTT.Checked = false;
                    }
                    else if (_trongBH == 2)
                    {
                        rdbTrongDM.Checked = false;
                        rdbNgoaiDM.Checked = false;
                        rdbKhongTT.Checked = true;
                    }
                    else if (_trongBH == 0)
                    {
                        rdbTrongDM.Checked = false;
                        rdbNgoaiDM.Checked = true;
                        rdbKhongTT.Checked = false;
                    }
                }
                else
                {
                    int _mabn = 0, _madv = 0, _makp = 0;
                    int _trongBH = -1;
                    _mabn = this._mabn;
                    if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                        _madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
                    if (grvThanhToan.GetFocusedRowCellValue(colTrongBH) != null)
                        _trongBH = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTrongBH));
                    int _mien = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMien));
                    var ktratt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                  join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon
                                  select dtct).FirstOrDefault();
                    if (grvThanhToan.GetFocusedRowCellValue(colMaKP) != null)
                        _makp = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaKP));
                    if (ktratt != null)
                    {
                        ChucNang.frm_UpdateTrongDM frm = new ChucNang.frm_UpdateTrongDM(_mabn, _madv, _trongBH, _mien, _makp);
                        frm.ShowDialog();
                        frm_XemChiPhi_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Dịch vụ đã thu trực tiếp, không thể sửa");
                    }
                }
            }
        }

        private void grvTamUng_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //List<TamUngct> taungct = new List<TamUngct>();
            if (grvTamUng.GetFocusedRowCellValue("IDTamUng") != null)
            {
                int idtamung = Convert.ToInt32(grvTamUng.GetFocusedRowCellValue("IDTamUng"));
                var _ltaungct = (from tu in _dataContext.TamUngcts.Where(p => p.IDTamUng == idtamung).Where(p => p.Status == 0)
                                 join dv in _dataContext.DichVus on tu.MaDV equals dv.MaDV
                                 select new { tu.IDTamUngct, tu.MaDV, tu.SoLuong, tu.DonGia, tu.ThanhTien, tu.SoTien, dv.TenDV, tu.TrongBH, dv.DonVi }).OrderBy(p => p.IDTamUngct).ToList();
                grcTamUngct.DataSource = _ltaungct;
            }

        }

        private void grvTamUng_DataSourceChanged(object sender, EventArgs e)
        {
            grvTamUng_FocusedRowChanged(null, null);
        }

        private void panelBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grcThanhToan_Click(object sender, EventArgs e)
        {

        }

        private void btnXemChiPhi_Click(object sender, EventArgs e)
        {
            {
                _soTien = "";
                _noiDung = "";
                _tinhTamUng = false;
                _lchidinh = new List<frm_CPChiDinh.lCPCHiDinh>();
                _ploaiTamThu = 3;
                _lmack = new List<int>();
                _IdGoiDV = 0;
                _maKP = 0;
            }

            frm_CPChiDinh frm = new frm_CPChiDinh(_mabn);
            frm.GetData = new frm_CPChiDinh._getstring(_getValue);
            frm.ShowDialog();

        }

        string _soTien;
        string _noiDung;
        bool _tinhTamUng;
        List<FormNhap.frm_CPChiDinh.lCPCHiDinh> _lchidinh;
        int _ploaiTamThu = 3;
        List<int> _lmack;
        int _IdGoiDV;
        int _maKP;

        public void _getValue(string gt1, string gt2, string gt3, bool tinhtamung, List<FormNhap.frm_CPChiDinh.lCPCHiDinh> lchidinh, int plthu, List<int> lmack, int IdGoiDV, bool _thuBangThe)
        {
            _IdGoiDV = IdGoiDV;
            _soTien = gt1;
            _noiDung = gt2;
            _lchidinh = lchidinh;
            _tinhTamUng = tinhtamung;
            _ploaiTamThu = plthu;
            if (lchidinh.Count > 0)
                _maKP = lchidinh.First().MaKP;
            _lmack = lmack;
            btnLuutthu_Click(null, null);
        }

        public bool _ktQuyen_SoHD(QLBV_Database.QLBVEntities _data, int ploai, string _quyen, string _so)
        {
            if (string.IsNullOrEmpty(_so))
                return false;
            var kt = _data.TamUngs.Where(p => p.PhanLoai == ploai && p.QuyenHD == _quyen && p.SoHD == _so).ToList();
            if (kt.Count > 0)
                return true;
            return false;
        }

        private void btnLuutthu_Click(object sender, EventArgs e)
        {
            bool _tthai = false;
            int ploai = _ploaiTamThu == 3 ? 1 : _ploaiTamThu;
            string quyen = "";
            string soHDTU = "";
            var quyenSoBL = QLBV.FormNhap.usTamThu_TToan.QuyenSoBL._getQuyen_SoBL(ploai, "");
            if (quyenSoBL != null)
            {
                quyen = quyenSoBL.FirstOrDefault().Quyen;
                soHDTU = (quyenSoBL.FirstOrDefault().So + 1).ToString();

            }
            else
                return;

            {
                if (_tthai == false)
                {
                    if (_ktQuyen_SoHD(_dataContext, ploai, quyen, soHDTU))
                    {
                        DialogResult _result2 = MessageBox.Show("Số hóa đơn đã được sử dụng, bạn vẫn muốn lưu?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result2 == DialogResult.No)
                            return;
                    }

                    TamUng _tamung = new TamUng();
                    bool _ngoaih = false;
                    _ngoaih = DungChung.Ham.CheckNGioHC(DateTime.Now);

                    if (_ngoaih == true)
                    {
                        MessageBox.Show("Thu-Chi ngoài giờ HC");
                        _tamung.NgoaiGio = 1;
                    }
                    else
                    {
                        _tamung.NgoaiGio = 0;
                    }
                    _tamung.IDGoiDV = _IdGoiDV;
                    _tamung.Status = true;
                    _tamung.NgayThu = DateTime.Now;
                    _tamung.MaKP = _maKP;//0k
                    _tamung.MaCB = DungChung.Bien.MaCB;
                    _tamung.MaBNhan = _mabn;
                    if (_lchidinh.Count() > 0)
                    {
                        _tamung.TongTien = _lchidinh.Sum(p => p.DonGia);
                        _tamung.Mien = _lchidinh.First().Mien;
                    }
                    _tamung.PhanLoai = _ploaiTamThu;
                    if (_ploaiTamThu == 3)
                    {
                        _tamung.TienChenh = double.Parse(_soTien);
                    }

                    _tamung.SoTien = double.Parse(_soTien);

                    _tamung.LyDo = _noiDung;
                    _tamung.QuyenHD = quyen;
                    _tamung.SoHD = soHDTU;
                    _dataContext.TamUngs.Add(_tamung);
                    _dataContext.SaveChanges();

                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var id1 = _tamung.IDTamUng;

                    if (id1 > 0)
                    {
                        try
                        {
                            SoBienLai soBL = _dataContext.SoBienLais.Where(p => p.PLoai == ploai && p.Quyen == quyen).FirstOrDefault();//var soBL = _dataContext.SoBienLais.Where(p => p.PLoai == 0 && p.Quyen == cbo_quyenTU.Text).ToList(); sai
                            int soht = soBL.SoHT + 1;
                            if (soht == soBL.SoDen)
                            {
                                soBL.SoHT = soht;
                                soBL.Status = 2;
                                _dataContext.SaveChanges();
                                MessageBox.Show("quyển hóa đơn: " + quyen + "đã sử dụng hết, \nthiết lập thêm quyển khác để sử dụng cho hóa đơn sau");
                            }
                            else
                            {
                                soBL.SoHT = soht;
                                _dataContext.SaveChanges();
                            }
                            //foreach (var item in soBL)
                            //{
                            //    int soht = item.SoHT + 1;
                            //    if (soht==item.SoDen)
                            //    {

                            //    }
                            //    item.SoHT = item.SoHT + 1;
                            //    _dataContext.SaveChanges();
                            //}

                        }
                        catch { }

                        try
                        {
                            if (_IdGoiDV <= 0)//Thu trực tiếp đối tượng khám sức khỏe thì ko cần thêm vào tamungct và dthuoc
                            {
                                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                List<QLBV.FormNhap.frm_CPChiDinh.lCPCHiDinh> _lChuaCoDT = new List<frm_CPChiDinh.lCPCHiDinh>();// những dịch vụ thu thẳng mà chưa có trong đơn thuốc
                                foreach (var a in _lchidinh)
                                {
                                    if (a.Chon)
                                    {

                                        // lưu tạm ứng ct
                                        try
                                        {
                                            TamUngct moi = new TamUngct();
                                            moi.IDTamUng = id1;
                                            moi.MaKP = a.MaKP;
                                            moi.MaKPth = a.MaKPth;
                                            moi.MaDV = a.MaDV;
                                            moi.SoLuong = a.SoLuong;
                                            moi.DonGia = a.DonGia;
                                            moi.ThanhTien = a.ThanhTien;
                                            moi.SoTien = a.TienBN;
                                            moi.TrongBH = a.TrongBH;
                                            moi.TienBN = a.TienBN;
                                            moi.Mien = a.Mien;
                                            moi.IDDonct = a.IDDonct;
                                            _dataContext.TamUngcts.Add(moi);
                                            _dataContext.SaveChanges();

                                        }
                                        catch
                                        {

                                        }
                                        //
                                        if (_lmack.Contains(a.MaDV))//  if (a.MaDV == _maCKham)
                                        {
                                            var _update = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                                           join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == a.MaDV) on dt.IDDon equals dtct.IDDon
                                                           select dtct).ToList();
                                            if (_update.Count > 0)
                                            {
                                                foreach (var b in _update)
                                                {
                                                    b.IDCD = id1;
                                                    b.ThanhToan = 1;
                                                    _dataContext.SaveChanges();
                                                }
                                            }
                                            else
                                            {
                                                _lChuaCoDT.Add(a);

                                            }
                                        }

                                        var sophieu = _dataContext.ChiDinhs.Where(p => p.IDCD == a.IDCD).FirstOrDefault();
                                        if (sophieu != null)
                                            sophieu.SoPhieu = id1;
                                        var dthuoctDVThuThang = _dataContext.DThuoccts.Where(p => p.IDDonct == a.IDDonct).FirstOrDefault();
                                        if (dthuoctDVThuThang != null)
                                            dthuoctDVThuThang.ThanhToan = 1;
                                        var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == a.IDCD).FirstOrDefault();
                                        if (dthuoct != null)
                                            dthuoct.ThanhToan = 1;

                                        _dataContext.SaveChanges();
                                    }

                                }


                                #region add thêm đơn thuốc
                                var CheckCK = (from ck in _lmack
                                               join a1 in _lChuaCoDT on ck equals a1.MaDV
                                               select a1).ToList();
                                if (CheckCK.Count > 0) // đức 27-09
                                {
                                    if (_lChuaCoDT.Count > 0)
                                    {
                                        int makp = 0;
                                        DThuoc dthuoccd = new DThuoc();
                                        var qbnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                                        var bnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                        if (qbnkb.Count > 0)
                                        {
                                            makp = qbnkb.FirstOrDefault().MaKP ?? 0;
                                            dthuoccd.NgayKe = qbnkb.First().NgayKham;
                                        }
                                        else
                                        {

                                            if (bnhan != null)
                                                makp = bnhan.MaKP ?? 0;
                                            dthuoccd.NgayKe = bnhan.NNhap;
                                        }
                                        dthuoccd.MaBNhan = _mabn;
                                        dthuoccd.KieuDon = -1;
                                        dthuoccd.PLDV = 2;
                                        if (qbnkb.Count > 0 && qbnkb.First().MaCB != null)
                                            dthuoccd.MaCB = qbnkb.First().MaCB;

                                        _dataContext.DThuocs.Add(dthuoccd);
                                        _dataContext.SaveChanges();

                                        foreach (var b in _lChuaCoDT)
                                        {
                                            if (_lmack.Contains(b.MaDV))//chỉ thêm vào DThuocct khi dịch vụ là công khám 
                                            {
                                                DThuocct dtct = new DThuocct();
                                                dtct.MaKP = makp;
                                                dtct.IDDon = dthuoccd.IDDon;
                                                if (qbnkb.Count > 0)
                                                    dtct.IDKB = qbnkb.First().IDKB;
                                                dtct.MaDV = b.MaDV;
                                                dtct.SoLuong = 1;
                                                if (qbnkb.Count > 0 && qbnkb.First().NgayKham != null)
                                                    dtct.NgayNhap = qbnkb.First().NgayKham;
                                                else
                                                    dtct.NgayNhap = bnhan.NNhap;
                                                dtct.DonVi = b.DonVi;
                                                dtct.DonGia = b.DonGia;
                                                dtct.ThanhTien = b.DonGia;
                                                dtct.TrongBH = 0; //item.TrongDM == null ? 0 : item.TrongDM.Value;
                                                dtct.ThanhToan = 1;
                                                dtct.TyLeTT = 100;
                                                dtct.IDCD = id1;
                                                _dataContext.DThuoccts.Add(dtct);
                                                _dataContext.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        catch (Exception)
                        {

                        }


                    }
                    var _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDTamUng).ToList();
                    grcTamUng.DataSource = _litamung;

                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể thu tiền tạm ứng");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //    var vienPhi = dataContext.VienPhis.Where(o => o.MaBNhan == _mabn).ToList();
            //    if (vienPhi.Count > 0)
            //    {
            //        foreach (var item in vienPhi)
            //        {
            //            var vp = dataContext.VienPhis.FirstOrDefault(o => o.idVPhi == item.idVPhi);
            //            if (vp != null)
            //            {
            //                ConnectBanking.Agribank.ParamResult paramResult = null;
            //                if (DungChung.Ham.HuyThanhToanPOS_Agribank(vp.MaBNhan, vp.ReceiptNo, vp.ClientId, vp.RequestId, ref paramResult))
            //                {
            //                    vp.LoaiThanhToan = null;
            //                    vp.ClientId = null;
            //                    vp.RequestId = null;
            //                    vp.ReceiptNo = null;
            //                    dataContext.SaveChanges();
            //                    MessageBox.Show("Hủy thành công!");
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Không hủy được!");
            //                }
            //            }
            //        }
            //        frm_XemChiPhi_Load(null, null);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Bệnh nhân chưa thanh toán");
            //    }
            //}
        }

        public int CheckNoiNgoaiTru(int mabn)
        {
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.NoiTru).FirstOrDefault();
            if (bn != null)
                return bn.Value;
            return -1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            int _trongBH = 0;
            int _tyleTT = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTyLeTT));
            int _mien = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMien));
            if (grvThanhToan.GetFocusedRowCellValue(colTrongBH) != null)
                _trongBH = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTrongBH));
            if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                _madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
            if (grvThanhToan.GetFocusedRowCellValue(colMaKP) != null)
                _makp = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaKP));
            if (!DungChung.Ham.KTraTT(_dataContext, _mabn))
            {
                var dv = _dataContext.DichVus.ToList();
                List<DThuocct> thaydoi = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                          join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.MaKP == _makp).Where(p => p.ThanhToan != 1) on dt.IDDon equals dtct.IDDon
                                          select dtct).ToList();
                foreach (var a in thaydoi)
                {
                    double dongia = 0;
                    
                    DateTime ngaynhap = DateTime.Now;
                    if (a.NgayNhap != null)
                        ngaynhap = a.NgayNhap.Value;
                    int trongdm = _trongBH;
                    dongia = DungChung.Ham._getGiaDM(_dataContext, a.MaDV ?? 0, trongdm, _mabn, ngaynhap);
                    if (rdbNgoaiDM.Checked)
                    {
                        trongdm = 0;
                    }
                    else if (rdbKhongTT.Checked)
                    {
                        trongdm = 2;
                    }
                    else if (rdbTrongDM.Checked)
                    {
                        trongdm = 1;
                    }
                    a.TrongBH = trongdm;
                    a.Mien = Convert.ToInt32(textEdit1.Text);
                    //a.XHH = ckXHH.Checked ? 1 : 0;
                    a.TyLeTT = _tyleTT == 0 ? 100 : _tyleTT;
                    var ktdv = dv.Where(p => p.MaDV == a.MaDV).Where(p => p.PLoai == 2).ToList();
                    if (ktdv.Count > 0)
                        a.DonGia = dongia;
                    a.ThanhTien = Math.Round(a.DonGia * a.SoLuong * _tyleTT * (double)(100 - a.Mien) / 10000, 3);
                    _dataContext.SaveChanges();
                }
                MessageBox.Show("Sửa thành công");
                frm_XemChiPhi_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể sửa");
            }
        }
    }
}