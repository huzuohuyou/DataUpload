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
        public static int successcount = 0, falsecount = 0;
        /// <summary>
        /// ���鲡�����ݼ�
        /// </summary>
        public static DataSet dsCut = new DataSet();

        /// <summary>
        ///  ���˷�����Ŀ
        /// </summary>
        public static int GroupCount = 10;
        /// <summary>
        /// ���������������
        /// </summary>
        public static object[] ExportParam = new object[6];
        /// <summary>
        /// ��ȡ����SQL
        /// </summary>
        public static string SqlGetPT = "select distinct(PT_ID),PT_NAME from pt_dict";
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public static string DatabaseType = "";
        /// <summary>
        /// �ֵ���Ϣ
        /// </summary>
        public static DataSet CompareDict = new DataSet();
        /// <summary>
        /// ��ѯ�����ĵ�����Ϣ
        /// </summary>
        public static DataSet ExportData = new DataSet();
        /// <summary>
        /// �������ݼ�
        /// </summary>
        public static DataTable m_dsPatients = null;
        /// <summary>
        /// ��ѯ��SQL���
        /// </summary>
        public static DataTable m_dtSQL = new DataTable();
        /// <summary>
        /// ƽ̨id
        /// </summary>
        public static string Pt_Id = "aa823d8a-0e9d-4fdc-b220-e815185c0cce";
        /// <summary>
        /// ƽ̨����
        /// </summary>
        public static string Pt_Name = "";
        /// <summary>
        /// ����excel������·��
        /// </summary>
        public static string ExcelPath = "";
        /// <summary>
        /// ����excel���ݣ�datatable
        /// </summary>
        public static DataTable ExcelSource = null;
        /// <summary>
        /// �Ƿ�ѡ��Ժʱ��
        /// </summary>
        public static bool CheckOutHospitalDate = true;
        /// <summary>
        /// ��Ժ��ʼʱ��
        /// </summary>
        public static string OutDateSta = "";
        /// <summary>
        /// ��Ժ��ֹʱ��
        /// </summary>
        public static string OutDateEnd = "";
        /// <summary>
        /// ��ȡ������Ϣ��SQL
        /// </summary>
        public static string ExcuteSQL = "";
        /// <summary>
        /// ����Excel
        /// </summary>
        public const string CExportExcel = "EXCEL";
        /// <summary>
        /// ����DBF
        /// </summary>
        public const string CExportDBF = "DBF";
        /// <summary>
        /// �������ݿ�
        /// </summary>
        public const string CExportDB = "DB";

    }
}
