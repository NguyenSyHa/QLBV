using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuKTraBenhAn : DevExpress.XtraReports.UI.XtraReport
    {
        int _mabn = 0;
        public Rep_PhieuKTraBenhAn(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtMaBN.Text = "Mã bệnh nhân: " + _mabn.ToString();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            BenhNhan ttbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            int phuongan = data.BNKBs.Where(p => p.MaBNhan == _mabn && p.PhuongAn == 2).Count();

            if(ttbn!=null)
            {
                txtTenBN.Text = ttbn.TenBNhan.ToUpper();
                txtNgaySinh.Text = (ttbn.NgaySinh != null && ttbn.ThangSinh != null && ttbn.NamSinh != null) ? (ttbn.NgaySinh + "/" + ttbn.ThangSinh + "/" + ttbn.NamSinh) : ttbn.NamSinh.ToString();
                if (ttbn.GTinh == 1)
                    txtNam.Text = "X";
                else
                    txtNu.Text = "X";
                txtDiaChi.Text = ttbn.DChi;
                KPhong kp = data.KPhongs.Where(p => p.MaKP == ttbn.MaKP).FirstOrDefault();
                txtKhoa.Text = kp != null ? kp.TenKP : "Khoa:..................";
            }
            RaVien rv = data.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (rv != null)
            {
                txtNgayVV.Text = rv.NgayVao.Value.Day + "/" + rv.NgayVao.Value.Month + "/" + rv.NgayVao.Value.Year + "  " + rv.NgayVao.Value.Hour + ":" + rv.NgayVao.Value.Minute;
                txtNgayRV.Text = rv.NgayRa.Value.Day + "/" + rv.NgayRa.Value.Month + "/" + rv.NgayRa.Value.Year + "  " + rv.NgayRa.Value.Hour + ":" + rv.NgayRa.Value.Minute;
                string[] _arrcd = rv.ChanDoan.Split(';');
                string[] _arricd = rv.MaICD.Split(';');
                txtBenhChinh.Text = _arrcd[0];
                txtMaBenhC.Text = _arricd[0];
                txtBenhPhu.Text = DungChung.Ham.FreshString(rv.ChanDoan.Replace(_arrcd[0], ";"));
                txtMaICDPhu.Text = DungChung.Ham.FreshString(rv.MaICD.Replace(_arricd[0], ";"));
                txtSoNgayDT.Text = rv.SoNgaydt.ToString();
               
                    if (phuongan > 0)
                    {
                        txtTinhTrangRV.Text = rv.TinhTrangC;
                    }
                    else
                        txtTinhTrangRV.Text = rv.KetQua;
            }
        }
    }
}
