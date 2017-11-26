using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Octo
{
    class Program
    {

        public static void Main(string[] args)
        {

            Console.WriteLine("Hello Dev!");
            ProcessRepositories().Wait();


        }
        private static async Task ProcessRepositories()
        {

            var gitclient = new GitHubClient(new Octokit.ProductHeaderValue("pwilq"));
            var user = await gitclient.User.Get("pwilq");

            Console.WriteLine("Username: {0}; PublicRepo {1}; Url: {2}",
                user.Name,
                user.PublicRepos,
                user.Url);

            Console.ReadKey();

            var branch = await gitclient.Repository.Branch.GetAll("pwilq", "Octo");

            for (int i = 0; i < branch.Count; i++)
            {
                Console.WriteLine(branch[i].Name + "; commit: " + branch[i].Commit);
            }

            Console.ReadKey();




        }
    }
}
