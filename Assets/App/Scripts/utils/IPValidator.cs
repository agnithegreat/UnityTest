using System;
using System.Net;

namespace App.Scripts.utils
{
    public class IPValidator
    {
        public static bool Validate(string value)
        {
            if (value.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length == 4)
            {
                IPAddress ipAddr;
                return IPAddress.TryParse(value, out ipAddr);
            }
            return value == "localhost";
        }
    }
}