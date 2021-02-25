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
using Xunit.Extensions;

namespace Kubernox.UI.Tests
{
    public class TableComponentsTests
    {

        public static SshKeyResponse[] SshKeysData
        {
            get
            {
                return new SshKeyResponse[]
                {
                    new SshKeyResponse()
                    {
                        Id = 0,
                        Name = "TestKey01",
                        Fingerprint = "00:00:00:00:00:00:00:00:00:01"
                    },
                    new SshKeyResponse()
                    {
                        Id = 0,
                        Name = "TestKey02",
                        Fingerprint = "00:00:00:00:00:00:00:00:00:02"
                    }
                };
            }
        }

        public TestContext MakeSut()
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var dispatcherMock = new Mock<IDispatcher>();
            var actionSubscriberMock = new Mock<IActionSubscriber>();
            var componentIdGeneratorMock = new Mock<IComponentIdGenerator>();
            var sshKeyServiceMock = new Mock<ISshKeyService>();
            var translatorMock = new Mock<IStringLocalizer<App>>();

            ctx.Services.AddSingleton(dispatcherMock.Object);
            ctx.Services.AddSingleton(actionSubscriberMock.Object);
            ctx.Services.AddSingleton(new DomEventService(ctx.JSInterop.JSRuntime));
            ctx.Services.AddSingleton(new IconService(ctx.JSInterop.JSRuntime));
            ctx.Services.AddSingleton(sshKeyServiceMock.Object);
            ctx.Services.AddSingleton(translatorMock.Object);
            ctx.Services.AddSingleton(componentIdGeneratorMock.Object);

            return ctx;
        }

        [Theory]
        [InlineData(0, "TestSshKey", "00:00:00:00:00:00:00:00:00:01")]
        [InlineData(1, "TestSshKey02", "00:00:00:00:00:00:00:00:00:02")]
        public void SshTableRendersCorrectly(int id, string name, string fingerprint)
        {
            using var ctx = MakeSut();

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


            ctx.Services.AddSingleton(stateMock.Object);
            var cut = ctx.RenderComponent<Components.Tables.SshKeyTable>();

            var tableElementRendered = cut.Find("table");

            Assert.Contains(name, tableElementRendered.InnerHtml);
            Assert.Contains(fingerprint, tableElementRendered.InnerHtml);
        }

        [Fact]
        public void SshTableRenderCorrectlyMultipleKeys()
        {
            using var ctx = MakeSut();

            var stateMock = new Mock<IState<SshKeyState>>();
            stateMock.Setup(s => s.Value)
                    .Returns(new SshKeyState(SshKeysData, true, string.Empty));

            ctx.Services.AddSingleton(stateMock.Object);
            var cut = ctx.RenderComponent<Components.Tables.SshKeyTable>();
            var tableElementRendered = cut.Find("table");

            foreach (var key in SshKeysData)
            {
                Assert.Contains(key.Name, tableElementRendered.InnerHtml);
                Assert.Contains(key.Fingerprint, tableElementRendered.InnerHtml);
            }
        }
    }
}
