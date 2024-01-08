using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoVaoRaChuyenVien_12122_A3_ThemCot : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoVaoRaChuyenVien_12122_A3_ThemCot()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan1");
          //  xrLabel12.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text",DataSource,"Nam1");
            colNu.DataBindings.Add("Text", DataSource, "Nu1"); 
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT1");
            colTuoi12.DataBindings.Add("Text",DataSource,"TE121");
            colTuoi15.DataBindings.Add("Text", DataSource, "TE151");
            colNgheNghiep.DataBindings.Add("Text", DataSource, "NgheNghiep1");
            colDiaChi.DataBindings.Add("Text",DataSource,"DiaChi1");
            colNoiGT.DataBindings.Add("Text",DataSource,"NoiGT1");
            colVV.DataBindings.Add("Text", DataSource, "VV1").FormatString="{0:dd/MM}";
            colCV.DataBindings.Add("Text", DataSource, "CV1").FormatString = "{0:dd/MM}";
            colRV.DataBindings.Add("Text", DataSource, "RV1").FormatString = "{0:dd/MM}";
            colTuVong.DataBindings.Add("Text", DataSource, "TV1").FormatString = "{0:dd/MM}";
            colCDTD.DataBindings.Add("Text", DataSource, "CDTD1");
            colCDKKB.DataBindings.Add("Text", DataSource, "CDKKB1");
            colCDKDT.DataBindings.Add("Text", DataSource, "CDKDT1");
            colKQKhoi.DataBindings.Add("Text",DataSource,"Khoi1");
            colKQDoGiam.DataBindings.Add("Text", DataSource, "DoGiam1");
            colKQNangHon.DataBindings.Add("Text", DataSource, "NangHon1");
            colChuyenKhoaDi.DataBindings.Add("Text", DataSource, "NgayChuyenKhoa").FormatString = "{0:dd/MM}";
            colChuyenKhoaDen.DataBindings.Add("Text", DataSource, "RV3").FormatString = "{0:dd/MM}";
            colKQKoTD.DataBindings.Add("Text", DataSource, "KoTD1");
            colTSNgayDT.DataBindings.Add("Text", DataSource, "SoNgaydt1").FormatString="{0:##,###}";
            if(DungChung.Bien.MaBV == "12122")
                colTSNgayDT.DataBindings.Add("Text", DataSource, "SoNgaydt1").FormatString = "{0:##,0.##}";
            colSoBA.DataBindings.Add("Text", DataSource, "SoBA");
            colSoLT.DataBindings.Add("Text", DataSource, "SoLT");
            celTenKP.DataBindings.Add("Text", DataSource, "KPChuyen");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

       
    }
}
