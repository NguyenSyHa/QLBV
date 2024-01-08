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
    public partial class Frm_SoTheoDoiNhapXuatThuocGNHTT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoTheoDoiNhapXuatThuocGNHTT()
        {
            InitializeComponent();
        }
        private bool ktcd()
        {
            if (lupTenDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn tên thuốc");
                lupTenDV.Focus();
                return false;
            }
            if (lupngay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng");
                lupngay.Focus();
                return false;
            }
            return true;
        }



        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_SoTheoDoiNhapXuatThuocGNHTT_Load(object sender, EventArgs e)
        { 
            var D = from tk in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { tk.TenKP, tk.MaKP };
            lupKho.Properties.DataSource = D.ToList();
            lupngaytu.DateTime = System.DateTime.Now;
            lupngay.DateTime = System.DateTime.Now;
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ktcd())
            {
                
                frmIn frm = new frmIn();
                int _Maduoc = 0; int _Kho = 0;
                _Maduoc = lupTenDV.EditValue == null ? 0 : Convert.ToInt32(lupTenDV.EditValue);
                _Kho = lupKho.EditValue == null ? 0 : Convert.ToInt32(lupKho.EditValue);
                DateTime ngayden = System.DateTime.Now.Date;
                DateTime ngaytu = System.DateTime.Now;
                ngaytu = DungChung.Ham.NgayTu(lupngaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngay.DateTime);
                BaoCao.Rep_SoTheoDoiNhapXuatThuocGNHTT rep = new BaoCao.Rep_SoTheoDoiNhapXuatThuocGNHTT();

                rep.Ngaythang.Value = "(Từ ngày " + ngaytu.ToString().Substring(0, 10) + " đến ngày " + ngayden.ToString().Substring(0, 10) + ")";
                rep.ngaytu.Value = ngaytu;
                rep.ngayden.Value = ngayden;
                rep.Khoaphong.Value = _Kho;
                rep.Madv.Value = _Maduoc;
                rep.CQCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.DiaChi.Value = DungChung.Bien.DiaChi;
                if (ckIn.Checked == true) { rep.InDK.Value = 1; }

                var dv = data.DichVus.Where(p => p.MaDV == _Maduoc).Select(p => new { p.TenDV,p.DonVi }).ToList();
                rep.TenDV.Value = dv.First().TenDV;
                rep.Donvi.Value = dv.First().DonVi;

                var q = (from nx in data.NhapDs.Where(p=>p.NgayNhap>=ngaytu && p.NgayNhap<=ngayden).Where(p=>p.MaKP==_Kho)
                         join nxct in data.NhapDcts.Where(p => p.MaDV == _Maduoc) on nx.IDNhap equals nxct.IDNhap
                         join kp in data.KPhongs on nx.MaKP equals kp.MaKP
                          group new { nx, nxct,kp } by new
                         {
                             nx.MaKP,
                             kp.TenKP,
                             nx.NgayNhap,
                             nxct.SoLo,
                             nxct.HanDung,
                             nx.SoCT,
                            // nx.GhiChu,
                         } into kq
                         select new
                         {
                             MaKP=kq.Key.MaKP,
                             Ngaythang = kq.Key.NgayNhap,
                             SoCT = kq.Key.SoCT,
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung,
                             SLNhap = kq.Where(p => p.nx.PLoai == 1).Count() > 0 ? kq.Where(p=>p.nx.PLoai == 1).Sum(p=>p.nxct.SoLuongN) : 0,
                             SLXuat = kq.Where(p => p.nx.PLoai == 2 || p.nx.PLoai == 3).Count() > 0 ? kq.Where(p => p.nx.PLoai == 2 || p.nx.PLoai == 3).Sum(p => p.nxct.SoLuongX) : 0,
                             SLTon = (kq.Where(p => p.nx.PLoai == 1).Count() > 0 && kq.Where(p => p.nx.PLoai == 2 || p.nx.PLoai == 3).Count() > 0) ? (kq.Where(p => p.nx.PLoai == 1).Sum(p => p.nxct.SoLuongN) - kq.Where(p => p.nx.PLoai == 2 || p.nx.PLoai == 3).Sum(p => p.nxct.SoLuongX)) : 0,
                           //  GChu=kq.Key.GhiChu,
                
                         }).ToList().OrderBy(p=>p.Ngaythang).ToList();
                rep.DataSource = q.ToList(); 
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
              
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
               int _MaKP=0;
            if(lupKho.EditValue!=null)
            _MaKP = Convert.ToInt32( lupKho.EditValue);
           
            var q = (from nd in data.NhapDs.Where(p=>p.MaKP==_MaKP && p.PLoai==1)
                     join nxct in data.NhapDcts on nd.IDNhap equals nxct.IDNhap
                     join DV in data.DichVus on nxct.MaDV equals DV.MaDV
                     join tn in data.TieuNhomDVs.Where(p=>p.TenRG=="Thuốc gây nghiện"||p.TenRG=="Thuốc hướng tâm thần") on DV.IdTieuNhom equals tn.IdTieuNhom
                    group new { DV } by new { DV.TenDV, DV.MaDV } into kq
                    select new { TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV }).OrderBy(P=>P.TenDV).ToList();
            lupTenDV.Properties.DataSource = q;
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}