using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml.Linq;
using System.IO;
using System.Linq;

namespace QLBV.FormNhap
{
    //bệnh nhân tiểu đường và tăng huyết áp Insert vào DtuongManTinh trong bảng ravien : 1: Tiểu đường; 2: Tăng huyết áp; 3: Cả tiểu đường và tăng HA
    public partial class uc_DuyetBenhAn : DevExpress.XtraEditors.XtraUserControl
    {
        //  QLBV_Database.QLBVEntities data;
        string fileXML = "";
        //int _intSoHoSo = -1, _intMaYTe = -1;
        //private string _strsoluutru = "", _strMaYTe = "";
        private int _intMaKP = 0;
        int _mabn;
        private string _strStaticMayte = "";
        bool load = true;
        public uc_DuyetBenhAn()
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001")
                _ploaiNoiNgoaiTru = 1;
            else
                _ploaiNoiNgoaiTru = -1;
            _lMaYTe = SetMaYTe();
            List<KPhong> _lkp = data.KPhongs.Where(p => p.TrongBV == 1 && p.PLoai == "Lâm sàng").ToList();
            _lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaPhong.Properties.DataSource = _lkp;
            dtTuNgay.DateTime = System.DateTime.Now;
            dtDenNgay.DateTime = System.DateTime.Now;
            lupKPhongGr.DataSource = _lkp;
            lupKhoaPhong.EditValue = 0;
            radioGroup1.SelectedIndex = 0;
            loadDSBenhNhan();
            //_strStaticMayte = GetMaYTe(DungChung.Bien.MaBV);
            load = false;
        }
        void EnableControl(bool T)
        {
            btnSua.Enabled = T;
            btnLaySoTuDong.Enabled = !T;
            txtSoHoSo2.Properties.ReadOnly = T;
            if (DungChung.Bien.MaBV == "30009")
                txtMaYTe2.Properties.ReadOnly = false;
            else
                txtMaYTe2.Properties.ReadOnly = T;
            //btnSetSoHS.Enabled = !T;
            //btnSetMaYTe.Enabled = !T;
            btnLuu.Enabled = !T;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            loadDSBenhNhan();
        }
        private void grv_DsBenhNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _intMaKP = -1;

            if (grv_DsBenhNhan.GetRowCellValue(grv_DsBenhNhan.FocusedRowHandle, colMaBN) != null)
            {
                int maBN = Convert.ToInt32(grv_DsBenhNhan.GetRowCellValue(grv_DsBenhNhan.FocusedRowHandle, colMaBN));
                if (grv_DsBenhNhan.GetRowCellValue(grv_DsBenhNhan.FocusedRowHandle, colKhoaPhong) != null)
                {
                    _intMaKP = Convert.ToInt32(grv_DsBenhNhan.GetRowCellValue(grv_DsBenhNhan.FocusedRowHandle, colKhoaPhong));
                    string tenkhoa = grv_DsBenhNhan.GetRowCellDisplayText(grv_DsBenhNhan.FocusedRowHandle, colKhoaPhong).ToString().Trim();
                    lblKP.Text = tenkhoa == "" ? _intMaKP.ToString() : tenkhoa;
                    lblKPravien1.Text = tenkhoa == "" ? _intMaKP.ToString() : tenkhoa;
                    //if (lupKhoaPhong.EditValue == null || Convert.ToInt32(lupKhoaPhong.EditValue) == 0)
                    //GetSoHienTai();
                }
                if (maBN > 0)
                {
                    viewBenhNhanInfo(maBN);
                    btnLaySoTuDong.Text = "Lấy số  hồ sơ, mã y tế tự động";
                }

            }
        }
        private void viewBenhNhanInfo(int maBN)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (radioGroup1.SelectedIndex == 1)
            {
                var q = (from bn in data.BenhNhans.Where(p => p.MaBNhan == maBN)
                         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                         join vv in data.VaoViens on ttbx.MaBNhan equals vv.MaBNhan
                         select new { bn, vv, rv, ttbx.So_eTBM }).ToList();
                if (q.Count > 0)
                {
                    var qmnt = (from cls in data.CLS.Where(p1 => p1.MaBNhan == maBN)
                                join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                                select new { kp.TenKP }).ToList();
                    if (qmnt.Count > 0)
                    {
                        var p1 = qmnt.First();


                        txtkp1.Visible = true;
                        txtkp1.Text = p1.TenKP.ToString();


                    }
                    var p = q.First();
                    if (!String.IsNullOrEmpty(p.bn.TenBNhan))
                        lblTenBNhan.Text = p.bn.TenBNhan.ToUpper();
                    else
                        lblTenBNhan.Text = "";
                    txtMaBN2.Text = maBN.ToString();
                    if (!String.IsNullOrEmpty(p.bn.NgaySinh))
                        txtNgaySinh.Text = p.bn.NgaySinh.ToString();
                    else
                        txtNgaySinh.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.ThangSinh))
                        txtThangSinh.Text = p.bn.ThangSinh.ToString();
                    else
                        txtThangSinh.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.NamSinh))
                        txtNamSinh.Text = p.bn.NamSinh.ToString();
                    else
                        txtNamSinh.Text = "";
                    if (p.bn.GTinh == 0)
                        lblGioiTinh.Text = "Nữ";
                    else
                        lblGioiTinh.Text = "Nam";
                    if (p.bn.NoiTru == 0)
                    {
                        lblNoiNgoaiTru.Text = "Ngoại trú";
                        txtkp1.Visible = true;
                    }
                    else
                    {
                        lblNoiNgoaiTru.Text = "Nội trú";
                        txtkp1.Visible = false;
                    }
                    _noitru = p.bn.NoiTru ?? -1;
                    if (p.bn.CapCuu == 1)
                        ckCapCuu.Checked = true;
                    else
                        ckCapCuu.Checked = false;
                    if (!String.IsNullOrEmpty(p.bn.DChi))
                        lblDiaChi.Text = p.bn.DChi.ToString();

                    if (data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).Count() > 0 && data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).First().DTBN1 != null)
                    {
                        lblDoituong.Text = data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).First().DTBN1;
                    }
                    else
                        lblDoituong.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.SThe))
                    {
                        lblSoThe.Text = p.bn.SThe.ToString();
                        if (p.bn.Tuyen == 1)
                            lbltuyen.Text = "Đúng tuyến";
                        else if (p.bn.Tuyen == 2)
                            lbltuyen.Text = "Trái tuyến";
                        else
                            lbltuyen.Text = "";
                        if (p.bn.HanBHTu != null)
                            dtHanBHtu.DateTime = Convert.ToDateTime(p.bn.HanBHTu);
                        if (p.bn.HanBHDen != null)
                            dtHanBHDen.DateTime = Convert.ToDateTime(p.bn.HanBHDen);
                    }
                    else
                    {
                        lblSoThe.Text = "";
                    }

                    if (p.rv.NgayRa != null)
                        dtNgayRV.DateTime = Convert.ToDateTime(p.rv.NgayRa);
                    if (p.rv.SoNgaydt != null)
                        lblSoNgayDT.Text = p.rv.SoNgaydt.ToString();
                    lblCBCV.Text = "";
                    lupLanhDaoKyDUyet.ReadOnly = true;
                    lupLanhDaoKyDUyet.EditValue = "";
                    if (p.rv.Status == 1)
                    {
                        var cb = data.CanBoes.Where(a => a.Status == 1).ToList();
                        lblTrangThaiRV.Text = "Chuyển viện";
                        string cbcv = "";
                        if(data.CanBoes.Where(a => a.MaCB == p.rv.MaBS).Select(a => a.TenCB).ToList().Count() > 0)
                        {
                            cbcv = !string.IsNullOrEmpty(p.rv.MaBS) ? data.CanBoes.Where(a => a.MaCB == p.rv.MaBS).Select(a => a.TenCB).FirstOrDefault().ToString() : "";
                        }
                        lblCBCV.Text = cbcv;
                        lupLanhDaoKyDUyet.ReadOnly = false;
                        lupLanhDaoKyDUyet.Properties.DataSource = cb.Where(a => a.ChucVu.ToLower().Contains("giám đốc") || a.ChucVu.ToLower().Contains("gđ") || a.ChucVu.ToLower().Contains("gd")).ToList();
                        if (!string.IsNullOrEmpty(p.rv.MaCBKyDuyet))
                        {
                            lupLanhDaoKyDUyet.EditValue = p.rv.MaCBKyDuyet;
                        }
                    }
                    else if (p.rv.Status == 2)
                        lblTrangThaiRV.Text = "Ra viện";
                    else if (p.rv.Status == 3)
                        lblTrangThaiRV.Text = "Trốn viện";
                    else if (p.rv.Status == 4)
                        lblTrangThaiRV.Text = "Xin ra viện";
                    else
                        lblTrangThaiRV.Text = "";
                    ckTieuDuong.Checked = false;
                    ckTangHuyetAp.Checked = false;
                    if (p.rv.DTuongManTinh == "3")
                    {
                        ckTieuDuong.Checked = true;
                        ckTangHuyetAp.Checked = true;
                    }
                    else if (p.rv.DTuongManTinh == "2")
                    {
                        ckTangHuyetAp.Checked = true;
                    }
                    else if (p.rv.DTuongManTinh == "1")
                    {
                        ckTieuDuong.Checked = true;
                    }


                    if (p.So_eTBM != null && p.So_eTBM != "")
                    {
                        txtsohsbnmantinh.Text = p.So_eTBM.ToString();
                    }


                    if (!String.IsNullOrEmpty(p.rv.ChanDoan))
                        txtchanDoan.Text = DungChung.Ham.FreshString(p.rv.ChanDoan);
                    else
                        txtchanDoan.Text = "";

                    if (!String.IsNullOrEmpty(p.rv.LoiDan))
                        mmLoiKhuyenBS.Text = p.rv.LoiDan.ToString();
                    else
                        mmLoiKhuyenBS.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.TinhTrangC))
                        lbltinhtrangchuyen.Text = p.rv.TinhTrangC.ToString();
                    else
                        lbltinhtrangchuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.LyDoC))
                        lblLydochuyen.Text = p.rv.LyDoC.ToString();
                    else
                        lblLydochuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.HinhThucC))
                        lblHinhThucChuyen.Text = p.rv.HinhThucC.ToString();
                    else
                        lblHinhThucChuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.SoLT) && p.rv.SoLT.ToString().Trim() != "")
                    {
                        txtSoHoSo2.Text = p.rv.SoLT.ToString().Trim();
                        EnableControl(true);
                    }
                    else
                    {
                        txtSoHoSo2.Text = "";
                        EnableControl(false);
                    }
                    if (!String.IsNullOrEmpty(p.rv.MaYTe) && p.rv.MaYTe.ToString().Trim() != "")
                    {
                        txtMaYTe2.Text = p.rv.MaYTe.ToString().Trim();
                    }
                    else
                    {
                        txtMaYTe2.Text = "";
                    }
                }
            }
            else if (radioGroup1.SelectedIndex == 0)
            {
                var q = (from bn in data.BenhNhans.Where(p => p.MaBNhan == maBN)
                     join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                     join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                     select new { bn, rv, ttbx.So_eTBM }).ToList();
                if (q.Count > 0)
                {
                    var qmnt = (from cls in data.CLS.Where(p1 => p1.MaBNhan == maBN)
                                join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                                select new { kp.TenKP }).ToList();
                    if (qmnt.Count > 0)
                    {
                        var p1 = qmnt.First();


                        txtkp1.Visible = true;
                        txtkp1.Text = p1.TenKP.ToString();


                    }
                    var p = q.First();
                    if (!String.IsNullOrEmpty(p.bn.TenBNhan))
                        lblTenBNhan.Text = p.bn.TenBNhan.ToUpper();
                    else
                        lblTenBNhan.Text = "";
                    txtMaBN2.Text = maBN.ToString();
                    if (!String.IsNullOrEmpty(p.bn.NgaySinh))
                        txtNgaySinh.Text = p.bn.NgaySinh.ToString();
                    else
                        txtNgaySinh.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.ThangSinh))
                        txtThangSinh.Text = p.bn.ThangSinh.ToString();
                    else
                        txtThangSinh.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.NamSinh))
                        txtNamSinh.Text = p.bn.NamSinh.ToString();
                    else
                        txtNamSinh.Text = "";
                    if (p.bn.GTinh == 0)
                        lblGioiTinh.Text = "Nữ";
                    else
                        lblGioiTinh.Text = "Nam";
                    if (p.bn.NoiTru == 0)
                    {
                        lblNoiNgoaiTru.Text = "Ngoại trú";
                        txtkp1.Visible = true;
                    }
                    else
                    {
                        lblNoiNgoaiTru.Text = "Nội trú";
                        txtkp1.Visible = false;
                    }
                    _noitru = p.bn.NoiTru ?? -1;
                    if (p.bn.CapCuu == 1)
                        ckCapCuu.Checked = true;
                    else
                        ckCapCuu.Checked = false;
                    if (!String.IsNullOrEmpty(p.bn.DChi))
                        lblDiaChi.Text = p.bn.DChi.ToString();

                    if (data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).Count() > 0 && data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).First().DTBN1 != null)
                    {
                        lblDoituong.Text = data.DTBNs.Where(m => m.IDDTBN == p.bn.IDDTBN).First().DTBN1;
                    }
                    else
                        lblDoituong.Text = "";
                    if (!String.IsNullOrEmpty(p.bn.SThe))
                    {
                        lblSoThe.Text = p.bn.SThe.ToString();
                        if (p.bn.Tuyen == 1)
                            lbltuyen.Text = "Đúng tuyến";
                        else if (p.bn.Tuyen == 2)
                            lbltuyen.Text = "Trái tuyến";
                        else
                            lbltuyen.Text = "";
                        if (p.bn.HanBHTu != null)
                            dtHanBHtu.DateTime = Convert.ToDateTime(p.bn.HanBHTu);
                        if (p.bn.HanBHDen != null)
                            dtHanBHDen.DateTime = Convert.ToDateTime(p.bn.HanBHDen);
                    }
                    else
                    {
                        lblSoThe.Text = "";
                    }

                    if (p.rv.NgayRa != null)
                        dtNgayRV.DateTime = Convert.ToDateTime(p.rv.NgayRa);
                    if (p.rv.SoNgaydt != null)
                        lblSoNgayDT.Text = p.rv.SoNgaydt.ToString();
                    lblCBCV.Text = "";
                    lupLanhDaoKyDUyet.ReadOnly = true;
                    lupLanhDaoKyDUyet.EditValue = "";
                    if (p.rv.Status == 1)
                    {
                        var cb = data.CanBoes.Where(a => a.Status == 1).ToList();
                        lblTrangThaiRV.Text = "Chuyển viện";
                        string cbcv = "";
                        if (data.CanBoes.Where(a => a.MaCB == p.rv.MaBS).Select(a => a.TenCB).ToList().Count() > 0)
                        {
                            cbcv = !string.IsNullOrEmpty(p.rv.MaBS) ? data.CanBoes.Where(a => a.MaCB == p.rv.MaBS).Select(a => a.TenCB).FirstOrDefault().ToString() : "";
                        }
                        lblCBCV.Text = cbcv;
                        lupLanhDaoKyDUyet.ReadOnly = false;
                        lupLanhDaoKyDUyet.Properties.DataSource = cb.Where(a => a.ChucVu.ToLower().Contains("giám đốc") || a.ChucVu.ToLower().Contains("gđ") || a.ChucVu.ToLower().Contains("gd")).ToList();
                        if (!string.IsNullOrEmpty(p.rv.MaCBKyDuyet))
                        {
                            lupLanhDaoKyDUyet.EditValue = p.rv.MaCBKyDuyet;
                        }
                    }
                    else if (p.rv.Status == 2)
                        lblTrangThaiRV.Text = "Ra viện";
                    else if (p.rv.Status == 3)
                        lblTrangThaiRV.Text = "Trốn viện";
                    else if (p.rv.Status == 4)
                        lblTrangThaiRV.Text = "Xin ra viện";
                    else
                        lblTrangThaiRV.Text = "";
                    ckTieuDuong.Checked = false;
                    ckTangHuyetAp.Checked = false;
                    if (p.rv.DTuongManTinh == "3")
                    {
                        ckTieuDuong.Checked = true;
                        ckTangHuyetAp.Checked = true;
                    }
                    else if (p.rv.DTuongManTinh == "2")
                    {
                        ckTangHuyetAp.Checked = true;
                    }
                    else if (p.rv.DTuongManTinh == "1")
                    {
                        ckTieuDuong.Checked = true;
                    }


                    if (p.So_eTBM != null && p.So_eTBM != "")
                    {
                        txtsohsbnmantinh.Text = p.So_eTBM.ToString();
                    }


                    if (!String.IsNullOrEmpty(p.rv.ChanDoan))
                        txtchanDoan.Text = DungChung.Ham.FreshString(p.rv.ChanDoan);
                    else
                        txtchanDoan.Text = "";

                    if (!String.IsNullOrEmpty(p.rv.LoiDan))
                        mmLoiKhuyenBS.Text = p.rv.LoiDan.ToString();
                    else
                        mmLoiKhuyenBS.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.TinhTrangC))
                        lbltinhtrangchuyen.Text = p.rv.TinhTrangC.ToString();
                    else
                        lbltinhtrangchuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.LyDoC))
                        lblLydochuyen.Text = p.rv.LyDoC.ToString();
                    else
                        lblLydochuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.HinhThucC))
                        lblHinhThucChuyen.Text = p.rv.HinhThucC.ToString();
                    else
                        lblHinhThucChuyen.Text = "";
                    if (!String.IsNullOrEmpty(p.rv.SoLT) && p.rv.SoLT.ToString().Trim() != "")
                    {
                        txtSoHoSo2.Text = p.rv.SoLT.ToString().Trim();
                        EnableControl(true);
                    }
                    else
                    {
                        txtSoHoSo2.Text = "";
                        EnableControl(false);
                    }
                    if (!String.IsNullOrEmpty(p.rv.MaYTe) && p.rv.MaYTe.ToString().Trim() != "")
                    {
                        txtMaYTe2.Text = p.rv.MaYTe.ToString().Trim();
                    }
                    else
                    {
                        txtMaYTe2.Text = "";
                    }
                }
            }
            
        }
        private void loadDSBenhNhan()
        {
            if (DungChung.Bien.MaBV != "24012") 
            {
                colSoHS.Visible = false;
            }    
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _intMaKP = 0;
            string _strMaYTe = "", _strSoHoSo = "";
            DateTime _dtTuNgay = DateTime.Now;
            DateTime _dtDenNgay = DateTime.Now;
            int status = -1;// 0: cả hai; 1- chưa duyệt; 2- đã duyệt
            bool BNChuyen = false; // lọc bn chuyển
            if (lupKhoaPhong.EditValue != null)
                _intMaKP = Convert.ToInt32(lupKhoaPhong.EditValue);

            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            string tenbn = txtMaBN.Text.ToLower();
            _strMaYTe = txt_MaYte_TK.Text;
            _strSoHoSo = txtSoHoSo.Text;
            _dtTuNgay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            _dtDenNgay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            status = rgchonduyet.SelectedIndex;
            BNChuyen = ckcBNChuyen.Checked;
            var q2 = (from bn in data.BenhNhans.Where(p => p.NoiTru == radioGroup1.SelectedIndex)
                      join rv in data.RaViens.Where(p => p.NgayRa >= _dtTuNgay && p.NgayRa <= _dtDenNgay) on bn.MaBNhan equals rv.MaBNhan
                      join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                      select new { bn.MaBNhan, bn.TenBNhan, bn.DTNT, bn.NoiTru, bn.TuyenDuoi, bn.GTinh, bn.Tuoi, rv.NgayRa, rv.MaKP, rv.Status, rv.SoLT, rv.MaYTe, bn.NNhap, kp.TenKP }).ToList();


            grc_DSBenhNhan.DataSource = null;
            if (BNChuyen)
            {
                var q = (from bn in q2.Where(p => p.TuyenDuoi == 0 && (p.NoiTru == radioGroup1.SelectedIndex || p.DTNT == true))
                    .Where(p => (tenbn == "" ? true : p.MaBNhan == _int_maBN) || p.TenBNhan.ToLower().Contains(tenbn))
                    .Where(p => _intMaKP == 0 ? true : p.MaKP == _intMaKP)
                    .Where(p => _strMaYTe == "" ? true : (p.MaYTe != null && p.MaYTe.Trim() == _strMaYTe))
                    .Where(p => p.Status == 1)
                    .Where(p => _strSoHoSo == "" ? true : (p.SoLT != null && p.SoLT.Trim() == _strSoHoSo))
                    .Where(p => status == 0 ? true : (status == 1 ? (p.SoLT == null || p.SoLT.Trim() == "") : (p.SoLT != null && p.SoLT.Trim() != "")))
                        join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                        select new { bn.MaBNhan, bn.NgayRa, bn.MaKP, bn.MaYTe, bn.SoLT, bn.TenBNhan, NgayVao = bn.NNhap, kp.TenKP, GTinh = bn.GTinh == 2 ? "Nữ" : "Nam", bn.Tuoi }
                ).OrderBy(p => p.NgayRa).ToList();
                grc_DSBenhNhan.DataSource = q;
            }
            else
            {
                var q = (from bn in q2.Where(p => p.TuyenDuoi == 0 && (p.NoiTru == radioGroup1.SelectedIndex || p.DTNT == true))
                         .Where(p => (tenbn == "" ? true : p.MaBNhan == _int_maBN) || p.TenBNhan.ToLower().Contains(tenbn))
                         .Where(p => _intMaKP == 0 ? true : p.MaKP == _intMaKP)
                         .Where(p => _strMaYTe == "" ? true : (p.MaYTe != null && p.MaYTe.Trim() == _strMaYTe))
                         .Where(p => BNChuyen == true ? p.Status == 1 : true)
                         .Where(p => _strSoHoSo == "" ? true : (p.SoLT != null && p.SoLT.Trim() == _strSoHoSo))
                         .Where(p => status == 0 ? true : (status == 1 ? (p.SoLT == null || p.SoLT.Trim() == "") : (p.SoLT != null && p.SoLT.Trim() != "")))
                         join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                         join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                         select new { bn.MaBNhan, bn.NgayRa, bn.MaKP, bn.MaYTe, bn.SoLT, bn.TenBNhan, vv.NgayVao, kp.TenKP, GTinh = bn.GTinh == 2 ? "Nữ" : "Nam", bn.Tuoi }
                     ).OrderBy(p => p.NgayRa).ToList();
                grc_DSBenhNhan.DataSource = q;
            }
            

        }
        private void btnLaySo_Click(object sender, EventArgs e)
        {

        }
        // đánh dấu có đặt số trên text box làm số hiện tại hay không
        //bool _bolSetSoHoSo = false;
        //bool _bolSetMaYTe = false;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int noingoaitru = -1;
            if (DungChung.Bien.MaBV == "12122")
                noingoaitru = _noitru;
            if (DungChung.Bien.MaBV == "30003")
            {
                txtSoHoSo2.Text = "Z";
            }
            if (string.IsNullOrEmpty(txtSoHoSo2.Text) && DungChung.Bien.MaBV != "30003")
            {
                MessageBox.Show("Bạn chưa nhập số lưu trữ");
                txtSoHoSo2.Focus();
            }
            else
            {
                int _mabn = String.IsNullOrEmpty(txtMaBN2.Text) ? 0 : Convert.ToInt32(txtMaBN2.Text);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                bool kt = true;

                if (txtSoHoSo2.Text != "")
                {
                    var q1 = (from rv in data.RaViens.Where(p => p.SoLT == txtSoHoSo2.Text)
                              join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                              where (bn.MaBNhan != _mabn && (_ploaiNoiNgoaiTru == -1 || (_ploaiNoiNgoaiTru == 1 && bn.NoiTru == noingoaitru)))
                              select rv
                                 ).ToList();
                    if (q1.Count > 0 && DungChung.Bien.MaBV != "30009" && DungChung.Bien.MaBV != "30003")
                    {
                        MessageBox.Show("Số lưu trữ trùng lặp. Vui lòng nhập số lưu trữ mới");
                        kt = false;
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "12122")
                        {
                            if (KTChuoi(txtSoHoSo2.Text))
                                kt = true;
                            else
                            {
                                MessageBox.Show("Số lưu trữ chỉ nhập kiểu số");
                                kt = false;
                            }
                        }
                    }
                    if (txtMaYTe2.Text != "" && DungChung.Bien.MaBV != "30009" && DungChung.Bien.MaBV != "30003")
                    {
                        int dtkt = DateTime.Now.Year;
                        var qrv = data.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (qrv != null)
                            dtkt = qrv.NgayRa.Value.Year;
                        var q2 = (from rv in data.RaViens.Where(p => p.MaBNhan != _mabn)
                                  join vv in data.VaoViens on rv.MaBNhan equals vv.MaBNhan
                                  select new { rv, vv }).Where(p =>( p.rv.MaKP == _intMaKP )&& p.rv.NgayRa.Value.Year == dtkt && ((p.rv.MaYTe != null && p.rv.MaYTe.Trim() == txtMaYTe2.Text.Trim()) || (p.vv.SoBA != null && p.vv.SoBA.Trim() == txtMaYTe2.Text))).ToList();

                        if (q2.Count > 0)
                        {
                            MessageBox.Show("Mã y tế trùng lặp. Vui lòng nhập mã y tế mới");
                            kt = false;
                        }
                    }
                }
                if (kt)
                {
                    // data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    List<RaVien> qrv = data.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                    List<TTboXung> tt = data.TTboXungs.Where(p => p.MaBNhan == _mabn).ToList();
                    if (qrv.Count > 0)
                    {
                        RaVien rv = qrv.First();
                        rv.SoLT = txtSoHoSo2.Text.Trim();
                        rv.MaYTe = txtMaYTe2.Text.Trim();
                        rv.MaCBKyDuyet = lupLanhDaoKyDUyet.EditValue.ToString();
                        if (ckTieuDuong.Checked && ckTangHuyetAp.Checked)
                        { rv.DTuongManTinh = "3"; }
                        else if (ckTieuDuong.Checked)
                            rv.DTuongManTinh = "1";
                        else if (ckTangHuyetAp.Checked)
                            rv.DTuongManTinh = "2";
                        TTboXung ttbx = tt.First();
                        ttbx.So_eTBM = txtsohsbnmantinh.Text;

                        if (data.SaveChanges() > 0)
                        {
                            MessageBox.Show("Lưu thành công");
                            btnSua.Enabled = true;
                        }
                        if (LayTuDong)
                        {
                            int _makp = 0;
                            if (grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong) != null)
                            {
                                _makp = Convert.ToInt32(grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong));
                            }
                            int solt = Convert.ToInt32(txtSoHoSo2.Text);
                            if (DungChung.Bien.PP_SoLT == 1)
                            {
                                DungChung.Ham.SetSoPL(_makp, solt, 7, noingoaitru);
                            }
                            else if (DungChung.Bien.PP_SoLT == 2)
                            {
                                DungChung.Ham.SetSoPL(0, solt, 7, noingoaitru);
                            }
                        }
                    }
                    btnLaySoTuDong.Text = "Lấy số  hồ sơ, mã y tế tự động";
                    EnableControl(true);
                    loadDSBenhNhan();
                }
                ckTieuDuong.Properties.ReadOnly = false;
                ckTangHuyetAp.Properties.ReadOnly = false;

            }
        }

        #region Function KTChuoi
        private bool KTChuoi(string str)
        {
            foreach (Char c in str)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        #endregion

        //private bool updateXMLFile(string nodeNam, int value)
        //{
        //    int noingoaitru = -1;
        //    if (DungChung.Bien.MaBV == "12122")
        //        noingoaitru = _noitru;
        //    if (nodeNam == "sohoso" && _ploaiNoiNgoaiTru == 1)
        //    {
        //        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //        SoPL qsolt = data.SoPLs.Where(p => p.PhanLoai == 7 && p.MaKP == 0).Where(p => _ploaiNoiNgoaiTru == -1 || (_ploaiNoiNgoaiTru == 1 && p.NoiTru == noingoaitru)).OrderByDescending(p => p.NgayNhap).FirstOrDefault();
        //        if (qsolt != null)
        //        {
        //            SoPL moi = qsolt;
        //            data.Remove(qsolt);
        //            data.SaveChanges();
        //            moi.SoPL1 = value - 1;
        //            data.SoPLs.Add(moi);
        //            data.SaveChanges();
        //            return true;
        //        }
        //        else
        //        {
        //            SoPL moi = new SoPL();                   
        //            moi.SoPL1 = value - 1;
        //            moi.PhanLoai = 7;
        //            moi.MaKP = 0;
        //            moi.NoiTru = noingoaitru;
        //            moi.NgayNhap = DateTime.Now;
        //            data.SoPLs.Add(moi);
        //            data.SaveChanges();
        //            return true;
        //        }
        //        return false;

        //    }
        //    else
        //    {
        //        try
        //        {
        //            XDocument doc = new XDocument();
        //            doc = XDocument.Load(fileXML);
        //            var nam = (from c in doc.Descendants("str" + DateTime.Now.Year.ToString()) select c).ToList();
        //            if (nam.Count > 0)
        //            {

        //                if (nodeNam == "sohoso")
        //                    nam.First().SetElementValue(nodeNam, value);
        //                else
        //                    if (nodeNam == "mayte" && _intMaKP > 0)
        //                    {
        //                        if (doc.Element("str" + DateTime.Now.Year.ToString()).Element("mayte") == null)
        //                            doc.Element("str" + DateTime.Now.Year.ToString()).Add(new XElement("mayte"));
        //                        XElement xmlEl = doc.Element("str" + DateTime.Now.Year.ToString()).Element("mayte");
        //                        if (doc.Element("str" + DateTime.Now.Year.ToString()).Element("mayte").Element("KP" + _intMaKP.ToString()) == null)
        //                            xmlEl.Add(new XElement("KP" + _intMaKP.ToString(), value));
        //                        else
        //                            doc.Element("str" + DateTime.Now.Year.ToString()).Element("mayte").SetElementValue("KP" + _intMaKP.ToString(), value);
        //                    }
        //                doc.Save(fileXML);
        //            }
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}
        private void lupKhoaPhong_EditValueChanged(object sender, EventArgs e)
        {
          if (lupKhoaPhong.EditValue != null)
               _intMaKP = Convert.ToInt32(lupKhoaPhong.EditValue);
           if (!load)
                loadDSBenhNhan();
            //GetSoHienTai();
        }

        private void txtMaBN_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!load)
                loadDSBenhNhan();
        }

        private void dtDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!load)
            //    loadDSBenhNhan();
        }

        private void txt_MaYte_TK_TextChanged(object sender, EventArgs e)
        {
            //if (!load)
            //    loadDSBenhNhan();
        }

        private void txtSoHoSo_TextChanged(object sender, EventArgs e)
        {
            //if (!load)
            //    loadDSBenhNhan();
        }

        private void ckDaDuyet_CheckedChanged(object sender, EventArgs e)
        {
            //if (!load)
            //    loadDSBenhNhan();
        }

        private void ckChuaDuyet_CheckedChanged(object sender, EventArgs e)
        {
            //if (!load)
            //    loadDSBenhNhan();
        }

        private void ckLaySoTuDong_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnSetSoHS_Click(object sender, EventArgs e)
        {
            //int out1;

            //if (int.TryParse(txtSoHoSo2.Text.Trim(), out out1))
            //{
            //    _intSoHoSo = Convert.ToInt32(txtSoHoSo2.Text.Trim());
            //    _strsoluutru = txtSoHoSo2.Text.Trim();
            //    try
            //    {

            //            updateXMLFile("sohoso", _intSoHoSo + 1);
            //            MessageBox.Show("Cập nhật số hồ sơ thành công");

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Cập nhật số hồ sơ lỗi");
            //    }
            //}
            //_bolSetSoHoSo = true;
        }

        //private string getEndstring()
        //{
        //    string rs = "";
        //    if (txtMaYTe2.Text.Trim().Length > _strStaticMayte.Length && txtMaYTe2.Text.Trim().Substring(0, _strStaticMayte.Length) == (_strStaticMayte))
        //        rs = txtMaYTe2.Text.Trim().Substring(_strStaticMayte.Length, txtMaYTe2.Text.Trim().Length - _strStaticMayte.Length);

        //    return rs;
        //}
        private void btnSetMaYTe_Click(object sender, EventArgs e)
        {
            //int out2;
            //string rs = getEndstring();
            //if (rs == "")
            //    MessageBox.Show("Mã y tế không hợp lệ");
            //else
            //{
            //    if (int.TryParse(rs, out out2))
            //    {
            //        _intMaYTe = Convert.ToInt32(rs);
            //        _strMaYTe = txtMaYTe2.Text.Trim();
            //        try
            //        {
            //            updateXMLFile("mayte", _intMaYTe + 1);
            //            _bolSetMaYTe = true;
            //            MessageBox.Show("Cập nhật mã y tế thành công");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Cập nhật mã y tế lỗi");
            //        }
            //    }
            //}

        }
        public class MaYTe
        {
            string _MaBV;

            public string MaBV
            {
                get { return _MaBV; }
                set { _MaBV = value; }
            }
            string _MaYTe;

            public string MaYTe1
            {
                get { return _MaYTe; }
                set { _MaYTe = value; }
            }
        }

        /// <summary>
        /// Lấy chuỗi đầu trong mã y tế (chuỗi thay đổi theo bệnh viện và năm
        /// </summary>
        /// <param name="mabv"></param>
        /// <returns></returns>
        //private string GetMaYTe(string mabv)
        //{
        //    var q = _lMaYTe.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
        //    if (q.Count > 0 && q.First().MaYTe1 != null && q.First().MaYTe1.ToString().Trim() != "")
        //        return q.First().MaYTe1.ToString().Trim();
        //    else
        //        return "";
        //}
        bool LayTuDong = false;
        private void btnLaySoTuDong_Click(object sender, EventArgs e)
        {
            // List<MaYTe> _lmayte=  SetMaYTe();
            //string mayte = GetMaYTe(DungChung.Bien.MaBV);
            if (btnLaySoTuDong.Text == "Lấy số  hồ sơ, mã y tế tự động")
            {
                LayTuDong = true;
                int _intSoHoSo = 1;
                string _intMaYTe = "";
                int noingoaitru = -1, _makp = 0;
                if (DungChung.Bien.MaBV == "12122")
                    noingoaitru = _noitru;// thiết lập số lưu trữ tách theo nội ngoại trú
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (txtMaBN2.Text != "")
                {
                    int _mabn = Convert.ToInt32(txtMaBN2.Text);
                    // số hồ sơ

                    if (grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong) != null)
                    {
                        _makp = Convert.ToInt32(grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong));
                    }
                    if (DungChung.Bien.PP_SoLT == 0)
                    {
                        if (DungChung.Bien.MaBV == "30003")
                        {
                            txtSoHoSo2.Text = "z";
                            txtSoHoSo2.Properties.ReadOnly = true;
                        }
                    }
                    if (DungChung.Bien.PP_SoLT == 1)
                    {
                        _intSoHoSo = DungChung.Ham.GetSoPL(7, _makp, noingoaitru);
                        txtSoHoSo2.Text = _intSoHoSo.ToString();
                        txtSoHoSo2.Properties.ReadOnly = true;
                    }
                    else if (DungChung.Bien.PP_SoLT == 2)
                    {
                        _intSoHoSo = DungChung.Ham.GetSoPL(7, 0, noingoaitru);
                        txtSoHoSo2.Text = _intSoHoSo.ToString();
                        txtSoHoSo2.Properties.ReadOnly = true;
                    }

                    // mã y tế: -	Mã Y tế toàn quốc (Mã YT) gồm 14 ký tự:
                    //+ 3 ký tự đầu là mã tỉnh, thành phố, ví dụ thành phố Hà Nội là 101 (xem phụ lục: danh mục hành chính Việt Nam).
                    //+ 3 ký tự thứ hai là mã bệnh viện, viện (số này do Bộ Y tế cấp - xem phụ lục).
                    //+ 2 ký tự thứ ba: Mã năm, ví dụ 2001: ghi 01
                    //+ 6 ký tự là số vào viện của người bệnh do phòng Kế hoạch tổng hợp cấp cho người bệnh bằng số tự nhiên. Ví dụ: người bệnh vào bệnh viện, viện o giờ, ngày 1 tháng 1 năm 2001 được cấp mã 000001, người bệnh vào thứ hai được cấp mã số 000002... đến hết 24 giờ ngày 31 tháng 12 năm 2001. Cũng lấy các ký tự này ghi số vào viện cho các phiếu giấy có đề mục "Số vào viện".
                    var vv = data.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (vv != null)
                    {
                        string nam = "";
                        DateTime ngayvao = Convert.ToDateTime(vv.NgayVao);
                        nam = ngayvao.Year.ToString().Substring(2);
                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "01049")
                        {
                            if (DungChung.Bien.MaBV == "30009")
                            {
                                txtMaYTe2.Text = vv.SoBA;
                            }
                            else
                            {
                                try
                                {
                                    int sovv = Convert.ToInt32(vv.SoVV);
                                    _intMaYTe = DungChung.Bien.MaBV + nam + sovv.ToString("D5");
                                    txtMaYTe2.Text = _intMaYTe;
                                }
                                catch
                                {
                                    if (DungChung.Bien.PP_SoVV == 0)
                                        txtMaYTe2.Text = "0";
                                    else
                                        MessageBox.Show("Số vào viện không chính xác, không lấy được mã y tế");
                                }
                            }
                        }
                        else
                        {
                            txtMaYTe2.Text = vv.SoVV;
                        }
                    }
                }
                btnLaySoTuDong.Text = "Hủy lấy số hồ sơ, mã y tế tự động";
                if(DungChung.Bien.MaBV != "30009")
                txtMaYTe2.Properties.ReadOnly = true;
                //txtSoHoSo2.Properties.ReadOnly = true;
            }
            else
            {
                txtMaYTe2.Properties.ReadOnly = false;
                txtSoHoSo2.Properties.ReadOnly = false;
                txtSoHoSo2.Text = "";
                txtMaYTe2.Text = "";
                LayTuDong = false;
                btnLaySoTuDong.Text = "Lấy số  hồ sơ, mã y tế tự động";
            }


            //#region lấy từ fileXML
            //// kiểm tra xem file XML đã tồn tại hay chưa
            //XDocument document = new XDocument();
            //XDocument doc2 = new XDocument();// file XML chứa số hồ sơ, mã y tế từng năm           

            //fileXML = System.IO.Directory.GetCurrentDirectory() + "\\duyetbenhan.xml";
            //try
            //{

            //    if (!File.Exists(fileXML))
            //    {
            //        XElement xmlE = new XElement("str" + DateTime.Now.Year.ToString());
            //        xmlE.Add(new XElement("sohoso", 1));
            //        if (_intMaKP > 0)
            //        {
            //            XElement xmlMaYte = new XElement("mayte", new XElement("KP" + _intMaKP.ToString(), 1));
            //            xmlE.Add(xmlMaYte);
            //        }
            //        xmlE.Save(fileXML);
            //    }


            //    doc2 = XDocument.Load(fileXML);
            //    // XElement xmlENam = new XElement("str" + DateTime.Now.Year.ToString());
            //    XElement xmlENam = doc2.Element("str" + DateTime.Now.Year.ToString());
            //    if (_ploaiNoiNgoaiTru == -1)
            //    {
            //        if (xmlENam != null)
            //        {

            //            _intSoHoSo = xmlENam.Element("sohoso") == null ? 1 : Convert.ToInt32(xmlENam.Element("sohoso").Value);

            //            if (_intMaKP == 0)
            //            {
            //                _intMaYTe = -1;
            //            }
            //            else
            //            {

            //                if (xmlENam.Element("mayte") == null)
            //                {
            //                    XElement xmlMaYte = new XElement("mayte", new XElement("KP" + _intMaKP.ToString(), 1));
            //                    xmlENam.Add(xmlMaYte);
            //                    xmlENam.Save(fileXML);
            //                    _intMaYTe = 1;
            //                }
            //                else // đã tồn tại
            //                {
            //                    if (xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()) == null)
            //                    {
            //                        XElement xmlMaYte = new XElement("KP" + _intMaKP.ToString(), 1);
            //                        xmlENam.Element("mayte").Add(xmlMaYte);
            //                        xmlENam.Save(fileXML);
            //                        _intMaYTe = 1;
            //                    }
            //                    else
            //                        _intMaYTe = Convert.ToInt32(xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()).Value);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            XElement xmlE = new XElement("str" + DateTime.Now.Year.ToString());
            //            xmlE.Add(new XElement("sohoso", 1));
            //            if (_intMaKP > 0)
            //            {
            //                XElement xmlMaYte = new XElement("mayte", new XElement("KP" + _intMaKP.ToString(), 1));
            //                xmlE.Add(xmlMaYte);
            //                xmlE.Save(fileXML);
            //            }
            //            _intMaYTe = 1;
            //            _intSoHoSo = 1;
            //        }
            //    }
            //    else
            //    {
            //        // số hồ sơ

            //        if (grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong) != null)
            //        {
            //            _makp = Convert.ToInt32(grv_DsBenhNhan.GetFocusedRowCellValue(colKhoaPhong));
            //        }
            //        if(DungChung.Bien.PP_SoLT==1)
            //        {
            //            _intSoHoSo = DungChung.Ham.GetSoPL(7, _makp, noingoaitru);
            //        }
            //        else if (DungChung.Bien.PP_SoLT == 2)
            //        {
            //            _intSoHoSo = DungChung.Ham.GetSoPL(7, 0, noingoaitru);
            //        }
            //        //var qsohoso = data.SoPLs.Where(p => p.PhanLoai == 7 && p.MaKP == 0).Where(p => _ploaiNoiNgoaiTru == 1 && p.NoiTru == noingoaitru).OrderByDescending(p => p.NgayNhap).FirstOrDefault();
            //        //if (qsohoso != null)
            //        //{
            //        //    //SoPL moi = qsohoso;
            //        //    _intSoHoSo = qsohoso.SoPL1 + 1;

            //        //}
            //        //else
            //        //{
            //        //    _intSoHoSo = 1;

            //        //}
            //        // mã y tế
            //        if (xmlENam != null)
            //        {                                                    

            //            if (_intMaKP == 0)
            //            {
            //                _intMaYTe = -1;
            //            }
            //            else
            //            {

            //                if (xmlENam.Element("mayte") == null)
            //                {
            //                    XElement xmlMaYte = new XElement("mayte", new XElement("KP" + _intMaKP.ToString(), 1));
            //                    xmlENam.Add(xmlMaYte);
            //                    xmlENam.Save(fileXML);
            //                    _intMaYTe = 1;
            //                }
            //                else // đã tồn tại
            //                {
            //                    if (xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()) == null)
            //                    {
            //                        XElement xmlMaYte = new XElement("KP" + _intMaKP.ToString(), 1);
            //                        xmlENam.Element("mayte").Add(xmlMaYte);
            //                        xmlENam.Save(fileXML);
            //                        _intMaYTe = 1;
            //                    }
            //                    else
            //                        _intMaYTe = Convert.ToInt32(xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()).Value);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            XElement xmlE = new XElement("str" + DateTime.Now.Year.ToString());
            //            xmlE.Add(new XElement("sohoso", 1));
            //            if (_intMaKP > 0)
            //            {
            //                XElement xmlMaYte = new XElement("mayte", new XElement("KP" + _intMaKP.ToString(), 1));
            //                xmlE.Add(xmlMaYte);
            //                xmlE.Save(fileXML);
            //            }
            //            _intMaYTe = 1;                           
            //        }
            //    }

            //    //else
            //    //{
            //    //    var qsohoso = data.SoPLs.Where(p => p.PhanLoai == 7 && p.MaKP == 0).Where(p => _ploaiNoiNgoaiTru == -1 || (_ploaiNoiNgoaiTru == 1 && p.NoiTru == _noitru)).OrderByDescending(p => p.NgayNhap).FirstOrDefault();
            //    //    if (qsohoso != null)
            //    //    {
            //    //        SoPL moi = qsohoso;
            //    //        _intSoHoSo = qsohoso.SoPL1 + 1;
            //    //        data.Remove(qsohoso);
            //    //        data.SaveChanges();
            //    //        moi.SoPL1 = _intSoHoSo;
            //    //        data.SoPLs.Add(moi);
            //    //        data.SaveChanges();
            //    //    }
            //    //    else
            //    //    {
            //    //        _intSoHoSo = 1;
            //    //        SoPL moi = new SoPL();
            //    //        moi.MaKP = 0;
            //    //        moi.SoPL1 = 1;
            //    //        moi.PhanLoai = 7;
            //    //        moi.NoiTru = _noitru;
            //    //        moi.NgayNhap = DateTime.Now;
            //    //        data.SoPLs.Add(moi);
            //    //        data.SaveChanges();
            //    //    }
            //    //}
            //    //if (_strsoluutru.Trim() == "")
            //    if (String.IsNullOrEmpty(txtSoHoSo2.Text.Trim()))
            //        txtSoHoSo2.Text = _intSoHoSo.ToString();
            //    //if (_strMaYTe == "" && _intMaYTe != -1)
            //    if (String.IsNullOrEmpty(txtMaYTe2.Text.Trim()) && _intMaYTe != -1)
            //        txtMaYTe2.Text = _strStaticMayte + _intMaYTe.ToString("D6");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //#endregion

        }
        /// <summary>
        /// phân loại nội ngoại trú cho số lưu trữ (số hồ sơ): -21: không phân loại theo nội trú hay ngoại trú; (lấy theo xml)
        /// 1: có phân loại nội ngoại  trú
        /// </summary>
        int _ploaiNoiNgoaiTru = -1;


        /// <summary>
        /// 0: ngoại trú; 1: nội trú
        /// </summary>
        int _noitru = -1;
        //private void GetSoHienTai()
        //{
        //    int noingoaitru = -1;
        //    if (DungChung.Bien.MaBV == "12122")
        //        noingoaitru = _noitru;
        //    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    string _fileXML = "";
        //    txtSoHoSoHT.Text = "";
        //    txtMaYTeHT.Text = "";
        //    #region lấy từ fileXML
        //    // kiểm tra xem file XML đã tồn tại hay chưa
        //    XDocument document = new XDocument();
        //    XDocument doc2 = new XDocument();// file XML chứa số hồ sơ, mã y tế từng năm  

        //    try
        //    {
        //        _fileXML = System.IO.Directory.GetCurrentDirectory() + "\\duyetbenhan.xml";
        //        if (File.Exists(_fileXML))
        //        {
        //            doc2 = XDocument.Load(_fileXML);
        //            XElement xmlENam = doc2.Element("str" + DateTime.Now.Year.ToString());

        //            if (_ploaiNoiNgoaiTru == -1)
        //            {
        //                if (xmlENam != null)
        //                {


        //                    txtSoHoSoHT.Text = xmlENam.Element("sohoso") == null ? "0" : (Convert.ToInt32(xmlENam.Element("sohoso").Value) - 1).ToString();
        //                    if (_intMaKP > 0 && xmlENam.Element("mayte") != null && xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()) != null)
        //                    {
        //                        txtMaYTeHT.Text = _strStaticMayte + (Convert.ToInt32(xmlENam.Element("mayte").Element("KP" + _intMaKP.ToString()).Value) - 1).ToString("D6");
        //                    }
        //                    else
        //                        txtMaYTeHT.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                var qsoho = data.SoPLs.Where(p => p.PhanLoai == 7 && p.MaKP == 0).Where(p=>_ploaiNoiNgoaiTru == -1 || (_ploaiNoiNgoaiTru == 1 && p.NoiTru == noingoaitru)).OrderByDescending(p => p.NgayNhap).ToList();
        //                if (qsoho == null)
        //                    txtSoHoSoHT.Text = "0";
        //                else
        //                {
        //                    txtSoHoSoHT.Text = (qsoho.First().SoPL1 - 1).ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    #endregion
        //}

        List<MaYTe> _lMaYTe = new List<MaYTe>();
        private void uc_DuyetBenhAn_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                colSoHS.Caption = "Số hồ sơ";
                btnSearch.Visible = true;
            }
        }
        public static List<MaYTe> SetMaYTe()
        {
            List<MaYTe> _lmayt = new List<MaYTe>();
            _lmayt.Add(new MaYTe { MaBV = "30009", MaYTe1 = "107/519/" + DateTime.Now.ToString("yy") + "/" });
            _lmayt.Add(new MaYTe { MaBV = "30003", MaYTe1 = "107/057/" + DateTime.Now.ToString("yy") + "/" });
            return _lmayt;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DungChung.Ham.checkQuyen(this.Name)[1])
            {
                if (txtMaBN2.Text.Trim() != "")
                {
                    btnLuu.Enabled = true;
                    ckTieuDuong.Properties.ReadOnly = false;
                    ckTangHuyetAp.Properties.ReadOnly = false;
                }
                else
                    btnLuu.Enabled = false;
            }
            else
            {
                MessageBox.Show("Chức năng bị giới hạn");
            }
        }

        private void grv_DsBenhNhan_DataSourceChanged(object sender, EventArgs e)
        {
            grv_DsBenhNhan_FocusedRowChanged(null, null);
        }

        private void btnSua_EnabledChanged(object sender, EventArgs e)
        {
            //btnLaySoTuDong.Enabled = !btnSua.Enabled;
            //txtSoHoSo2.Properties.ReadOnly = btnSua.Enabled;
            //txtMaYTe2.Properties.ReadOnly = btnSua.Enabled;
            //btnSetSoHS.Enabled = !btnSua.Enabled;
            //btnSetMaYTe.Enabled = !btnSua.Enabled;
            //btnLuu.Enabled = !btnSua.Enabled;

        }

        private void txtMaBN2_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtMaBN2.Text.Trim() != "")
            //{
            //    btnSua.Enabled = true;
            //}
            //else
            //{
            //    btnSua.Enabled = false;
            //}
        }

        private void btnLuu_EnabledChanged(object sender, EventArgs e)
        {
            //if (btnLuu.Enabled)
            //    btnSua.Enabled = false;
        }

        private void btnHuyLaySo_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN2.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN2.Text);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool huy = true;
            huy = DungChung.Ham.checkQuyen(this.Name)[2];
            if (!DungChung.Ham.checkQuyen(this.Name)[2])
            {

                MessageBox.Show("Chức năng bị giới hạn");
            }
            if (_int_maBN > 0 && huy)
            {
                List<RaVien> _lrv = data.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                if (_lrv.Count > 0)
                {
                    RaVien rv = _lrv.First();
                    if (rv.SoLT != null && rv.SoLT.ToString().Trim() != "")
                    {
                        DialogResult dr = MessageBox.Show("Bạn có muốn hủy duyệt hồ sơ", "Hủy duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            rv.SoLT = null;
                            rv.MaYTe = null;
                            data.SaveChanges();
                            //List<VaoVien> _lvv = data.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                            //if (_lvv.Count > 0)
                            //{
                            //    foreach (var a in _lvv)
                            //    {
                            //        a.SoBA = "";
                            //        data.SaveChanges();
                            //    }
                            //}
                            if (DungChung.Bien.MaBV == "26007")
                            {
                                var Ktrabn = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                                if (Ktrabn.Count > 0)
                                {
                                    string sthe = Ktrabn.First().SThe.Trim();
                                    List<Person> q1 = data.People.Where(p => p.SThe.Contains(sthe)).ToList();
                                    if (q1.Count > 0)
                                    {
                                        Person p1 = q1.First();
                                        p1.GhiChu = null;
                                        data.SaveChanges();
                                    }
                                }
                            }
                            loadDSBenhNhan();
                        }
                    }
                    else
                    {
                        txtSoHoSo2.Text = "";
                        txtMaYTe2.Text = "";
                    }
                }

            }

        }

        private void lblKP_TextChanged(object sender, EventArgs e)
        {
            //GetSoHienTai();
        }

        private void txtSoHoSo2_TextChanged(object sender, EventArgs e)
        {
            if (txtSoHoSo2.Text.Trim() != "")
            {
                comboBoxEdit1.Enabled = true;
            }
            else
                comboBoxEdit1.Enabled = false;

        }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                loadDSBenhNhan();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }





        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV != "24012")
                loadDSBenhNhan();
        }

        private void ckTieuDuong_CheckedChanged(object sender, EventArgs e)
        {
            int _mabn = String.IsNullOrEmpty(txtMaBN2.Text) ? 0 : Convert.ToInt32(txtMaBN2.Text);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            string dtuong = "";
            dtuong = _lbn.First().DTuong.ToLower();
            if (ckTieuDuong.Checked && dtuong == "dịch vụ")
            {
                txtsohsbnmantinh.Text = _mabn.ToString();
            }
            else
                if (ckTangHuyetAp.Checked == false)
                {
                    txtsohsbnmantinh.Text = "";
                }
        }

        private void ckTangHuyetAp_CheckedChanged(object sender, EventArgs e)
        {
            int _mabn = String.IsNullOrEmpty(txtMaBN2.Text) ? 0 : Convert.ToInt32(txtMaBN2.Text);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            string dtuong = "";
            dtuong = _lbn.First().DTuong.ToLower();
            if (ckTangHuyetAp.Checked && dtuong == "dịch vụ")
            {
                txtsohsbnmantinh.Text = _mabn.ToString();
            }
            else
                if (ckTieuDuong.Checked == false)
                {
                    txtsohsbnmantinh.Text = "";
                }
        }

        private void rgchonduyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                loadDSBenhNhan();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN2.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN2.Text);
            if (comboBoxEdit1.Text == "Hủy duyệt")
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                bool huy = true;
                huy = DungChung.Ham.checkQuyen(this.Name)[2];
                if (!DungChung.Ham.checkQuyen(this.Name)[2])
                {

                    MessageBox.Show("Chức năng bị giới hạn");
                }
                if (_int_maBN > 0 && huy)
                {
                    List<RaVien> _lrv = data.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                    if (_lrv.Count > 0)
                    {
                        RaVien rv = _lrv.First();
                        if (rv.SoLT != null && rv.SoLT.ToString().Trim() != "")
                        {
                            DialogResult dr = MessageBox.Show("Bạn có muốn hủy duyệt hồ sơ", "Hủy duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                rv.SoLT = null;
                                rv.MaYTe = null;
                                data.SaveChanges();
                                //List<VaoVien> _lvv = data.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                //if (_lvv.Count > 0)
                                //{
                                //    foreach (var a in _lvv)
                                //    {
                                //        a.SoBA = "";
                                //        data.SaveChanges();
                                //    }
                                //}
                                if (DungChung.Bien.MaBV == "26007")
                                {
                                    var Ktrabn = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                                    if (Ktrabn.Count > 0)
                                    {
                                        string sthe = Ktrabn.First().SThe.Trim();
                                        List<Person> q1 = data.People.Where(p => p.SThe.Contains(sthe)).ToList();
                                        if (q1.Count > 0)
                                        {
                                            Person p1 = q1.First();
                                            p1.GhiChu = null;
                                            data.SaveChanges();
                                        }
                                    }
                                }
                                loadDSBenhNhan();
                            }
                        }
                        else
                        {
                            txtSoHoSo2.Text = "";
                            txtMaYTe2.Text = "";
                        }
                    }
                }
            }
            else if (comboBoxEdit1.Text == "Sao bệnh án")
            {
                FormThamSo.frm_SaoBenhAn frm = new FormThamSo.frm_SaoBenhAn(_int_maBN);
                frm.ShowDialog();
            }
            comboBoxEdit1.Text = "--Chọn--";
        }

        private void comboBoxEdit1_DoubleClick(object sender, EventArgs e)
        {
            //int rs;
            //int _int_maBN = 0;
            //if (Int32.TryParse(txtMaBN2.Text, out rs))
            //    _int_maBN = Convert.ToInt32(txtMaBN2.Text);
            //if (comboBoxEdit1.Text == "Sao bệnh án")
            //{
            //    FormThamSo.frm_SaoBenhAn frm = new FormThamSo.frm_SaoBenhAn(_int_maBN);
            //    frm.ShowDialog();
            //}
        }

        private void txt_MaYte_TK_EditValueChanged(object sender, EventArgs e)
        {
            if (!load)
                loadDSBenhNhan();
        }

        private void txtSoHoSo_EditValueChanged(object sender, EventArgs e)
        {
            if (!load)
                loadDSBenhNhan();
        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtkp1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private class DSBN
        {
            public int MaBNhan { get; set; }
            public string SoLT { get; set; }
            public string HoTen { get; set; }
            public string GTinh { get; set; }
            public DateTime? NgayVao { get; set; }
            public DateTime? NgayRa { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
            public int? Tuoi { get; set; }
            public int Status { get; set; }
        }

        private void ckcBNChuyen_CheckedChanged(object sender, EventArgs e)
        {
            loadDSBenhNhan();
        }

        private void txtSoHoSo2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadDSBenhNhan();
        }
    }
}
