
using System.ServiceProcess;
using WebSocket.SocketServer;

namespace WebSocket
{
    class Program
    {

        static void Main(string[] args)
        {


            try
            {
                SocketSend socketSend = new SocketSend();
                socketSend.Run();
          //      ServiceBase[] serviceBases = new ServiceBase[] { new SocketSendServer() };
          //ServiceBase.Run(serviceBases);
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
            }
        }



    }
}
