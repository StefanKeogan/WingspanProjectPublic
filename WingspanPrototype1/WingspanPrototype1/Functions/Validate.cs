using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WingspanPrototype1.Functions
{
    class Validate
    {
        public static bool AllFeildsEmpty(string[] feilds)
        {
            bool result = true;

            foreach (var feild in feilds)
            {
                if ((feild != string.Empty) && 
                    (feild != null) && 
                    (feild != ""))
                {
                    result = false;
                }
                
            }

            return result;
        }

        public static bool FeildPopulated(string feild)
        {
            bool result = false;

            if ((feild != string.Empty) &&
                    (feild != null) &&
                    (feild != ""))
            {
                result = true;
            }

            return result;
        }


        public static bool ContainsNumberOrSymbol(string feild)
        {
            bool result = false;

            if ((feild.Any(char.IsDigit)) ||
                (feild.Any(char.IsPunctuation)) ||
                (feild.Any(char.IsSymbol)))
            {
                result = true;
            }

            return result;

        }

        public static bool ContainsLetterOrSymbol(string feild)
        {
            bool result = false;

            if ((feild.Any(char.IsLetter)) ||
                (feild.Any(char.IsPunctuation)) ||
                (feild.Any(char.IsSymbol)))
            {
                result = true;
            }

            return result;
        }
    }

    
}
