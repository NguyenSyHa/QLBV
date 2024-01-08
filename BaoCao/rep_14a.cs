using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_14a : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_14a()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colBenhnhandungtuyen.DataBindings.Add("Text", DataSource, "Benhnhandungtuyen").FormatString = DungChung.Bien.FormatString[1];
            colBenhnhantraituyen.DataBindings.Add("Text", DataSource, "Benhnhantraituyen").FormatString = DungChung.Bien.FormatString[1];
            colBHXHchitra.DataBindings.Add("Text", DataSource, "BHXHchitra").FormatString = DungChung.Bien.FormatString[1];
            colBHXHtuchoithanhtoan.DataBindings.Add("Text", DataSource, "BHXHtuchoithanhtoan").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colDVKTcao.DataBindings.Add("Text", DataSource, "DVKTcao").FormatString = DungChung.Bien.FormatString[1];
            //colSoluotdungtuyen.DataBindings.Add("Text", DataSource, "Soluotdungtuyen");
            colSoluotTTTT.DataBindings.Add("Text", DataSource, "SoluotTTTT");
            //colSoluottraituyen.DataBindings.Add("Text", DataSource, "Soluottraituyen");
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colTentuyen.DataBindings.Add("Text", DataSource, "Tentuyen");
            colTienkham.DataBindings.Add("Text", DataSource, "Tienkham").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuocDT.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
            colThuocthaighep.DataBindings.Add("Text", DataSource, "Thuocthaighep").FormatString = DungChung.Bien.FormatString[1];
            colVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYTtieuhao.DataBindings.Add("Text", DataSource, "VTYTtieuhao").FormatString = DungChung.Bien.FormatString[1];
            colVTYTthaythe.DataBindings.Add("Text", DataSource, "VTYTthaythe").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "mau").FormatString = DungChung.Bien.FormatString[1];
            colRFBHXH.DataBindings.Add("Text", DataSource, "BHXHchitra").FormatString = DungChung.Bien.FormatString[1];
            colRFCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            //colRFDungtuyen.DataBindings.Add("Text", DataSource, "Soluotdungtuyen");
            colRFDVKTC.DataBindings.Add("Text", DataSource, "DVKTcao").FormatString = DungChung.Bien.FormatString[1];
            colRFMau.DataBindings.Add("Text", DataSource, "mau").FormatString = DungChung.Bien.FormatString[1];
            colRFTienDT.DataBindings.Add("Text", DataSource, "Benhnhandungtuyen").FormatString = DungChung.Bien.FormatString[1];
            colRFTienkham.DataBindings.Add("Text", DataSource, "Tienkham").FormatString = DungChung.Bien.FormatString[1];
            colRFTKTG.DataBindings.Add("Text", DataSource, "Thuocthaighep").FormatString = DungChung.Bien.FormatString[1];
            colRFTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colRFTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colRFTtraituyen.DataBindings.Add("Text", DataSource, "Benhnhantraituyen").FormatString = DungChung.Bien.FormatString[1];
            colRFThuoc.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
           // colRFTraituyen.DataBindings.Add("Text", DataSource, "Soluottraituyen");
            colRFVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colRFVTYTtieuhao.DataBindings.Add("Text", DataSource, "VTYTtieuhao").FormatString = DungChung.Bien.FormatString[1];
            colRFVTYTThaythe.DataBindings.Add("Text", DataSource, "VTYTthaythe").FormatString = DungChung.Bien.FormatString[1];
            colRFxetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colRFBHXHtuchoi.DataBindings.Add("Text", DataSource, "BHXHtuchoithanhtoan").FormatString = DungChung.Bien.FormatString[1];
            //GF
            colGFBHXH.DataBindings.Add("Text", DataSource, "BHXHchitra").FormatString = DungChung.Bien.FormatString[1];
            colGFCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            //colRFDungtuyen.DataBindings.Add("Text", DataSource, "Soluotdungtuyen");
            colGFDVKTC.DataBindings.Add("Text", DataSource, "DVKTcao").FormatString = DungChung.Bien.FormatString[1];
            colGFMau.DataBindings.Add("Text", DataSource, "mau").FormatString = DungChung.Bien.FormatString[1];
            colGFTienDT.DataBindings.Add("Text", DataSource, "Benhnhandungtuyen").FormatString = DungChung.Bien.FormatString[1];
            colGFTienkham.DataBindings.Add("Text", DataSource, "Tienkham").FormatString = DungChung.Bien.FormatString[1];
            colGFTKTG.DataBindings.Add("Text", DataSource, "Thuocthaighep").FormatString = DungChung.Bien.FormatString[1];
            colGFTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colGFTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colGFTtraituyen.DataBindings.Add("Text", DataSource, "Benhnhantraituyen").FormatString = DungChung.Bien.FormatString[1];
            colGFThuoc.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
            // colRFTraituyen.DataBindings.Add("Text", DataSource, "Soluottraituyen");
            colGFVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colGFVTYTtieuhao.DataBindings.Add("Text", DataSource, "VTYTtieuhao").FormatString = DungChung.Bien.FormatString[1];
            colGFVTYTThaythe.DataBindings.Add("Text", DataSource, "VTYTthaythe").FormatString = DungChung.Bien.FormatString[1];
            colGFxetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colGFBHXHtuchoi.DataBindings.Add("Text", DataSource, "BHXHtuchoithanhtoan").FormatString = DungChung.Bien.FormatString[1];
            //
            colRFSoluottructiep.DataBindings.Add("Text", DataSource, "SLTT");
            colRFTT2.DataBindings.Add("Text", DataSource, "TT2");
            colRFTT3.DataBindings.Add("Text", DataSource, "TT3");
            TXThangBV.DataBindings.Add("Text", DataSource, "HangBV");
            GroupHeader1.GroupFields.Add(new GroupField("HangBV"));

        }
        int tt1 = 0;
        int dt1 = 0;
        int dt2 = 0;
        int tt2 = 0;
        int dt3 = 0;
        int tt3 = 0;
        int dt4 = 0;
        int tt4 = 0;
        int dt5 = 0;
        int tt5 = 0;
        int dt6 = 0;
        int tt6 = 0;
        int dt = 0;
        int tt = 0;
        //DateTime ngaytu1 = System.DateTime.Now.Date;
        //DateTime ngayden1 = System.DateTime.Now.Date;
        private void soluot(string _HangBV)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //ngaytu1 = GetCurrentColumnValue("ngaytu").ToString().Trim();
            //ngayden1 = ngayden.Value;
            DateTime _ngayt = DungChung.Ham.NgayTu(Convert.ToDateTime(ngaytu.Value));
            DateTime _ngayd = DungChung.Ham.NgayDen(Convert.ToDateTime(ngayden.Value));
            int TD = Convert.ToInt32(_HangBV);
            var q = (from bn in Data.BenhNhans.Where(p => p.NoiTru == 0).Where(p=>p.TuyenDuoi==TD)
                     //join BV in Data.BenhViens.Where(p => p.TuyenBV.Trim()== (_HangBV)) on bn.MaCS equals BV.MaBV
                     join nhom in Data.DTuongs on bn.SThe.Substring(0, 2) equals nhom.MaDTuong
                     join vp in Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                     where (vp.NgayTT >= _ngayt && vp.NgayTT <= _ngayd && vp.Duyet != 0)
                     where (nhom.Nhom == "Nhóm 1" || nhom.Nhom == "Nhóm 2" || nhom.Nhom == "Nhóm 3" || nhom.Nhom == "Nhóm 4" || nhom.Nhom == "Nhóm 5" || nhom.Nhom == "Nhóm 6")
                     group new { bn, nhom } by new { bn.MaBNhan, bn.Tuyen, nhom.Nhom,bn.Tuoi, bn.TuyenDuoi } into kq
                     select new { tuyen = kq.Key.Tuyen, SL = kq.Key.MaBNhan, NH = kq.Key.Nhom, SLX = kq.Key.Tuoi, TuyenDuoi = kq.Key.TuyenDuoi }).ToList();
            if (q.Count > 0)
            {
                if (TD == 1)
                {
                    dt1 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 1").Sum(p => p.SLX).Value;
                    tt1 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 1").Sum(p => p.SLX).Value;
                    dt2 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 2").Sum(p => p.SLX).Value;
                    tt2 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 2").Sum(p => p.SLX).Value;
                    dt3 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 3").Sum(p => p.SLX).Value;
                    tt3 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 3").Sum(p => p.SLX).Value;
                    dt4 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 4").Sum(p => p.SLX).Value;
                    tt4 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 4").Sum(p => p.SLX).Value;
                    dt5 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 5").Sum(p => p.SLX).Value;
                    tt5 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 5").Sum(p => p.SLX).Value;
                    dt6 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 6").Sum(p => p.SLX).Value;
                    tt6 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 6").Sum(p => p.SLX).Value;
                    tt = q.Where(p => p.tuyen == 2).Sum(p => p.SLX).Value;
                    dt = q.Where(p => p.tuyen == 1).Sum(p => p.SLX).Value;
                }
                else
                {
                    dt1 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 1").Select(p => p.SL).Count();
                    tt1 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 1").Select(p => p.SL).Count();
                    dt2 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 2").Select(p => p.SL).Count();
                    tt2 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 2").Select(p => p.SL).Count();
                    dt3 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 3").Select(p => p.SL).Count();
                    tt3 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 3").Select(p => p.SL).Count();
                    dt4 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 4").Select(p => p.SL).Count();
                    tt4 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 4").Select(p => p.SL).Count();
                    dt5 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 5").Select(p => p.SL).Count();
                    tt5 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 5").Select(p => p.SL).Count();
                    dt6 = q.Where(p => p.tuyen == 1).Where(p => p.NH == "Nhóm 6").Select(p => p.SL).Count();
                    tt6 = q.Where(p => p.tuyen == 2).Where(p => p.NH == "Nhóm 6").Select(p => p.SL).Count();
                    tt = q.Where(p => p.tuyen == 2).Select(p => p.SL).Count();
                    dt = q.Where(p => p.tuyen == 1).Select(p => p.SL).Count();
                }

            }
        }
        int sldt = 0;
        int sltt = 0;
        private void colSoluotdungtuyen_BeforePrint(object sender, CancelEventArgs e)
        {
          //  MessageBox.Show("Test");

            if (this.GetCurrentColumnValue("HangBV") != null)
            {
                string BV = this.GetCurrentColumnValue("HangBV").ToString();
                soluot(BV);
                //int sldt = 0;
                //int sltt = 0;

                string tuyen = "";
                if (this.GetCurrentColumnValue("Tentuyen") != null)
                {
                    colGFDungtuyen.Text = dt.ToString();
                    colGFTraituyen.Text = tt.ToString();
                    tuyen = GetCurrentColumnValue("Tentuyen").ToString().Trim();
                    switch (tuyen)
                    {
                        case ("Nhóm 1"):
                            colSoluotdungtuyen.Text = dt1.ToString();
                            sldt = sldt + dt1;
                            colSoluottraituyen.Text = tt1.ToString();
                            sltt = sltt + tt1;
                            break;
                        case ("Nhóm 2"):
                            colSoluotdungtuyen.Text = dt2.ToString();
                            sldt = sldt + dt2;
                            colSoluottraituyen.Text = tt2.ToString();
                            sltt = sltt + tt2;
                            break;
                        case ("Nhóm 3"):
                            colSoluotdungtuyen.Text = dt3.ToString();
                            sldt = sldt + dt3;
                            colSoluottraituyen.Text = tt3.ToString();
                            sltt = sltt + tt3;
                            break;
                        case ("Nhóm 4"):
                            colSoluotdungtuyen.Text = dt4.ToString();
                            sldt = sldt + dt4;
                            colSoluottraituyen.Text = tt4.ToString();
                            sltt = sltt + tt4;
                            break;
                        case ("Nhóm 5"):
                            colSoluotdungtuyen.Text = dt5.ToString();
                            sldt = sldt + dt5;
                            colSoluottraituyen.Text = tt5.ToString();
                            sltt = sltt + tt5;
                            break;
                        case ("Nhóm 6"):
                            colSoluotdungtuyen.Text = dt6.ToString();
                            sldt = sldt + dt6;
                            colSoluottraituyen.Text = tt6.ToString();
                            sltt = sltt + tt6;
                            break;
                    }

                }
            }
            
              

        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            txtDiaChi.Text = DungChung.Bien.MaBV;
            txtSoyte.Text = DungChung.Bien.TenCQCQ;
            //soluot();
            //colRFDungtuyen.Text = dt.ToString();
            //colRFTraituyen.Text = tt.ToString();
        }

        private void colTentuyen_BeforePrint(object sender, CancelEventArgs e)
        {
            //soluot();
            //colRFDungtuyen.Text = dt.ToString();
            //colRFTraituyen.Text = tt.ToString();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          //  MessageBox.Show("Test");
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("HangBV") != null)
            {
                string HangBV = this.GetCurrentColumnValue("HangBV").ToString().Trim();
                if(HangBV=="1")
                    colGHTenBV.Text = "D. TUYẾN XÃ";
                else
                    colGHTenBV.Text = "C. TUYẾN HUYỆN";
                //switch (HangBV)
                //{
                //    case "0":
                //        colGHTenBV.Text = "C. TUYẾN HUYỆN";
                //        //colTieuDeGF.Text = "CỘNG A";
                //        break;
                //    case "1":
                //        colGHTenBV.Text = "D. TUYẾN XÃ";
                //        //colTieuDeGF.Text = "CỘNG B";
                //        break;

                //}
            }
        }

        private void xrTableCell31_BeforePrint(object sender, CancelEventArgs e)
        {
            colRFDungtuyen.Text = sldt.ToString();
            colRFTraituyen.Text = sltt.ToString();
        }
    }
}
        

     

        
   