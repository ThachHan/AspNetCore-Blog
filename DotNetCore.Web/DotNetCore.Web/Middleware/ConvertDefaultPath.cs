using DotNetCore.Service;
using DotNetCore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Web.Middleware
{
    public class ConvertDefaultPath
    {
        private RequestDelegate nextMiddleware;
        public IServiceProvider _services { get; }
        public ConvertDefaultPath(RequestDelegate next, IServiceProvider services)
        {
            this.nextMiddleware = next;
            _services = services;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _services.CreateScope())
            {
                var routingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRoutingService>();

                context.Request.Path = routingService.GetRoutingByUrl(context.Request.Path);
            }
            
            await this.nextMiddleware.Invoke(context);
        }
    }
}
