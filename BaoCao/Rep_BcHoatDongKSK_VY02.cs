using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKSK_VY02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKSK_VY02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));                                                    
            {
                var qksk = (from bn in data.BenhNhans
                            join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan 
                            where (bn.NNhap >= tungay && bn.NNhap <= denngay && bn.DTuong== ("KSK"))
                            select new
                            {
                                bn.MaBNhan,
                                bn.Tuoi,
                                bn.NNhap,
                                bn.DChi,
                                bn.TChung,
                                bn.GTinh,
                                tu.KetLuan 
                            }).ToList();
                //if (qksk.Count() > 0)
                //{
                //    int a = qksk.Select(p => p.MaBNhan).Count();

                //    colTS11.Text = a.ToString("##,###");
                //    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                //    {
                //        int b = qksk.Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                //        colSLa11.Text = b.ToString("##,###");
                //    }
                //    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                //    {
                //        int b = qksk.Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                //        colSLb11.Text = b.ToString("##,###");
                //    }
                //    if (qksk.Where(p => p.GTinh == 1).Count() > 0)
                //    {
                //        int b = qksk.Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();

                //        colGTa11.Text = b.ToString("##,###");
                //    }
                //    if (qksk.Where(p => p.GTinh == 0).Count() > 0)
                //    {
                //        int b = qksk.Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                //        colGTb11.Text = b.ToString("##,###");
                //    }
                //}
                int ts = 0, dusk = 0, kdu = 0, nam = 0, nu = 0;
                if (qksk.Where(p => p.TChung =="Khám tuyển").Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Khám tuyển")).Select(p => p.MaBNhan).Count();
                    ts = ts + a;
                    colTS1.Text = a.ToString("##,###");
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa1.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb1.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa1.Text = b.ToString("##,###");
                        nam = nam + b;
                    }
                    
                    if (qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb1.Text = b.ToString("##,###");
                        nu = nu + b;

                    }
                }
                if (qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Select(p => p.MaBNhan).Count();
                    colTS2.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)

                    {
                        int b = qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa2.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb2.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    } 
                    if (qksk.Where(p => p.TChung.Contains("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa2.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Người lao động, đi học(trên 18 tuổi)")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb2.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }
                if (qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Select(p => p.MaBNhan).Count();
                    colTS3.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa3.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb3.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa3.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Học sinh, sinh viên( dưới 18 tuổi )")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb3.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }
                if (qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Select(p => p.MaBNhan).Count();
                    colTS4.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa4.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb4.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa4.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb4.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }
                if (qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Select(p => p.MaBNhan).Count();
                    colTS5.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa5.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb5.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa5.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Cấp bằng lái xe máy") || p.TChung.Contains("Cấp bằng ô tô")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb5.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }

                if (qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Select(p => p.MaBNhan).Count();
                    colTS6.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa6.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb6.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa6.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Contains("Đổi bằng lái xe máy") || p.TChung.Contains("Đổi bằng ô tô")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb6.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }
                if (qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Select(p => p.MaBNhan).Count();
                    colTS8.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa8.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb8.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Contains("Khám tuyển sinh")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa8.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Contains("Khám tuyển sinh")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khám tuyển sinh")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb8.Text = b.ToString("##,###");
                        nu = nu + b;
                    }
                }
                if (qksk.Where(p => p.TChung.Equals("Khác")).Count() > 0)
                {
                    int a = qksk.Where(p => p.TChung.Equals("Khác")).Select(p => p.MaBNhan).Count();
                    colTS7.Text = a.ToString("##,###");
                    ts = ts + a;
                    if (qksk.Where(p => p.KetLuan == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                        colSLa7.Text = b.ToString("##,###");
                        dusk = dusk + b;
                    }
                    if (qksk.Where(p => p.KetLuan == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                        colSLb7.Text = b.ToString("##,###");
                        kdu = kdu + b;
                    }
                    if (qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.GTinh == 1).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                        colGTa7.Text = b.ToString("##,###");
                        nam = nam + b;
                    }

                    if (qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.GTinh == 0).Count() > 0)
                    {
                        int b = qksk.Where(p => p.TChung.Equals("Khác")).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                        colGTb7.Text = b.ToString("##,###");
                        nu = nu + b;
                    }

                }
                colTS11.Text = ts.ToString();
                colSLa11.Text = dusk.ToString();
                colSLb11.Text = kdu.ToString();
                colGTa11.Text = nam.ToString();
                colGTb11.Text = nu.ToString();
            }

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = ("tổ gđyk - "+ DungChung.Bien.TenCQ).ToUpper();
            if (DungChung.Bien.MaBV == "24009")
                lblngaythang.Text = "Việt Yên, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            else
                lblngaythang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
