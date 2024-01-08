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
    public partial class frm_BC_BenhTruyenNhiemThang_30004 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_BenhTruyenNhiemThang_30004()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_BenhTruyenNhiemThang_30004_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            var qbn = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                       join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                       from k1 in k.DefaultIfEmpty()
                       select new
                       {
                           MaICD = bnkb.MaICD,
                           KetQua = k1.KetQua == null ? "" : k1.KetQua,
                           bn.DChi
                       }).ToList();
            var q = (from b in qbn
                     group b by b.DChi into kq
                     select new
                     {
                         DiaChi = kq.Key,
                         M1 = kq.Where(p => p.MaICD.Equals("B30.0") || p.MaICD.Equals("B30.1") || p.MaICD.Equals("B30.2") || p.MaICD.Equals("B30.3")).Count(),
                         C1 = kq.Where(p => p.MaICD.Equals("B30.0") || p.MaICD.Equals("B30.1") || p.MaICD.Equals("B30.2") || p.MaICD.Equals("B30.3"))
                              .Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M2 = kq.Where(p => p.MaICD.Contains("J10")).Count(),
                         C2 = kq.Where(p => p.MaICD.Contains("J10")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M3 = kq.Where(p => p.MaICD.Contains("A06")).Count(),
                         C3 = kq.Where(p => p.MaICD.Contains("A06")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M4 = kq.Where(p => p.MaICD.Contains("A03")).Count(),
                         C4 = kq.Where(p => p.MaICD.Contains("A03")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M5 = kq.Where(p => p.MaICD.Contains("B26")).Count(),
                         C5 = kq.Where(p => p.MaICD.Contains("B26")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M6 = kq.Where(p => p.MaICD.Contains("B01")).Count(),
                         C6 = kq.Where(p => p.MaICD.Contains("B01")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M7 = kq.Where(p => p.MaICD.Contains("A09")).Count(),
                         C7 = kq.Where(p => p.MaICD.Contains("A09")).Where(p => p.KetQua.Equals("Tử vong")).Count(),
                         M8 = kq.Where(p => p.MaICD.Equals("B17") || p.MaICD.Equals("B17.8") || p.MaICD.Equals("B19") || p.MaICD.Equals("B19.0") ||
                                            p.MaICD.Equals("B19.9") || p.MaICD.Equals("B18.8") || p.MaICD.Equals("B18.9")).Count(),
                         C8 = kq.Where(p => p.MaICD.Equals("B17") || p.MaICD.Equals("B17.8") || p.MaICD.Equals("B19") || p.MaICD.Equals("B19.0") ||
                                            p.MaICD.Equals("B19.9") || p.MaICD.Equals("B18.8") || p.MaICD.Equals("B18.9")).Where(p => p.KetQua.Equals("Tử vong")).Count()
                     }).Where(p => p.M1 != 0 || p.C1 != 0 || p.M2 != 0 || p.C2 != 0 || p.M3 != 0 || p.C3 != 0 || p.M4 != 0 || p.C4 != 0
                                || p.M5 != 0 || p.C5 != 0 || p.M6 != 0 || p.C6 != 0 || p.M7 != 0 || p.C7 != 0 || p.M8 != 0 || p.C8 != 0).ToList();
            var query = (from a in q
                         select new
                         {
                             a.DiaChi,
                             M1 = a.M1 == 0 ? "" : a.M1.ToString(),
                             C1 = a.C1 == 0 ? "" : a.C1.ToString(),
                             M2 = a.M2 == 0 ? "" : a.M2.ToString(),
                             C2 = a.C2 == 0 ? "" : a.C2.ToString(),
                             M3 = a.M3 == 0 ? "" : a.M3.ToString(),
                             C3 = a.C3 == 0 ? "" : a.C3.ToString(),
                             M4 = a.M4 == 0 ? "" : a.M4.ToString(),
                             C4 = a.C4 == 0 ? "" : a.C4.ToString(),
                             M5 = a.M5 == 0 ? "" : a.M5.ToString(),
                             C5 = a.C5 == 0 ? "" : a.C5.ToString(),
                             M6 = a.M6 == 0 ? "" : a.M6.ToString(),
                             C6 = a.C6 == 0 ? "" : a.C6.ToString(),
                             M7 = a.M7 == 0 ? "" : a.M7.ToString(),
                             C7 = a.C7 == 0 ? "" : a.C7.ToString(),
                             M8 = a.M8 == 0 ? "" : a.M8.ToString(),
                             C8 = a.C8 == 0 ? "" : a.C8.ToString(),
                         }).ToList();

            BaoCao.rep_BC_BenhTruyenNhiemThang_30004 rep = new BaoCao.rep_BC_BenhTruyenNhiemThang_30004();
            frmIn frm = new frmIn();
            rep.lblThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = query.OrderBy(p => p.DiaChi).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}