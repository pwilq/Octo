﻿using Octokit;
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

            Console.WriteLine(Commits.Count());
            //foreach (var com in Commits)
            //{
            //    Console.WriteLine("Autor: " + com.Author + " Data: " + com.Created_at + " Opis: " + com.Message);
            //}

            List<GitJson> SortedList = Commits.OrderBy(o => o.Push_id).ToList();



            foreach (var com in SortedList)
            {
                Console.WriteLine("Autor: " + com.Author + " Data: " + com.Created_at + " ID: " + com.Push_id);
            }

            var DistinctList = SortedList.Select(x => x.Author).Distinct();

            Console.WriteLine("Distict values");
            foreach (var com in DistinctList)
            {
                
                Console.WriteLine("Autor: " + com );
            }

            Console.WriteLine("Searched values");
            var EqualList = SortedList.Select(s => s.Author.Equals("pwilq"));

            foreach (var com in EqualList)
            {

                Console.WriteLine("Autor: " + com);
            }

            Console.ReadKey();

            //Console.WriteLine("Hello Dev!");
            //ProcessRepositories().Wait();
            
        }



        private static async Task ProcessRepositories()
        {

            var gitclient = new GitHubClient(new Octokit.ProductHeaderValue("pwilq"));
            
            var user = await gitclient.User.Get("pwilq");

            Console.WriteLine("Username: {0}; PublicRepo {1}; Url: {2}", user.Name, user.PublicRepos, user.Url);

            var branch = await gitclient.Repository.Branch.GetAll("pwilq", "Octo");
            var myrepo = await gitclient.Repository.Get("pwilq", "Octo");
            var commits = await gitclient.Repository.Commit.GetAll("pwilq", "Octo");

            Console.WriteLine("Branch Count: " + branch.Count);

            for (int i = 0; i < branch.Count; i++)
            {
                Console.WriteLine("Branch: "+ branch[i].Name);
            }

            Console.WriteLine("Commits Count: " + commits.Count);

            foreach (var com in commits)
            {
                var commit = await gitclient.Repository.Commit.Get("pwilq", "Octo", com.Sha);
                var files = commit.Files;
                
                foreach (var f in files)
                {
                    Console.WriteLine("User: " + commit.Author.Login.ToString() + " File: " + f.Filename);
                }
            }
            
            Console.ReadKey();

        }
    }
}
