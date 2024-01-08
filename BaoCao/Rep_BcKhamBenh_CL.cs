using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKhamBenh_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKhamBenh_CL()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtMaKP.DataBindings.Add("Text", DataSource, "MaKP");
            colKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            colTS.DataBindings.Add("Text", DataSource, "TS");
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            colCapCuu.DataBindings.Add("Text",DataSource,"CapCuu");
            colVaoVien.DataBindings.Add("Text",DataSource,"VaoVien");
            //colCVNang.DataBindings.Add("Text",DataSource,"CVNang");
            //colCVXin.DataBindings.Add("Text", DataSource, "CVXin");
            colBNThuThuat.DataBindings.Add("Text", DataSource, "BNThuThuat");
            colBNDTNTSoNguoi.DataBindings.Add("Text", DataSource, "BNDTNTSoNguoi");
            colBNDTNTSoNgay.DataBindings.Add("Text", DataSource, "BNDTNTSoNgay");
            colTSLK.DataBindings.Add("Text", DataSource, "TS").FormatString=DungChung.Bien.FormatString[1];
            colBHYTTS.DataBindings.Add("Text", DataSource, "BHYT").FormatString=DungChung.Bien.FormatString[1];
            colCapCuuTS.DataBindings.Add("Text", DataSource, "CapCuu").FormatString=DungChung.Bien.FormatString[1];
            colVaoVienTS.DataBindings.Add("Text", DataSource, "VaoVien").FormatString=DungChung.Bien.FormatString[1];
            //colCVNangTS.DataBindings.Add("Text", DataSource, "CVNang");
            //colCVXinTS.DataBindings.Add("Text", DataSource, "CVXin");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            DateTime denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));

            var qcv = ((from bnkb in data.BNKBs.Where(p => p.PhuongAn == 2).Where(p=>p.NgayKham>=tungay&&p.NgayKham<=denngay)
                        join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                        join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                        join rv in data.RaViens on bnkb.MaBNhan equals rv.MaBNhan
                        group new { bnkb, bn, kp,rv } by new { kp.MaKP } into kq
                        select new
                        {
                            MaKP = kq.Key.MaKP,
                            CVNang = kq.Where(p => p.rv.LyDoC == "Đủ điều kiện chuyển tuyến(đúng tuyến)").Select(p => p.bnkb.MaBNhan).Count(),
                            CVXin = kq.Where(p => p.rv.LyDoC == "Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu người bệnh...(vượt tuyến)").Select(p => p.bnkb.MaBNhan).Count(),
                        }).ToList());
                        //.Select(p => new
                        //{
                        //    p.MaKP,
                        //    CVNang = p.CVNang.ToString() == "0" ? null : p.CVNang.ToString(),
                        //    CVXin = p.CVXin.ToString() == "0" ? null : p.CVXin.ToString(),
                        //}).ToList();
            if (qcv.Count() > 0)
            {
                if (qcv.Sum(p => p.CVNang) > 0)
                {
                    colCVNangTS.Text = qcv.Sum(p => p.CVNang).ToString();

                }
                else
                {
                    colCVNangTS.Text = "";
             
                }
                if (qcv.Sum(p => p.CVXin) > 0)
                {
                    colCVXinTS.Text = qcv.Sum(p => p.CVXin).ToString();
               
                }
                else
                {
                    colCVXinTS.Text = "";
            
                }
                if (this.GetCurrentColumnValue("MaKP") != null)
                {
                    int _makp= Convert.ToInt32( this.GetCurrentColumnValue("MaKP"));
                    if (qcv.Where(p => p.MaKP == _makp).Sum(p => p.CVNang) > 0)
                    {

                        colCVNang.Text = qcv.Where(p => p.MaKP == _makp).Sum(p => p.CVNang).ToString();
                     }
                    else
                    {
                        colCVNang.Text = "";
                     }
                    if (qcv.Where(p => p.MaKP == _makp).Sum(p => p.CVXin) > 0)
                    {

                        colCVXin.Text = qcv.Where(p => p.MaKP == _makp).Sum(p => p.CVXin).ToString();
                    }
                    else
                    {
                        colCVXin.Text = "";
                    }
                }
            }
        }
    }
}
