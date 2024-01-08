using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcPhauThuat_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcPhauThuat_CL()
        {
            InitializeComponent();
        }
        string _chon = "";
        public void BindingData()
        {
            colKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            //colCK.DataBindings.Add("Text", DataSource, "CK");
            colDV.DataBindings.Add("Text", DataSource, "TenDV");
            //txtBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            //txtLoai1.DataBindings.Add("Text",DataSource,"Loai1").FormatString=DungChung.Bien.FormatString[1];
            //txtLoai2.DataBindings.Add("Text", DataSource, "Loai2").FormatString = DungChung.Bien.FormatString[1];
            //txtLoai3.DataBindings.Add("Text", DataSource, "Loai3").FormatString = DungChung.Bien.FormatString[1];
            //colTS.DataBindings.Add("Text", DataSource, "TS").FormatString = DungChung.Bien.FormatString[1];
            colBHYTT.DataBindings.Add("Text", DataSource, "bhyt").FormatString = DungChung.Bien.FormatString[1];
            colLoai1T.DataBindings.Add("Text", DataSource, "loai1").FormatString = DungChung.Bien.FormatString[1];
            colLoai2T.DataBindings.Add("Text", DataSource, "loai2").FormatString = DungChung.Bien.FormatString[1];
            colLoai3T.DataBindings.Add("Text", DataSource, "loai3").FormatString = DungChung.Bien.FormatString[1];
            colTST.DataBindings.Add("Text", DataSource, "ts").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader2.GroupFields.Add(new GroupField("TenKP"));
           // GroupHeader1.GroupFields.Add(new GroupField("ChuyenKhoa"));

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTTDV.Text = DungChung.Bien.NguoiLapBieu;
    
            if (_chon != "1")
            {
                colNguoiLapBieu.Text = DungChung.Bien.GiamDoc;
            }
            else colNguoiLapBieu.Text = "";
        }
        int stt = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = false;
            //if (chon.Value.ToString() != null)
            //{
            //    _chon = chon.Value.ToString();
            //    if (_chon == "1")
            //    {
            //         switch (stt)
            //        {
            //            case 1:
            //                colSTTGh1.Text = "I";
            //                break;
            //            case 2:
            //                colSTTGh1.Text = "II";
            //                break;
            //            case 3:
            //                colSTTGh1.Text = "III";
            //                break;
            //            case 4:
            //                colSTTGh1.Text = "IV";
            //                break;
            //            case 5:
            //                colSTTGh1.Text = "V";
            //                break;

            //        }
            //        stt++;
              
               
            //          //if (_chon != "1")
            //        //{colSTTGh1.Visible = false;}
            //    }
            //}
        }

        

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (chon.Value.ToString() != null)
            {
                _chon = chon.Value.ToString();
                if (_chon == "1")
                    GroupHeader2.Visible = false;

            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int v;
            if (this.GetCurrentColumnValue("bhyt") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("bhyt"));
                if (v == 0)
                {
                    colBHYT.Text = "";
                }
                else { colBHYT.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("loai1") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("loai1"));
                if (v == 0)
                {
                    colLoai1.Text = "";
                }
                else { colLoai1.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("loai2") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("loai2"));
                if (v == 0)
                {
                    colLoai2.Text = "";
                }
                else { colLoai2.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("loai3") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("loai3"));
                if (v == 0)
                {
                    colLoai3.Text = "";
                }
                else { colLoai3.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("ts") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("ts"));
                if (v == 0)
                {
                    colTS.Text = "";
                }
                else { colTS.Text = v.ToString("#,##"); }
            }
        }
    }
}
