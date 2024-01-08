using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.BaoCao
{
    public partial class Frm_BaoCaoSoKhamBenh_30372 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BaoCaoSoKhamBenh_30372()
        {
            InitializeComponent();
        }
        public class BenhnhanKB
        {
            public string TenPhongKham { get; set; }
            public string GioPhutKham { get; set; }
            public DateTime? NgayKham { get; set; }
            public string TenBenhNhan { get; set; }
            public string NamSinh { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string GhiChu { get; set; }
            public string SoDienThoai { get; set; }
            public string YeuCauCLS { get; set; }
            public string ChanDoan { get; set; }
            public string NguonKhach { get; set; }
            public string MaBenhChinh { get; set; }
            public string TenBenhChinh { get; set; }
            public string MaBenhKemTheo { get; set; }
            public string MaBenhKemTheo2 { get; set; }
            public string TenBenhKemTheo { get; set; }
            public string SoTheBHYT { get; set; }
            public string CachXuTri { get; set; }
            public int MaBenhNhan { get; set; }
            public int? MaKP { get; set; }
            public int? NoiTru { get; set; }
            public bool DTNT { get; set; }
            public int STT { get; set; }
        }

        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhnhanKB> _listBNKB = new List<BenhnhanKB>();
        private void BtnInBaoCao_Click(object sender, EventArgs e)
        {
            _listBNKB.Clear();
            string _dtuong ="";
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            if (txtTuNgay.Text != null || txtTuNgay.Text !="")
                _ngaytu = Convert.ToDateTime(txtTuNgay.Text);
            if (txtDenNgay.Text != null || txtDenNgay.Text !="")
                _ngayden = Convert.ToDateTime(txtDenNgay.Text);
            if(cbDoiTuong.Text !=null || cbDoiTuong.Text!="")
                _dtuong=cbDoiTuong.Text;
            BenhnhanKB _bnkb = new BenhnhanKB();
            int noitru = radNT.SelectedIndex;
            var q1 = (from bn in _Data.BenhNhans.Where(p => noitru == 2 ? (p.NoiTru == 0 && p.DTNT) : p.NoiTru == noitru).Where(p=>p.DTuong ==_dtuong )
                      join bnkb in _Data.BNKBs.Where(p => p.NgayKham >= _ngaytu && p.NgayKham <= _ngayden)
                          on bn.MaBNhan equals bnkb.MaBNhan
                      select new BenhnhanKB
                      {
                         MaBenhNhan= bn.MaBNhan,
                         TenBenhNhan= bn.TenBNhan,
                         NgayKham = bnkb.NgayKham,
                         //GioPhutKham =bnkb.NgayKham.Value.Hour.ToString(),
                          NamSinh= bn.NamSinh,
                         GioiTinh = bn.GTinh == 0 ? "Nữ" : "Nam",
                         DiaChi= bn.DChi,
                         GhiChu= bnkb.GhiChu,
                         ChanDoan= bnkb.ChanDoan,
                         SoTheBHYT= bn.SThe,
                         MaBenhChinh= bnkb.MaICD,
                         MaBenhKemTheo=bnkb.MaICD2,
                         MaKP=bnkb.MaKP,
                         NoiTru=bn.NoiTru,
                         DTNT=bn.DTNT,
                      }).ToList();
            
            _listBNKB.AddRange(q1);
            if (q1.Count > 0)
            {
                int t = 0;
                foreach (var item in _listBNKB)
                {
                    item.STT = t + 1;
                    item.SoDienThoai = _Data.TTboXungs.Where(p => p.MaBNhan == item.MaBenhNhan).Select(p => p.DThoai).FirstOrDefault();
                    item.TenBenhChinh = _Data.ICD10.Where(p => p.MaICD == item.MaBenhChinh).Select(p => p.TenICD).FirstOrDefault();
                    item.GioPhutKham = item.NgayKham.Value.Hour + " giờ " + item.NgayKham.Value.Minute + " phút";
                   
                    string[] _mabenhphu = item.MaBenhKemTheo.Split(';');
                    
                    for(int i=0; i<_mabenhphu.Count();i++)
                    {
                        string s = "";
                        if (_mabenhphu[i].Length > 0)
                        {
                            s = _mabenhphu[i].ToString();
                            item.MaBenhKemTheo2 += s + ";";
                            item.TenBenhKemTheo += _Data.ICD10.Where(p => p.MaICD == s).Select(p => p.TenICD).FirstOrDefault() + "/";
                        }
                        
                    }
                    //------------------------------------------------
                    if(item.MaKP != null)
                    {
                        var _kp = _Data.KPhongs.Where(p => p.MaKP == item.MaKP).Select(p => p.TenKP).ToList();
                        item.TenPhongKham = _kp.First().ToString();
                    }
                    t++;
                }

                foreach (var item in q1 )
                {
                    //---------------------------------------------------
                    var q2 = (from cls in _Data.CLS.Where(p => p.MaBNhan == item.MaBenhNhan)
                              join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                              join tndv in _Data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                              select new
                              {
                                  tndv.TenRG,
                                  tndv.TenTN
                              }).Distinct().ToList();
                    if (q2.Count > 0)
                    {
                        if (q2.Count == 1)
                        {
                            item.YeuCauCLS = q2[0].TenRG;
                        }
                        else
                        {
                            for (int i = 0; i < q2.Count; i++)
                            {
                                item.YeuCauCLS += q2[i].TenRG + ", ";
                            }
                        }
                    }
                }

                    //DungChung.Ham.Print(DungChung.PrintConfig.Rep_BaoCaoSoKhamBenh_30372, _listBNKB, new Dictionary<string, object>(), false);
                    gridControl1.DataSource = _listBNKB;
                gridView1.BestFitColumns();
                
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        switch (fileExtenstion)
                        {
                            case ".xls":
                                gridControl1.ExportToXls(exportFilePath);
                                break;
                            case ".xlsx":
                                gridControl1.ExportToXlsx(exportFilePath);
                                break;
                            case ".rtf":
                                gridControl1.ExportToRtf(exportFilePath);
                                break;
                            case ".pdf":
                                gridControl1.ExportToPdf(exportFilePath);
                                break;
                            case ".html":
                                gridControl1.ExportToHtml(exportFilePath);
                                break;
                            case ".mht":
                                gridControl1.ExportToMht(exportFilePath);
                                break;
                            default:
                                break;
                        }

                        if (File.Exists(exportFilePath))
                        {
                            try
                            {
                                //Try to open the file and let windows decide how to open it.
                                System.Diagnostics.Process.Start(exportFilePath);
                            }
                            catch
                            {
                                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu !");
            }
        }

        private void Frm_BaoCaoSoKhamBenh_30372_Load(object sender, EventArgs e)
        {
            txtDenNgay.Text = DateTime.Now.ToShortDateString();
            txtTuNgay.Text = DateTime.Now.ToShortDateString();
            radNT.SelectedIndex = 0;
            cbDoiTuong.SelectedIndex = 0;
        }

        private void gridView1_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            //e.Column.AppearanceHeader.BackColor = Color.Red;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if(e.Column.Name=="STT")
            //{
            //    e.DisplayText =Convert.ToString(e.RowHandle + 1);
            //}
        }
    }
}