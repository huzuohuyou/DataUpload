using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataExport
{
    interface IExport
    {
        /// <summary>
        /// 导出方法
        /// </summary>
        void Export();

        /// <summary>
        /// 记录导出失败的记录
        /// </summary>
        void LogFalse(List<string> p_list);

        /// <summary>
        /// 生成xml、表、excel、dbf对象的绑定数据sql
        /// </summary>
        /// <param name="p_strObjName"></param>
        /// <returns></returns>
        string SynSQL(string p_strObjName);
        
    }
}
