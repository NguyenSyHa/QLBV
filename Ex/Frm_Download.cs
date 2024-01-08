using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Net;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_Download : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Download()
        {
            InitializeComponent();
        }
        
        string taikhoan = "";
        string MK = "";
        private class DS
        {
            private int stt;
            private string ten;
            private bool chon;
            public int SoTT
            { set { stt = value; } get { return stt; } }
            public string Tenfile
            { set { ten = value; } get { return ten; } }
            public bool Chon
            { set { chon = value; } get { return chon; } }
        }
        List<DS> _DS = new List<DS>();
        string _fileroot = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            taikhoan = txtTaikhoan.Text;
            MK = txtMatkhau.Text;
            //ftp ftpClient = new ftp();
            sbtDownload.Enabled = true;
            Frm_Upload.ftp ftpClient = new Frm_Upload.ftp(@"ftp://118.70.117.247/", taikhoan, MK);
            if (ftpClient.checkserver())
            {
                
                //MessageBox.Show("Login thành công");
                sbtDownload.Enabled = true;
                sbtBR2.Enabled = true;
                _fileroot = DungChung.Bien.MaBV;
                string[] list;
                list = ftpClient.directoryListSimple(_fileroot);
                int st=1;
                _DS.Clear();
                DS moi = new DS();
                moi.Chon = true;
                moi.SoTT = 0;
                moi.Tenfile = "Chọn tất cả";
                _DS.Add(moi);
                foreach (var a in list)
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        DS themmoi = new DS();
                        themmoi.SoTT = st;
                        themmoi.Tenfile = a.ToString();
                        themmoi.Chon = true;
                        _DS.Add(themmoi);
                        st = st+1;
                    }
                }
                GrcDS.DataSource = _DS;
            }
        }

        private void Frm_Download_Load(object sender, EventArgs e)
        {
            
             simpleButton1_Click(sender, e);
        }

        private void txtTaikhoan_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtMatkhau_EditValueChanged(object sender, EventArgs e)
        {
         
        }

        private void sbtBR2_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string _fileanh = string.Empty;
            FolderBrowserDialog op = new FolderBrowserDialog();
            //op.Multiselect = true;
            
            //op.Filter = "XML (*.xml)|*.xml|All files (*.*)|*.*";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = op.SelectedPath;
                ////string b1 = op
                //_fileanh = fileName;
                //int i = op.SafeFileNames.Length;
                //a1 = new string[i];
                //int j = 0;
                //foreach (var a in op.FileNames)
                //{
                //    a1[j] = a.ToString();
                //    j = j + 1;
                //}
                if (!string.IsNullOrEmpty(fileName))
                    txtDuongdan.Text = fileName;
                else
                    txtDuongdan.Text = "";
            }
        }

        private void sbtDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDuongdan.Text))
            {
                //int a = _DS.Count();
                taikhoan = txtTaikhoan.Text;
                MK = txtMatkhau.Text;
                Frm_Upload.ftp ftpClient = new Frm_Upload.ftp(@"ftp://118.70.117.247/", taikhoan, MK);

                string _duongdan = txtDuongdan.Text;
                bool a = true;
                foreach (var b in _DS)
                {
                    if (b.Chon && b.SoTT != 0)
                    {
                        string _ten = b.Tenfile;
                        string localFile = _duongdan + "/" + _ten;
                        string remoteFile = _fileroot + "/" + _ten;
                        try
                        {
                            ftpClient.download(remoteFile, localFile);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi tải về");
                            a = false;
                        }
                    }
                }
                if (a)
                { MessageBox.Show("Tải về thành công"); }


            }
            else
            { MessageBox.Show("Bạn cần chọn đường dẫn  lưu file!"); }
        }

        private void GrvDS_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "chk")
            {
                if (GrvDS.GetFocusedRowCellValue("Tenfile") != null)
                {
                    string Ten = GrvDS.GetFocusedRowCellValue("Tenfile").ToString();
                    if (Ten == "Chọn tất cả")
                    {
                        if (_DS.First().Chon == true)
                        {
                            foreach (var a in _DS)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _DS)
                            {
                                a.Chon = true;
                            }
                        }
                        GrcDS.DataSource = "";
                        GrcDS.DataSource = _DS.ToList();
                    }
                }
            }
        }

        private void txtTaikhoan_Leave(object sender, EventArgs e)
        {
            simpleButton1_Click(sender, e);
        }

        private void txtMatkhau_Leave(object sender, EventArgs e)
        {
            simpleButton1_Click(sender, e);
        }
    }
}