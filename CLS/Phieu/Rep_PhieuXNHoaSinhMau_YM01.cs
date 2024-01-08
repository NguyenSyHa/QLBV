using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNHoaSinhMau_YM01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNHoaSinhMau_YM01()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
            //colTSBTn.DataBindings.Add("Text", DataSource, "TSBT");
            colKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            //colTSTBNu.DataBindings.Add("Text", DataSource, "TSBTnu");
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string _macbdt = BSCD.Value.ToString();
            string _macbth = MaBSTH.Value.ToString();
             var qcb = (from bs in _data.CanBoes.Where(p => p.MaCB == _macbdt) select new { bs.TenCB }).ToList();
            if (qcb.Count > 0)
            {
                colBSCD.Text = qcb.First().TenCB;
            }
            var qk = (from cb in _data.CanBoes.Where(p => p.MaCB == _macbth) select new { cb.TenCB }).ToList();
            if (qk.Count > 0)
            {
                colTenTKXN.Text = qk.First().TenCB;
            }
            
        }

        private void colKetQua_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("KetQua") != null)
            {
                double ketqua;
                String kq = GetCurrentColumnValue("KetQua").ToString();
                string maDVct = "";
                if (GetCurrentColumnValue("MaDVct") != null)
                    maDVct = GetCurrentColumnValue("MaDVct").ToString();
                List<String> TenRG = new List<String>() { "XN hóa sinh máu", "XN nước tiểu", "XN huyết học", "XN khác" };
                if (!double.TryParse(GetCurrentColumnValue("KetQua").ToString(), out ketqua))
                {
                    colKetQua.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    colKetQua.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Regular);
                }
                else
                {
                    if (DungChung.Ham.kiemtraKQ(_data, String.IsNullOrEmpty(paramMaBN.Value.ToString()) ? 0 : Convert.ToInt32(paramMaBN.Value), maDVct, 1, ketqua, TenRG) == "right")
                    {
                        colKetQua.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        colKetQua.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Bold);
                    }
                    else if (DungChung.Ham.kiemtraKQ(_data, String.IsNullOrEmpty(paramMaBN.Value.ToString()) ? 0 : Convert.ToInt32(paramMaBN.Value), maDVct, 1, ketqua, TenRG) == "left")
                    {
                        colKetQua.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        colKetQua.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Bold);
                    }
                    else
                    {

                        colKetQua.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        colKetQua.Font = new Font(new FontFamily("Times New Roman"), 12, FontStyle.Regular);
                    }
                }
            }
        }

        private void xrTable4_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("MaDVct") != null)
            {
                string madvct = GetCurrentColumnValue("MaDVct").ToString();
                DichVuct dvct = _data.DichVucts.Where(p => p.MaDVct == madvct).First();
                string[] kq = DungChung.Ham.layTSBT(_data, dvct.MaDVct, null, 1);
                colTSBTn.Text = kq[3].ToLower().Replace("nam", "").ToString();
                colTSTBNu.Text = kq[4].ToLower().Replace("nữ", "").ToString();
            }
        }
    }
}
