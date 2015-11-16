using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using JHEMR.EmrSysDAL;
using ToolFunction;

namespace DataExport
{
    public class clsConvertDataToXML
    {

       
        private DataTable source = new DataTable();
        private DataTable dtlayout = new DataTable();

        public static void DoConvert(DataTable source, string pt_id, string filename)
        {
            XmlDocument doc = new XmlDocument();
            string sqlgetlayout = string.Format("select * from pt_xml_config where pt_id = '{0}'", pt_id);
            DataTable dtlayout = DALUse.Query(sqlgetlayout).Tables[0];
            DataRow[] drroot = dtlayout.Select("parient_id is null");
            XmlNode root = doc.CreateElement(drroot[0]["field_name"].ToString());
            root.InnerText = "";
            doc.AppendChild(root);

            DataRow[] drsecondes = dtlayout.Select(string.Format("parient_id ='{0}'", drroot[0]["id"].ToString()));
            if (drsecondes.Length == 0)
            {
                return;
            }
            foreach (DataRow drdata in source.Rows)
            {
                XmlElement xn = doc.CreateElement(drsecondes[0]["field_name"].ToString());
                root.AppendChild(xn);
                foreach (DataRow drfield in dtlayout.Rows)
                {
                    foreach (DataColumn columnname in source.Columns)
                    {
                        if (columnname.ToString().ToUpper().Equals(drfield["field"].ToString().ToUpper()))
                        {
                            XmlNode xnleaf = doc.CreateElement(drfield["field_name"].ToString());
                            xnleaf.InnerText = drdata[drfield["field"].ToString()].ToString();
                            xn.AppendChild(xnleaf);
                        }
                    }

                }
            }
            CommonFunction.ConverDataSetToXMLFile(doc.InnerXml, filename + ".xml");

        }
        public string DoConvert1(DataTable dtsource, string pt_id, string filename)
        {
            try
            {
                string sqlgetlayout = string.Format("select * from pt_xml_config where pt_id = '{0}'", pt_id);
                dtlayout = DALUse.Query(sqlgetlayout).Tables[0];
                this.source = dtsource;
                DataRow[] drroot = dtlayout.Select("parient_id is null");
                foreach (DataRow drlayout in dtlayout.Rows)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlNode root = doc.CreateElement(drroot[0]["field_name"].ToString());
                    root.InnerText = "";
                    doc.AppendChild(root);
                    getNode(drroot[0], root, doc);
                    CommonFunction.ConverDataSetToXMLFile(doc.InnerXml, filename+Guid.NewGuid() + ".xml");
                }
            }
            catch (Exception ex)
            {
                CommonFunction.WriteErrotLog(ex.ToString());
            }
            return "";
        }
        /// <summary>
        /// 传入根节点后，遍历节点生成相应格式xml
        /// </summary>
        /// <param name="drnode">叶子节点集合</param>
        /// <param name="xn">初始为根节点，以后为各个节点</param>
        public void getNode(DataRow drnode, XmlNode xn,XmlDocument doc)
        {
            DataRow[] nodes = dtlayout.Select(string.Format("parient_id ='{0}'", drnode["id"].ToString()));
            foreach (DataRow mynode in nodes)
            {
                XmlNode xnleaf = null;
                bool hascreated = false;
                if (mynode["multiple"].ToString() != "" && mynode["field"].ToString() == "")//有子节点
                {
                    //foreach (DataRow drdata in source.Rows)//遍历取数据
                    //{
                    xnleaf = doc.CreateElement(mynode["field_name"].ToString());
                    //xnleaf.InnerText = drdata[mynode["field"].ToString()].ToString();
                    xn.AppendChild(xnleaf);
                    //}
                    getNode(mynode, xnleaf,doc);
                }
                else if (mynode["multiple"].ToString() == "" && mynode["field"].ToString() == "")
                {
                    continue;
                }
                else//叶子节点
                {
                    if (mynode["multiple"].ToString() == "YES")//多重节点
                    {
                        foreach (DataRow drdata in source.Rows)//遍历取数据
                        {
                            if (drdata[mynode["field"].ToString()].ToString() == "")//merge出来的会有空数据，不是所需数据，跳过。
                            {
                                continue;
                            }
                            xnleaf = doc.CreateElement(mynode["field_name"].ToString());
                            xnleaf.InnerText = drdata[mynode["field"].ToString()].ToString();
                            xn.AppendChild(xnleaf);
                        }
                    }
                    else if (mynode["multiple"].ToString() == "NO" && !hascreated)//单节点
                    {
                        xnleaf = doc.CreateElement(mynode["field_name"].ToString());
                        foreach (DataRow drdata in source.Rows)//遍历取数据
                        {
                            if (drdata[mynode["field"].ToString()].ToString() == "")//merge出来的会有空数据，不是所需数据，跳过。
                            {
                                continue;
                            }
                            xnleaf.InnerText = drdata[mynode["field"].ToString()].ToString();
                            hascreated = true;//仅需一次赋值穿件一次节点。
                        }
                        xn.AppendChild(xnleaf);
                    }
                }
            }
        }
        public string getNodedata(string field)
        {
            return "";
        }
    }
}
