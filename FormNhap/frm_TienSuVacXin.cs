using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using QLBV.DungChung;
using System.Xml.Linq;
using System.Xml.Serialization;

using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data.Filtering;
using QLBV.ChucNang;

namespace QLBV.FormNhap
{
    public partial class frm_TienSuVacXin : Form
    {
        QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Person> _danhsachBNBHYT = new List<Person>();
        int _noitinh = 1;
        string _ketqua = "";
        int _mabn = 0;
        string _suahs = "";
        int TTLuu = 1, _boPhan = 0;
        public delegate void getString(int mabn);
        public getString getdata;
        int sodk = 0;
        public frm_TienSuVacXin()
        {
            InitializeComponent();
        }
        public frm_TienSuVacXin(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();

        }
        public frm_TienSuVacXin(int ttluu, int mabn)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;

        }
        public frm_TienSuVacXin(int ttluu, int mabn, int _bPhan)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;
            _boPhan = _bPhan;
        }
        public frm_TienSuVacXin(int ttluu, int mabn, string sua)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;
            _suahs = sua;
        }
   
        private void frm_TienSuVacXin_Load(object sender, EventArgs e)
        {
            TienSuTiemChung TSTC = new TienSuTiemChung();
            var tstc2 = DataContext.TienSuTiemChungs.Where(p => p.MaBNhan == _mabn).ToList();
            if (tstc2.Count > 0)
            {
                int i = 0;
                foreach (var item in tstc2)
                {
                    if(i == 0)
                    {
                        lupMui1.Text = item.Loai_Vacxin;
                        dtMui1.Text = item.NgayTiem.ToString();
                        if(!string.IsNullOrEmpty(item.Loai_Vacxin))
                        {
                            checkBox1.Checked = true;
                            lbmui.Text = "1 Mũi";
                        }
                        if (tstc2.Count == 1)
                        {
                            lupMui2.ReadOnly = true;
                            dtMui2.ReadOnly = true;
                            lupMui3.ReadOnly = true;
                            dtMui3.ReadOnly = true;
                            checkBox3.Enabled = false;
                        }
                    }
                    if (i == 1)
                    {
                        lupMui2.Text = item.Loai_Vacxin;
                        dtMui2.Text = item.NgayTiem.ToString();
                        if (!string.IsNullOrEmpty(item.Loai_Vacxin))
                        {
                            checkBox2.Checked = true;
                            lbmui.Text = "2 Mũi";
                        }
                        if (tstc2.Count == 2)
                        {
                            lupMui3.ReadOnly = true;
                            dtMui3.ReadOnly = true;
                        }
                    }
                    if (i == 2)
                    {
                        lupMui3.Text = item.Loai_Vacxin;
                        dtMui3.Text = item.NgayTiem.ToString();
                        if (!string.IsNullOrEmpty(item.Loai_Vacxin))
                        {
                            checkBox3.Checked = true;
                            lbmui.Text = "3 Mũi";
                        }
                    }
                    i++;
                }
            }
            else
            {
                lupMui1.ReadOnly = true;
                lupMui2.ReadOnly = true;
                lupMui3.ReadOnly = true;
                dtMui1.ReadOnly = true;
                dtMui2.ReadOnly = true;
                dtMui3.ReadOnly = true;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                lupMui1.ReadOnly = true;
                dtMui1.ReadOnly = true;
                lupMui1.Text = "";
                dtMui1.Text = "";
                lupMui2.Text = "";
                dtMui2.Text = "";
                lupMui3.Text = "";
                dtMui3.Text = "";
                lbmui.Text = "0 Mũi";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            if (checkBox1.Checked == true)
            {
                lupMui1.ReadOnly = false;
                dtMui1.ReadOnly = false;
                checkBox2.Enabled = true;
                if (checkBox3.Checked == true)
                {
                    lbmui.Text = "3 Mũi";
                }
                else if (checkBox2.Checked == true)
                {
                    lbmui.Text = "2 Mũi";
                }
                else
                {
                    lbmui.Text = "1 Mũi";
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                lupMui2.ReadOnly = true;
                dtMui2.ReadOnly = true;
                lupMui2.Text = "";
                dtMui2.Text = "";
                lupMui3.Text = "";
                dtMui3.Text = "";
                lbmui.Text = "1 Mũi";
                checkBox3.Checked = false;
                checkBox3.Enabled = false;
                lupMui3.ReadOnly = true;
                dtMui3.ReadOnly = true;
            }
            if (checkBox2.Checked == true)
            {
                lupMui2.ReadOnly = false;
                dtMui2.ReadOnly = false;
                checkBox3.Enabled = true;
                if (checkBox3.Checked == true)
                {
                    lbmui.Text = "3 Mũi";
                }
                else
                {
                    lbmui.Text = "2 Mũi";
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                lupMui3.ReadOnly = true;
                dtMui3.ReadOnly = true;
                lupMui3.Text = "";
                dtMui3.Text = "";
                lbmui.Text = "2 Mũi";
            }
            if (checkBox3.Checked == true)
            {
                lupMui3.ReadOnly = false;
                dtMui3.ReadOnly = false;
                lbmui.Text = "3 Mũi";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TienSuTiemChung TSTC = new TienSuTiemChung();
            //Thêm tiền sử tiêm chủng
            var tstc1 = DataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            if (tstc1.Count > 0)
            {
                var tstc2 = DataContext.TienSuTiemChungs.Where(p => p.MaBNhan == _mabn).ToList();
                if (tstc2.Count <= 0)
                {
                    int i = 0;
                    if (!string.IsNullOrEmpty(lupMui1.Text))
                    {
                        TSTC.MaBNhan = _mabn;
                        TSTC.Loai_Vacxin = lupMui1.Text;
                        TSTC.NgayTiem = dtMui1.DateTime;
                        DataContext.TienSuTiemChungs.Add(TSTC);
                        if(DataContext.SaveChanges() > 0)
                        {
                            i = 1;
                            lbmui.Text = "1 Mũi";
                            if (!string.IsNullOrEmpty(lupMui2.Text))
                            {
                                TSTC.MaBNhan = _mabn;
                                TSTC.Loai_Vacxin = lupMui2.Text;
                                TSTC.NgayTiem = dtMui2.DateTime;
                                DataContext.TienSuTiemChungs.Add(TSTC);
                                if (DataContext.SaveChanges() > 0)
                                {
                                    i = 2;
                                    lbmui.Text = "2 Mũi";
                                    if (!string.IsNullOrEmpty(lupMui3.Text))
                                    {
                                        TSTC.MaBNhan = _mabn;
                                        TSTC.Loai_Vacxin = lupMui3.Text;
                                        TSTC.NgayTiem = dtMui3.DateTime;
                                        DataContext.TienSuTiemChungs.Add(TSTC);
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            i = 3;
                                            lbmui.Text = "3 Mũi";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lupMui3.Text = "";
                                dtMui3.Text = "";
                            }
                        }
                    }
                    else
                    {
                        lupMui2.Text = "";
                        lupMui3.Text = "";
                        dtMui2.Text = "";
                        dtMui3.Text = "";
                    }
                    if(i > 0)
                    {
                        MessageBox.Show("Thêm mới tiêm chủng thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới tiêm chủng thất bại!");
                    }
                }
                else
                {
                    //sửa xóa
                    if (tstc2.Count == 3)
                    {
                        int n = 1;
                        foreach (var item in tstc2)
                        {
                            TienSuTiemChung TSTC1 = new TienSuTiemChung();
                            int id = item.ID_TiemChung;
                            TienSuTiemChung tstc3 = DataContext.TienSuTiemChungs.Single(p => p.ID_TiemChung == id);
                            switch (n)
                            {
                                case 1:

                                    if (!string.IsNullOrEmpty(lupMui1.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui1.Text;
                                        tstc3.NgayTiem = dtMui1.DateTime;
                                        lbmui.Text = "1 Mũi";
                                        DataContext.SaveChanges();
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(lupMui2.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui2.Text;
                                        tstc3.NgayTiem = dtMui2.DateTime;
                                        lbmui.Text = "2 Mũi";
                                        DataContext.SaveChanges();
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(lupMui3.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui3.Text;
                                        tstc3.NgayTiem = dtMui3.DateTime;
                                        lbmui.Text = "3 Mũi";
                                        DataContext.SaveChanges();
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                            }
                            n++;
                        }
                        if (n != 1)
                        {
                            MessageBox.Show("Sửa tiêm chủng thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Sửa tiêm chủng thất bại!");
                        }
                    }
                    else if (tstc2.Count == 2)
                    {
                        //thêm mũi 3
                        if (!string.IsNullOrEmpty(lupMui3.Text))
                        {
                            TSTC.MaBNhan = _mabn;
                            TSTC.Loai_Vacxin = lupMui3.Text;
                            TSTC.NgayTiem = dtMui3.DateTime;
                            DataContext.TienSuTiemChungs.Add(TSTC);
                            if (DataContext.SaveChanges() > 0)
                            {
                                lbmui.Text = "3 Mũi";
                            }
                        }
                        //sửa xóa
                        int n = 1;
                        foreach (var item in tstc2)
                        {
                            TienSuTiemChung TSTC1 = new TienSuTiemChung();
                            int id = item.ID_TiemChung;
                            TienSuTiemChung tstc3 = DataContext.TienSuTiemChungs.Single(p => p.ID_TiemChung == id);
                            switch (n)
                            {
                                case 1:

                                    if (!string.IsNullOrEmpty(lupMui1.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui1.Text;
                                        tstc3.NgayTiem = dtMui1.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "1 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(lupMui2.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui2.Text;
                                        tstc3.NgayTiem = dtMui2.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "2 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(lupMui3.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui3.Text;
                                        tstc3.NgayTiem = dtMui3.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "3 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                            }
                            n++;
                        }
                        if (n != 1)
                        {
                            MessageBox.Show("Sửa tiêm chủng thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Sửa tiêm chủng thất bại!");
                        }
                    }
                    else if (tstc2.Count == 1)
                    {
                        //thêm mũi 2
                        if (!string.IsNullOrEmpty(lupMui2.Text))
                        {
                            TSTC.MaBNhan = _mabn;
                            TSTC.Loai_Vacxin = lupMui2.Text;
                            TSTC.NgayTiem = dtMui2.DateTime;
                            DataContext.TienSuTiemChungs.Add(TSTC);
                            if (DataContext.SaveChanges() > 0)
                            {
                                lbmui.Text = "2 Mũi";
                            }
                        }
                        //thêm mũi 3
                        if (!string.IsNullOrEmpty(lupMui3.Text))
                        {
                            TSTC.MaBNhan = _mabn;
                            TSTC.Loai_Vacxin = lupMui1.Text;
                            TSTC.NgayTiem = dtMui1.DateTime;
                            DataContext.TienSuTiemChungs.Add(TSTC);
                            if (DataContext.SaveChanges() > 0)
                            {
                                lbmui.Text = "3 Mũi";
                            }
                        }
                        //sửa xóa
                        int n = 1;
                        foreach (var item in tstc2)
                        {
                            TienSuTiemChung TSTC1 = new TienSuTiemChung();
                            int id = item.ID_TiemChung;
                            TienSuTiemChung tstc3 = DataContext.TienSuTiemChungs.Single(p => p.ID_TiemChung == id);
                            switch (n)
                            {
                                case 1:

                                    if (!string.IsNullOrEmpty(lupMui1.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui1.Text;
                                        tstc3.NgayTiem = dtMui1.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "1 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(lupMui2.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui2.Text;
                                        tstc3.NgayTiem = dtMui2.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "2 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(lupMui3.Text))
                                    {
                                        tstc3.Loai_Vacxin = lupMui3.Text;
                                        tstc3.NgayTiem = dtMui3.DateTime;
                                        if (DataContext.SaveChanges() > 0)
                                        {
                                            lbmui.Text = "3 Mũi";
                                        }
                                    }
                                    else
                                    {
                                        DataContext.TienSuTiemChungs.Remove(tstc3);
                                        DataContext.SaveChanges();
                                    }
                                    break;
                            }
                            n++;
                        }
                        if (n != 1)
                        {
                            MessageBox.Show("Sửa tiêm chủng thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Sửa tiêm chủng thất bại!");
                        }
                    }
                    // thêm sửa xóa
                    //int n = 1;
                    //foreach (var item in tstc2)
                    //{
                    //    TienSuTiemChung TSTC1 = new TienSuTiemChung();
                    //    int id = item.ID_TiemChung;
                    //    TienSuTiemChung tstc3 = DataContext.TienSuTiemChungs.Single(p => p.ID_TiemChung == id);
                    //    switch (n)
                    //    {
                    //        case 1:

                    //            if (!string.IsNullOrEmpty(lupMui1.Text))
                    //            {
                    //                tstc3.Loai_Vacxin = lupMui1.Text;
                    //                tstc3.NgayTiem = dtMui1.DateTime;
                    //                lbmui.Text = "1 Mũi";
                    //                DataContext.SaveChanges();
                    //            }
                    //            else
                    //            {
                    //                DataContext.TienSuTiemChungs.Remove(tstc3);
                    //                DataContext.SaveChanges();
                    //            }
                    //            break;
                    //        case 2:
                    //            if (!string.IsNullOrEmpty(lupMui2.Text))
                    //            {
                    //                tstc3.Loai_Vacxin = lupMui2.Text;
                    //                tstc3.NgayTiem = dtMui2.DateTime;
                    //                lbmui.Text = "2 Mũi";
                    //                DataContext.SaveChanges();
                    //            }
                    //            else
                    //            {
                    //                DataContext.TienSuTiemChungs.Remove(tstc3);
                    //                DataContext.SaveChanges();
                    //            }
                    //            break;
                    //        case 3:
                    //            if (!string.IsNullOrEmpty(lupMui3.Text))
                    //            {
                    //                tstc3.Loai_Vacxin = lupMui3.Text;
                    //                tstc3.NgayTiem = dtMui3.DateTime;
                    //                lbmui.Text = "3 Mũi";
                    //                DataContext.SaveChanges();
                    //            }
                    //            else
                    //            {
                    //                DataContext.TienSuTiemChungs.Remove(tstc3);
                    //                DataContext.SaveChanges();
                    //            }
                    //            break;
                    //    }
                    //    n++;
                    //}
                    //if (n != 1)
                    //{
                    //    MessageBox.Show("Sửa tiêm chủng thành công!");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Sửa tiêm chủng thất bại!");
                    //} 
                }
            }
        }
    }
}
