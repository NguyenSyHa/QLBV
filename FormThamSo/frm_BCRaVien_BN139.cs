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
    public partial class frm_BCRaVien_BN139 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCRaVien_BN139()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCRaVien_BN139_Load(object sender, EventArgs e)
        {
            lup_NgayTu.DateTime = DateTime.Now;
            lup_NgayDen.DateTime = DateTime.Now;
            var _khoa = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            KPhong moi = new KPhong();
            moi.MaKP = 0;
            moi.TenKP = "Tất cả";
            _khoa.Add(moi);
            lup_Khoa.Properties.DataSource = _khoa.OrderBy(p => p.MaKP).ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lup_NgayTu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lup_NgayDen.DateTime);
            int makp =0;
            if(lup_Khoa.Text != "")
                makp = Convert.ToInt32(lup_Khoa.EditValue.ToString());
            var bn = (from a in data.BenhNhans.Where(p => p.MaDTuong == "HN" || p.MaDTuong == "DT")
                      join d in data.VaoViens on a.MaBNhan equals d.MaBNhan
                      join b in data.TTboXungs on a.MaBNhan equals b.MaBNhan
                      join c in data.DmNNs on b.MaNN equals c.MaNN into kq
                      from k1 in kq.DefaultIfEmpty()
                      select new {
                          a.MaBNhan,
                          a.TenBNhan,
                          a.Tuoi,
                          a.GTinh,
                          DTuong = a.MaDTuong,
                          a.DChi,
                          TenNN = k1!= null ? k1.TenNN : ""
                      }).ToList();

            var _ds = (from b in bn 
                       join a in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on b.MaBNhan equals a.MaBNhan
                       join c in data.RaViens.Where(p => p.MaKP == makp || makp == 0) on a.MaBNhan equals c.MaBNhan 
                       join d in data.KPhongs on c.MaKP equals d.MaKP
                       select new { 
                           a.MaBNhan,
                           c.SoLT,
                           Khoa = d.TenKP,
                           b.TenBNhan,
                           TuoiNam = b.GTinh == 0 ? "" : (b.Tuoi ?? 0).ToString(),
                           TuoiNu = b.GTinh == 1 ? "" : (b.Tuoi ?? 0).ToString(),
                           b.DTuong,
                           b.TenNN,
                           b.DChi,
                           NgayVao = c.NgayVao.Value.Day + "/" + c.NgayVao.Value.Month + "/" + c.NgayVao.Value.Year,
                           NgayRa = c.NgayRa.Value.Day + "/" + c.NgayRa.Value.Month + "/" + c.NgayRa.Value.Year,
                           SoNgayDT = c.SoNgaydt,
                           ChanDoan = c.ChanDoan.Split(';')[0],
                           MaBenh = c.MaICD.Split(';')[0],
                           Khoi = c.KetQua == "Khỏi" && c.Status != 1 ? "X" : "",
                           Do = c.KetQua == "Đỡ|Giảm" && c.Status != 1 ? "X" : "",
                           Nang = c.KetQua == "Không T.đổi" && c.Status != 1 ? "X" : "",
                           KhongDoi = c.KetQua == "Nặng hơn" && c.Status != 1 ? "X" : "",
                           ChuyenVien = c.Status == 1 ? "CV" : "",
                           a.NgayTT
                       }).OrderBy(p => p.NgayTT).ToList();
            BaoCao.rep_BCRaVien_BN139 rep = new BaoCao.rep_BCRaVien_BN139();
            if (makp != 0)
                rep.TieuDe.Value = lup_Khoa.Text.ToUpper();
            else
                rep.TieuDe.Value = "BỆNH VIỆN ";
            rep.TieuDe1.Value = " NGUỒN 139 THÁNG " + tungay.Month;
            rep.DataSource = _ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}