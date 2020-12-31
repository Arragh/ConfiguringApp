using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguringApp.Infrastructure
{
    public class ShortCircuitMiddleware
    {
        private RequestDelegate nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate nextDelegate) => this.nextDelegate = nextDelegate;

        public async Task Invoke(HttpContext httpContext)
        {
            //if (httpContext.Request.Headers["User-Agent"].Any(h => h.ToLower().Contains("edge")))
            //{
            //    //httpContext.Response.StatusCode = 403;
            //    await httpContext.Response.WriteAsync("Tvoi brauzer govno");
            //}
            if (httpContext.Items["EDGE Browser"] as bool? == true)
            {
                //httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Tvoi brauzer govno");
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }

    }
}
