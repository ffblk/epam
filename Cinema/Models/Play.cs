using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Play
    {
        public int ID { private set; get; }
        public string Name { private set; get; }
        public HashSet<string> Genres { private set; get; }
        public HashSet<string> Authors { private set; get; }
        public HashSet<DateTime> Dates { private set; get; }      
        public Play(int id, string name, HashSet<string> genres, HashSet<string> authors, HashSet<DateTime> dates)
        {
            ID = id;
            Name = name;
            Genres = genres;
            Authors = authors;
            Dates = dates;
        }
        public Play(int id, string name)
        {
            ID = id;
            Name = name;
            Genres = new HashSet<string>();
            Authors = new HashSet<string>();
            Dates = new HashSet<DateTime>();
        }
        public Play()
        { }
        public void Addl(string genres, string authors, DateTime dates)
        {
            Genres.Add(genres);
            Authors.Add(authors);
            Dates.Add(dates);
        }
        public override string ToString()
        {
            return "";
        }
    }
}