using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_BCHDTaiChinh_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        private DateTime tungay;
        private DateTime denngay;
        private int noitru;

        public rep_BCHDTaiChinh_30010()
        {
            InitializeComponent();
        }
        List<VP> _lVP = new List<VP>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public rep_BCHDTaiChinh_30010(DateTime tungay, DateTime denngay, int noitru)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lVP = new List<VP>();
            this.tungay = tungay;
            this.denngay = denngay;
            this.noitru = noitru;

            var qvp = (from vp in data.VienPhis.Where(p=>p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join bn in data.BenhNhans.Where(p => noitru == 2 ? true : p.NoiTru == noitru) on vp.MaBNhan equals bn.MaBNhan
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       select vpct).ToList();
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { dv.MaDV, tn.TenRG, tn.IdTieuNhom, n.IDNhom, n.TenNhomCT}).ToList();
            var q1 = (from vp in qvp
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      select new {

                          dv.TenRG, dv.TenNhomCT, dv.IDNhom, dv.IdTieuNhom,                         
                          TienBH = vp.TienBH/1000,TienBN = vp.TienBN/1000                         
                      }).ToList();
           
            VP moi1 = new VP();
            moi1.STT = "1";
            moi1.ChiTieu = "Khám bệnh";
            moi1.vp = q1.Where(p => p.TenNhomCT == "Khám bệnh").Sum(p => p.TienBN);
            moi1.bh = q1.Where(p => p.TenNhomCT == "Khám bệnh").Sum(p => p.TienBH);

            VP moi2 = new VP();
            moi2.STT = "2";
            moi2.ChiTieu = "Tiền giường";
            moi2.vp = q1.Where(p => p.IDNhom == 14 || p.IDNhom == 15).Sum(p => p.TienBN);
            moi2.bh = q1.Where(p => p.IDNhom == 14 || p.IDNhom == 15).Sum(p => p.TienBH);

            VP moi3 = new VP();
            moi3.STT = "3";
            moi3.ChiTieu = "Tiền thuốc";
            moi3.vp = q1.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.TienBN);
            moi3.bh = q1.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Sum(p => p.TienBH);

            VP moi4 = new VP();
            moi4.STT = "4";
            moi4.ChiTieu = "Phẫu thuật";
            moi4.vp = q1.Where(p => p.TenRG == "Phẫu Thuật" ).Sum(p => p.TienBN);
            moi4.bh = q1.Where(p => p.TenRG == "Phẫu Thuật").Sum(p => p.TienBH);

            VP moi5 = new VP();
            moi5.STT = "5";
            moi5.ChiTieu = "Máu truyền";
            moi5.vp = q1.Where(p => p.IDNhom == 7).Sum(p => p.TienBN);
            moi5.bh = q1.Where(p => p.IDNhom == 7).Sum(p => p.TienBH);

            VP moi6 = new VP();
            moi6.STT = "6";
            moi6.ChiTieu = "Xét nghiệm";
            moi6.vp = q1.Where(p => p.IDNhom == 1).Sum(p => p.TienBN);
            moi6.bh = q1.Where(p => p.IDNhom == 1).Sum(p => p.TienBH);

            VP moi7 = new VP();
            moi7.STT = "";
            moi7.ChiTieu = "- Huyết học";

            VP moi8 = new VP();
            moi8.STT = "";
            moi8.ChiTieu = "- Hóa sinh";

            VP moi9 = new VP();
            moi9.STT = "";
            moi9.ChiTieu = "- Vi sinh";

            VP moi10 = new VP();
            moi10.STT = "";
            moi10.ChiTieu = "- HIV";

            VP moi11 = new VP();
            moi11.STT = "7";
            moi11.ChiTieu = "Điện tim";
            moi11.vp = q1.Where(p => p.TenRG == "Điện tim").Sum(p => p.TienBN);
            moi11.bh = q1.Where(p => p.TenRG == "Điện tim").Sum(p => p.TienBH);

            VP moi12 = new VP();
            moi12.STT = "8";
            moi12.ChiTieu = "Điện não";

            VP moi13 = new VP();
            moi13.STT = "9";
            moi13.ChiTieu = "Thăm dò chức năng";
            moi13.vp = q1.Where(p => p.IDNhom == 3).Sum(p => p.TienBN);
            moi13.bh = q1.Where(p => p.IDNhom == 3).Sum(p => p.TienBH);

            VP moi14 = new VP();
            moi14.STT = "10";
            moi14.ChiTieu = "Siêu âm";
            moi14.vp = q1.Where(p => p.TenRG == "Siêu âm").Sum(p => p.TienBN);
            moi14.bh = q1.Where(p => p.TenRG == "Siêu âm").Sum(p => p.TienBH);

            VP moi15 = new VP();
            moi15.STT = "11";
            moi15.ChiTieu = "X - quang";
            moi15.vp = q1.Where(p => p.TenRG == "X-Quang").Sum(p => p.TienBN);
            moi15.bh = q1.Where(p => p.TenRG == "X-Quang").Sum(p => p.TienBH);

            VP moi16 = new VP();
            moi16.STT = "12";
            moi16.ChiTieu = "CT - Scanner";
            moi16.vp = q1.Where(p => p.TenRG == "X-Quang (CT)").Sum(p => p.TienBN);//?????
            moi16.bh = q1.Where(p => p.TenRG == "X-Quang (CT)").Sum(p => p.TienBH);//?????

            VP moi17 = new VP();
            moi17.STT = "13";
            moi17.ChiTieu = "Cộng hưởng từ";

            VP moi18 = new VP();
            moi18.STT = "14";
            moi18.ChiTieu = "Nội soi các loại";
            moi18.vp = q1.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.TienBN);
            moi18.bh = q1.Where(p => p.TenRG.Contains("Nội soi")).Sum(p => p.TienBH);

            VP moi19 = new VP();
            moi19.STT = "15";
            moi19.ChiTieu = "Thận nhân tạo";


            VP moi20 = new VP();
            moi20.STT = "16";
            moi20.ChiTieu = "Giải phẫu bệnh: - Vi thể";

            VP moi21 = new VP();
            moi21.STT = "";
            moi21.ChiTieu = "                    - Đại thể";

            double tongthevp = moi1.vp +  +moi2.vp +  moi3.vp +  moi4.vp + moi5.vp  + moi6.vp + moi11.vp + moi13.vp + moi14.vp +moi15.vp + moi16.vp + + moi18.vp;
            double tongthebh =  moi1.bh   + moi2.bh + moi3.bh + moi4.bh + moi5.bh + moi6.bh + moi11.bh +  moi13.bh + moi14.bh + moi15.bh +  moi16.bh +    moi18.bh ;

            VP moi22 = new VP();
            moi22.STT = "17";
            moi22.ChiTieu = "Khác";
            moi22.vp = q1.Sum(p => p.TienBN) - tongthevp;
            moi22.bh = q1.Sum(p => p.TienBH) - tongthebh;

            _lVP.Add(moi1);
            _lVP.Add(moi2);
            _lVP.Add(moi3);
            _lVP.Add(moi4);
            _lVP.Add(moi5);
            _lVP.Add(moi6);
            _lVP.Add(moi7);
            _lVP.Add(moi8);
            _lVP.Add(moi9);
            _lVP.Add(moi10);
            _lVP.Add(moi11);
            _lVP.Add(moi12);
            _lVP.Add(moi13);
            _lVP.Add(moi14);
            _lVP.Add(moi15);
            _lVP.Add(moi16);
            _lVP.Add(moi17);
            _lVP.Add(moi18);
            _lVP.Add(moi19);
            _lVP.Add(moi20);
            _lVP.Add(moi21);
            _lVP.Add(moi22);
        }
        public class VP
        {
            public string STT { set; get; }
            public string ChiTieu { set; get; }
            public double vp { set; get; }
            public double bh { set; get; }
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThangNam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            colGD.Text = DungChung.Bien.GiamDoc;
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BCHDTaiChinh_30010_sub repSub = (rep_BCHDTaiChinh_30010_sub)xrSubreport1.ReportSource;
            repSub.DataSource = _lVP;
            repSub.dataBinding();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {


        }

    }
}
