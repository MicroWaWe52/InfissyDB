using Infissy.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infissy
{
    public static class ExMethods
    {
        public static string MazzoToString(this List<Carta> mazzo)
        {
            var mazzoString = "";
            foreach (var carta in mazzo)
            {
                mazzoString += carta.ToString()+"<br>";
            }
            return mazzoString;
        }
    }
}