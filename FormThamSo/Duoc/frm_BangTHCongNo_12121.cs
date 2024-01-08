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
    public partial class frm_BangTHCongNo_12121 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BangTHCongNo_12121()
        {
            InitializeComponent();
        }

        private void frm_BangTHCongNo_12121_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<NhaCC> qncc = data.NhaCCs.ToList();
            qncc.Insert(0, new NhaCC { MaCC = "0", TenCC = "Tất cả" });
            cklNhaCC.DataSource = qncc;

            for (int i = 0; i < cklNhaCC.ItemCount; i++)
            {
                cklNhaCC.SetItemChecked(i, true);
            }
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
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
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            int maKP = 0;
            if (lupKho.EditValue != null)
                maKP = Convert.ToInt32(lupKho.EditValue);
            List<NhaCC> lNcc = new List<NhaCC>();
            for (int i = 0; i < cklNhaCC.ItemCount; i++)
            {
                if (cklNhaCC.GetItemChecked(i) == true)
                    lNcc.Add(new NhaCC { MaCC = cklNhaCC.GetItemValue(i).ToString(), TenCC = cklNhaCC.GetItemText(i) });
            }
            var qnd = (from nd in data.NhapDs.Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => maKP == 0 || p.MaKP == maKP).Where(p => p.NgayNhap <= denngay).Where(p => p.NgayTT == null || p.NgayTT >= tungay)
                       join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                       select new { nd.NgayNhap, nd.NgayTT, ndct.MaDV, ndct.MaCC, ndct, ndct.SoLuongN, ndct.ThanhTienN, ndct.DonGiaCT, ndct.VAT }).ToList();

            var qnd1 = (from nd in qnd
                        join ncc in lNcc on nd.MaCC equals ncc.MaCC
                        group new { nd, ncc } by new { ncc.MaCC, ncc.TenCC } into kq
                        select new
                        {
                            kq.Key.TenCC,
                            TonDK = kq.Where(p => p.nd.NgayNhap < tungay).Where(p => p.nd.NgayTT == null || p.nd.NgayTT >= tungay).Sum(p => p.nd.ThanhTienN),
                            NhapTK = kq.Where(p => p.nd.NgayNhap >= tungay && p.nd.NgayNhap <= denngay).Sum(p => p.nd.ThanhTienN),
                            TTTK = kq.Where(p => p.nd.NgayTT >= tungay && p.nd.NgayTT <= denngay).Sum(p => p.nd.ThanhTienN),
                            TonCK = kq.Where(p => p.nd.NgayTT == null || p.nd.NgayTT > denngay).Sum(p => p.nd.ThanhTienN)
                        }).ToList();
            if (qnd1.Count == 0)
                MessageBox.Show("Không có dữ liệu");
            else
            {
                BaoCao.rep_BangTHCongNo_12121 rep = new BaoCao.rep_BangTHCongNo_12121();
                frmIn frm = new frmIn();
                rep.DataSource = qnd1;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void cklNhaCC_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklNhaCC.GetItemCheckState(0) == CheckState.Checked)
                {
                    for (int i = 0; i < cklNhaCC.ItemCount; i++)
                    {
                        cklNhaCC.SetItemChecked(i, true);
                    }
                }
                else
                    for (int i = 0; i < cklNhaCC.ItemCount; i++)
                    {
                        cklNhaCC.SetItemChecked(i, false);
                    }
            }
        }


    }
}