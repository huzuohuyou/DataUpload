using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace ToolFunction
{
    public class RemoteMessage
    {
        public static IMessage obj = null;
        /// <summary>
        /// 初始化服务器
        /// </summary>
        public static void InitServer()
        {
            TcpChannel channel = new TcpChannel(8085);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ConsoleMessage), "ConsoleMessage", WellKnownObjectMode.SingleCall);

            Console.WriteLine("Server:Press Enter key to exit");
            Console.ReadLine();

        }

        /// <summary>
        /// 初始化客户端
        /// </summary>
        public static void InitClient()
        {
            try
            {
                if (obj == null)
                {
                    TcpChannel channel = new TcpChannel();
                    ChannelServices.RegisterChannel(channel, false);
                    IMessage _obj = (IMessage)Activator.GetObject(typeof(IMessage), "tcp://localhost:8085/ConsoleMessage");
                    obj = _obj;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="p_strMess"></param>
        public static void SendMessage(string p_strMess)
        {
            try
            {
                if (obj == null)
                {
                    Console.WriteLine("Could not locate TCP server");
                }
                obj.ShowMess(p_strMess);
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError(ex.ToString());
            }
        }
    }
}
