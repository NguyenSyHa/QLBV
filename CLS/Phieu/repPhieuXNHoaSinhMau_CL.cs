using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using QLBV.DungChung;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHoaSinhMau_CL : DevExpress.XtraReports.UI.XtraReport
    {
        List<C_CLSct> _lCLSct = new List<C_CLSct>();
        int sophieu = 0;
        public repPhieuXNHoaSinhMau_CL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27777")
            {
                //
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                colTenCQCQLogo.Text = DungChung.Bien.TenCQCQ.ToUpper();
                colTenCQLogo.Text = DungChung.Bien.TenCQ.ToUpper();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
                pictureLogo.Visible = true;
                sophieu = int.Parse(paramIdCLS.Value.ToString());
                _lCLSct = (from chidinh in DataContect.ChiDinhs.Where(p => p.IdCLS == sophieu)
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           select new C_CLSct { MaDVct = clsct.MaDVct, KetQua = clsct.KetQua, STT = dvct.STT == null ? 0 : dvct.STT.Value }).ToList();
            }
            else
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
                sophieu = int.Parse(paramIdCLS.Value.ToString());
                _lCLSct = (from chidinh in DataContect.ChiDinhs.Where(p => p.IdCLS == sophieu)
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           select new C_CLSct { MaDVct = clsct.MaDVct, KetQua = clsct.KetQua, STT = dvct.STT == null ? 0 : dvct.STT.Value }).ToList();
            }
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
          
            if (Macb.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            List<String> _lTenRG = new List<String>() { "XN hóa sinh máu" };
            lab22.Text = Ham.layTSBT(DataContect, "1", _lTenRG, 0)[0];
            DV1.Text = Ham.layTSBT(DataContect, "1", _lTenRG, 0)[1];
            lab23.Text = Ham.layTSBT(DataContect, "1", _lTenRG, 0)[2];

            lab26.Text = Ham.layTSBT(DataContect, "2", _lTenRG, 0)[0];
            DV2.Text = Ham.layTSBT(DataContect, "2", _lTenRG, 0)[1];
            lab27.Text = Ham.layTSBT(DataContect, "2", _lTenRG, 0)[2];

            lab30.Text = Ham.layTSBT(DataContect, "3", _lTenRG, 0)[0];
            DV3.Text = Ham.layTSBT(DataContect, "3", _lTenRG, 0)[1];
            lab31.Text = Ham.layTSBT(DataContect, "3", _lTenRG, 0)[2];

            lab34.Text = Ham.layTSBT(DataContect, "4", _lTenRG, 0)[0];
            DV4.Text = Ham.layTSBT(DataContect, "4", _lTenRG, 0)[1];
            lab35.Text = Ham.layTSBT(DataContect, "4", _lTenRG, 0)[2];

            lab38.Text = Ham.layTSBT(DataContect, "5", _lTenRG, 0)[0];
            DV5.Text = Ham.layTSBT(DataContect, "5", _lTenRG, 0)[1];
            lab39.Text = Ham.layTSBT(DataContect, "5", _lTenRG, 0)[2];

            lab42.Text = Ham.layTSBT(DataContect, "6", _lTenRG, 0)[0];
            DV6.Text = Ham.layTSBT(DataContect, "6", _lTenRG, 0)[1];
            lab43.Text = Ham.layTSBT(DataContect, "6", _lTenRG, 0)[2];

            lab46.Text = Ham.layTSBT(DataContect, "7", _lTenRG, 0)[0];
            DV7.Text = Ham.layTSBT(DataContect, "7", _lTenRG, 0)[1];
            lab47.Text = Ham.layTSBT(DataContect, "7", _lTenRG, 0)[2];

            lab50.Text = Ham.layTSBT(DataContect, "8", _lTenRG, 0)[0];
            DV8.Text = Ham.layTSBT(DataContect, "8", _lTenRG, 0)[1];
            lab51.Text = Ham.layTSBT(DataContect, "8", _lTenRG, 0)[2];

            lab54.Text = Ham.layTSBT(DataContect, "9", _lTenRG, 0)[0];
            DV9.Text = Ham.layTSBT(DataContect, "9", _lTenRG, 0)[1];
            lab55.Text = Ham.layTSBT(DataContect, "9", _lTenRG, 0)[2];

            lab58.Text = Ham.layTSBT(DataContect, "10", _lTenRG, 0)[0];
            DV10.Text = Ham.layTSBT(DataContect, "10", _lTenRG, 0)[1];
            lab59.Text = Ham.layTSBT(DataContect, "10", _lTenRG, 0)[2];

            lab62.Text = Ham.layTSBT(DataContect, "11", _lTenRG, 0)[0];
            DV11.Text = Ham.layTSBT(DataContect, "11", _lTenRG, 0)[1];
            lab63.Text = Ham.layTSBT(DataContect, "11", _lTenRG, 0)[2];

            lab66.Text = Ham.layTSBT(DataContect, "12", _lTenRG, 0)[0];
            DV12.Text = Ham.layTSBT(DataContect, "12", _lTenRG, 0)[1];
            lab67.Text = Ham.layTSBT(DataContect, "12", _lTenRG, 0)[2];

            lab70.Text = Ham.layTSBT(DataContect, "13", _lTenRG, 0)[0];
            DV13.Text = Ham.layTSBT(DataContect, "13", _lTenRG, 0)[1];
            lab71.Text = Ham.layTSBT(DataContect, "13", _lTenRG, 0)[2];

            lab74.Text = Ham.layTSBT(DataContect, "14", _lTenRG, 0)[0];
            DV14.Text = Ham.layTSBT(DataContect, "14", _lTenRG, 0)[1];
            lab75.Text = Ham.layTSBT(DataContect, "14", _lTenRG, 0)[2];

            lab78.Text = Ham.layTSBT(DataContect, "15", _lTenRG, 0)[0];
            DV15.Text = Ham.layTSBT(DataContect, "15", _lTenRG, 0)[1];
            lab79.Text = Ham.layTSBT(DataContect, "15", _lTenRG, 0)[2];

            lab82.Text = Ham.layTSBT(DataContect, "16", _lTenRG, 0)[0];
            DV16.Text = Ham.layTSBT(DataContect, "16", _lTenRG, 0)[1];
            lab83.Text = Ham.layTSBT(DataContect, "16", _lTenRG, 0)[2];

            lab86.Text = Ham.layTSBT(DataContect, "17", _lTenRG, 0)[0];
            DV17.Text = Ham.layTSBT(DataContect, "17", _lTenRG, 0)[1];
            lab87.Text = Ham.layTSBT(DataContect, "17", _lTenRG, 0)[2];

            lab90.Text = Ham.layTSBT(DataContect, "18", _lTenRG, 0)[0];
            DV18.Text = Ham.layTSBT(DataContect, "18", _lTenRG, 0)[1];
            lab91.Text = Ham.layTSBT(DataContect, "18", _lTenRG, 0)[2];

            lab94.Text = Ham.layTSBT(DataContect, "19", _lTenRG, 0)[0];
            DV19.Text = Ham.layTSBT(DataContect, "19", _lTenRG, 0)[1];
            lab95.Text = Ham.layTSBT(DataContect, "19", _lTenRG, 0)[2];

            lab98.Text = Ham.layTSBT(DataContect, "20", _lTenRG, 0)[0];
            DV20.Text = Ham.layTSBT(DataContect, "20", _lTenRG, 0)[1];
            lab99.Text = Ham.layTSBT(DataContect, "20", _lTenRG, 0)[2];

            lab102.Text = Ham.layTSBT(DataContect, "21", _lTenRG, 0)[0];
            DV21.Text = Ham.layTSBT(DataContect, "21", _lTenRG, 0)[1];
            lab103.Text = Ham.layTSBT(DataContect, "21", _lTenRG, 0)[2];

            lab106.Text = Ham.layTSBT(DataContect, "22", _lTenRG, 0)[0];
            DV22.Text = Ham.layTSBT(DataContect, "22", _lTenRG, 0)[1];
            lab107.Text = Ham.layTSBT(DataContect, "22", _lTenRG, 0)[2];

            lab24.Text = Ham.layTSBT(DataContect, "23", _lTenRG, 0)[0];
            DV23.Text = Ham.layTSBT(DataContect, "23", _lTenRG, 0)[1];
            lab25.Text = Ham.layTSBT(DataContect, "23", _lTenRG, 0)[2];

            lab28.Text = Ham.layTSBT(DataContect, "24", _lTenRG, 0)[0];
            DV24.Text = Ham.layTSBT(DataContect, "24", _lTenRG, 0)[1];
            lab29.Text = Ham.layTSBT(DataContect, "24", _lTenRG, 0)[2];

            lab32.Text = Ham.layTSBT(DataContect, "25", _lTenRG, 0)[0];
            DV25.Text = Ham.layTSBT(DataContect, "25", _lTenRG, 0)[1];
            lab33.Text = Ham.layTSBT(DataContect, "25", _lTenRG, 0)[2];

            lab36.Text = Ham.layTSBT(DataContect, "26", _lTenRG, 0)[0];
            DV26.Text = Ham.layTSBT(DataContect, "26", _lTenRG, 0)[1];
            lab37.Text = Ham.layTSBT(DataContect, "26", _lTenRG, 0)[2];

            lab40.Text = Ham.layTSBT(DataContect, "27", _lTenRG, 0)[0];
            DV27.Text = Ham.layTSBT(DataContect, "27", _lTenRG, 0)[1];
            lab41.Text = Ham.layTSBT(DataContect, "27", _lTenRG, 0)[2];

            lab44.Text = Ham.layTSBT(DataContect, "28", _lTenRG, 0)[0];
            DV28.Text = Ham.layTSBT(DataContect, "28", _lTenRG, 0)[1];
            lab45.Text = Ham.layTSBT(DataContect, "28", _lTenRG, 0)[2];

            lab48.Text = Ham.layTSBT(DataContect, "29", _lTenRG, 0)[0];
            DV29.Text = Ham.layTSBT(DataContect, "29", _lTenRG, 0)[1];
            lab49.Text = Ham.layTSBT(DataContect, "29", _lTenRG, 0)[2];

            lab52.Text = Ham.layTSBT(DataContect, "30", _lTenRG, 0)[0];
            DV30.Text = Ham.layTSBT(DataContect, "30", _lTenRG, 0)[1];
            lab53.Text = Ham.layTSBT(DataContect, "30", _lTenRG, 0)[2];

            lab56.Text = Ham.layTSBT(DataContect, "31", _lTenRG, 0)[0];
            DV31.Text = Ham.layTSBT(DataContect, "31", _lTenRG, 0)[1];
            lab57.Text = Ham.layTSBT(DataContect, "31", _lTenRG, 0)[2];

            lab60.Text = Ham.layTSBT(DataContect, "32", _lTenRG, 0)[0];
            DV32.Text = Ham.layTSBT(DataContect, "32", _lTenRG, 0)[1];
            lab61.Text = Ham.layTSBT(DataContect, "32", _lTenRG, 0)[2];

            lab64.Text = Ham.layTSBT(DataContect, "33", _lTenRG, 0)[0];
            DV33.Text = Ham.layTSBT(DataContect, "33", _lTenRG, 0)[1];
            lab65.Text = Ham.layTSBT(DataContect, "33", _lTenRG, 0)[2];

            lab68.Text = Ham.layTSBT(DataContect, "34", _lTenRG, 0)[0];
            DV34.Text = Ham.layTSBT(DataContect, "34", _lTenRG, 0)[1];
            lab69.Text = Ham.layTSBT(DataContect, "34", _lTenRG, 0)[2];

            lab72.Text = Ham.layTSBT(DataContect, "35", _lTenRG, 0)[0];
            DV35.Text = Ham.layTSBT(DataContect, "35", _lTenRG, 0)[1];
            lab73.Text = Ham.layTSBT(DataContect, "35", _lTenRG, 0)[2];

            lab76.Text = Ham.layTSBT(DataContect, "36", _lTenRG, 0)[0];
            DV36.Text = Ham.layTSBT(DataContect, "36", _lTenRG, 0)[1];
            lab77.Text = Ham.layTSBT(DataContect, "36", _lTenRG, 0)[2];

            lab80.Text = Ham.layTSBT(DataContect, "37", _lTenRG, 0)[0];
            DV37.Text = Ham.layTSBT(DataContect, "37", _lTenRG, 0)[1];
            lab81.Text = Ham.layTSBT(DataContect, "37", _lTenRG, 0)[2];

            lab84.Text = Ham.layTSBT(DataContect, "38", _lTenRG, 0)[0];
            DV38.Text = Ham.layTSBT(DataContect, "38", _lTenRG, 0)[1];
            lab85.Text = Ham.layTSBT(DataContect, "38", _lTenRG, 0)[2];

            lab88.Text = Ham.layTSBT(DataContect, "39", _lTenRG, 0)[0];
            DV39.Text = Ham.layTSBT(DataContect, "39", _lTenRG, 0)[1];
            lab89.Text = Ham.layTSBT(DataContect, "39", _lTenRG, 0)[2];

            lab92.Text = Ham.layTSBT(DataContect, "40", _lTenRG, 0)[0];
            DV40.Text = Ham.layTSBT(DataContect, "40", _lTenRG, 0)[1];
            lab93.Text = Ham.layTSBT(DataContect, "40", _lTenRG, 0)[2];

            lab100.Text = Ham.layTSBT(DataContect, "41", _lTenRG, 0)[0];
            DV41.Text = Ham.layTSBT(DataContect, "41", _lTenRG, 0)[1];
            lab101.Text = Ham.layTSBT(DataContect, "41", _lTenRG, 0)[2];

            lab104.Text = Ham.layTSBT(DataContect, "42", _lTenRG, 0)[0];
            DV42.Text = Ham.layTSBT(DataContect, "42", _lTenRG, 0)[1];
            lab105.Text = Ham.layTSBT(DataContect, "42", _lTenRG, 0)[2];

            lab108.Text = Ham.layTSBT(DataContect, "43", _lTenRG, 0)[0];
            DV43.Text = Ham.layTSBT(DataContect, "43", _lTenRG, 0)[1];
            lab109.Text = Ham.layTSBT(DataContect, "43", _lTenRG, 0)[2];

            lab108.Text = Ham.layTSBT(DataContect, "44", _lTenRG, 0)[0];
            DV43.Text = Ham.layTSBT(DataContect, "44", _lTenRG, 0)[1];
            lab109.Text = Ham.layTSBT(DataContect, "44", _lTenRG, 0)[2];


            hienthiKQ(MaBNhan.Value.ToString(), 1, col1, a1, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 2, col2, a2, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 3, col3, a3, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 4, col4, a4, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 5, col5, a5, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 6, col6, a6, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 7, col7, a7, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 8, col8, a8, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 9, col9, a9, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 10, col10, a10, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 11, col11, a11, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 12, col12, a12, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 13, col13, a13, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 14, col14, a14, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 15, col15, a15, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 16, col16, a16, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 17, col17, a17, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 18, col18, a18, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 19, col19, a19, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 20, col20, a20, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 21, col21, a21, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 22, col22, a22, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 23, col23, a23, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 24, col24, a24, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 25, col25, a25, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 26, col26, a26, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 27, col27, a27, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 28, col28, a28, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 29, col29, a29, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 30, col30, a30, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 31, col31, a31, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 32, col32, a32, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 33, col33, a33, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 34, col34, a34, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 35, col35, a35, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 36, col36, a36, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 37, col37, a37, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 38, col38, a38, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 39, col39, a39, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 40, col40, a40, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 41, col41, a41, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 42, col42, a42, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 43, col43, a43, sophieu);
            hienthiKQ(MaBNhan.Value.ToString(), 44, col44, a44, sophieu);
        }
        private void hienthiKQ(String MaBNhan, int STT, XRTableCell colKQ, XRTableCell colCheck, int sophieu)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(MaBNhan, out rs))
                _int_maBN = Convert.ToInt32(MaBNhan);
            if (_lCLSct.Where(p => p.STT == STT).Count() > 0 && _lCLSct.Where(p => p.STT == STT).First().MaDVct != null)
            {
                colCheck.Text = "X";
            }
            if (_lCLSct.Where(p => p.STT == STT).Count() > 0 && _lCLSct.Where(p => p.STT == STT).First().KetQua != null && _lCLSct.Where(p => p.STT == STT).First().KetQua != "")
            {
                colKQ.Text = _lCLSct.Where(p => p.STT == STT).First().KetQua.ToString();
                double ketqua = Convert.ToDouble(_lCLSct.Where(p => p.STT == STT).First().KetQua);
                String stt = STT.ToString();
                if (DungChung.Ham.kiemtraKQ(DataContect,_int_maBN,STT.ToString(),0, ketqua, new List<String>(){"XN hóa sinh máu"}) == "right")
                {
                    colKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    colKQ.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Bold);
                }
                if (DungChung.Ham.kiemtraKQ(DataContect, _int_maBN, STT.ToString(), 0, ketqua, new List<String>() { "XN hóa sinh máu" }) == "left")
                {
                    colKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    colKQ.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Bold);
                }
            }
        }
        private class C_CLSct
        {
            private string maDVct;
            private String ketQua;
            private int sTT;
            public string MaDVct
            {
                get { return maDVct; }
                set { maDVct = value; }
            }
            public String KetQua
            {
                get { return ketQua; }
                set { ketQua = value; }
            }
            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }

        }
    }
}
