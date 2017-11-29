using Octokit;
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
            rClient.endPoint = "https://api.github.com/repos/pwilq/octo/events";

            string strResponse = string.Empty;

            strResponse = rClient.MakeRequest();

            List<GitJson> Commits = rClient.GetCommitsFromJson(strResponse);
            List<GitJson> SortedList = Commits.OrderBy(o => o.Created_at).ToList();


            // commity po użytkowniku
            var DistinctUser = SortedList.Select(x => x.Author).Distinct();
            var DistinctDate = SortedList.Select(x => x.Created_at).Distinct();


            foreach (var ddates in DistinctDate)
            {
                foreach (var duser in DistinctUser)
                {
                    var CommitsByDate = from user in Commits
                                        where user.Author == duser && user.Created_at == ddates
                                        select user;

                    Console.WriteLine("Dnia: " + ddates + " user: " + duser + " popełnił commitów: " + CommitsByDate.Count());
                }
            }

            var startDate = DateTime.Parse(SortedList[0].Created_at);
            var curentDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            var totalDays = (curentDate - startDate).TotalDays;


            Console.WriteLine();

            // Średnia ilość commmitów puer user od dnia rozpoczęcia
            foreach (var user in DistinctUser)
            {
                var CommitsByUser = from com in Commits
                                    where com.Author == user
                                    select com;

                Console.WriteLine("User: " + user + " popełnił commitów: " + CommitsByUser.Count()/totalDays);

            }

            Console.ReadKey();
            
        }

    }
}
