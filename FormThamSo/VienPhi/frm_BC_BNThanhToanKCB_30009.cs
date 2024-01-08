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
    public partial class frm_BC_BNThanhToanKCB_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_BNThanhToanKCB_30009()
        {
            InitializeComponent();
        }

        #region class TieuNhom
        public class KPCLS
        {
            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
        }
        #endregion
        List<KPCLS> _lKPCLS = new List<KPCLS>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BC_BNThanhToanKCB_30009_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;

            var tieunhom = (from kp in data.KPhongs.Where(p => p.PLoai == "Cận lâm sàng")
                            select new { kp.TenKP, kp.MaKP }).Distinct().ToList();
            if (tieunhom.Count > 0)
            {
                KPCLS themmoi1 = new KPCLS();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKPCLS.Add(themmoi1);
                foreach (var a in tieunhom)
                {
                    KPCLS themmoi = new KPCLS();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKPCLS.Add(themmoi);
                }
                grcTieuNhom.DataSource = _lKPCLS.OrderBy(p => p.MaKP).ToList();
            }
        }

        private void grvTieuNhom_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvTieuNhom.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvTieuNhom.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKPCLS.First().Chon == true)
                        {
                            foreach (var a in _lKPCLS)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKPCLS)
                            {
                                a.Chon = true;
                            }
                        }
                        grcTieuNhom.DataSource = "";
                        grcTieuNhom.DataSource = _lKPCLS.OrderBy(p => p.MaKP).ToList();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KTtaoBc()
        {

            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dtTuNgay.Focus();
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                dtDenNgay.Focus();
                return false;
            }
            else return true;
        }

        List<BNTTKCB> _lBN = new List<BNTTKCB>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            _lBN.Clear();

            List<KPCLS> dsKP = new List<KPCLS>();
            dsKP = _lKPCLS.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();
            string tnhom = string.Empty;
            if (dsKP.Count > 0)
            {
                foreach (var item in dsKP)
                {
                    tnhom += item.TenKP + ";";
                }
                tnhom = tnhom.Remove(tnhom.Length - 1, 1);
            }
            List<int> _lMaKP = _lKPCLS.Where(p => p.Chon == true).Select(p=>p.MaKP).ToList();
            string dtuong = "";
            if (radDTuong.SelectedIndex == 0)
            {
                dtuong = "Dịch vụ";
            }
            if (radDTuong.SelectedIndex == 1)
            {
                dtuong = "BHYT";
            }
            if (radDTuong.SelectedIndex == 2)
            {
                dtuong = "";
            }
            int nt = 2;
            if (rdgNNgTru.SelectedIndex == 0)//ngoại trú
            {
                nt = 0;
            }
            if (rdgNNgTru.SelectedIndex == 1)//nội trú
            {
                nt = 1;
            }
            if (rdgNNgTru.SelectedIndex == 2)//tất cả
            {
                nt = 2;
            }
            if (KTtaoBc())
            {
            
                var qcls = (from dv in data.DichVus.Where(p => p.PLoai == 2).Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3)
                            join cd in data.ChiDinhs//.Where(p => p.Status == 1)  
                            on dv.MaDV equals cd.MaDV
                            join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                            join cb in data.CanBoes on cls.MaCB equals cb.MaCB
                            select new { cls.MaBNhan, cls.MaKPth, cb.TenCB, cd.MaDV, dv.TenDV }).ToList();
                var q0 = (from cls in qcls
                          group new { cls } by new { cls.MaBNhan, cls.TenDV, cls.MaDV, cls.MaKPth } into kq
                          select new { kq.Key.MaBNhan, kq.Key.MaKPth, TenCB = kq.First().cls.TenCB, kq.Key.TenDV, kq.Key.MaDV }).Distinct().ToList();

                //var q1 = (from k in dsKP
                //          join c in q0 on k.MaKP equals c.MaKPth
                //          group new { c } by new { c.MaBNhan, c.TenDV, c.MaDV } into kq
                //          select new { kq.Key.MaBNhan, TenCB = kq.First().c.TenCB, kq.Key.TenDV, kq.Key.MaDV }).ToList();
              
                //var q2 = (from a in q1
                //          join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt).Where(p => dtuong == "" || p.DTuong == dtuong) on a.MaBNhan equals bn.MaBNhan
                //          join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                //          select new { a.MaBNhan, a.TenCB, a.TenDV, MaDV = a.MaDV ?? 0, bn.TenBNhan, bn.DChi }).ToList();

                var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                           join vpct in data.VienPhicts.Where(p => rdgLoaiIn.SelectedIndex == 0 ? p.ThanhToan == 1 : (rdgLoaiIn.SelectedIndex == 1 ? p.ThanhToan == 0 : true)) on vp.idVPhi equals vpct.idVPhi
                           join dv in data.DichVus.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3) on vpct.MaDV equals dv.MaDV
                           select new { vp.MaBNhan, vpct.MaDV, SoTien = vpct.ThanhTien, dv.TenDV }).ToList();
                #region tạm bỏ do dữ liệu lấy từ CLS không khớp được với bên viện phí
                //List<BNTTKCB> _listThanhToan = (from bn in q2
                //                                join vp in qvp on new { bn.MaBNhan, bn.MaDV } equals new { vp.MaBNhan,MaDV= vp.MaDV??0 }
                //                                select new BNTTKCB { MaBNhan = bn.MaBNhan, TenCB = bn.TenCB, TenDV = bn.TenDV, ThanhTien = vp.SoTien, TenBNhan = bn.TenBNhan, DChi = bn.DChi }).OrderBy(p=>p.MaBNhan).ThenBy(p=>p.TenDV).ToList();
                #endregion
                List<BNTTKCB> _listThanhToan = (from vp in qvp
                                                join bn in q0 on new { vp.MaBNhan, MaDV = vp.MaDV ?? 0 } equals new { bn.MaBNhan, MaDV = bn.MaDV ?? 0 } into kq
                                                from kq1 in kq.DefaultIfEmpty()
                                                select new BNTTKCB { MaBNhan = vp.MaBNhan, MaKPth =  kq1 == null ? 0 : kq1.MaKPth.Value, TenCB = kq1 == null ? "" : kq1.TenCB, TenDV = vp.TenDV, ThanhTien = vp.SoTien }).ToList();//.OrderBy(p => p.MaBNhan).ThenBy(p => p.TenDV).ToList();
                _listThanhToan = (from vp in _listThanhToan.Where(p=> _lMaKP.Contains(0) || _lMaKP.Contains(p.MaKPth))                                  
                                  join bn in data.BenhNhans.Where(p => nt == 2 || p.NoiTru == nt).Where(p => dtuong == "" || p.DTuong == dtuong) on vp.MaBNhan equals bn.MaBNhan
                                  join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                  select new BNTTKCB { MaBNhan = bn.MaBNhan, TenCB = vp.TenCB, TenDV = vp.TenDV, ThanhTien = vp.ThanhTien, TenBNhan = bn.TenBNhan, DChi = bn.DChi }).OrderBy(p => p.MaBNhan).ThenBy(p => p.TenDV).ToList();
              


                double tongtien = _listThanhToan.Sum(p => p.ThanhTien);

                BaoCao.Rep_BC_BNThanhToanKCB_30009 rep = new BaoCao.Rep_BC_BNThanhToanKCB_30009();
                frmIn frm = new frmIn();
                rep.lblTuNgayDenNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + "\nKhoa: " + tnhom;
                rep.lblTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng chẵn.");
                rep.txtNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.DataSource = _listThanhToan.OrderBy(p => p.TenBNhan).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        #region class BNTTKCB
        private class BNTTKCB
        {
            public int? MaBNhan { get; set; }
            public string TenBNhan { get; set; }
            public string DChi { get; set; }
            public string TenDV { get; set; }
            public double ThanhTien { get; set; }
            public string TenCB { get; set; }

            public int MaKPth { get; set; }
        }
        #endregion
    }
}