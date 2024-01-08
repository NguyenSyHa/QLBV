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
    public partial class frm_BCThuVPTheoCa : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThuVPTheoCa()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCThuVPTheoCa_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupNgayDen.DateTime = DateTime.Now;
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in _data.CanBoes
                       join kp in _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan || p.PLoai ==  DungChung.Bien.st_PhanLoaiKP.Admin) on cb.MaKP equals kp.MaKP
                       select cb).ToList();
            _lcanbo.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupCanBoTT.Properties.DataSource = _lcanbo;
            lupCanBoTT.EditValue = lupCanBoTT.Properties.GetKeyValueByDisplayText("Tất cả");

            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = _data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");
            rdNoiTru.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupNgayDen.DateTime);
            int noingoaitru = 2;
            noingoaitru = rdNoiTru.SelectedIndex;

            var kphong = _data.KPhongs.ToList();
            string macb = "";
            if(lupCanBoTT.EditValue != null)
            {
                macb = lupCanBoTT.EditValue.ToString();
            }
            int iddtbn = 100;
            if(lupDoituong.EditValue != null)
            {
                iddtbn = Convert.ToInt32(lupDoituong.EditValue);
            }
            // tạm ứng, chi tạm ứng + thu thẳng + thanh toán
            var qdv = (from dv in _data.DichVus join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom select new {dv.MaDV, Kham = tn.IDNhom == 13 ? true: false, DichVu = dv.PLoai == 2 && tn.IDNhom != 13 ? true : false, Thuoc = dv.PLoai == 1 ? true : false}).ToList();
            var qtamung =(from tu in _data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay && (macb == "" || p.MaCB == macb))
                              join bn in _data.BenhNhans.Where(p=>(iddtbn == 100 || p.IDDTBN == iddtbn) && (noingoaitru == 2 || p.NoiTru == noingoaitru)) on tu.MaBNhan equals bn.MaBNhan select new {bn.MaBNhan, bn.TenBNhan, bn.MaKP, tu.PhanLoai, tu.SoTien, tu.IDTamUng, tu.TienChenh}).ToList();
            List<int> lmabn = new List<int>();
            DateTime ngayvao = tungay.AddMonths(-3);
            var qvv = _data.VaoViens.Where(p=>p.NgayVao >= ngayvao).ToList();
            var qbn = (from bn in qtamung select new { bn.MaBNhan, bn.TenBNhan , bn.MaKP}).Distinct().ToList();

            if (qtamung.Count > 0)
                lmabn = qtamung.Where(p=>p.PhanLoai == 1 || p.PhanLoai == 2).Select(p => p.MaBNhan).Distinct().ToList();


            var qthuthang = (from tu in _data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay && (macb == "" || p.MaCB == macb) && p.PhanLoai == 3)
                             join bn in _data.BenhNhans.Where(p => (iddtbn == 100 || p.IDDTBN == iddtbn) && (noingoaitru == 2 || p.NoiTru == noingoaitru)) on tu.MaBNhan equals bn.MaBNhan
                             join tuct in _data.TamUngcts.Where(p=>p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                             select new { tu.MaBNhan, tuct.MaDV, tuct.SoTien, tuct.ThanhTien, tuct.TienBN }).ToList();
            var qvp = (from vp in _data.VienPhis.Where(p => lmabn.Contains(p.MaBNhan ?? 0) && (macb == "" || p.MaCB == macb))
                       join vpct in _data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                          select new { vp.MaBNhan , vpct.MaDV, vpct.ThanhTien, vpct.TienBN, vpct.TienBH, vpct.ThanhToan, vpct.TrongBH}).ToList();


            var qthuthang2 = (from tu in qthuthang join dv in qdv on tu.MaDV equals dv.MaDV select new {tu.MaBNhan,  Kham = dv.Kham ? tu.SoTien : 0, DichVu = dv.DichVu ? tu.SoTien : 0, Thuoc = dv.Thuoc ? tu.SoTien : 0, Tong = tu.SoTien }).ToList();
            var qthuthang3 = (from tu in qthuthang2 group tu by new { tu.MaBNhan} into kq select new BC {MaBNhan = kq.Key.MaBNhan ??0, Kham = kq.Sum(p=>p.Kham), DichVu = kq.Sum(p=>p.DichVu), Thuoc = kq.Sum(p=>p.Thuoc), Tong = kq.Sum(p=>p.Tong)}).ToList();

             var qTamUng1 = (from tu in qtamung.Where(p => p.PhanLoai == 0 || p.PhanLoai == 4 || p.PhanLoai== 1 || p.PhanLoai == 2) select new BC { MaBNhan = tu.MaBNhan,TamUng =  tu.PhanLoai == 0 ? tu.SoTien??0 : 0 , TraLai =  tu.PhanLoai == 2 ? tu.TienChenh : (tu.PhanLoai == 4 ? tu.SoTien : 0), ThuThem = tu.PhanLoai == 1 ? tu.TienChenh : 0 }).ToList();
            var qTamUng2 = (from tu in qTamUng1 group tu by new { tu.MaBNhan } into kq select new BC { MaBNhan = kq.Key.MaBNhan,  TamUng =  kq.Sum(p => p.TamUng) , TraLai =  kq.Sum(p=>p.TraLai), ThuThem = kq.Sum(p=>p.ThuThem) }).ToList();

            var qvp2 = (from vp in qvp join dv in qdv on vp.MaDV equals dv.MaDV group new { vp, dv } by new {  vp.MaBNhan } into kq select new BC {MaBNhan = kq.Key.MaBNhan ?? 0, Kham = kq.Where(p => p.vp.TrongBH == 0 && p.dv.Kham).Sum(p => p.vp.TienBN), DichVu = kq.Where(p =>p.vp.TrongBH == 0 && p.dv.DichVu).Sum(p => p.vp.TienBN), Thuoc = kq.Where(p =>p.vp.TrongBH == 0 && p.dv.Thuoc).Sum(p => p.vp.TienBN), VienPhi = kq.Where(p=>p.vp.TrongBH != 0).Sum(p=>p.vp.TienBN)}).ToList();

            List<BC> _list = new List<BC>();
            _list.AddRange(qthuthang3);
            _list.AddRange(qTamUng2);
            _list.AddRange(qvp2);

            _list = (from l in _list group l by new {  l.MaBNhan } into kq select new BC { MaBNhan = kq.Key.MaBNhan, Thuoc = kq.Sum(p => p.Thuoc), DichVu = kq.Sum(p => p.DichVu), Kham = kq.Sum(p => p.Kham), TamUng = kq.Sum(p => p.TamUng), VienPhi = kq.Sum(p => p.VienPhi), Tong = kq.Sum(p => p.Tong), ThuThem = kq.Sum(p=>p.ThuThem), TraLai = kq.Sum(p=>p.TraLai) }).ToList();
            _list = (from l in _list
                     join bn in qbn on l.MaBNhan equals bn.MaBNhan
                     join kp in kphong on bn.MaKP equals kp.MaKP                     
                     select new BC { MaBNhan = l.MaBNhan, MaKP = kp.MaKP, Kham = l.Kham, DichVu = l.DichVu, Thuoc = l.Thuoc, TamUng = l.TamUng, VienPhi = l.VienPhi, Tong = l.Tong + l.TamUng +l.ThuThem - l.TraLai ?? 0, TenBNhan = bn.TenBNhan, KP = kp.TenKP, TraLai = l.TraLai, ThuThem = l.ThuThem }).Where(p=>p.Tong>0).ToList();
            _list = (from l in _list join vv in qvv on l.MaBNhan equals vv.MaBNhan into kq from kq1 in kq.DefaultIfEmpty() select new BC {MaBNhan = l.MaBNhan, MaKP = l.MaKP, Kham = l.Kham, DichVu = l.DichVu, Thuoc = l.Thuoc, TamUng = l.TamUng, VienPhi = l.VienPhi, Tong = l.Tong , TenBNhan = l.TenBNhan, KP = l.KP, TraLai = l.TraLai, ThuThem = l.ThuThem, SoVV = kq1 == null?"" : kq1.SoVV}).ToList();
            frmIn frm = new frmIn();
            BaoCao.rep_BCThuVPTheoCa rep = new BaoCao.rep_BCThuVPTheoCa();
          //  rep.rowCount.Value = _list.Count;
            rep.celNgay.Text = "TỪ NGÀY: " + tungay.ToShortDateString() + " ĐẾN NGÀY " + denngay.ToShortDateString() ;
            rep.celNhanVien.Text = "NHÂN VIÊN: " + lupCanBoTT.Text.ToUpper();
            if(_list.Count > 0)
            {
                double tongtien = _list.Sum(p => p.Tong);
                rep.celTongSoTien.Text = string.Format(DungChung.Bien.FormatString[1], Math.Round(tongtien, 0));
                rep.celVietBangChu.Text = DungChung.Ham.DocTienBangChu(Math.Round( tongtien,0), " đồng");
            }
            rep.celCa.Text = "CA" + txtCa.Text;
            rep.DataSource = _list;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        private class BC
        {
            public int MaBNhan { set; get; }
            public int MaKP { set; get; }
            public double Kham { set; get; }
            public double DichVu { set; get; }
            public double Thuoc { set; get; }
            public double TamUng { set; get; }
            public double VienPhi { set; get; }
            public double Tong { set; get; }

            public string TenBNhan { get; set; }

            public string KP { get; set; }

            public double? TraLai { get; set; }

            public double ThuThem { get; set; }

            public string SoVV { get; set; }
        }
    }
} 