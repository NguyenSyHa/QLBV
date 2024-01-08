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
using QLBV.Class;

namespace QLBV.FormNhap
{
    public partial class frmPhieulinh : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieulinh()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "01071")
            {
                cboLoaiDon.Properties.Items.RemoveAt(3);
                cboLoaiDon.Properties.Items.Add("Tủ trực");
            }
            if (DungChung.Bien.MaBV == "27022")
            {
                cboLoaiDon.Properties.Items.Add("Ngoại trú");
            }
        }
        // int TT = 0;//1 là in bc, 2 la xem
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
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int i = 0;
        string _maCQCQ = "";
        List<TieuNhomDV> _ltieunhomDV = new List<TieuNhomDV>();
        List<DichVu> _ldv = new List<DichVu>();
        private void frmPhieulinh_Load(object sender, EventArgs e)
        {
            var qCQCQ = data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;
            chkLinhThang.Checked = false;
            i = 0;
            radNNT.SelectedIndex = 1;

            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            var D = from tk in _lkp.Where(p => p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang) select new { tk.TenKP, tk.MaKP };
            lupKhoa.Properties.DataSource = D.ToList();
            DateTime ngaytu = DungChung.Ham.NgayTu(System.DateTime.Now.Date);
            DateTime ngayden = DungChung.Ham.NgayDen(System.DateTime.Now.Date);
            lupNgay1.EditValue = ngaytu;
            lupNgay2.EditValue = ngayden;
            var w = from tk in _lkp where (tk.PLoai == ("Khoa dược") || tk.PLoai == ("Tủ trực")) select new { tk.TenKP, tk.MaKP };
            var chonkho = from tk in _lkp where (tk.PLoai == ("Khoa dược")) select new { tk.TenKP, tk.MaKP };
            lupKhoD.Properties.DataSource = w.ToList();
            lupKhoD.EditValue = DungChung.Bien.MaKho;
            lupKhoa.EditValue = DungChung.Bien.MaKP;

            lupChonKhoXuat.DataSource = chonkho.ToList();
            _ldv = data.DichVus.ToList();
            _ltieunhomDV = data.TieuNhomDVs.ToList();
            if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
            {

                foreach (var a in _ldv)
                    a.TenDV = a.TenRG ?? "";
            }

            else if (DungChung.Bien.MaBV == "26007")
            {

                foreach (var a in _ldv)
                    a.TenDV = a.TenDV + " " + a.HamLuong;
            }
            
            lupTenDV.DataSource = _ldv;
            i++;
            List<DTBN> _dtbn = data.DTBNs.ToList();
            //if (DungChung.Bien.MaTinh == "24")
            _dtbn.Add(new DTBN { IDDTBN = 99, DTBN1 = " Tất cả" });
            lup_DTuong.Properties.DataSource = _dtbn.OrderBy(p => p.DTBN1).ToList();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lup_DTuong.EditValue = lup_DTuong.Properties.GetKeyValueByDisplayText("BHYT");
            }
            else
                lup_DTuong.EditValue = 99;
            _visbleNgay(false);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        //List<BenhNhan> _lBNhan = new List<BenhNhan>();
        int[] arrIDdonct;
        List<DThuoc> _lDthuoc = new List<DThuoc>();
        private class Thuocthang
        {
            private string tendv;
            private string donvi;
            private double ngay1;
            private double ngay2;
            private double ngay3;
            private double ngay4;
            private double ngay5;
            private double thanhtien;
            private double thanhTien;
            private double soluong;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int sTT;

            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }
            private int madv;
            public int MaDV
            { set { madv = value; } get { return madv; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public double SoLuong
            { set { soluong = value; } get { return soluong; } }
            public double Ngay1
            { set { ngay1 = value; } get { return ngay1; } }
            public double Ngay2
            { set { ngay2 = value; } get { return ngay2; } }
            public double Ngay3
            { set { ngay3 = value; } get { return ngay3; } }
            public double Ngay4
            { set { ngay4 = value; } get { return ngay4; } }
            public double Ngay5
            { set { ngay5 = value; } get { return ngay5; } }
            public double DonGia
            { set { thanhtien = value; } get { return thanhtien; } }
        }
        private void TenBN()
        {

            DateTime NT = lupNgay1.DateTime;// DungChung.Ham.NgayTu(lupNgay1.DateTime);
            DateTime ND = lupNgay2.DateTime; //DungChung.Ham.NgayDen(lupNgay2.DateTime);
            int _Mak = 0, _MaKho = 0;
            if (lupKhoa.EditValue != null)    
            { _Mak = Convert.ToInt32(lupKhoa.EditValue); }
            if (lupKhoD.EditValue != null)
            { _MaKho = Convert.ToInt32(lupKhoD.EditValue); }
            int KD;
            if (cboLoaiDon.Text == ("Ngoài giờ (trực)"))
            {
                KD = 5;
            } else if (cboLoaiDon.Text == ("Ngoại trú"))
            {
                KD = -1;
            }
            else
            {
                KD = cboLoaiDon.SelectedIndex;
            }
            int _Noitru = radNNT.SelectedIndex;
            int _idDTBN = 99;
            if (lup_DTuong.EditValue != null)
            {
                _idDTBN = Convert.ToInt32(lup_DTuong.EditValue);
                //_Doituong2 = "Dịch vụ";
            }

            var q = (from bn in data.BenhNhans.Where(p => p.NoiTru == _Noitru && p.NoThe == chkNoThe.Checked).Where(p => (_idDTBN == 99 ? true : p.IDDTBN == _idDTBN))
                     join DT in data.DThuocs.Where(p => p.NgayKe >= NT && p.NgayKe <= ND).Where(p => p.KieuDon == KD).Where(p => p.MaKP == _Mak).Where(p => p.MaKXuat == _MaKho)//.Where(p => p.LoaiDuoc == cboLoaiDuoc.SelectedIndex)
                     on bn.MaBNhan equals DT.MaBNhan
                     group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi } into kq
                     select new { MaBN = kq.Key.MaBNhan, TenBN = kq.Key.TenBNhan, Tuoi = kq.Key.Tuoi }).ToList();
            lupBN.Properties.DataSource = q;
            lupBNNoThe.Properties.DataSource = q;
        }
        // int _mabn = 0;
        List<ChonKhoXuat> qtn = new List<ChonKhoXuat>();
        List<BenhNhan> _lbnTimKiem = new List<BenhNhan>();
        private void TimKiem(int TThai)
        {
            //\int TT = 0;//trạng thái 1 là xem dữ liệu, 2 là tạo báo cáo


            int khoa = 0;
            int kho = 0; //mã kho
            int idDTBN = 99;
            if (lup_DTuong.EditValue != null)
                idDTBN = Convert.ToInt32(lup_DTuong.EditValue);
            //string noitru = ""; //nội ngoại trú
            int noitru = 0;
            int loaidon = 0;
            int _mabn_NoThe = 0, _mabn_DY = 0;
            //if (lupBNNoThe.EditValue != null)
            //    _mabn_NoThe = Convert.ToInt32(lupBNNoThe.EditValue);
            if (lupBN.EditValue != null)
                _mabn_DY = Convert.ToInt32(lupBN.EditValue);
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            DateTime ngay = System.DateTime.Now.Date;
            if (KTtaoso())
            {
                if (cboLoaiDon.Text == ("Ngoài giờ (trực)") || cboLoaiDon.Text == "Tủ trực")
                {
                    loaidon = 5;
                }
                else if (cboLoaiDon.Text == "Tủ trực NgTrú")
                {
                    loaidon = 8;
                }
                else if (cboLoaiDon.Text == "Tủ trực CLS")
                {
                    loaidon = 7;
                }
                else if (cboLoaiDon.Text == "Ngoại trú")
                {
                    loaidon = -1;
                }
                else
                {
                    loaidon = cboLoaiDon.SelectedIndex;
                }
                //loaidon = cboLoaiDon.SelectedIndex;
                khoa = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);

                ngaytu = lupNgay1.DateTime;//DungChung.Ham.NgayTu(lupNgay1.DateTime);
                ngayden = lupNgay2.DateTime; //DungChung.Ham.NgayDen(lupNgay2.DateTime);
                kho = lupKhoD.EditValue == null ? 0 : Convert.ToInt32(lupKhoD.EditValue);

                noitru = radNNT.SelectedIndex;
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                frmIn frm = new frmIn();
                string _madt = "";
                if (chkTreEm.Checked)
                    _madt = "TE";
                var iddon3 = (from kd in data.DThuocs.Where(p => p.PLDV == 1)
                              join dtct in data.DThuoccts.Where(o => o.IsMuaNgoai == null || o.IsMuaNgoai == false) on kd.IDDon equals dtct.IDDon
                              where (dtct.NgayNhap >= ngaytu && dtct.NgayNhap <= ngayden)
                              select new { kd.IDDon, dtct.NgayNhap, kd.MaBNhan, SoPL = dtct.SoPL, KieuDon = kd.KieuDon ?? -1, MaKP = dtct.MaKP ?? -1, MaKXuat = dtct.MaKXuat ?? -1, PLDV = kd.PLDV ?? -1, Status = dtct.Status ?? -1, dtct }).ToList();
                List<int> _mabnhan = iddon3.Select(p => (p.MaBNhan ?? 0)).Distinct().ToList();
                _lbnTimKiem = data.BenhNhans.Where(p => DungChung.Bien.MaBV == "30005" ? p.NoThe == false : true).Where(p => _mabnhan.Contains(p.MaBNhan)).ToList();
                //(from kd in _mabnhan
                //         join bn in data.BenhNhans on kd equals bn.MaBNhan//.Where(p => (chkNoThe.Checked ? p.MaBNhan == _mabn_NoThe : p.NoThe == false))
                //         select bn).ToList();
                _mabnhan = (from bn in _lbnTimKiem
                            where
                                ((idDTBN == 99 ? true : bn.IDDTBN == idDTBN)
                                && (chkLinhThang.Checked ? bn.MaBNhan == _mabn_DY : true)
                                && (noitru == 2 || bn.NoiTru == noitru)
                                && (_madt.Length > 0 ? (bn.MaDTuong == _madt) : true))
                            select bn.MaBNhan).ToList();

                var iddon_tong = (from bn in _mabnhan
                                  join idd in iddon3 on bn equals idd.MaBNhan
                                  where ((idd.dtct.SoPL == 0)
                                  && (idd.KieuDon == loaidon)
                                  && (idd.MaKP == khoa)
                                  && (idd.MaKXuat == kho)
                                  && (idd.PLDV == 1)
                                  && (idd.dtct.Status == 0))
                                  select idd).ToList();

                List<int> iddon = new List<int>();

                //Là kho nhà thuốc thì load bệnh nhân lên cho chọn
                if (DungChung.Bien.MaBV == "34019" && nhaThuoc)
                {
                    var benhnhan = (from bn in _mabnhan
                                    join idd in iddon3 on bn equals idd.MaBNhan
                                    where ((idd.dtct.SoPL == 0)
                                    && (idd.KieuDon == loaidon)
                                    && (idd.MaKP == khoa)
                                    && (idd.MaKXuat == kho)
                                    && (idd.PLDV == 1)
                                    && (idd.dtct.Status == 0))
                                    select bn).ToList();

                    if (benhnhan.Count > 0)
                    {
                        var bn1 = (from b in data.BenhNhans.Where(o => benhnhan.Contains(o.MaBNhan))
                                   select new BenhNhanADO { MaBNhan = b.MaBNhan, TenBNhan = b.TenBNhan, Check = false, GioiTinh = (b.GTinh == 1 ? "Nam" : "Nữ"), Tuoi = (b.Tuoi ?? 0) }
                                     ).Distinct().ToList();
                        frmDSBN_TaoPhieuLinh frmDSBN = new frmDSBN_TaoPhieuLinh(bn1, DanhSachBN);
                        frmDSBN.ShowDialog();
                        _mabnhan = dsBnhan;
                        iddon = (from idd in iddon_tong.Where(o => _mabnhan.Contains(o.MaBNhan ?? 0))
                                 select idd.dtct.IDDonct).Distinct().ToList();
                        iddon_tong = iddon_tong.Where(o => _mabnhan.Contains(o.MaBNhan ?? 0)).ToList();
                    }
                }
                else
                {
                    iddon = (from idd in iddon_tong
                             select idd.dtct.IDDonct).Distinct().ToList();
                }

                int j = 0;
                arrIDdonct = new int[iddon.Count];
                foreach (var i in iddon)
                {
                    arrIDdonct[j] = i;
                    j++;
                }
                #region lên phiếu lĩnh tủ trực
                if (_tutruc)
                {
                    _ktraLaySoPL = true;
                    panelControl3.Visible = true;
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    //var qdtTuTruc = data.DThuoccts.Where(p=>iddon.Contains(p.IDDonct)).ToList();
                    qtn = (from dt in iddon_tong
                           join dv in _ldv on dt.dtct.MaDV equals dv.MaDV
                           join tn in _ltieunhomDV on dv.IdTieuNhom equals tn.IdTieuNhom
                           //  join bn in _lbnTimKiem on dt.MaBNhan equals bn.MaBNhan
                           select new ChonKhoXuat
                           {
                               IDDonct = dt.dtct.IDDonct,
                               IDDon = dt.IDDon,
                               SoPL = dt.SoPL,
                               MaDV = dv.MaDV,
                               TenDV = dv.TenDV,
                               SoLuong = dt.dtct.SoLuong,
                               SoLuongct = dt.dtct.SoLuongct,
                               DonGia = dt.dtct.DonGia,
                               SoLo = dt.dtct.SoLo,
                               DonVi = dt.dtct.DonVi,
                               DuongD = dt.dtct.DuongD,
                               DviUong = dt.dtct.DviUong,
                               HanDung = dt.dtct.HanDung,
                               LoaiDV = dt.dtct.Loai,
                               ThanhTien = dt.dtct.ThanhTien,
                               MaBNhan = dt.MaBNhan,
                               TenRG = tn.TenRG.Contains("Thuốc thường") ? "Thuốc thường" : tn.TenRG
                           }).ToList();


                    if (qtn.Count > 0)
                    {
                        List<ChonKhoXuat> lChonKhoXuat = new List<ChonKhoXuat>();
                        lChonKhoXuat = (from chonkho in qtn
                                        group chonkho by new { chonkho.SoPL, chonkho.TenRG } into kq
                                        select new ChonKhoXuat { Chon = true, Loai = kq.Key.TenRG, SoPL = kq.Key.SoPL, MaKP = 0 }
                        ).ToList();
                        grc_ChonKhoXuat.DataSource = lChonKhoXuat;
                    }
                    else
                        grc_ChonKhoXuat.DataSource = null;


                }
                #endregion
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

                            var abc = (from a in iddon_tong select a.NgayNhap).Distinct().ToList();
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
                                      kq.Key.MaDV,
                                      DonVi = kq.Key.DonVi,
                                      DonGia = kq.Key.DonGia,
                                      SoLuong = kq.Sum(p => p.dtct.SoLuong),
                                      Ngay1 = kq.Where(p => p.NgayNhap.Value.Day == ngayke_int[0]).Sum(p => p.dtct.SoLuong),
                                      Ngay2 = kq.Where(p => p.NgayNhap.Value.Day == ngayke_int[1]).Sum(p => p.dtct.SoLuong),
                                      Ngay3 = kq.Where(p => p.NgayNhap.Value.Day == ngayke_int[2]).Sum(p => p.dtct.SoLuong),
                                      Ngay4 = kq.Where(p => p.NgayNhap.Value.Day == ngayke_int[3]).Sum(p => p.dtct.SoLuong),
                                      Ngay5 = kq.Where(p => p.NgayNhap.Value.Day == ngayke_int[4]).Sum(p => p.dtct.SoLuong),
                                      ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                                  }).OrderBy(p => p.DonVi).ToList();

                        // danh sách bn
                        _lBNhan.Clear();
                        _lBNhan = (from bn in _lbnTimKiem
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
                        int x = 1;
                        List<Thuocthang> ds = new List<Thuocthang>();
                        if (DungChung.Bien.MaBV == "30002")
                            q4 = q4.OrderBy(p => p.MaDV).ToList();
                        else
                            q4 = q4.OrderBy(p => p.DonVi).ToList();
                        foreach (var item in q4)
                        {
                            Thuocthang moi = new Thuocthang();
                            moi.STT = x++;
                            moi.MaDV = item.MaDV ?? 0;
                            moi.DonVi = item.DonVi;
                            moi.SoLuong = item.SoLuong;
                            moi.ThanhTien = item.ThanhTien;
                            moi.DonGia = item.DonGia;
                            moi.Ngay1 = item.Ngay1;
                            moi.Ngay2 = item.Ngay2;
                            moi.Ngay3 = item.Ngay3;
                            moi.Ngay4 = item.Ngay4;
                            moi.Ngay5 = item.Ngay5;
                            ds.Add(moi);
                        }
                        if (DungChung.Bien.MaBV == "30002")
                            grcPhieulinh.DataSource = ds;
                        else
                            grcPhieulinh.DataSource = ds;


                    }
                }

            }
        }
        int _soPLThuocYHDT = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_tutruc)
            {
                if (ktraTon())
                {
                    bool kt = true;
                    int makhoake = Convert.ToInt32(lupKhoD.EditValue);
                    List<ChonKhoXuat> lchonkhoxuat = new List<ChonKhoXuat>();
                    for (int i = 0; i < grv_ChonKhoXuat.RowCount; i++)
                    {

                        ChonKhoXuat chon = new ChonKhoXuat();
                        chon.MaKP = Convert.ToInt32(grv_ChonKhoXuat.GetRowCellValue(i, colChonKhoXuat));
                        if (grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai) != null && grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai) != "")
                            chon.TenRG = grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai).ToString();
                        lchonkhoxuat.Add(chon);
                    }

                    frm_Check_moi frmTaoPL = new frm_Check_moi();
                    foreach (ChonKhoXuat kx in lchonkhoxuat)
                    {

                        var qdtct = qtn.Where(p => p.TenRG == kx.TenRG && !_maDVHet.Contains(p.MaDV)).ToList();
                        if (qdtct.Count > 0)
                        {
                            // List<int> qdsIDDonct = qdtct.Select(p => p.IDDonct).ToList();
                            List<int> qdsMaBNhan = qdtct.Select(p => p.MaBNhan ?? 0).Distinct().ToList();
                            List<int> liddct = new List<int>();
                            foreach (int mabn in qdsMaBNhan)
                            {
                                var qdt = (from dt in qdtct.Where(p => p.MaBNhan == mabn)
                                           group dt by new { dt.DonGia, dt.DonVi, dt.DuongD, dt.DviUong, dt.HanDung, dt.Loai, dt.MaDV, dt.SoLo } into kq
                                           select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.DuongD, kq.Key.DviUong, kq.Key.HanDung, kq.Key.Loai, kq.Key.MaDV, kq.Key.SoLo, SoLuong = kq.Sum(p => p.SoLuong), SoLuongct = kq.Sum(p => p.SoLuongct), ThanhTien = kq.Sum(p => p.ThanhTien) }).ToList();


                                DThuoc dthuocNew = new DThuoc();
                                dthuocNew.KieuDon = 3;
                                dthuocNew.NgayKe = DateTime.Now;
                                dthuocNew.PLDV = 1;
                                dthuocNew.MaCB = DungChung.Bien.MaCB;
                                dthuocNew.MaKP = makhoake;
                                dthuocNew.MaKXuat = kx.MaKP;
                                dthuocNew.MaBNhanChiTiet = mabn;
                                data.DThuocs.Add(dthuocNew);
                                data.SaveChanges();


                                foreach (var a in qdt)
                                {
                                    DThuocct moi = new DThuocct();
                                    moi.IDDon = dthuocNew.IDDon;
                                    moi.DonGia = a.DonGia;
                                    moi.DonVi = a.DonVi;
                                    moi.DuongD = a.DuongD;
                                    moi.DviUong = a.DviUong;
                                    moi.HanDung = a.HanDung;
                                    moi.Loai = 0;
                                    moi.MaCB = DungChung.Bien.MaCB;
                                    moi.MaDV = a.MaDV;
                                    moi.MaKP = makhoake;
                                    moi.MaKXuat = dthuocNew.MaKXuat;
                                    moi.SoLuong = a.SoLuong;
                                    moi.SoLuongct = a.SoLuongct;
                                    moi.SoLo = a.SoLo;
                                    // moi.SoPL = sopl;
                                    moi.Status = 0;
                                    moi.ThanhTien = a.ThanhTien;
                                    data.DThuoccts.Add(moi);
                                    data.SaveChanges();
                                    liddct.Add(moi.IDDonct);

                                }
                            }
                            List<string[]> dsPLDaTao = new List<string[]>();
                            List<string[]> lsoPL = frmTaoPL.TaoPL(liddct.ToArray(), ref dsPLDaTao);
                            int sopl = 0;
                            if (lsoPL.Count > 0)
                                sopl = Convert.ToInt32(lsoPL.First()[0]);
                            int iddtbn = 99;
                            if (lup_DTuong.EditValue != null)
                                iddtbn = Convert.ToInt32(lup_DTuong.EditValue);

                            //var qdtbn = data.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                            //if (qdtbn != null)
                            //    iddtbn = qdtbn.IDDTBN;
                            foreach (var a in qdtct)
                            {
                                DThuocct dt = data.DThuoccts.Where(p => p.IDDonct == a.IDDonct).FirstOrDefault();
                                if (dt != null)
                                {
                                    dt.DSCBTH = sopl.ToString();// lưu vào trường dscbth số phiếu lĩnh kho kê 
                                    dt.Status = 1;
                                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                    {
                                        if (dt.XHH == null || dt.XHH == 0)
                                            dt.XHH = iddtbn; // lưu đối tượng bệnh nhân hiển thị trên phiếu lĩnh
                                    }
                                }
                                data.SaveChanges();
                            }
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                            {
                                for (int i = 0; i < lsoPL.Count; i++)
                                {
                                    for (int j = 0; j < lsoPL.Skip(i).First().Length; j++)
                                    {
                                        int spl = -2;
                                        string strsopl = (lsoPL.Skip(i).First())[j].ToString();
                                        spl = Convert.ToInt32(strsopl);
                                        List<DThuocct> ldt = data.DThuoccts.Where(p => spl != 0 && p.SoPL == spl).ToList();
                                        foreach (DThuocct dt in ldt)
                                        {
                                            if (dt.XHH == null || dt.XHH == 0)
                                                dt.XHH = iddtbn;
                                        }
                                    }
                                    data.SaveChanges();
                                }
                            }
                            if (lsoPL.Count > 0)
                                frmTaoPL.InPhieu(lsoPL.First(), 2);

                        }
                    }

                    DateTime ngaytu = lupNgay1.DateTime;
                    DateTime ngayden = lupNgay2.DateTime;
                    btnXem_CheckedChanged(sender, e);
                }
            }
            else
            {
                if (DungChung.Bien.MaBV == "24297" && chkLinhThang.Checked == true) // bv24297 in phieu dong y rep_phieulinhthuoc_20001
                {
                    frm_Check_moi frm = new frm_Check_moi(arrIDdonct, 2, chkLinhThang.Checked, true, 1);
                    frm.ShowDialog();
                }
                else
                {
                    frm_Check_moi frm = new frm_Check_moi(arrIDdonct, 2, chkLinhThang.Checked, false, 1);
                    frm.ShowDialog();
                }
                DateTime ngaytu = lupNgay1.DateTime;
                DateTime ngayden = lupNgay2.DateTime;
                btnXem_CheckedChanged(sender, e);
            }

        }



        private void simpleButton3_Click(object sender, EventArgs e)
        {

            TimKiem(2);

        }
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
        List<LDSBNhan> _lBNhan = new List<LDSBNhan>();
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



        private void ReadOnlyControl(bool t)
        {
            lupKhoa.Properties.ReadOnly = t;
            lupNgay1.Properties.ReadOnly = t;
            lupNgay2.Properties.ReadOnly = t;
            cboLoaiDon.Properties.ReadOnly = t;

            lupKhoD.Properties.ReadOnly = t;
            radNNT.Properties.ReadOnly = t;
            lup_DTuong.Properties.ReadOnly = t;
            chkTreEm.Properties.ReadOnly = t;
        }
        bool thaydoi = true;
        bool _ktraLaySoPL = false;//kiểm tra nếu đã lấy số phiếu lính tủ trực (khi click nút xem)
        private void btnXem_CheckedChanged(object sender, EventArgs e)
        {
            bool _xem = true;
            //if (DungChung.Bien.MaBV == "30009" && cboDTuong.SelectedIndex==1) { 

            //}

            if (_xem)
            {
                _ktraLaySoPL = false;
                if (btnXem.Checked && KTtaoso())
                {
                    if (btnXem.Checked)
                    {
                        TimKiem(2);
                        //chkLinhThang.Properties.ReadOnly = false;
                    }
                    else
                    {
                        grcPhieulinh.DataSource = "";
                        grc_ChonKhoXuat.DataSource = null;
                    }
                }
                else
                {
                    if (i > 0)
                    {
                        btnXem.Checked = false;
                    }
                }
                btnTaophieu.Enabled = btnXem.Checked;
                //chkLinhThang.Properties.ReadOnly = btnXem.Checked;
                ReadOnlyControl(btnXem.Checked);
            }
        }

        private void lupBN_Click(object sender, EventArgs e)
        {

        }



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

        private void chkLinhThang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLinhThang.Checked)
            {
                TenBN();

            }
            _visbleNgay(chkLinhThang.Checked);
            lupBN.Visible = chkLinhThang.Checked;
            labelBenhNhan.Visible = chkLinhThang.Checked;
        }

        //private void cboLoaiDuoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboLoaiDuoc.SelectedIndex == 6)
        //    {
        //        chkLinhThang.Visible = true;
        //        paner_NoThe.Visible = false;
        //    }
        //    else
        //    {
        //        chkLinhThang.Visible = false;

        //    }
        //    if (chkNoThe.Checked)
        //    {
        //        TenBN();
        //    }
        //    chkLinhThang.Checked = false;
        //}


        public static void _InPhieuThuocDY(int _spl)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<Thuocthang> _BC = new List<Thuocthang>();
            _BC.Clear();
            var a = (from dt in Data.DThuocs
                     join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                     join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                     group new { dt, dtct, dv } by new { dt.KieuDon, dt.NgayKe.Value.Day, dtct.MaDV, dv.TenDV, dt.GhiChu } into kq
                     select new
                     {
                         kq.Key.KieuDon,
                         NgayKe = kq.Key.Day,
                         Madv = kq.Key.MaDV,
                         TenDV = kq.Key.TenDV,
                         kq.Key.GhiChu,
                         Soluong = kq.Sum(p => p.dtct.SoLuong),
                         Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                     }).OrderBy(p => p.NgayKe).ToList();

            int _a1 = 0, _a2 = 0, _a3 = 0, _a4 = 0, _a5 = 0;
            string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "";
            string sothang1 = "", sothang2 = "", sothang3 = "", sothang4 = "", sothang5 = "";
            var q0 = (from DT in Data.DThuocs
                      join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on DT.IDDon equals dtct.IDDon
                      select new { DT.NgayKe, GhiChu = DT.GhiChu == null ? "" : DT.GhiChu, DT.IDDon, DT.KieuDon, DT.LoaiDuoc, DT.MaBNhan, DT.MaCB, DT.MaKP, DT.MaKXuat, DT.PLDV, dtct.SoPL, dtct.Loai }).ToList();
            var q = (from DT in q0// Data.DThuocs.Where(p => p.SoPL == _spl)
                     group new { DT } by new { DT.NgayKe.Value.Day, DT.NgayKe.Value.Month, DT.Loai } into kq
                     select new { Ngay = kq.Key.Day, kq.Key.Loai, Thang = kq.Key.Month, GhiChu = string.Join(",", kq.Select(p => p.DT.GhiChu)) }).OrderBy(p => p.Thang).ThenBy(p => p.Ngay).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                if (q.Skip(i).First().Ngay != null && q.Skip(i).First().Ngay.ToString() != "")
                {
                    switch (i)
                    {
                        case 0:
                            _a1 = q.Skip(i).First().Ngay;
                            a1 = q.Skip(i).First().Ngay.ToString() + "/" + q.Skip(i).First().Thang;
                            //sothang1 = q.Skip(i).First().GhiChu;
                            sothang1 = Convert.ToString(q.Skip(i).First().Loai);
                            break;
                        case 1:
                            _a2 = q.Skip(i).First().Ngay;
                            a2 = q.Skip(i).First().Ngay.ToString() + "/" + q.Skip(i).First().Thang;
                            //sothang2 = q.Skip(i).First().GhiChu;
                            sothang2 = Convert.ToString(q.Skip(i).First().Loai);
                            break;
                        case 2:
                            _a3 = q.Skip(i).First().Ngay;
                            a3 = q.Skip(i).First().Ngay.ToString() + "/" + q.Skip(i).First().Thang;
                            //sothang3 = q.Skip(i).First().GhiChu;
                            sothang3 = Convert.ToString(q.Skip(i).First().Loai);
                            break;
                        case 3:
                            _a4 = q.Skip(i).First().Ngay;
                            a4 = q.Skip(i).First().Ngay.ToString() + "/" + q.Skip(i).First().Thang;
                            //sothang4 = q.Skip(i).First().GhiChu;
                            sothang4 = Convert.ToString(q.Skip(i).First().Loai);
                            break;
                        case 4:
                            _a5 = q.Skip(i).First().Ngay;
                            a5 = q.Skip(i).First().Ngay.ToString() + "/" + q.Skip(i).First().Thang;
                            //sothang5 = q.Skip(i).First().GhiChu;
                            sothang5 = Convert.ToString(q.Skip(i).First().Loai);
                            break;
                    }
                }
            }
            var dichvu = (from dt in Data.DThuocs
                          join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                          join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                          group new { dtct, dv } by new { dtct.MaDV, dv.TenDV, dtct.DonVi } into kq
                          select new
                          {
                              //NgayKe = kq.Key.Day,
                              Madv = kq.Key.MaDV,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              //Soluong = kq.Sum(p => p.dtct.SoLuong),
                              Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                          }).OrderBy(p => p.TenDV).ToList();
            foreach (var c in dichvu)
            {
                Thuocthang themmoi = new Thuocthang();
                themmoi.DonVi = c.DonVi;
                themmoi.MaDV = c.Madv == null ? 0 : c.Madv.Value;
                themmoi.TenDV = c.TenDV;
                themmoi.DonGia = c.Thanhtien;
                _BC.Add(themmoi);
            }
            foreach (var d in _BC)
            {
                foreach (var n in a)
                {
                    if (d.MaDV == n.Madv)
                    {
                        if (n.NgayKe == _a1)
                        { d.SoLuong = n.Soluong; }
                        if (n.NgayKe == _a2)
                        { d.Ngay2 = n.Soluong; }
                        if (n.NgayKe == _a3)
                        { d.Ngay3 = n.Soluong; }
                        if (n.NgayKe == _a4)
                        { d.Ngay4 = n.Soluong; }
                        if (n.NgayKe == _a5)
                        { d.Ngay5 = n.Soluong; }
                    }
                }
            }

            BaoCao.Rep_PLThuoctheothang rep = new BaoCao.Rep_PLThuoctheothang();
            frmIn frm = new frmIn();
            rep.SoPL.Value = _spl;
            if (a.Count > 0 && a.First().KieuDon != null && a.First().KieuDon == 2)
            {
                rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC THANG";
            }
            var bg = (from dt in Data.DThuocs
                      join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                      join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                      join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                      join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                      where (dt.MaKP == bnkb.MaKP)
                      select new { bnkb.Buong, bnkb.Giuong, kp.TenKP, bn.TenBNhan, bn.Tuoi, bn.DChi, bnkb.ChanDoan, bn.MaBNhan }).ToList();

            if (bg.First().Buong != null && bg.First().Buong.ToString() != "")
            {
                string buong = bg.First().Buong.ToString();
                if (bg.First().Giuong != null && bg.First().Giuong.ToString() != "")
                {
                    buong += "  Giường: " + bg.First().Giuong.ToString();
                }
                rep.BuongGiuong.Value = "Buồng: " + buong;
            }
            if (bg.First().TenKP != null && bg.First().TenKP.ToString() != "")
            {
                rep.Khoa.Value = "Khoa: " + DungChung.Ham.GetTenKP_20001(bg.First().TenKP.ToString());
            }
            if (bg.First().TenBNhan != null && bg.First().TenBNhan.ToString() != "")
            {
                if (DungChung.Bien.MaBV == "30012")
                    rep.Hoten.Value = "Họ và tên người bệnh: " + bg.First().TenBNhan;
                else
                    rep.Hoten.Value = "Họ tên bệnh nhân: " + bg.First().TenBNhan;
            }

            if (DungChung.Bien.MaBV == "27021" && bg.First().MaBNhan != null)
            {
                int mabn = bg.First().MaBNhan;
                var qvv = (from vv in Data.VaoViens.Where(p => p.MaBNhan == mabn) select vv).FirstOrDefault();
                if (qvv != null)
                    rep.SoBA.Value = "Số V.V: " + qvv.SoVV;
            }
            if (bg.First().Tuoi != null && bg.First().Tuoi.ToString() != "")
            { rep.Tuoi.Value = "Tuổi: " + bg.First().Tuoi; }
            if (bg.First().DChi != null && bg.First().DChi.ToString() != "")
            { rep.Diachi.Value = "Địa chỉ: " + bg.First().DChi; }
            string _CD = "Chẩn đoán: ";
            if (bg.First().ChanDoan != null && bg.First().ChanDoan.ToString() != "")
            { rep.Chandoan.Value = "Chẩn đoán: " + bg.First().ChanDoan; }
            else { rep.Chandoan.Value = _CD; }
            rep.N1.Value = a1;
            rep.N2.Value = a2;
            rep.N3.Value = a3;
            rep.N4.Value = a4;
            rep.N5.Value = a5;
            if (DungChung.Bien.MaBV == "27021")
            {
                rep.celSoThang1.Text = sothang1;
                rep.celSoThang2.Text = sothang2;
                rep.celSoThang3.Text = sothang3;
                rep.celSoThang4.Text = sothang4;
                rep.celSoThang5.Text = sothang5;
            }
            if (DungChung.Bien.MaBV == "30002")
                rep.DataSource = _BC.OrderBy(p => p.MaDV).ToList();
            else
                rep.DataSource = _BC;

            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();///


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_spl"></param>
        /// <param name="PLoaiThuoc">PLoaiThuoc = 0: Đông ý/ 1: thuốc thường</param>
        public static void _InPhieuLinh_20001(int _spl, int PLoaiThuoc)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon); 
            var kp1 = (from a in Data.DThuocs
                      join b in Data.DThuoccts.Where(p => p.SoPL == _spl) on a.IDDon equals b.IDDon
                      select new
                      {
                          a.KieuDon,
                          a.MaKP 
                      }).ToList();

            
            var dichvu = (from dt in Data.DThuocs
                          join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                          join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                          group new { dt, dtct, dv } by new { dv.MaTam, GhiChu = DungChung.Bien.MaBV == "14017" ? (dtct.GhiChu) : (dt.GhiChu), dtct.MaDV, dv.TenDV, dtct.DonVi, dv.MaQD, DonGia = DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "24297" ? (dtct.DonGia) : 0 } into kq
                          select new
                          {
                              MaBN = kq.Min(p => p.dt.MaBNhan),
                              NgayKe = kq.Min(p => p.dtct.NgayNhap),
                              Madv = kq.Key.MaDV,
                              kq.Key.MaQD,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              DonGia = kq.Key.DonGia,
                              SoLuongct = kq.Sum(p => p.dtct.SoLuongct),
                              SoLuong = kq.Sum(p => p.dtct.SoLuong),
                              SoLuongTong = kq.Sum(p => p.dtct.SoLuong),
                              GhiChu = kq.Key.GhiChu,
                              SoThang = kq.Max(p => p.dtct.Loai),
                              Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                              MaTam = kq.Key.MaTam,


                          }).OrderBy(p => p.TenDV).ToList();
            if (dichvu.Count <= 0)
                return;
            if(kp1.Count > 0 && DungChung.Bien.MaBV == "14017" && kp1.First().KieuDon == 2)
            {
                FormNhap.frmPhieuLinh_New.InPhieu(_spl, kp1.First().MaKP ?? 0, 2);
            }
            if (DungChung.Bien.MaBV == "27021")
            {
                BaoCao.rep_PhieuLinhThuoc_27021 rep = new BaoCao.rep_PhieuLinhThuoc_27021();
                int mabn = dichvu.First().MaBN ?? 0;
                var qvv = (from bn in Data.BenhNhans.Where(p => p.MaBNhan == mabn) join vv in Data.VaoViens.Where(p => p.MaBNhan == mabn) on bn.MaBNhan equals vv.MaBNhan select new { bn.MaBNhan, vv.NgayVao, vv.SoVV, bn.NoiTru }).FirstOrDefault();
                if (qvv != null)
                {
                    rep.SoVV.Value = qvv.SoVV;
                }


                frmIn frm = new frmIn();
                rep.SoPL.Value = _spl;

                if (dichvu.Count > 0 && dichvu.First().SoLuong < 0)
                {
                    rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC";
                }
                var bg = (from dt in Data.DThuocs
                          join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                          join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                          join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                          join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          where (dtct.MaKP == bnkb.MaKP)
                          select new { bnkb.Buong, bnkb.Giuong, kp.TenKP, bn.TenBNhan, bn.Tuoi, bn.DChi, ChanDoan = bnkb.ChanDoan + " " + bnkb.BenhKhac, bn.MaBNhan, dt.GhiChu, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen}).ToList(); // dung sửa lấy ghi chú (ngày sử dụng) từ bảng BNKB sang  bảng dthuoc


                if (bg.First().Buong != null)
                {
                    string buong = bg.First().Buong.ToString();
                    rep.BuongGiuong.Value = "Buồng: " + buong;
                }
                if (bg.First().TenKP != null && bg.First().TenKP.ToString() != "")
                {
                    rep.Khoa.Value = "Khoa: " + bg.First().TenKP.ToString();
                }
                if (bg.First().TenBNhan != null && bg.First().TenBNhan.ToString() != "")
                {
                    rep.Hoten.Value = bg.First().TenBNhan.ToUpper();
                }
                rep.SoThe.Value = bg.First().SThe;
                if (bg.First().GTinh == 0)
                    rep.GT.Value = "Nữ";
                else rep.GT.Value = "Nam";
                if (bg.First().HanBHTu != null && bg.First().HanBHDen != null && bg.First().HanBHTu.ToString() != "" && bg.First().HanBHDen.ToString() != "")
                {
                    rep.BHYT.Value = "BHYT giá trị từ: " + bg.First().HanBHTu.ToString().Remove(11) + " đến " + bg.First().HanBHDen.ToString().Remove(11);
                }
                if (bg.First().Tuoi != null && bg.First().Tuoi.ToString() != "")
                { rep.Tuoi.Value = bg.First().Tuoi; }
                if (bg.First().DChi != null && bg.First().DChi.ToString() != "")
                { rep.Diachi.Value = "Địa chỉ: " + bg.First().DChi; }
                string _CD = "Chẩn đoán: ";
                if (bg.First().ChanDoan != null && bg.First().ChanDoan.ToString() != "")
                { rep.ChanDoan.Value = "Chẩn đoán: " + DungChung.Ham.FreshString(bg.First().ChanDoan); }
                else { rep.ChanDoan.Value = _CD; }


                #region cuongtm 29/06/17
                int sothang = 1;
                if (PLoaiThuoc == 0)
                {


                }
                else
                {

                    rep.lblSoNgay.Text = "Số ngày:";
                }
                sothang = dichvu.First().SoThang;
                DateTime ngaytu = dichvu.First().NgayKe.Value.Date;
                DateTime ngayden = dichvu.First().NgayKe.Value.Date.AddDays(sothang - 1);
                rep.NgayTu.Value = ngaytu.ToShortDateString();
                rep.NgayDen.Value = ngayden.ToShortDateString();
                rep.SoThang.Value = sothang;
                #endregion
                #region  tạm bỏ của Dungtt 29/06
                //if (DungChung.Bien.MaBV == "20001")
                //{
                //    string ghichu = bg.Last().GhiChu;
                //    string[] ar = new string[10];
                //    if (!string.IsNullOrEmpty(ghichu))
                //        ar = ghichu.Split(';');

                //    if (ar.Length > 0 && PLoaiThuoc == 0)
                //    {
                //        //rep.date_sudung.Value = "Thuốc sử dụng: " + ar[0];
                //        int index = -1;
                //        index = ar[0].IndexOf("/");
                //        if (index > 1 && ar[0].Length > (index + 8))
                //            rep.NgayTu.Value = ar[0].Substring(index - 2, 10);

                //        index = ar[0].LastIndexOf("/");
                //        if (index > 4 && ar[0].Length > (index + 4))
                //            rep.NgayDen.Value = ar[0].Substring(index - 5, 10);
                //        if (dichvu != null)
                //            rep.SoThang.Value = sothang;
                //    }
                //    else
                //    {
                //        //  rep.date_sudung.Value = "Thuốc sử dụng từ ngày:          đến ngày                       Số ngày:";
                //        rep.lblSoNgay.Text = "Số ngày:";
                //        rep.NgayTu.Value = "";
                //        rep.NgayDen.Value = "";
                //        rep.SoThang.Value = "";
                //    }
                //}
                #endregion

                rep.TongMuc.Value = "Tổng số:  " + dichvu.Count() + " khoản";
                rep.NgayThang.Value = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + "  ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
                rep.DataSource = dichvu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_PhieuLinhThuoc_20001 rep = new BaoCao.rep_PhieuLinhThuoc_20001();


                frmIn frm = new frmIn();
                rep.SoPL.Value = DungChung.Bien.MaBV == "14017" ? _spl + "/2021" : _spl.ToString();

                if (dichvu.Count > 0 && dichvu.First().SoLuong < 0)
                {
                    rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC";
                }
                var bg = (from dt in Data.DThuocs
                          join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                          join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                          join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                          join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          where (dt.MaKP == bnkb.MaKP || dt.MaKP == bnkb.MaKPDTKH)
                          select new { dt.MaKXuat, dt.IDDon_Mau, bnkb.Buong, bnkb.Giuong, kp.TenKP, bn.TenBNhan, bn.Tuoi, bn.DChi, ChanDoan = bnkb.ChanDoan + "; " + bnkb.BenhKhac, bn.MaBNhan, dt.GhiChu, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.DTuong }).ToList(); // dung sửa lấy ghi chú (ngày sử dụng) từ bảng BNKB sang  bảng dthuoc

                //if (DungChung.Bien.MaBV == "14017")
                //{
                //string[] arrListStr1 = bg.First().GhiChu.Split(new char[] { ';' });
                //if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 0)
                //{
                //theongay += "Từ ngày: " + arrListStr[1];
                //rep.SoThang.Value = arrListStr1[0];
                //rep.SoThang14017.Value = arrListStr1[0];
                //rep.ST.Text = arrListStr1[0];
                //}
                //if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 1)
                //{
                //theongay += "Từ ngày: " + arrListStr[1];
                //rep.NgayTu.Value = arrListStr1[1];
                //}
                //if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 2)
                //{
                //theongay += " đến ngày: " + arrListStr[2];
                //rep.NgayDen.Value = arrListStr1[2];
                //}

                //var iddmau = bg.First().IDDon_Mau;
                //if (iddmau != null)
                //{
                //string tdtm = (from kp in Data.DThuocMaus.Where(p => p.IDDonMau == iddmau) select kp.TenDTM).FirstOrDefault();
                //rep.iddonmau.Value = tdtm;
                //}
                //}
                //else
                //{
                if (DungChung.Bien.MaBV == "14017")
                {
                    int sothang = 1;
                    sothang = dichvu.First().SoThang;
                    rep.SoThang.Value = sothang;
                    string[] arrListStr1 = bg.First().GhiChu.Split(new char[] { ';' });
                    if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 1)
                    {
                        //theongay += "Từ ngày: " + arrListStr[1];
                        rep.NgayTu.Value = arrListStr1[1];
                    }
                    if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 2)
                    {
                        //theongay += " đến ngày: " + arrListStr[2];
                        rep.NgayDen.Value = arrListStr1[2];
                    }




                    var iddmau = bg.First().IDDon_Mau;
                    if (iddmau != null)
                    {
                        string tdtm = (from kp in Data.DThuocMaus.Where(p => p.IDDonMau == iddmau) select kp.TenDTM).FirstOrDefault();
                        rep.iddonmau.Value = tdtm;
                    }
                }
                else
                {
                    int sothang = 1;
                    sothang = dichvu.First().SoThang;

                    rep.SoThang.Value = sothang;
                    DateTime ngaytu = dichvu.First().NgayKe.Value.Date;
                    DateTime ngayden = dichvu.First().NgayKe.Value.Date.AddDays(sothang - 1);
                    rep.NgayTu.Value = ngaytu.ToShortDateString();
                    rep.NgayDen.Value = ngayden.ToShortDateString(); 
                }
                

                //}

                int makp = Convert.ToInt32(bg.First().MaKXuat.ToString());
                string noigiao = (from kp in Data.KPhongs.Where(p => p.MaKP == makp) select kp.TenKP).FirstOrDefault();
                if (noigiao != null)
                    rep.NoiGiao.Value = noigiao;

                int mabn = bg.First().MaBNhan;
                var qvv = (from vv in Data.VaoViens.Where(p => p.MaBNhan == mabn) select vv).ToList();
                string ngayvv = qvv.First().NgayVao.ToString();
                rep.SoBA.Value = DungChung.Bien.MaBV == "14017" ? "01D/BV-01" : qvv.First().SoBA;

                if (bg.First().Buong != null)
                {
                    string buong = bg.First().Buong.ToString();
                    string giuong = bg.First().Giuong.ToString();
                    rep.BuongGiuong.Value = DungChung.Bien.MaBV == "14017" ? buong : "Buồng:   " + buong;
                    rep.Giuong.Value = giuong;
                }
                if (bg.First().TenKP != null && bg.First().TenKP.ToString() != "")
                {
                    rep.Khoa.Value = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14017" ? bg.First().TenKP.ToString().ToUpper() : DungChung.Bien.MaBV == "24297" ? bg.First().TenKP.ToString(): "Khoa: " + bg.First().TenKP.ToString();
                }
                if (bg.First().TenBNhan != null && bg.First().TenBNhan.ToString() != "")
                {
                    rep.Hoten.Value = bg.First().TenBNhan.ToUpper();
                }
                rep.SoThe.Value = bg.First().SThe;

                if (bg.First().GTinh == 0)
                    rep.GT.Value = "Nữ";
                else rep.GT.Value = "Nam";
                if (bg.First().HanBHTu != null && bg.First().HanBHDen != null && bg.First().HanBHTu.ToString() != "" && bg.First().HanBHDen.ToString() != "")
                {
                    rep.BHYT.Value = "BHYT giá trị từ: " + bg.First().HanBHTu.ToString().Remove(11) + " đến " + bg.First().HanBHDen.ToString().Remove(11);
                }
                if (bg.First().Tuoi != null && bg.First().Tuoi.ToString() != "")
                { rep.Tuoi.Value = bg.First().Tuoi; }
                if (bg.First().DChi != null && bg.First().DChi.ToString() != "")
                { rep.Diachi.Value = "Địa chỉ: " + bg.First().DChi; }
                string _CD = "Chẩn đoán: ";
                if (bg.First().ChanDoan != null && bg.First().ChanDoan.ToString() != "")
                { rep.ChanDoan.Value = "Chẩn đoán: " + DungChung.Ham.FreshString(bg.First().ChanDoan); }
                else { rep.ChanDoan.Value = _CD; }

                #region cuongtm 29/06/17
                if (PLoaiThuoc == 0)
                {
                }
                else
                {
                    rep.lblSoNgay.Text = "Số ngày:";
                }

                if (bg[0].GhiChu != null)
                {
                    string[] arrListStr = bg[0].GhiChu.Split(new char[] { ';' });
                    rep.ST.Text = arrListStr[0];
                }

                #endregion
                #region  tạm bỏ của Dungtt 29/06
                //if (DungChung.Bien.MaBV == "20001")
                //{
                //    string ghichu = bg.Last().GhiChu;
                //    string[] ar = new string[10];
                //    if (!string.IsNullOrEmpty(ghichu))
                //        ar = ghichu.Split(';');

                //    if (ar.Length > 0 && PLoaiThuoc == 0)
                //    {
                //        //rep.date_sudung.Value = "Thuốc sử dụng: " + ar[0];
                //        int index = -1;
                //        index = ar[0].IndexOf("/");
                //        if (index > 1 && ar[0].Length > (index + 8))
                //            rep.NgayTu.Value = ar[0].Substring(index - 2, 10);

                //        index = ar[0].LastIndexOf("/");
                //        if (index > 4 && ar[0].Length > (index + 4))
                //            rep.NgayDen.Value = ar[0].Substring(index - 5, 10);
                //        if (dichvu != null)
                //            rep.SoThang.Value = sothang;
                //    }
                //    else
                //    {
                //        //  rep.date_sudung.Value = "Thuốc sử dụng từ ngày:          đến ngày                       Số ngày:";
                //        rep.lblSoNgay.Text = "Số ngày:";
                //        rep.NgayTu.Value = "";
                //        rep.NgayDen.Value = "";
                //        rep.SoThang.Value = "";
                //    }
                //}
                #endregion
                rep.celTongSL1.Text = rep.celTongSL2.Text = dichvu.Sum(p => p.SoLuong).ToString();
                rep.txtMaKCB.Text = mabn.ToString();
                rep.DoiTuong.Value = bg.First().DTuong.ToString();
                rep.TongMuc.Value = "Tổng số:      " + dichvu.Count() + "     khoản";
                rep.NgayThang.Value = DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297" ? "Ngày " + Convert.ToDateTime(dichvu.First().NgayKe).Day + " tháng " + Convert.ToDateTime(dichvu.First().NgayKe).Month + " năm " + Convert.ToDateTime(dichvu.First().NgayKe).Year : DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + "  ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
                rep.DataSource = DungChung.Bien.MaBV == "14017" ? dichvu.OrderBy(p => p.TenDV).ToList() : dichvu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }

        }

        public static void _InPhieuLinh_14017(int IDDon, int PLoaiThuoc)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var dichvu = (from dt in Data.DThuocs.Where(p => p.IDDon == IDDon)
                          join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                          join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          group new { dt, dtct, dv } by new { dv.MaTam, dtct.SoPL, bn.NNhap, GhiChu = DungChung.Bien.MaBV == "14017" ? (dtct.GhiChu) : (dt.GhiChu), dtct.MaDV, dv.TenDV, dtct.DonVi, dv.MaQD, DonGia = DungChung.Bien.MaBV == "27021" ? (dtct.DonGia) : 0 } into kq
                          select new
                          {
                              SoPL = kq.Key.SoPL,
                              NgayNhap = kq.Key.NNhap,
                              MaBN = kq.Min(p => p.dt.MaBNhan),
                              NgayKe = kq.Min(p => p.dtct.NgayNhap),
                              Madv = kq.Key.MaDV,
                              kq.Key.MaQD,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              DonGia = kq.Key.DonGia,
                              SoLuongct = kq.Sum(p => p.dtct.SoLuongct),
                              SoLuong = kq.Sum(p => p.dtct.SoLuong),
                              SoLuongTong = kq.Sum(p => p.dtct.SoLuong),
                              //SoThang = DungChung.Bien.MaBV == "14017" ? Convert.ToInt32(kq.Key.GhiChu.Split(';').FirstOrDefault().ToString()) : kq.Max(p => p.dtct.Loai), 
                              SoThang = kq.Max(p => p.dtct.Loai),
                              Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                              GhiChu = kq.Key.GhiChu,
                              MaTam = kq.Key.MaTam,

                          }).OrderBy(p => p.TenDV).ToList();
            if (dichvu.Count <= 0)
                return;

            else
            {
                BaoCao.rep_PhieuLinhThuoc_20001 rep = new BaoCao.rep_PhieuLinhThuoc_20001(); 


                frmIn frm = new frmIn();
                rep.SoPL.Value = DungChung.Bien.MaBV == "14017" ? dichvu.First().SoPL + "/" + dichvu.First().NgayNhap.Value.Year : IDDon.ToString();

                if (dichvu.Count > 0 && dichvu.First().SoLuong < 0)
                {
                    rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC";
                }
                var bg = (from dt in Data.DThuocs.Where(p => p.IDDon == IDDon)
                          join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                          join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                          join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                          where (dt.MaKP == bnkb.MaKP || dt.MaKP == bnkb.MaKPDTKH)
                          select new { dt.MaKXuat, dt.IDDon_Mau, bnkb.Buong, bnkb.Giuong, kp.TenKP, bn.TenBNhan, bn.Tuoi, bn.DChi, ChanDoan = bnkb.ChanDoan + "; " + bnkb.BenhKhac, bn.MaBNhan, dt.GhiChu, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, dt.NgayKe }).ToList(); // dung sửa lấy ghi chú (ngày sử dụng) từ bảng BNKB sang  bảng dthuoc


                if (DungChung.Bien.MaBV == "14017")
                {
                    string[] arrListStr1 = bg.First().GhiChu.Split(new char[] { ';' });
                    if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 1)
                    {
                        //theongay += "Từ ngày: " + arrListStr[1];
                        rep.NgayTu.Value = arrListStr1[1];
                    }
                    if (DungChung.Bien.MaBV == "14017" && arrListStr1.Count() > 2)
                    {
                        //theongay += " đến ngày: " + arrListStr[2];
                        rep.NgayDen.Value = arrListStr1[2];
                    }




                    var iddmau = bg.First().IDDon_Mau;
                    if (iddmau != null)
                    {
                        string tdtm = (from kp in Data.DThuocMaus.Where(p => p.IDDonMau == iddmau) select kp.TenDTM).FirstOrDefault();
                        rep.iddonmau.Value = tdtm;
                    }
                }

                int makp = Convert.ToInt32(bg.First().MaKXuat.ToString());
                string noigiao = (from kp in Data.KPhongs.Where(p => p.MaKP == makp) select kp.TenKP).FirstOrDefault();
                if (noigiao != null)
                    rep.NoiGiao.Value = noigiao;

                int mabn = bg.First().MaBNhan;
                var qvv = (from vv in Data.VaoViens.Where(p => p.MaBNhan == mabn) select vv).ToList();
                string ngayvv = qvv.First().NgayVao.ToString();
                rep.SoBA.Value = DungChung.Bien.MaBV == "14017" ? "01D/BV-01" : qvv.First().SoBA;

                if (bg.First().Buong != null)
                {
                    string buong = bg.First().Buong.ToString();
                    string giuong = bg.First().Giuong.ToString();
                    rep.BuongGiuong.Value = DungChung.Bien.MaBV == "14017" ? buong : "Buồng:   " + buong;
                    rep.Giuong.Value = giuong;
                }
                if (bg.First().TenKP != null && bg.First().TenKP.ToString() != "")
                {
                    rep.Khoa.Value = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14017" ? bg.First().TenKP.ToString().ToUpper() : "Khoa: " + bg.First().TenKP.ToString();
                }

                if (bg.First().TenBNhan != null && bg.First().TenBNhan.ToString() != "")
                {
                    rep.Hoten.Value = bg.First().TenBNhan.ToUpper();
                }
                rep.SoThe.Value = bg.First().SThe;
                if (bg.First().GTinh == 0)
                    rep.GT.Value = "Nữ";
                else rep.GT.Value = "Nam";
                if (bg.First().HanBHTu != null && bg.First().HanBHDen != null && bg.First().HanBHTu.ToString() != "" && bg.First().HanBHDen.ToString() != "")
                {
                    rep.BHYT.Value = "BHYT giá trị từ: " + bg.First().HanBHTu.ToString().Remove(11) + " đến " + bg.First().HanBHDen.ToString().Remove(11);
                }
                if (bg.First().Tuoi != null && bg.First().Tuoi.ToString() != "")
                { rep.Tuoi.Value = bg.First().Tuoi; }
                if (bg.First().DChi != null && bg.First().DChi.ToString() != "")
                { rep.Diachi.Value = "Địa chỉ: " + bg.First().DChi; }
                string _CD = "Chẩn đoán: ";
                if (bg.First().ChanDoan != null && bg.First().ChanDoan.ToString() != "")
                { rep.ChanDoan.Value = "Chẩn đoán: " + DungChung.Ham.FreshString(bg.First().ChanDoan); }
                else { rep.ChanDoan.Value = _CD; }


                #region cuongtm 29/06/17
                int SoThang = 1; //ko hieu vi sao nen de vay
                if (PLoaiThuoc == 0)
                {


                }
                else
                {


                    rep.lblSoNgay.Text = "Số ngày:";
                }
                SoThang = dichvu.First().SoThang;
                DateTime ngaytu = dichvu.First().NgayKe.Value.Date;
                DateTime ngayden = dichvu.First().NgayKe.Value.Date.AddDays(SoThang - 1);

                if (bg[0].GhiChu != null)
                {
                    string[] arrListStr = bg[0].GhiChu.Split(new char[] { ';' });
                    rep.ST.Text = arrListStr[0];
                }
                #endregion
                #region  tạm bỏ của Dungtt 29/06
                //if (DungChung.Bien.MaBV == "20001")
                //{
                //    string ghichu = bg.Last().GhiChu;
                //    string[] ar = new string[10];
                //    if (!string.IsNullOrEmpty(ghichu))
                //        ar = ghichu.Split(';');

                //    if (ar.Length > 0 && PLoaiThuoc == 0)
                //    {
                //        //rep.date_sudung.Value = "Thuốc sử dụng: " + ar[0];
                //        int index = -1;
                //        index = ar[0].IndexOf("/");
                //        if (index > 1 && ar[0].Length > (index + 8))
                //            rep.NgayTu.Value = ar[0].Substring(index - 2, 10);

                //        index = ar[0].LastIndexOf("/");
                //        if (index > 4 && ar[0].Length > (index + 4))
                //            rep.NgayDen.Value = ar[0].Substring(index - 5, 10);
                //        if (dichvu != null)
                //            rep.SoThang.Value = sothang;
                //    }
                //    else
                //    {
                //        //  rep.date_sudung.Value = "Thuốc sử dụng từ ngày:          đến ngày                       Số ngày:";
                //        rep.lblSoNgay.Text = "Số ngày:";
                //        rep.NgayTu.Value = "";
                //        rep.NgayDen.Value = "";
                //        rep.SoThang.Value = "";
                //    }
                //}
                #endregion
                rep.TongTien.Value = dichvu.Sum(p => p.Thanhtien).ToString();
                rep.TongMuc.Value = "Tổng số:      " + dichvu.Count() + "     khoản";
                rep.NgayThang.Value = DungChung.Bien.MaBV == "14017" ? DungChung.Ham.NgaySangChu(Convert.ToDateTime(bg.First().NgayKe)) : DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + "  ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
                rep.DataSource = DungChung.Bien.MaBV == "14017" ? dichvu.OrderBy(p => p.TenDV).ToList() : dichvu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }

        }
        public static void _InPhieuThuocDY_20001(int _spl)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dichvu = (from dt in Data.DThuocs
                          join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                          join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                          group new { dtct, dv } by new { dtct.MaDV, dv.TenDV, dtct.DonVi } into kq
                          select new
                          {
                              //NgayKe = kq.Key.Day,
                              Madv = kq.Key.MaDV,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              SoLuong = kq.Sum(p => p.dtct.SoLuongct),
                              SoLuongTong = kq.Sum(p => p.dtct.SoLuong),
                              SoThang = kq.Sum(p => p.dtct.Loai),
                              Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                          }).OrderBy(p => p.TenDV).ToList();


            BaoCao.Rep_PLThuoctheothang_20001 rep = new BaoCao.Rep_PLThuoctheothang_20001();
            frmIn frm = new frmIn();
            rep.SoPL.Value = _spl;
            if (dichvu.Count > 0 && dichvu.First().SoLuong < 0)
            {
                rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC THANG";
            }
            var bg = (from dt in Data.DThuocs
                      join dtct in Data.DThuoccts.Where(p => p.SoPL == _spl) on dt.IDDon equals dtct.IDDon
                      join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                      join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                      join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                      where (dt.MaKP == bnkb.MaKP)
                      select new { bnkb.Buong, bnkb.Giuong, kp.TenKP, bn.TenBNhan, bn.Tuoi, bn.DChi, bnkb.ChanDoan, bn.MaBNhan, bnkb.GhiChu, bnkb.MaICD, bnkb.BenhKhac }).ToList();

            if (bg.First().Buong != null && bg.First().Buong.ToString() != "")
            {
                string buong = bg.First().Buong.ToString();
                if (bg.First().Giuong != null && bg.First().Giuong.ToString() != "")
                {
                    buong += "  Giường: " + bg.First().Giuong.ToString();
                }
                rep.BuongGiuong.Value = "Buồng: " + buong;
            }
            if (bg.First().TenKP != null && bg.First().TenKP.ToString() != "")
            {
                rep.Khoa.Value = bg.First().TenKP.ToString();
            }
            if (bg.First().TenBNhan != null && bg.First().TenBNhan.ToString() != "")
            {
                if (DungChung.Bien.MaBV == "30012")
                    rep.Hoten.Value = "Họ và tên người bệnh: " + bg.First().TenBNhan;
                else
                    rep.Hoten.Value = "Họ tên bệnh nhân: " + bg.First().TenBNhan;
            }

            if (DungChung.Bien.MaBV == "27021" && bg.First().MaBNhan != null)
            {
                int mabn = bg.First().MaBNhan;
                var qvv = (from vv in Data.VaoViens.Where(p => p.MaBNhan == mabn) select vv).FirstOrDefault();
                if (qvv != null)
                    rep.SoBA.Value = "Số V.V: " + qvv.SoVV;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                string ghichu = bg.Last().GhiChu;
                if (ghichu != null)
                {
                    string[] ar = ghichu.Split(';');

                    if (ar.Length > 0)
                        rep.NgayUongThuoc.Value = "Sử dụng: " + ar[0];
                }
            }
            if (bg.First().Tuoi != null && bg.First().Tuoi.ToString() != "")
            { rep.Tuoi.Value = "Tuổi: " + bg.First().Tuoi; }
            if (bg.First().DChi != null && bg.First().DChi.ToString() != "")
            { rep.Diachi.Value = "Địa chỉ: " + bg.First().DChi; }
            string _CD = "Chẩn đoán: ";
            if (bg.First().ChanDoan != null && bg.First().ChanDoan.ToString() != "")
            {
                if (DungChung.Bien.MaBV == "20001")
                    rep.Chandoan.Value = "Chẩn đoán: " + DungChung.Ham.GetICDstr(bg.First().ChanDoan + "; " + bg.First().BenhKhac);
                else
                    rep.Chandoan.Value = "Chẩn đoán: " + bg.First().ChanDoan;
            }
            else { rep.Chandoan.Value = _CD; }
            rep.Ngaythang.Value = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
            rep.DataSource = dichvu;
            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }


        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
        }

        private void lupNgay1_EditValueChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
        }

        private void lupNgay2_EditValueChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
        }

        private void cboLoaiDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
        }
        bool _tutruc = false;
        List<int> dsBnhan = new List<int>();
        bool nhaThuoc = false;
        private void lupKhoD_EditValueChanged(object sender, EventArgs e)
        {
            nhaThuoc = false;
            dsBnhan = new List<int>();
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
            int makho = 0;
            if (lupKhoD.EditValue != null)
                makho = Convert.ToInt32(lupKhoD.EditValue);

            var kho = data.KPhongs.FirstOrDefault(o => o.MaKP == makho);
            if (kho != null && (kho.TenKP.Contains("nhà thuốc") || kho.TenKP.Contains("Nhà thuốc")))
            {
                nhaThuoc = true;
            }

            var qtutruc = data.KPhongs.Where(p => p.MaKP == makho && p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).FirstOrDefault();
            if (qtutruc != null)
                _tutruc = true;
            else
                _tutruc = false;
        }

        private void DanhSachBN(List<BenhNhanADO> data)
        {
            if (data != null && data.Count > 0)
            {
                dsBnhan = data.Select(o => o.MaBNhan).ToList();
            }
        }

        private void radNNT_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
            if (chkNoThe.Checked)
            {
                TenBN();
            }
        }

        private void cboDTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkLinhThang_CheckedChanged(sender, e);
        }

        private void chkNoThe_CheckedChanged(object sender, EventArgs e)
        {

            TenBN();

        }
        private void grvPhieulinh_BeforePrintRow(object sender, DevExpress.XtraGrid.Views.Printing.CancelPrintRowEventArgs e)
        {
        }

        private class ChonKhoXuat
        {
            public bool Chon { set; get; }
            public string Loai { set; get; }
            public int? SoPL { set; get; }
            public int? MaKP { set; get; }

            public int IDDonct { get; set; }

            public int IDDon { get; set; }

            public int MaDV { get; set; }

            public string TenRG { get; set; }

            public double SoLuong { get; set; }

            public string TenDV { get; set; }

            public double DonGia { get; set; }

            public string SoLo { get; set; }

            public string DonVi { get; set; }

            public string DuongD { get; set; }

            public string DviUong { get; set; }

            public DateTime? HanDung { get; set; }

            public byte LoaiDV { get; set; }

            public double SoLuongct { get; set; }

            public double ThanhTien { get; set; }

            public int IDDTBN { get; set; }

            public int? MaBNhan { get; set; }
        }

        private void frmPhieulinh_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void grv_ChonKhoXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colChonKhoXuat":

                    int row = grv_ChonKhoXuat.FocusedRowHandle;
                    if (grv_ChonKhoXuat.GetRowCellDisplayText(row, colChonKhoXuat) != null && grv_ChonKhoXuat.GetRowCellDisplayText(row, colChonKhoXuat) != "")
                    {
                        string tenRG = "";
                        int makp = Convert.ToInt32(grv_ChonKhoXuat.GetRowCellValue(row, colChonKhoXuat));
                        if (grv_ChonKhoXuat.GetRowCellDisplayText(row, colLoai) != null && grv_ChonKhoXuat.GetRowCellDisplayText(row, colLoai) != "")
                            tenRG = grv_ChonKhoXuat.GetRowCellDisplayText(row, colLoai).ToString();
                        var qcheckton = (from a in qtn.Where(p => p.TenRG == tenRG)
                                         group a by new { a.MaDV, a.TenDV, a.DonGia, a.SoLo } into kq
                                         select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.DonGia, kq.Key.SoLo, SoLuong = kq.Sum(p => p.SoLuong) }).ToList();
                        string ms = "";
                        foreach (var b in qcheckton)
                        {
                            double ton = 0;
                            ton = DungChung.Ham._checkTon_KD(data, b.MaDV, makp, b.DonGia, 0, b.SoLo);
                            if (ton - b.SoLuong < 0)
                            {
                                ms += b.TenDV + ", ";
                            }
                        }
                        if (ms != "")
                        {

                            MessageBox.Show("Các thuốc : " + ms + " Có số lượng tồn không đủ");
                        }

                    }
                    break;
            }
        }

        List<int> _maDVHet = new List<int>(); //mã dịch vụ không còn tồn
        private bool ktraTon()
        {
            _maDVHet = new List<int>();

            string ms = "";
            for (int i = 0; i < grv_ChonKhoXuat.RowCount; i++)
            {
                int row = grv_ChonKhoXuat.FocusedRowHandle;
                if (grv_ChonKhoXuat.GetRowCellDisplayText(i, colChonKhoXuat) == null || grv_ChonKhoXuat.GetRowCellDisplayText(i, colChonKhoXuat) == "")
                {
                    MessageBox.Show("Bạn chưa chọn đầy đủ kho xuất");
                    return false;
                }
                else
                {
                    string tenRG = "";
                    int makp = Convert.ToInt32(grv_ChonKhoXuat.GetRowCellValue(i, colChonKhoXuat));
                    if (grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai) != null && grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai) != "")
                        tenRG = grv_ChonKhoXuat.GetRowCellDisplayText(i, colLoai).ToString();
                    var qcheckton = (from a in qtn.Where(p => p.TenRG == tenRG)
                                     group a by new { a.MaDV, a.TenDV, a.DonGia, a.SoLo } into kq
                                     select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.DonGia, kq.Key.SoLo, SoLuong = kq.Sum(p => p.SoLuong) }).ToList();

                    foreach (var b in qcheckton)
                    {
                        double ton = 0;
                        ton = DungChung.Ham._checkTon_KD(data, b.MaDV, makp, b.DonGia, 0, b.SoLo);
                        if (ton - b.SoLuong < 0)
                        {
                            _maDVHet.Add(b.MaDV);
                            ms += b.TenDV + ", ";
                        }
                    }

                }

            }
            if (ms != "")
            {

                MessageBox.Show("Các thuốc : " + ms + " Có số lượng tồn không đủ");
            }
            return true;
        }


    }
}
