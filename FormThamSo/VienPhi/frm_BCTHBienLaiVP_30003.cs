using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BCTHBienLaiVP_30003 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTHBienLaiVP_30003()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lKPhong = new List<KPhong>();
        public class MyObject
        {
            public int Value { set; get; }
        }
        private void frm_BCTHBienLaiVP_30003_Load(object sender, EventArgs e)
        {
            //load ds năm
            int namHT = DateTime.Now.Year;
            List<MyObject> _list = new List<MyObject>();
            List<MyObject> _listthang = new List<MyObject>();
            for (int i = namHT - 10; i < namHT + 11; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }

            for (int i = 1; i < 13; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _listthang.Add(obj);
            }
            cbNam.DisplayMember = "Value";
            cbNam.ValueMember = "Value";
            cbNam.DataSource = _list;
            cbNam.SelectedValue = namHT;

            cboThang.DisplayMember = "Value";
            cboThang.ValueMember = "Value";
            cboThang.DataSource = _listthang;
            cboThang.SelectedValue = DateTime.Now.Month - 1;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            rdTrongNgoaiBH.SelectedIndex = 0;

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = new DateTime(Convert.ToInt32(cbNam.Text), Convert.ToInt32(cboThang.Text), 1);
            DateTime denngay = DungChung.Ham.NgayDen(tungay.AddMonths(1).AddDays(-1));
            int doituong = 100;

            int khoaphong = 0;
            int trongbh = -1;

            trongbh = rdTrongNgoaiBH.SelectedIndex - 1;

            var q = (from tuct in data.TamUngcts.Where(p => (trongbh == -1 || p.TrongBH == trongbh))
                     join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3) on tuct.IDTamUng equals tu.IDTamUng
                     join dv in data.DichVus.Where(p => p.PLoai == 2) on tuct.MaDV equals dv.MaDV
                     join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                     select new
                     {
                         tu.QuyenHD,
                         tuct.TienBN,
                         tn.TenRG,
                         dv.Loai
                     }).ToList();
            var q1 = (from a in q
                      select new
                          {
                              a.QuyenHD,
                              XQ1 = (a.TenRG == "X-Quang" && a.Loai == 1) ? a.TienBN : 0,
                              XQ2 = (a.TenRG == "X-Quang" && a.Loai == 2) ? a.TienBN : 0,
                              XQ3 = (a.TenRG == "X-Quang" && a.Loai == 3) ? a.TienBN : 0,
                              XQ0 = (a.TenRG == "X-Quang (CT)") ? a.TienBN : 0,
                              SA = (a.TenRG.Contains("Siêu âm")) ? a.TienBN : 0,
                          }).ToList();
            var q2 = (from a in q1 group a by a.QuyenHD into kq select new { QuyenHD = kq.Key, XQ0 = kq.Sum(p => p.XQ0), XQ1 = kq.Sum(p => p.XQ1), XQ2 = kq.Sum(p => p.XQ2), XQ3 = kq.Sum(p => p.XQ3), SA = kq.Sum(p => p.SA), Cong = kq.Sum(p => p.XQ0 + p.XQ1 + p.XQ2 + p.XQ3 + p.SA) }).Where(p => p.Cong != 0).OrderBy(p => p.QuyenHD).ToList();
            if (q2.Count > 0)
            {
                frmIn frm = new frmIn();
                BaoCao.rep_BCTHBienLaiVP_30003 rep = new BaoCao.rep_BCTHBienLaiVP_30003();
                rep.cel_thang.Text = "Nguồn xã hội hóa - tháng " + cboThang.Text + " năm " + cbNam.Text;
                rep.DataSource = q2;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Không có dữ liệu");
        }


    }
}