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
    public partial class frm_ChamSocHIV_AIDS : Form
    {
        int mabn = 0;
        public frm_ChamSocHIV_AIDS(int _mabn)
        {
            mabn = _mabn;
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        class LSHIV
        {
            public string HIV { get; set; }
        }
        List<LSHIV> _lst = new List<LSHIV>();
        private void frm_ChamSocHIV_AIDS_Load(object sender, EventArgs e)
        {
            var ttbn = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == mabn)
                        join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                        select new
                        {
                            bn.TenBNhan,
                            bn.Tuoi,
                            bn.GTinh,
                            bn.SThe,
                            ttbx.SoKSinh,
                        }).ToList();
            var tthiv = (from hiv in _data.BA_HIV.Where(p => p.MaBNhan == mabn)
                         select new
                         {
                             hiv.NGAYKD_HIV,
                             hiv.BDDT_ARV,
                             hiv.MA_PHAC_DO_DIEU_TRI_BD,
                             hiv.MA_BAC_PHAC_DO_BD,
                             hiv.MA_LYDO_DTRI,
                             hiv.LOAI_DTRI_LAO,
                             hiv.PHACDO_DTRI_LAO,
                             hiv.NGAYBD_DTRI_LAO,
                             hiv.NGAYKT_DTRI_LAO,
                             hiv.MA_LYDO_XNTL_VR,
                             hiv.NGAY_XN_TLVR,
                             hiv.KQ_XNTL_VR,
                             hiv.NGAY_KQ_XN_TLVR,
                             hiv.MA_LOAI_BN,
                             hiv.MA_TINH_TRANG_DK,
                             hiv.LAN_XN_PCR,
                             hiv.NGAY_KQ_XN_PCR,
                             hiv.MA_KQ_XN_PCR,
                             hiv.NGAY_NHAN_TT_MANG_THAI,
                             hiv.NGAY_BAT_DAU_DT_CTX,
                             hiv.MA_XU_TRI,
                             hiv.NGAY_BAT_DAU_XU_TRI,
                             hiv.NGAY_KET_THUC_XU_TRI,
                             hiv.MA_PHAC_DO_DIEU_TRI,
                             hiv.MA_BAC_PHAC_DO,
                             hiv.SO_NGAY_CAP_THUOC_ARV,
                             hiv.NGAY_XN_PCR
                         }).ToList();
            if (ttbn.Count > 0)
            {
                txtMaBN.Text = Convert.ToString(mabn);
                if (cbx_LoaiDTLao.SelectedIndex == -1 || cbx_LoaiDTLao.SelectedIndex == 0 || cbx_LoaiDTLao.SelectedIndex == 2 || cbx_LoaiDTLao.SelectedIndex == 3)
                {
                    cbx_PhacDoDTLao.Visible = true;
                }
                else if (cbx_LoaiDTLao.SelectedIndex == 1)
                    cbx_PhacDoDTLao1.Visible = true;
                if (ttbn.First().GTinh == 1)
                {
                    txtGT.Text = "Nam";
                }
                else
                    txtGT.Text = "Nữ";
                txtTenBN.Text = ttbn.First().TenBNhan;
                txtCCCD.Text = ttbn.First().SoKSinh;
                txtBHYT.Text = ttbn.First().SThe;
                txtTuoi.Text = Convert.ToString(ttbn.First().Tuoi);
            }
            if(tthiv.Count > 0)
            {
                panelControl3.Enabled = false;
                btnLuu.Enabled = false;
                if (tthiv.First().NGAYKD_HIV != null)
                {
                    dtNgayKD.DateTime = tthiv.First().NGAYKD_HIV.Value;
                }
                if (tthiv.First().BDDT_ARV != null)
                {
                    dtThoiGianNhanThuoc.DateTime = tthiv.First().BDDT_ARV.Value;
                }
                txtPhacDoDT.Text = tthiv.First().MA_PHAC_DO_DIEU_TRI_BD;
                cbx_BacDT.SelectedIndex = Convert.ToInt32(tthiv.First().MA_BAC_PHAC_DO_BD);
                cbx_LyDoDK.SelectedIndex = Convert.ToInt32(tthiv.First().MA_LYDO_DTRI);
                cbx_LoaiDTLao.SelectedIndex = Convert.ToInt32(tthiv.First().LOAI_DTRI_LAO);
                #region Loại ĐT Lao
                if (tthiv.First().LOAI_DTRI_LAO == 1)
                {
                    if(tthiv.First().PHACDO_DTRI_LAO == 6)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 0;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO == 7)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 1;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO == 8)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 2;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO == 9)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 3;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO == 10)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 4;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO == 11)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 5;
                    }
                    else if (tthiv.First().PHACDO_DTRI_LAO ==12)
                    {
                        cbx_PhacDoDTLao1.SelectedIndex = 6;
                    }
                }    
                else if(tthiv.First().LOAI_DTRI_LAO == 0 || tthiv.First().LOAI_DTRI_LAO == 2 || tthiv.First().LOAI_DTRI_LAO == 3)
                {
                    cbx_PhacDoDTLao.SelectedIndex = Convert.ToInt32(tthiv.First().PHACDO_DTRI_LAO);
                }
                #endregion
                if (tthiv.First().NGAYBD_DTRI_LAO != null)
                {
                    dtNgayVao.DateTime = tthiv.First().NGAYBD_DTRI_LAO.Value;
                }
                if (tthiv.First().NGAYKT_DTRI_LAO != null)
                {
                    dtDenNgay.DateTime = tthiv.First().NGAYKT_DTRI_LAO.Value;
                }
                cbx_KetQuaXN.SelectedIndex = Convert.ToInt32(tthiv.First().MA_LYDO_XNTL_VR);
                if (tthiv.First().NGAY_XN_TLVR != null)
                {
                    dtThoiGianLamXN.DateTime = tthiv.First().NGAY_XN_TLVR.Value;
                }
                cbx_KetQuaXN.SelectedIndex = Convert.ToInt32(tthiv.First().KQ_XNTL_VR);
                if (tthiv.First().NGAY_KQ_XN_TLVR != null)
                {
                    dtThoiGianCoKQ.DateTime = tthiv.First().NGAY_KQ_XN_TLVR.Value;
                }
                cbx_DoiTuong.SelectedIndex = Convert.ToInt32(tthiv.First().MA_LOAI_BN);
                #region Tình trạng bệnh
                List<string> HIV = tthiv.First().MA_TINH_TRANG_DK.Split(';').ToList();
                foreach (var ab in HIV)
                {
                    LSHIV item = new LSHIV();
                    item.HIV = ab.ToString();
                    _lst.Add(item);
                    if(ab.ToString() == "1")
                    {
                        chk_c1.Checked = true;
                    }
                    if (ab.ToString() == "2")
                    {
                        chk_c2.Checked = true;
                    }
                    if (ab.ToString() == "3")
                    {
                        chk_c3.Checked = true;
                    }
                    if (ab.ToString() == "4")
                    {
                        chk_c4.Checked = true;
                    }
                    if (ab.ToString() == "5")
                    {
                        chk_c5.Checked = true;
                    }
                    if (ab.ToString() == "6")
                    {
                        chk_c6.Checked = true;
                    }
                    if (ab.ToString() == "7")
                    {
                        chk_c7.Checked = true;
                    }
                    if (ab.ToString() == "8")
                    {
                        chk_c8.Checked = true;
                    }
                    if (ab.ToString() == "9")
                    {
                        chk_c9.Checked = true;
                    }
                }
                #endregion
                cbx_XetNghiemPCR.SelectedIndex = Convert.ToInt32(tthiv.First().LAN_XN_PCR);
                if (tthiv.First().NGAY_XN_PCR != null)
                {
                    dtNgayThucHienPCR.DateTime = tthiv.First().NGAY_XN_PCR.Value;
                }
                if (tthiv.First().NGAY_KQ_XN_PCR != null)
                {
                    dtNgayCoKQPCR.DateTime = tthiv.First().NGAY_KQ_XN_PCR.Value;
                }
                cbx_KQPCR.SelectedIndex = Convert.ToInt32(tthiv.First().MA_KQ_XN_PCR);
                if (tthiv.First().NGAY_NHAN_TT_MANG_THAI != null)
                {
                    dtNgayMangThai.DateTime = tthiv.First().NGAY_NHAN_TT_MANG_THAI.Value;
                }
                if (tthiv.First().NGAY_BAT_DAU_DT_CTX != null)
                {
                    dtNgayDTCTX.DateTime = tthiv.First().NGAY_BAT_DAU_DT_CTX.Value;
                }
                cbx_XuTri.SelectedIndex = Convert.ToInt32(tthiv.First().MA_XU_TRI);
                if (tthiv.First().NGAY_BAT_DAU_XU_TRI != null)
                {
                    dtNgayBDXuTri.DateTime = tthiv.First().NGAY_BAT_DAU_XU_TRI.Value;
                }
                if (tthiv.First().NGAY_KET_THUC_XU_TRI != null)
                {
                    dtNgayKTXuTri.DateTime = tthiv.First().NGAY_KET_THUC_XU_TRI.Value;
                }
                txt_PhacDoDTHIV.Text = tthiv.First().MA_PHAC_DO_DIEU_TRI;
                cbx_BacPhacDoHIV.SelectedIndex = Convert.ToInt32(tthiv.First().MA_BAC_PHAC_DO);
                txt_SoNgayThuocCap.Text = Convert.ToString(tthiv.First().SO_NGAY_CAP_THUOC_ARV);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            panelControl3.Enabled = true;
            btnLuu.Enabled = true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            var bahiv = _data.BA_HIV.Where(p => p.MaBNhan == mabn).ToList();
            #region Sửa
            if (bahiv.Count() > 0)
            {
                DateTime ngayvao = new DateTime(0001, 01, 01, 00, 00, 00);
                BA_HIV hiv = _data.BA_HIV.Single(p => p.MaBNhan == mabn);
                #region Ngày KĐ
                if (dtNgayKD.DateTime == ngayvao)
                {
                    hiv.NGAYKD_HIV = null;
                }
                else
                {
                    hiv.NGAYKD_HIV = dtNgayKD.DateTime;
                }
                #endregion
                #region Thời Gian Nhận Thuốc
                if (dtThoiGianNhanThuoc.DateTime == ngayvao)
                {
                    hiv.BDDT_ARV = null;
                }
                else
                {
                    hiv.BDDT_ARV = dtThoiGianNhanThuoc.DateTime;
                }
                #endregion
                #region Bậc ĐT
                if (cbx_BacDT.SelectedIndex == 0)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 1;
                }
                else if (cbx_BacDT.SelectedIndex == 1)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 2;
                }
                else if (cbx_BacDT.SelectedIndex == 2)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 3;
                }
                #endregion
                #region Lý Do DT
                if (cbx_LyDoDK.SelectedIndex == 0)
                {
                    hiv.MA_LYDO_DTRI = 1;
                }
                else if (cbx_LyDoDK.SelectedIndex == 1)
                {
                    hiv.MA_LYDO_DTRI = 2;
                }
                else if (cbx_LyDoDK.SelectedIndex == 2)
                {
                    hiv.MA_LYDO_DTRI = 3;
                }
                else if (cbx_LyDoDK.SelectedIndex == 3)
                {
                    hiv.MA_LYDO_DTRI = 4;
                }
                else if (cbx_LyDoDK.SelectedIndex == 4)
                {
                    hiv.MA_LYDO_DTRI = 5;
                }
                #endregion
                #region Loại ĐT Lao
                if (cbx_LoaiDTLao.SelectedIndex == 0)
                {
                    hiv.LOAI_DTRI_LAO = 0;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 1)
                {
                    hiv.LOAI_DTRI_LAO = 1;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 2)
                {
                    hiv.LOAI_DTRI_LAO = 2;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 3)
                {
                    hiv.LOAI_DTRI_LAO = 3;
                }
                #endregion
                #region Phác Đồ ĐT Lao
                if (cbx_LoaiDTLao.SelectedIndex != 1)
                {
                    if (cbx_PhacDoDTLao.SelectedIndex == 0)
                    {
                        hiv.PHACDO_DTRI_LAO = 1;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 1)
                    {
                        hiv.PHACDO_DTRI_LAO = 2;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 2)
                    {
                        hiv.PHACDO_DTRI_LAO = 3;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 3)
                    {
                        hiv.PHACDO_DTRI_LAO = 4;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 4)
                    {
                        hiv.PHACDO_DTRI_LAO = 5;
                    }
                }
                else if (cbx_LoaiDTLao.SelectedIndex == 1)
                {
                    if (cbx_PhacDoDTLao1.SelectedIndex == 0)
                    {
                        hiv.PHACDO_DTRI_LAO = 6;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 1)
                    {
                        hiv.PHACDO_DTRI_LAO = 7;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 2)
                    {
                        hiv.PHACDO_DTRI_LAO = 8;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 3)
                    {
                        hiv.PHACDO_DTRI_LAO = 9;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 4)
                    {
                        hiv.PHACDO_DTRI_LAO = 10;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 5)
                    {
                        hiv.PHACDO_DTRI_LAO = 11;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 6)
                    {
                        hiv.PHACDO_DTRI_LAO = 12;
                    }
                }
                #endregion
                #region Ngày ĐT, KTĐT Lao
                if (dtNgayVao.DateTime == ngayvao)
                {
                    hiv.NGAYBD_DTRI_LAO = null;
                }
                else
                {
                    hiv.NGAYBD_DTRI_LAO = dtNgayVao.DateTime;
                }
                if (dtDenNgay.DateTime == ngayvao)
                {
                    hiv.NGAYKT_DTRI_LAO = null;
                }
                else
                {
                    hiv.NGAYKT_DTRI_LAO = dtDenNgay.DateTime;
                }
                #endregion
                #region Lý Do XNTL
                if (cbx_LyDoXetNghiem.SelectedIndex == 0)
                {
                    hiv.MA_LYDO_XNTL_VR = 1;
                }
                if (cbx_LyDoXetNghiem.SelectedIndex == 1)
                {
                    hiv.MA_LYDO_XNTL_VR = 2;
                }
                if (cbx_LyDoXetNghiem.SelectedIndex == 2)
                {
                    hiv.MA_LYDO_XNTL_VR = 3;
                }
                #endregion
                #region Ngày XN TLVR, KQXN
                if (dtThoiGianLamXN.DateTime == ngayvao)
                {
                    hiv.NGAY_XN_TLVR = null;
                }
                else
                {
                    hiv.NGAY_XN_TLVR = dtThoiGianLamXN.DateTime;
                }
                if (dtThoiGianCoKQ.DateTime == ngayvao)
                {
                    hiv.NGAY_KQ_XN_TLVR = null;
                }
                else
                {
                    hiv.NGAY_KQ_XN_TLVR = dtThoiGianCoKQ.DateTime;
                }
                #endregion
                #region KQ XNLT
                if (cbx_KetQuaXN.SelectedIndex == 0)
                {
                    hiv.KQ_XNTL_VR = 1;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 1)
                {
                    hiv.KQ_XNTL_VR = 2;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 2)
                {
                    hiv.KQ_XNTL_VR = 3;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 3)
                {
                    hiv.KQ_XNTL_VR = 4;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 4)
                {
                    hiv.KQ_XNTL_VR = 5;
                }
                #endregion
                #region Đối Tượng
                if (cbx_DoiTuong.SelectedIndex == 0)
                {
                    hiv.MA_LOAI_BN = 1;
                }
                else if (cbx_DoiTuong.SelectedIndex == 1)
                {
                    hiv.MA_LOAI_BN = 2;
                }
                else if (cbx_DoiTuong.SelectedIndex == 2)
                {
                    hiv.MA_LOAI_BN = 3;
                }
                else if (cbx_DoiTuong.SelectedIndex == 3)
                {
                    hiv.MA_LOAI_BN = 4;
                }
                else if (cbx_DoiTuong.SelectedIndex == 4)
                {
                    hiv.MA_LOAI_BN = 5;
                }
                #endregion
                #region Tình trạng BN
                string chuoi = ""; string chuoi1 = ""; string chuoi2 = ""; string chuoi3 = ""; string chuoi4 = ""; string chuoi5 = ""; string chuoi6 = ""; string chuoi7 = ""; string chuoi8 = ""; string chuoi9 = "";
                if (chk_c1.Checked)
                {
                    chuoi1 = "1" + ";";
                }
                if (chk_c2.Checked)
                {
                    chuoi2 = "2" + ";";
                }
                if (chk_c3.Checked)
                {
                    chuoi3 = "3" + ";";
                }
                if (chk_c4.Checked)
                {
                    chuoi4 = "4" + ";";
                }
                if (chk_c5.Checked)
                {
                    chuoi5 = "5" + ";";
                }
                if (chk_c6.Checked)
                {
                    chuoi6 = "6" + ";";
                }
                if (chk_c7.Checked)
                {
                    chuoi7 = "7" + ";";
                }
                if (chk_c8.Checked)
                {
                    chuoi8 = "8" + ";";
                }
                if (chk_c9.Checked)
                {
                    chuoi9 = "9";
                }
                chuoi = chuoi1 + chuoi2 + chuoi3 + chuoi4 + chuoi5 + chuoi6 + chuoi7 + chuoi8 + chuoi9;
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Remove(chuoi.Length - 1);
                }
                hiv.MA_TINH_TRANG_DK = chuoi;
                #endregion
                #region Thực hiện xét nghiệm PCR
                if (cbx_XetNghiemPCR.SelectedIndex == 0)
                {
                    hiv.LAN_XN_PCR = 1;
                }
                else if (cbx_XetNghiemPCR.SelectedIndex == 1)
                {
                    hiv.LAN_XN_PCR = 2;
                }
                else if (cbx_XetNghiemPCR.SelectedIndex == 2)
                {
                    hiv.LAN_XN_PCR = 3;
                }
                #endregion
                #region Ngày XNPCR, KQXNPCR, Ngay Mang Thai, Bat Dau DTCTX
                if (dtNgayThucHienPCR.DateTime == ngayvao)
                {
                    hiv.NGAY_XN_PCR = null;
                }
                else
                {
                    hiv.NGAY_XN_PCR = dtNgayThucHienPCR.DateTime;
                }
                if (dtNgayCoKQPCR.DateTime == ngayvao)
                {
                    hiv.NGAY_KQ_XN_PCR = null;
                }
                else
                {
                    hiv.NGAY_KQ_XN_PCR = dtNgayCoKQPCR.DateTime;
                }
                if (dtNgayMangThai.DateTime == ngayvao)
                {
                    hiv.NGAY_NHAN_TT_MANG_THAI = null;
                }
                else
                {
                    hiv.NGAY_NHAN_TT_MANG_THAI = dtNgayMangThai.DateTime;
                }
                if (dtNgayDTCTX.DateTime == ngayvao)
                {
                    hiv.NGAY_BAT_DAU_DT_CTX = null;
                }
                else
                {
                    hiv.NGAY_BAT_DAU_DT_CTX = dtNgayDTCTX.DateTime;
                }
                #endregion
                #region Mã KQ PCR
                if (cbx_KQPCR.SelectedIndex == 0)
                {
                    hiv.MA_KQ_XN_PCR = 0;
                }
                else if (cbx_KQPCR.SelectedIndex == 1)
                {
                    hiv.MA_KQ_XN_PCR = 1;
                }
                #endregion
                #region Ngày xử trí
                if (dtNgayBDXuTri.DateTime == ngayvao)
                {
                    hiv.NGAY_BAT_DAU_XU_TRI = null;
                }
                else
                {
                    hiv.NGAY_BAT_DAU_XU_TRI = dtNgayBDXuTri.DateTime;
                }
                if (dtNgayKTXuTri.DateTime == ngayvao)
                {
                    hiv.NGAY_KET_THUC_XU_TRI = null;
                }
                else
                {
                    hiv.NGAY_KET_THUC_XU_TRI = dtNgayKTXuTri.DateTime;
                }
                #endregion
                #region Xử trí
                //string xutri = ""; string xutri1 = ""; string xutri2 = ""; string xutri3 = ""; string xutri4 = ""; string xutri5 = ""; string xutri6 = ""; string xutri7 = "";
                //if (chk_c1.Checked)
                //{
                //    xutri1 = "1" + ";";
                //}
                //if (chk_c2.Checked)
                //{
                //    xutri2 = "2" + ";";
                //}
                //if (chk_c3.Checked)
                //{
                //    xutri3 = "3" + ";";
                //}
                //if (chk_c4.Checked)
                //{
                //    xutri4 = "4" + ";";
                //}
                //if (chk_c5.Checked)
                //{
                //    xutri5 = "5" + ";";
                //}
                //if (chk_c6.Checked)
                //{
                //    xutri6 = "6" + ";";
                //}
                //if (chk_c7.Checked)
                //{
                //    xutri7 = "7" + ";";
                //}
                //xutri = xutri1 + xutri2 + xutri3 + xutri4 + xutri5 + xutri6 + xutri7;
                //xutri = xutri.Remove(xutri.Length - 1);
                //hiv.MA_XU_TRI = xutri;
                if (cbx_XuTri.SelectedIndex == 0)
                {
                    hiv.MA_XU_TRI = 1;
                }
                else if (cbx_XuTri.SelectedIndex == 1)
                {
                    hiv.MA_XU_TRI = 2;
                }
                else if (cbx_XuTri.SelectedIndex == 2)
                {
                    hiv.MA_XU_TRI = 3;
                }
                else if (cbx_XuTri.SelectedIndex == 3)
                {
                    hiv.MA_XU_TRI = 4;
                }
                else if (cbx_XuTri.SelectedIndex == 4)
                {
                    hiv.MA_XU_TRI = 5;
                }
                else if (cbx_XuTri.SelectedIndex == 5)
                {
                    hiv.MA_XU_TRI = 6;
                }
                else if (cbx_XuTri.SelectedIndex == 6)
                {
                    hiv.MA_XU_TRI = 7;
                }
                #endregion
                #region Mã Bậc Phác đồ đt
                if (cbx_BacPhacDoHIV.SelectedIndex == 0)
                {
                    hiv.MA_BAC_PHAC_DO = 1;
                }
                else if (cbx_BacPhacDoHIV.SelectedIndex == 1)
                {
                    hiv.MA_BAC_PHAC_DO = 2;
                }
                else if (cbx_BacPhacDoHIV.SelectedIndex == 2)
                {
                    hiv.MA_BAC_PHAC_DO = 3;
                }
                #endregion
                hiv.MaBNhan = mabn;
                hiv.MA_PHAC_DO_DIEU_TRI_BD = txtPhacDoDT.Text;
                hiv.MA_PHAC_DO_DIEU_TRI = txtPhacDoDT.Text;
                if (txt_SoNgayThuocCap.Text == "")
                {
                    hiv.SO_NGAY_CAP_THUOC_ARV = 0;
                }
                else
                {
                    hiv.SO_NGAY_CAP_THUOC_ARV = Convert.ToInt32(txt_SoNgayThuocCap.Text);
                }
                _data.SaveChanges();
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Sửa thông tin thành công");
                    frm_ChamSocHIV_AIDS_Load(null, null);
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
                DateTime ngayvao = new DateTime(0001, 01, 01, 00, 00, 00);
                BA_HIV hiv = new BA_HIV();
                #region Ngày KĐ
                if (dtNgayKD.DateTime == ngayvao)
                {
                    hiv.NGAYKD_HIV = null;
                }
                else
                {
                    hiv.NGAYKD_HIV = dtNgayKD.DateTime;
                }
                #endregion
                #region Thời Gian Nhận Thuốc
                if (dtThoiGianNhanThuoc.DateTime == ngayvao)
                {
                    hiv.BDDT_ARV = null;
                }
                else
                {
                    hiv.BDDT_ARV = dtThoiGianNhanThuoc.DateTime;
                }
                #endregion
                #region Bậc ĐT
                if (cbx_BacDT.SelectedIndex == 0)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 1;
                }
                else if (cbx_BacDT.SelectedIndex == 1)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 2;
                }
                else if (cbx_BacDT.SelectedIndex == 2)
                {
                    hiv.MA_BAC_PHAC_DO_BD = 3;
                }
                #endregion
                #region Lý Do DT
                if (cbx_LyDoDK.SelectedIndex == 0)
                {
                    hiv.MA_LYDO_DTRI = 1;
                }
                else if (cbx_LyDoDK.SelectedIndex == 1)
                {
                    hiv.MA_LYDO_DTRI = 2;
                }
                else if (cbx_LyDoDK.SelectedIndex == 2)
                {
                    hiv.MA_LYDO_DTRI = 3;
                }
                else if (cbx_LyDoDK.SelectedIndex == 3)
                {
                    hiv.MA_LYDO_DTRI = 4;
                }
                else if (cbx_LyDoDK.SelectedIndex == 4)
                {
                    hiv.MA_LYDO_DTRI = 5;
                }
                #endregion
                #region Loại ĐT Lao
                if (cbx_LoaiDTLao.SelectedIndex == 0)
                {
                    hiv.LOAI_DTRI_LAO = 0;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 1)
                {
                    hiv.LOAI_DTRI_LAO = 1;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 2)
                {
                    hiv.LOAI_DTRI_LAO = 2;
                }
                if (cbx_LoaiDTLao.SelectedIndex == 3)
                {
                    hiv.LOAI_DTRI_LAO = 3;
                }
                #endregion
                #region Phác Đồ ĐT Lao
                if (cbx_LoaiDTLao.SelectedIndex != 1)
                {
                    if (cbx_PhacDoDTLao.SelectedIndex == 0)
                    {
                        hiv.PHACDO_DTRI_LAO = 1;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 1)
                    {
                        hiv.PHACDO_DTRI_LAO = 2;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 2)
                    {
                        hiv.PHACDO_DTRI_LAO = 3;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 3)
                    {
                        hiv.PHACDO_DTRI_LAO = 4;
                    }
                    if (cbx_PhacDoDTLao.SelectedIndex == 4)
                    {
                        hiv.PHACDO_DTRI_LAO = 5;
                    }
                }
                else if (cbx_LoaiDTLao.SelectedIndex == 1)
                {
                    if (cbx_PhacDoDTLao1.SelectedIndex == 0)
                    {
                        hiv.PHACDO_DTRI_LAO = 6;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 1)
                    {
                        hiv.PHACDO_DTRI_LAO = 7;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 2)
                    {
                        hiv.PHACDO_DTRI_LAO = 8;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 3)
                    {
                        hiv.PHACDO_DTRI_LAO = 9;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 4)
                    {
                        hiv.PHACDO_DTRI_LAO = 10;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 5)
                    {
                        hiv.PHACDO_DTRI_LAO = 11;
                    }
                    if (cbx_PhacDoDTLao1.SelectedIndex == 6)
                    {
                        hiv.PHACDO_DTRI_LAO = 12;
                    }
                }
                #endregion
                #region Ngày ĐT, KTĐT Lao
                if (dtNgayVao.DateTime == ngayvao)
                {
                    hiv.NGAYBD_DTRI_LAO = null;
                }
                else
                {
                    hiv.NGAYBD_DTRI_LAO = dtNgayVao.DateTime;
                }
                if (dtDenNgay.DateTime == ngayvao)
                {
                    hiv.NGAYKT_DTRI_LAO = null;
                }
                else
                {
                    hiv.NGAYKT_DTRI_LAO = dtDenNgay.DateTime;
                }
                #endregion
                #region Lý Do XNTL
                if (cbx_LyDoXetNghiem.SelectedIndex == 0)
                {
                    hiv.MA_LYDO_XNTL_VR = 1;
                }
                if (cbx_LyDoXetNghiem.SelectedIndex == 1)
                {
                    hiv.MA_LYDO_XNTL_VR = 2;
                }
                if (cbx_LyDoXetNghiem.SelectedIndex == 2)
                {
                    hiv.MA_LYDO_XNTL_VR = 3;
                }
                #endregion
                #region Ngày XN TLVR, KQXN
                if (dtThoiGianLamXN.DateTime == ngayvao)
                {
                    hiv.NGAY_XN_TLVR = null;
                }
                else
                {
                    hiv.NGAY_XN_TLVR = dtThoiGianLamXN.DateTime;
                }
                if (dtThoiGianCoKQ.DateTime == ngayvao)
                {
                    hiv.NGAY_KQ_XN_TLVR = null;
                }
                else
                {
                    hiv.NGAY_KQ_XN_TLVR = dtThoiGianCoKQ.DateTime;
                }
                #endregion
                #region KQ XNLT
                if (cbx_KetQuaXN.SelectedIndex == 0)
                {
                    hiv.KQ_XNTL_VR = 1;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 1)
                {
                    hiv.KQ_XNTL_VR = 2;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 2)
                {
                    hiv.KQ_XNTL_VR = 3;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 3)
                {
                    hiv.KQ_XNTL_VR = 4;
                }
                else if (cbx_KetQuaXN.SelectedIndex == 4)
                {
                    hiv.KQ_XNTL_VR = 5;
                }
                #endregion
                #region Đối Tượng
                if (cbx_DoiTuong.SelectedIndex == 0)
                {
                    hiv.MA_LOAI_BN = 1;
                }
                else if (cbx_DoiTuong.SelectedIndex == 1)
                {
                    hiv.MA_LOAI_BN = 2;
                }
                else if (cbx_DoiTuong.SelectedIndex == 2)
                {
                    hiv.MA_LOAI_BN = 3;
                }
                else if (cbx_DoiTuong.SelectedIndex == 3)
                {
                    hiv.MA_LOAI_BN = 4;
                }
                else if (cbx_DoiTuong.SelectedIndex == 4)
                {
                    hiv.MA_LOAI_BN = 5;
                }
                #endregion
                #region Tình trạng BN
                string chuoi = ""; string chuoi1 = ""; string chuoi2 = ""; string chuoi3 = ""; string chuoi4 = ""; string chuoi5 = ""; string chuoi6 = ""; string chuoi7 = ""; string chuoi8 = ""; string chuoi9 = "";
                if (chk_c1.Checked)
                {
                    chuoi1 = "1" + ";";
                }
                if (chk_c2.Checked)
                {
                    chuoi2 = "2" + ";";
                }
                if (chk_c3.Checked)
                {
                    chuoi3 = "3" + ";";
                }
                if (chk_c4.Checked)
                {
                    chuoi4 = "4" + ";";
                }
                if (chk_c5.Checked)
                {
                    chuoi5 = "5" + ";";
                }
                if (chk_c6.Checked)
                {
                    chuoi6 = "6" + ";";
                }
                if (chk_c7.Checked)
                {
                    chuoi7 = "7" + ";";
                }
                if (chk_c8.Checked)
                {
                    chuoi8 = "8" + ";";
                }
                if (chk_c9.Checked)
                {
                    chuoi9 = "9";
                }
                chuoi = chuoi1 + chuoi2 + chuoi3 + chuoi4 + chuoi5 + chuoi6 + chuoi7 + chuoi8 + chuoi9;
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Remove(chuoi.Length - 1);
                }
                hiv.MA_TINH_TRANG_DK = chuoi;
                #endregion
                #region Thực hiện xét nghiệm PCR
                if (cbx_XetNghiemPCR.SelectedIndex == 0)
                {
                    hiv.LAN_XN_PCR = 1;
                }
                else if (cbx_XetNghiemPCR.SelectedIndex == 1)
                {
                    hiv.LAN_XN_PCR = 2;
                }
                else if (cbx_XetNghiemPCR.SelectedIndex == 2)
                {
                    hiv.LAN_XN_PCR = 3;
                }
                #endregion
                #region Ngày XNPCR, KQXNPCR, Ngay Mang Thai, Bat Dau DTCTX
                if (dtNgayThucHienPCR.DateTime == ngayvao)
                {
                    hiv.NGAY_XN_PCR = null;
                }
                else
                {
                    hiv.NGAY_XN_PCR = dtNgayThucHienPCR.DateTime;
                }
                if (dtNgayCoKQPCR.DateTime == ngayvao)
                {
                    hiv.NGAY_KQ_XN_PCR = null;
                }
                else
                {
                    hiv.NGAY_KQ_XN_PCR = dtNgayCoKQPCR.DateTime;
                }
                if (dtNgayMangThai.DateTime == ngayvao)
                {
                    hiv.NGAY_NHAN_TT_MANG_THAI = null;
                }
                else
                {
                    hiv.NGAY_NHAN_TT_MANG_THAI = dtNgayMangThai.DateTime;
                }
                if (dtNgayDTCTX.DateTime == ngayvao)
                {
                    hiv.NGAY_BAT_DAU_DT_CTX = null;
                }
                else
                {
                    hiv.NGAY_BAT_DAU_DT_CTX = dtNgayDTCTX.DateTime;
                }
                #endregion
                #region Mã KQ PCR
                if (cbx_KQPCR.SelectedIndex == 0)
                {
                    hiv.MA_KQ_XN_PCR = 0;
                }
                else if (cbx_KQPCR.SelectedIndex == 1)
                {
                    hiv.MA_KQ_XN_PCR = 1;
                }
                #endregion
                #region Ngày xử trí
                if (dtNgayBDXuTri.DateTime == ngayvao)
                {
                    hiv.NGAY_BAT_DAU_XU_TRI = null;
                }
                else
                {
                    hiv.NGAY_BAT_DAU_XU_TRI = dtNgayBDXuTri.DateTime;
                }
                if (dtNgayKTXuTri.DateTime == ngayvao)
                {
                    hiv.NGAY_KET_THUC_XU_TRI = null;
                }
                else
                {
                    hiv.NGAY_KET_THUC_XU_TRI = dtNgayKTXuTri.DateTime;
                }
                #endregion
                #region Xử trí
                //string xutri = ""; string xutri1 = ""; string xutri2 = ""; string xutri3 = ""; string xutri4 = ""; string xutri5 = ""; string xutri6 = ""; string xutri7 = "";
                //if (chk_c1.Checked)
                //{
                //    xutri1 = "1" + ";";
                //}
                //if (chk_c2.Checked)
                //{
                //    xutri2 = "2" + ";";
                //}
                //if (chk_c3.Checked)
                //{
                //    xutri3 = "3" + ";";
                //}
                //if (chk_c4.Checked)
                //{
                //    xutri4 = "4" + ";";
                //}
                //if (chk_c5.Checked)
                //{
                //    xutri5 = "5" + ";";
                //}
                //if (chk_c6.Checked)
                //{
                //    xutri6 = "6" + ";";
                //}
                //if (chk_c7.Checked)
                //{
                //    xutri7 = "7" + ";";
                //}
                //xutri = xutri1 + xutri2 + xutri3 + xutri4 + xutri5 + xutri6 + xutri7;
                //xutri = xutri.Remove(xutri.Length - 1);
                //hiv.MA_XU_TRI = xutri;
                if (cbx_XuTri.SelectedIndex == 0)
                {
                    hiv.MA_XU_TRI = 1;
                }
                else if (cbx_XuTri.SelectedIndex == 1)
                {
                    hiv.MA_XU_TRI = 2;
                }
                else if (cbx_XuTri.SelectedIndex == 2)
                {
                    hiv.MA_XU_TRI = 3;
                }
                else if (cbx_XuTri.SelectedIndex == 3)
                {
                    hiv.MA_XU_TRI = 4;
                }
                else if (cbx_XuTri.SelectedIndex == 4)
                {
                    hiv.MA_XU_TRI = 5;
                }
                else if (cbx_XuTri.SelectedIndex == 5)
                {
                    hiv.MA_XU_TRI = 6;
                }
                else if (cbx_XuTri.SelectedIndex == 6)
                {
                    hiv.MA_XU_TRI = 7;
                }
                #endregion
                #region Mã Bậc Phác đồ đt
                if (cbx_BacPhacDoHIV.SelectedIndex == 0)
                {
                    hiv.MA_BAC_PHAC_DO = 1;
                }
                else if (cbx_BacPhacDoHIV.SelectedIndex == 1)
                {
                    hiv.MA_BAC_PHAC_DO = 2;
                }
                else if (cbx_BacPhacDoHIV.SelectedIndex == 2)
                {
                    hiv.MA_BAC_PHAC_DO = 3;
                }
                #endregion
                hiv.MaBNhan = mabn;
                hiv.MA_PHAC_DO_DIEU_TRI_BD = txtPhacDoDT.Text;
                hiv.MA_PHAC_DO_DIEU_TRI = txtPhacDoDT.Text;
                if (txt_SoNgayThuocCap.Text == "")
                {
                    hiv.SO_NGAY_CAP_THUOC_ARV = 0;
                }
                else
                {
                    hiv.SO_NGAY_CAP_THUOC_ARV = Convert.ToInt32(txt_SoNgayThuocCap.Text);
                }
                _data.BA_HIV.Add(hiv);
                if (_data.SaveChanges() >= 0)
                {
                    MessageBox.Show("Tạo mới thành công");
                    frm_ChamSocHIV_AIDS_Load(sender, e);
                }
            }
            #endregion
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbx_LoaiDTLao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_LoaiDTLao.SelectedIndex == 0)
            {
                cbx_PhacDoDTLao.Visible = true;
                cbx_PhacDoDTLao1.Visible = false;
                dtNgayVao.Enabled = false;
                dtDenNgay.Enabled = false;
                cbx_PhacDoDTLao.Enabled = false;
            }
            else if (cbx_LoaiDTLao.SelectedIndex == 1)
            {
                cbx_PhacDoDTLao1.Visible = true;
                cbx_PhacDoDTLao.Visible = false;
                dtNgayVao.Enabled = true;
                dtDenNgay.Enabled = true;
                cbx_PhacDoDTLao1.Enabled = true;
            }
            else if (cbx_LoaiDTLao.SelectedIndex == 2 || cbx_LoaiDTLao.SelectedIndex == 3)
            {
                cbx_PhacDoDTLao.Visible = true;
                cbx_PhacDoDTLao1.Visible = false;
                dtNgayVao.Enabled = true;
                dtDenNgay.Enabled = true;
                cbx_PhacDoDTLao.Enabled = true;
            }
        }

        private void chk_TT1_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_c1.Checked == true)
            {
                cbx_XetNghiemPCR.Enabled = true;
                dtNgayThucHienPCR.Enabled = true;
                dtNgayCoKQPCR.Enabled = true;
                cbx_KQPCR.Enabled = true;
            }    
            else
            {
                cbx_XetNghiemPCR.Enabled = false;
                dtNgayThucHienPCR.Enabled = false;
                dtNgayCoKQPCR.Enabled = false;
                cbx_KQPCR.Enabled = false;
            }    
        }

        private void chk_c9_CheckedChanged(object sender, EventArgs e)
        {
            chk_c1.Checked = false;
            chk_c2.Checked = false;
            chk_c3.Checked = false;
            chk_c4.Checked = false;
            chk_c5.Checked = false;
            chk_c6.Checked = false;
            chk_c7.Checked = false;
            chk_c8.Checked = false;
        }
    }
}
