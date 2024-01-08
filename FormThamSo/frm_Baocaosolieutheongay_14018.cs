using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    public partial class frm_Baocaosolieutheongay_14018 : DevExpress.XtraEditors.XtraForm
    {
        public frm_Baocaosolieutheongay_14018()
        {
            InitializeComponent();
        }

        class solieu 
        {
            public int stt { get; set; }
            public string Muc { get; set; }
            public string groupDV { get; set; }
            public string tendv { get; set; }
            
            public string Ngay1 { get; set; }
            public string Ngay2 { get; set; }
            public string Ngay3 { get; set; }
            public string Ngay4 { get; set; }
            public string Ngay5 { get; set; }
            public string Ngay6 { get; set; }
            public string Ngay7 { get; set; }
            public string Ngay8 { get; set; }
            public string Ngay9 { get; set; }
            public string Ngay10 { get; set; }
            public string Ngay11 { get; set; }
            public string Ngay12 { get; set; }
            public string Ngay13 { get; set; }
            public string Ngay14 { get; set; }
            public string Ngay15 { get; set; }
            public string Ngay16 { get; set; }
            public string Ngay17 { get; set; }
            public string Ngay18 { get; set; }
            public string Ngay19 { get; set; }
            public string Ngay20 { get; set; }
            public string Ngay21 { get; set; }
            public string Ngay22 { get; set; }
            public string Ngay23 { get; set; }
            public string Ngay24 { get; set; }
            public string Ngay25 { get; set; }
            public string Ngay26 { get; set; }
            public string Ngay27 { get; set; }
            public string Ngay28 { get; set; }
            public string Ngay29 { get; set; }
            public string Ngay30 { get; set; }
            public string Ngay31 { get; set; }
            public string tong { get; set; }
        }

       
        private void BtnBaoCao_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<solieu> _list = new List<solieu>();
            DateTime tungay = DateTime.Now;
            DateTime denngay = DateTime.Now;
            if (txtTuNgay.Text != "")
                tungay =DungChung.Ham.NgayTu(Convert.ToDateTime(txtTuNgay.Text));
            if (txtdenngay.Text != "")
                denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));
            TimeSpan _tsday = denngay - tungay;
            
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Ngaythang","Từ ngày "+txtTuNgay.Text+ " đến ngày "+txtdenngay.Text);
            _dic.Add("tungay", tungay);
            _dic.Add("denngay", denngay);
            if (_tsday.Days >= 0)
            {
                if (_tsday.Days >= 31)
                {
                    MessageBox.Show("Bạn đã chọn ngày vượt quá giới hạn 31 ngày. Vui lòng chọn lại");
                    return;
                }
                if (chk.Checked == true)
                {
                    
                    int count = 0;
                    var getdv = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                 join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                                 join dv in data.DichVus.Where(p => p.IS_EXECUTE_CLS == true) on cd.MaDV equals dv.MaDV
                                 join tndv in data.TieuNhomDVs.Where(p => p.IdTieuNhom == 26 || p.IdTieuNhom == 27 || p.IdTieuNhom == 28 || p.IdTieuNhom==29) on dv.IdTieuNhom equals tndv.IdTieuNhom
                                 
                                 select new { dv.MaDV, dv.TenDV, cls.NgayTH }).ToList();

                    if (getdv.Count > 0)
                    {
                        var rgdv = (from dv in getdv select new { dv.MaDV, dv.TenDV }).Distinct().ToList();

                        //int[] Ngay = new int[31];
                        #region Theo từng dịch vụ
                        
                        foreach (var item in rgdv)
                        {
                            count++;
                            solieu _TungSL = new solieu();
                            _TungSL.groupDV = "Dịch vụ tổng";
                            _TungSL.Muc = "I";
                            _TungSL.tendv = item.TenDV;
                            _TungSL.stt = count;
                            int _countday = _tsday.Days;
                            int[] Ngay = new int[31];
                            int tong = 0;
                            for (int i = 0; i < 31; i++)
                            {
                                DateTime _bandau = DungChung.Ham.NgayTu(tungay.AddDays(i));
                                DateTime _ketthuc = DungChung.Ham.NgayDen(tungay.AddDays(i));
                                Ngay[i] = getdv.Where(p => p.NgayTH >= DungChung.Ham.NgayTu(tungay.AddDays(i)) && p.NgayTH <= DungChung.Ham.NgayDen(tungay.AddDays(i))).Where(p => p.TenDV == item.TenDV).Count();
                                tong += Ngay[i];
                                _TungSL.tong = tong != 0 ? tong.ToString() : "";
                            }
                            _TungSL.Ngay1 = Ngay[0] != 0 ? Ngay[0].ToString() : "";
                            _TungSL.Ngay2 = Ngay[1] != 0 ? Ngay[1].ToString() : "";
                            _TungSL.Ngay3 = Ngay[2] != 0 ? Ngay[2].ToString() : "";
                            _TungSL.Ngay4 = Ngay[3] != 0 ? Ngay[3].ToString() : "";
                            _TungSL.Ngay5 = Ngay[4] != 0 ? Ngay[4].ToString() : "";
                            _TungSL.Ngay6 = Ngay[5] != 0 ? Ngay[5].ToString() : "";
                            _TungSL.Ngay7 = Ngay[6] != 0 ? Ngay[6].ToString() : "";
                            _TungSL.Ngay8 = Ngay[7] != 0 ? Ngay[7].ToString() : "";
                            _TungSL.Ngay9 = Ngay[8] != 0 ? Ngay[8].ToString() : "";
                            _TungSL.Ngay10 = Ngay[9] != 0 ? Ngay[9].ToString() : "";
                            _TungSL.Ngay11 = Ngay[10] != 0 ? Ngay[10].ToString() : "";
                            _TungSL.Ngay12 = Ngay[11] != 0 ? Ngay[11].ToString() : "";
                            _TungSL.Ngay13 = Ngay[12] != 0 ? Ngay[12].ToString() : "";
                            _TungSL.Ngay14 = Ngay[13] != 0 ? Ngay[13].ToString() : "";
                            _TungSL.Ngay15 = Ngay[14] != 0 ? Ngay[14].ToString() : "";
                            _TungSL.Ngay16 = Ngay[15] != 0 ? Ngay[15].ToString() : "";
                            _TungSL.Ngay17 = Ngay[16] != 0 ? Ngay[16].ToString() : "";
                            _TungSL.Ngay18 = Ngay[17] != 0 ? Ngay[17].ToString() : "";
                            _TungSL.Ngay19 = Ngay[18] != 0 ? Ngay[18].ToString() : "";
                            _TungSL.Ngay20 = Ngay[19] != 0 ? Ngay[19].ToString() : "";
                            _TungSL.Ngay21 = Ngay[20] != 0 ? Ngay[20].ToString() : "";
                            _TungSL.Ngay22 = Ngay[21] != 0 ? Ngay[21].ToString() : "";
                            _TungSL.Ngay23 = Ngay[22] != 0 ? Ngay[22].ToString() : "";
                            _TungSL.Ngay24 = Ngay[23] != 0 ? Ngay[23].ToString() : "";
                            _TungSL.Ngay25 = Ngay[24] != 0 ? Ngay[24].ToString() : "";
                            _TungSL.Ngay26 = Ngay[25] != 0 ? Ngay[25].ToString() : "";
                            _TungSL.Ngay27 = Ngay[26] != 0 ? Ngay[26].ToString() : "";
                            _TungSL.Ngay28 = Ngay[27] != 0 ? Ngay[27].ToString() : "";
                            _TungSL.Ngay29 = Ngay[28] != 0 ? Ngay[28].ToString() : "";
                            _TungSL.Ngay30 = Ngay[29] != 0 ? Ngay[29].ToString() : "";
                            _TungSL.Ngay31 = Ngay[30] != 0 ? Ngay[30].ToString() : "";

                            _list.Add(_TungSL);
                        }
                            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BaoCaoSoLieuTheoNgay_14018, _list, _dic, false);
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu");
                    }
                }
                else
                {
                    int count = 0;

                    var getdv = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                 join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                 join tndv in data.TieuNhomDVs.Where(p => p.IdTieuNhom == 26 || p.IdTieuNhom == 27 || p.IdTieuNhom == 28 || p.IdTieuNhom == 29) on dv.IdTieuNhom equals tndv.IdTieuNhom

                                 select new { dv.MaDV, dv.TenDV, cls.NgayTH ,kp.TenKP,kp.PLoai}).ToList();

                    var getdv2 = (from kp in getdv.Where(p => p.PLoai != "Phòng khám" && p.PLoai != "Lâm sàng")
                                

                                 select new { kp.TenKP, kp.PLoai }).ToList();

                    var getkp = (from kp in getdv.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")

                                 select new 
                                 { 
                                     kp.MaDV, 
                                     kp.TenDV, 
                                     kp.NgayTH,kp.TenKP 
                                 }).ToList();

                    if (getdv.Count > 0)
                    {
                        var rgdv = (from dv in getdv select new { dv.MaDV, dv.TenDV }).Distinct().ToList();

                        //int[] Ngay = new int[31];
                        #region Theo từng dịch vụ
                        foreach (var item in rgdv)
                        {
                            count++;
                            solieu _TungSL = new solieu();
                            _TungSL.groupDV = "Dịch vụ tổng";
                            _TungSL.Muc = "I";
                            _TungSL.tendv = item.TenDV;
                            _TungSL.stt = count;
                            int _countday = _tsday.Days;
                            int[] Ngay = new int[31];
                            int tong = 0;
                            for (int i = 0; i < 31; i++)
                            {
                                DateTime _bandau = DungChung.Ham.NgayTu(tungay.AddDays(i));
                                DateTime _ketthuc = DungChung.Ham.NgayDen(tungay.AddDays(i));
                                Ngay[i] = getdv.Where(p => p.NgayTH >= DungChung.Ham.NgayTu(tungay.AddDays(i)) && p.NgayTH <= DungChung.Ham.NgayDen(tungay.AddDays(i))).Where(p => p.MaDV == item.MaDV).Count();
                                tong = tong + Ngay[i];
                                _TungSL.tong = tong != 0 ? tong.ToString() : "";
                            }
                            
                            _TungSL.Ngay1 = Ngay[0] != 0 ? Ngay[0].ToString() : ""; 
                            _TungSL.Ngay2 = Ngay[1] != 0 ? Ngay[1].ToString() : "";
                            _TungSL.Ngay3 = Ngay[2] != 0 ? Ngay[2].ToString() : "";
                            _TungSL.Ngay4 = Ngay[3] != 0 ? Ngay[3].ToString() : "";
                            _TungSL.Ngay5 = Ngay[4] != 0 ? Ngay[4].ToString() : "";
                            _TungSL.Ngay6 = Ngay[5] != 0 ? Ngay[5].ToString() : "";
                            _TungSL.Ngay7 = Ngay[6] != 0 ? Ngay[6].ToString() : "";
                            _TungSL.Ngay8 = Ngay[7] != 0 ? Ngay[7].ToString() : "";
                            _TungSL.Ngay9 = Ngay[8] != 0 ? Ngay[8].ToString() : "";
                            _TungSL.Ngay10 = Ngay[9] != 0 ? Ngay[9].ToString() : "";
                            _TungSL.Ngay11 = Ngay[10] != 0 ? Ngay[10].ToString() : "";
                            _TungSL.Ngay12 = Ngay[11] != 0 ? Ngay[11].ToString() : "";
                            _TungSL.Ngay13 = Ngay[12] != 0 ? Ngay[12].ToString() : "";
                            _TungSL.Ngay14 = Ngay[13] != 0 ? Ngay[13].ToString() : "";
                            _TungSL.Ngay15 = Ngay[14] != 0 ? Ngay[14].ToString() : "";
                            _TungSL.Ngay16 = Ngay[15] != 0 ? Ngay[15].ToString() : "";
                            _TungSL.Ngay17 = Ngay[16] != 0 ? Ngay[16].ToString() : "";
                            _TungSL.Ngay18 = Ngay[17] != 0 ? Ngay[17].ToString() : "";
                            _TungSL.Ngay19 = Ngay[18] != 0 ? Ngay[18].ToString() : "";
                            _TungSL.Ngay20 = Ngay[19] != 0 ? Ngay[19].ToString() : "";
                            _TungSL.Ngay21 = Ngay[20] != 0 ? Ngay[20].ToString() : "";
                            _TungSL.Ngay22 = Ngay[21] != 0 ? Ngay[21].ToString() : "";
                            _TungSL.Ngay23 = Ngay[22] != 0 ? Ngay[22].ToString() : "";
                            _TungSL.Ngay24 = Ngay[23] != 0 ? Ngay[23].ToString() : "";
                            _TungSL.Ngay25 = Ngay[24] != 0 ? Ngay[24].ToString() : "";
                            _TungSL.Ngay26 = Ngay[25] != 0 ? Ngay[25].ToString() : "";
                            _TungSL.Ngay27 = Ngay[26] != 0 ? Ngay[26].ToString() : "";
                            _TungSL.Ngay28 = Ngay[27] != 0 ? Ngay[27].ToString() : "";
                            _TungSL.Ngay29 = Ngay[28] != 0 ? Ngay[28].ToString() : "";
                            _TungSL.Ngay30 = Ngay[29] != 0 ? Ngay[29].ToString() : "";
                            _TungSL.Ngay31 = Ngay[30] != 0 ? Ngay[30].ToString() : "";

                            _list.Add(_TungSL);
                        }
                        
                        #endregion


                    }
                    if (getkp.Count > 0)
                    {
                        int count2 = 0;
                        var rgkp = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP }).Distinct().ToList();
                        #region Theo từng khoa
                        foreach (var item in rgkp)
                        {
                            count2++;
                            solieu _TungSL = new solieu();
                            _TungSL.groupDV = "Dịch vụ từng khoa";
                            _TungSL.Muc = "II";
                            _TungSL.tendv = item.TenKP;
                            _TungSL.stt = count2;

                            int _countday = _tsday.Days;
                            int[] Ngay = new int[31];
                            int tong = 0;
                            for (int i = 0; i < 31; i++)
                            {
                                DateTime _bandau = DungChung.Ham.NgayTu(tungay.AddDays(i));
                                DateTime _ketthuc = DungChung.Ham.NgayDen(tungay.AddDays(i));
                                Ngay[i] = getkp.Where(p => p.NgayTH >= DungChung.Ham.NgayTu(tungay.AddDays(i)) && p.NgayTH <= DungChung.Ham.NgayDen(tungay.AddDays(i))).Where(p => p.TenKP == item.TenKP).Count();
                                tong =tong+ Ngay[i];
                                _TungSL.tong = tong != 0 ? tong.ToString() : "";
                            }
                            _TungSL.Ngay1 = Ngay[0] != 0 ? Ngay[0].ToString() : "";
                            _TungSL.Ngay2 = Ngay[1] != 0 ? Ngay[1].ToString() : "";
                            _TungSL.Ngay3 = Ngay[2] != 0 ? Ngay[2].ToString() : "";
                            _TungSL.Ngay4 = Ngay[3] != 0 ? Ngay[3].ToString() : "";
                            _TungSL.Ngay5 = Ngay[4] != 0 ? Ngay[4].ToString() : "";
                            _TungSL.Ngay6 = Ngay[5] != 0 ? Ngay[5].ToString() : "";
                            _TungSL.Ngay7 = Ngay[6] != 0 ? Ngay[6].ToString() : "";
                            _TungSL.Ngay8 = Ngay[7] != 0 ? Ngay[7].ToString() : "";
                            _TungSL.Ngay9 = Ngay[8] != 0 ? Ngay[8].ToString() : "";
                            _TungSL.Ngay10 = Ngay[9] != 0 ? Ngay[9].ToString() : "";
                            _TungSL.Ngay11 = Ngay[10] != 0 ? Ngay[10].ToString() : "";
                            _TungSL.Ngay12 = Ngay[11] != 0 ? Ngay[11].ToString() : "";
                            _TungSL.Ngay13 = Ngay[12] != 0 ? Ngay[12].ToString() : "";
                            _TungSL.Ngay14 = Ngay[13] != 0 ? Ngay[13].ToString() : "";
                            _TungSL.Ngay15 = Ngay[14] != 0 ? Ngay[14].ToString() : "";
                            _TungSL.Ngay16 = Ngay[15] != 0 ? Ngay[15].ToString() : "";
                            _TungSL.Ngay17 = Ngay[16] != 0 ? Ngay[16].ToString() : "";
                            _TungSL.Ngay18 = Ngay[17] != 0 ? Ngay[17].ToString() : "";
                            _TungSL.Ngay19 = Ngay[18] != 0 ? Ngay[18].ToString() : "";
                            _TungSL.Ngay20 = Ngay[19] != 0 ? Ngay[19].ToString() : "";
                            _TungSL.Ngay21 = Ngay[20] != 0 ? Ngay[20].ToString() : "";
                            _TungSL.Ngay22 = Ngay[21] != 0 ? Ngay[21].ToString() : "";
                            _TungSL.Ngay23 = Ngay[22] != 0 ? Ngay[22].ToString() : "";
                            _TungSL.Ngay24 = Ngay[23] != 0 ? Ngay[23].ToString() : "";
                            _TungSL.Ngay25 = Ngay[24] != 0 ? Ngay[24].ToString() : "";
                            _TungSL.Ngay26 = Ngay[25] != 0 ? Ngay[25].ToString() : "";
                            _TungSL.Ngay27 = Ngay[26] != 0 ? Ngay[26].ToString() : "";
                            _TungSL.Ngay28 = Ngay[27] != 0 ? Ngay[27].ToString() : "";
                            _TungSL.Ngay29 = Ngay[28] != 0 ? Ngay[28].ToString() : "";
                            _TungSL.Ngay30 = Ngay[29] != 0 ? Ngay[29].ToString() : "";
                            _TungSL.Ngay31 = Ngay[30] != 0 ? Ngay[30].ToString() : "";

                            _list.Add(_TungSL);
                        }
                        
                        #endregion
                    }

                    DungChung.Ham.Print(DungChung.PrintConfig.Rep_BaoCaoSoLieuTheoNgay_14018, _list, _dic, false);

                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
            
        }

        private void frm_Baocaosolieutheongay_14018_Load(object sender, EventArgs e)
        {
            txtTuNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtdenngay.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void txtdenngay_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtdenngay.Text))
            {
                e.Cancel = true;
                //txtdenngay.Focus();
                errorProvider1.SetError(txtdenngay, "Bạn chưa điền ngày");
            }
            else
            {
                DateTime tungay = DateTime.Now;
                DateTime denngay = DateTime.Now;
                if (txtTuNgay.Text != "")
                    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txtTuNgay.Text));
                if (txtdenngay.Text != "")
                    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));
                TimeSpan _tsday = denngay - tungay;
                if(_tsday.Days<0)
                {
                    e.Cancel = true;
                    txtdenngay.Focus();
                    errorProvider1.SetError(txtdenngay, "Từ ngày lớn hơn đến ngày, vui lòng chọn lại !");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtdenngay, null);
                }
            }
        }

        private void txtTuNgay_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTuNgay.Text))
            {
                e.Cancel = true;
                //txtTuNgay.Focus();
                errorProvider1.SetError(txtTuNgay, "Bạn chưa điền ngày");
            }
            else
            {
                DateTime tungay = DateTime.Now;
                DateTime denngay = DateTime.Now;
                if (txtTuNgay.Text != "")
                    tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txtTuNgay.Text));
                if (txtdenngay.Text != "")
                    denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtdenngay.Text));
                TimeSpan _tsday = denngay - tungay;
                if (_tsday.Days < 0)
                {
                    e.Cancel = true;
                    txtTuNgay.Focus();
                    errorProvider1.SetError(txtTuNgay, "Từ ngày lớn hơn đến ngày, vui lòng chọn lại !");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtTuNgay, null);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}