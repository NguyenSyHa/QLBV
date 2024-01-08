using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_TKChiPhiTheoDV : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TKChiPhiTheoDV()
        {
            InitializeComponent();
            //_lds = ds;
        }
      
        public class lds
        {

            int pLoai;
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public int PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            double sLN;

            public double SLN
            {
                get { return sLN; }
                set { sLN = value; }
            }
            double sLX;

            public double SLX
            {
                get { return sLX; }
                set { sLX = value; }
            }
        }
        List<lds> _lds = new List<lds>();
        private void repsothekho_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        double sltck = 0;
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
     
        //private void sltdk()
        //{
        //    //try
        //    //{
        //        int _Maduoc = 0;
        //        if (this.GetCurrentColumnValue("MaDV") != null)
        //            _Maduoc =Convert.ToInt32( this.GetCurrentColumnValue("MaDV"));
        //        DateTime _ngaytu = Convert.ToDateTime(ngaytu.Value);
        //        DateTime _ngayden = Convert.ToDateTime(Ngaythang.Value);
        //        int _makp =0;
        //        if(Khoaphong.Value!=null)
        //            _makp=Convert.ToInt32(Khoaphong.Value);
             
            
        //        var b = (from a in _lds where a.MaDV == _Maduoc
        //                 group a by new { a.PLoai, a.MaKP } into kp
        //                 select new
        //                 {
        //                     SLTDK = kp.Sum(p => p.SLN) - kp.Sum(p => p.SLX),
        //                     Makp = kp.Key.MaKP
        //                 }).ToList();
        //        if (b !=null && b.Count() > 0)
        //        {
                    
        //            sltck = b.Sum(p => p.SLTDK);

        //        }
        //        else
        //        {
        //            sltck = 0;

        //        }
        //    //}catch(Exception){
                
        //    //System.Windows.Forms.MessageBox.Show("Lỗi hàm tính tồn cuối ");
        //    //this.Dispose();
        //    //}
        //}
        //int sltdk = 0;
        private void xrTableCell19_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(colDonGia.Text)) {

            //    //sltck += +int.Parse(colNhap.Text);
            //    sltck += +Convert.ToInt32(GetCurrentColumnValue("SLNhap"));

            //}
            //if (!string.IsNullOrEmpty(colSoLuong.Text))
            //{

            //   // sltck += -Convert.ToInt32(colXuat.Text);

            //    sltck += -Convert.ToInt32(GetCurrentColumnValue("SLXuat"));

            //}
            //colTyLeTT.Text = sltck.ToString();
        }

        private void colSCTn_BeforePrint(object sender, CancelEventArgs e)
        {
            //colDiengiai.Text = "";
        }
        public void BindingData()
        {
            //colNgaythang.DataBindings.Add("Text", DataSource, "Ngaythang");
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            colMaKCB.DataBindings.Add("Text", DataSource, "MaKCB");
            //colSoluongton.DataBindings.Add("Text", DataSource, "Soluongton");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colNgayTT.DataBindings.Add("Text", DataSource, "NgayTT");
            colTyLeTT.DataBindings.Add("Text", DataSource, "TyLeTT");
            colKP.DataBindings.Add("Text", DataSource, "TenKP");
            //colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
            //celDonGiaG.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            celSoLuong_G.DataBindings.Add("Text", DataSource, "SoLuong");
            celSoLuong_G.Summary.FormatString = DungChung.Bien.FormatString[0];
            celThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien");
            celThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];

            //GroupHeader2.GroupFields.Add(new GroupField("IdTieuNhom"));
            //coltenTN.DataBindings.Add("Text", DataSource, "TenTN");
            //colThanhTienR.DataBindings.Add("Text", DataSource, "ThanhTien");
            //colThanhTienR.Summary.FormatString = DungChung.Bien.FormatString[1];
        }
        //int tdk = 0;
        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
       // int td=0;
        private void colSoluongton_BeforePrint(object sender, CancelEventArgs e)
        {

            //colKP.Text = sltck.ToString();
        }
        string rong = "";
        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (colphanl.Text == "1")
            //{
            //    colTenBN.Text = rong;

            //}
            //else
            //{
            //    colMaBN.Text = rong;
            //}
            //if (this.GetCurrentColumnValue("Ngaythang") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
            //    colSTT.Text = nt.Substring(0, 10);
            //}
        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNgayt.Text = colNgaythang.Text.Substring(0, 10);
            //ToString().Substring(0, 10);
            
        }

        private void colNgayt_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Ngaythang") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
            //    colPhanloai.Text = nt.Substring(0, 10);
            //}
        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            //sltdk();
        }
        int _break = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //sltdk();
            ////sltck = 0;
        
            //if (_break == 0)
            //{
            //    xrPageBreak1.Visible = false;
            //    //xrLine2.Visible = false;
            //}
            //else
            //{
            //    //xrLine2.Visible = true;
            //    //xrPageBreak1.Visible = true;
            //    xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
            //}
            //_break++;
            //if (DungChung.Bien.MaBV == "12122")
            //    GroupFooter1.Visible = true;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
        }
        //int stt = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
        //    switch(stt)
        //    {
        //        case 1:
        //            colTTTN.Text = "I";
        //            break;
        //        case 2:
        //            colTTTN.Text = "II";
        //            break;
        //        case 3:
        //            colTTTN.Text = "III";
        //            break;
        //        case 4:
        //            colTTTN.Text = "IV";
        //            break;
        //        case 5:
        //            colTTTN.Text = "V";
        //            break;
        //        case 6:
        //            colTTTN.Text = "VI";
        //            break;
        //        case 7:
        //            colTTTN.Text = "VII";
        //            break;
        //        case 8:
        //            colTTTN.Text = "VIII";
        //            break;
        //        case 9:
        //            colTTTN.Text = "IX";
        //            break;
        //        case 10:
        //            colTTTN.Text = "X";
        //            break;
        //        case 11:
        //            colTTTN.Text = "XI";
        //            break;
        //    }
        //    stt++;
        }


    }
}
