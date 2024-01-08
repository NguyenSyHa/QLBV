using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_SoDe_30004 : DevExpress.XtraEditors.XtraForm
    {
        int value;
        public frm_BC_SoDe_30004(int STTSo)
        {
            InitializeComponent();
            value = STTSo;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void btnInBC_Click(object sender, EventArgs e)
        {
            switch (value)
            {
                case 0:
                    DungChung.Ham.CallProcessWaitingForm(InSo);
                    break;
                case 1:
                    DungChung.Ham.CallProcessWaitingForm(InSo1);
                    break;
            }

        }

        private void rep_BC_SoDe_30004_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
        }
        private void InSo()
        {
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            var qbn = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                       join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into t
                       from t1 in t.DefaultIfEmpty()
                       join n in data.DmNNs on t1.MaNN equals n.MaNN into k
                       from k1 in k.DefaultIfEmpty()
                       join tt in data.TTTHs on bn.MaBNhan equals tt.MaBNhan
                       join cb in data.CanBoes on tt.MaCB equals cb.MaCB into q
                       from q1 in q.DefaultIfEmpty()
                       select new
                       {
                           bn.TenBNhan,
                           bn.Tuoi,
                           bn.DChi,
                           TenNN = k1 == null ? "" : k1.TenNN,
                           tt.SoLD,
                           tt.NgaySinh,
                           tt.MaBV,
                           tt.Ploai,
                           tt.DBien,
                           tt.TaiBien,
                           //VoTuCung = tt.TaiBien == 1 ? "X" : "",
                           //SanGiat = tt.TaiBien == 2 ? "X" : "",
                           //NhiemTrung = tt.TaiBien == 3 ? "X" : "",
                           //UonVan = tt.TaiBien == 4 ? "X" : "",
                           GioiTinh = tt.GioiTinh.Value,
                           CanNangCon = tt.CanNangCon.Value,
                           CCaoCon = tt.CCaoCon.Value,
                           tt.Thaichet,
                           //ChetKhiDe = tt.Thaichet == 1 ? "X" : "",
                           //ChetDuoi7Ngay = tt.Thaichet == 2 ? "X" : "",
                           //ChetSau28Ngay = tt.Thaichet == 3 ? "X" : "",
                           CBTH = q1 == null ? "" : (q1.ChucVu + " " + q1.TenCB)
                       }).ToList();
            var query = (from bn in qbn
                         select new
                         {
                             bn.TenBNhan,
                             bn.Tuoi,
                             bn.DChi,
                             TenNN = bn.TenNN,
                             bn.SoLD,
                             bn.NgaySinh,
                             NoiDe = (bn.MaBV != null && bn.MaBV != "") ? "X" : "",
                             TSNaoThai = bn.Ploai == 1 ? "Nạo thai" : (bn.Ploai == 2 ? "Phá thai" : ""),
                             DeThuong = bn.DBien == 0 ? "X" : "",
                             DeKho = bn.DBien == 1 ? "X" : "",
                             MoDe = bn.DBien == 2 ? "X" : "",
                             Chet = bn.DBien == 3 ? "X" : "",
                             BangHuyet = bn.TaiBien == 0 ? "X" : "",
                             VoTuCung = bn.TaiBien == 1 ? "X" : "",
                             SanGiat = bn.TaiBien == 2 ? "X" : "",
                             NhiemTrung = bn.TaiBien == 3 ? "X" : "",
                             UonVan = bn.TaiBien == 4 ? "X" : "",
                             Trai = (bn.GioiTinh == 0) ? (bn.CanNangCon + "/" + bn.CCaoCon) : null,
                             Gai = (bn.GioiTinh == 1) ? (bn.CanNangCon + "/" + bn.CCaoCon) : null,
                             ChetLuu = bn.Thaichet == 0 ? "X" : "",
                             ChetKhiDe = bn.Thaichet == 1 ? "X" : "",
                             ChetDuoi7Ngay = bn.Thaichet == 2 ? "X" : "",
                             ChetSau28Ngay = bn.Thaichet == 3 ? "X" : "",
                             CBTH = bn.CBTH
                         }).ToList();

            BaoCao.rep_BC_SoDe_30004 rep = new BaoCao.rep_BC_SoDe_30004();
            frmIn frm = new frmIn();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = query.OrderBy(p => p.TenBNhan).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        private void InSo1()
        {
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            var sode2 = (from bn in data.BenhNhans
                         join th in data.TTTHs.Where(p => p.NgaySinh >= tungay && p.NgaySinh <= denngay) on bn.MaBNhan equals th.MaBNhan
                         join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                         join tdts in data.TheoDoiThaiSans on bn.MaBNhan equals tdts.MaBNhan into tdtss
                         join cb in data.CanBoes on th.MaCB equals cb.MaCB
                         join bv in data.BenhViens on th.MaBV equals bv.MaBV
                         from kq in tdtss.DefaultIfEmpty() 
                         select new
                         {
                             bn.MaBNhan,
                             bn.TenBNhan,
                             bn.Tuoi,
                             bn.GTinh,
                             bn.DChi,
                             bn.DTuong,
                             bn.SThe,
                             ttbx.MaNN,
                             ttbx.MaDT,
                             th.SoLC,
                             th.GioiTinh,
                             th.CanNangCon,
                             th.TaiBien,
                             th.DBien,
                             th.NgaySinh,
                             th.CCaoCon,
                             th.MaBV,
                             th.MaCB,
                             th.Thaichet,
                             th.Ploai,
                             SoChungSinh = kq.SoChungSinh == null ? "" : "X",
                             QuyenSo = kq.QuyenSo == null ? "" : kq.QuyenSo,
                             GhiChu = kq.GhiChu == null ? "" : kq.GhiChu,
                             cb.TenCB,
                             bv.TenBV
                         }).ToList();


            var rs = (from ts in sode2
                      join nn in data.DmNNs on ts.MaNN equals nn.MaNN into kq
                      join _dantoc in data.DanTocs on ts.MaDT equals _dantoc.MaDT into tq
                      from tq1 in tq.DefaultIfEmpty()
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          ts.MaBNhan,
                          ts.TenBNhan,
                          ts.Tuoi,
                          ts.GTinh,
                          ts.DChi,
                          ts.DTuong,
                          ts.SThe,
                          ts.MaNN,
                          TenNN = ts.MaNN != null ? kq1.TenNN : "",
                          ts.MaDT,
                          TenDT = ts.MaDT != null ? tq1.TenDT : "",
                          ts.SoLC,
                          ts.GioiTinh,
                          ts.CanNangCon,
                          ts.TaiBien,
                          ts.DBien,
                          Trai = ts.GioiTinh == 0 ? ts.CanNangCon + "" : null,
                          Gai = ts.GioiTinh == 1 ? ts.CanNangCon + "" : null,
                          Naotsai = ts.Ploai == 1 ? "Nạo tsai" : (ts.Ploai == 2 ? "Phá tsai" : null),
                          taibienSK = ts.TaiBien == 1 ? "Vỡ tử cung" : (ts.TaiBien == 2 ? "Sản giật" : (ts.TaiBien == 3 ? "Nhiễm trùng" : (ts.TaiBien == 4 ? "Uốn ván" : null))),
                          Cachthucde = ts.DBien == 0 ? "Đẻ thường" : (ts.DBien == 1 ? "Đẻ khó" : (ts.DBien == 2 ? "Mổ đẻ" : (ts.DBien == 3 ? "Chết" : null))),
                          NgaySinh = ts.NgaySinh == null ? " " : Convert.ToDateTime(ts.NgaySinh).ToString("dd/MM/yyyy"),
                          ts.MaBV,
                          ts.MaCB,
                          ts.Ploai,
                          ts.SoChungSinh,
                          ts.QuyenSo,
                          ts.GhiChu,
                          TenCB = DungChung.Ham._getTenCB(data, ts.MaCB),
                          ts.TenBV
                      }).OrderBy(p=>p.NgaySinh).ToList();
            if (rs.Count() > 0)
            {
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_SoDe_30007, rs, new Dictionary<string, object>(), false);
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
    }
}