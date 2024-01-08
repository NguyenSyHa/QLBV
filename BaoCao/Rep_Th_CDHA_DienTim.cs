using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_Th_CDHA_DienTim : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_Th_CDHA_DienTim()
        {
            InitializeComponent();
        }

        List<FormThamSo.Frm_TongHop_CLS.c_DSDV> _lDichVu = new List<FormThamSo.Frm_TongHop_CLS.c_DSDV>();
        List<FormThamSo.Frm_TongHop_CLS.BenhNhan> _lSoLuongt = new List<FormThamSo.Frm_TongHop_CLS.BenhNhan>();
        public Rep_Th_CDHA_DienTim(List<FormThamSo.Frm_TongHop_CLS.c_DSDV> lDichVu, List<FormThamSo.Frm_TongHop_CLS.BenhNhan> lSoLuongt)
        {
            InitializeComponent();
            this._lDichVu = lDichVu;
            this._lSoLuongt = lSoLuongt;
        }
        public void BindingData()
        {
            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            colGioitinh.DataBindings.Add("Text", DataSource, "gioitinh");
            colGTNu.DataBindings.Add("Text", DataSource, "gioitinh_nu");
            colDiachi.DataBindings.Add("Text", DataSource, "diachi");
            colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
            colTenKP.DataBindings.Add("Text", DataSource, "noigui");
            colChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
            colKQ.DataBindings.Add("Text", DataSource, "KetLuan");
            colTenCBth.DataBindings.Add("Text", DataSource, "TenCBth");
            colYeCau.DataBindings.Add("Text", DataSource, "YeuCau");
            cell_MaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            cell_DTVP.DataBindings.Add("Text", DataSource, "DichVu");
            cellBSDT.DataBindings.Add("Text", DataSource, "TenCBcd");
            NgayThang.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM/yy HH:mm}";
              colNgayTH.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
              if (DungChung.Bien.MaBV == "30010")
              {
                  colCKham.DataBindings.Add("Text", DataSource, "CKham");
                  colCKhamT.DataBindings.Add("Text", DataSource, "CKham");
              }
            GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));

        }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            int _makp = DungChung.Bien.MaKP;
            var q = from kp in _data.KPhongs.Where(p => p.MaKP == _makp) select new { kp.TenKP };
            if (q.Count() > 0)
            {
               labKhoaPhong.Text = q.First().TenKP;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                foreach (XRTableCell _tableCell in xrTableRow3)
                {
                    //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                    if (_tableCell.Name == "colT" + (i + 1).ToString())
                    {
                        string _madvct = "";
                        if (_lDichVu.Skip(i).ToList().Count > 0)
                            _madvct = _lDichVu.Skip(i).First().MaDVct;
                        string _soluong = "";
                        if (_lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().Count > 0 && _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua != null)
                            _soluong = _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua;
                        else
                            _soluong = "";
                        _tableCell.Text = _soluong.ToString();
                        break;
                    }
                }

            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                foreach (XRTableCell _tableCell in xrTableRow2)
                {
                    if (_tableCell.Name == "TEN" + (i + 1).ToString())
                    {
                        string _tendv = _lDichVu.Skip(i).First().TenDVct;
                        _tableCell.Text = _tendv;
                        if (_tendv.Length > 40)
                        {
                            _tableCell.Font = new Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                        }
                    }
                }

            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void TEN30_BeforePrint(object sender, CancelEventArgs e)
        {

        }


        private void colTenCBth_BeforePrint(object sender, CancelEventArgs e)
        {
            string x= TenBC.Value.ToString();
            if (DungChung.Bien.MaBV == "30009" && (TenBC.Value.ToString().Contains("SIÊU ÂM") || TenBC.Value.ToString().Contains("X-QUANG"))) { colTenCBth.Text = "Nguyễn Mạnh Thắng"; }
        }
    }
}
