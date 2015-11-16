using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ToolFunction;
using JHEMR.EmrSysDAL;

namespace DataExport
{

    public class clsUploadDataToDataBase
    {
        public static string  pt_id = "";
        //public static int inputok = 0;
        private static int okcount = 0;
        private static int falsecount = 0;
        public static void InsertDataIntoTarget1(object o)
        {
            CommonFunction.WriteErrorLog("" + DateTime.Now.ToString() + "进行数据导入....详细信息如下");
            CommonFunction cf = new CommonFunction();
            cf.WaitingThreadStart();
            try
            {
                DataSet ds = (DataSet)o;
                //foreach (DataTable dt in ds.Tables)
                //{
                 InsertDataIntoTarget(ds);
                //}
                
            }
            catch (Exception ex)
            {
                CommonFunction.WriteErrorLog(ex.ToString());
            }
            cf.WaitingThreadStop();
            string mess = ""+DateTime.Now.ToString()+"数据导出完成\n导出成功" + okcount.ToString() + "条； \n 导出失败" + falsecount.ToString() + "条； \n 详情信息查看Errorlog.txt!";
            CommonFunction.WriteErrorLog(mess + "\n");
            ToolFunction.uctlMessageBox.frmDisappearShow(mess);
        }
        public static bool InsertDataIntoTarget(DataSet source)
        {
            bool result = true;
            string errormess = "";
            string connstr = "";
            List<string> sqllist = new List<string>();
            try
            {
                //修改人:吴海龙;修改时间2014-07-19;修改原因:从pt_tables_dict 设置那些表导出
                //DataTable targettable = DALUse.Query(string.Format("select * from PT_Up_DataBase_Table where pt_id ='{0}'", pt_id)).Tables[0];
                DataTable targettable = DALUse.Query(string.Format("select * from pt_tables_dict where pt_id ='{0}' and exportflag = 'TRUE'", pt_id)).Tables[0];
                connstr = DALUse.Query(string.Format("select * from PT_Setting where pt_id ='{0}'", pt_id)).Tables[0].Rows[0]["connstr"].ToString();
                string dbType = StrConvertToDateTime.getClientConnectType(connstr);
                foreach (DataRow drtarget in targettable.Rows)
                {//修改PT_TABLE->TABLE_NAME
                    //string sql1 = string.Format("select * from PT_TARGET_FIELD where table_name = '{0}' ", drtarget["TABLE_NAME"].ToString());
                    string sql1 = string.Format("select * from PT_comparison where pt_id ='{1}' and  table_name = '{0}' ", drtarget["TABLE_NAME"].ToString(), pt_id);
                    DataTable targetcolumns = DALUse.Query(sql1).Tables[0];
                    //构造插入的列
                    string insertcolumns = "";
                    foreach (DataRow drcolumnname in targetcolumns.Rows)//拼接插入的列
                    {
                        insertcolumns += drcolumnname["FIELD"].ToString() + ",";
                    }
                    insertcolumns = insertcolumns.Trim(',');
                    DataTable dtsource = null;
                    foreach (DataTable dt1 in source.Tables)
                    {
                        if (dt1.TableName.Contains(drtarget["ID"].ToString()))
                        {
                            dtsource = dt1;
                        }
                    }
                    if (dtsource==null)
                    {
                        clsWriteErrorLogToDataBase.WriteErrorLogTodataBase(drtarget["table_name"].ToString()+"未导出！");
                        continue;
                    }
                    //构造插入的行
                    //------------
                    foreach (DataRow drinsertdata in dtsource.Rows)
                    {
                        string sql = "";
                        string insertvalue = "";
                        foreach (string dcitem in insertcolumns.Split(','))
                        {

                            string sqldatatype = string.Format("select * from PT_TARGET_FIELD where pt_id = '{0}' and table_name = '{1}' and field = '{2}'", pt_id, drtarget["TABLE_NAME"].ToString(), dcitem.ToString());
                            string dataType = DALUse.Query(sqldatatype).Tables[0].Rows[0]["FIELD_TYPE"].ToString();
                            insertvalue += StrConvertToDateTime.makeInsertvalue(drinsertdata[dcitem.ToUpper()].ToString(), false, StrConvertToDateTime.getClientConnectType("TargetConnection"), dataType);
                            //bool valueflag = false;//指定列是否赋值。
                            //foreach (DataColumn dcdata in dtsource.Columns)
                            //{
                            //    //2014-12-30  吴海龙 优化赋值过程
                            //    if (dcitem.ToUpper().Equals(getFieldName(pt_id, drtarget["TABLE_NAME"].ToString(), dcdata.ToString()).ToUpper()))
                            //    {
                            //        //insertvalue += "'" + drinsertdata[dcitem].ToString() + "',";
                            //        //insertvalue += StrConvertToDateTime.makeInsertvalue(drinsertdata[dcdata.ToString()].ToString(), false, StrConvertToDateTime.getClientConnectType("TargetConnection"), dataType);
                            //        valueflag = true;
                            //        //StrConvertToDateTime.ToDate("2014-06-19 11:11:11", false, dbType);
                            //    }
                            //}
                            //if (!valueflag)
                            //{
                            //    insertvalue += "'',";
                            //}
                            //----------------------
                            //string sqldatatype = string.Format("select * from PT_TARGET_FIELD where pt_id = '{0}' and table_name = '{1}' and field = '{2}'", pt_id, drtarget["TABLE_NAME"].ToString(), dcitem.ToString());
                            //string dataType = DALUse.Query(sqldatatype).Tables[0].Rows[0]["FIELD_TYPE"].ToString();
                            //bool valueflag = false;//指定列是否赋值。
                            //foreach (DataColumn dcdata in dtsource.Columns)
                            //{
                            //    //2014-12-30  吴海龙 优化赋值过程
                            //    if (dcitem.ToUpper().Equals(getFieldName(pt_id, drtarget["TABLE_NAME"].ToString(), dcdata.ToString()).ToUpper()))
                            //    {
                            //        //insertvalue += "'" + drinsertdata[dcitem].ToString() + "',";
                            //        insertvalue += StrConvertToDateTime.makeInsertvalue(drinsertdata[dcdata.ToString()].ToString(), false, StrConvertToDateTime.getClientConnectType("TargetConnection"), dataType);
                            //        valueflag = true;
                            //        //StrConvertToDateTime.ToDate("2014-06-19 11:11:11", false, dbType);
                            //    }
                            //}
                            //if (!valueflag)
                            //{
                            //    insertvalue += "'',";
                            //}
                        }
                        insertvalue = insertvalue.Trim(',');
                        sql = string.Format("insert into {0} ({1}) values ({2})",
                            drtarget["TABLE_NAME"].ToString(),
                            insertcolumns,
                            insertvalue);
                        sqllist.Add(sql);
                        //CommonFunction.WriteErrorLog("插入目标表sql："+sql);
                        errormess = sql;
                        ToolFunction.clsProperty.insertcount = ToolFunction.clsProperty.insertcount++;
                        try//针对插入操作上传日志。
                        {
                            if (DALUseSpecial.ExecuteSql(sql, connstr) == 1)
                            {
                                okcount++;
                                result = true;
                            }
                            else
                            {
                                falsecount++;
                                result = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            falsecount++;
                            result = false;
                            CommonFunction.WriteErrorLog("插入目标表sql：" + sql + "\n" + ex.ToString());
                            continue;
                        }
                    }
                    //------------------
                }
            }
            catch (Exception ex)
            {
                result = false;
                //throw;
                CommonFunction.WriteErrorLog(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 取出对比字段
        /// </summary>
        /// <param name="pt_id">平台名</param>
        /// <param name="table_name">表名</param>
        /// <param name="compare_name">字段名</param>
        /// <returns></returns>
        public static string getFieldName(string pt_id, string table_name,string compare_name)
        {
            string result = "";
            try
            {
                DataSet ds = DALUse.Query(string.Format("select * from PT_COMPARISON where pt_id = '{0}' and table_name = '{1}' and compare_name = '{2}'", pt_id, table_name, compare_name));
                if (ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
                {
                     result = ds.Tables[0].Rows[0]["field"].ToString();
                }
            }
            catch (Exception ex)
            {
                CommonFunction.WriteErrotLog(ex.ToString());
            }
            return result;
        }
    }
}
