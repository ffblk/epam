using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class AllOrders : ListMyOrder
    {
        public const string SelectAllOrders = "SELECT Users.name, plays.name, dates.Date, order.id, category.price, Users.mail, Users.phone, places.series, places.place"
            + " FROM category INNER JOIN (plays INNER JOIN (dates INNER JOIN (Users INNER JOIN (places INNER JOIN [order] ON places.id = order.idPlaces) ON Users.id = order.idUser) ON dates.id = order.idDate) ON plays.id = dates.idPlays) ON category.id = places.idCategory"
            + " WHERE (((order.status)=1))"
            + " ORDER BY Users.name, dates.Date;";
        const string UpdOrderMy = "UPDATE [order] SET status = 2 WHERE id = ? ";
        public string NameUser { set; get; }     
        public string Mail { set; get; }
        public string Phone { set; get; }       
        public AllOrders()
        {

        }
        public AllOrders(int id, int price, DateTime date, string nameUser, string namePlays, string mail, string phone, int row, int seat, int status)
        {
            Id = id;
            Price = price;
            Date = date;
            NameUser = nameUser;
            NamePlays = namePlays;
            Mail = mail;
            Phone = phone;
            Row = row;
            Seat = seat;
            Status = status;
        }
        public static List<AllOrders> Show()
        {
            List<AllOrders> list = new List<AllOrders>();
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(SelectAllOrders, conn);
            OleDbDataReader rdr = cmd.ExecuteReader();
            AllOrders p;
            while (rdr.Read())
            {
                p = new AllOrders(rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetDateTime(2), rdr.GetString(0), rdr.GetString(1), rdr.GetString(5), rdr.GetString(6), rdr.GetInt32(7), rdr.GetInt32(8), 1);
                list.Add(p);
            }
            return list;
        }
        public static void CancelOrder(int ? id)
        {
            if (id != null)
            {
                OleDbConnection conn = MyConnection.GetConnection();
                OleDbCommand cmd = new OleDbCommand(UpdOrderMy, conn);
                cmd.Parameters.AddWithValue("param1", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}