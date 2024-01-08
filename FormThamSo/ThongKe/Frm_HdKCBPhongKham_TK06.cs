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
    public partial class Frm_HdKCBPhongKham_TK06 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_HdKCBPhongKham_TK06()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        class DSCK
        {
            private string TenCK;
            private int? TongKhamChung;
            private int? TongKhamBHYT;
            private int? CapCuu;
            private int? XetNghiem;
            private int? XQuang;
            private int? GioiThieu;
            public int MaCK { set; get; }
            public string tenck
            { set { TenCK = value; } get { return TenCK; } }
            public int? tongkhamchung
            { set { TongKhamChung = value; } get { return TongKhamChung; } }
            public int? tongkhambhyt
            { set { TongKhamBHYT = value; } get { return TongKhamBHYT; } }
            public int? capcuu
            { set { CapCuu = value; } get { return CapCuu; } }
            public int? xetnghiem
            { set { XetNghiem = value; } get { return XetNghiem; } }
            public int? xquang
            { set { XQuang = value; } get { return XQuang; } }
            public int? gioithieu
            { set { GioiThieu = value; } get { return GioiThieu; } }


            public int MaBNhan { get; set; }

            public int IDKB { get; set; }
        }
        List<DSCK> _DSCK = new List<DSCK>();
        private void Frm_HdKCBPhongKham_TK06_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();

            if (kt())
            {
                _DSCK.Clear();
                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);

                BaoCao.Rep_HdKCBPhongKham_TK06 rep = new BaoCao.Rep_HdKCBPhongKham_TK06();
                rep.TuNgayDenNgay.Value = "Từ ngày " + lupNgaytu.Text + " Đến ngày " + lupNgayden.Text;
                if (rdLoai.SelectedIndex == 1)
                {
                    #region in theo số lượt
                    var all = (from kb in dataContext.BNKBs.Where(p => ((rdNgay.SelectedIndex == 0) ? (p.NgayKham >= tungay && p.NgayKham <= denngay) : true) )
                               join bn in dataContext.BenhNhans.Where(p => (rdNgay.SelectedIndex == 1) ? (p.NNhap >= tungay && p.NNhap <= denngay) : true) on kb.MaBNhan equals bn.MaBNhan
                               join ck in dataContext.ChuyenKhoas on kb.MaCK equals ck.MaCK

                               select new
                               {
                                   ck.MaCK,
                                   ck.TenCK,
                                   bn.MaBNhan,
                                   bn.NoiTru,
                                   bn.Tuoi,
                                   bn.CapCuu,
                                   bn.DTuong,
                                   bn.SThe,
                                   kb.NgayKham,
                                   kb.IDKB,
                                   kb.PhuongAn
                               }).ToList();
                  

                    var qdthuoc = (from tnhom in dataContext.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG.Contains("XN"))
                                   join dv in dataContext.DichVus on tnhom.IdTieuNhom equals dv.IdTieuNhom
                                   join dtct in dataContext.DThuoccts on dv.MaDV equals dtct.MaDV
                                   join dt in dataContext.DThuocs on dtct.IDDon equals dt.IDDon
                                   join kb in dataContext.BNKBs on dtct.IDKB equals kb.IDKB
                                   group new { dt, tnhom, kb } by new { dt.MaBNhan, dt.MaKP , kb.MaCK} into kq
                                   select new
                                   {
                                       kq.Key.MaBNhan,
                                       kq.Key.MaCK,
                                       xquang = kq.Where(p => p.tnhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() ,
                                       xetnghiem = kq.Where(p => p.tnhom.TenRG.Contains("XN")).Count()
                                   }).ToList();




                    var q1 = (from kb in all
                              join dt in qdthuoc on new { kb.MaBNhan, kb.MaCK } equals new {MaBNhan = dt.MaBNhan ??0, dt.MaCK} into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new { MaBNhan = kb.MaBNhan, kb.NoiTru, kb.SThe, kb.Tuoi, CapCuu = kb.CapCuu, kb.DTuong, kb.PhuongAn, tenck = kb.TenCK, kb.MaCK, IDKB = kb.IDKB, xetnghiem = (kq1 == null ? 0 : kq1.xetnghiem), xquang = (kq1 == null ? 0 : kq1.xquang) }).ToList();

                    

                    var bc = (from kb in q1
                              group new { kb } by new { kb.MaCK, kb.tenck } into kq
                              select new DSCK { MaCK = kq.Key.MaCK, tenck = kq.Key.tenck, capcuu = kq.Where(p => p.kb.CapCuu == 1).Count(), tongkhamchung = kq.Count(), tongkhambhyt = kq.Where(p => p.kb.DTuong == "BHYT").Count(), gioithieu = kq.Where(p => p.kb.PhuongAn == 2).Count(), xetnghiem = kq.Sum(p=>p.kb.xetnghiem), xquang = kq.Sum(p=>p.kb.xquang)}).ToList();
                   
                    //var q2 = (from kb in all
                    //          join bn in allBN on kb.IDKB equals bn.IDKB
                    //          select kb).ToList();

                    if (all.Count() > 0)
                    {
                        rep.BHYT.Value = all.Where(p => p.DTuong == "BHYT").Count();
                        rep.BHHN.Value = all.Where(p => p.SThe != null && p.SThe.Length > 2 && p.SThe.Substring(0, 2) == "HN").Count();
                        rep.NoiTr.Value = all.Where(p => p.NoiTru == 1).Count();
                        rep.NgTru.Value = all.Where(p => p.NoiTru == 0).Count();
                        rep.TE6.Value = all.Where(p => p.Tuoi < 6).Count();
                        rep.NL60.Value = all.Where(p => p.Tuoi > 60).Count();
                    }
                    rep.DataSource = bc.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                else
                {
                    #region in theo số người
                    var all = (from kb in dataContext.BNKBs.Where(p => ((rdNgay.SelectedIndex == 0) ? (p.NgayKham >= tungay && p.NgayKham <= denngay) : true))
                               join bn in dataContext.BenhNhans.Where(p => (rdNgay.SelectedIndex == 1) ? (p.NNhap >= tungay && p.NNhap <= denngay) : true) on kb.MaBNhan equals bn.MaBNhan
                               join ck in dataContext.ChuyenKhoas on kb.MaCK equals ck.MaCK

                               select new
                               {
                                   ck.MaCK,
                                   ck.TenCK,
                                   bn.NNhap,
                                   bn.MaBNhan,
                                   bn.NoiTru,
                                   bn.Tuoi,
                                   bn.CapCuu,
                                   bn.DTuong,
                                   bn.SThe,
                                   kb.NgayKham,
                                   kb.IDKB,
                                   kb.PhuongAn
                               }).OrderBy(p=>p.MaBNhan).ToList();
                 //   _DSCK = (from a in all group a by new { a.MaCK, a.TenCK } into kq select new DSCK { MaCK = kq.Key.MaCK, tenck = kq.Key.TenCK }).OrderBy(p => p.tenck).ToList();

                    // Lấy IDKB cuối cùng đang điều trị trong thời gian tìm kiếm
                    var allBN = (from a in all group a by new { a.MaBNhan } into kq select new DSCK { MaBNhan = kq.Key.MaBNhan, IDKB = kq.Where(p => p.NgayKham <= denngay).Max(p => p.IDKB) }).ToList();

                    List<int> _lbn = allBN.Select(p => p.MaBNhan).ToList();
                    //Lấy các thông tin theo IDKB cuối cùng


                    var qdthuoc = (from tnhom in dataContext.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG.Contains("XN")) 
                                     join dv in dataContext.DichVus on tnhom.IdTieuNhom equals dv.IdTieuNhom
                                     join dtct in dataContext.DThuoccts on dv.MaDV equals dtct.MaDV
                                     join dt in dataContext.DThuocs on dtct.IDDon equals dt.IDDon
                                     group new{dt, tnhom} by new {dt.MaBNhan} into kq
                                     select new { kq.Key.MaBNhan,
                                         xquang = (kq.Where(p=>p.tnhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() > 0) ? 1 :0  ,
                                                  xetnghiem = (kq.Where(p => p.tnhom.TenRG .Contains("XN")).Count() > 0) ? 1 : 0
                                     }).ToList();

                    var q1 = (from kb in allBN
                              join dt in qdthuoc on kb.MaBNhan equals dt.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new DSCK {MaBNhan = kb.MaBNhan, IDKB = kb.IDKB, xetnghiem = (kq1 == null ? 0 : kq1.xetnghiem), xquang = (kq1 == null ? 0 : kq1.xquang)  }).ToList();



                    var q2 = (from kb in all
                              join bn in q1 on kb.IDKB equals bn.IDKB
                              select new { kb.MaBNhan, kb.IDKB, kb.CapCuu, kb.DTuong, kb.MaCK, kb.NgayKham, kb.NoiTru, kb.PhuongAn, kb.SThe, kb.TenCK, kb.Tuoi, bn.xquang, bn.xetnghiem}).ToList();

                    //var bc = (from kb in all
                    //          join bn in q1 on kb.IDKB equals bn.IDKB                             
                    //          group new { kb, bn } by new { kb.MaCK, kb.TenCK } into kq
                    //          select new DSCK { MaCK = kq.Key.MaCK, tenck = kq.Key.TenCK, capcuu = kq.Where(p => p.kb.CapCuu == 1).Count(), tongkhamchung = kq.Count(), tongkhambhyt = kq.Where(p => p.kb.DTuong == "BHYT").Count(), gioithieu = kq.Where(p => p.kb.PhuongAn == 2).Count(), xetnghiem = kq.Sum(p => p.bn.xetnghiem), xquang = kq.Sum(p => p.bn.xquang) }).ToList();

                    var bc = (from kb in q2                              
                              group new { kb} by new { kb.MaCK, kb.TenCK } into kq
                              select new DSCK { MaCK = kq.Key.MaCK, tenck = kq.Key.TenCK, capcuu = kq.Where(p => p.kb.CapCuu == 1).Count(), tongkhamchung = kq.Count(), tongkhambhyt = kq.Where(p => p.kb.DTuong == "BHYT").Count(), gioithieu = kq.Where(p => p.kb.PhuongAn == 2).Count(), xetnghiem = kq.Sum(p => p.kb.xetnghiem), xquang = kq.Sum(p => p.kb.xquang) }).ToList();

                    //var q2 = (from kb in all
                    //          join bn in allBN on kb.IDKB equals bn.IDKB
                    //          select kb).ToList();

                    if (q2.Count() > 0)
                    {
                        rep.BHYT.Value = q2.Where(p => p.DTuong == "BHYT").Count();
                        rep.BHHN.Value = q2.Where(p => p.SThe != null && p.SThe.Length > 2 && p.SThe.Substring(0, 2) == "HN").Count();
                        rep.NoiTr.Value = q2.Where(p => p.NoiTru == 1).Count();
                        rep.NgTru.Value = q2.Where(p => p.NoiTru == 0).Count();
                        rep.TE6.Value = q2.Where(p => p.Tuoi < 6).Count();
                        rep.NL60.Value = q2.Where(p => p.Tuoi > 60).Count();
                    }
                    rep.DataSource = bc.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}