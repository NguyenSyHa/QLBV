using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace QLBV.CLS
{
    public partial class frm_Execute_Multi_PHCN : Form
    {
        PHCNADO phcnADO;
        public frm_Execute_Multi_PHCN(PHCNADO _ado)
        {
            InitializeComponent();
            phcnADO = _ado;
        }

        private void frm_Execute_Multi_PHCN_Load(object sender, EventArgs e)
        {
            LoadComboExecuteCB(phcnADO.MaKP);
            dtExecuteTime.DateTime = DateTime.Now;
            dtChonNgayChiDinh.DateTime = phcnADO.FromTime;
        }

        private void LoadComboExecuteCB(int _maKP)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in dataContext.CanBoes.Where(p => p.Status == 1 && p.MaCCHN != "" && p.MaCCHN != null).Where(p => p.MaKPsd.Contains(_makp))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            cboCBExecute.Properties.DataSource = c;
        }

        private void LoadDichVu(PHCNADO ado, DateTime ngayChiDinh)
        {
            var ngayTu = DungChung.Ham.NgayTu(ado.FromTime);
            var ngayDen = DungChung.Ham.NgayDen(ado.ToTime);
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dichVus = (from bn in dataContext.BenhNhans
                           join cls in dataContext.CLS.Where(p => (ado.Is_Execute_Cls ? (p.MaKP == ado.MaKP) : (p.MaKPth == ado.MaKP)) && p.NgayThang.Value.Year == ngayChiDinh.Year && p.NgayThang.Value.Month == ngayChiDinh.Month && p.NgayThang.Value.Day == ngayChiDinh.Day) on bn.MaBNhan equals cls.MaBNhan
                           join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in dataContext.DichVus.Where(o => ado.Is_Execute_Cls ? (o.IS_EXECUTE_CLS == true) : (o.IS_EXECUTE_CLS == null || o.IS_EXECUTE_CLS == false)) on cd.MaDV equals dv.MaDV
                           where (cd.Status == 0 || cd.Status == null)
                           select dv).Distinct().ToList();
            gridControlDichVu.DataSource = dichVus;
        }

        public class PHCNADO
        {
            public int MaKP { get; set; }
            public DateTime FromTime { get; set; }
            public DateTime ToTime { get; set; }
            public int? NoiTru { get; set; }
            public bool Is_Execute_Cls { get; set; }
        }

        private void gridViewDichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (DichVu)gridViewDichVu.GetRow(e.FocusedRowHandle);
            if (row != null)
            {
                LoadBenhNhanByDichVu(row, phcnADO);
            }
        }

        private void LoadBenhNhanByDichVu(DichVu dichVu, PHCNADO ado)
        {
            var ngayTu = DungChung.Ham.NgayTu(ado.FromTime);
            var ngayDen = DungChung.Ham.NgayDen(ado.ToTime);
            var ngayChiDinh = dtChonNgayChiDinh.DateTime;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dichVus = (from bn in dataContext.BenhNhans
                           join cls in dataContext.CLS.Where(p => (ado.Is_Execute_Cls ? (p.MaKP == ado.MaKP) : (p.MaKPth == ado.MaKP)) && p.NgayThang.Value.Year == ngayChiDinh.Year && p.NgayThang.Value.Month == ngayChiDinh.Month && p.NgayThang.Value.Day == ngayChiDinh.Day) on bn.MaBNhan equals cls.MaBNhan
                           join cd in dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in dataContext.DichVus.Where(o => ado.Is_Execute_Cls ? (o.IS_EXECUTE_CLS == true) : (o.IS_EXECUTE_CLS == null || o.IS_EXECUTE_CLS == false)) on cd.MaDV equals dv.MaDV
                           where (cd.Status == 0 || cd.Status == null)
                           where dv.MaDV == dichVu.MaDV
                           select new BenhNhanADO { MaBNhan = bn.MaBNhan, TenBNhan = bn.TenBNhan, MaDV = cd.MaDV, DChi = bn.DChi, DTuong = bn.DTuong, Tuoi = bn.Tuoi, GTinh = bn.GTinh }).Distinct().ToList();
            gridControlBenhNhan.DataSource = dichVus;
        }

        private class BenhNhanADO
        {
            public int MaBNhan { get; set; }
            public string TenBNhan { get; set; }
            public int? GTinh { get; set; }
            public int? Tuoi { get; set; }
            public string DTuong { get; set; }
            public string DChi { get; set; }
            public int? MaDV { get; set; }
        }

        private void gridViewBenhNhan_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var row = (int)gridViewBenhNhan.GetListSourceRowCellValue(e.ListSourceRowIndex, "GTinh");
                if (row != null)
                {
                    if (e.Column.FieldName == "GioiTinh")
                        e.Value = row == 0 ? "Nữ" : "Nam";
                    else if (e.Column.FieldName == "STT")
                        e.Value = e.ListSourceRowIndex + 1;
                }
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var checks = gridViewBenhNhan.GetSelectedRows();
            if (checks.Count() == 0)
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
                return;
            }
            if (cboCBExecute.EditValue == null)
            {
                MessageBox.Show("Bạn phải nhập bác sỹ thực hiện");
                return;
            }
            if (dtExecuteTime.EditValue == null)
            {
                MessageBox.Show("Bạn phải nhập thời gian thực hiện");
                return;
            }

            List<string> listError = new List<string>();

            if (dtExecuteTime.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày thực hiện không được lớn hơn ngày hiện tại");
                return;
            }

            bool success = false;
            foreach (var item in checks)
            {
                BenhNhanADO ado = (BenhNhanADO)gridViewBenhNhan.GetRow(item);
                if (ado != null)
                {
                    var vp = (from vpct in dataContext.VienPhis.Where(p => p.MaBNhan == ado.MaBNhan) select new { vpct.idVPhi, vpct.NgayTT }).ToList();

                    if (vp.Count > 0)
                    {
                        listError.Add(string.Format("Bệnh nhân: {0} đã thanh toán không thể sửa!", ado.TenBNhan));
                        continue;
                    }
                    var ktRaVien = dataContext.RaViens.Where(p => p.MaBNhan == ado.MaBNhan).ToList();
                    if (ktRaVien.Count > 0)
                    {
                        listError.Add(string.Format("Bệnh nhân: {0} đã ra viện, bạn không thể lưu kết quả", ado.TenBNhan));
                        continue;
                    }
                    var ngayChiDinh = dtChonNgayChiDinh.DateTime;

                    var chiDinhs = (from cls in dataContext.CLS.Where(o => o.MaBNhan == ado.MaBNhan && o.NgayThang.Value.Year == ngayChiDinh.Year && o.NgayThang.Value.Month == ngayChiDinh.Month && o.NgayThang.Value.Day == ngayChiDinh.Day)
                                    join cd in dataContext.ChiDinhs.Where(o => o.MaDV == ado.MaDV) on cls.IdCLS equals cd.IdCLS
                                    join dv in dataContext.DichVus on cd.MaDV equals dv.MaDV
                                    select new { cls, cd, dv.TenDV }).ToList();
                    foreach (var subItem in chiDinhs)
                    {
                        if (subItem.cls.NgayThang > dtExecuteTime.DateTime)
                        {
                            listError.Add(string.Format("Bệnh nhân: {0} có dịch vụ: {1} có ngày chỉ định lớn hơn ngày thực hiện", ado.TenBNhan, subItem.TenDV));
                            break;
                        }
                        bool KtraBNKSK = false;
                        #region Update CLS
                        var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == ado.MaDV);
                        var suacls = dataContext.CLS.Single(p => p.IdCLS == subItem.cls.IdCLS);
                        suacls.MaCBth = cboCBExecute.EditValue.ToString();
                        //A quý bảo sau 10 phút
                        suacls.NgayTH = dtExecuteTime.DateTime;
                        suacls.DSCBTH = suacls.MaCBth;
                        suacls.MaKPth = phcnADO.MaKP;

                        var ktstatuscd = dataContext.ChiDinhs.Where(p => p.IdCLS == subItem.cls.IdCLS && p.IDCD != subItem.cd.IDCD).Where(p => p.Status == 0 || p.Status == null).ToList();
                        if (ktstatuscd.Count > 0)
                            suacls.Status = 0;
                        else
                        {
                            suacls.Status = 1;
                            BenhNhan sua = dataContext.BenhNhans.Where(p => p.MaBNhan == ado.MaBNhan).FirstOrDefault();
                            if (sua != null)
                            {
                                var b = dataContext.BNKBs.Where(p => p.MaBNhan == ado.MaBNhan).ToList();
                                var vienphi = dataContext.VienPhis.Where(p => p.MaBNhan == ado.MaBNhan).ToList();
                                if (b.Count > 0 && vienphi.Count <= 0 && sua.Status != 2 && sua.Status != 3)
                                {
                                    sua.Status = 5;
                                }
                                if (sua.IDDTBN == 3 && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297"))
                                    KtraBNKSK = true;
                            }
                        }
                        #endregion

                        #region Update ChiDinh
                        int ID = subItem.cd.IDCD;
                        var suacd = dataContext.ChiDinhs.Single(p => p.IDCD == ID);
                        suacd.Status = 1;
                        suacd.NgayTH = dtExecuteTime.DateTime;
                        suacd.MaCBth = cboCBExecute.EditValue.ToString();
                        #endregion

                        #region Update CLSct
                        var _CLSct = dataContext.CLScts.Where(o => o.IDCD == ID).ToList();
                        foreach (var c in _CLSct)
                        {
                            var suaclsct = dataContext.CLScts.FirstOrDefault(p => p.Id == c.Id);
                            suaclsct.Status = 1;
                        }
                        #endregion

                        int? makp = suacls.MaKPth;
                        int IdCLS = subItem.cls.IdCLS;
                        int iddthuoc = 0;
                        var ktdthuoc = dataContext.DThuocs.Where(p => p.MaBNhan == ado.MaBNhan).Where(p => p.PLDV == 2).ToList();
                        if (ktdthuoc.Count > 0)
                            iddthuoc = ktdthuoc.First().IDDon;

                        var dichVu = dataContext.DichVus.FirstOrDefault(o => o.MaDV == suacd.MaDV);
                        List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                        if (KtraBNKSK == true)
                        {
                            var _lThuTT = dataContext.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == ado.MaBNhan).Select(p => p.IDGoiDV ?? 0).ToList();
                            dsIDGOiDV.AddRange(_lThuTT);
                        }
                        if (iddthuoc > 0)
                        {
                            var kt = (from dt in dataContext.DThuoccts.Where(p => p.IDCD == suacd.IDCD) select dt).ToList();
                            if (kt.Count <= 0)
                            {
                                double _dongia = DungChung.Ham._getGiaDM(dataContext, suacd.MaDV == null ? 0 : suacd.MaDV.Value, suacd.TrongBH == null ? 1 : suacd.TrongBH.Value, ado.MaBNhan, suacls.NgayTH ?? DateTime.Now);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = suacd.MaDV;
                                moi.IDDon = iddthuoc;
                                moi.DonVi = dichVu != null ? dichVu.DonVi : "";
                                moi.TrongBH = suacd.TrongBH == null ? 0 : suacd.TrongBH.Value;
                                moi.IDCD = suacd.IDCD;
                                moi.DonGia = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                                moi.XHH = suacd.XHH;
                                moi.LoaiDV = suacd.LoaiDV;
                                moi.MaCB = suacls.MaCBth;
                                moi.MaKP = makp;
                                moi.ThanhTien = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                                moi.NgayNhap = suacls.NgayTH;
                                moi.SoLuong = 1;
                                moi.Status = 0;
                                if (KtraBNKSK == true && suacd.IDGoi != null && dsIDGOiDV.Where(p => p == suacd.IDGoi).Count() > 0)
                                {
                                    moi.ThanhToan = 1;
                                }
                                else if (suacd.SoPhieu != null && suacd.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = IdCLS;
                                dataContext.DThuoccts.Add(moi);
                                dataContext.SaveChanges();
                                var CheckGiaPhuThu = dataContext.DichVus.Where(p => p.MaDV == suacd.MaDV).FirstOrDefault();
                                var sss = dataContext.BenhNhans.Where(p => p.MaBNhan == suacls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(dataContext, moi.IDDonct, s);
                                }
                            }
                            else
                            {
                                foreach (var dt in kt)
                                {
                                    dt.NgayNhap = suacls.NgayTH;
                                    dt.IDCLS = IdCLS;
                                }
                                dataContext.SaveChanges();
                            }
                        }
                        else
                        {
                            DThuoc dthuoccd = new DThuoc();
                            dthuoccd.NgayKe = suacls.NgayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                            dthuoccd.MaBNhan = suacls.MaBNhan;
                            dthuoccd.MaKP = suacls.MaKP;
                            dthuoccd.MaCB = suacls.MaCB;
                            dthuoccd.PLDV = 2;
                            dthuoccd.KieuDon = -1;
                            dataContext.DThuocs.Add(dthuoccd);
                            if (dataContext.SaveChanges() >= 0)
                            {
                                int maxid = dthuoccd.IDDon;
                                double _dongia = DungChung.Ham._getGiaDM(dataContext, suacd.MaDV == null ? 0 : suacd.MaDV.Value, suacd.TrongBH == null ? 1 : suacd.TrongBH.Value, ado.MaBNhan, suacls.NgayTH ?? DateTime.Now);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = suacd.MaDV;
                                moi.IDDon = maxid;
                                moi.TrongBH = suacd.TrongBH == null ? 0 : suacd.TrongBH.Value;
                                moi.MaCB = suacls.MaCB;
                                moi.NgayNhap = suacls.NgayTH;
                                moi.MaKP = makp;
                                moi.IDCD = suacd.IDCD;
                                moi.DonVi = dichVu != null ? dichVu.DonVi : "";
                                moi.XHH = suacd.XHH;
                                moi.DonGia = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                                moi.ThanhTien = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                                moi.SoLuong = 1;
                                moi.Status = 0;
                                moi.LoaiDV = suacd.LoaiDV;
                                if (KtraBNKSK == true && suacd.IDGoi != null && dsIDGOiDV.Where(p => p == suacd.IDGoi).Count() > 0)
                                {
                                    moi.ThanhToan = 1;
                                }
                                else if (suacd.SoPhieu != null && suacd.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = IdCLS;
                                dataContext.DThuoccts.Add(moi);
                                dataContext.SaveChanges();
                                var CheckGiaPhuThu = dataContext.DichVus.Where(p => p.MaDV == suacd.MaDV).FirstOrDefault();
                                var sss = dataContext.BenhNhans.Where(p => p.MaBNhan == suacls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(dataContext, moi.IDDonct, s);
                                }
                            }
                        }
                        success = true;
                    }
                }
            }
            if (success)
            {
                if (listError.Count > 0)
                {
                    MessageBox.Show(string.Format("Các bệnh nhân không lưu được vui lòng kiểm tra lại: {0}", Environment.NewLine + string.Join(Environment.NewLine + "- ", listError)), "Cảnh báo");
                }
                else
                    MessageBox.Show("Lưu thành công!");
                LoadDichVu(phcnADO, dtChonNgayChiDinh.DateTime);
                gridViewDichVu.FocusedRowHandle = 0;
                gridViewDichVu_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
            }
        }

        private void dtChonNgayChiDinh_EditValueChanged(object sender, EventArgs e)
        {
            LoadDichVu(phcnADO, dtChonNgayChiDinh.DateTime);
            gridViewDichVu.FocusedRowHandle = 0;
            gridViewDichVu_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }
    }
}
