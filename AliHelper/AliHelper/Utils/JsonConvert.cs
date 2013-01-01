using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace AliHelper
{
    static class JsonConvert
    {
        /// <summary>
        /// 序列化数据为Json数据格式.
        /// </summary>
        /// <param name="value">被序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object value)
        {
            Type type = value.GetType();
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Newtonsoft.Json.Formatting.None;
            writer.QuoteChar = '"';
            json.Serialize(writer, value);
            string output = sw.ToString();
            writer.Close();
            sw.Close();
            return output;
        }
        /// <summary>
        /// 将Json数据转为对象
        /// </summary>
        /// <typeparam name="T">目标对象</typeparam>
        /// <param name="jsonText">json数据字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(this string jsonText)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            StringReader sr = new StringReader(jsonText);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            T result = (T)json.Deserialize(reader, typeof(T));
            reader.Close();
            return result;
        }
    }
}
