using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Access
{
    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = Encoding.UTF8.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            var resultString = BitConverter.ToString(resultBytes);
            resultString = resultString.Replace("-", "");
            return resultString;
        }
    }
}
