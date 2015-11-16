using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DataExport
{
    public class Win32API
    {

        #region INI�ļ�����

        /* 
     * ���INI�ļ���API�������������еĽڵ㣨Section)������KEY���������ִ�Сд 
     * ���ָ����INI�ļ������ڣ����Զ��������ļ��� 
     *  
     * CharSet�����ʱ��ʹ����ʲô���ͣ���ʹ����ط���ʱ����Ҫʹ����Ӧ������ 
     *      ���� GetPrivateProfileSectionNames����ΪCharSet.Auto,��ô��Ӧ��ʹ�� Marshal.PtrToStringAuto����ȡ������� 
     *      ���ʹ�õ���CharSet.Ansi����Ӧ��ʹ��Marshal.PtrToStringAnsi����ȡ���� 
     *       
     */

        #region API����

        /// <summary>  
        /// ��ȡ���нڵ�����(Section)  
        /// </summary>  
        /// <param name="lpszReturnBuffer">��Žڵ����Ƶ��ڴ��ַ,ÿ���ڵ�֮����\0�ָ�</param>  
        /// <param name="nSize">�ڴ��С(characters)</param>  
        /// <param name="lpFileName">Ini�ļ�</param>  
        /// <returns>���ݵ�ʵ�ʳ���,Ϊ0��ʾû������,ΪnSize-2��ʾ�ڴ��С����</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>  
        /// ��ȡĳ��ָ���ڵ�(Section)������KEY��Value  
        /// </summary>  
        /// <param name="lpAppName">�ڵ�����</param>  
        /// <param name="lpReturnedString">����ֵ���ڴ��ַ,ÿ��֮����\0�ָ�</param>  
        /// <param name="nSize">�ڴ��С(characters)</param>  
        /// <param name="lpFileName">Ini�ļ�</param>  
        /// <returns>���ݵ�ʵ�ʳ���,Ϊ0��ʾû������,ΪnSize-2��ʾ�ڴ��С����</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// ��ȡINI�ļ���ָ����Key��ֵ  
        /// </summary>  
        /// <param name="lpAppName">�ڵ����ơ����Ϊnull,���ȡINI�����нڵ�����,ÿ���ڵ�����֮����\0�ָ�</param>  
        /// <param name="lpKeyName">Key���ơ����Ϊnull,���ȡINI��ָ���ڵ��е�����KEY,ÿ��KEY֮����\0�ָ�</param>  
        /// <param name="lpDefault">��ȡʧ��ʱ��Ĭ��ֵ</param>  
        /// <param name="lpReturnedString">��ȡ�����ݻ���������ȡ֮�󣬶���ĵط�ʹ��\0���</param>  
        /// <param name="nSize">���ݻ������ĳ���</param>  
        /// <param name="lpFileName">INI�ļ���</param>  
        /// <returns>ʵ�ʶ�ȡ���ĳ���</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        //��һ��������ʽ,ʹ�� StringBuilder ��Ϊ���������͵�ȱ���ǲ��ܽ���\0�ַ����Ὣ\0�������ַ��ض�,  
        //���Զ���lpAppName��lpKeyNameΪnull������Ͳ�����  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        //��һ��������ʹ��string��Ϊ������������ͬchar[]  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// ��ָ���ļ�ֵ��д��ָ���Ľڵ㣬����Ѿ��������滻��  
        /// </summary>  
        /// <param name="lpAppName">�ڵ㣬��������ڴ˽ڵ㣬�򴴽��˽ڵ�</param>  
        /// <param name="lpString">Item��ֵ�ԣ������\0�ָ�,����key1=value1\0key2=value2  
        /// <para>���Ϊstring.Empty����ɾ��ָ���ڵ��µ��������ݣ������ڵ�</para>  
        /// <para>���Ϊnull����ɾ��ָ���ڵ��µ��������ݣ�����ɾ���ýڵ�</para>  
        /// </param>  
        /// <param name="lpFileName">INI�ļ�</param>  
        /// <returns>�Ƿ�ɹ�д��</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]     //����û�д���  
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        /// <summary>  
        /// ��ָ���ļ���ֵд��ָ���Ľڵ㣬����Ѿ��������滻  
        /// </summary>  
        /// <param name="lpAppName">�ڵ�����</param>  
        /// <param name="lpKeyName">�����ơ����Ϊnull����ɾ��ָ���Ľڵ㼰�����е���Ŀ</param>  
        /// <param name="lpString">ֵ���ݡ����Ϊnull����ɾ��ָ���ڵ���ָ���ļ���</param>  
        /// <param name="lpFileName">INI�ļ�</param>  
        /// <returns>�����Ƿ�ɹ�</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region ��װ

        /// <summary>  
        /// ��ȡINI�ļ���ָ��INI�ļ��е����нڵ�����(Section)  
        /// </summary>  
        /// <param name="iniFile">Ini�ļ�</param>  
        /// <returns>���нڵ�,û�����ݷ���string[0]</returns>  
        public static string[] INIGetAllSectionNames(string iniFile)
        {
            uint MAX_BUFFER = 32767;    //Ĭ��Ϊ32767  

            string[] sections = new string[0];      //����ֵ  

            //�����ڴ�  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = Win32API.GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFile);
            if (bytesReturned != 0)
            {
                //��ȡָ���ڴ������  
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();

                //ÿ���ڵ�֮����\0�ָ�,ĩβ��һ��\0  
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //�ͷ��ڴ�  
            Marshal.FreeCoTaskMem(pReturnedString);

            return sections;
        }

        /// <summary>  
        /// ��ȡINI�ļ���ָ���ڵ�(Section)�е�������Ŀ(key=value��ʽ)  
        /// </summary>  
        /// <param name="iniFile">Ini�ļ�</param>  
        /// <param name="section">�ڵ�����</param>  
        /// <returns>ָ���ڵ��е�������Ŀ,û�����ݷ���string[0]</returns>  
        public static string[] INIGetAllItems(string iniFile, string section)
        {
            //����ֵ��ʽΪ key=value,���� Color=Red  
            uint MAX_BUFFER = 32767;    //Ĭ��Ϊ32767  

            string[] items = new string[0];      //����ֵ  

            //�����ڴ�  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = Win32API.GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, iniFile);

            if (!(bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {

                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnedString);     //�ͷ��ڴ�  

            return items;
        }

        /// <summary>  
        /// ��ȡINI�ļ���ָ���ڵ�(Section)�е�������Ŀ��Key�б�  
        /// </summary>  
        /// <param name="iniFile">Ini�ļ�</param>  
        /// <param name="section">�ڵ�����</param>  
        /// <returns>���û������,����string[0]</returns>  
        public static string[] INIGetAllItemKeys(string iniFile, string section)
        {
            string[] value = new string[0];
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            char[] chars = new char[SIZE];
            uint bytesReturned = Win32API.GetPrivateProfileString(section, null, null, chars, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            chars = null;

            return value;
        }

        /// <summary>  
        /// ��ȡINI�ļ���ָ��KEY���ַ�����ֵ  
        /// </summary>  
        /// <param name="iniFile">Ini�ļ�</param>  
        /// <param name="section">�ڵ�����</param>  
        /// <param name="key">������</param>  
        /// <param name="defaultValue">���û��KEY��ʹ�õ�Ĭ��ֵ</param>  
        /// <returns>��ȡ����ֵ</returns>  
        public static string INIGetStringValue(string iniFile, string section, string key, string defaultValue)
        {
            string value = defaultValue;
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("����ָ��������(key)", "key");
            }

            StringBuilder sb = new StringBuilder(SIZE);
            uint bytesReturned = Win32API.GetPrivateProfileString(section, key, defaultValue, sb, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = sb.ToString();
            }
            sb = null;

            return value;
        }

        /// <summary>  
        /// ��INI�ļ��У���ָ���ļ�ֵ��д��ָ���Ľڵ㣬����Ѿ��������滻  
        /// </summary>  
        /// <param name="iniFile">INI�ļ�</param>  
        /// <param name="section">�ڵ㣬��������ڴ˽ڵ㣬�򴴽��˽ڵ�</param>  
        /// <param name="items">��ֵ�ԣ������\0�ָ�,����key1=value1\0key2=value2</param>  
        /// <returns></returns>  
        public static bool INIWriteItems(string iniFile, string section, string items)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            if (string.IsNullOrEmpty(items))
            {
                throw new ArgumentException("����ָ����ֵ��", "items");
            }

            return Win32API.WritePrivateProfileSection(section, items, iniFile);
        }

        /// <summary>  
        /// ��INI�ļ��У�ָ���ڵ�д��ָ���ļ���ֵ������Ѿ����ڣ����滻�����û���򴴽���  
        /// </summary>  
        /// <param name="iniFile">INI�ļ�</param>  
        /// <param name="section">�ڵ�</param>  
        /// <param name="key">��</param>  
        /// <param name="value">ֵ</param>  
        /// <returns>�����Ƿ�ɹ�</returns>  
        public static bool INIWriteValue(string iniFile, string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("����ָ��������", "key");
            }

            if (value == null)
            {
                throw new ArgumentException("ֵ����Ϊnull", "value");
            }

            return Win32API.WritePrivateProfileString(section, key, value, iniFile);

        }

        /// <summary>  
        /// ��INI�ļ��У�ɾ��ָ���ڵ��е�ָ���ļ���  
        /// </summary>  
        /// <param name="iniFile">INI�ļ�</param>  
        /// <param name="section">�ڵ�</param>  
        /// <param name="key">��</param>  
        /// <returns>�����Ƿ�ɹ�</returns>  
        public static bool INIDeleteKey(string iniFile, string section, string key)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("����ָ��������", "key");
            }

            return Win32API.WritePrivateProfileString(section, key, null, iniFile);
        }

        /// <summary>  
        /// ��INI�ļ��У�ɾ��ָ���Ľڵ㡣  
        /// </summary>  
        /// <param name="iniFile">INI�ļ�</param>  
        /// <param name="section">�ڵ�</param>  
        /// <returns>�����Ƿ�ɹ�</returns>  
        public static bool INIDeleteSection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            return Win32API.WritePrivateProfileString(section, null, null, iniFile);
        }

        /// <summary>  
        /// ��INI�ļ��У�ɾ��ָ���ڵ��е��������ݡ�  
        /// </summary>  
        /// <param name="iniFile">INI�ļ�</param>  
        /// <param name="section">�ڵ�</param>  
        /// <returns>�����Ƿ�ɹ�</returns>  
        public static bool INIEmptySection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("����ָ���ڵ�����", "section");
            }

            return Win32API.WritePrivateProfileSection(section, string.Empty, iniFile);
        }

        #endregion

        #endregion  
    }
}
