using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkChiTietPhauThuat_BG01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BkChiTietPhauThuat_BG01()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colQueQuan.DataBindings.Add("Text", DataSource, "DChi");
            colKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao");
            colNgayTT.DataBindings.Add("Text", DataSource, "NgayTT");
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colTieuHao.DataBindings.Add("Text", DataSource, "TieuHao").FormatString = DungChung.Bien.FormatString[1];
            colThuThuat.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            colThuocT.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colTieuHaoT.DataBindings.Add("Text", DataSource, "TieuHao").FormatString = DungChung.Bien.FormatString[1];
            colThuThuatT.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            //colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            //colTongT.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            //colTongT.Text = tong.ToString("0,0");
        
        }

        private void colNgayVao_BeforePrint(object sender, CancelEventArgs e)
        {
            string nv="";
            if (this.GetCurrentColumnValue("NgayVao") != null && GetCurrentColumnValue("NgayVao").ToString().Length >= 10)
            {
                nv=GetCurrentColumnValue("NgayVao").ToString().Substring(0,5);
                colNgayVao.Text = nv.ToString();
            }
        }

        private void colNgayTT_BeforePrint(object sender, CancelEventArgs e)
        {
            string ntt = "";
            if (this.GetCurrentColumnValue("NgayTT") != null && GetCurrentColumnValue("NgayTT").ToString().Length >= 10)
            {
                ntt = GetCurrentColumnValue("NgayTT").ToString().Substring(0, 5);
                colNgayVao.Text = ntt.ToString();
            }
        }
        double thuoc = 0;
        double tieuhao = 0;
        double thuthuat = 0;
        double tong = 0;
        private void colTong_BeforePrint(object sender, CancelEventArgs e)
        {
          
            if (!string.IsNullOrEmpty(colThuoc.Text))
            {
                thuoc = Convert.ToDouble(colThuoc.Text);
                if (!string.IsNullOrEmpty(colTieuHao.Text))
                {
                    tieuhao = Convert.ToDouble(colTieuHao.Text);
                    if (!string.IsNullOrEmpty(colThuThuat.Text))
                    {
                        thuthuat = Convert.ToDouble(colThuThuat.Text);

                    }
                    else thuthuat = 0;
                }
                else tieuhao = 0;
            }
            else { thuoc = 0; }
            tong = thuoc + tieuhao + thuthuat;
            colTong.Text = tong.ToString("0,0");
        }

        private void colTongT_BeforePrint(object sender, CancelEventArgs e)
        {
            int thuoct = 0;
            double tieuhaot = 0;
            double thuthuatt = 0;
            double tongt = 0;
            if (ThuocT.Value!=null)
            {
                thuoct = Convert.ToInt32(ThuocT.Value);
                if (TieuHaoT.Value!=null)
                {
                    tieuhaot = Convert.ToInt32(TieuHaoT.Value);
                    if (TieuHaoT.Value!=null)
                    {
                        thuthuatt = Convert.ToInt32(TThuatT.Value);

                    }
                    else thuthuatt = 0;
                }
                else tieuhaot = 0;
            }
            else { thuoct = 0; }
            tongt =thuoct + tieuhaot + thuthuatt;
            colTongT.Text = tongt.ToString("0,0");
        } 
               
    }
}
