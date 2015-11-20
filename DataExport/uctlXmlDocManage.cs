using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using JHEMR.EmrSysDAL;

namespace DataExport
{
    public partial class uctlXmlDocManage : UserControl
    {
        public uctlXmlDocManage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开xml
            OpenXml();
            tvList.Nodes.Clear();
        }
        /// <summary>
        /// 选择打开模板
        /// 转换成树形结构
        /// 将xml结构和默认值保存到数据库
        /// </summary>
        void OpenXml()
        {
            OpenFileDialog ofp = new OpenFileDialog();
            if (ofp.ShowDialog() == DialogResult.OK)
            {
                string _strTempName = ofp.SafeFileName.Replace(".xml", "");
                string strXml = File.ReadAllText(ofp.FileName);
                XmlDocument xmlDoc = new XmlDocument(); 
                xmlDoc.LoadXml(strXml.Trim());
                ConvertToDB(_strTempName, xmlDoc);//保存到数据库
            }
        }

      

        /// <summary>
        /// 保存到数据库开始
        /// </summary>
        void ConvertToDB(string p_strTempletName, XmlDocument p_xmlDoc)
        {
            XmlDocument xmlDoc = p_xmlDoc;
            string _strTempletName = p_strTempletName;
            TreeNode _tnNode = new TreeNode();
            string _strSQL = "select * from emr_xml_dict t where t.temp_NAME = '" + _strTempletName + "'";
            //是否已经有该文件模板
            if (DALUse.Query(_strSQL).Tables[0].Rows.Count < 1)
            {
                string _strTempCode = Guid.NewGuid().ToString();
                _strSQL = "insert into emr_xml_dict (temp_name,temp_code)values( '" + _strTempletName + "','" + _strTempCode + "')";
                DALUse.ExecuteSql(_strSQL);
                ConvertToTreeView(ref _tnNode, xmlDoc.ChildNodes, "header", 0, _strTempCode);//加入第一个节点作为树节点
                tvList.Nodes.Clear();
                _tnNode.Tag = "header";
                _tnNode.Text = _strTempletName;
                tvList.Nodes.Add(_tnNode);
            }
            else
            {
                MessageBox.Show("已存在！");
                return;
            }
           
        }

        /// <summary>
        /// 转换成树形结构
        /// </summary>
        /// <param name="tnNode"></param>
        /// <param name="xmlNodes"></param>
        /// <param name="parentID"></param>
        /// <param name="lvl"></param>
        void ConvertToTreeView(ref TreeNode tnNode, XmlNodeList xmlNodes, string parentID, int lvl ,string p_strTempletCode)
        {
            foreach (XmlNode xnDoc in xmlNodes)
            {
                bool isProperty = false;
                bool isChildNode = false;
                if (xnDoc.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                TreeNode tnDoc = new TreeNode();
                tnDoc.Name = xnDoc.Name;
                tnDoc.Text = xnDoc.Name;
                string NodeIndexID = Guid.NewGuid().ToString();
                tnDoc.Tag = NodeIndexID;

                ///判断属性 
                if (xnDoc.Attributes.Count > 0)
                {
                    ConvertToAttributes(xnDoc.Attributes, NodeIndexID, p_strTempletCode);
                    isProperty = true;
                }
                if (xnDoc.ChildNodes.Count > 0)
                {
                    ConvertToTreeView(ref tnDoc, xnDoc.ChildNodes, NodeIndexID, lvl++, p_strTempletCode);
                }
                ConvertToNode(xnDoc, NodeIndexID, isProperty, isChildNode, parentID, lvl, p_strTempletCode);
                tnNode.Nodes.Add(tnDoc);
            }
        }

        /// <summary>
        /// 转换成节点保存到数据库
        /// </summary>
        /// <param name="xnDoc"></param>
        /// <param name="NodeIndexID"></param>
        /// <param name="isProperty"></param>
        /// <param name="isChildNodes"></param>
        /// <param name="parent_id"></param>
        /// <param name="node_level"></param>
        private void ConvertToNode(XmlNode xnDoc, string NodeIndexID, bool isProperty, bool isChildNodes, string parent_id, int node_level,string p_strTempletCode)
        {
            string _strTempletCode = p_strTempletCode;
            string default_value = xnDoc.InnerText;
            if (xnDoc.ChildNodes.Count > 1)
                default_value = "";
            else if (isInnerText(xnDoc) && default_value != string.Empty)
            {
                default_value = "";
            }
            string is_property = "0";
            if (isProperty)
            {
                is_property = "1";
            }
            string node_id = NodeIndexID;
            string node_name = xnDoc.Name;

            string sql = "insert into emr_xml_elem_dict t (node_id,node_name,is_property,node_level,parent_id,temp_code,default_value)values";
            sql += "('" + node_id + "','" + node_name + "'," + is_property + ",'" + node_level.ToString() + "','" + parent_id + "','" + _strTempletCode + "','" + default_value + "')";
            JHEMR.EmrSysDAL.DALUse.ExecuteSql(sql);
        }

        /// <summary>
        /// 转换为属性，保存到数据库
        /// </summary>
        /// <param name="xmlA"></param>
        /// <param name="NodeIndexID"></param>
        void ConvertToAttributes(XmlAttributeCollection xmlA, string NodeIndexID, string p_strTempletCode)
        {
            string _strTempletCode = p_strTempletCode;
            string[] sqls = new string[xmlA.Count];
            int i = 0;
            foreach (XmlAttribute xmlAb in xmlA)
            {
                string property_id = Guid.NewGuid().ToString();
                string property_name = xmlAb.Name;
                string node_id = NodeIndexID;
                string default_value = xmlAb.Value;
                sqls[i] = ("insert into emr_xml_property_dict (property_name,property_id ,node_id,default_value,temp_code)values('" + property_name + "','" + property_id + "','" + node_id + "','" + default_value + "','" + _strTempletCode + "')");
                i++;
            }
            DALUse.ExecuteSqlTran(sqls);
        }
        /// <summary>
        /// 判断是否为最终节点，以保存其innertext为默认值
        /// </summary>
        /// <param name="xnDoc"></param>
        /// <returns></returns>
        bool isInnerText(XmlNode xnDoc)
        {
            foreach (XmlNode childNode in xnDoc.ChildNodes)
            {
                if (childNode.ChildNodes.Count > 0)
                {
                    return true;
                }
                else if (isInnerText(childNode))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
