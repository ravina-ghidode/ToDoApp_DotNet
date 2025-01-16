using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool IsSuccessful { get; set; }

        public ServiceResponse<T> GetResponse(T data, string message, bool isSuccess = false) 
        {
            Data = data;
            Message = message;  
            IsSuccessful = isSuccess;
            return this;
        }
    }
}
