using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    //非代码生成器生成代码写在此处

    public partial class MemberInfo
    {
        public string MType { get; set; }
        public string MDiscount { get; set; }
    }
    public partial class DishInfo
    {
        public string DType { get; set; }
    }
    public partial class TableInfo
    {
        public string TType { get; set; }
    }
    public partial class OrderDetailInfo
    {
        public string DTitle { get; set; }
        public decimal DPrice { get; set; }
    }
}
