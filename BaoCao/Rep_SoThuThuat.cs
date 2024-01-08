using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoThuThuat : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoThuThuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            #region Mẫu cũ
            colKhoa.DataBindings.Add("Text", DataSource, "NoiGui");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            txtMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            txtNam.DataBindings.Add("Text", DataSource, "GTinh");
            //txtNu.DataBindings.Add("Text",DataSource,"GTinh");
            txtBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colCDTruocPT.DataBindings.Add("Text", DataSource, "ChanDoan");
            colNgayPT.DataBindings.Add("Text", DataSource, "NgayTH");
            colPPPT.DataBindings.Add("Text", DataSource, "YeuCau");
            txtBSPT.DataBindings.Add("Text", DataSource, "bsth");
            GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
            #endregion
            #region Mẫu mới
            colTenBNhan_A3_moi.DataBindings.Add("Text", DataSource, "TenBNhan");
            txt_GT_A3_moi.DataBindings.Add("Text", DataSource, "GTinh");
            txt_Tuoi_A3_moi.DataBindings.Add("Text", DataSource, "Tuoi");
            txt_BHYT_A3_moi.DataBindings.Add("Text", DataSource, "DTuong");
            colDiaChi_A3_moi.DataBindings.Add("Text", DataSource, "DChi");
            colCDTruocPT_A3_moi.DataBindings.Add("Text", DataSource, "ChanDoan");
            colSauPT_A3_moi.DataBindings.Add("Text", DataSource, "ChanDoanSau");
            colNgayPT_A3_moi.DataBindings.Add("Text", DataSource, "NgayBDTH");
            colNgayKT_A3_moi.DataBindings.Add("Text", DataSource, "NgayTH");
            txt_PPTT_A3_moi.DataBindings.Add("Text", DataSource, "MaDV");
            txt_BSCD_A3_moi.DataBindings.Add("Text", DataSource, "MaCB");
            txt_KTV_A3_moi.DataBindings.Add("Text", DataSource, "MaCBth");
            colPPVC_A3_moi.DataBindings.Add("Text", DataSource, "PPvocam");
            txt_Loaitt_A3_moi.DataBindings.Add("Text", DataSource, "PLoai");
            colLoaiPT_A3_moi.DataBindings.Add("Text", DataSource, "PhanLoaiTT");
            colGhiChu_A3_moi.DataBindings.Add("Text", DataSource, "GhiChu");
            #endregion
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            int _gt = 0;
            int _tuoi = 0;
            if (GetCurrentColumnValue("tuoi") != null)
            {
                _tuoi = int.Parse(this.GetCurrentColumnValue("tuoi").ToString());
                if (GetCurrentColumnValue("gtinh") != null)
                {
                    _gt = int.Parse(this.GetCurrentColumnValue("gtinh").ToString());
                    if (_gt == 1)
                    {
                        colNam.Text = _tuoi.ToString();
                        colNu.Text = "";
                    }
                    else { colNam.Text = " "; colNu.Text = _tuoi.ToString(); }
                }
            }
        }

        private void colNu_BeforePrint(object sender, CancelEventArgs e)
        {
            //int _gt = 0;
            //if (GetCurrentColumnValue("GTinh") != null)
            //{
            //    _gt = int.Parse(this.GetCurrentColumnValue("GTinh").ToString());
            //    if (_gt == 0)
            //    {
            //        colNu.Text = "X";
            //    }
            //    else colNu.Text = " ";
            //}
        }

        private void colBHYT_BeforePrint(object sender, CancelEventArgs e)
        {
            string dt = "";
            if (GetCurrentColumnValue("dtuong") != null)
            {
                dt = GetCurrentColumnValue("dtuong").ToString();
                if (dt == "BHYT")
                {
                    colBHYT.Text = "X";
                }
                else colBHYT.Text = " ";
            }
        }

        private void colBSPT_BeforePrint(object sender, CancelEventArgs e)
        {
            string _bsth = "";
            if (this.GetCurrentColumnValue("bsth") != null && this.GetCurrentColumnValue("bsth") != "")
            {
                _bsth = GetCurrentColumnValue("bsth").ToString();
                var qbsth = from cb in data.CanBoes.Where(p => p.MaCB == _bsth) select new { cb.TenCB };
                if (qbsth.Count() > 0)
                {
                    colBSPT.Text = qbsth.First().TenCB;
                }
                else colBSPT.Text = "";
            }
        }

        private void colCDTruocPT_BeforePrint(object sender, CancelEventArgs e)
        {
            //int  _mabn = 0;
            //if (this.GetCurrentColumnValue("MaBNhan") != null)
            //{
            //    _mabn = GetCurrentColumnValue("MaBNhan").ToString();
            //    var q = (from bnkb in data.BNKBs.Where(p => p.MaBNhan == _mabn)
            //             join kp in data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng")) on bnkb.MaKP equals kp.MaKP
            //             select new { bnkb.ChanDoan }).ToList();
            //    if (q.Count > 0)
            //    {
            //        colCDTruocPT.Text = q.First().ChanDoan;
            //    }
            //    else colCDTruocPT.Text = "";
            //}
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void colNam_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {

            if (txt_GT_A3_moi.Text == "1")
            {
                colNam_A3_moi.Text = txt_Tuoi_A3_moi.Text;
            }
            else
            {
                colNam_A3_moi.Text = "";
            }
        }

        private void colNu_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txt_GT_A3_moi.Text == "0")
            {
                colNu_A3_moi.Text = txt_Tuoi_A3_moi.Text;
            }
            else
            {
                colNu_A3_moi.Text = "";
            }
        }

        private void colBHYT_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txt_BHYT_A3_moi.Text == "BHYT")
            {
                colBHYT_A3_moi.Text = "X";
            }
            else
            {
                colBHYT_A3_moi.Text = "";
            }
        }
        private void colPPPT_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = Convert.ToInt32(txt_PPTT_A3_moi.Text);
            var pptt = data.DichVus.FirstOrDefault(p => p.MaDV == _madv);
            if (pptt != null)
                colPPPT_A3_moi.Text = pptt.TenDV;
        }

        private void colLoaiPT_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void colBSPT_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.FirstOrDefault(p => p.MaCB == txt_BSCD_A3_moi.Text);
            if (tencb != null)
                colBSPT_A3_moi.Text = tencb.TenCB;
        }

        private void colBSGM_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            var tencb = data.CanBoes.FirstOrDefault(p => p.MaCB == txt_KTV_A3_moi.Text);
            if (tencb != null)
                colBSGM_A3_moi.Text = tencb.TenCB;
        }

        private void colNgayPT_A3_moi_BeforePrint(object sender, CancelEventArgs e)
        {
             
        }

        private void sub_detail_moi_BeforePrint(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(colNgayPT_A3_moi.Text))
            {
                colNgayPT_A3_moi.Text = string.Format("HH:mm dd/MM/yyyy");
            }
        }
    }
}
