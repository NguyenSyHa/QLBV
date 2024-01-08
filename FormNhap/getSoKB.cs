using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Configuration;
namespace QLBV.FormNhap
{
    public partial class getSoKB : DevExpress.XtraEditors.XtraUserControl
    {
        public getSoKB()
        {
            InitializeComponent();
        }
        DateTime dt = new DateTime();

        public static bool UpdateGoiSoDK(int sodk, int mabn){
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var status = _data.SoDKKBs.Where(p => p.SoDK == sodk && p.MaBNhan == mabn).ToList();
            int sophong = 0;
            if (ConfigurationManager.AppSettings["phongdoc"] != null)
                sophong = Convert.ToInt32(ConfigurationManager.AppSettings["phongdoc"]);
            if(sophong<=0)
                MessageBox.Show("chưa thiết lập số phòng");
            foreach (var item in status)
            {
                item.Status = 1;
                item.ThoiGian = DateTime.Now;
                item.Phong = sophong;
                _data.SaveChanges();
            }

            return true;
        }
        public static void ComPort(int so)
        {
            try
            {
                string Port = ConfigurationManager.AppSettings["ComPort"];
                if (!string.IsNullOrWhiteSpace(Port) && !string.IsNullOrEmpty(Port))
                {
                    Library_LED.LEDCommunication Com = new Library_LED.LEDCommunication(Port);
                    string a = Com.ShowView(so.ToString());
                    if (!string.IsNullOrEmpty(a))
                    {
                        MessageBox.Show(a);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa thiết lập cổng kết nối");
                }
            }
            catch
            {

                return;
            }
        }
        private void getSoKB_Load(object sender, EventArgs e)
        {
            labNgayHT.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            dt = DateTime.Now;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTenKP.DataSource = _data.KPhongs.ToList();
            TimKiem();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labNgayHT.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            dt = DateTime.Now;
        }
        void inSoDK(int sodk, DateTime ngay)
        {
            frmIn frm = new frmIn();
            BaoCao.Rep_SoDKKB rep = new BaoCao.Rep_SoDKKB();
            rep.STT.Value = sodk;
            rep.Ngaygio.Value = ngay.ToString("dd/MM/yyyy HH:mm");
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }
        void TimKiem()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            grcSoDK.DataSource = (from so in _data.SoDKKBs.Where(p => p.NgayDK == dt.Date)
                                  join bn in _data.BenhNhans on so.MaBNhan equals bn.MaBNhan into leftjoin
                                  from lf in leftjoin.DefaultIfEmpty()
                                  select new { so.MaBNhan, so.NgayDK, so.SoDK, so.GioDK, TenBNhan = lf == null ? "" : lf.TenBNhan, MaKP = lf == null ? 0 : lf.MaKP, }
                                                ).OrderBy(p => p.SoDK).ToList();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int sodk = 1;
            DateTime ngaydk = dt;
            var maxsodk = _data.SoDKKBs.Where(p => p.NgayDK == ngaydk.Date).Select(p => p.SoDK).ToList();
            if (maxsodk.Count > 0)
                sodk = maxsodk.Max() + 1;
            SoDKKB moi = new SoDKKB();
            moi.SoDK = sodk;
            moi.NgayDK = ngaydk.Date;
            moi.GioDK = ngaydk.TimeOfDay;
            _data.SoDKKBs.Add(moi);
            _data.SaveChanges();
            inSoDK(sodk, ngaydk);
            dt = DateTime.Now;
            TimKiem();
        }
        int mabn = 0;
        void getvalue_deleg(int mabn)
        {
            this.mabn = mabn;
        }
        private void grvSoDK_DoubleClick(object sender, EventArgs e)
        {
           
            mabn = 0;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int sodk = 0;
            if (grvSoDK.GetFocusedRowCellValue(colSoDKKB) != null)
                sodk = Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colSoDKKB));
            dt = DateTime.Now;
            if (sodk > 0)
            {
                var updateSoDK = _data.SoDKKBs.Where(p => p.SoDK == sodk && p.NgayDK == dt.Date).ToList();
                foreach (var item in updateSoDK)
                {

                    if (item.MaBNhan == null || item.MaBNhan == 0)
                    {
                        frmHSBNNhapMoi frm = new frmHSBNNhapMoi(sodk);
                        frm.getdata = new frmHSBNNhapMoi.getString(getvalue_deleg);
                        frm.ShowDialog();
                        item.MaBNhan = mabn;
                        _data.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Sửa thông tin bệnh nhân");
                        frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, Convert.ToInt32(item.MaBNhan));
                        frm.ShowDialog();
                    }
                }

            }
        }
        //Library_LED.LEDCommunication ComPort;
        private void grvSoDK_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colTenBN")
            {
                if (grvSoDK.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    frmHSBN.InPhieuGiuThe_TB(Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colMaBNhan)));
                }
            }
            if (e.Column.Name == "colSoDKKB")
            {
                int sodk = 0;
                DateTime dt = new DateTime();
                if (grvSoDK.GetFocusedRowCellValue(colSoDKKB) != null)
                    sodk =Convert.ToInt32(grvSoDK.GetFocusedRowCellValue(colSoDKKB));
                if (grvSoDK.GetFocusedRowCellValue(colNgayDK) != null)
                    dt = Convert.ToDateTime(grvSoDK.GetFocusedRowCellValue(colNgayDK));
                    getSoKB.ComPort(sodk);
                    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var status = _data.SoDKKBs.Where(p => p.SoDK == sodk && p.NgayDK == dt.Date).ToList();
                    int sophong = 0;
                    if (ConfigurationManager.AppSettings["phongdoc"] != null)
                        sophong = Convert.ToInt32(ConfigurationManager.AppSettings["phongdoc"]);
                    if (sophong <= 0)
                        MessageBox.Show("chưa thiết lập số phòng");
                    foreach (var item in status)
                    {
                        item.Status = 1;
                        item.ThoiGian = DateTime.Now;
                        item.Phong = sophong;
                        _data.SaveChanges();
                    }

            }
        }
    }
}
