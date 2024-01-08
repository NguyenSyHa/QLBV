using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_BBKiemNhap_InSoLo_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        List<string> _a = new List<string>();
        public Rep_BBKiemNhap_InSoLo_30009(List<string> a)
        {
            InitializeComponent();
            _a = a;
        }
        public void BindingData()
        {
            colTenDuocGh22.DataBindings.Add("Text", DataSource, "TenNhom");
            colTenDuocGh21.DataBindings.Add("Text", DataSource, "TenTN");
            colTenDuoc1.DataBindings.Add("Text", DataSource, "TenDV");
            colSoLo1.DataBindings.Add("Text", DataSource, "SoLo");
            colSoLuong1.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien1.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colQC.DataBindings.Add("Text", DataSource, "QCPC");
            colHangSX.DataBindings.Add("Text", DataSource, "NhaSX");
            colSoCT1.DataBindings.Add("Text", DataSource, "SoCT");
            colDonVi1.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS1.DataBindings.Add("Text", DataSource, "SoDK");
            colNuocSX1.DataBindings.Add("Text", DataSource, "NuocSX");
            colNhaSX.DataBindings.Add("Text", DataSource, "MaQD");
            colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            colDonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BBKiemNhap_Sub repSub = (rep_BBKiemNhap_Sub)xrSubreport1.ReportSource;
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
                repSub.InCD.Value = InCD.Value.ToString();

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
        }

        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
            if (TongTien.Value != null && TongTien.Value.ToString() != "")
            {
                Double st = Convert.ToDouble(TongTien.Value);
                st = Math.Round(st, 0);
                txtsotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng./");
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }
    }
}
