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
    public partial class frm_SoKhamBenh_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoKhamBenh_27022()
        {
            InitializeComponent();
        }

        public class Sokhambenh
        {
            private string TenBN;
            public string tenBN
            {
                get { return TenBN; }
                set { TenBN = value; }
            }
            private string Nam;
            public string nam
            {
                get { return Nam; }
                set { Nam = value; }
            }
            private string Nu;
            public string nu
            {
                get { return Nu; }
                set { Nu = value; }
            }
            private string DiaChi;
            public string diaChi
            {
                get { return DiaChi; }
                set { DiaChi = value; }
            }
            private string BHYT;
            public string Bhyt
            {
                get { return BHYT; }
                set { BHYT = value; }
            }
            private string NoiGT;
            public string noiGT
            {
                get { return NoiGT; }
                set { NoiGT = value; }
            }
            private string ChanDoan;
            public string chanDoan
            {
                get { return ChanDoan; }
                set { ChanDoan = value; }
            }
            private string ChoVe;
            public string choVe
            {
                get { return ChoVe; }
                set { ChoVe = value; }
            }
            private string DTNoiC;
            public string dtNoiC
            {
                get { return DTNoiC; }
                set { DTNoiC = value; }
            }
            private string DTNgoaiC;
            public string dtNgoaiC
            {
                get { return DTNgoaiC; }
                set { DTNgoaiC = value; }
            }
            private string KCK;
            public string kck
            {
                get { return KCK; }
                set { KCK = value; }
            }
            private string TL;
            public string tl
            {
                get { return TL; }
                set { TL = value; }
            }
            private string NA;
            public string na
            {
                get { return NA; }
                set { NA = value; }
            }
            private string KX;
            public string kx
            {
                get { return KX; }
                set { KX = value; }
            }
            private string TK;
            public string tk
            {
                get { return TK; }
                set { TK = value; }
            }
            private string SDM;
            public string sdm
            {
                get { return SDM; }
                set { SDM = value; }
            }
            private string Javal;
            public string javal
            {
                get { return Javal; }
                set { Javal = value; }
            }
            private string SieuAm;
            public string sieuam
            {
                get { return SieuAm; }
                set { SieuAm = value; }
            }
            private string Mau;
            public string mau
            {
                get { return Mau; }
                set { Mau = value; }
            }
            private string NuocTieu;
            public string nt
            {
                get { return NuocTieu; }
                set { NuocTieu = value; }
            }
            private string SoiTT;
            public string soiTT
            {
                get { return SoiTT; }
                set { SoiTT = value; }
            }
            private string HSM;
            public string hsm
            {
                get { return HSM; }
                set { HSM = value; }
            }
            private string SoiGocTP;
            public string soiGocTP
            {
                get { return SoiGocTP; }
                set { SoiGocTP = value; }
            }
            private string SDMBangKinh3Mat;
            public string sdmBangKinh3Mat
            {
                get { return SDMBangKinh3Mat; }
                set { SDMBangKinh3Mat = value; }
            }
            private string TenBS;
            public string tenBS
            {
                get { return TenBS; }
                set { TenBS = value; }
            }
            public string nuocTieu
            {
                get { return NuocTieu; }
                set { NuocTieu = value; }
            }
        }

        List<Sokhambenh> _sokhambenh = new List<Sokhambenh>();
        private void radNTru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private class DoiTuongBn
        {
            public int id { get; set; }
            public string dtbn { get; set; }
        }

        private void frm_SoKhamBenh_27022_Load(object sender, EventArgs e)
        {
            DateTime ngay = System.DateTime.Now;
            lupNgaytu.DateTime = DateTime.Now;
            lupNgayden.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<frm_ThongKeBNHuySoHSBA.KhoaPhong> lstKP = new List<frm_ThongKeBNHuySoHSBA.KhoaPhong>();
            lstKP = db.KPhongs.Where(p => p.PLoai.Equals("Lâm sàng") || p.PLoai.Equals("Phòng khám")).Select(p => new frm_ThongKeBNHuySoHSBA.KhoaPhong { MaKP = p.MaKP, TenKP = p.TenKP }).ToList();
            frm_ThongKeBNHuySoHSBA.KhoaPhong all = new frm_ThongKeBNHuySoHSBA.KhoaPhong();
            all.MaKP = 0;
            all.TenKP = "Tất cả";
            lstKP.Insert(0, all);
            lupKhoa.Properties.DataSource = lstKP;
            lupKhoa.EditValue = 0;
            List<DoiTuongBn> lstDTBN = new List<DoiTuongBn>();
            lstDTBN = db.DTBNs.Where(p => p.Status == 1).Select(p => new DoiTuongBn { id = p.IDDTBN, dtbn = p.DTBN1 }).ToList();
            DoiTuongBn allDT = new DoiTuongBn();
            allDT.id = -1;
            allDT.dtbn = "Tất cả";
            lstDTBN.Insert(0, allDT);
            cklDTBN.DataSource = lstDTBN;
            cklDTBN.CheckAll();
            radNTru.SelectedIndex = 2;


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _sokhambenh.Clear();
            int nc;
            if (radNTru.SelectedIndex == 0)
                nc = 1;
            else if (radNTru.SelectedIndex == 1)
                nc = 0;
            else nc = -1;
            QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            List<int> lstIDDTBN = new List<int>();
            for (int i = 0; i < cklDTBN.ItemCount; i++)
            {
                if (cklDTBN.GetItemChecked(i))
                {
                    lstIDDTBN.Add(Convert.ToInt16(cklDTBN.GetItemValue(i)));
                }
            }
            int makhoa = 0;
            if (lupKhoa.EditValue != null)
            {
                makhoa = Convert.ToInt32(lupKhoa.EditValue);
            }
            var lstCB = db.CanBoes.ToList();
            var lstVP = (from vp in db.VienPhis
                         join vpct in db.VienPhicts.Where(p => makhoa == 0 || p.MaKP == makhoa) on vp.idVPhi equals vpct.idVPhi
                         group new { vp, vpct } by new
                         {
                             vp.MaBNhan,
                             vpct.MaDV
                         } into kq1
                         select new
                         {
                             MaBNhan = kq1.Key.MaBNhan ?? 0,
                             MaDV = kq1.Key.MaDV ?? 0
                         }).ToList();

            var lstDT = (from dt in db.DThuocs
                         join dtct in db.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => makhoa == 0 || p.MaKP == makhoa) on dt.IDDon equals dtct.IDDon
                         group new { dt, dtct } by new
                         {
                             dt.MaBNhan,
                             dtct.MaDV
                         } into kq1
                         select new
                         {
                             MaBNhan = kq1.Key.MaBNhan ?? 0,
                             MaDV = kq1.Key.MaDV ?? 0
                         }).ToList();
            var lstDV = (from dv in db.DichVus.Where(p => p.PLoai == 2)
                         join tn in db.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         group new { dv, tn } by new
                         {
                             dv.MaDV,
                             tn.IdTieuNhom,
                             tn.TenRG,
                             dv.IDNhom,
                             dv.TenDV
                         } into kq2
                         select new
                         {
                             kq2.Key.MaDV,
                             TenRG = kq2.Key.TenRG ?? "",
                             kq2.Key.IDNhom,
                             TenDV = kq2.Key.TenDV ?? ""
                         }).ToList();
            var dtt = (from a in db.DThuocs
                       join b in db.DThuoccts on a.IDDon equals b.IDDon
                       join c in db.DichVus.Where(p => p.PLoai == 1) on b.MaDV equals c.MaDV
                       join d in db.BenhNhans.Where(p => p.DTuong != "KSK") on a.MaBNhan equals d.MaBNhan
                       group new { a, b, c, d } by new
                       {
                           a.MaBNhan,
                           noic = (d.NoiTru == 1) ? "X" : "",
                           ngoaic = (d.NoiTru == 0 && d.DTuong == "BHYT") ? "X" : "",
                       } into kq
                       select new { kq.Key.MaBNhan, kq.Key.noic, kq.Key.ngoaic }).ToList();

            var lstBN1 = (from bn in db.BenhNhans.Where(p => lstIDDTBN.Contains(p.IDDTBN)).Where(p => nc == -1 || p.NoiTru == nc)
                         join kb in db.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay && (makhoa == 0 || p.MaKP == makhoa))
                          on bn.MaBNhan equals kb.MaBNhan
                         join cb in db.CanBoes on kb.MaCB equals cb.MaCB
                         group new { bn, kb, cb } by new
                         {
                             bn.MaBNhan,
                             bn.TenBNhan,
                             nam = (bn.GTinh == 1) ? "X" : "",
                             nu = (bn.GTinh == 0) ? "X" : "",
                             bn.DChi,
                             bn.SThe,
                             bn.MaBV,
                             kb.ChanDoan,
                             cb.TenCB,
                             kb.IDKB
                         } into kq
                         select new
                         {
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             kq.Key.nam,
                             kq.Key.nu,
                             kq.Key.DChi,
                             kq.Key.SThe,
                             kq.Key.MaBV,
                             kq.Key.ChanDoan,
                             kq.Key.TenCB,
                             kq.Key.IDKB
                         }).ToList();
            var lstBN = (from a in lstBN1
                         join b in dtt on a.MaBNhan equals b.MaBNhan into k
                         from k1 in k.DefaultIfEmpty()
                         select new
                         {
                             a.MaBNhan,
                             a.TenBNhan,
                             a.nam,
                             a.nu,
                             a.DChi,
                             a.SThe,
                             a.MaBV,
                             a.ChanDoan,
                             a.TenCB,
                             a.IDKB,
                             noic = (k1 != null) ? k1.noic : "",
                             ngoaic = (k1 != null) ? k1.ngoaic : "",
                         }).ToList();
            var union = (from a in lstVP select new { a.MaBNhan, a.MaDV }).Concat
                        (from b in lstDT select new { b.MaBNhan, b.MaDV }).ToList();

            var benhnhan = (from a in db.BenhNhans
                            select new
                            {
                                a.MaBNhan,
                                bncv = "",
                                TenBV = "",
                                kck = "",
                                tl = "",
                                na = "",
                                kx = "",
                                tk = "",
                                sdm = "",
                                javal = "",
                                sieuam = "",
                                mau = "",
                                nuoctieu = "",
                                soiTT = "",
                                soigocTP = "",
                                sdm3 = "",
                                hsm = ""
                            }).ToList();

            var BN_DV = (from a in lstBN
                         join vp in union on a.MaBNhan equals vp.MaBNhan
                         join dv in lstDV on vp.MaDV equals dv.MaDV
                         group new { a, vp, dv } by new
                         {
                             a.MaBNhan,
                             bncv = "",
                             TenBV = "",
                             kck = (dv.IDNhom == 13) ? "X" : ((dv.IDNhom == null) ? "" : ""),
                             tl = ((dv.TenDV.Contains("Thử Thị Lực")) ? "X" : ""),
                             na = ((dv.TenDV.Contains("Đo nhãn áp")) ? "X" : ""),
                             kx = ((dv.TenDV.Contains("Đo khúc xạ máy")) ? "X" : ""),
                             tk = ((dv.TenDV.Contains("Thử kính")) ? "X" : ""),
                             sdm = ((dv.TenDV.Contains("Soi đáy mắt") && !dv.TenDV.Contains("Soi đáy mắt bằng kính 3 mặt gương")) ? "X" : ((dv.TenDV.Contains("Soi đáy mắt trực tiếp")) ? "X" : "")),
                             javal = ((dv.TenDV.Contains("Đo khúc xạ giác mạc Javal")) ? "X" : ""),
                             sieuam = ((dv.TenRG.Contains("Siêu âm")) ? "X" : ""),
                             mau = ((dv.TenRG.Contains("XN huyết học")) ? "X" : ""),
                             nuoctieu = ((dv.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo)) ? "X" : ""),
                             //soiTT = ((dv.TenDV.Contains("")) ? "" : ""),
                             soiTT = "",
                             soigocTP = ((dv.TenDV.Contains("Soi góc tiền phòng")) ? "X" : ""),
                             sdm3 = ((dv.TenDV.Contains("Soi đáy mắt bằng kính 3 mặt gương")) ? "X" : ""),
                             hsm = ((dv.TenRG.Contains("XN hóa sinh máu")) ? "X" : "")
                         } into kq
                         select new
                         {
                             kq.Key.MaBNhan,
                             kq.Key.bncv,
                             kq.Key.TenBV,
                             kq.Key.kck,
                             kq.Key.tl,
                             kq.Key.na,
                             kq.Key.kx,
                             kq.Key.tk,
                             kq.Key.sdm,
                             kq.Key.javal,
                             kq.Key.sieuam,
                             kq.Key.mau,
                             kq.Key.nuoctieu,
                             kq.Key.soiTT,
                             kq.Key.soigocTP,
                             kq.Key.sdm3,
                             kq.Key.hsm
                         }).ToList();
            var bnrv1 = db.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).ToList();
            var bnrv = (from b in bnrv1
                        join a in dtt on b.MaBNhan equals a.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new
                        {
                            b.MaBNhan,
                            bncv = (k1 != null) ? "" : "X",
                            TenBV = "",
                            kck = "",
                            tl = "",
                            na = "",
                            kx = "",
                            tk = "",
                            sdm = "",
                            javal = "",
                            sieuam = "",
                            mau = "",
                            nuoctieu = "",
                            soiTT = "",
                            soigocTP = "",
                            sdm3 = "",
                            hsm = ""
                        }).ToList();

            var bnbv = (from a in db.BenhNhans
                        join b in db.BenhViens on a.MaBV equals b.MaBV
                        select new
                        {
                            a.MaBNhan,
                            bncv = "",
                            TenBV = b.TenBV,
                            kck = "",
                            tl = "",
                            na = "",
                            kx = "",
                            tk = "",
                            sdm = "",
                            javal = "",
                            sieuam = "",
                            mau = "",
                            nuoctieu = "",
                            soiTT = "",
                            soigocTP = "",
                            sdm3 = "",
                            hsm = ""
                        }).ToList();

            var concat = ((benhnhan.Concat(bnrv)).Concat(BN_DV)).Concat(bnbv).ToList();
            //var concat = benhnhan.Concat(BN_DV).ToList();
            var lstBN_DV = (from a in concat
                            group new { a } by new { a.MaBNhan } into kq
                            select new
                            {
                                kq.Key.MaBNhan,
                                bncv = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.bncv)),
                                TenBV = String.Join(";", kq.Where(p => p.a != null).Select(p => p.a.TenBV)),
                                kck = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.kck)),
                                tl = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.tl)),
                                na = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.na)),
                                kx = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.kx)),
                                tk = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.tk)),
                                sdm = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.sdm)),
                                javal = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.javal)),
                                sieuam = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.sieuam)),
                                mau = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.mau)),
                                nuoctieu = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.nuoctieu)),
                                soiTT = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.soiTT)),
                                soigocTP = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.soigocTP)),
                                sdm3 = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.sdm3)),
                                hsm = String.Join("", kq.Where(p => p.a != null).Select(p => p.a.hsm))
                            }).ToList();


            var sokhambenh = (from a in lstBN
                              join b in lstBN_DV on a.MaBNhan equals b.MaBNhan
                              group new { a, b } by new
                              {
                                  a.MaBNhan,
                                  a.TenBNhan,
                                  a.nam,
                                  a.nu,
                                  noic = (a.noic.Contains("X")) ? "X" : "",
                                  ngoaic = (a.ngoaic.Contains("X")) ? "X" : "",
                                  a.DChi,
                                  a.SThe,
                                  TenBV = (b.TenBV != "") ? b.TenBV : "",
                                  bncv = (b.bncv.Contains("X")) ? "X" : "",
                                  kck = (b.kck.Contains("X")) ? "X" : "",
                                  tl = (b.tl.Contains("X")) ? "X" : "",
                                  na = (b.na.Contains("X")) ? "X" : "",
                                  kx = (b.kx.Contains("X")) ? "X" : "",
                                  tk = (b.tk.Contains("X")) ? "X" : "",
                                  sdm = (b.sdm.Contains("X")) ? "X" : "",
                                  javal = (b.javal.Contains("X")) ? "X" : "",
                                  sieuam = (b.sieuam.Contains("X")) ? "X" : "",
                                  mau = (b.mau.Contains("X")) ? "X" : "",
                                  nuoctieu = (b.nuoctieu.Contains("X")) ? "X" : "",
                                  soiTT = (b.soiTT.Contains("X")) ? "X" : "",
                                  soigocTP = (b.soigocTP.Contains("X")) ? "X" : "",
                                  sdm3 = (b.sdm3.Contains("X")) ? "X" : "",
                                  hsm = (b.hsm.Contains("X")) ? "X" : ""
                              } into kq3
                              select new
                              {
                                  kq3.Key.MaBNhan,
                                  kq3.Key.TenBNhan,
                                  kq3.Key.nam,
                                  kq3.Key.nu,
                                  kq3.Key.noic,
                                  kq3.Key.ngoaic,
                                  kq3.Key.DChi,
                                  kq3.Key.SThe,
                                  kq3.Key.TenBV,
                                  kq3.Key.bncv,
                                  ChanDoan = String.Join(";", kq3.Where(p => p.a != null).Select(p => p.a.ChanDoan)),
                                  TenCB = String.Join(";", kq3.Where(p => p.a != null).Select(p => p.a.TenCB)),
                                  kq3.Key.kck,
                                  kq3.Key.tl,
                                  kq3.Key.na,
                                  kq3.Key.kx,
                                  kq3.Key.tk,
                                  kq3.Key.sdm,
                                  kq3.Key.javal,
                                  kq3.Key.sieuam,
                                  kq3.Key.mau,
                                  kq3.Key.nuoctieu,
                                  kq3.Key.soiTT,
                                  kq3.Key.soigocTP,
                                  kq3.Key.sdm3,
                                  kq3.Key.hsm
                              }).Distinct().ToList();


            foreach (var a in sokhambenh)
            {
                Sokhambenh skb = new Sokhambenh();
                skb.tenBN = a.TenBNhan;
                skb.nam = a.nam;
                skb.nu = a.nu;
                skb.dtNoiC = a.noic;
                skb.dtNgoaiC = a.ngoaic;
                skb.diaChi = a.DChi;
                skb.Bhyt = a.SThe;
                string[] arrTenBV = a.TenBV.Split(';');
                string TenBV = "";
                if (arrTenBV.Count() > 1)
                    for (int j = 0; j < arrTenBV.Count(); j++)
                    {
                        for (int z = j + 1; z < arrTenBV.Count(); z++)
                        {
                            if (arrTenBV[j] == arrTenBV[z] && arrTenBV[z] != null)
                            {
                                arrTenBV[z] = null;
                            }
                        }
                        if (arrTenBV[j] != null)
                            TenBV += arrTenBV[j];
                    }
                else TenBV = a.TenBV;
                skb.noiGT = TenBV;
                skb.choVe = a.bncv;
                skb.kck = a.kck;
                skb.tl = a.tl;
                skb.na = a.na;
                skb.kx = a.kx;
                skb.tk = a.tk;
                skb.sdm = a.sdm;
                skb.javal = a.javal;
                skb.sieuam = a.sieuam;
                skb.mau = a.mau;
                skb.nuocTieu = a.nuoctieu;
                skb.soiTT = a.soiTT;
                skb.soiGocTP = a.soigocTP;
                skb.sdmBangKinh3Mat = a.sdm3;
                skb.hsm = a.hsm;
                string[] arrchuandoan = a.ChanDoan.Split(';');
                string ChanDoan = "";
                if (arrchuandoan.Count() > 1)
                    for (int j = 0; j < arrchuandoan.Count(); j++)
                    {
                        for (int z = j + 1; z < arrchuandoan.Count(); z++)
                        {
                            if (arrchuandoan[j] == arrchuandoan[z] && arrchuandoan[z] != null)
                            {
                                arrchuandoan[z] = null;
                            }
                        }
                        if (arrchuandoan[j] != null)
                            ChanDoan += arrchuandoan[j] + "; ";
                    }
                else ChanDoan = a.ChanDoan;
                string[] arrTenCB = a.TenCB.Split(';');
                string TenCB = "";
                if (arrTenCB.Count() > 1)
                    for (int j = 0; j < arrTenCB.Count(); j++)
                    {
                        for (int z = j + 1; z < arrTenCB.Count(); z++)
                        {
                            if (arrTenCB[j] == arrTenCB[z] && arrTenCB[z] != null)
                            {
                                arrTenCB[z] = null;
                            }
                        }
                        if (arrTenCB[j] != null)
                            TenCB += arrTenCB[j] + "; ";
                    }
                else TenCB = a.TenCB;
                skb.chanDoan = ChanDoan;
                skb.tenBS = TenCB;
                _sokhambenh.Add(skb);
            }
            BaoCao.rep_SoKhamBenh_27022 rep = new BaoCao.rep_SoKhamBenh_27022();
            frmIn frm = new frmIn();
            rep.DataSource = _sokhambenh;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }

        private void cklDTBN_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklDTBN.GetItemCheckState(0) == CheckState.Checked)
                {
                    for (int i = 0; i < cklDTBN.ItemCount; i++)
                    {
                        cklDTBN.SetItemCheckState(i, CheckState.Checked);
                    }
                }
                if (cklDTBN.GetItemCheckState(0) == CheckState.Unchecked)
                {
                    for (int i = 0; i < cklDTBN.ItemCount; i++)
                    {
                        cklDTBN.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }
        }
    }
}