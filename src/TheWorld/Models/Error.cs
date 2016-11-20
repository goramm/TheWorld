using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Error
    {
        public Error(string message, int status = 0)
        {
            this.Message = message;
            this.Status = status;
        }

        public string Message { get; set; }
        public int Status { get; set; }
    }
}
