using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDDQ_CalibratorX.MetersStandards
{
    public interface IDevice
    {
        void ConnectAsync(string portname);
        void DisconnectAsync();
        Task WriteAsync(string data);
        Task<string> ReadAsync(TimeSpan timeout);
        string ParseCommand(string command);
        List<string> SplitAndPackageCommand(string keywords, string values);
        string GenerateCommand(string keyword, string value);
    }

}
