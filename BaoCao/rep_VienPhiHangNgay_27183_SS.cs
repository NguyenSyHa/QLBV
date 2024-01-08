using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_VienPhiHangNgay_27183_SS : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_VienPhiHangNgay_27183_SS()
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
            celTDCN.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celDoLX.DataBindings.Add("Text", DataSource, "DoLoangXuong").FormatString = DungChung.Bien.FormatString[1];
            celLHN.DataBindings.Add("Text", DataSource, "LuuHuyetNao").FormatString = DungChung.Bien.FormatString[1];
            celDND.DataBindings.Add("Text", DataSource, "DienNaoDo").FormatString = DungChung.Bien.FormatString[1];
            celTienThuoc.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            celTienVTYT.DataBindings.Add("Text", DataSource, "TienVTYT").FormatString = DungChung.Bien.FormatString[1];
           
            celTienBN.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBH.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienVTKhongTT.DataBindings.Add("Text", DataSource, "TienKTT").FormatString = DungChung.Bien.FormatString[1];
            celTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            celTienBN.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTong.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXN_G.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celTDCN_G.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm_G.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_G.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_G.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_G.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_G.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_G.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_G.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_G.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celDoLX_G.DataBindings.Add("Text", DataSource, "DoLoangXuong").FormatString = DungChung.Bien.FormatString[1];
            celLHN_G.DataBindings.Add("Text", DataSource, "LuuHuyetNao").FormatString = DungChung.Bien.FormatString[1];
            celDND_G.DataBindings.Add("Text", DataSource, "DienNaoDo").FormatString = DungChung.Bien.FormatString[1];
            celTienThuoc_G.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            celTienVTYT_G.DataBindings.Add("Text", DataSource, "TienVTYT").FormatString = DungChung.Bien.FormatString[1];
         
            celTienBN_G.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienVTKhongTT_G.DataBindings.Add("Text", DataSource, "TienKTT").FormatString = DungChung.Bien.FormatString[1];
            //celTong_G.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_G.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            celTienBN_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienVTKhongTT_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTong_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXN_R.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celTDCN_R.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm_R.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celDienTim_R.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celPttt_R.DataBindings.Add("Text", DataSource, "ThuThuatPT").FormatString = DungChung.Bien.FormatString[1];
            celNoisoi_R.DataBindings.Add("Text", DataSource, "NoiSoi").FormatString = DungChung.Bien.FormatString[1];
            celKSK_R.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_R.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            celTienVC_R.DataBindings.Add("Text", DataSource, "TienVC").FormatString = DungChung.Bien.FormatString[1];
            celChupXQ_R.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celDoLX_R.DataBindings.Add("Text", DataSource, "DoLoangXuong").FormatString = DungChung.Bien.FormatString[1];
            celLHN_R.DataBindings.Add("Text", DataSource, "LuuHuyetNao").FormatString = DungChung.Bien.FormatString[1];
            celDND_R.DataBindings.Add("Text", DataSource, "DienNaoDo").FormatString = DungChung.Bien.FormatString[1];
            celTienThuoc_R.DataBindings.Add("Text", DataSource, "TienThuoc").FormatString = DungChung.Bien.FormatString[1];
            celTienVTYT_R.DataBindings.Add("Text", DataSource, "TienVTYT").FormatString = DungChung.Bien.FormatString[1];
          
            celTienBN_R.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBH_R.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienVTKhongTT_R.DataBindings.Add("Text", DataSource, "TienKTT").FormatString = DungChung.Bien.FormatString[1];
            //celTong_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
            celTong_R.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celTienBN_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienVTKhongTT_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTong_R.Summary.FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));

        }
    }
}
