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
    public partial class frmTsBcMau21BHYT : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcMau21BHYT()
        {
            InitializeComponent();
        }
        private bool KTtaoBcMau21()
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
            if (string.IsNullOrEmpty(cboDoiTuong.Text))
            {
                MessageBox.Show("Bạn chưa chọn đối tượng");
                cboDoiTuong.Focus();
                return false;
            }
            return true;
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            int idnhom = 0;
            int _makp = 0;
            int trongBH = 0;
            string _doituong = "";
            if (lupKhoaphong2.EditValue != null)
                _makp = Convert.ToInt32( lupKhoaphong2.EditValue);
            if (!string.IsNullOrEmpty(cboDoiTuong.Text))
            {
                _doituong = cboDoiTuong.Text;
            }
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            int _noitru = 0;
            if (lupNhom.EditValue != null && lupNhom.EditValue.ToString() != "")
                idnhom = Convert.ToInt32(lupNhom.EditValue);
            if (KTtaoBcMau21())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                _noitru = radNoiTru.SelectedIndex;
                frmIn frm = new frmIn();
                BaoCao.repBcMau21BHYT rep = new BaoCao.repBcMau21BHYT();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
                if (radTimKiem.SelectedIndex == 0)
                {
                    List<KetQua> _lKQ =
                        (from bn in data.BenhNhans
                         join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         join rv in data.RaViens.Where(p => p.NgayRa <= ngayden).Where(p => p.NgayRa >= ngaytu) on bn.MaBNhan equals rv.MaBNhan
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join tieunhom in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhom.IdTieuNhom
                         join nhomdv in data.NhomDVs on tieunhom.IDNhom equals nhomdv.IDNhom
                         group new { dv, vpct, bn } by new { nhomdv.Status, bn.DTuong, bn.NoiTru, vpct.TrongBH, rv.MaKP, nhomdv.IDNhom, nhomdv.TenNhom, dv.MaDV, dv.TenDV, dv.HamLuong, dv.SoDK, dv.DonVi, vpct.DonGia, dv.MaQD } into kq
                         select new
                         {
                             Status = kq.Key.Status,
                             DTuong = kq.Key.DTuong,
                             NoiTru = kq.Key.NoiTru,
                             TrongBH = kq.Key.TrongBH,
                             MaKP = kq.Key.MaKP,
                             MaQD = kq.Key.MaQD,
                             IdNhom = kq.Key.IDNhom,
                             TenNhomThuoc = kq.Key.TenNhom,
                             MaDV = kq.Key.MaDV,
                             SoDK = kq.Key.SoDK,
                             DonVi = kq.Key.DonVi,
                             TenThuoc = kq.Key.TenDV,
                             HamLuong = kq.Key.HamLuong,
                             DonGia = kq.Key.DonGia,
                             SoLuongNT = kq.Sum(p => p.vpct.SoLuong),
                             ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                         }).ToList().Select(c => new KetQua
                         {
                             Status = c.Status == null ? 0 : c.Status.Value,
                             DTuong = c.DTuong,
                             NoiTru = c.NoiTru == null ? 0 : c.NoiTru.Value,
                             TrongBH =  c.TrongBH,
                             MaKP =  c.MaKP,
                             MaQD = c.MaQD,
                             IdNhom = c.IdNhom,
                             TenNhomThuoc = c.TenNhomThuoc,
                             MaDV = c.MaDV,
                             SoDK = c.SoDK,
                             DonVi = c.DonVi,
                             TenThuoc = c.TenThuoc,
                             HamLuong = c.HamLuong,
                             DonGia = c.DonGia,
                             SoLuongNT = c.SoLuongNT == null ? 0 : c.SoLuongNT,
                             ThanhTien = c.ThanhTien == null ? 0 : c.ThanhTien
                         }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                    if (idnhom > 0)
                    {
                        if (_makp== null)
                        {
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.IdNhom == idnhom
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = que.ToList();
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
                                exSheet.Name = "bcthuoc_21";// gán tên sheet
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
                                foreach (var a in que)
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
                                    r7.Value2 = a.SoLuongNT;
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
                                exQLBV.SaveAs("C:\\Bieu21.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.IdNhom == idnhom && k.MaKP == _makp
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupKhoaphong2.Text.ToUpper();
                            rep.DataSource = que.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        if (_makp== null)
                        {
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.Status == 2
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = que.ToList();
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
                                exSheet.Name = "bcthuoc_21";// gán tên sheet
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
                                foreach (var a in que)
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
                                    r7.Value2 = a.SoLuongNT;
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
                                exQLBV.SaveAs("C:\\Bieu21.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.Status == 2 && k.MaKP == _makp
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupKhoaphong2.Text.ToUpper();
                            rep.DataSource = que.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                }
                else
                {
                    List<KetQua> _lKQ =
                        (from bn in data.BenhNhans
                         join vp in data.VienPhis.Where(p => p.NgayTT <= ngayden).Where(p => p.NgayTT >= ngaytu) on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                         join tieunhom in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhom.IdTieuNhom
                         join nhomdv in data.NhomDVs on tieunhom.IDNhom equals nhomdv.IDNhom
                         group new { dv, vpct, bn } by new { nhomdv.Status, bn.DTuong, bn.NoiTru, vpct.TrongBH, rv.MaKP, nhomdv.IDNhom, nhomdv.TenNhom, dv.MaDV, dv.TenDV, dv.HamLuong, dv.SoDK, dv.DonVi, vpct.DonGia, dv.MaQD } into kq
                         select new
                         {
                             Status = kq.Key.Status,
                             DTuong = kq.Key.DTuong,
                             NoiTru = kq.Key.NoiTru,
                             TrongBH = kq.Key.TrongBH,
                             MaKP = kq.Key.MaKP,
                             MaQD = kq.Key.MaQD,
                             IdNhom = kq.Key.IDNhom,
                             TenNhomThuoc = kq.Key.TenNhom,
                             MaDV = kq.Key.MaDV,
                             SoDK = kq.Key.SoDK,
                             DonVi = kq.Key.DonVi,
                             TenThuoc = kq.Key.TenDV,
                             HamLuong = kq.Key.HamLuong,
                             DonGia = kq.Key.DonGia,
                             SoLuongNT = kq.Sum(p => p.vpct.SoLuong),
                             ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                         }).ToList().Select(c => new KetQua
                         {
                             Status = c.Status == null ? 0 : c.Status.Value,
                             DTuong = c.DTuong,
                             NoiTru = c.NoiTru == null ? 0 : c.NoiTru.Value,
                             TrongBH =  c.TrongBH,
                             MaKP = c.MaKP,
                             MaQD = c.MaQD,
                             IdNhom = c.IdNhom,
                             TenNhomThuoc = c.TenNhomThuoc,
                             MaDV = c.MaDV,
                             SoDK = c.SoDK,
                             DonVi = c.DonVi,
                             TenThuoc = c.TenThuoc,
                             HamLuong = c.HamLuong,
                             DonGia = c.DonGia,
                             SoLuongNT = c.SoLuongNT == null ? 0 : c.SoLuongNT,
                             ThanhTien = c.ThanhTien == null ? 0 : c.ThanhTien
                         }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                    if (idnhom > 0)
                    {
                        if (_makp== null)
                        {
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.IdNhom == idnhom
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = que.ToList();
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
                                exSheet.Name = "bcthuoc_21";// gán tên sheet
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
                                foreach (var a in que)
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
                                    r7.Value2 = a.SoLuongNT;
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
                                exQLBV.SaveAs("C:\\Bieu21.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.IdNhom == idnhom
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupKhoaphong2.Text.ToUpper();
                            rep.DataSource = que.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        if (_makp== null)
                        {
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.Status == 2
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.DataSource = que.ToList();
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
                                exSheet.Name = "bcthuoc_21";// gán tên sheet
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
                                foreach (var a in que)
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
                                    r7.Value2 = a.SoLuongNT;
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
                                exQLBV.SaveAs("C:\\Bieu21.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                            List<KetQua> que =
                                (from k in _lKQ
                                 where k.NoiTru == _noitru && k.DTuong == _doituong && k.TrongBH == trongBH
                                 where k.Status == 2 && k.MaKP == _makp
                                 group new { k } by new { k.IdNhom, k.TenNhomThuoc, k.MaDV, k.TenThuoc, k.HamLuong, k.SoDK, k.DonVi, k.DonGia } into kq
                                 select new KetQua
                                 {
                                     IdNhom = kq.Key.IdNhom,
                                     TenNhomThuoc = kq.Key.TenNhomThuoc,
                                     MaDV = kq.Key.MaDV,
                                     SoDK = kq.Key.SoDK,
                                     DonVi = kq.Key.DonVi,
                                     TenThuoc = kq.Key.TenThuoc,
                                     HamLuong = kq.Key.HamLuong,
                                     DonGia = kq.Key.DonGia,
                                     SoLuongNT = kq.Sum(p => p.k.SoLuongNT),
                                     ThanhTien = kq.Sum(p => p.k.ThanhTien)
                                 }).OrderBy(p => p.TenNhomThuoc).OrderBy(p => p.TenThuoc).ToList();
                            rep.paramKhoaPhong.Value = lupKhoaphong2.Text.ToUpper();
                            rep.DataSource = que.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
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
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = (from kp in data.KPhongs
                     where (kp.PLoai== ("Lâm sàng") || kp.PLoai== ("Phòng khám"))
                     select new { kp.TenKP, kp.MaKP }).ToList();
            lupKhoaphong2.Properties.DataSource = q.ToList();
            var nhom = data.NhomDVs.Where(p => p.Status == 2).OrderBy(p => p.TenNhom).ToList();
            lupNhom.Properties.DataSource = nhom;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

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
            private double soLuongNT;
            private double soLuongNgT;
            private double thanhTien;
            private double thanhTienTong;
            private string maCC;
            private int status;
            public int Status
            {
                get { return status; }
                set { status = value; }
            }
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
            public double SoLuongNT
            {
                get { return soLuongNT; }
                set { soLuongNT = value; }
            }
            public double SoLuongNgT
            {
                get { return soLuongNgT; }
                set { soLuongNgT = value; }
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