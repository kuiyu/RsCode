using System;
namespace RsCode.AspNetCore
{
    public  class AppException:Exception
    {
        public AppException(string msg)
        {
            Message = msg;
        }
        public AppException(int status, string msg)
        {
            Status = status;
            Message = msg;
        }
        public int Status { get; set; } = 500;

        public string  Message { get; set; }
    }
}
