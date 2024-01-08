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
    public partial class frm_BC_PhanTichBenhTat_30003 : DevExpress.XtraEditors.XtraForm
    {
        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_BC_PhanTichBenhTat_30003()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_PhanTichBenhTat_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;

            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng").ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoa.Properties.DataSource = lkp;
            lupKhoa.Properties.DisplayMember = "TenKP";
            lupKhoa.Properties.ValueMember = "MaKP";

            lupKhoa.EditValue = lupKhoa.Properties.GetKeyValueByDisplayText("Tất cả");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int maKP = 0;
            if (lupKhoa.EditValue != null)
                maKP = Convert.ToInt32(lupKhoa.EditValue);

            var query = (from rv in data.RaViens
                         join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                         select new
                         {
                             rv.ChanDoan,
                             MaICD = rv.MaICD.Contains(";") == false ? rv.MaICD : rv.MaICD.Substring(0, rv.MaICD.IndexOf(";")),
                             rv.NgayRa,
                             rv.SoNgaydt,
                             rv.KetQua,
                             rv.MaKP,
                             bn.Tuoi,
                             bn.NoiTru,
                             bn.DTNT,
                             bn.GTinh
                         }).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => maKP == 0 || p.MaKP == maKP).OrderBy(p => p.NgayRa).ToList();

            var result = (from n in query
                          join icd in data.ICD10 on n.MaICD equals icd.MaICD
                          group new { n, icd } by new { n.MaICD, icd.TenICD } into kq
                          select new
                          {
                              TenBenh = kq.Key.TenICD,
                              MaBenh = kq.Key.MaICD,
                              TongSoMacBenh = kq.Count(),
                              TongTuVong = kq.Where(p => p.n.KetQua == "Tử vong").Count(),
                              TongNgayDT = kq.Sum(p => p.n.SoNgaydt) != 0 ? kq.Sum(p => p.n.SoNgaydt).ToString() : "",
                              TreEmDuoi15MacBenh = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 15)).Count(),
                              TreEmDuoi4MacBenh = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 4)).Count(),
                              TreEmTuVongDuoi15 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 15)).Where(p => p.n.KetQua == "Tử vong").Count(),
                              TreEmTuVongDuoi4 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 4)).Where(p => p.n.KetQua == "Tử vong").Count(),
                              TongNgayDTTreEmDuoi15 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 15)).Sum(p => p.n.SoNgaydt),
                              TongNgayDTTreEmDuoi4 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi <= 4)).Sum(p => p.n.SoNgaydt),
                              TongMacBenhTren60 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi >= 60)).Count(),
                              TongNuMacBenhTren60 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi >= 60)).Where(p => p.n.GTinh == 0).Count(),
                              TongTuVongTren60 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi >= 60)).Where(p => p.n.KetQua == "Tử vong").Count(),
                              TongNuTuVongTren60 = kq.Where(p => (p.n.Tuoi == null || p.n.Tuoi >= 60)).Where(p => p.n.KetQua == "Tử vong").Where(p => p.n.GTinh == 0).Count()
                          }).ToList();

            BaoCao.Rep_BC_PhanTichBenhTat_30003 rep = new BaoCao.Rep_BC_PhanTichBenhTat_30003();
            frmIn frm = new frmIn();
            rep.DataSource = result;
            rep.lbl_tungaydenngay.Text = "Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy");
            rep.Bindingdata();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}