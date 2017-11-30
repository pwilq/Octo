using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Octo
{
    class Program
    {

        public static void Main(string[] args)
        {
            RestClient rClient = new RestClient();
            GitJson services = new GitJson();

            string strResponse = string.Empty;
            strResponse = rClient.MakeRequest();

            List<GitJson> GitData = rClient.GetCommitsFromJson(strResponse);

            //Commity użytkowników w poszczególnych dniach
            List<GitJson> CommitsData = services.CommitsPerDay(GitData);
            //Console.WriteLine(CommitsData.Count());


            //Średnia ilość commmitów per user od dnia rozpoczęcia
            List<GitJson> CommitsAvg = services.CommitsAvg(GitData);
            //Console.WriteLine(CommitsAvg.Count());

            
            Console.ReadKey();
            
        }

    }
}
