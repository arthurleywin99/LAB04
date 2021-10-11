using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB04_01.Controller
{
    public class UtilityMethod
    {
        private static readonly string[] VietNameseChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string UnicodeToAscii(string str)
        {
            for (int i = 1; i < VietNameseChar.Length; i++)
            {
                for (int j = 0; j < VietNameseChar[i].Length; j++)
                    str = str.Replace(VietNameseChar[i][j], VietNameseChar[0][i - 1]);
            }
            return str;
        }
    }
}
