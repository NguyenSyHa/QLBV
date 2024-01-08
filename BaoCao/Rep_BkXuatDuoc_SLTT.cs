using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkXuatDuoc_SLTT : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _DSKP = new List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP>();
        public Rep_BkXuatDuoc_SLTT()
        {

        }
        public Rep_BkXuatDuoc_SLTT(List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _lKP)
        {
            InitializeComponent();
            _DSKP = _lKP;
        }
        public int j = 0, f = 0;

        public void BindingData(int i, int k)
        {
            this.j = i;
            this.f = k;
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell38.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";
            }
            if (DungChung.Bien.MaBV == "30003")
            {

                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrTable7.Visible = true;
                xrTable2.Visible = false;
                colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
                MaNB.DataBindings.Add("Text", DataSource, "MaTam");
                TenDV.DataBindings.Add("Text", DataSource, "TenDV");
                DVT.DataBindings.Add("Text", DataSource, "DonVi");
                if (Convert.ToInt32(DG.Value) == 1)
                {
                    NuocSX.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                else
                {
                    NuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
                }
                string SL = "SL";
                string TT = "TT";

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP1.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP2.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP3.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP4.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP5.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP6.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP7.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();

                cKP8.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                cKP10.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                TT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                if (j + 10 < k)
                {
                    cKP9.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                    TT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                    colRP_TT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                    colGF_TT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                }
                else
                {
                    cKP9.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[0];
                    TT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                    colRP_TT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                    colGF_TT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                }
                GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));
            }
            else
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                xrTable2.Visible = true;
                xrTable7.Visible = false;
                colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                if (Convert.ToInt32(DG.Value) == 1)
                {
                    colNuocSX.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                else
                {
                    colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
                }
                string SL = "SL";
                string TT = "TT";

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP1.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT1.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP2.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT2.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP3.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT3.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP4.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT4.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP5.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT5.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP6.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT6.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP7.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT7.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();

                colKP8.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT8.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                colKP10.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                colTT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colRP_TT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                colGF_TT10.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];

                i++;
                SL = "SL" + i.ToString();
                TT = "TT" + i.ToString();
                if (j + 10 < k)
                {
                    colKP9.DataBindings.Add("Text", DataSource, SL).FormatString = DungChung.Bien.FormatString[0];
                    colTT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                    colRP_TT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                    colGF_TT9.DataBindings.Add("Text", DataSource, TT).FormatString = DungChung.Bien.FormatString[0];
                }
                else
                {
                    colKP9.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[0];
                    colTT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                    colRP_TT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                    colGF_TT9.DataBindings.Add("Text", DataSource, "TCTT").FormatString = DungChung.Bien.FormatString[0];
                }
                GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));
            }
            

        }
        private void xrLabel1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colKPTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //int sl1 = 0; int sl2 = 0;
            //if(this.GetCurrentColumnValue("SL1")!=null)
            //     sl1=Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //if (this.GetCurrentColumnValue("SL1") != null)
            //    sl1 = Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //colKPTong.Text = sl1.ToString() + sl2.ToString();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
          
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();

            for (int i = j; i < _DSKP.Count; i++)
            {
                switch (i - j)
                {
                    case 0:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP1.Value = _DSKP.Skip(i).First().tenkp;
                        }
                        break;
                    case 1:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP2.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 2:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP3.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 3:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP4.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 4:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP5.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 5:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP6.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 6:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP7.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 7:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP8.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 8:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP10.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 9:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP9.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                }
            }
            if (j + 10 >= f)
            {
                KP9.Value = "Tổng xuất";
            }

            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void xrLabel3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(DG.Value) == 1)
            {
                xrTableCell22.Text = "Đơn giá";
                colNuocSX.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

            }
            else
            {
                xrTableCell22.Text = "Nước sản xuất";
            }

        }

    }
}
