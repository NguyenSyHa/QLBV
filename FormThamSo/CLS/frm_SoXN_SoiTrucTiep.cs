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
    public partial class frm_SoXN_SoiTrucTiep : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoXN_SoiTrucTiep()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            int loaingay = rdLoaiNgay.SelectedIndex;           
            int makp = 0;
            if(lupKhoa.EditValue != null)
                makp = Convert.ToInt32(lupKhoa.EditValue);

            var bnhan = (from bn in data.BenhNhans
                         join ttbx in data.TTboXungs.Where(p=>p.ChanDoanLao != null && p.ChanDoanLao >= 1 && p.ChanDoanLao <= 4) on bn.MaBNhan equals ttbx.MaBNhan
                         join cls in data.CLS.Where(p=>p.Status == 1) on bn.MaBNhan equals cls.MaBNhan
                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                         select new
                         {
                             soXN = cls.IdCLS,
                             NgayNhanMau = cls.ThoiGianNhanMau,
                             bn.TenBNhan,
                             TuoiNam = bn.GTinh == 1 ? bn.Tuoi : null,
                             TuoiNu = bn.GTinh == 0 ? bn.Tuoi : null,
                             DChi = (ttbx.ThangTheoDoi == null || ttbx.ThangTheoDoi == 0)? bn.DChi : "",
                             cls.MaKP,                             
                             cd.Mau_Lan_MTruongXN,// lần thứ
                             cls.GhiChu,// lý do
                             ttbx.TinhTrangH,
                             cls.BenhPham,
                             BPlower = cls.BenhPham == null ? "" : cls.BenhPham.ToLower(),
                             cls.TrangThaiBP,
                             cls.NgayTH,
                             cls.NgayKQ,
                             clsct.KetQua,
                             clsct.STTHT,
                             clsct.MaDVct,
                             cls.MaCBth,
                             ttbx.ThangTheoDoi,
                             Mau = (cd.Mau_Lan_MTruongXN == null || cd.Mau_Lan_MTruongXN == "" || cd.Mau_Lan_MTruongXN == "1") ? 1 :(cd.Mau_Lan_MTruongXN == "2" ? 2 : 0),
                         }).Where(p=>loaingay == 0 ? (p.NgayNhanMau >= tungay && p.NgayNhanMau <= denngay) :(loaingay == 1 ? (p.NgayTH >= tungay && p.NgayTH <= denngay) : (p.NgayKQ >= tungay && p.NgayKQ <= denngay))).ToList();

            var qdv = (from dvct in data.DichVucts
                       join dv in data.DichVus.Where(p=>p.SoTT == 1 || p.SoTT == 2) on dvct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs.Where(p=>p.TenRG == "XN đờm") on dv.IdTieuNhom equals tn.IdTieuNhom                      
                       select new {
                           dv.SoTT, dv.MaDV, dvct.MaDVct
                       }).ToList();

            var lkp = data.KPhongs.Where(p => makp == 0 || p.MaKP == makp).ToList();
            var canbo = data.CanBoes.ToList();
            var q1 = (from bn in bnhan
                      join cb in canbo on bn.MaCBth equals cb.MaCB
                      join dv in qdv on bn.MaDVct equals dv.MaDVct
                      join kp in lkp on bn.MaKP equals kp.MaKP
                      select new {
                          bn.soXN,
                          bn.NgayNhanMau,
                          bn.TenBNhan,
                          bn.TuoiNam ,
                          bn.TuoiNu,
                          bn.DChi,
                          kp.TenKP,
                          BPDom = (bn.BPlower == "đờm" || bn.BPlower.Contains("nb") || bn.BPlower.Contains("nm") || bn.BPlower == "m") ? "x" : "",
                          BPKhac = (bn.BPlower == "đờm" || bn.BPlower.Contains("nb") || bn.BPlower.Contains("nm") || bn.BPlower == "m") ? "" : "x",
                          bn.TrangThaiBP,
                          LydoXN1 =  ( bn.ThangTheoDoi == null || bn.ThangTheoDoi == 0) ? "x" : "",
                          LydoXN2 = (bn.ThangTheoDoi == null || bn.ThangTheoDoi == 0) ? bn.TinhTrangH : null,
                          LydoXN3 = (bn.ThangTheoDoi == null || bn.ThangTheoDoi == 0) ? null : bn.ThangTheoDoi,                         
                          M1 = bn.Mau == 1 ? ( bn.STTHT == 1? "Âm" :(bn.STTHT == 2 ? bn.KetQua :(bn.STTHT == 3 ? "1+" : (bn.STTHT == 4 ? "2+" : (bn.STTHT == 5 ? "3+" : bn.KetQua))))) : "",
                          M2 = bn.Mau == 2 ? (bn.STTHT == 1 ? "Âm" : (bn.STTHT == 2 ? bn.KetQua : (bn.STTHT == 3 ? "1+" : (bn.STTHT == 4 ? "2+" : (bn.STTHT == 5 ? "3+" : bn.KetQua))))) : "",
                          CanBoTH = cb.TenCB,
                          GhiChu = (bn.BPlower == "đờm" || bn.BPlower.Contains("nb") || bn.BPlower.Contains("nm") || bn.BPlower == "m") ? "" : bn.BenhPham,                                             
                      }).OrderBy(p=>p.NgayNhanMau).ThenBy(p=>p.TenBNhan).ToList();

            frmIn frm = new frmIn();
            BaoCao.rep_SoXN_SoiTrucTiep rep = new BaoCao.rep_SoXN_SoiTrucTiep();
            rep.DataSource = q1;
            rep.DataBinding();
            rep.CreateDocument();           
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
       // QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_SoXN_Expert_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            List<KPhong> khoa = data.KPhongs.ToList();          
            khoa.Insert(0,new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoa.Properties.DataSource = khoa;
            lupKhoa.Properties.DisplayMember = "TenKP";
            lupKhoa.Properties.ValueMember = "MaKP";
            lupKhoa.EditValue = lupKhoa.Properties.GetKeyValueByDisplayText("Tất cả");
            
        }
    }
}