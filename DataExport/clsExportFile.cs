using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace DataExport
{
    public class clsExportFile
    {
        [DllImport("EMRAssist.dll")]
        private static extern bool SaveFileToFieldElem(string strOpenFileName, string strSaveFileName, int nDocumentMode);

        [DllImport("EMRAssist.dll")]
        private static extern bool GetXmlNodeContent(string strOpenFileName, int nDocumentMode, int nType, string strFindText, short nLayerNo, short nFindType, [Out, MarshalAs(UnmanagedType.LPArray)]char[] strBuff, int nBufSize);//
        // ����PATIENT_ID �����ļ�·��
        //�����ļ�����Ŀ¼
        public static string genMrPaths(string strPatientID)
        {
            string strPath, strTemp;
            if (strPatientID.Length < 2)
            {
                strPath = strPatientID;
                strTemp = "";
            }
            else
            {
                if (strPatientID.Length <= 8)
                {
                    strPath = strPatientID.Substring(strPatientID.Length - 2, 2); //ȡĩβ��λ
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 2);  //ȡʣ�ಿ��
                }
                else
                {
                    strPath = strPatientID.Substring(strPatientID.Length - 4, 4); //ȡĩβ��λ
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 4);  //ȡʣ�ಿ��
                }
            }

            if (strPath.Length == 1)
                strPath = "0" + strPath;
            strPath = strPath + "\\";

            string strPrefix = "00000000000000";

            if (strTemp.Length <= 8)
                strTemp = strPrefix.Substring(0, 8 - strTemp.Length) + strTemp;
            else
                strTemp = strPrefix.Substring(0, 12 - strTemp.Length) + strTemp;

            strPath = strPath + strTemp + "\\";
            return strPath;

        }

        //�����ļ�����Ŀ¼�����Ƶ�ָ��Ŀ¼
       public static void CopyFile(string aPath, string bPath)
        {
            try
            {
                File.Copy(aPath, bPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //ͨ���ļ����ڸ�Ŀ¼  ��ȡ�ļ�����  ����κš�Ԫ�ء��꣩
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetXmlNodeStr(string strPath, string strfindtxt, short cengci_coode)
        {
            //string strRootPath = Application.StartupPath + "\\content";
            bool bReturn;
            char[] sReCheckCodeList = new char[2560];
            bReturn = GetXmlNodeContent(strPath, 0, 0, strfindtxt, cengci_coode, 1, sReCheckCodeList, 2560);
            String s = new string(sReCheckCodeList);
            return s;
        }
    }
}
