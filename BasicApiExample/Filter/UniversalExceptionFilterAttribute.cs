using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BasicApiExample.Filter
{
    public class UniversalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UniversalExceptionFilterAttribute));
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                log.Error(context.Exception.Message, context.Exception);
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}