using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_Sokhambenh : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_Sokhambenh()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "tenbn");
            txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            if (DungChung.Bien.MaBV == "30003")
            {
                colCDKKB.DataBindings.Add("Text", DataSource, "ticd");
            }
            else
            { colCDKKB.DataBindings.Add("Text", DataSource, "CD"); }
            colCDDT.DataBindings.Add("Text", DataSource, "cddt");
            colVVien.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru.DataBindings.Add("Text", DataSource, "NT1");
            colVenha.DataBindings.Add("Text", DataSource, "VN1");
            colTT.DataBindings.Add("Text", DataSource, "ThuThuat1");
            colChuyenkhoa.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            colThuphi.DataBindings.Add("Text", DataSource, "dtuongtp");
            colMienphi.DataBindings.Add("Text", DataSource, "dtuongbh");
            colCapcuu.DataBindings.Add("Text", DataSource, "capcuu");
            //TXTCT.DataBindings.Add("Text", DataSource, "ct");
        }

        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void colCDDT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("phuongan") != null)
            //{
            //    string PA = this.GetCurrentColumnValue("phuongan").ToString();
            //    switch (PA)
            //    {
            //        case "1":
            //            if (this.GetCurrentColumnValue("ct") != null)
            //            {
            //                int RV = Convert.ToInt32(this.GetCurrentColumnValue("ct").ToString());
            //                if (RV == 2)
            //                {
            //                    colVVien.Text = "X";
            //                    colNgoaitru.Text = " ";
            //                    colTuyentren.Text = " ";
            //                }
            //                else
            //                {
            //                    colVVien.Text = " ";
            //                    colNgoaitru.Text = " ";
            //                    colTuyentren.Text = "X";
            //                }
            //            }
            //            break;
            //        case "0":
            //            if (this.GetCurrentColumnValue("ct") != null)
            //            {
            //                int RV = Convert.ToInt32(this.GetCurrentColumnValue("ct").ToString());
            //                if (RV == 2)
            //                {
            //                    colVVien.Text = " ";
            //                    colNgoaitru.Text = "X";
            //                    colTuyentren.Text = " ";
            //                }
            //                else
            //                {
            //                    colVVien.Text = " ";
            //                    colNgoaitru.Text = " ";
            //                    colTuyentren.Text = "X";
            //                }
            //            }
            //            break;
                    //default:
                    //    colNgoaitru.Text = "";
                    //    colVVien.Text = " ";
                    //    colTuyentren.Text = "X";
                    //    break;
            //    }
            //}
        }

     

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper(); ;

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _mabn = "", _cd = "";
            //if (this.GetCurrentColumnValue("maBN") != null)
            //{
            //    _mabn = this.GetCurrentColumnValue("maBN").ToString();
            //    var tt = (from dt in _Data.DThuocs.Where(p => p.MaBNhan.Equals(_mabn))
            //              join dtct in _Data.DThuoccts on dt.IDDon equals dtct.IDDon
            //              join dv in _Data.DichVus.Where(p=>p.Status==1) on dtct.MaDV equals dv.MaDV
            //              join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //              select new { tn.TenTN}).ToList();
            //    if (tt.Count > 0)
            //    {
            //        for (int i = 1; i <= tt.Count; i++)
            //        {
            //            switch (i)
            //            {
            //                case 1:
            //                    _cd = tt[i - 1].TenTN.ToString();
            //                    break;
            //                case 2:
            //                    _cd = tt[i - 2].TenTN.ToString() +"; "+ tt[i - 2].TenTN.ToString();
            //                        break;
            //                case 3:
            //                        _cd = tt[i - 3].TenTN.ToString() + "; " + tt[i - 2].TenTN.ToString() + "; " + tt[i - 1].TenTN.ToString();
            //                       break;
            //                case 4:
            //                       _cd = tt[i - 4].TenTN.ToString() + "; " + tt[i - 3].TenTN.ToString() + "; " + tt[i - 2].TenTN.ToString() + "; " + tt[i - 1].TenTN.ToString();
            //                       break;
            //                case 5:
            //                       _cd = tt[i - 5].TenTN.ToString() + "; " + tt[i - 4].TenTN.ToString() + "; " + tt[i - 3].TenTN.ToString() + "; " + tt[i - 2].TenTN.ToString()+ "; " + tt[i - 1].TenTN.ToString();
            //                       break;
            //                case 6:
            //                       _cd = tt[i - 6].TenTN.ToString() + tt[i - 5].TenTN.ToString() + "; " + tt[i - 4].TenTN.ToString() + "; " + tt[i - 3].TenTN.ToString() + "; " + tt[i - 2].TenTN.ToString() + "; " + tt[i - 1].TenTN.ToString();
            //                       break;
            //             }
            //        }
            //    }
            //    if (_cd != null) { colCDDT.Text = _cd; } else { colCDDT.Text = ""; }
            //}
            
        }

      
       
    }
}
