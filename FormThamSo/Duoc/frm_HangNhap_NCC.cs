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

namespace QLBV.FormThamSo
{
    public partial class frm_HangNhap_NCC : DevExpress.XtraEditors.XtraForm
    {
        public frm_HangNhap_NCC()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        string _khoa = "Khoa dược";
        private void frm_HangNhap_NCC_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dateTuNgay.EditValue = System.DateTime.Now;
            dateDenNgay.EditValue = System.DateTime.Now;
            var q = (from kd in data.KPhongs
                     where (kd.PLoai== (_khoa))
                     select new { kd.MaKP, kd.TenKP }).ToList();
            lupKho.Properties.DataSource = q.ToList();
            lupKho.EditValue = DungChung.Bien.MaKP;
            List<NhaCC> _lNhaCC = (from ncc in data.NhaCCs select ncc).ToList();
            _lNhaCC.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });
            lupNhaCC.Properties.DataSource = _lNhaCC;
            List<NhomDV> _lNhomDV = (data.NhomDVs).Where(p => p.Status == 1).OrderBy(p => p.TenNhom).ToList();
            _lNhomDV.Insert(0, new NhomDV { IDNhom = -1, TenNhom = "Tất cả" });
            lup_NhomDV.Properties.DataSource = _lNhomDV;
            List<TieuNhomDV> _lTNhomDV = (from tn in data.TieuNhomDVs join n in data.NhomDVs on tn.IDNhom equals n.IDNhom where n.Status == 1 select tn).ToList();
            _lTNhomDV.Insert(0, new TieuNhomDV { IdTieuNhom = -1, TenTN = "Tất cả" });
            lupTieuNhomDV.Properties.DataSource = _lTNhomDV;
        }
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
            string kho = "";
            if (lupKho.EditValue != null)
                kho = lupKho.EditValue.ToString();
            if (string.IsNullOrEmpty(kho))
            {
                MessageBox.Show("Bạn chưa chọn kho để in báo cáo");
                lupKho.Focus();
                return false;
            }
            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now;
            DateTime denngay = System.DateTime.Now;

            if (KTtaoBc())
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                int kho = 0;
                string nhacc = "";
                int int_nhom = -1, int_TN = -1;
                if (lupKho.EditValue != null)
                    kho = Convert.ToInt32( lupKho.EditValue);
                if (lupNhaCC.EditValue != null)
                    nhacc = lupNhaCC.EditValue.ToString();
                if (lup_NhomDV.EditValue != null)
                    int_nhom = Convert.ToInt32(lup_NhomDV.EditValue);
                if (lupTieuNhomDV.EditValue != null)
                    int_TN = Convert.ToInt32(lupTieuNhomDV.EditValue);

                int m = 0;
                if (kho>0)
                {
                    var qnxt2 = (from nhapd in data.NhapDs.Where(p => p.MaKP == kho)
                                 join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                 join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                 where (nhapd.PLoai == 1 && (int_nhom == -1 || nhomdv.IDNhom == int_nhom) && (int_TN == -1 || tn.IdTieuNhom == int_TN))
                                 select new { dv.MaDV, nhapd.NgayNhap, nhapd.MaCC, tn.TenTN, nhomdv.TenNhom, dv.TenDV, dv.DonVi, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX })
                                 .Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).ToList();
                    var qnxt = (from a in qnxt2
                                group a by new { a.TenTN, a.MaCC, a.TenNhom, a.TenDV, a.DonVi, a.DonGia, a.MaDV } into kq
                                select new
                                {
                                    kq.Key.MaCC,
                                    TenTN = kq.Key.TenTN,
                                    MaDV = kq.Key.MaDV,
                                    TenDV = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    SoLuongN = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                    ThanhTienN = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) * kq.Key.DonGia,
                                }).Distinct().ToList().OrderBy(p => p.TenTN).ThenBy(p => p.TenDV).ToList();
                    if (qnxt.Count > 0)
                    {


                        var q = qnxt.Where(p =>nhacc == "" || p.MaCC == nhacc).ToList();
                        if (q.Count > 0)
                        {

                            string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0" };
                            int[] _arrWidth = new int[] { 10, 12, 40, 8, 12, 15, 15 };
                            string[] _tieude =new string[] { "STT", "Mã thuốc", "Tên thuốc và hàm lượng", "Đơn vị", "Đơn giá", "Số lượng", "Thành tiền" };
                            DungChung.Bien.MangHaiChieu = new Object[q.Count + 1, 7];
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                               
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }
                          
                            int num = 1;
                            foreach (var r in q)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenDV;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhTienN;
                                num++;
                            }

                            BaoCao.rep_TKHangNhap_NCC rep = new BaoCao.rep_TKHangNhap_NCC();
                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo nhập dược", "C:\\BcNhapHang.xls", false, this.Name);
                            rep.Ngay.Value = "Từ ngày:  " + dateTuNgay.Text + "     đến ngày:  " + dateDenNgay.Text;
                            rep.TenKhoa.Value = _khoa.ToUpper();
                            string tieude = "Thống kê lượng nhập " + (int_nhom >0 ? lup_NhomDV.Text : "Thuốc - vật tư y tế - Hóa chất ") + (lupNhaCC.Text == "Tất cả" ? "" : lupNhaCC.Text);
                            rep.TieuDe.Value = tieude.ToUpper();
                            rep.DataSource = q;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            this.Hide();
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu");
                    }

                }

            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lup_NhomDV_EditValueChanged(object sender, EventArgs e)
        {
            int IDNhom = -1;
            if (lup_NhomDV.EditValue != null)
                IDNhom = Convert.ToInt32(lup_NhomDV.EditValue);
            List<TieuNhomDV> _lTNhomDV = (from tn in data.TieuNhomDVs join n in data.NhomDVs on tn.IDNhom equals n.IDNhom where ((IDNhom == -1 && n.Status == 1) || (IDNhom > 0 && tn.IDNhom == IDNhom)) select tn).ToList();
            _lTNhomDV.Insert(0, new TieuNhomDV { IdTieuNhom = -1, TenTN = "Tất cả" });
            lupTieuNhomDV.Properties.DataSource = _lTNhomDV;
        }
    }
}