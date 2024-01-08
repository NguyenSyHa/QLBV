using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoCLS_NS : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoCLS_NS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            txt_MaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            txtGT.DataBindings.Add("Text", DataSource, "GTinh");
            txt_Tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            txtBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            txt_MaKP.DataBindings.Add("Text", DataSource, "MaKP");
            txt_MaCB.DataBindings.Add("Text", DataSource, "MaCB");
            txt_MaCBth.DataBindings.Add("Text", DataSource, "MaCBth");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colKetQua.DataBindings.Add("Text", DataSource, "KQNS");
            //colGhiChu.DataBindings.Add("Text", DataSource, "");
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtGT.Text == "1")
            {
                colNam.Text = txt_Tuoi.Text;
            }
            else
            {
                colNam.Text = "";
            }
        }

        private void colNu_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtGT.Text == "0")
            {
                colNu.Text = txt_Tuoi.Text;
            }
            else
            {
                colNu.Text = "";
            }

        }

        private void colBHYT_BeforePrint(object sender, CancelEventArgs e)
        {
            if(txtBHYT.Text == "BHYT")
            {
                colBHYT.Text = "X";
            }    
        }

        private void colNoiGui_BeforePrint(object sender, CancelEventArgs e)
        {
            int _makp = Convert.ToInt32(txt_MaKP.Text);
            var khoaphong = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
            if (khoaphong.Count() > 0)
            {
                colNoiGui.Text = khoaphong.First().TenKP;
            }
            else
            {
                colNoiGui.Text = "";
            }
        }

        private void colYeuCau_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.Where(p => p.MaCB == txt_MaCB.Text).ToList();
            if (tencb.Count() > 0)
            {
                colYeuCau.Text = tencb.First().TenCB;
            }    
            else
            {
                colYeuCau.Text = "";
            }    
        }

        private void colNguoiDoc_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.Where(p => p.MaCB == txt_MaCBth.Text).ToList();
            if (tencb.Count() > 0)
            {
                colNguoiDoc.Text = tencb.First().TenCB;
            }    
            else
            {
                colNguoiDoc.Text = "";
            }    
        }
    }
}
