using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    [Serializable]
    public class Places : ICloneable
    {
        public int Category { private set; get; }
        public int Row { private set; get; }
        public int Place { /*private */set; get; }
        public int Status { private set; get; }
        public int CompareTo(Places p)
        {
            if (Row == p.Row)
                return Place - p.Place;
            else
                return Row - p.Row;
        }
        public void AddStatus(int i)
        {
            Status = i;
        }
        public Places()
        {
            Row = -1;
            Place = -1;
            Category = -1;
        }
        public Places(int series, int place, int category, int status = 0)
        {
            Category = category;
            Row = series;
            Place = place;
            Status = status;
        }

        public object Clone()
        {
            Places p = new Places(this.Row, this.Place, this.Category, this.Status);
            return p;
        }
    }
}