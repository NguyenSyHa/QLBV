using System;
using QLBV_Database;
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
    public partial class frm_SaoChiDinh : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_SaoChiDinh(int MaBN)
        {
            InitializeComponent();
            _mabn = MaBN;
        }
        public class DichVuCD
        {
            public int IDCLS { get; set; }
            public bool Chon { get; set; }
            public string TenDV { get; set; }
        }

        QLBV_Database.QLBVEntities data;
        List<DichVu> _ldv = new List<DichVu>();
        public delegate void ReLoad();
        public ReLoad reloaddata;
        private void frm_SaoChiDinh_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                    join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 8) on dv.IdTieuNhom equals tn.IdTieuNhom
                    select dv).ToList();
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
            dateNgayCD.DateTime = DateTime.Now;
            dateNgayTH.DateTime = DateTime.Now;
            cbotrangthai.SelectedIndex = 0;
            var lkp = (from bnkb in data.BNKBs.Where(p => p.MaBNhan == _mabn)
                       join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                       select new { kp.MaKP, kp.TenKP }).Distinct().ToList();
            var kpkh = (from benhNhan in data.BenhNhans.Where(o => o.MaBNhan == _mabn)
                        join kp in data.KPhongs on benhNhan.MaKPDTKH equals kp.MaKP
                        select new { kp.MaKP, kp.TenKP }).ToList();
            lkp.AddRange(kpkh);
            lkp = lkp.Distinct().ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupKPhong.Properties.DataSource = lkp;
                if (lkp.Count > 0)
                    lupKPhong.EditValue = lkp.First().MaKP;
            }
            else
            {
                var kp = (from a in lkp
                          join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                          select a).ToList();
                lupKPhong.Properties.DataSource = kp;
                lupKPhong.EditValue = DungChung.Bien.MaKP;
            }
            var bn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (bn != null)
            {
                this.Text = "Sao Thủ thuật - Phẫu thuật bệnh nhân: " + bn.TenBNhan.ToUpper() + " - " + bn.MaBNhan.ToString();
            }
            dateNgayCD1.DateTime = DateTime.Now.AddDays(1).AddMinutes(5);
            labelControl8.Visible = false;
            dateNgayCD1.Visible = false;
        }
        List<DichVuCD> ListDV = new List<DichVuCD>();
        void LoadDSDichVu()
        {
            ListDV.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _TuNgay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime _DenNgay = DungChung.Ham.NgayDen(dedenngay.DateTime);
            int TrangThai = cbotrangthai.SelectedIndex;
            int Makp = 0;
            if (lupKPhong.EditValue != null)
            {
                Makp = Convert.ToInt32(lupKPhong.EditValue);
            }
            var _q1 = (from cls in data.CLS.Where(p => p.MaKP == Makp).Where(p => p.NgayThang >= _TuNgay && p.NgayThang <= _DenNgay).Where(p => p.MaBNhan == _mabn).Where(p => TrangThai == 2 ? true : p.Status == TrangThai)
                       join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                       select new
                       {
                           cls.IdCLS,
                           cd.MaDV
                       }).ToList();
            ListDV.Add(new DichVuCD { IDCLS = 0, Chon = true, TenDV = "Chọn tất cả" });
            var _q2 = (from a in _q1
                       join b in _ldv on a.MaDV equals b.MaDV
                       select new DichVuCD
                       {
                           IDCLS = a.IdCLS,
                           TenDV = b.TenDV,
                           Chon = true
                       }).OrderBy(p => p.IDCLS).ToList();
            ListDV.AddRange(_q2);
            grcDichVu.DataSource = null;
            grcDichVu.DataSource = ListDV;
        }

        private void detungay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSDichVu();
        }

        private void dedenngay_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSDichVu();
        }

        private void cbotrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSDichVu();
            if (cbotrangthai.SelectedIndex == 0)
            {
                ckcSaoKQ.Visible = false;
            }
            else
            {
                ckcSaoKQ.Visible = true;
            }
            if (chkSaoNhieu.Checked == true)
            {
                ckcSaoKQ.Visible = false;
                ckcSaoKQ.Checked = false;
            }
        }

        private void ckcSaoKQ_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcSaoKQ.Checked == true)
            {
                labelControl5.Visible = true;
                dateNgayTH.Visible = true;
                labelControl7.Visible = true;
                cboTyLe.Visible = true;
            }
            else
            {
                dateNgayTH.Visible = false;
                labelControl5.Visible = false;
            }
            if (ckcSaoKQ.Checked == true && chkSaoNhieu.Checked == true)
            {
                dateNgayTH.Visible = false;
                labelControl5.Visible = false;
            }

        }

        private void grvDichVu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {

                if (e.RowHandle == 0)
                {
                    if (grvDichVu.GetFocusedRowCellValue(colChon) != null)
                    {
                        if (grvDichVu.GetRowCellValue(0, colChon).ToString() == "False")
                        {
                            for (int i = 0; i < grvDichVu.RowCount; i++)
                            {
                                grvDichVu.SetRowCellValue(i, "Chon", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvDichVu.RowCount; i++)
                            {
                                grvDichVu.SetRowCellValue(i, "Chon", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvDichVu.RowCount; i++)
                    {
                        if (grvDichVu.GetRowCellValue(i, colChon) != null && grvDichVu.GetRowCellValue(i, colChon).ToString() == "True")
                        {
                            grvDichVu.SetRowCellValue(0, colChon, false);
                            break;
                        }
                    }

                }

            }
        }

        private void btnSaoCD_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> _lidCLS = new List<int>();

                for (int i = 0; i < grvDichVu.RowCount; i++)
                {
                    if (grvDichVu.GetRowCellValue(i, "Chon") != null && grvDichVu.GetRowCellValue(i, "Chon").ToString() == "True")
                    {
                        if (Convert.ToInt32(grvDichVu.GetRowCellValue(i, "IDCLS")) > 0)
                            _lidCLS.Add(Convert.ToInt32(grvDichVu.GetRowCellValue(i, "IDCLS")));
                    }
                }
                
                var _madv = (from b in data.CLS.Where(p => p.MaBNhan == _mabn).Where(p => _lidCLS.Contains(p.IdCLS))
                             join c in data.ChiDinhs on b.IdCLS equals c.IdCLS
                             select new
                             {
                                 c.MaDV,
                                 b.IdCLS,
                                 b.MaBNhan,
                                 b.MaCB,
                                 b.MaKP,
                                 b.MaKPth,
                                 b.CapCuu,
                                 b.ChanDoan,
                                 b.MaICD,
                                 b.Status,
                                 b.DSCBTH,
                                 b.NgayThang,
                                 c.NgayBDTH,
                                 b.MaCBth,
                                 b.NgayTH
                             }).ToList();

                int idcl = _madv.Select(p=>p.IdCLS).FirstOrDefault();

                var cls = data.CLS.Where(p => p.IdCLS == idcl).FirstOrDefault();

                bool SaoKQ = ckcSaoKQ.Checked;
                bool Sao = true;
                bool SaoNhieu = chkSaoNhieu.Checked;
                if (_lidCLS.Count <= 0)
                {
                    MessageBox.Show("Không có dịch vụ để sao!");
                    Sao = false;
                }
                //else if (dateNgayCD.DateTime != null && dateNgayCD1.DateTime != null && dateNgayTH.DateTime != null && _mabn > 0 && SaoKQ && SaoNhieu)
                //    Sao = false;

                //else if (!check(cls, dateNgayCD.DateTime, dateNgayCD1.DateTime, dateNgayTH.DateTime, _mabn, SaoKQ, SaoNhieu))
                //    Sao = false;
                var bn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                if (bn.Count > 0 && dateNgayCD.DateTime < bn.First().NNhap)
                {
                    MessageBox.Show("Sao đơn không thành công! \n Ngày CĐ nhỏ hơn ngày nhập. \n Ngày nhập: " + bn.First().NNhap.ToString(), "Thông báo!");
                    Sao = false;
                }

                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                int makp = 0;
                if (lupKPhong.EditValue != null)
                    makp = Convert.ToInt32(lupKPhong.EditValue);
                if (Sao)
                {
                    if (SaoKQ && SaoNhieu)
                    {
                        int x = 0;
                        x = (dateNgayCD1.DateTime - dateNgayCD.DateTime).Days;
                        if (x == 0)
                        {
                            foreach (var item in _madv)
                            {
                                int IdCLS = item.IdCLS;
                                CL moicls = new CL();
                                moicls.MaBNhan = item.MaBNhan;
                                moicls.MaCB = item.MaCB;
                                moicls.MaKP = item.MaKP;
                                moicls.MaKPth = item.MaKPth;
                                moicls.NgayThang = dateNgayCD.DateTime;
                                moicls.CapCuu = item.CapCuu;
                                moicls.Status = 0;
                                moicls.ChanDoan = item.ChanDoan;
                                moicls.MaICD = item.MaICD;
                                data.CLS.Add(moicls);
                                data.SaveChanges();
                                int IdCLSNew = moicls.IdCLS;
                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                                foreach (var items in chidinh)
                                {
                                    int idcd = items.IDCD;
                                    ChiDinh themmoiCD = new ChiDinh();
                                    themmoiCD.IdCLS = IdCLSNew;
                                    themmoiCD.MaDV = items.MaDV;
                                    themmoiCD.Status = 0;
                                    themmoiCD.DonGia = items.DonGia;
                                    themmoiCD.TrongBH = items.TrongBH;
                                    themmoiCD.ChiDinh1 = items.ChiDinh1;
                                    themmoiCD.XHH = 0;
                                    data.ChiDinhs.Add(themmoiCD);
                                    if (data.SaveChanges() >= 0)
                                    {
                                        int idCDnew = themmoiCD.IDCD;
                                        var clsct = data.CLScts.Where(p => p.IDCD == idcd).ToList();
                                        foreach (var item2 in clsct)
                                        {
                                            CLSct themmoiCL = new CLSct();
                                            themmoiCL.IDCD = idCDnew;
                                            themmoiCL.MaDVct = item2.MaDVct;
                                            themmoiCL.Status = 0;
                                            data.CLScts.Add(themmoiCL);
                                            data.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        if (x > 0)
                        {

                            for (int i = 0; i <= x; i++)
                            {
                                foreach (var item in _madv)
                                {
                                    int IdCLS = item.IdCLS;
                                    CL moicls = new CL();
                                    moicls.MaBNhan = item.MaBNhan;
                                    moicls.MaCB = item.MaCB;
                                    moicls.MaKP = item.MaKP;
                                    moicls.MaKPth = item.MaKPth;
                                    int a = item.NgayBDTH.Value.Hour;
                                    int b = item.NgayBDTH.Value.Minute;
                                    moicls.NgayThang = dateNgayCD.DateTime.AddDays(i);
                                    moicls.NgayThang = Convert.ToDateTime(moicls.NgayThang.Value.Day + "/" + moicls.NgayThang.Value.Month + "/" + moicls.NgayThang.Value.Year + " " + a + ":" + b);
                                    
                                    {
                                        moicls.NgayTH = Convert.ToDateTime(moicls.NgayThang.Value.Day + "/" + moicls.NgayThang.Value.Month + "/" + moicls.NgayThang.Value.Year + " " + item.NgayTH.Value.Hour + ":" + item.NgayTH.Value.Minute);
                                        moicls.DSCBTH = item.DSCBTH;
                                        moicls.MaCBth = item.MaCBth;
                                    }
                                    if (ckcSaoKQ.Checked == true && chkSaoNhieu.Checked == false)
                                    {
                                        moicls.NgayTH = dateNgayTH.DateTime;
                                    }
                                    moicls.CapCuu = item.CapCuu;
                                    moicls.Status = item.Status;
                                    moicls.ChanDoan = item.ChanDoan;
                                    moicls.MaICD = item.MaICD;
                                    data.CLS.Add(moicls);
                                    data.SaveChanges();
                                    int IdCLSNew = moicls.IdCLS;
                                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                                    foreach (var items in chidinh)
                                    {
                                        int idCD = items.IDCD;
                                        ChiDinh themmoiCD = new ChiDinh();
                                        themmoiCD.IdCLS = IdCLSNew;
                                        themmoiCD.MaDV = items.MaDV;
                                        if (items.Status == 1)
                                        {
                                            themmoiCD.Status = 1;
                                            themmoiCD.KetLuan = items.KetLuan;
                                            themmoiCD.LoiDan = items.LoiDan;
                                            themmoiCD.GhiChu = items.GhiChu;
                                            themmoiCD.DSCBTH = items.DSCBTH;
                                            themmoiCD.NgayTH = Convert.ToDateTime(moicls.NgayThang.Value.Day + "/" + moicls.NgayThang.Value.Month + "/" + moicls.NgayThang.Value.Year + " " + item.NgayTH.Value.Hour + ":" + item.NgayTH.Value.Minute);
                                            themmoiCD.MaCBth = items.MaCBth;
                                            themmoiCD.NgayBDTH = moicls.NgayThang;
                                        }
                                        else
                                            themmoiCD.Status = 0;
                                        themmoiCD.DonGia = items.DonGia;
                                        themmoiCD.TrongBH = items.TrongBH;
                                        themmoiCD.ChiDinh1 = items.ChiDinh1;
                                        themmoiCD.XHH = 0;
                                        data.ChiDinhs.Add(themmoiCD);
                                        if (data.SaveChanges() >= 0)
                                        {
                                            int idCDNew = themmoiCD.IDCD;
                                            var clsct = data.CLScts.Where(p => p.IDCD == idCD).ToList();
                                            foreach (var item2 in clsct)
                                            {
                                                CLSct themmoiCL = new CLSct();
                                                themmoiCL.IDCD = idCDNew;
                                                themmoiCL.MaDVct = item2.MaDVct;
                                                if (item2.Status == 1)
                                                {
                                                    themmoiCL.KetQua = item2.KetQua;
                                                    themmoiCL.Status = 1;
                                                }
                                                else
                                                {
                                                    themmoiCL.Status = 0;
                                                }

                                                data.CLScts.Add(themmoiCL);
                                                data.SaveChanges();
                                            }
                                        }
                                    }

                                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                    {
                                        var ngayDB = moicls.NgayThang ?? DateTime.Now;
                                        DungChung.Ham.Update_CLS_DienBienct(moicls.MaBNhan ?? 0, ngayDB.Date, moicls.MaKP);
                                        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        var dienBien = data.DienBiens.FirstOrDefault(o => o.MaBNhan == (moicls.MaBNhan ?? 0) && o.NgayNhap.Value.Day == ngayDB.Day && o.NgayNhap.Value.Month == ngayDB.Month && o.NgayNhap.Value.Year == ngayDB.Year && o.Loai == 1 && o.MaKP == moicls.MaKP);
                                        if (dienBien != null)
                                        {
                                            dienBien.YLenh = "";
                                            dienBien.MaKP = moicls.MaKP;
                                            data.SaveChanges();
                                            DungChung.Ham.Update_DienBien_All(dienBien.ID, moicls.MaKP);
                                        }
                                        else
                                        {
                                            DienBien dienBienNew = new DienBien();
                                            dienBienNew.NgayNhap = ngayDB;
                                            dienBienNew.YLenh = "";
                                            dienBienNew.DienBien1 = "";
                                            dienBienNew.MaCB = DungChung.Bien.MaCB;
                                            dienBienNew.Loai = 1;
                                            dienBienNew.MaKP = moicls.MaKP;
                                            dienBienNew.Ploai = 0;
                                            dienBienNew.MaBNhan = moicls.MaBNhan;
                                            data.DienBiens.Add(dienBienNew);
                                            data.SaveChanges();

                                            DungChung.Ham.Update_DienBien_All(dienBienNew.ID, moicls.MaKP);
                                        }
                                    }

                                    //thêm vào bảng đơn thuốc
                                    if (item.Status == 1)
                                    {
                                        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        int tyleTT = Convert.ToInt32(cboTyLe.Text);
                                        int _idkb = 0;
                                        var bnkb = data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                                        if (bnkb.Count > 0)
                                            _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                                        int iddthuoc = 0;
                                        var cdinh = (from cl in data.CLS.Where(p => p.IdCLS == IdCLSNew)
                                                     join cd1 in data.ChiDinhs on cl.IdCLS equals cd1.IdCLS
                                                     join dv in data.DichVus on cd1.MaDV equals dv.MaDV
                                                     select new { cl.NgayTH, cl.MaCBth, cl.DSCBTH, cd1.SoPhieu, cl.MaKP, cl.MaCB, cd1.MaDV, dv.DonGia, dv.DonGia2, cd1.IDCD, dv.DonVi, cd1.TrongBH }).ToList();
                                        //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                                        var ktdthuoc = data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                                        if (ktdthuoc.Count > 0)
                                            iddthuoc = ktdthuoc.First().IDDon;
                                        if (iddthuoc > 0)
                                        {
                                            int Tontai = 0;
                                            foreach (var d in cdinh)
                                            {
                                                var kt = (from dt in data.DThuoccts.Where(p => p.IDCD == d.IDCD) select new { dt.IDDonct }).ToList();
                                                if (kt.Count > 0)
                                                { Tontai = Tontai + 1; }
                                            }
                                            if (Tontai == 0)
                                            {

                                                foreach (var cd2 in cdinh)
                                                {
                                                    double _dongia = DungChung.Ham._getGiaDM(data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, _mabn, cd2.NgayTH == null ? DateTime.Now : cd2.NgayTH.Value);
                                                    DThuocct moi = new DThuocct();
                                                    moi.MaDV = cd2.MaDV;
                                                    moi.IDDon = iddthuoc;
                                                    moi.IDKB = _idkb;
                                                    moi.DonVi = cd2.DonVi;
                                                    moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                                                    moi.IDCD = cd2.IDCD;
                                                    moi.DonGia = _dongia;
                                                    moi.MaCB = cd2.MaCBth;
                                                    moi.DSCBTH = cd2.DSCBTH;
                                                    moi.MaKP = makp;
                                                    moi.ThanhTien = _dongia * tyleTT / 100;
                                                    moi.NgayNhap = cd2.NgayTH;
                                                    moi.SoLuong = 1;
                                                    if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                                        moi.ThanhToan = 1;
                                                    moi.TyLeTT = tyleTT;
                                                    moi.IDCLS = IdCLSNew;
                                                    data.DThuoccts.Add(moi);
                                                    data.SaveChanges();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DThuoc dthuoccd = new DThuoc();
                                            dthuoccd.NgayKe = cdinh.FirstOrDefault().NgayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                                            dthuoccd.MaBNhan = _mabn;
                                            dthuoccd.MaKP = cdinh.First().MaKP;
                                            dthuoccd.MaCB = cdinh.First().MaCB;
                                            dthuoccd.PLDV = 2;
                                            dthuoccd.KieuDon = -1;
                                            data.DThuocs.Add(dthuoccd);
                                            if (data.SaveChanges() >= 0)
                                            {
                                                var maxid = data.DThuocs.Where(p => p.MaBNhan == _mabn).ToList().Max(p => p.IDDon);
                                                foreach (var cd3 in cdinh)
                                                {
                                                    double _dongia = DungChung.Ham._getGiaDM(data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, _mabn, cd3.NgayTH == null ? DateTime.Now : cd3.NgayTH.Value);
                                                    DThuocct moi = new DThuocct();
                                                    moi.MaDV = cd3.MaDV;
                                                    moi.IDDon = maxid;
                                                    moi.IDKB = _idkb;
                                                    //var BH = (from Dv in _db.DichVus.Where(p => p.MaDV == cd3.MaDV) select new { Dv.TrongDM }).ToList();
                                                    //if (BH.Count > 0)
                                                    //{
                                                    //    moi.TrongBH = BH.First().TrongDM;
                                                    //}
                                                    moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;

                                                    moi.MaCB = cd3.MaCBth;

                                                    moi.DSCBTH = cd3.DSCBTH;
                                                    moi.IDKB = _idkb;
                                                    moi.NgayNhap = cd3.NgayTH;
                                                    moi.MaKP = makp;
                                                    moi.IDCD = cd3.IDCD;
                                                    moi.DonVi = cd3.DonVi;
                                                    moi.DonGia = _dongia;
                                                    moi.ThanhTien = _dongia * tyleTT / 100;
                                                    moi.SoLuong = 1;
                                                    if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                                        moi.ThanhToan = 1;
                                                    moi.TyLeTT = tyleTT;
                                                    moi.IDCLS = IdCLSNew;
                                                    data.DThuoccts.Add(moi);
                                                    data.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (SaoKQ)
                    {
                        foreach (var item in _madv)
                        {
                            int IdCLS = item.IdCLS;
                            CL moicls = new CL();
                            moicls.MaBNhan = item.MaBNhan;
                            moicls.MaCB = item.MaCB;
                            moicls.MaKP = item.MaKP;
                            moicls.MaKPth = item.MaKPth;
                            moicls.NgayThang = dateNgayCD.DateTime;

                            moicls.NgayTH = dateNgayTH.DateTime;
                            if (item.Status == 1)
                            {
                                moicls.DSCBTH = item.DSCBTH;
                                moicls.MaCBth = item.MaCBth;
                            }

                            moicls.CapCuu = item.CapCuu;
                            moicls.Status = item.Status;
                            moicls.ChanDoan = item.ChanDoan;
                            moicls.MaICD = item.MaICD;
                            data.CLS.Add(moicls);
                            data.SaveChanges();
                            int IdCLSNew = moicls.IdCLS;
                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                            foreach (var items in chidinh)
                            {
                                int idCD = items.IDCD;
                                ChiDinh themmoiCD = new ChiDinh();
                                themmoiCD.IdCLS = IdCLSNew;
                                themmoiCD.MaDV = items.MaDV;
                                if (items.Status == 1)
                                {
                                    themmoiCD.Status = 1;
                                    themmoiCD.KetLuan = items.KetLuan;
                                    themmoiCD.LoiDan = items.LoiDan;
                                    themmoiCD.GhiChu = items.GhiChu;
                                    themmoiCD.DSCBTH = items.DSCBTH;
                                    themmoiCD.MaCBth = items.MaCBth;
                                    themmoiCD.NgayTH = dateNgayTH.DateTime;
                                    themmoiCD.NgayBDTH = moicls.NgayThang;


                                }
                                else
                                    themmoiCD.Status = 0;
                                themmoiCD.DonGia = items.DonGia;
                                themmoiCD.TrongBH = items.TrongBH;
                                themmoiCD.ChiDinh1 = items.ChiDinh1;
                                themmoiCD.XHH = 0;
                                data.ChiDinhs.Add(themmoiCD);
                                if (data.SaveChanges() >= 0)
                                {
                                    int idCDNew = themmoiCD.IDCD;
                                    var clsct = data.CLScts.Where(p => p.IDCD == idCD).ToList();
                                    foreach (var item2 in clsct)
                                    {
                                        CLSct themmoiCL = new CLSct();
                                        themmoiCL.IDCD = idCDNew;
                                        themmoiCL.MaDVct = item2.MaDVct;
                                        if (item2.Status == 1)
                                        {
                                            themmoiCL.KetQua = item2.KetQua;
                                            themmoiCL.Status = 1;
                                        }
                                        else
                                        {
                                            themmoiCL.Status = 0;
                                        }

                                        data.CLScts.Add(themmoiCL);
                                        data.SaveChanges();
                                    }
                                }
                                //thêm vào bảng đơn thuốc
                                if (item.Status == 1)
                                {
                                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    int tyleTT = Convert.ToInt32(cboTyLe.Text);
                                    int _idkb = 0;
                                    var bnkb = data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                                    if (bnkb.Count > 0)
                                        _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                                    int iddthuoc = 0;
                                    var cdinh = (from cl in data.CLS.Where(p => p.IdCLS == IdCLSNew)
                                                 join cd1 in data.ChiDinhs on cl.IdCLS equals cd1.IdCLS
                                                 join dv in data.DichVus on cd1.MaDV equals dv.MaDV
                                                 select new { cl.NgayTH, cl.MaCBth, cl.DSCBTH, cd1.SoPhieu, cl.MaKP, cl.MaCB, cd1.MaDV, dv.DonGia, dv.DonGia2, cd1.IDCD, dv.DonVi, cd1.TrongBH }).ToList();
                                    //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                                    var ktdthuoc = data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                                    if (ktdthuoc.Count > 0)
                                        iddthuoc = ktdthuoc.First().IDDon;
                                    if (iddthuoc > 0)
                                    {
                                        int Tontai = 0;
                                        foreach (var d in cdinh)
                                        {
                                            var kt = (from dt in data.DThuoccts.Where(p => p.IDCD == d.IDCD) select new { dt.IDDonct }).ToList();
                                            if (kt.Count > 0)
                                            { Tontai = Tontai + 1; }
                                        }
                                        if (Tontai == 0)
                                        {

                                            foreach (var cd2 in cdinh)
                                            {
                                                double _dongia = DungChung.Ham._getGiaDM(data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, _mabn, cd2.NgayTH == null ? DateTime.Now : cd2.NgayTH.Value);
                                                DThuocct moi = new DThuocct();
                                                moi.MaDV = cd2.MaDV;
                                                moi.IDDon = iddthuoc;
                                                moi.IDKB = _idkb;
                                                moi.DonVi = cd2.DonVi;
                                                moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                                                moi.IDCD = cd2.IDCD;
                                                moi.DonGia = _dongia;
                                                moi.MaCB = cd2.MaCBth;
                                                moi.DSCBTH = cd2.DSCBTH;
                                                moi.MaKP = makp;
                                                moi.ThanhTien = _dongia * tyleTT / 100;
                                                moi.NgayNhap = cd2.NgayTH;
                                                moi.SoLuong = 1;
                                                if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                                    moi.ThanhToan = 1;
                                                moi.TyLeTT = tyleTT;
                                                moi.IDCLS = IdCLSNew;
                                                data.DThuoccts.Add(moi);
                                                data.SaveChanges();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DThuoc dthuoccd = new DThuoc();
                                        dthuoccd.NgayKe = cdinh.FirstOrDefault().NgayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                                        dthuoccd.MaBNhan = _mabn;
                                        dthuoccd.MaKP = cdinh.First().MaKP;
                                        dthuoccd.MaCB = cdinh.First().MaCB;
                                        dthuoccd.PLDV = 2;
                                        dthuoccd.KieuDon = -1;
                                        data.DThuocs.Add(dthuoccd);
                                        if (data.SaveChanges() >= 0)
                                        {
                                            var maxid = data.DThuocs.Where(p => p.MaBNhan == _mabn).ToList().Max(p => p.IDDon);
                                            foreach (var cd3 in cdinh)
                                            {
                                                double _dongia = DungChung.Ham._getGiaDM(data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, _mabn, cd3.NgayTH == null ? DateTime.Now : cd3.NgayTH.Value);
                                                DThuocct moi = new DThuocct();
                                                moi.MaDV = cd3.MaDV;
                                                moi.IDDon = maxid;
                                                moi.IDKB = _idkb;
                                                //var BH = (from Dv in _db.DichVus.Where(p => p.MaDV == cd3.MaDV) select new { Dv.TrongDM }).ToList();
                                                //if (BH.Count > 0)
                                                //{
                                                //    moi.TrongBH = BH.First().TrongDM;
                                                //}
                                                moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;

                                                moi.MaCB = cd3.MaCBth;

                                                moi.DSCBTH = cd3.DSCBTH;
                                                moi.IDKB = _idkb;
                                                moi.NgayNhap = cd3.NgayTH;
                                                moi.MaKP = makp;
                                                moi.IDCD = cd3.IDCD;
                                                moi.DonVi = cd3.DonVi;
                                                moi.DonGia = _dongia;
                                                moi.ThanhTien = _dongia * tyleTT / 100;
                                                moi.SoLuong = 1;
                                                if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                                    moi.ThanhToan = 1;
                                                moi.TyLeTT = tyleTT;
                                                moi.IDCLS = IdCLSNew;
                                                data.DThuoccts.Add(moi);
                                                data.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //ko sao kết quả
                        if (SaoNhieu)
                        {
                            int x = 0;
                            x = (dateNgayCD1.DateTime - dateNgayCD.DateTime).Days;
                            if (x == 0)
                            {
                                foreach (var item in _madv)
                                {
                                    int IdCLS = item.IdCLS;
                                    CL moicls = new CL();
                                    moicls.MaBNhan = item.MaBNhan;
                                    moicls.MaCB = item.MaCB;
                                    moicls.MaKP = item.MaKP;
                                    moicls.MaKPth = item.MaKPth;
                                    moicls.NgayThang = dateNgayCD.DateTime;
                                    moicls.CapCuu = item.CapCuu;
                                    moicls.Status = 0;
                                    moicls.ChanDoan = item.ChanDoan;
                                    moicls.MaICD = item.MaICD;
                                    data.CLS.Add(moicls);
                                    data.SaveChanges();
                                    int IdCLSNew = moicls.IdCLS;
                                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                                    foreach (var items in chidinh)
                                    {
                                        int idcd = items.IDCD;
                                        ChiDinh themmoiCD = new ChiDinh();
                                        themmoiCD.IdCLS = IdCLSNew;
                                        themmoiCD.MaDV = items.MaDV;
                                        themmoiCD.Status = 0;
                                        themmoiCD.DonGia = items.DonGia;
                                        themmoiCD.TrongBH = items.TrongBH;
                                        themmoiCD.ChiDinh1 = items.ChiDinh1;
                                        themmoiCD.XHH = 0;
                                        themmoiCD.NgayBDTH = moicls.NgayThang;


                                        data.ChiDinhs.Add(themmoiCD);
                                        if (data.SaveChanges() >= 0)
                                        {
                                            int idCDnew = themmoiCD.IDCD;
                                            var clsct = data.CLScts.Where(p => p.IDCD == idcd).ToList();
                                            foreach (var item2 in clsct)
                                            {
                                                CLSct themmoiCL = new CLSct();
                                                themmoiCL.IDCD = idCDnew;
                                                themmoiCL.MaDVct = item2.MaDVct;
                                                themmoiCL.Status = 0;
                                                data.CLScts.Add(themmoiCL);
                                                data.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            else if (x > 0)
                            {
                                for (int i = 0; i <= x; i++)
                                {
                                    foreach (var item in _madv)
                                    {
                                        int IdCLS = item.IdCLS;
                                        CL moicls = new CL();
                                        moicls.MaBNhan = item.MaBNhan;
                                        moicls.MaCB = item.MaCB;
                                        moicls.MaKP = item.MaKP;
                                        moicls.MaKPth = item.MaKPth;
                                        moicls.NgayThang = dateNgayCD.DateTime.AddDays(i);
                                        moicls.CapCuu = item.CapCuu;
                                        moicls.Status = 0;
                                        moicls.ChanDoan = item.ChanDoan;
                                        moicls.MaICD = item.MaICD;
                                        data.CLS.Add(moicls);
                                        data.SaveChanges();
                                        int IdCLSNew = moicls.IdCLS;
                                        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                                        foreach (var items in chidinh)
                                        {
                                            int idcd = items.IDCD;
                                            ChiDinh themmoiCD = new ChiDinh();
                                            themmoiCD.IdCLS = IdCLSNew;
                                            themmoiCD.MaDV = items.MaDV;
                                            themmoiCD.Status = 0;
                                            themmoiCD.DonGia = items.DonGia;
                                            themmoiCD.TrongBH = items.TrongBH;
                                            themmoiCD.ChiDinh1 = items.ChiDinh1;
                                            themmoiCD.XHH = 0;
                                            data.ChiDinhs.Add(themmoiCD);
                                            if (data.SaveChanges() >= 0)
                                            {
                                                int idCDnew = themmoiCD.IDCD;
                                                var clsct = data.CLScts.Where(p => p.IDCD == idcd).ToList();
                                                foreach (var item2 in clsct)
                                                {
                                                    CLSct themmoiCL = new CLSct();
                                                    themmoiCL.IDCD = idCDnew;
                                                    themmoiCL.MaDVct = item2.MaDVct;
                                                    themmoiCL.Status = 0;
                                                    data.CLScts.Add(themmoiCL);
                                                    data.SaveChanges();
                                                }
                                            }
                                        }

                                        if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                        {
                                            var ngayDB = moicls.NgayThang ?? DateTime.Now;
                                            DungChung.Ham.Update_CLS_DienBienct(moicls.MaBNhan ?? 0, ngayDB.Date, moicls.MaKP);
                                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                            var dienBien = data.DienBiens.FirstOrDefault(o => o.MaBNhan == (moicls.MaBNhan ?? 0) && o.NgayNhap.Value.Day == ngayDB.Day && o.NgayNhap.Value.Month == ngayDB.Month && o.NgayNhap.Value.Year == ngayDB.Year && o.Loai == 1 && o.MaKP == moicls.MaKP);
                                            if (dienBien != null)
                                            {
                                                dienBien.YLenh = "";
                                                dienBien.MaKP = moicls.MaKP;
                                                data.SaveChanges();
                                                DungChung.Ham.Update_DienBien_All(dienBien.ID, moicls.MaKP);
                                            }
                                            else
                                            {
                                                DienBien dienBienNew = new DienBien();
                                                dienBienNew.NgayNhap = ngayDB;
                                                dienBienNew.YLenh = "";
                                                dienBienNew.DienBien1 = "";
                                                dienBienNew.MaCB = DungChung.Bien.MaCB;
                                                dienBienNew.Loai = 1;
                                                dienBienNew.MaKP = moicls.MaKP;
                                                dienBienNew.Ploai = 0;
                                                dienBienNew.MaBNhan = moicls.MaBNhan;
                                                data.DienBiens.Add(dienBienNew);
                                                data.SaveChanges();

                                                DungChung.Ham.Update_DienBien_All(dienBienNew.ID, moicls.MaKP);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in _madv)
                            {
                                int IdCLS = item.IdCLS;
                                CL moicls = new CL();
                                moicls.MaBNhan = item.MaBNhan;
                                moicls.MaCB = item.MaCB;
                                moicls.MaKP = item.MaKP;
                                moicls.MaKPth = item.MaKPth;
                                moicls.NgayThang = dateNgayCD.DateTime;
                                moicls.CapCuu = item.CapCuu;
                                moicls.Status = 0;
                                moicls.ChanDoan = item.ChanDoan;
                                moicls.MaICD = item.MaICD;
                                data.CLS.Add(moicls);
                                data.SaveChanges();
                                int IdCLSNew = moicls.IdCLS;
                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var chidinh = data.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                                foreach (var items in chidinh)
                                {
                                    int idcd = items.IDCD;
                                    ChiDinh themmoiCD = new ChiDinh();
                                    themmoiCD.IdCLS = IdCLSNew;
                                    themmoiCD.MaDV = items.MaDV;
                                    themmoiCD.Status = 0;
                                    themmoiCD.DonGia = items.DonGia;
                                    themmoiCD.TrongBH = items.TrongBH;
                                    themmoiCD.ChiDinh1 = items.ChiDinh1;
                                    themmoiCD.XHH = 0;
                                    data.ChiDinhs.Add(themmoiCD);
                                    if (data.SaveChanges() >= 0)
                                    {
                                        int idCDnew = themmoiCD.IDCD;
                                        var clsct = data.CLScts.Where(p => p.IDCD == idcd).ToList();
                                        foreach (var item2 in clsct)
                                        {
                                            CLSct themmoiCL = new CLSct();
                                            themmoiCL.IDCD = idCDnew;
                                            themmoiCL.MaDVct = item2.MaDVct;
                                            themmoiCL.Status = 0;
                                            data.CLScts.Add(themmoiCL);
                                            data.SaveChanges();
                                        }
                                    }
                                }

                                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                {
                                    var ngayDB = moicls.NgayThang ?? DateTime.Now;
                                    DungChung.Ham.Update_CLS_DienBienct(moicls.MaBNhan ?? 0, ngayDB.Date, moicls.MaKP);
                                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var dienBien = data.DienBiens.FirstOrDefault(o => o.MaBNhan == (moicls.MaBNhan ?? 0) && o.NgayNhap.Value.Day == ngayDB.Day && o.NgayNhap.Value.Month == ngayDB.Month && o.NgayNhap.Value.Year == ngayDB.Year && o.Loai == 1 && o.MaKP == moicls.MaKP);
                                    if (dienBien != null)
                                    {
                                        dienBien.YLenh = "";
                                        dienBien.MaKP = moicls.MaKP;
                                        data.SaveChanges();
                                        DungChung.Ham.Update_DienBien_All(dienBien.ID, moicls.MaKP);
                                    }
                                    else
                                    {
                                        DienBien dienBienNew = new DienBien();
                                        dienBienNew.NgayNhap = ngayDB;
                                        dienBienNew.YLenh = "";
                                        dienBienNew.DienBien1 = "";
                                        dienBienNew.MaCB = DungChung.Bien.MaCB;
                                        dienBienNew.Loai = 1;
                                        dienBienNew.MaKP = moicls.MaKP;
                                        dienBienNew.Ploai = 0;
                                        dienBienNew.MaBNhan = moicls.MaBNhan;
                                        data.DienBiens.Add(dienBienNew);
                                        data.SaveChanges();

                                        DungChung.Ham.Update_DienBien_All(dienBienNew.ID, moicls.MaKP);
                                    }
                                }
                            }
                        }
                    }
                    MessageBox.Show("Sao chỉ định thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Lưu Mới: " + ex.ToString());
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lupKPhong_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSDichVu();
        }
        static bool check(CL cls, DateTime dtNgayCD, DateTime dtNgayCD1, DateTime dtNgayKQ, int mabn, bool chkSaoKQ, bool chkSaoNhieu)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ngaykham = _db.BNKBs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDKB).ToList();
            if (ngaykham.Count > 0)
            {
                if (ngaykham.First().NgayKham != null)
                {
                    if ((dtNgayCD < ngaykham.First().NgayKham.Value))
                    {
                        MessageBox.Show("Ngày chỉ định phải > ngày khám bệnh");
                        return false;
                    }
                    if (chkSaoNhieu && (dtNgayCD1 < ngaykham.First().NgayKham.Value))
                    {
                        MessageBox.Show("Ngày chỉ định phải > ngày khám bệnh");

                        return false;
                    }
                }
                var chandoan = ngaykham.FirstOrDefault(p => p.MaKP == cls.MaKP || (DungChung.Bien.MaBV == "14017" ? (p.MaKPDTKH == cls.MaKP) : false));
                if (chandoan == null)
                {
                    MessageBox.Show("Chưa có chẩn đoán tại khoa|phòng của bạn");
                    return false;
                }

            }

            if (DungChung.Bien.MaBV != "14017")
            {
                //if(DungChung.Bien.CapDo<9)
                if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                    if (cls.MaKP != DungChung.Bien.MaKP)
                    {
                        MessageBox.Show("Không thể sao chỉ định của khoa|phòng khác");
                        return false;
                    }
            }
            var rv = _db.RaViens.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            if (rv != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện!");
                return false;
            }
            if (chkSaoKQ)
                if ((dtNgayKQ <= dtNgayCD) && (DungChung.Bien.MaBV != "24012"))
                {
                    MessageBox.Show("Ngày thực hiện phải > ngày chỉ định");
                    return false;
                }


            if (cls == null || dtNgayCD < cls.NgayThang)
            {
                MessageBox.Show("Ngày sao chỉ định phải > ngày đã chỉ định");
                return false;
            }
            if (chkSaoNhieu)
            {
                if (dtNgayCD > dtNgayCD1)
                {
                    MessageBox.Show("Ngày sao chỉ định đến phải > ngày sao chỉ định từ");
                    return false;
                }
            }
            return true;
        }

        private void chkSaoNhieu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSaoNhieu.Checked == true)
            {
                //ckcSaoKQ.Checked = false;
                ckcSaoKQ.Visible = true;
                labelControl8.Visible = true;
                dateNgayCD1.Visible = true;
                dateNgayCD1.DateTime = DateTime.Now.AddDays(1).AddMinutes(5);
                labelControl4.Text = "Sao từ:";
                labelControl8.Text = "Đến: ";
                dateNgayCD.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
                dateNgayCD1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
                dateNgayCD.Properties.EditFormat.FormatString = "dd/MM/yyyy";
                dateNgayCD1.Properties.EditFormat.FormatString = "dd/MM/yyyy";
                dateNgayCD.Properties.Mask.EditMask = "dd/MM/yyyy";
                dateNgayCD1.Properties.Mask.EditMask = "dd/MM/yyyy";
                if (chkSaoNhieu.Checked == true && ckcSaoKQ.Checked == true)
                {
                    labelControl5.Visible = false;
                    dateNgayTH.Visible = false;
                }
            }
            else
            {
                if (cbotrangthai.SelectedIndex == 1)
                    ckcSaoKQ.Visible = true;
                else
                    ckcSaoKQ.Visible = false;
                labelControl8.Visible = false;
                dateNgayCD1.Visible = false;
                labelControl4.Text = "Ngày CĐ: ";
                dateNgayCD.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                dateNgayCD.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
                dateNgayCD.Properties.Mask.EditMask = "g";
            }
        }

        private void frm_SaoChiDinh_FormClosed(object sender, FormClosedEventArgs e)
        {
            reloaddata();
        }

        private void grcDichVu_Click(object sender, EventArgs e)
        {

        }
    }
}