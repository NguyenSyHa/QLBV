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
    public partial class frm_DuyetPhieuLinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_DuyetPhieuLinh()
        {
            InitializeComponent();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        public class PhieuLinh
        {
            private int SoPL;
            private DateTime NgayTao;
            private string MaKP;
            private string MaKXuat;
            private bool Duyet;
            public int sopl { set { SoPL = value; } get { return SoPL; } }
            public DateTime ngaytao { set { NgayTao = value; } get { return NgayTao; } }
            public string makp { set { MaKP = value; } get { return MaKP; } }
            public string makxuat { set { MaKXuat = value; } get { return MaKXuat; } }
            public bool duyet { set { Duyet = value; } get { return Duyet; } }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DuyetPhieuLinh_Load(object sender, EventArgs e)
        {
            dttungay.DateTime = DateTime.Now;
            dtdenngay.DateTime = DateTime.Now;
                var perm = _data.Permissions.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
                if (perm.Where(p => p.ID == 9049).ToList().Count > 0 && (perm.Where(p => p.ID == 9049).First().C_Edit == true || perm.Where(p => p.ID == 9049).First().C_New == true || perm.Where(p => p.ID == 9049).First().C_Delete == true))
                {
                    btnluu.Enabled = true;
                }
                else
                    btnluu.Visible = false;
            var _lkp = _data.KPhongs.ToList();
            var _lkp1 = _lkp.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng" || p.PLoai == "Cận lâm sàng").ToList();
            KPhong moi = new KPhong();
            moi.MaKP = 0;
            moi.TenKP = "Tất cả";
            _lkp1.Add(moi);
            lupkhoake.Properties.DataSource = _lkp1.OrderBy(p => p.MaKP).ToList();
            lupkhoake.EditValue = 0;

            var _lkp2 = _lkp.Where(p => p.PLoai == "Khoa dược" || p.PLoai == "Tủ trực").ToList();
            KPhong moi1 = new KPhong();
            moi1.MaKP = 0;
            moi1.TenKP = "Tất cả";
            _lkp2.Add(moi1);
            lupkhoxuat.Properties.DataSource = _lkp2.OrderBy(p=>p.MaKP).ToList();
            lupkhoxuat.EditValue = 0;

            cbotrangthai.SelectedIndex = 0;
        }
        List<PhieuLinh> _lphieulinh = new List<PhieuLinh>();
        void LoadDSPhieuLinh()
        {
            _lphieulinh.Clear();
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            DateTime _ngaytuke = DateTime.Now;

            _ngaytu = dttungay.DateTime.Date;
            _ngayden = dtdenngay.DateTime.Date;
            _ngaytuke = _ngaytu.AddMonths(-1);

            int makhoa = 0, makhox = 0, duyet = cbotrangthai.SelectedIndex, sopl = 0;
            if (lupkhoake.EditValue != null)
                makhoa = Convert.ToInt32(lupkhoake.EditValue);
            if (lupkhoxuat.EditValue != null)
                makhox = Convert.ToInt32(lupkhoxuat.EditValue);
            var _lkp = _data.KPhongs.ToList();
            var _lkhoxuat = _lkp.Where(p => p.PLoai == "Khoa dược" || p.PLoai == "Tủ trực").ToList();
            var _lkhoap = _lkp.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng" || p.PLoai == "Cận lâm sàng").ToList();
            if (txtSoPL.Text != "")
                sopl = Convert.ToInt32(txtSoPL.Text);
            var _ldspl = (from pl in _data.SoPLs.Where(p => p.PhanLoai == 1).Where(p => p.Status == cbotrangthai.SelectedIndex).Where(p => p.NgayNhap >= _ngaytu && p.NgayNhap <= _ngayden)
                          join dtct in _data.DThuoccts.Where(p => p.SoPL > 0).Where(p => p.MaKXuat != null && p.MaKP != null).Where(p => makhoa == 0 ? true : p.MaKP == makhoa).Where(p => makhox == 0 ? true : p.MaKXuat == makhox) on pl.SoPL1 equals dtct.SoPL
                          group new { pl, dtct } by new { pl.SoPL1, dtct.MaKP, dtct.MaKXuat, pl.NgayNhap } into kq
                          select new { kq.Key.SoPL1, kq.Key.MaKP, kq.Key.MaKXuat, kq.Key.NgayNhap }).OrderBy(p => p.NgayNhap).ThenBy(p => p.SoPL1).ToList();
            foreach (var item in _ldspl)
            {
                PhieuLinh moi = new PhieuLinh();
                moi.sopl = item.SoPL1;
                moi.ngaytao = Convert.ToDateTime(item.NgayNhap);
                moi.duyet = false;
                var tenkpke = _lkhoap.Where(p => p.MaKP == item.MaKP).Select(p => p.TenKP).FirstOrDefault();
                if (tenkpke != null)
                    moi.makp = tenkpke.ToString();
                else
                    moi.makp = "";
                var tenkpxuat = _lkhoxuat.Where(p => p.MaKP == item.MaKXuat).Select(p => p.TenKP).FirstOrDefault();
                if (tenkpxuat != null)
                    moi.makxuat = tenkpxuat.ToString();
                else
                    moi.makxuat = "";
                _lphieulinh.Add(moi);
            }
            grcDSPhieuLinh.DataSource = null;
            grcDSPhieuLinh.DataSource = _lphieulinh.Where(p => sopl != 0 ? p.sopl == sopl : true).ToList();
        }

        private void dttungay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
            //colduyet.ch


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
        }

        private void dtdenngay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
        }

        private void cbotrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
            if(cbotrangthai.SelectedIndex==2)
            {
                hybochon.Enabled = false;
                hychon.Enabled = false;
                btnluu.Enabled = false;
                btnluu.Text = "Lưu";
            }
            else
            {
                hybochon.Enabled = true;
                hychon.Enabled = true;
                btnluu.Enabled = true;
                if (cbotrangthai.SelectedIndex == 0)
                    btnluu.Text = "Duyệt";
                else
                    btnluu.Text = "Hủy Duyệt";
            }
        }

        private void txtSoPL_Leave(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
        }

        private void lupkhoake_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
        }

        private void lupkhoxuat_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSPhieuLinh();
        }

        private void hychon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            
            //if (cbotrangthai.SelectedIndex == 0)
            //{
                foreach (var item in _lphieulinh)
                {
                    item.duyet = true;
                }
                grcDSPhieuLinh.DataSource = null;
                grcDSPhieuLinh.DataSource = _lphieulinh;
            //}
            //grcDSPhieuLinh.DataSource=
        }

        private void hybochon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            //if (cbotrangthai.SelectedIndex == 1)
            //{
                foreach (var item in _lphieulinh)
                {
                    item.duyet = false;
                }
                grcDSPhieuLinh.DataSource = null;
                grcDSPhieuLinh.DataSource = _lphieulinh;
            //}
        }

        private void grvDSPhieuLinh_DoubleClick(object sender, EventArgs e)
        {
            int _sopl = 0;
            if (grvDSPhieuLinh.GetFocusedRowCellValue(colsopl) != null)
            {
                _sopl = Convert.ToInt32(grvDSPhieuLinh.GetFocusedRowCellValue(colsopl));
                FormNhap.frm_ChiTietPL frm = new FormNhap.frm_ChiTietPL(_sopl);
                frm.ShowDialog();
            }
        }

        public class _soPL
        {
            private int sopl;
            public int SoPL
            {
                set { sopl = value; }
                get { return sopl; }
            }
        }
        private void btnluu_Click(object sender, EventArgs e)
        {
            //_lphieulinh = _lphieulinh.Where(p => p.duyet = true).ToList();
            List<_soPL> _sopl = new List<_soPL>();
            for (int k = 0; k < grvDSPhieuLinh.RowCount; k++)
            {
                if (grvDSPhieuLinh.GetRowCellValue(k, colduyet).ToString().ToLower() == "true")
                {
                    _sopl.Add(new _soPL { SoPL = Convert.ToInt32(grvDSPhieuLinh.GetRowCellValue(k, colsopl)) });
                }
            }
            switch(btnluu.Text)
            {
                case "Duyệt":
                    foreach (var item in _sopl)
                    {
                        var lpl = _data.SoPLs.Where(p => p.SoPL1 == item.SoPL && p.PhanLoai == 1).FirstOrDefault();
                        if (lpl != null)
                            lpl.Status = 1;
                        
                    }
                    _data.SaveChanges();
                    break;
                case "Hủy Duyệt":
                    foreach (var item in _sopl)
                    {
                        var lpl = _data.SoPLs.Where(p => p.SoPL1 == item.SoPL && p.PhanLoai == 1).FirstOrDefault();
                        if (lpl != null)
                            lpl.Status = 0;
                        
                    }
                    _data.SaveChanges();
                    break;
            }
            LoadDSPhieuLinh();
        }

        private void grvDSPhieuLinh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void grvDSPhieuLinh_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if(e.Column.Name=="colduyet")
            //{

            //}
        }

        private void grvDSPhieuLinh_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int _sopl = grvDSPhieuLinh.GetFocusedRowCellValue(colsopl) == null ? 0 : Convert.ToInt32(grvDSPhieuLinh.GetFocusedRowCellValue(colsopl));
            if(_lphieulinh.Where(p=>p.sopl==_sopl).Count()>0)
            {
                PhieuLinh chon = _lphieulinh.Where(p => p.sopl == _sopl).First();
                if (e.Column == colduyet)
                {
                    if (chon.duyet == false)
                        chon.duyet = true;
                    else
                        chon.duyet = false;

                }
            }
        }
    }
}