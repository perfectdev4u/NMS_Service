using System.Collections.Generic;

namespace NmsService.Models
{
    public class Result
    {
        public int statement_id { get; set; }
        public List<Series> series { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
    }

    public class Series
    {
        public string name { get; set; }
        public List<string> columns { get; set; }
        public List<List<object>> values { get; set; }
    }
}
