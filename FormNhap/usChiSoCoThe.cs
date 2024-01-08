using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using QLBV.Phieu;

namespace QLBV.FormDanhMuc
{
    public partial class usChiSoCoThe : DevExpress.XtraEditors.XtraUserControl
    {
        public usChiSoCoThe()
        {
            InitializeComponent();
        }

        public usChiSoCoThe(int mabn, int makp)
        {
            _maBN = mabn;
            _makp = makp;
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        int _id = 0;
        int _makp = 0;
        private void enableControl(bool T)
        {
            lupKP.Enabled = T;
            lupCB.Enabled = T;
            lupChamSoc.Enabled = T;
            lupBuong.Enabled = T;
            lupGiuong.Enabled = T;
            txtMach.Enabled = T;
            txtHuyetAp.Enabled = T;
            txtChieuCao.Enabled = T;
            txtHuyetApD.Enabled = T;
            txtCanNang.Enabled = T;
            txtnhipTho.Enabled = T;
            txtNhietDo.Enabled = T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            grcChiSoCoThe.Enabled = !T;
        }
        private void resetControl()
        {
            lupChamSoc.EditValue = 0;
            lupBuong.EditValue = 0;
            lupGiuong.EditValue = 0;
            lupKP.EditValue = _makp;
            lupCB.EditValue = "";
            dateEdit1.DateTime = DateTime.Now;
            txtMach.Text = string.Empty;
            txtHuyetAp.Text = string.Empty;
            txtChieuCao.Text = string.Empty;
            txtHuyetApD.Text = string.Empty;
            txtCanNang.Text = string.Empty;
            txtnhipTho.Text = string.Empty;
            txtNhietDo.Text = string.Empty;

        }
        #region
        private bool KTLuu()
        {
            if (lupKP.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng");
                lupKP.Focus();
                return false;
            }
            if (lupCB.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn cán bộ");
                lupCB.Focus();
                return false;
            }
            if (dateEdit1.Text == null || dateEdit1.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn ngày giờ theo dõi");
                dateEdit1.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtTenBuong.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập tên buồng");
            //    txtTenBuong.Focus();
            //    return false;
            //}


            return true;
        }

        #endregion


        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Buong> _lBuong = new List<Buong>();
        private void us_dmDanToc_Load(object sender, EventArgs e)
        {

            //this.txtMaBuong.Properties.Mask.EditMask = "[0-9]{1,6}";
            //this.txtMaBuong.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dateEdit1.DateTime = DateTime.Now;
            List<KPhong> _lkp = dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            lupKP.Properties.DataSource = _lkp;
            lupKP.EditValue = _makp;
            List<DienBien> qdienbien = (from db in dataContext.DienBiens.Where(p => p.MaBNhan == _maBN && p.Ploai == 1)
                                        select db
                          ).ToList();
            foreach (DienBien db in qdienbien)
            {
                var qcb = dataContext.CanBoes.Where(p => p.MaCB == db.MaCB).FirstOrDefault();
                if (qcb != null)
                    db.KetQua = qcb.TenCB;
                else
                    db.KetQua = "";
            }
            qdienbien.Insert(0, new DienBien { ID = 0, NgayNhap = null, KetQua = "", DienBien1 = "" });
            lupChamSoc.Properties.DataSource = qdienbien;

            lupGrKP.DataSource = _lkp;
            //List<Buong> _lBuong  = dataContext.Buongs.OrderBy(p => p.TenBuong).ToList();
            //_lBuong.Insert(0, new Buong { MaBuong = 0, TenBuong = "" });
            //lupGrBuong.DataSource = _lBuong;
            //List<Giuong> _lGiuong = dataContext.Giuongs.OrderBy(p => p.TenGiuong).ToList();
            //_lGiuong.Insert(0, new Giuong { MaGiuong = 0, TenGiuong = "" });
            //lupGrGiuong.DataSource = _lGiuong;

            List<CanBo> _lcb = dataContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            _lcb.Insert(0, new CanBo { MaCB = "", TenCB = "" });
            lupGrCB.DataSource = _lcb;
            lupCB.Properties.DataSource = _lcb;
            enableControl(false);

            List<ChiSoCoThe> qchisocothe = dataContext.ChiSoCoThes.Where(p => p.MaBNhan == _maBN).ToList();
            grcChiSoCoThe.DataSource = qchisocothe;
            loadChart();



        }


        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:

                        var ma = dataContext.ChiSoCoThes.Where(p => p.ID == _id).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Bạn kiểm tra lại trạng thái thêm hoặc sửa. ID = " + _id);
                        }
                        else
                        {
                            ChiSoCoThe bp = new ChiSoCoThe();
                            bp.MaBNhan = _maBN;
                            if (lupKP.EditValue != null)
                                bp.MaKP = Convert.ToInt32(lupKP.EditValue);
                            if (lupCB.EditValue != null)
                                bp.MaCB = lupCB.EditValue.ToString();
                            bp.NgayGioDo = dateEdit1.DateTime;
                            if (lupChamSoc.EditValue != null && lupChamSoc.EditValue.ToString() != "0")
                                bp.IDCS = Convert.ToInt32(lupChamSoc.EditValue.ToString());
                            if (txtMach.Text != null && txtMach.Text != "")
                                bp.Mach = Convert.ToInt32(txtMach.Text);
                            if (txtNhietDo.Text != null && txtNhietDo.Text != "")
                                bp.NhietDo = Convert.ToDouble(txtNhietDo.Text);
                            if (txtHuyetApD.Text != null && txtHuyetApD.Text != "")
                                bp.HuyetApD = Convert.ToInt32(txtHuyetApD.Text);
                            if (txtHuyetAp.Text != null && txtHuyetAp.Text != "")
                                bp.HuyetApT = Convert.ToInt32(txtHuyetAp.Text);
                            if (txtCanNang.Text != null && txtCanNang.Text != "")
                                bp.CanNang = Convert.ToDouble(txtCanNang.Text);
                            if (txtnhipTho.Text != null && txtnhipTho.Text != "")
                                bp.NhipTho = Convert.ToInt32(txtnhipTho.Text);
                            if (txtChieuCao.Text != null && txtChieuCao.Text != "")
                                bp.ChieuCao = Convert.ToDouble(txtChieuCao.Text);
                            //if (lupBuong.EditValue != null && lupBuong.EditValue.ToString() != "0")
                            //    bp.MaBuong = Convert.ToInt32(lupBuong.EditValue.ToString());
                            //if (lupGiuong.EditValue != null && lupGiuong.EditValue.ToString() != "0")
                            //    bp.MaGiuong = Convert.ToInt32(lupGiuong.EditValue.ToString()); 
                            dataContext.ChiSoCoThes.Add(bp);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");

                        }
                        break;
                    case 2:

                        ChiSoCoThe sua = dataContext.ChiSoCoThes.Where(p => p.ID == _id).FirstOrDefault();
                        if (sua != null)
                        {

                            if (lupKP.EditValue != null)
                                sua.MaKP = Convert.ToInt32(lupKP.EditValue);
                            else
                                sua.MaKP = null;
                            if (lupCB.EditValue != null)
                                sua.MaCB = lupCB.EditValue.ToString();
                            else
                                sua.MaCB = null;
                            sua.NgayGioDo = dateEdit1.DateTime;
                            if (lupChamSoc.EditValue != null && lupChamSoc.EditValue.ToString() != "0")
                                sua.IDCS = Convert.ToInt32(lupChamSoc.EditValue.ToString());
                            else
                                sua.IDCS = null;
                            if (txtMach.Text != null && txtMach.Text != "")
                                sua.Mach = Convert.ToInt32(txtMach.Text);
                            else
                                sua.Mach = null;
                            if (txtNhietDo.Text != null && txtNhietDo.Text != "")
                                sua.NhietDo = Convert.ToDouble(txtNhietDo.Text);
                            else
                                sua.NhietDo = null;
                            if (txtHuyetApD.Text != null && txtHuyetApD.Text != "")
                                sua.HuyetApD = Convert.ToInt32(txtHuyetApD.Text);
                            else
                                sua.HuyetApD = null;
                            if (txtHuyetAp.Text != null && txtHuyetAp.Text != "")
                                sua.HuyetApT = Convert.ToInt32(txtHuyetAp.Text);
                            else
                                sua.HuyetApT = null;
                            if (txtCanNang.Text != null && txtCanNang.Text != "")
                                sua.CanNang = Convert.ToDouble(txtCanNang.Text);
                            else
                                sua.CanNang = null;
                            if (txtChieuCao.Text != null && txtChieuCao.Text != "")
                                sua.ChieuCao = Convert.ToDouble(txtChieuCao.Text);
                            else sua.ChieuCao = null;
                            if (txtnhipTho.Text != null && txtnhipTho.Text != "")
                                sua.NhipTho = Convert.ToInt32(txtnhipTho.Text);
                            else
                                sua.NhipTho = null;
                            //thiếu chiều cao
                            if (lupBuong.EditValue != null && lupBuong.EditValue.ToString() != "0")
                                sua.MaBuong = Convert.ToInt32(lupBuong.EditValue.ToString());
                            else
                                sua.MaBuong = null;
                            if (lupGiuong.EditValue != null && lupGiuong.EditValue.ToString() != "0")
                                sua.MaGiuong = Convert.ToInt32(lupGiuong.EditValue.ToString());
                            else
                                sua.MaGiuong = null;
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");

                        }

                        break;
                }
                us_dmDanToc_Load(null, null);
                loadChart();
            }
        }

        private void grvDanToc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvChiSoCoThe.GetFocusedRowCellValue(colID) != null && grvChiSoCoThe.GetFocusedRowCellValue(colID).ToString() != "")
            {
                lblIDChiSo.Text = grvChiSoCoThe.GetFocusedRowCellValue(colID).ToString();
                lupKP.EditValue = 0;
                lupCB.EditValue = "";
                lupChamSoc.EditValue = 0;
                lupBuong.EditValue = 0;
                lupGiuong.EditValue = 0;
                txtMach.Text = null;
                txtHuyetAp.Text = null;
                txtNhietDo.Text = null;
                txtHuyetApD.Text = null;
                txtChieuCao.Text = null;
                txtCanNang.Text = null;
                txtnhipTho.Text = null;

                int ot;
                if (int.TryParse(lblIDChiSo.Text, out ot))
                {
                    _id = Convert.ToInt32(lblIDChiSo.Text);
                    var chiso = dataContext.ChiSoCoThes.Where(p => p.ID == _id).FirstOrDefault();
                    if (chiso != null)
                    {
                        if (chiso.MaKP != null)
                            lupKP.EditValue = chiso.MaKP;
                        if (chiso.MaCB != null)
                            lupCB.EditValue = chiso.MaCB;
                        if (chiso.IDCS != null)
                            lupChamSoc.EditValue = chiso.IDCS;
                        if (chiso.MaBuong != null)
                            lupBuong.EditValue = chiso.MaBuong;
                        //else
                        //    lupBuong.EditValue = 0;
                        if (chiso.MaGiuong != null)
                            lupGiuong.EditValue = chiso.MaGiuong;
                        //else
                        //    lupGiuong.EditValue = 0;
                        if (chiso.NgayGioDo != null)
                            dateEdit1.DateTime = chiso.NgayGioDo.Value;
                        if (chiso.Mach != null)
                            txtMach.Text = chiso.Mach.ToString();
                        if (chiso.NhietDo != null)
                            txtNhietDo.Text = chiso.NhietDo.ToString();
                        if (chiso.HuyetApT != null)
                            txtHuyetAp.Text = chiso.HuyetApT.ToString();
                        if (chiso.HuyetApD != null)
                            txtHuyetApD.Text = chiso.HuyetApD.ToString();
                        if (chiso.ChieuCao != null)
                            txtChieuCao.Text = chiso.ChieuCao.ToString();
                        if (chiso.CanNang != null)
                            txtCanNang.Text = chiso.CanNang.ToString();
                        if (chiso.NhipTho != null)
                            txtnhipTho.Text = chiso.NhipTho.ToString();

                    }
                }
                else
                    _id = 0;
            }
            else
            {
                lblIDChiSo.Text = "0";
                _id = 0;
            }
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            _id = 0;

        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            //  txtMaBuong.Enabled = false;
            TTLuu = 2;
            if (grvChiSoCoThe.GetFocusedRowCellValue(colID) != null && grvChiSoCoThe.GetFocusedRowCellValue(colID).ToString() != "")
            {
                lblIDChiSo.Text = grvChiSoCoThe.GetFocusedRowCellValue(colID).ToString();
                int ot;
                if (int.TryParse(lblIDChiSo.Text, out ot))
                {
                    _id = Convert.ToInt32(lblIDChiSo.Text);
                }
            }
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            if (grvChiSoCoThe.GetFocusedRowCellValue(colID) != null && grvChiSoCoThe.GetFocusedRowCellValue(colID).ToString() != "")
            {
                int idChiSo = Convert.ToInt32(lblIDChiSo.Text);
                ChiSoCoThe qchiso = dataContext.ChiSoCoThes.Where(p => p.ID == idChiSo).FirstOrDefault();
                if (qchiso != null)
                {
                    DialogResult _result;
                    _result = MessageBox.Show("Bạn muốn xóa chỉ số chức năng sống ngày : " + qchiso.NgayGioDo.Value.ToString(), "Xóa dịch vụ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        dataContext.ChiSoCoThes.Remove(qchiso);
                        dataContext.SaveChanges();
                        List<ChiSoCoThe> qchisocothe = dataContext.ChiSoCoThes.Where(p => p.MaBNhan == _maBN).ToList();
                        grcChiSoCoThe.DataSource = qchisocothe;
                        loadChart();
                    }
                }
            }

        }



        private void labelControl4_Click(object sender, EventArgs e)
        {

        }


        private void lupBuong_EditValueChanged(object sender, EventArgs e)
        {
        }

        int _maBN = 0;
        private void lupKP_EditValueChanged(object sender, EventArgs e)
        {
            int makp = 0; if (lupKP.EditValue != null)
                makp = Convert.ToInt32(lupKP.EditValue);
            //_lBuong = dataContext.Buongs.Where(p => makp == 0 || p.MaKP == makp).OrderBy(p => p.TenBuong).ToList();
            //_lBuong.Insert(0, new Buong { MaBuong = 0, TenBuong = "" });
            //lupBuong.Properties.DataSource = _lBuong;
            List<CanBo> _lcb = dataContext.CanBoes.Where(p => p.MaKP == makp).OrderBy(p => p.TenCB).ToList();
            _lcb.Insert(0, new CanBo { MaCB = "", TenCB = "" });
            lupCB.Properties.DataSource = _lcb;
            //List<BNKB> qbnkb =(from bnkb in dataContext.BNKBs.Where(p => p.MaBNhan == _maBN && p.MaKP == makp)

            //            select bnkb
            //                ).ToList();
            //foreach(BNKB kb in qbnkb)
            //{
            //    var qcb = dataContext.CanBoes.Where(p => p.MaCB == kb.MaCB).FirstOrDefault();
            //    if (qcb != null)
            //        kb.GhiChu = qcb.TenCB;
            //    else
            //        kb.GhiChu = "";
            //}
            //qbnkb.Insert(0, new BNKB { IDKB = 0, NgayKham = null, GhiChu = "" });
            //lupBNKB.Properties.DataSource = qbnkb;

        }
        private void loadChart()
        {
            var qcs = dataContext.ChiSoCoThes.Where(p => p.MaBNhan == _maBN).OrderBy(p => p.NgayGioDo).ToList();

            DataTable table = new DataTable("Table1");

            // Add two columns to the table. 
            table.Columns.Add("NgayGioDo", typeof(DateTime));
            table.Columns.Add("Mach", typeof(Int32));
            table.Columns.Add("NhietDo", typeof(Int32));

            // Add data rows to the table. 
            Random rnd = new Random();
            DataRow row = null;
            int num = 0;
            foreach (var a in qcs)
            {
                row = table.NewRow();
                row["NgayGioDo"] = a.NgayGioDo == null ? DateTime.Now : a.NgayGioDo.Value;
                row["Mach"] = a.Mach ?? 0;
                row["NhietDo"] = a.NhietDo ?? 37;
                table.Rows.Add(row);
            }
            chartControl1.DataSource = table;


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qcs = dataContext.ChiSoCoThes.Where(p => p.MaBNhan == _maBN).OrderBy(p => p.NgayGioDo).ToList();

            DataTable table = new DataTable("Table1");

            // Add two columns to the table. 
            table.Columns.Add("NgayGioDo", typeof(DateTime));
            table.Columns.Add("Mach", typeof(Int32));
            table.Columns.Add("NhietDo", typeof(Int32));

            // Add data rows to the table. 
            Random rnd = new Random();
            DataRow row = null;
            int num = 0;
            foreach (var a in qcs)
            {
                row = table.NewRow();
                row["NgayGioDo"] = a.NgayGioDo == null ? DateTime.Now : a.NgayGioDo.Value;
                row["Mach"] = a.Mach ?? 0;
                row["NhietDo"] = a.NhietDo ?? 37;
                table.Rows.Add(row);

            }




            frmIn frm = new frmIn();
            QLBV.Phieu.rep_ChiSoCoThe rep = new rep_ChiSoCoThe();
            rep.lblSoYTe.Text = "Sở y tế: " + DungChung.Bien.TenCQCQ.ToUpper();
            rep.lblBenhVien.Text = "BV: " + DungChung.Bien.TenCQ.ToUpper();
            rep.lblKhoa.Text = "Khoa: " + lupKP.Text;
            foreach (var a in qcs)
            {
                row = table.NewRow();
                row["NgayGioDo"] = a.NgayGioDo == null ? DateTime.Now : a.NgayGioDo.Value;
                row["Mach"] = a.Mach ?? 0;
                row["NhietDo"] = a.NhietDo ?? 37;
                table.Rows.Add(row);
                num++;
                if (num == 1)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel11.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel21.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel31.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay1.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
                else if (num == 2)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel12.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel22.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel32.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay2.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
                else if (num == 3)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel13.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel23.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel33.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay3.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
                else if (num == 4)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel14.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel24.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel34.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay4.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
                else if (num == 5)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel15.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel25.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel35.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay5.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
                else if (num == 6)
                {
                    if (a.HuyetApT != null && a.HuyetApD != null)
                        rep.cel16.Text = a.HuyetApT.ToString() + "/" + a.HuyetApD.ToString();
                    rep.cel26.Text = a.CanNang == null ? "" : a.CanNang.ToString();
                    rep.cel36.Text = a.NhipTho == null ? "" : a.NhipTho.ToString();
                    if (a.NgayGioDo != null)
                        rep.celNgay6.Text = a.NgayGioDo.Value.ToString("dd/MM/yyyy");
                }
            }
            var qbn = dataContext.BenhNhans.Where(p => p.MaBNhan == _maBN).FirstOrDefault();
            if (qbn != null)
            {
                rep.celHoTen.Text = qbn.TenBNhan.ToUpper();
                rep.celTuoi.Text = qbn.Tuoi.ToString();
                rep.celGtinh.Text = qbn.GTinh == 1 ? "Nam" : (qbn.GTinh == 0 ? "Nữ" : "");
                int makp = 0;
                if (lupKP.EditValue != null)
                    makp = Convert.ToInt32(lupKP.EditValue);
                var qbnkb = dataContext.BNKBs.Where(p => p.MaBNhan == _maBN && p.MaKP == makp).OrderByDescending(p => p.IDKB).FirstOrDefault();
                if (qbnkb != null)
                {
                    rep.celChanDoan.Text = qbnkb.ChanDoan;
                    int magiuong = 0;
                    string strgiuong = qbnkb.Giuong;
                    if (strgiuong != null && strgiuong != "")
                    {
                        string[] l = strgiuong.Split(';');
                        if (l.Count() > 0)
                        {
                            string g = l[l.Count() - 1];
                            int ot;
                            if (int.TryParse(g, out ot))
                            {
                                magiuong = Convert.ToInt32(g);
                                var qgiuong = dataContext.Giuongs.Where(p => p.MaGiuong == magiuong).FirstOrDefault();
                                if (qgiuong != null)
                                {
                                    rep.celGiuong.Text = qgiuong.TenGiuong;
                                    var qbuong = dataContext.Buongs.Where(p => p.MaBuong == qgiuong.MaBuong).FirstOrDefault();
                                    if (qbuong != null)
                                        rep.celBuong.Text = qbuong.TenBuong;
                                }
                            }
                        }
                    }
                    var qvv = dataContext.VaoViens.Where(p => p.MaBNhan == _maBN).FirstOrDefault();
                    if (qvv != null)
                    {
                        rep.lblSoVV.Text = qvv.SoVV;
                    }
                }
                rep.xrChart1.DataSource = table;
                rep.Bindings();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void txtChieuCao_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCanNang.EditValue != null && !string.IsNullOrWhiteSpace(txtCanNang.EditValue.ToString()) && txtChieuCao.EditValue != null && !string.IsNullOrWhiteSpace(txtChieuCao.EditValue.ToString()))
            {
                var canNang = double.Parse(txtCanNang.EditValue.ToString());
                var chieuCao = double.Parse(txtChieuCao.EditValue.ToString());
                double bmi = 0;
                string ketqua = "";
                DungChung.Ham.CalculateBMI(canNang, chieuCao, ref bmi, ref ketqua);
                txtBMI.Text = string.Format("{0:0.0}", bmi);
                lblChiSo.Text = ketqua;
            }
        }

        private void txtCanNang_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCanNang.EditValue != null && !string.IsNullOrWhiteSpace(txtCanNang.EditValue.ToString()) && txtChieuCao.EditValue != null && !string.IsNullOrWhiteSpace(txtChieuCao.EditValue.ToString()))
            {
                var canNang = double.Parse(txtCanNang.EditValue.ToString());
                var chieuCao = double.Parse(txtChieuCao.EditValue.ToString());
                double bmi = 0;
                string ketqua = "";
                DungChung.Ham.CalculateBMI(canNang, chieuCao, ref bmi, ref ketqua);
                txtBMI.Text = string.Format("{0:0.0}", bmi);
                lblChiSo.Text = ketqua;
            }
        }

    }
}