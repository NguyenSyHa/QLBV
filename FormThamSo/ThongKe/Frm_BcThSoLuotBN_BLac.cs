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
    public partial class Frm_BcThSoLuotBN_BLac : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcThSoLuotBN_BLac()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string _madt;
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
        private class MaDT
        {
            private string MaDTuong;
            private bool Chon;
            public string madtuong
            { set { MaDTuong = value; } get { return MaDTuong; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<MaDT> _lmadt = new List<MaDT>();
   
        private void Frm_BcThSoLuotBN_BLac_Load(object sender, EventArgs e)
        {

            _lmadt.Clear();
             lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var dtuong = (from bn in data.BenhNhans.Where(p=>p.DTuong=="BHYT") group bn by new {bn.MaDTuong,} into kq select new { kq.Key.MaDTuong }).ToList();
            if (dtuong.Count > 0)
            {
                MaDT themmoi1 = new MaDT();
                themmoi1.madtuong = "";
                themmoi1.chon = true;
                _lmadt.Add(themmoi1);
                foreach (var a in dtuong)
                {
                    MaDT themmoi = new MaDT();
                    themmoi.madtuong = a.MaDTuong.ToUpper();
                    themmoi.chon = true;
                    _lmadt.Add(themmoi);
                }
                grcDTuong.DataSource = _lmadt.ToList();
            }
           
        }
       
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<MaDT> _ldt = new List<MaDT>();
        
           frmIn frm = new frmIn();
           BaoCao.Rep_BcThSoLuotBN_BLac rep = new BaoCao.Rep_BcThSoLuotBN_BLac();
           #region Hiển thị thời gian
           int nam = Convert.ToInt32(tungay.Year);
           int thang = Convert.ToInt32(tungay.Month);
           if (radIn.SelectedIndex == 0)
           { rep.NgayThang.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text; }
           if (radIn.SelectedIndex == 1)
           {
               if (thang > 1 && thang <= 3) { rep.NgayThang.Value = "Quý I năm " + nam; }
               if (thang > 3 && thang <= 6) { rep.NgayThang.Value = "Quý II năm " + nam; }
               if (thang > 6 && thang <= 9) { rep.NgayThang.Value = "Quý III năm " + nam; }
               if (thang > 9 && thang <= 12) { rep.NgayThang.Value = "Quý IV năm " + nam; }
           }
           if (radIn.SelectedIndex == 2)
           {
               rep.NgayThang.Value = "Báo cáo 6 tháng/ năm " + nam;
           }
           if (radIn.SelectedIndex == 3)
           {
               rep.NgayThang.Value = "Báo cáo 9 tháng/ năm " + nam;
           }
           if (radIn.SelectedIndex == 4)
           { rep.NgayThang.Value = "Năm " + nam; }
           #endregion
      
           if (KTtaoBc())
            {
                _ldt = _lmadt.Where(p => p.chon == true).ToList();
                _ldt.Add(new MaDT { madtuong = ""});
                 int _bn1 = -1; int _bn2 = -1;
                {
                    if (radBN.SelectedIndex == 0) { _bn1 = 1; _bn2 = 0; rep.TenSo1.Value = ("I. số lượt nội trú và ngoại của toàn bộ bệnh nhân").ToUpper(); }
                    if (radBN.SelectedIndex == 1) { _bn1 = 1; _bn2 = -1; rep.TenSo1.Value =  ("I. số lượt nội trú của bệnh nhân").ToUpper(); }
                    if (radBN.SelectedIndex == 2) { _bn2 = 0; _bn1 = -1; rep.TenSo1.Value =  ("I.p số lượt ngoại trú của bệnh nhân").ToUpper(); }
                }
                string _dt1 = "", _dt2 = "";
                {
                    if (radDT.SelectedIndex == 0) { _dt1 = "BHYT"; _dt2 = "Dịch vụ"; rep.Gom.Value = "(Gồm cả người có thẻ và người không có thẻ BHYT)"; }
                    if (radDT.SelectedIndex == 1) { _dt1 = "BHYT"; _dt2 = ""; rep.Gom.Value = "(Bệnh nhân có thẻ BHYT)"; }
                    if (radDT.SelectedIndex == 2) { _dt1 = ""; _dt2 = "Dịch vụ"; rep.Gom.Value = "(Bệnh nhân không có thẻ BHYT)"; }
                }
         
                var q = (from vp in data.VienPhis
                           join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                           select new {vp.NgayTT,bn.MaBNhan, bn.DTuong,bn.NoiTru,bn.MaDTuong }).ToList();

                var qbn = (from bn in q.Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2).Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => p.DTuong == _dt1 || p.DTuong == _dt2)
                           group bn by new { bn.MaBNhan ,bn.NgayTT} into kq
                           select new
                           {
                               Thang=kq.Key.NgayTT.Value.Month,
                              // NoiTru = kq.Key.NoiTru,
                               BN = kq.Select(p => p.MaBNhan).Count(),
                           }).ToList();
                if (qbn.Count > 0)
                {
                    rep.BN11.Value = qbn.Where(p => p.Thang == 1).Sum(p => p.BN).ToString();
                    rep.BN12.Value = qbn.Where(p => p.Thang == 2).Sum(p => p.BN).ToString();
                    rep.BN13.Value = qbn.Where(p => p.Thang == 3).Sum(p => p.BN).ToString();
                    rep.BN14.Value = qbn.Where(p => p.Thang == 4).Sum(p => p.BN).ToString();
                    rep.BN15.Value = qbn.Where(p => p.Thang == 5).Sum(p => p.BN).ToString();
                    rep.BN16.Value = qbn.Where(p => p.Thang == 6).Sum(p => p.BN).ToString();
                    rep.BN17.Value = qbn.Where(p => p.Thang == 7).Sum(p => p.BN).ToString();
                    rep.BN18.Value = qbn.Where(p => p.Thang == 8).Sum(p => p.BN).ToString();
                    rep.BN19.Value = qbn.Where(p => p.Thang == 9).Sum(p => p.BN).ToString();
                    rep.BN110.Value = qbn.Where(p => p.Thang == 10).Sum(p => p.BN).ToString();
                    rep.BN111.Value = qbn.Where(p => p.Thang == 11).Sum(p => p.BN).ToString();
                    rep.BN112.Value = qbn.Where(p => p.Thang == 12).Sum(p => p.BN).ToString();
                    rep.BN1cn.Value = qbn.Sum(p => p.BN).ToString();
                }
               //DL phần II và III
                if (_madt != null && _madt != "")
                {
                    rep.TenSo2.Value = ("ii. số lượt nội trú bệnh nhân có thẻ bhyt " + _madt + " tại bệnh viện").ToUpper();
                    rep.TenSo3.Value = ("iii. số lượt ngoại trú bệnh nhân có thẻ bhyt " + _madt + " tại bệnh viện").ToUpper();
                    var qsl = (from bn in q.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p=>p.MaDTuong.ToUpper()==_madt)
                               group bn by new { bn.MaBNhan, bn.NgayTT,bn.NoiTru } into kq
                               select new
                               {
                                   NoiTru=kq.Key.NoiTru,
                                   Thang = kq.Key.NgayTT.Value.Month,
                                   BN = kq.Select(p => p.MaBNhan).Count(),
                               }).ToList();
                    if (qsl.Count > 0)
                    {
                        rep.BN21.Value = qsl.Where(p=>p.NoiTru==1).Where(p => p.Thang == 1).Sum(p => p.BN).ToString();
                        rep.BN22.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 2).Sum(p => p.BN).ToString();
                        rep.BN23.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 3).Sum(p => p.BN).ToString();
                        rep.BN24.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 4).Sum(p => p.BN).ToString();
                        rep.BN25.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 5).Sum(p => p.BN).ToString();
                        rep.BN26.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 6).Sum(p => p.BN).ToString();
                        rep.BN27.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 7).Sum(p => p.BN).ToString();
                        rep.BN28.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 8).Sum(p => p.BN).ToString();
                        rep.BN29.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 9).Sum(p => p.BN).ToString();
                        rep.BN210.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 10).Sum(p => p.BN).ToString();
                        rep.BN211.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 11).Sum(p => p.BN).ToString();
                        rep.BN212.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 12).Sum(p => p.BN).ToString();
                        rep.BN2cn.Value = qsl.Where(p => p.NoiTru == 1).Sum(p => p.BN).ToString();

                        rep.BN31.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 1).Sum(p => p.BN).ToString();
                        rep.BN32.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 2).Sum(p => p.BN).ToString();
                        rep.BN33.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 3).Sum(p => p.BN).ToString();
                        rep.BN34.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 4).Sum(p => p.BN).ToString();
                        rep.BN35.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 5).Sum(p => p.BN).ToString();
                        rep.BN36.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 6).Sum(p => p.BN).ToString();
                        rep.BN37.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 7).Sum(p => p.BN).ToString();
                        rep.BN38.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 8).Sum(p => p.BN).ToString();
                        rep.BN39.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 9).Sum(p => p.BN).ToString();
                        rep.BN310.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 10).Sum(p => p.BN).ToString();
                        rep.BN311.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 11).Sum(p => p.BN).ToString();
                        rep.BN312.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 12).Sum(p => p.BN).ToString();
                        rep.BN3cn.Value = qsl.Where(p => p.NoiTru == 0).Sum(p => p.BN).ToString();
                    }
                }
                var a = _lmadt.Where(p => p.chon == true).ToList();

                if (a.Count > 1)
                {
                    rep.TenSo2.Value = ("ii. số lượt nội trú bệnh nhân có thẻ bhyt tại bệnh viện").ToUpper();
                    rep.TenSo3.Value = ("iii. số lượt ngoại trú bệnh nhân có thẻ bhyt tại bệnh viện").ToUpper();
                  
                    var qsl = (from ma in _lmadt
                               join  bn in q.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on ma.madtuong equals bn.MaDTuong.ToUpper()
                               group bn by new { bn.MaBNhan, bn.NgayTT, bn.NoiTru } into kq
                               select new
                               {
                                   NoiTru = kq.Key.NoiTru,
                                   Thang = kq.Key.NgayTT.Value.Month,
                                   BN = kq.Select(p => p.MaBNhan).Count(),
                               }).ToList();
                    if (qsl.Count > 0)
                    {
                        rep.BN21.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 1).Sum(p => p.BN).ToString();
                        rep.BN22.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 2).Sum(p => p.BN).ToString();
                        rep.BN23.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 3).Sum(p => p.BN).ToString();
                        rep.BN24.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 4).Sum(p => p.BN).ToString();
                        rep.BN25.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 5).Sum(p => p.BN).ToString();
                        rep.BN26.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 6).Sum(p => p.BN).ToString();
                        rep.BN27.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 7).Sum(p => p.BN).ToString();
                        rep.BN28.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 8).Sum(p => p.BN).ToString();
                        rep.BN29.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 9).Sum(p => p.BN).ToString();
                        rep.BN210.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 10).Sum(p => p.BN).ToString();
                        rep.BN211.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 11).Sum(p => p.BN).ToString();
                        rep.BN212.Value = qsl.Where(p => p.NoiTru == 1).Where(p => p.Thang == 12).Sum(p => p.BN).ToString();
                        rep.BN2cn.Value = qsl.Where(p => p.NoiTru == 1).Sum(p => p.BN).ToString();

                        rep.BN31.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 1).Sum(p => p.BN).ToString();
                        rep.BN32.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 2).Sum(p => p.BN).ToString();
                        rep.BN33.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 3).Sum(p => p.BN).ToString();
                        rep.BN34.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 4).Sum(p => p.BN).ToString();
                        rep.BN35.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 5).Sum(p => p.BN).ToString();
                        rep.BN36.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 6).Sum(p => p.BN).ToString();
                        rep.BN37.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 7).Sum(p => p.BN).ToString();
                        rep.BN38.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 8).Sum(p => p.BN).ToString();
                        rep.BN39.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 9).Sum(p => p.BN).ToString();
                        rep.BN310.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 10).Sum(p => p.BN).ToString();
                        rep.BN311.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 11).Sum(p => p.BN).ToString();
                        rep.BN312.Value = qsl.Where(p => p.NoiTru == 0).Where(p => p.Thang == 12).Sum(p => p.BN).ToString();
                        rep.BN3cn.Value = qsl.Where(p => p.NoiTru == 0).Sum(p => p.BN).ToString();
                    }
                }
                rep.DiaDanh.Value = DungChung.Bien.DiaDanh + ", ngày ..... tháng ..... năm .....";
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                
            }
    
 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void grvDTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "Chọn")
            {
                if (grvDTuong.GetFocusedRowCellValue("madtuong") != null)
                {
                    string Madt = grvDTuong.GetFocusedRowCellValue("madtuong").ToString();

                    if (Madt == "")
                    {
                        if (_lmadt.First().chon == true)
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = true;
                            }
                        }
                        grcDTuong.DataSource = "";
                        grcDTuong.DataSource = _lmadt.ToList();
                       
                        //int so = grvDTuong.RowCount;

                        //if (so == 1)
                        //{
                        //    _madt = grvDTuong.GetFocusedRowCellValue("madtuong").ToString();
                        //}
         
                    }
                }
            }
        }

        private void grvDTuong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int soma = 0;
            if (e.Column.Name == "Chọn")
            {
                for (int i = 0; i < grvDTuong.RowCount; i++)
                {
                    if (grvDTuong.GetRowCellValue(i, Chọn).ToString() == "true" || grvDTuong.GetRowCellValue(i, Chọn).ToString() == "True")
                    {
                        soma++;
                    }
                }
                if (soma > 1 || soma == grvDTuong.RowCount)
                {
                    MessageBox.Show("Bạn chỉ chọn 1 hoặc tất cả mã đối tượng ");
                }
            }
            if (soma == 1)
            {
                _madt = grvDTuong.GetFocusedRowCellValue("madtuong").ToString();
            }
        }

      
    }
}