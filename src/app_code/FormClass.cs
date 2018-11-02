using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;

namespace TPM3.Sys
{
    class FormClass
    {
        /// <summary>
        /// 根据URL创建窗口，并用参数初始化窗口类中的 url 和 paramList 参数
        /// url格式： classname?param1=value1 param2=value2......
        /// </summary>
        public static object GetFormFromUrl(string url)
        {
            string classname = GetClassNameFromUrl(url);
            object obj = CreateClass(classname);
            if( obj is MyBaseForm )
            {
                MyBaseForm bf = obj as MyBaseForm;
                bf.SetFormUrl(url);
            }
            return obj;
        }

        public static string GetClassNameFromUrl(string url)
        {
            string[] ss = url.Split('?');
            if( ss.Length == 0 ) return "";
            return ss[0];
        }

        public static Dictionary<string, string> GetParamsFromUrl(string url)
        {
            Dictionary<string, string> paramList = new Dictionary<string, string>();

            string[] ss = url.Split('?');
            if( ss.Length <= 1 ) return paramList;
            string[] ss2 = ss[1].Split('&');

            foreach( string param in ss2 )
            {   // 每一个值对 a=b
                string[] pair = param.Split('=');
                if( pair.Length == 0 ) continue;
                if( pair.Length == 1 ) paramList[pair[0]] = "";
                if( pair.Length >= 2 ) paramList[pair[0]] = pair[1];
            }
            return paramList;
        }

        /// <summary>
        /// 根据类型反射生成对象
        /// </summary>
        public static object CreateClass(string formClass)
        {
            if( string.IsNullOrEmpty(formClass) ) return null;
            try
            {
                Type t = TypeNameRegister.GetTypeByName(formClass);
                if( t == null ) return null;
                return ClassAccesser.CreateObject(t);
            }
            catch { }  // 创建失败

            return null;
        }
    }
}
