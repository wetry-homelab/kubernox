using AntDesign;
using AntDesign.JsInterop;
using Bunit;
using Fluxor;
using Kubernox.UI.Store.States;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kubernox.UI.Tests
{
    public class DatacenterPageIndexTests
    {
        [Fact]
        public void IndexPageRendersCorrectly()
        {
            using var ctx = new TestContext();
            var stateMock = new Mock<IState<DatacenterState>>();
            stateMock.Setup(s => s.Value)
                    .Returns(new DatacenterState(null, null, true, string.Empty));
            var dispatcherMock = new Mock<IDispatcher>();
            var actionSubscriberMock = new Mock<IActionSubscriber>();
            var domEventServiceMock = new Mock<IComponentIdGenerator>();

            ctx.Services.AddSingleton(stateMock.Object);
            ctx.Services.AddSingleton(dispatcherMock.Object);
            ctx.Services.AddSingleton(actionSubscriberMock.Object);
            ctx.Services.AddSingleton(domEventServiceMock.Object);
            var cut = ctx.RenderComponent<Pages.Datacenter.Index>();
        }
    }
}
