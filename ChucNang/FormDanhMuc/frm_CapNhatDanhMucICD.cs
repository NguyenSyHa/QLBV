using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace QLBV.FormNhap
{
    public partial class frm_CapNhatDanhMucICD : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data;
        List<CBICD> _listCB;

        public frm_CapNhatDanhMucICD()
        {
            InitializeComponent();
        }

        private void btnChuongBenh_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _listCB = data.CBICDs.ToList();
            if(_listCB.Count>0)
            if (MessageBox.Show("Bảng CBICD đã có dữ liệu, bạn có chắc chắn muốn cập nhật lại không", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                #region cập nhật chương bệnh vào bảng CBICD               
                foreach (CBICD a in _listCB)
                {
                    data.CBICDs.Remove(a);
                }
                data.SaveChanges();
                _listCB = GetListCBICD();

                foreach (CBICD a in _listCB)
                    data.CBICDs.Add(a);
                data.SaveChanges();

                #endregion
                //CapNhatMaSoCB();
                MessageBox.Show("Đã cập nhật chương bệnh vào cơ sở dữ liệu");
                LoadMBICD();
            }
        }
        /// <summary>
        /// Lấy ra danh sách chương bệnh để add vào bảng Chương bệnh
        /// </summary>
        /// <returns></returns>
        private List<CBICD> GetListCBICD()
        {
            _listCB.Clear();
            _listCB.Add(new CBICD { MSCB = 1, TENCB = "Chương I: Bệnh nhiễm khuẩn và ký sinh vật - Chapter I: Certain infectious and parasistic discases" });
            _listCB.Add(new CBICD { MSCB = 2, TENCB = "Chương II: Khối u - Chapter II: Neoplasms" });
            _listCB.Add(new CBICD { MSCB = 3, TENCB = "Chương III: Bệnh của máu, cơ quan tạo máu và cơ chế miễn dịch - Chapter III: Diseases of the blood and blood-forming organ and disorders involving immune mechanism" });
            _listCB.Add(new CBICD { MSCB = 4, TENCB = "Chương IV: Bệnh nội tiết, dinh dưỡng chuyển hóa - Chapter IV: Endocrine, Nutritional and metabolic diseases" });
            _listCB.Add(new CBICD { MSCB = 5, TENCB = "Chương V: Rối loạn tâm thần và hành vi - Chapter V: Mental and behavioural disorders" });
            _listCB.Add(new CBICD { MSCB = 6, TENCB = "Chương VI: Bệnh của hệ thống thần kinh - Chapter VI: Diseases of the nervous system" });
            _listCB.Add(new CBICD { MSCB = 7, TENCB = "Chương VII: Bệnh của mắt và phần phụ - Chapter VII: Diseaser of the eye and adnexa" });
            _listCB.Add(new CBICD { MSCB = 8, TENCB = "Chương VIII: Bệnh của tai và xương chũm - Chapter VIII: Diseases of the ear and mastoid process" });
            _listCB.Add(new CBICD { MSCB = 9, TENCB = "Chương IX: Bệnh của hệ tuần hoàn - Chapter IX: Diseases of the circulatory system" });
            _listCB.Add(new CBICD { MSCB = 10, TENCB = "Chương X: Bệnh của hệ hô hấp - Chapter X: Diseases of the respiratory system" });
            _listCB.Add(new CBICD { MSCB = 11, TENCB = "Chương XI: Bệnh của hệ tiêu hóa - Chapter XI: Diseases of the digestive system" });
            _listCB.Add(new CBICD { MSCB = 12, TENCB = "Chương XII: Bệnh của da và tổ chức dưới da - Chapter XII: Diseases of skin and subcutanneous tissue" });
            _listCB.Add(new CBICD { MSCB = 13, TENCB = "Chương XIII: Bệnh của hệ thống cơ, xương và mô liên kết - Chapter XIII: Diseases of the musculoskeletal system" });
            _listCB.Add(new CBICD { MSCB = 14, TENCB = "Chương XIV: Bệnh của hệ tiết niệu, sinh dục - Chapter XIV: Diseases of the genitourinay system B212" });
            _listCB.Add(new CBICD { MSCB = 15, TENCB = "Chương XV: Chửa đẻ và sau đẻ - Chapter XV: Pregnancy, childbirth and the puerperrium" });
            _listCB.Add(new CBICD { MSCB = 16, TENCB = "Chương XVI: Một số bệnh trong thời kỳ chu sinh - Chapter XVI: certain conditions orginating in the perinaltal period" });
            _listCB.Add(new CBICD { MSCB = 17, TENCB = "Chương XVII: Dị dạng bẩm sinh, biến dạng của cromosom - Chapter XVII: Congenital malformations, deformations and chromosomal abnormalities" });
            _listCB.Add(new CBICD { MSCB = 18, TENCB = "Chương XVIII: Triệu chứng, dấu hiệu và phát hiện bất thường lâm sàng, xét nghiệm - Chapter XVIII: Symptoms, signs and abnormal clinical and laboratory findings, not elsewwhere classifield" });
            _listCB.Add(new CBICD { MSCB = 19, TENCB = "Chương XIX: Vết thương, ngộ độc và kết quả của các nguyên nhân bên ngoài - Chapter XIX: Injury, poisoning and cetain other consequences of external causes" });
            _listCB.Add(new CBICD { MSCB = 20, TENCB = "Chương XX: Nguyên nhân bên ngoài của bệnh tật và tử vong - Chapter XX: Enternal causes of morbidity and mortality" });
            _listCB.Add(new CBICD { MSCB = 21, TENCB = "Chương XXI: Các yếu tố ảnh hưởng đến sức khỏe người khám nghiệm và điều tra - Chapter XXI: Person encountering health services for examination and investigation" });
            return _listCB;
        }
        private string getFstString(string a)
        {
            string b = "";
            if (a == "")
                b = "";
            else
            {
                #region lấy ra list các ký tự đầu tiên của các phần tử của chuỗi
                string[] strArr = a.Split(',');// mảng các phần tử của chuỗi        
                List<string> chStr = new List<string>();//list các ký tự đầu tiên của các phần tử của chuỗi
                foreach (string s in strArr)
                {
                    if (s != "")
                    {
                        string ch = s.Substring(0, 1).ToUpper(); // ký tự đầu tiên của 1 phần tử
                        if (chStr.Count > 0)
                        {
                            int count = 0;
                            foreach (string c in chStr)
                            {
                                if (c == (ch))
                                    count++;
                            }
                            if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                                chStr.Add(ch);
                        }
                        else
                            chStr.Add(ch);
                    }
                #endregion
                }
                chStr.Sort(); // sắp xếp các phần tử trong list
                foreach (string c in chStr)
                {
                    if (c != "")
                        b += getEndtring(c, a) + ";";
                }
                if (b.LastIndexOf(";") == b.Length - 1)
                    b = b.Substring(0, b.Length - 1);
            }
            return b;
        }
        private string getEndtring(string kytu, string chuoi)
        {
            string trave = "";
            #region lấy ra list các phần cuối các phần tử của chuỗi
            string[] strArr = chuoi.Split(',');// mảng các phần tử của chuỗi        
            List<int> chInt = new List<int>();//list các ký tự cuối của các phần tử của chuỗi
            foreach (string s in strArr)
            {
                if (s != "")
                {
                    if ((s.Substring(0, 1).ToLower()) == (kytu.ToLower()))
                    {
                        string ch = s.Substring(1, s.Length - 1); // phần đuôi của 1 phần tử
                        int rs;
                        if (Int32.TryParse(ch, out rs))
                        {

                            if (chInt.Count > 0)
                            {
                                int count = 0;
                                foreach (int i in chInt)
                                {
                                    if (i == Convert.ToInt32(ch))
                                        count++;
                                }
                                if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                                    chInt.Add(Convert.ToInt32(ch));
                            }
                            else
                                chInt.Add(Convert.ToInt32(ch));
                        }
                    }
                }
            }
            #endregion
            chInt.Sort(); // sắp xếp các phần tử trong list
            #region gộp chuỗi
            int n = -2;
            for (int so = 0; so < chInt.Count; so++)
            {
                if (chInt[so] != chInt[chInt.Count - 1])// s không phải phần tử cuối
                {
                    string trf;
                    if (chInt[so] < 10)
                        trf = String.Format("{0:00}", chInt[so]);
                    else
                        trf = chInt[so].ToString();

                    if (n == -2)// phần tử đầu tiên
                    {
                        trave = kytu + trf;
                    }
                    else
                    {
                        if (chInt[so] == n + 1) // có tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "-";
                        }
                        else //không tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "," + kytu + trf;
                            else
                            {
                                if (n < 10)
                                    trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                                else
                                    trave = trave + kytu + n.ToString() + "," + kytu + trf;
                            }
                        }
                    }
                    n = chInt[so];
                }
                else// là phần tử cuối
                {
                    string trf;
                    if (chInt[so] < 10)
                        trf = String.Format("{0:00}", chInt[so]);
                    else
                        trf = chInt[so].ToString();
                    if (n == -2)// phần tử đầu tiên
                    {
                        trave = kytu + trf;
                    }
                    else
                    {
                        if (chInt[so] == n + 1) // có tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "-" + kytu + trf;
                            else
                                trave = trave + kytu + trf;
                        }
                        else //không tăng
                        {
                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "," + kytu + trf;
                            else
                            {
                                if (n < 10)
                                    trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                                else
                                    trave = trave + kytu + n.ToString() + "," + kytu + trf;
                            }
                        }
                    }
                    n = chInt[so];
                }
            }
            return trave;
            #endregion
        }
        /// <summary>
        ///  Cập nhật MSCB trong bảng CBICD vào cột MSCB trong bảng ICD10 căn cứ vào tên chương bệnh có chứa STT chương
        /// </summary>      
        //private void CapNhatMaSoCB()
        //{
        //    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    _listCB = data.CBICDs.ToList();
        //    List<ICD10> _listICD = data.ICD10.ToList();
        //    int i = 0;
        //    if (_listCB.Count == 0)
        //        MessageBox.Show("Bạn chưa cập nhật danh mục chương bệnh");
        //    else
        //    {
        //        try
        //        {
        //            foreach (ICD10 a in _listICD)
        //            {
        //                if (a.TenCB.Contains("Chương I:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương I:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương II:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương II:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương III:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương III:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương IV:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương IV:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương V:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương V:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương VI:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương VI:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương VII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương VII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương VIII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương VIII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương IX:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương IX:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;

        //                }
        //                else if (a.TenCB.Contains("Chương X:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương X:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XI:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XI:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XIII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XIII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XIV:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XIV:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XV:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XV:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XVI:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XVI:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XVII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XVII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XVIII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XVIII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XIX:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XIX:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XX:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XX:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XXI:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XXI:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else if (a.TenCB.Contains("Chương XXII:"))
        //                {
        //                    var qCB = _listCB.Where(p => p.TENCB.Contains("Chương XXII:")).ToList();
        //                    if (qCB.Count > 0)
        //                    {
        //                        a.IDCB = qCB.Max(p => p.IDCB);
        //                        i++;
        //                    }
        //                    continue;
        //                }
        //                else
        //                { }
        //            }
        //            data.SaveChanges();
        //            MessageBox.Show("Cập nhật IDCB vào bảng ICD10 thành công " + i.ToString() + "dòng.");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //}
        /// <summary>
        /// Cập nhật danh mục mã bênh từ bảng ICD10 vào bảng MBICD
        /// </summary>       
        //private void btnCN_MBICD_Click(object sender, EventArgs e)
        //{
        //    #region  Lấy các ICD có 3 ký tự từ bảng ICD10 vào bảng MBICD
        //    //Lấy các ICD có 3 ký tự từ bảng ICD10 vào bảng MBICD----------------------------------------------------------------
        //    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    bool kt = true;
        //    List<MBICD> _lMaBenh = data.MBICDs.ToList();
        //    if (_lMaBenh.Count > 0)
        //    {
        //        if (MessageBox.Show("Bảng MBICD đã có dữ liệu, bạn có chắc chắn muốn cập nhật lại không", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            foreach (MBICD a in _lMaBenh)
        //            {
        //                data.MBICDs.Remove(a);
        //            }
        //            data.SaveChanges();
        //            kt = true;
        //        }
        //        else
        //            kt = false;
        //    }

        //    if (kt)
        //    {
        //        List<ICD10> _listICD = data.ICD10.Where(p => p.Status == 1).Where(p => p.MaICD != null && p.MaICD.Length == 3).ToList();
        //        var qICD = (from a in _listICD
        //                    group a by new {  a.TenWHO, a.TenWHOe } into kq
        //                    select new
        //                        {
                                
        //                            kq.Key.TenWHO,
        //                            kq.Key.TenWHOe,
        //                            MaICD = String.Join(",", kq.Select(p => p.MaICD.Trim()).Distinct()),
        //                            //  EndChar = (a.maicd==null || a.maicd.Length <3) ? "" : a.maicd.Substring(a.maicd.Length-3, 3);
        //                        }).ToList();
        //        var qICD2 = (from a in qICD
        //                     select new
        //                     {
                     
        //                         a.TenWHO,
        //                         a.TenWHOe,
        //                         MaICD = getFstString(a.MaICD),
        //                         DSMaICD = a.MaICD
        //                     }).ToList();
        //        var qICD3 = (from a in qICD2
        //                     select new { a.TenWHO, a.TenWHOe, a.MaICD, a.DSMaICD, EndChar = a.MaICD.Substring(a.MaICD.Length - 3, 3) }).ToList();
        //        _listCB = data.CBICDs.ToList();

        //        var qICD4 = (from a in qICD3 join b in _listCB on a.IDCB equals b.IDCB select new { a.IDCB, a.TenWHO, a.TenWHOe, a.MaICD, a.DSMaICD, a.EndChar, b.MSCB }).OrderBy(p => p.MSCB).ThenBy(p => p.EndChar).ToList();
        //        int i = 0;
        //        foreach (var a in qICD4)
        //        {
        //            i++;
        //            MBICD mbICD = new MBICD();
        //            mbICD.MaICD = a.MaICD;
        //            mbICD.DS_MaICD = a.DSMaICD;
                
        //            mbICD.TenWHO = a.TenWHO;
        //            mbICD.TenWHOe = a.TenWHOe;
        //            mbICD.STATUS = true;
        //            mbICD.STT = i;
        //            data.MBICDs.Add(mbICD);
        //        }
        //        data.SaveChanges();
        //        MessageBox.Show("Cập nhật thành công");
        //        LoadMBICD();
        //    }
        //    #endregion Lấy các ICD có 3 ký tự từ bảng ICD10 vào bảng MBICD
        //    #region
        //    #endregion

        //}
        /// <summary>
        /// Cập nhật ID_MBICD trong bảng MBICD vào bảng ICD10
        /// </summary>       
        private void btn_CN_ID_MBICD_Click(object sender, EventArgs e)
        {
            //data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //List<MBICD> _listMBICD = data.MBICDs.ToList();
            //List<ICD10> _listICD10 = data.ICD10.Where(p => p.MaICD != null && p.MaICD.Length >= 3).ToList();
            //foreach (ICD10 icd in _listICD10)
            //{
            //    foreach (MBICD mbicd in _listMBICD)
            //    {
            //        string str = icd.MaICD.Substring(0, 3);
            //        if (str == mbicd.MaICD)
            //        {
            //            icd.ID_MBICD = mbicd.ID_MBICD;
            //            break;
            //        }
            //    }
            //}
        }

        private void frm_CapNhatDanhMucICD_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<CBICD> qCB = data.CBICDs.ToList();
            List<CBICD> qCB_TK = qCB;
            qCB_TK.Insert(0, new CBICD {IDCB = 0, TENCB = "Tất cả" });
            lupCB.DataSource = qCB;
            lupCB_TK.Properties.DataSource = qCB_TK;
            lupCB_TK.EditValue = 0;
        
            LoadMBICD();
            load = true;
        }
        List<MBICD> _lMBICD = new List<MBICD>();
        private void LoadMBICD()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lMBICD = data.MBICDs.OrderBy(p => p.STT).ToList();
            string maICD = txtMaICD.Text.Trim();
            string tenWho = txtTenWHO.Text.Trim();
            string tenWhoe = txtTenWHOe.Text.Trim();
            int stt = (txtSTT.Text == null || txtSTT.Text.Trim() == "") ? -1 : Convert.ToInt32(txtSTT.Text);
            int idCB = 0;
            if (lupCB_TK.EditValue != null)
                idCB = Convert.ToInt32(lupCB_TK.EditValue);
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            binMBICD.DataSource=        _lMBICD.Where(p => (maICD == "" || p.DS_MaICD.Contains(maICD)) && (tenWho == "" || p.TenWHO.Contains(tenWho)) && (tenWhoe == "" || p.TenWHOe.Contains(tenWhoe)) && (stt == -1 || p.STT == stt) && (idCB == 0 || p.IDCB == idCB)).ToList();
            grc_MBICD.DataSource = binMBICD;
           
        }

        private void grv_MBICD_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int row = grv_MBICD.FocusedRowHandle;
            if (row >= 0)
            {
                data.SaveChanges();
                int stt = 0;
                bool kt = true;

                if (grv_MBICD.GetRowCellValue(row, colSTT) != null && grv_MBICD.GetRowCellValue(row, colSTT).ToString().Trim() != "")
                {
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    int id_MBICD = Convert.ToInt32(Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colIDMB)));
                    bool status = Convert.ToBoolean(Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colTrangThaiSD)));
                    stt = Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colSTT));
                    MBICD sua = data.MBICDs.Single(p => p.ID_MBICD == id_MBICD);
                    if (kt)
                    {
                        if (grv_MBICD.GetRowCellValue(row, colTenWHO) != null)
                            sua.TenWHO = grv_MBICD.GetRowCellValue(row, colTenWHO).ToString();
                        if (grv_MBICD.GetRowCellValue(row, colTenWHOe) != null)
                            sua.TenWHOe = grv_MBICD.GetRowCellValue(row, colTenWHOe).ToString();
                        if (grv_MBICD.GetRowCellValue(row, colMaBenh) != null)
                            sua.MaICD = grv_MBICD.GetRowCellValue(row, colMaBenh).ToString();
                        if (grv_MBICD.GetRowCellValue(row, DS_MaICD) != null)
                            sua.DS_MaICD = grv_MBICD.GetRowCellValue(row, DS_MaICD).ToString();
                        if (grv_MBICD.GetRowCellValue(row, colTenCB) != null && grv_MBICD.GetRowCellValue(row, colTenCB).ToString() != "")
                            sua.IDCB = Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colTenCB));
                        sua.STATUS = status;
                        sua.STT = stt;
                        data.SaveChanges();
                    }
                }
            }
            else {
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                MBICD sua = new MBICD();
                if (grv_MBICD.GetRowCellValue(row, colTenWHO) != null)
                    sua.TenWHO = grv_MBICD.GetRowCellValue(row, colTenWHO).ToString();
                if (grv_MBICD.GetRowCellValue(row, colTenWHOe) != null)
                    sua.TenWHOe = grv_MBICD.GetRowCellValue(row, colTenWHOe).ToString();
                if (grv_MBICD.GetRowCellValue(row, colMaBenh) != null)
                    sua.MaICD = grv_MBICD.GetRowCellValue(row, colMaBenh).ToString();
                if (grv_MBICD.GetRowCellValue(row, DS_MaICD) != null)
                    sua.DS_MaICD = grv_MBICD.GetRowCellValue(row, DS_MaICD).ToString();
                if (grv_MBICD.GetRowCellValue(row, colTenCB) != null && grv_MBICD.GetRowCellValue(row, colTenCB).ToString() != "")
                    sua.IDCB = Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colTenCB));
                bool status = Convert.ToBoolean(Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colTrangThaiSD)));
             int   stt = Convert.ToInt32(grv_MBICD.GetRowCellValue(row, colSTT));
                sua.STATUS = status;
                sua.STT = stt;
                data.MBICDs.Add(sua);
                data.SaveChanges();
                SuaSTT();
                LoadMBICD();
            }
        }

        private void btn_Timkiem_Click(object sender, EventArgs e)
        {
          
        }
        bool load = false;
        private void txtMaICD_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
            LoadMBICD();
        }

        private void txtTenWHO_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                LoadMBICD();
        }

        private void txtTenWHOe_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                LoadMBICD();
        }

        private void txtSTT_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                LoadMBICD();
        }

        private void lupCB_TK_EditValueChanged(object sender, EventArgs e)
        {
            if (load)
                LoadMBICD();
        }

        private void grv_MBICD_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoa") {
                DialogResult _result=     MessageBox.Show("Bạn muốn xóa: "+(grv_MBICD.GetRowCellValue(e.RowHandle, colMaBenh)==null?"":grv_MBICD.GetRowCellValue(e.RowHandle, colMaBenh).ToString()),"Hỏi xóa",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
           if (_result == DialogResult.Yes)
           {
               data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
               int id_MBICD = Convert.ToInt32(Convert.ToInt32(grv_MBICD.GetRowCellValue(e.RowHandle, colIDMB)));
               List<MBICD> xoa = data.MBICDs.Where(p => p.ID_MBICD == id_MBICD).ToList();
               foreach (var item in xoa)
               {
                   data.MBICDs.Remove(item);
                   data.SaveChanges();
               }
               SuaSTT();
               LoadMBICD();
           }
            }
        }
        void SuaSTT() { 
            List<MBICD> _lMB=data.MBICDs.OrderBy(p=>p.STT).ToList();
            int i = 1;
            foreach (var item in _lMB) {
                item.STT = i;
                i++;
            }
            data.SaveChanges();
        }
        private void btnSoTT_Click(object sender, EventArgs e)
        {
          
        }
        //private void grv_MBICD_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    GridView view = sender as GridView;

        //    if (view.FocusedColumn.FieldName == "colSTT"|| view.FocusedColumn.FieldName == "colTenBenh")

        //        e.Cancel = true;

        //}
    }
}