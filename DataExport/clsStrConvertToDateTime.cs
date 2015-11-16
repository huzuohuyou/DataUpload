using System;
using System.Collections.Generic;
using System.Text;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public class StrConvertToDateTime
    {

        private static string ToOracleDate(string strDate, bool bDate)
        {
            string strReturn = "";
           
                if (bDate)
                    strReturn = "TO_DATE(" + strDate + ",'YYYY-MM-DD')";
                else
                    strReturn = "TO_DATE(" + strDate + ",'YYYY-MM-DD HH24:MI:SS')";
            //}
            //else
            //{
            //    if (bDate)
            //        strReturn = "TO_DATE('" + strDate + "','YYYY-MM-DD')";
            //    else
            //        strReturn = "TO_DATE('" + strDate + "','YYYY-MM-DD HH24:MI:SS')";

            //}
            return strReturn;
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��ͬ���ݿ����������
        /// </summary>
        /// <param name="strDate">��Ҫת�����ַ���</param>
        /// <param name="bDate">true��ʾλ�����ͣ�����Сʱ���ӵ� YYYY-MM-DD����false����ʱ�����ͣ�������ʧ���� YYYY-MM-DD HH24:MI:SS��</param>
        /// <param name="strDBType">���ݿ�����</param>
        /// <returns>ת��������ַ���</returns>
        public static string ToDate(string strDate, bool bDate, string strDBType)
        {
            string result = "";
            switch (strDBType)
            {
                case "ORACLE":
                    {

                        result = ToOracleDate(strDate, bDate);
                    }
                    break;
                case "SQLSERVER":
                    {

                        result = ToSQLServerDate(strDate);
                    }
                    break;
                //case "CACHE":
                //    {
                //        DBCacheFunction pDBCacheFunction = new DBCacheFunction();
                //        return StrConvertToDateTime.ToDate(strDate, bDate, bField);
                //    }
                //    break;
                case "OLEDB":
                    {
                        result = ToOracleDate(strDate, bDate);
                    }
                    break;
                case "DB2":
                    {
                        result = ToOracleDate(strDate, bDate);
                    }
                    break;

            }
            return result;

        }

        private static string ToSQLServerDate(string strDate)
        {
            string strReturn = "";

            strReturn = " CONVERT(datetime,' " + strDate + "') ";
            //if (bField)
            //    strReturn = " CONVERT(datetime, " + strDate + ") ";
            //else
            //    strReturn = " CONVERT(datetime, '" + strDate + "') ";

            return strReturn;
        }

        /// <summary>
        /// ��app.config �ļ��л�ȡ���ݿ�����ͣ�
        /// </summary>
        /// <param name="strConnectKey">key������</param>
        /// <returns></returns>
        public static string getClientConnectType(string strConnectKey)
        {
            string strConnectType = "";
            foreach (string key in DALUse.getConfig().AppSettings.Settings.AllKeys)
            {
                string value = DALUse.getConfig().AppSettings.Settings[key].Value;
                if (key.Equals(strConnectKey))
                {
                    strConnectType = value.ToUpper();
                    return strConnectType;
                }
            }
            return "";

        }

        /// <summary>
        /// ���ַ���ת��Ϊ��ͬ���ݿ����������
        /// </summary>
        /// <param name="strDate">��Ҫת�����ַ���</param>
        /// <param name="bDate">true��ʾλ�����ͣ�����Сʱ���ӵ� YYYY-MM-DD����false����ʱ�����ͣ�������ʧ���� YYYY-MM-DD HH24:MI:SS��</param>
        /// <param name="strDBType">���ݿ�����</param>
        /// <param name="dataType">�����������ַ������ͣ�datatime���ͣ�������������</param>
        /// <returns></returns>
        public static string makeInsertvalue(string strValue, bool bDate, string strDBType,string dataType)
        {
            string result = "";
            if (dataType == "DATE")
            {

                //if (strValue == "")
                //{
                //    result = "NULL,";
                //}
                //else if (strValue.ToUpper() == "NULL")
                //{
                //    result = "NULL,";
                //}
                //else 
                DateTime dtDate = DateTime.Now;
                //if (strValue != "" && strValue.ToUpper() != "NULL")
                //{
                if (DateTime.TryParse(strValue, out dtDate))
                {
                    result = ToDate(dtDate.ToString(), bDate, strDBType) + ",";//SqlServer����������
                }
                else
                {
                    result = ToDate("1900-01-01", bDate, strDBType) + ",";//SqlServer����������
                }
                //}
                //else
                //{
                //    result = "NULL,";
                //}
            }
            else if (dataType == "STRING")
            {
               
                //if (strValue.Trim() == "")
                //{
                //    result = "NULL,";
                //}
                //else if (strValue.ToUpper() == "NULL")
                //{
                //    result = "NULL,";
                //}
                //else 
                if (strValue.ToUpper() != "NULL" && strValue.Trim() != "")
                {
                    result = "'" + strValue + "',";
                    //result =  strValue + ",";
                }
                else
                {
                    result = "NULL,";
                }
            }
            else if (dataType == "NUMBER")
            {
                //if (strValue.Trim() == "")
                //{
                //    result = "NULL,";
                //}
                //else if (strValue.ToUpper() == "NULL")
                //{
                //    result = "NULL,";
                //} 
                double iValue = 0.00;
                if (double.TryParse(strValue,out iValue))
                {
                    result = iValue + ",";
                }
                else
                {
                    result = "NULL,";
                }
               
            }
            return result;
        }

    }
}
