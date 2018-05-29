﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.Utils
{
    public static class ViewHelper
    {
        public static string IsActivePage(string actionName, string controllerName)
        {
            //https://stackoverflow.com/questions/18248547/get-controller-and-action-name-from-within-controller
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string controller = "", action = "";

            if (routeValues.ContainsKey("controller"))
                controller = (string)routeValues["controller"];

            if (routeValues.ContainsKey("action"))
                action = (string)routeValues["action"];

            return controller == controllerName && action == actionName ? "active" : "";
        }
    }
}