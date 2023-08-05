using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTierCommon.ResponseObjects
{
    public class Response<T> : Response, IResponse<T>
    {
        public T Data { get ; set ; }
        
        public List<CustomValidationError> ValidationErrors { get ; set; }
        public Response(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }

        public Response(ResponseType responseType,T data,string message):base(responseType,message)
        {
            Data=data;
        }
        public Response(ResponseType responseType, string message) : base(responseType, message)
        {

        }

        public Response(T data, List<CustomValidationError> validationErrors)
        {
            Data = data;
            ValidationErrors = validationErrors;
        }

        public Response(ResponseType responseType, T data, List<CustomValidationError> errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }
    }
}
