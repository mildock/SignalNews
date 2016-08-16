using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalkToLog.Bot.Data;

namespace TalkToLog.Bot.Dialogs
{
    public static class DialogHelper
    {
        public static string ConvertHumanReadable(long n)
        {
            if (n == 0) return "0";

            double number = (double)n;

            int mag = (int)(Math.Floor(Math.Log10(number)) / 3); // Truncates to 6, divides to 2
            double divisor = Math.Pow(10, mag * 3);

            double shortNumber = number / divisor;

            string suffix;
            switch (mag)
            {
                case 0:
                    suffix = string.Empty;
                    break;
                case 1:
                    suffix = "k";
                    break;
                case 2:
                    suffix = "m";
                    break;
                case 3:
                    suffix = "b";
                    break;
                default:
                    suffix = string.Empty;
                    break;
            }

            string result = shortNumber.ToString("N1") + suffix;
            return result;
        }

        public static string CovertSignalToImoticon(HealthSignal signal)
        {
            string result;
            switch (signal)
            {
                case HealthSignal.Red:
                    result = "(shock)";
                    break;
                case HealthSignal.Yellow:
                    result = ":(";
                    break;
                default:
                    result = ":)";
                    break;
            }
            return result;
        }
    }
}