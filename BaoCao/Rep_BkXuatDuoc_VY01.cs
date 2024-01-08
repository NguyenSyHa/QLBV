using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkXuatDuoc_VY01 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _DSKP = new List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP>();
        public Rep_BkXuatDuoc_VY01()
        {

        }
        public Rep_BkXuatDuoc_VY01(List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _lKP)
        {
            InitializeComponent();
            _DSKP = _lKP;
        }
        public void BindingData()
        {
            
            colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            if (Convert.ToInt32(DG.Value) == 1)
            {
                colNuocSX.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0];
            }
            else
            {
                colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            }
             colKP1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[0];
             colKP2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[0];
             colKP3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[0];
             colKP4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
             colKP5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[0];
             colKP6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[0];
             colKP7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[0];
             colKP8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[0];
             colKP9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[0];
             colKP10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[0];
             colKP11.DataBindings.Add("Text", DataSource, "SL11").FormatString = DungChung.Bien.FormatString[0];
             colKP12.DataBindings.Add("Text", DataSource, "Sl12").FormatString = DungChung.Bien.FormatString[0];
             colKP13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[0];
             colKP14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[0];
             colKP15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[0];
             colKP16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[0];
             colKP17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[0];
             colKP18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[0];
             colKP19.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[0];
            //if (DungChung.Bien.MaBV == "26007")
            //    colXuatKhac.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colKPTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));

        }
        private void xrLabel1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colKPTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //int sl1 = 0 ; int sl2 = 0 ;
            //if(this.GetCurrentColumnValue("SL1")!=null)
            //     sl1=Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //if (this.GetCurrentColumnValue("SL1") != null)
            //    sl1 = Convert.ToInt32(this.GetCurrentColumnValue("SL1"));
            //colKPTong.Text = sl1.ToString() + sl2.ToString();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            for (int i = 0; i < _DSKP.Count; i++)
            {
                switch (i)
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
                            KP9.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 9:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP10.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 10:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP11.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 11:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP12.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 12:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP13.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 13:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP14.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 14:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP15.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 15:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP16.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 16:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP17.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 17:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP18.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 18:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            KP19.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;

                }
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void xrTableCell38_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell38.Text = "Phó trưởng khoa phòng khám_dược_CLS";
            }
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

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "26007")
            //    KP20.Text = "Xuất khác";
            //else
            //    KP20.Text = "";
        }

    }
}
