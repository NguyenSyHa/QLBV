using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNHuyetHoc_VX03 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNHuyetHoc_VX03()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDVct");
            colKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            colTSBT.DataBindings.Add("Text", DataSource, "TSBT");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string _macbdt = MsBSCD.Value.ToString();
            int _makdt = KhoaBSCD.Value == null ? 0 : Convert.ToInt32(KhoaBSCD.Value);
            var qcb = (from bs in _data.CanBoes.Where(p => p.MaCB == _macbdt) select new { bs.TenCB }).ToList();
            if (qcb.Count > 0)
            {
                colBSCD.Text = qcb.First().TenCB;
            }
            var qk = (from kp in _data.KPhongs.Where(p => p.MaKP == _makdt) select new { kp.TenKP }).ToList();
            if (qk.Count > 0)
            {
                colKhoaDT.Text = qk.First().TenKP;
            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            // int  _mabn = 0;
            string _mabsck = MsBSXN.Value.ToString();

            var qcb = (from bs in _data.CanBoes.Where(p => p.MaCB == _mabsck) select bs).ToList();
            if (qcb.Count > 0)
            {
                if (qcb.Count > 0)
                {
                    if (DungChung.Bien.MaBV == "27183")
                    {

                        colTenTKXN.Text = qcb.FirstOrDefault().CapBac + ". " + qcb.FirstOrDefault().TenCB;
                    }
                    else
                        colTenTKXN.Text = qcb.First().TenCB.ToUpper();
                }
            }
            int _mabn = String.IsNullOrEmpty(MaBNhan.Value.ToString()) ? 0 : Convert.ToInt32(MaBNhan.Value);
            var bn = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                      join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                      join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                      join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                      group new { dvct, clsct } by new { dvct.TenDVct, clsct.KetQua } into kq
                      select new { TenDVct = kq.Key.TenDVct, KetQua = kq.Key.KetQua }).ToList();
            if (bn.Count() > 0)
            {
                colKQMauChay.Text = bn.Where(p => p.TenDVct.Contains("chảy")).First().KetQua;
                colKQMauDong.Text = bn.Where(p => p.TenDVct.Contains("đông")).First().KetQua;
                colKQHeABO.Text = bn.Where(p => p.TenDVct.Contains("ABO")).First().KetQua;
                colKQHeRh.Text = bn.Where(p => p.TenDVct.Contains("Rh")).First().KetQua;
            }
        }
    }

}

