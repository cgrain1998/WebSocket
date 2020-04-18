using Fleck;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace WebSocket.SocketServer
{
    /// <summary>
    /// Send 发送
    /// </summary>
    public class SocketSend
    {
        private delegate int Weekly(WebSocketServer socket, List<IWebSocketConnection> allSockets,
          string Markmessage,
          int Marknum,
          string ConnectsocketnotificationSection = "已连接",
          string ConnectsocketCloseSection = "连接已关闭",
          string ReturnMesSection = "数据已更新!"

          );
        private static int Marknum = 0;
        private static string Markmessage = "";

        public void Run()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configurationRoot = builder.Build();
            var ipaddresSection = configurationRoot.GetSection("ipaddres").Value;
            var sockettypeSection = configurationRoot.GetSection("sockettype").Value;
            var ipportSection = configurationRoot.GetSection("ipport").Value;
            var ConnectsocketnotificationSection = configurationRoot.GetSection("Connectsocketnotification").Value;
            var ConnectsocketCloseSection = configurationRoot.GetSection("ConnectsocketCloseSection").Value;
            var ReturnMesSection = configurationRoot.GetSection("ReturnMes").Value;
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer($"{sockettypeSection }://{ipaddresSection}:{ ipportSection}");
            try
            {
                Weekly weekly = new Weekly(SendMes);
                weekly.Invoke(server, allSockets, Markmessage, Marknum, ConnectsocketnotificationSection, ConnectsocketCloseSection, ReturnMesSection);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"错误:  { ex.Message}");
            }
            var IsExit = Console.ReadLine();
            if (!string.IsNullOrEmpty(IsExit))
            {
                Console.WriteLine("是否关闭程序(Y/N)");
                while (true)
                {
                    IsExit = Console.ReadLine();
                    if (IsExit != "N")
                    {
                        return;
                    }
                }

            }
        }


        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="allSockets"></param>
        /// <param name="Markmessage"></param>
        /// <param name="Marknum"></param>
        /// <param name="ConnectsocketnotificationSection"></param>
        /// <param name="ConnectsocketCloseSection"></param>
        /// <param name="ReturnMesSection"></param>
        /// <returns></returns>
        private int SendMes(WebSocketServer webSocket, List<IWebSocketConnection> allSockets,
              string Markmessage,
              int Marknum,
              string ConnectsocketnotificationSection = "",
              string ConnectsocketCloseSection = "",
              string ReturnMesSection = ""
              )
        {
            webSocket.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine(ConnectsocketnotificationSection);
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine(ConnectsocketCloseSection);
                    allSockets.Remove(socket);

                };
                socket.OnMessage = message =>
                {
                    Markmessage = message;
                    Console.WriteLine(Markmessage);
                    //message 然后开启查询
                    allSockets.ToList().ForEach(s => s.Send($"Echo: {message}"));

                };
            });
            while (true)
            {
                Thread.Sleep(100);
                if (Markmessage == "OK")
                {
                    //对返回值进行分析
                    Marknum = ReceiveMes();
                    //  Console.WriteLine("接收num" + Marknum);
                    if (Marknum == 200)
                    {
                        allSockets.ToList().ForEach(s => s.Send(string.IsNullOrEmpty(ReturnMesSection.ToString()) ? "data Upload!" : ReturnMesSection.ToString()));
                        Console.WriteLine($"收到消息=>{Markmessage} 返回前段消息=>{ReturnMesSection.ToString()}");
                        Markmessage = "";
                    }
                    else
                    {
                        allSockets.ToList().ForEach(s => s.Send(Marknum.ToString()));
                    }
                }
                Console.WriteLine($"监听前端消息=>  {Markmessage}");
            }
        }


        /// <summary>
        /// 接收消息 返回 200成功, 500 表示失败
        /// </summary>
        /// <returns></returns>
        private int ReceiveMes()
        {
            try
            {
                //前段命令
                //if (message=="")
                //{

                //}
                //数据库查询
                //查到修改 则跳出循序
                var num = 0;
                while (true)
                {
                    Thread.Sleep(100); //每秒查询一次
                    num++;
                    Console.WriteLine($"执行数据库查询第{num.ToString()}次");
                    if (num > 12)
                    {

                        break;  //跳出数据库查询
                    }
                }
                //表示查询到了数据,返回
                Console.WriteLine($"查询数据成功,正在进行返回数据操作");
                return 200;
            }
            catch (Exception ex)
            {
                //记录Log
                return 500;
            }
        }



        /// <summary>
        /// 发送消息给前端
        /// </summary>
        /// <param name="type">true 表示 开启,false 表示关闭</param>
        /// <returns></returns>
        private int ReturnMes(string message = "")
        {
            int ReceiveType;
            Console.WriteLine("Type");
            ReceiveType = ReceiveMes();
            if (ReceiveType != 0)
            {
                return ReceiveType;
            }
            return 0;

        }
    }


}
