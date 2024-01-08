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
    public partial class frm_BC_BNVaoVienNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_BNVaoVienNgoaiTru()
        {
            InitializeComponent();
        }

        private void frm_BC_BNVaoVienNgoaiTru_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            dateNgayRaV.DateTime = DateTime.Now;
            dateNgayVaoV.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngayVV = DungChung.Ham.NgayTu(dateNgayVaoV.DateTime);
            DateTime ngayRV = DungChung.Ham.NgayDen(dateNgayRaV.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var benhnhan = (from vv in data.VaoViens
                            join bn in data.BenhNhans on vv.MaBNhan equals bn.MaBNhan
                            join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                            select new
                            {
                                vv.NgayVao,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.Tuoi,
                                bn.GTinh,
                                bn.DChi,
                                bn.IDDTBN,
                                bn.NoiTru,
                                bn.DTNT,
                                NgayRa = (kq1 == null && kq1.NgayRa >= ngayRV) ? null : kq1.NgayRa,
                                //MaKP = kq1 != null ? kq1.MaKP : vv.MaKP,
                                MaKP = (kq1 != null && kq1.NgayRa >= ngayVV && kq1.NgayRa <= ngayRV) ? kq1.MaKP : vv.MaKP,
                                ChanDoan = kq1 != null ? kq1.ChanDoan : vv.ChanDoan
                            }).Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => (p.NgayVao >= ngayVV && p.NgayVao <= ngayRV) || (p.NgayVao <= ngayVV && (p.NgayRa == null || p.NgayRa >= ngayVV || (p.NgayRa >= ngayVV && p.NgayRa <= ngayRV))) || (p.NgayVao >= ngayVV && p.NgayVao <= ngayRV && p.NgayRa >= ngayRV)).ToList();

            var query = (from n in benhnhan
                         join kp in data.KPhongs on n.MaKP equals kp.MaKP
                         join dtbn in data.DTBNs on n.IDDTBN equals dtbn.IDDTBN
                         select new
                         {
                             kp.TenKP,
                             DTBN = (dtbn.DTBN1 == "BHYT") ? "BHYT" : "Viện phí",
                             n.TenBNhan,
                             n.Tuoi,
                             Nam = (n.GTinh == 1) ? "x" : "",
                             Nu = (n.GTinh == 0) ? "x" : "",
                             n.DChi,
                             n.ChanDoan
                         }).ToList();

            var result = (from n in query
                          group n by new { n.DTBN, n.TenKP, n.TenBNhan, n.Nam, n.Nu, n.Tuoi, n.ChanDoan, n.DChi } into kq
                          select new
                          {
                              DTBN = kq.Key.DTBN,
                              TenKP = kq.Key.TenKP,
                              TenBN = kq.Key.TenBNhan,
                              Tuoi = kq.Key.Tuoi,
                              ChuanDoan = kq.Key.ChanDoan,
                              DiaChi = kq.Key.DChi,
                              Nam = kq.Key.Nam,
                              Nu = kq.Key.Nu
                          }).ToList();

            BaoCao.Rep_BC_BNVaoVienNgoaiTru rep = new BaoCao.Rep_BC_BNVaoVienNgoaiTru();
            frmIn frm = new frmIn();
            rep.DataSource = result;
            rep.lbl_tungaydenngay.Text = "Từ ngày " + dateNgayVaoV.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateNgayRaV.DateTime.ToString("dd/MM/yyyy");
            rep.Bindingdata();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}