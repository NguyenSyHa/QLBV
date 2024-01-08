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
using System.IO;

namespace QLBV.CLS
{
    public partial class frmInPhieuNS_TMH : DevExpress.XtraEditors.XtraForm
    {
        int _IDCLS = 0, _maBN = 0;
        public frmInPhieuNS_TMH(int IDCLS, int MabN)
        {
            InitializeComponent();
            this._IDCLS = IDCLS;
            this._maBN = MabN;
        }
        void InPhieu(int _IDCLS, int _mabn, int[] Index)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var par = (from bn in _Data.BenhNhans
                       join cls in _Data.CLS.Where(p => p.IdCLS == _IDCLS) on bn.MaBNhan equals cls.MaBNhan
                       join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                       join chidinh in _Data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join clsct in _Data.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                       where bn.MaBNhan == _mabn
                       select new { kp.TenKP, kp.IsDongY, chidinh.Status, cls.MaKPth, cls.IdCLS, bn.DTuong, cls.MaCBth, dvct.STT, cls.NgayTH, clsct.KetQua, clsct.DuongDan, clsct.DuongDan2, cls.NgayThang, cls.MaKP, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.SThe, cls.MaCB, chidinh.ChiDinh1, chidinh.KetLuan, dv.TenDV, chidinh.LoiDan }).ToList();
            int _makp = 0;
            if (par.Count > 0)
                _makp = par.First().MaKP == null ? 0 : par.First().MaKP.Value;
            if (par.Count > 0)
            {
                _makp = par.First().MaKP == null ? 0 : par.First().MaKP.Value;
                if (DungChung.Bien._Visible_CDHA[1] == false)
                {
                    if (par.First().Status == 1)
                        DungChung.Bien._Visible_CDHA[2] = true;
                    else
                        DungChung.Bien._Visible_CDHA[2] = false;
                }
            }
            int _mkp4 = 0;
            if (DungChung.Bien.MaBV == "30009")
            {
                frmIn frm4 = new frmIn();
                BaoCao.Rep_PhieuNoiSoiTMH_30009_moi rep4 = new BaoCao.Rep_PhieuNoiSoiTMH_30009_moi();
                if (par.Count > 0)
                {
                    var ttbx = (from tt in _Data.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                join nn in _Data.DmNNs on tt.MaNN equals nn.MaNN into kq
                                from kq1 in kq.DefaultIfEmpty()
                                select new { tt.DThoai, NgheNghiep = kq1 == null ? "" : kq1.TenNN }).FirstOrDefault();
                    if (ttbx != null)
                    {
                        rep4.NgheNghiep.Value = ttbx.NgheNghiep;
                        rep4.tel.Value = ttbx.DThoai;
                    }
                    _mkp4 = par.First().MaKP == null ? 0 : par.First().MaKP.Value;
                    rep4.MaBNhan.Value = par.First().MaBNhan;
                    rep4.ChiDinh.Value = par.First().TenDV + (string.IsNullOrEmpty(par.First().ChiDinh1) ? "" : ("_" + par.First().ChiDinh1));
                    rep4.paramHoTen.Value = par.First().TenBNhan.ToUpper();
                    rep4.paramDiaChi.Value = par.First().DChi;
                    rep4.IDCLS.Value = _IDCLS;
                    rep4.paramGioiTinh.Value = par.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep4.paramSThe.Value = par.First().SThe;
                    rep4.paramDoiTuong.Value = par.First().DTuong;
                    rep4.paramTuoi.Value = DungChung.Ham.TuoitheoThang(_Data, _mabn, DungChung.Bien.formatAge);
                    rep4.paramKetLuan.Value = par.First().KetLuan;
                    rep4.paraLoiDan.Value = par.First().LoiDan;
                    rep4.paramPhieuSo.Value = par.First().IdCLS;
                    if (par.First().NgayThang != null)
                    {
                        rep4.paramNgayCD.Value = DungChung.Ham.NgaySangChu(par.First().NgayThang.Value, DungChung.Bien.FormatDate);
                    }
                    else
                        rep4.paramNgayCD.Value = ".....giờ, ngày ..... tháng ..... năm ......";
                    if (par.First().NgayTH != null)
                        rep4.paramNgayTH.Value = DungChung.Ham.NgaySangChu(par.First().NgayTH.Value, DungChung.Bien.FormatDate);
                    else
                        rep4.paramNgayTH.Value = ".....giờ, ngày ..... tháng ..... năm ......";
                    int _makpth = 0;
                    if (par.First().MaKPth != null)
                    {
                        _makpth = par.First().MaKPth.Value;

                    }
                    var kp = _Data.KPhongs.Where(p => p.MaKP == _makpth).ToList();
                    if (kp.Count > 0)
                        rep4.paramPhongKham.Value = kp.First().TenKP.ToUpper();
                    string a = "", b = ""; ; a = par.First().MaCB;
                    b = par.First().MaCBth;

                    if (!string.IsNullOrEmpty(a))
                        rep4.paramCanBoDT.Value = DungChung.Ham._getTenCB(_Data, a).ToUpper();
                    if (!string.IsNullOrEmpty(b))
                        rep4.BSTH.Value = DungChung.Ham._getTenCB(_Data, b).ToUpper();
                    string ddan = "", ketqua = "";
                    if (par.First().DuongDan2 != null)
                        ddan = par.First().DuongDan2;
                    if (par.First().KetQua != null)
                        ketqua = par.First().KetQua;

                    rep4.hienthi(ketqua, ddan, Index);
                }
                var kb4 = (from kb2 in _Data.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.MaKP == _mkp4) select new { kb2.IDKB, kb2.ChanDoan, kb2.BenhKhac }).OrderByDescending(p => p.IDKB).ToList();
                if (kb4.Count > 0)
                    rep4.paramChanDoan.Value = kb4.First().ChanDoan + "/" + kb4.First().BenhKhac;

                rep4.CreateDocument();
                frm4.prcIN.PrintingSystem = rep4.PrintingSystem;
                frm4.ShowDialog();
            }
            else
            {
                frmIn frm4 = new frmIn();
                BaoCao.Rep_PhieuNoiSoiTMH_30009 rep4 = new BaoCao.Rep_PhieuNoiSoiTMH_30009();
                if (par.Count > 0)
                {
                    var ttbx = (from tt in _Data.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                join nn in _Data.DmNNs on tt.MaNN equals nn.MaNN into kq
                                from kq1 in kq.DefaultIfEmpty()
                                select new { tt.DThoai, NgheNghiep = kq1 == null ? "" : kq1.TenNN }).FirstOrDefault();
                    if (ttbx != null)
                    {
                        rep4.NgheNghiep.Value = ttbx.NgheNghiep;
                        rep4.tel.Value = ttbx.DThoai;
                    }
                    if (DungChung.Bien.MaBV == "24297")
                    {
                        var kpcd = (from kp2 in _Data.KPhongs.Where(p => p.MaKP == _makp)
                                    select new { KPCD = kp2.TenKP }).ToList();
                        rep4.KPhong.Value = kpcd.First().KPCD;
                    }
                    else
                    {
                        rep4.KPhong.Value = par.First().TenKP;
                        _mkp4 = par.First().MaKP == null ? 0 : par.First().MaKP.Value;
                    }

                    rep4.MaBNhan.Value = par.First().MaBNhan;
                    rep4.ChiDinh.Value = par.First().TenDV + (string.IsNullOrEmpty(par.First().ChiDinh1) ? "" : ("_" + par.First().ChiDinh1));
                    rep4.paramHoTen.Value = par.First().TenBNhan.ToUpper();
                    rep4.paramDiaChi.Value = par.First().DChi;
                    rep4.IDCLS.Value = _IDCLS;
                    rep4.paramGioiTinh.Value = par.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep4.paramSThe.Value = par.First().SThe;
                    rep4.paramDoiTuong.Value = par.First().DTuong;
                    rep4.paramTuoi.Value = DungChung.Ham.TuoitheoThang(_Data, _mabn, DungChung.Bien.formatAge);
                    rep4.paramKetLuan.Value = par.First().KetLuan;
                    rep4.paraLoiDan.Value = par.First().LoiDan;
                    rep4.paramPhieuSo.Value = par.First().IdCLS;
                    if (par.First().NgayThang != null)
                    {
                        rep4.paramNgayCD.Value = DungChung.Ham.NgaySangChu(par.First().NgayThang.Value, DungChung.Bien.FormatDate);
                    }
                    else
                        rep4.paramNgayCD.Value = ".....giờ, ngày ..... tháng ..... năm ......";
                    if (par.First().NgayTH != null)
                        rep4.paramNgayTH.Value = DungChung.Ham.NgaySangChu(par.First().NgayTH.Value, DungChung.Bien.FormatDate);
                    else
                        rep4.paramNgayTH.Value = ".....giờ, ngày ..... tháng ..... năm ......";
                    int _makpth = 0;
                    if (par.First().MaKPth != null)
                    {
                        _makpth = par.First().MaKPth.Value;

                    }
                    var kp = _Data.KPhongs.Where(p => p.MaKP == _makpth).ToList();
                    if (kp.Count > 0)
                        rep4.paramPhongKham.Value = kp.First().TenKP.ToUpper();
                    string a = "", b = ""; ; a = par.First().MaCB;
                    b = par.First().MaCBth;

                    if (!string.IsNullOrEmpty(a))
                        rep4.paramCanBoDT.Value = DungChung.Ham._getTenCB(_Data, a).ToUpper();
                    if (!string.IsNullOrEmpty(b))
                        rep4.BSTH.Value = DungChung.Ham._getTenCB(_Data, b).ToUpper();
                    string ddan = "", ketqua = "";
                    if (par.First().DuongDan2 != null)
                        ddan = par.First().DuongDan2;
                    if (par.First().KetQua != null)
                        ketqua = par.First().KetQua;

                    rep4.hienthi(ketqua, ddan, Index);
                }
                if (DungChung.Bien.MaBV == "24297" && par.First().IsDongY == true)
                {
                    rep4.paramChanDoan.Value = DungChung.Ham.GetChanDoanKB_ByKP(_Data, _mabn, _makp);
                }
                else
                {
                    var kb4 = (from kb2 in _Data.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.MaKP == _makp) select new { kb2.IDKB, kb2.ChanDoan, kb2.BenhKhac }).OrderByDescending(p => p.IDKB).ToList();
                    if (kb4.Count > 0)
                        rep4.paramChanDoan.Value = kb4.First().ChanDoan + "/" + kb4.First().BenhKhac;
                }


                rep4.CreateDocument();
                frm4.prcIN.PrintingSystem = rep4.PrintingSystem;
                frm4.ShowDialog();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int[] Index = new int[4] { 10, 10, 10, 10 };
            if (chkchon1.Checked)
                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 0;
                        break;
                    }
                }

            if (chkchon2.Checked)
                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 1;
                        break;
                    }
                }

            if (chkchon3.Checked)
                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 2;
                        break;
                    }
                }

            if (chkchon4.Checked)
                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 3; break;
                    }
                }
            if (chkchon5.Checked)

                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 4; break;
                    }
                }
            if (chkchon6.Checked)

                for (int i = 0; i < 4; i++)
                {
                    if (Index[i] == 10)
                    {
                        Index[i] = 5; break;
                    }
                }
            if (chkchon7.Checked)
                for (int i = 0; i < 4; i++)
                {

                    if (Index[i] == 10)
                    {
                        Index[i] = 6; break;
                    }
                }

            InPhieu(_IDCLS, _maBN, Index);
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void loadKetQuaTMH(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        mmeTMH1.Text = arr[0];
                        break;
                    case 1:
                        mmeTMH2.Text = arr[1];
                        break;
                    case 2:
                        mmeTMH3.Text = arr[2];
                        break;
                    case 3:
                        mmeTMH4.Text = arr[3];
                        break;
                    case 4:
                        mmeTMH5.Text = arr[4];
                        break;
                    case 5:
                        mmeTMH6.Text = arr[5];
                        break;
                    case 6:
                        mmeTMH7.Text = arr[6];
                        break;
                    default:
                        break;
                }
            }
        }
        private void frmInPhieuNS_TMH_Load(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _CLSct = (from cls in _Data.CLS.Where(p => p.IdCLS == _IDCLS) join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD select clsct).ToList();
            if (_CLSct.Count > 0)
            {
                String strDD = _CLSct.First().DuongDan2;
                string Duongdandasua = strDD;
                string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                string KetQua = _CLSct.First().KetQua;
                loadKetQuaTMH(QLBV_Library.QLBV_Ham.LayChuoi('|', KetQua));
                for (int i = 0; i < arrDD.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                            {
                                ptTMH1.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH1.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH1.Image = null;
                            }
                            break;
                        case 1:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH2.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH2.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH2.Image = null;
                            }
                            break;
                        case 2:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH3.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH3.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH3.Image = null;
                            }
                            break;
                        case 3:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH4.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH4.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH4.Image = null;
                            }
                            break;
                        case 4:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH5.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH5.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH5.Image = null;
                            }
                            break;
                        case 5:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH6.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH6.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH6.Image = null;
                            }
                            break;
                        case 6:
                            if (string.IsNullOrEmpty(arrDD[i]))
                            {
                                ptTMH7.Image = null;
                            }
                            else
                            {
                                if (File.Exists(arrDD[i]))
                                    ptTMH7.Image = Image.FromFile(arrDD[i]);
                                else
                                    ptTMH7.Image = null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}