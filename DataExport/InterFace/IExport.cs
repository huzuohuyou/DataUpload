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

        ///// <summary>
        ///// 获取对象方法
        ///// </summary>
        ///// <param name="p_strObjectName"></param>
        ///// <returns></returns>
        //DataTable GetObject(string p_strObjectName);
        
    }
}
