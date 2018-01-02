using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Builder
{
    /// <summary>
    /// 代码生成工具类
    /// </summary>
    public static class BuilderTools
    {
        /// <summary>
        /// 获取字段类型对应属性
        /// </summary>
        public static string GetAttrType(string columnType)
        {
            string AttrType = "";
            switch (columnType.ToLower())
            {
                case "int":
                case "long":
                    AttrType = "int";
                    break;
                case "bool":
                case "bit":
                    AttrType = "bool";
                    break;
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "char":
                case "text":
                case "varchar":
                case "string":
                    AttrType = "string";
                    break;
                case "datetime":
                    AttrType = "DateTime";
                    break;
                case "uniqueidentifier":
                    AttrType = "Guid";
                    break;
                case "decimal":
                case "double":
                case "float":
                    AttrType = "double";
                    break;
                default: break;
            }
            return AttrType;
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        public static string LoadTemplate(string filepath)
        {
            string filePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TemplateDir"] + filepath);
            //打开文件
            StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("utf-8"));
            //读取流
            string tempContent = reader.ReadToEnd();
            reader.Dispose();
            return tempContent;
        }



    }
}
