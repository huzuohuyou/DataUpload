using System;
using System.Collections.Generic;
using System.Text;

namespace DataExport
{
    public class PatientIndex
    {
        public string m_strMrBackDateTime = string.Empty;
        public string m_strInpNo = string.Empty;
        public string m_strPatientId = string.Empty;
        public string m_strVisitId = string.Empty;

        public PatientIndex(string p_strInpNo, string p_strPatientId, string p_strVisitId, string p_strMrBackDateTime)
        {
            m_strInpNo = p_strInpNo;
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
            m_strMrBackDateTime = p_strMrBackDateTime;
        }

    }
}
