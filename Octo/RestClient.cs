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

        List<GitJson> J = new List<GitJson>();

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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        public List<GitJson> GetCommitsFromJson(string _json)
        {
            dynamic _jsonDes = JsonConvert.DeserializeObject(_json);

            for (int i = 0; i < _jsonDes.Count; i++)
            {
                if (_jsonDes[i].type == "PushEvent")
                {
                    for (int j = 0; j < _jsonDes[i].payload.commits.Count; j++)
                    {
                        J.Add(new GitJson((string)_jsonDes[i].payload.commits[j].author.name, (string)_jsonDes[i].payload.commits[j].message, (long)_jsonDes[i].payload.push_id, (string)_jsonDes[i].created_at.ToString("yyyy/MM/dd")));
                    }
                }
            }
            return J;
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
