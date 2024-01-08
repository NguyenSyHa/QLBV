using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.TraCuu
{
    public partial class us_KhoaNgay : DevExpress.XtraEditors.XtraUserControl
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        List<KhoaDL> _lkhoa = new List<KhoaDL>();
        public us_KhoaNgay()
        {
            InitializeComponent();
            loadSource();
            _lkhoa = (from kdl in _data.KhoaDLs select kdl).OrderBy(p => p.NgayKhoa).ToList();
            string a2 = "01/01/" + System.DateTime.Now.Year.ToString();
            dtTimTuNgay.DateTime = Convert.ToDateTime(a2);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            TimKiem();
        }

        private void loadSource()
        {
            List<trangthaiNT> _lTTNT = new List<trangthaiNT>();
            List<trangthaiNgT> _lTTNgT = new List<trangthaiNgT>();
            List<trangthaiDC> _lTTDC = new List<trangthaiDC>();
            _lTTNT.Add(new trangthaiNT { TrangThaiNT = "Đã khóa", KhoaNT = true });
            _lTTNT.Add(new trangthaiNT { TrangThaiNT = "Chưa khóa", KhoaNT = false });
            _lTTNgT.Add(new trangthaiNgT { TrangThaiNgT = "Đã khóa", KhoaNgT = true });
            _lTTNgT.Add(new trangthaiNgT { TrangThaiNgT = "Chưa khóa", KhoaNgT = false });
            _lTTDC.Add(new trangthaiDC { TrangThaiDC = "Đã khóa", KhoaDC = true });
            _lTTDC.Add(new trangthaiDC { TrangThaiDC = "Chưa khóa", KhoaDC = false });

            var canboes = (from a in _data.CanBoes select new { MaCB = a.MaCB, a.TenCB }).ToList();
            lupCanBo.DataSource = canboes;
            lupKhoaDC.DataSource = _lTTDC.ToList();
            lupNoiTru.DataSource = _lTTNT.ToList();
            lupNgoaiTru.DataSource = _lTTNgT.ToList();
            var khoes = _data.KPhongs.Where(p => p.PLoai == "Khoa dược").Select(p => new { MaKho = p.MaKP, TenKho = p.TenKP }).ToList();
            lupKho.DataSource = khoes;
            lupKP.Properties.DataSource = khoes;
        }
        public void TimKiem()
        {
            _lkhoa = (from kdl in _data.KhoaDLs select kdl).OrderBy(p => p.NgayKhoa).ToList();
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (rdPLoaiKhoa.SelectedIndex == 0) // tìm kiếm theo viện phí
            {
                if (rdNNgTSua.SelectedIndex == 0)
                {
                    var q =
                        (from d in _lkhoa.Where(p => p.NgayKhoa >= _dttu).Where(p => p.NgayKhoa <= _dtden).Where(p => p.MaKho == null)
                         where d.NgoaiTru != null
                         select new
                         {
                             d.ID,
                             MaCB = d.MaCB,
                             d.NgayKhoa,
                             KhoaNT = d.NoiTru,
                             KhoaNgT = d.NgoaiTru,
                             KhoaDC = d.KhoaDC,
                             MaKho = d.MaKho,
                             d.PLoai
                         }).ToList().OrderBy(p => p.NgayKhoa);
                    grcNhapD.DataSource = q.ToList();
                }
                else
                {
                    var q =
                        (from d in _lkhoa.Where(p => p.NgayKhoa >= _dttu).Where(p => p.NgayKhoa <= _dtden).Where(p => p.MaKho == null)
                         where d.NoiTru != null
                         select new
                         {
                             d.ID,
                             MaCB = d.MaCB,
                             d.NgayKhoa,
                             KhoaNT = d.NoiTru,
                             KhoaNgT = d.NgoaiTru,
                             KhoaDC = d.KhoaDC,
                             MaKho = d.MaKho,
                             d.PLoai
                         }).ToList().OrderBy(p => p.NgayKhoa);
                    grcNhapD.DataSource = q.ToList();
                }
            }
            else
            {
                string makp = "";
                if (lupKP.EditValue != null)
                {
                    makp = lupKP.EditValue.ToString();
                }
                var q = (from d in _lkhoa.Where(p => p.NgayKhoa >= _dttu).Where(p => p.NgayKhoa <= _dtden).Where(p => p.MaKho != null)
                         where (d.MaKho == makp && d.PLoai == 1)
                         select new
                         {
                             d.ID,
                             MaCB = d.MaCB,
                             d.NgayKhoa,
                             KhoaNT = d.NoiTru,
                             KhoaNgT = d.NgoaiTru,
                             KhoaDC = d.KhoaDC,
                             MaKho = d.MaKho,
                             d.PLoai
                         }).ToList().OrderBy(p => p.NgayKhoa);
                grcNhapD.DataSource = q.ToList();
            }
        }

        private void us_TCKhoaCT_Load(object sender, EventArgs e)
        {
            var kp = _data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).ToList();
            if (kp.Count > 0)
            {
                if (kp.First().PLoai == "Khoa dược")
                {
                    lupKP.Visible = true;
                    rdNNgTSua.Visible = false;
                    rdPLoaiKhoa.Enabled = false;
                    lupKP.EditValue = DungChung.Bien.MaKP;
                    lupKP.Enabled = false;
                    rdPLoaiKhoa.SelectedIndex = 1;
                }
                if (kp.First().PLoai == "Kế toán")
                {
                    rdNNgTSua.Visible = true;
                    lupKP.Visible = false;
                    rdPLoaiKhoa.Enabled = false;
                    rdPLoaiKhoa.SelectedIndex = 0;
                }
                if (kp.First().PLoai == "Admin")
                {
                    rdNNgTSua.Visible = true;
                    lupKP.Visible = true;
                }
            }
            TimKiem();
            var cb = _data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).Select(p => p.Khoa).ToList();
            if (cb.Count > 0 && cb.First() != null && cb.First().Value == 1)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }


        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void cboPLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool status = true;
            DateTime ngaykhoaForm = Convert.ToDateTime(dateNgayKhoa.EditValue.ToString());
            if (rdPLoaiKhoa.SelectedIndex == 0) // Chọn khóa dữ liệu VP
            {
                if (rdNNgTSua.SelectedIndex == 0)// Chọn Ngoại trú
                {
                    if (rdKhoaNgT.SelectedIndex == 0) // Chọn Khóa Ngày
                    {
                        var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm).Where(p => p.NgoaiTru != null && p.PLoai == 0).ToList();
                        if (kt.Count > 0)
                        {
                            status = false;
                        }
                        else
                            status = true;
                        if (status)
                        {
                            KhoaDL moi = new KhoaDL();
                            moi.NgayKhoa = dateNgayKhoa.DateTime;
                            moi.NgoaiTru = chkTT.Checked;
                            moi.Status = 0;
                            moi.PLoai = 0;
                            moi.MaCB = DungChung.Bien.MaCB;
                            _data.KhoaDLs.Add(moi);
                            _data.SaveChanges();
                            MessageBox.Show("Thêm mới thành công");
                        }
                        else
                        {
                            if (DungChung.Bien.MaCB == kt.First().MaCB || _data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).First().PLoai == "Admin")
                            {
                                KhoaDL sua = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm && p.NgoaiTru != null && p.PLoai == 0).First();
                                sua.NgoaiTru = chkTT.Checked;
                                sua.Status = 0;
                                sua.PLoai = 0;
                                sua.NgoaiTru = chkTT.Checked;
                                sua.MaCB = DungChung.Bien.MaCB;
                                _data.SaveChanges();
                                MessageBox.Show("Sửa thành công");
                            }
                            else
                            {
                                if (_data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB) != null)
                                {
                                    string tenCB = _data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB).First();
                                    MessageBox.Show("Chỉ người khóa" + tenCB + " mới có quyền sửa");
                                }

                            }
                        }
                    }
                    else // Chọn khóa tháng
                    {
                        int month = dateNgayKhoa.DateTime.Month;
                        int year = dateNgayKhoa.DateTime.Year;
                        int songay = DateTime.DaysInMonth(year, month);
                        for (int i = 1; i <= songay; i++)
                        {
                            DateTime ngaykhoa = Convert.ToDateTime(i + "/" + month + "/" + year);
                            var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoa && p.NgoaiTru != null && p.PLoai == 0).ToList();
                            if (kt.Count == 0)
                            {
                                KhoaDL moi = new KhoaDL();
                                moi.NgayKhoa = ngaykhoa;
                                moi.NgoaiTru = chkTT.Checked;
                                moi.Status = 0;
                                moi.PLoai = 0;
                                moi.MaCB = DungChung.Bien.MaCB;
                                _data.KhoaDLs.Add(moi);
                                _data.SaveChanges();
                            }
                        }
                        MessageBox.Show("Khóa Viện phí ngoại trú tháng "+month+ " thành công!. ");
                    }
                }
                else // Chọn nội trú
                {
                    if (rdKhoaNgT.SelectedIndex == 0) // Khóa Ngày
                    {
                        var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm).Where(p => p.NoiTru != null).ToList();
                        if (kt.Count > 0)
                        {
                            status = false;
                        }
                        else
                            status = true;
                        if (status)
                        {
                            KhoaDL moi = new KhoaDL();
                            moi.NgayKhoa = dateNgayKhoa.DateTime;
                            moi.NoiTru = chkTT.Checked;
                            moi.Status = 0;
                            moi.PLoai = 0;
                            moi.MaCB = DungChung.Bien.MaCB;
                            _data.KhoaDLs.Add(moi);
                            _data.SaveChanges();
                            MessageBox.Show("Thêm mới thành công");
                        }
                        else
                        {
                            if (DungChung.Bien.MaCB == kt.First().MaCB || _data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).First().PLoai == "Admin")
                            {
                                KhoaDL sua = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm && p.NoiTru != null && p.PLoai == 0).First();
                                sua.Status = 0;
                                sua.PLoai = 0;
                                sua.NoiTru = chkTT.Checked;
                                sua.MaCB = DungChung.Bien.MaCB;
                                _data.SaveChanges();
                                MessageBox.Show("Sửa thành công");
                            }
                            else
                            {
                                if (_data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB) != null)
                                {
                                    string tenCB = _data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB).First();
                                    MessageBox.Show("Chỉ người khóa" + tenCB + " mới có quyền sửa");
                                }
                            }
                        }
                    }
                    else // Khóa tháng
                    {
                        int month = dateNgayKhoa.DateTime.Month;
                        int year = dateNgayKhoa.DateTime.Year;
                        int songay = DateTime.DaysInMonth(year, month);
                        for (int i = 1; i <= songay; i++)
                        {
                            DateTime ngaykhoa = Convert.ToDateTime(i + "/" + month + "/" + year);
                            var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoa && p.NoiTru != null && p.PLoai == 0).ToList();
                            if (kt.Count == 0)
                            {
                                KhoaDL moi = new KhoaDL();
                                moi.NgayKhoa = ngaykhoa;
                                moi.NoiTru = chkTT.Checked;
                                moi.Status = 0;
                                moi.PLoai = 0;
                                moi.MaCB = DungChung.Bien.MaCB;
                                _data.KhoaDLs.Add(moi);
                                _data.SaveChanges();
                            }
                        }
                        MessageBox.Show("Khóa Viện phí nội trú tháng " + month + " thành công!. ");
                    }
                }
            } // Nếu chọn Dược
            else
            {
                string tenKho = "";
                if (lupKP.EditValue != null || lupKP.EditValue.ToString() != "")
                {
                    tenKho = lupKP.EditValue.ToString();
                }
                if (rdKhoaNgT.SelectedIndex == 0 ) // Khóa ngày
                {
                    var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm).Where(p => p.MaKho == tenKho && p.PLoai == 1).ToList();
                    if (kt.Count > 0)
                    {
                        status = false;
                    }
                    else
                        status = true;
                    if (status)
                    {
                        KhoaDL moi = new KhoaDL();
                        moi.NgayKhoa = dateNgayKhoa.DateTime.Date;
                        moi.KhoaDC = chkTT.Checked;
                        moi.Status = 0;
                        moi.PLoai = 1;
                        moi.MaKho = tenKho;
                        moi.MaCB = DungChung.Bien.MaCB;
                        _data.KhoaDLs.Add(moi);
                        _data.SaveChanges();
                        MessageBox.Show("Thêm mới thành công");
                    }
                    else
                    {
                        if (DungChung.Bien.MaCB == kt.First().MaCB || _data.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).First().PLoai == "Admin")
                        {
                            KhoaDL sua = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoaForm && p.MaKho == tenKho && p.PLoai == 1).First();
                            sua.Status = 0;
                            sua.PLoai = 1;
                            sua.KhoaDC = chkTT.Checked;
                            sua.MaCB = DungChung.Bien.MaCB;
                            sua.MaKho = tenKho;
                            _data.SaveChanges();
                            MessageBox.Show("Sửa thành công");
                        }
                        else
                        {
                            if (_data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB) != null)
                            {
                                string tenCB = _data.CanBoes.Where(p => p.MaCB == kt.First().MaCB).Select(p => p.TenCB).First();
                                MessageBox.Show("Chỉ người khóa : " + tenCB + " mới có quyền sửa");
                            }
                        }
                    }
                }
                else // Khóa tháng
                {
                    int month = dateNgayKhoa.DateTime.Month;
                    int year = dateNgayKhoa.DateTime.Year;
                    int songay = DateTime.DaysInMonth(year, month);
                    for (int i = 1; i <= songay; i++)
                    {
                        DateTime ngaykhoa = Convert.ToDateTime(i + "/" + month + "/" + year);
                        var kt = _data.KhoaDLs.Where(p => p.NgayKhoa == ngaykhoa && p.KhoaDC != null && p.PLoai == 1).ToList();
                        if (kt.Count == 0)
                        {
                            KhoaDL moi = new KhoaDL();
                            moi.NgayKhoa = ngaykhoa;
                            moi.KhoaDC = chkTT.Checked;
                            moi.Status = 0;
                            moi.PLoai = 1;
                            moi.MaKho = tenKho;
                            moi.MaCB = DungChung.Bien.MaCB;
                            _data.KhoaDLs.Add(moi);
                            _data.SaveChanges();
                        }
                    }
                    MessageBox.Show("Khóa dược tháng " + month + " "+lupKP.Text+" thành công!. ");
                }
            }
            loadSource();
            TimKiem();
        }


        private void btnTaongay_Click(object sender, EventArgs e)
        {
            DateTime _dt = System.DateTime.Now;
            DateTime _dt2 = System.DateTime.Now;
            _dt = Convert.ToDateTime("01/01/2015");
            _dt2 = Convert.ToDateTime("01/01/2015");
            for (int i = 0; i <= 3; i++)
            {
                var them = new DateTime(2015, 01, 01);
                MessageBox.Show(_dt.ToString());

            }
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        private class trangthaiNT
        {
            string tthaiNT;
            bool khoaNT;
            public string TrangThaiNT
            {
                set { tthaiNT = value; }
                get { return tthaiNT; }
            }
            public bool KhoaNT
            {
                set { khoaNT = value; }
                get { return khoaNT; }
            }
        }
        private class trangthaiNgT
        {
            string tthaiNgt;
            bool khoaNgT;
            public string TrangThaiNgT
            {
                set { tthaiNgt = value; }
                get { return tthaiNgt; }
            }
            public bool KhoaNgT
            {
                set { khoaNgT = value; }
                get { return khoaNgT; }
            }
        }
        private class trangthaiDC
        {
            string tthaiDC;
            bool khoaDC;
            public string TrangThaiDC
            {
                set { tthaiDC = value; }
                get { return tthaiDC; }
            }
            public bool KhoaDC
            {
                set { khoaDC = value; }
                get { return khoaDC; }
            }
        }
        private void grvNhapD_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }

        private void dateNgayKhoa_EditValueChanged(object sender, EventArgs e)
        {
            //loadKhoaDL();
        }

        private void loadKhoaDL()
        {
            List<KhoaDL> kt = _data.KhoaDLs.Where(p => p.NgayKhoa == dateNgayKhoa.DateTime).ToList();
            if (kt.Count > 0)
            {
                if (rdPLoaiKhoa.SelectedIndex == 0)// Nếu chọn viện phí
                {
                    if (rdNNgTSua.SelectedIndex == 0) // Nếu chọn ngoại trú
                    {
                        kt = kt.Where(p => p.PLoai == 0 && p.NgoaiTru != null).ToList();
                        if (kt.Count > 0)
                        {
                            if (kt.First().NgoaiTru == true)
                            {
                                chkTT.Checked = true;
                            }
                            else
                            {
                                chkTT.Checked = false;
                            }
                        }
                    }
                    else // Nếu chọn nội trú
                    {
                        kt = kt.Where(p => p.PLoai == 0 && p.NoiTru != null).ToList();
                        if (kt.Count > 0)
                        {
                            if (kt.First().NoiTru == true)
                            {
                                chkTT.Checked = true;
                            }
                            else
                            {
                                chkTT.Checked = false;
                            }
                        }
                    }
                }
                else // Nếu chọn Dược
                {
                    if (lupKP.EditValue != null)
                    {
                        kt = kt.Where(p => p.KhoaDC != null && p.MaKho == lupKP.EditValue.ToString() && p.PLoai == 1).ToList();
                        if (kt.Count > 0)
                        {
                            if (kt.First().KhoaDC == true)
                            {
                                chkTT.Checked = kt.First().KhoaDC.Value;
                            }
                        }
                        else
                        {
                            chkTT.Checked = false;
                        }
                    }
                    else
                    {
                        kt = kt.Where(p => p.KhoaDC != null && p.MaKho == "" && p.PLoai == 1).ToList();
                    }

                }
                var k = kt.Select(d => new
                {
                    d.ID,
                    MaCB = d.MaCB,
                    d.NgayKhoa,
                    KhoaNT = d.NoiTru,
                    KhoaNgT = d.NgoaiTru,
                    KhoaDC = d.KhoaDC,
                    MaKho = d.MaKho,
                    d.PLoai
                }).OrderBy(p => p.NgayKhoa).ToList();
                grcNhapD.DataSource = k;
            }
            else
            {
                chkTT.Checked = false;
            }
        }

        private void grvNhapD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhapD.GetFocusedRowCellValue(colNgayKhoa) != null && grvNhapD.GetFocusedRowCellValue(colNgayKhoa).ToString() != "")
            {
                if (grvNhapD.GetFocusedRowCellValue(colKhoaDC) != null && grvNhapD.GetFocusedRowCellValue(colKhoaDC) != "")
                {
                    rdPLoaiKhoa.SelectedIndex = 1;
                    chkTT.Checked = grvNhapD.GetFocusedRowCellValue(colKhoaDC).ToString() == "True" ? true : false;
                    if (grvNhapD.GetFocusedRowCellValue(colKho) != null && grvNhapD.GetFocusedRowCellValue(colKho).ToString() != "")
                        lupKP.EditValue = grvNhapD.GetFocusedRowCellValue(colKho).ToString();
                    else
                        lupKP.EditValue = "";
                }
                if (grvNhapD.GetFocusedRowCellValue(colNoiTru) != null && grvNhapD.GetFocusedRowCellValue(colNoiTru).ToString() != "")
                {
                    rdPLoaiKhoa.SelectedIndex = 0;
                    chkTT.Checked = grvNhapD.GetFocusedRowCellValue(colNoiTru).ToString() == "True" ? true : false;
                }
                if (grvNhapD.GetFocusedRowCellValue(colNgTru) != null && grvNhapD.GetFocusedRowCellValue(colNgTru).ToString() != "")
                {
                    rdPLoaiKhoa.SelectedIndex = 0;
                    chkTT.Checked = grvNhapD.GetFocusedRowCellValue(colNgTru).ToString() == "True" ? true : false;
                }
                dateNgayKhoa.DateTime = Convert.ToDateTime(grvNhapD.GetFocusedRowCellValue(colNgayKhoa));
            }
            else
            {
                chkTT.Checked = false;
            }
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lupKhoTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void rdNNgT_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void grvNhapD_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhapD_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 1));
        }

        private void rdNNgTSua_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadKhoaDL();
        }

        private void lupKP_EditValueChanged(object sender, EventArgs e)
        {
            loadKhoaDL();
        }

        private void rdPLoaiKhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (rdPLoaiKhoa.SelectedIndex == 0)
            {
                rdNNgTSua.Visible = true;
                lupKP.Visible = false;
            }
            else
            {
                rdNNgTSua.Visible = false;
                lupKP.Visible = true;
            }
            loadKhoaDL();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

    }
}
