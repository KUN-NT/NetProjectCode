using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;
using System.Data.SQLite;
using CaterComman;

namespace CaterDal
{
    public partial class ManagerInfoDal
    {
        #region 获取结果集
        public List<ManagerInfo> GetList()
        {
            List<ManagerInfo> list = new List<ManagerInfo>();
            string sql = "select * from ManagerInfo";
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    MId = Convert.ToInt32(row["MId"]),
                    MName = row["MName"].ToString(),
                    MPwd = row["MPwd"].ToString(),
                    MType = Convert.ToInt32(row["MType"])
                });
            }
            return list;
        }
        #endregion

        #region 插入数据并返回受影响的行数
        public int Insert(ManagerInfo manager)
        {
            string sql = "insert into ManagerInfo(mname,mpwd,mtype) values(@name,@pwd,@type)";
            SQLiteParameter[] ps ={
                new SQLiteParameter("@name",manager.MName),
                new SQLiteParameter("@pwd",MD5Helper.EncryptString(manager.MPwd)),
                new SQLiteParameter("@type",manager.MType),
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }
        #endregion

        #region Update
        public int Update(ManagerInfo manager)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            string sql = "update managerInfo set mname=@name";
            list.Add(new SQLiteParameter("@name", manager.MName));
            if (!(manager.MPwd == "thisold"))
            {
                sql += ",mpwd=@pwd";
                list.Add(new SQLiteParameter("@pwd", MD5Helper.EncryptString(manager.MPwd)));
            }
            sql += ",mtype=@type where mid=@id";
            list.Add(new SQLiteParameter("@type", manager.MType));
            list.Add(new SQLiteParameter("@id", manager.MId));

            return SqliteHelper.ExecuteNonQuery(sql, list.ToArray());
        }
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "delete from ManagerInfo where Mid=" + id;
            //SQLiteParameter[] para ={
            //                           new SQLiteParameter("@id",manager.MId)
            //                       };
            return SqliteHelper.ExecuteNonQuery(sql);
        } 
        #endregion

        #region 登录
        public ManagerInfo Login(string name)
        {
            ManagerInfo manager = null;
            string sql = "select * from ManagerInfo where MName=@name";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@name",name)
                                    };
            DataTable dt = SqliteHelper.GetDataTable(sql, param);
            if (dt.Rows.Count > 0)
            {
                manager = new ManagerInfo()
                {
                    MId = Convert.ToInt32(dt.Rows[0][0]),
                    MName = name,
                    MPwd = dt.Rows[0][2].ToString(),
                    MType = Convert.ToInt32(dt.Rows[0][3])
                };
            }
            else
            {

            }
            return manager;
        } 
        #endregion
    }
}
