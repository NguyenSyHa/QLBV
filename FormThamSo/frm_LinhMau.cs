using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_LinhMau : DevExpress.XtraEditors.XtraForm
    {
        public frm_LinhMau()
        {
            InitializeComponent();
        }
        int _mabn, _idkb;
        private int id = 0;
        private string tencb = "";
        private int x = 0, y = 0, xx = 0, yy = 0;
        double gia1 = 0;
        public frm_LinhMau(int mabn, int idkb)
        {
            _mabn = mabn;
            _idkb = idkb;
            InitializeComponent();
        }
        public class MyObject
        {
            public int Value { set; get; }
            public string Text { set; get; }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        List<LinhMau> mau = new List<LinhMau>();
        private void frm_LinhMau_Load(object sender, EventArgs e)
        {
            id = 0;
            mau.Clear();
            #region load trong bao hiem
            List<MyObject> _ltrongbh = new List<MyObject>();
            _ltrongbh.Add(new MyObject { Value = 0, Text = "Ngoài DM" });
            _ltrongbh.Add(new MyObject { Value = 1, Text = "Trong DM" });
            lookUpEdit1.Properties.DataSource = _ltrongbh;
            #endregion
            var bn = (from a in _data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                      join b in _data.KPhongs on a.MaKP equals b.MaKP
                      join c in _data.BNKBs.Where(p => p.IDKB == _idkb) on a.MaBNhan equals c.MaBNhan
                      join d in _data.VaoViens on a.MaBNhan equals d.MaBNhan
                      select new { a.MaBNhan, a.TenBNhan, b.TenKP, a.Tuoi, a.GTinh, c.Buong, c.Giuong, c.ChanDoan, d.NhomMau, d.HeMau,a.MaKP,c.MaCB, a.DTuong }).ToList();
            if(bn.Count>0)
            {
                MaBN.Text = Convert.ToString(bn.First().MaBNhan);
                TenBN.Text = bn.First().TenBNhan;
                Tuoi.Text = Convert.ToString(bn.First().Tuoi);
                GT.Text = bn.First().GTinh == 1 ? "Nam" : "Nữ";
                KhoaDT.Text = bn.First().TenKP;
                string[] arrThongTinBNKB;
                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(_data, _mabn, bn.First().MaKP ?? 0,true);
                ChanDoan.Text = arrThongTinBNKB[1];
                Buong.Text = arrThongTinBNKB[2];
                Giuong.Text = arrThongTinBNKB[3];
                NhomMau.Text = bn.First().NhomMau != null ? bn.First().NhomMau : "";
                HeMau.Text = bn.First().HeMau != null ? bn.First().HeMau : "";
                string macb = bn.First().MaCB;
                var cb = _data.CanBoes.Where(p => p.MaCB == macb).ToList();
                tencb = cb.First().TenCB;
            }
            var mau1 = (from a in _data.LinhMaus.Where(p => p.MaBNhan == _mabn) select a).ToList();
            foreach (var a in mau1)
            {
                LinhMau themmoi = new LinhMau();
                themmoi.IdLinhMau = a.IdLinhMau;
                themmoi.NgayDuTru = a.NgayDuTru;
                themmoi.SoLuong_yc = a.SoLuong_yc;
                mau.Add(themmoi);
            }
            gridControl1.DataSource = mau.ToList();
            var dv = (from a in _data.DichVus
                      join b in _data.NhomDVs on a.IDNhom equals b.IDNhom
                      join c in _data.TieuNhomDVs.Where(p => p.TenTN.ToLower().Contains("máu và chế phẩm của máu")) on a.IdTieuNhom equals c.IdTieuNhom
                      select a).ToList();
            List<DichVu> dv1 = dv.ToList();
            dichvu.Properties.DataSource = dv1;
           
        }
        
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            xx = 0;
            yy = 0;
            id = 0;
            if (gridView1.GetFocusedRowCellValue(IdLinhMau) != null)
            {
                id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(IdLinhMau).ToString());
                var mau1 = (from a in _data.LinhMaus.Where(p => p.MaBNhan == _mabn && p.IdLinhMau == id) select a).ToList();
                if(mau1.Count>0)
                {
                    sophieu.Text = Convert.ToString(mau1.First().IdLinhMau);
                    ngaydutru1.DateTime = mau1.First().NgayDuTru;
                    soluongyc.Text = Convert.ToString(mau1.First().SoLuong_yc);
                    nhommauyc.Text = mau1.First().NhomMauABO_yc;
                    hemauyc.Text = mau1.First().NhomMauRh_yc;
                    iddonct.Text = Convert.ToString(mau1.First().IDDonct);
                    if (mau1.First().NgayLinhMau != null)
                    ngaylinh.DateTime = mau1.First().NgayLinhMau.Value;
                    soluongtl.Text = Convert.ToString(mau1.First().SoLuong_tl);
                    nhommautl.Text = mau1.First().NhomMauABO_tl;
                    hemautl.Text = mau1.First().NhomMauRh_tl;
                    dichvu.EditValue = mau1.First().MaDV;
                    gia.Text = Convert.ToString(mau1.First().DonGia);
                    gia1 = mau1.First().DonGia;
                    tyle.Text = Convert.ToString(mau1.First().TyLeTT);
                    lookUpEdit1.EditValue = mau1.First().TrongBH;

                }
            }
            else
            {
                sophieu.Text = "";
                ngaydutru1.DateTime = DateTime.Now;
                soluongyc.Text = "";
                nhommauyc.Text = "";
                hemauyc.Text = "";
                iddonct.Text = "";
                ngaylinh.Text = "";
                soluongtl.Text = "";
                nhommautl.Text = "";
                hemautl.Text = "";
                dichvu.EditValue = null;
                gia.Text = "";
                tyle.Text = "100";
            }
            if(sophieu.Text != "")
            {
                simpleButton3.Enabled = true;
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var bnkb = (from a in _data.BNKBs.Where(p => p.IDKB == _idkb) select a).ToList();
            int makp1 = bnkb.First().MaKP??0;
            if (ngaydutru1.DateTime >= bnkb.First().NgayKham)
            {
                if (ngaylinh.DateTime < ngaydutru1.DateTime && ngaylinh.Text != "")
                {
                    MessageBox.Show("Ngày lĩnh không được nhỏ hơn ngày dự trù!");
                }
                else
                {
                    if (dichvu.Text != "")
                    {
                        //var dt = (from a in _data.DThuocs.Where(p => p.MaBNhan == _mabn && p.PLDV == 2) select new { a.MaBNhan, a.IDDon }).ToList();
                        //int iddon = 0;
                        //if(dt.Count > 0)
                        //    iddon = dt.First().IDDon;
                        //else
                        //{
                        //    DThuoc moi2 = new DThuoc();
                        //    moi2.MaKP = makp1;
                        //    moi2.MaBNhan = _mabn;
                        //    moi2.PLDV = 2;
                        //    moi2.NgayKe = ngaylinh.DateTime;
                        //    moi2.KieuDon = 0;
                        //    _data.DThuocs.Add(moi2);
                        //    _data.SaveChanges();
                        //}
                        int idd = (sophieu.Text != "") ? Convert.ToInt32(sophieu.Text) : 0;
                        var mau1 = _data.LinhMaus.Where(p => p.IdLinhMau == idd).ToList();
                        int madv = dichvu.EditValue != null ? Convert.ToInt32(dichvu.EditValue) : 0;
                        if (mau1.Count > 0)
                        {
                            LinhMau moi = _data.LinhMaus.Single(p => p.IdLinhMau == idd);
                            moi.MaBNhan = _mabn;
                            moi.IDKB = _idkb;
                            moi.MaDV = madv;
                            moi.DonGia = gia.Text != "" ? Convert.ToDouble(gia.Text) : 0;
                            moi.NgayDuTru = ngaydutru1.DateTime;
                            moi.NhomMauABO_yc = nhommauyc.Text;
                            moi.NhomMauRh_yc = hemauyc.Text;
                            moi.SoLuong_yc = soluongyc.Text != "" ? Convert.ToDouble(soluongyc.Text) : 0;
                            
                            if (ngaylinh.DateTime.Year > 2000)
                            {
                                if (iddonct.Text == "0")
                                {
                                    DThuoc moi2 = new DThuoc();
                                    moi2.MaKP = makp1;
                                    moi2.MaBNhan = _mabn;
                                    moi2.PLDV = 1;
                                    moi2.NgayKe = ngaylinh.DateTime;
                                    moi2.KieuDon = 0;
                                    moi2.MaCB = DungChung.Bien.MaCB;
                                    _data.DThuocs.Add(moi2);
                                    DThuocct moi1 = new DThuocct();
                                    moi1.IDDon = moi2.IDDon;
                                    moi1.MaDV = madv;
                                    moi1.DonVi = dichvu.EditValue != null ? _data.DichVus.Where( p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                    moi1.DonGia = gia.Text != "" ? Convert.ToDouble(gia.Text) : 0;
                                    moi1.SoLuong = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                    moi1.ThanhTien = Convert.ToDouble(gia.Text) * (soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0) * (tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0)/100;
                                    moi1.TienBH = 0;
                                    moi1.TienBN = 0;
                                    moi1.TienChenh = 0;
                                    moi1.TrongBH = lookUpEdit1.EditValue != "" ? Convert.ToInt32(lookUpEdit1.EditValue) : 0;
                                    moi1.SoPL = 0;
                                    moi1.IDKB = _idkb;
                                    moi1.Loai = 0;
                                    moi1.ThanhToan = 0;
                                    moi1.Mien = 0;
                                    moi1.MaKPtk = 0;
                                    moi1.TyLeTT = tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0;
                                    moi1.XHH = 0;
                                    moi1.LoaiDV = 0;
                                    moi1.NgayNhap = ngaylinh.DateTime;
                                    moi1.MaKP = makp1;
                                    moi1.SoLuongct = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                    moi1.Luong = "1";
                                    moi1.MoiLan = "lần,mỗi lần";
                                    moi1.SoLan = "1";
                                    moi1.DviUong = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                    moi1.DuongD = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DuongD : "";
                                    moi1.MaCB = DungChung.Bien.MaCB;
                                    _data.DThuoccts.Add(moi1);
                                    _data.SaveChanges();

                                    moi.IDDonct = moi1.IDDonct;
                                    moi.NgayLinhMau = ngaylinh.DateTime;
                                    moi.NhomMauABO_tl = nhommautl.Text;
                                    moi.NhomMauRh_tl = hemautl.Text;
                                    moi.SoLuong_tl = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                }
                                else
                                {
                                    int iddonct1 = Convert.ToInt32(iddonct.Text);
                                    DThuocct moi1 = _data.DThuoccts.Single(p => p.IDDonct == iddonct1);
                                    moi1.NgayNhap = ngaylinh.DateTime;
                                    moi1.MaDV = madv;
                                    moi1.DonVi = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                    moi1.DonGia = gia.Text != "" ? Convert.ToDouble(gia.Text) : 0;
                                    moi1.SoLuong = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                    moi1.TyLeTT = tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0;
                                    moi1.TrongBH = lookUpEdit1.EditValue != "" ? Convert.ToInt32(lookUpEdit1.EditValue) : 0;
                                    moi1.ThanhTien = Convert.ToDouble(gia.Text) * (soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0) * (tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0) / 100;
                                    moi1.NgayNhap = ngaylinh.DateTime;
                                    moi1.MaKP = makp1;
                                    moi1.SoLuongct = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                    moi1.Luong = "1";
                                    moi1.MoiLan = "lần,mỗi lần";
                                    moi1.SoLan = "1";
                                    moi1.DviUong = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                    moi1.DuongD = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DuongD : "";
                                    moi1.MaCB = DungChung.Bien.MaCB;

                                    moi.IDDonct = Convert.ToInt32(iddonct.Text);
                                    moi.NgayLinhMau = ngaylinh.DateTime;
                                    moi.NhomMauABO_tl = nhommautl.Text;
                                    moi.NhomMauRh_tl = hemautl.Text;
                                    moi.SoLuong_tl = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                }
                            }
                            moi.TyLeTT = tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0;
                            if (_data.SaveChanges() >= 0)
                            {
                                MessageBox.Show("Lưu thành công");
                                frm_LinhMau_Load(sender, e);
                            }
                        }
                        else
                        {
                            LinhMau moi = new LinhMau(); ;
                            moi.MaBNhan = _mabn;
                            moi.IDKB = _idkb;
                            moi.MaDV = dichvu.EditValue != null ? Convert.ToInt32(dichvu.EditValue) : 0;
                            moi.DonGia = gia.Text != "" ? Convert.ToDouble(gia.Text) : 0;
                            moi.NgayDuTru = ngaydutru1.DateTime;
                            moi.NhomMauABO_yc = nhommauyc.Text;
                            moi.NhomMauRh_yc = hemauyc.Text;
                            moi.SoLuong_yc = soluongyc.Text != "" ? Convert.ToDouble(soluongyc.Text) : 0;

                            if (ngaylinh.DateTime.Year > 2000)
                            {
                                
                                DThuoc moi2 = new DThuoc();
                                moi2.MaKP = makp1;
                                moi2.MaBNhan = _mabn;
                                moi2.PLDV = 1;
                                moi2.NgayKe = ngaylinh.DateTime;
                                moi2.KieuDon = 0;
                                moi2.MaCB = DungChung.Bien.MaCB;
                                _data.DThuocs.Add(moi2);
                                _data.SaveChanges();
                                DThuocct moi1 = new DThuocct();
                                moi1.IDDon = moi2.IDDon;
                                moi1.MaDV = madv;
                                moi1.DonVi = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                moi1.DonGia = gia.Text != "" ? Convert.ToDouble(gia.Text) : 0;
                                moi1.SoLuong = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                moi1.ThanhTien = Convert.ToDouble(gia.Text) * (soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0) * (tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0) / 100;
                                moi1.TienBH = 0;
                                moi1.TienBN = 0;
                                moi1.TienChenh = 0;
                                moi1.TrongBH = lookUpEdit1.EditValue != null ? Convert.ToInt32(lookUpEdit1.EditValue) : 0;
                                moi1.SoPL = 0;
                                moi1.IDKB = _idkb;
                                moi1.Loai = 0;
                                moi1.ThanhToan = 0;
                                moi1.Mien = 0;
                                moi1.MaKPtk = 0;
                                moi1.TyLeTT = tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0;
                                moi1.XHH = 0;
                                moi1.LoaiDV = 0;
                                moi1.NgayNhap = ngaylinh.DateTime;
                                moi1.MaKP = makp1;
                                moi1.MaCB = DungChung.Bien.MaCB;
                                moi1.SoLuongct = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                                moi1.Luong = "1";
                                moi1.MoiLan = "lần,mỗi lần";
                                moi1.SoLan = "1";
                                moi1.DviUong = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DonVi : "";
                                moi1.DuongD = dichvu.EditValue != null ? _data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault().DuongD : "";
                                moi1.MaCB = DungChung.Bien.MaCB;
                                _data.DThuoccts.Add(moi1);
                                _data.SaveChanges();

                                moi.IDDonct = moi1.IDDonct;
                                moi.NgayLinhMau = ngaylinh.DateTime;
                                moi.NhomMauABO_tl = nhommautl.Text;
                                moi.NhomMauRh_tl = hemautl.Text;
                                moi.SoLuong_tl = soluongtl.Text != "" ? Convert.ToDouble(soluongtl.Text) : 0;
                            }

                            moi.TrongBH = lookUpEdit1.EditValue != "" ? Convert.ToInt32(lookUpEdit1.EditValue) : 0;
                            moi.TyLeTT = tyle.Text != "" ? Convert.ToInt32(tyle.Text) : 0;
                            _data.LinhMaus.Add(moi);
                            _data.SaveChanges();
                            if (_data.SaveChanges() >= 0)
                            {
                                MessageBox.Show("Lưu thành công");
                                
                                frm_LinhMau_Load(sender, e);
                            }
                        }
                        simpleButton4.Enabled = true;
                    }
                    else
                        MessageBox.Show("Chưa nhập dịch vụ!");
                }
                
            }
            else
                MessageBox.Show("Ngày dự trù phải lớn hơn ngày khám!");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sophieu.Text = "";
            ngaydutru1.DateTime = DateTime.Now;
            soluongyc.Text = "";
            nhommauyc.Text = "";
            hemauyc.Text = "";
            iddonct.Text = "";
            ngaylinh.Text = "" ;
            soluongtl.Text = "";
            nhommautl.Text = "";
            hemautl.Text = "";
            dichvu.EditValue = null;
            gia.Text = "";
            tyle.Text ="100";
            lookUpEdit1.EditValue = null ;
            simpleButton3.Enabled = false;
            simpleButton4.Enabled = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(id != 0)
            {
                var test = _data.LinhMaus.Where(p => p.IdLinhMau == id).ToList();
                int idct1 = test.First().IDDonct;
                if (idct1 > 0)
                {
                    DThuocct dtct = _data.DThuoccts.Single(p => p.IDDonct == idct1);
                    _data.DThuoccts.Remove(dtct);
                    LinhMau lm = _data.LinhMaus.Single(p => p.IdLinhMau == id);
                    _data.LinhMaus.Remove(lm);
                    if (_data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Xóa thành công");
                        frm_LinhMau_Load(sender, e);
                    }
                }
                else
                {
                    LinhMau lm = _data.LinhMaus.Single(p => p.IdLinhMau == id);
                    _data.LinhMaus.Remove(lm);
                    if (_data.SaveChanges() >= 0)
                    {
                        MessageBox.Show("Xóa thành công");
                        frm_LinhMau_Load(sender, e);
                    }
                }
                
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            simpleButton3.Enabled = true;
            simpleButton4.Enabled = true;
            frm_LinhMau_Load(sender, e);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            BaoCao.rep_PhieuLinhMau rep = new BaoCao.rep_PhieuLinhMau();
            rep.ten.Value = TenBN.Text;
            rep.tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_data, _mabn) :  Tuoi.Text;
            rep.gioitinh.Value = GT.Text;
            rep.chandoan.Value = ChanDoan.Text;
            rep.khoaphong.Value = KhoaDT.Text;
            rep.nhommauyc.Value = nhommauyc.Text;
            rep.hemauyc.Value = hemauyc.Text;
            rep.dvdt.Value = dichvu.Text;
            rep.sldt.Value = soluongyc.Text;
            rep.sltltl.Value = soluongtl.Text;
            rep.bsdt.Value = tencb;
            rep.buong.Value = Giuong.Text;
            if (ngaydutru1.DateTime != null)
                rep.ngaydutru.Value = ngaydutru1.DateTime.Hour + " giờ " + ngaydutru1.DateTime.Minute + " phút, ngày " + ngaydutru1.DateTime.Day + " tháng " + ngaydutru1.DateTime.Month + " năm " + ngaydutru1.DateTime.Year;
            else
                rep.ngaydutru.Value = ".....giờ....phút,ngày....tháng....năm....";
            if(ngaylinh.DateTime != null)
                rep.ngaythuclinh.Value = ngaylinh.DateTime.Hour + " giờ " + ngaylinh.DateTime.Minute + " phút, ngày " + ngaylinh.DateTime.Day + " tháng " + ngaylinh.DateTime.Month + " năm " + ngaylinh.DateTime.Year;
            else
                rep.ngaythuclinh.Value = ".....giờ....phút,ngày....tháng....năm....";
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            int idd = Convert.ToInt32(dichvu.EditValue);
            int idtrongbh = Convert.ToInt32(lookUpEdit1.EditValue);
            if(idd>0)
            {
                xx++;
                var dvv = (from a in _data.DichVus.Where(p => p.MaDV == idd) select new { a.MaDV, a.DonGia, a.DonGia2 }).ToList();
                if (idtrongbh == 0)
                    gia.Text = Convert.ToString(dvv.First().DonGia2);
                else
                    gia.Text = Convert.ToString(dvv.First().DonGia);
            }
            if(xx==2 && sophieu.Text != "")
            {
                gia.Text = Convert.ToString(gia1);
            }
           
        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1_FocusedRowChanged(null, null);
        }

        private void dichvu_EditValueChanged(object sender, EventArgs e)
        {
            int idd = Convert.ToInt32(dichvu.EditValue);
            
            if (idd > 0)
            {
                yy++;
                var bn = (from a in _data.BenhNhans.Where(p => p.MaBNhan == _mabn) select a).ToList();
                var dvv = (from a in _data.DichVus.Where(p => p.MaDV == idd) select new { a.MaDV, a.DonGia, a.DonGia2, a.BHTT, a.TrongDM }).ToList();
                if (bn.Count > 0)
                {
                    if (bn.First().DTuong == "BHYT")
                        lookUpEdit1.EditValue = dvv.First().TrongDM??0;
                    else
                        lookUpEdit1.EditValue = 0;
                }
                int idtrongbh = Convert.ToInt32(lookUpEdit1.EditValue);
                if (idtrongbh == 0)
                    gia.Text = Convert.ToString(dvv.First().DonGia2);
                else
                    gia.Text = Convert.ToString(dvv.First().DonGia);
                tyle.Text = dvv.First().BHTT != null ? Convert.ToString(dvv.First().BHTT) : "100";
            }

            if (yy == 2 && sophieu.Text != "")
            {
                gia.Text = Convert.ToString(gia1);
            }
        }

        private void ngaylinh_EditValueChanged(object sender, EventArgs e)
        {
            if (ngaylinh.DateTime != Convert.ToDateTime("01/01/0001 00:00") && y == 0)
            {
                string a = ngaylinh.DateTime.Day + "/" + ngaylinh.DateTime.Month + "/" + ngaylinh.DateTime.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                y++;
                ngaylinh.DateTime = Convert.ToDateTime(a);
            }
            else if (ngaylinh.DateTime == Convert.ToDateTime("01/01/0001 00:00:00"))
            {
                y = 0;
            }
            
            if (soluongtl.Text == "")
            {
                soluongtl.Text = soluongyc.Text;
                nhommautl.Text = nhommauyc.Text;
                hemautl.Text = hemauyc.Text;
            }
            
        }
        
        private void ngaydutru1_EditValueChanged(object sender, EventArgs e)
        {
            if (ngaydutru1.DateTime != Convert.ToDateTime( "01/01/0001 00:00") && x == 0)
            {
                string a = ngaydutru1.DateTime.Day + "/" + ngaydutru1.DateTime.Month + "/" + ngaydutru1.DateTime.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                x++;
                ngaydutru1.DateTime = Convert.ToDateTime(a);
            }
            else if (ngaydutru1.DateTime == Convert.ToDateTime("01/01/0001 00:00:00"))
            {
                x = 0;
            }
        }

    }
}