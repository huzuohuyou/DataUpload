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
        public static ProcessManage pm = new ProcessManage(@"E:\��Ŀ����\�ۺ����\Դ��\���ݶԽ�ƽ̨����\DataExport\DataExport\bin\Debug\alert.txt");
        public ProcessManage(string p_strPath)
        {
            m_strPath = p_strPath;
        }
        //��������ִ�б�ǣ����Է�ֹ�����ķ���
        private bool readFlag = false;

        public void Read()
        {
            Console.WriteLine("read");
            //�����������������ȴ���һ�ζ��������
            lock (this)
            {
                //��һ��֮��Ϊflase������ȴ�
                if (!readFlag)
                {
                    try
                    {
                        //����ȴ�������һ���߳�д
                        Monitor.Wait(this);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                ReadLine();
                //Console.WriteLine("���ѣ���ȡ��: {0}", numberOfCounter);

                //���ã������Ѿ����
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
            //����������д�����ȴ���һ��д�������
            lock (this)
            {
                //��һ��readFlagΪflase,����ִ���±ߵ�д
                //�����ǰ���ڶ����ȴ�������ִ��Monitor.Pulse
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
                //Console.WriteLine("���������£�: {0}", numberOfCounter);
                WriteAlter(p_strMess);
                //���ã������Ѿ����
                readFlag = true;

                //ͬ��ͨ���ȴ�Pulse�����
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
