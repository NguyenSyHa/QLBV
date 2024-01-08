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
    public partial class frm_NhapKQDongMachThan : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapKQDongMachThan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private int _mabn, _idcls, _makp, _idcd;
        public frm_NhapKQDongMachThan(int mabn, int idcls, int makp, int IDCD)
        {
            InitializeComponent();
            _mabn = mabn;
            _idcls = idcls;
            _makp = makp;
            _idcd = IDCD;
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
        private void frm_NhapKQDongMachThan_Load(object sender, EventArgs e)
        {
            dtpNgayThucHien.DateTime = DateTime.Now;
            loadBSTH(_makp);
            LoadBenhNhan(_mabn, _idcls);
        }

        private void LoadBenhNhan(int mabn, int idcls)
        {
            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                            join cls in data.CLS.Where(p => p.IdCLS == idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            if (benhnhan.First().clsct.KetQua != null)
            {
                string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                for (int i = 0; i < ketqua.Length; i++)
                {
                    Kq(i, ketqua[i].ToString());
                }
                EControl(false);
                btnSua.Enabled = true;
            }
            if (benhnhan.First().clsct.Status == 1)
            {
                btnIn.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                btnKetQuaMau.Enabled = false;
                btnXoa.Enabled = true;
                EControl(false);
            }
            else
            {
                btnIn.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKetQuaMau.Enabled = true;
                btnXoa.Enabled = false;
                EControl(true);
            }
            txtKetLuan.Text = benhnhan.First().cd.KetLuan;
            txtLoiDan.Text = benhnhan.First().cd.LoiDan;
            lupBacSyThucHien.EditValue = benhnhan.First().cls.MaCBth;
            dtpNgayThucHien.DateTime = benhnhan.First().cls.NgayTH ?? DateTime.Now;
        }
        private void Kq(int index, string value)
        {
            switch (index)
            {
                case 0:
                    txtVp1P.Text = value;
                    break;
                case 1:
                    txtVp2P.Text = value;
                    break;
                case 2:
                    txtVd1P.Text = value;
                    break;
                case 3:
                    txtVd2P.Text = value;
                    break;
                case 4:
                    txtRI1P.Text = value;
                    break;
                case 5:
                    txtRI2P.Text = value;
                    break;
                case 6:
                    txtVp1T.Text = value;
                    break;
                case 7:
                    txtVp2T.Text = value;
                    break;
                case 8:
                    txtVd1T.Text = value;
                    break;
                case 9:
                    txtVd2T.Text = value;
                    break;
                case 10:
                    txtRI1T.Text = value;
                    break;
                case 11:
                    txtRI2T.Text = value;
                    break;
                case 12:
                    txtThanPhai.Text = value;
                    break;
                case 13:
                    txtThanTrai.Text = value;
                    break;
                case 14:
                    txtDongMach2Ben.Text = value;
                    break;
                case 15:
                    txtTuyenThuong2Ben.Text = value;
                    break;
            }
        }
        private void btnKetQuaMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtThanPhai.Text = "Hình dạng và kích thước bình thường. Đài bể thận không giãn, không thấy cản âm bất thường. Nhu mô tưới máu đều.";
            txtThanTrai.Text = "Hình dạng và kích thước bình thường. Đài bể thận không giãn, không thấy cản âm bất thường. Nhu mô tưới máu đều.";
            txtDongMach2Ben.Text = "Tốc độ và phổ Doppler trong giới hạn bình thường.";
            txtTuyenThuong2Ben.Text = "Không có khối bất thường.";
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult r = XtraMessageBox.Show("Bạn có muốn xóa!", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
            {
                for (int i = 0; i < 16; i++)
                {
                    Kq(i, "");
                }
                string KetQua = txtVp1P.Text + "|" + txtVp2P.Text + "|" + txtVd1P.Text + "|" + txtVd2P.Text + "|" + txtRI1P.Text + "|" + txtRI2P.Text + "|" + txtVp1T.Text + "|" + txtVp2T.Text + "|" + txtVd1P.Text + "|" + txtVd2P.Text + "|" + txtRI1T.Text + "|" + txtRI2T.Text + "|" + txtThanPhai.Text + "|" + txtThanTrai.Text + "|" + txtDongMach2Ben.Text + "|" + txtTuyenThuong2Ben.Text;
                CLSct ct = data.CLScts.Where(p => p.IDCD == _idcd).Single();
                ct.KetQua = KetQua;
                ct.Status = 0;

                ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
                cd.KetLuan = "";
                cd.LoiDan = "";
                cd.Status = 0;

                CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
                cls.Status = 0;
                cls.NgayTH = null;
                cls.MaCBth = "";

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

                data.SaveChanges();
                XtraMessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuu.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                EControl(true);
                LoadBenhNhan(_mabn, _idcls);

            }


        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string bsth = lupBacSyThucHien.EditValue != null ? lupBacSyThucHien.EditValue.ToString() : "";
            if (bsth != "")
            {
                Save();
            }
            else
            {
                XtraMessageBox.Show("Hãy chọn bác sỹ thực hiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lupBacSyThucHien.Focus();
                return;
            }

        }
        private void Save()
        {
            string KetQua = txtVp1P.Text + "|" + txtVp2P.Text + "|" + txtVd1P.Text + "|" + txtVd2P.Text + "|" + txtRI1P.Text + "|" + txtRI2P.Text + "|" + txtVp1T.Text + "|" + txtVp2T.Text + "|" + txtVd1T.Text + "|" + txtVd2T.Text + "|" + txtRI1T.Text + "|" + txtRI2T.Text + "|" + txtThanPhai.Text + "|" + txtThanTrai.Text + "|" + txtDongMach2Ben.Text + "|" + txtTuyenThuong2Ben.Text;
            CLSct ct = data.CLScts.Where(p => p.IDCD == _idcd).Single();
            ct.KetQua = KetQua;
            ct.Status = 1;

            ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _idcls).Single();
            cd.KetLuan = txtKetLuan.Text;
            cd.LoiDan = txtLoiDan.Text;
            cd.Status = 1;

            CL cls = data.CLS.Where(p => p.IdCLS == _idcls).Single();
            cls.Status = 1;
            cls.NgayTH = dtpNgayThucHien.DateTime;
            cls.MaCBth = lupBacSyThucHien.EditValue.ToString() == null ? "" : lupBacSyThucHien.EditValue.ToString();
            data.SaveChanges();

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
                        if (lupBacSyThucHien.EditValue != null)
                            moi.MaCB = lupBacSyThucHien.EditValue.ToString();
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
                        if (lupBacSyThucHien.EditValue != null)
                            moi.MaCB = lupBacSyThucHien.EditValue.ToString();
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
            
            XtraMessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnKetQuaMau.Enabled = false;
            btnIn.Enabled = true;
            EControl(false);

        }
        private void EControl(bool T)
        {
            txtLoiDan.Enabled = txtKetLuan.Enabled = txtVd1T.Enabled = txtVd2T.Enabled = txtVp1P.Enabled = txtVp2P.Enabled = txtVd1P.Enabled = txtVd2P.Enabled = txtRI1P.Enabled = txtRI2P.Enabled = txtVp1T.Enabled = txtVp2T.Enabled = txtVd1P.Enabled = txtVd2P.Enabled = txtRI1T.Enabled = txtRI2T.Enabled = txtThanPhai.Enabled = txtThanTrai.Enabled = txtDongMach2Ben.Enabled = txtTuyenThuong2Ben.Enabled = T;

        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EControl(true);
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnIn.Enabled = false;
            btnKetQuaMau.Enabled = true;
        }

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
            public string vp1p { get; set; }
            public string vp2p { get; set; }
            public string vd1p { get; set; }
            public string vd2p { get; set; }
            public string RI1p { get; set; }
            public string RI2p { get; set; }
            public string vp1t { get; set; }
            public string vp2t { get; set; }
            public string vd1t { get; set; }
            public string vd2t { get; set; }
            public string RI1t { get; set; }
            public string RI2t { get; set; }
            public string ThanPhai { get; set; }
            public string ThanTrai { get; set; }
            public string DongMachThan2Ben { get; set; }
            public string TuyenThuongThan { get; set; }
            public string LoiDan { get; set; }
            public string KetLuan { get; set; }
            public string TimeLocation { get; set; }
            public string BSTH { get; set; }

        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                            join cls in data.CLS.Where(p => p.IdCLS == _idcls) on bn.MaBNhan equals cls.MaBNhan
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            select new { bn, cls, cd, dv, clsct }).ToList();
            List<RepPhieuSieuAmDoopler> rep = new List<RepPhieuSieuAmDoopler>();
            foreach (var item in benhnhan)
            {
                RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                r.DiaChiBV = DungChung.Bien.DiaChi;
                r.DienThoai = DungChung.Bien.SDTCQ == null ? "" : "Điện thoại: " + DungChung.Bien.SDTCQ;
                r.HoVaTen = item.bn.TenBNhan.ToUpper();
                r.Tuoi = item.bn.Tuoi;
                r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                r.DiaChi = item.bn.DChi;
                r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                r.ChanDoan = item.cls.ChanDoan;
                if (benhnhan.First().clsct.KetQua != null)
                {
                    string[] ketqua = benhnhan.First().clsct.KetQua.Split('|');
                    for (int i = 0; i < ketqua.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                r.vp1p = ketqua[i].ToString();
                                break;
                            case 1:
                                r.vp2p = ketqua[i].ToString();
                                break;
                            case 2:
                                r.vd1p = ketqua[i].ToString();
                                break;
                            case 3:
                                r.vd2p = ketqua[i].ToString();
                                break;
                            case 4:
                                r.RI1p = ketqua[i].ToString();
                                break;
                            case 5:
                                r.RI2p = ketqua[i].ToString();
                                break;
                            case 6:
                                r.vp1t = ketqua[i].ToString();
                                break;
                            case 7:
                                r.vp2t = ketqua[i].ToString();
                                break;
                            case 8:
                                r.vd1t = ketqua[i].ToString();
                                break;
                            case 9:
                                r.vd2t = ketqua[i].ToString();
                                break;
                            case 10:
                                r.RI1t = ketqua[i].ToString();
                                break;
                            case 11:
                                r.RI2t = ketqua[i].ToString();
                                break;
                            case 12:
                                r.ThanPhai = "- Thận phải:" + ketqua[i].ToString();
                                break;
                            case 13:
                                r.ThanTrai = "- Thận trái: " + ketqua[i].ToString();
                                break;
                            case 14:
                                r.DongMachThan2Ben = "- Động mạch thận hai bên: " + ketqua[i].ToString();
                                break;
                            case 15:
                                r.TuyenThuongThan = "- Tuyến thượng thận hai bên: " + ketqua[i].ToString();
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
            DungChung.Ham.Print(DungChung.PrintConfig.rep_CDHA_SieuAmDopplerDongMachThan_01071, rep, new Dictionary<string, object>(), false);

        }
    }
}