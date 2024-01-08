using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_TongHopHuyetHoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TongHopHuyetHoc()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colHoten.DataBindings.Add("Text", DataSource, "tenBN");
            colGioitinh.DataBindings.Add("Text", DataSource, "gioitinh");
            colTuoi.DataBindings.Add("Text", DataSource, "tuoi");
            colDiachi.DataBindings.Add("Text", DataSource, "diachi");
            colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
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
            colKQ26.DataBindings.Add("Text", DataSource, "KQ26");
            colKQ27.DataBindings.Add("Text", DataSource, "KQ27");
            colKQ28.DataBindings.Add("Text", DataSource, "KQ28");
            colKQ29.DataBindings.Add("Text", DataSource, "KQ29");
            colKQ30.DataBindings.Add("Text", DataSource, "KQ30");
            colKQ1T.DataBindings.Add("Text", DataSource, "KQ1");
            colKQ2T.DataBindings.Add("Text", DataSource, "KQ2");
            colKQ3T.DataBindings.Add("Text", DataSource, "KQ3");
            colKQ4T.DataBindings.Add("Text", DataSource, "KQ4");
            colKQ5T.DataBindings.Add("Text", DataSource, "KQ5");
            colKQ6T.DataBindings.Add("Text", DataSource, "KQ6");
            colKQ7T.DataBindings.Add("Text", DataSource, "KQ7");
            colKQ8T.DataBindings.Add("Text", DataSource, "KQ8");
            colKQ9T.DataBindings.Add("Text", DataSource, "KQ9");
            colKQ10T.DataBindings.Add("Text", DataSource, "KQ10");
            colKQ11T.DataBindings.Add("Text", DataSource, "KQ11");
            colKQ12T.DataBindings.Add("Text", DataSource, "KQ12");
            colKQ13T.DataBindings.Add("Text", DataSource, "KQ13");
            colKQ14T.DataBindings.Add("Text", DataSource, "KQ14");
            colKQ15T.DataBindings.Add("Text", DataSource, "KQ15");
            colKQ16T.DataBindings.Add("Text", DataSource, "KQ16");
            colKQ17T.DataBindings.Add("Text", DataSource, "KQ17");
            colKQ18T.DataBindings.Add("Text", DataSource, "KQ18");
            colKQ19T.DataBindings.Add("Text", DataSource, "KQ19");
            colKQ20T.DataBindings.Add("Text", DataSource, "KQ20");
            colKQ21T.DataBindings.Add("Text", DataSource, "KQ21");
            colKQ22T.DataBindings.Add("Text", DataSource, "KQ22");
            colKQ23T.DataBindings.Add("Text", DataSource, "KQ23");
            colKQ24T.DataBindings.Add("Text", DataSource, "KQ24");
            colKQ25T.DataBindings.Add("Text", DataSource, "KQ25");
            colKQ26T.DataBindings.Add("Text", DataSource, "KQ26");
            colKQ27T.DataBindings.Add("Text", DataSource, "KQ27");
            colKQ28T.DataBindings.Add("Text", DataSource, "KQ28");
            colKQ29T.DataBindings.Add("Text", DataSource, "KQ29");
            colKQ30T.DataBindings.Add("Text", DataSource, "KQ30");
            colChuanDoan.DataBindings.Add("Text", DataSource, "chuandoan");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dvct1 = (from tn in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains("XN huyết học".ToLower()))
                        join dv in _Data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                        join dvct in _Data.DichVucts on dv.MaDV equals dvct.MaDV
                         join clsct in _Data.CLScts on dvct.MaDVct equals clsct.MaDVct
                        select new { dvct.TenDVct, dvct.STT }).ToList();
            if (dvct1.Count > 0)
            {
                foreach (var a in dvct1)
                {
                    if (a.STT != null)
                    {
                        string STT=a.STT.ToString();
                        switch (STT)
                        {
                            case "1":
                                txt1.Text = a.TenDVct;
                                break;
                            case "2":
                                txt2.Text = a.TenDVct;
                                break;
                            case "3":
                                txt3.Text = a.TenDVct;
                                break;
                            case "4":
                                txt4.Text = a.TenDVct;
                                break;
                            case "5":
                                txt5.Text = a.TenDVct;
                                break;
                            case "6":
                                txt6.Text = a.TenDVct;
                                break;
                            case "7":
                                txt7.Text = a.TenDVct;
                                break;
                            case "8":
                                txt8.Text = a.TenDVct;
                                break;
                            case "9":
                                txt9.Text = a.TenDVct;
                                break;
                            case "10":
                                txt10.Text = a.TenDVct;
                                break;
                            case "11":
                                txt11.Text = a.TenDVct;
                                break;
                            case "12":
                                txt12.Text = a.TenDVct;
                                break;
                            case "13":
                                txt13.Text = a.TenDVct;
                                break;
                            case "14":
                                txt14.Text = a.TenDVct;
                                break;
                            case "15":
                                txt15.Text = a.TenDVct;
                                break;
                            case "16":
                                txt16.Text = a.TenDVct;
                                break;
                            case "17":
                                txt17.Text = a.TenDVct;
                                break;
                            case "18":
                                txt18.Text = a.TenDVct;
                                break;
                            case "19":
                                txt19.Text = a.TenDVct;
                                break;
                            case "20":
                                txt20.Text = a.TenDVct;
                                break;
                            case "21":
                                txt21.Text = a.TenDVct;
                                break;
                            case "22":
                                txt22.Text = a.TenDVct;
                                break;
                            case "23":
                                txt23.Text = a.TenDVct;
                                break;
                            case "24":
                                txt24.Text = a.TenDVct;
                                break;
                            case "25":
                                txt25.Text = a.TenDVct;
                                break;
                            case "26":
                                txt26.Text = a.TenDVct;
                                break;
                            case "27":
                                txt27.Text = a.TenDVct;
                                break;
                            case "28":
                                txt28.Text = a.TenDVct;
                                break;
                            case "29":
                                txt29.Text = a.TenDVct;
                                break;
                            case "30":
                                txt30.Text = a.TenDVct;
                                break;
                        }
                    }
                }
            }
        }

    }
}
