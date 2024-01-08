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
    public partial class frm_BCChiDinhMienDich : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCChiDinhMienDich()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCChiDinhMienDich_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DungChung.Ham.NgayTu( DateTime.Now);
            lupDenNgay.DateTime = DungChung.Ham.NgayDen( DateTime.Now);
            var ltieunhom = data.TieuNhomDVs.Where(p => p.IDNhom == 1).Select(p => new { p.TenRG}).Distinct().ToList();
            lup_NhomXN.Properties.DisplayMember = "TenRG";
            lup_NhomXN.Properties.ValueMember = "TenRG";
            lup_NhomXN.Properties.DataSource = ltieunhom;

            List<DTBN> ldtbn = new List<DTBN>();
            ldtbn = data.DTBNs.Where(p => p.Status == 1).ToList();
            ldtbn.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cboDTuong.Properties.DataSource = ldtbn;
            cboDTuong.EditValue = cboDTuong.Properties.GetKeyValueByDisplayText("Tất cả");
            radioGroup1.SelectedIndex = 2;
            List<DichVu> qdv = (from tnhom in data.TieuNhomDVs.Where(p => p.TenTN.ToLower() == "xét nghiệm miễn dịch")
                                join dv in data.DichVus on tnhom.IdTieuNhom equals dv.IdTieuNhom
                                select dv).ToList();
            ckl_DichVu.DisplayMember = "TenDV";
            ckl_DichVu.ValueMember = "MaDV";
            ckl_DichVu.DataSource = qdv;
            qdv.Insert(0, new DichVu { MaDV = 0, TenDV = "Tất cả" });
            ckl_DichVu.UnCheckAll();
        }

        private void lup_NhomXN_EditValueChanged(object sender, EventArgs e)
        {
            string tn = "";
            if(lup_NhomXN.EditValue != null)
            {
                tn = lup_NhomXN.EditValue.ToString();
            }
            if(tn == "")
            {
                ckl_DichVu.DataSource = null;
            }
            else
            {
                List<DichVu> qdv = (from tnhom in data.TieuNhomDVs.Where(p => p.TenRG == tn)
                           join dv in data.DichVus on tnhom.IdTieuNhom equals dv.IdTieuNhom
                           select  dv).ToList();
                ckl_DichVu.DisplayMember = "TenDV";
                ckl_DichVu.ValueMember = "MaDV";
                ckl_DichVu.DataSource = qdv;
                qdv.Insert(0, new DichVu{MaDV = 0, TenDV = "Tất cả"});
                ckl_DichVu.UnCheckAll();
            }
        }

        private void ckl_DichVu_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                {
                    ckl_DichVu.CheckAll();
                }
                else
                    ckl_DichVu.UnCheckAll();
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = lupTuNgay.DateTime;
            DateTime denngay = lupDenNgay.DateTime;
            int dtbn = 99;
            if (cboDTuong.EditValue != null)
                dtbn = Convert.ToInt32(cboDTuong.EditValue);
            int noitru = radioGroup1.SelectedIndex;
            List<int> _lmadvChon = new List<int>();
            
            int count = 0;
            for (int i = 1; i < ckl_DichVu.ItemCount; i++)
            {

                if (ckl_DichVu.GetItemCheckState(i) == CheckState.Checked)
                {
                    _lmadvChon.Add(Convert.ToInt32(ckl_DichVu.GetItemValue(i)));
                    count++;
                }
            }
            if (count > 14 && rgChonMau.SelectedIndex == 0)
            {
                MessageBox.Show("Bạn đã chọn nhiều hơn 14 dịch vụ để lên báo cáo. Có một số dịch vụ sẽ không được hiển thị");
            }

            var qdv = (from dv in data.DichVus.Where(p => _lmadvChon.Contains(p.MaDV))
                       select new { dv.TenDV, dv.MaDV, dv.SoTT, TenUp = dv.TenDV.ToUpper() }).OrderBy(p => p.SoTT).ToList();

            var qcls = (from cls in data.CLS.Where(p => p.NgayThang >= tungay && p.NgayThang <= denngay)
                        join cd in data.ChiDinhs.Where(p => _lmadvChon.Contains(p.MaDV ?? 0)) on cls.IdCLS equals cd.IdCLS
                        join bn in data.BenhNhans.Where(p => (dtbn == 99 ? true : p.IDDTBN == dtbn) && (noitru == 2 ? true : p.NoiTru == noitru)) on cls.MaBNhan equals bn.MaBNhan
                        select new { cls.NgayThang, cd.MaDV, cd.DonGia, bn.TenBNhan, bn.MaBNhan, cls.MaCB, cd.IDCD, bn.DTuong }).ToList();
            var qcb = data.CanBoes.ToList();
            List<BC> _ltenDV = new List<BC>();
            _ltenDV = (from dv in qdv
                       select new BC
                       {
                           MaDV = dv.MaDV,
                           SoTT = dv.SoTT,
                           TenDV = dv.TenDV,
                           ten = dv.TenUp.Contains("BHCG") ? "BETA HCG" : (dv.TenUp == "T3" ? "T3" : (dv.TenUp == "FT3" ? "FT3" : (dv.TenUp == "FT4" ? "FT4" : (dv.TenUp.Contains("TSH") ? "TSH" : (dv.TenUp.Contains("CEA") ? "CEA" : (dv.TenUp.Contains("AFP") ? "AFP" : (dv.TenUp.Contains("TnI3") ? "TnI3" : (dv.TenUp.Contains("125") ? "CA - 125" : (dv.TenUp.Contains("CA 15 - 3") ? "CA 15-3" : (dv.TenUp.Contains("CA 19 - 9") ? "CA 199" : (dv.TenUp.Contains("PSA") ? "PSA" : (dv.TenUp.Contains("SCC") ? "SCC" : (dv.TenUp.Contains("XÉT NGHIỆM TG") ? "TG" : dv.TenDV)))))))))))))
                       }).ToList();
            if (rgChonMau.SelectedIndex == 0)
            {
                List<string> _lTendv = _ltenDV.Select(p => p.ten).Distinct().ToList();
                List<BC> _lBC = new List<BC>();
                int num = 0;
                BaoCao.rep_BCChiDinhMienDich rep = new BaoCao.rep_BCChiDinhMienDich();
                foreach (string a in _lTendv)
                {
                    num++;
                    if (num < 15)
                    {
                        List<int> lmadv = (from dv in _ltenDV.Where(p => p.ten == a) select dv.MaDV).ToList();
                        var qcls1 = (from cls in qcls
                                     join dv in lmadv on cls.MaDV equals dv
                                     select new BC
                                         {
                                             Ngay = cls.NgayThang.Value.Date,
                                             TenBNhan = cls.TenBNhan,
                                             MaBNhan = cls.MaBNhan,
                                             TenBS = cls.MaCB,//DungChung.Ham._getTenCB(data, cls.MaCB.Trim()),
                                             ten = a,
                                             ThanhTien = cls.DonGia,
                                             SoTT = num
                                         }).ToList();
                        _lBC.AddRange(qcls1);
                        if (num == 1)
                            rep.tit1.Text = a;
                        else if (num == 2)
                            rep.tit2.Text = a;
                        else if (num == 3)
                            rep.tit3.Text = a;
                        else if (num == 4)
                            rep.tit4.Text = a;
                        else if (num == 5)
                            rep.tit5.Text = a;
                        else if (num == 6)
                            rep.tit6.Text = a;
                        else if (num == 7)
                            rep.tit7.Text = a;
                        else if (num == 8)
                            rep.tit8.Text = a;
                        else if (num == 9)
                            rep.tit9.Text = a;
                        else if (num == 10)
                            rep.tit10.Text = a;
                        else if (num == 11)
                            rep.tit11.Text = a;
                        else if (num == 12)
                            rep.tit12.Text = a;
                        else if (num == 13)
                            rep.tit13.Text = a;
                        else if (num == 14)
                            rep.tit14.Text = a;
                    }
                }

                _lBC = (from bc in _lBC
                        group bc by new { bc.Ngay, bc.TenBNhan, bc.MaBNhan, bc.TenBS } into kq
                        select new BC
                        {
                            Ngay = kq.Key.Ngay,
                            ID = kq.Key.MaBNhan,
                            TenBNhan = kq.Key.TenBNhan,
                            TenBS = DungChung.Ham._getTenCB(data, kq.Key.TenBS),
                            ThanhTien = kq.Sum(p => p.ThanhTien),
                            col1 = kq.Where(p => p.SoTT == 1).Count(),
                            col2 = kq.Where(p => p.SoTT == 2).Count(),
                            col3 = kq.Where(p => p.SoTT == 3).Count(),
                            col4 = kq.Where(p => p.SoTT == 4).Count(),
                            col5 = kq.Where(p => p.SoTT == 5).Count(),
                            col6 = kq.Where(p => p.SoTT == 6).Count(),
                            col7 = kq.Where(p => p.SoTT == 7).Count(),
                            col8 = kq.Where(p => p.SoTT == 8).Count(),
                            col9 = kq.Where(p => p.SoTT == 9).Count(),
                            col10 = kq.Where(p => p.SoTT == 10).Count(),
                            col11 = kq.Where(p => p.SoTT == 11).Count(),
                            col12 = kq.Where(p => p.SoTT == 12).Count(),
                            col13 = kq.Where(p => p.SoTT == 13).Count(),
                            col14 = kq.Where(p => p.SoTT == 14).Count()
                        }).ToList();

                frmIn frm = new frmIn();
                rep.DataSource = _lBC.OrderBy(p => p.Ngay).ThenBy(p => p.ID).ThenBy(p => p.TenBNhan).ToList();
                rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var kqq = (from a in qcls
                           join dv in _ltenDV on a.MaDV equals dv.MaDV
                           group new { a, dv } by new { a.MaDV, dv.ten } into kq
                           select new
                           {
                               kq.Key.MaDV,
                               kq.Key.ten,
                               SoLuongBH = kq.Where(p => p.a.DTuong == "BHYT").Select(p => p.a.IDCD).Distinct().Count(),
                               SoLuongDV = kq.Where(p => p.a.DTuong == "Dịch vụ").Select(p => p.a.IDCD).Distinct().Count()
                           }).OrderBy(p => p.ten).ToList();

                BaoCao.Rep_BCChiDinhMienDichSoLuong rep = new BaoCao.Rep_BCChiDinhMienDichSoLuong();
                frmIn frm = new frmIn();
                rep.DataSource = kqq;
                rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        class BC
        {
            public DateTime? Ngay { set; get; }
            public int ID { set; get; }
            public string TenBNhan { set; get; }
            public string TenBS { set; get; }
            public int? col1 { set; get; }
            public int? col2 { set; get; }
            public int? col3 { set; get; }
            public int? col4 { set; get; }
            public int? col5 { set; get; }
            public int? col6 { set; get; }
            public int? col7 { set; get; }
            public int? col8 { set; get; }
            public int? col9 { set; get; }
            public int? col10 { set; get; }
            public int? col11 { set; get; }
            public int? col12 { set; get; }
            public int? col13 { set; get; }
            public int? col14 { set; get; }           
            public double? ThanhTien { set; get; }
            public string ten { set; get; }
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public int MaBNhan { get; set; }
            public int? SoTT { get; set; }
        }
    }
}