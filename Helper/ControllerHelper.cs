using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Volare.Helper
{
    public static class ControllerHelper
    {
        public static bool IsUserOn(ISession session)
        {
            string logged = session.GetString("Logged");
            return logged != null;
        }
        public static string GetUsername(ISession session)
        {
            string username = session.GetString("Username");
            return username;
        }
    }
}