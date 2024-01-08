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
    public partial class frm_BCDSTEDuoi6TuoiKCBKHongSDThe : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDSTEDuoi6TuoiKCBKHongSDThe()
        {
            InitializeComponent();
        }

        private void frm_BCDSTEDuoi6TuoiKCBKHongSDThe_Load(object sender, EventArgs e)
        {
            lupngaytu.DateTime = DateTime.Now;
            lupdenngay.DateTime = DateTime.Now;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnin_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupngaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupdenngay.DateTime);
            List<TTboXung> tt = new List<TTboXung>();
            foreach(var item in data.TTboXungs)
            {
                TTboXung moi = new TTboXung();
                moi.MaBNhan = item.MaBNhan;
                if(item.NThan != null)
                {
                    string[] ar = item.NThan.Split(';');
                    moi.NThan = ar[0];
                }
                else
                {
                    moi.NThan = "";
                }
                moi.HTThanhToan = 0;
                tt.Add(moi);
            }
            var bn = (from b in tt

                      join a in data.BenhNhans.Where(p => p.Tuoi < 6).Where(p => p.SThe.Contains("KT") || p.DTuong == "Dịch vụ") on b.MaBNhan equals a.MaBNhan 
                      select new { 
                          a.MaBNhan,
                          a.TenBNhan,
                          Ngaysinh = (a.NgaySinh + "/" + a.ThangSinh + "/" + a.NamSinh),
                          Nam = a.GTinh == 1 ? "X" : "",
                          Nu = a.GTinh == 0 ? "X" : "",
                          a.DChi,
                          NguoiGiamHo = b.NThan,
                          a.NNhap
                      }).ToList();
            BaoCao.rep_DSTreKhamChuaKThe rep = new BaoCao.rep_DSTreKhamChuaKThe();
            frmIn frm = new frmIn();
                var vp = (from a in data.VienPhis
                              join b in data.VienPhicts on a.idVPhi equals b.idVPhi
                              group new {a,b} by new {a.MaBNhan} into kq
                              select new {kq.Key.MaBNhan , ThanhTien = kq.Sum(p => p.b.ThanhTien)}).ToList();
              var ds1 = (from a in bn.Where(p => grngaytim.SelectedIndex == 0 ? (p.NNhap >= tungay && p.NNhap <= denngay) : true)
                       join c in vp on a.MaBNhan equals c.MaBNhan into k
                       from k1 in k.DefaultIfEmpty()
                         join b in data.RaViens.Where(p => grngaytim.SelectedIndex == 2 ? (p.NgayRa >= tungay && p.NgayRa <= denngay) : true) on a.MaBNhan equals b.MaBNhan
                       select new
                       {
                           a.MaBNhan,
                           a.TenBNhan,
                           a.Nam,
                           a.Nu,
                           a.Ngaysinh,
                           a.DChi,
                           a.NguoiGiamHo,
                           a.NNhap,
                           Ngay = grngaytim.SelectedIndex == 0 ? a.NNhap.Value.Date : ( b.NgayRa.Value.Date),
                           Tong = k1 != null ? k1.ThanhTien : 0.0
                       }).ToList();
              if (grngaytim.SelectedIndex == 1)
                  ds1 = (from a in ds1
                         join b in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay) on a.MaBNhan equals b.MaBNhan
                         select new {
                             a.MaBNhan,
                             a.TenBNhan,
                             a.Nam,
                             a.Nu,
                             a.Ngaysinh,
                             a.DChi,
                             a.NguoiGiamHo,
                             a.NNhap,
                             Ngay = b.NgayVao.Value.Date,
                             a.Tong
                         }).ToList();
            rep.NgayTim.Value = "Từ " + tungay.Day + "/" + tungay.Month + "/" + tungay.Year + "/" + " Đến " + denngay.Day + "/" + denngay.Month + "/" + denngay.Year;
            rep.ngaythang.Value = DungChung.Bien.DiaDanh + ", Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.DataSource = ds1;
            rep.BindingData(); 
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}