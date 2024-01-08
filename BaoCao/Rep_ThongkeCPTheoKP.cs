using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_ThongkeCPTheoKP : DevExpress.XtraReports.UI.XtraReport
    {
        private int _mau = 0;// mẫu: 0: Chi tiết; 1: Tổng hợp

        public Rep_ThongkeCPTheoKP()
        {
            InitializeComponent();
        }

        public Rep_ThongkeCPTheoKP(int mau)
        {
           
            InitializeComponent();
            this._mau = mau;
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN").FormatString=DungChung.Bien.FormatString[1];
            colSoThe.DataBindings.Add("Text", DataSource, "Sothe").FormatString = DungChung.Bien.FormatString[1];
            colMaBenh.DataBindings.Add("Text", DataSource, "MaBenh").FormatString = DungChung.Bien.FormatString[1];
            colNoiDKKCB.DataBindings.Add("Text", DataSource, "DKKCB").FormatString = DungChung.Bien.FormatString[1];
            colNgayKham.DataBindings.Add("Text", DataSource, "Ngaykham").FormatString = "{0:dd/MM/yyyy}" ;
            colXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colTDCN.DataBindings.Add("Text", DataSource, "TDCN").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colVTTH.DataBindings.Add("Text", DataSource, "VTTH").FormatString = DungChung.Bien.FormatString[1];
            colVTTT.DataBindings.Add("Text", DataSource, "VTTT").FormatString = DungChung.Bien.FormatString[1];
            colDVKTC.DataBindings.Add("Text", DataSource, "DVKTC").FormatString = DungChung.Bien.FormatString[1];
            colTTG.DataBindings.Add("Text", DataSource, "TTG").FormatString = DungChung.Bien.FormatString[1];
            colCongKham.DataBindings.Add("Text", DataSource, "Congkham").FormatString = DungChung.Bien.FormatString[1];
            colVanChuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGFVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongCP.DataBindings.Add("Text", DataSource, "TongCP").FormatString = DungChung.Bien.FormatString[1];
            colBNChiTra.DataBindings.Add("Text", DataSource, "BNchitra").FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];

            colGFBN.DataBindings.Add("Text", DataSource, "BNchitra");
            colGFBN.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem");
            colGFXetnghiem.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFCDHA.DataBindings.Add("Text", DataSource, "CDHA");
            colGFCDHA.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFTDCN.DataBindings.Add("Text", DataSource, "TDCN");
            colGFTDCN.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFThuoc.DataBindings.Add("Text", DataSource, "Thuoc");
            colGFThuoc.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFMau.DataBindings.Add("Text", DataSource, "Mau");
            colGFMau.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFTTPT.DataBindings.Add("Text", DataSource, "TTPT");
            colGFTTPT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFVTTH.DataBindings.Add("Text", DataSource, "VTTH");
            colGFVTTH.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFVTTT.DataBindings.Add("Text", DataSource, "VTTT");
            colGFVTTT.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFDVKTC.DataBindings.Add("Text", DataSource, "DVKTC");
            colGFDVKTC.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFTTG.DataBindings.Add("Text", DataSource, "TTG");
            colGFTTG.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFCongkham.DataBindings.Add("Text", DataSource, "Congkham");
            colGFCongkham.Summary.FormatString = DungChung.Bien.FormatString[1];

            colVanChuyen.DataBindings.Add("Text", DataSource, "Vanchuyen");
            colVanChuyen.Summary.FormatString = DungChung.Bien.FormatString[1];

            colGFTongCP.DataBindings.Add("Text", DataSource, "TongCP");
            colGFTongCP.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienGiuong_G.DataBindings.Add("Text", DataSource, "TienGiuong");
            celTienGiuong_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            colRFXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem");
            colRFXetnghiem.Summary.FormatString = DungChung.Bien.FormatString[1];

            colRFCDHA.DataBindings.Add("Text", DataSource, "CDHA");
            colRFCDHA.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTDCN.DataBindings.Add("Text", DataSource, "TDCN");
            colRFTDCN.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFThuoc.DataBindings.Add("Text", DataSource, "Thuoc");
            colRFThuoc.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFMau.DataBindings.Add("Text", DataSource, "Mau");
            colRFMau.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTTPT.DataBindings.Add("Text", DataSource, "TTPT");
            colRFTTPT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVTTH.DataBindings.Add("Text", DataSource, "VTTH");
            colRFVTTH.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVTTT.DataBindings.Add("Text", DataSource, "VTTT");
            colRFVTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFDVKTC.DataBindings.Add("Text", DataSource, "DVKTC");
            colRFDVKTC.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTTG.DataBindings.Add("Text", DataSource, "TTG");
            colRFTTG.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFCongKham.DataBindings.Add("Text", DataSource, "Congkham");
            colRFCongKham.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFVanChuyen.DataBindings.Add("Text", DataSource, "Vanchuyen");
            colRFVanChuyen.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFTongCP.DataBindings.Add("Text", DataSource, "TongCP");
            colRFTongCP.Summary.FormatString = DungChung.Bien.FormatString[1];
            colRFBN.DataBindings.Add("Text", DataSource, "BNchitra");
            colRFBN.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_T.DataBindings.Add("Text", DataSource, "TienGiuong");
            celTienGiuong_T.Summary.FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
                
        }

        private void colGH_BeforePrint(object sender, CancelEventArgs e)
        {
           
                
            if (this.GetCurrentColumnValue("TenKP") != null)
            {
                colGH.Text = "Khoa phòng: " +this.GetCurrentColumnValue("TenKP").ToString();
            }
        }


        private void colGF_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenKP") != null)
            {
                if(_mau == 0)
                colGF.Text ="Tổng cộng khoa phòng: "+ this.GetCurrentColumnValue("TenKP").ToString();
                else if(_mau == 1)
                    colGF.Text = this.GetCurrentColumnValue("TenKP").ToString();
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_mau == 0)
                Detail.Visible = true;
            else if (_mau == 1)
                Detail.Visible = false;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_mau == 0)
                GroupHeader1.Visible = true;
            else if (_mau == 1)
                GroupHeader1.Visible = false;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_mau == 0)
            {
                xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.All;
                colGF.Borders = DevExpress.XtraPrinting.BorderSide.All;
            }
            else if (_mau == 1)
            {
                xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));


                colGF.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
               
            }
        }

    }
}
