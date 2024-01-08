using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QLBV.DungChung;

namespace QLBV.FormThamSo
{
    public partial class frm_LoadDuocXP : Form
    {
        public frm_LoadDuocXP()
        {
            InitializeComponent();
        }

        class File_XML
        {
            public bool check { set; get; }
            public string filename { set; get; }
            public string fulfilepath { set; get; }
            public DateTime datemodify { set; get; }

            public string trangthai { set; get; }
        }
        private void btn_ViewChiTiet_Click(object sender, EventArgs e)
        {
           
        
        }

        private void frm_KetNoiXP_Load(object sender, EventArgs e)
        {
            txtFromFileFolder.Text = QLBV.CLS.frm_filePath_LIS._connectArr[10];
            txtBackUP.Text = QLBV.CLS.frm_filePath_LIS._connectArr[11];

        }
        List<FileInfo> _lAllFileInfo = new List<FileInfo>();
        List<File_XML> _lAllFileXML = new List<File_XML>();
        List<File_XML> _lFileXML = new List<File_XML>();
        private void LoadDSFile()
        {
            try { 
            _lAllFileXML = new List<File_XML>();
            if (!string.IsNullOrEmpty(txtFromFileFolder.Text)) { 
            DirectoryInfo d = new DirectoryInfo(txtFromFileFolder.Text);
            FileInfo[] Files = d.GetFiles("*.xml");
            _lAllFileInfo = Files.OrderBy(p => p.LastWriteTime).ToList();
            foreach (FileInfo file in _lAllFileInfo)
            {
                File_XML filexml = new File_XML();
                cls_KetNoiXP_SA cls = new cls_KetNoiXP_SA();
                bool ck = cls.GetDichVu(file.FullName, false);
                filexml.check = ck;
                filexml.fulfilepath = file.FullName;
                filexml.filename = file.Name;
                filexml.datemodify = file.LastWriteTime;
                filexml.trangthai = ck == true ? "OK" : "Lỗi";
                _lAllFileXML.Add(filexml);
            }
            }
            grcFile.DataSource = _lAllFileXML;
            }
            catch
            {
                MessageBox.Show("không có dữ liệu");

            }
        }
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        private void btnChonFilePath_XML_Click(object sender, EventArgs e)
        {

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFromFileFolder.Text = dialog.SelectedPath;
                
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            //  _lFileXML = new List<File_XML>();
            List<string> lstStr = new List<string>();
            for (int i = 0; i < grvFile.DataRowCount; i++)
            {
                if (grvFile.GetRowCellValue(i, colCheck) != null && Convert.ToBoolean(grvFile.GetRowCellValue(i, colCheck)) == true && grvFile.GetRowCellValue(i, colTrangThai).ToString() == "OK")
                {
                    string fileName = grvFile.GetRowCellValue(i, colTenFile).ToString();
                    lstStr.Add(fileName);
                }
            }
            //   _lFileXML = (from a in _lAllFileXML join b in lstStr on a.filename equals b select a).ToList();

            cls_KetNoiXP_SA cls = new cls_KetNoiXP_SA();
            int count = cls.GetDSDichVu(txtFromFileFolder.Text, txtBackUP.Text, true, lstStr);
            if (count > 0)
            {
                MessageBox.Show("Đã đẩy thành công " + count.ToString() + " file");
                LoadDSFile();
            }

        }

        private void btnBackUpFolder_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBackUP.Text = dialog.SelectedPath;
            }
        }

        private void grvFile_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string filename = "";
            cls_KetNoiXP_SA cls = new cls_KetNoiXP_SA();
            List<QLBV.DungChung.cls_KetNoiXP_SA.Err> lstErr = new List<cls_KetNoiXP_SA.Err>();
            //  if (grvFile.GetRowCellValue(i, colCheck) != null && Convert.ToBoolean(grvFile.GetRowCellValue(i, colCheck)) == true && grvFile.GetRowCellValue(i, colTrangThai).ToString() == "OK")
            if (grvFile.GetFocusedRowCellValue(colTenFile) != null)
            {
                filename = grvFile.GetFocusedRowCellValue(colTenFile).ToString();
                if (grvFile.GetFocusedRowCellValue(colTrangThai) != null && grvFile.GetFocusedRowCellValue(colTrangThai).ToString() == "Lỗi")
                {
                   
                    cls.GetDichVu(txtFromFileFolder.Text + "\\" + filename, false);
                    lstErr = cls._lstErr1TH;
                    lstErr.AddRange(cls._listErr1Don);
                    if (lstErr.Count <= 0)
                        lstErr.Add(new QLBV.DungChung.cls_KetNoiXP_SA.Err { Mss = "Không đúng định dạng file" });
                  
                }
            }
            grcErr.DataSource = lstErr;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //  _lFileXML = new List<File_XML>();
            List<string> lstStr = new List<string>();
            for (int i = 0; i < grvFile.DataRowCount; i++)
            {
                if (grvFile.GetRowCellValue(i, colCheck) != null && Convert.ToBoolean(grvFile.GetRowCellValue(i, colCheck)) == true && grvFile.GetRowCellValue(i, colTrangThai).ToString() == "OK")
                {
                    string fileName = grvFile.GetRowCellValue(i, colTenFile).ToString();
                    lstStr.Add(fileName);
                }
            }
            //   _lFileXML = (from a in _lAllFileXML join b in lstStr on a.filename equals b select a).ToList();

            cls_KetNoiXP_SA cls = new cls_KetNoiXP_SA();
           // int count = cls.GetDSDichVu(txtFromFileFolder.Text, txtBackUP.Text, true, lstStr);
            int count = cls.GetDSDichVu(txtFromFileFolder.Text, txtBackUP.Text, true, lstStr);
            if (count > 0)
            {
                MessageBox.Show("Đã đẩy thành công " + count.ToString() + " file");
                LoadDSFile();
            }
        }

        private void txtFromFileFolder_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtBackUP_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void TimKiem_Click(object sender, EventArgs e)
        {
            LoadDSFile();
        }

    }

}
