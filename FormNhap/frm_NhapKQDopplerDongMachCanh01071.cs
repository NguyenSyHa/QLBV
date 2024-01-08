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
    public partial class frm_NhapKQDopplerDongMachCanh01071 : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_NhapKQDopplerDongMachCanh01071()
        {
            InitializeComponent();
        }
        private int _MaBN, _idcls, _MaKP, _IDCD;
        public frm_NhapKQDopplerDongMachCanh01071(int MaBN, int idcls, int makp, int IDCD)
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
        private void frm_NhapKQDopplerDongMachCanh01071_Load(object sender, EventArgs e)
        {
            dtpNgayTH.DateTime = DateTime.Now;
            LoadBN();

        }
        private void LoadBN()
        {
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
                    dtpNgayTH.DateTime = (DateTime)benhnhan.First().cls.NgayTH;
                }
                if (benhnhan.First().clsct.KetQua != null)
                {
                    string[] kq = benhnhan.First().clsct.KetQua.Split('|');
                    for (int i = 0; i < kq.Length; i++)
                    {
                        Kq(i, kq[i].ToString());
                    }
                }
                mnKetLuan.Text = benhnhan.First().cd.KetLuan;
                mnLoiDan.Text = benhnhan.First().cd.LoiDan;
                if (benhnhan.First().clsct.Status == 1)
                {
                    econtrol(false);
                    btnluu.Enabled = false;
                    btnKetQuaMau.Enabled = false;
                    btnin.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;

                }
                else
                {
                    econtrol(true);
                    btnluu.Enabled = true;
                    btnKetQuaMau.Enabled = true;
                    btnin.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
        }
        private void econtrol(bool T)
        {
            txtvpcms1p.Enabled = txtvpcms2p.Enabled = txtvpcms3p.Enabled = txtvdcms1p.Enabled = txtvdcms2p.Enabled = txtvdcms3p.Enabled = txtRI1p.Enabled = txtRI2P.Enabled = txtRI3p.Enabled = txtVpcms1t.Enabled = txtVpcms2t.Enabled = txtVpcms3t.Enabled = txtVdcms1t.Enabled = txtVdcms2t.Enabled = txtVdcms3t.Enabled = txtRI1t.Enabled = txtRI2t.Enabled = txtRI3t.Enabled = mnKetQua.Enabled = mnKetLuan.Enabled = mnLoiDan.Enabled = T;
        }
        private void Kq(int index, string value)
        {
            switch (index)
            {
                case 0:
                    txtvpcms1p.Text = value;
                    break;
                case 1:
                    txtvpcms2p.Text = value;
                    break;
                case 2:
                    txtvpcms3p.Text = value;
                    break;
                case 3:
                    txtvdcms1p.Text = value;
                    break;
                case 4:
                    txtvdcms2p.Text = value;
                    break;
                case 5:
                    txtvdcms3p.Text = value;
                    break;
                case 6:
                    txtRI1p.Text = value;
                    break;
                case 7:
                    txtRI2P.Text = value;
                    break;
                case 8:
                    txtRI3p.Text = value;
                    break;
                case 9:
                    txtVpcms1t.Text = value;
                    break;
                case 10:
                    txtVpcms2t.Text = value;
                    break;
                case 11:
                    txtVpcms3t.Text = value;
                    break;
                case 12:
                    txtVdcms1t.Text = value;
                    break;
                case 13:
                    txtVdcms2t.Text = value;
                    break;
                case 14:
                    txtVdcms3t.Text = value;
                    break;
                case 15:
                    txtRI1t.Text = value;
                    break;
                case 16:
                    txtRI2t.Text = value;
                    break;
                case 17:
                    txtRI3t.Text = value;
                    break;
                case 18:
                    mnKetQua.Text = value;
                    break;

            }
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnKetQuaMau.Enabled = true;
            btnluu.Enabled = true;
            btnXoa.Enabled = false;
            btnin.Enabled = false;
            btnSua.Enabled = false;
            econtrol(true);
        }
        private void btnluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var bsth = lupBSTH.EditValue.ToString() ?? "";
            if (bsth != "")
            {
                CDHA(1);
                btnSua.Enabled = true;
                btnKetQuaMau.Enabled = false;
                btnluu.Enabled = false;
                btnXoa.Enabled = true;
                btnin.Enabled = true;
                econtrol(false);
            }
            else
            {
                XtraMessageBox.Show("Hãy chọn 1 bác sỹ thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupBSTH.Focus();
                return;
            }
        }
        private void CDHA(int value)
        {
            string KetQua = txtvpcms1p.Text + "|" + txtvpcms2p.Text + "|" + txtvpcms3p.Text + "|" + txtvdcms1p.Text + "|" + txtvdcms2p.Text + "|" + txtvdcms3p.Text + "|" + txtRI1p.Text + "|" + txtRI2P.Text + "|" + txtRI3p.Text + "|" + txtVpcms1t.Text + "|" + txtVpcms2t.Text + "|" + txtVpcms3t.Text + "|" + txtVdcms1t.Text + "|" + txtVdcms2t.Text + "|" + txtVdcms3t.Text + "|" + txtRI1t.Text + "|" + txtRI2t.Text + "|" + txtRI3t.Text + "|" + mnKetQua.Text;
            CLSct ct = data.CLScts.Where(p => p.IDCD == _IDCD).Single();
            ct.KetQua = KetQua;
            ct.Status = value;

            ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
            cd.KetLuan = mnKetLuan.Text;
            cd.LoiDan = mnLoiDan.Text;
            cd.Status = value;

            CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
            cls.Status = (byte)value;
            if (value == 0)
            {
                cls.NgayTH = null;
            }
            else
            {
                cls.NgayTH = dtpNgayTH.DateTime;
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
                            double _dongia = DungChung.Ham._getGiaDM(data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, cls.MaBNhan ?? 0, dtpNgayTH.DateTime);
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
                            moi.NgayNhap = dtpNgayTH.DateTime;
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
                                dt.NgayNhap = dtpNgayTH.DateTime;
                                dt.IDCLS = cls.IdCLS;
                            }
                            data.SaveChanges();
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = dtpNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
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
                            double _dongia = DungChung.Ham._getGiaDM(data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, cls.MaBNhan ?? 0, dtpNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            moi.TrongBH = cd3.TrongBH ?? 0;
                            if (lupBSTH.EditValue != null)
                                moi.MaCB = lupBSTH.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.NgayNhap = dtpNgayTH.DateTime;
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
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult r = XtraMessageBox.Show("Bạn muốn xóa kết quả!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                for (int i = 0; i < 20; i++)
                {
                    Kq(i, "");

                }
                mnKetLuan.Text = "";
                mnLoiDan.Text = "";
                CDHA(0);
                LoadBN();

            }
            econtrol(true);
            btnXoa.Enabled = false;
            btnin.Enabled = false;
            btnKetQuaMau.Enabled = true;
            btnSua.Enabled = true;
            btnluu.Enabled = true;
        }
        private class PhieuSieuAmDopplerDMC
        {

            public string DiaChiBV { get; set; }
            public string DienThoai { get; set; }
            public string HoVaTen { get; set; }
            public int? Tuoi { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string ChanDoan { get; set; }
            public string vpcms1p { get; set; }
            public string vpcms2p { get; set; }
            public string vpcms3p { get; set; }
            public string vdcms1p { get; set; }
            public string vdcms2p { get; set; }
            public string vdcms3p { get; set; }
            public string RI1p { get; set; }
            public string RI2p { get; set; }
            public string RI3p { get; set; }
            public string vpcms1t { get; set; }
            public string vpcms2t { get; set; }
            public string vpcms3t { get; set; }
            public string vdcms1t { get; set; }
            public string vdcms2t { get; set; }
            public string vdcms3t { get; set; }
            public string RI1t { get; set; }
            public string RI2t { get; set; }
            public string RI3t { get; set; }
            public string ketqua { get; set; }
            public string LoiDan { get; set; }
            public string KetLuan { get; set; }
            public string TimeLocation { get; set; }
            public string BSTH { get; set; }


        }
        private void btnin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<PhieuSieuAmDopplerDMC> rep = new List<PhieuSieuAmDopplerDMC>();
            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                            join cls in data.CLS.Where(p => p.IdCLS == _idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            if (benhnhan.Count() > 0)
            {

                PhieuSieuAmDopplerDMC r = new PhieuSieuAmDopplerDMC();
                foreach (var item in benhnhan)
                {
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.DienThoai = data.HTHONGs.First().SDT == null ? "" : "Điện thoại: " + data.HTHONGs.First().SDT;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _MaBN).First().DThoai ?? "";
                    r.ChanDoan = item.cls.ChanDoan;
                    if (benhnhan.First().clsct.KetQua != null)
                    {
                        string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                        for (int i = 0; i < ketqua.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    r.vpcms1p = ketqua[i].ToString();
                                    break;
                                case 1:
                                    r.vpcms2p = ketqua[i].ToString();
                                    break;
                                case 2:
                                    r.vpcms3p = ketqua[i].ToString();
                                    break;
                                case 3:
                                    r.vdcms1p = ketqua[i].ToString();
                                    break;
                                case 4:
                                    r.vdcms2p = ketqua[i].ToString();
                                    break;
                                case 5:
                                    r.vdcms3p = ketqua[i].ToString();
                                    break;
                                case 6:
                                    r.RI1p = ketqua[i].ToString();
                                    break;
                                case 7:
                                    r.RI2p = ketqua[i].ToString();
                                    break;
                                case 8:
                                    r.RI3p = ketqua[i].ToString(); ;
                                    break;
                                case 9:
                                    r.vpcms1t = ketqua[i].ToString();
                                    break;
                                case 10:
                                    r.vpcms2t = ketqua[i].ToString();
                                    break;
                                case 11:
                                    r.vpcms3t = ketqua[i].ToString();
                                    break;
                                case 12:
                                    r.vdcms1t = ketqua[i].ToString();
                                    break;
                                case 13:
                                    r.vdcms2t = ketqua[i].ToString();
                                    break;
                                case 14:
                                    r.vdcms3t = ketqua[i].ToString();
                                    break;
                                case 15:
                                    r.RI1t = ketqua[i].ToString();
                                    break;
                                case 16:
                                    r.RI2t = ketqua[i].ToString();
                                    break;
                                case 17:
                                    r.RI3t = ketqua[i].ToString();
                                    break;
                                case 18:
                                    r.ketqua = ketqua[i].ToString();
                                    break;
                            }
                        }
                    }
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày " + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    if (item.cls.MaCBth != null)
                    {
                        r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);
                    }
                    rep.Add(r);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.rep_CDHA_SieuAmDopplerMachCanhXuyenSo_01071, rep, new Dictionary<string, object>(), false);



            }

        }

        private void btnKetQuaMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mnKetQua.Text = "- Động mạch cảnh chung, cảnh trong, đốt sống 2 bên không thấy màng xơ vữa. \r\n - Tốc độ dòng chảy và phổ Doppler trong giới hạn bình thường.";
        }

    }
}