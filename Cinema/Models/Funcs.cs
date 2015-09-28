using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Funcs
    {
        string siteDir = HttpContext.Current.Server.MapPath(@"\");
        public const string SelectAllOrder = "SELECT places.series, places.place, order.status"
            + " FROM dates INNER JOIN (places INNER JOIN [order] ON places.id = order.idPlaces) ON dates.id = order.idDate"
            + " WHERE dates.id=?";
        public const string CheckOrder = "SELECT order.id"
            + " FROM places INNER JOIN [order] ON places.id = order.idPlaces"
            + " WHERE (((places.place)= ? ) AND ((places.series)= ?) AND ((order.idDate)= ? ) AND ((order.status)>0))";
        public const string CheckPlaces = "SELECT places.id"
            +" FROM places"
            + " WHERE (places.place= ?) AND (places.series = ?) AND (places.idCategory= ?)";
        public const string AddPlace = "INSERT INTO places ( idCategory, place, series )"
            +" VALUES (?, ?, ?)";
        public const string GetIdUser = "SELECT id FROM Users WHERE name= ? ";
        public const string AddOrder = "INSERT INTO [order] (idDate, status, idPlaces, idUser)  VALUES (?, 1, ?, ?)";
        public const string GetPrice = "SELECT category.id, category.price"
            + " FROM category";
        public static Dictionary<int, int> IdPrice;
        public static void GetPrices()
        {
            IdPrice = new Dictionary<int, int>();
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(GetPrice, conn);
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                IdPrice.Add(rdr.GetInt32(0), rdr.GetInt32(1));
            }            
        }
        public static int[][] RequestConfirm(int id, string order, out int Price)
        {
            int[][] abc = null;
            Price = 0;
            if (order != null)
            {
                string input = string.Join(";", order.ToLower().Split(';').Distinct());
                string[] separators = { ";", "," };
                string[] words;
                
                words = input.Split(separators, StringSplitOptions.None);
                int length = words.Count() / 2;
                abc = new int[length][]; //[row][place]
                int t1, t2;
                for (int i = 0; i < words.Count() - 1; i++)
                {
                    abc[i / 2] = new int[2];
                    t1 = abc[i / 2][0] = Int32.Parse(words[i]);
                    t2 = abc[i / 2][1] = Int32.Parse(words[++i]);
                    Price += IdPrice[Hall.placeList[t1 - 1][t2 - 1].Category];
                }
            }
            return abc;
        }
        public static int[][] InsertOrders(int id, string order, int idUser)
        {
            string input = string.Join(";", order.ToLower().Split(';').Distinct()); 
            string[] separators = { ";", "," };
            string[] words;
            words = input.Split(separators, StringSplitOptions.None);
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(CheckOrder, conn);
            OleDbDataReader rdr;
            int length = words.Count()/2;
            int[][] abc= new int[length][]; //[row][place]
            bool checkOrderInt = false;
            
            cmd.Parameters.Add("@param1", OleDbType.Integer);
            cmd.Parameters.Add("@param2", OleDbType.Integer);
            cmd.Parameters.Add("@param3", OleDbType.Integer);
           
            for (int i = 0; i < words.Count()-1; i++)
            {
                abc[i/2] = new int[2];
                abc[i/2][0]=Int32.Parse(words[i]);
                abc[i/2][1]=Int32.Parse(words[++i]);
                cmd.Parameters[0].Value = abc[i / 2][1];
                cmd.Parameters[1].Value = abc[i / 2][0];
                cmd.Parameters[2].Value = id;
                rdr = cmd.ExecuteReader();
                checkOrderInt = checkOrderInt|rdr.HasRows;
                rdr.Close();
            }
            cmd.CommandText = CheckPlaces;           
            
            if (!checkOrderInt)
            {
                
                foreach (int[] e in abc)
                {
                    cmd.Parameters[0].Value = e[1];
                    cmd.Parameters[1].Value = e[0];                   
                    cmd.Parameters[2].Value = Hall.placeList[e[0] - 1][e[1] - 1].Category;
                    rdr = cmd.ExecuteReader();
                    
                    if (!rdr.HasRows)
                    {
                        rdr.Close();
                        cmd.CommandText = AddPlace;
                        cmd.Parameters[0].Value = Hall.placeList[e[0] - 1][e[1] - 1].Category;
                        cmd.Parameters[1].Value = e[1];
                        cmd.Parameters[2].Value = e[0];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = CheckPlaces;  
                    }
                    else { rdr.Close(); }
                }
                int[] idPlaces = new int[abc.Count()];
                int ind = 0;
                cmd.CommandText = CheckPlaces;
               
                foreach(int[] e in abc)
                {
                    cmd.Parameters[0].Value = e[1];
                    cmd.Parameters[1].Value = e[0];
                    cmd.Parameters[2].Value = Hall.placeList[e[0] - 1][e[1] - 1].Category;
                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                    idPlaces[ind] = rdr.GetInt32(0);
                    ind++;
                    rdr.Close();
                }
                             
                cmd.CommandText = AddOrder;
                foreach (int e in idPlaces)
                {
                    cmd.Parameters[0].Value = id;
                    cmd.Parameters[1].Value = e;
                    cmd.Parameters[2].Value = idUser;
                    cmd.ExecuteNonQuery();
                }
                return abc;
            }
            else            
                return null;
        }
    }
}