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
    public partial class frm_SoXN_Expert : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoXN_Expert()
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
                             bn.DChi,
                             cls.MaKP,
                             ChanDoanDaKhang = ttbx.ChanDoanLao == 2 ? ttbx.DTuongLao : null,
                             ChanDoanLao = ttbx.ChanDoanLao == 1 ? ttbx.DTuongLao : null,
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
                         }).Where(p=>loaingay == 0 ? (p.NgayNhanMau >= tungay && p.NgayNhanMau <= denngay) :(loaingay == 1 ? (p.NgayTH >= tungay && p.NgayTH <= denngay) : (p.NgayKQ >= tungay && p.NgayKQ <= denngay))).ToList();

            var qdv = (from dvct in data.DichVucts
                       join dv in data.DichVus.Where(p=>p.SoTT == 6) on dvct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs.Where(p=>p.TenRG == "XN đờm") on dv.IdTieuNhom equals tn.IdTieuNhom                      
                       select new {
                           dv.SoTT, dv.MaDV, dvct.MaDVct
                       }).ToList();

            var lkp = data.KPhongs.Where(p => makp == 0 || p.MaKP == makp).ToList();
            var q1 = (from bn in bnhan
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
                          bn.ChanDoanDaKhang,
                          bn.ChanDoanLao,
                          Lan1 =  (bn.Mau_Lan_MTruongXN == null || bn.Mau_Lan_MTruongXN == "1") ? "x" :"",// lần thứ
                          LyDo =  (bn.Mau_Lan_MTruongXN == null || bn.Mau_Lan_MTruongXN == "1") ? "" : bn.GhiChu,// lý do
                          bn.TinhTrangH,
                          BPDom = (bn.BPlower == "đờm" || bn.BPlower == "nb" || bn.BPlower == "nm" || bn.BPlower == "m") ? "x" : "",
                          BPKhac = (bn.BPlower == "đờm" || bn.BPlower == "nb" || bn.BPlower == "nm" || bn.BPlower == "m") ? "" : "x",
                          bn.TrangThaiBP,
                          bn.NgayTH,
                          KQ1 = bn.STTHT == 1 ? "x" : "",
                          KQ2 = bn.STTHT == 2 ? "x" : "",
                          KQ3 = bn.STTHT == 3 ? "x" : "",
                          KQ4 = bn.STTHT == 4 ? "x" : "",
                          KQ5 = bn.STTHT == 5 ? bn.KetQua : "",
                          NgayTraKQ = bn.NgayKQ,
                          GhiChu = (bn.BPlower == "đờm" || bn.BPlower == "nb" || bn.BPlower == "nm" || bn.BPlower == "m") ? "" : bn.BenhPham,                                             
                      }).OrderBy(p=>p.NgayNhanMau).ThenBy(p=>p.TenBNhan).ToList();

            frmIn frm = new frmIn();
            BaoCao.rep_SoXN_Expert rep = new BaoCao.rep_SoXN_Expert();
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