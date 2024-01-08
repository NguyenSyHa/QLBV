using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_SoKiemNhapYHCT : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoKiemNhapYHCT()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region class DsChungTu
        public class DsChungTu
        {
            public int iddon;
            public string soctl;
            public string ngaynhap;
            public string ghichu;
            //public bool check;
            public bool chon;
            public int IDNhap
            {
                set { iddon = value; }
                get { return iddon; }
            }
            public string NgayNhap
            {
                set { ngaynhap = value; }
                get { return ngaynhap; }
            }
            public string SoCT
            {
                set { soctl = value; }
                get { return soctl; }
            }
            public string GhiChu
            {
                set { ghichu = value; }
                get { return ghichu; }
            }
            //public bool Check
            //{
            //    set { check = value; }
            //    get { return check; }
            //}


            public bool Chon
            { set { chon = value; } get { return chon; } }
        }
        #endregion

        #region class KHO
        private class KHO
        {
            private int MaKP;
            private string TenKP;
            public int makp
            {
                set { MaKP = value; }
                get { return MaKP; }
            }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
        }
        #endregion

        List<KHO> _lkho = new List<KHO>();
        List<DsChungTu> _lDSct = new List<DsChungTu>();
        private void frm_SoKiemNhapYHCT_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            DateTime _ng = System.DateTime.Now;
            var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == "Khoa dược") select new { khoa.TenKP, khoa.MaKP }).ToList();
            if (kd.Count() > 0)
            {
                KHO them1 = new KHO();
                them1.makp = 0;
                them1.tenkp = "Tất cả";
                _lkho.Add(them1);
                foreach (var a in kd)
                {
                    KHO themmoi = new KHO();
                    themmoi.makp = a.MaKP;
                    themmoi.tenkp = a.TenKP;
                    _lkho.Add(themmoi);
                }

                lupKho.Properties.DataSource = _lkho.ToList();
            }
            var dsid = data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= _ng && p.NgayNhap <= _ng).OrderByDescending(p => p.IDNhap).ToList();
            if (dsid.Count > 0)
            {
                DsChungTu themmoi1 = new DsChungTu();
                themmoi1.soctl = "Chọn tất cả";
                themmoi1.iddon = 0;
                themmoi1.ngaynhap = "";
                themmoi1.ghichu = "";
                themmoi1.chon = true;
                _lDSct.Add(themmoi1);
                foreach (var a in dsid)
                {
                    DsChungTu ds = new DsChungTu();
                    ds.iddon = a.IDNhap;
                    ds.soctl = a.SoCT;
                    ds.ghichu = a.GhiChu;
                    ds.ngaynhap = a.NgayNhap.ToString();
                    ds.chon = true;
                    _lDSct.Add(ds);
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupKho.EditValue == null)
            {
                if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
                {
                    if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                    {
                        DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                        DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                        var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                                  select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                        if (qn.Count > 0)
                            if (qn.Count > 0)
                            {
                                DsChungTu themmoi1 = new DsChungTu();
                                themmoi1.soctl = "Chọn tất cả";
                                themmoi1.iddon = 0;
                                themmoi1.ngaynhap = "";
                                themmoi1.ghichu = "";
                                themmoi1.chon = true;
                                _lDSct.Add(themmoi1);
                                foreach (var a in qn)
                                {
                                    DsChungTu ds = new DsChungTu();
                                    ds.iddon = a.IDNhap;
                                    ds.soctl = a.SoCT;
                                    ds.ghichu = a.GhiChu;
                                    ds.ngaynhap = a.NgayNhap.ToString();
                                    ds.chon = true;
                                    _lDSct.Add(ds);
                                }
                            }
                    }
                }
            }
            else
            {
                int _makp = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
                {
                    if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                    {
                        DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                        DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                        var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn && p.MaKP == _makp).Where(p => p.PLoai == 1)
                                  select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                        if (qn.Count > 0)
                            if (qn.Count > 0)
                            {
                                DsChungTu themmoi1 = new DsChungTu();
                                themmoi1.soctl = "Chọn tất cả";
                                themmoi1.iddon = 0;
                                themmoi1.ngaynhap = "";
                                themmoi1.ghichu = "";
                                themmoi1.chon = true;
                                _lDSct.Add(themmoi1);
                                foreach (var a in qn)
                                {
                                    DsChungTu ds = new DsChungTu();
                                    ds.iddon = a.IDNhap;
                                    ds.soctl = a.SoCT;
                                    ds.ghichu = a.GhiChu;
                                    ds.ngaynhap = a.NgayNhap.ToString();
                                    ds.chon = true;
                                    _lDSct.Add(ds);
                                }
                            }
                    }
                }

            }
            grcDSCT.DataSource = _lDSct.ToList();

        }

        private void lupTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            DsChungTu themmoi = new DsChungTu();
                            themmoi.soctl = "Chọn tất cả";
                            themmoi.iddon = 0;
                            themmoi.ngaynhap = "";
                            themmoi.ghichu = "";
                            themmoi.chon = true;
                            _lDSct.Add(themmoi);
                            foreach (var a in qn)
                            {
                                DsChungTu ds = new DsChungTu();
                                ds.iddon = a.IDNhap;
                                ds.soctl = a.SoCT;
                                ds.ghichu = a.GhiChu;
                                ds.ngaynhap = a.NgayNhap.ToString();
                                ds.chon = true;
                                _lDSct.Add(ds);
                            }
                        }
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();


        }

        private void lupDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            _lDSct.Clear();
            if (lupTuNgay.EditValue != null && lupTuNgay.EditValue.ToString() != "")
            {
                if (lupDenNgay.EditValue != null && lupDenNgay.EditValue.ToString() != "")
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var qn = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                              select new { nd.IDNhap, nd.SoCT, nd.NgayNhap, nd.GhiChu }).ToList();
                    if (qn.Count > 0)
                        if (qn.Count > 0)
                        {
                            DsChungTu themmoi1 = new DsChungTu();
                            themmoi1.soctl = "Chọn tất cả";
                            themmoi1.iddon = 0;
                            themmoi1.ngaynhap = "";
                            themmoi1.ghichu = "";
                            themmoi1.chon = true;
                            _lDSct.Add(themmoi1);
                            foreach (var a in qn)
                            {
                                DsChungTu ds = new DsChungTu();
                                ds.iddon = a.IDNhap;
                                ds.soctl = a.SoCT;
                                ds.ghichu = a.GhiChu;
                                ds.ngaynhap = a.NgayNhap.ToString();
                                ds.chon = true;
                                _lDSct.Add(ds);
                            }
                        }
                }
            }
            grcDSCT.DataSource = _lDSct.ToList();

        }
        List<_Id> _lid = new List<_Id>();
        public class _Id
        {
            private int id;
            public int ID
            {
                set { id = value; }
                get { return id; }
            }
        }
        private string _macbIn, _tencbIn;
        private void PassData(string maCB, string tenCB)
        {
            _macbIn = maCB;
            _tencbIn = tenCB;

        }
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            _lid.Clear();
            for (int k = 0; k < grvDSCT.RowCount; k++)
            {
                string a = grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower();
                if (grvDSCT.GetRowCellValue(k, colCheck).ToString().ToLower() == "true")
                {
                    _lid.Add(new _Id { ID = Convert.ToInt32(grvDSCT.GetRowCellValue(k, colIDNhap)) });
                }
            }
            if (_lid.Count > 0)
            {
                if (RGChonmau.SelectedIndex == 0)
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var nd1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                               join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               select new { nd.IDNhap, nd.SoCT, nd.MaCC, ndct.MaDV, ndct.SoLo, ndct.HanDung, ndct.SoLuongN, nd.TenNguoiCC }).ToList();
                    var _lnd1 = (from id in _lid
                                 join nd in nd1 on id.ID equals nd.IDNhap
                                 select new { nd.IDNhap, nd.SoCT, nd.MaCC, nd.MaDV, nd.SoLo, nd.HanDung, nd.SoLuongN, nd.TenNguoiCC }).ToList();
                    var dv1 = (from a in data.DichVus.Where(p => p.DongY == 1)
                               select new { a.MaDV, a.TenDV, a.TinhTNhap, a.NhaSX }).ToList();
                    var _lnd = (from nd in _lnd1
                                join dv in dv1 on nd.MaDV equals dv.MaDV
                                select new { nd.IDNhap, nd.SoCT, nd.MaDV, nd.SoLo, dv.TenDV, dv.TinhTNhap, nd.HanDung, nd.SoLuongN, dv.NhaSX, nd.MaCC, nd.TenNguoiCC }).ToList();
                    var _kq = (from nd in _lnd
                               join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC
                               group new { nd, ncc } by new { nd.IDNhap, nd.SoCT, nd.MaDV, nd.TenDV, nd.TinhTNhap, nd.SoLo, nd.HanDung, nd.NhaSX, ncc.TenCC, nd.TenNguoiCC } into kq
                               select new
                               {
                                   kq.Key.IDNhap,
                                   SoCT = DungChung.Bien.MaBV == "20001" ? ("Số chứng từ: " + kq.Key.SoCT) : kq.Key.SoCT,
                                   kq.Key.MaDV,
                                   kq.Key.TenDV,
                                   kq.Key.TinhTNhap,
                                   kq.Key.SoLo,
                                   kq.Key.HanDung,
                                   kq.Key.NhaSX,
                                   kq.Key.TenCC,
                                   TenNguoiCC = kq.Key.TenNguoiCC,
                                   SoLuongN = kq.Sum(p => p.nd.SoLuongN),
                                   chuacb = kq.Key.TinhTNhap == "C" ? "X" : "",
                                   dacb = (kq.Key.TinhTNhap == "S" || kq.Key.TinhTNhap == "P") ? "X" : "",

                               }).ToList();
                    if (DungChung.Bien.MaBV != "20001")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_SoKiemNhap_A4 rep = new BaoCao.Rep_SoKiemNhap_A4();
                        rep.TuNgay.Value = lupTuNgay.DateTime.ToShortDateString();
                        rep.DenNgay.Value = lupDenNgay.DateTime.ToShortDateString();
                        rep.tencqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.tenbv.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.celtencs.Text = "TÊN CƠ SỞ KHÁM BỆNH, CHỮA BỆNH: " + DungChung.Bien.TenCQ.ToUpper();
                        rep.DataSource = _kq.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_SoKiemNhap_A4_20001 rep = new BaoCao.Rep_SoKiemNhap_A4_20001();
                        rep.TuNgay.Value = lupTuNgay.DateTime.ToShortDateString();
                        rep.DenNgay.Value = lupDenNgay.DateTime.ToShortDateString();
                        rep.tencqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.tenbv.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.celtencs.Text = "TÊN CƠ SỞ KHÁM BỆNH, CHỮA BỆNH: " + DungChung.Bien.TenCQ.ToUpper();
                        rep.celngay.Text = DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);
                        rep.Tongso.Text = Convert.ToString(_kq.Count()) + " khoản";
                        if (_lid.Count > 1)
                        {
                            QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001 frmts = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001("Người giao hàng", 1);
                            frmts.passCB = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001.PassCB(PassData);
                            frmts.ShowDialog();
                            rep.DataSource = _kq.ToList();
                            rep.BindingData(1, _tencbIn);    
                        }
                        else
                        {
                            if (_kq.Count == 0)
                            {
                                string TenCb = _lnd1.First().TenNguoiCC == null ? "" : _lnd1.First().TenNguoiCC;
                                rep.BindingData(2, TenCb);
                            }
                            else
                            {
                                rep.DataSource = _kq.ToList();
                                rep.BindingData(0, null);
                            }
                           
                        }


                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    DateTime _dtn = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    DateTime _ddn = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    var nd1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= _dtn && p.NgayNhap <= _ddn).Where(p => p.PLoai == 1)
                               join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               select new { nd.IDNhap, nd.SoCT, nd.MaCC, ndct.MaDV, ndct.SoLo, ndct.HanDung, ndct.SoLuongN }).ToList();
                    var _lnd1 = (from id in _lid
                                 join nd in nd1 on id.ID equals nd.IDNhap
                                 select new { nd.IDNhap, nd.SoCT, nd.MaCC, nd.MaDV, nd.SoLo, nd.HanDung, nd.SoLuongN }).ToList();
                    var dv1 = (from a in data.DichVus.Where(p => p.DongY == 1)
                               select new { a.MaDV, a.TenDV, a.TinhTNhap, a.NhaSX }).ToList();
                    var _lnd = (from nd in _lnd1
                                join dv in dv1 on nd.MaDV equals dv.MaDV
                                select new { nd.IDNhap, nd.SoCT, nd.MaDV, nd.SoLo, dv.TenDV, dv.TinhTNhap, nd.HanDung, nd.SoLuongN, dv.NhaSX, nd.MaCC }).ToList();
                    var _kq = (from nd in _lnd
                               join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC
                               group new { nd, ncc } by new { nd.IDNhap, nd.SoCT, nd.MaDV, nd.TenDV, nd.TinhTNhap, nd.SoLo, nd.HanDung, nd.NhaSX, ncc.TenCC } into kq
                               select new
                               {
                                   kq.Key.IDNhap,
                                   SoCT = kq.Key.SoCT,
                                   kq.Key.MaDV,
                                   kq.Key.TenDV,
                                   kq.Key.TinhTNhap,
                                   kq.Key.SoLo,
                                   kq.Key.HanDung,
                                   kq.Key.NhaSX,
                                   kq.Key.TenCC,
                                   SoLuongN = kq.Sum(p => p.nd.SoLuongN),
                                   chuacb = kq.Key.TinhTNhap == "C" ? "X" : "",
                                   dacb = (kq.Key.TinhTNhap == "S" || kq.Key.TinhTNhap == "P") ? "X" : "",

                               }).ToList();

                    frmIn frm = new frmIn();
                    BaoCao.Rep_SoKiemNhap rep = new BaoCao.Rep_SoKiemNhap();
                    rep.TuNgay.Value = lupTuNgay.DateTime.ToShortDateString();
                    rep.DenNgay.Value = lupDenNgay.DateTime.ToShortDateString();
                    rep.tencqcq.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.tenbv.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.celtencs.Text = "TÊN CƠ SỞ KHÁM BỆNH, CHỮA BỆNH: " + DungChung.Bien.TenCQ.ToUpper();
                    rep.celngay.Text = "Ngày " + System.DateTime.Now.Day + " Tháng " + System.DateTime.Now.Month + " Năm " + System.DateTime.Now.Year;
                    rep.DataSource = _kq.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void grvDSCT_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "colCheck")
            {

                if (grvDSCT.GetFocusedRowCellValue(colIDNhap) != null)
                {
                    string q = grvDSCT.GetFocusedRowCellValue(colSoCT).ToString();

                    if (q == "Chọn tất cả")
                    {
                        string s = grvDSCT.GetFocusedRowCellValue(colCheck).ToString();
                        if (s.ToLower() == "true")
                        {
                            foreach (var a in _lDSct)
                            {
                                a.chon = false;

                            }
                        }
                        else
                        {
                            foreach (var a in _lDSct)
                            {
                                a.chon = true;

                            }
                        }
                        grcDSCT.DataSource = "";
                        grcDSCT.DataSource = _lDSct.ToList();
                    }
                }

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvDSCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

    }
}