using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_Mau_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi_Mau_01071()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var hthong = _db.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
            
            if (DungChung.Bien.MaBV == "01071")
            {
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = true;
                tb_KetQua.Visible = true;
            }
            else
            {
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            }
            txtSoTY.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtBenhVien.Text = DungChung.Bien.TenCQ.ToUpper();
            txtSoYT.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;
            txtTenBV1.Text = DungChung.Bien.TenCQ.ToUpper();
            txtdchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            xrTableRow2.Visible = false;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                if(DungChung.Bien.MaBV == "12345")
                {
                    xrPictureBox6.Visible = true;
                    xrPictureBox6.Image = DungChung.Ham.GetLogo();
                    xrPictureBox10.Visible = false;
                    xrPictureBox11.Visible = false;
                }
                else if (DungChung.Bien.MaBV == "24297")
                {
                    xrPictureBox6.Visible = false;
                    xrPictureBox10.Visible = false;
                    xrPictureBox11.Visible = true;
                }
                else
                {
                    xrPictureBox6.Visible = false;
                    xrPictureBox10.Visible = true;
                    xrPictureBox11.Visible = false;
                }
            }
            else
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                SubBand3.Visible = true;
                SubBand4.Visible = false;
            }
            if(DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                xrLabel50.Visible = true;
                xrLabel31.Visible = true;
                xrLabel32.Visible = true;
                xrLabel24.Text += "Khám bệnh tất cả các ngày trong tuần \r\n";
                if (hthong.IsTV != true)
                {
                    xrLabel24.Text += "Giảm 20% cho bệnh nhân BHYT";
                }

                xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            }
        }

        private void xrLabel18_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
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
                            this.xrPictureBox8.ImageUrl = arrDuongDan[0];
                            break;
                        case 1:
                            if (!string.IsNullOrEmpty(arrDuongDan[1]))
                                dem++;
                            this.xrPictureBox2.ImageUrl = arrDuongDan[1];
                            this.xrPictureBox9.ImageUrl = arrDuongDan[1];
                            break;
                        case 2:
                            if (!string.IsNullOrEmpty(arrDuongDan[2]))
                                dem++;
                            this.xrPictureBox3.ImageUrl = arrDuongDan[2];
                            this.xrPictureBox5.ImageUrl = arrDuongDan[2];
                            break;
                        case 3:
                            if (!string.IsNullOrEmpty(arrDuongDan[3]))
                                dem++;
                            this.xrPictureBox4.ImageUrl = arrDuongDan[3];
                            this.xrPictureBox7.ImageUrl = arrDuongDan[3];
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
                    xrPictureBox9.Visible = false;
                    xrPictureBox5.Visible = false;
                    xrPictureBox7.Visible = false;
                    break;
                case 2:
                    //this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(100F, 25F);
                    //this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(400F, 25F);
                    xrPictureBox3.Visible = false;
                    xrPictureBox4.Visible = false;
                    xrPictureBox5.Visible = false;
                    xrPictureBox7.Visible = false;
                    break;
                case 3:
                    //this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 25F);
                    //this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(300F, 25F);
                    //this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(550F, 25F);
                    xrPictureBox4.Visible = false;
                    xrPictureBox7.Visible = false;
                    break;
            }



        }
        //public void hienthiKQ(string str){
        //    xrKQ.Html = str;
        //}

    }
}
