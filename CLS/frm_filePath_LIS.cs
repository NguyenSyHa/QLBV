using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml.Linq;
using System.Linq;
using System.IO.Ports;

namespace QLBV.CLS
{
    public partial class frm_filePath_LIS : DevExpress.XtraEditors.XtraForm
    {
        public frm_filePath_LIS()
        {
            InitializeComponent();
        }
        #region luu connect
        private void LuuConnect()
        {

            string _chuoikn = txt_2348.Text.Trim() + "*" + txt_2348_bak.Text.Trim() + "*" + txtTuDong.Text.Trim() + "*" + txtTime_post9324.Text + "*" + txt_Post_9324_IP.Text + "*" + txt_user_post_9324.Text.Trim() + "*" + txt_pass_post_9324.Text +
            "*7*8*9*" + txt_path_Get9324.Text.Trim() + "*" + txt_path_Get9324_bak.Text.Trim() + "*" + txt_CTTD_Get9324.Text.Trim() + "*" + txt_Time_Get9324.Text.Trim() + "*" + txt_Get_9324_IP.Text + "*" + txt_user_Get9324.Text.Trim() + "*" + txt_pass_Get9324.Text;
            QLBV_Library.QLBV_Ham.Write_update("Cuong.F9324", _chuoikn);

        }
        #endregion
        #region Get connect
        public static string[] _connectArr = new string[20];
        public static void _getConnect()
        {
            string _connect = "";
            _connect = QLBV_Library.QLBV_Ham.Read_Update("Cuong.F9324", 1);
            if (!string.IsNullOrEmpty(_connect))
                _connectArr = _connect.Split('*');
        }
        #endregion
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                if (!string.IsNullOrWhiteSpace(txtKiHieu.Text))
                {
                    var spKiHieu = txtKiHieu.Text.Split(':');
                    if (spKiHieu.Count() != 2)
                    {
                        MessageBox.Show("Ký hiệu hóa đơn phải có định dạng: {Ký hiệu hóa đơn có thuế}:{Ký hiệu hóa đơn không thuế}");
                        return;
                    }
                }
            }
            string[] str = new string[44];
            str[0] = txt_DD_CD.Text.Trim() + ";";
            str[1] = txtDD_KQ.Text.Trim() + ";";
            str[2] = txtDD_KQ_bak.Text.Trim() + ";";
            str[3] = txt_2348.Text.Trim() + ";";
            str[4] = txt_2348_bak.Text.Trim() + ";";
            str[5] = txt_benhAn.Text.Trim() + ";";
            str[6] = txt_Export.Text.Trim() + ";";
            str[7] = txt_GuiBHXH.Text.Trim() + ";";
            str[8] = txt_GuiBHXHbak.Text.Trim() + ";";
            str[9] = txt_GocBHXH.Text.Trim() + ";";
            str[10] = txtUserBHXH.Text.Trim() + ";";
            str[11] = txtPassBHXH.Text.Trim() + ";";
            str[12] = txtGuiBYT.Text.Trim() + ";";
            str[13] = txtGuiBYT_bak.Text.Trim() + ";";
            str[14] = txtUserBYT.Text.Trim() + ";";
            str[15] = txtPassBYT.Text.Trim() + ";";
            //thiết lập HĐ Viettel
            str[16] = txtDuongDanHD.Text.Trim() + ";";
            str[17] = txtMaSoThue.Text.Trim() + ";";
            str[18] = txtUserSvoice.Text.Trim() + ";";
            str[19] = txtURL.Text.Trim() + ";";
            str[20] = rgCTY.SelectedIndex.ToString() + ";";
            str[21] = txtLoaiHD.Text.Trim() + ";";
            str[22] = txtMauHD.Text.Trim() + ";";
            str[23] = txtKiHieu.Text.Trim() + ";";
            //thiết lập HĐ VNPT
            str[24] = txtPathHDVNPT.Text.Trim() + ";";
            str[25] = txtUserAdminVNPT.Text.Trim() + ";";
            str[26] = txtPassAdminVPT.Text.Trim() + ";";
            str[27] = txtUserWSVNPT.Text.Trim() + ";";
            str[28] = txtPassWSVPT.Text.Trim() + ";";
            str[29] = txtPublish.Text.Trim() + ";";
            str[30] = txtBusiness.Text.Trim() + ";";
            str[31] = txtPortal.Text.Trim() + ";";
            str[32] = txtMauHDVNPT.Text.Trim() + ";";
            str[33] = txtSerialVNPT.Text.Trim() + ";";
            str[40] = grKieuChay.SelectedIndex.ToString() + ";";

            //thiết lập gửi HSSK _VietTel
            str[34] = txtGetPID.Text.Trim() + ";";
            str[35] = txtPostHSSK.Text.Trim() + ";";
            str[36] = txtUserPID.Text.Trim() + ";";
            str[37] = txtPassPID.Text.Trim() + ";";
            str[38] = txtUserHSSK.Text.Trim() + ";";
            str[39] = txtPassHSSK.Text.Trim() + ";";

            ///Thiết lập gửi HSYTCS
            str[41] = txtClientID.Text.Trim() + ";";
            str[42] = txtUser.Text.Trim() + ";";
            str[43] = txtPassWord.Text.Trim() + ";";




            var xEle = new XElement("xml",
                   new XElement("Connect_LIS", str[0] + str[1] + str[2]),
                   new XElement("Connect_2348", str[3] + str[4]),
                   new XElement("duyetbenhan", str[5]),
                   new XElement("Export", str[6]),
                    new XElement("BHXH", str[7] + str[8] + str[9] + str[10] + str[11]),
                    new XElement("BYT", str[12] + str[13] + str[14] + str[15]),
                    new XElement("SVOICEVIETTEL", str[16] + str[17] + str[18] + str[19] + str[20] + str[21] + str[22] + str[23]),
                      new XElement("SVOICEVNPT", str[24] + str[25] + str[26] + str[27] + str[28] + str[29] + str[30] + str[31] + str[32] + str[33]),
                      new XElement("HSSK", str[34] + str[35] + str[36] + str[37] + str[38] + str[39]),
                       new XElement("SVOICE", str[40]),
                       new XElement("client_id", str[41]),
                       new XElement("username", str[42]),
                       new XElement("password", str[43])
                   );
            // string path =  System.IO.Directory.GetCurrentDirectory() + "\\xmlFilePath.xml";
            xEle.Save(path);
            GetConnect_LisFolder();
            LuuConnect();
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kho = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (kho != null && !string.IsNullOrEmpty(txtURLGD.Text))
            {
                kho.URL = txtURLGD.Text;
                _data.SaveChanges();
            }

            //HTHONG _ht=_data.HTHONGs.to
            this.Close();
        }

        public static void GetConnect_LisFolder()
        {
            if (System.IO.File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                string str_cn = "";
                XElement xelement = doc.Element("xml").Element("Connect_LIS");
                if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("Connect_2348");
                if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("duyetbenhan");
                if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("Export");
                if (xelement != null)
                    str_cn += xelement.Value;
                else
                    str_cn += "D:";

                xelement = doc.Element("xml").Element("BHXH"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("BYT"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("SVOICEVIETTEL"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("SVOICEVNPT"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("HSSK"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("SVOICE"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("client_id"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("username"); if (xelement != null)
                    str_cn += xelement.Value;
                xelement = doc.Element("xml").Element("password"); if (xelement != null)
                    str_cn += xelement.Value;
                DungChung.Bien.xmlFilePath_LIS = str_cn.Split(';');
                if (!string.IsNullOrEmpty(DungChung.Bien.UserPasswordHDDT))
                {
                    var splitUP = DungChung.Bien.UserPasswordHDDT.Split(':');
                    if (splitUP.Count() > 1)
                    {
                        DungChung.Bien.xmlFilePath_LIS[18] = DungChung.Bien.UserPasswordHDDT;
                        DungChung.Bien.xmlFilePath_LIS[25] = splitUP[0];
                        DungChung.Bien.xmlFilePath_LIS[26] = splitUP[1];
                    }
                }

            }

        }
        private static string path = System.IO.Directory.GetCurrentDirectory() + "\\xmlFilePath.xml";
        private void frm_filePath_LIS_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kho = _data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (kho != null && !string.IsNullOrEmpty(kho.URL))
            {
                txtURLGD.Text = kho.URL;
            }
            string[] str2 = DungChung.Bien.xmlFilePath_LIS;
            string[] str = new string[str2.Length];
            for (int i = 0; i < str2.Length; i++)
                str[i] = "";


            for (int i = 0; i < str2.Length; i++)
            {
                str[i] = str2[i];
            }
            if (str.Length < 43)
            {
                MessageBox.Show("Lỗi đọc file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txt_DD_CD.Text = str[0];
            txtDD_KQ.Text = str[1];
            txtDD_KQ_bak.Text = str[2];
            txt_2348.Text = str[3];
            txt_2348_bak.Text = str[4];
            txt_benhAn.Text = str[5];
            txt_Export.Text = str[6];
            txt_GuiBHXH.Text = str[7];
            txt_GuiBHXHbak.Text = str[8];
            txt_GocBHXH.Text = str[9];
            txtUserBHXH.Text = str[10];
            txtPassBHXH.Text = str[11];
            txtGuiBYT.Text = str[12];
            txtGuiBYT_bak.Text = str[13];
            txtUserBYT.Text = str[14];
            txtPassBYT.Text = str[15];
            //Viettel
            txtDuongDanHD.Text = str[16];
            txtMaSoThue.Text = str[17];
            txtUserSvoice.Text = str[18];
            txtURL.Text = str[19];
            rgCTY.SelectedIndex = string.IsNullOrEmpty(str[20]) ? 0 : Convert.ToInt32(str[20]);
            txtLoaiHD.Text = str[21];
            txtMauHD.Text = str[22];
            txtKiHieu.Text = str[23];
            //VNPT
           
            txtPathHDVNPT.Text = str[24];
            txtUserAdminVNPT.Text = str[25];
            txtPassAdminVPT.Text = str[26];
            txtUserWSVNPT.Text = str[27];
            txtPassWSVPT.Text = str[28];
            txtPublish.Text = str[29];
            txtBusiness.Text = str[30];
            txtPortal.Text = str[31];
            txtMauHDVNPT.Text = str[32];
            txtSerialVNPT.Text = str[33];
            grKieuChay.SelectedIndex = string.IsNullOrEmpty(str[40]) ? 0 : Convert.ToInt32(str[40]);

            // HSSK
            txtGetPID.Text = str[34];
            txtPostHSSK.Text = str[35];
            txtUserPID.Text = str[36];
            txtPassPID.Text = str[37];
            txtUserHSSK.Text = str[38];
            txtPassHSSK.Text = str[39];

            //HSYTCS
            txtClientID.Text = str[41];
            txtUser.Text = str[42];
            txtPassWord.Text = str[43];

            txtXMLPath.Text = System.IO.Directory.GetCurrentDirectory();
            _getConnect();
            if (!string.IsNullOrEmpty(_connectArr[2]))
                txtTuDong.Text = _connectArr[2];
            if (!string.IsNullOrEmpty(_connectArr[3]))
                txtTime_post9324.Text = _connectArr[3];
            if (!string.IsNullOrEmpty(_connectArr[4]))
                txt_Post_9324_IP.Text = _connectArr[4];
            if (!string.IsNullOrEmpty(_connectArr[5]))
                txt_user_post_9324.Text = _connectArr[5];
            if (!string.IsNullOrEmpty(_connectArr[6]))
                txt_pass_post_9324.Text = _connectArr[6];
            if (!string.IsNullOrEmpty(_connectArr[10]))
                txt_path_Get9324.Text = _connectArr[10];
            if (!string.IsNullOrEmpty(_connectArr[11]))
                txt_path_Get9324_bak.Text = _connectArr[11];
            if (!string.IsNullOrEmpty(_connectArr[12]))
                txt_CTTD_Get9324.Text = _connectArr[12];
            if (!string.IsNullOrEmpty(_connectArr[13]))
                txt_Time_Get9324.Text = _connectArr[13];
            if (!string.IsNullOrEmpty(_connectArr[14]))
                txt_Get_9324_IP.Text = _connectArr[14];
            if (!string.IsNullOrEmpty(_connectArr[15]))
                txt_user_Get9324.Text = _connectArr[15];
            if (!string.IsNullOrEmpty(_connectArr[16]))
                txt_pass_Get9324.Text = _connectArr[16];

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        private void btnDuongDan_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[0];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DD_CD.Text = dialog.SelectedPath;
            }
        }

        private void btn_DD_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[1];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDD_KQ.Text = dialog.SelectedPath;
            }
        }

        private void btn_DD3_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[2];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDD_KQ_bak.Text = dialog.SelectedPath;
            }
        }
        private void btnXML_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[1];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtXMLPath.Text = dialog.SelectedPath;
                path = txtXMLPath.Text + "\\xmlFilePath.xml";
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[3];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_2348.Text = dialog.SelectedPath;
            }
        }

        private void btn_2348_bak_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[4];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_2348_bak.Text = dialog.SelectedPath;
            }
        }

        private void btn_benhAn_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[5];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_benhAn.Text = dialog.SelectedPath;
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = DungChung.Bien.xmlFilePath_LIS[6];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_Export.Text = dialog.SelectedPath;
            }
        }

        private void btn_TuDong_Click(object sender, EventArgs e)
        {
            dialog.SelectedPath = txtTuDong.Text = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtTuDong.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_path_Get9324.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_path_Get9324_bak.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_CTTD_Get9324.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_GuiBHXH.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_GuiBHXHbak.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_GocBHXH.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtGuiBYT.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtGuiBYT_bak.Text = dialog.SelectedPath;
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDanHD.Text = dialog.SelectedPath;
            }
        }

        private void txtURL_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtUserSvoice_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rgCTY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgCTY.SelectedIndex == 0)
            {
                xtrViettel.PageEnabled = true;
                xtraTabControl1.SelectedTabPage = xtrViettel;
                xtrVNPT.PageEnabled = false;
            }
            else
            {
                xtrVNPT.PageEnabled = true;
                xtraTabControl1.SelectedTabPage = xtrVNPT;
                xtrViettel.PageEnabled = false;
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathHDVNPT.Text = dialog.SelectedPath;
            }
        }

        private void labelControl41_Click(object sender, EventArgs e)
        {

        }

        private void labelControl40_Click(object sender, EventArgs e)
        {

        }

       

        private void btnGetPID_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtGetPID.Text = dialog.SelectedPath;
            }

        }

        private void btnPostHSSK_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPostHSSK.Text = dialog.SelectedPath;
            }

        }

        private void groupControl8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}