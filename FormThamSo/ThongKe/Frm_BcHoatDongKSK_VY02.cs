using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraEditors.Controls;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcHoatDongKSK_VY02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHoatDongKSK_VY02()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV == "30009")
            {
                rdMau.Properties.BeginUpdate();
                rdMau.Properties.Items.Add(new RadioGroupItem(2,"Mẫu 30009"));
                rdMau.Properties.EndUpdate();
            }
            
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
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private void Frm_BcHoatDongKSK_VY02_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public  class Baocao_KSK
        {
            private string _colTS11;

            public string ColTS11
            {
                get { return _colTS11; }
                set { _colTS11 = value; }
            }
            private string _colSLa11;

            public string ColSLa11
            {
                get { return _colSLa11; }
                set { _colSLa11 = value; }
            }
            private string _colSLb11;

            public string ColSLb11
            {
                get { return _colSLb11; }
                set { _colSLb11 = value; }
            }
            private string _colGTa11;

            public string ColGTa11
            {
                get { return _colGTa11; }
                set { _colGTa11 = value; }
            }
            private string _colGTb11;

            public string ColGTb11
            {
                get { return _colGTb11; }
                set { _colGTb11 = value; }
            }

            private string _colTS2;

            public string ColTS2
            {
                get { return _colTS2; }
                set { _colTS2 = value; }
            }
            private string _colTS3;

            public string ColTS3
            {
                get { return _colTS3; }
                set { _colTS3 = value; }
            }
            private string _colTS4;

            public string ColTS4
            {
                get { return _colTS4; }
                set { _colTS4 = value; }
            }
            private string _colTS5;

            public string ColTS5
            {
                get { return _colTS5; }
                set { _colTS5 = value; }
            }

            private string _colSLa2;

            public string ColSLa2
            {
                get { return _colSLa2; }
                set { _colSLa2 = value; }
            }
            private string _colSLa3;

            public string ColSLa3
            {
                get { return _colSLa3; }
                set { _colSLa3 = value; }
            }
            private string _colSLa4;

            public string ColSLa4
            {
                get { return _colSLa4; }
                set { _colSLa4 = value; }
            }
            private string _colSLa5;

            public string ColSLa5
            {
                get { return _colSLa5; }
                set { _colSLa5 = value; }
            }

            private string _colSLb2;

            public string ColSLb2
            {
                get { return _colSLb2; }
                set { _colSLb2 = value; }
            }
            private string _colSLb3;

            public string ColSLb3
            {
                get { return _colSLb3; }
                set { _colSLb3 = value; }
            }
            private string _colSLb4;

            public string ColSLb4
            {
                get { return _colSLb4; }
                set { _colSLb4 = value; }
            }
            private string _colSLb5;

            public string ColSLb5
            {
                get { return _colSLb5; }
                set { _colSLb5 = value; }
            }

            private string _colGTa2;

            public string ColGTa2
            {
                get { return _colGTa2; }
                set { _colGTa2 = value; }
            }
            private string _colGTa3;

            public string ColGTa3
            {
                get { return _colGTa3; }
                set { _colGTa3 = value; }
            }
            private string _colGTa4;

            public string ColGTa4
            {
                get { return _colGTa4; }
                set { _colGTa4 = value; }
            }
            private string _colGTa5;

            public string ColGTa5
            {
                get { return _colGTa5; }
                set { _colGTa5 = value; }
            }

            private string _colGTb2;

            public string ColGTb2
            {
                get { return _colGTb2; }
                set { _colGTb2 = value; }
            }
            private string _colGTb3;

            public string ColGTb3
            {
                get { return _colGTb3; }
                set { _colGTb3 = value; }
            }
            private string _colGTb4;

            public string ColGTb4
            {
                get { return _colGTb4; }
                set { _colGTb4 = value; }
            }
            private string _colGTb5;

            public string ColGTb5
            {
                get { return _colGTb5; }
                set { _colGTb5 = value; }
            }

        }




        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qksk = (from bn in data.BenhNhans
                        join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                        where (bn.NNhap >= ngaytu && bn.NNhap <= ngayden && bn.DTuong == ("KSK"))
                        select new
                        {
                            bn.MaBNhan,
                            bn.TenBNhan,
                            bn.NamSinh,
                            Ngay = bn.NNhap,
                            bn.DChi,
                            bn.TChung,
                            GTinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                            duSK = tu.KetLuan == 1 ? "X" : "",
                            KduSK = (tu.KetLuan == 0) ? "X" : ""

                        }).ToList();

            if (KTtaoBc())
            {
                if (qksk.Count > 0)
                {
                    #region mẫu 1
                    if (rdMau.SelectedIndex == 0)
                    {
                        var qkskp = qksk.Where(p => p.TChung.Equals("Cấp bằng lái xe máy") || p.TChung.Equals("Cấp bằng ô tô") || p.TChung.Equals("Khám sức khỏe định kỳ") || p.TChung.Equals("Khám sức khỏe(lái xe)")).ToList();
                        if (qkskp.Count > 0)
                        {
                            frmIn frm = new frmIn();
                            BaoCao.Rep_BcHoatDongKSK_VY02 rep = new BaoCao.Rep_BcHoatDongKSK_VY02();
                            rep.TuNgay.Value = lupTuNgay.Text;
                            rep.DenNgay.Value = lupDenNgay.Text;
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text;
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            BaoCao.rep_DanhsachKSKLaiXe_54 rep1 = new BaoCao.rep_DanhsachKSKLaiXe_54();
                            rep1.DataSource = qkskp;
                            rep1.databinding();
                            rep1.CreateDocument();
                            frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    #endregion
                    #region mẫu 2
                    else
                        if (rdMau.SelectedIndex == 1)
                        {
                            // tạm bỏ mục có kết quả dương tính với chất gây nghiện 
                            //KSK định kỳ
                            List<KSK> q1 = (from a in qksk.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ"))// || p.TChung.Contains("Khám sức khỏe(lái xe)"))
                                            select new KSK
                                      {
                                          Loai = 1,
                                          TenNhom = "Kết quả khám sức khỏe định kỳ cho người lái xe",
                                          tenDVi = a.DChi,
                                          tenBN = a.TenBNhan,
                                          maBN = a.MaBNhan,
                                          Gtinh = a.GTinh,
                                          Nsinh = a.NamSinh,
                                          NgayKham = a.Ngay,
                                          dudksk = a.duSK == "X" ? 1 : 0,
                                          Kdudksk = a.KduSK == "X" ? 1 : 0,
                                      }).ToList();

                            List<KSK> q2 = (from a in q1
                                            group a by new { a.Loai, a.TenNhom, a.tenDVi } into kq // || p.TChung.Contains("Khám sức khỏe(lái xe)"))
                                            select new KSK
                                            {
                                                Loai = kq.Key.Loai,
                                                STTNhom = "I",


                                                TenNhom = kq.Key.TenNhom,
                                                tenDVi = kq.Key.tenDVi,
                                                Tongso = kq.Count(),
                                                dudksk = kq.Sum(p => p.dudksk),
                                                Kdudksk = kq.Sum(p => p.Kdudksk),
                                            }).ToList();

                            //KSK cho người lái xe
                            List<KSK> q3 = (from a in qksk.Where(p => p.TChung.Contains("Khám sức khỏe(lái xe)"))
                                            select new KSK
                                            {
                                                Loai = 2,
                                                STTNhom = "II",
                                                TenNhom = "Kết quả khám sức khỏe cho người lái xe",
                                                tenDVi = a.TenBNhan,
                                                Gtinh = a.GTinh,
                                                Nsinh = a.NamSinh,
                                                NgayKham = a.Ngay,
                                                Dchi = a.DChi,
                                                maBN = a.MaBNhan,
                                                tenBN = a.TenBNhan,
                                                dudksk = a.duSK == "X" ? 1 : 0,
                                                Kdudksk = a.KduSK == "X" ? 1 : 0,
                                            }).ToList();

                            q2.AddRange(q3);
                            if (q2.Count > 0)
                            {
                                frmIn frm = new frmIn();
                                BaoCao.rep_BCKSK_LaiXe_Mau2_24009 rep = new BaoCao.rep_BCKSK_LaiXe_Mau2_24009();
                                rep.DataSource = q2.OrderBy(p => p.Loai).OrderBy(p => p.tenDVi);
                                if (txtTieuDeQuy.Text != "")
                                    rep.lblBaocaoQuy.Text = txtTieuDeQuy.Text.ToUpper();
                                rep.databinding();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();

                                q1.AddRange(q3);
                                var q4 = q1.Where(p => p.Kdudksk == 1).ToList();
                                if (q4.Count > 0)
                                {
                                    BaoCao.rep_KSKLaiXeKhongDat_Mau2_24009 rep2 = new BaoCao.rep_KSKLaiXeKhongDat_Mau2_24009();
                                    rep2.DataSource = q4.OrderBy(p => p.Loai).OrderBy(p => p.tenDVi);
                                    rep2.databinding();
                                    rep2.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep2.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            else MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    #endregion
                        #region mẫu 30009
                        else
                        {
                            if (rdMau.SelectedIndex == 2)
                            {
                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                DateTime tungay = System.DateTime.Now.Date;
                                DateTime denngay = System.DateTime.Now.Date;
                                DateTime bd = System.DateTime.Now.Date;
                                //tungay = Convert.ToDateTime(lupTuNgay.Text);
                                //denngay = Convert.ToDateTime(lupDenNgay.Text);
                                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                                string TuNgayDenNgay = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                                string Ngayxuat = "Ngày " + bd.Day + " tháng " + bd.Month + " năm " + bd.Year;
                                var qksk4 = (from bn in data.BenhNhans
                                             join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                                             where (tu.NgayThu >= tungay && tu.NgayThu <= denngay && bn.DTuong == ("KSK"))
                                             select new
                                             {
                                                 bn.MaBNhan,
                                                 bn.Tuoi,
                                                 bn.NNhap,
                                                 bn.DChi,
                                                 bn.TChung,
                                                 bn.GTinh,
                                                 tu.KetLuan,
                                                 tu.PhanLoai
                                             }).ToList();
                                List<Baocao_KSK> bcksk = new List<Baocao_KSK>();
                                Baocao_KSK bc30009 = new Baocao_KSK();

                                int ts = 0, dusk = 0, kdu = 0, nam = 0, nu = 0;
                                if (qksk4.Count > 0)
                                {


                                    #region 2
                                    if (qksk4.Where(p => p.TChung.Equals("Người lao động")).Count() > 0)
                                    {
                                        int a = qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(k => k.PhanLoai == 1).Select(p => p.MaBNhan).Count();
                                        bc30009.ColTS2 = a.ToString("##,###");
                                        ts = ts + a;
                                        if (qksk4.Where(p => p.KetLuan == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLa2 = b.ToString("##,###");
                                            dusk = dusk + b;
                                        }
                                        if (qksk4.Where(p => p.KetLuan == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLb2 = b.ToString("##,###");
                                            kdu = kdu + b;
                                        }
                                        if (qksk4.Where(p => p.TChung.Contains("Người lao động")).Where(p => p.GTinh == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTa2 = b.ToString("##,###");
                                            nam = nam + b;
                                        }

                                        if (qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(p => p.GTinh == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Người lao động")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTb2 = b.ToString("##,###");
                                            nu = nu + b;
                                        }
                                    }
                                    #endregion
                                    #region 3
                                    if (qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Count() > 0)
                                    {
                                        int a = qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(k => k.PhanLoai == 1).Select(p => p.MaBNhan).Count();
                                        bc30009.ColTS3 = a.ToString("##,###");
                                        ts = ts + a;
                                        if (qksk4.Where(p => p.KetLuan == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLa3 = b.ToString("##,###");
                                            dusk = dusk + b;
                                        }
                                        if (qksk4.Where(p => p.KetLuan == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLb3 = b.ToString("##,###");
                                            kdu = kdu + b;
                                        }
                                        if (qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(p => p.GTinh == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTa3 = b.ToString("##,###");
                                            nam = nam + b;
                                        }

                                        if (qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(p => p.GTinh == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Equals("Học sinh, sinh viên")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTb3 = b.ToString("##,###");
                                            nu = nu + b;
                                        }
                                    }
                                    #endregion
                                    #region 4
                                    if (qksk4.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Count() > 0)
                                    {
                                        int a = qksk4.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ")).Where(k => k.PhanLoai == 1).Select(p => p.MaBNhan).Count();
                                        bc30009.ColTS4 = a.ToString("##,###");
                                        ts = ts + a;
                                        if (qksk4.Where(p => p.KetLuan == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLa4 = b.ToString("##,###");
                                            dusk = dusk + b;
                                        }
                                        if (qksk4.Where(p => p.KetLuan == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLb4 = b.ToString("##,###");
                                            kdu = kdu + b;
                                        }
                                        if (qksk4.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTa4 = b.ToString("##,###");
                                            nam = nam + b;
                                        }

                                        if (qksk4.Where(p => p.TChung.Equals("Khám sức khỏe định kỳ")).Where(p => p.GTinh == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Khám sức khỏe định kỳ")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTb4 = b.ToString("##,###");
                                            nu = nu + b;
                                        }
                                    }
                                    #endregion
                                    #region 5
                                    if (qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Count() > 0)
                                    {
                                        int a = qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(k => k.PhanLoai == 1).Select(p => p.MaBNhan).Count();
                                        bc30009.ColTS5 = a.ToString("##,###");
                                        ts = ts + a;
                                        if (qksk4.Where(p => p.KetLuan == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLa5 = b.ToString("##,###");
                                            dusk = dusk + b;
                                        }
                                        if (qksk4.Where(p => p.KetLuan == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(k => k.PhanLoai == 1).Where(p => p.KetLuan == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColSLb5 = b.ToString("##,###");
                                            kdu = kdu + b;
                                        }
                                        if (qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(p => p.GTinh == 1).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 1).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTa5 = b.ToString("##,###");
                                            nam = nam + b;
                                        }

                                        if (qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(p => p.GTinh == 0).Count() > 0)
                                        {
                                            int b = qksk4.Where(p => p.TChung.Contains("Cấp bằng lái xe mới")).Where(k => k.PhanLoai == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Count();
                                            bc30009.ColGTb5 = b.ToString("##,###");
                                            nu = nu + b;
                                        }
                                    }
                                    #endregion

                                    bc30009.ColTS11 = ts.ToString();
                                    bc30009.ColSLa11 = dusk.ToString();
                                    bc30009.ColSLb11 = kdu.ToString();
                                    bc30009.ColGTa11 = nam.ToString();
                                    bc30009.ColGTb11 = nu.ToString();
                                    bcksk.Add(bc30009);
                                    Dictionary<string, object> dtksk = new Dictionary<string, object>();
                                    dtksk.Add("TuNgayDenNgay", TuNgayDenNgay);
                                    dtksk.Add("Ngayxuat", Ngayxuat);
                                    dtksk.Add("CQCQ", DungChung.Bien.TenCQCQ.ToUpper());
                                    dtksk.Add("TenCQ", DungChung.Bien.TenCQ.ToUpper());
                                    DungChung.Ham.Print(DungChung.PrintConfig.Rp_KSK_Thanhha, bcksk, dtksk, false);
                                }
                                else MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                        }
                        #endregion
                }
                else MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        class KSK
        {
            public int Loai { set; get; }
            public string STTNhom { set; get; }
            public string Gtinh  { set; get; }
            public string Nsinh { set; get; }
            public DateTime? NgayKham { set; get; }
            public string TenNhom { set; get; }
            public string tenDVi { set; get; }
            public int Tongso { set; get; }
            public int dudksk { set; get; }
            public int Kdudksk { set; get; }
            public int maBN { set; get; }
            public string tenBN { set; get; }
            public string Dchi { set; get; }
        }
    }
}