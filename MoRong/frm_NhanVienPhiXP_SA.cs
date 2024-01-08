using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
//using  Library_CLS;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace QLBV.FormThamSo
{
    public partial class frm_NhanVienPhiXP_SA : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan> _listDSBN = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan>();// thông tin chung của bệnh nhân
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> _listThuoc = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> _listThuocNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> _listDV = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> _listDVNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS> _listCLS = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS>();
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh> _listDienBienBenh = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh>();
        List<DichVu> _listDV_SQL = new List<DichVu>();
        //  List<DichVu> _listDV_Insert = new List<DichVu>();
        public frm_NhanVienPhiXP_SA()
        {
            InitializeComponent();


        }
        int load = 0;
        private void frm_LayBenhNhanXaPhuongcs_Load(object sender, EventArgs e)
        {
            //data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _listDV_SQL = data.DichVus.ToList();
            if (DungChung.Bien.xmlFilePath_LIS.Length > 6)
                txtFilePath.Text = DungChung.Bien.xmlFilePath_LIS[5];
            if (DungChung.Bien.xmlFilePath_LIS.Length > 7)
                txt_bak.Text = DungChung.Bien.xmlFilePath_LIS[6];
            loadDanhSachBenhNhan(txtFilePath.Text);
            load = 1;
        }
        private void loadDanhSachBenhNhan(string strPath)
        {
            _listDVNotExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
            if (strPath != "")
            {
                DirectoryInfo d = new DirectoryInfo(strPath);//Assuming Test is your Folder//(@"F:\\XML\");
                List<FileInfo> Files = new List<FileInfo>();
                try
                {
                    Files = d.GetFiles("*.xml").OrderBy(p => p.LastWriteTime).ToList(); //Getting Text files    
                }
                catch (Exception) { return; }

                _listDSBN = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan>();
                _listThuoc = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
                _listDV = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
                _listCLS = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS>();
                _listDienBienBenh = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh>();

                if (Files.Count > 0)
                {
                    foreach (FileInfo file in Files)
                    {
                        readFileXML(file.FullName);
                    }
                }


                #region xuất ra báo cáo danh sách dịch vụ chưa có trong danh mục

                //BaoCao.rep_DanhMucDVChuaCapNhat rep = new BaoCao.rep_DanhMucDVChuaCapNhat();
                List<DichVu> listDVRep = new List<DichVu>();
                foreach (var n in _listDVNotExist)
                {
                    DichVu dv = new DichVu();
                    //dv.MaDV = n.Ma_dich_vu;
                    dv.MaQD = n.Ma_dich_vu;
                    dv.TenDV = n.Ten_dich_vu;
                    dv.DonVi = n.Don_vi_tinh;
                    dv.DonGia = n.Don_gia;
                    listDVRep.Add(dv);

                }
                foreach (var n in _listThuocNotExist)
                {
                    DichVu dv = new DichVu();
                    // dv.MaDV = n.Ma_thuoc;
                    dv.MaDV = n.Ma_DV;
                    dv.TenDV = n.Ten_thuoc;
                    dv.DonVi = n.Don_vi_tinh;
                    dv.DonGia = n.Don_gia;
                    listDVRep.Add(dv);
                }
                var qdv = (from n in listDVRep select new { n.MaDV, n.TenDV, n.DonVi, n.DonGia }).Distinct().OrderBy(p => p.TenDV).ToList();
                if (qdv.Count > 0)
                {
                    string[] _arr = new string[] { "0", "@", "@", "@", "0" };
                    string[] _tieude = { "STT", "Mã QĐ", "Tên thuốc/ dịch vụ", "Đơn vị tính", "Đơn giá" };
                    int num = 0;
                    int[] _arrWidth = new int[] { };
                    DungChung.Bien.MangHaiChieu = new Object[qdv.Count, 5];
                    foreach (var r in qdv)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                        num++;

                    }
                    //frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh sách dịch vụ chưa cập nhật", "C:\\DSDichVu_QD.xls", true, this.Name);
                    //rep.para_count.Value = qdv.Count().ToString();
                    //rep.DataSource = qdv;
                    //rep.bindingdata();
                    //rep.CreateDocument();
                    //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //frm.ShowDialog();
                }
                #endregion
                grcDSBenhNhan.DataSource = _listDSBN;

            }
        }
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs> _listAllErr = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs>();// tất cả lỗi của tất cả các file trong 1 lần đọc
        List<Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs> _list1BNErr = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs>();// tất cả lỗi của 1  các file XML
        private void readFileXML(string filePath)
        {
            _list1BNErr = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs>();
            XDocument doc = new XDocument();
            try
            {
                doc = XDocument.Load(filePath);
                List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh> listDienBienBenh = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_DienBienBenh(doc);
                List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan> listDSBN = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_ThongTinBenhNhan(doc);/////////////////
                List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> listThuoc = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTThuoc(doc);//////////////////
                List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> listDV = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTDV(doc);/////////////////
                List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS> listCLS = Lib_QLBV_Connect_XP.clsKetNoiXP.fn_CTCLS(doc);/////////////////Current

                foreach (Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV dv in listDV)
                {
                    List<DichVu> q1 = _listDV_SQL.Where(p => p.MaQD == dv.Ma_dich_vu).ToList();
                    DichVu dichvu = new DichVu();
                    if (q1.Count > 0)
                    {
                        dichvu = q1.First();
                        dv.Ma_DV = dichvu.MaDV;
                    }
                    else // Chưa có dịch vụ có mã QD = dv.Ma_DV
                    {
                        _listDVNotExist.Add(dv);
                        Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs er = new Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs();
                        er.ID = dv.Ma_lk;
                        er.ColName = dv.Ma_dich_vu;
                        er.Ms = dv.Ten_dich_vu;
                        _list1BNErr.Add(er);
                    }
                }
                foreach (Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc thuoc in listThuoc)
                {

                    List<DichVu> q1 = _listDV_SQL.Where(p => thuoc.Ma_thuoc.Trim() != "" && p.MaQD == thuoc.Ma_thuoc).ToList();
                    DichVu dichvu = new DichVu();
                    if (q1.Count > 0)
                    {
                        dichvu = q1.First();
                        // thuoc.Ma_thuoc = dichvu.MaDV;
                        thuoc.Ma_DV = dichvu.MaDV;
                    }
                    else // Chưa có dịch vụ có mã QD = dv.Ma_DV
                    {
                        _listThuocNotExist.Add(thuoc);
                        Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs er = new Lib_QLBV_Connect_XP.clsKetNoiXP.ErrMs();
                        er.ID = thuoc.Ma_lk;
                        er.ColName = thuoc.Ma_thuoc;
                        er.Ms = thuoc.Ten_thuoc;
                        _list1BNErr.Add(er);
                    }
                }
                //add lỗi vào _listAllErr
                if (_list1BNErr.Count > 0)
                    _listAllErr.AddRange(_list1BNErr);

                // get ds BN
                _listDSBN.AddRange(listDSBN);
                _listThuoc.AddRange(listThuoc);
                _listDV.AddRange(listDV);
                _listCLS.AddRange(listCLS);
                _listDienBienBenh.AddRange(listDienBienBenh);
            }
            catch (Exception es)
            {
                string s = es.ToString();
            }
        }

        int _row = -1;
        private void hplView_Click(object sender, EventArgs e)
        {

        }

        private void grvDSBenhNhan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int row = e.RowHandle;
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan> listDSBN = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.ThongTinBenhNhan>();// thông tin chung của bệnh nhân
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> listThuoc = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> listDV = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS> listCLS = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CT_CLS>();
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh> listDienBienBenh = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.DienBienBenh>();

            if (e.Column.Name == "colXemCT")
            {
                string malk = "";

                if (grvDSBenhNhan.GetRowCellValue(row, colMaLK) != null)
                {
                    malk = grvDSBenhNhan.GetRowCellValue(row, colMaLK).ToString();
                    listDSBN = _listDSBN.Where(p => p.Ma_lk == malk).ToList();
                    listThuoc = _listThuoc.Where(p => p.Ma_lk == malk).ToList();
                    listDV = _listDV.Where(p => p.Ma_lk == malk).ToList();
                    listCLS = _listCLS.Where(p => p.Ma_lk == malk).ToList();
                    listDienBienBenh = _listDienBienBenh.Where(p => p.Ma_lk == malk).ToList();
                    frm_NhanVPhiXaPhuong_SA_ChiTiet frm = new frm_NhanVPhiXaPhuong_SA_ChiTiet(listDSBN, listThuoc, listDV, listCLS, listDienBienBenh);
                    frm.Text = "Chi tiết bệnh nhân xã phường _ " + malk;
                    frm.ShowDialog();
                }
            }
        }
        private void btnChonTatCa_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDSBenhNhan.RowCount; i++)
            {
                grvDSBenhNhan.SetRowCellValue(i, colCheck, true);
            }
        }
        private void btnBoChon_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDSBenhNhan.RowCount; i++)
            {
                grvDSBenhNhan.SetRowCellValue(i, colCheck, false);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<string> _listMalk = new List<string>();
            List<BenhNhan> _listBenhNhan = new List<BenhNhan>();
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV> listDV_notExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTDV>();// những dịch vụ của những bệnh nhân được chọn không có mã dịch vụ tương ứng
            List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc> listThuoc_notExist = new List<Lib_QLBV_Connect_XP.clsKetNoiXP.CTThuoc>();

            var qKPhong = data.KPhongs.ToList();
            var qDTBN = data.DTBNs.ToList();
            for (int i = 0; i < grvDSBenhNhan.RowCount; i++)
            {
                if (Convert.ToBoolean(grvDSBenhNhan.GetRowCellValue(i, colCheck)) && grvDSBenhNhan.GetRowCellValue(i, colMaLK) != null)
                    _listMalk.Add(grvDSBenhNhan.GetRowCellValue(i, colMaLK).ToString());
            }
            //Kiểm tra các bệnh nhân được kê đơn thuốc (thuốc +dịch vụ) có mã DV đã tồn tại


            List<string> q1 = (from bn in _listMalk
                      join thuoc in _listThuoc on bn equals thuoc.Ma_lk
                      join dv in _listDV_SQL on thuoc.Ma_DV equals dv.MaDV
                      select bn).Distinct().ToList();

            List<string> q2 = (from bn in _listMalk
                      join dvu in _listDV on bn equals dvu.Ma_lk
                      join dv in _listDV_SQL on dvu.Ma_DV equals dv.MaDV
                      select bn).Distinct().ToList();

            q1.AddRange(q2);
            q1 = q1.Distinct().ToList();
            var q = (from bn in _listDSBN
                     join mabn in q1 on bn.Ma_lk equals mabn
                     select bn).Distinct().ToList();
 
            listDV_notExist = (from n in _listDVNotExist join m in _listMalk on n.Ma_lk equals m select n).ToList();
            listThuoc_notExist = (from n in _listThuocNotExist join m in _listMalk on n.Ma_lk equals m select n).ToList();
            // danh sách bệnh nhân có mã dịch vụ, thuốc ko có mã danh mục tương đương
            List<string> _listMaBN_NotExit = listDV_notExist.Select(p => p.Ma_lk).ToList();
            _listMaBN_NotExit.AddRange(listThuoc_notExist.Select(p => p.Ma_lk).ToList());
         
            //
            #region xuất ra báo cáo danh sách dịch vụ chưa có trng danh mục
            List<DichVu> listDVRep = new List<DichVu>();
            #region xuất báo cáo những dịch vụ chưa tồn tại
            foreach (var n in listDV_notExist)
            {
                DichVu dv = new DichVu();
                // dv.MaDV = n.Ma_dich_vu;
                dv.MaDV = n.Ma_DV;
                dv.TenDV = n.Ten_dich_vu;
                dv.DonVi = n.Don_vi_tinh;
                dv.DonGia = n.Don_gia;
                listDVRep.Add(dv);

            }
            foreach (var n in listThuoc_notExist)
            {
                DichVu dv = new DichVu();
                //dv.MaDV = n.Ma_thuoc;
                dv.MaDV = n.Ma_DV;
                dv.TenDV = n.Ten_thuoc;
                dv.DonVi = n.Don_vi_tinh;
                dv.DonGia = n.Don_gia;
                listDVRep.Add(dv);
            }


            var qdv = (from n in listDVRep select new { n.MaDV, n.TenDV, n.DonVi, n.DonGia }).Distinct().OrderBy(p => p.TenDV).ToList();
            if (qdv.Count > 0)
            {
                string[] _arr = new string[] { "0", "@", "@", "@" };
                string[] _tieude = { "STT", "Malk", "Mã dùng chung", "Tên thuốc/ dịch vụ" };
                int num = 0;
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[_listAllErr.Count+1, 4];
                foreach(var tit in _tieude)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = _tieude[0];
                    DungChung.Bien.MangHaiChieu[num, 1] = _tieude[1];
                    DungChung.Bien.MangHaiChieu[num, 2] = _tieude[2];
                    DungChung.Bien.MangHaiChieu[num, 3] = _tieude[3];
                }
                num = 1;
                foreach (var r in _listAllErr)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.ID;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.ColName;// mã dịch vụ
                    DungChung.Bien.MangHaiChieu[num, 3] = r.Ms;//tên dịch vụ
                   
                    num++;
                }
                string path = txtFilePath.Text + "\\" + "DSDichVu_QD.xls";
                QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh sách dịch vụ chưa cập nhật", path, true);
                frmIn frm = new frmIn();
              //  frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh sách dịch vụ chưa cập nhật", path, true, this.Name);
                BaoCao.rep_DanhMucDVChuaCapNhat rep = new BaoCao.rep_DanhMucDVChuaCapNhat();
                rep.para_count.Value = qdv.Count().ToString();
                rep.DataSource = qdv;
                rep.bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                #region in gộp dịch vụ không tồn tại
                //string[] _arr = new string[] { "0", "@", "@", "@", "0" };
                //string[] _tieude = { "STT", "Mã QĐ", "Tên thuốc/ dịch vụ", "Đơn vị tính", "Đơn giá" };
                //int num = 0;
                //int[] _arrWidth = new int[] { };
                //DungChung.Bien.MangHaiChieu = new Object[qdv.Count, 5];
                //foreach (var r in qdv)
                //{
                //    DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                //    DungChung.Bien.MangHaiChieu[num, 1] = r.MaDV;
                //    DungChung.Bien.MangHaiChieu[num, 2] = r.TenDV;
                //    DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                //    DungChung.Bien.MangHaiChieu[num, 4] = r.DonGia;
                //    num++;
                //}
                //frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Danh sách dịch vụ chưa cập nhật", "C:\\DSDichVu_QD.xls", true, this.Name);
                //BaoCao.rep_DanhMucDVChuaCapNhat rep = new BaoCao.rep_DanhMucDVChuaCapNhat();
                //rep.para_count.Value = qdv.Count().ToString();
                //rep.DataSource = qdv;
                //rep.bindingdata();
                //rep.CreateDocument();
                //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //frm.ShowDialog();
                #endregion

            }
            #endregion
            #endregion

            if (q.Count > 0)
            {
                DateTime ngayra = new DateTime();
                List<string> _lMalk = _listMaBN_NotExit.Distinct().ToList();
                int soBN = 0;
                try
                {
                    foreach (var n in q)
                    {
                        if (!_lMalk.Contains(n.Ma_lk))// kiểm tra bệnh nhân có tất cả các thuốc và dịch vụ có mã tương đương
                        {
                            soBN++;
                            if (n.Trangthai != 1)//xóa
                            {
                                xoaBNhan(n.Ma_lk);
                            }
                            else//thêm mới
                            {
                                #region thêm mới
                                //data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                //xóa bệnh nhân nếu đã tồn tại
                                if (data.BenhNhans.Where(p => p.Ma_lk == n.Ma_lk && p.MaKCB == n.Ma_cskcb).Count() > 0)
                                    xoaBNhan(n.Ma_lk);
                                #region add table BenhNhan

                                int _idBN = data.BenhNhans.Max(p => p.MaBNhan);
                                BenhNhan bn = new BenhNhan();
                                string[] formatDateTime = { "yyyyMMddHHmm" };
                                string[] fomatDate = { "yyyyMMdd" };
                                DateTime date;
                                DateTime hanbhtu = new DateTime();
                                DateTime hanbhden = new DateTime();
                                //DateTime ngaytt = new DateTime();
                                string ngaysinh = "", thangsinh = "", namsinh = "";
                                int maBN = 0;
                                maBN = Convert.ToInt32((_idBN + 1).ToString());
                                bn.MaBNhan = maBN;
                                bn.Ma_lk = n.Ma_lk;
                                bn.TenBNhan = n.Ho_ten;
                                bn.MaKCB = n.Ma_cskcb; //"KCB76767";
                                if (n.Ma_dkbd != null && n.Ma_dkbd.Length >= 2)
                                {
                                    if (n.Ma_dkbd == DungChung.Bien.MaBV)
                                        bn.NoiTinh = 1;
                                    else if (DungChung.Bien.MaBV.Substring(0, 2) == n.Ma_dkbd.Substring(0, 2))
                                        bn.NoiTinh = 2;
                                    else
                                        bn.NoiTinh = 3;
                                }
                                bn.Tuoi = Convert.ToInt32(n.Ngay_vao.ToString().Substring(0, 4)) - Convert.ToInt32(n.Ngay_sinh.ToString().Substring(0, 4));
                                bn.GTinh = n.Gioi_tinh == 2 ? 0 : 1;
                                bn.DChi = n.Dia_chi;
                                var _Kphong = data.KPhongs.Where(p => p.MaQD == n.Ma_cskcb).FirstOrDefault();
                                if (_Kphong != null)
                                    bn.MaKP = _Kphong.MaKP;
                                bn.DTuong = String.IsNullOrEmpty(n.Ma_the) ? "Dịch vụ" : "BHYT";
                                if (n.Ngay_vao != null)
                                {
                                    if (n.Ngay_vao.ToString().Length == 8)
                                        bn.NNhap = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    else if (n.Ngay_vao.ToString().Length == 12)
                                        bn.NNhap = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                }
                                bn.SThe = n.Ma_the;
                                bn.MaCS = n.Ma_dkbd;

                                if (!n.Ma_loai_kcb.Trim().Equals(string.Empty))
                                {


                                    if (Convert.ToInt32(n.Ma_loai_kcb) == 3)
                                        bn.NoiTru = 1;
                                    else if (Convert.ToInt32(n.Ma_loai_kcb) == 2)
                                        bn.NoiTru = 1;
                                    else if (Convert.ToInt32(n.Ma_loai_kcb) == 1)// ??? Khám bệnh
                                        bn.NoiTru = 0;
                                }
                                else
                                {
                                    bn.NoiTru = 2;
                                }

                                //  bn.MaKP = n.Ma_cskcb;
                                bn.MaBV = n.Ma_cskcb;
                                if (!n.Ma_lydo_vvien.Trim().Equals(string.Empty))
                                {


                                    if (Convert.ToInt32(n.Ma_lydo_vvien) == 3)
                                        bn.Tuyen = 2;
                                    else if (Convert.ToInt32(n.Ma_lydo_vvien) == 1)// đúng tuyến
                                        bn.Tuyen = 1;// đúng tuyến
                                    else if (Convert.ToInt32(n.Ma_lydo_vvien) == 2)// cấp cứu
                                        bn.Tuyen = 1;
                                }
                                else
                                {
                                    bn.Tuyen = 3;
                                }
                                bn.CapCuu = bn.Tuyen == 2 ? 1 : 0;
                                // thiếu nội tỉnh
                                bn.Status = 3;
                                if (!String.IsNullOrEmpty(n.Gt_the_tu))
                                {
                                    if (DateTime.TryParseExact(n.Gt_the_tu,
                                                           fomatDate,
                                                           System.Globalization.CultureInfo.InvariantCulture,
                                                           System.Globalization.DateTimeStyles.None,
                                                           out date))
                                        hanbhtu = DateTime.ParseExact(n.Gt_the_tu.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    bn.HanBHTu = hanbhtu;
                                }

                                if (!String.IsNullOrEmpty(n.Gt_the_den))
                                {
                                    if (DateTime.TryParseExact(n.Gt_the_den,
                                                           fomatDate,
                                                           System.Globalization.CultureInfo.InvariantCulture,
                                                           System.Globalization.DateTimeStyles.None,
                                                           out date))
                                        hanbhden = DateTime.ParseExact(n.Gt_the_den.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    bn.HanBHDen = hanbhden;

                                }
                                if (n.Ngay_sinh != null)
                                {
                                    if (n.Ngay_sinh.Length == 8)
                                    {
                                        ngaysinh = n.Ngay_sinh.ToString().Substring(6, 2);
                                        thangsinh = n.Ngay_sinh.ToString().Substring(4, 2);
                                        namsinh = n.Ngay_sinh.ToString().Substring(0, 4);
                                        bn.NgaySinh = ngaysinh;
                                        bn.ThangSinh = thangsinh;
                                        bn.NamSinh = namsinh;
                                    }
                                    else if (n.Ngay_sinh.Length == 4)
                                    {
                                        namsinh = n.Ngay_sinh;
                                        bn.NamSinh = namsinh;
                                    }
                                }
                                bn.MaBV = n.Ma_cskcb;
                                // Sửa sau
                                //if (qKPhong.Where(p => p.MaKP ==  n.Ma_cskcb && p.PLoai == "Xã phường").Count() > 0)
                                bn.TuyenDuoi = 1;
                                //else if (qKPhong.Where(p => p.MaKP == n.Ma_cskcb && p.PLoai == "PK khu vực").Count() > 0)
                                //    bn.TuyenDuoi = 2;

                                if (n.Ma_the != null && n.Ma_the.Length > 2)
                                    bn.MucHuong = Convert.ToInt16(n.Ma_the.ToString().Substring(2, 1));
                                bn.KhuVuc = n.Ma_khuvuc;

                                if (Convert.ToInt32(n.Ma_loai_kcb.Equals(string.Empty) ? "2" : n.Ma_loai_kcb) == 2)
                                    bn.DTNT = true;
                                else if (Convert.ToInt32(n.Ma_loai_kcb.Equals(string.Empty) ? "2" : n.Ma_loai_kcb) == 1)// ??? Khám bệnh
                                    bn.DTNT = false;
                                if (n.Ma_the != null)
                                {
                                    if (n.Ma_the.Length > 1)
                                    {
                                        bn.MaDTuong = n.Ma_the.ToString().Substring(0, 2);
                                    }
                                    // id person
                                    var qP = data.People.Where(p => p.SThe == n.Ma_the).ToList();
                                    if (qP.Count > 0)
                                    {
                                        bn.IDPerson = qP.OrderByDescending(p => p.IDPerson).First().IDPerson;
                                    }
                                    else
                                    {
                                        // insert vào bảng person
                                        Person ps = new Person();
                                        ps.SThe = n.Ma_the;
                                        if (n.Ma_the.Length > 1)
                                        {
                                            ps.MaDTuong = n.Ma_the.ToString().Substring(0, 2);
                                        }
                                        ps.TenBNhan = n.Ho_ten;
                                        ps.GTinh = n.Gioi_tinh == 2 ? 0 : 1;
                                        ps.DChi = n.Dia_chi;
                                        ps.HanBHTu = hanbhtu;
                                        ps.HanBHDen = hanbhden;
                                        ps.MaCS = n.Ma_dkbd;
                                        int num;
                                        if (Int32.TryParse(namsinh, out num))
                                            ps.NSinh = Convert.ToInt32(namsinh);
                                        ps.NgaySinh = ngaysinh;
                                        ps.ThangSinh = thangsinh;
                                        ps.KhuVuc = n.Ma_khuvuc;
                                        //thiếu ngày cấp, Status, mã tỉnh, mã huyện, mã xã
                                        data.People.Add(ps);
                                        if (data.SaveChanges() > 0)
                                        {
                                            int idPs = data.People.OrderByDescending(p => p.IDPerson).First().IDPerson;
                                            bn.IDPerson = idPs;
                                        }
                                    }
                                }
                                //idDTBN
                                if (n.Ma_the != null)
                                {
                                    if (qDTBN.Where(p => p.DTBN1 == "BHYT").Count() > 0)
                                        bn.IDDTBN = qDTBN.Where(p => p.DTBN1 == "BHYT").First().IDDTBN;
                                }
                                else
                                {
                                    if (qDTBN.Where(p => p.DTBN1 == "Dịch vụ").Count() > 0)
                                        bn.IDDTBN = qDTBN.Where(p => p.DTBN1 == "Dịch vụ").First().IDDTBN;
                                }
                                data.BenhNhans.Add(bn);
                                if (data.SaveChanges() <= 0)
                                    MessageBox.Show("Lỗi thêm mới bệnh bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")");

                                #endregion add table BenhNhan
                                #region add table DThuoc

                                var qdonThuocCT = _listThuoc.Where(p => p.Ma_lk == n.Ma_lk).ToList();
                                var qDichvuCT = _listDV.Where(p => p.Ma_lk == n.Ma_lk).ToList();
                                var qdonThuoc = (from a in qdonThuocCT
                                                 group a by new { a.Ngay_yl } into kq
                                                 select new
                                                 {
                                                     kq.Key.Ngay_yl,
                                                     Ma_bac_si = kq.Max(p => p.Ma_bac_si),
                                                     Ma_khoa = kq.Max(p => p.Ma_khoa)
                                                 }
                                                ).ToList();

                                var qDichvu = (from a in qDichvuCT
                                               group a by new { a.Ngay_yl } into kq
                                               select new
                                               {
                                                   kq.Key.Ngay_yl,
                                                   Ma_bac_si = kq.Max(p => p.Ma_bac_si),
                                                   Ma_khoa = kq.Max(p => p.Ma_khoa)
                                               }
                                                ).ToList();


                                foreach (var m in qdonThuoc)
                                {
                                    DThuoc don = new DThuoc();
                                    don.MaKP = getMaKP(m.Ma_khoa);
                                    don.MaBNhan = maBN;
                                    don.MaCB = m.Ma_bac_si;
                                    if (!String.IsNullOrEmpty(m.Ngay_yl))
                                    {
                                        if (m.Ngay_yl.ToString().Length == 12)
                                            don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                        else if (m.Ngay_yl.ToString().Length == 8)
                                            don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    }
                                    don.MaKXuat = 0;
                                    //don.Status = -1;
                                    don.KieuDon = -1;
                                    don.LoaiDuoc = -1;
                                    //don.SoPL = -1;
                                    don.PLDV = 1;
                                    data.DThuocs.Add(don);
                                    if (data.SaveChanges() > 0)
                                    {
                                        #region add table DThuocct
                                        int _idDon = data.DThuocs.Where(p => p.MaBNhan == maBN).OrderByDescending(p => p.IDDon).First().IDDon;
                                        var qdonCTByIDDon = qdonThuocCT.Where(p => p.Ngay_yl == m.Ngay_yl).ToList();
                                        foreach (var o in qdonCTByIDDon)
                                        {
                                            DThuocct donct = new DThuocct();
                                            donct.IDDon = _idDon;
                                            //  donct.MaDV = o.Ma_thuoc;
                                            donct.MaDV = o.Ma_DV;
                                            donct.DonVi = o.Don_vi_tinh;
                                            donct.DonGia = o.Don_gia;
                                            donct.SoLuong = o.So_luong;
                                            double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                            double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                            donct.ThanhTien = _thanhtien;
                                            donct.TienBH = _tienbh;
                                            donct.TienBN = _thanhtien - _tienbh;
                                            donct.TrongBH = n.Muc_huong > 0 ? 1 : 0; //????????
                                            data.DThuoccts.Add(donct);
                                            if (data.SaveChanges() <= 0)
                                                MessageBox.Show("Lỗi thêm mới thuốc bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);
                                        }

                                        #endregion add table DThuocct
                                    }
                                }
                                #region add dịch vụ
                                foreach (var m in qDichvu)
                                {
                                    DThuoc don = new DThuoc();
                                    // don.MaKP = m.Ma_khoa;

                                    don.MaBNhan = maBN;
                                    don.MaCB = m.Ma_bac_si;
                                    if (!String.IsNullOrEmpty(m.Ngay_yl))
                                    {
                                        if (m.Ngay_yl.ToString().Length == 12)
                                            don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                        else if (m.Ngay_yl.ToString().Length == 8)
                                            don.NgayKe = DateTime.ParseExact(m.Ngay_yl.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    }
                                    don.MaKXuat = 0;
                                    //don.Status = -1;
                                    don.KieuDon = -1;
                                    don.LoaiDuoc = -1;
                                    //don.SoPL = -1;
                                    don.PLDV = 2;
                                    data.DThuocs.Add(don);
                                    if (data.SaveChanges() > 0)
                                    {
                                        #region add table DThuocct
                                        int _idDon = data.DThuocs.Where(p => p.MaBNhan == maBN).OrderByDescending(p => p.IDDon).First().IDDon;
                                        var qdonCTDVByIDDon = qDichvuCT.Where(p => p.Ngay_yl == m.Ngay_yl).ToList();
                                        foreach (var o in qdonCTDVByIDDon)
                                        {
                                            DThuocct donct = new DThuocct();
                                            donct.IDDon = _idDon;
                                            // donct.MaDV = o.Ma_dich_vu;
                                            donct.MaDV = o.Ma_DV;
                                            donct.DonVi = o.Don_vi_tinh;
                                            donct.DonGia = o.Don_gia;
                                            donct.SoLuong = o.So_luong;
                                            double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                            double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                            donct.ThanhTien = _thanhtien;
                                            donct.TienBH = _tienbh;
                                            donct.TienBN = _thanhtien - _tienbh;
                                            donct.TrongBH = n.Muc_huong > 0 ? 1 : 0; //????????
                                            data.DThuoccts.Add(donct);
                                            if (data.SaveChanges() <= 0)
                                                MessageBox.Show("Lỗi thêm mới dich vụ bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);
                                        }
                                        #endregion add table DThuocct
                                    }
                                }
                                #endregion add dịch vụ
                                #endregion add table DThuoc
                                #region add table VienPhi
                                VienPhi vp = new VienPhi();
                                if (!String.IsNullOrEmpty(n.Ngay_ra))
                                {
                                    if (n.Ngay_ra.ToString().Length == 12)
                                        ngayra = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                    else if (n.Ngay_ra.ToString().Length == 8)
                                        ngayra = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    vp.NgayRa = ngayra;
                                }
                                if (!String.IsNullOrEmpty(n.Ngay_ttoan))
                                {
                                    if (n.Ngay_ttoan.ToString().Length == 12)
                                        vp.NgayTT = DateTime.ParseExact(n.Ngay_ttoan.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                    else if (n.Ngay_ttoan.ToString().Length == 8)
                                        vp.NgayTT = DateTime.ParseExact(n.Ngay_ttoan.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                }
                                vp.MaBNhan = maBN;
                                // vp.MaKP = n.Ma_cskcb;
                                // vp.MucHuong = Convert.ToByte(n.Muc_huong);
                                data.VienPhis.Add(vp);
                                if (data.SaveChanges() > 0)
                                {
                                    int _idVP = data.VienPhis.Where(p => p.MaBNhan == maBN).Where(p => p.MaBNhan == maBN).OrderByDescending(p => p.idVPhi).First().idVPhi;
                                    #region add table VienPhict
                                    foreach (var o in qdonThuocCT)
                                    {
                                        VienPhict vpct = new VienPhict();
                                        vpct.idVPhi = _idVP;
                                        // vpct.MaDV = o.Ma_thuoc;
                                        vpct.MaDV = o.Ma_DV;
                                        vpct.DonVi = o.Don_vi_tinh;
                                        vpct.DonGia = o.Don_gia;
                                        vpct.SoLuong = o.So_luong;
                                        double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                        double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                        vpct.ThanhTien = _thanhtien;
                                        vpct.TienBH = _tienbh;
                                        vpct.TienBN = _thanhtien - _tienbh;
                                        vpct.TrongBH = n.Muc_huong > 0 ? 1 : 0;
                                        //vpct.MaKP = o.Ma_khoa;
                                        data.VienPhicts.Add(vpct);
                                        if (data.SaveChanges() <= 0)
                                            MessageBox.Show("Lỗi thêm mới viện phí chi tiết bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);
                                    }
                                    foreach (var o in qDichvuCT)
                                    {
                                        VienPhict vpct = new VienPhict();
                                        vpct.idVPhi = _idVP;
                                        // vpct.MaDV = o.Ma_dich_vu;
                                        vpct.MaDV = o.Ma_DV;
                                        vpct.DonVi = o.Don_vi_tinh;
                                        vpct.DonGia = o.Don_gia;
                                        vpct.SoLuong = o.So_luong;
                                        double _tienbh = Math.Round(Math.Round(o.Thanh_tien, 3) * n.Muc_huong / 100, 3);
                                        double _thanhtien = Math.Round(o.Thanh_tien, 3);
                                        vpct.ThanhTien = _thanhtien;
                                        vpct.TienBH = _tienbh;
                                        vpct.TienBN = _thanhtien - _tienbh;
                                        vpct.TrongBH = n.Muc_huong > 0 ? 1 : 0;
                                        // vpct.MaKP = o.Ma_khoa;
                                        data.VienPhicts.Add(vpct);
                                        if (data.SaveChanges() <= 0)
                                            MessageBox.Show("Lỗi thêm mới viện phí chi tiết bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);
                                    }
                                    #endregion add table VienPhict
                                }
                                else
                                {
                                    MessageBox.Show("Lỗi thêm mới viện phí bệnh nhân " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);
                                }
                                #endregion add table VienPhi
                                #region add table Ravien
                                RaVien rv = new RaVien();
                                if (!String.IsNullOrEmpty(n.Ngay_ra))
                                {
                                    //if (n.Ngay_ra.ToString().Length == 12)
                                    //    rv.NgayRa = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                    //else if (n.Ngay_ra.ToString().Length == 8)
                                    //    rv.NgayRa = DateTime.ParseExact(n.Ngay_ra.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    vp.NgayRa = ngayra;
                                }
                                rv.MaBNhan = maBN;
                                if (_Kphong != null)
                                    rv.MaKP = _Kphong.MaKP;
                                rv.SoNgaydt = n.So_ngay_dtri;
                                if (n.Tinh_trang_rv == "2")
                                    rv.Status = 1;
                                else if (n.Tinh_trang_rv == "1")
                                    rv.Status = 2;
                                else if (n.Tinh_trang_rv == "3" || n.Tinh_trang_rv == "4")
                                    rv.Status = Convert.ToInt16(n.Tinh_trang_rv);
                                // rv.MaKP = n.Ma_cskcb;
                                rv.MaICD = n.Ma_benhkhac == "" ? n.Ma_benh : (n.Ma_benh + ";" + n.Ma_benhkhac);
                                rv.ChanDoan = n.Ten_benh;
                                if (n.Ket_qua_dtri == "1")
                                    rv.KetQua = "Khỏi";
                                else if (n.Ket_qua_dtri == "2")
                                    rv.KetQua = "Đỡ|Giảm";
                                else if (n.Ket_qua_dtri == "3")
                                    rv.KetQua = "Không T.đổi";
                                else if (n.Ket_qua_dtri == "4")
                                    rv.KetQua = "Nặng hơn";
                                else if (n.Ket_qua_dtri == "5")
                                    rv.KetQua = "Tử vong";
                                if (!String.IsNullOrEmpty(n.Ngay_vao))
                                {
                                    if (n.Ngay_vao.ToString().Length == 12)
                                        rv.NgayVao = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                    else if (n.Ngay_vao.ToString().Length == 8)
                                        rv.NgayVao = DateTime.ParseExact(n.Ngay_vao.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                }
                                data.RaViens.Add(rv);
                                if (data.SaveChanges() <= 0)
                                    MessageBox.Show("Lỗi thêm mới Bảng ra viện bệnh nhân: " + n.Ho_ten + " (" + n.Ma_lk + ")_Mã bệnh nhân :" + maBN);

                                #endregion add table Ravien
                                #endregion
                            }
                        }
                    }
                    MessageBox.Show("Đã thêm mới " + soBN + " bệnh nhân");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Không có bệnh nhân nào được thêm");
            }
        }

        private void xoaBNhan(string ma_lk)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region xóa
            List<BenhNhan> lBNxoa = data.BenhNhans.Where(p => p.Ma_lk == ma_lk).ToList();
            foreach (BenhNhan benhnhanXoa in lBNxoa)
            {
                RaVien rvXoa = data.RaViens.Where(p => p.MaBNhan == benhnhanXoa.MaBNhan).FirstOrDefault();
                if (rvXoa != null)
                    data.RaViens.Remove(rvXoa);
                List<VienPhi> lVPXoa = data.VienPhis.Where(p => p.MaBNhan == benhnhanXoa.MaBNhan).ToList();
                foreach (VienPhi vpXoa in lVPXoa)
                {
                    List<VienPhict> lVPctXoa = data.VienPhicts.Where(p => p.idVPhi == vpXoa.idVPhi).ToList();
                    foreach (VienPhict ct in lVPctXoa)
                        data.VienPhicts.Remove(ct);
                    data.VienPhis.Remove(vpXoa);
                }
                List<DThuoc> ldthuocXoa = data.DThuocs.Where(p => p.MaBNhan == benhnhanXoa.MaBNhan).ToList();
                foreach (DThuoc dthuocXoa in ldthuocXoa)
                {
                    List<DThuocct> ldthuocctXoa = data.DThuoccts.Where(p => p.IDDon == dthuocXoa.IDDon).ToList();
                    foreach (DThuocct ct in ldthuocctXoa)
                        data.DThuoccts.Remove(ct);
                    data.DThuocs.Remove(dthuocXoa);
                }
            }
            #endregion
        }
        private int getMaKP(string maKpQD)
        {
            int makp = 0;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qKP = data.KPhongs.Where(p => p.MaQD == maKpQD).ToList();
            if (qKP.Count > 0)
                makp = qKP.First().MaKP;
            return makp;

        }
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        private void btnChonDuongDan_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = "D:\\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.SelectedPath + "\\";
                loadDanhSachBenhNhan(txtFilePath.Text);
            }
        }

        private void lupBenhVien_EditValueChanged(object sender, EventArgs e)
        {
            if (load == 1)
                loadDanhSachBenhNhan(txtFilePath.Text);
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            //if (load == 1)
            //loadDanhSachBenhNhan(txtFilePath.Text);
        }

        private void btnFolderBack_Click(object sender, EventArgs e)
        {

            dialog.SelectedPath = "D:\\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.SelectedPath + "\\";
            }
        }

        private void txtFilePath_EditValueChanged(object sender, EventArgs e)
        {
            //if (load == 1)
            //loadDanhSachBenhNhan(txtFilePath.Text);
        }
    }
}