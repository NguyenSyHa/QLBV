using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi_27183()
        {
            InitializeComponent();
            xpic1.Visible = false;
            xpic2.Visible = false;
            xpic3.Visible = false;
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004") {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (Macb.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //string mabn = MaBNhan.Value.ToString();
            //var qhh = (from clsct in DataContect.CLScts
            //           join cls in DataContect.CLS on clsct.IdCLS equals cls.IdCLS
            //           where (cls.MaBNhan == mabn)
            //           select new { clsct.MaDVct, clsct.KetQua }).ToList();
            //if (qhh.Count > 0)
            //{
            //    try
            //    {
            //        txtKetQua.Text = qhh.Where(p => p.MaDVct== ("NoiSoi")).First().KetQua.ToString();
                   
            //    }
            //    catch { }
            //}
        }
        public void hienthiKQ(string str) 
        {
            xrKetQua.Html = str;
        }
        public void hienthi( string duongdan)
        {
            string[] arrDD = QLBV_Ham.LayChuoi('|', duongdan);
            string[] arr = {"","","" };
            int j = 0;
            for (int i = 0; i < arrDD.Length; i++)
            {
                if(arrDD[i] != "")
                {
                    if(j<3)
                    arr[j] = arrDD[i];
                    j++;
                }
                
            }
            if(j <= 2)
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
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
