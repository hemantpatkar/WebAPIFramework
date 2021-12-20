using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Base.Security
{
    public class Secure : ISecure
    {
        public string Password(string input)
        {
            StringBuilder hash = new StringBuilder();
            SHA512 provider = new SHA512Managed();
            byte[] bytes = provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }

    public interface ISecure
    {
        string Password(string input);
    }
}
