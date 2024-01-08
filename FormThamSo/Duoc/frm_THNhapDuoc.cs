using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_THNhapDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_THNhapDuoc()
        {
            InitializeComponent();
        }
        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            int _kho = -1;
            if (lupKho.EditValue != null)
                _kho =Convert.ToInt32( lupKho.EditValue);
            var lcc = _lNhaCC.Where(p => p.chon == true && p.tenkp != "Chọn tất cả").Distinct().ToList();
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
            int KieuDon = 4;
            KieuDon = cboNhap.SelectedIndex;
            int idtn = 0;
            if(lupNhomThuoc.EditValue!=null)
            {
                idtn = Convert.ToInt32(lupNhomThuoc.EditValue);
            }
            var _ldv = (from tn in data.TieuNhomDVs.Where(p => idtn == 0 ? true : p.IDNhom == idtn)
                        join dv in data.DichVus.Where(p => p.PLoai == 1) on tn.IdTieuNhom equals dv.IdTieuNhom
                        select dv).ToList();
            if (KTtaoBcNXT())
            {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                frmIn frm = new frmIn();
                BaoCao.rep_BKNhapKho rep = new BaoCao.rep_BKNhapKho(checkEdit1.Checked, chkInTH.Checked);
                rep.Ngay.Value = "Từ ngày:" + tungay.ToString().Substring(0, 10) + " đến ngày: " + denngay.ToString().Substring(0, 10);

                List<NhaCC> _lnhacc = data.NhaCCs.ToList();
                List<KPhong> _lkp = data.KPhongs.ToList();
                var q = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => _kho == 0 || p.MaKP == (_kho)).Where(p => p.PLoai == 1)
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join kp in data.KPhongs on nd.MaKPnx equals kp.MaKP
                         into k
                         from k1 in k.DefaultIfEmpty()
                         where (KieuDon == 4 ? true : nd.KieuDon == KieuDon)
                         select new {nd.MaKPnx, nd.IDNhap, nd.NgayNhap, nd.SoCT, ndct.MaDV, ndct.SoLuongN, ndct.ThanhTienN, ndct.DonGia, nd.MaCC, nd.KieuDon, TenKP1 = k1 != null ? k1.TenKP : "" }).OrderBy(p => p.NgayNhap).ToList();// dv.DonVi, dv.TenDV,
                if (chkInTH.Checked)
                {
                    if (KieuDon != 2 && KieuDon != 0)
                    {
                        var q1 = (from nd in q
                                  join ncc in lcc on nd.MaCC equals ncc.makp
                                  group nd by new { ncc.tenkp} into k
                                  select new { ThanhTienN = k.Sum(p => p.ThanhTienN), TenCC = k.Key.tenkp}).ToList();
                        rep.DataSource = q1.OrderBy(p => p.TenCC).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        var q1 = (from nd in q
                                  join kp in data.KPhongs on nd.MaKPnx equals kp.MaKP
                                  group nd by new { kp.TenKP } into k
                                  select new { ThanhTienN = k.Sum(p => p.ThanhTienN), TenCC = k.Key.TenKP }).ToList();
                        rep.DataSource = q1.OrderBy(p => p.TenCC).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else 
                {
                    if (KieuDon != 2)
                    {
                        var q1 = (from nd in q
                                  join dv in _ldv on nd.MaDV equals dv.MaDV
                                  join ncc in lcc on nd.MaCC equals ncc.makp
                                  select new { nd.IDNhap, nd.NgayNhap, nd.SoCT, nd.MaDV, nd.SoLuongN, nd.ThanhTienN, nd.DonGia, TenCC = (nd.KieuDon == 2) ? nd.TenKP1 : (ncc.tenkp), dv.TenDV, dv.DonVi, ncc.makp }).ToList();//TenCC = (nd.KieuDon == 2) ? (k1 != null ? k1.TenKP : "") : (kq1 == null ? "" : kq1.TenCC),

                        rep.DataSource = q1.OrderBy(p => p.NgayNhap).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        var q2 = (from nd in q
                                  join dv in _ldv on nd.MaDV equals dv.MaDV
                                  select new { nd.IDNhap, nd.NgayNhap, nd.SoCT, nd.MaDV, nd.SoLuongN, nd.ThanhTienN, nd.DonGia, TenCC = nd.TenKP1, dv.TenDV, dv.DonVi }).ToList();//TenCC = (nd.KieuDon == 2) ? (k1 != null ? k1.TenKP : "") : (kq1 == null ? "" : kq1.TenCC),

                        rep.DataSource = q2.OrderBy(p => p.NgayNhap).ToList();

                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            } 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data;
        private class KPhongc
        {
            private string TenKP;
            private string MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }


        }
        List<KPhongc> _lNhaCC = new List<KPhongc>();
        List<NhaCC> _lcc = new List<NhaCC>();
        private void frmTsBcNXTXuat_Load(object sender, EventArgs e)
        {
           
            cboNhap.SelectedIndex = 4;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> q = data.KPhongs.Where(p => p.PLoai== ("Khoa dược")).ToList();
            q.Insert(0,new KPhong { MaKP = 0, TenKP = "Tất cả" });

            lupKho.Properties.DataSource = q.ToList();
            List<NhaCC> qcc = data.NhaCCs.ToList();
            if (qcc.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = "";
                themmoi1.chon = true;
                _lNhaCC.Add(themmoi1);
                foreach (var a in qcc)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenCC;
                    themmoi.makp = a.MaCC;
                    themmoi.chon = true;
                    _lNhaCC.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lNhaCC.ToList();
            }

            var _ltieuNhom = data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 7 || (p.TenNhomCT != null && p.TenNhomCT.ToLower().Contains("hóa chất"))).Select(p => new { IdTieuNhom = p.IDNhom, TenTN = p.TenNhomCT }).ToList();
            _ltieuNhom.Add(new { IdTieuNhom = 0, TenTN = "Tất cả" });
            lupNhomThuoc.Properties.DataSource = _ltieuNhom.OrderBy(p => p.IdTieuNhom).ToList();
            lupNhomThuoc.EditValue = 0;
            //lupNhaCC.Properties.DataSource = qcc.ToList();
            //lupNhaCC.EditValue = "0";
            lupKho.EditValue = 0;
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var nd = data.NhapDs.Where(p => p.PLoai == 1).ToList();
            int i = 0;
            foreach (var item in nd)
            {
                if (item.XuatTD == 2 )
                {
                    if (item.KieuDon != 1)
                    {
                        item.KieuDon = 1;
                        i++;
                    }
                }
                else
                {
                    if (item.XuatTD > 2)
                    {
                        if (item.KieuDon != 0)
                        {
                            item.KieuDon = 0;
                            i++;
                        }
                    }
                    else
                    {
                        if (item.KieuDon == null)
                        {
                            item.KieuDon = 3;
                            i++;
                        }

                    }
                }
               

            }
            if (i > 0)
            {
                data.SaveChanges();

            }
            MessageBox.Show("update thành công: " + i + " chứng từ");
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lNhaCC.First().chon == true)
                        {
                            foreach (var a in _lNhaCC)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lNhaCC)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lNhaCC.ToList();
                    }
                }
            }
        }
    }
}