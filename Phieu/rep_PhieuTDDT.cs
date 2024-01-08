using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class rep_PhieuTDDT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuTDDT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVI.DataBindings.Add("Text", DataSource, "Donvi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom");
            colThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienrep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (paramSoThe.Value == null || paramSoThe.Value.ToString().Length<1)
                xrLabel14.Visible = false;
            if (paramSoThe.Value != null)
            {
                string _ketqua = "";
                if (this.KetQua.Value != null)
                    _ketqua = this.KetQua.Value.ToString();
                if (Status.Value.ToString() == "1")
                {
                    txtChuyenVien.Text = "X".ToUpper();
                }
                else
                {

                    if (_ketqua.Contains("Khỏi"))
                    {
                        txtKhoi.Text = "X".ToUpper();
                    }
                    if (_ketqua.Contains("Đỡ|Giảm"))
                    {
                        txtDo.Text = "X".ToUpper();
                    }
                    if (_ketqua.Contains("Tử vong"))
                    {
                        txtTuVong.Text = "X".ToUpper();
                    }
                }


            }


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //ColBangChu.Text = " (Bằng chữ): " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng!");
            //txtNguoiLapBieu.Text ="Họ tên:"+ DungChung.Bien.NguoiLapBieu;
            //txtKeToanVP.Text = "Họ tên:" + DungChung.Bien.KeToanVP;
            //txtGiamDinhBH.Text = "Họ tên:" + DungChung.Bien.GiamDinhBH;
        }

        private void ColBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
            //ColBangChu.Text=   " Bằng chữ: " + DungChung.Ham.DocTienBangChu(Convert.ToDouble(Tongtien.Value), " đồng.");
        }
        int cong = 1;

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            String strCong = "";
            switch (cong)
            {
                case 1:
                    strCong = "Cộng I:";
                    break;
                case 2:
                    strCong = "Cộng II:";
                    break;
                case 3:
                    strCong = "Cộng III:";
                    break;
                case 4:
                    strCong = "Cộng IV:";
                    break;
                case 5:
                    strCong = "Cộng V:";
                    break;
                case 6:
                    strCong = "Cộng VI:";
                    break;
                case 7:
                    strCong = "Cộng VII:";
                    break;
                case 8:
                    strCong = "Cộng VIII:";
                    break;
                default:
                    break;
            }
            cong++;
            xrTableCell15.Text = strCong;
        }

    }
}
