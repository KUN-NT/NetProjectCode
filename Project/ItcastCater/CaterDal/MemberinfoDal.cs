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
    public partial class MemberinfoDal
    {
        #region 查询所有/筛选
        public List<MemberInfo> SelectAll(Dictionary<string, string> dic)
        {
            string sql = "select m.*,t.MTitle from MemberInfo m left join memberTypeInfo t on m.Mtypeid=t.mid where m.misdelete=0";
            if (dic.Count > 0)
            {
                foreach (var pair in dic)
                {
                    sql += " and " + pair.Key + " like '%" + pair.Value + "%'";
                }
            }
            List<MemberInfo> list = new List<MemberInfo>();
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new MemberInfo()
                {
                    MId = Convert.ToInt32(row["MId"]),
                    MName = row["MName"].ToString(),
                    MPhone = row["MPhone"].ToString(),
                    MMoney = Convert.ToDecimal(row["MMoney"]),
                    MType = row["MTitle"].ToString()
                });
            }
            return list;
        } 
        #endregion

        #region 添加
        public int Add(MemberInfo model)
        {
            string sql = "insert into MemberInfo(Mtypeid,mname,mphone,mmoney,misdelete) values(@typeid,@name,@phone,@money,0)";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@typeid",model.MTypeId),
                                        new SQLiteParameter("@name",model.MName),
                                        new SQLiteParameter("@phone",model.MPhone),
                                        new SQLiteParameter("@money",model.MMoney),
                                        new SQLiteParameter("@isdelete",0)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 修改
        public int Update(MemberInfo model)
        {
            string sql = "update MemberInfo set Mtypeid=@type,mname=@name,mphone=@phone,mmoney=@money where mid=@id";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@type",model.MTypeId),
                                        new SQLiteParameter("@name",model.MName),
                                        new SQLiteParameter("@phone",model.MPhone),
                                        new SQLiteParameter("@money",model.MMoney),
                                        new SQLiteParameter("@id",model.MId)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "update memberinfo set misdelete=1 where mid=@id";
            SQLiteParameter param = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        public MemberInfo SelectMember(string id)
        {
            string sql = "select m.*,t.MTitle,t.MDiscount from MemberInfo m left join memberTypeInfo t on m.Mtypeid=t.mid where m.misdelete=0 and m.mid="+id;
            
            DataTable table = SqliteHelper.GetDataTable(sql);
            MemberInfo model=null;
            foreach (DataRow row in table.Rows)
            {
                model= new MemberInfo()
                {
                    MId = Convert.ToInt32(row["MId"]),
                    MName = row["MName"].ToString(),
                    MPhone = row["MPhone"].ToString(),
                    MMoney = Convert.ToDecimal(row["MMoney"]),
                    MType = row["MTitle"].ToString(),
                    MDiscount=row["MDiscount"].ToString()
                };
            }
            return model;
        }
    }
}
