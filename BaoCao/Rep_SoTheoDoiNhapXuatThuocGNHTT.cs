using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoTheoDoiNhapXuatThuocGNHTT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoTheoDoiNhapXuatThuocGNHTT()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        public void BindingData()
        {
            txtNgaythang.DataBindings.Add("Text", DataSource, "Ngaythang");
            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colNoiXN.DataBindings.Add("Text", DataSource, "NoiXN");
            txtSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            txtHanDung.DataBindings.Add("Text", DataSource, "Handung");
            colSoluongN.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongX.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[1];
            //txtTon.DataBindings.Add("Text", DataSource, "SLTon").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongNtc.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongXtc.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[1];
          //  colSoLuongTontc.DataBindings.Add("Text", DataSource, "SLTon").FormatString = DungChung.Bien.FormatString[1];
            //colTon.DataBindings.Add("Text", DataSource, "Ton");
            colGhiChu.DataBindings.Add("Text", DataSource, "GChu");
        }
       
        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {
           
            if (this.GetCurrentColumnValue("Ngaythang") != null)
            {
                string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
                colNTN.Text = nt.Substring(0, 10);
            }
        }

     

        //private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    sltdk();
        //}

        private void colSoLoHD_BeforePrint(object sender, CancelEventArgs e)
        {
            string solo = ""; string handung = ""; ; string nhasx = ""; string nuocsx = "";
            if (this.GetCurrentColumnValue("SoLo") != null && this.GetCurrentColumnValue("SoLo") != "")
            {
                solo = this.GetCurrentColumnValue("SoLo").ToString();
            }
            if (this.GetCurrentColumnValue("HanDung") != null && this.GetCurrentColumnValue("HanDung") != "")
            {
                handung = this.GetCurrentColumnValue("HanDung").ToString();
            }
          
            if (solo != null && solo != "" && handung != null && handung != "") { colSoLoHD.Text = solo.ToString() + " - " + handung.ToString().Substring(0, 10); }
            if (solo != null && solo != "" && (handung == null || handung == "")) { colSoLoHD.Text = solo.ToString(); }
            if ((solo == null || solo == "") && handung != null && handung != "") { colSoLoHD.Text = handung.ToString().Substring(0, 10); }
        }
        double sltck = 0; int sltdk = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (InDK.Value.ToString() != null)
            {
                string _tnhom = InDK.Value.ToString();
                if (_tnhom == "1")
                    GroupHeader1.Visible = false;
            }
            int _Maduoc = Madv.Value == null ? 0 : Convert.ToInt32(Madv.Value);
            DateTime _ngaytu = Convert.ToDateTime(ngaytu.Value);
            DateTime _ngayden = Convert.ToDateTime(ngayden.Value);
            int _Makp = Khoaphong.Value == null ? 0 : Convert.ToInt32(Khoaphong.Value);
            var b = (from nx in Data.NhapDs.Where(p => p.MaKP == _Makp)
                     join nxct in Data.NhapDcts.Where(p => p.MaDV == _Maduoc) on nx.IDNhap equals nxct.IDNhap
                     group new { nx, nxct } by new { nx.PLoai, nx.MaKP } into kp
                     select new
                     {
                         SLTDK = (kp.Where(p => p.nx.NgayNhap < _ngaytu).Count() > 0) ? (kp.Where(p => p.nx.NgayNhap < _ngaytu).Sum(p => p.nxct.SoLuongN) - kp.Where(p => p.nx.NgayNhap < _ngaytu).Sum(p => p.nxct.SoLuongX)) : 0,
                      }).ToList();
            if (b.Count > 0)
            {
                 sltdk = Convert.ToInt32(b.Sum(p => p.SLTDK));
                colTonDK.Text = sltdk.ToString("#,#");
            }
            else { colTonDK.Text = ""; }
        }
         private void colSoLuongTon_BeforePrint_1(object sender, CancelEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(colSoluongN.Text))
            {

                sltck += +Convert.ToDouble(colSoluongN.Text);
            }
            if (!string.IsNullOrEmpty(colSoLuongX.Text))
            {

                sltck += -Convert.ToDouble(colSoLuongX.Text);
            }
            colSoLuongTon.Text =(sltdk+ sltck).ToString();
          //  colSoLuongTontc.Text = (sltdk + sltck).ToString();
        }

         private void colSoLuongTontc_BeforePrint(object sender, CancelEventArgs e)
         {
             //colSoLuongTontc.Text = (sltdk + sltck).ToString();
    
         }

  
    }
}
