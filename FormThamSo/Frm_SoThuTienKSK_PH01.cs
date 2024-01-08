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
    public partial class Frm_SoThuTienKSK_PH01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoThuTienKSK_PH01()
        {
            InitializeComponent();
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
        public  class BN_ksk
        {
            int _mabn;
            string _tenBn;
            string _dchi;
            int? _stt;

            public int? Stt
            {
                get { return _stt; }
                set { _stt = value; }
            }
            DateTime _ngaythu;
            int _soto;
            double _sotien;
            string _tchung;

            public string Tchung
            {
                get { return _tchung; }
                set { _tchung = value; }
            }

            public int Mabn
            {
              get { return _mabn; }
              set { _mabn = value; }
            }

            public string TenBn
            {
                get { return _tenBn; }
                set { _tenBn = value; }
            }

            public string Dchi
            {
                get { return _dchi; }
                set { _dchi = value; }
            }

           

            public DateTime Ngaythu
            {
                get { return _ngaythu; }
                set { _ngaythu = value; }
            }

            public int Soto
            {
                get { return _soto; }
                set { _soto = value; }
            }


            public double Sotien
            {
                get { return _sotien; }
                set { _sotien = value; }
            }
        }
        List<BN_ksk> bnKSK = new List<BN_ksk>(); 

        List<DTBN> _lDTBN = new List<DTBN>();
        private void Frm_SoThuTienKSK_PH01_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                byte _dtBN = 0;
                bnKSK.Clear();
                if (lupDoituong.EditValue != null)
                    _dtBN = Convert.ToByte(lupDoituong.EditValue.ToString());
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                string _td = "";
                if (_lDTBN.Where(p => p.IDDTBN == _dtBN).ToList().Count > 0)
                    _td = _lDTBN.Where(p => p.IDDTBN == _dtBN).First().MoTa;

                if (DungChung.Bien.MaBV == "30005" && lupDoituong.Text == "KSK")
                {
                    // những bệnh nhân thu thẳng
                    var q = (from bn in data.BenhNhans
                             where bn.IDDTBN == _dtBN 
                             join tu in data.TamUngs.Where(p => p.NgayThu >= ngaytu && p.NgayThu <= ngayden && p.PhanLoai == 3) on bn.MaBNhan equals tu.MaBNhan
                             join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                             join dv in data.DichVus on tuct.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 13) on dv.IdTieuNhom equals tn.IdTieuNhom
                             select new
                             {
                                 bn.MaBNhan,
                                 bn.TenBNhan,
                                 bn.DChi,
                                 bn.TChung,
                                 bn.SoTT,
                                 tu.NgayThu,
                                 tu.SoTo,
                                 tu.SoTien
                             }).OrderBy(p => p.TenBNhan).ToList();
                    #region những bệnh nhân nộp tiền khám sức khỏe khi duyệt
                    var q2 = (from bn in data.BenhNhans
                              where bn.IDDTBN == _dtBN
                              join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                              where (tu.NgayThu >= ngaytu && tu.NgayThu <= ngayden && tu.PhanLoai == 1)
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.DChi,
                                  bn.TChung,
                                  bn.SoTT,
                                  tu.NgayThu,
                                  tu.SoTo,
                                  tu.SoTien
                              }).OrderBy(p => p.TenBNhan).ToList();

                    var qduyet = (from tu in q2
                                  join bn in q on tu.MaBNhan equals bn.MaBNhan into kq
                                  from kq1 in kq.DefaultIfEmpty()
                                  where kq1 == null
                                  select tu).ToList();
                    var qkq = q.Union(qduyet);

                    


                    #endregion


                    if (qkq.Count() > 0)
                    {
                        foreach(var item in qkq)
                        {
                            BN_ksk _bnksk = new BN_ksk();
                            _bnksk.Mabn = item.MaBNhan;
                            _bnksk.Ngaythu = item.NgayThu.Value.Date;
                            _bnksk.Stt = item.SoTT;
                            _bnksk.Soto = item.SoTo.Value;
                            _bnksk.Sotien = item.SoTien.Value;
                            _bnksk.Tchung = item.TChung;
                            _bnksk.TenBn = item.TenBNhan;
                            _bnksk.Dchi = item.DChi;
                            bnKSK.Add(_bnksk);

                        }
                        if (ckInDoc.Checked)
                        {

                            frmIn frm = new frmIn();
                            BaoCao.Rep_SoThuTienKSK_PH01_Indoc rep = new BaoCao.Rep_SoThuTienKSK_PH01_Indoc();
                            rep.TuNgay.Value = lupTuNgay.Text;
                            rep.DenNgay.Value = lupDenNgay.Text;
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text;
                            rep.tieude.Text = "SỔ THU TIỀN ĐỐI TƯỢNG " + "KHÁM SỨC KHỎE";
                            rep.DataSource = bnKSK;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            frmIn frm = new frmIn();
                            BaoCao.Rep_SoThuTienKSK_PH01 rep = new BaoCao.Rep_SoThuTienKSK_PH01();
                            rep.TuNgay.Value = lupTuNgay.Text;
                            rep.DenNgay.Value = lupDenNgay.Text;
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text;
                            rep.tieude.Text = "SỔ THU TIỀN ĐỐI TƯỢNG " + "KHÁM SỨC KHỎE";
                            rep.DataSource = qkq.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Không có dữ liệu");

                }
                else
                {
                    var q = (from bn in data.BenhNhans
                             where bn.IDDTBN == _dtBN
                             join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                             where (tu.NgayThu >= ngaytu && tu.NgayThu <= ngayden && tu.PhanLoai != 0)
                             //group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.TChung } into kq
                             select new
                             {
                                 bn.MaBNhan,
                                 bn.TenBNhan,
                                 bn.DChi,
                                 bn.TChung,
                                 bn.SoTT,
                                 tu.NgayThu,
                                 tu.SoTo,
                                 tu.SoTien
                             }).OrderBy(p => p.TenBNhan).ToList();

                    if(q.Count >0)
                    {
                        foreach (var item in q)
                        {
                            BN_ksk _bnksk = new BN_ksk();
                            _bnksk.Mabn = item.MaBNhan;
                            _bnksk.Ngaythu = item.NgayThu.Value.Date;
                            _bnksk.Stt = item.SoTT;
                            _bnksk.Soto = item.SoTo.Value;
                            _bnksk.Sotien = item.SoTien.Value;
                            _bnksk.Tchung = item.TChung;
                            _bnksk.TenBn = item.TenBNhan;
                            _bnksk.Dchi = item.DChi;
                            bnKSK.Add(_bnksk);

                        }
                    }

                    if (q.Count > 0)
                    {
                        
                        if (ckInDoc.Checked)
                        {
                            frmIn frm = new frmIn();
                            BaoCao.Rep_SoThuTienKSK_PH01_Indoc rep = new BaoCao.Rep_SoThuTienKSK_PH01_Indoc();
                            rep.TuNgay.Value = lupTuNgay.Text;
                            rep.DenNgay.Value = lupDenNgay.Text;
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text;
                            rep.tieude.Text = "SỔ THU TIỀN ĐỐI TƯỢNG " + _td.ToUpper();
                            rep.DataSource = bnKSK;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            frmIn frm = new frmIn();
                            BaoCao.Rep_SoThuTienKSK_PH01 rep = new BaoCao.Rep_SoThuTienKSK_PH01();
                            rep.TuNgay.Value = lupTuNgay.Text;
                            rep.DenNgay.Value = lupDenNgay.Text;
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text;
                            rep.tieude.Text = "SỔ THU TIỀN ĐỐI TƯỢNG " + _td.ToUpper();
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Không có dữ liệu");
                }

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}