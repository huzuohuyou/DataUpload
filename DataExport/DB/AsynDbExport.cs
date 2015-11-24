using System;
using System.Collections.Generic;
using System.Text;
using ToolFunction;
using System.Data;
using System.Configuration;

namespace DataExport
{
    public class AsynDbExport
    {
        public static EmrInfo ei = new EmrInfo();
        public string m_strTableName = string.Empty;
        public string m_strColumns = string.Empty;
        public string m_strValues = string.Empty;
        public DataSet m_dsSource = null;
        public static string m_strDbType = ConfigurationManager.AppSettings["DBTYPE"].ToUpper();
        public static string m_strTargetDBType = ConfigurationManager.AppSettings["TARGETDBTYPE"].ToUpper();
        public System.Data.DataTable m_dtSource = null;
        public System.Data.DataTable m_dtColumns = null;

        public AsynDbExport(string p_strTableName, DataSet p_dsSource)
        {
            m_strTableName = p_strTableName;
            m_dsSource = p_dsSource;
        }

        /// <summary>
        /// 通过名字获取m_dsSource中指定名称datatable
        /// 并赋值给m_dtSource
        /// </summary>
        public System.Data.DataTable GetTableByName(string p_strTableName)
        {
            string m_strTableName = p_strTableName;
            DataTable m_dtSource = null;
            foreach (DataTable dtTemp in m_dsSource.Tables)
            {
                if (dtTemp.TableName.Contains(m_strTableName))
                {
                    m_dtSource = dtTemp;
                }
            }
            this.m_dtSource = m_dtSource;
            return m_dtSource;
        }

        public DataTable GetTargetTable(string p_strTableName)
        {
            string m_strSQL = "select * from " + p_strTableName + " where 1=0 ";
            DataTable m_dtColumns = CommonFunction.OleExecuteBySQL(m_strSQL, "", "TARGET");
            this.m_dtColumns = m_dtColumns;
            return m_dtColumns;
        }

        /// <summary>
        /// 构造生成插入表 的列
        /// 并赋值给m_strColumns
        /// </summary>
        /// <returns></returns>
        public string CombineColumns(string p_strTableName)
        {
            string m_strTableName = p_strTableName;
            string m_strColumns = string.Empty;
            RemoteMessage.SendMessage("正在拼接[" + m_strTableName + "]的列");
            DataTable m_dtColumns = GetTargetTable(p_strTableName);
            foreach (DataColumn drColumnName in m_dtColumns.Columns)//拼接插入的列
            {
                m_strColumns += drColumnName.ToString() + ",";
            }
            if ("SQLSERVER" == m_strDbType)
            {
                m_strColumns = m_strColumns.Trim(',');
                m_strColumns = m_strColumns.Remove(0, 4);
            }
            else
            {
                m_strColumns = m_strColumns.Trim(',');
            }
            this.m_strColumns = m_strColumns;
            return m_strColumns;
            //RemoteMessage.SendMessage("拼接完成[" + m_strColumns + "]");
        }

        /// <summary>
        /// 构造生成插入表 的列
        /// 并赋值给m_strValues
        /// </summary>
        /// <returns></returns>
        public string CombineValues(DataRow p_drSource)
        {
            string m_strValues = string.Empty;
            //RemoteMessage.SendMessage("正在拼接[" + m_strTableName + "]的值" + m_strColumns);
            DataRow _drValue = p_drSource;
            DataTable m_dtColumns = GetTargetTable(m_strTableName);
            foreach (string dcItem in m_strColumns.Split(','))
            {
                if (p_drSource.Table.Columns.Contains(dcItem))
                {
                    string _strValue = string.Empty;
                    //if (_drValue[dcItem].ToString().StartsWith("@"))
                    //{
                    //    string _strPatientId = _drValue["PATIENT_ID"].ToString();
                    //    int _iVisitId = int.Parse(_drValue["VISIT_ID"].ToString());
                    //    RemoteMessage.SendMessage("[调取模板]" + _strPatientId + "||" + _iVisitId.ToString() + "||" + _drValue[dcItem].ToString());
                    //    try
                    //    {
                    //        _strValue = CallMrInfo2(_strPatientId, _iVisitId, _drValue[dcItem].ToString());

                    //    }
                    //    catch (Exception exp)
                    //    {
                    //        RemoteMessage.SendMessage("[获取模板信息错误]"+exp.Message);
                    //        m_strValues += "无,";
                    //    }
                    //    RemoteMessage.SendMessage("[值]" + _strValue);
                    //}
                    //else
                    //{
                    _strValue = _drValue[dcItem].ToString();
                    //}
                    string type = m_dtColumns.Columns[dcItem].DataType.Name.ToString();
                    m_strValues += FixDateTime.makeInsertvalue(_strValue, false, m_strTargetDBType, type);
                }
                else
                {
                    m_strValues += "NULL,";
                }
            }
            m_strValues = m_strValues.Trim(',');
            this.m_strValues = m_strValues;
            return m_strValues;
            //RemoteMessage.SendMessage("拼接完成[" + m_strValues + "]");
        }

        /// <summary>
        /// 执行拼接的sql
        /// </summary>
        /// <returns></returns>
        public bool ExeCuteSQL(string p_strTableName, string p_strColumns, string p_strValues)
        {
            //RemoteMessage.SendMessage("开始执行插入");
            string m_strSQL = string.Format("insert into {0} ({1}) values ({2})",
                               p_strTableName,
                               p_strColumns,
                               p_strValues);
            if (CommonFunction.OleExecuteNonQuery(m_strSQL, "TARGET") == 1)
            {
                RemoteMessage.SendMessage("[执行]:" + m_strSQL + "......TRUE");
                return true;
            }
            RemoteMessage.SendMessage("[执行]:" + m_strSQL + "......FALSE");
            CommonFunction.WriteError("执行异常[SQL]:" + m_strSQL);
            return false;
        }

        /// <summary>
        /// 按流程执行操作
        /// </summary>
        public void DoProcess()
        {
            string _strTableName = m_strTableName;
            int m_iItemSuccessCount = 0;
            int m_iItemFalseCount = 0;
            DataTable m_dtSource = GetTableByName(_strTableName);
            string m_strColumns = CombineColumns(_strTableName);
            foreach (DataRow drValue in m_dtSource.Rows)
            {
                string _strPatientId = drValue["PATIENT_ID"].ToString();
                string _strVisitId = drValue["VISIT_ID"].ToString();
                string m_strValues = CombineValues(drValue);
                if (ExeCuteSQL(_strTableName, m_strColumns, m_strValues))
                {
                    m_iItemSuccessCount++;
                    uctlRestoreManage.RemoveRecord(_strTableName, _strPatientId, _strVisitId);
                }
                else
                {
                    m_iItemFalseCount++;
                    uctlRestoreManage.LogFalsePatient(_strTableName,_strPatientId, _strVisitId);
                }
            }
            CommonFunction.WriteLog("[表]" + _strTableName + "[成功]" + m_iItemSuccessCount + "[失败]" + m_iItemFalseCount);
        }

        /// <summary>
        /// 调用模板
        /// 将由逗号分隔的字符串都转换为模板值
        /// 2015-09-16
        /// </summary>
        /// <param name="p_strInsertValue"></param>
        /// <returns></returns>
        public static string CallMrInfo(string p_strPatientId, int p_iVisitId, string p_strInsertValue)
        {
            string[] _arrValue = p_strInsertValue.Split(',');
            string[] _arrValueNew = new string[50];
            for (int i = 0; i < _arrValue.Length; i++)
            {
                if (_arrValue[i].Contains("@"))
                {
                    string _strEleName = _arrValue[i].Remove(1, 1);
                    _arrValueNew[i] = EmrInfo.GetEmrInfo(p_strPatientId, p_iVisitId, _strEleName);
                }
                else
                {
                    _arrValueNew[i] = _arrValue[i];
                }
            }

            return ConvertArrayToString(_arrValueNew);
        }

        /// <summary>
        /// 将字符串数组转换为字符串，以，分隔
        /// 2015-09-16
        /// wuahilong
        /// </summary>
        /// <param name="p_arrValue"></param>
        /// <returns></returns>
        public static string ConvertArrayToString(string[] p_arrValue)
        {

            string _strResult = string.Empty;
            foreach (string var in p_arrValue)
            {
                if (var != null)
                {
                    _strResult += var.ToString() + ",";
                }

            }
            _strResult = _strResult.Trim(',');
            return _strResult;

        }

        /// <summary>
        /// 调用模板将当前模板元素转换为对应值
        /// 2015-09-16
        /// </summary>
        /// <returns></returns>
        public static string CallMrInfo2(string p_strPatientId, int p_iVisitId, string p_strInsertValue)
        {
            string _strResult = "无";
            string _strEleName = p_strInsertValue.Remove(0, 1);
            _strResult = ei.GetMrInfo(p_strPatientId, p_iVisitId, _strEleName);
            if (_strResult == "")
            {
                _strResult = "无";
            }
            return _strResult.Trim();
        }
    }
}
