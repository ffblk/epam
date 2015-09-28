using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class ListMyOrder
    {
        public const string SelectAllForUser = "SELECT order.status, places.series,"
            + " places.place, plays.name, dates.Date, category.price, order.id"
            + " FROM category INNER JOIN (plays INNER JOIN (dates INNER JOIN (Users INNER JOIN"
            + " (places INNER JOIN [order] ON places.id = order.idPlaces) ON Users.id = order.idUser) ON dates.id = order.idDate)"
            + " ON plays.id = dates.idPlays) ON category.id = places.idCategory"
            + " WHERE Users.id = ? AND status > 0"
            + " ORDER BY dates.Date, places.series;";
        const string UpdOrderMy = "UPDATE [order] SET status = 0 WHERE id = ? ";
        public int Id { set; get; }
        public string NamePlays { set; get; }
        public DateTime Date { set; get; }
        public int Price { set; get; }
        public int Status { set; get; }
        public int Row { set; get; }
        public int Seat { set; get; }
        public ListMyOrder() { }
        public ListMyOrder(int id, int status, int row, int seat, int price, DateTime date, string namePlays)
        {
            Id = id;
            NamePlays = namePlays;
            Date = date;
            Price = price;
            Status = status;
            Row = row;
            Seat = seat;
        }
        public static List<ListMyOrder> Show(int id)
        {
            List<ListMyOrder> list = new List<ListMyOrder>();
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(SelectAllForUser, conn);
            cmd.Parameters.AddWithValue("param1", id);
            OleDbDataReader rdr = cmd.ExecuteReader();
            ListMyOrder p;
            while (rdr.Read())
            {
                p = new ListMyOrder(rdr.GetInt32(6), rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2),
                    rdr.GetInt32(5), rdr.GetDateTime(4), rdr.GetString(3));
                list.Add(p);
            }
            return list;
        }
        public static void CancelMyOrder(int ? id)
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