using AntDesign.Charts;
using Infrastructure.Contracts.Response;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Style { get; set; }

        [Parameter]
        public int? Height { get; set; }

        [Parameter]
        public ChartType Type { get; set; }

        protected LineConfig chartMetricConfiguration = new LineConfig();

        IChartComponent chartRef;

        List<object> data = new List<object> { };


        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        public ClusterChart()
        {
            chartMetricConfiguration = new LineConfig
            {
                XField = "dateValue",
                YField = "value",
                Color = new[] { "#82d1de" },
                ForceFit = true,
                Height = Height,
                XAxis = new ValueCatTimeAxis
                {
                    Type = "dateTime",
                    TickCount = 5
                }
            };
        }

        protected override void OnAfterRender(bool firstRender)
        {
            foreach (var metric in Metrics)
            {
                data.Add(new
                {
                    dateValue = metric.DateValue,
                    value = (Type == ChartType.CPU) ? ((metric.CpuValue / (1024))) : ((metric.RamValue / (1024)))
                });
            }

            chartMetricConfiguration = new LineConfig
            {
                XField = "dateValue",
                YField = "value",
                Color = new[] { "#82d1de" },
                ForceFit = true,
                Height = Height,
                XAxis = new ValueCatTimeAxis
                {
                    Type = "dateTime",
                    TickCount = 5
                }
            };

            chartRef.ChangeData(data);

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
