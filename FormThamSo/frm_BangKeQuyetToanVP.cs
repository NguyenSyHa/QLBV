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
    public partial class frm_BangKeQuyetToanVP : DevExpress.XtraEditors.XtraForm
    {
        public frm_BangKeQuyetToanVP()
        {
            InitializeComponent();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BangKeQuyetToanVP_Load(object sender, EventArgs e)
        {
            List<DTBN> qdtbn = data.DTBNs.Where(p => p.Status == 1).ToList();
            qdtbn.Insert( 0,new DTBN{IDDTBN = 100, DTBN1 = "Tất cả"});
            lupDoituong.Properties.DataSource = qdtbn;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang && p.Status == 1).ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaphong.Properties.DataSource = lkp;
            lupKhoaphong.EditValue = lupKhoaphong.Properties.GetKeyValueByDisplayText("Tất cả");
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            int noitru = rdNoiNgoaiTru.SelectedIndex;
            int makp = -1;
            if (lupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
            int idDTBN = -1;
            if (lupDoituong.EditValue != null)
                idDTBN = Convert.ToInt32(lupDoituong.EditValue);

            int trongNgoaiDM = -1;
            if(lupTrongDM.SelectedIndex == 0)
                trongNgoaiDM = -1;
            else  if(lupTrongDM.SelectedIndex == 1)
                trongNgoaiDM = 1;
             else  if(lupTrongDM.SelectedIndex == 2)
                trongNgoaiDM = 0;
             else  if(lupTrongDM.SelectedIndex == 3)
                trongNgoaiDM = 2;
            var qbn = (from rv in data.RaViens.Where(p => (makp == 0 || p.MaKP == makp) && (radTimKiem.SelectedIndex == 1 ? true : (p.NgayRa >= tungay && p.NgayRa <= denngay)))
                       join vp in data.VienPhis.Where(p => radTimKiem.SelectedIndex == 0 ? true : (p.NgayTT >= tungay && p.NgayTT <= denngay)) on rv.MaBNhan equals vp.MaBNhan
                       join bn in data.BenhNhans.Where(p=> (noitru == 0 ? (p.NoiTru == 0 && p.DTNT == true) : p.NoiTru == 1) && (idDTBN == 100 ? true : p.IDDTBN == idDTBN)) on rv.MaBNhan equals bn.MaBNhan
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       select new { bn.MaBNhan, rv.NgayRa, rv.SoNgaydt, vp.NgayTT, vp.idVPhi, vv.NgayVao, bn.GTinh, bn.NamSinh, bn.DChi, bn.TenBNhan }).ToList();
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.MaDV,
                           dv.TenDV,
                           tn.TenRG,
                           tn.IDNhom
                       }).ToList();
            List<int> lIDVP = qbn.Select(p => p.idVPhi).ToList();
            var qvpct = data.VienPhicts.Where(p => lIDVP.Contains(p.idVPhi ?? 0) && (trongNgoaiDM == -1 ?  true : p.TrongBH == trongNgoaiDM)).ToList();
            var qkq1 = (from vp in qvpct
                        join dv in qdv on vp.MaDV equals dv.MaDV
                        join bn in qbn on vp.idVPhi equals bn.idVPhi
                        select new { 
                        bn.MaBNhan, bn.TenBNhan,bn.DChi, bn.NgayTT, bn.NgayVao, bn.NgayRa, bn.GTinh,NamSinhNam = bn.GTinh == 1?  bn.NamSinh : "", NamSinhNu = bn.GTinh == 0 ? bn.NamSinh : "", bn.SoNgaydt, dv.TenRG, dv.IDNhom, vp.ThanhTien
                        }).ToList();
            var qkq2 = (from vp in qkq1 select new { vp.MaBNhan, vp.TenBNhan,vp.DChi, vp.NgayTT, vp.NgayVao, vp.NgayRa, vp.GTinh,vp.NamSinhNam ,vp.NamSinhNu , vp.SoNgaydt,
            XN = vp.IDNhom == 1? vp.ThanhTien : 0,
            CDHA = vp.IDNhom == 2 ? vp.ThanhTien : 0,
            Thuoc = (vp.IDNhom == 4 || vp.IDNhom == 5 || vp.IDNhom == 6) ? vp.ThanhTien : 0,
            Mau = vp.IDNhom == 7? vp.ThanhTien : 0,
            PhauThuat = vp.IDNhom == 8 ? vp.ThanhTien:0,
            VTYT = (vp.IDNhom == 10 || vp.IDNhom == 11) ? vp.ThanhTien : 0,
            Kham = vp.IDNhom == 13? vp.ThanhTien : 0,
            Giuong = (vp.IDNhom == 14 || vp.IDNhom == 15) ? vp.ThanhTien : 0,
           
            Cong = vp.ThanhTien
            }).ToList();

            var qkq3 = (from vp in qkq2 group vp by new { vp.MaBNhan,
                            vp.TenBNhan,
                            vp.DChi,
                            vp.NgayTT,
                            vp.NgayVao,
                            vp.NgayRa,
                            vp.GTinh,
                            vp.NamSinhNam,
                            vp.NamSinhNu,
                            vp.SoNgaydt} into kq
                        select new
                        {
                            kq.Key.MaBNhan,
                            kq.Key.TenBNhan,
                            kq.Key.DChi,
                            kq.Key.NgayTT,
                            kq.Key.NgayVao,
                            kq.Key.NgayRa,
                            kq.Key.GTinh,
                            kq.Key.NamSinhNam,
                            kq.Key.NamSinhNu,
                            kq.Key.SoNgaydt,
                            XN = kq.Sum(p=>p.XN),
                            CDHA = kq.Sum(p => p.CDHA),
                            Thuoc = kq.Sum(p => p.Thuoc),
                            Mau = kq.Sum(p => p.Mau),
                            PhauThuat = kq.Sum(p => p.PhauThuat),
                            VTYT = kq.Sum(p => p.VTYT),
                            Kham = kq.Sum(p => p.Kham),
                            Giuong = kq.Sum(p => p.Giuong),                          
                            Cong = kq.Sum(p => p.Cong)
                        }).ToList();

            var qkq4 = (from vp in qkq3
                       
                        select new
                        {
                            vp.MaBNhan,
                            vp.TenBNhan,
                            vp.DChi,
                            vp.NgayTT,
                            vp.NgayVao,
                            vp.NgayRa,
                            vp.GTinh,
                            vp.NamSinhNam,
                            vp.NamSinhNu,
                            vp.SoNgaydt,
                            XN = vp.XN,
                            CDHA = vp.CDHA,
                            Thuoc = vp.Thuoc,
                            Mau = vp.Mau,
                            PhauThuat =vp.PhauThuat,
                            VTYT = vp.VTYT,
                            Kham = vp.Kham,
                            Giuong = vp.Giuong,
                            Khac = vp.Cong - vp.XN - vp.CDHA - vp.Thuoc - vp.Mau - vp.PhauThuat - vp.VTYT - vp.Kham - vp.Giuong,
                            Cong = vp.Cong,
                        }).ToList();
            
            frmIn frm = new frmIn();
            BaoCao.rep_BangKeQuyetToanVP rep = new BaoCao.rep_BangKeQuyetToanVP();
            if(cbosx.SelectedIndex == 0)
                rep.DataSource = qkq4.OrderBy(p=>p.MaBNhan).ToList();
            else if (cbosx.SelectedIndex == 1)
                rep.DataSource = qkq4.OrderBy(p => p.NgayRa).ToList();
            else if (cbosx.SelectedIndex == 2)
                rep.DataSource = qkq4.OrderBy(p => p.NgayTT).ToList();
           
                rep.colKP.Text ="Khoa: " + lupKhoaphong.Text;
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
            rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " năm " + DateTime.Now.Year.ToString();
            if(rdNoiNgoaiTru.SelectedIndex == 0)
            rep.CelTieuDe.Text = "BẢNG KÊ QUYẾT TOÁN VIỆN PHÍ ĐIỀU TRỊ NGOẠI TRÚ";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
           
        }
    }
}