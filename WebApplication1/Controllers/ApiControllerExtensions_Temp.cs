using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Http;

namespace Galcon.GSI.Systems.GSIGroup.WebServices.Controllers
{
    public static class ApiControllerExtensions
    {
        public static Response<T> ConvertException<T>(Exception e)
        {
            return new Response<T>()
            {
                Messages = ConvertException(e).Messages,
                Result = false
            };
        }
        public static Response ConvertException(Exception e)
        {
            if (e is System.Data.SqlClient.SqlException)
            {
                var eSQL = e as System.Data.SqlClient.SqlException;
                return new Response()
                {
                    Messages = new MessageCode[] { new MessageCode(eSQL.Number, e.Message) }
                };
            }
            else
            {
                return new Response()
                {
                    Messages = new MessageCode[] { new MessageCode(e.Message) },
                    Result = false
                };
            }
        }
        public static Response<T> HandleResponse<T>(this ApiController controller, Func<T> fun, Func<bool> TestedCondition = null)
        {
            try
            {
                var response = fun();

                if (typeof(T).IsClass && response == null)
                {
                    return new Response<T>()
                    {
                        Result = false
                    };
                }

                if (TestedCondition != null && !TestedCondition())
                {
                    return new Response<T>()
                    {
                        Result = false
                    };
                }

                return new Response<T>()
                {
                    Result = true,
                    Body = response
                };
            }
            catch (SecurityException e)
            {
                throw ThrowHttpResponseException(controller, new MessageCode[] { new MessageCode(e.Source, e.Message) }, HttpStatusCode.Forbidden);
            }
            catch (Exception e)
            {
                throw ThrowHttpResponseException(controller, ConvertException<T>(e), code: HttpStatusCode.InternalServerError);
            }
        }

        public static Exception ThrowHttpResponseException<T>(this ApiController controller, T content, MessageCode[] messages = null, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            var response = new Response<T>()
            {
                Body = content,
                Messages = messages,
                Result = false
            };

            return new HttpResponseException(new HttpResponseMessage(code)
            {
                Content = new ObjectContent<Response<T>>(response, controller.RequestContext.Configuration.Formatters.JsonFormatter)
            });
        }

        public static Exception ThrowHttpResponseException(this ApiController controller, MessageCode[] messages = null, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            var response = new Response()
            {
                Messages = messages,
                Result = false
            };

            return new HttpResponseException(new HttpResponseMessage(code)
            {
                Content = new ObjectContent<Response>(response, controller.RequestContext.Configuration.Formatters.JsonFormatter)
            });
        }
    }
}