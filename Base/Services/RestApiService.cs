using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    public class RestApiService
    {
        private static RestApiService m_instance;

        private RestApiService()
        {

        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static RestApiService Instance()
        {
            if (m_instance == null)
            {
                m_instance = new RestApiService();
            }
            return m_instance;
        }

        public string POST(JObject json, string url)
        {
            LogService.LogInformation($"Start POST to URL: {url} with json: {json}");
            //log start POST
            string result;
            try
            {
                Uri myUri = new Uri(url);
                WebRequest myWebRequest = HttpWebRequest.Create(myUri);
                HttpWebRequest httpWebRequest = (HttpWebRequest)myWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json.ToString());
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    LogService.LogInformation($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                LogService.LogError(result);
            }
            return result;
        }


        public string GET(string url)
        {
            LogService.LogInformation($"Start GET to URL: {url} ");
            //log start POST
            string result;
            try
            {
                //_AWSUrl += _days;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    LogService.LogInformation($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                LogService.LogError(result);
            }
            return result;
        }

    }
}
