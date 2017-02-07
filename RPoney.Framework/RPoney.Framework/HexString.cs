using System;

namespace RPoney
{
    /// <summary>
    /// 16进制字符串
    /// </summary>
    public static class HexString
    {
        // Fields
        private static readonly char[] HexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static readonly char[] HexDigitsLower = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// 获取16进制字符串的字节数组
        /// </summary>
        /// <param name="str">16进制字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(string str)
        {
            char[] chArray = str.ToCharArray();
            byte[] buffer = new byte[chArray.Length / 2];
            int index = 0;
            int length = buffer.Length;
            while (index < length)
            {
                int num3 = Convert.ToInt32(new string(new char[] { chArray[index * 2], chArray[(index * 2) + 1] }), 0x10);
                buffer[index] = (byte)num3;
                index++;
            }
            return buffer;
        }
        /// <summary>
        /// 获取字节数组的16进制字符串值
        /// </summary>
        /// <param name="bytes">源字节数组</param>
        /// <returns>大写16进制值</returns>
        public static string GetHexString(this byte[] bytes)
        {
            char[] chArray = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int num2 = bytes[i];
                chArray[i * 2] = HexDigits[num2 >> 4];
                chArray[(i * 2) + 1] = HexDigits[num2 & 15];
            }
            return new string(chArray);
        }
        /// <summary>
        /// 获取字节数组的16进制字符串值
        /// </summary>
        /// <param name="bytes">源字节数组</param>
        /// <returns>小写16进制值</returns>
        public static string GetLowerHexString(this byte[] bytes)
        {
            char[] chArray = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int num2 = bytes[i];
                chArray[i * 2] = HexDigitsLower[num2 >> 4];
                chArray[(i * 2) + 1] = HexDigitsLower[num2 & 15];
            }
            return new string(chArray);
        }
    }
}
