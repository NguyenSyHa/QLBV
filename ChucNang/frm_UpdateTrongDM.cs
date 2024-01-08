using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV_Database.Common;

namespace QLBV.ChucNang
{
    public partial class frm_UpdateTrongDM : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateTrongDM()
        {
            InitializeComponent();
        }
        int _madv = 0, _mabn = 0; int _trongBH = -1; int _mien = 0; int _xahoihoa = 0; int _tyleTT = 0, _makp;
        public frm_UpdateTrongDM(int mabn, int tendv, int trongBH, int mien, int xahoihoa, int tyleTT,int makp)
        {
            InitializeComponent();
            this._mabn = mabn;
            this._trongBH = trongBH;
            this._madv = tendv;
            this._xahoihoa = xahoihoa;
            this._mien = mien;
            this._tyleTT = tyleTT;
            this._makp = makp;
        }
        public frm_UpdateTrongDM(int mabn, int tendv, int trongBH, int mien, int makp)
        {
            InitializeComponent();
            this._mabn = mabn;
            this._trongBH = trongBH;
            this._madv = tendv;
            this._mien = mien;
            this._makp = makp;
        }
        QLBVEntities _data = EntityDbContext.DbContext;
        int dem = 0;
        private void frm_UpdateTrongDM_Load(object sender, EventArgs e)
        {
            var dichvu = _data.DichVus.Where(p => p.MaDV == _madv).ToList();
            if (dichvu.Count > 0)
                memoTenDV.Text = dichvu.First().TenDV;
            textEdit1.Text = _mien.ToString();
            if (DungChung.Bien.MaBV.Substring(0, 2) != "24")
                ckXHH.Visible = false;
            //radTrongBH.SelectedIndex = _trongBH;

            if (_xahoihoa == 1)
                ckXHH.Checked = true;
            dem++;

            var qdtct = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                         join bn in _data.BenhNhans.Where(p => p.DTuong == "Dịch vụ") on dt.MaBNhan equals bn.MaBNhan
                         join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv && p.TrongBH == _trongBH) on dt.IDDon equals dtct.IDDon
                         //join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                         //where dv.TrongDM == 1
                         select dtct).FirstOrDefault();


            radTrongBH.Properties.Items.Clear();
            this.radTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Ngoài DM")});
            this.radTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Không Thanh Toán")});
            if (qdtct == null && dichvu.First().TrongDM == 1)
            {
                this.radTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Trong DM")});
            }
            else if (DungChung.Bien.MaBV == "30010" && _trongBH == 2)
            {
                this.radTrongBH.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Trong DM")});
            }
            radTrongBH.EditValue = _trongBH;

            //radTrongBH.Enabled = false;
            //}
        }

        private void radTrongBH_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (dem > 0)
            //{
            //    DialogResult _resul = MessageBox.Show("Bạn chắc chắn muốn thay đổi hình thức thanh toán cho chi phí này?", "Hỏi thay đổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (_resul == DialogResult.No)
            //        e.Cancel = false;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!DungChung.Ham.KTraTT(_data, _mabn))
            {
                var dv = _data.DichVus.ToList();
                List<DThuocct> thaydoi = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                          join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.MaKP == _makp).Where(p => p.ThanhToan != 1) on dt.IDDon equals dtct.IDDon
                                          select dtct).ToList();
                foreach (var a in thaydoi)
                {
                    double dongia = 0;
                    //if (radTrongBH.SelectedIndex == 1)
                    //{
                    //    if (DungChung.Ham.GiaCu(_mabn,radTrongBH.SelectedIndex,  a.NgayNhap ?? DateTime.Now))
                    //        dongia = dv.Where(p => p.MaDV == a.MaDV).Select(p => p.DonGia).FirstOrDefault();
                    //    else
                    //        dongia = dv.Where(p => p.MaDV == a.MaDV).Select(p => p.DonGiaBHYT).FirstOrDefault();
                    //}
                    //else if (radTrongBH.SelectedIndex == 0)
                    //    dongia = dv.Where(p => p.MaDV == a.MaDV).Select(p => p.DonGia2).FirstOrDefault();
                    //else
                    //    dongia = a.DonGia;
                    DateTime ngaynhap = DateTime.Now;
                    if(a.NgayNhap != null)
                        ngaynhap = a.NgayNhap.Value;
                    int trongdm = Convert.ToInt32(radTrongBH.EditValue);
                    dongia = DungChung.Ham._getGiaDM(_data, a.MaDV ?? 0, trongdm, _mabn, ngaynhap);
                    a.TrongBH = trongdm;
                    a.Mien = Convert.ToInt32(textEdit1.Text);
                    a.XHH = ckXHH.Checked ? 1 : 0;
                    a.TyLeTT = _tyleTT == 0 ? 100 : _tyleTT;
                    //var ktdv = dv.Where(p => p.MaDV == a.MaDV).Where(p => p.PLoai == 2).ToList();
                    //if (ktdv.Count > 0)
                    //{
                    //    a.DonGia = dongia;
                    //    a.ThanhTien = Math.Round(a.SoLuong * dongia, 3);
                    //}

                    var ktdv = dv.Where(p => p.MaDV == a.MaDV).Where(p => p.PLoai == 2).ToList();
                    if (ktdv.Count > 0)
                        a.DonGia = dongia;
                    a.ThanhTien =Math.Round( a.DonGia * a.SoLuong * _tyleTT * (double)(100 - a.Mien) / 10000,3);
                    _data.SaveChanges();
                }
                MessageBox.Show("Sửa thành công");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể sửa");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckXHH_CheckedChanged(object sender, EventArgs e)
        {
            if (ckXHH.Checked)
            {
                DialogResult _resul = MessageBox.Show("Bạn chắc chắn dịch vụ này là dịch vụ xã hội hóa?", "Hỏi thay đổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_resul == DialogResult.No)
                {
                    if (_xahoihoa == 1)
                        ckXHH.Checked = true;
                }
            }
        }
    }
}