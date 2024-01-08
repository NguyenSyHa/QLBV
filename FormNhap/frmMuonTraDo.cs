using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frmMuonTraDo : DevExpress.XtraEditors.XtraForm
    {
        int mabn;

        List<DThuoc> _ldthuoc = new List<DThuoc>();
        List<DThuocct> _ldthuocct = new List<DThuocct>();
        DateTime _dttu = new DateTime();
        DateTime _dtden = new DateTime();
        int _makp = 0;
        int StatusDT = 1;
        int SoPL = -1;
        int _selIndex = -1;
        int ppxuat = -1;
        static double tonthuoc = 0;
        static double soluongt = 0;// số lượng một loại thuốc được kê trên cùng 1 đơn thuốc
        double _TT = 0;
        int iddon = 0;
        int StatusTra;
       // int rowhandle = 0;
        public frmMuonTraDo()
        {
            InitializeComponent();
        }
       
        private void EnableControl(bool status)
        {
            txtTiencoc.Properties.ReadOnly = !status;
            dtNgayKe.Properties.ReadOnly = !status;
            //lupBPKe.Properties.ReadOnly = !status;
            lupKhoXuat.Properties.ReadOnly = !status;
            lupNguoiKe.Properties.ReadOnly = !status;
            // cboNhomDuoc.Properties.ReadOnly = !status;
            grvDonThuocct.OptionsBehavior.Editable = status;
            grcDonThuocdt.Enabled = !status;
            cboKieuPL.Properties.ReadOnly = !status;
            grcBNhankb.Enabled = !status;
            groupControl3.Enabled = !status;
        }
        int TrangThai;
        List<KPhong> _lKphongall = new List<KPhong>();

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frmMuonTraDo_Load(object sender, EventArgs e)
        {
            dtTimDenNgay.DateTime = DateTime.Now;
            dtTimTuNgay.DateTime = DateTime.Now;
            _lkp = new List<KPhong>();

            _lKphongall = (from kps in _data.KPhongs.Where(p => p.Status == 1)

                           select kps).OrderBy(p => p.TenKP).ToList();
            var kpkhamdt = _lKphongall.Where(p => p.PLoai == ("Lâm sàng") || (p.PLoai == ("Phòng khám"))).ToList();
            _lkp.AddRange(kpkhamdt);
            _lkp.Add(new KPhong { TenKP = "", MaKP = 0, ChuyenKhoa = "", PLoai = "Lâm sàng" });
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var a2 = (from dt in _data.DThuocs.Where(p => p.KieuDon == 2).Where(p => p.MaBNhan == null)
                      join dtct in _data.DThuoccts.Where(p => p.SoLuong < 0) on dt.IDDon equals dtct.IDDon
                      select dt).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimMaKP.Properties.DataSource = kpkhamdt;
               // lupKhoaKhamkb.Properties.DataSource = kpkhamdt;
            }
            else
            {

                kpkhamdt = (from a in kpkhamdt
                            join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                            select a).ToList();
                lupTimMaKP.Properties.DataSource = kpkhamdt;
               // lupKhoaKhamkb.Properties.DataSource = kpkhamdt;
            }
            if (a2.Count > 0)
            {
                foreach (var b in a2)
                {
                    var sua = _data.DThuocs.Single(p => p.IDDon == b.IDDon);
                    sua.KieuDon = 4;
                    _data.SaveChanges();
                }
            }
            Loadgrid();

            Enablebutton(true);
            EnableControl(false);
            var kp = (from kphong in _data.KPhongs
                      where (kphong.Status == 1 && (kphong.PLoai.ToLower() == "lâm sàng" || kphong.PLoai.ToLower() == "phòng khám" || kphong.PLoai.ToLower() == "khoa dược" || kphong.PLoai.ToLower() == "cận lâm sàng" || kphong.PLoai.ToLower() == "tủ trực"))
                      select new { kphong.MaKP, kphong.TenKP, kphong.PLoai }).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
               // lupBPKe.Properties.DataSource = kp;

            }
            else
            {
                var kptheotk = (from k in kp
                                join p in DungChung.Bien.listKPHoatDong on k.MaKP equals p
                                select k).ToList();
              //  lupBPKe.Properties.DataSource = kptheotk;

            }
            lupKhoXuat.Properties.DataSource = kp.Where(p => p.PLoai.ToLower().Contains("khoa dược"));
            TrangThai = 0;

            binSDonThuocct.DataSource = _ldthuocct;
            grcDonThuocct.DataSource = binSDonThuocct;

            var canbo = (from cb in _data.CanBoes.Where(p => p.Status == 1) select new { cb.MaCB, cb.TenCB }).ToList();
            lupNguoiKe.Properties.DataSource = canbo;
            lupNguoiKe.Properties.DisplayMember = "TenCB";
            lupNguoiKe.Properties.ValueMember = "MaCB";

        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            btnKLuu.Enabled = !Status;
            groupControl2.Enabled = Status;
        }
        private bool KtraLuu()
        {
            if (dtNgayKe.EditValue == null || dtNgayKe.EditValue.ToString() == "")
            {
                MessageBox.Show("Ngày mượn không hợp lệ!");
                dtNgayKe.Focus();
                return false;
            }
            //if (lupBPKe.EditValue == null || string.IsNullOrEmpty(lupBPKe.Text))
            //{
            //    MessageBox.Show("Bộ phận kê không hợp lệ");
            //    lupBPKe.Focus();
            //    return false;
            //}
            if (lupKhoXuat.EditValue == null)
            {
                MessageBox.Show("Kho xuất không hợp lệ");
                lupKhoXuat.Focus();
                return false;
            }
            if (grvDonThuocct.GetRowCellValue(0, colMaDVdt) == null)
            {
                MessageBox.Show("Bạn chưa chọn đồ");
                lupKhoXuat.Focus();
                return false;
            }
            if (txtTiencoc.Text=="")
            {
                MessageBox.Show("Bạn chưa nhập tiền cọc");
                txtTiencoc.Focus();
                return false;
            }  
            for (int i=0;i<grvDonThuocct.DataRowCount;i++)
            {
                if (Convert.ToInt32(grvDonThuocct.GetRowCellValue(i,colSoLuong))==0)
                {
                    MessageBox.Show("Số lượng đồ dùng phải khác 0");
                    return false;
                }    

            }    
           

            return true;
        }



        private void cboKieuPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKieuPL.SelectedIndex == 0)
            {
                dtNgayTra.Visible = false;
                labelControl2.Visible = false;
                dtNgayKe.Visible = true;
                labelControl6.Visible = true;
                txtTiencoc.Enabled = true;
                // labelControl7.Visible = true;
                //  txtTiencoc.Visible = true;
                //txtTiencoc.Text = "";

            }
            else if(cboKieuPL.SelectedIndex == 1)
            {
                dtNgayKe.Visible = false;
                labelControl6.Visible = false;
                labelControl2.Visible = true;
                dtNgayTra.Visible = true;
                txtTiencoc.Enabled = false;
                dtNgayTra.DateTime = DateTime.Now;
                txtTiencoc.Text = "0";
                // labelControl7.Visible = false;
                // txtTiencoc.Visible = false;
                // var tiencoc = (from dt in _data.DThuocs.Where(p=>p.PLDV ==3 && p.MaBNhan == mabn && p.Status !=1) select new {dt.TienCoc})
            }

            lupKhoXuat_EditValueChanged(sender, e);

        }
        bool BothuocKoSD = false;

        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            if (lupKhoXuat.EditValue != null)
                makho = Convert.ToInt32(lupKhoXuat.EditValue);
            var kp = _data.KPhongs.Where(p => p.MaKP == makho).ToList();
            if (kp.Count > 0 && kp.First().PPXuat == 3)
            {
                colDonGia.OptionsColumn.ReadOnly = true;
                ppxuat = 3;
            }
            else
            {
                colSoLo.OptionsColumn.ReadOnly = true;
                ppxuat = -1;
            }

            if (lupTimMaKP.EditValue != null)
            {
                _makp = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                string makhoake = ";" + _makp.ToString() + ";";

                var dvu = (from tenduoc in _data.DichVus.Where(p => BothuocKoSD == true ? p.Status == 1 : true).Where(p => p.MaKPsd.Contains(makhoake))
                           join tn in _data.TieuNhomDVs
                           on tenduoc.IdTieuNhom equals tn.IdTieuNhom
                           select tenduoc).ToList();
                if (cboKieuPL.SelectedIndex == 1)
                {
                    
                    var domuon = (from dtct in _data.DThuoccts
                                  join dt in _data.DThuocs.Where(p => p.PLDV == 3 && p.MaBNhan == mabn && p.KieuDon == 0) on dtct.IDDon equals dt.IDDon
                                  join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                  select new { dtct.MaDV, dv.TenDV, dv.HamLuong, dtct.SoLo, dtct.HanDung }).ToList();
                    var result = domuon.GroupBy(p => p.MaDV)
                   .Select(grp => grp.First())
                   .ToList();
                    lupMaDuocdt.DataSource = result;

                }

                else
                {
                    var duoc2 = (from nhapduoc in _data.NhapDcts
                                 join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                                 select new { nhapduoc.MaDV, nhapduoc.DonGia, nhapduoc.DonVi, nduoc.MaKP, nhapduoc.SoLuongN, nhapduoc.SoLo, nhapduoc.HanDung }).ToList();
                    if (ppxuat == 3)
                    {
                        var duoc = (from tenduoc in duoc2
                                    join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
                                    group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong, tenduoc.SoLo, tenduoc.HanDung } into kq
                                    select new { TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP, kq.Key.HamLuong, kq.Key.SoLo, kq.Key.HanDung }
                                ).OrderBy(p => p.TenDV).ToList();
                        List<DV> _lDV = new List<DV>();

                        if (DungChung.Bien.MaBV == "24012")
                        {
                            int kho = 0;
                            kho = lupKhoXuat.EditValue != null ? Convert.ToInt32(lupKhoXuat.EditValue) : 0;
                            foreach (var item in duoc)
                            {
                                int madv = item.MaDV ?? 0;
                                // List<DungChung.Ham.giaSoLoHSD> dsgia = new List<DungChung.Ham.giaSoLoHSD>();
                                //  dsgia = DungChung.Ham._getDSGia(_data, madv, kho, true);
                                //  if (DungChung.Bien.SoLuongTon > 0)
                                // {
                                DV a = new DV();
                                a.MaDV = item.MaDV;
                                a.TenDV = item.TenDV;
                                a.HamLuong = item.HamLuong;
                                a.SoLo = item.SoLo;
                                a.HanDung = item.HanDung;
                                _lDV.Add(a);
                                // }
                            }
                            lupMaDuocdt.DataSource = _lDV;
                        }
                        else
                        {
                            lupMaDuocdt.DataSource = duoc.ToList();
                        }
                    }
                    else
                    {
                        var duoc = (from tenduoc in duoc2
                                    join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
                                    group new { tenduoc, nduoc } by new { nduoc.TenRG, tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, nduoc.HamLuong } into kq
                                    select new { TenDV = (DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009") ? kq.Key.TenRG : kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP, kq.Key.HamLuong }
                                ).OrderBy(p => p.TenDV).ToList();
                        List<DV> _lDV = new List<DV>();
                        lupMaDuocdt.DataSource = duoc.ToList();
                    }
                    if (lupTimMaKP.EditValue != null)
                    {
                        _makp = Convert.ToInt32(lupTimMaKP.EditValue);
                    }
                    if (lupKhoXuat.EditValue != null)
                    {
                        makho = Convert.ToInt32(lupKhoXuat.EditValue);
                    }
                    var duoc1 = (from tenduoc in duoc2
                                 join nduoc in dvu on tenduoc.MaDV equals nduoc.MaDV
                                 group new { tenduoc, nduoc } by new { tenduoc.MaKP, nduoc.TenDV, tenduoc.MaDV, nduoc.DonVi, tenduoc.DonGia } into kq
                                 select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.tenduoc.SoLuongN), kq.Key.DonGia }
                             ).OrderBy(p => p.TenDV).ToList();
                }
            }
        }

        private void grvDonThuocdt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null && grvDonThuocdt.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                _ldthuoc = DataContext.DThuocs.Where(p => p.IDDon == iddon).ToList();
                _ldthuocct = DataContext.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                if (_ldthuoc.Count > 0)
                {
                   // lupBPKe.EditValue = _ldthuoc.First().MaKP;
                    lupKhoXuat.EditValue = _ldthuoc.First().MaKXuat;
                    lupNguoiKe.EditValue = _ldthuoc.First().MaCB;
                    txtTiencoc.Text = _ldthuoc.First().TienCoc.ToString();
                    if (_ldthuoc.First().NgayTra != null)
                        dtNgayTra.DateTime = _ldthuoc.First().NgayTra.Value;
                    if (_ldthuoc.First().NgayKe != null)
                        dtNgayKe.DateTime = _ldthuoc.First().NgayKe.Value;
                    if (_ldthuoc.First().KieuDon == 0)
                    { cboKieuPL.SelectedIndex = 0; }
                    else
                    { cboKieuPL.SelectedIndex = 1; }
                    var sp = (from d in _ldthuocct select new { d.SoPL }).ToList();
                    if (sp.Count > 0 && sp.First().SoPL != null && sp.First().SoPL.ToString() != "")
                    {
                        SoPL = sp.First().SoPL;
                    }
                    else
                        SoPL = -1;
                    
                }
                binSDonThuocct.DataSource = _ldthuocct.ToList();
                grcDonThuocct.DataSource = binSDonThuocct;
            }
            else
            {
                iddon = 0;
                StatusDT = 1;
                SoPL = -1;
                StatusDT = -1;
                grcDonThuocct.DataSource = "";
            }
        }

        private void grvDonThuocct_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (e.Column.Name == "colXoactdt")
            {
                if (TrangThai == 1)
                {
                    grvDonThuocct.DeleteSelectedRows();
                }
                if (TrangThai == 2)
                {
                    int idct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                    if (idct > 0)
                    {
                        var xoa = _data.DThuoccts.Single(p => p.IDDonct == idct);


                        if (xoa.Status == 0)
                        {
                            if (xoa.SoPL > 0)
                            {
                                MessageBox.Show("Không thể xoá!");
                            }
                            else
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa đồ : " + grvDonThuocct.GetFocusedRowCellDisplayText(colMaDVdt).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    _data.DThuoccts.Remove(xoa);
                                    _data.SaveChanges();
                                    grvDonThuocct.DeleteSelectedRows();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Đồ dùng đã hủy hoặc đã xuất, bạn không thể xóa");
                        }
                    }

                }
            }
        }

        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DungChung.Ham.giaSoLoHSD> dsgia = new List<DungChung.Ham.giaSoLoHSD>();
            DungChung.Ham.giaSoLoHSD dsgianew = new DungChung.Ham.giaSoLoHSD();
            string _solo = "";
            int makho = 0;
            int madv = 0;
            int maKhoKe = 0;
            int _linh = 0;
            double soluongsua = 0;
            if (lupTimMaKP.EditValue != null)
                maKhoKe = Convert.ToInt32(lupTimMaKP.EditValue);
            int _mak = 0;
            if (lupKhoXuat.EditValue != null)
            {
                _mak = Convert.ToInt32(lupKhoXuat.EditValue);
            }
            if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null)
                madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));

            var qtt = _data.KPhongs.Where(parameters => parameters.MaKP == maKhoKe && parameters.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            var qdtct = _data.DThuoccts.Where(p => p.IDDon == iddon).Where(p => p.MaDV == madv).ToList();
            if (qdtct.Count > 0)
                soluongsua = qdtct.Sum(p => p.SoLuong);
            int ppxuat = 0;
            var kp = _data.KPhongs.Where(p => p.MaKP == _mak).Select(p => p.PPXuat).ToList();
            if (kp.Count > 0 && kp.First() != null)
                ppxuat = kp.First().Value;
            switch (e.Column.Name)
            {
                case "colMaDVdt":
                    #region colMaDVdt
                    if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null)
                    {
                        cboDonGia.Items.Clear();
                        if (cboKieuPL.SelectedIndex == 0)
                        {

                            madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                            grvDonThuocct.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));

                            grvDonThuocct.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                            soluongt = 0;

                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                    {
                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                        {

                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                        }
                                    }
                            }
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                            grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");
                            int thuocTrung = 0;

                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                    {
                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                        {
                                            if (i != e.RowHandle)
                                                thuocTrung++;
                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                            //DungChung.Bien.SoLuongTon = 0;
                                            {
                                                if (grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() != "")
                                                    _linh = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colStatusct));
                                                else
                                                    _linh = 0;
                                                if (_linh == 0)
                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                            }
                                        }
                                    }
                            }
                            if (thuocTrung > 0)
                            {
                                MessageBox.Show("Đồ đã được kê " + thuocTrung + " lần");
                                grvDonThuocct.DeleteRow(e.RowHandle);
                                //grvDonThuocct.SetFocusedRowCellValue("colMaDVdt", e.RowHandle);
                            }    
                               
                            if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null)
                                _solo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
                            dsgia = DungChung.Ham._getDSGia(_data, madv, _mak, true);

                            if (dsgia.Count > 0)
                            {
                                grvDonThuocct.SetFocusedRowCellValue(colSoLo, dsgia.First().SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, dsgia.First().Gia);
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    grvDonThuocct.SetFocusedRowCellValue(colHanDung, dsgia.First().HanDung);
                                }
                                tonthuoc = DungChung.Bien.SoLuongTon;
                                grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc - soluongt).ToString();
                            }
                            else
                            {
                                grpDThuocct.Text = "Số lượng tồn: 0 ";
                            }

                        }
                        else
                        {
                            madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                            _makp = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                            int thuocTrung =0;
                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                    {
                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                        {
                                            if (i != e.RowHandle)
                                                thuocTrung++;
                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                            //DungChung.Bien.SoLuongTon = 0;
                                            {
                                                if (grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() != "")
                                                    _linh = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colStatusct));
                                                else
                                                    _linh = 0;
                                                if (_linh == 0)
                                                    soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                            }
                                        }
                                    }
                            }
                            if (thuocTrung > 0)
                            {
                                MessageBox.Show("Đồ đã được kê " + thuocTrung + " lần");
                                grvDonThuocct.DeleteRow(e.RowHandle);
                                return;
                            }
                            makho = Convert.ToInt32(lupKhoXuat.EditValue);

                            var duoc1 = (from nd in _data.NhapDs.Where(p => p.MaKP == makho).Where(p => p.MaKPnx == _makp).Where(p => (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 2 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7 || p.KieuDon == 11)) || (p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 2 || p.TraDuoc_KieuDon == 6 || p.TraDuoc_KieuDon == 5 || p.TraDuoc_KieuDon == 7 || p.TraDuoc_KieuDon == 11)))
                                         join ndct in _data.NhapDcts.Where(p => p.MaDV == madv) on nd.IDNhap equals ndct.IDNhap
                                         join tenduoc in _data.DichVus on ndct.MaDV equals tenduoc.MaDV
                                         group new { nd, ndct, tenduoc } by new { tenduoc.TenDV, tenduoc.MaDV, ndct.DonGia, tenduoc.DonVi, ndct.SoLo, ndct.HanDung } into kq
                                         select new { kq.Key.TenDV, kq.Key.SoLo, kq.Key.MaDV, kq.Key.DonVi, SL = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongN), kq.Key.DonGia, kq.Key.HanDung }
                                   ).OrderBy(p => p.TenDV).Where(p => p.SL > 0).ToList();
                            var duoc = (from d in duoc1
                                        group new { d } by new { d.TenDV, d.MaDV, d.DonVi, d.DonGia, d.SoLo, d.SL } into k
                                        select new
                                        {
                                            k.Key.TenDV,
                                            k.Key.SoLo,
                                            k.Key.SL,
                                            k.Key.DonVi,
                                            k.Key.DonGia,
                                            k.Key.MaDV,
                                        }).ToList();
                            var dvm = (from dv in _data.DichVus.Where(p => p.MaDV == madv) select new { dv.DonVi }).ToList();
                            // string Dv = duoc.Where(p => p.MaDV == madv).FirstOrDefault().DonVi;
                            string Dv = dvm.FirstOrDefault().DonVi;
                            grvDonThuocct.SetFocusedRowCellValue(colDonVi, Dv);
                            cboDonGia.Items.Clear();
                            grvDonThuocct.SetFocusedRowCellValue(colDonGia, "0");
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                            grvDonThuocct.SetFocusedRowCellValue(colThanhTien, "0");


                            foreach (var a in duoc)
                            {
                                cboDonGia.Items.Add(a.DonGia);
                                if (!string.IsNullOrEmpty(a.SoLo))
                                    cboSoLo.Items.Add(a.SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLo, a.SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, a.DonGia);
                            }
                          /*  if (DungChung.Bien.MaBV == "24012")
                            {
                                string solo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
                                double dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                                var date = duoc1.Where(p => p.DonGia == dongia).Where(p => p.SoLo == solo).Where(p => !string.IsNullOrEmpty(p.HanDung.ToString())).Select(p => p.HanDung).ToList();
                                if (date.Count > 0)
                                {
                                    grvDonThuocct.SetFocusedRowCellValue(colHanDung, Convert.ToDateTime(date.First()));
                                }
                            }
                          */
                        }

                    }
                    break;
                #endregion

                case "colSoLuong":
                    #region lĩnh dược
                    
                    
                    if (cboKieuPL.SelectedIndex == 0)
                    {
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                        {

                            double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
                            if (a != 0)
                            {
                                switch (TrangThai)
                                {
                                    case 1: // khi tao don moi
                                        if (a < 0)
                                        {
                                            MessageBox.Show("Số lượng phải >0");
                                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                        }
                                        else if (a > 0)
                                        {

                                            dsgianew = DungChung.Ham._getGia(_data, madv, _mak);
                                            double soluongt = 0;
                                            double dongia = 0;
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                            {
                                                dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                            }
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                            {
                                                _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                            }

                                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                            {
                                                if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                    {
                                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                        {
                                                            double dongiact = 0;
                                                            if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                            {
                                                                dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                            }
                                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                        }
                                                    }
                                            }

                                            int Madv = int.Parse(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt).ToString());
                                            var tonkho = (from nd in _data.NhapDs.Where(p => p.PLoai == 1)
                                                          join ndct in _data.NhapDcts.Where(p => p.MaDV == Madv) on nd.IDNhap equals ndct.IDNhap
                                                          select new { ndct.SoLuongN }).ToList();
                                            var tongkho = tonkho.Sum(p => p.SoLuongN);
                                            var SLmuon = (from dt in _data.DThuocs.Where(p => p.Status != 1)
                                                          join dtct in _data.DThuoccts.Where(p => p.MaDV == Madv) on dt.IDDon equals dtct.IDDon
                                                          select new { dtct.SoLuong }).ToList();
                                            var SumMuon = SLmuon.Sum(p => p.SoLuong);
                                            tonthuoc = tongkho - SumMuon - a;

                                            if (tonthuoc >= 0)
                                            {

                                                if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                {
                                                    double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                    grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                }
                                                grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Số lượng trong kho không đủ");
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                //DungChung.Bien.SoLuongTon = 0;
                                                grpDThuocct.Text = "Số   tồn: " + (tonthuoc - a).ToString();
                                            }
                                        }
                                        break;
                                    case 2:// khi sua don
                                        //soluongt = a;
                                        if (a < 0)
                                        {
                                            MessageBox.Show("Số lượng phải >0");
                                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                        }
                                        else if (a > 0)
                                        {

                                            dsgianew = DungChung.Ham._getGia(_data, madv, _mak);

                                            // tonthuoc = DungChung.Bien.SoLuongTon;
                                            double soluongt = 0;
                                            double dongia = 0;
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia) != null)
                                            {
                                                dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colDonGia));
                                            }
                                            if (grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo) != null)
                                            {
                                                _solo = Convert.ToString(grvDonThuocct.GetRowCellValue(grvDonThuocct.FocusedRowHandle, colSoLo));
                                            }
                                            for (int i = 0; i <= grvDonThuocct.RowCount; i++)
                                            {
                                                if (i != grvDonThuocct.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                                                    {
                                                        if (madv == Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt)))
                                                        {
                                                            double dongiact = 0;
                                                            if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null)
                                                            {
                                                                dongiact = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                            }
                                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0)
                                                                soluongt += Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                                        }
                                                    }
                                            }
                                            if (ppxuat == 3)
                                            {
                                                if (_solo == dsgianew.SoLo)
                                                    tonthuoc = dsgianew.SoLuong + soluongsua - soluongt - a;
                                                else
                                                    tonthuoc = 0 + soluongsua - soluongt - a;
                                            }
                                            else
                                                tonthuoc = DungChung.Bien.SoLuongTon + soluongsua - soluongt - a;
                                            //tonthuoc = DungChung.Bien.SoLuongTon - soluongt;
                                            if (tonthuoc >= 0)
                                            {

                                                if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                                {
                                                    double b = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString());
                                                    grvDonThuocct.SetFocusedRowCellValue(colThanhTien, a * b);
                                                }
                                                grpDThuocct.Text = "Số lượng tồn: " + tonthuoc.ToString();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Số lượng trong kho không đủ");
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                //DungChung.Bien.SoLuongTon = 0;
                                                grpDThuocct.Text = "Số lượng tồn: " + (tonthuoc + a).ToString();
                                                break;
                                            }
                                        }

                                        //xem lại lượng tồn
                                        break;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập số lượng ");
                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                        }
                    }
                    #endregion
                    #region trả dược
                    else
                    {
                        int _madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                        var muondo1 = (from dtct in _data.DThuoccts.Where(p => p.MaDV == _madv) join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.Status != 1/* && p.KieuDon == 0 */&& p.PLDV == 3) on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                        double slMuon1 = muondo1.Select(p => p.SoLuong).Sum();
                        grpDThuocct.Text = "Số lượng tồn: " + (slMuon1).ToString();
                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "" && double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString()) <= 0)
                        {

                            double a = double.Parse(grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString());
                            switch (TrangThai)
                            {
                                case 1: // khi tao don moi
                                    if (a > 0)
                                    {
                                        MessageBox.Show("Số lượng phải <0");
                                        grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                        grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                    }
                                    else if (a < 0)
                                    {
                                        if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double soluong = 0;
                                            double sl = 0;
                                            int Madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                            var trado = (from dtct in _data.DThuoccts.Where(p => p.MaDV == Madv) join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.Status != 1 && p.KieuDon == 1 && p.PLDV == 3) on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                                            for (int i=0 ;i<grvDonThuocct.DataRowCount;i++)
                                            {
                                                if (Madv == (int)grvDonThuocct.GetRowCellValue(i, colMaDVdt) && i != grvDonThuocct.FocusedRowHandle)
                                                {
                                                    soluong += (double)grvDonThuocct.GetRowCellValue(i, colSoLuong);
                                                }
                                            }
                                            double sltra = trado.Select(p => p.SoLuong).Sum();

                                            sl = soluong + sltra+(double)grvDonThuocct.GetFocusedRowCellValue(colSoLuong);

                                            var muondo = (from dtct in _data.DThuoccts.Where(p => p.MaDV == Madv) join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.Status != 1 && p.KieuDon == 0 && p.PLDV == 3) on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                                            double slMuon = muondo.Select(p => p.SoLuong ).Sum();


                                            if (sl + slMuon < 0)
                                            {
                                                MessageBox.Show("Số lượng trả vượt quá số lượng mượn! \n Số lượng mượn là: " + (slMuon + sltra));
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (slMuon + sl == 0)
                                            {
                                                grvDonThuocct.SetFocusedRowCellValue(colStatustra, "1");
                                                grpDThuocct.Text = "Số lượng tồn: " + (slMuon + sl).ToString();
                                            }
                                            else
                                            {
                                                grvDonThuocct.SetFocusedRowCellValue(colStatustra, "0");
                                                grpDThuocct.Text = "Số lượng tồn: " + (slMuon + sl).ToString();

                                            }

                                        }
                                    }
                                    break;
                                case 2:// khi sua don
                                    //soluongt = a;
                                    if (a > 0)
                                    {
                                        MessageBox.Show("Số lượng phải <0");
                                        grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];
                                        grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                    }
                                    else if (a < 0)
                                    {

                                        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                                        if (grvDonThuocct.GetFocusedRowCellValue(colMaDVdt) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double soluong = 0;
                                            madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                            int iddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                                            double sl = 0;
                                            int Madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMaDVdt));
                                            for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                            {
                                                if (Madv == (int)grvDonThuocct.GetRowCellValue(i, colMaDVdt) && i != grvDonThuocct.FocusedRowHandle)
                                                {
                                                    soluong += (double)grvDonThuocct.GetRowCellValue(i, colSoLuong);
                                                }
                                            }
                                            var trado = (from dtct in _data.DThuoccts.Where(p => p.MaDV == Madv && p.IDDonct != iddonct) join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.Status != 1 && p.KieuDon == 1) on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                                            double sltra = trado.Select(p => p.SoLuong).Sum();
                                            sl = -soluongsua + soluong + sltra;

                                            var muondo = (from dtct in _data.DThuoccts.Where(p => p.MaDV == Madv) join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.Status != 1 && p.KieuDon == 0) on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                                            double slMuon = trado.Select(p => p.SoLuong).Sum();
                                            if (sl + slMuon < 0)
                                            {
                                                MessageBox.Show("Số lượng trả vượt quá số lượng mượn! \n Số lượng mượn là: " + (slMuon+sltra));
                                                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");
                                            }
                                            else if (slMuon + sl == 0)
                                            {
                                                grvDonThuocct.SetFocusedRowCellValue(colStatustra, "1");
                                            }
                                            else grvDonThuocct.SetFocusedRowCellValue(colStatustra, "0");




                                        }

                                       
                                    }

                                    //xem lại lượng tồn

                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng nhập trả đồ phải nhỏ hơn 0");
                            grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[3];// set focus
                            grvDonThuocct.SetFocusedRowCellValue(colSoLuong, "0");

                        }
                    }
                    #endregion trả dược
                    break;
            }
        }
        private void ResetControl()
        {
            dtNgayKe.EditValue = System.DateTime.Now;
             //cboNhomDuoc.Text = "";
           // lupBPKe.EditValue = 0;
            lupKhoXuat.EditValue = 0;
            lupNguoiKe.EditValue = "";
            //txtTiencoc.Text = "";
            cboKieuPL.Text = "";
            //lbltiencoc.Text = "";

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            if (mabn==0)
            {
                MessageBox.Show("Bạn chưa chọn Bệnh nhân");
                return;
            }    
            Enablebutton(false);
            EnableControl(true);
            BothuocKoSD = true;

            ResetControl();
           // lupBPKe.EditValue = DungChung.Bien.MaKP;
            lupNguoiKe.EditValue = DungChung.Bien.MaCB;
            binSDonThuocct.DataSource = _ldthuocct.Where(p => p.IDDon == 0).ToList();
            grcDonThuocct.DataSource = binSDonThuocct;
            TrangThai = 1;
            // lupKhoXuat.Text = "Kho mượn đồ";
            //lupKhoXuat.Enabled = false;
            // lupKhoXuat.ReadOnly = true;
            lupKhoXuat.SelectedText="Kho mượn đồ";
        }
        private void Loadgrid()
        {
            grcDonThuocdt.DataSource = null;
            var danhsach = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3) join kp in _data.KPhongs on dt.MaKP equals kp.MaKP select new { dt.IDDon, dt.NgayKe, dt.MaBNhanChiTiet, kp.PLoai }).OrderByDescending(p=>p.NgayKe).ToList();
            grcDonThuocdt.DataSource = danhsach;
          //  binSDonThuocct.DataSource = _ldthuocct.Where(p => p.IDDon == 0).ToList();
           // grcDonThuocct.DataSource = binSDonThuocct;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (iddon > 0)
            {
                // lupKhoXuat.Enabled = false;
                //lupKhoXuat.ReadOnly = true;
                //TrangThai = 2;
                BothuocKoSD = true;
                int _mabp = 0;
                if (lupTimMaKP.EditValue != null)
                    _mabp = Convert.ToInt32(lupTimMaKP.EditValue);
                if (DungChung.Bien.listKPHoatDong.Where(p => p == _mabp).Count() > 0 || DungChung.Bien.PLoaiKP == "Admin")
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var qdt = data.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                    if (qdt.Count > 0)
                    {
                        if (qdt.Where(p => p.SoPL <= 0).Count() > 0)
                        {
                            Enablebutton(false);
                            EnableControl(true);

                            TrangThai = 2;
                        }
                        else if (qdt.Where(p => p.Status == null || p.Status <= 0).Count() > 0)
                            MessageBox.Show("Phiếu lĩnh đã in, bạn không thể sửa");
                        else
                            MessageBox.Show("Phiếu lĩnh đã được xuất dược, bạn không được sửa");
                    }
                    else
                        MessageBox.Show("Không có phiếu lĩnh để sửa");
                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để sửa");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (iddon != 0)
            {
                QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int id = iddon;
                var kt = (from dt in Data.DThuocs.Where(p => p.IDDon == id) join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).ToList();
                if (kt.Count > 0)
                {
                    if (kt.Where(p => p.Status != 0).Count() > 0)
                    {
                        MessageBox.Show("Có phiếu lĩnh đã xuất hoặc đã hủy, Bạn không được xóa");

                    }
                    else
                    {
                        if (kt.Where(p => p.SoPL != null && p.SoPL > 0).Count() > 0)
                        {
                            var ploai = grvDonThuocdt.GetFocusedRowCellValue(colPLoai) != null ? grvDonThuocdt.GetFocusedRowCellValue(colPLoai).ToString() : "";
                            var mabnhanchitiet = grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet) != null ? grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet).ToString() : "";
                            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && ploai.ToString() == "Tủ trực" && !string.IsNullOrEmpty(mabnhanchitiet.ToString()))
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    var ktct = Data.DThuoccts.Where(p => p.IDDon == id).Select(p => p.IDDonct).ToList();
                                    foreach (var i in ktct)
                                    {
                                        string sopl = "";
                                        var xoact = Data.DThuoccts.Single(p => p.IDDonct == i);
                                        sopl = xoact.SoPL.ToString();
                                        Data.DThuoccts.Remove(xoact);
                                        if (Data.SaveChanges() > 0)
                                        {
                                            // dthuocct.sopl của đơn bù quan hệ 1-n vs dthuocct.dscbth của đơn kê bn -> khi xóa đơn bù thì sửa đơn kê bn dthuocct.dscbth = null & dthuocct.status = 0
                                            var editdt = Data.DThuoccts.Where(p => p.DSCBTH == sopl).Select(p => p.IDDonct).ToList();
                                            foreach (var item in editdt)
                                            {
                                                var dtct = Data.DThuoccts.Single(p => p.IDDonct == item);
                                                dtct.DSCBTH = null;
                                                dtct.Status = 0;
                                                Data.SaveChanges();
                                            }
                                        }
                                    }
                                    var xoa = Data.DThuocs.Single(p => p.IDDon == id);
                                    Data.DThuocs.Remove(xoa);
                                    Data.SaveChanges();

                                }
                            }
                            else
                                MessageBox.Show("Phiếu lĩnh đã in, bạn không được xóa");
                        }
                        else
                        {
                            DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                var ktct = Data.DThuoccts.Where(p => p.IDDon == id).Select(p => p.IDDonct).ToList();
                                foreach (var i in ktct)
                                {
                                    var xoact = Data.DThuoccts.Single(p => p.IDDonct == i);
                                    Data.DThuoccts.Remove(xoact);
                                    Data.SaveChanges();
                                }
                                var xoa = Data.DThuocs.Single(p => p.IDDon == id);
                                Data.DThuocs.Remove(xoa);
                                Data.SaveChanges();
                                

                            }
                        }
                    }

                }
                else
                {
                    var xoa = Data.DThuocs.Single(p => p.IDDon == id);
                    Data.DThuocs.Remove(xoa);
                    Data.SaveChanges();
                    MessageBox.Show("Xoá thành công!");
                    
                }
                Loadgrid();
                var lstIDDon = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3) select new { dt.IDDon }).ToList();
                foreach (var item in lstIDDon)
                {

                    var dthuoc = _data.DThuocs.Single(p => p.IDDon == item.IDDon);
                    dthuoc.Status = 0;
                    _data.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để xóa");
            }
        }
        int maxid = 0;
        private void btnKLuu_Click(object sender, EventArgs e)
        {
            this.frmMuonTraDo_Load(sender, e);
        }
        double sLMuon;
        double sLuongtra;
        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (KtraLuu())
            {
                int[] arrIDdonct;
                List<Baocaos> baocaos = new List<Baocaos>();
                QLBV_Database.QLBVEntities D = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                #region thêm mới
                if (TrangThai == 1)
                {
                    DThuoc dthuoc = new DThuoc();
                    dthuoc.MaBNhan = mabn;
                    dthuoc.NgayKe = dtNgayKe.DateTime;
                    dthuoc.MaKP = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                    dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                    dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                    dthuoc.PLDV = 3;
                    dthuoc.Status = 0;
                    if (cboKieuPL.SelectedIndex == 0)
                    {
                        dthuoc.KieuDon = 0;
                    }
                    else
                    {
                        dthuoc.KieuDon = 1;
                        dthuoc.NgayTra = new DateTime(dtNgayTra.DateTime.Year, dtNgayTra.DateTime.Month, dtNgayTra.DateTime.Day, 00, 00, 00);
                    }
                    if (txtTiencoc.Text != "")
                    {
                        dthuoc.TienCoc = Convert.ToInt32(txtTiencoc.Text);
                    }
                    else dthuoc.TienCoc = 0;
                    D.DThuocs.Add(dthuoc);
                    if (D.SaveChanges() >= 0)
                    {

                        var que = (from max in _data.DThuocs.OrderByDescending(p => p.IDDon) select max.IDDon).ToList();
                        if (que.Count > 0)
                        {
                            maxid = int.Parse(que.First().ToString());
                        }
                        for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                        {
                            if (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) == null || (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoPLct)) <= 0))
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                    {
                                        DThuocct dthuocct = new DThuocct();
                                        dthuocct.SoPL = 0;
                                        dthuocct.IDDon = maxid;
                                        dthuocct.MaKP = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                                        dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                        dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                        dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                       
                                        if(grvDonThuocct.GetRowCellDisplayText(i, colGhichu)!=string.Empty)
                                            dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhichu).ToString();
                                        dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                        dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                        dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                        dthuocct.NgayNhap = dtNgayKe.DateTime;
                                        dthuocct.Status = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colStatustra));
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                            dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                            dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung).ToString());
                                        if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                            dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                        dthuocct.Status = 0;
                                        Baocaos bc = new Baocaos();
                                        bc.TenDV = grvDonThuocct.GetRowCellDisplayText(i, colMaDVdt);
                                        bc.GhiChu = grvDonThuocct.GetRowCellDisplayText(i, colGhichu);
                                        bc.SoLuong = Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoLuong));
                                        bc.DonVi = grvDonThuocct.GetRowCellDisplayText(i, colDonVi);
                                        baocaos.Add(bc);
                                        D.DThuoccts.Add(dthuocct);
                                        D.SaveChanges();

                                    }
                                }
                            }
                        }
                        TrangThai = 0;
                        MessageBox.Show("Tạo đơn thành công!");
                        #region checktttrado

                        #endregion


                       // Loadgrid();
                    }
                }
                #endregion
                #region sửa
                else
                {
                    if (TrangThai == 2)
                    {
                        QLBV_Database.QLBVEntities D1 = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        if (iddon > 0)
                        {
                            int id = iddon;
                            var dthuoc = D1.DThuocs.Single(p => p.IDDon == id);
                            dthuoc.NgayKe = dtNgayKe.DateTime;
                            dthuoc.MaKP = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                            dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                            dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                            dthuoc.PLDV = 3;
                            dthuoc.Status = 0;
                            if (cboKieuPL.SelectedIndex == 0)
                            {
                                dthuoc.KieuDon = 0;
                            }
                            else
                            {
                                dthuoc.KieuDon = 1;
                                dthuoc.NgayTra = new DateTime(dtNgayTra.DateTime.Year, dtNgayTra.DateTime.Month, dtNgayTra.DateTime.Day, 00, 00, 00);
                            }
                            dthuoc.TienCoc = Convert.ToInt32(txtTiencoc.Text);
                            if (D1.SaveChanges() >= 0)
                            {
                                // lưu chi tiết đơn
                                for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colMaDVdt) != null)
                                    {
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString() != "")
                                            {
                                                int idct = int.Parse(grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString());
                                                if (idct > 0)// sửa row
                                                {
                                                    DThuocct dthuocct = D1.DThuoccts.Single(p => p.IDDonct == idct);
                                                    dthuocct.IDDon = id;
                                                    dthuocct.MaKP = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    if (grvDonThuocct.GetRowCellDisplayText(i, colGhichu) !=string.Empty)
                                                    {
                                                        dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhichu).ToString();
                                                    }    
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    dthuocct.Status = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colStatustra));
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colHanDung).ToString();
                                                    dthuocct.Status = 0;
                                                    Baocaos bc = new Baocaos();
                                                    bc.TenDV = grvDonThuocct.GetRowCellDisplayText(i, colMaDVdt);
                                                    bc.GhiChu = grvDonThuocct.GetRowCellDisplayText(i, colGhichu);
                                                    bc.SoLuong = Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoLuong));
                                                    bc.DonVi = grvDonThuocct.GetRowCellDisplayText(i, colDonVi);
                                                    baocaos.Add(bc);
                                                    D1.SaveChanges();
                                                }
                                                else
                                                {// lưu row mới 
                                                    DThuocct dthuocct = new DThuocct();
                                                    dthuocct.IDDon = id;
                                                    dthuocct.SoPL = 0;
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMaDVdt));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    if (grvDonThuocct.GetRowCellDisplayText(i, colGhichu) != string.Empty)
                                                        dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhichu).ToString();
                                                    dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhichu).ToString();
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colHanDung).ToString();
                                                    dthuocct.Status = 0;
                                                    Baocaos bc = new Baocaos();
                                                    bc.TenDV = grvDonThuocct.GetRowCellDisplayText(i, colMaDVdt);
                                                    bc.GhiChu = grvDonThuocct.GetRowCellDisplayText(i, colGhichu);
                                                    bc.SoLuong = Math.Abs(Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoLuong)));
                                                    bc.DonVi = grvDonThuocct.GetRowCellDisplayText(i, colDonVi);
                                                    baocaos.Add(bc);
                                                    D1.DThuoccts.Add(dthuocct);
                                                    D1.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                                TrangThai = 0;
                                MessageBox.Show("Sửa thành công!");


                            }
                        }
                    }
                }
                int[] IDdonct = new int[grvDonThuocct.DataRowCount];
                // var idon = (from dtct in _data.DThuoccts join dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3 && p.NgayKe == dtNgayKe.DateTime) on dtct.IDDon equals dt.IDDon select new { }).ToList();


                #endregion
                #region Xuất phiếu
                var benhnhan = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == mabn)
                                join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                                join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                join cb in _data.CanBoes on dt.MaCB equals cb.MaCB
                                select new { bnkb.IDKB, cb.TenCB,dt.MaCB, bn.MaBNhan, bn.TenBNhan, kp.TenKP, bnkb.Buong, ttbx.CMT, bn.DChi, bn.NgaySinh, bn.ThangSinh, bn.NamSinh 
                                }).Where(p => p.MaBNhan == mabn).OrderByDescending(p => p.IDKB).ToList();


                frmIn frm = new frmIn();
                BaoCao.PhieuMuonTuTrang rep = new BaoCao.PhieuMuonTuTrang();
                rep.lblTenbn.Text = benhnhan.FirstOrDefault().TenBNhan;
                rep.lvlBuong.Text = benhnhan.FirstOrDefault().Buong;
                rep.celNguoiGiao.Text = Convert.ToString(lupNguoiKe.Text.Trim());
                rep.celNguoiNhan.Text = benhnhan.FirstOrDefault().TenBNhan;
                rep.lblKhoa.Text = lupTimMaKP.Text;
                rep.lblCMND.Text = benhnhan.FirstOrDefault().CMT;
                rep.lblDiaChi.Text = benhnhan.FirstOrDefault().DChi;
                rep.lblNgayMuon.Text = dtNgayKe.DateTime.ToString("dd/MM/yyyy");
                rep.lblNgaySinh.Text = benhnhan.FirstOrDefault().NgaySinh +"/"+ benhnhan.FirstOrDefault().ThangSinh +"/"+ benhnhan.FirstOrDefault().NamSinh;
                if (txtTiencoc.Text != "")
                {
                    int tiencoc = Convert.ToInt32(txtTiencoc.Text.Trim());
                    rep.lblTienCuoc.Text = String.Format("{0:#,##0.##}", tiencoc);
                   // rep.lblTienCuoc.Text = txtTiencoc.Text;
                    decimal tiencoc1 = Convert.ToDecimal(txtTiencoc.Text.Trim());
                    rep.lblTienCuocText.Text = DungChung.Ham.NumberToTextVN(tiencoc1);
                }
                else
                {
                    rep.lblTienCuoc.Text = "0";
                    decimal tiencoc = 0;
                    rep.lblTienCuocText.Text = DungChung.Ham.NumberToTextVN(tiencoc);
                }


                decimal tiencocs = Convert.ToDecimal(txtTiencoc.Text.Trim());
                rep.lblTienCuocText.Text = DungChung.Ham.NumberToTextVN(tiencocs);



                rep.DataSource = baocaos;
                if (cboKieuPL.SelectedIndex == 1)
                {
                    rep.lblNgayTratext.Visible = true;
                    rep.lblNgayTra.Visible = true;
                    rep.lblNgayTra.Text = dtNgayTra.DateTime.ToString("dd/MM/yyyy");
                    rep.lblTenPhieu.Text = "PHIẾU TRẢ TƯ TRANG CHO NGƯỜI BỆNH";
                }
                else
                {
                    rep.lblNgayTra.Visible = false;
                    rep.lblNgayTratext.Visible = false;
                    rep.lblTenPhieu.Text = "PHIẾU MƯỢN TƯ TRANG CHO NGƯỜI BỆNH";
                }

                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                Updatetrangthai();



               // Loadgrid();
                
                // grvBNhankb.FocusedRowHandle = rowhandle;
                grvBNhankb_RowClick(null,null);
              //  frmMuonTraDo_Load(sender, e);
                Enablebutton(true);
                EnableControl(false);
              //  ResetControl();
            }
           
        }
        List<BenhNhan> _lTKbncho = new List<BenhNhan>();
        List<LBNKB> _lBNKB = new List<LBNKB>();
        private string ThongBaoBNChuyenPK()
        {
            _lBNKB = (from kb in _data.BNKBs
                      where ((DungChung.Bien.MaBV != "14018" && DungChung.Bien.MaBV != "14017") ? (kb.NgayKham <= _dtden && kb.NgayKham >= _dttu) : true)
                      group kb by new { kb.MaBNhan } into kq
                      select new LBNKB()
                      {
                          MaBNhan = kq.Key.MaBNhan == null ? 0 : kq.Key.MaBNhan.Value,
                          IDKB = kq.Max(p => p.IDKB),
                      }).ToList();
            List<int> idkbs = _lBNKB.Select(o => o.IDKB).ToList();
            _lTKbncho = (
                from bn in _data.BenhNhans.Where(o => radNoiTru.SelectedIndex == 1 ? o.NoiTru == 1 : (o.NoiTru == 0 && o.DTNT == true)) // on kb.MaBNhan equals bn.MaBNhan
                join bnkbenh in _data.BNKBs.Where(p => idkbs.Contains(p.IDKB)) on bn.MaBNhan equals bnkbenh.MaBNhan//kb.IDKB equals bnkbenh.IDKB
                where ((bnkbenh.MaKPdt == _makp || ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && bnkbenh.MaKPDTKH == _makp)) && bnkbenh.MaKP != _makp && (bnkbenh.PhuongAn == 3 || bnkbenh.PhuongAn == 1) && bn.Status <= 1)
                select bn).Distinct().ToList();
            int id = 0;
            id = _lTKbncho.Count;
            if (id > 0)
            {
                string a = "Có: " + id + " bệnh nhân được chuyển vào khoa đang chờ...";
                return a;
            }
            else
            {
                return "";
            }
        }
        private class LBNKB
        {
            public int idkb;
            public int mabnhan;
            //public DateTime ngay;
            public int IDKB
            {
                set { idkb = value; }
                get { return idkb; }
            }
            public int MaBNhan
            {
                set { mabnhan = value; }
                get { return mabnhan; }
            }
        }
        List<KPhong> _lkp = new List<KPhong>();
        List<BenhNhan> _lTKbn = new List<BenhNhan>();
        string ChuyenKhoa = "";
        private void TimKiem()
        {
            _lTKbn = (from bn in _data.BenhNhans select bn).Distinct().ToList();
            if (lupTimMaKP.EditValue != null)
            {
                _makp = Convert.ToInt32(lupTimMaKP.EditValue);
            }

            if (_lkp.Where(p => p.MaKP == _makp).ToList().Count > 0)
            {
                ChuyenKhoa = _lkp.Where(p => p.MaKP == _makp).First().ChuyenKhoa;

            }
            else
            {
                ChuyenKhoa = "";

            }
            string _tk = "";
            int _mabnTK = 0;// ma benh nhan tim kiem nhao theo o text txtTimKiem
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                _tk = txtTimKiem.Text;
                int rs;

                if (Int32.TryParse(txtTimKiem.Text, out rs))
                    _mabnTK = Convert.ToInt32(txtTimKiem.Text);
            }
            if (chkBNcho.Checked)
            {
                //labThongBaoBNCP.Text = ThongBaoBNChuyenPK();
                _lTKbn = _lTKbncho;
            }
            else
            {
                int noitru = radNoiTru.SelectedIndex;
              //  string maCB = (cboBacSyDT.EditValue != null && !string.IsNullOrWhiteSpace(cboBacSyDT.Text)) ? cboBacSyDT.EditValue.ToString() : "";

                switch (cboTimRaVien.SelectedIndex)
                {
                    case 0: //chưa ra
                        if (ChuyenKhoa == "Phẫu thuật" || ChuyenKhoa == "Thủ thuật")
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => p.MaKCB == DungChung.Bien.MaBV)
                                      join cls in _data.CLS.Where(p => p.MaKPth == _makp).Where(p => p.NgayThang >= _dttu && p.NgayThang <= _dtden).Where(p => p.Status == 0) on bn.MaBNhan equals cls.MaBNhan
                                      select bn).Distinct().OrderBy(p => p.NNhap).ToList();

                        }
                        else
                        {
                            var bnChuaRa = (from bn in _data.BenhNhans.Where(p => (p.Status == 1 || p.Status == 4 || p.Status == 5)).Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru)
                                            join bnkb in _data.BNKBs.Where(o => (o.MaKP == _makp || o.MaKPDTKH == _makp)) on bn.MaBNhan equals bnkb.MaBNhan into kq
                                            from bkkbc in kq.DefaultIfEmpty()
                                            where (bn.MaKP == _makp || bn.MaKPDTKH == _makp)
                                            where (bn.MaKCB == DungChung.Bien.MaBV)
                                            select new { bkkbc, bn }).OrderBy(p => p.bn.MaBNhan).OrderBy(p => p.bn.TenBNhan).ToList();


                            _lTKbn = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? bnChuaRa.Where(o => o.bkkbc != null).Select(o => o.bn).Distinct().ToList() : bnChuaRa.Select(o => o.bn).Distinct().ToList();

                        }
                        break;
                    case 1: //ra viện chưa TT
                        if (ChuyenKhoa == "Phẫu thuật" || ChuyenKhoa == "Thủ thuật")
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru)
                                      join cls in _data.CLS.Where(p => p.MaKPth == _makp) on bn.MaBNhan equals cls.MaBNhan
                                      where (cls.NgayThang >= _dttu && cls.NgayThang <= _dtden)
                                      where (cls.Status == 1)
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();
                        }
                        else
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => (p.Status <= 2 || p.Status == 4 || p.Status == 5)).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                      join kb in _data.RaViens.Where(p => p.Status == 2 || p.Status == 4) on bn.MaBNhan equals kb.MaBNhan
                                      where (bn.MaKCB == DungChung.Bien.MaBV)

                                      where (kb.NgayRa >= _dttu && kb.NgayRa <= _dtden)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();

                        }

                        break;
                    case 2: // chuyển viện
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                      join kb in _data.RaViens.Where(p => p.Status == 1) on bn.MaBNhan equals kb.MaBNhan

                                      where (kb.NgayRa >= _dttu && kb.NgayRa <= _dtden)
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();
                        }
                        break;
                    case 3: // vào khoa trực tiếp
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => p.Status == 0).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))

                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.SoTT).ToList();

                        }
                        break;
                    case 4: // chuyển khoa điều trị
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => p.Status >= 1)
                                      join kb in _data.BNKBs.Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp) && p.PhuongAn == 3) on bn.MaBNhan equals kb.MaBNhan

                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).Distinct().OrderBy(p => p.MaBNhan).OrderBy(p => p.SoTT).ToList();

                        }
                        break;
                    case 5: //đã TT
                        if (ChuyenKhoa == "Phẫu thuật" || ChuyenKhoa == "Thủ thuật")
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => p.NoiTru == 1)
                                      join cls in _data.CLS.Where(p => p.MaKPth == _makp) on bn.MaBNhan equals cls.MaBNhan
                                      where (cls.NgayThang >= _dttu && cls.NgayThang <= _dtden)
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      where (cls.Status == 1)
                                      select bn).Distinct().OrderBy(p => p.NNhap).ToList();

                        }
                        else
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                      join kb in _data.VienPhis on bn.MaBNhan equals kb.MaBNhan
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();

                        }

                        break;
                    case 6: // trốn viện
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                      join kb in _data.RaViens.Where(p => p.Status == 3) on bn.MaBNhan equals kb.MaBNhan

                                      where (kb.NgayRa >= _dttu && kb.NgayRa <= _dtden)
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();

                        }

                        break;
                    case 7: //ra viện chưa TT
                        {
                            _lTKbn = (from bn in _data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                      join kb in _data.RaViens.Where(p => p.Status == 4) on bn.MaBNhan equals kb.MaBNhan
                                      where (kb.NgayRa >= _dttu && kb.NgayRa <= _dtden)
                                      where (bn.MaKCB == DungChung.Bien.MaBV)
                                      select bn).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();

                        }
                        break;
                }
                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                    _lTKbn = _lTKbn.OrderByDescending(o => o.NNhap).ToList();
                grcBNhankb.DataSource = null;
                grcBNhankb.DataSource = _lTKbn.ToList();

                int soBNBH = _lTKbn.Where(p => p.DTuong == "BHYT").Count();
                int soBNDV = _lTKbn.Where(p => p.DTuong.ToLower() == "dịch vụ").Count();
                int soBNKMP = _lTKbn.Where(p => p.DTuong.ToLower() == "khám miễn phí").Count();
               // lblTSBN.Text = "TS: " + _lTKbn.Count() + " (" + soBNBH + " BN BHYT, " + soBNDV + " BN Dịch vụ" + (DungChung.Bien.MaBV == "20001" ? (", " + soBNKMP + " BN Khám miễn phí") : "") + ")";// swith

            }
            timkiem2();
        }
        void timkiem2()
        {
           
           // bool all = false;
           
              //  all = true;
            string _tk = "";
            int _mabnTK = 0;// ma benh nhan tim kiem nhao theo o text txtTimKiem
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                _tk = txtTimKiem.Text.Trim().ToLower();
                int rs;

                if (Int32.TryParse(txtTimKiem.Text, out rs))
                    _mabnTK = Convert.ToInt32(txtTimKiem.Text);
            }
            DateTime ngaytu = DungChung.Ham.NgayTu(DateTime.Now);
            DateTime ngayden = DungChung.Ham.NgayDen(DateTime.Now);
            grcBNhankb.DataSource = null;
            grcBNhankb.DataSource = (from a in _lTKbn
                                     where (a.TenBNhan.ToLower().Contains(_tk) || a.MaBNhan == _mabnTK)
                                     select a).ToList();
          

        }


        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void cboTimRaVien_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            timkiem2();
        }

        private void grvBNhankb_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            grvBNhankb_FocusedRowChanged(null,null);
        }
        

        private void grvBNhankb_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           // rowhandle = grvBNhankb.FocusedRowHandle;
            mabn = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colMaBNhan));
            Loadgrid();
            var tiencoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3 && p.KieuDon == 0 && p.Status !=1) select new { dt.TienCoc }).ToList();
            var tiencoctra = (from dt in _data.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3 && p.KieuDon == 1 && p.Status != 1) select new { dt.TienCoc }).ToList();
            int tongdacoc = (int)tiencoc.Sum(p => p.TienCoc);
            int tongdatra = (int)tiencoctra.Sum(p => p.TienCoc);
            int tongtien = tongdacoc - tongdatra;
            lblTiencocs.Text = "Tổng tiền cọc: " + String.Format("{0:#,##0.##}", tongtien);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTiencoc_EditValueChanged(object sender, EventArgs e)
        {
             
           
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            var benhnhan = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == mabn)
                            join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                            join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                            join ttbx in _data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                            join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                            join cb in _data.CanBoes on dt.MaCB equals cb.MaCB
                            select new { bnkb.IDKB, kp.TenKP, cb.TenCB, dt.MaCB, bn.MaBNhan, bn.TenBNhan, bnkb.MaKP, bnkb.Buong, ttbx.CMT, bn.DChi, bn.NgaySinh, bn.ThangSinh, bn.NamSinh 
                            }).Where(p => p.MaBNhan == mabn).OrderByDescending(p => p.IDKB).ToList();

            frmIn frm = new frmIn();
            BaoCao.PhieuMuonTuTrang rep = new BaoCao.PhieuMuonTuTrang();
            rep.lblTenbn.Text = benhnhan.FirstOrDefault().TenBNhan;
            rep.celNguoiGiao.Text = Convert.ToString(lupNguoiKe.Text.Trim());
            rep.celNguoiNhan.Text = benhnhan.FirstOrDefault().TenBNhan;
            rep.lvlBuong.Text = benhnhan.FirstOrDefault().Buong;
            rep.lblKhoa.Text = lupTimMaKP.Text;
            rep.lblCMND.Text = benhnhan.FirstOrDefault().CMT;
            rep.lblDiaChi.Text = benhnhan.FirstOrDefault().DChi;
            rep.lblNgayMuon.Text = dtNgayKe.DateTime.ToString("dd/MM/yyyy");
            rep.lblNgaySinh.Text = benhnhan.FirstOrDefault().NgaySinh +"/"+ benhnhan.FirstOrDefault().ThangSinh +"/"+ benhnhan.FirstOrDefault().NamSinh;
            if (txtTiencoc.Text != "")
            {
                int tiencoc = Convert.ToInt32(txtTiencoc.Text.Trim());
                rep.lblTienCuoc.Text = String.Format("{0:#,##0.##}", tiencoc);
                decimal tiencocs = Convert.ToDecimal(txtTiencoc.Text.Trim());
                rep.lblTienCuocText.Text = DungChung.Ham.NumberToTextVN(tiencocs);
            }
            else
            {
                 rep.lblTienCuoc.Text = "0";
                decimal tiencocs = 0;
                rep.lblTienCuocText.Text = DungChung.Ham.NumberToTextVN(tiencocs);
            }
            
           

            var domuon = (from dt in _data.DThuoccts.Where(p => p.IDDon == iddon) join dv in _data.DichVus on dt.MaDV equals dv.MaDV select new {dv.TenDV,dt.SoLuong,dt.DonVi,dt.GhiChu }).ToList();

            rep.DataSource = domuon;
            if (cboKieuPL.SelectedIndex == 1)
            {
                rep.lblNgayTratext.Visible = true;
                rep.lblNgayTra.Visible = true;
                rep.lblNgayTra.Text = dtNgayTra.DateTime.ToString("dd/MM/yyyy");
                rep.lblTenPhieu.Text = "PHIẾU TRẢ TƯ TRANG CHO NGƯỜI BỆNH";
            }
            else
            {
                rep.lblNgayTra.Visible = false;
                rep.lblNgayTratext.Visible = false;
                rep.lblTenPhieu.Text = "PHIẾU MƯỢN TƯ TRANG CHO NGƯỜI BỆNH";
            }

            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            #endregion
        }
       
        private void Updatetrangthai()
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (cboKieuPL.SelectedIndex==1)
            {
                var domuon = (from dtct in _dataContext.DThuoccts 
                              join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.KieuDon == 0 && p.PLDV == 3)
                              on dtct.IDDon equals dt.IDDon select new { dtct.SoLuong }).ToList();
                double tongmuon = domuon.Sum(p => p.SoLuong);
                var dotra = (from dtct in _dataContext.DThuoccts
                              join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.KieuDon == 1 && p.PLDV == 3)
                              on dtct.IDDon equals dt.IDDon
                              select new { dtct.SoLuong }).ToList();
                double tongtra = dotra.Sum(p => p.SoLuong);
                if ((tongmuon + tongtra) ==0)
                {
                    var lstIDDon = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3) select new { dt.IDDon }).ToList();
                    foreach (var item in lstIDDon)
                    {
                        
                        var dthuoc = _dataContext.DThuocs.Single(p => p.IDDon == item.IDDon);
                        dthuoc.Status = 1;
                        _dataContext.SaveChanges();
                    }    
                }
                else
                {
                    var lstIDDon = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 3) select new { dt.IDDon }).ToList();
                    foreach (var item in lstIDDon)
                    {

                        var dthuoc = _data.DThuocs.Single(p => p.IDDon == item.IDDon);
                        dthuoc.Status = 0;
                        _dataContext.SaveChanges();
                    }
                } 
                    
            }    
        }

        private void grvDonThuocct_Click(object sender, EventArgs e)
        {

        }

        private void grvDonThuocdt_DataSourceChanged(object sender, EventArgs e)
        {
          
        }

        private void radNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvDonThuocct_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                    }
    }
    public class DV
    {
        public int? MaDV { get; set; }
        public string TenDV { get; set; }
        public string HamLuong { get; set; }
        public string SoLo { get; set; }
        public DateTime? HanDung { get; set; }
    }
    public class Baocaos
    {
        public string TenDV { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
        public string DonVi { get; set; }
    }
    
}