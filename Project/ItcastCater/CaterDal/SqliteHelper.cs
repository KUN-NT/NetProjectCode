using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace CaterDal
{
    public static class SqliteHelper
    {
        //获得连接字符串
        private static string conStr = ConfigurationManager.ConnectionStrings["conter"].ConnectionString;

        #region 返回受影响行数
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                SQLiteCommand com = new SQLiteCommand(sql, con);
                com.Parameters.AddRange(param);
                con.Open();
                return com.ExecuteNonQuery();
            }
        } 
        #endregion

        #region 返回首行首列
        public static object ExecuteScalar(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                SQLiteCommand com = new SQLiteCommand(sql, con);
                com.Parameters.AddRange(param);
                con.Open();
                return com.ExecuteScalar();
            }
        } 
        #endregion

        #region 返回数据表
        public static DataTable GetDataTable(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddRange(param);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        } 
        #endregion
    }
}
