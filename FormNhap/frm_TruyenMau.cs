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

namespace QLBV.FormNhap
{
    public partial class frm_TruyenMau : DevExpress.XtraEditors.XtraForm
    {
        int mabn = 0, idkb = 0, moi_sua = 0;
        int id = 0; int x = -1, y = -1;
        string mabc = "";
        public frm_TruyenMau(int _mabn, int _idkb)
        {
            InitializeComponent();
            mabn = _mabn;
            idkb = _idkb;
        }
        public frm_TruyenMau()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void EnableControl(bool T)
        {
            textEdit11.Enabled = T;
            dateEdit1.Enabled = T;
            dateEdit2.Enabled = T;
            dateEdit3.Enabled = T;
            dateEdit4.Enabled = T;
            dateEdit5.Enabled = T;
            textEdit5.Enabled = T;
            textEdit3.Enabled = T;
            textEdit7.Enabled = T;
            textEdit10.Enabled = T;
            textEdit22.Enabled = T;
            textEdit28.Enabled = T;
            lup_DDTM.Enabled = T;
            comboBoxEdit1.Enabled = T;
            dvmautruyen.Enabled = T;
        }
        private void trang()
        {
            textEdit11.Text = "";
            dateEdit1.DateTime = DateTime.Now;
            dateEdit2.DateTime = DateTime.Now;
            dateEdit3.DateTime = DateTime.Now;
            dateEdit4.DateTime = DateTime.Now;
            dateEdit5.DateTime = DateTime.Now;
            textEdit5.Text = "";
            textEdit3.Text = "";
            textEdit7.Text = "";
            textEdit10.Text = "";
            textEdit22.Text = "";
            textEdit28.Text = "";
            comboBoxEdit1.Text = "";
            dvmautruyen.Text = "";
            lup_DDTM.Text = "";
        }
        private bool kt1()
        {
            //if (dateEdit1.Text != "" && dateEdit2.Text != "")
            //{
            //    if (dateEdit2.DateTime <= dateEdit1.DateTime)
            //    {
            //        MessageBox.Show("Thời gian phát máu phải nhỏ hơn TG xong PU chéo!");
            //        dateEdit1.Focus();
            //        return false;
            //    }
            //}
            if (dateEdit3.Text != "" && dateEdit4.Text != "")
            {
                if (dateEdit3.DateTime >= dateEdit4.DateTime)
                {
                    MessageBox.Show("Thời gian bắt đầu truyền phải nhỏ hơn kết thúc!");
                    dateEdit3.Focus();
                    return false;
                }
            }
            return true;
        }
        List<LinhMau> mau = new List<LinhMau>();
        private void frm_TruyenMau_Load(object sender, EventArgs e)
        {
            EnableControl(false);
            mau.Clear();
            var mau1 = (from a in data.LinhMaus.Where(p => p.MaBNhan == mabn && p.IDKB == idkb && p.NgayLinhMau != null) select a).ToList();
            foreach (var a in mau1)
            {
                LinhMau themmoi = new LinhMau();
                themmoi.IdLinhMau = a.IdLinhMau;
                themmoi.NgayLinhMau = a.NgayLinhMau;
                themmoi.SoLuong_yc = a.SoLuong_yc;
                mau.Add(themmoi);
            }
            bindingSource3.DataSource = mau.ToList();
            gridControl4.DataSource = bindingSource3;
            var tm = (from a in data.TruyenMaus.Where(p => p.MaBNhan == mabn && p.IDKB == idkb) select a).ToList();
            bindingSource1.DataSource = tm.ToList();
            gridControl3.DataSource = bindingSource1;
            int idtm = tm.Count > 0 ? tm.First().IDTruyenMau : 0;
            var tmct = data.TruyenMauCTs.Where(p => p.IDTruyenMau == idtm).ToList();
            bindingSource2.DataSource = tmct.ToList();
            gridControl2.DataSource = bindingSource2;
            var cb = data.CanBoes.ToList();
            lup_canbo.DataSource = cb.ToList();
            lup_DDTM.Properties.DataSource = cb.ToList();
            var bn = (from a in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                      join b in data.KPhongs on a.MaKP equals b.MaKP
                      join c in data.BNKBs.Where(p => p.IDKB == idkb) on a.MaBNhan equals c.MaBNhan
                      join d in data.VaoViens on a.MaBNhan equals d.MaBNhan
                      select new { a.MaBNhan, a.TenBNhan, b.TenKP, a.Tuoi, a.GTinh, c.Buong, c.Giuong, c.ChanDoan, d.NhomMau, d.HeMau, a.MaKP, c.MaCB, a.DTuong }).ToList();
            if (bn.Count > 0)
            {
                lup_MaBNhan.Enabled = false;
                lup_MaBNhan.Text = bn.First().TenBNhan;
            }
            if (tm.Count > 0)
            {
                int x = tm.FirstOrDefault().IDLinhMau ?? 0;
                var lm = data.LinhMaus.Where(p => p.IdLinhMau == x).ToList();
                textEdit11.Text = tm.First().NguoiThuPUCheo;
                if (tm.First().ThoiGianXNHHMD != null)
                    dateEdit2.DateTime = tm.First().ThoiGianXNHHMD.Value;
                if (tm.First().HanDungCPMau != null)
                    dateEdit1.DateTime = tm.First().HanDungCPMau.Value;
                textEdit5.Text = tm.First().MTM_Ong1;
                textEdit3.Text = tm.First().Globulin_Ong1;
                textEdit7.Text = tm.First().MTM_Ong2;
                textEdit10.Text = tm.First().Globulin_Ong2;
                textEdit11.Text = tm.First().NguoiThuPUCheo;
                comboBoxEdit1.Text = tm.First().KQPUCTaiGiuong;
                if (tm.First().ThoiGianKThucPUCheo != null)
                    dateEdit5.DateTime = tm.First().ThoiGianKThucPUCheo.Value;
                if (tm.First().ThoiGianBDau != null)
                    dateEdit3.DateTime = tm.First().ThoiGianBDau.Value;
                if (tm.First().SoLuongMauTT != null)
                {
                    string[] ar1 = tm.First().SoLuongMauTT.Split(';');
                    textEdit22.Text = ar1[0];
                    if (ar1.Count() > 1)
                        dvmautruyen.Text = ar1[1];
                }
                if (tm.First().ThoiGianKThuc != null)
                    dateEdit4.DateTime = tm.First().ThoiGianKThuc.Value;
                textEdit28.Text = tm.First().NhanXet;
                gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            }
            //gridView3.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView4_FocusedRowChanged(null, null);
            gridView3_FocusedRowChanged(null, null);
            btnLuu.Enabled = false;
            simpleButton1.Enabled = false;
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            var bnrv = data.RaViens.Where(p => p.MaBNhan == mabn).ToList();
            if (bnrv.Count == 0)
            {
                if (mau.Count() > 0)
                {
                    EnableControl(true);
                    trang();
                    mabc = "";
                    moi_sua = 0;
                    btnLuu.Enabled = true;
                    simpleButton1.Enabled = true;
                    btnSua.Enabled = false;
                    btnIn.Enabled = false;
                    btnXoa.Enabled = false;
                    //bindingSource2.DataSource = null;
                    //gridControl2.DataSource = bindingSource2;
                    gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                }
                else
                    MessageBox.Show("Bệnh nhân chưa lĩnh máu không thể thêm mới!");
                //gridView3.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

            }
            else
            {
                MessageBox.Show("Bệnh nhân đã ra viện không thể thêm mới!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (kt1())
            {
                EnableControl(false);
                if (moi_sua == 0)
                {
                    TruyenMau moi = new TruyenMau();
                    moi.MaBNhan = mabn;
                    moi.IDKB = idkb;
                    moi.IDLinhMau = idlm;
                    moi.NguoiThuPUCheo = textEdit11.Text;
                    moi.MaCB_YTa = lup_DDTM.EditValue.ToString();
                    if (dateEdit1.Text != "")
                        moi.HanDungCPMau = dateEdit1.DateTime;
                    if (dateEdit2.Text != "")
                        moi.ThoiGianXNHHMD = dateEdit2.DateTime;
                    if (dateEdit3.Text != "")
                        moi.ThoiGianBDau = dateEdit3.DateTime;
                    moi.SoLuongMauTT = textEdit22.Text + ";" + dvmautruyen.Text;
                    if (dateEdit4.Text != "")
                        moi.ThoiGianKThuc = dateEdit4.DateTime;
                    if (dateEdit5.Text != "")
                        moi.ThoiGianKThucPUCheo = dateEdit5.DateTime;
                    moi.NhanXet = textEdit28.Text;
                    var bc = data.BNKBs.Where(p => p.IDKB == idkb).ToList();
                    if (bc.Count > 0)
                        moi.MaCB_BS = bc.First().MaCB;
                    moi.MTM_Ong1 = textEdit5.Text;
                    moi.MTM_Ong2 = textEdit7.Text;
                    moi.Globulin_Ong1 = textEdit3.Text;
                    moi.Globulin_Ong2 = textEdit10.Text;
                    moi.KQPUCTaiGiuong = comboBoxEdit1.Text;
                    data.TruyenMaus.Add(moi);
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int id1 = moi.IDTruyenMau;
                        int id2 = Convert.ToInt32(gridView2.GetRowCellValue(i, IDTruyenMauCT));
                        var tmct = data.TruyenMauCTs.Where(p => p.IDTruyenMauCT == id2).FirstOrDefault();
                        if (tmct == null)
                        {
                            TruyenMauCT moi1 = new TruyenMauCT();
                            moi1.IDTruyenMau = id1;
                            if (gridView2.GetRowCellValue(i, ThoiGian) != null && gridView2.GetRowCellValue(i, ThoiGian).ToString() != "")
                                moi1.ThoiGian = Convert.ToDateTime(gridView2.GetRowCellValue(i, ThoiGian).ToString());

                            if (gridView2.GetRowCellValue(i, TocDoTruyen) != null && gridView2.GetRowCellValue(i, TocDoTruyen).ToString() != "")
                                moi1.TocDoTruyen = Convert.ToInt32(gridView2.GetRowCellValue(i, TocDoTruyen).ToString());

                            if (gridView2.GetRowCellValue(i, MauSac_NiemMac) != null && gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString() != "")
                                moi1.MauSac_NiemMac = gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString();

                            if (gridView2.GetRowCellValue(i, NhipTho) != null && gridView2.GetRowCellValue(i, NhipTho).ToString() != "")
                                moi1.NhipTho = gridView2.GetRowCellValue(i, NhipTho).ToString();

                            if (gridView2.GetRowCellValue(i, Mach) != null && gridView2.GetRowCellValue(i, Mach).ToString() != "")
                                moi1.Mach = gridView2.GetRowCellValue(i, Mach).ToString();

                            if (gridView2.GetRowCellValue(i, NhietDo) != null && gridView2.GetRowCellValue(i, NhietDo).ToString() != "")
                                moi1.NhietDo = gridView2.GetRowCellValue(i, NhietDo).ToString();

                            if (gridView2.GetRowCellValue(i, HuyetAp) != null && gridView2.GetRowCellValue(i, HuyetAp).ToString() != "")
                                moi1.HuyetAp = gridView2.GetRowCellValue(i, HuyetAp).ToString();

                            if (gridView2.GetRowCellValue(i, GhiChu) != null && gridView2.GetRowCellValue(i, GhiChu).ToString() != "")
                                moi1.GhiChu = gridView2.GetRowCellValue(i, GhiChu).ToString();

                            if (moi1.ThoiGian != null || moi1.TocDoTruyen != null || moi1.MauSac_NiemMac != null || moi1.NhipTho != null || moi1.Mach != null || moi1.HuyetAp != null || moi1.GhiChu != null)
                            {
                                data.TruyenMauCTs.Add(moi1);
                            }
                        }
                    }
                    if (data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Thêm mới thành công");
                        frm_TruyenMau_Load(sender, e);
                    }
                }
                else
                {
                    TruyenMau moi = data.TruyenMaus.Single(p => p.MaBNhan == mabn && p.IDTruyenMau == id);
                    if (dateEdit2.Text != "")
                        moi.ThoiGianXNHHMD = dateEdit2.DateTime;
                    else
                        moi.ThoiGianXNHHMD = null;
                    if (dateEdit1.Text != "")
                        moi.HanDungCPMau = dateEdit1.DateTime;
                    else
                        moi.HanDungCPMau = null;
                    moi.NguoiThuPUCheo = textEdit11.Text;
                    if (dateEdit5.Text != "")
                        moi.ThoiGianKThucPUCheo = dateEdit5.DateTime;
                    else
                        moi.ThoiGianKThucPUCheo = null;
                    if (dateEdit3.Text != "")
                        moi.ThoiGianBDau = dateEdit3.DateTime;
                    else
                        moi.ThoiGianBDau = null;
                    if (dateEdit4.Text != "")
                        moi.ThoiGianKThuc = dateEdit4.DateTime;
                    else
                        moi.ThoiGianKThuc = null;
                    moi.SoLuongMauTT = textEdit22.Text + ";" + dvmautruyen.Text;
                    moi.NhanXet = textEdit28.Text;
                    moi.MTM_Ong1 = textEdit5.Text;
                    moi.MTM_Ong2 = textEdit7.Text;
                    moi.Globulin_Ong1 = textEdit3.Text;
                    moi.Globulin_Ong2 = textEdit10.Text;
                    moi.KQPUCTaiGiuong = comboBoxEdit1.Text;
                    moi.MaCB_YTa = lup_DDTM.EditValue.ToString();
                    if (mabc != "")
                        moi.MaCB_BS = mabc;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int id1 = moi.IDTruyenMau;
                        int id2 = Convert.ToInt32(gridView2.GetRowCellValue(i, IDTruyenMauCT));
                        var tmct = data.TruyenMauCTs.Where(p => p.IDTruyenMauCT == id2).FirstOrDefault();
                        if (tmct == null)
                        {
                            TruyenMauCT moi1 = new TruyenMauCT();
                            moi1.IDTruyenMau = id1;
                            if (gridView2.GetRowCellValue(i, ThoiGian) != null && gridView2.GetRowCellValue(i, ThoiGian).ToString() != "")
                                moi1.ThoiGian = Convert.ToDateTime(gridView2.GetRowCellValue(i, ThoiGian).ToString());

                            if (gridView2.GetRowCellValue(i, TocDoTruyen) != null && gridView2.GetRowCellValue(i, TocDoTruyen).ToString() != "")
                                moi1.TocDoTruyen = Convert.ToInt32(gridView2.GetRowCellValue(i, TocDoTruyen).ToString());

                            if (gridView2.GetRowCellValue(i, MauSac_NiemMac) != null && gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString() != "")
                                moi1.MauSac_NiemMac = gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString();

                            if (gridView2.GetRowCellValue(i, NhipTho) != null && gridView2.GetRowCellValue(i, NhipTho).ToString() != "")
                                moi1.NhipTho = gridView2.GetRowCellValue(i, NhipTho).ToString();

                            if (gridView2.GetRowCellValue(i, Mach) != null && gridView2.GetRowCellValue(i, Mach).ToString() != "")
                                moi1.Mach = gridView2.GetRowCellValue(i, Mach).ToString();

                            if (gridView2.GetRowCellValue(i, NhietDo) != null && gridView2.GetRowCellValue(i, NhietDo).ToString() != "")
                                moi1.NhietDo = gridView2.GetRowCellValue(i, NhietDo).ToString();

                            if (gridView2.GetRowCellValue(i, HuyetAp) != null && gridView2.GetRowCellValue(i, HuyetAp).ToString() != "")
                                moi1.HuyetAp = gridView2.GetRowCellValue(i, HuyetAp).ToString();

                            if (gridView2.GetRowCellValue(i, GhiChu) != null && gridView2.GetRowCellValue(i, GhiChu).ToString() != "")
                                moi1.GhiChu = gridView2.GetRowCellValue(i, GhiChu).ToString();

                            if (moi1.ThoiGian != null || moi1.TocDoTruyen != null || moi1.MauSac_NiemMac != null || moi1.NhipTho != null || moi1.Mach != null || moi1.HuyetAp != null || moi1.GhiChu != null)
                            {
                                data.TruyenMauCTs.Add(moi1);
                            }
                        }
                        else
                        {
                            TruyenMauCT moi1 = data.TruyenMauCTs.Single(p => p.IDTruyenMauCT == id2);
                            moi1.IDTruyenMau = id1;
                            if (gridView2.GetRowCellValue(i, ThoiGian) != null && gridView2.GetRowCellValue(i, ThoiGian).ToString() != "")
                                moi1.ThoiGian = Convert.ToDateTime(gridView2.GetRowCellValue(i, ThoiGian).ToString());

                            if (gridView2.GetRowCellValue(i, TocDoTruyen) != null && gridView2.GetRowCellValue(i, TocDoTruyen).ToString() != "")
                                moi1.TocDoTruyen = Convert.ToInt32(gridView2.GetRowCellValue(i, TocDoTruyen).ToString());

                            if (gridView2.GetRowCellValue(i, MauSac_NiemMac) != null && gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString() != "")
                                moi1.MauSac_NiemMac = gridView2.GetRowCellValue(i, MauSac_NiemMac).ToString();

                            if (gridView2.GetRowCellValue(i, NhipTho) != null && gridView2.GetRowCellValue(i, NhipTho).ToString() != "")
                                moi1.NhipTho = gridView2.GetRowCellValue(i, NhipTho).ToString();

                            if (gridView2.GetRowCellValue(i, Mach) != null && gridView2.GetRowCellValue(i, Mach).ToString() != "")
                                moi1.Mach = gridView2.GetRowCellValue(i, Mach).ToString();

                            if (gridView2.GetRowCellValue(i, NhietDo) != null && gridView2.GetRowCellValue(i, NhietDo).ToString() != "")
                                moi1.NhietDo = gridView2.GetRowCellValue(i, NhietDo).ToString();

                            if (gridView2.GetRowCellValue(i, HuyetAp) != null && gridView2.GetRowCellValue(i, HuyetAp).ToString() != "")
                                moi1.HuyetAp = gridView2.GetRowCellValue(i, HuyetAp).ToString();

                            if (gridView2.GetRowCellValue(i, GhiChu) != null && gridView2.GetRowCellValue(i, GhiChu).ToString() != "")
                                moi1.GhiChu = gridView2.GetRowCellValue(i, GhiChu).ToString();
                        }
                    }
                    if (data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Sửa thành công");
                        frm_TruyenMau_Load(sender, e);
                    }
                }
                btnLuu.Enabled = false;
                simpleButton1.Enabled = false;
                btnIn.Enabled = true;
                btnXoa.Enabled = true; ;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            BaoCao.rep_PhieuTruyenMau rep = new BaoCao.rep_PhieuTruyenMau();
            var truyenmau = (from a in data.TruyenMaus.Where(p => p.IDTruyenMau == id)
                             select new
                             {
                                 a
                             }).ToList();
            if (truyenmau.Count > 0)
            {
                int? idlinhmau = truyenmau.First().a.IDLinhMau;
                int idkb = truyenmau.FirstOrDefault().a.IDKB;
                int idtruyenmau = truyenmau.First().a.IDTruyenMau;
                var linhmau = (from a in data.LinhMaus.Where(p => p.IdLinhMau == idlinhmau)
                               join b in data.DichVus on a.MaDV equals b.MaDV
                               select new { b.TenDV, b.MaTam, a.NgayLinhMau, a.NhomMauABO_yc, a.NhomMauABO_tl, a.NhomMauRh_tl, a.NhomMauRh_yc }).ToList();

                var bn = (from a in data.BenhNhans
                          join b in data.BNKBs.Where(p => p.IDKB == idkb) on a.MaBNhan equals b.MaBNhan
                          join c in data.KPhongs on b.MaKP equals c.MaKP
                          select new { a.MaBNhan, a.TenBNhan, a.Tuoi, a.GTinh, b.ChanDoan, c.TenKP }).ToList(); ;

                rep.TenBN.Value = bn.First().TenBNhan.ToUpper();
                rep.Tuoi.Value = bn.First().Tuoi;
                rep.GioiTinh.Value = bn.First().GTinh == 0 ? "Nữ" : "Nam";
                rep.ChanDoan.Value = bn.First().ChanDoan;
                rep.Khoa.Value = bn.First().TenKP;
                rep.LoaiCPM.Value = linhmau.First().TenDV;
                rep.MaDVCPM.Value = linhmau.First().MaTam;
                rep.NgayLayMau.Value = linhmau.First().NgayLinhMau;
                rep.HanDung.Value = truyenmau.First().a.HanDungCPMau;
                rep.NhomMauABO.Value = "(" + linhmau.First().NhomMauABO_yc + ")   " + linhmau.First().NhomMauRh_yc;
                rep.ChePhamABO.Value = "(" + linhmau.First().NhomMauABO_tl + ")   " + linhmau.First().NhomMauRh_tl;
                rep.Ong1.Value = truyenmau.First().a.MTM_Ong1;
                rep.Ong1_2.Value = truyenmau.First().a.MTM_Ong2;
                rep.Ong2.Value = truyenmau.First().a.Globulin_Ong1;
                rep.Ong2_2.Value = truyenmau.First().a.Globulin_Ong2;
                rep.DinhNhomMau.Value = linhmau.First().TenDV + " ( " + linhmau.First().NhomMauABO_tl + " )";
                rep.DinhNhomMauBN.Value = "( " + linhmau.First().NhomMauABO_yc + " )";
                rep.KQPUCheo.Value = truyenmau.First().a.KQPUCTaiGiuong;
                rep.TGBDTruyen.Value = truyenmau.First().a.ThoiGianBDau.Value.Hour + " giờ " + truyenmau.First().a.ThoiGianBDau.Value.Minute + " ngày " + truyenmau.First().a.ThoiGianBDau.Value.Day + " tháng " + truyenmau.First().a.ThoiGianBDau.Value.Month + " năm " + truyenmau.First().a.ThoiGianBDau.Value.Year;
                rep.TGNgungTruyen.Value = truyenmau.First().a.ThoiGianKThuc.Value.Hour + " giờ " + truyenmau.First().a.ThoiGianKThuc.Value.Minute + " ngày " + truyenmau.First().a.ThoiGianKThuc.Value.Day + " tháng " + truyenmau.First().a.ThoiGianKThuc.Value.Month + " năm " + truyenmau.First().a.ThoiGianKThuc.Value.Year;
                string[] ar11 = { "", "" };
                if (truyenmau.First().a.SoLuongMauTT != null)
                    ar11 = truyenmau.First().a.SoLuongMauTT.Split(';');
                rep.SoLuongTT.Value = ar11[0] + " " + ar11[1];
                rep.NhanXet.Value = truyenmau.First().a.NhanXet;
                rep.NgayXN.Value = "Hồi " + truyenmau.First().a.ThoiGianXNHHMD.Value.Hour + " giờ " + truyenmau.First().a.ThoiGianXNHHMD.Value.Minute + " ngày " + truyenmau.First().a.ThoiGianXNHHMD.Value.Day + " tháng " + truyenmau.First().a.ThoiGianXNHHMD.Value.Month + " năm " + truyenmau.First().a.ThoiGianXNHHMD.Value.Year;
                string macb = truyenmau.First().a.MaCB_BS;
                string tencb = data.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault().TenCB;
                rep.TenBSDT.Value = tencb.ToUpper();
                macb = truyenmau.First().a.MaCB_YTa;
                tencb = data.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault().TenCB;
                rep.TenDDTM.Value = tencb.ToUpper();
                var truyenmauct = (from a in data.TruyenMaus.Where(p => p.IDTruyenMau == idtruyenmau)
                                   join b in data.TruyenMauCTs on a.IDTruyenMau equals b.IDTruyenMau
                                   select b).OrderBy(p => p.ThoiGian).ToList();
                rep.DataSource = truyenmauct.ToList();
                rep.BindingData();
            }


            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            moi_sua = 1;
            btnLuu.Enabled = true;
            simpleButton1.Enabled = true;
            btnMoi.Enabled = false;
            btnIn.Enabled = false;
            gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            id = 0; y = -1;
            if (gridView3.GetFocusedRowCellValue(colIDTruyenMau) != null)
            {
                id = Convert.ToInt32(gridView3.GetFocusedRowCellValue(colIDTruyenMau).ToString());
            }
            if (gridView3.GetFocusedRowCellValue(colIDTruyenMau) != null && gridView3.GetFocusedRowCellValue(colIDTruyenMau).ToString() != "0")
            {
                int idtm = Convert.ToInt32(gridView3.GetFocusedRowCellValue(colIDTruyenMau).ToString());
                y = Convert.ToInt32(gridView3.GetFocusedRowCellValue(colIDTruyenMau).ToString());
                var tmct = data.TruyenMauCTs.Where(p => p.IDTruyenMau == idtm).ToList();
                bindingSource2.DataSource = tmct.ToList();
                gridControl2.DataSource = bindingSource2;
            }

            if (id != 0)
            {
                var tm = (from a in data.TruyenMaus.Where(p => id != 0 ? (p.MaBNhan == mabn && p.IDTruyenMau == id) : false) select a).ToList();
                if (tm.Count > 0)
                {
                    textEdit11.Text = tm.First().NguoiThuPUCheo;
                    if (tm.First().ThoiGianKThucPUCheo != null)
                        dateEdit5.DateTime = tm.First().ThoiGianKThucPUCheo.Value;
                    if (tm.First().ThoiGianBDau != null)
                        dateEdit3.DateTime = tm.First().ThoiGianBDau.Value;
                    if (tm.First().SoLuongMauTT != null)
                    {
                        string[] ar1 = tm.First().SoLuongMauTT.Split(';');
                        textEdit22.Text = ar1[0];
                        if (ar1.Count() > 1)
                            dvmautruyen.Text = ar1[1];
                    }
                    if (tm.First().ThoiGianKThuc != null)
                        dateEdit4.DateTime = tm.First().ThoiGianKThuc.Value;
                    textEdit28.Text = tm.First().NhanXet;
                    lup_DDTM.EditValue = tm.First().MaCB_YTa;

                }
            }
            else
            {
                if (gridView3.GetFocusedRowCellValue(colMaCB_BS) != null)
                    mabc = gridView3.GetFocusedRowCellValue(colMaCB_BS).ToString();
                trang();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            frm_TruyenMau_Load(sender, e);
            btnLuu.Enabled = false;
            simpleButton1.Enabled = false;
            btnIn.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void gridView3_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue(colMaCB_BS) != null)
                mabc = gridView3.GetFocusedRowCellValue(colMaCB_BS).ToString();

        }
        List<TruyenMauCT> ds = new List<TruyenMauCT>();
        private void btnXoa_Click(object sender, EventArgs e)
        {
            TruyenMau xoa = data.TruyenMaus.Single(p => p.IDTruyenMau == id);
            DialogResult _result = MessageBox.Show("Bạn muốn xóa lần truyền máu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                var xoa1 = data.TruyenMauCTs.Where(p => p.IDTruyenMau == id).ToList();
                foreach (var item in xoa1)
                {
                    TruyenMauCT xoact = data.TruyenMauCTs.Single(p => p.IDTruyenMauCT == item.IDTruyenMauCT);
                    data.TruyenMauCTs.Remove(xoact);
                }
                data.TruyenMaus.Remove(xoa);
                if (data.SaveChanges() > 0)
                {
                    MessageBox.Show("Xóa thành công");
                    frm_TruyenMau_Load(sender, e);
                }
            }

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int e1 = e.FocusedRowHandle;
            x = -1;
            if (gridView2.GetFocusedRowCellValue(IDTruyenMauCT) != null)
            {
                x = Convert.ToInt32(gridView2.GetFocusedRowCellValue(IDTruyenMauCT).ToString());
            }
        }

        private void btn_xoact1_Click(object sender, EventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue(IDTruyenMauCT) != null && gridView2.GetFocusedRowCellValue(IDTruyenMauCT).ToString() != "0")
            {
                if (MessageBox.Show("Bạn có muốn xóa chi tiết không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TruyenMauCT moi = data.TruyenMauCTs.Single(p => p.IDTruyenMauCT == x);
                    data.TruyenMauCTs.Remove(moi);
                    if (data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Xóa chi tiết thành công!");
                    }
                    x = -1;
                    frm_TruyenMau_Load(sender, e);
                }
            }
            else
            {
                gridView2.DeleteRow(Convert.ToInt32(gridView2.GetFocusedRowCellValue(IDTruyenMauCT).ToString()));
            }
        }

        private void txt_NhomMauNNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void textEdit22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txt_LanTruyen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void dvmautruyen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {

        }
        int idlm = 0;
        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int x = 0;
            if (gridView4.GetFocusedRowCellValue(IdLinhMau) != null)
            {
                x = Convert.ToInt32(gridView4.GetFocusedRowCellValue(IdLinhMau).ToString());
                idlm = x;
                var tm = (from a in data.TruyenMaus.Where(p => p.MaBNhan == mabn && p.IDLinhMau == x) select a).ToList();
                bindingSource1.DataSource = tm.ToList();
                gridControl3.DataSource = bindingSource1;
                var lm = data.LinhMaus.Where(p => p.IdLinhMau == x).ToList();
                txt_NhomMauNNhan.Text = lm.Count > 0 ? lm.First().NhomMauABO_yc : "";
                if (tm.Count() > 0)
                {
                    btnMoi.Enabled = false;
                    btnSua.Enabled = true;
                    btnIn.Enabled = true;
                }
                else
                {
                    btnMoi.Enabled = true;
                    btnSua.Enabled = false;
                    btnIn.Enabled = false;
                }
            }
            else
                btnMoi.Enabled = false;
            gridView3_FocusedRowChanged(null, null);
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
    }
}