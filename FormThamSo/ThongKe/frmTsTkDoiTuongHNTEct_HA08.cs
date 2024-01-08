using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frmTsTkDoiTuongHNTEct_HA08 : DevExpress.XtraEditors.XtraForm
    {
        public frmTsTkDoiTuongHNTEct_HA08()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBcNXT())
            {
                List<BV> dsbv = new List<BV>();
                dsbv = _lBV.Where(p => p.MaBV != "0" && p.Chon == true).ToList();
                int j = 0;
                for (int i = 0; i < grvDoiTuong.RowCount; i++)
                {
                    if (grvDoiTuong.GetRowCellValue(i, colStatus) != null && (grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "true" || grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "True"))
                    {
                        arrdt[j] = grvDoiTuong.GetRowCellValue(i, colMaDTuong).ToString().Trim();
                        j++;
                    }
                }
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                int noitru = 0;
                noitru = radioGroup1.SelectedIndex;
                frmIn frm = new frmIn();
                BaoCao.rep_TkDoiTuongHNTEct_HA08 rep = new BaoCao.rep_TkDoiTuongHNTEct_HA08();
                // QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "";
                a1 = arrdt[0];
                a2 = arrdt[1];
                a3 = arrdt[2];
                a4 = arrdt[3];
                a5 = arrdt[4];
                a6 = arrdt[5];
                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                if (noitru == 1)
                {
                    var qsl = (from bv in dsbv
                               join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT") on bv.MaBV equals bn.MaKCB
                               join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                               join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                               where (bn.NoiTru == noitru && vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                               where (bn.SThe.Substring(0, 2).Contains(a1) || bn.SThe.Substring(0, 2) == a2 || bn.SThe.Substring(0, 2) == a3 || bn.SThe.Substring(0, 2) == a4 || bn.SThe.Substring(0, 2) == a5
                               || bn.SThe.Substring(0, 2) == a6)
                               //where (rdgTuyen.SelectedIndex == 0 ? bn.MaKCB == DungChung.Bien.MaBV : bn.MaKCB != DungChung.Bien.MaBV)
                               group new { bn, vv } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.SThe, bn.DChi, bn.Tuoi, vv.NgayVao, vv.SoBA } into kq
                               select new
                               {
                                   Mabn = kq.Key.MaBNhan,
                                   HoTen = kq.Key.TenBNhan,
                                   GTinh = kq.Key.GTinh,
                                   Tuoi = kq.Key.Tuoi,
                                   SThe = kq.Key.SThe,
                                   DiaChi = kq.Key.DChi,
                                   NgayVV = kq.Key.NgayVao,
                                   SoBA = kq.Key.SoBA,
                                   NhomDT = kq.Key.SThe.Substring(0, 2),
                               }).ToList();

                    if (qsl.Count > 0)
                    {
                        rep.DataSource = qsl.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Không có dữ liệu để in báo cáo");
                }
                else
                {
                    var qsl = (from bv in dsbv
                               join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT") on bv.MaBV equals bn.MaKCB
                               join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                               where (bn.NoiTru == noitru && vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                               where (bn.SThe.Substring(0, 2).Contains(a1) || bn.SThe.Substring(0, 2) == a2 || bn.SThe.Substring(0, 2) == a3 || bn.SThe.Substring(0, 2) == a4 || bn.SThe.Substring(0, 2) == a5
                               || bn.SThe.Substring(0, 2) == a6)
                               //where (rdgTuyen.SelectedIndex == 0 ? bn.MaKCB == DungChung.Bien.MaBV : bn.MaKCB != DungChung.Bien.MaBV)
                               group new { bn, } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.SThe, bn.DChi, bn.Tuoi, vp.NgayTT } into kq
                               select new
                               {
                                   Mabn = kq.Key.MaBNhan,
                                   HoTen = kq.Key.TenBNhan,
                                   GTinh = kq.Key.GTinh,
                                   Tuoi = kq.Key.Tuoi,
                                   SThe = kq.Key.SThe,
                                   DiaChi = kq.Key.DChi,
                                   NgayVV = kq.Key.NgayTT,
                                   SoBA = "",
                                   NhomDT = kq.Key.SThe.Substring(0, 2),
                               }).ToList();

                    if (qsl.Count > 0)
                    {
                        rep.DataSource = qsl.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Không có dữ liệu để in báo cáo");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string[] arrdt = new string[50];
        private void grvDoiTuong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvDoiTuong.FocusedColumn = grvDoiTuong.VisibleColumns[0];
            grvDoiTuong.FocusedColumn = grvDoiTuong.VisibleColumns[1];
            string doituong = "";
            int j = 0;
            for (int i = 0; i < grvDoiTuong.RowCount; i++)
            {
                if (grvDoiTuong.GetRowCellValue(i, colStatus) != null && (grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "true" || grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "True"))
                {
                    arrdt[j] = grvDoiTuong.GetRowCellValue(i, colMaDTuong).ToString().Trim();
                    doituong += arrdt[j] + "-";
                    j++;
                }
            }

            txtDtuong.Text = doituong;
            if (j > 5)
            {
                MessageBox.Show("Bạn chỉ được chọn tối đa 6 mã đối tượng");
                grvDoiTuong.SetFocusedRowCellValue(colStatus, false);
            }
        }
        public class LDoiTuong
        {

            public string madt;
            public bool check;
            public string MaDTuong
            {
                set { madt = value; }
                get { return madt; }
            }
            public bool Status
            {
                set { check = value; }
                get { return check; }
            }
        }
        List<LDoiTuong> _ldtuong = new List<LDoiTuong>();
        private void frmTsTkDoiTuongHNTEct_HA08_Load(object sender, EventArgs e)
        {
            rdgTuyen_SelectedIndexChanged(sender, e);
            for (int i = 0; i < 50; i++)
            {
                arrdt[i] = "";
            }
            var dtuong = data.DTuongs.OrderBy(p => p.MaDTuong).ToList();
            foreach (var a in dtuong)
            {
                LDoiTuong moi = new LDoiTuong();
                moi.MaDTuong = a.MaDTuong;
                moi.check = false;
                _ldtuong.Add(moi);
            }
            grcDoiTuong.DataSource = _ldtuong.ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
        }

        private void grvDoiTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == "colStatus")
            //{
            //    int j = 0;
            //    for (int i = 0; i < grvDoiTuong.RowCount; i++)
            //    {
            //        if (grvDoiTuong.GetRowCellValue(i, colStatus) != null && (grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "true" || grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "True"))
            //            j++;
            //    }
            //    if (j > 5)
            //    {
            //        MessageBox.Show("Bạn chỉ được chọn tối đa 6 mã đối tượng");
            //        grvDoiTuong.SetFocusedRowCellValue(colStatus, false);
            //    }
            //}
        }

        private void grvDoiTuong_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void grvDoiTuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (e.Column.Name == "colStatus")
            //{
            string doituong = "";
            int j = 0;
            for (int i = 0; i < grvDoiTuong.RowCount; i++)
            {
                if (grvDoiTuong.GetRowCellValue(i, colStatus) != null && (grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "true" || grvDoiTuong.GetRowCellValue(i, colStatus).ToString() == "True"))
                {
                    arrdt[j] = grvDoiTuong.GetRowCellValue(i, colMaDTuong).ToString();
                    doituong += arrdt[j] + "-";
                    j++;
                }
            }

            txtDtuong.Text = doituong;
            if (j > 5)
            {
                MessageBox.Show("Bạn chỉ được chọn tối đa 6 mã đối tượng");
                grvDoiTuong.SetFocusedRowCellValue(colStatus, false);
            }
            //}
        }

        #region class BV
        private class BV
        {
            public bool Chon { get; set; }
            public string MaBV { get; set; }
            public string TenBV { get; set; }
        }
        #endregion

        List<BV> _lBV = new List<BV>();
        private void rdgTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lBV.Clear();
            // Dự yêu cầu sửa ngày 30/05; hiện tại đang lấy tuyến huyện là serial của bệnh viện đó, còn tuyến xã là các bv còn lại
            // => sửa thành tuyến huyện : Lấy tất cả MaDV sử dụng trong bảng khoa phòng
            //var qbv = (from bn in data.BenhNhans
            //           join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
            //           where (rdgTuyen.SelectedIndex == 0 ? bn.MaKCB == DungChung.Bien.MaBV : bn.MaKCB != DungChung.Bien.MaBV)
            //           select new { bv.MaBV, bv.TenBV }).Distinct().ToList();
            var qBVHuyen = (from kp in data.KPhongs.Where(parameters => parameters.TrongBV == 1) group kp by new { kp.MaBVsd } into kq select new { kq.Key.MaBVsd, count = kq.Count() }).ToList();
            string mabvhuyen = "";
            if (qBVHuyen.Count > 0)
                mabvhuyen = qBVHuyen.OrderByDescending(p => p.count).First().MaBVsd;
            var qbv = (from kp in data.KPhongs
                       join bv in data.BenhViens on kp.MaBVsd equals bv.MaBV
                       where (rdgTuyen.SelectedIndex == 0 ? bv.MaBV == mabvhuyen : bv.MaBV != mabvhuyen)
                       select new { bv.MaBV, bv.TenBV }).Distinct().ToList();
            if (qbv.Count > 0)
            {
                BV themmoi1 = new BV();
                themmoi1.TenBV = "Chọn tất cả";
                themmoi1.MaBV = "0";
                themmoi1.Chon = true;
                _lBV.Add(themmoi1);
                foreach (var a in qbv)
                {
                    BV themmoi = new BV();
                    themmoi.TenBV = a.TenBV.Trim();
                    themmoi.MaBV = a.MaBV;
                    themmoi.Chon = true;
                    _lBV.Add(themmoi);
                }
                grcTenBV.DataSource = _lBV.ToList();
            }
        }

        private void grvTenBV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvTenBV.GetFocusedRowCellValue("TenBV") != null)
                {
                    string Ten = grvTenBV.GetFocusedRowCellValue("TenBV").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lBV.First().Chon == true)
                        {
                            foreach (var a in _lBV)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lBV)
                            {
                                a.Chon = true;
                            }
                        }
                        grcTenBV.DataSource = "";
                        grcTenBV.DataSource = _lBV.ToList();
                    }
                }
            }
        }
    }
}