using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataExport
{
    interface IExport
    {
        /// <summary>
        /// ��������
        /// </summary>
        void Export();

        /// <summary>
        /// ��¼����ʧ�ܵļ�¼
        /// </summary>
        void LogFalse(List<string> p_list);

        /// <summary>
        /// ����xml����excel��dbf����İ�����sql
        /// </summary>
        /// <param name="p_strObjName"></param>
        /// <returns></returns>
        string SynSQL(string p_strObjName);
        
    }
}
