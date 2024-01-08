using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class DSBNNhanHTNgheo : DevExpress.XtraEditors.XtraForm
    {
        public DSBNNhanHTNgheo()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DateTime.Now;
            DateTime denngay = DateTime.Now;
            if(lupKhoaphong.EditValue != null)
            {
                if(radioGroup2.SelectedIndex == 0)
                {
                    tungay = GetFirstDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text));
                    denngay = GetLastDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text));
                }
                else
                {
                    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                }
                int makp = Convert.ToInt32(lupKhoaphong.EditValue);
                var ds1 = (from a in _data.BNKBs.Where(p => p.NgayKham < tungay)
                           join b in _data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt") || p.MaDTuong.ToLower().Contains("te")) on a.MaBNhan equals b.MaBNhan
                           group a by new { b } into kq
                           select new { kq.Key.b, IDKB = kq.Max(p => p.IDKB) }).ToList();
                var ds2 = (from a in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                           join b in _data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt") || p.MaDTuong.ToLower().Contains("te")) on a.MaBNhan equals b.MaBNhan
                           group new { a,b} by new { a,b } into kq
                           select new { kq.Key.b, kq.Key.a.IDKB }).ToList();
                var bnkb1 = (from a in ds1
                             join b in _data.BNKBs on a.IDKB equals b.IDKB
                             join c in _data.RaViens on a.b.MaBNhan equals c.MaBNhan into k
                             join d in _data.VaoViens on a.b.MaBNhan equals d.MaBNhan
                             from k1 in k.DefaultIfEmpty()
                             select new {a.b.MaDTuong, a.b.TenBNhan, a.b.MaBNhan, a.b.DTuong, a.b.DChi, a.b.SThe, b.MaKP, d.SoBA, d.NgayVao, NgayRa = k1 != null ? k1.NgayRa : null }).ToList();
                var bnkb2 = (from a in ds2
                             join b in _data.BNKBs on a.IDKB equals b.IDKB
                             join c in _data.RaViens on a.b.MaBNhan equals c.MaBNhan into k
                             join d in _data.VaoViens on a.b.MaBNhan equals d.MaBNhan
                             from k1 in k.DefaultIfEmpty()
                             select new {a.b.MaDTuong, a.b.TenBNhan, a.b.MaBNhan, a.b.DTuong, a.b.DChi, a.b.SThe, b.MaKP, d.SoBA, d.NgayVao, NgayRa = k1 != null ? k1.NgayRa : null }).ToList();
                var bn = bnkb1.Concat(bnkb2).Where(p => p.NgayRa == null || p.NgayRa >= tungay).ToList();
                var bn1 = (from a in bn
                      group a by new
                      {
                          a.MaDTuong,
                          a.TenBNhan,
                          a.MaBNhan,
                          a.NgayRa,
                          a.NgayVao,
                          a.SoBA,
                          a.SThe,
                          a.DChi,
                          a.DTuong,
                          a.MaKP
                      } into kq
                      select new
                      {
                          kq.Key.MaDTuong,
                          kq.Key.TenBNhan,
                          kq.Key.MaBNhan,
                          kq.Key.NgayRa,
                          kq.Key.NgayVao,
                          kq.Key.SoBA,
                          kq.Key.SThe,
                          kq.Key.DChi,
                          kq.Key.DTuong,
                          kq.Key.MaKP
                      }).ToList();
                var ds = (from a in bn1.Where(p => p.MaKP == makp)
                          select new {a.MaDTuong, a.TenBNhan, a.MaBNhan, a.NgayRa,a.NgayVao, a.SoBA,a.SThe,a.DChi,a.DTuong,a.MaKP }).ToList();
                if (radioGroup1.SelectedIndex == 0)
                {
                    BaoCao.BangChamCom rep = new BaoCao.BangChamCom();
                    rep.txtkhoa.Text = "Khoa: " + lupKhoaphong.Text;
                    rep.thang.Text = "Từ ngày " + tungay.Day + " / " + tungay.Month + " Đến " + denngay.Day + " / " + denngay.Month + " Năm " + denngay.Year;
                    rep.DataSource = ds.OrderBy(p => p.NgayVao).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    BaoCao.BC_DSBNNhanHTAn rep = new BaoCao.BC_DSBNNhanHTAn();
                    rep.txtkhoa.Text = "Khoa: " + lupKhoaphong.Text;
                    rep.thang.Text = "Từ ngày " + tungay.Day + " / " + tungay.Month + " Đến " + denngay.Day + " / " + denngay.Month + " Năm " + denngay.Year;
                    rep.DataSource = ds.OrderBy(p => p.NgayVao).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show("Chưa chọn khoa phòng!", "Thông báo");
            }
        }

        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        private void DSBNNhanHTNgheo_Load(object sender, EventArgs e)
        {
            var kp = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            lupKhoaphong.Properties.DataSource = kp.ToList();
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            radioGroup2.Enabled = false;
            dateTuNgay.Enabled = false;
            dateDenNgay.Enabled = false;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                radioGroup2.SelectedIndex = 0;
                radioGroup2.Enabled = false;
            }
            else
                radioGroup2.Enabled = true;
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radioGroup2.SelectedIndex == 1 )
            {
                dateTuNgay.Enabled = true;
                dateDenNgay.Enabled = true;
                comboBoxEdit1.Enabled = false;
            }
            else
            {
                dateTuNgay.Enabled = false;
                dateDenNgay.Enabled = false;
                comboBoxEdit1.Enabled = true;
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}