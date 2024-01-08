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
    public partial class frm_BCDieuTriNoiTruTaiKhoaPhong_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDieuTriNoiTruTaiKhoaPhong_12122()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BCDieuTriNoiTruTaiKhoaPhong_12122_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV=="30012")
            {
                lupNgaytu.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                lupNgaytu.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                lupNgaytu.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
                lupngayden.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                lupngayden.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                lupngayden.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
            }
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            rdKPDT.SelectedIndex = 0;


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay;
            DateTime denngay;
            if(DungChung.Bien.MaBV =="30012")
            {
                 tungay = lupNgaytu.DateTime;
                 denngay = lupngayden.DateTime;
            }
            else
            {
                 tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                 denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            }
            

            var q = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                     join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                     join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                     from kq1 in kq.DefaultIfEmpty()
                     where (kq1 == null || (kq1.NgayRa >= tungay && bn.NNhap <= denngay))
                     select new
                     {
                         bn.MaBNhan,
                         bn.NNhap,
                         bn.Tuoi,
                         bn.NoiTru,
                         bn.MaDTuong,
                         vv.NgayVao,
                         vv.MaKP,
                         NgayRa = kq1 == null ? null : kq1.NgayRa,
                         KQDT = kq1 == null ? "" : kq1.KetQua,
                     }).ToList();

            var q1 = (from a in q
                      select new
                      {
                          a.MaBNhan,
                          VV = (a.NgayVao >= tungay && a.NgayVao <= denngay) ? 1 : 0,
                          DK = a.NgayVao < tungay ? 1 : 0,
                          a.MaKP,
                          // nội trú
                          TSNT = 1,
                          TS6T = (a.Tuoi < 6) ? 1 : 0,
                          TS15T = (a.Tuoi >= 6 && a.Tuoi < 15) ? 1 : 0,
                          TSHN = (a.MaDTuong.Trim().ToLower() == "hn") ? 1 : 0,
                          //tử vong
                          TVTS = a.KQDT == "Tử vong" ? 1 : 0,
                          TV15 = (a.KQDT == "Tử vong" && a.Tuoi < 15) ? 1 : 0,
                          TV24H = (a.KQDT == "Tử vong" && a.NgayRa != null && (a.NgayRa.Value - a.NgayVao.Value).TotalHours < 24) ? 1 : 0,
                          //Ra viện
                          RV = (a.NgayRa >= tungay && a.NgayRa <= denngay) ? 1 : 0,
                          //Kết quả đtrị
                          KQDoGiam = (a.KQDT == "Đỡ/ giảm" || a.KQDT == "Đỡ|Giảm") ? 1 : 0,
                          KQKTD = (a.KQDT == "Không T.đổi" || a.KQDT == "Không thay đổi") ? 1 : 0,
                          KQKhoi = a.KQDT == "Khỏi" ? 1 : 0,
                          KQNangHon = a.KQDT == "Nặng hơn" ? 1 : 0,
                          BNYHCT = 0,
                          bnConLai = (a.NgayRa == null || a.NgayRa > denngay) ? 1 : 0,
                          SoNGayDT = (a.NgayVao.Value <= tungay) ? (((a.NgayRa == null || a.NgayRa >= denngay) ? (int)((denngay - tungay).TotalDays) + 1 : (int)((a.NgayRa.Value - tungay).TotalDays) + 1)) : (((a.NgayRa == null || a.NgayRa >= denngay) ? (int)((denngay - a.NgayVao.Value).TotalDays) + 1 : (int)((a.NgayRa.Value - a.NgayVao.Value).TotalDays) + 1))
                      }).ToList();

            #region tìm theo khoa phòng vào viện
            if (rdKPDT.SelectedIndex == 0)
            {
                var lbnkb = data.BNKBs.Where(p => p.NgayKham <= denngay).Select(p => p.MaBNhan).Distinct().ToList();
                var q2 = (from a in q1
                          join bn in lbnkb on a.MaBNhan equals bn
                          group a by new { a.MaKP } into kq
                          select new { 
                          kq.Key.MaKP,
                          DK = kq.Sum(p=>p.DK),
                          VV = kq.Sum(p=>p.VV),
                          TSNT = kq.Sum(p => p.TSNT),
                          TS6T = kq.Sum(p => p.TS6T),
                          TS15T = kq.Sum(p => p.TS15T),
                          TSHN = kq.Sum(p => p.TSHN),
                          TVTS = kq.Sum(p => p.TVTS),
                          TV15 = kq.Sum(p => p.TV15),
                          TV24H = kq.Sum(p => p.TV24H),
                          RV = kq.Sum(p => p.RV),
                          KQDoGiam = kq.Sum(p => p.KQDoGiam),
                          KQKTD = kq.Sum(p => p.KQKTD),
                          KQKhoi = kq.Sum(p => p.KQKhoi),
                          KQNangHon = kq.Sum(p => p.KQNangHon),
                          BNYHCT = 0,
                          bnConLai = kq.Sum(p => p.bnConLai),
                          SoNGayDT = kq.Sum(p => p.SoNGayDT),
                          }).ToList();

                frmIn frm = new frmIn();
                BaoCao.rep_BCDieuTriNoiTruTaiKP_12122 rep = new BaoCao.rep_BCDieuTriNoiTruTaiKP_12122();
                rep.lblNgayThang.Text = " Từ ngày " + tungay.Day.ToString("D2") + " tháng " + tungay.Month.ToString("D2") + " đến ngày " + denngay.Day.ToString("D2") + " tháng " + denngay.Month.ToString("D2") + " năm " + denngay.Year.ToString();
                rep.BindingData();
                rep.DataSource = q2.OrderBy(p => p.MaKP).ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region tìm theo khoa phòng điều trị cuối cùng
            else
            {
                // lấy ra mã khoa phòng điều tị cuối cùng
                var q2 = (from a in q
                          join bnkb in data.BNKBs.Where(p => p.NgayKham <= denngay) on a.MaBNhan equals bnkb.MaBNhan
                          group new { a, bnkb } by new { a.MaBNhan } into kq
                          select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.bnkb.IDKB) }).ToList();
                var q3 = (from a in q2 
                          join bnkb in data.BNKBs on a.IDKB equals bnkb.IDKB 
                          select new { a.MaBNhan, bnkb.MaKP }).ToList();
                var q4 = (from a in q1
                          join b in q3 on a.MaBNhan equals b.MaBNhan
                          join c in data.KPhongs on b.MaKP equals c.MaKP
                          group new { a, b } by new { b.MaKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              DK = kq.Sum(p => p.a.DK),
                              VV = kq.Sum(p => p.a.VV),
                              TSNT = kq.Sum(p => p.a.TSNT),
                              TS6T = kq.Sum(p => p.a.TS6T),
                              TS15T = kq.Sum(p => p.a.TS15T),
                              TSHN = kq.Sum(p => p.a.TSHN),
                              TVTS = kq.Sum(p => p.a.TVTS),
                              TV15 = kq.Sum(p => p.a.TV15),
                              TV24H = kq.Sum(p => p.a.TV24H),
                              RV = kq.Sum(p => p.a.RV),
                              KQDoGiam = kq.Sum(p => p.a.KQDoGiam),
                              KQKTD = kq.Sum(p => p.a.KQKTD),
                              KQKhoi = kq.Sum(p => p.a.KQKhoi),
                              KQNangHon = kq.Sum(p => p.a.KQNangHon),
                              BNYHCT = 0,
                              bnConLai = kq.Sum(p => p.a.bnConLai),
                              SoNGayDT = kq.Sum(p => p.a.SoNGayDT),
                          }).ToList();

                frmIn frm = new frmIn();
                BaoCao.rep_BCDieuTriNoiTruTaiKP_12122 rep = new BaoCao.rep_BCDieuTriNoiTruTaiKP_12122();
                rep.lblNgayThang.Text = " Từ ngày " + tungay.Day.ToString("D2") + " tháng " + tungay.Month.ToString("D2") + " đến ngày " + denngay.Day.ToString("D2") + " tháng " + denngay.Month.ToString("D2") + " năm " + denngay.Year.ToString();
                rep.BindingData();
                rep.DataSource = q4.OrderBy(p => p.MaKP).ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            #endregion
        }
    }
}