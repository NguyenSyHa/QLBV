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
    public partial class frm_NhanNhap : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhanNhap()
        {
            InitializeComponent();
        }
        List<NhapDct> _lnhapct = new List<NhapDct>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps)) > 0)
            {
                txtID.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                int id = Convert.ToInt32(txtID.Text);
                var listct = _data.NhapDs.Where(p => p.IDNhap.Equals(id)).ToList();
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
        }
        List<NDuocExp> _nhap = new List<NDuocExp>();
        private void frm_NhanXuat_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "24012")
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
            var khp = (from kp in _data.KPhongs
                       where (kp.PLoai.Contains("Khoa dược") || kp.PLoai.Contains("Xã phường") || kp.PLoai.Contains("Tủ trực"))
                       select kp
                          ).OrderBy(p => p.PLoai).ToList();
            lupKhoNhap.Properties.DataSource = khp.ToList();
            lupTimKhoX.Properties.DataSource = khp.Where(p => p.PLoai == ("Khoa dược")).ToList();
            var _tenduoc = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            lupMaDuoc.DataSource = _tenduoc;
            TimKiem();
        }
        private void TimKiem()
        {
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
                _nhap = _data.NhapDs.Where(p => p.IDNhap == _sophieu).Select(nd => new NDuocExp
                {
                    IDNhap = nd.IDNhap,
                    GhiChu = nd.GhiChu,
                    NgayNhap = nd.NgayNhap,
                    SoCT = nd.SoCT,
                    SoPhieu = nd.SoPhieu,
                    Status = nd.Status,
                    XuatTD = nd.XuatTD == 1 ? "Đã nhận" : "Chưa nhận",
                }).ToList();
            }
            else
            {
                if (DungChung.Bien.MaBV == "24012")
                {
                    _nhap = (from nd in _data.NhapDs.Where(p => p.PLoai == 1)
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
                    _nhap = (from nd in _data.NhapDs.Where(p => p.PLoai == 1)
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
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                grcNhapCT.DataSource = _lnhapct;
            }
            else
            {
                txtID.Text = "";
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
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
        public static bool nhannhapthanhxuat(int _id, DateTime dt, int makhoxuat, int makp, string ghichu, int pl)
        {
            try
            {
                string _thuochet = "";
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<NhapD> _lnhap = _data.NhapDs.Where(p => p.IDNhap == _id).ToList();
                List<NhapDct> _lnhapct = new List<NhapDct>();
                _lnhapct = _data.NhapDcts.Where(p => p.IDNhap == _id).ToList();
                if (_lnhap.Count > 0 && makp == 0)
                    makp = _lnhap.First().MaKP.Value;
                var ktxuattd = _data.NhapDs.Where(p => p.XuatTD == _id).ToList();
                if (ktxuattd.Count > 0)
                {
                    MessageBox.Show("Chứng từ đã được sử dụng");
                    return false;
                }
                foreach (var i in _lnhapct)
                {
                    double soluongton = 0;
                    soluongton = DungChung.Ham._checkTon(_data, i.MaDV == null ? 0 : i.MaDV.Value, makhoxuat, i.DonGia, i.SoLuongN, i.SoLo);
                    if (soluongton < 0)
                    {
                        var tenthuoc = _data.DichVus.Where(p => p.MaDV == i.MaDV).ToList();
                        if (tenthuoc.Count > 0)
                        {
                            _thuochet += tenthuoc.First().TenDV + "; ";


                        }
                    }
                }
                if (!string.IsNullOrEmpty(_thuochet))
                {
                    MessageBox.Show("Thuốc: " + _thuochet + " đã hết, hãy kê thuốc khác");
                    return false;
                }
                string _ploai = "";
                var kt = _data.KPhongs.Where(p => p.MaKP == (makp)).ToList();
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
                    
                    //sửa status của đơn cũ
                    if (_lnhap.Count() > 0)
                    {
                        _lnhap.First().Status = null;
                        nd.MaKP = _lnhap.First().MaKPnx;
                        nd.MaKPnx = _lnhap.First().MaKP;
                    }
                }
                else
                {

                    nd.MaKP = makhoxuat;
                    nd.MaKPnx = makp ;
                }
                nd.MaCB = DungChung.Bien.MaCB;
                nd.GhiChu = ghichu;
                

                

                _data.NhapDs.Add(nd);
                if (_data.SaveChanges() >= 0)
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
                            _data.NhapDcts.Add(ndct);

                        }
                        _data.SaveChanges();
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
                    //        var tenthuoc = _data.DichVus.Where(p => p.MaDV == i.MaDV).ToList();
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