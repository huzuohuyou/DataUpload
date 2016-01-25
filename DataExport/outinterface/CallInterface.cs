using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DataExport.外部接口
{
    public class UploadInterface
    {
        public static UploadMrInfoService.EmrService es = new DataExport.UploadMrInfoService.EmrService();

        public UploadInterface()
        {
            es.Url = ConfigurationManager.AppSettings["WebServiceUrl"].ToString();
        }

        public  string CallInterface(string p_strKey)
        {
            return  es.CallInterface(p_strKey);
        }
    }
}
