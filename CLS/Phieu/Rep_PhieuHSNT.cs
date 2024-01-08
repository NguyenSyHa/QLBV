using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuHSNT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuHSNT()
        {
            InitializeComponent();
        }

        private int Tongso = 0;
        private QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG.Contains("XN hóa sinh nội tiết tố"))
                        select new { tnhomdv.TenTN, dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    xrLabel9.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    xrTableCell8.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    xrLabel10.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    xrTableCell14.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    xrLabel11.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    xrTableCell20.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    xrLabel12.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    xrTableCell11.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    xrLabel13.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    xrTableCell17.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    xrLabel14.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    xrTableCell23.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
            }
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dichvu in DataContect.DichVus on dvct.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT, cls.NgayTH, cls.NgayThang, cls.Status }).ToList();
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null)
                {
                    xrLabel18.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    xrTableCell9.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null)
                {
                    xrLabel19.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    xrTableCell15.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null)
                {
                    xrLabel22.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    xrTableCell21.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null)
                {
                    xrLabel17.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    xrTableCell12.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null)
                {
                    xrLabel20.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    xrTableCell18.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null)
                {
                    xrLabel21.Text = "X";
                    Tongso++;
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    xrTableCell24.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                }
                xrTableCell25.Text = DungChung.Ham.NgaySangChu(qhh.First().NgayThang.Value);
                if (qhh.First().NgayTH != null)
                    xrTableCell27.Text = DungChung.Ham.NgaySangChu(qhh.First().NgayTH.Value);
            }

            #region check kq null
            //if (qhh.Count > 0)
            //{
            //    if (qhh.Where(p => p.STT == 1).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell9.Text = "";
            //    }
            //    if (qhh.Where(p => p.STT == 2).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell15.Text = "";
            //    }
            //    if (qhh.Where(p => p.STT == 3).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell21.Text = "";
            //    }
            //    if (qhh.Where(p => p.STT == 4).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell12.Text = "";
            //    }
            //    if (qhh.Where(p => p.STT == 5).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell18.Text = "";
            //    }
            //    if (qhh.Where(p => p.STT == 6).FirstOrDefault().Status == 0)
            //    {
            //        xrTableCell24.Text = "";
            //    }
            //}
            #endregion
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel25.Visible = false;
                xrLabel26.Visible = false;
            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colSo.Text = SoPhieu.Value.ToString();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                lab2.Visible = false;
            if (DungChung.Bien.MaBV == "27001")
            {
                xrTableCell31.Text = "Y BÁC SỸ";
                xrTableCell33.Text = "PHÒNG XÉT NGHIỆM";
            }
            if (DungChung.Bien.MaBV == "27777")
            {
                picBoxLogo.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                picBoxLogo.Visible = false;
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                picBoxLogo.Visible = false;
                xrPictureBox2.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            labTongSoLuot.Text = "Số lượng XN: " + Tongso;
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = BSDT.Value.ToString();
            repsub.TKXN.Value = TKXN.Value.ToString();
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void Rep_PhieuHSNT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }
    }
}