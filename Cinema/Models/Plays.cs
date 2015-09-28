using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Globalization;

namespace Cinema.Models
{
    public class Plays
    {
        const string AddQuery = "INSERT INTO Purchases ([Name],Price,[Count]) VALUES (@name,@price,@count)";
        private static List<Play> pList = new List<Play>();
        const string SelectAll = "SELECT plays.id, plays.name, genres.name, authors.name, dates.date "
            +"FROM plays, genres, authors, dates, genresPlays, authorPlays "
            +"WHERE plays.id=authorPlays.idPlays "
            +"AND authorPlays.idAuthor=authors.id "
            +"AND plays.id = genresPlays.idPlays "
            +"AND genresPlays.idGenres = genres.id "
            +"AND plays.id = dates.idPlays "
            +"ORDER BY plays.id ";
        const string GetIdDate = "SELECT id FROM dates WHERE date = @date";
        const string SelectOneName = "SELECT name FROM plays WHERE id = @id";
        const string SelectOneAutors = "SELECT authors.name FROM authors, authorPlays WHERE authorPlays.idPlays = @id AND authorPlays.idAuthor = authors.id";
        const string SelectOneGenres = "SELECT genres.name FROM genres, genresPlays WHERE genresPlays.idPlays = @id AND genresPlays.idGenres = genres.id";
        const string DelQuery = "DELETE * FROM Purchases WHERE id= @id";
        const string UpdQuery = "UPDATE Purchases SET [Name]=@name, Price=@price, [Count]=@count WHERE ID=@id";
        public static List<Play> PlList(int? id = null)
        {
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(SelectAll, conn);
            OleDbDataReader rdr = cmd.ExecuteReader();
            pList.Clear();
            Play p = null;
            selPur = null;
            int idPlays = -1;
            int idP=0;
            while (rdr.Read())
            {
                idP = rdr.GetInt32(0);
                if (idP != idPlays)
                {
                    if (p != null)
                        pList.Add(p);
                    idPlays = idP;
                    p = new Play(idPlays, rdr.GetString(1));
                    if (p.ID == id) selPur = p;
                }
                p.Addl(rdr.GetString(2),rdr.GetString(3),rdr.GetDateTime(4));
            }
            if (p != null)
                pList.Add(p);
            return pList;
        }
        public static Play SelectPlay(int id)
        {
            Play p = null;
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(SelectOneName, conn);
            cmd.Parameters.AddWithValue("@id", id);
            OleDbDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {                
                string name = rdr.GetString(0);
                HashSet<string> genres = new HashSet<string>();
                HashSet<string> authors = new HashSet<string>();
                cmd.CommandText = SelectOneAutors;
                cmd.Parameters.AddWithValue("@id", id);
                rdr.Close();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    authors.Add(rdr.GetString(0));
                }
                cmd.CommandText = SelectOneGenres;
                cmd.Parameters.AddWithValue("@id", id);
                rdr.Close();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    genres.Add(rdr.GetString(0));
                }
                p = new Play(id, name, genres, authors, null);
            }        
            return p;
        }
        private static Play selPur = null;

        public static Play SelPur() { return selPur; }
        public static int DateId(DateTime date)
        {
            int id = -1;
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(GetIdDate, conn);
            cmd.Parameters.AddWithValue("@date", date.ToString("M/d/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
            OleDbDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
                id = rdr.GetInt32(0);
            return id;
        }
    }

}