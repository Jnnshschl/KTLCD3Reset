using System;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace KTLCD3Reset
{
    class Program
    {
        private static string ComPortName { get; set; } = "COM8";

        static void Main(string[] args)
        {
            Console.WriteLine(">> KT-LCD3 Reset Tool");

            if (args.Length > 0)
            {
                ComPortName = args[0];
            }
            else
            {
                Console.WriteLine($">> No COM port specified, using default: \"{ComPortName}\"");
                Console.WriteLine($">> To specify a COM port run: \"KT3LCDReset.exe COM2\"");
            }

            string[] serialPorts = SerialPort.GetPortNames();

            if (!serialPorts.Contains(ComPortName))
            {
                Console.WriteLine($">> Unknown Serial Port: \"{ComPortName}\"");
                Console.WriteLine($">> Avialable Ports: {string.Join(", ", serialPorts)}");
                return;
            }

            SerialPort serialPort = new SerialPort(ComPortName, 9600, Parity.None, 8, StopBits.One);

            try
            {
                serialPort.Open();
            }
            catch
            {
                Console.WriteLine($">> Failed to open \"{ComPortName}\", make sure no other program is using it...");
                return;
            }

            Console.WriteLine($">> Opened Serial Port: \"{ComPortName}\"");

            int count = 0;
            int maxCount = 32;

            // 255 (reset instruction?), diameter, speed, pas level, P1, P2, P3, P4, P5, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C12, C13, C14, checksum
            byte[] buffer = new byte[] { 0xFF, 0x1C, 0x19, 0x00, 0xA0, 0x00, 0x00, 0x01, 0x0B, 0x02, 0x00, 0x00, 0x00, 0x0A, 0x05, 0x00, 0x00, 0x00, 0x14, 0x04, 0x05, 0x02, 0x11 };

            while (count < maxCount)
            {
                ++count;
                Console.WriteLine($">> [{count}/{maxCount}] Emulating KT-LCD3 copy...");

                for (int i = 0; i < buffer.Length; ++i)
                {
                    serialPort.Write(buffer, 0, buffer.Length);
                }

                Task.Delay(100).Wait();
            }

            Console.WriteLine($">> Device should be reset, if not try it again...");

            serialPort.Close();
            Console.WriteLine($">> Closed Serial Port: \"{ComPortName}\"");
        }
    }
}
