using AntDesign.Charts;
using Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Kubernox.UI.Pages.K3SCluster
{
    public partial class ClusterChart : ComponentBase
    {
        public enum ChartType
        {
            CPU,
            RAM
        };

        [Parameter]
        public List<SimpleMetricItemResponse> Metrics { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public ChartType Type { get; set; }

        protected PercentStackedAreaConfig chartMetricConfiguration;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                chartMetricConfiguration = new PercentStackedAreaConfig
                {
                    Title = new Title
                    {
                        Visible = true,
                        Text = Title
                    },
                    XField = "DateValue",
                    YField = GetYFieldValue(Type),
                    Color = new[] { "#82d1de" },
                    AreaStyle = new GraphicStyle
                    {
                        FillOpacity = 0.7M
                    }
                };

                StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private string GetYFieldValue(ChartType type)
        {
            if (type == ChartType.CPU)
                return "CpuValue";

            return "RamValue";
        }
    }
}
