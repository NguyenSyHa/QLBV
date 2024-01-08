using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using OpenXmlPackaging;
using QLBV.DungChung;
using COMExcel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Xml.Linq;


namespace QLBV.FormThamSo
{
   
    public partial class frm_Bang4_5_2348 : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int load = 0;
        string tenbang = "Bang4";
        public frm_Bang4_5_2348()
        {
            InitializeComponent();
        }
        
        public frm_Bang4_5_2348(int mau)
        {           
            InitializeComponent();
        }
        private bool KT()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private void frmTsBcMau20_1399_Load(object sender, EventArgs e)
        {           
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            if (DungChung.Bien.MaBV == "12001")// bệnh viện tam đường
                ckDtuongCT.Visible = true;
            else
                ckDtuongCT.Visible = false;                      
            // load danh sách khoa phòng
          
            var q = (from k in data.KPhongs
                     join rv in data.RaViens on k.MaKP equals rv.MaKP
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }         
           
            load++;
            cboDTuongCT.SelectedIndex = 0;
            txtFileXMLPath.Text = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + tenbang + ".xml";
        }

        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        private string MaKPQD(int mKP)
        {
            string rs = "";
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKP = new List<KPhong>();
            _lKP = data.KPhongs.Where(p=>p.MaKP == mKP).ToList();
            if (_lKP.Count > 0)
                rs = _lKP.First().MaQD == null ? "" : _lKP.First().MaQD.ToString();
            return rs;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            #region Lấy danh sách khoa phòng
            List<int> _lMaKhoa = new List<int>();
            int kp = 0;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null)
                {
                    if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                    {
                        int mKhoa =  grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? 0 : Convert.ToInt32( grvKhoaPhong.GetRowCellValue(i, colmaKP));
                        if (mKhoa == 0)
                        {
                            kp = 0;
                            
                            break;
                        }
                        else
                            _lMaKhoa.Add(mKhoa);                        
                    }
                    else
                    {
                        kp = -1;
                    }
                }
            }
            #endregion lấy danh sách khoa phòng
            #region Biến                    
            int _makp = 0;            
            DateTime tungay, denngay;          
            int trongBH = 5; 
            int xp = radXP.SelectedIndex;
            if (KT())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;
                List<string> _ltamung = new List<string>();              

            #endregion biến
                if (rdBang.SelectedIndex == 0)
                {
                    #region Bảng 4
                    var q = (
                              from bn in data.BenhNhans
                              join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                              join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                              where (_ngaytt == 1 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (vp.NgayTT >= tungay && vp.NgayTT <= denngay))
                              where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.IDDTBN,
                                  bn.MaDTuong,
                                  bn.TuyenDuoi,
                                  bn.NoiTru,
                                  clsct.MaDVct,
                                  clsct.KetQua,
                                  cd.KetLuan,
                                  cd.TrongBH,
                                  cls.NgayTH,
                                  cls.MaKP
                              }).ToList();

                    var q1 = (from a in q
                             
                              where  ((a.TrongBH == 1) && (radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) )
                              where (kp == 0 ? true : _lMaKhoa.Contains(a.MaKP== null ? 0 : a.MaKP.Value))
                              where fnCheckDtuong(a.MaDTuong == null ? "" : a.MaDTuong.ToString().Trim())
                              where (radNoiTru.SelectedIndex < 2 ? a.NoiTru == radNoiTru.SelectedIndex : true)
                              select new
                              {
                                  a.MaBNhan,
                                  a.MaDVct,
                                  a.KetQua,
                                  a.KetLuan,
                                  a.NgayTH
                              }).ToList();
                    var q2 = (from a in q1
                              join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                              join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                              select new
                              {
                                  a.MaBNhan,
                                  dv.MaQD,
                                  dv.MaDV,
                                  a.MaDVct,
                                  dvct.TenDVct,
                                  a.KetQua,
                                  dvct.TenMay,
                                  MoTa = "",
                                  a.KetLuan,
                                  a.NgayTH
                              }).ToList();
                    if (q2.Count > 0)
                    {
                        bool rs = false;
                        try
                        {
                            int num = 1;
                            var xEle = new XElement("Bang4",
                                        from item in q2
                                        select new XElement("ma_lk",
                                            new XAttribute("ma_lk", item.MaBNhan),
                                                       new XElement("stt", num++),
                                                       new XElement("ma_dich_vu", item.MaQD),
                                                       new XElement("ma_chi_so", item.MaDVct),
                                                       new XElement("ten_chi_so", item.TenDVct),
                                                       new XElement("gia_tri", item.KetQua),
                                                       new XElement("ma_may", item.TenMay),
                                                       new XElement("mo_ta", item.MoTa),
                                                       new XElement("ket_luan", item.KetLuan),
                                                       new XElement("ngay_kq", item.NgayTH == null ? "" : Convert.ToDateTime(item.NgayTH).ToString("yyyyMMddhhmm"))
                                                   ));
                            xEle.Save(txtFileXMLPath.Text);
                            rs = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.ToString());
                            rs = false;
                        }
                        if (rs)
                            MessageBox.Show("Xuất file thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu");
                    }

                    #endregion
                }
                else
                {
                    #region Bảng 5
                    var q = (
                             from bn in data.BenhNhans
                             join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             join dt in data.DTBNs on bn.IDDTBN equals dt.IDDTBN                           
                             where (_ngaytt == 1 ? (vp.NgayDuyet >= tungay && vp.NgayDuyet <= denngay) : (vp.NgayTT >= tungay && vp.NgayTT <= denngay))
                             where (cboNoiTinh.SelectedIndex == 0 ? true : bn.NoiTinh == cboNoiTinh.SelectedIndex)
                             select new
                             {
                                 bn.MaBNhan,
                                 bn.IDDTBN,
                                 bn.MaDTuong,
                                 bn.TuyenDuoi,
                                 bn.NoiTru, 
                                 rv.MaKP,
                                 DT = dt.DTBN1
                                 //vpct.TrongBH,
                                 //vpct.MaKP
                             }).ToList();
                    
                    var q1 = (from a in q                                                         
                              where (kp == 0 ? true : _lMaKhoa.Contains(a.MaKP))
                              where fnCheckDtuong(a.MaDTuong == null ? "" : a.MaDTuong.ToString().Trim())
                              where (radNoiTru.SelectedIndex < 2 ? a.NoiTru == radNoiTru.SelectedIndex : true)
                              where (radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true)
                              where (a.DT == "BHYT")
                              select new
                              {
                                  a.MaBNhan
                              }).Distinct().ToList();
                    var q2 = (from a in q1
                              join db in data.DienBiens on a.MaBNhan equals db.MaBNhan
                            
                              select new
                              {
                                  a.MaBNhan,
                                  db.DienBien1,                                  
                                  Ngay_yl = db.NgayNhap,                                  
                                  PhauThuat = ""                                 
                              }).Select(p=> new {
                              p.MaBNhan,
                              p.DienBien1,
                              NgayOderby = Convert.ToDateTime(p.Ngay_yl).Date.ToString("yyyyMMddhhmm"),
                              p.PhauThuat,
                              KetLuan = p.Ngay_yl == null ? "" : getKL(p.MaBNhan,p.Ngay_yl.ToString()) //data.BBHCs.Where(p => p.NgayHC != null && Convert.ToDateTime(p.NgayHC).Date.ToString("yyyyMMdd") == Convert.ToDateTime(db.NgayNhap).Date.ToString("yyyyMMdd")).Select(p => p.KetLuan == null ? "" : p.KetLuan.ToString()).ToList(),
                              })
                              
                              .ToList();

                    if (q2.Count > 0)
                    {
                        bool rs = false;
                        try
                        {
                            int num = 1;
                            var xEle = new XElement("Bang5",
                                        from item in q2
                                        select new XElement("ma_lk",
                                            new XAttribute("ma_lk", item.MaBNhan),
                                                       new XElement("stt", num++),
                                                       new XElement("dien_bien", item.DienBien1),
                                                       new XElement("hoi_chan", item.KetLuan),
                                                       new XElement("phau_thuat", item.PhauThuat),
                                                       new XElement("ngay_yl", item.NgayOderby)
                                                   ));
                            xEle.Save(txtFileXMLPath.Text);
                            rs = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.ToString());
                            rs = false;
                        }
                        if (rs)
                            MessageBox.Show("Xuất file thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu");
                    }
                    #endregion
                }
            }
        }

        private string getKL(int maBNHan, string ngayYL)
        {
            string rs = "";
            ngayYL = Convert.ToDateTime(ngayYL).ToString("yyyyMMdd");
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = (from a in data.BBHCs select new {a.KetLuan, a.NgayHC, a.MaBNhan }).Where(p => p.MaBNhan != null && p.MaBNhan == maBNHan)
                .Select(p => new
                {
                    p.KetLuan,
                    p.NgayHC,
                    NgayHC1 = p.NgayHC //== null ? "" :Convert.ToDateTime(p.NgayHC).ToString("yyyyMMdd")
                })
                .ToList();
            var q2 = q.
                Where(p =>  p.NgayHC1 == null ? false: Convert.ToDateTime(p.NgayHC).ToString("yyyyMMdd") == ngayYL).
                Select(p => p.KetLuan).ToList();
                
            if (q2.Count > 0)
            {
                rs = String.Join(",", q2);
            }
            return rs;
        }
      
        private bool fnCheckDtuong(string maDT)
        {
            bool rs = false;
            if (cboDTuongCT.SelectedIndex == 0)
                rs = true;
            else if (cboDTuongCT.SelectedIndex == 1)
            {
                if (maDT == "DT" || maDT == "HN" || maDT == "DK")
                    rs = true;
            }
            else if (cboDTuongCT.SelectedIndex == 2)
            {
                if (maDT == "TE")
                    rs = true;
            }
            else if (cboDTuongCT.SelectedIndex == 3)
            {
                if (maDT != "DT" && maDT != "HN" && maDT != "DK" && maDT != "TE")
                    rs = true;
            }
            return rs;
        }           
        static String NgayTu_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 00:00:00 AM");
        }
        public static String NgayDen_Store(DateTime ngaydmy)
        {
            int d = ngaydmy.Day;
            int m = ngaydmy.Month;
            int y = ngaydmy.Year;

            return (m.ToString() + "-" + d.ToString() + "-" + y.ToString() + " 23:59:59.998 PM");
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
     
        SaveFileDialog dialog = new SaveFileDialog();   

       
        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {

                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }
     
               
        

        private void btnChonfileXML_Click_1(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "XML files (*.xml)|*.xml;";
            dialog.FilterIndex = 1;
            dialog.FileName = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + tenbang +".xml";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileXMLPath.Text = dialog.FileName;
            }
        }
        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private void grcKhoaPhong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {

                GridControl grid = sender as GridControl;
                GridView view = grid.FocusedView as GridView;               
                if (view.IsEditing)
                    view.CloseEditor();
                grid.SelectNextControl(grid, e.Modifiers == Keys.None, false, false, true);
                e.Handled = true;
                radXP.Focus();
            }
        }
        private void rdBang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdBang.SelectedIndex == 0)                
                tenbang = "Bang4";
            else
                tenbang = "Bang5";
            txtFileXMLPath.Text = "C:\\" + DungChung.Bien.MaTinh + DateTime.Now.Year + timquy(DateTime.Now.Month) + "_" + tenbang + ".xml";
        }

       

     
    }
}
