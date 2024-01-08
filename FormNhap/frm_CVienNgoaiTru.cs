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
    public partial class frm_CVienNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        int  _mabn = 0;
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_CVienNgoaiTru()
        {
            InitializeComponent();
        }
        public frm_CVienNgoaiTru(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = Status;
            btnInPhieu.Enabled = Status;
            btnThoat.Enabled = Status;
        }

        List<RaVien> _chuyenvien = new List<RaVien>();

               
        private void frm_CVienNgoaiTru_Load(object sender, EventArgs e)
        {
            dtHanC.DateTime = System.DateTime.Now;
            int mb = _mabn;
            var qbv = (from TK in DataContect.BenhViens.Where(p => p.status == 3) select new { TK.TenBV, TK.MaBV }).ToList();
            lupBVC.Properties.DataSource = qbv.ToList();

            var tenbv = DataContect.RaViens.Where(p => p.MaBNhan== _mabn).ToList();
            if (tenbv.Count > 0)
            {

                lupBVC.EditValue = tenbv.First().MaBVC;
            }

            var cv = (from bn in DataContect.BenhNhans
                      where (bn.MaBNhan== _mabn)
                      join bnkb in DataContect.BNKBs on bn.MaBNhan equals bnkb.MaBNhan 
                      join ttbs in DataContect.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan                     
                      select new { bn.TenBNhan, bn.Tuoi, bn.MaBNhan, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.DChi, ttbs.MaNN}).ToList();
            if (cv.Count > 0)
            {
                txtTenBNhan.Text = cv.First().TenBNhan;
                txtMaBNhan.Text = cv.First().MaBNhan == null ? "" : cv.First().MaBNhan.ToString();
                txtTuoi.Text = cv.First().Tuoi.ToString();
                var dantoc = (from ttbs in DataContect.TTboXungs join dt in DataContect.DanTocs on ttbs.MaDT equals dt.MaDT select dt.TenDT).ToList();
                if (dantoc.Count > 0)
                    txtTenDT.Text = dantoc.First();
                if (cv.First().GTinh == 1)
                {
                    txtGTinh.Text = "Nam";
                }
                else txtGTinh.Text = "Nữ";

                txtNgheNghiep.Text = cv.First().MaNN;
                txtSThe.Text = cv.First().SThe;
                txtHanBHTu.Text = cv.First().HanBHTu.ToString();
                txtHanBHDen.Text = cv.First().HanBHDen.ToString();
                txtDChi.Text = cv.First().DChi;
                
            }
            _chuyenvien = DataContect.RaViens.ToList();
            var crv = DataContect.RaViens.Where(p => p.MaBNhan== _mabn).ToList();
            if (crv.Count > 0)
            {
                txtSo.Text = crv.First().SoChuyenVien.ToString();
                if (int.Parse(txtSo.Text) <= 100)
                {
                    txtQuyenSo.Text = "1";
                }
                if (int.Parse(txtSo.Text) > 100 && int.Parse(txtSo.Text) <= 200)
                {
                    txtQuyenSo.Text = "2";
                }
                if (int.Parse(txtSo.Text) > 300)
                {
                    txtQuyenSo.Text = "3";
                }
                
                txtLyDoCV.Text = crv.First().LyDoC;
                txtGGTSo.Text = crv.First().SoGT.ToString();

            }
           
            btnLuu.Enabled = false;
        }

      
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            BaoCao.repGiayCVNgoaitru rep = new BaoCao.repGiayCVNgoaitru();
            if (_mabn != null)
            {
                var par = (from bn in DataContect.BenhNhans
                           join cv in DataContect.RaViens on bn.MaBNhan equals cv.MaBNhan
                           join ttbx in DataContect.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                           join bv in DataContect.BenhViens on cv.MaBVC equals bv.MaBV
                           where (bn.MaBNhan == _mabn)
                           select new { bn.TenBNhan, bn.Tuoi, cv.HanC,bn.NNhap, bn.GTinh, ttbx.NgoaiKieu, ttbx.MaNN, ttbx.NoiLV, bn.HanBHTu, bn.HanBHDen, bn.SThe, bn.DChi, cv.LyDoC, cv.SoChuyenVien,bv.TenBV,cv.NgayRa,  }).ToList();
                if (par.Count > 0)
                {
                    rep.Chuyenden.Value = par.First().TenBV;
                    rep.NgayHHC.Value = par.First().HanC;
                    rep.TenBN.Value = par.First().TenBNhan.ToUpper();
                    var dtoc = (from ttbx in DataContect.TTboXungs.Where(p => p.MaBNhan== _mabn) join dt in DataContect.DanTocs on ttbx.MaDT equals dt.MaDT select dt.TenDT).ToList();
                    if (dtoc.Count > 0)
                        rep.Dantoc.Value = dtoc.First();
                    rep.Tuoi.Value = par.First().Tuoi;
                    rep.Gioitinh.Value = par.First().GTinh;
                    rep.Ngoaikieu.Value = par.First().NgoaiKieu;
                    rep.Nghenghiep.Value = par.First().MaNN;
                    rep.GTBHYTtu.Value = par.First().HanBHTu;
                    rep.GTBHYTden.Value = par.First().HanBHDen;
                    rep.Sothe.Value = par.First().SThe;
                    rep.Diachi.Value = par.First().DChi;
                    rep.Lydochuyen.Value = par.First().LyDoC;
                    rep.SoGGT.Value = par.First().SoChuyenVien;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ;
                    rep.Ngayvao.Value = par.First().NNhap;
                    rep.Ngayra.Value = par.First().NgayRa;
                    rep.Noilamviec.Value = par.First().NoiLV;
                }

                var bsdt = (from kb in DataContect.BNKBs.Where(p=>p.MaBNhan== _mabn)
                            join cb in DataContect.CanBoes on kb.MaCB equals cb.MaCB
                            select new { cb.TenCB }).ToList();
                if (bsdt.Count > 0)
                {
                    rep.BSDT.Value = bsdt.First().TenCB;
                }
                var bscv = (from rv in DataContect.RaViens.Where(p => p.MaBNhan== _mabn)
                            join cb in DataContect.CanBoes on rv.MaCB equals cb.MaCB
                            select new { cb.TenCB }).ToList();
                if (bsdt.Count > 0)
                {
                    rep.BSchuyen.Value = bsdt.First().TenCB;
                }

                rep.QuyenSo.Value = txtQuyenSo.Text;
            }
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            // luu bang RaVien
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int ot;
                if (Int32.TryParse(txtMaBNhan.Text, out ot))
                    _mabn = Convert.ToInt32(txtMaBNhan.Text);               
                var kt = DataContect.RaViens.Where(p => p.MaBNhan== _mabn).ToList();
                if (kt.Count > 0)
                {
                    //sửa
                    int id = kt.First().IdRaVien;
                    RaVien nhapcv = _data.RaViens.Single(p => p.IdRaVien== (id));
                    if (!string.IsNullOrEmpty(txtSo.Text))
                    {
                        nhapcv.SoChuyenVien = int.Parse(txtSo.Text);
                    }
                    if (!string.IsNullOrEmpty(txtGGTSo.Text))
                    {
                        nhapcv.SoGT = int.Parse(txtGGTSo.Text);
                    }
                        nhapcv.LyDoC = txtLyDoCV.Text;
                    if (lupBVC.EditValue != null)
                        nhapcv.MaBVC = lupBVC.EditValue.ToString();
                    nhapcv.HanC = dtHanC.DateTime.Date;
                    _data.SaveChanges();
                    MessageBox.Show("Lưu thành công!");
                    btnLuu.Enabled = false;
                }
                else
                {
                    RaVien nhapcv = new RaVien();
                    nhapcv.MaBNhan = _mabn;
                    if (!string.IsNullOrEmpty(txtSo.Text))
                    {
                        nhapcv.SoChuyenVien = int.Parse(txtSo.Text);
                    }
                    if (!string.IsNullOrEmpty(txtGGTSo.Text))
                    {
                        nhapcv.SoGT = int.Parse(txtGGTSo.Text);
                    }
                    nhapcv.Status = 1;
                    nhapcv.HanC = dtHanC.DateTime.Date;
                    nhapcv.LyDoC = txtLyDoCV.Text;
                    if (lupBVC.EditValue != null)
                        nhapcv.MaBVC = lupBVC.EditValue.ToString();
                    _data.RaViens.Add(nhapcv);
                    _data.SaveChanges();
                    MessageBox.Show("Lưu thành công!");
                    btnLuu.Enabled = false;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupBVC_EditValueChanged(object sender, EventArgs e)
        {
            if (_chuyenvien.Count > 0)
            {
                if (lupBVC.EditValue !=null && lupBVC.EditValue.ToString()!= _chuyenvien.First().MaBVC)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtQuyenSo_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled =true;
        }

        private void txtSo_EditValueChanged(object sender, EventArgs e)
        {
            if (_chuyenvien.Count > 0)
            {
                if (txtSo.Text != _chuyenvien.First().SoChuyenVien.ToString())
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }  

        private void txtLyDoCV_EditValueChanged(object sender, EventArgs e)
        {
            if (_chuyenvien.Count > 0)
                {
                    if (txtLyDoCV.Text != _chuyenvien.First().LyDoC)
                        btnLuu.Enabled = true;
                    else
                        btnLuu.Enabled = false;
                }
                else btnLuu.Enabled = true;
        }

        private void txtGGTSo_EditValueChanged(object sender, EventArgs e)
        {
            if (_chuyenvien.Count > 0)
            {
                if (txtGGTSo.Text != _chuyenvien.First().SoGT.ToString())
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }
        
    }
}