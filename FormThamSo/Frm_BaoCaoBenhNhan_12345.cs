using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;
using System.Threading;

namespace QLBV.FormThamSo
{
    public partial class Frm_BaoCaoBenhNhan_12345 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BaoCaoBenhNhan_12345()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao, "Đang tạo báo cáo!", "Xin vui lòng chờ.");
        }
        int[] idNhomDV = new int[] { 1, 2, 3, 8 }; // ID nhóm dv
        private class repbc
        {
            public int STT { get; set; }
            public int? MaBNhan { get; set; }
            public string TenBN { get; set; }
            public string GioiTinh { get; set; }
            public string NamSinh { get; set; }
            public string SoDienThoai { get; set; }
            public string NgayKham { get; set; }
            public string DichVu { get; set; }
            public string DonGia { get; set; }
            public double SoLuong { get; set; }
            public string ThanhTien { get; set; }
            public string KetQua { get; set; }
            public string DChi { get; set; }
                
        }
        private void TaoBaoCao()
        {
            List<int> NhomDV = new List<int>();
            NhomDV.AddRange(idNhomDV);
            List<repbc> rep = new List<repbc>();
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            var BenhNhan = (from bnkb in data.BNKBs.Where(p => p.NgayKham >= TuNgay && p.NgayKham <= DenNgay)
                            join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                            join ttbx in data.TTboXungs on bnkb.MaBNhan equals ttbx.MaBNhan into kq
                            from k in kq.DefaultIfEmpty()
                            select new { bnkb.MaBNhan, bn.TenBNhan, bn.Tuoi, bnkb.NgayKham, bn.GTinh, bn.DChi, bn.NamSinh, sdt = k != null ? k.DThoai : "" }).Distinct().ToList();
            if (BenhNhan.Count > 0)
            {
                frmIn frm = new frmIn();
                var q0 = (from bnkb in BenhNhan
                          join dt in data.DThuocs on bnkb.MaBNhan equals dt.MaBNhan
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                          join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                          select new { bnkb, dtct, dv, ndv.IDNhom }).ToList();

                var q1 = (from l1 in q0
                          group l1 by new { l1.bnkb.NgayKham, l1.bnkb.sdt, l1.bnkb.NamSinh, l1.bnkb.GTinh, l1.bnkb.TenBNhan, l1.bnkb.MaBNhan, l1.dv.TenDV, l1.IDNhom, l1.dtct.DonGia, l1.bnkb.DChi } into kq
                          select new
                          {
                              MaBNhan = kq.Key.MaBNhan,
                              TenBN = kq.Key.TenBNhan,
                              GioiTinh = kq.Key.GTinh == 1 ? "Nam" : "Nữ",
                              NamSinh = kq.Key.NamSinh,
                              SoDienThoai = kq.Key.sdt,
                              NgayKham = kq.Key.NgayKham,
                              DichVu = kq.Key.TenDV,
                              DonGia = kq.Key.DonGia,
                              SoLuong = kq.Sum(p => p.dtct.SoLuong),
                              ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                              DChi = kq.Key.DChi,
                          }).ToList();
                int num = 1;
                foreach (var item in q1)
                {

                    repbc rep1 = new repbc();
                    rep1.STT = num;
                    rep1.MaBNhan = item.MaBNhan;
                    rep1.TenBN = item.TenBN;
                    rep1.GioiTinh = item.GioiTinh;
                    rep1.NamSinh = item.NamSinh;
                    rep1.SoDienThoai = item.SoDienThoai;
                    rep1.NgayKham = item.NgayKham == null ? "" : Convert.ToDateTime(item.NgayKham).ToString("dd/MM/yyyy");
                    rep1.DichVu = item.DichVu;
                    rep1.DonGia = item.DonGia == null ? "0" : item.DonGia.ToString("#,##");
                    rep1.SoLuong = item.SoLuong;
                    rep1.ThanhTien = item.ThanhTien == null ? "0" : item.ThanhTien.ToString("#,##");
                    rep1.DChi = item.DChi;
                    if (rep.Where(p => p.MaBNhan == item.MaBNhan).Count() == 0)
                    {
                        num++;

                    }
                    rep.Add(rep1);



                }

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCBenhNhanTheoTuanThangNam, rep.ToList(), new Dictionary<string, object>(), false);

            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

        }
        private void Frm_BaoCaoBenhNhan_14018_Load(object sender, EventArgs e)
        {
            dtpDenNgay.DateTime = dtpTuNgay.DateTime = DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}