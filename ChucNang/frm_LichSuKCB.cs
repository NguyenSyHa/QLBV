using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

namespace QLBV.ChucNang
{
    public partial class frm_LichSuKCB : DevExpress.XtraEditors.XtraForm
    {
        string ds = "";
       // string sthe = "";
        string _user = "";
        string _pass = "";
        GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 _the = new GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018();
        List<GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet> hsct = new List<GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet>();
        List<GiamDinhBHXH.BHXH_Model.XML1> _lTHop = new List<GiamDinhBHXH.BHXH_Model.XML1>();
        List<GiamDinhBHXH.BHXH_Model.XML2> _lThuoc = new List<GiamDinhBHXH.BHXH_Model.XML2>();
        List<GiamDinhBHXH.BHXH_Model.XML3> _lDVu = new List<GiamDinhBHXH.BHXH_Model.XML3>();
        List<GiamDinhBHXH.BHXH_Model.XML4> _lCLS = new List<GiamDinhBHXH.BHXH_Model.XML4>();
        //public frm_LichSuKCB(string ls, string _sthe)
        //{
        //    InitializeComponent();
        //    ds = ls;
        //    this.sthe = _sthe;
        //}
        public frm_LichSuKCB(GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 TTthe, string user, string pass)
        {
            InitializeComponent();
            _the = TTthe;
            _user = user;
            _pass = pass;

        }
        List<BenhVien> _lBenhVien = new List<BenhVien>();
        private void frm_LichSuKCB_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lBenhVien = DaTaContext.BenhViens.ToList();
            _lcb = DaTaContext.CanBoes.ToList();
            _ldv = DaTaContext.DichVus.ToList();
            #region lịch sử khám tại viện
            if (_the != null &&  _the.maThe != null && _the.maThe.Length == 15)
            {
                var person = DaTaContext.People.Where(p => p.SThe == _the.maThe).Select(p => p.IDPerson).FirstOrDefault();
                var kp = DaTaContext.KPhongs.ToList();
                var lsu1 = (from bn in DaTaContext.BenhNhans.Where(p => p.IDPerson == person).Where(p => p.SThe != null && p.SThe.Length > 1)
                            join rv in DaTaContext.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from b in kq.DefaultIfEmpty()
                            select new { MaKP = b == null ? bn.MaKP : b.MaKP, NgayRa = b == null ? null : b.NgayRa, ChanDoan = b == null ? "" : b.ChanDoan, bn.MaBNhan, bn.NNhap }).ToList();
                var lsu = (from a in lsu1
                           join b in kp on a.MaKP equals b.MaKP
                           select new
                           {
                               TenKP = b.TenKP,
                               NgayRa = a.NgayRa,
                               ChanDoan = a.ChanDoan,
                               a.MaBNhan,
                               a.NNhap,
                           }).OrderByDescending(p => p.NNhap).ToList();
                gridControl1.DataSource = lsu.ToList();
            }
            #endregion
            #region lấy dữ liệu từ trên cổng

            // lịch sử chi tiết
            List<GiamDinhBHXH.BHXH_Model.LichSuKCB2018> ListLS = new List<GiamDinhBHXH.BHXH_Model.LichSuKCB2018>();
            string maKQ = "";// mã kết quả trả về
            //string user = "30004_BV";
            //string password = "123456a@";
            //GiamDinhBHXH.BHXH_Model.theBHYT the = new GiamDinhBHXH.BHXH_Model.theBHYT { maCSKCB = "30009", gioiTinh = 0, hoTen = "Cao Thị Ga", maThe = "CN3300606700424", ngayBD = "01/01/2016", ngayKT = "31/12/2016", ngaySinh = "01/01/1948" };

            try
            {
                //var response = GiamDinhBHXH.BHXH_Model.Service.nha(_user, _pass, _the, ref ListLS, ref maKQ);
                GiamDinhBHXH.BHXH_Model.KQNhanLichSuKCBBS rp = GiamDinhBHXH.BHXH_Model.Service.NhanLichSuKCBBS_CV366(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], _the);
                if (rp != null && rp.dsLichSuKCB2018 != null)
                    ListLS = rp.dsLichSuKCB2018.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

            foreach (var a in ListLS)
            {
                string msg = ""; // tin nhắn  trả về
                GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet hs = new GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet();
                var qNhanHoSoKCBChiTiet = GiamDinhBHXH.BHXH_Model.Service.NhanHoSoKCBChiTiet(_user, _pass, a.maHoSo, ref hs, ref msg);
                hsct.Add(hs);
                if (hs.xml1 != null)
                _lTHop.Add(hs.xml1);
                if (hs.dsXml2 != null)
                _lThuoc.AddRange(hs.dsXml2);
                if (hs.dsXml3 != null)
                _lDVu.AddRange(hs.dsXml3);
                if (hs.dsXml4 != null)
                _lCLS.AddRange(hs.dsXml4);


            }


            //convert ngày
            foreach (var b in _lTHop)
            {
                b.NgayVao = GetNgayThang(b.NgayVao);
                b.NgayRa = GetNgayThang(b.NgayRa);
            }
            foreach (var b in _lThuoc)
            {
                b.NgayYl = GetNgayThang(b.NgayYl);
            }
            foreach (var b in _lDVu)
            {
                b.NgayYl = GetNgayThang(b.NgayYl);
            }
            foreach (var b in _lCLS)
            {
                b.NgayKq = GetNgayThang(b.NgayKq);
            }


            grc_LSBenhVienKham.DataSource = _lTHop.OrderBy(p => p.Id).ToList();
            #endregion


        }

        private string GetNgayThang(string ngay)
        {
            string rs = "";
            if (ngay != null && ngay.Length >= 8)
                rs = ngay.Substring(6, 2) + "/" + ngay.Substring(4, 2) + "/" + ngay.Substring(0, 4);
            return rs;
        }
        private void grc_LSBenhVienKham_MouseHover(object sender, EventArgs e)
        {

        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            GridHitInfo hi = grv_LSBenhVienKham.CalcHitInfo(e.ControlMousePosition);
            if (hi.Column != null)
            {
                if (hi.Column.Name == "colMaBV")
                {
                    if (grv_LSBenhVienKham.GetRowCellValue(hi.RowHandle, colMaBV) != null && grv_LSBenhVienKham.GetRowCellValue(hi.RowHandle, colMaBV).ToString() != "")
                    {
                        string maBV = grv_LSBenhVienKham.GetRowCellValue(hi.RowHandle, colMaBV).ToString();
                        string tenbv = _lBenhVien.Where(p => p.MaBV == maBV).Select(p => p.TenBV).FirstOrDefault();
                        object o = hi.RowHandle.ToString() + hi.Column.FieldName;
                        ToolTipControlInfo info = new ToolTipControlInfo(o, tenbv);
                        info.ImmediateToolTip = true;
                        e.Info = info;
                    }
                }
            }
        }

        private void grv_LSBenhVienKham_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grv_LSBenhVienKham.GetRowCellValue(grv_LSBenhVienKham.FocusedRowHandle, colMaLk) != null && grv_LSBenhVienKham.GetRowCellValue(grv_LSBenhVienKham.FocusedRowHandle, colMaLk).ToString() != "")
            {
                string malk = grv_LSBenhVienKham.GetRowCellValue(grv_LSBenhVienKham.FocusedRowHandle, colMaLk).ToString();
                grc_Thuoc.DataSource = _lThuoc.Where(p => p.MaLk == malk).OrderBy(p => p.Stt).ToList();
                grc_DichVu.DataSource = _lDVu.Where(p => p.MaLk == malk).OrderBy(p => p.Stt).ToList();
                grc_CLS.DataSource = _lCLS.Where(p => p.MaLk == malk).OrderBy(p => p.Stt).ToList();
            }

        }

        private void grv_LSBenhVienKham_DataSourceChanged(object sender, EventArgs e)
        {
            grv_LSBenhVienKham_FocusedRowChanged(null, null);
        }
        private class LSKCB
        {
            private DateTime ngaynhap;
            private int mabn;
            private string ngayke;
            private string tendv;
            private string tencb;
            private string chandoan;
            private string donvi;
            private string soluong;
            private string huongdan;
            private string ketqua;
            private int ploai;
            public int PhanLoai
            {
                set { ploai = value; }
                get { return ploai; }
            }
            public DateTime NNhap
            { set { ngaynhap = value; } get { return ngaynhap; } }
            public int MaBNhan
            { set { mabn = value; } get { return mabn; } }
            public string NgayKe
            { set { ngayke = value; } get { return ngayke; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string TenCB
            { set { tencb = value; } get { return tencb; } }
            public string ChanDoan
            { set { chandoan = value; } get { return chandoan; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public string SoLuong
            { set { soluong = value; } get { return soluong; } }
            public string HuongDan
            { set { huongdan = value; } get { return huongdan; } }
            public string KetQua
            { set { ketqua = value; } get { return ketqua; } }
        }
        List<CanBo> _lcb = new List<CanBo>();
        private string _getTenCB(List<CanBo> lcb, string _macb)
        {
            string _tencb = "";
            var ten = lcb.Where(p => p.MaCB == _macb).Select(p => p.TenCB).ToList();
            if (ten.Count > 0 && ten.First() != null)
                _tencb = ten.First();
            return _tencb;
        }
        List<DichVu> _ldv = new List<DichVu>();
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gridColumn8")
            {
                QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int _mabn = 0;
                if (gridView1.GetFocusedRowCellValue(gridColumn9) != null)
                    _mabn = Convert.ToInt32(gridView1.GetFocusedRowCellValue(gridColumn9));
                List<LSKCB> _BC = new List<LSKCB>();
                //string _sthe = "";
                _BC.Clear();
                var ttbn = DaTaContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                var rvbn = DaTaContext.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                var dthuoc = (from dt in DaTaContext.DThuocs.Where(p => p.PLDV == 1).Where(p => p.MaBNhan == _mabn)
                              join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                              select new { dt.MaBNhan, dtct.MaDV, dt.MaCB, dt.NgayKe, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList();

                //_lcb = DaTaContext.CanBoes.ToList();
                var LS = (from bn in ttbn
                          join dt in dthuoc on bn.MaBNhan equals dt.MaBNhan
                          join dv in _ldv on dt.MaDV equals dv.MaDV
                          join rv in rvbn on bn.MaBNhan equals rv.MaBNhan
                          //(from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                          //      join dt in DaTaContext.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals dt.MaBNhan
                          //      join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                          //      join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                          //      join rv in DaTaContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                          select new { dt.MaCB, bn.NNhap, bn.MaBNhan, dt.NgayKe, dt.MaDV, dv.TenDV, rv.ChanDoan, dt.DonVi, dt.SoLuong, dt.HuongDan }).ToList().OrderBy(p => p.NNhap).ToList();
                //select new { bn.NNhap, bn.MaBNhan, dt.NgayKe, dtct.MaDV, dv.TenDV,  rv.ChanDoan, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList().OrderBy(p => p.NNhap).ToList();
                var lcls = (from cl in DaTaContext.CLS.Where(p => p.MaBNhan == _mabn)
                            join cd in DaTaContext.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals cd.IdCLS
                            join clsct in DaTaContext.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                            join dvct in DaTaContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                            select new { cl.MaCB, cl.MaBNhan, cl.NgayThang, cd.MaDV, clsct.KetQua, dvct.TenDVct }).ToList();
                var cls = (from bn in ttbn
                           join cls1 in lcls on bn.MaBNhan equals cls1.MaBNhan
                           join dv in _ldv on cls1.MaDV equals dv.MaDV
                           join rv in rvbn on bn.MaBNhan equals rv.MaBNhan
                           //(from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                           //       join cl in DaTaContext.CLS on bn.MaBNhan equals cl.MaBNhan
                           //       join cd in DaTaContext.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals cd.IdCLS
                           //       join clsct in DaTaContext.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                           //       join dvct in DaTaContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           //       join dv in DaTaContext.DichVus on dvct.MaDV equals dv.MaDV
                           //       join rv in DaTaContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                           select new { cls1.MaCB, bn.NNhap, bn.MaBNhan, cls1.NgayThang, dv.TenDV, rv.ChanDoan, cls1.KetQua, cls1.TenDVct }).ToList().OrderBy(p => p.NNhap).ToList();

                foreach (var a in LS)
                {
                    LSKCB themmoi = new LSKCB();
                    themmoi.PhanLoai = 1;
                    themmoi.ChanDoan = a.ChanDoan;
                    themmoi.DonVi = a.DonVi;
                    themmoi.HuongDan = a.HuongDan;
                    themmoi.MaBNhan = a.MaBNhan;
                    themmoi.NNhap = a.NNhap.Value;
                    themmoi.NgayKe = a.NgayKe.Value.ToShortDateString();
                    themmoi.SoLuong = a.SoLuong.ToString();
                    themmoi.TenCB = _getTenCB(_lcb, a.MaCB);
                    themmoi.TenDV = a.TenDV;
                    _BC.Add(themmoi);
                }
                foreach (var b in cls)
                {
                    LSKCB themmoi = new LSKCB();
                    themmoi.PhanLoai = 2;
                    themmoi.TenDV = b.TenDVct;
                    themmoi.TenCB = _getTenCB(_lcb, b.MaCB);
                    themmoi.SoLuong = "0";
                    themmoi.NgayKe = b.NgayThang.Value.ToShortDateString();
                    themmoi.NNhap = b.NNhap.Value;
                    themmoi.MaBNhan = b.MaBNhan;
                    themmoi.KetQua = b.KetQua;
                    themmoi.ChanDoan = b.ChanDoan;
                    _BC.Add(themmoi);
                }
                _BC = _BC.OrderByDescending(p => p.NNhap).ThenBy(p => p.PhanLoai).ToList();
                if (_BC.Count > 0 && ttbn.Count > 0)
                {
                    Phieu.rep_lskcb rep = new Phieu.rep_lskcb();
                    frmIn frm = new frmIn();
                    rep.TenBN.Value = ttbn.First().TenBNhan.ToUpper();
                    rep.DataSource = _BC;
                    rep.BindData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu!");
                }
            }
        }
    }
}