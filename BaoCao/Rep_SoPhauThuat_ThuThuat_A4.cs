using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoPhauThuat_ThuThuat_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoPhauThuat_ThuThuat_A4()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            if(DungChung.Bien.MaBV == "30010")
            {
                colGhiChu1.DataBindings.Add("Text", DataSource, "NoiGui");
                colTenBNhan1.DataBindings.Add("Text", DataSource, "TenBNhan");
                xrLabel23.DataBindings.Add("Text", DataSource, "MaBNhan");
                colNam1.DataBindings.Add("Text", DataSource, "Nam");
                colNu1.DataBindings.Add("Text", DataSource, "Nu");
                colBHYT1.DataBindings.Add("Text", DataSource, "BHYT");
                colDiaChi1.DataBindings.Add("Text", DataSource, "DiaChi");
                colCDTruocPT1.DataBindings.Add("Text", DataSource, "ChanDoan");
                colSauPT1.DataBindings.Add("Text", DataSource, "KetLuan");
                colPPPT1.DataBindings.Add("Text", DataSource, "YeuCau");
                colPPVC1.DataBindings.Add("Text", DataSource, "PPVC");
                colLoaiPT1.DataBindings.Add("Text", DataSource, "Loai");
                colNgayPTtruoc.DataBindings.Add("Text", DataSource, "NgayBD");
                colNgayPTsau.DataBindings.Add("Text", DataSource, "NgayTH");
                colBSPT1.DataBindings.Add("Text", DataSource, "BSTH");
                colBSGM1.DataBindings.Add("Text", DataSource, "BSGM");

            }
           else
            {
                colGhiChu.DataBindings.Add("Text", DataSource, "NoiGui");
                colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan");
                txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
                colNam.DataBindings.Add("Text",DataSource,"Nam");
                colNu.DataBindings.Add("Text",DataSource,"Nu");
                colBHYT.DataBindings.Add("Text",DataSource,"BHYT");
                colDiaChi.DataBindings.Add("Text",DataSource,"DiaChi");
                colCDTruocPT.DataBindings.Add("Text", DataSource, "ChanDoan");
                colSauPT.DataBindings.Add("Text", DataSource, "KetLuan");
                colNgayPT.DataBindings.Add("Text", DataSource, "NgayTH");
                colNgayPTtruoc.DataBindings.Add("Text", DataSource, "NgayTH");
                colPPPT.DataBindings.Add("Text", DataSource, "YeuCau");
                colPPVC.DataBindings.Add("Text", DataSource, "PPVC");
                colLoaiPT.DataBindings.Add("Text", DataSource, "Loai");
                colBSPT.DataBindings.Add("Text", DataSource, "BSTH");
                colBSGM.DataBindings.Add("Text", DataSource, "BSGM");
            }
            
          //  GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell1.Text = "Người giúp việc";
            }
            if (DungChung.Bien.MaBV == "30010")
            {
                xrTable3.Visible = false;
                SubBand1.Visible = true;
            }
            else
            {
                xrTable3.Visible = true;
            }

        }
       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text=DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = true;
            if (DungChung.Bien.MaBV == "30010")
            {
                SubBand3.Visible = true;
                xrTable4.Visible = false;
            }
            else
            {
                xrTable4.Visible = true;
            }         
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30010")
            {
                xrTable1.Visible = false;
                SubBand2.Visible = true;
            }
            else
            {
                xrTable1.Visible = true;
            }
            string nth = "";
            string nkt = "";
            if (this.GetCurrentColumnValue("ngayth") != null)
            {
                
                nth = this.GetCurrentColumnValue("ngayth").ToString();
                if (nth == "00/00")
                {
                    colNgayPT.Text = "";
                }
                else
                {
                    if(DungChung.Bien.MaBV == "30010")
                    {
                        
                        if (this.GetCurrentColumnValue("ngaykt") != null)
                        {
                            nkt = this.GetCurrentColumnValue("ngaykt").ToString();
                            colNgayPTsau.Text = nkt;
                        }
                        if (nkt == "00/00")
                        {
                            colNgayPTsau.Text = "";
                        }
                        colNgayPTtruoc.Text = nth;
                    }
                    else
                    {
                        colNgayPT.Text = nth;
                    }
                    
                }         
            }
        }
    }
}
