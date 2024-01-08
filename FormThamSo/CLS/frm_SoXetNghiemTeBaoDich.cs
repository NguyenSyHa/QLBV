using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_SoXetNghiemTeBaoDich : Form
    {
        public frm_SoXetNghiemTeBaoDich()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_SoXetNghiemTeBaoDich_Load(object sender, EventArgs e)
        {
            dtDenNgay.DateTime = DateTime.Now.Date;
            dtTuNgay.DateTime = DateTime.Now.Date;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dtTuNgay.EditValue == null || dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa chọn ngày");
                return;
            }
            var tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            var denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["TuNgay"] = tungay.ToString("dd/MM/yyyy");
            dic["DenNgay"] = denngay.ToString("dd/MM/yyyy");
            dic["TuDen"] = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");

            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon, 180);

            var qBn = (from bn in dataContext.BenhNhans
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       select new { bnkb.ChanDoan, bn.MaBNhan, bn.TenBNhan, bn.DTuong, bn.DChi, bn.Tuoi, bn.GTinh }).ToList();
            var qDvu = (from dv in dataContext.DichVus
                        join tndv in dataContext.TieuNhomDVs.Where(o => o.TenRG == "XN tế bào dịch") on dv.IdTieuNhom equals tndv.IdTieuNhom
                        select new { dv, tndv });
            var qCd = (from cd in dataContext.ChiDinhs
                       join clsct in dataContext.CLScts on cd.IDCD equals clsct.IDCD
                       join dvct in dataContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { cd, clsct, dvct });
            var qCls = (from cls in dataContext.CLS.Where(o => o.NgayTH >= tungay && o.NgayTH <= denngay && o.NgayTH != null)
                        join kpcd in dataContext.KPhongs on cls.MaKP equals kpcd.MaKP
                        join cbth in dataContext.CanBoes on cls.MaCBth equals cbth.MaCB
                        select new
                        {
                            cls,
                            kpcd,
                            cbth,
                        });

            int fetched = 0;
            int totalFetched = 0;
            List<CutEntity> qAll = new List<CutEntity>();
            do
            {
                var q0 = (from cls_1 in qCls
                          join cd_1 in qCd on cls_1.cls.IdCLS equals cd_1.cd.IdCLS
                          join dv_1 in qDvu on cd_1.cd.MaDV equals dv_1.dv.MaDV
                          select new CutEntity { cbth = cls_1.cbth, cls = cls_1.cls, kpcd = cls_1.kpcd, dv = dv_1.dv, clsct = cd_1.clsct, dvct = cd_1.dvct }).OrderBy(o => o.cls.NgayTH).Skip(totalFetched).Take(1000).ToList();

                fetched = q0.Count;
                totalFetched += fetched;
                qAll.AddRange(q0);
            }
            while (fetched > 0);

            //var qAll = (from cls_1 in qCls
            //            join cd_1 in qCd on cls_1.cls.IdCLS equals cd_1.cd.IdCLS
            //            join dv_1 in qDvu on cd_1.cd.MaDV equals dv_1.dv.MaDV
            //            select new { cls_1.cbth, cls_1.cls, cls_1.kpcd, dv_1.dv, cd_1.clsct, cd_1.dvct }).ToList();

            var query = (from bn in qBn
                         join cls in qAll on bn.MaBNhan equals cls.cls.MaBNhan
                         group new { bn, cls } by new { bn, cls.cls, cls.cbth.TenCB, cls.kpcd.TenKP, cls.dv.TenDV } into kq
                         select new XNTeBaoDich
                         {
                             NgayTH = kq.First().cls.cls.NgayTH,
                             NgayThang = kq.First().cls.cls.NgayTH.Value.Day + "/" + kq.First().cls.cls.NgayTH.Value.Month,
                             BA = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn ưa bazo") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn ưa bazo").cls.clsct.KetQua : "",
                             ChanDoan = kq.Key.bn.ChanDoan,
                             Co_BHYT = kq.Key.bn.DTuong == "BHYT" ? "X" : "",
                             CV = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hồng cầu có nhân") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hồng cầu có nhân").cls.clsct.KetQua : "",
                             EO = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn ưa axit") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn ưa axit").cls.clsct.KetQua : "",
                             Hematocrit = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hematocrit") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hematocrit").cls.clsct.KetQua : "",
                             HSTo = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Huyết sắc tố") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Huyết sắc tố").cls.clsct.KetQua : "",
                             Khac = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Khac") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Khac").cls.clsct.KetQua : "",
                             LY = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Lympho") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Lympho").cls.clsct.KetQua : "",
                             MauLang_1h = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "1h") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "LY").cls.clsct.KetQua : "",
                             MauLang_2h = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "2h") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "LY").cls.clsct.KetQua : "",
                             MC = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MC") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MC").cls.clsct.KetQua : "",
                             MCH = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCH") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCH").cls.clsct.KetQua : "",
                             MCHC = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCHC") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCHC").cls.clsct.KetQua : "",
                             MCV = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCV") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MCV").cls.clsct.KetQua : "",
                             MD = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MD") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MD").cls.clsct.KetQua : "",
                             MO = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Mono") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Mono").cls.clsct.KetQua : "",
                             MPV = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MPV") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "MPV").cls.clsct.KetQua : "",
                             NE = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn trung tính") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Đoạn trung tính").cls.clsct.KetQua : "",
                             PCT = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "PCT") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "PCT").cls.clsct.KetQua : "",
                             PDW = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "PDW") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "PDW").cls.clsct.KetQua : "",
                             SD = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hồng cầu lưới") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Hồng cầu lưới").cls.clsct.KetQua : "",
                             SLBC = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Số lượng BC") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Số lượng BC").cls.clsct.KetQua : "",
                             SLHC = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Số lượng HC") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Số lượng HC").cls.clsct.KetQua : "",
                             SLTC = kq.FirstOrDefault(o => o.cls.dvct.TenDVct == "Số lượng tiểu cầu") != null ? kq.FirstOrDefault(o => o.cls.dvct.TenDVct ==
                                 "Số lượng tiểu cầu").cls.clsct.KetQua : "",
                             NguoiLam = kq.Key.TenCB,
                             NhomMau = "",
                             NoiGui = kq.Key.TenKP,
                             TenBN = kq.Key.bn.TenBNhan,
                             Tuoi_Nam = kq.Key.bn.GTinh == 1 ? (kq.Key.bn.Tuoi != null ? kq.Key.bn.Tuoi.Value.ToString() : "") : "",
                             Tuoi_Nu = kq.Key.bn.GTinh != 1 ? (kq.Key.bn.Tuoi != null ? kq.Key.bn.Tuoi.Value.ToString() : "") : "",
                             YeuCau = kq.Key.TenDV
                         }).ToList();

            DungChung.Ham.Print(DungChung.PrintConfig.rep_SoXetNghiemTeBaoDich_27023, query, dic, false);
        }

        public class CutEntity
        {
            public CanBo cbth { get; set; }
            public CL cls { get; set; }
            public KPhong kpcd { get; set; }
            public DichVu dv { get; set; }
            public DichVuct dvct { get; set; }
            public CLSct clsct { get; set; }
        }

        public class XNTeBaoDich
        {
            public string NgayThang { get; set; }
            public DateTime? NgayTH { get; set; }
            public string TenBN { get; set; }
            public string Tuoi_Nam { get; set; }
            public string Tuoi_Nu { get; set; }
            public string Co_BHYT { get; set; }
            public string NoiGui { get; set; }
            public string ChanDoan { get; set; }
            public string YeuCau { get; set; }
            public string NguoiLam { get; set; }
            public string SLBC { get; set; }
            public string NE { get; set; }
            public string LY { get; set; }
            public string MO { get; set; }
            public string EO { get; set; }
            public string BA { get; set; }
            public string SLHC { get; set; }
            public string HSTo { get; set; }
            public string Hematocrit { get; set; }
            public string MCV { get; set; }
            public string MCH { get; set; }
            public string MCHC { get; set; }
            public string CV { get; set; }
            public string SD { get; set; }
            public string SLTC { get; set; }
            public string PCT { get; set; }
            public string MPV { get; set; }
            public string PDW { get; set; }
            public string MauLang_1h { get; set; }
            public string MauLang_2h { get; set; }
            public string MC { get; set; }
            public string MD { get; set; }
            public string NhomMau { get; set; }
            public string Khac { get; set; }
        }
    }
}
