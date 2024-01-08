using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_VienPhiHangNgay_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_VienPhiHangNgay_27023()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiachi.DataBindings.Add("Text", DataSource, "DChi");
            celSoPhieu.DataBindings.Add("Text", DataSource, "MaBNhan");

            celXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            
            celSieuAm.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK.DataBindings.Add("Text", DataSource, "ChupCT").FormatString = DungChung.Bien.FormatString[1];
            celCongKham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC.DataBindings.Add("Text", DataSource, "DoCNHH").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];          
            celBHNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];          
            
            celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celXN_G.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
           
            celSieuAm_G.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_G.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_G.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_G.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_G.DataBindings.Add("Text", DataSource, "ChupCT").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_G.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_G.DataBindings.Add("Text", DataSource, "DoCNHH").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_G.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celBHNgoaiTru_G.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];

            //celTong_G.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
           

            celXN_R.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
           
            celSieuAm_R.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_R.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_R.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_R.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_R.DataBindings.Add("Text", DataSource, "ChupCT").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_R.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_R.DataBindings.Add("Text", DataSource, "DoCNHH").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_R.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celBHNgoaiTru_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];

            //celTong_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
           

            GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));

        }
    }
}
