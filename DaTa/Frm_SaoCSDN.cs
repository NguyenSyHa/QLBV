using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Linq;


namespace QLBV.FormNhap
{
    public partial class Frm_SaoCSDN : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SaoCSDN()
        {
            InitializeComponent();
        }
       public string ketnoi1()
        { 
        string chuoikn=  @"Data Source=" + txtmaychu.Text + ";User ID=sa;Password=" + txtmatkhau.Text + "";
        return chuoikn;
        }
       SqlConnection ketnoi = new SqlConnection();
        SqlDataAdapter data;
        
        public DataTable dsdatabases(){
            ketnoi.ConnectionString = ketnoi1();
            string sql = @"select name from sys.databases";
            DataTable bang = new DataTable();
            data = new SqlDataAdapter(sql,ketnoi);
            data.Fill(bang);
            return bang;
        }
        public string datengay(DateTime ngay)
        {
            string date = ngay.Year.ToString() + "-" + ngay.Month.ToString() + "-" + ngay.Day.ToString();
            return date;
        } 
        public void backupdatabases()
        {
            ketnoi.ConnectionString = ketnoi1();
            string tring = txtduongdan.Text;
            string lenhbackup = @"backup database " + loup_datable.Text+ " to disk ='" + tring + "'";
            //MessageBox.Show(tring);
            ketnoi.Open();
            SqlCommand backup = new SqlCommand(lenhbackup, ketnoi);
            backup.CommandTimeout = 180000;
            backup.ExecuteNonQuery();
            ketnoi.Close();
            MessageBox.Show("Backup dữ liệu thành công ", "Thông báo");

        }
        public void taodatabases1()
        {
            ketnoi.ConnectionString = ketnoi1();
            string lenhtaodatabases = @"create database " + txttendatabases.Text + "";
            ketnoi.Open();
            SqlCommand thuhienlenh = new SqlCommand(lenhtaodatabases, ketnoi);
            thuhienlenh.ExecuteNonQuery();
            ketnoi.Close();

        }
        public void saocheptable()
        {
            ketnoi.ConnectionString = ketnoi1();
            taodatabases1();
            string lenh = @"use " + loup_datable.Text + " SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,isnull(convert(varchar(10),CHARACTER_MAXIMUM_LENGTH),''), isnull(COLUMN_DEFAULT,'null'),nulhaykhong= case IS_NULLABLE when 'NO' then 'is null' when 'YES' then 'not null' end  FROM INFORMATION_SCHEMA.COLUMNS ";
            ketnoi.Open();
            SqlCommand thu = new SqlCommand(lenh, ketnoi);
            DataTable k = new DataTable();
            k.Load(thu.ExecuteReader());
            string selelctname = @"use " + loup_datable.Text + " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS  group by TABLE_NAME";
            SqlCommand thu1 = new SqlCommand(selelctname, ketnoi);
            DataTable k1 = new DataTable();
            k1.Load(thu1.ExecuteReader());
            string use = @" use " + loup_datable.Text + " ";
            string thuchien = @"select tenbang=k.name,tencolumn=j.name,khoa=l.COLUMN_NAME,tudongtang=convert(varchar,j.is_identity,10) into #hu from sys.tables k,sys.columns j ,INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE l where k.object_id=j.object_id and l.TABLE_NAME=k.name
update #hu set khoa = '' where tencolumn<>khoa
select tenbang,tencolumn,khoa,tt=case tudongtang when 0 then '' when 1 then 'identity' end into #huancao from #hu
update #huancao set khoa = 'primary key' where khoa<>''
--ham2
declare @huan table 
(tenbang nvarchar(50),tencot varchar(50),dodai varchar(50),kieu varchar(50),giatrimd varchar(10))
 SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,ddai=isnull('('+convert(varchar(10),CHARACTER_MAXIMUM_LENGTH)+')',''),
 giachi=isnull(COLUMN_DEFAULT,'')into #huan 
 FROM INFORMATION_SCHEMA.COLUMNS 
 update #huan set giachi='default 0' where giachi<>''
 insert @huan(tenbang,tencot,kieu,dodai,giatrimd)select TABLE_NAME,COLUMN_NAME,DATA_TYPE,convert(varchar(50),ddai),giachi from #huan 
 update @huan set dodai='(max)' where dodai=N'(-1)'
 select tenbang,tencot,TYPE1=kieu+dodai,giatrimd into #huancao1 from @huan 
 select #huancao1.tenbang,lenh=#huancao1.tencot+' '+#huancao1.TYPE1+' '+#huancao.khoa +' '+#huancao.tt+' '+#huancao1.giatrimd from #huancao,#huancao1 where #huancao.tenbang=#huancao1.tenbang and #huancao.tencolumn=#huancao1.tencot
 drop table #huan 
 drop table #hu 
 drop table #huancao
 drop table #huancao1";

            string ketquachuan = use + thuchien;
            DataTable huan = new DataTable();
            SqlCommand thu3 = new SqlCommand(ketquachuan, ketnoi);
            huan.Load(thu3.ExecuteReader());
            List<cautrucbang1> dulieu = new List<cautrucbang1>();
            for (int h = 0; h < huan.Rows.Count; h++)
            {
                cautrucbang1 colom = new cautrucbang1();
                colom.Tenbang = huan.Rows[h]["tenbang"].ToString();
                colom.lenh = huan.Rows[h]["lenh"].ToString();
                dulieu.Add(colom);
            }
            var tk = (from dh in dulieu select new { dh.Tenbang, dh.lenh }).ToList();
            string ketqua;
            string ketquacuoi;
            string taomoi;
            DataTable them = new DataTable();
            SqlDataAdapter th;

            for (int a = 0; a < k1.Rows.Count; a++)
            {
  
                taomoi = @" use " + txttendatabases.Text + "  create table " + k1.Rows[a]["TABLE_NAME"].ToString() + "";
                int hg = 0;
                var thuxem = (from h in tk.Where(p => p.Tenbang == k1.Rows[a]["TABLE_NAME"].ToString()) select new { id = hg++, h.lenh }).ToList();
                string kk = "";
                string lenhtaobang = "";
                for (int t = 0; t < thuxem.Count; t++)
                {
                    kk += thuxem.Where(p => p.id == t).First().lenh.ToString() + ",";
                }
                if (kk != "")
                {
                    int cat = kk.Length;
                    lenhtaobang = taomoi + "(" + kk.Substring(0, cat - 1) + ")";
                    //MessageBox.Show(lenhtaobang);
                    SqlCommand taobang = new SqlCommand(lenhtaobang, ketnoi);
                    try
                    {
                        taobang.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    thuxem.Clear();
                    them.Clear();
                }

            }

            MessageBox.Show("Tao bảng thành công", "Thông báo");
            ketnoi.Close();
        }
      
       
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtduongdan.Text = folderBrowserDialog1.SelectedPath + (@"\"+DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString()+".bak");
        }
        public void hamchec() 
        {
            if (radiobackup.Checked == true)
            {
                loup_datable.Enabled = true;
                txtduongdan.Enabled = true;
                txttendatabases.Enabled = false;
                Editngay.Enabled = false;
                taodatabases.Enabled = false;
                Taotable.Enabled = false;
                Backup.Enabled = true;
            }
            else if(radiodatabases.Checked==true)
            {
                loup_datable.Enabled =false;
                txtduongdan.Enabled = false;
                txttendatabases.Enabled = true;
                Editngay.Enabled = false;
                Backup.Enabled = false;
                Taotable.Enabled = false;
                taodatabases.Enabled = true;
            }
            else if (radiochuyen.Checked == true)
            {
                loup_datable.Enabled = true;
                txtduongdan.Enabled = false;
                txttendatabases.Enabled = true;
                Editngay.Enabled = true;
                Backup.Enabled = false;
                Taotable.Enabled = true;
                taodatabases.Enabled = false;
            }
            else if (radiobackup.Checked == false && radiodatabases.Checked == false && radiochuyen.Checked == false)
            {
                loup_datable.Enabled = false;
                txtduongdan.Enabled = false;
                txttendatabases.Enabled =false;
                Editngay.Enabled = false;
                Backup.Enabled = false;
                Taotable.Enabled = false;
                taodatabases.Enabled = false;
            }
        
        }
        private void Frm_SaoCSDN_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(298, 180);
            hamchec();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
            try {
                if (txtmaychu.Text != "" && txttaikhoan.Text != "" && txtmatkhau.Text != "")
                {
                    ketnoi.ConnectionString = ketnoi1();
                    ketnoi.Open();
                    ketnoi.Close();
                    MessageBox.Show("kết nối thành công", "Thông báo");
                    txtmaychu.Enabled = false;
                    txttaikhoan.Enabled = false;
                    txtmatkhau.Enabled = false;
                    this.Size = new System.Drawing.Size(298,395);
                    hamchec();
                    loup_datable.Properties.DataSource = dsdatabases();
                }
                else {
                    MessageBox.Show("Bạn chưa điền đầy đủ thông tin", "Thông báo");
                }
                }
            catch(Exception){
                MessageBox.Show("kết nối thất bại", "Thông báo");
            }
        }
        // ham chuyen du lieu  sang da ta databases
        public void hamchuyendulieu()
        {

            string lenh = @"use " + loup_datable.Text + " SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,isnull(convert(varchar(10),CHARACTER_MAXIMUM_LENGTH),''), isnull(COLUMN_DEFAULT,'null'),nulhaykhong= case IS_NULLABLE when 'NO' then 'is null' when 'YES' then 'not null' end  FROM INFORMATION_SCHEMA.COLUMNS ";
            ketnoi.Open();
            SqlCommand thu = new SqlCommand(lenh, ketnoi);
            DataTable k = new DataTable();
            k.Load(thu.ExecuteReader());
            string selelctname = @"use " + loup_datable.Text + " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS  group by TABLE_NAME";
            SqlCommand thu1 = new SqlCommand(selelctname, ketnoi);
            DataTable k1 = new DataTable();
            k1.Load(thu1.ExecuteReader());
            string usetb = @" use " + loup_datable.Text + " ";
            string cautrutaobang =""+usetb+"" + @"declare @huan table (tenbang nvarchar(50),tencot varchar(50),dodai varchar(50),kieu varchar(50)) SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,ddai=isnull('('+convert(varchar(10),CHARACTER_MAXIMUM_LENGTH)+')',''),giachi=isnull(COLUMN_DEFAULT,'null'),nulhaykhong= case IS_NULLABLE when 'NO' then 'is null' when 'YES' then 'not null' end  into #huan FROM INFORMATION_SCHEMA.COLUMNS insert @huan(tenbang,tencot,kieu,dodai)select TABLE_NAME,COLUMN_NAME,DATA_TYPE,convert(varchar(50),ddai) from #huan update @huan set dodai='(max)' where dodai=N'(-1)'select tenbang,tencot,TYPE1=kieu+dodai from @huan  drop table #huan";
            SqlCommand thu2 = new SqlCommand(cautrutaobang, ketnoi);
            string taomoi;
            DataTable them = new DataTable();
            them.Load(thu2.ExecuteReader());
            List<taobang> bangthongtin = new List<taobang>();
            for (int h = 0; h < them.Rows.Count;h++)
            {
                taobang col = new taobang();
                col.tenbang = them.Rows[h]["tenbang"].ToString();
                col.tencot = them.Rows[h]["tencot"].ToString();
                col.TYPE1 = them.Rows[h]["TYPE1"].ToString();
                bangthongtin.Add(col);
            }
            string selectbnc = "";
            string[] danhsachbang = new string[] { "BenhNhan", "DichVu", "DTBN", "NhomDV", "TieuNhomDV", "CanBo", "ICD10", "HTHONG", "ADMIN", "DTuong", "BenhVien", "DanToc", "DmHuyen", "DmTinh", "DmXa", "GiaUT", "VienPhict", "ChiDinh", "CLSct", "VienPhi", "DThuoc", "CLS", "BNKB", "KPhong", "KQMau", "DichVuct", "NhaCC", "Permission" };
            for (int a = 0; a < danhsachbang.Length; a++)
            {
             
                int th = 0;
                //MessageBox.Show(danhsachbang[a]);
                var ketquatimkiem = (from h in bangthongtin.Where(p=>p.tenbang==danhsachbang[a]) select new { id = th++, h.tenbang, h.tencot, h.TYPE1 }).ToList();
                taomoi = @" use " + loup_datable.Text + " declare  @" + danhsachbang[a] + " " + " table";
                string kk = "";
                string tencolumn = "";
                string lenhtaobang = "";
                string[] kiemtra = new string[] { "VienPhi", "DThuoc", "CLS", "BNKB" };
                int dem = 0;
                //try
                //{
                for (int t = 0; t < ketquatimkiem.Count(); t++)
                {
                    kk += ketquatimkiem.Where(p => p.id == t).First().tencot.ToString() + " " + ketquatimkiem.Where(p => p.id == t).First().TYPE1.ToString() + ",";
                    tencolumn += ketquatimkiem.Where(p => p.id == t).First().tencot.ToString() + ",";
                }
                int cat = kk.Length;
                int laytencolomn = tencolumn.Length;
                lenhtaobang = taomoi + "(" + kk.Substring(0, cat - 1) + ")";
                for (int b = 0; b < kiemtra.Length; b++)
                {
                    if (danhsachbang[a] == kiemtra[b])
                    {
                        dem += 1;
                        break;
                    }
                }
                if (danhsachbang[a] == "Benhnhan" && dem == 0)
                {
                    selectbnc = @"select * from benhnhan where (benhnhan.MaBNhan not in (select RaVien.MaBNhan from RaVien where convert(date,RaVien.NgayRa,0)<'" + datengay(Editngay.DateTime) + "') and BenhNhan.NoiTru=1) or (BenhNhan.MaBNhan not in (select VienPhi.MaBNhan from VienPhi where convert(date,VienPhi.NgayTT,0)<'" + datengay(Editngay.DateTime) + "')) and BenhNhan.Status<>0";
                }
                else if (dem != 0)
                {
                    selectbnc = @"select * from " + danhsachbang[a] + "" + " " + "where " + "" + danhsachbang[a] + ".MaBNhan" + " " + "in" + " (" + @"select benhnhan.MaBNhan from benhnhan where (benhnhan.MaBNhan not in (select RaVien.MaBNhan from RaVien where convert(date,RaVien.NgayRa,0)<'" + datengay(Editngay.DateTime) + "') and BenhNhan.NoiTru=1) or (BenhNhan.MaBNhan not in (select VienPhi.MaBNhan from VienPhi where convert(date,VienPhi.NgayTT,0)<'" + datengay(Editngay.DateTime) + "')) and BenhNhan.Status<>0" + ")";
                }
                else
                {
                    selectbnc = @"select * from " + danhsachbang[a] + "";
                }
                
                    try
                    {
                        string lenhchuyen = lenhtaobang + "  " + "insert into" + "  " + @"@" + "" + danhsachbang[a] + "" + " " + selectbnc + "" + " use " + txttendatabases.Text + " " + " " + "insert into  " + "" + danhsachbang[a] + "(" + tencolumn.Substring(0, laytencolomn - 1) + ") " + "" + "select  " + tencolumn.Substring(0, laytencolomn - 1) + " from " + @"@" + "" + danhsachbang[a] + "";
                        ketnoi.Close();
                        ketnoi.Open();
                        SqlCommand taobang = new SqlCommand(lenhchuyen, ketnoi);
                        taobang.CommandTimeout = 18000;
                        taobang.ExecuteNonQuery();
                        them.Clear();
                        ketnoi.Close();
                    }
                    catch (Exception)
                    {
                        string lenhchuyen = lenhtaobang + "  " + "insert into" + "  " + @"@" + "" + danhsachbang[a] + "" + " " + selectbnc + "" + " use " + txttendatabases.Text + " " + "set identity_insert  " + danhsachbang[a] + "  on  " + " " + "insert into  " + "" + danhsachbang[a] + "(" + tencolumn.Substring(0, laytencolomn - 1) + ") " + "" + "select  " + tencolumn.Substring(0, laytencolomn - 1) + " from " + @"@" + "" + danhsachbang[a] + "";
                        //MessageBox.Show(lenhchuyen);
                        ketnoi.Close();
                        ketnoi.Open();
                        SqlCommand taobang = new SqlCommand(lenhchuyen, ketnoi);
                        taobang.CommandTimeout = 18000;
                        taobang.ExecuteNonQuery();
                        them.Clear();
                        ketnoi.Close();

                    }
                //}
                //catch (Exception)
                //{

                //    MessageBox.Show("Không thể sao chép " + danhsachbang[a] + "", "Thông báo lỗi");

                //}
          
            }
                MessageBox.Show("Chuyển dữ liệu thánh công", "Thông báo");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Frm_SaoCSDN_Load(sender, e);
            txtmaychu.Text = "";
            txttaikhoan.Text ="";
            txtmatkhau.Text = "";
            txtmaychu.Enabled = true;
            txttaikhoan.Enabled = true;
            txtmatkhau.Enabled = true;
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            if (loup_datable.EditValue != null && txtduongdan.Text!=null)
            {
                backupdatabases();
            }
        }

        private void taodatabases_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttendatabases.Text != "")
                {
                    taodatabases1();
                    MessageBox.Show("Tao mới databases thành công ", "thông báo");
                }
                else
                {

                    MessageBox.Show("bạn chưa nhập tên databeses", "Thông báo");
                }
            }
            catch (Exception) {

                MessageBox.Show("nỗi khi tạo databases", "Thông báo");
               
            
            }
        }

        private void Taotable_Click(object sender, EventArgs e)
        {
            saocheptable();
            hamchuyendulieu();

        }

        private void radiobackup_CheckedChanged_1(object sender, EventArgs e)
        {
            hamchec();
        }

        private void radiodatabases_CheckedChanged(object sender, EventArgs e)
        {
            hamchec();
        }

        private void radiochuyen_CheckedChanged(object sender, EventArgs e)
        {
            hamchec();
     
        }

  
    }
    public class cautrucbang1
    {
        public string Tenbang { set; get; }
        public string lenh { set; get; }
    }
    public class taobang {
        public string tenbang { set; get;}
        public string tencot { set; get;}
        public string TYPE1 { set; get;}
    }
    
}