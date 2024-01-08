using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_Mau_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi_Mau_27183()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_BSDT.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            txtSoTY.Text = DungChung.Bien.TenCQCQ;
            txtBenhVien.Text = DungChung.Bien.TenCQ;
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
        }

        private void xrLabel18_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DuongDan2.Value != null && !String.IsNullOrEmpty(DuongDan2.Value.ToString()))
            {
                String[] arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', DuongDan2.Value.ToString());
                string[] arr = { "", "", "" };
                int j = 0;
                for (int i = 0; i < arrDuongDan.Length; i++)
                {
                    if (arrDuongDan[i] != "")
                    {
                        if (j < 3)
                            arr[j] = arrDuongDan[i];
                        j++;
                    }

                }
                if (j <= 2)
                {
                    xpic1.Visible = false;
                    xpic2.Visible = false;
                    xpic3.Visible = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            switch (i)
                            {
                                case 0:
                                    xpic4.Visible = true;
                                    this.xpic4.ImageUrl = arr[i];
                                    break;
                                case 1:
                                    xpic5.Visible = true;
                                    this.xpic5.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
                else if (j >= 3)
                {
                    xpic4.Visible = false;
                    xpic5.Visible = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            switch (i)
                            {
                                case 0:
                                    xpic1.Visible = true;
                                    this.xpic1.ImageUrl = arr[i];
                                    break;
                                case 1:
                                    xpic2.Visible = true;
                                    this.xpic2.ImageUrl = arr[i];
                                    break;
                                case 2:
                                    xpic3.Visible = true;
                                    this.xpic3.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
            }

        }
        //public void hienthiKQ(string str){
        //    xrKQ.Html = str;
        //}

    }
}
