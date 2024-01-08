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

    public partial class Frm_BcHoatDongDTri_TH04 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongDTri_TH04()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
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

        private void Frm_BcHoatDongDtri_TH04_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
    
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            var kp = data.KPhongs.ToList();
           // List<DTri> _lDT = new List<DTri>();
            List<KPhong> _lKhoaP = new List<KPhong>();
        
            if (KTtaoBc())
            {
                _lKhoaP.Clear();
                var bv = data.BenhViens.ToList();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_BcHoatDongDTri_TH04 rep = new BaoCao.Rep_BcHoatDongDTri_TH04();
                int nam =Convert.ToInt32(tungay.Year);
                int thang = Convert.ToInt32(tungay.Month);
                if (radIn.SelectedIndex == 0)
                { rep.TuNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
                if (radIn.SelectedIndex == 1)
                {
                    if (thang > 1 && thang <= 3) { rep.TuNgay.Value = "Quý I năm " + nam; }
                    if (thang > 3 && thang <= 6) { rep.TuNgay.Value = "Quý II năm " + nam; }
                    if (thang > 6 && thang <= 9) { rep.TuNgay.Value = "Quý III năm " + nam; }
                    if (thang > 9 && thang <= 12) { rep.TuNgay.Value = "Quý IV năm " + nam; }
                }
                if (radIn.SelectedIndex == 2)
                {
                    if (thang > 1 && thang <= 6) { rep.TuNgay.Value = "Báo cáo thống kê 06 tháng đầu năm " + nam;}
                    if (thang > 6 && thang <= 12) { rep.TuNgay.Value = "Báo cáo thống kê 06 tháng cuối năm " + nam; }
                }
                if (radIn.SelectedIndex == 3)
                { rep.TuNgay.Value = "Báo cáo năm "+ nam; }
                rep.NTN.Value = DungChung.Bien.DiaDanh + ", ngày ..... tháng ..... năm .....";
              
                //for (int i = 0; i < grvKhoaphong.RowCount; i++)
                //{
                //    if (grvKhoaphong.GetRowCellValue(i, Chọn) != null && grvKhoaphong.GetRowCellValue(i, Chọn).ToString() == "True")
                //    {
                //        KPhong newkp = new KPhong();
                //        newkp.chon = true;
                //        newkp.makp = grvKhoaphong.GetRowCellValue(i, MaKP).ToString();
                //        if (grvKhoaphong.GetRowCellValue(i, TenKP) != null)
                //            newkp.tenkp = grvKhoaphong.GetRowCellValue(i, TenKP).ToString();
                //        _lKhoaP.Add(newkp);
                //    }
                //}
                //_lKhoaP.Add(new KPhong { makp = "", tenkp = "", chon = true });
                _lKhoaP = _Kphong.Where(p => p.makp >0).Where(p => p.chon == true).ToList();
                var bnhan = (from rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                             join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                             select new { rv, bn }).ToList();
                int chonNNgoai=radNoiNgoaiT.SelectedIndex;
                    var qdt = (from p in _lKhoaP
                               join rv in bnhan on p.makp equals rv.rv.MaKP
                               where rv.bn.NoiTru==1 
                               where chonNNgoai==0?(rv.bn.DTNT):(chonNNgoai==1?(rv.bn.DTNT==false): true )
                                group new { p,rv } by new { p.tenkp } into kq
                               select new
                               {
                                   Khoa = kq.Key.tenkp,
                                   C1 = kq.Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Select(p => p.rv.bn.MaBNhan).Count().ToString(),
                                   C2 = kq.Where(p => p.rv.bn.Tuoi < 15).Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.Tuoi < 15).Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C3 = kq.Where(p => p.rv.bn.Tuoi < 6).Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.Tuoi < 6).Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C4 = kq.Where(p => p.rv.bn.Tuoi >= 0 && p.rv.bn.Tuoi <= 4).Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.Tuoi >= 0 && p.rv.bn.Tuoi <= 4).Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C5 = kq.Where(p => p.rv.bn.Tuoi > 60).Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.Tuoi > 60).Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C6 = kq.Where(p => p.rv.rv.KetQua == "Khỏi").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.KetQua == "Khỏi").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C7 = kq.Where(p => p.rv.rv.KetQua == "Đỡ|Giảm").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.KetQua == "Đỡ|Giảm").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C8 = kq.Where(p => p.rv.rv.KetQua == "Không T.đổi").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.KetQua == "Không T.đổi").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C9 = kq.Where(p => p.rv.rv.KetQua == "Nặng hơn").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.KetQua == "Nặng hơn").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C11 = kq.Sum(p => p.rv.rv.SoNgaydt) == 0 ? null : kq.Sum(p => p.rv.rv.SoNgaydt).ToString(),
                                   C12 = kq.Where(p => p.rv.rv.Status == 1).Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.Status == 1).Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C13 = kq.Where(p => p.rv.rv.KetQua == "Tử vong").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.rv.KetQua == "Tử vong").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C14 = kq.Where(p => p.rv.bn.DTuong == "BHYT").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.DTuong == "BHYT").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                                   C15 = kq.Where(p => p.rv.bn.DTuong == "Dịch vụ").Select(p => p.rv.rv.MaBNhan).Count() == 0 ? null : kq.Where(p => p.rv.bn.DTuong == "Dịch vụ").Select(p => p.rv.rv.MaBNhan).Count().ToString(),
                               }).ToList();
                    rep.DataSource =qdt.OrderBy(p=>p.Khoa).ToList();

                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                    }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}