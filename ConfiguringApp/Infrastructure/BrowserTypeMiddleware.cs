using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfiguringApp.Infrastructure
{
    public class BrowserTypeMiddleware
    {
        private RequestDelegate nextDelegate;

        public BrowserTypeMiddleware(RequestDelegate nextDelegate) => this.nextDelegate = nextDelegate;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EDGE Browser"] = httpContext.Request.Headers["User-Agent"]
                                                           .Any(h => h.ToLower().Contains("edge"));
            await nextDelegate.Invoke(httpContext);
        }

    }
}
