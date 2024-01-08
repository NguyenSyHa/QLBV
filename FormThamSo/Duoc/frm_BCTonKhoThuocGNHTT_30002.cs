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
    public partial class frm_BCTonKhoThuocGNHTT_30002 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTonKhoThuocGNHTT_30002()
        {
            InitializeComponent();
        }
        public class MyObject
        {
            public int Value { set; get; }
        }
        private void frm_BCTonKhoThuocGNHTT_30002_Load(object sender, EventArgs e)
        {
            //load ds năm
            int namHT = DateTime.Now.Year;
            List<MyObject> _list = new List<MyObject>();
            for (int i = namHT - 10; i < namHT + 11; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }
            cbNam.DisplayMember = "Value";
            cbNam.ValueMember = "Value";
            cbNam.DataSource = _list;
            cbNam.SelectedValue = namHT;

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKho.Properties.DataSource = lkp;
            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";
            lupKho.EditValue = lupKho.Properties.GetKeyValueByDisplayText("Tất cả");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if (rd6Thang.SelectedIndex == 0)// 6 tháng đầu năm
            {
                tungay =new DateTime(Convert.ToInt32(cbNam.Text), 1,1);
                denngay = DungChung.Ham.NgayDen(tungay.AddMonths(6).AddDays(-1));
            }
            else if (rd6Thang.SelectedIndex == 1) // 6 tháng cuối năm
            {
                tungay = new DateTime(Convert.ToInt32(cbNam.Text), 7, 1);
                denngay = DungChung.Ham.NgayDen(tungay.AddYears(1).AddDays(-1));
            }
            int kho = 0;
            if(lupKho.EditValue != null)
                kho =  Convert.ToInt32(lupKho.EditValue);

            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Thuốc gây nghiện" || p.TenRG == "Thuốc hướng tâm thần") on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV , dv.TenDV}).ToList();
            var qnd = (from nd in data.NhapDs.Where(p => p.NgayNhap <= denngay).Where(p=>p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3).Where(p=>kho == 0 || p.MaKP == kho)
                       join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                       select new { 
                           ndct.MaDV,
                           SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                           SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,
                           ndct.DonVi, nd.NgayNhap, nd.PLoai, nd.KieuDon, nd.XuatTD                    
                       }).ToList();

            var q1 = (from dv in qdv join nd in qnd on dv.MaDV equals nd.MaDV 
                      group new {nd, dv} by new {dv.TenDV, nd.MaDV, nd.DonVi} into kq
                      select new { kq.Key.TenDV, kq.Key.DonVi,kq.Key.MaDV,
                                   TonDK = kq.Where(p => p.nd.NgayNhap < tungay).Sum(p => p.nd.SoLuongN) - kq.Where(p => p.nd.NgayNhap < tungay).Sum(p => p.nd.SoLuongX) + (ckNhapTheoHD.Checked ? kq.Where(p=>p.nd.NgayNhap >= tungay &&p.nd.NgayNhap <= denngay).Where(p=>p.nd.PLoai == 1 && p.nd.XuatTD != 2).Sum(p=>p.nd.SoLuongN) : 0 ),
                                   NhapTK = kq.Where(p=>p.nd.NgayNhap >= tungay &&p.nd.NgayNhap <= denngay).Where(p=>p.nd.PLoai == 1 && (ckNhapTheoHD.Checked ? p.nd.XuatTD == 2 : true)).Sum(p=>p.nd.SoLuongN),// nhập theo đơn ?
                                   XuatTK = kq.Where(p => p.nd.NgayNhap >= tungay && p.nd.NgayNhap <= denngay).Where(p => p.nd.PLoai == 2).Sum(p => p.nd.SoLuongX),// xuất trong kỳ
                                   HuHao = kq.Where(p => p.nd.NgayNhap >= tungay && p.nd.NgayNhap <= denngay).Where(p => p.nd.PLoai == 3).Sum(p => p.nd.SoLuongX),// xuất hư hao

                      }).ToList();
            var q2 = (from nd in q1
                      select new { 
                      nd.TenDV,
                      nd.DonVi,
                      nd.TonDK,
                      nd.NhapTK,
                      TongDauVao = nd.TonDK + nd.NhapTK ,
                      nd.XuatTK,
                      nd.HuHao,
                      TonCK = nd.TonDK + nd.NhapTK - nd.XuatTK - nd.HuHao
                      }).Where(p=>p.TongDauVao != 0 || p.XuatTK != 0 || p.HuHao != 0).ToList();


            frmIn frm = new frmIn();
            BaoCao.rep_BCTonKhoThuocGNHTT_30002 rep = new BaoCao.rep_BCTonKhoThuocGNHTT_30002();
            rep.celTitTonTrongKy.Text = "Số lượng mua trong 06 tháng năm " + cbNam.Text;
            rep.celTitXuatTK.Text = "Số lượng xuất trong 06 tháng năm " + cbNam.Text;
            if (rd6Thang.SelectedIndex == 0)
            {
                rep.cel6thang.Text = "6 THÁNG ĐẦU NĂM " + cbNam.Text;
                rep.celTitTonDK.Text = "Số lượng tồn kho tháng 12/" + (Convert.ToInt32(cbNam.Text) - 1).ToString() + " chuyển sang";
                rep.celTitTonCK.Text = " Tồn kho đến tháng 6 năm " + cbNam.Text;
            }
            else
            {
                rep.cel6thang.Text = "6 THÁNG CUỐI NĂM " + cbNam.Text;
                rep.celTitTonDK.Text = "Số lượng tồn kho tháng 6/" + cbNam.Text + " chuyển sang";
                rep.celTitTonCK.Text = " Tồn kho đến tháng 12/" + cbNam.Text;
            }
           
            rep.BindingData();
            rep.DataSource = q2.OrderBy(p => p.TenDV).ToList();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }
    }
}