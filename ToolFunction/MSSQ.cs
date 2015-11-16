using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;

namespace ToolFunction
{
    public class MSSQ
    {
        MessageQueue queue = null;
        /// <summary>
        /// 初始化消息队列
        /// </summary>
        public void InitMSSQ(string p_strQueueName)
        {
            queue = new MessageQueue(".");
        }

        /// <summary>
        /// 创建消息队列
        /// </summary>
        public void CreateMSSQ(string p_strQueueName)
        {
            string queueName = p_strQueueName;
            if (MessageQueue.Exists(queueName))
            {
                queue = new MessageQueue(queueName);
            }
            else
            {
                queue = MessageQueue.Create(queueName, false);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="p_strMess"></param>
        public void SendMessage(string p_strMess)
        {
            queue.Send(p_strMess);
        }

        /// <summary>
        /// 一次性读取队列中所有消息
        /// </summary>
        public void GetAllMessage()
        {
            System.Messaging.Message[] messages = queue.GetAllMessages();
            foreach (System.Messaging.Message message in messages)
            {
                //Do something with the message.
            }
        }

        /// <summary>
        /// 获取消息队列的第一条，此消息在读取时删除
        /// </summary>
        public Message ReceiveMessage()
        {
            return queue.Receive();
        }

        /// <summary>
        /// Peek方法领取队列中的第一条消息；但是，它在队列中保留消息备份
        /// </summary>
        public Message PeekMessage()
        {
            return queue.Peek();
        }

        /// <summary>
        /// 生成带有级别的Message
        /// </summary>
        /// <param name="p_strPriority"></param>
        /// <returns></returns>
        public Message GetPriority(string p_strPriority)
        {
            Message queueMessage = new System.Messaging.Message();
            switch (p_strPriority)
            {
                case "AboveNormal":
                    queueMessage.Priority = MessagePriority.AboveNormal;
                    break;
                case "High":
                    queueMessage.Priority = MessagePriority.High;
                    break;
                case "Highest":
                    queueMessage.Priority = MessagePriority.Highest;
                    break;
                case "Low":
                    queueMessage.Priority = MessagePriority.Low;
                    break;
                case "Lowest":
                    queueMessage.Priority = MessagePriority.Lowest;
                    break;
                case "Normal":
                    queueMessage.Priority = MessagePriority.Normal;
                    break;
                case "VeryHigh":
                    queueMessage.Priority = MessagePriority.VeryHigh;
                    break;
                case "VeryLow":
                    queueMessage.Priority = MessagePriority.VeryLow;
                    break;
                default:
                    queueMessage.Priority = MessagePriority.Normal;
                    break;
            }
            return queueMessage;
        }

        /// <summary>
        /// 生成带有级别的消息
        /// </summary>
        /// <param name="p_strMess">消息内容</param>
        /// <param name="p_strPriority">消息级别 Highest、VeryHigh，High，AboveNormal，Normal，Low，VeryLow，Lowest/</param>
        public void SentMessageIdentityByPriority(string p_strMess, string p_strPriority)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(MessageContent));
            //Message queueMessage = GetPriority(p_strPriority);
            //MessageContent messageContent = new MessageContent(p_strMess);
            //serializer.Serialize(queueMessage.BodyStream, messageContent);
            //queue.Send(queueMessage, p_strPriority);
            Message queueMessage = GetPriority(p_strPriority);
            MessageContent messageContent = new MessageContent(p_strMess);
            queueMessage.Body = messageContent;
            queue.Send(queueMessage, "HIGH PRIORITY");
        }

        public void StartMyCmd(string p_strExePath)
        {
            Process.Start(p_strExePath);
        }

        public void StartMyCmd(string p_strExePath, string p_strFilePath)
        {
            
            //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(StartExe);
            //Thread myThread = new Thread(StartExe);
            //string[] ss = new string[2];
            //ss[0] = p_strExePath;
            //ss[1] = p_strFilePath;
            //object o = ss;
            //myThread.Start(o);
        }

        public void StartExe(object o)
        {
            string[] ss = (string[])o;
            Process.Start(ss[0].ToString(), ss[1].ToString());
        }

        public void CallMessPlatForm()
        {
            try
            {
                string _strExePath = @"E:\项目代码\售后服务部\源码\数据对接平台代码\DataExport\MessagePlatform\bin\Debug\MessagePlatform.exe";
                Process p = Process.Start(_strExePath);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
