using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBV_Database;

namespace QLBV.ChucNang
{
    public partial class Frm_CapLaiGiayRaVien : Form
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public Frm_CapLaiGiayRaVien()
        {
            InitializeComponent();
        }

        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_CapLaiGiayRaVien_Load(object sender, EventArgs e)
        {
            lupMaKPbn.DataSource = _data.KPhongs.ToList();
            var kphong = (from kp in _data.KPhongs
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = " Tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaPhong.DataSource = _Kphong.ToList();
            }

            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, "chon", true);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {
                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {

                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "chon", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "chon", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {

                        }
                        else
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                    }

                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            List<BenhNhan> lbn = new List<BenhNhan>();
            DateTime tungay = Convert.ToDateTime(lupTuNgay.Text);
            DateTime denngay = Convert.ToDateTime(lupDenNgay.Text);
            lbn = (from bn in _data.BenhNhans
                   join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                   join kp in lkp on rv.MaKP equals kp
                   where rv.NgayRa >= tungay && rv.NgayRa <= denngay
                        && (rdgDoiTuong.SelectedIndex == 1 ? bn.DTuong == "BHYT" : rdgDoiTuong.SelectedIndex == 2 ? bn.DTuong == "Dịch vụ" : true)
                        && bn.NoiTru == 1 && bn.TenBNhan.Contains(txtTenBenhNhan.Text)
                   select bn).ToList();
            var qvv = _data.VaoViens.ToList();
            foreach (BenhNhan bn in lbn)// tạm để ngày vào viện vào ngày MaIK để hiển thị
            {
                VaoVien vv = qvv.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                if (vv != null)
                {
                    bn.Ma_lk = (vv.NgayVao != null ? vv.NgayVao.ToString() : null);
                }
                else
                {
                    bn.Ma_lk = null;
                }
            }
            var qrv = _data.RaViens.ToList();
            foreach (BenhNhan bn in lbn)// tạm để ngày ra viện vào PID để hiển thị
            {
                RaVien rv = qrv.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                if (rv != null)
                    bn.PID = (rv.NgayRa != null ? rv.NgayRa.ToString() : null);
                else
                    bn.PID = null;
            }
            if (lbn.Count() > 0)
                grcBNhan.DataSource = lbn;
            else
                MessageBox.Show("Không có dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void grvBNhan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int mabn = Convert.ToInt32(grvBNhan.GetFocusedRowCellValue(colMaBNhan));
            InGiayRaVien(mabn);
        }

        public void InGiayRaVien(int mbn)
        {
            frmIn frm = new frmIn();
            BaoCao.repGiayRaVien_12121 rep = new BaoCao.repGiayRaVien_12121();
            rep.lblCapLai.Visible = true;

            var par = (from bn in _data.BenhNhans.Where(o => o.MaBNhan == mbn)
                       join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                       join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                       from kq2 in kq.DefaultIfEmpty()
                       where (bn.MaBNhan == mbn)
                       select new { rv.MaKP, rv.ChanDoan, rv.ChanDoanYHCT, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, rv.MaICD, NoiLV = kq2 == null ? "" : kq2.NoiLV, vv.SoBA, vv.SoVV, bn.TenBNhan, bn.Tuoi, bn.DTuong, bn.GTinh, MaNN = kq2 == null ? "" : kq2.MaNN, bn.HanBHTu, bn.HanBHDen, bn.SThe, bn.DChi, vv.NgayVao, rv.NgayRa, rv.PPDTr, rv.LoiDan, rv.SoLT, rv.MaYTe }).ToList();
            if (par.Count > 0)
            {
                var dtoc = (from tt in _data.TTboXungs.Where(p => p.MaBNhan == mbn) join dt in _data.DanTocs on tt.MaDT equals dt.MaDT select dt.TenDT).ToList();
                if (dtoc.Count > 0)
                    rep.TenDT.Value = dtoc.First();
                rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                rep.GTinh.Value = par.First().GTinh == 1 ? "Nam" : "Nữ";
                string mann = "";
                if (par.First().MaNN != null)
                    mann = par.First().MaNN;
                var nn = _data.DmNNs.Where(p => p.MaNN == (mann)).FirstOrDefault();
                string _NN = "";
                _NN = nn == null ? "" : nn.TenNN;
                if (!string.IsNullOrEmpty(par.First().NoiLV))
                {
                    if (DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "20001")
                        _NN += " - " + par.First().NoiLV;
                }
                rep.NgheNghiep.Value = _NN;

                rep.DChi.Value = par.First().DChi;

                rep.PPDTr.Value = par.First().PPDTr;
                rep.LoiDan.Value = par.First().LoiDan;
                if (par.First().DTuong == "BHYT")
                {
                    rep.HanBHTu.Value = par.First().HanBHTu;
                    //rep.HanBHDen.Value = par.First().HanBHDen;
                    rep.SThe.Value = par.First().SThe;
                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                }
                else
                {
                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_data, mbn, DungChung.Bien.formatAge);
                }

                if (par.First().NgayVao != null)
                {
                    rep.NgayVao.Value = par.First().NgayVao.Value.Hour + " giờ " + par.First().NgayVao.Value.Minute + " phút, Ngày " + par.First().NgayVao.Value.Day + "  tháng  " + par.First().NgayVao.Value.Month + "  năm  " + par.First().NgayVao.Value.Year;
                }
                //rep.NgayVao.Value = par.First().NgayVao;
                if (par.First().NgayRa != null)
                {
                    rep.NgayRa.Value = par.First().NgayRa.Value.Hour + " giờ " + par.First().NgayRa.Value.Minute + " phút, Ngày " + par.First().NgayRa.Value.Day + "  tháng  " + par.First().NgayRa.Value.Month + "  năm  " + par.First().NgayRa.Value.Year;
                }

                rep.SoLuu.Value = par.First().SoLT;
                rep.MaYTe.Value = par.First().MaYTe;
                rep.ChanDoan.Value = DungChung.Ham.FreshString(DungChung.Ham.GetICDstr(par.First().ChanDoan));
                int _makp = 0;
                if (par.First().MaKP != null)
                    _makp = Convert.ToInt32(par.First().MaKP);

                var kp = _data.KPhongs.Where(p => p.MaKP == _makp).FirstOrDefault();
                rep.Khoa.Value = kp.TenKP;
                //rep.NgayThangDT.Value = ("Ngày " + par.First().NgayRa.Value.Day + "  Tháng  " + par.First().NgayRa.Value.Month + "  Năm  " + par.First().NgayRa.Value.Year);
                //rep.NgayThangGD.Value = ("Ngày " + par.First().NgayRa.Value.Day + "  Tháng  " + par.First().NgayRa.Value.Month + "  Năm  " + par.First().NgayRa.Value.Year);
                rep.NgayThangDT.Value = ("Ngày " + DateTime.Now.Day + "  Tháng  " + DateTime.Now.Month + "  Năm  " + DateTime.Now.Year);
                rep.NgayThangGD.Value = ("Ngày " + DateTime.Now.Day + "  Tháng  " + DateTime.Now.Month + "  Năm  " + DateTime.Now.Year);
                rep.GiamDoc.Value = DungChung.Bien.GiamDoc;
                rep.HoTenTruongKhoa.Value = string.IsNullOrEmpty(DungChung.Bien.TruongKhoaLS) ? DungChung.Bien.MaBV == "24297" ? "" : "Họ tên:................." : "Họ tên: " + DungChung.Bien.TruongKhoaLS;

            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int mabn = Convert.ToInt32(grvBNhan.GetFocusedRowCellValue(colMaBNhan));
            if (mabn == 0)
                MessageBox.Show("Chưa chọn bệnh nhân", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                InGiayRaVien(mabn);
        }
    }
}
