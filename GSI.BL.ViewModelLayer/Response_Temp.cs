using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer
{
    
        public class Response<T> : Response
        {
            public T Body { get; set; }
            public Response()
                : base()
            {
            }



        }


    public class Response
    {
        public MessageCode[] Messages { get; set; }
        public bool Result { get; set; }

        public Response()
        {
            Result = true;
        }
    }


    public class MessageCode
    {
        #region properties

        public int Code { get; set; }
        public string Message { get; set; }

        #endregion

        #region ctor

        public MessageCode(int code, string message = null, params object[] args)
        {
            Code = code;
            Message = String.IsNullOrEmpty(message) ? null : String.Format(message, args);
        }

        public MessageCode(string message = null, params object[] args)
            : this(0, message, args)
        {

        }

        #endregion
    }

}
