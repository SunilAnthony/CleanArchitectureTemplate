using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public static class Response
    {
        public static Response<T> Fail<T>(string message, HttpStatusCode httpStatusCode, T data = default) =>
            new Response<T>(data, httpStatusCode, message, true);

        public static Response<T> Ok<T>(T data, HttpStatusCode httpStatusCode, string message ) =>
            new Response<T>(data, httpStatusCode, message, false);
    }
    public class Response<T>
    {
        public Response(T data, HttpStatusCode httpStatusCode, string msg, bool error)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            Message = msg;
            Error = error;
        }
        public T Data { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
