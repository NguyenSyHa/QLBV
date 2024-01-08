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

namespace QLBV.FormDanhMuc
{
    public partial class frm_Excell_CV908 : DevExpress.XtraEditors.XtraForm
    {
        public frm_Excell_CV908()
        {
            InitializeComponent();
        }
       
        private void frm_Excell_CV908_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
         
          var  _lnhom =(from nhom in data.NhomDVs.Where(p=>p.Status==1) select new{nhom.TenNhomCT,nhom.IDNhom}).OrderBy(p => p.TenNhomCT).ToList();
     
            cklNhom.DataSource = _lnhom;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<NhomDV> _lnhom = new List<NhomDV>();
            for (int i = 0; i < cklNhom.ItemCount; i++)
            {
                if (cklNhom.GetItemChecked(i))
                {
                    _lnhom.Add(new NhomDV { IDNhom = cklNhom.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklNhom.GetItemValue(i)) });
                }
            }
            var qdv = (from idnhom in _lnhom
                       join dv in data.DichVus.Where(p => p.Status == 1) on idnhom.IDNhom equals dv.IDNhom
                       join dvex in data.DichVuExes on dv.MaDV equals dvex.MaDV into kq
                       from b in kq.DefaultIfEmpty()
                       select new { dv,MaHC=b==null?"": b.MaHC}).ToList();
            List<NhaCC> _lnhacc = new List<NhaCC>();

            _lnhacc = data.NhaCCs.ToList();
            if (rad_mau.SelectedIndex == 0)
            {
                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@","dd/MM/yyyy" , "@", "0", "0", "@" };
                string[] _tieude = { "stt", "ma_hoat_chat", "hoat_chat", "ma_duong_dung", "duong_dung", "ham_luong", "ten_thuoc", "so_dang_ky", "dong_goi", "don_vi_tinh", "don_gia", "don_gia_tt", "so_luong", "ma_cskcb", "hang_sx", "nuoc_sx", "nha_thau", "quyet_dinh", "cong_bo", "ma_thuoc_bv", "loai_thuoc", "loai_thau", "nhom_thau" };
                string _filePath =DungChung.Bien.xmlFilePath_LIS[6]+ "\\CV_908_Thuoc.xls";
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[qdv.Count + 1, 23];
                for (int i = 0; i < 23; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in qdv)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaHC;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.dv.TenHC;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.dv.MaDuongDung;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.dv.DuongD;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.dv.HamLuong;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.dv.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.dv.SoDK;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.dv.QCPC;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.dv.DonVi;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.dv.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.dv.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.dv.SLuong;
                    DungChung.Bien.MangHaiChieu[num, 13] = DungChung.Bien.MaBV;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.dv.NhaSX;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.dv.NuocSX;
                    DungChung.Bien.MangHaiChieu[num, 16] = _lnhacc.Where(p => p.MaCC == r.dv.MaCC).Select(p => p.TenCC).ToList().Count > 0 ? _lnhacc.Where(p => p.MaCC == r.dv.MaCC).Select(p => p.TenCC).ToList().First() : "";
                    DungChung.Bien.MangHaiChieu[num, 17] = r.dv.SoQD;
                    string ngaycb = r.dv.NgayQD == null ? "" : (r.dv.NgayQD.Value.ToShortDateString());
                    DungChung.Bien.MangHaiChieu[num, 18] = ngaycb;

                    DungChung.Bien.MangHaiChieu[num, 19] = r.dv.MaTam;
                    DungChung.Bien.MangHaiChieu[num, 20] = r.dv.LThuoc;
                    DungChung.Bien.MangHaiChieu[num, 21] = r.dv.LThau;
                    DungChung.Bien.MangHaiChieu[num, 22] = r.dv.NhomThau;
                    num++;
                }
              xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Bang1_Thuoc", _filePath, true);
            }
            else {
                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "dd/MM/yyyy", "0", "@", "@", "0", "0", "@" };
                string[] _tieude = { "stt", "ma_nhom_vtyt", "ten_nhom_vtyt", "ma_hieu", "ma_vtyt_bv", "ten_vtyt_bv", "quy_cach", "nuoc_sx", "hang_sx", "don_vi_tinh", "don_gia", "don_gia_tt", "nha_thau", "quyet_dinh", "cong_bo", "dinh_muc", "so_luong", "ma_cskcb", "loai_thuoc", "loai_thau", "nhom_thau" };
                string _filePath =DungChung.Bien.xmlFilePath_LIS[6]+ "\\CV_908_VTYT.xls";
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[qdv.Count + 1, 21];
                for (int i = 0; i < 21; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in qdv)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.MaHC;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.dv.TenHC;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.dv.SoDK;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.dv.MaTam;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.dv.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.dv.QCPC;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.dv.NuocSX;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.dv.NhaSX;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.dv.DonVi;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.dv.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.dv.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 12] = _lnhacc.Where(p => p.MaCC == r.dv.MaCC).Select(p => p.TenCC).ToList().Count > 0 ? _lnhacc.Where(p => p.MaCC == r.dv.MaCC).Select(p => p.TenCC).ToList().First() : "";
                    DungChung.Bien.MangHaiChieu[num, 13] = r.dv.SoQD;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.dv.NgayQD == null ? "" : r.dv.NgayQD.Value.ToShortDateString();
                    DungChung.Bien.MangHaiChieu[num, 15] =r.dv.DinhMuc;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.dv.SLuong;
                    DungChung.Bien.MangHaiChieu[num, 17] = DungChung.Bien.MaBV;
                    DungChung.Bien.MangHaiChieu[num, 18] = r.dv.LThuoc;
                    DungChung.Bien.MangHaiChieu[num, 19] = r.dv.LThau;
                    DungChung.Bien.MangHaiChieu[num, 20] = r.dv.NhomThau;
                    num++;
                }
                xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Bang2_VTYT", _filePath, true);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #region _xuất Excel
        private static bool checkValidate(bool AutoFomat, int lengthColum, int lengthFomat)
        {
            if (AutoFomat == false && lengthColum != lengthFomat)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Hàm xuất Excel 
        /// </summary>
        /// <param name="mangHaiChieu">Mảng 2 chiều truyền vào Object[,] </param>
        /// <param name="arrcolName">Mảng 2 chiều tên cột và kiểu fomat của cột (String [,] arrcolName = = new String[] {{"Tên cột 1","Tên cột 2",...},{"Fomat cột 1", "Fomat cột 2",...}})</param>
        /// <param name="arrWidth">Mảng int xác định độ rộng của các cột theo thứ tự  int[] arrWidth = new int[] {10,40,...} (hoặc checkAutoWith = true ==> int[] arrWidth = new int[]{})</param>
        /// <param name="sheetName">Tên Sheet trong file Excel xuất ra </param>
        /// <param name="path"> Đường dẫn xuất file</param>
        /// <param name="checkAutoWith"> Auto Fomat độ rộng cột hoặc không</param>
        /// <returns>trả về "True" nếu xuất thành công và ngược lại</returns>
        public static bool xuatExcelArr(Object[,] mangHaiChieu, string[] arrcolName, int[] arrWidth, string sheetName, string path, bool checkAutoWith)
        {

            try
            {
                bool rs = false;
                int row = mangHaiChieu.GetLength(0);
                int col = arrcolName.Length;
                if (checkValidate(checkAutoWith, col, arrWidth.Length)) 
                {
                    // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = sheetName;

                    //  new Object[_ldv.Count, 5];                     
                    // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                    int k = 0;
                    foreach (var b in arrcolName)
                        for (int i = 0; i < arrcolName.Length; i++)
                        {
                            k++;

                            exSheet.Range[exSheet.Cells[2, i + 1], exSheet.Cells[row + 1, i + 1]].NumberFormat = arrcolName[i];
                        }


                    // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------                
                    exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = mangHaiChieu;
                    //Fomat độ rộng các cột ------------ -------------------------------------------------------------------------------------------
                    for (int i = 0; i < arrcolName.Length; i++)
                    {
                        Microsoft.Office.Interop.Excel.Range er = exSheet.Range[exSheet.Cells[2, i + 1], exSheet.Cells[row + 1, i + 1]];
                        if (checkAutoWith)
                            er.EntireColumn.AutoFit();
                        else
                        {
                            er.EntireColumn.ColumnWidth = arrWidth[i];
                            er.WrapText = true;
                        }
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(path, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                        rs = true;
                    }
                    catch (Exception ex)
                    {
                        rs = false;
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }
                }
                else
                    rs = false;
                return rs;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
    }
}