using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;


namespace QLBV.BaoCao
{
    public partial class BCTaiNanLaoDong_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.frm_BCTaiNanLaoDong_30010._ListBNTaiNan> _lkq = new List<FormThamSo.frm_BCTaiNanLaoDong_30010._ListBNTaiNan>();
        public BCTaiNanLaoDong_30010(List<QLBV.FormThamSo.frm_BCTaiNanLaoDong_30010._ListBNTaiNan> _lits)
        {
            InitializeComponent();
            _lkq = _lits;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void Bindingdata()
        {
            coltenbn.DataBindings.Add("Text", DataSource, "TenBN");
            celtuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celnam.DataBindings.Add("Text", DataSource, "Nam");
            celnu.DataBindings.Add("Text", DataSource, "Nu");
            celtrieuchung.DataBindings.Add("Text", DataSource, "TChung");
            celnnghiep.DataBindings.Add("Text", DataSource, "MaNN");
            celngaytainan.DataBindings.Add("Text", DataSource, "NgayN");
            celsongatdt.DataBindings.Add("Text", DataSource, "SoNgaydt");
            celkhoi.DataBindings.Add("Text", DataSource, "Khoi");
            celdogiam.DataBindings.Add("Text", DataSource, "DoGiam");
            celkhongdoi.DataBindings.Add("Text", DataSource, "KhongDoi");
            celnanghon.DataBindings.Add("Text", DataSource, "NangHon");
            celtuvong.DataBindings.Add("Text", DataSource, "TuVong");
        }

        private void BCTaiNanLaoDong_30010_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            xrSubreport1.ReportSource = new BaoCao.Rep_BCTaiNan_30010_Sub1();
            Rep_BCTaiNan_30010_Sub1 repsub = (Rep_BCTaiNan_30010_Sub1)xrSubreport1.ReportSource;
            var _listSub1 = (from a in _lkq
                             group a by new { a.MaCSKCB } into kq
                             select new
                             {
                                 kq.Key.MaCSKCB,
                                 TongSo = kq.Select(p => p.MaBN).Count(),
                                 Khoi = kq.Where(p => p.Khoi == "X").Select(p => p.MaBN).Count(),
                                 DoGiam = kq.Where(p => p.DoGiam == "X").Select(p => p.MaBN).Count(),
                                 KoDoi = kq.Where(p => p.KhongDoi == "X").Select(p => p.MaBN).Count(),
                                 NangHon = kq.Where(p => p.NangHon == "X").Select(p => p.MaBN).Count(),
                                 TuVong = kq.Where(p => p.TuVong == "X").Select(p => p.MaBN).Count(),
                             }).ToList();
            //BaoCao.Rep_BCTaiNan_30010_Sub1 repsub1 = new BaoCao.Rep_BCTaiNan_30010_Sub1();
            repsub.DataSource = _listSub1;
            repsub.Bindingdata();
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            xrSubreport2.ReportSource = new BaoCao.Rep_BCTaiNan_30010_Sub2();
            Rep_BCTaiNan_30010_Sub2 repsub = (Rep_BCTaiNan_30010_Sub2)xrSubreport2.ReportSource;
            var _listsub2 = (from a in _lkq
                             join b in _data.DmNNs on a.MaNN equals b.MaNN
                             group new { a, b } by new { a.MaNN, b.TenNN } into kq
                             select new
                             {
                                 kq.Key.MaNN,
                                 kq.Key.TenNN,
                                 Tong = kq.Select(p => p.a.MaBN).Count()
                             }).ToList();
            repsub.DataSource = _listsub2;
            repsub.Bindingdata();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            Celgd.Text = DungChung.Bien.GiamDoc;
        }
    }
}
