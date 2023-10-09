using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{


    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ServiceResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
    public class ServiceResponse <T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }


        public ServiceResponse(T data, bool success, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public ServiceResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
