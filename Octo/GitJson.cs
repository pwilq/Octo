using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo
{
    class GitJson
    {
        public string Author { get; private set; }
        public string Message { get; private set; }
        public long Push_id { get; private set; }
        public string Created_at { get; private set; }
        public int Commits { get; private set; }
        public double AvgCom { get; private set; }

        List<GitJson> _CommitsByUser = new List<GitJson>();
        List<GitJson> _CommitsAvg = new List<GitJson>();

        public GitJson() { }

        public GitJson(string _author, string _message, long _push_id, string _created_at)
        {
            this.Author = _author;
            this.Message = _message;
            this.Push_id = _push_id;
            this.Created_at = _created_at;
        }

        public GitJson(string _author, string _created_at, double _avgcom)
        {
            this.Author = _author;
            this.Created_at = _created_at;
            this.AvgCom = _avgcom;
        }

        public GitJson(string _author, double _avgCom)
        {
            this.Author = _author;
            this.AvgCom = _avgCom;
        }

        public List<GitJson> CommitsPerDay(List<GitJson> _gitdata)
        {
            var DistinctUser = _gitdata.Select(x => x.Author).Distinct();
            var DistinctDate = _gitdata.Select(x => x.Created_at).Distinct();

            foreach (var ddates in DistinctDate)
            {
                foreach (var duser in DistinctUser)
                {
                    var CommitsByDate = from user in _gitdata
                                        where user.Author == duser && user.Created_at == ddates
                                        select user;

                    Console.WriteLine("Dnia: " + ddates + "\t user: " + duser + "\t popełnił commitów: " + CommitsByDate.Count());
                    _CommitsByUser.Add(new GitJson(duser, ddates, CommitsByDate.Count()));
                }
            }

            return _CommitsByUser;
        }

        public List<GitJson> CommitsAvg(List<GitJson> _gitdata)
        {
            var startDate = DateTime.Parse(_gitdata.OrderBy(o => o.Created_at).ToList()[0].Created_at);
            var curentDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            var totalDays = (curentDate - startDate).TotalDays;
            var DistinctUser = _gitdata.Select(x => x.Author).Distinct();

            
            foreach (var user in DistinctUser)
            {
                var CommitsUser = from com in _gitdata
                                  where com.Author == user
                                  select com;
                var avg = CommitsUser.Count() / totalDays;
                Console.WriteLine("User: " + user + "\t popełnił średnio commitów: " + avg);
                _CommitsAvg.Add(new GitJson(user, avg));
            }

            return _CommitsAvg;
        }
    }
}
