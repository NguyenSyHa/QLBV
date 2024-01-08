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
    public partial class frm_TKDSBN : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKDSBN()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            BaoCao.rep_TKDSBN rep = new BaoCao.rep_TKDSBN();
            int _mabntu = 0,_mabnden=0;
            DateTime ngay=System.DateTime.Now;
            int ot;
            int _int_maBNTu = 0;
            if (Int32.TryParse(txtMaBNtu.Text, out ot))
                _int_maBNTu = Convert.ToInt32(txtMaBNtu.Text);
            //if (!string.IsNullOrEmpty(txtMaBNtu.Text))
            //    int.TryParse(txtMaBNtu.Text,out _mabntu);
            //if (!string.IsNullOrEmpty(txtMaBNden.Text))
            //    int.TryParse(txtMaBNden.Text, out _mabnden);


            var bntu = _data.BenhNhans.Where(p => p.MaBNhan == _int_maBNTu).ToList();
            if (bntu.Count > 0)
            {
                _mabntu = bntu.First().MaBNhan;
                ngay = bntu.First().NNhap.Value;
            }
            var bnden = _data.BenhNhans.Where(p => p.MaBNhan == _int_maBNTu).ToList();
            if (bnden.Count > 0)
                _mabnden = bnden.First().MaBNhan;
            var dsbn = (from bn in _data.BenhNhans.Where(p=>p.DTuong=="BHYT")
                        where (bn.MaBNhan >= _mabntu && bn.MaBNhan <= _mabnden)
                        select bn).OrderBy(p => p.TenBNhan).ToList();
            rep.MaSoBN.Value="Mã Bệnh Nhân từ: "+_mabntu +"đến "+_mabnden;
            rep.TuNgay.Value = DungChung.Ham.NgaySangChu(ngay);
            rep.DataSource = dsbn.ToList();
            rep.BindingDaTa();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TKDSBN_Load(object sender, EventArgs e)
        {
            int _kcach = 20;
            int id = 0;
            int idtu = 0;
            var mabn = (from bn in _data.BenhNhans select bn).OrderByDescending(p => p.MaBNhan).ToList();
            if (mabn.Count > 0)
            {
                id = mabn.First().MaBNhan;
                txtMaBNden.Text = mabn.First().MaBNhan.ToString();
            }
            idtu = id - _kcach;
            var mabntu = (from bn in _data.BenhNhans.Where(p=>p.MaBNhan==idtu) select bn).OrderByDescending(p => p.MaBNhan).ToList();
            if (mabntu.Count > 0)
                txtMaBNtu.Text = mabntu.First().MaBNhan.ToString() ;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}