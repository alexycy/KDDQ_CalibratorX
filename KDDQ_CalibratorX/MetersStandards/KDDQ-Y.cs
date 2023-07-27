using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDDQ_CalibratorX.MetersStandards
{
    public class KDDQ_Y:IDevice
    {
        // 其他代码...

        private SerialPort _serialPort;
        private TaskCompletionSource<string> _tcs;
        private CancellationTokenSource _cts;

        public KDDQ_Y(string portName)
        {
            _serialPort = new SerialPort(portName);
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string ret = _serialPort.ReadExisting();

            var data = ParseCommand(ret);


            _tcs?.TrySetResult(data);
        }

        public async Task WriteAsync(string data)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                await _serialPort.BaseStream.WriteAsync(Encoding.ASCII.GetBytes(data), 0, data.Length);
            }
            else
            {
                throw new InvalidOperationException("Serial port is not open");
            }
        }


        public async Task<string> ReadAsync(TimeSpan timeout)
        {
            _tcs = new TaskCompletionSource<string>();
            _cts = new CancellationTokenSource(timeout);

            _cts.Token.Register(() => _tcs.TrySetResult(null));

            return await _tcs.Task;
        }
        /// <summary>
        /// 解析接收到的指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string ParseCommand(string command)
        {
            // 在这里添加解析指令的逻辑
            // 这部分需要根据具体的设备协议来实现
            return command;
        }

        /// <summary>
        /// 返回分割并组包后的指令
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<string> SplitAndPackageCommand(string keywords, string values)
        {
            List<string> ret = new List<string>();

            if (!keywords.Contains(";") && !values.Contains(";"))
            {
                // 如果没有分号，直接使用GenerateCommand生成指令并返回
                string command = GenerateCommand(keywords, values);
                ret.Add(command);
            }
            else
            {
                var keywordList = keywords.Split(';');
                var valueList = values.Split(';');

                for (int i = 0; i < keywordList.Length; i++)
                {
                    string command = GenerateCommand(keywordList[i], valueList[i]);
                    ret.Add(command);
                }
            }

            return ret;
        }

        /// <summary>
        /// 根据关键字生成指令的逻辑
        /// 这部分需要根据具体的设备协议来实现
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GenerateCommand(string keyword, string value)
        {
            string ret="";
            switch (keyword)
            {
                case "RANGE":
                    ret = $"KDST,[RANGE,{value}]\r\n";
                    break;
                case "WAVE":
                    ret = $"KDST,[WAVE,{value}]\r\n";
                    break;
                case "READ":
                    ret = $"KDST,[READ,{value}]\r\n";
                    break;
                default:
                    break;
            }
            return ret;
        }

        public void ConnectAsync(string portName)
        {
            // 读取INI文件
            var settings = new NameValueCollection();
            var lines = File.ReadAllLines("settings.ini");
            string currentSection = "";
            foreach (var line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // 这是一个新的段，更新当前段的名称
                    currentSection = line.Trim('[', ']');
                }
                else
                {
                    var parts = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 2)
                    {
                        var key = currentSection + "." + parts[0].Trim();
                        var value = parts[1].Trim();
                        settings.Add(key, value);
                    }
                }
            }

            // 从INI文件中获取设置
            int baudRate = int.Parse(settings[portName + ".baudRate"]);
            Parity parity = (Parity)Enum.Parse(typeof(Parity), settings[portName + ".parity"]);
            int dataBits = int.Parse(settings[portName + ".dataBits"]);
            StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), settings[portName + ".stopBits"]);

            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            _serialPort.Open();
        }

        public void DisconnectAsync()
        {
            _serialPort.Close();
        }
    }
}
