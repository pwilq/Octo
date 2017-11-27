using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Octo
{
    public enum httpVerb
    {
        GET,
        POST
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        private string oAuthtoken { get; set; }
        private string userAgent { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
            userAgent = "pwilq";
        }


        public void Deserialize(string strJSON)
        {
            try
            {
                var gitResponse = JsonConvert.DeserializeObject<dynamic>(strJSON);
                Console.WriteLine(gitResponse); 

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
        }


        public string MakeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();
            request.UserAgent = userAgent;
            //request.Headers.Add(HttpRequestHeader.Authorization, oAuthtoken);
             
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException("error code: " + response.StatusCode);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        strResponseValue = reader.ReadToEnd();
                    }
                }
            }

            return strResponseValue;
            
        }
    }
}
