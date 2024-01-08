using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThCPKCBTEKoThe_HL02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThCPKCBTEKoThe_HL02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            
            txtNoiNgoaiTru.DataBindings.Add("Text", DataSource, "NoiTru");
            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            txtGTinh.DataBindings.Add("Text", DataSource, "GTinh");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
            colMaCS.DataBindings.Add("Text", DataSource, "MaCS");
            colNguoiThan.DataBindings.Add("Text", DataSource, "NguoiThan");
            colNgayVao.DataBindings.Add("Text", DataSource, "NNhap").FormatString = "{0:dd/MM/yyyy}";
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            colGiaTriTu.DataBindings.Add("Text", DataSource, "HanBHTu").FormatString = "{0:dd/MM/yyyy}";
            colGiaTriDen.DataBindings.Add("Text", DataSource, "HanBHDen").FormatString = "{0:dd/MM/yyyy}";
            colTongCP.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongCPde.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongCPrf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NoiTru"));
        }

  
        private void colNoiNgoaiTru_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("NoiTru") != null )
            {
                if(GetCurrentColumnValue("NoiTru").ToString()== ("1"))
                {
                    colNoiNgoaiTru.Text = "Nội trú".ToUpper();
                }
                else if(GetCurrentColumnValue("NoiTru").ToString()== ("0"))
                {
                    colNoiNgoaiTru.Text = "Ngoại trú".ToUpper();
                }
            }
        }

       
        private void colGTinh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("GTinh") != null)
            {
                if (GetCurrentColumnValue("GTinh").ToString()== ("1"))
                {
                    colGTinh.Text = "Nam";
                }
                else
                    if (GetCurrentColumnValue("GTinh").ToString()== ("0"))
                    {
                        colGTinh.Text = "Nữ";
                    }
            }
        }


         int stt = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
         switch (stt)
         {
                case 1:
                    colSTT.Text = "I";
                     break;
                case 2:
                    colSTT.Text = "II";
                     break;
         }
            stt++;
        }

        private void colNguoiThan_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("MaBNhan") != null && this.GetCurrentColumnValue("MaBNhan").ToString() != "")
            {
                int _mabn= Convert.ToInt32( this.GetCurrentColumnValue("MaBNhan"));
                var nt = data.TTboXungs.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NThan }).ToList();
                if (nt.Count > 0)
                {
                    colNguoiThan.Text = nt.First().NThan;
                }
                else colNguoiThan.Text = "";
            }
        }

        
        
    }
}
