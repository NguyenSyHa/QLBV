using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Columns;




namespace QLBV.FormNhap
{
    public partial class frm_Update_DichVuEx : DevExpress.XtraEditors.XtraForm
    {
        public frm_Update_DichVuEx()
        {
            InitializeComponent();
        }
        string _tk = "";
        public frm_Update_DichVuEx(string tk)
        {
            _tk = tk;
            InitializeComponent();
        }
        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Mã / tên dịch vụ/ Mã QD")         

                txtTimKiem.Text = "";
            
        }

        private void frm_Update_DichVuEx_Load(object sender, EventArgs e)
        {
             QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
             txtTimKiem.Text = "Mã / tên dịch vụ/ Mã QD";
            if (!string.IsNullOrEmpty(_tk))
            txtTimKiem.Text = _tk;
            List<HinhThucMua> _listHinhThuc = new List<HinhThucMua>();
            _listHinhThuc.Add(new HinhThucMua { MaHinhThuc = 0, TenHinhThuc = "" });
            _listHinhThuc.Add(new HinhThucMua { MaHinhThuc = 1, TenHinhThuc = "Theo kết quả đấu thầu của SYT" });
            _listHinhThuc.Add(new HinhThucMua { MaHinhThuc = 2, TenHinhThuc = "Đấu thầu do đơn vị tự tổ chức" });
            _listHinhThuc.Add(new HinhThucMua { MaHinhThuc = 3, TenHinhThuc = "Hình thức mua khác" });
            lupHinhThucMua.DataSource = _listHinhThuc;

            List<NhaCC> _listNCC = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.DataSource = _listNCC;


            #region hiển thị theo gridlookupedit
            //grLupNCC.DataSource = _listNCC;
            //grLupNCC.View.PopulateColumns(grLupNCC.DataSource);
            //grLupNCC.View.Columns.ColumnByFieldName("MaCC").Visible = true;
            //grLupNCC.View.Columns.ColumnByFieldName("TenCC").Visible = true;
            //   grLupNCC.View.Columns(grLupNCC.ValueMember).Visible = False;
            #endregion
            LoadDSDichVu();
        }
        class HinhThucMua {
            private int maHinhThuc;

            public int MaHinhThuc
            {
                get { return maHinhThuc; }
                set { maHinhThuc = value; }
            }
            private string tenHinhThuc;

            public string TenHinhThuc
            {
                get { return tenHinhThuc; }
                set { tenHinhThuc = value; }
            }
        }
        private class Dichvubx
        {
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string maHC;

            public string MaHC
            {
                get { return maHC; }
                set { maHC = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private string nCC;

            public string NCC
            {
                get { return nCC; }
                set { nCC = value; }
            }
            private string nhaSX;

            public string NhaSX
            {
                get { return nhaSX; }
                set { nhaSX = value; }
            }
            private string maATC;

            public string MaATC
            {
                get { return maATC; }
                set { maATC = value; }
            }
            private string ven;

            public string VEN
            {
                get { return ven; }
                set { ven = value; }
            }
            private int maHinhThuc;

            public int MaHinhThuc
            {
                get { return maHinhThuc; }
                set { maHinhThuc = value; }
            }
            private double? donGia;

            public double? DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            public string TenThau2017 { set; get; }
        }
        private void LoadDSDichVu()
        {
            string tk = txtTimKiem.Text;
            if (tk == "Mã / tên dịch vụ/ Mã QD")
                tk = "";
            int maDV = 0;
            int ot;
            if( Int32.TryParse(tk, out ot))
                maDV = Convert.ToInt32(tk);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dvu = (from dv in data.DichVus join ncc in data.NhaCCs on dv.MaCC equals ncc.MaCC into kq from kq1 in kq.DefaultIfEmpty() select new { dv.MaDV, dv.TenDV, dv.MaQD, dv.DonGia, dv.NhaSX, TenCC =kq1!=null? kq1.TenCC:"" }).ToList();
            var _listDV = dvu.Where(p => tk == "" || p.MaDV == maDV || p.MaQD == tk || (p.TenDV!=null && p.TenDV.ToUpper().Contains(tk.ToUpper())) ).OrderBy(p => p.TenDV).ToList();
            var q = (from dv in _listDV                     
                     join dvex in data.DichVuExes on dv.MaDV equals dvex.MaDV into kq
                     from kq1 in kq.DefaultIfEmpty()
                     select new Dichvubx { MaDV = dv.MaDV, TenDV = dv.TenDV, NCC = dv.TenCC, NhaSX = dv.NhaSX, MaHC =kq1==null?"": kq1.MaHC, DonGia = dv.DonGia, MaATC = kq1 == null ? "" : kq1.MaATC,VEN=kq1==null?"":(kq1.VEN==null?"":kq1.VEN), MaHinhThuc = kq1 == null ? 0 : kq1.HinhThucMua??0 , TenThau2017 = kq1 == null ? "" : kq1.TenThau2017}).ToList();
            grc_DichVu.DataSource = q;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "Mã / tên dịch vụ/ Mã QD")
                LoadDSDichVu();
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
                txtTimKiem.Text = "Mã / tên dịch vụ/ Mã QD";
        }

        private void grv_DichVu_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int row = grv_DichVu.FocusedRowHandle;
            QLBV_Database.QLBVEntities data  = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (row >= 0)
            {
                int maDv = Convert.ToInt32(grv_DichVu.GetRowCellValue(row, colMaDV));
                DichVuEx dv = new DichVuEx();
                  dv  = data.DichVuExes.Where(p => p.MaDV == maDv).FirstOrDefault();
                if (dv == null)
                {
                    dv = new DichVuEx();
                    dv.MaDV = maDv;
                    dv.MaATC = grv_DichVu.GetRowCellValue(row, colMaATC).ToString();
                    dv.VEN = grv_DichVu.GetRowCellValue(row, colVEN).ToString();
                    if (grv_DichVu.GetRowCellValue(row, colTenThhau2017) != null)
                    dv.TenThau2017 = grv_DichVu.GetRowCellValue(row, colTenThhau2017).ToString();

                    if (grv_DichVu.GetRowCellValue(row, colHinhThucMua) != null)
                        dv.HinhThucMua = Convert.ToInt32(grv_DichVu.GetRowCellValue(row, colHinhThucMua));
                    if (grv_DichVu.GetRowCellValue(row, colMaHC) != null)
                        dv.MaHC = grv_DichVu.GetRowCellValue(row, colMaHC).ToString();
                    data.DichVuExes.Add(dv);
                    data.SaveChanges();
                }
                else
                {
                    dv.VEN = grv_DichVu.GetRowCellValue(row, colVEN).ToString();
                    dv.MaATC = grv_DichVu.GetRowCellValue(row, colMaATC).ToString();
                    if (grv_DichVu.GetRowCellValue(row, colTenThhau2017) != null)
                    dv.TenThau2017 = grv_DichVu.GetRowCellValue(row, colTenThhau2017).ToString();
                    if (grv_DichVu.GetRowCellValue(row, colHinhThucMua) != null)
                        dv.HinhThucMua = Convert.ToInt32(grv_DichVu.GetRowCellValue(row, colHinhThucMua));
                    if (grv_DichVu.GetRowCellValue(row, colMaHC) != null)
                        dv.MaHC = grv_DichVu.GetRowCellValue(row, colMaHC).ToString();
                    data.SaveChanges();
                }

            }
           
        }

        private void frm_Update_DichVuEx_FormClosing(object sender, FormClosingEventArgs e)
        {
            grv_DichVu_ValidateRow(null, null);
        }

       
    }
}