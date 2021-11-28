using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCommands
{
    class StringToCommand
    {
        //private const string CURRENTDATE = "@@CURRENTDATE@@";
        //private const string CURRENTTIME = "@@CURRENTTIME@@";
        private const string CURRENTDATETIME = "CURRENTDATETIME";
        private const string ADDHOUR = "ADDHOUR";
        private const string ADDDAY = "ADDDAY";
        private const string DELIMITER = "@@";

        private readonly string C1 = $"{DELIMITER}CURRENTDATE{DELIMITER}";
        private readonly string C2 = $"{DELIMITER}CURRENTDATE{DELIMITER}{DELIMITER}{ADDDAY}-5{DELIMITER}";
        private readonly string C3 = $"{DELIMITER}CURRENTDATE{DELIMITER}{DELIMITER}{ADDHOUR}5{DELIMITER}";
        private readonly string C4 = $"{DELIMITER}CURRENTDATE{DELIMITER}{DELIMITER}{ADDHOUR}-12{DELIMITER}{DELIMITER}{ADDDAY}7{DELIMITER}";
        public void Run()
        {
            var reportParams = new Dictionary<string, string>();
            reportParams.Add("paramA", "randomShit");
            reportParams.Add("paramB", C1);
            reportParams.Add("paramC", C2);
            reportParams.Add("paramD", C3);
            reportParams.Add("paramE", C4);

            foreach (var key in reportParams.Keys.ToList())
            {
                reportParams[key] = this.ApplyCommand(reportParams[key]);
            }

            if (DateTime.TryParse(reportParams["paramE"], out DateTime result))
            {
                Console.WriteLine(result);
            }
        }

        private string ApplyCommand(string command) 
        {
            if (!command.Contains(C1))
            {
                return command;
            }

            var currentDateTime = DateTime.Now;

            if (command.Contains(ADDDAY))
            {
                this.AdjustDateTime(command, ADDDAY, ref currentDateTime);
            }

            if (command.Contains(ADDHOUR))
            {
                this.AdjustDateTime(command, ADDHOUR, ref currentDateTime);
            }

            return currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void AdjustDateTime(string command, string flag, ref DateTime datetime)
        {
            var flagStart = command.Substring(command.IndexOf(flag)).Replace(flag, string.Empty);
            var numberString = flagStart.Substring(0, flagStart.IndexOf(DELIMITER));
            if (int.TryParse(numberString, out int number))
            {
                if (flag == ADDDAY)
                {
                    datetime = datetime.AddDays(number);
                }
                else if (true)
                {
                    datetime = datetime.AddHours(number);
                }
            }
        }
    }
}
