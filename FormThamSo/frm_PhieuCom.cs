using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
namespace QLBV.FormThamSo
{
    public partial class frm_PhieuCom : DevExpress.XtraEditors.XtraForm
    {
        public frm_PhieuCom()
        {
            InitializeComponent();
        }
        class baocom
        {
            bool chek;

            public bool Chek
            {
                get { return chek; }
                set { chek = value; }
            }
            int maBNhan;

            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            int iDChamCom;

            public int IDChamCom
            {
                get { return iDChamCom; }
                set { iDChamCom = value; }
            }
            string xuat;

            public string Xuat
            {
                get { return xuat; }
                set { xuat = value; }
            }
            private int _soLuong;

            public int SoLuong
            {
                get { return _soLuong; }
                set { _soLuong = value; }
            }


            string tenBNhan, dChi;

            public string DChi
            {
                get { return dChi; }
                set { dChi = value; }
            }

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            private Double? donGia;

            public Double? DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private Double thanhTien;

            public Double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }

            public double? X1 { get; set; }
            public double? X2 { get; set; }
            public double? X3 { get; set; }
            public double? X4 { get; set; }
            public double? X5 { get; set; }
            public double? X6 { get; set; }
            public double? X7 { get; set; }
            public double? X8 { get; set; }
            public double? X9 { get; set; }
            public double? X10 { get; set; }
            public double? X11 { get; set; }
            public double? X12 { get; set; }
            public double? X13 { get; set; }
            public double? X14 { get; set; }
            public double? X15 { get; set; }
            public double? X16 { get; set; }
            public double? X17 { get; set; }
            public double? X18 { get; set; }
            public double? X19 { get; set; }
            public double? X20 { get; set; }
            public double? X21 { get; set; }
            public double? X22 { get; set; }
            public double? X23 { get; set; }
            public double? X24 { get; set; }
            public double? X25 { get; set; }
            public double? X26 { get; set; }
            public double? X27 { get; set; }
            public double? X28 { get; set; }
            public double? X29 { get; set; }
            public double? X30 { get; set; }
            public double? X31 { get; set; }
            public int SoNgay { get; set; }
            public double? SoXuat { get; set; }

        }
        List<baocom> _listbc = new List<baocom>();
        List<baocom> _listbc1 = new List<baocom>();
        List<baocom> _listbc2 = new List<baocom>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_PhieuCom_Load(object sender, EventArgs e)
        {
            lup_Ngay.DateTime = DateTime.Now;
            var kp = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            lup_khoa.Properties.DataSource = kp;
            List<DTuong> madt = new List<DTuong>();
            DTuong moi = new DTuong();
            moi.MaDTuong = "Tất cả";
            madt.Add(moi);
            DTuong moi1 = new DTuong();
            moi1.MaDTuong = "DT";
            madt.Add(moi1);
            DTuong moi2 = new DTuong();
            moi2.MaDTuong = "HN";
            madt.Add(moi2);
            DTuong moi3 = new DTuong();
            moi3.MaDTuong = "TE";
            madt.Add(moi3);
            if (DungChung.Bien.MaBV == "34019")
            {
                DTuong moi4 = new DTuong();
                moi4.MaDTuong = "CN";
                madt.Add(moi4);
                DTuong moi5 = new DTuong();
                moi5.MaDTuong = "BT";
                madt.Add(moi5);
                //grvBenhNhan.Columns["DonGia"].Visible = true;
                //grvBenhNhan.Columns["ThanhTien"].Visible = true;

            }
            cbb_DTuong.Properties.DataSource = madt.ToList();
            radioGroup1_SelectedIndexChanged(sender, e);
        }
        private void ds()
        {
            grcBenhNhan.Refresh();
            DateTime ngay = lup_Ngay.DateTime.Date;
            DateTime ngayden = DungChung.Ham.NgayDen(lup_Ngay.DateTime);
            DateTime ngaytu = DungChung.Ham.NgayTu(lup_Ngay.DateTime);
            DateTime ngaytu1 = DungChung.Ham.NgayTu(lup_Ngay.DateTime.AddMonths(-3));
            int makp = Convert.ToInt32(lup_khoa.EditValue.ToString());
            //double dongia = Convert.ToDouble(txtDonGia.Text);
            string maDTuong = cbb_DTuong.Text;
            _listbc.Clear();

            var bndt = (from a in _data.VaoViens.Where(p => p.NgayVao >= ngaytu1)
                        join b in _data.RaViens on a.MaBNhan equals b.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new { a.MaBNhan, NgayRa = k1 != null ? k1.NgayRa : null }).ToList();
            var bnkb = (from a in bndt.Where(p => p.NgayRa == null || p.NgayRa > ngaytu)
                        join b in _data.BNKBs.Where(p => p.NgayKham <= ngayden) on a.MaBNhan equals b.MaBNhan
                        group new { a, b } by new { a.MaBNhan } into kq
                        select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.b.IDKB) }).ToList();
            #region Bệnh viện khác
            //if (DungChung.Bien.MaBV != "34019")
            //{
            //    var bn = _data.BenhNhans.Where(p => maDTuong != "Tất cả" ? p.MaDTuong.ToLower().Contains(maDTuong.ToLower()) : (p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt") || p.MaDTuong.ToLower().Contains("te"))).ToList();
            //    var bnkbmax = (from a in bnkb
            //                   join b in _data.BNKBs.Where(p => p.MaKP == makp) on a.IDKB equals b.IDKB
            //                   join c in bn on a.MaBNhan equals c.MaBNhan
            //                   select new { a.MaBNhan, c.TenBNhan, c.DChi, b.MaKP }).ToList();
            //    var bnbaocom = (from b in bn
            //                    join a in _data.ChamComs.Where(p => p.Ngaycham == ngay  && p.MaKP == makp) on b.MaBNhan equals a.MaBNhan
            //                    select new { a.MaBNhan, b.TenBNhan, a.Xuat, a.IDChamCom }).ToList();
            //    var xq = (from a in bnkbmax
            //              join b in bnbaocom on a.MaBNhan equals b.MaBNhan into k
            //              from k1 in k.DefaultIfEmpty()
            //              select new
            //              {
            //                  MaBNhan = a.MaBNhan,
            //                  TenBNhan = a.TenBNhan,
            //                  DChi = a.DChi,
            //                  Xuat = k1 != null ? (k1.Xuat == 1 ? "1/2" : "1") : "1",
            //                  IDChamCom = k1 != null ? k1.IDChamCom : 0,
            //                  Chek = k1 != null ? true : false
            //              }).ToList();

            //    baocom moi1 = new baocom();
            //    moi1.MaBNhan = 0;
            //    moi1.TenBNhan = "Tất cả";
            //    moi1.DChi = "";
            //    moi1.Xuat = "";
            //    moi1.IDChamCom = -1;

            //    if (bnbaocom.Count == bnkbmax.Count)
            //    {

            //        moi1.Chek = true;
            //    }
            //    else
            //    {
            //        moi1.IDChamCom = -1;

            //    }

            //    _listbc.Add(moi1);
            //    foreach (var item in xq.Distinct())
            //    {
            //        baocom moi = new baocom();
            //        moi.Chek = item.Chek;
            //        moi.IDChamCom = item.IDChamCom;
            //        moi.Xuat = item.Xuat;
            //        moi.MaBNhan = item.MaBNhan;
            //        moi.TenBNhan = item.TenBNhan;
            //        moi.DChi = item.DChi;
            //        Double b = Convert.ToInt32(item.Xuat);
            //        _listbc.Add(moi);
            //    }
            //    bindingSource1.DataSource = _listbc.OrderBy(p => p.MaBNhan).ToList();
            //    grcBenhNhan.DataSource = bindingSource1;

            //}
            #endregion
            #region Dùng cho bệnh viện Thái Bình
            //else 
            //{
            var bn34019 = _data.BenhNhans.Where(p => maDTuong != "Tất cả" ? p.MaDTuong.ToLower().Contains(maDTuong.ToLower()) : (p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt") || p.MaDTuong.ToLower().Contains("te") || p.MaDTuong.ToLower().Contains("cn") || p.MaDTuong.ToLower().Contains("bt"))).ToList();

            var bnkbmax34019 = (from a in bnkb
                                join b in _data.BNKBs.Where(p => p.MaKP == makp) on a.IDKB equals b.IDKB
                                join c in bn34019 on a.MaBNhan equals c.MaBNhan
                                select new { a.MaBNhan, c.TenBNhan, c.DChi, b.MaKP }).ToList();


            var bnbaocom34019 = (from b in bn34019
                                 join a in _data.ChamComs.Where(p => p.Ngaycham == ngay && p.MaKP == makp) on b.MaBNhan equals a.MaBNhan
                                 select new { a.MaBNhan, b.TenBNhan, a.Xuat, a.IDChamCom, a.DonGia }).ToList();


            var xq34019 = (from a in bnkbmax34019
                           join b in bnbaocom34019 on a.MaBNhan equals b.MaBNhan into k
                           from k1 in k.DefaultIfEmpty()
                           select new
                           {
                               MaBNhan = a.MaBNhan,
                               TenBNhan = a.TenBNhan,
                               DChi = a.DChi,
                               Xuat = k1 != null ? (k1.Xuat == 1 ? "1/2" : "1") : "1",
                               IDChamCom = k1 != null ? k1.IDChamCom : 0,
                               Chek = k1 != null ? true : false
                           }).ToList();

            foreach (var item in xq34019.Distinct())
            {
                baocom moi = new baocom();
                moi.Chek = item.Chek;
                moi.IDChamCom = item.IDChamCom;

                if (item.Xuat == "1")
                {
                    moi.SoLuong = 2;
                }
                if (item.Xuat == "1/2")
                {
                    moi.SoLuong = 1;
                }

                moi.Xuat = item.Xuat;
                moi.MaBNhan = item.MaBNhan;
                moi.TenBNhan = item.TenBNhan;
                moi.DChi = item.DChi;
                _listbc.Add(moi);
            }

            bindingSource1.DataSource = _listbc.OrderBy(p => p.MaBNhan).ToList();
            grcBenhNhan.DataSource = bindingSource1;

            foreach (var item2 in bnbaocom34019)
            {
                for (int i = 0; i < grvBenhNhan.RowCount; i++)
                {
                    if (item2.MaBNhan == Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "MaBNhan").ToString()))
                    {
                        int xuat = Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "SoLuong").ToString());
                        grvBenhNhan.SetRowCellValue(i, "DonGia", item2.DonGia);
                        double thanhtien = Convert.ToDouble(item2.DonGia * xuat);
                        grvBenhNhan.SetRowCellValue(i, "ThanhTien", thanhtien);
                        break;
                    }
                }
            }
            // }
            #endregion

        }
        private void lup_Ngay_EditValueChanged(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lup_Ngay.DateTime);
            if (lup_Ngay.Text != "" && lup_khoa.Text != "" && cbb_DTuong.Text != "")
            {
                ds();
            }
        }

        private void lup_khoa_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0 && lup_Ngay.Text != "" && lup_khoa.Text != "" && cbb_DTuong.Text != "")
            {
                ds();
            }
        }

        private void cbb_DTuong_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0 && lup_Ngay.Text != "" && lup_khoa.Text != "" && cbb_DTuong.Text != "")
            {
                ds();
            }
        }


        private void bnt_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            int makp = Convert.ToInt32(lup_khoa.EditValue.ToString());
            if (radioGroup1.SelectedIndex == 0)
            {
                _listbc1.Clear();
                DateTime ngay = lup_Ngay.DateTime.Date;
                if (txtDonGia.Text == "")
                {
                    MessageBox.Show("Bạn hãy nhập Định mức !");
                }
                else
                {
                    Double? dongia = Convert.ToDouble(txtDonGia.Text);
                    string maDTuong = cbb_DTuong.Text;
                    //if(DungChung.Bien.MaBV =="34019")
                    //{
                    var bnbaocom34019 = (from a in _data.ChamComs.Where(p => p.Ngaycham == ngay && p.MaKP == makp)
                                         join b in _data.BenhNhans.Where(p => maDTuong != "Tất cả" ? p.MaDTuong.ToLower().Contains(maDTuong.ToLower()) : (p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt") || p.MaDTuong.ToLower().Contains("bt") || p.MaDTuong.ToLower().Contains("cn") || p.MaDTuong.ToLower().Contains("te"))) on a.MaBNhan equals b.MaBNhan
                                         select new { a.MaBNhan, b.TenBNhan, a.Xuat, b.DChi, a.DonGia }).ToList();
                    foreach (var item in bnbaocom34019)
                    {
                        baocom moi = new baocom();
                        moi.MaBNhan = item.MaBNhan ?? 0;
                        moi.TenBNhan = item.TenBNhan;
                        moi.DChi = item.DChi;
                        moi.DonGia = item.DonGia;
                        if (item.Xuat != 0)
                            moi.Xuat = Convert.ToString(item.Xuat);
                        _listbc1.Add(moi);
                    }
                    BaoCao.rep_PhieuBaoCom rep = new BaoCao.rep_PhieuBaoCom();
                    rep.Khoa.Value = "Khoa: " + lup_khoa.Text;
                    rep.ngaythang.Value = DungChung.Bien.DiaDanh + ", ngày" + lup_Ngay.DateTime.Day + " tháng " + lup_Ngay.DateTime.Month + " năm " + lup_Ngay.DateTime.Year;
                    rep.DTuong.Value = "Đối tượng: " + (cbb_DTuong.Text == "Tất cả" ? "TE, HN, DT, CN, BT" : cbb_DTuong.Text);
                    rep.DinhMuc.Value = "Định mức: " + txtDonGia.Text + " đồng/người/ngày";
                    rep.DataSource = _listbc1.OrderBy(p => p.MaBNhan).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                DateTime tungay = DateTime.Now;
                DateTime denngay = DateTime.Now;
                _listbc2.Clear();
                if (radioGroup2.SelectedIndex == 0)
                {
                    tungay = GetFirstDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text));
                    denngay = GetLastDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text));
                }
                else
                {
                    tungay = DungChung.Ham.NgayTu(lup_Ngay.DateTime);
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                }
                if (denngay > tungay)
                {
                    var chamcom1 = (from a in _data.ChamComs.Where(p => p.Ngaycham >= tungay && p.Ngaycham <= denngay && p.MaKP == makp)
                                    group a by a.MaBNhan into kq
                                    select new { MaBNhan = kq.Key, SoNgay = kq.Count(), Xuat = kq.Sum(p => p.Xuat) }).ToList();
                    var chamcom2 = _data.ChamComs.Where(p => p.Ngaycham >= tungay && p.Ngaycham <= denngay && p.MaKP == makp).ToList();
                    var dsbn1 = (from a in chamcom1
                                 join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan
                                 join c in _data.VaoViens on a.MaBNhan equals c.MaBNhan
                                 join d in _data.RaViens on a.MaBNhan equals d.MaBNhan into k
                                 from k1 in k.DefaultIfEmpty()
                                 select new { a.Xuat, b.MaBNhan, b.TenBNhan, b.DChi, b.SThe, b.DTuong, b.MaDTuong, c.NgayVao, c.SoBA, NgayRa = k1 != null ? k1.NgayRa : null, SoNgay = a.SoNgay }).OrderBy(p => p.NgayVao).ToList();
                    foreach (var item in dsbn1)
                    {
                        double xx1 = 1 / 2;
                        baocom moi = new baocom();
                        moi.MaBNhan = item.MaBNhan;
                        moi.TenBNhan = item.TenBNhan;
                        moi.DChi = item.DChi;
                        foreach (var item2 in chamcom2)
                        {
                            if (item.MaBNhan == item2.MaBNhan)
                            {
                                if (item2.Ngaycham.Value.Day == 1)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X1 = a; //((Convert.ToDouble(item2.Xuat ?? 0) / 2));
                                }

                                if (item2.Ngaycham.Value.Day == 2)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X2 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 3)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X3 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 4)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X4 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 5)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X5 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 6)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X6 = a; //((Convert.ToDouble(item2.Xuat ?? 0) / 2));
                                }

                                if (item2.Ngaycham.Value.Day == 7)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X7 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 8)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X8 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 9)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X9 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 10)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X10 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 11)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X11 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 12)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X12 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 13)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X13 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 14)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X14 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 15)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X15 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 16)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X16 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 17)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X17 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 18)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X18 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 19)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X19 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 20)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X20 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 21)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X21 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 22)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X22 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 23)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X23 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 24)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X24 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 25)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X25 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 26)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X26 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 27)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X27 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 28)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X28 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 29)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X29 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 30)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X30 = a;
                                }

                                if (item2.Ngaycham.Value.Day == 31)
                                {
                                    double a = Convert.ToDouble(item2.Xuat ?? 0) / 2;
                                    if (a > 0)
                                        moi.X31 = a;
                                }
                            }
                        }
                        moi.SoXuat = (double)item.Xuat / 2;
                        double a1 = Convert.ToDouble(item.Xuat ?? 0) / 2;
                        if (a1 > 0)
                            moi.SoXuat = a1;
                        _listbc2.Add(moi);
                    }
                    if (radioGroup1.SelectedIndex == 2)
                    {
                        BaoCao.BC_DSBNNhanHTAn rep = new BaoCao.BC_DSBNNhanHTAn();
                        rep.txtkhoa.Text = rep.txtkhoa2.Text = "Khoa: " + lup_khoa.Text;
                        rep.thang.Text = "Từ ngày " + tungay.Day + " / " + tungay.Month + " Đến " + denngay.Day + " / " + denngay.Month + " Năm " + denngay.Year;
                        rep.DinhMuc.Value = "Định mức: " + txtDonGia.Text + " đồng/người/ngày";
                        rep.DataSource = dsbn1.OrderBy(p => p.NgayVao).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.BangChamCom rep = new BaoCao.BangChamCom();
                        rep.txtkhoa.Text = rep.txtkhoa2.Text = "Khoa: " + lup_khoa.Text;
                        rep.thang.Text = "Từ ngày " + tungay.Day + " / " + tungay.Month + " Đến " + denngay.Day + " / " + denngay.Month + " Năm " + denngay.Year;
                        rep.txtDinhMuc.Text = rep.txtDinhMuc2.Text = "Nguồn: Quỹ KCBNN (" + txtDonGia.Text + " đ/ngày)";
                        rep.txtngaythangin.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.DataSource = _listbc2.OrderBy(p => p.TenBNhan).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Ngày từ phải < ngày đến!");
                    lup_Ngay.Focus();
                }
            }
        }

        private void grvBenhNhan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Check")
            {
                var data = (List<baocom>)bindingSource1.DataSource;
                var row = (baocom)grvBenhNhan.GetRow(e.RowHandle);
                if (row != null)
                {
                    data.FirstOrDefault(o => o.MaBNhan == row.MaBNhan).Chek = (bool)e.Value;
                }

                grcBenhNhan.BeginUpdate();
                bindingSource1.DataSource = data;
                grcBenhNhan.DataSource = bindingSource1;
                grcBenhNhan.EndUpdate();
                if (data != null && data.Count > 0)
                {
                    if (data.Exists(o => !o.Chek))
                    {
                        selectAll = false;
                        e.Column.Image = imageList1.Images[1];
                    }
                    else
                    {
                        e.Column.Image = imageList1.Images[0];
                        selectAll = true;
                    }
                }
            }
        }

        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                radioGroup2.Enabled = false;
                comboBoxEdit1.Enabled = false;
                comboBoxEdit1.Text = "";
                dateDenNgay.Enabled = false;
                dateDenNgay.Text = "";
                grcBenhNhan.Enabled = true;
                lup_Ngay.Enabled = true;
                btn_luu.Enabled = true;
                label2.Text = "Ngày báo cơm:";
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                radioGroup2.SelectedIndex = 1;
                radioGroup2.Enabled = false;
                grcBenhNhan.Enabled = false;
                dateDenNgay.Enabled = true;
                lup_Ngay.Enabled = true;
                btn_luu.Enabled = false;
                label2.Text = "Từ ngày:";
            }
            else
            {
                radioGroup2.Enabled = true;
                grcBenhNhan.Enabled = false;
                btn_luu.Enabled = false;
                label2.Text = "Từ ngày:";
                radioGroup2.SelectedIndex = 1;
            }

        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup2.SelectedIndex == 1)
            {
                lup_Ngay.Enabled = true;
                dateDenNgay.Enabled = true;
                comboBoxEdit1.Enabled = false;
                dateDenNgay.DateTime = DateTime.Now;
            }
            else if (radioGroup2.SelectedIndex == 0)
            {
                lup_Ngay.Enabled = false;
                dateDenNgay.Enabled = false;
                comboBoxEdit1.Enabled = true;
                dateDenNgay.Text = "";
            }
        }
        private void btn_luu_Click(object sender, EventArgs e)
        {
            grcBenhNhan.Refresh();
            int makp = Convert.ToInt32(lup_khoa.EditValue.ToString());
            if (radioGroup1.SelectedIndex == 0)
            {
                DateTime ngay = lup_Ngay.DateTime.Date;
                if (ngay > DateTime.Now.Date)
                {
                    MessageBox.Show("Bạn đã nhập ngày vượt quá ngày hiện tại, vui lòng nhập lại !");
                }
                else
                {
                    var bcom = (from a in _data.ChamComs.Where(p => p.Ngaycham == ngay && p.MaKP == makp) select a).ToList();
                    //foreach (var item in bcom)
                    //{
                    //    ChamCom xoa = _data.ChamComs.Single(p => p.IDChamCom == item.IDChamCom);
                    //    _data.ChamComs.Remove(xoa);
                    //}
                    int count = 0;
                    for (int t = 0; t < grvBenhNhan.RowCount; t++)
                    {

                        if (grvBenhNhan.GetRowCellValue(t, "Chek") != null && grvBenhNhan.GetRowCellValue(t, "Chek").ToString().ToLower() == "true")
                        {
                            Double? dongia = Convert.ToDouble(grvBenhNhan.GetRowCellValue(t, "DonGia"));
                            if (dongia == null || dongia == 0) count++;
                        }
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("Bạn chưa nhập đơn giá, vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        for (int i = 0; i < grvBenhNhan.RowCount; i++)
                        {
                            if (grvBenhNhan.GetRowCellValue(i, "Chek") != null && grvBenhNhan.GetRowCellValue(i, "Chek").ToString().ToLower() == "true")
                            {

                                if (Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "IDChamCom").ToString()) == 0)
                                {
                                    if (grvBenhNhan.GetRowCellValue(i, "Xuat") != null)
                                    {
                                        _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        ChamCom moi1 = new ChamCom();
                                        moi1.MaBNhan = Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "MaBNhan").ToString());
                                        moi1.Ngaycham = ngay;
                                        moi1.DonGia = Convert.ToDouble(grvBenhNhan.GetRowCellValue(i, "DonGia").ToString());
                                        moi1.MaKP = makp;
                                        if (grvBenhNhan.GetRowCellValue(i, "Xuat").ToString() == "1/2")
                                            moi1.Xuat = 1;
                                        if (grvBenhNhan.GetRowCellValue(i, "Xuat").ToString() == "1")
                                            moi1.Xuat = 2;
                                        _data.ChamComs.Add(moi1);
                                        _data.SaveChanges();
                                    }
                                }
                                else
                                {
                                    int id = Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "IDChamCom").ToString());
                                    if (id != 0 && id != -1)
                                    {
                                        ChamCom sua = _data.ChamComs.Single(p => p.IDChamCom == id);

                                        if (grvBenhNhan.GetRowCellValue(i, "Xuat").ToString() == "1/2")
                                            sua.Xuat = 1;
                                        if (grvBenhNhan.GetRowCellValue(i, "Xuat").ToString() == "1")
                                            sua.Xuat = 2;

                                        sua.DonGia = Convert.ToDouble(grvBenhNhan.GetRowCellValue(i, "DonGia").ToString());
                                        _data.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                int id = Convert.ToInt32(grvBenhNhan.GetRowCellValue(i, "IDChamCom").ToString());
                                if (id != 0 && id != -1)
                                {
                                    ChamCom xoa = _data.ChamComs.Single(p => p.IDChamCom == id);
                                    _data.ChamComs.Remove(xoa);
                                    _data.SaveChanges();
                                }
                            }
                        }
                        MessageBox.Show("Lưu thành công!");
                        ds();
                    }
                }
            }
        }

        private void grvBenhNhan_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            grcBenhNhan.Refresh();
            if (grvBenhNhan.GetFocusedRowCellValue("DonGia") != null)
            {
                double thanhtien = Convert.ToDouble(grvBenhNhan.GetFocusedRowCellValue("DonGia").ToString());
                int t = 0;
                if (grvBenhNhan.GetFocusedRowCellValue("Xuat").ToString() == "1/2")
                {
                    grvBenhNhan.SetFocusedRowCellValue("Soluong", 1);
                    t = 1;
                }
                if (grvBenhNhan.GetFocusedRowCellValue("Xuat").ToString() == "1")
                {
                    grvBenhNhan.SetFocusedRowCellValue("Soluong", 2);
                    t = 2;
                }
                grvBenhNhan.SetFocusedRowCellValue("ThanhTien", thanhtien * t);
            }


        }

        private void grvBenhNhan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            double? dongia = Convert.ToDouble(grvBenhNhan.GetRowCellValue(e.RowHandle, "DonGia"));
            if (dongia == null || dongia == 0)
            {
                e.Appearance.BackColor = Color.Azure;
            }
        }

        private void grvBenhNhan_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvBenhNhan.FocusedColumn.FieldName == "DonGia")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Nhập sai đơn giá, vui lòng nhập lại";
                }
                else if (price <= 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Nhập sai đơn giá, vui lòng nhập lại";
                }
            }
        }

        bool selectAll;
        private void grvBenhNhan_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.HitTest == GridHitTest.Column)
                {
                    if (hi.Column.FieldName == "Chek")
                    {
                        var data = (List<baocom>)bindingSource1.DataSource;
                        if (selectAll)
                        {
                            hi.Column.Image = imageList1.Images[1];
                            if (data != null && data.Count > 0)
                            {
                                data.ForEach(o => o.Chek = false);
                            }
                            selectAll = false;
                        }
                        else
                        {
                            hi.Column.Image = imageList1.Images[0];
                            if (data != null && data.Count > 0)
                            {
                                data.ForEach(o => o.Chek = true);
                            }
                            selectAll = true;
                        }
                        grcBenhNhan.BeginUpdate();
                        bindingSource1.DataSource = data;
                        grcBenhNhan.DataSource = bindingSource1;
                        grcBenhNhan.EndUpdate();
                    }
                }
            }
        }

        private void grvBenhNhan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
    }
}