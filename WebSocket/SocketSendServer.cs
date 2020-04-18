using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using WebSocket.SocketServer;

namespace WebSocket
{
    partial class SocketSendServer : ServiceBase
    {
        SocketSend socketSend = new SocketSend();
        public SocketSendServer()
        {
            InitializeComponent();
            base.ServiceName = "SocketTest";
            
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            socketSend.Run();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
           // socketSend.Run();
        }
    }
}
