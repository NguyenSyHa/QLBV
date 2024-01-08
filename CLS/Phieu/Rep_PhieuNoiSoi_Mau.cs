using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_Mau : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi_Mau()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_BSDT.Visible = DungChung.Bien._Visible_CDHA[0];
            Detail.Visible = DungChung.Bien._Visible_CDHA[1];
            Detail.Visible = DungChung.Bien._Visible_CDHA[2];

            if (DungChung.Bien.MaBV == "24009")
            {
                Detail.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            txtSoTY.Text = DungChung.Bien.TenCQCQ;
            txtBenhVien.Text = DungChung.Bien.TenCQ;
            if(DungChung.Bien.MaBV == "27194")
            {
                ReportHeader.Visible = false;
                PageHeader.Visible = true;
                xrTableCell7.Visible = false;
                xrTableCell3.Visible = true;
                xrPictureBox6.Visible = false;
                txtTenTrungtam.Text = DungChung.Bien.TenCQCQ;
                txtTenPkham.Text = DungChung.Bien.TenBV;
                txtAddress.Text = DungChung.Bien.DiaChi;
                txtSDT.Text = "0123456789";
            } else
            {
                ReportHeader.Visible = true;
                PageHeader.Visible = false;
                xrTableCell7.Visible = true;
                xrTableCell3.Visible = false;
            }

            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                tb_BSDT.Visible = false;
                xrLine1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                txtSoTY.Visible = false;
                txtBenhVien.Visible = false;
               
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if (DungChung.Bien.MaBV == "24297")
                xrPictureBox3.Visible = false;
            else
                xrPictureBox4.Visible = false;

        }

        private void xrLabel18_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DuongDan2.Value != null && !String.IsNullOrEmpty(DuongDan2.Value.ToString()))
            {
                String[] arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', DuongDan2.Value.ToString());
                for (int i = 0; i < arrDuongDan.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.xrPictureBox1.ImageUrl = arrDuongDan[0];
                            break;
                        case 1:
                            this.xrPictureBox2.ImageUrl = arrDuongDan[1];
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        
        
    }
}
