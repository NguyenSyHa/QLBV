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
    public partial class frm_BC_BNVaoVienNgoaiTru_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_BNVaoVienNgoaiTru_30007()
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
                            join bn in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on vv.MaBNhan equals bn.MaBNhan
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
                                kq1.NgayRa,
                                MaKP = kq1 != null ? kq1.MaKP : vv.MaKP,
                                ChanDoan = kq1 != null ? kq1.ChanDoan : vv.ChanDoan
                            }).Where(p => (p.NgayVao.Value >= ngayVV && p.NgayVao.Value <= ngayRV) || (p.NgayVao.Value <= ngayVV && (p.NgayRa.Value == null || p.NgayRa.Value >= ngayVV)) || (p.NgayRa.Value >= ngayVV && p.NgayRa.Value <= ngayRV)).ToList();

            var query = (from n in benhnhan
                         join kp in data.KPhongs on n.MaKP equals kp.MaKP
                         join dtbn in data.DTBNs on n.IDDTBN equals dtbn.IDDTBN
                         select new
                         {
                             n.MaBNhan,
                             kp.TenKP,
                             dtbn.DTBN1,
                             n.TenBNhan,
                             n.Tuoi,
                             Nam = (n.GTinh == 1) ? "x" : "",
                             Nu = (n.GTinh == 0) ? "x" : "",
                             n.DChi,
                             n.ChanDoan
                         }).ToList();

            var result = (from n in query
                          group n by new { n.DTBN1, n.TenKP, n.TenBNhan, n.Nam, n.Nu, n.Tuoi, n.ChanDoan, n.DChi, n.MaBNhan } into kq
                          select new
                          {
                              
                              DTBN = kq.Key.DTBN1,
                              TenKP = kq.Key.TenKP,
                              TenBN = kq.Key.TenBNhan,
                              Tuoi = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && kq.Key.Tuoi.ToString() != "") ? DungChung.Ham.TuoitheoThang(data, Convert.ToInt32(kq.Key.MaBNhan), "12-30") : kq.Key.Tuoi.ToString(),
                              ChuanDoan = kq.Key.ChanDoan,
                              DiaChi = kq.Key.DChi,
                              Nam = kq.Key.Nam,
                              Nu = kq.Key.Nu
                          }).ToList();

            BaoCao.rep_BC_BNVaoVienNgoaiTru_30007 rep = new BaoCao.rep_BC_BNVaoVienNgoaiTru_30007();
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