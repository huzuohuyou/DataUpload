using System;
using System.Collections.Generic;
using System.Text;
using ToolFunction;
using System.Data;

namespace DataExport
{
    public class PublicVar
    {
        public static string m_strCurrentPatientId = string.Empty;
        public static string m_strCurrentVisitId = string.Empty;
        public static string m_strEmrConnection = "EMR";
        public static int m_nSuccessCount = 0, m_nFalseCount = 0;
        /// <summary>
        /// 分组病人数据集
        /// </summary>
        public static DataSet dsCut = new DataSet();

        /// <summary>
        ///  病人分组数目
        /// </summary>
        public static int GroupCount = 10;
        /// <summary>
        /// 导出数据所需参数
        /// </summary>
        public static object[] ExportParam = new object[6];
        /// <summary>
        /// 获取科室SQL
        /// </summary>
        public static string SqlGetPT = "select distinct(PT_ID),PT_NAME from pt_dict";
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DatabaseType = "";
        /// <summary>
        /// 字典信息
        /// </summary>
        public static DataSet CompareDict = new DataSet();
        /// <summary>
        /// 查询出来的导入信息
        /// </summary>
        public static DataSet ExportData = new DataSet();
        /// <summary>
        /// 病人数据集
        /// </summary>
        public static DataTable m_dsPatients = null;
        /// <summary>
        /// 查询的SQL语句
        /// </summary>
        public static DataTable m_dtSQL = new DataTable();
        /// <summary>
        /// 平台id
        /// </summary>
        public static string Pt_Id = "aa823d8a-0e9d-4fdc-b220-e815185c0cce";
        /// <summary>
        /// 平台名称
        /// </summary>
        public static string Pt_Name = "";
        /// <summary>
        /// 导出excel参数：路径
        /// </summary>
        public static string ExcelPath = "";
        /// <summary>
        /// 导出excel数据：datatable
        /// </summary>
        public static DataTable ExcelSource = null;
        /// <summary>
        /// 是否勾选出院时间
        /// </summary>
        public static bool CheckOutHospitalDate = true;
        /// <summary>
        /// 出院开始时间
        /// </summary>
        public static string OutDateSta = "";
        /// <summary>
        /// 出院截止时间
        /// </summary>
        public static string OutDateEnd = "";
        /// <summary>
        /// 获取病人信息的SQL
        /// </summary>
        public static string ExcuteSQL = "";
        /// <summary>
        /// 导出Excel
        /// </summary>
        public const string CExportExcel = "EXCEL";
        /// <summary>
        /// 导出DBF
        /// </summary>
        public const string CExportDBF = "DBF";
        /// <summary>
        /// 导入数据库
        /// </summary>
        public const string CExportDB = "DB";

    }
}
