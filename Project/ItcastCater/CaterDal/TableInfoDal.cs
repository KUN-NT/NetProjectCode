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
    public partial class TableInfoDal
    {
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = "select T.* ,H.HTitle TType from TableInfo T left join HallInfo H on T.THallId==H.Hid where T.TIsDelete=0 and H.HIsDelete=0";
            if (dic.Count > 0)
            {
                foreach (var key in dic)
                {
                    sql += " and " + key.Key + "=" + key.Value;
                }
            }
            List<TableInfo> list = new List<TableInfo>();
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId=Convert.ToInt32(row["TId"]),
                    TTitle=row["TTitle"].ToString(),
                    TType=row["TType"].ToString(),
                    TIsFree=Convert.ToBoolean(row["TIsFree"])
                });
            }
            return list;
        }
        public int Add(TableInfo model)
        {
            string sql = "insert into TableInfo(TTitle,THallId,TIsFree,TIsDelete) values(@title,@hall,@free,0)";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@title",model.TTitle),
                                         new SQLiteParameter("@hall",model.THallId),
                                         new SQLiteParameter("@free",model.TIsFree)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }
        public int Update(TableInfo model)
        {
            string sql = "update TableInfo set TTitle=@title,THallId=@hall,TIsFree=@free where Tid=@id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@title",model.TTitle),
                                         new SQLiteParameter("@hall",model.THallId),
                                         new SQLiteParameter("@free",model.TIsFree),
                                         new SQLiteParameter("@id",model.TId)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }
        public int Delete(string id)
        {
            string sql = "update TableInfo set TIsDelete=1 where Tid=@id";
            SQLiteParameter param =new SQLiteParameter("@id",id);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }
    }
}
