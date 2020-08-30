using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    /// <summary>
    /// 结账
    /// 1、扣除会员余额
    /// 2、更新付款状态 IsPay
    /// 3、更新餐桌状态 TIsFree
    /// </summary>
    public partial class OrderPayDal
    {
        public int Pay(bool isUseMoney,int memberId,decimal payMoney,int orderid,decimal discount)
        {
            //创建数据库的链接对象
            using (SQLiteConnection conn = new SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conter"].ConnectionString))
            {
                int result = 0;
                //由数据库链接对象创建事务
                conn.Open();
                SQLiteTransaction tran = conn.BeginTransaction();

                //创建command对象
                SQLiteCommand cmd=new SQLiteCommand();
                //将命令对象启用事务
                cmd.Transaction = tran;
                //执行各命令
                string sql = "";
                SQLiteParameter[] ps;
                try
                {
                    //1、根据是否使用余额决定扣款方式
                    if (isUseMoney)
                    {
                        //使用余额
                        sql = "update MemberInfo set mMoney=mMoney-@payMoney where mid=@mid";
                        ps = new SQLiteParameter[]
                        {
                            new SQLiteParameter("@payMoney", payMoney),
                            new SQLiteParameter("@mid", memberId)
                        };
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(ps);
                        result+=cmd.ExecuteNonQuery();
                    }

                    //2、将订单状态为IsPage=1
                    sql = "update orderInfo set isPay=1,memberId=@mid,discount=@discount where oid=@oid";
                    ps = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mid", memberId),
                        new SQLiteParameter("@discount", discount),
                        new SQLiteParameter("@oid", orderid)
                    };
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();

                    //3、将餐桌状态IsFree=1
                    sql = "update tableInfo set tIsFree=1 where tid=(select tableId from orderinfo where oid=@oid)";
                    SQLiteParameter p = new SQLiteParameter("@oid", orderid);
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(p);
                    result += cmd.ExecuteNonQuery();

                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    result = 0;
                    //回滚事务
                    tran.Rollback();
                }
                return result;
            }
        }
    
    }
}
