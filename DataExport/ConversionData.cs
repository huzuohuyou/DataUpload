/*----------------------------------------------------------------
            // �ļ�����TransData
            // �ļ����������������ֵ�ת��
            //
            // 
            // ������ʶ���⺣�� 2015-02-03
            //
            // �޸ı�ʶ���⺣�� 2015-10-21
            // �޸��������ع��ֵ�ת�����������߼���
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ToolFunction;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public class ConversionData
    {


        /// <summary>
        /// �����ز���׼�����ݸ�ʽ��Ϊ�м���׼����
        /// 2015-10-20
        /// �⺣��
        /// </summary>
        /// <param name="_dtDictDetail"></param>
        public static void ExchangeData()
        {
            string _strSQL = string.Format("select FIELD_NAME,LOCAL_VALUE,TARGET_VALUE FROM pt_comparison ");
            DataTable _dtDict = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataTable _dtTemp in PublicProperty.ExportData.Tables)
            {
                foreach (DataColumn _dcColumn in _dtTemp.Columns)
                {
                    foreach (DataRow _drTemp in _dtTemp.Rows)
                    {
                        DataRow[] _arrDataRow = _dtDict.Select("FIELD_NAME = '" + _dcColumn.Caption + "'");
                        foreach (DataRow _drDict in _arrDataRow)
                        {
                            if (_drTemp[_dcColumn].ToString() == _drDict["LOCAL_VALUE"].ToString())
                            {
                                RemoteMessage.SendMessage("����ת���ֵ�[" + _dcColumn.Caption.PadRight(30, '.') + "]:" + _drTemp[_dcColumn].ToString().PadRight(5,'��') + _drDict["TARGET_VALUE"].ToString());
                                _drTemp[_dcColumn] = _drDict["TARGET_VALUE"].ToString();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �����ز���׼�����ݸ�ʽ��Ϊ�м���׼����
        /// 2015-10-20
        /// �⺣��
        /// </summary>
        /// <param name="_dtDictDetail"></param>
        public static DataTable ExchangeData(DataTable p_dtOnePatInfo)
        {
            if (p_dtOnePatInfo == null || p_dtOnePatInfo.Rows.Count != 1)
            {
                CommonFunction.WriteError("�޲�����Ϣ.ExchangeDataʧ��");
                return null;
            }
            string _strSQL = string.Format("select FIELD_NAME,LOCAL_VALUE,TARGET_VALUE FROM pt_comparison ");
            DataTable _dtDict = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataColumn _dcColumn in p_dtOnePatInfo.Columns)
            {
                DataRow _drTemp = p_dtOnePatInfo.Rows[0];
                DataRow[] _arrDataRow = _dtDict.Select("FIELD_NAME = '" + _dcColumn.Caption + "'");
                foreach (DataRow _drDict in _arrDataRow)
                {
                    if (_drTemp[_dcColumn].ToString() == _drDict["LOCAL_VALUE"].ToString())
                    {
                        RemoteMessage.SendMessage("����ת���ֵ�[" + _dcColumn.Caption.PadRight(30, '.') + "]:" + _drTemp[_dcColumn].ToString().PadRight(5, '��') + _drDict["TARGET_VALUE"].ToString());
                        _drTemp[_dcColumn] = _drDict["TARGET_VALUE"].ToString();
                    }
                }
            }
            return p_dtOnePatInfo;
        }

      
        

        /// <summary>
        /// ͨ��pt_id��ȡ��Ҫת�����ֵ�
        /// </summary>
        /// <param name="pt_id">ƽ̨id</param>
        /// <returns>�ֵ伯��Ϣ</returns>
        public static void GetPtCompareDictDetail()
        {
            PublicProperty.CompareDict.Tables.Clear();
            DataSet _dsType = GetPtCompareDict(PublicProperty.Pt_Id);
            try
            {
                foreach (DataRow _dr in _dsType.Tables[0].Rows)
                {
                    DataTable _dt = new DataTable(_dr["type_name"].ToString());
                    string strSQL = string.Format("select * from pt_comparison_detail_dict t where pt_id ='{0}'  and type_name = '{1}'", PublicProperty.Pt_Id, _dr["type_name"].ToString());
                    DataSet _ds = DALUse.Query(strSQL);
                    if (_ds.Tables.Count > 0)
                    {
                        _dt = _ds.Tables[0].Copy();
                        _dt.TableName = _dr["type_name"].ToString();
                    }
                    PublicProperty.CompareDict.Tables.Add(_dt);
                }
            }
            catch (Exception exp1)
            {
                CommonFunction.WriteError(exp1.ToString());
            }
        }

        /// <summary>
        /// ͬ��ƽ̨id��ȡ������Ҫת�����м���
        /// </summary>
        /// <param name="_id">ƽ̨id</param>
        /// <returns>��Ҫת�����м���</returns>
        private static DataSet GetPtCompareDict(string _id)
        {
            DataSet _ds = new DataSet();
            string strSQL = string.Format("select distinct type_name from pt_comparison_detail_dict t where pt_id ='{0}'", _id);
            try
            {
                _ds = DALUse.Query(strSQL);
            }
            catch (Exception exp)
            {
                CommonFunction.WriteError(exp.ToString());
            }
            return _ds;
        }


    }
}
