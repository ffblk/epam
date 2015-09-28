using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Cinema.Models
{
    public class Detail
    {       
        string siteDir = HttpContext.Current.Server.MapPath(@"\");
        const string InputFile = @"App_Data\repertory.xml";
        public int Id { private set; get; }
        public string Name { private set; get; }
        public string Description { private set; get; }
        public string LinkPic { private set; get; }
        public Detail()
        { }
        public static List<Detail> Plist()
        {
            string siteDir = HttpContext.Current.Server.MapPath(@"\");
            XmlTextReader reader = new XmlTextReader(siteDir + InputFile);
            List<Detail> pList = new List<Detail>();
            //
            pList.Clear();
            Detail p = null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    p = new Detail();
                    switch (reader.Name)
                    {
                        case "id":
                            reader.Read();
                            p.Id = Int32.Parse(reader.Value);
                            break;
                        case "name":
                            reader.Read();
                            p.Name = reader.Value;
                            break;
                        case "description":
                            reader.Read();
                            p.Description = reader.Value;
                            break;
                        case "link":
                            reader.Read();
                            p.LinkPic = reader.Value;
                            break;
                    }
                    pList.Add(p);
                }
            }
            return pList;
        }
        public static Detail FindId(int id)
        {
            string siteDir = HttpContext.Current.Server.MapPath(@"\");
            XmlTextReader reader = new XmlTextReader(siteDir + InputFile);
            Detail p = new Detail();
            int t = 0;
            string t1 = "";
            string t2 = "";
            string t3 = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "id":
                            reader.Read();
                            t = Int32.Parse(reader.Value);
                            break;
                        case "name":
                            reader.Read();
                            t1 = reader.Value;
                            break;
                        case "description":
                            reader.Read();
                            t2 = reader.Value;
                            break;
                        case "link":
                            reader.Read();
                            t3 = reader.Value;
                            break;
                    }
                    if (t == id)
                    {
                        p.Id = t;
                        p.Name = t1;
                        p.Description = t2;
                        p.LinkPic = t3;
                    }
                }
            }
            return p;
        }
    }
}