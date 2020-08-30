using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    /// <summary>
    /// RemoveInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RemoveInfo
    {
        public RemoveInfo()
        { }
        #region Model
        private int _id;
        private int _rid;
        private string _rname;
        private string _rtable;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RId
        {
            set { _rid = value; }
            get { return _rid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RName
        {
            set { _rname = value; }
            get { return _rname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RTable
        {
            set { _rtable = value; }
            get { return _rtable; }
        }
        #endregion
    }
}
