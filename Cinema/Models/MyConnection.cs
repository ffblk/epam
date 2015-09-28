using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Cinema.Models
{
    public class MyConnection
    {
        private static OleDbConnection conn;
        public static string siteDir = HttpContext.Current.Server.MapPath(@"\");
        public static OleDbConnection GetConnection()
        {
            if (conn == null)
            {
                string siteDir = HttpContext.Current.Server.MapPath(@"\");
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
       siteDir + @"App_Data\cinema.mdb");
                conn.Open();
            }
            return conn;
        }
    }
}