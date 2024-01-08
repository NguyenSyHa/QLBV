using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.IO.Ports;
using System.Configuration;
using QLBV.ChucNang;
using QLBV.Utilities.Commons;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace QLBV.FormThamSo
{
    public partial class us_hethong : DevExpress.XtraEditors.XtraUserControl
    {
        public us_hethong()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }
        
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int load = 0;
        int rdthuchi = 0; 
        int TTLuu = 0;
        string macb = "";
        Image defaultImage = QLBV.Properties.Resources.Signature;
        private void us_hethong_Load(object sender, EventArgs e)
        {
            string[] strPort = SerialPort.GetPortNames();
            cboCom.Properties.Items.AddRange(strPort);
            cboCom.Text = ConfigurationManager.AppSettings["ComPort"];
            cboSoPhong.Text = ConfigurationManager.AppSettings["phongdoc"];
            txtWidthListBN.Text = ConfigurationManager.AppSettings["withListBN"];
            txtAutoCLS.Text = ConfigurationManager.AppSettings["timerAutoResultCLS"];

            #region load HThong User
            var quser = (from cb in _data.CanBoes
                         join adm in _data.ADMINs on cb.MaCB equals adm.MaCB
                         join kp in _data.KPhongs on cb.MaKP equals kp.MaKP
                         select new
                         {
                             cb.MaCB,
                             cb.TenCB,
                             adm.TenDN,
                             kp.TenKP
                         }).ToList();
            txtKTVP.DataSource = quser;
            #endregion

            groupTTC.Text = "Thông tin bệnh viện - " + DungChung.Bien.MaBV;
            //if (DungChung.Bien.PLoaiKP == "Admin" && DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                chkTuDuyet.Enabled = false;
            }
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                txtSoBA.Enabled = true;
                txtSoCV.Enabled = true;
                txtSoKB.Enabled = true;
                txtSoLuuTru.Enabled = true;
                cboSoVaoVien.Enabled = true;
                chkTuDuyet.Enabled = true;
                //btnThietLap.Enabled = true;
            }
            else
            {
                txtSoBA.Enabled = false;
                txtSoCV.Enabled = false;
                txtSoKB.Enabled = false;
                txtSoLuuTru.Enabled = false;
                cboSoVaoVien.Enabled = false;
                chkTuDuyet.Enabled = false;
                //btnThietLap.Enabled = false;
            }

            load = 0;
            //if (DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                groupTTC.Enabled = true;
            else
                groupTTC.Enabled = false;
            var tinh = (from tin in _data.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
            if (tinh.Count > 0)
                lupMaTinh.Properties.DataSource = tinh.ToList();
            var kho = _data.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP).ToList();
            lupKhoXuat.Properties.DataSource = kho.Where(p => (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? (p.PLoai == ("Khoa dược") || p.PLoai == ("Tủ trực")) : p.PLoai == ("Khoa dược")).ToList(); //yc hùng 11-06
            lupKhoXuatDonNgoai.Properties.DataSource = kho.Where(p => (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? (p.PLoai == ("Khoa dược") || p.PLoai == ("Tủ trực")) : p.PLoai == ("Khoa dược")).ToList();
            var q = (from ht in _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV) select ht).FirstOrDefault();

            if (q != null)
            {
                txtTenCQ.Text = DungChung.Bien.TenCQ;
                txtTenCQrg.Text = q.TenCQrg;
                txtDiachi.Text = q.DiaChi;
                txtDiaDanh.Text = q.DiaDanh;
                txtDienThoai.Text = q.SDT;
                txtCQChuquan.Text = q.TenCQCQ;
                //lupMaTinh.EditValue=q.ma
                txtMangansach.Text = q.MaNganSach;
                txtGiamdoc.Text = q.GiamDoc;
                txtKetoantruong.Text = q.KeToanTruong;
                txtTruongkhoaduoc.Text = q.TruongKhoaDuoc;
                if (q.LamTron != null)
                    cboLamTron.SelectedIndex = q.LamTron.Value;
                else
                    cboLamTron.SelectedIndex = -1;
                try
                {
                    string[] giotu = q.GioTu.Split(';');
                    string[] gioden = q.GioDen.Split(';');
                    txtGioDen1.Text = gioden[0];
                    txtGioTu1.Text = giotu[0];
                    txtGioTu2.Text = giotu[1];
                    txtGioDen2.Text = gioden[1];
                }
                catch (Exception)
                {
                    txtGioDen1.Text = "";
                    txtGioTu1.Text = "";
                    txtGioTu2.Text = "";
                    txtGioDen2.Text = "";
                }
                cboInPhoi.SelectedIndex = q.InPhoi ?? -1;
                cbo_PPXuat0.SelectedIndex = PhuongPGuiBHXH(q.PPXuat)[0];

                cbo_PPXuat1.SelectedIndex = PhuongPGuiBHXH(q.PPXuat)[1];
                if (q.GHanTT100 != null)
                    txtGHBHYT.Text = q.GHanTT100.ToString();
                if (q.NgayGiaMoi != null)
                    dt_NgayGiaMoi.DateTime = q.NgayGiaMoi.Value;
                if (q.NgayGiaMoiDV != null)
                    dt_NgayGiaMoi_DV.DateTime = q.NgayGiaMoiDV.Value;
                chkKeNhieuKho.Checked = q.KeDonNhieuKho;
                txtHienThiTuoi.Text = q.FormatAge;
                txtTenCQBH.Text = q.TenCQBH;
                txtTenCQCQBH.Text = q.TenCQCQBH;
                txtGiamdinhBH.Text = q.GiamDinhBH;
                txtGDBH2.Text = q.GiamDinhBH2;
                txtSoCV.SelectedIndex = q.SoChuyenVien;
                txtSoBA.SelectedIndex = q.SoBenhAn;
                txtSoLuuTru.SelectedIndex = q.SoLuuTru;
                txtSoKB.SelectedIndex = q.SoKB;
                cboSoVaoVien.SelectedIndex = q.SoVaoVien;
                ckcChiTamThu.Checked = q.ChiTamUng;
                chkTuDuyet.Checked = q.TuDongDuyet ?? false;
                if (q.InPhoi != null)
                    cboInPhoi.SelectedIndex = q.InPhoi.Value;
                else
                    cboInPhoi.SelectedIndex = -1;
                string[] MauIn = q.MauIn == null ? new string[] { } : q.MauIn.Split(';');
                if (MauIn.Length > 0 && !string.IsNullOrEmpty(MauIn[0]))
                {
                    int mau = Convert.ToInt16(MauIn[0].Trim());
                    rdInBK01.SelectedIndex = mau;
                }
                if (MauIn.Length > 1 && !string.IsNullOrEmpty(MauIn[1]))
                {
                    int mau = Convert.ToInt16(MauIn[1].Trim());
                    rdInBK02.SelectedIndex = mau;
                }
                if (MauIn.Length > 2 && !string.IsNullOrEmpty(MauIn[2]))
                {
                    int mau = Convert.ToInt16(MauIn[2].Trim());
                    rdlInThuChi.SelectedIndex = mau;
                    rdthuchi = mau;
                }
                if (MauIn.Length > 3 && !string.IsNullOrEmpty(MauIn[3]))
                {
                    int mau = Convert.ToInt16(MauIn[3].Trim());
                    rdInPThu.SelectedIndex = mau;
                }
                if (MauIn.Length > 4 && !string.IsNullOrEmpty(MauIn[4]))
                {
                    int mau = Convert.ToInt16(MauIn[4].Trim());
                    rdpptinhton.SelectedIndex = mau;
                }
                if (MauIn.Length > 5 && !string.IsNullOrEmpty(MauIn[5]))
                {
                    int mau = Convert.ToInt16(MauIn[5].Trim());
                    rdInBK6556.SelectedIndex = mau;
                }

                txtUser.Text = q.MaLienThongCSKCB;
                txtPassword.Text = Security.Decrypt(q.MKDonThuocLienThong);
                txtConfirmPassword.Text = Security.Decrypt(q.MKDonThuocLienThong);

            }
            else
            {
                txtGDBH2.ResetText();
            }

            var perm = _data.Permissions.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
            if (perm.Where(p => p.ID == 9051).ToList().Count > 0 && perm.Where(p => p.ID == 9051).First().C_View == true)
            {
                btnThietLap.Enabled = true;
            }
            sbtLuu.Enabled = false;
            load++;
        }

        private void txtTenCQ_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtDiachi_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtCQChuquan_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtMangansach_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtKetoanvp_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtKetoantruong_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtTruongkhoaduoc_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGiamdinhBH_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGHBHYT_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtNguoilapbieu_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void txtPPxuatduoc_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtDinhdang_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtThukho_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void txtTruongkhoa_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }
        List<HTHONG> _Hthong = new List<HTHONG>();

        public static int[] PhuongPGuiBHXH(int? pp)
        {
            int[] ppgui = new int[2] { -1, -1 };
            if (pp != null)
            {
                if (pp.ToString().Length > 1)
                {
                    ppgui[0] = Convert.ToInt16(pp.ToString().Substring(0, 1));
                    ppgui[1] = Convert.ToInt16(pp.ToString().Substring(1, 1));
                }
                else
                {
                    ppgui[0] = 0;
                    ppgui[1] = pp.Value;
                }
            }
            return ppgui;
        }
        private void sbtLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("MK liên thông CSKCB và Nhập lại MK liên thông CSKCB không giống nhau. Xin vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var update = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                if (update.Count <= 0)
                {
                    HTHONG themmoi = new HTHONG();
                    themmoi.TenCQCQ = txtCQChuquan.Text;
                    themmoi.TenCQrg = txtTenCQrg.Text;
                    themmoi.DiaChi = txtDiachi.Text;
                    themmoi.DiaDanh = txtDiaDanh.Text;
                    themmoi.MaNganSach = txtMangansach.Text;
                    themmoi.GiamDoc = txtGiamdoc.Text;
                    themmoi.KeToanTruong = txtKetoantruong.Text;
                    themmoi.TruongKhoaDuoc = txtTruongkhoaduoc.Text;
                    themmoi.GioTu = txtGioTu1.Text + ";" + txtGioTu2;
                    themmoi.GioDen = txtGioDen1.Text + ";" + txtGioDen2;
                    themmoi.InPhoi = cboInPhoi.SelectedIndex;
                    themmoi.ChiTamUng = ckcChiTamThu.Checked;

                    if (!string.IsNullOrEmpty(txtGHBHYT.Text))
                    {
                        themmoi.GHanTT100 = Convert.ToInt32(txtGHBHYT.Text);
                    }

                    else
                    {
                        themmoi.GHanTT100 = 172500;
                    }
                    themmoi.LamTron = cboLamTron.SelectedIndex;
                    themmoi.GiamDinhBH = txtGiamdinhBH.Text;
                    themmoi.TenCQBH = txtTenCQBH.Text;
                    themmoi.TenCQCQBH = txtTenCQCQBH.Text;
                    themmoi.MaBV = DungChung.Bien.MaBV;
                    themmoi.FormatAge = txtHienThiTuoi.Text;
                    themmoi.GiamDinhBH2 = txtGDBH2.Text.Trim();
                    themmoi.PPXuat = Convert.ToInt32(cbo_PPXuat0.SelectedIndex.ToString() + cbo_PPXuat1.SelectedIndex.ToString());
                    if (dt_NgayGiaMoi.Text != null)
                        themmoi.NgayGiaMoi = dt_NgayGiaMoi.DateTime;
                    if (dt_NgayGiaMoi_DV.Text != null)
                        themmoi.NgayGiaMoiDV = dt_NgayGiaMoi_DV.DateTime;
                    themmoi.KeDonNhieuKho = chkKeNhieuKho.Checked;
                    themmoi.InPhoi = cboInPhoi.SelectedIndex;
                    themmoi.SoChuyenVien = txtSoCV.SelectedIndex;
                    themmoi.SoBenhAn = (txtSoBA.SelectedIndex);
                    themmoi.SoLuuTru = (txtSoLuuTru.SelectedIndex);
                    themmoi.SoKB = (txtSoKB.SelectedIndex);
                    themmoi.SoVaoVien = cboSoVaoVien.SelectedIndex;
                    themmoi.SDT = txtDienThoai.Text;
                    string mauin = rdInBK01.SelectedIndex.ToString() + ";" + rdInBK02.SelectedIndex.ToString() + ";" + rdlInThuChi.SelectedIndex.ToString() + ";" + rdInPThu.SelectedIndex.ToString() + ";" + rdpptinhton.SelectedIndex.ToString() + ";" + rdInBK6556.SelectedIndex.ToString();
                    themmoi.MauIn = mauin;
                    themmoi.MaLienThongCSKCB = txtUser.Text;
                    themmoi.MKDonThuocLienThong = Security.Encrypt(txtPassword.Text);

                    _Hthong.Add(themmoi);
                    _data.HTHONGs.Add(themmoi);
                    _data.SaveChanges();

                    sbtLuu.Enabled = false;
                    MessageBox.Show("Hãy đăng nhập lại hệ thống");
                }
                else
                {
                    DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin đã thay đổi?", "Sửa thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dia == DialogResult.Yes)
                    {
                        foreach (var sua in update)
                        {
                            sua.DiaChi = txtDiachi.Text;
                            if (!string.IsNullOrEmpty(txtGHBHYT.Text))
                            {
                                sua.GHanTT100 = Convert.ToInt32(txtGHBHYT.Text);
                            }
                            else
                            {
                                sua.GHanTT100 = 182500;
                            }
                            sua.InPhoi = cboInPhoi.SelectedIndex;
                            sua.LamTron = cboLamTron.SelectedIndex;
                            sua.GiamDinhBH = txtGiamdinhBH.Text;
                            sua.TenCQBH = txtTenCQBH.Text;
                            sua.TenCQCQBH = txtTenCQCQBH.Text;
                            sua.GioTu = txtGioTu1.Text + ";" + txtGioTu2.Text;
                            sua.GioDen = txtGioDen1.Text + ";" + txtGioDen2.Text;
                            sua.GiamDoc = txtGiamdoc.Text;
                            sua.KeToanTruong = txtKetoantruong.Text;
                            sua.MaNganSach = txtMangansach.Text;
                            sua.FormatAge = txtHienThiTuoi.Text;
                            sua.TenCQCQ = txtCQChuquan.Text;
                            sua.TruongKhoaDuoc = txtTruongkhoaduoc.Text;
                            sua.TenCQrg = txtTenCQrg.Text;
                            sua.DiaDanh = txtDiaDanh.Text;
                            sua.KeDonNhieuKho = chkKeNhieuKho.Checked;
                            sua.PPXuat = Convert.ToInt32(cbo_PPXuat0.SelectedIndex.ToString() + cbo_PPXuat1.SelectedIndex.ToString());
                            sua.GiamDinhBH2 = txtGDBH2.Text.Trim();
                            if (dt_NgayGiaMoi.Text != null)
                                sua.NgayGiaMoi = dt_NgayGiaMoi.DateTime;
                            if (dt_NgayGiaMoi_DV.Text != null)
                                sua.NgayGiaMoiDV = dt_NgayGiaMoi_DV.DateTime;
                            sua.InPhoi = cboInPhoi.SelectedIndex;
                            sua.SoChuyenVien = txtSoCV.SelectedIndex;
                            sua.SoBenhAn = (txtSoBA.SelectedIndex);
                            sua.SoLuuTru = (txtSoLuuTru.SelectedIndex);
                            sua.SoKB = (txtSoKB.SelectedIndex);
                            sua.SoVaoVien = cboSoVaoVien.SelectedIndex;
                            sua.ChiTamUng = ckcChiTamThu.Checked;
                            sua.SDT = txtDienThoai.Text;
                            string[] mauin = sua.MauIn == null ? new string[] { } : sua.MauIn.Split(';');
                            if (mauin.Length > 5)
                            {
                                mauin[0] = rdInBK01.SelectedIndex.ToString();
                                mauin[1] = rdInBK02.SelectedIndex.ToString();
                                //if (mauin.Length > 2)
                                mauin[2] = rdlInThuChi.SelectedIndex.ToString();
                                mauin[3] = rdInPThu.SelectedIndex.ToString();
                                mauin[4] = rdpptinhton.SelectedIndex.ToString();
                                mauin[5] = rdInBK6556.SelectedIndex.ToString();
                                sua.MauIn = string.Join(";", mauin);
                            }
                            else
                            {
                                sua.MauIn = rdInBK01.SelectedIndex.ToString() + ";" + rdInBK02.SelectedIndex.ToString() + ";" + rdlInThuChi.SelectedIndex.ToString() + ";" + rdInPThu.SelectedIndex.ToString() + ";" + rdpptinhton.SelectedIndex.ToString() + ";" + rdInBK6556.SelectedIndex.ToString();
                            }

                            sua.MaLienThongCSKCB = txtUser.Text;
                            sua.MKDonThuocLienThong = Security.Encrypt(txtPassword.Text);

                            _data.SaveChanges();
                        }
                        sbtLuu.Enabled = false;
                        //
                        MessageBox.Show("Hãy đăng nhập lại hệ thống");
                    }
                    else
                    {
                        us_hethong_Load(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }
        }

        private void txtTenCQ_Leave(object sender, EventArgs e)
        {
            //if (txtTenCQ.Text == "" || txtTenCQ.Text == null)
            //{
            //    MessageBox.Show("Bạn chưa nhập Tên cơ quan");
            //    txtTenCQ.Focus();
            //}
        }

        private void txtDiachi_Leave(object sender, EventArgs e)
        {
            //if (txtDiachi.Text == "" || txtDiachi.Text == null)
            //{
            //    MessageBox.Show("Bạn chưa nhập địa chỉ");
            //    txtDiachi.Focus();
            //}
        }

        private void txtCQChuquan_Leave(object sender, EventArgs e)
        {
            //    if (txtCQChuquan.Text == "" || txtCQChuquan.Text == null)
            //    {
            //        MessageBox.Show("Bạn chưa nhập Cơ quan chủ quản");
            //        txtCQChuquan.Focus();
            //    }
        }

        private void txtMangansach_Leave(object sender, EventArgs e)
        {
            //if (txtMangansach.Text == "" || txtMangansach.Text == null)
            //{
            //    MessageBox.Show("Bạn chưa nhập");
            //}
        }

        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void lupMaTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaTinh.EditValue != null)
            {
                string matinh = lupMaTinh.EditValue.ToString().Trim();
                var huyen = (from h in _data.DmHuyens.Where(p => p.MaTinh.Trim() == matinh) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
                if (huyen.Count > 0)
                    lupMaHuyen.Properties.DataSource = huyen.ToList();
            }
        }

        private void lupMaHuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaTinh.EditValue != null)
            {
                string matinh = lupMaTinh.EditValue.ToString().Trim();
                if (lupMaHuyen.EditValue != null)
                {
                    string mahuyen = lupMaHuyen.ToString().Trim();
                    var xa = (from x in _data.DmXas.Where(p => p.MaHuyen.Trim() == (mahuyen)).Where(p => p.MaTinh.Trim() == (matinh)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
                    if (xa.Count > 0)
                        lupMaXa.Properties.DataSource = xa.ToList();
                }
            }
        }

        private void txtGioTu_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGioDen_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtTenCQBH_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtTenCQCQBH_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void cboInPhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtDuongDan_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void textEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtTenCQrg_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtDiaDanh_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void cboLamTron_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGDBH2_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void cboNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void cboGetICD_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void labelControl29_Click(object sender, EventArgs e)
        {

        }

        private void labelControl26_Click(object sender, EventArgs e)
        {

        }

        private void labelControl24_Click(object sender, EventArgs e)
        {

        }

        private void labelControl19_Click(object sender, EventArgs e)
        {

        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

        private void labelControl17_Click(object sender, EventArgs e)
        {

        }

        private void labelControl34_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void cboGetICD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboLamTron_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (load > 0)
            {
                DialogResult _result = MessageBox.Show("Việc thay đổi dạng làm tròn số sẽ ảnh hưởng đến dữ liệu, bạn vẫn muốn thay đổi?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                { e.Cancel = true; }
            }
        }

        private void cboDangSo_SL_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void cbo_KieuDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void btn_FilePath_Click(object sender, EventArgs e)
        {
            QLBV.CLS.frm_filePath_LIS frm = new QLBV.CLS.frm_filePath_LIS();
            frm.ShowDialog();
        }

        private void cbo_PPXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGioTu2_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtGioDen2_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void dt_NgayGiaMoi_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void cboHDSDThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void grvDSTK_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDSTK.GetRowCellValue(grvDSTK.FocusedRowHandle, colTaiKhoan) != null)
            {
                txtTenDN.Text = grvDSTK.GetRowCellValue(grvDSTK.FocusedRowHandle, colTaiKhoan).ToString();
            }
            loadHThongUser();
        }

        private void loadHThongUser()
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qht = (from ht in _data.HThong_User.Where(p => p.TenDN == txtTenDN.Text) select ht).FirstOrDefault();
            if (qht != null)
            {
                txtThukho.Text = qht.ThuKho;
                txtNguoilapbieu.Text = qht.NguoiLapBieu;
                txtKeToanVP.Text = qht.KeToanVPdv;
                txtKTBHNT.Text = qht.KeToanVPnt;
                txtKTBHNgt.Text = qht.KeToanVP;
                if (qht.MaKho != null)
                    lupKhoXuat.EditValue = qht.MaKho.Value;
                else
                    lupKhoXuat.EditValue = -1;
                if (qht.MaKhoKDNgoai != null)
                    lupKhoXuatDonNgoai.EditValue = qht.MaKhoKDNgoai.Value;
                else
                    lupKhoXuatDonNgoai.EditValue = -1;
                if (qht.FormatDate != null)
                    cboNgay.SelectedIndex = qht.FormatDate.Value;
                else
                    cboNgay.SelectedIndex = -1;
                txtTruongkhoa.Text = qht.TruongKhoa;
                if (qht.GetICD != null)
                    cboGetICD.SelectedIndex = qht.GetICD.Value;
                else
                    cboGetICD.SelectedIndex = -1;
                cboHDSDThuoc.SelectedIndex = qht.HDSDThuoc;
                txtDuongDan.Text = qht.DuongDan;
                string[] _fomatString = new string[2] { "", "" };
                _fomatString = QLBV_Library.QLBV_Ham.LayChuoi('/', qht.FormatString);
                cbo_DangSo_TT.Text = _fomatString[1];
                cboDangSo_SL.Text = _fomatString[0];
                cbo_KieuDoc.Text = qht.KieuDoc;
                chkFormatVn.Checked = qht.FormatVn ?? false;
                txtUserPass.Text = qht.HDDTInfo;
                checkAllow.Checked = qht.IsAllowCancelHDDT ?? false;
                chkDuyetPhieuThu.Checked = qht.DuyetPhieu ?? false;
            }
            else
            {
                txtThukho.ResetText();
                txtNguoilapbieu.ResetText();
                lupKhoXuat.EditValue = -1;
                lupKhoXuatDonNgoai.EditValue = -1;
                cboNgay.SelectedIndex = -1;
                txtTruongkhoa.ResetText();
                cboGetICD.SelectedIndex = -1;
                cboHDSDThuoc.SelectedIndex = -1;
                txtDuongDan.ResetText();
                cbo_DangSo_TT.Text = "";
                chkFormatVn.Checked = false;
                checkAllow.Checked = false;
                txtUserPass.Text = "";
                cboDangSo_SL.Text = "";
                cboInPhoi.SelectedIndex = -1;
                cbo_KieuDoc.ResetText();
                txtNguoilapbieu.Text = "";
                txtKeToanVP.Text = "";
                txtKTBHNT.Text = "";
                txtKTBHNgt.Text = "";

            }

            var colMaCBValue = grvDSTK.GetRowCellValue(grvDSTK.FocusedRowHandle, colMaCB);
            if (colMaCBValue != null)
            {
                var maCb = colMaCBValue.ToString();
                var canbo = _data.CanBoes.FirstOrDefault(f => f.MaCB == maCb);
                if (canbo != null)
                {
                    txtUserDoctor.Text = canbo.MaDinhDanh;
                    txtEmail.Text = canbo.Email;
                    txtPasswordDoctor.Text = Security.Decrypt(canbo.MKBacSi);
                    txtConfirmPasswordDoctor.Text = Security.Decrypt(canbo.MKBacSi);
                    txtSignaturePassword.Text = Security.Decrypt(canbo.MKChuKySo);
                    txtSignatureConfirmPassword.Text = Security.Decrypt(canbo.MKChuKySo);
                    Image chukyso = null;
                    if (canbo.ChuKySo != null)
                        chukyso = (Bitmap)(new ImageConverter()).ConvertFrom(canbo.ChuKySo);
                    LoadImage(pictureBox1, chukyso, defaultImage);
                }
            }
        }


        private void grvDSTK_DataSourceChanged(object sender, EventArgs e)
        {
            grvDSTK_FocusedRowChanged(null, null);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtPasswordDoctor.Text != txtConfirmPasswordDoctor.Text)
            {
                MessageBox.Show("MK đơn thuốc QG và Nhập lại MK không giống nhau. Xin vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtSignaturePassword.Text != txtSignatureConfirmPassword.Text)
            {
                MessageBox.Show("MK chữ ký số và Nhập lại MK không giống nhau. Xin vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var update = _data.HThong_User.Where(p => p.TenDN == txtTenDN.Text).ToList();
            if (update.Count <= 0)
            {
                HThong_User themmoi = new HThong_User();
                themmoi.TenDN = txtTenDN.Text;
                themmoi.GetICD = cboGetICD.SelectedIndex;
                themmoi.FormatString = cboDangSo_SL.Text + "/" + cbo_DangSo_TT.Text;
                themmoi.NguoiLapBieu = txtNguoilapbieu.Text;
                themmoi.ThuKho = txtThukho.Text;
                themmoi.DuongDan = txtDuongDan.Text;
                themmoi.KieuDoc = cbo_KieuDoc.Text;
                if (lupKhoXuat.EditValue != null)
                    themmoi.MaKho = Convert.ToInt32(lupKhoXuat.EditValue);
                if (lupKhoXuatDonNgoai.EditValue != null)
                    themmoi.MaKhoKDNgoai = Convert.ToInt32(lupKhoXuatDonNgoai.EditValue);
                themmoi.FormatDate = cboNgay.SelectedIndex;
                themmoi.HDSDThuoc = cboHDSDThuoc.SelectedIndex;
                themmoi.TruongKhoa = txtTruongkhoa.Text;
                themmoi.KeToanVPdv = txtKTVP.Text;
                themmoi.KeToanVP = txtKTBHNgt.Text;
                themmoi.KeToanVPnt = txtKTBHNT.Text;
                themmoi.FormatVn = chkFormatVn.Checked;
                themmoi.IsAllowCancelHDDT = checkAllow.Checked;
                themmoi.HDDTInfo = txtUserPass.Text;
                themmoi.DuyetPhieu = chkDuyetPhieuThu.Checked;
                _data.HThong_User.Add(themmoi);
                _data.SaveChanges();

                //
                //
                sbtLuu.Enabled = false;
                if (DungChung.Bien.TenDN != "vss20")
                {
                    DialogResult dialog = MessageBox.Show("Bấm OK để khởi động lại ứng dụng", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        Process.Start(Application.StartupPath + "\\QLBV.exe");
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
            else
            {
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin đã thay đổi?", "Sửa thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    foreach (var sua in update)
                    {
                        sua.FormatString = cboDangSo_SL.Text + "/" + cbo_DangSo_TT.Text;

                        sua.NguoiLapBieu = txtNguoilapbieu.Text;
                        sua.DuongDan = txtDuongDan.Text;
                        if (lupKhoXuat.EditValue != null)
                            sua.MaKho = Convert.ToInt32(lupKhoXuat.EditValue);
                        if (lupKhoXuatDonNgoai.EditValue != null)
                            sua.MaKhoKDNgoai = Convert.ToInt32(lupKhoXuatDonNgoai.EditValue);
                        sua.ThuKho = txtThukho.Text;
                        sua.FormatDate = cboNgay.SelectedIndex;
                        sua.GetICD = cboGetICD.SelectedIndex;
                        sua.KieuDoc = cbo_KieuDoc.Text;
                        sua.HDSDThuoc = cboHDSDThuoc.SelectedIndex;
                        sua.TruongKhoa = txtTruongkhoa.Text;
                        sua.KeToanVPdv = txtKeToanVP.Text;
                        sua.KeToanVP = txtKTBHNgt.Text;
                        sua.KeToanVPnt = txtKTBHNT.Text;
                        sua.FormatVn = chkFormatVn.Checked;
                        sua.IsAllowCancelHDDT = checkAllow.Checked;
                        sua.HDDTInfo = txtUserPass.Text;
                        sua.DuyetPhieu = chkDuyetPhieuThu.Checked;
                        _data.SaveChanges();
                    }

                    var colMaCBValue = grvDSTK.GetRowCellValue(grvDSTK.FocusedRowHandle, colMaCB);
                    if (colMaCBValue != null)
                    {
                        var maCb = colMaCBValue.ToString();
                        var canbo = _data.CanBoes.FirstOrDefault(f => f.MaCB == maCb);
                        if (canbo != null)
                        {
                            canbo.MaDinhDanh = txtUserDoctor.Text;
                            canbo.Email = txtEmail.Text;
                            canbo.MKBacSi = Security.Encrypt(txtPasswordDoctor.Text);
                            canbo.MKChuKySo = Security.Encrypt(txtSignaturePassword.Text);
                            canbo.ChuKySo = (System.Byte[])(new ImageConverter()).ConvertTo(pictureBox1.Image, Type.GetType("System.Byte[]"));

                            _data.SaveChanges();
                        }
                    }

                    sbtLuu.Enabled = false;
                    if (DungChung.Bien.TenDN != "vss20")
                    {
                        DialogResult dialog = MessageBox.Show("Bấm OK để khởi động lại ứng dụng", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            Process.Start(Application.StartupPath + "\\QLBV.exe");
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                    
                }
                else
                {
                    us_hethong_Load(sender, e);
                }
            }

            //var colMaCBValue = grvDSTK.GetRowCellValue(grvDSTK.FocusedRowHandle, colMaCB);
            //if (colMaCBValue != null)
            //{
            //    var maCb = colMaCBValue.ToString();
            //    var canbo = _data.CanBoes.FirstOrDefault(f => f.MaCB == maCb);
            //    if (canbo != null)
            //    {
            //        canbo.MaDinhDanh = txtUserDoctor.Text;
            //        canbo.Email = txtEmail.Text;
            //        canbo.MKBacSi = Security.Encrypt(txtPasswordDoctor.Text);
            //        canbo.MKChuKySo = Security.Encrypt(txtSignaturePassword.Text);
            //        canbo.ChuKySo = (System.Byte[])(new ImageConverter()).ConvertTo(pictureBox1.Image, Type.GetType("System.Byte[]"));

            //        _data.SaveChanges();
            //    }
            //}
        }

        private void txtSoVaoVien_EditValueChanged(object sender, EventArgs e)
        {

            sbtLuu.Enabled = true;
        }

        private void txtSoBA_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtSoCV_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtSoLuuTru_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtSoKB_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void rdInBK01_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void rdInBK02_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void rdInPThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void labelControl50_Click(object sender, EventArgs e)
        {

        }

        private void btnThietLap_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_SoPL_SoVV frm = new frm_SoPL_SoVV();
            frm.ShowDialog();
        }

        private void txtKTBHNgt_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void txtKTBHNT_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        public static bool EditAppSetting(string key, string value)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool AddAppSetting(string key, string value)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); return true;
            }
            catch
            {
                return false;
            }
        }
        private void cboCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (load > 0)
                {
                    if (!string.IsNullOrWhiteSpace(cboCom.Text) && !string.IsNullOrEmpty(cboCom.Text))
                    {
                        Library_LED.LEDCommunication Com = new Library_LED.LEDCommunication(cboCom.Text);
                        string a = Com.ShowView(0.ToString());
                        if (!string.IsNullOrEmpty(a))
                        {
                            MessageBox.Show(a);
                        }
                        else
                        {

                            if (DialogResult.Yes == MessageBox.Show("Lựa chọn thành công! bạn muốn lưu lại cấu hình?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {

                                if (!EditAppSetting("ComPort", cboCom.Text))
                                    AddAppSetting("ComPort", cboCom.Text);
                            }
                        }
                    }  //
                }

            }
            catch
            {

            }
        }

        private void cboSoPhong_EditValueChanged(object sender, EventArgs e)
        {
            if (load > 0)
                if (DialogResult.Yes == MessageBox.Show("Bạn muốn lưu lại cấu hình?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    if (!EditAppSetting("phongdoc", cboSoPhong.Text))
                        AddAppSetting("phongdoc", cboSoPhong.Text);
                }
        }

        private void txtWidthListBN_Leave(object sender, EventArgs e)
        {
            if (load > 0)
                if (DialogResult.Yes == MessageBox.Show("Bạn muốn lưu lại cấu hình?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    if (!EditAppSetting("withListBN", txtWidthListBN.Text))
                        AddAppSetting("withListBN", txtWidthListBN.Text);
                }
        }

        private void txtAutoCLS_Leave(object sender, EventArgs e)
        {
            if (load > 0)
                if (DialogResult.Yes == MessageBox.Show("Bạn muốn lưu lại cấu hình?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    if (!EditAppSetting("timerAutoResultCLS", txtAutoCLS.Text))
                        AddAppSetting("timerAutoResultCLS", txtAutoCLS.Text);
                }
        }

        private void cbo_PPXuat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void dt_NgayGiaMoi_DV_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void rdlChiTamThu_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcChiTamThu.Checked == true)
            {
                rdlInThuChi.SelectedIndex = 0;
                rdlInThuChi.Enabled = false;
            }
            else
            {
                rdlInThuChi.Enabled = true;
                rdlInThuChi.SelectedIndex = rdthuchi;
            }
            sbtLuu.Enabled = true;
        }

        private void rdlInThuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtWidthListBN_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void rdInBK6556_SelectedIndexChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void btnQLBVConfig_Click(object sender, EventArgs e)
        {
            frm_QLBV_Config frm = new frm_QLBV_Config();
            frm.ShowDialog();
        }

        private void chkTuDuyet_CheckedChanged(object sender, EventArgs e)
        {
            var HeThong = _data.HTHONGs.Single();
            HeThong.TuDongDuyet = chkTuDuyet.Checked;
            _data.SaveChanges();
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtConfirmPassword_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtUserPass_EditValueChanged(object sender, EventArgs e)
        {
            sbtLuu.Enabled = true;
        }

        private void txtPasswordDoctor_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void txtUserDoctor_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void txtConfirmPasswordDoctor_EditValueChanged(object sender, EventArgs e)
        {
            btn_Luu_US.Enabled = true;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image igame = new Bitmap(dialog.FileName);

                //pictureBox1.Image = new Bitmap(dialog.FileName);
                pictureBox1.Image = ResizeImage(igame, 32, 32);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public Image ResizeImage(Image image, int width, int height)
        {
            Bitmap new_image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, width, height);

            return new_image;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa ảnh không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
                LoadImage(pictureBox1, null, defaultImage);
        }
        private void LoadImage(PictureBox pictureBox, Image image, Image defaultImage)
        {
            if(image != null)
            {
                pictureBox.Image = image;
            }
            else
            {
                pictureBox.Image = defaultImage;
            }    
        }
       
    }
}