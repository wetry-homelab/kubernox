using AntDesign;
using Fluxor;
using Infrastructure.Contracts.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Services;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.Actions.Template;
using Kubernox.UI.Store.States;
using System;
using System.Linq;
using System.Threading.Tasks;
using Kubernox.UI.Utils;
using Kubernox.UI.Services.Interfaces;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class Index : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        private IState<ClusterState> ClusterState { get; set; }

        [Inject]
        private IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        private IState<TemplateState> TemplateState { get; set; }

        [Inject]
        private IState<SshKeyState> SshKeyState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IClusterService ClusterService { get; set; }

        public bool IsLoad
        {
            get
            {
                return ClusterState != null && ClusterState.Value.IsLoading
                        && TemplateState != null && TemplateState.Value.IsLoading
                        && SshKeyState != null && SshKeyState.Value.IsLoading;
            }
        }

        public string Layout { get; set; } = FormLayout.Vertical;
        protected ITable table;
        protected bool visible = false;

        protected ClusterCreateRequest clusterCreateRequest = new ClusterCreateRequest()
        {
            Memory = 1024,
            Cpu = 1,
            SelectedTemplate = -1,
            Storage = 20,
            Node = 2,
            DeployNodeId = -1
        };

        protected string selectedTemplate = "custom";

        protected override void OnInitialized()
        {
            ClusterState.StateChanged += ClusterState_StateChanged;
            SshKeyState.StateChanged += SshKeyState_StateChanged;
            TemplateState.StateChanged += TemplateState_StateChanged;
            DatacenterState.StateChanged += DatacenterState_StateChanged;
            Dispatcher.Dispatch(new FetchTemplateAction());
            Dispatcher.Dispatch(new FetchSshKeyAction());
            Dispatcher.Dispatch(new FetchClusterAction());
            Dispatcher.Dispatch(new FetchDatacenterAction());
            base.OnInitialized();
        }

        private void DatacenterState_StateChanged(object sender, DatacenterState e)
        {
            StateHasChanged();
        }

        private void ClusterState_StateChanged(object sender, ClusterState e)
        {
            StateHasChanged();
        }

        private void TemplateState_StateChanged(object sender, TemplateState e)
        {
            StateHasChanged();
        }

        private void SshKeyState_StateChanged(object sender, SshKeyState e)
        {
            StateHasChanged();
        }

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

        private async Task HandleOk(MouseEventArgs e)
        {
            Dispatcher.Dispatch(new CreateClusterAction(clusterCreateRequest));
            visible = false;
        }

        private void HandleCancel(MouseEventArgs e)
        {
            visible = false;
        }

        void ChangeTemplate(int value)
        {
            if (value != -1)
            {
                var template = TemplateState.Value.Templates.FirstOrDefault(t => t.Id == value);

                if (template != null)
                {
                    clusterCreateRequest.Cpu = template.CpuCount;
                    clusterCreateRequest.Memory = template.MemoryCount;
                    clusterCreateRequest.Storage = template.DiskSpace;
                    StateHasChanged();
                }
            }
        }

        private double GetRamUsedWidth()
        {
            var node = DatacenterState.Value.Nodes.FirstOrDefault(f => f.Id == clusterCreateRequest.DeployNodeId);
            if (node != null)
            {
                return ((((double)node.RamUsed / (1024 * 1024)) / ((double)node.RamTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private double GetRamToClaimWidth()
        {
            var node = DatacenterState.Value.Nodes.FirstOrDefault(f => f.Id == clusterCreateRequest.DeployNodeId);
            if (node != null)
            {
                return (((double)(clusterCreateRequest.Memory * (clusterCreateRequest.Node + 1)) / ((double)node.RamTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private bool Overheat()
        {
            return (GetRamUsedWidth() + GetRamToClaimWidth()) > 100;
        }

        protected async Task DownloadKubeConfig(string id)
        {
            var download = await ClusterService.DownloadConfigAsync(id);
            await FileUtil.SaveAs(JSRuntime, download.Name, System.Text.Encoding.UTF8.GetBytes(download.Content));
        }
    }
}
