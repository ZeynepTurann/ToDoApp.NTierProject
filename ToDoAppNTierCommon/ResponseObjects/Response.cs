﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppNTierCommon.ResponseObjects
{
    public class Response:IResponse
    {
        public Response()
        {

        }
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        public Response(ResponseType responseType,string message)
        {
            Message = message;
            ResponseType = responseType;
        }

        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
        
    }
    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound
    }
}