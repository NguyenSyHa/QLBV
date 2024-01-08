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
    public partial class frm_rep79aCT : DevExpress.XtraEditors.XtraForm
    {
        public frm_rep79aCT()
        {
            InitializeComponent();
            rdFont.Enabled = false;
        }
        string _dtuong = "BHYT";
        private int Font = 0;
        private static char[] arrTCVN = {'µ','¸','¶','·','¹','¨', '»', '¾', '¼', '½', 'Æ','©', 'Ç', 'Ê', 'È', 'É', 'Ë',
                                         '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ','ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö','×', 'Ý', 'Ø', 'Ü', 'Þ',
                                         'ß', 'ã', 'á', 'â', 'ä','«', 'å', 'è', 'æ', 'ç', 'é','¬', 'ê', 'í', 'ë', 'ì', 'î','ï',
                                         'ó', 'ñ', 'ò', 'ô','­', 'õ', 'ø', 'ö', '÷', 'ù','ú', 'ý', 'û', 'ü', 'þ','¡', '¢', '§', '£', '¤', '¥', '¦'
                                        };
        private static char[] arrUnicode = {'à', 'á', 'ả', 'ã', 'ạ','ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ','â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ','đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
                                           'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ','ì', 'í', 'ỉ', 'ĩ', 'ị','ò', 'ó', 'ỏ', 'õ', 'ọ','ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
                                          'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ','ù', 'ú', 'ủ', 'ũ', 'ụ','ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự','ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ','Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
                                        };
        private static Char[] Converter;

        private String convert(String str)
        {
            bool tt = false;
            Converter = new char[str.Length];
            Char[] arrStr = str.ToCharArray();
            for (int i = 0; i < arrStr.Length; i++)
            {
                for (int j = 0; j < arrUnicode.Length; j++)
                {
                    if (arrStr[i] == (arrUnicode[j]))
                    {
                        Converter[i] = arrTCVN[j];
                        tt = true;
                        break;
                    }
                }
                if (tt == false)
                {
                    Converter[i] = arrStr[i];
                }
                tt = false;
            }
            return new String(Converter);
        }
        private String convertFont(String str)
        {
            String result = "";
            if (Font == 0)
            {
                result = convert(str);
                return result;
            }
            else
                return str;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
            idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;
        private void setIDNhom()
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.Select(a => new { a.IDNhom, a.TenNhomCT }).ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case "Thuốc trong danh mục BHYT":
                        idThuoc = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        idMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        idXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        idCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        idTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        idCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        idDVKTC = item.IDNhom;
                        break;
                    case "Vật tư y tế trong danh mục BHYT":
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        idNgayGiuong = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        idChiPhiVC = item.IDNhom;
                        break;
                    case "VTYT thanh toán theo tỷ lệ":
                        idVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        idThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        idHoaChat = item.IDNhom;
                        break;

                }

            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            setIDNhom();
            if (chkNhandan.Checked == false)
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (kt())
                {
                    DateTime ngaytu = System.DateTime.Now.Date;
                    DateTime ngayden = System.DateTime.Now.Date;
                    ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                    ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);

                    if (cbosx.SelectedIndex == 1) //Mã bệnh nhân (Số phiếu)
                    {
                        var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0)
                                 join vp in _dataContext.VienPhis.Where(P => P.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                 join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                 join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                 join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                 group new { bn, rv, dv, vpct, vp } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, bn.NNhap, rv.MaKP, rv.MaICD, rv.NgayRa } into kq
                                 select new
                                 {
                                     NoiTinh = kq.Key.NoiTinh,
                                     Tuyen = kq.Key.Tuyen,
                                     Makp = kq.Key.MaKP,
                                     Mabn = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     NSinh = kq.Key.NamSinh,
                                     SThe = kq.Key.SThe,
                                     GTinh = kq.Key.GTinh,
                                     MaCS = kq.Key.MaCS,
                                     MaICD = kq.Key.MaICD,
                                     Ngaykham = kq.Key.NNhap,
                                     Ngayra = kq.Key.NgayRa,
                                     Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                     CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                     Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                     xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                     Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                     TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                     VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                     VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                     DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                     ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                     CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                     ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                     Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                     TienBN = kq.Sum(p => p.vpct.TienBN),
                                     TienBH = kq.Sum(p => p.vpct.TienBH),
                                 }).OrderBy(p => p.Mabn).ToList().OrderBy(p => p.Mabn).ToList();
                        if (lupKhoaphong.Text == "")
                        {
                            if (chkGT.Checked == true)
                            {
                                frmIn frmv = new frmIn();
                                BaoCao.rep79aCTMoi repv = new BaoCao.rep79aCTMoi();
                                repv.DataSource = q.OrderBy(p => p.Mabn);
                                repv.Ngaythang.Value = theoquy();
                                repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                repv.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                repv.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                double st = 0;
                                st = q.Sum(a => a.TienBH);
                                repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                repv.BindingData();
                                repv.CreateDocument();
                                frmv.prcIN.PrintingSystem = repv.PrintingSystem;
                                frmv.ShowDialog();
                            }
                            else
                            {
                                frmIn frm = new frmIn();
                                BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                rep.DataSource = q.OrderBy(p => p.Mabn);
                                rep.Ngaythang.Value = theoquy();
                                rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                double st = 0;
                                st = q.Sum(a => a.TienBH);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.BindingData();
                                rep.CreateDocument();
                                //rep.DataMember = "Table";
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            if (chkGT.Checked == true)
                            {
                                int _MaKPc = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                                frmIn frmc = new frmIn();
                                BaoCao.rep79aCTMoi repc = new BaoCao.rep79aCTMoi();
                                repc.MaCS.Value = DungChung.Bien.MaBV;
                                repc.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                repc.DataSource = q.ToList().Where(p => p.Makp == _MaKPc).OrderBy(p => p.Mabn);
                                repc.Ngaythang.Value = theoquy();
                                double stc = 0;
                                stc = q.Where(a => a.Makp == _MaKPc).Sum(a => a.TienBH);
                                repc.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                repc.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                repc.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(stc, " đồng.");
                                repc.BindingData();
                                repc.CreateDocument();
                                frmc.prcIN.PrintingSystem = repc.PrintingSystem;
                                frmc.ShowDialog();
                            }
                            else
                            {
                                int _MaKP = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                                frmIn frm = new frmIn();
                                BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                rep.MaCS.Value = DungChung.Bien.MaBV;
                                rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                rep.DataSource = q.ToList().Where(p => p.Makp == _MaKP).OrderBy(p => p.Mabn);
                                rep.Ngaythang.Value = theoquy();
                                double st = 0;
                                st = q.Where(a => a.Makp == _MaKP).Sum(a => a.TienBH);
                                rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                rep.BindingData();
                                rep.CreateDocument();
                                //rep.DataMember = "Table";
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }

                        }
                        #region xuat Excel
                        if (Xuatex.Checked)
                        {
                            var q1 = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0)
                                      join dt in _dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                                      join vp in _dataContext.VienPhis.Where(P => P.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                      join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                      join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                      join nhom in _dataContext.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                      join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                      //join kp in _dataContext.KPhongs on rv.MaKP equals kp.MaKP
                                      group new { bn, rv, dv, vpct, nhom, vp, dt } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, bn.NNhap, rv.MaKP, dt.Nhom, rv.MaICD, rv.NgayRa, bn.HanBHTu, bn.HanBHDen, bn.DChi } into kq
                                      select new
                                      {
                                          // kq.Key.MaBNhan,
                                          NoiTinh = kq.Key.NoiTinh,
                                          Tuyen = kq.Key.Tuyen,
                                          Makp = kq.Key.MaKP,
                                          Mabn = kq.Key.MaBNhan,
                                          TenBNhan = kq.Key.TenBNhan,
                                          NSinh = kq.Key.NamSinh,
                                          SThe = kq.Key.SThe,
                                          GTinh = kq.Key.GTinh,
                                          MaCS = kq.Key.MaCS,
                                          nhom = kq.Key.Nhom,
                                          BHtu = kq.Key.HanBHTu,
                                          BHden = kq.Key.HanBHDen,
                                          Diachi = kq.Key.DChi,
                                          MaICD = kq.Key.MaICD,
                                          Ngaykham = kq.Key.NNhap,
                                          Ngayra = kq.Key.NgayRa,
                                          //TenKP = kq.Key.TenKP,
                                          Thuoc = kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien),
                                          CDHA = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.ThanhTien),
                                          Congkham = kq.Where(p => p.nhom.TenNhom.Contains("khám")).Sum(p => p.vpct.ThanhTien),
                                          xetnghiem = kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.ThanhTien),
                                          Mau = kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.ThanhTien),
                                          TTPT = kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.ThanhTien),
                                          VTYTH = kq.Where(p => p.nhom.TenNhomCT.ToLower().Contains("Vật tư y tế trong danh mục BHYT".ToLower())).Sum(p => p.vpct.ThanhTien),
                                          VTTT = kq.Where(p => p.nhom.TenNhomCT.ToLower().Contains("VTYT thanh toán theo tỷ lệ".ToLower())).Sum(p => p.vpct.ThanhTien),
                                          DVKTC = kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật")).Sum(p => p.vpct.ThanhTien),
                                          ThuocKTCG = kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.ThanhTien),
                                          CPVanchuyen = kq.Where(p => p.nhom.TenNhom.ToLower().Contains("vận chuyển")).Sum(p => p.vpct.ThanhTien),
                                          ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                          Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                          Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                          TienBN = kq.Sum(p => p.vpct.TienBN),
                                          TienBH = kq.Sum(p => p.vpct.TienBH),
                                      }).ToList().OrderBy(p => p.Mabn).OrderBy(p => p.Tuyen).OrderBy(p => p.NoiTinh).ToList();
                            COMExcel.Application exApp = new COMExcel.Application();
                            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                            //exSheet.Activate();
                            exSheet.Name = "NgoaiTru";// gán tên sheet
                            int i = 1;
                            string[] _arr = new string[45] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl", "sophieu" };
                            int k = 0;
                            foreach (var b in _arr)
                            {
                                k++;
                                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                r.Value2 = b.ToString();
                                r.Columns.AutoFit();
                            }
                            foreach (var a in q1)
                            {
                                i++;
                                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                r1.Value2 = i - 1;
                                r1.Columns.AutoFit();
                                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                r2.NumberFormat = "@";
                                if (a.TenBNhan != null)
                                    r2.Value2 = convertFont(a.TenBNhan);
                                r2.Columns.AutoFit();
                                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                r3.NumberFormat = "0";
                                r3.Value2 = a.NSinh;
                                r3.Columns.AutoFit();
                                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                r4.NumberFormat = "0";
                                r4.Value2 = a.GTinh;
                                r4.Columns.AutoFit();
                                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                r5.NumberFormat = "@";
                                if (a.SThe != null)
                                    r5.Value2 = convertFont(a.SThe);
                                r5.Columns.AutoFit();
                                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                r6.NumberFormat = "@";
                                if (a.MaCS != null)
                                    r6.Value2 = convertFont(a.MaCS.ToString());
                                r6.Columns.AutoFit();
                                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                r7.NumberFormat = "@";
                                if (a.MaICD != null)
                                    r7.Value2 = convertFont(a.MaICD);
                                r7.Columns.AutoFit();
                                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                r8.NumberFormat = "@";
                                if (a.Ngaykham != null)
                                    r8.Value2 = convertFont(a.Ngaykham.ToString().Substring(0, 10));
                                r8.Columns.AutoFit();
                                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                r9.NumberFormat = "@";
                                if (a.Ngayra != null)
                                    r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                r9.Columns.AutoFit();
                                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                r10.NumberFormat = "0";
                                r10.Value2 = 1;
                                r10.Columns.AutoFit();
                                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                r11.NumberFormat = "0";
                                r11.Value2 = a.Tongcong;
                                r11.Columns.AutoFit();
                                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                r12.NumberFormat = "0";
                                r12.Value2 = a.xetnghiem;
                                r12.Columns.AutoFit();
                                COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                r13.NumberFormat = "0";
                                r13.Value2 = a.CDHA;
                                r13.Columns.AutoFit();
                                COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                r14.NumberFormat = "0";
                                r14.Value2 = a.Thuoc;
                                r14.Columns.AutoFit();
                                COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                r15.NumberFormat = "0";
                                r15.Value2 = a.Mau;
                                r15.Columns.AutoFit();
                                COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                r16.NumberFormat = "0";
                                r16.Value2 = a.TTPT;
                                r16.Columns.AutoFit();
                                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                r17.NumberFormat = "0";
                                r17.Value2 = a.VTYTH;
                                r17.Columns.AutoFit();
                                COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                r18.NumberFormat = "0";
                                r18.Value2 = 0;
                                r18.Columns.AutoFit();
                                COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                r19.NumberFormat = "0";
                                r19.Value2 = a.DVKTC;
                                r19.Columns.AutoFit();
                                COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                r20.NumberFormat = "0";
                                r20.Value2 = a.ThuocKTCG;
                                r20.Columns.AutoFit();
                                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                r21.NumberFormat = "0";
                                r21.Value2 = a.Congkham;
                                r21.Columns.AutoFit();
                                COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                r22.NumberFormat = "0";
                                r22.Value2 = a.CPVanchuyen;
                                r22.Columns.AutoFit();
                                COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                r23.NumberFormat = "0";
                                r23.Value2 = a.TienBN;
                                r23.Columns.AutoFit();
                                COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                r24.NumberFormat = "0";
                                r24.Value2 = a.TienBH;
                                r24.Columns.AutoFit();
                                COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                r25.NumberFormat = "0";
                                r25.Value2 = a.CPVanchuyen;
                                r25.Columns.AutoFit();
                                COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                r26.NumberFormat = "@";
                                if (a.Tuyen != null && a.Tuyen == 1)
                                    r26.Value2 = 1;
                                else
                                    r26.Value2 = 0;
                                r26.Columns.AutoFit();
                                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                r27.Value2 = "";
                                r27.Columns.AutoFit();
                                COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                r28.NumberFormat = "@";
                                r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                r28.Columns.AutoFit();
                                //COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                //r29.Value2 = convertFont(a.TenKP);
                                //r29.Columns.AutoFit();
                                COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                r30.NumberFormat = "0";
                                if (a.Ngayra != null)
                                {
                                    r30.Value2 = a.Ngayra.Value.Month;
                                    r30.Columns.AutoFit();
                                }
                                COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                r31.NumberFormat = "0";
                                if (a.Ngayra != null)
                                {
                                    r31.Value2 = a.Ngayra.Value.Year;
                                    r31.Columns.AutoFit();
                                }
                                COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                if (a.BHtu != null)
                                {
                                    r32.NumberFormat = "@";
                                    r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                    r32.Columns.AutoFit();
                                }
                                COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                if (a.BHden != null)
                                {
                                    r33.NumberFormat = "@";

                                    r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                    r33.Columns.AutoFit();
                                }
                                COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                r34.NumberFormat = "@";
                                r34.Value2 = convertFont(a.Diachi);
                                r34.Columns.AutoFit();
                                COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                r35.Value2 = "";
                                r35.Columns.AutoFit();
                                COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                r36.Value2 = 0;
                                r36.Columns.AutoFit();
                                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                r37.Value2 = "";
                                r37.Columns.AutoFit();
                                COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                r38.NumberFormat = "0";
                                r38.Value2 = 0;
                                r38.Columns.AutoFit();
                                COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                r39.NumberFormat = "0";
                                r39.Value2 = 0;
                                r39.Columns.AutoFit();
                                COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                r40.NumberFormat = "@";
                                r40.Value2 = convertFont("NGOAI");
                                r40.Columns.AutoFit();
                                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                r41.NumberFormat = "@";
                                r41.Value2 = convertFont("CSKCB");
                                r41.Columns.AutoFit();
                                COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                r42.NumberFormat = "@";
                                r42.Value2 = a.Mabn;
                                r42.Columns.AutoFit();
                                COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                r43.NumberFormat = "@";
                                r43.Value2 = a.Makp;
                                r43.Columns.AutoFit();
                                COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                r44.Value2 = "";
                                r44.Columns.AutoFit();
                                //COMExcel.Range r45 = (COMExcel.Range)exSheet.Cells[i, 45];
                                //r45.Value2 = a.MaBNhan;
                                //r45.Columns.AutoFit();
                            }
                            exApp.Visible = true;//Ẩn hiện chương trình
                            exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                    else// nhóm đối tượng
                    {
                        if (cbosx.SelectedIndex == 2)
                        {
                            var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0).Where(p => p.SThe.Length > 2)
                                     join dt in _dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                                     join vp in _dataContext.VienPhis.Where(P => P.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                     join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                     join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                     join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                     //join kp in _dataContext.KPhongs on bn.MaKP equals kp.MaKP
                                     group new { bn, rv, dv, vpct, vp, dt } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, bn.NNhap, rv.MaKP, dt.Nhom, rv.MaICD, rv.NgayRa, bn.HanBHTu, bn.HanBHDen, bn.DChi } into kq
                                     select new
                                     {
                                         dt = kq.Key.SThe.Substring(0, 2),
                                         NoiTinh = kq.Key.NoiTinh,
                                         Tuyen = kq.Key.Tuyen,
                                         Makp = kq.Key.MaKP,
                                         Mabn = kq.Key.MaBNhan,
                                         TenBNhan = kq.Key.TenBNhan,
                                         NSinh = kq.Key.NamSinh,
                                         SThe = kq.Key.SThe,
                                         GTinh = kq.Key.GTinh,
                                         MaCS = kq.Key.MaCS,
                                         nhom = kq.Key.Nhom,
                                         BHtu = kq.Key.HanBHTu,
                                         BHden = kq.Key.HanBHDen,
                                         Diachi = kq.Key.DChi,
                                         MaICD = kq.Key.MaICD,
                                         Ngaykham = kq.Key.NNhap,
                                         Ngayra = kq.Key.NgayRa,
                                         Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                         CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                         Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                         xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                         Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                         TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                         VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                         VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                         DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                         ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                         CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                         ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                         Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                         Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                         TienBN = kq.Sum(p => p.vpct.TienBN),
                                         TienBH = kq.Sum(p => p.vpct.TienBH),
                                     }).OrderBy(p => p.NoiTinh).OrderBy(p => p.Ngayra).OrderBy(p => p.SThe).OrderBy(p => p.nhom).ToList();
                            if (lupKhoaphong.Text == "")
                            {
                                if (chkGT.Checked == true)
                                {
                                    frmIn frmb = new frmIn();
                                    BaoCao.rep79aCTMoi repb = new BaoCao.rep79aCTMoi();
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                                    repb.DataSource = q;
                                    repb.Ngaythang.Value = theoquy();
                                    repb.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repb.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    repb.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    repb.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repb.BindingData();
                                    repb.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmb.prcIN.PrintingSystem = repb.PrintingSystem;
                                    frmb.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                                    rep.DataSource = q;
                                    rep.Ngaythang.Value = theoquy();
                                    rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }

                            }
                            else
                            {
                                if (chkGT.Checked == true)
                                {
                                    int _MaKPn = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                                    frmIn frmn = new frmIn();
                                    BaoCao.rep79aCTMoi repn = new BaoCao.rep79aCTMoi();
                                    repn.MaCS.Value = DungChung.Bien.MaBV;
                                    repn.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    repn.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    repn.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    q = q.Where(a => a.Makp == _MaKPn).OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.nhom).ThenBy(p => p.dt).ThenBy(p => p.Ngayra).ToList();
                                    repn.DataSource = q;
                                    repn.Ngaythang.Value = theoquy();
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    repn.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repn.BindingData();
                                    repn.CreateDocument();
                                    frmn.prcIN.PrintingSystem = repn.PrintingSystem;
                                    frmn.ShowDialog();
                                }
                                else
                                {
                                    int _MaKP = Convert.ToInt32(lupKhoaphong.EditValue);
                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                    rep.MaCS.Value = DungChung.Bien.MaBV;
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    var que = q.ToList().Where(p => p.Makp == _MaKP).OrderBy(p => p.Ngayra).OrderBy(p => p.SThe).OrderBy(p => p.nhom);
                                    rep.DataSource = que.ToList();
                                    rep.Ngaythang.Value = theoquy();
                                    double st = 0;
                                    st = que.Sum(a => a.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }

                            #region xuat Excel
                            if (Xuatex.Checked)
                            {
                                COMExcel.Application exApp = new COMExcel.Application();
                                COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                          COMExcel.XlWBATemplate.xlWBATWorksheet);
                                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                                //exSheet.Activate();
                                exSheet.Name = "79aCT";// gán tên sheet
                                int i = 1;
                                string[] _arr = new string[44] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra", "ngaydtr", "t_tongchi", "t_xn", "t_cdha", "t_thuoc", "t_mau", "t_pttt", "t_vtytth", "t_vtyttt", "t_dvktc", "t_ktg", "t_kham", "t_vchuyen", "t_bnct", "t_bhtt", "t_ngoaids", "lydo_vv", "benhkhac", "noikcb", "khoa", "thang_qt", "nam_qt", "gt_tu", "gt_den", "diachi", "giamdinh", "t_xuattoan", "lydo_xt", "t_datuyen", "t_vuottran", "loaikcb", "noi_ttoan", "sophieu", "ma_khoa", "keysl" };
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
                                    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                    r1.Value2 = i - 1;
                                    r1.Columns.AutoFit();
                                    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                    r2.NumberFormat = "@";
                                    r2.Value2 = convertFont(a.TenBNhan);
                                    r2.Columns.AutoFit();
                                    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                    r3.NumberFormat = "0";
                                    r3.Value2 = a.NSinh;
                                    r3.Columns.AutoFit();
                                    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                    r4.NumberFormat = "0";
                                    r4.Value2 = a.GTinh;
                                    r4.Columns.AutoFit();
                                    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                    r5.NumberFormat = "@";
                                    if (a.SThe != null)
                                        r5.Value2 = convertFont(a.SThe);
                                    r5.Columns.AutoFit();
                                    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                    r6.NumberFormat = "@";
                                    if (a.MaCS != null)
                                        r6.Value2 = convertFont(a.MaCS.ToString());
                                    r6.Columns.AutoFit();
                                    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                    r7.NumberFormat = "@";
                                    if (a.MaICD != null)
                                        r7.Value2 = convertFont(a.MaICD);
                                    r7.Columns.AutoFit();
                                    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                    r8.NumberFormat = "@";
                                    if (a.Ngaykham != null)
                                        r8.Value2 = convertFont(a.Ngaykham.ToString().Substring(0, 10));
                                    r8.Columns.AutoFit();
                                    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                    r9.NumberFormat = "@";
                                    if (a.Ngayra != null)
                                        r9.Value2 = convertFont(a.Ngayra.ToString().Substring(0, 10));
                                    r9.Columns.AutoFit();
                                    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                    r10.NumberFormat = "0";
                                    r10.Value2 = 1;
                                    r10.Columns.AutoFit();
                                    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                    r11.NumberFormat = "0";
                                    r11.Value2 = a.Tongcong;
                                    r11.Columns.AutoFit();
                                    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                    r12.NumberFormat = "0";
                                    r12.Value2 = a.xetnghiem;
                                    r12.Columns.AutoFit();
                                    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                    r13.NumberFormat = "0";
                                    r13.Value2 = a.CDHA;
                                    r13.Columns.AutoFit();
                                    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                    r14.NumberFormat = "0";
                                    r14.Value2 = a.Thuoc;
                                    r14.Columns.AutoFit();
                                    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                    r15.NumberFormat = "0";
                                    r15.Value2 = a.Mau;
                                    r15.Columns.AutoFit();
                                    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                    r16.NumberFormat = "0";
                                    r16.Value2 = a.TTPT;
                                    r16.Columns.AutoFit();
                                    COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                    r17.NumberFormat = "0";
                                    r17.Value2 = a.VTYTH;
                                    r17.Columns.AutoFit();
                                    COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                    r18.NumberFormat = "0";
                                    r18.Value2 = 0;
                                    r18.Columns.AutoFit();
                                    COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                                    r19.NumberFormat = "0";
                                    r19.Value2 = a.DVKTC;
                                    r19.Columns.AutoFit();
                                    COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                                    r20.NumberFormat = "0";
                                    r20.Value2 = a.ThuocKTCG;
                                    r20.Columns.AutoFit();
                                    COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                                    r21.NumberFormat = "0";
                                    r21.Value2 = a.Congkham;
                                    r21.Columns.AutoFit();
                                    COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                                    r22.NumberFormat = "0";
                                    r22.Value2 = a.CPVanchuyen;
                                    r22.Columns.AutoFit();
                                    COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                                    r23.NumberFormat = "0";
                                    r23.Value2 = a.TienBN;
                                    r23.Columns.AutoFit();
                                    COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                                    r24.NumberFormat = "0";
                                    r24.Value2 = a.TienBH;
                                    r24.Columns.AutoFit();
                                    COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                                    r25.NumberFormat = "0";
                                    r25.Value2 = a.CPVanchuyen;
                                    r25.Columns.AutoFit();
                                    COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                                    r26.NumberFormat = "@";
                                    if (a.Tuyen != null && a.Tuyen == 1)
                                    { r26.Value2 = 1; }
                                    if (a.Tuyen != null && a.Tuyen == 0)
                                    { r26.Value2 = 0; }
                                    r26.Columns.AutoFit();
                                    COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                                    r27.Value2 = "";
                                    r27.Columns.AutoFit();
                                    COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 28];
                                    r28.NumberFormat = "@";
                                    r28.Value2 = convertFont(DungChung.Bien.MaBV);
                                    r28.Columns.AutoFit();
                                    //COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                                    //r29.Value2 = a.TenKP;
                                    //r29.Columns.AutoFit();
                                    COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                                    r30.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r30.Value2 = a.Ngayra.Value.Month;
                                        r30.Columns.AutoFit();
                                    }
                                    COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];
                                    r31.NumberFormat = "0";
                                    if (a.Ngayra != null)
                                    {
                                        r31.Value2 = a.Ngayra.Value.Year;
                                        r31.Columns.AutoFit();
                                    }
                                    COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                                    if (a.BHtu != null)
                                    {
                                        r32.NumberFormat = "@";
                                        r32.Value2 = convertFont(a.BHtu.ToString().Substring(0, 10));
                                        r32.Columns.AutoFit();
                                    }
                                    COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                                    if (a.BHden != null)
                                    {
                                        r33.NumberFormat = "@";
                                        r33.Value2 = convertFont(a.BHden.ToString().Substring(0, 10));
                                        r33.Columns.AutoFit();
                                    }
                                    COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                                    r34.NumberFormat = "@";
                                    if (a.Diachi != null)
                                        r34.Value2 = convertFont(a.Diachi);
                                    r34.Columns.AutoFit();
                                    COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                                    r35.Value2 = "";
                                    r35.Columns.AutoFit();
                                    COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                                    r36.Value2 = 0;
                                    r36.Columns.AutoFit();
                                    COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                                    r37.Value2 = "";
                                    r37.Columns.AutoFit();
                                    COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                                    r38.NumberFormat = "0";
                                    r38.Value2 = 0;
                                    r38.Columns.AutoFit();
                                    COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                                    r39.NumberFormat = "0";
                                    r39.Value2 = 0;
                                    r39.Columns.AutoFit();
                                    COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                                    r40.NumberFormat = "@";
                                    r40.Value2 = convertFont("NGOAI");
                                    r40.Columns.AutoFit();
                                    COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                                    r41.NumberFormat = "@";
                                    r41.Value2 = convertFont("CSKCB");
                                    r41.Columns.AutoFit();
                                    COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                                    r42.NumberFormat = "@";
                                    r42.Value2 = a.Mabn;
                                    r42.Columns.AutoFit();
                                    COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                                    r43.NumberFormat = "@";
                                    if (a.Makp != null)
                                        r43.Value2 = a.Makp;
                                    r43.Columns.AutoFit();
                                    COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                                    r44.Value2 = "";
                                    r44.Columns.AutoFit();
                                    //COMExcel.Range r45 = (COMExcel.Range)exSheet.Cells[i, 45];
                                    //r45.Value2 = a.MaBNhan;
                                    //r45.Columns.AutoFit();
                                }
                                exApp.Visible = true;//Ẩn hiện chương trình
                                exQLBV.SaveAs("C:\\Bieu79.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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
                            var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTru == 0)
                                         //join dt in _dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                                     join vp in _dataContext.VienPhis.Where(P => P.NgayTT >= ngaytu).Where(p => p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                                     join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                     join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                     join nhom in _dataContext.NhomDVs on dv.IDNhom equals nhom.IDNhom
                                     join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                     //join kp in _dataContext.KPhongs on bn.MaKP equals kp.MaKP
                                     group new { bn, rv, dv, vpct, nhom, vp } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, bn.NNhap, rv.MaKP, rv.MaICD, rv.NgayRa, bn.HanBHTu, bn.HanBHDen, bn.DChi } into kq
                                     select new
                                     {
                                         NoiTinh = kq.Key.NoiTinh,
                                         Tuyen = kq.Key.Tuyen,
                                         Makp = kq.Key.MaKP,
                                         Mabn = kq.Key.MaBNhan,
                                         TenBNhan = kq.Key.TenBNhan,
                                         NSinh = kq.Key.NamSinh,
                                         SThe = kq.Key.SThe,
                                         GTinh = kq.Key.GTinh,
                                         MaCS = kq.Key.MaCS,
                                         //nhom = kq.Key.Nhom,
                                         BHtu = kq.Key.HanBHTu,
                                         BHden = kq.Key.HanBHDen,
                                         Diachi = kq.Key.DChi,
                                         MaICD = kq.Key.MaICD,
                                         Ngaykham = kq.Key.NNhap,
                                         Ngayra = kq.Key.NgayRa,
                                         Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                         CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                         Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                         xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                         Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                         TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                         VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                         VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                         DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                         ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                         CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                         ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                         Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                         Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                         TienBN = kq.Sum(p => p.vpct.TienBN),
                                         TienBH = kq.Sum(p => p.vpct.TienBH),
                                     }).ToList().Select(x => new
                                     {
                                         dt = x.SThe.Substring(0, 2),
                                         x.NoiTinh,
                                         x.Tuyen,
                                         x.Makp,
                                         x.Mabn,
                                         x.TenBNhan,
                                         x.NSinh,
                                         x.SThe,
                                         x.GTinh,
                                         x.MaCS,
                                         x.MaICD,
                                         x.BHtu,
                                         x.BHden,
                                         x.Diachi,
                                         x.Ngaykham,
                                         x.Ngayra,
                                         Ngayra2 = x.Ngayra.Value.Date,
                                         Thuoc = x.Thuoc,
                                         x.CDHA,
                                         x.Congkham,
                                         x.xetnghiem,
                                         x.Mau,
                                         x.TTPT,
                                         x.VTYTH,
                                         x.VTTT,
                                         x.DVKTC,
                                         x.ThuocKTCG,
                                         x.CPVanchuyen,
                                         x.ThanhTien,
                                         x.Tongchi,
                                         x.Tongcong,
                                         x.TienBN,
                                         x.TienBH,
                                     }).ToList();
                            if (lupKhoaphong.Text == "")
                            {
                                if (chkGT.Checked == true)
                                {
                                    frmIn frmm = new frmIn();
                                    BaoCao.rep79aCTMoi repm = new BaoCao.rep79aCTMoi();
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngayra2).ThenBy(p => p.dt).ToList();
                                    repm.DataSource = q;
                                    repm.Ngaythang.Value = theoquy();
                                    repm.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repm.TenCQ.Value = DungChung.Bien.TenCQ;
                                    repm.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    repm.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repm.BindingData();
                                    repm.CreateDocument();
                                    frmm.prcIN.PrintingSystem = repm.PrintingSystem;
                                    frmm.ShowDialog();
                                }
                                else
                                {

                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                    q = q.OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngayra2).ThenBy(p => p.dt).ToList();
                                    rep.DataSource = q;
                                    rep.Ngaythang.Value = theoquy();
                                    rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ;
                                    rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                if (chkGT.Checked == true)
                                {
                                    int _MaKPa = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                                    frmIn frma = new frmIn();
                                    BaoCao.rep79aCTMoi repa = new BaoCao.rep79aCTMoi();
                                    repa.MaCS.Value = DungChung.Bien.MaBV;
                                    repa.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    repa.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    repa.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    q = q.Where(a => a.Makp == _MaKPa).OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngayra2).ThenBy(p => p.dt).ToList();
                                    repa.DataSource = q;
                                    repa.Ngaythang.Value = theoquy();
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    repa.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repa.BindingData();
                                    repa.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frma.prcIN.PrintingSystem = repa.PrintingSystem;
                                    frma.ShowDialog();
                                }
                                else
                                {
                                    int _MaKP = Convert.ToInt32(lupKhoaphong.EditValue);
                                    frmIn frm = new frmIn();
                                    BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                                    rep.MaCS.Value = DungChung.Bien.MaBV;
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                    rep.ngaythanghientai.Value = "Ngày ..... tháng ..... năm .....";
                                    rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    q = q.Where(a => a.Makp == _MaKP).OrderBy(p => p.NoiTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngayra2).ThenBy(p => p.dt).ToList();
                                    rep.DataSource = q;
                                    rep.Ngaythang.Value = theoquy();
                                    double st = 0;
                                    st = q.Sum(a => a.TienBH);
                                    rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (kt())
                {
                    _dtuong = "Dịch vụ";
                    DateTime ngaytu = System.DateTime.Now.Date;
                    DateTime ngayden = System.DateTime.Now.Date;
                    ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                    ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                    var q = (from bn in _dataContext.BenhNhans.Where(p => p.DTuong != "BHYT").Where(p => p.NoiTru == 0)
                             join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                             join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                             join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                             where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                             group new { bn, rv, dv, vpct, vp } by new { bn.NoiTinh, vp.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.GTinh, bn.SThe, bn.MaCS, bn.Tuyen, kb.NgayKham, rv.MaKP, rv.MaICD } into kq
                             select new
                             {
                                 NoiTinh = kq.Key.NoiTinh,
                                 Tuyen = kq.Key.Tuyen,
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 NSinh = kq.Key.NamSinh,
                                 SThe = kq.Key.SThe,
                                 GTinh = kq.Key.GTinh,
                                 MaCS = kq.Key.MaCS,
                                 MaICD = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NgayKham,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == idThuoc).Sum(p => p.vpct.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.vpct.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.IDNhom == idCongKham).Sum(p => p.vpct.ThanhTien),
                                 xetnghiem = kq.Where(p => p.dv.IDNhom == idXN).Sum(p => p.vpct.ThanhTien),
                                 Mau = kq.Where(p => p.dv.IDNhom == idMau).Sum(p => p.vpct.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.IDNhom == idTTPT).Sum(p => p.vpct.ThanhTien),
                                 VTYTH = kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.vpct.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.IDNhom == idVTTT).Sum(p => p.vpct.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.IDNhom == idDVKTC).Sum(p => p.vpct.ThanhTien),
                                 ThuocKTCG = kq.Where(p => p.dv.IDNhom == idThuocUngThuCTG).Sum(p => p.vpct.ThanhTien),
                                 CPVanchuyen = kq.Where(p => p.dv.IDNhom == idChiPhiVC).Sum(p => p.vpct.ThanhTien),
                                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                                 Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                                 TienBN = kq.Sum(p => p.vpct.TienBN),
                                 TienBH = kq.Sum(p => p.vpct.TienBH),

                             }).OrderByDescending(p => p.NoiTinh).OrderBy(p => p.Tuyen).ToList();
                    if (lupKhoaphong.Text == "")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi(_dtuong);
                        rep.DataSource = q;
                        rep.Ngaythang.Value = theoquy();
                        rep.MaCS.Value = DungChung.Bien.MaBV;
                        rep.TenCQ.Value = DungChung.Bien.TenCQ;
                        rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                        double st = 0;
                        if (q.Count > 0)
                        {
                            st = q.Sum(p => p.ThanhTien);
                        }
                        rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        int _MaKP = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                        frmIn frm = new frmIn();
                        BaoCao.rep79aCTMoi rep = new BaoCao.rep79aCTMoi();
                        rep.MaCS.Value = DungChung.Bien.MaBV;
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                        rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                        rep.DataSource = q.ToList().Where(p => p.Makp == _MaKP);
                        rep.Ngaythang.Value = theoquy();
                        double st = 0;
                        st = q.Where(a => a.Makp == _MaKP).Sum(a => a.TienBH);
                        rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                        rep.BindingData();
                        rep.CreateDocument();
                        rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void frm_rep79aCT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = (from kp in _dataContext.KPhongs
                     where (kp.PLoai == ("Lâm sàng") || kp.PLoai == ("Phòng khám"))
                     select new { kp.TenKP, kp.MaKP }).ToList();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            lupNgaytu.EditValue = ngaytu;
            lupngayden.EditValue = ngayden;
            lupNgaytu.Focus();
            lupKhoaphong.Properties.DataSource = q.ToList();
        }
        private bool kt()
        {
            if (lupNgaytu.Text == "" || lupngayden.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckBC.Checked == true)
            {
                switch (timquy(lupNgaytu.DateTime.Month))
                {
                    case 1:
                        quy = "Quý I";
                        break;
                    case 2:
                        quy = "Quý II";
                        break;
                    case 3:
                        quy = "Quý III";
                        break;
                    case 4:
                        quy = "Quý IV";
                        break;
                }

            }
            else
            {
                quy = "Từ ngày: " + lupNgaytu.DateTime.ToString().Substring(0, 10) + "  đến ngày: " + lupngayden.DateTime.ToString().Substring(0, 10);
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

        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void Xuatex_CheckedChanged(object sender, EventArgs e)
        {
            if (Xuatex.Checked)
            {
                rdFont.Enabled = true;
            }
        }

        private void rdFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdFont.SelectedIndex == 0)
            {
                Font = 0;
            }
            else
                Font = 1;
        }
    }
}