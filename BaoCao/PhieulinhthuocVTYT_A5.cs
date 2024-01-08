using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;
        public PhieulinhthuocVTYT_A5()
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT_A5(int dy)
        {
            InitializeComponent();
            _dongy = dy;
        }
        public void BindingData()
        {
            string _fomat = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
                _fomat = "{0:00}";
            else
                _fomat = "{0:##,###.##}";
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;
            }
            else
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            if (DungChung.Bien.MaBV == "24009")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;//.FormatString = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "27023")
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong");//.FormatString = DungChung.Bien.FormatString[0];
            //colTsongluongG.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            GroupHeader1.GroupFields.Add(new GroupField("MaBNhan"));

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (_dongy == 6)
                GroupHeader1.Visible = true;
            if (DungChung.Bien.MaBV == "30004")
            {
                int _soPL = 0;
                if (this.SoPL.Value != null && this.SoPL.Value.ToString() != "")
                {
                    _soPL = Convert.ToInt32(this.SoPL.Value.ToString());
                }
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var gc = (from bn in _data.BenhNhans
                          join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _data.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                          select bn.MaDTuong).ToList();
                if (gc.Count > 0)
                {
                    if (gc.Count == gc.Where(p => p == "TE").ToList().Count)
                    {
                        this.Chuthich.Value = "Trẻ em " + this.Chuthich.Value;
                    }
                }
            }

            if (DungChung.Bien.MaBV == "34019")
            {
                txtTenCQCQ.SizeF = new SizeF(190, 23);
                txtTenCQCQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                txtTenCQ.SizeF = new SizeF(190, 23);
                txtTenCQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel8.SizeF = new SizeF(190, 23);
                xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel2.LocationF = new PointF(float.Parse("391.52"), 0);
                xrLabel2.SizeF = new SizeF(float.Parse("140.4"), 23);
                xrLabel1.Text = "Mã xuất:";
                xrLabel1.LocationF = new PointF(float.Parse("391.52"), 23);
                xrLabel1.SizeF = new SizeF(float.Parse("59.15"), 23);
            }
            //if (SoPL.Value != null)
            //{
            //    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //    int _PL = Convert.ToInt32(SoPL.Value.ToString());
            //    var No = (from DT in _data.DThuocs.Where(p => p.SoPL == _PL)
            //              join bn in _data.BenhNhans on DT.MaBNhan equals bn.MaBNhan
            //              select new { bn.NoThe }).ToList();
            //    if (No.Count > 0 && No.First().NoThe != null && No.First().NoThe.Value == true)
            //    {
            //        NoThe.Text = "Dành cho bệnh nhân nợ thẻ";
            //    }
            //    else
            //    { NoThe.Text = "";}
            //}

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ..... tháng ..... năm 20...";
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa.Value != null)
                tenkp = Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count > 0 && kp.First() == DungChung.Bien.st_PhanLoaiKP.CanLamSang)
            {
                xrTableCell30.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell30.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }

            if (DungChung.Bien.MaBV == "01071")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30002")
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "34019")
            {
                lblTongSoKhoan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
        }
    }

}
