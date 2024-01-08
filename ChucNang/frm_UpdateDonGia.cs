using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.SqlClient;

namespace QLBV.FormNhap
{
    public partial class frm_UpdateDonGia : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateDonGia()
        {
            InitializeComponent();
        }
        int _sopl = 19431;
        public frm_UpdateDonGia(int spl)
        {
            InitializeComponent();
            this._sopl = spl;
        }
        public void dongiadung(int mak, int mad)
        {
            var gia1 = ((from gia in _data.NhapDcts join nd in _data.NhapDs on gia.IDNhap equals nd.IDNhap where (gia.MaDV == mad && nd.MaKP == mak) group new { gia, nd } by new { gia.MaDV, gia.DonGia, nd.MaKP } into hk select new { hk.Key.DonGia, soton = hk.Sum((p => p.gia.SoLuongN - p.gia.SoLuongX)) }).Where(p => p.soton > 0)).ToList();
            lookUpEdit1.Properties.DataSource = gia1;
        }
        public void hamgoidonthuoc(int spl)
        {

            var dt_ct1 = from dt in _data.DThuocs
                         join dt_ct in _data.DThuoccts on dt.IDDon equals dt_ct.IDDon
                         join dv in _data.DichVus on dt_ct.MaDV equals dv.MaDV
                         where (dt_ct.SoPL == spl)
                         select new
                             {
                                 dt_ct.IDDonct,
                                 dt_ct.MaDV,
                                 dt_ct,
                                 dv.TenDV,
                                 dt_ct.DonVi,
                                 dt_ct.DonGia,
                                 dt_ct.SoLuong
                             };

            // hiện dơn thuốc chi tiết 
            var dt_ct2 = (from dt_ct21 in dt_ct1
                          group
                              dt_ct21 by new { dt_ct21.MaDV, dt_ct21.DonVi, dt_ct21.DonGia, dt_ct21.TenDV }
                              into h
                              select new { h.Key.MaDV, h.Key.TenDV, h.Key.DonVi, h.Key.DonGia, SoLuong = h.Sum(z => z.SoLuong) }).ToList();


            var kho = (from k in _data.KPhongs 
                       join d in _data.DThuocs on k.MaKP equals d.MaKXuat 
                       join dtct in _data.DThuoccts on d.IDDon equals dtct.IDDon
                       where dtct.SoPL == spl 
                       group k by new { k.TenKP, k.MaKP } into j select new { j.Key.TenKP, j.Key.MaKP }).ToList();
            var k4 = (from hk in kho group hk by new { hk.MaKP } into gg select gg.Key.MaKP).ToList();
            int kk = k4.First()== null? 0: k4.First();
            // hiện thuốc don giá khồng còn trong trong kho
            var tonthuocupdate = (from th in _data.NhapDs join th1 in _data.NhapDcts on th.IDNhap equals th1.IDNhap where (th.MaKP == kk) group new { th, th1 } by new { th.MaKP, th1.MaDV, th1.DonGia } into h select new { h.Key.DonGia, h.Key.MaDV, ton = h.Sum((p => p.th1.SoLuongN - p.th1.SoLuongX))}).Where(p => p.ton > 0);
            //
            var dthuocctupdate = (from th in dt_ct2 from th1 in tonthuocupdate where (th.MaDV == th1.MaDV && th.DonGia != th1.DonGia) select new { th.MaDV, th.TenDV, th.DonVi, th.DonGia, th.SoLuong, ThanhTien = th.DonGia * th.SoLuong}).ToList();
            lou_kho.Properties.DataSource = kho;
            lou_kho.EditValue = kk;
            gct_dthuocct.DataSource = dthuocctupdate;
            tex_spl.Text = spl.ToString();
        }
        private void frm_UpdateDonGia_Load(object sender, EventArgs e)
        {
            hamgoidonthuoc(_sopl);

        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void simpleButton1_Click(object sender, EventArgs e)
        {


        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //// insert
            //KPhong moi = new KPhong();
            //moi.MaKP = "5454";
            //moi.TenKP = "5";
            //_data.KPhongs.Add(moi);
            //_data.SaveChanges();
            //// update
            //var sua = _data.KPhongs.Single(p => p.MaKP == "5454");
            //var suadg = from dv in _data.DThuoccts where dv.MaDV == "sdsd" && dv.DonGia == 2123 select dv;
            //foreach (var a in suadg)
            //{
            //    a.DonGia = 545454;
            //    _data.SaveChanges();
            //}
            //sua.TenKP = "6";
            //_data.SaveChanges();
            //// delete
            //var xoa = _data.KPhongs.Single(p => p.MaKP == "5454");
            //_data.Remove(xoa);
            //_data.SaveChanges();

        }

        private void lou_KhoD_EditValueChanged(object sender, EventArgs e)
        {
            //XtraMessageBox.Show(lou_KhoD.EditValue.ToString());
            //string a = lou_KhoD.EditValue.ToString();
            //hamgoidonthuoc(a);

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            tex_madv.Text = gridView1.GetFocusedRowCellDisplayText(maduoc).ToString();
            tex_tendv.Text = gridView1.GetFocusedRowCellDisplayText(TenDV).ToString();
            if (lou_kho.EditValue != null)
            {
                {
                    //MessageBox.Show(lou_kho.EditValue.ToString());
                    dongiadung(lou_kho.EditValue== null ? 0 : Convert.ToInt32(lou_kho.EditValue), String.IsNullOrEmpty(tex_madv.Text) ? 0 : Convert.ToInt32(tex_madv.Text));
                   
                }
            }

        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1_FocusedRowChanged(null, null);
        }
        //update dươc
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (lookUpEdit1.Text != "" && lookUpEdit1.Text != null)
            {
                var dothuoc = (from h in _data.DThuoccts 
                               join h1 in _data.DThuocs on h.IDDon equals h1.IDDon 
                               where (h.MaDV == (String.IsNullOrEmpty(tex_madv.Text) ? 0 : Convert.ToInt32(tex_madv.Text)) && h.SoPL == _sopl) select h).ToList();
                foreach (var update in dothuoc)
                {
                    update.DonGia = float.Parse(lookUpEdit1.Text);
                    update.ThanhTien = update.SoLuong * float.Parse(lookUpEdit1.Text);
                    _data.SaveChanges();
                }
                XtraMessageBox.Show("Update thành công", "Thông Báo");
                frm_UpdateDonGia_Load(sender, e);
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            tex_ton.Text = "Số lướng Tồn:" + lookUpEdit1.EditValue.ToString();
        }
    }
}