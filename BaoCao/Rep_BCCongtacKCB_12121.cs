using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCCongtacKCB_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCCongtacKCB_12121()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            GroupHeader1.GroupFields.Add(new GroupField("stt"));
            GroupHeader2.GroupFields.Add(new GroupField("danhmuctong"));
            //GroupHeader1.GroupFields.Add(new GroupField("tongkb"));
            //GroupHeader1.GroupFields.Add(new GroupField("tongcc"));
            //GroupHeader1.GroupFields.Add(new GroupField("tongphcn"));

            cel_danhmuctong.DataBindings.Add("Text", DataSource, "danhmuctong");
            cel_danhmuc.DataBindings.Add("Text", DataSource, "danhmuc");
            //cel_stt.DataBindings.Add("Text", DataSource, "stt");
            cel_danhmucct.DataBindings.Add("Text", DataSource, "danhmucct");
            cel_dv.DataBindings.Add("Text", DataSource, "donvi");
            cel_khambenh.DataBindings.Add("Text", DataSource, "khambenh").FormatString = DungChung.Bien.FormatString[0];
            cel_noi.DataBindings.Add("Text", DataSource, "noi").FormatString = DungChung.Bien.FormatString[0];
            cel_chamcuu.DataBindings.Add("Text", DataSource, "chamcuu").FormatString = DungChung.Bien.FormatString[0];
            cel_cls.DataBindings.Add("Text", DataSource, "cls").FormatString = DungChung.Bien.FormatString[0];
            cel_phcn.DataBindings.Add("Text", DataSource, "phcn").FormatString = DungChung.Bien.FormatString[0];
            cel_tongso.DataBindings.Add("Text", DataSource, "tongso").FormatString = DungChung.Bien.FormatString[0];
            col_tongkb.DataBindings.Add("Text", DataSource, "tongkb").FormatString = DungChung.Bien.FormatString[0];
            col_tongnoi.DataBindings.Add("Text", DataSource, "tongnoi").FormatString = DungChung.Bien.FormatString[0];
            col_tongcc.DataBindings.Add("Text", DataSource, "tongcc").FormatString = DungChung.Bien.FormatString[0];
            col_phcn.DataBindings.Add("Text", DataSource, "tongphcn").FormatString = DungChung.Bien.FormatString[0];
            col_tongcls.DataBindings.Add("Text", DataSource, "tongcls").FormatString = DungChung.Bien.FormatString[0];
            col_tong.DataBindings.Add("Text", DataSource, "tong").FormatString = DungChung.Bien.FormatString[0];
            cel_donvi.DataBindings.Add("Text", DataSource, "donvi");

        }

        private void cel_danhmuc_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void GroupHeader2_AfterPrint(object sender, EventArgs e)
        {

        }

        int i = 1, a = 1, b = 1, c = 1, d = 1, g = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            

            if (this.GetCurrentColumnValue("danhmuc") != null)
            {
                string nt = this.GetCurrentColumnValue("danhmuc").ToString();
                if (nt == "7. Tổng số ngày điều trị nội trú" || nt == "9. Điều trị bằng YHCT đơn" || nt == "10. Ngày điều trị TB 1 BN khỏi")
                    Detail.Visible = false;
                else
                    Detail.Visible = true;
            }
            
            //if (this.GetCurrentColumnValue("danhmuctong") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("danhmuctong").ToString();
            //    if (nt == "I. CÔNG TÁC KHÁM CHỮA BỆNH")
            //    //{
            //    //    switch (a)
            //    //    {
            //    //        case 1:
            //    //            col_tt.Text = "1";
            //    //            break;
            //    //        case 2:
            //    //            col_tt.Text = "2";
            //    //            break;
            //    //        case 3:
            //    //            col_tt.Text = "3";
            //    //            break;
            //    //        case 4:
            //    //            col_tt.Text = "4";
            //    //            break;
            //    //        case 5:
            //    //            col_tt.Text = "5";
            //    //            break;
            //    //        case 6:
            //    //            col_tt.Text = "6";
            //    //            break;
            //    //        case 7:
            //    //            col_tt.Text = "7";
            //    //            break;
            //    //        case 8:
            //    //            col_tt.Text = "8";
            //    //            break;
            //    //        case 9:
            //    //            col_tt.Text = "9";
            //    //            break;
            //    //        case 10:
            //    //            col_tt.Text = "10";
            //    //            break;
            //    //        case 11:
            //    //            col_tt.Text = "11";
            //    //            break;
            //    //    }
            //    //    a++;
                
            //}
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            //if (this.GetCurrentColumnValue("danhmuc") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("danhmuc").ToString();
            //    if (nt == "1. Tổng số siêu âm" || nt == "2. Tổng số X-Quang" || nt == "3. Điện não" || nt == "4. Điện tâm đồ" || nt == "5. Đo độ loãng xương" || nt == "1. Tổng số" || nt == "2. Khám chữa bệnh viện phí" || nt == "3. Khám chữa bệnh người nghèo" || nt == "4. Khám chữa bệnh trẻ em < 6 tuổi" || nt == "5. Khám chữa bệnh bệnh nhân > 60 tuổi" || nt == "6. Khám chữa bệnh bệnh nhân > 80 tuổi" || nt == "7. Tổng số ngày điều trị nội trú" || nt == "8. Tổng số bệnh nhân ra viện" || nt == "9. Điều trị bằng YHCT đơn" || nt == "Ngày điều trị TB 1 BN khỏi" || nt == "1. Nhận người bệnh từ các tuyến chuyển đến" || nt == "2. Chuyển người bệnh đi các tuyến")
            //        xrTableCell14.Text = "";
            //    else
            //    {
            //        xrTableCell14.Text = i.ToString();
            //        i++;
            //    }
            //}

            //if(this.GetCurrentColumnValue("danhmuctong")!=null)
            //{
            //    string nt = this.GetCurrentColumnValue("danhmuctong").ToString();
            //    if (nt == "I. CÔNG TÁC KHÁM CHỮA BỆNH" || nt == "II. CÔNG TÁC CHUYỂN TUYẾN")
            //    {
            //        xrTableCell14.Text = "";
            //    }
            //    else
            //    {
            //        
            //        else
            //        {
            //            //xrTableCell14.Text = i.ToString();
            //            //i++;
            //        }

                    
            //    }

                
            //}
            if (this.GetCurrentColumnValue("danhmuctong") != null)
            {
                string nt = this.GetCurrentColumnValue("danhmuctong").ToString();
                if (nt == "III. CÁC DANH MỤC KỸ THUẬT")
                {
                    xrTableCell14.Text = i.ToString();
                    i++;
                }
                else
                {
                    if(nt=="IV. CẬN LÂM SÀNG")
                    {
                        if (this.GetCurrentColumnValue("danhmuc") != null)
                        {
                            string ntn = this.GetCurrentColumnValue("danhmuc").ToString();
                            if (ntn == "1. Tổng số siêu âm" || ntn == "2. Tổng số X-Quang" || ntn == "3. Điện não" || ntn == "4. Điện tâm đồ" || ntn == "5. Đo độ loãng xương" || ntn == "6. Thủ thuật")
                                xrTableCell14.Text = "";
                            if (ntn == "7. Xét nghiệm hóa sinh máu")
                            {
                                
                                xrTableCell14.Text = a.ToString();
                                a++;
                            }
                            if (ntn == "8. Xét nghiệm huyết học")
                            {

                                xrTableCell14.Text = c.ToString();
                                c++;
                            }
                            if (ntn == "9. Xét nghiệm nước tiểu 10")
                            {

                                xrTableCell14.Text = d.ToString();
                                d++;
                            }
                            if (ntn == "10. Các xét nghiệm khác")
                            {

                                xrTableCell14.Text = g.ToString();
                                g++;
                            }
                        }
                    }
                }
            }
           
        }

        private void cel_donvi_BeforePrint(object sender, CancelEventArgs e)
        {
            string nt = this.GetCurrentColumnValue("danhmuc").ToString();
            if (nt == "1. Tổng số" || nt == "2. Khám chữa bệnh viện phí" || nt == "3. Khám chữa bệnh người nghèo" || nt == "4. Khám chữa bệnh trẻ em < 6 tuổi" || nt == "5. Khám chữa bệnh bệnh nhân > 60 tuổi" ||  nt == "6. Khám chữa bệnh bệnh nhân > 80 tuổi" || nt == "1. Nhận người bệnh từ các tuyến chuyển đến" || nt == "2. Chuyển người bệnh đi các tuyến")
            {
                cel_donvi.Text = "";
            }
        }

        private void cel_danhmuctong_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }      
}
