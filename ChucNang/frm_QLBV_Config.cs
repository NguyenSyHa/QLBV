using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_QLBV_Config : Form
    {
        public frm_QLBV_Config()
        {
            InitializeComponent();
        }

        private void frm_QLBV_Config_Load(object sender, EventArgs e)
        {
            txtUrlPOS.EditValue = ConfigurationManager.AppSettings["URL_POS_AGRIBANK"];
            spSoLanHienThi.EditValue = ConfigurationManager.AppSettings["SoBenhNhanHienThi"];
            txtF1.EditValue = ConfigurationManager.AppSettings["F1"];
            txtF2.EditValue = ConfigurationManager.AppSettings["F2"];
            txtF3.EditValue = ConfigurationManager.AppSettings["F3"];
            txtF4.EditValue = ConfigurationManager.AppSettings["F4"];
            txtF5.EditValue = ConfigurationManager.AppSettings["F5"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddOrUpdateAppSettings("URL_POS_AGRIBANK", txtUrlPOS.EditValue);
            AddOrUpdateAppSettings("SoBenhNhanHienThi", spSoLanHienThi.EditValue);
            AddOrUpdateAppSettings("F1", txtF1.EditValue);
            AddOrUpdateAppSettings("F2", txtF2.EditValue);
            AddOrUpdateAppSettings("F3", txtF3.EditValue);
            AddOrUpdateAppSettings("F4", txtF4.EditValue);
            AddOrUpdateAppSettings("F5", txtF5.EditValue);
            UpdateBien();
            MessageBox.Show("Lưu thành công!");
            this.Close();
        }

        private static void AddOrUpdateAppSettings(string key, object value)
        {
            string _value = "";
            if (value == null)
                _value = "";
            else
                _value = value.ToString();
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, _value);
            }
            else
            {
                settings[key].Value = _value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void UpdateBien()
        {
            DungChung.Bien.URL_POS_AGRIGANK = txtUrlPOS.EditValue != null ? txtUrlPOS.EditValue.ToString() : "";
            DungChung.Bien.SoBenhNhanHienThi = spSoLanHienThi.EditValue != null ? Convert.ToInt32(spSoLanHienThi.EditValue) : 0;
            DungChung.Bien.F1 = txtF1.EditValue != null ? txtF1.EditValue.ToString() : "";
            DungChung.Bien.F2 = txtF2.EditValue != null ? txtF2.EditValue.ToString() : "";
            DungChung.Bien.F3 = txtF3.EditValue != null ? txtF3.EditValue.ToString() : "";
            DungChung.Bien.F4 = txtF4.EditValue != null ? txtF4.EditValue.ToString() : "";
            DungChung.Bien.F5 = txtF5.EditValue != null ? txtF5.EditValue.ToString() : "";
        }
    }
}
