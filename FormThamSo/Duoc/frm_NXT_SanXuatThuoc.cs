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
    public partial class frm_NXT_SanXuatThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_NXT_SanXuatThuoc()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_NXT_SanXuatThuoc_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            cklKP.DataSource = lkp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
            }

            List<MyObject> lPloaiNhap = new List<MyObject>();
            lPloaiNhap.Add(new MyObject { Value = -1, Text = "Tất cả" });
            lPloaiNhap.Add(new MyObject { Value = 0, Text = "Nhập từ kho khác"});
            lPloaiNhap.Add(new MyObject { Value = 1, Text = "Nhập theo hóa đơn" });
            lPloaiNhap.Add(new MyObject { Value = 2, Text = "Nhập trả dược" });
            lPloaiNhap.Add(new MyObject { Value = 3, Text = "Nhập tồn" });
            lPloaiNhap.Add(new MyObject { Value = 4, Text = "Nhập sản xuất" });

            List<MyObject> lPloaiXuat = new List<MyObject>();
            lPloaiXuat.Add(new MyObject { Value = -1, Text = "Tất cả" });
            lPloaiXuat.Add(new MyObject { Value = 0, Text = "Xuất ngoại trú" });
            lPloaiXuat.Add(new MyObject { Value = 1, Text = "Xuất nội trú" });
            lPloaiXuat.Add(new MyObject { Value = 2, Text = "Xuất nội bộ" });
            lPloaiXuat.Add(new MyObject { Value = 3, Text = "Xuất ngoài bệnh viện" });
            lPloaiXuat.Add(new MyObject { Value = 4, Text = "Xuất nhân dân" });
            lPloaiXuat.Add(new MyObject { Value = 5, Text = "Xuất cận lâm sàng" });
            lPloaiXuat.Add(new MyObject { Value = 6, Text = "Xuất tủ trực" });
            lPloaiXuat.Add(new MyObject { Value = 7, Text = "Xuất phòng khám" });
            lPloaiXuat.Add(new MyObject { Value = 8, Text = "Xuất kiểm nghiệm" });
            lPloaiXuat.Add(new MyObject { Value = 9, Text = "Xuất khác" });
            lPloaiXuat.Add(new MyObject { Value = 10, Text = "Xuất sản xuất" });
            lPloaiXuat.Add(new MyObject { Value = 11, Text = "Hư hao" });

            cklPLNhap.DataSource = lPloaiNhap;
            cklPLNhap.DisplayMember = "Text";
            cklPLNhap.ValueMember = "Value";

            cklPLXuat.DataSource = lPloaiXuat;
            cklPLXuat.DisplayMember = "Text";
            cklPLXuat.ValueMember = "Value";
            if (DungChung.Bien.MaBV == "27021")
            {               
                    cklPLNhap.SetItemChecked(0, true);
                    cklPLNhap.SetItemChecked(1, true);                   
                    cklPLXuat.SetItemChecked(2, true);
            }
            else
            {
                cklPLNhap.CheckAll();
                cklPLXuat.CheckAll();
            }

           // cklKP.CheckAll();
        }
        private void loadBBKK()
        {
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            List<int> lMaKP = new List<int>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
            }
            var qKK = (from nd in data.NhapDs.Where(p => p.PLoai == 4).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                       join kp in lMaKP on nd.MaKP equals kp
                       select nd).OrderByDescending(p => p.NgayNhap).ToList();
            lupBBKiemKe.Properties.DataSource = qKK;
        }
        public  class NXT
        {
            public byte? STT { get; set; }

            public string TenTN { get; set; }

            public string MaCC { get; set; }

            public string TenRG { get; set; }

            public string MaTam { get; set; }

            public string TenDV { get; set; }

            public int? MaDV { get; set; }
            public int? IDNhom { get; set; }

            public string DonVi { get; set; }

            public double TonDKSL { get; set; }

            public double TonDKTT { get; set; }

            public double NhapTKSL { get; set; }

            public double NhapTKTT { get; set; }

            public double NhapKhac_SL { get; set; }

            public double NhapKhac_TT { get; set; }

            public double TongXuatTKSL { get; set; }

            public double TongXuatTKTT { get; set; }

            public double XuatKhac_SL { get; set; }

            public double XuatKhac_TT { get; set; }

            public double TonCKSL { get; set; }

            public double TonCKTT { get; set; }

            public double? ConKK_TT { get; set; }

            public double? ConKK_SL { get; set; }

            public double? Thua_SL { get; set; }

            public double? Thua_TT { get; set; }

            public double? Thieu_SL { get; set; }

            public double? Thieu_TT { get; set; }
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            List<int> lMaKP = new List<int>();
            string kho = "Kho: ";
            int count = 1;
            List<string> lkho = new List<string>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                    lkho.Add(cklKP.GetItemText(i).Trim().ToLower());
                    if (count == 1)
                        kho = kho + cklKP.GetItemText(i);
                    else
                        kho = kho + ", " + cklKP.GetItemText(i);
                    count++;
                }
            }

            List<int> lPLoaiNhap = new List<int>();
            List<int> lPloaiXuat = new List<int>();
            for (int i = 0; i < cklPLNhap.ItemCount; i++)
            {
                if (cklPLNhap.GetItemChecked(i))
                {
                    lPLoaiNhap.Add(Convert.ToInt32(cklPLNhap.GetItemValue(i)));                   
                }
            }

            for (int i = 0; i < 12; i++)
            {
                if (cklPLXuat.GetItemChecked(i))
                {
                    lPloaiXuat.Add(Convert.ToInt32(cklPLXuat.GetItemValue(i)));
                }
            }

            bool huhao = false;
            if (cklPLXuat.GetItemChecked(12))
                huhao = true;
            var qnxt2 = (from nhapd in data.NhapDs
                         join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                         where ((nhapd.PLoai == 1 && lPLoaiNhap.Contains(nhapd.KieuDon?? -1)) || (nhapd.PLoai == 2 && lPloaiXuat.Contains(nhapd.KieuDon ?? -1)) || (nhapd.PLoai == 3 && huhao))
                         select new { nhapd.TraDuoc_KieuDon, nhapd.XuatTD, nhapd.MaKP, nhapdct.MaDV, nhapdct.IDDTBN, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia,
                                      SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                      SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0,
                                      ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0,
                                      ThanhTienX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct
                                      .ThanhTienX : 0,
                         }).ToList();
            var dichvu = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          select new { dv.MaDV, dv.TenDV, tn.TenTN, tn.TenRG, tn.STT, dv.MaCC, dv.DonVi, dv.MaTam, tn.IDNhom }).ToList();
            List<NXT> BC = new List<NXT>();
            BC = (from a in qnxt2
                  join dv in dichvu on a.MaDV equals dv.MaDV
                  join kp in lMaKP on a.MaKP equals kp
                  group a by new { dv.STT, dv.MaTam, dv.MaCC, dv.TenTN, dv.TenRG, dv.TenDV, dv.DonVi, a.MaDV, dv.IDNhom } into kq
                  select new NXT
                  {
                      STT = kq.Key.STT,
                      TenTN = kq.Key.TenTN,
                      TenRG = kq.Key.TenRG,
                      MaCC = kq.Key.MaCC,
                      MaTam = kq.Key.MaTam,
                      IDNhom = kq.Key.IDNhom,
                      MaDV = kq.Key.MaDV,
                      TenDV = kq.Key.TenDV,
                      DonVi = kq.Key.DonVi,
                      TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                      TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                      // nhập trong kỳ (cột 6,7)
                      NhapTKSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 1).Where(p => p.KieuDon != 2 && p.KieuDon != 4).Sum(p => p.SoLuongN),  // nhập trong kỳ = tổng nhập - nhập trả (phần không phải trả dược cho sản xuất) - nhập sản xuất
                      NhapTKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 1).Where(p => p.KieuDon != 2 && p.KieuDon != 4).Sum(p => p.ThanhTienN),

                      //  Nhập khác (nhập sản xuất)
                      NhapKhac_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && (p.KieuDon == 4)).Sum(p => p.SoLuongN), //nhập sản xuất - trả dược sản xuất
                      NhapKhac_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && (p.KieuDon == 4)).Sum(p => p.ThanhTienN),

                      //xuất trong kỳ (không tính trả dược và xuất sản xuất)
                      TongXuatTKSL = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 10).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                      TongXuatTKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 10).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                      //  xuất khác (xuất sản xuất)
                      XuatKhac_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 10).Sum(p => p.SoLuongX),
                      XuatKhac_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 2 && p.KieuDon == 10).Sum(p => p.ThanhTienX),

                      TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                      TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                  }).Where(p => p.TonDKSL != 0 || p.NhapTKSL != 0 || p.NhapKhac_SL != 0 || p.TongXuatTKSL != 0 || p.XuatKhac_SL != 0).ToList();
            if (lupBBKiemKe.EditValue != null)
            {
                int idnhap = Convert.ToInt32(lupBBKiemKe.EditValue);
                var qndct = data.NhapDcts.Where(p => p.IDNhap == idnhap).ToList();
                foreach (NXT bc in BC)
                { 
                    var qdv = qndct.Where(p=>p.MaDV ==  bc.MaDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        bc.ConKK_SL = qdv.SoLuongKK;
                        bc.ConKK_TT = qdv.ThanhTienKK;
                        if (qdv.SoLuongKK - bc.TonCKSL > 0)
                        {
                            bc.Thua_SL = qdv.SoLuongKK - bc.TonCKSL;
                            bc.Thua_TT = qdv.ThanhTienKK - bc.TonCKTT;
                        }
                        else if (qdv.SoLuongKK - bc.TonCKSL < 0)
                        {
                            bc.Thieu_SL = bc.TonCKSL - qdv.SoLuongKK;
                            bc.Thieu_TT = bc.TonCKTT - qdv.ThanhTienKK;
                        }
                    }
                    else
                    {
                        bc.ConKK_SL = 0;
                        bc.ConKK_TT = 0;
                        bc.Thieu_SL = bc.TonCKSL;
                        bc.Thieu_TT = bc.TonCKTT;
                    }
                }
            }
            
            FormThamSo.rep_NXT_SanXuatThuoc rep = new FormThamSo.rep_NXT_SanXuatThuoc();
            frmIn frm2 = new frmIn();
            if (lkho.Where(p => p != "kho tổng").Count() > 0)
            {
                rep.celTit1.Text = "SL nhập";
                rep.celTit2.Text = "TT nhập";
            }
            rep.celTuNgay.Text = tungay.ToString("dd/MM/yyyy");
            rep.celDenNgay.Text = denngay.ToString("dd/MM/yyyy");
            if (lupBBKiemKe.EditValue != null)
            { 
                DateTime ngaykk = Convert.ToDateTime(lupBBKiemKe.Text);
                rep.celNgayKK.Text = ngaykk.ToString("dd/MM/yyyy HH:mm");
            }
            rep.celKho.Text = kho;
            rep.DataSource = BC.OrderBy(p => p.IDNhom).ThenBy(p => p.TenTN).ThenBy(p => p.TenDV).ToList(); ;
            rep.BindingData();
            rep.CreateDocument();
            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
            frm2.ShowDialog();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            loadBBKK();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            loadBBKK();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            loadBBKK();
        }

        public class MyObject
        {
            public int Value { set; get;}
            public string Text { set; get; }
        }

        private void cklPLNhap_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if(e.Index == 0)
            {
                if (cklPLNhap.GetItemCheckState(0) == CheckState.Checked)
                    cklPLNhap.CheckAll();
                else
                    cklPLNhap.UnCheckAll();
            }
        }

        private void cklPLXuat_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklPLXuat.GetItemCheckState(0) == CheckState.Checked)
                    cklPLXuat.CheckAll();
                else
                    cklPLXuat.UnCheckAll();
            }
        }
    }
}