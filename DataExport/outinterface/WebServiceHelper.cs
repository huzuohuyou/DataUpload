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
                // 1. 使用 WebClient 下载 WSDL 信息。
                WebClient web = new WebClient();
                Stream stream = web.OpenRead(_strUrl);
                // 2. 创建和格式化 WSDL 文档。
                ServiceDescription description = ServiceDescription.Read(stream);
                // 3. 创建客户端代理代理类。
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap"; // 指定访问协议。
                importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。
                // 4. 使用 CodeDom 编译客户端代理类。
                CodeNamespace nmspace = new CodeNamespace(); // 为代理类添加命名空间，缺省为全局空间。
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = "DynamicWebService.dll"; // 可以指定你所需的任何文件名。
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
                if (result.Errors.HasErrors)
                {
                    // 显示编译错误信息
                }
                //调用程序集文件演示
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
        /// 动态调用WebService   
        /// </summary>   
        /// <param name="url">WebService地址</param>   
        /// <param name="classname">类名</param>   
        /// <param name="methodname">方法名(模块名)</param>   
        /// <param name="args">参数列表</param>   
        /// <returns>object</returns>   
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            if (classname == null || classname == "")
            {
                classname = WebServiceHelper.GetClassName(url);
            }
            //获取服务描述语言(WSDL)   
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //生成客户端代理类代码   
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();
            //设定编译器的参数   
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类   
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
            //生成代理实例,并调用方法   
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
        /// 根据WebService的URL，生成一个本地的dll,放在C盘下面，例如：C:|DBMS_WebService.dll  
        /// 创建人：程媛媛 创建时间：2010-6-21  
        /// </summary>  
        /// <param name="url">WebService的UR</param>  
        /// <returns></returns>  
        public static void CreateWebServiceDLL(string url)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            string classname = WebServiceHelper.GetClassName(url);
            // 1. 使用 WebClient 下载 WSDL 信息。  
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url + "?WSDL");
            // 2. 创建和格式化 WSDL 文档。  
            ServiceDescription description = ServiceDescription.Read(stream);
            // 3. 创建客户端代理代理类。  
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // 指定访问协议。  
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。  
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。  
            // 4. 使用 CodeDom 编译客户端代理类。  
            CodeNamespace nmspace = new CodeNamespace(@namespace); // 为代理类添加命名空间，缺省为全局空间。  
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = Application.StartupPath + "//DBMS_Service.dll";  // 可以指定你所需的任何文件名。  
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // 显示编译错误信息  
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
        /// 根据WebService的URL，生成一个本地的dll,并返回WebService的实例  
        /// 创建人：程媛媛 创建时间：2010-6-21  
        /// </summary>  
        /// <param name="url">WebService的UR</param>  
        /// <returns></returns>  
        public static object GetWebServiceInstance(string url)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            string classname = WebServiceHelper.GetClassName(url);
            // 1. 使用 WebClient 下载 WSDL 信息。  
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url + "?WSDL");
            // 2. 创建和格式化 WSDL 文档。  
            ServiceDescription description = ServiceDescription.Read(stream);
            // 3. 创建客户端代理代理类。  
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // 指定访问协议。  
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。  
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。  
            // 4. 使用 CodeDom 编译客户端代理类。  
            CodeNamespace nmspace = new CodeNamespace(@namespace); // 为代理类添加命名空间，缺省为全局空间。  
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = Application.StartupPath + "//DBMS_Service.dll"; // 可以指定你所需的任何文件名。  

            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // 显示编译错误信息  
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in result.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            //生成代理实例  
            System.Reflection.Assembly assembly = Assembly.Load("DBMS_Service");
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            return obj;
        }  

    }
}
