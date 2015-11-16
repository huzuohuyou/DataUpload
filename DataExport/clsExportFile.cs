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
        // 根据PATIENT_ID 生成文件路径
        //返回文件所在目录
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
                    strPath = strPatientID.Substring(strPatientID.Length - 2, 2); //取末尾两位
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 2);  //取剩余部分
                }
                else
                {
                    strPath = strPatientID.Substring(strPatientID.Length - 4, 4); //取末尾四位
                    strTemp = strPatientID.Substring(0, strPatientID.Length - 4);  //取剩余部分
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

        //根据文件所在目录，复制到指定目录
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

        //通过文件所在根目录  获取文件内容  （层次号、元素、宏）
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
