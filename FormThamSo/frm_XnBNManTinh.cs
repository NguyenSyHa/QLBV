using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_XnBNManTinh : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_XnBNManTinh(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        QLBV_Database.QLBVEntities data;
        private bool Ktraluu()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if(Dtuong=="Dịch vụ")
            {
                if(string.IsNullOrEmpty(txtsocmt.Text))
                {
                    MessageBox.Show("Đối với bệnh nhân dịch vụ, cần nhập số CMT|CCCD");
                    return false;
                }
            }
            var vp = data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
            if(vp.Count>0)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán, không thể sửa");
                return false;
            }
           
            if (string.IsNullOrEmpty(lupChanDoanKb.Text))
            {
                MessageBox.Show("bạn chưa nhập tên bệnh mãn tính");
                lupChanDoanKb.Focus();
                return false;
            }
            return true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if(Ktraluu())
            {
                var ktra = data.BNManTinhs.Where(p => p.MaBNhan == _mabn).ToList();
                if (ktra.Count > 0)
                {
                    try
                    {
                        if (Dtuong == "Dịch vụ")
                        {
                            TTboXung sua = data.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                            sua.CMT = txtsocmt.Text;
                            data.SaveChanges();
                        }
                        BNManTinh moi = ktra.FirstOrDefault();
                        //moi.MaBNhan = _mabn;
                        moi.MaICD = lupMaICDkb.Text;
                        moi.TenBenh = lupChanDoanKb.Text;
                        moi.MucDo = txtmucdo.Text;
                        if (Dtuong == "Dịch vụ")
                            moi.STheSoCMT = txtsocmt.Text;
                        else
                            moi.STheSoCMT = Sthe;
                        //data.BNManTinhs.Add(moi);
                        data.SaveChanges();
                        MessageBox.Show("Sửa thành công");
                        frm_XnBNManTinh_Load(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sửa: " + ex.ToString());
                    }
                }
                else
                {
                    DialogResult Result = MessageBox.Show("Xác nhận Bệnh nhân: " + lbhoten.Text + "Là bệnh nhân mãn tính điều trị lâu dài", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        try
                        {
                            if (Dtuong == "Dịch vụ")
                            {
                                TTboXung sua = data.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                sua.CMT = txtsocmt.Text;
                                data.SaveChanges();
                            }
                            BNManTinh moi = new BNManTinh();
                            moi.MaBNhan = _mabn;
                            moi.MaICD = lupMaICDkb.Text;
                            moi.TenBenh = lupChanDoanKb.Text;
                            moi.MucDo = txtmucdo.Text;
                            if (Dtuong == "Dịch vụ")
                                moi.STheSoCMT = txtsocmt.Text;
                            else
                                moi.STheSoCMT = Sthe;
                            data.BNManTinhs.Add(moi);
                            data.SaveChanges();
                            MessageBox.Show("Lưu thành công");
                            frm_XnBNManTinh_Load(null, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi lưu: " + ex.ToString());
                        }

                    }
                }
            }
        }
        class c_ICD
        {
            string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        List<c_ICD> lICD = new List<c_ICD>();
        string Dtuong = "", Sthe = "";
        private void frm_XnBNManTinh_Load(object sender, EventArgs e)
        {
            dtngayxn.DateTime = DateTime.Now;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var TTbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            Sthe = "";
            if(TTbn!=null)
            {
                lbhoten.Text = TTbn.TenBNhan;
                lbdchi.Text = TTbn.DChi;
                Dtuong = TTbn.DTuong;
                Sthe = TTbn.SThe;
                if (Dtuong == "Dịch vụ")
                {
                    var Socmt = data.TTboXungs.Where(p => p.MaBNhan == _mabn).Select(p => p.CMT).FirstOrDefault();
                    if (Socmt != null)
                    {
                        txtsocmt.Text = Socmt;
                        Sthe = Socmt;
                    }
                    txtsocmt.Enabled = true;
                }
            }
            if(!string.IsNullOrEmpty(Sthe))
            {
                var BNmt = data.BNManTinhs.Where(p => p.STheSoCMT == Sthe).FirstOrDefault();
                if(BNmt!=null)
                {
                    lupMaICDkb.EditValue = BNmt.MaICD;
                    dtngayxn.DateTime = Convert.ToDateTime(BNmt.NgayXN);
                    txtmucdo.Text = BNmt.MucDo;
                }
            }
            lICD = (from ICD in data.ICD10 select new c_ICD { MaICD = ICD.MaICD ?? "", TenICD = ICD.TenICD ?? "" }).OrderBy(p => p.TenICD).ToList();
            lupChanDoanKb.Properties.DataSource = lICD.ToList();
            lupMaICDkb.Properties.DataSource = lICD.ToList();
            var bnkb = data.BNKBs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (bnkb != null)
            {
                lupMaICDkb.EditValue = bnkb.MaICD;
            }
        }

        private void lupChanDoanKb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupChanDoanKb.Text))
            {
                if (lupChanDoanKb.EditValue == null)
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                }
                else
                    //lupMaICDkb.EditValue = lupChanDoanKb.EditValue.ToString();
                    lupMaICDkb.EditValue = lICD.Where(p => p.TenICD == lupChanDoanKb.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();

            }
            else
            {
                lupMaICDkb.EditValue = "";
            }
        }

        private void lupMaICDkb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupMaICDkb.Text))
            {
                if (lupMaICDkb.EditValue.ToString() == null)
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                }
                else
                    //lupChanDoanKb.EditValue =lupMaICDkb.EditValue.ToString();
                    lupChanDoanKb.EditValue = lICD.Where(p => p.MaICD == lupMaICDkb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            }
            else
            {
                lupChanDoanKb.EditValue = "";
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool ktraxoa = true;
            var vp = data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
            if (vp.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán, không thể xóa");
                ktraxoa = false;
            }
            if(ktraxoa)
            {
                DialogResult Result = MessageBox.Show("Bạn muốn xóa xác nhận này", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                {
                    BNManTinh xoa = data.BNManTinhs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    data.BNManTinhs.Remove(xoa);
                    data.SaveChanges();
                    MessageBox.Show("Xóa thành công!");
                    frm_XnBNManTinh_Load(null, null);
                }

            }
        }
    }
}