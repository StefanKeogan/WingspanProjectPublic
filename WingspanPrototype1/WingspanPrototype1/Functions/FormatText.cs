using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Functions
{
    class FormatText
    {
        public static string FirstToUpper(string text)
        {
            if (Validate.FeildPopulated(text))
            {
                string upperText = char.ToUpper(text[0]) + text.Substring(1);

                return upperText;
            }
            else
            {
                return null;
            }
        }
    }
}
