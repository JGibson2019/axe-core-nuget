#nullable enable

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Deque.AxeCore.Playwright.Test
{
    /// <summary>
    /// Server for hosting test files for integration testing.
    /// </summary>
    public sealed class TestServer
    {
        private readonly IWebHost m_webHost;

        private const int Port = 3000;

        private static readonly IPAddress s_ipAddress = IPAddress.Loopback;

        public static readonly Uri BaseUri = new($"http://{s_ipAddress}:{Port}");

        public TestServer()
        {
            m_webHost = WebHost.CreateDefaultBuilder()
                .UseWebRoot("TestFiles")
                .Configure(app =>
                {
                    app.UseStaticFiles();
                })
                .UseKestrel(options =>
                {
                    options.Listen(s_ipAddress, Port);
                })
                .Build();
        }

        public Task StartAsync() => m_webHost.StartAsync();

        public Task StopAsync() => m_webHost.StopAsync();
    }
}
