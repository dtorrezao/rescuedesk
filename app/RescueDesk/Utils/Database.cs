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
    }
}