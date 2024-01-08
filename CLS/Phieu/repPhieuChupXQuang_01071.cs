using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChupXQuang_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupXQuang_01071()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") 
            {

                SubBand1.Visible = true;
                SubBand2.Visible = false;
                if(DungChung.Bien.checkcls == "X-Quang CT")
                {
                    lab1.Text = "PHIẾU CHIẾU CHỤP CT - SCANNER";
                }
            }
            else if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                SubBand5.Visible = true;
            }
            else
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"  || DungChung.Bien.MaBV == "56789")
            {
                xrTable5.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lab1.Font = new Font(lab1.Font, FontStyle.Regular);
                lab2.Font = new Font(lab2.Font, FontStyle.Regular);
                colSo.Font = new Font(colSo.Font, FontStyle.Regular);
                lab3.Font = new Font(lab3.Font, FontStyle.Regular);
                xrTableCell6.Font = new Font(xrTableCell6.Font, FontStyle.Regular);
                colMaBN.Font = new Font(colMaBN.Font, FontStyle.Regular);
                sovv.Font = new Font(sovv.Font, FontStyle.Regular);
                lab54.Font = new Font(lab54.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);
                colTenBSDT1.Font = new Font(colTenBSDT.Font, FontStyle.Regular);
                xrTableCell1.Font = new Font(xrTableCell1.Font, FontStyle.Regular);
                colYCCC.Font = new Font(colYCCC.Font, FontStyle.Regular);
                xrTable5.LocationF = lab2.LocationF;              
                colSo.Visible = lab3.Visible = lab2.Visible = sovv.Visible = false;
            }

            //In đậm với các trường hợp khác 01071
            if (DungChung.Bien.MaBV != "01071")
            {
                xrTableCell3.Font = new Font(xrTableCell3.Font, FontStyle.Bold);
                colKQCC.Font = new Font(colKQCC.Font, FontStyle.Bold);
                xrTableCell5.Font = new Font(xrTableCell5.Font, FontStyle.Bold);
                lab55.Font = new Font(lab55.Font, FontStyle.Bold);
                colTenTKXN.Font = new Font(colTenTKXN.Font, FontStyle.Bold);
            }
          
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
           
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();       
            if (DungChung.Bien.MaBV == "24009")
            {
                this.colKQCC.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.colYCCC.Font = new System.Drawing.Font("Times New Roman", 13F);
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                //xrTable6.Visible = DungChung.Bien._Visible_CDHA[1];
                SubBand2.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                colBSDT2.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                colBSDT3.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (MaCBth.Value != null)
            {
                colTenBSDT1.Text = DungChung.Ham._getTenCB(DataContect, MaCBth.Value.ToString());
                colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, MaCBth.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                rowBSDieuTri.Visible = false;
            }
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "12345")
            {
                SubBand4.Visible = true;
                // start HIS-1475
                xrTableRow18.Visible = false;
                // end HIS-1475
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                xrPictureBox1.Visible = false;
                colCQ.Visible = colCQCQ.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24297")
            {
                //SubBand4.Visible = true;
                //xrTableRow18.Visible = false;
                //xrPictureBox1.Visible = true;
                //xrPictureBox2.Visible = false;
                if(DungChung.Bien.CheckInFull == 1)
                {
                    SubBand2.Visible = false;
                    SubBand1.Visible = true;
                    SubBand5.Visible = false;
                    SubBand4.Visible = false;
                    xrTableRow18.Visible = false;
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = false;
                }
                if (DungChung.Bien.CheckInFull == 2)
                {
                    SubBand2.Visible = false;
                    SubBand1.Visible = true;
                    SubBand5.Visible = false;
                    SubBand4.Visible = false;
                    xrTableRow18.Visible = false;
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = false;
                    SubBand3.Visible = true;
                    rowBSDieuTri.Visible = true;
                }
                if (DungChung.Bien.CheckInFull == 3)
                {
                    ReportFooter.Visible = false;
                    SubBand2.Visible = false;
                    SubBand1.Visible = false;
                    SubBand5.Visible = false;
                    SubBand4.Visible = false;
                    //xrTableRow18.Visible = false;
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = false;
                    SubBand3.Visible = true;
                    rowBSDieuTri.Visible = true;
                }
                if (DungChung.Bien.CheckInFull == 4)
                {
                    ReportFooter.Visible = true;
                    SubBand2.Visible = true;
                    SubBand1.Visible = false;
                    SubBand5.Visible = false;
                    SubBand4.Visible = false;
                    //xrTableRow18.Visible = false;
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = false;
                    SubBand3.Visible = true;
                    rowBSDieuTri.Visible = true;
                }
                colTenCQ2.Visible = colTenCQCQ2.Visible = false;
                
            }
            else
                SubBand3.Visible = true;
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30012")
            {
                colTenBSDT1.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colYCCC.Font = new Font(colYCCC.Font, FontStyle.Bold);
                xrTableCell11.Font = new Font(xrTableCell11.Font, FontStyle.Bold);
                xrTableCell13.Font = new Font(xrTableCell11.Font, FontStyle.Bold);
            }
            if (DungChung.Bien.MaBV == "20001")
                lab55.Text = "BÁC SỸ ĐỌC KẾT QUẢ";
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                colKQ1.Font = new System.Drawing.Font("Times New Roman", 13F, FontStyle.Regular);
                colKL1.Font = new System.Drawing.Font("Times New Roman", 13F, FontStyle.Bold);

            }
        }
    }
}
