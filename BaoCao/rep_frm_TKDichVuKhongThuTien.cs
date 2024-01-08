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
    public partial class rep_frm_TKDichVuKhongThuTien : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_frm_TKDichVuKhongThuTien()
        {
            InitializeComponent();
        }

        List<FormThamSo.frm_TKDichVuKhongThuTien.c_DSDV> _lDichVu = new List<FormThamSo.frm_TKDichVuKhongThuTien.c_DSDV>();
        List<FormThamSo.frm_TKDichVuKhongThuTien.BenhNhan> _lSoLuongt = new List<FormThamSo.frm_TKDichVuKhongThuTien.BenhNhan>();
        public rep_frm_TKDichVuKhongThuTien(List<FormThamSo.frm_TKDichVuKhongThuTien.c_DSDV> lDichVu, List<FormThamSo.frm_TKDichVuKhongThuTien.BenhNhan> lSoLuongt)
        {
            InitializeComponent();
            this._lDichVu = lDichVu;
            this._lSoLuongt = lSoLuongt;
        }
        public void BindingData()
        {
            colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSoBA.DataBindings.Add("Text", DataSource, "SoBA");
            colDiachi.DataBindings.Add("Text", DataSource, "diachi");
            colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa");
            colNgayVao.DataBindings.Add("Text", DataSource, "ngaythang");
            col_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            int i = 1;
            foreach (XRTableCell _tableCell in xrTableRow1) {
                
                if (_tableCell.Name == "colKQ" + (i).ToString())
                {
                    _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                    i++;
                }
            }
         }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            int _makp = DungChung.Bien.MaKP;
            var q = from kp in _data.KPhongs.Where(p => p.MaKP == _makp) select new { kp.TenKP };
            if (q.Count() > 0)
            {
                colKP.Text = q.First().TenKP;
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
                        int _madv = 0;
                        if (_lDichVu.Skip(i).ToList().Count > 0)
                            _madv = _lDichVu.Skip(i).First().MaDV;
                        string _soluong ="";
                        if (_lSoLuongt.Where(p => p.MaDV == _madv).ToList().Count > 0 && _lSoLuongt.Where(p => p.MaDV == _madv).ToList().First().Soluong != null)
                            _soluong = _lSoLuongt.Where(p => p.MaDV == _madv).ToList().First().Soluong.ToString("##,###.##");
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
                        string _tendv = _lDichVu.Skip(i).First().TenDV;
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

    }
}
