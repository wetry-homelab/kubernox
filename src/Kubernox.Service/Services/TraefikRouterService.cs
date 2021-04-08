using Application.Entities;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Service.Services
{
    public class TraefikRouterService : ITraefikRouterService
    {
        private readonly ITraefikRouteValueRepository traefikRouteValueRepository;
        private readonly ITraefikRedisStore traefikRedisStore;
        private readonly ILogger<TraefikRouterService> logger;
        private readonly string defaultResolver;

        public TraefikRouterService(IConfiguration configuration, ITraefikRouteValueRepository traefikRouteValueRepository, ILogger<TraefikRouterService> logger, ITraefikRedisStore traefikRedisStore)
        {
            this.traefikRouteValueRepository = traefikRouteValueRepository;
            this.logger = logger;
            this.traefikRedisStore = traefikRedisStore;
            this.defaultResolver = configuration["Kubernox:DefaultResolver"];
        }

        public async Task GenerateClusterBasicRules(Cluster cluster)
        {
            var traefikBasesRules = GenerateKubectlBaseRouting(cluster.Id, cluster.Domain, cluster.Name, cluster.Ip);
            traefikBasesRules.AddRange(GenerateHttpBaseRouting(cluster.Id, cluster.Domain, cluster.Name, cluster.Ip));
            traefikBasesRules.AddRange(GenerateHttpsBaseRouting(cluster.Id, cluster.Domain, cluster.Name, cluster.Ip));

            if ((await traefikRouteValueRepository.InsertsAsync(traefikBasesRules.ToArray()) == traefikBasesRules.Count))
            {
                var values = ConvertToKvp(traefikBasesRules.ToArray());
                await traefikRedisStore.StoreValues(values.ToList());
            }
        }

        public async Task RefreshClusterRule(string clusterId)
        {
            var rules = await traefikRouteValueRepository.ReadsAsync(r => r.ClusterId == clusterId);
            var values = ConvertToKvp(rules.ToArray());
            await traefikRedisStore.StoreValues(values.ToList());
        }

        public async Task StoreNewRule(Cluster cluster, string domain)
        {
            var newTraefikRule = GenerateNewRule(cluster.Id, cluster.Name, domain, cluster.Ip);

            if ((await traefikRouteValueRepository.InsertsAsync(newTraefikRule.ToArray()) == newTraefikRule.Count))
            {
                var values = ConvertToKvp(newTraefikRule.ToArray());
                await traefikRedisStore.StoreValues(values.ToList());
            }
        }

        public async Task StoreNewHttpRule(Cluster cluster, string domain)
        {
            var newTraefikRule = GenerateNewHttpRule(cluster.Id, cluster.Name, domain, cluster.Ip);

            if ((await traefikRouteValueRepository.InsertsAsync(newTraefikRule.ToArray()) == newTraefikRule.Count))
            {
                var values = ConvertToKvp(newTraefikRule.ToArray());
                await traefikRedisStore.StoreValues(values.ToList());
            }
        }

        public async Task StoreNewHttpsRule(Cluster cluster,  string subDomain, string domain)
        {
            var newTraefikRule = GenerateNewHttpsRule(cluster.Id, cluster.Name, subDomain, domain, cluster.Ip);

            if ((await traefikRouteValueRepository.InsertsAsync(newTraefikRule.ToArray()) == newTraefikRule.Count))
            {
                var values = ConvertToKvp(newTraefikRule.ToArray());
                await traefikRedisStore.StoreValues(values.ToList());
            }
        }

        public async Task DeleteClusterRules(Cluster cluster)
        {
            var rules = await traefikRouteValueRepository.ReadsAsync(r => r.ClusterId == cluster.Id);
            var redisRuleKeys = ConvertToKvp(rules).Select(s => s.Key).ToList();

            if ((await traefikRouteValueRepository.DeletesAsync(rules)) == rules.Length)
            {
                await traefikRedisStore.DeleteValues(redisRuleKeys);
            }
        }

        public async Task DeleteRule(string ruleId)
        {
            var rules = await traefikRouteValueRepository.ReadsAsync(r => r.RuleId == ruleId);
            var redisRuleKeys = ConvertToKvp(rules).Select(s => s.Key).ToList();

            if ((await traefikRouteValueRepository.DeletesAsync(rules)) == rules.Length)
            {
                await traefikRedisStore.DeleteValues(redisRuleKeys);
            }
        }

        public async Task DeleteRule(string clusterName, string domain)
        {
            var ruleId = $"{clusterName}-webSecure-{domain}";
            var rules = await traefikRouteValueRepository.ReadsAsync(r => r.RuleId.Contains(ruleId) && r.Domain == domain);
            var redisRuleKeys = ConvertToKvp(rules).Select(s => s.Key).ToList();

            if ((await traefikRouteValueRepository.DeletesAsync(rules)) == rules.Length)
            {
                await traefikRedisStore.DeleteValues(redisRuleKeys);
            }
        }

        private List<TraefikRouteValue> GenerateKubectlBaseRouting(string clusterId, string domain, string clusterName, string ip)
        {
            var ruleId = $"{clusterName}-k3s";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/tcp/services/{ruleId}-tcp-lb/loadbalancer/servers/0/address", $"{ip}:6443"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/tcp/routers/{ruleId}/service", $"{ruleId}-tcp-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/tcp/routers/{ruleId}/tls/passthrough", $"true"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/tcp/routers/{ruleId}/entryPoints/0", $"k3s"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/tcp/routers/{ruleId}/rule", $"HostSNI(`{clusterName}.{domain}`)"));

            return ruleValues.ToList();
        }

        private List<TraefikRouteValue> GenerateHttpBaseRouting(string clusterId, string domain, string clusterName, string ip)
        {
            var ruleId = $"{clusterName}-web-base";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/services/{ruleId}-http-lb/loadbalancer/servers/0/url", $"http://{ip}:80"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/entrypoints/0", $"web"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/service", $"{ruleId}-http-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/rule", $"Host(`{domain}`)"));

            return ruleValues.ToList();
        }

        private List<TraefikRouteValue> GenerateHttpsBaseRouting(string clusterId, string domain, string clusterName, string ip)
        {
            var ruleId = $"{clusterName}-webSecure-base";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/services/{ruleId}-https-lb/loadbalancer/servers/0/url", $"http://{ip}:443"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/entrypoints/0", $"websecure"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/tls", $"true"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/service", $"{ruleId}-https-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/rule", $"Host(`{clusterName}.{domain}`)"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/tls/domains/0/sans/0", $"*.{domain}"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-https/tls/certresolver", $"{defaultResolver}"));

            return ruleValues.ToList();
        }

        private List<TraefikRouteValue> GenerateNewHttpRule(string clusterId, string clusterName, string domain, string ip)
        {
            var ruleId = $"{clusterName}-custom-{domain.Replace(".", "-")}";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/services/{ruleId}-http-lb/loadbalancer/servers/0/url", $"http://{ip}:80"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/entrypoints/0", $"web"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/service", $"{ruleId}-http-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, $"traefik/http/routers/{ruleId}-http/rule", $"Host(`{domain}`)"));

            return ruleValues.ToList();
        }

        private List<TraefikRouteValue> GenerateNewRule(string clusterId, string clusterName, string domain, string ip)
        {
            var ruleId = $"{clusterName}-custom-{domain.Replace(".", "-")}";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/services/{ruleId}-https-lb/loadbalancer/servers/0/url", $"http://{ip}:443"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/entrypoints/0", $"websecure"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls", $"true"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/service", $"{ruleId}-https-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/rule", $"Host(`{domain}`)"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls/domains/0/sans/0", $"*.{domain}"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls/certresolver", $"{defaultResolver}"));

            return ruleValues.ToList();
        }

        private List<TraefikRouteValue> GenerateNewHttpsRule(string clusterId, string clusterName, string subDomain, string domain, string ip)
        {
            var ruleId = $"{clusterName}-custom-{subDomain.Replace(".", "-")}-{domain.Replace(".", "-")}";
            var ruleValues = new List<TraefikRouteValue>();

            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/services/{ruleId}-https-lb/loadbalancer/servers/0/url", $"http://{ip}:443"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/entrypoints/0", $"websecure"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls", $"true"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/service", $"{ruleId}-https-lb"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/rule", $"Host(`{subDomain}.{domain}`)"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls/domains/0/sans/0", $"*.{domain}"));
            ruleValues.Add(new TraefikRouteValue(clusterId, ruleId, domain, $"traefik/http/routers/{ruleId}-https/tls/certresolver", $"{defaultResolver}"));

            return ruleValues.ToList();
        }

        public async Task LoadAndRefreshRouting(CancellationToken stoppingToken)
        {
            logger.LogInformation("Loading Traefik KV Table to inject.");
            var routeValues = await traefikRouteValueRepository.ReadsAsync();
            await traefikRedisStore.StoreValues(routeValues.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value)).ToList());
        }

        private KeyValuePair<string, string>[] ConvertToKvp(TraefikRouteValue[] values)
        {
            return values.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value)).ToArray();
        }
    }
}
