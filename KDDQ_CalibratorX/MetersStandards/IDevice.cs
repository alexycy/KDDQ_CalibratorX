using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDDQ_CalibratorX.MetersStandards
{
    public interface IDevice
    {
        void ConnectAsync();
        void DisconnectAsync();
        Task<string> ReadAsync(TimeSpan timeout);
       void PreCMD(string keywords, string values);
       string GenerateCommand(string keyword, string value);
    }

}
