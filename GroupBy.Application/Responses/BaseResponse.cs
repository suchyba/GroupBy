using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Succes = true;
        }
        public BaseResponse(string message)
        {
            Succes = true;
            Message = message;
        }
        public BaseResponse(string message, bool succes)
        {
            Succes = succes;
            Message = message;
        }
        public bool Succes { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
