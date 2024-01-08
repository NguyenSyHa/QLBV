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
    public partial class frm_BC_BN_HoiChan : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<HoiChan> _listHC = new List<HoiChan>();
        List<KhoaKe> _lKhoake = new List<KhoaKe>();
        public frm_BC_BN_HoiChan()
        {
            InitializeComponent();
        }

        public class HoiChan
        {
            public string hoTen { get; set; }
            public string tuoiNam { get; set; }
            public string tuoiNu { get; set; }
            public string diaChi { get; set; }
            public string sThe { get; set; }
            public string chanDoan { get; set; }
            public string khoaHC { get; set; }
            public string thuocHC { get; set; }
            public string bsHC { get; set; }
            public string ngayHC { get; set; }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            _listHC.Clear();
            BaoCao.rep_BC_BN_HoiChan rep = new BaoCao.rep_BC_BN_HoiChan();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            if (tungay <= denngay)
            {
                if (DungChung.Ham.NgayTu(denngay) == tungay)
                    rep.TuNgayDenNgay.Text = "Trong ngày";
                else
                    rep.TuNgayDenNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                List<KhoaKe> dskp = new List<KhoaKe>();
                dskp = _lKhoake.Where(p => p.Chon == true && p.MaKP > 0).ToList();
                var qtheongay = (from bbhc in data.BBHCs.Where(p => p.NgayHC != null).Where(p => p.NgayHC <= denngay).Where(p => p.NgayHC >= tungay)
                                 join bn in data.BenhNhans on bbhc.MaBNhan equals bn.MaBNhan
                                 select new { bbhc, bn }).Distinct().ToList();

                var qkqtheongaykp = (from q1 in qtheongay
                                     join q2 in dskp on q1.bbhc.MaKP equals q2.MaKP
                                     select new
                                     {
                                         q1,
                                         q2
                                     }).Distinct().ToList();

                var lbn = qkqtheongaykp.Select(p => p.q1.bbhc.MaBNhan).Distinct().ToList();

                var qdt = (from a in data.DThuocs.Where(p => p.NgayKe != null).Where(p => lbn.Contains(p.MaBNhan ?? 0))
                           join dtct in data.DThuoccts on a.IDDon equals dtct.IDDon
                           join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                           select new { a.MaBNhan, a.MaKP, a.NgayKe, dv.MaDV, dv.TenDV }).Distinct().ToList();
                
                var qTH = (from a in qkqtheongaykp
                           join b in qdt on new { a.q1.bbhc.MaBNhan, a.q2.MaKP, Ngay = a.q1.bbhc.NgayHC.Value.Date } equals new { b.MaBNhan, b.MaKP, Ngay = b.NgayKe.Value.Date } into kq
                           from kq1 in kq.DefaultIfEmpty()
                           join cb in data.CanBoes on a.q1.bbhc.MaCB equals cb.MaCB
                           group new{kq,kq1} by new {a.q1.bn.TenBNhan,
                               a.q1.bn.GTinh,
                               a.q1.bn.Tuoi,
                               a.q1.bn.DChi,
                               a.q1.bn.SThe,
                               a.q1.bbhc.ChanDoan,
                               a.q2.TenKP,
                               a.q2.MaKP,
                               a.q1.bbhc.MaCB,
                               ngayHC = a.q1.bbhc.NgayHC.Value.Date} into kq3 
                           select new
                           {
                               kq3.Key.TenBNhan,
                               kq3.Key.GTinh,
                               kq3.Key.Tuoi,
                               kq3.Key.DChi,
                               kq3.Key.SThe,
                               kq3.Key.ChanDoan,
                               kq3.Key.TenKP,
                               kq3.Key.MaKP,
                               kq3.Key.MaCB,
                               kq3.Key.ngayHC,
                               TenThuoc = String.Join(";", kq3.Where(p => p.kq1 != null).Select(p => p.kq1.TenDV))
                           }).Distinct().ToList();


                if (qTH.Count() > 0)
                {
                    foreach (var i in qTH)
                    {
                        HoiChan hc = new HoiChan();
                        hc.hoTen = i.TenBNhan;
                        hc.tuoiNam = Convert.ToString(i.GTinh == 1 ? i.Tuoi : null);
                        hc.tuoiNu = Convert.ToString(i.GTinh == 0 ? i.Tuoi : null);
                        hc.diaChi = i.DChi;
                        hc.sThe = i.SThe;
                        hc.chanDoan = i.ChanDoan;
                        hc.khoaHC = i.TenKP;
                        string[] arrTenThuoc = i.TenThuoc.Split(';');
                        string TenThuoc = "";
                        if (arrTenThuoc.Count() > 1)
                            for (int j = 0; j < arrTenThuoc.Count(); j++)
                            {
                                for (int z = j + 1; z < arrTenThuoc.Count(); z++)
                                {
                                    if (arrTenThuoc[j] == arrTenThuoc[z] && arrTenThuoc[z] != null)
                                    {
                                        arrTenThuoc[z] = null;
                                    }
                                }
                                if (arrTenThuoc[j] != null)
                                    TenThuoc += arrTenThuoc[j] + "; ";
                            }
                        else TenThuoc = i.TenThuoc;
                        hc.thuocHC = TenThuoc;
                        hc.bsHC = DungChung.Ham._getTenCB(data, i.MaCB);
                        hc.ngayHC = i.ngayHC.ToString("dd/MM/yyyy");
                        _listHC.Add(hc);
                    }
                }
                frmIn frm = new frmIn();
                rep.DataSource = _listHC;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Từ ngày không được phép lớn hơn đến ngày. Mời bạn chọn lại!");
            }
        }
        private class KhoaKe
        {
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int? MaKP
            {
                get { return maKP; }
                set { maKP = Convert.ToInt32(value); }
            }

            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }
        private void frm_BC_BN_HoiChan_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupngayden.DateTime = System.DateTime.Now;
            //var kp = _data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược")).ToList();
            var kphong = _data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám").ToList();
            if (kphong.Count > 0)
            {
                KhoaKe themmoi1 = new KhoaKe();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoake.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KhoaKe themmoi = new KhoaKe();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoake.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoake.ToList();
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoake.First().Chon == true)
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoake.ToList();
                    }
                }
            }
        }
    }
}