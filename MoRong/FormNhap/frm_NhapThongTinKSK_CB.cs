using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Xml.Linq;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

using System.Diagnostics;
namespace QLBV.FormNhap
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frm_NhapThongTinKSK_CB : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapThongTinKSK_CB()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;     
        int trangthai = -1;//-1: reset; 0: thêm mới, 1: Sửa, 2: Xem
       // bool load = false;
       
        private void frm_NhapThongTinKSK_CB_Load(object sender, EventArgs e)
        {           
            txtDuongDan.Text = QLBV_Library.QLBV_Ham.Read_registry("XML_CB_Path", 1);
            path = QLBV_Library.QLBV_Ham.Read_registry("XML_CB_Path", 1);           
            if (!File.Exists(path))
            {               
                path = txtDuongDan.Text; 
            }          
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            listBCN = new List<LichSuBenh>();
            listBNN = new List<LichSuBenh>();
            var cb = (from dscb in data.CanBoes
                      join kp in data.KPhongs on dscb.MaKP equals kp.MaKP
                      select new
                      {
                          dscb.MaCB,
                          kp.TenKP,
                          dscb.TenCB,
                          GioiTinh = dscb.GioiTinh == 0 ? "Nữ" : "Nam",
                          dscb.DiaChi,
                          dscb.ChucVu,
                      }
                    ).OrderBy(p => p.TenCB).ToList();
            lupCB.Properties.DataSource = cb;
            dtNgayBDLamViec.EditValue = DateTime.Now;
            dtNgayCap.EditValue = DateTime.Now;
            loadNam();
            cboNam.SelectedItem = DateTime.Now.Year;
            GetDSCB();
            setReadOnly();            
            bintienSuBenh.DataSource = listBCN;
            bintiensuNN.DataSource = listBNN;
            grc_TienSuBenh.DataSource = bintienSuBenh;
            grc_TiensuBNN.DataSource = bintiensuNN;  
        }
        private void loadNam()
        {
            List<int> list = new List<int>();
            int year = DateTime.Now.Year;
            for (int i = year - 20; i < year + 20; i++)
            {
                list.Add(i);
            }
            this.cboNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNam.Properties.Items.AddRange(list);

        }
        private void txtTimkiem_EditValueChanged(object sender, EventArgs e)
        {
            GetDSCB();
        }
        string path = "";
        private void GetDSCB()
        {
            path = txtDuongDan.Text;
            XDocument doc = new XDocument();
            if (System.IO.File.Exists(path))
            {
                doc = XDocument.Load(path);
                string tk = txtTimkiem.Text.ToLower();
                if (tk == ("Mã/Tên CB").ToLower())
                    tk = "";
                string nam = cboNam.SelectedIndex > 0 ? cboNam.SelectedItem.ToString() : DateTime.Now.Year.ToString();
                var listCB = (from bn in
                                  (from n in doc.Descendants("str" + nam) select n).Descendants("Benhnhan")
                              where (tk == "" || (bn.Element("MaBNhan") != null && bn.Element("TenBNhan") != null && (bn.Element("MaBNhan").Value.ToLower().Contains(tk) || bn.Element("TenBNhan").Value.ToLower().Contains(tk))))
                              select new
                              {
                                  MaBNhan = bn.Element("MaBNhan") == null ? "" : bn.Element("MaBNhan").Value,
                                  TenBNhan = bn.Element("TenBNhan") == null ? "" : bn.Element("TenBNhan").Value,
                                  Tuoi = bn.Element("Tuoi") == null ? "" : bn.Element("Tuoi").Value,
                                  GTinh = bn.Element("GTinh") == null ? "" : bn.Element("GTinh").Value,
                                  DiaChi = bn.Element("DiaChi") == null ? "" : bn.Element("DiaChi").Value,
                                  ChucVu = bn.Element("ChucVu") == null ? "" : bn.Element("ChucVu").Value,
                              }).ToList();
                grc_DSCB.DataSource = listCB;
            }
            else
            {
                grc_DSCB.DataSource = null;
            }
           
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            //if (txtTimkiem.Text == "")
            //    txtTimkiem.Text = "Mã/Tên CB";
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            GetDSCB();
        }

        private void txtTimkiem_Click(object sender, EventArgs e)
        {
            //if (txtTimkiem.Text == "Mã/Tên CB")
            //    txtTimkiem.Text = "";
        }

        private void grv_DSCB_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
                int row = grv_DSCB.FocusedRowHandle;
                if (grv_DSCB.GetRowCellValue(row, "MaBNhan") != null)
                {
                    maBN = grv_DSCB.GetRowCellValue(row, "MaBNhan").ToString();
                }
                if (maBN != "")
                    showCBInfo_XML_linq(maBN);
                if (row >= 0)
                    trangthai = 2;
                setReadOnly();           
        }
        List<LichSuBenh> listBCN; 
        List<LichSuBenh> listBNN;
        #region load thông tin cán bộ lên form nhập (load từ file XML)
        private void showCBInfo_XML_linq(string maBnhan)
        {
            path = txtDuongDan.Text;
            XDocument doc = new XDocument();
            listBCN = new List<LichSuBenh>();
            listBNN = new List<LichSuBenh>();
            if (System.IO.File.Exists(path))
            {
                doc = XDocument.Load(path);               
                string nam = cboNam.SelectedIndex > 0 ? cboNam.SelectedItem.ToString() : DateTime.Now.Year.ToString();
                var CB = (from bn in
                                  (from n in doc.Descendants("str" + nam) select n).Descendants("Benhnhan")
                          where (bn.Element("MaBNhan") != null && bn.Element("MaBNhan").Value == maBN)
                              select new
                              {
                                  goitinh = bn.Element("GTinh") == null ? "" : bn.Element("GTinh").Value,
                                  mabenhnhan = bn.Element("MaBNhan") == null ? "" : bn.Element("MaBNhan").Value,
                                  tuoi = bn.Element("Tuoi") == null ? "" : bn.Element("Tuoi").Value,
                                  TenBNhan = bn.Element("TenBNhan") == null ? "" : bn.Element("TenBNhan").Value,
                                  socmnn = bn.Element("CMND") == null ? "" : bn.Element("CMND").Value,
                                  ngaycap = bn.Element("NgayCap") == null ? "" : bn.Element("NgayCap").Value,
                                  hokhau = bn.Element("ThuongTru") == null ? "" : bn.Element("ThuongTru").Value,
                                  diachi = bn.Element("DiaChi") == null ? "" : bn.Element("DiaChi").Value,
                                  nhenghiep = bn.Element("ChucVu") == null ? "" : bn.Element("ChucVu").Value,
                                  noicongtac = bn.Element("NoiCongTacHT") == null ? "" : bn.Element("NoiCongTacHT").Value,
                                  ngaybatdau = bn.Element("ngaybatdau") == null ? "" : bn.Element("ngaybatdau").Value,
                                  tiensubenhgiadinh = bn.Element("TienSuBenhGD") == null ? "" : bn.Element("TienSuBenhGD").Value,
                                  TienSuBenh = bn.Descendants("TienSuBenh"),//from tsb in bn.Descendants("TienSuBenh") select tsb,
                                  phanloaisukhoe = bn.Element("KetLuan").Element("PloaiSK") == null ? "" : bn.Element("KetLuan").Element("PloaiSK").Value,
                                  cacbenhtat = bn.Element("KetLuan").Element("CacBenhTat") == null ? "" : bn.Element("KetLuan").Element("CacBenhTat").Value,
                                  anh = bn.Element("Anh") == null ? "" : bn.Element("Anh").Value,
                                  cls = bn.Element("KhamCLS") == null ? "" : bn.Element("KhamCLS").Value,
                                  ls = bn.Element("KhamLS") == null ? "" : bn.Element("KhamLS").Value,
                              }).ToList();
                if (CB.Count > 0)
                {
                    lupCB.EditValue = CB.First().mabenhnhan;
                    txtTuoi.Text = CB.First().tuoi;
                    txtNSinh.Text = CB.First().tuoi == "" ? "" :(DateTime.Now.Year - Convert.ToInt16(CB.First().tuoi)).ToString();
                    txtCMND.Text = CB.First().socmnn;
                    dtNgayCap.EditValue = Convert.ToDateTime(CB.First().ngaycap);
                    txtHoKhau.Text = CB.First().hokhau;
                    txtTuoi.Text = CB.First().tuoi;
                    txtDiaChi.Text = CB.First().diachi;
                    txtNgheNghiep.Text = CB.First().nhenghiep;
                    txtNoiCongTac.Text = CB.First().noicongtac;
                    dtNgayBDLamViec.EditValue = Convert.ToDateTime(CB.First().ngaybatdau);
                    txtTienSuGD.Text = CB.First().tiensubenhgiadinh;
                    txtPhanLoaiSK.Text = CB.First().phanloaisukhoe;
                    txtCacbenhtat.Text = CB.First().cacbenhtat;
                    txtKhamLamSang.Text = CB.First().ls;
                    txtKhamCLS.Text = CB.First().cls;
                    if (CB.First().anh != "" && File.Exists(CB.First().anh))
                    {
                        pictureEdit1.Image = Image.FromFile(CB.First().anh);
                    }
                    else
                    {
                        pictureEdit1.Image = null;
                    }
                    #region c1 Thiếu phần bệnh nghề nghiệp
                    //var benhCN = (from tscn in
                    //             (from ts in 
                    //             (from bn in
                    //             (from n in doc.Descendants("str" + nam) select n).Descendants("Benhnhan")
                    //              where (bn.Element("MaBNhan") != null && bn.Element("MaBNhan").Value == maBN) select bn).Descendants("TienSuBenh") select ts).Descendants("BenhCNhan") select 
                    //           new
                    //          {
                    //              TenBenh = tscn.Element("TenBenh") == null ? "" : tscn.Element("TenBenh").Value,
                    //              NamPH = tscn.Element("NamPH") == null ? "" : tscn.Element("NamPH").Value,                                  
                    //          }).ToList();

                    // if (benhCN.Count > 0)
                    // {
                    //     foreach (var b in benhCN)
                    //     {
                    //         LichSuBenh ls = new LichSuBenh();
                    //         if (b.TenBenh != "")
                    //         {
                    //             ls.Tenbenh = b.TenBenh.ToString();
                    //             if (b.NamPH != null)
                    //                 ls.Namph = b.NamPH.ToString();
                    //             listBCN.Add(ls);
                    //         }
                    //     }
                    // }
                    // bintienSuBenh.DataSource = listBCN;
                    // grc_TienSuBenh.DataSource = listBCN;
                    #endregion
                    if (CB.First().TienSuBenh != null)
                    {
                        var benhCN = (from bcn in CB.First().TienSuBenh.Descendants("BenhCNhan")
                                      select new
                                          {
                                              TenBenh = bcn.Element("TenBenh") == null ? "" : bcn.Element("TenBenh").Value,
                                              NamPH = bcn.Element("NamPH") == null ? "" : bcn.Element("NamPH").Value
                                          }).ToList();
                        if (benhCN.Count > 0)
                        {
                            foreach (var b in benhCN)
                            {
                                LichSuBenh ls = new LichSuBenh();
                                if (b.TenBenh != "")
                                {
                                    ls.Tenbenh = b.TenBenh.ToString();
                                    if (b.NamPH != null)
                                        ls.Namph = b.NamPH.ToString();
                                    listBCN.Add(ls);
                                }
                            }
                        }

                        var benhNN = (from bnn in CB.First().TienSuBenh.Descendants("BenhNN")
                                      select new
                                      {
                                          TenBenh = bnn.Element("TenBenhNN") == null ? "" : bnn.Element("TenBenhNN").Value,
                                          NamPH = bnn.Element("NamPHNN") == null ? "" : bnn.Element("NamPHNN").Value
                                      }).ToList();
                        if (benhNN.Count > 0)
                        {
                            foreach (var b in benhNN)
                            {
                                LichSuBenh ls = new LichSuBenh();
                                if (b.TenBenh != "")
                                {
                                    ls.Tenbenh = b.TenBenh.ToString();
                                    if (b.NamPH != null)
                                        ls.Namph = b.NamPH.ToString();
                                    listBNN.Add(ls);
                                }
                            }
                        }
                        bintienSuBenh.DataSource = listBCN;
                        bintiensuNN.DataSource = listBNN;
                        grc_TienSuBenh.DataSource = bintienSuBenh;
                        grc_TiensuBNN.DataSource = bintiensuNN;
                    }
                   
                }
            }
        }
        private void showCBInfo_XML(string maBnhan)//ok
        {           
            listBCN = new List<LichSuBenh>();
            listBNN = new List<LichSuBenh>();
            string _maBN = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList ndlist1 = doc.SelectNodes("/xml/str" + cboNam.SelectedItem.ToString()); // các năm
            foreach (XmlNode ndnam in ndlist1)
            {
                if (ndnam.Name == "str" + cboNam.SelectedItem.ToString())
                {
                    XmlNodeList ndlist2 = ndnam.SelectNodes("//Benhnhan");// ndlist2 = list các BenhNhan
                    foreach (XmlNode m in ndlist2)// m = thông tin đầy đủ 1 bệnh nhân
                    {
                        bool rs = false;
                        foreach (XmlNode n in m.ChildNodes)// n là thông tin chi tiết i node chi tiết cấp 1 cho 1 bệnh nhân
                        {
                            if (n.Name == "MaBNhan")
                                _maBN = n.InnerText;

                            if (_maBN == maBnhan)
                            {
                                rs = true;
                                break;
                            }
                        }
                        if (rs == true)
                        {
                            lupCB.EditValue = maBnhan;
                            if (m["GTinh"] != null && m["GTinh"].InnerText == "Nam")
                                rdGTinh.SelectedIndex = 0;
                            else
                                rdGTinh.SelectedIndex = 1;
                            string imgPath =  m["Anh"] == null ? "" :  m["Anh"].InnerText;
                            if (File.Exists(imgPath))
                                pictureEdit1.Image = Image.FromFile(imgPath);
                            else
                                pictureEdit1.Image = null;
                            txtTuoi.Text =  m["Tuoi"] == null? "" : m["Tuoi"].InnerText;
                            txtCMND.Text = m["CMND"] == null ? "" : m["CMND"].InnerText;
                            dtNgayCap.EditValue = m["NgayCap"] == null ? DateTime.Now : Convert.ToDateTime(m["NgayCap"].InnerText);
                            txtHoKhau.Text = m["ThuongTru"] == null ? "" : m["ThuongTru"].InnerText;
                            txtDiaChi.Text = m["DiaChi"] == null ? "" : m["DiaChi"].InnerText;
                            txtNgheNghiep.Text = m["ChucVu"] == null ? "" : m["ChucVu"].InnerText;
                            txtNoiCongTac.Text = m["NoiCongTacHT"] == null ? "" : m["NoiCongTacHT"].InnerText;
                            dtNgayBDLamViec.EditValue = String.IsNullOrEmpty(m["ngaybatdau"].InnerText) ? DateTime.Now : Convert.ToDateTime(m["ngaybatdau"].InnerText);
                            txtTienSuGD.Text =  m["TienSuBenhGD"] == null ? "" : m["TienSuBenhGD"].InnerText;
                            XmlNode ndTiensucanhan = doc.SelectSingleNode("/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']/TienSuBenh");
                            if (ndTiensucanhan != null)
                            {
                                XmlNodeList ndlistBCN = ndTiensucanhan.ChildNodes;
                                foreach (XmlNode x in ndlistBCN)
                                {
                                    if (x.Name == "BenhCNhan")
                                    {
                                        if (x["TenBenh"] != null)
                                        {
                                            LichSuBenh ls = new LichSuBenh();
                                            ls.Tenbenh = x["TenBenh"].InnerText;
                                            if (x["NamPH"] != null)
                                                ls.Namph = x["NamPH"].InnerText;
                                            listBCN.Add(ls);
                                        }
                                    }
                                    else if (x.Name == "BenhNN")
                                    {
                                        if (x["TenBenhNN"] != null)
                                        {
                                            LichSuBenh ls = new LichSuBenh();
                                            ls.Tenbenh = x["TenBenhNN"].InnerText;
                                            if (x["NamPHNN"] != null)
                                                ls.Namph = x["NamPHNN"].InnerText;
                                            listBNN.Add(ls);
                                        }
                                    }
                                }
                            }
                            bintienSuBenh.DataSource = listBCN;
                            bintiensuNN.DataSource = listBNN;
                            grc_TienSuBenh.DataSource = bintienSuBenh;
                            grc_TiensuBNN.DataSource = bintiensuNN;
                            txtKhamLamSang.Text = m["KhamLS"].InnerText;
                            txtKhamCLS.Text = m["KhamCLS"].InnerText;
                            string r = "/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']/KetLuan";
                            XmlNode ndKetLuan = doc.SelectSingleNode(r);
                            txtPhanLoaiSK.Text = ndKetLuan["PloaiSK"].InnerText;
                            txtCacbenhtat.Text = ndKetLuan["CacBenhTat"].InnerText;
                            break;
                        }
                    }
                }
            }


            doc.Save(path);


        }
        #endregion
        string maBN = "";
        string _fileanh = "";
        string _tenfileanh = "";
        private void btnLuu_Click(object sender, EventArgs e)
        {
            path = txtDuongDan.Text;
            string tenBN = "";

            if (lupCB.EditValue != null)
            {
                maBN = lupCB.EditValue.ToString();
                if (lupCB.GetColumnValue("TenCB") != null)
                    tenBN = lupCB.GetColumnValue("TenCB").ToString();              
            }
            if (maBN == "")
            {
                MessageBox.Show("Bạn chưa chọn cán bộ để cập nhật thông tin");
            }
            {
                string year = DateTime.Now.Year.ToString();
                if (cboNam.SelectedIndex > 0)
                    year = cboNam.SelectedItem.ToString();

                #region insert anh               
                try
                {
                    if (!string.IsNullOrEmpty(_fileanh) && !string.IsNullOrEmpty(_tenfileanh))
                    {
                        if (!File.Exists(_tenfileanh))
                        {
                            File.Copy(_fileanh, _tenfileanh);
                        }
                        else
                        {
                            if (chonAnh == true)
                            {
                                DialogResult _dresult1 = MessageBox.Show("Tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult1 == DialogResult.Yes)
                                {
                                    File.Delete(_tenfileanh);
                                    File.Copy(_fileanh, _tenfileanh);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    _tenfileanh = "";
                }
                #endregion insert anh
                #region insert vào file xml
                if (trangthai == 0)
                {
                    try
                    {

                        XmlDocument xmldoc = new XmlDocument();
                        XmlNode Root;
                        if (System.IO.File.Exists(path))
                        {
                            xmldoc.Load(path);
                            Root = xmldoc.SelectSingleNode("//xml");
                        }
                        else
                        {
                            XmlDeclaration xmlDeclaration = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                            Root = xmldoc.CreateElement("xml");
                            xmldoc.InsertBefore(xmlDeclaration, xmldoc.DocumentElement);
                            xmldoc.AppendChild(Root);
                        }
                        #region checkExist
                        string ck = "/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']";
                        XmlNode ndCheck = xmldoc.SelectSingleNode(ck);
                        if (ndCheck == null)
                        {
                            XmlNodeList listNodeNam = Root.ChildNodes;
                            XmlNode nodeNam;
                            string strnam = "str" + year;
                            // đoạn dưới có thể tìm hiểu the attribute cho thẻ Năm
                            if (Root.SelectSingleNode("//" + strnam) != null)
                            {
                                nodeNam = Root.SelectSingleNode("//" + strnam);
                            }
                            else
                            {
                                nodeNam = xmldoc.CreateElement(strnam);
                                Root.AppendChild(nodeNam);
                            }
                            XmlNode nodeRoot = xmldoc.CreateElement("Benhnhan");
                            nodeNam.AppendChild(nodeRoot);
                            XmlNode ndMaCB = xmldoc.CreateElement("MaBNhan");
                            nodeRoot.AppendChild(ndMaCB);
                            ndMaCB.InnerText = maBN;
                            XmlNode ndNgaykham = xmldoc.CreateElement("Ngaykham");
                            nodeRoot.AppendChild(ndNgaykham);
                            ndNgaykham.InnerText = DateTime.Now.ToShortDateString();
                            XmlNode ndTenBNhan = xmldoc.CreateElement("TenBNhan");
                            nodeRoot.AppendChild(ndTenBNhan);
                            ndTenBNhan.InnerText = tenBN;
                            XmlNode ndAnh = xmldoc.CreateElement("Anh");
                            nodeRoot.AppendChild(ndAnh);
                            ndAnh.InnerText = _tenfileanh; // đường dẫn ảnh
                            XmlNode ndGTinh = xmldoc.CreateElement("GTinh");
                            nodeRoot.AppendChild(ndGTinh);
                            ndGTinh.InnerText = rdGTinh.SelectedIndex == 0 ? "Nam" : "Nữ";
                            XmlNode ndTuoi = xmldoc.CreateElement("Tuoi");
                            nodeRoot.AppendChild(ndTuoi);
                            ndTuoi.InnerText = txtTuoi.Text;
                            XmlNode ndCMND = xmldoc.CreateElement("CMND");
                            nodeRoot.AppendChild(ndCMND);
                            ndCMND.InnerText = txtCMND.Text;
                            XmlNode ndNgayCap = xmldoc.CreateElement("NgayCap");
                            nodeRoot.AppendChild(ndNgayCap);
                            ndNgayCap.InnerText = dtNgayCap.EditValue.ToString();
                            XmlNode ndThuongTru = xmldoc.CreateElement("ThuongTru");
                            nodeRoot.AppendChild(ndThuongTru);
                            ndThuongTru.InnerText = txtHoKhau.Text;
                            XmlNode ndDiaChi = xmldoc.CreateElement("DiaChi");
                            nodeRoot.AppendChild(ndDiaChi);
                            ndDiaChi.InnerText = txtDiaChi.Text;
                            XmlNode ndChucVu = xmldoc.CreateElement("ChucVu");
                            nodeRoot.AppendChild(ndChucVu);
                            ndChucVu.InnerText = txtNgheNghiep.Text;
                            XmlNode ndNoiCongTacHT = xmldoc.CreateElement("NoiCongTacHT");
                            nodeRoot.AppendChild(ndNoiCongTacHT);
                            ndNoiCongTacHT.InnerText = txtNoiCongTac.Text;
                            XmlNode ndngaybatdau = xmldoc.CreateElement("ngaybatdau");
                            nodeRoot.AppendChild(ndngaybatdau);
                            ndngaybatdau.InnerText = dtNgayBDLamViec.EditValue.ToString();
                            XmlNode ndTienSuBenhGD = xmldoc.CreateElement("TienSuBenhGD");
                            nodeRoot.AppendChild(ndTienSuBenhGD);
                            ndTienSuBenhGD.InnerText = txtTienSuGD.Text;
                            XmlNode ndTienSuBenh = xmldoc.CreateElement("TienSuBenh");
                            nodeRoot.AppendChild(ndTienSuBenh);

                            // node child của ndBenhCNhan
                            for (int i = 0; i < grv_TienSuBenh.RowCount; i++)
                            {
                                // node child của tien su benh                   
                                XmlNode ndBenhCNhan = xmldoc.CreateElement("BenhCNhan");
                                ndTienSuBenh.AppendChild(ndBenhCNhan);
                                if (grv_TienSuBenh.GetRowCellValue(i, colTenBenh) != null)
                                {
                                    string tenbenh = grv_TienSuBenh.GetRowCellValue(i, colTenBenh).ToString();
                                    XmlNode ndTenBenh = xmldoc.CreateElement("TenBenh");
                                    ndBenhCNhan.AppendChild(ndTenBenh);
                                    ndTenBenh.InnerText = tenbenh;
                                    if (grv_TienSuBenh.GetRowCellValue(i, colNamPH) != null)
                                    {
                                        XmlNode ndNamPH = xmldoc.CreateElement("NamPH");
                                        ndBenhCNhan.AppendChild(ndNamPH);
                                        ndNamPH.InnerText = grv_TienSuBenh.GetRowCellValue(i, colNamPH).ToString();
                                    }
                                }
                            }
                            for (int i = 0; i < grv_TiensuBNN.RowCount; i++)
                            {
                                XmlNode ndBenhNN = xmldoc.CreateElement("BenhNN");
                                ndTienSuBenh.AppendChild(ndBenhNN);
                                if (grv_TiensuBNN.GetRowCellValue(i, colTenBenhNN) != null)
                                {
                                    string tenbenh = grv_TiensuBNN.GetRowCellValue(i, colTenBenhNN).ToString();
                                    XmlNode ndTenBenh = xmldoc.CreateElement("TenBenhNN");
                                    ndBenhNN.AppendChild(ndTenBenh);
                                    ndTenBenh.InnerText = tenbenh;
                                    if (grv_TiensuBNN.GetRowCellValue(i, colNamPHNN) != null)
                                    {
                                        XmlNode ndNamPH = xmldoc.CreateElement("NamPHNN");
                                        ndBenhNN.AppendChild(ndNamPH);
                                        ndNamPH.InnerText = grv_TiensuBNN.GetRowCellValue(i, colNamPHNN).ToString();
                                    }
                                }
                            }

                            XmlNode ndKhamLS = xmldoc.CreateElement("KhamLS");
                            nodeRoot.AppendChild(ndKhamLS);
                            ndKhamLS.InnerText = txtKhamLamSang.Text;
                            XmlNode ndKhamCLS = xmldoc.CreateElement("KhamCLS");
                            nodeRoot.AppendChild(ndKhamCLS);
                            ndKhamCLS.InnerText = txtKhamCLS.Text;
                            XmlNode ndKetLuan = xmldoc.CreateElement("KetLuan");
                            nodeRoot.AppendChild(ndKetLuan);
                            XmlNode ndPloaiSK = xmldoc.CreateElement("PloaiSK");
                            ndKetLuan.AppendChild(ndPloaiSK);
                            ndPloaiSK.InnerText = txtPhanLoaiSK.Text;
                            XmlNode ndCacBenhTat = xmldoc.CreateElement("CacBenhTat");
                            ndKetLuan.AppendChild(ndCacBenhTat);
                            ndCacBenhTat.InnerText = txtCacbenhtat.Text;
                            xmldoc.Save(path);
                            MessageBox.Show("Thêm mới thành công");
                        }
                        else
                        {
                            MessageBox.Show("Đã tồn tại cán bộ trong danh sách");
                        }
                        #endregion checkExist
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                    GetDSCB();
                }
                #endregion insert vào file xml
                #region update file xml
                else
                {
                    try
                    {
                        int  _mabn = 0;
                        string _Nam = "";
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        XmlNode ndBN = doc.SelectSingleNode("/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']");
                        if (ndBN != null)
                        {
                            if (ndBN["TenBNhan"] != null)
                                ndBN["TenBNhan"].InnerText = tenBN;
                            if (ndBN["Ngaykham"] != null)
                                ndBN["Ngaykham"].InnerText = DateTime.Now.ToShortDateString();
                            if (ndBN["Anh"] != null)
                                ndBN["Anh"].InnerText = _tenfileanh;
                            if (ndBN["GTinh"] != null)
                                ndBN["GTinh"].InnerText = rdGTinh.SelectedIndex == 0 ? "Nam" : "Nữ";
                            if (ndBN["Tuoi"] != null)
                                ndBN["Tuoi"].InnerText = txtTuoi.Text;
                            if (ndBN["CMND"] != null)
                                ndBN["CMND"].InnerText = txtCMND.Text;
                            if (ndBN["NgayCap"] != null)
                                ndBN["NgayCap"].InnerText = dtNgayCap.EditValue.ToString();
                            if (ndBN["ThuongTru"] != null)
                                ndBN["ThuongTru"].InnerText = txtHoKhau.Text;
                            if (ndBN["DiaChi"] != null)
                                ndBN["DiaChi"].InnerText = txtDiaChi.Text;
                            if (ndBN["ChucVu"] != null)
                                ndBN["ChucVu"].InnerText = txtNgheNghiep.Text;
                            if (ndBN["NoiCongTacHT"] != null)
                                ndBN["NoiCongTacHT"].InnerText = txtNoiCongTac.Text;
                            if (ndBN["ngaybatdau"] != null)
                                ndBN["ngaybatdau"].InnerText = dtNgayBDLamViec.EditValue.ToString();
                            if (ndBN["TienSuBenhGD"] != null)
                                ndBN["TienSuBenhGD"].InnerText = txtTienSuGD.Text;
                        string d = "/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']/TienSuBenh";
                        XmlNode ndDelTienSuBenh = doc.SelectSingleNode(d);
                        if (ndDelTienSuBenh != null)
                            (ndDelTienSuBenh.ParentNode).RemoveChild(ndDelTienSuBenh);
                        XmlNode ndTienSuBenh = doc.CreateElement("TienSuBenh");
                        ndBN.AppendChild(ndTienSuBenh);

                        // node child của ndBenhCNhan
                        for (int i = 0; i < grv_TienSuBenh.RowCount; i++)
                        {
                            // node child của tien su benh                   
                            XmlNode ndBenhCNhan = doc.CreateElement("BenhCNhan");
                            ndTienSuBenh.AppendChild(ndBenhCNhan);
                            if (grv_TienSuBenh.GetRowCellValue(i, colTenBenh) != null)
                            {
                                string tenbenh = grv_TienSuBenh.GetRowCellValue(i, colTenBenh).ToString();
                                XmlNode ndTenBenh = doc.CreateElement("TenBenh");
                                ndBenhCNhan.AppendChild(ndTenBenh);
                                ndTenBenh.InnerText = tenbenh;
                                if (grv_TienSuBenh.GetRowCellValue(i, colNamPH) != null)
                                {
                                    XmlNode ndNamPH = doc.CreateElement("NamPH");
                                    ndBenhCNhan.AppendChild(ndNamPH);
                                    ndNamPH.InnerText = grv_TienSuBenh.GetRowCellValue(i, colNamPH).ToString();
                                }
                            }
                        }
                        for (int i = 0; i < grv_TiensuBNN.RowCount; i++)
                        {
                            XmlNode ndBenhNN = doc.CreateElement("BenhNN");
                            ndTienSuBenh.AppendChild(ndBenhNN);
                            if (grv_TiensuBNN.GetRowCellValue(i, colTenBenhNN) != null)
                            {
                                string tenbenh = grv_TiensuBNN.GetRowCellValue(i, colTenBenhNN).ToString();
                                XmlNode ndTenBenh = doc.CreateElement("TenBenhNN");
                                ndBenhNN.AppendChild(ndTenBenh);
                                ndTenBenh.InnerText = tenbenh;
                                if (grv_TiensuBNN.GetRowCellValue(i, colNamPHNN) != null)
                                {
                                    XmlNode ndNamPH = doc.CreateElement("NamPHNN");
                                    ndBenhNN.AppendChild(ndNamPH);
                                    ndNamPH.InnerText = grv_TiensuBNN.GetRowCellValue(i, colNamPHNN).ToString();
                                }
                            }
                        }
                        if (ndBN["KhamLS"] != null)
                            ndBN["KhamLS"].InnerText = txtKhamLamSang.Text;
                        if (ndBN["KhamCLS"] != null)
                            ndBN["KhamCLS"].InnerText = txtKhamCLS.Text;
                        string r = "/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']/KetLuan";
                        XmlNode ndKetLuan = doc.SelectSingleNode(r);
                        if (ndKetLuan["PloaiSK"] != null)
                            ndKetLuan["PloaiSK"].InnerText = txtPhanLoaiSK.Text;
                        if (ndKetLuan["CacBenhTat"] != null)
                            ndKetLuan["CacBenhTat"].InnerText = txtCacbenhtat.Text;
                        }
                       
                        doc.Save(path);
                        MessageBox.Show("Sửa thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
                #endregion update file xml
                trangthai = -1;
                chonAnh = false;
                GetDSCB();                
            }
            resetForm();
            setReadOnly();
        }

        private bool checkValidate()
        {
            bool rs = true;
            return rs;
        }

        private void txtNSinh_Leave(object sender, EventArgs e)
        {
            if (txtNSinh.Text != "")
            {
                int NSinh = Convert.ToInt32(txtNSinh.Text);
                if (NSinh < 0)
                {
                    txtNSinh.Focus();
                    MessageBox.Show("Năm sinh phải lớn hơn 0");
                }
                else
                {
                    txtTuoi.Text = (DateTime.Now.Year - NSinh).ToString();
                }
            }
        }

        private void txtTuoi_Leave(object sender, EventArgs e)
        {
            if (txtTuoi.Text != "")
            {
                int tuoi = Convert.ToInt32(txtTuoi.Text);
                if (tuoi < 0)
                {
                    txtTuoi.Focus();
                    MessageBox.Show("Tuổi phải lớn hơn 0");
                }
                else
                {
                    if (txtNSinh.Text == "")
                    {
                        txtNSinh.Text = (DateTime.Now.Year - Convert.ToInt16(txtTuoi.Text)).ToString();
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetForm();
            trangthai = 0;
            groupControl2.Text = ("Thông tin cán bộ - Thêm mới").ToUpper();
            setReadOnly();

        }
        private void resetForm()
        {
            lupCB.EditValue = null;
            txtNSinh.Text = "";
            txtTuoi.Text = "";
            txtCMND.Text = "";
            dtNgayCap.EditValue = DateTime.Now;
            txtHoKhau.Text = "";
            txtDiaChi.Text = "";
            txtNgheNghiep.Text = "";
            txtNoiCongTac.Text = "";
            dtNgayBDLamViec.EditValue = DateTime.Now;
            txtTienSuGD.Text = "";
            txtKhamLamSang.Text = "";
            txtKhamCLS.Text = "";
            txtPhanLoaiSK.Text = "";
            txtCacbenhtat.Text = "";
            pictureEdit1.Image = null;           
            listBCN = new List<LichSuBenh>();
            listBNN = new List<LichSuBenh>();
            bintienSuBenh.DataSource = listBCN;
            bintiensuNN.DataSource = listBNN;           
            groupControl2.Text = ("Thông tin cán bộ").ToUpper();
            setReadOnly();            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {           
            trangthai = 1;
            groupControl2.Text = ("Thông tin cán bộ - Sửa").ToUpper();
            setReadOnly();
        }
        private void setReadOnly()
        {
            if (trangthai == -1)// trạng thái chờ
            {
                lupCB.Properties.ReadOnly = true;
                btnChonAnh.Enabled = false;

                txtNSinh.Properties.ReadOnly = true;
                txtTuoi.Properties.ReadOnly = true;
                txtCMND.Properties.ReadOnly = true;
                dtNgayCap.Properties.ReadOnly = true;
                txtHoKhau.Properties.ReadOnly = true;
                txtDiaChi.Properties.ReadOnly = true;
                txtNgheNghiep.Properties.ReadOnly = true;
                txtNoiCongTac.Properties.ReadOnly = true;
                dtNgayBDLamViec.Properties.ReadOnly = true;
                txtTienSuGD.Properties.ReadOnly = true;
                txtKhamLamSang.Properties.ReadOnly = true;
                txtKhamCLS.Properties.ReadOnly = true;
                txtPhanLoaiSK.Properties.ReadOnly = true;
                txtCacbenhtat.Properties.ReadOnly = true;
                btnNew.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnIn.Enabled = false ;
                btnLuu.Enabled = false;
                grv_TienSuBenh.OptionsBehavior.ReadOnly = true;
                grv_TiensuBNN.OptionsBehavior.ReadOnly = true;
            }
            if (trangthai == 0)// trạng thái thêm mới
            {
                lupCB.Properties.ReadOnly = false;
                btnChonAnh.Enabled = true;
                txtNSinh.Properties.ReadOnly = false;
                txtTuoi.Properties.ReadOnly = false;
                txtCMND.Properties.ReadOnly = false;
                dtNgayCap.Properties.ReadOnly = false;
                txtHoKhau.Properties.ReadOnly = false;
                txtDiaChi.Properties.ReadOnly = false;
                txtNgheNghiep.Properties.ReadOnly = false;
                txtNoiCongTac.Properties.ReadOnly = false;
                dtNgayBDLamViec.Properties.ReadOnly = false;
                txtTienSuGD.Properties.ReadOnly = false;
                txtKhamLamSang.Properties.ReadOnly = false;
                txtKhamCLS.Properties.ReadOnly = false;
                txtPhanLoaiSK.Properties.ReadOnly = false;
                txtCacbenhtat.Properties.ReadOnly = false;
                btnNew.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnIn.Enabled = false;
                btnLuu.Enabled = true;
                grv_TienSuBenh.OptionsBehavior.ReadOnly = false;
                grv_TiensuBNN.OptionsBehavior.ReadOnly = false ;
            }
            if (trangthai == 1)// trạng thái sửa
            {
                lupCB.Properties.ReadOnly = false;
                btnChonAnh.Enabled = true;               
                txtNSinh.Properties.ReadOnly = false;
                txtTuoi.Properties.ReadOnly = false;
                txtCMND.Properties.ReadOnly = false;
                dtNgayCap.Properties.ReadOnly = false;
                txtHoKhau.Properties.ReadOnly = false;
                txtDiaChi.Properties.ReadOnly = false;
                txtNgheNghiep.Properties.ReadOnly = false;
                txtNoiCongTac.Properties.ReadOnly = false;
                dtNgayBDLamViec.Properties.ReadOnly = false;
                txtTienSuGD.Properties.ReadOnly = false;
                txtKhamLamSang.Properties.ReadOnly = false;
                txtKhamCLS.Properties.ReadOnly = false;
                txtPhanLoaiSK.Properties.ReadOnly = false;
                txtCacbenhtat.Properties.ReadOnly = false;
                btnNew.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
                btnLuu.Enabled = true;
                grv_TienSuBenh.OptionsBehavior.ReadOnly = false;
                grv_TiensuBNN.OptionsBehavior.ReadOnly = false;
            }
            if (trangthai == 2)// trạng thái sửa
            {               
                lupCB.Properties.ReadOnly = true;
                btnChonAnh.Enabled = false;
                txtNSinh.Properties.ReadOnly = true;
                txtTuoi.Properties.ReadOnly = true;
                txtCMND.Properties.ReadOnly = true;
                dtNgayCap.Properties.ReadOnly = true;
                txtHoKhau.Properties.ReadOnly = true;
                txtDiaChi.Properties.ReadOnly = true;
                txtNgheNghiep.Properties.ReadOnly = true;
                txtNoiCongTac.Properties.ReadOnly = true;
                dtNgayBDLamViec.Properties.ReadOnly = true;
                txtTienSuGD.Properties.ReadOnly = true;
                txtKhamLamSang.Properties.ReadOnly = true;
                txtKhamCLS.Properties.ReadOnly = true;
                txtPhanLoaiSK.Properties.ReadOnly = true;
                txtCacbenhtat.Properties.ReadOnly = true;
                btnNew.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
                btnLuu.Enabled = false;
                grv_TienSuBenh.OptionsBehavior.ReadOnly = true;
                grv_TiensuBNN.OptionsBehavior.ReadOnly = true;
            }
        }

        private void lupCB_EditValueChanged(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (lupCB.EditValue != null && trangthai == 0)
            {
                maBN = lupCB.EditValue.ToString();
                var CanBoInfo = data.CanBoes.Where(p => p.MaCB == maBN).ToList();
                if (CanBoInfo.Count > 0)
                {
                    rdGTinh.SelectedIndex = CanBoInfo.First().GioiTinh == 1 ? 0 : 1;
                    txtDiaChi.Text = CanBoInfo.First().DiaChi == null ? "" : CanBoInfo.First().DiaChi;
                    txtNgheNghiep.Text = CanBoInfo.First().ChucVu == null ? "" : CanBoInfo.First().ChucVu;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maBN != "" && trangthai == 2) //điều kiện cán bộ có trong file xml
            {
                if (MessageBox.Show("Bạn muốn xóa thông tin cho cán bộ này ?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    path = txtDuongDan.Text;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode nodedel = doc.SelectSingleNode("/xml/str" + cboNam.SelectedItem.ToString() + "/Benhnhan[MaBNhan ='" + maBN + "']");
                    XmlNode prNode = nodedel.ParentNode;
                    prNode.RemoveChild(nodedel);
                    doc.Save(path);
                    MessageBox.Show("Xóa thành công");                  
                    GetDSCB();
                   
                    trangthai = -1;
                    resetForm();
                    setReadOnly();
                }

            }
            else
            {
                MessageBox.Show("Lỗi");
            }
            
        }
        public class LichSuBenh
        {            
            private string tenbenh;
            public string Tenbenh
            {
              get { return tenbenh; }
              set { tenbenh = value; }
            }
             private string namph;

            public string Namph
            {
              get { return namph; }
              set { namph = value; }
            }
        }
        private bool chonAnh = false;
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG|*.JPG|PNG|*.PNG|GIF|*.GIF|BMP|*.BMP|All files (*.*)|*.*";//"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";          
            dialog.Title = "Chọn file Ảnh";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                 _fileanh = dialog.FileName;
                 Image img = pictureEdit1.Image;
                 if(img != null)
                 img.Dispose();
                 pictureEdit1.Image = Image.FromFile(_fileanh);
                 chonAnh = true;
                 _tenfileanh = DungChung.Bien.ImageFilePath_CBInfo;
                 _tenfileanh += maBN + "_" + cboNam.SelectedItem.ToString() + ".jpg";

            }    
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (maBN != "" && trangthai == 2) // đang update hoặc đang chọn cán bộ từ danh sách file xML
                print();
            else
                MessageBox.Show("Bạn chưa chọn cán bộ để in");
        }       
        public void print()
        {
            XDocument doc = XDocument.Load(path);
            listBCN = new List<LichSuBenh>();
            listBNN = new List<LichSuBenh>();
            List<tiensubenhAll> listTS = new List<tiensubenhAll>();
            string nam = cboNam.SelectedIndex >= 0 ? cboNam.SelectedItem.ToString() : DateTime.Now.Year.ToString();
            string a = "str" + nam;          
            var thongtincanbo = (from bn in //doc.Elements(a).Elements("Benhnhan")
                                 ( from n in doc.Descendants(a) select n).Descendants("Benhnhan")
                                 where ( bn.Element("MaBNhan") != null && bn.Element("MaBNhan").Value == maBN)
                                 select new
                                 {
                                     goitinh = bn.Element("GTinh") == null? "" : bn.Element("GTinh").Value,
                                     mabenhnhan =bn.Element("MaBNhan")== null ? "" : bn.Element("MaBNhan").Value,
                                     tuoi = bn.Element("Tuoi") == null ? "" :bn.Element("Tuoi").Value,
                                     TenBNhan = bn.Element("TenBNhan") == null ? "" : bn.Element("TenBNhan").Value,
                                     socmnn = bn.Element("CMND") == null ? "" : bn.Element("CMND").Value,
                                     ngaycap = bn.Element("NgayCap") == null ? "" : bn.Element("NgayCap").Value,
                                     hokhau = bn.Element("ThuongTru") == null ? "" : bn.Element("ThuongTru").Value,
                                     choo =bn.Element("DiaChi") == null ? "" : bn.Element("DiaChi").Value,
                                     nhenghiep = bn.Element("ChucVu") == null ? "" : bn.Element("ChucVu").Value,
                                     locongtac = bn.Element("NoiCongTacHT") == null ? "" : bn.Element("NoiCongTacHT").Value,
                                     ngaybatdau = bn.Element("ngaybatdau") == null ? "" : bn.Element("ngaybatdau").Value,
                                     tiensubenhgiadinh = bn.Element("TienSuBenhGD") == null ? "": bn.Element("TienSuBenhGD").Value,
                                     tenbenh = bn.Element("TienSuBenh").Element("BenhCNhan").Element("TenBenh") == null ? "" : bn.Element("TienSuBenh").Element("BenhCNhan").Element("TenBenh").Value,
                                     nammacbenh = bn.Element("TienSuBenh").Element("BenhCNhan").Element("NamPH") == null ? "" : bn.Element("TienSuBenh").Element("BenhCNhan").Element("NamPH").Value,
                                     tenbenhnn = bn.Element("TienSuBenh").Element("BenhNN").Element("TenBenhNN") == null ? "" : bn.Element("TienSuBenh").Element("BenhNN").Element("TenBenhNN").Value,
                                     nammacbnn = bn.Element("TienSuBenh").Element("BenhNN").Element("NamPHNN") == null ? "" : bn.Element("TienSuBenh").Element("BenhNN").Element("NamPHNN").Value,
                                     phanloaisukhoe = bn.Element("KetLuan").Element("PloaiSK") == null ? "" : bn.Element("KetLuan").Element("PloaiSK").Value,
                                     cacbenhtat =bn.Element("KetLuan").Element("CacBenhTat") == null ? "" : bn.Element("KetLuan").Element("CacBenhTat").Value,
                                     anh = bn.Element("Anh") == null ? "" : bn.Element("Anh").Value,
                                     cls = bn.Element("KhamCLS") == null ? "" : bn.Element("KhamCLS").Value,
                                     ls = bn.Element("KhamLS") == null ? "" : bn.Element("KhamLS").Value,
                                     TienSuBenh = bn.Descendants("TienSuBenh"),
                                 }
                                     ).ToList();
            if (thongtincanbo.Count > 0)
            {
                BaoCao.rep_pksk bao = new BaoCao.rep_pksk();
                frmIn inbaocao = new frmIn();
              
                if (thongtincanbo.First().goitinh == "Nam")
                {
                    bao.nam = true;
                }
                if (thongtincanbo.First().goitinh == "Nữ")
                {
                    bao.nu = true;

                }
                bao.ploaibt = thongtincanbo.First().cacbenhtat.ToString();
                bao.ploaiksk = thongtincanbo.First().phanloaisukhoe.ToString();
                bao.tuoi = thongtincanbo.First().tuoi.ToString();
                bao.hokhau = thongtincanbo.First().hokhau.ToString();
                bao.choo = thongtincanbo.First().choo.ToString();
                bao.ngaybatdaulam = thongtincanbo.First().ngaybatdau.ToString();
                bao.hoten = thongtincanbo.First().TenBNhan.ToString();
                bao.socmthu = thongtincanbo.First().socmnn.ToString();
                bao.ngaycap = thongtincanbo.First().ngaycap.ToString();
                bao.nghenghiep = thongtincanbo.First().nhenghiep.ToString();
                bao.tiensubenhgiadinh = thongtincanbo.First().tiensubenhgiadinh.ToString();
                bao.coqua = thongtincanbo.First().locongtac.ToString();
                bao.DUONGDAN = thongtincanbo.First().anh.ToString();
                bao.ketquacls = thongtincanbo.First().cls.ToString();
                bao.ketquals = thongtincanbo.First().ls.ToString();
                if (thongtincanbo.First().TienSuBenh != null)
                {
                    var benhCN = (from bcn in thongtincanbo.First().TienSuBenh.Descendants("BenhCNhan")
                                  select new
                                      {
                                          TenBenh = bcn.Element("TenBenh") == null ? "" : bcn.Element("TenBenh").Value,
                                          NamPH = bcn.Element("NamPH") == null ? "" : bcn.Element("NamPH").Value
                                      }).ToList();
                    if (benhCN.Count > 0)
                    {
                        foreach (var b in benhCN)
                        {
                            LichSuBenh ls = new LichSuBenh();
                            if (b.TenBenh != "")
                            {
                                ls.Tenbenh = b.TenBenh.ToString();
                                if (b.NamPH != null)
                                    ls.Namph = b.NamPH.ToString();
                                listBCN.Add(ls);
                            }
                        }
                    }

                    var benhNN = (from bcn in thongtincanbo.First().TienSuBenh.Descendants("BenhNN")
                                  select new
                                  {
                                      TenBenh = bcn.Element("TenBenhNN") == null ? "" : bcn.Element("TenBenhNN").Value,
                                      NamPH = bcn.Element("NamPHNN") == null ? "" : bcn.Element("NamPHNN").Value
                                  }).ToList();
                    if (benhNN.Count > 0)
                    {
                        foreach (var b in benhNN)
                        {
                            LichSuBenh ls = new LichSuBenh();
                            if (b.TenBenh != "")
                            {
                                ls.Tenbenh = b.TenBenh.ToString();
                                if (b.NamPH != null)
                                    ls.Namph = b.NamPH.ToString();
                                listBNN.Add(ls);
                            }
                        }
                    }
                    int k = 0;
                    do
                    {
                        tiensubenhAll ts = new tiensubenhAll();
                        if (k < listBCN.Count)
                        {
                            ts.Tenbenh = listBCN[k].Tenbenh;
                            ts.Namph = listBCN[k].Namph;
                        }
                        if (k < listBNN.Count)
                        {
                            ts.Tenbenhnn = listBNN[k].Tenbenh;
                            ts.Namphnn = listBNN[k].Namph;
                        }
                        listTS.Add(ts);
                        k++;
                    } while (k < listBCN.Count || k < listBNN.Count);
                }
                bao.DataSource = listTS;
                bao.ham();
                bao.CreateDocument();
                this.Hide();
                inbaocao.prcIN.PrintingSystem = bao.PrintingSystem;
                inbaocao.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Không tìm thấy cán bộ trong năm này");
            }
        }
        public class tiensubenhAll
        {
            private string tenbenh;

            public string Tenbenh
            {
                get { return tenbenh; }
                set { tenbenh = value; }
            }
            private string namph;

            public string Namph
            {
                get { return namph; }
                set { namph = value; }
            }
            private string tenbenhnn;

            public string Tenbenhnn
            {
                get { return tenbenhnn; }
                set { tenbenhnn = value; }
            }
            private string namphnn;

            public string Namphnn
            {
                get { return namphnn; }
                set { namphnn = value; }
            }

        }
        /// <summary>        
        /// </summary>
        /// <returns> true: nếu hệ điều hành là 64 bit và ngược lại</returns>
        private bool getOP() 
        { 
            Process ps = new Process();
            if (!Environment.Is64BitOperatingSystem)
                return false;
            else
                return true;
        }

        private void txtKhamLamSang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void xtraTab_TienSuBenh_Click(object sender, EventArgs e)
        {
            if (xtraTab_TSBenh.Focus())
            {
                xtraTab_TSBenh.Text = "Tiền sử bệnh *";
                xtraTab_TienSuBenhNN.Text = "Tiền sử bệnh nghề nghiệp";
            }
            else if(xtraTab_TienSuBenhNN.Focus())
            {
                xtraTab_TSBenh.Text = "Tiền sử bệnh";
                xtraTab_TienSuBenhNN.Text = "Tiền sử bệnh nghề nghiệp*";
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frm_NhapThongTinKSK_CB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Dispose();
        }

        private void grv_DSCB_Click(object sender, EventArgs e)
        {
            grv_DSCB_FocusedRowChanged(null, null);
        }
        SaveFileDialog dialog = new SaveFileDialog();
        private void btnGetFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;           
            dialog.InitialDirectory = "d:\\";
            dialog.Filter = "XML files (*.xml)|*.xml";
            dialog.FilterIndex = 1;
            dialog.FileName = QLBV_Library.QLBV_Ham.Read_registry("XML_CB_Path",1);
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDan.Text = dialog.FileName;               
                QLBV_Library.QLBV_Ham.Write_registry("XML_CB_Path", txtDuongDan.Text.Trim());
                path = QLBV_Library.QLBV_Ham.Read_registry("XML_CB_Path", 1);
                GetDSCB();
                resetForm();
            }
        }

        private void btnBoAnh_Click(object sender, EventArgs e)
        {
            Image img = pictureEdit1.Image;
            if (img != null)            
                img.Dispose();
            pictureEdit1.Image = null;
            chonAnh = true;
            _tenfileanh = "";
        }

        private void cboNam_EditValueChanged(object sender, EventArgs e)
        {
            GetDSCB();
        }

      
    }

}