using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public void SendEmail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Send Mail: To: {to} From: {from} Subject: {subject} Body: {body}");
        }
    }
}
