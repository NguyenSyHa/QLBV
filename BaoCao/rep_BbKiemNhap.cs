using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_BbKiemNhap : DevExpress.XtraReports.UI.XtraReport
    {
        List<string> _a = new List<string>();
        public rep_BbKiemNhap(List<string> a)
        {
            InitializeComponent();
            _a = a;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "27022" && DungChung.Bien.CheckinVTYT == true)
            {
                colGhiChu.DataBindings.Add("Text", DataSource, "MaTam");
                xrTableCell9.Text = "Ghi chú";
            }
            else
            {
                //colGhiChu.DataBindings.Add("Text", DataSource, "SoLo");
            }
            colTenDuocGh2.DataBindings.Add("Text", DataSource, "TenNhom");
            colTenDuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS.DataBindings.Add("Text", DataSource, "SoDK");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            if (DungChung.Bien.MaBV == "30007")
            {
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia"); 
            }
            else
            {
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            }

            // colSoLuongGh2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            // colSoLuongGh1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
          
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];

            // colThanhTienGh2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString =DungChung.Bien.FormatString[1];
            // colThanhTienGh1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            //  colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            //GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
           
        }



        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {

            if (TongTien.Value != null && TongTien.Value.ToString() != "")
            {
                Double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                txtsotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng.");
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader2.Visible = false;
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
           
                SubBand1.Visible = true;
                SubBand2.Visible = true;
               
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BBKiemNhap_Sub repSub = new rep_BBKiemNhap_Sub();

            repSub.TV1.Value = TV1.Value.ToString();
            repSub.TV2.Value = TV2.Value.ToString();
            repSub.TV3.Value = TV3.Value.ToString();
            repSub.TV4.Value = TV4.Value.ToString();
            repSub.TV5.Value = TV5.Value.ToString();
            repSub.TV6.Value = TV6.Value.ToString();
            repSub.TV7.Value = TV7.Value.ToString();
            repSub.lblCKCT.Value = CD1.Value.ToString().ToUpper();
            repSub.lblCK1.Value = CD2.Value.ToString().ToUpper();
            repSub.lblCK2.Value = CD3.Value.ToString().ToUpper();
            repSub.lblCK3.Value = CD4.Value.ToString().ToUpper();
            repSub.lblCK4.Value = CD5.Value.ToString().ToUpper();
            repSub.chutich.Text = TV1.Value.ToString();

            if (DungChung.Bien.MaBV == "30009")
            {
                for (int i = 0; i < _a.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            repSub.colChucDanh1.Text = _a[0].ToString();
                            break;
                        case 1:
                            repSub.colChucDanh2.Text = _a[1].ToString();
                            break;
                        case 2:
                            repSub.colChucDanh3.Text = _a[2].ToString();
                            break;
                        case 3:
                            repSub.colChucDanh4.Text = _a[3].ToString();
                            break;
                        case 4:
                            repSub.colChucDanh5.Text = _a[4].ToString();
                            break;
                        case 5:
                            repSub.colChucDanh6.Text = _a[5].ToString();
                            break;
                        case 6:
                            repSub.colChucDanh7.Text = _a[6].ToString();
                            break;
                        default:
                            break;
                    }
                }


            }

            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    repSub.InCD.Value = "CHỦ TỊCH HỘI ĐỒNG THUỐC";
            //}
            //else
            repSub.InCD.Value = InCD.Value.ToString();

            xrSubreport1.ReportSource = repSub;
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BBKiemNhap_Sub repSub = new rep_BBKiemNhap_Sub();

            repSub.TV1.Value = TV1.Value.ToString();
            repSub.TV2.Value = TV2.Value.ToString();
            repSub.TV3.Value = TV3.Value.ToString();
            repSub.TV4.Value = TV4.Value.ToString();
            repSub.TV5.Value = TV5.Value.ToString();
            repSub.TV6.Value = TV6.Value.ToString();
            repSub.TV7.Value = TV7.Value.ToString();
            repSub.lblCKCT.Value = CD1.Value.ToString().ToUpper();
            repSub.lblCK1.Value = CD2.Value.ToString().ToUpper();
            repSub.lblCK2.Value = CD3.Value.ToString().ToUpper();
            repSub.lblCK3.Value = CD4.Value.ToString().ToUpper();
            repSub.lblCK4.Value = CD5.Value.ToString().ToUpper();
            repSub.chutich.Text = TV1.Value.ToString();

            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    repSub.InCD.Value = "CHỦ TỊCH HỘI ĐỒNG THUỐC";
            //}
            //else
            repSub.InCD.Value = InCD.Value.ToString();

        }

        private void txtsotien1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
