using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_Mau13_ChitietThuoc : DevExpress.XtraEditors.XtraForm
    {

        private List<FormThamSo.frm_CapNhatKPtongKet.cl_chiphi> lcp;
        private List<KPhong> dskp;

        public frm_Mau13_ChitietThuoc()
        {
            InitializeComponent();
        }


        public frm_Mau13_ChitietThuoc(List<FormThamSo.frm_CapNhatKPtongKet.cl_chiphi> lcp, List<KPhong> dskp)
        {
            // TODO: Complete member initialization
            this.lcp = lcp;
            this.dskp = dskp;
            InitializeComponent();
        }

        private void frm_Mau13_ChitietThuoc_Load(object sender, EventArgs e)
        {
            lup_MaKP.DisplayMember = "TenKP";
            lup_MaKP.ValueMember = "MaKP";
            lup_MaKP.DataSource = dskp;

            grcThanhToan.DataSource = lcp;
        }
    }
}