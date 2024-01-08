using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChupXQuang : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupXQuang()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3_30372.Visible = false;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
            colDiaChi27.Text = DungChung.Ham.GetDiaChiBV();
            if (DungChung.Bien.MaBV == "27777")
            {
                picBoxLogo.Image = DungChung.Ham.GetLogo();
                SubBand5.Visible = true;
                SubBand1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false; 
                SubBand3.Visible = true;
                picBoxLogo.Visible = false;
                SubBand3_30372.Visible = false;
                xrPictureBox2.Visible = true;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                colDiaChi24272.Text = DungChung.Ham.GetDiaChiBV();
                xrTable14.Visible = true; 
            }
            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand2.Visible = false;
                xrLabel43.Visible = false;
                SubBand3_30372.Visible = true;
                SubBand5_30372.Visible = true;
                LoGo30372.Image = DungChung.Ham.GetLogo();
                xrLabel54.Text = DungChung.Ham.GetDiaChiBV();
                xrLabel44.Text = "SĐT: " + DungChung.Ham.GetSDTBV() + " - " + "Fax: " + DungChung.Ham.GetFaxBV();
                switch (DungChung.Bien.MauIn)
                {
                    case 1:
                        SubBand1.Visible = false;
                        SubBand3_30372.Visible = true;
                        break;
                    case 2:
                        SubBand1.Visible = true;
                        SubBand3_30372.Visible = false;
                        break;
                    case 3:
                        SubBand1.Visible = false;
                        SubBand3_30372.Visible = true;
                        break;
                    default:
                        SubBand1.Visible = true;
                        SubBand3_30372.Visible = false;
                        break;
                }
            }
            if (DungChung.Bien.MaBV == "14017")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3_30372.Visible = false;
                SubBand6_14017.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30012")
            {
                this.colKQCC.Font = new System.Drawing.Font("Times New Roman", 13F, FontStyle.Bold);
                //this.xrLabel90.Font = new System.Drawing.Font("Times New Roman", 13F, FontStyle.Bold);
            }

        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                xrTable5.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
                if (DungChung.Bien.MaBV != "24297")
                    xrPictureBox3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27777" /*|| DungChung.Bien.MaBV == "24272"*/)
            {
                picBoxLogo.Image = DungChung.Ham.GetLogo();
                SubBand5.Visible = true;
                SubBand1.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24009")
            {
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
                xrPictureBox3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                xrPictureBox3.Visible = false;
                colTenCQCQ.Font = new Font("Times New Roman", 10f, FontStyle.Regular);
            }


            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "24272")
            {
                xrTable14.Visible = DungChung.Bien._Visible_CDHA[2];
            }
            
            xrTable11.Visible = DungChung.Bien._Visible_CDHA[2];
            if(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                xrTable4.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                SubBand4.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            xrLabel8.Text = colTenCQCQ.Text = colTenCQCQ27.Text = colTenCQCQ24272.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel11.Text = colTenCQ.Text = colTenCQ27.Text = colTenCQ24272.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24009")
            {
                this.colKQCC.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.colYCCC.Font = new System.Drawing.Font("Times New Roman", 13F);
                xrPictureBox3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = xrPictureBox3.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
                xrPictureBox3.Visible = false;
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                xrLabel41.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
                xrPictureBox3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30372")
            {
                this.colKQCC.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                xrPictureBox3.Visible = false;
            }

            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());

            }
            if (DungChung.Bien.MaBV == "26007")
            {
                xrPictureBox3.Visible = false;
                xrBarCode1.Visible = true;
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand2.Visible = false;
                xrLabel43.Visible = false;
                SubBand3_30372.Visible = true;
                SubBand5_30372.Visible = true;
                LoGo30372.Image = DungChung.Ham.GetLogo();
                xrLabel54.Text = DungChung.Ham.GetDiaChiBV();
                xrLabel44.Text = "SĐT: " + DungChung.Ham.GetSDTBV() + " - " + "Fax: " + DungChung.Ham.GetFaxBV();
                switch (DungChung.Bien.MauIn)
                {
                    case 1:
                        SubBand1.Visible = false;
                        SubBand3_30372.Visible = true;
                        break;
                    case 2:
                        SubBand1.Visible = true;
                        SubBand3_30372.Visible = false;
                        break;
                    case 3:
                        SubBand1.Visible = false;
                        SubBand3_30372.Visible = true;
                        break;
                    default:
                        SubBand1.Visible = true;
                        SubBand3_30372.Visible = false;
                        break;
                }
            }


        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "20001")
            {
                lab55.Text = "BÁC SỸ ĐỌC KẾT QUẢ";
            }
            else if (DungChung.Bien.MaBV == "30372")
            {
                SubBand4.Visible = false;
                SubBand5_30372.Visible = true;
                this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                this.xrRichText2.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            }
        }
    }
}
