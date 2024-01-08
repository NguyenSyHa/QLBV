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
    public partial class frm_THChungTuThuTU_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_THChungTuThuTU_27022()
        {
            InitializeComponent();
        }
        List<MucTT> _listmuc = new List<MucTT>();
        private void frm_THChungTuThuTU_27022_Load(object sender, EventArgs e)
        {
            radNoiTru.SelectedIndex = 2;
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DTBN> dtuong = data.DTBNs.Where(p => p.Status == 1).ToList();
            dtuong.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            LupDTuong.Properties.DisplayMember = "DTBN1";
            LupDTuong.Properties.ValueMember = "IDDTBN";
            LupDTuong.Properties.DataSource = dtuong;
            LupDTuong.EditValue = Convert.ToInt32(LupDTuong.Properties.GetKeyValueByDisplayText("Tất cả"));
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _listmuc = data.MucTTs.ToList();
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            int noitru = 2;
            if (radNoiTru.SelectedIndex == 0)
                noitru = 0;
            if (radNoiTru.SelectedIndex == 1)
                noitru = 1;
            if (radNoiTru.SelectedIndex == 2)
                noitru = 2;
            // lấy hàm getmuc
            int iddtuong = 99;
            if (LupDTuong.EditValue != null)
                iddtuong = Convert.ToInt32(LupDTuong.EditValue);

            //  _getmuc(_listmuc, hangbv, bn.SThe, bn.Tuyen.Value, bn.NoiTru.Value, vp.NgayTT.Value)
            var qbn = (from tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 0)
                       join bn in data.BenhNhans.Where(p => noitru == 2 || p.NoiTru == noitru).Where(p => iddtuong == 99 || p.IDDTBN == iddtuong) on tu.MaBNhan equals bn.MaBNhan// thiếu  đối tượng 
                       join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
                       select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.NoiTru,tu.IDTamUng, bn.SThe, bn.Tuyen, tu.NgayThu, tu.QuyenHD, tu.SoHD, SoTien = tu.SoTien, bv.TuyenBV, bn.SoTT }).ToList();
            var qbn1 = (from bn in qbn
                        select new
                        {
                            bn.MaBNhan,
                            bn.TenBNhan,
                            bn.DChi,
                            bn.NoiTru,
                            bn.SThe,
                            bn.Tuyen,
                            bn.NgayThu,
                            bn.QuyenHD,
                            SoHD = DungChung.Bien.MaBV == "27023" ? bn.IDTamUng.ToString() : bn.SoHD,
                            SoTien = bn.SoTien,
                            HangBV = bn.TuyenBV.Trim() == "A" ? 1 : (bn.TuyenBV.Trim() == "B" ? 2 : (bn.TuyenBV.Trim() == "C" ? 3 : (bn.TuyenBV.Trim() == "D" ? 4 : 0)))
                        }).OrderBy(p => p.NgayThu).ThenBy(p => p.QuyenHD).ThenBy(p => p.SoHD).ToList();
            var qbn2 = (from bn in qbn1
                        select new
                        {
                            bn.MaBNhan,
                            bn.TenBNhan,
                            bn.DChi,
                            bn.NoiTru,
                            bn.SThe,
                            bn.Tuyen,
                            bn.NgayThu,
                            bn.QuyenHD,
                            bn.SoHD,
                            SoTien = bn.SoTien,
                            Muc = 100 - DungChung.Ham._getmuc(_listmuc, bn.HangBV, bn.SThe, bn.Tuyen ?? 0, bn.NoiTru ?? 0, bn.NgayThu.Value)
                        }).OrderBy(p => p.NgayThu).ThenBy(p => p.QuyenHD).ThenBy(p => p.SoHD).ToList();

            double tongtien = qbn.Sum(p => p.SoTien ?? 0);

            if (DungChung.Bien.MaBV == "27023")
            {
                rep_THChungTuThuTU_27023 rep = new rep_THChungTuThuTU_27023();
                frmIn frm = new frmIn();
                rep.celNgayIn.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                rep.celSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, "Đồng");
                rep.DataSource = qbn2;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                rep_THChungTuThuTU_27022 rep = new rep_THChungTuThuTU_27022();
                frmIn frm = new frmIn();
                rep.celNgayIn.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                rep.celSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, "Đồng");
                rep.DataSource = qbn2;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
          
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}