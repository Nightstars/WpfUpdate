using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfMainApp.Util
{
    public class UpdateUtil
    {
        public string md5value { get; set; }

        public void checckUpdate(string fileName)
        {
            var md5=GetMD5(fileName);
            var version = GetVersion(fileName);
        }

        public String GetMD5(string fileName)
        {
            try
            {
                FileStream md5filestream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                MD5 mD5 = MD5.Create();
                byte[] hash_byte = mD5.ComputeHash(md5filestream);
                md5filestream.Close();
                return Convert.ToBase64String(hash_byte);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string GetVersion(string filename)
        {
            try
            {
                FileVersionInfo file = System.Diagnostics.FileVersionInfo.GetVersionInfo(filename);
                return file.ProductVersion;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
