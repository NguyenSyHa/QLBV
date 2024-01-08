using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_TongHopHoaSinhMauA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TongHopHoaSinhMauA4()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colHoten.DataBindings.Add("Text", DataSource, "tenBN");
            colGioitinh.DataBindings.Add("Text", DataSource, "gioitinh");
            colTuoi.DataBindings.Add("Text", DataSource, "tuoi");
            colDiachi.DataBindings.Add("Text", DataSource, "diachi");
            xrTableCell14.DataBindings.Add("Text", DataSource, "bhyt");
            colTenKP.DataBindings.Add("Text", DataSource, "noigui");
            colKQ1.DataBindings.Add("Text", DataSource, "KQ1");
            colKQ2.DataBindings.Add("Text", DataSource, "KQ2");
            colKQ3.DataBindings.Add("Text", DataSource, "KQ3");
            colKQ4.DataBindings.Add("Text", DataSource, "KQ4");
            colKQ5.DataBindings.Add("Text", DataSource, "KQ5");
            colKQ6.DataBindings.Add("Text", DataSource, "KQ6");
            colKQ7.DataBindings.Add("Text", DataSource, "KQ7");
            colKQ8.DataBindings.Add("Text", DataSource, "KQ8");
            colKQ9.DataBindings.Add("Text", DataSource, "KQ9");
            colKQ10.DataBindings.Add("Text", DataSource, "KQ10");
            colKQ11.DataBindings.Add("Text", DataSource, "KQ11");
            colKQ12.DataBindings.Add("Text", DataSource, "KQ12");
            colKQ13.DataBindings.Add("Text", DataSource, "KQ13");
            colKQ14.DataBindings.Add("Text", DataSource, "KQ14");
            colKQ15.DataBindings.Add("Text", DataSource, "KQ15");
            colKQ16.DataBindings.Add("Text", DataSource, "KQ16");
            colKQ17.DataBindings.Add("Text", DataSource, "KQ17");
            colKQ18.DataBindings.Add("Text", DataSource, "KQ18");
            colKQ19.DataBindings.Add("Text", DataSource, "KQ19");
            colKQ20.DataBindings.Add("Text", DataSource, "KQ20");
            colKQ21.DataBindings.Add("Text", DataSource, "KQ21");
            colKQ22.DataBindings.Add("Text", DataSource, "KQ22");
            colKQ23.DataBindings.Add("Text", DataSource, "KQ23");
            colKQ24.DataBindings.Add("Text", DataSource, "KQ24");
            colKQ25.DataBindings.Add("Text", DataSource, "KQ25");
            T1.DataBindings.Add("Text", DataSource, "KQ1");
            T2.DataBindings.Add("Text", DataSource, "KQ2");
            T3.DataBindings.Add("Text", DataSource, "KQ3");
            T4.DataBindings.Add("Text", DataSource, "KQ4");
            T5.DataBindings.Add("Text", DataSource, "KQ5");
            T6.DataBindings.Add("Text", DataSource, "KQ6");
            T7.DataBindings.Add("Text", DataSource, "KQ7");
            T8.DataBindings.Add("Text", DataSource, "KQ8");
            T9.DataBindings.Add("Text", DataSource, "KQ9");
            T10.DataBindings.Add("Text", DataSource, "KQ10");
            T11.DataBindings.Add("Text", DataSource, "KQ11");
            T12.DataBindings.Add("Text", DataSource, "KQ12");
            T13.DataBindings.Add("Text", DataSource, "KQ13");
            T14.DataBindings.Add("Text", DataSource, "KQ14");
            T15.DataBindings.Add("Text", DataSource, "KQ15");
            T16.DataBindings.Add("Text", DataSource, "KQ16");
            T17.DataBindings.Add("Text", DataSource, "KQ17");
            T18.DataBindings.Add("Text", DataSource, "KQ18");
            T19.DataBindings.Add("Text", DataSource, "KQ19");
            T20.DataBindings.Add("Text", DataSource, "KQ20");
            T21.DataBindings.Add("Text", DataSource, "KQ21");
            T22.DataBindings.Add("Text", DataSource, "KQ22");
            T23.DataBindings.Add("Text", DataSource, "KQ23");
            T24.DataBindings.Add("Text", DataSource, "KQ24");
            T25.DataBindings.Add("Text", DataSource, "KQ25");
            //colKQ26.DataBindings.Add("Text", DataSource, "KQ26");
            //colKQ27.DataBindings.Add("Text", DataSource, "KQ27");
            //colKQ28.DataBindings.Add("Text", DataSource, "KQ28");
            //colKQ29.DataBindings.Add("Text", DataSource, "KQ29");
            //colKQ30.DataBindings.Add("Text", DataSource, "KQ30");
            colChuanDoan.DataBindings.Add("Text", DataSource, "chuandoan");
        }
        public void setTieuDe(List<DichVuct> _lDvct)
        {
            for (int i = 0; i < _lDvct.Count; i++)
            {
                string STT = i.ToString();
                switch (STT)
                {
                    case "0":
                        txt1.Text = _lDvct[i].TenDVct;
                        break;
                    case "1":
                        txt2.Text = _lDvct[i].TenDVct;
                        break;
                    case "2":
                        txt3.Text = _lDvct[i].TenDVct;
                        break;
                    case "3":
                        txt4.Text = _lDvct[i].TenDVct;
                        break;
                    case "4":
                        txt5.Text = _lDvct[i].TenDVct;
                        break;
                    case "5":
                        txt6.Text = _lDvct[i].TenDVct;
                        break;
                    case "6":
                        txt7.Text = _lDvct[i].TenDVct;
                        break;
                    case "7":
                        txt8.Text = _lDvct[i].TenDVct;
                        break;
                    case "8":
                        txt9.Text = _lDvct[i].TenDVct;
                        break;
                    case "9":
                        txt10.Text = _lDvct[i].TenDVct;
                        break;
                    case "10":
                        txt11.Text = _lDvct[i].TenDVct;
                        break;
                    case "11":
                        txt12.Text = _lDvct[i].TenDVct;
                        break;
                    case "12":
                        txt13.Text = _lDvct[i].TenDVct;
                        break;
                    case "13":
                        txt14.Text = _lDvct[i].TenDVct;
                        break;
                    case "14":
                        txt15.Text = _lDvct[i].TenDVct;
                        break;
                    case "15":
                        txt16.Text = _lDvct[i].TenDVct;
                        break;
                    case "16":
                        txt17.Text = _lDvct[i].TenDVct;
                        break;
                    case "17":
                        txt18.Text = _lDvct[i].TenDVct;
                        break;
                    case "18":
                        txt19.Text = _lDvct[i].TenDVct;
                        break;
                    case "19":
                        txt20.Text = _lDvct[i].TenDVct;
                        break;
                    case "20":
                        txt21.Text = _lDvct[i].TenDVct;
                        break;
                    case "21":
                        txt22.Text = _lDvct[i].TenDVct;
                        break;
                    case "22":
                        txt23.Text = _lDvct[i].TenDVct;
                        break;
                    case "23":
                        txt24.Text = _lDvct[i].TenDVct;
                        break;
                    case "24":
                        txt25.Text = _lDvct[i].TenDVct;
                        break;
                }
            }
        }
    }
}
