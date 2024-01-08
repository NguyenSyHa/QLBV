using QLBV.FormThamSo;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_ChiDinhBNLao : Form
    {
        public frm_ChiDinhBNLao()
        {
            InitializeComponent();
        }

        int _idCLS = 0;
        string _makp = "";
        int _mabn = 0;
        public frm_ChiDinhBNLao(int idCLS, string makp)
        {
            InitializeComponent();
            _idCLS = idCLS;
            _makp = makp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCLS"></param>
        /// <param name="makp"></param>
        /// <param name="ploaiXN">0: XN lao và XN khác; 1: xn phẫu thuật sinh thiết (mô bệnh học); 2: XN tế bào học</param>
        public frm_ChiDinhBNLao(int idCLS, string makp, int maBN, int ploaiXN)
        {
            InitializeComponent();
            _idCLS = idCLS;
            _makp = makp;
            _ploaiXN = ploaiXN;
            _mabn = maBN;
        }
        public delegate void PassData(string maCBTH, DateTime? ngayTH);
        public PassData passData;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// //0: XN lao, xn chung; 1: xn giải phẫu sinh thiết (mô bệnh học)
        /// </summary>
        int _ploaiXN = 0;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_ChiDinhBNLao_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xtraTabPage1.PageVisible = false;
                xtraTabPage2.PageVisible = false;
                xtraTabPage3.PageVisible = false;
            }
            else
                xtraTabPage4.PageVisible = false;
            lupNgayNhanMau.DateTime = DateTime.Now;
            lupNgayNhanMau1.DateTime = DateTime.Now;
            lupNgayLayMau1.DateTime = DateTime.Now;
            dtTraKQ1.DateTime = DungChung.Ham.NgayTu(DateTime.Now);
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in data.CanBoes.Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                       select cb).ToList();
            _lcanbo.Add(new CanBo { MaCB = "", TenCB = "" });
            //if (DungChung.Bien.CapDo == 8 || DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                _lcanbo = _lcanbo.OrderBy(p => p.TenCB).ToList();
            else
                _lcanbo = _lcanbo.Where(p => p.MaKPsd != null && p.MaKPsd.Contains(_makp)).OrderBy(p => p.TenCB).ToList();

            lupMaCB.Properties.DataSource = _lcanbo;
            lupMaCB.EditValue = DungChung.Bien.MaCB;
            lupMaCB1.Properties.DataSource = _lcanbo;
            lupMaCB1.EditValue = DungChung.Bien.MaCB;
            LupCanBo.Properties.DataSource = _lcanbo;
            LupCanBo1.Properties.DataSource = _lcanbo;


            lupNguoiPhaBP.Properties.DataSource = _lcanbo;
            lupNguoiLamTieuBan.Properties.DataSource = _lcanbo;
            lupNguoiDocKQ.Properties.DataSource = _lcanbo;

            lupNguoiPhaBP.EditValue = DungChung.Bien.MaCB;

            lupMaCB.EditValue = DungChung.Bien.MaCB;
            lupMaCB1.EditValue = DungChung.Bien.MaCB;

            CL cls = data.CLS.Where(p => p.IdCLS == _idCLS).FirstOrDefault();
            if (cls != null)
            {
                if (cls.ThoiGianLayMau != null)
                    lupThoiGianLayMau.DateTime = Convert.ToDateTime(cls.ThoiGianLayMau);
                if (cls.ThoiGianNhanMau != null)
                    lupThoiGianNhanMau.DateTime = Convert.ToDateTime(cls.ThoiGianNhanMau);
                if (cls.MaCBLayMau != null)
                    LupCanBo.EditValue = cls.MaCBLayMau;
                if (cls.MaCBNhanMau != null)
                    LupCanBo1.EditValue = cls.MaCBNhanMau;

                ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idCLS).FirstOrDefault();
                if (cd != null)
                {
                    //var qtn = (from tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc) join dv in data.DichVus.Where(p => p.MaDV == cd.MaDV.Value) on tn.IdTieuNhom equals dv.IdTieuNhom select tn).FirstOrDefault();
                    //if (qtn != null)
                    //{
                    //    _ploaiXN = 1;

                    //}
                }
                if (_ploaiXN == 1)//xn mô bệnh học
                {
                    tab.SelectedTabPageIndex = 1;
                    if (cls.CapCuu == true)
                        rdCapCuu.SelectedIndex = 1;
                    else
                        rdCapCuu.SelectedIndex = 0;
                    txtViTriSinhThiet.Text = cls.BenhPham;
                    if (cls.ThoiGianLayMau != null)
                        dtThoiGianCoDinhMau.DateTime = cls.ThoiGianLayMau.Value;

                    if (cls.ThoiGianNhanMau != null)
                        dtThoiGianGuiMau.DateTime = cls.ThoiGianNhanMau.Value;//ngày gửi kết quả


                    txtTrangThaiBPSinhThiet.Text = cls.TrangThaiBP;
                    if (cls.NgayKQ != null)
                        dtNgayTra.DateTime = cls.NgayKQ.Value;//ngày trả kết quả                    

                    if (cls.NgayTH != null)
                        dtNgayTH.DateTime = cls.NgayTH.Value;
                    mmChanDoanLS.Text = cls.ChanDoan;
                    lupNguoiDocKQ.EditValue = cls.MaCBth;

                    if (cls.Status == 1)
                        btnThucHien.Enabled = false;
                    else
                        btnThucHien.Enabled = true;

                    mmQtrinhDtri.Text = cd.KetLuan;// quá trình điều trị                                       
                    txtSoManh.Text = cd.Mau_Lan_MTruongXN;
                    var qttxn = data.ThongTinXetNghiems.Where(p => p.IDCD == cd.IDCD).FirstOrDefault();
                    if (qttxn != null)
                    {
                        txtDungDichCoDinhMau.Text = qttxn.DDCoDinh;//dung dịch cố định bệnh phẩm
                        mmTomTatDauHieuLS.Text = qttxn.DauHieuLSVaXNKhac;//tóm tắt dấu hiệu lâm sàng chính và các xét nghiệm khác  
                        mmKQSinhThietTruoc.Text = qttxn.KQXNLanTruoc;
                        if (qttxn.ThoiGianLamTieuBan != null)
                            dtThoiGianPhaBP.DateTime = qttxn.ThoiGianLamTieuBan.Value;
                        txtPhuongPhapNhuom.Text = qttxn.PhuongPhap;
                        lupNguoiPhaBP.EditValue = qttxn.CBPhaBenhPham;
                        lupNguoiLamTieuBan.EditValue = qttxn.CBLamTieuBan;
                        mmNhanXetDaiThe.Text = qttxn.NXDaiThe;
                        mmNhanXetViThe.Text = qttxn.NXViThe;
                        mmChanDoanGiaiPhau.Text = qttxn.ChanDoanGiaiPhau;
                        mmSuPhuHop.Text = qttxn.PhuHopChanDoan;
                    }
                }
                else if (_ploaiXN == 2)
                {
                    tab.SelectedTabPageIndex = 2;
                    lupMaCB1.EditValue = cls.MaCBLayMau;
                    txtBenhPham1.Text = cls.BenhPham;
                    txttrangThaiBP1.Text = cls.TrangThaiBP;
                    if (cls.ThoiGianLayMau != null)
                    {
                        lupNgayLayMau1.DateTime = cls.ThoiGianLayMau.Value;
                    }
                    else
                    {
                        lupNgayLayMau1.DateTime = cls.NgayThang.Value;
                    }
                    if (cls.ThoiGianNhanMau != null)
                    {
                        lupNgayNhanMau1.DateTime = cls.ThoiGianNhanMau.Value;
                        if (cls.NgayKQ != null)
                            dtTraKQ1.DateTime = cls.NgayKQ.Value;
                        if (cls.TrangThaiBN != null)
                        {
                            txtTrangThaiBN1.Text = cls.TrangThaiBN;
                        }

                    }
                    txtGhiChu1.Text = cls.GhiChu;
                }
                else
                {
                    tab.SelectedTabPageIndex = 0;
                    lupMaCB.EditValue = cls.MaCBLayMau;
                    txtBenhPham.Text = cls.BenhPham;
                    txttrangThaiBP.Text = cls.TrangThaiBP;
                    if (cls.ThoiGianLayMau != null)
                    {
                        lupNgayLayMau.DateTime = cls.ThoiGianLayMau.Value;
                    }
                    else
                    {
                        lupNgayLayMau.DateTime = cls.NgayThang.Value;
                    }
                    if (cls.ThoiGianNhanMau != null)
                    {
                        lupNgayNhanMau.DateTime = cls.ThoiGianNhanMau.Value;
                        if (cls.NgayKQ != null)
                            dtTraKQ.DateTime = cls.NgayKQ.Value;
                        if (cls.TrangThaiBN != null)
                        {
                            txtTrangThaiBN.Text = cls.TrangThaiBN;
                        }

                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (checkValidate())
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                CL cls = data.CLS.Where(p => p.IdCLS == _idCLS).FirstOrDefault();
                if (cls != null)
                {
                    #region XN giai phau sinh thiet
                    if (_ploaiXN == 1)
                    {

                        cls.CapCuu = rdCapCuu.SelectedIndex == 1 ? true : false;
                        cls.BenhPham = txtViTriSinhThiet.Text;
                        if (!string.IsNullOrEmpty(dtThoiGianCoDinhMau.Text))
                            cls.ThoiGianLayMau = dtThoiGianCoDinhMau.DateTime;
                        else
                            cls.ThoiGianLayMau = null;
                        cls.TrangThaiBP = txtTrangThaiBPSinhThiet.Text;
                        if (!string.IsNullOrEmpty(dtNgayTra.Text))
                            cls.NgayKQ = dtNgayTra.DateTime;//ngày trả kết quả
                        else
                            cls.NgayKQ = null;
                        if (!string.IsNullOrEmpty(dtThoiGianGuiMau.Text))
                            cls.ThoiGianNhanMau = dtThoiGianGuiMau.DateTime;//ngày gửi kết quả
                        else
                            cls.ThoiGianNhanMau = null;

                        DateTime? ngayTH = null;
                        string maCB = "";
                        if (!string.IsNullOrEmpty(dtNgayTH.Text))
                            ngayTH = dtNgayTH.DateTime;
                        if (lupNguoiDocKQ.EditValue != null && lupNguoiDocKQ.Text != "")
                            maCB = lupNguoiDocKQ.EditValue.ToString();
                        cls.ChanDoan = mmChanDoanLS.Text;
                        ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idCLS).FirstOrDefault();
                        if (cd != null)
                        {
                            cd.KetLuan = mmQtrinhDtri.Text;// quá trình điều trị                                       
                            cd.Mau_Lan_MTruongXN = txtSoManh.Text;
                            //ThongTinXetNghiem
                            var qttxn = data.ThongTinXetNghiems.Where(p => p.IDCD == cd.IDCD).FirstOrDefault();
                            if (qttxn == null)//thêm mới
                            {
                                ThongTinXetNghiem moi = new ThongTinXetNghiem();
                                moi.IDCD = cd.IDCD;
                                moi.DDCoDinh = txtDungDichCoDinhMau.Text;//dung dịch cố định bệnh phẩm
                                moi.DauHieuLSVaXNKhac = mmTomTatDauHieuLS.Text;//tóm tắt dấu hiệu lâm sàng chính và các xét nghiệm khác  
                                moi.KQXNLanTruoc = mmKQSinhThietTruoc.Text;
                                if (dtThoiGianPhaBP.Text != null && dtThoiGianPhaBP.Text != "")
                                    moi.ThoiGianLamTieuBan = dtThoiGianPhaBP.DateTime;
                                else
                                    moi.ThoiGianLamTieuBan = null;
                                moi.PhuongPhap = txtPhuongPhapNhuom.Text;
                                if (lupNguoiPhaBP.EditValue != null && lupNguoiPhaBP.Text != "")
                                    moi.CBPhaBenhPham = lupNguoiPhaBP.EditValue.ToString();
                                if (lupNguoiLamTieuBan.EditValue != null && lupNguoiLamTieuBan.Text != "")
                                    moi.CBLamTieuBan = lupNguoiLamTieuBan.EditValue.ToString();
                                moi.NXDaiThe = mmNhanXetDaiThe.Text;
                                moi.NXViThe = mmNhanXetViThe.Text;
                                moi.ChanDoanGiaiPhau = mmChanDoanGiaiPhau.Text;
                                moi.PhuHopChanDoan = mmSuPhuHop.Text;

                                data.ThongTinXetNghiems.Add(moi);
                            }
                            else// sửa
                            {
                                qttxn.PhuongPhap = txtPhuongPhapNhuom.Text;
                                qttxn.DauHieuLSVaXNKhac = mmTomTatDauHieuLS.Text;//tóm tắt dấu hiệu lâm sàng chính và các xét nghiệm khác     
                                qttxn.KQXNLanTruoc = mmKQSinhThietTruoc.Text;
                                if (lupNguoiPhaBP.EditValue != null && lupNguoiPhaBP.Text != "")
                                    qttxn.CBPhaBenhPham = lupNguoiPhaBP.EditValue.ToString();
                                qttxn.DDCoDinh = txtDungDichCoDinhMau.Text;
                                if (dtThoiGianPhaBP.Text != null && dtThoiGianPhaBP.Text != "")
                                    qttxn.ThoiGianLamTieuBan = dtThoiGianPhaBP.DateTime;
                                else
                                    qttxn.ThoiGianLamTieuBan = null;

                                if (lupNguoiLamTieuBan.EditValue != null && lupNguoiLamTieuBan.Text != "")
                                    qttxn.CBLamTieuBan = lupNguoiLamTieuBan.EditValue.ToString();
                                qttxn.NXDaiThe = mmNhanXetDaiThe.Text;
                                qttxn.NXViThe = mmNhanXetViThe.Text;
                                qttxn.ChanDoanGiaiPhau = mmChanDoanGiaiPhau.Text;
                                qttxn.PhuHopChanDoan = mmSuPhuHop.Text;
                            }

                            if (data.SaveChanges() >= 0)
                            {
                                if (!ktraTH)
                                    MessageBox.Show("Cập nhật thông tin thành công");

                            }
                        }
                        if (ktraTH)
                        {
                            if (passData != null)
                            {
                                this.Close();
                                passData(maCB, ngayTH);
                            }
                        }
                    }
                    #endregion

                    #region XN Tế bào học
                    else if (_ploaiXN == 2)
                    {

                        if (lupMaCB1.EditValue != "0" && lupMaCB1.EditValue != null)
                            cls.MaCBLayMau = lupMaCB1.EditValue.ToString();
                        cls.BenhPham = txtBenhPham1.Text;
                        cls.TrangThaiBP = txttrangThaiBP1.Text;
                        if (!string.IsNullOrEmpty(lupNgayLayMau1.Text))
                            cls.ThoiGianLayMau = lupNgayLayMau1.DateTime;
                        else
                            cls.ThoiGianLayMau = null;
                        if (!string.IsNullOrEmpty(lupNgayNhanMau1.Text))
                            cls.ThoiGianNhanMau = lupNgayNhanMau1.DateTime;
                        else
                            cls.ThoiGianNhanMau = null;
                        if (!string.IsNullOrEmpty(dtTraKQ1.Text))
                            cls.NgayKQ = dtTraKQ1.DateTime;
                        else
                            cls.NgayKQ = null;
                        if (!string.IsNullOrEmpty(txtTrangThaiBN1.Text))
                            cls.TrangThaiBN = txtTrangThaiBN1.Text;
                        cls.GhiChu = txtGhiChu1.Text;
                        if (data.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công");
                            this.Close();
                        }
                    }

                    #endregion

                    #region chi dinh bn lao
                    else
                    {

                        if (lupMaCB.EditValue != null)
                            cls.MaCBLayMau = lupMaCB.EditValue.ToString();
                        cls.BenhPham = txtBenhPham.Text;
                        cls.TrangThaiBP = txttrangThaiBP.Text;
                        if (!string.IsNullOrEmpty(lupNgayLayMau.Text))
                            cls.ThoiGianLayMau = lupNgayLayMau.DateTime;
                        else
                            cls.ThoiGianLayMau = null;
                        if (!string.IsNullOrEmpty(lupNgayNhanMau.Text))
                            cls.ThoiGianNhanMau = lupNgayNhanMau.DateTime;
                        else
                            cls.ThoiGianNhanMau = null;
                        if (!string.IsNullOrEmpty(dtTraKQ.Text))
                            cls.NgayKQ = dtTraKQ.DateTime;
                        else
                            cls.NgayKQ = null;
                        if (!string.IsNullOrEmpty(txtTrangThaiBN.Text))
                            cls.TrangThaiBN = txtTrangThaiBN.Text;
                        if (data.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công");
                            this.Close();
                        }
                    }

                    #endregion
                }
                else
                    MessageBox.Show("Không tìm thấy IDCLs được chỉ định");
            }
        }

        private bool checkValidate()
        {
            if (_ploaiXN == 1)
            {
                if (!string.IsNullOrEmpty(dtNgayTra.Text) && !string.IsNullOrEmpty(dtNgayTH.Text) && dtNgayTra.DateTime < dtNgayTH.DateTime)
                {
                    MessageBox.Show("Ngày trả phải lớn hơn ngày thực hiện");
                    return false;
                }
                if (!string.IsNullOrEmpty(dtNgayTH.Text) && !string.IsNullOrEmpty(dtThoiGianPhaBP.Text) && dtNgayTH.DateTime < dtThoiGianPhaBP.DateTime)
                {
                    MessageBox.Show("Thời gian thực hiện phải lớn hơn thời gian pha bệnh phẩm");
                    return false;
                }
                if (!string.IsNullOrEmpty(dtThoiGianPhaBP.Text) && !string.IsNullOrEmpty(dtThoiGianCoDinhMau.Text) && dtThoiGianPhaBP.DateTime < dtThoiGianCoDinhMau.DateTime)
                {
                    MessageBox.Show("Thời gian pha bệnh phẩm phải lớn hơn thời gian cố định mẫu");
                    return false;
                }
                if (ktraTH)
                {
                    if (string.IsNullOrEmpty(dtNgayTH.Text))
                    {
                        MessageBox.Show("Bạn chưa nhập ngày thực hiện");
                        return false;
                    }
                    else if (string.IsNullOrEmpty(lupNguoiDocKQ.Text))
                    {
                        MessageBox.Show("Bạn chưa chọn cán bộ thực hiện/ người đọc kết quả");
                        return false;
                    }
                    else
                    {
                        if (dtNgayTH.DateTime > DateTime.Now)
                        {
                            MessageBox.Show("Thời gian thực hiện không được > thời gian hiện tại");
                            return false;
                        }
                        else
                        {
                            CL cls = data.CLS.Where(p => p.IdCLS == _idCLS).FirstOrDefault();
                            if (cls != null && cls.NgayThang != null && cls.NgayThang.Value > dtNgayTH.DateTime)
                            {
                                MessageBox.Show("Thời gian thực hiện không được < thời gian chỉ định");
                                return false;
                            }
                        }
                    }
                }
            }
            if (_ploaiXN == 2)
            {
                if (!string.IsNullOrEmpty(lupNgayLayMau1.Text) && !string.IsNullOrEmpty(lupNgayNhanMau1.Text) && lupNgayLayMau1.DateTime > lupNgayNhanMau1.DateTime)
                {
                    MessageBox.Show("Ngày lấy mẫu phải lớn hơn ngày nhận mẫu!");
                    return false;
                }
                if (!string.IsNullOrEmpty(dtTraKQ1.Text) && !string.IsNullOrEmpty(lupNgayNhanMau1.Text) && dtTraKQ1.DateTime < lupNgayNhanMau1.DateTime)
                {
                    MessageBox.Show("Ngày trả phải lớn hơn ngày nhận mẫu!");
                    return false;
                }
            }
            return true;
        }

        private void lupNgayLayMau_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ckXNGiaiPhauSinhThiet_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckXNGiaiPhauSinhThiet.Checked)
            //{
            //    lblThoiGianNhanMau.Text = "Thời gian cố định mẫu:";
            //    labelControl6.Visible = false;
            //    lblGhiChu.Text = "Tóm tắt dấu hiệu LS:";
            //    lblBenhPham.Text = "Vị trí sinh thiết:";
            //    lblTrangThaiBP.Text = "Nhận xét đại thể:";
            //}
            //else
            //{
            //    lblThoiGianNhanMau.Text = "Thời gian nhận mẫu:";
            //    labelControl6.Visible = true;
            //    lblGhiChu.Text = "Ghi chú: ";
            //    lblBenhPham.Text = "Bệnh phẩm:";
            //    lblTrangThaiBP.Text = "Trạng thái bệnh phẩm:";
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool ktraTH = false;
        private void btnLuuTT_Click(object sender, EventArgs e)
        {
            ktraTH = false;
            btnOK_Click(null, null);
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            ktraTH = true;
            btnOK_Click(null, null);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frm_kqcls.InPhieuXNGiaiPhauSinhThiet(_idCLS, _mabn);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lupThoiGianLayMau.Text))
            {
                MessageBox.Show("Chưa chọn thời gian lấy mẫu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupThoiGianLayMau.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(lupThoiGianLayMau.Text))
            {
                MessageBox.Show("Chưa chọn thời gian nhận mẫu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupThoiGianNhanMau.Focus();
                return;
            }
            else if (LupCanBo.EditValue == null)
            {
                MessageBox.Show("Chưa chọn cán bộ lấy mẫu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LupCanBo.Focus();
                return;
            }
            else if (LupCanBo.EditValue == null)
            {
                MessageBox.Show("Chưa chọn cán bộ nhận mẫu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LupCanBo1.Focus();
                return;
            }
            var cls = data.CLS.Where(p => p.IdCLS == _idCLS).ToList();
            if(cls.Count() > 0)
            {
                CL sua = cls.First();
                sua.ThoiGianLayMau = Convert.ToDateTime(lupThoiGianLayMau.EditValue);
                sua.ThoiGianNhanMau = Convert.ToDateTime(lupThoiGianNhanMau.EditValue);
                sua.MaCBLayMau = LupCanBo.EditValue.ToString();
                sua.MaCBNhanMau = LupCanBo1.EditValue.ToString();
            }
            data.SaveChanges();
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
