using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_Sotonghopsinhde : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Sotonghopsinhde()
        {
            InitializeComponent();
        }
        public void bindingData(){
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colNgheNghiep.DataBindings.Add("Text", DataSource, "TenNN");
            colLanDe.DataBindings.Add("Text", DataSource, "SoLD");
            colNgayDe.DataBindings.Add("Text", DataSource, "NgaySinhc").FormatString="{0:dd/MM/yyyy}";
            colNoiDe1.DataBindings.Add("Text", DataSource, "NoiDe1");
            colNoiDe2.DataBindings.Add("Text", DataSource, "NoiDe2");
            colNoiDe3.DataBindings.Add("Text", DataSource, "NoiDe3");
            colTienSu.DataBindings.Add("Text", DataSource, "TienSu");
            colDienBien1.DataBindings.Add("Text", DataSource, "DBien1");
            colDienBien2.DataBindings.Add("Text", DataSource, "DBien2");
            colDienBien3.DataBindings.Add("Text", DataSource, "DBien3");
            colDienBien4.DataBindings.Add("Text", DataSource, "DBien4");
            colTaiBien1.DataBindings.Add("Text", DataSource, "TaiBien1");
            colTaiBien2.DataBindings.Add("Text", DataSource, "TaiBien2");
            colTaiBien3.DataBindings.Add("Text", DataSource, "TaiBien3");
            colTaiBien4.DataBindings.Add("Text", DataSource, "TaiBien4");
            colTinhTrang1.DataBindings.Add("Text", DataSource, "TinhTrang1");
            colTinhTrang2.DataBindings.Add("Text", DataSource, "TinhTrang2");
            colThaiChet1.DataBindings.Add("Text", DataSource, "ThaiChet1");
            colThaiChet2.DataBindings.Add("Text", DataSource, "ThaiChet2");
            colThaiChet3.DataBindings.Add("Text", DataSource, "ThaiChet3");
            colThaiChet4.DataBindings.Add("Text", DataSource, "ThaiChet4");

        }
    }
}
