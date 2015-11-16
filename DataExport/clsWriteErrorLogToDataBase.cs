using System;
using System.Collections.Generic;
using System.Text;
using ToolFunction;
using JHEMR;
using JHEMR.EmrSysDAL;
namespace DataExport
{
    public class clsWriteErrorLogToDataBase
    {
        public static void WriteErrorLogTodataBase(string mess)
        {
            string sqlerror = "";
            try
            {
                string s = mess.Replace("'", "''");
                sqlerror = string.Format("insert into PT_ERROR_LOG values('{0}','{1}','{2}')", Guid.NewGuid(), DateTime.Now.ToString(), s);
                DALUse.ExecuteSql(sqlerror);
            }
            catch (Exception ex)
            {
                CommonFunction.WriteErrotLog(ex.Message + "=====" + sqlerror);
            }
        }
    }
}
