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
    public partial class frmTsBcNXTrutgon : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNXTrutgon()
        {
            InitializeComponent();
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
            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32(lupKho.EditValue);
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                bool hienhom = true;
                hienhom = chkHienthi.Checked;
                BaoCao.repBcNXTrutgon rep = new BaoCao.repBcNXTrutgon(hienhom, true);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                rep.Kho.Value = _kho;
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                if (qtenkho.Count > 0)
                {
                    rep.Kho.Value = qtenkho.First().TenKP;
                }
                var qtenncc = (from nhapd in data.NhapDs
                               join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                               where (nhacc.MaCC == _nhacc)
                               select new { nhacc.TenCC }).ToList();
                if (qtenncc.Count > 0)
                {
                    rep.NhaCC.Value = qtenncc.First().TenCC;
                }

                //*********************************************

                if (_kho>0)
                {

                    var qnxt2 = (from nhapd in data.NhapDs.Where(p => p.MaKP == _kho)
                                 join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                 join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                 where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                                 select new {tn.STT, nhapd.NgayNhap, dv.MaCC, tn.TenTN, nhomdv.TenNhom, dv.TenDV, dv.MaDV, dv.DonVi, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX }).ToList();
                    var qnxt = (from a in qnxt2
                                where (_nhacc == "" ? true : a.MaCC == _nhacc)
                                group a by new {a.STT, a.MaCC, a.TenTN, a.TenNhom, a.TenDV, a.DonVi, a.DonGia, a.MaDV } into kq
                                select new
                                {
                                   kq.Key.STT,
                                    kq.Key.MaCC,
                                    TenNhomDuoc = kq.Key.TenNhom,
                                    TenTieuNhomDuoc = kq.Key.TenTN,
                                    TenHamLuong = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,

                                    TonDKSL = (kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX)),
                                    TonDKTT =  kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),

                                    NhapTKSL =  kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                    NhapTKTT = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),

                                    XuatTKTongSL =  kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    xuatTKTongTT =  kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                }).Where(p => p.TonDKSL != null || p.TonCKSL != null || p.NhapTKSL!=null || p.XuatTKTongSL !=null).ToList().OrderBy(p => p.TenHamLuong).ToList();
                  
                        rep.DataSource = qnxt.OrderBy(p=>p.STT).ThenBy(p => p.TenHamLuong).ToList();
                        #region xuat Excel
                        if (Xuatex.Checked)
                        {


                            COMExcel.Application exApp = new COMExcel.Application();
                            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                            //exSheet.Activate();
                            exSheet.Name = "BcNhapXuatTon";// gán tên sheet
                            int i = 1;
                            string[] _arr = new string[12] { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Xuất TK - SL", "Xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            int k = 0;
                            var qexcel = qnxt.OrderBy(p => p.TenHamLuong).Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL > 0 || p.NhapTKSL > 0).ToList();
                            foreach (var b in _arr)
                            {
                                k++;
                                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                r.Value2 = b.ToString();
                                r.Columns.AutoFit();
                            }
                            foreach (var a in qexcel)
                            {
                                i++;
                                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                r1.Value2 = i - 1;
                                r1.Columns.AutoFit();
                                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                r2.NumberFormat = "@";
                                if (a.TenHamLuong != null)
                                    r2.Value2 = a.TenHamLuong;
                                r2.Columns.AutoFit();
                                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                r3.NumberFormat = "@";
                                r3.Value2 = a.DVT;
                                r3.Columns.AutoFit();
                                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                r4.NumberFormat = "0";
                                r4.Value2 = a.DonGia;
                                r4.Columns.AutoFit();
                                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                r5.NumberFormat = "0";
                                r5.Value2 = a.TonDKSL;
                                r5.Columns.AutoFit();
                                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                r6.NumberFormat = "0";
                                r6.Value2 = a.TonDKTT;
                                r6.Columns.AutoFit();
                                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                r7.NumberFormat = "0";
                                r7.Value2 = a.NhapTKSL;
                                r7.Columns.AutoFit();
                                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                r8.NumberFormat = "0";
                                r8.Value2 = a.NhapTKTT;
                                r8.Columns.AutoFit();
                                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                r9.NumberFormat = "0";
                                r9.Value2 = a.XuatTKTongSL;
                                r9.Columns.AutoFit();
                                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                r10.NumberFormat = "0";
                                r10.Value2 = a.xuatTKTongTT;
                                r10.Columns.AutoFit();
                                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                r11.NumberFormat = "0";
                                r11.Value2 = a.TonCKSL;
                                r11.Columns.AutoFit();
                                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                r12.NumberFormat = "0";
                                r12.Value2 = a.TonCKTT;
                                r12.Columns.AutoFit();

                            }
                            exApp.Visible = true;//Ẩn hiện chương trình
                            exQLBV.SaveAs("C:\\BcNXTrutgon.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                            //exQLBV.Close(false, false, false);
                            //exApp.Quit(); // thoát ứng dụng
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }
                        #endregion

                  
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn kho");
                    lupKho.Focus();
                }

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data;
        private void frmTsBcNXTthongthuong_Load(object sender, EventArgs e)
        {
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
         data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();

            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            _lnhom=data.NhomDVs.Where(p => p.Status == 1).OrderBy(p => p.TenNhom).ToList();
            _lnhom.Add(new NhomDV { TenNhom = " Tất cả", IDNhom = 0 });
            lupNhom.Properties.DataSource = _lnhom.OrderBy(p=>p.TenNhom);
            
        }
        List<TieuNhomDV> _ltieunhom = new List<TieuNhomDV>();
        List<NhomDV> _lnhom = new List<NhomDV>();
        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int idnhom = 0;
            if (lupNhom.EditValue != null)
                idnhom = Convert.ToInt32(lupNhom.EditValue);
            if (idnhom == 0)
            {
                lupTieuNhom.Properties.DataSource = null;
            }
            else
                _ltieunhom = data.TieuNhomDVs.Where(p => p.IDNhom == idnhom).OrderBy(p => p.TenTN).ToList();
            lupTieuNhom.Properties.DataSource = data.TieuNhomDVs.Where(p => p.IDNhom == idnhom).OrderBy(p => p.TenTN).ToList();
        }
    }
}