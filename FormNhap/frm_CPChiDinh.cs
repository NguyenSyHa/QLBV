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
    public partial class frm_CPChiDinh : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_CPChiDinh()
        {
            InitializeComponent();
        }
        public frm_CPChiDinh(int ma)
        {
            InitializeComponent();
            _mabn = ma;
        }

        public class lCPCHiDinh
        {
            public string tendv, donvi;
            public double dongia, soluong, thanhtien, tienbn;
            private bool chon = false;
            private int idcd, madv;
            int trongBH;
            private int status;
            private int maKP;
            public int MaKPth { get; set; }
            public int IDDonct { set; get; }
            public int? PLoai { get; set; }
            public int Status
            {
                get { return status; }
                set { status = value; }
            }
            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            public int TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }
            public int IDCD
            {
                set { idcd = value; }
                get { return idcd; }
            }
            public bool Chon
            {
                set { chon = value; }
                get { return chon; }
            }
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string DonVi
            {
                set { donvi = value; }
                get { return donvi; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public double ThanhTien
            {
                set { thanhtien = value; }
                get { return thanhtien; }
            }
            public double TienBN
            {
                set { tienbn = value; }
                get { return tienbn; }
            }
            int mien;

            public int Mien
            {
                get { return mien; }
                set { mien = value; }
            }
        }
        int _pttt = 0;
        string _muc = "";
        string _dtuong = "";
        int _tuyen = 0;
        double _tienTThu = 0;
        int _madvCK = 0;
        List<int> _lMadvCK = new List<int>();
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //public delegate void _getstring(string chuoi1, string chuoi2, string chuoi3, bool giatri, List<lCPCHiDinh> lcdinh, int ploaithu, int mack);
        public delegate void _getstring(string chuoi1, string chuoi2, string chuoi3, bool giatri, List<lCPCHiDinh> lcdinh, int ploaithu, List<int> mack, int IDGoiDV, bool thuBangThe);
        public _getstring GetData;
        List<lCPCHiDinh> _lchidinh = new List<lCPCHiDinh>();
        List<lCPCHiDinh> _ldvKeThuThang = new List<lCPCHiDinh>();// dịch vụ kê tại tab dịch vụ (không qua chỉ định, ko tính công khám)
        List<lCPCHiDinh> _lAll = new List<lCPCHiDinh>();
        bool _ktraSuaTienCongKham = false;
        private void frm_CPChiDinh_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV!="30372")
            {
                ckMienGiamAll.Visible = false;
                lbMien.Visible = true;
                txtMien.Visible = true;
                colMien.Visible = false;
                colXoa.Visible = false;
            }
            
            if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30012")
                txtNoiDung.Text = "Thu tiền viện phí";
            else if (DungChung.Bien.MaBV == "30012")
            {
                txtNoiDung.Text = "Thu viện phí";
            }
            ckcCheckThuGoi.Checked = false;

            if (DungChung.Bien.MaBV == "23456" || DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "30372")
            {
                chkDuoc.Visible = true;
                chkDuoc.Location = new Point(293, 49);
            }

            //radTrongNgoaiDM.SelectedIndex == 0;
            int mien = 0;
            if (!string.IsNullOrEmpty(txtMien.Text))
                mien = Convert.ToInt32(txtMien.Text);
            // tính tiền công khám
            //
            _tienTThu = 0;
            int _noitru = -1, HTTT = -1;
            //if (DungChung.Bien.MaBV == "01071")
            //{
            //    var _KtraCLS = _dataContext.CLS.Where(p => p.MaBNhan == _mabn).ToList();
            //    if (_KtraCLS.Count == 0)
            //    {
            //        chkTienKham.Checked = true;
            //    }
            //}
            var dt = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            if (dt.Count > 0)
            {
                _noitru = dt.First().NoiTru.Value;
                int _idDTBN = dt.First().IDDTBN;
                if ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") && _idDTBN == 3)
                    ckcCheckThuGoi.Visible = true;
                int? _makp = dt.First().MaKP;

                var DTBNhan = _dataContext.DTBNs.Where(p => p.IDDTBN == _idDTBN).ToList();
                if (DTBNhan.Count > 0)
                {
                    HTTT = DTBNhan.First().HTTT;
                    if (DungChung.Bien.MaBV == "30005" && DTBNhan.First().DTBN1 == "KSK")
                        _ktraSuaTienCongKham = true;
                }
                //
                int _HangBV = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                double _tyle = 0;

                switch (_HangBV)
                {
                    case 1:
                        if (_noitru == 0)
                            _tyle = 0;
                        else
                            _tyle = 40;

                        break;
                    case 2:
                        if (_noitru == 0)
                            _tyle = 0;
                        else
                            _tyle = 60;

                        break;
                    case 3:
                        _tyle = 70;

                        break;
                    case 4:
                        _tyle = 100;

                        break;
                }

                _lchidinh.Clear();
                _lAll.Clear();
                _ldvKeThuThang.Clear();
                if (dt.Count > 0)
                {
                    var chidinh = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _mabn)
                                   join cd in _dataContext.ChiDinhs.Where(p => p.SoPhieu == null || p.SoPhieu <= 0) on cls.IdCLS equals cd.IdCLS
                                   join dv in _dataContext.DichVus on cd.MaDV equals dv.MaDV
                                   select new { cls.MaKP, cd.IDCD, cd.MaDV, dv.TenDV, DonGia = cd.DonGia, dv.DonVi, SoLuong = 1, ThanhTien = cd.DonGia, cd.TamThu, cd.TrongBH, cd.Status, dv.PLoai, cls.MaKPth }).ToList();
                    _dtuong = dt.First().DTuong;
                    if (dt.First().SThe != null && dt.First().SThe.Length > 2)
                        _muc = dt.First().SThe.ToString().Substring(2, 1);
                    if (dt.First().Tuyen != null)
                        _tuyen = dt.First().Tuyen.Value;
                    _pttt = DungChung.Ham._PtramTT(_dataContext, _muc);




                    #region add chỉ định
                    foreach (var a in chidinh)
                    {
                        lCPCHiDinh moi = new lCPCHiDinh();
                        moi.MaKP = a.MaKP ?? 0;
                        moi.MaKPth = a.MaKPth ?? 0;
                        moi.TenDV = a.TenDV;
                        moi.MaDV = a.MaDV == null ? 0 : a.MaDV.Value;
                        moi.IDCD = a.IDCD;
                        moi.DonVi = a.DonVi;
                        moi.DonGia = a.DonGia;
                        moi.ThanhTien = a.ThanhTien;
                        moi.SoLuong = a.SoLuong;
                        moi.Status = a.Status ?? 0;
                        moi.Mien = mien;
                        moi.PLoai = a.PLoai;
                        if (a.TrongBH == 1 && HTTT == 1)
                            moi.TienBN = _tuyen == 1 ? a.ThanhTien * (100 - _pttt) / 100 : a.ThanhTien * (1 - _tyle * _pttt / 10000) * (100 - mien) / 100;
                        else
                            moi.TienBN = a.ThanhTien * (100 - mien) / 100;

                        if (a.TrongBH == 1)
                            moi.Chon = false;
                        else
                            moi.Chon = true;

                        moi.TrongBH = a.TrongBH ?? 0;
                        _lchidinh.Add(moi);
                        // _lMadvCK.Add(moi.MaDV);
                    }
                    #endregion


                    #region add công khám
                    var ck = (from nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                              join dvu in _dataContext.DichVus.Where(p => p.PLoai == 2 && p.Status == 1 && (DungChung.Bien.MaBV == "20001" ? true : (p.Loai == _idDTBN))) on nhom.IDNhom equals dvu.IDNhom
                              select new { dvu.DonGia, dvu.MaDV, dvu.DonGia2, dvu.DonGiaDV2, dvu.MaQD, dvu.DonVi, dvu.TrongDM, dvu.TenDV, dvu.MaKPsd, dvu.PLoai }).OrderByDescending(p => p.DonGia).ToList();

                    if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30012" || _noitru == 0) && chkTienKham.Checked) //thu trực tiếp công khám cho bn dịch vụ nội trú 30012 đoài y/cầu 16-10
                    {
                        #region xác định có khai báo công khám theo chuyên khoa
                        List<string> lck = new List<string>();
                        for (int i = 1; i < 18; i++)
                        {
                            lck.Add(i.ToString("D2") + ".1895");
                            lck.Add(i.ToString("D2") + ".1896");
                            lck.Add(i.ToString("D2") + ".1897");
                            lck.Add(i.ToString("D2") + ".1898");
                            lck.Add(i.ToString("D2") + ".1899");

                        }
                        var qCheckChuyenKhoa = ck.Where(p => lck.Contains(p.MaQD)).ToList();
                        bool boolTheoChuyenKhoa = false;
                        if (qCheckChuyenKhoa.Count > 0)
                            boolTheoChuyenKhoa = true;
                        #endregion



                        List<int> _lmack = ck.Select(p => p.MaDV).ToList();


                        if (ck.Count > 0)
                        {
                            _madvCK = ck.First().MaDV;
                        }

                        _lMadvCK = new List<int>();
                        //01071_ dung 100917

                        var congkham = (from dtc in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                        join dtct in _dataContext.DThuoccts//.Where(p => p.IDCD == null || p.IDCD <= 0)
                                        on dtc.IDDon equals dtct.IDDon
                                        join dv in _dataContext.DichVus.Where(p => _lmack.Count > 0 && _lmack.Contains(p.MaDV)) on dtct.MaDV equals dv.MaDV
                                        join bnkb in _dataContext.BNKBs on dtct.IDKB equals bnkb.IDKB into kq
                                        from kq1 in kq.DefaultIfEmpty()
                                        select new { dv.TenDV, dv.MaDV, dv.MaQD, dtct.DonVi, dtct.DonGia, dtct.IDDonct, dtct.ThanhTien, dtct.IDKB, dv.Loai, MaKP = kq1 == null ? dtc.MaKP : kq1.MaKP, dv.MaKPsd, dtct.IDCD, MaCK = kq1 == null ? -1 : kq1.MaCK, dv.PLoai, dtct.TrongBH }).ToList();//.OrderByDescending(p => p.DonGia).ToList();

                        var qcongkham = (from a in congkham.Where(p => p.MaKP != null && p.MaKPsd != null && (p.MaKPsd.Contains(";" + p.MaKP.ToString() + ";"))).Where(p => HTTT != 1 || checkCongKham(_dataContext, p.Loai, boolTheoChuyenKhoa, p.MaQD, p.MaCK, p.MaKP ?? 0)) select a).OrderByDescending(p => p.DonGia).ToList();

                        #region đã có công khám
                        if (congkham.Count > 0)
                        {
                            if (qcongkham.Count > 0)
                            {
                                qcongkham = qcongkham.Where(p => p.IDCD == null || p.IDCD <= 0).ToList();// chỉ lấy những công khám chưa thu thẳng
                                #region 01071: Bệnh nhân dịch vụ sẽ lấy tất cả các công khám trong bảng dịch vụ dành cho đối tượng dịch vụ (mặc định là ko tick chọn đối với những công khám chưa được kê)
                                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") // bn dịch vụ                              
                                {

                                    //add công khám đã có trong đơn thuốc
                                    foreach (var a in congkham)
                                    {
                                        lCPCHiDinh moi = new lCPCHiDinh();
                                        moi.MaKP = a.MaKP ?? 0;
                                        moi.IDCD = a.IDDonct;
                                        moi.TenDV = a.TenDV;
                                        moi.MaDV = a.MaDV;
                                        moi.DonVi = a.DonVi;
                                        moi.DonGia = a.DonGia;
                                        moi.ThanhTien = a.ThanhTien;
                                        moi.Mien = mien;
                                        moi.SoLuong = 1;
                                        moi.PLoai = a.PLoai;
                                        if (DungChung.Bien.MaBV == "20001" && qcongkham.First().TrongBH == 0)
                                        {
                                            moi.TienBN = moi.ThanhTien * ((double)(100 - mien) / (double)100);
                                        }
                                        else
                                        {
                                            if (HTTT == 1)
                                                moi.TienBN = _tuyen == 1 ? a.ThanhTien * (100 - _pttt) / 100 : a.ThanhTien * (1 - _tyle * _tyle / 10000) * (100 - mien) / 100;// dung 100917
                                            else
                                                moi.TienBN = a.ThanhTien * (100 - mien) / 100;
                                        }
                                        if (DungChung.Bien.MaBV == "20001")
                                            moi.TrongBH = qcongkham.First().TrongBH;
                                        else
                                        {
                                            if (HTTT == 1)
                                                moi.TrongBH = 1;
                                            else
                                                moi.TrongBH = 0;
                                        }
                                        moi.Chon = true;
                                        _lchidinh.Add(moi);
                                        _lMadvCK.Add(moi.MaDV);
                                    }

                                }
                                #endregion
                                #region bv khác
                                else
                                {
                                    if (qcongkham.Count > 0)
                                    {
                                        qcongkham = qcongkham.OrderBy(p => p.IDDonct).ToList();// lấy công khám đầu tiên
                                        lCPCHiDinh moi = new lCPCHiDinh();
                                        moi.MaKP = qcongkham.First().MaKP ?? 0;
                                        moi.IDCD = qcongkham.First().IDDonct;
                                        moi.IDDonct = qcongkham.First().IDDonct;
                                        moi.TenDV = qcongkham.First().TenDV;
                                        moi.MaDV = qcongkham.First().MaDV;
                                        moi.DonVi = qcongkham.First().DonVi;
                                        moi.DonGia = qcongkham.First().DonGia;
                                        moi.ThanhTien = qcongkham.First().DonGia;
                                        moi.Mien = mien;
                                        moi.PLoai = qcongkham.First().PLoai;
                                        moi.SoLuong = 1;
                                        if (DungChung.Bien.MaBV == "20001" && qcongkham.First().TrongBH == 0)
                                        {
                                            moi.TienBN = moi.ThanhTien * ((double)(100 - mien) / (double)100);
                                        }
                                        else
                                        {
                                            if (HTTT == 1)
                                                moi.TienBN = _tuyen == 1 ? qcongkham.First().DonGia * (100 - _pttt) / 100 : qcongkham.First().DonGia * (1 - _tyle * _pttt / 10000) * (100 - mien) / 100;
                                            else
                                                moi.TienBN = qcongkham.First().DonGia * (100 - mien) / 100;
                                        }

                                        if (DungChung.Bien.MaBV == "20001")
                                            moi.TrongBH = qcongkham.First().TrongBH;
                                        else
                                        {
                                            if (HTTT == 1)
                                                moi.TrongBH = 1;
                                            else
                                                moi.TrongBH = 0;
                                        }
                                        moi.Chon = true;
                                        _lchidinh.Add(moi);
                                        _lMadvCK.Add(moi.MaDV);
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        #region chưa có công khám
                        //dung 100917
                        else
                        {
                            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345") && HTTT == 1))
                            {
                                var _lck1 = ck.Where(p => p.MaKPsd.Contains(_makp.ToString())).ToList();
                                DungChung.Ham.Update_Delete_CongKham(_mabn, -1, true, DateTime.Now);
                                var _lck = (from dthuoc in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                            join dtct in _dataContext.DThuoccts on dthuoc.IDDon equals dtct.IDDon
                                            select dtct).ToList();
                                var _listck = (from a in _lck1
                                               join b in _lck on a.MaDV equals b.MaDV
                                               select new { a.TenDV, b, a.PLoai }).ToList();
                                if (_listck.Count > 0)
                                {
                                    lCPCHiDinh moi = new lCPCHiDinh();
                                    moi.MaKP = _listck.First().b.MaKP ?? 0;
                                    moi.IDCD = -1;
                                    moi.TenDV = _listck.First().TenDV;
                                    moi.MaDV = Convert.ToInt32(_listck.First().b.MaDV);
                                    moi.DonVi = _listck.First().b.DonVi;
                                    moi.DonGia = _listck.First().b.DonGia;
                                    moi.Mien = mien;
                                    moi.ThanhTien = _listck.First().b.DonGia;
                                    moi.PLoai = _listck.First().PLoai;
                                    moi.SoLuong = 1;
                                    if (DungChung.Bien.MaBV == "20001" && qcongkham.Count > 0 && qcongkham.First().TrongBH == 0)
                                    {
                                        moi.TienBN = moi.ThanhTien * ((double)(100 - mien) / (double)100);
                                    }
                                    else
                                    {
                                        if (HTTT == 1)
                                            moi.TienBN = _tuyen == 1 ? _listck.First().b.DonGia * (100 - _pttt) / 100 : _listck.First().b.DonGia * (1 - _tyle * _pttt / 10000) * (100 - mien) / 100;
                                        else
                                            moi.TienBN = _listck.First().b.DonGia * (100 - mien) / 100;
                                    }

                                    if (DungChung.Bien.MaBV == "20001" && qcongkham.Count > 0)
                                        moi.TrongBH = qcongkham.First().TrongBH;
                                    else
                                    {
                                        if (HTTT == 1)
                                            moi.TrongBH = 1;
                                        else
                                            moi.TrongBH = 0;
                                    }
                                    moi.Chon = true;
                                    _lchidinh.Add(moi);
                                    _lMadvCK.Add(moi.MaDV);

                                }
                            }
                        }
                        #endregion

                        #region add công khám chưa có trong đơn thuốc
                        if (HTTT != 1 && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")) // bệnh nhân không phải BHYT
                        {
                            List<int> _CKdaKe = congkham.Select(p => p.MaDV).ToList();
                            var qCKChuaKe = ck.Where(p => !_CKdaKe.Contains(p.MaDV)).ToList();
                            int giacu = DungChung.Ham.GiaCu(_mabn, -1);

                            int idkb = -1;
                            var qbnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).FirstOrDefault();
                            if (qbnkb != null)
                                idkb = qbnkb.IDKB;
                            int mack_CK = DungChung.Ham.GetMackham_ck(_mabn, idkb, DateTime.Now);

                            foreach (var a in qCKChuaKe)
                            {

                                lCPCHiDinh moi = new lCPCHiDinh();
                                moi.MaKP = 0;
                                moi.TenDV = a.TenDV;
                                moi.MaDV = a.MaDV;
                                moi.DonVi = a.DonVi;
                                moi.DonGia = giacu == 0 ? a.DonGia2 : a.DonGiaDV2;
                                moi.ThanhTien = moi.DonGia;
                                moi.Mien = mien;
                                moi.SoLuong = 1;
                                moi.TienBN = moi.DonGia * (100 - mien) / 100;
                                moi.TrongBH = 0;
                                moi.PLoai = a.PLoai;
                                if (a.MaDV == mack_CK && congkham.Count == 0)
                                    moi.Chon = true;
                                else
                                    moi.Chon = false;
                                _lchidinh.Add(moi);
                                _lMadvCK.Add(moi.MaDV);
                            }
                        }
                        #endregion
                    }

                    #endregion

                    #region add dịch vụ kê trực tiếp không tính công khám his757-05092018

                    List<int> qtu = (from tu in _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn)
                                     join tuct in _dataContext.TamUngcts.Where(p => p.IDDonct != null).Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                     select new { IDDonct = tuct.IDDonct.Value }).Select(p => p.IDDonct).Distinct().ToList();
                    List<int> lMaDVCK = ck.Select(p => p.MaDV).Distinct().ToList(); // lấy mã dịch vụ công khám
                    var qdvke = (from dtdv in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2 || (chkDuoc.Checked && p.PLDV == 1))// chỉ lấy dịch vụ, không lấy đơn thuốc
                                 join dtct in _dataContext.DThuoccts.Where(p => p.ThanhToan != 1).Where(p => p.IDCD == null || p.IDCD == 0).Where(p => lMaDVCK.Count == 0 || !lMaDVCK.Contains(p.MaDV.Value)).Where(p => qtu.Count == 0 || !qtu.Contains(p.IDDonct))
                                 on dtdv.IDDon equals dtct.IDDon
                                 join dvu in _dataContext.DichVus on dtct.MaDV equals dvu.MaDV
                                 select new {dtct.MaKP, dvu.TenDV, dvu.MaDV, dtct.DonGia, dtct.DonVi, dvu.DonGia2, dvu.DonGiaDV2, dtct.SoLuong, dtct.TrongBH, dtct.IDDonct, dvu.PLoai }).ToList();
                    
                    foreach (var a in qdvke)
                    {

                        lCPCHiDinh moi = new lCPCHiDinh();
                        if (DungChung.Bien.MaBV == "30372")
                            moi.MaKP = Convert.ToInt32(a.MaKP);
                        else
                            moi.MaKP = 0;
                        moi.TenDV = a.TenDV;
                        moi.MaDV = a.MaDV;
                        moi.DonVi = a.DonVi;
                        moi.DonGia = a.DonGia;
                        moi.Mien = mien;
                        moi.SoLuong = a.SoLuong;
                        moi.ThanhTien = a.SoLuong * a.DonGia;
                        moi.Mien = mien;
                        moi.IDDonct = a.IDDonct;
                        moi.PLoai = a.PLoai;
                        if (a.TrongBH == 1 && HTTT == 1)
                            moi.TienBN = _tuyen == 1 ? moi.ThanhTien * (100 - _pttt) / 100 : moi.ThanhTien * (1 - _tyle * _pttt / 10000) * (100 - mien) / 100;
                        else
                            moi.TienBN = moi.ThanhTien * (100 - mien) / 100;

                        if (a.TrongBH != 1)
                            moi.Chon = true;
                        else
                            moi.Chon = false;

                        moi.TrongBH = a.TrongBH;
                        _ldvKeThuThang.Add(moi);

                    }

                    #endregion
                    _lAll.AddRange(_lchidinh);
                    if (ckDichVuKhac.Checked || chkDuoc.Checked)
                        _lAll.AddRange(_ldvKeThuThang);

                    if (chkDuoc.Checked)
                        _lAll = _lAll.Where(o => o.PLoai == 1).ToList();

                    _tienTThu = _lAll.Where(p => p.Chon == true).Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).Sum(p => p.TienBN);
                }
                grcThanhToan.DataSource = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
                txtTamThu.Text = _tienTThu.ToString();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loai">Trường loại trong bảng dịch vụ: ID đối tượng bệnh nhân</param>
        /// <param name="boolTheoChuyenKhoa">true: Công khám được set theo chuyên khoa ; false: Công khám không set theo chuyên khoa mà do bệnh viện tự thiết lập theo từng khoa phòng</param>

        /// <param name="MaQD"></param>
        /// <param name="MaCK">Mã chuyên khoa</param>      
        /// <returns></returns>
        private bool checkCongKham(QLBV_Database.QLBVEntities data, int? loai, bool boolTheoChuyenKhoa, string MaQD, int MaCK, int MaKP)
        {
            if (boolTheoChuyenKhoa)
            {
                #region Công khám được set theo chuyên khoa
                if (MaCK == -1)
                {
                    var qck = data.KPhongs.Where(p => p.MaKP == MaKP).FirstOrDefault();
                    if (qck != null)
                        MaCK = qck.MaCK;
                }

                var a = DungChung.Bien._lChuyenKhoa.Where(p => p.MaCK == MaCK).Select(p => p.MaCK_QD).FirstOrDefault();
                string mack_qd = a.ToString("D2");
                string maCongKham = mack_qd + "." + DungChung.Bien.mack_theoHangBV[DungChung.Ham.hangBVCK(DungChung.Bien.MaBV)];
                if (maCongKham == MaQD)
                    return true;
                else
                    return false;


                #endregion
            }
            else
            {
                #region Công khám không được set theo chuyên khoa (được set theo khoa phòng)

                string maCongKham = (loai == null ? "" : loai.ToString()) + "." + DungChung.Bien.mack_theoHangBV[DungChung.Ham.hangBVCK(DungChung.Bien.MaBV)];
                if (maCongKham == MaQD)
                    return true;
                else
                    return false;
                #endregion
            }


        }
        bool tinhtamung = false, Ktratmthu = false;
        private void btnOK_Click(object sender, EventArgs e)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Ham.KTraTT(_dataContext, _mabn) == false)
            {
                var kt = _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.MaBNhan == _mabn).ToList();
                bool check = ckcCheckThuGoi.Checked;
                if (kt.Count > 0 && DungChung.Bien.MaBV != "30372")
                {
                    MessageBox.Show("Bệnh nhân đã được duyệt chi phí, bạn không thể tạm thu");
                }
                else if (radTrongNgoaiDM.SelectedIndex != 0 && cboPLoaithu.EditValue == "Thu trực tiếp")
                {
                    MessageBox.Show("Thu trực tiếp chỉ dành cho dịch vụ ngoài danh mục", "Thông báo", MessageBoxButtons.OK);
                }
                else if (check && _lAll.Where(p => p.Chon == true).Count() > 1)
                {
                    MessageBox.Show("Thu trực tiếp theo gói dịch vụ chỉ có thể thu mỗi cứng từ cho 1 gói dịch vụ", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    int _plthu = 0;
                    if (cboPLoaithu.SelectedIndex == 1)
                        _plthu = 3;
                    double a = 0;
                    if (!string.IsNullOrEmpty(txtTamThu.Text))
                        a = Convert.ToDouble(txtTamThu.Text);
                    //if(a>10000)
                    //    tinhtamung=true;
                    //else

                    tinhtamung = false;
                    Ktratmthu = true;
                    string noidung = txtNoiDung.Text;
                    var qkq = _lAll.Where(p => p.Chon == true).Where(p => check ? true : radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        foreach (var a1 in qkq)
                        {
                            if (a1.IDCD != 0)
                            {
                                if(a1.IDDonct == 0)
                                {
                                    ChiDinh cd = _dataContext.ChiDinhs.Single(p => p.IDCD == a1.IDCD);
                                    cd.TamThu = 1;
                                    _dataContext.SaveChanges();
                                }   
                            }
                        }
                    }
                    var idDoncts = qkq.Select(o => o.IDDonct).ToList();

                    if (chkDuoc.Checked && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                    {
                        var checkDuyetDT = (from dt in _dataContext.DThuocs
                                            join dtct in _dataContext.DThuoccts.Where(o => idDoncts.Contains(o.IDDonct)) on dt.IDDon equals dtct.IDDon
                                            select dt).Distinct().ToList();
                        if (checkDuyetDT.Exists(o => o.SoVV != -1))
                        {
                            MessageBox.Show("Bệnh nhân có đơn thuốc chưa được duyệt. Không thể thu");
                            return;
                        }
                    }

                    if (DungChung.Bien.MaBV == "30010")
                    {
                        noidung = "";
                        foreach (var item in qkq)
                        {
                            noidung += item.TenDV + "; ";
                        }
                    }
                    else if ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") && qkq.Count > 0 && check)
                    {
                        noidung = "Thu dịch vụ: " + qkq.First().TenDV;
                    }
                    #region set lại đơn giá, thành tiền, tiền bệnh nhân
                    var qdv = _dataContext.DichVus.Where(p => p.IDNhom == 13 && p.PLoai == 2).ToList();
                    if (_ktraSuaTienCongKham)
                    {
                        foreach (var dt in qkq)
                        {
                            var qdv1 = qdv.Where(p => p.MaDV == dt.MaDV).FirstOrDefault();
                            if (qdv1 != null)
                            {
                                var dtct = _dataContext.DThuoccts.Where(p => p.IDDonct == dt.IDDonct).FirstOrDefault();
                                if (dtct != null)
                                {
                                    dtct.DonGia = dt.DonGia;
                                    dtct.ThanhTien = dt.ThanhTien;
                                    dtct.TienBN = dt.TienBN;
                                    _dataContext.SaveChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    int IdGoiDV = 0;
                    if (qkq.Count > 0 && ckcCheckThuGoi.Checked == true)
                        IdGoiDV = qkq.First().MaDV;
                    // GetData(a.ToString(), noidung, "", tinhtamung, _lchidinh, _plthu, _madvCK);
                    GetData(a.ToString(), noidung, "", tinhtamung, qkq, _plthu, _lMadvCK, IdGoiDV, thuBangThe);
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể tạm thu");
            }
            // update trang thái tamthu trong bảng dịchvu
            //

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //DungChung.Ham.Update_Delete_CongKham(_mabn, -1, false, DateTime.Now);
            this.Dispose();
        }

        private void chkTienKham_CheckedChanged(object sender, EventArgs e)
        {
            frm_CPChiDinh_Load(sender, e);
        }

        private void grvThanhToan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void grvThanhToan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colDonGia":
                    if (_ktraSetThanhTien)
                    {
                        double _dongia = 0;
                        double _soluong = 0;
                        if (grvThanhToan.GetFocusedRowCellValue(colDonGia) != null && grvThanhToan.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            _dongia = Convert.ToDouble(grvThanhToan.GetFocusedRowCellValue(colDonGia));
                        if (grvThanhToan.GetFocusedRowCellValue(colSoLuong) != null && grvThanhToan.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                            _soluong = Convert.ToDouble(grvThanhToan.GetFocusedRowCellValue(colSoLuong));
                        grvThanhToan.SetFocusedRowCellValue(colThanhTien, _dongia * _soluong);
                        grvThanhToan.SetFocusedRowCellValue(colTienBN, _dongia * _soluong);
                        int madv = 0;
                        if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                        {
                            madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
                        }
                        var qkq = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
                        foreach (var b in qkq)
                        {
                            if (b.MaDV == madv)
                            {
                                b.DonGia = _dongia;
                                b.ThanhTien = _dongia * _soluong;
                                b.TienBN = _dongia * _soluong;
                                double a = 0;
                                a = qkq.Where(p => p.Chon == true).Sum(p => p.TienBN);
                                txtTamThu.Text = a.ToString("##,###");
                                break;
                            }
                        }
                    }
                    break;
                case "colchon":
                    {
                        var row = (lCPCHiDinh)grvThanhToan.GetRow(e.RowHandle);
                        if (row != null)
                        {

                            var index = grvThanhToan.ViewRowHandleToDataSourceIndex(e.RowHandle);
                            double st = 0;
                            var source = (List<lCPCHiDinh>)grcThanhToan.DataSource;
                            if (source != null && source.Count > 0)
                            {
                                source[index].Chon = row.Chon;
                                st = source.Where(p => p.Chon == true).Sum(p => p.TienBN);
                            }
                            txtTamThu.Text = st.ToString("##,###");
                        }
                    }
                    break;
                case "colMien":
                    {
                        var row = (lCPCHiDinh)grvThanhToan.GetRow(e.RowHandle);
                        
                        int _mien = row.Mien;
                        row.tienbn = row.thanhtien * (100 - _mien) / 100;
                        var qkq = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
                        double a = 0;
                        a = qkq.Where(p => p.Chon == true).Sum(p => p.TienBN);
                        txtTamThu.Text = a.ToString("##,###");
                    }
                    break;
                //if (e.Column.Name == "colchon") {
                //grvThanhToan.FocusedColumn = grvThanhToan.VisibleColumns[6];
                //grvThanhToan_CellValueChanged(null,null);

            }

        }

        private void grvThanhToan_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (!_ktraSuaTienCongKham)
                grvThanhToan.FocusedColumn = grvThanhToan.VisibleColumns[6];
        }

        private void grvThanhToan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoa")
            {
                //kiem tra thanh toan

                //kiem tra thuc hien
                int madv = 0;
                madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
                var chidinh = (from a in _dataContext.ChiDinhs
                             join b in _dataContext.CLS on a.IdCLS equals b.IdCLS
                             where b.MaBNhan == _mabn && a.MaDV == madv
                             select new { a, b }).ToList();
                if(chidinh.Count() > 0)
                {
                    if(chidinh.First().a.Status == 1)
                    {
                        MessageBox.Show("Chỉ định đã được thực hiện, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa chỉ định dịch vụ này không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                    {
                        //xoa bang ChiDinh
                        int idcd = chidinh.First().a.IDCD;
                        int idcls = chidinh.First().b.IdCLS;

                        var delChiDinh = (from cd in _dataContext.ChiDinhs
                                          where cd.IDCD == idcd
                                          select cd).ToList();
                        if (delChiDinh.Count() > 0)
                        {
                            _dataContext.ChiDinhs.Remove(delChiDinh.First());
                        }

                        //xoa bang clsct
                        var delCLSct = (from a in _dataContext.CLScts
                                        where a.IDCD == idcd
                                        select a).ToList();
                        if (delCLSct.Count() > 0)
                        {
                            _dataContext.CLScts.Remove(delCLSct.First());
                        }
                        _dataContext.SaveChanges();
                    }
                }

                frm_CPChiDinh_Load(null,null);
            }
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            double a = 0;
            var qkq = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
            foreach (var item in qkq)
            {
                item.Chon = true;

            }

            grcThanhToan.DataSource = null;
            grcThanhToan.DataSource = qkq;
            a = qkq.Where(p => p.Chon == true).Sum(p => p.TienBN);
            txtTamThu.Text = a.ToString("##,###");
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            double a = 0;
            var qkq = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
            foreach (var item in qkq)
            {
                item.Chon = false;

            }
            grcThanhToan.DataSource = null;
            grcThanhToan.DataSource = qkq;
            a = qkq.Where(p => p.Chon == true).Sum(p => p.TienBN);
            txtTamThu.Text = a.ToString("##,###");
        }

        private void radTrongNgoaiDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();

        }
        public void TimKiem()
        {
            double a = 0;
            _lAll.Clear();
            _lAll.AddRange(_lchidinh);
            if (ckDichVuKhac.Checked || chkDuoc.Checked)
            {
                _lAll.AddRange(_ldvKeThuThang);
            }
            foreach (var item in _lAll)
            {

                if (radTrongNgoaiDM.SelectedIndex == 0)
                    if (item.TrongBH == 0)
                        item.Chon = true;
                    else item.Chon = false;
                else if (radTrongNgoaiDM.SelectedIndex == 1)
                    if (item.TrongBH == 1)
                        item.Chon = true;
                    else
                        item.Chon = false;
                else
                    item.Chon = true;

            }

            if (chkDuoc.Checked)
            {
                _lAll = _lAll.Where(o => o.PLoai == 1).ToList();
            }

            grcThanhToan.DataSource = null;
            grcThanhToan.DataSource = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).ToList();
            a = _lAll.Where(p => radTrongNgoaiDM.SelectedIndex != 2 ? p.TrongBH == radTrongNgoaiDM.SelectedIndex : true).Where(p => p.Chon == true).Sum(p => p.TienBN);
            txtTamThu.Text = a.ToString("##,###");
        }

        private void txtMien_TextChanged(object sender, EventArgs e)
        {
            frm_CPChiDinh_Load(sender, e);
        }
        public void InChiDinhTH(int _Mabn)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var XQ2 = ((from canls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                        join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                        join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                        join chidinh in _data.ChiDinhs.Where(p => p.Status == 0) on canls.IdCLS equals chidinh.IdCLS
                        join dichvu in _data.DichVus on chidinh.MaDV equals dichvu.MaDV
                        join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                        join Nhom in _data.NhomDVs on tn.IDNhom equals Nhom.IDNhom
                        // group new {}
                        select new { canls.NgayThang, kp.MaKP, cb.TenCB, kp.TenKP, TenDV = dichvu.TenDV + " - " + chidinh.ChiDinh1, tn.TenTN, SoLuong = 1, chidinh.DonGia }
                    )).ToList();
            int[] MaKP = XQ2.Select(p => p.MaKP).Distinct().ToList().ToArray();
            for (int i = 0; i < MaKP.Length; i++)
            {
                var XQ = XQ2.Where(p => p.MaKP == MaKP[i]).ToList();
                frmIn frm = new frmIn();
                BaoCao.Rep_PhieuTHCD_DichVu rep = new BaoCao.Rep_PhieuTHCD_DichVu();
                rep.MaICD.Value = "Mã BN:" + _Mabn.ToString();
                int makp = MaKP[i];
                var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp) select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bnkb.MaICD, bnkb.MaICD2 }).OrderByDescending(p => p.IDKB).ToList();
                if (k.Count > 0)
                {
                    rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoan + ";" + k.First().BenhKhac);
                    //rep.MaICD.Value = "Mã ICD: " + DungChung.Ham.GetICDstr(k.First().MaICD + ";" + k.First().MaICD2);
                }
                var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                           select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe }).ToList();
                if (bn2.Count > 0)
                {

                    rep.Diachi.Value = bn2.First().DChi;
                    rep.Tuoi.Value = bn2.First().Tuoi;
                    rep.TenBN.Value = bn2.First().TenBNhan;
                    int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                    if (gioiTinh == 1)
                    {
                        rep.Nu.Value = "/";
                        rep.Nam.Value = "";
                    }
                    else
                    {
                        rep.Nu.Value = "";
                        rep.Nam.Value = "/";
                    }
                    if (bn2.First().SThe.Length == 15)
                    {
                        rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                        rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                        rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                        rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                        rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                    }
                }

                if (XQ.Count > 0)
                {
                    DateTime _dt = System.DateTime.Now;
                    if (XQ.First().NgayThang != null)
                        _dt = XQ.First().NgayThang.Value;
                    rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, DungChung.Bien.FormatDate);
                    rep.BSCD.Value = XQ.Last().TenCB;
                    rep.TenKP.Value = XQ.First().TenKP;
                    rep.DataSource = XQ;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InChiDinhTH(_mabn);
        }

        private void frm_CPChiDinh_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Ktratmthu)
                DungChung.Ham.Update_Delete_CongKham(_mabn, -1, false, DateTime.Now);
        }

        private void ckDichVuKhac_CheckedChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void ckcCheckThuGoi_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcCheckThuGoi.Checked)
            {
                int mien = 0;
                if (!string.IsNullOrEmpty(txtMien.Text))
                    mien = Convert.ToInt32(txtMien.Text);
                ckDichVuKhac.Enabled = false;
                radTrongNgoaiDM.Enabled = false;
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var _lGoiDV = _data.DmGoiDVs.Where(p => p.DSDTBN.Contains(";3;") && p.Status == 1).ToList();
                double a = 0;
                _lAll.Clear();
                foreach (var item in _lGoiDV)
                {
                    lCPCHiDinh moi = new lCPCHiDinh();
                    moi.TenDV = item.TenGoi;
                    moi.MaDV = item.IDGoi;
                    moi.SoLuong = 1;
                    moi.DonVi = "Lần";
                    moi.DonGia = item.DonGia;
                    moi.ThanhTien = item.DonGia;
                    moi.TienBN = moi.ThanhTien * (100 - mien) / 100;
                    moi.Mien = mien;
                    moi.Chon = false;
                    moi.TrongBH = 0;
                    _lAll.Add(moi);
                }
                grcThanhToan.DataSource = null;
                grcThanhToan.DataSource = _lAll;

            }
            else
            {
                ckDichVuKhac.Enabled = true;
                radTrongNgoaiDM.Enabled = true;
                TimKiem();
            }
        }

        private void txtMien_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Chỉ = true khi sửa giá công khám cho bệnh nhân KSK bệnh viện Kinh Môn, kết hợp với biến _ktraSuaTienCongKham (là điều kiện đủ)
        /// </summary>
        bool _ktraSetThanhTien = false;
        private void grvThanhToan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _ktraSetThanhTien = false;
            colDonGia.OptionsColumn.ReadOnly = true;
            colDonGia.OptionsColumn.AllowEdit = false;
            if (_ktraSuaTienCongKham)
            {
                int madv = 0;
                if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                {
                    madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
                    var qdv = _dataContext.DichVus.Where(p => p.MaDV == madv).FirstOrDefault();
                    if (qdv != null && qdv.IDNhom == 13)
                    {
                        colDonGia.OptionsColumn.ReadOnly = false;
                        colDonGia.OptionsColumn.AllowEdit = true;
                        _ktraSetThanhTien = true;
                    }
                }

            }
        }

        private void chkDuoc_CheckedChanged(object sender, EventArgs e)
        {
            frm_CPChiDinh_Load(null, null);
        }

        private void chkChon_CheckedChanged(object sender, EventArgs e)
        {
            grvThanhToan.PostEditor();
        }

        bool thuBangThe = false;

        private void ckMienGiamAll_CheckedChanged(object sender, EventArgs e)
        {
            if(lbMien.Visible==false)
            {
                lbMien.Visible = true;
                txtMien.Visible = true;
                colMien.Visible = false;
            }
            else
            {
                txtMien.Text = "0";
                frm_CPChiDinh_Load(sender, e);
                lbMien.Visible = false;
                txtMien.Visible = false;
                colMien.Visible = true;
                
            }
        }

        private void grcThanhToan_Click(object sender, EventArgs e)
        {

        }

        private void btnThuBangThe_Click(object sender, EventArgs e)
        {
            try
            {
                thuBangThe = true;
                btnOK_Click(null, null);
            }
            finally
            {
                thuBangThe = false;
            }
        }
    }
}