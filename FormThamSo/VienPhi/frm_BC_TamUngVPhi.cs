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
    public partial class frm_BC_TamUngVPhi : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TamUngVPhi()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DateTime _tungay = date_TuNgay.DateTime;
            DateTime _denngay = date_DenNgay.DateTime;

            BaoCao.BC_TamUngVPhi _rep = new BaoCao.BC_TamUngVPhi();
            frmIn _frm = new frmIn();
            
            var _ld = (from tu in _dataContext.TamUngs
                       join bn in _dataContext.BenhNhans on tu.MaBNhan equals bn.MaBNhan

                       join kp in _dataContext.KPhongs on tu.MaKP equals kp.MaKP
                       select new
                       {
                           
                           TenBNhan = bn.TenBNhan,
                           Tuoi = bn.Tuoi,
                           SoBL = tu.SoHD,
                           DChi = bn.DChi,
                           Khoa = kp.TenKP,
                           IDDTBN = bn.IDDTBN,
                           SoTien = tu.SoTien,
                           Tong = tu.SoTien,
                           NgayThu = tu.NgayThu
                       }).ToList().AsEnumerable().Where(s => s.NgayThu.Value.Date >= _tungay.Date && s.NgayThu.Value.Date <= _denngay.Date).Select(p => new
                       {
                           
                           TenBNhan = p.TenBNhan,
                           Tuoi = p.Tuoi,
                           SoBL = p.SoBL,
                           DChi = p.DChi,
                           Khoa = p.Khoa,
                           VP = p.IDDTBN!=2?p.SoTien:0,
                           BaoHiem =p.IDDTBN==2?p.SoTien:0,
                           Tong = p.SoTien,
                           NgayThu = p.NgayThu.Value.Date
                       }).ToList();
            if (txt_TieudeBC.Text.Trim().Length > 0)
            {
                _rep.p_Tieude.Value = txt_TieudeBC.Text.ToUpper();
            }
            _rep.p_TuNgayDenNgay.Value = string.Format("Từ ngày: {0} - Đến ngày: {1}",_tungay.ToString("dd/MM/yyyy"),_denngay.ToString("dd/MM/yyyy"));
            _rep.DataSource = _ld;
            _rep.BindingData();
            _rep.CreateDocument();
            _frm.prcIN.PrintingSystem = _rep.PrintingSystem;
            _frm.ShowDialog();
        }

        private void frm_BC_TamUngVPhi_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;

        }
    }
}