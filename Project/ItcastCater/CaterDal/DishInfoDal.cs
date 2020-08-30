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
    public partial class DishInfoDal
    {
        #region 查询全部
        public List<DishInfo> GetList(Dictionary<string, string> dic)
        {
            List<DishInfo> list = new List<DishInfo>();
            string sql = "select di.*,type.DTitle as DType from DishInfo as di left join DishTypeInfo as type on di.dtypeid=type.did where di.disdelete=0 and type.disdelete=0";
            if (dic != null)
            {
                if (dic.Count > 0)
                {
                    foreach (var pair in dic)
                    {
                        sql += " and " + pair.Key + " like '%" + pair.Value + "%'";
                    }
                }
            }
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId = Convert.ToInt32(row["DId"]),
                    DTitle = row["DTitle"].ToString(),
                    DType = row["DType"].ToString(),
                    DPrice = Convert.ToDecimal(row["DPrice"]),
                    DChar = row["DChar"].ToString()
                });
            }
            return list;
        } 
        #endregion

        #region 添加
        public int Add(DishInfo model)
        {
            string sql = "Insert into DishInfo(Dtitle,DTypeId,DPrice,DChar,DIsDelete) values(@title,@id,@price,@char,0)";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@title",model.DTitle),
                                        new SQLiteParameter("@id",model.DTypeId.ToString()),
                                        new SQLiteParameter("@price",model.DPrice.ToString()),
                                        new SQLiteParameter("@char",model.DChar),
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 修改
        public int Update(DishInfo model)
        {
            string sql = "update DishInfo set Dtitle=@title,DTypeId=@Tid,DPrice=@price,DChar=@char where DId=@id";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@title",model.DTitle),
                                        new SQLiteParameter("@Tid",model.DTypeId.ToString()),
                                        new SQLiteParameter("@price",model.DPrice.ToString()),
                                        new SQLiteParameter("@char",model.DChar),
                                        new SQLiteParameter("@id",model.DId.ToString())
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "update DishInfo set DIsDelete=1 where Did=@id";
            SQLiteParameter parm = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, parm);
        } 
        #endregion
    }
}
