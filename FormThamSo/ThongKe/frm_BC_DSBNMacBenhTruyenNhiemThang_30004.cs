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
    public partial class frm_BC_DSBNMacBenhTruyenNhiemThang_30004 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_DSBNMacBenhTruyenNhiemThang_30004()
        {
            InitializeComponent();
        }

        private void frm_BC_DSBNMacBenhTruyenNhiemThang_30004_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);

            #region Mã ICD bệnh truyền nhiễm phải báo cáo hàng tháng
            string maICD = "A82;A37;A15;B50;B51;B52;B53;B54;A01;A33;A34;A35;B15;B16;B17.1;A83.0;A83;A84;A85;A27;B30.0;B30.1;B30.2;B30.3;J10;A06;A03;B26;B01;A09;A80;A36;B95;A20;A98.4;A96.2;A98.3;B06;A92.3;A95;A91;B05;A00;A08.4;A22;A39.0";
            List<string> lMaICD = new List<string>();
            lMaICD = maICD.Split(';').ToList();
            #endregion

            //var qcls = (from cls in data.CLS
            //            join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
            //            join clsct in data.CLScts.Where(p => p.KetQua != null && p.KetQua != "") on cd.IDCD equals clsct.IDCD
            //            join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //            join dv in data.DichVus on dvct.MaDV equals dv.MaDV
            //            join ndv in data.NhomDVs.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")) on dv.IDNhom equals ndv.IDNhom
            //            group new { cls, cd, clsct, dvct } by new { cls.MaBNhan, dvct.TenDVct, clsct.KetQua } into kq
            //            select new
            //            {
            //                kq.Key.MaBNhan,
            //                KQXN = kq.Key.TenDVct + ": " + kq.Key.KetQua
            //            }).ToList();

            var qbn = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                       join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join vv in data.VaoViens on bnkb.MaBNhan equals vv.MaBNhan
                       join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into q
                       from q1 in q.DefaultIfEmpty()
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                       from k1 in k.DefaultIfEmpty()
                       select new
                       {
                           bn.MaBNhan,
                           MaICD = bnkb.MaICD,
                           KetQua = k1.KetQua == null ? "" : k1.KetQua,
                           bn.TenBNhan,
                           bn.Tuoi,
                           bn.GTinh,
                           SDT = q1 == null ? "" : q1.DThoai,
                           q1.NgayKhoiPhat,
                           bn.DChi,
                           ChanDoan = bnkb.ChanDoan + ", " + vv.ChanDoan
                       }).ToList();

            var query = (from a in lMaICD
                         join b in qbn on a equals b.MaICD
                         //join c in qcls on b.MaBNhan equals c.MaBNhan into kq
                         //from kq1 in kq.DefaultIfEmpty()
                         group new { a, b } by new { b.MaBNhan, b.MaICD, b.KetQua, b.TenBNhan, b.Tuoi, b.GTinh, b.SDT, b.NgayKhoiPhat, b.DChi, b.ChanDoan } into q
                         select new
                         {
                             q.Key.MaBNhan,
                             q.Key.MaICD,
                             q.Key.KetQua,
                             q.Key.TenBNhan,
                             q.Key.Tuoi,
                             GioiTinh = q.Key.GTinh == 0 ? "Nữ" : "Nam",
                             q.Key.SDT,
                             q.Key.NgayKhoiPhat,
                             q.Key.DChi,
                             q.Key.ChanDoan
                         }).Distinct().ToList();

            BaoCao.rep_BC_DSBNMacBenhTruyenNhiem_30004 rep = new BaoCao.rep_BC_DSBNMacBenhTruyenNhiem_30004();
            frmIn frm = new frmIn();
            rep.lblThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = query.OrderBy(p => p.TenBNhan).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}