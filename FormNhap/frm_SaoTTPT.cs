using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_SaoTTPT : DevExpress.XtraEditors.XtraForm
    {
        int idCLS = 0;
        public frm_SaoTTPT(int IDCLS)
        {
            InitializeComponent();
            this.idCLS = IDCLS;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSaoKQ_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSaoKQ.Checked)
            {
                dtNgayKQ.ReadOnly = true;
                cboTyLe.ReadOnly = true;
            }
            else
            {
                dtNgayKQ.ReadOnly = false;
                cboTyLe.ReadOnly = false;
            }

        }
        QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        static bool check(CL cls, DateTime dtNgayCD, DateTime dtNgayKQ, int mabn, bool chkSaoKQ)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ngaykham = _db.BNKBs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDKB).ToList();
            if (ngaykham.Count > 0)
            {
                if (ngaykham.First().NgayKham != null)
                    if ((dtNgayCD < ngaykham.First().NgayKham.Value))
                    {
                        MessageBox.Show("Ngày chỉ định > ngày khám bệnh");
                        return false;
                    }
                var chandoan = ngaykham.Where(p => p.MaKP == cls.MaKP).FirstOrDefault();
                if (chandoan == null)
                {
                    MessageBox.Show("chưa có chẩn đoán tại khoa|phòng của bạn");
                    return false;
                }

            }
            //if(DungChung.Bien.CapDo<9)
            if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                if (cls.MaKP != DungChung.Bien.MaKP)
                {
                    MessageBox.Show("không thể sao chỉ định của khoa|phòng khác");
                    return false;
                }
            var rv = _db.RaViens.Where(p => p.MaBNhan == mabn).FirstOrDefault();
            if (rv != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện!");
                return false;
            }
            if (chkSaoKQ)
                if ((dtNgayKQ <= dtNgayCD))
                {
                    MessageBox.Show("Ngày thực hiện > ngày chỉ định");
                    return false;
                }


            if (cls == null || dtNgayCD < cls.NgayThang)
            {
                MessageBox.Show("Ngày sao chỉ định phải > ngày đã chỉ định");
                return false;
            }

            return true;
        }

        List<FormThamSo.frm_kqcls.Status_CD> _lstatus = new List<FormThamSo.frm_kqcls.Status_CD>();
        public static bool CopyChiDinhCLS(int IDCLS, DateTime ngayCD, int tyle, bool SaoKQ, DateTime ngayKQ)
        {
            try
            {
                QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int makp = 0, mabn = 0;

                var ktra = (from cls in _db.CLS.Where(p => p.IdCLS == IDCLS)
                            join cd in _db.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join clsct in _db.CLScts on cd.IDCD equals clsct.IDCD
                            select new { cls, cd, clsct }).ToList();
                CL _lcls = ktra.Select(p => p.cls).FirstOrDefault();
                if (_lcls == null)
                {
                    MessageBox.Show("Không có chỉ định nào để sao!");
                    return false;
                }
                makp = _lcls.MaKP ?? 0;
                mabn = _lcls.MaBNhan ?? 0;
                if (!check(_lcls, ngayCD, ngayKQ, mabn, SaoKQ))
                    return false;
                var chidinh = ktra.Select(p => p.cd).Distinct().ToList();
                string dvdachidinh = "";
                foreach (var item in chidinh)
                {
                    DateTime ngaytu = DungChung.Ham.NgayTu(ngayCD);
                    DateTime ngayden = DungChung.Ham.NgayDen(ngayCD);
                    var ktratrongngay = (from cls in _db.CLS.Where(p => p.MaBNhan == mabn)
                                         join cd in _db.ChiDinhs.Where(p => p.MaDV == item.MaDV) on cls.IdCLS equals cd.IDCD
                                         join dv in _db.DichVus on cd.MaDV equals dv.MaDV
                                         join tn in _db.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                         where (cls.NgayThang >= ngaytu && cls.NgayThang <= ngayden)
                                         select new { dv.TenDV, tn.IDNhom }).ToList();

                    if (ktratrongngay.Count > 0)
                    {
                        if (ktratrongngay.First().IDNhom != 8)
                            SaoKQ = false;
                        dvdachidinh += ktratrongngay.First().TenDV + "; ";

                    }
                }
                if (!string.IsNullOrEmpty(dvdachidinh))
                    if (DialogResult.No == MessageBox.Show("Ngày " + ngayCD.ToShortDateString() + dvdachidinh + " đã có chỉ định, ban vẫn muốn tạo?", "hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return false;

                CL newcl = new CL();
                newcl.BarCode = _lcls.BarCode;
                newcl.BenhPham = _lcls.BenhPham;
                newcl.CapCuu = _lcls.CapCuu;
                newcl.ChanDoan = _lcls.ChanDoan;
                newcl.Code = _lcls.Code;
                newcl.GhiChu = _lcls.GhiChu;
                newcl.IDBB = _lcls.IDBB;
                newcl.MaBNhan = _lcls.MaBNhan;
                newcl.MaCB = _lcls.MaCB;
                newcl.MaICD = _lcls.MaICD;
                newcl.MaKP = _lcls.MaKP;
                newcl.MaKPth = _lcls.MaKPth;

                newcl.NgayThang = ngayCD;

                newcl.STT = _lcls.STT;

                if (SaoKQ)
                {
                    newcl.DSCBTH = _lcls.DSCBTH;
                    newcl.MaCBth = _lcls.MaCBth;
                    newcl.NgayKQ = ngayKQ;
                    newcl.NgayTH = ngayKQ;
                    newcl.Status = _lcls.Status;
                    newcl.NgayKQ = ngayKQ;
                }
                else
                {

                    newcl.Status = 0;
                }

                //newcl.ThoiGianLayMau = _lcls.ThoiGianLayMau;
                //newcl.ThoiGianNhanMau = _lcls.ThoiGianNhanMau;
                //newcl.TrangThaiBN = _lcls.TrangThaiBN;
                //newcl.TrangThaiBP = _lcls.TrangThaiBP;
                _db.CLS.Add(newcl);
                if (_db.SaveChanges() >= 0)
                {
                    int idCLS = newcl.IdCLS;

                    int idCD = 0;
                    foreach (var item in chidinh)
                    {
                        ChiDinh themmoiCD = new ChiDinh();
                        themmoiCD.IdCLS = idCLS;
                        themmoiCD.MaDV = item.MaDV;
                        if (SaoKQ && item.Status == 1)
                        {
                            themmoiCD.Status = 1;
                            themmoiCD.KetLuan = item.KetLuan;
                            themmoiCD.LoiDan = item.LoiDan;
                            themmoiCD.GhiChu = item.GhiChu;

                        }
                        else
                            themmoiCD.Status = 0;
                        themmoiCD.DonGia = item.DonGia;
                        themmoiCD.TrongBH = item.TrongBH;
                        themmoiCD.ChiDinh1 = item.ChiDinh1;
                        themmoiCD.XHH = 0;
                        _db.ChiDinhs.Add(themmoiCD);
                        if (_db.SaveChanges() >= 0)
                        {
                            idCD = themmoiCD.IDCD;
                            var clsct = ktra.Where(p=>p.clsct.IDCD==item.IDCD).Select(p => p.clsct).ToList();
                            foreach (var item2 in clsct)
                            {


                                CLSct themmoiCL = new CLSct();
                                themmoiCL.IDCD = idCD;
                                themmoiCL.MaDVct = item2.MaDVct;
                                if (SaoKQ && item.Status == 1)
                                {
                                    themmoiCL.KetQua = item2.KetQua;
                                    themmoiCL.Status = 1;
                                }
                                else
                                {
                                    themmoiCL.Status = 0;
                                }

                                //themmoiCL.STTHT=
                                _db.CLScts.Add(themmoiCL);
                                _db.SaveChanges();
                            }
                        }
                  
                    }
                 
                
                    if (SaoKQ)
                    {
                        int tyleTT = tyle;
                        //Convert.ToInt32(txtt)
                        int _idkb = 0;
                        var bnkb = _db.BNKBs.Where(p => p.MaBNhan == mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                        if (bnkb.Count > 0)
                            _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                        int iddthuoc = 0;
                        var cdinh = (from cl in _db.CLS.Where(p => p.IdCLS == idCLS)
                                     join cd1 in _db.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals cd1.IdCLS
                                     join dv in _db.DichVus on cd1.MaDV equals dv.MaDV
                                     select new { cl.NgayTH, cl.MaCBth, cl.DSCBTH, cd1.SoPhieu, cl.MaKP, cl.MaCB, cd1.MaDV, dv.DonGia, dv.DonGia2, cd1.IDCD, dv.DonVi, cd1.TrongBH }).ToList();
                        //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                        var ktdthuoc = _db.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 2).ToList();
                        if (ktdthuoc.Count > 0)
                            iddthuoc = ktdthuoc.First().IDDon;
                        if (iddthuoc > 0)
                        {
                            int Tontai = 0;
                            foreach (var d in cdinh)
                            {
                                var kt = (from dt in _db.DThuoccts.Where(p => p.IDCD == d.IDCD) select new { dt.IDDonct }).ToList();
                                if (kt.Count > 0)
                                { Tontai = Tontai + 1; }
                            }
                            if (Tontai == 0)
                            {

                                foreach (var cd2 in cdinh)
                                {
                                    double _dongia = DungChung.Ham._getGiaDM(_db, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, mabn, cd2.NgayTH == null ? DateTime.Now : cd2.NgayTH.Value);
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
                                    moi.IDCLS = idCLS;
                                    _db.DThuoccts.Add(moi);
                                    _db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            DThuoc dthuoccd = new DThuoc();
                            dthuoccd.NgayKe = cdinh.FirstOrDefault().NgayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                            dthuoccd.MaBNhan = mabn;
                            dthuoccd.MaKP = cdinh.First().MaKP;
                            dthuoccd.MaCB = cdinh.First().MaCB;
                            dthuoccd.PLDV = 2;
                            dthuoccd.KieuDon = -1;
                            _db.DThuocs.Add(dthuoccd);
                            if (_db.SaveChanges() >= 0)
                            {
                                var maxid = _db.DThuocs.Where(p => p.MaBNhan == mabn).ToList().Max(p => p.IDDon);
                                foreach (var cd3 in cdinh)
                                {
                                    double _dongia = DungChung.Ham._getGiaDM(_db, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, mabn, cd3.NgayTH == null ? DateTime.Now : cd3.NgayTH.Value);
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
                                    moi.IDCLS = idCLS;
                                    _db.DThuoccts.Add(moi);
                                    _db.SaveChanges();
                                }
                            }
                        }

                    }
                    //MessageBox.Show("Sao thành công");

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.InnerException + ex.Data + ex.Message);
                return false;
            }

        }
        private void btnSao_Click(object sender, EventArgs e)
        {


            bool a = true;


            a = frm_SaoTTPT.CopyChiDinhCLS(idCLS, dtNgayCD.DateTime, Convert.ToInt32(cboTyLe.Text), chkSaoKQ.Checked, dtNgayKQ.DateTime);
            if (!a)
            {
                MessageBox.Show("Lỗi sao chỉ định");
            }
            else
            {
                MessageBox.Show("Sao thành công");
                this.Dispose();
            }



        }

        private void cboTyLe_SelectedValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(cboTyLe.Text))
            {
                int tyle = Convert.ToInt32(cboTyLe.Text);
                if (tyle < 0 || tyle > 100)
                {
                    MessageBox.Show("Tỷ lệ không hợp lệ!");
                    cboTyLe.Focus();
                }
            }
        }
        List<FormThamSo.frm_kqcls.ListCLS> _lCLS = new List<FormThamSo.frm_kqcls.ListCLS>();

        private void frm_SaoTTPT_Load(object sender, EventArgs e)
        {
            _lstatus.Add(new FormThamSo.frm_kqcls.Status_CD { Ten = "Chưa H.Thành", Status = 0 });
            _lstatus.Add(new FormThamSo.frm_kqcls.Status_CD { Ten = "Hoàn thành", Status = 1 });
            _lstatus.Add(new FormThamSo.frm_kqcls.Status_CD { Ten = "Hủy", Status = -1 });

            chkSaoKQ_CheckedChanged(sender, e);
            dtNgayCD.DateTime = DateTime.Now;
            dtNgayKQ.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
           
        }

        private void radTenRG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}