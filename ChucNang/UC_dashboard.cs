using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Globalization;
using QLBV.ChucNang;

namespace QLBV.FormNhap
{
    public partial class UC_dashboard : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_dashboard()
        {
            InitializeComponent();
            //chartControl2.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            chartControl2.RuntimeHitTesting = true;
        }

        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void dashboardDesigner1_Load(object sender, EventArgs e)
        {
        }

        private void UC_dashboard_Load(object sender, EventArgs e)
        {
            ThongKe_BenhNhan();
            lab_tencq.Text = DungChung.Bien.TenCQ.ToUpper();
            Change_ChartPie(DateTime.Today);
            LoadTB();
            LoadFormCanhBaoThuoc();
        }

        private void LoadFormCanhBaoThuoc()
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                var kphong = _dataContext.KPhongs.FirstOrDefault(o => o.MaKP == DungChung.Bien.MaKP);
                if (kphong != null && kphong.PLoai == "Khoa dược")
                {
                    frmCanhBaoThuoc frm = new frmCanhBaoThuoc();
                    frm.ShowDialog();
                }
            }
        }

        private void ThongKe_BenhNhan()
        {
            try
            {
                DateTime _now = DateTime.Now; //DateTime.Today;
                if (chartControl1.Series.Count > 0)
                {
                    chartControl1.BeginInit();
                    Series _series_Ngoaitru = chartControl1.Series[0];
                    Series _series_Noitru = chartControl1.Series[1];
                    Series _series_NoitruXuatVien = chartControl1.Series[2];
                    Series _series_Chuyen_Vien = chartControl1.Series[3];
                    Series _sersa = chartControl2.Series[0];
                    _series_Ngoaitru.Points.Clear();
                    _series_Noitru.Points.Clear();
                    _series_NoitruXuatVien.Points.Clear();
                    _sersa.Points.Clear();
                    foreach (Series series in chartControl1.Series)
                    {
                        AreaSeriesView view = series.View as AreaSeriesView;
                        if (view != null)
                            view.Transparency = Convert.ToByte(60);
                    }
                    //
                    DateTime ngaytu = DungChung.Ham.NgayTu(_now.AddDays(-9));
                    DateTime ngayden = DungChung.Ham.NgayDen(_now);
                    var Bnhan = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden) select bn).ToList();

                    /////nội trú ra viện
                    var rvien = (from ravien in _dataContext.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden) join bn in _dataContext.BenhNhans on ravien.MaBNhan equals bn.MaBNhan select new { ravien, bn.NoiTru }).ToList();

                    ////chuyển viện

                    /////////////////thành tiền
                    var vphi = (from vp in _dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi select new { vp.NgayTT, vpct }).ToList();
                    //

                    for (int i = -9; i <= 0; i++)
                    {
                        DateTime _getday = _now.AddDays(i);
                        ////////Bệnh nhân trong ngày
                        var _ld = (from bn in Bnhan.Where(p => p.NNhap.Value.Year == _getday.Year && p.NNhap.Value.Day == _getday.Day && p.NNhap.Value.Month == _getday.Month) select bn).ToList();
                        int _ngoaitru = _ld.Where(p => p.NoiTru == 0).ToList().Count;
                        int _noitru = _ld.Where(p => p.NoiTru == 1).ToList().Count;
                        /////nội trú ra viện

                        int _ntravien = (from it in rvien.Where(p => p.NoiTru == 1 && p.ravien.Status == 2 && p.ravien.NgayRa.Value.Year == _getday.Year && p.ravien.NgayRa.Value.Month == _getday.Month && _getday.Day == p.ravien.NgayRa.Value.Day) select it).ToList().Count;
                        ////chuyển viện
                        var _chVien = (from ravien in rvien.Where(p => p.ravien.Status == 1 && p.ravien.NgayRa.Value.Year == _getday.Year && p.ravien.NgayRa.Value.Month == _getday.Month && _getday.Day == p.ravien.NgayRa.Value.Day) select ravien).ToList();
                        int _slcv = _chVien.Count;

                        /////////////////thành tiền
                        var _tt = (from vp in vphi.Where(p => p.NgayTT.Value.Year == _getday.Year && p.NgayTT.Value.Month == _getday.Month && _getday.Day == p.NgayTT.Value.Day) select vp).ToList();
                        Int64 _tongtien = Convert.ToInt64(_tt.Sum(p => p.vpct.ThanhTien));
                        /////////////////////////////////////////////////////////////////////////////////
                        _series_Ngoaitru.Points.Add(new SeriesPoint(_getday.ToShortDateString(), _ngoaitru));
                        _series_Noitru.Points.Add(new SeriesPoint(_getday.ToShortDateString(), _noitru));
                        _series_NoitruXuatVien.Points.Add(new SeriesPoint(_getday.ToShortDateString(), _ntravien));
                        _series_Chuyen_Vien.Points.Add(new SeriesPoint(_getday.ToShortDateString(), _slcv));
                        _sersa.Points.Add(new SeriesPoint(_getday.ToShortDateString(), _tongtien));
                    }
                    chartControl1.EndInit();
                }
            }
            catch (Exception) { }
        }

        private void chartControl2_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl2.CalcHitInfo(e.X, e.Y);
            var diagram = chartControl2.Diagram as XYDiagram;
            if (hi.InDiagram)
            {
                var chartCoords = diagram.PointToDiagram(e.Location);
                Change_ChartPie(chartCoords.DateTimeArgument);
            }
        }
        private void Change_ChartPie(DateTime _date)
        {
            try
            {

                var _tt = (from vp in _dataContext.VienPhis.Where(p => p.NgayRa.Value.Year == _date.Year && p.NgayRa.Value.Month == _date.Month && _date.Day == p.NgayRa.Value.Day) join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi select vpct).ToList();
                Int64 _tongtien = Convert.ToInt64(_tt.Sum(p => p.ThanhTien));
                chartControl3.Titles[0].Text = "Số liệu thống kê ngày " + _date.ToString("dd/MM/yyyy");
                if (_tongtien > 0)
                {


                    double _bh = (_tt.Sum(p => p.TienBH));
                    double _bn = (_tt.Sum(p => p.TienBN));
                    chartControl3.BeginInit();
                    chartControl3.Series[0].Points.Clear();
                    chartControl3.Series[0].Points.Add(new SeriesPoint("Bảo hiểm trả", _bh));
                    chartControl3.Series[0].Points.Add(new SeriesPoint("Bệnh nhân trả", _bn));
                    chartControl3.Series[0].Name = string.Format("Tổng : {0:0,000} VNĐ", _bh + _bn);
                    chartControl3.EndInit();
                }
                else
                {

                    Int64 _bh = 0;
                    Int64 _bn = 0;
                    chartControl3.BeginInit();
                    chartControl3.Series[0].Points.Clear();
                    chartControl3.Series[0].Points.Add(new SeriesPoint("Bảo hiểm trả", _bh));
                    chartControl3.Series[0].Points.Add(new SeriesPoint("Bệnh nhân trả", _bn));
                    chartControl3.Series[0].Name = string.Format("Không có số liệu thống kê.");

                    chartControl3.EndInit();


                }
            }
            catch (Exception) { }
        }

        private void chartControl3_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {

        }

        private void btnthongbao_Click(object sender, EventArgs e)
        {
            QLBV.FormThamSo.frm_XemTB frm = new FormThamSo.frm_XemTB();
            frm.getdata = new FormThamSo.frm_XemTB.getString(GetValueSoThang);
            frm.ShowDialog();
        }
        void GetValueSoThang(int SoTBNew)
        {
            if (SoTBNew > 0)
                btnthongbao.Text = SoTBNew.ToString();
            else
                btnthongbao.Text = "";
        }
        private void LoadTB()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lTb = data.ThongBaos.Where(p => p.Status == 1).Where(p => DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin ? true : p.PLoai == "Toàn viện" || p.PLoai == DungChung.Bien.PLoaiKP).ToList();
            int dem = 0;
            string _macb = ";" + DungChung.Bien.MaCB + ";";
            foreach (var item in _lTb)
            {
                if (item.DSCBDaXem != null && item.DSCBDaXem.Contains(_macb))
                {

                }
                else
                {
                    dem++;
                }
            }
            if (dem > 0)
                btnthongbao.Text = dem.ToString();
            else
                btnthongbao.Text = "";
        }
    }
}
