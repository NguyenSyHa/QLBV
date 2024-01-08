using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_ThanhToanLai : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThanhToanLai()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        List<VienPhi> _lvienphi;
        bool xoaTT(int mabn)
        {
            try
            {
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var xoad = _data.VienPhis.Where(p => p.MaBNhan == mabn).ToList();
                foreach (var item in xoad)
                {
                    var dtct = _data.VienPhicts.Where(p => p.idVPhi == item.idVPhi).ToList();
                    foreach (var s in dtct)
                    {
                        _data.VienPhicts.Remove(s);
                    }
                    if (dtct.Count > 0)
                        _data.SaveChanges();
                    _data.VienPhis.Remove(item);
                    _data.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DialogResult _result = MessageBox.Show("Bạn muốn TT lại?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.No)
                return;

            progressBarControl1.Visible = true;
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = _lvienphi.Where(p => p.ExportBYT).ToList().Count;
            foreach (var a in _lvienphi)
            {
                if (a.ExportBYT == false)
                    continue;
                int _int_maBN = a.MaBNhan ?? 0;
                if (xoaTT(_int_maBN))
                {

                    progressBarControl1.EditValue = 0;
                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var rv = _data.RaViens.Where(p => p.MaBNhan == a.MaBNhan).FirstOrDefault();
                    int makp = rv.MaKP;
                    DateTime NgayRa = rv.NgayRa == null ? rv.NgayRa.Value.AddMinutes(10) : DateTime.Now;
                    if (DungChung.Ham.ThanhToan(_data, _int_maBN, NgayRa, makp))
                    {
                        var upadte = _data.VienPhis.Where(p => p.MaBNhan == a.MaBNhan).ToList();
                        foreach (var item in upadte)
                        {
                            item.MaGD_BHXH = "TTL";
                            _data.SaveChanges();
                        }
                        progressBarControl1.PerformStep();
                        progressBarControl1.Update();
                    }
                }
            }
            MessageBox.Show("Hoàn thành");
        }
        void timkiem()
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lvienphi = _data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden).Where(p => rad_ExPort.SelectedIndex == 0 ? (p.MaGD_BHXH == null || p.MaGD_BHXH == "") : p.MaGD_BHXH == "TTL").ToList();
            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _lvienphi;
        }
        private void frm_ThanhToanLai_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = DateTime.Now;
            dtTimDenNgay.DateTime = DateTime.Now;

        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            foreach (var a in _lvienphi)
            {
                a.ExportBYT = true;
            }
            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _lvienphi;
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            foreach (var a in _lvienphi)
            {
                a.ExportBYT = false;
            }
            grc_Export_XML_2348.DataSource = null;
            grc_Export_XML_2348.DataSource = _lvienphi;
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }

        private void rad_ExPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            timkiem();
        }
    }
}