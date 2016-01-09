/*----------------------------------------------------------------
            // 文件名：TransData
            // 文件功能描述：数据字典转换
            //
            // 
            // 创建标识：吴海龙 2015-02-03
            //
            // 修改标识：吴海龙 2015-10-21
            // 修改描述：重构字典转换方法，简化逻辑。
            //
            // 修改标识：
            // 修改描述：
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
        /// 将本地不标准的数据格式化为中间库标准数据
        /// 2015-10-20
        /// 吴海龙
        /// </summary>
        /// <param name="_dtDictDetail"></param>
        public static void ExchangeData()
        {
            string _strSQL = string.Format("select FIELD_NAME,LOCAL_VALUE,TARGET_VALUE FROM pt_comparison ");
            DataTable _dtDict = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataTable _dtTemp in PublicVar.ExportData.Tables)
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
                                RemoteMessage.SendMessage("正在转换字典[" + _dcColumn.Caption.PadRight(30, '.') + "]:" + _drTemp[_dcColumn].ToString().PadRight(5,'　') + _drDict["TARGET_VALUE"].ToString());
                                _drTemp[_dcColumn] = _drDict["TARGET_VALUE"].ToString();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将本地不标准的数据格式化为中间库标准数据
        /// 2015-10-20
        /// 吴海龙
        /// </summary>
        /// <param name="_dtDictDetail"></param>
        public static DataTable ExchangeData(DataTable p_dtOnePatInfo)
        {
            if (p_dtOnePatInfo == null)
            {
                CommonFunction.WriteError("无病人信息.ExchangeData失败");
                return null;
            }
            string _strSQL = string.Format("select FIELD_NAME,LOCAL_VALUE,TARGET_VALUE FROM pt_comparison ");
            DataTable _dtDict = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            foreach (DataColumn _dcColumn in p_dtOnePatInfo.Columns)
            {
                DataRow _drTemp = p_dtOnePatInfo.Rows[0];
                DataRow[] _arrDataRow = _dtDict.Select("FIELD_NAME = '" + _dcColumn.Caption.ToUpper() + "'");
                foreach (DataRow _drDict in _arrDataRow)
                {
                    if (_drTemp[_dcColumn].ToString().Trim() == _drDict["LOCAL_VALUE"].ToString().Trim())
                    {
                        RemoteMessage.SendMessage("正在转换字典[" + _dcColumn.Caption.PadRight(30, '.') + "]:" + _drTemp[_dcColumn].ToString().PadRight(5, '　') + _drDict["TARGET_VALUE"].ToString());
                        _drTemp[_dcColumn] = _drDict["TARGET_VALUE"].ToString();
                    }
                }
            }
            return p_dtOnePatInfo;
        }

      
        

        /// <summary>
        /// 通过pt_id获取需要转换的字典
        /// </summary>
        /// <param name="pt_id">平台id</param>
        /// <returns>字典集信息</returns>
        public static void GetPtCompareDictDetail()
        {
            PublicVar.CompareDict.Tables.Clear();
            DataSet _dsType = GetPtCompareDict(PublicVar.Pt_Id);
            try
            {
                foreach (DataRow _dr in _dsType.Tables[0].Rows)
                {
                    DataTable _dt = new DataTable(_dr["type_name"].ToString());
                    string strSQL = string.Format("select * from pt_comparison_detail_dict t where pt_id ='{0}'  and type_name = '{1}'", PublicVar.Pt_Id, _dr["type_name"].ToString());
                    DataSet _ds = DALUse.Query(strSQL);
                    if (_ds.Tables.Count > 0)
                    {
                        _dt = _ds.Tables[0].Copy();
                        _dt.TableName = _dr["type_name"].ToString();
                    }
                    PublicVar.CompareDict.Tables.Add(_dt);
                }
            }
            catch (Exception exp1)
            {
                CommonFunction.WriteError(exp1.ToString());
            }
        }

        /// <summary>
        /// 同过平台id获取所有需要转化的列集合
        /// </summary>
        /// <param name="_id">平台id</param>
        /// <returns>需要转化的列集合</returns>
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
