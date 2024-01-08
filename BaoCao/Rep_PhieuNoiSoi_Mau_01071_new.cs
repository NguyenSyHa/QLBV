using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_Mau_01071_new : DevExpress.XtraReports.UI.XtraReport
    {
        int loai = 0;
        public Rep_PhieuNoiSoi_Mau_01071_new(int _loai)
        {
            InitializeComponent();
            loai = _loai;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoTY.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtBenhVien.Text = DungChung.Bien.TenCQ.ToUpper();
            if (loai == 1)
            {
                xrPictureBox6.Visible = true;
            }
            else if (loai == 2)
            {
                xrPictureBox5.Visible = true;
            }
            if (loai == 4)
            {
                xrPictureBox5.Visible = false;
                xrPictureBox5.Visible = false;
            }
            else
            {
                xrPictureBox6.Visible = true;
                xrPictureBox4.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24297")
            {
                
                xrPictureBox7.Visible = true;
            }
            else
            {
                xrPictureBox7.Visible = false;
                xrPictureBox6.Visible = true;
            }
            if (DungChung.Bien.MaBV == "12345")
            {
                xrPictureBox6.Image = DungChung.Ham.GetLogo();
                xrPictureBox7.Visible = false;
                
            }
            int dem = 0;
            if (DuongDan2.Value != null && !String.IsNullOrEmpty(DuongDan2.Value.ToString()))
            {
                String[] arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', DuongDan2.Value.ToString());
                for (int i = 0; i < arrDuongDan.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(arrDuongDan[0]))
                                dem++;
                            this.xrPictureBox1.ImageUrl = arrDuongDan[0];
                            break;
                        case 1:
                            if (!string.IsNullOrEmpty(arrDuongDan[1]))
                                dem++;
                            this.xrPictureBox2.ImageUrl = arrDuongDan[1];
                            break;
                        case 2:
                            if (!string.IsNullOrEmpty(arrDuongDan[2]))
                                dem++;
                            this.xrPictureBox3.ImageUrl = arrDuongDan[2];
                            break;
                        case 3:
                            if (!string.IsNullOrEmpty(arrDuongDan[3]))
                                dem++;
                            this.xrPictureBox4.ImageUrl = arrDuongDan[3];
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (dem)
            {
                case 0:
                    break;

                case 1:
                    //this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(250F, 25F);
                    xrPictureBox2.Visible = false;
                    xrPictureBox3.Visible = false;
                    xrPictureBox4.Visible = false;
                    break;
                case 2:
                    //    this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(100F, 25F);
                    //    this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(400F, 25F);
                    xrPictureBox3.Visible = false;
                    xrPictureBox4.Visible = false;
                    break;
                case 3:
                    //this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 25F);
                    //this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(300F, 25F);
                    //this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(550F, 25F);
                    xrPictureBox4.Visible = false;
                    break;
            }
        }

        private void xrLabel18_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {




        }
        //public void hienthiKQ(string str)
        //{
        //    xrKQ.Html = str;
        //}

    }
}
