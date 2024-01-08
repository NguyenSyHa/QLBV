using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace QLBV.FormNhap
{
    public partial class frmPhieuLinh_New : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuLinh_New()
        {
            InitializeComponent();
        }
        int i = 0;
        int[] arrIDdon;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region kt truoc khi tao so
        private bool KTtaoso()
        {
            if (i > 0)
            {
                if (string.IsNullOrEmpty(lupKhoa.Text))
                {
                    MessageBox.Show("Bạn chưa chọn khoa phòng ");
                    lupKhoa.Focus();
                    return false;

                }
                if (string.IsNullOrEmpty(cboLoaiDon.Text))
                {
                    MessageBox.Show("Bạn chưa nhập kiểu phiếu lĩnh");
                    cboLoaiDon.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(lupKhoD.Text))
                {
                    MessageBox.Show("Bạn chưa chọn kho xuất");
                    lupKhoD.Focus();
                    return false;

                }
                if (string.IsNullOrEmpty(lup_DTuong.Text))
                {
                    MessageBox.Show("Bạn chưa chọn đối tượng");
                    lup_DTuong.Focus();
                    return false;

                }

                if (lupNgay1.EditValue == null || lupNgay2.EditValue == null)
                {
                    MessageBox.Show("Bạn chưa nhập ngày tháng");
                    lupNgay1.Focus();
                    return false;
                }
                if (radNNT.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn chưa chọn nội trú hay ngoại trú");
                    radNNT.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region visible Ngay
        private void _visbleNgay(bool b)
        {
            colNgay1.Visible = b;
            colNgay2.Visible = b;
            colNgay3.Visible = b;
            colNgay4.Visible = b;
            colNgay5.Visible = b;
            if (b == true)
            {
                colNgay1.VisibleIndex = 4;
                colNgay2.VisibleIndex = 5;
                colNgay3.VisibleIndex = 6;
                colNgay4.VisibleIndex = 7;
                colNgay5.VisibleIndex = 8;
            }

        }
        #endregion
        #region ReadOnlyControl
        private void ReadOnlyControl(bool t)
        {
            lupKhoa.Properties.ReadOnly = t;
            lupNgay1.Properties.ReadOnly = t;
            lupNgay2.Properties.ReadOnly = t;
            cboLoaiDon.Properties.ReadOnly = t;
            //cboLoaiDuoc.Properties.ReadOnly = t;
            lupKhoD.Properties.ReadOnly = t;
            radNNT.Properties.ReadOnly = t;
            lup_DTuong.Properties.ReadOnly = t;
            //chkTreEm.Properties.ReadOnly = t;
        }
        #endregion
        #region danh sách bệnh nhân
        public class LDSBNhan
        {
            public string ten, dchi; double soluong; int tuoi;
            private int ma;
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public string TenBNhan
            {
                set { ten = value; }
                get { return ten; }
            }
            public int MaBNhan
            {
                set { ma = value; }
                get { return ma; }
            }
            public string DChi
            {
                set { dchi = value; }
                get { return dchi; }
            }
            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public int Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
        }
        #endregion
        List<LDSBNhan> _lBNhan = new List<LDSBNhan>();
        List<string> _lIDDonct = new List<string>();

        private void frmPhieuLinh_New_Load(object sender, EventArgs e)
        {
            i = 0;
            try
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                radNNT.SelectedIndex = 1;
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                lupNgay1.EditValue = ngaytu;
                lupNgay2.EditValue = ngayden;
                List<KPhong> Listkp = data.KPhongs.OrderBy(p => p.TenKP).ToList();
                lupTenDV.DataSource = data.DichVus.ToList();
                //var tenTN = (from tn in data.TieuNhomDVs.Where(p => p.Status == 1)
                //             join ndv in data.NhomDVs.Where(p => p.Status == 1) on tn.IDNhom equals ndv.IDNhom
                //             select new { tn.TenRG }).Where(p => p.TenRG != "").Distinct().OrderBy(p => p.TenRG).ToList();
                //lupLoaiDuoc.Properties.DataSource = tenTN.ToList();
                //lĩnh từ kho
                var w = from tk in Listkp where (tk.PLoai == ("Khoa dược")) select new { tk.TenKP, tk.MaKP };
                lupKhoD.Properties.DataSource = w.OrderBy(p => p.TenKP).ToList();
                //khoa kê
                var D = from tk in Listkp.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám") select new { tk.TenKP, tk.MaKP };
                lupKhoa.Properties.DataSource = D.ToList();
                i++;
                //đối tượng BN
                List<DTBN> _dtbn = data.DTBNs.ToList();
                _dtbn.Add(new DTBN { IDDTBN = 99, DTBN1 = " Tất cả" });
                lup_DTuong.Properties.DataSource = _dtbn.OrderBy(p => p.DTBN1).ToList();
                lup_DTuong.EditValue = 99;
                _visbleNgay(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnXem_CheckedChanged(object sender, EventArgs e)
        {
            bool xem = true;
            if (xem)
            {
                if (btnXem.Checked && KTtaoso())
                {
                    if (btnXem.Checked)
                    {
                        TimKiem(2);
                    }
                    else
                    {
                        grcPhieulinh.DataSource = "";
                    }
                }
                else
                {
                    if (i > 0)
                    {
                        btnXem.Checked = false;
                        grcPhieulinh.DataSource = "";
                    }
                }
                btnTaophieu.Enabled = btnXem.Checked;
                //chkLinhThang.Properties.ReadOnly = btnXem.Checked;
                ReadOnlyControl(btnXem.Checked);
            }
        }

        #region TimKiem
        private void TimKiem(int TThai)
        {
            try
            {
                //int TT = 0;//trạng thái 1 là xem dữ liệu, 2 là tạo báo cáo
                int khoa = 0;//khoa kê đơn
                int kho = 0; //mã kho
                int idDTBN = 99;
                if (lup_DTuong.EditValue != null)
                    idDTBN = Convert.ToInt32(lup_DTuong.EditValue);
                //string noitru = ""; //nội ngoại trú
                int noitru = 0;
                int loaidon = 0;
                int _mabn_DY = 0;
                //  string loaiduoc = lupLoaiDuoc.Properties.GetDisplayText(lupLoaiDuoc.EditValue);
                if (lupBN.EditValue != null)
                    _mabn_DY = Convert.ToInt32(lupBN.EditValue);
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                DateTime ngay = System.DateTime.Now.Date;
                if (KTtaoso())
                {
                    if (cboLoaiDon.Text == ("Ngoài giờ (trực)"))
                    {
                        loaidon = 5;
                    }
                    else if (cboLoaiDon.Text == "Tủ trực Ngtrú")
                    {
                        loaidon = 8;
                    }
                    else
                    {
                        loaidon = cboLoaiDon.SelectedIndex;
                    }
                    //loaidon = cboLoaiDon.SelectedIndex;
                    khoa = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
                    ngaytu = DungChung.Ham.NgayTu(lupNgay1.DateTime);
                    ngayden = DungChung.Ham.NgayDen(lupNgay2.DateTime);
                    kho = lupKhoD.EditValue == null ? 0 : Convert.ToInt32(lupKhoD.EditValue);

                    noitru = radNNT.SelectedIndex;
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    frmIn frm = new frmIn();

                    var iddon3 = (from kd in data.DThuocs.Where(p => p.NgayKe >= ngaytu && p.NgayKe <= ngayden)
                                  join dtct in data.DThuoccts.Where(o => o.IsMuaNgoai == null || o.IsMuaNgoai == false) on kd.IDDon equals dtct.IDDon
                                  join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                  join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                  where ((dtct.SoPL == 0) && (dtct.MaKXuat == kho)) && dtct.Status != -1
                                  select new
                                  {
                                      kd.IDDon,
                                      kd.NgayKe,
                                      kd.MaBNhan,
                                      dtct.IDDonct,
                                      SoPL = dtct.SoPL,
                                      KieuDon = kd.KieuDon ?? -1,
                                      MaKP = kd.MaKP ?? -1,
                                      MaKXuat = dtct.MaKXuat ?? -1,
                                      PLDV = kd.PLDV ?? -1,
                                      Status = dtct.Status ?? -1,
                                      tn.TenRG,
                                      dtct
                                  }).ToList();//.Where(p => p.TenRG.Equals(loaiduoc)).ToList();
                    List<int> _mabnhan = iddon3.Select(p => (p.MaBNhan ?? 0)).Distinct().ToList();
                    var bnhan = (from kd in _mabnhan
                                 join bn in data.BenhNhans on kd equals bn.MaBNhan
                                 select bn).ToList();
                    _mabnhan = (from bn in bnhan
                                where
                                    ((idDTBN == 99 ? true : bn.IDDTBN == idDTBN)
                                    && (chkLinhThang.Checked ? bn.MaBNhan == _mabn_DY : true)
                                    && (bn.NoiTru == noitru))
                                select bn.MaBNhan).ToList();
                    var iddon_tong = (from bn in _mabnhan
                                      join idd in iddon3 on bn equals idd.MaBNhan
                                      where ((idd.KieuDon == loaidon)
                                       && (idd.MaKP == khoa)
                                       && (idd.PLDV == 1)
                                       && (idd.dtct.Status == 0))
                                      select idd).ToList();
                    List<int> iddon = (from idd in iddon_tong
                                       select idd.IDDon).Distinct().ToList();

                    int j = 0;
                    arrIDdon = new int[iddon.Count];
                    foreach (var i in iddon)
                    {
                        arrIDdon[j] = i;
                        j++;
                    }
                    if (TThai == 1)
                    {
                    }
                    else
                    {
                        if (TThai == 2)
                        {
                            int[] ngayke_int = new int[5];
                            for (int i = 0; i < 5; i++)
                                ngayke_int[i] = 0;
                            if (chkLinhThang.Checked)
                            {
                                var abc = (from a in iddon_tong select a.NgayKe).Distinct().ToList();
                                int k = 0;
                                foreach (var item in abc)
                                {
                                    ngayke_int[k] = item.Value.Day;
                                    k++;
                                    if (k == 5)
                                        break;
                                }
                                _visbleNgay(chkLinhThang.Checked);
                                colNgay1.Caption = ngayke_int[0].ToString();
                                colNgay2.Caption = ngayke_int[1].ToString();
                                colNgay3.Caption = ngayke_int[2].ToString();
                                colNgay4.Caption = ngayke_int[3].ToString();
                                colNgay5.Caption = ngayke_int[4].ToString();
                            }
                            var q4 = (from a in iddon_tong
                                      group a by new { a.dtct.DonGia, a.dtct.DonVi, a.dtct.MaDV, a.dtct.Status } into kq
                                      select new
                                      {
                                          //kq.Key.IDDon,
                                          kq.Key.MaDV,
                                          DonVi = kq.Key.DonVi,
                                          DonGia = kq.Key.DonGia,
                                          SoLuong = kq.Sum(p => p.dtct.SoLuong),
                                          Ngay1 = kq.Where(p => p.NgayKe.Value.Day == ngayke_int[0]).Sum(p => p.dtct.SoLuong),
                                          Ngay2 = kq.Where(p => p.NgayKe.Value.Day == ngayke_int[1]).Sum(p => p.dtct.SoLuong),
                                          Ngay3 = kq.Where(p => p.NgayKe.Value.Day == ngayke_int[2]).Sum(p => p.dtct.SoLuong),
                                          Ngay4 = kq.Where(p => p.NgayKe.Value.Day == ngayke_int[3]).Sum(p => p.dtct.SoLuong),
                                          Ngay5 = kq.Where(p => p.NgayKe.Value.Day == ngayke_int[4]).Sum(p => p.dtct.SoLuong),
                                          ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                                      }).OrderBy(p => p.DonVi).ToList();
                            //lay danh sach IDDonct
                            _lIDDonct.Clear();
                            foreach (var item in iddon_tong)
                            {
                                _lIDDonct.Add(item.IDDonct.ToString());
                            }
                            // danh sách bn
                            _lBNhan.Clear();
                            _lBNhan = (from bn in bnhan
                                       join id in iddon_tong on bn.MaBNhan equals id.MaBNhan
                                       group new { bn, id } by new { bn.MaBNhan, bn.Tuoi, bn.DChi, id.dtct.MaDV, bn.TenBNhan } into kq
                                       select new LDSBNhan
                                       {
                                           DChi = kq.Key.DChi,
                                           MaBNhan = kq.Key.MaBNhan,
                                           SoLuong = kq.Sum(p => p.id.dtct.SoLuong),
                                           TenBNhan = kq.Key.TenBNhan,
                                           Tuoi = kq.Key.Tuoi ?? 0,
                                           MaDV = kq.Key.MaDV ?? 0,
                                       }).ToList();

                            grcPhieulinh.DataSource = q4.OrderBy(p => p.DonVi).ToList();
                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region function In phiếu lĩnh
        public static void InPhieu(int soPL, int maKp, int stt)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _status = 0;
            int _soPL = soPL;
            int _makp = 0;

            _makp = maKp;
            if (DungChung.Bien.MaBV != "19048")
                _makp = 0;
            _status = stt;
            bool mauA5 = false;// in mẫu A5 
            switch (_status)
            {
                case 2:
                    string _dtuong = "";
                    var ktdtuong3 = (from bn in _dataContext.BenhNhans
                                     join dtbn in _dataContext.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                     join dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                     on bn.MaBNhan equals dt.MaBNhan
                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                     select new { dt.NgayKe, bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru, dtbn.MoTa }).ToList();
                    var ktdtuong = (from bn in ktdtuong3
                                    group new { bn } by new { bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru } into kq
                                    select new { kq.Key.IDDTBN, kq.Key.DTuong, kq.Key.NoThe, kq.Key.NoiTru }).ToList();
                    if (ktdtuong.Count > 1)
                    {
                        _dtuong = "";
                    }
                    else
                    {
                        if (ktdtuong.Count > 0 && ktdtuong.First().NoThe == true)
                        {
                            _dtuong = "(Dành cho đối tượng dịch vụ _ nợ thẻ BHYT) \n";
                        }
                        else
                        {
                            var ktdtuong2 = (from bn in ktdtuong3
                                             select new { bn.MoTa }).ToList();
                            if (ktdtuong2.Count > 0)
                            {
                                _dtuong = "(Dành cho đối tượng " + ktdtuong2.First().MoTa + " ) \n";
                            }
                        }
                    }

                    var ngay = ktdtuong3.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();
                    string ngay1 = "";
                    string ngay2 = "";
                    if (ngay.Count > 0)
                    {
                        ngay1 = ngay.First().ToString().Substring(0, 10);
                        ngay2 = ngay.Last().ToString().Substring(0, 10);
                    }
                    frmIn frm = new frmIn();
                    var bph = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                               join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                               join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                               join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               join kp in _dataContext.KPhongs
                                   on kd.MaKP equals kp.MaKP
                               select new { kp.TenKP, tn.TenRG, kd.MaKXuat, kd.KieuDon, kd.MaKP, dtct.DonGia, dtct.ThanhTien, dtct.SoLuong, kd.GhiChu }).ToList();
                    if (bph.Count > 0)
                    {
                        int kieudon = 0;
                        kieudon = bph.First().KieuDon.Value;
                        if (kieudon == 2)
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                           join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                           join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                           select new { TenRGTN = tn.TenRG, kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0 }).ToList();


                                var q = (from kd in q33
                                         group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "" } into kq
                                         select new
                                         {
                                             HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                             SoLo = kq.Key.SoLo,
                                             MaDV = kq.Key.MaDV ?? 0,
                                             TenDV = kq.Key.TenDV,
                                             HamLuong = kq.Key.HamLuong,
                                             TenHC = kq.Key.TenHC,
                                             TenRG = kq.Key.TenRG,
                                             MaTam = kq.Key.MaTam,
                                             DonGia = kq.Key.DonGia,
                                             SoDK = kq.Key.SoDK,
                                             DonVi = kq.Key.DonVi,
                                             LoaiDuoc = kq.Key.LoaiDuoc,
                                             SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                             ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1)
                                         }).OrderBy(p => p.TenDV).ThenBy(p => p.DonVi).ThenBy(p => p.DonGia).ToList();

                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("SoPL", _soPL);
                                string tenKhoNhan = "";
                                int idKN = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                var khoNhan = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKN);
                                if (khoNhan != null)
                                    tenKhoNhan = khoNhan.TenKP;
                                string tenKhoXuat = "";
                                int idKX = bph.First().MaKP == null ? 0 : bph.First().MaKP.Value;
                                var khoXuat = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKX);
                                if (khoXuat != null)
                                    tenKhoXuat = khoXuat.TenKP;
                                dic.Add("TenKhoNhan", tenKhoNhan);
                                dic.Add("TenKhoXuat", tenKhoXuat);
                                dic.Add("LyDoNhap", "Nhập trả");
                                dic.Add("SoTienChu", (DungChung.Ham.DocTienBangChu(q.Sum(o => o.ThanhTien), "")));

                                DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuNhapKhoTraThuocThua_14018, q, dic, false);
                            }
                            else
                            {
                                string loaiduoc = bph.First().TenRG;
                                if ((loaiduoc == "Thuốc gây nghiện" || loaiduoc == "Thuốc hướng tâm thần") && DungChung.Bien.MaBV != "24009")
                                {
                                    #region 30002 _GN HTT
                                    if (DungChung.Bien.MaBV == "30002")
                                    {
                                        BaoCao.PhieutrathuocGNHTT_A5 rep = new BaoCao.PhieutrathuocGNHTT_A5();
                                        //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:.../BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    #endregion
                                    #region BV khác
                                    else
                                    {
                                        BaoCao.PhieutrathuocGNHTT rep = new BaoCao.PhieutrathuocGNHTT();
                                        //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:.../BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region PLThuoc VTYT 30002
                                    if (DungChung.Bien.MaBV == "30002")
                                    {
                                        BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:05D/BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    #endregion
                                    #region BV khác
                                    else
                                    {
                                        BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                                        var q1 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                  select new { kd.NgayKe, kd.GhiChu, kdct.MaKXuat }).First();
                                        
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                        if (DungChung.Bien.MaBV == "14017")
                                        {
                                            if (tekho == 27)
                                            {
                                                string a = bph.First().GhiChu == null ? "" : bph.First().GhiChu;
                                                if (a != "" && a.Contains(";") && a.Split(';').Length >= 3)
                                                {
                                                    ngay1 = a.Split(';')[1];
                                                    ngay2 = a.Split(';')[2];
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                }
                                                else
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            }
                                            else
                                                rep.theongay.Value = "";
                                        }else
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.xrLabel1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q1.NgayKe));
                                        rep.MauSo.Value = "MS:05D/BV-01";
                                        rep.xrTableCell67.Text = q.Count().ToString();
                                        //rep.xrLabel1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime())
                                        rep.DataSource = q.OrderBy(p => p.TenDV).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion
                                }
                            }
                        }
                        else
                        {

                            var q2 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                      join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      select new { kd, kdct, dv, tn }).ToList();

                            var q = (from kd in q2
                                     group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.tn.TenRG, kd.dv.MaTam, kd.kdct.DonGia, kd.kdct.ThanhTien, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc } into kq
                                     select new { kq.Key.TenRG, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, ThanhTien = kq.Key.ThanhTien, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                            kieudon = bph.First().KieuDon.Value;
                            string loaiduoc = bph.First().TenRG;
                            if ((loaiduoc == "Thuốc gây nghiện" || loaiduoc == "Thuốc hướng tâm thần"))
                            {
                                if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "19048" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "30002") // dung 0609
                                {
                                    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                    BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                    if (DungChung.Bien.MaBV == "12001")
                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                    rep.SoPL.Value = _soPL.ToString();
                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                    rep.Khoa.Value = bph.First().TenKP;
                                    if (tenkho.Count > 0)
                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                    switch (kieudon)
                                    {
                                        case 0:
                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                            break;
                                        case 1:
                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                            break;
                                        case 5:
                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                            break;
                                            //    case 2:
                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                            //break;
                                    }


                                    switch (loaiduoc)
                                    {
                                        case "Thuốc thường":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                            rep.MauSo.Value = "MS:01D/BV-01";
                                            break;
                                        case "Hóa chất":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                            rep.MauSo.Value = "MS:02D/BV-01";
                                            break;
                                        case "Vật tư y tế tiêu hao":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                            rep.MauSo.Value = "MS:03D/BV-01";
                                            break;
                                        case "Thuốc gây nghiện":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:08";
                                            break;
                                        case "Thuốc hướng tâm thần":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:08";
                                            break;
                                        //case "":
                                        //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                        //    rep.MauSo.Value = "MS:...D/BV-01";
                                        //    break;
                                        case "Thuốc đông y":
                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep.MauSo.Value = "MS:...D/BV-01";
                                            break;
                                    }


                                    rep.DataSource = q.OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    rep.BindingData();
                                    //rep.DataMember = "";
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                    //this.Dispose();
                                }
                                else
                                {
                                    if (DungChung.Bien.MaBV == "24009")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocGNHTT_24009 rep = new BaoCao.PhieulinhthuocGNHTT_24009();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case "":
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocGNHTT rep = new BaoCao.PhieulinhthuocGNHTT();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                }
                            }
                            else
                            {
                                if (loaiduoc == "Thuốc đông y")
                                {
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        var q61 = (from bn in _dataContext.BenhNhans
                                                   join kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                   join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                   select new
                                                   {
                                                       kd.MaKP,
                                                       bn.TenBNhan,
                                                       bn.DChi,
                                                       bn.MaBNhan,
                                                       MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong,
                                                       DTuong = bn.DTuong == null ? "" : bn.DTuong,
                                                       TenDV = dv.TenDV,
                                                       dv.MaDV,
                                                       dv.MaTam,
                                                       DonVi = dv.DonVi,
                                                       SoLuong = kdct.SoLuong,
                                                       DonGia = kdct.DonGia,
                                                       LoaiDuoc = kd.LoaiDuoc
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q6 = (from bn in q61
                                                  where (_makp == 0 ? true : bn.MaKP == _makp)
                                                  group new { bn } by new { bn.MaTam, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.DonGia, bn.DonVi, bn.MaDV, bn.TenDV, bn.LoaiDuoc } into kq
                                                  select new
                                                  {
                                                      kq.Key.TenBNhan,
                                                      kq.Key.DChi,
                                                      TenDV = kq.Key.TenDV,
                                                      kq.Key.MaDV,
                                                      kq.Key.MaTam,
                                                      DonVi = kq.Key.DonVi,
                                                      SoLuong139 = kq.Where(p => ((p.bn.DTuong == ("BHYT")) && (p.bn.MaDTuong == ("DT") || p.bn.MaDTuong == ("HN") || p.bn.MaDTuong == ("DK")))).Sum(p => p.bn.SoLuong),
                                                      SoLuongTE = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong == ("TE")).Sum(p => p.bn.SoLuong),
                                                      SoLuongBHYT = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong != "DT" && p.bn.MaDTuong != "HN" && p.bn.MaDTuong != "DK" && p.bn.MaDTuong != "TE").Sum(p => p.bn.SoLuong),
                                                      SoLuongDichVu = kq.Where(p => p.bn.DTuong == ("Dịch vụ")).Sum(p => p.bn.SoLuong),
                                                      SoLuong = kq.Sum(p => p.bn.SoLuong),
                                                      DonGia = kq.Key.DonGia,
                                                      LoaiDuoc = kq.Key.LoaiDuoc
                                                  }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD(6);
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;

                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }

                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }

                                        if (DungChung.Bien.MaBV == "30009")
                                        {
                                            var q7 = (from dongy in q6
                                                      group new { dongy } by new { dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                      select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                        }
                                        else
                                        {
                                            rep.DataSource = q6.OrderBy(p => p.DonVi).ToList();
                                        }
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        var q6 = (
                                                    from bn in _dataContext.BenhNhans
                                                    join kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp)) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    group new { kdct, dv, kd, bn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, bn.Tuoi, kd.MaBNhan, bn.TenBNhan, bn.DChi, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                    select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.MaTam, Tuoi = kq.Key.Tuoi, kq.Key.DChi, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();

                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }

                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }

                                        var q7 = (from dongy in q6
                                                  group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                  select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();

                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else
                                    {
                                        //if (ktdtuong.Count > 0 && ktdtuong.First().NoiTru==0)
                                        //{
                                        //    _InPhieuThuocDY_TT01(_soPL);
                                        //}
                                        //else
                                        //{
                                        #region 27021 đơn giá thành tiền, trừ thuốc gây nghiện và hướng tâm thần
                                        if (DungChung.Bien.MaBV == "27021" && loaiduoc != "Thuốc gây nghiện" && loaiduoc != "Thuốc hướng tâm thần")
                                        {
                                            var q61 = (from bn in _dataContext.BenhNhans
                                                       join kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp)) on bn.MaBNhan equals kd.MaBNhan
                                                       join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                       join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                       join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                       select new { bn.Tuoi, bn.DChi, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                            var q6 = (
                                                from kd in q61
                                                group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.Tuoi, kd.kd.MaBNhan, kd.TenBNhan, kd.DChi, kd.kdct.DonGia, kd.kdct.ThanhTien, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc } into kq
                                                select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.MaTam, Tuoi = kq.Key.Tuoi, kq.Key.DChi, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, ThanhTien = kq.Key.ThanhTien, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);

                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }

                                            switch (loaiduoc)
                                            {
                                                case "Thuốc thường":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                    break;
                                                case "Hóa chất":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                    break;
                                                case "Vật tư y tế tiêu hao":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                    break;
                                                //case "Thuốc gây nghiện":
                                                //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                //    rep.MauSo.Value = "MS:08";
                                                //    break;
                                                //case "Thuốc hướng tâm thần":
                                                //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                //    rep.MauSo.Value = "MS:08";
                                                //    break;
                                                //case 5:
                                                //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                //    rep.MauSo.Value = "MS:...D/BV-01";
                                                //    break;
                                                case "Thuốc đông y":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                    break;
                                            }


                                            rep.DataSource = q6.OrderBy(p => p.DonVi).ToList();

                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                        }
                                        #endregion
                                        else
                                        {
                                            var q61 = (from bn in _dataContext.BenhNhans
                                                       join kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp)) on bn.MaBNhan equals kd.MaBNhan
                                                       join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                       join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                       join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                       select new { bn.Tuoi, bn.DChi, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                            var q6 = (
                                                from kd in q61
                                                group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.Tuoi, kd.kd.MaBNhan, kd.TenBNhan, kd.DChi, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc } into kq
                                                select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.MaTam, Tuoi = kq.Key.Tuoi, kq.Key.DChi, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);

                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }

                                            switch (loaiduoc)
                                            {
                                                case "Thuốc thường":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                    break;
                                                case "Hóa chất":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                    break;
                                                case "Vật tư y tế tiêu hao":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                    break;
                                                case "Thuốc gây nghiện":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                case "Thuốc hướng tâm thần":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                //case 5:
                                                //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                //    rep.MauSo.Value = "MS:...D/BV-01";
                                                //    break;
                                                case "Thuốc đông y":
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                    break;
                                            }

                                            if (DungChung.Bien.MaBV == "30009")
                                            {
                                                var q7 = (from dongy in q6
                                                          group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                          select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                            }
                                            else
                                            {
                                                rep.DataSource = q6.OrderBy(p => p.DonVi).ToList();
                                            }
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            //this.Dispose();
                                        }
                                    }
                                    //}
                                }
                                else
                                {
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        var qTD3 = (from
                                                     bn in _dataContext.BenhNhans
                                                    join
                                                        kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp)) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    select new { bn.Tuoi, bn.DChi, bn.MaDTuong, bn.DTuong, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                        var qTD2 = (from kd in qTD3
                                                    group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.MaDTuong, kd.DTuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc } into kq
                                                    select new
                                                    {
                                                        kq.Key.MaTam,
                                                        MaDTuong = kq.Key.MaDTuong,
                                                        DTuong = kq.Key.DTuong,
                                                        TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV),
                                                        kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                        DonGia = kq.Key.DonGia,
                                                        LoaiDuoc = kq.Key.LoaiDuoc
                                                    }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var qTD = (from kqe in qTD2
                                                   group kqe by new { kqe.DonGia, kqe.MaTam, kqe.DonVi, kqe.MaDV, kqe.TenDV, kqe.LoaiDuoc } into kq
                                                   select new
                                                   {
                                                       kq.Key.MaTam,
                                                       TenDV = kq.Key.TenDV,
                                                       MaDV = kq.Key.MaDV,
                                                       DonVi = kq.Key.DonVi,
                                                       SoLuong = kq.Sum(p => p.SoLuong),
                                                       SoLuong139 = kq.Where(p => p.MaDTuong == ("DT") || p.MaDTuong == ("HN") || p.MaDTuong == ("DK")).Sum(p => p.SoLuong),
                                                       SoLuongTE = kq.Where(p => p.MaDTuong.Contains("TE")).Sum(p => p.SoLuong),
                                                       SoLuongBHYT = kq.Where(p => p.DTuong == ("BHYT") && p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong),
                                                       SoLuongDichVu = kq.Where(p => p.DTuong == ("Dịch vụ")).Sum(p => p.SoLuong),
                                                       DonGia = kq.Key.DonGia,
                                                       LoaiDuoc = kq.Key.LoaiDuoc
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = qTD.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else
                                    {

                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        //this.Dispose();

                                    }
                                }
                            }
                        }
                    }
                    break;
                case 3:

                    //rep3.Ngaythang.Value = ngay.ToString().Substring(0, 10);
                    var TD = (from dt in _dataContext.DThuocs
                              join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                              select new { dt.KieuDon }).ToList();
                    if (TD.First().KieuDon.Value != 4)
                    {
                        var q3 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                  group new { kdct, dv, kd, tn } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, tn.TenRG, dv.TenHC, dv.HamLuong, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                  select new { kq.Key.NgayKe, kq.Key.MaTam, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, kq.Key.TenRG }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                        if (q3.Count > 0)
                        {
                            int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                            var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                            string _ploaiKP = "";
                            if (ten.Count > 0)
                            {
                                _ploaiKP = ten.First().PLoai;
                            }
                            bool _inPhieuDT = true;
                            if (DungChung.Bien.MaBV == "30003")
                            {
                                DialogResult _result = MessageBox.Show("In phiếu dự trù thuốc?", "Hỏi mẫu in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.No)
                                    _inPhieuDT = false;
                            }
                            if (_ploaiKP.Contains("Khoa dược") && _inPhieuDT)
                            {
                                tenkp = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                string loaiduoc = q3.First().TenRG;
                                frmIn frm3 = new frmIn();
                                BaoCao.rep_dutruthuoc rep3 = new BaoCao.rep_dutruthuoc();
                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                rep3.SoPL.Value = _soPL.ToString();
                                rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                if (ten.Count > 0)
                                    rep3.Khoa.Value = ten.First().TenKP;
                                if (tenkho.Count > 0)
                                    rep3.Kholinh.Value = "Kính gửi:  " + tenkho.First().TenKP;
                                rep3.Loaiphieulinh.Value = "DỰ TRÙ THUỐC";
                                rep3.MauSo.Value = "MS:06D/BV-01";


                                rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                rep3.DataSource = q3.OrderBy(p => p.DonVi).ToList();
                                rep3.BindingData();
                                //rep.DataMember = "";
                                rep3.CreateDocument();
                                frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                frm3.ShowDialog();
                            }
                            else
                            {

                                tenkp = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                string loaiduoc = q3.First().TenRG;
                                if (loaiduoc == "Thuốc gây nghiện" || loaiduoc == "Thuốc hướng tâm thần")
                                {
                                    string _dtuong1 = "";
                                    var ktdtuong1 = (from bn in _dataContext.BenhNhans
                                                     join dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                     on bn.MaBNhan equals dt.MaBNhan
                                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                     group new { bn } by new { bn.IDDTBN, bn.DTuong, bn.NoThe } into kq
                                                     select new { kq.Key.IDDTBN, kq.Key.DTuong, kq.Key.NoThe }).ToList();
                                    if (ktdtuong1.Count > 1)
                                    {
                                        _dtuong = "";
                                    }
                                    else
                                    {
                                        if (ktdtuong1.Count > 0 && ktdtuong1.First().NoThe == true)
                                        {
                                            _dtuong1 = "(Dành cho đối tượng dịch vụ _ nợ thẻ BHYT) \n";
                                        }
                                        else
                                        {
                                            var ktdtuong2 = (from bn in _dataContext.BenhNhans
                                                             join dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp)) on bn.MaBNhan equals dt.MaBNhan
                                                             join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                             join dtbn in _dataContext.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                                             select new { dtbn.MoTa }).ToList();
                                            if (ktdtuong2.Count > 0)
                                            {
                                                _dtuong1 = "(Dành cho đối tượng " + ktdtuong2.First().MoTa + " ) \n";
                                            }
                                        }
                                    }
                                    var bph1 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                                                join kp in _dataContext.KPhongs
                                                    on kd.MaKP equals kp.MaKP
                                                select new { kp.TenKP, kd.LoaiDuoc, kd.MaKXuat, kd.KieuDon }).ToList();
                                    var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                             join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                             join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                             group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, tn.TenRG, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                             select new { kq.Key.TenRG, kq.Key.MaTam, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    var n3 = (from dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                              join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                              select new { dt.NgayKe }).ToList();
                                    var ngay3 = n3.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();
                                    string ngay4 = "";
                                    string ngay5 = "";
                                    if (ngay3.Count > 0)
                                    {
                                        ngay4 = ngay3.First().ToString().Substring(0, 10);
                                        ngay5 = ngay3.Last().ToString().Substring(0, 10);
                                    }
                                    int kieudon = bph1.First().KieuDon.Value;
                                    frmIn frm4 = new frmIn();
                                    int tekho = bph1.First().MaKXuat == null ? 0 : bph1.First().MaKXuat.Value;
                                    var tenkho1 = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                    if (DungChung.Bien.MaBV == "08204")
                                    {
                                        BaoCao.PhieulinhthuocGNHTT rep = new BaoCao.PhieulinhthuocGNHTT();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                        rep.Khoa.Value = bph1.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                break;
                                            case 3:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                break;
                                            case 4:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm4.ShowDialog();
                                        //this.Dispose();

                                    }
                                    else
                                        if (DungChung.Bien.MaBV == "24009")
                                    {
                                        BaoCao.PhieulinhthuocGNHTT_24009 rep = new BaoCao.PhieulinhthuocGNHTT_24009();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                        rep.Khoa.Value = bph1.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                break;
                                            case 3:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                break;
                                            case 4:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm4.ShowDialog();
                                        //this.Dispose();
                                    }
                                    else
                                    {
                                        BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                        rep.Khoa.Value = bph1.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                break;
                                            case 3:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                break;
                                            case 4:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                                //    case 2:
                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //break;
                                        }


                                        switch (loaiduoc)
                                        {
                                            case "Thuốc thường":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case "Hóa chất":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case "Vật tư y tế tiêu hao":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case "Thuốc gây nghiện":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            case "Thuốc hướng tâm thần":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                break;
                                            //case 5:
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                            //    rep.MauSo.Value = "MS:...D/BV-01";
                                            //    break;
                                            case "Thuốc đông y":
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                        }


                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm4.ShowDialog();
                                        //this.Dispose();
                                    }
                                }
                                else if (DungChung.Bien.MaBV == "30002")
                                {
                                    frmIn frm3 = new frmIn();
                                    BaoCao.Phieulinhchokhoa_A5 rep3 = new BaoCao.Phieulinhchokhoa_A5();
                                    var kt1 = (from dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                               join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                               join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                               join tn in _dataContext.TieuNhomDVs.Where(p => p.TenRG == "Y cụ") on dv.IdTieuNhom equals tn.IdTieuNhom
                                               select dt).ToList();
                                    if (kt1.Count > 0)
                                    {
                                        loaiduoc = "Y cụ";
                                        rep3.Ycu.Value = 7;
                                    }
                                    rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                    rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                    rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                    rep3.SoPL.Value = _soPL.ToString();
                                    rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                    rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                    var kieudon = (from dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                   select dt).ToList();
                                    var TD1 = kieudon.Select(p => p.KieuDon).ToList();
                                    if (TD1.First().Value != 2)
                                    {
                                        rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                    }
                                    else
                                    { rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                                    if (ten.Count > 0)
                                        rep3.Khoa.Value = ten.First().TenKP;
                                    if (tenkho.Count > 0)
                                        rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                    switch (loaiduoc)
                                    {
                                        case "Thuốc thường":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                            rep3.MauSo.Value = "MS:01D/BV-01";
                                            break;
                                        case "Hóa chất":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                            rep3.MauSo.Value = "MS:02D/BV-01";
                                            break;
                                        case "Vật tư y tế tiêu hao":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                            rep3.MauSo.Value = "MS:03D/BV-01";
                                            break;
                                        case "Thuốc gây nghiện":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep3.MauSo.Value = "MS:08";
                                            break;
                                        case "Thuốc hướng tâm thần":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep3.MauSo.Value = "MS:08";
                                            break;
                                        //case 5:
                                        //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                        //    rep.MauSo.Value = "MS:...D/BV-01";
                                        //    break;
                                        case "Thuốc đông y":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                            break;
                                        case "Y cụ":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                            break;
                                    }

                                    rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                    rep3.DataSource = q3.OrderBy(p => p.DonVi).ToList();
                                    rep3.BindingData();
                                    //rep.DataMember = "";
                                    rep3.CreateDocument();
                                    frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                    frm3.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm3 = new frmIn();
                                    BaoCao.Phieulinhchokhoa rep3 = new BaoCao.Phieulinhchokhoa();
                                    var kt1 = (from dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                               join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                               join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                               join tn in _dataContext.TieuNhomDVs.Where(p => p.TenRG == "Y cụ") on dv.IdTieuNhom equals tn.IdTieuNhom
                                               select dt).ToList();
                                    if (kt1.Count > 0)
                                    {
                                        loaiduoc = "Y cụ";
                                        rep3.Ycu.Value = 7;
                                    }
                                    rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                    rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                    rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                    rep3.SoPL.Value = _soPL.ToString();
                                    rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                    rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                    var kieudon = (from dt in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                   select dt).ToList();
                                    var TD1 = kieudon.Select(p => p.KieuDon).ToList();
                                    if (TD1.First().Value != 2)
                                    {
                                        rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                    }
                                    else
                                    { rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                                    if (ten.Count > 0)
                                        rep3.Khoa.Value = ten.First().TenKP;
                                    if (tenkho.Count > 0)
                                        rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                    switch (loaiduoc)
                                    {
                                        case "Thuốc thường":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                            rep3.MauSo.Value = "MS:01D/BV-01";
                                            break;
                                        case "Hóa chất":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                            rep3.MauSo.Value = "MS:02D/BV-01";
                                            break;
                                        case "Vật tư y tế tiêu hao":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                            rep3.MauSo.Value = "MS:03D/BV-01";
                                            break;
                                        case "Thuốc gây nghiện":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep3.MauSo.Value = "MS:08";
                                            break;
                                        case "Thuốc hướng tâm thần":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep3.MauSo.Value = "MS:08";
                                            break;
                                        //case 5:
                                        //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                        //    rep.MauSo.Value = "MS:...D/BV-01";
                                        //    break;
                                        case "Thuốc đông y":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                            break;
                                        case "Y cụ":
                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                            break;
                                    }

                                    rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                    rep3.DataSource = q3.OrderBy(p => p.DonVi).ToList();
                                    rep3.BindingData();
                                    //rep.DataMember = "";
                                    rep3.CreateDocument();
                                    frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                    frm3.ShowDialog();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "30002")
                        {
                            frmIn frm3 = new frmIn();
                            BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                            rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                            rep.SoPL.Value = _soPL.ToString();
                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                            var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                     join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                     join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                     group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe } into kq
                                     select new { kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                            rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                            var q3 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                      join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                      select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                            if (q3.Count > 0)
                            {
                                int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                rep.Khoa.Value = ten.First().TenKP;


                                int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                            }


                            rep.MauSo.Value = "MS:05D/BV-01";

                            rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                            rep.BindingData();
                            //rep.DataMember = "";
                            rep.CreateDocument();
                            frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm3.ShowDialog();
                            //this.Dispose();
                        }
                        else
                        {
                            frmIn frm3 = new frmIn();
                            BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                            rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                            rep.SoPL.Value = _soPL.ToString();
                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                            var q = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                     join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0 && p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                     join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                     group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe } into kq
                                     select new { kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                            rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                            var q3 = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                                      join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                      select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                            if (q3.Count > 0)
                            {
                                int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                rep.Khoa.Value = ten.First().TenKP;


                                int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                            }


                            rep.MauSo.Value = "MS:05D/BV-01";

                            rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                            rep.BindingData();
                            //rep.DataMember = "";
                            rep.CreateDocument();
                            frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm3.ShowDialog();
                            //this.Dispose();
                        }
                    }
                    //this.Dispose();
                    break;
            }
        }
        #endregion
        // lấy SoPL

        #region Set SoPL
        public static int _spl = 0;
        public static void SetSoPL(int maKP, List<string> IdDonct)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _soPL = 0;
            //if (DungChung.Bien.PP_SoLT == 1)// thay bằng số phiếu lĩnh
            //{ _soPL = DungChung.Ham.GetSoPL(1, maKP, -1); }
            //else if (DungChung.Bien.PP_SoLT == 2)
            //{ _soPL = DungChung.Ham.GetSoPL(1, 0, -1); }

            _soPL = DungChung.Ham.GetSoPL(1, 0, -1);

            //maKP = 0: khong yeu cau lay SoPL theo MaKP

            string idDonct = string.Empty;
            foreach (var item in IdDonct)
            {
                idDonct += item + ";";
            }
            idDonct = idDonct.Remove(idDonct.Length - 1, 1);
            if (data.SoPLs.Any(p => p.SoPL1 == _soPL))
            {
                _soPL = _soPL + 1;
                _spl = _soPL;
            }
            else
            {
                _soPL = _soPL;
                _spl = _soPL;
            }
            SoPL soPLMoi = new SoPL();
            // if (DungChung.Bien.PP_SoLT == 1)
            //{ soPLMoi.MaKP = maKP; }
            //else if (DungChung.Bien.PP_SoLT == 2)// thay bằng số phiếu linh
            //{ soPLMoi.MaKP = 0; }   
            soPLMoi.MaKP = 0;
            soPLMoi.SoPL1 = _soPL;
            soPLMoi.Status = 0;
            soPLMoi.PhanLoai = 1;
            soPLMoi.DSIdDonct = idDonct;
            soPLMoi.NoiTru = -1;
            data.SoPLs.Add(soPLMoi);
            data.SaveChanges();
        }
        #endregion
        #region Update SoPL
        public static void UpdateSoPL(List<string> iddonct)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int id = 0;
            foreach (var item in iddonct)
            {
                id = Convert.ToInt32(item);
                var dtct = data.DThuoccts.Where(p => p.IDDonct == id).FirstOrDefault();
                if (dtct != null && dtct.Status != -1)
                    dtct.SoPL = _spl;
                data.SaveChanges();
            }
        }
        #endregion

        private void btnTaophieu_Click(object sender, EventArgs e)
        {
            int khoa = 0;
            if (lupKhoa.EditValue != null)
                khoa = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            SetSoPL(khoa, _lIDDonct);
            UpdateSoPL(_lIDDonct);
            InPhieu(_spl, khoa, 2);
            btnXem_CheckedChanged(sender, e);
            //switch (_status)
            //{
            //    case 1:
            //        TaoPL(_id, _mbn, true);
            //        break;
            //    case 2:
            //        string[] pl = TaoPL(_arr);

            //        if (_thuocthang == false)
            //            InPhieu(pl, 2);
            //        else
            //        {
            //            if (pl[0].Length > 0)
            //            {
            //                int _soPL = Convert.ToInt32(pl[0]);
            //                FormNhap.frmPhieulinh._InPhieuThuocDY(_soPL);
            //                this.Dispose();
            //            }
            //        }
            //        break;
            //    case 3:
            //        string[] pl1 = TaoPL_1(iddon);
            //        InPhieu(pl1, 3);
            //        break;
            //    case 4:
            //        TaoPL(iddon, _sopl);
            //        break;
            //    case 5:
            //        var idd = _dataContext.DThuocs.Where(p => p.SoPL == iddon).Select(p => p.IDDon).ToList();
            //        foreach (var i in idd)
            //        {
            //            var sua = _dataContext.DThuocs.Single(p => p.IDDon == i);
            //            sua.SoPL = 0;
            //            _dataContext.SaveChanges();
            //        }
            //        this.Dispose();
            //        break;
            //    case 6:
            //        HuyDon(iddon);
            //        break;
            //    case 7:
            //        TaoPL(iddon);
            //        break;
            //    case 8:
            //        if (_id > 0)
            //        {
            //            var sua8 = _dataContext.DThuocs.Single(p => p.IDDon == _id);
            //            sua8.Status = 3;
            //            _dataContext.SaveChanges();
            //        }
            //        var sua81 = _dataContext.BenhNhans.Single(p => p.MaBNhan == _mbn);
            //        sua81.Status = 3;
            //        _dataContext.SaveChanges();
            //        this.Dispose();
            //        break;
            //}

        }

        private void grvPhieulinh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int _madv = 0;
            string _tendv = "";
            double _dongia = 0;
            if (grvPhieulinh.GetFocusedRowCellValue(colHanghoa) != null)
                _tendv = grvPhieulinh.GetFocusedRowCellValue(colHanghoa).ToString();
            if (grvPhieulinh.GetFocusedRowCellValue(colMaDV) != null)
            {
                if (grvPhieulinh.GetFocusedRowCellValue(colDonGia) != null)
                    _dongia = Convert.ToDouble(grvPhieulinh.GetFocusedRowCellValue(colDonGia));
                _madv = Convert.ToInt32(grvPhieulinh.GetFocusedRowCellValue(colMaDV));
                FormThamSo.frm_dsBNlinhthuoc frm = new FormThamSo.frm_dsBNlinhthuoc(_lBNhan.Where(p => p.MaDV == _madv).ToList(), _tendv, 0, _dongia);
                frm.ShowDialog();
            }
        }

        private void grvPhieulinh_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
        }

    }
}
