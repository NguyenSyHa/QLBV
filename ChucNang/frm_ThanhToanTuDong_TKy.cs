using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.ChucNang
{
    public partial class frm_ThanhToanTuDong_TK : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThanhToanTuDong_TK()
        {
            InitializeComponent();
        }
        List<Bang79a> _lDsbn = new List<Bang79a>();
        public frm_ThanhToanTuDong_TK(List<Bang79a> ds)
        {
            InitializeComponent();
            _lDsbn = ds;
        }

        private void frm_ThanhToanTuDong_Load(object sender, EventArgs e)
        {
            memoEdit1.Text = "Sửa tự động sẽ thanh toán lại những bệnh nhân sai tổng tiền và chưa có mã ICD";
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _dem = 0,tongso=0;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            MessageBox.Show("Backup dữ liệu trước khi thực hiện");
            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
            frm.ShowDialog();
           // _ktmatkhau = true;//
            if (_ktmatkhau)
            {
                _lDsbn = _dataContext.Bang79a.ToList();
                tongso = _lDsbn.Count;
                foreach (var c in _lDsbn)
                {
                    try{
                    int _mabn = c.MaBNhan;
                    #region update thanh toán
                    if (c.DChi == "30007")
                    {
                        var ktt = _dataContext.VienPhis.Where(p => p.MaBNhan== _mabn).ToList();
                        // xóa dữ liệu viện phí
                        if (ktt.Count > 0)
                        {
                            //int _idxoa = ktt.First().idVPhi;
                            //var sl = _dataContext.VienPhicts.Where(p => p.idVPhi == _idxoa).ToList();
                            //if (sl.Count > 0)
                            //{

                            //    foreach (var s in sl)
                            //    {
                            //        var dtct = _dataContext.VienPhicts.Single(p => p.idVPhict == s.idVPhict);
                            //        _dataContext.Remove(dtct);
                            //        _dataContext.SaveChanges();
                            //    }
                            //}
                            //var xoad = _dataContext.VienPhis.Single(p => p.idVPhi == _idxoa);
                            //_dataContext.Remove(xoad);
                            //_dataContext.SaveChanges();
                        }//
                        // thanh toán lại
                        else
                        // if ( DungChung.Ham._checkNgayKhoa(_dataContext, c.NgayTT.Value, "KhoaVP") == false)
                        {
                            //if (!DungChung.Ham.KTCongKham_ngaygiuong(_dataContext, _mabn))
                            //{
                            if( _mabn>0)
                            {
                                //if (KTraLuu())
                                //{
                                int _tyle = 0;
                                //string tencd = "";
                               // tencd = DungChung.Ham.KTChiDinh(_dataContext, _mabn);
                                bool ttoan = true;
                                DateTime dt = System.DateTime.Now;
                                var bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                                if (bnkb.Count > 0)
                                    dt = bnkb.First().NgayKham.Value;
                                if (ttoan)
                                {
                                    int idvp = -1;
                                    double _tienBH = 0;
                                    double _tienBN = 0;
                                    int _pttt = 0;
                                    string _muc = "";
                                    string _dtuong = "";
                                    int _vanchuyen = -1;//9
                                    double _tienvc = 0;
                                    // bxung mới
                                    decimal _luongCS = 0;
                                    string _khuvuc = "";
                                    int _HangBV = 4;
                                    int _tuyen = 1;
                                    string _DTuong = "";
                                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan== _mabn).ToList();
                                    if (bn.Count > 0)
                                    {
                                        if (bn.First().LuongCS != null)
                                            _luongCS = bn.First().LuongCS.Value;
                                        if (bn.First().KhuVuc != null && bn.First().KhuVuc.Contains("K"))
                                            _khuvuc = bn.First().KhuVuc;
                                        if (bn.First().MucHuong != null && bn.First().MucHuong.Value > 0)
                                        {
                                            _muc = bn.First().MucHuong.ToString();
                                            //MessageBox.Show(_muc.ToString());
                                            _pttt = DungChung.Ham._PtramTT(_dataContext, _muc);
                                        }
                                    }
                                    //
                                    _HangBV = DungChung.Ham.hangBV(DungChung.Bien.MaBV);

                                    // var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan== _mabn).ToList();
                                    if (bn.Count > 0)
                                    {
                                        try
                                        {
                                            _tuyen = bn.First().Tuyen.Value;
                                            _DTuong = bn.First().DTuong;
                                            if (bn.First().SThe != null && bn.First().SThe.ToString() != "" && bn.First().SThe.Length > 10)
                                            {
                                                // _muc = bn.First().SThe.Substring(2, 1);
                                                _dtuong = bn.First().SThe.Substring(0, 2);
                                            }

                                            // 9
                                            var ktvc = _dataContext.DTuongs.Where(p => p.MaDTuong.Contains(_dtuong)).Select(p => p.VanChuyen).ToList();
                                            if (ktvc.Count > 0)
                                                if (ktvc.First() != null && ktvc.First().ToString() != "")
                                                    _vanchuyen = ktvc.First().Value;
                                        }
                                        catch (Exception)
                                        {
                                            _DTuong = "Dịch vụ";
                                        }
                                    }
                                    _pttt = DungChung.Ham._PtramTT(_dataContext, _muc);
                                    var kttt = _dataContext.VienPhis.Where(p => p.MaBNhan== _mabn).ToList();
                                    if (kttt.Count <= 0)
                                    {
                                        var kt = (from ds in _dataContext.DThuocs.Where(p => p.MaBNhan== _mabn).OrderBy(p => p.IDDon) select ds).ToList();
                                        if (kt.Count > 0)
                                        {
                                            bool thanhtoan = true;

                                            if (thanhtoan)
                                            {
                                                var ktntru = _dataContext.BenhNhans.Where(p => p.MaBNhan== _mabn).Where(p => p.NoiTru == 1).ToList();
                                                if (ktntru.Count > 0)
                                                {
                                                    var vienphi = (from kd in _dataContext.DThuocs
                                                                   join kdct in _dataContext.DThuoccts.OrderBy(p => p.IDDonct) on kd.IDDon equals kdct.IDDon
                                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                                   where (kd.MaBNhan == _mabn)
                                                                   //where(kd.KieuDon==1 || kd.KieuDon==0) //kieemrtra lai kieu don
                                                                   group new { kdct, dv } by new { kdct.MaDV, kdct.DonGia, kdct.DonVi, kdct.TrongBH } into kq
                                                                   select new { madv = kq.Key.MaDV, dongia = kq.Key.DonGia, donvi = kq.Key.DonVi, trongBH = kq.Key.TrongBH, soluong = kq.Sum(p => p.kdct.SoLuong), thanhtien = kq.Sum(p => p.kdct.ThanhTien) }).OrderBy(p => p.donvi).ToList();

                                                    // kiểm tra vận chuyển
                                                    foreach (var a in vienphi)
                                                    {
                                                        var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                      join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                      select nhom.TenNhom).ToList();
                                                        if (ktmadv.Count > 0)
                                                        {
                                                            _tienvc = a.thanhtien;
                                                            break;
                                                        }
                                                    }
                                                    //
                                                    VienPhi vp = new VienPhi();
                                                    vp.MaBNhan = kt.First().MaBNhan;
                                                    vp.NgayTT = dt;
                                                    // ngoai h
                                                    bool _ngoaih = false;
                                                  //  _ngoaih = DungChung.Ham.CheckNGioHC(c.NgayTT.Value);
                                                    vp.NgoaiGio = 0;
                                                    if (_ngoaih == true)
                                                    {

                                                        vp.NgoaiGio = 1;
                                                    }
                                                    //
                                                    //if (lupNguoiTT.EditValue != null && lupNguoiTT.EditValue.ToString() != "")
                                                    //    vp.MaCB = lupNguoiTT.EditValue.ToString();
                                                    //else
                                                    vp.MaCB = DungChung.Bien.MaCB;
                                                    vp.MaKP = String.IsNullOrEmpty(DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]) ? 0 : Convert.ToInt32(DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]);
                                                    _dataContext.VienPhis.Add(vp);

                                                    if (_dataContext.SaveChanges() >= 0)
                                                    {
                                                        _dem++;
                                                        var q = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.idVPhi).ToList();
                                                        if (q.Count > 0)
                                                        {
                                                            idvp = q.First().idVPhi;
                                                        }
                                                        else
                                                        {
                                                            idvp = 0;
                                                        }
                                                        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                                        double tt = 0;
                                                        double _tongtien = vienphi.Where(p => p.trongBH == 1).Sum(p => p.thanhtien);//ktra
                                                        if (idvp > 0)
                                                        {

                                                            foreach (var a in vienphi)
                                                            {
                                                                if (a.soluong != 0)
                                                                {
                                                                    VienPhict vpct = new VienPhict();
                                                                    vpct.MaDV = a.madv;
                                                                    vpct.DonGia = a.dongia == null ? 0 : a.dongia;
                                                                    vpct.SoLuong = a.soluong == null ? 0 : a.soluong;
                                                                    vpct.DonVi = a.donvi;
                                                                    vpct.idVPhi = idvp;
                                                                    vpct.TienChenh = 0;
                                                                    vpct.SoLuongD = 0;
                                                                    if (a.thanhtien != null && a.thanhtien.ToString() != "")
                                                                    {
                                                                        tt = Math.Round(Convert.ToDouble(a.thanhtien), DungChung.Bien.LamTronSo);
                                                                        vpct.ThanhTien = tt;
                                                                    }
                                                                    if (_DTuong == "BHYT")
                                                                    {
                                                                        var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                                      join nhom in _dataContext.NhomDVs.Where(p => p.TenNhom.ToLower().Contains("vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                                      select nhom.TenNhom).ToList();
                                                                        if (_tuyen == 1)//bệnh nhân đúng tuyến
                                                                        {
                                                                            if (a.trongBH == 1)
                                                                            {//dịch vụ trong danh mục BHYT
                                                                                if (ktmadv.Count > 0)
                                                                                {

                                                                                    if (_vanchuyen == 1)
                                                                                    {
                                                                                        vpct.TienBH = tt;
                                                                                        vpct.TrongBH = 1;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        vpct.TienBN = tt;
                                                                                        vpct.TrongBH = 0;
                                                                                    }
                                                                                }

                                                                            //k thúc
                                                                                else
                                                                                {

                                                                                    if ((_tongtien - _tienvc) >= DungChung.Bien.GHanTT100 && _luongCS != 1)
                                                                                    {
                                                                                        _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                        vpct.TienBH = _tienBH;
                                                                                        vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        vpct.TienBH = tt;
                                                                                    }
                                                                                    vpct.TrongBH = 1;
                                                                                }


                                                                            }
                                                                            else
                                                                            {
                                                                                if (a.trongBH == 0)
                                                                                {
                                                                                    vpct.TrongBH = 0;
                                                                                    vpct.TienBH = 0;
                                                                                    vpct.TienBN = tt;
                                                                                }
                                                                                else
                                                                                {
                                                                                    vpct.TrongBH = 2;
                                                                                    vpct.TienBH = 0;
                                                                                    vpct.TienBN = 0;
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (_tuyen == 2) //BN trái tuyến
                                                                            {
                                                                                if (a.trongBH == 1)
                                                                                {//dịch vụ trong danh mục BHYT
                                                                                    if (ktmadv.Count > 0)
                                                                                    {

                                                                                        if (_vanchuyen == 1)
                                                                                        {
                                                                                            vpct.TienBH = tt;
                                                                                            vpct.TrongBH = 0;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TienBN = tt;
                                                                                            vpct.TrongBH = 1;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        // mới
                                                                                        if (!string.IsNullOrEmpty(_khuvuc))
                                                                                        {
                                                                                            _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                            vpct.TienBH = _tienBH;
                                                                                            vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if ((_tongtien - _tienvc) < DungChung.Bien.GHanTT100 && (DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "12001"))
                                                                                            {
                                                                                                _tienBH = Math.Round(tt * _tyle / 100, DungChung.Bien.LamTronSo);
                                                                                                vpct.TienBH = _tienBH;
                                                                                                vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                _tienBH = Math.Round(tt * _tyle / 100 * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                                vpct.TienBH = _tienBH;
                                                                                                vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                            }
                                                                                        }
                                                                                        //
                                                                                        vpct.TrongBH = 1;
                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    if (a.trongBH == 0)
                                                                                    {
                                                                                        vpct.TrongBH = 0;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = tt;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        vpct.TrongBH = 2;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = 0;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (a.trongBH == 2)
                                                                        {
                                                                            vpct.TrongBH = 2;
                                                                            vpct.TienBH = 0;
                                                                            vpct.TienBN = 0;
                                                                        }
                                                                        else
                                                                        {
                                                                            vpct.TrongBH = 0;
                                                                            vpct.TienBH = 0;
                                                                            vpct.TienBN = tt;
                                                                        }
                                                                    }
                                                                    _data.VienPhicts.Add(vpct);
                                                                    _data.SaveChanges();
                                                                }
                                                            }

                                                        }//kt
                                                        else
                                                        {
                                                            MessageBox.Show("không có mã bệnh nhân trong bảng viện phí");
                                                        }
                                                    }

                                                } // ketthu noi tru
                                                else
                                                { // ngoại trú
                                                    var ktkd = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1)
                                                                where (kd.MaBNhan == _mabn)
                                                                select kd.IDDon).ToList();
                                                    if (ktkd.Count >= 2)
                                                    { // để hiển thị theo kê đơn 11/09
                                                        var vienphi = (from kd in _dataContext.DThuocs
                                                                       join kdct in _dataContext.DThuoccts.OrderBy(p => p.IDDonct) on kd.IDDon equals kdct.IDDon
                                                                       join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                                       where (kd.MaBNhan == _mabn)
                                                                       //where(kd.KieuDon==1 || kd.KieuDon==0) //kieemrtra lai kieu don
                                                                       group new { kdct, dv } by new { kdct.MaDV, kdct.DonGia, kdct.DonVi, kdct.TrongBH } into kq
                                                                       select new { madv = kq.Key.MaDV, dongia = kq.Key.DonGia, donvi = kq.Key.DonVi, trongBH = kq.Key.TrongBH, soluong = kq.Sum(p => p.kdct.SoLuong), thanhtien = kq.Sum(p => p.kdct.ThanhTien) }).ToList();
                                                        // kiểm tra vận chuyển
                                                        foreach (var a in vienphi)
                                                        {
                                                            var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                          join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                          select nhom.TenNhom).ToList();
                                                            if (ktmadv.Count > 0)
                                                            {
                                                                _tienvc = a.thanhtien;
                                                                break;
                                                            }
                                                        }
                                                        //
                                                        VienPhi vp = new VienPhi();
                                                        vp.MaBNhan = kt.First().MaBNhan;
                                                        vp.NgayTT = dt;
                                                        //if (lupNguoiTT.EditValue != null)
                                                        //    vp.MaCB = lupNguoiTT.EditValue.ToString();
                                                        //if (lupNguoiTT.EditValue != null && lupNguoiTT.EditValue.ToString() != "")
                                                        //    vp.MaCB = lupNguoiTT.EditValue.ToString();
                                                        //else
                                                        vp.MaCB = DungChung.Bien.MaCB;
                                                        vp.MaKP = String.IsNullOrEmpty(DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]) ? 0 : Convert.ToInt32(DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]);
                                                        _dataContext.VienPhis.Add(vp);
                                                        if (_dataContext.SaveChanges() >= 0)
                                                        {
                                                            _dem++;
                                                            DungChung.Ham._setStatus(_mabn, 3);
                                                            var q = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.idVPhi).ToList();
                                                            if (q.Count > 0)
                                                            {
                                                                idvp = q.First().idVPhi;
                                                            }
                                                            else
                                                            {
                                                                idvp = 0;
                                                            }
                                                            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                                            double tt = 0;
                                                            double _tongtien = vienphi.Where(p => p.trongBH == 1).Sum(p => p.thanhtien);//ktra
                                                            if (idvp > 0)
                                                            {

                                                                foreach (var a in vienphi)
                                                                {
                                                                    if (a.soluong != 0)
                                                                    {
                                                                        VienPhict vpct = new VienPhict();
                                                                        vpct.MaDV = a.madv;
                                                                        vpct.DonGia = a.dongia;
                                                                        vpct.SoLuong = a.soluong;
                                                                        vpct.DonVi = a.donvi;
                                                                        vpct.idVPhi = idvp;
                                                                        if (a.thanhtien != null && a.thanhtien.ToString() != "")
                                                                        {
                                                                            tt = Math.Round(Convert.ToDouble(a.thanhtien), DungChung.Bien.LamTronSo);
                                                                            vpct.ThanhTien = tt;
                                                                        }
                                                                        if (_DTuong == "BHYT")
                                                                        {
                                                                            var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                                          join nhom in _dataContext.NhomDVs.Where(p => p.TenNhom.ToLower().Contains("vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                                          select nhom.TenNhom).ToList();
                                                                            if (_tuyen == 1)//bệnh nhân đúng tuyến
                                                                            {
                                                                                if (a.trongBH == 1)
                                                                                {//dịch vụ trong danh mục BHYT
                                                                                    if (ktmadv.Count > 0)
                                                                                    {
                                                                                        if (_vanchuyen == 1)
                                                                                        {
                                                                                            vpct.TienBH = tt;
                                                                                            vpct.TrongBH = 1;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TienBN = tt;
                                                                                            vpct.TrongBH = 0;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if ((_tongtien - _tienvc) >= DungChung.Bien.GHanTT100 && _luongCS != 1)
                                                                                        {
                                                                                            _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                            vpct.TienBH = _tienBH;
                                                                                            vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TienBH = tt;
                                                                                        }
                                                                                        vpct.TrongBH = 1;
                                                                                    }


                                                                                }
                                                                                else
                                                                                {
                                                                                    if (a.trongBH == 0)
                                                                                    {
                                                                                        vpct.TrongBH = 0;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = tt;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        vpct.TrongBH = 2;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = 0;
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (_tuyen == 2) //BN trái tuyến
                                                                                {
                                                                                    if (a.trongBH == 1)
                                                                                    {//dịch vụ trong danh mục BHYT
                                                                                        if (ktmadv.Count > 0)
                                                                                        {
                                                                                            if (_vanchuyen == 1)
                                                                                            {
                                                                                                vpct.TienBH = tt;
                                                                                                vpct.TrongBH = 1;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                vpct.TienBN = tt;
                                                                                                vpct.TrongBH = 0;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // mới
                                                                                            if (!string.IsNullOrEmpty(_khuvuc))
                                                                                            {
                                                                                                _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                                vpct.TienBH = _tienBH;
                                                                                                vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if ((_tongtien - _tienvc) < DungChung.Bien.GHanTT100 && (DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "12001"))
                                                                                                {
                                                                                                    _tienBH = Math.Round(tt * _tyle / 100, DungChung.Bien.LamTronSo);
                                                                                                    vpct.TienBH = _tienBH;
                                                                                                    vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    _tienBH = Math.Round(tt * _tyle / 100 * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                                    vpct.TienBH = _tienBH;
                                                                                                    vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                                }

                                                                                            }
                                                                                            //
                                                                                            vpct.TrongBH = 1;
                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (a.trongBH == 0)
                                                                                        {
                                                                                            vpct.TrongBH = 0;
                                                                                            vpct.TienBH = 0;
                                                                                            vpct.TienBN = tt;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TrongBH = 2;
                                                                                            vpct.TienBH = 0;
                                                                                            vpct.TienBN = 0;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (a.trongBH == 2)
                                                                            {
                                                                                vpct.TrongBH = 2;
                                                                                vpct.TienBH = 0;
                                                                                vpct.TienBN = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                vpct.TrongBH = 0;
                                                                                vpct.TienBH = 0;
                                                                                vpct.TienBN = tt;
                                                                            }
                                                                        }
                                                                        _data.VienPhicts.Add(vpct);
                                                                        _data.SaveChanges();
                                                                    }
                                                                }

                                                            }//kt
                                                            else
                                                            {
                                                                MessageBox.Show("không có mã bệnh nhân trong bảng viện phí");
                                                            }
                                                        }
                                                        //MessageBox.Show("ok");
                                                        // 1. thêm mới vào bảng ra viện
                                                        //if (ktntru.Count > 0)
                                                        //}
                                                        //_ravien.
                                                        //1.kthuc
                                                    }
                                                    else
                                                    {
                                                        var vienphi = (from kd in _dataContext.DThuocs.Where(p => p.KieuDon != -2)
                                                                       join kdct in _dataContext.DThuoccts.OrderBy(p => p.IDDonct) on kd.IDDon equals kdct.IDDon
                                                                       join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                                       where (kd.MaBNhan == _mabn)
                                                                       //where(kd.KieuDon==1 || kd.KieuDon==0) //kieemrtra lai kieu don
                                                                       group new { kdct, dv } by new { kdct.IDDonct, kdct.MaDV, kdct.DonGia, kdct.DonVi, kdct.TrongBH } into kq
                                                                       select new { kq.Key.IDDonct, madv = kq.Key.MaDV, dongia = kq.Key.DonGia, donvi = kq.Key.DonVi, trongBH = kq.Key.TrongBH, soluong = kq.Sum(p => p.kdct.SoLuong), thanhtien = kq.Sum(p => p.kdct.ThanhTien) }).OrderBy(p => p.IDDonct).ToList();

                                                        // kiểm tra vận chuyển
                                                        foreach (var a in vienphi)
                                                        {
                                                            var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                          join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                          select nhom.TenNhom).ToList();
                                                            if (ktmadv.Count > 0)
                                                            {
                                                                _tienvc = a.thanhtien;
                                                                break;
                                                            }
                                                        }
                                                        //
                                                        VienPhi vp = new VienPhi();
                                                        vp.MaBNhan = kt.First().MaBNhan;
                                                        vp.NgayTT = dt;
                                                        //if (lupNguoiTT.EditValue != null)
                                                        //    vp.MaCB = lupNguoiTT.EditValue.ToString();
                                                        //if (lupNguoiTT.EditValue != null && lupNguoiTT.EditValue.ToString() != "")
                                                        //    vp.MaCB = lupNguoiTT.EditValue.ToString();
                                                        //else
                                                        vp.MaCB = DungChung.Bien.MaCB;
                                                        vp.MaKP =String.IsNullOrEmpty( DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]) ? 0 : Convert.ToInt32( DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]);
                                                        _dataContext.VienPhis.Add(vp);
                                                        if (_dataContext.SaveChanges() >= 0)
                                                        {
                                                            _dem++;
                                                            DungChung.Ham._setStatus(_mabn, 3);
                                                            var q = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.idVPhi).ToList();
                                                            if (q.Count > 0)
                                                            {
                                                                idvp = q.First().idVPhi;
                                                            }
                                                            else
                                                            {
                                                                idvp = 0;
                                                            }
                                                            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                                            double tt = 0;
                                                            double _tongtien = vienphi.Where(p => p.trongBH == 1).Sum(p => p.thanhtien);//ktra
                                                            if (idvp > 0)
                                                            {

                                                                foreach (var a in vienphi)
                                                                {
                                                                    if (a.soluong != 0)
                                                                    {
                                                                        VienPhict vpct = new VienPhict();
                                                                        vpct.MaDV = a.madv;
                                                                        vpct.DonGia = a.dongia;
                                                                        vpct.SoLuong = a.soluong;
                                                                        vpct.DonVi = a.donvi;
                                                                        vpct.idVPhi = idvp;
                                                                        if (a.thanhtien != null && a.thanhtien.ToString() != "")
                                                                        {
                                                                            tt = Math.Round(Convert.ToDouble(a.thanhtien), DungChung.Bien.LamTronSo);
                                                                            vpct.ThanhTien = tt;
                                                                        }
                                                                        if (_DTuong == "BHYT")
                                                                        {
                                                                            var ktmadv = (from dv in _dataContext.DichVus.Where(p => p.MaDV== (a.madv))
                                                                                          join nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Vận chuyển")) on dv.IDNhom equals nhom.IDNhom
                                                                                          select nhom.TenNhom).ToList();
                                                                            if (_tuyen == 1)//bệnh nhân đúng tuyến
                                                                            {
                                                                                if (a.trongBH == 1)
                                                                                {//dịch vụ trong danh mục BHYT
                                                                                    if (ktmadv.Count > 0)
                                                                                    {
                                                                                        if (_vanchuyen == 1)
                                                                                        {
                                                                                            vpct.TienBH = tt;
                                                                                            vpct.TrongBH = 1;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TienBN = tt;
                                                                                            vpct.TrongBH = 0;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if ((_tongtien - _tienvc) >= DungChung.Bien.GHanTT100 && _luongCS != 1)
                                                                                        {
                                                                                            _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                            vpct.TienBH = _tienBH;
                                                                                            vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TienBH = tt;
                                                                                        }
                                                                                        vpct.TrongBH = 1;
                                                                                    }


                                                                                }
                                                                                else
                                                                                {
                                                                                    if (a.trongBH == 0)
                                                                                    {
                                                                                        vpct.TrongBH = 0;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = tt;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        vpct.TrongBH = 2;
                                                                                        vpct.TienBH = 0;
                                                                                        vpct.TienBN = 0;
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (_tuyen == 2) //BN trái tuyến
                                                                                {
                                                                                    if (a.trongBH == 1)
                                                                                    {//dịch vụ trong danh mục BHYT
                                                                                        if (ktmadv.Count > 0)
                                                                                        {
                                                                                            if (_vanchuyen == 1)
                                                                                            {
                                                                                                vpct.TienBH = tt;
                                                                                                vpct.TrongBH = 1;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                vpct.TienBN = tt;
                                                                                                vpct.TrongBH = 0;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // mới
                                                                                            if (!string.IsNullOrEmpty(_khuvuc))
                                                                                            {
                                                                                                _tienBH = Math.Round(tt * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                                vpct.TienBH = _tienBH;
                                                                                                vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if ((_tongtien - _tienvc) < DungChung.Bien.GHanTT100 && (DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "12001"))
                                                                                                {
                                                                                                    _tienBH = Math.Round(tt * _tyle / 100, DungChung.Bien.LamTronSo);
                                                                                                    vpct.TienBH = _tienBH;
                                                                                                    vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    _tienBH = Math.Round(tt * _tyle / 100 * _pttt / 100, DungChung.Bien.LamTronSo);
                                                                                                    vpct.TienBH = _tienBH;
                                                                                                    vpct.TienBN = Math.Round((tt - _tienBH), DungChung.Bien.LamTronSo);
                                                                                                }

                                                                                            }
                                                                                            //
                                                                                            vpct.TrongBH = 1;
                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (a.trongBH == 0)
                                                                                        {
                                                                                            vpct.TrongBH = 0;
                                                                                            vpct.TienBH = 0;
                                                                                            vpct.TienBN = tt;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            vpct.TrongBH = 2;
                                                                                            vpct.TienBH = 0;
                                                                                            vpct.TienBN = 0;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (a.trongBH == 2)
                                                                            {
                                                                                vpct.TrongBH = 2;
                                                                                vpct.TienBH = 0;
                                                                                vpct.TienBN = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                vpct.TrongBH = 0;
                                                                                vpct.TienBH = 0;
                                                                                vpct.TienBN = tt;
                                                                            }
                                                                        }
                                                                        _data.VienPhicts.Add(vpct);
                                                                        _data.SaveChanges();
                                                                    }
                                                                }

                                                            }
                                                            //kt
                                                            else
                                                            {
                                                                MessageBox.Show("không có mã bệnh nhân trong bảng viện phí");
                                                            }

                                                        }

                                                    }
                                                }
                                            }

                                        } // két thúc kiểm tra bn có chi phí hay ko

                                        else
                                        {
                                            MessageBox.Show("Bệnh nhân này không có dịch vụ để thanh toán!");
                                        }
                                    }
                                    else
                                    {
                                    }

                                    // lưu ra viện
                                    var ktrvien = _dataContext.RaViens.Where(p => p.MaBNhan== _mabn).ToList();
                                    if (ktrvien.Count <= 0)
                                    {
                                       // var bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan== _mabn).OrderByDescending(p => p.IDKB).ToList();

                                        RaVien _ravien = new RaVien();
                                        if (bnkb.Count > 0)
                                        {
                                            _ravien.MaKP = String.IsNullOrEmpty( DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]) ? 0 : Convert.ToInt32( DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[2]);
                                            _ravien.MaICD = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[0];
                                            _ravien.ChanDoan = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[1];

                                            if (bnkb.First().PhuongAn != null && bnkb.First().PhuongAn == 2)
                                                _ravien.Status = 1;
                                            else
                                                _ravien.Status = 2;
                                        }
                                        // kiểm tra lại số ngày điều trị
                                        _ravien.SoNgaydt = 1;
                                        _ravien.NgayRa =dt;
                                        _ravien.MaBNhan = _mabn;
                                        _dataContext.RaViens.Add(_ravien);
                                        _dataContext.SaveChanges();
                                    }
                                }
                                //}
                            }// kết thúc kiểm tra ma bn
                            //}
                        
                        }

                    }
                    #endregion
                    //
                    else
                    {
                        // update mã ICD
                        #region update mã ICD
                        var ktrv = _dataContext.RaViens.Where(p => p.MaBNhan== _mabn).ToList();
                        if (ktrv.Count > 0)
                        {
                            var sua = _dataContext.RaViens.Single(p => p.MaBNhan== _mabn);
                            sua.MaICD = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[0];
                            sua.ChanDoan = DungChung.Ham.getMaICDarr(_dataContext, _mabn, DungChung.Bien.GetICD,0)[1];
                        }
                        #endregion
                    }
                } catch(Exception){
                
                }
                }
                MessageBox.Show("Thực hiện thành công: "+_dem+"/"+tongso);
            }
        }
    }
}