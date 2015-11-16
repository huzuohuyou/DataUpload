using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToolFunction
{
    public class ByteConvertHelper
    {
        /// <summary>
        /// ������ת��Ϊbyte����
        /// </summary>
        /// <param name="obj">��ת������</param>
        /// <returns>ת����byte����</returns>
        public static byte[] Object2Bytes(object obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// ��byte����ת���ɶ���
        /// </summary>
        /// <param name="buff">��ת��byte����</param>
        /// <returns>ת����ɺ�Ķ���</returns>
        public static object Bytes2Object(byte[] buff)
        {
            object obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                ms.Position = 0;
                IFormatter iFormatter = new BinaryFormatter();
                try
                {
                    obj = iFormatter.Deserialize(ms);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return obj;
        }
    }
}
