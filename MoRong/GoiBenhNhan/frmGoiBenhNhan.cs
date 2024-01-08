using QLBV.DungChung;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.MoRong.GoiBenhNhan
{
    public partial class frmGoiBenhNhan : Form
    {
        ConnectData connect;
        QLBV_Database.QLBVEntities dataContext;
        int maKP;
        public frmGoiBenhNhan(int _maKP)
        {
            InitializeComponent();

            try
            {
                this.maKP = _maKP;

                // Nếu đang mở 2 trên màn hình thì Form gọi số được mở ở màn phụ
                if (Screen.AllScreens.Count() > 1)
                {
                    var screen = Screen.AllScreens.Where(w => !w.Primary).FirstOrDefault(); // Lấy màn hình phụ
                    if (screen != null)
                        SetFormLocation(this, screen);
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void frmGoiBenhNhan_Load(object sender, EventArgs e)
        {
            //timer1.Start();
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            connect = Program._connect;

            pictureEditLogo.Image = DungChung.Ham.GetLogo();
            var kp = dataContext.KPhongs.FirstOrDefault(o => o.MaKP == maKP);
            if (kp != null)
                lblTenKP.Text = kp.TenKP.ToUpper();
            var cb = dataContext.CanBoes.FirstOrDefault(o => o.MaCB == DungChung.Bien.MaCB);
            if (cb != null)
            {
                lblBacSi.Text = (!string.IsNullOrEmpty(cb.ChucVu) ? cb.ChucVu.ToUpper() + ": " : "BÁC SỸ: ") + cb.TenCB.ToUpper();
            }
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper() + " - TẬN TỤY - NGHĨA TÌNH - HẾT MÌNH VÌ NGƯỜI BỆNH";
            var ngaytu = DungChung.Ham.NgayTu(DateTime.Now);
            var ngayden = DungChung.Ham.NgayDen(DateTime.Now);
            //var bnkb = dataContext.BNKBs.Where(o => true).Select(o => o.MaBNhan);
            var dsbnChuaKham = (from bn in dataContext.BenhNhans.Where(o => o.Status == 0 && o.NNhap >= ngaytu && o.NNhap <= ngayden && o.MaKP == maKP && o.MaKCB == DungChung.Bien.MaBV && o.NoiTru == 0)
                                select bn).ToList();

            var bnkb = (from kb in dataContext.BNKBs.Where(o => o.NgayKham >= ngaytu && o.NgayKham <= ngayden)
                        group kb by new { kb.MaBNhan } into kq
                        select new { kq.Key.MaBNhan, IDKB = kq.Max(o => o.IDKB) });

            var dsbnChuyenPKham = (from q in bnkb
                                   join bn in dataContext.BenhNhans.Where(o => o.MaKCB == DungChung.Bien.MaBV) on q.MaBNhan equals bn.MaBNhan
                                   join kb in dataContext.BNKBs.Where(o => o.PhuongAn == 3 && o.MaKPdt == maKP) on q.IDKB equals kb.IDKB
                                   select bn).ToList();

            dsbnChuaKham.AddRange(dsbnChuyenPKham);
            //RefreshData();
        }

        private void SetFormLocation(Form form, Screen screen)
        {
            Rectangle bounds = screen.Bounds;

            form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            form.StartPosition = FormStartPosition.Manual;
            form.Location = screen.WorkingArea.Location;
        }

        public void RefreshData()
        {
            try
            {
                //var ngaytu = DungChung.Ham.NgayTu(DateTime.Now);
                //var ngayden = DungChung.Ham.NgayDen(DateTime.Now);
                ////var bnkb = dataContext.BNKBs.Where(o => true).Select(o => o.MaBNhan);
                //var dsbnChuaKham = (from bn in dataContext.BenhNhans.Where(o => o.Status == 0 && o.NNhap >= ngaytu && o.NNhap <= ngayden && o.MaKP == maKP && o.MaKCB == DungChung.Bien.MaBV && o.NoiTru == 0)
                //                    select bn).ToList();

                //var bnkb = (from kb in dataContext.BNKBs.Where(o => o.NgayKham >= ngaytu && o.NgayKham <= ngayden)
                //            group kb by new { kb.MaBNhan } into kq
                //            select new { kq.Key.MaBNhan, IDKB = kq.Max(o => o.IDKB) });

                //var dsbnChuyenPKham = (from q in bnkb
                //                       join bn in dataContext.BenhNhans.Where(o => o.MaKCB == DungChung.Bien.MaBV) on q.MaBNhan equals bn.MaBNhan
                //                       join kb in dataContext.BNKBs.Where(o => o.PhuongAn == 3 && o.MaKPdt == maKP) on q.IDKB equals kb.IDKB
                //                       select bn).ToList();

                //dsbnChuaKham.AddRange(dsbnChuyenPKham);



                string strSQL = "sp_KB_DSBN_ChoKham";
                string[] strpara = new string[] { "@MaKP", "@MaKCB", "@tungay", "@denngay" };
                object[] oValue = new object[] { maKP, DungChung.Bien.MaBV, DungChung.Ham.NgayTu(DateTime.Now), DungChung.Ham.NgayDen(DateTime.Now) };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime };

                connect.Connect();

                var tbBenhnhan = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                gridControlDanhSachCho.BeginUpdate();
                gridControlDanhSachCho.DataSource = null;
                if (tbBenhnhan != null && tbBenhnhan.Rows != null && tbBenhnhan.Rows.Count > 0)
                    gridControlDanhSachCho.DataSource = tbBenhnhan.Rows.Cast<System.Data.DataRow>().Take(DungChung.Bien.SoBenhNhanHienThi).CopyToDataTable<DataRow>();
                //dsbnChuaKham.OrderByDescending(o => o.UuTien).ThenBy(o => o.SoTT).ThenBy(o => o.MaBNhan).Take(DungChung.Bien.SoBenhNhanHienThi).ToList();
                gridControlDanhSachCho.EndUpdate();

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        public void Call(string tenBN, string maBN, string stt)
        {
            lblMaBN.Text = "Mã số: " + maBN.ToUpper();
            lblTenBN.Text = tenBN.ToUpper();
            lblSTT.Text = stt;
            RefreshData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void frmGoiBenhNhan_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void frmGoiBenhNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void lblSTT_Click(object sender, EventArgs e)
        {

        }
    }
}
