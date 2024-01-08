using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_Mau_TD : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi_Mau_TD()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoTY.Text = DungChung.Bien.TenCQCQ;
            txtBenhVien.Text = DungChung.Bien.TenCQ;
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void xrLabel18_BeforePrint(object sender, CancelEventArgs e)
        {
            var bsxn = (from bs in DataContect.CanBoes.Where(p => p.MaCB == Macb.Value)
                        select new { bs.TenCB }).ToList();
            if (bsxn.Count > 0)
            {
                colTenTKXN.Text = bsxn.First().TenCB;
            }
        }
        //public void hienthiKQ(string str){
        //    xrKQ.Html = str;
        //}


        public void editPhieu(string kq,string duongdan2)
        {
            if (paramLoaiPhieu != null && paramLoaiPhieu.Value.ToString() == "nstmh")
            {
                if (!String.IsNullOrEmpty(kq))
                {
                    string[] arrKQ = QLBV_Library.QLBV_Ham.LayChuoi('|', kq);
                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', duongdan2);
                    string ketqua = "";
                    if (!String.IsNullOrEmpty(arrKQ[0]))
                    {
                        ketqua += "Tai phải: " + arrKQ[0] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[0]))
                    {
                        xrPictureBox2.ImageUrl = arrDD[0];    
                    }

                    if (!String.IsNullOrEmpty(arrKQ[1]))
                    {
                        ketqua += "Tai trái: " + arrKQ[1] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[1]))
                    {
                        xrPictureBox3.ImageUrl = arrDD[1];
                    }

                    if (!String.IsNullOrEmpty(arrKQ[2]))
                    {
                        ketqua += "Mũi phải: " + arrKQ[2] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[2]))
                    {
                        xrPictureBox2.ImageUrl = arrDD[2];
                    }

                    if (!String.IsNullOrEmpty(arrKQ[3]))
                    {
                        ketqua += "Mũi trái: " + arrKQ[3] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[3]))
                    {
                        xrPictureBox3.ImageUrl = arrDD[3];
                    }

                    if (!String.IsNullOrEmpty(arrKQ[4]))
                    {
                        ketqua += "Hạ họng: " + arrKQ[4] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[4]))
                    {
                        xrPictureBox2.ImageUrl = arrDD[4];
                    }

                    if (!String.IsNullOrEmpty(arrKQ[5]))
                    {
                        ketqua += "Họng: " + arrKQ[5] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[5]))
                    {
                        xrPictureBox3.ImageUrl = arrDD[5];
                    }

                    if (!String.IsNullOrEmpty(arrKQ[6]))
                    {
                        ketqua += "Thanh quản: " + arrKQ[6] + "\n";
                    }
                    if (!String.IsNullOrEmpty(arrDD[6]))
                    {
                        xrPictureBox2.ImageUrl = arrDD[6];
                    }
                    lbKetQua.Text = ketqua;
                }
            }
            else
            {
                lbKetQua.Text = kq;
            }

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (paramLoaiPhieu.Value.ToString() != "nstmh")
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
                            case 2:
                                this.xrPictureBox3.ImageUrl = arrDuongDan[2];
                                break;
                            case 3:
                                this.xrPictureBox4.ImageUrl = arrDuongDan[3];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }


    }
}
