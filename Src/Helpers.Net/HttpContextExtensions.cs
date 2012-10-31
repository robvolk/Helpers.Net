using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web
{
    public static class HttpContextExtensions
    {
        public static void PermanentRedirect(this HttpResponse response, string url, bool endResponse = true)
        {
            response.StatusCode = 301;
            response.Redirect(url, endResponse);
        }

        /// <summary>
        /// Gets the request URL, but uses the HOST server variable to account for the port 20000 issue with Azure
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string PublicUrl(this HttpRequest request)
        {
            return request.Domain() + request.Url.PathAndQuery;
        }

        /// <summary>
        /// Gets the actual requested domain from the HTTP_HOST header instead of the port 20000 that Azure uses internally
        /// </summary>
        public static string Domain(this HttpRequest request)
        {
            return request.Url.Scheme + "://" + request.Headers["HOST"];
        }
    }
}
