using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class us_NangCap : DevExpress.XtraEditors.XtraUserControl
    {
        string _ploaimenu = "",_kieu="",_mabv="";
        bool _public = true;
        public us_NangCap()
        {
            InitializeComponent();
        }
        public us_NangCap(string pl)
        {
            InitializeComponent();
            _ploaimenu = pl;
        }
        public us_NangCap(string pl, string kieu, string mabv, bool _pl)
        {
            InitializeComponent();
            _ploaimenu = pl;
            _kieu = kieu;
            _mabv = mabv;
            _public = _pl;
        }
        public class limenu
        {
            public string tenbc;
            public int id;
            public string ploai;
            public string mabv;
            public string kieu;
            public bool _public;
            public bool _Public
            {
                set { _public = value; }
                get { return _public; }
            }
            public string Kieu
            {
                set { kieu = value; }
                get { return kieu; }
            }
            public string MaBV
            {
                set { mabv = value; }
                get { return mabv; }
            }
            public string TenBC
            {
                set { tenbc = value; }
                get { return tenbc; }
            }
            public int ID
            {
                set { id = value; }
                get { return id; }
            }
            public string Ploai
            {
                set { ploai = value; }
                get { return ploai; }
            }
        }
        private void us_menubc_Load(object sender, EventArgs e)
        {
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
            List<limenu> _lm = new List<limenu>();
            _lm.Add(new limenu { TenBC = "BC nội trú theo khoa phòng", ID = 1, Ploai = "Tổng hợp",_Public=true,MaBV="",Kieu=""});
            _lm.Add(new limenu { TenBC = "BC nội trú theo mã bệnh", ID = 2, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "BC nội trú theo chuyên khoa", ID = 3, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "BC theo nhóm đối tượng ", ID = 4, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT) ", ID = 5, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-BHYT) ", ID = 6, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79ath-BHYT) ", ID = 7, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80ath-BHYT) ", ID = 8, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thông kê tổng hợp thuốc sử dụng(mẫu 20-BHYT) ", ID = 9, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê tổng hợp dịch vụ sử dụng(mẫu 21-BHYT) ", ID = 10, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a-BHYT) ", ID = 11, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB nội trú các nhóm đối tượng theo tuyến CMKT(mẫu 14b-BHYT)", ID = 12, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến nội-ngoại tỉnh CT( mẫu 10ct|11ct-BHYT)", ID = 13, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến ngoại tỉnh TH( mẫu 11TH-BHYT)", ID = 14, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH chi phí KCB BHYT thanh toán đa tuyến nội tỉnh TH( mẫu 10TH-BHYT)", ID = 15, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79B-BHYT)", ID = 16, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80B-BHYT)", ID = 17, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB ngoại trú(mẫu 79B(%BH)-BHYT)", ID = 18, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "TH BHYT KCB nội trú(mẫu 80B(%BH)-BHYT)", ID = 19, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a(%BH)-BHYT) ", ID = 20, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB nội trú các nhóm đối tượng theo tuyến CMKT(mẫu 14b(%BH)-BHYT)", ID = 21, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách bệnh nhân nộp tạm thu", ID = 22, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách dịch vụ bệnh nhân đã thực hiện chưa thanh toán", ID = 23, Ploai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ vào viện(YS-TQ)", ID = 24, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp số lượt xét nghiệm", ID = 25, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ vào viện theo đối tượng(YS-TQ)", ID = 26, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ theo dõi viện phí(VY-BG)", ID = 27, Ploai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo chi tiết dược nội trú tồn thực sử dụng và thực sự dụng", ID = 28, Ploai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê dịch vụ kỹ thuật thanh toán(mẫu 21-BHYT_1399) ", ID = 29, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            //_lm.Add(new limenu { TenBC = "Thống kê thuốc thanh toán(mẫu 20-BHYT_1399) ", ID = 30, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê Bệnh Nhân theo điều trị ", ID = 31, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT_1399)_SP", ID = 32, Ploai = "BHYT- Tổng hợp", _Public = true, MaBV = "", Kieu = "TTD" });
            _lm.Add(new limenu { TenBC = "Thông kê  thuốc thanh toán(mẫu 20_1399-BHYT)_SP ", ID = 33, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "TTD" });
            _lm.Add(new limenu { TenBC = "Thống kê  dịch vụ thanh toán(mẫu 21_1399-BHYT)_SP ", ID = 34, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "TTD" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-BHYT_1399)_SP", ID = 35, Ploai = "BHYT- Tổng hợp", _Public = true, MaBV = "", Kieu = "TTD" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB ngoại trú các nhóm đối tượng theo tuyến CMKT(mẫu 14a-BHYT)_SP ", ID = 36, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê KCB nội trú các nhóm đối tượng theo tuyến CMKT(mẫu 14b-BHYT)_SP", ID = 37, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thông kê VTYT thanh toán BHYT(mẫu 19_1399-BHYT)_SP ", ID = 38, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "TTD" });
            _lm.Add(new limenu { TenBC = "Báo cáo chi phí khám chữa bệnh theo từng bệnh nhân ", ID = 39, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo phẫu thuật theo phân loại ", ID = 40, Ploai = "Tổng hợp - CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo hoạt động cận lâm sàng ", ID = 41, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Biên bản kiểm kê thuốc tại các khoa điều trị(CL-HD)", ID = 42, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp tình hình người bệnh chuyển đến", ID = 43, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo khoa xét nghiệm (YM -HY)", ID = 44, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            //
            _lm.Add(new limenu { TenBC = "Thống kê danh sách Bệnh Nhân Vào|Chuyển viện", ID = 50, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê danh sách Bệnh Nhân có đơn thuốc", ID = 51, Ploai = "Tổng hợp - Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách thu tiền khám sức khỏe ", ID = 52, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp CP KCB BHYT trẻ em dưới 6 tuổi chưa có thẻ BHYT", ID = 53, Ploai = "Tổng hợp - Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo tình hình hoạt động khám sức khỏe(VY-BG)", ID = 54, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo hoạt động tại khoa khám bệnh", ID = 55, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp danh sách Bệnh Nhân có BHYT đăng ký KCB", ID = 56, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp khám chữa bệnh toàn viện(YS)", ID = 57, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê số lượt khám bệnh ngoại trú theo chuyên khoa(YS)", ID = 58, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ khám bệnh", ID = 59, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ vào-ra-chuyển viện", ID = 60, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê số lượt từng BN khám ngoại trú", ID = 61, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Hoạt động khám chữa bệnh phòng khám(TK-CB)", ID = 62, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê kết cấu bệnh tật người lớn(TK-CB)", ID = 63, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê kết cấu bệnh tật trẻ em(TK-CB)", ID = 64, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách nộp tiền viện phí theo khoa(TK-CB)", ID = 65, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách nộp tiền viện phí khám sức khỏe(TK-CB)", ID = 66, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách nộp tiền viện phí(TK-CB)", ID = 67, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp xét nghiệm (A4)", ID = 68, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp XN Nước Tiểu", ID = 69, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp kết quả Xét Nghiệm", ID = 70, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp XN Huyết Học", ID = 71, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp XN Khác", ID = 72, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp Phẫu Thuật", ID = 73, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp Thủ Thuật", ID = 74, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp Nội Soi", ID = 75, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ tổng hợp Siêu Âm - X.Quang", ID = 76, Ploai = "CLS", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp thông tin chuyển tuyến", ID = 77, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp chi phí vận chuyển người bệnh BHYT", ID = 78, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Nộp tiền vào quỹ", ID = 79, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tam tra thuốc", ID = 80, Ploai = "Tổng hợp - Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Thống kê chi phí theo khoa phòng", ID = 81, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách tổng hợp thu tiền % viện phí BHYT", ID = 82, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng kê chi tiết phẫu thuật (BG - HD)", ID = 83, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "BẢNG KÊ CHI TIẾT THU VIỆN PHÍ(BG-HD)", ID = 84, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng kê viện phí theo khoa(BG-HD)", ID = 85, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tổng hợp thu viện phí(BG-HD)", ID = 86, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng tổng hợp viện phí nội trú(BG-HD)", ID = 87, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng tổng hợp tạm ứng  và thanh toán tạm ứng(BG-HD)", ID = 88, Ploai = "Viện phí", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Tình hình bệnh tật, tử vong tại bệnh viện theo icd 10 - who(VY-BG)", ID = 89, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo hoạt động khám chữa bệnh(VY-BG)", ID = 90, Ploai = "Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
           _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-BHYT_1399) ", ID = 91, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            //_lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-BHYT_1399) ", ID = 92, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo chi phí nội trú(HL - CB) ", ID = 93, Ploai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng chấm công thủ thuật - Phẫu thuật", ID = 94, Ploai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo thuốc tồn gối", ID = 95, Ploai = "Viện phí - Tổng hợp - Dược" , _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB ngoại trú(mẫu 79act-(%)BHYT)", ID = 96, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Danh sách người bệnh BHYT KCB nội trú(mẫu 80act-(%)BHYT)", ID = 97, Ploai = "BHYT", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Bảng kê tiền thuốc BHYT-Dịch vụ(YM-HY)", ID = 98, Ploai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo khoa khám bệnh(Chí Linh-HD)", ID = 99, Ploai = "Viện phí - Tổng hợp", _Public = true, MaBV = "30003", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo công tác khám bệnh(Na Hang-TQ)", ID = 100, Ploai = "Viện phí - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "BC nhập xuất tồn", ID = 101, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "BC nhập xuất tồn(rút gọn)", ID = 102, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "BC hàng xuất", ID = 103, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });
            _lm.Add(new limenu { TenBC = "TH danh sách BN xuất dược ", ID = 104, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });
            _lm.Add(new limenu { TenBC = "BC sử dụng thuốc", ID = 105, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "SD" });
            _lm.Add(new limenu { TenBC = "Báo cáo hàng xuất theo đối tượng(Nam Sách-HD)", ID = 106, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });

            _lm.Add(new limenu { TenBC = "Thẻ kho(TT22) ", ID = 108, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "BB kiểm kê thuốc-Hóa chất -VTYT", ID = 109, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "KK" });
            _lm.Add(new limenu { TenBC = "Báo cáo VTYT tiêu hao theo khoa|Phòng(Không TT)", ID = 110, Ploai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ thẻ kho", ID = 111, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo 15 ngày sử dụng thuốc", ID = 112, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Sổ kiểm nhập(TT22)", ID = 113, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn toàn viện(CM|NB)", ID = 114, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "TK Danh Mục thuốc trong BHYT sử dụng tại đơn vị(CM-06)", ID = 115, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn kho cấp phát(CM)", ID = 116, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn kho tổng(CM)", ID = 117, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "BB kiểm nhập thuốc - hóa chất - VTYT(CM)", ID = 118, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo NXT xã- phường(CM)", ID = 119, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "Báo cáo công tác khoa dược(TT22)", ID = 120, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn thuốc đông y", ID = 121, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "Tổng hợp hàng nhập", ID = 122, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Nhap" });
            _lm.Add(new limenu { TenBC = "Tổng hợp hàng xuất", ID = 123, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });
            _lm.Add(new limenu { TenBC = " Báo cáo xuất dược", ID = 124, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });
            _lm.Add(new limenu { TenBC = "Sổ đối chiếu thuốc theo khoa phòng", ID = 125, Ploai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn tủ trực", ID = 126, Ploai = "Khoa dược - Tổng hợp", _Public = true, MaBV = "", Kieu = "NXT" });
            _lm.Add(new limenu { TenBC = "Báo cáo sử dụng dược(BG-HD)", ID = 127, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "" });
            _lm.Add(new limenu { TenBC = "Báo cáo hàng xuất(BG-HD)", ID = 128, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "Xuat" });
            _lm.Add(new limenu { TenBC = "Báo cáo nhập xuất tồn theo từng phân loại", ID = 129, Ploai = "Khoa dược", _Public = true, MaBV = "", Kieu = "NXT" });
            grcMenu.DataSource = _lm.Where(p => p.Ploai.Contains(_ploaimenu) && p.Kieu.Contains(_kieu)&& p.MaBV.Contains(_mabv)&& p._Public==_public).OrderBy(p=>p.TenBC);
        }

        private void grvMenu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int id=0;
            if (grvMenu.GetFocusedRowCellValue(colNgay) != null)
                id = Convert.ToInt32(grvMenu.GetFocusedRowCellValue(colNgay));
            switch (id) { 
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
                    FormThamSo.frm_80bHDCD frm17 = new frm_80bHDCD();
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
                    FormThamSo.Frm_ThHoaSinhMauSL frm25 = new Frm_ThHoaSinhMauSL();
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
                    FormThamSo.frmTsBcMau21BHYT_1399 frm29 = new frmTsBcMau21BHYT_1399();
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
                    //   case 32:
                    //FormThamSo.frm_rep79aCT_1399_SP frm32 = new FormThamSo.frm_rep79aCT_1399_SP();
                    //frm32.ShowDialog();
                    //break;
                    //   case 33:
                    //FormThamSo.frmTsBcMau19_20_1399 frm33 = new FormThamSo.frmTsBcMau19_20_1399();
                    //frm33.ShowDialog();
                    //break;
                    //   case 34:
                    //FormThamSo.frmTsBcMau21BHYT_1399_SP frm34 = new FormThamSo.frmTsBcMau21BHYT_1399_SP();
                    //frm34.ShowDialog();
                    //break;
                    //   case 35:
                    //FormThamSo.frm_80aHD_1399_SP frm35 = new FormThamSo.frm_80aHD_1399_SP();
                    //frm35.ShowDialog();
                    //break;
                    //   case 36:
                    //FormThamSo.frm_14a_Moi frm36 = new FormThamSo.frm_14a_Moi();
                    //frm36.ShowDialog();
                    //break;
                    //   case 37:
                    //FormThamSo.frm_14b_Moi frm37 = new FormThamSo.frm_14b_Moi();
                    //frm37.ShowDialog();
                    //break;
                    //   case 38:
                    //FormThamSo.frmTsBcMau19_20_1399 frm38 = new FormThamSo.frmTsBcMau19_20_1399(19);
                    //frm38.ShowDialog();
                    //break;
                       case 39:
                    FormThamSo.Frm_BcChiPhiKCB frm39 = new FormThamSo.Frm_BcChiPhiKCB();
                    frm39.ShowDialog();
                    break;
                       case 40:
                    FormThamSo.Frm_BcPhauThuat_CL frm40 = new FormThamSo.Frm_BcPhauThuat_CL();
                    frm40.ShowDialog();
                    break;
                       case 41:
                    FormThamSo.frm_BCHD_CLS frm41 = new FormThamSo.frm_BCHD_CLS();
                    frm41.ShowDialog();
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
                    FormThamSo.Frm_BcXetNghiem_YM frm44 = new FormThamSo.Frm_BcXetNghiem_YM();
                    frm44.ShowDialog();
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
                    //FormThamSo.Frm_Sokhambenh frm59 = new Frm_Sokhambenh();
                    //frm59.ShowDialog();
                    break;
                       case 60:
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
                    FormThamSo.Frm_repTonghopHoaSinhA4 frm68 = new Frm_repTonghopHoaSinhA4();
                    frm68.ShowDialog();
                    break;
                       case 69:
                    FormThamSo.Frm_repTonghopXNNuocTieu frm69 = new Frm_repTonghopXNNuocTieu();
                    frm69.ShowDialog();
                    break;
                       case 70:
                    FormThamSo.Frm_TongHopXetNghiem frm70 = new Frm_TongHopXetNghiem();
                    frm70.ShowDialog();
                    break;
                       case 71:
                    FormThamSo.Frm_repTonghopHuyetHoc frm71 = new Frm_repTonghopHuyetHoc();
                    frm71.ShowDialog();
                    break;
                       case 72:
                    FormThamSo.Frm_repTonghopXNKhac frm72 = new Frm_repTonghopXNKhac();
                    frm72.ShowDialog();
                    break;
                       case 73:
                    FormThamSo.Frm_SoPhauThuat frm73 = new Frm_SoPhauThuat();
                    frm73.ShowDialog();
                    break;
                       case 74:
                    FormThamSo.Frm_SoThuThuat frm74 = new Frm_SoThuThuat();
                    frm74.ShowDialog();
                    break;
                       case 75:
                    FormThamSo.Frm_SoNoiSoi frm75 = new Frm_SoNoiSoi();
                    frm75.ShowDialog();
                    break;
                       case 76:
                    FormThamSo.Frm_SoSieuAm frm76 = new Frm_SoSieuAm();
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
                    FormThamSo.Frm_TamTraThuoc frm80 = new Frm_TamTraThuoc();
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
                    //   case 91:
                    //FormThamSo.frm_rep79aCT_1399 frm91 = new frm_rep79aCT_1399();
                    //frm91.ShowDialog();
                    //break;
                    //   case 92:
                    //FormThamSo.frm_80aHD_1399 frm92 = new frm_80aHD_1399();
                    //frm92.ShowDialog();
                    //break;
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
                    //FormThamSo.Frm_BcKKB_CL frm99 = new Frm_BcKKB_CL();
                    //frm99.ShowDialog();
                    break;
                       case 100:
                    FormThamSo.Frm_BcCongTacKB_NH frm100 = new Frm_BcCongTacKB_NH();
                    frm100.ShowDialog();
                    break;
                case 101:
                    FormThamSo.frmTsBCNXT frm101 = new frmTsBCNXT();
                    frm101.ShowDialog();
                    break;
                case 102:
                    FormThamSo.frmTsBcNXTrutgon frm102 = new frmTsBcNXTrutgon();
                    frm102.ShowDialog();
                    break;
                case 103:
                    FormThamSo.frmTsBcNXTXuat frm103 = new frmTsBcNXTXuat();
                    frm103.ShowDialog();
                    break;
                case 104:
                    FormThamSo.frm_benhnhanxuatduoc frm104 = new frm_benhnhanxuatduoc();
                    frm104.ShowDialog();
                    break;
                case 105://cauvithao
                    //FormThamSo.frmTsBcSuDungThuoc frm105 = new frmTsBcSuDungThuoc();
                    //frm105.ShowDialog();
                    break;
                case 106:
                    FormThamSo.Frm_BcHangXuatTheoDT_NS frm106 = new Frm_BcHangXuatTheoDT_NS();
                    frm106.ShowDialog();
                    break;
                case 107:
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
                    FormThamSo.Frm_BcNXTToanTT_CM09 frm114 = new FormThamSo.Frm_BcNXTToanTT_CM09();
                    frm114.ShowDialog();
                    break;
                case 115:
                    FormThamSo.frm_06 frm115 = new FormThamSo.frm_06();
                    frm115.ShowDialog();
                    break;
                case 116:
                    FormThamSo.frm_BcNXT_CM05 frm116 = new FormThamSo.frm_BcNXT_CM05();
                    frm116.ShowDialog();
                    break;
                case 117:
                    FormThamSo.Frm_BcNXTTong_CM10 frm117 = new FormThamSo.Frm_BcNXTTong_CM10();
                    frm117.ShowDialog();
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
                case 128://cauvithao
                    //FormThamSo.frmTsBcNXTXuat_BG frm128 = new FormThamSo.frmTsBcNXTXuat_BG();
                    //frm128.ShowDialog();
                    break;
                case 129://cavithao
                    //FormThamSo.Frm_BkTonD frm129 = new FormThamSo.Frm_BkTonD();
                    //frm129.ShowDialog();
                    break;
            }

        }

   

     
     
    }
 
}
