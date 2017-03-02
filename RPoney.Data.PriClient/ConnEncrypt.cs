using System;
using System.IO;
using System.Text;

namespace RPoney.Data.PriClient
{
    internal static class ConnEncrypt
    {
        // Fields
        private static readonly string KEY = "&@pNC*~/*(2c5@~_1iKl+@PcyCyOviXCc871!AS#*P6#C/.=1AuI#BrCYb6%1&*===";
        private static readonly Encoding StringEncoding = Encoding.GetEncoding("UTF-8");

        // Methods
        public static string DecryptFile(string fileFullName)
        {
            byte[] bytes = RC4Decrypt(File.ReadAllBytes(fileFullName), KEY);
            return StringEncoding.GetString(bytes);
        }

        public static void EncryptAndWriteFile(string connString, string fileFullName)
        {
            byte[] bytes = RC4Encrypt(StringEncoding.GetBytes(connString), KEY);
            File.WriteAllBytes(fileFullName, bytes);
        }

        private static byte[] GetKey(byte[] pass, int kLen)
        {
            long num;
            byte[] buffer = new byte[kLen];
            for (num = 0L; num < kLen; num += 1L)
            {
                buffer[(int)((IntPtr)num)] = (byte)num;
            }
            long num2 = 0L;
            for (num = 0L; num < kLen; num += 1L)
            {
                num2 = ((num2 + buffer[(int)((IntPtr)num)]) + pass[(int)((IntPtr)(num % ((long)pass.Length)))]) % ((long)kLen);
                byte num3 = buffer[(int)((IntPtr)num)];
                buffer[(int)((IntPtr)num)] = buffer[(int)((IntPtr)num2)];
                buffer[(int)((IntPtr)num2)] = num3;
            }
            return buffer;
        }

        public static byte[] RC4Decrypt(byte[] data, string pass) => RC4Encrypt(data, pass);

        public static byte[] RC4Encrypt(byte[] data, string pass)
        {
            if ((data == null) || (pass == null))
            {
                return null;
            }
            byte[] buffer = new byte[data.Length];
            long num = 0L;
            long num2 = 0L;
            byte[] key = GetKey(StringEncoding.GetBytes(pass), 0x100);
            for (long i = 0L; i < data.Length; i += 1L)
            {
                num = (num + 1L) % ((long)key.Length);
                num2 = (num2 + key[(int)((IntPtr)num)]) % ((long)key.Length);
                byte num4 = key[(int)((IntPtr)num)];
                key[(int)((IntPtr)num)] = key[(int)((IntPtr)num2)];
                key[(int)((IntPtr)num2)] = num4;
                byte num5 = data[(int)((IntPtr)i)];
                byte num6 = key[(key[(int)((IntPtr)num)] + key[(int)((IntPtr)num2)]) % key.Length];
                buffer[(int)((IntPtr)i)] = (byte)(num5 ^ num6);
            }
            return buffer;
        }
    }
}
