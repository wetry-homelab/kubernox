using AntDesign.Charts;
using Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

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

        protected PercentStackedAreaConfig chartMetricConfiguration = new PercentStackedAreaConfig();

        IChartComponent chartRef;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine(JsonSerializer.Serialize(Metrics));
                chartRef.ChangeData(Metrics, true);
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        public ClusterChart()
        {
            chartMetricConfiguration = new PercentStackedAreaConfig
            {
                Title = new Title
                {
                    Visible = true,
                    Text = Title
                },
                Data = Metrics,
                XField = "DateValue",
                YField = GetYFieldValue(Type),
                Color = new[] { "#82d1de" },
                AreaStyle = new GraphicStyle
                {
                    FillOpacity = 0.7M
                }
            };
        }

        private string GetYFieldValue(ChartType type)
        {
            if (type == ChartType.CPU)
                return "CpuValue";

            return "RamValue";
        }
    }
}
