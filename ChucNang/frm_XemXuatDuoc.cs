using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormNhap
{
    public partial class frm_XemXuatDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_XemXuatDuoc()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV != "24012")
            {
                grvNhapCT.Columns.Remove(colXemDSBN);
            }
        }
        int _id = 0;
        string _tenbn = "";
        string _maCQCQ = "";
        int mabn = 0;
        bool noitru = true;
        public frm_XemXuatDuoc(int id, string tenbn)
        {
            InitializeComponent();
            _id = id;
            _tenbn = tenbn;
            colMaDV.ColumnEdit = lupMaDuoc;
            colMaDV.FieldName = "MaDV";
            colSoLuong.FieldName = "SoLuongX";
            colThanhTien.FieldName = "ThanhTienX";
            colThanhTien.SummaryItem.FieldName = "ThanhTienX";
            noitru = false;
        }
        List<DThuocct> _lstdt = new List<DThuocct>();
        public frm_XemXuatDuoc(List<DThuocct> lstdt, string tenkp, int mabn)
        {
            InitializeComponent();
            labTenBN.Text = tenkp;
            _lstdt = lstdt;
            colSoLuong.FieldName = "SoLuong";
            colThanhTien.FieldName = "ThanhTien";
            colThanhTien.SummaryItem.FieldName = "ThanhTien";
            if (mabn != 0)
            {
                var tentuoimabn = _data.BenhNhans.Where(p => p.MaBNhan == mabn).ToList();
                if (tentuoimabn.Count > 0)
                {
                    TenBN24012.Text = "Tên BN: " + tentuoimabn.First().TenBNhan + " - Tuổi: " + tentuoimabn.First().Tuoi + " - Mã BN: " + mabn;
                }
            }
            noitru = true;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_XemXuatDuoc_Load(object sender, EventArgs e)
        {
            if (noitru)
            {
                lupMaDuoc.DataSource = _data.DichVus.ToList();
                grcNhapCT.DataSource = _lstdt;
            }
            else
            {
                labTenBN.Text = _tenbn;
                var dv = _data.DichVus.ToList();

                var result = (from dt in _data.DThuocs.Where(p => p.IDDon == _id)
                              join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                              join ttdv in _data.DichVus on dtct.MaDV equals ttdv.MaDV
                              select new NDCT
                              {
                                  MaDV = dtct.MaDV,
                                  TenDV = ttdv.TenDV,
                                  DonVi = dtct.DonVi,
                                  DonGia = dtct.DonGia,
                                  SoLuongX = dtct.SoLuong,
                                  ThanhTienX = dtct.ThanhTien,
                                  SoLuongN = 0,
                                  ThanhTienN = 0,
                                  GhiChu = (ttdv.IDNhom == 10 || ttdv.IDNhom == 11) ? "Vật tư" : "Thuốc",
                                  DuongD = ttdv.DuongD
                              }).OrderBy(p => p.DonVi).ThenBy(p => p.DuongD).ToList();
                lupMaDuoc.DataSource = dv.ToList();
                var dct = _data.NhapDcts.Where(p => p.IDNhap == _id).ToList();

                grcNhapCT.DataSource = result;
            }
        }

        public class NDCT
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public double DonGia { get; set; }
            public double SoLuongX { get; set; }
            public double ThanhTienX { get; set; }
            public double SoLuongN { get; set; }
            public double ThanhTienN { get; set; }
            public string GhiChu { get; set; }
            public string DuongD { get; set; }
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {

        }

        private void grvNhapCT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXemDSBN")
            {
                int madv = 0;
                double dongia = 0;
                if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                {
                    madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                    var azx = grvNhapCT.DataSource as List<DThuocct>;
                    if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null)
                        dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                    FormThamSo.frm_dsBNlinhthuoc frm = new FormThamSo.frm_dsBNlinhthuoc(_lstdt.First().MaKP.Value, _lstdt.First().SoPL, madv, 1, dongia, true);
                    frm.ShowDialog();
                }
            }
        }
    }
}