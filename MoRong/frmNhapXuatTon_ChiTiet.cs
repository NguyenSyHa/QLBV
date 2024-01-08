using DevExpress.Data;
using QLBV.Class;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.MoRong
{
    public partial class frmNhapXuatTon_ChiTiet : Form
    {
        NXT currentNxt;
        List<NXT> listNXT;
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frmNhapXuatTon_ChiTiet(NXT _nxt, List<NXT> _listNXT, object fromTime, object toTime)
        {
            InitializeComponent();
            this.currentNxt = _nxt;
            this.listNXT = _listNXT;
            dtTuNgay.EditValue = fromTime != null ? fromTime : DateTime.Now;
            dtDenNgay.EditValue = toTime != null ? toTime : DateTime.Now;
        }

        private void frmNhapXuatTon_ChiTiet_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                colSoLo.Visible = colHanDung.Visible = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtTuNgay.EditValue != null || dtDenNgay.EditValue != null)
            {
                DateTime? ngayTu = null;
                if (dtTuNgay.EditValue != null)
                    ngayTu = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
                else
                    ngayTu = null;
                DateTime? ngayDen = null;
                if (dtDenNgay.EditValue != null)
                    ngayDen = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
                else
                    ngayDen = null;

                if(DungChung.Bien.MaBV == "24012")
                {
                    listNXT = (from dv in listNXT.Where(o => o.HamLuong == currentNxt.HamLuong && o.DuongD == currentNxt.DuongD && o.NhaSX == currentNxt.NhaSX && o.NuocSX == currentNxt.NuocSX && o.TenDV == currentNxt.TenDV && o.DonVi == currentNxt.DonVi && o.DonGia == currentNxt.DonGia && o.MaDV == currentNxt.MaDV && o.MaTam == currentNxt.MaTam && o.SoDK == currentNxt.SoDK && o.NhomThau == currentNxt.NhomThau && o.MaQD == currentNxt.MaQD && o.TenHC == currentNxt.TenHC)
                               join kp in dataContext.KPhongs on dv.MaKP equals kp.MaKP
                               select new NXT
                               {
                                   NuocSX = dv.NuocSX,
                                   NhaSX = dv.NhaSX,
                                   DuongD = dv.DuongD,
                                   HamLuong = dv.HamLuong,
                                   MaDV = dv.MaDV,
                                   MaTam = dv.MaTam,
                                   DonVi = dv.DonVi,
                                   DonGia = dv.DonGia,
                                   TenHC = dv.TenHC,
                                   TenDV = dv.TenDV,
                                   NhomThau = dv.NhomThau,
                                   SoDK = dv.SoDK,
                                   MaQD = dv.MaQD,
                                   ThuocVatTu = dv.ThuocVatTu,
                                   SoLuong = dv.IsNhap ? dv.SoLuongN : dv.SoLuongX,
                                   SoLuongN = dv.SoLuongN,
                                   SoLuongX = dv.SoLuongX,
                                   ThanhTienN = dv.ThanhTienN,
                                   ThanhTienX = dv.ThanhTienX,
                                   NgayNhap = dv.NgayNhap,
                                   MaKP = dv.MaKP,
                                   TenKP = kp.TenKP,
                                   IsNhap = dv.IsNhap,
                                   SoLo = dv.SoLo,
                                   HanDung = dv.HanDung
                               }).OrderByDescending(o => o.NgayNhap).ToList();
                }
                else
                {
                    listNXT = (from dv in listNXT.Where(o => o.HamLuong == currentNxt.HamLuong && o.DuongD == currentNxt.DuongD && o.NhaSX == currentNxt.NhaSX && o.NuocSX == currentNxt.NuocSX && o.TenDV == currentNxt.TenDV && o.DonVi == currentNxt.DonVi && o.DonGia == currentNxt.DonGia && o.MaDV == currentNxt.MaDV && o.MaTam == currentNxt.MaTam && o.SoDK == currentNxt.SoDK && o.NhomThau == currentNxt.NhomThau && o.MaQD == currentNxt.MaQD && o.TenHC == currentNxt.TenHC)
                               join kp in dataContext.KPhongs on dv.MaKP equals kp.MaKP
                               select new NXT
                               {
                                   NuocSX = dv.NuocSX,
                                   NhaSX = dv.NhaSX,
                                   DuongD = dv.DuongD,
                                   HamLuong = dv.HamLuong,
                                   MaDV = dv.MaDV,
                                   MaTam = dv.MaTam,
                                   DonVi = dv.DonVi,
                                   DonGia = dv.DonGia,
                                   TenHC = dv.TenHC,
                                   TenDV = dv.TenDV,
                                   NhomThau = dv.NhomThau,
                                   SoDK = dv.SoDK,
                                   MaQD = dv.MaQD,
                                   ThuocVatTu = dv.ThuocVatTu,
                                   SoLuong = dv.IsNhap ? dv.SoLuongN : dv.SoLuongX,
                                   SoLuongN = dv.SoLuongN,
                                   SoLuongX = dv.SoLuongX,
                                   ThanhTienN = dv.ThanhTienN,
                                   ThanhTienX = dv.ThanhTienX,
                                   NgayNhap = dv.NgayNhap,
                                   MaKP = dv.MaKP,
                                   TenKP = kp.TenKP,
                                   IsNhap = dv.IsNhap,
                               }).OrderByDescending(o => o.NgayNhap).ToList();
                }

                gridControlNXT.DataSource = listNXT;
            }
            else
                gridControlNXT.DataSource = null;
        }

        private void gridViewNXT_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
            {
                if (e.Column.FieldName == "STT")
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
        }

        private void gridViewNXT_DoubleClick(object sender, EventArgs e)
        {
        }

        private void gridViewNXT_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var row = (NXT)gridViewNXT.GetRow(e.RowHandle);
            if (row != null)
            {
                e.Appearance.ForeColor = row.IsNhap ? Color.Green : Color.Red;
            }

        }
    }
}
