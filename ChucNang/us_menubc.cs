using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;
using QLBV.TraCuu;
using QLBV.ChucNang.FormDanhMuc;
using QLBV.ChucNang;
using QLBV.BaoCao;
using QLBV.FormThamSo.SoTiepDon;

namespace QLBV.FormThamSo
{
    public partial class us_menubc : DevExpress.XtraEditors.XtraUserControl
    {
        string _ploaimenu = "", _kieu = "", _mabv = "";
        bool _public = true;
        public string tenLoaiBaoCao;
        public us_menubc()
        {
            InitializeComponent();
        }
        public us_menubc(string pl)
        {
            InitializeComponent();
            _ploaimenu = pl;
        }
        public us_menubc(string pl, string kieu, string mabv, bool _pl)
        {
            InitializeComponent();
            _ploaimenu = pl;
            _kieu = kieu;
            _mabv = mabv;
            _public = _pl;
        }
        //public class limenu
        //{
        //    public string tenbc;
        //    public int id;
        //    public string ploai;
        //    public string mabv;
        //    public string kieu, nhom;
        //    public bool _public;
        //    public bool _Public
        //    {
        //        set { _public = value; }
        //        get { return _public; }
        //    }
        //    public string Nhom
        //    {
        //        set { nhom = value; }
        //        get { return nhom; }
        //    }
        //    public string Kieu
        //    {
        //        set { kieu = value; }
        //        get { return kieu; }
        //    }
        //    public string MaBV
        //    {
        //        set { mabv = value; }
        //        get { return mabv; }
        //    }
        //    public string TenBC
        //    {
        //        set { tenbc = value; }
        //        get { return tenbc; }
        //    }
        //    public int ID
        //    {
        //        set { id = value; }
        //        get { return id; }
        //    }
        //    public string Loai
        //    {
        //        set { ploai = value; }
        //        get { return ploai; }
        //    }
        //}
        #region set danh mục
        public static List<Library_CLS.Lis_His.limenu> SetDM()
        {

            List<Library_CLS.Lis_His.limenu> _lm = new List<Library_CLS.Lis_His.limenu>();
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC nội trú theo khoa phòng", ID = 1, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC nội trú theo mã bệnh", ID = 2, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC nội trú theo chuyên khoa", ID = 3, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC theo nhóm đối tượng ", ID = 4, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT) ", ID = 5, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-BHYT) ", ID = 6, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79ath-BHYT) ", ID = 7, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80ath-BHYT) ", ID = 8, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thông kê tổng hợp thuốc sử dụng(mẫu 20-BHYT) ", ID = 9, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê tổng hợp dịch vụ sử dụng(mẫu 21-BHYT) ", ID = 10, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a-BHYT) ", ID = 11, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB các nhóm đối tượng theo tuyến CMKT(mẫu 14b-BHYT)", ID = 12, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến nội-ngoại tỉnh CT( mẫu 10ct|11ct-BHYT)", ID = 13, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến ngoại tỉnh TH( mẫu 11TH-BHYT)", ID = 14, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến nội tỉnh TH( mẫu 10TH-BHYT)", ID = 15, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79B-BHYT)", ID = 16, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80B-BHYT)", ID = 17, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79B(%BH)-BHYT)", ID = 18, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80B(%BH)-BHYT)", ID = 19, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a(%BH)-BHYT) ", ID = 20, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB nội trú các nhóm đối tượng theo tuyến CMKT(mẫu 14b(%BH)-BHYT)", ID = 21, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân nộp tạm thu", ID = 22, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách dịch vụ bệnh nhân đã thực hiện chưa thanh toán", ID = 23, Loai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ vào viện(YS-TQ)", ID = 24, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo các trường hợp tai nạn lao đông_30010", ID = 25, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ vào viện theo đối tượng(YS-TQ)", ID = 26, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi viện phí(VY-BG)", ID = 27, Loai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chi tiết dược nội trú tồn thực sử dụng và thực sự dụng", ID = 28, Loai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê Thuốc - VTYT - dịch vụ kỹ thuật thanh toán(mẫu 19|20|21-BHYT_1399) ", ID = 29, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê thuốc thanh toán(mẫu 20-BHYT_1399) ", ID = 30, Ploai = "BHYT", _Public = true, MaBV = "", Kieu="Report" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê Bệnh Nhân điều trị ", ID = 31, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám bệnh định mức theo ngày ", ID = 542, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT_1399)_SP", ID = 32, Loai = "BHYT- Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thông kê  thuốc thanh toán(mẫu 20_1399-BHYT)_SP ", ID = 33, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê  dịch vụ thanh toán(mẫu 21_1399-BHYT)_SP ", ID = 34, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-BHYT_1399)_SP", ID = 35, Loai = "BHYT- Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a-BHYT)_SP ", ID = 36, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê KCB nội trú các nhóm đối tượng theo tuyến CMKT(mẫu 14b-BHYT)_SP", ID = 37, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thông kê VTYT thanh toán BHYT(mẫu 19_1399-BHYT)_SP ", ID = 38, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chi phí khám chữa bệnh theo từng bệnh nhân ", ID = 39, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phẫu thuật theo phân loại ", ID = 40, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Biên bản kiểm kê thuốc tại các khoa điều trị(CL-HD)", ID = 42, Loai = "Tổng hợp", _Public = false, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp tình hình người bệnh chuyển đến", ID = 43, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 14/2014/TT-BYT" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác chuyển tuyến", ID = 44, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 14/2014/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám bệnh(CL - HD)", ID = 45, Loai = "Tổng hợp", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng thuốc điều trị", ID = 46, Loai = "Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tai nạn", ID = 47, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám chữa bệnh(CL - HD)", ID = 48, Loai = "Tổng hợp", _Public = false, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp thông tin nhận người bệnh từ các tuyến chuyển đến", ID = 49, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 14/2014/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê danh sách Bệnh Nhân Vào|Chuyển viện", ID = 50, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê danh sách Bệnh Nhân có đơn thuốc", ID = 51, Loai = "Khám bệnh|Điều trị - Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách thu tiền", ID = 52, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp CP KCB BHYT trẻ em dưới 6 tuổi chưa có thẻ BHYT", ID = 53, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tình hình hoạt động khám sức khỏe(VY-BG)", ID = 54, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Khám sức khỏe" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động tại khoa khám bệnh", ID = 55, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp danh sách Bệnh Nhân có BHYT đăng ký KCB", ID = 56, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp khám chữa bệnh toàn viện(YS)", ID = 57, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê số lượt khám bệnh ngoại trú theo chuyên khoa(YS)", ID = 58, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ khám bệnh", ID = 59, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ vào-ra-chuyển viện", ID = 60, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê số lượt từng BN khám ngoại trú", ID = 61, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Hoạt động khám chữa bệnh phòng khám(TK-CB)", ID = 62, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách cấp mẫu chứng sinh", ID = 551, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách cấp giấy ra viện", ID = 552, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số liệu khám bệnh hàng ngày", ID = 544, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê kết cấu bệnh tật người lớn(TK-CB)", ID = 63, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Phân tích bệnh tật" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê kết cấu bệnh tật trẻ em(TK-CB)", ID = 64, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Phân tích bệnh tật" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách nộp tiền viện phí theo khoa(TK-CB)", ID = 65, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách nộp tiền viện phí khám sức khỏe(TK-CB)", ID = 66, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách nộp tiền viện phí(TK-CB)", ID = 67, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê thu tiền viện phí", ID = 68, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê viện phí theo nhóm dịch vụ", ID = 69, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê thu tiền viện phí BHYT(BG_-HD)", ID = 70, Loai = "Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê thu tiền KSK(BG-HD)", ID = 71, Loai = "Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi tạm ứng và thanh toán tạm ứng(BG-HD)", ID = 72, Loai = "Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi tạm ứng", ID = 73, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo giao ban hằng ngày_01071", ID = 74, Loai = "Khám bệnh|Điều trị -Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });


            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách trẻ em khám không thẻ BHYT", ID = 75, Loai = "Khám bệnh|Điều trị -Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân cấp cứu", ID = 76, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp thông tin chuyển tuyến", ID = 77, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 14/2014/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp chi phí vận chuyển người bệnh BHYT", ID = 78, Loai = "Khám bệnh|Điều trị -Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 14/2014/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Nộp tiền vào quỹ", ID = 79, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp thuốc hàng ngày - Biểu công khai thuốc hàng ngày", ID = 80, Loai = "Khám bệnh|Điều trị - Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 23/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê chi phí theo khoa phòng", ID = 81, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách tổng hợp thu tiền % viện phí BHYT", ID = 82, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê chi tiết phẫu thuật (BG - HD)", ID = 83, Loai = "Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BẢNG KÊ CHI TIẾT THU VIỆN PHÍ(BG-HD)", ID = 84, Loai = "Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê viện phí theo khoa(BG-HD)", ID = 85, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp thu viện phí(BG-HD)", ID = 86, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng tổng hợp viện phí nội trú(BG-HD)", ID = 87, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng tổng hợp tạm ứng  và thanh toán tạm ứng(BG-HD)", ID = 88, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = DungChung.Bien.MaBV != "30010" ? "Tình hình bệnh tật, tử vong tại bệnh viện theo icd 10 - who(VY-BG)" : "Tình hình bệnh tật, tử vong tại bệnh viện theo ICD 10", ID = 89, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám chữa bệnh(VY-BG)", ID = 90, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT_1399) ", ID = 91, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB nội - ngoại trú(mẫu 79act|80act-BHYT_1399) ", ID = 92, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chi phí nội trú(HL - CB) ", ID = 93, Loai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động phòng khám cơ sở 2 ", ID = 541, Loai = "Viện phí - Tổng hợp", _Public = true, MaBV = "01049", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng chấm công thủ thuật - Phẫu thuật", ID = 94, Loai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thuốc tồn gối", ID = 95, Loai = "Viện phí - Khám bệnh|Điều trị - Dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng quản lý thuốc thành phẩm (gây nghiện, tâm thần, tiền chất)", ID = 546, Loai = "Viện phí - Khám bệnh|Điều trị - Dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-(%)BHYT)", ID = 96, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách bệnh nhân tồn nội trú có bảo hiểm", ID = 540, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-(%)BHYT)", ID = 97, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê tiền thuốc BHYT-Dịch vụ(YM-HY)", ID = 98, Loai = "Viện phí - Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khoa khám bệnh(Chí Linh-HD)", ID = 99, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám bệnh(Na Hang-TQ)", ID = 100, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số lượng bệnh nhân khám bệnh theo bác sĩ", ID = 537, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "27001", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" }); //Yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân BHYT nội ngoại trú (Mẫu C79 - TT102)", ID = 553, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BHYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám chữa bệnh theo tuần", ID = 173, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "30010", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            //Báo cáo khoa dược
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC nhập xuất tồn", ID = 101, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC nhập xuất tồn(rút gọn)", ID = 102, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC hàng xuất", ID = 103, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TH danh sách BN xuất dược ", ID = 104, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC sử dụng thuốc - Hóa chất - VTYT tiêu hao", ID = 105, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 22/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hàng xuất theo đối tượng(Nam Sách-HD)", ID = 106, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi nhóm thuốc gây nghiện-hướng tâm thần", ID = 107, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 20/2017/TT-BYT" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thẻ kho(TT22) ", ID = 108, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 22/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BB kiểm kê thuốc-Hóa chất -VTYT(TT22)", ID = 109, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 22/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo VTYT tiêu hao theo khoa|Phòng(Không TT)", ID = 110, Loai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ thẻ kho", ID = 111, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê 15 ngày sử dụng thuốc khoa dược", ID = 112, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 23/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ kiểm nhập(TT22)", ID = 113, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 22/2011/TT-BYT" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn toàn viện(CM|NB)", ID = 114, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "TK Danh Mục thuốc trong BHYT sử dụng tại đơn vị(CM-06)", ID = 115, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn kho cấp phát(CM)", ID = 116, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn kho tổng(CM)", ID = 117, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BB kiểm nhập thuốc - hóa chất - VTYT(CM)", ID = 118, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng nhập" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo NXT xã- phường", ID = 119, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khoa dược bệnh viện(TT22)", ID = 120, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 22/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn thuốc đông y", ID = 121, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp hàng nhập", ID = 122, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng nhập" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp hàng xuất", ID = 123, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = " Báo cáo xuất dược", ID = 124, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ đối chiếu thuốc theo khoa phòng", ID = 125, Loai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn tủ trực", ID = 126, Loai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng dược(BG-HD)", ID = 127, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hàng xuất", ID = 128, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn theo từng phân loại", ID = 129, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo xuất theo đối tượng(Tam đường - LC)", ID = 130, Loai = "Khoa dược", _Public = false, MaBV = "12001", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi xuất-nhập thuốc gây nghiện, thuốc hướng tâm thần(Mẫu số 2 - TT19)", ID = 131, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 19|2014|TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn kho(Thanh Hà - HD)", ID = 132, Loai = "Khoa dược", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê hàng nhập theo nhà cung cấp", ID = 133, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng nhập" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp chứng từ mua thuốc", ID = 134, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng nhập" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê nhập theo từng loại thuốc", ID = 135, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng nhập" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn (Nội tiết - CB)", ID = 136, Loai = "Khoa dược", _Public = false, MaBV = "04018", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn theo đối tượng(Tam đường - LC)", ID = 137, Loai = "Khoa dược", _Public = false, MaBV = "12001", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn theo đối tượng(Thanh hà - HD)", ID = 138, Loai = "Khoa dược", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hàng xuất theo khoa|phòng", ID = 139, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân xuất dược theo đối tượng(Tam đường - LC)", ID = 140, Loai = "Khoa dược", _Public = false, MaBV = "12001", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tồn xã phường(Bình Giang - HD)", ID = 141, Loai = "Khoa dược", _Public = false, MaBV = "30002", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khoa dược bệnh viện", ID = 142, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kiểm kê và sử dụng thuốc(Bảo lạc - CB)", ID = 143, Loai = "Khoa dược", _Public = false, MaBV = "04002", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn(Việt Yên - BG)", ID = 144, Loai = "Khoa dược", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng thuốc gây nghiện hướng tâm thần(Việt Yên - BG)", ID = 145, Loai = "Khoa dược", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập xuất tồn(Kinh Môn - HD)", ID = 146, Loai = "Khoa dược", _Public = true, MaBV = "30005", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập - xuất|sử dụng - tồn(Bình Giang - HD)", ID = 147, Loai = "Khoa dược", _Public = true, MaBV = "30002", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nhập - xuất - tồn Gây nghiện|Hướng tâm thần(Bình Giang - HD)", ID = 148, Loai = "Khoa dược", _Public = true, MaBV = "30002", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo xuất dược(Chí linh - HD)", ID = 149, Loai = "Khoa dược", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Biên bản kiểm kê dược(Kinh Môn - HD)", ID = 150, Loai = "Khoa dược", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Biên bản kiểm kê dược(Tứ kỳ - HD)", ID = 151, Loai = "Khoa dược", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "Nhập xuất tồn" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Phiếu tổng xuất(12122 - LC)", ID = 152, Loai = "Khoa dược", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Nhập Xuất Tồn(12122 - LC)", ID = 153, Loai = "Khoa dược", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "BC hàng xuất" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng xã(Thanh Hà - HD)", ID = 154, Loai = "Khoa dược", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "BC hàng xuất" });// kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thống kê thuốc thanh toán BHYT", ID = 155, Loai = "Khoa dược", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "BC hàng xuất" });// kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo NXT (Nhập khác)", ID = 156, Loai = "Khoa dược", _Public = false, MaBV = "04011", Kieu = "Report", Nhom = "Nhập xuất tồn" });// kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công nợ (nhập theo hóa đơn)", ID = 157, Loai = "Khoa dược", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Tổng hợp" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chi tiết nhập Thuốc - VTYT(TH-HD)", ID = 158, Loai = "Khoa dược", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "BC hàng nhập" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo xuất kháng sinh (30007)", ID = 159, Loai = "Khoa dược", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "BC hàng xuất" });// kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC tình hình sử dụng thuốc (30007)", ID = 160, Loai = "Khoa dược", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "BC hàng xuất" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách phiếu xuất kho", ID = 161, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "BC hàng xuất" });// kienl
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo NXT sản xuất dược", ID = 162, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo NXT (VAT)", ID = 163, Loai = "Khoa dược", _Public = false, MaBV = "27023", Kieu = "Report", Nhom = "Nhập xuất tồn" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo cảnh báo hạn dùng, số lượng Min", ID = 164, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hàng xuất (27022)", ID = 165, Loai = "Khoa dược", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "BC hàng xuất" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ kiểm nhập (20001)", ID = 166, Loai = "Khoa dược", _Public = true, MaBV = "166", Kieu = "Report", Nhom = "BC hàng nhập" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng thuốc bệnh viện (26007)", ID = 167, Loai = "Khoa dược", _Public = true, MaBV = "26007", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng tổng hợp thuốc lĩnh về khoa hoặc tủ trực", ID = 168, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo lĩnh thuốc, VTYT, hóa chất về khoa", ID = 169, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo N-X-T 30002", ID = 170, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });//đức
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Phiếu theo dõi chất lượng thuốc", ID = 171, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//Namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo N-X-T 01071", ID = 172, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Nhập xuất tồn" });//Namnt

            //hoạt động khám chữa bệnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám bệnh(Thanh Hà - HD)", ID = 200, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "báo cáo hoạt động  điều trị nội trú(Thanh Hà - HD)", ID = 201, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "báo cáo hoạt động  điều trị nội trú", ID = 202, Loai = "Tổng hợp", _Public = true, MaBV = "30003", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám bệnh(Chí Linh - HD)", ID = 203, Loai = "Tổng hợp", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám bệnh(Bảo Lạc - CB)", ID = 204, Loai = "Tổng hợp", _Public = false, MaBV = "04002", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám chữa bệnh người cao tuổi(Bảo Lạc - CB)", ID = 205, Loai = "Tổng hợp", _Public = false, MaBV = "04002", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chữa bệnh(Bảo Lạc - CB)", ID = 206, Loai = "Tổng hợp", _Public = false, MaBV = "04002", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp số lượt bệnh nhân theo tháng(Bảo Lạc - CB)", ID = 207, Loai = "Tổng hợp", _Public = false, MaBV = "04002", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê thời gian khám chữa bệnh theo từng bệnh nhân", ID = 208, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chữa bệnh(Bảo Lâm - CB)", ID = 209, Loai = "Tổng hợp", _Public = false, MaBV = "04012", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chữa bệnh trẻ em dưới 6 tuổi(Bảo Lâm - CB)", ID = 210, Loai = "Tổng hợp", _Public = false, MaBV = "04012", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám chữa bệnh(Việt Yên - BG)", ID = 211, Loai = "Tổng hợp", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp khám bệnh và điều trị nội trú (Tứ Kỳ)", ID = 212, Loai = "Tổng hợp", _Public = false, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác chuyển tuyến(Việt Yên - BG)", ID = 213, Loai = "Tổng hợp", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tai nạn giao thông", ID = 214, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động điều trị", ID = 215, Loai = "Tổng hợp", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động chuyên môn", ID = 216, Loai = "Tổng hợp", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp thủ thuật", ID = 217, Loai = "Tổng hợp", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám chữa bệnh", ID = 218, Loai = "Tổng hợp", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phân tích bệnh tật(Chí Linh - HD)", ID = 219, Loai = "Tổng hợp", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp hoạt động chuyên môn (Kim Thành - HD)", ID = 220, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tại nạn thương tích (Kim Thành - HD)", ID = 221, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khám chữa bệnh hàng ngày (Thường Tín - HN)", ID = 222, Loai = "Tổng hợp", _Public = false, MaBV = "01830", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số liệu bệnh viện hàng ngày (Thường Tín - HN)", ID = 223, Loai = "Tổng hợp", _Public = false, MaBV = "01830", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số liệu bệnh viện hàng ngày (Thường Tín - HN)", ID = 224, Loai = "Tổng hợp", _Public = false, MaBV = "01830", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số liệu bệnh viện hàng ngày (Thường Tín - HN)", ID = 225, Loai = "Tổng hợp", _Public = false, MaBV = "01830", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chỉ tiêu chuyên môn (Kim thành - HD)", ID = 226, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chỉ tiêu chuyên môn (Kim thành - HD)- Quý", ID = 533, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chỉ số công tác KCB (Lao phổi - LC)", ID = 227, Loai = "Tổng hợp", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp KCB (Kim Thành - HD)", ID = 228, Loai = "Tổng hợp", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ lưu trữ hồ sơ bệnh án (QĐ 4069)", ID = 229, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám bệnh (Cẩm Giàng - HD)", ID = 230, Loai = "Tổng hợp", _Public = false, MaBV = "30012", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động điều trị (Cẩm Giàng - HD)", ID = 231, Loai = "Tổng hợp", _Public = false, MaBV = "30012", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC tổng hợp danh sách bệnh nhân mổ (27022)", ID = 232, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Cận lâm sàng" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kết quả khám điều trị (27022)", ID = 233, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chuyên môn (27022)", ID = 234, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Tổng hợp" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo theo dõi BN nội trú (27022)", ID = 235, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Tổng hợp" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kết quả hoạt động tháng (27022)", ID = 236, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Tổng hợp" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động Pal tuyến huyện(KM)", ID = 237, Loai = "Tổng hợp", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo giao ban chuyên môn", ID = 238, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chữa bệnh (26007)", ID = 239, Loai = "Tổng hợp", _Public = true, MaBV = "26007", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo điều trị nội trú tại khoa phòng (12122)", ID = 240, Loai = "Tổng hợp", _Public = true, MaBV = "12122", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác KCB (12122)", ID = 241, Loai = "Tổng hợp", _Public = true, MaBV = "12122", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ khám bệnh (27022)", ID = 242, Loai = "Tổng hợp", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo giao ban tình hình điều trị", ID = 400, Loai = "Tổng hợp", _Public = true, MaBV = "14017", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám chữa bênh(12121)", ID = 243, Loai = "Tổng hợp", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// ducnh
            //  _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động điều trị nội trú (27021)", ID = 244, Loai = "Tổng hợp", _Public = false, MaBV = "27021", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách bệnh nhân xác nhận nghỉ ốm", ID = 245, Loai = "Tổng hợp", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tuần tháng _20001", ID = 246, Loai = "Tổng hợp", _Public = false, MaBV = "20001", Kieu = "Report", Nhom = "Tổng hợp" });// namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kết quả hoạt động_30010", ID = 247, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chưa bệnh ngoại trú _ 01071", ID = 248, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng kết năm - 01071", ID = 249, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "01071", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng kết cuối năm (30005)", ID = 251, Loai = "Tổng hợp", _Public = true, MaBV = "30005", Kieu = "Report", Nhom = "Tổng hợp" });//Namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ thống kê bán thuốc tổng hợp", ID = 252, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "27022", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//nam
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê bệnh nhân 139", ID = 253, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//nam
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp HĐ khám bệnh - 12001", ID = 254, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//nam
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng thuốc chi tiết", ID = 255, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//nam
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách BN KSK", ID = 256, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp chỉ đạo tuyến", ID = 257, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            //_lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Biểu công khia thuốc hàng ngày _ 01071", ID = 250, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "01071", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//namnt

            // _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ lưu trữ hồ sơ bệnh án (QĐ 4069)", ID = 230, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });// kienhv
            //báo cáo khoa khám bệnh/Điều trị
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê 15 ngày sử dụng thuốc, hóa chất, VTYT tiêu hao", ID = 300, Loai = "Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "TT: 23/2011/TT-BYT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê chi tiết viện phí(Bình Giang - HD)", ID = 301, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê chi tiết viện phí_%BHYT(Bình Giang - HD)", ID = 302, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp tạm ứng - thu - chi thanh toán", ID = 303, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tình hình bệnh tật theo ICD10(Thanh Hà - HD)", ID = 304, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tình hình bệnh tật theo ICD10(Chí Linh - HD)", ID = 305, Loai = "Tổng hợp", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi tạm ứng - thu - chi thanh toán", ID = 306, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Thu - Chi TT" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê số phiếu lĩnh", ID = 307, Loai = "Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thống kê thuốc - VTYT tiêu hao(Không TT)", ID = 308, Loai = "Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí hằng ngày", ID = 309, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám chữa bệnh(Kim thành - HD)", ID = 310, Loai = "Viện phí", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động điều trị(Kim thành - HD)", ID = 311, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp số liệu điều trị ngoại trú(Tứ kỳ - HD)", ID = 312, Loai = "Tổng hợp", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân vào viện ngoại trú(Tứ kỳ - HD)", ID = 313, Loai = "Tổng hợp", _Public = false, MaBV = "30007", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động tài chính(Kim thành - HD)", ID = 314, Loai = "Tổng hợp", _Public = false, MaBV = "30010", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phí xăng xe (phí vận chuyển) bệnh nhân BHYT được miễn", ID = 315, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Tổng hợp" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thuốc, VTYT trong cuộc mổ", ID = 316, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Tổng hợp" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo bệnh truyền nhiễm tháng", ID = 317, Loai = "Tổng hợp", _Public = false, MaBV = "30004", Kieu = "Report", Nhom = "Tổng hợp" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân mắc bệnh truyền nhiễm", ID = 318, Loai = "Tổng hợp", _Public = false, MaBV = "30004", Kieu = "Report", Nhom = "Tổng hợp" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi sinh sản", ID = 319, Loai = "Tổng hợp", _Public = false, MaBV = "30004", Kieu = "Report", Nhom = "Tổng hợp" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo theo dõi thanh toán KCB", ID = 320, Loai = "Tổng hợp", _Public = true, MaBV = "30005", Kieu = "Report", Nhom = "Viện phí" });//kienlv


            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp thu viện phí theo tháng(30021- BN)", ID = 321, Loai = "Tổng hợp", _Public = false, MaBV = "27023", Kieu = "Report", Nhom = "Viện phí" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp file 1,2,3 QĐ917", ID = 322, Loai = "Tổng hợp", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo theo nhóm tuổi và giới tính", ID = 323, Loai = "Tổng hợp", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Tổng hợp" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thực hiện chỉ tiêu chuyên môn", ID = 324, Loai = "Tổng hợp", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Tổng hợp" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tồn dược kê đơn", ID = 325, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//vietanh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách bệnh nhân hủy, xóa số", ID = 326, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//yenntb
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo giao ban hằng ngày (27021)", ID = 327, Loai = "Tổng hợp", _Public = true, MaBV = "27021", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo KCB và viện phí (27021)", ID = 328, Loai = "Tổng hợp", _Public = false, MaBV = "27021", Kieu = "Report", Nhom = "Viện phí" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo bệnh nhân ra vào viện", ID = 329, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thông tin hoạt động chuyên môn của bệnh viện", ID = 330, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ giao ban hồ sơ bệnh nhân _ 01071", ID = 331, Loai = "Tổng hợp", _Public = true, MaBV = "01071", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tác động điều chỉnh giá 30007", ID = 332, Loai = "Tổng hợp", _Public = true, MaBV = "30007", Kieu = "Report", Nhom = "Tổng hợp" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thống kê chi phí thanh toán theo dịch vụ", ID = 333, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động và công tác KCB 01049", ID = 334, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí hằng ngày 30005", ID = 335, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ chứng sinh 01071", ID = 336, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC Bệnh nhân điều trị ngoại trú", ID = 337, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí hằng ngày 30002", ID = 338, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kết quả công tác khám chữa bệnh", ID = 555, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Sổ khám chữa bệnh theo từng phòng khám", ID = 556, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách cán bộ", ID = 339, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });


            // báo cáo CLS
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân thực hiện thủ thuật - phẫu thuật", ID = 570, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp vật lý trị liệu", ID = 571, Loai = "Khám bệnh|Điều trị", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Hoạt động phẫu thuật - thủ thuật", ID = 402, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách BN thực hiện CLS theo khoa phòng", ID = 545, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" }); //yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp Phẫu Thuật", ID = 403, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp Thủ Thuật", ID = 404, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp Nội Soi", ID = 405, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp Siêu Âm - X.Quang - Điện tim", ID = 406, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Hoạt động cận lâm sàng(theo đối tượng và nội|ngoại trú)", ID = 407, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp số lượt - kết quả Cận Lâm Sàng", ID = 408, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động cận lâm sàng ", ID = 409, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo khoa xét nghiệm (YM -HY)", ID = 410, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp Siêu Âm - X.Quang - Điện tim(Yên sơn - TQ)", ID = 411, Loai = "CLS", _Public = false, MaBV = "08204", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động cận lâm sàng (Chí Linh - HD)", ID = 412, Loai = "CLS", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Cận lâm sàng" });//senpt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ xét nghiệm XPert", ID = 413, Loai = "CLS", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ xét nghiệm Soi trực tiếp", ID = 414, Loai = "CLS", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ xét nghiệm Kháng sinh đồ VKL", ID = 415, Loai = "CLS", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ xét nghiệm nuôi cấy VKL", ID = 416, Loai = "CLS", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp CLS (Viện Mắt BN)", ID = 417, Loai = "CLS", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC CLS trong ngày", ID = 418, Loai = "CLS", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC hoạt động CLS", ID = 419, Loai = "CLS", _Public = false, MaBV = "12121", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC tháng khoa Xét nghiệm CĐHA (Kinh Môn)", ID = 420, Loai = "CLS", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC công tác CLS (12122)", ID = 421, Loai = "CLS", _Public = false, MaBV = "12122", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC danh sách bệnh nhân hội chẩn", ID = 422, Loai = "CLS", _Public = false, MaBV = "26007", Kieu = "Report", Nhom = "Cận lâm sàng" });//vietanh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC số lần thực hiện phẫu thuật thủ thuật", ID = 423, Loai = "CLS", _Public = false, MaBV = "27021", Kieu = "Report", Nhom = "Cận lâm sàng" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân thực hiện phẫu thuật thủ thuật", ID = 424, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" }); //cuongtm
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân thực hiện xét nghiệm", ID = 425, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//cuongtm
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp thực hiện dịch vụ kỹ thuật", ID = 426, Loai = "CLS", _Public = false, MaBV = "20001", Kieu = "Report", Nhom = "Cận lâm sàng" });//cuongtm
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo dịch vụ kỹ thuật chi tiết (Tam tra PTTT - 20001)", ID = 427, Loai = "CLS", _Public = false, MaBV = "20001", Kieu = "Report", Nhom = "Cận lâm sàng" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo CLS theo bộ phận (01071)", ID = 428, Loai = "CLS", _Public = true, MaBV = "01071", Kieu = "Report", Nhom = "Cận lâm sàng" });//ducnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp thủ thuật - 01071", ID = 429, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//nam
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ đo Khúc xạ - Nhãn áp - Giác mạc", ID = 430, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//duc
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ tổng hợp CLS có qua chỉ định", ID = 431, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//duc
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chỉ định miễn dịch", ID = 432, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo xét nghiệm hàng ngày (12345)", ID = 433, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo xét nghiệm 30007", ID = 434, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo cận lâm sàng theo khoa|phòng 30007", ID = 435, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách BN phẫu thuật sử dụng PP vô cảm", ID = 436, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách BN phẫu thuật sử dụng PP vô cảm", ID = 436, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Thăm dò chức năng", ID = 538, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" }); // Yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BÁO CÁO TÌNH HÌNH SỨC KHOẺ CÁN BỘ CAO CẤP (CAT - TQ)", ID = 500, Loai = "Tổng hợp", _Public = false, MaBV = "?????", Kieu = "Report", Nhom = "Tổng hợp" });//dinhpv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi công tác sắc thuốc trong cơ sở khám bệnh, chữa bệnh", ID = 548, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "30005", Kieu = "Report", Nhom = "Cận lâm sàng" });//yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp thu theo từng phòng CLS", ID = 554, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phẫu thuật thủ thuật ngoài giờ hành chính", ID = 557, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "30007", Kieu = "Report", Nhom = "Cận lâm sàng" });//yenbg
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo bệnh nhân theo ngày/tháng/năm", ID = 558, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi bệnh nhân vào viện 14017", ID = 564, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "14017", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi thuốc kháng sinh", ID = 565, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "27022", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi thông tin bệnh nhân sử dụng thuốc phải kiểm soát đặc biệt", ID = 566, Loai = "Tổng hợp - CLS", _Public = true, MaBV = "27022", Kieu = "Report", Nhom = "Cận lâm sàng" });


            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usNhapDuoc", ID = 900, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usXuatDuoc", ID = 901, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_HuHao", ID = 902, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_SuDung", ID = 903, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usTamThu_TToan", ID = 904, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "xtraTThu", ID = 9041, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "xtraTToan", ID = 9042, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "xtraDuyet", ID = 9043, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "xtraDuyetCP", ID = 9044, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frm_XemChiPhi", ID = 905, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_Export_XML_2348", ID = 906, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frm_DmDTBN", ID = 907, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "uc_DuyetBenhAn", ID = 908, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usDieuTri", ID = 909, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_TCThuocKD", ID = 910, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frmHSBNNhapMoi", ID = 9045, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usDichVu", ID = 9046, Loai = "Danh mục", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Danh mục" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_dmDuoc", ID = 9047, Loai = "Danh mục", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Danh mục" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "usManHinhLanhDao", ID = 9048, Loai = "Danh mục", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Danh mục" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frmDuyetPhieuLinh", ID = 9049, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frmLichTruc", ID = 9050, Loai = "DƯỢC", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "frmThietLapSoLuuTru", ID = 9051, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "us_dmCanBo", ID = 9052, Loai = "Danh mục", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Danh mục" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "UpdateCanBo", ID = 9053, Loai = "Danh mục", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Danh mục" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Us_ThongBao", ID = 9054, Loai = "Chức năng", _Public = true, MaBV = "", Kieu = "Form", Nhom = "Chức năng" });

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp số liệu bệnh nhân thực hiện CLS theo Khoa", ID = 559, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "14018", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo số liệu dịch vụ CLS theo ngày", ID = 560, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "14018", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ đẻ", ID = 561, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "30007", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo cơ sở, giường bệnh và hoạt động khám chữa bệnh theo TT37 biểu 9", ID = 562, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phí thu thẳng bệnh nhân ngoại trú", ID = 501, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thuốc đã và chưa thanh toán", ID = 502, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê tạm ứng viện phí", ID = 503, Loai = "Viện phí", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí XHH", ID = 504, Loai = "Viện phí", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp biên lai viện phí", ID = 505, Loai = "Viện phí", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng thu viện phí", ID = 506, Loai = "Viện phí", _Public = false, MaBV = "30003", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê viện phí bệnh nhân ra viện", ID = 507, Loai = "Viện phí", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê tạm ứng viện phí hàng ngày", ID = 508, Loai = "Viện phí", _Public = false, MaBV = "30005", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Chi phí CLS theo khoa phòng thực hiện", ID = 509, Loai = "Viện phí", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo đối chiếu thuốc thanh toán BHYT ngoại trú", ID = 510, Loai = "Viện phí", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Viện phí" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp chứng từ thu chi (27022)", ID = 511, Loai = "Viện phí", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp chứng từ thu tạm ứng (27022)", ID = 512, Loai = "Viện phí", _Public = false, MaBV = "27022", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo TH bệnh nhân thanh toán ra viện (27023)", ID = 513, Loai = "Viện phí", _Public = false, MaBV = "27023", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo TH thu VP (27023)", ID = 514, Loai = "Viện phí", _Public = false, MaBV = "27023", Kieu = "Report", Nhom = "Viện phí" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi thu tạm ứng BN điều trị (27023)", ID = 515, Loai = "Viện phí", _Public = false, MaBV = "27023", Kieu = "Report", Nhom = "Viện phí" });//kienlv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo đối chiếu viện phí theo giá mới và giá cũ", ID = 516, Loai = "Viện phí", _Public = false, MaBV = "27001", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC Doanh Thu VAT (27183)", ID = 517, Loai = "Viện phí", _Public = false, MaBV = "27183", Kieu = "Report", Nhom = "Viện phí" });//vietanh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC tạm thu viện phí theo ngày (26007)", ID = 518, Loai = "Viện phí", _Public = false, MaBV = "26007", Kieu = "Report", Nhom = "Viện phí" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "BC viện phí theo khoa phòng thực hiện CLS (24009)", ID = 519, Loai = "Viện phí", _Public = false, MaBV = "24009", Kieu = "Report", Nhom = "Viện phí" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách thuốc BHYT chênh lệch giữa kho và kế toán", ID = 520, Loai = "Viện phí", _Public = false, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê  bán lẻ hàng hóa, DV (30003)", ID = 521, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//duc
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí theo phần trăm BHYT (30003)", ID = 522, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//duc
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu nguồn dịch vụ - XHH từ % BH (30003)", ID = 523, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo ra viện bệnh nhân BHYT (30003)", ID = 524, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//duc
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê quyết toán viện phí", ID = 525, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp viện phí BN ra viện _30005", ID = 526, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí theo ca", ID = 527, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí theo tháng", ID = 528, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí thu thẳng chi tiết theo dịch vụ", ID = 529, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí 30007", ID = 530, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí theo khoa phòng 30002", ID = 531, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí theo hóa đơn", ID = 532, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh mục thuốc chế phẩm YHCT thanh toán BHYT mẫu 16", ID = 534, Loai = "Khoa dược", _Public = true, MaBV = "20001", Kieu = "Report", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh mục vị thuốc YHCT thanh toán BHYT mẫu 17", ID = 535, Loai = "Khoa dược", _Public = true, MaBV = "20001", Kieu = "Report", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thu viện phí theo cán bộ", ID = 536, Loai = "Viện phí", _Public = true, MaBV = "30005", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tháng chẩn đoán hình ảnh", ID = 539, Loai = "Cận lâm sàng", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" }); // yenbg      
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo viện phí theo hóa đơn VIETTEL Nam Thăng Long ", ID = 543, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo nộp tiền vào quỹ NTL", ID = 547, Loai = "Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//dungtt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân thực hiện CLS", ID = 549, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ chi tiết nguyên liệu, vật liệu, công cụ, dụng cụ, sản phẩm, hàng hóa", ID = 550, Loai = "Khoa dược", _Public = true, MaBV = "14018", Kieu = "Report", Nhom = "Dược" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp chi phí thực hiện CLS tại khoa", ID = 563, Loai = "Cận lâm sàng", _Public = true, MaBV = "14017", Kieu = "Report", Nhom = "Tổng hợp" }); // yenbg 
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo công tác khám chữa bệnh nội - ngoại trú (01071, 01049)", ID = 567, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });//nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Test Sars-CoV-2 (01071)", ID = 568, Loai = "CLS", _Public = true, MaBV = "01071", Kieu = "Report", Nhom = "Cận lâm sàng" });//nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng theo dõi bệnh nhân BHYT", ID = 569, Loai = "BHYT", _Public = true, MaBV = "30009", Kieu = "Report", Nhom = "BHYT" });//Minhvd
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp khoa khám bệnh 24012", ID = 572, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//Minhvd

            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng hợp khoa lâm sàng 24012", ID = 573, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//Minhvd
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo tổng số bệnh nhân tại khoa phòng (24006)", ID = 574, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//longnh
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo doanh thu khoa phòng (24006)", ID = 575, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//Nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo danh sách bệnh nhân trả thuốc ngoại trú", ID = 576, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//Nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo phân loại mua thuốc, vật tư, hóa chất", ID = 577, Loai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Dược" });//Nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp khám chữa bệnh nội trú, ngoại trú, viện phí, xã tháng...", ID = 578, Loai = "Tổng hợp", _Public = false, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });//Nhukt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo hoạt động khám chữa bệnh", ID = 579, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });// Hoalv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Danh sách bệnh nhân", ID = 580, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
          /*  _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo thông tin hoạt động chuyên môn của bệnh viện", ID = 581, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });*///namnt
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ theo dõi bệnh nhân vào viện, ra viện, chuyển viện", ID = 582, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Tổng hợp" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp hoạt động khám chữa bệnh toàn viện 27023", ID = 583, Loai = "Khám bệnh|Điều trị - Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Tổng hợp bệnh nhân ngoại trú lĩnh thuốc", ID = 584, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo chi phí khám chữa bệnh theo mã bệnh", ID = 585, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Hoạt động khám chữa bệnh" });
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo lợi nhuận thuốc", ID = 586, Loai = "Khám bệnh|Điều trị - Viện phí", _Public = true, MaBV = "24297", Kieu = "Report", Nhom = "Tổng hợp" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo kê thuốc của bác sĩ", ID = 588, Loai = "Khoa dược", _Public = true, MaBV = "24297", Kieu = "Report", Nhom = "Dược" });//Linhvq
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Cấp lại giấy ra viện", ID = 589, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Tổng hợp" });// Hoalv
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo mới", ID = 590, Loai = "Tổng hợp", _Public = false, MaBV = "30009", Kieu = "Report", Nhom = "Tổng hợp" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Khám Bệnh Theo Khoa Phòng", ID = 592, Loai = "Tổng hợp", _Public = true, MaBV = "24012", Kieu = "Report", Nhom = "Tổng hợp" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo Hoạt Động", ID = 593, Loai = "Tổng hợp", _Public = true, MaBV = "24012", Kieu = "Report", Nhom = "Tổng hợp" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sử dụng CCCD/BHYT", ID = 594, Loai = "BHYT", _Public = true, MaBV = "", Kieu = "Report", Nhom = "QĐ: 1399/QĐ-BHXH" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ cận lâm sàng", ID = 595, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Sổ thủ thuật", ID = 596, Loai = "CLS", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Cận lâm sàng" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Báo cáo sổ tiếp đón bệnh nhân", ID = 597, Loai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "Report", Nhom = "Viện phí" });//longtp
            _lm.Add(new Library_CLS.Lis_His.limenu { TenBC = "Bảng kê thu tiền bệnh nhân cùng chi trả BHYT", ID = 598, Loai = "BHYT", _Public = true, MaBV = "24272", Kieu = "Report", Nhom = "BHYT" });//longtp

            return _lm;
        }
        #endregion

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void us_menubc_Load(object sender, EventArgs e)
        {
            groupControl1.Text = !string.IsNullOrWhiteSpace(tenLoaiBaoCao) ? tenLoaiBaoCao : _ploaimenu;
            //// tao treelist
            //tre_Menu.Nodes.Add("dsd");
            //TreeListNode codeNode = tre_Menu.AppendNode(null, null);
            //codeNode.SetValue("name", "Ngay");
            //TreeListNode ngayNode = tre_Menu.AppendNode(null, null);
            //ngayNode.SetValue("name", "Ngay2");
            //TreeListNode nhaCCNode = tre_Menu.AppendNode(null, null);
            //nhaCCNode.SetValue("name", "NhaCC");
            //TreeListNode childNode = null;
            //childNode = tre_Menu.AppendNode(null, codeNode);
            //childNode.SetValue("name", " ten 1");
            //// ket treelist
            List<Library_CLS.Lis_His.limenu> _lm = SetDM();
            string[] _Nhom = new string[5] { "", "", "", "", "" };
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<MenuBC> menu_sql = new List<MenuBC>();
            menu_sql = _data.MenuBCs.ToList();
            int luu = 0;
            foreach (var a in menu_sql)
            {
                if (a.Nhom == null)
                {
                    a.Nhom = "";
                    luu++;
                }
                if (a.Loai == null)
                {
                    a.Loai = "";
                    luu++;
                }
                if (luu > 0)
                    _data.SaveChanges();
            }
            foreach (var a in _lm)
            {

                if (menu_sql.Where(p => p.ID == a.ID).ToList().Count <= 0)
                {
                    if (a.Kieu == "Report")
                    {
                        MenuBC moi = new MenuBC();
                        moi.TenBC = a.TenBC;
                        moi.ID = a.ID;
                        if (a.Nhom != null)
                            moi.Nhom = a.Nhom;
                        else
                            moi.Nhom = "";
                        if (a.Loai != null)
                            moi.Loai = a.Loai;
                        else
                            moi.Loai = "";
                        if (a._Public)
                            moi.Status = true;
                        else
                            if (a.MaBV == DungChung.Bien.MaBV)
                            moi.Status = true;
                        else
                            moi.Status = false;
                        _data.MenuBCs.Add(moi);
                        _data.SaveChanges();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(menu_sql.Where(p => p.ID == a.ID).ToList().First().Nhom))
                    {
                        if (a.Nhom != null)
                        {
                            var mnu = _data.MenuBCs.Single(p => p.ID == a.ID);
                            mnu.Nhom = a.Nhom;
                            _data.SaveChanges();
                        }
                    }
                }

            }
            List<MenuBC> menu_sql_s = new List<MenuBC>();
            foreach (var item in menu_sql)
            {
                if(DungChung.Ham.checkQuyenFalse(item.TenBC)[3] == true)
                if (_lm.Where(p => p.ID == item.ID).ToList().Count > 0)
                    menu_sql_s.Add(item);

            }
            grcMenu.DataSource = menu_sql_s.Where(p => p.Loai.Contains(_ploaimenu) && p.Nhom.Contains(_kieu) && p.Status == _public).OrderBy(p => p.TenBC).ToList();
        }

        private void grvMenu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int id = 0;
            if (grvMenu.GetFocusedRowCellValue(colID) != null)
                id = Convert.ToInt32(grvMenu.GetFocusedRowCellValue(colID));
            switch (id)
            {
                case 1:
                    FormThamSo.frmTsThBNNoiTruTheoKhoa_HA frm = new frmTsThBNNoiTruTheoKhoa_HA();
                    frm.ShowDialog();
                    break;
                case 2:
                    FormThamSo.frmTsBcNoiTruThangct_HA frm2 = new frmTsBcNoiTruThangct_HA();
                    frm2.ShowDialog();
                    break;
                case 3:
                    FormThamSo.frmTsBcNoiTruThangchuyenkhoa_HA frm3 = new frmTsBcNoiTruThangchuyenkhoa_HA();
                    frm3.ShowDialog();
                    break;
                case 4:
                    FormThamSo.frmTsTkDoiTuongHNTEct_HA08 fr4 = new frmTsTkDoiTuongHNTEct_HA08();
                    fr4.ShowDialog();
                    break;
                case 5:
                    FormThamSo.frm_rep79aCT frm5 = new frm_rep79aCT();
                    frm5.ShowDialog();
                    break;
                case 6:
                    FormThamSo.frm_80aHD frm6 = new frm_80aHD();
                    frm6.ShowDialog();
                    break;
                case 7:
                    FormThamSo.frm_rep79aTH frm7 = new frm_rep79aTH();
                    frm7.ShowDialog();
                    break;
                case 8:
                    FormThamSo.frm_80aHDTH fr8 = new frm_80aHDTH();
                    fr8.ShowDialog(); // mẫu 80b
                    break;
                case 9:
                    FormThamSo.frmTsBcMau20 frm9 = new frmTsBcMau20();
                    frm9.ShowDialog();
                    break;
                case 10:
                    FormThamSo.frmTsBcMau21BHYT frm10 = new frmTsBcMau21BHYT();
                    frm10.ShowDialog();
                    break;
                //case 11:
                //    FormThamSo.frm_14a frm11 = new frm_14a();
                //    frm11.ShowDialog();
                //    break;
                case 12:
                    FormThamSo.frm14 frm12 = new frm14();
                    frm12.ShowDialog();
                    break;
                case 13:
                    FormThamSo.Frm_10_11ct frm13 = new Frm_10_11ct();
                    frm13.ShowDialog();
                    break;
                case 14:
                    FormThamSo.Frm_11TH frm14 = new Frm_11TH();
                    frm14.ShowDialog();
                    break;
                case 15:
                    FormThamSo.FRM_10TH frm15 = new FRM_10TH();
                    frm15.ShowDialog();
                    break;
                //case 16:
                //    FormThamSo.frm_79bHDDD frm16 = new frm_79bHDDD();
                //    frm16.ShowDialog();
                //    break;
                case 17:
                    FormThamSo.frm_80bHDDD frm17 = new frm_80bHDDD();
                    frm17.ShowDialog();
                    break;
                case 18:
                    FormThamSo.frm_79bHDDD_BHYT frm18 = new frm_79bHDDD_BHYT();
                    frm18.ShowDialog();
                    break;
                case 19:
                    FormThamSo.frm_80bHDDD_BHYT frm19 = new frm_80bHDDD_BHYT();
                    frm19.ShowDialog();
                    break;
                //case 20:
                //    FormThamSo.frm_14a_BHYT frm20 = new frm_14a_BHYT();
                //    frm20.ShowDialog();
                //    break;
                //case 21:
                //    FormThamSo.frm_14b_BHYT frm21 = new frm_14b_BHYT();
                //    frm21.ShowDialog();
                //    break;
                case 22:
                    FormThamSo.frm_TKTamThu frm22 = new frm_TKTamThu();
                    frm22.ShowDialog();
                    break;
                case 23:
                    FormThamSo.frmCPChuaTT frm23 = new frmCPChuaTT();
                    frm23.ShowDialog();
                    break;
                case 24:
                    FormThamSo.Frm_SoVaoVien_YS frm24 = new Frm_SoVaoVien_YS();
                    frm24.ShowDialog();
                    break;
                case 25:
                    FormThamSo.frm_BCTaiNanLaoDong_30010 frm25 = new frm_BCTaiNanLaoDong_30010();
                    frm25.ShowDialog();
                    break;


                case 26:
                    FormThamSo.frm_TonghopDTTheoKhoa frm26 = new frm_TonghopDTTheoKhoa();
                    frm26.ShowDialog();
                    break;
                case 27:
                    FormThamSo.Frm_BcThuVP_VY frm27 = new Frm_BcThuVP_VY();
                    frm27.ShowDialog();
                    break;
                case 28:
                    FormThamSo.frm_CTTonThucSDNoiTru frm28 = new frm_CTTonThucSDNoiTru();
                    frm28.ShowDialog();
                    break;
                case 29:
                    FormThamSo.frmTsBcMau19_20_21_1399 frm29 = new frmTsBcMau19_20_21_1399();
                    frm29.ShowDialog();
                    break;
                //case 30:
                //    FormThamSo.frmTsBcMau20_1399 frm30 = new frmTsBcMau20_1399();
                //    frm30.ShowDialog();
                //    break;
                case 31:
                    FormThamSo.Frm_BCBNKDT frm31 = new Frm_BCBNKDT();
                    frm31.ShowDialog();
                    break;
                //case 32:
                //    FormThamSo.frm_rep79aCT_1399_SP frm32 = new FormThamSo.frm_rep79aCT_1399_SP();
                //    frm32.ShowDialog();
                //    break;
                //case 33:
                //    FormThamSo.frmTsBcMau19_20_1399 frm33 = new FormThamSo.frmTsBcMau19_20_1399();
                //    frm33.ShowDialog();
                //    break;
                //case 34:
                //    FormThamSo.frmTsBcMau21BHYT_1399_SP frm34 = new FormThamSo.frmTsBcMau21BHYT_1399_SP();
                //    frm34.ShowDialog();
                //    break;
                //case 35:
                //    FormThamSo.frm_80aHD_1399_SP frm35 = new FormThamSo.frm_80aHD_1399_SP();
                //    frm35.ShowDialog();
                //    break;
                //case 36:
                //    FormThamSo.frm_14a_Moi frm36 = new FormThamSo.frm_14a_Moi();
                //    frm36.ShowDialog();
                //    break;
                //case 37:
                //    FormThamSo.frm_14b_Moi frm37 = new FormThamSo.frm_14b_Moi();
                //    frm37.ShowDialog();
                //    break;
                //case 38:
                //    FormThamSo.frmTsBcMau19_20_1399 frm38 = new FormThamSo.frmTsBcMau19_20_1399(19);
                //    frm38.ShowDialog();
                //    break;
                case 39:
                    FormThamSo.Frm_BcChiPhiKCB frm39 = new FormThamSo.Frm_BcChiPhiKCB();
                    frm39.ShowDialog();
                    break;
                case 40:
                    FormThamSo.Frm_BcPhauThuat_CL frm40 = new FormThamSo.Frm_BcPhauThuat_CL();
                    frm40.ShowDialog();
                    break;

                case 42:
                    FormThamSo.Frm_BbKiemKie_CL frm42 = new FormThamSo.Frm_BbKiemKie_CL();
                    frm42.ShowDialog();
                    break;
                case 43:
                    FormThamSo.Frm_THChuyenden frm43 = new FormThamSo.Frm_THChuyenden();
                    frm43.ShowDialog();
                    break;
                case 44:
                    FormThamSo.frm_BaoCaoNBChuyendi frm44 = new FormThamSo.frm_BaoCaoNBChuyendi();
                    frm44.ShowDialog();
                    break;
                case 45:
                    FormThamSo.Frm_BcKhamBenh_CL frm45 = new FormThamSo.Frm_BcKhamBenh_CL();
                    frm45.ShowDialog();
                    break;
                case 46:
                    FormThamSo.Frm_BCSudungthuoctaikhoaDT frm46 = new FormThamSo.Frm_BCSudungthuoctaikhoaDT();
                    frm46.ShowDialog();
                    break;
                case 47:
                    FormThamSo.Frm_BcTaiNan frm47 = new FormThamSo.Frm_BcTaiNan();
                    frm47.ShowDialog();
                    break;
                case 48:
                    FormThamSo.Frm_BcKhamChuaBenh_CL frm48 = new FormThamSo.Frm_BcKhamChuaBenh_CL();
                    frm48.ShowDialog();
                    break;
                case 49:
                    FormThamSo.Frm_ThTTNhanTuyen frm49 = new FormThamSo.Frm_ThTTNhanTuyen();
                    frm49.ShowDialog();
                    break;
                case 50:
                    FormThamSo.frm_TKBNVaoChuyenVien frm50 = new frm_TKBNVaoChuyenVien();
                    frm50.ShowDialog();
                    break;
                case 51:
                    FormThamSo.frm_TKBNTTkoXuatDuoc frm51 = new frm_TKBNTTkoXuatDuoc();
                    frm51.ShowDialog();
                    break;
                case 52:
                    FormThamSo.Frm_SoThuTienKSK_PH01 frm52 = new Frm_SoThuTienKSK_PH01();
                    frm52.ShowDialog();
                    break;
                case 53:
                    FormThamSo.Frm_ThCPKCBTEKoThe_HL02 frm53 = new Frm_ThCPKCBTEKoThe_HL02();
                    frm53.ShowDialog();
                    break;
                case 54:
                    FormThamSo.Frm_BcHoatDongKSK_VY02 frm54 = new Frm_BcHoatDongKSK_VY02();
                    frm54.ShowDialog();
                    break;
                case 55:
                    FormThamSo.Frm_BcHoatDongKKB_HL01 frm55 = new Frm_BcHoatDongKKB_HL01();
                    frm55.ShowDialog();
                    break;
                case 56:
                    FormThamSo.frm_TKDSBN frm56 = new frm_TKDSBN();
                    frm56.ShowDialog();
                    break;
                case 57:
                    FormThamSo.frm_Yenson_tonghop frm57 = new frm_Yenson_tonghop();
                    frm57.ShowDialog();
                    break;
                case 58:
                    FormThamSo.Frm_TkSoLieuKCB_YSon02 frm58 = new Frm_TkSoLieuKCB_YSon02();
                    frm58.ShowDialog();
                    break;
                case 59:
                    FormThamSo.Frm_Sokhambenh_new frm59 = new Frm_Sokhambenh_new();
                    frm59.ShowDialog();
                    break;
                case 60:
                    // MessageBox.Show("mẫu biểu đang nâng cấp, số liệu có thể chưa chính xác");
                    FormThamSo.Frm_SoVaoRaChuyenVien frm60 = new Frm_SoVaoRaChuyenVien();
                    frm60.ShowDialog();
                    break;
                case 61:
                    FormThamSo.Frm_BcTKSoLuotBNKNgT_HL03 frm61 = new Frm_BcTKSoLuotBNKNgT_HL03();
                    frm61.ShowDialog();
                    break;
                case 62:
                    FormThamSo.Frm_HdKCBPhongKham_TK06 frm62 = new Frm_HdKCBPhongKham_TK06();
                    frm62.ShowDialog();
                    break;
                case 63:
                    FormThamSo.Frm_KcBenhTatNL_TK04 frm63 = new Frm_KcBenhTatNL_TK04();
                    frm63.ShowDialog();
                    break;
                case 64:
                    FormThamSo.Frm_KcBenhTatTE_TK05 frm64 = new Frm_KcBenhTatTE_TK05();
                    frm64.ShowDialog();
                    break;
                case 65:
                    FormThamSo.Frm_DsNopVP_TK01 frm65 = new Frm_DsNopVP_TK01();
                    frm65.ShowDialog();
                    break;
                case 66:
                    FormThamSo.Frm_DsNopVP_TK02 frm66 = new Frm_DsNopVP_TK02();
                    frm66.ShowDialog();
                    break;
                case 67:
                    FormThamSo.Frm_DsNopVP_TK03 frm67 = new Frm_DsNopVP_TK03();
                    frm67.ShowDialog();
                    break;
                case 68:
                    FormThamSo.frm_BkeThuTienVP frm68 = new frm_BkeThuTienVP();
                    frm68.ShowDialog();
                    break;
                case 69:

                    FormThamSo.frm_BkeVienPhi frm69 = new frm_BkeVienPhi();
                    frm69.ShowDialog();
                    break;
                case 70:
                    FormThamSo.Frm_bcvienphiBHYT frm70 = new Frm_bcvienphiBHYT();
                    frm70.ShowDialog();
                    break;
                case 71:
                    FormThamSo.Frm_bcvttKSK frm71 = new Frm_bcvttKSK();
                    frm71.ShowDialog();
                    break;
                case 72:
                    FormThamSo.Frm_SoThuChiTU_BG frm72 = new Frm_SoThuChiTU_BG();
                    frm72.ShowDialog();
                    break;
                case 73:
                    FormThamSo.frm_BaKeCT_TamThuVPhi_ThanhHa frm73 = new frm_BaKeCT_TamThuVPhi_ThanhHa();
                    frm73.ShowDialog();
                    break;
                case 74:
                    FormThamSo.frm_BCGiaoBanHangNgay_01071 frm74 = new frm_BCGiaoBanHangNgay_01071();
                    frm74.ShowDialog();
                    break;
                case 75:
                    FormThamSo.frm_BCDSTEDuoi6TuoiKCBKHongSDThe frm75 = new frm_BCDSTEDuoi6TuoiKCBKHongSDThe();
                    frm75.ShowDialog();
                    break;

                case 76:
                    FormThamSo.frm_DSBenhNhanCC_01071 frm76 = new frm_DSBenhNhanCC_01071();
                    frm76.ShowDialog();
                    break;
                case 77:
                    FormThamSo.Frm_ThTTChuyenTuyen_YS frm77 = new Frm_ThTTChuyenTuyen_YS();
                    frm77.ShowDialog();
                    break;
                case 78:
                    FormThamSo.Frm_TkCPVanChuyenBHYT_YS frm78 = new Frm_TkCPVanChuyenBHYT_YS();
                    frm78.ShowDialog();
                    break;
                case 79:
                    FormThamSo.Frm_NopTienVaoQuy_GLoc frm79 = new Frm_NopTienVaoQuy_GLoc();
                    frm79.ShowDialog();
                    break;
                case 80:
                    FormThamSo.Frm_BieuCongKhaiThuocHangNgay frm80 = new Frm_BieuCongKhaiThuocHangNgay();
                    frm80.ShowDialog();
                    break;
                case 81:
                    FormThamSo.Frm_ThongkeCPTheoKP frm81 = new Frm_ThongkeCPTheoKP();
                    frm81.ShowDialog();
                    break;
                case 82:
                    FormThamSo.Frm_DsThThuTienBHYT_TY01 frm82 = new Frm_DsThThuTienBHYT_TY01();
                    frm82.ShowDialog();
                    break;
                case 83:
                    FormThamSo.Frm_BkChiTietPhauThuat_BG01 frm83 = new Frm_BkChiTietPhauThuat_BG01();
                    frm83.ShowDialog();
                    break;
                case 84:
                    FormThamSo.Frm_BkChiTietThuVP_BG06 frm84 = new Frm_BkChiTietThuVP_BG06();
                    frm84.ShowDialog();
                    break;
                case 85:
                    FormThamSo.Frm_BkVienPhiTheoKhoa_BG_04 frm85 = new Frm_BkVienPhiTheoKhoa_BG_04();
                    frm85.ShowDialog();
                    break;
                case 86:
                    FormThamSo.Frm_ThThuVienPhiS_BG07 frm86 = new Frm_ThThuVienPhiS_BG07();
                    frm86.ShowDialog();
                    break;
                case 87:
                    FormThamSo.Frm_ThVienPhi_BG05 frm87 = new Frm_ThVienPhi_BG05();
                    frm87.ShowDialog();
                    break;
                case 88:
                    FormThamSo.Frm_ThVPTUTTT_BG02 frm88 = new Frm_ThVPTUTTT_BG02();
                    frm88.ShowDialog();
                    break;
                case 89:
                    FormThamSo.Frm_BcTinhHinhBenhTatCD10_VY frm89 = new Frm_BcTinhHinhBenhTatCD10_VY();
                    frm89.ShowDialog();
                    break;
                case 90:
                    FormThamSo.Frm_BcHoatDongKCB_VY01 frm90 = new Frm_BcHoatDongKCB_VY01();
                    frm90.ShowDialog();
                    break;
                case 91:
                    //FormThamSo.frm_rep79aCT_1399 frm91 = new frm_rep79aCT_1399();
                    //frm91.ShowDialog();
                    break;
                case 92:

                    FormThamSo.frm_79_80aHD_1399 frm92 = new frm_79_80aHD_1399();
                    frm92.ShowDialog();
                    break;
                case 93:
                    FormThamSo.Frm_TKChiPhiKCBBHYT frm93 = new Frm_TKChiPhiKCBBHYT();
                    frm93.ShowDialog();
                    break;
                case 94:
                    FormThamSo.frm_ChamCongPhauThuat frm94 = new frm_ChamCongPhauThuat();
                    frm94.ShowDialog();
                    break;
                case 95:
                    FormThamSo.Frm_DsTonGoiThuoc frm95 = new Frm_DsTonGoiThuoc();
                    frm95.ShowDialog();
                    break;
                case 96:
                    FormThamSo.frm_rep79aCT_1399_BHYT frm96 = new frm_rep79aCT_1399_BHYT();
                    frm96.ShowDialog();
                    break;
                case 97:
                    FormThamSo.frm_80aHD_1399_BHYT frm97 = new frm_80aHD_1399_BHYT();
                    frm97.ShowDialog();
                    break;
                case 98:
                    FormThamSo.Frm_BKThuocDV frm98 = new Frm_BKThuocDV();
                    frm98.ShowDialog();
                    break;
                case 99:
                    FormThamSo.Frm_BcKKB_CL frm99 = new Frm_BcKKB_CL();
                    frm99.ShowDialog();
                    break;
                case 100:
                    FormThamSo.Frm_BcCongTacKB_NH frm100 = new Frm_BcCongTacKB_NH();
                    frm100.ShowDialog();
                    break;
                case 101:
                    FormThamSo.frmTsBCNXT frm101 = new frmTsBCNXT();
                    frm101.ShowDialog();
                    break;
                //case 102:
                //    FormThamSo.frmTsBcNXTrutgon frm102 = new frmTsBcNXTrutgon();
                //    frm102.ShowDialog();
                //    break;
                case 103:
                    //FormThamSo.frmTsBcNXTXuat frm103 = new frmTsBcNXTXuat();
                    //frm103.ShowDialog();
                    break;
                case 104:
                    FormThamSo.frm_benhnhanxuatduoc frm104 = new frm_benhnhanxuatduoc();
                    frm104.ShowDialog();
                    break;
                case 105:

                    FormThamSo.frmTsBcSuDungThuoc frm105 = new frmTsBcSuDungThuoc();
                    frm105.ShowDialog();
                    break;
                case 106:
                    FormThamSo.Frm_BcHangXuatTheoDT_NS frm106 = new Frm_BcHangXuatTheoDT_NS();
                    frm106.ShowDialog();
                    break;
                case 107:
                    FormNhap.frmSoXNT_GayNghien_HTT_PhongXa frm107 = new FormNhap.frmSoXNT_GayNghien_HTT_PhongXa();
                    frm107.ShowDialog();
                    break;
                case 108:
                    FormNhap.frm_TheKho frm108 = new FormNhap.frm_TheKho();
                    frm108.ShowDialog();
                    break;
                case 109:
                    FormThamSo.frmTsBbKKThuoc frm109 = new frmTsBbKKThuoc();
                    frm109.ShowDialog();
                    break;
                case 110:
                    FormThamSo.frm_BcVTYTKoTT frm110 = new frm_BcVTYTKoTT();
                    frm110.ShowDialog();
                    break;
                case 111:
                    FormNhap.frmSoThekho frm111 = new FormNhap.frmSoThekho();
                    frm111.ShowDialog();
                    break;
                case 112:
                    FormThamSo.frmTk15NgaySDThuoc_NB01 frm112 = new FormThamSo.frmTk15NgaySDThuoc_NB01();
                    frm112.ShowDialog();
                    break;
                case 113:
                    FormThamSo.frmTsSoKiemNhap frm113 = new FormThamSo.frmTsSoKiemNhap();
                    frm113.ShowDialog();
                    break;
                case 114:
                    //FormThamSo.Frm_BcNXTToanTT_CM09 frm114 = new FormThamSo.Frm_BcNXTToanTT_CM09();
                    //frm114.ShowDialog();
                    break;
                case 115:
                    FormThamSo.frm_06 frm115 = new FormThamSo.frm_06();
                    frm115.ShowDialog();
                    break;
                case 116:
                    //FormThamSo.frm_BcNXT_CM05 frm116 = new FormThamSo.frm_BcNXT_CM05();
                    //frm116.ShowDialog();
                    break;
                case 117:
                    //FormThamSo.Frm_BcNXTTong_CM10 frm117 = new FormThamSo.Frm_BcNXTTong_CM10();
                    //frm117.ShowDialog();
                    break;
                case 118:
                    FormThamSo.frm_BbKiemNhap frm118 = new FormThamSo.frm_BbKiemNhap();
                    frm118.ShowDialog();
                    break;
                case 119:
                    FormThamSo.Frm_BcNXTTheoXa_CM08 frm119 = new FormThamSo.Frm_BcNXTTheoXa_CM08();
                    frm119.ShowDialog();
                    break;
                case 120:
                    FormThamSo.frm_BcCTKhoaDuoc frm120 = new FormThamSo.frm_BcCTKhoaDuoc();
                    frm120.ShowDialog();
                    break;
                case 121:
                    FormThamSo.Frm_BcNXTThuocYHCT frm121 = new FormThamSo.Frm_BcNXTThuocYHCT();
                    frm121.ShowDialog();
                    break;
                case 122:
                    FormThamSo.frm_THNhapDuoc frm122 = new FormThamSo.frm_THNhapDuoc();
                    frm122.ShowDialog();
                    break;
                case 123:
                    FormThamSo.frm_THXuatDuoc frm123 = new FormThamSo.frm_THXuatDuoc();
                    frm123.ShowDialog();
                    break;
                case 124:
                    FormThamSo.Frm_BkXuatDuoc_VY01 frm124 = new FormThamSo.Frm_BkXuatDuoc_VY01();
                    frm124.ShowDialog();
                    break;
                case 125:
                    FormThamSo.frm_DoiChieuThuocTheoNgay frm125 = new FormThamSo.frm_DoiChieuThuocTheoNgay();
                    frm125.ShowDialog();
                    break;
                case 126:
                    FormThamSo.Frm_BcNXTTuTruc frm126 = new FormThamSo.Frm_BcNXTTuTruc();
                    frm126.ShowDialog();
                    break;
                case 127:
                    FormThamSo.Frm_BcSuDungDuoc_BG frm127 = new FormThamSo.Frm_BcSuDungDuoc_BG();
                    frm127.ShowDialog();
                    break;
                case 128:
                    //cauvithao
                    //FormThamSo.frmTsBcNXTXuat_BG frm128 = new FormThamSo.frmTsBcNXTXuat_BG();
                    //frm128.ShowDialog();
                    break;
                case 129:
                    //cauvithao
                    //FormThamSo.Frm_BkTonD frm129 = new FormThamSo.Frm_BkTonD();
                    //frm129.ShowDialog();
                    break;
                case 130:
                    //cauvithao
                    FormThamSo.Frm_BcSuDungThuoc_TDuong frm130 = new FormThamSo.Frm_BcSuDungThuoc_TDuong();
                    frm130.ShowDialog();
                    break;
                case 131:
                    FormThamSo.Frm_SoTheoDoiNhapXuatThuocGNHTT frm131 = new FormThamSo.Frm_SoTheoDoiNhapXuatThuocGNHTT();
                    frm131.ShowDialog();
                    break;
                case 132:
                    FormThamSo.Frm_BcNXT_TH frm132 = new FormThamSo.Frm_BcNXT_TH();
                    frm132.ShowDialog();
                    break;
                case 133:
                    FormThamSo.frm_HangNhap_NCC frm133 = new FormThamSo.frm_HangNhap_NCC();
                    frm133.ShowDialog();
                    break;
                case 134:
                    FormThamSo.frm_THChungTuMuaThuoc frm134 = new FormThamSo.frm_THChungTuMuaThuoc();
                    frm134.ShowDialog();
                    break;
                case 135:
                    FormThamSo.frm_TKNhapCTLoaiThuoc frm135 = new FormThamSo.frm_TKNhapCTLoaiThuoc();
                    frm135.ShowDialog();
                    break;
                case 136:
                    FormThamSo.frmTsBCNXTHH_NT frm136 = new FormThamSo.frmTsBCNXTHH_NT();
                    frm136.ShowDialog();
                    break;
                //case 137:
                //    FormThamSo.frmTsBCNXT_MoiHT frm137 = new FormThamSo.frmTsBCNXT_MoiHT();
                //    frm137.ShowDialog();
                //    break;
                case 138:
                    FormThamSo.Frm_NXTThuoc_TDuong_TH frm138 = new FormThamSo.Frm_NXTThuoc_TDuong_TH();
                    frm138.ShowDialog();
                    break;
                case 139:
                    //cauvithao
                    //FormThamSo.Frm_BcXuatTheoPLoai frm139 = new FormThamSo.Frm_BcXuatTheoPLoai();
                    //frm139.ShowDialog();
                    break;
                case 140:
                    FormThamSo.Frm_DsXuatDuocTheoDT frm140 = new FormThamSo.Frm_DsXuatDuocTheoDT();
                    frm140.ShowDialog();
                    break;
                case 141:
                    FormThamSo.Frm_BcTonDuoc_BG frm141 = new FormThamSo.Frm_BcTonDuoc_BG();
                    frm141.ShowDialog();
                    break;
                case 142:
                    FormThamSo.Frm_baocaoduoctheonhomthuoc frm142 = new FormThamSo.Frm_baocaoduoctheonhomthuoc();
                    frm142.ShowDialog();
                    break;
                case 143:
                    FormThamSo.Frm_BcKKvaSDThuoc_BLac frm143 = new FormThamSo.Frm_BcKKvaSDThuoc_BLac();
                    frm143.ShowDialog();
                    break;
                case 144:
                    FormThamSo.frmTsBCNXT_24009 frm144 = new frmTsBCNXT_24009();
                    frm144.ShowDialog();
                    break;
                case 145:
                    FormThamSo.frmTsBCNXT_24009_N frm145 = new frmTsBCNXT_24009_N();
                    frm145.ShowDialog();
                    break;
                case 146:
                    FormThamSo.frm_BCNXT_30005 frm146 = new frm_BCNXT_30005();
                    frm146.ShowDialog();
                    break;
                case 147:
                    FormThamSo.frm_BcNXTSD_30002 frm147 = new frm_BcNXTSD_30002();
                    frm147.ShowDialog();
                    break;
                case 148:
                    FormThamSo.frm_BCTonKhoThuocGNHTT_30002 frm148 = new frm_BCTonKhoThuocGNHTT_30002();
                    frm148.ShowDialog();
                    break;
                case 149:
                    FormThamSo.frm_BC_XuatDuoc frm149 = new frm_BC_XuatDuoc();
                    frm149.ShowDialog();
                    break;
                case 150:
                    FormThamSo.frm_BC_BBKiemKe_30005 frm150 = new frm_BC_BBKiemKe_30005();
                    frm150.ShowDialog();
                    break;
                case 151:
                    FormThamSo.frm_BC_KiemKeThuoc_30007 frm151 = new frm_BC_KiemKeThuoc_30007();
                    frm151.ShowDialog();
                    break;
                case 152:
                    FormThamSo.frm_PhieuTongXuat_12122 frm152 = new frm_PhieuTongXuat_12122();
                    frm152.ShowDialog();
                    break;
                case 153:
                    FormThamSo.Frm_BcNXT_12122 frm153 = new Frm_BcNXT_12122();
                    frm153.ShowDialog();
                    break;
                case 154:
                    FormThamSo.frm_BC_SuDungThuocCacXa_30009 frm154 = new frm_BC_SuDungThuocCacXa_30009();
                    frm154.ShowDialog();
                    break;
                case 155:
                    FormThamSo.frm_BC_TKThuocTTBHYT_30009 frm155 = new frm_BC_TKThuocTTBHYT_30009();
                    frm155.ShowDialog();
                    break;
                case 156:
                    FormThamSo.frm_BC_NXT_04011 frm156 = new frm_BC_NXT_04011();
                    frm156.ShowDialog();
                    break;
                case 157:
                    FormThamSo.frm_BangTHCongNo_12121 frm157 = new frm_BangTHCongNo_12121();
                    frm157.ShowDialog();
                    break;
                case 158:
                    FormThamSo.frm_BC_CTNhapThuoc_VTYT_30009 frm158 = new frm_BC_CTNhapThuoc_VTYT_30009();
                    frm158.ShowDialog();
                    break;
                case 159:
                    FormThamSo.frm_BC_XuatKhangSinh frm159 = new frm_BC_XuatKhangSinh();
                    frm159.ShowDialog();
                    break;
                case 160:
                    FormThamSo.frm_TinhHinhSDThuoc_30007 frm160 = new frm_TinhHinhSDThuoc_30007();
                    frm160.ShowDialog();
                    break;
                case 161:
                    FormThamSo.frm_BC_PhieuXuat_27022 frm161 = new frm_BC_PhieuXuat_27022();
                    frm161.ShowDialog();
                    break;
                case 162:
                    FormThamSo.frm_NXT_SanXuatThuoc frm162 = new frm_NXT_SanXuatThuoc();
                    frm162.ShowDialog();
                    break;
                case 163:
                    FormThamSo.frm_BCNXT_VAT frm163 = new frm_BCNXT_VAT();
                    frm163.ShowDialog();
                    break;

                case 164:
                    FormThamSo.frm_CanhBaoThuocSLHDmin frm164 = new frm_CanhBaoThuocSLHDmin();
                    frm164.ShowDialog();
                    break;

                case 165:
                    FormThamSo.frm_BCHX_27022 frm165 = new frm_BCHX_27022();
                    frm165.ShowDialog();
                    break;
                case 166:
                    FormThamSo.frm_SoKiemNhapYHCT frm166 = new frm_SoKiemNhapYHCT();
                    frm166.ShowDialog();
                    break;
                case 167:
                    FormThamSo.frm_BCSDThuocBV frm167 = new frm_BCSDThuocBV();
                    frm167.ShowDialog();
                    break;
                case 168:
                    FormThamSo.frm_BCTHThuocLinhKhoa_TuTruccs frm168 = new frm_BCTHThuocLinhKhoa_TuTruccs();
                    frm168.ShowDialog();
                    break;
                case 169:
                    FormThamSo.frm_BCLinhTongHop frm169 = new frm_BCLinhTongHop();
                    frm169.ShowDialog();
                    break;
                case 170:
                    FormThamSo.frm_TonDuoc_BìnhGiang frm170 = new frm_TonDuoc_BìnhGiang();
                    frm170.ShowDialog();
                    break;
                case 171:
                    FormThamSo.frm_PTDChatLuongThuoc frm171 = new frm_PTDChatLuongThuoc();
                    frm171.ShowDialog();
                    break;
                case 172:
                    FormThamSo.frm_NXT_01071 frm172 = new frm_NXT_01071();
                    frm172.ShowDialog();
                    break;
                case 173:
                    FormThamSo.frm_BC_KCB_30003 frm173 = new frm_BC_KCB_30003();
                    frm173.ShowDialog();
                    break;


                case 200:
                    FormThamSo.Frm_BcHoatDongKKB_TH01 frm200 = new FormThamSo.Frm_BcHoatDongKKB_TH01();
                    frm200.ShowDialog();
                    break;
                case 201:
                    FormThamSo.Frm_BcHoatDongDTri_TH04 frm201 = new FormThamSo.Frm_BcHoatDongDTri_TH04();
                    frm201.ShowDialog();
                    break;
                case 202:
                    FormThamSo.Frm_BcHoatDongDTri_CL frm202 = new FormThamSo.Frm_BcHoatDongDTri_CL();
                    frm202.ShowDialog();
                    break;
                case 203:
                    FormThamSo.Frm_BcHoatDongKKB_CL frm203 = new FormThamSo.Frm_BcHoatDongKKB_CL();
                    frm203.ShowDialog();
                    break;
                case 204:
                    FormThamSo.Frm_BcKhoaKB_BLac frm204 = new FormThamSo.Frm_BcKhoaKB_BLac();
                    frm204.ShowDialog();
                    break;
                case 205:
                    FormThamSo.Frm_BcKCBNguoiCaoTuoi_BLac frm205 = new FormThamSo.Frm_BcKCBNguoiCaoTuoi_BLac();
                    frm205.ShowDialog();
                    break;
                case 206:
                    FormThamSo.Frm_BcCongTacKCB_BLac frm206 = new FormThamSo.Frm_BcCongTacKCB_BLac();
                    frm206.ShowDialog();
                    break;
                case 207:
                    FormThamSo.Frm_BcThSoLuotBN_BLac frm207 = new FormThamSo.Frm_BcThSoLuotBN_BLac();
                    frm207.ShowDialog();
                    break;
                case 208:
                    FormThamSo.From_Thongkethoigiankhambenh frm208 = new FormThamSo.From_Thongkethoigiankhambenh();
                    frm208.ShowDialog();
                    break;
                case 209:
                    FormThamSo.Frm_BcCongTacKCB_BLam frm209 = new FormThamSo.Frm_BcCongTacKCB_BLam();
                    frm209.ShowDialog();
                    break;
                case 210:
                    FormThamSo.Frm_BcKCBTe_BLam frm210 = new FormThamSo.Frm_BcKCBTe_BLam();
                    frm210.ShowDialog();
                    break;
                case 211:
                    FormThamSo.frm_HDKCB_24009 frm211 = new FormThamSo.frm_HDKCB_24009();
                    frm211.ShowDialog();
                    break;
                case 212:
                    FormThamSo.frm_BCTH_NoiTru_30007 frm212 = new FormThamSo.frm_BCTH_NoiTru_30007();
                    frm212.ShowDialog();
                    break;
                case 213:
                    FormThamSo.frm_Congtacchuyentuyen frm213 = new FormThamSo.frm_Congtacchuyentuyen();
                    frm213.ShowDialog();
                    break;
                case 214:
                    FormThamSo.frm_BCTaiNanGiaoThong_CapCuu frm214 = new FormThamSo.frm_BCTaiNanGiaoThong_CapCuu();
                    frm214.ShowDialog();
                    break;
                case 215:
                    FormThamSo.Frm_BcHoatDongDTri_24009 frm215 = new FormThamSo.Frm_BcHoatDongDTri_24009();
                    frm215.ShowDialog();
                    break;
                case 216:
                    FormThamSo.frm_THHoatDongChuyenMon frm216 = new FormThamSo.frm_THHoatDongChuyenMon();
                    frm216.ShowDialog();
                    break;
                case 217:
                    FormThamSo.frm_SoThuThuat_12121 frm217 = new FormThamSo.frm_SoThuThuat_12121();
                    frm217.ShowDialog();
                    break;
                case 218:
                    FormThamSo.frm_KhamChuaBenh_BVYHCT_12121 frm218 = new FormThamSo.frm_KhamChuaBenh_BVYHCT_12121();
                    frm218.ShowDialog();
                    break;
                case 219:
                    FormThamSo.frm_BC_PhanTichBenhTat_30003 frm219 = new FormThamSo.frm_BC_PhanTichBenhTat_30003();
                    frm219.ShowDialog();
                    break;
                case 220:
                    FormThamSo.frm_BC_ChiTieu frm220 = new FormThamSo.frm_BC_ChiTieu();
                    frm220.ShowDialog();
                    break;
                case 221:
                    FormThamSo.frm_BC_TVTaiNanDoThuongTich frm221 = new FormThamSo.frm_BC_TVTaiNanDoThuongTich();
                    frm221.ShowDialog();
                    break;
                case 222:
                    FormThamSo.frm_BC_KhamChuaBenhHangNgay frm222 = new FormThamSo.frm_BC_KhamChuaBenhHangNgay();
                    frm222.ShowDialog();
                    break;
                case 223:
                    FormThamSo.frm_BCSoLieuBvHangNgay_08130 frm223 = new FormThamSo.frm_BCSoLieuBvHangNgay_08130();
                    frm223.ShowDialog();
                    break;
                case 224:
                    FormThamSo.frm_BC_SoLieuChanDoanHinhAnhHangNgay frm224 = new FormThamSo.frm_BC_SoLieuChanDoanHinhAnhHangNgay();
                    frm224.ShowDialog();
                    break;
                case 225:
                    FormThamSo.frm_BC_ThongKeTaiNanThuongTich frm225 = new FormThamSo.frm_BC_ThongKeTaiNanThuongTich();
                    frm225.ShowDialog();
                    break;
                case 226:
                    FormThamSo.frm_BC_ChiTieuChuyenMon_BVDKKimThanh frm226 = new FormThamSo.frm_BC_ChiTieuChuyenMon_BVDKKimThanh();
                    frm226.ShowDialog();
                    break;
                case 227:
                    FormThamSo.frm_BC_ChiSoCongTacKCB_12122 frm227 = new FormThamSo.frm_BC_ChiSoCongTacKCB_12122();
                    frm227.ShowDialog();
                    break;
                case 228:
                    FormThamSo.frm_BC_TongHopKhamBenh_30010 frm228 = new FormThamSo.frm_BC_TongHopKhamBenh_30010();
                    frm228.ShowDialog();
                    break;
                case 229:
                    FormThamSo.frm_BC_SoLuuTruHS_30009 frm229 = new FormThamSo.frm_BC_SoLuuTruHS_30009();
                    frm229.ShowDialog();
                    break;
                case 230:
                    FormThamSo.frm_BC_HDKhamBenh_30012 frm230 = new FormThamSo.frm_BC_HDKhamBenh_30012();
                    frm230.ShowDialog();
                    break;
                case 231:
                    FormThamSo.frm_BC_HDDieuTri_30012 frm231 = new FormThamSo.frm_BC_HDDieuTri_30012();
                    frm231.ShowDialog();
                    break;
                case 232:
                    FormThamSo.frm_BC_DsBNMo_27022 frm232 = new FormThamSo.frm_BC_DsBNMo_27022();
                    frm232.ShowDialog();
                    break;
                case 233:
                    FormThamSo.frm_BC_KQKhamDT_27022 frm233 = new FormThamSo.frm_BC_KQKhamDT_27022();
                    frm233.ShowDialog();
                    break;
                case 234:
                    FormThamSo.frm_BCCM_27022 frm234 = new FormThamSo.frm_BCCM_27022();
                    frm234.ShowDialog();
                    break;
                case 235:
                    FormThamSo.frm_BC_THTheoDoiBNNoiTru_27022 frm235 = new FormThamSo.frm_BC_THTheoDoiBNNoiTru_27022();
                    frm235.ShowDialog();
                    break;
                case 236:
                    FormThamSo.frm_BC_KQHD_27022 frm236 = new FormThamSo.frm_BC_KQHD_27022();
                    frm236.ShowDialog();
                    break;
                case 237:
                    FormThamSo.frm_BC_HDPalTuyenHuyen_30005 frm237 = new FormThamSo.frm_BC_HDPalTuyenHuyen_30005();
                    frm237.ShowDialog();
                    break;

                case 238:
                    FormThamSo.frm_BC_GiaoBanChuyenMon_BacNinh frm238 = new FormThamSo.frm_BC_GiaoBanChuyenMon_BacNinh();
                    frm238.ShowDialog();
                    break;
                case 239:
                    FormThamSo.frm_BC_CongTacKhamChuaBenh_26007 frm239 = new FormThamSo.frm_BC_CongTacKhamChuaBenh_26007();
                    frm239.ShowDialog();
                    break;

                case 240:
                    FormThamSo.frm_BCDieuTriNoiTruTaiKhoaPhong_12122 frm240 = new FormThamSo.frm_BCDieuTriNoiTruTaiKhoaPhong_12122();
                    frm240.ShowDialog();
                    break;

                case 241:
                    FormThamSo.frm_BCCongTacKCB_12122 frm241 = new FormThamSo.frm_BCCongTacKCB_12122();
                    frm241.ShowDialog();
                    break;

                case 242:
                    FormThamSo.frm_SoKhamBenh_27022 frm242 = new FormThamSo.frm_SoKhamBenh_27022();
                    frm242.ShowDialog();
                    break;
                case 243:
                    FormThamSo.frm_BCCongtacKCB_12121 frm243 = new FormThamSo.frm_BCCongtacKCB_12121();
                    frm243.ShowDialog();
                    break;
                case 244:
                    FormThamSo.frm_BC_HoatDongDTNoiTru frm244 = new FormThamSo.frm_BC_HoatDongDTNoiTru();
                    frm244.ShowDialog();
                    break;
                case 245:
                    FormThamSo.frm_BCnghiom frm245 = new FormThamSo.frm_BCnghiom();
                    frm245.ShowDialog();
                    break;

                case 246:
                    FormThamSo.frm_BCTuan frm246 = new FormThamSo.frm_BCTuan();
                    frm246.ShowDialog();
                    break;

                case 247:
                    FormThamSo.frm_KetQuaHDThang_30010 frm247 = new FormThamSo.frm_KetQuaHDThang_30010();
                    frm247.ShowDialog();
                    break;
                case 248:
                    FormThamSo.frm_BCCTKCBNgoaiTru frm248 = new FormThamSo.frm_BCCTKCBNgoaiTru();
                    frm248.ShowDialog();
                    break;

                case 249:
                    FormThamSo.frm_BCTongKetNam_01071 frm249 = new FormThamSo.frm_BCTongKetNam_01071();
                    frm249.ShowDialog();
                    break;
                //case 250:
                //    FormThamSo.Frm_BieuCongKhaiThuocHangNgay frm250 = new FormThamSo.Frm_BieuCongKhaiThuocHangNgay();
                //    frm250.ShowDialog();
                //    break;
                case 251:
                    FormThamSo.frm_BCCuoiNam_KinhMon frm251 = new FormThamSo.frm_BCCuoiNam_KinhMon();
                    frm251.ShowDialog();
                    break;
                case 252:
                    FormThamSo.frm_SoTheoDoi_27022 frm252 = new FormThamSo.frm_SoTheoDoi_27022();
                    frm252.ShowDialog();
                    break;
                case 253:
                    FormThamSo.frm_BCRaVien_BN139 frm253 = new FormThamSo.frm_BCRaVien_BN139();
                    frm253.ShowDialog();
                    break;
                case 254:
                    FormThamSo.frm_BCKhamBenh_TamDuong frm254 = new FormThamSo.frm_BCKhamBenh_TamDuong();
                    frm254.ShowDialog();
                    break;
                case 255:
                    FormThamSo.frm_BCCTTHuocKD frm255 = new FormThamSo.frm_BCCTTHuocKD();
                    frm255.ShowDialog();
                    break;
                case 256:
                    FormThamSo.frm_BCDanhSachKSK frm256 = new FormThamSo.frm_BCDanhSachKSK();
                    frm256.ShowDialog();
                    break;

                case 257:
                    ChucNang.frm_ThongTinChiDaoTuyen frm257 = new ChucNang.frm_ThongTinChiDaoTuyen();
                    frm257.ShowDialog();
                    break;

                case 300:
                    FormThamSo.frmTk15NgaySDThuoc_TT23 frm300 = new FormThamSo.frmTk15NgaySDThuoc_TT23();
                    frm300.ShowDialog();
                    break;
                case 301:
                    FormThamSo.frm_BkeThuTienVP_BG03 frm301 = new FormThamSo.frm_BkeThuTienVP_BG03();
                    frm301.ShowDialog();
                    break;
                case 302:
                    FormThamSo.frm_BkeVP frm302 = new FormThamSo.frm_BkeVP();
                    frm302.ShowDialog();
                    break;
                case 303:
                    FormThamSo.Frm_ThVPhi_TU_TTTU_BG frm303 = new FormThamSo.Frm_ThVPhi_TU_TTTU_BG();
                    frm303.ShowDialog();
                    break;
                case 304:
                    FormThamSo.Frm_BcTinhHinhBenhTatCD10_TH frm304 = new FormThamSo.Frm_BcTinhHinhBenhTatCD10_TH();
                    frm304.ShowDialog();
                    break;
                case 305:
                    FormThamSo.Frm_BcTinhHinhBenhTatCD10_CL frm305 = new FormThamSo.Frm_BcTinhHinhBenhTatCD10_CL();
                    frm305.ShowDialog();
                    break;
                case 306:
                    FormThamSo.frm_phieutamthuthainguyen frm306 = new FormThamSo.frm_phieutamthuthainguyen();
                    frm306.ShowDialog();
                    break;
                case 307:
                    FormThamSo.Frm_BcTKSoPhieuLinhTheoKhoa frm307 = new FormThamSo.Frm_BcTKSoPhieuLinhTheoKhoa();
                    frm307.ShowDialog();
                    break;
                case 308:
                    FormThamSo.frm_TKDichVuKhongThuTien frm308 = new FormThamSo.frm_TKDichVuKhongThuTien();
                    frm308.ShowDialog();
                    break;
                case 309:
                    FormThamSo.frm_VienPhiHangNgay_30005 frm309 = new FormThamSo.frm_VienPhiHangNgay_30005();
                    frm309.ShowDialog();
                    break;
                case 310:
                    FormThamSo.Frm_BcHoatDongKKB_TH_30010 frm310 = new FormThamSo.Frm_BcHoatDongKKB_TH_30010();
                    frm310.ShowDialog();
                    break;
                case 311:
                    MessageBox.Show("Mẫu biểu đang nâng cấp, số liệu có thể chưa chính xác!");
                    FormThamSo.frm_BCHoatDongDieuTri_30010 frm311 = new FormThamSo.frm_BCHoatDongDieuTri_30010();
                    frm311.ShowDialog();
                    break;
                case 312:
                    //MessageBox.Show("Mẫu biểu đang nâng cấp, số liệu có thể chưa chính xác!");
                    FormThamSo.frm_BC_DieuTriNgoaiTru_30007 frm312 = new FormThamSo.frm_BC_DieuTriNgoaiTru_30007();
                    frm312.ShowDialog();
                    break;
                case 313:
                    //MessageBox.Show("Mẫu biểu đang nâng cấp, số liệu có thể chưa chính xác!");
                    FormThamSo.frm_BC_BNVaoVienNgoaiTru_30007 frm313 = new FormThamSo.frm_BC_BNVaoVienNgoaiTru_30007();
                    frm313.ShowDialog();
                    break;
                case 314:
                    //MessageBox.Show("Mẫu biểu đang nâng cấp, số liệu có thể chưa chính xác!");
                    FormThamSo.frm_BCHDTaiChinh_30010 frm314 = new FormThamSo.frm_BCHDTaiChinh_30010();
                    frm314.ShowDialog();
                    break;

                case 315:
                    FormThamSo.frm_PhiVCDuocMien frm315 = new FormThamSo.frm_PhiVCDuocMien();
                    frm315.ShowDialog();
                    break;
                case 316:
                    FormThamSo.frm_BC_ThuocDichTruyenVTYTtrongCuocMo frm316 = new FormThamSo.frm_BC_ThuocDichTruyenVTYTtrongCuocMo();
                    frm316.ShowDialog();
                    break;
                case 317:
                    FormThamSo.frm_BC_BenhTruyenNhiemThang_30004 frm317 = new FormThamSo.frm_BC_BenhTruyenNhiemThang_30004();
                    frm317.ShowDialog();
                    break;
                case 318:
                    FormThamSo.frm_BC_DSBNMacBenhTruyenNhiemThang_30004 frm318 = new FormThamSo.frm_BC_DSBNMacBenhTruyenNhiemThang_30004();
                    frm318.ShowDialog();
                    break;
                case 319:
                    FormThamSo.frm_BC_SoDe_30004 frm319 = new FormThamSo.frm_BC_SoDe_30004(0);
                    frm319.ShowDialog();
                    break;
                case 320:
                    FormThamSo.frm_BC_TheoDoiTTKCB_30009 frm320 = new FormThamSo.frm_BC_TheoDoiTTKCB_30009();
                    frm320.ShowDialog();
                    break;
                case 321:
                    FormThamSo.frm_BC_THThuVPtheoThang_27021 frm321 = new FormThamSo.frm_BC_THThuVPtheoThang_27021();
                    frm321.ShowDialog();
                    break;
                case 322:
                    FormThamSo.frm_BCQD917 frm322 = new FormThamSo.frm_BCQD917();
                    frm322.ShowDialog();
                    break;
                case 323:
                    FormThamSo.frm_BC_TheoNhomTuoiVaGTinh_12122 frm323 = new FormThamSo.frm_BC_TheoNhomTuoiVaGTinh_12122();
                    frm323.ShowDialog();
                    break;
                case 324:
                    FormThamSo.frm_BC_THChiTieuCM_12122 frm324 = new FormThamSo.frm_BC_THChiTieuCM_12122();
                    frm324.ShowDialog();
                    break;
                case 325:
                    TCThuoc frm325 = new TCThuoc();
                    frm325.ShowDialog();
                    break;
                case 326:
                    frm_ThongKeBNHuySoHSBA frm326 = new frm_ThongKeBNHuySoHSBA();
                    frm326.ShowDialog();
                    break;

                case 327:
                    frm_BC_GiaoBanHangNgay frm327 = new frm_BC_GiaoBanHangNgay();
                    frm327.ShowDialog();
                    break;
                case 328:
                    frm_BC_KCBvaVienPhi frm328 = new frm_BC_KCBvaVienPhi();
                    frm328.ShowDialog();
                    break;

                case 329:
                    frm_BCBNRa_VaoVien frm329 = new frm_BCBNRa_VaoVien();
                    frm329.ShowDialog();
                    break;
                case 330:
                    frm_BC_TTHDChuyenMon frm330 = new frm_BC_TTHDChuyenMon();
                    frm330.ShowDialog();
                    break;
                case 331:
                    frm_SoBanGiaoHSBN frm331 = new frm_SoBanGiaoHSBN();
                    frm331.ShowDialog();
                    break;
                case 332:
                    frm_BCDCGiaTT37 frm332 = new frm_BCDCGiaTT37();
                    frm332.ShowDialog();
                    break;
                case 333:
                    Frm_TKChiPhiTheoDV frm333 = new Frm_TKChiPhiTheoDV();
                    frm333.ShowDialog();
                    break;
                case 334:
                    frm_BCHD_CTKB_01049 frm334 = new frm_BCHD_CTKB_01049();
                    frm334.ShowDialog();
                    break;
                case 335:
                    frm_VienPhiHangNgay_30005_TheoTN frm335 = new frm_VienPhiHangNgay_30005_TheoTN();
                    frm335.ShowDialog();
                    break;
                case 336:
                    frm_SoChungSinh frm336 = new frm_SoChungSinh();
                    frm336.ShowDialog();
                    break;
                case 337:
                    frm_BCBenhNhanDieuTriNgoaiTru frm337 = new frm_BCBenhNhanDieuTriNgoaiTru();
                    frm337.ShowDialog();
                    break;
                case 338:
                    frm_BCVienPhiHNgay30002 frm338 = new frm_BCVienPhiHNgay30002();
                    frm338.ShowDialog();
                    break;
                case 339:
                    frm_BCDanhSachCanBo frm339 = new frm_BCDanhSachCanBo();
                    frm339.ShowDialog();
                    break;


                case 400:
                    FormThamSo.Frm_repTonghopHoaSinhA4 frm400 = new Frm_repTonghopHoaSinhA4();
                    frm400.ShowDialog();
                    break;
                case 401:
                    FormThamSo.Frm_TongHopXetNghiem frm401 = new Frm_TongHopXetNghiem();
                    frm401.ShowDialog();
                    break;
                case 402:
                    FormThamSo.Frm_BcHoatDongPTTT_TH02 frm402 = new Frm_BcHoatDongPTTT_TH02();
                    frm402.ShowDialog();
                    break;
                case 403:
                    FormThamSo.Frm_SoPhauThuat frm403 = new Frm_SoPhauThuat();
                    frm403.ShowDialog();
                    break;
                case 404:
                    //FormThamSo.Frm_SoThuThuat frm404 = new Frm_SoThuThuat();
                    FormThamSo.Frm_SoPhauThuat_ThuThuat frm404 = new Frm_SoPhauThuat_ThuThuat();
                    frm404.ShowDialog();
                    break;

                case 405:
                    FormThamSo.Frm_SoNoiSoi frm405 = new Frm_SoNoiSoi();
                    frm405.ShowDialog();
                    break;
                case 406:
                    FormThamSo.Frm_SoSieuAm frm406 = new Frm_SoSieuAm();
                    frm406.ShowDialog();
                    break;
                case 407:
                    FormThamSo.Frm_BcHoatDongCLS_TH03 frm407 = new Frm_BcHoatDongCLS_TH03();
                    frm407.ShowDialog();
                    break;
                case 408:

                    //MessageBox.Show("Mẫu biểu đang nâng cấp...");
                    FormThamSo.Frm_TongHop_CLS frm408 = new Frm_TongHop_CLS();
                    frm408.ShowDialog();
                    break;
                case 409:
                    FormThamSo.frm_BCHD_CLS frm409 = new FormThamSo.frm_BCHD_CLS();
                    frm409.ShowDialog();
                    break;
                case 410:
                    FormThamSo.Frm_BcXetNghiem_YM frm410 = new FormThamSo.Frm_BcXetNghiem_YM();
                    frm410.ShowDialog();
                    break;
                case 411:
                    FormThamSo.Frm_SoSieuAm_YS frm411 = new FormThamSo.Frm_SoSieuAm_YS();
                    frm411.ShowDialog();
                    break;
                case 412:
                    FormThamSo.Frm_BcHoatDongCLS_CL frm412 = new FormThamSo.Frm_BcHoatDongCLS_CL();
                    frm412.ShowDialog();
                    break;
                case 413:
                    FormThamSo.frm_SoXN_Expert frm413 = new FormThamSo.frm_SoXN_Expert();
                    frm413.ShowDialog();
                    break;
                case 414:
                    FormThamSo.frm_SoXN_SoiTrucTiep frm414 = new FormThamSo.frm_SoXN_SoiTrucTiep();
                    frm414.ShowDialog();
                    break;
                case 415:
                    FormThamSo.frm_BC_XNKSDViKhuanLao frm415 = new FormThamSo.frm_BC_XNKSDViKhuanLao();
                    frm415.ShowDialog();
                    break;
                case 416:
                    FormThamSo.frm_BC_XNNuoiCayViKhuanLao frm416 = new FormThamSo.frm_BC_XNNuoiCayViKhuanLao();
                    frm416.ShowDialog();
                    break;

                case 417:
                    FormThamSo.frm_BC_TongHopCLS_27022 frm417 = new FormThamSo.frm_BC_TongHopCLS_27022();
                    frm417.ShowDialog();
                    break;
                case 418:
                    FormThamSo.frm_BaoCaoCLSTrongNgay_12121 frm418 = new FormThamSo.frm_BaoCaoCLSTrongNgay_12121();
                    frm418.ShowDialog();
                    break;
                case 419:
                    FormThamSo.frm_BaoCaoHoatDongCLS_12121 frm419 = new FormThamSo.frm_BaoCaoHoatDongCLS_12121();
                    frm419.ShowDialog();
                    break;
                case 420:
                    FormThamSo.frm_BCThang_KhoaXNCDHA frm420 = new FormThamSo.frm_BCThang_KhoaXNCDHA();
                    frm420.ShowDialog();
                    break;
                case 421:
                    FormThamSo.frm_BaoCaoHoatDongCLS_12122 frm421 = new FormThamSo.frm_BaoCaoHoatDongCLS_12122();
                    frm421.ShowDialog();
                    break;
                case 422:
                    FormThamSo.frm_BC_BN_HoiChan frm422 = new FormThamSo.frm_BC_BN_HoiChan();
                    frm422.ShowDialog();
                    break;

                case 423:
                    FormThamSo.frm_BC_SolanThucHienPTTT frm423 = new FormThamSo.frm_BC_SolanThucHienPTTT();
                    frm423.ShowDialog();
                    break;

                case 424:
                    FormThamSo.frmThongKeTTPT frm424 = new FormThamSo.frmThongKeTTPT();
                    frm424.ShowDialog();
                    break;
                case 425:
                    FormThamSo.frm_THSoLuotXN_27183 frm425 = new FormThamSo.frm_THSoLuotXN_27183();
                    frm425.ShowDialog();
                    break;
                case 426:
                    FormThamSo.Frm_SoPhauThuat_ThuThuat_20001 frm426 = new FormThamSo.Frm_SoPhauThuat_ThuThuat_20001();
                    frm426.ShowDialog();
                    break;
                case 427:
                    FormThamSo.Frm_TamTraDVKT_New frm427 = new FormThamSo.Frm_TamTraDVKT_New();
                    frm427.ShowDialog();
                    break;
                case 428:
                    FormThamSo.frm_BCXN_TheoBoPhan_01071 frm428 = new FormThamSo.frm_BCXN_TheoBoPhan_01071();
                    frm428.ShowDialog();
                    break;
                case 429:
                    FormThamSo.frm_SoThuThuat_01071 frm429 = new FormThamSo.frm_SoThuThuat_01071();
                    frm429.ShowDialog();
                    break;
                case 430:
                    FormThamSo.frm_SoDoKhucXa_NhanAp_GiacMac frm430 = new FormThamSo.frm_SoDoKhucXa_NhanAp_GiacMac();
                    frm430.ShowDialog();
                    break;
                case 431:
                    FormThamSo.frm_SoTHCLS frm431 = new FormThamSo.frm_SoTHCLS();
                    frm431.ShowDialog();
                    break;
                case 432:
                    FormThamSo.frm_BCChiDinhMienDich frm432 = new FormThamSo.frm_BCChiDinhMienDich();
                    frm432.ShowDialog();
                    break;
                case 433:
                    FormThamSo.frm_BCXetNghiemHangNgay frm433 = new FormThamSo.frm_BCXetNghiemHangNgay();
                    frm433.ShowDialog();
                    break;
                case 434:
                    FormThamSo.frm_BCXetNghiem_30007 frm434 = new FormThamSo.frm_BCXetNghiem_30007();
                    frm434.ShowDialog();
                    break;
                case 435:
                    FormThamSo.frm_BCXetNghiemTheoKP_30007 frm435 = new FormThamSo.frm_BCXetNghiemTheoKP_30007();
                    frm435.ShowDialog();
                    break;
                case 436:
                    FormThamSo.frm_BCDanhSachBNPTTT_PPVoCam frm436 = new FormThamSo.frm_BCDanhSachBNPTTT_PPVoCam();
                    frm436.ShowDialog();
                    break;

                case 500:
                    FormThamSo.Frm_BCSKSCanbo frm500 = new FormThamSo.Frm_BCSKSCanbo();
                    frm500.ShowDialog();
                    break;
                case 501:
                    FormThamSo.frm_BCThuThangNgoaiTru_ChiLinh frm501 = new FormThamSo.frm_BCThuThangNgoaiTru_ChiLinh();
                    frm501.ShowDialog();
                    break;
                case 502:
                    FormThamSo.frm_BCThuocThanhToanVaTon frm502 = new FormThamSo.frm_BCThuocThanhToanVaTon();
                    frm502.ShowDialog();
                    break;
                case 503:
                    FormThamSo.frm_BC_TamUngVPhi frm503 = new FormThamSo.frm_BC_TamUngVPhi();
                    frm503.ShowDialog();
                    break;
                case 504:
                    FormThamSo.frm_BCNhomVienPhiTheoKhoa_30003 frm504 = new FormThamSo.frm_BCNhomVienPhiTheoKhoa_30003();
                    frm504.ShowDialog();
                    break;
                case 505:
                    FormThamSo.frm_BCTHBienLaiVP_30003 frm505 = new FormThamSo.frm_BCTHBienLaiVP_30003();
                    frm505.ShowDialog();
                    break;
                case 506:
                    FormThamSo.frm_BCThuVienPhi_LuyKe_30003 frm506 = new FormThamSo.frm_BCThuVienPhi_LuyKe_30003();
                    frm506.ShowDialog();
                    break;
                case 507:
                    FormThamSo.frm_BkeVPBNhanRaVien_30005 frm507 = new FormThamSo.frm_BkeVPBNhanRaVien_30005();
                    frm507.ShowDialog();
                    break;
                case 508:
                    FormThamSo.frm_TamUngVPHangNgay_30005 frm508 = new FormThamSo.frm_TamUngVPHangNgay_30005();
                    frm508.ShowDialog();
                    break;
                case 509:
                    FormThamSo.frm_BC_BNThanhToanKCB_30009 frm509 = new FormThamSo.frm_BC_BNThanhToanKCB_30009();
                    frm509.ShowDialog();
                    break;
                case 510:
                    FormThamSo.frm_BC_DoiChieuThuocTTBHYTNgoaiTru_30009 frm510 = new FormThamSo.frm_BC_DoiChieuThuocTTBHYTNgoaiTru_30009();
                    frm510.ShowDialog();
                    break;
                case 511:
                    FormThamSo.frm_TongHopThuChi_27022 frm511 = new FormThamSo.frm_TongHopThuChi_27022();
                    frm511.ShowDialog();
                    break;
                case 512:
                    FormThamSo.frm_THChungTuThuTU_27022 frm512 = new FormThamSo.frm_THChungTuThuTU_27022();
                    frm512.ShowDialog();
                    break;
                case 513:
                    FormThamSo.frm_BC_THBNThanhToanRaVien_27023 frm513 = new FormThamSo.frm_BC_THBNThanhToanRaVien_27023();
                    frm513.ShowDialog();
                    break;
                case 514:
                    FormThamSo.frm_BC_THThuVP_27023 frm514 = new FormThamSo.frm_BC_THThuVP_27023();
                    frm514.ShowDialog();
                    break;
                case 515:
                    FormThamSo.frm_BC_SoTheoDoiThuTamUngBNDTri_27023 frm515 = new FormThamSo.frm_BC_SoTheoDoiThuTamUngBNDTri_27023();
                    frm515.ShowDialog();
                    break;
                case 516:
                    FormThamSo.frm_BCDoiChieuGia frm516 = new FormThamSo.frm_BCDoiChieuGia();
                    frm516.ShowDialog();
                    break;
                case 517:
                    FormThamSo.frm_BC_DoanhthuVAT frm517 = new FormThamSo.frm_BC_DoanhthuVAT();
                    frm517.ShowDialog();
                    break;
                case 518:
                    FormThamSo.frm_BC_TTVPTheoNgay_26007cs frm518 = new FormThamSo.frm_BC_TTVPTheoNgay_26007cs();
                    frm518.ShowDialog();
                    break;
                case 519:
                    FormThamSo.frm_BCVPTheoKPhongThucHienCLS frm519 = new FormThamSo.frm_BCVPTheoKPhongThucHienCLS();
                    frm519.ShowDialog();
                    break;
                case 520:
                    FormThamSo.frm_dsthuocBHYTChenhKho_KT frm520 = new FormThamSo.frm_dsthuocBHYTChenhKho_KT();
                    frm520.ShowDialog();
                    break;
                case 521:
                    FormThamSo.frm_BangKeChungTuThu_30003 frm521 = new FormThamSo.frm_BangKeChungTuThu_30003();
                    frm521.ShowDialog();
                    break;
                case 522:
                    FormThamSo.frm_BCVienPhiPtramBHYT frm522 = new FormThamSo.frm_BCVienPhiPtramBHYT();
                    frm522.ShowDialog();
                    break;
                case 523:
                    FormThamSo.frm_BCThuNguonDV_XHH frm523 = new FormThamSo.frm_BCThuNguonDV_XHH();
                    frm523.ShowDialog();
                    break;
                case 524:
                    FormThamSo.frm_BCRV frm524 = new FormThamSo.frm_BCRV();
                    frm524.ShowDialog();
                    break;
                case 525:
                    FormThamSo.frm_BangKeQuyetToanVP frm525 = new FormThamSo.frm_BangKeQuyetToanVP();
                    frm525.ShowDialog();
                    break;
                case 526:
                    FormThamSo.frm_THThanhToamBNRaVien frm526 = new FormThamSo.frm_THThanhToamBNRaVien();
                    frm526.ShowDialog();
                    break;
                case 527:
                    FormThamSo.frm_BCThuVPTheoCa frm527 = new FormThamSo.frm_BCThuVPTheoCa();
                    frm527.ShowDialog();
                    break;
                case 528:
                    FormThamSo.frm_BaoCaoThuVienPhiThang frm528 = new FormThamSo.frm_BaoCaoThuVienPhiThang();
                    frm528.ShowDialog();
                    break;
                case 529:
                    FormThamSo.frm_BCChiPhiDVChiTiet frm529 = new FormThamSo.frm_BCChiPhiDVChiTiet();
                    frm529.ShowDialog();
                    break;
                case 530:
                    FormThamSo.frm_SoTheoDoiThuChi30007 frm530 = new FormThamSo.frm_SoTheoDoiThuChi30007();
                    frm530.ShowDialog();
                    break;
                case 531:
                    FormThamSo.frm_BaoCaoVienPhiTongHop_30002 frm531 = new FormThamSo.frm_BaoCaoVienPhiTongHop_30002();
                    frm531.ShowDialog();
                    break;
                case 532:
                    FormThamSo.frm_BCVienPhiHNgay_TheoHD frm532 = new FormThamSo.frm_BCVienPhiHNgay_TheoHD();
                    frm532.ShowDialog();
                    break;
                case 533:
                    FormThamSo.frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy frm533 = new FormThamSo.frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy();
                    frm533.ShowDialog();
                    break;
                case 534:
                    FormThamSo.Duoc.BCKhongThamSo.In_Id534();
                    break;
                case 535:
                    FormThamSo.Duoc.BCKhongThamSo.In_Id535();
                    break;
                case 536:
                    FormThamSo.frm_BC_VP_TheoKeToan_TrongNgay frm536 = new frm_BC_VP_TheoKeToan_TrongNgay();
                    frm536.ShowDialog();
                    break;
                case 537:
                    FormThamSo.frm_BCSL_BenhNhantheo_BS frm537 = new frm_BCSL_BenhNhantheo_BS();
                    frm537.ShowDialog();
                    break;
                case 538:
                    FormThamSo.frm_BC_ChucNangHoHap frm538 = new frm_BC_ChucNangHoHap();
                    frm538.ShowDialog();
                    break;
                case 539:
                    FormThamSo.frm_BC_ChanDoanHinhAnh frm539 = new frm_BC_ChanDoanHinhAnh();
                    frm539.ShowDialog();
                    break;
                case 540:
                    FormThamSo.Frm_BC_BenhNhanNoiTruBH frm540 = new Frm_BC_BenhNhanNoiTruBH();
                    frm540.ShowDialog();
                    break;
                case 541:
                    FormThamSo.Frm_BCHoatDongPK_cs2_01049 frm541 = new Frm_BCHoatDongPK_cs2_01049();
                    frm541.ShowDialog();
                    break;
                case 542:
                    FormThamSo.Frm_DinhMucTheoNgay_30010 frm542 = new Frm_DinhMucTheoNgay_30010();
                    frm542.ShowDialog();
                    break;
                case 543:
                    FormThamSo.frm_BC_VienPhi_HDViettel_NTL frm543 = new frm_BC_VienPhi_HDViettel_NTL();
                    frm543.ShowDialog();
                    break;
                case 544:
                    FormThamSo.Frm_BaoCaoSoLieuHangNgay_NTL frm544 = new Frm_BaoCaoSoLieuHangNgay_NTL();
                    frm544.ShowDialog();
                    break;
                case 545:
                    FormThamSo.frm_BNThucHienCLSTheoKP frm545 = new FormThamSo.frm_BNThucHienCLSTheoKP();
                    frm545.ShowDialog();
                    break;
                case 546:
                    FormThamSo.Frm_BcSuDungThuocTp_NTL frm546 = new FormThamSo.Frm_BcSuDungThuocTp_NTL();
                    frm546.ShowDialog();
                    break;
                case 547:
                    FormThamSo.frm_BaoCaoNopTienQuy_01071_new frm547 = new frm_BaoCaoNopTienQuy_01071_new();
                    frm547.ShowDialog();
                    break;
                case 548:
                    frm_BC_SoTheoDoiCongTacKBC frm548 = new frm_BC_SoTheoDoiCongTacKBC();
                    frm548.ShowDialog();
                    break;
                case 549:
                    frm_BN_ThucHienCLS frm549 = new frm_BN_ThucHienCLS();
                    frm549.ShowDialog();
                    break;
                case 550:
                    frm_SoChiTiet_14018 frm550 = new frm_SoChiTiet_14018();
                    frm550.ShowDialog();
                    break;
                case 551:
                    frm_DSCapGiayChungSinh frm551 = new frm_DSCapGiayChungSinh();
                    frm551.ShowDialog();
                    break;
                case 552:
                    Frm_DS_CapGiayRaVien frm552 = new Frm_DS_CapGiayRaVien();
                    frm552.ShowDialog();
                    break;
                case 553:
                    frm_DK_TT102 frm553 = new frm_DK_TT102();
                    frm553.ShowDialog();
                    break;
                case 554:
                    frm_THThuTheoCLS frm554 = new frm_THThuTheoCLS();
                    frm554.ShowDialog();
                    break;
                case 555:
                    frm_BaocaoKQKhamChuaBenh frm555 = new frm_BaocaoKQKhamChuaBenh();
                    frm555.ShowDialog();
                    break;
                case 556:
                    Frm_BaoCaoSoKhamBenh_30372 frm556 = new Frm_BaoCaoSoKhamBenh_30372();
                    frm556.ShowDialog();
                    break;
                case 557:
                    Frm_BaoCaoThuThuatNgoaiGio frm557 = new Frm_BaoCaoThuThuatNgoaiGio();
                    frm557.ShowDialog();
                    break;
                case 558:
                    Frm_BaoCaoBenhNhan_12345 frm558 = new Frm_BaoCaoBenhNhan_12345();
                    frm558.ShowDialog();
                    break;
                case 559:
                    frm_BaoCaoTongHopSoLieu_14018 frm559 = new frm_BaoCaoTongHopSoLieu_14018();
                    frm559.ShowDialog();
                    break;
                case 560:
                    frm_Baocaosolieutheongay_14018 frm560 = new frm_Baocaosolieutheongay_14018();
                    frm560.ShowDialog();
                    break;
                case 561:
                    frm_BC_SoDe_30004 frm561 = new frm_BC_SoDe_30004(1);
                    frm561.ShowDialog();
                    break;
                case 562:
                    frm_BaocaoTT37_Bieu9 frm562 = new frm_BaocaoTT37_Bieu9();
                    frm562.ShowDialog();
                    break;
                case 563:
                    frm_BaoCaoCLSCacKhoa_14017 frm563 = new frm_BaoCaoCLSCacKhoa_14017();
                    frm563.ShowDialog();
                    break;
                case 564:
                    frm_SoTheoDoiBNVV_14017 frm564 = new frm_SoTheoDoiBNVV_14017();
                    frm564.ShowDialog();
                    break;

                case 565:
                    frm_SoTheoDoiThuocKS_27022 frm565 = new frm_SoTheoDoiThuocKS_27022();
                    frm565.ShowDialog();
                    break;

                case 566:
                    frm_SoTTBNDungThuocKSDB_27022 frm566 = new frm_SoTTBNDungThuocKSDB_27022();
                    frm566.ShowDialog();
                    break;

                case 567:
                    frm_BCBenhNhanNoiNgoaiTru_01071 frm567 = new frm_BCBenhNhanNoiNgoaiTru_01071();
                    frm567.ShowDialog();
                    break;

                case 568:
                    frm_BCTestCovid19_01071 frm568 = new frm_BCTestCovid19_01071();
                    frm568.ShowDialog();
                    break;

                case 569:
                    Frm_BangTheoDoiBNBHYT frm569 = new Frm_BangTheoDoiBNBHYT();
                    frm569.ShowDialog();
                    break;
                //duc
                case 570: // nhukt
                    frm_DSBNThucHienTT_PT frm570 = new frm_DSBNThucHienTT_PT();
                    frm570.ShowDialog();
                    break;

                case 571: // nhukt
                    frm_TongHopVLTL frm571 = new frm_TongHopVLTL();
                    frm571.ShowDialog();
                    break;

                case 572: //Minhvd
                    frm_BaoCao frm572 = new frm_BaoCao();
                    DungChung.Bien.check = 1;
                    frm572.ShowDialog();
                    break;

                case 573: //Minhvd
                    frm_BaoCao frm573 = new frm_BaoCao();
                    DungChung.Bien.check = 2;
                    frm573.ShowDialog();
                    break;

                case 574:
                    FormThamSo.frm_BCSoBN24006 frm574 = new frm_BCSoBN24006();
                    frm574.ShowDialog();
                    break;

                case 575: //Nhukt
                    Frm_BCDoanhThuKhoaPhong24006 frm575 = new Frm_BCDoanhThuKhoaPhong24006();
                    frm575.ShowDialog();
                    break;

                case 576: //Nhukt
                    Frm_BCDanhSachBNTraThuocNgoaiTru frm576 = new Frm_BCDanhSachBNTraThuocNgoaiTru();
                    frm576.ShowDialog();
                    break;

                case 577: //Nhukt
                    Frm_TheoDoiPhanLoaiMuaThuoc24012 frm577 = new Frm_TheoDoiPhanLoaiMuaThuoc24012();
                    frm577.ShowDialog();
                    break;

                case 578: //minhvd
                    FormThamSo.Frm_TongHopChiPhi_NT_NgT_VP_XaThang frm578 = new Frm_TongHopChiPhi_NT_NgT_VP_XaThang();
                    frm578.ShowDialog();
                    break;

                case 579: //Hoalv
                    Frm_BcKhamChuaBenh_24012 frm579 = new Frm_BcKhamChuaBenh_24012();
                    frm579.ShowDialog();
                    break;

                case 580:
                    FormThamSo.Frm_DS_BenhNhan frm580 = new Frm_DS_BenhNhan();
                    frm580.ShowDialog();
                    break;
                //case 581:
                //    FormThamSo.Frm_BC_TTHDCMBenhVien frm581 = new Frm_BC_TTHDCMBenhVien();
                //    frm581.ShowDialog();
                //    break;
                case 582:
                    FormThamSo.frm_TheoDoi_RaVaoChuyenVien frm582 = new FormThamSo.frm_TheoDoi_RaVaoChuyenVien();
                    frm582.ShowDialog();
                    break;
                case 583: //Hoalv
                    Frm_BCHoatDongKCBToanVien_27023 frm583 = new Frm_BCHoatDongKCBToanVien_27023();
                    frm583.ShowDialog();
                    break;
                case 584:
                    frm_BC_BNNTLinhThuoc frm584 = new frm_BC_BNNTLinhThuoc();
                    frm584.ShowDialog();
                    break;
                case 585:
                    Rep_BC_ChiPhiKhamChuaBenhTheoMaICD frm585 = new Rep_BC_ChiPhiKhamChuaBenhTheoMaICD();
                    frm585.ShowDialog();
                    break;
                case 586:
                    FormThamSo.frm_LoiNhuan frm586 = new FormThamSo.frm_LoiNhuan();
                    frm586.ShowDialog();
                    break;
                case 588:
                    frmBCKeThuocTheoBSi frm588 = new frmBCKeThuocTheoBSi();
                    frm588.ShowDialog();
                    break;

                case 589: //Hoalv
                    Frm_CapLaiGiayRaVien frm589 = new Frm_CapLaiGiayRaVien();
                    frm589.ShowDialog();
                    break;

                case 590:
                    frm_BaoCao_Moi frm590 = new frm_BaoCao_Moi();
                    frm590.ShowDialog();
                    break;

                case 592:
                    frm_BC_KhamBenhTheo_PK frm592 = new frm_BC_KhamBenhTheo_PK();
                    frm592.ShowDialog();
                    break;

                case 593:
                    Frm_ThongTinHoatDong_24012 frm593 = new Frm_ThongTinHoatDong_24012();
                    frm593.ShowDialog();
                    break;

                case 594:
                    Frm_BaoCao_CCCD frm594 = new Frm_BaoCao_CCCD();
                    frm594.ShowDialog();
                    break;

                case 595:
                    frm_SoCLS frm595 = new frm_SoCLS();
                    frm595.ShowDialog();
                    break;

                case 596:
                    FormThamSo.Frm_SoThuThuat frm596 = new FormThamSo.Frm_SoThuThuat();
                    frm596.ShowDialog();
                    break;

                case 597:
                    frm_BC_SoTiepDonBenhNhan frm597 = new frm_BC_SoTiepDonBenhNhan();
                    frm597.ShowDialog();
                    break;
               
                case 598:
                    frm_BangKe_TT_BenhNhanCungChiTraBHYT frm598 = new frm_BangKe_TT_BenhNhanCungChiTraBHYT();
                    frm598.ShowDialog();
                    break;
            }
        }
    }

}
