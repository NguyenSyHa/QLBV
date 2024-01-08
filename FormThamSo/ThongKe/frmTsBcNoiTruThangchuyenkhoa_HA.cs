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
    public partial class frmTsBcNoiTruThangchuyenkhoa_HA : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNoiTruThangchuyenkhoa_HA()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            else return true;
        }
        //private class ChuyenKhoa
        //{
        //    public string chuyenkhoa;
        // //
        //    public string Chuyenkhoa
        //    {
        //        set { chuyenkhoa = value; }
        //        get { return chuyenkhoa; }
        //    }

        //}
        private void btnInBC_Click(object sender, EventArgs e)
        {
            if (KTtaoBc())
            {
                DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.repBcNoiTruThangchuyenkhoa_HA rep = new BaoCao.repBcNoiTruThangchuyenkhoa_HA();

                rep.TuNgay.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                rep.TenBC.Value = ("Thống kê BỆNH NHÂN KHÁM BỆNH THEO CHUYÊN KHOA").ToUpper();
                int makhoa = 0, ntru = 2;
                int mack = -1;
                if (lupKhoa.EditValue != null)
                    makhoa = Convert.ToInt32(lupKhoa.EditValue);
                if (lupCK.EditValue != null)
                    mack = Convert.ToInt32(lupCK.EditValue);
                ntru = Convert.ToInt32(radNoiNgoaiTru.SelectedIndex);//2: tất cả, 0: ngoại trú, 1: nội trú
                #region không chọn chuyên khoa
                if (radioGroup1.SelectedIndex == 0)//Lươt KB || Ko thống kê BN chuyển PK
                {
                    var id = (from kb in data.BNKBs.Where(p => makhoa == 0 || p.MaKP == makhoa).Where(p => mack == -1 || p.MaCK == mack)
                              join ck in data.ChuyenKhoas on kb.MaCK equals ck.MaCK
                              join bn in data.BenhNhans.Where(p => ntru == 2 || p.NoiTru == ntru) on kb.MaBNhan equals bn.MaBNhan
                              where (radNgay.SelectedIndex == 0 ? (kb.NgayKham >= tungay && kb.NgayKham <= denngay) : (bn.NNhap >= tungay && bn.NNhap <= denngay))
                              group new { kb, ck, bn } by new { kb.MaBNhan, ChuyenKhoa = ck.TenCK, bn.DTuong, bn.CapCuu } into kq
                              select new { kq.Key.MaBNhan, kq.Key.ChuyenKhoa, kq.Key.DTuong, kq.Key.CapCuu/*, IDKB = kq.Max(p => p.kb.IDKB)*/ }).Distinct().ToList();

                    var a1 = ((from q in id
                               join kb in data.BNKBs.Where(p => p.PhuongAn != 3) on q.MaBNhan equals kb.MaBNhan
                               group new { q, kb } by new { q.ChuyenKhoa } into kq
                               select new
                               {
                                   ChuyenKhoa = kq.Key.ChuyenKhoa,
                                   TongSo = kq.Select(p => p.q.MaBNhan).Distinct().Count(),
                                   BHYT = kq.Where(p => p.q.DTuong.Equals("BHYT")).Select(p => p.q.MaBNhan).Distinct().Count(),
                                   VienPhi = kq.Where(p => p.q.DTuong.Equals("Dịch vụ")).Select(p => p.q.MaBNhan).Distinct().Count(),
                                   CapCuu = kq.Where(p => p.q.CapCuu == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                                   BNVV = kq.Where(p => p.kb.PhuongAn == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                               }).OrderBy(p => p.ChuyenKhoa).ToList());
                    if (a1.Count > 0)
                    {
                        rep.DataSource = a1.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Không có dữ liệu.", "THÔNG BÁO");
                }
                else if (radioGroup1.SelectedIndex == 1)//Thống kê BN chuyển PK
                {
                    var id = (from kb in data.BNKBs.Where(p => makhoa == 0 || p.MaKP == makhoa).Where(p => mack == -1 || p.MaCK == mack)
                              join ck in data.ChuyenKhoas on kb.MaCK equals ck.MaCK
                              join bn in data.BenhNhans.Where(p => ntru == 2 || p.NoiTru == ntru) on kb.MaBNhan equals bn.MaBNhan
                              where (radNgay.SelectedIndex == 0 ? (kb.NgayKham >= tungay && kb.NgayKham <= denngay) : (bn.NNhap >= tungay && bn.NNhap <= denngay))
                              group new { kb, ck, bn } by new { kb.MaBNhan, ChuyenKhoa = ck.TenCK, bn.DTuong, bn.CapCuu } into kq
                              select new { kq.Key.MaBNhan, kq.Key.ChuyenKhoa, kq.Key.DTuong, kq.Key.CapCuu/*, IDKB = kq.Max(p => p.kb.IDKB)*/ }).Distinct().ToList();
                    var qbn = ((from q in id
                                join kb in data.BNKBs on q.MaBNhan equals kb.MaBNhan
                                group new { q, kb } by new { q.ChuyenKhoa } into kq
                                select new
                                {
                                    ChuyenKhoa = kq.Key.ChuyenKhoa,
                                    TongSo = kq.Select(p => p.q.MaBNhan).Distinct().Count(),
                                    BHYT = kq.Where(p => p.q.DTuong.Equals("BHYT")).Select(p => p.q.MaBNhan).Distinct().Count(),
                                    VienPhi = kq.Where(p => p.q.DTuong.Equals("Dịch vụ")).Select(p => p.q.MaBNhan).Distinct().Count(),
                                    CapCuu = kq.Where(p => p.q.CapCuu == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                                    BNVV = kq.Where(p => p.kb.PhuongAn == 1).Select(p => p.q.MaBNhan).Distinct().Count(),
                                }).OrderBy(p => p.ChuyenKhoa).ToList());

                    rep.DataSource = qbn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class CK
        {
            private int MaCK;

            public int mack
            {
                get { return MaCK; }
                set { MaCK = value; }
            }
            private string ChuyenKhoa;

            public string chuyenkhoa
            { set { ChuyenKhoa = value; } get { return ChuyenKhoa; } }

        }
        List<CK> _lck = new List<CK>();
        private void frmTsBcNoiTruThangchuyenkhoa_HA_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.Focus();
            List<KPhong> _lkp = new List<KPhong>();
            _lkp = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                    select kp).ToList();
            _lkp.Add(new KPhong { TenKP = "Tất cả ", MaKP = 0 }); //
            _lkp.OrderBy(p => p.TenKP);
            if (_lkp.Count > 0)
            {
                lupKhoa.Properties.DataSource = _lkp;
            }

            var qck = (from ck in data.ChuyenKhoas
                       select new { ck.TenCK, ck.MaCK }).Distinct().ToList();
            if (qck.Count() > 0)
            {
                CK them1 = new CK();
                them1.mack = -1;
                them1.chuyenkhoa = "Tất cả";
                _lck.Add(them1);
                foreach (var a in qck)
                {
                    CK themmoi = new CK();
                    themmoi.mack = a.MaCK;
                    themmoi.chuyenkhoa = a.TenCK;
                    _lck.Add(themmoi);
                }
                _lck.OrderBy(p => p.chuyenkhoa).ToList();
                lupCK.Properties.DataSource = _lck.ToList();
            }
        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lupCK_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radNoiNgoaiTru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}