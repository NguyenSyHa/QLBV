using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoPhauThuat_ThuThuat : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoPhauThuat_ThuThuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            //if (DungChung.Bien.MaBV == "01071")
            //{
            //    colGhiChu.DataBindings.Add("Text", DataSource, "MaBNhan");
            //    xrTableCell29.Text = "Mã bệnh nhân";
            //}
            //else
            colGhiChu.DataBindings.Add("Text", DataSource, "NoiGui");
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan");
            txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text",DataSource,"Nam");
            colNu.DataBindings.Add("Text",DataSource,"Nu");
            colBHYT.DataBindings.Add("Text",DataSource,"BHYT");
            colDiaChi.DataBindings.Add("Text",DataSource,"DiaChi");
            colCDTruocPT.DataBindings.Add("Text", DataSource, "ChanDoan");
            colSauPT.DataBindings.Add("Text",DataSource,"KetLuan");
            //colNgayPT.DataBindings.Add("Text", DataSource, "NgayTH");
            colPPPT.DataBindings.Add("Text", DataSource, "YeuCau");
            colPPVC.DataBindings.Add("Text", DataSource, "PPVC");
            colLoaiPT.DataBindings.Add("Text", DataSource, "Loai");
            colBSPT.DataBindings.Add("Text", DataSource, "BSTH");
            colBSGM.DataBindings.Add("Text", DataSource, "BSGM");
           // GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
            
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell1.Text = "Người giúp việc";
                xrTableCell27.Text = "Người thực hiện";
                
            }
 
        }

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text=DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = false;
   
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string nth = "";
            if (this.GetCurrentColumnValue("ngayth") != null)
            {
                nth = this.GetCurrentColumnValue("ngayth").ToString();
                if (nth == "00/00") { colNgayPT.Text = ""; }
                else
                {
                    colNgayPT.Text = nth;
                }
            }
        }
    }
}
