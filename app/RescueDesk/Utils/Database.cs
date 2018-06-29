using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public static class Utils
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}