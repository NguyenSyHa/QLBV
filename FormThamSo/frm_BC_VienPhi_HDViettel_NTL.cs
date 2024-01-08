using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_VienPhi_HDViettel_NTL : Form
    {
        public frm_BC_VienPhi_HDViettel_NTL()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập từ ngày");
                return;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập dến ngày");
                return;
            }
            if (cbbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn trạng thái");
                return;
            }

            var tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            var denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            bool isNgayThu = true;
            string maCB = "";
            if (cboCanBo.EditValue != null)
            {
                maCB = cboCanBo.EditValue.ToString();
            }
            int trangThai = cbbTrangThai.SelectedIndex;

            List<HDVT> hdvt = new List<HDVT>();

            if (isNgayThu)
            {
                var tamung = (from tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 3) && o.NgayThu >= tungay && o.NgayThu <= denngay && (maCB != "" ? (o.MaCB == maCB) : true) && (trangThai == 1 ? ((o.MaHD != "" && o.MaHD != null)) : (trangThai == 2 ? true : (o.MaHD == "" || o.MaHD == null))))
                              join cb in dataContext.CanBoes on tu.MaCB equals cb.MaCB into kq
                              from k in kq.DefaultIfEmpty()
                              select new { tu, k }).ToList();

                var vpCT_TTs = (from vp in dataContext.VienPhis
                                join vpct in dataContext.VienPhicts.Where(o => o.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                                join dv in dataContext.DichVus.Where(o => o.IDNhom == 12) on vpct.MaDV equals dv.MaDV
                                group vpct by new { vp.MaBNhan, dv.IDNhom } into kq
                                select new VP { MaBNhan = kq.Key.MaBNhan, TienVanChuyen = kq.Sum(o => o.TienBN) }).ToList();

                var tamUngCTs = (from tuct in dataContext.TamUngcts.Where(o => o.Status == 0)
                                 join tu in dataContext.TamUngs on tuct.IDTamUng equals tu.IDTamUng
                                 join dv in dataContext.DichVus.Where(o => o.IDNhom == 12) on tuct.MaDV equals dv.MaDV
                                 group tuct by new { tu.IDTamUng, dv.IDNhom } into kq
                                 select new TU { IDTamUng = kq.Key.IDTamUng, TienVanChuyen = kq.Sum(o => o.SoTien) }).ToList();

                var canBos = dataContext.CanBoes.ToList();
                var maBnhans = tamung.Select(o => o.tu.MaBNhan).ToList();
                var benhNhans = dataContext.BenhNhans.Where(o => maBnhans.Contains(o.MaBNhan)).ToList();

                foreach (var item in tamung)
                {
                    HDVT hd = new HDVT();
                    LibraryStore.Mapper.DataObjectMapper.Map<HDVT>(hd, item.tu);
                    hd.NgayThu = item.tu.NgayThu.Value;
                    hd.TenCB = item.k != null ? item.k.TenCB : "";
                    hd.TenCBTaoHoaDon = item.tu.MaCBTaoHD != null ? canBos.FirstOrDefault(p => p.MaCB == item.tu.MaCBTaoHD).TenCB.ToString() : "";
                    if (!string.IsNullOrWhiteSpace(item.tu.NgayTaoHD))
                        hd.NgayTao = DateTime.ParseExact(item.tu.NgayTaoHD, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    else
                        hd.NgayTao = null;
                    if (isNgayThu)
                        hd.NgayThang = item.tu.NgayThu.Value.Date;
                    else
                        hd.NgayThang = hd.NgayTao.Value.Date;

                    var benhNhan = benhNhans.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan);
                    if (benhNhan.DTuong == "BHYT")
                    {
                        if (item.tu.PhanLoai == 1)
                        {
                            hd.DichVu = (vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan) != null ? vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan).TienVanChuyen : 0);
                            hd.BHYT = item.tu.SoTien - (vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan) != null ? vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan).TienVanChuyen : 0);
                        }
                        else
                        {
                            hd.DichVu = tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng) != null ? tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng).TienVanChuyen : 0;
                            hd.BHYT = item.tu.SoTien - (tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng) != null ? tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng).TienVanChuyen : 0);
                        }
                        hd.SoTien = (hd.DichVu ?? 0) + (hd.BHYT ?? 0);
                        hdvt.Add(hd);
                    }
                    else
                    {
                        if (item.tu.PhanLoai == 1)
                        {
                            if (!string.IsNullOrWhiteSpace(item.tu.MaHD))
                            {
                                var soHDs = item.tu.MaHD.Split('|').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                for (int i = 0; i < soHDs.Count(); i++)
                                {
                                    HDVT addNew = new HDVT();
                                    LibraryStore.Mapper.DataObjectMapper.Map<HDVT>(addNew, hd);
                                    addNew.MaHD = soHDs[i];
                                    if (i == 0)
                                    {
                                        if (soHDs.Count() > 1)
                                            addNew.DichVu = vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan) != null ? vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan).TienVanChuyen : 0;
                                        else
                                            addNew.DichVu = item.tu.SoTien;

                                    }
                                    else
                                        addNew.DichVu = item.tu.SoTien - (vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan) != null ? vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan).TienVanChuyen : 0);
                                    addNew.SoTien = (addNew.DichVu ?? 0) + (addNew.BHYT ?? 0);
                                    hdvt.Add(addNew);
                                }
                            }
                            else
                            {
                                hd.DichVu = item.tu.SoTien;
                                hd.SoTien = item.tu.SoTien;
                                hdvt.Add(hd);
                                //var tienVC = vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan) != null ? vpCT_TTs.FirstOrDefault(o => o.MaBNhan == item.tu.MaBNhan).TienVanChuyen : 0;
                                //if (tienVC > 0 && item.tu.SoTien > tienVC)
                                //{

                                //}
                                //else
                                //{
                                //    hd.DichVu = item.tu.SoTien;
                                //    hd.BHYT = null;
                                //    hd.SoTien = item.tu.SoTien;
                                //    hdvt.Add(hd);
                                //}
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(item.tu.MaHD))
                            {
                                var soHDs = item.tu.MaHD.Split('|').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                for (int i = 0; i < soHDs.Count(); i++)
                                {
                                    HDVT addNew = new HDVT();
                                    LibraryStore.Mapper.DataObjectMapper.Map<HDVT>(addNew, hd);
                                    addNew.MaHD = soHDs[i];
                                    if (i == 0)
                                    {
                                        if (soHDs.Count() > 1)
                                            addNew.DichVu = tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng) != null ? tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng).TienVanChuyen : 0;
                                        else
                                            addNew.DichVu = item.tu.SoTien;
                                    }
                                    else
                                        addNew.DichVu = item.tu.SoTien - (tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng) != null ? tamUngCTs.FirstOrDefault(o => o.IDTamUng == item.tu.IDTamUng).TienVanChuyen : 0);
                                    addNew.SoTien = (addNew.DichVu ?? 0) + (addNew.BHYT ?? 0);
                                    hdvt.Add(addNew);
                                }
                            }
                            else
                            {
                                hd.DichVu = item.tu.SoTien;
                                hd.SoTien = item.tu.SoTien;
                                hdvt.Add(hd);
                            }
                        }
                    }
                }
            }
            //else
            //{ 
            //    var tamung = (from tu in dataContext.TamUngs.Where(o => (o.PhanLoai == 1 || o.PhanLoai == 3) && (maCB != "" ? (o.MaCBTaoHD == maCB) : true) && (o.MaHD != "" && o.MaHD != null))
            //                  join cb in dataContext.CanBoes on tu.MaCBTaoHD equals cb.MaCB into kq
            //                  from k in kq.DefaultIfEmpty()
            //                  select new { tu, k }).ToList();
            //    foreach (var item in tamung)
            //    {
            //        HDVT hd = new HDVT(item.tu, false);
            //        hd.TenCB = item.k != null ? item.k.TenCB : "";
            //        hd.TenCBTaoHoaDon = item.tu != null ? item.tu.MaCBTaoHD : "";
            //        if (hd.NgayTao != null && hd.NgayTao >= tungay && hd.NgayTao <= denngay)
            //            hdvt.Add(hd);
            //    }
            //}

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["TuNgay"] = tungay.ToString("dd/MM/yyyy");
            dic["DenNgay"] = denngay.ToString("dd/MM/yyyy");
            dic["TuDen"] = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");

            DungChung.Ham.Print(DungChung.PrintConfig.In_BC_HDDT_VIETTEL, hdvt.OrderBy(o => o.MaBNhan).ThenBy(o => o.NgayTao).ToList(), dic, false);

        }

        public class VP
        {
            public int? MaBNhan { get; set; }
            public double TienVanChuyen { get; set; }
        }

        public class TU
        {
            public int IDTamUng { get; set; }
            public double TienVanChuyen { get; set; }
        }

        public class HDVT : TamUng
        {
            public DateTime NgayThang { get; set; }
            public DateTime? NgayTao { get; set; }
            public string TenCB { get; set; }
            public string TenCBTaoHoaDon { get; set; }
            public double? BHYT { get; set; }
            public double? DichVu { get; set; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_VienPhi_HDViettel_NTL_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtDenNgay.DateTime = DateTime.Now;
            dtTuNgay.DateTime = DateTime.Now;



            var canbo = (from tu in dataContext.TamUngs
                         join cb in dataContext.CanBoes.Where(o => o.Status == 1) on tu.MaCB equals cb.MaCB
                         join kp in dataContext.KPhongs.Where(o => o.PLoai == "Kế toán") on tu.MaKP equals kp.MaKP
                         group new { tu, cb } by new { tu.MaCB, cb.TenCB }
                         into kq
                         select new { kq.Key.MaCB, kq.Key.TenCB }
                             ).ToList();
            cboCanBo.Properties.DataSource = canbo;
        }

        private void radioGroupChonNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (radioGroupChonNgay.SelectedIndex == 0)
            //{
            //    cbbTrangThai.SelectedIndex = 2;
            //    cbbTrangThai.Enabled = true;
            //}
            //else
            //{
            //    cbbTrangThai.SelectedIndex = 1;
            //    cbbTrangThai.Enabled = false;
            //}
        }
    }
}
