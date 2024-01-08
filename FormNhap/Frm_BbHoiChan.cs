using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class Frm_BbHoiChan : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        int _idkb = 0;
        int _id = 0;
        public Frm_BbHoiChan()
        {
            InitializeComponent();
        }
        //  usKhamBenh uskb = new usKhamBenh();
        int TTLuu = 0;

        public Frm_BbHoiChan(int mabn, int idkb)
        {
            _mabn = mabn;
            _idkb = idkb;
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24012")
            {
                txtTuoi.Width = 68;
            }
        }
        #region function EnableButton
        private void EnableButton(bool t)
        {
            btnLuu.Enabled = !t;
            simpleButton1.Enabled = t;
            simpleButton2.Enabled = t;
            btnXoa.Enabled = t;
            txtBuong.Properties.ReadOnly = t;
            txtGiuong.Properties.ReadOnly = t;
            lupKhoa.Properties.ReadOnly = t;
            txtTenICD.Properties.ReadOnly = t;
            dtNgayVao.Properties.ReadOnly = t;
            dtDenNgay.Properties.ReadOnly = t;
            dtHoiChan.Properties.ReadOnly = t;
            lupChuToa.Properties.ReadOnly = t;
            lupThuKy.Properties.ReadOnly = t;
            txtTVTG.Properties.ReadOnly = t;
            txtTTDB.Properties.ReadOnly = t;
            txtKetLuan.Properties.ReadOnly = t;
            txtHDTT.Properties.ReadOnly = t;
            txtPPPT.Properties.ReadOnly = t;
            txtVoCam.Properties.ReadOnly = t;
            dtDuKienTGMo.Properties.ReadOnly = t;
            txtYKienBSGayMe.Properties.ReadOnly = t;
            txtYKienHDong.Properties.ReadOnly = t;
            txtKipPT.Properties.ReadOnly = t;
            txtDuKienDuongPT.Properties.ReadOnly = t;
            txtLoaiPT.Properties.ReadOnly = t;
            txtDuTruMau.Properties.ReadOnly = t;
            txtNhomMau.Properties.ReadOnly = t;
            lupCBGayMe.Properties.ReadOnly = t;
            lupCBPT.Properties.ReadOnly = t;
            grcNhapCT.Enabled = t;
        }
        #endregion
        #region function resetcontrol
        private void resetcontrol()
        {
            txtID.Text = "";
            lupThuKy.Text = "";
            txtTVTG.Text = "";
            txtTTDB.Text = "";
            if (DungChung.Bien.MaBV != "27022")
            {
                txtKetLuan.Text = "";
                txtHDTT.Text = "";
            }
            if (DungChung.Bien.MaBV != "27002")
            {
                txtPPPT.Text = "";
                txtYKienHDong.Text = "";
            }

            txtVoCam.Text = "";
            txtYKienBSGayMe.Text = "";
        }
        #endregion
        #region Class HC
        private class HC
        {
            private int id;

            public int IDBB
            {
                get { return id; }
                set { id = value; }
            }
            private string tenkp;

            public string TenKP
            {
                get { return tenkp; }
                set { tenkp = value; }
            }
            private DateTime ngaydttu;

            public DateTime NgayDTTu
            {
                get { return ngaydttu; }
                set { ngaydttu = value; }
            }
            private DateTime ngaydtden;


            public DateTime NgayDTDen
            {
                get { return ngaydtden; }
                set { ngaydtden = value; }
            }
            private DateTime ngayhc;
            public DateTime NgayHC
            {
                get { return ngayhc; }
                set { ngayhc = value; }
            }
            private string buong;

            public string Buong
            {
                get { return buong; }
                set { buong = value; }
            }
            private string giuong;

            public string Giuong
            {
                get { return giuong; }
                set { giuong = value; }
            }
            private string qtdbdt;

            public string QTDBDT
            {
                get { return qtdbdt; }
                set { qtdbdt = value; }
            }
            private string ketluan;

            public string KetLuan
            {
                get { return ketluan; }
                set { ketluan = value; }
            }
            private string huongdt;

            public string HuongDT
            {
                get { return huongdt; }
                set { huongdt = value; }
            }
            private string chutoa;

            public string ChuToa
            {
                get { return chutoa; }
                set { chutoa = value; }
            }
            private string thuky;

            public string ThuKy
            {
                get { return thuky; }
                set { thuky = value; }
            }
            private string thanhvien;

            public string ThanhVien
            {
                get { return thanhvien; }
                set { thanhvien = value; }
            }
            private string chandoan;

            public string ChanDoan
            {
                get { return chandoan; }
                set { chandoan = value; }
            }
            private int makp;

            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            private string macb;

            public string MaCB
            {
                get { return macb; }
                set { macb = value; }
            }
            private string macbtk;

            public string MaCBtk
            {
                get { return macbtk; }
                set { macbtk = value; }
            }
        }
        #endregion
        List<KPhong> _kp = new List<KPhong>();
        DateTime _ngaytu = System.DateTime.Now;
        List<BNKB> _bnkb = new List<BNKB>();
        List<DienBien> _db = new List<DienBien>();
        List<HC> _bbhc = new List<HC>();
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region Phiếu tóm tắt thông qua mổ 20001
        private void phieu_ttthongquamo(int mabn, int _IDHC)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.rep_tomtatmo20001 rep = new BaoCao.rep_tomtatmo20001();
            BenhNhan benhnhan = _data.BenhNhans.Single(p => p.MaBNhan == mabn);
            var kphong = _data.KPhongs.ToList();
            var vaovien = _data.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
            var bnkb = _data.BNKBs.Where(p => p.MaBNhan == mabn).ToList();
            var khambenh = (from kb in bnkb
                            join kp in kphong on kb.MaKP equals kp.MaKP
                            select new
                            {
                                kb.IDKB,
                                kb.ChanDoan,
                                kb.BenhKhac,
                                kb.MaKP,
                                kb.MaICD,
                                kp.TenKP,
                                kb.MaCB
                            }).OrderByDescending(p => p.IDKB).ToList();
            var _LHoiChan = _data.BBHCs.Where(p => p.IDBB == _IDHC).FirstOrDefault();
            if (_LHoiChan != null)
            {
                if (_LHoiChan.QTDBDT != null)
                    rep.TTBenhan.Text = _LHoiChan.QTDBDT.ToString();
                if (_LHoiChan.PPPhauThuat != null)
                    rep.colppphauthuat.Text = _LHoiChan.PPPhauThuat.ToString();
                if (_LHoiChan.PPVoCam != null)
                    rep.colppvocam.Text = _LHoiChan.PPVoCam.ToString();
                if (_LHoiChan.BSPhauThuat != null)
                {
                    var bspp = _data.CanBoes.Where(p => p.MaCB == _LHoiChan.BSPhauThuat).Select(p => p.TenCB).FirstOrDefault();
                    if (bspp != null)
                        rep.colppptv.Text = bspp.ToString();
                }
                rep.colngaymo.Text = _LHoiChan.ThoiGianDuKienPT.Value.ToShortDateString();
                if (_LHoiChan.BSGayMe != null)
                {
                    var bspp = _data.CanBoes.Where(p => p.MaCB == _LHoiChan.BSGayMe).Select(p => p.TenCB).FirstOrDefault();
                    if (bspp != null)
                        rep.colbsgayme.Text = bspp.ToString();
                }
            }
            #region kết quả CLS
            var qCLS = (from cls in _data.CLS
                        join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                        join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                        join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                        join ndv in _data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                        join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        group new { cls, cd, clsct, dv, ndv } by new { dv.TenDV, dvct.TenDVct, ndv.TenNhom, cd.Status, cls.MaBNhan, clsct.KetQua, tn.TenRG, cd.KetLuan } into kq
                        select new
                        {
                            kq.Key.TenRG,
                            kq.Key.MaBNhan,
                            kq.Key.TenDVct,
                            kq.Key.TenDV,
                            kq.Key.TenNhom,
                            kq.Key.Status,
                            kq.Key.KetQua,
                            kq.Key.KetLuan
                        }).Where(p => p.TenNhom.Trim().Equals("Xét nghiệm") || p.TenNhom.Trim().Equals("Chẩn đoán hình ảnh")).Where(p => p.Status == 1 && p.KetQua != null && p.KetQua.Trim() != "").Where(p => p.MaBNhan == mabn).OrderBy(p => p.TenDV).ToList();
            #endregion
            var _lkqsinhhoa = qCLS.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).ToList();
            if (_lkqsinhhoa.Count() > 0)
            {
                string kqsh = string.Empty;
                foreach (var item in _lkqsinhhoa)
                {
                    kqsh += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                }
                rep.colsinhhoa.Text = kqsh.ToString();
            }

            var _lkqhuyethoc = qCLS.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).ToList();
            if (_lkqhuyethoc.Count() > 0)
            {
                string kqsh = string.Empty;
                foreach (var item in _lkqhuyethoc)
                {
                    kqsh += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                }
                rep.colmaucb.Text = kqsh.ToString();
            }

            var _lkqnuoctieu = qCLS.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).ToList();
            if (_lkqnuoctieu.Count() > 0)
            {
                string kqsh = string.Empty;
                foreach (var item in _lkqnuoctieu)
                {
                    kqsh += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                }
                rep.colnuoctieu.Text = kqsh.ToString();
            }

            var _lkqxquang = qCLS.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).ToList();
            if (_lkqxquang.Count() > 0)
            {
                string kqsh = string.Empty;
                foreach (var item in _lkqxquang)
                {
                    kqsh += item.KetLuan + ";\n";
                }
                rep.colkqxquang.Text = " X-Quang tim phổi:" + kqsh.ToString();
            }

            if (khambenh.Count() > 0 && benhnhan != null && vaovien.Count > 0)
            {
                string chandoan = "";
                chandoan = khambenh.First().ChanDoan + ";" + khambenh.First().BenhKhac;
                rep.TENBN.Value = benhnhan.TenBNhan.ToUpper().ToString();
                rep.TUOI.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_data, _mabn) 
                    : DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(_data, _mabn, DungChung.Bien.formatAge_24012) 
                    : benhnhan.Tuoi.ToString();
                rep.GTINH.Value = benhnhan.GTinh == 1 ? "Nam" : "Nữ";
                DateTime ngayvao = Convert.ToDateTime(vaovien.First().NgayVao);
                rep.NGAYVV.Value = ngayvao.ToShortDateString();
                rep.CHANDOAN.Value = DungChung.Ham.FreshString(chandoan);
                rep.KHOA.Value = khambenh.First().TenKP.ToUpper();
                string macb = khambenh.First().MaCB;
                var bspp = _data.CanBoes.Where(p => p.MaCB == macb).Select(p => p.TenCB).FirstOrDefault();
                if (bspp != null)
                    rep.colbsdtri.Text = bspp.ToString();
            }
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.TENBV.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        #endregion


        private void Frm_BbHoiChan_Load(object sender, EventArgs e)
        {
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //try
            //{
            EnableButton(true);
            _bbhc.Clear();
            var qkp = (from kp in DataContect.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng") select new { kp.MaKP, kp.TenKP }).ToList();
            if (qkp.Count > 0)
            {
                lupKhoa.Properties.DataSource = qkp.ToList();
            }
            List<KPhong> _lkp = new List<KPhong>();
            List<CanBo> _lcb = new List<CanBo>();
            _lkp = DataContect.KPhongs.ToList();
            _lcb = DataContect.CanBoes.Where(p => p.Status == 1).ToList();
            lupChuToa.Properties.DataSource = _lcb;
            lupThuKy.Properties.DataSource = _lcb;
            lupCBPT.Properties.DataSource = _lcb;
            lupCBGayMe.Properties.DataSource = _lcb;
            dtDenNgay.DateTime = _ngaytu;
            DateTime GioMayTinh = DateTime.Now;
            dtHoiChan.DateTime = DungChung.Bien.MaBV == "27022" ? GioMayTinh.AddSeconds(-2) : _ngaytu.AddMinutes(-5);
            dtDuKienTGMo.Text = _ngaytu.ToString();

            var q1 = DataContect.BBHCs.Where(p => p.MaBNhan == _mabn).ToList();
            var q2 = DataContect.KPhongs.Where(p => p.Status == 1).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            var q3 = DataContect.CanBoes.Where(p => p.Status == 1).ToList();

            var q4 = (from bb in q1
                      join kp in q2 on bb.MaKP equals kp.MaKP into a
                      from kq in a.DefaultIfEmpty()
                      select new { bb.IDBB, bb.Giuong, bb.Buong, bb.ChanDoan, bb.HuongDT, bb.KetLuan, bb.NgayDTTu, bb.NgayDTDen, bb.QTDBDT, bb.ThanhVien, bb.NgayHC, bb.MaKP, bb.MaCB, bb.MaCBtk, TenKP = bb.MaKP == null ? "" : kq.TenKP }).ToList();
            var q5 = (from bb in q4
                      join cb in q3 on bb.MaCB equals cb.MaCB into c
                      from kq in c.DefaultIfEmpty()
                      select new { bb.IDBB, bb.Giuong, bb.Buong, bb.ChanDoan, bb.HuongDT, bb.KetLuan, bb.NgayDTTu, bb.NgayDTDen, bb.QTDBDT, bb.ThanhVien, bb.NgayHC, bb.MaKP, bb.MaCB, bb.MaCBtk, bb.TenKP, TenCB = (bb.MaCB == null || bb.MaCB == "") ? "" : kq.TenCB }).ToList();

            //var h = (from b in DataContect.BBHCs.Where(p => p.MaBNhan == _mabn)
            //         join kp in DataContect.KPhongs on b.MaKP equals kp.MaKP into a
            //         from kq in a.DefaultIfEmpty()
            //         join cb in DataContect.CanBoes on b.MaCB equals cb.MaCB into c
            //         from kq1 in c.DefaultIfEmpty()
            //         select new { b.IDBB, b.Giuong, b.Buong, b.ChanDoan, b.HuongDT, b.KetLuan, b.NgayDTTu, b.NgayDTDen, b.QTDBDT, b.ThanhVien, b.NgayHC, b.MaKP, b.MaCB, b.MaCBtk, TenKP = b.MaKP == null ? "" : kq.TenKP, TenCB = (b.MaCB == null || b.MaCB == "") ? "" : kq1.TenCB }).ToList();

            var hc = (from b in q5
                      join cb in q3 on b.MaCBtk equals cb.MaCB into c
                      from kq1 in c.DefaultIfEmpty()
                      select new { b.IDBB, b.Giuong, b.Buong, b.ChanDoan, b.HuongDT, b.KetLuan, b.NgayDTTu, b.NgayDTDen, b.QTDBDT, b.ThanhVien, b.NgayHC, b.MaKP, b.MaCB, b.MaCBtk, b.TenKP, b.TenCB, TenCBtk = (b.MaCBtk == null || b.MaCBtk == "") ? "" : kq1.TenCB }).ToList();
            if (hc.Count > 0)
            {
                foreach (var a in hc)
                {
                    HC them = new HC();
                    them.IDBB = a.IDBB;
                    them.Giuong = a.Giuong;
                    them.Buong = a.Buong;
                    them.ChanDoan = a.ChanDoan;
                    them.HuongDT = a.HuongDT;
                    them.KetLuan = a.KetLuan;
                    them.NgayDTTu = Convert.ToDateTime(a.NgayDTTu);
                    them.NgayDTDen = Convert.ToDateTime(a.NgayDTDen);
                    them.QTDBDT = a.QTDBDT;
                    them.ThanhVien = a.ThanhVien;
                    them.NgayHC = Convert.ToDateTime(a.NgayHC);
                    them.MaKP = a.MaKP == null ? 0 : a.MaKP.Value;
                    them.MaCB = a.MaCB;
                    them.MaCBtk = a.MaCBtk;
                    them.TenKP = a.TenKP;
                    them.ChuToa = a.TenCB;
                    them.ThuKy = a.TenCBtk;
                    _bbhc.Add(them);
                }
                grcNhapCT.DataSource = _bbhc.OrderByDescending(p => p.IDBB).ToList();
            }
            var bn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            //int id = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
            var id = (from bb in q1
                      group bb by bb.MaBNhan into kq
                      select new { kq.Key, IDKB = kq.Max(p => p.IDBB) }).ToList();

            var _ldv = (from n in DataContect.NhomDVs.Where(p => p.TenNhom.Trim().Equals("Xét nghiệm") || p.IDNhom == 8)
                        join dv in DataContect.DichVus on n.IDNhom equals dv.IDNhom
                        join dvct in DataContect.DichVucts on dv.MaDV equals dvct.MaDV
                        select new { dv.MaDV, dv.TenDV, n.TenNhom, dvct.MaDVct, dvct.TenDVct }).ToList();

            var _lCLS = (from cls in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                         join cd in DataContect.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                         join clsct in DataContect.CLScts.Where(p => p.KetQua != null && p.KetQua.Trim() != "") on cd.IDCD equals clsct.IDCD
                         select new { cd.MaDV, cls.MaBNhan, clsct.MaDVct, clsct.KetQua, cd.Status, cd.IDCD }).Distinct().ToList();
            if (id.Count() > 0)
            {
                _id = id.First().IDKB;
                txtID.Text = _id.ToString();
                #region kết quả CLS
                //var _BBHC = DataContect.BBHCs.Where(p => p.IDBB == _id).ToList();


                #endregion
                var bnbb = DataContect.BBHCs.Where(p => p.IDBB == _id).ToList();
                //join bn in DataContect.BenhNhans on bb.MaBNhan equals bn.MaBNhan
                //select new { bb.IDBB, bn.MaBNhan, bn.TenBNhan, bb.MaKP, bn.Tuoi, bn.GTinh, bn.NNhap, bb.Buong, bb.Giuong, bb.NgayDTTu, bb.NgayDTDen, bb.ChanDoan, bb.NgayHC, bb.MaCB, bb.MaCBtk, bb.ThanhVien, bb.QTDBDT, bb.KetLuan, bb.HuongDT, bb.PPPhauThuat, bb.PPVoCam, bb.ThoiGianDuKienPT, bb.YKienBSGayMe, bb.YKienHoiDong, bb.BSGayMe, bb.BSPhauThuat, bb.KipPT, bb.DuKienDuongPT, bb.LoaiPT, bb.DuTruMau, bb.NhomMau, bb.KQCLS }).FirstOrDefault();

                if (bnbb.Count > 0)
                {
                    txtMaBN.Text = bn.First().MaBNhan.ToString();
                    txtTenBNhan.Text = bn.First().TenBNhan;
                    txtGTinh.Text = bn.First().GTinh == 1 ? "Nam" : "Nữ";
                    txtTuoi.Text = DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(DataContect, _mabn, DungChung.Bien.formatAge_24012) : bn.First().Tuoi.ToString();
                    dtNgayVao.DateTime = bn.First().NNhap.Value;
                    dtHoiChan.DateTime = bnbb.First().NgayHC.Value;
                    lupKhoa.EditValue = bnbb.First().MaKP == null ? 0 : bnbb.First().MaKP.Value;
                    lupChuToa.EditValue = bnbb.First().MaCB == null ? "" : bnbb.First().MaCB.ToString();
                    lupThuKy.EditValue = bnbb.First().MaCBtk == null ? "" : bnbb.First().MaCBtk.ToString();
                    lupCBPT.EditValue = bnbb.First().BSPhauThuat == null ? "" : bnbb.First().BSPhauThuat.ToString();
                    lupCBGayMe.EditValue = bnbb.First().BSGayMe == null ? "" : bnbb.First().BSGayMe.ToString();
                    txtTVTG.Text = bnbb.First().ThanhVien;
                    txtTTDB.Text = bnbb.First().QTDBDT;
                    txtKetLuan.Text = bnbb.First().KetLuan;
                    txtHDTT.Text = bnbb.First().HuongDT;
                    txtPPPT.Text = bnbb.First().PPPhauThuat;
                    txtVoCam.Text = bnbb.First().PPVoCam;
                    if (bnbb.First().ThoiGianDuKienPT != null)
                    {
                        dtDuKienTGMo.DateTime = bnbb.First().ThoiGianDuKienPT.Value;
                    }
                    txtYKienBSGayMe.Text = bnbb.First().YKienBSGayMe;
                    txtYKienHDong.Text = bnbb.First().YKienHoiDong;
                    txtKipPT.Text = bnbb.First().KipPT;
                    txtDuKienDuongPT.Text = bnbb.First().DuKienDuongPT;
                    txtLoaiPT.Text = bnbb.First().LoaiPT;
                    txtDuTruMau.Text = bnbb.First().DuTruMau;
                    txtNhomMau.Text = bnbb.First().NhomMau;
                    if (bnbb.First().KQCLS != null && bnbb.First().KQCLS != "")
                        txtKQCLS.Text = bnbb.First().KQCLS;
                    else
                    {
                        if (DungChung.Bien.MaBV == "30007")
                        {
                            txtKQCLS.Text = "";
                        }
                        else
                        {
                            var qCLS = (from cls in _lCLS
                                        join dv in _ldv.Where(p => p.TenNhom.Trim().Equals("Xét nghiệm")) on cls.MaDV equals dv.MaDV
                                        join bb in bnbb on cls.MaBNhan equals bb.MaBNhan
                                        group new { cls, dv, bb } by new { dv.TenDV, dv.TenDVct, dv.TenNhom, cls.Status, cls.MaBNhan, cls.KetQua, bb.IDBB } into kq
                                        select new
                                        {
                                            kq.Key.IDBB,
                                            kq.Key.MaBNhan,
                                            kq.Key.TenDVct,
                                            kq.Key.TenDV,
                                            kq.Key.TenNhom,
                                            kq.Key.Status,
                                            kq.Key.KetQua
                                        }).OrderBy(p => p.TenDV).ToList();
                            string xn = string.Empty;
                            foreach (var item in qCLS)
                            {
                                xn += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                            }
                            txtKQCLS.Text = xn;
                        }
                    }
                    if (DungChung.Bien.MaBV == "27002")
                    {
                        var test = (from cls in _lCLS
                                    join dv in _ldv.Where(p => p.TenNhom.ToLower().Contains("phẫu thuật")) on cls.MaDV equals dv.MaDV
                                    //from a in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                                    //        join b in DataContect.ChiDinhs on a.IdCLS equals b.IdCLS
                                    //        join c in DataContect.DichVus on b.MaDV equals c.MaDV
                                    //        join d in DataContect.TieuNhomDVs.Where(p => p.TenTN.ToLower().Contains("phẫu thuật")) on c.IdTieuNhom equals d.IdTieuNhom
                                    select new { dv.TenDV, cls.IDCD }).ToList();
                        int maxcd = test.Count > 0 ? test.Max(p => p.IDCD) : 0;
                        txtPPPT.Text = bnbb.First().PPPhauThuat == "" ? (test.Count > 0 ? test.Where(p => p.IDCD == maxcd).First().TenDV : "") : bnbb.First().PPPhauThuat;
                        txtYKienHDong.Text = bnbb.First().YKienHoiDong == "" ? "Duyệt phẫu thuật" : bnbb.First().YKienHoiDong;
                    }
                }
            }
            else
            {
                if (_idkb > 0)
                {
                    var bnkb = DataContect.BNKBs.Where(p => p.IDKB == (_idkb)).ToList();

                    if (bnkb.Count() > 0)
                    {
                        if (bn.Count > 0)
                        {
                            txtMaBN.Text = bn.First().MaBNhan.ToString();
                            txtTenBNhan.Text = bn.First().TenBNhan;
                            txtGTinh.Text = bn.First().GTinh == 1 ? "Nam" : "Nữ";
                            txtTuoi.Text = bn.First().Tuoi.ToString();
                            txtID.Text = bn.First().MaBNhan.ToString();
                            dtNgayVao.DateTime = bn.First().NNhap.Value;
                            txtBuong.Text = (bnkb.First().Buong != null) ? bnkb.First().Buong.ToString() : "";
                            txtGiuong.Text = (bnkb.First().Giuong != null) ? bnkb.First().Giuong.ToString() : "";
                            lupKhoa.EditValue = bnkb.First().MaKP;
                            txtTenICD.Text = bnkb.First().ChanDoan;
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                txtKetLuan.Text = bnkb.First().ChanDoan;
                                var test = (from cls in _lCLS
                                            join dv in _ldv.Where(p => p.TenNhom.ToLower().Contains("phẫu thuật")) on cls.MaDV equals dv.MaDV
                                            //from a in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                                            //        join b in DataContect.ChiDinhs on a.IdCLS equals b.IdCLS
                                            //        join c in DataContect.DichVus on b.MaDV equals c.MaDV
                                            //join d in DataContect.TieuNhomDVs.Where(p => p.TenTN.ToLower().Contains("phẫu thuật")) on c.IdTieuNhom equals d.IdTieuNhom
                                            select new { dv.TenDV, cls.IDCD }).ToList();
                                int maxcd = test.Count > 0 ? test.Max(p => p.IDCD) : 0;
                                txtHDTT.Text = test.Count > 0 ? test.Where(p => p.IDCD == maxcd).First().TenDV : "";
                                txtPPPT.Text = test.Count > 0 ? test.Where(p => p.IDCD == maxcd).First().TenDV : "";
                                txtYKienHDong.Text = "Duyệt phẫu thuật";
                            }
                            if (DungChung.Bien.MaBV != "30007")
                            {
                                #region kết quả CLS
                                var qCLS = (from cls in _lCLS
                                            join dv in _ldv.Where(p => p.TenNhom.Trim().Equals("Xét nghiệm")) on cls.MaDV equals dv.MaDV
                                            join kb in bnkb on cls.MaBNhan equals kb.MaBNhan
                                            group new { cls, dv, kb } by new { dv.TenDV, dv.TenDVct, dv.TenNhom, cls.Status, cls.MaBNhan, cls.KetQua, kb.IDKB } into kq
                                            select new
                                            {
                                                kq.Key.IDKB,
                                                kq.Key.MaBNhan,
                                                kq.Key.TenDVct,
                                                kq.Key.TenDV,
                                                kq.Key.TenNhom,
                                                kq.Key.Status,
                                                kq.Key.KetQua
                                            }).OrderBy(p => p.TenDV).ToList();
                                #endregion
                                string xn = string.Empty;
                                foreach (var item in qCLS)
                                {
                                    xn += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                                }
                                txtKQCLS.Text = xn;
                            }
                        }
                    }
                }
                else
                {

                    if (bn.Count > 0)
                    {

                        txtMaBN.Text = bn.First().MaBNhan.ToString();
                        txtTenBNhan.Text = bn.First().TenBNhan;
                        txtGTinh.Text = bn.First().GTinh == 1 ? "Nam" : "Nữ";
                        txtTuoi.Text = bn.First().Tuoi.ToString();
                        txtID.Text = bn.First().MaBNhan.ToString();
                        dtNgayVao.DateTime = bn.First().NNhap.Value;
                        lupKhoa.EditValue = bn.First().MaKP;
                        if (DungChung.Bien.MaBV != "30007")
                        {
                            #region kết quả CLS
                            var qCLS = (from cls in _lCLS
                                        join dv in _ldv.Where(p => p.TenNhom.Trim().Equals("Xét nghiệm")) on cls.MaDV equals dv.MaDV
                                        //    DataContect.CLS
                                        //join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                        //join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                                        //join dv in DataContect.DichVus on cd.MaDV equals dv.MaDV
                                        //join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                        //join ndv in DataContect.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                        group new { cls, dv } by new { dv.TenDV, dv.TenDVct, dv.TenNhom, cls.Status, cls.MaBNhan, cls.KetQua } into kq
                                        select new
                                        {
                                            kq.Key.MaBNhan,
                                            kq.Key.TenDVct,
                                            kq.Key.TenDV,
                                            kq.Key.TenNhom,
                                            kq.Key.Status,
                                            kq.Key.KetQua
                                        }).Where(p => p.MaBNhan == _mabn).OrderBy(p => p.TenDV).ToList();
                            #endregion
                            string xn = string.Empty;
                            foreach (var item in qCLS)
                            {
                                xn += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                            }
                            txtKQCLS.Text = xn;
                        }
                    }
                }

                TTLuu = 1;
                EnableButton(false);
                resetcontrol();
            }
        }
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtTenBNhan.Text))
            {
                MessageBox.Show("Chưa có bệnh nhân để lưu");
                txtTenBNhan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(dtNgayVao.Text))
            {
                MessageBox.Show("Chưa có ngày bắt đầu điều trị");
                dtNgayVao.Focus();
                return false;
            }
            else if (dtNgayVao.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu điều trị không được lớn hơn ngày giờ hiện tại");
                dtNgayVao.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(dtHoiChan.Text))
            {
                MessageBox.Show("Chưa có ngày hội chẩn");
                dtHoiChan.Focus();
                return false;
            }
            //else if (dtHoiChan.DateTime > DateTime.Now)
            //{
            //    MessageBox.Show("Ngày hội chẩn không được lớn hơn ngày giờ hiện tại");
            //    dtHoiChan.Focus();
            //    return false;
            //}
            if (Convert.ToDateTime(dtDenNgay.Text.ToString()) < Convert.ToDateTime(dtNgayVao.Text.ToString()))
            {
                MessageBox.Show("Trường Đến ngày phải lớn hơn hoặc bằng Ngày bắt đầu điều trị của bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtDenNgay.Focus();
                return false;
            }
            //else if (dtDenNgay.DateTime > DateTime.Now)
            //{
            //    MessageBox.Show("Ngày kết thúc điều trị không được lớn hơn ngày giờ hiện tại");
            //    dtDenNgay.Focus();
            //    return false;
            //}
            if (Convert.ToDateTime(dtHoiChan.Text.ToString()) < Convert.ToDateTime(dtNgayVao.Text.ToString()))
            {
                MessageBox.Show("Ngày Hội chẩn phải lớn hơn hoặc bằng Ngày bắt đầu điều trị của bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtHoiChan.Focus();
                return false;
            }
            DateTime ngayvao = dtNgayVao.DateTime;
            DateTime Ngayra = dtDenNgay.DateTime;
            DateTime Ngahc = dtHoiChan.DateTime;
            if (Ngahc < ngayvao || Ngahc > Ngayra)
            {
                MessageBox.Show("Ngày hội chẩn phải nằm trong khoảng từ ngày và đến ngày");
                dtHoiChan.Focus();
                return false;
            }
            DateTime NgayduyetMo = dtDuKienTGMo.DateTime;
            if (NgayduyetMo < Ngahc)
            {
                MessageBox.Show("Ngày dự kiến mổ phải lớn hơn ngày hội chẩn");
                dtDuKienTGMo.Focus();
                return false;
            }
            //if (Convert.ToDateTime(dtDuKienTGMo.Text.ToString()) < Convert.ToDateTime(dtNgayVao.Text.ToString()))
            //{
            //    MessageBox.Show("Ngày Dự kiến mổ phải trong lớn hơn ngày bắt đầu điều trị của bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dtDuKienTGMo.Focus();
            //    return false;
            //}
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                bool ttluu = false;
                int ot;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBN.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtMaBN.Text);
                switch (TTLuu)
                {
                    case 1:
                        //try
                        //{
                        BBHC moibb = new BBHC();
                        moibb.MaBNhan = _int_maBN;
                        moibb.NgayDTTu = Convert.ToDateTime(dtNgayVao.Text);
                        moibb.NgayDTDen = Convert.ToDateTime(dtDenNgay.Text);
                        moibb.Giuong = txtGiuong.Text;
                        moibb.Buong = txtBuong.Text;
                        moibb.ChanDoan = txtTenICD.Text;
                        moibb.NgayHC = Convert.ToDateTime(dtHoiChan.Text);
                        if (lupKhoa.EditValue != null)
                        { moibb.MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue); }
                        if (lupChuToa.EditValue != null)
                        { moibb.MaCB = lupChuToa.EditValue.ToString(); }
                        if (lupThuKy.EditValue != null)
                        { moibb.MaCBtk = lupThuKy.EditValue.ToString(); }
                        if (lupCBPT.EditValue != null)
                        { moibb.BSPhauThuat = lupCBPT.EditValue.ToString(); }
                        if (lupCBGayMe.EditValue != null)
                        { moibb.BSGayMe = lupCBGayMe.EditValue.ToString(); }
                        moibb.ThanhVien = txtTVTG.Text;
                        moibb.QTDBDT = txtTTDB.Text;
                        moibb.KetLuan = txtKetLuan.Text;
                        moibb.HuongDT = txtHDTT.Text;
                        moibb.PPPhauThuat = txtPPPT.Text;
                        moibb.PPVoCam = txtVoCam.Text;
                        if (!string.IsNullOrEmpty(dtDuKienTGMo.Text))
                            moibb.ThoiGianDuKienPT = Convert.ToDateTime(dtDuKienTGMo.Text);
                        moibb.YKienBSGayMe = txtYKienBSGayMe.Text;
                        moibb.YKienHoiDong = txtYKienHDong.Text;
                        moibb.KipPT = txtKipPT.Text;
                        moibb.DuKienDuongPT = txtDuKienDuongPT.Text;
                        moibb.LoaiPT = txtLoaiPT.Text;
                        moibb.DuTruMau = txtDuTruMau.Text;
                        moibb.NhomMau = txtNhomMau.Text;
                        moibb.KQCLS = txtKQCLS.Text;
                        DataContect.BBHCs.Add(moibb);

                        //try
                        //{
                        //    DataContect.SaveChanges();
                        //    ttluu = true;
                        //}
                        //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                        //{
                        //    ttluu = false;
                        //    Exception raise = dbEx;

                        //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        //    {

                        //        foreach (var validationError in validationErrors.ValidationErrors)
                        //        {

                        //            string message = string.Format("{0}:{1}",

                        //              validationErrors.Entry.Entity.ToString(),

                        //                validationError.ErrorMessage);

                        //            // raise a new exception nesting

                        //            // the current instance as InnerException

                        //            raise = new InvalidOperationException(message, raise);

                        //        }

                        //    }

                        //    throw raise;
                        //}
                        if (DataContect.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Tạo mới thành công");
                            Frm_BbHoiChan_Load(sender, e);
                            TTLuu = 0;
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Lỗi không tạo mới được: " + ex.Message);
                        //}
                        break;
                    case 2:
                        //try
                        //{
                        var id = DataContect.BBHCs.Where(p => p.IDBB == _id).ToList();
                        if (id.Count > 0)
                        {
                            // sua
                            BBHC suabb = DataContect.BBHCs.Single(p => p.IDBB == _id);
                            suabb.NgayDTTu = Convert.ToDateTime(dtNgayVao.Text);
                            suabb.NgayDTDen = Convert.ToDateTime(dtDenNgay.Text);
                            suabb.Giuong = txtGiuong.Text;
                            suabb.Buong = txtBuong.Text;
                            if (lupKhoa.EditValue != null)
                            { suabb.MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue); }
                            suabb.ChanDoan = txtTenICD.Text;
                            suabb.NgayHC = Convert.ToDateTime(dtHoiChan.Text);
                            if (lupChuToa.EditValue != null)
                            { suabb.MaCB = lupChuToa.EditValue.ToString(); }
                            if (lupThuKy.EditValue != null)
                            { suabb.MaCBtk = lupThuKy.EditValue.ToString(); }
                            if (lupCBPT.EditValue != null)
                            { suabb.BSPhauThuat = lupCBPT.EditValue.ToString(); }
                            if (lupCBGayMe.EditValue != null)
                            { suabb.BSGayMe = lupCBGayMe.EditValue.ToString(); }
                            suabb.ThanhVien = txtTVTG.Text;
                            suabb.QTDBDT = txtTTDB.Text;
                            suabb.KetLuan = txtKetLuan.Text;
                            suabb.HuongDT = txtHDTT.Text;
                            suabb.PPPhauThuat = txtPPPT.Text;
                            suabb.PPVoCam = txtVoCam.Text;
                            if (!string.IsNullOrEmpty(dtDuKienTGMo.Text))
                                suabb.ThoiGianDuKienPT = Convert.ToDateTime(dtDuKienTGMo.Text);
                            suabb.YKienBSGayMe = txtYKienBSGayMe.Text;
                            suabb.YKienHoiDong = txtYKienHDong.Text;
                            suabb.KipPT = txtKipPT.Text;
                            suabb.DuKienDuongPT = txtDuKienDuongPT.Text;
                            suabb.LoaiPT = txtLoaiPT.Text;
                            suabb.DuTruMau = txtDuTruMau.Text;
                            suabb.NhomMau = txtNhomMau.Text;
                            suabb.KQCLS = txtKQCLS.Text;

                            //try
                            //{
                            //    DataContect.SaveChanges();
                            //    ttluu = true;
                            //}
                            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            //{
                            //    ttluu = false;
                            //    Exception raise = dbEx;

                            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                            //    {

                            //        foreach (var validationError in validationErrors.ValidationErrors)
                            //        {

                            //            string message = string.Format("{0}:{1}",

                            //              validationErrors.Entry.Entity.ToString(),

                            //                validationError.ErrorMessage);

                            //            // raise a new exception nesting

                            //            // the current instance as InnerException

                            //            raise = new InvalidOperationException(message, raise);

                            //        }

                            //    }

                            //    throw raise;
                            //}
                            if (DataContect.SaveChanges() >= 0)
                            {
                                TTLuu = 0;
                                MessageBox.Show("Sửa thành công");
                                Frm_BbHoiChan_Load(sender, e);
                            }
                        }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Không sửa được! " + ex.Message);
                        //}
                        break;
                        btnLuu.Enabled = false;
                }


            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            //if (_kp.Count > 0)
            //{
            //    if (lupKhoa.EditValue != null && lupKhoa.EditValue.ToString() != _kp.First().MaKP)
            //        btnLuu.Enabled = true;
            //    else
            //        btnLuu.Enabled = false;
            //}
            //else btnLuu.Enabled = true;
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInPhieu_Click_1(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_BbHoiChan rep = new BaoCao.Rep_BbHoiChan();                           
                //if (!string.IsNullOrEmpty(txtID.Text))
                //   {
                //       int _id =Convert.ToInt32(txtID.Text);

                var par = (from bb in DataContect.BBHCs.Where(p => p.IDBB == _id)
                       join bn in DataContect.BenhNhans on bb.MaBNhan equals bn.MaBNhan
                       join kp in DataContect.KPhongs on bb.MaKP equals kp.MaKP
                       select new
                      {
                          bn.TenBNhan,
                          bn.GTinh,
                          bn.Tuoi,  
                          bb.NgayDTTu,
                          bb.NgayDTDen,
                          bb.Buong,
                          bb.Giuong,
                          bb.MaKP,
                          bb.ChanDoan,
                          bb.NgayHC,
                          bb.MaCB,
                          bb.MaCBtk,
                          bb.ThanhVien,
                          bb.QTDBDT,
                          bb.KetLuan,
                          bb.HuongDT,
                          kp.PLoai                         
                      }).ToList();

            rep.So.Value = _id;
            if (par.Count > 0)
            {                
                if (par.First().MaKP != null)
                {
                    int _mp = par.First().MaKP.Value;
                    var kp = DataContect.KPhongs.Where(p => p.MaKP == _mp).Select(x => new { x.TenKP }).ToList();
                    rep.Khoa.Value = kp.First().TenKP;
                    if (DungChung.Bien.MaBV == "27022")
                    {
                        if (par.First().PLoai == "Phòng khám") //Phòng khám
                        {
                            rep.lab1.Text = "BIÊN BẢN HỘI CHẨN";
                            rep.lab2.Text = "MS: 42/BV - 01";
                            rep.xrLabel21.Text = "Khoa khám bệnh: ";
                            rep.lab14.Text = "Phòng khám: ";
                            rep.lab13.Visible = false;
                            rep.txtBuong.Visible = false;
                            rep.lab12.Visible = false;
                            rep.lab13.Visible = false;
                            rep.txtGiuong.Visible = false;
                            //lab12.LocationF = new PointF(1, 187);
                            //lab12.SizeF = new SizeF(108, 24);
                            rep.txtKhoa.SizeF = new SizeF(299, 24);
                            rep.txtKhoa.LocationF = new PointF(110, 187);
                        }
                        else if (par.First().PLoai == "Lâm sàng")//Lâm sàng
                        {
                            rep.lab2.Text = "MS: 42/BV - 01";
                            rep.lab1.Text = "BIÊN BẢN HỘI CHẨN";
                            rep.xrLabel21.Visible = false;
                            rep.xrLabel1.SizeF = new SizeF(300, 24);
                            rep.xrLabel1.LocationF = new PointF(75, 49);
                        }
                    }
                }
                rep.TenBN.Value = par.First().TenBNhan.ToUpper();
                rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(data , _mabn) : DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012) : par.First().Tuoi.ToString();
                if (par.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }
                if (par.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                rep.DTTuNgay.Value = par.First().NgayDTTu;
                rep.DTDenNgay.Value = par.First().NgayDTDen;
                rep.Giuong.Value = par.First().Giuong;
                rep.Buong.Value = par.First().Buong;
                rep.Giuong.Value = par.First().Giuong;
                rep.ChanDoan.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.FreshString(par.First().ChanDoan) : par.First().ChanDoan;
                rep.NgayHoiChan.Value = par.First().NgayHC;
                if (par.First().MaCB != null)
                {
                    string _cb1 = par.First().MaCB;
                    var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                    if (cb1.Count > 0)
                    {
                        rep.ChuToa.Value = cb1.First().TenCB;
                    }
                }
                if (par.First().MaCBtk != null)
                {
                    string _cb1 = par.First().MaCBtk;
                    var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                    if (cb1.Count > 0)
                    { rep.ThuKy.Value = cb1.First().TenCB; }
                }
                if (par.First().ThanhVien != null)
                rep.TVTG.Value =  par.First().ThanhVien.Replace("\r\n", Environment.NewLine);
                rep.TTDB.Value = par.First().QTDBDT;
                rep.KetLuan.Value = par.First().KetLuan;
                rep.HDTT.Value = par.First().HuongDT;                
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                int idbb = Convert.ToInt32(txtID.Text);
                var query = (from bbhc in DataContect.BBHCs.Where(p => p.IDBB == idbb)
                             join cls in DataContect.CLS on bbhc.IDBB equals cls.IDBB
                             join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join dv in DataContect.DichVus on cd.MaDV equals dv.MaDV
                             join tn in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new
                             {
                                 cd.Status,
                                 tn.TenRG
                             }).Where(p => p.Status == 1).Where(p => p.TenRG == "Phẫu thuật").ToList();
                if (query.Count == 0)
                {
                    var ktbb = DataContect.BBHCs.Where(p => p.IDBB == idbb).ToList();
                    if (ktbb.Count > 0)
                    {
                        DialogResult _reuslt = MessageBox.Show("Bạn thực sự muốn xóa biên bản hội chẩn của BN: " + txtTenBNhan.Text, "Xóa Biên bản hội chẩn", MessageBoxButtons.YesNo, MessageBoxIcon.Question); if (_reuslt == DialogResult.Yes)
                        {
                            var xoa = DataContect.BBHCs.Single(p => p.IDBB == idbb);
                            DataContect.BBHCs.Remove(xoa);
                            DataContect.SaveChanges();
                            Frm_BbHoiChan_Load(sender, e);
                        }
                    }
                    else
                    {
                        var xoa = DataContect.BBHCs.Single(p => p.IDBB == idbb);
                        DataContect.BBHCs.Remove(xoa);
                        DataContect.SaveChanges();
                        Frm_BbHoiChan_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã thực hiện phẫu thuật, không thể xóa biên bản hội chẩn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Frm_BbHoiChan_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var bb = DataContect.BBHCs.Where(p => p.MaBNhan== _mabn).ToList();
            //if (bb.Count > 0)
            //{

            //        DialogResult _result = MessageBox.Show("Bạn chưa lưu biên bản hội chẩn, bạn có muốn lưu không?", "hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (_result == DialogResult.Yes)
            //            btnLuu_Click(sender, e);

            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TTLuu = 1;
            EnableButton(false);
            resetcontrol();
        }

        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            List<BBHC> _lbb = new List<BBHC>();
            if (grvNhapCT.GetFocusedRowCellValue(colID) != null && grvNhapCT.GetFocusedRowCellValue(colID).ToString() != "")
            {
                _id = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
                txtID.Text = _id.ToString();
                _lbb = DataContect.BBHCs.Where(p => p.IDBB == _id).ToList();
                if (_lbb.Count() > 0)
                {
                    #region kết quả CLS
                    var qCLS = (from cls in DataContect.CLS
                                join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                                join dv in DataContect.DichVus on cd.MaDV equals dv.MaDV
                                join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                join ndv in DataContect.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                join bbhc in DataContect.BBHCs on cls.MaBNhan equals bbhc.MaBNhan
                                group new { cls, cd, clsct, dv, ndv, bbhc } by new { dv.TenDV, dvct.TenDVct, ndv.TenNhom, cd.Status, cls.MaBNhan, clsct.KetQua, bbhc.IDBB } into kq
                                select new
                                {
                                    kq.Key.IDBB,
                                    kq.Key.MaBNhan,
                                    kq.Key.TenDVct,
                                    kq.Key.TenDV,
                                    kq.Key.TenNhom,
                                    kq.Key.Status,
                                    kq.Key.KetQua
                                }).Where(p => p.Status == 1 && p.TenNhom.Trim().Equals("Xét nghiệm") && p.KetQua != null && p.KetQua.Trim() != "").Where(p => p.IDBB == _id).OrderBy(p => p.TenDV).ToList();
                    #endregion
                    dtNgayVao.DateTime = Convert.ToDateTime(_lbb.First().NgayDTTu);
                    dtDenNgay.Text = _lbb.First().NgayDTDen.ToString();
                    txtGiuong.Text = _lbb.First().Giuong;
                    txtBuong.Text = _lbb.First().Buong;

                    lupKhoa.EditValue = _lbb.First().MaKP == null ? 0 : _lbb.First().MaKP.Value;
                    lupChuToa.EditValue = (_lbb.First().MaCB == null) ? "" : _lbb.First().MaCB.ToString();
                    lupThuKy.EditValue = (_lbb.First().MaCBtk == null) ? "" : _lbb.First().MaCBtk.ToString();
                    lupCBGayMe.EditValue = (_lbb.First().BSGayMe == null) ? "" : _lbb.First().BSGayMe.ToString();
                    lupCBPT.EditValue = (_lbb.First().BSPhauThuat == null) ? "" : _lbb.First().BSPhauThuat.ToString();
                    //if (_lbb.First().MaKP != null)
                    //{
                    //    //int _mk = _lbb.Where(p => p.IDBB == _id).First().MaKP == null ? 0 : _lbb.Where(p => p.IDBB == _id).First().MaKP.Value;
                    //    lupKhoa.EditValue = _lbb.First().MaKP;
                    //}
                    //else lupKhoa.EditValue = "";

                    //if (_lbb.First().MaCB != null && !String.IsNullOrEmpty(_lbb.First().MaCB.ToString()))
                    //{
                    //    //string _cb1 = _lbb.Where(p => p.IDBB == _id).First().MaCB;
                    //    lupChuToa.EditValue = _lbb.First().MaCB;
                    //}
                    //else lupChuToa.EditValue = "";
                    //if (_lbb.First().MaCBtk != null && !String.IsNullOrEmpty(_lbb.First().MaCBtk.ToString()))
                    //{
                    //    //string _cb2 = _lbb.Where(p => p.IDBB == _id).First().MaCBtk;
                    //    lupThuKy.EditValue = _lbb.First().MaCBtk;
                    //}
                    //else { lupThuKy.EditValue = ""; }
                    //if (_lbb.First().BSGayMe != null && !String.IsNullOrEmpty(_lbb.First().BSGayMe.ToString()))
                    //{
                    //    //string _bsgm = _lbb.Where(p => p.IDBB == _id).First().BSGayMe;
                    //    lupCBGayMe.EditValue = _lbb.First().BSGayMe;
                    //}
                    //else { lupCBGayMe.EditValue = ""; }
                    //if (_lbb.First().BSPhauThuat != null && !String.IsNullOrEmpty(_lbb.First().BSPhauThuat.ToString()))
                    //{
                    //    //string _bspt = _lbb.Where(p => p.IDBB == _id).First().BSPhauThuat;
                    //    lupCBPT.EditValue = _lbb.First().BSPhauThuat;
                    //}
                    //else { lupCBPT.EditValue = ""; }
                    txtTenICD.Text = _lbb.First().ChanDoan;
                    dtHoiChan.DateTime = _lbb.First().NgayHC.Value;
                    txtTVTG.Text = _lbb.First().ThanhVien;
                    txtTTDB.Text = _lbb.First().QTDBDT;
                    txtKetLuan.Text = _lbb.First().KetLuan;
                    txtHDTT.Text = _lbb.First().HuongDT;
                    txtKipPT.Text = _lbb.First().KipPT;
                    txtVoCam.Text = _lbb.First().PPVoCam;
                    txtPPPT.Text = _lbb.First().PPPhauThuat;
                    txtYKienBSGayMe.Text = _lbb.First().YKienBSGayMe;
                    txtYKienHDong.Text = _lbb.First().YKienHoiDong;
                    dtDuKienTGMo.Text = _lbb.First().ThoiGianDuKienPT.ToString();
                    txtDuKienDuongPT.Text = _lbb.First().DuKienDuongPT;
                    txtLoaiPT.Text = _lbb.First().LoaiPT;
                    txtDuTruMau.Text = _lbb.First().DuTruMau;
                    txtNhomMau.Text = _lbb.First().NhomMau;
                    if (_lbb.First().KQCLS != null && _lbb.First().KQCLS != "")
                        txtKQCLS.Text = _lbb.First().KQCLS;
                    else
                    {
                        string xn = string.Empty;
                        foreach (var item in qCLS)
                        {
                            xn += "- " + item.TenDVct + ": " + item.KetQua + ";\n";
                        }
                        txtKQCLS.Text = xn;
                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTenBNhan.Properties.ReadOnly = true;
            txtTenBNhan.Properties.AllowFocused = false;
            txtTuoi.Properties.ReadOnly = true;
            txtGTinh.Properties.ReadOnly = true;
            if (!string.IsNullOrEmpty(txtTenBNhan.Text))
            {
                TTLuu = 2;
                EnableButton(false);
            }
            else
            {
                MessageBox.Show("không có biên bản hội chẩn để sửa");
            }
        }

        private void btnInPhieuDuyetMo_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_BC_PhieuDuyetMo_24009 rep = new BaoCao.Rep_BC_PhieuDuyetMo_24009();
            string huyen = DungChung.Bien.MaHuyen != "" ? data.DmHuyens.Where(p => p.MaHuyen == DungChung.Bien.MaHuyen).FirstOrDefault().TenHuyen : "";
            int idBB = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
            var qBB = (from bbhc in data.BBHCs.Where(p => p.IDBB == idBB)
                       join bn in data.BenhNhans on bbhc.MaBNhan equals bn.MaBNhan
                       join kp in data.KPhongs on bbhc.MaKP equals kp.MaKP
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       select new
                       {
                           bn.TenBNhan,
                           bn.Tuoi,
                           GTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                           bbhc.HuongDT,
                           ChanDoan = DungChung.Bien.MaBV == "27002" ? vv.ChanDoan : bbhc.ChanDoan,
                           bbhc.PPPhauThuat,
                           bbhc.PPVoCam,
                           bbhc.ThoiGianDuKienPT,
                           bbhc.YKienBSGayMe,
                           bbhc.YKienHoiDong,
                           bbhc.BSPhauThuat,
                           bbhc.KipPT,
                           bbhc.BSGayMe,
                           kp.TenKP
                       }).FirstOrDefault();

            if (qBB != null)
            {
                string _cbth = (!string.IsNullOrEmpty(qBB.BSPhauThuat)) ? data.CanBoes.Where(p => p.MaCB == qBB.BSPhauThuat).FirstOrDefault().TenCB : "";
                string _bsgayme = (!string.IsNullOrEmpty(qBB.BSGayMe)) ? data.CanBoes.Where(p => p.MaCB == qBB.BSGayMe).FirstOrDefault().TenCB : "";

                rep.lblHoTenBN.Text = qBB.TenBNhan;
                rep.lblTuoi.Text = "Tuổi: " +( DungChung.Bien.MaBV == "14018"?DungChung.Ham.TinhTuoiBenhNhan(data,_mabn): DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012) : qBB.Tuoi.ToString());
                rep.lblGTinh.Text = "Giới tính: " + qBB.GTinh;
                rep.lblChanDoan.Text = qBB.ChanDoan;
                rep.lblPPPhauThuat.Text = "1. Phương pháp phẫu thuật: " + qBB.PPPhauThuat;
                rep.lblVoCam.Text = "2. Vô cảm: " + qBB.PPVoCam;
                rep.lblTGMo.Text = "3. Dự kiến thời gian mổ: " + String.Format("{0:dd/MM/yyyy}", qBB.ThoiGianDuKienPT);
                rep.lblKipPhauThuat.Text = "4. Kíp phẫu thuật: " + qBB.KipPT;
                rep.celTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
                rep.celPhauThuatVien.Text = _cbth;
                rep.lblYKienBSgayMe.Text = qBB.YKienBSGayMe;
                rep.celBSgayMe.Text = _bsgayme;
                rep.lblYKienHD.Text = qBB.YKienHoiDong;
                rep.lblKhoa.Text = "KHOA: " + qBB.TenKP;
                rep.lblNgayThang.Text = huyen + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có biên bản hội chẩn nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBenhAnPT_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            BaoCao.Rep_BC_TomTatBenhAnPT_27022 rep = new BaoCao.Rep_BC_TomTatBenhAnPT_27022();
            int idBB = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
            if (idBB > 0)
            {
                #region query
                var qBB = (from bbhc in data.BBHCs.Where(p => p.IDBB == idBB)
                           join bn in data.BenhNhans on bbhc.MaBNhan equals bn.MaBNhan
                           join bnkb in data.BNKBs on new { MaBNhan = bbhc.MaBNhan, MaKP = bbhc.MaKP } equals new { MaBNhan = bnkb.MaBNhan, MaKP = bnkb.MaKP }
                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                           join nn in data.DmNNs on ttbx.MaNN equals nn.MaNN into q
                           from q1 in q.DefaultIfEmpty()
                           group new { bbhc, bn, bnkb, vv, ttbx, q1 } by new
                           {
                               bbhc.MaKP,
                               bbhc.MaBNhan,
                               bn.TenBNhan,
                               bn.Tuoi,
                               bn.DChi,
                               bn.GTinh,
                               ChanDoanPT = bbhc.ChanDoan,
                               bbhc.PPPhauThuat,
                               bbhc.PPVoCam,
                               bbhc.ThoiGianDuKienPT,
                               bbhc.YKienBSGayMe,
                               bbhc.YKienHoiDong,
                               bbhc.BSPhauThuat,
                               bbhc.KipPT,
                               bbhc.BSGayMe,
                               bnkb.Buong,
                               bnkb.Giuong,
                               bbhc.LoaiPT,
                               bbhc.DuKienDuongPT,
                               bbhc.DuTruMau,
                               bbhc.NhomMau,
                               bbhc.QTDBDT,
                               bbhc.KQCLS,
                               bnkb.NgayKham,
                               bnkb.NgayNghi,
                               bnkb.MaCB,
                               vv.NgayVao,
                               vv.SoBA,
                               vv.BenhLy,
                               vv.TienSuBT,
                               vv.ChanDoan,
                               vv.KhamBPhan,
                               q1.TenNN
                           } into kq
                           select new
                           {
                               kq.Key.MaKP,
                               kq.Key.MaBNhan,
                               kq.Key.TenBNhan,
                               kq.Key.Tuoi,
                               kq.Key.DChi,
                               GTinh = kq.Key.GTinh == 0 ? "Nữ" : "Nam",
                               kq.Key.ChanDoanPT,
                               kq.Key.PPPhauThuat,
                               kq.Key.PPVoCam,
                               kq.Key.ThoiGianDuKienPT,
                               kq.Key.YKienBSGayMe,
                               kq.Key.YKienHoiDong,
                               kq.Key.BSPhauThuat,
                               kq.Key.KipPT,
                               kq.Key.BSGayMe,
                               kq.Key.Buong,
                               kq.Key.Giuong,
                               kq.Key.LoaiPT,
                               kq.Key.DuKienDuongPT,
                               kq.Key.DuTruMau,
                               kq.Key.NhomMau,
                               kq.Key.KQCLS,
                               kq.Key.QTDBDT,
                               kq.Key.NgayKham,
                               kq.Key.NgayNghi,
                               kq.Key.MaCB,
                               kq.Key.NgayVao,
                               kq.Key.SoBA,
                               kq.Key.BenhLy,
                               kq.Key.TienSuBT,
                               kq.Key.ChanDoan,
                               kq.Key.KhamBPhan,
                               kq.Key.TenNN
                           }).FirstOrDefault();
                #endregion
                if (qBB != null)
                {
                    string bspt = (!string.IsNullOrEmpty(qBB.BSPhauThuat)) ? data.CanBoes.Where(p => p.MaCB == qBB.BSPhauThuat).First().TenCB : "";
                    string bsgayme = (!string.IsNullOrEmpty(qBB.BSGayMe)) ? data.CanBoes.Where(p => p.MaCB == qBB.BSGayMe).First().TenCB : "";
                    string bsdt = (!string.IsNullOrEmpty(qBB.MaCB)) ? data.CanBoes.Where(p => p.MaCB == qBB.MaCB).First().TenCB : "";

                    rep.lblTenBN.Text = qBB.TenBNhan;
                    rep.lblTuoi.Text = DungChung.Bien.MaBV == "14018"?DungChung.Ham.TinhTuoiBenhNhan(data,_mabn): qBB.Tuoi.ToString();
                    rep.lblGTinh.Text = qBB.GTinh;
                    rep.lblNgayVV.Text = (qBB.NgayVao != null) ? String.Format("{0:dd/MM/yyyy}", qBB.NgayVao.Value) : "";
                    rep.lblNgayVaoKhoa.Text = (qBB.NgayKham != null) ? String.Format("{0:dd/MM/yyyy}", qBB.NgayKham.Value) : "";
                    rep.lblNgayCK.Text = (qBB.NgayNghi != null) ? String.Format("{0:dd/MM/yyyy}", qBB.NgayNghi.Value) : "";
                    rep.lblSoBA.Text = qBB.SoBA;
                    rep.lblBuong.Text = qBB.Buong;
                    rep.lblGiuong.Text = qBB.Giuong;
                    rep.lblNgheNghiepDC.Text = "Nghề nghiệp: " + qBB.TenNN + " - Địa chỉ: " + qBB.DChi;
                    rep.lblBenhLy.Text = qBB.BenhLy + ", Quá trình diễn biến bệnh: " + qBB.QTDBDT;
                    rep.lblTienSuBT.Text = qBB.TienSuBT;
                    rep.lblChanDoanKhoa.Text = qBB.ChanDoan;
                    rep.lblKhamBP.Text = qBB.KhamBPhan;
                    rep.lblXetNghiemDaLam.Text = qBB.KQCLS;
                    rep.lblChanDoanTruocPT.Text = qBB.ChanDoanPT;
                    rep.lblNgayPT.Text = (qBB.ThoiGianDuKienPT != null) ? String.Format("{0:dd/MM/yyyy}", qBB.ThoiGianDuKienPT.Value) : "";
                    rep.lblPPPT.Text = qBB.PPPhauThuat;
                    rep.lblLoaiPT.Text = qBB.LoaiPT;
                    rep.lblDuKienDuongPT.Text = qBB.DuKienDuongPT;
                    rep.lblCBPT.Text = bspt;
                    rep.lblPPVoCam.Text = qBB.PPVoCam;
                    rep.lblNguoiTHVoCam.Text = bsgayme;
                    rep.lblDuTruMau.Text = qBB.DuTruMau;
                    rep.lblNhomMau.Text = (qBB.NhomMau != null) ? qBB.NhomMau.ToUpper() : "";
                    rep.lblYKienHD.Text = qBB.YKienHoiDong;
                    rep.celGiamDoc.Text = DungChung.Bien.GiamDoc;
                    rep.celTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
                    rep.celBSGayMe.Text = bsgayme;
                    rep.celBSDieuTri.Text = bsdt;
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Chưa chọn biên bản hội chẩn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btntttqmo_Click(object sender, EventArgs e)
        {
            int IDBB = 0;
            if (grvNhapCT.GetFocusedRowCellValue(colID) != null && grvNhapCT.GetFocusedRowCellValue(colID).ToString() != "")
            {
                IDBB = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colID));
                phieu_ttthongquamo(_mabn, IDBB);
            }
            else
            {
                MessageBox.Show("Chưa thực hiện hội chẩn cho bệnh nhân", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btn_sochoichan_Click(object sender, EventArgs e)
        {
            SoHC_30010(DataContect, _idkb, _mabn);
        }

        public static void SoHC_30010(QLBV_Database.QLBVEntities _Data, int bnkb, int mabn1)
        {
            frmIn frm = new frmIn();
            BaoCao.Rep_SoHoiChan rep = new BaoCao.Rep_SoHoiChan();
            var id = (from bb in _Data.BBHCs.Where(p => p.MaBNhan == mabn1)
                      group bb by bb.MaBNhan into kq
                      select new { kq.Key, IDKB = kq.Max(p => p.IDBB) }).ToList();

            if (id.Count > 0)
            {
                int _id = id.First().IDKB;
                rep.xrLabel5.Text = "3. Hôm nay, ngày.......tháng......năm " + DateTime.Now.Year + "; lúc........gờ.........phút.";
                var bn1 = (from a in _Data.BNKBs.Where(p => p.IDKB == bnkb)
                           join b in _Data.BBHCs.Where(p => p.IDBB == _id) on a.MaBNhan equals b.MaBNhan
                           join c in _Data.BenhNhans.Where(p => p.MaBNhan == mabn1) on a.MaBNhan equals c.MaBNhan
                           join d in _Data.TTboXungs on c.MaBNhan equals d.MaBNhan
                           join e in _Data.VaoViens on d.MaBNhan equals e.MaBNhan
                           select new { a.MaKP, c, d, e, b }).ToList();
                if (bn1.Count > 0)
                {
                    int? makp = bn1.First().MaKP;
                    var kp = (from a in _Data.KPhongs.Where(p => p.MaKP == makp) select new { a.TenKP }).ToList();
                    rep.KhoaDT.Value = kp.First().TenKP;
                    rep.TenBN.Value = bn1.First().c.TenBNhan;
                    rep.Tuoi.Value = bn1.First().c.Tuoi;
                    rep.GT.Value = bn1.First().c.GTinh == 1 ? "Nam" : "Nữ";
                    string madt = bn1.First().d.MaDT;
                    var dt = (from a in _Data.DanTocs.Where(p => p.MaDT == madt) select new { a.TenDT }).ToList();
                    rep.DanToc.Value = dt.Count > 0 ? dt.First().TenDT : "";
                    rep.NgoaiKieu.Value = bn1.First().d.NgoaiKieu;
                    string mann = bn1.First().d.MaNN;
                    var nn = (from a in _Data.DmNNs.Where(p => p.MaNN == mann) select new { a.TenNN }).ToList();
                    rep.NgheNghiep.Value = nn.Count > 0 ? nn.First().TenNN : "";
                    rep.NoiLV.Value = bn1.First().d.NoiLV;
                    rep.DiaChi.Value = bn1.First().c.DChi;
                    rep.SoVV.Value = bn1.First().e.SoVV;
                    rep.SoThe.Value = bn1.First().c.SThe;
                    rep.NgayGioVV.Value = bn1.First().e.NgayVao.Value.Hour + " giờ " + bn1.First().e.NgayVao.Value.Minute + " phút, ngày" + bn1.First().e.NgayVao.Value.Day + " tháng " + bn1.First().e.NgayVao.Value.Month + " năm " + bn1.First().e.NgayVao.Value.Year;
                    rep.TienSuVV.Value = bn1.First().e.TienSuBT;
                    rep.TinhTrangVV.Value = bn1.First().e.LyDo;
                    rep.ChanDoanVV.Value = bn1.First().e.ChanDoan;
                    rep.ChanDoanHC.Value = bn1.First().b.ChanDoan;
                    var dv = (from a in _Data.CLS.Where(p => p.MaKP == makp && p.MaBNhan == mabn1)
                              join b in _Data.ChiDinhs on a.IdCLS equals b.IdCLS
                              join c in _Data.DichVus on b.MaDV equals c.MaDV
                              join d in _Data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on c.IdTieuNhom equals d.IdTieuNhom
                              join e in _Data.CLScts on b.IDCD equals e.IDCD
                              group new { a, b, c, d } by new { a.MaBNhan } into kq
                              select new { IDCD = kq.Max(p => p.b.IDCD), kq.Key.MaBNhan }).ToList();
                    int idcd = dv.First().IDCD;
                    var dv1 = (from a in _Data.ChiDinhs.Where(p => p.IDCD == idcd)
                               join b in _Data.DichVus on a.MaDV equals b.MaDV
                               select new { a.NgayTH, b.TenDV, a.MaCBth, a.DSCBTH }).ToList();
                    rep.PPDT.Value = dv1.First().TenDV;
                    rep.ngayTH.Value = dv1.First().NgayTH.ToString();
                    string macbth = dv1.First().MaCBth;
                    var cb = (from a in _Data.CanBoes.Where(p => p.MaCB == macbth) select new { a.TenCB, a.ChucVu }).ToList();
                    rep.kipPT.Value = cb.Count > 0 ? (cb.First().TenCB + ", " + dv1.First().DSCBTH) : "";

                    macbth = bn1.First().b.MaCB;
                    cb = (from a in _Data.CanBoes.Where(p => p.MaCB == macbth) select new { a.TenCB, a.ChucVu }).ToList();
                    rep.BS1.Value = cb.First().TenCB + " ( " + cb.First().ChucVu + " )";

                    macbth = bn1.First().b.MaCBtk;
                    cb = (from a in _Data.CanBoes.Where(p => p.MaCB == macbth) select new { a.TenCB, a.ChucVu }).ToList();
                    rep.BS2.Value = cb.First().TenCB;

                    string[] arr = bn1.First().b.ThanhVien.Split(',');
                    rep.BS3.Value = arr[0];
                    rep.BS4.Value = bn1.First().b.ThanhVien.Split(',').LongLength > 1 ? arr[1] : "";
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void dtHoiChan_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}