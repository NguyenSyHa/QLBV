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
using QLBV.Providers.Business.Medicines;

namespace QLBV.FormNhap
{
    public partial class frm_Check : DevExpress.XtraEditors.XtraForm
    {
        private QLBVEntities _dataContext;
        private MedicinesProvider _medicinesProvider;
        int _id = 0;
        int _mbn = 0;
        int dem = 0;
        int _status = 0;
        int _sopl = 0;
        int _makhoake = -1;
        bool _ktratutruc = false;
        // _status 1: xóa xuất dược ngoại trú
        // _status 2: update số phiếu lĩnh vào bảng donthuocct
        int[] _arr;
        // _status 3: update số phiếu lĩnh cho khoa là 0 vào bảng donthuocct, 
        // _status 4: xóa xuất dược nội trú
        //_status 6: hủy đơn của BN ngoai tru update status 2;
        //_status 7:  hủy plinh update status 2;
        //_status 8: hủy đơn BN chưa thanh toán
        int iddon;
        bool CheckMK = false;
        public frm_Check()
        {
            InitializeComponent();

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mbn"></param>
        /// <param name="sts"></param>
        /// <param name="b">them de xac dinh ham co maBNhan</param>
        public frm_Check(int id, int mbn, int sts, bool b)// _status 1
        {
            InitializeComponent();
            _id = id;
            _mbn = mbn;
            _status = sts;

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        public frm_Check(int[] ar, int sts) // _status 2
        {
            InitializeComponent();
            _arr = ar;
            _status = sts;

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        public frm_Check(int id, int sts) // _status 3
        {
            InitializeComponent();
            iddon = id;
            _status = sts;

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();

        }
        public frm_Check(int id, int sopl, int sts) // _status 4
        {
            InitializeComponent();
            iddon = id;
            _sopl = sopl;
            _status = sts;

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sopl"></param>
        /// <param name="sts"></param>
        /// <param name="makhoake"></param>
        /// <param name="ktratutruc">truyền bất kỳ</param>
        public frm_Check(int sopl, int sts, int makhoake, string ktratutruc) // _status 9
        {
            InitializeComponent();
            iddon = sopl;
            _makhoake = makhoake;
            _status = sts;
            _ktratutruc = true;

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {

            string matkhau = "";
            var mk = _dataContext.ADMINs.Where(p => p.MaCB == DungChung.Bien.MaCB).Select(p => p.MatK).ToList();
            if (mk.Count > 0)
                matkhau = mk.First();
            if (dem <= 2)
            {

                if (QLBV_Library.QLBV_Ham.MaHoa(txtMatKhau.Text) == matkhau || CheckMK) // sưa lại matkhau= txtmatKhau.text
                {
                    //try
                    //{
                    switch (_status)
                    {
                        case 1:
                            var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == _id).ToList();
                            foreach (var xoa in xoact)
                            {
                                var _xoa = _dataContext.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                                _dataContext.NhapDcts.Remove(_xoa);
                                _dataContext.SaveChanges();
                            }
                            var xoac = _dataContext.NhapDs.Single(p => p.IDNhap == (_id));
                            _dataContext.NhapDs.Remove(xoac);
                            _dataContext.SaveChanges();
                            var iddt = (_dataContext.DThuocs.Where(p => p.MaBNhan == (_mbn)).Where(p => p.PLDV == 1).Select(p => p.IDDon)).ToList();
                            int idxoa = 0;
                            if (iddt.Count > 0)
                                idxoa = iddt.First();
                            //var dthuoc = _dataContext.DThuocs.Single(p => p.IDDon== (idxoa));
                            //dthuoc.Status = 0;
                            //_dataContext.SaveChanges();
                            //}
                            //catch (Exception ex) {
                            //    MessageBox.Show("Lỗi! không xóa được: "+ex.Message);
                            //}
                            this.Dispose();
                            break;
                        case 2:

                            int _soPL = 1;
                            var maxdt = _dataContext.DThuoccts.OrderByDescending(p => p.SoPL).Select(p => p.SoPL).ToList();
                            try
                            {
                                if (maxdt.Count > 0)
                                {
                                    _soPL = maxdt.First() + 1;
                                }
                            }
                            catch (Exception)
                            {
                                _soPL = 1;
                            }
                            string _dtuong = "";

                            //foreach (var i in _arr)
                            //{
                            //    var donthuoc = _dataContext.DThuocs.Single(p => p.IDDon == i);
                            //    donthuoc.SoPL = _soPL;
                            //    _dataContext.SaveChanges();
                            //}
                            var ktdtuong = (from bn in _dataContext.BenhNhans
                                            join dt in _dataContext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                            join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                            select new { bn.DTuong }).ToList();
                            int _bh = 0;
                            int _dv = 0;
                            foreach (var a in ktdtuong)
                            {
                                if (a.DTuong == "BHYT")
                                    _bh++;
                                else
                                    _dv++;
                            }
                            if (_bh > 0 && _dv > 0)
                            {
                                _dtuong = "";
                            }
                            else
                            {
                                if (_dv > 0)
                                {
                                    _dtuong = "(Dành cho đối tượng thu phí) \n";
                                }
                                else
                                {
                                    _dtuong = "(Dành cho đối tượng BHYT) \n";
                                }
                            }
                            var ngay = (from dt in _dataContext.DThuocs
                                        join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                        select new { dt.NgayKe }).OrderBy(p => p.NgayKe).ToList();
                            string ngay1 = "";
                            string ngay2 = "";
                            if (ngay.Count > 0)
                            {
                                ngay1 = ngay.First().ToString().Substring(0, 10);
                                ngay2 = ngay.Last().ToString().Substring(0, 10);
                            }
                            frmIn frm = new frmIn();
                            var bph = (from kd in _dataContext.DThuocs
                                       join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                                       join kp in _dataContext.KPhongs
                                           on kd.MaKP equals kp.MaKP
                                       select new { kp.TenKP, kd.LoaiDuoc, kd.MaKXuat, kd.KieuDon }).ToList();
                            if (bph.Count > 0)
                            {
                                int kieudon = 0;
                                kieudon = bph.First().KieuDon.Value;
                                if (kieudon == 2)
                                {
                                    int loaiduoc = bph.First().LoaiDuoc.Value;
                                    if ((loaiduoc == 3 || loaiduoc == 4) && DungChung.Bien.MaBV != "24009")
                                    {
                                        BaoCao.PhieutrathuocGNHTT rep = new BaoCao.PhieutrathuocGNHTT();
                                        //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        var q = (from kd in _dataContext.DThuocs
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();


                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:.../BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
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
                                        var q = (from kd in _dataContext.DThuocs
                                                 join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                 join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                 group new { kdct, dv, kd } by new { kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                 select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();


                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:05D/BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
                                }
                                else
                                {

                                    var q = (from kd in _dataContext.DThuocs
                                             join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                             join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                             group new { kdct, dv, kd, tn } by new { tn.TenRG, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                             select new { kq.Key.TenRG, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();

                                    kieudon = bph.First().KieuDon.Value;
                                    int loaiduoc = bph.First().LoaiDuoc.Value;
                                    if (loaiduoc == 3 || loaiduoc == 4)
                                    {
                                        if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();

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
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            switch (loaiduoc)
                                            {
                                                case 0:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                    break;
                                                case 1:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                    break;
                                                case 2:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                    break;
                                                case 3:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                case 4:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                case 5:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                    break;
                                                case 6:
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
                                            this.Dispose();
                                        }
                                        else
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();

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
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            switch (loaiduoc)
                                            {
                                                case 0:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                    break;
                                                case 1:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                    break;
                                                case 2:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                    break;
                                                case 3:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                case 4:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    break;
                                                case 5:
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                    break;
                                                case 6:
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
                                            this.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        if (loaiduoc == 6)
                                        {
                                            if (DungChung.Bien.MaBV == "12001")
                                            {
                                                var q6 = (from bn in _dataContext.BenhNhans
                                                          join kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                          join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                          group new { kdct, dv, kd, bn } by new { kd.MaBNhan, bn.TenBNhan, bn.DChi, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                          select new
                                                          {
                                                              kq.Key.TenBNhan,
                                                              kq.Key.DChi,
                                                              TenDV = kq.Key.TenDV,
                                                              kq.Key.MaDV,
                                                              DonVi = kq.Key.DonVi,
                                                              SoLuong139 = kq.Where(p => p.bn.MaDTuong == ("DT") || p.bn.MaDTuong == ("HN") || p.bn.MaDTuong == ("DK")).Sum(p => p.kdct.SoLuong),
                                                              SoLuongTE = kq.Where(p => p.bn.MaDTuong.Contains("TE")).Sum(p => p.kdct.SoLuong),
                                                              SoLuongBHYT = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong != "DT" && p.bn.MaDTuong != "HN" && p.bn.MaDTuong != "DK" && p.bn.MaDTuong != "TE").Sum(p => p.kdct.SoLuong),
                                                              SoLuongDichVu = kq.Where(p => p.bn.DTuong == ("Dịch vụ")).Sum(p => p.kdct.SoLuong),
                                                              SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                              DonGia = kq.Key.DonGia,
                                                              LoaiDuoc = kq.Key.LoaiDuoc
                                                          }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();

                                                BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD(6);
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
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }

                                                switch (loaiduoc)
                                                {
                                                    case 0:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                        rep.MauSo.Value = "MS:01D/BV-01";
                                                        break;
                                                    case 1:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                        rep.MauSo.Value = "MS:02D/BV-01";
                                                        break;
                                                    case 2:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                        rep.MauSo.Value = "MS:03D/BV-01";
                                                        break;
                                                    case 3:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 4:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 5:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                        rep.MauSo.Value = "MS:...D/BV-01";
                                                        break;
                                                    case 6:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                        rep.MauSo.Value = "MS:...D/BV-01";
                                                        break;
                                                }

                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6
                                                              group new { dongy } by new { dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
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
                                                this.Dispose();
                                            }
                                            else
                                            {
                                                var q6 = (
                                                    from bn in _dataContext.BenhNhans
                                                    join kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    group new { kdct, dv, kd, bn } by new { bn.Tuoi, kd.MaBNhan, bn.TenBNhan, bn.DChi, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                    select new { kq.Key.TenBNhan, Tuoi = kq.Key.Tuoi, kq.Key.DChi, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();

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
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }

                                                switch (loaiduoc)
                                                {
                                                    case 0:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                        rep.MauSo.Value = "MS:01D/BV-01";
                                                        break;
                                                    case 1:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                        rep.MauSo.Value = "MS:02D/BV-01";
                                                        break;
                                                    case 2:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                        rep.MauSo.Value = "MS:03D/BV-01";
                                                        break;
                                                    case 3:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 4:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 5:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                        rep.MauSo.Value = "MS:...D/BV-01";
                                                        break;
                                                    case 6:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                        rep.MauSo.Value = "MS:...D/BV-01";
                                                        break;
                                                }

                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6
                                                              group new { dongy } by new { dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
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
                                                this.Dispose();
                                            }
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.MaBV == "12001")
                                            {
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();
                                                var qTD = (from bn in _dataContext.BenhNhans
                                                           join
                                                               kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                           join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                                           join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                           group new { bn, kdct, dv, kd } by new { kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc } into kq
                                                           select new
                                                           {
                                                               TenDV = kq.Key.TenDV,
                                                               kq.Key.MaDV,
                                                               DonVi = kq.Key.DonVi,
                                                               SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                               SoLuong139 = kq.Where(p => p.bn.MaDTuong == ("DT") || p.bn.MaDTuong == ("HN") || p.bn.MaDTuong == ("DK")).Sum(p => p.kdct.SoLuong),
                                                               SoLuongTE = kq.Where(p => p.bn.MaDTuong.Contains("TE")).Sum(p => p.kdct.SoLuong),
                                                               SoLuongBHYT = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong != "DT" && p.bn.MaDTuong != "HN" && p.bn.MaDTuong != "DK" && p.bn.MaDTuong != "TE").Sum(p => p.kdct.SoLuong),
                                                               SoLuongDichVu = kq.Where(p => p.bn.DTuong == ("Dịch vụ")).Sum(p => p.kdct.SoLuong),
                                                               DonGia = kq.Key.DonGia,
                                                               LoaiDuoc = kq.Key.LoaiDuoc
                                                           }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
                                                BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD();
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
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }


                                                switch (loaiduoc)
                                                {
                                                    case 0:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                        rep.MauSo.Value = "MS:01D/BV-01";
                                                        break;
                                                    case 1:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                        rep.MauSo.Value = "MS:02D/BV-01";
                                                        break;
                                                    case 2:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                        rep.MauSo.Value = "MS:03D/BV-01";
                                                        break;
                                                    case 3:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 4:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                        rep.MauSo.Value = "MS:08";
                                                        break;
                                                    case 5:
                                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                        rep.MauSo.Value = "MS:...D/BV-01";
                                                        break;
                                                    case 6:
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
                                                this.Dispose();
                                            }
                                            else
                                            {
                                                bool _mau2lien = false;
                                                if (DungChung.Bien.MaBV == "30009")
                                                    _mau2lien = true;
                                                if (_mau2lien)
                                                {
                                                    string a = _soPL.ToString();
                                                    BaoCao.Rep_PhieulinhThuocthuong_2Lien rep = new BaoCao.Rep_PhieulinhThuocthuong_2Lien();
                                                    frm = new frmIn();
                                                    rep.SoPL.Value = a;
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();

                                                }
                                                else
                                                {
                                                    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tekho).ToList();
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
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }


                                                    switch (loaiduoc)
                                                    {
                                                        case 0:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                            rep.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 4:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 5:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
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
                                                    this.Dispose();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                            int _soPL3 = 1;
                            var maxdt3 = _dataContext.DThuoccts.OrderByDescending(p => p.SoPL).Select(p => p.SoPL).ToList();
                            try
                            {
                                if (maxdt3.Count > 0)
                                {
                                    _soPL3 = maxdt3.First() + 1;
                                }
                            }
                            catch (Exception)
                            {
                                _soPL3 = 1;
                            }

                            //var donthuoc3 = _dataContext.DThuocs.Single(p => p.IDDon == iddon);
                            //donthuoc3.SoPL = _soPL3;
                            //_dataContext.SaveChanges();

                            //rep3.Ngaythang.Value = ngay.ToString().Substring(0, 10);
                            var TD = (from dt in _dataContext.DThuocs
                                      join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on dt.IDDon equals dtct.IDDon
                                      select new { dt.KieuDon }).ToList();
                            if (TD.First().KieuDon.Value != 2)
                            {
                                var q3 = (from kd in _dataContext.DThuocs
                                          join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on kd.IDDon equals kdct.IDDon
                                          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                          group new { kdct, dv, kd } by new { kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                          select new { kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
                                if (q3.Count > 0)
                                {
                                    int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                    var ten = _dataContext.KPhongs.Where(p => p.MaKP == tenkp).ToList();
                                    string _ploaiKP = "";
                                    if (ten.Count > 0)
                                    {
                                        _ploaiKP = ten.First().PLoai;
                                    }
                                    if (_ploaiKP.Contains("Khoa dược"))
                                    {
                                        tenkp = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tenkp).ToList();
                                        int loaiduoc = q3.First().LoaiDuoc.Value;
                                        frmIn frm3 = new frmIn();
                                        BaoCao.rep_dutruthuoc rep3 = new BaoCao.rep_dutruthuoc();
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep3.SoPL.Value = _soPL3.ToString();
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
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == tenkp).ToList();
                                        int loaiduoc = q3.First().LoaiDuoc.Value;
                                        frmIn frm3 = new frmIn();
                                        BaoCao.Phieulinhchokhoa rep3 = new BaoCao.Phieulinhchokhoa();
                                        rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep3.SoPL.Value = _soPL3.ToString();
                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                        //var TD = _dataContext.DThuocs.Where(p => p.SoPL == _soPL3).Select(p => p.KieuDon).ToList();
                                        //if (TD.First().Value != 2)
                                        //{
                                        //    rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                        //}
                                        //else
                                        //{ rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                                        if (ten.Count > 0)
                                            rep3.Khoa.Value = ten.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                        switch (loaiduoc)
                                        {
                                            case 0:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                rep3.MauSo.Value = "MS:01D/BV-01";
                                                break;
                                            case 1:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                rep3.MauSo.Value = "MS:02D/BV-01";
                                                break;
                                            case 2:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                rep3.MauSo.Value = "MS:03D/BV-01";
                                                break;
                                            case 3:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                            case 4:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                rep3.MauSo.Value = "MS:09D/BV-01";
                                                break;
                                            case 5:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                break;
                                            case 6:
                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
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
                            else
                            {
                                frmIn frm3 = new frmIn();
                                BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                rep.SoPL.Value = _soPL3.ToString();
                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                var q = (from kd in _dataContext.DThuocs
                                         join kdct in _dataContext.DThuoccts.Where(p => p.Status == 0).Where(p => p.SoPL == _soPL3) on kd.IDDon equals kdct.IDDon
                                         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                         group new { kdct, dv, kd } by new { kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe } into kq
                                         select new { kq.Key.NgayKe, TenDV = kq.Key.TenDV, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();


                                rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                                var q3 = (from kd in _dataContext.DThuocs
                                          join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL3) on kd.IDDon equals kdct.IDDon
                                          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                          group new { kdct, dv, kd } by new { kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                          select new { kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ToList();
                                if (q3.Count > 0)
                                {
                                    int tenkp = tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                    var ten = _dataContext.KPhongs.Where(p => p.MaKP == tenkp).ToList();
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
                                this.Dispose();
                            }
                            this.Dispose();
                            break;
                        case 4:
                            var xoact4 = _dataContext.NhapDcts.Where(p => p.IDNhap == iddon).ToList();
                            foreach (var xoa in xoact4)
                            {
                                _dataContext.NhapDcts.Remove(xoa);
                                _dataContext.SaveChanges();
                            }
                            var xoac4 = _dataContext.NhapDs.Single(p => p.IDNhap == iddon);
                            _dataContext.NhapDs.Remove(xoac4);
                            _dataContext.SaveChanges();
                            var iddt4 = (_dataContext.DThuoccts.Where(p => p.SoPL == _sopl).Select(p => p.IDDon)).Distinct().ToList();
                            foreach (var id in iddt4)
                            {
                                //var dthuoc4 = _dataContext.DThuocs.Single(p => p.IDDon == id);
                                //dthuoc4.Status = 0;
                                var suact4 = _dataContext.DThuoccts.Where(p => p.IDDon == id).ToList();
                                foreach (var item in suact4)
                                {
                                    if (item.Status != -1)
                                    {
                                        item.Status = 0;
                                    }
                                }
                                _dataContext.SaveChanges();
                            }

                            //}
                            //catch (Exception ex) {
                            //    MessageBox.Show("Lỗi! không xóa được: "+ex.Message);
                            //}
                            this.Dispose();
                            break;
                        case 5:
                            //sửa theo SoPL trong donthuoct
                            var idd = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).Where(p => p.Status != 1 && p.Status != -1).ToList();// không cần thiết phải != -1 ()vì ko lĩnh ko có số PL

                            foreach (var i in idd)
                            {
                                var sua = _dataContext.DThuoccts.Single(p => p.IDDonct == i.IDDonct);
                                sua.SoPL = 0;
                                _dataContext.SaveChanges();
                            }
                            var _lSoPL = _dataContext.SoPLs.Where(p => p.SoPL1 == iddon && p.PhanLoai == 1).FirstOrDefault();
                            if (_lSoPL != null)
                            {
                                _lSoPL.Status = -1;
                                
                                if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV=="24012")
                                {
                                    _lSoPL.MaCBHuy = DungChung.Bien.MaCB;
                                    _lSoPL.TGHuy = DateTime.Now;
                                }
                            }

                            _dataContext.SaveChanges();
                            this.Dispose();
                            break;
                        case 6:

                            //var sua6 = _dataContext.DThuocs.Single(p => p.IDDon == iddon);
                            //sua6.Status = 2;
                            //_dataContext.SaveChanges();
                            var suact6 = _dataContext.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                            foreach (var item in suact6)
                            {
                                if (item.Status != -1)
                                {
                                    item.Status = 2;
                                    _dataContext.SaveChanges();
                                }
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    int TH = -1;
                                    if (_medicinesProvider.isTuTruc((int)item.MaKXuat))
                                        TH = 2;
                                    else
                                        TH = 0;
                                    _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, 0, (int)item.MaKXuat, -item.SoLuong, TH);
                                }
                            }
                            this.Dispose();
                            break;
                        case 7:
                            if (DungChung.Bien.keNhieuKho)
                            {
                                var sua7 = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).ToList();
                                foreach (var upd in sua7)
                                {
                                    if (upd.Status != -1)
                                        upd.Status = 2;

                                    _dataContext.SaveChanges();
                                }
                                //List<int> dsIDdon = sua7.Select(p => p.IDDon ?? 0).Distinct().ToList();
                                //foreach (int id in dsIDdon)
                                //{
                                //    DThuoc dth = _dataContext.DThuocs.Single(p => p.IDDon == id);
                                //    var dsdtct = _dataContext.DThuoccts.Where(p => p.Status == 0 || p.Status == 1).ToList();
                                //    //if (dsdtct.Count <= 0 && sua7.Count > 0) // Tất cả các thuốc chi tiết trong đơn thuốc đều là trạng thái đã hủy hoặc ko lĩnh, (phải có ít nhất 1 thuốc là trạng thái hủy)
                                //    //    dth.Status = -1;
                                //}
                                //_dataContext.SaveChanges();
                                this.Dispose();
                            }
                            else
                            {
                                var sua7 = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).Select(p => p.IDDonct).ToList();
                                foreach (var upd in sua7)
                                {
                                    var suact7 = _dataContext.DThuoccts.Where(p => p.IDDonct == upd).ToList();
                                    foreach (var item in suact7)
                                    {
                                        if (item.Status != -1)
                                            item.Status = 2;
                                    }

                                    _dataContext.SaveChanges();
                                }
                                var _lSoPL1 = _dataContext.SoPLs.Where(p => p.SoPL1 == iddon && p.PhanLoai == 1).FirstOrDefault();
                                if (_lSoPL1 != null)
                                    _lSoPL1.Status = -1;
                                this.Dispose();
                            }
                            break;
                        case 8:
                            var suact8 = _dataContext.DThuoccts.Where(p => p.IDDon == _id).ToList();
                            foreach (var item in suact8)
                            {
                                item.Status = 3;
                                _dataContext.SaveChanges();
                            }
                            var sua81 = _dataContext.BenhNhans.Single(p => p.MaBNhan == _mbn);
                            sua81.Status = 3;
                            _dataContext.SaveChanges();
                            this.Dispose();
                            break;
                        case 9: //Hủy phiếu  của tủ trực
                            //sửa theo SoPL trong donthuoct
                            if (_ktratutruc)
                            {
                                //var qdttt = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).Where(p => p.Status != 1 && p.Status != -1).ToList();// không cần thiết phải != -1 ()vì ko lĩnh ko có số PL
                                //string strsopl = iddon.ToString();

                                //foreach (var i in qdttt)
                                //{
                                //    var sua = _dataContext.DThuoccts.Single(p => p.IDDonct == i.IDDonct);
                                //    _dataContext.DThuoccts.Remove(sua);
                                //    _dataContext.SaveChanges();
                                //}
                                ////khoa lâm sàng kê tủ trực xuất
                                //var dttt = _dataContext.DThuoccts.Where(p => p.DSCBTH == strsopl && (p.MaKXuat == _makhoake)).ToList();
                                //foreach (var a in dttt)
                                //{
                                //    DThuocct dtct = _dataContext.DThuoccts.Where(p => p.IDDonct == a.IDDonct).Single();
                                //    if (dtct.Status == 1)
                                //    {
                                //        dtct.Status = 0;
                                //        dtct.DSCBTH = null;
                                //    }
                                //    _dataContext.SaveChanges();
                                //}

                                //var qSoPL = _dataContext.SoPLs.Where(p => p.SoPL1 == iddon && p.PhanLoai == 1).FirstOrDefault();
                                //if (qSoPL != null)
                                //    qSoPL.Status = -1;
                                //_dataContext.SaveChanges();

                                var qdttt = _dataContext.DThuoccts.Where(p => p.SoPL == iddon && p.Status == 0).ToList();
                                foreach (var i in qdttt)
                                {
                                    var sua = _dataContext.DThuoccts.FirstOrDefault(p => p.IDDonct == i.IDDonct);
                                    sua.SoPL = 0;
                                    _dataContext.SaveChanges();
                                }

                                if (qdttt.Count > 0)
                                {
                                    var qSoPL = _dataContext.SoPLs.Where(p => p.SoPL1 == iddon && p.PhanLoai == 1).FirstOrDefault();
                                    if (qSoPL != null)
                                        qSoPL.Status = -1;
                                    _dataContext.SaveChanges();
                                }

                                this.Dispose();
                            }
                            break;

                    }
                }
                else
                {
                    if (!CheckMK)
                    {
                        MessageBox.Show("Sai mật khẩu");
                        dem++;
                    }
                }
            }
            else
            {
                this.Dispose();
                MessageBox.Show("không xóa được, sai mật khẩu nhiều lần");
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Check_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                CheckMK = true;
                btnOK_Click(null, null);
                this.Close();
            }
        }
    }
}