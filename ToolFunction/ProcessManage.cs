using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace MessagePlatform
{
    public class ProcessManage
    {
        private string m_strPath = string.Empty;
        private string _strTemp = string.Empty;
        public static ProcessManage pm = new ProcessManage(@"E:\项目代码\售后服务部\源码\数据对接平台代码\DataExport\DataExport\bin\Debug\alert.txt");
        public ProcessManage(string p_strPath)
        {
            m_strPath = p_strPath;
        }
        //读操作可执行标记，可以防止死锁的发生
        private bool readFlag = false;

        public void Read()
        {
            Console.WriteLine("read");
            //锁定后，其它读操作等待这一次读操作完成
            lock (this)
            {
                //第一次之行为flase，进入等待
                if (!readFlag)
                {
                    try
                    {
                        //进入等待读，另一个线程写
                        Monitor.Wait(this);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                ReadLine();
                //Console.WriteLine("消费（获取）: {0}", numberOfCounter);

                //重置，消费已经完成
                readFlag = false;
                Monitor.Pulse(this);
            }
        }

        public void ReadLine()
        {
            using (StreamReader sr = new StreamReader(m_strPath))
            {
                string _strLine = "";
                try
                {
                    _strLine = sr.ReadLine();
                }
                catch (Exception exp)
                {
                    Thread.Sleep(100);
                }
                Console.WriteLine("_strTemp:" + _strTemp + "---" + "_strLine:" + _strLine);
                if (_strTemp != _strLine && "" != _strLine)
                {
                    _strTemp = _strLine;
                    Console.WriteLine(_strLine);
                }
            }
        }

        public void Write(string p_strMess)
        {
            Console.WriteLine("write");
            //锁定后，其它写操作等待这一次写操作完成
            lock (this)
            {
                //第一次readFlag为flase,跳过执行下边的写
                //如果当前正在读，等待读操作执行Monitor.Pulse
                if (readFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                //Console.WriteLine("生产（更新）: {0}", numberOfCounter);
                WriteAlter(p_strMess);
                //重置，生产已经完成
                readFlag = true;

                //同步通过等待Pulse来完成
                Monitor.Pulse(this);
            }
        }

        public void WriteAlter(string p_strMess)
        {
            using (StreamWriter sw = new StreamWriter(m_strPath, false))
            {
                sw.WriteLine(p_strMess);
            }
        }
    }
}
