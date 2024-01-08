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
    public partial class frm_SoTheoDoiThuChi30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoTheoDoiThuChi30007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        private void frm_SoTheoDoiThuChi30007_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
            var _lcb = data.CanBoes.ToList();
            var _lcbthu = data.TamUngs.Where(p => p.PhanLoai == 1 && p.MaCB != null).Select(p => p.MaCB).Distinct().ToList();
            var _lcbc = (from a in _lcbthu
                         join b in _lcb on a equals b.MaCB
                         select b).OrderBy(p => p.TenCB).ToList();
            _lcbc.Add(new CanBo { MaCB = "-1", TenCB = "Tất cả" });
            lupCanBo.Properties.DataSource = _lcbc.ToList();
            lupCanBo.EditValue = "-1";
            rgDTuong.SelectedIndex = 2;
            radBN.SelectedIndex = 3;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            int dt = rgDTuong.SelectedIndex;
            int ntru = radBN.SelectedIndex;
            string cb = "";
            if(lupCanBo.EditValue!=null)
            {
                cb = lupCanBo.EditValue.ToString();
            }
            DateTime _tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);

            var q1 = (from tu in data.TamUngs.Where(p => cb == "-1" ? true : p.MaCB == cb).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.NgayThu >= _tungay && p.NgayThu <= _denngay)
                      join bn in data.BenhNhans.Where(p => dt == 2 ? true : (dt == 0 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ")).Where(p => ntru == 3 ? true : (ntru == 0 ? (p.NoiTru == 0 && p.DTNT == false) : (ntru == 1 ? (p.NoiTru == 0 && p.DTNT == true) : p.NoiTru == 1))) on tu.MaBNhan equals bn.MaBNhan
                      select new { bn.MaBNhan, tu.SoTien, bn.TenBNhan, bn.DChi, bn.DTuong, tu.NgayThu, tu.PhanLoai }).ToList();

            List<int> _lmabn = q1.Select(p => p.MaBNhan).Distinct().ToList();

            var q2 = (from tu in data.TamUngs.Where(p => _lmabn.Contains(p.MaBNhan ?? 0)).Where(p => p.PhanLoai == 0)
                      group new { tu } by new { tu.MaBNhan } into kq
                      select new
                      {
                          kq.Key.MaBNhan,
                          TongUng = kq.Sum(p => p.tu.SoTien)
                      }).ToList();
            List<BNDTNoiTru> ketqua = new List<BNDTNoiTru>();
            foreach (var item in q1)
            {
                BNDTNoiTru moi = new BNDTNoiTru();
                moi.NgayVao = item.NgayThu;
                moi.DT = item.DTuong;
                moi.MaBNhan = item.MaBNhan;
                moi.HoTen = item.TenBNhan;
                moi.DiaChi = item.DChi;
                double tongung = 0, tientt = 0, tt = 0;
                var tienung = q2.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                if (tienung.Count() > 0)
                {
                    tongung = tienung.Sum(p => p.TongUng ?? 0);
                    moi.TongTU = tongung;
                }
               
                if (item.PhanLoai == 2)
                {
                    moi.ThanhToan = moi.TongTU - item.SoTien ?? 0;
                    moi.ChiTra = item.SoTien ?? 0;
                }
                else
                {
                    moi.ThanhToan = item.SoTien ?? 0;
                    tt = item.SoTien ?? 0;
                    tientt = tt - tongung;
                    if (tientt < 0)
                    {
                        moi.ChiTra = tientt * -1;
                    }
                    else if (tientt > 0)
                        moi.ThuThieu = tientt;
                }
                ketqua.Add(moi);
            }

            frmIn frm = new frmIn();
            BaoCao.Rep_SoTheoDoiThuChi_30007 rep = new BaoCao.Rep_SoTheoDoiThuChi_30007();
            rep.celThang.Text = "Từ ngày: " + _tungay.ToShortDateString() + " đến ngày: " + _denngay.ToShortDateString();
            rep.DataSource = ketqua.Where(p => p.TongTU > 0 || p.ThanhToan > 0).OrderBy(p => p.NgayVao).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        private class BNDTNoiTru
        {
            public DateTime? NgayVao { get; set; }
            public string DTuong { get; set; }
            public string DT { get; set; }
            public int MaBNhan { get; set; }
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public double? TongTU { get; set; }
            public double ThanhToan { get; set; }
            public double? ChiTra { get; set; }
            public double? ThuThieu { get; set; }
        }
    }
}