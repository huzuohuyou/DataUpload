using System;
using System.Collections.Generic;
using System.Text;
using ToolFunction;
using System.Data;
using System.Configuration;

namespace DataExport
{
    public class SingleObjectDBExport: IExport
    {
        public static EmrInfo ei = new EmrInfo();
        public static string m_strTableName = string.Empty;
        public static string m_strColumns = string.Empty;
        public static string m_strValues = string.Empty;
        public DataSet m_dsSource = null;
        public static string m_strDbType = ConfigurationManager.AppSettings["DBTYPE"].ToUpper();
        public static string m_strTargetDBType = ConfigurationManager.AppSettings["TARGETDBTYPE"].ToUpper();
        public DataTable m_dtSource = null;
        public DataTable m_dtColumns = null;

        public SingleObjectDBExport(string p_strTableName, DataSet p_dsSource)
        {
            m_strTableName = p_strTableName;
            m_dsSource = p_dsSource;
        }

        public SingleObjectDBExport(DataTable p_dtSource)
        {
            m_dtSource = p_dtSource;
        }

        /// <summary>
        /// ͨ�����ֻ�ȡm_dsSource��ָ������datatable
        /// ����ֵ��m_dtSource
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

        public static DataTable GetTargetTable(string p_strTableName)
        {
            string m_strSQL = "select * from " + p_strTableName + " where 1=0 ";
            DataTable m_dtColumns = CommonFunction.OleExecuteBySQL(m_strSQL, "", "TARGET");
            return m_dtColumns;
        }

        /// <summary>
        /// �������ɲ���� ����
        /// ����ֵ��m_strColumns
        /// </summary>
        /// <returns></returns>
        public static string CombineColumns(string p_strTableName)
        {
            string m_strTableName = p_strTableName;
            m_strColumns = string.Empty;
            RemoteMessage.SendMessage("����ƴ��[" + m_strTableName + "]����");
            DataTable m_dtColumns = GetTargetTable(p_strTableName);
            foreach (DataColumn drColumnName in m_dtColumns.Columns)//ƴ�Ӳ������
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
            return m_strColumns;
            //RemoteMessage.SendMessage("ƴ�����[" + m_strColumns + "]");
        }

        /// <summary>
        /// �������ɲ���� ����
        /// ����ֵ��m_strValues
        /// </summary>
        /// <returns></returns>
        public static string CombineValues(DataRow p_drSource)
        {
            m_strValues = string.Empty;
            //RemoteMessage.SendMessage("����ƴ��[" + m_strTableName + "]��ֵ" + m_strColumns);
            DataRow _drValue = p_drSource;
            DataTable m_dtColumns = GetTargetTable(m_strTableName);
            foreach (string dcItem in m_strColumns.Split(','))
            {
                if (p_drSource.Table.Columns.Contains(dcItem))
                {
                    string _strValue = string.Empty;
                    if (_drValue[dcItem].ToString().StartsWith("@"))
                    {
                        string _strPatientId = _drValue["PATIENT_ID"].ToString();
                        int _iVisitId = int.Parse(_drValue["VISIT_ID"].ToString());
                        RemoteMessage.SendMessage("[��ȡģ��]" + _strPatientId + "||" + _iVisitId.ToString() + "||" + _drValue[dcItem].ToString());
                        try
                        {
                            _strValue = CallMrInfo2(_strPatientId, _iVisitId, _drValue[dcItem].ToString());

                        }
                        catch (Exception exp)
                        {
                            RemoteMessage.SendMessage("[��ȡģ����Ϣ����]"+exp.Message);
                            m_strValues += "��,";
                        }
                        RemoteMessage.SendMessage("[ֵ]" + _strValue);
                    }
                    else
                    {
                        _strValue = _drValue[dcItem].ToString();
                    }
                    string type = m_dtColumns.Columns[dcItem].DataType.Name.ToString();
                    m_strValues += FixDateTime.makeInsertvalue(_strValue, false, m_strTargetDBType, type);
                }
                else
                {
                    m_strValues += "NULL,";
                }
            }
            m_strValues = m_strValues.Trim(',');
            return m_strValues;
            //RemoteMessage.SendMessage("ƴ�����[" + m_strValues + "]");
        }

        /// <summary>
        /// ִ��ƴ�ӵ�sql
        /// </summary>
        /// <returns></returns>
        public static bool ExeCuteSQL(string p_strTableName, string p_strColumns, string p_strValues)
        {
            //RemoteMessage.SendMessage("��ʼִ�в���");
            string m_strSQL = string.Format("insert into {0} ({1}) values ({2})",
                               p_strTableName,
                               p_strColumns,
                               p_strValues);
           
            if (CommonFunction.OleExecuteNonQuery(m_strSQL, "TARGET") > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ִ�в������
        /// </summary>
        /// <param name="p_dtPatObj"></param>
        public static void DoImport(DataTable p_dtPatObj)
        {
            m_strTableName = p_dtPatObj.TableName;
            m_strColumns = CombineColumns(m_strTableName);
            foreach (DataRow drValue in p_dtPatObj.Rows)
            {
                string m_strValues = CombineValues(drValue);
                if (!ExeCuteSQL(m_strTableName, m_strColumns, m_strValues))
                {
                    RemoteMessage.SendMessage("[ִ��].....................FALSE");
                    PublicVar.m_nFalseCount++;
                    WritLog();
                }
                else
                {
                    RemoteMessage.SendMessage("[ִ��].....................TRUE");
                    PublicVar.m_nSuccessCount++;
                }
            }

        }

        public static void WritLog() { }

        /// <summary>
        /// ������ִ�в���
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
                string m_strValues = CombineValues(drValue);
                if (ExeCuteSQL(_strTableName, m_strColumns, m_strValues))
                {
                    m_iItemSuccessCount++;
                }
                else
                {
                    m_iItemFalseCount++;
                }
            }
            CommonFunction.WriteLog("[��]" + _strTableName + "[�ɹ�]" + m_iItemSuccessCount + "[ʧ��]" + m_iItemFalseCount);
        }

        /// <summary>
        /// ����ģ��
        /// ���ɶ��ŷָ����ַ�����ת��Ϊģ��ֵ
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
        /// ���ַ�������ת��Ϊ�ַ������ԣ��ָ�
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
        /// ����ģ�彫��ǰģ��Ԫ��ת��Ϊ��Ӧֵ
        /// 2015-09-16
        /// </summary>
        /// <returns></returns>
        public static string CallMrInfo2(string p_strPatientId, int p_iVisitId, string p_strInsertValue)
        {
            string _strResult = "��";
            string _strEleName = p_strInsertValue.Remove(0, 1);
            _strResult = ei.GetMrInfo(p_strPatientId, p_iVisitId, _strEleName);
            if (_strResult == "")
            {
                _strResult = "��";
            }
            return _strResult.Trim();
        }

        #region IExport ��Ա

        public void Export()
        {
            DoImport(m_dtSource);
        }

        #endregion



        #region IExport ��Ա


        public void LogFalse(List<string> p_list)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IExport ��Ա


        public string SynSQL(string p_strObjName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
