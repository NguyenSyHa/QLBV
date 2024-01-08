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
    public partial class frm_THThanhToamBNRaVien : DevExpress.XtraEditors.XtraForm
    {
        public frm_THThanhToamBNRaVien()
        {
            InitializeComponent();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lkp = new List<KPhong>();
        private void frm_BangKeQuyetToanVP_Load(object sender, EventArgs e)
        {
          
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            _lkp = data.KPhongs.ToList();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

           

           // var qkp = (from kpchon in lmakp join kp in _lkp on kpchon equals kp.MaKP select new { kp.MaKP, kp.TenKP }).ToList();
            //var qbn0 = (from rv in data.RaViens.Where(p => (radTimKiem.SelectedIndex == 1 ? true : (p.NgayRa >= tungay && p.NgayRa <= denngay)) && lmakp.Contains(p.MaKP ?? 0))
            //            join vp in data.VienPhis.Where(p => radTimKiem.SelectedIndex == 0 ? true : (p.NgayTT >= tungay && p.NgayTT <= denngay)) on rv.MaBNhan equals vp.MaBNhan
            //            join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
            //            select new { bn.MaBNhan, bn.NoiTru, bn.DTuong, rv.MaKP, vp.idVPhi }).ToList();

            //var qbn = (from bn in qbn0 join kp in qkp on bn.MaKP equals kp.MaKP select new { bn.MaBNhan, bn.NoiTru, bn.DTuong, kp.MaKP, kp.TenKP, bn.idVPhi }).ToList();

            //List<int> lMaBN = qbn.Select(p => p.MaBNhan).ToList();
            //List<int> lIDVP = qbn.Select(p => p.idVPhi).ToList();
            ////  List<int> lIDVPNoitru = qbn.Where(p=>p.NoiTru == 1).Select(p => p.idVPhi).ToList();
            //var qvpct = (from vp in data.VienPhicts.Where(p => lIDVP.Contains(p.idVPhi ?? 0)) select vp).ToList();





            //var qvp1 = (from rv in data.RaViens.Where(p => (radTimKiem.SelectedIndex == 1 ? true : (p.NgayRa >= tungay && p.NgayRa <= denngay)))
            //            join vp in data.VienPhis.Where(p => radTimKiem.SelectedIndex == 0 ? true : (p.NgayTT >= tungay && p.NgayTT <= denngay)) on rv.MaBNhan equals vp.MaBNhan
            //            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //            select new { rv.MaBNhan, rv.MaKP, vp.idVPhi, vpct }).ToList();
            //var qbn = data.BenhNhans.Where(p => idDTBN == 100 ? true : p.IDDTBN == idDTBN).ToList();
            //var qbntu = (from vp in qvp1
            //             join bn in qbn on vp.MaBNhan equals bn.MaBNhan
            //             group new { vp, bn } by new { vp.MaBNhan, vp.MaKP, bn.DTuong, bn.NoiTru, vp.idVPhi } into kq
            //             select new { kq.Key.MaKP, kq.Key.MaBNhan, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.idVPhi }).ToList();

            //var qvpct = (from vp in qbntu
            //             join kp in qkp on vp.MaKP equals kp.MaKP
            //             select new { vp.MaBNhan, vp.DTuong, vp.NoiTru, kp.MaKP, kp.TenKP }).ToList();



            var qvp1 = (from rv in data.RaViens.Where(p => (radTimKiem.SelectedIndex == 0 ? (p.NgayRa >= tungay && p.NgayRa <= denngay) : true))
                        join vp in data.VienPhis.Where(p =>( radTimKiem.SelectedIndex == 1 ? (p.NgayTT >= tungay && p.NgayTT <= denngay) : (radTimKiem.SelectedIndex == 2 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : true))) on rv.MaBNhan equals vp.MaBNhan
                         join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                        join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi                       
                        select new { rv.MaBNhan,bn.DTuong, bn.NoiTru, rv.MaKP, vp.idVPhi, vpct, vp.Duyet, vp.NgayDuyet }).ToList();
           // var qbn = data.BenhNhans.Where(p => idDTBN == 100 ? true : p.IDDTBN == idDTBN).ToList();
            var qbntu = (from vp in qvp1
                       //  join bn in qbn on vp.MaBNhan equals bn.MaBNhan
                         group new { vp } by new { vp.MaBNhan, vp.MaKP, vp.DTuong, vp.NoiTru, vp.idVPhi } into kq
                         select new { kq.Key.MaKP, kq.Key.MaBNhan, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.idVPhi }).ToList();

            var qvpct = (from vp in qbntu
                         join kp in _lkp on vp.MaKP equals kp.MaKP
                         select new { vp.MaBNhan, vp.DTuong, vp.NoiTru, kp.MaKP, kp.TenKP }).ToList();
         

            var qtu = data.TamUngs.ToList();
           

            var qtamung0 = (from tu in qtu
                            join bn in qvpct on tu.MaBNhan equals bn.MaBNhan
                               //.Where(p => lMaBN.Contains(p.MaBNhan ?? 0))
                           select new
                           {
                               tu.PhanLoai,
                               bn.MaBNhan,
                               bn.NoiTru,
                               bn.DTuong,
                               bn.TenKP, bn.MaKP,                              
                               TamUngSL = tu.PhanLoai == 0 ? true : false,
                               TamUngTT = tu.PhanLoai == 0 ? tu.SoTien : 0,
                               ThuThangSL = tu.PhanLoai == 3 ? true : false,
                               ThuThangTT = tu.PhanLoai == 3 ? tu.SoTien : 0,
                               ThanhToanSL = (tu.PhanLoai == 1 || tu.PhanLoai == 2) ? true : false,
                               ThanhToanTT = (tu.PhanLoai == 1 || tu.PhanLoai == 2) ? tu.SoTien : 0,
                               TienTraLaiBN = tu.PhanLoai == 2 ? tu.TienChenh : (tu.PhanLoai == 4 ? tu.SoTien : 0),
                               TienNopThem = tu.PhanLoai == 1 ? tu.TienChenh : 0

                           }).ToList();
            var qbnDuyet = (from tu in qtamung0.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select tu.MaBNhan).ToList();

            var qtamung = (from tu in qtamung0
                           join bn in qbnDuyet on tu.MaBNhan equals bn
                          group tu by new {tu.MaBNhan, tu.NoiTru, tu.DTuong, tu.MaKP, tu.TenKP} into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.NoiTru,
                               kq.Key.DTuong,
                               kq.Key.TenKP,
                               kq.Key.MaKP,
                               TamUngSL = kq.Where(p=>p.TamUngSL ==true).Count() > 0  ? 1 : 0,
                               TamUngTT = kq.Sum(p=>p.TamUngTT),
                               ThuThangSL = kq.Where(p => p.ThuThangSL == true).Count() > 0 ? 1 : 0,
                               ThuThangTT = kq.Sum(p => p.ThuThangTT),
                               ThanhToanSL = kq.Where(p => p.ThanhToanSL == true).Count() > 0 ? 1 : 0,
                               ThanhToanTT = kq.Sum(p => p.ThanhToanTT),
                               TienTraLaiBN = kq.Sum(p => p.TienTraLaiBN),
                               TienNopThem = kq.Sum(p => p.TienNopThem)
                           }).ToList();
           
            #region nội tru
            var qkq = (from tu in qtamung.Where(p => p.NoiTru == 1 && p.DTuong != "BHYT")                      
                       group tu  by new { tu.TenKP, tu.MaKP } into kq
                       select new
                       {
                           kq.Key.MaKP,
                           kq.Key.TenKP,
                           TamUngSL = kq.Sum(p => p.TamUngSL),
                           TamUngTT = kq.Sum(p => p.TamUngTT),
                           ThuThangSL = kq.Sum(p => p.ThuThangSL),
                           ThuThangTT = kq.Sum(p => p.ThuThangTT),
                           ThanhToanTT = kq.Sum(p => p.ThanhToanTT),
                           ThanhToanSL = kq.Sum(p => p.ThanhToanSL),                          
                           TienTraLaiBN = kq.Sum(p => p.TienTraLaiBN),
                           TienNopThem = kq.Sum(p => p.TienNopThem),
                           tsbn = kq.Count()
                       }).ToList();

            frmIn frm = new frmIn();
            BaoCao.rep_THThanhToanBNRaVien rep = new BaoCao.rep_THThanhToanBNRaVien();
            rep.DataSource = qkq;

            int index1 = DungChung.Bien.FormatString[1].IndexOf(':');
            int index2 = DungChung.Bien.FormatString[1].IndexOf('}');
            string fomat = "c2";
            if (index2 > index1 + 1)
            {
                fomat = DungChung.Bien.FormatString[1].Substring(index1 + 1, index2 - index1 - 1);
            }

            //Xquang
            List<int> dvuXQ = (from dv in data.DichVus join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) on dv.IdTieuNhom equals tn.IdTieuNhom select dv.MaDV).ToList();

            var qXQuangT = (from vp in qvp1                    
                          //  join bn in qbn on vp.MaBNhan equals bn.MaBNhan
                            join dv in dvuXQ on vp.vpct.MaDV equals dv                          
                            join bn in qbnDuyet on vp.MaBNhan equals bn
                            select new { vp.vpct.TienBN, vp.NoiTru, vp.DTuong, vp.vpct.ThanhToan }).ToList();// (from vp in data.VienPhicts.Where(p => lIDVPNoitru.Contains(p.idVPhi ?? 0) && dvuXQ.Contains(p.MaDV ?? 0)) select vp).ToList();
          
            var qXQ0 = qXQuangT.Where(p => p.NoiTru == 1 && p.DTuong != "BHYT").ToList();
            var qXQ1 = qXQuangT.Where(p => p.NoiTru == 0 && p.DTuong != "BHYT").ToList();
            var qXQ4 = qXQuangT.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT").ToList();
            var qXQ5 = qXQuangT.Where(p => p.NoiTru == 0 && p.DTuong == "BHYT").ToList();
            // xquang nội trú dịch vụ (phần thu thẳng)
            double qXQTT = 0;
            if (qXQ0.Where(p => p.ThanhToan == 1).Count() > 0)
            {
                qXQTT = qXQ0.Where(p => p.ThanhToan == 1).Sum(p => p.TienBN);
                rep.cel08.Text = qXQTT.ToString(fomat);
            }

            double qXQ05 = 0, qXQ15 = 0, qXQ45 = 0, qXQ55 = 0, qXQ85 = 0;

            //XQuang nội trú dịch vụ (không tính thu thẳng)
            if (qXQ0.Where(p => p.ThanhToan == 0).Count() > 0)
                qXQ05 = qXQ0.Where(p => p.ThanhToan == 0).Sum(p => p.TienBN);
            rep.celXQTT.Text = qXQ05.ToString(fomat);
            rep.celXQNopThem.Text = qXQ05.ToString(fomat);

            //xquang ngoại trú dịch vụ (không tính thu thẳng)
            if (qXQ1.Where(p => p.ThanhToan == 0).Count() > 0)
                qXQ15 = qXQ1.Where(p => p.ThanhToan == 0).Sum(p => p.TienBN);

            //xquang nội trú bảo hiểm (không tính thu thẳng)
            if (qXQ4.Where(p => p.ThanhToan == 0).Count() > 0)
                qXQ45 = qXQ4.Where(p => p.ThanhToan == 0).Sum(p => p.TienBN);

            //xquang ngoại trú bảo hiểm (không tính thu thẳng)
            if (qXQ5.Where(p => p.ThanhToan == 0).Count() > 0)
                qXQ55 = qXQ5.Where(p => p.ThanhToan == 0).Sum(p => p.TienBN);

            //xquang tổng (không tính thu thẳng)
            if (qXQuangT.Where(p => p.ThanhToan == 0).Count() > 0)
                qXQ85 = qXQuangT.Where(p => p.ThanhToan == 0).Sum(p => p.TienBN);

            double tongthu1 = 0;
            double tongthu2 = 0;
            double tongthu3 = 0;
            double tongthu4 = 0;
            double tongthu5 = 0;// thu thẳng
            if (qkq.Count > 0)
            {
                tongthu1 = qkq.Sum(p => p.TamUngTT ?? 0);
                tongthu2 = qkq.Sum(p => p.ThanhToanTT ?? 0);
                tongthu3 = qkq.Sum(p => p.TienTraLaiBN ?? 0);
                tongthu4 = qkq.Sum(p => p.TienNopThem);
                tongthu5 = qkq.Sum(p => p.ThuThangTT??0);
                rep.celTongThuTamUng.Text = tongthu1.ToString(fomat);
                rep.celTongTT.Text = (tongthu2 - qXQ05).ToString(fomat);
                rep.celTongThu_tienthua.Text = tongthu3.ToString(fomat);
                rep.celTongThu_NopThem.Text = (tongthu4 - qXQ05).ToString(fomat);
                rep.celTongThuThang.Text = (tongthu5 - qXQTT).ToString(fomat);
            }
            #endregion


            var qtamungNgTru2 = (from tu in qtamung.Where(p => p.NoiTru == 0 || p.DTuong == "BHYT")
                                // join bn in qbn.Where(p => p.NoiTru == 0 || p.DTuong == "BHYT") on tu.MaBNhan equals bn.MaBNhan
                                 select new
                                 {
                                     tu.MaBNhan,
                                     tu.DTuong,
                                     tu.NoiTru,
                                     TamUngTT = tu.TamUngTT,
                                     ThanhToanTT = tu.ThanhToanTT,
                                     TienTraLaiBN = tu.TienTraLaiBN,
                                     TienNopThem = tu.TienNopThem,
                                     ThuThangTT =  tu.ThuThangTT,
                                 }).ToList();

            
            if (qtamungNgTru2.Count > 0)
            {
                


                //ngoại trú dịch vụ, ksk
                var q1 = qtamungNgTru2.Where(p => p.DTuong != "BHYT" && p.NoiTru == 0).ToList();
                double q13 = 0, q15 = 0, q16 = 0, q17 = 0, q18 =0;
                if (q1.Count > 0)
                {
                    q13 = q1.Sum(p => p.TamUngTT ?? 0);//tạm ứng
                    rep.cel13.Text = q13.ToString(fomat);

                    q15 = q1.Sum(p => p.ThanhToanTT ?? 0);
                    rep.cel15.Text = (q15 - qXQ15).ToString(fomat);

                    q16 = q1.Sum(p => p.TienTraLaiBN ?? 0);//trả lại
                    rep.cel16.Text = q16.ToString(fomat);

                    q17 = q1.Sum(p => p.TienNopThem);// nộp thêm
                    rep.cel17.Text = (q17-qXQ15) .ToString(fomat);

                    q18 = q1.Sum(p => p.ThuThangTT ??0);// nộp thêm
                    rep.cel18.Text = q18.ToString(fomat);
                }

                // vận chuyển
                List<int> qdvVanChuyen = data.DichVus.Where(p => p.IDNhom == 12).Select(p => p.MaDV).ToList();
                var qVC = (from vp in qvp1 join vc in qdvVanChuyen on vp.vpct.MaDV equals vc select vp.vpct.TienBN).ToList();// (from vp in data.VienPhicts.Where(p => lIDVP.Contains(p.idVPhi ?? 0) && qdvVanChuyen.Contains(p.MaDV ?? 0)) select vp).ToList();
                double q35 = 0;
                if (qVC.Count() > 0)
                    q35 = qVC.Sum(p => p);
                rep.cel35.Text = q35.ToString(fomat);
                rep.cel37.Text = q35.ToString(fomat);

                //BH nội trú
                var q4 = qtamungNgTru2.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT").ToList();
                double q43 = 0, q46 = 0, q47 = 0, q45 = 0, q48 = 0;
                if (q4.Count > 0)
                {
                    q43 = q4.Sum(p => p.TamUngTT ?? 0);//tạm ứng
                    rep.cel43.Text = q43.ToString(fomat);

                    q45 = q4.Sum(p => p.ThanhToanTT ?? 0);
                    rep.cel45.Text = (q45 - qXQ45).ToString(fomat);

                     q46 = q4.Sum(p => p.TienTraLaiBN ?? 0);//trả lại
                    rep.cel46.Text = q46.ToString(fomat);

                     q47 = q4.Sum(p => p.TienNopThem);// nộp thêm
                    rep.cel47.Text = (q47- qXQ45).ToString(fomat);

                    q48 = q4.Sum(p => p.ThuThangTT??0);// thu thẳng
                    rep.cel48.Text = q48.ToString(fomat);

                   
                }


                //bh ngoại trú
                var q5 = qtamungNgTru2.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0).ToList();
                double q53 = 0, q55 = 0, q56 = 0, q57 = 0, q58 = 0;
                if (q5.Count > 0)
                {
                     q53 = q5.Sum(p => p.TamUngTT ?? 0);
                    rep.cel53.Text = q53.ToString(fomat);
                     q55 = q5.Sum(p => p.ThanhToanTT ?? 0);
                    rep.cel55.Text = (q55 - qXQ55).ToString(fomat);
                     q56 = q5.Sum(p => p.TienTraLaiBN ?? 0);
                    rep.cel56.Text = q56.ToString(fomat);
                     q57 = q5.Sum(p => p.TienNopThem);
                    rep.cel57.Text =( q57 - qXQ55).ToString(fomat);
                    q58 = q5.Sum(p => p.ThuThangTT??0);
                    rep.cel58.Text = q58.ToString(fomat);
                }

              
              

                //Tổng cộng
                rep.cel83.Text = (q13 + q43 + q53 + tongthu1).ToString(fomat);
                rep.cel85.Text = (tongthu2 - qXQ85 + q15 + q35 + q45 + q55).ToString(fomat);
                rep.cel86.Text = (tongthu3 + q16+ q46 + q56).ToString(fomat);
                rep.cel87.Text = (tongthu4 - qXQ85 + q17 + q35 + q47 + q57).ToString(fomat);
                rep.cel88.Text = (tongthu5 - qXQTT + q18 + q48 + q58 ).ToString(fomat);
               
                rep.celVP1.Text = rep.cel85.Text;
                rep.celVP2.Text = qXQ85.ToString(fomat);


            }

            rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }

       

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}