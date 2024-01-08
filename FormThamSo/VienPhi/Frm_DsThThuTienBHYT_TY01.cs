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
    public partial class Frm_DsThThuTienBHYT_TY01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsThThuTienBHYT_TY01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        private void Frm_DsThThuTienBHYT_TY01_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            List<CanBo> _lcb = new List<CanBo>();
            _lcb = (from kp in dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan || p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh || p.PLoai == DungChung.Bien.st_PhanLoaiKP.Admin)
                    join cb in dataContext.CanBoes on kp.MaKP equals cb.MaKP
                    select cb).Distinct().ToList();
            _lcb.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupcbthu.Properties.DataSource = _lcb;
            lupcbthu.EditValue = "";
            rad_Duyet_SelectedIndexChanged(null, null);
        }
        public class TienBHYT
        {
            public DateTime NTN;
            public int MaBN;
            public string TenBN;
            public string dChi;
            public int tuoi;
            public double TongT;
            public double Tien20a;
            public double Tien5a;
            public double Tien44a;
            public double Tien30a;
            public double Tien33a;
            public double SoNop;
            public double NgoaiDM;
            public int mucThu;
            private DateTime ngay;

            public DateTime Ngay
            {
                get { return ngay; }
                set { ngay = value; }
            }
            private DateTime? ngayDuyet;

            public DateTime? NgayDuyet
            {
                get { return ngayDuyet; }
                set { ngayDuyet = value; }
            }



            public int MucThu
            { set { mucThu = value; } get { return mucThu; } }
            public DateTime NgayTT
            { set { NTN = value; } get { return NTN; } }
            public int MaBNhan
            { set { MaBN = value; } get { return MaBN; } }
            public string TenBNhan
            { set { TenBN = value; } get { return TenBN; } }
            public string DChi
            { set { dChi = value; } get { return dChi; } }
            public int Tuoi
            { set { tuoi = value; } get { return tuoi; } }
            public double TongTien
            { set { TongT = value; } get { return TongT; } }
            public double Tien5
            { set { Tien5a = value; } get { return Tien5a; } }
            public double Tien20
            { set { Tien20a = value; } get { return Tien20a; } }
            public double Tien44
            { set { Tien44a = value; } get { return Tien44a; } }
            public double Tien33
            { set { Tien33a = value; } get { return Tien33a; } }
            public double Tien30
            { set { Tien30a = value; } get { return Tien30a; } }
            public double SoTienNop
            { set { SoNop = value; } get { return SoNop; } }
            public double ngoaidm
            { set { NgoaiDM = value; } get { return NgoaiDM; } }
        }

        private int SetMucThu(double tien5, double tien20, double tien30, double tien33, double tien44)
        {
            int rs = 0;
            if (tien5 > 0)
                rs = 5;
            else if (tien20 > 0)
                rs = 20;
            else if (tien30 > 0)
                rs = 30;
            else if (tien33 > 0)
                rs = 33;
            else if (tien44 > 0)
                rs = 44;
            return rs;
        }
        List<TienBHYT> _TienBHYT = new List<TienBHYT>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            //int noitru = -1;
            //noitru = radNoitru.SelectedIndex;
            frmIn frm = new frmIn();

            _TienBHYT.Clear();
            bool HTNgoaiDM = ckcngoaidm.Checked;
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                int duyet = rad_Duyet.SelectedIndex;
                string macbthu = "";
                if (lupcbthu.EditValue != null)
                    macbthu = Convert.ToString(lupcbthu.EditValue);


                var qbh1 = (from bn in dataContext.BenhNhans.Where(p => p.DTuong == "BHYT" && ((ckNoiTru.Checked && p.NoiTru == 1) || (ckNgoaiTru.Checked && (p.NoiTru == 0 && p.DTNT == false)) || (ckDTNT.Checked && (p.NoiTru == 0 && p.DTNT == true))))
                            join vp in dataContext.VienPhis.Where(p => DungChung.Bien.MaBV == "30005" ? (duyet == 2 ? true : (macbthu == "" ? true : p.MaCB == macbthu)) : macbthu == "" ? true : p.MaCB == macbthu).Where(p => duyet == 2 || (duyet == 0 && p.NgayDuyet == null) || (duyet == 1 && p.NgayDuyet != null)) on bn.MaBNhan equals vp.MaBNhan
                            join vpct in dataContext.VienPhicts.Where(p => p.TienBN != 0).Where(P => HTNgoaiDM == false ? P.TrongBH == 1 : (P.TrongBH == 1 || P.TrongBH == 0)).Where(p => p.ThanhToan == 0)
                            on vp.idVPhi equals vpct.idVPhi
                            // join tu in dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2 || p.PhanLoai == 3) on vp.MaBNhan equals tu.MaBNhan
                            where (rdLoaiNgay.SelectedIndex == 0 ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) : (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden))
                            select new
                            {
                                bn.TenBNhan,
                                bn.Tuoi,
                                bn.DChi,
                                vp.NgayTT,
                                vp.NgayDuyet,
                                bn.MaBNhan,
                                vpct.TienBN,
                                vpct.ThanhTien,
                                bn.Tuyen,
                                bn.MucHuong,
                                bn.KhuVuc,
                                vpct.TrongBH,
                                vpct.idVPhict
                                //vp.idVPhi,
                                //bn.TenBNhan,
                                //bn.Tuoi,
                                //bn.DChi,
                                //vp.NgayTT,
                                //vp.NgayDuyet,
                                //MaBNhan = bn.MaBNhan,
                                //bn.Tuyen,
                                //bn.MucHuong,
                                //bn.KhuVuc
                            }).Distinct().ToList();
                if (DungChung.Bien.MaBV == "30005" && duyet == 1)
                {
                    qbh1 = (from bn in dataContext.BenhNhans.Where(p => p.DTuong == "BHYT" && ((ckNoiTru.Checked && p.NoiTru == 1) || (ckNgoaiTru.Checked && (p.NoiTru == 0 && p.DTNT == false)) || (ckDTNT.Checked && (p.NoiTru == 0 && p.DTNT == true))))
                            join vp in dataContext.VienPhis.Where(p => duyet == 2 || (duyet == 0 && p.NgayDuyet == null) || (duyet == 1 && p.NgayDuyet != null)) on bn.MaBNhan equals vp.MaBNhan
                            join tu in dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => macbthu == "" ? true : p.MaCB == macbthu) on bn.MaBNhan equals tu.MaBNhan
                            join vpct in dataContext.VienPhicts.Where(p => p.TienBN != 0).Where(P => HTNgoaiDM == false ? P.TrongBH == 1 : (P.TrongBH == 1 || P.TrongBH == 0)).Where(p => p.ThanhToan == 0)
                            on vp.idVPhi equals vpct.idVPhi
                            // join tu in dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2 || p.PhanLoai == 3) on vp.MaBNhan equals tu.MaBNhan
                            where (rdLoaiNgay.SelectedIndex == 0 ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) : (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden))
                            select new
                            {
                                bn.TenBNhan,
                                bn.Tuoi,
                                bn.DChi,
                                vp.NgayTT,
                                vp.NgayDuyet,
                                bn.MaBNhan,
                                vpct.TienBN,
                                vpct.ThanhTien,
                                bn.Tuyen,
                                bn.MucHuong,
                                bn.KhuVuc,
                                vpct.TrongBH,
                                vpct.idVPhict
                                //vp.idVPhi,
                                //bn.TenBNhan,
                                //bn.Tuoi,
                                //bn.DChi,
                                //vp.NgayTT,
                                //vp.NgayDuyet,
                                //MaBNhan = bn.MaBNhan,
                                //bn.Tuyen,
                                //bn.MucHuong,
                                //bn.KhuVuc
                            }).Distinct().ToList();
                }
                //var qbh1 = (from bn in qbh2
                //            join vpct in dataContext.VienPhicts.Where(p => p.TienBN > 0).Where(P => HTNgoaiDM == false ? P.TrongBH == 1 : (P.TrongBH == 1 || P.TrongBH == 0)).Where(p => p.ThanhToan == 0)
                //            on bn.idVPhi equals vpct.idVPhi
                //            select new
                //            {
                //                bn.TenBNhan,
                //                bn.Tuoi,
                //                bn.DChi,
                //                bn.NgayTT,
                //                MaBNhan = bn.MaBNhan,
                //                vpct.TienBN,
                //                vpct.ThanhTien,
                //                bn.Tuyen,
                //                bn.MucHuong,
                //                bn.KhuVuc,
                //                vpct.TrongBH
                //            }).ToList();
                var qbh = (from b in qbh1
                           group b by new { b.MaBNhan, b.NgayTT, b.NgayDuyet, b.TenBNhan, b.Tuoi, b.DChi } into kq
                           select new
                           {
                               kq.Key.TenBNhan,
                               kq.Key.Tuoi,
                               kq.Key.DChi,
                               kq.Key.NgayTT,
                               kq.Key.NgayDuyet,
                               MaBNhan = kq.Key.MaBNhan,
                               SoTienNop = kq.Sum(p => p.TienBN),
                               TongTien = kq.Sum(p => p.ThanhTien),
                               Tien20dt = kq.Where(p => p.Tuyen == 1).Where(p => p.MucHuong == 4).Where(p => p.TrongBH == 1).Sum(p => p.TienBN),
                               Tien20tt = kq.Where(p => p.Tuyen == 2).Where(p => p.MucHuong == 4).Where(p => p.TrongBH == 1).Where(p => p.KhuVuc != "").Sum(p => p.TienBN),
                               Tien5dt = kq.Where(p => p.Tuyen == 1).Where(p => p.MucHuong == 3).Where(p => p.TrongBH == 1).Sum(p => p.TienBN),
                               Tien5tt = kq.Where(p => p.Tuyen == 2).Where(p => p.MucHuong == 3).Where(p => p.TrongBH == 1).Where(p => p.KhuVuc != "").Sum(p => p.TienBN),
                               Tien44 = kq.Where(p => p.Tuyen == 2).Where(p => p.MucHuong == 4).Where(p => p.TrongBH == 1).Where(p => p.KhuVuc == "").Sum(p => p.TienBN),
                               Tien33 = kq.Where(p => p.Tuyen == 2).Where(p => p.MucHuong == 3).Where(p => p.TrongBH == 1).Where(p => p.KhuVuc == "").Sum(p => p.TienBN),
                               Tien30 = kq.Where(p => p.Tuyen == 2).Where(p => p.MucHuong != 4).Where(p => p.TrongBH == 1).Where(p => p.MucHuong != 3).Where(p => p.KhuVuc == "").Sum(p => p.TienBN),
                               NgoaiDM = kq.Where(p => p.TrongBH == 0).Sum(p => p.ThanhTien)
                           }).ToList();
                //                    
                if (qbh.Count > 0)
                {
                    foreach (var a in qbh)
                    {
                        double c = 0, b = 0, d = 0, f = 0;
                        TienBHYT moi = new TienBHYT();
                        moi.NgayTT = a.NgayTT.Value.Date;
                        if (a.NgayDuyet != null)
                        {
                            moi.NgayDuyet = a.NgayDuyet.Value.Date;
                        }
                        else moi.NgayDuyet = null;

                        if (rdLoaiNgay.SelectedIndex == 0)
                        {
                            moi.Ngay = a.NgayDuyet.Value.Date;
                        }
                        else
                        {
                            moi.Ngay = a.NgayTT.Value.Date;
                        }
                        moi.MaBNhan = a.MaBNhan;
                        moi.TenBNhan = a.TenBNhan;
                        moi.Tuoi = a.Tuoi.Value;
                        moi.DChi = a.DChi;
                        b = a.Tien20dt;
                        c = a.Tien20tt;
                        moi.Tien20 = b + c;
                        d = a.Tien5dt;
                        f = a.Tien5tt;
                        moi.Tien5 = d + f;
                        moi.Tien33 = a.Tien33;
                        moi.Tien44 = a.Tien44;
                        moi.Tien30 = a.Tien30;
                        moi.TongTien = a.TongTien;
                        moi.SoTienNop = a.SoTienNop;
                        moi.NgoaiDM = a.NgoaiDM;
                        moi.MucThu = SetMucThu(moi.Tien5, moi.Tien20, moi.Tien30, moi.Tien33, moi.Tien44);
                        _TienBHYT.Add(moi);
                    }

                }
                if (qbh.Count > 0)
                {
                    if (chkMauMoi.Checked && DungChung.Bien.MaBV == "30005")
                    {
                        string NguoiLapBieu = dataContext.HThong_User.FirstOrDefault(o => o.TenDN == DungChung.Bien.TenDN).NguoiLapBieu;
                        if (HTNgoaiDM)
                        {
                            BaoCao.Rep_DsThThuTienBHYT_TY01_New_MauMoi rep = new BaoCao.Rep_DsThThuTienBHYT_TY01_New_MauMoi();
                            if (ckNgoaiTru.Checked && ckcngoaidm.Checked)
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % BHYT ngoại trú tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            else if (ckNoiTru.Checked && ckcngoaidm.Checked && ckDTNT.Checked)
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % BHYT nội trú tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            else
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % viện phí BHYT tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            rep.TuNgayDenNgay.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " Đến ngày " + ngayden.ToString().Substring(0, 10);
                            rep.TongTien.Value = _TienBHYT.Sum(p => p.TongTien).ToString();
                            rep.colNguoiLapBieu.Text = NguoiLapBieu;


                            // if (rdLoaiNgay.SelectedIndex == 0)
                            // {
                            //     rep.DataSource = _TienBHYT.OrderBy(p => p.NgayDuyet);
                            // }
                            // else
                            // {
                            //     rep.DataSource = _TienBHYT.OrderBy(p => p.NgayTT);
                            // }
                            rep.DataSource = _TienBHYT.OrderBy(p => p.NgayTT);
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            BaoCao.Rep_DsThThuTienBHYT_TY01_MauMoi rep = new BaoCao.Rep_DsThThuTienBHYT_TY01_MauMoi();
                            if (ckNgoaiTru.Checked && ckcngoaidm.Checked)
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % BHYT ngoại trú tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            else if (ckNoiTru.Checked && ckcngoaidm.Checked && ckDTNT.Checked)
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % BHYT nội trú tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            else
                                rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % viện phí BHYT tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            rep.TuNgayDenNgay.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " Đến ngày " + ngayden.ToString().Substring(0, 10);
                            rep.TongTien.Value = _TienBHYT.Sum(p => p.TongTien).ToString();
                            rep.colNguoiLapBieu.Text = NguoiLapBieu;

                            rep.DataSource = _TienBHYT.OrderBy(p => p.NgayTT);
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        if (HTNgoaiDM)
                        {
                            BaoCao.Rep_DsThThuTienBHYT_TY01_New rep = new BaoCao.Rep_DsThThuTienBHYT_TY01_New();
                            rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % viện phí BHYT tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            rep.TuNgayDenNgay.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " Đến ngày " + ngayden.ToString().Substring(0, 10);
                            rep.TongTien.Value = _TienBHYT.Sum(p => p.TongTien).ToString();
                            rep.DataSource = _TienBHYT.OrderBy(p => p.NgayTT);
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            BaoCao.Rep_DsThThuTienBHYT_TY01 rep = new BaoCao.Rep_DsThThuTienBHYT_TY01();
                            rep.TenBC.Value = ("Danh sách tổng hợp thu tiền % viện phí BHYT tháng " + ngayden.ToString().Substring(3, 2) + " năm " + ngayden.ToString().Substring(6, 4)).ToUpper();
                            rep.TuNgayDenNgay.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " Đến ngày " + ngayden.ToString().Substring(0, 10);
                            rep.TongTien.Value = _TienBHYT.Sum(p => p.TongTien).ToString();
                            rep.DataSource = _TienBHYT.OrderBy(p => p.NgayTT);
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }

                }
                else MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rad_Duyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30005")
            {
                if (rad_Duyet.SelectedIndex == 1)
                {
                    labelControl9.Text = "CB Duyệt:";
                }
                else
                {
                    labelControl9.Text = "CB Thu:";
                }
                if (rad_Duyet.SelectedIndex == 2)
                {
                    lupcbthu.EditValue = "";
                    lupcbthu.Enabled = false;
                }
                else
                    lupcbthu.Enabled = true;
            }
        }

        private void rdLoaiNgay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}