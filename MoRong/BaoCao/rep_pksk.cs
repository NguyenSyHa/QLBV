using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace QLBV.BaoCao
{
    public partial class rep_pksk : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_pksk()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV =="30009")
            {
                xrLabel2.Text = "QUẢN LÝ"+Environment.NewLine+" HỒ SƠ KHÁM SỨC KHỎE ĐỊNH KỲ CÁN BỘ TRUNG TÂM";
                //xrLabel2.Font = new Font(xrLabel2.Font.FontFamily, 14);
                xrLabel2.Font = new Font(xrLabel2.Font, FontStyle.Bold);
            }
        }
        public bool nam;
        public bool nu;
        public string DUONGDAN;
        public string hoten;
        public string tuoi ;
        public string socmthu;
        public string hokhau;
        public string ngaycap;
        public string choo;
        public string nghenghiep;
        public string coqua;
        public string ngaybatdaulam;
        public string tiensubenhgiadinh;
        public string tiensubanthan;
        public string tenbenh;
        public string tenbenhnghenghiep;
        public string namphathien;
        public string namphathien1;
        public string ketquals;
        public string ketquacls;
        public string ploaiksk;
        public string ploaibt;
     public  void ham()
        {
         xrPictureBox1.ImageUrl = DUONGDAN;
         che_nam.Checked = nam;
         che_nu.Checked = nu;
         rep_hote.Text = hoten;
         rep_Tuoi.Text = tuoi;
         rep_ngaycap.Text = ngaycap;
         rep_hochieu.Text = socmthu;
         hokhaucvbvc.Text = hokhau;
         thuongchu.Text = choo;
         nghenghiepcvbvc.Text = nghenghiep;
         coquanbvnbv.Text = coqua;
         ngaybatdaylamviec.Text = ngaybatdaulam;
         tiensubenh.Text = tiensubenhgiadinh;
         tiensubenhbanthan.Text = tiensubanthan;
         rep_tenbenh.DataBindings.Add("Text", DataSource, "Tenbenh");
         rep_nam.DataBindings.Add("Text", DataSource, "Namph");
         rep_Tenbenhnghenghiep.DataBindings.Add("Text", DataSource, "Tenbenhnn");
         rep_nam1.DataBindings.Add("Text", DataSource, "Namphnn");
         rep_ketquals.Text = ketquals;
         rep_ketquacls.Text = ketquacls;
         rep_phanloaiksk.Text =ploaiksk;
         rep_ploaibt.Text = ploaibt;
     }

    }
}
