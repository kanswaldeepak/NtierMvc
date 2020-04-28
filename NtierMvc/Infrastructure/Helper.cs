using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtierMvc.Infrastructure
{
    public class Helper
    {
        private static Random random = new Random();
        public const string workflowInstanceEncryptString = "dkuyzu";
        public const string workitemEncryptString = "tpwyqm";
        public const string registrationIdEncryptString = "lxaymu";
        public static string RandomString(int size)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, size)
                .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray()).ToUpperInvariant();
        }
    }
}