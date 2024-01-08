using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using static QLBV.FormThamSo.frm_benhnhanxuatduoc;
using System.IO;
using QLBV.FormThamSo;

namespace QLBV.ChucNang
{
    public partial class frm_KQmau : DevExpress.XtraEditors.XtraForm
    {
        public frm_KQmau()
        {
            InitializeComponent();
        }
        int _madv = 0;
        string _tabThucHien = "";
        public frm_KQmau(int madv)
        {
            InitializeComponent();
            _madv = madv;
        }
        public frm_KQmau(int madv, string tabThuHien)
        {
            InitializeComponent();
            _madv = madv;
            _tabThucHien = tabThuHien;
        }
        public delegate void _getstring(string chuoi1, string chuoi2, string chuoi3);
        
        public _getstring GetData;
        

        

        private void btnDongY_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            if (DungChung.Bien.MaBV == "30012")
                op.Filter = "BMP(*.bmp)| *.bmp|JPEG (*.jpg)|*.jpg";
            else
                op.Filter = "JPEG (*.jpg)| *.jpg|BMP(*.bmp)| *.bmp";
            string fileName = string.Empty;
            int IdCLS = Convert.ToInt32(grvKQmau.GetFocusedRowCellValue(colMaDV));
            if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
            {
                fileName = op.FileName;
                string ex = System.IO.Path.GetExtension(op.FileName);
                string _tenfileanh = string.Empty;
                //dung310516
                //if(DungChung.Bien.MaBV == "30012")
                //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                //else
                //     _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                for (int i = 1; i <= arrDuongDan.Length; i++)
                {
                    switch (i)
                    {
                        case 1:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh = layTenFileAnh(fileName, _tenfileanh);
                            ptA1.Text = arrDuongDan[0];
                            break;
                        case 2:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh2 = layTenFileAnh(fileName, _tenfileanh);
                            ptA2.Text = arrDuongDan[1];
                            break;
                        case 3:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh3 = layTenFileAnh(fileName, _tenfileanh);
                            ptA3.Text = arrDuongDan[2];
                            break;
                        case 4:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh4 = layTenFileAnh(fileName, _tenfileanh);
                            ptA4.Text = arrDuongDan[3];
                            break;
                        case 5:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh5 = layTenFileAnh(fileName, _tenfileanh);
                            ptA5.Text = arrDuongDan[4];
                            break;
                        case 6:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh6 = layTenFileAnh(fileName, _tenfileanh);
                            ptA6.Text = arrDuongDan[5];
                            break;
                        case 7:
                            _tenfileanh = _mabn + "_" + IdCLS + "_" + i + ex;
                            _fileanh7 = layTenFileAnh(fileName, _tenfileanh);
                            ptA7.Text = arrDuongDan[6];
                            break;
                    }
                }
            }
            if (_tabThucHien == ("tabNSTMH"))
            {
                string[] str = new string[] { mmTMHTaiPhai.Text, mmTMHTaiTrai.Text, mmTMHMuiPhai.Text, mmTMHMuiTrai.Text, mmTMHVom.Text, mmTMHHong.Text, mmTMHThanhQuan.Text, ptA1.Text, ptA2.Text, ptA3.Text, ptA4.Text, ptA5.Text, ptA6.Text, ptA7.Text };
                string str2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', str);
                GetData(str2, mmKetLuan.Text, mmLoidan.Text);
            }
            //else if (_tabThucHien == ("PageDientim"))
            //{
                //string[] kq = new string[] { memoKQ.Text };
                //string ketqua = QLBV_Library.QLBV_Ham.LuuChuoi(';', kq);
                //GetData(ketqua, mmLoidan.Text, mmKetLuan.Text);
            //}
            else
                GetData(memoKQ.Text, mmKetLuan.Text, mmLoidan.Text);
            this.Dispose();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        #region KQCLSmau
        private class KQCLSmau
        {
            public bool Chon { get; set; }
            public string MaKQ { get; set; }
            public string TenKQ { get; set; }
            public int? MaDV { get; set; }
            public string TenRG { get; set; }
            public string KetLuan { get; set; }
            public string LoiDan { get; set; }
        }
        #endregion
        List<KQCLSmau> dsKQ = new List<KQCLSmau>();
        private void frm_KQmau_Load(object sender, EventArgs e)
        {
            dsKQ.Clear();
            if (_tabThucHien == ("tabNSTMH"))
            {
                panelTMH.Visible = true;
            }
            else
                panelTMH.Visible = false;
            if (_tabThucHien == "TTPT")
            {
                groupControl4.Text = "Cách thức phẫu thuật";
                groupControl5.Text = "Phương pháp vô cảm";

            }
            if (DungChung.Bien.MaBV != "14018")
            {
                var ds = _data.KQMaus.Where(p => p.MaDV == _madv).ToList();
                if (ds.Count > 0)
                {
                    foreach (var item in ds)
                    {
                        KQCLSmau kq = new KQCLSmau();
                        kq.Chon = false;
                        kq.KetLuan = item.KetLuan;
                        kq.LoiDan = item.LoiDan;
                        kq.MaDV = item.MaDV;
                        kq.MaKQ = item.MaKQ;
                        kq.TenKQ = item.TenKQ;
                        kq.TenRG = item.TenRG;
                        dsKQ.Add(kq);
                    }
                    grcKQmau.DataSource = dsKQ;
                }
            }
            
            else
            {
                var ds = _data.KQMaus.ToList();
                if (ds.Count > 0)
                {
                    foreach (var item in ds)
                    {
                        KQCLSmau kq = new KQCLSmau();
                        kq.Chon = false;
                        kq.KetLuan = item.KetLuan;
                        kq.LoiDan = item.LoiDan;
                        kq.MaDV = item.MaDV;
                        kq.MaKQ = item.MaKQ;
                        kq.TenKQ = item.TenKQ;
                        kq.TenRG = item.TenRG;
                        dsKQ.Add(kq);
                    }
                    grcKQmau.DataSource = dsKQ;
                }
            }


        }

        private void grvKQmau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (_tabThucHien == ("tabNSTMH"))
            //{
            //    if (grvKQmau.GetFocusedRowCellDisplayText(colKQmau) != null && grvKQmau.GetFocusedRowCellDisplayText(colKQmau).ToString() != "")
            //    {
            //        string strKQ = grvKQmau.GetFocusedRowCellDisplayText(colKQmau).ToString();
            //        string[] str = strKQ.Split('|');
            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    mmTMHTaiPhai.Text = str[i];
            //                    break;
            //                case 1:
            //                    mmTMHTaiTrai.Text = str[i];
            //                    break;
            //                case 2:
            //                    mmTMHMuiPhai.Text = str[i];
            //                    break;
            //                case 3:
            //                    mmTMHMuiTrai.Text = str[i];
            //                    break;
            //                case 4:
            //                    mmTMHVom.Text = str[i];
            //                    break;
            //                case 5:
            //                    mmTMHHong.Text = str[i];
            //                    break;
            //                case 6:
            //                    mmTMHThanhQuan.Text = str[i];
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //    }
            //    else
            //        memoKQ.Text = "";
            //}
            //else
            //{
            //    if (grvKQmau.GetFocusedRowCellDisplayText(colKQmau) != null && grvKQmau.GetFocusedRowCellDisplayText(colKQmau).ToString() != "")
            //    {
            //        memoKQ.Text = grvKQmau.GetFocusedRowCellDisplayText(colKQmau).ToString();
            //    }
            //    else
            //        memoKQ.Text = "";
            //}

            //if (grvKQmau.GetFocusedRowCellDisplayText(colKetLuan) != null)
            //    mmKetLuan.Text = grvKQmau.GetFocusedRowCellDisplayText(colKetLuan).ToString();
            //else
            //    mmKetLuan.Text = "";
            //if (grvKQmau.GetFocusedRowCellDisplayText(colLoiDan) != null)
            //    mmLoidan.Text = grvKQmau.GetFocusedRowCellDisplayText(colLoiDan).ToString();
            //else
            //    mmLoidan.Text = "";

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaKQ.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã kết quả");
                txtMaKQ.Focus();
                return false;
            }

            return true;
        }

        #endregion
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (KTLuu())
            //{
            //    switch (_TTLuu)
            //    {
            //        case 1:
            //            _Makqm = txtMaKQ.Text.Trim();
            //            var ma = dataContext.KQMaus.Where(p => p.MaKQ== (_Makqm)).ToList();
            //            if (ma.Count > 0)
            //            {
            //                MessageBox.Show("Mã kết quả mẫu đã có, vui lòng nhập mã khác");
            //            }
            //            else
            //            {
            //                KQMau kqm = new KQMau();
            //                kqm.MaKQ = txtMaKQ.Text;
            //                kqm.MaDV = lupMaDV.EditValue.ToString();
            //                kqm.TenKQ = txtTenKQ.Text;
            //                kqm.TenRG = txtTenRG.Text;
            //                kqm.KetLuan = txtKetLuan.Text;
            //                kqm.LoiDan = txtLoiDan.Text;
            //                dataContext.KQMaus.Add(kqm);
            //                dataContext.SaveChanges();
            //                enableControl(false);
            //                MessageBox.Show("Lưu thành công!");
            //            }
            //            break;

            //        case 2:
            //            if (!string.IsNullOrEmpty(txtMaKQ.Text))
            //            {
            //                string makq = txtMaKQ.Text;
            //                KQMau kqmsua = dataContext.KQMaus.Single(p => p.MaKQ== (makq));
            //                kqmsua.MaKQ = txtMaKQ.Text;
            //                kqmsua.MaDV = lupMaDV.EditValue.ToString();
            //                kqmsua.TenKQ = txtTenKQ.Text;
            //                kqmsua.TenRG = txtTenRG.Text;
            //                kqmsua.KetLuan = txtKetLuan.Text;
            //                kqmsua.LoiDan = txtLoiDan.Text;
            //                dataContext.SaveChanges();
            //                MessageBox.Show("Lưu thành công!");
            //                enableControl(false);
            //            }
            //            break;
            //    }
            //    var _lkqm = dataContext.KQMaus.OrderBy(p => p.TenKQ).ToList();
            //    grcKQMau.DataSource = _lkqm.ToList();
            //}
        }

        private void grvKQmau_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKQmau.GetFocusedRowCellDisplayText(colMaKQmau) != null && grvKQmau.GetFocusedRowCellDisplayText(colMaKQmau).ToString() != "")
                {
                    string maKQ = grvKQmau.GetFocusedRowCellDisplayText(colMaKQmau).ToString();
                    var item = dsKQ.Where(p => p.MaKQ == maKQ).ToList();
                    if (item.Count > 0)
                    {
                        if (item.FirstOrDefault().Chon == true)
                        {
                            item.FirstOrDefault().Chon = false;
                        }
                        else
                        {
                            item.FirstOrDefault().Chon = true;
                        }
                    }
                    string kqmau = string.Empty;
                    string taiphai = string.Empty;
                    string taitrai = string.Empty;
                    string muiphai = string.Empty;
                    string muitrai = string.Empty;
                    string vom = string.Empty;
                    string hong = string.Empty;
                    string thanhquan = string.Empty;
                    string ptA_1 = string.Empty;
                    string ptA_2 = string.Empty;
                    string ptA_3 = string.Empty;
                    string ptA_4 = string.Empty;
                    string ptA_5 = string.Empty;
                    string ptA_6 = string.Empty;
                    string ptA_7 = string.Empty;
                    string ketluan = "";
                    string loidan = "";
                    string makq = "";
                    string tenrg = "";
                    foreach (var a in dsKQ)
                    {
                        if (a.Chon == true)
                        {
                            //a.Chon = true;
                            if (_tabThucHien == ("tabNSTMH"))
                            {
                                if (!string.IsNullOrEmpty(a.TenKQ))
                                {
                                    string strKQ = a.TenKQ;
                                    string[] str = strKQ.Split('|');
                                    for (int i = 0; i < str.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                taiphai += str[i] + ",";
                                                break;
                                            case 1:
                                                taitrai += str[i] + ",";
                                                break;
                                            case 2:
                                                muiphai += str[i] + ",";
                                                break;
                                            case 3:
                                                muitrai += str[i] + ",";
                                                break;
                                            case 4:
                                                vom += str[i] + ",";
                                                break;
                                            case 5:
                                                hong += str[i] + ",";
                                                break;
                                            case 6:
                                                thanhquan += str[i] + ",";
                                                break;
                                            case 7:
                                                ptA_1 += str[i] + ",";
                                                break;
                                            case 8:
                                                ptA_2 += str[i] + ",";
                                                break;
                                            case 9:
                                                ptA_3 += str[i] + ",";
                                                break;
                                            case 10:
                                                ptA_4 += str[i] + ",";
                                                break;
                                            case 11:
                                                ptA_5 += str[i] + ",";
                                                break;
                                            case 12:
                                                ptA_6 += str[i] + ",";
                                                break;
                                            case 13:
                                                ptA_7 += str[i] + ",";
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                    memoKQ.Text = "";
                                makq += a.MaKQ;
                                tenrg += a.TenRG;
                                ketluan += a.KetLuan;
                                loidan += " " + a.LoiDan;
                            }
                            else
                            {
                                kqmau += a.TenKQ;
                                ketluan += a.KetLuan;
                                loidan += " " + a.LoiDan;
                            }
                        }
                        else
                        {
                            if (_tabThucHien == ("tabNSTMH"))
                            {
                                if (a.TenKQ != null && a.TenKQ != "")
                                {
                                    string strKQ = a.TenKQ;
                                    string[] str = strKQ.Split('|');
                                    for (int i = 0; i < str.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                taiphai += "";
                                                break;
                                            case 1:
                                                taitrai += "";
                                                break;
                                            case 2:
                                                muiphai += "";
                                                break;
                                            case 3:
                                                muitrai += "";
                                                break;
                                            case 4:
                                                vom += "";
                                                break;
                                            case 5:
                                                hong += "";
                                                break;
                                            case 6:
                                                thanhquan += "";
                                                break;
                                            case 7:
                                                ptA_1 += "";
                                                break;
                                            case 8:
                                                ptA_2 += "";
                                                break;
                                            case 9:
                                                ptA_3 += "";
                                                break;
                                            case 10:
                                                ptA_4 += "";
                                                break;
                                            case 11:
                                                ptA_5 += "";
                                                break;
                                            case 12:
                                                ptA_6 += "";
                                                break;
                                            case 13:
                                                ptA_7 += "";
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                    memoKQ.Text = "";
                            }
                            else
                            {
                                kqmau += "";
                                ketluan += " ";
                                loidan += " ";
                            }
                        }
                    }
                    if (_tabThucHien == "tabNSTMH")
                    {
                        mmTMHTaiPhai.Text = taiphai;
                        mmTMHTaiTrai.Text = taitrai;
                        mmTMHMuiPhai.Text = muiphai;
                        mmTMHMuiTrai.Text = muitrai;
                        mmTMHVom.Text = vom;
                        mmTMHHong.Text = hong;
                        mmTMHThanhQuan.Text = thanhquan;
                        mmKetLuan.Text = ketluan;
                        mmLoidan.Text = loidan;
                        txtMaKQ.Text = makq;
                        txtTenRG.Text = tenrg;
                        ptA1.Text = ptA_1 + ".jpg";
                        ptA2.Text = ptA_2 + ".jpg";
                        ptA3.Text = ptA_3 + ".jpg";
                        ptA4.Text = ptA_4 + ".jpg";
                        ptA5.Text = ptA_5 + ".jpg";
                        ptA6.Text = ptA_6 + ".jpg";
                        ptA7.Text = ptA_7 + ".jpg";

                    }
                    else
                    {
                        memoKQ.Text = kqmau;
                        mmKetLuan.Text = ketluan;
                        mmLoidan.Text = loidan;
                    }
                }
            }
        }

        string _fileanh = "";
        string _fileanh2 = "";
        string _fileanh3 = "";
        string _fileanh4 = "";
        string _fileanh5 = "";
        string _fileanh6 = "";
        string _fileanh7 = "";
        string _fileanh8 = "";
        string Duongdandasua = "";
        int trangthaiLuu = 0;
        int _mabn = 0;
        public string[] arrDuongDan = new string[8];
        List<string> listAnh = new List<string>();

        private void sbtChon1_Click(object sender, EventArgs e)
        {
            chonAnh(ptA1, 1);
        }

        private void sbtChon2_Click(object sender, EventArgs e)
        {
            chonAnh(ptA2, 2);
        }

        private void sbtChon3_Click(object sender, EventArgs e)
        {
            chonAnh(ptA3, 3);
        }

        private void sbtChon4_Click(object sender, EventArgs e)
        {
            chonAnh(ptA4, 4);
        }

        private void sbtChon5_Click(object sender, EventArgs e)
        {
            chonAnh(ptA5, 5);
        }

        private void sbtChon6_Click(object sender, EventArgs e)
        {
            chonAnh(ptA6, 6);
        }

        private void sbtChon7_Click(object sender, EventArgs e)
        {
            chonAnh(ptA7, 7);
        }

        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh, true);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = System.IO.Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        //if(DungChung.Bien.MaBV== "30012")
                        //    a = tenfileanh.Replace(".bmp", i + ".bmp");
                        //else
                        //    a = tenfileanh.Replace(".jpg", i + ".jpg");
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a, true);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        List<CLSct> _CLSct = new List<CLSct>();
        private void chonAnh(PictureEdit pt, int i)
        {
            bool tontai = true;
            if (DungChung.Bien.MaBV == "30372")
            {
                tontai = false;
            }
            else
            {
                switch (i)
                {
                    case 1:
                        if (ptA1.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptA1.Image == null)
                                tontai = false;
                        }
                        break;
                    case 2:
                        if (ptA2.Image == null)
                            tontai = false;
                        break;
                    case 3:
                        if (ptA3.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptA3.Image == null)
                                tontai = false;
                        }
                        break;
                    case 4:
                        if (ptA4.Image == null)
                            tontai = false;
                        break;
                    case 5:
                        if (ptA5.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptA5.Image == null)
                                tontai = false;
                        }
                        break;
                    case 6:
                        if (ptA6.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptA6.Image == null)
                                tontai = false;
                        }
                        break;
                    case 7:
                        if (ptA7.Image == null)
                            tontai = false;

                        break;
                    default:
                        break;
                }
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;
                    if (DungChung.Bien.MaBV == "30012")
                        op.Filter = "BMP(*.bmp)| *.bmp|JPEG (*.jpg)|*.jpg";
                    else
                        op.Filter = "JPEG (*.jpg)| *.jpg|BMP(*.bmp)| *.bmp";
                    int IdCLS = Convert.ToInt32(grvKQmau.GetFocusedRowCellValue(colMaDV));
                    if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            //dung310516
                            //if(DungChung.Bien.MaBV == "30012")
                            //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                            //else
                            //     _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";

                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;

                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                            listAnh.Add(arrDuongDan[i - 1]);
                        }
                    }
                    if (trangthaiLuu == 1) // Nếu là sửa ảnh
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            //dung310516
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            //if (DungChung.Bien.MaBV == "30012")
                            //     _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                            //else
                            //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                            listAnh.Add(arrDuongDan[i - 1]);
                        }
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                if (DungChung.Bien.MaBV == "30012")
                    op.Filter = "BMP(*.bmp)| *.bmp|JPEG (*.jpg)|*.jpg";// "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                else
                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                int IdCLS = Convert.ToInt32(grvKQmau.GetFocusedRowCellValue(colMaDV));
                if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //dung310516
                        //if(DungChung.Bien.MaBV == "30012")
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                        //else
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        listAnh.Add(arrDuongDan[i - 1]);
                    }
                }
                if (trangthaiLuu == 1) // Nếu là sửa ảnh
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //dung310516
                        //if (DungChung.Bien.MaBV == "30012")
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                        //else
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        listAnh.Add(arrDuongDan[i - 1]);
                    }
                }
            }
        }

        private void xoaAnh(PictureEdit pt, List<CLSct> _lCLSCT, int i)
        {
            i = i - 1;
            if (trangthaiLuu == 0)
            {
                pt.Image = null;
                arrDuongDan[i] = "";
            }
            if (trangthaiLuu == 1)
            {
                arrDuongDan[i] = "";
                pt.Image = null;
            }

        }

        private void sbtXoa1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA1, _CLSct, 1);
        }

        private void sbtXoa2_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA2, _CLSct, 2);
        }

        private void sbtXoa3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA3, _CLSct, 3);
        }

        private void sbtXoa4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA4, _CLSct, 4);
        }

        private void sbtXoa5_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA5, _CLSct, 5);
        }

        private void sbtXoa6_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA6, _CLSct, 6);
        }

        private void sbtXoa7_Click(object sender, EventArgs e)
        {
            xoaAnh(ptA7, _CLSct, 7);
        }
    }
}