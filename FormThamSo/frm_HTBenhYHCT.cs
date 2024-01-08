using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace QLBV.FormThamSo
{
    public partial class frm_HTBenhYHCT : DevExpress.XtraEditors.XtraForm
    {
        string _ICD = "", _ICD2 = "", _ICD3 = "", _ICDKhac = "";
        int _idkb = 0;
        List<ICD10> _licd = new List<ICD10>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_HTBenhYHCT(string ICD, string ICD2, string ICD3, string ICDKhac, List<ICD10> licd, int idkb)
        {
            InitializeComponent();
            _ICD = ICD;
            _ICD2 = ICD2;
            _ICD3 = ICD3;
            _ICDKhac = ICDKhac;
            _licd = licd;
            _idkb = idkb;
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupBenhPhu2_EditValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(lupBenhPhu2.Text))
            {
                if (lupBenhPhu2.EditValue.ToString() == "0")
                {
                    lupBenhPhu2.EditValue = "";
                    lupMaBenh.EditValue = "";
                    lupBenhPhu2.Text = "";
                }
                else
                {
                    if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272")
                    {
                        lupMaBenh2.EditValue = _licd.Where(p => p.MaICD == _ICD2).Select(p => p.MaYHCT).FirstOrDefault();
                    }
                    else
                    {
                        lupMaBenh2.EditValue = _licd.Where(p => p.TenYHCT == lupBenhPhu2.EditValue.ToString()).Select(p => p.MaYHCT).FirstOrDefault();
                    }

                    txtBenhPhu2.EditValue = _licd.Where(p => p.MaYHCT == lupMaBenh2.EditValue.ToString()).Select(p => p.TenYHCT).FirstOrDefault();
                }
            }
            else
            {
                lupBenhPhu2.Text = "";
                txtBenhPhu2.Text = "";
            }
        }

        private void lupBenhChinh_EditValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(lupBenhChinh.Text))
            {
                if (lupBenhChinh.EditValue.ToString() == "0")
                {
                    lupBenhChinh.EditValue = "";
                    lupMaBenh.EditValue = "";
                    lupBenhChinh.Text = "";
                }
                else
                {
                    if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272")
                        lupMaBenh.EditValue = _licd.Where(p => p.MaICD == _ICD).Select(p => p.MaYHCT).FirstOrDefault();
                    else
                        lupMaBenh.EditValue = _licd.Where(p => p.TenYHCT == lupBenhChinh.EditValue.ToString()).Select(p => p.MaYHCT).FirstOrDefault();

                    txtBenhChinh.EditValue = _licd.Where(p => p.MaYHCT == lupMaBenh.EditValue.ToString()).Select(p => p.TenYHCT).FirstOrDefault();
                }
            }
            else
            {
                lupBenhChinh.Text = "";
                txtBenhChinh.Text = "";
            }
        }

        private void lupMaBenh_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaBenh.EditValue != null)
            {
                txtBenhChinh.EditValue = _licd.Where(p => p.MaYHCT == lupMaBenh.EditValue.ToString()).Select(p => p.TenYHCT).FirstOrDefault();
            }
        }

        private void frm_HTBenhYHCT_Load(object sender, EventArgs e)
        {
            var tenyhct = (from a in _licd select new { MaYHCT = a.MaYHCT, TenYHCT = a.TenYHCT }).ToList();
            lupBenhChinh.Properties.DataSource = tenyhct;
            lupBenhPhu2.Properties.DataSource = tenyhct;
            lupMaBenh.Properties.DataSource = tenyhct;
            lupMaBenh2.Properties.DataSource = tenyhct;

            if (_idkb > 0)
            {
                var kb = _data.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault();
                if (kb != null)
                {
                    if (string.IsNullOrEmpty(kb.MaYHCT) && string.IsNullOrEmpty(kb.MaYHCT2))
                    {

                        kb.MaYHCT = DungChung.Ham.GetMaYHCT(kb.MaICD, _licd)[0];
                        kb.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(kb.MaICD, _licd)[1];
                        kb.MaYHCT2 = DungChung.Ham.GetMaYHCT(kb.MaICD2, _licd)[0];
                        kb.BenhKhacYHCT = DungChung.Ham.GetMaYHCT(kb.MaICD2, _licd)[1];
                        _data.SaveChanges();
                    }
                }
            }
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018")
            {
                txtBenhChinh.Enabled = txtBenhPhu2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = false;
                var kb = _data.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault();
                if (kb != null)
                {

                    var maicd = _licd.Where(p => p.MaICD == _ICD2).FirstOrDefault();
                    lupMaBenh.EditValue = txtMaICD.Text = kb.MaYHCT;
                    lupMaBenh2.EditValue = txtMaICD2.Text = Splitstring(kb.MaYHCT2)[0];
                    txtBenhChinh.Text = txtChanDoan.Text = kb.ChanDoanYHCT;
                    lupBenhPhu2.EditValue = txtBenhPhu2.Text = txtChanDoan2.Text = Splitstring(kb.BenhKhacYHCT)[0];
                    txtICD10.Text = _ICD;
                    txtICD102.Text = _ICD2;
                }

            }
            else if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24272")
            {
                txtBenhChinh.Enabled = txtBenhPhu2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = lupMaBenh.Enabled = false;
                var kb = _data.BNKBs.Where(p => p.IDKB == _idkb).FirstOrDefault();
                if (kb != null)
                {

                    var maicd1 = _licd.Where(p => p.MaICD == _ICD).FirstOrDefault();
                    lupMaBenh.Text = kb.MaYHCT;
                    txtBenhChinh.Text = kb.ChanDoanYHCT;
                    //lupMaBenh.EditValue = txtMaICD.Text = maicd1.MaYHCT;
                    //txtBenhChinh.Text = txtChanDoan.Text = kb.ChanDoanYHCT;
                    txtICD10.Text = _ICD;
                    if (!string.IsNullOrEmpty(_ICD2))
                    {
                        var maicd2 = _licd.Where(p => p.MaICD == _ICD2).FirstOrDefault();
                        //lupMaBenh2.EditValue = txtMaICD2.Text = maicd2.MaYHCT;
                        //lupBenhPhu2.EditValue = txtBenhPhu2.Text = txtChanDoan2.Text = maicd2.TenYHCT;
                        txtICD102.Text = _ICD2;
                        lupMaBenh2.Text = kb.MaYHCT2.Split(';')[0];
                        txtBenhPhu2.Text = kb.BenhKhacYHCT.Split(';')[0];
                    }
                    else
                    {
                        lupMaBenh2.EditValue = txtMaICD2.Text = "";
                        lupBenhPhu2.EditValue = txtBenhPhu2.Text = txtChanDoan2.Text = "";
                        txtICD102.Text = "";
                    }

                    //var maicd_check = _licd.Where(p => p.MaYHCT == kb.MaYHCT).FirstOrDefault();


                }
            }
            else
            {
                btnSua.Visible = txtBenhChinh.Visible = txtBenhPhu2.Visible = lupMaBenh.Visible = lupMaBenh2.Visible = lupMaBenh.Visible = lupMaBenh2.Visible = false;

                if (!string.IsNullOrEmpty(_ICD))
                {
                    var maicd = _licd.Where(p => p.MaICD == _ICD).FirstOrDefault();
                    if (maicd != null)
                    {
                        txtChanDoan.Text = maicd.TenYHCT;
                        txtMaICD.Text = maicd.MaYHCT;
                        txtICD10.Text = _ICD;
                    }
                }
                else
                {
                    txtChanDoan.Text = "";
                    txtMaICD.Text = "";
                    txtICD10.Text = "";
                }
                if (!string.IsNullOrEmpty(_ICD2))
                {
                    var maicd = _licd.Where(p => p.MaICD == _ICD2).FirstOrDefault();
                    if (maicd != null)
                    {
                        txtChanDoan2.Text = maicd.TenYHCT;
                        txtMaICD2.Text = maicd.MaYHCT;
                        txtICD102.Text = _ICD2;
                    }
                }
                else
                {
                    txtChanDoan2.Text = "";
                    txtMaICD2.Text = "";
                    txtICD102.Text = "";
                }


            }
            if (!string.IsNullOrEmpty(_ICD3))
            {
                var maicd = _licd.Where(p => p.MaICD == _ICD3).FirstOrDefault();
                if (maicd != null)
                {
                    txtChanDoan3.Text = maicd.TenYHCT;
                    txtMaICD3.Text = maicd.MaYHCT;
                    txtICD103.Text = _ICD3;
                }
            }
            else
            {
                txtChanDoan3.Text = "";
                txtMaICD3.Text = "";
                txtICD103.Text = "";
            }
            if (!string.IsNullOrEmpty(_ICDKhac))
            {
                if (_ICDKhac.Contains(";"))
                {
                    List<string> q1 = _ICDKhac.Split(';').Distinct().ToList();

                    var maicd = (from a in q1
                                 join icd in _licd on a equals icd.MaICD
                                 select new
                                 {
                                     icd
                                 }).ToList();
                    if (maicd.Count() > 0)
                    {
                        txtBenhKhac.Text = string.Join(";", maicd.Select(p => p.icd.TenYHCT).ToArray());
                        txtMaICDKhac.Text = string.Join(";", maicd.Select(p => p.icd.MaYHCT).ToArray());
                        txtICD10Khac.Text = _ICDKhac;
                    }
                }
                else
                {
                    var maicd = _licd.Where(p => p.MaICD == _ICDKhac).FirstOrDefault();
                    if (maicd != null)
                    {
                        txtBenhKhac.Text = maicd.TenYHCT;
                        txtMaICDKhac.Text = maicd.MaYHCT;
                        txtICD10Khac.Text = _ICDKhac;
                    }
                }
            }
            else
            {
                txtBenhKhac.Text = "";
                txtMaICDKhac.Text = "";
                txtICD10Khac.Text = "";
            }
        }
        private string[] Splitstring(string value)
        {
            string[] Setvalue = new string[4];
            if (!string.IsNullOrEmpty(value))
            {
                Setvalue = value.Split(';');
            }
            return Setvalue;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBenhPhu2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (lupBenhPhu2.IsPopupOpen)
                    lupBenhPhu2.ClosePopup();
                else
                    lupBenhPhu2.ShowPopup();
            }
        }

        private void txtBenhChinh_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (lupBenhChinh.IsPopupOpen)
                    lupBenhChinh.ClosePopup();
                else
                    lupBenhChinh.ShowPopup();
            }

        }





        private void lupMaBenh2_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaBenh2.EditValue != null)
            {
                txtBenhPhu2.EditValue = _licd.Where(p => p.MaYHCT == lupMaBenh2.EditValue.ToString()).Select(p => p.TenYHCT).FirstOrDefault();
            }

        }
        int i = 0;
        private void btnSua_Click(object sender, EventArgs e)
        {

            if (i == 0)
            {
                txtBenhChinh.Enabled = txtBenhPhu2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = true;
                txtBenhChinh.ReadOnly = txtBenhPhu2.ReadOnly = lupMaBenh.ReadOnly = lupMaBenh2.ReadOnly = lupMaBenh.ReadOnly = lupMaBenh2.ReadOnly = false;
                i = 1;
                btnSua.Text = "Lưu";
            }
            else
            {

                BNKB kb = _data.BNKBs.Where(p => p.IDKB == _idkb).Single();
                kb.MaYHCT = lupMaBenh.Text;
                kb.ChanDoanYHCT = txtBenhChinh.Text;
                kb.MaYHCT2 = lupMaBenh2.Text + ";" + txtMaICD3.Text + ";" + txtMaICDKhac.Text;
                kb.BenhKhacYHCT = txtBenhPhu2.Text + ";" + txtChanDoan3.Text + ";" + txtBenhKhac.Text;
                _data.SaveChanges();
                txtBenhChinh.ReadOnly = txtBenhPhu2.ReadOnly = lupMaBenh.ReadOnly = lupMaBenh2.ReadOnly = lupMaBenh.ReadOnly = lupMaBenh2.ReadOnly = true;
                txtBenhChinh.Enabled = txtBenhPhu2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = lupMaBenh.Enabled = lupMaBenh2.Enabled = false;
                i = 0;
                btnSua.Text = "Sửa";
            }


        }
    }
}