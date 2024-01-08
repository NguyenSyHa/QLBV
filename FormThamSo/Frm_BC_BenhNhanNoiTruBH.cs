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
    public partial class Frm_BC_BenhNhanNoiTruBH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BC_BenhNhanNoiTruBH()
        {
            InitializeComponent();
        }

        private void Frm_BC_BenhNhanNoiTruBH_Load(object sender, EventArgs e)
        {
            txtTungay.Text = DateTime.Now.ToString();
            txtDenngay.Text = DateTime.Now.ToString();
            
        }
        public class bnTonNoiTru
        {
            private int _bnDauKi;

            public int BnDauKi
            {
                get { return _bnDauKi; }
                set { _bnDauKi = value; }
            }
            private int _bnVaoVienTrongKi;

            public int BnVaoVienTrongKi
            {
                get { return _bnVaoVienTrongKi; }
                set { _bnVaoVienTrongKi = value; }
            }
            private int _bnRaVienTrongKi;

            public int BnRaVienTrongKi
            {
                get { return _bnRaVienTrongKi; }
                set { _bnRaVienTrongKi = value; }
            }
            private string _bh;

            public string Bh
            {
                get { return _bh; }
                set { _bh = value; }
            }
            //private string _dv;

            //public string Dv
            //{
            //    get { return _dv; }
            //    set { _dv = value; }
            //}
            //private int _bnDauKiDV;

            //public int BnDauKiDV
            //{
            //    get { return _bnDauKiDV; }
            //    set { _bnDauKiDV = value; }
            //}
            //private int _bnVaoVienTrongKiDV;

            //public int BnVaoVienTrongKiDV
            //{
            //    get { return _bnVaoVienTrongKiDV; }
            //    set { _bnVaoVienTrongKiDV = value; }
            //}
            //private int _bnRaVienTrongKiDV;

            //public int BnRaVienTrongKiDV
            //{
            //    get { return _bnRaVienTrongKiDV; }
            //    set { _bnRaVienTrongKiDV = value; }
            //}
            private int _tonNT;

            public int TonNT
            {
                get { return _tonNT; }
                set { _tonNT = value; }
            }
            //private int _tonNTDV;

            //public int TonNTDV
            //{
            //    get { return _tonNTDV; }
            //    set { _tonNTDV = value; }
            //}
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        
        private void BtnReport_Click(object sender, EventArgs e)
        {
            
            DateTime tungay = Convert.ToDateTime(txtTungay.Text);
            DateTime denngay = Convert.ToDateTime(txtDenngay.Text);

            Dictionary<String, Object> dic = new Dictionary<string, object>();
            bnTonNoiTru _bntonNTBH = new bnTonNoiTru();
            bnTonNoiTru _bntonNTDV = new bnTonNoiTru();
            List<bnTonNoiTru> _listBnTonNT = new List<bnTonNoiTru>();
            if(tungay > denngay)
            {
                MessageBox.Show("Bạn nhập khoảng thời gian chưa chính xác, vui lòng nhập lại");
            }
            else
            {
                if(chkBHYT.Checked == false && chkDichvu.Checked == false)
                {
                    MessageBox.Show("Bạn chưa chọn đối tượng bệnh nhân (BHYT hoặc Dịch vụ)");
                }
                else
                {
                    string s = "Từ " + tungay.Hour.ToString() + " giờ " + tungay.Minute.ToString() + " phút, ngày " + tungay.Day.ToString() + " tháng " + tungay.Month.ToString() + " năm " + tungay.Year.ToString() + ", đến " + denngay.Hour.ToString() + " giờ " + denngay.Minute.ToString() + " phút, ngày " + denngay.Day.ToString() + " tháng " + denngay.Month.ToString() + " năm " + denngay.Year.ToString();
                    string signdate = "........, ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " năm " + DateTime.Now.Year.ToString();
                    //SELECT COUNT(BenhNhan.MaBNhan) AS Expr1
                    //FROM BenhNhan INNER JOIN
                    //VaoVien ON BenhNhan.MaBNhan = VaoVien.MaBNhan
                    //WHERE (BenhNhan.NoiTru = 1) AND (BenhNhan.DTuong = N'bhyt') AND (VaoVien.NgayVao < CONVERT(DATETIME, '2019-10-16 17:00:00', 102))

                    //-----------------------------------------------------------------------------------

                    //SELECT COUNT(BenhNhan.MaBNhan) AS Expr1
                    //FROM BenhNhan INNER JOIN
                    //VaoVien ON BenhNhan.MaBNhan = VaoVien.MaBNhan INNER JOIN
                    //RaVien ON BenhNhan.MaBNhan = RaVien.MaBNhan
                    //WHERE (BenhNhan.NoiTru = 1) AND (BenhNhan.DTuong = N'bhyt') AND (RaVien.NgayRa < CONVERT(DATETIME, '2019-10-16 17:00:00', 102))

                    #region Bệnh nhân vào viện đầu kì tính từ "từ ngày" trở về trước
                    var vaovien = (from bn in _data.BenhNhans.Where(p => p.NoiTru == 1).Where(x => x.DTuong == "BHYT" || x.DTuong == "Dịch vụ")
                                   join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                   select new { vv.MaBNhan, bn.DTuong,vv.NgayVao }).ToList();
                    #endregion

                    #region Bệnh nhân ra viện đầu kì tính từ "từ ngày" trở về trước
                    var ravien = (from bn in _data.BenhNhans.Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ").Where(x => x.NoiTru == 1) join vv in _data.VaoViens on bn.MaBNhan equals vv.MaBNhan join rv in _data.RaViens on bn.MaBNhan equals rv.MaBNhan select new { bn.MaBNhan, bn.DTuong,rv.NgayRa }).ToList();
                    #endregion

                    //Đầu kì BH
                    int slvvBH = vaovien.Where(p => p.NgayVao < tungay).Where(x => x.DTuong == "BHYT").Count();
                    int slrvBH = ravien.Where(p => p.NgayRa < tungay).Where(x => x.DTuong == "BHYT").Count();
                    int _slTonDKBH = slvvBH - slrvBH;
                    _bntonNTBH.BnDauKi = _slTonDKBH;

                    //Đầu kì DV
                    int slvvDV = vaovien.Where(p => p.NgayVao < tungay).Where(x => x.DTuong == "Dịch vụ").Count();
                    int slrvDV = ravien.Where(p => p.NgayRa < tungay).Where(x => x.DTuong == "Dịch vụ").Count();
                    int _slTonDKDV = slvvDV - slrvDV;
                    _bntonNTDV.BnDauKi = _slTonDKDV;
                    _bntonNTBH.Bh = "X";

                    //Trong kì BH
                    int _VaoVienTrongKiBH = vaovien.Where(p => p.NgayVao > tungay && p.NgayVao < denngay).Where(x => x.DTuong == "BHYT").Count();
                    _bntonNTBH.BnVaoVienTrongKi = _VaoVienTrongKiBH;
                    int _RaVienTrongKiBH = ravien.Where(p => p.NgayRa > tungay && p.NgayRa < denngay).Where(x => x.DTuong == "BHYT").Count();
                    _bntonNTBH.BnRaVienTrongKi = _RaVienTrongKiBH;
                    
                    // Trong kì DV
                    int _VaoVienTrongKiDV = vaovien.Where(p => p.NgayVao > tungay && p.NgayVao < denngay).Where(x => x.DTuong == "Dịch vụ").Count();
                    _bntonNTDV.BnVaoVienTrongKi = _VaoVienTrongKiDV;
                    int _RaVienTrongKiDV = ravien.Where(p => p.NgayRa > tungay && p.NgayRa < denngay).Where(x => x.DTuong == "Dịch vụ").Count();
                    _bntonNTDV.BnRaVienTrongKi = _RaVienTrongKiDV;

                    //Tồn nội trú BH
                    int _tonVVNTBH = vaovien.Where(p => p.DTuong == "BHYT").Where(x => x.NgayVao < denngay).Count();
                    int _tonRVNTBH = ravien.Where(p => p.DTuong == "BHYT").Where(x => x.NgayRa < denngay).Count();
                    int _tsTonNTBH = _tonVVNTBH - _tonRVNTBH;
                    _bntonNTBH.TonNT = _tsTonNTBH;

                    //Tồn nội trú DV 
                    int _tonVVNTDV = vaovien.Where(p => p.DTuong == "Dịch vụ").Where(x => x.NgayVao < denngay).Count();
                    int _tonRVNTDV = ravien.Where(p => p.DTuong == "Dịch vụ").Where(x => x.NgayRa < denngay).Count();
                    int _tsTonNTDV = _tonVVNTDV - _tonRVNTDV;
                    _bntonNTDV.TonNT = _tsTonNTDV;

                    if (chkBHYT.Checked)
                    {
                        _listBnTonNT.Add(_bntonNTBH);
                    }
                    if (chkDichvu.Checked)
                    {
                        _listBnTonNT.Add(_bntonNTDV);
                    }
                    
                    

                    string _mabv = DungChung.Bien.MaBV;
                    string _tencqcq = DungChung.Bien.TenCQCQ;
                    string _tencq = DungChung.Bien.TenCQ;
                    dic.Add("TuNgayDenNgay", s);
                    dic.Add("TenCQCQ", _tencqcq);
                    dic.Add("TenCQ", _tencq);
                    dic.Add("SignDate", signdate);
                   // if(chkBHYT.Checked)
                   // {
                   //     dic.Add("Bh", "X");
                   //     dic.Add("BnVaoVienTrongKiBH", _VaoVienTrongKiBH);
                   //     dic.Add("BnRaVienTrongKiBH", _RaVienTrongKiBH);
                   //     dic.Add("BnDauKiBH", _slTonDKBH);
                   //     dic.Add("TonNTBH", _tsTonNTBH);
                   // }
                   //if(chkDichvu.Checked)
                   //{
                   //    dic.Add("BnRaVienTrongKiDV", _RaVienTrongKiDV);
                   //    dic.Add("BnVaoVienTrongKiDV", _VaoVienTrongKiDV);
                   //    dic.Add("BnDauKiDV", _slTonDKDV);
                   //    dic.Add("TonNTDV", _tsTonNTDV);
                   //}
                    DungChung.Ham.Print(DungChung.PrintConfig.Rp_TonNoiTruBH_TuKy, _listBnTonNT, dic, false);
                }
            }
            
        }
    }
}