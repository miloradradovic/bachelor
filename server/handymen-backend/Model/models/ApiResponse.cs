using System;

namespace Model.models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public Object ResponseObject { get; set; }
        public int Status { get; set; }
    }
}