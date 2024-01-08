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
    public partial class frm_UpdateChiPhiDV : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateChiPhiDV()
        {
            InitializeComponent();
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        bool _ktmatkhau = true;
        private void simpleButton43_Click(object sender, EventArgs e)
        {


            if (DungChung.Bien.PLoaiKP != "Admin")
            {
                _ktmatkhau = false;
                MessageBox.Show("Tài khoản không hợp lệ");
            }
            if (_ktmatkhau)
            {
                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                frm.ShowDialog();
            }
            if (_ktmatkhau)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
                var cpdv = data.BenhNhans.Where(p => p.DTNT == null).ToList();
                foreach (var item in cpdv)
                {
                    item.DTNT = false;
                    data.SaveChanges();
                }
                var dtnt = (from cp in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false)
                            join vv in data.VaoViens on cp.MaBNhan equals vv.MaBNhan
                            select new { cp.MaBNhan, vv.NgayVao }).ToList();
                foreach (var item in dtnt)
                {
                    BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == item.MaBNhan).First();
                    bn.DTNT = true;
                    data.SaveChanges();
                }


           //     var vp = data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).ToList();
                var vp2 = (from vphi in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                         //  where !(from cp in data.ChiPhiDVs.Where(p => p.Status == true) select cp.idVPhi).Contains(vphi.idVPhi)
                           select new { vphi.idVPhi }).ToList();
                int _dem = 0, kothuchien = 0, tong = vp2.Count;
                int labdem = 0;
                //foreach (var item in vp2)
                //{
                //    //labdem++;
                //    //labeldem.Text = labeldem.ToString();
                //    if (DungChung.Ham.updateChiPhiDV(data, item.idVPhi))
                //        _dem++;
                //    else
                //        kothuchien++;
                //}
                MessageBox.Show("Thực hiện thành công: " + _dem + "/" + tong + " bệnh nhân, đã tồn tại: " + kothuchien + " bệnh nhân");
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void lupngayden_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frm_UpdateChiPhiDV_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupngayden.DateTime = System.DateTime.Now;
        }
    }
}