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
    public partial class frm_BCXetNghiemHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCXetNghiemHangNgay()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCXetNghiemHangNgay_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DungChung.Ham.NgayTu(DateTime.Now);
            lupDenNgay.DateTime = DungChung.Ham.NgayDen(DateTime.Now);
            List<DTBN> ldtbn = new List<DTBN>();
            ldtbn = data.DTBNs.Where(p => p.Status == 1).ToList();
            ldtbn.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cboDTuong.Properties.DataSource = ldtbn;
            cboDTuong.EditValue = cboDTuong.Properties.GetKeyValueByDisplayText("Tất cả");
            radioGroup1.SelectedIndex = 2;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = lupTuNgay.DateTime;
            DateTime denngay = lupDenNgay.DateTime;
            int dtbn = 99;
            if (cboDTuong.EditValue != null)
                dtbn = Convert.ToInt32(cboDTuong.EditValue);
            int noitru = radioGroup1.SelectedIndex;

            var qcls = (from cls in data.CLS.Where(p => p.NgayTH != null && p.NgayTH >= tungay && p.NgayTH <= denngay)
                        join bn in data.BenhNhans.Where(p=> (noitru == 2 || p.NoiTru == noitru) && (dtbn == 99 || p.IDDTBN == dtbn)) on cls.MaBNhan equals bn.MaBNhan
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        select new { bn.MaBNhan, bn.NoiTru, bn.IDDTBN, cls.MaKP, cd.MaDV, cd.SoPhim , cls.IdCLS}).ToList();
            var qkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh).OrderByDescending(p => p.PLoai).ToList();
            var qdv = (from tn in data.TieuNhomDVs.Where(p=>p.IDNhom == 1) join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom select new { dv.MaDV, dv.TenDV,tn.IdTieuNhom, tn.TenRG, tn.TenTN }).ToList();
            var qkq1 = (from cls in qcls
                        join dv in qdv on cls.MaDV equals dv.MaDV
                        select new
                        {
                            cls.MaDV,
                            cls.MaBNhan,
                            cls.MaKP,
                            cls.IdCLS,
                            SinhHoa = ((dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo) && dv.TenTN.ToLower() != "xét nghiệm miễn dịch" && !dv.TenDV.ToLower().Contains("điện giải")) ? 1 : 0,
                            HuyetHoc = dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc ? 1 : 0,
                            NuocTieu = dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu ? 1 : 0,
                            ViSinh = dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh ? 1 : 0,
                            MienDich = (dv.TenTN.ToLower() == "xét nghiệm miễn dịch") ? 1 : 0,
                            DongMau = dv.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDongCamMau ? 1 : 0,
                            DienGiai = dv.TenDV.ToLower().Contains("điện giải") ? 1 : 0,
                            SoTieuBan = (cls.SoPhim == null || cls.SoPhim == 0) ? 1 : cls.SoPhim.Value
                        }).Where(p=>p.SinhHoa >0 || p.HuyetHoc >0 || p.NuocTieu >0 || p.ViSinh>0 || p.MienDich > 0 || p.DongMau>0 || p.DienGiai >0).ToList();
            var qkq2 = (from cls in qkq1
                        group cls by new { cls.MaKP, cls.MaBNhan, cls.IdCLS } into kq
                        select new
                        {
                            kq.Key.MaKP,
                            kq.Key.MaBNhan,
                            SinhHoa = kq.Sum(p => p.SinhHoa) > 0 ? 1 :0,
                            HuyetHoc = kq.Sum(p => p.HuyetHoc) > 0 ? 1 : 0,
                            NuocTieu = kq.Sum(p => p.NuocTieu) > 0 ? 1 : 0,
                            ViSinh = kq.Sum(p => p.ViSinh) > 0 ? 1 : 0,
                            MienDich = kq.Sum(p => p.MienDich) > 0 ? 1 : 0,
                            DongMau = kq.Sum(p => p.DongMau) > 0 ? 1 : 0,
                            DienGiai = kq.Sum(p => p.DienGiai) > 0 ? 1 : 0,
                            SoTieuBan = kq.Sum(p => p.SoTieuBan),
                        }).ToList();

            var qkq3 = (from cls in qkq2 group cls by new { cls.MaKP, cls.MaBNhan } into kq select new {
            kq.Key.MaKP,
            kq.Key.MaBNhan,
            SinhHoa = kq.Sum(p => p.SinhHoa),
            HuyetHoc = kq.Sum(p => p.HuyetHoc),
            NuocTieu = kq.Sum(p => p.NuocTieu),
            ViSinh = kq.Sum(p => p.ViSinh),
            MienDich = kq.Sum(p => p.MienDich),
            DongMau = kq.Sum(p => p.DongMau),
            DienGiai = kq.Sum(p => p.DienGiai),
            SoTieuBan = kq.Sum(p => p.SoTieuBan),
            }).ToList();
            List<BC> lbc = new List<BC>();

            foreach(var kp in qkp)
            {
                var qkq4 = qkq3.Where(p => p.MaKP == kp.MaKP).ToList();
                if(qkq4.Count >0)
                {
                BC moi = new BC();
                moi.MaKP = kp.MaKP;
                moi.TenKP = kp.TenKP;
                moi.SoBN = qkq4.Count();
                moi.SinhHoa = qkq4.Sum(p => p.SinhHoa);
                moi.HuyetHoc = qkq4.Sum(p => p.HuyetHoc);
                moi.NuocTieu = qkq4.Sum(p => p.NuocTieu);
                moi.ViSinh = qkq4.Sum(p => p.ViSinh);
                moi.MienDich = qkq4.Sum(p => p.MienDich);
                moi.DongMau = qkq4.Sum(p => p.DongMau);
                moi.DienGiai = qkq4.Sum(p => p.DienGiai);
                moi.SoTieuBan = qkq4.Sum(p => p.SoTieuBan);
                lbc.Add(moi);
                }
            }
            BaoCao.rep_BCXetNghiemHangNgay rep = new BaoCao.rep_BCXetNghiemHangNgay();
            frmIn frm = new frmIn();
            rep.DataSource = lbc;
            rep.celNgay.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy") ;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
           
        }
        class BC
        {
            public int MaKP { set; get; }
            public string TenKP { set; get; }
            public int SoBN { set; get; }
            public int SinhHoa { set; get; }
            public int HuyetHoc { set; get; }
            public int NuocTieu { set; get; }
            public int ViSinh { set; get; }
            public int MienDich { set; get; }

            public int DongMau { set; get; }
            public int DienGiai { set; get; }
            public int SoTieuBan { set; get; }
          
        }
    }
}