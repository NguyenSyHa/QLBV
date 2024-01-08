using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using static QLBV.FormThamSo.frm_BC_VienPhi_HDViettel_NTL;

namespace QLBV.FormThamSo
{
    public partial class Frm_NopTienVaoQuy_GLoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_NopTienVaoQuy_GLoc()
        {
            InitializeComponent();
        }
        string _macb = "";
        string _dt = "";
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateNgayTu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày từ");
                dateNgayTu.Focus();
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày đến");
                dtDenNgay.Focus();
                return false;
            }
            //if (cmbDoiTuong.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn đối tượng in báo cáo");
            //    cmbDoiTuong.Focus();
            //    return false;
            //}
            if (lupCanBo.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn cán bộ báo cáo");
                lupCanBo.Focus();
                return false;
            }
            else return true;
        }
        private void Frm_NopTienVaoQuy_GLoc_Load(object sender, EventArgs e)
        {

            List<DTBN> dtbn = data.DTBNs.Where(p => p.Status == 1).OrderBy(p => p.IDDTBN).ToList();
            dtbn.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDTBN.Properties.DataSource = dtbn;


            var _lkp = (from kp in data.KPhongs select new { kp.MaKP, kp.TenKP }).ToList();
            _lkp.Insert(0, new { MaKP = 0, TenKP = "Tất cả" });
            lupkhoaphong.Properties.DataSource = _lkp;
            dateNgayTu.DateTime = System.DateTime.Today;
            DateTime tungay = dateNgayTu.DateTime.AddHours(23).AddMinutes(59);
            dtDenNgay.DateTime = tungay;
            lupkhoaphong_EditValueChanged(null, null);
            rgChonMauBCct.SelectedIndex = 0;
            rgChonMauBCct_SelectedIndexChanged(null, null);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                radioGroupLoaiThu.Visible = true;
            if (DungChung.Bien.MaBV != "01071")
            {
                radioGroupLoaiThu.Properties.Items.RemoveAt(2);
            }
        }
        private class BC
        {
            private int MaBNhan1;

            private string TenBNhan1;
            private string NgayLP1;
            private string SoPhieu1;
            private double TienUng1;
            private double TienThu1;
            private double TienChi1;
            public int MaBNhan
            { set { MaBNhan1 = value; } get { return MaBNhan1; } }
            public string TenBNhan
            { set { TenBNhan1 = value; } get { return TenBNhan1; } }
            public string NgayLP
            { set { NgayLP1 = value; } get { return NgayLP1; } }
            public string SoPhieu
            { set { SoPhieu1 = value; } get { return SoPhieu1; } }
            public double TienUng
            { set { TienUng1 = value; } get { return TienUng1; } }
            public double TienThu
            { set { TienThu1 = value; } get { return TienThu1; } }
            public double TienChi
            { set { TienChi1 = value; } get { return TienChi1; } }

            public double ThuNgoaiTru { get; set; }

            public double ThuNoiTru { get; set; }
            public double TienThuoc { get; set; }
            public string SoBA { get; set; }
            public string SoHD { get; set; }
            public double TienKham { get; set; }
            public double TienDVu { get; set; }
            public double TienThuocVTYT { get; set; }
            public double TienTU { get; set; }
            public double TienVP { get; set; }
            public double TienTong { get; set; }
            public double TienMien { get; set; }
            public int Mien { get; set; }
            public int? PhanLoai { get; set; }
        }
        List<BC> _BC = new List<BC>();
        List<BC> _lastBC = new List<BC>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            //DateTime ngaytu = System.DateTime.Now.Date;
            if (KTtaoBc())
            {
                _BC.Clear();
                //ngaytu = DungChung.Ham.NgayTu(dateNgayTu.DateTime);
                //DateTime ngayden = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
                DateTime ngaytu = dateNgayTu.DateTime;
                DateTime ngayden = dtDenNgay.DateTime;
                int dtuong = 100;
                if (lupDTBN.EditValue != null)
                    dtuong = Convert.ToInt32(lupDTBN.EditValue);

                string _macb = "";
                if (lupCanBo.EditValue != null)
                    _macb = lupCanBo.EditValue.ToString();

                int makp = 0;
                if (lupkhoaphong.EditValue != null)
                    makp = Convert.ToInt32(lupkhoaphong.EditValue);
                if (rgChonMauBCct.SelectedIndex == 0)
                {
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        
                        frmIn frm = new frmIn();
                        BaoCao.Rep_NopTienVaoQuy_GLoc_TH rep = new BaoCao.Rep_NopTienVaoQuy_GLoc_TH();
                        if (DungChung.Bien.MaBV == "01071")
                        {
                            rep.Ngay.Value = "Từ ngày " + dateNgayTu.Text + " đến ngày " + dtDenNgay.Text;
                            rep.NgayNop.Value = dateNgayTu.Text;


                            rep.DoiTuong.Value = "Đối tượng: " + lupDTBN.Text + " - (Trong và ngoài giờ)";
                            var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                            if (qtencb.Count() > 0)
                            {
                                rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB;
                            }
                            // tính số tiền ứng
                            var qnt = (from bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong)//.Where(p => p.NoiTru == 0)
                                       join tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp)
                                              .Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                              on bn.MaBNhan equals tu.MaBNhan
                                       where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                              tu.PhanLoai != 5 && ((radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null :
                                              radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : true)))
                                       //group new { bn, tu } by new { tu.NgayThu, bn.MaBNhan, bn.TenBNhan, bn.DTuong, tu.PhanLoai, tu.IDTamUng } into kq
                                       select new
                                       {
                                           NgayLP = tu.NgayThu,
                                           MaBNhan = bn.MaBNhan,
                                           TenBNhan = bn.TenBNhan,
                                           NoiTru = bn.NoiTru,
                                           DTuong = bn.DTuong,
                                           SoPhieu = tu.IDTamUng,
                                           SoTien = tu.SoTien,
                                           TienChenh = tu.TienChenh,
                                           tu.PhanLoai,
                                       }).OrderBy(p => p.SoPhieu).ToList();
                            if (DungChung.Bien.MaBV == "01071")
                            {
                                qnt = (from bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong)//.Where(p => p.NoiTru == 0)
                                       join tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp)
                                              .Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                              on bn.MaBNhan equals tu.MaBNhan
                                       where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                              tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 3 ? true ://|| tu.PhanLoai == 7 :
                                              (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null && tu.PhanLoai != 7 :
                                              radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null && tu.PhanLoai != 7 : tu.PhanLoai == 7)))
                                       //group new { bn, tu } by new { tu.NgayThu, bn.MaBNhan, bn.TenBNhan, bn.DTuong, tu.PhanLoai, tu.IDTamUng } into kq
                                       select new
                                       {
                                           NgayLP = tu.NgayThu,
                                           MaBNhan = bn.MaBNhan,
                                           TenBNhan = bn.TenBNhan,
                                           NoiTru = bn.NoiTru,
                                           DTuong = bn.DTuong,
                                           SoPhieu = tu.IDTamUng,
                                           SoTien = tu.SoTien,
                                           TienChenh = tu.TienChenh,
                                           tu.PhanLoai,
                                       }).OrderBy(p => p.SoPhieu).ToList();
                            }
                            //var q1 = (from tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                            //          join nd in data.NhapDs on tu.IDNhapD equals nd.IDNhap
                            //          where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden)
                            //          select new
                            //          {
                            //              nd.TenNguoiCC,
                            //              tu.IDTamUng,
                            //              tu.SoTien,
                            //              tu.NgayThu
                            //          }).ToList();

                            foreach (var a in qnt)
                            {
                                BC themmoi = new BC();
                                themmoi.NgayLP = a.NgayLP.Value.ToString("dd/MM/yy HH:MM");
                                themmoi.MaBNhan = a.MaBNhan;
                                themmoi.SoPhieu = a.SoPhieu.ToString();
                                themmoi.TenBNhan = a.TenBNhan;

                                if (a.PhanLoai == 0 && a.SoTien != null)
                                    themmoi.TienUng = a.SoTien.Value;
                                double chitamung = 0, chitt = 0;
                                if (a.PhanLoai == 4 && a.SoTien != null)
                                    chitamung = a.SoTien.Value;
                                if (a.PhanLoai == 2)
                                    chitt = a.SoPhieu;
                                if (a.PhanLoai == 7 && a.SoTien != null)
                                {
                                    //themmoi.
                                    themmoi.TienUng = a.SoTien.Value;

                                }
                                themmoi.TienChi = chitamung + chitt;
                                if ((a.PhanLoai == 1 || a.PhanLoai == 3 || a.PhanLoai == 8))
                                {
                                    themmoi.TienThu = a.SoTien ?? 0;
                                }
                                _BC.Add(themmoi);
                            }
                        }
                        else
                        {
                            rep.Ngay.Value = "Từ ngày " + dateNgayTu.Text + " đến ngày " + dtDenNgay.Text;
                            rep.NgayNop.Value = dateNgayTu.Text;


                            rep.DoiTuong.Value = "Đối tượng: " + lupDTBN.Text + " - (Trong và ngoài giờ)";
                            var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                            if (qtencb.Count() > 0)
                            {
                                rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB;
                            }
                            // tính số tiền ứng
                            int TEST = radioGroupLoaiThu.SelectedIndex;
                            var qnt = (from bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong)//.Where(p => p.NoiTru == 0)
                                       join tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp)
                                              .Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                              on bn.MaBNhan equals tu.MaBNhan
                                       where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                              tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 2 ? true :
                                              (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null :
                                              radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : false/*tu.PhanLoai == 7 || tu.PhanLoai == 8*/)))
                                       //group new { bn, tu } by new { tu.NgayThu, bn.MaBNhan, bn.TenBNhan, bn.DTuong, tu.PhanLoai, tu.IDTamUng } into kq
                                       select new
                                       {
                                           NgayLP = tu.NgayThu,
                                           MaBNhan = bn.MaBNhan,
                                           TenBNhan = bn.TenBNhan,
                                           NoiTru = bn.NoiTru,
                                           DTuong = bn.DTuong,
                                           SoPhieu = tu.IDTamUng,
                                           SoTien = tu.SoTien,
                                           TienChenh = tu.TienChenh,
                                           tu.PhanLoai,
                                       }).OrderBy(p => p.SoPhieu).ToList();
                            //var q1 = (from tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                            //          join nd in data.NhapDs on tu.IDNhapD equals nd.IDNhap
                            //          where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden)
                            //          select new
                            //          {
                            //              nd.TenNguoiCC,
                            //              tu.IDTamUng,
                            //              tu.SoTien,
                            //              tu.NgayThu
                            //          }).ToList();

                            foreach (var a in qnt)
                            {
                                BC themmoi = new BC();
                                themmoi.NgayLP = a.NgayLP.Value.ToString("dd/MM/yy HH:MM");
                                themmoi.MaBNhan = a.MaBNhan;
                                themmoi.SoPhieu = a.SoPhieu.ToString();
                                themmoi.TenBNhan = a.TenBNhan;

                                if (a.PhanLoai == 0 && a.SoTien != null)
                                    themmoi.TienUng = a.SoTien.Value;
                                double chitamung = 0, chitt = 0;
                                if (a.PhanLoai == 4 && a.SoTien != null)
                                    chitamung = a.SoTien.Value;
                                if (a.PhanLoai == 2)
                                    chitt = a.SoPhieu;
                                if (a.PhanLoai == 8 && a.SoTien != null)
                                {
                                    //themmoi.
                                    themmoi.TienUng = a.SoTien.Value;

                                }
                                themmoi.TienChi = chitamung + chitt;
                                if ((a.PhanLoai == 1 || a.PhanLoai == 3 || a.PhanLoai == 8 ))
                                {
                                    themmoi.TienThu = a.SoTien ?? 0;
                                }
                                _BC.Add(themmoi);
                            }
                        }
                        

                        //foreach (var a in q1)
                        //{
                        //    BC themmoi = new BC();
                        //    themmoi.NgayLP = a.NgayThu.Value.ToString("dd/MM/yy HH:MM");
                        //    themmoi.SoPhieu = a.IDTamUng.ToString();
                        //    themmoi.TenBNhan = a.TenNguoiCC;
                        //    themmoi.TienThuoc = a.SoTien ?? 0;
                        //    _BC.Add(themmoi);
                        //}

                        double b = _BC.Sum(p => p.TienUng) + _BC.Sum(p => p.TienThu) - _BC.Sum(p => p.TienChi);
                        rep.TongTien.Value = b;
                        rep.DataSource = _BC.OrderBy(p => p.SoPhieu).Where(p => p.TienChi != 0 || p.TienThu != 0 || p.TienUng != 0).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else if (DungChung.Bien.MaBV == "01071")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_NopTienVaoQuy_GLoc rep = new BaoCao.Rep_NopTienVaoQuy_GLoc();

                        rep.Ngay.Value = "Từ ngày " + dateNgayTu.Text + " đến ngày " + dtDenNgay.Text;
                        rep.NgayNop.Value = dateNgayTu.Text;


                        rep.DoiTuong.Value = "Đối tượng: " + lupDTBN.Text + " - (Trong và ngoài giờ)";
                        var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                        if (qtencb.Count() > 0)
                        {
                            rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB;
                        }
                        // tính số tiền ứng
                        var qnt = (from bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong)//.Where(p => p.NoiTru == 0)
                                   join tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp)
                                   .Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb) on bn.MaBNhan equals tu.MaBNhan
                                   join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                   join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                                   where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden && 
                                          tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 3 ? true : 
                                          (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null : 
                                          radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : tu.PhanLoai == 7)))
                                   //group new { bn, tu } by new { tu.NgayThu, bn.MaBNhan, bn.TenBNhan, bn.DTuong, tu.PhanLoai, tu.IDTamUng } into kq
                                   select new
                                   {
                                       NgayLP = tu.NgayThu,
                                       MaBNhan = bn.MaBNhan,
                                       TenBNhan = bn.TenBNhan,
                                       NoiTru = bn.NoiTru,
                                       DTuong = bn.DTuong,
                                       SoPhieu = tu.IDTamUng,
                                       SoTien = tu.SoTien,
                                       TienChenh = tu.TienChenh,
                                       tu.PhanLoai,
                                       dv.PLoai
                                   }).Distinct().OrderBy(p => p.SoPhieu).ToList();
                        var q1 = (from tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                  join nd in data.NhapDs on tu.IDNhapD equals nd.IDNhap
                                  where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                  tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 3 ? true :
                                  (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null :
                                  radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : tu.PhanLoai == 7)))
                                  select new
                                  {
                                      nd.TenNguoiCC,
                                      tu.IDTamUng,
                                      tu.SoTien,
                                      tu.NgayThu
                                  }).ToList();

                        foreach (var a in qnt)
                        {
                            BC themmoi = new BC();
                            themmoi.NgayLP = a.NgayLP.Value.ToString("dd/MM/yy HH:MM");
                            themmoi.MaBNhan = a.MaBNhan;
                            themmoi.SoPhieu = a.SoPhieu.ToString();
                            themmoi.TenBNhan = a.TenBNhan;
                            if (a.PhanLoai == 3 && a.PLoai == 1)
                            {
                                themmoi.TienThuoc = (double)a.SoTien;
                            }
                            if (a.PhanLoai == 3 && a.PLoai == 2)
                            {
                                themmoi.ThuNgoaiTru = (double)a.SoTien;
                            }
                            if (a.PhanLoai == 0 && a.SoTien != null)
                                themmoi.TienUng = a.SoTien.Value;
                            double chitamung = 0, chitt = 0;
                            if (a.PhanLoai == 4 && a.SoTien != null)
                                chitamung = a.SoTien.Value;
                            if (a.PhanLoai == 2)
                                chitt = a.TienChenh;
                            themmoi.TienChi = chitamung + chitt;
                            if ((a.PhanLoai == 1 || a.PhanLoai == 3 || a.PhanLoai == 8))
                            {
                                if (a.NoiTru == 1)
                                    themmoi.ThuNoiTru = a.TienChenh;
                                themmoi.TienThu = a.TienChenh;
                            }

                            _BC.Add(themmoi);
                        }

                        foreach (var a in q1)
                        {
                            BC themmoi = new BC();
                            themmoi.NgayLP = a.NgayThu.Value.ToString("dd/MM/yy HH:MM");
                            themmoi.SoPhieu = a.IDTamUng.ToString();
                            themmoi.TenBNhan = a.TenNguoiCC;
                            themmoi.TienThuoc = a.SoTien ?? 0;
                            _BC.Add(themmoi);
                        }

                        double b = _BC.Sum(p => p.TienUng) + _BC.Sum(p => p.TienThu) + _BC.Sum(p => p.TienThuoc) - _BC.Sum(p => p.TienChi);
                        rep.TongTien.Value = b;
                        rep.DataSource = _BC.OrderBy(p => p.SoPhieu).Where(p => p.TienChi != 0 || p.TienThu != 0 || p.TienUng != 0 || p.TienThuoc != 0).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_NopTienVaoQuy_GLoc rep = new BaoCao.Rep_NopTienVaoQuy_GLoc();

                        rep.Ngay.Value = "Từ ngày " + dateNgayTu.Text + " đến ngày " + dtDenNgay.Text;
                        rep.NgayNop.Value = dateNgayTu.Text;


                        rep.DoiTuong.Value = "Đối tượng: " + lupDTBN.Text + " - (Trong và ngoài giờ)";
                        var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                        if (qtencb.Count() > 0)
                        {
                            rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB;
                        }
                        // tính số tiền ứng
                        var qnt = (from bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong)//.Where(p => p.NoiTru == 0)
                                   join tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp)
                                   .Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb) on bn.MaBNhan equals tu.MaBNhan
                                   join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                   join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                                   where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                          tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 2 ? true :
                                          (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null :
                                          radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : false)))
                                   //group new { bn, tu } by new { tu.NgayThu, bn.MaBNhan, bn.TenBNhan, bn.DTuong, tu.PhanLoai, tu.IDTamUng } into kq
                                   select new
                                   {
                                       NgayLP = tu.NgayThu,
                                       MaBNhan = bn.MaBNhan,
                                       TenBNhan = bn.TenBNhan,
                                       NoiTru = bn.NoiTru,
                                       DTuong = bn.DTuong,
                                       SoPhieu = tu.IDTamUng,
                                       SoTien = tu.SoTien,
                                       TienChenh = tu.TienChenh,
                                       tu.PhanLoai,
                                       dv.PLoai
                                   }).Distinct().OrderBy(p => p.SoPhieu).ToList();
                        var q1 = (from tu in data.TamUngs.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                  join nd in data.NhapDs on tu.IDNhapD equals nd.IDNhap
                                  where (tu.NgayThu >= ngaytu & tu.NgayThu <= ngayden &&
                                  tu.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 3 ? true :
                                  (radioGroupLoaiThu.SelectedIndex == 0 ? tu.IDTamUngThe == null :
                                  radioGroupLoaiThu.SelectedIndex == 1 ? tu.IDTamUngThe != null : tu.PhanLoai == 7)))
                                  select new
                                  {
                                      nd.TenNguoiCC,
                                      tu.IDTamUng,
                                      tu.SoTien,
                                      tu.NgayThu
                                  }).ToList();

                        foreach (var a in qnt)
                        {
                            BC themmoi = new BC();
                            themmoi.NgayLP = a.NgayLP.Value.ToString("dd/MM/yy HH:MM");
                            themmoi.MaBNhan = a.MaBNhan;
                            themmoi.SoPhieu = a.SoPhieu.ToString();
                            themmoi.TenBNhan = a.TenBNhan;
                            if (a.PhanLoai == 3 && a.PLoai == 1)
                            {
                                themmoi.TienThuoc = (double)a.SoTien;
                            }
                            if (a.PhanLoai == 3 && a.PLoai == 2)
                            {
                                themmoi.ThuNgoaiTru = (double)a.SoTien;
                            }
                            if (a.PhanLoai == 0 && a.SoTien != null)
                                themmoi.TienUng = a.SoTien.Value;
                            double chitamung = 0, chitt = 0;
                            if (a.PhanLoai == 4 && a.SoTien != null)
                                chitamung = a.SoTien.Value;
                            if (a.PhanLoai == 2)
                                chitt = a.TienChenh;
                            themmoi.TienChi = chitamung + chitt;
                            if ((a.PhanLoai == 1 || a.PhanLoai == 3 || a.PhanLoai == 8 ))
                            {
                                if (a.NoiTru == 1)
                                    themmoi.ThuNoiTru = a.TienChenh;
                                themmoi.TienThu = a.TienChenh;
                            }

                            _BC.Add(themmoi);
                        }

                        foreach (var a in q1)
                        {
                            BC themmoi = new BC();
                            themmoi.NgayLP = a.NgayThu.Value.ToString("dd/MM/yy HH:MM");
                            themmoi.SoPhieu = a.IDTamUng.ToString();
                            themmoi.TenBNhan = a.TenNguoiCC;
                            themmoi.TienThuoc = a.SoTien ?? 0;
                            _BC.Add(themmoi);
                        }

                        double b = _BC.Sum(p => p.TienUng) + _BC.Sum(p => p.TienThu) + _BC.Sum(p => p.TienThuoc) - _BC.Sum(p => p.TienChi);
                        rep.TongTien.Value = b;
                        rep.DataSource = _BC.OrderBy(p => p.SoPhieu).Where(p => p.TienChi != 0 || p.TienThu != 0 || p.TienUng != 0 || p.TienThuoc != 0).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    var _lvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)
                                join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join bn in data.BenhNhans.Where(p => dtuong == 100 || p.IDDTBN == dtuong) on vp.MaBNhan equals bn.MaBNhan
                                join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                                join vv in data.VaoViens on vp.MaBNhan equals vv.MaBNhan into kq
                                from vv1 in kq.DefaultIfEmpty()
                                select new { bn.MaBNhan, bn.TenBNhan, vpct.MaDV, vpct.TrongBH, vpct.TienBN, SoBA = vv1 != null ? vv1.SoVV : "" }).ToList();
                    //var _lvp1 = (from vp in _lvp join tu in data.TamUngs on vp.MaBNhan equals tu.MaBNhan select new 
                    //            {
                    //                vp.MaBNhan, vp.TenBNhan, vp.MaDV, vp.TrongBH, vp.TienBN, vp.SoBA,tu.Mien
                    //            }).ToList();

                    var _ldv = (from dv in data.DichVus
                                join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                select new { dv.MaDV, tn.IdTieuNhom, tn.IDNhom, dv.PLoai }).ToList();
                    List<int> _lmabn = _lvp.Select(p => p.MaBNhan).Distinct().ToList();

                    var _ltung = data.TamUngs.Where(p => _lmabn.Contains(p.MaBNhan ?? 0) && p.PhanLoai == 0 && (radioGroupLoaiThu.SelectedIndex == 2 ? true : (radioGroupLoaiThu.SelectedIndex == 0 ? p.IDTamUngThe == null : p.IDTamUngThe != null))).ToList();
                    //.Where(p => makp == 0 ? true : p.MaKP == makp).Where(p => (_macb == "" || _macb == "0") ? true : p.MaCB == _macb)

                    var ketqua = (from vp in _lvp
                                  join dv in _ldv on vp.MaDV equals dv.MaDV
                                  group new { dv, vp } by new { vp.MaBNhan, vp.TenBNhan, vp.SoBA } into kq
                                  select new BC
                                  {
                                      MaBNhan = kq.Key.MaBNhan,
                                      TenBNhan = kq.Key.TenBNhan,
                                      SoBA = kq.Key.SoBA,
                                      TienKham = kq.Where(p => p.dv.IDNhom == 13 && p.vp.TrongBH == 0 && p.dv.PLoai != 1).Sum(p => p.vp.TienBN),
                                      TienDVu = kq.Where(p => p.dv.IDNhom != 13 && p.vp.TrongBH == 0 && p.dv.PLoai == 2).Sum(p => p.vp.TienBN),
                                      TienThuocVTYT = kq.Where(p => p.vp.TrongBH == 0 && p.dv.PLoai == 1).Sum(p => p.vp.TienBN),
                                      TienVP = kq.Where(p => p.vp.TrongBH == 1).Sum(p => p.vp.TienBN),
                                  }).ToList();
                    _BC.AddRange(ketqua);

                    #region older
                    if (DungChung.Bien.MaBV != "30372")
                    {
                        foreach (var item in _BC)
                        {
                            var tu = _ltung.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                            if (tu.Count > 0)
                            {
                                item.TienTU = tu.Sum(p => p.SoTien ?? 0);
                            }
                            item.TienTong = item.TienKham + item.TienDVu + item.TienThuocVTYT + item.TienVP + item.TienTU;

                            var _mien = data.TamUngs.Where(p => p.MaBNhan == item.MaBNhan).Where(o => o.NgayThu >= ngaytu && o.NgayThu <= ngayden && o.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 2 ? true : (radioGroupLoaiThu.SelectedIndex == 0 ? o.IDTamUngThe == null : o.IDTamUngThe != null))).Select(p => p.Mien).ToList();
                            if (_mien.Count > 0)
                            {
                                item.Mien = _mien[0];
                                item.TienMien = (Convert.ToDouble(item.Mien) / 100) * item.TienTong;
                            }
                            else
                            {
                                item.Mien = 0;
                                item.TienMien = 0;
                            }


                        }
                        if (_BC.Count > 0)
                        {
                            double Tongtien = _BC.Sum(p => p.TienTong);

                            frmIn frm = new frmIn();
                            BaoCao.Rep_TongHopTienThuTheoCa_30303_Khac rep = new BaoCao.Rep_TongHopTienThuTheoCa_30303_Khac();
                            var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                            if (qtencb.Count() > 0)
                            {
                                rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB + ", Ca từ: " + ngaytu.ToString("dd/MM/yyy HH:mm") + " đến: " + ngayden.ToString("dd/MM/yyy HH:mm");
                                rep.CBKy.Value = qtencb.First().TenCB;
                            }
                            else
                            {
                                rep.CanBo.Value = "Ca từ: " + ngaytu.ToString("dd/MM/yyy HH:mm") + " đến: " + ngayden.ToString("dd/MM/yyy HH:mm");

                            }
                            rep.TongTien.Value = Tongtien.ToString();
                            rep.TongTienChu.Value = DungChung.Ham.DocTienBangChu(Tongtien, " đồng");
                            rep.DataSource = _BC.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("không có dữ liệu!");
                        }
                    }
                    else
                    {
                    #endregion


                        #region Newest
                        _lastBC.Clear();
                        foreach (var item in ketqua)
                        {
                            var _mienTU = data.TamUngs.Where(p => p.MaBNhan == item.MaBNhan && p.PhanLoai != 5 && (radioGroupLoaiThu.SelectedIndex == 2 ? true : (radioGroupLoaiThu.SelectedIndex == 0 ? p.IDTamUngThe == null : p.IDTamUngThe != null))).ToList();

                            foreach (var item2 in _mienTU)
                            {
                                BC _dsBC = new BC();
                                _dsBC.MaBNhan = item.MaBNhan;
                                _dsBC.TenBNhan = item.TenBNhan;
                                _dsBC.Mien = item2.Mien;
                                _dsBC.SoHD = item2.SoHD;
                                _dsBC.PhanLoai = item2.PhanLoai;
                                _dsBC.SoBA = item.SoBA;
                                if (item2.PhanLoai == 0)
                                {
                                    _dsBC.TienTU = Convert.ToDouble(item2.SoTien);
                                    _dsBC.TienTong = _dsBC.TienTU;
                                }
                                if (item2.PhanLoai == 1)
                                {
                                    var ketqua100 = (from vp in data.VienPhis.Where(p => p.MaBNhan == item.MaBNhan).Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden)
                                                     join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                                     join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                                                     join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                                     join tu in data.TamUngs on vpct.IDTamUng equals tu.IDTamUng
                                                     select new
                                                         {
                                                             vp.MaBNhan,
                                                             dv.IDNhom,
                                                             dv.PLoai,
                                                             vpct.TienBN,
                                                             vpct.ThanhToan
                                                         }).ToList();

                                    _dsBC.TienKham = ketqua100.Where(p => p.IDNhom == 13).Where(p => p.ThanhToan != 1).Sum(p => p.TienBN) != null ? ketqua100.Where(p => p.IDNhom == 13).Where(p => p.ThanhToan != 1).Sum(p => p.TienBN) : 0;

                                    _dsBC.TienDVu = ketqua100.Where(p => p.IDNhom != 13).Where(p => p.ThanhToan != 1).Where(p => p.PLoai == 2).Sum(p => p.TienBN) != null ? ketqua100.Where(p => p.IDNhom != 13).Where(p => p.ThanhToan != 1).Where(p => p.PLoai == 2).Sum(p => p.TienBN) : 0;

                                    _dsBC.TienThuocVTYT = ketqua100.Where(p => p.IDNhom != 13).Where(p => p.ThanhToan != 1).Where(p => p.PLoai == 1).Sum(p => p.TienBN) != null ? ketqua100.Where(p => p.IDNhom != 13).Where(p => p.ThanhToan != 1).Where(p => p.PLoai == 1).Sum(p => p.TienBN) : 0;

                                    _dsBC.TienMien = 0;
                                    _dsBC.TienVP = 0;
                                    _dsBC.TienTong = ketqua100.Where(p => p.ThanhToan != 1).Sum(p => p.TienBN);
                                }
                                if (item2.PhanLoai == 3)
                                {
                                    var ketqua3 = (from tu in data.TamUngs
                                                   .Where(p => p.MaBNhan == item.MaBNhan)
                                                   .Where(p => p.SoHD == item2.SoHD && p.PhanLoai != 5 && 
                                                   (radioGroupLoaiThu.SelectedIndex == 3 ? true : 
                                                   (radioGroupLoaiThu.SelectedIndex == 0 ? p.IDTamUngThe == null : radioGroupLoaiThu.SelectedIndex == 1 ? p.IDTamUngThe != null : p.PhanLoai == 7)))
                                                   join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                                   join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                                                   join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                                                   join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                                   select new
                                    {
                                        dv.IDNhom,
                                        tuct.ThanhTien,
                                        dv.PLoai,
                                        tu.TongTien,
                                        tu.TienChenh
                                    }).ToList();

                                    _dsBC.TienKham = ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienDVu = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienThuocVTYT = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienVP = ketqua3.Sum(p => p.ThanhTien) != null ? ketqua3.Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienMien = item2.Mien != 0 ? Convert.ToDouble(item2.TongTien - item2.TienChenh) : 0;
                                    _dsBC.TienTong = item2.TienChenh;
                                }
                                if (item2.PhanLoai == 7 || item2.PhanLoai == 8 && DungChung.Bien.MaBV == "01071")
                                {
                                    var ketqua3 = (from tu in data.TamUngs
                                                   .Where(p => p.MaBNhan == item.MaBNhan)
                                                   .Where(p => p.SoHD == item2.SoHD && p.PhanLoai != 5 &&
                                                   (radioGroupLoaiThu.SelectedIndex == 3 ? true :
                                                   (radioGroupLoaiThu.SelectedIndex == 0 ? p.IDTamUngThe == null : radioGroupLoaiThu.SelectedIndex == 1 ? p.IDTamUngThe != null : (p.PhanLoai == 7 || p.PhanLoai == 8))))
                                    join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                                   join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                                                   join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                                                   join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                                   select new
                                                   {
                                                       dv.IDNhom,
                                                       tuct.ThanhTien,
                                                       dv.PLoai,
                                                       tu.TongTien,
                                                       tu.TienChenh
                                                   }).ToList();

                                    _dsBC.TienKham = ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienDVu = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienThuocVTYT = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienVP = ketqua3.Sum(p => p.ThanhTien) != null ? ketqua3.Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienMien = item2.Mien != 0 ? Convert.ToDouble(item2.TongTien - item2.TienChenh) : 0;
                                    _dsBC.TienTong = item2.TienChenh;
                                }
                                if (item2.PhanLoai == 8 || item2.PhanLoai == 9)
                                {
                                    var ketqua3 = (from tu in data.TamUngs
                                                   .Where(p => p.MaBNhan == item.MaBNhan)
                                                   .Where(p => p.SoHD == item2.SoHD && p.PhanLoai != 5 &&
                                                   (radioGroupLoaiThu.SelectedIndex == 3 ? true :
                                                   (radioGroupLoaiThu.SelectedIndex == 0 ? p.IDTamUngThe == null : radioGroupLoaiThu.SelectedIndex == 1 ? p.IDTamUngThe != null : item2.PhanLoai == 7)))
                                                   join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                                   join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                                                   join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                                                   join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                                   select new
                                                   {
                                                       dv.IDNhom,
                                                       tuct.ThanhTien,
                                                       dv.PLoai,
                                                       tu.TongTien,
                                                       tu.TienChenh
                                                   }).ToList();

                                    _dsBC.TienKham = ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom == 13).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienDVu = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 2).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienThuocVTYT = ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) != null ? ketqua3.Where(p => p.IDNhom != 13).Where(p => p.PLoai == 1).Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienVP = ketqua3.Sum(p => p.ThanhTien) != null ? ketqua3.Sum(p => p.ThanhTien) : 0;
                                    _dsBC.TienMien = item2.Mien != 0 ? Convert.ToDouble(item2.TongTien - item2.TienChenh) : 0;
                                    _dsBC.TienTong = item2.TienChenh;
                                }
                                _lastBC.Add(_dsBC);
                            }
                        }
                        if (/*_BC.Count > 0**/_lastBC.Count > 0)
                        {
                            //double Tongtien = _BC.Sum(p => p.TienTong);
                            double Tongtien = _lastBC.Sum(p => p.TienTong);


                            frmIn frm = new frmIn();
                            BaoCao.Rep_TongHopTienThuTheoCa_30303 rep = new BaoCao.Rep_TongHopTienThuTheoCa_30303();
                            var qtencb = from cb in data.CanBoes.Where(p => p.MaCB == _macb) select new { cb.TenCB, cb.MaCB };
                            if (qtencb.Count() > 0)
                            {
                                rep.CanBo.Value = "Nhân viên báo cáo: " + qtencb.First().TenCB + ", Ca từ: " + ngaytu.ToString("dd/MM/yyy HH:mm") + " đến: " + ngayden.ToString("dd/MM/yyy HH:mm");
                                rep.CBKy.Value = qtencb.First().TenCB;
                            }
                            else
                            {
                                rep.CanBo.Value = "Ca từ: " + ngaytu.ToString("dd/MM/yyy HH:mm") + " đến: " + ngayden.ToString("dd/MM/yyy HH:mm");

                            }
                            rep.TongTien.Value = Tongtien.ToString();
                            rep.TongTienChu.Value = DungChung.Ham.DocTienBangChu(Tongtien, " đồng");
                            //rep.DataSource = _BC.OrderBy(p=>p.TenBNhan).ToList();
                            rep.DataSource = _lastBC.OrderBy(p => p.TenBNhan).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("không có dữ liệu!");
                        }


                        #endregion
                    }

                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupkhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            int makp = 0;
            if (lupkhoaphong.EditValue != null)
            {
                makp = Convert.ToInt32(lupkhoaphong.EditValue);
            }
            var qcb = (from cb in data.CanBoes
                       join kp in data.KPhongs on cb.MaKP equals kp.MaKP
                       where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan || kp.MaKP == makp)
                       select new { cb.TenCB, cb.MaCB }).Distinct().OrderBy(p => p.TenCB).ToList();
            qcb.Insert(0, new { TenCB = "Tất cả", MaCB = "0" });
            lupCanBo.Properties.DataSource = qcb.ToList();
            lupCanBo.EditValue = "0";
        }

        private void rgChonMauBCct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgChonMauBCct.SelectedIndex == 0)
            {
                radioGroup1.Visible = true;
                radioGroupLoaiThu.Visible = true;
                radioGroupLoaiThu.SelectedIndex = 2;
            }
            else
            {
                radioGroup1.Visible = false;
                radioGroupLoaiThu.Visible = false;
                radioGroupLoaiThu.SelectedIndex = 2;
            }

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}