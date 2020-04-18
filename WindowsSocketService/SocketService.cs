using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsSocketService
{
    public partial class SocketService : ServiceBase
    {
        public SocketService()
        {
            InitializeComponent();
            base.ServiceName = "Socket测试";
        }

        protected override void OnStart(string[] args)
        {
            //开启服务
        }

        protected override void OnStop()
        {
            //取消服务
        }
    }
}
