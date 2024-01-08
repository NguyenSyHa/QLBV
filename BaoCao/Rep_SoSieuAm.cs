using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_SoSieuAm : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoSieuAm()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "30002")
            {
                if (SL.Value.ToString() == "-1")
                {
                    // colKhoa.DataBindings.Add("Text", DataSource, "noigui");
                    xrTableCell58.DataBindings.Add("Text", DataSource, "tenbnhan");
                    // txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
                    xrTableCell59.DataBindings.Add("Text", DataSource, "nam");
                    xrTableCell60.DataBindings.Add("Text", DataSource, "nu");
                    celDC.DataBindings.Add("Text", DataSource, "bhyt");
                    celBHYT.DataBindings.Add("Text", DataSource, "diachi");
                    xrTableCell63.DataBindings.Add("Text", DataSource, "khac");
                    celCD.DataBindings.Add("Text", DataSource, "noigui");
                    //xrTableCell58.DataBindings.Add("Text", DataSource, "SoPhim");
                    celGui.DataBindings.Add("Text", DataSource, "chandoan");
                    xrTableCell66.DataBindings.Add("Text", DataSource, "yeucau");
                    xrTableCell67.DataBindings.Add("Text", DataSource, "ketqua");
                    xrTableCell68.DataBindings.Add("Text", DataSource, "bsth");
                    xrTableCell69.DataBindings.Add("Text", DataSource, "ngayth").FormatString = "{0: HH:mm dd/MM}";
                    // GroupHeader1.GroupFields.Add(new GroupField("noigui"));
                }
                else
                {
                    // colKhoa.DataBindings.Add("Text", DataSource, "noigui");
                    colTenBNhan.DataBindings.Add("Text", DataSource, "tenbnhan");
                    // txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
                    colNam.DataBindings.Add("Text", DataSource, "nam");
                    colNu.DataBindings.Add("Text", DataSource, "nu");
                    colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                    colKhac.DataBindings.Add("Text", DataSource, "khac");
                    colDiaChi.DataBindings.Add("Text", DataSource, "diachi");
                    colNoiGui.DataBindings.Add("Text", DataSource, "noigui");
                    colSLphim.DataBindings.Add("Text", DataSource, "SoPhim");
                    colCDTruocPT.DataBindings.Add("Text", DataSource, "chandoan");
                    colYeuCau.DataBindings.Add("Text", DataSource, "yeucau");
                    colKetQua.DataBindings.Add("Text", DataSource, "ketqua");
                    colBSTH.DataBindings.Add("Text", DataSource, "bsth");
                    colNTH.DataBindings.Add("Text", DataSource, "ngayth").FormatString = "{0: HH:mm dd/MM}";
                    // GroupHeader1.GroupFields.Add(new GroupField("noigui"));
                }
                
            }
            else
            {
                // colKhoa.DataBindings.Add("Text", DataSource, "noigui");
                xrTableCell58.DataBindings.Add("Text", DataSource, "tenbnhan");
                // txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
                xrTableCell59.DataBindings.Add("Text", DataSource, "nam");
                xrTableCell60.DataBindings.Add("Text", DataSource, "nu");
                celDC.DataBindings.Add("Text", DataSource, "diachi");
                celBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                xrTableCell63.DataBindings.Add("Text", DataSource, "khac");
                celCD.DataBindings.Add("Text", DataSource, "chandoan");
                //xrTableCell58.DataBindings.Add("Text", DataSource, "SoPhim");
                celGui.DataBindings.Add("Text", DataSource, "noigui");
                xrTableCell66.DataBindings.Add("Text", DataSource, "yeucau");
                xrTableCell67.DataBindings.Add("Text", DataSource, "ketqua");
                xrTableCell68.DataBindings.Add("Text", DataSource, "bsth");
                xrTableCell69.DataBindings.Add("Text", DataSource, "ngayth").FormatString = "{0: HH:mm dd/MM}";
                // GroupHeader1.GroupFields.Add(new GroupField("noigui"));
            }

        }



        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = false;

            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //string nth = "";
            //if (this.GetCurrentColumnValue("ngayth") != null)
            //{
            //    nth = this.GetCurrentColumnValue("ngayth").ToString();
            //    if (nth == "00/00") { colNTH.Text = ""; }
            //    else
            //    {
            //        colNTH.Text = nth;
            //    }
            //}
            if (SL.Value.ToString() == "-1")
            {
                SubBand3.Visible = false;
            }
            else
            {
                SubBand4.Visible = false;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "30009") { xrTableCell1.Text = "Người đọc"; }
            if (SL.Value.ToString() == "-1")
            {
                SubBand1.Visible = false;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }

        private void colBSTH_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (TenSo.Value.ToString() != null)
            //{
            //    string _ts = TenSo.Value.ToString();
            //    if (DungChung.Bien.MaBV == "30009" && (_ts == "SỔ SIÊU ÂM" || _ts == "SỔ X-QUANG")) { colBSTH.Text = ("Nguyễn Mạnh Thắng").ToString(); }
            //}
        }
    }

}

