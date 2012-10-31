using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HelperMethods
{
    /// <summary>
    /// Same as WebClient, but extended to expose the final ResponseUri after following redirects.
    /// Ref: http://stackoverflow.com/questions/690587/using-webclient-in-c-is-there-a-way-to-get-the-url-of-a-site-after-being-redirec
    /// </summary>
    public class WebClientEx : WebClient
    {
        Uri responseUri;

        public Uri ResponseUri
        {
            get { return responseUri; }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            responseUri = response.ResponseUri;
            return response;
        }
    }
}
