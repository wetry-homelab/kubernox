using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Kubernox.UI.Components.Forms
{
    public partial class CreateClusterForm : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        public IState<SshKeyState> SshKeyState { get; set; }
        public IState<TemplateState> TemplateState { get; set; }
        public IState<DatacenterState> DatacenterState { get; set; }

        public string Layout { get; set; } = FormLayout.Vertical;
        protected ITable table;
        protected bool visible = false;

        [Parameter]
        public ClusterCreateRequest ClusterCreateRequest { get; set; } 

        private string FormatMB(int value)
        {
            return $"{value}MB";
        }

        private string ParseMB(string value)
        {
            return value.Replace("MB", "");
        }

        private string FormatGB(int value)
        {
            return $"{value}GB";
        }

        private string ParseGB(string value)
        {
            return value.Replace("GB", "");
        }

        void ChangeTemplate(int value)
        {
            if (value != -1)
            {
                var template = TemplateState.Value.Templates.FirstOrDefault(t => t.Id == value);

                if (template != null)
                {
                    ClusterCreateRequest.Cpu = template.CpuCount;
                    ClusterCreateRequest.Memory = template.MemoryCount;
                    ClusterCreateRequest.Storage = template.DiskSpace;
                    StateHasChanged();
                }
            }
        }

        private double GetRamUsedWidth()
        {
            var node = DatacenterState.Value.Nodes.FirstOrDefault(f => f.Id == ClusterCreateRequest.DeployNodeId);
            if (node != null)
            {
                return ((((double)node.RamUsed / (1024 * 1024)) / ((double)node.RamTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private double GetRamToClaimWidth()
        {
            var node = DatacenterState.Value.Nodes.FirstOrDefault(f => f.Id == ClusterCreateRequest.DeployNodeId);
            if (node != null)
            {
                return (((double)(ClusterCreateRequest.Memory * (ClusterCreateRequest.Node + 1)) / ((double)node.RamTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private bool Overheat()
        {
            return (GetRamUsedWidth() + GetRamToClaimWidth()) > 100;
        }
    }
}
