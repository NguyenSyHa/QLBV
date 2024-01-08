using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;
using QLBV.DungChung;
using OpenXmlPackaging;
using System.IO;

namespace QLBV.FormThamSo
{

    public partial class frm_THSoLuotXN_27183 : DevExpress.XtraEditors.XtraForm
    {
        public frm_THSoLuotXN_27183()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (kt())
            {
                List<int> _lKhoaPhongcheck = new List<int>();
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemChecked(i))
                    {
                        _lKhoaPhongcheck.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                    }
                }
                int id_DoiTuongKham = 99;                                         // tất cả
                if (lupDoituong.EditValue != null)
                    id_DoiTuongKham = Convert.ToInt32(lupDoituong.EditValue);
                int noingoaitru = cboNoiNgoaiTru.SelectedIndex; // =2 tất cả
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                if (DungChung.Bien.MaBV == "30005")
                {
                    var q1 = (from bn in _db.BenhNhans.Where(p => id_DoiTuongKham == 99 ? true : p.IDDTBN == id_DoiTuongKham).Where(p => noingoaitru == 2 ? true : p.NoiTru == noingoaitru)
                              join cls in _db.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden) on bn.MaBNhan equals cls.MaBNhan
                              join cd in _db.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              join kp in _db.KPhongs on cls.MaKP equals kp.MaKP
                              select new { cls.NgayTH, bn.TenBNhan, bn.MaBNhan, cls.MaKP, cls.NgayThang, cd.MaDV, kp.TenKP}).ToList();
                    var q2 = (from dv in _db.DichVus
                              join tn in _db.TieuNhomDVs.Where(p => p.IDNhom == 1) on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { dv.MaDV, dv.TenDV, tn.TenRG }).ToList();
                    var q3 = (from a in q1
                              join b in q2 on a.MaDV equals b.MaDV
                              join c in _lKhoaPhongcheck on a.MaKP equals c
                              select new { a.NgayTH,a.MaBNhan, a.TenBNhan, TenNhom = b.TenRG, TenDV = b.TenDV, NgayCD = a.NgayThang.Value.Date, a.TenKP ,a.MaKP}).OrderBy(p => p.NgayTH).ToList();
                    var q4 = (from a in q3
                              group a by new { a.MaBNhan, a.TenBNhan, a.NgayCD, a.TenKP, a.MaKP } into kq
                              select new { kq.Key.TenBNhan, kq.Key.MaBNhan, TenNhom = string.Join("\n", kq.Select(p => p.TenNhom).Distinct().ToArray()), kq.Key.TenKP, kq.Key.MaKP, kq.Key.NgayCD, TenDV = string.Join("\n", kq.Select(p => p.TenDV).ToArray()) }).OrderBy(p => p.NgayCD).ToList();
                    BaoCao.rep_THSoLuotXN_30005 rep = new BaoCao.rep_THSoLuotXN_30005();
                    rep.labtitle.Text = "Từ ngày " + ngaytu.ToShortDateString() + " đến ngày " + ngayden.ToShortDateString();
                    rep.DataSource = q4;
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var q1 = (from bn in _db.BenhNhans.Where(p => id_DoiTuongKham == 99 ? true : p.IDDTBN == id_DoiTuongKham).Where(p => noingoaitru == 2 ? true : p.NoiTru == noingoaitru)
                              join cls in _db.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden) on bn.MaBNhan equals cls.MaBNhan
                              join cd in _db.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              select new { cls.NgayTH, bn.TenBNhan, bn.MaBNhan, cls.MaKP, cd.MaDV }).ToList();
                    var q2 = (from dv in _db.DichVus
                              join tn in _db.TieuNhomDVs.Where(p => p.IDNhom == 1) on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { dv.MaDV, dv.TenDV, tn.TenRG }).ToList();
                    var q3 = (from a in q1
                              join b in q2 on a.MaDV equals b.MaDV
                              join c in _lKhoaPhongcheck on a.MaKP equals c
                              group new { a, b } by new { a.TenBNhan, a.MaBNhan } into kq
                              select new { NgayTH = kq.Select(p => p.a.NgayTH).Min(), kq.Key.TenBNhan, TenNhom = string.Join("\n", kq.Select(p => p.b.TenRG).Distinct().ToArray()), TenDV = string.Join("; ", kq.Select(p => p.b.TenDV).Distinct().ToArray()) }).OrderBy(p => p.NgayTH).ToList();
                    BaoCao.rep_THSoLuotXN_27183 rep = new BaoCao.rep_THSoLuotXN_27183();
                    rep.labtitle.Text = "Từ ngày " + ngaytu.ToShortDateString() + " đến ngày " + ngayden.ToShortDateString();
                    rep.DataSource = q3;
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }                                                                                   
            }
        }



        List<KPhong> _lKphong = new List<KPhong>();
        List<DichVu> _ldv = new List<DichVu>();
        private int _hangbv = -1;
        List<khoaphongcheck> _lKPsd = new List<khoaphongcheck>();
        public class khoaphongcheck
        {
            public string TenKP { set; get; }
            public int MaKP { set; get; }
            public bool Check { set; get; }
        }
        private void frm_80ct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldv = _dataContext.DichVus.ToList();
            _lKphong = _dataContext.KPhongs.ToList();
            _lKPsd = (from kp in _lKphong.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                      select new khoaphongcheck()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            _lKPsd.Insert(0, new khoaphongcheck { MaKP = 0, TenKP = "Tất cả", Check = false, });
            cklKP.DataSource = _lKPsd;
            cklKP.CheckAll();
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = _dataContext.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { Status = 1, IDDTBN = 99, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = 99;

        }

        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupngayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupngayden.Focus();
                return false;
            }
            else if ((lupngayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupngayden.Focus();
                return false;
            }
            return true;
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKP.GetItemChecked(0) == true)
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }


    }
}
