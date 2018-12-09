using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApi.Utils
{
    public class CUtil
    {
        public static string GetValueBaseOnKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}