using System;using QLBV_Database;
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
    public partial class frm_LichTrucBan : DevExpress.XtraEditors.XtraForm
    {
        public frm_LichTrucBan()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
        private bool KtraLuu()
        {
            if(lupKhoaP.EditValue==null)
            {
                MessageBox.Show("Bạn chưa chọn khoa|phòng !");
                lupKhoaP.Focus();
                return false;
            }
            int dem=0;
            for (int i = 0; i < cklCanBo.ItemCount;i++ )
            {
                if (cklCanBo.GetItemChecked(i) == true)
                    dem++;
            }
            if (dem == 0)
            {
                MessageBox.Show("Bạn chưa chọn cán bộ trực");
                return false;
            }
            DateTime ngaytruc = dtngaytruc.DateTime;
            DateTime tructu = dttructu.DateTime;
            DateTime trucden = dttrucden.DateTime;
            TimeSpan ktra1 = tructu - ngaytruc;
            TimeSpan ktra2 = trucden - ngaytruc;
            if (ktra1.TotalHours >= 24 || tructu < ngaytruc)
            {
                MessageBox.Show("Thời gian bắt đầu trực không hợp lệ");
                return false;
            }
            if (ktra2.TotalHours >= 48 || trucden < ngaytruc)
            {
                MessageBox.Show("thời gian kết thúc trực không hợp lệ");
                return false;
            }
            if(tructu>=trucden)
            {
                MessageBox.Show("Thời gian trực đến không được nhỏ hơn thời gian trực từ");
                return false;
            }
                return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<CanBoTruc> _lCanBoTruc = new List<CanBoTruc>();
        List<CanBo> _lcb = new List<CanBo>();
        List<KPhong> _lkhoap = new List<KPhong>();
        bool New = true, Edit = true, Delete = true;
        private void frm_LichTrucBan_Load(object sender, EventArgs e)
        {
            _lkhoap = data.KPhongs.Where(p => p.Status == 1).ToList();
            var _lkp = _lkhoap.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Phòng khám")).ToList();
            lupKhoaP.Properties.DataSource = _lkp;
            var _lkptimkiem = _lkp.ToList();
            _lkptimkiem.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaSearch.Properties.DataSource = _lkptimkiem;
            lupKhoaSearch.EditValue = 0;
            dtngaytruc.DateTime = DateTime.Today;
            _lcb = data.CanBoes.Where(p => p.Status == 1).ToList();
            dengaytu.DateTime = DateTime.Today;
            dengayden.DateTime = DateTime.Today;
            var KtraPer = data.Permissions.Where(p => p.ID == 9050).FirstOrDefault();
            if (KtraPer != null)
            {
                New = KtraPer.C_New;
                Edit = KtraPer.C_Edit;
                Delete = KtraPer.C_Delete;
            }
            if (!New)
                btnMoi.Enabled = false;
            if (!Edit)
                btnSua.Enabled = false;
            if (!Delete)
                btnXoa.Enabled = false;

            //LoadLichtruc();
        }
        void Enablecontrol(bool A)
        {
            dtngaytruc.Properties.ReadOnly = !A;
            lupKhoaP.Properties.ReadOnly = !A;
            dttrucden.Properties.ReadOnly = !A;
            dttructu.Properties.ReadOnly = !A;
            //cklCanBo = A;

            btnLuu.Enabled = A;
            if (New)
                btnMoi.Enabled = !A;
            if (Edit)
                btnSua.Enabled = !A;
            if (Delete)
                btnXoa.Enabled = !A;
        }
        private class CanBoTruc
        {
            public bool _Check;
            public string _Macb;
            public string _Tencb;
            public bool Check { get { return _Check; } set { _Check = value; } }
            public string Macb { get { return _Macb; } set { _Macb = value; } }
            public string Tencb { get { return _Tencb; } set { _Tencb = value; } }
        }

        private void lupKhoaPP_EditValueChanged(object sender, EventArgs e)
        {
            
        }
        int TLuu = 0;
        private class LichTrucBan
        {
            public DateTime ngaytruc { get; set; }
            public string Khoa { get; set; }
            public int Makp { get; set; }
            public string Tructu { get; set; }
            public string Trucden { get; set; }
            public string DsCanBo { get; set; }
            public string DsMaCB { get; set; }
        }
        List<LichTrucBan> _lichtruc = new List<LichTrucBan>();
        private void LoadLichtruc()
        {
            Enablecontrol(false);
            _lichtruc.Clear();
            int makp = -1;
            if (lupKhoaSearch.EditValue != null)
                makp = Convert.ToInt32(lupKhoaSearch.EditValue);
            DateTime ngaytu = DateTime.Now;
            DateTime ngayden = DateTime.Now;
            ngaytu = dengaytu.DateTime;
            ngayden = dengayden.DateTime;
            var _llichtruc = data.LichTrucs.Where(p => p.Ngay >= ngaytu && p.Ngay <= ngayden).Where(p => makp == 0 ? true : p.MaKP == makp).ToList();
            foreach (var item in _llichtruc)
            {
                LichTrucBan moi = new LichTrucBan();
                moi.ngaytruc = item.Ngay;
                moi.Makp = item.MaKP;
                var TenKP = _lkhoap.Where(p => p.MaKP == item.MaKP).Select(p => p.TenKP).FirstOrDefault();
                if (TenKP != null)
                    moi.Khoa = TenKP.ToString();
                else moi.Khoa = "";
                moi.Tructu = item.ThoiGianTu.ToString();
                moi.Trucden = item.ThoiGianDen.ToString();
                moi.DsCanBo = DSCanBo(item.ListMaCB);
                moi.DsMaCB = item.ListMaCB;
                _lichtruc.Add(moi);
            }
            grcLichTruc.DataSource = null;
            grcLichTruc.DataSource = _lichtruc;

        }
        private string DSCanBo(string ChuoiCB)
        {
            string dscb = "";
            if(!string.IsNullOrEmpty(ChuoiCB)&&ChuoiCB.Contains(";"))
            {
                string[] _dsmacb = ChuoiCB.Split(';');
                foreach (var item in _dsmacb)
                {
                    var Tencb = _lcb.Where(p => p.MaCB == item).Select(p => p.TenCB).FirstOrDefault();
                    if (Tencb != null)
                        dscb += Tencb + ";\n";
                }
            }
            return dscb;
        }

        private void panelControl5_Paint()
        {

        }
        void Reset()
        {
            dtngaytruc.DateTime = DateTime.Today;
            DateTime ngaytructu = DateTime.Today.AddHours(DungChung.Bien.GioDen[1]).AddMinutes(DungChung.Bien.PhutDen[1]);
            DateTime ngaytrucden = DateTime.Today.AddDays(1).AddHours(DungChung.Bien.GioTu[0]).AddMinutes(DungChung.Bien.PhutTu[0]);
            dttructu.DateTime = ngaytructu;
            dttrucden.DateTime = ngaytrucden;
            lupKhoaP.EditValue = null;
        }
        private void btnMoi_Click(object sender, EventArgs e)
        {
            Enablecontrol(true);
            Reset();
            TLuu = 1;
        }

        private void dttrucden_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grcLichTruc_Click(object sender, EventArgs e)
        {

        }
        private void _loadCbTruc(string DSCB, List<CanBoTruc> _lCanBoTruc, CheckedListBoxControl cklCanBo)
        {

            try
            {
                if (!string.IsNullOrEmpty(DSCB))
                {
                    string[] kp = DSCB.Split(';');
                    for (int i = 0; i < cklCanBo.ItemCount; i++)
                    {

                        cklCanBo.SetItemChecked(i, false);
                    }
                    foreach (var item in kp)
                    {
                        foreach (var item2 in _lCanBoTruc)
                        {
                            if (!string.IsNullOrEmpty(item))
                                if (Convert.ToString(item) == item2.Macb)
                                {
                                    item2.Check = true;
                                    for (int i = 0; i < cklCanBo.ItemCount; i++)
                                    {
                                        if (cklCanBo.GetItemValue(i) != null && Convert.ToString(cklCanBo.GetItemValue(i)) == item2.Macb)
                                        {
                                            cklCanBo.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load Danh sách cán bộ: " + ex.Message);
            }

        }

        private void grvLichTruc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(grvLichTruc.GetFocusedRowCellValue(colNgay)!=null)
            {
                DateTime _ngaytruc = Convert.ToDateTime(grvLichTruc.GetFocusedRowCellValue(colNgay));
                dtngaytruc.DateTime = _ngaytruc;
                int makp = 0;
                makp = Convert.ToInt32(grvLichTruc.GetFocusedRowCellValue(colMakp));
                lupKhoaP.EditValue = makp;
                DateTime _ngaytructu = Convert.ToDateTime(grvLichTruc.GetFocusedRowCellValue(colTruTu));
                dttructu.DateTime = _ngaytructu;
                DateTime _ngaytrucden = Convert.ToDateTime(grvLichTruc.GetFocusedRowCellValue(colTrucDen));
                dttrucden.DateTime = _ngaytrucden;
                string DsCB = Convert.ToString(grvLichTruc.GetFocusedRowCellValue(colDsMacb));
                _loadCbTruc(DsCB, _lCanBoTruc, cklCanBo);
            }
        }

        private void dengaytu_EditValueChanged(object sender, EventArgs e)
        {
            LoadLichtruc();
        }

        private void dengayden_EditValueChanged(object sender, EventArgs e)
        {
            LoadLichtruc();
        }

        private void lupKhoaPSearch_EditValueChanged(object sender, EventArgs e)
        {
            LoadLichtruc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Enablecontrol(true);
            dtngaytruc.Properties.ReadOnly = true;
            lupKhoaP.Properties.ReadOnly = true;
            TLuu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool DY_Xoa = true;
            if (lupKhoaP.EditValue == null)
            {
                MessageBox.Show("Chưa chọn lịch trực ");
                DY_Xoa = false;
            }
            if (DY_Xoa)
            {
                DialogResult Result = MessageBox.Show("Bạn muốn xóa lich trực này ?", "Hỏi xóa!", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    DateTime ngaytruc = dtngaytruc.DateTime;
                    int makp = Convert.ToInt32(lupKhoaP.EditValue);
                    LichTruc xoa = data.LichTrucs.Where(p => p.MaKP == makp && p.Ngay == ngaytruc).FirstOrDefault();
                    if (xoa != null)
                    {
                        data.LichTrucs.Remove(xoa);
                        data.SaveChanges();
                        LoadLichtruc();
                    }
                }
            }
        }

    

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
        if(KtraLuu())
            {
                if (TLuu == 1)
                {
                    int MaKP = 0; string TenKP = lupKhoaP.Text;
                    MaKP = Convert.ToInt32(lupKhoaP.EditValue);
                    DateTime _ngaytruc = dtngaytruc.DateTime.Date;
                    var ktra = data.LichTrucs.Where(p => p.Ngay == _ngaytruc && p.MaKP == MaKP).FirstOrDefault();
                    if (ktra != null)
                    {
                        MessageBox.Show(TenKP + " đã có lịch trực ngày: " + _ngaytruc.ToShortDateString() + " \nBạn không thể thêm mới !");
                    }
                    else
                    {
                        try
                        {
                            LichTruc moi = new LichTruc();
                            moi.Ngay = _ngaytruc;
                            moi.MaKP = MaKP;
                            moi.ThoiGianTu = dttructu.DateTime;
                            moi.ThoiGianDen = dttrucden.DateTime;
                            string DsCB = "";
                            for (int i = 0; i < cklCanBo.ItemCount; i++)
                            {
                                if (cklCanBo.GetItemCheckState(i) == CheckState.Checked)
                                    DsCB += cklCanBo.GetItemValue(i) + ";";
                            }
                            moi.ListMaCB = DsCB;
                            data.LichTrucs.Add(moi);
                            data.SaveChanges();
                            LoadLichtruc();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi thêm mới: " + ex);
                        }
                    }
                }
                else
                {
                    int MaKP = 0; string TenKP = lupKhoaP.Text;
                    MaKP = Convert.ToInt32(lupKhoaP.EditValue);
                    DateTime _ngaytruc = dtngaytruc.DateTime.Date;
                    LichTruc Sua = data.LichTrucs.Where(p => p.Ngay == _ngaytruc && p.MaKP == MaKP).FirstOrDefault();
                    if (Sua != null)
                    {
                        Sua.ThoiGianTu = dttructu.DateTime;
                        Sua.ThoiGianDen = dttrucden.DateTime;
                        string DsCB = "";
                        for (int i = 0; i < cklCanBo.ItemCount; i++)
                        {
                            if (cklCanBo.GetItemCheckState(i) == CheckState.Checked)
                                DsCB += cklCanBo.GetItemValue(i) + ";";
                        }
                        Sua.ListMaCB = DsCB;
                        data.SaveChanges();
                        LoadLichtruc();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupKhoaP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemCB();
        }
        private void TimKiemCB()
        {
            int Makp = 0;
            if (lupKhoaP.EditValue != null)
                Makp = Convert.ToInt32(lupKhoaP.EditValue);
            if (Makp > 0)
            {
                string makp = Makp.ToString();
                if (string.IsNullOrEmpty(txtTKCB.Text))
                {
                    _lCanBoTruc = (from cb in _lcb
                                   group cb by new { cb.MaCB, cb.TenCB } into kq
                                   select new CanBoTruc { Check = false, _Macb = kq.Key.MaCB, Tencb = kq.Key.TenCB }).ToList();
                    cklCanBo.DataSource = null;
                    cklCanBo.DataSource = _lCanBoTruc.OrderBy(p => p.Tencb);
                }
                else
                {
                    string tkten = txtTKCB.Text.ToLower();
                    _lCanBoTruc = (from cb in _lcb.Where(p => p.TenCB.ToLower().Contains(tkten)) 
                                   group cb by new { cb.MaCB, cb.TenCB } into kq
                                   select new CanBoTruc { Check = false, _Macb = kq.Key.MaCB, Tencb = kq.Key.TenCB }).ToList();
                    cklCanBo.DataSource = null;
                    cklCanBo.DataSource = _lCanBoTruc.OrderBy(p => p.Tencb);
                }
            }
        }

        private void txtTKCB_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtTKCB_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemCB();
        }
    }
}