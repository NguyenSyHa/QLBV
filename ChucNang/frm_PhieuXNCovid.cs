using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_PhieuXNCovid : Form
    {
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_PhieuXNCovid()
        {
            InitializeComponent();
        }
        public frm_PhieuXNCovid(int MaBN, string TenNhom, int IdCLS)
        {
            InitializeComponent();
            txtMaBN.Text = Convert.ToString(MaBN);
            txtTenNhom.Text = TenNhom;
            txtIdCls.Text = Convert.ToString(IdCLS);
        }

        private void frm_PhieuXNCovid_Load(object sender, EventArgs e)
        {
            var listPP = _Data.DichVus.Where(p => p.TenDV == "Xét nghiệm nhanh kháng nguyên virus SARS-CoV-2").Select(p => p.TenPhuongPhap).ToList();
            var list = listPP.First().Split(';').ToList();
            List<ListPP> newList = new List<ListPP>();
            for (int i = 0; i < list.Count(); i++)
            {
                newList.Add(new ListPP
                {
                    PhuongPhap = list[i]
                });
            }
            grcPhuongPhap.DataSource = newList;
        }

        private void grvPhuongPhap_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string PP = grvPhuongPhap.GetFocusedRowCellValue(PhuongPhap).ToString();
            int _Mabn = Convert.ToInt32(txtMaBN.Text);
            string TenNhom = txtTenNhom.Text;
            int idcls = Convert.ToInt32(txtIdCls.Text);
            var q1 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls in _Data.CLS.Where(p => p.IdCLS == idcls) on bn.MaBNhan equals cls.MaBNhan
                        join cdcls in _Data.ChiDinhs on cls.IdCLS equals cdcls.IdCLS
                        join ttbs in _Data.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                        select new
                        { cls.GhiChu, cls.MaKP, NgayTH = cls.NgayTH, cls.MaCBth, bn.MaBNhan, bn.TenBNhan, bn.TChung, bn.DChi, bn.GTinh, bn.SThe, cls.ChanDoan, MaCBDT = cls.MaCB, cls.BarCode, ttbs.SoKSinh, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, ttbs.DThoai, ttbs.Mach_NDo_HAp }).ToList();

           
            if (q1.Count > 0)
            {
                
                var CBDT = GetCB(q1.First().MaCBDT);
                var CBTH = GetCB(q1.First().MaCBth);
                var NgayTH = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q1.First().NgayTH), 2);
                var NgSinh = (q1.First().NgaySinh != null ? q1.First().NgaySinh : "") + "/" + (q1.First().ThangSinh != null ? q1.First().ThangSinh : "") + "/" + (q1.First().NamSinh != null ? q1.First().NamSinh : "");
                var NhietDo = " Nhiệt độ : " + (q1.First().Mach_NDo_HAp != null ? q1.First().Mach_NDo_HAp.Split(';')[1] : "") + "°C";
                var TrieuChung = q1.First().TChung != null ? q1.First().TChung : "";
                var KPDT = GetKP(Convert.ToInt32(q1.First().MaKP));
                dic.Add("TenBNhan", q1.First().TenBNhan.ToUpper());
                dic.Add("DC", q1.First().DChi);
                dic.Add("GT", q1.First().GTinh.ToString() == "1" ? "Nam" : "Nữ");
                dic.Add("NS", NgSinh);
                dic.Add("SThe", q1.First().SThe != null ? q1.First().SThe : "");
                dic.Add("KP", KPDT);
                dic.Add("CD", q1.First().ChanDoan);
                dic.Add("CBCD", CBDT);
                dic.Add("CBTH", CBTH);
                dic.Add("CanCuoc", q1.First().SoKSinh != null ? q1.First().SoKSinh : "");
                dic.Add("DT", q1.First().DThoai != null ? q1.First().DThoai : "");
                dic.Add("NDo", NhietDo);
                dic.Add("TChung", TrieuChung);
                dic.Add("NgayTH", NgayTH);
                dic.Add("MaBN", "Số: " + _Mabn);
                dic.Add("KL", q1.First().GhiChu != null ? q1.First().GhiChu : "");
            }

            var q2 = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                        join cls3 in _Data.CLS.Where(p => p.IdCLS == idcls) on bn.MaBNhan equals cls3.MaBNhan
                        join cdcls in _Data.ChiDinhs on cls3.IdCLS equals cdcls.IdCLS
                        join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                        join clsct in _Data.CLScts on cdcls.IDCD equals clsct.IDCD
                        group new { dv, clsct } by new { TenDV = DungChung.Bien.MaBV == "20001 " ? dv.TenRG : dv.TenDV, dv.DonVi, clsct.KetQua } into kq
                        select new { YC = kq.Key.TenDV, KQ = kq.Key.KetQua }).ToList();
            List<ListKQ> lkq = new List<ListKQ>();
            foreach(var item in q2)
            {
                ListKQ kq = new ListKQ();
                kq.TenDV = item.YC;
                kq.PhuongPhap = PP;
                kq.KetQua = item.KQ;
                lkq.Add(kq);
            }

            DungChung.Ham.Print(DungChung.PrintConfig.Rep_XNCovid19_30372, lkq, dic, false);
        }
        private string GetCB(string macb)
        {
            var qcb = _Data.CanBoes.Where(p => p.MaCB == macb).FirstOrDefault();
            if (qcb != null)
                return qcb.ChucVu + " " + qcb.TenCB;
            else
                return "";
        }
        private string GetKP(int makp)
        {
            var qkp = _Data.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
            if (qkp != null)
                return qkp.TenKP;
            else
                return "";
        }
    }
    public class ListPP
    {
        public string PhuongPhap { get; set; }
    }
    public class ListKQ
    {
        public string KetQua { get; set; }
        public string TenDV { get; set; }
        public string PhuongPhap { get; set; }
    }
    public class TTPhieu
    {
        public int MaBn { get; set; }
        public string HoTen { get;set;}
        public string NgSinh { get; set; }
        public string DChi { get;set;}
        public string GTinh { get; set; }
        public string KPhong { get; set; }
        public string SThe { get; set; }
        public string BSCD { get; set; }
        public string ChanDoan { get; set; }
        public string CCCD { get; set; }
        public string DThoai { get; set; }
        public string NhietDo { get; set; }
        public string TrieuChungHH { get; set; }
    }
    
}
