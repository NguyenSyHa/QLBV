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
    public partial class frm_BkeVPBNhanRaVien_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BkeVPBNhanRaVien_30005()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class BKTTVPRV
        {
            public int STT { get; set; }
            public int? MaBNhan { get; set; }
            public string TenBNhan { get; set; }
            public string TenKP { get; set; }
            public string QuyenHD { get; set; }
            public string SoHDTU { get; set; }
            public double SoTienTU { get; set; }
            public string SoHDVP { get; set; }
            public double SoTienVP { get; set; }
            public double Thu { get; set; }
            public double chi { get; set; }
        }
        private void frm_TamUngVP_30005_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30002")
                chkChiTamUng.Checked = true;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");
            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            _lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaphong.Properties.DataSource = _lkp;
            lupKhoaphong.EditValue = lupKhoaphong.Properties.GetKeyValueByDisplayText("Tất cả");
            radio_noitru.SelectedIndex = 3;
            DuyetVP.SelectedIndex = 2;
            if (DungChung.Bien.MaBV == "30005")
                DuyetVP.Visible = true;
            else
                DuyetVP.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            //Nội ngoại trú
            int noitru = -1;
            noitru = radio_noitru.SelectedIndex;

            //đối tượng bệnh nhân
            int dtbn = -1;
            if (lupDoituong.EditValue != null)
                dtbn = Convert.ToInt32(lupDoituong.EditValue);

            //Thời gian
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

            //Chi phí trong ngoài danh mục
            //khoa phòng thanh toán
            int makp = 0;
            if (lupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(lupKhoaphong.EditValue);

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qbn = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => noitru == 3 || (noitru == 1 ? p.NoiTru == 1 : (p.NoiTru == 0 && (noitru == 0 ? p.DTNT == false : p.DTNT == true))))
                       //join dt in data.DTBNs on bn.IDDTBN equals dt.IDDTBN
                       //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan  into kq from kq1 in kq.DefaultIfEmpty()

                       select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, }).ToList();

            var qtamung = (from tu in data.TamUngs
                           join vp in data.VienPhis on tu.MaBNhan equals vp.MaBNhan into kqVP
                           from kq2 in kqVP.DefaultIfEmpty()
                           where (rdLoaiNgay.SelectedIndex == 1 ? (kq2 != null && kq2.NgayDuyet >= tungay && kq2.NgayDuyet <= denngay)
                           : (tu.NgayThu >= tungay && tu.NgayThu <= denngay))
                           orderby tu.NgayThu descending
                           select new
                           { 
                               tu.MaBNhan,
                               tu.MaKP,
                               tu.SoHD,
                               tu.SoTien,
                               tu.TienChenh,
                               tu.PhanLoai,
                               tu.NgayThu,
                               kq2.NgayDuyet, 
                               tu.QuyenHD,
                           }).Where(p => DuyetVP.SelectedIndex == 0 ? p.NgayDuyet == null : (DuyetVP.SelectedIndex == 1 ? p.NgayDuyet != null : true)).ToList();
            if(radMaBC.SelectedIndex == 1)
            {
                qtamung = (from bn in data.BenhNhans
                           join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                           join vp in data.VienPhis on tu.MaBNhan equals vp.MaBNhan into kqVP
                           from kq2 in kqVP.DefaultIfEmpty()
                           where (rdLoaiNgay.SelectedIndex == 1 ? (kq2 != null && kq2.NgayDuyet >= tungay && kq2.NgayDuyet <= denngay)
                           : (tu.NgayThu >= tungay && tu.NgayThu <= denngay))
                           orderby tu.NgayThu descending
                           select new
                           {
                               tu.MaBNhan,
                               bn.MaKP,
                               tu.SoHD,
                               tu.SoTien,
                               tu.TienChenh,
                               tu.PhanLoai,
                               tu.NgayThu,
                               kq2.NgayDuyet,
                               tu.QuyenHD,
                           }).Where(p => DuyetVP.SelectedIndex == 0 ? p.NgayDuyet == null : (DuyetVP.SelectedIndex == 1 ? p.NgayDuyet != null : true)).ToList();
            }
            var qkp = data.KPhongs.Where(p => makp == 0 || p.MaKP == makp).ToList();
            var q1 = (from tu in qtamung
                      join bn in qbn on tu.MaBNhan equals bn.MaBNhan
                      join kp in qkp on tu.MaKP equals kp.MaKP
                      orderby tu.NgayThu descending
                      select new
                      {

                          Ngayra = tu.NgayThu.Value.Date,
                          tu.PhanLoai,
                          kp.MaKP,
                          tu.SoHD,
                          tu.MaBNhan,
                          tu.NgayThu,
                          bn.TenBNhan,
                          bn.Tuoi,
                          bn.DChi,
                          bn.NoiTru,
                          kp.TenKP,
                          tu.QuyenHD,
                          TamUng = tu.PhanLoai == 0 ? tu.SoTien : 0,
                          TraLai = (tu.PhanLoai == 2 ? tu.TienChenh : 0),
                          NopThem = (tu.PhanLoai == 1) ? tu.TienChenh : 0,
                          ThuTT = (tu.PhanLoai == 3) ? tu.TienChenh : 0,
                          ChiTamThu = (tu.PhanLoai == 4) ? tu.SoTien : 0,
                          SoTien = (tu.PhanLoai == 1 || tu.PhanLoai == 2) ? tu.SoTien : 0,
                      }).ToList();

            var teet = q1.Where(p => p.NopThem > 0).ToList();
            var teet2 = q1.Where(p => p.PhanLoai == 4).ToList();
            if (radMaBC.SelectedIndex == 0)
            {
                List<BC> _list = new List<BC>();
                _list.Clear();
                foreach (var a in q1)
                {
                    var q = q1.Where(p => p.MaBNhan == a.MaBNhan).OrderByDescending(p => p.NgayThu).ToList();
                    BC moi = new BC();
                    moi.TenBNhan = a.TenBNhan;
                    moi.Tuoi = a.Tuoi ?? 0;
                    moi.DChi = a.DChi;
                    moi.MaBNhan = a.MaBNhan ?? 0;
                    if (radKieuHienThi.SelectedIndex == 0)
                        moi.Ngayra = a.Ngayra;
                    else
                        moi.Ngayra = DateTime.Now.Date;

                    moi.TenKP = a.TenKP;
                    moi.TamUng = a.TamUng ?? 0;
                    moi.TraLai = a.TraLai;
                    moi.NopThem = a.NopThem;
                    moi.TienBN = a.SoTien ?? 0;

                    _list.Add(moi);
                }


                var q2 = (from bn in _list
                          group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.Ngayra } into kq
                          select new
                          {
                              kq.Key.TenBNhan,
                              kq.Key.Tuoi,
                              kq.Key.DChi,
                              kq.Key.MaBNhan,
                              kq.Key.Ngayra,
                              TenKP = String.Join(", ", kq.Select(p => p.TenKP).Distinct()),
                              TamUng = kq.Sum(p => p.TamUng),
                              TraLai = chkChiTamUng.Checked ? (kq.Sum(p => p.TamUng) - kq.Sum(p => p.TienBN)) : kq.Sum(p => p.TraLai),
                              NopThem = chkChiTamUng.Checked ? (kq.Sum(p => p.TienBN) - kq.Sum(p => p.TamUng)) : kq.Sum(p => p.NopThem),
                              TienBN = kq.Sum(p => p.TienBN)
                          }).OrderBy(p => p.Ngayra).Where(p => p.TamUng != 0 || p.TraLai != 0 || p.NopThem != 0 || p.TienBN != 0).ToList().Select(x => new
                          {
                              x.TenBNhan,
                              x.Tuoi,
                              x.DChi,
                              x.MaBNhan,
                              x.Ngayra,
                              x.TenKP,
                              x.TamUng,
                              TraLai = x.TraLai < 0 ? 0 : x.TraLai,
                              NopThem = x.NopThem < 0 ? 0 : x.NopThem,
                              x.TienBN
                          }).OrderBy(p => p.Ngayra).ToList();//.Where(p=>p.Tong != 0).ToList();
                if (q2.Where(p => p.TamUng > 0).Count() > 0)
                {
                    BaoCao.rep_BkeVPBNhanRaVien_30005 rep = new BaoCao.rep_BkeVPBNhanRaVien_30005(radKieuHienThi.SelectedIndex);
                    frmIn frm = new frmIn();

                    rep.DataSource = q2.Where(p => p.TamUng > 0).OrderBy(p => p.TenBNhan);
                    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                    if (String.IsNullOrEmpty(txtTieude.Text))
                    {
                        rep.CelTieuDe.Text = "BẢNG KÊ TẠM ỨNG VIỆN PHÍ";
                    }
                    else
                    { rep.CelTieuDe.Text = txtTieude.Text.ToUpper(); }

                    if (!String.IsNullOrEmpty(txtNgayThang.Text))
                        rep.celNgayThang.Text = txtNgayThang.Text;
                    rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
            else
            {
                List<BKTTVPRV> _list = new List<BKTTVPRV>();
                _list.Clear();
                foreach (var a in q1)
                {
                    BKTTVPRV moi = new BKTTVPRV();
                    moi.MaBNhan = a.MaBNhan;
                    moi.TenBNhan = a.TenBNhan;
                    moi.TenKP = a.TenKP;
                    moi.QuyenHD = (a.PhanLoai == 0 || a.PhanLoai == 4) ? a.QuyenHD : "";
                    moi.SoHDTU = (a.PhanLoai == 0 || a.PhanLoai == 4) ? a.SoHD : "";
                    moi.SoTienTU = a.TamUng != 0 ? a.TamUng ?? 0 : (a.ChiTamThu != 0 ? a.ChiTamThu??0 : 0);
                    moi.SoHDVP = (a.PhanLoai == 1 || a.PhanLoai == 3) ? a.SoHD : "";
                    moi.SoTienVP = a.NopThem != 0 ? a.NopThem : (a.ThuTT != 0 ? a.ThuTT : 0);
                    moi.Thu = (a.TamUng != 0 ? a.TamUng ?? 0 : (a.ThuTT != 0 ? a.ThuTT : 0)) + a.NopThem;
                    moi.chi = (a.ChiTamThu != 0 ? a.ChiTamThu??0 : 0);
                    _list.Add(moi);
 
                }
                if (_list.Count() > 0)
                {
                    BaoCao.rep_BkeTTVPRaVien rep = new BaoCao.rep_BkeTTVPRaVien();
                    frmIn frm = new frmIn();

                    rep.DataSource = _list.OrderBy(p => p.MaBNhan);
                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.ngaythang.Value = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                    if (!String.IsNullOrEmpty(txtNgayThang.Text))
                        rep.celNgayThang.Text = txtNgayThang.Text;
                    rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
        }
        public class BC
        {
            public string TenBNhan { set; get; }
            public int Tuoi { set; get; }
            public string DChi { set; get; }
            public int MaBNhan { set; get; }
            public DateTime Ngayra { set; get; }
            public string TenKP { set; get; }
            public double TamUng { set; get; }
            public double TraLai { set; get; }
            public double NopThem { set; get; }
            public double TienBN { set; get; }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdLoaiNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdLoaiNgay.SelectedIndex == 0)
            {
                DuyetVP.Enabled = true;
                DuyetVP.SelectedIndex = 2;
            }
            else
            {
                DuyetVP.Enabled = false;
                DuyetVP.SelectedIndex = 1;
            }
        }

        private void radMaBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radMaBC.SelectedIndex == 1)
            {
                radKieuHienThi.SelectedIndex = 0;
                radKieuHienThi.Enabled = false;
                chkChiTamUng.Visible = false;
            }
            else
            {
                radKieuHienThi.Enabled = true;
                chkChiTamUng.Visible = true;
            }
        }
    }
}


