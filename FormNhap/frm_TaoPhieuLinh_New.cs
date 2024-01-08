using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.DXErrorProvider;

namespace QLBV.FormNhap
{
    public partial class frm_TaoPhieuLinh_New : DevExpress.XtraEditors.XtraForm
    {
        public frm_TaoPhieuLinh_New()
        {
            InitializeComponent();
        }
        public frm_TaoPhieuLinh_New(int maBN)
        {
            _maBN = maBN;
            InitializeComponent();
        }
        public int _maBN = 0;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> _ldvu = new List<DichVu>();
        DonThuoc _dthuoc = new DonThuoc();//đơn thuốc đang thêm mới hoặc sửa
        List<DThuocct> _ldthuocct = new List<DThuocct>();// danh sách đơn thuốc chi tiết đang thêm mới hoặc sửa

        int change = 0;// xác định sự thay đổi đơn thuốc
        int changect = 0;// xác định sự thay đổi đơn thuốc chi tiết
        int trangthai = 0;// trạng thái: 0: mặc định, 1: thêm, 2: sửa
        bool bnBHYT = false;
        public class DonThuoc
        {
            public int IDDon { set; get; }
            public DateTime? NgayKe { set; get; }
            public string MaCB { set; get; }
            public int? KieuDon { set; get; }
            public string DienBien { set; get; }
            public int? MaBNhan { set; get; }


            public int MaKP { get; set; }
        }
        private void frm_TaoPhieuLinh_New_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region load kiểu đơn
            List<KDon> lkieudon = new List<KDon>();
            lkieudon.Add(new KDon { KieuDon = 0, TenKieuDon = "Hàng ngày" });
            lkieudon.Add(new KDon { KieuDon = 1, TenKieuDon = "Bổ sung" });
            lkieudon.Add(new KDon { KieuDon = 2, TenKieuDon = "Trả thuốc" });
            lkieudon.Add(new KDon { KieuDon = 3, TenKieuDon = "Lĩnh khoa" });
            lkieudon.Add(new KDon { KieuDon = 4, TenKieuDon = "Khoa trả thuốc" });
            lkieudon.Add(new KDon { KieuDon = 4, TenKieuDon = "Trực" });
            lupKieuDon.DisplayMember = "TenKieuDon";
            lupKieuDon.ValueMember = "KieuDon";
            lupKieuDon.DataSource = lkieudon;
            #endregion
            #region load bác sỹ chỉ định
            var qbsy = data.CanBoes.ToList();
            lupBSKeDon.ValueMember = "MaCB";
            lupBSKeDon.DisplayMember = "TenCB";
            lupBSKeDon.DataSource = qbsy;
            #endregion
            #region load khoa phong
            var qkhoake = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            lupKhoaKe.DisplayMember = "TenKP";
            lupKhoaKe.ValueMember = "MaKP";
            lupKhoaKe.DataSource = qkhoake;

            var qkhoXuat = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            lupKhoXuat.DisplayMember = "TenKP";
            lupKhoXuat.ValueMember = "MaKP";
            lupKhoXuat.DataSource = qkhoXuat;

            lupKhoXuatct.DisplayMember = "TenKP";
            lupKhoXuatct.ValueMember = "MaKP";
            lupKhoXuatct.DataSource = qkhoXuat;
            #endregion
            #region load Loại dược
            List<MyObject> loaiduoc = new List<MyObject>();
            loaiduoc.Add(new MyObject { Value = 0, Text = "Thuốc thường" });
            loaiduoc.Add(new MyObject { Value = 1, Text = "Hóa chất" });
            loaiduoc.Add(new MyObject { Value = 2, Text = "Vật tư y tế" });
            loaiduoc.Add(new MyObject { Value = 3, Text = "Thuốc gây nghiện" });
            loaiduoc.Add(new MyObject { Value = 4, Text = "Thuốc hướng tâm thần" });
            loaiduoc.Add(new MyObject { Value = 5, Text = "Thuốc trẻ em" });
            loaiduoc.Add(new MyObject { Value = 6, Text = "Thuốc đông y" });
            lupNhomDuoc.DisplayMember = "Text";
            lupNhomDuoc.ValueMember = "Value";
            lupNhomDuoc.DataSource = loaiduoc;
            #endregion
            #region load dịch vụ
            _ldvu = data.DichVus.Where(p => p.PLoai == 1).ToList();
            lupDichVu.DisplayMember = "TenDV";
            lupDichVu.ValueMember = "MaDV";
            lupDichVu.DataSource = _ldvu;
            #endregion
            #region load trong bao hiem
            List<MyObject> _ltrongbh = new List<MyObject>();
            _ltrongbh.Add(new MyObject { Value = 0, Text = "Ngoài DM" });
            _ltrongbh.Add(new MyObject { Value = 1, Text = "Trong DM" });
            _ltrongbh.Add(new MyObject { Value = 2, Text = "Không thanh toán" });
            lupTrongBH.DisplayMember = "Text";
            lupTrongBH.ValueMember = "Value";
            lupTrongBH.DataSource = _ltrongbh;

            List<MyObject> _linh = new List<MyObject>();
            _linh.Add(new MyObject { Value = -1, Text = "Không lĩnh" });
            _linh.Add(new MyObject { Value = 0, Text = "Lĩnh" });
            _linh.Add(new MyObject { Value = 1, Text = "Đã lĩnh" });
            _linh.Add(new MyObject { Value = 2, Text = "Hủy" });
            lupLinh.DisplayMember = "Text";
            lupLinh.ValueMember = "Value";
            lupLinh.DataSource = _linh;

            #endregion
            loadDonThuoc();
            loadDthuocct();
            editable = false;
            setEditTable(false);
            BenhNhan bn = data.BenhNhans.Where(p => p.MaBNhan == _maBN).FirstOrDefault();
            if (bn != null && !String.IsNullOrEmpty(bn.SThe))
                bnBHYT = true;

        }

        int rowDThuoc = 0;
        int rowDThuocct = 0;
        private void loadDonThuoc()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            if (_maBN > 0)
            {
                List<DonThuoc> ldthuoc = (from dt in data.DThuocs.Where(p => p.MaBNhan == _maBN).Where(p => p.PLDV == 1)
                                          join db in data.DienBiens on dt.IDDon equals db.IDDon into kq
                                          from kq1 in kq.DefaultIfEmpty()
                                          select new DonThuoc
                                          {
                                              IDDon = dt.IDDon,
                                              NgayKe = dt.NgayKe,
                                              MaKP = dt.MaKP ?? 0,
                                              MaCB = dt.MaCB,
                                              MaBNhan = dt.MaBNhan,
                                              KieuDon = dt.KieuDon,
                                              DienBien = kq1 == null ? null : kq1.DienBien1
                                          }).ToList();
                bindingSource1.DataSource = ldthuoc;
                grc_Dthuoc.DataSource = bindingSource1;
            }
        }
        public void loadDthuocct()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (_maBN > 0 && grv_Dthuoc.FocusedRowHandle >= 0)
            {
                if (grv_Dthuoc.GetRowCellValue(rowDThuoc, colIDDon) != null)
                {
                    int Iddon = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(rowDThuoc, colIDDon));
                    List<DThuocct> ldthuocct = (from dtct in data.DThuoccts.Where(p => Iddon != 0 && p.IDDon == Iddon)
                                                select dtct).ToList();
                    bindingSource2.DataSource = ldthuocct;
                    grc_Dthuocct.DataSource = bindingSource2;
                }
            }
            else
            {
                bindingSource2.DataSource = null;
                grc_Dthuocct.DataSource = bindingSource2;
            }
        }
        public class MyObject
        {
            public int Value { set; get; }
            public string Text { set; get; }
        }

        /// <summary>
        /// Kiểu đơn
        /// </summary>
        public class KDon
        {
            public int KieuDon { set; get; }
            public string TenKieuDon { set; get; }

        }


        //    int rowvalid = -1;
        bool editable = false;
        private void grv_Dthuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string iddon = "0";
            if (grv_Dthuoc.GetRowCellValue(grv_Dthuoc.FocusedRowHandle, colIDDon) != null)
                iddon = (grv_Dthuoc.GetRowCellValue(grv_Dthuoc.FocusedRowHandle, colIDDon)).ToString();
            grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Beige;
            if (trangthai == 0)// chưa làm gì
            {
                rowDThuoc = grv_Dthuoc.FocusedRowHandle;
                loadDthuocct();
                lblIDDon.Text = iddon;
            }
            else if (trangthai == 1)// thêm mới
            {
                if (change != 0 || changect != 0)// đã có sự thay đổi
                {
                    if (grv_Dthuoc.FocusedRowHandle < 0 || iddon != "0")// di chuyển sang dòng khác
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn chưa lưu dữ liệu, bạn có muốn lưu lại không ?", "Xác nhận lưu", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            btnLuu_Click(null, null);
                        }
                        else
                        {
                            change = 0;
                            changect = 0;
                            loadDonThuoc();
                            trangthai = 0;
                        }
                    }
                }
                rowDThuoc = grv_Dthuoc.FocusedRowHandle;
                loadDthuocct();
                lblIDDon.Text = iddon;
            }
            else if (trangthai == 2) // sửa
            {
                if ((change != 0 || changect != 0) && (grv_Dthuoc.FocusedRowHandle != rowDThuoc))// đã có sự thay đổi
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn chưa lưu dữ liệu, bạn có muốn lưu lại không ?", "Xác nhận lưu", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        btnLuu_Click(null, null);
                    }
                    else
                    {
                        change = 0;
                        changect = 0;
                        loadDonThuoc();
                        trangthai = 0;
                    }
                }

                rowDThuoc = grv_Dthuoc.FocusedRowHandle;
                loadDthuocct();
                lblIDDon.Text = iddon;
            }

            //    string iddon = lblIDDon.Text; 
            //    grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Beige;
            //    if (grv_Dthuoc.FocusedRowHandle < 0)
            //    {
            //        lblIDDon.Text = "";
            //    }
            //    else
            //        if (grv_Dthuoc.GetRowCellValue(grv_Dthuoc.FocusedRowHandle, colIDDon) != null)
            //            lblIDDon.Text = (grv_Dthuoc.GetRowCellValue(grv_Dthuoc.FocusedRowHandle, colIDDon)).ToString();

            //    if ( lblIDDon.Text == iddon && grv_Dthuoc.FocusedRowHandle < 0)
            //    {
            //        rowDThuoc = grv_Dthuoc.FocusedRowHandle;
            //    }

            //    loadDthuocct();

            //    if (trangthai == 1 && (lblIDDon.Text == "0" || lblIDDon.Text == ""))
            //    {
            //        editable = true;
            //        grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Aquamarine;

            //    }
            //    else if (trangthai == 2 && rowDThuoc == rowsua)
            //    {
            //        editable = true;
            //        grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Aquamarine;
            //    }
            //    else
            //    {
            //        editable = false;
            //        grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Beige;
            //        trangthai = 0;
            //    }
            //    setEditTable(editable);

            //_dthuoc = new DonThuoc();
            //rowDThuoc = grv_Dthuoc.FocusedRowHandle;
        }
        private void setEditTable(bool rs)
        {
            grv_Dthuoc.Columns["NgayKe"].OptionsColumn.AllowEdit = rs;
            grv_Dthuoc.Columns["MaKP"].OptionsColumn.AllowEdit = rs;
            grv_Dthuoc.Columns["MaCB"].OptionsColumn.AllowEdit = rs;
            grv_Dthuoc.Columns["KieuDon"].OptionsColumn.AllowEdit = rs;
            grv_Dthuoc.Columns["DienBien"].OptionsColumn.AllowEdit = rs;

            // grv_Dthuocct.Columns["SoPL"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["DonGia"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["DonVi"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["SoLuong"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["ThanhTien"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["MaDV"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["TrongBH"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = rs;
            grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = rs;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldthuocct = new List<DThuocct>();
            // GetDthuoc();
            //_ldthuocct=  getDthuocct();
            int rowDthuocFC = -1;
            int iddon = 0;

            for (int i = 0; i < grv_Dthuoc.RowCount; i++)
            {
                if (grv_Dthuoc.GetRowCellValue(i, colIDDon) != null && grv_Dthuoc.GetRowCellValue(i, colIDDon).ToString() == lblIDDon.Text)
                {
                    iddon = Convert.ToInt32(lblIDDon.Text);
                    rowDthuocFC = i;
                    break;
                }
            }

            DThuoc dthuoc = new DThuoc();
            #region cập nhật đơn thuốc

            int makp = 0;
            //_dthuoc = new DonThuoc();

            #region cập nhật đơn thuốc
            if (iddon == 0)
            {
                #region thêm mới
                dthuoc.MaBNhan = _maBN;
                //dthuoc.SoPL = 0;
                //dthuoc.Status = 0;
                dthuoc.PLDV = 1;
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colNgayKe) != null)
                {
                    DateTime ngayke = Convert.ToDateTime(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colNgayKe));
                    dthuoc.NgayKe = ngayke;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKhoaKe) != null)
                {
                    makp = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKhoaKe));
                    dthuoc.MaKP = makp;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colBsKeDon) != null)
                {
                    string MaCB = grv_Dthuoc.GetRowCellValue(rowDthuocFC, colBsKeDon).ToString(); ;
                    dthuoc.MaCB = MaCB;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKieuDon) != null)
                {
                    int kieudon = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKieuDon));
                    dthuoc.KieuDon = kieudon;
                }
                data.DThuocs.Add(dthuoc);
                data.SaveChanges();
                iddon = dthuoc.IDDon;

                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colDBBenh) != null)
                {
                    string dienbien = grv_Dthuoc.GetRowCellValue(rowDthuocFC, colDBBenh).ToString();
                    if (dienbien.Trim() != "")
                    {
                        DienBien dbien = new DienBien();
                        dbien.IDDon = iddon;
                        dbien.DienBien1 = dienbien;
                        data.DienBiens.Add(dbien);
                        data.SaveChanges();
                    }
                }

                #endregion
                //thiếu add vào diễn biến
            }
            else
            {
                #region sửa
                _dthuoc = (from dt in data.DThuocs.Where(p => p.IDDon == iddon)
                           join db in data.DienBiens on dt.IDDon equals db.IDDon into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           DonThuoc { IDDon = dt.IDDon, MaBNhan = dt.MaBNhan, KieuDon = dt.KieuDon, MaCB = dt.MaCB, MaKP = dt.MaKP ?? 0, NgayKe = dt.NgayKe, DienBien = kq1 == null ? "" : kq1.DienBien1 })
                           .FirstOrDefault();
                // _dthuoc.SoPL = 0;

                dthuoc = data.DThuocs.Where(p => p.IDDon == iddon).FirstOrDefault();
                if (grv_Dthuoc.GetRowCellValue(rowDThuoc, colNgayKe) != null)
                {
                    DateTime ngayke = Convert.ToDateTime(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colNgayKe));
                    dthuoc.NgayKe = ngayke;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKhoaKe) != null)
                {
                    makp = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKhoaKe));
                    dthuoc.MaKP = makp;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colBsKeDon) != null)
                {
                    string MaCB = grv_Dthuoc.GetRowCellValue(rowDThuoc, colBsKeDon).ToString(); ;
                    dthuoc.MaCB = MaCB;
                }
                if (grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKieuDon) != null)
                {
                    int kieudon = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(rowDthuocFC, colKieuDon));
                    dthuoc.KieuDon = kieudon;
                }


                if (grv_Dthuoc.GetRowCellValue(rowDThuoc, colDBBenh) != null)
                {
                    DienBien db = data.DienBiens.Where(p => p.IDDon == iddon).FirstOrDefault();

                    string dienbien = grv_Dthuoc.GetRowCellValue(rowDThuoc, colDBBenh).ToString();
                    if (db == null)
                    {
                        db = new DienBien();
                        if (dienbien.Trim() != "")
                        {
                            db.IDDon = iddon;
                            db.DienBien1 = dienbien;
                            data.DienBiens.Add(db);
                            data.SaveChanges();
                        }
                    }
                    else
                    {
                        db.DienBien1 = dienbien;
                        data.SaveChanges();
                    }
                }

                data.SaveChanges();
                rowDThuoc = grv_Dthuoc.FocusedRowHandle;
                #endregion
            }
            #endregion
            #endregion


            #region cập nhật đơn thuốc chi tiết
            for (int i = 0; i < grv_Dthuocct.RowCount; i++)
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DThuocct dthuocct = new DThuocct();
                int iddonct = 0; int maDV = 0;
                if (grv_Dthuocct.GetRowCellValue(i, colIDDonct) != null)
                    iddonct = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colIDDonct));
                if (grv_Dthuocct.GetRowCellValue(i, coltenDVct) != null)
                    maDV = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, coltenDVct));
                if (maDV > 0)
                {
                    if (iddonct <= 0)
                    {
                        #region thêm mới
                        dthuocct.IDDon = iddon;
                        dthuocct.MaDV = maDV;
                        dthuocct.MaKP = makp;
                        dthuocct.SoPL = 0;
                        dthuocct.Status = 0;
                        if (grv_Dthuocct.GetRowCellValue(i, colSoPLct) != null)
                        {
                            int sopl = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colSoPLct));
                            dthuocct.SoPL = sopl;
                        }

                        if (grv_Dthuocct.GetRowCellValue(i, colKhoXuatct) != null)
                        {
                            int khoxuat = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colKhoXuatct));
                            dthuocct.MaKXuat = khoxuat;
                            //thêm kho xuất vào đơn chi tiết
                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colDongiact) != null)
                        {
                            dthuocct.DonGia = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colDongiact));


                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colDonvict) != null)
                        {
                            dthuocct.DonVi = grv_Dthuocct.GetRowCellValue(i, colDonvict).ToString();
                            //thêm đơn vị vào đơn chi tiết
                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colThanhTien) != null)
                        {
                            dthuocct.ThanhTien = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colThanhTien));
                            //thêm thành tiền vào đơn chi tiết
                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colSoLuongct) != null)
                        {
                            dthuocct.SoLuong = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colSoLuongct));
                            //thêm số lượng vào đơn chi tiết
                        }

                        #endregion
                        data.DThuoccts.Add(dthuocct);
                        int count = data.SaveChanges();
                    }
                    else
                    {
                        #region sửa
                        dthuocct = data.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        dthuocct.MaDV = maDV;
                        dthuocct.MaKP = makp;
                        dthuocct.SoPL = 0;

                        if (grv_Dthuocct.GetRowCellValue(i, colSoPLct) != null)
                        {
                            int sopl = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colSoPLct));
                            dthuocct.SoPL = sopl;
                        }

                        if (grv_Dthuocct.GetRowCellValue(i, colKhoXuatct) != null)
                        {
                            int khoxuat = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colKhoXuatct));
                            //thêm kho xuất vào đơn chi tiết
                        }

                        if (grv_Dthuocct.GetRowCellValue(i, colDongiact) != null)
                        {
                            dthuocct.DonGia = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colDongiact));

                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colDonvict) != null)
                        {
                            dthuocct.DonVi = grv_Dthuocct.GetRowCellValue(i, colDonvict).ToString();

                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colThanhTien) != null)
                        {
                            dthuocct.ThanhTien = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colThanhTien));

                        }
                        if (grv_Dthuocct.GetRowCellValue(i, colSoLuongct) != null)
                        {
                            dthuocct.SoLuong = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(i, colSoLuongct));

                        }
                        #endregion
                        int count = data.SaveChanges();
                    }
                }
            }
            #endregion
            _dthuoc = new DonThuoc();
            change = 0;
            changect = 0;
            trangthai = 0;
            loadDonThuoc();
            loadDthuocct();
            editable = false;
            setEditTable(false);
            rowsua = -1;
        }
        private void grv_Dthuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (string.IsNullOrEmpty(grv_Dthuoc.GetRowCellDisplayText(e.RowHandle, colNgayKe).ToString()))
                grv_Dthuoc.SetRowCellValue(e.RowHandle, colNgayKe, DateTime.Now);
            grv_Dthuoc.SetRowCellValue(e.RowHandle, colBsKeDon, DungChung.Bien.MaCB);
            if (string.IsNullOrEmpty(grv_Dthuoc.GetRowCellDisplayText(e.RowHandle, colKhoaKe).ToString()))
                grv_Dthuoc.SetRowCellValue(e.RowHandle, colKhoaKe, DungChung.Bien.MaKP);
        }

        private bool checkDThuocCT_setColumnError(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn colDonGia = view.Columns["DonGia"];
            GridColumn colSoLuong = view.Columns["SoLuong"];
            GridColumn colThanhTien = view.Columns["ThanhTien"];
            GridColumn colMaDV = view.Columns["MaDV"];
            GridColumn colLinh = view.Columns["Status"];
            object valMaDV = view.GetRowCellValue(e.RowHandle, colMaDV);
            object valSoLuong = view.GetRowCellValue(e.RowHandle, colSoLuong);
            object valDonGia = view.GetRowCellValue(e.RowHandle, colDonGia);
            object valThanhTien = view.GetRowCellValue(e.RowHandle, colThanhTien);
            object valLinh = view.GetRowCellValue(e.RowHandle, colLinh);
            if (valMaDV == null || valMaDV.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colMaDV, "Bạn chưa chọn loại thuốc", ErrorType.Default);
                return false;
            }
            else if (valDonGia == null || valDonGia.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "Bạn chưa nhập đơn giá", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valDonGia).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colDonGia, "đơn giá phải lơn hơn 0", ErrorType.Default);
                return false;
            }
            else if (valSoLuong == null || valSoLuong.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "Bạn chưa nhập số lượng", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valSoLuong).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colSoLuong, "số lượng phải lơn hơn 0", ErrorType.Default);
                return false;
            }

            else if (valThanhTien == null || valThanhTien.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colThanhTien, "Bạn chưa nhập thành tiền", ErrorType.Default);
                return false;
            }
            else if (Convert.ToDouble((valThanhTien).ToString()) <= 0)
            {
                e.Valid = false;
                view.SetColumnError(colThanhTien, "Thành tiền phải lớn hơn 0", ErrorType.Default);
                return false;
            }
            else if (grv_Dthuocct.FocusedRowHandle <0 && Convert.ToInt32((valLinh).ToString()) != 0 && Convert.ToInt32((valLinh).ToString()) != -1)
            {
                e.Valid = false;
                view.SetColumnError(colLinhct, "Bạn chỉ có thể chọn lĩnh hoặc không lĩnh", ErrorType.Default);
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool checkDthuoc_setColumnError(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            // rowvalid = grv_Dthuoc.FocusedRowHandle;
            GridView view = sender as GridView;
            GridColumn colNgayKe = view.Columns["NgayKe"];
            GridColumn colMaKP = view.Columns["MaKP"];
            GridColumn colMaCB = view.Columns["MaCB"];

            object valNgayKe = view.GetRowCellValue(e.RowHandle, colNgayKe);
            object valMaKP = view.GetRowCellValue(e.RowHandle, colMaKP);
            object valMaCB = view.GetRowCellValue(e.RowHandle, colMaCB);

            if (valNgayKe == null || valNgayKe.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colNgayKe, "Bạn chưa nhập ngày kê", ErrorType.Default);
                return false;
            }

            else if (valMaKP == null || valMaKP.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colMaKP, "Bạn chưa nhập khoa kê", ErrorType.Default);
                return false;
            }
            else if (valMaCB == null || valMaCB.ToString() == string.Empty)
            {
                e.Valid = false;
                view.SetColumnError(colMaCB, "Bạn chưa nhập bác sỹ kê đơn", ErrorType.Default);
                return false;
            }
            else
            {
                return true;
            }


        }

        private void grv_Dthuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            checkDthuoc_setColumnError(sender, e);

        }

        private void grv_Dthuocct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            checkDThuocCT_setColumnError(sender, e);
        }

        private void grv_Dthuoc_RowStyle(object sender, RowStyleEventArgs e)
        {
        }

        private void grv_Dthuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            rowDThuocct = grv_Dthuocct.FocusedRowHandle;
            if (grv_Dthuocct.FocusedRowHandle > 0)
            {
                int idct = 0;
                if (grv_Dthuocct.GetRowCellValue(grv_Dthuocct.FocusedRowHandle, colIDDonct) != null)
                {
                    idct = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(grv_Dthuocct.FocusedRowHandle, colIDDonct));
                    if (idct > 0)
                    {
                        var qlinh = data.DThuoccts.Where(p => p.IDDonct == idct).FirstOrDefault();
                        if (qlinh != null)
                            linh = qlinh.Status??0;
                    }
                }
            }

            //set thuốc đã có phiếu lĩnh không được xóa nhưng được sửa trong ngoài danh mục

            if (editable)
            {
                if (grv_Dthuocct.GetRowCellValue(rowDThuocct, colIDDonct) != null)
                {
                    int idct = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(rowDThuocct, colIDDonct));
                    int sopl = 0;
                    int status = 0;
                    var dtct = data.DThuoccts.Where(p => p.IDDonct == idct).FirstOrDefault();
                    if (dtct != null)
                    {
                        sopl = dtct.SoPL;
                        status = dtct.Status ?? 0;
                      
                    }
                    if (sopl > 0) // đã có số phiếu lĩnh
                    {
                        grv_Dthuocct.Columns[0].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns[0].OptionsColumn.ReadOnly = true;
                        // grv_Dthuocct.Columns["SoPL"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["DonGia"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["DonVi"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["SoLuong"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["MaDV"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["TrongBH"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = false;
                        grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = false;                       
                    }
                    else
                    {
                        grv_Dthuocct.Columns[0].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns[0].OptionsColumn.ReadOnly = false;
                        // grv_Dthuocct.Columns["SoPL"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["DonGia"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["DonVi"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["SoLuong"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["ThanhTien"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["MaDV"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["TrongBH"].OptionsColumn.AllowEdit = true;
                        grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = true;
                        if (status == -1 || status == 0)
                        {
                            grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = true;
                        }
                        else
                            grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = false;
                    }

                }
                else
                {
                    grv_Dthuocct.Columns[0].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns[0].OptionsColumn.ReadOnly = false;
                    // grv_Dthuocct.Columns["SoPL"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["DonGia"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["DonVi"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["SoLuong"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["ThanhTien"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["MaDV"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["TrongBH"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = true;
                    grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = true;

                }


            }
            else
            {
                grv_Dthuocct.Columns[0].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns[0].OptionsColumn.ReadOnly = true;
                //grv_Dthuocct.Columns["SoPL"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["DonGia"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["DonVi"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["SoLuong"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["MaDV"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["TrongBH"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["Status"].OptionsColumn.AllowEdit = false;
                grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = false;
            }
        }

        private void grv_Dthuocct_RowStyle(object sender, RowStyleEventArgs e)
        {
        }

        private void grv_Dthuoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            change++;
        }
        int linh = 0;
        private void grv_Dthuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            changect++;
            double dongia1 = 0, soluong = 0;
            int maDV = 0;
            string donvi = "";
            string dongia = "";
            int maKhoXuat = 0;
            if (e.Column.Name == "coltenDVct")
            {
                if (e.Value != null)
                    maDV = Convert.ToInt32(e.Value.ToString());
                if (grv_Dthuocct.GetRowCellValue(e.RowHandle, colKhoXuatct) != null)
                    maKhoXuat = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(e.RowHandle, colKhoXuatct));
                soluong = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(e.RowHandle, colSoLuongct));

                // Lấy đơn vị trong bảng dịch vụ
                var dv = (from d in _ldvu
                          where d.MaDV == maDV
                          select new { d.DonVi }).FirstOrDefault();

                // lấy đơn giá trong bảng nhập dược chi tiết
                var nhapdct_dg = (from dct in data.NhapDcts
                                  join d in data.NhapDs on dct.IDNhap equals d.IDNhap
                                  where dct.MaDV == maDV && (d.PLoai == 1) && (maKhoXuat == 0 || d.MaKP == maKhoXuat)
                                  select new { dct.DonGia }).Distinct().ToList();
                if (dv != null)
                {
                    if (dv.DonVi != null)
                        donvi = dv.DonVi.ToString();


                    cboDonGia.Items.Clear();
                    if (nhapdct_dg.Count() > 0)
                    {
                        foreach (var n in nhapdct_dg)
                        {
                            if (n.DonGia != null)
                                cboDonGia.Items.Add(n.DonGia);
                        }

                        if (cboDonGia.Items.Count > 0)
                        {
                            grv_Dthuocct.SetRowCellValue(e.RowHandle, colDongiact, cboDonGia.Items[0]);
                        }

                    }

                    grv_Dthuocct.SetFocusedRowCellValue(colDonvict, donvi);

                }
            }
            else if (e.Column.Name == "colDongiact")
            {

                if (e.Value != null)
                {
                    dongia1 = Convert.ToDouble(e.Value);
                    if (grv_Dthuocct.GetRowCellValue(e.RowHandle, colSoLuongct) != null)
                    {
                        soluong = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(e.RowHandle, colSoLuongct));
                        grv_Dthuocct.SetRowCellValue(e.RowHandle, colThanhTien, soluong * dongia1);
                    }
                }

            }
            else if (e.Column.Name == "colKhoXuatct")
            {
                grv_Dthuoc_ShownEditor(sender, null);

            }
            else if (e.Column.Name == "colSoLuongct")
            {

                if (e.Value != null)
                {
                    soluong = Convert.ToDouble(e.Value);
                    if (grv_Dthuocct.GetRowCellValue(e.RowHandle, colDongiact) != null)
                    {
                        dongia1 = Convert.ToDouble(grv_Dthuocct.GetRowCellValue(e.RowHandle, colDongiact));
                        grv_Dthuocct.SetRowCellValue(e.RowHandle, colThanhTien, soluong * dongia1);
                    }
                }
                else
                {
                    grv_Dthuocct.SetRowCellValue(e.RowHandle, colThanhTien, null);
                }
            }
            else if (e.Column.Name == "colLinhct")
            {
                if (e.Value != null)
                {
                    if (linh == -1 || linh == 0)
                    {
                        int idct = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(rowDThuocct, colIDDonct));
                        int sopl = 0;
                        int status = 0;
                        var dtct = data.DThuoccts.Where(p => p.IDDonct == idct).FirstOrDefault();
                        if (dtct != null)
                        {
                            sopl = dtct.SoPL;
                            status = dtct.Status ?? 0;
                            if (status != -1 && status != 0)
                            {

                                MessageBox.Show("Chỉ được sửa trạng thái giữa lĩnh và không lĩnh");
                                grv_Dthuocct.SetRowCellValue(grv_Dthuocct.FocusedRowHandle, colLinhct, linh);
                            }

                        }
                    }
                    else
                    {                       
                        grv_Dthuocct.SetRowCellValue(grv_Dthuocct.FocusedRowHandle, colLinhct, linh);
                    }

                }
            }
        }

        private void grv_Dthuoc_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grv_Dthuocct_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grv_Dthuoc_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        int rowsua = -1;
        private void grv_Dthuoc_Click(object sender, EventArgs e)
        {
            grv_Dthuoc_FocusedRowChanged(null, null);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            trangthai = 1;
            rowsua = -1;
        }

        private void grv_Dthuocct_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            grv_Dthuocct.SetRowCellValue(e.RowHandle, colLinhct, 0);
            grv_Dthuocct.SetRowCellValue(e.RowHandle, colSoPLct, 0);
            if (bnBHYT)
                grv_Dthuocct.SetRowCellValue(e.RowHandle, colBHYTct, 1);
            else
                grv_Dthuocct.SetRowCellValue(e.RowHandle, colBHYTct, 0);
        }

        private void btnSuaPL_Click(object sender, EventArgs e)
        {

            trangthai = 2;
            editable = true;
            setEditTable(editable);
            rowsua = grv_Dthuoc.FocusedRowHandle;
            grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Aquamarine;
        }

        private void btnXoaPL_Click(object sender, EventArgs e)
        {
            //int iddon = 0;
            //int row = grv_Dthuoc.FocusedRowHandle;
            //if (grv_Dthuoc.GetRowCellValue(row, colIDDon) != null)
            //{
            //    iddon = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(row, colIDDon).ToString());
            //    if (iddon > 0)
            //    {
            //        bool kt = true;
            //        for (int i = 0; i < grv_Dthuocct.RowCount; i++)
            //        {
            //            if (grv_Dthuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colIDDonct)) > 0)
            //            {
            //                kt = false;
            //                break;
            //            }

            //        }
            //        if (kt)
            //        {
            //            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đơn thuốc này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            //            if (dialogResult == DialogResult.Yes)
            //            {
            //                var don = data.DThuocs.Single(p => p.IDDon == iddon);
            //                List<DThuocct> ldonct = data.DThuoccts.Where(p => p.IDDon == iddon).ToList();
            //                foreach (DThuocct dt in ldonct)
            //                {
            //                    data.Remove(dt);
            //                }
            //                data.Remove(don);
            //                data.SaveChanges();
            //                grv_Dthuoc.DeleteRow(row);
            //                loadDthuocct();
            //            }
            //        }
            //        else
            //            MessageBox.Show("Đơn đã có thuốc, bạn không thể xóa");
            //    }
            //    else
            //    {
            //        GridView view = sender as GridView;
            //        view.DeleteRow(row);
            //        loadDthuocct();
            //    }
            //}
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            trangthai = 1;
            editable = true;
            setEditTable(false);

        }

        private void btnChucNang_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            EditorButton Button = e.Button;
            int index = editor.Properties.Buttons.IndexOf(e.Button);
            if (index == 0)//thêm
            {
                trangthai = 1;
                editable = true;
                setEditTable(editable);
                grc_Dthuoc.ForceInitialize();
                grv_Dthuoc.FocusedRowHandle = GridControl.NewItemRowHandle;
            }
            else if (index == 1)// sửa
            {
                trangthai = 2;
                editable = true;
                setEditTable(true);
                rowsua = grv_Dthuoc.FocusedRowHandle;
                grv_Dthuoc.Appearance.FocusedRow.BackColor = Color.Aquamarine;
            }
            else if (index == 2)// xóa
            {
                int iddon = 0;
                int row = grv_Dthuoc.FocusedRowHandle;
                if (grv_Dthuoc.GetRowCellValue(row, colIDDon) != null)
                {
                    iddon = Convert.ToInt32(grv_Dthuoc.GetRowCellValue(row, colIDDon).ToString());
                    if (iddon > 0)
                    {
                        bool kt = true;
                        for (int i = 0; i < grv_Dthuocct.RowCount; i++)
                        {
                            if (grv_Dthuocct.GetRowCellValue(i, colIDDonct) != null && Convert.ToInt32(grv_Dthuocct.GetRowCellValue(i, colIDDonct)) > 0)
                            {
                                kt = false;
                                break;
                            }

                        }
                        if (kt)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa đơn thuốc này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                var don = data.DThuocs.Single(p => p.IDDon == iddon);
                                List<DThuocct> ldonct = data.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                                foreach (DThuocct dt in ldonct)
                                {
                                    data.DThuoccts.Remove(dt);
                                }
                                data.DThuocs.Remove(don);
                                data.SaveChanges();
                                grv_Dthuoc.DeleteRow(row);
                                loadDthuocct();
                            }
                        }
                        else
                            MessageBox.Show("Đơn đã có thuốc, bạn không thể xóa");
                    }
                    else
                    {
                        // GridView view = sender as GridView;
                        // view.DeleteRow(row);
                        loadDthuocct();
                    }
                }
            }
        }

        private void grv_Dthuoc_ShownEditor(object sender, EventArgs e)
        {
            //    GridView view = (GridView)sender;

            //    if (view.FocusedColumn.FieldName == "MaDV")
            //    {
            //        LookUpEdit edit;

            //        if (view.ActiveEditor is LookUpEdit)
            //        {
            //            edit = view.ActiveEditor as LookUpEdit; // (LookUpEdit)view.ActiveEditor;

            //            int makhoxuat = 0;

            //            if (view.GetRowCellValue(view.FocusedRowHandle, "MaKXuat") != null)
            //            {
            //                makhoxuat = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "MaKXuat"));
            //                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //                var qdv = (from nd in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makhoxuat)
            //                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
            //                           join dv in data.DichVus on ndct.MaDV equals dv.MaDV
            //                           group dv by dv into kq
            //                           select new { MaDV = kq.Key.MaDV, kq.Key.TenDV }).ToList();
            //                edit.Properties.DataSource = qdv;
            //                edit.Properties.DisplayMember = "TenDV";
            //                edit.Properties.ValueMember = "MaDV";
            //                edit.Properties.ForceInitialize();
            //                edit.Properties.PopulateColumns();
            //                //edit.Properties.ShowPopup();
            //            }              

            //        }
            //    }
        }

        private void grv_Dthuoc_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == colKhoXuatct)
            {
                e.RepositoryItem = lupDichVu;
                e.RepositoryItem.NullText = "";
            }
        }

        private void grv_Dthuocct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int row = e.RowHandle;

            if (e.Column.Name == "colXoact")
            {
                int iddonct = 0;

                if (grv_Dthuocct.GetRowCellValue(row, colIDDonct) != null)
                {
                    iddonct = Convert.ToInt32(grv_Dthuocct.GetRowCellValue(row, colIDDonct).ToString());
                    if (iddonct > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Xóa thuốc " + grv_Dthuocct.GetRowCellDisplayText(row, coltenDVct) + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var donct = data.DThuoccts.Single(p => p.IDDonct == iddonct);
                            data.DThuoccts.Remove(donct);
                            data.SaveChanges();
                            loadDthuocct();
                        }
                    }
                    else
                    {
                        GridView view = sender as GridView;
                        view.DeleteRow(row);
                    }
                }
            }
            else
                if (e.Column.Name == "colKhoXuatct")
                {
                    // grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = true;
                    if (grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit == true)
                    {
                        if (!string.IsNullOrEmpty(grv_Dthuocct.GetRowCellDisplayText(grv_Dthuocct.FocusedRowHandle, coltenDVct)))
                            grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = false;
                        else
                            grv_Dthuocct.Columns["MaKXuat"].OptionsColumn.AllowEdit = true;

                    }
                }

        }

        private void lblIDDon_TextChanged(object sender, EventArgs e)
        {
            if ((change > 0 || changect > 0) && trangthai != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn chưa lưu dữ liệu, bạn có muốn lưu lại không ?", "Xác nhận lưu", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    btnLuu_Click(null, null);
                }
                else
                {
                    change = 0;
                    changect = 0;
                    loadDonThuoc();
                    trangthai = 0;

                }
            }
        }





    }

}