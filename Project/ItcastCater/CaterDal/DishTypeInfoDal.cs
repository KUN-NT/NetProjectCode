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
    public partial class DishTypeInfoDal
    {
        #region 查询所有
        public List<DishTypeInfo> GetList()
        {
            string sql = "select * from DishTypeInfo where DIsDelete=0";
            List<DishTypeInfo> list = new List<DishTypeInfo>();
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    DId = Convert.ToInt32(row["DId"]),
                    DTitle = row["DTitle"].ToString()
                });
            }
            return list;
        } 
        #endregion

        #region 添加
        public int Add(DishTypeInfo model)
        {
            string sql = "insert into DishTypeInfo(DTitle,DIsdelete) values(@title,@del)";
            SQLiteParameter[] param =
            {
                new SQLiteParameter("@title",model.DTitle),
                new SQLiteParameter("@del",model.DIsDelete)
            };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 修改
        public int Update(DishTypeInfo model)
        {
            string sql = "update DishTypeInfo set DTitle=@title where Did=@id";
            SQLiteParameter[] param =
            { 
                new SQLiteParameter("@title", model.DTitle),
                new SQLiteParameter("@id",model.DId)
            };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "update DishTypeInfo set DIsDelete=1 where DId=@id";
            SQLiteParameter param = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion
    }
}
