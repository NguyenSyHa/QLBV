using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.ComponentModel;
using COMExcel = Microsoft.Office.Interop.Excel;
using OpenXmlPackaging;
using System.Xml.Linq;

namespace QLBV.DungChung
{
    public class Cls79_80
    {
        #region xuất Excel
        private static void setTenCot(DataTable dt)
        {
            //Type myType = typeof(Cls79_80.cl_79_80);
            //FieldInfo[] myField = myType.GetFields();
            string[] _arr = new string[] { "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtyt", "t_dvkt_tyle", "t_thuoc_tyle", "t_vtyt_tyle", "t_kham", "t_vchuyen", "t_bntt", "t_bhtt", "ma_khoa", "nam_qt", "thang_qt", "ma_khuvuc", "ma_loaikcb", "ma_cskcb", "noi_ttoan", "giam_dinh", "t_xuattoan", "lydo_xt", "T_datuyen", "T_vuottran" };
            for (int i = 0; i < _arr.Count(); i++)
            {
                dt.Columns.Add(_arr[i]);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_listVPBH"></param>
        /// <param name="_duongdan"></param>
        /// <param name="font"></param> = 0: TCVN3; 1: TimeNewRoment
        /// <returns></returns>
        public static bool xuatExcel(List<cl_79_80> _listVPBH, string _duongdan, bool convert, string font)
        {
            DataTable dt = new DataTable();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            setTenCot(dt);
            // List<KPhong> _lKP = _dataContext.KPhongs.ToList();
            int i = 1;
            foreach (var item in _listVPBH)
            {

                if (item.Ma_cskcb.Length < 5)
                {
                    item.Ma_cskcb = "0" + item.Ma_cskcb;
                }
                DataRow workRow = dt.NewRow();
                workRow["stt"] = i;
                workRow["ma_bn"] = item.Ma_bn;
                workRow["ho_ten"] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                workRow["ngay_sinh"] = item.NSinh;
                if (item.Gioi_tinh == false)
                {
                    workRow["gioi_tinh"] = 2;
                }
                else
                {
                    workRow["gioi_tinh"] = 1;
                }
                workRow["dia_chi"] = DungChung.Ham.convertFont(convert, item.Dia_chi, font);
                workRow["ma_the"] = item.Ma_the;
                workRow["ma_dkbd"] = item.Ma_cskcb.ToString();
                if (item.Gt_the_tu != null)
                {
                    workRow["gt_the_tu"] = item.Gt_the_tu.ToString("yyyyMMdd");
                }
                if (item.Gt_the_den != null)
                {
                    workRow["gt_the_den"] = item.Gt_the_den.ToString("yyyyMMdd");
                }
                workRow["ma_benh"] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0]; //item.Ma_benh;
                workRow["ma_benhkhac"] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[1];
                workRow["ma_lydo_vvien"] = item.Ma_lydo_vvien;
                workRow["ma_noi_chuyen"] = item.Ma_noi_chuyen;
                if (item.Ngay_vao != null)
                {
                    workRow["ngay_vao"] = item.Ngay_vao.ToString("yyyyMMddHHmm");
                }
                if (item.NgayTT != null)
                {

                    workRow["ngay_ra"] = item.NgayTT.ToString("yyyyMMddHHmm");

                }
                workRow["so_ngay_dtri"] = item.So_ngay_dtri; ;

                var kq = _dataContext.RaViens.Where(p => p.MaBNhan == item.Ma_bn).Select(p => new { p.KetQua, p.Status }).ToList();
                if (kq.Count > 0)
                {
                    if (kq.First().KetQua == "Khỏi")
                        workRow["ket_qua_dtri"] = 1;
                    if (kq.First().KetQua == "Đỡ|Giảm")
                        workRow["ket_qua_dtri"] = 2;
                    if (kq.First().KetQua == "Không T.đổi")
                        workRow["ket_qua_dtri"] = 3;
                    if (kq.First().KetQua == "Nặng hơn")
                        workRow["ket_qua_dtri"] = 4;
                    if (kq.First().KetQua == "Tử vong")
                        workRow["ket_qua_dtri"] = 5;

                    if (Convert.ToInt16(kq.First().Status) == 1)
                        workRow["tinh_trang_rv"] = 2;
                    if (Convert.ToInt16(kq.First().Status) == 2)
                        workRow["tinh_trang_rv"] = 1;
                    if (Convert.ToInt16(kq.First().Status) == 3)
                        workRow["tinh_trang_rv"] = 3;
                    if (Convert.ToInt16(kq.First().Status) == 4)
                        workRow["tinh_trang_rv"] = 4;

                }
                workRow["t_tongchi"] = item.T_tongchi;
                workRow["t_xn"] = item.T_xn;
                workRow["t_cdha"] = item.T_cdha;
                workRow["t_thuoc"] = item.T_thuoc;
                workRow["t_mau"] = item.T_mau;
                workRow["t_pttt"] = item.T_pttt;
                workRow["t_vtyt"] = item.T_vtyt;
                workRow["t_dvkt_tyle"] = item.T_dvkt_tyle;
                workRow["t_thuoc_tyle"] = item.T_thuoc_tyle;
                workRow["t_vtyt_tyle"] = item.T_vtyt_tyle;
                workRow["t_kham"] = item.T_kham;
                workRow["t_vchuyen"] = item.T_vchuyen;
                workRow["t_bntt"] = item.T_bntt;
                workRow["t_bhtt"] = item.T_bhtt;
                workRow["ma_khoa"] = item.MaKhoaTongKet;
                if (item.NgayTT != null)
                {
                    workRow["nam_qt"] = item.NgayTT.Year;
                }
                if (item.NgayTT != null)
                {
                    workRow["thang_qt"] = item.NgayTT.Month;
                }
                if (item.Ma_khuvuc != null)
                { workRow["ma_khuvuc"] = item.Ma_khuvuc; }
                else { workRow["ma_khuvuc"] = ""; }
                if (item.Ma_loaikcb == 1)// điều trị nội trú
                {
                    workRow["ma_loaikcb"] = 2;
                }
                else
                {
                    if (item.Ma_loaikcb == 1) workRow["ma_loaikcb"] = 3;
                    if (item.Ma_loaikcb == 0) workRow["ma_loaikcb"] = 1;
                }
                workRow["ma_cskcb"] = item.Ma_cskcb;
                workRow["noi_ttoan"] = "";
                workRow["giam_dinh"] = "";
                workRow["t_xuattoan"] = "";
                workRow["lydo_xt"] = "";
                workRow["T_datuyen"] = "";
                workRow["T_vuottran"] = "";
                dt.Rows.Add(workRow);
                i++;
            }
            try
            {

                using (var doc = new SpreadsheetDocument(_duongdan))
                {
                    string tenbieu = "Biểu 79a";
                    int _loaiKCB = -1;
                    if (_listVPBH.Count > 0)
                        _loaiKCB = _listVPBH.First().Ma_loaikcb;
                    if (_loaiKCB == 3)
                        tenbieu = "Biểu 80a";
                    Worksheet sheet1 = doc.Worksheets.Add(tenbieu);
                    //   sheet1.Cells["H3"].Style.NumberFormat = new NumberFormat("@");                  
                    sheet1.ImportDataTable(dt, "A1", true);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            #endregion
        }
        public static bool xuatExcelRange(List<cl_79_80> _listVPNgT, string path, bool convert, string font, bool inCongKham_NoiTru)
        {
            bool rs = false;
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Biểu 79a";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";
            exSheet.Name = tenbieu;

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr;
            if (inCongKham_NoiTru)
                _arr = new string[] { "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtyt", "t_dvkt_tyle", "t_thuoc_tyle", "t_vtyt_tyle", "t_kham", "t_vchuyen", "t_bntt", "t_bhtt", "t_ngoaids", "ma_khoa", "nam_qt", "thang_qt", "ma_khuvuc", "ma_loaikcb", "ma_cskcb", "noi_ttoan", "giam_dinh", "t_xuattoan", "lydo_xt", "T_datuyen", "T_vuottran", "t_giuong" };
            else
                _arr = new string[] { "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtyt", "t_dvkt_tyle", "t_thuoc_tyle", "t_vtyt_tyle", "t_kham", "t_vchuyen", "t_bntt", "t_bhtt", "t_ngoaids", "ma_khoa", "nam_qt", "thang_qt", "ma_khuvuc", "ma_loaikcb", "ma_cskcb", "noi_ttoan", "giam_dinh", "t_xuattoan", "lydo_xt", "T_datuyen", "T_vuottran", "t_giuong" };

            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _listVPNgT.Count;
            int col = _arr.Length;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
            if (row > 0)
            {
                int num = 0;
                foreach (var item in _listVPNgT)
                {
                    // item.Ma_cskcb = item.Ma_cskcb;
                    _arr2[num, 0] = num + 1;
                    _arr2[num, 1] = item.Ma_bn;
                    _arr2[num, 2] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                    _arr2[num, 3] = item.Ngay_sinh;
                    _arr2[num, 4] = item.Gioi_tinh == false ? 2 : 1;
                    _arr2[num, 5] = DungChung.Ham.convertFont(convert, item.Dia_chi, font);
                    _arr2[num, 6] = item.Ma_the;
                    _arr2[num, 7] = item.Ma_dkbd;
                    if (item.Gt_the_tu != null)
                    {

                        _arr2[num, 8] = item.Gt_the_tu.ToString("yyyyMMdd"); ;
                    }
                    if (item.Gt_the_den != null)
                    {


                        _arr2[num, 9] = item.Gt_the_den.ToString("yyyyMMdd"); ;
                    }
                    _arr2[num, 10] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                    _arr2[num, 11] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[1];
                    _arr2[num, 12] = item.Ma_lydo_vvien;
                    _arr2[num, 13] = item.Ma_noi_chuyen;
                    if (item.Ngay_vao != null)
                    {

                        _arr2[num, 14] = item.Ngay_vao.ToString("yyyyMMddHHmm");
                    }
                    if (item.NgayTT != null)
                    {

                        _arr2[num, 15] = item.Ngay_ra.ToString("yyyyMMddHHmm");

                    }

                    _arr2[num, 16] = item.So_ngay_dtri;
                    _arr2[num, 17] = item.Ket_qua_dtri;
                    _arr2[num, 18] = item.Tinh_trang_rv;
                    _arr2[num, 19] = item.T_tongchi;
                    _arr2[num, 20] = item.T_xn;
                    _arr2[num, 21] = item.T_cdha;
                    _arr2[num, 22] = item.T_thuoc;
                    _arr2[num, 23] = item.T_mau;
                    _arr2[num, 24] = item.T_pttt;
                    _arr2[num, 25] = item.T_vtyt;
                    _arr2[num, 26] = item.T_dvkt_tyle;
                    _arr2[num, 27] = item.T_thuoc_tyle;
                    _arr2[num, 28] = item.T_vtyt_tyle;
                    _arr2[num, 29] = item.T_kham;  //inCongKham_NoiTru ? item.T_kham : (item.T_kham + item.T_giuong);//; //(tenbieu == "Biểu 80a" && !inCongKham_NoiTru) ?  (item.T_kham + item.T_congkhamNoiTru) : item.T_kham ;
                    _arr2[num, 30] = item.T_vchuyen;
                    _arr2[num, 31] = item.T_bntt;
                    _arr2[num, 32] = item.T_bhtt;
                    _arr2[num, 33] = item.T_vchuyen;
                    _arr2[num, 34] = item.MaKhoaTongKet;
                    if (item.NgayTT != null)
                    {
                        _arr2[num, 35] = item.NgayTT.Year;
                    }
                    if (item.NgayTT != null)
                    {
                        _arr2[num, 36] = item.NgayTT.Month;
                    }
                    if (item.Ma_khuvuc != null)
                    { _arr2[num, 37] = item.Ma_khuvuc == null ? "" : item.Ma_khuvuc; }
                    else { _arr2[num, 37] = ""; }
                    //if (item.Ma_loaikcb == 1)// điều trị nội trú
                    //{
                    //    _arr2[num, 38] = 2;
                    //}
                    //else
                    //{
                    //    if (item.Ma_loaikcb == 1) _arr2[num, 38] = 3;
                    //    if (item.Ma_loaikcb == 0) _arr2[num, 38] = 1;
                    //}
                    if (item.Ma_loaikcb != null)
                        _arr2[num, 38] = item.Ma_loaikcb;
                    _arr2[num, 39] = item.Ma_cskcb;
                    _arr2[num, 40] = (DungChung.Bien.MaBV == "26007" && item.Ma_cskcb == DungChung.Bien.MaBV) ? "1" : "";
                    _arr2[num, 41] = "";
                    _arr2[num, 42] = "";
                    _arr2[num, 43] = "";
                    _arr2[num, 44] = "";
                    _arr2[num, 45] = "";
                    if (inCongKham_NoiTru || DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280")
                        _arr2[num, 46] = item.T_giuong; //tiền giường
                    num++;
                }
                // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format
                exSheet.Range[exSheet.Cells[2, 2], exSheet.Cells[row + 1, 2]].NumberFormat = "@";// ma_benhnhan
                exSheet.Range[exSheet.Cells[2, 8], exSheet.Cells[row + 1, 8]].NumberFormat = "@";// ma_dkbd
                exSheet.Range[exSheet.Cells[2, 14], exSheet.Cells[row + 1, 14]].NumberFormat = "@";// ma_noi_chuyen
                exSheet.Range[exSheet.Cells[2, 15], exSheet.Cells[row + 1, 15]].NumberFormat = "@";// ngày vào
                exSheet.Range[exSheet.Cells[2, 16], exSheet.Cells[row + 1, 16]].NumberFormat = "@";// ngày ra               
                exSheet.Range[exSheet.Cells[2, 36], exSheet.Cells[row + 1, 36]].NumberFormat = "@";// năm quyết toán
                exSheet.Range[exSheet.Cells[2, 37], exSheet.Cells[row + 1, 37]].NumberFormat = "@";// tháng quyết toán 
                exSheet.Range[exSheet.Cells[2, 40], exSheet.Cells[row + 1, 40]].NumberFormat = "@";// ma_cskcb 

                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
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

            return rs;
        }
        public static bool xuatExcelRangeQD917(List<cl_79_80> _listVPNgT, string path, bool convert, string font)
        {
            bool rs = false;
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Biểu 79a";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";
            exSheet.Name = tenbieu;

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr;
            if (DungChung.Bien.MaBV != "24009") // thêm 1 số cột đằng sau mã cskcb
                _arr = new string[] { "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtyt", "t_dvkt_tyle", "t_thuoc_tyle", "t_vtyt_tyle", "t_kham", "t_giuong", "t_vchuyen", "t_bntt", "t_bhtt", "t_ngoaids", "ma_khoa", "nam_qt", "thang_qt", "ma_khuvuc", "ma_loaikcb", "ma_cskcb", "t_nguonkhac" };

            else
                _arr = new string[] { "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtyt", "t_dvkt_tyle", "t_thuoc_tyle", "t_vtyt_tyle", "t_kham", "t_giuong", "t_vchuyen", "t_bntt", "t_bhtt", "t_ngoaids", "ma_khoa", "nam_qt", "thang_qt", "ma_khuvuc", "ma_loaikcb", "ma_cskcb", "t_nguonkhac", "noi_ttoan", "giam_dinh", "t_xuattoan", "lyso_xt", "t_datuyen", "t_vuottran", "ma_dt", "sodk_kcb" };


            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _listVPNgT.Count;
            int col = _arr.Length;
            int lamtron = 2;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
            if (row > 0)
            {
                int num = 0;
                foreach (var item in _listVPNgT)
                {
                    // item.Ma_cskcb = item.Ma_cskcb;
                    _arr2[num, 0] = num + 1;
                    _arr2[num, 1] = item.Ma_bn;
                    _arr2[num, 2] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                    _arr2[num, 3] = item.Ngay_sinh;
                    _arr2[num, 4] = item.Gioi_tinh == false ? 2 : 1;
                    _arr2[num, 5] = DungChung.Ham.convertFont(convert, item.Dia_chi, font);
                    _arr2[num, 6] = item.Ma_the;
                    _arr2[num, 7] = item.Ma_dkbd;
                    if (item.Gt_the_tu != null)
                    {

                        _arr2[num, 8] = item.Gt_the_tu.ToString("yyyyMMdd"); ;
                    }
                    if (item.Gt_the_den != null)
                    {


                        _arr2[num, 9] = item.Gt_the_den.ToString("yyyyMMdd"); ;
                    }
                    _arr2[num, 10] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                    _arr2[num, 11] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[1];
                    _arr2[num, 12] = item.Ma_lydo_vvien;
                    _arr2[num, 13] = item.Ma_noi_chuyen;
                    if (item.Ngay_vao != null)
                    {

                        _arr2[num, 14] = item.Ngay_vao.ToString("yyyyMMddHHmm");
                    }
                    if (item.NgayTT != null)
                    {

                        _arr2[num, 15] = item.Ngay_ra.ToString("yyyyMMddHHmm");

                    }

                    _arr2[num, 16] = item.So_ngay_dtri;
                    _arr2[num, 17] = item.Ket_qua_dtri;
                    _arr2[num, 18] = item.Tinh_trang_rv;
                    _arr2[num, 19] = Math.Round(item.T_tongchi, lamtron, MidpointRounding.AwayFromZero);
                    _arr2[num, 20] = Math.Round(item.T_xn, lamtron);
                    _arr2[num, 21] = Math.Round(item.T_cdha, lamtron);
                    _arr2[num, 22] = Math.Round(item.T_thuoc, lamtron);
                    _arr2[num, 23] = Math.Round(item.T_mau, lamtron);
                    _arr2[num, 24] = Math.Round(item.T_pttt, lamtron);
                    _arr2[num, 25] = Math.Round(item.T_vtyt, lamtron);
                    _arr2[num, 26] = Math.Round(item.T_dvkt_tyle, lamtron);
                    _arr2[num, 27] = Math.Round(item.T_thuoc_tyle, lamtron);
                    _arr2[num, 28] = Math.Round(item.T_vtyt_tyle, lamtron);
                    _arr2[num, 29] = Math.Round(item.T_kham, lamtron);  //inCongKham_NoiTru ? item.T_kham : (item.T_kham + item.T_giuong);//; //(tenbieu == "Biểu 80a" && !inCongKham_NoiTru) ?  (item.T_kham + item.T_congkhamNoiTru) : item.T_kham ;
                    _arr2[num, 30] = Math.Round(item.T_giuong, lamtron);
                    _arr2[num, 31] = Math.Round(item.T_vchuyen, lamtron);
                    _arr2[num, 32] = Math.Round(item.T_bntt, lamtron, MidpointRounding.AwayFromZero);
                    _arr2[num, 33] = Math.Round(item.T_bhtt, lamtron, MidpointRounding.AwayFromZero);
                    _arr2[num, 34] = Math.Round(item.T_ngoaids, lamtron);
                    _arr2[num, 35] = item.MaKhoaTongKet;
                    if (item.NgayTT != null)
                    {
                        _arr2[num, 36] = item.NgayTT.Year;
                    }
                    if (item.NgayTT != null)
                    {
                        _arr2[num, 37] = item.NgayTT.Month;
                    }
                    if (item.Ma_khuvuc != null)
                    { _arr2[num, 38] = item.Ma_khuvuc == null ? "" : item.Ma_khuvuc; }
                    else { _arr2[num, 38] = ""; }
                    //if (item.Ma_loaikcb == 1)// điều trị nội trú
                    //{
                    //    _arr2[num, 38] = 2;
                    //}
                    //else
                    //{
                    //    if (item.Ma_loaikcb == 1) _arr2[num, 38] = 3;
                    //    if (item.Ma_loaikcb == 0) _arr2[num, 38] = 1;
                    //}
                    if (item.Ma_loaikcb != null)
                        _arr2[num, 39] = item.Ma_loaikcb;
                    _arr2[num, 40] = "'" + item.Ma_cskcb;
                    _arr2[num, 41] = item.T_NguonKhac;
                    if (DungChung.Bien.MaBV == "24009")
                    {
                        _arr2[num, 42] = "CSKCB";
                        _arr2[num, 43] = 0;
                        _arr2[num, 44] = 0;
                        _arr2[num, 45] = "";
                        _arr2[num, 46] = 0;
                        _arr2[num, 47] = 0;
                        _arr2[num, 48] = item.MaDTuong;
                        _arr2[num, 49] = item.SoDK == 0 ? item.Ma_bn : item.SoDK;
                    }
                    num++;
                }
                // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format
                exSheet.Range[exSheet.Cells[2, 2], exSheet.Cells[row + 1, 2]].NumberFormat = "@";// ma_benhnhan
                exSheet.Range[exSheet.Cells[2, 8], exSheet.Cells[row + 1, 8]].NumberFormat = "@";// ma_dkbd
                exSheet.Range[exSheet.Cells[2, 14], exSheet.Cells[row + 1, 14]].NumberFormat = "@";// ma_noi_chuyen
                exSheet.Range[exSheet.Cells[2, 15], exSheet.Cells[row + 1, 15]].NumberFormat = "@";// ngày vào
                exSheet.Range[exSheet.Cells[2, 16], exSheet.Cells[row + 1, 16]].NumberFormat = "@";// ngày ra               
                exSheet.Range[exSheet.Cells[2, 36], exSheet.Cells[row + 1, 36]].NumberFormat = "@";// năm quyết toán
                exSheet.Range[exSheet.Cells[2, 37], exSheet.Cells[row + 1, 37]].NumberFormat = "@";// tháng quyết toán 
                exSheet.Range[exSheet.Cells[2, 40], exSheet.Cells[row + 1, 40]].NumberFormat = "@";// ma_cskcb 

                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
                exApp.Visible = true;
                //try
                //{

                exQLBV.SaveAs(path, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);
                rs = true;
                //}
                //catch (Exception ex)
                //{
                //    rs = false;
                //}
                //finally
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                //}

            }

            return rs;
        }

        /// <summary>
        /// Xuất theo báo cáo
        /// </summary>
        /// <param name="_listVPNgT"></param>
        /// <param name="path"></param>
        /// <param name="convert"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        /// 
        public static bool xuatExcelRangeBC(List<cl_79_80> _listVPNgT, string path, bool convert, string font, bool inCongKham_NoiTru, string ngaythang)
        {
            bool rs = false;
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Biểu 79a";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";
            exSheet.Name = tenbieu;
            if (tenbieu == "Biểu 80a")
            {
                #region BC 80

                // mảng tên cột--------------------------------------------------------------------------------------------------------
                string[] _arr;
                if (inCongKham_NoiTru)
                    _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày vào", "ngày ra",
                    "số ngày điều trị", "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền khám","Tiền giường", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};
                else
                    _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày vào", "ngày ra",
                    "số ngày điều trị", "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền giường", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};



                // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
                int row = _listVPNgT.Count + 11;
                int col = _arr.Length;
                Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử

                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[1, 1];
                r11.Value2 = DungChung.Ham.convertFont(convert, "TÊN CƠ SỞ KCB: " + DungChung.Bien.TenCQ.ToUpper(), font);
                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[1, 7];
                r17.Value2 = DungChung.Ham.convertFont(convert, "Mẫu số: C80a-HD", font);

                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[2, 1];
                r21.Value2 = DungChung.Ham.convertFont(convert, "Mã số: " + DungChung.Bien.MaBV, font);
                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[2, 7];
                r27.Value2 = DungChung.Ham.convertFont(convert, "(Ban hành theo Thông tư số: 178/2012/TT-", font);
                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[3, 7];
                r37.Value2 = DungChung.Ham.convertFont(convert, "BTC ngày 23/10/2012 của Bộ Tài Chính)", font);
                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[4, 1];
                r41.Value2 = DungChung.Ham.convertFont(convert, "DANH SÁCH NGƯỜI BỆNH BẢO HIỂM TẾ NỘI TRÚ ĐỀ NGHỊ THANH TOÁN", font);//tiêu đề
                COMExcel.Range r51 = (COMExcel.Range)exSheet.Cells[5, 1];
                r51.Value2 = DungChung.Ham.convertFont(convert, ngaythang, font);//ngày tháng tìm kiếm
                COMExcel.Range r61 = (COMExcel.Range)exSheet.Cells[6, 1];
                r61.Value2 = DungChung.Ham.convertFont(convert, "(Gửi cùng với file dữ liệu hàng tháng)", font);

                // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                int k = 0;
                foreach (var b in _arr)
                {
                    k++;
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[7, k];
                    r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                    r.Columns.AutoFit();
                    r.Cells.Font.Bold = true;
                }
                if (row > 11)
                {
                    int num = 0;

                    List<cl_79_80> tt = new List<cl_79_80>();

                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 0)// không xác định
                        {
                            tt = _listVPNgT.Where(p => (p.NTinh != 1 && p.NTinh != 2 && p.NTinh != 3) || (p.Tuyen != 1 && p.Tuyen != 2)).ToList();
                        }
                        else if (i == 1)
                        {
                            _arr2[num, 0] = "A. Bệnh nhân nội tỉnh kcb ban đầu";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 2)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 3)
                        {
                            _arr2[num, 0] = "B. Bệnh nhân nội tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 4)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 5)
                        {
                            _arr2[num, 0] = "C. Bệnh nhân Ngoại tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 6)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 2).ToList();
                        }
                        num++;


                        int count = 0;
                        foreach (var item in tt)
                        {
                            // item.Ma_cskcb = item.Ma_cskcb;
                            _arr2[num, 0] = count + 1;//num + 1; 
                            _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                            _arr2[num, 2] = item.Gioi_tinh == true ? item.NSinh : "";
                            _arr2[num, 3] = item.Gioi_tinh == false ? item.NSinh : "";
                            _arr2[num, 4] = item.Ma_the;
                            _arr2[num, 5] = item.Ma_dkbd;
                            _arr2[num, 6] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                            if (item.Ngaykham != null)
                            {
                                _arr2[num, 7] = item.Ngaykham.ToString("dd/MM/yyyy");
                            }
                            if (item.Ngay_ra != null)
                            {
                                _arr2[num, 8] = item.Ngay_ra.ToString("dd/MM/yyyy");

                            }
                            _arr2[num, 9] = item.So_ngay_dtri;
                            _arr2[num, 10] = item.T_tongchi;
                            _arr2[num, 11] = item.T_xn;
                            _arr2[num, 12] = item.T_cdha;
                            _arr2[num, 13] = item.T_thuoc;
                            _arr2[num, 14] = item.T_mau;
                            _arr2[num, 15] = item.T_pttt;
                            _arr2[num, 16] = item.T_vtyt;
                            _arr2[num, 17] = item.T_dvkt_tyle;
                            _arr2[num, 18] = item.T_thuoc_tyle;
                            _arr2[num, 19] = item.T_vtyt_tyle;
                            if (inCongKham_NoiTru)
                            {
                                _arr2[num, 20] = item.T_kham;
                                _arr2[num, 21] = item.T_giuong;
                                _arr2[num, 22] = item.T_vchuyen;
                                _arr2[num, 23] = item.T_bntt;
                                _arr2[num, 24] = item.T_bhtt;
                                _arr2[num, 25] = item.CPNgoaiBH;
                            }
                            else
                            {
                                _arr2[num, 20] = item.T_kham;
                                _arr2[num, 21] = item.T_vchuyen;
                                _arr2[num, 22] = item.T_bntt;
                                _arr2[num, 23] = item.T_bhtt;
                                _arr2[num, 24] = item.CPNgoaiBH;
                            }

                            num++;
                            count++;
                        }
                    }
                    // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format                  
                    exSheet.Range[exSheet.Cells[8, 6], exSheet.Cells[row + 7, 6]].NumberFormat = "@";// ma_dkbd                    
                    exSheet.Range[exSheet.Cells[8, 8], exSheet.Cells[row + 7, 8]].NumberFormat = "@";// ngày vào
                    exSheet.Range[exSheet.Cells[8, 9], exSheet.Cells[row + 7, 9]].NumberFormat = "@";// ngày ra   
                    //-------------------------------------------------------------------------------------------
                    exSheet.Range[exSheet.Cells[8, 1], exSheet.Cells[row + 7, col]].Value = _arr2;
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

                #endregion
            }
            else if (tenbieu == "Biểu 79a")
            {
                #region BC 79

                // mảng tên cột--------------------------------------------------------------------------------------------------------
                string[] _arr;
                _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày khám",
                     "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền khám", "Tiền giường", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};



                // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
                int row = _listVPNgT.Count + 11;
                int col = _arr.Length;
                Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[1, 1];
                r11.Value2 = DungChung.Ham.convertFont(convert, "TÊN CƠ SỞ KCB: " + DungChung.Bien.TenCQ.ToUpper(), font);
                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[1, 7];
                r17.Value2 = DungChung.Ham.convertFont(convert, "Mẫu số: C79a-HD", font);
                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[2, 1];
                r21.Value2 = DungChung.Ham.convertFont(convert, "Mã số: " + DungChung.Bien.MaBV, font);
                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[2, 7];
                r27.Value2 = DungChung.Ham.convertFont(convert, "(Ban hành theo Thông tư số: 178/2012/TT-", font);
                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[3, 7];
                r37.Value2 = DungChung.Ham.convertFont(convert, "BTC ngày 23/10/2012 của Bộ Tài Chính)", font);
                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[4, 1];
                r41.Value2 = DungChung.Ham.convertFont(convert, "DANH SÁCH NGƯỜI BỆNH BẢO HIỂM TẾ NGOẠI TRÚ ĐỀ NGHỊ THANH TOÁN", font);//tiêu đề
                COMExcel.Range r51 = (COMExcel.Range)exSheet.Cells[5, 1];
                r51.Value2 = DungChung.Ham.convertFont(convert, ngaythang, font);//ngày tháng tìm kiếm
                COMExcel.Range r61 = (COMExcel.Range)exSheet.Cells[6, 1];
                r61.Value2 = DungChung.Ham.convertFont(convert, "(Gửi cùng với file dữ liệu hàng tháng)", font);

                // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                int k = 0;
                foreach (var b in _arr)
                {
                    k++;
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[7, k];
                    r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                    r.Columns.AutoFit();
                    r.Cells.Font.Bold = true;
                }

                if (row > 11)
                {
                    int num = 0;
                    List<cl_79_80> tt = new List<cl_79_80>();

                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 0)// không xác định
                        {
                            tt = _listVPNgT.Where(p => (p.NTinh != 1 && p.NTinh != 2 && p.NTinh != 3) || (p.Tuyen != 1 && p.Tuyen != 2)).ToList();
                        }
                        else if (i == 1)
                        {
                            _arr2[num, 0] = "A. Bệnh nhân nội tỉnh kcb ban đầu";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 2)// đúng tuyến
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 3)
                        {
                            _arr2[num, 0] = "B. Bệnh nhân nội tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 4)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 5)
                        {
                            _arr2[num, 0] = "C. Bệnh nhân Ngoại tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 6)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 2).ToList();
                        }
                        num++;
                        int count = 0;
                        foreach (var item in tt)
                        {
                            // item.Ma_cskcb = item.Ma_cskcb;
                            _arr2[num, 0] = count + 1; //num + 1;
                            _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                            _arr2[num, 2] = item.Gioi_tinh == true ? item.NSinh : "";
                            _arr2[num, 3] = item.Gioi_tinh == false ? item.NSinh : "";
                            _arr2[num, 4] = item.Ma_the;
                            _arr2[num, 5] = item.Ma_dkbd;
                            _arr2[num, 6] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                            if (item.Ngaykham != null)
                            {
                                _arr2[num, 7] = item.Ngaykham.ToString("dd/MM/yyyy");
                            }
                            _arr2[num, 8] = item.T_tongchi;
                            _arr2[num, 9] = item.T_xn;
                            _arr2[num, 10] = item.T_cdha;
                            _arr2[num, 11] = item.T_thuoc;
                            _arr2[num, 12] = item.T_mau;
                            _arr2[num, 13] = item.T_pttt;
                            _arr2[num, 14] = item.T_vtyt;
                            _arr2[num, 15] = item.T_dvkt_tyle;
                            _arr2[num, 16] = item.T_thuoc_tyle;
                            _arr2[num, 17] = item.T_vtyt_tyle;
                            _arr2[num, 18] = item.T_kham;
                            _arr2[num, 19] = item.T_giuong;
                            _arr2[num, 20] = item.T_vchuyen;
                            _arr2[num, 21] = item.T_bntt;
                            _arr2[num, 22] = item.T_bhtt;
                            _arr2[num, 23] = item.CPNgoaiBH;

                            num++;
                            count++;
                        }
                    }
                    // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format  
                    exSheet.Range[exSheet.Cells[8, 6], exSheet.Cells[row + 7, 6]].NumberFormat = "@";// mã đkbd
                    exSheet.Range[exSheet.Cells[8, 8], exSheet.Cells[row + 7, 8]].NumberFormat = "@";        // ngày khám       

                    //-------------------------------------------------------------------------------------------
                    exSheet.Range[exSheet.Cells[8, 1], exSheet.Cells[row + 7, col]].Value = _arr2;
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

                #endregion
            }

            return rs;
        }
        public static bool xuatExcelRangeBC_TyleBHTT(List<cl_79_80> _listVPNgT, string path, bool convert, string font, bool inCongKham_NoiTru, string ngaythang)
        {
            bool rs = false;
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Biểu 79a";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";
            exSheet.Name = tenbieu;
            if (tenbieu == "Biểu 80a")
            {
                #region BC 80

                // mảng tên cột--------------------------------------------------------------------------------------------------------
                string[] _arr;
                if (inCongKham_NoiTru)
                    _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ","TyLeBHTT", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày vào", "ngày ra",
                    "số ngày điều trị", "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền khám","Tiền giường", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};
                else
                    _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ","TyLeBHTT", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày vào", "ngày ra",
                    "số ngày điều trị", "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền giường", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};



                // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
                int row = _listVPNgT.Count + 11;
                int col = _arr.Length;
                Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử

                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[1, 1];
                r11.Value2 = DungChung.Ham.convertFont(convert, "TÊN CƠ SỞ KCB: " + DungChung.Bien.TenCQ.ToUpper(), font);
                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[1, 7];
                r17.Value2 = DungChung.Ham.convertFont(convert, "Mẫu số: C80a-HD", font);

                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[2, 1];
                r21.Value2 = DungChung.Ham.convertFont(convert, "Mã số: " + DungChung.Bien.MaBV, font);
                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[2, 7];
                r27.Value2 = DungChung.Ham.convertFont(convert, "(Ban hành theo Thông tư số: 178/2012/TT-", font);
                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[3, 7];
                r37.Value2 = DungChung.Ham.convertFont(convert, "BTC ngày 23/10/2012 của Bộ Tài Chính)", font);
                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[4, 1];
                r41.Value2 = DungChung.Ham.convertFont(convert, "DANH SÁCH NGƯỜI BỆNH BẢO HIỂM TẾ NỘI TRÚ ĐỀ NGHỊ THANH TOÁN", font);//tiêu đề
                COMExcel.Range r51 = (COMExcel.Range)exSheet.Cells[5, 1];
                r51.Value2 = DungChung.Ham.convertFont(convert, ngaythang, font);//ngày tháng tìm kiếm
                COMExcel.Range r61 = (COMExcel.Range)exSheet.Cells[6, 1];
                r61.Value2 = DungChung.Ham.convertFont(convert, "(Gửi cùng với file dữ liệu hàng tháng)", font);

                // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                int k = 0;
                foreach (var b in _arr)
                {
                    k++;
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[7, k];
                    r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                    r.Columns.AutoFit();
                    r.Cells.Font.Bold = true;
                }
                if (row > 11)
                {
                    int num = 0;

                    List<cl_79_80> tt = new List<cl_79_80>();

                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 0)// không xác định
                        {
                            tt = _listVPNgT.Where(p => (p.NTinh != 1 && p.NTinh != 2 && p.NTinh != 3) || (p.Tuyen != 1 && p.Tuyen != 2)).ToList();
                        }
                        else if (i == 1)
                        {
                            _arr2[num, 0] = "A. Bệnh nhân nội tỉnh kcb ban đầu";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 2)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 3)
                        {
                            _arr2[num, 0] = "B. Bệnh nhân nội tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 4)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 5)
                        {
                            _arr2[num, 0] = "C. Bệnh nhân Ngoại tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 6)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 2).ToList();
                        }
                        num++;


                        int count = 0;
                        foreach (var item in tt)
                        {
                            // item.Ma_cskcb = item.Ma_cskcb;
                            _arr2[num, 0] = count + 1;//num + 1; 
                            _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                            _arr2[num, 2] = item.Gioi_tinh == true ? item.NSinh : "";
                            _arr2[num, 3] = item.Gioi_tinh == false ? item.NSinh : "";
                            _arr2[num, 4] = item.TyLeBHTT;
                            _arr2[num, 5] = item.Ma_the;
                            _arr2[num, 6] = item.Ma_dkbd;
                            _arr2[num, 7] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                            if (item.Ngaykham != null)
                            {
                                _arr2[num, 8] = item.Ngaykham.ToString("dd/MM/yyyy");
                            }
                            if (item.Ngay_ra != null)
                            {
                                _arr2[num, 9] = item.Ngay_ra.ToString("dd/MM/yyyy");

                            }
                            _arr2[num, 10] = item.So_ngay_dtri;
                            _arr2[num, 11] = item.T_tongchi;
                            _arr2[num, 12] = item.T_xn;
                            _arr2[num, 13] = item.T_cdha;
                            _arr2[num, 14] = item.T_thuoc;
                            _arr2[num, 15] = item.T_mau;
                            _arr2[num, 16] = item.T_pttt;
                            _arr2[num, 17] = item.T_vtyt;
                            _arr2[num, 18] = item.T_dvkt_tyle;
                            _arr2[num, 19] = item.T_thuoc_tyle;
                            _arr2[num, 20] = item.T_vtyt_tyle;
                            if (inCongKham_NoiTru)
                            {
                                _arr2[num, 21] = item.T_kham;
                                _arr2[num, 22] = item.T_giuong;
                                _arr2[num, 23] = item.T_vchuyen;
                                _arr2[num, 24] = item.T_bntt;
                                _arr2[num, 25] = item.T_bhtt;
                                _arr2[num, 26] = item.CPNgoaiBH;
                            }
                            else
                            {
                                _arr2[num, 21] = item.T_kham;
                                _arr2[num, 22] = item.T_vchuyen;
                                _arr2[num, 23] = item.T_bntt;
                                _arr2[num, 24] = item.T_bhtt;
                                _arr2[num, 25] = item.CPNgoaiBH;
                            }

                            num++;
                            count++;
                        }
                    }
                    // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format                  
                    exSheet.Range[exSheet.Cells[8, 7], exSheet.Cells[row + 7, 7]].NumberFormat = "@";// ma_dkbd                    
                    exSheet.Range[exSheet.Cells[8, 8], exSheet.Cells[row + 7, 8]].NumberFormat = "@";// ngày vào
                    exSheet.Range[exSheet.Cells[8, 10], exSheet.Cells[row + 7, 10]].NumberFormat = "@";// ngày ra   
                    //-------------------------------------------------------------------------------------------
                    exSheet.Range[exSheet.Cells[8, 1], exSheet.Cells[row + 7, col]].Value = _arr2;
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

                #endregion
            }
            else if (tenbieu == "Biểu 79a")
            {
                #region BC 79

                // mảng tên cột--------------------------------------------------------------------------------------------------------
                string[] _arr;
                _arr = new string[] { "stt", "Họ tên", "Năm sinh nam", "Năm sinh Nữ","TyLeBHTT", "Mã thẻ BHYT", "Mã ĐKKCBBĐ", "mã bệnh", "ngày khám",
                     "Tổng chi phí", "Xét nghiệm", "CĐHA TDCN", "Thuốc", "Máu", "PTTT", "VTYT", "DVKT Tỷ lệ", "Thuốc tỷ lệ", "VTYT tỷ lệ", "Tiền khám", "Tiền vận chuyển", "Người bệnh cùng chi trả", "Tổng CP đề nghị BHYT thanh toán", "CP Ngoài định suất"};



                // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
                int row = _listVPNgT.Count + 11;
                int col = _arr.Length;
                Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[1, 1];
                r11.Value2 = DungChung.Ham.convertFont(convert, "TÊN CƠ SỞ KCB: " + DungChung.Bien.TenCQ.ToUpper(), font);
                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[1, 7];
                r17.Value2 = DungChung.Ham.convertFont(convert, "Mẫu số: C79a-HD", font);
                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[2, 1];
                r21.Value2 = DungChung.Ham.convertFont(convert, "Mã số: " + DungChung.Bien.MaBV, font);
                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[2, 7];
                r27.Value2 = DungChung.Ham.convertFont(convert, "(Ban hành theo Thông tư số: 178/2012/TT-", font);
                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[3, 7];
                r37.Value2 = DungChung.Ham.convertFont(convert, "BTC ngày 23/10/2012 của Bộ Tài Chính)", font);
                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[4, 1];
                r41.Value2 = DungChung.Ham.convertFont(convert, "DANH SÁCH NGƯỜI BỆNH BẢO HIỂM TẾ NGOẠI TRÚ ĐỀ NGHỊ THANH TOÁN", font);//tiêu đề
                COMExcel.Range r51 = (COMExcel.Range)exSheet.Cells[5, 1];
                r51.Value2 = DungChung.Ham.convertFont(convert, ngaythang, font);//ngày tháng tìm kiếm
                COMExcel.Range r61 = (COMExcel.Range)exSheet.Cells[6, 1];
                r61.Value2 = DungChung.Ham.convertFont(convert, "(Gửi cùng với file dữ liệu hàng tháng)", font);

                // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
                int k = 0;
                foreach (var b in _arr)
                {
                    k++;
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[7, k];
                    r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font);
                    r.Columns.AutoFit();
                    r.Cells.Font.Bold = true;
                }

                if (row > 11)
                {
                    int num = 0;
                    List<cl_79_80> tt = new List<cl_79_80>();

                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 0)// không xác định
                        {
                            tt = _listVPNgT.Where(p => (p.NTinh != 1 && p.NTinh != 2 && p.NTinh != 3) || (p.Tuyen != 1 && p.Tuyen != 2)).ToList();
                        }
                        else if (i == 1)
                        {
                            _arr2[num, 0] = "A. Bệnh nhân nội tỉnh kcb ban đầu";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 2)// đúng tuyến
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 1).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 3)
                        {
                            _arr2[num, 0] = "B. Bệnh nhân nội tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 4)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 2).Where(p => p.Tuyen == 2).ToList();
                        }
                        else if (i == 5)
                        {
                            _arr2[num, 0] = "C. Bệnh nhân Ngoại tỉnh đến";
                            num++;
                            _arr2[num, 0] = "I. Đúng tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 1).ToList();
                        }
                        else if (i == 6)
                        {
                            _arr2[num, 0] = "II. Trái tuyến";
                            tt = _listVPNgT.Where(p => p.NTinh == 3).Where(p => p.Tuyen == 2).ToList();
                        }
                        num++;
                        int count = 0;
                        foreach (var item in tt)
                        {
                            // item.Ma_cskcb = item.Ma_cskcb;
                            _arr2[num, 0] = count + 1; //num + 1;
                            _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                            _arr2[num, 2] = item.Gioi_tinh == true ? item.NSinh : "";
                            _arr2[num, 3] = item.Gioi_tinh == false ? item.NSinh : "";
                            _arr2[num, 4] = item.TyLeBHTT;
                            _arr2[num, 5] = item.Ma_the;
                            _arr2[num, 6] = item.Ma_dkbd;
                            _arr2[num, 7] = GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0];//item.Ma_benh;
                            if (item.Ngaykham != null)
                            {
                                _arr2[num, 8] = item.Ngaykham.ToString("dd/MM/yyyy");
                            }
                            _arr2[num, 9] = item.T_tongchi;
                            _arr2[num, 10] = item.T_xn;
                            _arr2[num, 11] = item.T_cdha;
                            _arr2[num, 12] = item.T_thuoc;
                            _arr2[num, 13] = item.T_mau;
                            _arr2[num, 14] = item.T_pttt;
                            _arr2[num, 15] = item.T_vtyt;
                            _arr2[num, 16] = item.T_dvkt_tyle;
                            _arr2[num, 17] = item.T_thuoc_tyle;
                            _arr2[num, 18] = item.T_vtyt_tyle;
                            _arr2[num, 19] = item.T_kham;
                            _arr2[num, 20] = item.T_vchuyen;
                            _arr2[num, 21] = item.T_bntt;
                            _arr2[num, 22] = item.T_bhtt;
                            _arr2[num, 23] = item.CPNgoaiBH;

                            num++;
                            count++;
                        }
                    }
                    // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format  
                    exSheet.Range[exSheet.Cells[8, 7], exSheet.Cells[row + 7, 7]].NumberFormat = "@";// mã đkbd
                    exSheet.Range[exSheet.Cells[8, 9], exSheet.Cells[row + 7, 9]].NumberFormat = "@";        // ngày khám       

                    //-------------------------------------------------------------------------------------------
                    exSheet.Range[exSheet.Cells[8, 1], exSheet.Cells[row + 7, col]].Value = _arr2;
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

                #endregion
            }

            return rs;
        }
        public static bool xuatExcelRange_Old(List<cl_79_80> _listVPNgT, string path, bool convert, string font, bool inCongKham_NoiTru)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKP = _dataContext.KPhongs.ToList();
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------

            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Biểu 79a";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";

            exSheet.Name = tenbieu;

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr;
            if (inCongKham_NoiTru)
                _arr = new string[] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh" , "ngay_vao","ngay_ra", "ngaydtr", "t_tongchi", "t_xn",
                "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct",
                "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa",  "thang_qt", "nam_qt", "gt_tu",
                "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "t_giuong"};
            else
                _arr = new string[] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh" , "ngay_vao","ngay_ra", "ngaydtr", "t_tongchi", "t_xn",
                "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct",
                "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa",  "thang_qt", "nam_qt", "gt_tu",
                "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", };
            if (DungChung.Bien.MaBV == "01830")
            {
                _arr = new string[] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh" , "ngay_vao","ngay_ra", "ngaydtr", "t_xn",
                "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen","t_tongchi", "t_bnct",
                "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "nam_qt",  "thang_qt","gt_tu",
                "gt_den", "diachi", "dvkt_vt", "khoa", };
            }


            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _listVPNgT.Count;
            int col = _arr.Length;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử

            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font); ;
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            if (row > 0)
            {
                int num = 0;
                if (DungChung.Bien.MaBV == "01830")
                {
                    foreach (var item in _listVPNgT)
                    {
                        // item.Ma_cskcb = item.Ma_cskcb;
                        _arr2[num, 0] = num + 1;
                        _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                        _arr2[num, 2] = item.NSinh;
                        _arr2[num, 3] = item.Gioi_tinh == false ? 0 : 1;
                        _arr2[num, 4] = item.Ma_the;
                        _arr2[num, 5] = item.Ma_dkbd;
                        _arr2[num, 6] = item.Ma_benh;
                        _arr2[num, 7] = item.Ngay_vao.ToString("dd/MM/yyyy");
                        _arr2[num, 8] = item.Ngay_ra.ToString("dd/MM/yyyy");
                        _arr2[num, 9] = item.So_ngay_dtri;
                        _arr2[num, 10] = item.T_xn;
                        _arr2[num, 11] = item.T_cdha;
                        _arr2[num, 12] = item.T_thuoc;
                        _arr2[num, 13] = item.T_mau;
                        _arr2[num, 14] = item.T_pttt;
                        _arr2[num, 15] = item.T_vtyt;
                        _arr2[num, 16] = item.T_vtyt_tyle;
                        _arr2[num, 17] = item.T_dvkt_tyle;
                        _arr2[num, 18] = item.T_thuoc_tyle;
                        _arr2[num, 19] = item.T_kham; //inCongKham_NoiTru ? item.T_kham : (item.T_kham + item.T_giuong);
                        _arr2[num, 20] = item.T_vchuyen;
                        _arr2[num, 21] = item.T_tongchi;
                        _arr2[num, 22] = item.T_bntt;
                        _arr2[num, 23] = item.T_bhtt;
                        _arr2[num, 24] = item.T_vchuyen;
                        _arr2[num, 25] = item.Ma_lydo_vvien == 3 ? 0 : item.Ma_lydo_vvien;
                        _arr2[num, 26] = item.Ma_benhkhac;
                        _arr2[num, 27] = item.Ma_cskcb;
                        _arr2[num, 28] = item.NgayTT.Year;
                        _arr2[num, 29] = item.NgayTT.Month;
                        _arr2[num, 30] = item.Gt_the_tu.ToShortDateString();
                        _arr2[num, 31] = item.Gt_the_den.ToShortDateString();
                        _arr2[num, 32] = item.Dia_chi == null ? "" : DungChung.Ham.convertFont(convert, item.Dia_chi.ToString(), font);
                        _arr2[num, 33] = "";
                        _arr2[num, 34] = item.MaKhoaTongKet;
                        num++;
                    }
                }
                else
                    foreach (var item in _listVPNgT)
                    {
                        // item.Ma_cskcb = item.Ma_cskcb;
                        _arr2[num, 0] = num + 1;
                        _arr2[num, 1] = DungChung.Ham.convertFont(convert, item.Ho_ten.ToString(), font);
                        _arr2[num, 2] = item.NSinh;
                        _arr2[num, 3] = item.Gioi_tinh == false ? 0 : 1;
                        _arr2[num, 4] = item.Ma_the;
                        _arr2[num, 5] = item.Ma_dkbd;
                        _arr2[num, 6] = item.Ma_benh;
                        _arr2[num, 7] = item.Ngay_vao.ToShortDateString();
                        _arr2[num, 8] = item.Ngay_ra.ToShortDateString();
                        _arr2[num, 9] = item.So_ngay_dtri;
                        _arr2[num, 10] = item.T_tongchi;
                        _arr2[num, 11] = item.T_xn;
                        _arr2[num, 12] = item.T_cdha;
                        _arr2[num, 13] = item.T_thuoc;
                        _arr2[num, 14] = item.T_mau;
                        _arr2[num, 15] = item.T_pttt;
                        _arr2[num, 16] = item.T_vtyt;
                        _arr2[num, 17] = item.T_vtyt_tyle;
                        _arr2[num, 18] = item.T_dvkt_tyle;
                        _arr2[num, 19] = item.T_thuoc_tyle;
                        _arr2[num, 20] = item.T_kham; //inCongKham_NoiTru ? item.T_kham : (item.T_kham + item.T_giuong);
                        _arr2[num, 21] = item.T_vchuyen;
                        _arr2[num, 22] = item.T_bntt;
                        _arr2[num, 23] = item.T_bhtt;
                        _arr2[num, 24] = item.T_vchuyen;
                        _arr2[num, 25] = item.Ma_lydo_vvien == 3 ? 0 : item.Ma_lydo_vvien;
                        _arr2[num, 26] = "";
                        _arr2[num, 27] = item.Ma_cskcb;
                        var tenkp = _lKP.Where(p => p.MaKP == item.MaKP);// item.MaKP (int) thay cho MaKhoaTongKet(string)
                        _arr2[num, 28] = (tenkp.Count() > 0 && tenkp.First().TenKP != null) ? (DungChung.Ham.convertFont(convert, tenkp.First().TenKP, font)) : "";
                        _arr2[num, 29] = item.NgayTT.Month;
                        _arr2[num, 30] = item.NgayTT.Year;
                        _arr2[num, 31] = item.Gt_the_tu.ToShortDateString();
                        _arr2[num, 32] = item.Gt_the_den.ToShortDateString();
                        _arr2[num, 33] = item.Dia_chi == null ? "" : DungChung.Ham.convertFont(convert, item.Dia_chi.ToString(), font);
                        _arr2[num, 34] = "";
                        _arr2[num, 35] = 0;
                        _arr2[num, 36] = "";
                        _arr2[num, 37] = 0;
                        _arr2[num, 38] = 0;
                        _arr2[num, 39] = tenbieu == "Biểu 79a" ? "NGOAI" : "NOI";
                        _arr2[num, 40] = "CSKCB";
                        _arr2[num, 41] = item.Ma_bn;
                        _arr2[num, 42] = item.MaKhoaTongKet;
                        if (inCongKham_NoiTru)
                            _arr2[num, 43] = item.T_giuong; //tiền giường 
                        num++;
                    }
                // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format
                exSheet.Range[exSheet.Cells[2, 6], exSheet.Cells[row + 1, 6]].NumberFormat = "@";// ma_dkbd
                exSheet.Range[exSheet.Cells[2, 8], exSheet.Cells[row + 1, 8]].NumberFormat = "@";// ngày vào
                exSheet.Range[exSheet.Cells[2, 9], exSheet.Cells[row + 1, 9]].NumberFormat = "@";// ngày ra
                exSheet.Range[exSheet.Cells[2, 28], exSheet.Cells[row + 1, 28]].NumberFormat = "@";// ma_cskcb                 
                exSheet.Range[exSheet.Cells[2, 30], exSheet.Cells[row + 1, 30]].NumberFormat = "@";// tháng quyết toán 
                exSheet.Range[exSheet.Cells[2, 31], exSheet.Cells[row + 1, 31]].NumberFormat = "@";// năm quyết toán                
                exSheet.Range[exSheet.Cells[2, 32], exSheet.Cells[row + 1, 32]].NumberFormat = "@";// giá trị thẻ từ
                exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 33]].NumberFormat = "@";// giá trị thẻ đến




                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
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

            return rs;
        }
        public static bool xuatExcel_ICD10(List<cl_79_80> _listVPNgT, string path, bool convert, string font)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKP = _dataContext.KPhongs.ToList();
            // gọi ứng dụng excel--------------------------------------------------------------------------------------------------

            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
            string tenbieu = "Mã bệnh";
            int _loaiKCB = -1;
            if (_listVPNgT.Count > 0)
                _loaiKCB = _listVPNgT.First().Ma_loaikcb;
            if (_loaiKCB == 3)
                tenbieu = "Biểu 80a";

            exSheet.Name = tenbieu;

            // mảng tên cột--------------------------------------------------------------------------------------------------------
            string[] _arr = new string[] { "ct_id", "matinh", "benh_vien", "loai", "sophieu", "ma_khoa", "ten_khoa" , "mathe","ma_dt", "loai_tt", "vanchuyen", "loai_tt1",
                "ho_ten", "gioi_tinh", "nam_sinh", "dia_chi", "tinh_kcb", "ma_kcb", "noi_kham", "noi_den", "lenh_dx", "ngay_vao", "ngay_ra",
                "gtri_tu", "gtri_den", "ngay_tt", "ma_icd", "ten_benh", "benhan",  "dieu_tri", "ket_qua", "benh_phu",
                "ngay_g1", "ngay_g2", "ngay_g3", "da_in", "excel", "so_luot", "so_ngay", "ma",
                "cot001", "cot002", "cot003", "cot004", "cot005", "cot006", "cot007", "cot008", "cot009", "cot010", "cot011", "ngoai_ds", "tong_tien", "tien_bn", "tien_bhxh", "nam_qt","thang_qt"};

            // add tên cột vào sheet exSheet---------------------------------------------------------------------------------------
            int k = 0;
            foreach (var b in _arr)
            {
                k++;
                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                r.Value2 = DungChung.Ham.convertFont(convert, b.ToString(), font); ;
                r.Columns.AutoFit();
                r.Cells.Font.Bold = true;
            }

            // đổ dữ liệu từ list vào mảng 2 chiều---------------------------------------------------------------------------------
            int row = _listVPNgT.Count;
            int col = _arr.Length;
            Object[,] _arr2 = new Object[row, col]; // tạo ra mảng count, col phần tử
            if (row > 0)
            {
                int num = 0;
                foreach (var item in _listVPNgT)
                {
                    // item.Ma_cskcb = item.Ma_cskcb;
                    //
                    _arr2[num, 0] = item.NgayTT.Year + item.Ma_bn;
                    _arr2[num, 1] = DungChung.Bien.MaTinh;
                    _arr2[num, 2] = DungChung.Bien.MaBV.Substring(2, 3);
                    _arr2[num, 3] = tenbieu == "Biểu 79a" ? "01" : "02";
                    _arr2[num, 4] = item.Ma_bn;
                    _arr2[num, 5] = item.MaKhoaTongKet;
                    var tenkp = _lKP.Where(p => p.MaKP == item.MaKP);
                    _arr2[num, 6] = (tenkp.Count() > 0 && tenkp.First().TenKP != null) ? (DungChung.Ham.convertFont(convert, tenkp.First().TenKP, font)) : "";

                    _arr2[num, 7] = item.Ma_the;

                    _arr2[num, 8] = item.Ma_the.Substring(0, 2);
                    _arr2[num, 9] = item.Ma_the.Substring(2, 1);
                    _arr2[num, 10] = "";
                    _arr2[num, 11] = 0;
                    _arr2[num, 12] = item.Ho_ten;
                    _arr2[num, 13] = item.Gioi_tinh == false ? 0 : 1;
                    _arr2[num, 14] = item.Ngay_sinh.PadRight(4);
                    _arr2[num, 15] = item.Dia_chi;
                    _arr2[num, 16] = item.Ma_cskcb.Substring(0, 2);
                    _arr2[num, 17] = item.Ma_cskcb.Substring(2, 3);
                    _arr2[num, 18] = DungChung.Bien.TenCQ;
                    _arr2[num, 19] = item.Ma_noi_chuyen;
                    _arr2[num, 20] = "";
                    _arr2[num, 21] = item.Ngay_vao;
                    _arr2[num, 22] = item.Ngay_ra;
                    _arr2[num, 23] = item.Gt_the_tu;
                    _arr2[num, 24] = item.Gt_the_den;
                    _arr2[num, 25] = item.NgayTT;
                    _arr2[num, 26] = item.Ma_benh;
                    _arr2[num, 27] = item.Chandoan;
                    _arr2[num, 28] = "";
                    _arr2[num, 29] = 1;
                    _arr2[num, 30] = item.Ket_qua_dtri;
                    _arr2[num, 31] = item.Ma_benhkhac;
                    _arr2[num, 32] = "";
                    _arr2[num, 33] = "";
                    _arr2[num, 34] = "";
                    _arr2[num, 35] = 0;
                    _arr2[num, 36] = 0;
                    _arr2[num, 37] = 0;
                    _arr2[num, 38] = item.So_ngay_dtri;
                    _arr2[num, 39] = 1;

                    _arr2[num, 40] = item.T_xn;
                    _arr2[num, 41] = item.T_cdha;
                    _arr2[num, 42] = item.T_thuoc;
                    _arr2[num, 43] = item.T_mau;
                    _arr2[num, 44] = item.T_pttt;
                    _arr2[num, 45] = item.T_vtyt;
                    _arr2[num, 46] = item.T_dvkt_tyle;
                    _arr2[num, 47] = item.T_thuoc_tyle;
                    _arr2[num, 48] = item.T_vtyt_tyle;
                    _arr2[num, 49] = item.T_kham;
                    _arr2[num, 50] = item.T_vchuyen;
                    _arr2[num, 51] = item.T_ngoaids;
                    _arr2[num, 52] = item.T_tongchi;
                    _arr2[num, 53] = item.T_bntt;
                    _arr2[num, 54] = item.T_bhtt;
                    _arr2[num, 55] = item.NgayTT.Year;
                    _arr2[num, 56] = item.NgayTT.Month;
                    num++;
                }
                // chú ý: định dạng cho ngày ra ngày vào, nếu thay đổi thứ tự cột phải set lại thứ tự range format
                exSheet.Range[exSheet.Cells[2, 2], exSheet.Cells[row + 1, 2]].NumberFormat = "@";// ma_dkbd
                exSheet.Range[exSheet.Cells[2, 3], exSheet.Cells[row + 1, 3]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 16], exSheet.Cells[row + 1, 16]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 17], exSheet.Cells[row + 1, 17]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 19], exSheet.Cells[row + 1, 19]].NumberFormat = "@";

                exSheet.Range[exSheet.Cells[2, 22], exSheet.Cells[row + 1, 22]].NumberFormat = "dd/MM/yyyy";
                exSheet.Range[exSheet.Cells[2, 23], exSheet.Cells[row + 1, 23]].NumberFormat = "dd/MM/yyyy";
                exSheet.Range[exSheet.Cells[2, 24], exSheet.Cells[row + 1, 24]].NumberFormat = "dd/MM/yyyy";
                exSheet.Range[exSheet.Cells[2, 25], exSheet.Cells[row + 1, 25]].NumberFormat = "dd/MM/yyyy";
                exSheet.Range[exSheet.Cells[2, 26], exSheet.Cells[row + 1, 26]].NumberFormat = "dd/MM/yyyy";

                exSheet.Range[exSheet.Cells[2, 31], exSheet.Cells[row + 1, 31]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 32], exSheet.Cells[row + 1, 32]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 33]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 55]].NumberFormat = "@";
                exSheet.Range[exSheet.Cells[2, 33], exSheet.Cells[row + 1, 56]].NumberFormat = "@";


                //-------------------------------------------------------------------------------------------
                exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[row + 1, col]].Value = _arr2;
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

            return rs;
        }

        public class cl_79_80
        {
            public double tongchi2 { set; get; }

            string err;
            public string Err
            {
                get { return err; }
                set { err = value; }
            }

            DateTime ngayGuiBHXH;
            public DateTime NgayGuiBHXH
            {
                get { return ngayGuiBHXH; }
                set { ngayGuiBHXH = value; }
            }

            private int sTT;
            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }

            private int ma_bn;
            public int Ma_bn
            {
                get { return ma_bn; }
                set { ma_bn = value; }
            }

            private string ho_ten;
            public string Ho_ten
            {
                get { return ho_ten; }
                set { ho_ten = value; }
            }

            private string ngay_sinh;
            public string Ngay_sinh
            {
                get { return ngay_sinh; }
                set { ngay_sinh = value; }
            }

            private bool gioi_tinh;
            public bool Gioi_tinh
            {
                get { return gioi_tinh; }
                set { gioi_tinh = value; }
            }

            private string dia_chi;
            public string Dia_chi
            {
                get { return dia_chi; }
                set { dia_chi = value; }
            }

            private string ma_the;
            public string Ma_the
            {
                get { return ma_the; }
                set { ma_the = value; }
            }

            private string ma_dkbd;
            public string Ma_dkbd
            {
                get { return ma_dkbd; }
                set { ma_dkbd = value; }
            }

            private DateTime gt_the_tu;
            public DateTime Gt_the_tu
            {
                get { return gt_the_tu; }
                set { gt_the_tu = value; }
            }

            private DateTime gt_the_den;
            public DateTime Gt_the_den
            {
                get { return gt_the_den; }
                set { gt_the_den = value; }
            }

            private string ma_benh;
            public string Ma_benh
            {
                get { return ma_benh; }
                set { ma_benh = value; }
            }

            private string ma_benhkhac;
            public string Ma_benhkhac
            {
                get { return ma_benhkhac; }
                set { ma_benhkhac = value; }
            }

            private int ma_lydo_vvien;
            public int Ma_lydo_vvien
            {
                get { return ma_lydo_vvien; }
                set { ma_lydo_vvien = value; }
            }

            private string ma_noi_chuyen;
            public string Ma_noi_chuyen
            {
                get { return ma_noi_chuyen; }
                set { ma_noi_chuyen = value; }
            }

            private DateTime ngay_vao;
            public DateTime Ngay_vao
            {
                get { return ngay_vao; }
                set { ngay_vao = value; }
            }

            private DateTime ngay_ra;
            public DateTime Ngay_ra
            {
                get { return ngay_ra; }
                set { ngay_ra = value; }
            }

            private double so_ngay_dtri;
            public double So_ngay_dtri
            {
                get { return so_ngay_dtri; }
                set { so_ngay_dtri = value; }
            }

            private int ket_qua_dtri;
            public int Ket_qua_dtri
            {
                get { return ket_qua_dtri; }
                set { ket_qua_dtri = value; }
            }

            private int tinh_trang_rv;
            public int Tinh_trang_rv
            {
                get { return tinh_trang_rv; }
                set { tinh_trang_rv = value; }
            }

            private double t_tongchi;
            public double T_tongchi
            {
                get { return t_tongchi; }
                set { t_tongchi = value; }
            }

            private double t_xn;
            public double T_xn
            {
                get { return t_xn; }
                set { t_xn = value; }
            }

            private double t_cdha;
            public double T_cdha
            {
                get { return t_cdha; }
                set { t_cdha = value; }
            }

            private double t_thuoc;
            public double T_thuoc
            {
                get { return t_thuoc; }
                set { t_thuoc = value; }
            }

            private double t_mau;
            public double T_mau
            {
                get { return t_mau; }
                set { t_mau = value; }
            }

            private double t_pttt;
            public double T_pttt
            {
                get { return t_pttt; }
                set { t_pttt = value; }
            }

            private double t_vtyt;
            public double T_vtyt
            {
                get { return t_vtyt; }
                set { t_vtyt = value; }
            }

            private double t_dvkt_tyle;
            public double T_dvkt_tyle
            {
                get { return t_dvkt_tyle; }
                set { t_dvkt_tyle = value; }
            }

            private double t_thuoc_tyle;
            public double T_thuoc_tyle
            {
                get { return t_thuoc_tyle; }
                set { t_thuoc_tyle = value; }
            }

            private double t_vtyt_tyle;
            public double T_vtyt_tyle
            {
                get { return t_vtyt_tyle; }
                set { t_vtyt_tyle = value; }
            }

            private double t_kham;
            public double T_kham
            {
                get { return t_kham; }
                set { t_kham = value; }
            }

            private double t_giuong;
            public double T_giuong
            {
                get { return t_giuong; }
                set { t_giuong = value; }
            }

            private double t_vchuyen;
            public double T_vchuyen
            {
                get { return t_vchuyen; }
                set { t_vchuyen = value; }
            }

            private double t_bntt;
            public double T_bntt
            {
                get { return t_bntt; }
                set { t_bntt = value; }
            }

            private double t_bhtt;
            public double T_bhtt
            {
                get { return t_bhtt; }
                set { t_bhtt = value; }
            }

            private double t_ngoaids;
            public double T_ngoaids
            {
                get { return t_ngoaids; }
                set { t_ngoaids = value; }
            }

            private string ma_khoa;
            public string Ma_khoa
            {
                get { return ma_khoa; }
                set { ma_khoa = value; }
            }

            private int nam_qt;
            public int Nam_qt
            {
                get { return nam_qt; }
                set { nam_qt = value; }
            }

            private int thang_qt;
            public int Thang_qt
            {
                get { return thang_qt; }
                set { thang_qt = value; }
            }

            private string ma_khuvuc;
            public string Ma_khuvuc
            {
                get { return ma_khuvuc; }
                set { ma_khuvuc = value; }
            }

            private int ma_loaikcb;
            public int Ma_loaikcb
            {
                get { return ma_loaikcb; }
                set { ma_loaikcb = value; }
            }

            private string ma_cskcb;
            public string Ma_cskcb
            {
                get { return ma_cskcb; }
                set { ma_cskcb = value; }
            }

            private string noi_ttoan;
            public string Noi_ttoan
            {
                get { return noi_ttoan; }
                set { noi_ttoan = value; }
            }

            private int giam_dinh;
            public int Giam_dinh
            {
                get { return giam_dinh; }
                set { giam_dinh = value; }
            }

            private double t_xuattoan;
            public double T_xuattoan
            {
                get { return t_xuattoan; }
                set { t_xuattoan = value; }
            }

            private string lydo_xt;
            public string Lydo_xt
            {
                get { return lydo_xt; }
                set { lydo_xt = value; }
            }

            private double t_datuyen;
            public double T_datuyen
            {
                get { return t_datuyen; }
                set { t_datuyen = value; }
            }

            private double t_vuottran;
            public double T_vuottran
            {
                get { return t_vuottran; }
                set { t_vuottran = value; }
            }

            private DateTime ngayTT;
            public DateTime NgayTT
            {
                get { return ngayTT; }
                set { ngayTT = value; }
            }

            private string dTuong;
            public string DTuong
            {
                get { return dTuong; }
                set { dTuong = value; }
            }

            private string n_dTuong;
            public string NhomDTuong
            {
                get { return n_dTuong; }
                set { n_dTuong = value; }
            }

            private string maDTuong;
            public string MaDTuong
            {
                get { return maDTuong; }
                set { maDTuong = value; }
            }

            private int nTinh;
            public int NTinh
            {
                get { return nTinh; }
                set { nTinh = value; }
            }

            private int idBenhNhan;
            public int IdBenhNhan
            {
                get { return idBenhNhan; }
                set { idBenhNhan = value; }
            }

            private DateTime ngaykham;
            public DateTime Ngaykham
            {
                get { return ngaykham; }
                set { ngaykham = value; }
            }

            private double tongCong;
            public double TongCong
            {
                get { return tongCong; }
                set { tongCong = value; }
            }

            private double thanhtien;
            public double Thanhtien
            {
                get { return thanhtien; }
                set { thanhtien = value; }
            }

            private string nSinh;
            public string NSinh
            {
                get { return nSinh; }
                set { nSinh = value; }
            }

            private int makp;
            public int MaKP // mã khoa phòng phát sinh chi phí của bệnh nhân, 1 bn có thể có >1 mã KP get value VPhict
            {
                get { return makp; }
                set { makp = value; }
            }

            private double cpNgoaiBH;
            public double CPNgoaiBH
            {
                get { return cpNgoaiBH; }
                set { cpNgoaiBH = value; }
            }

            private int capcuu;
            public int Capcuu
            {
                get { return capcuu; }
                set { capcuu = value; }
            }

            private int soluot;
            public int Soluot
            {
                get { return soluot; }
                set { soluot = value; }
            }

            private int tuyen;
            public int Tuyen
            {
                get { return tuyen; }
                set { tuyen = value; }
            }

            private string chandoan;
            public string Chandoan
            {
                get { return chandoan; }
                set { chandoan = value; }
            }

            private string cannang;
            public string Cannang
            {
                get { return cannang; }
                set { cannang = value; }
            }

            private string maKhoaTongKet;
            /// <summary>
            /// theo ma QD
            /// </summary>
            public string MaKhoaTongKet
            {
                get { return maKhoaTongKet; }
                set { maKhoaTongKet = value; }
            }

            private string ma_pttt_qt;
            public string Ma_pttt_qt
            {
                get { return ma_pttt_qt; }
                set { ma_pttt_qt = value; }
            }

            private double muchuong;
            public double Muchuong
            {
                get { return muchuong; }
                set { muchuong = value; }
            }

            private bool st;
            public bool Status
            {
                get { return st; }
                set { st = value; }
            }

            bool export;
            public bool Export
            {
                get { return export; }
                set { export = value; }
            }

            public double TyLeBHTT { get; set; }

            public int SoDK { get; set; }

            public double T_NguonKhac { get; set; }
        }

        public static string[] GetICD(string maBenh)
        {
            string _strIcd = "";
            string _strIcdKhac = "";
            if (maBenh != "")
            {
                string[] _arr = maBenh.Split(';');
                _strIcd = _arr[0];
                for (int i = 1; i < _arr.Length; i++)
                {
                    if (i == _arr.Length - 1)
                    {
                        _strIcdKhac += _arr[i];
                    }
                    else
                    {
                        _strIcdKhac += _arr[i] + ";";
                    }
                }
            }
            return new string[] { _strIcd, _strIcdKhac };
        }

        public static bool xuatXML(List<MucTT> _listmuc, int _hangbv, List<cl_79_80> _listVPBH, string path, int noingoaitru)
        {
            bool rs = true;
            try
            {
                int num = 1;
                var xEle = new XElement("Bang1",
                            from item in _listVPBH
                            select new XElement("ma_lk",
                                new XAttribute("ma_lk", item.Ma_bn),
                                           new XElement("stt", num++),
                                           new XElement("ma_bn", item.Ma_bn),
                                           new XElement("ho_ten", item.Ho_ten.ToString()),
                                           new XElement("ngay_sinh", item.Ngay_sinh),
                                           new XElement("gioi_tinh", item.Gioi_tinh == false ? 2 : 1),
                                           new XElement("dia_chi", item.Dia_chi),
                                           new XElement("ma_the", item.Ma_the),
                                           new XElement("ma_dkbd", item.Ma_dkbd),
                                           new XElement("gt_the_tu", item.Gt_the_tu != null ? item.Gt_the_tu.ToString("yyyyMMdd") : ""),
                                           new XElement("gt_the_den", item.Gt_the_den != null ? item.Gt_the_den.ToString("yyyyMMdd") : ""),
                                           new XElement("ten_benh", item.Chandoan),
                                           new XElement("ma_benh", GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[0]),
                                           new XElement("ma_benhkhac", GetICD(item.Ma_benh == null ? "" : item.Ma_benh)[1]),
                                           new XElement("ma_lydo_vvien", item.Ma_lydo_vvien),
                                           new XElement("ma_noi_chuyen", item.Ma_noi_chuyen),
                                           new XElement("ma_tai_nan", ""),
                                           new XElement("ngay_vao", item.Ngay_vao != null ? item.Ngay_vao.ToString("yyyyMMddHHmm") : ""),
                                           new XElement("ngay_ra", item.Ngay_ra != null ? item.Ngay_ra.ToString("yyyyMMddHHmm") : ""),
                                           new XElement("so_ngay_dtri", item.So_ngay_dtri),
                                           new XElement("ket_qua_dtri", item.Ket_qua_dtri),
                                           new XElement("tinh_trang_rv", item.Tinh_trang_rv),
                                           new XElement("ngay_ttoan", item.NgayTT != null ? item.NgayTT.ToString("yyyyMMddHHmm") : ""),
                                           new XElement("muc_huong", DungChung.Ham._getmuc(_listmuc, _hangbv, item.Ma_the, item.Tuyen, noingoaitru, item.NgayTT).ToString()),
                                           new XElement("t_thuoc", Math.Round(item.T_thuoc, 0)),
                                           new XElement("t_vtyt", Math.Round(item.T_vtyt, 0)),
                                           new XElement("t_tongchi", item.T_tongchi),
                                           new XElement("t_bntt", item.T_bntt),
                                           new XElement("t_bhtt", item.T_bhtt),
                                           new XElement("t_nguonkhac", ""),
                                           new XElement("t_ngoaids", item.T_vchuyen),
                                           new XElement("nam_qt", item.NgayTT != null ? item.NgayTT.Year.ToString() : ""),
                                           new XElement("thang_qt", item.NgayTT != null ? item.NgayTT.Month.ToString() : ""),
                                           new XElement("ma_loaikcb", item.Ma_loaikcb.ToString()),
                                           new XElement("ma_khoa", item.MaKhoaTongKet),
                                           new XElement("ma_cskcb", item.Ma_cskcb),
                                           new XElement("ma_khuvuc", item.Ma_khuvuc == null ? "" : item.Ma_khuvuc),
                                           new XElement("ma_pttt_qt", item.Ma_pttt_qt),// mã QD,
                                           new XElement("can_nang", item.Cannang)
                                       ));
                xEle.Save(path);
                rs = true;
            }
            catch (Exception ex)
            {
                rs = false;
            }
            return rs;
        }
    }
}
