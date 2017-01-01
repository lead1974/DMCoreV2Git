using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCoreV2.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
