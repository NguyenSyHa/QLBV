using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_NhapKetQuaCDHA_01071 : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_NhapKetQuaCDHA_01071()
        {
            InitializeComponent();
        }
        private int _mabn, _idcls, _makp, _idcd;
        private string _tendv;
        private int suadoi = 0;
        private string DuongDan = "";
        private string[] DuongDan2 = new string[2];
        public frm_NhapKetQuaCDHA_01071(int mabn, int idcls, int makp, string TenDv, int IDCD)
        {
            InitializeComponent();
            _mabn = mabn;
            _idcls = idcls;
            _makp = makp;
            _tendv = TenDv;
            _idcd = IDCD;
        }
        private void frm_NhapKetQuaCDHA_01071_Load(object sender, EventArgs e)
        {
            dtpNgayThucHien.DateTime = DateTime.Now;
            loadbenhnhan(_mabn, _idcls);
            loadBSTH(_makp);
        }
        private void KiemTraDKControl()
        {
            if (lupBacSyThucHien.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn bác sỹ thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupBacSyThucHien.Focus();
                return;
            }
        }
        private void loadbenhnhan(int mabn, int idcls)
        {
            if (mabn != null)
            {
                var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                                join cls in data.CLS.Where(p => p.IdCLS == idcls) on bn.MaBNhan equals cls.MaBNhan
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                select new { bn, cls, cd, dv, clsct }).ToList();
                txtMaBenhNhan.Text = _mabn.ToString();
                txtTenBenhNhan.Text = benhnhan.First().bn.TenBNhan.ToUpper();
                txtTuoi.Text = benhnhan.First().bn.Tuoi.ToString();
                txtChanDoan.Text = benhnhan.First().cls.ChanDoan;
                txtTenDichVu.Text = benhnhan.First().dv.TenDV;
                txtTrangThai.Text = benhnhan.First().cls.Status == 1 ? "Đã thực hiện" : "Chưa thực hiện";
                txtKetLuan.Text = benhnhan.First().cd.KetLuan;
                txtLoiDanBacSy.Text = benhnhan.First().cd.LoiDan;
                lupBacSyThucHien.EditValue = benhnhan.First().cls.MaCBth;
                dtpNgayThucHien.DateTime = benhnhan.First().cls.NgayTH == null ? DateTime.Now : Convert.ToDateTime(benhnhan.First().cls.NgayTH);
                #region Siêu âm Doppler động mạch, tĩnh mạch chi dưới
                if (_tendv == "Siêu âm Doppler động mạch, tĩnh mạch chi dưới")
                {
                    PageChinh.SelectedTabPage = xtraTabPage1;
                    xtraTabPage1.PageEnabled = true;
                    if (benhnhan.Count > 0)
                    {
                        //kết quả
                        if (benhnhan.First().clsct.KetQua != null)
                        {
                            string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                            if (ketqua.Count() == 1)
                            {
                                txtHeDongMach.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            }
                            if (ketqua.Count() == 2)
                            {
                                txtHeDongMach.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                                txtHeTinhMachSau.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            } if (ketqua.Count() == 3)
                            {
                                txtHeDongMach.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                                txtHeTinhMachSau.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                                txtHeTinhMachNong.Text = ketqua[2].ToString() ?? ketqua[2].ToString();

                            }
                            if (ketqua.Count() == 4)
                            {
                                txtHeDongMach.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                                txtHeTinhMachSau.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                                txtHeTinhMachNong.Text = ketqua[2].ToString() ?? ketqua[2].ToString();
                                txtHeTinhMachXien.Text = ketqua[3].ToString() ?? ketqua[3].ToString();
                            }
                        }




                    }
                }
                #endregion

                #region Siêu âm Doppler tuyến vú
                if (_tendv == "Siêu âm Doppler tuyến vú")
                {
                    PageChinh.SelectedTabPage = pageDooplerTuyenVu;
                    pageDooplerTuyenVu.PageEnabled = true;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            txtTuyenVuPhai.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            txtTuyenVuPhai.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtTuyenVuTrai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                        }
                        if (ketqua.Count() == 3)
                        {
                            txtTuyenVuPhai.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtTuyenVuTrai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            txtHach.Text = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                        if (benhnhan.First().clsct.DuongDan != null)
                        {
                            if (File.Exists(benhnhan.First().clsct.DuongDan))
                            {
                                ptAnhTuyenVu.Image = Image.FromFile(benhnhan.First().clsct.DuongDan);
                            }
                            else
                            {
                                MessageBox.Show("Ảnh của bệnh nhân hiện tại đang được lưu ở thư mục: " + benhnhan.First().clsct.DuongDan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                    }
                }
                #endregion

                #region Siêu âm Doppler động mạch thận
                if (_tendv == "Siêu âm Doppler động mạch thận")
                {

                }
                #endregion
                #region Siêu âm Doppler tử cung phần phụ
                if (_tendv == "Siêu âm Doppler tử cung phần phụ")
                {
                    PageChinh.SelectedTabPage = pagetucungphanphu;
                    pagetucungphanphu.PageEnabled = true;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            txtTuCung.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            txtTuCung.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtBuongTrungPhai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                        }
                        if (ketqua.Count() == 3)
                        {
                            txtTuCung.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtBuongTrungPhai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            txtBuongtrungTrai.Text = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                        if (ketqua.Count() == 4)
                        {
                            txtTuCung.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtBuongTrungPhai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            txtBuongtrungTrai.Text = ketqua[2].ToString() ?? ketqua[2].ToString();
                            txtDouglas.Text = ketqua[3].ToString() ?? ketqua[3].ToString();
                        }
                        if (benhnhan.First().clsct.DuongDan2 != null)
                        {
                            string[] patd = benhnhan.First().clsct.DuongDan2.Split('|');
                            if (patd.Count() == 1)
                            {
                                KiemTraDuongDanFile(patd[0], ptAnhTuyenGiap1);
                            }
                            else if (patd.Count() == 2)
                            {
                                KiemTraDuongDanFile(patd[0], ptAnhTuCung1);
                                KiemTraDuongDanFile(patd[1], ptAnhTuCung2);
                            }
                        }
                    }

                }
                #endregion
                #region Doppler động mạch cảnh, Doppler xuyên sọ
                if (_tendv == "Doppler động mạch cảnh, Doppler xuyên sọ")
                {

                }
                #endregion
                #region Siêu âm Doppler u tuyến, hạch vùng cổ
                if (_tendv == "Siêu âm Doppler u tuyến, hạch vùng cổ")
                {
                    PageChinh.SelectedTabPage = pageutuyenhachvungco;
                    pageutuyenhachvungco.PageEnabled = true;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            txtThuyPhaiTuyenGiap.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            txtThuyPhaiTuyenGiap.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtThuyTraiTuyenGiap.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                        }
                        if (ketqua.Count() == 3)
                        {
                            txtThuyPhaiTuyenGiap.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtThuyTraiTuyenGiap.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            txtEoTuyenGiap.Text = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                        if (ketqua.Count() == 4)
                        {
                            txtThuyPhaiTuyenGiap.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtThuyTraiTuyenGiap.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                            txtEoTuyenGiap.Text = ketqua[2].ToString() ?? ketqua[2].ToString();
                            txtHachTuyenGiap.Text = ketqua[3].ToString() ?? ketqua[3].ToString();
                        }
                        if (benhnhan.First().clsct.DuongDan2 != null)
                        {
                            string[] patd = benhnhan.First().clsct.DuongDan2.Split('|');
                            if (patd.Count() == 1)
                            {
                                KiemTraDuongDanFile(patd[0], ptAnhTuyenGiap1);
                            }
                            else if (patd.Count() == 2)
                            {
                                KiemTraDuongDanFile(patd[0], ptAnhTuyenGiap1);
                                KiemTraDuongDanFile(patd[1], ptAnhTuyenGiap2);
                            }
                        }
                    }
                }
                #endregion
                #region Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên
                if (_tendv == "Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên")
                {
                    PageChinh.SelectedTabPage = pageSieuAmTinhHoan2Ben;
                    pageSieuAmTinhHoan2Ben.PageEnabled = true;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            txtTinhHoanPhai.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            txtTinhHoanPhai.Text = ketqua[0].ToString() ?? ketqua[0].ToString();
                            txtTinhHoanTrai.Text = ketqua[1].ToString() ?? ketqua[1].ToString();
                        }
                    }

                }
                #endregion

                if (benhnhan.First().cls.Status == 1)
                {
                    econtrol(false);
                    btnluu.Enabled = false;
                    btnSua.Enabled = true;
                }
                else
                {
                    econtrol(true);
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }

            }
        }
        private void KiemTraDuongDanFile(string patd, PictureEdit pt)
        {
            if (File.Exists(patd))
            {
                pt.Image = Image.FromFile(patd);
            }

        }
        private void econtrol(bool T)
        {
            txtTuyenVuPhai.Enabled = txtTuyenVuTrai.Enabled = txtHach.Enabled = btnChonAnh.Enabled = btnXoaAnh.Enabled = txtTinhHoanPhai.Enabled = txtTinhHoanTrai.Enabled = txtHeDongMach.Enabled = txtHeTinhMachNong.Enabled = txtHeTinhMachSau.Enabled = txtKetLuan.Enabled = txtLoiDanBacSy.Enabled = T;
        }
        private void loadBSTH(int _maKP)
        {
            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            //if(DungChung.Bien.PLoaiKP=DungChung.Bien.st_PhanLoaiKP.Admin)
            lupBacSyThucHien.Properties.DataSource = c.ToList();
        }
        private void btnluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KiemTraDKControl();
            save();
        }
        private void save()
        {
            CLSct ct = data.CLScts.Where(p => p.IDCD == _idcd).Single();
            string ketqua = "";
            #region Siêu âm Doppler động mạch, tĩnh mạch chi dưới
            if (_tendv == "Siêu âm Doppler động mạch, tĩnh mạch chi dưới")
            {
                ketqua = txtHeDongMach.Text + "|" + txtHeTinhMachSau.Text + "|" + txtHeTinhMachNong.Text + "|" + txtHeTinhMachXien.Text;

            }
            #endregion

            #region Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên
            if (_tendv == "Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên")
            {
                ketqua = txtTinhHoanPhai.Text + "|" + txtTinhHoanTrai.Text;
            }

            #endregion

            #region Siêu âm tuyến vú
            if (_tendv == "Siêu âm Doppler tuyến vú")
            {
                ketqua = txtTuyenVuPhai.Text + "|" + txtTuyenVuTrai.Text + "|" + txtHach.Text;

                if (DuongDan != "")
                {
                    ct.DuongDan = DuongDan;
                }
            }
            #endregion

            #region Siêu âm Doppler u tuyến, hạch vùng cổ
            if (_tendv.Contains("Siêu âm Doppler u tuyến, hạch vùng cổ"))
            {
                ketqua = txtThuyPhaiTuyenGiap.Text + "|" + txtThuyTraiTuyenGiap.Text + "|" + txtEoTuyenGiap.Text + "|" + txtHachTuyenGiap.Text;
                string patd1 = DuongDan2[0] == null ? "" : DuongDan2[0].ToString();
                string patd2 = DuongDan2[1] == null ? "" : DuongDan2[1].ToString();
                ct.DuongDan2 = patd1 + "|" + patd2;
            }
            #endregion

            if (_tendv == "Siêu âm Doppler tử cung phần phụ")
            {
                ketqua = txtTuCung.Text + "|" + txtBuongTrungPhai.Text + "|" + txtBuongtrungTrai.Text + "|" + txtDouglas.Text;
                string patd1 = DuongDan2[0] == null ? "" : DuongDan2[0].ToString();
                string patd2 = DuongDan2[1] == null ? "" : DuongDan2[1].ToString();
                ct.DuongDan2 = patd1 + "|" + patd2;
            }

            ct.KetQua = ketqua;
            ct.Status = 1;

            ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
            cd.KetLuan = txtKetLuan.Text;
            cd.LoiDan = txtLoiDanBacSy.Text;
            cd.Status = 1;

            CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
            cls.Status = 1;
            cls.NgayTH = dtpNgayThucHien.DateTime;
            cls.MaCBth = lupBacSyThucHien.EditValue.ToString() == null ? "" : lupBacSyThucHien.EditValue.ToString();
            if (data.SaveChanges() > 0)
            {
                XtraMessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnluu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnluu.Enabled = true;
            econtrol(true);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult rf = XtraMessageBox.Show("Bạn chắc chắn muốn xóa!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rf == DialogResult.OK)
            {
                CLSct ct = data.CLScts.Where(p => p.IDCD == _idcd).Single();
                ct.KetQua = "";
                ct.Status = 0;
                ct.DuongDan = "";
                ct.DuongDan2 = "";
                ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
                cd.KetLuan = "";
                cd.LoiDan = "";
                cd.Status = 0;
                CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
                cls.Status = 0;
                cls.NgayTH = dtpNgayThucHien.DateTime;
                cls.MaCBth = "";

                if (data.SaveChanges() > 0)
                {
                    XtraMessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSua.Enabled = false;
                    btnluu.Enabled = true;
                    econtrol(true);
                    btnXoa.Enabled = false;
                    Cleartext();
                    loadbenhnhan(_mabn, _idcls);
                }
            }
        }
        private void Cleartext()
        {
            txtLoiDanBacSy.Text = txtKetLuan.Text = txtHeDongMach.Text = txtHeTinhMachSau.Text = txtHeTinhMachNong.Text = txtThuyPhaiTuyenGiap.Text = txtThuyTraiTuyenGiap.Text = txtHach.Text = txtEoTuyenGiap.Text = txtTuyenVuPhai.Text = txtTuyenVuTrai.Text = txtHach.Text = "";
            ptAnhTuyenGiap1.Image = ptAnhTuyenGiap2.Image = ptAnhTuyenVu.Image = null;
        }
        #region class các phiếu
        private class RepPhieuSieuAmDoopler
        {
            public string DiaChiBV { get; set; }
            public string DienThoai { get; set; }
            public string HoVaTen { get; set; }
            public int? Tuoi { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string ChanDoan { get; set; }
            #region Tinh hoàn
            public string TinhHoanPhai { get; set; }
            public string TinhHoanTrai { get; set; }
            #endregion
            #region động mạch tĩnh mạch
            public string HeDongMach { get; set; }
            public string HeTinhMachSau { get; set; }
            public string HeTinhMachNong { get; set; }
            public string HeTinhMachXien { get; set; }
            #endregion
            #region tuyến vú
            public string TuyenVuPhai { get; set; }
            public string TuyenVuTrai { get; set; }
            public string Hach { get; set; }
            public Image AnhTuyenVu { get; set; }
            #endregion
            #region Tuyến giáp
            public string ThuyPhaiTuyenGiap { get; set; }
            public string ThuyTraiTuyenGiap { get; set; }
            public string EoTuyenGiap { get; set; }
            public string HachTuyenGiap { get; set; }
            public Image AnhTuyenGiap1 { get; set; }
            public Image AnhTuyenGiap2 { get; set; }

            #endregion
            #region Tử Cung
            public string TuCung { get; set; }
            public string BuongTrungPhai { get; set; }
            public string BuongTrungTrai { get; set; }
            public string Douglas { get; set; }
            public Image AnhTuCung1 { get; set; }
            public Image AnhTuCung2 { get; set; }
            #endregion
            public string SDTCanBo { get; set; }
            public string KetLuan { get; set; }
            public string TimeLocation { get; set; }
            public string BSTH { get; set; }

        }

        #endregion

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                            join cls in data.CLS.Where(p => p.IdCLS == _idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            List<RepPhieuSieuAmDoopler> repbc = new List<RepPhieuSieuAmDoopler>();
            #region Siêu âm Doppler động mạch, tĩnh mạch chi dưới
            if (_tendv == "Siêu âm Doppler động mạch, tĩnh mạch chi dưới")
            {

                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.SDT = DungChung.Bien.SDTCQ;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            r.HeDongMach = " - Hệ động mạch: " + ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            r.HeDongMach = "- Hệ động mạch: " + ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.HeTinhMachSau = "- Hệ tĩnh mạch sâu: " + ketqua[1].ToString() ?? ketqua[1].ToString();
                        } if (ketqua.Count() == 3)
                        {
                            r.HeDongMach = " - Hệ động mạch: " + ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.HeTinhMachSau = "- Hệ tĩnh mạch sâu: " + ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.HeTinhMachNong = "- Hệ tĩnh mạch nông: " + ketqua[2].ToString() ?? ketqua[0].ToString();

                        } if (ketqua.Count() == 4)
                        {
                            r.HeDongMach = " - Hệ động mạch: " + ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.HeTinhMachSau = "- Hệ tĩnh mạch sâu: " + ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.HeTinhMachNong = "- Hệ tĩnh mạch nông: " + ketqua[2].ToString() ?? ketqua[0].ToString();
                            r.HeTinhMachXien = "- Hệ tĩnh mạch xiên: " + ketqua[3].ToString() ?? ketqua[3].ToString();
                        }
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày" + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                    }
                    repbc.Add(r);

                }

                DungChung.Ham.Print(DungChung.PrintConfig.rep_CDHA_SieuAmDongMachTinhMachChiDuoi_01071, repbc, new Dictionary<string, object>(), false);
            }
            #endregion
            #region Siêu âm Doppler tuyến vú
            if (_tendv == "Siêu âm Doppler tuyến vú")
            {
                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.SDT = DungChung.Bien.SDTCQ;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            r.TuyenVuPhai = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            r.TuyenVuPhai = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.TuyenVuTrai = ketqua[1].ToString() ?? ketqua[1].ToString();
                        } if (ketqua.Count() == 3)
                        {
                            r.TuyenVuPhai = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.TuyenVuTrai = ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.Hach = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                    }
                    if (benhnhan.First().clsct.DuongDan != "")
                    {
                        r.AnhTuyenVu = Image.FromFile(benhnhan.First().clsct.DuongDan);
                    }
                    else
                    {
                        r.AnhTuyenVu = null;
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày" + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                    }
                    if (benhnhan.First().cls.MaCBth != null)
                    {
                        string macb = benhnhan.First().cls.MaCBth;
                        r.SDTCanBo = data.CanBoes.Where(p => p.MaCB == macb).First().SoDT != null ? "ĐT: " + data.CanBoes.Where(p => p.MaCB == macb).First().SoDT : "ĐT:";
                    }

                    repbc.Add(r);

                }

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerTuyenVu_01071, repbc, new Dictionary<string, object>(), false);
            }
            #endregion 
            #region Siêu âm Doppler tử cung phần phụ
            if (_tendv == "Siêu âm Doppler tử cung phần phụ")
            {
                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.SDT = DungChung.Bien.SDTCQ;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            r.TuCung = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            r.TuCung = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.BuongTrungPhai = ketqua[1].ToString() ?? ketqua[1].ToString();
                        } if (ketqua.Count() == 3)
                        {
                            r.TuCung = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.BuongTrungPhai = ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.BuongTrungTrai = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                        if (ketqua.Count() == 4)
                        {
                            r.TuCung = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.BuongTrungPhai = ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.BuongTrungTrai = ketqua[2].ToString() ?? ketqua[2].ToString();
                            r.Douglas = ketqua[3].ToString() ?? ketqua[3].ToString();
                        }
                    }
                    if (benhnhan.First().clsct.DuongDan2 != "")
                    {
                        string[] patd = benhnhan.First().clsct.DuongDan2.Split('|');
                        if (patd[0] != "")
                        {
                            r.AnhTuCung1 = Image.FromFile(patd[0]);

                        }
                        if (patd[1] != "")
                        {
                            r.AnhTuCung2 = Image.FromFile(patd[1]);
                        }
                    }
                    else
                    {
                        r.AnhTuCung1 = null;
                        r.AnhTuCung2 = null;
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày" + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);
                    }

                    repbc.Add(r);

                }

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerPhanPhu_01071, repbc, new Dictionary<string, object>(), false);
            }
            #endregion     
            #region Siêu âm Doppler u tuyến, hạch vùng cổ
            if (_tendv == "Siêu âm Doppler u tuyến, hạch vùng cổ")
            {
                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.SDT = DungChung.Bien.SDTCQ;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            r.ThuyPhaiTuyenGiap = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            r.ThuyPhaiTuyenGiap = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.ThuyTraiTuyenGiap = ketqua[1].ToString() ?? ketqua[1].ToString();
                        } if (ketqua.Count() == 3)
                        {
                            r.ThuyPhaiTuyenGiap = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.ThuyTraiTuyenGiap = ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.EoTuyenGiap = ketqua[2].ToString() ?? ketqua[2].ToString();

                        }
                        if (ketqua.Count() == 4)
                        {
                            r.ThuyPhaiTuyenGiap = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.ThuyTraiTuyenGiap = ketqua[1].ToString() ?? ketqua[1].ToString();
                            r.EoTuyenGiap = ketqua[2].ToString() ?? ketqua[2].ToString();
                            r.HachTuyenGiap = ketqua[3].ToString() ?? ketqua[3].ToString();
                        }
                    }
                    if (benhnhan.First().clsct.DuongDan2 != "")
                    {
                        string[] patd = benhnhan.First().clsct.DuongDan2.Split('|');
                        if (patd[0] != "")
                        {
                            r.AnhTuyenGiap1 = Image.FromFile(patd[0]);

                        }
                        if (patd[1] != "")
                        {
                            r.AnhTuyenGiap2 = Image.FromFile(patd[1]);
                        }
                    }
                    else
                    {
                        r.AnhTuyenGiap1 = null;
                        r.AnhTuyenGiap2 = null;
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày" + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                    }

                    repbc.Add(r);

                }

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerUTuyenHachVungCo_01071, repbc, new Dictionary<string, object>(), false);
            }
            #endregion
            #region Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên
            if (_tendv == "Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên")
            {
                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.SDT = DungChung.Bien.SDTCQ;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        if (ketqua.Count() == 1)
                        {
                            r.TinhHoanPhai = ketqua[0].ToString() ?? ketqua[0].ToString();
                        }
                        if (ketqua.Count() == 2)
                        {
                            r.TinhHoanPhai = ketqua[0].ToString() ?? ketqua[0].ToString();
                            r.TinhHoanTrai = ketqua[1].ToString() ?? ketqua[1].ToString();
                        }
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày" + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                    }
                    repbc.Add(r);

                }

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_Sieuamdopplertinhhoan_01071, repbc, new Dictionary<string, object>(), false);
            }
            #endregion



        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnhTuyenVu, "DopplerTuyenVu");
        }

        private void chonAnh(PictureEdit pt, string tendv)
        {

            bool tontai = false;
            if (pt.Image != null)
            {
                tontai = true;
            }
            else
            {
                tontai = false;

            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;

                        _tenfileanh += _mabn + "_" + _idcls + "_" + tendv + "_" + ex;
                        DuongDan = layTenFileAnh(fileName, _tenfileanh);
                    }

                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    string ex = Path.GetExtension(op.FileName);
                    if (!string.IsNullOrEmpty(fileName))
                        pt.Image = Image.FromFile(fileName);
                    else
                        pt.Image = null;

                    string _tenfileanh = DungChung.Bien.DuongDan;
                    _tenfileanh += _mabn + "_" + _idcls + "_" + tendv + "_" + ex;
                    DuongDan = layTenFileAnh(fileName, _tenfileanh);
                }
            }

        } // số lượng 1 ảnh lưu vào hàm này
        private void chonAnh(PictureEdit pt, string tendv, int i)
        {

            bool tontai = false;
            if (pt.Image != null)
            {
                tontai = true;
            }
            else
            {
                tontai = false;

            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;

                        _tenfileanh += _mabn + "_" + _idcls + "_" + tendv + "_" + ex;
                        DuongDan2[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }

                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    string ex = Path.GetExtension(op.FileName);
                    if (!string.IsNullOrEmpty(fileName))
                        pt.Image = Image.FromFile(fileName);
                    else
                        pt.Image = null;

                    string _tenfileanh = DungChung.Bien.DuongDan;
                    _tenfileanh += _mabn + "_" + _idcls + "_" + tendv + "_" + ex;
                    DuongDan2[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                }
            }

        } // số lượng 2 trở lên ảnh lưu vào hàm này
        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh, true);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a, true);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void txtXoaAnh_Click(object sender, EventArgs e)
        {
            XoaAnh(ptAnhTuyenVu);
        }
        private void XoaAnh(PictureEdit pt)
        {
            if (pt.Image != null)
            {
                pt.Image = null;
                DuongDan = "";
            }
        }
        private void XoaAnh(PictureEdit pt, int value)
        {
            if (pt.Image != null)
            {
                pt.Image = null;
                DuongDan2[value - 1] = "";
            }
        }
        private void btnChonUTuyen1_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnhTuyenGiap1, "TuyenGiap", 1);
        }

        private void btnChonUTuyen2_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnhTuyenGiap2, "TuyenGiap", 2);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            XoaAnh(ptAnhTuyenGiap1, 1);

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            XoaAnh(ptAnhTuyenGiap2, 2);
        }

        private void btnChonAnhTuCung1_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnhTuCung1, "TuCung", 1);
        }

        private void btnChonAnhTuCung2_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnhTuCung2, "TuCung", 2);
        }

        private void btnXoaAnhTuCung1_Click(object sender, EventArgs e)
        {
            XoaAnh(ptAnhTuCung1, 1);
        }

        private void btnXoaAnhTuCung2_Click(object sender, EventArgs e)
        {
            XoaAnh(ptAnhTuCung2, 2);
        }

        private void btnKetQuaMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InKetQuaMau();
        }
        private void InKetQuaMau()
        {
            if (_tendv == "Siêu âm Doppler động mạch, tĩnh mạch chi dưới")
            {
                txtHeDongMach.Text = "ĐM đùi nông, đùi sâu, ĐM khoeo, ĐM chày nước và chày sau hai bên. Khẩu kính bình thường, Thành mạch mảnh, Không có sơ vữa. Tốc độ dòng chày và phổ Doppler trong giới hạn bình thường.";
                txtHeTinhMachSau.Text = "Tĩnh mạch đùi, tĩnh mạch khoeo ấn xẹp, không có huyết khối, không giãn, không có dấu hiệu suy van tĩnh mạch";
                txtHeTinhMachNong.Text = "Tĩnh mạch hiển lớn, hiển bé  ấn xẹp, không có huyết khối, không giãn, không có dấu hiệu suy van tĩnh mạch.";
                txtHeTinhMachXien.Text = "Các tĩnh mạch xiên chi dưới hai bên không giãn, không có huyết khối.";
            }
            if (_tendv == "Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên")
            {
                txtTinhHoanPhai.Text = "- Tinh hoàn phải: Kích thước bình thường, nhu mô đều, không có khối bất thường. \r\n-	Mào tinh hoàn phải: Kích thước bình thường, nhu mô đều, không có khối bất thường.  \r\n -	Màng tinh hoàn phải: Không có dịch.\r\n -	Trên siêu âm Doppler màu nhu mô tưới máu đều\r\n -	Không thấy giãn tĩnh mạch tinh mạch tinh bên phải.";
                txtTinhHoanTrai.Text = "-	Tinh hoàn trái: Kích thước bình thường, nhu mô đều, không có khối bất thường.  \r\n -	Mào tinh hoàn trái: Kích thước bình thường, nhu mô đều, không có khối bất thường.  \r\n -	Màng tinh hoàn trái: Không có dịch. \r\n -	Trên siêu âm Doppler màu nhu mô tưới máu đều\r\n -	Không thấy giãn tĩnh mạch tinh bên trái.";
            }
            if (_tendv == "Siêu âm Doppler tử cung phần phụ")
            {
                txtTuCung.Text = "- Kích thước tử cung bình thường.  \r\n - Âm vang cơ tử cung đều, không thấy khối khu trú.  \r\n -  Niêm mặc tử cung mm.  \r\n Không thấy bất thường trong tử cung.";
                txtBuongTrungPhai.Text = "Kích thước bình thường có vài nang nhỏ.";
                txtBuongtrungTrai.Text = "Kích thước bình thường có vài nang nhỏ.";
                txtDouglas.Text = "Không có dịch";
            } if (_tendv == "Siêu âm Doppler tuyến vú")
            {
                txtTuyenVuPhai.Text = "Nhu mô đều, không thấy khối khu trú. \r\n Tổ chức mỡ xen kẽ tổ chức ống tuyến đều. \r\n ống tuyến không dãn. ";
                txtTuyenVuTrai.Text = "Nhu mô đều, không thấy khối khu trú. \r\n Tổ chức mỡ xen kẽ tổ chức ống tuyến đều. \r\n ống tuyến không dãn.";
                txtHach.Text = "Không thấy hạch nách hai bên.";
            }
            if (_tendv=="Siêu âm Doppler u tuyến, hạch vùng cổ")
            {
                txtThuyPhaiTuyenGiap.Text = "Kích thước bình thường \r\n Nhu mô đều, không có khối khu trú \r\n Không tăng sinh mạch trên phiếu siêu âm màu ";
                txtThuyTraiTuyenGiap.Text = "Kích thước bình thường \r\n Nhu mô đều, không có khối khu trú \r\n Không tăng sinh mạch trên phiếu siêu âm màu ";
                txtEoTuyenGiap.Text = "Mỏng";
                txtHachTuyenGiap.Text = "Không thấy hạch to bất thường vùng cổ";
            }
        }
    }
}