using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

/// <summary>
///XmlRWrite ��ժҪ˵��
/// </summary>
public class XmlRWrite
{
    private string _FilePath;
    private string _ParentNode;
    private string _ChildName;
    private string _AttributeName;

    public XmlRWrite()
    {

    }

    /// <summary>
    /// XML�ļ��洢·��
    /// </summary>
    public string FilePath
    {
        set
        {
            _FilePath = value;
        }
    }

    /// <summary>
    /// �����
    /// </summary>
    public string ParentNode
    {
        set
        {
            _ParentNode = value;
        }
    }

    /// <summary>
    /// �ӽ��
    /// </summary>
    public string ChildName
    {
        set
        {
            _ChildName = value;
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public string AttributeName
    {
        set
        {
            _AttributeName = value;
        }
    }

    /// <summary>
    /// �ݹ��ȡXML�ļ����ֵ
    /// </summary>
    /// <param name="XNL">XmlNodeList�б�</param>
    /// <returns>���ؽ��ֵ</returns>
    private string ReadNodeValue(XmlNodeList XNL)
    {
        string Value = "";
        for (int i = 0; i < XNL.Count; i++)
        {
            if (Value != "")
                break;
            if (XNL[i].Name == _ParentNode)
            {
                foreach (XmlNode Childnode in XNL[i])
                {
                    if (Childnode.Name == _ChildName)
                    {
                        Value = Childnode.InnerText;
                        break;
                    }
                }
            }
            else
            {
                XmlNodeList xnl = XNL[i].ChildNodes;
                if (xnl.Count > 0)
                    Value = ReadNodeValue(xnl);
            }
        }
        return Value;
    }

    /// <summary>
    /// �ݹ��ȡXML�ļ��������ֵ
    /// </summary>
    /// <param name="XNL">XmlNodeList�б�</param>
    /// <returns>���ؽ��ֵ</returns>
    private string ReadAttributeValue(XmlNodeList XNL)
    {
        string Value = "";
        for (int i = 0; i < XNL.Count; i++)
        {
            if (Value != "")
                break;
            if (XNL[i].Name == _ParentNode)
            {
                foreach (XmlNode Childnode in XNL[i])
                {
                    if (Childnode.Name == _ChildName)
                    {
                        Value = Childnode.Attributes[_AttributeName].Value;
                        break;
                    }
                }
            }
            else
            {
                XmlNodeList xnl = XNL[i].ChildNodes;
                if (xnl.Count > 0)
                    Value = ReadAttributeValue(xnl);
            }
        }
        return Value;
    }

    /// <summary>
    /// �ݹ����ý��ֵ
    /// </summary>
    /// <param name="Value">���ֵ</param>
    /// <param name="XNL">XmlNodeList�б�</param>
    private void WriteNodeValue(string Value, XmlNodeList XNL)
    {
        bool bl = false;
        for (int i = 0; i < XNL.Count; i++)
        {
            if (bl)
                break;
            if (XNL[i].Name == _ParentNode)
            {
                foreach (XmlNode Childnode in XNL[i])
                {
                    if (Childnode.Name == _ChildName)
                    {
                        Childnode.InnerText = Value;
                        bl = true;
                        break;
                    }
                }
            }
            else
            {
                XmlNodeList xnl = XNL[i].ChildNodes;
                if (xnl.Count > 0)
                    WriteNodeValue(Value, xnl);
            }
        }
    }

    /// <summary>
    /// �ݹ����ý������ֵ
    /// </summary>
    /// <param name="Value">����ֵ</param>
    /// <param name="XNL">XmlNodeList�б�</param>
    private void WriteAttributeValue(string Value, XmlNodeList XNL)
    {
        bool bl = false;
        for (int i = 0; i < XNL.Count; i++)
        {
            if (bl)
                break;
            if (XNL[i].Name == _ParentNode)
            {
                foreach (XmlNode Childnode in XNL[i])
                {
                    if (Childnode.Name == _ChildName)
                    {
                        Childnode.Attributes[_AttributeName].Value = Value;
                        bl = true;
                        break;
                    }
                }
            }
            else
            {
                XmlNodeList xnl = XNL[i].ChildNodes;
                if (xnl.Count > 0)
                    WriteAttributeValue(Value, xnl);
            }
        }
    }

    private XmlNodeList XNodeList()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(_FilePath);
        XmlNodeList XNL = xmlDoc.SelectSingleNode("configuration").ChildNodes;
        return XNL;
    }

    /// <summary>
    /// �ݹ��ȡ���ֵ
    /// </summary>
    /// <returns>����ֵ</returns>
    public string GetNodeValue()
    {
        XmlNodeList XNL = XNodeList();
        string Value = ReadNodeValue(XNL);
        return Value;
    }

    /// <summary>
    /// �ݹ��ȡ����ֵ
    /// </summary>
    /// <returns>����ֵ</returns>
    public string GetAttributeValue()
    {
        XmlNodeList XNL = XNodeList();
        string Value = ReadAttributeValue(XNL);
        return Value;
    }

    /// <summary>
    /// �ݹ����ý��ֵ
    /// </summary>
    /// <param name="Value">���ֵ</param>
    public void SetNodeValue(string Value)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(_FilePath);
        XmlNodeList XNL = xmlDoc.SelectSingleNode("configuration").ChildNodes;
        WriteNodeValue(Value, XNL);
        xmlDoc.Save(_FilePath);
    }

    /// <summary>
    /// �ݹ����ý������ֵ
    /// </summary>
    /// <param name="Value">����ֵ</param>
    public void SetAttributeValue(string Value)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(_FilePath);
        XmlNodeList XNL = xmlDoc.SelectSingleNode("configuration").ChildNodes;
        WriteAttributeValue(Value, XNL);
        xmlDoc.Save(_FilePath);
    }
}