using System;
using System.Net;

namespace System.Net
{
    public class WebHelper
    {
        public static string LoadUrl(string url)
        {
            using (var web = new WebClient())
            {
                try
                {
                    return web.DownloadString(url);
                }
                catch (WebException ex)
                {
                    ex.Data.Add("Url", url);
                    throw;
                }
            }
        }
    }
}
