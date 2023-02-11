using CsharpHardware.Properties;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace CsharpHardware
{
    internal class WebUtil
    {
        public static void StartWebSocketServer(int port)
        {
            var service = new HttpService();
            var config = new TouchSocketConfig();
            config.UsePlugin();
            config.SetListenIPHosts(new[] { new IPHost(port) });
            config.ConfigureContainer(a =>
            {
                a.SetSingletonLogger<ConsoleLogger>();
            });
            config.ConfigurePlugins(a =>
            {
                a.Add<WebSocketServerPlugin>()
                    .SetWSUrl("/CsharpMaster")
                    .SetCallback(WSCallback);
                a.Add<MyWebSocketPlugin>();
                a.Add<CheckClearPlugin>().SetDuration(TimeSpan.FromMinutes(5));
            });
            service.Setup(config).Start();
            Console.WriteLine($"websocket服务器已启动,ws://localhost:{port}/CsharpMaster");
        }

        private static void WSCallback(ITcpClientBase client, WSDataFrameEventArgs e)
        {
            switch (e.DataFrame.Opcode)
            {
                case WSDataType.Cont:
                    Console.WriteLine("WSDataType.Cont");
                    break;

                case WSDataType.Text:
                    string wsData = e.DataFrame.ToText();
                    Console.WriteLine($"接收到websocket数据：{wsData}");
                    if (wsData.StartsWith("printSource"))
                    {
                        FormPrintPreview.ShowDialog(client, wsData);
                    }
                    else if (wsData.StartsWith("printerList"))
                    {
                        FormPrintPreview.GetPrinters(client);
                    }
                    else if (wsData.StartsWith("paperList"))
                    {
                        FormPrintPreview.GetPapers(client, wsData);
                    }
                    else
                    {
                        throw new ApplicationException($"wrong:{wsData}");
                    }
                    break;

                case WSDataType.Close:
                    Console.WriteLine("WSDataType.Close");
                    client.Close("WSDataType.Close");
                    break;

                case WSDataType.Ping:
                    break;

                case WSDataType.Pong:
                    break;

                default:
                    break;
            }
        }

        private class MyWebSocketPlugin : WebSocketPluginBase
        {
        }

        public static void StartHttpServer(int port)
        {
            var service = new HttpService();
            var config = new TouchSocketConfig();
            config.UsePlugin()
                .SetListenIPHosts(new IPHost[] { new IPHost(port) })
                .ConfigureContainer(a =>
                {
                    a.SetSingletonLogger<ConsoleLogger>();
                })
                .ConfigurePlugins(a =>
                {
                    a.Add<MyHttpPlugin>();
                });
            service.Setup(config).Start();
            Console.WriteLine($"Http服务器已启动,http://localhost:{port}");
        }

        private class MyHttpPlugin : HttpPluginBase
        {
            protected override void OnGet(ITcpClientBase client, HttpContextEventArgs e)
            {
                Console.WriteLine($"接收到http请求：{e.Context.Request}");
                var content = Resources.test_hardware;
                var bytes = content.ToUTF8Bytes();
                e.Context.Response.ContentType = "text/html;charset=utf-8";
                e.Context.Response.ContentLength = bytes.Length;
                e.Context.Response.WriteContent(bytes, 0, bytes.Length);
            }
        }
    }
}