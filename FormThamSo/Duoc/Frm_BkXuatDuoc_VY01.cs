using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_BkXuatDuoc_VY01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BkXuatDuoc_VY01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Kho> _lKho = new List<Kho>();
        List<Kho> _lKPnx = new List<Kho>();
        //List<PLDV> _PLDV = new List<PLDV>();
        List<KPhong> _lXP = new List<KPhong>();
        bool Checkxuatkhac = false;
        private void Frm_BkXuatDuoc_VY01_Load(object sender, EventArgs e)
        {

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lKho.Clear();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            _lXP = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong || p.TrongBV == 0).ToList();
            var kd = from khoa in data.KPhongs.Where(p => p.PLoai == "Khoa dược") select new { khoa.TenKP, khoa.MaKP };
            if (kd.Count() > 0)
            {
                Kho themmoi1 = new Kho();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Check = true;
                _lKho.Add(themmoi1);
                foreach (var a in kd)
                {
                    Kho themmoi = new Kho();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Check = true;
                    _lKho.Add(themmoi);
                }
                grcKho.DataSource = _lKho.ToList();
            }
            addDataPPXuat();
            var kdnx = from khoa in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong || p.TrongBV == 0) select new { khoa.TenKP, khoa.MaKP };
            if (kdnx.Count() > 0)
            {
                Kho themmoi1 = new Kho();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Check = true;
                _lKPnx.Add(themmoi1);
                foreach (var a in kdnx)
                {
                    Kho themmoi = new Kho();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Check = true;
                    _lKPnx.Add(themmoi);
                }
                grcKPhongnx.DataSource = _lKPnx.ToList();
            }
            var pldv = (from nhom in data.NhomDVs.Where(p => p.Status == 1)
                        select new
                        {
                            IDNhom = nhom.IDNhom,
                            TenNhomCT = nhom.TenNhomCT,
                            nhom.TenNhom,
                        })
                        .OrderBy(n => n.TenNhom).ToList();
            NhomDV moi2 = new NhomDV();
            moi2.TenNhom = " Tất cả";
            moi2.IDNhom = 0;
            moi2.TenNhomCT = "Tất cả";
            _lnhom.Add(moi2);
            //_lnhom.Add(new NhomDV { IDNhom = 0, TenNhomCT = "Tất cả", TenNhom = "Tất cả" });
            //_lnhom.InsertRange(1,
            foreach (var a in pldv)
            {
                NhomDV moi = new NhomDV();
                moi.TenNhom = a.TenNhom;
                moi.IDNhom = a.IDNhom;
                moi.TenNhomCT = a.TenNhomCT;
                _lnhom.Add(moi);
            }
            lupPLDV.Properties.DataSource = _lnhom;
            lupPLDV.EditValue = lupPLDV.Properties.GetKeyValueByDisplayText("Tất cả");
        }
        class TNDichVu
        {
            public int? IDTieuNhom { get; set; }
            public string TenTieuNhom { get; set; }
        }
        private List<TNDichVu> TieuNhomDichVuThuoc()
        {
            int? nhompl = Convert.ToInt32(lupPLDV.EditValue);
            List<TNDichVu> tndv = new List<TNDichVu>();
            var a = (from dv in data.DichVus
                     join tndv1 in data.TieuNhomDVs on dv.IdTieuNhom equals tndv1.IdTieuNhom
                     join nhom in data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                     group new { tndv1, nhom } by new { nhom.IDNhom, tndv1.IdTieuNhom, tndv1.TenTN, dv.PLoai } into kq

                     select new
                     {
                         kq.Key.IDNhom,
                         kq.Key.IdTieuNhom,
                         kq.Key.TenTN,
                         kq.Key.PLoai
                     }).Where(p => p.PLoai == 1 && ((nhompl == 0 || nhompl == null) ? true : p.IDNhom == nhompl)).OrderBy(p => p.IdTieuNhom).ToList();
            TNDichVu dv1 = new TNDichVu();
            dv1.IDTieuNhom = 0;
            dv1.TenTieuNhom = "Tất cả";
            tndv.Add(dv1);

            foreach (var item in a)
            {
                TNDichVu dvtm = new TNDichVu();
                dvtm.IDTieuNhom = item.IdTieuNhom;
                dvtm.TenTieuNhom = item.TenTN;
                tndv.Add(dvtm);
            }
            lupTieuNhomThuoc.Properties.DataSource = tndv.ToList();
            return tndv;
        }
        List<DSKP> _DSKP;

        private void btnInBC_Click(object sender, EventArgs e)
        {
            //int _k = radDG.SelectedIndex;
            if (KTtaoBc())
            {
                string _maCQCQ = "";
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qCQCQ = _data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                if (qCQCQ != null)
                    _maCQCQ = qCQCQ.MaChuQuan;
                string tenkho = "";
                List<Kho> khochon = new List<Kho>();
                List<int> kpnx = new List<int>();
                khochon = _lKho.Where(p => p.MaKP > 0 && p.Check == true).ToList();
                if (khochon.Count > 0)
                {
                    foreach (var item in khochon)
                    {
                        tenkho += item.TenKP + ",";
                    }
                }

                kpnx = _lKPnx.Where(p => p.MaKP > 0 && p.Check == true).Select(p => p.MaKP).ToList();
                if (DungChung.Bien.MaBV == "30003")
                {
                    if (kpnx.Count == 0)
                    {
                        kpnx = _lKPnx.Where(p => p.MaKP > 0).Select(p => p.MaKP).ToList();
                    }
                }
                frmIn frm = new frmIn();
                List<NhomDV> _DSNhom = new List<NhomDV>();
                if (Convert.ToInt32(lupPLDV.EditValue.ToString()) != 0)
                {
                    foreach (var item in _lnhom)
                    {
                        if (item.IDNhom.Equals(Convert.ToInt32(lupPLDV.EditValue.ToString())))
                        {
                            _DSNhom.Add(item);
                        }
                    }
                }
                else _DSNhom.AddRange(_lnhom);

                for (int i = 0; i < grvPPXuat.RowCount; i++)
                {
                    bool check = false;
                    int id = Convert.ToInt16(grvPPXuat.GetRowCellValue(i, colID));
                    if (grvPPXuat.GetRowCellValue(i, "Check") != null && grvPPXuat.GetRowCellValue(i, "Check").ToString().ToLower() == "true" && id >= -1)
                    {

                        check = true;

                    }
                    else
                    {
                        check = false;
                    }
                    foreach (var item in _lPhanLoaiX)
                    {
                        if (item.Id == id)
                        {
                            item.Check = check;
                            break;
                        }
                    }
                }
                int? idtieunhom = 0;
                if (TieuNhomDichVuThuoc().First().IDTieuNhom != null)
                {
                    idtieunhom = Convert.ToInt32(lupTieuNhomThuoc.EditValue);
                }

                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                string _plxuat = "";
                for (int i = 0; i < grvPPXuat.RowCount; i++)
                {
                    if (grvPPXuat.GetRowCellValue(i, "Check") != null && grvPPXuat.GetRowCellValue(i, "Check").ToString().ToLower() == "true")
                        _plxuat += grvPPXuat.GetRowCellValue(i, "PhanLoai").ToString() + ", ";
                }
                #region in tổng hợp
                if (rad_MauIn.SelectedIndex == 1)
                {
                    List<DungChung.Bien.c_PhanLoaiXuat> _lPLChon = new List<DungChung.Bien.c_PhanLoaiXuat>();
                    _lPLChon = (_lPhanLoaiX.Where(p => p.Check && p.Id >= 0).OrderBy(p => p.Id).ToList());
                    for (int i = _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList().Count; i < _lPhanLoaiX.Count; i++)
                    {
                        _lPLChon.Add(new DungChung.Bien.c_PhanLoaiXuat { Check = true, Id = -1, PhanLoai = "" });
                    }
                    _lPhanLoaiX = _lPhanLoaiX.OrderBy(p => p.Id).ToList();
                    _DSKP = new List<DSKP>(100);
                    for (int i = 0; i < 100; i++)
                    {
                        _DSKP.Add(new DSKP { makp = -1, tenkp = "" });
                    }
                    int j = 0;
                    foreach (var item in _lPhanLoaiX)
                    {
                        if (item.Check && item.Id >= 0)
                        {
                            _DSKP[j].tenkp = item.PhanLoai;
                            _DSKP[j].makp = item.Id;
                            j++;
                        }
                    }
                    var qsl_TH1 = (from nd in data.NhapDs.Where(p => (p.PLoai == 2) || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay)
                                   join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                   join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                   join nhdv in data.NhomDVs on dv.IDNhom equals nhdv.IDNhom
                                   join tndv in data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                                   select new
                                   {
                                       tndv.IdTieuNhom,
                                       nd.MaKP,
                                       nd.KieuDon,
                                       nd.PLoai,
                                       nd.TraDuoc_KieuDon,
                                       nhdv.TenNhomCT,
                                       dv.MaDV,
                                       TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? (dv.TenRG ?? "") : dv.TenDV,
                                       dv.DonVi,
                                       ndct.DonGia,
                                       dv.NuocSX,
                                       SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                       SoLuongX = (nd.PLoai == 2) ? ndct.SoLuongX : 0,
                                       ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                                       ThanhTienX = (nd.PLoai == 2) ? ndct.ThanhTienX : 0,
                                       nd.MaKPnx
                                   }).OrderBy(p => p.TenDV).ToList();

                    var qsl_TH = (from k in khochon
                                  join s in qsl_TH1.Where(p => p.MaKPnx != null && kpnx.Contains(p.MaKPnx ?? 0)) on k.MaKP equals s.MaKP
                                  join tndv in TieuNhomDichVuThuoc().Where(p => (idtieunhom == 0 || idtieunhom == null) ? true : (p.IDTieuNhom == idtieunhom)) on s.IdTieuNhom equals tndv.IDTieuNhom
                                  select s).ToList();

                    //var q_TH = (from pl in _lPhanLoaiX.Where(p => p.Check && p.Id >= 0)
                    //            join dc in qsl_TH on pl.Id equals dc.KieuDon
                    List<int> lplxuat = _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).Select(p => p.Id).ToList();
                    var q_TH = (from dc in qsl_TH.Where(p => (p.PLoai == 1 && p.KieuDon == 2) || (p.PLoai == 2 && lplxuat.Contains(p.KieuDon ?? 100)))
                                group dc by new { DonGia = radDG.SelectedIndex == 0 ? dc.DonGia : 0, dc.MaDV, dc.TenNhomCT, dc.TenDV, dc.DonVi, dc.NuocSX } into kq
                                select new
                                {
                                    kq.Key.TenNhomCT,
                                    MaDV = kq.Key.MaDV,
                                    TenDV = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,
                                    SL1 = kq.Where(k => k.KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL2 = kq.Where(k => k.KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL3 = kq.Where(k => k.KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL4 = kq.Where(k => k.KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL5 = kq.Where(k => k.KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL6 = kq.Where(k => k.KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL7 = kq.Where(k => k.KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL8 = kq.Where(k => k.KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL9 = kq.Where(k => k.KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL10 = kq.Where(k => k.KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    TC = kq.Sum(k => k.SoLuongX) - kq.Sum(k => k.SoLuongN),
                                    // thành tiền
                                    TT1 = kq.Where(k => k.KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT2 = kq.Where(k => k.KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT3 = kq.Where(k => k.KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT4 = kq.Where(k => k.KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT5 = kq.Where(k => k.KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT6 = kq.Where(k => k.KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT7 = kq.Where(k => k.KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT8 = kq.Where(k => k.KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT9 = kq.Where(k => k.KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT10 = kq.Where(k => k.KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TCTT = kq.Sum(k => k.ThanhTienX) - kq.Sum(k => k.ThanhTienN),

                                }).OrderBy(p => p.TenDV).ToList();

                    if (rad_MauIn.SelectedIndex == 1 && _plxuat == "Xuất khác, ")
                        Checkxuatkhac = true;
                    if (Checkxuatkhac) // Xuất khác ko có khoa phòng nhận xuất, chỉ chọn kho xuất và pp xuất rồi in BC
                    {
                        qsl_TH = (from k in khochon
                                  join s in qsl_TH1 on k.MaKP equals s.MaKP
                                  join tndv in TieuNhomDichVuThuoc().Where(p => (idtieunhom == 0 || idtieunhom == null) ? true : (p.IDTieuNhom == idtieunhom)) on s.IdTieuNhom equals tndv.IDTieuNhom
                                  select s).ToList();

                        q_TH = (from dc in qsl_TH.Where(p => p.PLoai == 2 && lplxuat.Contains(p.KieuDon ?? 100))
                                group dc by new { DonGia = radDG.SelectedIndex == 0 ? dc.DonGia : 0, dc.MaDV, dc.TenNhomCT, dc.TenDV, dc.DonVi, dc.NuocSX } into kq
                                select new
                                {
                                    kq.Key.TenNhomCT,
                                    MaDV = kq.Key.MaDV,
                                    TenDV = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,
                                    SL1 = kq.Where(k => k.KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL2 = kq.Where(k => k.KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL3 = kq.Where(k => k.KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL4 = kq.Where(k => k.KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL5 = kq.Where(k => k.KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL6 = kq.Where(k => k.KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL7 = kq.Where(k => k.KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL8 = kq.Where(k => k.KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL9 = kq.Where(k => k.KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    SL10 = kq.Where(k => k.KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.SoLuongX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.SoLuongN),
                                    TC = kq.Sum(k => k.SoLuongX) - kq.Sum(k => k.SoLuongN),
                                    //thành tiền
                                        TT1 = kq.Where(k => k.KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(0).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT2 = kq.Where(k => k.KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(1).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT3 = kq.Where(k => k.KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(2).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT4 = kq.Where(k => k.KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(3).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT5 = kq.Where(k => k.KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(4).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT6 = kq.Where(k => k.KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(5).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT7 = kq.Where(k => k.KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(6).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT8 = kq.Where(k => k.KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(7).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT9 = kq.Where(k => k.KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(8).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TT10 = kq.Where(k => k.KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.ThanhTienX) - kq.Where(k => k.TraDuoc_KieuDon == _lPLChon.Skip(9).Take(1).First().Id).Sum(p => p.ThanhTienN),
                                    TCTT = kq.Sum(k => k.ThanhTienX) - kq.Sum(k => k.ThanhTienN),

                                }).OrderBy(p => p.TenDV).ToList();
                    }

                    #region excel
                    int[] _arrWidth = new int[] { };
                    string[] _arr = new string[_DSKP.Where(p => p.tenkp != null && p.tenkp != "").ToList().Count * 2 + 6];

                    int num = 1;
                    string[] _tieude = new string[_DSKP.Where(p => p.tenkp != null && p.tenkp != "").ToList().Count * 2 + 6];
                    _tieude[0] = "STT";
                    _tieude[1] = "TenThuoc";
                    _tieude[2] = radDG.SelectedIndex == 0 ? "DonGia" : "Nước SX";
                    _tieude[3] = "Đơn vị";
                    int l = 4;
                    foreach (var item in _DSKP)
                    {
                        if (!string.IsNullOrEmpty(item.tenkp))
                        {
                            _tieude[l] = item.tenkp + " - SL";
                            _tieude[l + 1] = item.tenkp + " - TT";
                            l += 2;
                        }
                    }
                    _tieude[l] = "Tổng - SL";
                    _tieude[l + 1] = "Tổng - TT";

                    List<string> _ltieuDe = new List<string>();
                    for (int i = 0; i < 26; i++)
                    {
                        if (_tieude.Length >= i + 1)
                            _ltieuDe.Add(_tieude[i]);
                        else
                            _ltieuDe.Add("");

                    }

                    DungChung.Bien.MangHaiChieu = new Object[q_TH.Count + 2, _ltieuDe.Count + 1];
                    for (int i = 0; i < _ltieuDe.Count; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _ltieuDe.Skip(i).First();
                    }

                    foreach (var r in q_TH)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SL1;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.TT1;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SL2;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.TT2;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.SL3;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.TT3;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.SL4;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.TT4;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.SL5;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TT5;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.SL6;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.TT6;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.SL7;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.TT7;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.SL8;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.TT8;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.SL9;
                        DungChung.Bien.MangHaiChieu[num, 21] = r.TT9;
                        DungChung.Bien.MangHaiChieu[num, 22] = r.SL10;
                        DungChung.Bien.MangHaiChieu[num, 23] = r.TT10;
                        DungChung.Bien.MangHaiChieu[num, 24] = r.TC;
                        DungChung.Bien.MangHaiChieu[num, 25] = r.TCTT;
                        num++;
                    }
                    frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Xuất_SD", "C:\\BcHangXuat.xls", true, this.Name);
                    #endregion

                    for (int i = 0; i < _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList().Count; i += 8)
                    {
                        BaoCao.Rep_BkXuatDuoc_SLTT rep = new BaoCao.Rep_BkXuatDuoc_SLTT(_DSKP);
                        if (radDG.SelectedIndex == 0)
                            rep.DG.Value = 1;
                        else
                            rep.DG.Value = 2;
                        string ngaythang = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                        rep.TuNgayDenNgay.Value = ngaythang.ToString();
                        rep.TenKP.Value = tenkho;
                        rep.TenBC.Value = ("Báo cáo xuất dược theo phân loại").ToUpper();
                        rep.DataSource = q_TH;
                        rep.BindingData(i, _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList().Count);
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                #endregion
                #region in chi tiết
                else
                {
                    var PhanLoai = _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList();
                    List<int> lplxuat = PhanLoai.Select(p => p.Id).ToList();
                    //var lKP = (from pp in _lPhanLoaiX.Where(p => p.Check && p.Id >= 0)
                    var q1 = (from k in khochon
                              join nd in data.NhapDs.Where(p => (p.PLoai == 2 && lplxuat.Contains(p.KieuDon ?? 100)) || (p.PLoai == 1 && p.KieuDon == 2))
                                 .Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay) on k.MaKP equals nd.MaKP
                              join kp in data.KPhongs on nd.MaKPnx equals kp.MaKP
                              select new
                              {
                                  MaKP = kp.MaKP,
                                  kp.TenKP,
                                  kp.PLoai
                              }).Distinct().OrderBy(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
                    var lKP = (from kp in q1
                               join kp2 in kpnx on kp.MaKP equals kp2
                               select kp).ToList();
                    int j = 0;
                    _DSKP = new List<DSKP>(lKP.Count + 1);
                    for (int i = 0; i < lKP.Count + 1; i++)
                    {
                        //if(DungChung.Bien.MaBV.Equals("30009"))
                        //    _DSKP.Add(new DSKP { makp = 0, tenkp = "" });
                        //else
                        _DSKP.Add(new DSKP { makp = -1, tenkp = "" });
                    }
                    //if (DungChung.Bien.MaBV == "26007")
                    //    _DSKP.Add(new DSKP { makp = -2, tenkp = "Khác" });
                    for (int i = 0; i < lKP.Count; i++)
                    {
                        _DSKP[j].tenkp = lKP[i].TenKP;
                        _DSKP[j].makp = lKP[i].MaKP;
                        j++;
                    }
                    var qsl1 = (from nd in data.NhapDs.Where(p => (p.PLoai == 2 && lplxuat.Contains(p.KieuDon ?? 100))
                                                            || (lplxuat.Contains(p.TraDuoc_KieuDon) && p.PLoai == 1 && p.KieuDon == 2))
                                                     .Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay)
                                join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                join nhdv in data.NhomDVs on dv.IDNhom equals nhdv.IDNhom
                                join tndv in data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                                select new
                                {
                                    tndv.IdTieuNhom,
                                    tndv.TenTN,
                                    MaKho = nd.MaKP,
                                    nd.KieuDon,
                                    nd.TraDuoc_KieuDon,
                                    makp = nd.MaKPnx,
                                    nhdv.TenNhomCT,
                                    dv.MaDV,
                                    TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? (dv.TenRG ?? "") : dv.TenDV,
                                    ndct.DonVi,
                                    ndct.DonGia,
                                    dv.NuocSX,
                                    dv.MaTam,
                                    SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                    SoLuongX = (nd.PLoai == 2) ? ndct.SoLuongX : 0,
                                    ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                                    ThanhTienX = (nd.PLoai == 2) ? ndct.ThanhTienX : 0,
                                    nd.MaKPnx
                                }).ToList();
                    var qsl = (from k in khochon
                               join sl in qsl1.Where(p => p.MaKPnx != null && kpnx.Contains(p.MaKPnx ?? 0)) on k.MaKP equals sl.MaKho
                               join tndv in data.TieuNhomDVs.Where(p => (idtieunhom == 0 || idtieunhom == null) ? true : (p.IdTieuNhom == idtieunhom)) on sl.IdTieuNhom equals tndv.IdTieuNhom
                               select sl).OrderBy(p => p.TenDV).ToList();
                    var qsl2 = (from dskp in _DSKP
                                join kp in qsl on dskp.makp equals kp.makp
                                join dsn in _DSNhom on kp.TenNhomCT equals dsn.TenNhomCT
                                // join pl in PhanLoai on kp.KieuDon equals pl.Id
                                group kp by new { DonGia = radDG.SelectedIndex == 0 ? kp.DonGia : 0, kp.MaDV, kp.TenNhomCT, kp.TenDV, kp.DonVi, kp.NuocSX, kp.TenTN, kp.MaTam } into kq
                                select new
                                {
                                    kq.Key.MaTam,
                                    kq.Key.TenNhomCT,
                                    MaDV = kq.Key.MaDV,
                                    TenDV = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    TenTNDV = kq.Key.TenTN,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,
                                    SL1 = (1 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(0).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => (k.makp == _DSKP.Skip(0).Take(1).First().makp)).Sum(p => p.SoLuongN) : 0,
                                    SL2 = (2 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(1).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(1).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL3 = (3 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(2).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(2).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL4 = (4 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(3).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(3).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL5 = (5 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(4).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(4).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL6 = (6 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(5).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(5).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL7 = (7 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(6).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(6).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL8 = (8 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(7).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(7).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL9 = (9 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(8).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(8).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL10 = (10 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(9).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(9).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL11 = (11 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(10).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(10).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL12 = (12 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(11).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(11).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL13 = (13 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(12).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(12).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL14 = (14 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(13).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(13).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL15 = (15 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(14).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(14).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL16 = (16 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(15).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(15).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL17 = (17 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(16).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(16).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL18 = (18 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(17).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(17).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL19 = (19 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(18).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(18).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL20 = (20 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(19).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(19).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL21 = (21 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(20).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(20).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL22 = (22 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(21).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(21).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL23 = (23 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(22).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(22).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL24 = (24 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(23).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(23).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL25 = (25 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(24).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(24).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL26 = (26 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(25).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(25).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL27 = (27 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(26).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(26).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL28 = (28 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(27).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(27).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL29 = (29 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(28).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(28).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL30 = (30 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(29).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(29).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL31 = (31 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(30).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(30).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL32 = (32 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(31).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(31).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL33 = (33 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(32).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(32).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL34 = (34 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(33).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(33).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL35 = (35 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(34).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(34).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL36 = (36 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(35).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(35).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL37 = (37 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(36).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(36).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL38 = (38 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(37).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(37).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL39 = (39 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(38).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(38).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL40 = (40 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(39).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(39).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL41 = (41 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(40).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(40).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL42 = (42 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(41).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(41).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL43 = (43 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(42).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(42).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL44 = (44 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(43).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(43).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL45 = (45 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(44).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(44).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL46 = (46 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(45).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(45).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL47 = (47 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(46).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(46).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL48 = (48 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(47).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(47).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL49 = (49 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(48).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(48).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL50 = (50 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(49).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(49).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL51 = (51 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(50).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(50).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL52 = (52 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(51).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(51).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL53 = (53 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(52).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(52).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL54 = (54 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(53).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(53).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL55 = (55 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(54).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(54).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL56 = (56 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(55).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(55).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL57 = (57 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(56).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(56).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL58 = (58 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(57).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(57).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL59 = (59 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(58).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(58).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL60 = (60 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(59).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(59).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL61 = (61 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(60).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(60).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL62 = (62 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(61).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(61).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,

                                    SL63 = (63 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(62).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(62).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL64 = (64 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(63).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(63).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL65 = (65 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(64).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(64).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL66 = (66 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(65).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(65).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL67 = (67 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(66).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(66).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL68 = (68 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(67).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(67).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL69 = (69 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(68).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(68).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL70 = (70 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(69).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(69).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL71 = (71 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(70).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(70).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL72 = (72 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(71).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(71).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL73 = (73 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(72).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(72).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL74 = (74 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(73).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(73).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL75 = (75 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(74).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(74).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL76 = (76 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(75).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(75).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL77 = (77 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(76).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(76).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL78 = (78 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(77).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(77).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL79 = (79 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(78).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(78).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL80 = (80 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(79).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(79).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL81 = (81 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(80).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(80).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL82 = (82 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(81).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(81).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL83 = (83 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(82).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(82).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL84 = (84 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(83).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(83).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL85 = (85 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(84).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(84).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL86 = (86 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(85).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(85).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL87 = (87 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(86).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(86).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL88 = (88 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(87).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(87).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL89 = (89 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(88).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(88).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL90 = (90 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(89).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(89).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL91 = (91 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(90).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(90).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL92 = (92 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(91).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(91).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL93 = (93 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(92).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(92).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL94 = (94 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(93).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(93).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL95 = (95 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(94).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(94).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL96 = (96 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(95).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(95).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL97 = (97 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(96).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(96).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL98 = (98 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(97).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(97).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL99 = (99 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(98).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(98).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL100 = (100 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(99).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(99).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL101 = (101 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(100).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(100).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,
                                    SL102 = (102 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(101).Take(1).First().makp).Sum(p => p.SoLuongX) - kq.Where(k => k.makp == _DSKP.Skip(101).Take(1).First().makp).Sum(p => p.SoLuongN) : 0,




                                    TS = kq.Sum(k => k.SoLuongX),
                                    TN = kq.Sum(k => k.SoLuongN),

                                    TC = kq.Sum(k => k.SoLuongX) - kq.Sum(k => k.SoLuongN),
                                    //XuatKhacSL = kq.Where(p => p.KieuDon == 9).Sum(p => p.SoLuongX),
                                    // thành tiền
                                    TT1 = (1 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(0).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(0).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT2 = (2 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(1).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(1).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT3 = (3 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(2).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(2).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT4 = (4 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(3).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(3).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT5 = (5 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(4).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(4).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT6 = (6 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(5).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(5).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT7 = (7 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(6).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(6).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT8 = (8 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(7).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(7).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT9 = (9 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(8).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(8).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT10 = (10 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(9).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(9).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT11 = (11 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(10).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(10).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT12 = (12 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(11).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(11).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT13 = (13 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(12).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(12).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT14 = (14 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(13).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(13).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT15 = (15 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(14).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(14).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT16 = (16 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(15).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(15).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT17 = (17 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(16).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(16).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT18 = (18 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(17).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(17).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT19 = (19 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(18).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(18).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT20 = (20 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(19).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(19).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT21 = (21 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(20).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(20).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT22 = (22 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(21).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(21).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT23 = (23 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(22).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(22).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT24 = (24 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(23).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(23).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT25 = (25 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(24).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(24).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT26 = (26 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(25).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(25).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT27 = (27 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(26).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(26).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT28 = (28 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(27).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(27).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT29 = (29 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(28).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(28).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT30 = (30 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(29).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(29).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT31 = (31 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(30).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(30).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT32 = (32 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(31).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(31).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT33 = (33 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(32).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(32).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT34 = (34 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(33).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(33).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT35 = (35 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(34).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(34).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT36 = (36 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(35).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(35).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT37 = (37 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(36).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(36).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT38 = (38 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(37).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(37).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT39 = (39 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(38).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(38).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT40 = (40 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(39).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(39).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT41 = (41 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(40).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(40).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT42 = (42 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(41).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(41).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT43 = (43 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(42).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(42).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT44 = (44 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(43).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(43).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT45 = (45 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(44).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(44).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT46 = (46 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(45).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(45).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT47 = (47 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(46).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(46).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT48 = (48 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(47).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(47).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT49 = (49 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(48).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(48).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT50 = (50 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(49).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(49).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT51 = (51 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(50).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(50).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT52 = (52 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(51).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(51).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT53 = (53 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(52).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(52).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT54 = (54 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(53).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(53).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT55 = (55 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(54).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(54).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT56 = (56 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(55).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(55).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT57 = (57 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(56).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(56).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT58 = (58 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(57).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(57).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT59 = (59 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(58).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(58).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT60 = (60 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(59).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(59).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT61 = (61 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(60).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(60).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT62 = (62 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(61).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(61).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,

                                    TT63 = (63 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(62).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(62).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT64 = (64 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(63).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(63).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT65 = (65 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(64).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(64).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT66 = (66 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(65).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(65).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT67 = (67 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(66).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(66).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT68 = (68 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(67).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(67).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT69 = (69 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(68).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(68).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT70 = (70 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(69).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(69).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT71 = (71 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(70).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(70).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT72 = (72 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(71).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(71).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT73 = (73 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(72).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(72).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT74 = (74 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(73).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(73).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT75 = (75 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(74).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(74).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT76 = (76 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(75).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(75).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT77 = (77 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(76).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(76).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT78 = (78 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(77).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(77).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT79 = (79 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(78).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(78).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT80 = (80 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(79).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(79).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT81 = (81 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(80).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(80).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT82 = (82 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(81).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(81).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT83 = (83 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(82).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(82).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT84 = (84 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(83).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(83).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT85 = (85 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(84).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(84).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT86 = (86 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(85).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(85).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT87 = (87 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(86).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(86).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT88 = (88 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(87).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(87).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT89 = (89 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(88).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(88).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT90 = (90 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(89).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(89).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT91 = (91 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(90).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(90).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT92 = (92 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(91).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(91).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT93 = (93 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(92).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(92).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT94 = (94 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(93).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(93).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT95 = (95 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(94).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(94).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT96 = (96 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(95).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(95).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT97 = (97 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(96).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(96).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT98 = (98 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(97).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(97).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT99 = (99 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(98).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(98).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT100 = (100 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(99).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(99).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT101 = (101 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(100).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(100).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,
                                    TT102 = (102 <= lKP.Count) ? kq.Where(k => k.makp == _DSKP.Skip(101).Take(1).First().makp).Sum(p => p.ThanhTienX) - kq.Where(k => k.makp == _DSKP.Skip(101).Take(1).First().makp).Sum(p => p.ThanhTienN) : 0,

                                    TCTT = kq.Sum(k => k.ThanhTienX) - kq.Sum(k => k.ThanhTienN),
                                    //XuatKhacTT = kq.Where(p => p.KieuDon == 9).Sum(p => p.ThanhTienX),
                                }).OrderBy(p => p.TenDV).ToList();
                    string tenKP = "", tenPLDV = "";
                    //var   PhanLoai = _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList();
                    for (int i = 0; i < PhanLoai.Count; i++)
                    {
                        if (i == PhanLoai.Count - 1)
                        {
                            tenKP += PhanLoai[i].PhanLoai;
                        }
                        else
                            tenKP += PhanLoai[i].PhanLoai + ", ";
                    }
                    for (int i = 0; i < _DSNhom.Count; i++)
                    {
                        if (i == _DSNhom.Count - 1)
                        {
                            tenPLDV += " " + _DSNhom[i].TenNhomCT;

                        }
                        break;
                        //else
                        //    tenPLDV += _DSNhom[i].TenNhomCT + ",";
                    }
                    if (chk_HTThanhTien.Checked)
                    {
                        #region
                        #region excel
                        //int[] _arrWidth = new int[] { };
                        //string[] _arr = new string[_DSKP.Where(p => p.tenkp != null && p.tenkp != "").ToList().Count * 2 + 6];

                        //int num = 1;
                        //string[] _tieude = new string[_DSKP.Where(p => p.tenkp != null && p.tenkp != "").ToList().Count * 2 + 6];
                        //_tieude[0] = "STT";
                        //_tieude[1] = "TenThuoc";
                        //_tieude[2] = radDG.SelectedIndex == 0 ? "DonGia" : "Nước SX";
                        //_tieude[3] = "Đơn vị";
                        //int l = 4;
                        //foreach (var item in _DSKP)
                        //{
                        //    if (!string.IsNullOrEmpty(item.tenkp))
                        //    {
                        //        _tieude[l] = item.tenkp + " - SL";
                        //        _tieude[l + 1] = item.tenkp + " - TT";
                        //        l += 2;
                        //    }
                        //}
                        //_tieude[l] = "Tổng - SL";
                        //_tieude[l + 1] = "Tổng - TT";
                        //DungChung.Bien.MangHaiChieu = new Object[qsl2.Count + 1, _tieude.Length];
                        //for (int i = 0; i < _tieude.Length; i++)
                        //{
                        //    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        //}

                        //object[,] _manghaichieu = new Object[qsl2.Count + 1, _tieude.Length];
                        //for (int i = 0; i < qsl2.Count; i++)
                        //{
                        //    _manghaichieu[,] = qsl2.Skip(num).Take(1).First();
                        //    num++;
                        //}
                        //foreach (var r in qsl2)
                        //{
                        //    DungChung.Bien.MangHaiChieu[num, 0] = num;
                        //    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        //    DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                        //    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                        //    DungChung.Bien.MangHaiChieu[num, 4] = r.SL1;
                        //    DungChung.Bien.MangHaiChieu[num, 5] = r.TT1;
                        //    DungChung.Bien.MangHaiChieu[num, 6] = r.SL2;
                        //    DungChung.Bien.MangHaiChieu[num, 7] = r.TT2;
                        //    DungChung.Bien.MangHaiChieu[num, 8] = r.SL3;
                        //    DungChung.Bien.MangHaiChieu[num, 9] = r.TT3;
                        //    DungChung.Bien.MangHaiChieu[num, 10] = r.SL4;
                        //    DungChung.Bien.MangHaiChieu[num, 11] = r.TT4;
                        //    DungChung.Bien.MangHaiChieu[num, 12] = r.SL5;
                        //    DungChung.Bien.MangHaiChieu[num, 13] = r.TT5;
                        //    DungChung.Bien.MangHaiChieu[num, 14] = r.SL6;
                        //    DungChung.Bien.MangHaiChieu[num, 15] = r.TT6;
                        //    DungChung.Bien.MangHaiChieu[num, 16] = r.SL7;
                        //    DungChung.Bien.MangHaiChieu[num, 17] = r.TT7;
                        //    DungChung.Bien.MangHaiChieu[num, 18] = r.SL8;
                        //    DungChung.Bien.MangHaiChieu[num, 19] = r.TT8;
                        //    DungChung.Bien.MangHaiChieu[num, 20] = r.SL9;
                        //    DungChung.Bien.MangHaiChieu[num, 21] = r.TT9;
                        //    DungChung.Bien.MangHaiChieu[num, 22] = r.SL10;
                        //    DungChung.Bien.MangHaiChieu[num, 23] = r.TT10;
                        //    DungChung.Bien.MangHaiChieu[num, 24] = r.TC;
                        //    DungChung.Bien.MangHaiChieu[num, 25] = r.TCTT;
                        //    num++;
                        //}
                        //frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Xuất_SD", "C:\\BcHangXuat.xls", true, this.Name);
                        #endregion
                        for (int i = 0; i < lKP.Count; i += 10)
                        {
                            var ktkpa = _DSKP.Where(p => p.makp == 66).ToList();
                            BaoCao.Rep_BkXuatDuoc_SLTT rep = new BaoCao.Rep_BkXuatDuoc_SLTT(_DSKP);
                            if (radDG.SelectedIndex == 0)

                                rep.DG.Value = 1;
                            else
                                rep.DG.Value = 2;
                            string ngaythang = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                            rep.TuNgayDenNgay.Value = ngaythang.ToString();
                            rep.TenKP.Value = tenkho;
                            rep.TenBC.Value = ("Báo cáo xuất" + tenPLDV + ": " + tenKP).ToUpper();
                            rep.DataSource = qsl2.Where(p => p.TS != 0 || p.TN != 0).ToList();
                            rep.BindingData(i, lKP.Count);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        #endregion
                    }
                    else
                    {
                        if (lKP.Count > 19)
                        {

                            int num = 1;
                            for (int i = 0; i < lKP.Count; i += 34)
                            {

                                BaoCao.Rep_BkXuatDuoc_VY001 rep = new BaoCao.Rep_BkXuatDuoc_VY001(_DSKP, num);
                                if (radDG.SelectedIndex == 0)

                                    rep.DG.Value = 1;
                                else
                                    rep.DG.Value = 2;
                                string ngaythang = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                                rep.TuNgay.Value = ngaythang.ToString();
                                //var kp = from k in data.KPhongs.Where(p => p.MaKP == _makpx) select new { k.TenKP };
                                rep.TenKho.Value = tenkho;

                                //tenPLDV += lupPLDV.Text;
                                rep.TenBC.Value = ("Báo cáo xuất" + tenPLDV + ": " + tenKP).ToUpper();
                                rep.DataSource = qsl2.Where(p => p.TS != 0 || p.TN != 0).ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                                num++;
                            }



                            //grvPPXuat.Focus();
                        }
                        else
                        {
                            BaoCao.Rep_BkXuatDuoc_VY01 rep = new BaoCao.Rep_BkXuatDuoc_VY01(_DSKP);
                            if (radDG.SelectedIndex == 0)

                                rep.DG.Value = 1;
                            else
                                rep.DG.Value = 2;
                            string ngaythang = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                            rep.TuNgayDenNgay.Value = ngaythang.ToString();
                            //var kp = from k in data.KPhongs.Where(p => p.MaKP == _makpx) select new { k.TenKP };
                            rep.TenKP.Value = tenkho;

                            //tenPLDV += lupPLDV.Text;
                            rep.TenBC.Value = ("Báo cáo xuất" + tenPLDV + ": " + tenKP).ToUpper();
                            rep.DataSource = qsl2.Where(p => p.TS != 0 || p.TN != 0).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }

                    }
                }
                #endregion
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<DungChung.Bien.c_PhanLoaiXuat> _lPhanLoaiX = new List<DungChung.Bien.c_PhanLoaiXuat>();

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            List<Kho> khochon = new List<Kho>();
            khochon = _lKho.Where(p => p.MaKP > 0 && p.Check == true).ToList();
            if (khochon.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn kho xuất dược");
                grcKho.Focus();
                return false;
            }

            if (lupPLDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại dược");
                lupPLDV.Focus();
                return false;
            }
            return true;
        }
        public void addDataPPXuat()
        {
            _lPhanLoaiX = new List<DungChung.Bien.c_PhanLoaiXuat>();
            grcPPXuat.DataSource = null;
            _lPhanLoaiX = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            _lPhanLoaiX.Add(new DungChung.Bien.c_PhanLoaiXuat { Id = -1, PhanLoai = "Chọn tất cả", Check = true });
            grcPPXuat.DataSource = _lPhanLoaiX.OrderBy(p => p.Id);
        }


        public class DSKP
        {
            private string TenKP;
            private int MaKP;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
        }
        List<NhomDV> _lnhom = new List<NhomDV>();

        #region Kho
        private class Kho
        {
            public bool Check { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion

        private void rad_MauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rad_MauIn.SelectedIndex == 0)
                chk_HTThanhTien.Visible = true;
            else
            {
                if (rad_MauIn.SelectedIndex == 2)
                {
                    grcPPXuat.Enabled = false;
                }
                else
                {
                    grcPPXuat.Enabled = true;
                }
                chk_HTThanhTien.Visible = false;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a = (from nd in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 0 && (p.MaKPnx == null))
                     join rv in data.RaViens.Where(p => p.MaKP != null) on nd.MaBNhan equals rv.MaBNhan
                     select new { nd, rv }).ToList();
            foreach (var item in a)
            {
                item.nd.MaKPnx = item.rv.MaKP;
            }
            data.SaveChanges();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void grvKho_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKho.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKho.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKho.First().Check == true)
                        {
                            foreach (var a in _lKho)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKho)
                            {
                                a.Check = true;
                            }
                        }
                        grcKho.DataSource = "";
                        grcKho.DataSource = _lKho.ToList();
                    }
                }
            }
        }

        private void grvKPhongnx_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChonnx")
            {
                if (grvKPhongnx.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKPhongnx.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKPnx.First().Check == true)
                        {
                            foreach (var a in _lKPnx)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKPnx)
                            {
                                a.Check = true;
                            }
                        }
                        grcKPhongnx.DataSource = "";
                        grcKPhongnx.DataSource = _lKPnx.ToList();
                    }
                }
            }
        }

        private void grvPPXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Check")
            {
                if (e.RowHandle == 0)
                {
                    if (grvPPXuat.GetFocusedRowCellValue(Check) != null)
                    {
                        if (grvPPXuat.GetRowCellValue(0, Check).ToString() == "False")
                        {
                            grvPPXuat.SetRowCellValue(0, "Check", true);
                            for (int i = 1; i < grvPPXuat.RowCount; i++)
                            {
                                if (grvPPXuat.GetRowCellValue(i, Check).ToString() == "False")
                                    grvPPXuat.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            grvPPXuat.SetRowCellValue(0, "Check", false);
                            for (int i = 1; i < grvPPXuat.RowCount; i++)
                            {
                                if (grvPPXuat.GetRowCellValue(i, Check).ToString() == "True")
                                    grvPPXuat.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    if (grvPPXuat.GetFocusedRowCellValue("Check") != null)
                    {
                        if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "False")// chọn
                        {
                            grvPPXuat.SetFocusedRowCellValue(Check, "True");

                        }
                        else if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "True")// bỏ chọn
                        {
                            grvPPXuat.SetFocusedRowCellValue(Check, "False");
                        }
                    }

                }

                //if (grvPPXuat.GetFocusedRowCellValue("Check") != null)
                //{

                //    if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "False")// chọn
                //    {
                //        grvPPXuat.SetFocusedRowCellValue(Check, "True");

                //    }
                //    else if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "True")// bỏ chọn
                //    {
                //        grvPPXuat.SetFocusedRowCellValue(Check, "False");
                //    }
                //}
                //if (grvPPXuat.GetFocusedRowCellValue("PhanLoai") != null)
                //{
                //    string Ten = grvPPXuat.GetFocusedRowCellValue("PhanLoai").ToString();
                //    if (Ten == "Chọn tất cả")
                //    {
                //        if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "True")
                //        {
                //            foreach (var a in _lPhanLoaiX)
                //            {
                //                if (a.Check == false)
                //                {
                //                    a.Check = true;
                //                }
                //            }
                //        }
                //        else if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "False")
                //        {
                //            foreach (var a in _lPhanLoaiX)
                //            {

                //                if (a.Check == true)
                //                    a.Check = false;
                //            }
                //        }
                //        grcPPXuat.DataSource = "";
                //        grcPPXuat.DataSource = _lPhanLoaiX.ToList();

                //    }

                //if (Ten == "Chọn tất cả")
                //{
                //    if (_lPhanLoaiX.First().Check == true)
                //    {
                //        foreach (var a in _lPhanLoaiX)
                //        {
                //            if (a.Check == true)
                //            {
                //                a.Check = false;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        foreach (var a in _lPhanLoaiX)
                //        {
                //                a.Check = true;
                //        }
                //    }
                //    grcPPXuat.DataSource = "";
                //    grcPPXuat.DataSource = _lPhanLoaiX.ToList();

                //}

                //if (Ten == "Xuất ngoài BV")
                //{

                //        if (grvPPXuat.GetFocusedRowCellValue("Check") != null)
                //        {

                //            if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "False")// chọn
                //            {
                //                grvPPXuat.SetFocusedRowCellValue(Check, "True");

                //            }
                //            else if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "True")// bỏ chọn
                //            {
                //                grvPPXuat.SetFocusedRowCellValue(Check, "False");
                //            }
                //        }

                //}
                #region
                //
                //if (Ten == "Xuất ngoài BV" || Ten == "Chọn tất cả")
                //{
                //    if (_lXP.Count > 0)
                //    {
                //        if (grvPPXuat.GetFocusedRowCellValue("Check") != null)
                //        {
                //            if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "False")// chọn
                //            {
                //                grvPPXuat.SetFocusedRowCellValue(Check, "True");
                //                foreach (var a in _lXP)
                //                {
                //                    Kho themmoi = new Kho();
                //                    themmoi.TenKP = a.TenKP;
                //                    themmoi.MaKP = a.MaKP;
                //                    themmoi.Check = true;
                //                    _lKPnx.Add(themmoi);
                //                }
                //                grcKPhongnx.DataSource = _lKPnx.ToList();
                //            }
                //            else if (grvPPXuat.GetFocusedRowCellValue("Check").ToString() == "True")// bỏ chọn
                //            {
                //                grvPPXuat.SetFocusedRowCellValue(Check, "False");
                //                List<int> lmakp = _lXP.Select(p => p.MaKP).ToList();
                //                foreach (int a in lmakp)
                //                {
                //                    Kho khoxa = _lKPnx.Where(p => p.MaKP == a).FirstOrDefault();
                //                    if (khoxa != null)
                //                        _lKPnx.Remove(khoxa);
                //                }
                //                grcKPhongnx.DataSource = _lKPnx.ToList();
                //            }
                //        }
                //    }
                //}
                #endregion
                //}

            }
        }
        private void grvPPXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Check")
            {
                if (grvPPXuat.GetRowCellValue(e.RowHandle, "PhanLoai") != null)
                {

                    string Ten = grvPPXuat.GetRowCellValue(e.RowHandle, "PhanLoai").ToString();
                    if (Ten == "Xuất ngoài BV")
                    {
                        if (_lXP.Count > 0)
                        {
                            if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check") != null)
                            {
                                if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check").ToString() == "True")// chọn
                                {
                                    foreach (var a in _lXP)
                                    {
                                        Kho themmoi = new Kho();
                                        themmoi.TenKP = a.TenKP;
                                        themmoi.MaKP = a.MaKP;
                                        themmoi.Check = true;
                                        _lKPnx.Add(themmoi);
                                    }
                                    grcKPhongnx.DataSource = _lKPnx.ToList();
                                }
                                else if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check").ToString() == "False")// bỏ chọn
                                {
                                    List<int> lmakp = _lXP.Select(p => p.MaKP).ToList();
                                    foreach (int a in lmakp)
                                    {
                                        Kho khoxa = _lKPnx.Where(p => p.MaKP == a).FirstOrDefault();
                                        if (khoxa != null)
                                            _lKPnx.Remove(khoxa);
                                    }
                                    grcKPhongnx.DataSource = _lKPnx.ToList();
                                }
                            }
                        }
                    }
                    /* if (Ten == "Xuất khác") // Xuất khác ko có kpnx
                     {
                         if (_lXP.Count > 0)
                         {
                             if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check") != null)
                             {
                                 if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check").ToString() == "True")// chọn
                                 {
                                     foreach (var a in _lXP)
                                     {
                                         Checkxuatkhac = true;
                                         rad_MauIn.SelectedIndex = 1;
                                         rad_MauIn.Enabled = false;
                                     }
                                 }
                                 else if (grvPPXuat.GetRowCellValue(e.RowHandle, "Check").ToString() == "False")// bỏ chọn
                                 {
                                     Checkxuatkhac = false;
                                     rad_MauIn.SelectedIndex = 0;
                                     rad_MauIn.Enabled = true;
                                 }
                             }
                         }
                     }*/
                }

            }
        }

        private void grvPPXuat_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }

        private void repositoryItemCheckEdit1_CheckStateChanged(object sender, EventArgs e)
        {




        }

        private void lupPLDV_EditValueChanged(object sender, EventArgs e)
        {
            TieuNhomDichVuThuoc();
        }





    }
}