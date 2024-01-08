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
    public partial class Frm_TamTraDVKT_New : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TamTraDVKT_New()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public class c_Check
        {
            string tenbuong;
            bool check;
            public string Buong
            {
                set { tenbuong = value; }
                get { return tenbuong; }
            }
            public bool Check
            {
                set { check = value; }
                get { return check; }
            }
        }
        List<c_Check> _lBuong = new List<c_Check>();
        private void Frm_TamTraThuoc_Load(object sender, EventArgs e)
        {
            LupKhoaPhong.EditValue = DungChung.Bien.MaKP;
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai=="Lâm sàng") select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
            //radM.SelectedIndex = 1;
            radNT.SelectedIndex = 2;
            //chkBoxung.Checked = true;
            //chkThuongxuyen.Checked = true;
            //chkTrathuoc.Checked = true;
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KT()
        {
            if (LupNgayTu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayTu.Focus();
                return false;
            }
            if (LupNgayDen.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayDen.Focus();
                return false;
            }
            if (LupKhoaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                LupKhoaPhong.Focus();
                return false;
            }
            //if (cbo_TrangThai.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa chọn trạng thái!");
            //    cbo_TrangThai.Focus();
            //    return false;
            //}
            if ((LupNgayDen.DateTime - LupNgayTu.DateTime).Days > 30)
            {
                MessageBox.Show("Khoảng ngày quá dài!");
                LupNgayTu.Focus();
                return false;
            }
            return true;
        }
        public class c_DSThuoc
        {
            string _tenDV, _donvi;
            int _madv;
            public string TenDV
            {
                set { _tenDV = value; }
                get { return _tenDV; }
            }
            public int MaDV
            {
                set { _madv = value; }
                get { return _madv; }
            }
            public string DonVi
            {
                set { _donvi = value; }
                get { return _donvi; }
            }
        }
        public class c_BN_Thuoc
        {
            string _buong;
            int _madv, _mabn;
            double _soluong;
            public int MaBNhan
            {
                set { _mabn = value; }
                get { return _mabn; }
            }
            public int MaDV
            {
                set { _madv = value; }
                get { return _madv; }
            }
            public string Buong
            {
                set { _buong = value; }
                get { return _buong; }
            }
            public double SoLuong
            {
                set { _soluong = value; }
                get { return _soluong; }
            }
        }
        //  List<string> _lBuong = new List<string>();
        private class clBuong_IDKB
        {
            private int id;
            public int IDKB
            {
                set { id = value; }
                get { return id; }
            }
            private string tenbuong;
            public string Buong
            {
                set { tenbuong = value; }
                get { return tenbuong; }
            }
        }
        public string GetChanDoan(string _chandoan)
        {
            string chandoan = "";
            chandoan = _chandoan;
            string[] getcd = chandoan.Split(';');
            if(getcd.Count()>0)
            {
                chandoan = getcd[0];
            }
            return chandoan.ToString();
        }
        private void Taoso_Click(object sender, EventArgs e)
        {
            if (KT())
            {
                //List<c_DSThuoc> _lDichVu_T = new List<c_DSThuoc>();
                List<c_DSThuoc> _lDichVu = new List<c_DSThuoc>();// nếu in trang 1
                List<c_DSThuoc> _lDichVu2 = new List<c_DSThuoc>();// nếu in trang 2
                List<c_DSThuoc> _lDichVu3 = new List<c_DSThuoc>();// nếu in trang 3
                List<c_BN_Thuoc> _lBN_Thuoc = new List<c_BN_Thuoc>();
                DateTime _tungay = System.DateTime.Now.Date;
                DateTime _denngay = System.DateTime.Now.Date;
                int _makp = 0;
                //int _status = -1;
                int _noitru1 = -1, _noitru2 = -1;
                _makp = LupKhoaPhong.EditValue == null ? 0 : Convert.ToInt32(LupKhoaPhong.EditValue);
                _tungay = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                _denngay = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
                if (radNT.SelectedIndex == 2)
                {
                    _noitru1 = 0;
                    _noitru2 = 1;
                }
                else
                {
                    _noitru1 = radNT.SelectedIndex;
                    _noitru2 = radNT.SelectedIndex;
                }
                // lấy buồng
                List<c_Check> _lBuong = new List<c_Check>();
                for (int i = 0; i < grvBuong.RowCount; i++)
                {
                    if (grvBuong.GetRowCellValue(i, colChon) != null && grvBuong.GetRowCellValue(i, colChon).ToString() == "True")
                    {
                        _lBuong.Add(new c_Check { Buong = grvBuong.GetRowCellValue(i, colTenBuong).ToString().Trim(), Check = true });

                    }

                }
                bool _tc = false;
                if (_lBuong.Count == grvBuong.RowCount)
                    _tc = true;
                if (_tc)
                    _lBuong.Add(new c_Check { Buong = "", Check = true });
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var dv_tn = (from dv in _data.DichVus
                             join tn in _data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new { dv.MaDV, dv.TenDV, dv.DonVi, tn.TenRG, tn.TenTN,tn.STT
                                 ,
                                          tnGNHTT = (tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) ? 1 : 0
                             }).ToList();
                var _dsthuoc2 = (from dt in _data.DThuocs.Where(p => p.PLDV == 2)
                                 join dtct in _data.DThuoccts//.Where(p => p.MaKP == _makp)
                                 on dt.IDDon equals dtct.IDDon
                                 select new { dt.MaBNhan, dtct.IDKB, Status_ct = dtct.Status, dt.KieuDon, dtct.MaDV, dtct.TrongBH, dtct.SoLuong }).ToList();
                var _dsthuoc3 = (from a in dv_tn
                                 join b in _dsthuoc2 on a.MaDV equals b.MaDV
                                 select new
                                 {
                                     a.STT,
                                     b.MaBNhan,
                                     b.IDKB,
                                     Status_ct = b.Status_ct,
                                     b.KieuDon,
                                     b.MaDV,
                                     b.TrongBH,
                                     b.SoLuong,
                                     a.tnGNHTT,
                                     a.TenDV, a.DonVi, a.TenRG, a.TenTN }).ToList();
                var mabnhan = (from a in _dsthuoc2 select a.MaBNhan).Distinct().ToList();
                var benhnhan = (from a in mabnhan
                                join bn in _data.BenhNhans on a equals bn.MaBNhan
                                join rv in _data.RaViens.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay).Where(p => p.MaKP == _makp) on bn.MaBNhan equals rv.MaBNhan
                                select new { bn.MaBNhan, bn.TenBNhan, bn.DTNT, bn.Tuoi, bn.NNhap, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.NoiTru,rv.ChanDoan,bn.SThe,bn.GTinh,rv.SoNgaydt }).ToList();
                var _dsthuoc = (from c in _dsthuoc3
                                join bn in benhnhan on c.MaBNhan equals bn.MaBNhan
                                where (_noitru2 == 1 || bn.DTNT == true)
                                select new
                                {
                                    c.STT,
                                    c.IDKB,
                                    c.tnGNHTT,
                                    Nam = bn.GTinh == 1 ? bn.Tuoi : null,
                                    Nu = bn.GTinh == 1 ? null : bn.Tuoi,
                                    Caotuoi = bn.Tuoi >= 60 ? bn.SoNgaydt : null,
                                    bn.SoNgaydt,
                                    bn.ChanDoan,
                                    bn.DTNT,
                                    bn.SThe,
                                    bn.Tuoi,
                                    bn.NNhap,
                                    bn.NgaySinh,
                                    bn.ThangSinh,
                                    bn.NamSinh,
                                    Status_ct = c.Status_ct,
                                    bn.NoiTru,
                                    bn.MaBNhan,
                                    bn.TenBNhan,
                                    c.KieuDon,
                                    c.MaDV,
                                    c.TrongBH,
                                    c.TenDV,
                                    c.DonVi,
                                    c.TenRG,
                                    c.TenTN,
                                    c.SoLuong
                                }).ToList();

                //get IDKB;
                var _qDsIDKB = _dsthuoc.Select(p => p.IDKB).Distinct().ToList();
                var _qDsBG = (from id in _qDsIDKB
                              join bnkb in _data.BNKBs on id equals bnkb.IDKB
                              select bnkb).ToList();
                List<clBuong_IDKB> _lIDKB = new List<clBuong_IDKB>();
                _lIDKB = (from a in _lBuong
                          join b in _qDsBG on a.Buong equals b.Buong
                          select new clBuong_IDKB { IDKB = b.IDKB, Buong = (b.Buong == null ? "" : b.Buong.Trim()) + (DungChung.Bien.MaBV == "20001" ? (b.Giuong??"") : "") }).Distinct().ToList();
                var listid = (from a in _lIDKB select new { a.IDKB, a.Buong }).Distinct().ToList();
                _lIDKB = (from a in listid select new clBuong_IDKB { IDKB = a.IDKB, Buong = a.Buong }).ToList();
                if (_tc)
                    _lIDKB.Add(new clBuong_IDKB { IDKB = 0, Buong = "" });
                // tạo danh sách tên dịch vụ
                var _ldv 
                    = (from id in _lIDKB
                            join dt in _dsthuoc on id.IDKB equals dt.IDKB
                            select new  {MaDV = dt.MaDV,TenDV = dt.TenDV,DonVi = dt.DonVi,TenTN = dt.TenTN,Status_ct = dt.Status_ct,STT = dt.STT
                                ,tnGNHTT = dt.tnGNHTT
                            }).Distinct()
                            .OrderBy(p => p.STT).ThenBy(p => p.TenTN).ThenBy(p => p.DonVi).ThenBy(p => p.TenDV)
                            .ToList();             

                int _dem = 0;
                List<LDichVu> _lDichVu_T = new List<LDichVu>();
                  var qdv2  = (from a in _ldv select new {DonVi = a.DonVi,MaDV = a.MaDV,TenDV = a.TenDV,STT = a.STT,tnGNHTT = a.tnGNHTT,TenTN = a.TenTN
                }).Distinct().ToList();
                  if (DungChung.Bien.MaBV == "27023")
                     _lDichVu_T =(from a in  qdv2 select new LDichVu{DonVi = a.DonVi,MaDV = a.MaDV,TenDV = a.TenDV,STT = a.STT,tnGNHTT = a.tnGNHTT,TenTN = a.TenTN}).OrderBy(p => p.tnGNHTT).ThenBy(p => p.DonVi).ThenBy(p => p.TenTN).ThenBy(p => p.TenDV).ToList();
                  else
                      _lDichVu_T = (from a in qdv2 select new LDichVu { DonVi = a.DonVi, MaDV = a.MaDV, TenDV = a.TenDV, STT = a.STT, tnGNHTT = a.tnGNHTT, TenTN = a.TenTN }).OrderBy(p => p.STT).ThenBy(p => p.TenTN).ThenBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();

                foreach (var a in _lDichVu_T)
                {
                    _dem++;
                    if (_dem < 40)
                    {
                        _lDichVu.Add(new c_DSThuoc { MaDV = a.MaDV == null ? 0 : a.MaDV.Value, TenDV = a.TenDV, DonVi = a.DonVi });
                    }
                    else
                    {
                        if (_dem < 80)
                        {
                            _lDichVu2.Add(new c_DSThuoc { MaDV = a.MaDV == null ? 0 : a.MaDV.Value, TenDV = a.TenDV, DonVi = a.DonVi });
                        }
                        else
                        {
                            _lDichVu3.Add(new c_DSThuoc { MaDV = a.MaDV == null ? 0 : a.MaDV.Value, TenDV = a.TenDV, DonVi = a.DonVi });
                        }
                    }
                }
                //
                // tạo danh sách bệnh nhân
                var _dsbn2 = (from
                              dt in _dsthuoc
                              select new { dt.IDKB, dt.MaBNhan, dt.TenBNhan, NNhap = dt.NNhap, dt.NgaySinh, dt.ThangSinh, dt.NamSinh, dt.Tuoi, dt.Nu, ChanDoan = GetChanDoan(dt.ChanDoan), dt.SoNgaydt, dt.Nam, SThe = dt.SThe.Trim() == "" ? "" : dt.SThe, dt.Caotuoi }).Distinct().ToList();

                var _dsbn = (from id in _lIDKB
                              join dt in _dsbn2 on id.IDKB equals dt.IDKB
                              select new { Buong =id.Buong, dt.MaBNhan, TenBNhan = dt.TenBNhan.ToUpper(), dt.Nam,dt.Nu,dt.Caotuoi,dt.ChanDoan,dt.SThe,dt.SoNgaydt }
                
                        ).Distinct().OrderBy(p => p.Buong).ThenBy(p => p.TenBNhan).ToList();
                var _BN_Thuoc = (from id in _lIDKB
                                 join dt in _dsthuoc on id.IDKB equals dt.IDKB
                                 group new { dt } by new { dt.MaBNhan, dt.MaDV, Buong = id.Buong, } into kq
                                 select new { kq.Key.Buong, kq.Key.MaBNhan, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dt.SoLuong) }
                                ).ToList();
                foreach (var a in _BN_Thuoc)
                {
                    _lBN_Thuoc.Add(new c_BN_Thuoc { MaBNhan = a.MaBNhan, MaDV = a.MaDV == null ? 0 : a.MaDV.Value, SoLuong = a.SoLuong, Buong = a.Buong });
                }
                bool _inTrang2 = false;
                if (_lDichVu2.Count > 0)
                {
                    _inTrang2 = true;
                }
                if (_lDichVu.Count > 0)
                {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_TamTraDVKT_New_20001 rep = new BaoCao.Rep_TamTraDVKT_New_20001(_lDichVu, _lBN_Thuoc);
                        rep.Ngaythang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                        rep.DataSource = _dsbn;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có đơn thuốc nào", "Thông báo", MessageBoxButtons.OK);
                }
                if (_lDichVu2.Count > 0)
                {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_TamTraDVKT_New_20001 rep = new BaoCao.Rep_TamTraDVKT_New_20001(_lDichVu2, _lBN_Thuoc);
                        rep.Ngaythang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                        rep.DataSource = _dsbn;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                }
                if (_lDichVu3.Count > 0)
                {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_TamTraDVKT_New_20001 rep = new BaoCao.Rep_TamTraDVKT_New_20001(_lDichVu3, _lBN_Thuoc);
                        rep.Ngaythang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString();
                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                        rep.DataSource = _dsbn;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();

                }
                //
            }
        }

        private void LupKhoaPhong_EditValueChanged(object sender, EventArgs e)
        {
            int _makp = 0;
            if (LupKhoaPhong.EditValue != null)
                _makp = Convert.ToInt32( LupKhoaPhong.EditValue);
            var Buong = _Data.KPhongs.Where(p => p.MaKP == _makp).ToList();
            DateTime _tungay = System.DateTime.Now.Date;
            DateTime _denngay = System.DateTime.Now.Date;
            _tungay = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
            _denngay = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
            string buonggiuong = "";
            if (Buong.Count > 0)
            {
                buonggiuong = Buong.First().BuongGiuong;
            }
            // add buồng, giường
            _lBuong.Clear();
            if (!string.IsNullOrEmpty(buonggiuong))
            {
                //string[] _bg = new string[20];
                //_bg = buonggiuong.Split(';');
                //int k = 0;
                //k = _bg.Length;
                //string[] _buong = new string[k];
                //for (int i = 0; i < k; i++)
                //{
                //    _buong[i] = "";
                //}

                //if (buonggiuong != null)
                //    _buong = buonggiuong.Split(';');
                //if (_buong.Length > 0)
                //{
                //    for (int i = 0; i < _buong.Length; i++)
                //    {
                //        int j = _buong[i].IndexOf('{');
                //        if (j > 0)
                //        {
                //            _buong[i] = _buong[i].Remove(j);
                //        }
                //    }
                //}
                List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _lBuongG = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
                string nam1 = Convert.ToString(_tungay.Year);
                string nam2 = Convert.ToString(_denngay.Year);
                if (nam1 == nam2)
                {
                    _lBuongG = QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.getBuongGiuong(_Data, _makp, nam1);
                    var lBuong = (from b in _lBuongG
                                  group b by new { b.buong } into kq
                                  select kq.Key.buong).ToList();
                    foreach (string bg in lBuong)
                    {
                        _lBuong.Add(new c_Check { Buong = bg, Check = true });
                    }
                }
                else
                {
                    _lBuongG = QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.getBuongGiuong(_Data, _makp, nam1);
                    var lBuong1 = (from b in _lBuongG
                                  group b by new { b.buong } into kq
                                  select kq.Key.buong).ToList();
                    foreach (string bg in lBuong1)
                    {
                        _lBuong.Add(new c_Check { Buong = bg, Check = true });
                    }
                    _lBuongG.Clear();
                    _lBuongG = QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.getBuongGiuong(_Data, _makp, nam2);
                    var lBuong2 = (from b in _lBuongG
                                   group b by new { b.buong } into kq
                                   select kq.Key.buong).ToList();
                    foreach (string bg in lBuong2)
                    {
                        _lBuong.Add(new c_Check { Buong = bg, Check = true });
                    }
                    
                }
            }
            grcBuong.DataSource = _lBuong.Distinct().ToList();
        }
        public class LDichVu
        {       
            public int? MaDV { set; get; }
            public string TenDV { set; get; }
            public string DonVi { set; get; }
            public string TenTN { set; get; }
            public int? Status { set; get; }
            public int? Status_ct { set; get; }
            public int? STT { set; get; }
            public int tnGNHTT { set; get; }
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void LupNgayTu_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}

