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
    public partial class HallInfoDal
    {
        public List<HallInfo> GetList()
        {
            string sql = "select * from HallInfo where HIsDelete=0";
            List<HallInfo> list = new List<HallInfo>();
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId=Convert.ToInt32(row["HId"]),
                    HTitle=row["HTitle"].ToString()
                });
            }
            return list;
        }

        public int Add(HallInfo model)
        {
            string sql = "insert into HallInfo(HTitle,HIsDelete) values(@title,0)";
            SQLiteParameter param = new SQLiteParameter("@title", model.HTitle);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }

        public int Update(HallInfo model)
        {
            string sql = "update HallInfo set HTitle=@title where HId=@id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@title",model.HTitle),
                                         new SQLiteParameter("@id",model.HId)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }

        public int Delete(string id)
        {
            string sql = "update HallInfo set HIsDelete=1 where HId=@id";
            SQLiteParameter param = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }
    }
}
