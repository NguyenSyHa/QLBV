using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_KetQuaSieuAmThai4D : DevExpress.XtraEditors.XtraForm
    {
        public frm_KetQuaSieuAmThai4D(int idcls, int makp)
        {
            InitializeComponent();
            _idcls = idcls;
            _maKP = makp;
        }

        int _idcls = 0, _maKP = 0;

        int giaodien = 0;
        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
        public class Ketqua
        {
            public int stt { get; set; }
            public string tendvct { get; set; }
            public string ketqua { get; set; }
        }


        public class BienKQ
        {
            public string TuCung { get; set; }
            public string CuDongThai { get; set; }
            public string DKLuongDinh_BPD { get; set; }
            public string DKChamTranDau_OFD { get; set; }
            public string ChuViDau_HC { get; set; }
            public string ChuViBung_AC { get; set; }
            public string DKTruocSauBung_APTD { get; set; }
            public string DKNgangBung_TTD { get; set; }
            public string ChieuDaiXuongDui_FL { get; set; }
            public string KichThuocNgangTieuNao { get; set; }
            public string NaoThatBen { get; set; }
            public string HoSau { get; set; }
            public string ChieuDaiXuongMui { get; set; }
            public string KhoangCach2HocMat { get; set; }
            public string HinhAnhVomSo { get; set; }
            public string CotSong { get; set; }
            public string CauTrucTimThai { get; set; }
            public string CauTrucPhoi { get; set; }
            public string ThoatViRuot { get; set; }
            public string DaDay { get; set; }
            public string QuaiRuot { get; set; }
            public string Than { get; set; }
            public string NgoiThai { get; set; }
            public string TuoiThai { get; set; }
            public string DuKienSinh { get; set; }
            public string CanNang { get; set; }
            public string NhipTimThai { get; set; }
            public string ChieuDaiDauMong_CRL { get; set; }
            public string KhoangSangSauGay { get; set; }
            public string ThoatViThanhBung { get; set; }
            public string RauThai { get; set; }
            public string RauThai2 { get; set; }
            public string RauThai3 { get; set; }
            public string NuocOi { get; set; }
            public string DayRon { get; set; }
            public string Khac { get; set; }
            public string Tomtat { get; set; }
        }

        //string kqsieuammau = "Số lượng thai:  thai.\rCử động thai: \rNgôi thai:  \rĐK lưỡng đỉnh(BPD):  mm \rChu vi vòng đầu(HC):  mm \rĐK ngang bụng(TTD):  mm \rĐK trước sau bụng(APTD):  mm \rChu vi vòng bụng(AC):  mm \rChiều dài xương đùi(FL):  mm \rChiều dài bàn chân:  mm \rChiều dài xương cánh tay:  mm \rXương sống mũi:  mm \rKhoảng cách hai hố mắt:  mm \rTim thai đều, tần số:  Ck/p \rKích thước não thất bên:  mm \rKích thước tiểu não:  mm \rKích thước hố sau:  mm \rCân nặng ước tính: Gram(Phù hợp với tuổi thai) \rTuổi thai ước tính:  tuần  ngày(+/-10 ngày) \rNgày sinh dự kiến:  /   /201  (+/-07 ngày) \rHàm mặt - Tứ chi: Hiện không thấy bất thường. \rXương hộp sọ: phát triển tốt. \rCột sống - Hệ xương bình thường \rCác não thất không giãn \rTim thai: Cấu trúc 4 buồng, không giãn. \rThành bụng: đóng kín \r- Cơ hoành bình thường. \r- Dạ dày trong ổ bụng \r- Hai thận bình thường \r- Bàng quang trong hổ chậu \r- Lồng ngực: Cấu trúc của phổi đồng nhất. \rVị trí rau bám mặt trước đáy tử cung, mật độ đều \rDịch ối trong, số lượng ối bình thường. \rCửa sổ Doppler hai động mạch, một tĩnh mạch.";
        public List<Ketqua> KQMau()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Số lượng thai", ketqua = "01 thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Ngôi thai", ketqua = "Chưa cố định" });
            kq.Add(new Ketqua { stt = 4, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Chu vi vòng đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chu vi vòng bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "Chiều dài bàn chân", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Chiều dài xương cánh tay", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Xương sống mũi", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Khoảng cách hai hố mắt", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Tim thai đều, tần số", ketqua = "  Ck/p" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Kích thước não thất bên", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Kích thước tiểu não", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Kích thước hố sau", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 18, tendvct = "Cân nặng ước tính", ketqua = "  Gram(Phù hợp với tuổi thai)" });
            kq.Add(new Ketqua { stt = 19, tendvct = "Tuổi thai ước tính", ketqua = "  tuần  ngày(+/-10 ngày)" });
            kq.Add(new Ketqua { stt = 20, tendvct = "Ngày sinh dự kiến", ketqua = "  /   /201  (+/-07 ngày)" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Hàm mặt - Tứ chi", ketqua = "Hiện không thấy bất thường" });
            kq.Add(new Ketqua { stt = 22, tendvct = "Xương hộp sọ", ketqua = "phát triển tốt" });
            kq.Add(new Ketqua { stt = 23, tendvct = "Cột sống - Hệ xương", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 24, tendvct = "Các não thất", ketqua = "không giãn" });
            kq.Add(new Ketqua { stt = 25, tendvct = "Tim thai", ketqua = "Cấu trúc 4 buồng, không giãn" });
            kq.Add(new Ketqua { stt = 26, tendvct = "Thành bụng", ketqua = "đóng kín" });
            kq.Add(new Ketqua { stt = 27, tendvct = "- Cơ hoành", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 28, tendvct = "- Dạ dày", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 29, tendvct = "- Hai thận", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 30, tendvct = "- Bàng quang", ketqua = "trong hổ chậu" });
            kq.Add(new Ketqua { stt = 31, tendvct = "- Lồng ngực", ketqua = "Cấu trúc của phổi đồng nhất" });
            kq.Add(new Ketqua { stt = 32, tendvct = "Rau thai", ketqua = "Vị trí rau bám mặt trước đáy tử cung mật độ đều" });
            kq.Add(new Ketqua { stt = 33, tendvct = "Nước ối", ketqua = "Dịch ối trong, số lượng ối bình thường" });
            kq.Add(new Ketqua { stt = 34, tendvct = "Dây rốn", ketqua = "Cửa sổ Doppler hai động mạch, một tĩnh mạch" });
            return kq;
        }

        public List<Ketqua> KQMau_12345()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Tử cung", ketqua = "Cấu trúc âm đồng đều, không có khối khu trú" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Trong buồng tử cung có hình ảnh", ketqua = "01 thai" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Phần phụ", ketqua = "Hai bên hiện không thấy hình ảnh bất thường" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Số lượng thai", ketqua = "01 thai" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Tim thai", ketqua = "  lần/phút" });
            kq.Add(new Ketqua { stt = 7, tendvct = "ĐK Lưỡng đỉnh (BPD)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Đường kính tiêu não (Cereb)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Chu vi vòng đầu (HC)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "KT hố sau (<10mm)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "ĐK chẩm trán~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "KT não thất bên (bt<10mm)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Khoảng cách 2 hố mắt (BD)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Đường giữa", ketqua = "Không bị lệch" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Chiều dài xương chính mũi~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Vách trong suốt", ketqua = "Bình thường" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Xương hộp sọ", ketqua = "Phát triển tốt" });
            kq.Add(new Ketqua { stt = 18, tendvct = "Cột sống, hệ xương", ketqua = "Không thấy bất thường" });
            kq.Add(new Ketqua { stt = 19, tendvct = "Ổ bụng", ketqua = "" });
            kq.Add(new Ketqua { stt = 20, tendvct = "Lồng ngực", ketqua = "Phổi cấu trúc đồng nhất, không có nang tuyến" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Chu vi vòng bụng (AC)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 22, tendvct = "Tim thai", ketqua = "Nằm đúng vị trí" });
            kq.Add(new Ketqua { stt = 23, tendvct = "Thành bụng", ketqua = "" });
            kq.Add(new Ketqua { stt = 24, tendvct = "Cơ hoành", ketqua = "Bình thường" });
            kq.Add(new Ketqua { stt = 25, tendvct = "Dạ dày", ketqua = "Đúng vị trí" });
            kq.Add(new Ketqua { stt = 26, tendvct = "Gan", ketqua = "Bình thường" });
            kq.Add(new Ketqua { stt = 27, tendvct = "Bàng quang", ketqua = "Trong hố chậu" });
            kq.Add(new Ketqua { stt = 28, tendvct = "Tứ chi", ketqua = "Phân chia 3 đoạn rõ" });
            kq.Add(new Ketqua { stt = 29, tendvct = "Cân nặng ước tính", ketqua = "  gr (+/- 200gr)" });
            kq.Add(new Ketqua { stt = 30, tendvct = "Chiều dài sương cánh tay (HL)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 31, tendvct = "Tuổi thai ước tính", ketqua = "  tuần  ngày (+/-7 ngày)" });
            kq.Add(new Ketqua { stt = 32, tendvct = "Chiều dài xương đùi (FL)~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 33, tendvct = "Dự kiến sinh", ketqua = "Ngày  tháng  năm" });
            kq.Add(new Ketqua { stt = 34, tendvct = "Chiều dài xương bàn chân~", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 35, tendvct = "Vị trí", ketqua = "Rau bám mặt sau tử cung" });
            kq.Add(new Ketqua { stt = 36, tendvct = "Mép bánh rau", ketqua = "Bình thường" });
            kq.Add(new Ketqua { stt = 37, tendvct = "Độ trưởng thành", ketqua = "Rau trưởng thành độ..." });
            kq.Add(new Ketqua { stt = 38, tendvct = "Dịch ối trong, số lượng ối", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 39, tendvct = "Dây rốn", ketqua = "2 động mạch, 1 tĩnh mạch" });
            kq.Add(new Ketqua { stt = 40, tendvct = "Vị trí gần dây rốn", ketqua = "Bình thường" });
            return kq;
        }

        public List<Ketqua> KQMauThai1()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Số lượng thai", ketqua = "  thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Cử động thai", ketqua = "" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Ngôi thai", ketqua = "" });
            kq.Add(new Ketqua { stt = 4, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Chu vi vòng đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Chu vi vòng bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Tim thai đều, tần số", ketqua = "  Ck/p" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Cân nặng ước tính", ketqua = "  Gram" });
            kq.Add(new Ketqua { stt = 10, tendvct = "Tuổi thai ước tính", ketqua = "  tuần  ngày(+/-10 ngày)" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Ngày sinh dự kiến", ketqua = "  /   /201  (+/-07 ngày)" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Hàm mặt - Tứ chi", ketqua = "Hiện không thấy bất thường" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Xương hộp sọ", ketqua = "phát triển tốt" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Cột sống - Hệ xương", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Các não thất", ketqua = "không giãn" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Tim thai", ketqua = "Cấu trúc 4 buồng, không giãn" });
            return kq;
        }
        public List<Ketqua> KQMauThai2()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Chu vi vòng đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Chu vi vòng bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Tim thai đều, tần số", ketqua = "  Ck/p" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Cân nặng ước tính", ketqua = "  Gram" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Thành bụng", ketqua = "đóng kín" });
            kq.Add(new Ketqua { stt = 8, tendvct = "- Cơ hoành", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 9, tendvct = "- Dạ dày", ketqua = "trong ổ bụng" });
            kq.Add(new Ketqua { stt = 10, tendvct = "- Hai thận", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 11, tendvct = "- Bàng quang", ketqua = "trong hổ chậu" });
            kq.Add(new Ketqua { stt = 12, tendvct = "- Lồng ngực", ketqua = "Cấu trúc của phổi đồng nhất" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Rau thai", ketqua = "Vị trí: Rau bám mặt trước đáy tử cung mật độ đều" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Nước ối", ketqua = "Dịch ối trong, số lượng ối bình thường" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Dây rốn", ketqua = "Cửa sổ Doppler hai động mạch, một tĩnh mạch" });
            return kq;
        }

        public List<Ketqua> KQThai3ThangDau()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Tử cung", ketqua = " Trong buồng tử cung có hình ảnh 01 thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Ngôi thai", ketqua = "Chưa cố định" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Tim thai", ketqua = "  ck/p" });
            kq.Add(new Ketqua { stt = 5, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Chiều dài xương mũi", ketqua = "  bình thường" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chiều dài đầu mông(CRL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Khoảng sáng sau gáy", ketqua = "  mm" });

            kq.Add(new Ketqua { stt = 10, tendvct = "Chu vi bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Thoát vị thành bụng", ketqua = "  âm tính" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Dạ dày", ketqua = "bình thường" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Tuổi thai ước tính theo siêu âm", ketqua = "  tuần  ngày" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Dự kiến sinh theo siêu âm", ketqua = "  /  /  " });
            kq.Add(new Ketqua { stt = 18, tendvct = "Cân nặng ước tính", ketqua = "  Gram" });
            kq.Add(new Ketqua { stt = 19, tendvct = "Rau thai", ketqua = "Vị trí: Rau bám diện rộng" });
            kq.Add(new Ketqua { stt = 20, tendvct = "Rau thai", ketqua = "Độ dày bánh rau:  mm" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Nước ối", ketqua = "Số lượng trung bình" });
            kq.Add(new Ketqua { stt = 22, tendvct = "Nước ối", ketqua = "Dịch ối đồng nhất" });
            kq.Add(new Ketqua { stt = 23, tendvct = "Khác", ketqua = " " });

            return kq;
        }

        public List<Ketqua> KQThai3ThangCuoi()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Tử cung", ketqua = " Trong buồng tử cung có hình ảnh 01 thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Ngôi thai", ketqua = " " });
            kq.Add(new Ketqua { stt = 3, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Tim thai", ketqua = "  ck/p" });
            kq.Add(new Ketqua { stt = 5, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "Kích thước chẩm trán (OFD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chu vi bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Tuổi thai ước tính theo siêu âm", ketqua = "  tuần  ngày" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Dự kiến sinh theo siêu âm", ketqua = "  / / " });
            kq.Add(new Ketqua { stt = 14, tendvct = "Cân nặng ước tính", ketqua = "  Gram ± " });
            kq.Add(new Ketqua { stt = 15, tendvct = "Rau thai", ketqua = "Vị trí: Rau bám diện rộng" });

            kq.Add(new Ketqua { stt = 16, tendvct = "Nước ối", ketqua = "Số lượng trung bình" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Nước ối", ketqua = "Dịch ối đồng nhất" });
            kq.Add(new Ketqua { stt = 18, tendvct = "Khác", ketqua = " " });

            return kq;
        }

        public List<Ketqua> KQThai3ThangGiua()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Tử cung", ketqua = " Trong buồng tử cung có hình ảnh 01 thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Ngôi thai", ketqua = " " });
            kq.Add(new Ketqua { stt = 3, tendvct = "Tim thai", ketqua = " lần/phút" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 5, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "ĐK chẩm trán đầu(OFD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chu vi bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Kích thước ngang tiểu não", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Não thất bên", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Hố sau", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Chiều dài xương mũi", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Khoảng cách hai hốc mắt", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Hình ảnh vòm sọ", ketqua = "  bình thường" });
            kq.Add(new Ketqua { stt = 18, tendvct = "Cột sống – hệ xương", ketqua = "  không thấy bất bình thường" });
            kq.Add(new Ketqua { stt = 19, tendvct = "Cấu trúc tim thai", ketqua = "  4 buồng" });
            kq.Add(new Ketqua { stt = 20, tendvct = "Cấu trúc phổi", ketqua = "  đồng nhất, không có nang tuyến" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Thoát vị", ketqua = " Không có hình ảnh thoát vị thành ngực, thành bụng" });
            kq.Add(new Ketqua { stt = 22, tendvct = "Dạ dày", ketqua = "  bình thường" });
            kq.Add(new Ketqua { stt = 23, tendvct = "Quai ruột ", ketqua = " Không thấy hình ảnh quai ruột giãn" });
            kq.Add(new Ketqua { stt = 24, tendvct = "Thận ", ketqua = " hai bên bình thường" });
            kq.Add(new Ketqua { stt = 25, tendvct = "Tuổi thai ước tính theo siêu âm", ketqua = "  tuần  ngày" });
            kq.Add(new Ketqua { stt = 26, tendvct = "Dự kiến sinh theo siêu âm", ketqua = "  / / " });
            kq.Add(new Ketqua { stt = 27, tendvct = "Cân nặng ước tính", ketqua = "  Gram ± 200 gram " });
            kq.Add(new Ketqua { stt = 28, tendvct = "Rau thai", ketqua = " Vị trí: Rau bám mặt tử cung" });
            kq.Add(new Ketqua { stt = 29, tendvct = "Rau thai", ketqua = "Độ dày bánh rau:  mm" });
            kq.Add(new Ketqua { stt = 30, tendvct = "Nước ối", ketqua = " Số lượng trung bình" });
            kq.Add(new Ketqua { stt = 31, tendvct = "Dây rốn", ketqua = " 03 mạch máu" });
            return kq;
        }

        public List<Ketqua> KQThaiDoi1()
        {
            List<Ketqua> kq = new List<Ketqua>();
            kq.Add(new Ketqua { stt = 1, tendvct = "Tử cung", ketqua = " Trong buồng tử cung có hình ảnh 02 thai" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Ngôi thai", ketqua = " chưa cố định" });
            kq.Add(new Ketqua { stt = 3, tendvct = "Cử động thai", ketqua = "Tốt" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Tim thai", ketqua = " ck/p" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Chiều dài đầu mông (CRL)", ketqua = " mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chu vi bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 10, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Thoát vị thành bụng", ketqua = "  âm tính" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Dạ dày", ketqua = "  bình thường" });
            kq.Add(new Ketqua { stt = 14, tendvct = "Chiều dài xương mũi", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 15, tendvct = "Khoảng sáng sau gáy", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 16, tendvct = "Cân nặng ước tính", ketqua = "  Gram" });
            kq.Add(new Ketqua { stt = 17, tendvct = "Tuổi thai ước tính theo siêu âm", ketqua = "  tuần  ngày" });
            kq.Add(new Ketqua { stt = 18, tendvct = "Dự kiến sinh theo siêu âm", ketqua = "  / / " });
            kq.Add(new Ketqua { stt = 19, tendvct = "Rau thai", ketqua = " Vị trí: Rau bám mặt trước" });
            kq.Add(new Ketqua { stt = 20, tendvct = "Rau thai", ketqua = "Độ dày bánh rau:  mm" });
            kq.Add(new Ketqua { stt = 21, tendvct = "Nước ối", ketqua = " Số lượng trung bình" });
            kq.Add(new Ketqua { stt = 22, tendvct = "Nước ối", ketqua = " Dịch ối đồng nhất" });
            kq.Add(new Ketqua { stt = 23, tendvct = "Khác", ketqua = " " });
            return kq;
        }
        public List<Ketqua> KQThaiDoi2()
        {
            List<Ketqua> kq = new List<Ketqua>();

            kq.Add(new Ketqua { stt = 1, tendvct = "Tim thai", ketqua = " ck/p" });
            kq.Add(new Ketqua { stt = 2, tendvct = "Chiều dài đầu mông (CRL)", ketqua = " mm" });
            kq.Add(new Ketqua { stt = 3, tendvct = "ĐK lưỡng đỉnh(BPD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 4, tendvct = "Chu vi đầu(HC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 5, tendvct = "Chu vi bụng(AC)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 6, tendvct = "ĐK trước sau bụng(APTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 7, tendvct = "ĐK ngang bụng(TTD)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 8, tendvct = "Chiều dài xương đùi(FL)", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 9, tendvct = "Thoát vị thành bụng", ketqua = "  âm tính" });
            kq.Add(new Ketqua { stt = 10, tendvct = "Dạ dày", ketqua = "  bình thường" });
            kq.Add(new Ketqua { stt = 11, tendvct = "Chiều dài xương mũi", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 12, tendvct = "Khoảng sáng sau gáy", ketqua = "  mm" });
            kq.Add(new Ketqua { stt = 13, tendvct = "Cân nặng ước tính", ketqua = "  Gram" });

            return kq;
        }


        int mabn = 0, madvcd = 0, _idcd = 0;
        bool _tamthu = true;
        string _LoaiThangThaiNhi = "";


        private void frm_KetQuaSieuAmThai4D_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in db.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            LupCanBo.Properties.DataSource = c.ToList();
            var madv = (from ts in db.TaiSans.Where(p => p.MaKP == _maKP) select new { ts.MaDV }).ToList();
            var mamay = (from m in madv join dv in db.DichVus on m.MaDV equals dv.MaDV select new { dv.MaQD, dv.TenDV }).ToList();
            lupMaMay.Properties.DataSource = mamay;
            if (mamay.Count > 0)
            {
                lupMaMay.Properties.DataSource = mamay;
                lupMaMay.EditValue = mamay.First().MaQD;
            }
            var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                         join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                         select new { cls.NgayTH, cls.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();

            if (kqcls.Count > 0)
            {
                _idcd = kqcls.First().IDCD;
                if (kqcls.First().Status == 0)
                {
                    TTLuu = 0;
                    LoadKQMau();
                    EnabledControl(false);
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    { mmKLSieuam.Text = "Trong tử cung có hình ảnh thai ~ thai  tuần  ngày (+/- 7 ngày)"; }
                    else
                    {
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01049")
                        {
                            mmKLSieuam.Text = "• Trong tử cung có hình ảnh một thai ~ thai   tuần   ngày." + Environment.NewLine + "• Hiện tại không phát hiện bất thường trên siêu âm " + Environment.NewLine + "• Hẹn khám lại khi thai    tuần";
                        }
                        else
                            mmKLSieuam.Text = "Hình ảnh   thai tương đương  tuần  ngày trong buồng tử cung. Hiện tại phát triển bình thường";
                    }

                }
                else
                {
                    EnabledControl(true);
                    TTLuu = 0;
                    if (kqcls.First().KetQua != null && kqcls.First().KetQua.Contains(";"))
                    {
                        LoadKetQua(kqcls.First().KetQua);
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01049")
                        {
                            string s = txttendv.Text;
                            if (rgSoThai.SelectedIndex == 0)
                            {
                                if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng đầu" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng đầu")
                                {
                                    List<Ketqua> kq = KQThai3ThangDau();
                                    grcketqua.DataSource = kq;
                                }
                                if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng giữa" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng giữa")
                                {
                                    List<Ketqua> kq = KQThai3ThangGiua();
                                    grcketqua.DataSource = kq;
                                }
                                if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng cuối" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng cuối")
                                {
                                    List<Ketqua> kq = KQThai3ThangCuoi();
                                    grcketqua.DataSource = kq;
                                }
                            }
                            else
                            {
                                if (s.Contains("Siêu âm thai"))
                                {
                                    List<Ketqua> kq1 = KQThaiDoi1();
                                    grcKQThai1.DataSource = kq1.OrderBy(p => p.stt);
                                    List<Ketqua> kq2 = KQThaiDoi2();
                                    grcKQThai2.DataSource = kq2.OrderBy(p => p.stt);
                                }
                            }
                        }
                    }
                }
                if (kqcls.First().DuongDan != null && File.Exists(kqcls.First().DuongDan))
                {
                    _fileanh = kqcls.First().DuongDan;
                    ptSieuam.Image = Image.FromFile(_fileanh);
                }
                else
                    ptSieuam.Image = null;
                if (kqcls.First().DuongDan2 != null && File.Exists(kqcls.First().DuongDan2))
                {
                    _fileanh2 = kqcls.First().DuongDan2;
                    ptSieuam2.Image = Image.FromFile(_fileanh2);
                }
                else
                    ptSieuam2.Image = null;

                if (!string.IsNullOrWhiteSpace(kqcls.First().KetLuan))
                {
                    mmKLSieuam.Text = kqcls.First().KetLuan;
                }
                if (kqcls.First().MaBNhan != null)
                {
                    mabn = kqcls.First().MaBNhan ?? 0;
                    var bn = db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                    if (bn != null)
                    {
                        txttenbn.Text = bn.TenBNhan;
                        txtmabn.Text = bn.MaBNhan.ToString();
                    }
                }
                if (kqcls.First().MaDV != null)
                {
                    madvcd = kqcls.First().MaDV ?? 0;
                    var tendv = db.DichVus.Where(p => p.MaDV == madvcd).Select(p => p.TenDV).FirstOrDefault();
                    if (tendv != null)
                        txttendv.Text = tendv;
                }
                if (kqcls.First().LoiDan != null)
                {
                    mmLoidanSieuAm.Text = kqcls.First().LoiDan;
                }
                if (kqcls.First().NgayTH != null)
                {
                    lupNgayTH.DateTime = Convert.ToDateTime(kqcls.First().NgayTH);
                }
                else
                    lupNgayTH.DateTime = DateTime.Now;
                if (kqcls.First().MaCBth != null && kqcls.First().MaCBth.ToString() != "")
                {
                    LupCanBo.EditValue = kqcls.First().MaCBth;
                }
                else
                {
                    if (kqcls.First().Status == 1)
                        LupCanBo.EditValue = "";
                    else LupCanBo.EditValue = DungChung.Bien.MaCB;
                }
                if (!string.IsNullOrEmpty(kqcls.First().MaMay))
                    lupMaMay.EditValue = kqcls.First().MaMay;
                if (!DungChung.Ham._checkTamThu(db, String.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), _idcls))
                {
                    _tamthu = false;
                }
            }
            else
            {

            }

            //if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            //{
            //    rgSoThai.Properties.Items.Clear();
            //    rgSoThai.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            //new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Kết quả 01 thai")});
            //    xtraTabControl1.TabPages[1].PageVisible = false;
            //}
        }


        void LoadKQMau()
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01049")
            {
                if (rgSoThai.SelectedIndex == 0)
                {

                    xpage2thai.PageEnabled = false;
                    xpage1thai.PageEnabled = true;
                    xtraTabControl1.SelectedTabPage = xpage1thai;
                    var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                                 join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                                 select new { cls.NgayTH, cd.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();
                    madvcd = kqcls.First().MaDV ?? 0;
                    var tendv = db.DichVus.Where(p => p.MaDV == madvcd).Select(p => p.TenDV).FirstOrDefault();
                    if (tendv != null)
                    {
                        txttendv.Text = tendv;
                        grcketqua.DataSource = null;
                       if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng đầu" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng đầu")
                        {
                            List<Ketqua> kq = KQThai3ThangDau();
                            grcketqua.DataSource = kq;
                            if (DungChung.Bien.MaBV == "24297" && (mmLoidanSieuAm.Text.Trim() == "" || TTLuu == 0))
                                mmLoidanSieuAm.Text = "- Siêu âm không ảnh hưởng tới sức khỏe của Mẹ và Thai nhi. Khi mang thai nên uống 2-2,5 lít nước mỗi ngày. " + Environment.NewLine + " - Siêu âm có thể phát hiện khoảng 75-80% các dị tật thường gặp ở thai nhi.";
                        }
                        if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng giữa" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng giữa")
                        {
                            List<Ketqua> kq = KQThai3ThangGiua();
                            grcketqua.DataSource = kq;
                            if (DungChung.Bien.MaBV == "24297" && (mmLoidanSieuAm.Text.Trim() == "" || TTLuu == 0))
                                mmLoidanSieuAm.Text = "- Siêu âm không ảnh hưởng tới sức khỏe của Mẹ và Thai nhi. Khi mang thai nên uống 2-2,5 lít nước mỗi ngày. " + Environment.NewLine + "- Tripple test ở tuổi thai từ 14-20 tuần ( tốt nhất ở tuần thứ 16). Đặc biệt, nhất định phải làm XN Tripple test nếu giai đoạn trước lỡ không làm Double test." + Environment.NewLine + " - Siêu âm có thể phát hiện khoảng 75-80% các dị tật thường gặp ở thai nhi." + Environment.NewLine + "- Giai đoạn kế tiếp ( 20 – 24 tuần) rất quan trọng để kiểm tra các dị tật bẩm sinh, đặc biệt là dị tật ở Tim, môi, não,…nên thai phụ phải nhớ đi khám theo hẹn của Bs.";
                        }
                        if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng cuối" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng cuối")
                        {
                            List<Ketqua> kq = KQThai3ThangCuoi();
                            grcketqua.DataSource = kq;
                            if (DungChung.Bien.MaBV == "24297" && (mmLoidanSieuAm.Text.Trim() == "" || TTLuu == 0))
                                mmLoidanSieuAm.Text = "- Siêu âm không ảnh hưởng tới sức khỏe của Mẹ và Thai nhi. Khi mang thai nên uống 2-2,5 lít nước mỗi ngày. " + Environment.NewLine + " - Siêu âm có thể phát hiện khoảng 75-80% các dị tật thường gặp ở thai nhi." + Environment.NewLine + "- Từ giai đoạn này, thai phụ cần xét nghiệm máu ( chức năng gan, thận, đường máu,…), XN nước tiểu ( 10 thông số) ít nhất 04 tuần/ lần và đo huyết áp thường xuyên để phát hiện các bệnh lý hay gặp khi mang thai ( như tiểu đường, tiền sản giật và các biến chứng của tiền sản giật,….)";
                        }
                        if (mmKLSieuam.Text.Trim() == "" || TTLuu == 0)
                        {
                            mmKLSieuam.Text = "• Trong tử cung có hình ảnh một thai ~ thai    tuần    ngày." + Environment.NewLine + "• Hiện tại không phát hiện bất thường trên siêu âm " + Environment.NewLine + "• Hẹn khám lại khi thai    tuần";
                        }
                    }
                }
                else
                {
                    xpage1thai.PageEnabled = false;
                    xpage2thai.PageEnabled = true;
                    xtraTabControl1.SelectedTabPage = xpage2thai;
                    var tendv = db.DichVus.Where(p => p.MaDV == madvcd).Select(p => p.TenDV).FirstOrDefault();
                    if (tendv != null)
                    {
                        txttendv.Text = tendv;
                        grcKQThai1.DataSource = null;
                        grcKQThai2.DataSource = null;
                        if (tendv.Contains("Siêu âm thai"))
                        {
                            List<Ketqua> kq1 = KQThaiDoi1();
                            grcKQThai1.DataSource = kq1.OrderBy(p => p.stt);
                            List<Ketqua> kq2 = KQThaiDoi2();
                            grcKQThai2.DataSource = kq2.OrderBy(p => p.stt);
                        }
                    }

                    if (mmKLSieuam.Text.Trim() == "" || TTLuu == 0)
                    {
                        mmKLSieuam.Text = "• Trong tử cung có hình ảnh một thai ~ thai   tuần   ngày." + Environment.NewLine + "• Hiện tại không phát hiện bất thường trên siêu âm " + Environment.NewLine + "• Hẹn khám lại khi thai    tuần";
                    }
                    if (DungChung.Bien.MaBV == "24297" && (mmLoidanSieuAm.Text.Trim() == "" || TTLuu == 0))
                        mmLoidanSieuAm.Text = "- Siêu âm không ảnh hưởng tới sức khỏe của Mẹ và Thai nhi. Khi mang thai nên uống 2-2,5 lít nước mỗi ngày." + Environment.NewLine + "- Siêu âm có thể phát hiện khoảng 75-80% các dị tật thường gặp ở thai nhi." + Environment.NewLine + "- Mang thai đôi có nhiều nguy cơ về dị tật, các biến cố trong thai kỳ ( đẻ non, ối vỡ non-sớm,…) hơn những trường hợp thai một. ";
                }
            }

            else
            {
                if (rgSoThai.SelectedIndex == 0)
                {
                    xpage2thai.PageEnabled = false;
                    xpage1thai.PageEnabled = true;
                    xtraTabControl1.SelectedTabPage = xpage1thai;
                    if (grcketqua.DataSource == null || TTLuu == 0)
                    {
                        List<Ketqua> kq = (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? KQMau_12345() : KQMau();
                        grcketqua.DataSource = kq.OrderBy(p => p.stt);
                    }
                    if (mmKLSieuam.Text.Trim() == "" || TTLuu == 0)
                    {
                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                        {
                            mmKLSieuam.Text = "Trong tử cung có hình ảnh thai ~ thai  tuần  ngày (+/- 7 ngày)";
                        }
                        else
                            mmKLSieuam.Text = "Hình ảnh 01 thai tương đương  tuần  ngày trong buồng tử cung. Hiện tại phát triển bình thường";
                    }
                    
                }
                else
                {
                    xpage1thai.PageEnabled = false;
                    xpage2thai.PageEnabled = true;
                    xtraTabControl1.SelectedTabPage = xpage2thai;
                    if ((grcKQThai1.DataSource == null && grcKQThai2.DataSource == null) || TTLuu == 0)
                    {
                        List<Ketqua> kq1 = KQMauThai1();
                        grcKQThai1.DataSource = kq1.OrderBy(p => p.stt);
                        List<Ketqua> kq2 = KQMauThai2();
                        grcKQThai2.DataSource = kq2.OrderBy(p => p.stt);
                    }
                    if (mmKLSieuam.Text.Trim() == "" || TTLuu == 0)
                    {
                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                        {
                            mmKLSieuam.Text = "Trong tử cung có hình ảnh thai ~ thai  tuần  ngày (+/- 7 ngày)";
                        }
                        else
                            mmKLSieuam.Text = "Hình ảnh 02 thai tương đương  tuần  ngày trong buồng tử cung. Hiện tại phát triển bình thường";
                    }
                    
                }
            }
        }
        int TTLuu = -1;


        void LoadKetQua(string KetQua)
        {
            #region Bệnh viên 01071 || 01049
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01409")
            {
                if (KetQua.Contains("|"))
                {
                    giaodien = 4;
                    rgSoThai.SelectedIndex = 1;
                    rgSoThai.Enabled = false;
                    xtraTabControl1.SelectedTabPage = xpage2thai;
                    xpage1thai.PageEnabled = false;
                    string[] arr = KetQua.Split('|');
                    if (arr[0].Trim() != "")
                    {
                        string[] arrkq = arr[0].Split(';');
                        int stt = 1;
                        List<Ketqua> kq = new List<Ketqua>();
                        foreach (var item in arrkq)
                        {
                            Ketqua moi = new Ketqua();
                            if (item.Contains(":"))
                            {
                                
                                string[] ar = item.Split(':');
                                if (ar.Count() <= 2)
                                {
                                    moi.stt = stt;
                                    moi.tendvct = ar[0];
                                    moi.ketqua = ar[1];
                                }
                                else
                                {
                                    moi.stt = stt;
                                    moi.tendvct = ar[0];
                                    moi.ketqua = ar[1] + ":" + ar[2];
                                }
                                
                            }
                            else
                            {
                                moi.stt = stt;
                                moi.tendvct = item;
                            }
                            kq.Add(moi);
                        }
                        grcKQThai1.DataSource = null;
                        grcKQThai1.DataSource = kq;
                    }
                    if (arr[1].Trim() != "")
                    {
                        string[] arrkq = arr[1].Split(';');
                        int stt = 1;
                        List<Ketqua> kq = new List<Ketqua>();
                        foreach (var item in arrkq)
                        {
                            Ketqua moi = new Ketqua();
                            if (item.Contains(":"))
                            {
                                string[] ar = item.Split(':');
                                moi.stt = stt;
                                moi.tendvct = ar[0];
                                moi.ketqua = ar[1];
                            }
                            else
                            {
                                moi.stt = stt;
                                moi.tendvct = item;
                            }
                            kq.Add(moi);
                        }
                        grcKQThai2.DataSource = null;
                        grcKQThai2.DataSource = kq;
                    }
                }
                else
                {
                    rgSoThai.SelectedIndex = 0;
                    rgSoThai.Enabled = false;
                    xtraTabControl1.SelectedTabPage = xpage1thai;
                    xpage2thai.PageEnabled = false;
                    string[] arrkq = KetQua.Split(';');
                    int stt = 1;
                    List<Ketqua> kq = new List<Ketqua>();
                    foreach (var item in arrkq)
                    {
                        Ketqua moi = new Ketqua();
                        if (item.Contains(":"))
                        {
                            string[] ar = item.Split(':');
                            if (item.Contains("Rau thai") && ar.Length > 2)
                            {
                                moi.stt = stt;
                                moi.tendvct = ar[0];

                                moi.ketqua = ar[1] + ": " + ar[2];

                            }
                            else
                            {
                                moi.stt = stt;
                                moi.tendvct = ar[0];
                                moi.ketqua = ar[1];
                            }
                        }
                        else
                        {
                            moi.stt = stt;
                            moi.tendvct = item;
                        }
                        kq.Add(moi);
                    }
                    grcketqua.DataSource = null;
                    grcketqua.DataSource = kq;
                    if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng đầu" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng đầu")
                    {
                        giaodien = 1;
                    }
                    if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng giữa" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng giữa")
                    {
                        giaodien = 2;
                    }
                    if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng cuối" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng cuối")
                    {
                        giaodien = 3;
                    }
                }
            }
            #endregion
            else
            {
                #region BV Khac 01071
                if (KetQua.Contains("|"))
                {
                    rgSoThai.SelectedIndex = 1;
                    rgSoThai.Enabled = false;
                    xtraTabControl1.SelectedTabPage = xpage2thai;
                    xpage1thai.PageEnabled = false;
                    string[] arr = KetQua.Split('|');
                    if (arr[0].Trim() != "")
                    {
                        string[] arrkq = arr[0].Split(';');
                        int stt = 1;
                        List<Ketqua> kq = new List<Ketqua>();
                        foreach (var item in arrkq)
                        {
                            Ketqua moi = new Ketqua();
                            if (item.Contains(":"))
                            {
                                string[] ar = item.Split(':');
                                moi.stt = stt;
                                moi.tendvct = ar[0];
                                moi.ketqua = ar[1];
                            }
                            else
                            {
                                moi.stt = stt;
                                moi.tendvct = item;
                            }
                            kq.Add(moi);
                        }
                        grcKQThai1.DataSource = null;
                        grcKQThai1.DataSource = kq;
                    }
                    if (arr[1].Trim() != "")
                    {
                        string[] arrkq = arr[1].Split(';');
                        int stt = 1;
                        List<Ketqua> kq = new List<Ketqua>();
                        foreach (var item in arrkq)
                        {
                            Ketqua moi = new Ketqua();
                            if (item.Contains(":"))
                            {
                                string[] ar = item.Split(':');
                                moi.stt = stt;
                                moi.tendvct = ar[0];
                                moi.ketqua = ar[1];
                            }
                            else
                            {
                                moi.stt = stt;
                                moi.tendvct = item;
                            }
                            kq.Add(moi);
                        }
                        grcKQThai2.DataSource = null;
                        grcKQThai2.DataSource = kq;
                    }
                }
                else
                {
                    rgSoThai.SelectedIndex = 0;
                    rgSoThai.Enabled = false;
                    xtraTabControl1.SelectedTabPage = xpage1thai;
                    xpage2thai.PageEnabled = false;
                    string[] arrkq = KetQua.Split(';');
                    int stt = 1;
                    List<Ketqua> kq = new List<Ketqua>();
                    foreach (var item in arrkq)
                    {
                        Ketqua moi = new Ketqua();
                        if (item.Contains(":"))
                        {
                            string[] ar = item.Split(':');
                            moi.stt = stt;
                            moi.tendvct = ar[0];
                            moi.ketqua = ar[1];
                        }
                        else
                        {
                            moi.stt = stt;
                            moi.tendvct = item;
                        }
                        kq.Add(moi);
                    }
                    grcketqua.DataSource = null;
                    grcketqua.DataSource = kq;
                }
                #endregion
            }

        }
        string _fileanh = "", _fileanh2 = "";
        Boolean suaanhSA = true;
        private void sbtChonanhSA1_Click(object sender, EventArgs e)
        {
            bool tontai = true;
            if (ptSieuam.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        _fileanh = fileName;
                        if (!string.IsNullOrEmpty(_fileanh))
                            ptSieuam.Image = Image.FromFile(_fileanh);
                        else
                            ptSieuam.Image = null;
                        suaanhSA = true;
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh = fileName;
                    if (!string.IsNullOrEmpty(_fileanh))
                        ptSieuam.Image = Image.FromFile(_fileanh);
                    else
                        ptSieuam.Image = null;
                    suaanhSA = true;
                }
            }

        }

        private void sbtChonanhSA2_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (ptSieuam2.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh2 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh2))
                        ptSieuam2.Image = Image.FromFile(_fileanh2);
                    else
                        ptSieuam2.Image = null;
                    suaanhSA = true;
                }
            }
        }

        public string LuuKetQua()
        {
            if (rgSoThai.SelectedIndex == 0)
            {
                string kq = "";
                for (int i = 0; i < grvketqua.DataRowCount; i++)
                {
                    kq += grvketqua.GetRowCellValue(i, coltendvct).ToString() + (grvketqua.GetRowCellValue(i, colketqua) != null ? (":" + grvketqua.GetRowCellValue(i, colketqua).ToString()) : "") + ";";
                }
                return kq;
            }
            else
            {
                string kq = "", kq1 = "", kq2 = "";
                for (int i = 0; i < grvKQThai1.DataRowCount; i++)
                {
                    kq1 += grvKQThai1.GetRowCellValue(i, coltendvct).ToString() + (grvKQThai1.GetRowCellValue(i, colketqua) != null ? (":" + grvKQThai1.GetRowCellValue(i, colketqua).ToString()) : "") + ";";
                }
                for (int i = 0; i < grvKQThai2.DataRowCount; i++)
                {
                    kq2 += grvKQThai2.GetRowCellValue(i, coltendvct).ToString() + (grvKQThai2.GetRowCellValue(i, colketqua) != null ? (":" + grvKQThai2.GetRowCellValue(i, colketqua).ToString()) : "") + ";";
                }
                kq = kq1 + "|" + kq2;
                return kq;
            }
        }
        private void sbtXoaanhSA1_Click(object sender, EventArgs e)
        {
            _fileanh = "";
            ptSieuam.Image = null;
            suaanhSA = true;
        }

        private void sbtXoaanhSA2_Click(object sender, EventArgs e)
        {
            _fileanh2 = "";
            ptSieuam2.Image = null;
            suaanhSA = true;
        }
        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_tamthu == false)
            {
                MessageBox.Show("Bệnh nhân chưa nộp tiền dịch vụ, bạn không thể lưu");
            }
            if (_tamthu && KT())
            {
                db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                bool tieptuc = true;
                if (string.IsNullOrEmpty(mmKLSieuam.Text) || LupCanBo.EditValue == null || LupCanBo.EditValue == null)
                {
                    DialogResult _dresult3 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_dresult3 != DialogResult.Yes)
                    {
                        tieptuc = false;
                    }

                }
                List<CL> cls = db.CLS.Where(p => p.IdCLS == _idcls).ToList();
                List<ChiDinh> cd = db.ChiDinhs.Where(p => p.IdCLS == _idcls).ToList();
                List<CLSct> clsct = db.CLScts.Where(p => p.IDCD == _idcd).ToList();
                if (tieptuc)
                {

                    if (LupCanBo.EditValue != null)
                        cls.First().MaCBth = LupCanBo.EditValue.ToString();
                    else
                        cls.First().MaCBth = "";
                    cls.First().Status = 1;
                    foreach (var item in cd)
                    {
                        item.KetLuan = mmKLSieuam.Text;
                        item.LoiDan = mmLoidanSieuAm.Text;
                        item.TamThu = 1;
                        item.Status = 1;
                        item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    }
                    string _tenfileanh = "", _tenfileanh2 = "";
                    //fileanh1
                    if (!string.IsNullOrEmpty(_fileanh))
                    {
                        _tenfileanh = DungChung.Bien.DuongDan + "\\";
                        _tenfileanh += mabn + _idcls + ".jpg";
                        try
                        {
                            if (!string.IsNullOrEmpty(_fileanh))
                            {
                                if (!File.Exists(_tenfileanh))
                                {
                                    File.Copy(_fileanh, _tenfileanh);
                                }
                                else
                                {
                                    DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dresult1 == DialogResult.Yes)
                                    {
                                        _tenfileanh = layTenFileAnh(_fileanh, _tenfileanh);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("không lưu được ảnh");
                        }
                    }
                    //fileanh2
                    if (!string.IsNullOrEmpty(_fileanh2))
                    {
                        _tenfileanh2 = DungChung.Bien.DuongDan + "\\";
                        _tenfileanh2 += mabn + _idcls + ".jpg";
                        try
                        {
                            if (!string.IsNullOrEmpty(_fileanh2))
                            {
                                if (!File.Exists(_tenfileanh2))
                                {
                                    File.Copy(_fileanh2, _tenfileanh2);
                                }
                                else
                                {
                                    DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dresult1 == DialogResult.Yes)
                                    {
                                        _tenfileanh2 = layTenFileAnh(_fileanh2, _tenfileanh2);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("không lưu được ảnh");
                        }
                    }
                    foreach (var a in clsct)
                    {
                        a.KetQua = LuuKetQua();
                        if (suaanhSA)
                        {
                            a.DuongDan = _tenfileanh;
                            a.DuongDan2 = _tenfileanh2;
                        }
                    }
                }
                string kl = "";
                foreach (var b in cd)
                {
                    int ID = b.IDCD;
                    var suacd = db.ChiDinhs.Single(p => p.IDCD == ID);
                    suacd.KetLuan = b.KetLuan;
                    kl = b.KetLuan;
                    suacd.LoiDan = b.LoiDan;
                    suacd.GhiChu = b.GhiChu;
                    // suacd.SoPhieu = b.SoPhieu;
                    suacd.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    suacd.Status = 1;
                    suacd.TamThu = b.TamThu;
                    suacd.NgayTH = lupNgayTH.DateTime;
                    if (lupMaMay.EditValue != null)
                        suacd.MaMay = lupMaMay.EditValue.ToString();
                    if (LupCanBo.EditValue != null)
                        suacd.MaCBth = LupCanBo.EditValue.ToString();
                    db.SaveChanges();
                }
                foreach (var c in clsct)
                {
                    var suaclsct = db.CLScts.Single(p => p.Id == c.Id);
                    suaclsct.DuongDan = c.DuongDan;
                    suaclsct.DuongDan2 = c.DuongDan2;
                    suaclsct.KetQua = c.KetQua;
                    //suaclsct.MaCB = c.MaCB;
                    //suaclsct.Ngaythang = c.Ngaythang;
                    suaclsct.SoPhieu = c.SoPhieu;
                    if ((!String.IsNullOrEmpty(c.KetQua) && c.KetQua.Length > 0) || !string.IsNullOrEmpty(kl))
                    {
                        suaclsct.Status = 1;
                    }
                    else
                    {
                        suaclsct.Status = c.Status;
                    }
                    suaclsct.STTHT = c.STTHT;
                    db.SaveChanges();
                }
                int makp = 0;
                foreach (var a in cls)
                {
                    var suacls = db.CLS.Single(p => p.IdCLS == a.IdCLS);
                    makp = a.MaKP == null ? 0 : a.MaKP.Value;
                    suacls.MaCBth = a.MaCBth;
                    suacls.NgayTH = lupNgayTH.DateTime;
                    var ktstatuscd = db.ChiDinhs.Where(p => p.IdCLS == a.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suacls.Status = 0;
                    else
                    {
                        suacls.Status = 1;
                        BenhNhan sua = db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                        if (sua != null)
                        {
                            var b = db.BNKBs.Where(p => p.MaBNhan == mabn).ToList();
                            var vienphi = db.VienPhis.Where(p => p.MaBNhan == mabn).ToList();
                            if (b.Count > 0 && vienphi.Count <= 0)
                            {
                                sua.Status = 5;
                            }
                        }
                    }
                    db.SaveChanges();
                }
                //int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                var cdinh = (from cd1 in db.ChiDinhs.Where(p => p.IDCD == _idcd && p.Status == 1)
                             join dv in db.DichVus on cd1.MaDV equals dv.MaDV
                             select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV }).ToList();
                int iddthuoc = 0;
                //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                int _idkb = 0;
                var bnkb = db.BNKBs.Where(p => p.MaBNhan == mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = db.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                if (iddthuoc > 0)
                {
                    foreach (var cd2 in cdinh)
                    {
                        var kt = (from dt in db.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                        if (kt.Count <= 0)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(db, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, string.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = iddthuoc;
                            moi.DonVi = cd2.DonVi;
                            moi.TrongBH = cd.First().TrongBH == null ? 0 : cd.First().TrongBH.Value;
                            moi.IDCD = cd2.IDCD;
                            moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.XHH = cd2.XHH;
                            moi.LoaiDV = cd2.LoaiDV;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.MaKP = makp;
                            moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = _idcls;
                            db.DThuoccts.Add(moi);
                            db.SaveChanges();
                            var CheckGiaPhuThu = db.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                            var sss = db.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(db, moi.IDDonct, s);
                            }
                        }
                        else
                        {
                            foreach (var dt in kt)
                            {
                                dt.NgayNhap = lupNgayTH.DateTime;
                                dt.IDCLS = _idcls;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                    dthuoccd.MaBNhan = mabn;
                    dthuoccd.MaKP = cls.First().MaKP;
                    dthuoccd.MaCB = cls.First().MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    db.DThuocs.Add(dthuoccd);
                    if (db.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(db, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtmabn.Text) ? 0 : Convert.ToInt32(txtmabn.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            moi.TrongBH = cd.First().TrongBH == null ? 0 : cd.First().TrongBH.Value;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.MaKP = makp;
                            moi.IDCD = cd3.IDCD;
                            moi.DonVi = cd3.DonVi;
                            moi.XHH = cd3.XHH;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = _idcls;
                            db.DThuoccts.Add(moi);
                            db.SaveChanges();
                            var CheckGiaPhuThu = db.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = db.BenhNhans.Where(p => p.MaBNhan == mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(db, moi.IDDonct, s);
                            }
                        }
                    }
                }
                EnabledControl(true);
                //trangthaiLuu = 0;
                suaanhSA = false;
                _fileanh = "";
                _fileanh2 = "";

                //xpage1thai.PageEnabled = false;
                //xpage2thai.PageEnabled = false;
                rgSoThai.Enabled = false;
            }
        }
        private bool KT()
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int ot;
            int _int_maBN = 0;

            if (Int32.TryParse(txtmabn.Text, out ot))
                _int_maBN = Convert.ToInt32(txtmabn.Text);
            if (lupNgayTH.DateTime != null)
            {
                var _NgayCD = db.CLS.Where(p => p.IdCLS == _idcls).Select(p => p.NgayThang).FirstOrDefault();
                DateTime _NgayTH = lupNgayTH.DateTime;
                if (_NgayCD != null)
                {
                    if (_NgayTH < _NgayCD)
                    {

                        MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định", "Thông báo", MessageBoxButtons.OK);
                        lupNgayTH.Focus();
                        return false;
                    }
                    else
                    {
                        if (_NgayTH > DateTime.Now)
                        {

                            MessageBox.Show("Ngày Thực hiện không được > ngày hiện tại", "Thông báo", MessageBoxButtons.OK);
                            lupNgayTH.Focus();
                            return false;
                        }
                    }
                }
            }
            if (db.VienPhis.Where(p => p.MaBNhan == _int_maBN).Count() > 0)
            {
                MessageBox.Show("Bệnh Nhân đã thanh toán. Không thể lưu!.");
                return false;
            }
            return true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmabn.Text))
            {
                QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var vp = (from vpct in db.VienPhis.Where(p => p.MaBNhan == mabn) select new { vpct.idVPhi }).ToList();
                var rv = db.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                if (vp.Count > 0)
                { MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!"); }
                else if (rv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                }
                else
                {
                    DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dia == DialogResult.Yes)
                    {
                        List<CL> cls = db.CLS.Where(p => p.IdCLS == _idcls).ToList();
                        List<ChiDinh> cd = db.ChiDinhs.Where(p => p.IdCLS == _idcls).ToList();
                        List<CLSct> clsct = db.CLScts.Where(p => p.IDCD == _idcd).ToList();
                        int _maCK = 0;
                        var ck = (from nhom in db.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                  join dvu in db.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                  select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                        if (ck.Count > 0)
                            _maCK = ck.First().MaDV;
                        foreach (var b in cd)
                        {
                            int ID = b.IDCD;
                            var iddt = db.DThuoccts.Where(p => p.IDCD == ID && p.MaDV != _maCK).ToList();
                            if (iddt.Count > 0)
                            {
                                foreach (var item in iddt)
                                {
                                    int iddtct = item.IDDonct;
                                    var ktchiphi = db.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                                    if (ktchiphi.Count > 0)
                                    {
                                        MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                        return;
                                    }
                                    var xoa = db.DThuoccts.Single(p => p.IDDonct == iddtct);
                                    db.DThuoccts.Remove(xoa);
                                    db.SaveChanges();
                                }
                            }

                            var suacd = db.ChiDinhs.Single(p => p.IDCD == ID);
                            suacd.NgayTH = null;
                            suacd.KetLuan = "";
                            suacd.LoiDan = "";
                            suacd.MoTa = "";
                            //suacd.SoPhieu = 0;
                            suacd.Status = 0;
                            //suacd.TamThu = 1;
                            db.SaveChanges();
                        }
                        foreach (var c in clsct)
                        {
                            var suaclsct = db.CLScts.Single(p => p.Id == c.Id);
                            suaclsct.DuongDan = "";
                            suaclsct.DuongDan2 = "";
                            suaclsct.KetQua = "";
                            //suaclsct.MaCB = "";
                            //suaclsct.Ngaythang = null;
                            suaclsct.SoPhieu = 0;
                            suaclsct.Status = 0;
                            suaclsct.STTHT = 0;
                            db.SaveChanges();
                        }
                        foreach (var a in cls)
                        {
                            var suacls = db.CLS.Single(p => p.IdCLS == a.IdCLS);
                            suacls.MaCBth = "";
                            suacls.Status = 0;
                            suacls.NgayTH = null;
                            db.SaveChanges();
                        }
                        MessageBox.Show("Xoá thành công!");
                        frm_KetQuaSieuAmThai4D_Load(null, null);
                        ptSieuam.Image = null;
                        ptSieuam2.Image = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa! Bạn chưa chọn bệnh nhân.");
            }
        }
        //int trangthaiLuu=0;
        string[] arrDuongDan = new string[7];
        string strDD = "";
        private void EnabledControl(bool T)//status=1: true
        {
            btnLuu.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
            rgSoThai.Enabled = !T;
            sbtChonanhSA1.Enabled = !T;
            sbtChonanhSA2.Enabled = !T;
            sbtXoaanhSA1.Enabled = !T;
            sbtXoaanhSA2.Enabled = !T;
            lupNgayTH.Properties.ReadOnly = T;
            mmKLSieuam.Enabled = !T;
            mmLoidanSieuAm.Enabled = !T;
            LupCanBo.Properties.ReadOnly = T;
            lupMaMay.Properties.ReadOnly = T;
            this.grvketqua.OptionsBehavior.ReadOnly = T;
            this.grvKQThai1.OptionsBehavior.ReadOnly = T;
            this.grvKQThai2.OptionsBehavior.ReadOnly = T;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            //trangthaiLuu = 1;
            TTLuu = 2;

            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var vp = (from vpct in db.VienPhis.Where(p => p.MaBNhan == mabn) select new { vpct.idVPhi }).ToList();
            var rv = db.RaViens.Where(p => p.MaBNhan == mabn).ToList();
            if (vp.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
            }
            else
                if (rv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                }
                else
                {
                    EnabledControl(false);
                    mmKLSieuam.Properties.ReadOnly = false;
                    mmLoidanSieuAm.Properties.ReadOnly = false;
                    rgSoThai.Enabled = true;
                }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01049")
            {
                List<Ketqua> kq = new List<Ketqua>();
                List<BienKQ> _listBienKQ = new List<BienKQ>();
                var kqcls = (from cls in db.CLS.Where(p => p.IdCLS == _idcls)
                             join cd in db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join clsct in db.CLScts on cd.IDCD equals clsct.IDCD
                             select new { cls.NgayTH, cd.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2 }).ToList();
                if (kqcls.First().KetQua != null && kqcls.First().KetQua.Contains(";"))
                {
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01409")
                    {
                        #region Thai đôi
                        if (kqcls.First().KetQua.Contains("|"))
                        {
                            giaodien = 4;
                            rgSoThai.SelectedIndex = 1;
                            rgSoThai.Enabled = false;
                            xtraTabControl1.SelectedTabPage = xpage2thai;
                            xpage1thai.PageEnabled = false;
                            string[] arr = kqcls.First().KetQua.Split('|');

                            if (arr[0].Trim() != "")
                            {
                                string[] arrkq = arr[0].Split(';');
                                int stt = 1;
                                kq.Clear();
                                BienKQ _bienmoi = new BienKQ();
                                foreach (var item in arrkq)
                                {
                                    if (item.Contains("Tử cung"))
                                    {
                                        _bienmoi.TuCung = "•  " + item;
                                    }
                                    if (item.Contains("Ngôi thai"))
                                    {
                                        _bienmoi.NgoiThai = "•  " + item;
                                    }
                                    if (item.Contains("Cử động thai"))
                                    {
                                        _bienmoi.CuDongThai = "•  " + item;
                                    }
                                    if (item.Contains("Cân nặng"))
                                    {
                                        _bienmoi.CanNang = "•  " + item;
                                    }
                                    if (item.Contains("Cấu trúc phổi"))
                                    {
                                        _bienmoi.CauTrucPhoi = "•  " + item;
                                    }
                                    if (item.Contains("Cấu trúc tim thai"))
                                    {
                                        _bienmoi.CauTrucTimThai = "•  " + item;
                                    }
                                    if (item.Contains("CRL"))
                                    {
                                        _bienmoi.ChieuDaiDauMong_CRL = "•  " + item;
                                    }
                                    if (item.Contains("Chiều dài xương đùi"))
                                    {
                                        _bienmoi.ChieuDaiXuongDui_FL = "•  " + item;
                                    }
                                    if (item.Contains("Chiều dài xương mũi"))
                                    {
                                        _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                    }
                                    if (item.Contains("Chu vi bụng"))
                                    {
                                        _bienmoi.ChuViBung_AC = "•  " + item;
                                    }
                                    if (item.Contains("Chu vi đầu"))
                                    {
                                        _bienmoi.ChuViDau_HC = "•  " + item;
                                    }
                                    if (item.Contains("Cột sống"))
                                    {
                                        _bienmoi.CotSong = "•  " + item;
                                    }
                                    if (item.Contains("Kích thước ngang tiểu não"))
                                    {
                                        _bienmoi.KichThuocNgangTieuNao = "•  " + item;
                                    }
                                    if (item.Contains("Hố sau"))
                                    {
                                        _bienmoi.HoSau = "•  " + item;
                                    }
                                    if (item.Contains("Não thất bên"))
                                    {
                                        _bienmoi.NaoThatBen = "•  " + item;
                                    }
                                    if (item.Contains("Chiều dài xương mũi"))
                                    {
                                        _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                    }
                                    if (item.Contains("Khoảng cách hai hốc mắt"))
                                    {
                                        _bienmoi.KhoangCach2HocMat = "•  " + item;
                                    }
                                    if (item.Contains("Khoảng sáng sau gáy"))
                                    {
                                        _bienmoi.KhoangSangSauGay = "•  " + item;
                                    }
                                    if (item.Contains("Dạ dày"))
                                    {
                                        _bienmoi.DaDay = "•  " + item;
                                    }
                                    if (item.Contains("Quai ruột"))
                                    {
                                        _bienmoi.QuaiRuot = "•  " + item;
                                    }
                                    if (item.Contains("Thận"))
                                    {
                                        _bienmoi.Than = "•  " + item;
                                    }
                                    if (item.Contains("Hình ảnh vòm sọ"))
                                    {
                                        _bienmoi.HinhAnhVomSo = "•  " + item;
                                    }
                                    if (item.Contains("Tuổi thai ước tính"))
                                    {
                                        _bienmoi.TuoiThai = item;
                                    }
                                    if (item.Contains("Dự kiến sinh"))
                                    {
                                        _bienmoi.DuKienSinh = item;
                                    }
                                    if (item.Contains("OFD"))
                                    {
                                        _bienmoi.DKChamTranDau_OFD = "•  " + item;
                                    }
                                    if (item.Contains("BPD"))
                                    {
                                        _bienmoi.DKLuongDinh_BPD = "•  " + item;
                                    }
                                    if (item.Contains("APTD"))
                                    {
                                        _bienmoi.DKTruocSauBung_APTD = "•  " + item;
                                    }
                                    if (item.Contains("TTD"))
                                    {
                                        _bienmoi.DKNgangBung_TTD = "•  " + item;
                                    }
                                    if (item.Contains("Tuổi thai ước tính"))
                                    {
                                        _bienmoi.TuoiThai = item;
                                    }
                                    int t = 0;
                                    if (item.Contains("Rau thai"))
                                    {
                                        string[] ar = item.Split(':');
                                        if (ar.Count() > 2)
                                        {

                                            string s = ar[1] + ":" + ar[2];
                                            _bienmoi.RauThai += s + "\r\n";

                                        }
                                        else
                                        {
                                            string s = ar[1];
                                            _bienmoi.RauThai += s + "\r\n";
                                        }

                                    }
                                    if (item.Contains("Nước ối"))
                                    {
                                        string[] ar = item.Split(':');

                                        _bienmoi.NuocOi += ar[1] + "\r\n";
                                    }
                                    if (item.Contains("Khác"))
                                    {
                                        string[] ar = item.Split(':');
                                        if (ar[1] != null && ar[1].Length > 0)
                                            _bienmoi.Khac = ar[1];
                                        else _bienmoi.Khac = "";
                                    }
                                    if (item.Contains("Dây rốn"))
                                    {
                                        string[] ar = item.Split(':');

                                        _bienmoi.DayRon += ar[1] + "\r\n";
                                    }
                                    if (item.Contains("Tim thai"))
                                    {
                                        _bienmoi.NhipTimThai = "•  " + item;
                                    }
                                    if (item.Contains("Thoát vị thành bụng") || item.Contains("thoát vị thành ngực, thành bụng"))
                                    {
                                        _bienmoi.ThoatViThanhBung = "•  " + item;
                                    }
                                }
                                _listBienKQ.Add(_bienmoi);
                            }
                            if (arr[1].Trim() != "")
                            {
                                #region chuoi thai thứ 2
                                BienKQ _bienmoi = new BienKQ();
                                string[] arrkq = arr[1].Split(';');
                                int stt = 1;
                                foreach (var item in arrkq)
                                {
                                    if (item.Contains("Tim thai"))
                                    {
                                        _bienmoi.NhipTimThai = "•  " + item;
                                    }
                                    if (item.Contains("Cân nặng"))
                                    {
                                        _bienmoi.CanNang = "•  " + item;
                                    }
                                    if (item.Contains("Cấu trúc phổi"))
                                    {
                                        _bienmoi.CauTrucPhoi = "•  " + item;
                                    }
                                    if (item.Contains("Cấu trúc tim thai"))
                                    {
                                        _bienmoi.CauTrucTimThai = "•  " + item;
                                    }
                                    if (item.Contains("Thoát vị thành bụng") || item.Contains("thoát vị thành ngực, thành bụng"))
                                    {
                                        _bienmoi.ThoatViThanhBung = "•  " + item;
                                    }
                                    if (item.Contains("CRL"))
                                    {
                                        _bienmoi.ChieuDaiDauMong_CRL = "•  " + item;
                                    }
                                    if (item.Contains("FL"))
                                    {
                                        _bienmoi.ChieuDaiXuongDui_FL = "•  " + item;
                                    }
                                    if (item.Contains("Chiều dài xương mũi"))
                                    {
                                        _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                    }
                                    if (item.Contains("Chu vi bụng"))
                                    {
                                        _bienmoi.ChuViBung_AC = "•  " + item;
                                    }
                                    if (item.Contains("Chu vi đầu"))
                                    {
                                        _bienmoi.ChuViDau_HC = "•  " + item;
                                    }
                                    if (item.Contains("Cột sống"))
                                    {
                                        _bienmoi.CotSong = "•  " + item;
                                    }
                                    if (item.Contains("Kích thước ngang tiểu não"))
                                    {
                                        _bienmoi.KichThuocNgangTieuNao = "•  " + item;
                                    }
                                    if (item.Contains("Hố sau"))
                                    {
                                        _bienmoi.HoSau = "•  " + item;
                                    }
                                    if (item.Contains("Não thất bên"))
                                    {
                                        _bienmoi.NaoThatBen = "•  " + item;
                                    }
                                    if (item.Contains("Chiều dài xương mũi"))
                                    {
                                        _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                    }
                                    if (item.Contains("Khoảng cách hai hốc mắt"))
                                    {
                                        _bienmoi.KhoangCach2HocMat = "•  " + item;
                                    }
                                    if (item.Contains("Khoảng sáng sau gáy"))
                                    {
                                        _bienmoi.KhoangSangSauGay = "•  " + item;
                                    }
                                    if (item.Contains("Dạ dày"))
                                    {
                                        _bienmoi.DaDay = "•  " + item;
                                    }
                                    if (item.Contains("Quai ruột"))
                                    {
                                        _bienmoi.QuaiRuot = "•  " + item;
                                    }
                                    if (item.Contains("Thận"))
                                    {
                                        _bienmoi.Than = "•  " + item;
                                    }
                                    if (item.Contains("Hình ảnh vòm sọ"))
                                    {
                                        _bienmoi.HinhAnhVomSo = "•  " + item;
                                    }
                                    if (item.Contains("OFD"))
                                    {
                                        _bienmoi.DKChamTranDau_OFD = "•  " + item;
                                    }
                                    if (item.Contains("BPD"))
                                    {
                                        _bienmoi.DKLuongDinh_BPD = "•  " + item;
                                    }
                                    if (item.Contains("APTD"))
                                    {
                                        _bienmoi.DKTruocSauBung_APTD = "•  " + item;
                                    }
                                    if (item.Contains("TTD"))
                                    {
                                        _bienmoi.DKNgangBung_TTD = "•  " + item;
                                    }
                                }
                                _listBienKQ.Add(_bienmoi);
                                #endregion
                            }
                        }
                        #endregion
                        else
                        {
                            #region Sieu am 01 thai
                            rgSoThai.SelectedIndex = 0;
                            rgSoThai.Enabled = false;
                            xtraTabControl1.SelectedTabPage = xpage1thai;
                            xpage2thai.PageEnabled = false;
                            string[] arrkq = kqcls.First().KetQua.ToString().Split(';');
                            int stt = 1;
                            kq.Clear();
                            BienKQ _bienmoi = new BienKQ();
                            int t = 0;
                            foreach (var item in arrkq)
                            {
                                if (item.Contains("Tim thai"))
                                {
                                    _bienmoi.NhipTimThai = "* " + item;
                                }
                                if (item.Contains("Thoát vị thành bụng") || item.Contains("thoát vị thành ngực, thành bụng"))
                                {
                                    _bienmoi.ThoatViThanhBung = "•  " + item;
                                }
                                if (item.Contains("Tử cung"))
                                {
                                    _bienmoi.TuCung = "•  " + item;
                                }
                                if (item.Contains("Ngôi thai"))
                                {
                                    _bienmoi.NgoiThai = "•  " + item;
                                }
                                if (item.Contains("Cử động thai"))
                                {
                                    _bienmoi.CuDongThai = "* " + item;
                                }
                                if (item.Contains("Cân nặng"))
                                {
                                    _bienmoi.CanNang = item;
                                }
                                if (item.Contains("Cấu trúc phổi"))
                                {
                                    _bienmoi.CauTrucPhoi = "•  " + item;
                                }
                                if (item.Contains("Cấu trúc tim thai"))
                                {
                                    _bienmoi.CauTrucTimThai = "•  " + item;
                                }
                                if (item.Contains("CRL"))
                                {
                                    _bienmoi.ChieuDaiDauMong_CRL = "•  " + item;
                                }
                                if (item.Contains("FL"))
                                {
                                    _bienmoi.ChieuDaiXuongDui_FL = "•  " + item;
                                }
                                if (item.Contains("Chiều dài xương mũi"))
                                {
                                    _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                }
                                if (item.Contains("Chu vi bụng"))
                                {
                                    _bienmoi.ChuViBung_AC = "•  " + item;
                                }
                                if (item.Contains("Chu vi đầu"))
                                {
                                    _bienmoi.ChuViDau_HC = "•  " + item;
                                }
                                if (item.Contains("Cột sống"))
                                {
                                    _bienmoi.CotSong = "•  " + item;
                                }
                                if (item.Contains("Kích thước ngang tiểu não"))
                                {
                                    _bienmoi.KichThuocNgangTieuNao = "•  " + item;
                                }
                                if (item.Contains("Hố sau"))
                                {
                                    _bienmoi.HoSau = "•  " + item;
                                }
                                if (item.Contains("Não thất bên"))
                                {
                                    _bienmoi.NaoThatBen = "•  " + item;
                                }
                                if (item.Contains("Chiều dài xương mũi"))
                                {
                                    _bienmoi.ChieuDaiXuongMui = "•  " + item;
                                }
                                if (item.Contains("Khoảng cách hai hốc mắt"))
                                {
                                    _bienmoi.KhoangCach2HocMat = "•  " + item;
                                }
                                if (item.Contains("Khoảng sáng sau gáy"))
                                {
                                    _bienmoi.KhoangSangSauGay = "•  " + item;
                                }
                                if (item.Contains("Dạ dày"))
                                {
                                    _bienmoi.DaDay = "•  " + item;
                                }
                                if (item.Contains("Quai ruột"))
                                {
                                    _bienmoi.QuaiRuot = "•  " + item;
                                }
                                if (item.Contains("Thận"))
                                {
                                    _bienmoi.Than = "•  " + item;
                                }
                                if (item.Contains("Hình ảnh vòm sọ"))
                                {
                                    _bienmoi.HinhAnhVomSo = "•  " + item;
                                }
                                if (item.Contains("Tuổi thai ước tính"))
                                {
                                    _bienmoi.TuoiThai = item;
                                }
                                if (item.Contains("Dự kiến sinh"))
                                {
                                    _bienmoi.DuKienSinh = item;
                                }
                                if (item.Contains("OFD"))
                                {
                                    _bienmoi.DKChamTranDau_OFD = "•  " + item;
                                }
                                if (item.Contains("BPD"))
                                {
                                    _bienmoi.DKLuongDinh_BPD = "•  " + item;
                                }
                                if (item.Contains("APTD"))
                                {
                                    _bienmoi.DKTruocSauBung_APTD = "•  " + item;
                                }
                                if (item.Contains("TTD"))
                                {
                                    _bienmoi.DKNgangBung_TTD = "•  " + item;
                                }
                                if (item.Contains("Tuổi thai ước tính"))
                                {
                                    _bienmoi.TuoiThai = item;
                                }

                                if (item.Contains("Rau thai"))
                                {
                                    string[] ar = item.Split(':');
                                    if (ar.Count() > 2)
                                    {
                                        t++;
                                        string s = ar[1] + ":" + ar[2];
                                        _bienmoi.RauThai += s + "\r\n";
                                        if (t == 1)
                                            _bienmoi.RauThai2 = ar[1] + ":" + ar[2];
                                        if (t == 2)
                                            _bienmoi.RauThai3 = ar[1] + ": " + ar[2];
                                    }
                                    else
                                    {
                                        string s = ar[1];
                                        _bienmoi.RauThai += s + "\r\n";
                                    }

                                }
                                if (item.Contains("Nước ối"))
                                {
                                    string[] ar = item.Split(':');

                                    _bienmoi.NuocOi += ar[1] + "\r\n";
                                }
                                if (item.Contains("Khác"))
                                {
                                    string[] ar = item.Split(':');
                                    if (ar[1] != null && ar[1].Length > 0)
                                        _bienmoi.Khac = ar[1];
                                    else _bienmoi.Khac = "";
                                }
                                if (item.Contains("Dây rốn"))
                                {
                                    _bienmoi.DayRon += item + Environment.NewLine;
                                }
                            }
                            _listBienKQ.Add(_bienmoi);
                            if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng đầu" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng đầu")
                            {
                                giaodien = 1;
                            }
                            if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng giữa" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng giữa")
                            {
                                giaodien = 2;
                            }
                            if (txttendv.Text == "Siêu âm thai nhi 4D 3 tháng cuối" || txttendv.Text == "Siêu âm thai nhi trong 3 tháng cuối")
                            {
                                giaodien = 3;
                            }
                        }
                            #endregion
                    }
                    else
                    {
                        #region Bệnh viện khác 01071 || 01049


                        #endregion
                    }
                }
                if (DungChung.Bien.MaBV == "24297")
                    CLS.InPhieu.PhieuSieuAmMau4D_24297(_idcd, rgSoThai.SelectedIndex, giaodien, _listBienKQ);
                else
                    CLS.InPhieu.PhieuSieuAmMau4D_01071(_idcd, rgSoThai.SelectedIndex, giaodien, _listBienKQ);
            }
            else
                CLS.InPhieu.PhieuSieuAmMau4D(_idcd, rgSoThai.SelectedIndex);
        }

        private void rgSoThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTLuu == 0 || TTLuu == 2)
                LoadKQMau();
        }

        private void radThangThaiNhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTLuu == 0 || TTLuu == 2)
                LoadKQMau();
        }

        private void frm_KetQuaSieuAmThai4D_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "01049")
            //{
            //    int idcls = 0; string _ThamChieuKetQua = "";
            //    idcls = _idcls;
            //    _ThamChieuKetQua = LuuKetQua();

            //    Frm_CDHA_Moi frmCdha = new Frm_CDHA_Moi(_idcls);
                
            //}
        }
    }
}