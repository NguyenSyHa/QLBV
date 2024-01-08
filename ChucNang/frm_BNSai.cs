using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.BaoCao;
using QLBV.DungChung;

namespace QLBV.FormThamSo
{
    public partial class frm_BNSai : DevExpress.XtraEditors.XtraForm
    {
        public frm_BNSai()
        {
            InitializeComponent();
        }

        List<Bang79a> _lBN = new List<Bang79a>();
        public frm_BNSai(List<Bang79a> lBN)
        {
            InitializeComponent();
            _lBN = lBN;
        }

        public class GTinh
        {
            private int gtri;
            private string name;
            public int Gtri
            {
                get { return this.gtri; }
                set { this.gtri = value; }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

        private void frm_BNSai_Load(object sender, EventArgs e)
        {
            List<GTinh> _lGtinh = new List<GTinh>();
            _lGtinh.Add(new GTinh { Gtri = 1, Name = "Nam" });
            _lGtinh.Add(new GTinh { Gtri = 0, Name = "Nữ" });
            lupGTinh.DataSource = _lGtinh;
            grcBNhan.DataSource = _lBN.OrderBy(p => p.TenBNhan);

            if (DungChung.Bien.PLoaiKP == "Admin") {
                btnSua.Enabled = true;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            rep_BNhanSai rep = new rep_BNhanSai();
            rep.DataSource = _lBN;
            rep.DataBinding();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            List<BenhNhan> _lbenhNhan = new List<BenhNhan>();
            foreach (var a in _lBN)
            {
                if (a.DChi == "Chưa có mã ICD")
                _lbenhNhan.Add(new BenhNhan { MaBNhan = a.MaBNhan, TenBNhan = a.TenBNhan });
            }
            FormThamSo.Frm_NhapICD frm = new FormThamSo.Frm_NhapICD(_lbenhNhan);
            frm.ShowDialog();
        }

        internal static void _ktbnsai(List<Cls79_80.cl_79_80> listVPBH)
        {
            throw new NotImplementedException();
        }
    }
}