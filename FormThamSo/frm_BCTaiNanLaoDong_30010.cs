using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Office.Utils;

namespace QLBV.FormThamSo
{
    public partial class frm_BCTaiNanLaoDong_30010 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTaiNanLaoDong_30010()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void frm_BCTaiNanLaoDong_30010_Load(object sender, EventArgs e)
        {
            luptungay.DateTime = System.DateTime.Now;
            lupdenngay.DateTime = System.DateTime.Now;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class _ListBNTaiNan
        {
            public int MaBN { set; get; }
            public string TenBN { set; get; }
            public int Tuoi { set; get; }
            public string Nam  { set; get; }
            public string Nu { set; get; }
            public string TChung { set; get; }
            public string MaNN { set; get; }
            public int SoNgaydt { set; get; }
            public string NgayN { set; get; }
            public string Khoi { set; get; }
            public string DoGiam { set; get; }
            public string KhongDoi { set; get; }
            public string NangHon { set; get; }
            public string TuVong { set; get; }
            public string MaCSKCB { set; get; }
        }
        public List<_ListBNTaiNan> _list = new List<_ListBNTaiNan>();
        private void btntaobc_Click(object sender, EventArgs e)
        {
            DateTime TuNgay = System.DateTime.Now;
            DateTime DenNgay = System.DateTime.Now;
            _list.Clear();
            TuNgay = DungChung.Ham.NgayTu(luptungay.DateTime);
            DenNgay = DungChung.Ham.NgayDen(lupdenngay.DateTime);
            var _DSBN = (from bn in _data.BenhNhans.Where(p=>p.NNhap>=TuNgay&&p.NNhap<=DenNgay).Where(p => p.CapCuu == 1).Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.Contains("Tai nạn lao động"))
                         join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                         join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         select new
                         {
                             bn.MaBNhan,
                             bn.TenBNhan,
                             bn.Tuoi,
                             bn.GTinh,
                             bn.TChung,
                             ttbx.MaNN,
                             rv.SoNgaydt,
                             bn.NNhap,
                             rv.KetQua,
                             bn.MaKCB
                         }).OrderBy(p => p.NNhap).ToList();
            var _KetQua = (from a in _DSBN
                           select new _ListBNTaiNan
                           {
                               MaBN = a.MaBNhan,
                               TenBN = a.TenBNhan,
                               Tuoi = a.Tuoi ?? 0,
                               Nam = a.GTinh == 1 ? "X" : "",
                               Nu = a.GTinh == 1 ? "" : "X",
                               TChung = a.TChung,
                               MaNN = a.MaNN,
                               SoNgaydt = a.SoNgaydt ?? 0,
                               NgayN = a.NNhap.Value.Day + "/" + a.NNhap.Value.Month,
                               Khoi = a.KetQua == null ? "" :( a.KetQua.Contains("Khỏi") ? "X" : ""),
                               DoGiam =a.KetQua == null ? "" :(  a.KetQua.Contains("Đỡ|Giảm") ? "X" : ""),
                               KhongDoi = a.KetQua == null ? "" :( a.KetQua.Contains("Không T.đổi") ? "X" : ""),
                               NangHon = a.KetQua == null ? "" :( a.KetQua.Contains("Nặng hơn") ? "X" : ""),
                               TuVong = a.KetQua == null ? "" :(  a.KetQua.Contains("Tử vong") ? "X" : ""),
                               MaCSKCB = a.MaKCB
                           }).ToList();
            _list.AddRange(_KetQua);
            frmIn frm = new frmIn();
           
            BaoCao.BCTaiNanLaoDong_30010 rep = new BaoCao.BCTaiNanLaoDong_30010(_list);
            //rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.CQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.NGAYTHANGKY.Value = DungChung.Bien.DiaDanh + ", Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
            rep.TIEUDE1.Value = "BÁO CÁO CÁC TRƯỜNG HỢP TAI NẠN LAO ĐỘNG";
            rep.TIEUDE2.Value = "ĐƯỢC KHÁM, ĐIỀU TRỊ TẠI BVĐK KIM THÀNH";
            rep.NGAYTHANG1.Value = "Từ ngày: " + TuNgay.ToShortDateString() + " đến ngày: " + DenNgay.ToShortDateString();
            rep.NGAYTHANG2.Value = "BVĐK huyện Kim Thành báo cáo các bệnh nhân tai nạn lao động khám và điều trị" + " từ ngày: " + TuNgay.ToShortDateString() + " đến ngày: " + DenNgay.ToShortDateString();
            rep.DataSource = _list;
            rep.Bindingdata();
            
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}