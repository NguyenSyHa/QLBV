using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;

//using System.Linq.Dynamic;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_BNNTLinhThuoc : DevExpress.XtraEditors.XtraForm
    {
        
        public frm_BC_BNNTLinhThuoc()
        {
            InitializeComponent();
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = new DateTime(dtpTungay.Value.Year, dtpTungay.Value.Month, dtpTungay.Value.Day, 00, 00, 00);

            DateTime denngay = new DateTime(dtpDenngay.Value.Year, dtpDenngay.Value.Month, dtpDenngay.Value.Day, 23, 59, 00);


            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


            var bn = (from a in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT==true)
                      join b in data.DThuocs.Where(t => t.PLDV == 1) on a.MaBNhan equals b.MaBNhan
                      join c in data.RaViens on a.MaBNhan equals c.MaBNhan
                      join d in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals d.MaBNhan
                      orderby a.IDPerson, a.NNhap
                      select new
                      {
                          a.MaBNhan,
                          a.IDPerson,
                          a.TenBNhan,
                          a.Tuoi,
                          a.GTinh,
                          a.DChi,
                          a.SThe,
                          c.MaICD,
                          d.NgayKham,

                      }).ToList();


            BaoCao.Rep_BNNTLAYTHUOC rep = new BaoCao.Rep_BNNTLAYTHUOC();
            frmIn frm = new frmIn();
            // BCDTNT bc = new BCDTNT();
            List<BCDTNT> lst = new List<BCDTNT>();
               
                for (int i = 0; i < bn.Count - 1; i++)
                {
                    DateTime ngaykham = (DateTime)bn[i].NgayKham;
                    for (int j = i + 1; j < bn.Count; j++)
                    {
                        DateTime ngaykham1 = (DateTime)bn[j].NgayKham;
                        BCDTNT moi = new BCDTNT();
                        if (bn[i].IDPerson == bn[j].IDPerson)
                        {
                            if (radio1T.Checked && (ngaykham1 - ngaykham).Days > 29)
                            {
                            rep.xrLabel3.Text = "THỐNG KÊ BỆNH NHÂN NGOẠI TRÚ LĨNH THUỐC TỪ 1 THÁNG";
                            moi.MaBNhan = bn[i].MaBNhan;
                                moi.TenBNhan = bn[i].TenBNhan;
                                moi.Tuoi = bn[i].Tuoi;

                                if (bn[i].GTinh != null)
                                {
                                    if (bn[i].GTinh == 0)
                                    {
                                        moi.GTinh = "Nữ";
                                    }
                                    if (bn[i].GTinh == 1)
                                    {
                                        moi.GTinh = "Nam";
                                    }
                                }

                                moi.DChi = bn[i].DChi;
                                moi.SThe = bn[i].SThe;
                                moi.MaICD = bn[i].MaICD;
                                moi.NgayKham = bn[i].NgayKham.Value.ToString("dd/MM/yyyy");
                              
                                lst.Add(moi);
                            }
                            else if (radio2T.Checked && (ngaykham1 - ngaykham).Days > 59)
                            {
                            rep.xrLabel3.Text = "THỐNG KÊ BỆNH NHÂN NGOẠI TRÚ LĨNH THUỐC TỪ 2 THÁNG";
                            moi.MaBNhan = bn[i].MaBNhan;
                                moi.TenBNhan = bn[i].TenBNhan;
                                moi.Tuoi = bn[i].Tuoi;

                                if (bn[i].GTinh != null)
                                {
                                    if (bn[i].GTinh == 0)
                                    {
                                        moi.GTinh = "Nữ";
                                    }
                                    if (bn[i].GTinh == 1)
                                    {
                                        moi.GTinh = "Nam";
                                    }
                                }
                                moi.DChi = bn[i].DChi;
                                moi.SThe = bn[i].SThe;
                                moi.MaICD = bn[i].MaICD;
                                moi.NgayKham = bn[i].NgayKham.Value.ToString("dd/MM/yyyy");
                               
                                lst.Add(moi);
                            }

                            break;
                        }
                    }


                }

            var kq = (from _lst in lst
                      select new { _lst.MaBNhan, _lst.TenBNhan, _lst.DChi, _lst.GTinh, _lst.MaICD, _lst.NgayKham, _lst.SThe, _lst.Tuoi }).OrderBy(p=>p.NgayKham).Distinct().ToList();
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ;
            rep.BV.Value = DungChung.Bien.TenCQ;
            rep.DataSource = kq;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

           


        }

       

             

       
        public class BCDTNT
        {
            public int MaBNhan { get; set; }
            public string TenBNhan { get; set; }
            public int? Tuoi { get; set; }
            public string GTinh { get; set; }
            public string DChi { get; set; }
            public string SThe { get; set; }
            public string MaICD { get; set; }
            public string NgayKham { get; set; }

            
        }

        private void frm_BC_BNNTLinhThuoc_Load(object sender, EventArgs e)
        {
            dtpTungay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1,00,00,00);
            DateTime dtime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1,23,59,59);
            dtime = dtime.AddMonths(+1);
            dtime = dtime.AddDays(-1);
            dtpDenngay.Value = dtime;
        }
    }
}