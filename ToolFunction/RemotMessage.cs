using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace ToolFunction
{
    public interface IMessage
    {
        String getMess(String message);

    }

    public class RemotMessage : IMessage, System.MarshalByRefObject
    {
        #region IMessage ≥…‘±

        public string getMess(string message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public void InitServer()
        {
            TcpChannel channel = new TcpChannel(9999);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingObjects.Person), "RemotingMessageService", WellKnownObjectMode.SingleCall);
        }

        public void SendMessage(string p_strMess)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);
            IMessage obj = (IMessage)Activator.GetObject(typeof(RemotingObjects.IPerson), "tcp://localhost:9999/RemotingMessageService");
            String name = Console.ReadLine();
            obj.getMess(p_strMess);
        }
    }



}
