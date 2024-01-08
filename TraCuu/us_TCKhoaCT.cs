using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.TraCuu
{
    public partial class us_TCKhoaCT : DevExpress.XtraEditors.XtraUserControl
    {
        public us_TCKhoaCT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        List<NhapD> _lnhap = new List<NhapD>();
        bool ttluu = false;
        #region tao list
        public class lnhapd{
            public int id;
            public DateTime ngaynhap;
            public string sct;
            public int makho;
            public bool status;
            public int ploai;
            public string ghichu;
            public string MaCB { set; get; }
            public int IDNhap
            {
                set { id=value; }
                get { return id; }
            }
            public DateTime NgayNhap
            {
                set { ngaynhap = value; }
                get { return ngaynhap; }
            }
            public string SoCT
            {
                set { sct = value; }
                get { return sct; }
            }
            public int MaKP
            {
                set { makho = value; }
                get { return makho; }
            }
            public bool Status
            {
                set { status = value; }
                get { return status; }
            }
            public string LyDo
            {
                set { ghichu = value; }
                get { return ghichu; }
            }
            
        }
        #endregion

        public void TimKiem()
        {
            List<lnhapd> _ltaomoi = new List<lnhapd>();
            int _ploai = -1;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _ploai = cboPLoai.SelectedIndex;
            int tt = rgTrangThai.SelectedIndex;
            _lnhap.Clear();
            _lnhap = _data.NhapDs.Where(p => p.PLoai == _ploai).Where(p => tt == 2 ? true : (tt == 1 ? p.Status == 1 : (p.Status == 0 || p.Status == null))).Where(p => p.NgayNhap <= _dtden).Where(p => p.NgayNhap >= _dttu).ToList();
            if(DungChung.Bien.PLoaiKP!=DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                _lnhap = (from kp in DungChung.Bien.listKPHoatDong
                          join nd in _lnhap on kp equals nd.MaKP
                          select nd).ToList();
            }
            foreach (var n in _lnhap)
            {
                lnhapd them = new lnhapd();
                them.IDNhap = n.IDNhap;
                them.LyDo = n.GhiChu;
                them.MaKP = n.MaKP == null ? 0 : n.MaKP.Value;
                them.NgayNhap = n.NgayNhap.Value;
                them.SoCT = n.SoCT;
                them.MaCB = n.MaCB;
                if (n.Status != null)
                {
                    if (n.Status == 1)
                        them.Status = true;
                    else
                        them.Status = false;
                }
                else
                {
                    them.Status = false;
                }
                _ltaomoi.Add(them);
            }
            grcNhapD.DataSource = _ltaomoi.ToList();
        }
        List<CanBo> _lcb = new List<CanBo>();
        private void us_TCKhoaCT_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var _lkp = _data.KPhongs.ToList();
            lupKhoNhap.DataSource = _lkp;
            _lcb = _data.CanBoes.ToList();
            //rgTrangThai.SelectedIndex = 2;
            TimKiem();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                rdCheckAll.Enabled = true;
                btnOK.Enabled = true;
            }
            else
            {
                var cb = _lcb.Where(p => p.MaCB == DungChung.Bien.MaCB).Select(p => p.Khoa).ToList();
                if (cb.Count > 0 && cb.First() != null && cb.First().Value == 1)
                    btnOK.Enabled = true;
                else
                    btnOK.Enabled = false;
            }
            rdCheckAll.SelectedIndex = 1;
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void cboPLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvNhapD.RowCount; i++)
            {
                if (grvNhapD.GetRowCellValue(i, colIDNhap) != null)
                {
                    int id = Convert.ToInt32(grvNhapD.GetRowCellValue(i, colIDNhap).ToString());
                    int st = 0;
                    if (grvNhapD.GetRowCellValue(i, colStatus).ToString() == "True")
                    {
                        st = 1;
                    }
                    else
                        st = 0;
                    var update = _data.NhapDs.Single(p => p.IDNhap == id);
                    update.Status = st;
                    _data.SaveChanges();

                }
            }
            MessageBox.Show("Đã thực hiện");
            TimKiem();
        }

        private void grvNhapD_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == "colStatus")
            //{
            //    if (grvNhapD.GetRowCellValue(e.RowHandle, colIDNhap) != null)
            //    {
            //        int id = Convert.ToInt32(grvNhapD.GetRowCellValue(e.RowHandle, colIDNhap));
            //        _lnhap.Single(p => p.IDNhap == id);
            //        if (grvNhapD.GetRowCellValue(e.RowHandle, colStatus).ToString() == "true")
            //        {
            //            MessageBox.Show(grvNhapD.GetRowCellValue(e.RowHandle, colStatus).ToString());
            //            _lnhap.First().Status = 1;
            //        }
            //        else
            //            _lnhap.First().Status = 0;
            //    }
            //}
        }

        private void rdCheckAll_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (DungChung.Bien.MaBV != "30009")
            //{
            for (int i = 0; i < grvNhapD.RowCount; i++)
            {
                if (rdCheckAll.SelectedIndex == 0)
                    grvNhapD.SetRowCellValue(i, colStatus, true);
                else if (rdCheckAll.SelectedIndex == 1)
                { grvNhapD.SetRowCellValue(i, colStatus, false); }
            }
            //}
            //else
            //{

            //    for (int i = 0; i < grvNhapD.RowCount; i++)
            //    {
            //        string MaCB = "";
            //        if (grvNhapD.GetRowCellValue(i, colMaCB) != null && grvNhapD.GetRowCellValue(i, colMaCB).ToString() != "")
            //        {
            //            MaCB = grvNhapD.GetRowCellValue(i, colMaCB).ToString();
            //        }

            //        if (DungChung.Bien.MaCB == MaCB)
            //        {
            //            if (rdCheckAll.SelectedIndex == 0)
            //                grvNhapD.SetRowCellValue(i, colStatus, true);
            //            else if (rdCheckAll.SelectedIndex == 1)
            //            { grvNhapD.SetRowCellValue(i, colStatus, false); }
            //        }
            //    }

            //}
        }

        private void grvNhapD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string MaCB = "";
            if (grvNhapD.GetFocusedRowCellValue(colMaCB) != null && grvNhapD.GetFocusedRowCellValue(colMaCB).ToString() != "")
            {
                MaCB = grvNhapD.GetFocusedRowCellValue(colMaCB).ToString();
            }
            int makp = 0;
            if (grvNhapD.GetFocusedRowCellValue(colMaKP) != null && grvNhapD.GetFocusedRowCellValue(colMaKP).ToString() != "")
            {
                makp = Convert.ToInt32(grvNhapD.GetFocusedRowCellValue(colMaKP).ToString());
            }
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                colStatus.OptionsColumn.ReadOnly = false;
            }
            else
            {
                string _makp = ";" + makp + ";";
                var kt = _lcb.Where(p => p.MaCB == DungChung.Bien.MaCB).Where(p => p.MaKPsd.Contains(_makp)).ToList();
                if (kt.Count > 0)
                {
                    colStatus.OptionsColumn.ReadOnly = false;
                }
                else
                    colStatus.OptionsColumn.ReadOnly = true;
            }
            //if (DungChung.Bien.MaBV == "30009" && DungChung.Bien.MaCB != MaCB)
            //{
            //    MessageBox.Show("Bạn không có quyền khóa hoặc bỏ khóa chứng từ");
            //    colStatus.OptionsColumn.ReadOnly = true;
            //}
            //else
            //    colStatus.OptionsColumn.ReadOnly = false;
            
        }

        private void rgTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}
