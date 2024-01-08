using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Globalization;
using Library_CLS;
namespace QLBV.FormThamSo
{
    public partial class frm_KetNoiXaPhuong_ChiTiet : DevExpress.XtraEditors.XtraForm
    {

        private List<clsKetNoiXP.ThongTinBenhNhan> _listDSBN = new List<clsKetNoiXP.ThongTinBenhNhan>();
        private List<clsKetNoiXP.CTThuoc> _listThuoc;
        private List<clsKetNoiXP.CTDV> _listDV;
        private List<clsKetNoiXP.CT_CLS> _listCLS;
        private List<clsKetNoiXP.DienBienBenh> _listDienBienBenh;   
    
        public frm_KetNoiXaPhuong_ChiTiet()
        {
            InitializeComponent();           
        }

        public frm_KetNoiXaPhuong_ChiTiet(List<clsKetNoiXP.ThongTinBenhNhan> listDSBN, List<clsKetNoiXP.CTThuoc> listThuoc, List<clsKetNoiXP.CTDV> listDV, List<clsKetNoiXP.CT_CLS> listCLS, List<clsKetNoiXP.DienBienBenh> listDienBienBenh)
        {
            // TODO: Complete member initialization
            this._listDSBN = listDSBN;
            this._listThuoc = listThuoc;
            this._listDV = listDV;
            this._listCLS = listCLS;
            this._listDienBienBenh = listDienBienBenh;
            InitializeComponent(); 
        }

        private void frm_KetNoiXaPhuong_ChiTiet_Load(object sender, EventArgs e)
        {
            loadThongTinBN(); 
        }

        private void loadThongTinBN()
        {
            if (_listDSBN.Count > 0)
            {
                #region load danh sách bệnh nhân
                var q = _listDSBN.First();
                if(!String.IsNullOrEmpty(q.Ho_ten))
                    txtHoTen.Text = q.Ho_ten;               
                if(q.Gioi_tinh == 2)
                    txtGioiTinh.Text = "Nữ";
                else
                    txtGioiTinh.Text = "Nam";
                if (!String.IsNullOrEmpty(q.Ngay_sinh))
                {
                    if (q.Ngay_sinh.ToString().Length == 8)
                    {
                        txtNgaySinh.Text = q.Ngay_sinh.ToString().Substring(6, 2);
                        txtThangSinh.Text = q.Ngay_sinh.ToString().Substring(4, 2);
                        txtNamSinh.Text = q.Ngay_sinh.ToString().Substring(0, 4);
                    }
                    if (q.Ngay_sinh.ToString().Length == 4)
                        txtNamSinh.Text = q.Ngay_sinh.ToString();
                }
                txtMucHuong.Text = q.Muc_huong.ToString();
                txtDiaChi.Text = q.Dia_chi;
                string[] formatDateTime = { "yyyyMMddHHmm" };
                string[] fomatDate = { "yyyyMMdd" };
                DateTime date;
                if (!String.IsNullOrEmpty(q.Ngay_ttoan))
                {
                    //if (DateTime.TryParseExact(q.Ngay_ttoan,
                    //                       formatDateTime,
                    //                       System.Globalization.CultureInfo.InvariantCulture,
                    //                       System.Globalization.DateTimeStyles.None,
                    //                       out date))
                    if(q.Ngay_ttoan.ToString().Length == 12)
                        dtNgayTT.EditValue = DateTime.ParseExact(q.Ngay_ttoan.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                    else if(q.Ngay_ttoan.ToString().Length == 8)
                        dtNgayTT.EditValue = DateTime.ParseExact(q.Ngay_ttoan.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(q.Ngay_vao))
                {
                    //if (DateTime.TryParseExact(q.Ngay_vao,
                    //                       formatDateTime,
                    //                       System.Globalization.CultureInfo.InvariantCulture,
                    //                       System.Globalization.DateTimeStyles.None,
                    //                       out date))
                        if (q.Ngay_vao.ToString().Length == 12)
                            dtNgayVao.EditValue = DateTime.ParseExact(q.Ngay_vao.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                        else if (q.Ngay_ttoan.ToString().Length == 8)
                            dtNgayVao.EditValue = DateTime.ParseExact(q.Ngay_vao.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(q.Ngay_ra))
                {
                    //if (DateTime.TryParseExact(q.Ngay_ra,
                    //                       formatDateTime,
                    //                       System.Globalization.CultureInfo.InvariantCulture,
                    //                       System.Globalization.DateTimeStyles.None,
                    //                       out date))
                    if (q.Ngay_ra.ToString().Length == 12)
                        dtNgayRa.EditValue = DateTime.ParseExact(q.Ngay_ra.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                    else if(q.Ngay_ra.ToString().Length == 8)
                        dtNgayRa.EditValue = DateTime.ParseExact(q.Ngay_ra.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                txtMaThe.Text = q.Ma_the;
                if (!String.IsNullOrEmpty(q.Gt_the_tu))
                {
                    if (DateTime.TryParseExact(q.Gt_the_tu,
                                           fomatDate,
                                           System.Globalization.CultureInfo.InvariantCulture,
                                           System.Globalization.DateTimeStyles.None,
                                           out date))
                        dtTheTu.EditValue = DateTime.ParseExact(q.Gt_the_tu.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(q.Gt_the_den))
                {
                    if (DateTime.TryParseExact(q.Gt_the_den,
                                           fomatDate,
                                           System.Globalization.CultureInfo.InvariantCulture,
                                           System.Globalization.DateTimeStyles.None,
                                           out date))
                        dtTheDen.EditValue = DateTime.ParseExact(q.Gt_the_den.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(q.Ma_benh))
                {
                    if (q.Ma_benh.ToString().IndexOf(";") > 0)
                    {
                        txtMaBenh.Text = q.Ma_benh.ToString().Substring(0, q.Ma_benh.ToString().IndexOf(";"));
                        if (q.Ma_benh.ToString().Length > q.Ma_benh.ToString().IndexOf(";") + 2)
                            txtMaBenhKhac.Text = q.Ma_benh.ToString().Substring(q.Ma_benh.ToString().IndexOf(";") + 1);
                    }
                    else
                        txtMaBenh.Text = q.Ma_benh;
                }
                txtTenBenh.Text = q.Ten_benh;
                if (q.Tinh_trang_rv == "1")
                    txtTinhTrangrv.Text = "Ra viện";
                else if (q.Tinh_trang_rv == "2")
                    txtTinhTrangrv.Text = "Chuyển viện";
                else if (q.Tinh_trang_rv == "3")
                    txtTinhTrangrv.Text = "Trốn viện";
                else if(q.Tinh_trang_rv == "4")
                    txtTinhTrangrv.Text = "Xin ra viện";

                if (q.Ket_qua_dtri == "1")
                    txtTinhTrangrv.Text = "Khỏi";
                else if (q.Ket_qua_dtri == "2")
                    txtTinhTrangrv.Text = "Đỡ";
                else if (q.Ket_qua_dtri == "3")
                    txtTinhTrangrv.Text = "Không thay đổi";
                else if (q.Ket_qua_dtri == "4")
                    txtTinhTrangrv.Text = "Nặng hơn";
                else if (q.Ket_qua_dtri == "5")
                    txtTinhTrangrv.Text = "Tử vong";

                txtTongTien.Text = q.T_tongchi.ToString();
                txtBHTT.Text = q.T_bhtt.ToString();
                txtBNTT.Text = q.T_bntt.ToString();
                txtTienThuoc.Text = q.T_thuoc.ToString();
                txtVatTuYT.Text = q.T_vtyt.ToString();

               
                #endregion load danh sách bệnh nhân

                #region load danh sách thuốc
                grcThuoc.DataSource = _listThuoc;
                #endregion load danh sách thuốc

                #region load danh sách dịch vụ
                grc_Dichvu.DataSource = _listDV;
                #endregion load danh sách dịch vụ

                #region load danh sách CLS
                grc_CLS.DataSource = _listCLS;
                #endregion load danh sách CLS

                #region load danh sách diễn biến bệnh
                grc_Dienbien.DataSource = _listDienBienBenh;
                #endregion load danh sách diễn biến bệnh
            }
        }

       
    }
}