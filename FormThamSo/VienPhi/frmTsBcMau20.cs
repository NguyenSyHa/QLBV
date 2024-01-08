using System;
using QLBV_Database;
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
    public partial class frmTsBcMau20 : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcMau20()
        {
            InitializeComponent();
        }

        private bool KTtaoBcMau20()
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
        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " QÚY I NĂM 2014";
                        break;
                    case 2:
                        quy = " QÚY II NĂM 2014 ";
                        break;
                    case 3:
                        quy = " QUÝ III NĂM 2014";
                        break;
                    case 4:
                        quy = " QÚY IV NĂM 2014";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            string macc = "";
            int _makp = 0;
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            int _noitru = -1;
            int trongBH = 0;
            string _doituong = "";
            if (lupMaKP.EditValue != null)
                _makp = Convert.ToInt32(lupMaKP.EditValue);
            if (!string.IsNullOrEmpty(cboDoiTuong.Text))
            {
                _doituong = cboDoiTuong.Text;
            }
            if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                macc = lupNhaCC.EditValue.ToString();
            if (KTtaoBcMau20())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                _noitru = radNoiTru.SelectedIndex;
                frmIn frm = new frmIn();
                BaoCao.repBcMau20 rep = new BaoCao.repBcMau20();

                if (radNoiTru.SelectedIndex == 1)
                {
                    if (_doituong == "BHYT")
                    {
                        rep.NoiNgoaiTru.Value = "CHO NGƯỜI BỆNH BHYT ĐIỀU TRỊ NỘI TRÚ";
                        trongBH = 1;
                    }
                    else
                        rep.NoiNgoaiTru.Value = "CHO NGƯỜI BỆNH NHÂN DÂN ĐIỀU TRỊ NỘI TRÚ";
                }
                else
                {
                    if (_doituong == "BHYT")
                    {
                        trongBH = 1;
                        rep.NoiNgoaiTru.Value = "CHO NGƯỜI BỆNH BHYT ĐIỀU TRỊ NGOẠI TRÚ";
                    }
                    else
                        rep.NoiNgoaiTru.Value = "CHO NGƯỜI BỆNH NHÂN DÂN ĐIỀU TRỊ NGOẠI TRÚ";
                }
                if (ckQuy.Checked == true)
                {
                    rep.Quy.Value = theoquy();
                }
                else rep.TuNgayDenNgay.Value = theoquy();

                var a1 = data.NhomDVs.ToList();
                int idNhom1 = 0, idNhom2 = 0;
                if (a1.Where(p => p.TenNhomCT == null).ToList().Count > 0)
                {
                    MessageBox.Show("Tên nhóm Chi tiết có giá trị null => Update lại TenNhomCT trong bảng NhomDV.");
                }
                else
                {
                    if (a1.Where(p => p.TenNhomCT == ("Thuốc trong danh mục BHYT")).ToList().Count > 0 && a1.Where(p => p.TenNhomCT == ("Vật tư y tế trong danh mục BHYT")).ToList().Count > 0)
                    {
                        idNhom1 = a1.Where(p => p.TenNhomCT == ("Thuốc trong danh mục BHYT")).First().IDNhom;
                        idNhom2 = a1.Where(p => p.TenNhomCT == ("Vật tư y tế trong danh mục BHYT")).First().IDNhom;
                    }
                }
                if (radTimKiem.SelectedIndex == 0)
                {
                    List<KetQua> _lKetQua =
                        (from bn in data.BenhNhans
                         join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join rv in data.RaViens.Where(p => p.NgayRa <= ngayden).Where(p => p.NgayRa >= ngaytu) on bn.MaBNhan equals rv.MaBNhan
                         join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         where (nhomdv.IDNhom == idNhom1 || nhomdv.IDNhom == idNhom2)
                         group new { dv, vpct, bn, rv } by new { bn.NoiTru, vpct.TrongBH, bn.DTuong, dv.MaCC, rv.MaKP, nhomdv.IDNhom, nhomdv.TenNhom, dv.MaDV, dv.TenDV, dv.HamLuong, vpct.DonVi, vpct.DonGia, dv.SoDK } into kq
                         select new
                         {
                             DTuong = kq.Key.DTuong,
                             MaCC = kq.Key.MaCC,
                             MaKP = kq.Key.MaKP,
                             IdNhom = kq.Key.IDNhom,
                             TenNhomThuoc = kq.Key.TenNhom,
                             TenThuoc = kq.Key.TenDV,
                             HamLuong = kq.Key.HamLuong,
                             DonVi = kq.Key.DonVi,
                             SoDK = kq.Key.SoDK,
                             MaDV = kq.Key.MaDV,
                             NoiTru = kq.Key.NoiTru,
                             TrongBH = kq.Key.TrongBH,
                             DonGia = kq.Key.DonGia,
                             SoLuong = kq.Sum(p => p.vpct.SoLuong),
                             ThanhTien = kq.Sum(p => p.vpct.ThanhTien)
                         }).ToList().Select(c => new KetQua
                         {
                             DTuong = c.DTuong,
                             MaCC = c.MaCC,
                             MaKP = c.MaKP,
                             IdNhom = c.IdNhom,
                             TenNhomThuoc = c.TenNhomThuoc,
                             TenThuoc = c.TenThuoc,
                             HamLuong = c.HamLuong,
                             DonVi = c.DonVi,
                             SoDK = c.SoDK,
                             MaDV = c.MaDV,
                             NoiTru = c.NoiTru == null ? 0 : c.NoiTru.Value,
                             TrongBH = c.TrongBH,
                             DonGia = c.DonGia,
                             SoLuong = c.SoLuong,
                             ThanhTien = c.ThanhTien
                         }).OrderBy(p => p.TenThuoc).ToList<KetQua>();
                    if (!string.IsNullOrEmpty(macc))
                    {
                        if (_makp > 0)
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaKP == _makp && k.MaCC == macc)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaCC == macc)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (_makp > 0)
                        {
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaKP == _makp)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    List<KetQua> _lKetQua =
                        (from bn in data.BenhNhans
                         join vp in data.VienPhis.Where(p => p.NgayTT <= ngayden).Where(p => p.NgayTT >= ngaytu) on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         where (nhomdv.IDNhom == idNhom1 || nhomdv.IDNhom == idNhom2)
                         group new { dv, vpct, bn, rv } by new { bn.NoiTru, vpct.TrongBH, bn.DTuong, dv.MaCC, rv.MaKP, nhomdv.IDNhom, nhomdv.TenNhom, dv.MaDV, dv.TenDV, dv.HamLuong, vpct.DonVi, vpct.DonGia, dv.SoDK } into kq
                         select new
                         {
                             DTuong = kq.Key.DTuong,
                             MaCC = kq.Key.MaCC,
                             MaKP = kq.Key.MaKP,
                             IdNhom = kq.Key.IDNhom,
                             TenNhomThuoc = kq.Key.TenNhom,
                             TenThuoc = kq.Key.TenDV,
                             HamLuong = kq.Key.HamLuong,
                             DonVi = kq.Key.DonVi,
                             SoDK = kq.Key.SoDK,
                             MaDV = kq.Key.MaDV,
                             NoiTru = kq.Key.NoiTru,
                             TrongBH = kq.Key.TrongBH,
                             DonGia = kq.Key.DonGia,
                             SoLuong = kq.Sum(p => p.vpct.SoLuong),
                             ThanhTien = kq.Sum(p => p.vpct.ThanhTien)
                         }).ToList().Select(c => new KetQua
                         {
                             DTuong = c.DTuong,
                             MaCC = c.MaCC,
                             MaKP = c.MaKP,
                             IdNhom = c.IdNhom,
                             TenNhomThuoc = c.TenNhomThuoc,
                             TenThuoc = c.TenThuoc,
                             HamLuong = c.HamLuong,
                             DonVi = c.DonVi,
                             SoDK = c.SoDK,
                             MaDV = c.MaDV,
                             NoiTru = c.NoiTru == null ? 0 : c.NoiTru.Value,
                             TrongBH = c.TrongBH,
                             DonGia = c.DonGia,
                             SoLuong = c.SoLuong,
                             ThanhTien = c.ThanhTien
                         }).OrderBy(p => p.TenThuoc).ToList<KetQua>();
                    if (!string.IsNullOrEmpty(macc))
                    {
                        if (_makp > 0)
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaKP == _makp && k.MaCC == macc)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaCC == macc)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (_makp > 0)
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 where (k.MaKP == _makp)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupMaKP.Text.ToUpper();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                        else
                        {
                            rep.NhaCC.Value = lupNhaCC.Text;
                            List<KetQua> q =
                                (from k in _lKetQua
                                 where (k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH)
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.DonVi, k.DonGia, k.SoDK } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.k.SoLuong),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            #region xuat Excel
                            if (chkXuatExel.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                exSheet.Activate();
                                exSheet.Name = "bcthuoc_005";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[15] { "chonin", "ma_khoa", "stt_nh", "ct_id", "ma_hieu", "gia", "tong_sl", "sotien", "ten", "ten_05", "so_dk", "h_luong", "donvitinh", "ten_khoa", "ten_nh" };
                                int k = 0;
                                foreach (var b in _arr)
                                {
                                    k++;
                                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                    r.Value2 = b.ToString();
                                    r.Columns.AutoFit();
                                }
                                foreach (var a in q)
                                {
                                    i++;
                                    //COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    //r1.Value2 = i - 1;
                                    //r1.Columns.AutoFit();
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = "TRUE";
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.Value2 = "";
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.Value2 = a.IdNhom;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.Value2 = "";
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.Value2 = a.MaDV;
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.Value2 = a.DonGia;
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.Value2 = a.SoLuong;
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.Value2 = a.ThanhTien;
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.Value2 = a.TenThuoc;
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.Value2 = "";
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.Value2 = a.SoDK;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.Value2 = a.HamLuong;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.Value2 = a.DonVi;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.Value2 = "";
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.Value2 = a.TenNhomThuoc;
                                    r15.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu20.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                                null, null, false, false,
                                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                                false, false, false, false, false);
                                //exQLBV.Close(false, false, false);
                                //exApp.Quit(); // thoát ứng dụng
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                            }
                            #endregion
                        }
                    }
                }
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsBcMau20_Load(object sender, EventArgs e)
        {
            var nhacc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.Properties.DataSource = nhacc;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            var kp = (from k in data.KPhongs where (k.PLoai.Contains("Lâm sàng") || k.PLoai.Contains("Phòng khám")) select k).ToList();
            lupMaKP.Properties.DataSource = kp.ToList();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public class KetQua
        {
            private int maKP;
            private string maBN;
            private string dTuong;
            private int noiTru;
            private int trongBH;
            private int idVienPhi;
            private int maDV;
            private string tenDV;
            private string tenHC;
            private string duongD;
            private string hamLuong;
            private string soDK;
            private string donVi;
            private double donGia;
            private int idTieuNhom;
            private int idNhom;
            private string tenNhom;
            private string tenTN;
            private string tenNhomCT;
            private int stt;
            private string maQD;
            private string soTTqd;
            private double soLuong;
            private double thanhTien;
            private double thanhTienTong;
            private string maCC;
            public string MaCC
            {
                get { return maCC; }
                set { maCC = value; }
            }
            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            public string MaBN
            {
                get { return maBN; }
                set { maBN = value; }
            }
            public string DTuong
            {
                get { return dTuong; }
                set { dTuong = value; }
            }
            public int NoiTru
            {
                get { return noiTru; }
                set { noiTru = value; }
            }
            public int TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }
            public int IdVienPhi
            {
                get { return idVienPhi; }
                set { idVienPhi = value; }
            }
            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public string TenThuoc
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            public string TenHC
            {
                get { return tenHC; }
                set { tenHC = value; }
            }
            public string DuongDung
            {
                get { return duongD; }
                set { duongD = value; }
            }
            public string HamLuong
            {
                get { return hamLuong; }
                set { hamLuong = value; }
            }
            public string SoDK
            {
                get { return soDK; }
                set { soDK = value; }
            }
            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            public int IdTieuNhom
            {
                get { return idTieuNhom; }
                set { idTieuNhom = value; }
            }
            public int IdNhom
            {
                get { return idNhom; }
                set { idNhom = value; }
            }
            public string TenNhomThuoc
            {
                get { return tenNhom; }
                set { tenNhom = value; }
            }
            public string TenTNhom
            {
                get { return tenTN; }
                set { tenTN = value; }
            }
            public string TenNhomCT
            {
                get { return tenNhomCT; }
                set { tenNhomCT = value; }
            }
            public int STT
            {
                get { return stt; }
                set { stt = value; }
            }
            public string MaQD
            {
                get { return maQD; }
                set { maQD = value; }
            }
            public string SoTTqd
            {
                get { return soTTqd; }
                set { soTTqd = value; }
            }
            public double SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            public double ThanhTienTong
            {
                get { return thanhTienTong; }
                set { thanhTienTong = value; }
            }
        }


    }
}