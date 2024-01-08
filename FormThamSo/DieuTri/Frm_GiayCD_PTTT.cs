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
    public partial class Frm_GiayCD_PTTT : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        
        public Frm_GiayCD_PTTT(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_GiayCD_PTTT_Load(object sender, EventArgs e)
        {
            var bn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => new { p.TenBNhan}).ToList();
            if (bn.Count > 0)
            { lab1.Text = ("Cam đoan PT, TT và GMHS cho bệnh nhân: " + bn.First().TenBNhan).ToUpper();
            txtTenBN.Text = bn.First().TenBNhan;
            }
            else { lab1.Text = ("Cam đoan của bệnh nhân: ").ToUpper(); txtTenBN.Text = ""; }
            var id = (from kb in Data.BNKBs.Where(p=>p.MaBNhan==_mabn)
                      group kb by kb.MaBNhan into kq
                      select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
            var qkp = (from i in id
                      join kb in Data.BNKBs on i.IDKB equals kb.IDKB
                     // join kp in Data.KPhongs on kb.MaKP equals kp.MaKP
                      select new { kb.MaKP }).ToList();
            if (qkp.Count > 0)
            {
                lupKhoa.EditValue = qkp.First().MaKP;
            }
            var q = Data.KPhongs.Where(p=>p.PLoai=="Lâm sàng"||p.PLoai=="Phòng khám").Select(p => new { p.MaKP, p.TenKP }).ToList();
            lupKhoa.Properties.DataSource = q.ToList();
         
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            BaoCao.Rep_GiayCD_PTTT rep = new BaoCao.Rep_GiayCD_PTTT();

            if (txtTenCD.Text != null && txtTenCD.Text!="") { rep.HoTenCD.Value = txtTenCD.Text.ToUpper(); }
            else { rep.HoTenCD.Value="............................................."; }
            if (txtTuoi.Text != null&&txtTuoi.Text!="") { rep.Tuoi.Value = txtTuoi.Text; }
            else{rep.Tuoi.Value="......";}
            if (radGT.SelectedIndex == 0) { rep.Nu.Value = "/".ToUpper(); }
            if (radGT.SelectedIndex == 1) { rep.Nam.Value = "/".ToUpper(); }
            if (txtDanToc.Text != null&&txtDanToc.Text!="") { rep.DanToc.Value = txtDanToc.Text; } else { rep.DanToc.Value = "..........................................."; }
            if (txtNK.Text != null&&txtNK.Text!="") { rep.NgoaiKieu.Value = txtNK.Text; } else { rep.NgoaiKieu.Value = "..................................................."; }
            if (txtNN.Text != null&&txtNN.Text!="") { rep.NgheNghiep.Value = txtNN.Text; } else { rep.NgheNghiep.Value = ".................................................."; }
            if (txtNoiLV.Text != null&&txtNoiLV.Text!="") { rep.NoiLV.Value = txtNoiLV.Text; } else { rep.NoiLV.Value = "....................................................."; }
            if (txtDC.Text != null&&txtDC.Text!="") { rep.DiaChi.Value = txtDC.Text; } else { rep.DiaChi.Value = ".........................................................................."; }
            if (txtTenBN.Text != null&&txtTenBN.Text!="") { rep.HoTenBN.Value = txtTenBN.Text; } else { rep.HoTenBN.Value = "......................................"; }
            if (lupKhoa.EditValue != null&&lupKhoa.EditValue.ToString()!="")
            { int _makp = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            var kp = Data.KPhongs.Where(p => p.MaKP == _makp).Select(p => new { p.TenKP }).ToList();
            if (kp.Count > 0) { rep.TenKP.Value = kp.First().TenKP; }
            }
            var bv = Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => new { p.TenBV }).ToList();
            if (bv.Count > 0)
            {
                rep.TenBV.Value = bv.First().TenBV;
            }
            else
            { rep.TenBV.Value = ".........................................."; }
            if (rad2.SelectedIndex == 0)
            {
                rep.Tich1.Value = "x".ToUpper();
            }
            else { rep.Tich2.Value = "x".ToUpper(); }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void resetcontrol()
        {
            txtTenCD.Text = "";
            txtDC.Text = "";
            txtTuoi.Text = "";
            txtNK.EditValue = "";
            txtNoiLV.Text = "";
            txtNK.Text = "";
            //txtSoDK.Text = "";
            //txtNhaSX.Text = "";
            //txtQCPC.Text = "";
        }
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk.Checked == true)
            {
                var qbn = Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => new { p.TenBNhan, p.DChi, p.Tuoi, p.GTinh }).ToList();
                if (qbn.Count > 0)
                {
                    txtTenCD.Text = qbn.First().TenBNhan;
                    txtDC.Text = qbn.First().DChi;
                    txtTuoi.Text = qbn.First().Tuoi.ToString();
                    if (qbn.First().GTinh == 1) { radGT.EditValue = 0; }
                    if (qbn.First().GTinh == 0) { radGT.EditValue = 1; }
                    var dt = Data.TTboXungs.Where(p => p.MaBNhan == _mabn).Select(p => new { p.NgoaiKieu, p.NoiLV, p.MaDT, p.MaNN }).ToList();
                    if (dt.Count() > 0)
                    {
                        txtNK.Text = dt.First().NgoaiKieu;
                        txtNoiLV.Text = dt.First().NoiLV;
                        if (dt.First().MaDT != null)
                        {
                            string _madt = dt.First().MaDT;
                            var dtoc = Data.DanTocs.Where(p => p.MaDT == _madt).Select(p => new { p.TenDT }).ToList();
                            if (dtoc.Count > 0) { txtDanToc.Text = dtoc.First().TenDT; }

                        }
                        if(dt.First().MaNN!=null)
                        {
                            string _mann=dt.First().MaNN;
                            var nn = Data.DmNNs.Where(p => p.MaNN == _mann).Select(p => new { p.TenNN }).ToList();
                            if (nn.Count > 0) { txtNN.Text = nn.First().TenNN; }
                   
                        }
                    }
                    
                }
            }
            else
            {
                resetcontrol();
            }
        }

       
    }
}