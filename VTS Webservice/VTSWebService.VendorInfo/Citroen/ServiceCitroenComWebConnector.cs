using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using VTSWebService.VendorInfo.PsaCommon;

namespace VTSWebService.VendorInfo.Citroen
{
    internal class ServiceCitroenComWebConnector
    {
        //private const string Login = "AC47473733";
        //private const string Password = "toriyama";
        private const string LoginUrlStringFormat =
            "http://service.citroen.com/do/login?userid={0}&password={1}";
        private const string InitialRedirectAddress =
            "http://service.citroen.com/do/newApvprStart";
        private const string LogoutUrl =
            "http://service.citroen.com/do/logout";
        private const string VinUrlStringFormat =
            "http://service.citroen.com/do/ok?VIN_OK_BUTTON.x=10&VIN_OK_BUTTON.y=13&jvin={0}&wmi={1}&vds={2}&vis={3}";
        private const string CharacteristicsUrl =
            "http://service.citroen.com/docapv/caracteristique.do";

        private static string cookieHeader;
        private string language;

        public ServiceCitroenComWebConnector(string language)
        {
            this.language = language;
        }

        public void Connect()
        {
            VendorServiceCredentialsProvider prov =
                new VendorServiceCredentialsProvider();
            KeyValuePair<string, string> creds = prov.GetCitroenCredentials();
            string login = creds.Key;
            string password = creds.Value;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(
                String.Format(LoginUrlStringFormat, login, password));
            req.AllowAutoRedirect = false;
            req.Method = "GET";
            WebResponse resp1;
            try
            {
                resp1 = req.GetResponse();
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
            HttpWebRequest rq2 =
                (HttpWebRequest)WebRequest.Create(InitialRedirectAddress);
            rq2.Method = "GET";
            try
            {
                rq2.GetResponse();
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
            if (String.IsNullOrEmpty(cookieHeader))
            {
                cookieHeader = ProcessCookieHeader(resp1.Headers["Set-cookie"]);
            }
        }

        public void Disconnect()
        {
            cookieHeader = String.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(
                LogoutUrl);
            req.ContentType = "text/html; charset=UTF-8";
            req.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9,*/*,q=0.8";
            req.Method = "GET";
            req.GetResponse();
        }

        public string Retrieve(string vin)
        {
            SubmitVin(vin);
            return GetCharacteristicsPage();
        }

        private void SubmitVin(string vin)
        {
            WebRequest request = WebRequest.Create(GetVinUrl(vin));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AuthenticationLevel = AuthenticationLevel.None;
            request.Headers.Add("Cookie", cookieHeader);
            try
            {
                WebResponse resp = request.GetResponse();
                if (resp.ContentLength != -1)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
            // TODO : if response is not 0 length than something went wrong
        }

        private string GetCharacteristicsPage()
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(CharacteristicsUrl);
            request.AuthenticationLevel = AuthenticationLevel.None;
            request.Headers.Add("Cookie", cookieHeader);
            request.Method = "GET";
            WebResponse resp2;
            try
            {
                resp2 = request.GetResponse();
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
            string pageSource2;
            using (StreamReader sr =
                new StreamReader(resp2.GetResponseStream()))
            {
                pageSource2 = sr.ReadToEnd();
            }
            return pageSource2;
        }

        private string ProcessCookieHeader(string rawCookieHeader)
        {
            string cookieFormat = "CodeLanguePaysOI={2}; NEWAPVPROI_OIN_CITROEN={1}; JSESSIONID={0}; Enabled=true";
            IList<string> split = rawCookieHeader.Split(';').ToList();
            string jsessionId = split.FirstOrDefault(li => li.Contains("JSESSIONID")).Substring(19);
            string oin = split.FirstOrDefault(li => li.Contains("NEWAPVPROI_OIN_CITROEN")).Substring(23);
            string result = String.Format(cookieFormat, jsessionId, oin, language);
            return result;
        }

        private static string GetVinUrl(string vin)
        {
            string wmi = vin.Substring(0, 3);
            string vds = vin.Substring(3, 6);
            string vis = vin.Substring(9);
            string fullUrl =
                String.Format(VinUrlStringFormat, vin, wmi, vds, vis);
            return fullUrl;
        }
    }
}
