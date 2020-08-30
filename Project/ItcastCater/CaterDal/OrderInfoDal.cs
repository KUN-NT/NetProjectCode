using CaterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace CaterDal
{
    public partial class OrderInfoDal
    {
        //OrderInfo 谁 在那个桌子上 什么时候 多少钱 买单没
        //OrderDetailInfo 点了什么 点了多少
        public int KaiDan(int id)
        {
            string sql =
                //订单表插入数据
                "insert into OrderInfo(ODate,IsPay,TableId) values(datetime('now','localtime'),0,@id);"
                //更新餐桌
                + "update TableInfo set TIsFree=0 where TId=@id;"
                //获得单号
                + "select Oid from OrderInfo order by ODate desc limit 0,1";
            SQLiteParameter param = new SQLiteParameter("@id", id);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param));
        }

        public int DianCai(int Did, int Oid)
        {
            string sql = "select Count(*) from OrderDetailInfo where OrderId=@OId and DishId=@DId";
            SQLiteParameter[] param ={
                                        new SQLiteParameter("@Oid",Oid),
                                        new SQLiteParameter("@Did",Did)
                                    };
            if (Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param)) > 0)
            {
                sql = "update OrderDetailInfo set count=count+1 where OrderId=@OId and DishId=@DId";
            }
            else
            {
                sql = "insert into OrderDetailInfo(OrderId,DishId,Count) values(@Oid,@Did,1)";
            }
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }

        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            //string sql = "select * from OrderDetailInfo where orderid=(select Oid from OrderInfo where tableid=@id)";
            string sql = "select odi.oid,di.dTitle,di.dPrice,odi.count from dishinfo as di inner join orderDetailInfo as odi on di.did=odi.dishid where odi.orderId=@orderid";
            SQLiteParameter param = new SQLiteParameter("@orderid", orderId);
            DataTable table = SqliteHelper.GetDataTable(sql, param);
            List<OrderDetailInfo> list = new List<OrderDetailInfo>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["oid"]),
                    DTitle = row["dtitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"]),
                    Count = Convert.ToInt32(row["count"])
                });
            }
            return list;
        }

        public int GetOrderIdByTableId(int tableid)
        {
            string sql = "select OId from OrderInfo where IsPay=0 and TableId=@tableId";
            SQLiteParameter param = new SQLiteParameter("@tableId", tableid);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param));
        }

        public int GetCountByOId(string oid, string count)
        {
            string sql = "update OrderDetailInfo set Count=@count where OId=@oid";
            SQLiteParameter[] param ={
                                 new SQLiteParameter("@count",count),
                                 new SQLiteParameter("@oid",oid)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }

        public int GetTotalMoneyByOrderId(int Oid)
        {
            string sql = "select sum(o.Count*d.DPrice) from orderdetailinfo o left join dishinfo d on did==dishid where orderid=@id";
            SQLiteParameter param = new SQLiteParameter("@id", Oid);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param));
        }

        public int SetOrderMomey(int orderid, decimal money)
        {
            string sql = "update orderinfo set omoney=@money where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@money", money),
                new SQLiteParameter("@oid", orderid)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(string id)
        {
            string sql = "Delete from OrderDetailInfo where Oid=@id";
            SQLiteParameter param = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }

        public int CancellationOfOrder(string Oid)
        {
            string sql = "delete from OrderDetailInfo where OrderId=@id;"
                + "delete from Orderinfo where Oid=@id;";
            SQLiteParameter param = new SQLiteParameter("@id", Oid);
            SqliteHelper.ExecuteNonQuery("update TableInfo set TIsFree=1 where tid=(select Tableid from  Orderinfo where Oid=@id)", param);
            return SqliteHelper.ExecuteNonQuery(sql, param);
        }
    }
}
