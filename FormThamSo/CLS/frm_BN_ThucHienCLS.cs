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

    public partial class frm_BN_ThucHienCLS : DevExpress.XtraEditors.XtraForm
    {
        public frm_BN_ThucHienCLS()
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

                var q0 = (from bn in _db.BenhNhans.Where(p => id_DoiTuongKham == 99 ? true : p.IDDTBN == id_DoiTuongKham).Where(p => noingoaitru == 2 ? true : p.NoiTru == noingoaitru)
                          join bnkb in _db.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                          select new { bn.MaBNhan, bn.TenBNhan, bnkb.ChanDoan, bnkb.BenhKhac });
                var q1 = (from bn in q0
                          join cls in _db.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden && p.Status == 1) on bn.MaBNhan equals cls.MaBNhan
                          join cd in _db.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                          select new { cls.NgayTH, bn.TenBNhan, bn.MaBNhan, cls.MaKP, cd.MaDV, bn.BenhKhac, bn.ChanDoan });
                var q2 = (from dv in _db.DichVus
                          join tn in _db.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          select new { dv.MaDV, dv.TenDV, tn.TenRG });
                var q3 = (from a in q1.Where(o => _lKhoaPhongcheck.Contains(o.MaKP ?? 0))
                          join b in q2 on a.MaDV equals b.MaDV
                          group new { a, b } by new { a.TenBNhan, a.MaBNhan } into kq
                          select new { MaBNhan = kq.Key.MaBNhan, TenBNhan = kq.Key.TenBNhan, ListTenDV = kq.Select(p => p.b.TenDV).Distinct(), kq.FirstOrDefault().a.ChanDoan, kq.FirstOrDefault().a.BenhKhac }).ToList();
                var q4 = (from q in q3
                          select new BN_CLS {MaBenhNhan = q.MaBNhan, ChanDoan = string.Format("{0}; {1}", q.ChanDoan, q.BenhKhac), TenBNhan = q.TenBNhan, TenDV = string.Join("<br>", q.ListTenDV.ToList()) }).ToList();

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("TenSo", "BẢNG TỔNG HỢP BỆNH NHÂN THỰC HIỆN CẬN LÂM SÀNG");
                dic.Add("TuNgay", ngaytu.Date.ToString("dd/MM/yyyy"));
                dic.Add("DenNgay", ngayden.Date.ToString("dd/MM/yyyy"));
                dic.Add("TuNgayDenNgay", string.Format("Từ ngày {0} đến ngày {1}", ngaytu.Date.ToString("dd/MM/yyyy"), ngayden.Date.ToString("dd/MM/yyyy")));

                DungChung.Ham.Print(DungChung.PrintConfig.rep_BC_DS_BN_ThucHienCLS_ID549, q4, dic, false);
            }
        }

        public class BN_CLS
        {
            public int MaBenhNhan { get; set; }
            public string TenBNhan { get; set; }
            public string TenDV { get; set; }
            public string ChanDoan { get; set; }
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
            _lKPsd = (from kp in _lKphong
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
