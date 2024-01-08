using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class repKQNoiSoiTMH_YM : DevExpress.XtraReports.UI.XtraReport
    {
        public repKQNoiSoiTMH_YM()
        {
            InitializeComponent();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        public void hienthi(string ketqua, string duongdan) {
            string[] arrKQ = QLBV_Ham.LayChuoi('|', ketqua);
            string[] arrDD = QLBV_Ham.LayChuoi('|', duongdan);
            int demanh = 1;
            for (int i = 0; i < arrDD.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(arrDD[i]) || String.IsNullOrEmpty(arrDD[i]))
                {
                    continue;
                }
                switch (demanh) 
                { 
                    case 1:
                        xrpic1.ImageUrl = arrDD[i];
                        demanh++;
                        break;
                    case 2: 
                        xrpic2.ImageUrl = arrDD[i];
                        demanh++;
                        break;
                    case 3:
                        xrpic3.ImageUrl = arrDD[i];
                        demanh++;
                        break;
                    case 4:
                        xrpic4.ImageUrl = arrDD[i];
                        demanh++;
                        break;
                }
            }
            string chuoimoi = "";
            for (int i = 0; i < arrKQ.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(arrKQ[i]) || String.IsNullOrEmpty(arrKQ[i]))
                {
                    continue;
                }
                switch (i)
                {
                    case 0:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 1:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 2:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 3:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 4:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 5:
                        chuoimoi += "  " + arrKQ[i] + "\r\n";
                        break;
                    case 6:
                        chuoimoi += "  " + arrKQ[i];
                        break;
                }
            }
            xrRTKetQua.Text = chuoimoi;
            
        }
    }
}
