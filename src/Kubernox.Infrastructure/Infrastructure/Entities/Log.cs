using System;

namespace Kubernox.Infrastructure.Infrastructure.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public string LogEvent { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }
    }
}