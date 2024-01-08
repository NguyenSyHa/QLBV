using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_NXT_GAYNG_HTT : DevExpress.XtraReports.UI.XtraReport
    {
        double xtrang = 0;

        public rep_NXT_GAYNG_HTT()
        {
            InitializeComponent();
        }
        public rep_NXT_GAYNG_HTT(List<lds> ds)
        {
            InitializeComponent();
            _lds = ds;
        }
      
        public class lds
        {

            int pLoai;
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public int PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            double sLN;

            public double SLN
            {
                get { return sLN; }
                set { sLN = value; }
            }
            double sLX;

            public double SLX
            {
                get { return sLX; }
                set { sLX = value; }
            }
        }
        List<lds> _lds = new List<lds>();
        private void repsothekho_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        double sltck = 0;
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
     
        private void sltdk()
        {
            //try
            //{
                int _Maduoc = 0;
                if (this.GetCurrentColumnValue("MaDV") != null)
                    _Maduoc =Convert.ToInt32( this.GetCurrentColumnValue("MaDV"));
                DateTime _ngaytu = Convert.ToDateTime(ngaytu.Value);
                //DateTime _ngayden = Convert.ToDateTime(Ngaythang.Value);
            
                var b = (from a in _lds where a.MaDV == _Maduoc
                         group a by new { a.PLoai, a.MaKP } into kp
                         select new
                         {
                             SLTDK = kp.Sum(p => p.SLN) - kp.Sum(p => p.SLX),
                             Makp = kp.Key.MaKP
                         }).ToList();
                if (b !=null && b.Count() > 0)
                {
                    
                    sltck = b.Sum(p => p.SLTDK);

                }
                else
                {
                    sltck = 0;

                }
            //}catch(Exception){
                
            //System.Windows.Forms.MessageBox.Show("Lỗi hàm tính tồn cuối ");
            //this.Dispose();
            //}
        }
        //int sltdk = 0;
        private void xrTableCell19_BeforePrint(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(colNhap.Text)) {

                //sltck += +int.Parse(colNhap.Text);
                sltck += +Convert.ToInt32(GetCurrentColumnValue("SLNhap"));

            }
            if (!string.IsNullOrEmpty(colXuat.Text))
            {

               // sltck += -Convert.ToInt32(colXuat.Text);

                sltck += -Convert.ToInt32(GetCurrentColumnValue("SLXuat"));

            }

            string[] arr = DungChung.Bien.FormatString[1].Split(':');
            int x1 = arr[1].ToString().Length;
            string x = arr[1].Substring(0, x1-1);
            colTon.Text = sltck.ToString(x);
        }

        private void colSCTn_BeforePrint(object sender, CancelEventArgs e)
        {
        } 
        public void BindingData()
        {
            colNgaythang.DataBindings.Add("Text", DataSource, "Ngaythang");
            colDiengiai.DataBindings.Add("Text", DataSource, "Ghichu");
            colSCT.DataBindings.Add("Text", DataSource, "SCT");
            //colSoluongton.DataBindings.Add("Text", DataSource, "Soluongton");
            colXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[1];
            colNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            colSoLo_HanDung.DataBindings.Add("Text", DataSource, "SoLo_HanDung");
            //colTon.DataBindings.Add("Text", DataSource, "Ton");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colphanl.DataBindings.Add("Text", DataSource, "Phanloai");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            //celGhichu.DataBindings.Add("Text", DataSource, "Ghichu");
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
        }
        //int tdk = 0;
        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
       // int td=0;

        string rong = "";
        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("Ngaythang") != null)
            {
                string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
                colPhanloai.Text = nt.Substring(0, 10);
            }
        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNgayt.Text = colNgaythang.Text.Substring(0, 10);
            //ToString().Substring(0, 10);
            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Ngaythang") != null)
            //{
            //    string nt = this.GetCurrentColumnValue("Ngaythang").ToString();
            //    colPhanloai.Text = nt.Substring(0, 10);
            //}
        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            sltdk();
        }
        int _break = 0;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            sltdk();
            //sltck = 0;
        
            if (_break == 0)
            {
                xrPageBreak1.Visible = false;
                xrLine2.Visible = false;
            }
            else
            {
                xrLine2.Visible = true;
                //xrPageBreak1.Visible = true;
                xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
            }
            _break++; 
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //xrPageBreak1.Visible = Convert.ToBoolean(PhanTrang.Value);
        }

        private void BottomMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel8.Text = xtrang.ToString();
            xtrang = xtrang + 0.5;
        }

        private void BottomMargin_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            txtThuKho.Text = DungChung.Bien.ThuKho;
            txtTruongKD.Text = DungChung.Bien.TruongKhoaDuoc;
            if (DungChung.Bien.MaBV == "27022")
                xrTable5.Visible = false;
        }
    }
}
