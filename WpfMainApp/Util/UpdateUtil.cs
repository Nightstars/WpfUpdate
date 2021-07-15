using Newtonsoft.Json;
using SmartApi.Areas.Business.Models;
using SmartApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            using HttpClient httpclient= new HttpClient();
            var param = new
            {
                md5 = md5
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
            var httpResponse = httpclient.PostAsync("http://192.168.2.114:10002/api/AppInfo/CheckUpdate", content).Result;
            httpResponse.EnsureSuccessStatusCode();
            if (httpResponse.IsSuccessStatusCode)
            {
                var apiResult = JsonConvert.DeserializeObject<CoreVO<AppInfo>>(httpResponse.Content.ReadAsStringAsync().Result);
                if(apiResult.Msg== "检测到新版本")
                {
                    string currentVersion = GetVersion(fileName);
                    string lastVersion = apiResult?.Data?.AppVersion;
                    string logHtml = "http://cloudapps.life:9003/";
                    string updateUrl = $"http://192.168.2.114:10002{apiResult?.Data?.AppFullName}";
                    string installPath = AppDomain.CurrentDomain.BaseDirectory;
                    string updateZipMD5 = apiResult?.Data.AppMD5;
                    Process.Start("Update.exe", $"{currentVersion}|{lastVersion}|{logHtml}|{updateUrl}|{installPath}|{updateZipMD5}");
                    Process.GetCurrentProcess().Kill();
                }

            }
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
