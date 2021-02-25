using AntDesign;
using AntDesign.JsInterop;
using Bunit;
using Fluxor;
using Kubernox.UI.Store.States;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;
using Kubernox.UI.Services.Interfaces;
using Microsoft.Extensions.Localization;
using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Tests
{
    public class TableComponentsTests
    {
        [Theory]
        [InlineData(0, "TestSshKey", "00:00:00:00:00:00:00:00:00:01")]
        public void SshTableRendersCorrectly(int id, string name, string fingerprint)
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var stateMock = new Mock<IState<SshKeyState>>();
            stateMock.Setup(s => s.Value)
                    .Returns(new SshKeyState(new SshKeyResponse[] {
                        new SshKeyResponse()
                        {
                            Id = id,
                            Name = name,
                            Fingerprint = fingerprint
                        }
                    }, true, string.Empty));
            var dispatcherMock = new Mock<IDispatcher>();
            var actionSubscriberMock = new Mock<IActionSubscriber>();
            var componentIdGeneratorMock = new Mock<IComponentIdGenerator>();
            var sshKeyServiceMock = new Mock<ISshKeyService>();
            var translatorMock = new Mock<IStringLocalizer<App>>();

            ctx.Services.AddSingleton(stateMock.Object);
            ctx.Services.AddSingleton(dispatcherMock.Object);
            ctx.Services.AddSingleton(actionSubscriberMock.Object);
            ctx.Services.AddSingleton(new DomEventService(ctx.JSInterop.JSRuntime));
            ctx.Services.AddSingleton(new IconService(ctx.JSInterop.JSRuntime));
            ctx.Services.AddSingleton(sshKeyServiceMock.Object);
            ctx.Services.AddSingleton(translatorMock.Object);
            ctx.Services.AddSingleton(componentIdGeneratorMock.Object);
            var cut = ctx.RenderComponent<Components.Tables.SshKeyTable>();

            var tableElementRendered = cut.Find("table");

            Assert.Contains(name, tableElementRendered.InnerHtml);
            Assert.Contains(fingerprint, tableElementRendered.InnerHtml);
        }
    }
}
