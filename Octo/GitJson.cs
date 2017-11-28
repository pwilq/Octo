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

        public GitJson(string _author, string _message, long _push_id, string _created_at)
        {
            this.Author = _author;
            this.Message = _message;
            this.Push_id = _push_id;
            this.Created_at = _created_at;
        }
    }
}
