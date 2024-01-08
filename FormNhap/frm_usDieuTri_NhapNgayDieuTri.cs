using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_usDieuTri_NhapNgayDieuTri : Form
    {
        public delegate bool DelegateDieuTri(int maBN, List<QLBV.FormNhap.frm_usDieuTri_NhapNgayDieuTri.ThoiGianDieuTri> listThoiGianDT);
        public delegate bool DelegateDieuTri14018(int maBN, QLBV.FormNhap.frm_usDieuTri_NhapNgayDieuTri.ToDieuTri14018 toDieuTri14018, int? _maKP);
        DelegateDieuTri delegateDieuTri;
        DelegateDieuTri14018 delegateDieuTri14018;
        int maBN;
        int? maKP;

        public frm_usDieuTri_NhapNgayDieuTri(DelegateDieuTri _delegateDieuTri, int _maBN)
        {
            InitializeComponent();
            this.delegateDieuTri = _delegateDieuTri;
            this.maBN = _maBN;
        }

        public frm_usDieuTri_NhapNgayDieuTri(DelegateDieuTri14018 _delegateDieuTri14018, int _maBN)
        {
            InitializeComponent();
            this.delegateDieuTri14018 = _delegateDieuTri14018;
            this.maBN = _maBN;
        }

        public frm_usDieuTri_NhapNgayDieuTri(DelegateDieuTri14018 _delegateDieuTri14018, int _maBN, int? _maKP)
        {
            InitializeComponent();
            this.delegateDieuTri14018 = _delegateDieuTri14018;
            this.maBN = _maBN;
            this.maKP = _maKP;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (bindingSource1.DataSource != null)
            {
                var dataSource = (List<ThoiGianDieuTri>)bindingSource1.DataSource;
                if (dataSource.Count > 0)
                {
                    if (dataSource.Exists(o => o.TimeFrom == null || o.TimeTo == null))
                    {
                        MessageBox.Show("Thời gian không được để trống", "Thông báo");
                        return;
                    }
                    ToDieuTri14018 tdt = new ToDieuTri14018();
                    tdt.ThoiGianDieuTri = dataSource;
                    tdt.TongKet = memoEdit1.Text;
                    if (delegateDieuTri != null)
                        delegateDieuTri(maBN, dataSource);
                    if (delegateDieuTri14018 != null)
                        delegateDieuTri14018(maBN, tdt, maKP);
                }
            }
        }

        public class ToDieuTri14018
        {
            public List<ThoiGianDieuTri> ThoiGianDieuTri { get; set; }
            public string TongKet { get; set; }
        }

        public class ThoiGianDieuTri
        {
            public DateTime? TimeFrom { get; set; }
            public DateTime? TimeTo { get; set; }
        }

        private void frm_usDieuTri_NhapNgayDieuTri_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "14018" && DungChung.Bien.MaBV != "14017")
                xtraTabPage2.PageVisible = false;
            List<ThoiGianDieuTri> listThoiGianDieuTri = new List<ThoiGianDieuTri>();
            bindingSource1.DataSource = listThoiGianDieuTri;
            gridControlNgayDieuTri.DataSource = bindingSource1;
        }

        private void gridViewNgayDieuTri_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }
    }
}
