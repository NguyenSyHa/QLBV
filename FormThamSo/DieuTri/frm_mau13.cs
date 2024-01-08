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
    public partial class frm_mau13 : DevExpress.XtraEditors.XtraForm
    {
        public frm_mau13()
        {
            InitializeComponent();
        }
        int _mabn = 0;
        public frm_mau13(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        public class KhoaPhong
        {
            private bool _check;
            private string _maKP;
            private string _kp;
            private int _stt;
            private string dt;
            private string _nt;
            private string _nd;
            public string _Ngaytu { set { _nt = value; } get { return _nt; } }
            public string _Ngayden { set { _nd = value; } get { return _nd; } }
            public string DT { set { dt = value; } get { return dt; } }
            public int STT { get { return _stt; } set { _stt = value; } }
            public string MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KhoaPhong> _lKP = new List<KhoaPhong>();
        private void frm_mau13_Load(object sender, EventArgs e)
        {
            //
            //? kiểm tra ploai không phải lâm sàng
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //
            var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (ktvv.Count <= 0)
            {
                MessageBox.Show("bệnh nhân chưa có khám bệnh vào viện");
                this.Close();
            }
            else
            {
                if (ktvv.First().MaKP == null)
                {
                    MessageBox.Show("Kiểm tra lại khám bệnh vào viện, thiếu khoa phòng điều trị");
                    this.Close();
                }
            }

            lup_MaKP.DataSource = _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            //
            try
            {
                int i = 0;
                var kt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                          join dtct in _dataContext.DThuoccts.Where(p => p.MaKP == null)
                              on dt.IDDon equals dtct.IDDon
                          select dtct).ToList();
                foreach (var a in kt)
                {
                    if (a.MaKP== null)
                    {
                        var b = _dataContext.DThuocs.Where(p => p.IDDon == a.IDDon).ToList();
                        if (b.Count > 0)
                        {
                            i++;
                            a.MaKP = b.First().MaKP;
                            _dataContext.SaveChanges();
                        }
                    }
                }


                // kiểm tra IDKB và Mã KP không cùng 1 bản ghi trên BNKB
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ktIDKB = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                              join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                              join bnkb in _dataContext.BNKBs on dtct.IDKB equals bnkb.IDKB
                              where dtct.MaKP != bnkb.MaKP
                              select dtct).ToList();
                foreach (var c in ktIDKB)
                {
                    var bnkb = _dataContext.BNKBs.Where(p => p.IDKB == c.IDKB).ToList();
                    if (bnkb.Count > 0)
                    {
                        int _makp = bnkb.First().MaKP == null ? 0 : bnkb.First().MaKP.Value;
                        c.MaKP = _makp;
                        _dataContext.SaveChanges();
                    }
                }
                // kiểm tra IDKB không có
                var kt2 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                           join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
                           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           where dtct.IDKB == null || dtct.IDKB <= 0
                           select dtct).ToList();
                foreach (var b in kt2)
                {
                    var bnkb = _dataContext.BNKBs.Where(p => p.MaKP == b.MaKP && p.MaBNhan == _mabn).ToList();
                    if (bnkb.Count > 0)
                    {
                        b.IDKB = bnkb.First().IDKB;
                        _dataContext.SaveChanges();
                    }

                }
                // kiểm tra phòng khám update chi phí vào khoa điều trị đầu tiên
                var kt3 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                           join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
                           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           join kp in _dataContext.KPhongs on dtct.MaKP equals kp.MaKP
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham
                           select dtct).ToList();

                foreach (var b in kt3)
                {
                    var makp = (from vv in _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn)
                                join bnkb in _dataContext.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
                                where vv.MaKP == bnkb.MaKP
                                select bnkb
                                   ).ToList();
                    if (makp.Count > 0)
                    {
                        int makpnt = makp.First().MaKP == null ? 0 : makp.First().MaKP.Value;
                        b.MaKP = makpnt;
                        b.IDKB = makp.First().IDKB;
                        _dataContext.SaveChanges();
                    }

                }
                // KT kiểm tra khác Lâm sàng
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi update: " + ex.Message);
            }
            //if (DungChung.Bien.PLoaiKP == "Admin")
            //    btnUpdate.Visible = true;



            //
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //
            var dskp = (from kp in _dataContext.KPhongs
                        join dtct in _dataContext.DThuoccts on kp.MaKP equals dtct.MaKP
                        join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn) on dtct.IDDon equals dt.IDDon
                      
                        select new { kp.MaKP, kp.TenKP, dtct.IDKB }).Distinct().ToList();
            chl_KhoaPhong.DataSource = dskp;
            //
            lupMaDVtt.DataSource = _dataContext.DichVus.ToList();

            //

        }




        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                var kt = (from dt in _dataContext.DThuocs.Where(p => p.NgayKe.Value.Year == 2015)
                          join dtct in _dataContext.DThuoccts.Where(p => p.MaKP == null || p.NgayNhap == null)
                              on dt.IDDon equals dtct.IDDon
                          select dtct).ToList();
                foreach (var a in kt)
                {
                    if (a.MaKP != null)
                    {
                        var b = _dataContext.DThuocs.Where(p => p.IDDon == a.IDDon).ToList();
                        if (b.Count > 0)
                        {
                            i++;
                            a.MaKP = b.First().MaKP;
                            a.NgayNhap = b.First().NgayKe;
                            _dataContext.SaveChanges();
                        }
                    }
                }
                var kt2 = (from dt in _dataContext.DThuocs.Where(p => p.NgayKe.Value.Year == 2015)
                           join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
                           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           join kp in _dataContext.KPhongs.Where(p => p.PLoai == "Phòng khám") on dtct.MaKP equals kp.MaKP
                           join vv in _dataContext.VaoViens on dt.MaBNhan equals vv.MaBNhan
                           select dtct).ToList();
                foreach (var b in kt2)
                {
                    var kt3 = (from bn in _dataContext.BenhNhans
                               join dt in _dataContext.DThuocs.Where(p => p.IDDon == b.IDDon) on bn.MaBNhan equals dt.MaBNhan
                               select bn).ToList();
                    if (kt3.Count > 0)
                    {
                        int mbn = kt3.First().MaBNhan;
                        var makp = _dataContext.VaoViens.Where(p => p.MaBNhan == mbn).ToList();
                        if (makp.Count > 0 && makp.First().MaKP != null && makp.First().MaKP.ToString() != "")
                        {
                            int makpnt = makp.First().MaKP == null ? 0 : makp.First().MaKP.Value;
                            b.MaKP = makpnt;
                            _dataContext.SaveChanges();
                        }
                    }
                }

                MessageBox.Show("Update thành công: " + i.ToString() + " Bản ghi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi update: " + ex.Message);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void grvKhoaPhong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void chl_KhoaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            k = chl_KhoaPhong.SelectedIndex;
            for (int i = 0; i < chl_KhoaPhong.ItemCount; i++)
            {
                if (i != k)
                {
                    chl_KhoaPhong.SetItemChecked(i, false);
                }
            }
        }

        private void chl_KhoaPhong_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        List<cl_chiphi> _lchiphi = new List<cl_chiphi>();
        int[] _idkb = new int[10];
        public class cl_chiphi
        {
            string tendv, donvi, tieunhom;
            int madv, makp;
            double dongia, soluong, thanhtien;
            int trongbh, stt;
            int thanhToan;

            public int ThanhToan
            {
                get { return thanhToan; }
                set { thanhToan = value; }
            }
            public int TrongBH
            {
                set { trongbh = value; }
                get { return trongbh; }
            }
            public int STT
            {
                set { stt = value; }
                get { return stt; }
            }
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
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
            public string TenNhom
            {
                set { tieunhom = value; }
                get { return tieunhom; }
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
        }
        private void chl_KhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            _idkb = new int[10];
            int j = 0;
            for (int i = 0; i < chl_KhoaPhong.ItemCount; i++)
            {
                if (chl_KhoaPhong.GetItemChecked(i))
                {
                    _idkb[j] = Convert.ToInt32(chl_KhoaPhong.GetItemValue(i));
                    j++;
                }
                if (j >= 5)
                    break;
            }
            int _id = 0;
            _id = _idkb[0];
           
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //
             var  bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan==_mabn).ToList();
            int makp=bnkb.Where(p=>p.IDKB==_id).Select(p=>p.MaKP).FirstOrDefault()??0;
            var dsid=bnkb.Where(p=>p.MaKP==makp).Select(p=>p.IDKB).ToList();
            _lchiphi = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                        join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                        join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                        join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                        where dsid.Contains(dtct.IDKB)
                        group new { dtct, dv, nhomdv } by new {dtct.ThanhToan, dv.TenDV, nhomdv.TenNhom, nhomdv.STT, dtct.MaKP, dtct.MaDV, dtct.DonGia, dtct.DonVi, dtct.TrongBH } into kq
                        select new cl_chiphi
                        {
                         ThanhToan=   kq.Key.ThanhToan,
                            TenDV = kq.Key.TenDV,
                            DonGia = kq.Key.DonGia == null ? 0 : kq.Key.DonGia,
                            DonVi = kq.Key.DonVi,
                            MaDV = kq.Key.MaDV == null ? 0 : kq.Key.MaDV.Value,
                            MaKP = kq.Key.MaKP == null ? 0 : kq.Key.MaKP.Value,
                            TenNhom = kq.Key.TenNhom,
                            STT = kq.Key.STT == null ? 0 : kq.Key.STT.Value,
                            TrongBH = kq.Key.TrongBH ,
                            SoLuong = kq.Sum(p => p.dtct.SoLuong) == null ? 0 : kq.Sum(p => p.dtct.SoLuong),
                            ThanhTien = kq.Sum(p => p.dtct.ThanhTien) == null ? 0 : kq.Sum(p => p.dtct.ThanhTien),
                        }).ToList();
            grcThanhToan.DataSource = _lchiphi;
        }

        private void _inPhieu()
        {
            int _id = _idkb[0];
            List<cl_chiphi> _lcp = new List<cl_chiphi>();
            for (int i = 0; i < 3; i++)
            {
                int trongBH = -1;
                if (i == 0)
                    trongBH = 1;
                if (i == 1)
                    trongBH = 0;
                if (i == 2)
                    trongBH = i;
                int mabn = _mabn;
                _lcp = _lchiphi.Where(p => p.TrongBH == trongBH && (DungChung.Bien.MaBV=="30010"? p.ThanhToan==0 :true)).ToList();
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                BaoCao.rep_PhieuTDDT rep13 = new BaoCao.rep_PhieuTDDT();
                frmIn frm13 = new frmIn();
                rep13.Tongtien.Value = _lcp.Sum(p => p.ThanhTien);
                // rep13.TienBN.Value = bk01.Sum(p => p.TienBN).Value;
                if (trongBH == 0)
                    rep13.paramTrongNgoaiDM.Value = "(NGOÀI DANH MỤC BHYT)";
                if (trongBH == 1)
                    rep13.paramTrongNgoaiDM.Value = "(TRONG DANH MỤC BHYT)";
                if (trongBH == 2)
                    rep13.paramTrongNgoaiDM.Value = "(BỆNH NHÂN KHÔNG PHẢI THANH TOÁN)";
                rep13.DataSource = null;
                if (_lcp.Count > 0)
                {
                    int _mkp = 0;
                    string _ngayvao = "", _ngayra = "";
                    string _kphong = "", _chandoan = "", _maicd = "";
                    DateTime _ngayv = Convert.ToDateTime("01/01/2000");
                    DateTime _ngayr = Convert.ToDateTime("01/01/2000");
                    DateTime ngay1 = System.DateTime.Now;
                    var kp = _dataContext.BNKBs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDKB).ToList();
                    var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                    var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                    int _ktkp = 0; // kiểm tra bệnh nhân đt 2 khoa giông nhau
                    if (kp.Where(p => p.IDKB == _id).ToList().Count > 0)
                    {
                        _mkp = kp.Where(p => p.IDKB == _id).ToList().First().MaKP == null ? 0 : kp.Where(p => p.IDKB == _id).ToList().First().MaKP.Value;
                        _ktkp = kp.Where(p => p.MaKP == _mkp).ToList().Count;
                        if (!string.IsNullOrEmpty(kp.Where(p => p.IDKB == _id).ToList().First().MaICD))
                        {
                            _maicd = kp.Where(p => p.IDKB == _id).ToList().First().MaICD;
                        }
                        else
                        {
                            _maicd = kp.Where(p => p.IDKB == _id).ToList().First().MaICD2;
                        }
                        _chandoan = kp.Where(p => p.IDKB == _id).ToList().First().ChanDoan + "/" + kp.Where(p => p.IDKB == _id).ToList().First().BenhKhac;
                    }
                    var tenkhoa = _dataContext.KPhongs.Where(p => p.MaKP == _mkp).ToList();
                    if (tenkhoa.Count > 0)
                        _kphong = tenkhoa.First().TenKP;
                    if (rv.Where(p => p.MaKP == _mkp).ToList().Count > 0)
                    {
                        if (_id == kp.Where(p => p.MaKP == _mkp).Max(p => p.IDKB))
                        {
                            _ngayra = "- Ngày ra viện:";
                            _ngayr = rv.Where(p => p.MaKP == _mkp).ToList().First().NgayRa.Value.Date;
                        }
                        else
                        {
                            _ngayra = "- Ngày chuyển khoa:";
                            _ngayr = kp.Where(p => p.IDKB == _id).ToList().First().NgayNghi.Value;
                        }
                    }
                    else
                    {
                        if (kp.Where(p => p.IDKB == _id).ToList().Count > 0 && kp.Where(p => p.IDKB == _id).ToList().First().MaKP != null)
                        {

                            _ngayra = "- Ngày chuyển khoa:";
                            if (kp.Where(p => p.IDKB == _id).ToList().First().NgayNghi != null)
                                _ngayr = kp.Where(p => p.IDKB == _id).ToList().First().NgayNghi.Value;
                        }
                        else
                        {
                            _ngayra = "- Ngày ra viện:";
                        }
                    }
                    if (vv.Where(p => p.MaKP == _mkp).ToList().Count > 0 && _id == kp.Where(p => p.MaKP == _mkp).Min(p => p.IDKB))
                    {


                        _ngayvao = "- Ngày vào viện:";
                        _ngayv = vv.Where(p => p.MaKP == _mkp).ToList().First().NgayVao.Value;

                    }
                    else
                    {
                        _ngayvao = "- Ngày vào khoa:";
                        if (kp.Where(p => p.MaKPdt == _mkp).ToList().Count > 0 && kp.Where(p => p.MaKPdt == _mkp).ToList().First().NgayNghi != null)
                        {
                            _ngayv = kp.Where(p => p.MaKPdt == _mkp).ToList().First().NgayNghi.Value;
                        }
                        else
                            if (kp.Where(p => p.IDKB == _id).ToList().Count > 0 && kp.Where(p => p.IDKB == _id).ToList().First().NgayKham != null)
                            {
                                _ngayv = kp.Where(p => p.IDKB == _id).ToList().First().NgayKham.Value;
                            }
                    }
                    rep13.NgayR.Value = _ngayra;
                    rep13.NgayV.Value = _ngayvao;
                    if (_ngayv.Year > 2010)
                        rep13.NgayVao.Value = _ngayv;
                    else
                        MessageBox.Show("Bệnh nhân chưa có ngày vào khoa hoặc ngày vào viện");
                    if (_ngayr.Year > 2010)
                    {
                        rep13.NgayRa.Value = _ngayr;
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân chưa có ngày chuyển khoa hoặc ngày ra viện");
                    }

                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
                    if (bn.Count > 0)
                    {

                        if (bn.First().NoiTru == 0)
                            rep13.lab_TieuDe.Text = "PHIẾU THEO DÕI ĐIỀU TRỊ NGOẠI TRÚ";
                        rep13.MaBNhan.Value = mabn;
                        rep13.tenBN.Value = bn.First().TenBNhan.ToUpper();
                        if (bn.First().GTinh == 0)
                        {
                            rep13.Nam.Value = "/".ToUpper();
                        }
                        else
                            rep13.Nu.Value = "/".ToUpper();
                        rep13.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _mabn, DungChung.Bien.formatAge);
                        rep13.DiaChi.Value = bn.First().DChi;
                        rep13.paramSoThe.Value = bn.First().SThe;
                        string macs = bn.First().MaCS;
                        if (_dataContext.BenhViens.Where(p => p.MaBV == macs).Count() > 0)
                        {
                            rep13.CSDKKCB.Value = _dataContext.BenhViens.Where(p => p.MaBV == macs).First().TenBV;
                        }
                        rep13.HanBHTu.Value = bn.First().HanBHTu;
                        rep13.HanBHDen.Value = bn.First().HanBHDen;
                    }

                    if (vv.Count > 0)
                    {
                        rep13.SoBA.Value = vv.First().SoBA;
                    }
                    rep13.KhoaPhong.Value = _kphong;
                    if (rv.Count > 0)
                    {
                        rep13.Status.Value = rv.First().Status.ToString();
                        rep13.KetQua.Value = rv.First().KetQua;
                        //rep13.NgayRa.Value = rv.First().NgayRa;

                    }
                    rep13.ChanDoan.Value = _chandoan;
                    rep13.MaICD.Value = _maicd;

                    // rep13.paramTienBH.Value = bk01.First().TienBH;
                    rep13.DataSource = _lcp.ToList();
                    rep13.BindingData();
                    rep13.CreateDocument();
                    frm13.prcIN.PrintingSystem = rep13.PrintingSystem;
                    frm13.ShowDialog();

                }

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _inPhieu();
        }

        private void grvThanhToan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSua")
            {
                int _madv = 0;
                int _makpkt = 0;
                if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                    _madv = Convert.ToInt32( grvThanhToan.GetFocusedRowCellValue(colMaDV));
                if (grvThanhToan.GetFocusedRowCellValue(col_makp) != null)
                    _makpkt = Convert.ToInt32( grvThanhToan.GetFocusedRowCellValue(col_makp));

                ChucNang.frm_Update_MaKP_VP frm = new ChucNang.frm_Update_MaKP_VP(_mabn, _makpkt, _madv);
                frm.ShowDialog();
                frm_mau13_Load(sender, e);
                //   MessageBox.Show("Đang được nâng cấp!.... " + _madv);
            }
        }
    }
}