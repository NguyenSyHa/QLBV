using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frmDonNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        public frmDonNgoaiTru()
        {
            InitializeComponent();
        }

        private void frmDonNgoaiTru_Load(object sender, EventArgs e)
        {
            //vidu truy van tu 2 bang
            /*var query =from BN in DaTaContext.BNHANs 
                       join BHYT in DaTaContext.BNBHYTs on BN.STHE equals BHYT.STHE
                       select new (BN.MSBN,BN.TENBN);*/
            //C1
            //var query = DaTaContext.BNHANs;
            //QLBV_Database.QLBVEntities DataContext=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //var que = from TenDuoc in DataContext.Duocs
            //          join Maduoc in DataContext.NhapDcts on TenDuoc.MaDuoc equals Maduoc.MaDuoc
            //          join Maduocx in DataContext.XuatDcts on Maduoc.MaDuoc equals Maduocx.MaDuoc
            //          group Maduoc by new { TenDuoc.TenDuoc, Maduoc.MaDuoc, Maduoc.DonVi, Maduoc.DonGia,Maduoc.HanDung } into DG
            //          orderby DG.Key.TenDuoc, DG.Key.DonGia
                      
            //          select new { DG.Key.TenDuoc, DG.Key.MaDuoc, DG.Key.DonVi, DG.Key.DonGia,DG.Key.HanDung ,
            //          SLTon = DG.Sum(p=>p.SoLuong)- DG.Sum(p=>p.DonGia),
            //          };
            //lupMaDuoc.DataSource = que.ToList();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}