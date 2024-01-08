using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_TkDoiTuongHNTEct_HA08 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TkDoiTuongHNTEct_HA08()
        {
            InitializeComponent();
        }
       
        public void BindingData()
        {

            colNhomDTGh2.DataBindings.Add("Text", DataSource, "NhomDT");
            colNhomDTc.DataBindings.Add("Text", DataSource, "NhomDT");
            colCongNhom.DataBindings.Add("Text", DataSource, "NhomDT");
            colCong.DataBindings.Add("Text", DataSource, "NhomDT");
           
            colGTinhGh1.DataBindings.Add("Text", DataSource, "GTinh");
            txtGTinhsl.DataBindings.Add("Text", DataSource, "NhomDT");

            txtMax.DataBindings.Add("Text", DataSource, "Maxkb");
            txtMaxvv.DataBindings.Add("Text", DataSource, "Maxvv");
     
            colSoBA.DataBindings.Add("Text", DataSource, "SoBA");
            colHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            txtNamNu.DataBindings.Add("Text", DataSource, "GTinh");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colMaThe.DataBindings.Add("Text", DataSource, "SThe");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colMaBenh.DataBindings.Add("Text", DataSource, "MaBenh");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            txtNgay.DataBindings.Add("Text", DataSource, "NgayVV");


            GroupHeader1.GroupFields.Add(new GroupField("GTinh"));
            GroupHeader2.GroupFields.Add(new GroupField("NhomDT"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void txtNhomDT_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void txtGTinh_BeforePrint(object sender, CancelEventArgs e)
        {
            int gt=0;
            if (!string.IsNullOrEmpty(colGTinhGh1.Text))
            {
                gt = int.Parse(colGTinhGh1.Text);
                if (gt == 1)
                {
                    txtGTinh.Text = "Trong đó Nam: ";
                }
                else txtGTinh.Text = "Trong đó Nữ: ";
            }
            
        }


        string icd = "";
        string cd = "";
        private string tenkp(int id)
        {
            string ten = "";
            
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var sql = (from bnkb in _data.BNKBs.Where(p => p.IDKB == id)
                       join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP 
                        select new { bnkb.MaICD, bnkb.ChanDoan,kp.TenKP}).ToList();
            if (sql.Count > 0)
            {
                icd = sql.First().MaICD;
                cd = sql.First().ChanDoan;
                ten = sql.First().TenKP;
            }
            else
            {
                ten = "";
            }
            return ten;
        }

        private void colMaBenh_BeforePrint(object sender, CancelEventArgs e)
        {
            int id = 0;
            if (GetCurrentColumnValue("Maxkb") != null)
                id = Convert.ToInt32(GetCurrentColumnValue("Maxkb"));
            colKhoa.Text = tenkp(id);
            colMaBenh.Text = icd;
            colChanDoan.Text = cd;
            
        }

        private void colGioiTinh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("GTinh") != null) {
                if (GetCurrentColumnValue("GTinh").ToString() == "1")
                    colGioiTinh.Text = "Nam";
                else colGioiTinh.Text = "Nữ";
            }
        }

        private void colNgayVV_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("NgayVV") != null && GetCurrentColumnValue("NgayVV").ToString().Length>=10)
            {
                colNgayVV.Text = GetCurrentColumnValue("NgayVV").ToString().Substring(0,10);
                
            }
        }

        private void colNhomDTGh2_BeforePrint(object sender, CancelEventArgs e)
        {
            string dt = "";
            //int dtte = 0;
            if (GetCurrentColumnValue("NhomDT")!=null)
            {
                dt = GetCurrentColumnValue("NhomDT").ToString();
            }
            colNhomDTGh2.Text = "Nhóm đối tượng " + dt + ":  ";
        }

        private void colNhomDT(object sender, CancelEventArgs e)
        {

        }
    }
}
