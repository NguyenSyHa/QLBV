using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_ChanDoanHinhAnh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChanDoanHinhAnh()
        {
            InitializeComponent();
        }
        private static QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private string GhepChuoi(int so1, int so2)
        {
            return so1 + "  " + "Ca /" + "  " + so2 + "  " + "Phim";
        }

        public class DSCLS
        {
            public int MaBNhan { get; set; }
            public string DTuong { get; set; }
            public int? NoiTru { get; set; }
            public int? MaDV { get; set; }
            public int IdTieuNhom { get; set; }
            public string TenRG { get; set; }
            public int NgoaiGioHC { get; set; }
            public int Loai { get; set; }
        }

        void getData()
        {
            frmIn frm = new frmIn();
            BC_ChanDoanHinhAnh rep = new BC_ChanDoanHinhAnh();
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            List<CDHA> listCDHA = new List<CDHA>();

            var dsCD = (from bn in data.BenhNhans
                        join cls in data.CLS.Where(p => p.NgayTH >= TuNgay && p.NgayTH <= DenNgay) on bn.MaBNhan equals cls.MaBNhan
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        select new { bn.MaBNhan, bn.DTuong, bn.NoiTru, cd.MaDV, cd.NgoaiGioHC }).ToList();

            var dsDV = (from dv in data.DichVus
                        join tn in data.TieuNhomDVs.Where(o => o.TenRG == "X-Quang" || o.TenRG == "X-Quang CT") on dv.IdTieuNhom equals tn.IdTieuNhom
                        select new { dv.MaDV, tn.IdTieuNhom, tn.TenRG, dv.Loai }).ToList();

            List<DSCLS> dsCLS = new List<DSCLS>();
            int fetched = 0;
            int totalFetched = 0;
            do
            {
                var ds = (from cd in dsCD
                          join dv in dsDV on cd.MaDV equals dv.MaDV
                          select new DSCLS { MaBNhan = cd.MaBNhan, DTuong = cd.DTuong, NoiTru = cd.NoiTru, MaDV = cd.MaDV, IdTieuNhom = dv.IdTieuNhom, TenRG = dv.TenRG, NgoaiGioHC = cd.NgoaiGioHC, Loai = dv.Loai ?? 0 }).Skip(totalFetched).Take(10000).ToList();

                fetched = ds.Count;
                totalFetched += fetched;
                dsCLS.AddRange(ds);
            }
            while (fetched > 0);

            //var dsCLS = (from cd in dsCD
            //             join dv in dsDV.Where(o => o.TenRG == "X-Quang" || o.TenRG == "X-Quang CT") on cd.MaDV equals dv.MaDV
            //             select new DSCLS { cd.MaBNhan, cd.DTuong, cd.NoiTru, cd.MaDV, dv.IdTieuNhom, dv.TenRG, cd.NgoaiGioHC });

            CDHA cdha = new CDHA();

            rep.TongSoBNTrongThang.Value = cdha.TongBenhNhanTrongThang = dsCLS.Where(o => o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count();
            rep.TongSoLanChupXQuang.Value = cdha.TongSoLanChupXQuang = dsCLS.Where(o => o.TenRG == "X-Quang").Sum(o => o.Loai);
            rep.TongSoBenhNhanND.Value = cdha.TongSoBenhNhan_ND = dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count();
            rep.TongSoLanChupPhim2.Value = cdha.TongSoLanChupPhim_ND = dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.TenRG == "X-Quang").Sum(o => o.Loai);
            rep.NoiTru2.Value = cdha.TongSoLanChupPhim_ND_NoiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru == 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru == 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.NgoaiTru2.Value = cdha.TongSoLanChupPhim_ND_NgoaiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru != 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru != 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.TongSoBNBHYT.Value = cdha.TongSoBenhNhan_BHYT = dsCLS.Where(o => o.DTuong == "BHYT" && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count();
            rep.TongSoLanChupPhim3.Value = cdha.TongSoLanChupPhim_BHYT = dsCLS.Where(o => o.DTuong == "BHYT" && o.TenRG == "X-Quang").Sum(o => o.Loai);
            rep.NoiTru3.Value = cdha.TongSoLanChupPhim_BHYT_NoiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru == 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru == 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.NgoaiTru3.Value = cdha.TongSoLanChupPhim_BHYT_NgoaiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru != 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru != 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.TongSoBenhNhanChupNgoaiGio.Value = cdha.TongSoBenhNhanChupNgoaiGio = dsCLS.Where(o => o.NgoaiGioHC == 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count();
            rep.TongSoLanChupPhim4.Value = cdha.TongSoLanChupPhim_NG = dsCLS.Where(o => o.NgoaiGioHC == 1 && o.TenRG == "X-Quang").Sum(o => o.Loai);
            rep.NoiTru4.Value = cdha.TongSoLanChupPhim_NG_NoiTru = GhepChuoi(dsCLS.Where(o => o.NgoaiGioHC == 1 && o.NoiTru == 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.NgoaiGioHC == 1 && o.NoiTru == 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.NgoaiTru4.Value = cdha.TongSoLanChupPhim_NG_NgoaiTru = GhepChuoi(dsCLS.Where(o => o.NgoaiGioHC == 1 && o.NoiTru != 1 && o.TenRG == "X-Quang").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.NgoaiGioHC == 1 && o.NoiTru != 1 && o.TenRG == "X-Quang").Sum(o => o.Loai));
            rep.TongBNChupCT.Value = cdha.TongBenhNhanChupCT = dsCLS.Where(o => o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count();
            rep.TongSoBNChupBH.Value = cdha.TongBenhNhanChupCT_BHYT = dsCLS.Where(o => o.DTuong == "BHYT" && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count();
            rep.NgoaiTru5.Value = cdha.TongBenhNhanChupCT_BHYT_NgoaiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru != 1 && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru != 1 && o.TenRG == "X-Quang CT").Sum(o => o.Loai));
            rep.NoiTru5.Value = cdha.TongBenhNhanChupCT_BHYT_NoiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru == 1 && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "BHYT" && o.NoiTru == 1 && o.TenRG == "X-Quang CT").Sum(o => o.Loai));
            rep.TongSoBenhNhanChupNhanDan.Value = cdha.TongBenhNhanChupCT_ND = dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count();
             rep.NgoaiTru6.Value  = cdha.TongBenhNhanChupCT_ND_NgoaiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru != 1 && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru != 1 && o.TenRG == "X-Quang CT").Sum(o => o.Loai));
            rep.NoiTru6.Value =  cdha.TongBenhNhanChupCT_ND_NoiTru = GhepChuoi(dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru == 1 && o.TenRG == "X-Quang CT").Select(o => o.MaBNhan).Distinct().Count(), dsCLS.Where(o => o.DTuong == "Dịch vụ" && o.NoiTru == 1 && o.TenRG == "X-Quang CT").Sum(o => o.Loai));
            rep.TuNgay.Value = dtpTuNgay.Text;
            rep.DenNgay.Value = dtpDenNgay.Text;

            listCDHA.Add(cdha);

            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public class CDHA
        {
            public int TongBenhNhanTrongThang { get; set; }
            public int TongSoLanChupXQuang { get; set; }
            public int TongSoBenhNhan_ND { get; set; }
            public int TongSoLanChupPhim_ND { get; set; }
            public string TongSoLanChupPhim_ND_NoiTru { get; set; }
            public string TongSoLanChupPhim_ND_NgoaiTru { get; set; }
            public int TongSoBenhNhan_BHYT { get; set; }
            public int TongSoLanChupPhim_BHYT { get; set; }
            public string TongSoLanChupPhim_BHYT_NoiTru { get; set; }
            public string TongSoLanChupPhim_BHYT_NgoaiTru { get; set; }
            public int TongSoBenhNhanChupNgoaiGio { get; set; }
            public int TongSoLanChupPhim_NG { get; set; }
            public string TongSoLanChupPhim_NG_NoiTru { get; set; }
            public string TongSoLanChupPhim_NG_NgoaiTru { get; set; }
            public int TongBenhNhanChupCT { get; set; }
            public int TongBenhNhanChupCT_BHYT { get; set; }
            public string TongBenhNhanChupCT_BHYT_NoiTru { get; set; }
            public string TongBenhNhanChupCT_BHYT_NgoaiTru { get; set; }
            public int TongBenhNhanChupCT_ND { get; set; }
            public string TongBenhNhanChupCT_ND_NoiTru { get; set; }
            public string TongBenhNhanChupCT_ND_NgoaiTru { get; set; }

        }

        private void frm_BC_ChanDoanHinhAnh_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = System.DateTime.Now;
            dtpDenNgay.DateTime = System.DateTime.Now;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(getData, "Đang tạo báo cáo", "Xin vui lòng đợi!");
        }
    }
}