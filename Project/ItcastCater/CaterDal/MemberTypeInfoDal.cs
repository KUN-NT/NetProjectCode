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
    public partial class MemberTypeInfoDal
    {
        #region 查询
        public List<MemberTypeInfo> GetList()
        {
            string sql = "select * from MemberTypeInfo where MIsDelete=0";
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            DataTable table = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new MemberTypeInfo()
                {
                    MId = Convert.ToInt32(row[0]),
                    MTitle = row[1].ToString(),
                    MDiscount = Convert.ToDecimal(row[2])
                });
            }
            return list;
        } 
        #endregion

        #region 添加
        public int Add(MemberTypeInfo model)
        {
            string sql = "insert into MemberTypeInfo(MTitle,MDiscount,MIsDelete) values(@title,@discount,@isdelete)";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@title",model.MTitle),
                                         new SQLiteParameter("@discount",model.MDiscount),
                                         new SQLiteParameter("@isdelete",model.MIsDelete)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 删除
        public int Delete(string id)
        {
            string sql = "update MemberTypeInfo set MIsDelete=1 where Mid=@id";
            SQLiteParameter[] param ={
                                         new SQLiteParameter("@id",id)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion

        #region 修改
        public int Update(MemberTypeInfo model)
        {
            string sql = "update MemberTypeInfo set Mtitle=@title,MDiscount=@count where Mid=@id";
            SQLiteParameter[] param ={
                                       new SQLiteParameter("@title",model.MTitle),
                                       new SQLiteParameter("@count",model.MDiscount),
                                       new SQLiteParameter("@id",model.MId)
                                  };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        } 
        #endregion
    }
}
