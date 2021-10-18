using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.UI.Core
{
    public partial class CoreComponent : Fluxor.Blazor.Web.Components.FluxorComponent
    {
        [Inject]
        protected IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IStringLocalizer<App> Translator { get; set; }
    }
}
