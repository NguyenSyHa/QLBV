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
    public partial class rep_BkeVienPhi_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public static DateTime tungay;
        public static DateTime denngay;
        QLBV_Database.QLBVEntities data;
        public rep_BkeVienPhi_26007()
        {
            InitializeComponent();
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">mẫu in : 0: chi tiết; 1: tổng hợp</param>
        public rep_BkeVienPhi_26007(int p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
        }       
        private void rep_BkeVienPhi_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
        internal void BindingData()
        {
            celTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNTuoi.DataBindings.Add("Text", DataSource, "Tuoi");           
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
            coltn19.DataBindings.Add("Text", DataSource, "tn19").FormatString = DungChung.Bien.FormatString[1];
            coltn20.DataBindings.Add("Text", DataSource, "tn20").FormatString = DungChung.Bien.FormatString[1];
            colTongSo.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
           

            coltn1G.DataBindings.Add("Text", DataSource, "tn1").FormatString = DungChung.Bien.FormatString[1];
            coltn2G.DataBindings.Add("Text", DataSource, "tn2").FormatString = DungChung.Bien.FormatString[1];
            coltn3G.DataBindings.Add("Text", DataSource, "tn3").FormatString = DungChung.Bien.FormatString[1];
            coltn4G.DataBindings.Add("Text", DataSource, "tn4").FormatString = DungChung.Bien.FormatString[1];
            coltn5G.DataBindings.Add("Text", DataSource, "tn5").FormatString = DungChung.Bien.FormatString[1];
            coltn6G.DataBindings.Add("Text", DataSource, "tn6").FormatString = DungChung.Bien.FormatString[1];
            coltn7G.DataBindings.Add("Text", DataSource, "tn7").FormatString = DungChung.Bien.FormatString[1];
            coltn8G.DataBindings.Add("Text", DataSource, "tn8").FormatString = DungChung.Bien.FormatString[1];
            coltn9G.DataBindings.Add("Text", DataSource, "tn9").FormatString = DungChung.Bien.FormatString[1];
            coltn10G.DataBindings.Add("Text", DataSource, "tn10").FormatString = DungChung.Bien.FormatString[1];
            coltn11G.DataBindings.Add("Text", DataSource, "tn11").FormatString = DungChung.Bien.FormatString[1];
            coltn12G.DataBindings.Add("Text", DataSource, "tn12").FormatString = DungChung.Bien.FormatString[1];
            coltn13G.DataBindings.Add("Text", DataSource, "tn13").FormatString = DungChung.Bien.FormatString[1];
            coltn14G.DataBindings.Add("Text", DataSource, "tn14").FormatString = DungChung.Bien.FormatString[1];
            coltn15G.DataBindings.Add("Text", DataSource, "tn15").FormatString = DungChung.Bien.FormatString[1];
            coltn16G.DataBindings.Add("Text", DataSource, "tn16").FormatString = DungChung.Bien.FormatString[1];
            coltn17G.DataBindings.Add("Text", DataSource, "tn17").FormatString = DungChung.Bien.FormatString[1];
            coltn18G.DataBindings.Add("Text", DataSource, "tn18").FormatString = DungChung.Bien.FormatString[1];
            coltn19G.DataBindings.Add("Text", DataSource, "tn19").FormatString = DungChung.Bien.FormatString[1];
            coltn20G.DataBindings.Add("Text", DataSource, "tn20").FormatString = DungChung.Bien.FormatString[1];
            colTongSoG.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));

            coltn1_R.DataBindings.Add("Text", DataSource, "tn1").FormatString = DungChung.Bien.FormatString[1];
            coltn2_R.DataBindings.Add("Text", DataSource, "tn2").FormatString = DungChung.Bien.FormatString[1];
            coltn3_R.DataBindings.Add("Text", DataSource, "tn3").FormatString = DungChung.Bien.FormatString[1];
            coltn4_R.DataBindings.Add("Text", DataSource, "tn4").FormatString = DungChung.Bien.FormatString[1];
            coltn5_R.DataBindings.Add("Text", DataSource, "tn5").FormatString = DungChung.Bien.FormatString[1];
            coltn6_R.DataBindings.Add("Text", DataSource, "tn6").FormatString = DungChung.Bien.FormatString[1];
            coltn7_R.DataBindings.Add("Text", DataSource, "tn7").FormatString = DungChung.Bien.FormatString[1];
            coltn8_R.DataBindings.Add("Text", DataSource, "tn8").FormatString = DungChung.Bien.FormatString[1];
            coltn9_R.DataBindings.Add("Text", DataSource, "tn9").FormatString = DungChung.Bien.FormatString[1];
            coltn10_R.DataBindings.Add("Text", DataSource, "tn10").FormatString = DungChung.Bien.FormatString[1];
            coltn11_R.DataBindings.Add("Text", DataSource, "tn11").FormatString = DungChung.Bien.FormatString[1];
            coltn12_R.DataBindings.Add("Text", DataSource, "tn12").FormatString = DungChung.Bien.FormatString[1];
            coltn13_R.DataBindings.Add("Text", DataSource, "tn13").FormatString = DungChung.Bien.FormatString[1];
            coltn14_R.DataBindings.Add("Text", DataSource, "tn14").FormatString = DungChung.Bien.FormatString[1];
            coltn15_R.DataBindings.Add("Text", DataSource, "tn15").FormatString = DungChung.Bien.FormatString[1];
            coltn16_R.DataBindings.Add("Text", DataSource, "tn16").FormatString = DungChung.Bien.FormatString[1];
            coltn17_R.DataBindings.Add("Text", DataSource, "tn17").FormatString = DungChung.Bien.FormatString[1];
            coltn18_R.DataBindings.Add("Text", DataSource, "tn18").FormatString = DungChung.Bien.FormatString[1];
            coltn19_R.DataBindings.Add("Text", DataSource, "tn19").FormatString = DungChung.Bien.FormatString[1];
            coltn20_R.DataBindings.Add("Text", DataSource, "tn20").FormatString = DungChung.Bien.FormatString[1];
            colTongSo_R.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
           
            colTong.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
            //coltongbn.DataBindings.Add("Text", DataSource, "tn20").FormatString = DungChung.Bien.FormatString[1];
           
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
        private int p;
        private void colHoTen_BeforePrint(object sender, CancelEventArgs e)
        {            
            //num++;
            //colSTT.Text = num.ToString();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void DetailBand_BeforePrint(object sender, CancelEventArgs e)
        {
            if (p == 1)
                DetailBand.Visible = false;
        }

      
    }
}
