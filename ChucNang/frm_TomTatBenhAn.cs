using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_TomTatBenhAn : Form
    {
        int maBNhan;
        bool isEdit = false;
        public frm_TomTatBenhAn(int _maBNhan)
        {
            InitializeComponent();
            this.maBNhan = _maBNhan;
            LoadDataCombo();
        }

        private void frm_TomTatBenhAn_Load(object sender, EventArgs e)
        {
            ResetControl();
            LoadBenhNhanInfo(this.maBNhan);
            EnableControl(isEdit);
        }

        private void LoadDataCombo()
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dscb = dataContext.CanBoes.Where(p => p.Status == 1).Where(p => p.ChucVu.ToLower().Contains("gđ") || p.ChucVu.ToLower().Contains("giám đốc") || p.ChucVu.ToLower().Contains("pgđ") || p.ChucVu.ToLower().Contains("phó giám đôc")).ToList();
            cboNguoiDaiDien.Properties.DataSource = dscb;
        }

        private void ResetControl()
        {
            txtHoTen.ResetText();
            txtDonVi.ResetText();
            txtNgaySinh.ResetText();
            cboNguoiDaiDien.EditValue = null;
            txtGhiChu.ResetText();
            txtPPDieuTri.ResetText();
            txtQuaTrinhBenhLy.ResetText();
            txtSoBHXH.ResetText();
            txtSoTheBHYT.ResetText();
            txtHoTenCha.ResetText();
            txtHoTenMe.ResetText();
            txtTomTatKQCLS.ResetText();
            dtNgayChetCon.EditValue = null;
            dtNgayChungTu.EditValue = null;
            dtNgaySinhCon.EditValue = null;
            spSoConChet.EditValue = null;
            EnableControl(false);
        }

        private void EnableControl(bool edit)
        {
            btnEdit.Enabled = edit;
            btnXoa.Enabled = edit;
            btnPrint.Enabled = edit;
            txtDonVi.Enabled = !edit;
            cboNguoiDaiDien.Enabled = !edit;
            txtGhiChu.Enabled = !edit;
            txtPPDieuTri.Enabled = !edit;
            txtQuaTrinhBenhLy.Enabled = !edit;
            txtSoBHXH.Enabled = !edit;
            txtHoTenCha.Enabled = !edit;
            txtHoTenMe.Enabled = !edit;
            txtTomTatKQCLS.Enabled = !edit;
            dtNgayChetCon.Enabled = !edit;
            dtNgayChungTu.Enabled = !edit;
            dtNgaySinhCon.Enabled = !edit;
            spSoConChet.Enabled = !edit;
        }

        private void LoadBenhNhanInfo(int _maBNhan)
        {
            isEdit = false;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var benhNhan = dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            if (benhNhan != null)
            {
                txtHoTen.Text = benhNhan.TenBNhan;
                txtSoTheBHYT.Text = benhNhan.SThe;
                txtNgaySinh.Text = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                if (!string.IsNullOrWhiteSpace(benhNhan.SThe))
                    txtSoBHXH.Text = benhNhan.SThe.Substring(5);
            }

            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 1);
            if (hsctBHXH != null)
            {
                isEdit = true;
                txtGhiChu.Text = hsctBHXH.GHI_CHU;
                txtPPDieuTri.Text = hsctBHXH.PP_DIEUTRI;
                txtDonVi.Text = hsctBHXH.DON_VI;
                txtQuaTrinhBenhLy.Text = hsctBHXH.QT_BENHLY;
                txtSoBHXH.Text = hsctBHXH.MA_BHXH;
                txtTomTatKQCLS.Text = hsctBHXH.TOMTAT_KQ;
                spSoConChet.EditValue = hsctBHXH.SO_CONCHET;
                dtNgayChetCon.EditValue = hsctBHXH.NGAY_CHETCON;
                dtNgaySinhCon.EditValue = hsctBHXH.NGAY_SINHCON;
                dtNgayChungTu.EditValue = hsctBHXH.NGAY_CT;
                cboNguoiDaiDien.EditValue = hsctBHXH.NGUOI_DAI_DIEN;
                txtHoTenCha.Text = hsctBHXH.HO_TEN_CHA;
                txtHoTenMe.Text = hsctBHXH.HO_TEN_ME;
            }
            else
            {
                var raVien = dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
                if (raVien != null)
                {
                    dtNgayChungTu.EditValue = raVien.NgayRa;
                    txtPPDieuTri.Text = raVien.PPDTr;
                }
                string _ketqua = "";
                if (DungChung.Bien.MaBV != "30303")
                {
                    var kqcls = (from cls in dataContext.CLS.Where(p => p.MaBNhan == _maBNhan && p.Status == 1)
                                 join chidinh in dataContext.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                 join clsct in dataContext.CLScts on chidinh.IDCD equals clsct.IDCD
                                 join dv in dataContext.DichVus on chidinh.MaDV equals dv.MaDV
                                 join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join nhom in dataContext.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                 where (nhom.TenNhomCT == ("Chẩn đoán hình ảnh"))
                                 select new { dv.TenDV, clsct.KetQua }).ToList();
                    foreach (var a in kqcls)
                    {
                        _ketqua += (!string.IsNullOrWhiteSpace(a.KetQua) ? (a.KetQua + ". ") : " ");
                    }
                    if (DungChung.Bien.MaBV == "30009")
                    {
                        var kqcls_XN = (from cls in dataContext.CLS.Where(p => p.MaBNhan == _maBNhan && p.Status == 1)
                                        join chidinh in dataContext.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                        join clsct in dataContext.CLScts on chidinh.IDCD equals clsct.IDCD
                                        join dvct in dataContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                        join dv in dataContext.DichVus on dvct.MaDV equals dv.MaDV
                                        join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                        join nhom in dataContext.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                        where (nhom.TenNhomCT == ("Xét nghiệm"))
                                        select new { tn.IdTieuNhom, dv.TenDV, dvct.TenDVct, clsct.KetQua }).OrderBy(p => p.IdTieuNhom).ThenBy(p => p.TenDV).ToList();
                        _ketqua += "\n";
                        foreach (var a in kqcls_XN)
                        {
                            _ketqua += a.TenDVct + ": " + a.KetQua + ". ";
                        }
                    }
                }
                txtTomTatKQCLS.Text = _ketqua;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 1);
            if (hsctBHXH != null)
            {
                if (hsctBHXH.IS_SEND == true)
                {
                    MessageBox.Show("Hồ sơ đã được gửi không thể xóa");
                    return;
                }
                dataContext.HSCT_BHXH.Remove(hsctBHXH);
                if (dataContext.SaveChanges() >= 0)
                {
                    MessageBox.Show("Xóa thành công");
                    frm_TomTatBenhAn_Load(null, null);
                }
            }
        }

        private bool CheckSave()
        {
            bool rs = true;
            if (dtNgayChungTu.EditValue == null)
            {
                MessageBox.Show("Chưa nhập ngày chứng từ");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSoBHXH.Text))
            {
                MessageBox.Show("Chưa nhập số BHXH");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPPDieuTri.Text))
            {
                MessageBox.Show("Chưa nhập phương pháp điều trị");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuaTrinhBenhLy.Text))
            {
                MessageBox.Show("Chưa nhập quá trình bệnh lý");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTomTatKQCLS.Text))
            {
                MessageBox.Show("Chưa nhập tóm tắt kết quả CLS");
                return false;
            }
            if (txtSoBHXH.Text.Length != 10)
            {
                MessageBox.Show("Số BHXH phải đủ 10 ký tự");
                return false;
            }
            return rs;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (!CheckSave())
            {
                return;
            }
            if (isEdit)
            {
                var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 1);
                if (hsctBHXH != null)
                {
                    if (hsctBHXH.IS_SEND == true)
                    {
                        MessageBox.Show("Hồ sơ đã được gửi không thể sửa");
                        return;
                    }
                    hsctBHXH.DON_VI = txtDonVi.Text;
                    hsctBHXH.GHI_CHU = txtGhiChu.Text;
                    hsctBHXH.MA_BHXH = txtSoBHXH.Text;
                    hsctBHXH.HO_TEN_CHA = txtHoTenCha.Text;
                    hsctBHXH.HO_TEN_ME = txtHoTenMe.Text;
                    if (dtNgayChetCon.EditValue != null)
                    {
                        hsctBHXH.NGAY_CHETCON = dtNgayChetCon.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_CHETCON = null;
                    if (dtNgaySinhCon.EditValue != null)
                    {
                        hsctBHXH.NGAY_SINHCON = dtNgaySinhCon.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_SINHCON = null;
                    if (dtNgayChungTu.EditValue != null)
                    {
                        hsctBHXH.NGAY_CT = dtNgayChungTu.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_CT = null;
                    if (cboNguoiDaiDien.EditValue != null)
                        hsctBHXH.NGUOI_DAI_DIEN = cboNguoiDaiDien.EditValue.ToString();
                    else
                        hsctBHXH.NGUOI_DAI_DIEN = null;
                    hsctBHXH.PP_DIEUTRI = txtPPDieuTri.Text;
                    hsctBHXH.QT_BENHLY = txtQuaTrinhBenhLy.Text;
                    hsctBHXH.TOMTAT_KQ = txtTomTatKQCLS.Text;
                    if (spSoConChet.EditValue != null)
                        hsctBHXH.SO_CONCHET = Convert.ToInt32(spSoConChet.EditValue);
                    else
                        hsctBHXH.SO_CONCHET = null;
                    dataContext.SaveChanges();
                    if (dataContext.SaveChanges() >= 0)
                    {
                        rs = true;
                    }
                }
            }
            else
            {
                HSCT_BHXH hsctBHXHNew = new HSCT_BHXH();
                hsctBHXHNew.DON_VI = txtDonVi.Text;
                hsctBHXHNew.GHI_CHU = txtGhiChu.Text;
                hsctBHXHNew.MA_BHXH = txtSoBHXH.Text;
                hsctBHXHNew.HO_TEN_CHA = txtHoTenCha.Text;
                hsctBHXHNew.HO_TEN_ME = txtHoTenMe.Text;
                if (dtNgayChetCon.EditValue != null)
                {
                    hsctBHXHNew.NGAY_CHETCON = dtNgayChetCon.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_CHETCON = null;
                if (dtNgaySinhCon.EditValue != null)
                {
                    hsctBHXHNew.NGAY_SINHCON = dtNgaySinhCon.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_SINHCON = null;
                if (dtNgayChungTu.EditValue != null)
                {
                    hsctBHXHNew.NGAY_CT = dtNgayChungTu.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_CT = null;
                if (cboNguoiDaiDien.EditValue != null)
                    hsctBHXHNew.NGUOI_DAI_DIEN = cboNguoiDaiDien.EditValue.ToString();
                else
                    hsctBHXHNew.NGUOI_DAI_DIEN = null;
                hsctBHXHNew.PP_DIEUTRI = txtPPDieuTri.Text;
                hsctBHXHNew.QT_BENHLY = txtQuaTrinhBenhLy.Text;
                hsctBHXHNew.TOMTAT_KQ = txtTomTatKQCLS.Text;
                if (spSoConChet.EditValue != null)
                    hsctBHXHNew.SO_CONCHET = Convert.ToInt32(spSoConChet.EditValue);
                else
                    hsctBHXHNew.SO_CONCHET = null;
                hsctBHXHNew.LOAI = 1;
                hsctBHXHNew.MaBNhan = this.maBNhan;
                dataContext.HSCT_BHXH.Add(hsctBHXHNew);
                dataContext.SaveChanges();
                if (dataContext.SaveChanges() >= 0)
                {
                    rs = true;
                }
            }

            if (rs)
            {
                MessageBox.Show("Lưu thành công");
                frm_TomTatBenhAn_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BenhNhan bn = dataContext.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            TTboXung ttbx = dataContext.TTboXungs.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            VaoVien vaovien = dataContext.VaoViens.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            RaVien ravien = dataContext.RaViens.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            #region 01071_01049
            frmIn frm = new frmIn();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                BaoCao.Rep_TomTatBA_01071 rep = new BaoCao.Rep_TomTatBA_01071();
                if (bn != null)
                {
                    var Khoa = dataContext.KPhongs.Where(p => p.MaKP == bn.MaKP).Select(p => p.TenKP).FirstOrDefault();
                    rep.Parameters["CQCQ"].Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.Parameters["CQ"].Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.Parameters["TENBN"].Value = bn.TenBNhan.ToUpper().ToString();
                    rep.Parameters["NAMSINH"].Value = bn.NamSinh;
                    rep.Parameters["STHE"].Value = bn.SThe;
                    rep.Parameters["DCHI"].Value = bn.DChi;
                    rep.Parameters["Nam"].Value = bn.GTinh == 1 ? "X" : "";
                    rep.Parameters["Nu"].Value = bn.GTinh == 0 ? "X" : "";
                    rep.Parameters["Khoa"].Value = Khoa;
                    rep.Parameters["DTNoiTru"].Value = bn.NoiTru == 1 ? "X" : "";
                    //rep.Parameters["DTNoiTruBN"].Value = bn.NoiTru == 1 ? "X" : "";
                    rep.Parameters["DTNgoaiTru"].Value = bn.NoiTru == 0 ? "X" : "";
                }
                if (ttbx != null)
                {
                    if (!string.IsNullOrEmpty(ttbx.MaDT))
                    {
                        string MaDT = ttbx.MaDT;
                        var dantoc = dataContext.DanTocs.Where(p => p.MaDT == MaDT).Select(p => p.TenDT).FirstOrDefault();
                        if (dantoc != null)
                            rep.Parameters["DanToc"].Value = dantoc;
                    }
                    if (!string.IsNullOrEmpty(ttbx.MaNN))
                    {
                        string MaNN = ttbx.MaNN;
                        var nghenghiep = dataContext.DmNNs.Where(p => p.MaNN == MaNN).Select(p => p.TenNN).FirstOrDefault();
                        if (nghenghiep != null)
                            rep.Parameters["NGHENGHIEP"].Value = nghenghiep;
                    }
                    if (vaovien != null)
                    {
                        string maicd = vaovien.ChanDoan;
                        string icd = "";
                        List<string> iCD = maicd.Split(';').ToList();
                        foreach(var vv in iCD)
                        {
                            var maicd1 = dataContext.ICD10.Where(p => p.TenICD == vv).Select(p => p.MaICD).FirstOrDefault();
                            if (!string.IsNullOrEmpty(maicd1) && maicd1 != "0")
                            {
                                icd += maicd1 + ";";
                            }
                        }
                        string id = "(" + icd + ")";
                        rep.Parameters["MaICD1"].Value = id;
                        rep.Parameters["VAOVIIEN"].Value = vaovien.NgayVao.Value.ToShortDateString();
                        rep.Parameters["CDVAOVIEN"].Value = DungChung.Ham.FreshString(vaovien.ChanDoan);
                    }
                    if (ravien != null)
                    {
                        string maicd = ravien.ChanDoan;
                        string icd = "";
                        List<string> iCD = maicd.Split(';').ToList();
                        foreach (var rv in iCD)
                        {
                            var maicd1 = dataContext.ICD10.Where(p => p.TenICD == rv).Select(p => p.MaICD).FirstOrDefault();
                            if (!string.IsNullOrEmpty(maicd1) && maicd1 != "0")
                            {
                                icd += maicd1 + ";";
                            }
                        }
                        string id = "(" + icd + ")";
                        rep.Parameters["MaICD2"].Value = id;
                        rep.Parameters["RAVIEN"].Value = ravien.NgayRa.Value.ToShortDateString();
                        rep.Parameters["CDRAVIEN"].Value = DungChung.Ham.FreshString(ravien.ChanDoan);
                        rep.Parameters["TTRAVIEN"].Value = ravien.KetQua;
                        rep.Parameters["SoLuuTru"].Value = ravien.SoLT == null ? "" : ravien.SoLT;
                        rep.Parameters["MaYTe"].Value = ravien.MaYTe == null ? "" : ravien.MaYTe;
                    }
                    rep.Parameters["NGAYKY"].Value = DungChung.Bien.DiaDanh + ",ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.Parameters["GIAMDOC"].Value = DungChung.Bien.GiamDoc;

                    var tomTatBA = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 1);
                    if (tomTatBA != null)
                    {
                        rep.Parameters["QuaTrinhBenhLy"].Value = tomTatBA.QT_BENHLY;
                        rep.Parameters["TomTatKQ"].Value = tomTatBA.TOMTAT_KQ;
                        rep.Parameters["PhuongPhapDieuTri"].Value = tomTatBA.PP_DIEUTRI;
                        rep.Parameters["GhiChu"].Value = tomTatBA.GHI_CHU;
                        rep.Parameters["COQUAN"].Value = tomTatBA.DON_VI;
                    }
                }
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region Khác
            else
            {
                if (bn != null)
                {
                    dic["TENBN"] = bn.TenBNhan.ToUpper().ToString();
                    dic["NAMSINH"] = bn.NamSinh;
                    dic["STHE"] = bn.SThe;
                    dic["DCHI"] = bn.DChi;
                    dic["Nam"] = bn.GTinh == 1 ? "X" : "";
                    dic["Nu"] = bn.GTinh == 0 ? "X" : "";
                }
                if (ttbx != null)
                {
                    if (!string.IsNullOrEmpty(ttbx.MaDT))
                    {
                        string MaDT = ttbx.MaDT;
                        var dantoc = dataContext.DanTocs.Where(p => p.MaDT == MaDT).Select(p => p.TenDT).FirstOrDefault();
                        if (dantoc != null)
                            dic["DANTOC"] = dantoc;
                    }
                    if (!string.IsNullOrEmpty(ttbx.MaNN))
                    {
                        string MaNN = ttbx.MaNN;
                        var nghenghiep = dataContext.DmNNs.Where(p => p.MaNN == MaNN).Select(p => p.TenNN).FirstOrDefault();
                        if (nghenghiep != null)
                            dic["NGHENGHIEP"] = nghenghiep;
                    }
                }
                if (vaovien != null)
                {
                    dic["VAOVIEN"] = vaovien.NgayVao.Value.ToShortDateString();
                    dic["CDVAOVIEN"] = DungChung.Ham.FreshString(vaovien.ChanDoan);
                }
                if (ravien != null)
                {
                    dic["RAVIEN"] = ravien.NgayRa.Value.ToShortDateString();
                    dic["CDRAVIEN"] = DungChung.Ham.FreshString(ravien.ChanDoan);
                    dic["TTRAVIEN"] = ravien.KetQua;
                }
                dic["CQCQ"] = DungChung.Bien.TenCQCQ.ToUpper();
                dic["CQ"] = DungChung.Bien.TenCQ.ToUpper();
                dic["NGAYKY"] = DungChung.Bien.DiaDanh + ",ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                dic["GIAMDOC"] = DungChung.Bien.GiamDoc;

                var tomTatBA = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 1);
                if (tomTatBA != null)
                {
                    dic["QuaTrinhBenhLy"] = string.Format("a) Quá trình bệnh lý và diễn biến cận lâm sàng: {0}", tomTatBA.QT_BENHLY);
                    dic["TomTatKQ"] = string.Format("b) Tóm tắt kết quả xét nghiệm cận lâm sàng có giá trị chẩn đoán: {0}", tomTatBA.TOMTAT_KQ);
                    dic["PhuongPhapDieuTri"] = string.Format("c) Phương pháp điều trị: {0}", tomTatBA.PP_DIEUTRI);
                    dic["GhiChu"] = tomTatBA.GHI_CHU;
                    dic["COQUAN"] = tomTatBA.DON_VI;
                }

                DungChung.Ham.Print(DungChung.PrintConfig.rep_TomTatBenhAn, null, dic, false);
            }
            #endregion
        }
    }
}
