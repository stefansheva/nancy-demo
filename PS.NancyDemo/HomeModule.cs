using System;
using System.Linq;
using Nancy;
using Nancy.Responses;

namespace PS.NancyDemo
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Func<Request, bool> _isNotApiClient = req => !req.Headers.UserAgent.ToLower().StartsWith("curl");
            
            Get["/", ctx => _isNotApiClient.Invoke(ctx.Request)] = p => View["index.html"];

            Get["/"] = p => "CURL func invoked";

            Get["/courses"] = p => new JsonResponse(Course.List, new DefaultJsonSerializer());
            Get["/courses/{id}"] = p => Response.AsJson(Course.List.SingleOrDefault(x => x.Id == p.id));
        }
    }
}
