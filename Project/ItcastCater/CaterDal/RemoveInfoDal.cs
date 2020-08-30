using CaterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    public partial class RemoveInfoDal
    {
        #region 添加
        public int Add(RemoveInfo remove)
        {
            string sql = "insert into RemoveInfo(Rid,RName,RTable) values(@id,@name,@table)";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@id",remove.RId),
                                         new SQLiteParameter("@name",remove.RName),
                                         new SQLiteParameter("@table",remove.RTable)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 查询
        public List<RemoveInfo> GetList()
        {
            List<RemoveInfo> list = new List<RemoveInfo>();
            string sql = "select * from RemoveInfo";
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new RemoveInfo()
                {
                    RId = Convert.ToInt32(row["RId"]),
                    RName = row["RName"].ToString(),
                    RTable = row["RTable"].ToString()
                });
            }
            return list;
        } 
        #endregion

        #region 获取在RemoveInfo表中id
        public int GetId(string id, string table)
        {
            string sql = "select rid from RemoveInfo where Rtable=@table and Rid=@id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@table",table),
                                         new SQLiteParameter("@id",id)
                                    };
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param));
        } 
        #endregion

        #region 还原数据
        public int ReturnData(string id, string table)
        {
            string head = table[0].ToString();
            string sql = "update " + table + " set "+head+"isdelete=0 where "+head+"id=" + id;
            int rid = GetId(id, table);
            string rsql = "delete from RemoveInfo where rid=" + rid;
            SqliteHelper.ExecuteNonQuery(rsql);
            return SqliteHelper.ExecuteNonQuery(sql);
        } 
        #endregion

        #region 永久删除数据
        public int DeleteData(string id, string table)
        {
            string head = table[0].ToString();
            string sql = "delete from  " + table + " where " + head + "id=" + id;
            int rid = GetId(id, table);
            string rsql = "delete from RemoveInfo where rid=" + rid;
            SqliteHelper.ExecuteNonQuery(rsql);
            return SqliteHelper.ExecuteNonQuery(sql);
        } 
        #endregion
    }
}
