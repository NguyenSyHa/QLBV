using System;using QLBV_Database;
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
    public partial class frmTk15NgaySDThuoc_TT23 : DevExpress.XtraEditors.XtraForm
    {
        public frmTk15NgaySDThuoc_TT23()
        {
            InitializeComponent();
        }
        string[] _songay;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBcNXT(DateTime tungay, DateTime denngay)
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
           // if ((dateDenNgay.DateTime - dateTuNgay.DateTime).Days >15 || (dateDenNgay.DateTime - dateTuNgay.DateTime).Days <= 0) {
           
            if ((denngay-tungay).Days <0 || (denngay.AddMonths(-1) - tungay).Days >=  0)
              {
                MessageBox.Show("Khoảng cách giữa 2 ngày phải nhỏ hơn hoặc bằng 1 tháng");
                dateDenNgay.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(lupKhoa.Text)) {
            //    MessageBox.Show("Bạn chưa chọn khoa phòng");
            //    lupKhoa.Focus();
            //    return false;
            //}
            
           return true;
        }
        public class l_CTThuoc
        {
            public string tendv, donvi, tennhom, qcpc;
            int trongbh, madv, mabn;
            double soluong, dongia, thanhtien, sl0, sl1, sl2, sl3, sl4, sl5, sl6, sl7, sl8, sl9, sl10, sl11, sl12, sl13, sl14, sl15, sl16, sl17, sl18, sl19, sl20, sl21, sl22, sl23, sl24, sl25, sl26, sl27, sl28, sl29, sl30, slTong1, slTong2, thanhtienT1, thanhtienT2;

            DateTime ngay;
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public string TenNhomDV
            {
                set { tennhom = value; }
                get { return tennhom; }
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
            public string QCPC
            {
                set { qcpc = value; }
                get { return qcpc; }
            }
            public int MaBNhan
            {
                set { mabn = value; }
                get { return mabn; }
            }
            public int TrongBH
            {
                set { trongbh = value; }
                get { return trongbh; }
            }
            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
            public double ThanhTien
            {
                set { thanhtien = value; }
                get { return thanhtien; }
            }
            public DateTime NgayKe
            {
                set { ngay = value; }
                get { return ngay; }
            }
             public double SL0
            {
                set { sl0 = value; }
                get { return sl0; }
            }
            public double SL1
            {
                set { sl1 = value; }
                get { return sl1; }
            }
             public double SL2
            {
                set { sl2 = value; }
                get { return sl2; }
            }

             public double SL3
            {
                set { sl3 = value; }
                get { return sl3; }
            }

             public double SL4
            {
                set { sl4 = value; }
                get { return sl4; }
            }

             public double SL5
            {
                set { sl5 = value; }
                get { return sl5; }
            }

             public double SL6
            {
                set { sl6 = value; }
                get { return sl6; }
            }
              public double SL7
            {
                set { sl7 = value; }
                get { return sl7; }
            }

             public double SL8
            {
                set { sl8 = value; }
                get { return sl8; }
            }

             public double SL9
            {
                set { sl9 = value; }
                get { return sl9; }
            }

             public double SL10
            {
                set { sl10 = value; }
                get { return sl10; }
            }

             public double SL11
            {
                set { sl11 = value; }
                get { return sl11; }
            }
              public double SL12
            {
                set { sl12 = value; }
                get { return sl12; }
            }

             public double SL13
            {
                set { sl13 = value; }
                get { return sl13; }
            }

             public double SL14
            {
                set { sl14 = value; }
                get { return sl14; }
            }

             public double SL15
            {
                set { sl15 = value; }
                get { return sl15; }
            }

             public double SL16
            {
                set { sl16 = value; }
                get { return sl16; }
            }
              public double SL17
            {
                set { sl17 = value; }
                get { return sl17; }
            }

             public double SL18
            {
                set { sl18 = value; }
                get { return sl18; }
            }

             public double SL19
            {
                set { sl19 = value; }
                get { return sl19; }
            }

             public double SL20
            {
                set { sl20 = value; }
                get { return sl20; }
            }

             public double SL21
            {
                set { sl21 = value; }
                get { return sl21; }
            }
              public double SL22
            {
                set { sl22 = value; }
                get { return sl22; }
            }

             public double SL23
            {
                set { sl23 = value; }
                get { return sl23; }
            }

             public double SL24
            {
                set { sl24 = value; }
                get { return sl24; }
            }

             public double SL25
            {
                set { sl25 = value; }
                get { return sl25; }
            }

             public double SL26
            {
                set { sl26 = value; }
                get { return sl26; }
            }
              public double SL27
            {
                set { sl27 = value; }
                get { return sl27; }
            }

             public double SL28
            {
                set { sl28 = value; }
                get { return sl28; }
            }

             public double SL29
            {
                set { sl29 = value; }
                get { return sl29; }
            }

            public double SL30
            {
                set { sl30 = value; }
                get { return sl30; }
            }

            public double SLTong1
            {
                set { slTong1 = value; }
                get { return slTong1; }
            }
            public double SLTong2
            {
                set { slTong2 = value; }
                get { return slTong2; }
            }
            public double ThanhTienTong1
            {
                set { thanhtienT1 = value; }
                get { return thanhtienT1; }
            }
           
            

        }
        class Tong {
            DThuoc dthuoc;

            public DThuoc Dthuoc
            {
                get { return dthuoc; }
                set { dthuoc = value; }
            }
         
            int trongBH;

            public int TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            double soLuong;

            public double SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            double thanhTien;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
        }
        private void fn_InBC(List<int> _listMaKho, List<int> _lMaKhoa, DateTime tungay, DateTime denngay, int _IDDTBN, List<int> _listIDNhomDV, List<int> _listTrongNgoaiDM)
       // private void fn_InBC(int _Makho, int _MaKhoa, DateTime tungay, DateTime denngay,
        {
            List<l_CTThuoc> _ldthuocct = new List<l_CTThuoc>();
            DateTime ngayvao = System.DateTime.Now.Date;
            DateTime ngayra = System.DateTime.Now.Date;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            List<Tong> qdt = new List<Tong>();
            var dichvu = (from dv in data.DichVus
                          join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                          join nhom in data.NhomDVs on tnhom.IDNhom equals nhom.IDNhom
                          select new {dv.TenDV,dv.QCPC, dv.MaDV, nhom.IDNhom, nhom.TenNhom, dv.DonVi }).ToList();
            if (ckLinhVeKhoa.Checked)
            {
                var _ldthuoc = (from dt in data.DThuocs.Where(p => p.PLDV == 1)
                                join dtct in data.DThuoccts.Where(p => rdTrangThai.SelectedIndex == 2 || p.Status == rdTrangThai.SelectedIndex) on dt.IDDon equals dtct.IDDon
                                where (dt.NgayKe >= tungay && dt.NgayKe <= denngay)
                                select new { Dthuoc = dt, DonGia = dtct.DonGia, TrongBH = dtct.TrongBH, SoLuong = dtct.SoLuong, MaDV = dtct.MaDV ?? 0, ThanhTien = dtct.ThanhTien,dt.MaKP }).ToList();
                qdt = (from dt in _ldthuoc
                       join kp in _lMaKhoa on dt.MaKP equals kp
                       select new Tong { Dthuoc = dt.Dthuoc, DonGia = dt.DonGia, TrongBH = dt.TrongBH, SoLuong = dt.SoLuong, MaDV = dt.MaDV, ThanhTien = dt.ThanhTien }).ToList();
            }
            else
                qdt = (from dt in data.DThuocs.Where(p => p.PLDV == 1)
                       //join k in _listMaKho on dt.MaKXuat equals k
                       join dtct in data.DThuoccts.Where(p => rdTrangThai.SelectedIndex == 2 || p.Status == rdTrangThai.SelectedIndex) on dt.IDDon equals dtct.IDDon
                       join bn in data.BenhNhans.Where(p => _IDDTBN == 100 || p.IDDTBN == _IDDTBN) on dt.MaBNhan equals bn.MaBNhan
                       where (dt.NgayKe >= tungay && dt.NgayKe <= denngay)
                       select new Tong { Dthuoc = dt, DonGia = dtct.DonGia, TrongBH = dtct.TrongBH, MaDV = dtct.MaDV ?? 0, SoLuong = dtct.SoLuong, ThanhTien = dtct.ThanhTien }).ToList();

            var dt_rep = (from dthc in qdt
                          join k in _listMaKho on dthc.Dthuoc.MaKXuat equals k
                          join dv in dichvu on dthc.MaDV equals dv.MaDV
                          join trongDM in _listTrongNgoaiDM on dthc.TrongBH equals trongDM
                          group new { dthc } by new { dthc.Dthuoc.NgayKe, dv.DonVi, dthc.MaDV } into kq
                          select new { kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dthc.SoLuong), ThanhTien = kq.Sum(p => p.dthc.ThanhTien) }).Where(p => p.SoLuong != 0).ToList();

                string[] _songay;
                var ngaykd = (from nk in dt_rep select nk.NgayKe).ToList().Select(x => new { x.Value.Date }).Distinct().OrderBy(p => p.Date).ToList();
                int i = 0;
                _songay = new string[ngaykd.Count];
                if (_songay.Length == 0){
                    MessageBox.Show("Không có dữ liệu");
                    return;
                }
                
                    foreach (var a in ngaykd)
                    {
                        _songay[i] = a.Date.ToShortDateString();
                        i++;
                    }

                    for (int j = 0; j < _songay.Length; j += 16)
                    {
                        bool _InCongKhoan = false;
                        if (j > 0)
                            _InCongKhoan = true;
                        int m = 0;
                        string[] _songayT1 = new string[16];

                        for (int l = 0; l < 1; l++)
                        {
                            _songayT1[l] = "";
                        }
                        for (int k = j; k < _songay.Length; k++)
                        {
                            if (m < 16)
                            {
                                _songayT1[m] = _songay[k];
                                m++;
                            }
                            else
                            {
                                break;
                            }
                        }
                      

                        var khoa = (from kp in data.KPhongs 
                                    join a in _lMaKhoa on kp.MaKP equals a
                                    select new { kp.TenKP }).ToList();
                       

                        double thuoc = 0;
                        string[] _31ngay = new string[31];
                        for (int num = 0; num < 31; num++)
                        {
                            if (num < ngaykd.Count)
                                _31ngay[num] = _songay[num];
                            else
                                _31ngay[num] = "";
                        }

               
                        var q1 = (from dt in qdt
                                  join dv in dichvu on dt.MaDV equals dv.MaDV
                                  join k in _listMaKho on dt.Dthuoc.MaKXuat equals k
                                  where _listIDNhomDV.Contains(dv.IDNhom)
                                  where _listTrongNgoaiDM.Contains(dt.TrongBH)
                                  select new { dv.QCPC, dv.TenNhom, dt.MaDV, dv.TenDV, dv.DonVi, dt.DonGia, dt.Dthuoc.NgayKe, dt.SoLuong, dt.ThanhTien }
                               ).Where(p => p.SoLuong != 0).ToList();
                        var q = (from a in q1

                                 group a by new { a.TenNhom, a.MaDV, a.TenDV, a.DonVi, a.NgayKe.Value.Date, a.QCPC } into kq
                                 select new
                                 {
                                     QCPC = kq.Key.QCPC,
                                     MaDV = kq.Key.MaDV,
                                     TenNhomDV = kq.Key.TenNhom.ToUpper(),
                                     TenDV = kq.Key.TenDV,
                                     DonVi = kq.Key.DonVi,
                                     SoLuong = kq.Sum(p => p.SoLuong),
                                     NgayKe = kq.Key.Date,
                                     ThanhTien=kq.Sum(p=>p.ThanhTien)
                                 }).ToList();
                        _ldthuocct = new List<l_CTThuoc>();
                        List<l_CTThuoc> _ldthuocct2 = new List<l_CTThuoc>();
                        foreach (var a in q)
                        {
                            l_CTThuoc moi = new l_CTThuoc();
                            moi.TenDV = a.TenDV;
                            moi.MaDV = a.MaDV;
                            moi.TenNhomDV = a.TenNhomDV;
                            moi.DonVi = a.DonVi;
                            moi.SoLuong = a.SoLuong;
                            moi.QCPC = a.QCPC;
                            moi.ThanhTien = a.ThanhTien;
                            moi.SL0 = a.NgayKe.ToShortDateString() == _31ngay[0] ? a.SoLuong : 0;
                            moi.SL1 = a.NgayKe.ToShortDateString() == _31ngay[1] ? a.SoLuong : 0;
                            moi.SL2 = a.NgayKe.ToShortDateString() == _31ngay[2] ? a.SoLuong : 0;
                            moi.SL3 = a.NgayKe.ToShortDateString() == _31ngay[3] ? a.SoLuong : 0;
                            moi.SL4 = a.NgayKe.ToShortDateString() == _31ngay[4] ? a.SoLuong : 0;
                            moi.SL5 = a.NgayKe.ToShortDateString() == _31ngay[5] ? a.SoLuong : 0;
                            moi.SL6 = a.NgayKe.ToShortDateString() == _31ngay[6] ? a.SoLuong : 0;
                            moi.SL7 = a.NgayKe.ToShortDateString() == _31ngay[7] ? a.SoLuong : 0;
                            moi.SL8 = a.NgayKe.ToShortDateString() == _31ngay[8] ? a.SoLuong : 0;
                            moi.SL9 = a.NgayKe.ToShortDateString() == _31ngay[9] ? a.SoLuong : 0;
                            moi.SL10 = a.NgayKe.ToShortDateString() == _31ngay[10] ? a.SoLuong : 0;
                            moi.SL11 = a.NgayKe.ToShortDateString() == _31ngay[11] ? a.SoLuong : 0;
                            moi.SL12 = a.NgayKe.ToShortDateString() == _31ngay[12] ? a.SoLuong : 0;
                            moi.SL13 = a.NgayKe.ToShortDateString() == _31ngay[13] ? a.SoLuong : 0;
                            moi.SL14 = a.NgayKe.ToShortDateString() == _31ngay[14] ? a.SoLuong : 0;
                            moi.SL15 = a.NgayKe.ToShortDateString() == _31ngay[15] ? a.SoLuong : 0;
                            moi.SL16 = a.NgayKe.ToShortDateString() == _31ngay[16] ? a.SoLuong : 0;
                            moi.SL17 = a.NgayKe.ToShortDateString() == _31ngay[17] ? a.SoLuong : 0;
                            moi.SL18 = a.NgayKe.ToShortDateString() == _31ngay[18] ? a.SoLuong : 0;
                            moi.SL19 = a.NgayKe.ToShortDateString() == _31ngay[19] ? a.SoLuong : 0;
                            moi.SL20 = a.NgayKe.ToShortDateString() == _31ngay[20] ? a.SoLuong : 0;
                            moi.SL21 = a.NgayKe.ToShortDateString() == _31ngay[21] ? a.SoLuong : 0;
                            moi.SL22 = a.NgayKe.ToShortDateString() == _31ngay[22] ? a.SoLuong : 0;
                            moi.SL23 = a.NgayKe.ToShortDateString() == _31ngay[23] ? a.SoLuong : 0;
                            moi.SL24 = a.NgayKe.ToShortDateString() == _31ngay[24] ? a.SoLuong : 0;
                            moi.SL25 = a.NgayKe.ToShortDateString() == _31ngay[25] ? a.SoLuong : 0;
                            moi.SL26 = a.NgayKe.ToShortDateString() == _31ngay[26] ? a.SoLuong : 0;
                            moi.SL27 = a.NgayKe.ToShortDateString() == _31ngay[27] ? a.SoLuong : 0;
                            moi.SL28 = a.NgayKe.ToShortDateString() == _31ngay[28] ? a.SoLuong : 0;
                            moi.SL29 = a.NgayKe.ToShortDateString() == _31ngay[29] ? a.SoLuong : 0;
                            moi.SL30 = a.NgayKe.ToShortDateString() == _31ngay[30] ? a.SoLuong : 0;

                            _ldthuocct.Add(moi);
                        }
                        _ldthuocct2 = (from a in _ldthuocct
                                       group a by new { a.TenNhomDV, a.MaDV, a.TenDV, a.DonVi, a.QCPC } into kq

                                       select new l_CTThuoc
                                           {
                                               MaDV = kq.Key.MaDV,
                                               TenNhomDV = kq.Key.TenNhomDV.ToUpper(),
                                               TenDV = kq.Key.TenDV,
                                               DonVi = kq.Key.DonVi,
                                               QCPC = kq.Key.QCPC,
                                               SoLuong = kq.Sum(p => p.SoLuong),
                                               ThanhTien = kq.Sum(p => p.ThanhTien),
                                               SL0 = kq.Sum(p => p.SL0),
                                               SL1 = kq.Sum(p => p.SL1),
                                               SL2 = kq.Sum(p => p.SL2),
                                               SL3 = kq.Sum(p => p.SL3),
                                               SL4 = kq.Sum(p => p.SL4),
                                               SL5 = kq.Sum(p => p.SL5),
                                               SL6 = kq.Sum(p => p.SL6),
                                               SL7 = kq.Sum(p => p.SL7),
                                               SL8 = kq.Sum(p => p.SL8),
                                               SL9 = kq.Sum(p => p.SL9),
                                               SL10 = kq.Sum(p => p.SL10),
                                               SL11 = kq.Sum(p => p.SL11),
                                               SL12 = kq.Sum(p => p.SL12),
                                               SL13 = kq.Sum(p => p.SL13),
                                               SL14 = kq.Sum(p => p.SL14),
                                               SL15 = kq.Sum(p => p.SL15),
                                               SL16 = kq.Sum(p => p.SL16),
                                               SL17 = kq.Sum(p => p.SL17),
                                               SL18 = kq.Sum(p => p.SL18),
                                               SL19 = kq.Sum(p => p.SL19),
                                               SL20 = kq.Sum(p => p.SL20),
                                               SL21 = kq.Sum(p => p.SL21),
                                               SL22 = kq.Sum(p => p.SL22),
                                               SL23 = kq.Sum(p => p.SL23),
                                               SL24 = kq.Sum(p => p.SL24),
                                               SL25 = kq.Sum(p => p.SL25),
                                               SL26 = kq.Sum(p => p.SL26),
                                               SL27 = kq.Sum(p => p.SL27),
                                               SL28 = kq.Sum(p => p.SL28),
                                               SL29 = kq.Sum(p => p.SL29),
                                               SL30 = kq.Sum(p => p.SL30),
                                               // SLTong1 = _InCongKhoan ? kq.Sum(p=> (p.SL0 + p.SL1 + p.SL2+ p.SL3 + p.SL4 +  p.SL5+ p.SL6 + p.SL7 + p.SL8 + p.SL9 + p.SL10 + p.SL11 + p.SL12 + p.SL13 + p.SL14 + p.SL15)): kq.Sum(p=>p.SoLuong),
                                           }).Where(p => p.SoLuong > 0).OrderBy(p => p.TenNhomDV).ThenBy(p => p.TenDV).ToList();

                        //if (q.Count > 0)
                        //    thuoc = q.Sum(p => p.ThanhTien);
                        //rep.DataSource = q.ToList();
                        if (_ldthuocct2.Count > 0)
                        {
                            if (ckcHienThiTT.Checked)
                            {
                                BaoCao.rep_Tk15NgaySDThuoc_TT23_HTTT rep = new BaoCao.rep_Tk15NgaySDThuoc_TT23_HTTT(_songayT1, _InCongKhoan);
                                if (khoa.Count > 0 && khoa.Count < 2)
                                    rep.Khoa.Value = khoa.First().TenKP.ToUpper();

                                rep.TieuDe.Value = "THỐNG KÊ 15 NGÀY SỬ DỤNG THUỐC, HÓA CHẤT, VẬT TƯ Y TẾ TIÊU HAO";//????????????????????? +LIST NHÓM DỊCH VỤ
                                rep.DataSource = _ldthuocct2;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.rep_Tk15NgaySDThuoc_TT23 rep = new BaoCao.rep_Tk15NgaySDThuoc_TT23(_songayT1, _InCongKhoan);
                                if (khoa.Count > 0 && khoa.Count < 2)
                                    rep.Khoa.Value = khoa.First().TenKP.ToUpper();

                                rep.TieuDe.Value = "THỐNG KÊ 15 NGÀY SỬ DỤNG THUỐC, HÓA CHẤT, VẬT TƯ Y TẾ TIÊU HAO";//????????????????????? +LIST NHÓM DỊCH VỤ
                                rep.DataSource = _ldthuocct2;
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else
                            MessageBox.Show("Không có dữ liệu");

                    }
                
           
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            #region BC mới (Làm tương tự phiếu công khai thuốc)
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            //_listMaKPhong
            //_lkpc.Clear();
            if (KTtaoBcNXT(dateTuNgay.DateTime, dateDenNgay.DateTime))
            {
                int _makhoa = 0, _IDDTBN = 100;
                _listNhomDV = new List<NhomDV>();
                List<int> _listMaKho = new List<int>();
                List<int> _listMaKPhong = new List<int>();
                _listMaKPhong.Clear();
                _listMaKho.Clear();
                List<int> _listTrongNgoaiDM = new List<int>();
                _listIDNhomDV = new List<int>();
                //for (int i = 0; i < cklKho.ItemCount; i++)
                //{
                //    if (cklKho.GetItemCheckState(i) == CheckState.Checked)
                //        _listMaKho.Add(Convert.ToInt32(cklKho.GetItemValue(i)));
                //}
                for (int i = 0; i < cklKho.ItemCount; i++)
                {
                    if (cklKho.GetItemChecked(i))
                    {
                        int makp = Convert.ToInt32(cklKho.GetItemValue(i));
                        foreach (var item in _lkhoc)
                        {
                            if (item.MakP == makp && item.MakP != 0)
                            {
                                item.CHon = true;
                                break;
                            }
                        }
                    }
                }
                _listMaKho = _lkhoc.Where(p => p.CHon == true).Select(p => p.MakP).ToList();
                for (int i = 0; i < ckckphong.ItemCount; i++)
                {
                    if (ckckphong.GetItemChecked(i))
                    {
                        int makp = Convert.ToInt32(ckckphong.GetItemValue(i));
                        foreach (var item in _lkpc)
                        {
                            if (item.MakP == makp && item.MakP != 0)
                            {
                                item.CHon = true;
                                break;
                            }
                        }
                    }
                }
                _listMaKPhong = _lkpc.Where(p => p.CHon == true).Select(p => p.MakP).ToList();
                if (lupDtuong.EditValue != null)
                    _IDDTBN = Convert.ToInt32(lupDtuong.EditValue);


                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                        _listIDNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
                }

                for (int i = 0; i < cklTrongNgoaiDM.ItemCount; i++)
                {
                   if(cklTrongNgoaiDM.GetItemCheckState(i) == CheckState.Checked)
                        _listTrongNgoaiDM.Add(i);
                }
                
                //if (lupKho.EditValue != null)
                //    _makho = Convert.ToInt32(lupKho.EditValue);
                //if (lupKhoa.EditValue != null)
                //    _makhoa = Convert.ToInt32(lupKhoa.EditValue);
                if (rdChonLoaiBC.SelectedIndex == 1)
                {
                    fn_InBC30Ngay(_listMaKho, _listMaKPhong, tungay, denngay, _IDDTBN, _listIDNhomDV, _listTrongNgoaiDM);                   
                }
                else
                    fn_InBC(_listMaKho, _listMaKPhong, tungay, denngay, _IDDTBN, _listIDNhomDV, _listTrongNgoaiDM);
            }
            #endregion
       
        }


        private void fn_InBC30Ngay(List<int> _listMaKho, List<int> _lMaKhoa, DateTime tungay, DateTime denngay, int _IDDTBN, List<int> _listIDNhomDV, List<int> _listTrongNgoaiDM)
        // private void fn_InBC(int _Makho, int _MaKhoa, DateTime tungay, DateTime denngay,
        {
            List<l_CTThuoc> _ldthuocct = new List<l_CTThuoc>();
            DateTime ngayvao = System.DateTime.Now.Date;
            DateTime ngayra = System.DateTime.Now.Date;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            List<Tong> qdt = new List<Tong>();
            List<NhomDV> _lnhom = data.NhomDVs.ToList();
            List<DichVu> _lDV = data.DichVus.Where(p => p.PLoai == 1).ToList();
            List<TieuNhomDV> _lTieuNhom = data.TieuNhomDVs.ToList();

            if (ckLinhVeKhoa.Checked)
            {
                var _dthuoc = (from dt in data.DThuocs.Where(p => p.PLDV == 1)
                               join dtct in data.DThuoccts.Where(p => rdTrangThai.SelectedIndex == 2 || p.Status == rdTrangThai.SelectedIndex) on dt.IDDon equals dtct.IDDon
                               where (dt.NgayKe >= tungay && dt.NgayKe <= denngay)
                               select new { Dthuoc = dt, DonGia = dtct.DonGia, TrongBH = dtct.TrongBH, MaDV = dtct.MaDV ?? 0, SoLuong = dtct.SoLuong, ThanhTien = dtct.ThanhTien, dt.MaKP }).ToList();
                qdt = (from dt in _dthuoc
                       join kp in _lMaKhoa on dt.MaKP equals kp
                       select new Tong { Dthuoc = dt.Dthuoc, DonGia = dt.DonGia, TrongBH = dt.TrongBH, MaDV = dt.MaDV, SoLuong = dt.SoLuong, ThanhTien = dt.ThanhTien }).ToList();
            }
            else
                qdt = (from dt in data.DThuocs.Where(p => p.PLDV == 1)
                       //join k in _listMaKho on dt.MaKXuat equals k
                       join dtct in data.DThuoccts.Where(p => rdTrangThai.SelectedIndex == 2 || p.Status == rdTrangThai.SelectedIndex) on dt.IDDon equals dtct.IDDon
                       join bn in data.BenhNhans.Where(p => _IDDTBN == 100 || p.IDDTBN == _IDDTBN) on dt.MaBNhan equals bn.MaBNhan
                       where (dt.NgayKe >= tungay && dt.NgayKe <= denngay)
                       select new Tong { Dthuoc = dt, DonGia = dtct.DonGia, TrongBH = dtct.TrongBH, MaDV = dtct.MaDV ?? 0, SoLuong = dtct.SoLuong, ThanhTien = dtct.ThanhTien }).ToList();

               var dt_rep = (from dthc in qdt
                          join k in _listMaKho on dthc.Dthuoc.MaKXuat equals k 
                          join dv in _lDV on dthc.MaDV equals dv.MaDV
                          join tnhom in _lTieuNhom on dv.IdTieuNhom equals tnhom.IdTieuNhom
                          join nhom in _lnhom on tnhom.IDNhom equals nhom.IDNhom
                             join dsnhom in _listIDNhomDV on nhom.IDNhom equals dsnhom //(dv.IDNhom == _idnhomthuoc || dv.IDNhom == _idnhomVT)
                             join trongDM in _listTrongNgoaiDM on dthc.TrongBH equals trongDM
                          group new { dthc } by new { dthc.Dthuoc.NgayKe, dv.DonVi, dthc.MaDV } into kq
                          select new { kq.Key.DonVi, kq.Key.NgayKe, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dthc.SoLuong), ThanhTien = kq.Sum(p => p.dthc.ThanhTien) }).Where(p => p.SoLuong != 0).ToList();


            string[] _songay;
            var ngaykd = (from nk in dt_rep select nk.NgayKe).ToList().Select(x => new { x.Value.Date }).Distinct().OrderBy(p => p.Date).ToList();
            int i = 0;
            _songay = new string[ngaykd.Count];
            if (_songay.Length == 0)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }

            foreach (var a in ngaykd)
            {
                _songay[i] = a.Date.ToShortDateString();
                i++;
            }
            var khoa = (from kp in data.KPhongs
                        join a in _lMaKhoa on kp.MaKP equals a
                        select new { kp.TenKP }).ToList();
            double thuoc = 0;
            string[] _31ngay = new string[31];
            for (int num = 0; num < 31; num++)
            {
                if (num < ngaykd.Count)
                    _31ngay[num] = _songay[num];
                else
                    _31ngay[num] = "";
            }


            var q1 = (from dt in qdt
                      join dv in _lDV on dt.MaDV equals dv.MaDV
                      join tnhom in _lTieuNhom on dv.IdTieuNhom equals tnhom.IdTieuNhom
                      join nhom in _lnhom on tnhom.IDNhom equals nhom.IDNhom
                      join k in _listMaKho on dt.Dthuoc.MaKXuat equals k
                      where _listIDNhomDV.Contains(nhom.IDNhom)
                      where _listTrongNgoaiDM.Contains(dt.TrongBH)
                      select new { dv.QCPC, nhom.TenNhom, dt.MaDV, dv.TenDV, dv.DonVi, dt.DonGia, dt.Dthuoc.NgayKe, dt.SoLuong, dt.ThanhTien }
                            ).Where(p => p.SoLuong != 0).ToList();
            var q = (from a in q1
                     group a by new { a.TenNhom, a.MaDV, a.TenDV, a.DonVi, a.NgayKe.Value.Date, a.QCPC } into kq
                     select new
                     {
                         QCPC = kq.Key.QCPC,
                         MaDV = kq.Key.MaDV,
                         TenNhomDV = kq.Key.TenNhom.ToUpper(),
                         TenDV = kq.Key.TenDV,
                         DonVi = kq.Key.DonVi,
                         SoLuong = kq.Sum(p => p.SoLuong),
                         NgayKe = kq.Key.Date,
                         ThanhTien = kq.Sum(p => p.ThanhTien)
                     }).ToList();
            _ldthuocct = new List<l_CTThuoc>();
            List<l_CTThuoc> _ldthuocct2 = new List<l_CTThuoc>();
            foreach (var a in q)
            {
                l_CTThuoc moi = new l_CTThuoc();
                moi.TenDV = a.TenDV;
                moi.MaDV = a.MaDV;
                moi.TenNhomDV = a.TenNhomDV;
                moi.DonVi = a.DonVi;
                moi.SoLuong = a.SoLuong;
                moi.ThanhTien = a.ThanhTien;
                moi.QCPC = a.QCPC;
                moi.SL0 = a.NgayKe.ToShortDateString() == _31ngay[0] ? a.SoLuong : 0;
                moi.SL1 = a.NgayKe.ToShortDateString() == _31ngay[1] ? a.SoLuong : 0;
                moi.SL2 = a.NgayKe.ToShortDateString() == _31ngay[2] ? a.SoLuong : 0;
                moi.SL3 = a.NgayKe.ToShortDateString() == _31ngay[3] ? a.SoLuong : 0;
                moi.SL4 = a.NgayKe.ToShortDateString() == _31ngay[4] ? a.SoLuong : 0;
                moi.SL5 = a.NgayKe.ToShortDateString() == _31ngay[5] ? a.SoLuong : 0;
                moi.SL6 = a.NgayKe.ToShortDateString() == _31ngay[6] ? a.SoLuong : 0;
                moi.SL7 = a.NgayKe.ToShortDateString() == _31ngay[7] ? a.SoLuong : 0;
                moi.SL8 = a.NgayKe.ToShortDateString() == _31ngay[8] ? a.SoLuong : 0;
                moi.SL9 = a.NgayKe.ToShortDateString() == _31ngay[9] ? a.SoLuong : 0;
                moi.SL10 = a.NgayKe.ToShortDateString() == _31ngay[10] ? a.SoLuong : 0;
                moi.SL11 = a.NgayKe.ToShortDateString() == _31ngay[11] ? a.SoLuong : 0;
                moi.SL12 = a.NgayKe.ToShortDateString() == _31ngay[12] ? a.SoLuong : 0;
                moi.SL13 = a.NgayKe.ToShortDateString() == _31ngay[13] ? a.SoLuong : 0;
                moi.SL14 = a.NgayKe.ToShortDateString() == _31ngay[14] ? a.SoLuong : 0;
                moi.SL15 = a.NgayKe.ToShortDateString() == _31ngay[15] ? a.SoLuong : 0;
                moi.SL16 = a.NgayKe.ToShortDateString() == _31ngay[16] ? a.SoLuong : 0;
                moi.SL17 = a.NgayKe.ToShortDateString() == _31ngay[17] ? a.SoLuong : 0;
                moi.SL18 = a.NgayKe.ToShortDateString() == _31ngay[18] ? a.SoLuong : 0;
                moi.SL19 = a.NgayKe.ToShortDateString() == _31ngay[19] ? a.SoLuong : 0;
                moi.SL20 = a.NgayKe.ToShortDateString() == _31ngay[20] ? a.SoLuong : 0;
                moi.SL21 = a.NgayKe.ToShortDateString() == _31ngay[21] ? a.SoLuong : 0;
                moi.SL22 = a.NgayKe.ToShortDateString() == _31ngay[22] ? a.SoLuong : 0;
                moi.SL23 = a.NgayKe.ToShortDateString() == _31ngay[23] ? a.SoLuong : 0;
                moi.SL24 = a.NgayKe.ToShortDateString() == _31ngay[24] ? a.SoLuong : 0;
                moi.SL25 = a.NgayKe.ToShortDateString() == _31ngay[25] ? a.SoLuong : 0;
                moi.SL26 = a.NgayKe.ToShortDateString() == _31ngay[26] ? a.SoLuong : 0;
                moi.SL27 = a.NgayKe.ToShortDateString() == _31ngay[27] ? a.SoLuong : 0;
                moi.SL28 = a.NgayKe.ToShortDateString() == _31ngay[28] ? a.SoLuong : 0;
                moi.SL29 = a.NgayKe.ToShortDateString() == _31ngay[29] ? a.SoLuong : 0;
                moi.SL30 = a.NgayKe.ToShortDateString() == _31ngay[30] ? a.SoLuong : 0;

                _ldthuocct.Add(moi);
            }
            _ldthuocct2 = (from a in _ldthuocct
                           group a by new { a.TenNhomDV, a.MaDV, a.TenDV, a.DonVi, a.QCPC } into kq

                           select new l_CTThuoc
                           {
                               MaDV = kq.Key.MaDV,
                               TenNhomDV = kq.Key.TenNhomDV.ToUpper(),
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               QCPC = kq.Key.QCPC,
                               SoLuong = kq.Sum(p => p.SoLuong),
                               ThanhTien = kq.Sum(p => p.ThanhTien),
                               SL0 = kq.Sum(p => p.SL0),
                               SL1 = kq.Sum(p => p.SL1),
                               SL2 = kq.Sum(p => p.SL2),
                               SL3 = kq.Sum(p => p.SL3),
                               SL4 = kq.Sum(p => p.SL4),
                               SL5 = kq.Sum(p => p.SL5),
                               SL6 = kq.Sum(p => p.SL6),
                               SL7 = kq.Sum(p => p.SL7),
                               SL8 = kq.Sum(p => p.SL8),
                               SL9 = kq.Sum(p => p.SL9),
                               SL10 = kq.Sum(p => p.SL10),
                               SL11 = kq.Sum(p => p.SL11),
                               SL12 = kq.Sum(p => p.SL12),
                               SL13 = kq.Sum(p => p.SL13),
                               SL14 = kq.Sum(p => p.SL14),
                               SL15 = kq.Sum(p => p.SL15),
                               SL16 = kq.Sum(p => p.SL16),
                               SL17 = kq.Sum(p => p.SL17),
                               SL18 = kq.Sum(p => p.SL18),
                               SL19 = kq.Sum(p => p.SL19),
                               SL20 = kq.Sum(p => p.SL20),
                               SL21 = kq.Sum(p => p.SL21),
                               SL22 = kq.Sum(p => p.SL22),
                               SL23 = kq.Sum(p => p.SL23),
                               SL24 = kq.Sum(p => p.SL24),
                               SL25 = kq.Sum(p => p.SL25),
                               SL26 = kq.Sum(p => p.SL26),
                               SL27 = kq.Sum(p => p.SL27),
                               SL28 = kq.Sum(p => p.SL28),
                               SL29 = kq.Sum(p => p.SL29),
                               SL30 = kq.Sum(p => p.SL30),
                               // SLTong1 = _InCongKhoan ? kq.Sum(p=> (p.SL0 + p.SL1 + p.SL2+ p.SL3 + p.SL4 +  p.SL5+ p.SL6 + p.SL7 + p.SL8 + p.SL9 + p.SL10 + p.SL11 + p.SL12 + p.SL13 + p.SL14 + p.SL15)): kq.Sum(p=>p.SoLuong),
                           }).Where(p => p.SoLuong > 0).OrderBy(p => p.TenNhomDV).ThenBy(p => p.TenDV).ToList();

            //if (q.Count > 0)
            //    thuoc = q.Sum(p => p.ThanhTien);
            //rep.DataSource = q.ToList();
            if (_ldthuocct2.Count > 0)
            {
                if (ckcHienThiTT.Checked)
                {
                    BaoCao.rep_Tk30NgaySDThuoc_TT23_HTTT rep = new BaoCao.rep_Tk30NgaySDThuoc_TT23_HTTT(_songay);
                    if (khoa.Count > 0 && khoa.Count < 2)
                        rep.Khoa.Value = khoa.First().TenKP.ToUpper();

                    rep.TieuDe.Value = "THỐNG KÊ 30 NGÀY SỬ DỤNG THUỐC, HÓA CHẤT, VẬT TƯ Y TẾ TIÊU HAO";//????????????????????? +LIST NHÓM DỊCH VỤ
                    rep.DataSource = _ldthuocct2;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.rep_Tk30NgaySDThuoc_TT23 rep = new BaoCao.rep_Tk30NgaySDThuoc_TT23(_songay);
                    if (khoa.Count > 0 && khoa.Count < 2)
                        rep.Khoa.Value = khoa.First().TenKP.ToUpper();

                    rep.TieuDe.Value = "THỐNG KÊ 30 NGÀY SỬ DỤNG THUỐC, HÓA CHẤT, VẬT TƯ Y TẾ TIÊU HAO";//????????????????????? +LIST NHÓM DỊCH VỤ
                    rep.DataSource = _ldthuocct2;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("Không có dữ liệu");



        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class c_kphong
        {
            public string TenKP { get; set; }
            public int MakP { get; set; }
            public bool CHon { get; set; }
        }
        bool load = false;
        List<NhomDV> _listNhomDV = new List<NhomDV>();
        List<int> _listIDNhomDV = new List<int>();
        List<c_kphong> _lkpc = new List<c_kphong>();
        List<c_kphong> _lkhoc = new List<c_kphong>();
        private void frmTk15NgaySDThuoc_NB01_Load(object sender, EventArgs e)
        {
            //List<c_kphong> _lkpc=new List<c_kphong>();
            _lkpc.Clear(); _lkhoc.Clear();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs select new { TK.TenKP, TK.MaKP,TK.PLoai };
            //lupKhoa.Properties.DataSource = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang ).OrderBy(p=>p.PLoai).ThenBy(p=>p.TenKP).ToList();
            //ckckphong.ValueMember = "MakP";
            //ckckphong.DisplayMember = "TenKP";
            var _lkp = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).OrderBy(p => p.TenKP).ToList();
            if (_lkp.Count > 0)
            {
                c_kphong moi1 = new c_kphong();
                moi1.TenKP = "Chọn tất cả";
                moi1.MakP = 0;
                moi1.CHon = true;
                _lkpc.Add(moi1);
                foreach (var item in _lkp)
                {
                    c_kphong moi = new c_kphong();
                    moi.TenKP = item.TenKP;
                    moi.MakP = item.MaKP;
                    moi.CHon = true;
                    _lkpc.Add(moi);
                }
                ckckphong.DataSource = _lkpc;
            }
            ckckphong.CheckAll();
            //cklKho.ValueMember = "MaKP";
            //cklKho.DisplayMember = "TenKP";
            var _lkho = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).OrderBy(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
            //_lkho.Insert(0, new KPhong { MaKP = 0, TenKP = "Chọn tất cả" });
            if (_lkho.Count > 0)
            {
                c_kphong moi1 = new c_kphong();
                moi1.TenKP = "Chọn tất cả";
                moi1.MakP = 0;
                moi1.CHon = true;
                _lkhoc.Add(moi1);
                foreach (var item in _lkho)
                {
                    c_kphong moi = new c_kphong();
                    moi.TenKP = item.TenKP;
                    moi.MakP = item.MaKP;
                    moi.CHon = true;
                    _lkhoc.Add(moi);
                }
                cklKho.DataSource = _lkhoc;
            }
            cklKho.CheckAll();
           // lupKho.Properties.DataSource = q.Where(p=>p.PLoai.Contains("dược")).ToList();

            List<DTBN> qDT = (from dt in data.DTBNs select dt).ToList();// đối tượng bệnh nhân
            qDT.Insert( 0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDtuong.Properties.DataSource = qDT;
           
            List<string> _lTrongNgoaiDM = new List<string> { "Ngoài danh mục", "Trong danh mục", "Không thanh toán" };
            cklTrongNgoaiDM.DataSource = _lTrongNgoaiDM;
            for (int i = 0; i < cklTrongNgoaiDM.ItemCount; i++)
            {
                cklTrongNgoaiDM.SetItemChecked(i, true);
            }
            //for (int i = 0; i < cklKho.ItemCount; i++)
            //{
            //    cklKho.SetItemChecked(i, true);
            //}
            
            _listNhomDV = data.NhomDVs.Where(p => p.Status == 1).ToList();
            
            cklNhomDV.DisplayMember = "TenNhom";
            cklNhomDV.ValueMember = "IDNhom";
            cklNhomDV.DataSource = _listNhomDV;
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                cklNhomDV.SetItemChecked(i, true);
            }
            
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            load = true;
            lupDtuong.EditValue = 100;
            //lupKhoa.EditValue = DungChung.Bien.MaKP;
            rdTrangThai.SelectedIndex = 2;
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
        //    DateTime tungay = System.DateTime.Now.Date;
        //    DateTime denngay = System.DateTime.Now.Date;
        //    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
        //    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
        //    int ng = (denngay.AddMonths(-1) - tungay).Days;
        //    if (load && ng > 0)
        //    {
        //        MessageBox.Show("Báo cáo chỉ thống kê trong 1 tháng, số ngày bạn nhập > 1 tháng. Hãy kiểm tra lại.");
        //        lupKhoa.Focus();
        //    }
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            dateDenNgay.DateTime = dateTuNgay.DateTime.AddDays(15);
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lupDtuong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupDtuong.EditValue == null || Convert.ToInt32(lupDtuong.EditValue) == 100)
            {
                ckLinhVeKhoa.Enabled = true;
                ckLinhVeKhoa.Checked = true;
            }

            else
            {
                ckLinhVeKhoa.Enabled = false;
                ckLinhVeKhoa.Checked = false;
            }
        }

        private void ckckphong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(ckckphong.SelectedValue) == 0)
            {
                if (ckckphong.GetItemChecked(ckckphong.SelectedIndex))
                {
                    ckckphong.CheckAll();

                }
                else
                {
                    ckckphong.UnCheckAll();
                }
            }
            for (int i = 0; i < ckckphong.ItemCount; i++)
            {
                int makp = Convert.ToInt32(ckckphong.GetItemValue(i));
                if (ckckphong.GetItemChecked(i))
                {

                    foreach (var item in _lkpc)
                    {
                        if (item.MakP == makp&&item.MakP!=0)
                        {
                            item.CHon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _lkpc)
                    {
                        if (item.MakP == makp || makp == 0)
                        {
                            item.CHon = false;
                            // break;
                        }
                    }
                }
            }
        }

        private void cklKho_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            
        }

        private void cklKho_ItemCheck_1(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(cklKho.SelectedValue) == 0)
            {
                if (cklKho.GetItemChecked(ckckphong.SelectedIndex))
                {
                    cklKho.CheckAll();

                }
                else
                {
                    cklKho.UnCheckAll();
                }
            }
            for (int i = 0; i < cklKho.ItemCount; i++)
            {
                int makp = Convert.ToInt32(cklKho.GetItemValue(i));
                if (cklKho.GetItemChecked(i))
                {

                    foreach (var item in _lkhoc)
                    {
                        if (item.MakP == makp && item.MakP != 0)
                        {
                            item.CHon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _lkhoc)
                    {
                        if (item.MakP == makp || makp == 0)
                        {
                            item.CHon = false;
                            // break;
                        }
                    }
                }
            }
        }

      
    }
}