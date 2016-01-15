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

        public void CallInterface(string p_strXml,string p_strDocumentId)
        {
            string _s = es.CallInterface(p_strXml, p_strDocumentId);
        }
    }
}
