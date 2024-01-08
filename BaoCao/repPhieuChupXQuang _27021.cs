using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChupXQuang_27021 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupXQuang_27021()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            //tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }

            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DuongDan.Value != null && !String.IsNullOrEmpty(DuongDan.Value.ToString()))
            {
                String[] arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', DuongDan.Value.ToString());
                string[] arr = { "", "", "", "" };
                int j = 0;
                for (int i = 0; i < arrDuongDan.Length; i++)
                {
                    if (arrDuongDan[i] != "")
                    {
                        if (j < 4)
                            arr[j] = arrDuongDan[i];
                        j++;
                    }

                }
                if (j <= 1)
                {
                    xpic1.Visible = false;
                    xpic2.Visible = false;
                    xpic3.Visible = false;
                    xpic4.Visible = false;
                    xpic5.Visible = false;
                    xpic6.Visible = false;
                    xpic7.Visible = false;
                    xpic8.Visible = false;
                    xpic9.Visible = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            switch (i)
                            {
                                case 0:
                                    xpic10.Visible = true;
                                    this.xpic10.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
                else if (j == 2)
                {
                    xpic1.Visible = false;
                    xpic2.Visible = false;
                    xpic3.Visible = false;
                    xpic4.Visible = false;
                    xpic5.Visible = false;
                    xpic6.Visible = false;
                    xpic7.Visible = false;
                    xpic10.Visible = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            switch (i)
                            {
                                case 0:
                                    xpic8.Visible = true;
                                    this.xpic8.ImageUrl = arr[i];
                                    break;
                                case 1:
                                    xpic9.Visible = true;
                                    this.xpic9.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
                else if (j == 3)
                {
                    xpic1.Visible = false;
                    xpic2.Visible = false;
                    xpic3.Visible = false;
                    xpic4.Visible = false;
                    xpic9.Visible = false;
                    xpic8.Visible = false;
                    xpic10.Visible = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] != "")
                        {
                            switch (i)
                            {
                                case 0:
                                    xpic5.Visible = true;
                                    this.xpic5.ImageUrl = arr[i];
                                    break;
                                case 1:
                                    xpic6.Visible = true;
                                    this.xpic6.ImageUrl = arr[i];
                                    break;
                                case 2:
                                    xpic7.Visible = true;
                                    this.xpic7.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
                else if (j >= 4)
                {
                    xpic5.Visible = false;
                    xpic6.Visible = false;
                    xpic7.Visible = false;
                    xpic8.Visible = false;
                    xpic9.Visible = false;
                    xpic10.Visible = false;
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
                                case 3:
                                    xpic4.Visible = true;
                                    this.xpic4.ImageUrl = arr[i];
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
