using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormNhap;
namespace QLBV.FormThamSo
{
    public partial class frm_CapNhatKPtongKet : DevExpress.XtraEditors.XtraForm
    {
        public frm_CapNhatKPtongKet()
        {
            InitializeComponent();
        }
        int _mabn = 0;
        public frm_CapNhatKPtongKet(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();

        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
        List<KhoaPhong> _lKP = new List<KhoaPhong>();
        List<cl_chiphi> lAll = new List<cl_chiphi>();
        List<KPhong> dskp = new List<KPhong>();

        List<KPhong> dskpkedon = new List<KPhong>();
        public void frm_mau13_Load(object sender, EventArgs e)
        {
            #region tạm bỏ
            ////
            ////? kiểm tra ploai không phải lâm sàng
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            ////
            //var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            //if (ktvv.Count <= 0)
            //{
            //    MessageBox.Show("bệnh nhân chưa có khám bệnh vào viện");
            //    this.Close();
            //}
            //else
            //{
            //    if (ktvv.First().MaKP == null)
            //    {
            //        MessageBox.Show("Kiểm tra lại khám bệnh vào viện, thiếu khoa phòng điều trị");
            //        this.Close();
            //    }
            //}

            //lup_MaKP.DataSource = _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            ////
            //try
            //{
            //    int i = 0;
            //    var kt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
            //              join dtct in _dataContext.DThuoccts.Where(p => p.MaKP == null)
            //                  on dt.IDDon equals dtct.IDDon
            //              select dtct).ToList();
            //    foreach (var a in kt)
            //    {
            //        if (a.MaKP== null)
            //        {
            //            var b = _dataContext.DThuocs.Where(p => p.IDDon == a.IDDon).ToList();
            //            if (b.Count > 0)
            //            {
            //                i++;
            //                a.MaKP = b.First().MaKP;
            //                _dataContext.SaveChanges();
            //            }
            //        }
            //    }


            //    // kiểm tra IDKB và Mã KP không cùng 1 bản ghi trên BNKB
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //    var ktIDKB = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
            //                  join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
            //                  join bnkb in _dataContext.BNKBs on dtct.IDKB equals bnkb.IDKB
            //                  where dtct.MaKP != bnkb.MaKP
            //                  select dtct).ToList();
            //    foreach (var c in ktIDKB)
            //    {
            //        var bnkb = _dataContext.BNKBs.Where(p => p.IDKB == c.IDKB).ToList();
            //        if (bnkb.Count > 0)
            //        {
            //            int _makp = bnkb.First().MaKP == null ? 0 : bnkb.First().MaKP.Value;
            //            c.MaKP = _makp;
            //            _dataContext.SaveChanges();
            //        }
            //    }
            //    // kiểm tra IDKB không có
            //    var kt2 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
            //               join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
            //               join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
            //               where dtct.IDKB == null || dtct.IDKB <= 0
            //               select dtct).ToList();
            //    foreach (var b in kt2)
            //    {
            //        var bnkb = _dataContext.BNKBs.Where(p => p.MaKP == b.MaKP && p.MaBNhan == _mabn).ToList();
            //        if (bnkb.Count > 0)
            //        {
            //            b.IDKB = bnkb.First().IDKB;
            //            _dataContext.SaveChanges();
            //        }

            //    }
            //    // kiểm tra phòng khám update chi phí vào khoa điều trị đầu tiên
            //    var kt3 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
            //               join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 1) on dt.MaBNhan equals bn.MaBNhan
            //               join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
            //               join kp in _dataContext.KPhongs on dtct.MaKP equals kp.MaKP
            //               where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham
            //               select dtct).ToList();

            //    foreach (var b in kt3)
            //    {
            //        var makp = (from vv in _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn)
            //                    join bnkb in _dataContext.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
            //                    where vv.MaKP == bnkb.MaKP
            //                    select bnkb
            //                       ).ToList();
            //        if (makp.Count > 0)
            //        {
            //            int makpnt = makp.First().MaKP == null ? 0 : makp.First().MaKP.Value;
            //            b.MaKP = makpnt;
            //            b.IDKB = makp.First().IDKB;
            //            _dataContext.SaveChanges();
            //        }

            //    }
            //    // KT kiểm tra khác Lâm sàng
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("lỗi update: " + ex.Message);
            //}
            ////if (DungChung.Bien.PLoaiKP == "Admin")
            ////    btnUpdate.Visible = true;

            #endregion
            // var qkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DungChung.Bien._lChuyenKhoa = new List<DungChung.Bien.c_chuyenkhoa>();
            DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
            rdThanhToan.SelectedIndex = 0;
            ckTrongDM.Checked = true;
            loadAll();
            dskp = new List<KPhong>();// danh sách khoa phòng trong bảng BNKB của bênh nhân đó

           
            var dskp2 = (from kb in _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn)
                         join kp in _dataContext.KPhongs
                             on kb.MaKP equals kp.MaKP
                         select new  { MaKP = kp.MaKP, TenKP = kp.TenKP }).Distinct().ToList();
            List<KPhong> dskp3 = (from kp in dskp2 select new KPhong { MaKP = kp.MaKP, TenKP = kp.TenKP }).ToList();

            List<KPhong> lKhoxuat = (from kp in _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) select kp).ToList();


            dskpkedon = (from kp in lAll
                         group kp by new { kp.MaKP, kp.TenKP } into kq
                         select new KPhong { MaKP = kq.Key.MaKP, TenKP = kq.Key.TenKP }).ToList();
            dskp3.AddRange(dskpkedon);

            dskp = (from kp in dskp3 group kp by new {kp.MaKP, kp.TenKP} into kq
                    select new KPhong { MaKP = kq.Key.MaKP, TenKP = kq.Key.TenKP }).ToList();

            List<KPhong> lkpTongKetAll = (from kp in dskp
                                          select new KPhong { MaKP = kp.MaKP, TenKP = kp.TenKP }).ToList();
            lkpTongKetAll.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });


            dskpkedon.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });

            // dskp.Insert(0, new KPhong { MaKP = 0, TenKP = "" });

            // khoa phòng trên mục tìm kiếm
            lupKhoKe.Properties.DisplayMember = "TenKP";
            lupKhoKe.Properties.ValueMember = "MaKP";
            lupKhoKe.Properties.DataSource = dskpkedon;

            // khoa kê đơn trong gridview đơn thuốc, dịch vụ
            lup_MaKP.DisplayMember = "TenKP";
            lup_MaKP.ValueMember = "MaKP";
            lup_MaKP.DataSource = dskpkedon;

            // khoa phòng tổng kết in phiếu
            lupKPTongKet.Properties.DisplayMember = "TenKP";
            lupKPTongKet.Properties.ValueMember = "MaKP";
            lupKPTongKet.Properties.DataSource = lkpTongKetAll;
           
            //khoa phòng tổng kết cuối trong bảng thuốc
            lupMaKPTKD.DisplayMember = "TenKP";
            lupMaKPTKD.ValueMember = "MaKP";
            lupMaKPTKD.DataSource = dskp;

            // khoa phòng tổng kết dịch vụ
            lupKhoaTK_DV.DisplayMember = "TenKP";
            lupKhoaTK_DV.ValueMember = "MaKP";
            lupKhoaTK_DV.DataSource = dskp;

            //kho xuất dược
            lupKhoXuat.DisplayMember = "TenKP";
            lupKhoXuat.ValueMember = "MaKP";
            lupKhoXuat.DataSource = lKhoxuat;

            lupKPTongKet.EditValue = lupKPTongKet.Properties.GetKeyValueByDisplayValue(DungChung.Bien.MaKP);

            load = 1;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {

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

        }
        List<cl_chiphi> _lchiphi = new List<cl_chiphi>();

        List<cl_chiphi> _lThuoc = new List<cl_chiphi>();
        List<cl_chiphi> _lDichVu = new List<cl_chiphi>();

        int[] _idkb = new int[10];
        public class cl_chiphi
        {
            string tendv, donvi, tieunhom;
            int madv, makp;
            double dongia, soluong, thanhtien;
            int trongbh, stt;
            int thanhToan;
            public string DthuocKP { set; get; }
            public int PloaiDV { set; get; }
            public int IDKB { set; get; }
            public DateTime? NgayKe { set; get; }
            public int IDDon { set; get; }
            public int IDDonct { set; get; }
            public string lIDdonct{set; get;}
            public string KieuDon { set; get; }
            public int ThanhToan
            {
                get { return thanhToan; }
                set { thanhToan = value; }
            }
            public string TenKP { set; get; }
            public int MaKPtongket { set; get; }
            public int KhoXuat { set; get; }
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

            public string TenCK { get; set; }

            public int? IDCD { get; set; }

            public int? MaCK { get; set; }
        }
        int load = 0;

        private void _inPhieu()
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> lkp = _dataContext.KPhongs.ToList();

            int MaKPtongket = 0;
            if (lupKPTongKet.EditValue == null || lupKPTongKet.Text == "Tất cả")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng tổng kết để in phiếu");
            }
            else
            {
                MaKPtongket = Convert.ToInt32(lupKPTongKet.EditValue);
                {
                    //  _id = a.IDKB;//_idkb[0];
                    List<cl_chiphi> _lcp = new List<cl_chiphi>();
                    for (int i = 0; i < 4; i++)
                    {
                        int trongBH = -1;
                        if (i == 0)
                            trongBH = 1;
                        if (i == 1)
                            trongBH = 0;
                        if (i == 2)
                            trongBH = i;
                        int mabn = _mabn;
                        _lcp = (from cp in _lchiphi.Where(p => p.MaKPtongket == MaKPtongket).Where(p => p.TrongBH == trongBH && (DungChung.Bien.MaBV == "30010" ? p.ThanhToan == 0 : true))
                                group cp by new
                                {                                   
                                    PloaiDV = cp.PloaiDV,
                                    TenDV = cp.TenDV,
                                    DonGia = cp.DonGia,
                                    DonVi = cp.DonVi,
                                    MaDV = cp.MaDV,
                                    MaKP = cp.MaKP,
                                    TenKP = cp.TenKP,
                                    TenNhom = cp.TenNhom,
                                    STT = cp.STT,  
                                    MaKPtongket = cp.MaKPtongket,
                                    KhoXuat = cp.KhoXuat
                                } into kq
                                select new cl_chiphi {
                                    PloaiDV = kq.Key.PloaiDV,
                                    TenDV = kq.Key.TenDV,
                                    DonGia = kq.Key.DonGia,
                                    DonVi = kq.Key.DonVi,
                                    MaDV = kq.Key.MaDV,
                                    MaKP = kq.Key.MaKP,
                                    TenKP = kq.Key.TenKP,
                                    TenNhom = kq.Key.TenNhom,
                                    STT = kq.Key.STT,
                                    SoLuong = kq.Sum(p=>p.SoLuong),
                                    ThanhTien = kq.Sum(p=>p.ThanhTien),
                                    MaKPtongket = kq.Key.MaKPtongket,
                                    KhoXuat = kq.Key.KhoXuat
                                }
                                   ).Where(p=>p.SoLuong!=0).OrderBy(p=>p.PloaiDV).ThenBy(p=>p.TenDV).ToList();

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
                           // int _mkp = 0;
                            string _ngayvao = "", _ngayra = "";
                            string _kphong = "", _chandoan = "", _maicd = "";
                            DateTime _ngayv = Convert.ToDateTime("01/01/2000");
                            DateTime _ngayr = Convert.ToDateTime("01/01/2000");
                            DateTime ngay1 = System.DateTime.Now;
                            var kp = _dataContext.BNKBs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDKB).ToList();
                            var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                            var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                          //  int _ktkp = 0; // kiểm tra bệnh nhân đt 2 khoa giông nhau
                            if (kp.Where(p => p.MaKP == MaKPtongket).ToList().Count > 0)
                            {
                                //_mkp = kp.Where(p => p.MaKP == MaKPtongket).ToList().First().MaKP == null ? 0 : kp.Where(p => p.IDKB == _id).ToList().First().MaKP.Value;
                              //  _ktkp = kp.Where(p => p.MaKP == MaKPtongket).ToList().Count;
                                if (!string.IsNullOrEmpty(kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().MaICD))
                                {
                                    _maicd = kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().MaICD;
                                }
                                else
                                {
                                    _maicd = kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().MaICD2;
                                }
                                _chandoan = kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().ChanDoan + "/" + kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().BenhKhac;
                            }
                            var tenkhoa = _dataContext.KPhongs.Where(p => p.MaKP == MaKPtongket).ToList();
                            if (tenkhoa.Count > 0)
                                _kphong = tenkhoa.First().TenKP;
                            {
                                if (rv.Where(p => p.MaKP == MaKPtongket).Count() > 0)
                                {
                                    _ngayra = "- Ngày ra viện:";
                                    _ngayr = rv.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayRa.Value.Date;
                                }
                                else if (kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().NgayNghi != null)
                                {
                                    _ngayra = "- Ngày chuyển khoa:";
                                    _ngayr = kp.Where(p => p.MaKP == MaKPtongket).ToList().Last().NgayNghi.Value;
                                }

                                if (vv.Where(p => p.MaKP == MaKPtongket).ToList().Count > 0)
                                {


                                    _ngayvao = "- Ngày vào viện:";
                                    _ngayv = vv.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayVao.Value;

                                }
                                else
                                {
                                    _ngayvao = "- Ngày vào khoa:";
                                    if (kp.Where(p => p.MaKPdt == MaKPtongket).ToList().Count > 0)
                                    {
                                        if (kp.Where(p => p.MaKPdt == MaKPtongket).ToList().First().NgayNghi != null)
                                            _ngayv = kp.Where(p => p.MaKPdt == MaKPtongket).ToList().First().NgayNghi.Value;
                                        else if (kp.Where(p => p.MaKPdt == MaKPtongket).ToList().First().NgayKham != null)
                                            _ngayv = kp.Where(p => p.MaKPdt == MaKPtongket).ToList().First().NgayKham.Value;
                                    }
                                    else if (kp.Where(p => p.MaKP == MaKPtongket).ToList().Count > 0)
                                    {
                                        if (kp.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayNghi != null)
                                            _ngayv = kp.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayNghi.Value;
                                        else if (kp.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayKham != null)
                                            _ngayv = kp.Where(p => p.MaKP == MaKPtongket).ToList().First().NgayKham.Value;
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
                                rep13.KhoaPhong.Value = _kphong;// khoa phòng tổng kết
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
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _inPhieu();
        }

        public void loadAll()
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var lkp = _dataContext.KPhongs.ToList();
            var vaovien = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            lAll = new List<cl_chiphi>();
            rdThanhToan.SelectedIndex = 0;
            ckTrongDM.Checked = true;
            var qchidinh = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _mabn) join cd in _dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS select new { cd.IDCD, cls.MaKP }).ToList();
            var qdv = (from dv in _dataContext.DichVus
                       join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in _dataContext.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { dv.MaDV, dv.TenDV, n.TenNhom, n.STT, dv.PLoai }).ToList();

            lAll = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                    join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                    join kp in _dataContext.KPhongs
                     on dtct.MaKP equals kp.MaKP
                    //join ck in _dataContext.ChuyenKhoas on kp.MaCK equals ck.MaCK
                    select new cl_chiphi
                    {
                        MaKP = kp.MaKP,
                        NgayKe = dt.NgayKe,
                        IDCD = dtct.IDCD,
                        MaCK = kp.MaCK
                            //,TenCK = ck.TenCK
                        ,
                        KieuDon = dt.KieuDon == 0 ? "Hàng ngày" : (dt.KieuDon == 1 ? "Bổ sung" : (dt.KieuDon == 2 ? "Trả thuốc" : (dt.KieuDon == 3 ? "Lĩnh khoa" : (dt.KieuDon == 4 ? "Khoa trả thuốc" : (dt.KieuDon == 5 ? "Trực" : ""))))),
                        TenKP = kp.TenKP,
                        IDKB = dtct.IDKB,
                        MaDV = dtct.MaDV ?? 0,
                        IDDon = dt.IDDon,
                        IDDonct = dtct.IDDonct,
                        DonGia = dtct.DonGia,
                        TrongBH = dtct.TrongBH,
                        SoLuong = dtct.SoLuong,
                        ThanhTien = dtct.ThanhTien,
                        ThanhToan = dtct.ThanhToan,
                        DonVi = dtct.DonVi,
                        MaKPtongket = dtct.MaKPtk,
                        KhoXuat = dt.MaKXuat ?? 0
                    }).ToList();

            lAll = (from dv in qdv
                    join dt in lAll on dv.MaDV equals dt.MaDV
                    join ck in DungChung.Bien._lChuyenKhoa on dt.MaCK equals ck.MaCK
                    select new cl_chiphi { MaKP = dt.MaKP, //IDKB = dt.IDKB,
                        IDCD = dt.IDCD, TenCK = ck.ChuyenKhoa, TenKP = dt.TenKP, MaDV = dt.MaDV, IDDon = dt.IDDon, IDDonct = dt.IDDonct, DonGia = dt.DonGia, TrongBH = dt.TrongBH, SoLuong = dt.SoLuong, ThanhTien = dt.ThanhTien, ThanhToan = dt.ThanhToan, DonVi = dt.DonVi, DthuocKP = dt.IDDon + " - " + dt.TenKP, TenNhom = dv.TenNhom, TenDV = dv.TenDV, STT = dv.STT ?? 0, NgayKe = dt.NgayKe.Value.Date, PloaiDV = dv.PLoai ?? 0, MaKPtongket = dt.MaKPtongket, KhoXuat = dt.KhoXuat, KieuDon = dt.KieuDon }).ToList();
            if (load == 0)
            {
                foreach (cl_chiphi a in lAll)
                {                   
                    if (a.MaKPtongket <= 0)
                    {
                        var qkhoake = lkp.Where(p => p.MaKP == a.MaKP).FirstOrDefault();
                        DThuocct dtct = _dataContext.DThuoccts.Single(p => p.IDDonct == a.IDDonct);
                        if (qkhoake != null && qkhoake.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && vaovien != null)// add bằng khoa vào viện
                        {
                            a.MaKPtongket = vaovien.MaKP ?? 0;
                            dtct.MaKPtk = vaovien.MaKP ?? 0;
                        }
                        else if (a.PloaiDV == 2 && (a.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat || a.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat) && qkhoake.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang)
                        {
                            var chidinh = qchidinh.Where(p => p.IDCD == a.IDCD).FirstOrDefault();
                            if (chidinh != null)
                            {
                                a.MaKPtongket = chidinh.MaKP ?? 0;
                                dtct.MaKPtk = chidinh.MaKP ?? 0;
                            }
                        }
                        else // add bằng khoa kê
                        {
                            a.MaKPtongket = a.MaKP;
                            dtct.MaKPtk = a.MaKP;
                        }
                    }
                }
                _dataContext.SaveChanges();
            }
        }

        private void loadchitiet()
        {
            #region Tìm kiếm  đơn thuốc
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            //trong ngoài danh mục
            bool ngoaiBH = ckNgoaiDM.Checked;
            bool trongBH = ckTrongDM.Checked;
            bool khongTT = ckKhongThanhToan.Checked;

            //chi phí thanh tóa hoặc thu phí trực tiếp
            int thanhtoan = rdThanhToan.SelectedIndex;
            int makp = -1;
            int makpTk = -1;
            if (lupKhoKe.EditValue != null)
                makp = Convert.ToInt32(lupKhoKe.EditValue);
            if (lupKPTongKet.EditValue != null)
                makpTk = Convert.ToInt32(lupKPTongKet.EditValue);

            _lchiphi = (from dt in lAll
                        where ((ngoaiBH && dt.TrongBH == 0) || (trongBH && dt.TrongBH == 1) || (khongTT && dt.TrongBH == 2))
                        where (thanhtoan == 2 || dt.ThanhToan == thanhtoan)
                        where (makp == 0 || dt.MaKP == makp)
                        where (makpTk == 0 || dt.MaKPtongket == makpTk)
                        select new cl_chiphi
                        {
                            ThanhToan = dt.ThanhToan,
                            PloaiDV = dt.PloaiDV,
                            TenDV = dt.TenDV,
                            DonGia = dt.DonGia,
                            DonVi = dt.DonVi,
                            MaDV = dt.MaDV,
                            MaKP = dt.MaKP,
                            TenKP = dt.TenKP,
                            TenNhom = dt.TenNhom,
                            STT = dt.STT,
                            TrongBH = dt.TrongBH,
                            SoLuong = dt.SoLuong,
                            ThanhTien = dt.ThanhTien,
                            DthuocKP = dt.DthuocKP,
                            IDDonct = dt.IDDonct,
                            NgayKe = dt.NgayKe.Value,
                            MaKPtongket = dt.MaKPtongket,
                            IDKB = dt.IDKB,
                            KieuDon = dt.KieuDon,
                            KhoXuat = dt.KhoXuat,
                            IDDon = dt.IDDon,
                        }).OrderBy(p => p.NgayKe).ToList();


            _lThuoc = (from dt in _lchiphi
                       where dt.PloaiDV == 1
                       group new { dt } by new { dt.MaKPtongket, dt.KhoXuat, dt.MaKP, dt.TenKP,  dt.KieuDon } into kq
                       select new cl_chiphi
                       {
                           MaKP = kq.Key.MaKP,
                           TenKP = kq.Key.TenKP,
                           lIDdonct = string.Join(",",kq.Select(p=>p.dt.IDDonct.ToString())),                        
                           MaKPtongket = kq.Key.MaKPtongket,                          
                           KhoXuat = kq.Key.KhoXuat,
                           KieuDon = kq.Key.KieuDon
                       }).OrderBy(p => p.NgayKe).ToList();
            grcThuoc.DataSource = _lThuoc;

            #endregion

            #region Tìm kiếm dịch vụ
            _lDichVu = (from dt in _lchiphi
                        where dt.PloaiDV == 2
                        select new cl_chiphi
                        {
                            ThanhToan = dt.ThanhToan,
                            TenDV = dt.TenDV,
                            DonGia = dt.DonGia,
                            DonVi = dt.DonVi,
                            MaDV = dt.MaDV,
                            MaKP = dt.MaKP,
                            TenKP = dt.TenKP,
                            TenNhom = dt.TenNhom,
                            STT = dt.STT,
                            TrongBH = dt.TrongBH,
                            SoLuong = dt.SoLuong,
                            ThanhTien = dt.ThanhTien,
                            DthuocKP = dt.DthuocKP,
                            IDDonct = dt.IDDonct,
                            NgayKe = dt.NgayKe.Value,
                            MaKPtongket = dt.MaKPtongket,
                            IDKB = dt.IDKB,
                            KieuDon = dt.KieuDon,
                            KhoXuat = dt.KhoXuat
                        }).OrderBy(p => p.NgayKe).ToList();
            grcThanhToan.DataSource = _lDichVu;

            #endregion
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            for (int i = 0; i < grvThanhToan.RowCount; i++)
            {
                if (grvThanhToan.GetRowCellValue(i, colTenKPTongKet) != null)
                {
                    int maKPtongket = Convert.ToInt32(grvThanhToan.GetRowCellValue(i, colTenKPTongKet));
                    int idonct = Convert.ToInt32(grvThanhToan.GetRowCellValue(i, colIDDonct));
                    int makpTKBF = lAll.Single(p => p.IDDonct == idonct).MaKPtongket;// mã khoa phòng tổng kết từ đầu
                    if (maKPtongket != makpTKBF)
                    {
                        DThuocct dtct = data.DThuoccts.Single(p => p.IDDonct == idonct);
                        dtct.MaKPtk = maKPtongket;
                    }
                }
            }

            for (int i = 0; i < grvThuoc.RowCount; i++)
            {
                if (grvThuoc.GetRowCellValue(i, colKPTKD) != null)
                {
                    int maKPtongket = Convert.ToInt32(grvThuoc.GetRowCellValue(i, colKPTKD));
                    string lidonct = grvThuoc.GetRowCellValue(i, collIDDonDct).ToString();
                    var lmakpTKBF = lAll.Where(p => lidonct.Contains(p.IDDonct.ToString())).ToList();//.MaKPtongket;// mã khoa phòng tổng kết từ đầu
                    foreach (var b in lmakpTKBF)
                    {

                        if (maKPtongket != b.MaKPtongket)
                        {
                            DThuocct dtct = data.DThuoccts.Where(p => p.IDDonct == b.IDDonct).FirstOrDefault();
                            if (dtct != null)
                                dtct.MaKPtk = maKPtongket;
                        }
                    }
                }
            }
            int count = data.SaveChanges();
            MessageBox.Show("Đã cập nhật " + count + " bản ghi ");
            loadAll();
            loadchitiet();
        }

        private void rdTrongDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load > 0)
            {
                loadchitiet();
            }
        }

        private void rdThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load > 0)
            {
                loadchitiet();
            }
        }

        private void lupKhoKe_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoKe.Text != "Tất cả")
                lupKPTongKet.EditValue = lupKPTongKet.Properties.GetKeyValueByDisplayText("Tất cả");
            if (load > 0)
            {
                loadchitiet();
            }
        }



        private void rddichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load > 0)
            {
                loadchitiet();
            }

        }

        private void lupKPTongKet_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void ckNgoaiDM_CheckStateChanged(object sender, EventArgs e)
        {


            loadchitiet();

        }

        private void ckTrongDM_CheckStateChanged(object sender, EventArgs e)
        {

            loadchitiet();

        }

        private void ckKhongThanhToan_CheckStateChanged(object sender, EventArgs e)
        {
            loadchitiet();
        }

        int rowHandleThuoc = 0;
        int rowHandleDichVu = 0;
        private void lkXemct_Click(object sender, EventArgs e)
        {

        }

        private void grvThuoc_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            rowHandleThuoc = e.RowHandle;
            if (e.Column.Name == "colXemCT" && rowHandleThuoc >= 0)
            {
                string lIDdonct = "";

                if (grvThuoc.GetRowCellValue(rowHandleThuoc, collIDDonDct) != null)
                {
                    lIDdonct = grvThuoc.GetRowCellValue(rowHandleThuoc, collIDDonDct).ToString();
                    List<cl_chiphi> lcp = _lchiphi.Where(p => lIDdonct.Contains(p.IDDonct.ToString())).ToList();
                    QLBV.FormNhap.frm_Mau13_ChitietThuoc frm = new QLBV.FormNhap.frm_Mau13_ChitietThuoc(lcp, dskp);
                    frm.ShowDialog();
                }

            }

        }

        private void grvThuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int row = grvThuoc.FocusedRowHandle;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            if (grvThuoc.GetRowCellValue(row, colKPTKD) != null)
            {
                int maKPtongket = Convert.ToInt32(grvThuoc.GetRowCellValue(row, colKPTKD));
                string lIDdonct = grvThuoc.GetRowCellValue(row, collIDDonDct).ToString();
                var lmakpTKBF = _lchiphi.Where(p =>lIDdonct.Contains(p.IDDonct.ToString())).ToList();//.MaKPtongket;// mã khoa phòng tổng kết từ đầu
                foreach (var b in lmakpTKBF)
                {
                    if (maKPtongket != b.MaKPtongket)
                    {
                        DThuocct dtct = data.DThuoccts.Where(p => p.IDDonct == b.IDDonct).FirstOrDefault();
                        if (dtct != null)
                            dtct.MaKPtk = maKPtongket;
                    }
                }
            }

            data.SaveChanges();
            loadAll();
            loadchitiet();
        }

        private void grvThanhToan_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int row = grvThanhToan.FocusedRowHandle;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (grvThanhToan.GetRowCellValue(row, colTenKPTongKet) != null)
            {
                int maKPtongket = Convert.ToInt32(grvThanhToan.GetRowCellValue(row, colTenKPTongKet));
                int idonct = Convert.ToInt32(grvThanhToan.GetRowCellValue(row, colIDDonct));
                int makpTKBF = _lchiphi.Single(p => p.IDDonct == idonct).MaKPtongket;// mã khoa phòng tổng kết từ đầu
                if (maKPtongket != makpTKBF)
                {
                    DThuocct dtct = data.DThuoccts.Single(p => p.IDDonct == idonct);
                    dtct.MaKPtk = maKPtongket;
                }
            }

            data.SaveChanges();
            loadAll();
            loadchitiet();
        }

        private void grvThanhToan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            rowHandleDichVu = e.RowHandle;
        }

        private void lupMaKPTKD_EditValueChanged(object sender, EventArgs e)
        {
            //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //if (grvThanhToan.GetRowCellValue(rowHandleThuoc, colTenKPTongKet) != null)
            //{
            //    int maKPtongket = Convert.ToInt32(grvThanhToan.GetRowCellValue(rowHandleThuoc, colTenKPTongKet));
            //    int idonct = Convert.ToInt32(grvThanhToan.GetRowCellValue(rowHandleThuoc, colIDDonct));
            //    int makpTKBF = lAll.Single(p => p.IDDonct == idonct).MaKPtongket;// mã khoa phòng tổng kết từ đầu
            //    if (maKPtongket != makpTKBF)
            //    {
            //        DThuocct dtct = data.DThuoccts.Single(p => p.IDDonct == idonct);
            //        dtct.MaKPtk = maKPtongket;
            //    }
            //}

            //data.SaveChanges();
            ////loadAll();
            ////loadchitiet();
        }

        private void lupKhoaTK_DV_EditValueChanged(object sender, EventArgs e)
        {

            //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //if (grvThanhToan.GetRowCellValue(rowHandleDichVu, colTenKPTongKet) != null)
            //{

            //   // int maKPtongket = Convert.ToInt32((grvThanhToan.GetRowCellValue(rowHandleDichVu, colTenKPTongKet)));
            //    int maKPtongket = Convert.ToInt32(lupKhoaTK_DV.GetDataSourceValue("MaKP", rowHandleDichVu));//(grvThanhToan.GetRowCellValue(rowHandleDichVu, colTenKPTongKet));
            //    int idonct = Convert.ToInt32(grvThanhToan.GetRowCellValue(rowHandleDichVu, colIDDonct));
            //    int makpTKBF = lAll.Single(p => p.IDDonct == idonct).MaKPtongket;// mã khoa phòng tổng kết từ đầu
            //    if (maKPtongket != makpTKBF)
            //    {
            //        DThuocct dtct = data.DThuoccts.Single(p => p.IDDonct == idonct);
            //        dtct.MaKPtk = maKPtongket;
            //    }
            //}
            //data.SaveChanges();
            ////loadAll();
            ////loadchitiet();
        }

        private void lupKhoaTongKet_TK_EditValueChanged(object sender, EventArgs e)
        {
            if (load > 0)
            {
                loadchitiet();
            }
        }

        private void lupKPTongKet_EditValueChanged_1(object sender, EventArgs e)
        {
            if (lupKPTongKet.Text != "Tất cả")
                lupKhoKe.EditValue = lupKhoKe.Properties.GetKeyValueByDisplayText("Tất cả");
            if (load > 0)
            {
                loadchitiet();
            }
        }

        private void frm_CapNhatKPtongKet_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvThuoc.FocusedColumn = colKhoXuat;
            grvThanhToan.FocusedColumn = colSoLuong;
            grvThanhToan_ValidateRow(null, null);
            grvThuoc_ValidateRow(null, null);
        }







    }
}