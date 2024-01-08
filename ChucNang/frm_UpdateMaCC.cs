using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.ChucNang
{
    public partial class frm_UpdateMaCC : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateMaCC()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Duoc> _lDuoc = new List<Duoc>();
        private void TimKiem( int tt) {
          
            if (tt == 0)
            {
                var q = (from nd in _data.NhapDs.Where(p => p.PLoai == 1)
                         join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
                         where !(from  dv1 in _data.DichVus.Where(p=>p.MaDV== (ndct.MaDV)) select dv1.MaCC).Contains(ndct.MaCC)
                         group ndct by new { ndct.MaCC, ndct.MaDV, dv.TenDV } into kq
                         select new { kq.Key.MaCC, kq.Key.MaDV, kq.Key.TenDV }).ToList();
                foreach (var a in q) {
                    Duoc moi = new Duoc();
                    moi.MaDV = a.MaDV == null ? 0 : Convert.ToInt32(a.MaDV);
                    moi.MaCC = a.MaCC;
                    _lDuoc.Add(moi);
                }
                gridControl1.DataSource = q.ToList();
            }
            else {
                var q = (from nd in _data.NhapDs.Where(p => p.PLoai == 1)
                         join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
                         join dv1 in _data.DichVus.Where(p=>p.MaCC.Length>0 ) on  ndct.MaCC equals dv1.MaCC
                         group ndct by new { ndct.MaCC, ndct.MaDV, dv.TenDV } into kq
                         select new { kq.Key.MaCC, kq.Key.MaDV, kq.Key.TenDV }).ToList();
                gridControl1.DataSource = q.ToList();
            }
        }
        private void frm_UpdateMaCC_Load(object sender, EventArgs e)
        {
            lupTenCC.DataSource = _data.NhaCCs.ToList();
            lupMaCC.Properties.DataSource = _data.NhaCCs.ToList();
            TimKiem(radioGroup1.SelectedIndex);
        }
        private class Duoc{
            public string macc;
            public int madv;
            public string MaCC {
                set {macc=value; }
                get{return macc;}
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem(radioGroup1.SelectedIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DichVu> _ldv = new List<DichVu>();
            _ldv = _data.DichVus.ToList();
            foreach (var a in _lDuoc) {
                int tt = 0;
                foreach (var b in _ldv) {
                    if (a.MaDV == b.MaDV) {
                        tt++;
                        if (a.MaCC != b.MaCC) {
                            if (string.IsNullOrEmpty(b.MaCC))
                            {
                                var sua = _data.DichVus.Single(p => p.MaDV== (a.MaDV));
                                sua.MaCC = a.MaCC;
                                _data.SaveChanges();
                            }
                        }
                        break;
                    }
                }
                tt = 0;
            }
            MessageBox.Show("Update thành công");
            btnOK.Enabled = false;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maDv_moi = 0;
            if (Int32.TryParse(txtMaDuocMoi.Text.Trim(), out rs))
            {
                _int_maDv_moi = Convert.ToInt32(txtMaDuocMoi.Text.Trim());
                if (radioGroup1.SelectedIndex == 0 && !string.IsNullOrEmpty(txtMaDuocMoi.Text))
                {
                    if (lupMaCC.EditValue != null && lupMaCC.EditValue.ToString() != "" && !String.IsNullOrEmpty(txtMaDV.Text))
                    {
                        var b = _data.DichVus.Where(p => p.MaDV ==  Convert.ToInt32(txtMaDV.Text)).ToList();
                        if (b.Count > 0)
                        {
                            string _macc = "";
                            
                            _macc = lupMaCC.EditValue.ToString();
                            DichVu moi = new DichVu();
                            moi.MaDV = _int_maDv_moi;
                            moi.MaCC = lupMaCC.EditValue.ToString();
                            moi.TenDV = b.First().TenDV;
                            moi.DonVi = b.First().DonVi;
                            moi.DonGia = 0;
                            moi.DuongD = b.First().DuongD;
                            moi.DVKTC = b.First().DVKTC;
                            moi.HamLuong = b.First().HamLuong;
                            moi.IDNhom = b.First().IDNhom;
                            moi.IdTieuNhom = b.First().IdTieuNhom;
                            moi.Loai = b.First().Loai;
                            moi.PLoai = b.First().PLoai;
                            moi.PhuongPhap = b.First().PhuongPhap;
                            moi.QCPC = b.First().QCPC;
                            moi.SoDK = b.First().SoDK;
                            moi.Status = b.First().Status;
                            moi.TenHC = b.First().TenHC;
                            moi.TenRG = b.First().TenRG;
                            moi.TrongDM = b.First().TrongDM;
                            moi.YCSD = b.First().YCSD;
                            _data.DichVus.Add(moi);
                            _data.SaveChanges();
                            var id = _data.NhapDcts.Where(p => p.MaDV ==  Convert.ToInt32(txtMaDV.Text)).Where(p => p.MaCC== (_macc)).Select(p => p.IDNhapct).ToList();
                            foreach (var a in id)
                            {
                                var sua = _data.NhapDcts.Single(p => p.IDNhapct == a);
                                sua.MaDV = _int_maDv_moi;
                                _data.SaveChanges();
                            }
                            var iddon = (from dt in _data.DThuocs join dtct in _data.DThuoccts.Where(p => p.Status != 1).Where(p => p.Status != 2).Where(p => p.Status != 3).Where(p => p.MaDV == Convert.ToInt32(txtMaDV.Text)).Where(p => p.MaCC == (_macc)) on dt.IDDon equals dtct.IDDon select dtct.IDDonct).ToList();
                            foreach (var c in iddon)
                            {
                                var sua = _data.DThuoccts.Single(p => p.IDDonct == c);
                                sua.MaDV = _int_maDv_moi;
                                _data.SaveChanges();
                            }
                            frm_UpdateMaCC_Load(sender, e);
                            MessageBox.Show("Thêm thành công");

                            // cach 2

                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Ma dich vu moi phai la kieu so");
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaDV) != null )
            {
                txtMaDV.Text = gridView1.GetFocusedRowCellValue(colMaDV).ToString();
                if (gridView1.GetFocusedRowCellValue(colTenDV) != null && gridView1.GetFocusedRowCellValue(colTenDV).ToString() != "")
                    txtTenDV.Text = gridView1.GetFocusedRowCellValue(colTenDV).ToString();
                else
                    txtTenDV.Text = "";
                if (gridView1.GetFocusedRowCellValue(colMaCC) != null && gridView1.GetFocusedRowCellValue(colMaCC).ToString() != "")
                    lupMaCC.EditValue = gridView1.GetFocusedRowCellValue(colMaCC).ToString();
                else
                    lupMaCC.EditValue = "";
            }
            else {
                txtMaDV.Text = "";
                txtTenDV.Text = "";
                lupMaCC.EditValue = "";
            } 
        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1_FocusedRowChanged(null,null);
        }

        private void txtMaDuocMoi_Leave(object sender, EventArgs e)
        {
            int rs;
            int _int_MaDVMoi = 0;
            if (Int32.TryParse(txtMaDuocMoi.Text, out rs))
                _int_MaDVMoi = Convert.ToInt32(txtMaDuocMoi.Text);
            var b = _data.DichVus.Where(p => p.MaDV == _int_MaDVMoi).ToList();
            if (b.Count > 0)
            {
                MessageBox.Show("Mã dược đã tồn tại. Vui lòng chọn mã khác");
                btnThemMoi.Enabled = false;
            }
            else {
                btnThemMoi.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //this.Dispose();
            //FormNhap.frm_catkhoangtrang frm = new FormNhap.frm_catkhoangtrang();
            //frm.ShowDialog();
        }

       
    }
}