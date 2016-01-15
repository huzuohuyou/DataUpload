using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ToolFunction;
using JHEMR.EmrSysDAL;
using System.Configuration;
using System.Threading;
using ConfirmFileName;
using System.IO;
using System.Windows.Forms;

namespace DataExport
{

    public class ExportDB : IExport,IValidate
    {
        public static object[] ExportParam = new object[6];
        public static EmrInfo ei = new EmrInfo();
        public static string m_strTimeRange = string.Empty;
        public static string m_strDbType = ConfigurationManager.AppSettings["DBTYPE"].ToUpper();
        public static string m_strTargetDBType = ConfigurationManager.AppSettings["TARGETDBTYPE"].ToUpper();
        public static string m_strSQL = string.Empty;
        public static string m_strMess = string.Empty;
        public SingleObjectDBExport m_MyParam = null;
        public static DataSet m_dsSource = new DataSet();


        private static void InsertDataIntoTarget(object o)
        {
            DataSet ds = (DataSet)o;
            Insert(ds);
        }

        public static void Insert(DataSet p_dsSource)
        {
            CommonFunction.WriteLog("=====================" + m_strTimeRange + "=====================");
            try
            {
                m_dsSource = p_dsSource;
                //修改人:吴海龙;修改时间2014-07-19;修改原因:从pt_tables_dict 设置那些表导出
                m_strSQL = string.Format("select * from pt_tables_dict where  exportflag = 'TRUE'");
                DataTable _dtTarget = CommonFunction.OleExecuteBySQL(m_strSQL, "", "EMR");
                foreach (DataRow drTarget in _dtTarget.Rows)
                {
                    RemoteMessage.SendMessage("开启" + drTarget["TABLE_NAME"].ToString() + "数据上传线程");
                    SingleObjectDBExport p = new SingleObjectDBExport(drTarget["TABLE_NAME"].ToString(), p_dsSource);
                    p.DoProcess();
                    //Thread t = new Thread(new ThreadStart(p.DoProcess));
                    //t.Start();
                }
            }
            catch (Exception exp)
            {
                RemoteMessage.SendMessage("[异常]:" + exp.Message);
            }
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
        /// 调用模板将当前模板元素转换为对应值
        /// 2015-09-16
        /// </summary>
        /// <returns></returns>
        public static string CallMrInfo2(string p_strPatientId, int p_iVisitId, string p_strInsertValue)
        {
            string _strResult = "无";
            string _strEleName = p_strInsertValue.Remove(0, 1);
            _strResult = ei.GetMrInfo(p_strPatientId, p_iVisitId, _strEleName);
            if (_strResult=="")
            {
                _strResult = "无";
            }
            return _strResult.Trim();
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
        /// 取出对比字段
        /// </summary>
        /// <param name="pt_id">平台名</param>
        /// <param name="table_name">表名</param>
        /// <param name="compare_name">字段名</param>
        /// <returns></returns>
        public static string getFieldName(string pt_id, string table_name, string compare_name)
        {
            string result = "";
            DataSet ds = DALUse.Query(string.Format("select * from PT_COMPARISON where pt_id = '{0}' and table_name = '{1}' and compare_name = '{2}'", pt_id, table_name, compare_name));
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["field"].ToString();
            }
            return result;
        }

        #region IExport 成员

        public void Export()
        {
            InsertDataIntoTarget(PublicVar.ExportParam[0]);
        }

        /// <summary>
        /// 获取导出对象的数据标识集合
        /// </summary>
        /// <param name="p_strOjbectName"></param>
        /// <returns></returns>
        public static DataTable GetObject(string p_strOjbectName)
        {
            DataTable _dtTemp = new DataTable();
            _dtTemp.Columns.Add("CLASS");
            _dtTemp.Columns.Add("CHAPTER_NAME");
            //_dtTemp.Columns.Add("DATA_DETAIL");
            string _strSQL = string.Format(@"selct * from '{0}' where 1=0",p_strOjbectName);
            DataTable _dtObject = CommonFunction.OleExecuteBySQL(_strSQL, "", "TARGET");
            foreach (DataColumn var in _dtObject.Columns)
            {
                _dtTemp.Rows.Add("FILE", var.Caption.ToUpper());
            }
            return _dtTemp;
        }

        #endregion


        #region IValidate 成员

        public void ValidateAll(DataTable p_dt)
        {
            CommonFunction.WriteError("validate", "====================================================华丽分割线====================================================", false);
            string _strError = string.Empty;
            foreach (DataRow var in p_dt.Rows)
            {
                CommonFunction.WriteError("validate", ValidateData(var["TABLE_NAME"].ToString()));
            }
        }

        public string ValidateData(string p_strTableName)
        {
            ShowFixInfo.m_strWarning = String.Empty;
            string _strFalse = string.Empty;
            ShowFixInfo.m_dtSource.Rows.Clear();
            string _strSQLValue = GetSQL(p_strTableName);
            string _strSQL = string.Format("select * from {0} where 1=0", p_strTableName);
            DataTable _dtTarget = CommonFunction.OleExecuteBySQL(_strSQL, "", "TARGET");
            if (_dtTarget == null)
            {
                return ShowFixInfo.m_strWarning += "目标库中无表" + p_strTableName;
            }
            _strSQL = _strSQLValue.ToUpper().Trim().Replace("@PATIENT_ID", "1").Replace("@VISIT_ID", "1");
            DataTable _dtLocal = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtLocal == null)
            {
                ShowFixInfo.m_strWarning += "SQL语句有误" + _strSQL;
            }
            else
            {
                if ((!_dtLocal.Columns.Contains("PATIENT_ID") || !_dtLocal.Columns.Contains("VISIT_ID")))
                {
                    ShowFixInfo.m_strWarning += ("警告:不存在 PATIENT_ID , VISIT_ID 列");
                }
                foreach (DataColumn var in _dtTarget.Columns)
                {
                    if (_dtLocal.Columns.Contains(var.Caption))
                    {
                        ShowFixInfo.m_dtSource.Rows.Add(var.Caption, "TRUE");
                    }
                    else
                    {
                        ShowFixInfo.m_dtSource.Rows.Add(var.Caption, "FALSE");
                        _strFalse += "[" + var.Caption + "]";
                    }
                }
            }
            if (_strFalse=="")
            {
                 ShowFixInfo.m_strWarning += "对象:[" + p_strTableName + "] 对照条目完整";
                 return ShowFixInfo.m_strWarning;
            }
             ShowFixInfo.m_strWarning += "对象:[" + p_strTableName + "] 无对照条目:" + _strFalse;
             return ShowFixInfo.m_strWarning;
        }

        #endregion

        /// <summary>
        /// 以目标表 的名称保存sql脚本到根目录 的SQL文件夹中
        /// </summary>
        /// <param name="p_strSQL">sql脚本</param>
        /// <param name="p_strFileName">目标表的名称</param>
        public static bool SaveSQL(string p_strSQL, string p_strFileName)
        {
            try
            {
                string _strPath = Application.StartupPath + "/sql/";
                if (!Directory.Exists(_strPath))
                {
                    Directory.CreateDirectory(_strPath);
                }
                _strPath = _strPath + p_strFileName + ".sql";
                using (StreamWriter sw = new StreamWriter(_strPath, false, Encoding.UTF8))
                {
                    sw.WriteLine(p_strSQL);
                    return true;
                }
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError(exp.ToString());
                return false;
            }
        }

        /// <summary>
        /// 通过目标表的名称获取sql脚本
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetSQL(string p_strFileName)
        {
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/SQL/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".sql";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                return string.Empty;
            }
            return _strSQL;
        }

        /// <summary>
        /// 获取绑定的章节
        /// 2015-11-06
        /// 吴海龙
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <returns></returns>
        public static DataTable GetChapter(string p_strObjectName)
        {
            string _strSQL = string.Format(@"select CHAPTER_NAME,TABLE_NAME,CLASS,DATA_DETAIL,'' CHOSE from pt_chapter_dict where table_name = '{0}'", p_strObjectName);
            return CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
        }

        internal static string GetObjectBandedSQL(string _strObjectName)
        {
            string _strBandedSQL = " SELECT \n PATIENT_ID,\n VISIT_ID, ";
            string _strSQL = string.Format(@"select * from {0} where 1=0", _strObjectName);
            DataTable _dtObject = CommonFunction.OleExecuteBySQL(_strSQL, "", "TARGET");
            if (null==_dtObject)
            {
                return "目标库中无表" + _strObjectName;
            }
            foreach (DataColumn var in _dtObject.Columns)
            {
                _strBandedSQL += "\n " + var.Caption.ToUpper() + ",";
            }
            _strBandedSQL=_strBandedSQL.Trim(',');
            _strBandedSQL += "\n FROM " + _strObjectName + "\n WHERE ";
            return _strBandedSQL;
        }

        #region IExport 成员


        public void LogFalse(List<string> p_list)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IExport 成员


        public string SynSQL(string p_strObjName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
