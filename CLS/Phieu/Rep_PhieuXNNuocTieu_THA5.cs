using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu_THA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu_THA5()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG.Contains("Nước tiểu")) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.KetQua, clsct.STTHT, dvct.TenDVct }).ToList();
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ1.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ2.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ3.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ4.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ5.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BIL")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ6.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ7.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ8.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ9.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("KET")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("KET")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ10.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ11.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ12.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ13.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ14.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).Count() > 0)
                    {
                        colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ15.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("SG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("SG")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ16.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ17.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ18.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ19.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ20.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("BLD")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BLD")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ21.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ22.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ23.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ24.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ25.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("PH")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PH")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ26.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ27.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ28.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PRO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ29.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ30.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ31.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ32.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ33.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("UBG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("UBG")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ34.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ35.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ36.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ37.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ38.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("NIT")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ39.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ40.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("LEU")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ41.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ42.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ43.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ44.Text = " ";
                }
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            string _bs = MaCBDT.Value.ToString();

            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB == _bs)
                        select new { bs.TenCB }).ToList();
            if (bsdt.Count > 0)
            {
                colBSDT.Text = bsdt.First().TenCB;
            }
            var bsxn = (from bs in DataContect.CanBoes.Where(p => p.MaCB == Macb.Value)
                        select new { bs.TenCB }).ToList();
            if (bsxn.Count > 0)
            {
                colTKXN.Text = bsxn.First().TenCB;
            }
        }

    }
}
