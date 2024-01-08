using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Office.Utils;

namespace QLBV.BaoCao
{
    public partial class frm_BaocaoKQKhamChuaBenh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaocaoKQKhamChuaBenh()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public class _Dulieu
        {
            public int TS_Sieuam { get; set; }
            public int TS_SieuamDopler { get; set; }
            public int TS_Xetnghiem { get; set; }
            public int TS_NoiSoi { get; set; }
            public int TS_XQuang { get; set; }
            public int NgoaiTru_YHCT { get; set; }
            public int NgoaiTru_BHYT { get; set; }
            public int NgoaiTru_Nu { get; set; }
            public int NoiTru_Nu { get; set; }
            public int NoiTru_BHYT { get; set; }
            public int TS_BHYT { get; set; }
            public int TS_YHCT { get; set; }
            public int NoiTru_YHCT { get; set; }
            public int TS_TE15 { get; set; }
            public int NoiTru_TE { get; set; }
            public int NgoaiTru_TE { get; set; }
            public int TS_Tuvong { get; set; }
            public int TS_TuvongTE { get; set; }
            public int TS_Chuyentuyen { get; set; }
            public int TS_Ngoaitru { get; set; }
            public int TS_Noitru { get; set; }
            public int TS_Khambenh { get; set; }
            public int TS_Nu { get; set; }
            public int TS_PhauThuat { get; set; }
            public int TS_ThuThuat { get; set; }
            public int TS_NgayDT { get; set; }
            public double TS_NgayDTNoiTru { get; set; }
        }
        List<_Dulieu> _listDuLieu = new List<_Dulieu>();
        List<int> selectedRowIndexes = new List<int>();
            
            
        private void btnBaocao_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            DateTime dt = DateTime.Now;
            List<string> lstMaICD = new List<string>();

            if (DungChung.Bien.MaBV == "24012")
            {
                foreach(var i in selectedRowIndexes)
                {
                    string maicd = grvICD.GetRowCellValue(i, colMaICD).ToString();
                    lstMaICD.Add(maicd);
                }
            }
            if (dateEdit1.EditValue != null)
            {
                dt = Convert.ToDateTime(dateEdit1.Text);
                if (dt.Month > DateTime.Now.Month || dt.Year > DateTime.Now.Year)
                {
                    MessageBox.Show("Tháng hoặc năm bạn chọn lớn hơn tháng hoặc năm hiện tại. Vui lòng chọn lại !");
                }
                else
                {
                    _listDuLieu.Clear();
                    string s = "Kết quả công tác khám, chữa bệnh tháng " + dt.Month.ToString() + " năm " + dt.Year.ToString();
                    string s2 = "Kim Thành, ngày "+DateTime.Now.Day.ToString()+" tháng "+DateTime.Now.Month.ToString()+" năm "+DateTime.Now.Year.ToString();
                    
                    _dic.Add("NgaythangTheoForm", s);
                    _dic.Add("NgaythangHethong",s2);

                    var _dulieu = (from bn in DataContext.BenhNhans
                                   join bnkb in DataContext.BNKBs.Where(p => p.NgayKham.Value.Month == dt.Month && p.NgayKham.Value.Year == dt.Year) on bn.MaBNhan equals bnkb.MaBNhan
                                   select new
                                   {
                                       bn.MaBNhan,
                                       bn.TenBNhan,
                                       bn.Tuoi,
                                       bn.DTuong,
                                       bnkb.MaCK,
                                       bn.CapCuu,
                                       bn.NoiTru,
                                       bnkb.NgayKham,bn.GTinh,bnkb.PhuongAn
                                   }).ToList();
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        _dulieu = (from bn in DataContext.BenhNhans
                                       join bnkb in DataContext.BNKBs.Where(p => p.NgayKham.Value.Month == dt.Month && p.NgayKham.Value.Year == dt.Year) on bn.MaBNhan equals bnkb.MaBNhan
                                       join icd in DataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                                       where bnkb.NgayKham.Value.Month == dt.Month && bnkb.NgayKham.Value.Year == dt.Year &&
                                             lstMaICD.Contains(icd.MaICD)
                                   select new
                                       {
                                           bn.MaBNhan,
                                           bn.TenBNhan,
                                           bn.Tuoi,
                                           bn.DTuong,
                                           bnkb.MaCK,
                                           bn.CapCuu,
                                           bn.NoiTru,
                                           bnkb.NgayKham,
                                           bn.GTinh,
                                           bnkb.PhuongAn
                                       }).ToList();
                    }
                    _Dulieu _getdulieu = new _Dulieu();

                    _getdulieu.NoiTru_BHYT = _dulieu.Where(p => p.NoiTru == 1).Count();
                    _getdulieu.NgoaiTru_BHYT = _dulieu.Where(p => p.NoiTru == 0).Count();
                    _getdulieu.TS_BHYT = _getdulieu.NoiTru_BHYT + _getdulieu.NgoaiTru_BHYT;
                    _getdulieu.NoiTru_TE = _dulieu.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 15).Count();
                    _getdulieu.NgoaiTru_TE = _dulieu.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Count();
                    _getdulieu.TS_TE15 = _getdulieu.NoiTru_TE + _getdulieu.NgoaiTru_TE;
                    _getdulieu.TS_Tuvong = _dulieu.Where(p => p.CapCuu == 2).Count();
                    _getdulieu.TS_Tuvong = _dulieu.Where(p=>p.Tuoi<15).Where(p => p.CapCuu == 2).Count();
                    _getdulieu.NoiTru_Nu=_dulieu.Where(p=>p.GTinh==0).Where(p=>p.NoiTru==1).Count();
                    _getdulieu.NgoaiTru_Nu = _dulieu.Where(p => p.GTinh == 0).Where(p => p.NoiTru == 0).Count();
                    //int TSXetNghiem = _dulieu.Where(p => p.MaCK == 22).Count();
                    //int TSSieuAm = _dulieu.Where(p => p.MaCK == 11).Count();
                    //int TSSieuAmDopler = _dulieu.Where(p => p.MaCK == 12).Count();
                    int NoiTru_YHCT = _dulieu.Where(p => p.NoiTru == 1).Where(p => p.MaCK == 7).Count();
                    int NgoaiTru_YHCT = _dulieu.Where(p => p.NoiTru == 0).Where(p => p.MaCK == 7).Count();
                    _getdulieu.TS_YHCT = _getdulieu.NoiTru_YHCT + _getdulieu.NgoaiTru_YHCT;

                    _getdulieu.TS_Ngoaitru = _getdulieu.NgoaiTru_BHYT + _getdulieu.NgoaiTru_TE + _getdulieu.NgoaiTru_YHCT+_getdulieu.NgoaiTru_Nu;
                    _getdulieu.TS_Noitru = _getdulieu.NoiTru_BHYT + _getdulieu.NoiTru_TE + _getdulieu.NoiTru_YHCT+_getdulieu.NoiTru_Nu;
                    _getdulieu.TS_Khambenh = _getdulieu.TS_Ngoaitru + _getdulieu.TS_Noitru;
                    _getdulieu.TS_Nu = _getdulieu.NgoaiTru_Nu + _getdulieu.NoiTru_Nu;
                    _getdulieu.TS_Chuyentuyen = _dulieu.Where(p => p.PhuongAn == 2).Count();
                    
                    foreach (var item in _dulieu)
                    {
                        var _tsgiuong = (from bn in DataContext.BenhNhans.Where(p=>p.MaBNhan==item.MaBNhan) join rv in DataContext.RaViens on bn.MaBNhan equals rv.MaBNhan select new { bn.MaBNhan,bn.NoiTru, rv.SoNgaydt }).ToList() ;
                        if(_tsgiuong.Count>0)
                        {
                            _getdulieu.TS_NgayDT += Convert.ToInt32(_tsgiuong.Where(p=>p.NoiTru==1).Select(p=>p.SoNgaydt).FirstOrDefault());
                        }
                        var _dichvu = (from _dt in DataContext.DThuocs.Where(p => p.MaBNhan == item.MaBNhan)
                                       join dtct in DataContext.DThuoccts on _dt.IDDon equals dtct.IDDon
                                       join dv in DataContext.DichVus on dtct.MaDV equals dv.MaDV
                                       join nhomdv in DataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                       join tieunhomdv in DataContext.TieuNhomDVs on nhomdv.IDNhom equals tieunhomdv.IDNhom
                                       select new
                                       {
                                           _dt.MaBNhan,
                                           dtct.MaDV,
                                           dv.TenDV,
                                           dv.IDNhom,
                                           tieunhomdv.TenRG
                                       }).ToList();
                        if (_dichvu.Count > 0)
                        {
                            _getdulieu.TS_Sieuam += _dichvu.Where(p => p.TenRG == "Siêu âm").Count();
                            _getdulieu.TS_SieuamDopler += _dichvu.Where(p => p.TenRG == "Siêu âm ( Doppler )").Count();
                            _getdulieu.TS_Xetnghiem += _dichvu.Where(p => p.TenDV.StartsWith("Xét nghiệm")).Count();
                            _getdulieu.TS_NoiSoi += _dichvu.Where(p => p.TenRG == "Nội soi").Count();
                            _getdulieu.TS_XQuang += _dichvu.Where(p => p.TenRG.Contains("Siêu âm")).Count();
                            _getdulieu.TS_ThuThuat += _dichvu.Where(p => p.TenRG=="Thủ thuật").Count();
                            _getdulieu.TS_PhauThuat += _dichvu.Where(p => p.TenRG=="Phẫu thuật").Count();
                        }
                    }
                    _getdulieu.TS_NgayDTNoiTru =_getdulieu.TS_Noitru !=0? _getdulieu.TS_NgayDT / _getdulieu.TS_Noitru:0;
                    _listDuLieu.Add(_getdulieu);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.Rp_KetQuaCongTacKhamChuaBenh_30010,_listDuLieu, _dic, false);
            }
            else MessageBox.Show("Vui lòng chọn lại ngày tháng năm");
        }

        private void frm_BaocaoKQKhamChuaBenh_Load(object sender, EventArgs e)
        {
            grcICD.DataSource = DataContext.ICD10.ToList();
            if(DungChung.Bien.MaBV == "24012")
            {
                label1.Visible = true;
                popICD.Visible = true;
            }
        }

        private void popICD_Popup(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                if (selectedRowIndexes.Count == 0)
                {
                    grvICD.SelectAll();
                }
                else
                {
                    foreach (int index in selectedRowIndexes)
                    {
                        grvICD.SelectRow(index);
                    }
                }
            }
        }

        private void popICD_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {

        }

        private void popICD_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                selectedRowIndexes = grvICD.GetSelectedRows().ToList();
            }
        }
    }
}