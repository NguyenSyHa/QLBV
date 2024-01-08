using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoVaoRaChuyenVien_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoVaoRaChuyenVien_12121()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            celSoVV.DataBindings.Add("Text", DataSource, "SoVV");
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan1");
          //  xrLabel12.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text",DataSource,"Nam1");
            colNu.DataBindings.Add("Text", DataSource, "Nu1");
            colThanhThi.DataBindings.Add("Text", DataSource, "ThanhThi");
            colNongThon.DataBindings.Add("Text", DataSource, "NongThon");
            colCongVC.DataBindings.Add("Text", DataSource, "CongVienChuc");
            colTuoi12.DataBindings.Add("Text",DataSource,"TE121");
            colTuoi3.DataBindings.Add("Text", DataSource, "TE3Tuoi");
            colTuoi15.DataBindings.Add("Text", DataSource, "TE151");
            colNgheNghiep.DataBindings.Add("Text", DataSource, "NgheNghiep1");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi1");
            colLienHe.DataBindings.Add("Text", DataSource, "NThan");
            colCapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            colKhoaVV.DataBindings.Add("Text", DataSource, "KhoaVV");
            colNgayVV.DataBindings.Add("Text", DataSource, "VV1").FormatString = "{0:dd/MM}";
            colChuyenKhoa.DataBindings.Add("Text", DataSource, "NgayChuyenKhoa").FormatString = "{0:dd/MM}";
            colCV.DataBindings.Add("Text", DataSource, "CV1").FormatString = "{0:dd/MM}";
            colRV.DataBindings.Add("Text", DataSource, "RV1").FormatString = "{0:dd/MM}";
            colTongNgayDTri.DataBindings.Add("Text", DataSource, "SoNgaydt1").FormatString = "{0:##,###}";
            col_CDNoiGT.DataBindings.Add("Text", DataSource, "CDTD1");

            colCDKKB.DataBindings.Add("Text", DataSource, "chanDoan");

            colCDKDTVao.DataBindings.Add("Text", DataSource, "CDVV");
            colCDRa.DataBindings.Add("Text", DataSource, "CDKDT1");           
            colKQKhoi.DataBindings.Add("Text",DataSource,"Khoi1");
            colKQDoGiam.DataBindings.Add("Text", DataSource, "DoGiam1");
            colKQKoTD.DataBindings.Add("Text", DataSource, "KoTD1");
            colKQNangHon.DataBindings.Add("Text", DataSource, "NangHon1");
            colKQ_Chet.DataBindings.Add("Text", DataSource, "KQTuVong");
            
            
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
