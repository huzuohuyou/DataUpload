using System;
using System.Collections.Generic;
using System.Text;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public class FixDateTime
    {

        private static string ToOracleDate(string strDate, bool bDate)
        {
            string strReturn = "";
           
                if (bDate)
                    strReturn = "TO_DATE(" + strDate + ",'YYYY-MM-DD')";
                else
                    strReturn = "TO_DATE(" + strDate + ",'YYYY-MM-DD HH24:MI:SS')";
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
            }
            return result;

        }

        private static string ToSQLServerDate(string strDate)
        {
            string strReturn = "";
            strReturn = " CONVERT(datetime,' " + strDate + "') ";
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
            if (dataType.ToUpper() == "DATETIME")
            {
                DateTime dtDate = DateTime.Now;
                if (DateTime.TryParse(strValue, out dtDate))
                {
                    result = ToDate(dtDate.ToString(), bDate, strDBType) + ",";//SqlServer����������
                }
                else
                {
                    result = ToDate("1900-01-01", bDate, strDBType) + ",";//SqlServer����������
                }
            }
            else if (dataType.ToUpper() == "STRING")
            {
                if (strValue.ToUpper() != "NULL" && strValue.Trim() != "" && strValue.Trim() != "��" && strValue.Trim() != "-")
                {
                    result = "'" + strValue + "',";
                }
                else
                {
                    result = "NULL,";
                }
            }
            else if (dataType.ToUpper() == "DECIMAL")//Decimal
            {
                double iValue = 0.00;
                if (double.TryParse(strValue, out iValue))
                {
                    result = iValue + ",";
                }
                else
                {
                    result = "NULL,";
                }

            }
            else
            {
                result = "NULL,";
            }
            return result;
        }

    }
}
