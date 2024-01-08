using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_ThVPTUTTT_BG02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThVPTUTTT_BG02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        private void Frm_ThVPTUTTT_BG02_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }
        public class SoTienTU
        {
            private DateTime NTN;
            private double DuDauKy;
            private double TamUng;
            private double ThanhToan;
            private double DuCuoiKy;
            public DateTime ntn
            {
                set { NTN = value; }
                get { return NTN; }
            }
            public double dudauky
            {
                set { DuDauKy=value;} get{return DuDauKy;}
            }
            public double tamung
            {
                set { TamUng = value; }
                get { return TamUng; }
            }
            public double thanhtoan
            {
                set { ThanhToan = value; }
                get { return ThanhToan; }
            }
            public double ducuoiky
            {
                set { DuCuoiKy = value; }
                get { return DuCuoiKy; }
            }
        }
        List<SoTienTU> _SoTienTU = new List<SoTienTU>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            _SoTienTU.Clear();
            if (kt())
            {
                DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                DateTime NDK = DungChung.Ham.NgayTu(NgayDK.DateTime);

                BaoCao.Rep_ThVPTUTT_BG02 rep = new BaoCao.Rep_ThVPTUTT_BG02();
                rep.TuNgay.Value = lupNgaytu.Text;
                rep.DenNgay.Value = lupNgayden.Text;
                rep.TuNgayDenNgay.Value = " Từ ngày " + lupNgaytu.Text.Substring(0, 10) + " đến ngày " + lupNgayden.Text.Substring(0, 10);
                rep.DuCuoiThang.Value = "Dư cuối tháng (số tiền tồn tạm ứng chưa thanh toán hết ngày " + ngaytu.ToString().Substring(0, 5) + ") ";

                var qtu = (from  tu in dataContext.TamUngs.Where(p=>p.PhanLoai==0)
                           where (tu.NgayThu >= ngaytu && tu.NgayThu <= ngayden)
                           //where !(from vp in dataContext.VienPhis select vp.MaBNhan).Contains(tu.MaBNhan)
                           group new { tu } by new { tu.NgayThu } into kq
                           select new
                           {
                               NgayTT = kq.Key.NgayThu,
                               TienTU = kq.Sum(p=>p.tu.SoTien),
                               //TongTien = kq.Sum(p => p.vpct.ThanhTien),
                           }).OrderByDescending(p => p.NgayTT).ToList();
                if (qtu.Count > 0)
                {
                    foreach (var a in qtu)
                    {
                        SoTienTU themmoi = new SoTienTU();
                        themmoi.ntn = a.NgayTT.Value;
                        themmoi.tamung = Convert.ToInt32(a.TienTU);
                        //themmoi.thanhtoan = Convert.ToInt32(a.TongTien);
                        _SoTienTU.Add(themmoi);
                    }
                }
                var qtu1 = (from tu in dataContext.TamUngs.Where(p => p.PhanLoai == 0)
                           where (tu.NgayThu >= ngaytu && tu.NgayThu <= ngayden)
                            where (from vp in dataContext.VienPhis select vp.MaBNhan).Contains(tu.MaBNhan)
                           group new { tu } by new { tu.NgayThu } into kq
                           select new
                           {
                               NgayTT = kq.Key.NgayThu,
                               TTTienTU = kq.Sum(p => p.tu.SoTien),
                               //TongTien = kq.Sum(p => p.vpct.ThanhTien),
                           }).OrderByDescending(p => p.NgayTT).ToList();
                if (qtu1.Count > 0)
                {
                    foreach (var a in qtu1)
                    {
                        SoTienTU themmoi = new SoTienTU();
                        themmoi.ntn = a.NgayTT.Value;
                        themmoi.thanhtoan = Convert.ToInt32(a.TTTienTU);
                        //themmoi.thanhtoan = Convert.ToInt32(a.TongTien);
                        _SoTienTU.Add(themmoi);
                    }
                }


                //var qtt = (from vp in dataContext.VienPhis
                //           join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                //           where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                //           group new { vp, vpct } by new { vp.NgayTT, vpct.ThanhTien } into kq
                //           select new
                //           {
                //               NgayTT = kq.Key.NgayTT,
                //               TienTT = kq.Sum(p => p.vpct.ThanhTien)
                //               //      TongTien = kq.Sum(p => p.vpct.ThanhTien),
                //           }).OrderByDescending(p => p.NgayTT).ToList();
                //if (qtt.Count > 0)
                //{
                //    foreach (var n in _SoTienTU)
                //    {
                //        foreach (var m in qtt)
                //        {
                //            if (n.ntn == m.NgayTT.Value)
                //            {
                //                if (m.TienTT != null && m.TienTT != 0)
                //                {
                //                    n.thanhtoan = m.TienTT.Value;

                //                }
                //            }

                //        }
                //    }
                //}
                double dudk = 0;
                double dudk1 = 0;
                double duck = 0;
                var qt1 = (from tu in dataContext.TamUngs.Where(p=>p.PhanLoai==0)
                           where (tu.NgayThu >= NDK && tu.NgayThu < ngaytu)
                           select new { PL=tu.PhanLoai,TienTU = tu.SoTien, ma=tu.MaBNhan }).ToList();
                var qt2 = (from tu in dataContext.TamUngs.Where(p => p.PhanLoai == 0)
                           where (from vp in dataContext.VienPhis select vp.MaBNhan).Contains(tu.MaBNhan)
                           where (tu.NgayThu >= NDK && tu.NgayThu <= ngaytu)
                           select new { PL = tu.PhanLoai, TienTU = tu.SoTien }).ToList();
                if (qt1.Count > 0)
                {
                    //if (qt1.Where(p => p.NgayTU < ngaytu).Sum(p => p.TienTU) != null)
                    dudk = qt1.Where(p => p.PL == 0).Sum(p => p.TienTU).Value - qt2.Where(p => p.PL == 0).Sum(p => p.TienTU).Value;
                        //dudk1 = qt2.Where(p => p.PL == 0).Sum(p => p.TienTU).Value;
                    //if (qt2.Where(p => p.NgayTT < ngaytu).Sum(p => p.TienTT)!=null)
                    // dudk-=    qt2.Where(p => p.NgayTT < ngaytu).Sum(p => p.TienTT).Value;
                   // if (qt1.Where(p => p.NgayTU <= ngayden).Sum(p => p.TienTU) != null)
                   //     duck = qt1.Where(p => p.NgayTU <= ngayden).Sum(p => p.TienTU).Value;
                   // if (qt2.Where(p => p.NgayTT <= ngayden).Sum(p => p.TienTT)!=null)
                   //duck-= qt2.Where(p => p.NgayTT <= ngayden).Sum(p => p.TienTT).Value;
       
                }
                //if(dudk> 0)
                //{
                rep.DuDK.Value = dudk;
                rep.DuDKText.Value="Dư đầu tháng (số tiền tồn tạm ứng hết ngày "+ngaytu.ToString().Substring(0,5) + "):     "+ dudk.ToString("0,0");
                //}else rep.DuDK.Value = "Dư đầu tháng (số tiền tồn tạm ứng hết ngày "+ngaytu.ToString().Substring(0,5) + "): ";
                //if (duck >= 0)
                //{
                    rep.DuCK.Value = duck;
                //}
                //else rep.DuCK.Value = " ";
                //foreach (var t in _SoTienTU);
                //{
                //    for (int i = 0; i < _SoTienTU.Count; i++)
                //    {
                       
                //    }
                //}

                rep.DataSource = _SoTienTU.OrderBy(p => p.ntn);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            } 
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}