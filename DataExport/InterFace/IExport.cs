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

        ///// <summary>
        ///// ��ȡ���󷽷�
        ///// </summary>
        ///// <param name="p_strObjectName"></param>
        ///// <returns></returns>
        //DataTable GetObject(string p_strObjectName);
        
    }
}
