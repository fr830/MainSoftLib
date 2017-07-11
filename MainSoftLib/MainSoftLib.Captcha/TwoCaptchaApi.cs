using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Captcha
{
    public class TwoCaptchaApi
    {
        string url_request = "http://2captcha.com/in.php";
        string url_response = "http://2captcha.com/res.php";

        string key = null;
        public string captchaId;

        public TwoCaptchaApi(string key)
        {
            this.key = key;
        }

        public float getBalance()
        {
            float balance = -1;
            string response = "";

            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("key", key);
                client.QueryString.Add("action", "getbalance");
                response = client.DownloadString(url_response);
            }

            if (!float.TryParse(response, out balance))
            {
                throw new Exception($"2Captcha - Error while checking balance: {response}");
            }

            return balance;
        }

        public string solveReCaptcha(string googleKey, string pageUrl)
        {
            captchaId = sendReCaptcha(googleKey, pageUrl);
            Thread.Sleep(15 * 1000);
            return getResult(captchaId);
        }

        private string sendReCaptcha(string googleKey, string pageUrl)
        {
            string response = "";

            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("key", key);
                client.QueryString.Add("method", "userrecaptcha");
                client.QueryString.Add("googlekey", googleKey);
                client.QueryString.Add("pageurl", pageUrl);
                response = client.DownloadString(url_request);
            }

            if (response.Substring(0, 3) != "OK|")
                throw new Exception($"Captcha sending error: {response}");

            return response.Remove(0, 3);
        }

        public string uploadCaptcha(string path, Dictionary<string, string> param = null)
        {
            if (!File.Exists(path))
            {
                throw new Exception("File doesn't exist");
            }

            byte[] image = File.ReadAllBytes(path);
            string response = "";

            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("key", key);

                if (param!=null)
                {
                    foreach (var item in param)
                    {
                        client.QueryString.Add(item.Key, item.Value);
                    }
                }
                               
                //client.QueryString.Add("phrase", "1");
                //client.QueryString.Add("numeric", "1");
                //client.QueryString.Add("min_len", "5");
                //client.QueryString.Add("max_len", "5");

                response = Encoding.Default.GetString(client.UploadFile(url_request, path));
            }

            if (response.Substring(0, 3) != "OK|")
            {
                throw new Exception($"Captcha uploading error: {response}");
            }

            return response.Remove(0, 3);
        }

        public string getResult(string captchaId)
        {
            string response = "";

            for (int i = 0; i <= 10; i++)
            {
                using (WebClient client = new WebClient())
                {
                    client.QueryString.Add("key", key);
                    client.QueryString.Add("action", "get");
                    client.QueryString.Add("id", captchaId);

                    response = client.DownloadString(url_response);
                }

                if (response.Substring(0, 3) == "OK|")
                {
                    return response.Remove(0, 3);
                }
                else if (response.Contains("ERROR"))
                {
                    throw new Exception($"Captcha solve error: {response}");                    
                }

                Thread.Sleep(5 * 1000);
            }
            
            throw new Exception($"Captcha solve error: {response}");
        }

        public string solveCaptcha(string path)
        {
            captchaId = uploadCaptcha(path);
            Thread.Sleep(10 * 1000);
            return getResult(captchaId);
        }

        public bool reportBadCaptcha(string captchaId)
        {
            string response = "";

            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("key", key);
                client.QueryString.Add("action", "reportbad");
                client.QueryString.Add("id", captchaId);
                response = client.DownloadString(url_response);
            }

            if (response.Contains("OK_REPORT_RECORDED"))
            {
                return true;
            }

            return false;
        }
    }
}
