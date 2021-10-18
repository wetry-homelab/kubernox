using Fluxor;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kubernox.UI.Pages.Datacenter
{
    public partial class Node : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Parameter]
        public int NodeId { get; set; }

        [Inject]
        protected IState<DatacenterState> DatacenterState { get; set; }

        [Inject]
        IDispatcher Dispatcher { get; set; }

        string tabKey { get; set; } = "cpu";

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Dispatcher.Dispatch(new SelectDatacenterNodeAction(NodeId));
                DatacenterState.StateChanged += DatacenterState_StateChanged;
            }
            base.OnAfterRender(firstRender);
        }

        private double GetRamUsedWidth()
        {
            var node = DatacenterState.Value.SelectedNode;
            if (node != null)
            {
                return ((((double)node.RamUsed / (1024 * 1024)) / ((double)node.RamTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private double GetDiskUsedWidth()
        {
            var node = DatacenterState.Value.SelectedNode;
            if (node != null)
            {
                return ((((double)node.RootFsUsed / (1024 * 1024)) / ((double)node.RootFsTotal / (1024 * 1024))) * 100);
            }
            return 0;
        }

        private void DatacenterState_StateChanged(object sender, DatacenterState e)
        {
            StateHasChanged();
        }
    }
}
