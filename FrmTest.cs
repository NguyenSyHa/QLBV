using QLBV.Providers.StoredProcedure;
using QLBV.Signature.Models;
using QLBV_Database;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV
{
    public partial class FrmTest : Form
    {
        private readonly ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;

        public FrmTest()
        {
            InitializeComponent();
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            var login = new LoginModel()
            {
                //Username = "duynguyen.19x@gmail.com",
                //Password = "Qwe123#@!",
                Username = "misaesign01@gmail.com",
                Password = "12345678@Abc",
                PhoneNumber = "0984958917",
                //FileName = @"D:\VSSOFT\SOURCE CODE\HIS\QLBV\bin\Debug\Xmls\0d7fda71b0f64120bc24f5214ed66929.xml",
                FileName = @"C:\Users\Vssoft-PC\Desktop\Sample - Copy.xml",
                //FileName = @"C:\Users\Vssoft-PC\Desktop\Book1 - Copy.xlsx",
                Id = Guid.NewGuid().ToString(),
                FirstName = "ABC",
                LastName = "Nguyen Van",
                DocumentType = Signature.Enums.DocType.Xml,
                XmlId = "bk100"
            };

            Task.Run(async () => await Signature.Signature.SignMisa(login)).Wait();
        }
    }
}
