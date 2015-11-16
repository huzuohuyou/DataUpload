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
        /// 将字符串转换为不同数据库的日期类型
        /// </summary>
        /// <param name="strDate">需要转换的字符串</param>
        /// <param name="bDate">true表示位日期型（不带小时分钟的 YYYY-MM-DD），false日期时间类型（带有消失分钟 YYYY-MM-DD HH24:MI:SS）</param>
        /// <param name="strDBType">数据库类型</param>
        /// <returns>转换过后的字符串</returns>
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
        /// 从app.config 文件中获取数据库的类型；
        /// </summary>
        /// <param name="strConnectKey">key的名称</param>
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
        /// 将字符串转换为不同数据库的日期类型
        /// </summary>
        /// <param name="strDate">需要转换的字符串</param>
        /// <param name="bDate">true表示位日期型（不带小时分钟的 YYYY-MM-DD），false日期时间类型（带有消失分钟 YYYY-MM-DD HH24:MI:SS）</param>
        /// <param name="strDBType">数据库类型</param>
        /// <param name="dataType">数据区分是字符串类型，datatime类型，还是数字类型</param>
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
                    result = ToDate(dtDate.ToString(), bDate, strDBType) + ",";//SqlServer打他有引号
                }
                else
                {
                    result = ToDate("1900-01-01", bDate, strDBType) + ",";//SqlServer打他有引号
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
