using System.Collections.Generic;

namespace Kubernox.Model
{
    public class TraefikProvider
    {
        public string Path { get; set; }
        public string ApiKey { get; set; }
        public Dictionary<string, string> LetsencryptProviders { get; set; }
    }
}
