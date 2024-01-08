using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.DungChung;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    /// <summary>
    ///A. Xuất XML: Bảng 2,3 thiếu ma_bac_si (Bác sỹ chỉ định theo chứng chỉ hành nghề); ngày_yl: ngày ra y lệnh : yyyymmddHHmm; Bảng 3 thiếu ngay_kq: ngày có kết quả( định dạng như ngay_yl)
    /// Bảng 2: Biểu 20; Bảng 3 tách ra làm biểu 19( VTYT) và biểu 21 (dịch vụ)
    ///B. Báo cáo thống kê thuốc theo đối tượng : sử dụng cho Tam đường
    /// </summary>
    public partial class frmTsBcMau19_20_21_1399 : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcMau19_20_21_1399()
        {
            InitializeComponent();
        }

        private int _mauso = 20, Font = 0;

        public frmTsBcMau19_20_21_1399(int mau)
        {
            _mauso = mau;
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24012")
            {
                chk_3762.Visible = true;
            }
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

        private string tenbc = string.Empty;

        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " QUÝ I NĂM " + lupTuNgay.DateTime.Year;
                        break;

                    case 2:
                        quy = " QUÝ II NĂM " + lupTuNgay.DateTime.Year;
                        break;

                    case 3:
                        quy = " QUÝ III NĂM " + lupTuNgay.DateTime.Year;
                        break;

                    case 4:
                        quy = " QUÝ IV NĂM " + lupTuNgay.DateTime.Year;
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

        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private int load = 0;
        private List<NhomDV> _listNhomDV = new List<NhomDV>();

        private class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }

        private void frmTsBcMau20_1399_Load(object sender, EventArgs e)
        {
            ckc_BNCCT.Checked = true;
            ckBHTT.Checked = true;
            ckBNTT.Checked = true;

            if (DungChung.Bien.MaBV == "24012")
            {
                chk_3762.Visible = true;
            }

            // this.Text = "Báo cáo mẫu số " + _mauso + "/BHYT";
            // load nhà cung cấp
            rdHTThanhToan.SelectedIndex = 2;
            rdCKhoan.SelectedIndex = 2;
            rdChonBieu.EditValue = _mauso;
            var nhacc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.Properties.DataSource = nhacc;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            if (DungChung.Bien.MaBV == "12001")// bệnh viện tam đường
                ckDtuongCT.Visible = true;
            else
                ckDtuongCT.Visible = false;

            if (DungChung.Bien.MaBV == "30004")
            {
                this.radTimKiem.Properties.Items.Clear();
                this.radTimKiem.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Ngày ra viện"),
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Ngày T.Toán"),
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Ngày duyệt TT"),
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "Ngày duyệt CP"),
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "Ngày thu"),
                                                            new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "Ngày thực hiện")});
                if (rdChonBieu.SelectedIndex == 2)
                {
                    radTimKiem.Properties.Items[5].Enabled = true;
                }
                else
                {
                    radTimKiem.Properties.Items[5].Enabled = false;
                }
            }

            List<MyObject> lMaDtuong = new List<MyObject>();
            lMaDtuong = data.DTuongs.Where(p => p.Status == 1).Select(p => new MyObject { value = p.MaDTuong, Text = p.MaDTuong }).OrderBy(p => p.Text).ToList();
            //lMaDtuong = data.BenhNhans.Select(p => new MyObject { value = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper(), Text = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper() }).Distinct().OrderBy(p => p.Text).ToList();
            lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            if (lMaDtuong.Count <= 0)
                MessageBox.Show("Danh mục Đối Tượng chưa được thiết lập!");
            cklMaDTuong.DataSource = lMaDtuong;
            cklMaDTuong.CheckAll();

            // load nhóm Dv

            // load danh sách khoa phòng
            var q3 = (from k in data.KPhongs
                      join rv in data.RaViens on k.MaKP equals rv.MaKP
                      select k).ToList();
            var q2 = (from k in q3
                      select k).Distinct().ToList();
            var q = (from k in q2
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP,
                         PLoai = k.PLoai
                     }).Distinct().OrderBy(p => p.PLoai).ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }
            //  // load ds nhóm dịch vụ
            //  List<NhomDV> _lNhomDV = data.NhomDVs.OrderBy(p => p.TenNhom).ToList();
            //  _lNhomDV.Insert(0, new NhomDV { IDNhom = 0, TenNhom = "Tất cả" });
            //  lupNhomDV.Properties.DataSource = _lNhomDV;
            //  lupNhomDV.Text = "Tất cả";
            ////  txtDuongDan.Text = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xls";
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.Text = "BHYT";
            radNoiTru_SelectedIndexChanged(sender, e);
            radio_DTNT.SelectedIndex = 2;
            radXP_SelectedIndexChanged(sender, e);
            load++;
        }

        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP
            { get { return _maKP; } set { _maKP = value; } }
            public bool Check
            { get { return _check; } set { _check = value; } }
            public string TenKP
            { get { return _kp; } set { _kp = value; } }

            public string PLoai { get; set; }
        }

        private List<KPhong> _lKP = new List<KPhong>();

        private string MaKPQD(int mKP)
        {
            string rs = "";
            var a = _lKP.Where(p => p.MaKP == mKP).Select(p => p.MaQD).FirstOrDefault();
            if (a != null)
                rs = a.ToString();
            return rs;
        }

        private List<TamUng> _lIDtamung = new List<TamUng>();

        public string[,] NhomTheoTT(string tennhom, string tennhomct, string tenrg, int? id5937)
        {
            string[,] re = new string[1, 2] { { tennhom, "99" } };
            if (ck_CV285_20001.Checked)
            {
                if (tennhomct == "Khám bệnh")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (tennhomct == "Giường điều trị ngoại trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Giường điều trị nội trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Xét nghiệm")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "3";
                }
                if (tennhomct == "Chẩn đoán hình ảnh")
                {
                    re[0, 0] = tennhom; //+ ", Thăm dò chức năng";
                    re[0, 1] = "4";
                }

                if (tennhomct == "Thăm dò chức năng")
                {
                    re[0, 0] = tennhom;// "Chẩn đoán hình ảnh, " + tennhom;
                    re[0, 1] = "5";
                }

                if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                {
                    if (tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tennhom;
                        re[0, 1] = "6";
                    }
                }
                else
                {
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)//(tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "6";
                    }
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "7";
                    }
                }
                if (tennhomct == "Vận chuyển")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "8";
                }
                if (tennhomct == "VTYT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "9";
                }
                if (tennhomct == "DVKT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "10";
                }
                if (tennhomct == "Máu và chế phẩm của máu")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "11";
                }
                if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "12";
                }
                if (tennhomct == "Thuốc trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "13";
                }
                if (tennhomct == "Vật tư y tế trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "14";
                }
                if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "15";
                }
            }
            else
            {
                if (id5937 == 13)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (id5937 == 14)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (id5937 == 15)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "2";
                }
                if (id5937 == 1)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "3";
                }
                if (id5937 == 2)
                {
                    re[0, 0] = tennhom + ", Thăm dò chức năng";
                    re[0, 1] = "4";
                }

                if (id5937 == 3)
                {
                    re[0, 0] = "Chẩn đoán hình ảnh, thăm dò chức năng" /*+ tennhom*/;
                    re[0, 1] = "4";
                }
                if (id5937 == 8 || id5937 == 18)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "5";
                }
                if (id5937 == 12)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "6";
                }
                if (id5937 == 11)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "7";
                }
                if (id5937 == 9)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "8";
                }
                if (id5937 == 7)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "9";
                }
                if (id5937 == 6)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "10";
                }
                if (id5937 == 4)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "11";
                }
                if (id5937 == 10)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "12";
                }
                if (id5937 == 5)
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "13";
                }
            }
            return re;
            //string[,] re = new string[1, 2] { { tennhom, "99" } };
            //if (ck_CV285_20001.Checked)
            //{
            //    if (tennhomct == "Khám bệnh")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "1";
            //    }
            //    if (tennhomct == "Giường điều trị ngoại trú")
            //    {
            //        re[0, 0] = "Ngày giường";
            //        re[0, 1] = "2";
            //    }
            //    if (tennhomct == "Giường điều trị nội trú")
            //    {
            //        re[0, 0] = "Ngày giường";
            //        re[0, 1] = "2";
            //    }
            //    if (tennhomct == "Xét nghiệm")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "3";
            //    }
            //    if (tennhomct == "Chẩn đoán hình ảnh")
            //    {
            //        re[0, 0] = tennhom; //+ ", Thăm dò chức năng";
            //        re[0, 1] = "4";
            //    }

            //    if (tennhomct == "Thăm dò chức năng")
            //    {
            //        re[0, 0] = tennhom;// "Chẩn đoán hình ảnh, " + tennhom;
            //        re[0, 1] = "5";
            //    }

            //    if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
            //    {
            //        if (tennhomct == "Thủ thuật, phẫu thuật")
            //        {
            //            re[0, 0] = tennhom;
            //            re[0, 1] = "6";
            //        }
            //    }
            //    else
            //    {
            //        if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)//(tennhomct == "Thủ thuật, phẫu thuật")
            //        {
            //            re[0, 0] = tenrg;
            //            re[0, 1] = "6";
            //        }
            //        if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
            //        {
            //            re[0, 0] = tenrg;
            //            re[0, 1] = "7";
            //        }
            //    }
            //    if (tennhomct == "Vận chuyển")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "8";
            //    }
            //    if (tennhomct == "VTYT thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "9";
            //    }
            //    if (tennhomct == "DVKT thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "10";
            //    }
            //    if (tennhomct == "Máu và chế phẩm của máu")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "11";
            //    }
            //    if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "12";
            //    }
            //    if (tennhomct == "Thuốc trong danh mục BHYT")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "13";
            //    }
            //    if (tennhomct == "Vật tư y tế trong danh mục BHYT")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "14";
            //    }
            //    if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "15";
            //    }
            //}
            //else
            //{
            //    if (tennhomct == "Khám bệnh")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "1";
            //    }
            //    if (tennhomct == "Giường điều trị ngoại trú")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "1";
            //    }
            //    if (tennhomct == "Giường điều trị nội trú")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "2";
            //    }
            //    if (tennhomct == "Xét nghiệm")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "3";
            //    }
            //    if (tennhomct == "Chẩn đoán hình ảnh")
            //    {
            //        re[0, 0] = tennhom + ", Thăm dò chức năng";
            //        re[0, 1] = "4";
            //    }

            //    if (tennhomct == "Thăm dò chức năng")
            //    {
            //        re[0, 0] = "Chẩn đoán hình ảnh, " + tennhom;
            //        re[0, 1] = "4";
            //    }
            //    if (tennhomct == "Thủ thuật, phẫu thuật")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "5";
            //    }
            //    if (tennhomct == "Vận chuyển")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "6";
            //    }
            //    if (tennhomct == "VTYT thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "7";
            //    }
            //    if (tennhomct == "DVKT thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "8";
            //    }
            //    if (tennhomct == "Máu và chế phẩm của máu")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "9";
            //    }
            //    if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "10";
            //    }
            //    if (tennhomct == "Thuốc trong danh mục BHYT")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "11";
            //    }
            //    if (tennhomct == "Vật tư y tế trong danh mục BHYT")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "12";
            //    }
            //    if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
            //    {
            //        re[0, 0] = tennhom;
            //        re[0, 1] = "13";
            //    }
            //}
            //return re;
        }
        public string[,] NhomTheoTT(string tennhom, string tennhomct, string tenrg, int? id5937, int idTieuNhom)
        {
            string[,] re = new string[1, 2] { { tennhom, "99" } };
            if (ck_CV285_20001.Checked)
            {
                if (tennhomct == "Khám bệnh")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "1";
                }
                if (tennhomct == "Giường điều trị ngoại trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Giường điều trị nội trú")
                {
                    re[0, 0] = "Ngày giường";
                    re[0, 1] = "2";
                }
                if (tennhomct == "Xét nghiệm")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "3";
                }
                if (tennhomct == "Chẩn đoán hình ảnh")
                {
                    re[0, 0] = tennhom; //+ ", Thăm dò chức năng";
                    re[0, 1] = "4";
                }

                if (tennhomct == "Thăm dò chức năng")
                {
                    re[0, 0] = tennhom;// "Chẩn đoán hình ảnh, " + tennhom;
                    re[0, 1] = "5";
                }

                if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                {
                    if (tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tennhom;
                        re[0, 1] = "6";
                    }
                }
                else
                {
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)//(tennhomct == "Thủ thuật, phẫu thuật")
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "6";
                    }
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                    {
                        re[0, 0] = tenrg;
                        re[0, 1] = "7";
                    }
                }
                if (tennhomct == "Vận chuyển")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "8";
                }
                if (tennhomct == "VTYT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "9";
                }
                if (tennhomct == "DVKT thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "10";
                }
                if (tennhomct == "Máu và chế phẩm của máu")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "11";
                }
                if (tennhomct == "Thuốc thanh toán theo tỷ lệ")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "12";
                }
                if (tennhomct == "Thuốc trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "13";
                }
                if (tennhomct == "Vật tư y tế trong danh mục BHYT")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "14";
                }
                if (tennhomct == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
                {
                    re[0, 0] = tennhom;
                    re[0, 1] = "15";
                }
            }
            else
            {
                if (id5937 == 4 && idTieuNhom == 86)
                {
                    re[0, 0] = "Vị thuốc YHCT";
                }
                else if (id5937 == 4 && idTieuNhom == 77)
                {
                    re[0, 0] = "Chế phẩm YHCT";
                }
                else
                {
                    re[0, 0] = "Thuốc tân dược";
                }
            }
            return re;
        }

        private List<NhomDV> _lnhom = new List<NhomDV>();

        private void btnInBC_Click(object sender, EventArgs e)
        {
            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _dsCSKCB.Add(cklKP.GetItemValue(i).ToString());
                }
            }
            _dsCSKCB = _dsCSKCB.Distinct().ToList();

            #region Lấy danh sách khoa phòng

            List<string> _dsTN = new List<string>();
            for (int i = 0; i < cklTN.ItemCount; i++)
            {
                if (cklTN.GetItemChecked(i))
                {
                    _dsTN.Add(cklTN.GetItemValue(i).ToString());
                }
            }
            _dsTN = _dsTN.Distinct().ToList();

            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? -1 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));

                        if (mKhoa == 0)
                        {
                            kp = 0;

                            break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }

            #endregion Lấy danh sách khoa phòng

            #region Biến

            _lKP = data.KPhongs.ToList();
            string macc = "";
            int _makp = -1;

            DateTime tungay, denngay;
            int trongBH = 5;

            int _idDtuong = -1;
            if (lupDoituong.EditValue != null)
                _idDtuong = Convert.ToInt16(lupDoituong.EditValue);
            List<string> lmaDtuong = new List<string>();
            for (int i = 0; i < cklMaDTuong.ItemCount; i++)
            {
                if (cklMaDTuong.GetItemChecked(i))
                    lmaDtuong.Add(cklMaDTuong.GetItemValue(i).ToString());
            }
            if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                macc = lupNhaCC.EditValue.ToString();
            int xp = radXP.SelectedIndex;
            int _intduyet = 2;
            if (rad_Duyet.SelectedIndex != null)
                _intduyet = rad_Duyet.SelectedIndex;
            if (KTtaoBcMau20())
            {
                trongBH = rdTrongBH.SelectedIndex;

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;
                List<string> _ltamung = new List<string>();
                int _CP_BH = -1;
                if ((ckc_BNCCT.Checked && ckBHTT.Checked && ckBNTT.Checked) || (!ckc_BNCCT.Checked && !ckBHTT.Checked && !ckBNTT.Checked))
                    _CP_BH = 0;
                int HTThanhToan = rdHTThanhToan.SelectedIndex;
                int ChuyenKhoan = rdCKhoan.SelectedIndex;

                #endregion Biến

                #region select tất cả

                List<int> _idNhomDV = new List<int>();
                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                        _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
                }

                _lnhom = (from nhom in data.NhomDVs join id in _idNhomDV on nhom.IDNhom equals id select nhom).ToList();
                var qdv = (from nhom in _lnhom
                           join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new
                           {
                               nhom.IDNhom,
                               nhom.TenNhomCT,
                               nhom.TenNhom,
                               tn.IdTieuNhom,
                               tn.TenTN,
                               tn.TenRG,
                               dv
                           }).ToList();

                if (_mauso == 20 && chkTieuNhom.Checked == true)
                {
                    qdv = (from nhom in _lnhom
                           join tn in data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           where (_dsTN.Contains(tn.IdTieuNhom.ToString()))
                           select new
                           {
                               nhom.IDNhom,
                               nhom.TenNhomCT,
                               nhom.TenNhom,
                               tn.IdTieuNhom,
                               tn.TenTN,
                               tn.TenRG,
                               dv
                           }).ToList();
                }

                #region tạm ứng: lấy phần thu thẳng của bệnh nhân khi chọn thu thẳng và mẫu 21, BV 30002

                var qtu = (from tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3)
                           join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                           select new { tu.MaBNhan, tu.IDTamUng, tuct.MaDV, tu.ThanhToan }).ToList();

                #endregion tạm ứng: lấy phần thu thẳng của bệnh nhân khi chọn thu thẳng và mẫu 21, BV 30002

                var q = (from rv in data.RaViens
                         join bn in data.BenhNhans
                         on rv.MaBNhan equals bn.MaBNhan
                         join ttbx in data.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                         join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts//.Where(p => (DungChung.Bien.MaBV == "30002" && _mauso == 21 && radioThuChi.SelectedIndex == 0) ? (qtu.Where(p=>p.MaBNhan == )) : true)
                         on vp.idVPhi equals vpct.idVPhi
                         where ((radTimKiem.SelectedIndex == 4) ? true : (_ngaytt == 2 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (_ngaytt == 1 ? (vp.NgayTT >= tungay && vp.NgayTT <= denngay) : (_ngaytt == 3 ? (vp.NgayDuyetCP >= tungay && vp.NgayDuyetCP <= denngay) : (DungChung.Bien.MaBV == "30004" && _mauso == 21 && _ngaytt == 5 ? (vpct.NgayYLenh >= tungay && vpct.NgayYLenh <= denngay) : (rv.NgayRa >= tungay && rv.NgayRa <= denngay))))))
                         where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                         where (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 1 ? bn.DTNT : bn.DTNT == false))
                         where (radioThuChi.SelectedIndex == 2 || (radioThuChi.SelectedIndex == 0 && vpct.ThanhToan == 1) || (radioThuChi.SelectedIndex == 1 && vpct.ThanhToan == 0))
                         //  where((DungChung.Bien.MaBV == "30002" && _mauso == 21 && radioThuChi.SelectedIndex == 0) ? (qtu.Where(p=>p.MaBNhan == vp.MaBNhan).Where(p=>p.MaDV == vpct.MaDV.Value && vpct.ThanhToan == 1).Count() >0) : true)
                         select new
                         {
                             bn.MaKCB,
                             bn.DTNT,
                             bn.MaBNhan,
                             bn.DTuong,
                             MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                             bn.IDDTBN,
                             bn.NoiTru,
                             bn.TuyenDuoi,
                             vpct.TrongBH,
                             MaKP = vpct.MaKP == null ? 0 : vpct.MaKP,
                             rv.NgayRa,
                             rv.MaICD,
                             vp.NgayTT,
                             vpct.DonGia,
                             vpct.MaDV,
                             vpct.SoLuong,
                             vpct.ThanhTien,
                             vpct.TienBH,
                             vpct.TienBN,
                             vpct.IDTamUng,
                             vp.NgayDuyet,
                             vpct.TyLeTT,
                             vpct.XHH,
                             vpct.LoaiDV,
                             vpct.TBNCTT,
                             vpct.TBNTT
                         }).ToList();

                #endregion select tất cả

                List<cls19_20> _list = new List<cls19_20>();
                List<cls19_20> _listXML = new List<cls19_20>();
                //var q_tu = (from b in q
                //            join lcs in _lCSKCB on b.MaKCB equals lcs.MaKP
                //            where (_intduyet == 2 ? true : (_intduyet == 1 ? b.NgayDuyet != null : b.NgayDuyet == null))
                //            select b.MaBNhan).Distinct().ToList();
                //if (radioThuChi.SelectedIndex < 2)
                //{
                //    _lIDtamung = (from b in q_tu
                //                  join tu in data.TamUngs on b equals tu.MaBNhan
                //                  where radioThuChi.SelectedIndex == 0 ? (tu.PhanLoai == 3) : (tu.PhanLoai == 1 || tu.PhanLoai == 2)
                //                  select tu).ToList();
                //}
                var q6 = (from l in q.Where(p => (radTimKiem.SelectedIndex == 4) ? (qtu.Where(o => o.MaBNhan == p.MaBNhan).Where(o => o.MaDV == p.MaDV).Count() > 0) : true)
                          join dt in lmaDtuong on l.MaDTuong equals dt
                          join nhom in qdv on l.MaDV equals nhom.dv.MaDV
                          join lcs in _dsCSKCB on l.MaKCB equals lcs
                          where (l.IDDTBN == _idDtuong)
                          where (macc == "" ? true : nhom.dv.MaCC == macc)
                          where (kp == 0 ? true : _lMaKhoa.Contains(l.MaKP == null ? 0 : l.MaKP.Value))
                          where trongBH > 2 ? true : (l.TrongBH == trongBH) && (radXP.SelectedIndex < 3 ? l.TuyenDuoi == radXP.SelectedIndex : true)
                          where (radNoiTru.SelectedIndex < 2 ? l.NoiTru == radNoiTru.SelectedIndex : true)
                          where (_intduyet == 2 ? true : (_intduyet == 1 ? l.NgayDuyet != null : l.NgayDuyet == null))
                          where (ckcTheoYC.Checked ? l.LoaiDV == 1 : l.LoaiDV == 0)
                          //where nhom.dv.MaNhom5937 == 8 || nhom.dv.MaNhom5937 != 8
                          select new
                          {
                              l.TyLeTT,
                              l.MaKCB,
                              //  IDTamUng = _lIDtamung.Where(p => p.IDTamUng == l.IDTamUng).ToList().Count > 0 ? _lIDtamung.Where(p => p.IDTamUng == l.IDTamUng).First().IDTamUng : 0,
                              l.MaBNhan,
                              l.NoiTru,
                              l.MaDTuong,
                              MaKP = kp == 0 ? "" : MaKPQD(l.MaKP ?? 0),
                              l.MaICD,
                              l.TrongBH,
                              DonGia = chkTTTheoTyLe.Checked ? ((l.DonGia * l.TyLeTT) / 100) : l.DonGia,
                              nhom.dv.DonGiaTT15,
                              nhom.dv.SoTTqd,
                              //nhom.dv.IDNhom,
                              nhom.dv.MaNhom5937,
                              nhom.dv.BHTT,
                              nhom.TenNhomCT,
                              nhom.TenNhom,
                              nhom.IdTieuNhom,
                              nhom.TenTN,
                              nhom.dv.TenHC,
                              nhom.dv.MaQD,
                              nhom.dv.DuongD,
                              nhom.dv.MaDuongDung,
                              TenDV = (DungChung.Bien.MaBV == "12122" && _mauso == 20) ? (nhom.dv.TenDV + " " + nhom.dv.HamLuong) : nhom.dv.TenDV,
                              nhom.dv.HamLuong,
                              nhom.dv.DonVi,
                              nhom.dv.SoDK,
                              nhom.dv.MaDV,
                              nhom.dv.QCPC,
                              nhom.dv.MaCC,
                              nhom.dv.SoQD,
                              nhom.dv.NuocSX,
                              nhom.dv.NhaSX,
                              nhom.dv.MaTam,
                              nhom.dv.DangBC,
                              nhom.TenRG,
                              TenRGG = nhom.dv.TenRG,
                              GiaThau = nhom.dv.DonGia,
                              sapxep = nhom.dv.DongY == 1 ? 3 : ((nhom.TenRG == "Thuốc đông y" || nhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT) ? 2 : 1),
                              tensapxep = nhom.dv.DongY == 1 ? "Thuốc đông y" : ((nhom.TenRG == "Thuốc đông y" || nhom.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT) ? "Chế phẩm y học cổ truyền" : "Thuốc tân dược"),
                              l.SoLuong,
                              ThanhTien = Math.Round(_CP_BH == 0 ? ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30303") ? l.ThanhTien : l.ThanhTien * (chkTTTheoTyLe.Checked ? (l.TyLeTT / 100) : 1)) : (((ckBHTT.Checked ? l.TienBH : 0) + (ckBNTT.Checked ? l.TBNTT : 0) + (ckc_BNCCT.Checked ? l.TBNCTT : 0)) * (chkTTTheoTyLe.Checked ? (l.TyLeTT / 100) : 1)), 2, MidpointRounding.AwayFromZero), //chkLamTron.Checked ? (_CP_BH == 0 ? (Math.Round(Math.Round(l.SoLuong, 2) * Math.Round(l.DonGia, 2) * (l.TyLeTT / 100), 0)) : (_CP_BH == 1 ? l.TienBN : l.TienBH)) : (_CP_BH == 0 ? l.ThanhTien : (_CP_BH == 1 ? l.TienBN : l.TienBH))
                              TienBH = Math.Round(_CP_BH == 0 ? l.TienBH : (ckBHTT.Checked ? l.TienBH : 0), 2, MidpointRounding.AwayFromZero),
                              nhom.dv.DongY
                          }).ToList();

                #region chỉ tìm những bệnh nhân có thực hiện dịch vụ nào đấy

                List<int> _lMaBN_DV = new List<int>();
                if (_lDSMaDV.Count > 0 && _mauso == 21)
                {
                    var qdsbn = (from bn in q join dv in _lDSMaDV on bn.MaDV equals dv group bn by bn.MaBNhan into kq1 select new { MaBNhan = kq1.Key }).ToList();
                    _lMaBN_DV = qdsbn.Select(p => p.MaBNhan).ToList();
                }
                //var _ltranfer = data.Tranfers.ToList();
                //List<_ltranfer> _ltran = new List<_ltranfer>();
                //foreach (var item in _ltranfer)
                //{
                //    _ltranfer moi = new _ltranfer();
                //    moi.Mabn = item.MaBNhan.ToString();
                //    _ltran.Add(moi);
                //}
                //if (ChuyenKhoan == 1)
                //{
                //    _list = (from ab in _list
                //             join ba in _ltran on ab.Ma_lk equals ba.Mabn
                //             select ab).ToList();
                //}
                List<int> _ltranfer = data.Tranfers.Select(p => p.MaBNhan).ToList();

                #endregion chỉ tìm những bệnh nhân có thực hiện dịch vụ nào đấy

                #region group để tính tổng tiền theo bệnh nhân

                var q2 = (from lq in q6.Where(p => (_lDSMaDV.Count > 0 && _mauso == 21) ? _lMaBN_DV.Contains(p.MaBNhan) : true).Where(p => ChuyenKhoan == 1 ? _ltranfer.Contains(p.MaBNhan) : (ChuyenKhoan == 0 ? (!_ltranfer.Contains(p.MaBNhan)) : true))
                          group lq by new
                          {
                              lq.BHTT,
                              //lq.MaKCB,
                              lq.TrongBH,
                              lq.DonGia,
                              lq.DonGiaTT15,
                              lq.SoTTqd,
                              //lq.IDNhom,
                              lq.MaNhom5937,
                              lq.TenNhomCT,
                              lq.TenNhom,
                              lq.IdTieuNhom,
                              lq.TenTN,
                              lq.TenHC,
                              lq.MaQD,
                              lq.DangBC,
                              lq.DuongD,
                              lq.MaDuongDung,
                              lq.QCPC,
                              lq.TenDV,
                              lq.MaCC,
                              lq.HamLuong,
                              lq.DonVi,
                              lq.SoDK,
                              lq.MaDV,
                              lq.SoQD,
                              lq.NuocSX,
                              lq.NhaSX,
                              lq.GiaThau,
                              lq.MaTam,
                              lq.sapxep,
                              lq.tensapxep,
                              lq.TenRG,
                              lq.TenRGG,
                              lq.TienBH,
                              TyLeTT = DungChung.Bien.MaBV == "27194" || DungChung.Bien.MaBV == "24012" ? lq.TyLeTT : (ck_CV285_20001.Checked ? lq.TyLeTT : 1),
                              lq.DongY
                          } into kq
                          select new
                          {
                              // kq.Key.MaKCB,
                              TyLeTT = kq.Key.TyLeTT /*: kq.Select(x => x.TyLeTT).FirstOrDefault()*/,
                              MaKCB = string.Join(",", kq.Select(p => p.MaKCB).Distinct()),
                              TrongBH = kq.Key.TrongBH,
                              DonGia = kq.Select(x => x.DonGia).FirstOrDefault(),
                              DonGiaTT15 = kq.Key.DonGiaTT15,
                              SoTTqd = kq.Key.SoTTqd,
                              //IdNhom = kq.Key.IDNhom,
                              MaNhom5937 = kq.Key.MaNhom5937,
                              TenNhomCT = kq.Key.TenNhomCT,
                              TenNhomThuoc = kq.Key.TenNhom,
                              kq.Key.IdTieuNhom,
                              //TenTNhom = kq.Key.TenTN,
                              TenHC = kq.Key.TenHC,
                              MaQD = kq.Key.MaQD,
                              DuongDung = kq.Key.DuongD,
                              kq.Key.MaDuongDung,
                              kq.Key.NuocSX,
                              kq.Key.NhaSX,
                              kq.Key.GiaThau,
                              kq.Key.MaTam,
                              TenThuoc = kq.Key.TenDV,
                              HamLuong = kq.Key.HamLuong,
                              DonVi = kq.Key.DonVi,
                              SoDK = kq.Key.SoDK,
                              MaDV = kq.Key.MaDV,
                              MaCC = kq.Key.MaCC,
                              SoQD = kq.Key.SoQD,
                              TenRGG = kq.Key.TenRGG,
                              kq.Key.QCPC,
                              kq.Key.sapxep,
                              kq.Key.tensapxep,
                              kq.Key.DangBC,
                              SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                              SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                              BHTT = kq.Key.BHTT,
                              Thanhtien_NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.ThanhTien),
                              Thanhtien_Ngtru = kq.Where(p => p.NoiTru == 0).Sum(p => p.ThanhTien),
                              SoLuong139 = kq.Where(p => p.MaDTuong == "DT" || p.MaDTuong == "HN" || p.MaDTuong == "DK").Sum(p => p.SoLuong),
                              SoLuongTE = kq.Where(p => p.MaDTuong == "TE").Sum(p => p.SoLuong),
                              SoLuongBHYT_DV = (lupDoituong.Text == "Dịch vụ") ? (kq.Sum(p => p.SoLuong)) : ((kq.Where(p => p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong))),
                              SoLuong = kq.Sum(p => p.SoLuong),
                              ThanhTien = kq.Where(x => x.NoiTru == 1).Sum(p => p.SoLuong * p.DonGia * p.TyLeTT / 100) + kq.Where(x => x.NoiTru == 0).Sum(p => p.DonGia * p.SoLuong * p.TyLeTT / 100),
                              TienBH = kq.Where(p => p.NoiTru == 1).Sum(x => x.TienBH) + kq.Where(p => p.NoiTru == 0).Sum(x => x.TienBH),
                              Don_giaBHYT = (kq.Key.TyLeTT / 100) * kq.Key.DonGia,
                              TienBHYTThanhToan = kq.Where(x => x.NoiTru == 1).Sum(p => (p.TyLeTT / 100) * (p.SoLuong) * p.DonGia) + kq.Where(x => x.NoiTru == 0).Sum(p => (p.TyLeTT / 100) * (p.SoLuong) * p.DonGia),
                              kq.Key.TenRG,
                              kq.Key.DongY
                          }).OrderBy(p => p.TenThuoc).ToList();

                var q56 = q2.GroupBy(x => new
                {
                    x.TyLeTT,
                    x.DonGia,
                    // x.DonGiaTT15,
                    x.SoTTqd,
                    x.MaNhom5937,
                    x.TrongBH,
                    x.TenNhomCT,
                    x.TenNhomThuoc,
                    x.IdTieuNhom,
                    //x.TenTNhom,
                    x.TenHC,
                    x.MaQD,
                    x.DuongDung,
                    x.MaDuongDung,
                    //x.NuocSX,
                    //x.NhaSX,
                    //x.GiaThau,
                    //x.MaTam,
                    x.TenThuoc,
                    x.HamLuong,
                    x.DonVi,
                    x.SoDK,
                    x.MaDV,
                    x.MaCC,
                    x.SoQD,
                    x.TenRGG,
                    x.QCPC,
                    x.sapxep,
                    x.tensapxep,
                    x.DangBC,
                    x.BHTT,
                    x.TenRG,
                    //x.ThanhTien,
                    x.Don_giaBHYT,
                    //x.SoLuongNgT,
                    //x.SoLuongNT,,
                    x.DongY
                }).Select(x => new
                {
                    SoLuongNT = x.Sum(p => p.SoLuongNT),
                    SoLuongNgT = x.Sum(p => p.SoLuongNgT),
                    Don_giaBHYT = x.Key.Don_giaBHYT,//Sum(p => p.Don_giaBHYT),
                    TienBHYTThanhToan = x.Sum(p => p.TienBH), 
                    ThanhTien = x.Sum(p => p.ThanhTien),
                    x.Key.TyLeTT,
                    x.Key.DonGia,
                    //x.Key.DonGiaTT15,
                    x.Key.SoTTqd,
                    x.Key.MaNhom5937,
                    //x.Key.MaNhom5937,
                    x.Key.TrongBH,
                    x.Key.TenNhomCT,
                    x.Key.TenNhomThuoc,
                    x.Key.IdTieuNhom,
                    //x.Key.TenTNhom,
                    x.Key.TenHC,
                    x.Key.MaQD,
                    x.Key.DuongDung,
                    x.Key.MaDuongDung,
                    //x.Key.NuocSX,
                    //x.Key.NhaSX,
                    //x.Key.GiaThau,
                    //x.Key.MaTam,
                    x.Key.TenThuoc,
                    x.Key.HamLuong,
                    x.Key.DonVi,
                    x.Key.SoDK,
                    x.Key.MaDV,
                    x.Key.MaCC,
                    x.Key.SoQD,
                    x.Key.TenRGG,
                    x.Key.QCPC,
                    x.Key.sapxep,
                    x.Key.tensapxep,
                    x.Key.DangBC,
                    x.Key.BHTT,
                    x.Key.TenRG,
                    //Thanhtien_Ngtru = x.Sum(p => p.Thanhtien_Ngtru),
                    //Thanhtien_NoiTru = x.Sum(p => p.Thanhtien_NoiTru),
                    //SoLuong = x.Sum(p => p.SoLuong),
                    SoLuongTE = x.Sum(p => p.SoLuongTE),
                    SoLuong139 = x.Sum(p => p.SoLuong139),
                    SoLuongBHYT_DV = x.Sum(p => p.SoLuongBHYT_DV),
                    x.Key.DongY
                }).ToList();

                #endregion group để tính tổng tiền theo bệnh nhân

                #region đổ vào list cls19_20

                foreach (var l in q56)
                {
                    if (l.TenThuoc == "Ngưu tất")
                    {
                        string a = "";
                    }
                    cls19_20 cls = new cls19_20();
                    // cls.Ma_CSKCB = l.MaKCB;
                    cls.Stt = Convert.ToInt32(NhomTheoTT(l.TenNhomThuoc, l.TenNhomCT, l.TenRG, l.MaNhom5937)[0, 1]);
                    switch (cls.Stt)
                    {
                        case 1:
                            cls.STTNhomHT = "I.";
                            break;

                        case 2:
                            cls.STTNhomHT = "II.";
                            break;

                        case 3:
                            cls.STTNhomHT = "III.";
                            break;

                        case 4:
                            cls.STTNhomHT = "IV.";
                            break;

                        case 5:
                            cls.STTNhomHT = "V.";
                            break;

                        case 6:
                            cls.STTNhomHT = "VI.";
                            break;

                        case 7:
                            cls.STTNhomHT = "VII.";
                            break;

                        case 8:
                            cls.STTNhomHT = "VIII.";
                            break;

                        case 9:
                            cls.STTNhomHT = "IX.";
                            break;

                        case 10:
                            cls.STTNhomHT = "X.";
                            break;

                        case 11:
                            cls.STTNhomHT = "XI.";
                            break;

                        case 12:
                            cls.STTNhomHT = "XII.";
                            break;
                    }
                    cls.SoQD = l.SoQD;
                    cls.TrongBH = Convert.ToInt16(l.TrongBH);
                    cls.Don_gia = Math.Round(l.DonGia, 3);
                    //cls.Don_gia_tt39 = Math.Round(l.DonGiaTT15, 3);
                    cls.SoTTqd = l.SoTTqd != null ? l.SoTTqd.ToString() : "";
                    cls.Ma_nhom = Convert.ToInt32(l.MaNhom5937);
                    cls.Tennhom = chkTieuNhom.Checked && DungChung.Bien.MaBV == "24012" && _mauso == 20 ? NhomTheoTT(l.TenNhomThuoc, l.TenNhomCT, l.TenRG, l.MaNhom5937, l.IdTieuNhom)[0, 0] : NhomTheoTT(l.TenNhomThuoc, l.TenNhomCT, l.TenRG, l.MaNhom5937)[0, 0];
                    cls.TenTN = Bien.MaBV == "24012" && _mauso == 20 && !chkTieuNhom.Checked ? (l.DongY == 1 ? "Thuốc Đông Y" : "Thuốc Tây Y") : "";
                    cls.TenHC = l.TenHC == null ? "" : l.TenHC;
                    cls.MaQD = l.MaQD == null ? "" : l.MaQD;
                    cls.QCPC = l.QCPC == null ? "" : l.QCPC;
                    //cls.NuocSX = l.NuocSX == null ? "" : l.NuocSX;
                    //cls.Hangsx = l.NhaSX == null ? "" : l.NhaSX;
                    //cls.GiaThau = l.GiaThau;
                    //cls.Matam = l.MaTam;
                    cls.Duong_dung = l.DuongDung == null ? "" : l.DuongDung;
                    cls.Ma_DuongD = l.MaDuongDung == null ? "" : l.MaDuongDung;
                    cls.Ten_thuoc = l.TenThuoc == null ? "" : l.TenThuoc;
                    cls.DangBC = l.DangBC == null ? "" : l.DangBC;
                    cls.Ham_luong = l.HamLuong == null ? "" : l.HamLuong;
                    cls.Don_vi_tinh = l.DonVi == null ? "" : l.DonVi;
                    cls.So_dang_ky = l.SoDK == null ? "" : l.SoDK;
                    cls.Ma_thuoc = l.MaDV.ToString();
                    cls.MaCC = l.MaCC == null ? "" : l.MaCC;
                    //cls.MaNhom5937 = l.MaNhom5937.Value;
                    cls.SoluongNT = l.SoLuongNT.ToString() != null ? Math.Round(Convert.ToDouble(l.SoLuongNT), 3) : 0;
                    cls.SoluongNgT = l.SoLuongNgT.ToString() != null ? Math.Round(Convert.ToDouble(l.SoLuongNgT), 3) : 0;
                    //cls.Thanhtien_noitru = Convert.ToDouble(l.Thanhtien_NoiTru);
                    //cls.Thanhtien_ngtru = Convert.ToDouble(l.Thanhtien_Ngtru);
                    cls.SoLuong139 = Convert.ToDouble(l.SoLuong139);
                    cls.SoLuongTE = Convert.ToDouble(l.SoLuongTE);
                    cls.SoLuongBHYT_DV = Convert.ToDouble(l.SoLuongBHYT_DV);
                    //cls.So_luong = Convert.ToDouble(l.SoLuong);
                    cls.TyLeTT = l.TyLeTT.ToString() != null ? Math.Round(Convert.ToDouble(l.TyLeTT), 3) : 0;
                    cls.Don_giaBHYT = l.Don_giaBHYT.ToString() != null ? Math.Round(Convert.ToDouble(l.Don_giaBHYT), 3) : 0;
                    //string tt = ((cls.SoluongNT + cls.SoluongNgT) * cls.Don_gia).ToString("N2");
                    cls.Thanh_tien = l.ThanhTien.ToString() != null ? Math.Round(l.ThanhTien, 2) : 0;////Convert.ToDouble(l.ThanhTien); // a Quý yc ngày 28062018
                    cls.Thanh_tien_BHYT = l.TienBHYTThanhToan.ToString() != null || l.TienBHYTThanhToan.ToString() != "" ? Math.Round(Convert.ToDouble(l.TienBHYTThanhToan), 3) : 0;
                    if (l.MaNhom5937 == 15)
                    {
                        cls.TenTieuNhom = ".Ngày giường điều trị nội trú";
                        cls.STTTieuNhom = 2;
                    }
                    else if (l.MaNhom5937 == 14)
                    {
                        if (l.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongNgoaiTru)
                        {
                            cls.TenTieuNhom = ".Ngày giường điều trị ban ngày";
                            cls.STTTieuNhom = 1;
                        }
                        else if (l.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.GiuongLuu)
                        {
                            cls.TenTieuNhom = ".Ngày giường lưu";
                            cls.STTTieuNhom = 3;
                        }
                    }
                    cls.Tenrg = l.TenRGG;
                    cls.SapXep = l.sapxep;
                    cls.TenSapXep = l.tensapxep;
                    cls.TyLeTT = Convert.ToDouble(l.TyLeTT);
                    //cls.TienBH = l.TienBH;
                    _list.Add(cls);
                }
                _list = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();

                #endregion đổ vào list cls19_20

                if (ckXuatXML.Checked)
                {
                    #region group để tính tổng tiền theo bệnh nhân

                    var q3 = (from lq in q6
                              group lq by new
                              {
                                  lq.BHTT,
                                  lq.MaKP,
                                  lq.MaBNhan,
                                  lq.TrongBH,
                                  lq.DonGia,
                                  lq.DonGiaTT15,
                                  lq.SoTTqd,
                                  lq.MaNhom5937,
                                  //lq.MaNhom5937,
                                  lq.TenNhom,
                                  lq.TenTN,
                                  lq.TenHC,
                                  lq.MaQD,
                                  lq.DuongD,
                                  lq.MaDuongDung,
                                  lq.QCPC,
                                  lq.TenDV,
                                  lq.MaCC,
                                  lq.HamLuong,
                                  lq.DonVi,
                                  lq.SoDK,
                                  lq.MaDV,
                                  lq.DangBC,
                                  lq.MaICD,
                                  lq.NuocSX,
                                  lq.NhaSX,
                                  lq.GiaThau,
                                  lq.MaTam,
                                  lq.TyLeTT,
                                  lq.TienBH,
                              } into kq
                              select new
                              {
                                  TyLeTT = kq.Key.TyLeTT,
                                  MaBNhan = kq.Key.MaBNhan,
                                  MaKP = kq.Key.MaKP,
                                  kq.Key.MaICD,
                                  TrongBH = kq.Key.TrongBH,
                                  DonGia = kq.Key.DonGia,
                                  DonGiaTT15 = kq.Key.DonGiaTT15,
                                  SoTTqd = kq.Key.SoTTqd,
                                  //IdNhom = kq.Key.IDNhom != null ? kq.Key.IDNhom.Value : 0,
                                  IdNhom = kq.Key.MaNhom5937 != null ? kq.Key.MaNhom5937.Value : 0,
                                  TenNhomThuoc = kq.Key.TenNhom,
                                  TenTNhom = kq.Key.TenTN,
                                  TenHC = kq.Key.TenHC,
                                  MaQD = kq.Key.MaQD,
                                  DuongDung = kq.Key.DuongD,
                                  kq.Key.DangBC,
                                  kq.Key.MaDuongDung,
                                  kq.Key.NuocSX,
                                  kq.Key.NhaSX,
                                  kq.Key.GiaThau,
                                  kq.Key.MaTam,
                                  TenThuoc = kq.Key.TenDV,
                                  HamLuong = kq.Key.HamLuong,
                                  DonVi = kq.Key.DonVi,
                                  SoDK = kq.Key.SoDK,
                                  MaDV = kq.Key.MaDV,
                                  MaCC = kq.Key.MaCC,
                                  kq.Key.QCPC,
                                  kq.Key.BHTT,// bảo hiểm thanh toán
                                  SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                                  SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                                  Thanhtien_NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.ThanhTien),
                                  Thanhtien_Ngtru = kq.Where(p => p.NoiTru == 0).Sum(p => p.ThanhTien),
                                  SoLuong = kq.Sum(p => p.SoLuong),
                                  ThanhTien = kq.Sum(p => p.ThanhTien),
                                  TienBH = kq.Key.TienBH,
                                  ThanhToanBHYTTong = kq.Sum(p => p.TienBH)
                              }).OrderBy(p => p.TenThuoc).ToList();

                    #endregion group để tính tổng tiền theo bệnh nhân

                    #region đổ vào list cls19_20

                    foreach (var l in q3)
                    {
                        cls19_20 cls = new cls19_20();
                        cls.Ma_lk = l.MaBNhan.ToString();
                        cls.Ma_khoa = l.MaKP;
                        cls.Ma_benh = l.MaICD;
                        cls.TrongBH = Convert.ToInt16(l.TrongBH);
                        cls.Don_gia = Convert.ToDouble(l.DonGia);
                        cls.Don_gia_tt39 = Convert.ToDouble(l.DonGiaTT15);//day la don gia tt15 trong Db. Ko muon doi ten bien nua. hic
                        cls.SoTTqd = l.SoTTqd != null ? l.SoTTqd.ToString() : "";
                        cls.Ma_nhom = Convert.ToInt32(l.IdNhom);
                        //cls.Ma_nhom = l.MaNhom5937;
                        cls.Tennhom = l.TenNhomThuoc != null ? l.TenNhomThuoc : "";
                        cls.TenTN = l.TenTNhom == null ? "" : l.TenTNhom;
                        cls.TenHC = l.TenHC == null ? "" : l.TenHC;
                        cls.MaQD = l.MaQD == null ? "" : l.MaQD;
                        cls.QCPC = l.QCPC == null ? "" : l.QCPC;
                        cls.NuocSX = l.NuocSX == null ? "" : l.NuocSX;
                        cls.Hangsx = l.NhaSX == null ? "" : l.NhaSX;
                        cls.GiaThau = l.GiaThau;
                        cls.Matam = l.MaTam;
                        cls.Duong_dung = l.DuongDung == null ? "" : l.DuongDung;
                        cls.Ma_DuongD = l.MaDuongDung == null ? "" : l.MaDuongDung;
                        cls.DangBC = l.DangBC == null ? "" : l.DangBC;
                        cls.Ten_thuoc = l.TenThuoc == null ? "" : l.TenThuoc;
                        cls.Ham_luong = l.HamLuong == null ? "" : l.HamLuong;
                        cls.Don_vi_tinh = l.DonVi == null ? "" : l.DonVi;
                        cls.So_dang_ky = l.SoDK == null ? "" : l.SoDK;
                        cls.Ma_thuoc = l.MaDV.ToString();
                        cls.MaCC = l.MaCC == null ? "" : l.MaCC;
                        cls.Tyle_tt = (l.BHTT == null || l.BHTT == 100) ? "" : l.BHTT.ToString();
                        cls.SoluongNT = Convert.ToDouble(l.SoLuongNT);
                        cls.SoluongNgT = Convert.ToDouble(l.SoLuongNgT);
                        cls.Thanhtien_noitru = Convert.ToDouble(l.Thanhtien_NoiTru);
                        cls.Thanhtien_ngtru = Convert.ToDouble(l.Thanhtien_Ngtru);
                        cls.So_luong = Convert.ToDouble(l.SoLuong);
                        cls.Thanh_tien = Convert.ToDouble(l.ThanhTien);
                        cls.TyLeTT = l.TyLeTT;
                        cls.TienBH = Convert.ToDouble(l.TienBH);
                        cls.Thanh_tien_BHYT = Convert.ToDouble(l.ThanhToanBHYTTong);
                        _listXML.Add(cls);
                    }
                    _listXML.OrderBy(p => p.Ma_lk);

                    #endregion đổ vào list cls19_20
                }

                #region in báo cáo

                if (_list.Count > 0)
                {
                    #region biểu 20

                    if (_mauso == 20)
                    {
                        if (ckDtuongCT.Checked)
                        {
                            BaoCao.repBcMau20_DTuongCT rep;
                            rep = new BaoCao.repBcMau20_DTuongCT(Bien.MaBV != "24012" ? chkTieuNhom.Checked : true);
                            if (lupDoituong.Text == "BHYT")
                            {
                                rep.TuNgayDenNgay.Value = theoquy();
                                tenbc = trongBH == 0 ? "THỐNG KÊ THUỐC THEO ĐỐI TƯỢNG THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THEO ĐỐI TƯỢNG THANH TOÁN BHYT";
                                rep.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ THUỐC THEO ĐỐI TƯỢNG THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THEO ĐỐI TƯỢNG THANH TOÁN BHYT";
                            }
                            else
                            {
                                rep.TuNgayDenNgay.Value = theoquy();
                                rep.paramTenBC.Value = "THỐNG KÊ THUỐC THANH TOÁN THEO ĐỐI TƯỢNG DỊCH VỤ";
                                tenbc = "THỐNG KÊ THUỐC THANH TOÁN THEO ĐỐI TƯỢNG DỊCH VỤ";
                            }

                            #region xuat Excel

                            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng" + lupDoituong.Text, "Số lượng DT139", "Số lượng TE", "Đơn giá", "Thành tiền" };
                            DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 13];
                            DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                            DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                            DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                            DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                            }

                            int num = 6;
                            foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.SoLuongBHYT_DV;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.SoLuong139;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.SoLuongTE;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.Don_gia;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.Thanh_tien;
                                num++;
                            }

                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu20.xls", true, this.Name);

                            #endregion xuat Excel

                            if (!string.IsNullOrEmpty(macc))
                                rep.NhaCC.Value = lupNhaCC.Text;
                            rep.DataSource = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();
                            rep.paraSLBHYT_DV.Value = lupDoituong.Text;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            if (rdInBC.SelectedIndex == 0)
                            {
                                if (btnVsip.Checked && DungChung.Bien.MaBV == "27194")
                                {
                                    BaoCao.repBcMau20_1399 rep;
                                    rep = new BaoCao.repBcMau20_1399(chkTieuNhom.Checked);
                                    if (lupDoituong.Text == "BHYT")
                                    {
                                        rep.TuNgayDenNgay.Value = theoquy();
                                        rep.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                        tenbc = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                    }
                                    else
                                    {
                                        rep.TuNgayDenNgay.Value = theoquy();
                                        rep.paramTenBC.Value = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                        tenbc = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                                    DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 12];
                                    DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                                    DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                                    DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                                    DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                                    }

                                    int num = 6;
                                    foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNT;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.Don_gia;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu20.xls", true, this.Name);

                                    #endregion xuat Excel

                                    if (!string.IsNullOrEmpty(macc))
                                        rep.NhaCC.Value = lupNhaCC.Text;
                                    rep.DataSource = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();

                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    BaoCao.repBcMau20_1399 rep;
                                    rep = new BaoCao.repBcMau20_1399(Bien.MaBV != "24012" ? chkTieuNhom.Checked : true);
                                    if (lupDoituong.Text == "BHYT")
                                    {
                                        rep.TuNgayDenNgay.Value = theoquy();
                                        rep.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                        tenbc = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                    }
                                    else
                                    {
                                        rep.TuNgayDenNgay.Value = theoquy();
                                        rep.paramTenBC.Value = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                        tenbc = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                                    DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 12];
                                    DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                                    DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                                    DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                                    DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                    {
                                        _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        _arrWidth = new int[] { };
                                        _tieude = new string[] { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Đơn giá đề nghị thanh toán BHYT", "Tỷ lệ thanh toán", "Thành tiền", "Tiền đề nghị quỹ BHYT thanh toán" };
                                        DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 16];
                                        DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                                        DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                                        DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                                        DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                                    }
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                                    }
                                    int num = 6;

                                    foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
                                    {
                                        if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.DangBC;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.Ham_luong;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.So_dang_ky;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.Don_vi_tinh;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNgT;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoluongNT;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.Don_gia;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.Don_giaBHYT;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.Tyle_tt;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.Thanh_tien;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.Thanh_tien_BHYT;
                                        }
                                        else
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                            //DungChung.Bien.MangHaiChieu[num, 4] = r.;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNT;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.Don_gia;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
                                        }

                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu20.xls", true, this.Name);

                                    #endregion xuat Excel

                                    if (!string.IsNullOrEmpty(macc))
                                        rep.NhaCC.Value = lupNhaCC.Text;
                                    rep.DataSource = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();

                                    rep.BindingData();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                BaoCao.repBcMau20_1399_N rep;
                                rep = new BaoCao.repBcMau20_1399_N(Bien.MaBV != "24012" ? chkTieuNhom.Checked : true);
                                if (lupDoituong.Text == "BHYT")
                                {
                                    rep.TuNgayDenNgay.Value = theoquy();
                                    rep.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                    tenbc = trongBH == 0 ? "THỐNG KÊ THUỐC THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ THUỐC THANH TOÁN BHYT";
                                }
                                else
                                {
                                    rep.TuNgayDenNgay.Value = theoquy();
                                    rep.paramTenBC.Value = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                    tenbc = "THỐNG KÊ THUỐC THANH TOÁN DỊCH VỤ";
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng, dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                                DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 12];
                                DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                                DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                                DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                                DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                                }

                                if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                {
                                    _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    _arrWidth = new int[] { };
                                    _tieude = new string[] { "STT", "STT theo DMT của BYT", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng", "Dạng bào chế", "Hàm lượng nồng độ", "Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Đơn giá đề nghị thanh toán BHYT", "Tỷ lệ thanh toán", "Thành tiền", "Tiền đề nghị quỹ BHYT thanh toán" };
                                    DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList().Count + 6, 16];
                                    DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 20/BHYT";
                                    DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                                    DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                                    DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                                    }
                                }

                                int num = 6;
                                foreach (var r in _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList())
                                {
                                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Ten_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.TenHC;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.DangBC;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Ham_luong;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.So_dang_ky;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.Don_vi_tinh;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNgT;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.SoluongNT;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Don_gia;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.TienBH;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.TyLeTT;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.Thanh_tien;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.Thanh_tien_BHYT;
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Duong_dung;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Ham_luong;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.So_dang_ky;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Don_vi_tinh;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SoluongNT;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.Don_gia;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
                                    }

                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu20.xls", true, this.Name);

                                #endregion xuat Excel

                                if (!string.IsNullOrEmpty(macc))
                                    rep.NhaCC.Value = lupNhaCC.Text;
                                rep.DataSource = _list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        if (chkXuatExel.Checked)
                        {
                            xuatExcel(_list.OrderBy(p => p.SapXep).ThenBy(p => p.Ten_thuoc).ToList(), txtDuongDan.Text, 20);
                        }
                    }

                    #endregion biểu 20

                    #region biểu 19 and 21

                    #region 19

                    else if (_mauso == 19)
                    {
                        BaoCao.repBcMau19_1399_N rep19;
                        rep19 = new BaoCao.repBcMau19_1399_N(chkTieuNhom.Checked);

                        if (DungChung.Bien.MaBV == "24012")
                        {
                            rep19.tble1.Text = "Kế toán trưởng";
                        }

                        if (lupDoituong.Text == "BHYT")
                        {
                            rep19.TuNgayDenNgay.Value = theoquy();
                            rep19.paramTenBC.Value = trongBH == 0 ? "THỐNG KÊ VTYT THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ VTYT THANH TOÁN BHYT";
                            tenbc = trongBH == 0 ? "THỐNG KÊ VTYT THANH TOÁN NGOÀI BHYT" : "THỐNG KÊ VTYT THANH TOÁN BHYT";
                        }
                        else
                        {
                            rep19.TuNgayDenNgay.Value = theoquy();
                            rep19.paramTenBC.Value = "THỐNG KÊ VTYT THANH TOÁN DỊCH VỤ";
                            tenbc = "THỐNG KÊ VTYT THANH TOÁN DỊCH VỤ";
                        }

                        #region xuat Excel

                        string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        string[] _tieude = { "STT", "Mã số theo danh mục do BYT ban hành", "Tên VTYT theo danh mục do BYT ban hành", "Tên thương mại", "Quy cách", "Đơn vị tính", "Giá mua vào(đồng)", "Số lượng nội trú", "Số lượng ngoại trú", "Giá thanh toán BHYT(đồng)", "Thành tiền" };

                        if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                        {
                            _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            _arrWidth = new int[] { };
                            _tieude = new string[] { "STT", "Mã số theo danh mục do BYT ban hành", "Tên VTYT theo danh mục do BYT ban hành", "Tên thương mại", "Quy cách", "Đơn vị tính", "Số lượng nội trú", "Số lượng ngoại trú", "Giá mua vào(đồng)", "Giá thanh toán BHYT(đồng)", "Tỷ lệ thanh toán", "Thành tiền", "Tiền đề nghị quỹ BHYT thanh toán(đồng)" };
                            DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 8, 13];
                            DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 19/BHYT";
                            DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                            DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                            DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                            }
                        }
                        else
                        {
                            _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            _arrWidth = new int[] { };
                            _tieude = new string[] { "STT", "Mã số theo danh mục do BYT ban hành", "Tên VTYT theo danh mục do BYT ban hành", "Tên thương mại", "Quy cách", "Đơn vị tính", "Giá mua vào(đồng)", "Số lượng nội trú", "Số lượng ngoại trú", "Giá thanh toán BHYT(đồng)", "Thành tiền" };
                            DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 6, 11];
                            DungChung.Bien.MangHaiChieu[1, 0] = "Tên CSYT:"; DungChung.Bien.MangHaiChieu[1, 1] = DungChung.Bien.TenCQ.ToUpper().ToString(); DungChung.Bien.MangHaiChieu[1, 4] = "Mẫu số 19/BHYT";
                            DungChung.Bien.MangHaiChieu[2, 0] = "Mã CSYT:"; DungChung.Bien.MangHaiChieu[2, 1] = DungChung.Bien.MaBV;
                            DungChung.Bien.MangHaiChieu[3, 2] = tenbc;
                            DungChung.Bien.MangHaiChieu[4, 2] = theoquy();
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[5, i] = _tieude[i];
                            }
                        }

                        int num = 6;
                        if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                        {
                            num = 8;
                        }
                        foreach (var r in _list.OrderBy(p => p.Ten_thuoc).ToList())
                        {
                            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.QCPC;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.Don_vi_tinh;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.SoluongNT;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.SoluongNgT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.Don_gia;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.Don_giaBHYT;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.TyLeTT;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.Thanh_tien;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.Thanh_tien_BHYT;
                            }
                            else
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = r.SoTTqd;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenHC;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.Ten_thuoc;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.QCPC;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.Don_vi_tinh;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.Don_gia;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.SoluongNT;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.SoluongNgT;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.Don_gia;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.Thanh_tien;
                            }

                            num++;
                        }

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu19.xls", true, this.Name);

                        #endregion xuat Excel

                        if (!string.IsNullOrEmpty(macc))
                            rep19.NhaCC.Value = lupNhaCC.Text;
                        rep19.DataSource = _list.OrderBy(p => p.Ten_thuoc);
                        rep19.BindingData();
                        rep19.CreateDocument();
                        frm.prcIN.PrintingSystem = rep19.PrintingSystem;
                        frm.ShowDialog();
                        if (chkXuatExel.Checked)
                        {
                            xuatExcel(_list, txtDuongDan.Text, 19);
                        }
                    }

                    #endregion 19

                    #region 21

                    else // mau 21
                    {
                        #region mẫu thường

                        if (ck_CV285_20001.Checked == false)
                        {
                            #region bv 30003

                            if (DungChung.Bien.MaBV == "30003")
                            {
                                BaoCao.repBcMau21BHYT_1399_30003 rep21;
                                rep21 = new BaoCao.repBcMau21BHYT_1399_30003();
                                rep21.DTuong.Value = lupDoituong.Text;
                                rep21.TuNgayDenNgay.Value = theoquy();
                                rep21.DataSource = _list.OrderBy(p => p.Ten_thuoc);
                                rep21.BindingData();
                                rep21.CreateDocument();
                                tenbc = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN " + lupDoituong.Text.ToUpper();

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "STT", "Mã số theo danh mục do BYT", "Tên dịch vụ y tế", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                                DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 1, 7];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in _list.OrderBy(p => p.Ten_thuoc).ToList())
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    if (DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "04011")
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                    else
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoTTqd;

                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Ten_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.SoluongNgT;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.SoluongNT;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.Don_gia;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.Thanh_tien;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu21.xls", true, this.Name);

                                #endregion xuat Excel

                                frm.prcIN.PrintingSystem = rep21.PrintingSystem;
                                frm.ShowDialog();
                                if (chkXuatExel.Checked)
                                {
                                    xuatExcel(_list, txtDuongDan.Text, 21);
                                }
                            }

                            #endregion bv 30003

                            else
                            {
                                BaoCao.repBcMau21BHYT_1399 rep21;
                                rep21 = new BaoCao.repBcMau21BHYT_1399();
                                rep21.DTuong.Value = lupDoituong.Text;
                                rep21.TuNgayDenNgay.Value = theoquy();
                                rep21.DataSource = _list.OrderBy(p => p.Ten_thuoc);
                                rep21.BindingData();
                                rep21.CreateDocument();
                                tenbc = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN " + lupDoituong.Text.ToUpper();

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "STT", "Mã số theo danh mục do BYT", "Tên dịch vụ y tế", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền" };
                                DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 1, 7];
                                if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                {
                                    _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    _arrWidth = new int[] { };
                                    _tieude = new string[] { "STT", "Mã số theo danh mục do BYT", "Tên dịch vụ y tế", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Đơn giá đề nghĩ quỹ BHYT thanh toán", "Tỷ lệ thanh toán", "Thành tiền", "Tiền đề nghị quỹ BHYT thanh toán" };
                                    DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 3, 10];
                                }
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                {
                                    num = 3;
                                }
                                foreach (var r in _list.OrderBy(p => p.Ten_thuoc).ToList())
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    if (DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "04011")
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                    else
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoTTqd;

                                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Ten_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SoluongNgT;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoluongNT;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Don_gia;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Don_giaBHYT;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.TyLeTT;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.Thanh_tien;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.Thanh_tien_BHYT;
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Ten_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SoluongNgT;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoluongNT;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Don_gia;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Thanh_tien;
                                    }

                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu21.xls", true, this.Name);

                                #endregion xuat Excel

                                frm.prcIN.PrintingSystem = rep21.PrintingSystem;
                                frm.ShowDialog();
                                if (chkXuatExel.Checked)
                                {
                                    xuatExcel(_list, txtDuongDan.Text, 21);
                                }
                            }
                        }

                        #endregion mẫu thường

                        #region mẫu CV96

                        else
                        {
                            if (ck_CV285_20001.Checked == true)
                            {
                                double tienbh = Math.Round(_list.Sum(p => p.Thanh_tien_BHYT), 0);

                                BaoCao.repBcMau21BHYT_CV96 rep21;
                                rep21 = new BaoCao.repBcMau21BHYT_CV96();
                                rep21.DTuong.Value = lupDoituong.Text;
                                rep21.TuNgayDenNgay.Value = theoquy();
                                rep21.xrLabel4.Text = "Tổng số tiền dịch vụ kỹ thuật đề nghị thanh toán (viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tienbh, " đồng.");
                                rep21.DataSource = _list.OrderBy(p => p.Ten_thuoc);
                                rep21.BindingData();
                                rep21.CreateDocument();
                                tenbc = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN " + lupDoituong.Text.ToUpper();

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "STT", "Mã số theo danh mục do BYT", "Tên dịch vụ y tế", "Tỷ lệ thanh toán", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá", "Thành tiền", "Tiền đề nghị BH thanh toán" };
                                DungChung.Bien.MangHaiChieu = new Object[_list.OrderBy(p => p.Ten_thuoc).ToList().Count + 1, 9];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in _list.OrderBy(p => p.Ten_thuoc).ToList())
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    if (DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "04011")
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaQD;
                                    else
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.SoTTqd;

                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Ten_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.TyLeTT;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.SoluongNgT;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.SoluongNT;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.Don_gia;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.Thanh_tien;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.Thanh_tien_BHYT;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, tenbc, "C:\\Biểu21.xls", true, this.Name);

                                #endregion xuat Excel

                                frm.prcIN.PrintingSystem = rep21.PrintingSystem;
                                frm.ShowDialog();
                                if (chkXuatExel.Checked)
                                {
                                    xuatExcel(_list, txtDuongDan.Text, 21);
                                }
                            }
                        }

                        #endregion mẫu CV96
                    }

                    #endregion 21

                    #endregion biểu 19 and 21
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !");
                }

                #endregion in báo cáo

                #region xuất XML

                if (ckXuatXML.Checked)
                {
                    if (XuatXML(_listXML, txtFileXMLPath.Text, _mauso))
                        MessageBox.Show("Xuất file XML thành công");
                    else
                        MessageBox.Show("Xuất file XML lỗi");
                }

                #endregion xuất XML
            }
            _lDSMaDV = new List<int>();
        }

        private class _ltranfer
        {
            public string Mabn { get; set; }
        }

        #region xuất excel

        /// <summary>
        /// xuất excel biểu 19, 20
        /// </summary>
        /// <param name="_lKetQua"></param> List<cls19_20>
        /// <param name="duongdan"></param>
        /// <param name="mauso"></param> 19: mẫu 19, 20: Mẫu 20
        private void xuatExcel(List<cls19_20> _lKetQua, string duongdan, int mauso)
        {
            #region mẫu 20

            if (mauso == 20) // Xuất excel biểu 20
            {
                if (ckBV30007.Checked)
                {
                    #region bv tu ky, áp dụng hải dương từ ngày 22/08/2016

                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu20";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] {"stt","ma_thuoc","Ma_dmbyt" ,"hoat_chat","ma_duong_dung", "duong_dung","cach_dung","ham_luong","ten_thuoc","SĐK","qui cach dong goi","ĐVT",
                        "So luong ngoai tru", "So luong noi tru", "don gia","thanh tien", "ma_cskcb"};
                    foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        // xuất theo cv1399
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = i - 1;
                        r1.Columns.AutoFit();

                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.Ma_thuoc;
                        r2.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value = a.MaQD;
                        r3.EntireColumn.ColumnWidth = 40;

                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value = a.TenHC;
                        r4.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value = a.Ma_DuongD;
                        r5.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value = a.Duong_dung;
                        r6.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value = "";// cách dung chưa có
                        r7.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.Ham_luong;
                        r8.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        r9.Value = a.Ten_thuoc;
                        r9.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        r10.NumberFormat = "@";
                        r10.Value = a.So_dang_ky;
                        r10.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        r11.NumberFormat = "@";
                        r11.Value = a.QCPC;
                        r11.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                        r12.NumberFormat = "@";
                        r12.Value = a.Don_vi_tinh;
                        r12.EntireColumn.ColumnWidth = 15;

                        //
                        ////thêm cột cho bv tứ kỳ
                        COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                        //r13.NumberFormat = "@";
                        r13.Value = Math.Round(a.SoluongNgT, 2);
                        r13.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                        //r14.NumberFormat = "@";
                        r14.Value = Math.Round(a.SoluongNT, 2);
                        r14.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                        //r15.NumberFormat = "@";
                        r15.Value = a.Don_gia;
                        r15.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                        //r16.NumberFormat = "@";
                        r16.Value = a.Thanh_tien;
                        r16.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                        r17.NumberFormat = "@";
                        r17.Value = a.Ma_CSKCB;
                        r17.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                        MessageBox.Show("Xuất file thành công");
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }

                    #endregion bv tu ky, áp dụng hải dương từ ngày 22/08/2016
                }
                else if (btnVsip.Checked)
                {
                    #region bieu20Vsip

                    //COMExcel.Application exApp = new COMExcel.Application();
                    //COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                    //          COMExcel.XlWBATemplate.xlWBATWorksheet);
                    //COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    //exSheet.Name = "Bieu20";
                    //int k = 0;
                    //int i = 1;
                    //string[] arr = new string[] {"STT", "Mã Thuốc", "Tên hoạt chất", "Tên thuốc thành phẩm", "Đường dùng", "Dạng bào chế", "Hàm lượng/Nồng độ",
                    //"Số đăng ký hoặc số GPNK", "Đơn vị tính", "Số lượng ngoại trú", "Số lượng nội trú", "Đơn giá bệnh viện (đồng)", "Giá thanh toán BHYT(đồng)", "Tỷ lệ thanh toán", "Thành tiền(đồng)", "Tiền đề nghị quỹ BHYT thanh toán(đồng)"};

                    //COMExcel.Range rr = (COMExcel.Range)exSheet.Cells[1, 2];
                    //rr.Value2 = DungChung.Bien.TenCQCQ.ToUpper();
                    //rr.Cells.Font.Bold = true;
                    //rr.Cells.Font.Color = System.Drawing.Color.Red;
                    //COMExcel.Range rr1 = (COMExcel.Range)exSheet.Cells[2, 2];
                    //rr1.Value2 = DungChung.Bien.TenCQ.ToUpper();
                    //rr1.Cells.Font.Bold = true;
                    //rr.Cells.Font.Color = System.Drawing.Color.Blue;
                    //COMExcel.Range rr2 = (COMExcel.Range)exSheet.Cells[3, 4];
                    //rr2.Value2 = "THỐNG KÊ DỊCH VỤ THANH TOÁN BHYT";
                    //rr2.Cells.Font.Bold = true;
                    //rr2.Cells.Font.Size = 30;
                    //COMExcel.Range rr3 = (COMExcel.Range)exSheet.Cells[4, 5];
                    //rr3.Value2 = theoquy();
                    //foreach (var b in arr)
                    //{
                    //    k++;
                    //    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[5, k];
                    //    r.Value2 = b.ToString();
                    //    r.Columns.AutoFit();
                    //    r.Cells.Font.Bold = true;
                    //    Microsoft.Office.Interop.Excel.Borders border = r.Borders;
                    //    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //}
                    //foreach (var a in _lKetQua)
                    //{
                    //    i++;
                    //    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                    //    r1.Value2 = i - 1;
                    //    r1.Columns.AutoFit();
                    //    r1.EntireColumn.ColumnWidth = 3;
                    //    Microsoft.Office.Interop.Excel.Borders border = r1.Borders;
                    //    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                    //    r2.NumberFormat = "@";
                    //    r2.Value2 = a.MaQD;
                    //    r2.EntireColumn.ColumnWidth = 9;
                    //    Microsoft.Office.Interop.Excel.Borders border1 = r2.Borders;
                    //    border1.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border1.Weight = 2d;
                    //    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                    //    r3.NumberFormat = "@";
                    //    r3.Value = a.TenHC;
                    //    r3.EntireColumn.ColumnWidth = 12;
                    //    Microsoft.Office.Interop.Excel.Borders border2 = r3.Borders;
                    //    border2.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border2.Weight = 2d;
                    //    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                    //    r4.NumberFormat = "@";
                    //    r4.Value = a.Ten_thuoc;
                    //    r4.EntireColumn.ColumnWidth = 20;
                    //    Microsoft.Office.Interop.Excel.Borders border3 = r4.Borders;
                    //    border3.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border3.Weight = 2d;
                    //    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                    //    r5.NumberFormat = "@";
                    //    r5.Value = a.Ten_thuoc;
                    //    r5.EntireColumn.ColumnWidth = 12;
                    //    Microsoft.Office.Interop.Excel.Borders border4 = r5.Borders;
                    //    border4.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border4.Weight = 2d;
                    //    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                    //    r6.NumberFormat = "@";
                    //    r6.Value = a.Ma_DuongD; //a.Duong_dung;
                    //    r6.EntireColumn.ColumnWidth = 12;
                    //    Microsoft.Office.Interop.Excel.Borders border5 = r6.Borders;
                    //    border5.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border5.Weight = 2d;
                    //    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                    //    r7.NumberFormat = "@";
                    //    r7.Value = a.Ham_luong;
                    //    r7.EntireColumn.ColumnWidth = 16;
                    //    Microsoft.Office.Interop.Excel.Borders border6 = r7.Borders;
                    //    border6.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border6.Weight = 2d;
                    //    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                    //    r8.NumberFormat = "@";
                    //    r8.Value = a.So_dang_ky;
                    //    r8.EntireColumn.ColumnWidth = 22;
                    //    Microsoft.Office.Interop.Excel.Borders border7 = r8.Borders;
                    //    border7.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border7.Weight = 2d;
                    //    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                    //    r9.NumberFormat = "@";
                    //    r9.Value = a.Don_vi_tinh;
                    //    r9.EntireColumn.ColumnWidth = 11;
                    //    Microsoft.Office.Interop.Excel.Borders border8 = r9.Borders;
                    //    border8.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border8.Weight = 2d;
                    //    COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                    //    //r9.NumberFormat = "@";
                    //    r10.Value = a.SoluongNT;
                    //    r10.EntireColumn.ColumnWidth = 17;
                    //    Microsoft.Office.Interop.Excel.Borders border9 = r10.Borders;
                    //    border9.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border9.Weight = 2d;
                    //    COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                    //    //r10.NumberFormat = "@";
                    //    r11.Value = a.SoluongNgT;
                    //    r11.EntireColumn.ColumnWidth = 13;
                    //    Microsoft.Office.Interop.Excel.Borders border10 = r11.Borders;
                    //    border10.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border10.Weight = 2d;
                    //    COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                    //    r12.Value = a.Don_gia;
                    //    r12.EntireColumn.ColumnWidth = 24;
                    //    Microsoft.Office.Interop.Excel.Borders border11 = r12.Borders;
                    //    border11.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border11.Weight = 2d;
                    //    COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                    //    //r13.NumberFormat = "@";
                    //    r13.Value = a.Don_gia;
                    //    r13.EntireColumn.ColumnWidth = 24;
                    //    Microsoft.Office.Interop.Excel.Borders border12 = r13.Borders;
                    //    border12.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border12.Weight = 2d;
                    //    COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                    //    //r13.NumberFormat = "@";
                    //    r14.Value = a.TyLeTT;
                    //    r14.EntireColumn.ColumnWidth = 15;
                    //    Microsoft.Office.Interop.Excel.Borders border13 = r14.Borders;
                    //    border13.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border13.Weight = 2d;
                    //    COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                    //    //r13.NumberFormat = "@";
                    //    r15.Value = a.Thanh_tien;
                    //    r15.EntireColumn.ColumnWidth = 15;
                    //    Microsoft.Office.Interop.Excel.Borders border14 = r15.Borders;
                    //    border14.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border14.Weight = 2d;
                    //    COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                    //    //r13.NumberFormat = "@";
                    //    r16.Value = a.Thanh_tien;
                    //    r16.EntireColumn.ColumnWidth = 36;
                    //    Microsoft.Office.Interop.Excel.Borders border15 = r16.Borders;
                    //    border15.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //    border15.Weight = 2d;
                    //}
                    //exApp.Visible = true;
                    //try
                    //{
                    //    exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                    //                    null, null, false, false,
                    //                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                    //                    false, false, false, false, false);
                    //    MessageBox.Show("Xuất file thành công");
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                    //finally
                    //{
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    //}
                }

                #endregion bieu20Vsip

                else if (chk917.Checked)
                {
                    #region QĐ 917

                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu20";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] {"STT", "MA_THUOC", "TEN_HOATCHAT", "TEN_THUOC", "DUONG_DUNG", "HAM_LUONG", "SO_DKY",
                    "DON_VI", "SL_NOITRU", "SL_NGOAITRU", "DON_GIA", "THANH_TIEN"};
                    foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = i - 1;
                        r1.Columns.AutoFit();
                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.MaQD;
                        r2.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value = a.TenHC;
                        r3.EntireColumn.ColumnWidth = 40;
                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value = a.Ten_thuoc;
                        r4.EntireColumn.ColumnWidth = 30;
                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value = a.Ma_DuongD; //a.Duong_dung;
                        r5.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value = a.Ham_luong;
                        r6.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value = a.So_dang_ky;
                        r7.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.Don_vi_tinh;
                        r8.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        //r9.NumberFormat = "@";
                        r9.Value = a.SoluongNT;
                        r9.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        //r10.NumberFormat = "@";
                        r10.Value = a.SoluongNgT;
                        r10.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        r11.Value = a.Don_gia;
                        r11.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                        //r13.NumberFormat = "@";
                        r12.Value = a.Thanh_tien;
                        r12.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                        MessageBox.Show("Xuất file thành công");
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }

                    #endregion QĐ 917
                }
                else
                {
                    #region bv khác

                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu20";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] {"STT", "ten_thuoc", "hamluong", "sodangky", "donvitinh", "soluongngt", "slnoitru", "dongia",
                    "thanhtien", "ma_thuoc", "mabv", "loaikcb", "stt_dmbyt", "hoat_chat" };
                    foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = a.SoQD;
                        r1.Columns.AutoFit();
                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.Ten_thuoc;
                        r2.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value = a.Ham_luong;
                        r3.EntireColumn.ColumnWidth = 40;
                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value = a.So_dang_ky;
                        r4.EntireColumn.ColumnWidth = 30;
                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value = a.Don_vi_tinh;
                        r5.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value = Math.Round(a.SoluongNgT, 2);
                        r6.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value = Math.Round(a.SoluongNT, 2);
                        r7.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.Don_gia;
                        r8.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        r9.Value = a.Thanh_tien;
                        r9.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        r10.NumberFormat = "@";
                        r10.Value = a.MaQD;
                        r10.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        r11.NumberFormat = "@";
                        r11.Value = DungChung.Bien.MaBV;
                        r11.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                        r12.NumberFormat = "@";
                        if (radNoiTru.SelectedIndex == 0)
                        {
                            r12.Value = "NGOAI";
                        }
                        else
                        {
                            if (radNoiTru.SelectedIndex == 1)
                                r12.Value = "NOI";
                            else
                                r12.Value = "";
                        }
                        r12.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                        r13.Value = a.SoTTqd;
                        r13.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                        r14.NumberFormat = "@";
                        r14.Value = a.TenHC;
                        r14.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                        MessageBox.Show("Xuất file thành công");
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }
                }

                #endregion bv khác
            }

            #endregion mẫu 20

            #region Xuất excel biểu 19

            else if (mauso == 19)
            {
                #region bệnh viện tứ kỳ

                if (ckBV30007.Checked)
                {
                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu19";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] { "stt", "ma_vtyt_BV", "Stt_TTBYT", "Ma_ dmbyt", "ten_vtyt", "qui cach", "nuoc sx", "hang sx", "gia thau", "dvt", "So luong ngoai tru", "So luong noi tru", "Don gia", "Thanh tien", "ma cskcb" };
                    foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        // in theo công văn 1399
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = i - 1; //i - 1;
                        r1.Columns.AutoFit();

                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.Ma_thuoc;

                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value2 = a.SoTTqd;
                        r3.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value = a.MaQD;
                        r4.EntireColumn.ColumnWidth = 40;

                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value = a.Ten_thuoc;
                        r5.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value = a.QCPC;
                        r6.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value = a.NuocSX;
                        r7.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.Hangsx;
                        r8.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        r9.Value = a.GiaThau;
                        r9.EntireColumn.ColumnWidth = 12;

                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        r10.NumberFormat = "@";
                        r10.Value = a.Don_vi_tinh;
                        r10.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        //r11.NumberFormat = "@";
                        r11.Value = Math.Round(a.SoluongNgT, 2);
                        r11.EntireColumn.ColumnWidth = 15;

                        //
                        // các cột thêm cho bệnh viện Tứ kỳ
                        //
                        COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12]; // mã vật tư y tế sử dụng trong BV
                        //r12.NumberFormat = "@";
                        r12.Value = Math.Round(a.SoluongNT, 2);
                        r12.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13]; // Nước sản xuất
                        r13.NumberFormat = "@";
                        r13.Value = a.Don_gia;
                        r13.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14]; // nhà cung cấp
                        r14.NumberFormat = "@";
                        r14.Value = a.Thanh_tien;
                        r14.EntireColumn.ColumnWidth = 30;

                        COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15]; // Giá thầu
                        r15.NumberFormat = "@";
                        r15.Value = a.Ma_CSKCB;
                        r15.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }
                }

                #endregion bệnh viện tứ kỳ

                else if (chk917.Checked)
                {
                    #region QĐ 917

                    {
                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Name = "Bieu19";
                        int k = 0;
                        int i = 1;
                        string[] arr = new string[] {"STT","MA_VTYT","TEN_VTYT", "TEN_THUONGMAI", "QUY_CACH",
                    "DON_VI","GIA_MUA", "SL_NOITRU", "SL_NGOAITRU", "GIA_THANHTOAN", "THANH_TIEN" };
                        foreach (var b in arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                            r.Cells.Font.Bold = true;
                        }
                        foreach (var a in _lKetQua)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();
                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.NumberFormat = "@";
                            r2.Value2 = a.MaQD;
                            r2.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            r3.NumberFormat = "@";
                            r3.Value = DungChung.Bien.MaBV == "30303" ? a.TenHC : a.Ten_thuoc;
                            r3.EntireColumn.ColumnWidth = 40;
                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            r4.NumberFormat = "@";
                            r4.Value = DungChung.Bien.MaBV == "30303" ? a.Ten_thuoc : a.TenHC;
                            r4.EntireColumn.ColumnWidth = 30;
                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            r5.NumberFormat = "@";
                            r5.Value = a.QCPC;
                            r5.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            r6.NumberFormat = "@";
                            r6.Value = a.Don_vi_tinh;
                            r6.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            r7.NumberFormat = "@";
                            r7.Value = a.GiaThau;
                            r7.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                            r8.Value = a.SoluongNT;
                            r8.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                            r9.Value = a.SoluongNgT;
                            r9.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                            r10.Value = a.Don_gia;
                            r10.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                            r11.Value = a.Thanh_tien;
                            r11.EntireColumn.ColumnWidth = 15;
                        }
                        exApp.Visible = true;
                        try
                        {
                            exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }
                    }

                    #endregion QĐ 917
                }

                #region bệnh viện khác

                else
                {
                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu19";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] {"STT","ten_byt","ten_vtyt", "soluong", "dongia",
                    "thanhtien","ma_dvkt", "mabv", "loaikcb", "stt_dmbyt", "ten_dmbhyt" };
                    foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = a.SoQD;
                        r1.Columns.AutoFit();
                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.TenHC;
                        r2.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value = a.Ten_thuoc;
                        r3.EntireColumn.ColumnWidth = 40;
                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value = Math.Round(a.SoluongNgT + a.SoluongNT, 2);
                        r4.EntireColumn.ColumnWidth = 30;
                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value = Math.Round(a.Don_gia, 2);
                        r5.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value = Math.Round(a.Thanh_tien, 2);
                        r6.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value = DungChung.Bien.MaBV;
                        r7.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.MaQD;
                        r8.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        if (radNoiTru.SelectedIndex == 0)
                        {
                            r9.Value = "NGOAI";
                        }
                        else
                        {
                            if (radNoiTru.SelectedIndex == 1)
                                r9.Value = "NOI";
                            else
                                r9.Value = "";
                        }
                        r9.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        r10.NumberFormat = "@";
                        r10.Value = a.SoTTqd;
                        r10.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        r11.NumberFormat = "@";
                        r11.Value = a.TenHC;
                        r11.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }
                }

                #endregion bệnh viện khác
            }

            #endregion Xuất excel biểu 19

            #region Xuất excel biểu 21

            else if (mauso == 21)
            {
                if (ckBV30007.Checked)
                {
                    #region xuất theo bệnh viện 30007

                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    exSheet.Name = "Bieu21";
                    int k = 0;
                    int i = 1;
                    string[] arr = new string[] { "STT", "ten_dvkt", "ma_dvkt", "soluong_ngtru", "soluong_noitru", "dongia", "thanhtien", "ma_cskcb" }; foreach (var b in arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                        r.Cells.Font.Bold = true;
                    }
                    foreach (var a in _lKetQua)
                    {
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = i - 1;
                        r1.Columns.AutoFit();
                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        r2.Value2 = a.Ten_thuoc;
                        r2.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value = a.MaQD;
                        r3.EntireColumn.ColumnWidth = 40;
                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        //r4.NumberFormat = "@";
                        r4.Value = Math.Round(a.SoluongNgT, 2);
                        r4.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        //r5.NumberFormat = "@";
                        r5.Value = Math.Round(a.SoluongNT, 2);
                        r5.EntireColumn.ColumnWidth = 12;
                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        //r6.NumberFormat = "@";
                        r6.Value = a.Don_gia;
                        r6.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        //r7.NumberFormat = "@";
                        r7.Value = a.Thanh_tien;
                        r7.EntireColumn.ColumnWidth = 15;

                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value = a.Ma_CSKCB;
                        r8.EntireColumn.ColumnWidth = 15;
                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        r9.Value = a.Ma_thuoc;
                        r9.EntireColumn.ColumnWidth = 15;
                    }
                    exApp.Visible = true;
                    try
                    {
                        exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                        null, null, false, false,
                                        COMExcel.XlSaveAsAccessMode.xlExclusive,
                                        false, false, false, false, false);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    }

                    #endregion xuất theo bệnh viện 30007
                }
                else if (chk917.Checked)
                {
                    if (ck_CV285_20001.Checked == false)
                    {
                        #region 917

                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Name = "Bieu21";
                        int k = 0;
                        int i = 1;
                        string[] arr = new string[] { "STT", "MA_DVKT", "TEN_DVKT", "SL_NOITRU", "SL_NGOAITRU", "DON_GIA", "THANH_TIEN" }; foreach (var b in arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                            r.Cells.Font.Bold = true;
                        }
                        foreach (var a in _lKetQua)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();
                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.NumberFormat = "@";
                            r2.Value2 = a.MaQD;
                            r2.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            r3.NumberFormat = "@";
                            r3.Value = a.Ten_thuoc;
                            r3.EntireColumn.ColumnWidth = 40;
                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            //r4.NumberFormat = "@";
                            r4.Value = a.SoluongNT;
                            r4.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            //r5.NumberFormat = "@";
                            r5.Value = a.SoluongNgT;
                            r5.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            //r6.NumberFormat = "@";
                            r6.Value = a.Don_gia;
                            r6.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            //r7.NumberFormat = "@";
                            r7.Value = a.Thanh_tien;
                            r7.EntireColumn.ColumnWidth = 15;
                        }
                        exApp.Visible = true;
                        try
                        {
                            exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }

                        #endregion 917
                    }
                    else
                    {
                        #region CV96

                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Name = "Bieu21";
                        int k = 0;
                        int i = 1;
                        string[] arr = new string[] { "STT", "MA_DVKT", "TEN_DVKT", "TyLe_TT", "SL_NOITRU", "SL_NGOAITRU", "DON_GIA", "THANH_TIEN", "TienBH" }; foreach (var b in arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                            r.Cells.Font.Bold = true;
                        }
                        foreach (var a in _lKetQua)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();
                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.NumberFormat = "@";
                            r2.Value2 = a.MaQD;
                            r2.EntireColumn.ColumnWidth = 12;

                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            r3.NumberFormat = "@";
                            r3.Value = a.Ten_thuoc;
                            r3.EntireColumn.ColumnWidth = 40;

                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            r4.NumberFormat = "@";
                            r4.Value = a.TyLeTT;
                            r4.EntireColumn.ColumnWidth = 40;

                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            //r4.NumberFormat = "@";
                            r5.Value = a.SoluongNT;
                            r5.EntireColumn.ColumnWidth = 15;

                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            //r5.NumberFormat = "@";
                            r6.Value = a.SoluongNgT;
                            r6.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            //r6.NumberFormat = "@";
                            r7.Value = a.Don_gia;
                            r7.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                            //r7.NumberFormat = "@";
                            r8.Value = a.Thanh_tien;
                            r8.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                            //r7.NumberFormat = "@";
                            r9.Value = a.TienBH;
                            r9.EntireColumn.ColumnWidth = 15;
                        }
                        exApp.Visible = true;
                        try
                        {
                            exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }

                        #endregion CV96
                    }
                }
                else if (btnVsip.Checked)
                {
                    #region xuất theo bệnh viện 27194

                    //COMExcel.Application exApp = new COMExcel.Application();
                    //COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                    //          COMExcel.XlWBATemplate.xlWBATWorksheet);
                    //COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    //exSheet.Name = "Bieu21";
                    //int k = 0;
                    //int i = 1;
                    //string[] arr = new string[] { "STT", "Mã số theo danh mục BYT", "Tên dịch vụ kỹ thuật", "Số lượng Ngoại trú", "Số lượng Nội trú", "Đơn giá bệnh viện", "Đơn giá đề nghị ký quỹ BHYTTT", "Tỷ lệ thanh toán" }; foreach (var b in arr)
                    //{
                    //    k++;
                    //    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                    //    r.Value2 = b.ToString();
                    //    r.Columns.AutoFit();
                    //    r.Cells.Font.Bold = true;
                    //}
                    //foreach (var a in _lKetQua)
                    //{
                    //    i++;
                    //    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                    //    r1.Value2 = i - 1;
                    //    r1.Columns.AutoFit();
                    //    r1.EntireColumn.ColumnWidth = 3;
                    //    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                    //    r2.NumberFormat = "@";
                    //    r2.Value2 = a.MaQD;
                    //    r2.EntireColumn.ColumnWidth = 25;
                    //    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                    //    r3.NumberFormat = "@";
                    //    r3.Value = a.Ten_thuoc;
                    //    r3.EntireColumn.ColumnWidth = 23;
                    //    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                    //    //r4.NumberFormat = "@";
                    //    r4.Value = Math.Round(a.SoluongNgT, 2);
                    //    r4.EntireColumn.ColumnWidth = 17;
                    //    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                    //    //r5.NumberFormat = "@";
                    //    r5.Value = Math.Round(a.SoluongNT, 2);
                    //    r5.EntireColumn.ColumnWidth = 17;
                    //    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                    //    //r6.NumberFormat = "@";
                    //    r6.Value = a.Don_gia;
                    //    r6.EntireColumn.ColumnWidth = 17;
                    //    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                    //    //r7.NumberFormat = "@";
                    //    r7.Value = a.Don_gia_tt39;
                    //    r7.EntireColumn.ColumnWidth = 10;

                    //    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                    //    r8.NumberFormat = "@";
                    //    r8.Value = a.TyLeTT;
                    //    r8.EntireColumn.ColumnWidth = 15;
                    //}
                    //exApp.Visible = true;
                    //try
                    //{
                    //    exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                    //                    null, null, false, false,
                    //                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                    //                    false, false, false, false, false);
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                    //finally
                    //{
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                    //}

                    #endregion xuất theo bệnh viện 27194
                }
                else
                {
                    if (DungChung.Bien.MaBV == "24009")
                    {
                        #region xuất theo bệnh viện 24009

                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Name = "Bieu21";
                        int k = 0;
                        int i = 1;
                        string[] arr = new string[] { "STT", "ten_dvkt", "soluong_ngtru", "soluong_noitru", "dongia", "thanhtien", "ma_dvkt", "ma_cskcb", "loaikcb" }; foreach (var b in arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                            r.Cells.Font.Bold = true;
                        }
                        foreach (var a in _lKetQua)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();

                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.NumberFormat = "@";
                            r2.Value2 = a.Ten_thuoc;
                            r2.EntireColumn.ColumnWidth = 12;

                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            //r4.NumberFormat = "@";
                            r3.Value = Math.Round(a.SoluongNgT, 2);
                            r3.EntireColumn.ColumnWidth = 15;

                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            //r5.NumberFormat = "@";
                            r4.Value = Math.Round(a.SoluongNT, 2);
                            r4.EntireColumn.ColumnWidth = 12;

                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            //r6.NumberFormat = "@";
                            r5.Value = a.Don_gia;
                            r5.EntireColumn.ColumnWidth = 15;

                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            //r7.NumberFormat = "@";
                            r6.Value = a.Thanh_tien;
                            r6.EntireColumn.ColumnWidth = 15;

                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            r7.NumberFormat = "@";
                            r7.Value = a.MaQD;
                            r7.EntireColumn.ColumnWidth = 40;

                            COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                            r8.NumberFormat = "@";
                            r8.Value = a.Ma_CSKCB;
                            r8.EntireColumn.ColumnWidth = 15;

                            COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                            r9.NumberFormat = "@";
                            if (radNoiTru.SelectedIndex == 0)
                            {
                                r9.Value = "NGOAI";
                            }
                            else
                            {
                                if (radNoiTru.SelectedIndex == 1)
                                    r9.Value = "NOI";
                                else
                                    r9.Value = "";
                            }

                            r9.EntireColumn.ColumnWidth = 40;
                        }
                        exApp.Visible = true;
                        try
                        {
                            exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }

                        #endregion xuất theo bệnh viện 24009
                    }
                    else
                    {
                        #region xuất theo bệnh viện khác bỏ từ ngày 16/08/2016

                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Name = "Bieu21";
                        int k = 0;
                        int i = 1;
                        string[] arr = new string[] {"stt","ten_dvkt","ma_dvkt", "sl_ngoai", "sl_noi",
                    "dongia","thanhtien", "ma_cskcb"};
                        foreach (var b in arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                            r.Cells.Font.Bold = true;
                        }
                        foreach (var a in _lKetQua)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();
                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.NumberFormat = "@";
                            r2.Value2 = a.Ten_thuoc;
                            r2.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            r3.NumberFormat = "@";
                            r3.Value = a.MaQD;
                            r3.EntireColumn.ColumnWidth = 40;
                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            //r4.NumberFormat = "@";
                            r4.Value = Math.Round(a.SoluongNgT, 2);
                            r4.EntireColumn.ColumnWidth = 30;
                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            //r5.NumberFormat = "@";
                            r5.Value = Math.Round(a.SoluongNT, 2);
                            r5.EntireColumn.ColumnWidth = 12;
                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            //r6.NumberFormat = "@";
                            r6.Value = Math.Round(a.Don_gia, 2);
                            r6.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            //r7.NumberFormat = "@";
                            r7.Value = Math.Round(a.Thanh_tien, 2);
                            r7.EntireColumn.ColumnWidth = 15;
                            COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                            r8.NumberFormat = "@";
                            r8.Value = a.Ma_CSKCB;
                            r8.EntireColumn.ColumnWidth = 10;
                        }
                        exApp.Visible = true;
                        try
                        {
                            exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }

                        #endregion xuất theo bệnh viện khác bỏ từ ngày 16/08/2016

                        #region xuất theo bệnh viện khác bỏ từ ngày 16/08/2016

                        //COMExcel.Application exApp = new COMExcel.Application();
                        //COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                        //          COMExcel.XlWBATemplate.xlWBATWorksheet);
                        //COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        //exSheet.Name = "Bieu21";
                        //int k = 0;
                        //int i = 1;
                        //string[] arr = new string[] {"STT","ten_dvkt","soluong", "dongia", "thanhtien",
                        //"ma_dvkt","mabv", "loaikcb", "stt_dmbyt"};
                        //foreach (var b in arr)
                        //{
                        //    k++;
                        //    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        //    r.Value2 = b.ToString();
                        //    r.Columns.AutoFit();
                        //    r.Cells.Font.Bold = true;
                        //}
                        //foreach (var a in _lKetQua)
                        //{
                        //    i++;
                        //    COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        //    r1.Value2 = a.SoTTqd;
                        //    r1.Columns.AutoFit();
                        //    COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        //    r2.NumberFormat = "@";
                        //    r2.Value2 = a.Ten_thuoc;
                        //    r2.EntireColumn.ColumnWidth = 12;
                        //    COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        //    r3.NumberFormat = "@";
                        //    r3.Value = Math.Round(a.SoluongNgT + a.SoluongNT, 2);
                        //    r3.EntireColumn.ColumnWidth = 40;
                        //    COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        //    r4.NumberFormat = "@";
                        //    r4.Value = Math.Round(a.Don_gia, 2);
                        //    r4.EntireColumn.ColumnWidth = 30;
                        //    COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        //    r5.NumberFormat = "@";
                        //    r5.Value = Math.Round(a.Thanh_tien, 2);
                        //    r5.EntireColumn.ColumnWidth = 12;
                        //    COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        //    r6.NumberFormat = "@";
                        //    r6.Value = a.MaQD;
                        //    r6.EntireColumn.ColumnWidth = 15;
                        //    COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        //    r7.NumberFormat = "@";
                        //    r7.Value = DungChung.Bien.MaBV;
                        //    r7.EntireColumn.ColumnWidth = 15;
                        //    COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        //    r8.NumberFormat = "@";
                        //    if (radNoiTru.SelectedIndex == 0)
                        //    {
                        //        r8.Value = "NGOAI";
                        //    }
                        //    else
                        //    {
                        //        if (radNoiTru.SelectedIndex == 1)
                        //            r8.Value = "NOI";
                        //        else
                        //            r8.Value = "";
                        //    }
                        //    r8.EntireColumn.ColumnWidth = 15;
                        //    COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        //    r9.NumberFormat = "@";
                        //    r9.Value = a.SoTTqd;
                        //    r9.EntireColumn.ColumnWidth = 15;

                        //}
                        //exApp.Visible = true;
                        //try
                        //{
                        //    exQLBV.SaveAs(duongdan, COMExcel.XlFileFormat.xlWorkbookNormal,
                        //                    null, null, false, false,
                        //                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                        //                    false, false, false, false, false);
                        //}
                        //catch (Exception ex)
                        //{
                        //}
                        //finally
                        //{
                        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        //}

                        #endregion xuất theo bệnh viện khác bỏ từ ngày 16/08/2016
                    }
                }
            }

            #endregion Xuất excel biểu 21
        }

        #endregion xuất excel

        private bool XuatXML(List<cls19_20> _list, string path, int bieu)
        {
            bool rs = true;
            try
            {
                int num = 1;
                if (_mauso == 20)
                {
                    var xEle = new XElement("Bang2",
                                from item in _list
                                select new XElement("ma_lk",
                                    new XAttribute("ma_lk", item.Ma_lk),
                                               new XElement("stt", num++),
                                               new XElement("ma_thuoc", item.Ma_thuoc),
                                               new XElement("ma_nhom", item.Ma_nhom),
                                               new XElement("ten_thuoc", item.Ten_thuoc),
                                               new XElement("don_vi_tinh", item.Don_vi_tinh),
                                               new XElement("ham_luong", item.Ham_luong),
                                               new XElement("duong_dung", item.Duong_dung),
                                               new XElement("lieu_dung", ""),
                                               new XElement("so_dang_ky", item.So_dang_ky),
                                               new XElement("so_luong", Math.Round(item.SoluongNgT + item.SoluongNT, 2)),
                                               new XElement("don_gia", Math.Round(item.Don_gia, 2)),
                                               new XElement("tyle_tt", item.Tyle_tt),
                                               new XElement("thanh_tien", Math.Round(item.Thanh_tien, 0)),
                                               new XElement("ma_khoa", item.Ma_khoa),
                                               new XElement("ma_bac_si", item.Ma_ba_sy),
                                               new XElement("ma_benh", item.Ma_benh),
                                               new XElement("ngay_yl", item.Ngay_yl),
                                               new XElement("ma_pttt", rdTrongBH.SelectedIndex)
                                           ));
                    xEle.Save(path);
                    rs = true;
                }
                else if (_mauso == 19)// biểu 19
                {
                    var xEle = new XElement("Bang3",
                               from item in _list
                               select new XElement("ma_lk",
                                   new XAttribute("ma_lk", item.Ma_lk),
                                              new XElement("stt", num++),
                                              new XElement("ma_dich_vu", ""),
                                              new XElement("ma_vat_tu", item.Ma_thuoc),
                                              new XElement("ma_nhom", item.Ma_nhom),
                                              new XElement("ten_dich_vu", item.Ten_thuoc),
                                              new XElement("don_vi_tinh", item.Don_vi_tinh),
                                              new XElement("so_luong", Math.Round(item.SoluongNgT + item.SoluongNT, 2)),
                                              new XElement("don_gia", Math.Round(item.Don_gia, 2)),
                                              new XElement("tyle_tt", item.Tyle_tt),
                                              new XElement("thanh_tien", Math.Round(item.Thanh_tien, 0)),
                                              new XElement("ma_khoa", item.Ma_khoa),
                                              new XElement("ma_bac_si", item.Ma_ba_sy),
                                              new XElement("ma_benh", item.Ma_benh),
                                              new XElement("ngay_yl", item.Ngay_yl),
                                              new XElement("ngay_kq", item.Ngay_kq),
                                              new XElement("ma_pttt", rdTrongBH.SelectedIndex)
                                          ));
                    xEle.Save(path);
                    rs = true;
                }
                else// biểu 21
                {
                    var xEle = new XElement("Bang3",
                               from item in _list
                               select new XElement("ma_lk",
                                   new XAttribute("ma_lk", item.Ma_lk),
                                              new XElement("stt", num++),
                                              new XElement("ma_dich_vu", item.Ma_thuoc),
                                              new XElement("ma_vat_tu", ""),
                                              new XElement("ma_nhom", item.Ma_nhom),
                                              new XElement("ten_dich_vu", item.Ten_thuoc),
                                              new XElement("don_vi_tinh", item.Don_vi_tinh),
                                              new XElement("so_luong", Math.Round(item.SoluongNgT + item.SoluongNT, 2)),
                                              new XElement("don_gia", Math.Round(item.Don_gia, 2)),
                                              new XElement("tyle_tt", item.Tyle_tt),
                                              new XElement("thanh_tien", Math.Round(item.Thanh_tien, 0)),
                                              new XElement("ma_khoa", item.Ma_khoa),
                                              new XElement("ma_bac_si", item.Ma_ba_sy),
                                              new XElement("ma_benh", item.Ma_benh),
                                              new XElement("ngay_yl", item.Ngay_yl),
                                              new XElement("ngay_kq", item.Ngay_kq),
                                              new XElement("ma_pttt", rdTrongBH.SelectedIndex)
                                          ));
                    xEle.Save(path);
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                rs = false;
            }
            return rs;
        }

        private static String NgayTu_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 00:00:00 AM");
        }

        public static String NgayDen_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 23:59:59.998 PM");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private SaveFileDialog dialog = new SaveFileDialog();

        private void btnDuongDan_Click_1(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDan.Text = dialog.FileName;
            }
        }

        private void chkXuatExel_CheckedChanged(object sender, EventArgs e)
        {
            btnVsip.Visible = chkXuatExel.Checked;
            txtDuongDan.Visible = chkXuatExel.Checked;
            btnDuongDan.Visible = chkXuatExel.Checked;
            lbDuongDan.Visible = chkXuatExel.Checked;
            rdFont.Visible = chkXuatExel.Checked;
            chk917.Visible = chkXuatExel.Checked;
            ckBV30007.Visible = chkXuatExel.Checked;
            txtDuongDan.Text = chkXuatExel.Checked == true ? ("C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xls") : "";
            if (chkXuatExel.Checked == false)
            {
                btnVsip.Checked = false;
                DungChung.Bien.CheckbtnVsip = false;
            }
        }

        private void rdFont_EditValueChanged(object sender, EventArgs e)
        {
            if (rdFont.SelectedIndex == 0)
            {
                Font = 0;
            }
            else
                Font = 1;
        }

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

        private String convert(String str)
        {
            if (str != null)
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
            return str;
        }

        private void ckBNTT_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckBNTT.Checked)
            //    ckBHTT.Checked = false;
        }

        private void ckBHTT_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckBHTT.Checked)
            //    ckBNTT.Checked = false;
        }

        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {
                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {
                        }
                    }
                }
            }
        }

        private void rdChonBieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mauso = Convert.ToInt32(rdChonBieu.EditValue.ToString());
            cklTN.DataSource = null;
            resetForm();
        }

        private void resetForm()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            ckDtuongCT.Checked = false;
            _listNhomDV = new List<NhomDV>();

            if (_mauso == 19)
            {
                rdInBC.Enabled = false;
                rdInBC.Visible = false;
                lupNhaCC.Enabled = true;
                chkTieuNhom.Enabled = true;
                ckDtuongCT.Visible = false;
                ckcTheoYC.Enabled = false;
                _listNhomDV = (from ndv in data.NhomDVs.Where(p => p.Status == 1)
                               join dv in data.DichVus on ndv.IDNhom equals dv.IDNhom
                               where (dv.MaNhom5937 != 8 && dv.MaNhom5937 != 18)
                               select ndv).Distinct().ToList();
                //var listdvu = (from dv in data.DichVus
                //               join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                //               where ndv.Status == 1 && dv.MaNhom5937 != 8 //&& dv.MaNhom5937 != 18
                //               select ndv).Distinct().ToList();
                cklNhomDV.DisplayMember = "TenNhomCT";
                cklNhomDV.ValueMember = "IDNhom";
                cklNhomDV.DataSource = _listNhomDV;
                simpleButton1.Enabled = false;
                ck_CV285_20001.Checked = false;
                ck_CV285_20001.Visible = false;
                if (DungChung.Bien.MaBV == "24012")
                {
                    chk_3762.Visible = true;
                    chk_3762.Checked = false;
                }
                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    if (cklNhomDV.GetItemText(i).ToString() == "Vật tư y tế trong danh mục BHYT" || cklNhomDV.GetItemText(i).ToString() == "VTYT thanh toán theo tỷ lệ")
                        cklNhomDV.SetItemChecked(i, true);
                }
                if (DungChung.Bien.MaBV == "30004" && radTimKiem.Properties.Items.Count() > 5)
                {
                    radTimKiem.Properties.Items[5].Enabled = false;
                }
            }
            if (_mauso == 20)
            {
                rdInBC.Enabled = true;
                rdInBC.Visible = true;
                lupNhaCC.Enabled = true;
                chkTieuNhom.Enabled = true;
                ckcTheoYC.Enabled = false;
                if (DungChung.Bien.MaBV == "12001")
                    ckDtuongCT.Visible = true;
                _listNhomDV = (from ndv in data.NhomDVs.Where(p => p.Status == 1)
                               join dv in data.DichVus on ndv.IDNhom equals dv.IDNhom
                               where (dv.MaNhom5937 != 8 && dv.MaNhom5937 != 18)
                               select ndv).Distinct().ToList();
                //var listdvu = (from dv in data.DichVus
                //               join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                //               where ndv.Status == 1 && dv.MaNhom5937 != 8 //&& dv.MaNhom5937 != 18
                //               select ndv).Distinct().ToList();
                cklNhomDV.DisplayMember = "TenNhomCT";
                cklNhomDV.ValueMember = "IDNhom";
                cklNhomDV.DataSource = _listNhomDV;
                simpleButton1.Enabled = false;
                ck_CV285_20001.Checked = false;
                ck_CV285_20001.Visible = false;
                if (DungChung.Bien.MaBV == "24012")
                {
                    chk_3762.Visible = true;
                    chk_3762.Checked = false;
                }
                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    if (cklNhomDV.GetItemText(i).ToString() == "Thuốc trong danh mục BHYT" || cklNhomDV.GetItemText(i).ToString() == "Thuốc thanh toán theo tỷ lệ")
                        cklNhomDV.SetItemChecked(i, true);
                }
                if (DungChung.Bien.MaBV == "30004" && radTimKiem.Properties.Items.Count() > 5)
                {
                    radTimKiem.Properties.Items[5].Enabled = false;
                }
            }
            if (_mauso == 21)
            {
                rdInBC.Visible = false;
                rdInBC.Enabled = false;
                simpleButton1.Enabled = true;
                chkTieuNhom.Enabled = false;
                lupNhaCC.Enabled = false;
                ckDtuongCT.Visible = false;
                
                if (DungChung.Bien.MaBV == "24272")
                {
                    _listNhomDV = (from dv in data.DichVus
                                   join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                   where (dv.MaNhom5937 == 8 ||
                                   dv.MaNhom5937 == 18 ||
                                   dv.MaNhom5937 == 1 ||
                                   dv.MaNhom5937 == 2 ||
                                   dv.MaNhom5937 == 3 ||
                                   dv.MaNhom5937 == 9 ||
                                   dv.MaNhom5937 == 12 ||
                                   dv.MaNhom5937 == 13 ||
                                   dv.MaNhom5937 == 14 ||
                                   dv.MaNhom5937 == 15)
                                   select ndv).Distinct().ToList();
                }
                else
                {
                    _listNhomDV = (from dv in data.DichVus
                                   join ndv in data.NhomDVs on dv.MaNhom5937 equals ndv.IDNhom
                                   where (dv.MaNhom5937 == 8 ||
                                   dv.MaNhom5937 == 18 ||
                                   dv.MaNhom5937 == 1 ||
                                   dv.MaNhom5937 == 2 ||
                                   dv.MaNhom5937 == 3 ||
                                   dv.MaNhom5937 == 9 ||
                                   dv.MaNhom5937 == 12 ||
                                   dv.MaNhom5937 == 13 ||
                                   dv.MaNhom5937 == 14 ||
                                   dv.MaNhom5937 == 15)
                                   select ndv).Distinct().ToList();
                }
                //_listNhomDV = (from ndv in data.NhomDVs/*.Where(p => p.Status == 1 || p.Status == 2)*/
                //               join dv in data.DichVus on ndv.IDNhom equals dv.IDNhom
                //               where (dv.MaNhom5937 == 8 || dv.MaNhom5937 == 18 || dv.MaNhom5937 == 1 || dv.MaNhom5937 == 2 || dv.MaNhom5937 == 3 || dv.MaNhom5937 == 9 || dv.MaNhom5937 == 12 || dv.MaNhom5937 == 13 || dv.MaNhom5937 == 14 || dv.MaNhom5937 == 15)
                //               select ndv).Distinct().ToList();
                cklNhomDV.DisplayMember = "TenNhomCT";
                cklNhomDV.ValueMember = "IDNhom";
                cklNhomDV.DataSource = _listNhomDV;
                ckcTheoYC.Enabled = true;
                ck_CV285_20001.Checked = false;
                ck_CV285_20001.Visible = true;
                if (DungChung.Bien.MaBV == "24012")
                {
                    chk_3762.Visible = true;
                    chk_3762.Checked = false;
                }
                for (int i = 0; i < cklNhomDV.ItemCount; i++)
                {
                    cklNhomDV.SetItemChecked(i, true);
                }
                if (DungChung.Bien.MaBV == "30002" && radioThuChi.SelectedIndex == 0)
                {
                    radTimKiem.SelectedIndex = 4;
                }
                if (DungChung.Bien.MaBV == "30004" && radTimKiem.Properties.Items.Count() > 5)
                {
                    radTimKiem.Properties.Items[5].Enabled = true;
                }
            }

            ckXuatXML.Checked = false;
            chkXuatExel.Checked = false;
        }

        private void radNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radNoiTru.SelectedIndex == 0)
            {
                radio_DTNT.Properties.ReadOnly = false;
            }
            else
            {
                radio_DTNT.SelectedIndex = 2;
                radio_DTNT.Properties.ReadOnly = true;
            }
            //if (radNoiTru.SelectedIndex == 0)
            //{
            //    grvKhoaPhong.OptionsBehavior.ReadOnly = true;
            //    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            //    {
            //        grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            //    }
            //}
            //else
            //    grvKhoaPhong.OptionsBehavior.ReadOnly = false;
        }

        private void ckXuatXML_CheckedChanged_1(object sender, EventArgs e)
        {
            btnChonfileXML.Visible = ckXuatXML.Checked;
            txtFileXMLPath.Visible = ckXuatXML.Checked;
            txtFileXMLPath.Text = ckXuatXML.Checked == true ? ("C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xml") : "";
        }

        private void btnChonfileXML_Click_1(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "XML files (*.xml)|*.xml;";
            dialog.FilterIndex = 1;
            dialog.FileName = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + _mauso.ToString() + ".xml";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileXMLPath.Text = dialog.FileName;
            }
        }

        private void grcKhoaPhong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                GridControl grid = sender as GridControl;
                GridView view = grid.FocusedView as GridView;
                if (view.IsEditing)
                    view.CloseEditor();
                grid.SelectNextControl(grid, e.Modifiers == Keys.None, false, false, true);
                e.Handled = true;
                ckQuy.Focus();
            }
        }

        private void lupDoituong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupDoituong.Text == ("BHYT"))
            {
                rdTrongBH.Enabled = true;
                radXP.Enabled = true;
                //ckBHTT.Enabled = true;
                ckBNTT.Enabled = true;
                //ckBNTT.Checked = false;
                //ckBHTT.Checked = false;
                cboNoiTinh.Enabled = true;
                cklMaDTuong.Enabled = true;
            }
            else
            {
                cboNoiTinh.Enabled = false;
                cboNoiTinh.SelectedIndex = 0;
                rdTrongBH.Enabled = false;
                rdTrongBH.SelectedIndex = 0;
                radXP.Enabled = false;
                ckBNTT.Enabled = false;
                //ckBHTT.Enabled = false;
                ckBNTT.Checked = true;
                cklMaDTuong.Enabled = false;
            }
            cklMaDTuong.CheckAll();
        }

        private void radTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radTimKiem.SelectedIndex == 2)
            {
                rad_Duyet.Enabled = false;
                rad_Duyet.SelectedIndex = 1;
            }
            else
            {
                rad_Duyet.Enabled = true;
                rad_Duyet.SelectedIndex = 2;
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void rad_Duyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rad_Duyet.SelectedIndex == 1)
            {
                // radioThuChi.Properties.ReadOnly = false;
                radioThuChi.SelectedIndex = 2;
            }
            else
            {
                // radioThuChi.Properties.ReadOnly = true;
                radioThuChi.SelectedIndex = 2;
            }
        }

        private void grcKhoaPhong_Click(object sender, EventArgs e)
        {
        }

        public class CSKCB
        {
            public bool _check1;
            public string _maKP1;
            public string _kp1;

            public string MaKP
            { get { return _maKP1; } set { _maKP1 = value; } }
            public bool Check
            { get { return _check1; } set { _check1 = value; } }
            public string TenKP
            { get { return _kp1; } set { _kp1 = value; } }
        }

        private List<CSKCB> _lCSKCB = new List<CSKCB>();

        private void radXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            var kphong = _dataContext.KPhongs.ToList();
            if (radXP.SelectedIndex == 0)
            {
                _lCSKCB.Add(new CSKCB { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });
            }
            if (radXP.SelectedIndex == 1)
            {
                //DungChung.Bien.mack_theoHangBV
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong //|| (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
            }
            if (radXP.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new CSKCB()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
            }
            if (radXP.SelectedIndex == 3)
            {
                _lCSKCB = (from kp in _dataContext.BenhViens.Where(p => p.Connect)
                           select new CSKCB()
                           {
                               Check = false,//ád
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
            }
            _lCSKCB.Insert(0, new CSKCB { Check = false, MaKP = "0", TenKP = "Tất cả" });
            cklKP.DataSource = _lCSKCB;
            cklKP.CheckAll();
        }

        private void chk917_CheckedChanged(object sender, EventArgs e)
        {
            if (chk917.Checked)
            {
                ckBV30007.Checked = false;
                btnVsip.Checked = false;
            }
        }

        private void ckBV30007_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBV30007.Checked)
            {
                chk917.Checked = false;
                btnVsip.Checked = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frm_DsDichVuChon frm = new frm_DsDichVuChon();
            frm.passMaDV = new frm_DsDichVuChon.PassMaDV(PassData);
            frm.ShowDialog();
        }

        private List<int> _lDSMaDV = new List<int>();

        private void PassData(List<int> lmaDV)
        {
            _lDSMaDV = lmaDV;
        }

        private void cklMaDTuong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklMaDTuong.GetItemChecked(0) == true)
                    cklMaDTuong.CheckAll();
                else
                    cklMaDTuong.UnCheckAll();
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                for (int i = 1; i < cklKP.ItemCount; i++)
                {
                    cklKP.SetItemCheckState(i, e.State);
                }
            }
        }

        private void rdTrongBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdTrongBH.SelectedIndex == 0)
            {
                ckcTheoYC.Enabled = true;
            }
            else
            {
                ckcTheoYC.Checked = false;
                ckcTheoYC.Enabled = false;
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void rdHTThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdHTThanhToan.SelectedIndex == 1)
            {
                rdCKhoan.Enabled = true;
            }
            else
                rdCKhoan.Enabled = false;
        }

        private void btnVsip_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVsip.Checked)
            {
                ckBV30007.Checked = false;
                chk917.Checked = false;
                DungChung.Bien.CheckbtnVsip = true;
            }
            else
            {
                DungChung.Bien.CheckbtnVsip = false;
            }
        }

        private void cklTN_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                for (int i = 1; i < cklTN.ItemCount; i++)
                {
                    cklTN.SetItemCheckState(i, e.State);
                }
            }
        }

        public class LISTTieuNhomDV
        {
            public int? IdTieuNhom { get; set; }
            public string TenTN { get; set; }
        }

        private List<LISTTieuNhomDV> _lTN = new List<LISTTieuNhomDV>();

        private void chk_3762_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_3762.Checked)
            {
                DungChung.Bien.Check_CV3762 = true;
            }
            else
            {
                DungChung.Bien.Check_CV3762 = false;
            }
        }

        private void chk_3762_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private void rdInBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdInBC.EditValue.ToString() == "indoc")
            {
                //
                DungChung.Bien.Select_InDoc = true;
            }
            else
            {
                //
                DungChung.Bien.Select_InDoc = false;
            }
        }

        private void chkTieuNhom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTieuNhom.Checked == true && rdChonBieu.SelectedIndex == 1)
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _lTN.Clear();

                //_lTN = (from dv in _dataContext.DichVus
                //        join p in _dataContext.TieuNhomDVs on dv.IDNhom equals p.IDNhom
                //        //where dv.MaNhom5937 == 15 || dv.MaNhom5937 == 3 || dv.MaNhom5937 == 6 || dv.MaNhom5937 == 1 || dv.MaNhom5937 == 18 || dv.MaNhom5937 == 10 || dv.MaNhom5937 == 4 || dv.MaNhom5937 == 13 || dv.MaNhom5937 == null || dv.MaNhom5937 == 16 || dv.MaNhom5937 == 2 || dv.MaNhom5937 == 20 || dv.MaNhom5937 == 8
                //        select new LISTTieuNhomDV()
                //        {
                //            IdTieuNhom = dv.MaNhom5937,
                //            TenTN = p.TenTN
                //        }).Distinct().OrderBy(p => p.TenTN).ToList();

                _lTN = (from tn in _dataContext.TieuNhomDVs.Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 20)
                        select new LISTTieuNhomDV()
                        {
                            IdTieuNhom = tn.IdTieuNhom,
                            TenTN = tn.TenTN
                        }).Distinct().OrderBy(p => p.TenTN).ToList();

                _lTN.Insert(0, new LISTTieuNhomDV { IdTieuNhom = 0, TenTN = "Tất cả" });

                cklTN.DataSource = _lTN;
                cklTN.CheckAll();
            }
            else
            {
                cklTN.DataSource = null;
            }
        }
    }
}