using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using ToolFunction;

namespace DataExport
{
    public class EmrInfo
    {
        public static ServiceBase.WebService.DynamicWebLoad.Service pDBMS_Service;

        public static UploadMrInfoService.EmrService es = new DataExport.UploadMrInfoService.EmrService();

        public EmrInfo()
        {
            es.Url = ConfigurationManager.AppSettings["WebServiceUrl"].ToString();
        }

        public static void SetUrl()
        {
            es.Url = ConfigurationManager.AppSettings["WebServiceUrl"].ToString();
        }

        /// <summary>
        /// 获取病历模板信息
        /// 2015-09-09
        /// 吴海龙
        /// </summary>
        /// <param name="strPatientId">病人id</param>
        /// <param name="nVisitID">住院次</param>
        /// <param name="p_strElemName">元素名</param>
        /// <returns></returns>
        public static string GetEmrInfo(string p_strPatientId, int p_nVisitId, string p_strElemName)
        {
            return "";
            //string _strResult = es.GetMRInfoByEleMentName(p_strPatientId, p_nVisitId, p_strElemName);
            //return _strResult;

            //string _strWebServiceUrl = ConfigurationManager.AppSettings["WebServiceUrl"].ToString();
            //object o = WebServiceHelper.GetWebServiceInstance(_strWebServiceUrl);
            //pDBMS_Service = o as ServiceBase.WebService.DynamicWebLoad.Service;
            //string _strResult = pDBMS_Service.GetMRInfoByEleMentName(p_strPatientId, p_nVisitId, p_strElemName);
            //return _strResult;
        }

        /// <summary>
        /// 获取病历模板信息
        /// 2015-09-018
        /// 吴海龙
        /// </summary>
        /// <param name="strPatientId">病人id</param>
        /// <param name="nVisitID">住院次</param>
        /// <param name="p_strElemName">元素名</param>
        /// <returns></returns>
        public string GetMrInfo(string p_strPatientId, int p_nVisitId, string p_strElemName)
        {
            return "";

            //string _strResult = es.GetMRInfoByEleMentName(p_strPatientId, p_nVisitId, p_strElemName);
            //return _strResult;
        }

    }
}
