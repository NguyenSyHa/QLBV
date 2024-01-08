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
    public partial class frm_BC_TheoNhomTuoiVaGTinh_12122 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TheoNhomTuoiVaGTinh_12122()
        {
            InitializeComponent();
        }

        private void frm_BC_TheoNhomTuoiVaGTinh_12122_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

            var qrv = (from bn in data.BenhNhans
                       join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                       join kp in data.KPhongs.Where(p => p.PLoai.Equals("Lâm sàng")) on rv.MaKP equals kp.MaKP
                       select new { bn.Tuoi, bn.GTinh, kp.TenKP }).ToList();

            var query = (from b in qrv
                         group b by new { b.TenKP } into kq
                         select new
                         {
                             kq.Key.TenKP,
                             TS = kq.Count(),
                             TE6 = kq.Where(p => p.Tuoi < 6).Count(),
                             TE15 = kq.Where(p => p.Tuoi >= 6 && p.Tuoi < 15).Count(),
                             Tren60 = kq.Where(p => p.Tuoi >= 60).Count(),
                             Nam = kq.Where(p => p.GTinh == 1).Count(),
                             Nu = kq.Where(p => p.GTinh == 0).Count()
                         }).OrderBy(p => p.TenKP).ToList();

            BaoCao.rep_BC_TheoNhomTuoivaGTinh_12122 rep = new BaoCao.rep_BC_TheoNhomTuoivaGTinh_12122();
            frmIn frm = new frmIn();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = query.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}