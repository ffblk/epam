using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Cinema.Models
{
    public class Users
    {
        public const string Query = "SELECT roleId, id FROM Users WHERE name=@uName AND pass=@uPass";
        const string AddQuery = "INSERT INTO Users ([name],pass,roleId,mail,phone) VALUES (@name,@pass,@isadmin,@mail,@phone)";
        
        public static bool IsLogin(string uname,string upass, out int idUser, out int role)
        {
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(Query,conn);
            cmd.Parameters.AddWithValue("uName", uname);
            cmd.Parameters.AddWithValue("uPass", upass);
            OleDbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                 rdr.Read();
                 role = rdr.GetInt32(0);
                 idUser = rdr.GetInt32(1);
                 return true;
            }
            else 
            {
                idUser = -1;
                role = 0; 
            }
            return false;
        }
        public static void AddUser(User p)
        {
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(AddQuery, conn);
            cmd.Parameters.AddWithValue("@name", p.name);
            cmd.Parameters.AddWithValue("@pass", p.pass);
            cmd.Parameters.AddWithValue("@isadmin", 0);
            cmd.Parameters.AddWithValue("@mail", p.mail);
            cmd.Parameters.AddWithValue("@phone", p.phone);
            cmd.ExecuteNonQuery();
        }
    }
}