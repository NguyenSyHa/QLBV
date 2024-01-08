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
    public partial class Frm_TKChiPhiKCBBHYT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TKChiPhiKCBBHYT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        //private class BN
        //{
        //    private string PLoaiBN;
        //    private string MaBNhan;
        //    private string SThe;
        //    private string TenBNhan;
        //    private string NamSinh;
        //    private string DChi;
        //    private string NoiKCBBD;
        //    private string GTriTu;
        //    private string GTriDen;
        //    private string NgayVao;
        //    private string NgayRa;
        //    private string Tong;
           
        //    public string ploaibn
        //    { set { PLoaiBN = value; } get { return PLoaiBN; } }
        //    public string mabnhan
        //    { set { MaBNhan = value; } get { return MaBNhan; } }
        //    public string tenbnhan
        //    { set { TenBNhan = value; } get { return TenBNhan; } }
        //    public string namsinh
        //    { set { NamSinh = value; } get { return NamSinh; } }
        //    public string dchi
        //    { set { DChi = value; } get { return DChi; } }
        //    public string noikcbbd
        //    { set { NoiKCBBD = value; } get { return NoiKCBBD; } }
        //    public string gtritu
        //    { set { GTriTu = value; } get { return GTriTu; } }
        //    public string gtden
        //    { set { GTriDen = value; } get { return GTriDen; } }
        //    public string ngayvao
        //    { set { NgayVao = value; } get { return NgayVao; } }
        //    public string ngayra
        //    { set { NgayRa = value; } get { return NgayRa; } }
        //    public string tong
        //    { set { Tong = value; } get { return Tong; } }
        //}
        //List<BN> _BNhan = new List<BN>();
        private void Frm_TKChiPhiKCBBHYT_Load(object sender, EventArgs e)
        {
            //_BNhan.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
           
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            frmIn frm = new frmIn();
            if (KTtaoBc())
            {
                //_BNhan.Clear();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                BaoCao.Rep_TkChiPhiKCBBHYT rep = new BaoCao.Rep_TkChiPhiKCBBHYT();
                rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + "đến ngày" + denngay.ToString().Substring(0, 10);
                if (radTimKiem.SelectedIndex == 0)
                {
                    var qvp = (from bn in data.BenhNhans.Where(p => p.DTuong == "BHYT")
                               join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                               join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               where (rv.NgayRa >= tungay && rv.NgayRa <= denngay)
                               group new { bn, rv, vp, vpct } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.MaCS, bn.NamSinh, bn.NoiTru, bn.DTuong, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.NNhap, rv.NgayRa } into kq
                               select new
                               {
                                   kq.Key.MaBNhan,
                                  
                                   TenBNhan = kq.Key.TenBNhan,
                                   NamSinh = kq.Key.NamSinh,
                                   DTuong = kq.Key.DTuong,
                                   NoiTru = kq.Key.NoiTru,
                                   SThe = kq.Key.SThe,
                                   DChi = kq.Key.DChi,
                                   HanBHTu = kq.Key.HanBHTu,
                                   HanBHDen = kq.Key.HanBHDen,
                                   NNhap = kq.Key.NNhap,
                                   NgayRa = kq.Key.NgayRa,
                                   NgayVao = kq.Key.NNhap,
                                   NoiKCBBD = kq.Key.MaCS,
                                   Tong = kq.Sum(p => p.vpct.ThanhTien)
                               }).OrderBy(p => p.MaBNhan).ToList();
                    if (qvp.Count() > 0)
                    {
                        if (cboTKBN.Text == "Tất cả BN")
                        {
                            rep.DataSource = qvp.OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 1;

                        }
                        if (cboTKBN.Text == "BN Nội trú")
                        {
                            rep.DataSource = qvp.Where(p => p.NoiTru == 1).OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 0;

                        }
                        if (cboTKBN.Text == "BN Ngoại trú")
                        {
                            rep.DataSource = qvp.Where(p => p.NoiTru == 0).OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 0;

                        }
                    }
                }
                else {
                    var qvp = (from bn in data.BenhNhans.Where(p => p.DTuong == "BHYT")
                               join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                               join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               where (vp.NgayTT >= tungay && vp.NgayTT <= denngay)
                               group new { bn, rv, vp, vpct } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.MaCS, bn.NamSinh, bn.NoiTru, bn.DTuong, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.NNhap, rv.NgayRa } into kq
                               select new
                               {
                                  
                                   MaBNhan = kq.Key.MaBNhan,
                                   TenBNhan = kq.Key.TenBNhan,
                                   NamSinh = kq.Key.NamSinh,
                                   DTuong = kq.Key.DTuong,
                                   NoiTru = kq.Key.NoiTru,
                                   SThe = kq.Key.SThe,
                                   DChi = kq.Key.DChi,
                                   HanBHTu = kq.Key.HanBHTu,
                                   HanBHDen = kq.Key.HanBHDen,
                                   NNhap = kq.Key.NNhap,
                                   NgayRa = kq.Key.NgayRa,
                                   NgayVao = kq.Key.NNhap,
                                   NoiKCBBD = kq.Key.MaCS,
                                   Tong = kq.Sum(p => p.vpct.ThanhTien)
                               }).OrderBy(p => p.MaBNhan).ToList();
                    if (qvp.Count() > 0)
                    {
                        if (cboTKBN.Text == "Tất cả BN")
                        {
                            rep.DataSource = qvp.OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 1;

                        }
                        if (cboTKBN.Text == "BN Nội trú")
                        {
                            rep.DataSource = qvp.Where(p => p.NoiTru == 1).OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 0;

                        }
                        if (cboTKBN.Text == "BN Ngoại trú")
                        {
                            rep.DataSource = qvp.Where(p => p.NoiTru == 0).OrderBy(p => p.TenBNhan).OrderBy(p => p.MaBNhan).ToList();
                            rep.BN.Value = 0;

                        }
                    }
                }
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

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}