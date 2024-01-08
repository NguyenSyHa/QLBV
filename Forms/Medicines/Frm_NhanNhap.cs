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
using QLBV.Models.Dictionaries.DichVu;
using QLBV.Utilities.Commons;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_NhanNhap : DevExpress.XtraEditors.XtraForm
    {
        private QLBVEntities _dataContext;
        private readonly MedicinesProvider _medicinesProvider;
        public Frm_NhanNhap(QLBVEntities dataContext, MedicinesProvider medicinesProvider)
        {
            InitializeComponent();
            this._dataContext = dataContext;
            this._medicinesProvider = medicinesProvider;
        }
        IList<NhapDct> _lnhapct = new List<NhapDct>();
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps)) > 0)
            {
                txtID.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                int id = Convert.ToInt32(txtID.Text);
                var nhapd = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == id);

                if (nhapd != null)
                    txtKhoNhan.Text = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == nhapd.MaKP).TenKP ?? "";

                _lnhapct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _dataContext.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
        }
        List<NDuocExp> _nhap = new List<NDuocExp>();
        private void Frm_NhanNhap_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                labelControl5.Text = "Kho xuất";
                labelControl4.Text = "Kho xuất";
                txtkhox.Visible = true;
            }
            else
            {
                lupKhoNhap.Visible = true;
            }
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimKhoX.Properties.ReadOnly = false;
            }
            lupTimKhoX.EditValue = DungChung.Bien.MaKP;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtNgayNhap.DateTime = System.DateTime.Now;
            var khp = (from kp in _dataContext.KPhongs
                       where (kp.PLoai.Contains("Khoa dược") || kp.PLoai.Contains("Xã phường") || kp.PLoai.Contains("Tủ trực"))
                       select kp
                          ).OrderBy(p => p.PLoai).ToList();
            lupKhoNhap.Properties.DataSource = khp.ToList();
            lupTimKhoX.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
            var _tenduoc = _dataContext.DichVus.Where(p => p.PLoai == 1).ToList();
            lupMaDuoc.DataSource = _tenduoc;
            TimKiem();
        }
        private void TimKiem()
        {
            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            int _makx = 0;
            int _sophieu = 0;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimKhoX.EditValue != null)
                _makx = Convert.ToInt32(lupTimKhoX.EditValue);
            txtkhox.Text = Convert.ToString(lupTimKhoX.Text.Trim());
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _sophieu = Convert.ToInt32(txtTimKiem.Text);
            if (_sophieu > 0)
            {
                _nhap = _dataContext.NhapDs.Where(p => p.IDNhap == _sophieu && p.Status != 1).Select(nd => new NDuocExp
                {
                    IDNhap = nd.IDNhap,
                    GhiChu = nd.GhiChu,
                    NgayNhap = nd.NgayNhap,
                    SoCT = nd.SoCT,
                    SoPhieu = nd.SoPhieu,
                    Status = nd.Status,
                    XuatTD = nd.Status == 1 ? "Đã nhận" : "Chưa nhận",
                }).ToList();
            }
            else
            {
                if (DungChung.Bien.MaBV == "24012")
                {
                    _nhap = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1 && p.Status != 1)
                             where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                             where (nd.MaKPnx == (_makx))
                             //where (nd.MaKPnx== (_manNhan))
                             select new NDuocExp
                             {
                                 IDNhap = nd.IDNhap,
                                 GhiChu = nd.GhiChu,
                                 NgayNhap = nd.NgayNhap,
                                 SoCT = nd.SoCT,
                                 SoPhieu = nd.SoPhieu,
                                 Status = nd.Status,
                                 XuatTD = nd.XuatTD == 1 ? "Đã nhận" : "Chưa nhận",
                             }).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
                }
                else
                {
                    _nhap = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1)
                             where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                             where (nd.MaKP == (_makx))
                             //where (nd.MaKPnx== (_manNhan))
                             select new NDuocExp
                             {
                                 IDNhap = nd.IDNhap,
                                 GhiChu = nd.GhiChu,
                                 NgayNhap = nd.NgayNhap,
                                 SoCT = nd.SoCT,
                                 SoPhieu = nd.SoPhieu,
                                 Status = nd.Status,
                                 XuatTD = nd.XuatTD == 1 ? "Đã nhận" : "Chưa nhận",
                             }).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
                }
            }

            grcNhap.DataSource = _nhap.ToList();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
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

        private void lupTimKhoX_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimNoiNhan_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps)) > 0)
            {
                txtID.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                int id = Convert.ToInt32(txtID.Text);
                _lnhapct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _dataContext.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
        }
        private bool KTra()
        {
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn ngày xuất");
                dtNgayNhap.Focus();
                return false;
            }
            if (DungChung.Bien.MaBV != "24012")
            {
                if (string.IsNullOrEmpty(lupKhoNhap.Text))
                {
                    MessageBox.Show("Bạn chưa chọn nơi nhận");
                    lupKhoNhap.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(lupTimKhoX.Text))
            {
                MessageBox.Show("Bạn chưa chọn kho xuất");
                lupTimKhoX.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// pl 1: nhập thành xuất 2: xuất thành xuất
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="dt"></param>
        /// <param name="makhoxuat"></param>
        /// <param name="makp"></param>
        /// <param name="ghichu"></param>
        /// <param name="pl"></param>
        /// <returns></returns>
        string _thuochet = "";
        public bool nhannhapthanhxuat(int _id, DateTime dt, int makhoxuat, int makp, string ghichu, int pl)
        {
            try
            {
                List<NhapD> _lnhap = _dataContext.NhapDs.Where(p => p.IDNhap == _id).ToList();
                List<NhapDct> _lnhapct = new List<NhapDct>();
                _lnhapct = _dataContext.NhapDcts.Where(p => p.IDNhap == _id).ToList();
                if (_lnhap.Count > 0 && makp == 0)
                    makp = _lnhap.First().MaKP.Value;
                var ktxuattd = _dataContext.NhapDs.Where(p => p.XuatTD == _id).ToList();
                if (ktxuattd.Count > 0)
                {
                    MessageBox.Show("Chứng từ đã được sử dụng");
                    return false;
                }
                if (!IsDuThuoc(makhoxuat))
                {
                    MessageBox.Show("Thuốc: " + _thuochet + " đã hết, hãy kê thuốc khác");
                    return false;
                }
                string _ploai = "";
                var kt = _dataContext.KPhongs.Where(p => p.MaKP == (makp)).ToList();
                if (kt.Count > 0)
                    _ploai = kt.First().PLoai;
                NhapD nd = new NhapD();
                nd.NgayNhap = dt;
                nd.SoCT = _id.ToString();
                nd.PLoai = 2;
                nd.XuatTD = _id;

                switch (_ploai)
                {
                    case "Khoa dược":
                        nd.KieuDon = 2;
                        break;
                    case "Tủ trực":
                        nd.KieuDon = 6;
                        break;
                    case "Lâm sàng":
                        nd.KieuDon = 1;
                        break;
                    case "Cận lâm sàng":
                        nd.KieuDon = 5;
                        break;
                    case "Xã phường":
                        nd.KieuDon = 3;
                        break;
                    case "PK khu vực":
                        nd.KieuDon = 3;
                        break;

                }
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                {
                    if (_lnhap.Count() > 0)
                    {
                        nd.MaKP = _lnhap.First().MaKPnx;
                        nd.MaKPnx = _lnhap.First().MaKP;
                        _lnhap.First().SoCT = nd.SoCT;
                        _lnhap.First().Status = null;

                    }
                }
                else
                {

                    nd.MaKP = makhoxuat;
                    nd.MaKPnx = makp;
                }
                nd.MaCB = DungChung.Bien.MaCB;
                nd.GhiChu = ghichu;




                _dataContext.NhapDs.Add(nd);
                if (_dataContext.SaveChanges() >= 0)
                {
                    //Luu bang NhapDct
                    // lấy ID max trong bang NhapD
                    int idnhap = 0;
                    idnhap = nd.IDNhap;
                    if (idnhap > 0)
                    {
                        foreach (var a in _lnhapct)
                        {
                            NhapDct ndct = new NhapDct();
                            ndct.MaDV = a.MaDV;
                            ndct.IDNhap = idnhap;
                            ndct.DonVi = a.DonVi;
                            ndct.DonGia = a.DonGia;
                            ndct.DonGiaCT = a.DonGiaCT;
                            if (!string.IsNullOrEmpty(a.MaCC))
                                ndct.MaCC = a.MaCC;
                            else
                                ndct.MaCC = "";
                            ndct.SoLo = a.SoLo;
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                ndct.HanDung = a.HanDung;
                            }
                            ndct.SoDangKy = a.SoDangKy;
                            ndct.SoLuongX = pl == 1 ? a.SoLuongN : a.SoLuongX;
                            ndct.ThanhTienX = pl == 1 ? a.ThanhTienN : a.ThanhTienX;
                            ndct.SoLuongN = 0;
                            ndct.ThanhTienDY = a.ThanhTienDY;//
                            ndct.SoLuongDY = a.SoLuongDY;

                            ndct.ThanhTienN = 0;
                            ndct.SoLuongSD = 0;
                            ndct.ThanhTienSD = 0;
                            ndct.SoLuongKK = 0;
                            ndct.ThanhTienKK = 0;
                            _dataContext.NhapDcts.Add(ndct);

                            //Update tồn bảng MedicineList
                            _medicinesProvider.UpdateMedicineListPPX3((int)a.MaDV, a.DonGia, a.SoLo, (DateTime)a.HanDung, makhoxuat, makp, a.SoLuongN, 6);

                        }
                        _dataContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }
        private void btnNhanCT_Click(object sender, EventArgs e)
        {
            if (KTra())
            {
                bool _cothexuat = true;
                if (_lnhapct.Count > 0)
                {
                    int _id = 0;
                    _id = _lnhapct.First().IDNhap.Value;
                    //foreach (var i in _lnhapct)
                    //{
                    //    soluongton = 0;
                    //    soluongton = DungChung.Ham._checkTon(_data, i.MaDV, lupTimMaKP.EditValue.ToString(), 1, i.DonGia.Value, i.SoLuong.Value, i.MaCC);
                    //    if (soluongton < 0)
                    //    {
                    //        var tenthuoc = _dataContext.DichVus.Where(p => p.MaDV == i.MaDV).ToList();
                    //        if (tenthuoc.Count > 0)
                    //        {
                    //            _cothexuat = false;
                    //            MessageBox.Show("Thuốc: " + tenthuoc.First().TenDV + " đã hết, hãy kê thuốc khác cho bệnh nhân");

                    //        }
                    //    }
                    //}
                    DialogResult _result = MessageBox.Show("Bạn muốn nhận chứng từ số :" + txtID.Text, "Nhận CT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        int khoxuat = lupTimKhoX.EditValue == null ? 0 : Convert.ToInt32(lupTimKhoX.EditValue);
                        int noinhan = lupKhoNhap.EditValue == null ? 0 : Convert.ToInt32(lupKhoNhap.EditValue);
                        nhannhapthanhxuat(_id, dtNgayNhap.DateTime, khoxuat, noinhan, txtGhiChu.Text, 1);

                        this.Dispose();
                    }


                } // kiểm tra có dl ở NhapDct
            }
        }

        string msg = "Số lượng tồn không đủ: ";
        bool isDuThuoc = true;
        IList<NhapDctModel> nhapDcts = new List<NhapDctModel>();
        private bool IsDuThuoc(int maKhoXuat)
        {
            int ppxuat = _medicinesProvider.GetPPXuat(maKhoXuat);

            if (grvNhapCT.DataSource != null)
            {
                nhapDcts = AppConfig.MyMapper.Map<IList<NhapDctModel>>(grvNhapCT.DataSource);
                if (ppxuat == 3)
                {
                    nhapDcts = (from a in nhapDcts
                                group a by new { a.MaDV, a.DonGia, a.SoLo, a.HanDung } into kq
                                 select new NhapDctModel
                                 {
                                     MaDV = kq.Key.MaDV,
                                     SoLuongN = kq.Sum(p => p.SoLuongN),
                                     DonGia = kq.Key.DonGia,
                                     SoLo = kq.Key.SoLo,
                                     HanDung = kq.Key.HanDung
                                 }
                             ).ToList();
                }
                else
                {
                    nhapDcts = (from a in nhapDcts
                                group a by new { a.MaDV, a.DonGia } into kq
                                 select new NhapDctModel
                                 {
                                     MaDV = kq.Key.MaDV,
                                     SoLuongN = kq.Sum(p => p.SoLuongN),
                                     DonGia = kq.Key.DonGia
                                 }
                             ).ToList();
                }

                foreach (var item in nhapDcts)
                {
                    if (!_medicinesProvider.IsDuThuoc(maKhoXuat, (int)item.MaDV, item.DonGia, item.SoLo, item.HanDung, item.SoLuongN))
                    {
                        isDuThuoc = false;
                        string tenDV = _dataContext.DichVus.FirstOrDefault(p => p.MaDV == item.MaDV).TenDV ?? "";

                        _thuochet += tenDV + "; ";
                        if (_thuochet.EndsWith(";"))
                            _thuochet.Remove(_thuochet.LastIndexOf(';'));
                    }
                }
            }
            return isDuThuoc;
        }

        private void btnQLChungTu_Click(object sender, EventArgs e)
        {
            Frm_QuanLyChungTu frm = new Frm_QuanLyChungTu(_dataContext, false);
            frm.ShowDialog();
        }
    }

    public class NDuocExp
    {
        public int IDNhap { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string SoCT { get; set; }
        public string SoPhieu { get; set; }
        public int? Status { get; set; }
        public string XuatTD { get; set; }
    }
}