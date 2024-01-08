using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_06 : DevExpress.XtraEditors.XtraForm
    {
        public frm_06()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _noitru = -1;
            _noitru = radiongoaitru.SelectedIndex;
            DateTime Ngaytu=DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime Ngayden=DungChung.Ham.NgayDen(lupNgayden.DateTime);
            var q = (from dv in _Data.DichVus
                     join vpct in _Data.VienPhicts on dv.MaDV equals vpct.MaDV
                     join vp in _Data.VienPhis on vpct.idVPhi equals vp.idVPhi
                     join bn in _Data.BenhNhans.Where(p=>p.NoiTru==_noitru) on vp.MaBNhan equals bn.MaBNhan
                     //join nxct in _Data.NhapDcts on dv.MaDV equals nxct.MaDV
                     //join nx in _Data.NhapDs on nxct.IDNhap equals nx.IDNhap
                     join nd in _Data.NhomDVs on dv.IDNhom equals nd.IDNhom
                     join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                     where (vp.NgayTT<=Ngayden&&vp.NgayTT>=Ngaytu)
                     group new { dv, vpct,nd, tn } by new { tn.TenTN, dv.TenDV, dv.SoDK, dv.DuongD, dv.DangBC, dv.NhaSX, dv.NuocSX, dv.QCPC, dv.DonVi, vpct.DonGia, dv.TrongDM } into kq
                     select new
                     {
                         Chiphi = kq.Sum(p => p.vpct.ThanhTien),
                         CPthuoc = kq.Sum(p => p.vpct.ThanhTien),
                         DangBC = kq.Key.DangBC,
                         Duongdung = kq.Key.DuongD,
                         DVT = kq.Key.DonVi,
                         GiaBHYT = kq.Key.DonGia,
                         Giadenghi = kq.Key.DonGia,
                         GiaTT = kq.Key.DonGia,
                         NhaSX = kq.Key.NhaSX,
                         NuocSX = kq.Key.NuocSX,
                         Quycach = kq.Key.QCPC,
                         SLsudung = kq.Sum(p => p.vpct.SoLuong),
                         SoDK = kq.Key.SoDK,
                         Tenduoc = kq.Key.TenDV,
                         TLTT = kq.Key.TrongDM,
                         tieunhom = kq.Key.TenTN
                     }).OrderBy(p => p.tieunhom).ToList();
            if (q.Count > 0)
            {
                frmIn frm = new frmIn();
                BaoCao.rep_06 rep = new BaoCao.rep_06();
                rep.tungayden.Value = " Từ ngày: " + lupNgaytu.DateTime.ToString().Substring(0, 10) + " đến ngày: " + lupNgayden.DateTime.ToString().Substring(0, 10);
                rep.DataSource = q;
                rep.BindingData();
                rep.CreateDocument();
               // rep.DataMember = "Table";
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                frmIn frm = new frmIn();
                BaoCao.rep_06 rep = new BaoCao.rep_06();
                rep.CreateDocument();
               // rep.DataMember = "Table";
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void frm_06_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}