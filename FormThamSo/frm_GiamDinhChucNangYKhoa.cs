using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_GiamDinhChucNangYKhoa : Form
    {
        int _mabn = 0;
        public frm_GiamDinhChucNangYKhoa(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void cbx_MaGiamDinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbx_MaGiamDinh.SelectedIndex == 5 || cbx_MaGiamDinh.SelectedIndex == 6 || cbx_MaGiamDinh.SelectedIndex == 7)
            {
                txtSumTyLeTonThuong.Enabled = true;
            }  
            else
                txtSumTyLeTonThuong.Enabled = false;
        }

        private void chk_c7_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_c7.Checked)
            {
                cbx_DangKhuyetTat.Enabled = true;
                cbx_MucDoKhuyetTat.Enabled = true;
            }    
            else
            {
                cbx_DangKhuyetTat.Enabled = false;
                cbx_MucDoKhuyetTat.Enabled = false;
            }    
        }
        class LSGDYK
        {
            public string DT { get; set; }
        }
        List<LSGDYK> _lst = new List<LSGDYK>();
        private void frm_GiamDinhChucNangYKhoa_Load(object sender, EventArgs e)
        {
            var gdcd = _data.GiamDinhYKhoas.Where(p => p.MaBNhan == _mabn).ToList();
            var ttbx = (from ttbxs in _data.TTboXungs.Where(p => p.MaBNhan == _mabn)
                        join bn in _data.BenhNhans on ttbxs.MaBNhan equals bn.MaBNhan
                        select new
                        {
                            bn.TenBNhan,
                            bn.NgaySinh,
                            bn.NamSinh,
                            bn.ThangSinh,
                            bn.SThe,
                            bn.DChi,
                            ttbxs.MaTinh,
                            ttbxs.MaHuyen,
                            ttbxs.MaXa,
                            ttbxs.SoKSinh,
                            ttbxs.NoiCapCMT,
                            ttbxs.NgayCapCMT,
                            ttbxs.NgheNghiep,
                            ttbxs.DThoai,
                        }).ToList();
            if(ttbx.Count > 0)
            {
                txtTenBN.Text = ttbx.First().TenBNhan;
                txtNgaySinh.Text = ttbx.First().NgaySinh;
                txtThangSinh.Text = ttbx.First().ThangSinh;
                txtNamSinh.Text = ttbx.First().NamSinh;
                txtBHXH.Text = ttbx.First().SThe.Substring(5);
                txtBHYT.Text = ttbx.First().SThe;
                txtDiaChi.Text = ttbx.First().DChi;
                txtMaTinh.Text = ttbx.First().MaTinh;
                txtMaHuyen.Text = ttbx.First().MaHuyen;
                txtMaXa.Text = ttbx.First().MaXa;
                txtCCCD.Text = ttbx.First().SoKSinh;
                txtNoiCap.Text = ttbx.First().NoiCapCMT;
                if (ttbx.First().NgayCapCMT != null)
                {
                    dtNgayCap.DateTime = ttbx.First().NgayCapCMT.Value;
                }
                txtNgheNghiep.Text = ttbx.First().NgheNghiep;
                txtDienThoai.Text = ttbx.First().DThoai;
            }    
            if(gdcd.Count() > 0)
            {
                panelControl3.Enabled = false;
                btnLuu.Enabled = false;
                txtNguoiChuTri.Text = gdcd.First().NGUOI_CHU_TRI;
                txtSoTT.Text = gdcd.First().SO_BIEN_BAN;
                txtSoGioiThieu.Text = gdcd.First().SO_GIAY_GIOI_THIEU;
                txtTyLeTonThuong.Text = Convert.ToString(gdcd.First().TYLE_TTCT_CU);
                txtMaDV.Text = DungChung.Bien.MaBV;
                txtDVGioiThieu.Text = gdcd.First().GIOI_THIEU_CUA;
                mmKetQua.Text = gdcd.First().KET_QUA_KHAM;
                txtSoVBCu.Text = gdcd.First().SO_VAN_BAN_CAN_CU;
                txtTyLeTonThuongNew.Text = Convert.ToString(gdcd.First().TYLE_TTCT_MOI);
                txtSumTyLeTonThuong.Text = Convert.ToString(gdcd.First().TONG_TYLE_TTCT);
                mmDeNghi.Text = gdcd.First().DE_NGHI;
                mmDuocXacDinh.Text = gdcd.First().DUOC_XACDINH;
                if(gdcd.First().NGAY_HOP != null)
                {
                    dtNgayHop.DateTime = gdcd.First().NGAY_HOP.Value;
                }
                if(gdcd.First().NGAY_CHUNG_TU != null)
                {
                    dtNgayChungTu.DateTime = gdcd.First().NGAY_CHUNG_TU.Value;
                }    
                if(gdcd.First().NGAY_DE_NGHI != null)
                {
                    dtNgayDeNghi.DateTime = gdcd.First().NGAY_DE_NGHI.Value;
                }
                #region Mức độ khuyết tật
                if(gdcd.First().MUC_DO_KHUYETTAT == 1)
                {
                    cbx_MucDoKhuyetTat.SelectedIndex = 0;
                }    
                else if(gdcd.First().MUC_DO_KHUYETTAT == 2)
                {
                    cbx_MucDoKhuyetTat.SelectedIndex = 1;
                }
                else if(gdcd.First().MUC_DO_KHUYETTAT == 3)
                {
                    cbx_MucDoKhuyetTat.SelectedIndex = 2;
                }
                else if (gdcd.First().MUC_DO_KHUYETTAT == 4)
                {
                    cbx_MucDoKhuyetTat.SelectedIndex = 3;
                }
                #endregion
                #region Dạng khuyết tật
                if (gdcd.First().DANG_KHUYETTAT == 1)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 0;
                }    
                else if(gdcd.First().DANG_KHUYETTAT == 2)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 1;
                }
                else if (gdcd.First().DANG_KHUYETTAT == 3)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 2;
                }
                else if (gdcd.First().DANG_KHUYETTAT == 4)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 3;
                }
                else if (gdcd.First().DANG_KHUYETTAT == 5)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 4;
                }
                else if (gdcd.First().DANG_KHUYETTAT == 6)
                {
                    cbx_DangKhuyetTat.SelectedIndex = 5;
                }
                #endregion
                #region Đang hưởng chế độ
                if (gdcd.First().DANG_HUONG_CHE_DO == 1)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 0;
                }    
                else if(gdcd.First().DANG_HUONG_CHE_DO == 2)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 1;
                }
                else if (gdcd.First().DANG_HUONG_CHE_DO == 3)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 2;
                }
                else if (gdcd.First().DANG_HUONG_CHE_DO == 4)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 3;
                }
                else if (gdcd.First().DANG_HUONG_CHE_DO == 5)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 4;
                }
                else if (gdcd.First().DANG_HUONG_CHE_DO == 6)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 5;
                }
                else if (gdcd.First().DANG_HUONG_CHE_DO == 7)
                {
                    cbx_MaHuongCheDo.SelectedIndex = 6;
                }
                #endregion
                #region Khám giám định
                if (gdcd.First().KHAM_GIAM_DINH == 1)
                {
                    cbx_MaGiamDinh.SelectedIndex = 0;
                }    
                else if (gdcd.First().KHAM_GIAM_DINH == 2)
                {
                    cbx_MaGiamDinh.SelectedIndex = 1;
                }
                else if (gdcd.First().KHAM_GIAM_DINH == 3)
                {
                    cbx_MaGiamDinh.SelectedIndex = 2;
                }
                else if (gdcd.First().KHAM_GIAM_DINH == 4)
                {
                    cbx_MaGiamDinh.SelectedIndex = 3;
                }
                else if (gdcd.First().KHAM_GIAM_DINH == 5)
                {
                    cbx_MaGiamDinh.SelectedIndex = 4;
                }
                else if (gdcd.First().KHAM_GIAM_DINH == 6)
                {
                    cbx_MaGiamDinh.SelectedIndex = 5;
                }
                else if (gdcd.First().KHAM_GIAM_DINH == 7)
                {
                    cbx_MaGiamDinh.SelectedIndex = 6;
                }
                #endregion
                #region Đối tượng
                List<string> GDYK = gdcd.First().MA_DOI_TUONG.Split(';').ToList();
                foreach (var ab in GDYK)
                {
                    LSGDYK item = new LSGDYK();
                    item.DT = ab.ToString();
                    _lst.Add(item);
                    if (ab.ToString() == "BB")
                    {
                        chk_c1.Checked = true;
                    }
                    if (ab.ToString() == "BHXH1L")
                    {
                        chk_c2.Checked = true;
                    }
                    if (ab.ToString() == "BNN")
                    {
                        chk_c3.Checked = true;
                    }
                    if (ab.ToString() == "CĐHH")
                    {
                        chk_c4.Checked = true;
                    }
                    if (ab.ToString() == "KNLĐH")
                    {
                        chk_c5.Checked = true;
                    }
                    if (ab.ToString() == "KNLĐT")
                    {
                        chk_c6.Checked = true;
                    }
                    if (ab.ToString() == "NKT")
                    {
                        chk_c7.Checked = true;
                    }
                    if (ab.ToString() == "NVQS")
                    {
                        chk_c8.Checked = true;
                    }
                    if (ab.ToString() == "TB")
                    {
                        chk_c9.Checked = true;
                    }
                    if (ab.ToString() == "TH")
                    {
                        chk_c10.Checked = true;
                    }
                    if (ab.ToString() == "TNLĐ")
                    {
                        chk_c11.Checked = true;
                    }
                }
                #endregion
                #region chức vụ
                if (gdcd.First().CHUC_VU == 1)
                {
                    cbx_ChucVu.SelectedIndex = 0;
                }    
                else if(gdcd.First().CHUC_VU == 2)
                {
                    cbx_ChucVu.SelectedIndex = 1;
                }
                #endregion
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var gdcd = _data.GiamDinhYKhoas.Where(p => p.MaBNhan == _mabn).ToList();
            var ttbx = (from ttbxs in _data.TTboXungs.Where(p => p.MaBNhan == _mabn)
                        join bn in _data.BenhNhans on ttbxs.MaBNhan equals bn.MaBNhan
                        select new
                        {}).ToList();
            #region Sửa
            if (gdcd.Count > 0)
            {
                if (ttbx.Count > 0)
                {
                    TTboXung ttbn = _data.TTboXungs.Single(p => p.MaBNhan == _mabn);
                    BenhNhan bn = _data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                    ttbn.SoKSinh = txtCCCD.Text;
                    ttbn.NgayCapCMT = dtNgayCap.DateTime;
                    ttbn.NoiCapCMT = txtNoiCap.Text;
                    ttbn.MaTinh = txtMaTinh.Text;
                    ttbn.MaHuyen = txtMaHuyen.Text;
                    ttbn.MaXa = txtMaXa.Text;
                    ttbn.NgheNghiep = txtNgheNghiep.Text;
                    ttbn.DThoai = txtDienThoai.Text;
                    bn.NgaySinh = txtNgaySinh.Text;
                    bn.ThangSinh = txtThangSinh.Text;
                    bn.NamSinh = txtNamSinh.Text;
                    bn.DChi = txtDiaChi.Text;
                }
                GiamDinhYKhoa ykhoa = _data.GiamDinhYKhoas.Single(p => p.MaBNhan == _mabn);
                ykhoa.NGUOI_CHU_TRI = txtNguoiChuTri.Text;
                ykhoa.NGAY_HOP = dtNgayHop.DateTime;
                ykhoa.SO_BIEN_BAN = txtSoTT.Text;
                ykhoa.TYLE_TTCT_CU = Convert.ToInt32(txtTyLeTonThuong.Text);
                ykhoa.NGAY_CHUNG_TU = dtNgayChungTu.DateTime;
                ykhoa.SO_GIAY_GIOI_THIEU = txtSoGioiThieu.Text;
                ykhoa.NGAY_DE_NGHI = dtNgayDeNghi.DateTime;
                ykhoa.GIOI_THIEU_CUA = txtDVGioiThieu.Text;
                ykhoa.KET_QUA_KHAM = mmKetQua.Text;
                ykhoa.SO_VAN_BAN_CAN_CU = txtSoVBCu.Text;
                ykhoa.TYLE_TTCT_MOI = Convert.ToInt32(txtTyLeTonThuongNew.Text);
                ykhoa.TONG_TYLE_TTCT = Convert.ToInt32(txtSumTyLeTonThuong.Text);
                ykhoa.DE_NGHI = mmDeNghi.Text;
                ykhoa.DUOC_XACDINH = mmDuocXacDinh.Text;
                #region Mức độ khuyết tật
                if(cbx_MucDoKhuyetTat.SelectedIndex == 0)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 1;
                }    
                else if(cbx_MucDoKhuyetTat.SelectedIndex == 1)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 2;
                }    
                else if(cbx_MucDoKhuyetTat.SelectedIndex == 2)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 3;
                }    
                else if(cbx_MucDoKhuyetTat.SelectedIndex == 3)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 4;
                }    
                #endregion
                #region Dạng khuyết tật
                if (cbx_DangKhuyetTat.SelectedIndex == 0)
                {
                    ykhoa.DANG_KHUYETTAT = 1;
                }    
                else if(cbx_DangKhuyetTat.SelectedIndex == 1)
                {
                    ykhoa.DANG_KHUYETTAT = 2;
                }    
                else if(cbx_DangKhuyetTat.SelectedIndex == 2)
                {
                    ykhoa.DANG_KHUYETTAT = 3;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 3)
                {
                    ykhoa.DANG_KHUYETTAT = 4;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 4)
                {
                    ykhoa.DANG_KHUYETTAT = 5;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 5)
                {
                    ykhoa.DANG_KHUYETTAT = 6;
                }
                #endregion
                #region Hưởng chế độ
                if (cbx_MaHuongCheDo.SelectedIndex == 0)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 1;
                }    
                else if(cbx_MaHuongCheDo.SelectedIndex == 1)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 2;
                }    
                else if(cbx_MaHuongCheDo.SelectedIndex == 2)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 3;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 3)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 4;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 4)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 5;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 5)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 6;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 6)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 7;
                }
                #endregion
                #region Giám định
                if (cbx_MaGiamDinh.SelectedIndex == 0)
                {
                    ykhoa.KHAM_GIAM_DINH = 1;
                }    
                else if(cbx_MaGiamDinh.SelectedIndex == 1)
                {
                    ykhoa.KHAM_GIAM_DINH = 2;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 2)
                {
                    ykhoa.KHAM_GIAM_DINH = 3;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 3)
                {
                    ykhoa.KHAM_GIAM_DINH = 4;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 4)
                {
                    ykhoa.KHAM_GIAM_DINH = 5;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 5)
                {
                    ykhoa.KHAM_GIAM_DINH = 6;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 6)
                {
                    ykhoa.KHAM_GIAM_DINH = 7;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 7)
                {
                    ykhoa.KHAM_GIAM_DINH = 8;
                }
                #endregion
                #region Chức vụ
                if (cbx_ChucVu.SelectedIndex == 0)
                {
                    ykhoa.CHUC_VU = 1;
                }
                else if (cbx_ChucVu.SelectedIndex == 1)
                {
                    ykhoa.CHUC_VU = 2;
                }
                #endregion
                #region Đối tượng
                string chuoi = ""; string chuoi1 = ""; string chuoi2 = ""; string chuoi3 = ""; string chuoi4 = ""; string chuoi5 = ""; string chuoi6 = ""; string chuoi7 = ""; string chuoi8 = ""; string chuoi9 = ""; string chuoi10 = ""; string chuoi11 = "";
                if (chk_c1.Checked)
                {
                    chuoi1 = "BB" + ";";
                }
                if (chk_c2.Checked)
                {
                    chuoi2 = "BHXH1L" + ";";
                }
                if (chk_c3.Checked)
                {
                    chuoi3 = "BNN" + ";";
                }
                if (chk_c4.Checked)
                {
                    chuoi4 = "CĐHH:" + ";";
                }
                if (chk_c5.Checked)
                {
                    chuoi5 = "KNLĐH" + ";";
                }
                if (chk_c6.Checked)
                {
                    chuoi6 = "KNLĐT" + ";";
                }
                if (chk_c7.Checked)
                {
                    chuoi7 = "NKT" + ";";
                }
                if (chk_c8.Checked)
                {
                    chuoi8 = "NVQS" + ";";
                }
                if (chk_c9.Checked)
                {
                    chuoi9 = "TB" + ";";
                }
                if (chk_c10.Checked)
                {
                    chuoi10 = "TH" + ";";
                }
                if (chk_c11.Checked)
                {
                    chuoi11 = "TNLĐ" + ";";
                }
                chuoi = chuoi1 + chuoi2 + chuoi3 + chuoi4 + chuoi5 + chuoi6 + chuoi7 + chuoi8 + chuoi9 + chuoi10 + chuoi11;
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Remove(chuoi.Length - 1);
                }
                ykhoa.MA_DOI_TUONG = chuoi;
                #endregion
                _data.SaveChanges();
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Sửa thông tin thành công");
                    frm_GiamDinhChucNangYKhoa_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Sửa thông tin Thất bại");
                    return;
                }
            }
            #endregion
            #region Thêm mới
            else
            {
                if (ttbx.Count > 0)
                {
                    TTboXung ttbn = _data.TTboXungs.Single(p => p.MaBNhan == _mabn);
                    BenhNhan bn = _data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                    ttbn.SoKSinh = txtCCCD.Text;
                    ttbn.NgayCapCMT = dtNgayCap.DateTime;
                    ttbn.NoiCapCMT = txtNoiCap.Text;
                    ttbn.MaTinh = txtMaTinh.Text;
                    ttbn.MaHuyen = txtMaHuyen.Text;
                    ttbn.MaXa = txtMaXa.Text;
                    ttbn.NgheNghiep = txtNgheNghiep.Text;
                    ttbn.DThoai = txtDienThoai.Text;
                    bn.NgaySinh = txtNgaySinh.Text;
                    bn.ThangSinh = txtThangSinh.Text;
                    bn.NamSinh = txtNamSinh.Text;
                    bn.DChi = txtDiaChi.Text;
                    _data.SaveChanges();
                }
                GiamDinhYKhoa ykhoa = new GiamDinhYKhoa();
                ykhoa.MaBNhan = _mabn;
                ykhoa.NGUOI_CHU_TRI = txtNguoiChuTri.Text;
                ykhoa.NGAY_HOP = dtNgayHop.DateTime;
                ykhoa.SO_BIEN_BAN = txtSoTT.Text;
                ykhoa.TYLE_TTCT_CU = Convert.ToInt32(txtTyLeTonThuong.Text);
                ykhoa.NGAY_CHUNG_TU = dtNgayChungTu.DateTime;
                ykhoa.SO_GIAY_GIOI_THIEU = txtSoGioiThieu.Text;
                ykhoa.NGAY_DE_NGHI = dtNgayDeNghi.DateTime;
                ykhoa.GIOI_THIEU_CUA = txtDVGioiThieu.Text;
                ykhoa.KET_QUA_KHAM = mmKetQua.Text;
                ykhoa.SO_VAN_BAN_CAN_CU = txtSoVBCu.Text;
                ykhoa.TYLE_TTCT_MOI = Convert.ToInt32(txtTyLeTonThuongNew.Text);
                ykhoa.TONG_TYLE_TTCT = Convert.ToInt32(txtSumTyLeTonThuong.Text);
                ykhoa.DE_NGHI = mmDeNghi.Text;
                ykhoa.DUOC_XACDINH = mmDuocXacDinh.Text;
                #region Mức độ khuyết tật
                if (cbx_MucDoKhuyetTat.SelectedIndex == 0)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 1;
                }
                else if (cbx_MucDoKhuyetTat.SelectedIndex == 1)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 2;
                }
                else if (cbx_MucDoKhuyetTat.SelectedIndex == 2)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 3;
                }
                else if (cbx_MucDoKhuyetTat.SelectedIndex == 3)
                {
                    ykhoa.MUC_DO_KHUYETTAT = 4;
                }
                #endregion
                #region Dạng khuyết tật
                if (cbx_DangKhuyetTat.SelectedIndex == 0)
                {
                    ykhoa.DANG_KHUYETTAT = 1;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 1)
                {
                    ykhoa.DANG_KHUYETTAT = 2;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 2)
                {
                    ykhoa.DANG_KHUYETTAT = 3;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 3)
                {
                    ykhoa.DANG_KHUYETTAT = 4;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 4)
                {
                    ykhoa.DANG_KHUYETTAT = 5;
                }
                else if (cbx_DangKhuyetTat.SelectedIndex == 5)
                {
                    ykhoa.DANG_KHUYETTAT = 6;
                }
                #endregion
                #region Hưởng chế độ
                if (cbx_MaHuongCheDo.SelectedIndex == 0)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 1;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 1)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 2;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 2)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 3;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 3)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 4;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 4)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 5;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 5)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 6;
                }
                else if (cbx_MaHuongCheDo.SelectedIndex == 6)
                {
                    ykhoa.DANG_HUONG_CHE_DO = 7;
                }
                #endregion
                #region Giám định
                if (cbx_MaGiamDinh.SelectedIndex == 0)
                {
                    ykhoa.KHAM_GIAM_DINH = 1;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 1)
                {
                    ykhoa.KHAM_GIAM_DINH = 2;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 2)
                {
                    ykhoa.KHAM_GIAM_DINH = 3;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 3)
                {
                    ykhoa.KHAM_GIAM_DINH = 4;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 4)
                {
                    ykhoa.KHAM_GIAM_DINH = 5;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 5)
                {
                    ykhoa.KHAM_GIAM_DINH = 6;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 6)
                {
                    ykhoa.KHAM_GIAM_DINH = 7;
                }
                else if (cbx_MaGiamDinh.SelectedIndex == 7)
                {
                    ykhoa.KHAM_GIAM_DINH = 8;
                }
                #endregion
                #region Chức vụ
                if (cbx_ChucVu.SelectedIndex == 0)
                {
                    ykhoa.CHUC_VU = 1;
                }
                else if (cbx_ChucVu.SelectedIndex == 1)
                {
                    ykhoa.CHUC_VU = 2;
                }
                #endregion
                #region Đối tượng
                string chuoi = ""; string chuoi1 = ""; string chuoi2 = ""; string chuoi3 = ""; string chuoi4 = ""; string chuoi5 = ""; string chuoi6 = ""; string chuoi7 = ""; string chuoi8 = ""; string chuoi9 = ""; string chuoi10 = ""; string chuoi11 = "";
                if (chk_c1.Checked)
                {
                    chuoi1 = "BB" + ";";
                }
                if (chk_c2.Checked)
                {
                    chuoi2 = "BHXH1L" + ";";
                }
                if (chk_c3.Checked)
                {
                    chuoi3 = "BNN" + ";";
                }
                if (chk_c4.Checked)
                {
                    chuoi4 = "CĐHH:" + ";";
                }
                if (chk_c5.Checked)
                {
                    chuoi5 = "KNLĐH" + ";";
                }
                if (chk_c6.Checked)
                {
                    chuoi6 = "KNLĐT" + ";";
                }
                if (chk_c7.Checked)
                {
                    chuoi7 = "NKT" + ";";
                }
                if (chk_c8.Checked)
                {
                    chuoi8 = "NVQS" + ";";
                }
                if (chk_c9.Checked)
                {
                    chuoi9 = "TB" + ";";
                }
                if (chk_c10.Checked)
                {
                    chuoi10 = "TH" + ";";
                }
                if (chk_c11.Checked)
                {
                    chuoi11 = "TNLĐ" + ";";
                }
                chuoi = chuoi1 + chuoi2 + chuoi3 + chuoi4 + chuoi5 + chuoi6 + chuoi7 + chuoi8 + chuoi9 + chuoi10 + chuoi11;
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Remove(chuoi.Length - 1);
                }
                ykhoa.MA_DOI_TUONG = chuoi;
                #endregion
                _data.GiamDinhYKhoas.Add(ykhoa);
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Thêm thông tin thành công");
                    frm_GiamDinhChucNangYKhoa_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Thêm thông tin Thất bại");
                    return;
                }
            }
            #endregion
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            panelControl3.Enabled = true;
            btnLuu.Enabled = true;
        }
    }
}
