using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Xml;
using Cinema;
namespace Cinema.Models
{  
    public class Hall
    {
        public const string SelectAllOrder = "SELECT places.series, places.place, order.status"
            +" FROM dates INNER JOIN (places INNER JOIN [order] ON places.id = order.idPlaces) ON dates.id = order.idDate"
            +" WHERE dates.date=@date";
        string siteDir = HttpContext.Current.Server.MapPath(@"\");
        const string InputFile = @"App_Data\Hall.xml";   
        public static List<List<Places>> placeList;        
        public Hall() 
        {
            placeList = new List<List<Places>>();            
        }
        public void Add(Places p)//??
        {
            if (p.Row > placeList.Count)
            {
                placeList.Capacity = p.Row;
                placeList.AddRange(Enumerable.Repeat(new List<Places>(), placeList.Capacity - placeList.Count));
            }            
            if (p.Place > placeList[p.Row - 1].Count)
            {
                placeList[p.Row - 1].Capacity = p.Place;
                placeList[p.Row - 1].AddRange(Enumerable.Repeat(new Places(), placeList[p.Row - 1].Capacity - placeList[p.Row - 1].Count));
            }
            placeList[p.Row - 1][p.Place - 1] = p;
            
        }
        public static void ListPlaces()        
        {
            string siteDir = HttpContext.Current.Server.MapPath(@"\");
            XmlTextReader reader = new XmlTextReader(siteDir + InputFile);
            Hall pList = new Hall();
            Places p = null;
            int category = 0;
            int series = 0;
            int place = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {                  
                    switch (reader.Name)
                    {
                        case "category":
                            reader.MoveToNextAttribute();
                            category = Int32.Parse(reader.Value);
                            break;
                        case "row":
                            reader.MoveToNextAttribute();
                            series = Int32.Parse(reader.Value);
                            break;
                        case "seat":
                            reader.Read();
                            place = Int32.Parse(reader.Value);
                            p = new Places(series, place, category);
                            pList.Add(p);
                            break;
                    }                    
                }
            }
            
        }
        private List<List<Places>> GetList()
        {
            return placeList;
        }
        public static List<List<Places>> ListOrders(DateTime Date)
        {
            List<List<Places>> pList = new List<List<Places>>();
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, placeList);
            stream.Position = 0;
            pList = (List<List<Places>>)formatter.Deserialize(stream);
            OleDbConnection conn = MyConnection.GetConnection();
            OleDbCommand cmd = new OleDbCommand(SelectAllOrder, conn);            
            cmd.Parameters.AddWithValue("@date", Date.ToString("M/d/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
            OleDbDataReader rdr = cmd.ExecuteReader();
            Places p = null;
            while (rdr.Read())
            {
                p = new Places(rdr.GetInt32(0), rdr.GetInt32(1), 0, rdr.GetInt32(2));
                if(p.Row < pList.Count && p.Place < pList[p.Row-1].Count && pList[p.Row-1][p.Place-1].Category != -1)
                    pList[p.Row-1][p.Place-1] = p;
            }
            return pList;
        }
        
    }
}