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
        private const string DELIMITER = ";";

        private readonly string C1 = $"{CURRENTDATETIME}{DELIMITER}";
        private readonly string C2 = $"{CURRENTDATETIME}{DELIMITER}{ADDDAY}-5{DELIMITER}";
        private readonly string C3 = $"{CURRENTDATETIME}{DELIMITER}{ADDHOUR}5{DELIMITER}";
        private readonly string C4 = $"{CURRENTDATETIME}{DELIMITER}{ADDHOUR}-12{DELIMITER}{ADDDAY}7{DELIMITER}";
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

            DateTime currentDateTime = this.AdjustDateTime(command);

            return currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private DateTime AdjustDateTime(string command)
        {
            int days = 0;
            int hours = 0;
            DateTime datetime = DateTime.Now;

            if (command.Contains(ADDDAY))
            {
                days = this.ParseNumber(command, ADDDAY);
            }

            if (command.Contains(ADDHOUR))
            {
                hours = this.ParseNumber(command, ADDHOUR);
            }

            return datetime.Add(new TimeSpan(days, hours, 0, 0));
        }

        private int ParseNumber(string command ,string flag)
        {
            var flagStart = command.Substring(command.IndexOf(flag)).Replace(flag, string.Empty);
            var numberString = flagStart.Substring(0, flagStart.IndexOf(DELIMITER));
            if (int.TryParse(numberString, out int number) && !string.IsNullOrEmpty(flag))
            {
                return number;
            }

            return 0;
        }
    }
}
