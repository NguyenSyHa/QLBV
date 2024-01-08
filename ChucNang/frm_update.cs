using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
namespace QLBV.ChucNang
{
    public partial class frm_update : Form
    {
        public frm_update()
        {
            InitializeComponent();
        }
        List<BNKB> _lbnkb = new List<BNKB>();
        List<BNKB> _lbnkb2 = new List<BNKB>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            int thang = 0;
            if (!string.IsNullOrEmpty(cboThang.Text))
                thang = Convert.ToInt32(cboThang.Text);

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (radioGroup1.SelectedIndex == 0)//update bệnh nhân
            {
                try
                {
                    var a = (from b in _data.BNKBs.Where(p => p.NgayKham.Value.Month == thang) group b by b.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    foreach (var i in a)
                    {
                        BNKB moi = new BNKB();
                        moi.IDKB = i.IDKB;
                        moi.MaBNhan = i.Key;
                        _lbnkb.Add(moi);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get MaBNhan:" + ex);
                }
                try
                {
                    foreach (var i in _lbnkb)
                    {
                        var id = (from g in _data.BNKBs.Where(p => p.IDKB == i.IDKB) select new { g.MaKP, g.MaBNhan }).ToList();
                        if (id.Count > 0)
                        {
                            BNKB moi = new BNKB();
                            moi.MaKP = id.First().MaKP;
                            moi.MaBNhan = id.First().MaBNhan;
                            _lbnkb2.Add(moi);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get MaKP:" + ex);
                }
                int _mbn = 0;
                try
                {

                    foreach (var i in _lbnkb2)
                    {

                        _mbn = i.MaBNhan == null ? 0 : Convert.ToInt32(i.MaBNhan);

                        var kt = _data.BenhNhans.Where(p => p.MaBNhan== (i.MaBNhan)).ToList();
                        if (kt.Count > 0)
                        {
                            var bn = _data.BenhNhans.Single(p => p.MaBNhan== (i.MaBNhan));
                            bn.MaKP = i.MaKP;
                            _data.SaveChanges();
                        }


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đối chiếu khoa phòng:" + _mbn + ex);
                }
                MessageBox.Show("Hoàn thành");
            }
            else
            { // update ra viện
                var a = (from b in _data.BNKBs.Where(p => p.NgayKham.Value.Month == thang)
                         join rv in _data.RaViens.Where(p => p.MaKP == null || p.MaKP == 0) on b.MaBNhan equals rv.MaBNhan
                         group b by b.MaBNhan into kq
                         select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                foreach (var i in a)
                {
                    BNKB moi = new BNKB();
                    moi.IDKB = i.IDKB;
                    moi.MaBNhan = i.Key;
                    _lbnkb.Add(moi);
                }
                foreach (var i in _lbnkb)
                {
                    var id = (from g in _data.BNKBs.Where(p => p.IDKB == i.IDKB) select new { g.MaKP, g.MaBNhan }).ToList();
                    if (id.Count > 0)
                    {
                        BNKB moi = new BNKB();
                        moi.MaKP = id.First().MaKP;
                        moi.MaBNhan = id.First().MaBNhan;
                        _lbnkb2.Add(moi);
                    }
                }
                int j = 0;
                foreach (var i in _lbnkb2)
                {

                    var ravien = _data.RaViens.Where(p => p.MaBNhan == i.MaBNhan).ToList();
                    if (ravien.Count > 0)
                    {
                        ravien.First().MaKP = i.MaKP ?? 0;
                        _data.SaveChanges();
                        j++;
                    }
                }
                MessageBox.Show("Thực hiện thành công: "+j+" bản ghi");
            }
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string s = ("xml");
            string fileName = string.Format("{0}_{1}_{2}_{3}", "products", DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
          //  string path_file = string.Format("{0}/{1}.xml", s, fileName);
            string path_file = string.Format("{0}/{1}.xml",  fileName);
            if (!System.IO.File.Exists(path_file))
            {
                XmlTextWriter writer = new XmlTextWriter(path_file, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("products");
             DungChung.XML.createNode("1", "Shirt", "1000", writer);
             DungChung.XML.createNode("2", "Jeans", "2000", writer);
             DungChung.XML.createNode("3", "Jacket", "3000", writer);
             DungChung.XML.createNode("4", "Coast", "4000", writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }
    }
}
