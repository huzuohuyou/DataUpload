using System.IO;
using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Reflection;
using System;
using Microsoft.CSharp;
using System.Text;
using System.Windows.Forms;


namespace DataExport
{
    public class WebServiceHelper
    {

        public static void Test()
        {
            try
            {
                string _strUrl = uctlBaseConfig.GetConfig("WebServiceUrl");
                // 1. ʹ�� WebClient ���� WSDL ��Ϣ��
                WebClient web = new WebClient();
                Stream stream = web.OpenRead(_strUrl);
                // 2. �����͸�ʽ�� WSDL �ĵ���
                ServiceDescription description = ServiceDescription.Read(stream);
                // 3. �����ͻ��˴�������ࡣ
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap"; // ָ������Э�顣
                importer.Style = ServiceDescriptionImportStyle.Client; // ���ɿͻ��˴���
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                importer.AddServiceDescription(description, null, null); // ��� WSDL �ĵ���
                // 4. ʹ�� CodeDom ����ͻ��˴����ࡣ
                CodeNamespace nmspace = new CodeNamespace(); // Ϊ��������������ռ䣬ȱʡΪȫ�ֿռ䡣
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = "DynamicWebService.dll"; // ����ָ����������κ��ļ�����
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
                if (result.Errors.HasErrors)
                {
                    // ��ʾ���������Ϣ
                }
                //���ó����ļ���ʾ
                Assembly asm = Assembly.LoadFrom("DynamicWebService.dll");
                Type t = asm.GetType("WebService");
                object o = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("HelloWorld");
                object oo = method.Invoke(o, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>   
        /// ��̬����WebService   
        /// </summary>   
        /// <param name="url">WebService��ַ</param>   
        /// <param name="classname">����</param>   
        /// <param name="methodname">������(ģ����)</param>   
        /// <param name="args">�����б�</param>   
        /// <returns>object</returns>   
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            if (classname == null || classname == "")
            {
                classname = WebServiceHelper.GetClassName(url);
            }
            //��ȡ������������(WSDL)   
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //���ɿͻ��˴��������   
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();
            //�趨�������Ĳ���   
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //���������   
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            //���ɴ���ʵ��,�����÷���   
            Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(methodname);
            return mi.Invoke(obj, args);
        }

        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        /// <summary>  
        /// ����WebService��URL������һ�����ص�dll,����C�����棬���磺C:|DBMS_WebService.dll  
        /// �����ˣ������� ����ʱ�䣺2010-6-21  
        /// </summary>  
        /// <param name="url">WebService��UR</param>  
        /// <returns></returns>  
        public static void CreateWebServiceDLL(string url)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            string classname = WebServiceHelper.GetClassName(url);
            // 1. ʹ�� WebClient ���� WSDL ��Ϣ��  
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url + "?WSDL");
            // 2. �����͸�ʽ�� WSDL �ĵ���  
            ServiceDescription description = ServiceDescription.Read(stream);
            // 3. �����ͻ��˴�������ࡣ  
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // ָ������Э�顣  
            importer.Style = ServiceDescriptionImportStyle.Client; // ���ɿͻ��˴���  
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            importer.AddServiceDescription(description, null, null); // ��� WSDL �ĵ���  
            // 4. ʹ�� CodeDom ����ͻ��˴����ࡣ  
            CodeNamespace nmspace = new CodeNamespace(@namespace); // Ϊ��������������ռ䣬ȱʡΪȫ�ֿռ䡣  
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = Application.StartupPath + "//DBMS_Service.dll";  // ����ָ����������κ��ļ�����  
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // ��ʾ���������Ϣ  
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in result.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
        }

        /// <summary>  
        /// ����WebService��URL������һ�����ص�dll,������WebService��ʵ��  
        /// �����ˣ������� ����ʱ�䣺2010-6-21  
        /// </summary>  
        /// <param name="url">WebService��UR</param>  
        /// <returns></returns>  
        public static object GetWebServiceInstance(string url)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            string classname = WebServiceHelper.GetClassName(url);
            // 1. ʹ�� WebClient ���� WSDL ��Ϣ��  
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url + "?WSDL");
            // 2. �����͸�ʽ�� WSDL �ĵ���  
            ServiceDescription description = ServiceDescription.Read(stream);
            // 3. �����ͻ��˴�������ࡣ  
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // ָ������Э�顣  
            importer.Style = ServiceDescriptionImportStyle.Client; // ���ɿͻ��˴���  
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            importer.AddServiceDescription(description, null, null); // ��� WSDL �ĵ���  
            // 4. ʹ�� CodeDom ����ͻ��˴����ࡣ  
            CodeNamespace nmspace = new CodeNamespace(@namespace); // Ϊ��������������ռ䣬ȱʡΪȫ�ֿռ䡣  
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = Application.StartupPath + "//DBMS_Service.dll"; // ����ָ����������κ��ļ�����  

            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // ��ʾ���������Ϣ  
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in result.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            //���ɴ���ʵ��  
            System.Reflection.Assembly assembly = Assembly.Load("DBMS_Service");
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            return obj;
        }  

    }
}
