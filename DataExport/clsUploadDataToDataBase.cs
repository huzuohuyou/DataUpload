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
            CommonFunction.WriteErrorLog("" + DateTime.Now.ToString() + "�������ݵ���....��ϸ��Ϣ����");
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
            string mess = ""+DateTime.Now.ToString()+"���ݵ������\n�����ɹ�" + okcount.ToString() + "���� \n ����ʧ��" + falsecount.ToString() + "���� \n ������Ϣ�鿴Errorlog.txt!";
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
                //�޸���:�⺣��;�޸�ʱ��2014-07-19;�޸�ԭ��:��pt_tables_dict ������Щ����
                //DataTable targettable = DALUse.Query(string.Format("select * from PT_Up_DataBase_Table where pt_id ='{0}'", pt_id)).Tables[0];
                DataTable targettable = DALUse.Query(string.Format("select * from pt_tables_dict where pt_id ='{0}' and exportflag = 'TRUE'", pt_id)).Tables[0];
                connstr = DALUse.Query(string.Format("select * from PT_Setting where pt_id ='{0}'", pt_id)).Tables[0].Rows[0]["connstr"].ToString();
                string dbType = StrConvertToDateTime.getClientConnectType(connstr);
                foreach (DataRow drtarget in targettable.Rows)
                {//�޸�PT_TABLE->TABLE_NAME
                    //string sql1 = string.Format("select * from PT_TARGET_FIELD where table_name = '{0}' ", drtarget["TABLE_NAME"].ToString());
                    string sql1 = string.Format("select * from PT_comparison where pt_id ='{1}' and  table_name = '{0}' ", drtarget["TABLE_NAME"].ToString(), pt_id);
                    DataTable targetcolumns = DALUse.Query(sql1).Tables[0];
                    //����������
                    string insertcolumns = "";
                    foreach (DataRow drcolumnname in targetcolumns.Rows)//ƴ�Ӳ������
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
                        clsWriteErrorLogToDataBase.WriteErrorLogTodataBase(drtarget["table_name"].ToString()+"δ������");
                        continue;
                    }
                    //����������
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
                            //bool valueflag = false;//ָ�����Ƿ�ֵ��
                            //foreach (DataColumn dcdata in dtsource.Columns)
                            //{
                            //    //2014-12-30  �⺣�� �Ż���ֵ����
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
                            //bool valueflag = false;//ָ�����Ƿ�ֵ��
                            //foreach (DataColumn dcdata in dtsource.Columns)
                            //{
                            //    //2014-12-30  �⺣�� �Ż���ֵ����
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
                        //CommonFunction.WriteErrorLog("����Ŀ���sql��"+sql);
                        errormess = sql;
                        ToolFunction.clsProperty.insertcount = ToolFunction.clsProperty.insertcount++;
                        try//��Բ�������ϴ���־��
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
                            CommonFunction.WriteErrorLog("����Ŀ���sql��" + sql + "\n" + ex.ToString());
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
        /// ȡ���Ա��ֶ�
        /// </summary>
        /// <param name="pt_id">ƽ̨��</param>
        /// <param name="table_name">����</param>
        /// <param name="compare_name">�ֶ���</param>
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
