using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_thongkecanbophongkham : DevExpress.XtraEditors.XtraForm
    {
        public frm_thongkecanbophongkham()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void frm_thongkecanbophongkham_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime ngaytu=System.DateTime.Now;
            DateTime ngayden=System.DateTime.Now;
            ngaytu=DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden=DungChung.Ham.NgayDen(lupNgayden.DateTime);
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from DT in Data.DThuocs
                    join CB in Data.CanBoes on DT.MaCB equals CB.MaCB
                    join BN in Data.BenhNhans.Where(p => p.NoiTru == 0) on DT.MaBNhan equals BN.MaBNhan
                    where (DT.NgayKe >= ngaytu & DT.NgayKe <= ngayden)
                    group new { CB, DT } by new { CB.TenCB } into kq
                    select new
                    {
                        TenBS = kq.Key.TenCB,
                        Soluottong = kq.Select(p => p.DT.MaBNhan).Count()
                    };
            frmIn frm = new frmIn();
            BaoCao.Rep_thongkecanbophongkham rep = new BaoCao.Rep_thongkecanbophongkham();
            rep.ngaythang.Value = "Từ ngày: " + lupNgaytu.Text + " Đến ngày: " + lupNgayden.Text;
            rep.TenBV.Value = DungChung.Bien.TenCQ;
            rep.Macs.Value = DungChung.Bien.MaBV;
            rep.DataSource = q.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}