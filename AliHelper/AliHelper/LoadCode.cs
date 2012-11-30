using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace AliHelper
{
    class LoadCode
    {

        public static object StringEvalToCategoryObject(string code)
        {
            code = System.Web.HttpUtility.HtmlDecode(code);
            code = code.Replace("var ", "Category ").Replace(")", ");").Replace(@"\/", @"/");
            code = code + "return root;";
            string vSource = GenerateCodeBlocks(code, "Category", "");
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameter = new CompilerParameters();
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("Soomes.dll");
            parameter.GenerateExecutable = false; //<--不生成 EXE
            parameter.GenerateInMemory = true; //在内存中运行
            CompilerResults result = provider.CompileAssemblyFromSource(parameter, vSource);
            //动态编译后的结果
            if (result.Errors.Count > 0)
            {
                foreach (CompilerError e in result.Errors)
                {
                    System.Diagnostics.Trace.WriteLine(e.ErrorText);
                }
                throw new Exception(result.Errors[0].ErrorText);
            }
            MethodInfo newMethod = result.CompiledAssembly.GetType("AliHelper.Calculation").GetMethod("dowork");
            return newMethod.Invoke(null, null);

        }

        public static string GenerateCodeBlocks(string formula, string returnType, string paramExp)
        {
            string code =
            "using System;" +
            "using Soomes;" +
            "namespace AliHelper" +
            "{" +
               "public static class Calculation" +
               "{" +
               "public static " + returnType + " dowork(" + paramExp + ")" +
               "{ " + formula +
               ";}}}";
            return code;
        }
    }
}
