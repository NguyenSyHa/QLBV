using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Linq;
using System.Data;

namespace QLBV.BaoCao
{
    public partial class rep_BkeVienPhi_InMaThe : DevExpress.XtraReports.UI.XtraReport
    {
        public static DateTime tungay;
        public static DateTime denngay;
        QLBV_Database.QLBVEntities data;
        public rep_BkeVienPhi_InMaThe()
        {
            InitializeComponent();
         
        }       
        private void rep_BkeVienPhi_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
        internal void BindingData()
        {
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celMaThe.DataBindings.Add("Text", DataSource, "SThe");
            coldiachi.DataBindings.Add("Text", DataSource, "DChi");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao");
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa");
            colSoBA.DataBindings.Add("Text", DataSource, "SoVV");                
            coltn1.DataBindings.Add("Text", DataSource, "tn1").FormatString = DungChung.Bien.FormatString[1];
            coltn2.DataBindings.Add("Text", DataSource, "tn2").FormatString = DungChung.Bien.FormatString[1];
            coltn3.DataBindings.Add("Text", DataSource, "tn3").FormatString = DungChung.Bien.FormatString[1];
            coltn4.DataBindings.Add("Text", DataSource, "tn4").FormatString = DungChung.Bien.FormatString[1];
            coltn5.DataBindings.Add("Text", DataSource, "tn5").FormatString = DungChung.Bien.FormatString[1];
            coltn6.DataBindings.Add("Text", DataSource, "tn6").FormatString = DungChung.Bien.FormatString[1];
            coltn7.DataBindings.Add("Text", DataSource, "tn7").FormatString = DungChung.Bien.FormatString[1];
            coltn8.DataBindings.Add("Text", DataSource, "tn8").FormatString = DungChung.Bien.FormatString[1];
            coltn9.DataBindings.Add("Text", DataSource, "tn9").FormatString = DungChung.Bien.FormatString[1];
            coltn10.DataBindings.Add("Text", DataSource, "tn10").FormatString = DungChung.Bien.FormatString[1];
            coltn11.DataBindings.Add("Text", DataSource, "tn11").FormatString = DungChung.Bien.FormatString[1];
            coltn12.DataBindings.Add("Text", DataSource, "tn12").FormatString = DungChung.Bien.FormatString[1];
            coltn13.DataBindings.Add("Text", DataSource, "tn13").FormatString = DungChung.Bien.FormatString[1];
            coltn14.DataBindings.Add("Text", DataSource, "tn14").FormatString = DungChung.Bien.FormatString[1];
            coltn15.DataBindings.Add("Text", DataSource, "tn15").FormatString = DungChung.Bien.FormatString[1];
            coltn16.DataBindings.Add("Text", DataSource, "tn16").FormatString = DungChung.Bien.FormatString[1];
            coltn17.DataBindings.Add("Text", DataSource, "tn17").FormatString = DungChung.Bien.FormatString[1];
            coltn18.DataBindings.Add("Text", DataSource, "tn18").FormatString = DungChung.Bien.FormatString[1];
            colTongSo.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
           
            #region bo

            //var q = (from vp in data.VienPhis
            //         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //         join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
            //         join bnk in data.BNKBs on bn.MaBNhan equals bnk.MaBNhan
            //         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
            //         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
            //         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //         join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
            //         group new { vp, vpct, dv, tn, bn, bnk, rv, n } by new
            //         {
            //             bn.MaBNhan,
            //             n.IDNhom,
            //             bn.TenBNhan,
            //             bn.GTinh,
            //             bn.Tuoi,
            //             bn.DChi,
            //             bnk.ChanDoan,
            //             rv.SoNgaydt,
            //             tn.IdTieuNhom,
            //             vpct.SoLuong,
            //             vpct.TienBN
            //         } into kq
            //         orderby kq.Key.MaBNhan, kq.Key.IDNhom
            //         select new
            //         {
            //             TenBNhan = kq.Key.TenBNhan,
            //             GTinh = kq.Key.GTinh,
            //             Tuoi = kq.Key.Tuoi,
            //             DChi = kq.Key.DChi,
            //             ChanDoan = kq.Key.ChanDoan,
            //             SoNgaydt = kq.Key.SoNgaydt,
            //             IdTieuNhom = kq.Key.IdTieuNhom,
            //             SoLuong = kq.Key.SoLuong,
            //             TienBN = kq.Key.TienBN,
            //             TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : null,
            //             TuoiNu = kq.Key.GTinh == 0 ? kq.Key.Tuoi : null,
            //         }).ToList();
            //this.DataSource = q;
            #endregion
        }
        int num = 0;
        private void colHoTen_BeforePrint(object sender, CancelEventArgs e)
        {            
            //num++;
            //colSTT.Text = num.ToString();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

      
    }
}
