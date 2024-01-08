using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.SqlClient;


namespace QLBV.FormThamSo
{
    public partial class Frm_NangcapSQL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_NangcapSQL()
        {
            InitializeComponent();
        }
        public class SQLCMD
        {
            private string mota, caulenh;
            private DateTime ngayUpdate;

            public DateTime NgayUpdate
            {
                get { return ngayUpdate; }
                set { ngayUpdate = value; }
            }
            string ghiChu;

            public string GhiChu
            {
                get { return ghiChu; }
                set { ghiChu = value; }
            }
            private int stt;
            private bool chon;
            public string Mota
            { set { mota = value; } get { return mota; } }
            public string Caulenh
            { set { caulenh = value; } get { return caulenh; } }
            public int STT
            { set { stt = value; } get { return stt; } }
            public bool Chon
            { set { chon = value; } get { return chon; } }
        }
        string _message = "";
        private bool Executi(string _mota, string _caulenh, string _mk)
        {
            string mk = _mk;
            string lenh = _caulenh;
            string mota = _mota;
            string host = DungChung.Bien.TenServer;
            string data = DungChung.Bien.TenCSDL;
            string Chuoi = "Data Source=" + host + ";Initial Catalog=" + data + ";User ID=sa;password=" + mk;
            SqlConnection conn = new SqlConnection(Chuoi);
            conn.InfoMessage += OnInfoMessage;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = lenh;
            try
            {
                conn.Open();
                conn.StateChange += OnStateChange;
                cmd.Connection = conn;
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _message += "--> " + ex.Message + " \r\n ";
                mmcommand.Text = _message;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                //MessageBox.Show("Query executed successfully");
                _message += "--> " + "Query executed successfully" + " \r\n ";
                mmcommand.Text = _message;
            }

            return false;
        }
        private bool Test(string _pass)
        {
            string pass = _pass;
            string host = DungChung.Bien.TenServer;
            string data = DungChung.Bien.TenCSDL;
            string Chuoi = "Data Source=" + host + ";Initial Catalog=" + data + ";User ID=sa;password=" + pass;
            SqlConnection conn = new SqlConnection(Chuoi);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                _message += "--> " + ex.Message + " \r\n ";
                mmcommand.Text = _message;
                MessageBox.Show(ex.Message);
                return false;
            }

            conn.Close();
            MessageBox.Show("Kết nối thành công!");
            _message += "--> " + "Kết nối thành công" + " \r\n ";
            mmcommand.Text = _message;
            return true;


        }

        private static void OnStateChange(object sender, StateChangeEventArgs e)
        {
            string a1 = e.OriginalState.ToString();
            string a2 = e.CurrentState.ToString();

        }
        private static void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError info in e.Errors)
            {
                string a = info.Message;
            }
        }
        List<SQLCMD> _SQl = new List<SQLCMD>();
        void set_chuyenkhoa()
        {

            foreach (var item in DungChung.Bien._lChuyenKhoa)
            {
                chuyenkhoa += "INSERT  INTO ChuyenKhoa(TenCK, TenChiTiet, MaCK, Status)VALUES ( N'" + item.ChuyenKhoa + "', N'" + item.ChuyenKhoa + "'," + item.MaCK + ",1)     ";
            }
        }
        public Frm_NangcapSQL(List<SQLCMD> _SQL1)
        {
            InitializeComponent();
            _SQl = _SQL1.ToList();
        }
        string MaKPsd = "alter table DichVu add  MaKPsd varchar(250)  null";
        string chuyenkhoa = "";
        string c_ChuyenKhoa = "CREATE TABLE [dbo].[ChuyenKhoa]( [TenCK] [nvarchar](100) NULL,[TenChiTiet] [nvarchar](250) NULL,	[MaCK] [int] NOT NULL,	[Status] [int] NOT NULL,   CONSTRAINT [PK_ChuyenKhoa] PRIMARY KEY CLUSTERED (	[MaCK] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]  ALTER TABLE [dbo].[ChuyenKhoa] ADD  CONSTRAINT [DF_ChuyenKhoa_STT]  DEFAULT ((0)) FOR [Status]";

        string UpdateMaCK_RaVien = "UPDATE     RaVien SET MaCK = ChuyenKhoa.MaCK FROM  RaVien INNER JOIN BNKB ON RaVien.MaBNhan = BNKB.MaBNhan   AND RaVien.MaKP = BNKB.MaKP INNER JOIN  ChuyenKhoa ON BNKB.ChuyenKhoa = ChuyenKhoa.TenCK";
        string a = "";
        string _p = "";
        string _alter_DTNT = "update BenhNhan set DTNT =0 where DTNT is null alter table BenhNhan alter column DTNT bit not null ALTER TABLE [dbo].[BenhNhan] ADD  CONSTRAINT [DF_BenhNhan_DTNT]  DEFAULT ((0)) FOR DTNT";
        string _add_ngayra_VienPhi = "alter table VienPhi add NgayRa datetime";
        string _update_NgayRa = "update VienPhi set VienPhi.NgayRa = RaVien.NgayRa from VienPhi inner join RaVien on VienPhi.MaBNhan =RaVien.MaBNhan";
        string _add_DiaChi_NhapD = "alter table NhapD add DiaChi nvarchar(250) null";
        string _add_ChuyenKhoa = "alter table KPhong add MaCK int not null alter table KPhong add default ((0)) for MaCK";
        string _update_MaCK = "UPDATE     KPhong SET MaCK = ChuyenKhoa.MaCK FROM  KPhong INNER JOIN  ChuyenKhoa ON KPhong.ChuyenKhoa = ChuyenKhoa.TenCK";
        string _alter_MaICD = "alter table RaVien alter column  MaICD varchar(20)  null";
        string _add_Ma_LK = "alter table BenhNhan add  Ma_lk varchar(50) null";
        string _add_MaATC = "alter table DichVu add MaATC varchar(50) null";
        string _add_IDDTBN_NhapD = "alter table NhapD add IDDTBN tinyint null";
        string _add_table_TamUngct = "CREATE TABLE [dbo].[TamUngct](	[IDTamUngct] [int] IDENTITY(1,1) NOT NULL,	[IDTamUng] [int] NOT NULL,	[MaDV] [int] NOT NULL,	[SoLuong] [float] NOT NULL,	[DonGia] [float] NOT NULL,	[ThanhTien] [float] NOT NULL,	[SoTien] [float] NOT NULL,	[IDCD] [int] NULL,	[TrongBH] [int] NOT NULL, CONSTRAINT [PK_TamUngct] PRIMARY KEY CLUSTERED (	[IDTamUngct] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
        string _add_DichMuc = "alter table DichVu add DinhMuc int null";
        string _add_Status_TamUng = "alter table TamUng add Status bit   null ALTER TABLE [dbo].[TamUng] ADD  CONSTRAINT [DF_TamUng_Status]  DEFAULT ((1)) FOR Status";
        string mabv = DungChung.Bien.MaBV;
        string _add_MaKCB_BenhNhan = "alter table BenhNhan add MaKCB varchar(10) null";
        string _add_PhanUngT = "CREATE TABLE [dbo].[PhanUngT](	[MaBNhan] [int] NOT NULL,	[TimeStart] [datetime] NOT NULL,	[MaDV] [int] NOT NULL,	[PPThu] [nvarchar](500) NULL,	[MaCB] [varchar](50) NULL,	[TimeClose] [datetime] NULL,	[MaKP] [int] NOT NULL,	[ID_PUT] [int] IDENTITY(1,1) NOT NULL, CONSTRAINT [PK_PhanUngT] PRIMARY KEY CLUSTERED (	[ID_PUT] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
        string _update_SoPL_MaKXuat = "UPDATE  DThuocct  SET   SoPL = DThuoc.SoPL, MaKXuat = DThuoc.MaKXuat FROM   DThuoc INNER JOIN DThuocct ON DThuoc.IDDon = DThuocct.IDDon WHERE  (DThuoc.PLDV = 1) AND (DThuocct.SoPL = 0) AND (DThuocct.MaKXuat IS NULL OR DThuocct.MaKXuat = 0)";
        
        private void setUpdate()
        {
            _SQl.Add(new SQLCMD { Caulenh = MaKPsd, STT = 1, Chon = false, Mota = "Thêm column MaKPsd vào bảng dịch vụ, lưu trữ các khoa phòng được phép sử dụng dịch vụ" });
            _SQl.Add(new SQLCMD { Caulenh = c_ChuyenKhoa, STT = 2, Chon = false, Mota = "create table ChuyenKhoa", NgayUpdate = Convert.ToDateTime("16/03/2016") });
            set_chuyenkhoa();
            _SQl.Add(new SQLCMD { Caulenh = chuyenkhoa, STT = 3, Chon = false, Mota = "insert value table ChuyenKhoa", NgayUpdate = Convert.ToDateTime("16/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = UpdateMaCK_RaVien, STT = 4, Chon = false, Mota = "update value MaCK table RaVien", NgayUpdate = Convert.ToDateTime("16/03/2016"), GhiChu = "Run 2" });
            _SQl.Add(new SQLCMD { Caulenh = _alter_DTNT, STT = 5, Chon = false, Mota = "set BenhNhan.DTNT not null", NgayUpdate = Convert.ToDateTime("18/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_ngayra_VienPhi, STT = 6, Chon = false, Mota = "add NgayRa into table VienPhi", NgayUpdate = Convert.ToDateTime("18/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _update_NgayRa, STT = 7, Chon = false, Mota = "update NgayRa in VienPhi", NgayUpdate = Convert.ToDateTime("18/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_DiaChi_NhapD, STT =8, Chon = false, Mota = "add DiaChi in NhapD", NgayUpdate = Convert.ToDateTime("21/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_ChuyenKhoa, STT = 9, Chon = false, Mota = "add MaCK in KPhong", NgayUpdate = Convert.ToDateTime("28/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _update_MaCK, STT = 10, Chon = false, Mota = "update MaCK in KPhong", NgayUpdate = Convert.ToDateTime("28/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _alter_MaICD, STT = 11, Chon = false, Mota = "alter MaICD in RaVien", NgayUpdate = Convert.ToDateTime("28/03/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_Ma_LK, STT = 12, Chon = false, Mota = "add Ma_lk in BenhNhan", NgayUpdate = Convert.ToDateTime("20/04/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_MaATC, STT = 13, Chon = false, Mota = "add MaATC in DichVu", NgayUpdate = Convert.ToDateTime("10/05/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_IDDTBN_NhapD, STT = 14, Chon = false, Mota = "add Ma_lk in NhapD", NgayUpdate = Convert.ToDateTime("10/05/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_table_TamUngct, STT = 15, Chon = false, Mota = "add table TamUngct", NgayUpdate = Convert.ToDateTime("13/05/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_DichMuc, STT = 16, Chon = false, Mota = "add DinhMuc in DichVu", NgayUpdate = Convert.ToDateTime("04/05/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_Status_TamUng, STT = 17, Chon = false, Mota = "add Status in TamUng", NgayUpdate = Convert.ToDateTime("18/05/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_MaKCB_BenhNhan, STT = 18, Chon = false, Mota = "add MaKCB in BenhNhan", NgayUpdate = Convert.ToDateTime("20/06/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _add_PhanUngT, STT = 19, Chon = false, Mota = "add table PhanUngT", NgayUpdate = Convert.ToDateTime("20/06/2016") });
            _SQl.Add(new SQLCMD { Caulenh = _update_SoPL_MaKXuat, STT = 20, Chon = false, Mota = "update SoPL _MaKXuat in Dthuocct", NgayUpdate = Convert.ToDateTime("02/12/2016") });
            
        }
        private void sbtCheck_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Text))
            {
                _p = txtPass.Text;
                if (Test(_p))
                {
                    GrcDS.Enabled = true;
                    sbtUpdate.Enabled = true;
                }

            }
            else
            {
                MessageBox.Show("Bạn cần nhập mật khẩu!");
                txtPass.Focus();
            }

        }

        private void Frm_NangcapSQL_Load(object sender, EventArgs e)
        {
            setUpdate();
            GrcDS.Enabled = false;
            sbtUpdate.Enabled = false;
        }

        private void sbtUpdate_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
           
            var ht = _data.HTHONGs.Where(p => p.MaBV == null).ToList();
            foreach (var item in ht)
            {
                item.MaBV = DungChung.Bien.MaBV;
                _data.SaveChanges();

            }
            foreach (var a in _SQl)
            {
                if (a.Chon == true)
                {
                    _message += "--> " + a.Mota + " is running!" + " \r\n ";
                    mmcommand.Text = _message;
                    Executi(a.Mota, a.Caulenh, _p);
                }
            }
        }

        private void txtPass_EditValueChanged(object sender, EventArgs e)
        {
            GrcDS.DataSource = _SQl.ToList();
            GrcDS.Enabled = false;
            sbtUpdate.Enabled = false;
        }

    }
}