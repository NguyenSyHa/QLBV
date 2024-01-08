using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.Class
{
    public class ClsADO
    {
        public bool Check { get; set; }
        public int IdCLS { get; set; }
        public DateTime? NgayTH { get; set; }
        public DateTime? NgayThang { get; set; }
        public string TenDV { get; set; }
        public string KetLuan { get; set; }
    }
}
