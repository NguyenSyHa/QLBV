using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_Dopplertim_01071 : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_Dopplertim_01071()
        {
            InitializeComponent();
        }
        private int _MaBN, _idcls, _MaKP, _IDCD;
        public frm_Dopplertim_01071(int MaBN, int idcls, int makp, int IDCD)
        {
            InitializeComponent();
            _MaBN = MaBN;
            _idcls = idcls;
            _MaKP = makp;
            _IDCD = IDCD;
        }
        private void loadBSTH()
        {
            string _makp = ";" + _MaKP.ToString() + ";";
            var c = (from cb in data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            lupBSTH.Properties.DataSource = c.ToList();
        }
        private void frm_Dopplertim_01071_Load(object sender, EventArgs e)
        {
            LoadBN();
        }
        private void LoadBN()
        {
            dtpNgayThucHien.DateTime = DateTime.Now;
            loadBSTH();

            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                            join cls in data.CLS.Where(p => p.IdCLS == _idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            if (benhnhan.Count() > 0)
            {
                lupBSTH.EditValue = benhnhan.First().cls.MaCBth ?? "";
                if (benhnhan.First().cls.NgayTH != null)
                {
                    dtpNgayThucHien.DateTime = (DateTime)benhnhan.First().cls.NgayTH;
                }
                if (benhnhan.First().clsct.KetQua != null)
                {
                    string[] kq = benhnhan.First().clsct.KetQua.Split('|');
                    for (int i = 0; i < kq.Length; i++)
                    {
                        Kq(i, kq[i].ToString());
                    }
                }
                txtKetLuan.Text = benhnhan.First().cd.KetLuan;
                txtLoiDan.Text = benhnhan.First().cd.LoiDan;
                if (benhnhan.First().clsct.Status == 1)
                {
                    econtrol(false);
                    btnLuu.Enabled = false;
                    btnKetQuaMau.Enabled = false;
                    btnIN.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;

                }
                else
                {
                    econtrol(true);
                    btnLuu.Enabled = true;
                    btnKetQuaMau.Enabled = true;
                    btnIN.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }

        }
        private void econtrol(bool T)
        {
            txtNhiTrai.Enabled = txtDKGoc.Enabled = txtDD.Enabled = txtDs.Enabled = txtEDV.Enabled = txtESV.Enabled = txtFS.Enabled = txtEF.Enabled = txtThatPhai.Enabled = txtTamTruong.Enabled = txtTamThu.Enabled = txtTamTruong1.Enabled = txtTamThu1.Enabled = T;
            txtVan2la.Enabled = txtVan2la2.Enabled = txtVanDongMachChu.Enabled = txtVanDongMachChu2.Enabled = txtVanDongMachPhoi.Enabled = txtVanDongMachPhoi2.Enabled = txtVanBaLa.Enabled = txtVanBaLa2.Enabled = txtKhoangNgoaiMangTim.Enabled = txtNhanXetKhac.Enabled = txtKetLuan.Enabled = dtpNgayThucHien.Enabled = lupBSTH.Enabled = txtLoiDan.Enabled = T;
        }
        private void Kq(int i, string value)
        {
            switch (i)
            {
                case 0:
                    txtNhiTrai.Text = value;
                    break;
                case 1:
                    txtDKGoc.Text = value;
                    break;
                case 2:
                    txtDD.Text = value;
                    break;
                case 3:
                    txtDs.Text = value;
                    break;
                case 4:
                    txtEDV.Text = value;
                    break;
                case 5:
                    txtESV.Text = value;
                    break;
                case 6:
                    txtFS.Text = value;
                    break;
                case 7:
                    txtEF.Text = value;
                    break;
                case 8:
                    txtThatPhai.Text = value;
                    break;
                case 9:
                    txtTamTruong.Text = value;
                    break;
                case 10:
                    txtTamThu.Text = value;
                    break;
                case 11:
                    txtTamTruong1.Text = value;
                    break;
                case 12:
                    txtTamThu1.Text = value;
                    break;
                case 13:

                    txtVan2la.Text = value;

                    break;
                case 14:
                    txtVan2la2.Text = value;

                    break;
                case 15:
                    txtVanDongMachChu.Text = value;

                    break;
                case 16:
                    txtVanDongMachChu2.Text = value;

                    break;
                case 17:
                    txtVanDongMachPhoi.Text = value;

                    break;
                case 18:
                    txtVanDongMachPhoi2.Text = value;

                    break;
                case 19:
                    txtVanBaLa.Text = value;
                    break;
                case 20:
                    txtVanBaLa2.Text = value;
                    break;
                case 21:
                    txtKhoangNgoaiMangTim.Text = value;
                    break;
                case 22:
                    txtNhanXetKhac.Text = value;
                    break;
            }
        }

        private void btnKetQuaMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtVan2la.Text = "Kiểu di động: ngược chiều\r\nLá van: thanh mảnh\r\nDây chằng: Thanh mảnh\r\nMép van: Bình thường\r\nKhoảng cách hai bờ van: mm";
            txtVan2la2.Text = "Huyết khối nhĩ trái: không\r\nDE:     mm, E-IVS:   mm\r\nDốc tâm trương: mm/sec; E/S>1\r\nChênh áp tối đa: mmHg; Trung bình : mmHg\r\nHở van hai lá: Không (4) Diện tích hở\r\nDiện tích lỗ van: cm2;(2D): cm2 (PHT)";
            txtVanDongMachChu.Text = "Lá van: thanh mảnh\r\nĐộ mở van:  mm,\r\nThời gian tống máu thất trái (ET)";
            txtVanDongMachChu2.Text = "Chênh áp tối đa:  mmHg; Trung bình:  mmHg.\r\nHở van:không   ( 4); Diện tích lỗ van:  cm2\r\nGốc hở: mm/ĐRTT : mm; Dòng hở dài:  mm\r\nDòng hở lan tới ";
            txtVanDongMachPhoi.Text = "Lá van: thanh mảnh\r\nĐK gốc ĐMP:  mm	Thân ĐMP:  mm\r\nNhánh phải  mm	Nhánh trái   mm";
            txtVanDongMachPhoi2.Text = "Chênh áp tối đa:   mmHg; Trung bình:  mmHg\r\nHở van: không  (4)\r\nALĐMP trung bình:  mmHg.\r\nALĐMP tâm thu: mmHg.";
            txtVanBaLa.Text = "Lá van: thanh mảnh";
            txtVanBaLa2.Text = "Hở van: Không (4); Diện ích hở: cm2\r\nChênh áp tâm thu tối đa dòng hở: mmHg";
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnKetQuaMau.Enabled = true;
            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
            btnIN.Enabled = false;
            btnSua.Enabled = false;
            econtrol(true);

        }
        private class PhieuSieuAmDopplerTIM
        {

            public string DiaChiBV { get; set; }
            public string DienThoai { get; set; }
            public string HoVaTen { get; set; }
            public int? Tuoi { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string ChanDoan { get; set; }
            public string NhiTrai { get; set; }
            public string DKGoc { get; set; }
            public string DD { get; set; }
            public string Ds { get; set; }
            public string EDV { get; set; }
            public string ESV { get; set; }
            public string FS { get; set; }
            public string EF { get; set; }
            public string ThatPhai { get; set; }
            public string TamTruong { get; set; }
            public string TamThu { get; set; }
            public string Tamtruong1 { get; set; }
            public string Tamthu1 { get; set; }
            public string Van2la { get; set; }
            public string Van2la1 { get; set; }
            public string VanDongMachChu { get; set; }
            public string VanDongMachChu2 { get; set; }
            public string VanDongMachPhoi { get; set; }
            public string VanDongMachPhoi2 { get; set; }
            public string VanbaLa { get; set; }
            public string VanbaLa2 { get; set; }
            public string KhoangNgoaiMangTim { get; set; }
            public string NhanXetKhac { get; set; }
            public string KetLuan { get; set; }
            public string BSTH { get; set; }
            public string ThoiGian { get; set; }
            public Image AnhLogo { get; set; }
            public string BSCD { get; set; }
            public string BHYT { get; set; }
            public string Khoa { get; set; }

        }

        private void btnIN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                            join cls in data.CLS.Where(p => p.IdCLS == _idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            if (benhnhan.Count() > 0)
            {
                List<PhieuSieuAmDopplerTIM> rep = new List<PhieuSieuAmDopplerTIM>();
                foreach (var item in benhnhan)
                {
                    PhieuSieuAmDopplerTIM r = new PhieuSieuAmDopplerTIM();

                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.DienThoai = data.HTHONGs.First().SDT == null ? "" : "Điện thoại: " + data.HTHONGs.First().SDT;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _MaBN).First().DThoai ?? "";
                    r.ChanDoan = item.cls.ChanDoan;
                    r.KetLuan = item.cd.KetLuan;
                    r.ThoiGian = "Hà Nội, " + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 1);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);
                    }
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        for (int i = 0; i < ketqua.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    r.NhiTrai = ketqua[i].ToString();
                                    break;
                                case 1:
                                    r.DKGoc = ketqua[i].ToString();
                                    break;
                                case 2:
                                    r.DD = ketqua[i].ToString();
                                    break;
                                case 3:
                                    r.Ds = ketqua[i].ToString();
                                    break;
                                case 4:
                                    r.EDV = ketqua[i].ToString();
                                    break;
                                case 5:
                                    r.ESV = ketqua[i].ToString();
                                    break;
                                case 6:
                                    r.FS = ketqua[i].ToString();
                                    break;
                                case 7:
                                    r.EF = ketqua[i].ToString();
                                    break;
                                case 8:
                                    r.ThatPhai = ketqua[i].ToString();
                                    break;
                                case 9:
                                    r.TamTruong = ketqua[i].ToString();
                                    break;
                                case 10:
                                    r.TamThu = ketqua[i].ToString();
                                    break;
                                case 11:
                                    r.Tamtruong1 = ketqua[i].ToString();
                                    break;
                                case 12:
                                    r.Tamthu1 = ketqua[i].ToString();
                                    break;
                                case 13:
                                    r.Van2la = ketqua[i].ToString();

                                    break;
                                case 14:
                                    r.Van2la1 = ketqua[i].ToString();

                                    break;
                                case 15:
                                    r.VanDongMachChu = ketqua[i].ToString();
                                    break;
                                case 16:
                                    r.VanDongMachChu2 = ketqua[i].ToString();
                                    break;
                                case 17:
                                    r.VanDongMachPhoi = ketqua[i].ToString();
                                    break;
                                case 18:
                                    r.VanDongMachPhoi2 = ketqua[i].ToString();
                                    break;
                                case 19:
                                    r.VanbaLa = ketqua[i].ToString();
                                    break;
                                case 20:
                                    r.VanbaLa2 = ketqua[i].ToString();
                                    break;
                                case 21:
                                    r.KhoangNgoaiMangTim = ketqua[i].ToString();
                                    break;
                                case 22:
                                    r.NhanXetKhac = ketqua[i].ToString(); ;
                                    break;

                            }
                            rep.Add(r);
                        }
                    }
                    DungChung.Ham.Print(DungChung.PrintConfig.rep_CDHA_SieuAmDopplerVanTim_01071, rep.ToList(), new Dictionary<string, object>(), false);
                }
            }

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult r = XtraMessageBox.Show("Bạn muốn xóa kết quả!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                for (int i = 0; i < 24; i++)
                {
                    Kq(i, "");

                }
                txtKetLuan.Text = "";
                txtLoiDan.Text = "";
                CDHA(0);
                LoadBN();

            }
            econtrol(true);
            btnXoa.Enabled = false;
            btnIN.Enabled = false;
            btnKetQuaMau.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void CDHA(int value)
        {
            string KetQua = txtNhiTrai.Text + "|" + txtDKGoc.Text + "|" + txtDD.Text + "|" + txtDs.Text + "|" + txtEDV.Text + "|" + txtESV.Text + "|" + txtFS.Text + "|" + txtEF.Text + "|" + txtThatPhai.Text + "|" + txtTamTruong.Text + "|" + txtTamThu.Text + "|" + txtTamTruong1.Text + "|" + txtTamThu1.Text + "|" + txtVan2la.Text + "|" + txtVan2la2.Text + "|" + txtVanDongMachChu.Text + "|" + txtVanDongMachChu2.Text + "|" + txtVanDongMachPhoi.Text + "|" + txtVanDongMachPhoi2.Text + "|" + txtVanBaLa.Text + "|" + txtVanBaLa2.Text + "|" + txtKhoangNgoaiMangTim.Text + "|" + txtNhanXetKhac.Text;
            CLSct ct = data.CLScts.Where(p => p.IDCD == _IDCD).Single();
            ct.KetQua = KetQua;
            ct.Status = value;

            ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
            cd.KetLuan = txtKetLuan.Text;
            cd.LoiDan = txtLoiDan.Text;
            cd.Status = value;

            CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
            cls.Status = (byte)value;
            if (value == 0)
            {
                cls.NgayTH = null;
            }
            else
            {
                cls.NgayTH = dtpNgayThucHien.DateTime;
            }
            cls.MaCBth = value == 0 ? "" : lupBSTH.EditValue.ToString() ?? "";

            if (value == 0)
            {
                if (cd == null)
                    return;
                var ck = (from nhom in data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                          join dvu in data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                          select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).Select(p => p.MaDV).ToList();
                int ID = cd.IDCD;
                var iddt = data.DThuoccts.Where(p => p.IDCD == ID && (ck.Count == 0 || !ck.Contains(p.MaDV ?? 0))).ToList();
                if (iddt.Count > 0)
                {
                    foreach (var item in iddt)
                    {
                        int iddtct = item.IDDonct;
                        var ktchiphi = data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                        if (ktchiphi.Count > 0)
                        {
                            MessageBox.Show("Dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                            return;
                        }
                        var xoa = data.DThuoccts.Single(p => p.IDDonct == iddtct);
                        data.DThuoccts.Remove(xoa);
                        data.SaveChanges();
                    }
                }
            }

            data.SaveChanges();

            if (value == 1)
            {
                if (cls == null)
                    return;
                int makp = 0;
                int _idkb = 0;
                if (cls.MaKP != null)
                    makp = cls.MaKP.Value;
                var bnkb = data.BNKBs.Where(p => p.MaBNhan == cls.MaBNhan && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                int iddthuoc = 0;
                var ktdthuoc = data.DThuocs.Where(p => p.MaBNhan == cls.MaBNhan).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                var cdinh = (from cd1 in data.ChiDinhs.Where(p => p.IdCLS == _idcls && p.Status == 1)
                             join dv in data.DichVus on cd1.MaDV equals dv.MaDV
                             select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV }).ToList();
                if (iddthuoc > 0)
                {
                    foreach (var cd2 in cdinh)
                    {
                        var kt = (from dt in data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                        if (kt.Count <= 0)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, cls.MaBNhan ?? 0, dtpNgayThucHien.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = iddthuoc;
                            moi.DonVi = cd2.DonVi;
                            moi.TrongBH = cd2.TrongBH ?? 0;
                            moi.IDCD = cd2.IDCD;
                            moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.XHH = cd2.XHH;
                            moi.LoaiDV = cd2.LoaiDV;
                            if (lupBSTH.EditValue != null)
                                moi.MaCB = lupBSTH.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.MaKP = makp;
                            moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.NgayNhap = dtpNgayThucHien.DateTime;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = cls.IdCLS;
                            data.DThuoccts.Add(moi);
                            data.SaveChanges();
                            var CheckGiaPhuThu = data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                            var sss = data.BenhNhans.Where(p => p.MaBNhan == cls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(data, moi.IDDonct, s);
                            }
                        }
                        else
                        {
                            foreach (var dt in kt)
                            {
                                dt.NgayNhap = dtpNgayThucHien.DateTime;
                                dt.IDCLS = cls.IdCLS;
                            }
                            data.SaveChanges();
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = dtpNgayThucHien.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                    dthuoccd.MaBNhan = cls.MaBNhan;
                    dthuoccd.MaKP = cls.MaKP;
                    dthuoccd.MaCB = cls.MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    data.DThuocs.Add(dthuoccd);
                    if (data.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, cls.MaBNhan ?? 0, dtpNgayThucHien.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            moi.TrongBH = cd3.TrongBH ?? 0;
                            if (lupBSTH.EditValue != null)
                                moi.MaCB = lupBSTH.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.NgayNhap = dtpNgayThucHien.DateTime;
                            moi.MaKP = makp;
                            moi.IDCD = cd3.IDCD;
                            moi.DonVi = cd3.DonVi;
                            moi.XHH = cd3.XHH;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = cls.IdCLS;
                            data.DThuoccts.Add(moi);
                            data.SaveChanges();
                            var CheckGiaPhuThu = data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = data.BenhNhans.Where(p => p.MaBNhan == cls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(data, moi.IDDonct, s);
                            }
                        }
                    }
                }
            }

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lupBSTH.Text != "")
            {
                CDHA(1);
                btnSua.Enabled = true;
                btnKetQuaMau.Enabled = false;
                btnLuu.Enabled = false;
                btnXoa.Enabled = true;
                btnIN.Enabled = true;
                econtrol(false);
            }
            else
            {
                XtraMessageBox.Show("Hãy chọn 1 bác sỹ thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupBSTH.Focus();
                return;
            }



        }

    }
}