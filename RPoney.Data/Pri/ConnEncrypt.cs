using System;
using System.IO;
using System.Text;

namespace RPoney.Data.Pri
{
    internal static class ConnEncrypt
    {
        private static readonly string KEY = "&@pNC*~/*(2c5@~_1iKl+@PcyCyOviXCc871!AS#*P6#C/.=1AuI#BrCYb6%1&*===";
        private static readonly Encoding StringEncoding = Encoding.GetEncoding("UTF-8");
        private static byte[] DataDecrypt(byte[] data, string pass)
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
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string DecryptFile(string fileFullName)
        {
            byte[] bytes = DataDecrypt(File.ReadAllBytes(fileFullName), KEY);
            return StringEncoding.GetString(bytes);
        }

        private static byte[] GetKey(byte[] pass, int kLen)
        {
            byte[] buffer = new byte[kLen];
            for (long i = 0L; i < kLen; i += 1L)
            {
                buffer[(int)((IntPtr)i)] = (byte)i;
            }
            long num2 = 0L;
            for (long j = 0L; j < kLen; j += 1L)
            {
                num2 = ((num2 + buffer[(int)((IntPtr)j)]) + pass[(int)((IntPtr)(j % ((long)pass.Length)))]) % ((long)kLen);
                byte num4 = buffer[(int)((IntPtr)j)];
                buffer[(int)((IntPtr)j)] = buffer[(int)((IntPtr)num2)];
                buffer[(int)((IntPtr)num2)] = num4;
            }
            return buffer;
        }
    }
}
